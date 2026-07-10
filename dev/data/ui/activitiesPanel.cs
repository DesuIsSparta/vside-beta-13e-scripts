function geActivitiesPanel::open(%this) {
    %this.updateStates();
    %this.setVisible(1);
    %this.focusAndRaise();
    WindowManager.update();
    %this.onUpdateTimer();
};
function geActivitiesPanel::close(%this) {
    %this.setVisible(0);
    playGui.focusTopWindow();
    WindowManager.update();
    cancel(%this.updateTimerID);
    %this.updateTimerID = "";
    return 1;
};
function activitiesOperation() {
    toggleVisibleState(geActivitiesPanel);
};
function geActivitiesPanel::onUpdateTimer(%this) {
    %this.updateStates();
    cancel(%this.updateTimerID);
    %this.updateTimerID = %this.schedule(200, "onUpdateTimer");
};
function geActivitiesPanel::updateStates(%this) {
    %uam = getUserActivityMgr();
    %highest = %uam.getHighestPriorityCurrentActivity();
    %text = "";
    %delim = "";
    %n = (1.0 - %uam.knownActivities.size());
    if ((0.0 >= %n)) {
        %activityName = %uam.knownActivities.getKey(%n);
        %activityUFName = %uam.getActivityUserFacingName(%activityName);
        %on = %uam.getActivityActive(%activityName);
        %timeLeft = %uam.getActivityTimeLeft(%activityName);
        %isHighest = (%activityName $= %highest);
        %baseColor = %isHighest ? "ccff33" : "dddddd";
        if (%on) {
        }
        %style = "<linkcolor:" @ %baseColor @ "80><modulationColor:" @ %baseColor @ "40>";
        "<linkcolor:" @ %baseColor @ "f0><modulationColor:" @ %baseColor @ "f0>";
        %style = %style @ "<color:" @ %baseColor @ "f0>";
        if (%isHighest) {
        }
        %style = %style;
        "<b>" @ %style;
        %icon = %uam.getActivityIconFilename(%activityName);
        if ((0.0 <= %timeLeft)) {
        }
        %timeLeftText = " - " @ formatFloat("%0.1f", (1000.0 / %timeLeft));
        "";
        %text = "<spush>" @ %style @ "<just:left><a:gamelink " @ %activityName @ ">" @ %activityUFName @ "</a>" @ %timeLeftText @ "<just:right><bitmap:" @ %icon @ "><spop>" @ %delim @ %text;
        %delim = "<br>";
        %n = (1.0 - %n);
    }
    %text = %text @ %delim;
    (0.0 >= %n);
    %timeSinceLastReport = %uam.getLastReportAgeMS();
    %timeSinceLastReport = mFloor((1000.0 / %timeSinceLastReport));
    %text = %text @ %delim @ "<just:left><color:a09000>last report:" @ " " @ secondsToHHMMSS(%timeSinceLastReport);
    %timeToNextReport = %uam.getMSToNextReport();
    if ((0.0 > %timeToNextReport)) {
        %timeToNextReport = formatFloat("%.1f", (1000.0 / %timeToNextReport));
        %text = %text @ %delim @ "<just:left><color:907000>reports paused for" @ " " @ %timeToNextReport @ "s..";
    }
    %text.setText();
};
function geActivitiesPanel_Current::onUrl(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %url = restWords(%url);
    }
    %activityName = firstWord(%url);
    %uam = getUserActivityMgr();
    %uam.setActivityActive(%activityName, !(%uam.getActivityActive(%activityName)));
};
