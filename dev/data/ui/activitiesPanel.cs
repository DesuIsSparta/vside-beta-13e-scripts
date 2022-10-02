function geActivitiesPanel::open(%this)
{
    %this.updateStates();
    %this.setVisible(1);
    playGui.focusAndRaise(%this);
    WindowManager.update();
    %this.onUpdateTimer();
    return ;
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
    return ;
}
function geActivitiesPanel::onUpdateTimer(%this)
{
    %this.updateStates();
    cancel(%this.updateTimerID);
    %this.updateTimerID = %this.schedule(200, "onUpdateTimer");
    return ;
}
function geActivitiesPanel::updateStates(%this)
{
    %uam = getUserActivityMgr();
    %highest = %uam.getHighestPriorityCurrentActivity();
    %text = "";
    %delim = "";
    %n = %uam.knownActivities.size() - 1;
    while (%n >= 0)
    {
        %activityName = %uam.knownActivities.getKey(%n);
        %activityUFName = %uam.getActivityUserFacingName(%activityName);
        %on = %uam.getActivityActive(%activityName);
        %timeLeft = %uam.getActivityTimeLeft(%activityName);
        %isHighest = %activityName $= %highest;
        %baseColor = %isHighest ? "ccff33" : "dddddd";
        %style = %on ? "<linkcolor:" : "<linkcolor:";
        %style = %style @ "<color:" @ %baseColor @ "f0>";
        %style = %isHighest ? "<b>" : %style;
        %icon = %uam.getActivityIconFilename(%activityName);
        %timeLeftText = %timeLeft <= 0 ? "" : " - ";
        %text = "<spush>" @ %style @ "<just:left><a:gamelink " @ %activityName @ ">" @ %activityUFName @ "</a>" @ %timeLeftText @ "<just:right><bitmap:" @ %icon @ "><spop>" @ %delim @ %text;
        %delim = "<br>";
        %n = %n - 1;
    }
    %text = %text @ %delim;
    %timeSinceLastReport = %uam.getLastReportAgeMS();
    %timeSinceLastReport = mFloor(%timeSinceLastReport / 1000);
    %text = %text @ %delim @ "<just:left><color:a09000>last report:" SPC secondsToHHMMSS(%timeSinceLastReport);
    %timeToNextReport = %uam.getMSToNextReport();
    if (%timeToNextReport > 0)
    {
        %timeToNextReport = formatFloat("%.1f", %timeToNextReport / 1000);
        %text = %text @ %delim @ "<just:left><color:907000>reports paused for" SPC %timeToNextReport @ "s..";
    }
    geActivitiesPanel_Current.setText(%text);
    return ;
}
function geActivitiesPanel_Current::onUrl(%this, %url)
{
    if (firstWord(%url) $= "gamelink")
    {
        %url = restWords(%url);
    }
    %activityName = firstWord(%url);
    %uam = getUserActivityMgr();
    %uam.setActivityActive(%activityName, !%uam.getActivityActive(%activityName));
    return ;
}
