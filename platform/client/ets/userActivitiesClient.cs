function UserActivityMgr::defineActivities(%this)
{
    %this.defineActivity("idle", "idle", -1);
    %this.defineActivity("dressing", "dressing", -1);
    %this.defineActivity("shoppingForClothes", "clothes shopping", -1);
    %this.defineActivity("decorating", "decorating", -1);
    %this.defineActivity("gaming", "gaming", -1);
    %this.defineActivity("wrestling", "wrestling", -1);
    %this.defineActivity("discovering", "exploring", -1);
    %this.defineActivity("chatting", "chatting", 15000);
    %this.defineActivity("dancing", "dancing", 45000);
    %this.defineActivity("traveling", "traveling", -1);
}
function getUserActivityMgr()
{
    if (!isObject(gUserActivityMgr))
    {
        echo(getScopeName() @ " " @ "- initializing");
        safeNewScriptObject("ScriptObject", "gUserActivityMgr", 0);
        gUserActivityMgr.bindClassName("UserActivityMgr");
        gUserActivityMgr.knownActivities = safeNewScriptObject("Array", "", 0);
        gUserActivityMgr.currActivities = safeNewScriptObject("StringMap", "", 0);
        gUserActivityMgr.reportTimer = "";
        gUserActivityMgr.defineActivities();
        gUserActivityMgr.reset();
    }
    return gUserActivityMgr;
}
function UserActivityMgr::reset(%this)
{
    echo(getScopeName());
    %this.currActivities.clear();
    %this.lastReportTimeMS = 0;
    %this.maxReportPeriodMS = 20 * 1000;
    cancel(%this.reportTimer);
    %this.reportTimer = "";
}
function UserActivityMgr::defineActivity(%this, %activityName, %userFacingName, %duration)
{
    %userFacingName = isDefined("%userFacingName") ? %userFacingName : %activityName;
    %duration = isDefined("%duration") ? %duration : -1;
    %params = %userFacingName @ "\t" @ %duration;
    %this.knownActivities.put(%activityName, %params);
    if (!(isFile(%this.getActivityIconFilename(%activityName) @ ".png")))
    {
        error(getScopeName() @ " " @ "- no icon for" @ " " @ %activityName @ " " @ %this.getActivityIconFilename(%activityName));
    }
}
function UserActivityMgr::isKnownActivity(%this, %activityName, %warn)
{
    %known = %this.knownActivities.hasKey(%activityName);
    if (!%known && isDefined("%warn") && %warn)
    {
        error(getScopeName() @ " " @ "- unknown activity: \"" @ %activityName @ "\"." @ " " @ getTrace());
    }
    return %known;
}
function UserActivityMgr::getActivityIconFilename(%this, %activityName)
{
    if (%activityName $= "")
    {
        %activityName = "none";
    }
    return "platform/client/ui/activities/activity_" @ %activityName;
}
function UserActivityMgr::getActivityUserFacingName(%this, %activityName)
{
    if (%activityName $= "")
    {
        return "";
    }
    if (!%this.isKnownActivity(%activityName, 1))
    {
        return "[" @ %activityName @ "]";
    }
    else
    {
        return getField(%this.knownActivities.get(%activityName), 0);
    }
}
function UserActivityMgr::getActivityBitmapMLText(%this, %activityName)
{
    %ufn = %this.getActivityUserFacingName(%activityName);
    %bitmap = %this.getActivityIconFilename(%activityName);
    %tip = %ufn $= "" ? "" : "<tip:" @ %ufn @ ">";
    %ret = "<spush>" @ %tip @ "<bitmap:" @ %bitmap @ "><spop>";
    return %ret;
}
function UserActivityMgr::getActivityDuration(%this, %activityName)
{
    if (!%this.isKnownActivity(%activityName, 1))
    {
        return -1;
    }
    else
    {
        return getField(%this.knownActivities.get(%activityName), 1);
    }
}
function UserActivityMgr::getActivityPriority(%this, %activityName)
{
    return %this.knownActivities.getIndexFromKey(%activityName);
}
function UserActivityMgr::setActivityActive(%this, %activityName, %state)
{
    %this.isKnownActivity(%activityName, 1);
    %oldState = %this.currActivities.hasKey(%activityName);
    if (%oldState)
    {
        %timerID = %this.currActivities.get(%activityName);
        if (!(%timerID $= ""))
        {
            cancel(%timerID);
        }
    }
    if (%state)
    {
        %durationMS = %this.getActivityDuration(%activityName);
        if (%durationMS > 0)
        {
            %timerID = %this.schedule(%durationMS, "cancelActivity", %activityName);
        }
        else
        {
            %timerID = "";
        }
        %this.currActivities.put(%activityName, %timerID);
    }
    else
    {
        %this.currActivities.remove(%activityName);
    }
    if (%oldState != %state)
    {
        %this.tryReport();
    }
    if (isObject(geActivitiesPanel))
    {
        geActivitiesPanel.updateStates();
    }
}
function UserActivityMgr::cancelActivity(%this, %activityName)
{
    echoDebug(getScopeName() @ " " @ "- cancelling activity" @ " " @ %activityName);
    %this.setActivityActive(%activityName, 0);
}
function UserActivityMgr::getActivityActive(%this, %activityName)
{
    return %this.currActivities.hasKey(%activityName);
}
function UserActivityMgr::getActivityTimeLeft(%this, %activityName)
{
    if (!%this.currActivities.hasKey(%activityName))
    {
        return -1;
    }
    %timerID = %this.currActivities.get(%activityName);
    if (%timerID $= "")
    {
        return -1;
    }
    else
    {
        return getEventTimeLeft(%timerID);
    }
}
function UserActivityMgr::getHighestPriorityCurrentActivity(%this)
{
    %highestPri = "";
    %highestAct = "";
    %n = %this.currActivities.size() - 1;
    while (%n >= 0)
    {
        %act = %this.currActivities.getKey(%n);
        %pri = %this.getActivityPriority(%act);
        if ((%highestAct $= "") || (%pri < %highestPri))
        {
            %highestPri = %pri;
            %highestAct = %act;
        }
        %n = %n - 1;
    }
    return %highestAct;
}
function UserActivityMgr::tryReport(%this)
{
    %wait = %this.getMSToNextReport();
    if (%wait < 0)
    {
        %this._doReport();
    }
    else
    {
        if (%this.reportTimer $= "")
        {
            %this.reportTimer = %this.schedule(%wait, "_doReport");
            echoDebug(getScopeName() @ " " @ "- delaying for" @ " " @ %wait @ "MS");
        }
        else
        {
            echoDebug(getScopeName() @ " " @ "- waiting  for" @ " " @ %wait @ "MS");
        }
    }
}
function UserActivityMgr::getMSToNextReport(%this)
{
    %wait = %this.maxReportPeriodMS - %this.getLastReportAgeMS();
    return %wait;
}
function UserActivityMgr::_doReport(%this)
{
    cancel(%this.reportTimer);
    %this.reportTimer = "";
    %this.lastReportTimeMS = getSimTime();
    %list = "";
    %delim = "";
    %n = %this.currActivities.size() - 1;
    while (%n >= 0)
    {
        %list = %this.currActivities.getKey(%n) @ %delim @ %list;
        %delim = "\t";
        %n = %n - 1;
    }
    if (!$StandAlone)
    {
        sendRequest_UpdateUserStates(%list);
    }
}
function UserActivityMgr::getLastReportAgeMS(%this)
{
    return getSimTime() - %this.lastReportTimeMS;
}
function UserActivityMgr::getActivitiesMLText(%this, %activitiesList, %numToShow)
{
    %alphaOfSecond = 80;
    %alphaOfLast = 80;
    %ret = "";
    %delim = "";
    if (%numToShow == -1)
    {
        %numToShow = getFieldCount(%activitiesList);
    }
    else
    {
        %numToShow = mMin(%numToShow, getFieldCount(%activitiesList));
    }
    if (%numToShow <= 0)
    {
        %activityBitmapMLText = %this.getActivityBitmapMLText("");
        %ret = "<color:" @ ColorIToHex("255 255 255" @ " " @ %alphaOfLast) @ ">" @ %activityBitmapMLText;
    }
    else
    {
        %stepDown = (%alphaOfSecond - %alphaOfLast) / (%numToShow - 1);
        %m = 0;
        while (%m < %numToShow)
        {
            if (%m == 0)
            {
                %modulationColor = ColorIToHex("220 255 180 255");
            }
            else
            {
                %modulationColor = ColorIToHex("255 255 255" @ " " @ (%alphaOfSecond - (%m * %stepDown)));
            }
            %activityName = getField(%activitiesList, %m);
            %activityBitmapMLText = %this.getActivityBitmapMLText(%activityName);
            %ret = %ret @ %delim @ "<modulationColor:" @ %modulationColor @ ">" @ %activityBitmapMLText;
            %delim = " ";
            %m = %m + 1;
        }
    }
    %ret = "<spush>" @ %ret @ "<spop>";
    return %ret;
}
function ClientCmdBuddyActivitiesChanged(%userName, %activitiesTagged)
{
    %activitiesList = detag(%activitiesTagged);
    BuddyHudTabs.setBuddyActivities(%userName, %activitiesList);
    %infoMapEntry = PlayerInfoMap.get(%userName);
    if (isObject(%infoMapEntry))
    {
        %infoMapEntry.activities = %activitiesList;
    }
}
