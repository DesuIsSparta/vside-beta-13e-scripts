$gUserProperties_BackendImplemented = 1;
function userPropertiesMgr::getProperty(%this, %userName, %propertyName, %default) {
    %smValue = propertiesValue;
    %userName @ %this;
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
    %smValue = propertiesValue;
    %userName @ %this;
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
    %smValue = propertiesValue;
    %userName @ %this;
    if (!(isObject(%smValue))) {
        error(getScopeName() @ " " @ "- properties not fetched yet:" @ " " @ %userName @ " " @ %propertyName @ " " @ getTrace());
        return %default;
    }
    return %smValue.hasKey(%propertyName);
};
function userPropertiesMgr::dumpProperties(%this, %userName) {
    %smValue = propertiesValue;
    %userName @ %this;
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
    %smValue = propertiesValue;
    %userName @ %this;
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
    %smValue = propertiesValue;
    %userName @ %this;
    return isObject(%smValue);
};
function userPropertiesMgr::persistSchedule(%this, %userName) {
    if (!(%userName @ %this SPC propertiesPersistSchedule $= "")) {
        cancel(propertiesPersistSchedule);
    }
    propertiesPersistSchedule = %userName @ %this @ %this @ %this.schedule(persistPeriodMS, "persistReally", %userName) @ %userName @ %this;
};
function userPropertiesMgr::persistReally(%this, %userName, %callback) {
    if (!(isDefined("%callback"))) {
        %callback = "";
    }
    if (!(%userName @ %this SPC propertiesPersistSchedule $= "")) {
        cancel(propertiesPersistSchedule);
        propertiesPersistSchedule = %userName @ %this @ "" @ %userName @ %this;
    }
    %smValue = propertiesValue;
    %userName @ %this;
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
        warn(%this @ clientOrServer @ " " @ %userName);
        return getScopeName() @ " " @ "- not connected to backend - properties not persisted." @ " ";
    }
    if (isObject(saveUserPropertiesRequest)) {
        warn(getScopeName() @ " " @ "- already have a post outstanding!" @ " " @ getTrace());
        %this.persistSchedule(%userName);
    }
    if (%this.isClient()) {
        %request = sendRequest_SaveClientUserProperties(%userName, %smValue, "onDoneOrErrorCallback_SetClientOrServerUserProperties");
        %userName @ %this;
    }
    %request = sendRequest_SaveServerUserProperties(%userName, %smValue, "onDoneOrErrorCallback_SetClientOrServerUserProperties");
    userPropertiesMgr = %this @ %request;
    userName = %userName @ %request;
    otherCallback = %callback @ %request;
    saveUserPropertiesRequest = %request @ %userName @ %this;
};
function onDoneOrErrorCallback_SetClientOrServerUserProperties(%request) {
    saveUserPropertiesRequest = %request @ userName @ %request @ userPropertiesMgr;
    "";
    if (!(%request SPC otherCallback $= "")) {
        echoDebug(getScopeName() @ " " @ "- eval(" @ %request @ otherCallback @ "):");
        eval(otherCallback);
    }
};
function userProperties_makeManager(%name, %isClient) {
    if (isObject(%name)) {
        return %name;
    }
    %mgr = safeNewScriptObject("ScriptObject", "", 0);
    %mgr.bindClassName("userPropertiesMgr");
    %mgr.setName(%name);
    persistPeriodMS = 4000 @ %mgr;
    clientOrServer = %isClient ? "client" : "server" @ %mgr;
    return %mgr;
};
function userPropertiesMgr::isClient(%this) {
    return (%this SPC clientOrServer $= "client");
};
function userPropertiesMgr::isServer() {
    return !(%this.isClient());
};
function userPropertiesMgr::requestProperties(%this, %userName, %callback) {
    if (isObject(propertiesValue)) {
        warn(getScopeName() @ " " @ "- Requesting user properties when we've already got them." @ " " @ %userName @ " " @ getTrace());
        if (!(%userName @ %this SPC %callback $= "")) {
            eval(%callback);
        }
        return;
    }
    if ($StandAlone) {
        %fileName = %this.getStandaloneFilename(%userName);
        propertiesValue = safeNewScriptObject("StringMap", "", 0) @ %userName @ %this;
        propertiesValue.loadFromLocalStorage(%fileName, "debug");
        if (!(%userName @ %this SPC %callback $= "")) {
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
        warn(%this @ clientOrServer @ " " @ %userName);
        if (!(isObject(propertiesValue))) {
            propertiesValue = getScopeName() @ " " @ "- not connected to backend - properties not retrieved." @ " " @ %userName @ %this @ safeNewScriptObject("StringMap", "", 0) @ %userName @ %this;
        }
        eval(%callback);
        return;
    }
    if (isObject(getUserPropertiesRequest)) {
        return %userName @ %this;
    }
    if (%this.isClient()) {
        %request = sendRequest_GetClientUserProperties(%userName, "onDoneOrErrorCallback_GetClientOrServerUserProperties");
    }
    %request = sendRequest_GetServerUserProperties(%userName, "onDoneOrErrorCallback_GetClientOrServerUserProperties");
    userPropertiesMgr = %this @ %request;
    userName = %userName @ %request;
    otherCallback = %callback @ %request;
    retryTotal = 4 @ %request;
    retryDelay = 500 @ %request;
};
function onDoneOrErrorCallback_GetClientOrServerUserProperties(%request) {
    if (%request.checkSuccess()) {
        userPropertiesMgr.parseRequest(%request);
    }
    if (!(%request SPC otherCallback $= "")) {
        echoDebug(%request @ getScopeName() @ " " @ "- eval(" @ %request @ otherCallback @ "):");
        eval(otherCallback);
    }
};
function userPropertiesMgr::parseRequest(%this, %request) {
    if (!(isObject(propertiesValue))) {
        propertiesValue = %request @ userName @ %this @ safeNewScriptObject("StringMap", "", 0) @ %request @ userName @ %this;
    }
    %smValue = propertiesValue;
    %request @ userName @ %this;
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
    if (isObject(propertiesValue)) {
        propertiesValue.delete();
        propertiesValue = %userName @ %this @ %userName @ %this @ 0 @ %userName @ %this;
    }
    %this.requestProperties(%userName, %callback);
};
function userPropertiesMgr::forgetProperties(%this, %userName) {
    if (isObject(propertiesValue)) {
        propertiesValue.delete();
        propertiesValue = %userName @ %this @ %userName @ %this @ "" @ %userName @ %this;
    }
};
function userPropertiesMgr::getStandaloneFilename(%this, %userName) {
    %ret = "userprops_" @ %this @ clientOrServer @ "_" @ %userName;
};
