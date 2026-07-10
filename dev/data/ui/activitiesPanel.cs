function geActivitiesPanel::open(%this) {
    %this.updateStates();
    1.setVisible(%this);
    %this.focusAndRaise(playGui);
    WindowManager.update();
    %this.onUpdateTimer();
};
function geActivitiesPanel::close(%this) {
    0.setVisible(%this);
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
    %this.updateTimerID = "onUpdateTimer".schedule(%this, 200);
};
function geActivitiesPanel::updateStates(%this) {
    %uam = getUserActivityMgr();
    %highest = %uam.getHighestPriorityCurrentActivity();
    %text = "";
    %delim = "";
    %n = (%uam.knownActivities.size() - 1.0);
    while ((%n >= 0.0)) {
        %activityName = %n.getKey(%uam.knownActivities);
        %activityUFName = %activityName.getActivityUserFacingName(%uam);
        %on = %activityName.getActivityActive(%uam);
        %timeLeft = %activityName.getActivityTimeLeft(%uam);
        %isHighest = (%activityName $= %highest);
        %baseColor = %isHighest ? "ccff33" : "dddddd";
        if (%on) {
        }
        %style = "<linkcolor:" @ %baseColor @ "80><modulationColor:" @ %baseColor @ "40>";
        %style = %style @ "<color:" @ %baseColor @ "f0>";
        if (%isHighest) {
        }
        %style = %style;
        %icon = %activityName.getActivityIconFilename(%uam);
        if ((%timeLeft <= 0.0)) {
        }
        %timeLeftText = " - " @ formatFloat("%0.1f", (%timeLeft / 1000.0));
        %text = "<spush>" @ %style @ "<just:left><a:gamelink " @ %activityName @ ">" @ %activityUFName @ "</a>" @ %timeLeftText @ "<just:right><bitmap:" @ %icon @ "><spop>" @ %delim @ %text;
        %delim = "<br>";
        %n = (%n - 1.0);
    }
    %text = %text @ %delim;
    %timeSinceLastReport = %uam.getLastReportAgeMS();
    %timeSinceLastReport = mFloor((%timeSinceLastReport / 1000.0));
    %text = %text @ %delim @ "<just:left><color:a09000>last report:" @ " " @ secondsToHHMMSS(%timeSinceLastReport);
    %timeToNextReport = %uam.getMSToNextReport();
    if ((%timeToNextReport > 0.0)) {
        %timeToNextReport = formatFloat("%.1f", (%timeToNextReport / 1000.0));
        %text = %text @ %delim @ "<just:left><color:907000>reports paused for" @ " " @ %timeToNextReport @ "s..";
    }
    %text.setText(geActivitiesPanel_Current);
};
function geActivitiesPanel_Current::onUrl(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %url = restWords(%url);
    }
    %activityName = firstWord(%url);
    %uam = getUserActivityMgr();
    !(%activityName.getActivityActive(%uam)).setActivityActive(%uam, %activityName);
};
