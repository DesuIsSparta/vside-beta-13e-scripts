$gUserProperties_BackendImplemented = 1;
function userPropertiesMgr::getProperty(%this, %userName, %propertyName, %default) {
    %smValue = %this.propertiesValue;
    %userName;
    if (!(isObject(%smValue))) {
        error(getScopeName() @ " " @ "- properties not fetched yet:" @ " " @ %userName @ " " @ %propertyName @ " " @ getTrace());
        return %default;
    }
    if (!(%smValue.hasKey(%propertyName))) {
        log("general", "debug", getScopeName() @ " " @ "- asked for unknown property: \"" @ %propertyName @ "\"" @ " " @ getTrace());
        return %default;
    }
    return %smValue.get(%propertyName);
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
    if (%smValue.hasKey(%propertyName)) {
    }
    if ((%smValue.get(%propertyName) $= %propertyValue)) {
        return;
    }
    %smValue.put(%propertyName, %propertyValue);
    %this.persistSchedule(%userName);
};
function userPropertiesMgr::hasProperty(%this, %userName, %propertyName) {
    %smValue = %this.propertiesValue;
    %userName;
    if (!(isObject(%smValue))) {
        error(getScopeName() @ " " @ "- properties not fetched yet:" @ " " @ %userName @ " " @ %propertyName @ " " @ getTrace());
        return %default;
    }
    return %smValue.hasKey(%propertyName);
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
    %this._clearProperty(%userName, %propertyName, 1);
};
function userPropertiesMgr::clearPropertyIfExists(%this, %userName, %propertyName) {
    %this._clearProperty(%userName, %propertyName, 0);
};
function userPropertiesMgr::_clearProperty(%this, %userName, %propertyName, %warn) {
    %smValue = %this.propertiesValue;
    %userName;
    if (!(isObject(%smValue))) {
        error(getScopeName() @ " " @ "- not initialized for" @ " " @ %userName @ " " @ getTrace());
        return;
    }
    if (%smValue.hasKey(%propertyName)) {
        %smValue.remove(%propertyName);
        %this.persistSchedule(%userName);
    }
    if (%warn) {
        warn(getScopeName() @ " " @ "- property does not exist:" @ " " @ %propertyName @ " " @ %userName @ " " @ getTrace());
    }
};
function userPropertiesMgr::incrementIntegerProperty(%this, %userName, %propertyName, %incrementAmount) {
    %curVal = %this.getProperty(%userName, %propertyName, 0);
    %newVal = (%incrementAmount + %curVal);
    %this.setProperty(%userName, %propertyName, %newVal);
    return %newVal;
};
function userPropertiesMgr::haveProperties(%this, %userName) {
    %smValue = %this.propertiesValue;
    %userName;
    return isObject(%smValue);
};
function userPropertiesMgr::persistSchedule(%this, %userName) {
    if (!(%userName @ " " @ %this.propertiesPersistSchedule $= "")) {
        cancel(%this.propertiesPersistSchedule);
    }
    %this.propertiesPersistSchedule = %userName @ %this.schedule(%this.persistPeriodMS, "persistReally", %userName) @ %userName;
};
function userPropertiesMgr::persistReally(%this, %userName, %callback) {
    if (!(isDefined("%callback"))) {
        %callback = "";
    }
    if (!(%userName @ " " @ %this.propertiesPersistSchedule $= "")) {
        cancel(%this.propertiesPersistSchedule);
        %this.propertiesPersistSchedule = %userName @ "" @ %userName;
    }
    %smValue = %this.propertiesValue;
    %userName;
    if (!(isObject(%smValue))) {
        error(getScopeName() @ " " @ "- not initialized!" @ " " @ %userName @ " " @ getTrace());
        return;
    }
    if ($StandAlone) {
        %fileName = %this.getStandaloneFilename(%userName);
        %smValue.saveToLocalStorage(%fileName);
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
    if (isObject(%this.saveUserPropertiesRequest)) {
        warn(getScopeName() @ " " @ "- already have a post outstanding!" @ " " @ getTrace());
        %this.persistSchedule(%userName);
    }
    if (%this.isClient()) {
        %request = sendRequest_SaveClientUserProperties(%userName, %smValue, "onDoneOrErrorCallback_SetClientOrServerUserProperties");
        %userName;
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
    %mgr.bindClassName("userPropertiesMgr");
    %mgr.setName(%name);
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
    if (isObject(%this.propertiesValue)) {
        warn(getScopeName() @ " " @ "- Requesting user properties when we've already got them." @ " " @ %userName @ " " @ getTrace());
        if (!(%userName @ " " @ %callback $= "")) {
            eval(%callback);
        }
        return;
    }
    if ($StandAlone) {
        %fileName = %this.getStandaloneFilename(%userName);
        %this.propertiesValue = safeNewScriptObject("StringMap", "", 0) @ %userName;
        %this.propertiesValue.loadFromLocalStorage(%fileName, "debug");
        if (!(%userName @ " " @ %callback $= "")) {
            schedule(200, 0, "eval", %callback);
        }
        echo(getScopeName() @ " " @ "- standalone! loaded from" @ " " @ %fileName);
        return;
    }
    if (!(haveValidManagerHost())) {
        if (%this.isClient()) {
        }
    }
    if (!(haveValidToken())) {
        warn(getScopeName() @ " " @ "- not connected to backend - properties not retrieved." @ " " @ %this.clientOrServer @ " " @ %userName);
        if (!(isObject(%this.propertiesValue))) {
            %this.propertiesValue = %userName @ safeNewScriptObject("StringMap", "", 0) @ %userName;
        }
        eval(%callback);
        return;
    }
    if (isObject(%this.getUserPropertiesRequest)) {
        return %userName;
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
        %request.userPropertiesMgr.parseRequest(%request);
    }
    if (!(%request.otherCallback $= "")) {
        echoDebug(getScopeName() @ " " @ "- eval(" @ %request.otherCallback @ "):");
        eval(%request.otherCallback);
    }
};
function userPropertiesMgr::parseRequest(%this, %request) {
    if (!(isObject(%this.propertiesValue))) {
        %this.propertiesValue = %request.userName @ safeNewScriptObject("StringMap", "", 0) @ %request.userName;
    }
    %smValue = %this.propertiesValue;
    %request.userName;
    %smValue.clear();
    %num = %request.getValue("propertyCount");
    %n = 0;
    if ((%num < %n)) {
        %key = utf8Decode(%request.getValue("property" @ %n @ ".key"));
        %value = utf8Decode(%request.getValue("property" @ %n @ ".value"));
        if ((%value $= "false")) {
        }
        %value = %value;
        0;
        if ((%value $= "true")) {
        }
        %value = %value;
        1;
        %smValue.put(%key, %value);
        %n = (1.0 + %n);
    }
};
function userPropertiesMgr::requestPropertiesForce(%this, %userName, %callback) {
    if (isObject(%this.propertiesValue)) {
        %this.propertiesValue.delete();
        %this.propertiesValue = %userName @ %userName @ 0 @ %userName;
    }
    %this.requestProperties(%userName, %callback);
};
function userPropertiesMgr::forgetProperties(%this, %userName) {
    if (isObject(%this.propertiesValue)) {
        %this.propertiesValue.delete();
        %this.propertiesValue = %userName @ %userName @ "" @ %userName;
    }
};
function userPropertiesMgr::getStandaloneFilename(%this, %userName) {
    %ret = "userprops_" @ %this.clientOrServer @ "_" @ %userName;
};
