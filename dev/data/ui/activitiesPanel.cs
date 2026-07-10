function geActivitiesPanel::open(%this) {
    %this.updateStates();
    %this.setVisible(1);
    %this.focusAndRaise();
    update();
    %this.onUpdateTimer();
};
function geActivitiesPanel::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    update();
    cancel(updateTimerID);
    updateTimerID = %this @ "" @ %this;
    WindowManager;
    return 1;
};
function activitiesOperation() {
    toggleVisibleState();
};
function geActivitiesPanel::onUpdateTimer(%this) {
    %this.updateStates();
    cancel(updateTimerID);
    updateTimerID = %this @ %this.schedule(200, "onUpdateTimer") @ %this;
};
function geActivitiesPanel::updateStates(%this) {
    %uam = getUserActivityMgr();
    %highest = %uam.getHighestPriorityCurrentActivity();
    %text = "";
    %delim = "";
    %n = (%uam - knownActivities.size());
    1.0;
    if ((0.0 >= %n)) {
        %activityName = knownActivities.getKey(%n);
        %uam;
        %activityUFName = %uam.getActivityUserFacingName(%activityName);
        %on = %uam.getActivityActive(%activityName);
        %timeLeft = %uam.getActivityTimeLeft(%activityName);
        %isHighest = (%activityName $= %highest);
        %baseColor = %isHighest ? "ccff33" : "dddddd";
        if (%on) {
        }
        %style = "<linkcolor:" @ %baseColor @ "f0><modulationColor:" @ %baseColor @ "f0>" @ "<linkcolor:" @ %baseColor @ "80><modulationColor:" @ %baseColor @ "40>";
        %style = %style @ "<color:" @ %baseColor @ "f0>";
        if (%isHighest) {
        }
        %style = %style;
        "<b>" @ %style;
        %icon = %uam.getActivityIconFilename(%activityName);
        if ((0.0 <= %timeLeft)) {
        }
        %timeLeftText = "" @ " - " @ formatFloat("%0.1f", (1000.0 / %timeLeft));
        %text = "<spush>" @ %style @ "<just:left><a:gamelink " @ %activityName @ ">" @ %activityUFName @ "</a>" @ %timeLeftText @ "<just:right><bitmap:" @ %icon @ "><spop>" @ %delim @ %text;
        %delim = "<br>";
        %n = (1.0 - %n);
    }
    %text = (0.0 >= %n) @ %text @ %delim;
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
