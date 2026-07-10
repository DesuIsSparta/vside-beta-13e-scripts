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
    if (!(isObject())) {
        echo(getScopeName() @ " " @ "- initializing");
        safeNewScriptObject("ScriptObject", "gUserActivityMgr", 0);
        "UserActivityMgr".bindClassName();
        knownActivities = gUserActivityMgr @ safeNewScriptObject("Array", "", 0) @ gUserActivityMgr;
        gUserActivityMgr;
        currActivities = safeNewScriptObject("StringMap", "", 0) @ gUserActivityMgr;
        reportTimer = "" @ gUserActivityMgr;
        defineActivities();
        reset();
    }
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
    if (isDefined("%userFacingName")) {
    }
    %userFacingName = %activityName;
    %userFacingName;
    if (isDefined("%duration")) {
    }
    %duration = -(1.0);
    %duration;
    %params = %userFacingName @ "\t" @ %duration;
    knownActivities.put(%activityName, %params);
    if (!(isFile(%this @ %this.getActivityIconFilename(%activityName) @ ".png"))) {
        error(getScopeName() @ " " @ "- no icon for" @ " " @ %activityName @ " " @ %this.getActivityIconFilename(%activityName));
    }
};
function UserActivityMgr::isKnownActivity(%this, %activityName, %warn) {
    %known = knownActivities.hasKey(%activityName);
    %this;
    if (!(%known)) {
    }
    if (isDefined("%warn")) {
    }
    if (%warn) {
        error(getScopeName() @ " " @ "- unknown activity: \"" @ %activityName @ "\"." @ " " @ getTrace());
    }
    return %known;
};
function UserActivityMgr::getActivityIconFilename(%this, %activityName) {
    if ((%activityName $= "")) {
        %activityName = "none";
    }
    return "platform/client/ui/activities/activity_" @ %activityName;
};
function UserActivityMgr::getActivityUserFacingName(%this, %activityName) {
    if ((%activityName $= "")) {
        return "";
    }
    if (!(%this.isKnownActivity(%activityName, 1))) {
        return "[" @ %activityName @ "]";
    }
    return getField(knownActivities.get(%activityName), 0);
};
function UserActivityMgr::getActivityBitmapMLText(%this, %activityName) {
    %ufn = %this.getActivityUserFacingName(%activityName);
    %bitmap = %this.getActivityIconFilename(%activityName);
    if ((%ufn $= "")) {
    }
    %tip = "" @ "<tip:" @ %ufn @ ">";
    %ret = "<spush>" @ %tip @ "<bitmap:" @ %bitmap @ "><spop>";
    return %ret;
};
function UserActivityMgr::getActivityDuration(%this, %activityName) {
    if (!(%this.isKnownActivity(%activityName, 1))) {
        return -(1.0);
    }
    return getField(knownActivities.get(%activityName), 1);
};
function UserActivityMgr::getActivityPriority(%this, %activityName) {
    return knownActivities.getIndexFromKey(%activityName);
};
function UserActivityMgr::setActivityActive(%this, %activityName, %state) {
    %this.isKnownActivity(%activityName, 1);
    %oldState = currActivities.hasKey(%activityName);
    %this;
    if (%oldState) {
        %timerID = currActivities.get(%activityName);
        %this;
        if (!(%timerID $= "")) {
            cancel(%timerID);
        }
    }
    if (%state) {
        %durationMS = %this.getActivityDuration(%activityName);
        if ((0.0 > %durationMS)) {
            %timerID = %this.schedule(%durationMS, "cancelActivity", %activityName);
        }
        %timerID = "";
        currActivities.put(%activityName, %timerID);
    }
    currActivities.remove(%activityName);
    if ((%state != %oldState)) {
        %this.tryReport();
    }
    if (isObject()) {
        updateStates();
    }
};
function UserActivityMgr::cancelActivity(%this, %activityName) {
    echoDebug(getScopeName() @ " " @ "- cancelling activity" @ " " @ %activityName);
    %this.setActivityActive(%activityName, 0);
};
function UserActivityMgr::getActivityActive(%this, %activityName) {
    return currActivities.hasKey(%activityName);
};
function UserActivityMgr::getActivityTimeLeft(%this, %activityName) {
    if (!(currActivities.hasKey(%activityName))) {
        return -(1.0);
    }
    %timerID = currActivities.get(%activityName);
    %this;
    if ((%timerID $= "")) {
        return -(1.0);
    }
    return getEventTimeLeft(%timerID);
};
function UserActivityMgr::getHighestPriorityCurrentActivity(%this) {
    %highestPri = "";
    %highestAct = "";
    %n = (%this - currActivities.size());
    1.0;
    if ((0.0 >= %n)) {
        %act = currActivities.getKey(%n);
        %this;
        %pri = %this.getActivityPriority(%act);
        if ((%highestAct $= "")) {
        }
        if ((%highestPri < %pri)) {
            %highestPri = %pri;
            %highestAct = %act;
        }
        %n = (1.0 - %n);
    }
    return %highestAct;
};
function UserActivityMgr::tryReport(%this) {
    %wait = %this.getMSToNextReport();
    if ((0.0 < %wait)) {
        %this._doReport();
    }
    if ((%this SPC reportTimer $= "")) {
        reportTimer = %this.schedule(%wait, "_doReport") @ %this;
        echoDebug(getScopeName() @ " " @ "- delaying for" @ " " @ %wait @ "MS");
    }
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
    if ((0.0 >= %n)) {
        %list = %this @ currActivities.getKey(%n) @ %delim @ %list;
        %delim = "\t";
        %n = (1.0 - %n);
    }
    if (!($StandAlone)) {
        sendRequest_UpdateUserStates(%list);
    }
};
function UserActivityMgr::getLastReportAgeMS(%this) {
    return (lastReportTimeMS - getSimTime());
};
function UserActivityMgr::getActivitiesMLText(%this, %activitiesList, %numToShow) {
    %alphaOfSecond = 80;
    %alphaOfLast = 80;
    %ret = "";
    %delim = "";
    if ((-(1.0) == %numToShow)) {
        %numToShow = getFieldCount(%activitiesList);
    }
    %numToShow = mMin(%numToShow, getFieldCount(%activitiesList));
    if ((0.0 <= %numToShow)) {
        %activityBitmapMLText = %this.getActivityBitmapMLText("");
        %ret = "<color:" @ ColorIToHex("255 255 255" @ " " @ %alphaOfLast) @ ">" @ %activityBitmapMLText;
    }
    %stepDown = ((1.0 - %numToShow) / (%alphaOfLast - %alphaOfSecond));
    %m = 0;
    if ((%numToShow < %m)) {
        if ((0.0 == %m)) {
            %modulationColor = ColorIToHex("220 255 180 255");
        }
        %modulationColor = ColorIToHex("255 255 255" @ " " @ ((%stepDown * %m) - %alphaOfSecond));
        %activityName = getField(%activitiesList, %m);
        %activityBitmapMLText = %this.getActivityBitmapMLText(%activityName);
        %ret = %ret @ %delim @ "<modulationColor:" @ %modulationColor @ ">" @ %activityBitmapMLText;
        %delim = " ";
        %m = (1.0 + %m);
    }
    %ret = (%numToShow < %m) @ "<spush>" @ %ret @ "<spop>";
    return %ret;
};
function ClientCmdBuddyActivitiesChanged(%userName, %activitiesTagged) {
    %activitiesList = detag(%activitiesTagged);
    %userName.setBuddyActivities(%activitiesList);
    %infoMapEntry = %userName.get();
    PlayerInfoMap;
    if (isObject(%infoMapEntry)) {
        activities = BuddyHudTabs @ %activitiesList @ %infoMapEntry;
    }
};
