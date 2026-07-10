$gUserProperties_BackendImplemented = 1;
function userPropertiesMgr::getProperty(%this, %userName, %propertyName, %default) {
    %smValue = %this.propertiesValue;
    %userName;
    if (!(isObject(%smValue))) {
        error(getScopeName() @ " " @ "- properties not fetched yet:" @ " " @ %userName @ " " @ %propertyName @ " " @ getTrace());
        return %default;
    }
    if (!(%propertyName.hasKey(%smValue))) {
        log("general", "debug", getScopeName() @ " " @ "- asked for unknown property: \"" @ %propertyName @ "\"" @ " " @ getTrace());
        return %default;
    }
    return %propertyName.get(%smValue);
};
function userPropertiesMgr::setProperty(%this, %userName, %propertyName, %propertyValue) {
    %smValue = %this.propertiesValue;
    %userName;
    if (!(isObject(%smValue))) {
        error(getScopeName() @ " " @ "- not initialized for" @ " " @ %userName @ " " @ getTrace());
        return;
    }
    if ((%propertyValue $= "false")) {
    }
    %propertyValue = %propertyValue;
    0;
    if ((%propertyValue $= "true")) {
    }
    %propertyValue = %propertyValue;
    1;
    if (%propertyName.hasKey(%smValue)) {
    }
    if ((%propertyName.get(%smValue) $= %propertyValue)) {
        return;
    }
    %propertyValue.put(%smValue, %propertyName);
    %userName.persistSchedule(%this);
};
function userPropertiesMgr::hasProperty(%this, %userName, %propertyName) {
    %smValue = %this.propertiesValue;
    %userName;
    if (!(isObject(%smValue))) {
        error(getScopeName() @ " " @ "- properties not fetched yet:" @ " " @ %userName @ " " @ %propertyName @ " " @ getTrace());
        return %default;
    }
    return %propertyName.hasKey(%smValue);
};
function userPropertiesMgr::dumpProperties(%this, %userName) {
    %smValue = %this.propertiesValue;
    %userName;
    if (!(isObject(%smValue))) {
        error(getScopeName() @ " " @ "- not initialized for" @ " " @ %userName @ " " @ getTrace());
        return;
    }
    %smValue.dumpValues();
};
function userPropertiesMgr::clearProperty(%this, %userName, %propertyName) {
    1._clearProperty(%this, %userName, %propertyName);
};
function userPropertiesMgr::clearPropertyIfExists(%this, %userName, %propertyName) {
    0._clearProperty(%this, %userName, %propertyName);
};
function userPropertiesMgr::_clearProperty(%this, %userName, %propertyName, %warn) {
    %smValue = %this.propertiesValue;
    %userName;
    if (!(isObject(%smValue))) {
        error(getScopeName() @ " " @ "- not initialized for" @ " " @ %userName @ " " @ getTrace());
        return;
    }
    if (%propertyName.hasKey(%smValue)) {
        %propertyName.remove(%smValue);
        %userName.persistSchedule(%this);
    }
    if (%warn) {
        warn(getScopeName() @ " " @ "- property does not exist:" @ " " @ %propertyName @ " " @ %userName @ " " @ getTrace());
    }
};
function userPropertiesMgr::incrementIntegerProperty(%this, %userName, %propertyName, %incrementAmount) {
    %curVal = 0.getProperty(%this, %userName, %propertyName);
    %newVal = (%curVal + %incrementAmount);
    %newVal.setProperty(%this, %userName, %propertyName);
    return %newVal;
};
function userPropertiesMgr::haveProperties(%this, %userName) {
    %smValue = %this.propertiesValue;
    %userName;
    return isObject(%smValue);
};
function userPropertiesMgr::persistSchedule(%this, %userName) {
    if (!(%userName @ " " @ %this.propertiesPersistSchedule $= "")) {
        cancel(%userName, %this.propertiesPersistSchedule);
    }
    %this.propertiesPersistSchedule = %userName.schedule(%this, %this.persistPeriodMS, "persistReally") @ %userName;
};
function userPropertiesMgr::persistReally(%this, %userName, %callback) {
    if (!(isDefined("%callback"))) {
        %callback = "";
    }
    if (!(%userName @ " " @ %this.propertiesPersistSchedule $= "")) {
        cancel(%userName, %this.propertiesPersistSchedule);
        %this.propertiesPersistSchedule = "" @ %userName;
    }
    %smValue = %this.propertiesValue;
    %userName;
    if (!(isObject(%smValue))) {
        error(getScopeName() @ " " @ "- not initialized!" @ " " @ %userName @ " " @ getTrace());
        return;
    }
    if ($StandAlone) {
        %fileName = %userName.getStandaloneFilename(%this);
        %fileName.saveToLocalStorage(%smValue);
        if (!(%callback $= "")) {
            schedule(200, 0, "eval", %callback);
        }
        echo(getScopeName() @ " " @ "- standalone! persisted to" @ " " @ %fileName);
        return;
    }
    if (!(haveValidManagerHost())) {
        warn(getScopeName() @ " " @ "- not connected to backend - properties not persisted." @ " " @ %this.clientOrServer @ " " @ %userName);
        return;
    }
    if (isObject(%userName, %this.saveUserPropertiesRequest)) {
        warn(getScopeName() @ " " @ "- already have a post outstanding!" @ " " @ getTrace());
        %userName.persistSchedule(%this);
    }
    if (%this.isClient()) {
        %request = sendRequest_SaveClientUserProperties(%userName, %smValue, "onDoneOrErrorCallback_SetClientOrServerUserProperties");
    }
    %request = sendRequest_SaveServerUserProperties(%userName, %smValue, "onDoneOrErrorCallback_SetClientOrServerUserProperties");
    %request.userPropertiesMgr = %this;
    %request.userName = %userName;
    %request.otherCallback = %callback;
    %this.saveUserPropertiesRequest = %request @ %userName;
};
function onDoneOrErrorCallback_SetClientOrServerUserProperties(%request) {
    %request.userPropertiesMgr.saveUserPropertiesRequest = "" @ %request.userName;
    if (!(%request.otherCallback $= "")) {
        echoDebug(getScopeName() @ " " @ "- eval(" @ %request.otherCallback @ "):");
        eval(%request.otherCallback);
    }
};
function userProperties_makeManager(%name, %isClient) {
    if (isObject(%name)) {
        return %name;
    }
    %mgr = safeNewScriptObject("ScriptObject", "", 0);
    "userPropertiesMgr".bindClassName(%mgr);
    %name.setName(%mgr);
    %mgr.persistPeriodMS = 4000;
    %mgr.clientOrServer = %isClient ? "client" : "server";
    return %mgr;
};
function userPropertiesMgr::isClient(%this) {
    return (%this.clientOrServer $= "client");
};
function userPropertiesMgr::isServer() {
    return !(%this.isClient());
};
function userPropertiesMgr::requestProperties(%this, %userName, %callback) {
    if (isObject(%userName, %this.propertiesValue)) {
        warn(getScopeName() @ " " @ "- Requesting user properties when we've already got them." @ " " @ %userName @ " " @ getTrace());
        if (!(%callback $= "")) {
            eval(%callback);
        }
        return;
    }
    if ($StandAlone) {
        %fileName = %userName.getStandaloneFilename(%this);
        %this.propertiesValue = safeNewScriptObject("StringMap", "", 0) @ %userName;
        "debug".loadFromLocalStorage(%userName, %this.propertiesValue, %fileName);
        if (!(%callback $= "")) {
            schedule(200, 0, "eval", %callback);
        }
        echo(getScopeName() @ " " @ "- standalone! loaded from" @ " " @ %fileName);
        return;
    }
    if (!(haveValidManagerHost()) && %this.isClient()) {
    }
    if (!(haveValidToken())) {
        warn(getScopeName() @ " " @ "- not connected to backend - properties not retrieved." @ " " @ %this.clientOrServer @ " " @ %userName);
        if (!(isObject(%userName, %this.propertiesValue))) {
            %this.propertiesValue = safeNewScriptObject("StringMap", "", 0) @ %userName;
        }
        eval(%callback);
        return;
    }
    if (isObject(%userName, %this.getUserPropertiesRequest)) {
        return;
    }
    if (%this.isClient()) {
        %request = sendRequest_GetClientUserProperties(%userName, "onDoneOrErrorCallback_GetClientOrServerUserProperties");
    }
    %request = sendRequest_GetServerUserProperties(%userName, "onDoneOrErrorCallback_GetClientOrServerUserProperties");
    %request.userPropertiesMgr = %this;
    %request.userName = %userName;
    %request.otherCallback = %callback;
    %request.retryTotal = 4;
    %request.retryDelay = 500;
};
function onDoneOrErrorCallback_GetClientOrServerUserProperties(%request) {
    if (%request.checkSuccess()) {
        %request.parseRequest(%request.userPropertiesMgr);
    }
    if (!(%request.otherCallback $= "")) {
        echoDebug(getScopeName() @ " " @ "- eval(" @ %request.otherCallback @ "):");
        eval(%request.otherCallback);
    }
};
function userPropertiesMgr::parseRequest(%this, %request) {
    if (!(isObject(%request.userName, %this.propertiesValue))) {
        %this.propertiesValue = safeNewScriptObject("StringMap", "", 0) @ %request.userName;
    }
    %smValue = %this.propertiesValue;
    %request.userName;
    %smValue.clear();
    %num = "propertyCount".getValue(%request);
    %n = 0;
    while ((%n < %num)) {
        %key = utf8Decode("property" @ %n @ ".key".getValue(%request));
        %value = utf8Decode("property" @ %n @ ".value".getValue(%request));
        if ((%value $= "false")) {
        }
        %value = %value;
        0;
        if ((%value $= "true")) {
        }
        %value = %value;
        1;
        %value.put(%smValue, %key);
        %n = (%n + 1.0);
    }
};
function userPropertiesMgr::requestPropertiesForce(%this, %userName, %callback) {
    if (isObject(%userName, %this.propertiesValue)) {
        %this.propertiesValue.delete(%userName);
        %this.propertiesValue = 0 @ %userName;
    }
    %callback.requestProperties(%this, %userName);
};
function userPropertiesMgr::forgetProperties(%this, %userName) {
    if (isObject(%userName, %this.propertiesValue)) {
        %this.propertiesValue.delete(%userName);
        %this.propertiesValue = "" @ %userName;
    }
};
function userPropertiesMgr::getStandaloneFilename(%this, %userName) {
    %ret = "userprops_" @ %this.clientOrServer @ "_" @ %userName;
};
