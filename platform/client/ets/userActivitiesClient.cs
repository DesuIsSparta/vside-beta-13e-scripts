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
    if (!(isObject(gUserActivityMgr))) {
        echo(getScopeName() @ " " @ "- initializing");
        safeNewScriptObject("ScriptObject", "gUserActivityMgr", 0);
        "UserActivityMgr".bindClassName();
        knownActivities = safeNewScriptObject("Array", "", 0) @ gUserActivityMgr;
        gUserActivityMgr;
        currActivities = safeNewScriptObject("StringMap", "", 0) @ gUserActivityMgr;
        reportTimer = "" @ gUserActivityMgr;
        gUserActivityMgr.defineActivities();
        gUserActivityMgr.reset();
    }
};
function UserActivityMgr::reset(%this) {
    echo(getScopeName());
    %this.currActivities.clear();
    %this.lastReportTimeMS = 0;
    %this.maxReportPeriodMS = (1000.0 * 20.0);
    cancel(%this.reportTimer);
    %this.reportTimer = "";
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
    %this.knownActivities.put(%activityName, %params);
    if (!(isFile(%this.getActivityIconFilename(%activityName) @ ".png"))) {
        error(getScopeName() @ " " @ "- no icon for" @ " " @ %activityName @ " " @ %this.getActivityIconFilename(%activityName));
    }
};
function UserActivityMgr::isKnownActivity(%this, %activityName, %warn) {
    %known = %this.knownActivities.hasKey(%activityName);
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
    return getField(%this.knownActivities.get(%activityName), 0);
};
function UserActivityMgr::getActivityBitmapMLText(%this, %activityName) {
    %ufn = %this.getActivityUserFacingName(%activityName);
    %bitmap = %this.getActivityIconFilename(%activityName);
    if ((%ufn $= "")) {
    }
    %tip = "<tip:" @ %ufn @ ">";
    "";
    %ret = "<spush>" @ %tip @ "<bitmap:" @ %bitmap @ "><spop>";
    return %ret;
};
function UserActivityMgr::getActivityDuration(%this, %activityName) {
    if (!(%this.isKnownActivity(%activityName, 1))) {
        return -(1.0);
    }
    return getField(%this.knownActivities.get(%activityName), 1);
};
function UserActivityMgr::getActivityPriority(%this, %activityName) {
    return %this.knownActivities.getIndexFromKey(%activityName);
};
function UserActivityMgr::setActivityActive(%this, %activityName, %state) {
    %this.isKnownActivity(%activityName, 1);
    %oldState = %this.currActivities.hasKey(%activityName);
    if (%oldState) {
        %timerID = %this.currActivities.get(%activityName);
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
        %this.currActivities.put(%activityName, %timerID);
    }
    %this.currActivities.remove(%activityName);
    if ((%state != %oldState)) {
        %this.tryReport();
    }
    if (isObject(geActivitiesPanel)) {
        geActivitiesPanel.updateStates();
    }
};
function UserActivityMgr::cancelActivity(%this, %activityName) {
    echoDebug(getScopeName() @ " " @ "- cancelling activity" @ " " @ %activityName);
    %this.setActivityActive(%activityName, 0);
};
function UserActivityMgr::getActivityActive(%this, %activityName) {
    return %this.currActivities.hasKey(%activityName);
};
function UserActivityMgr::getActivityTimeLeft(%this, %activityName) {
    if (!(%this.currActivities.hasKey(%activityName))) {
        return -(1.0);
    }
    %timerID = %this.currActivities.get(%activityName);
    if ((%timerID $= "")) {
        return -(1.0);
    }
    return getEventTimeLeft(%timerID);
};
function UserActivityMgr::getHighestPriorityCurrentActivity(%this) {
    %highestPri = "";
    %highestAct = "";
    %n = (1.0 - %this.currActivities.size());
    if ((0.0 >= %n)) {
        %act = %this.currActivities.getKey(%n);
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
    if ((%this.reportTimer $= "")) {
        %this.reportTimer = %this.schedule(%wait, "_doReport");
        echoDebug(getScopeName() @ " " @ "- delaying for" @ " " @ %wait @ "MS");
    }
    echoDebug(getScopeName() @ " " @ "- waiting  for" @ " " @ %wait @ "MS");
};
function UserActivityMgr::getMSToNextReport(%this) {
    %wait = (%this.getLastReportAgeMS() - %this.maxReportPeriodMS);
    return %wait;
};
function UserActivityMgr::_doReport(%this) {
    cancel(%this.reportTimer);
    %this.reportTimer = "";
    %this.lastReportTimeMS = getSimTime();
    %list = "";
    %delim = "";
    %n = (1.0 - %this.currActivities.size());
    if ((0.0 >= %n)) {
        %list = %this.currActivities.getKey(%n) @ %delim @ %list;
        %delim = "\t";
        %n = (1.0 - %n);
    }
    if (!($StandAlone)) {
        sendRequest_UpdateUserStates(%list);
    }
};
function UserActivityMgr::getLastReportAgeMS(%this) {
    return (%this.lastReportTimeMS - getSimTime());
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
    %ret = "<spush>" @ %ret @ "<spop>";
    (%numToShow < %m);
    return %ret;
};
function ClientCmdBuddyActivitiesChanged(%userName, %activitiesTagged) {
    %activitiesList = detag(%activitiesTagged);
    %userName.setBuddyActivities(%activitiesList);
    %infoMapEntry = %userName.get();
    PlayerInfoMap;
    if (isObject(%infoMapEntry)) {
        %infoMapEntry.activities = BuddyHudTabs @ %activitiesList;
    }
};
