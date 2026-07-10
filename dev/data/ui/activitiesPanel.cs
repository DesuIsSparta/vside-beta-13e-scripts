function geActivitiesPanel::open(%this)
{
    %this.updateStates();
    %this.setVisible(1);
    playGui.focusAndRaise(%this);
    WindowManager.update();
    %this.onUpdateTimer();
}
function geActivitiesPanel::close(%this)
{
    %this.setVisible(0);
    playGui.focusTopWindow();
    WindowManager.update();
    cancel(%this.updateTimerID);
    %this.updateTimerID = "";
    return 1;
}
function activitiesOperation()
{
    toggleVisibleState(geActivitiesPanel);
}
function geActivitiesPanel::onUpdateTimer(%this)
{
    %this.updateStates();
    cancel(%this.updateTimerID);
    %this.updateTimerID = %this.schedule(200, "onUpdateTimer");
}
function geActivitiesPanel::updateStates(%this)
{
    %uam = getUserActivityMgr();
    %highest = %uam.getHighestPriorityCurrentActivity();
    %text = "";
    %delim = "";
    %n = (%uam.knownActivities.size() - 1.0);
    while ((%n >= 0.0))
    {
        %activityName = %uam.knownActivities.getKey(%n);
        %activityUFName = %uam.getActivityUserFacingName(%activityName);
        %on = %uam.getActivityActive(%activityName);
        %timeLeft = %uam.getActivityTimeLeft(%activityName);
        %isHighest = (%activityName $= %highest);
        %baseColor = %isHighest ? "ccff33" : "dddddd";
        if (%on)
        {
        }
        else
        {
        }
        %style = "<linkcolor:" @ %baseColor @ "80><modulationColor:" @ %baseColor @ "40>";
        "<linkcolor:" @ %baseColor @ "f0><modulationColor:" @ %baseColor @ "f0>";
        %style = %style @ "<color:" @ %baseColor @ "f0>";
        if (%isHighest)
        {
        }
        else
        {
        }
        %style = %style;
        "<b>" @ %style;
        %icon = %uam.getActivityIconFilename(%activityName);
        if ((%timeLeft <= 0.0))
        {
        }
        else
        {
        }
        %timeLeftText = " - " @ formatFloat("%0.1f", (%timeLeft / 1000.0));
        "";
        %text = "<spush>" @ %style @ "<just:left><a:gamelink " @ %activityName @ ">" @ %activityUFName @ "</a>" @ %timeLeftText @ "<just:right><bitmap:" @ %icon @ "><spop>" @ %delim @ %text;
        %delim = "<br>";
        %n = (%n - 1.0);
    }
    %text = %text @ %delim;
    (%n >= 0.0);
    %timeSinceLastReport = %uam.getLastReportAgeMS();
    %timeSinceLastReport = mFloor((%timeSinceLastReport / 1000.0));
    %text = %text @ %delim @ "<just:left><color:a09000>last report:" @ " " @ secondsToHHMMSS(%timeSinceLastReport);
    %timeToNextReport = %uam.getMSToNextReport();
    if ((%timeToNextReport > 0.0))
    {
        %timeToNextReport = formatFloat("%.1f", (%timeToNextReport / 1000.0));
        %text = %text @ %delim @ "<just:left><color:907000>reports paused for" @ " " @ %timeToNextReport @ "s..";
    }
    geActivitiesPanel_Current.setText(%text);
}
function geActivitiesPanel_Current::onUrl(%this, %url)
{
    if ((firstWord(%url) $= "gamelink"))
    {
        %url = restWords(%url);
    }
    %activityName = firstWord(%url);
    %uam = getUserActivityMgr();
    %uam.setActivityActive(%activityName, !%uam.getActivityActive(%activityName));
}
