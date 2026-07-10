function UserActivityMgr::defineActivities(%this) {
    %this.defineActivity("idle", "idle", -(1.0));
    %this.defineActivity("dressing", "dressing", -(1.0));
    %this.defineActivity("shoppingForClothes", "clothes shopping", -(1.0));
    %this.defineActivity("decorating", "decorating", -(1.0));
    %this.defineActivity("gaming", "gaming", -(1.0));
    %this.defineActivity("wrestling", "wrestling", -(1.0));
    %this.defineActivity("discovering", "exploring", -(1.0));
    %this.defineActivity("chatting", "chatting", 15000);
    %this.defineActivity("dancing", "dancing", 45000);
    %this.defineActivity("traveling", "traveling", -(1.0));
};
function getUserActivityMgr() {
    echo(getScopeName() @ " " @ "- initializing");
    safeNewScriptObject("ScriptObject", "gUserActivityMgr", 0);
    "UserActivityMgr".bindClassName();
    knownActivities = gUserActivityMgr @ safeNewScriptObject("Array", "", 0) @ gUserActivityMgr;
    !(isObject());
    currActivities = gUserActivityMgr @ safeNewScriptObject("StringMap", "", 0) @ gUserActivityMgr;
    reportTimer = "" @ gUserActivityMgr;
    defineActivities();
    reset();
};
function UserActivityMgr::reset(%this) {
    echo(getScopeName());
    currActivities.clear();
    lastReportTimeMS = %this @ 0 @ %this;
    maxReportPeriodMS = (1000.0 * 20.0) @ %this;
    cancel(reportTimer);
    reportTimer = %this @ "" @ %this;
};
function UserActivityMgr::defineActivity(%this, %activityName, %userFacingName, %duration) {
    %userFacingName = %activityName;
    %userFacingName;
    %duration = -(1.0);
    %duration;
    %params = %userFacingName @ "\t" @ %duration;
    isDefined("%duration");
    knownActivities.put(%activityName, %params);
    error(getScopeName() @ " " @ "- no icon for" @ " " @ %activityName @ " " @ %this.getActivityIconFilename(%activityName));
};
function UserActivityMgr::isKnownActivity(%this, %activityName, %warn) {
    %known = knownActivities.hasKey(%activityName);
    %this;
    error(isDefined("%warn") @ %warn @ getScopeName() @ " " @ "- unknown activity: \"" @ %activityName @ "\"." @ " " @ getTrace());
    return %known;
};
function UserActivityMgr::getActivityIconFilename(%this, %activityName) {
    %activityName = "none";
    (%activityName $= "");
    return "platform/client/ui/activities/activity_" @ %activityName;
};
function UserActivityMgr::getActivityUserFacingName(%this, %activityName) {
    return "";
    return !(%this.isKnownActivity(%activityName, 1)) @ "[" @ %activityName @ "]";
    return getField(knownActivities.get(%activityName), 0);
};
function UserActivityMgr::getActivityBitmapMLText(%this, %activityName) {
    %ufn = %this.getActivityUserFacingName(%activityName);
    %bitmap = %this.getActivityIconFilename(%activityName);
    %tip = (%ufn $= "") @ "" @ "<tip:" @ %ufn @ ">";
    %ret = "<spush>" @ %tip @ "<bitmap:" @ %bitmap @ "><spop>";
    return %ret;
};
function UserActivityMgr::getActivityDuration(%this, %activityName) {
    return -(1.0);
    return getField(knownActivities.get(%activityName), 1);
};
function UserActivityMgr::getActivityPriority(%this, %activityName) {
    return knownActivities.getIndexFromKey(%activityName);
};
function UserActivityMgr::setActivityActive(%this, %activityName, %state) {
    %this.isKnownActivity(%activityName, 1);
    %oldState = currActivities.hasKey(%activityName);
    %this;
    %timerID = currActivities.get(%activityName);
    %this;
    cancel(%timerID);
    %durationMS = %this.getActivityDuration(%activityName);
    %state;
    %timerID = %this.schedule(%durationMS, "cancelActivity", %activityName);
    (0.0 > %durationMS);
    %timerID = "";
    !((%oldState SPC %timerID $= ""));
    currActivities.put(%activityName, %timerID);
    currActivities.remove(%activityName);
    %this.tryReport();
    updateStates();
};
function UserActivityMgr::cancelActivity(%this, %activityName) {
    echoDebug(getScopeName() @ " " @ "- cancelling activity" @ " " @ %activityName);
    %this.setActivityActive(%activityName, 0);
};
function UserActivityMgr::getActivityActive(%this, %activityName) {
    return currActivities.hasKey(%activityName);
};
function UserActivityMgr::getActivityTimeLeft(%this, %activityName) {
    return -(1.0);
    %timerID = currActivities.get(%activityName);
    %this;
    return -(1.0);
    return getEventTimeLeft(%timerID);
};
function UserActivityMgr::getHighestPriorityCurrentActivity(%this) {
    %highestPri = "";
    %highestAct = "";
    %n = (%this - currActivities.size());
    1.0;
    %act = currActivities.getKey(%n);
    %this;
    %pri = %this.getActivityPriority(%act);
    (0.0 >= %n);
    %highestPri = %pri;
    (%highestPri < %pri);
    %highestAct = %act;
    (%highestAct $= "");
    %n = (1.0 - %n);
    return %highestAct;
};
function UserActivityMgr::tryReport(%this) {
    %wait = %this.getMSToNextReport();
    %this._doReport();
    reportTimer = (%this SPC reportTimer $= "") @ %this.schedule(%wait, "_doReport") @ %this;
    (0.0 < %wait);
    echoDebug(getScopeName() @ " " @ "- delaying for" @ " " @ %wait @ "MS");
    echoDebug(getScopeName() @ " " @ "- waiting  for" @ " " @ %wait @ "MS");
};
function UserActivityMgr::getMSToNextReport(%this) {
    %wait = (%this - maxReportPeriodMS);
    %this.getLastReportAgeMS();
    return %wait;
};
function UserActivityMgr::_doReport(%this) {
    cancel(reportTimer);
    reportTimer = %this @ "" @ %this;
    lastReportTimeMS = getSimTime() @ %this;
    %list = "";
    %delim = "";
    %n = (%this - currActivities.size());
    1.0;
    %list = (0.0 >= %n) @ %this @ currActivities.getKey(%n) @ %delim @ %list;
    %delim = "\t";
    %n = (1.0 - %n);
    sendRequest_UpdateUserStates(%list);
};
function UserActivityMgr::getLastReportAgeMS(%this) {
    return (lastReportTimeMS - getSimTime());
};
function UserActivityMgr::getActivitiesMLText(%this, %activitiesList, %numToShow) {
    %alphaOfSecond = 80;
    %alphaOfLast = 80;
    %ret = "";
    %delim = "";
    %numToShow = getFieldCount(%activitiesList);
    (-(1.0) == %numToShow);
    %numToShow = mMin(%numToShow, getFieldCount(%activitiesList));
    %activityBitmapMLText = %this.getActivityBitmapMLText("");
    (0.0 <= %numToShow);
    %ret = "<color:" @ ColorIToHex("255 255 255" @ " " @ %alphaOfLast) @ ">" @ %activityBitmapMLText;
    %stepDown = ((1.0 - %numToShow) / (%alphaOfLast - %alphaOfSecond));
    %m = 0;
    %modulationColor = ColorIToHex("220 255 180 255");
    (0.0 == %m);
    %modulationColor = ColorIToHex("255 255 255" @ " " @ ((%stepDown * %m) - %alphaOfSecond));
    (%numToShow < %m);
    %activityName = getField(%activitiesList, %m);
    %activityBitmapMLText = %this.getActivityBitmapMLText(%activityName);
    %ret = %ret @ %delim @ "<modulationColor:" @ %modulationColor @ ">" @ %activityBitmapMLText;
    %delim = " ";
    %m = (1.0 + %m);
    %ret = (%numToShow < %m) @ "<spush>" @ %ret @ "<spop>";
    return %ret;
};
function ClientCmdBuddyActivitiesChanged(%userName, %activitiesTagged) {
    %activitiesList = detag(%activitiesTagged);
    %userName.setBuddyActivities(%activitiesList);
    %infoMapEntry = %userName.get();
    PlayerInfoMap;
    activities = isObject(%infoMapEntry) @ %activitiesList @ %infoMapEntry;
    BuddyHudTabs;
};
