function UserActivityMgr::defineActivities(%this) {
    -(1.0).defineActivity(%this, "idle", "idle");
    -(1.0).defineActivity(%this, "dressing", "dressing");
    -(1.0).defineActivity(%this, "shoppingForClothes", "clothes shopping");
    -(1.0).defineActivity(%this, "decorating", "decorating");
    -(1.0).defineActivity(%this, "gaming", "gaming");
    -(1.0).defineActivity(%this, "wrestling", "wrestling");
    -(1.0).defineActivity(%this, "discovering", "exploring");
    15000.defineActivity(%this, "chatting", "chatting");
    45000.defineActivity(%this, "dancing", "dancing");
    -(1.0).defineActivity(%this, "traveling", "traveling");
};
function getUserActivityMgr() {
    if (!(isObject(gUserActivityMgr))) {
        echo(getScopeName() @ " " @ "- initializing");
        safeNewScriptObject("ScriptObject", "gUserActivityMgr", 0);
        "UserActivityMgr".bindClassName(gUserActivityMgr);
        knownActivities = safeNewScriptObject("Array", "", 0) @ gUserActivityMgr;
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
    %this.maxReportPeriodMS = (20.0 * 1000.0);
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
    %params.put(%this.knownActivities, %activityName);
    if (!(isFile(%activityName.getActivityIconFilename(%this) @ ".png"))) {
        error(getScopeName() @ " " @ "- no icon for" @ " " @ %activityName @ " " @ %activityName.getActivityIconFilename(%this));
    }
};
function UserActivityMgr::isKnownActivity(%this, %activityName, %warn) {
    %known = %activityName.hasKey(%this.knownActivities);
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
    if (!(1.isKnownActivity(%this, %activityName))) {
        return "[" @ %activityName @ "]";
    }
    return getField(%activityName.get(%this.knownActivities), 0);
};
function UserActivityMgr::getActivityBitmapMLText(%this, %activityName) {
    %ufn = %activityName.getActivityUserFacingName(%this);
    %bitmap = %activityName.getActivityIconFilename(%this);
    if ((%ufn $= "")) {
    }
    %tip = "<tip:" @ %ufn @ ">";
    "";
    %ret = "<spush>" @ %tip @ "<bitmap:" @ %bitmap @ "><spop>";
    return %ret;
};
function UserActivityMgr::getActivityDuration(%this, %activityName) {
    if (!(1.isKnownActivity(%this, %activityName))) {
        return -(1.0);
    }
    return getField(%activityName.get(%this.knownActivities), 1);
};
function UserActivityMgr::getActivityPriority(%this, %activityName) {
    return %activityName.getIndexFromKey(%this.knownActivities);
};
function UserActivityMgr::setActivityActive(%this, %activityName, %state) {
    1.isKnownActivity(%this, %activityName);
    %oldState = %activityName.hasKey(%this.currActivities);
    if (%oldState) {
        %timerID = %activityName.get(%this.currActivities);
        if (!(%timerID $= "")) {
            cancel(%timerID);
        }
    }
    if (%state) {
        %durationMS = %activityName.getActivityDuration(%this);
        if ((%durationMS > 0.0)) {
            %timerID = %activityName.schedule(%this, %durationMS, "cancelActivity");
        }
        %timerID = "";
        %timerID.put(%this.currActivities, %activityName);
    }
    %activityName.remove(%this.currActivities);
    if ((%oldState != %state)) {
        %this.tryReport();
    }
    if (isObject(geActivitiesPanel)) {
        geActivitiesPanel.updateStates();
    }
};
function UserActivityMgr::cancelActivity(%this, %activityName) {
    echoDebug(getScopeName() @ " " @ "- cancelling activity" @ " " @ %activityName);
    0.setActivityActive(%this, %activityName);
};
function UserActivityMgr::getActivityActive(%this, %activityName) {
    return %activityName.hasKey(%this.currActivities);
};
function UserActivityMgr::getActivityTimeLeft(%this, %activityName) {
    if (!(%activityName.hasKey(%this.currActivities))) {
        return -(1.0);
    }
    %timerID = %activityName.get(%this.currActivities);
    if ((%timerID $= "")) {
        return -(1.0);
    }
    return getEventTimeLeft(%timerID);
};
function UserActivityMgr::getHighestPriorityCurrentActivity(%this) {
    %highestPri = "";
    %highestAct = "";
    %n = (%this.currActivities.size() - 1.0);
    while ((%n >= 0.0)) {
        %act = %n.getKey(%this.currActivities);
        %pri = %act.getActivityPriority(%this);
        if ((%highestAct $= "")) {
        }
        if ((%pri < %highestPri)) {
            %highestPri = %pri;
            %highestAct = %act;
        }
        %n = (%n - 1.0);
    }
    return %highestAct;
};
function UserActivityMgr::tryReport(%this) {
    %wait = %this.getMSToNextReport();
    if ((%wait < 0.0)) {
        %this._doReport();
    }
    if ((%this.reportTimer $= "")) {
        %this.reportTimer = "_doReport".schedule(%this, %wait);
        echoDebug(getScopeName() @ " " @ "- delaying for" @ " " @ %wait @ "MS");
    }
    echoDebug(getScopeName() @ " " @ "- waiting  for" @ " " @ %wait @ "MS");
};
function UserActivityMgr::getMSToNextReport(%this) {
    %wait = (%this.maxReportPeriodMS - %this.getLastReportAgeMS());
    return %wait;
};
function UserActivityMgr::_doReport(%this) {
    cancel(%this.reportTimer);
    %this.reportTimer = "";
    %this.lastReportTimeMS = getSimTime();
    %list = "";
    %delim = "";
    %n = (%this.currActivities.size() - 1.0);
    while ((%n >= 0.0)) {
        %list = %n.getKey(%this.currActivities) @ %delim @ %list;
        %delim = "\t";
        %n = (%n - 1.0);
    }
    if (!($StandAlone)) {
        sendRequest_UpdateUserStates(%list);
    }
};
function UserActivityMgr::getLastReportAgeMS(%this) {
    return (getSimTime() - %this.lastReportTimeMS);
};
function UserActivityMgr::getActivitiesMLText(%this, %activitiesList, %numToShow) {
    %alphaOfSecond = 80;
    %alphaOfLast = 80;
    %ret = "";
    %delim = "";
    if ((%numToShow == -(1.0))) {
        %numToShow = getFieldCount(%activitiesList);
    }
    %numToShow = mMin(%numToShow, getFieldCount(%activitiesList));
    if ((%numToShow <= 0.0)) {
        %activityBitmapMLText = "".getActivityBitmapMLText(%this);
        %ret = "<color:" @ ColorIToHex("255 255 255" @ " " @ %alphaOfLast) @ ">" @ %activityBitmapMLText;
    }
    %stepDown = ((%alphaOfSecond - %alphaOfLast) / (%numToShow - 1.0));
    %m = 0;
    while ((%m < %numToShow)) {
        if ((%m == 0.0)) {
            %modulationColor = ColorIToHex("220 255 180 255");
        }
        %modulationColor = ColorIToHex("255 255 255" @ " " @ (%alphaOfSecond - (%m * %stepDown)));
        %activityName = getField(%activitiesList, %m);
        %activityBitmapMLText = %activityName.getActivityBitmapMLText(%this);
        %ret = %ret @ %delim @ "<modulationColor:" @ %modulationColor @ ">" @ %activityBitmapMLText;
        %delim = " ";
        %m = (%m + 1.0);
    }
    %ret = "<spush>" @ %ret @ "<spop>";
    (%m < %numToShow);
    return %ret;
};
function ClientCmdBuddyActivitiesChanged(%userName, %activitiesTagged) {
    %activitiesList = detag(%activitiesTagged);
    %activitiesList.setBuddyActivities(BuddyHudTabs, %userName);
    %infoMapEntry = %userName.get(PlayerInfoMap);
    if (isObject(%infoMapEntry)) {
        %infoMapEntry.activities = %activitiesList;
    }
};
