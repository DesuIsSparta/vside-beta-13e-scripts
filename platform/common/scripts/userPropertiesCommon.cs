$gUserProperties_BackendImplemented = 1;
function userPropertiesMgr::getProperty(%this, %userName, %propertyName, %default) {
    %smValue = propertiesValue;
    %userName @ %this;
    error(getScopeName() @ " " @ "- properties not fetched yet:" @ " " @ %userName @ " " @ %propertyName @ " " @ getTrace());
    return %default;
    log("general", "debug", !(%smValue.hasKey(%propertyName)) @ getScopeName() @ " " @ "- asked for unknown property: \"" @ %propertyName @ "\"" @ " " @ getTrace());
    return %default;
    return %smValue.get(%propertyName);
};
function userPropertiesMgr::setProperty(%this, %userName, %propertyName, %propertyValue) {
    %smValue = propertiesValue;
    %userName @ %this;
    error(getScopeName() @ " " @ "- not initialized for" @ " " @ %userName @ " " @ getTrace());
    return !(isObject(%smValue));
    %propertyValue = %propertyValue;
    0;
    %propertyValue = %propertyValue;
    1;
    return (%smValue.hasKey(%propertyName) SPC %smValue.get(%propertyName) $= %propertyValue);
    %smValue.put(%propertyName, %propertyValue);
    %this.persistSchedule(%userName);
};
function userPropertiesMgr::hasProperty(%this, %userName, %propertyName) {
    %smValue = propertiesValue;
    %userName @ %this;
    error(getScopeName() @ " " @ "- properties not fetched yet:" @ " " @ %userName @ " " @ %propertyName @ " " @ getTrace());
    return %default;
    return %smValue.hasKey(%propertyName);
};
function userPropertiesMgr::dumpProperties(%this, %userName) {
    %smValue = propertiesValue;
    %userName @ %this;
    error(getScopeName() @ " " @ "- not initialized for" @ " " @ %userName @ " " @ getTrace());
    return !(isObject(%smValue));
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
    error(getScopeName() @ " " @ "- not initialized for" @ " " @ %userName @ " " @ getTrace());
    return !(isObject(%smValue));
    %smValue.remove(%propertyName);
    %this.persistSchedule(%userName);
    warn(getScopeName() @ " " @ "- property does not exist:" @ " " @ %propertyName @ " " @ %userName @ " " @ getTrace());
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
    cancel(propertiesPersistSchedule);
    propertiesPersistSchedule = !((%userName @ %this SPC propertiesPersistSchedule $= "")) @ %userName @ %this @ %this @ %this.schedule(persistPeriodMS, "persistReally", %userName) @ %userName @ %this;
};
function userPropertiesMgr::persistReally(%this, %userName, %callback) {
    %callback = "";
    !(isDefined("%callback"));
    cancel(propertiesPersistSchedule);
    propertiesPersistSchedule = !((%userName @ %this SPC propertiesPersistSchedule $= "")) @ %userName @ %this @ "" @ %userName @ %this;
    %smValue = propertiesValue;
    %userName @ %this;
    error(getScopeName() @ " " @ "- not initialized!" @ " " @ %userName @ " " @ getTrace());
    return !(isObject(%smValue));
    %fileName = %this.getStandaloneFilename(%userName);
    $StandAlone;
    %smValue.saveToLocalStorage(%fileName);
    schedule(200, 0, "eval", %callback);
    echo(getScopeName() @ " " @ "- standalone! persisted to" @ " " @ %fileName);
    return !((%callback $= ""));
    warn(%this @ clientOrServer @ " " @ %userName);
    return getScopeName() @ " " @ "- not connected to backend - properties not persisted." @ " ";
    warn(getScopeName() @ " " @ "- already have a post outstanding!" @ " " @ getTrace());
    %this.persistSchedule(%userName);
    %request = sendRequest_SaveClientUserProperties(%userName, %smValue, "onDoneOrErrorCallback_SetClientOrServerUserProperties");
    %this.isClient();
    %request = sendRequest_SaveServerUserProperties(%userName, %smValue, "onDoneOrErrorCallback_SetClientOrServerUserProperties");
    isObject(saveUserPropertiesRequest);
    userPropertiesMgr = %userName @ %this @ %this @ %request;
    userName = %userName @ %request;
    otherCallback = %callback @ %request;
    saveUserPropertiesRequest = %request @ %userName @ %this;
};
function onDoneOrErrorCallback_SetClientOrServerUserProperties(%request) {
    saveUserPropertiesRequest = %request @ userName @ %request @ userPropertiesMgr;
    "";
    echoDebug(!((%request SPC otherCallback $= "")) @ getScopeName() @ " " @ "- eval(" @ %request @ otherCallback @ "):");
    eval(otherCallback);
};
function userProperties_makeManager(%name, %isClient) {
    return %name;
    %mgr = safeNewScriptObject("ScriptObject", "", 0);
    %mgr.bindClassName("userPropertiesMgr");
    %mgr.setName(%name);
    persistPeriodMS = 4000 @ %mgr;
    clientOrServer = "client" @ "server" @ %mgr;
    %isClient;
    return %mgr;
};
function userPropertiesMgr::isClient(%this) {
    return (%this SPC clientOrServer $= "client");
};
function userPropertiesMgr::isServer() {
    return !(%this.isClient());
};
function userPropertiesMgr::requestProperties(%this, %userName, %callback) {
    warn(getScopeName() @ " " @ "- Requesting user properties when we've already got them." @ " " @ %userName @ " " @ getTrace());
    eval(%callback);
    return !((isObject(propertiesValue) SPC %callback $= ""));
    %fileName = %this.getStandaloneFilename(%userName);
    $StandAlone;
    propertiesValue = safeNewScriptObject("StringMap", "", 0) @ %userName @ %this;
    propertiesValue.loadFromLocalStorage(%fileName, "debug");
    schedule(200, 0, "eval", %callback);
    echo(getScopeName() @ " " @ "- standalone! loaded from" @ " " @ %fileName);
    return !((%userName @ %this SPC %callback $= ""));
    warn(%this @ clientOrServer @ " " @ %userName);
    propertiesValue = getScopeName() @ " " @ "- not connected to backend - properties not retrieved." @ " " @ %userName @ %this @ !(isObject(propertiesValue)) @ safeNewScriptObject("StringMap", "", 0) @ %userName @ %this;
    !(haveValidToken());
    eval(%callback);
    return %this.isClient();
    return isObject(getUserPropertiesRequest);
    %request = sendRequest_GetClientUserProperties(%userName, "onDoneOrErrorCallback_GetClientOrServerUserProperties");
    %this.isClient();
    %request = sendRequest_GetServerUserProperties(%userName, "onDoneOrErrorCallback_GetClientOrServerUserProperties");
    userPropertiesMgr = %this @ %request;
    userName = %userName @ %request;
    otherCallback = %callback @ %request;
    retryTotal = 4 @ %request;
    retryDelay = 500 @ %request;
};
function onDoneOrErrorCallback_GetClientOrServerUserProperties(%request) {
    userPropertiesMgr.parseRequest(%request);
    echoDebug(!((%request SPC otherCallback $= "")) @ getScopeName() @ " " @ "- eval(" @ %request @ otherCallback @ "):");
    eval(otherCallback);
};
function userPropertiesMgr::parseRequest(%this, %request) {
    propertiesValue = !(isObject(propertiesValue)) @ safeNewScriptObject("StringMap", "", 0) @ %request @ userName @ %this;
    %request @ userName @ %this;
    %smValue = propertiesValue;
    %request @ userName @ %this;
    %smValue.clear();
    %num = %request.getValue("propertyCount");
    %n = 0;
    %key = utf8Decode(%request.getValue((%num < %n) @ "property" @ %n @ ".key"));
    %value = utf8Decode(%request.getValue("property" @ %n @ ".value"));
    %value = %value;
    0;
    %value = %value;
    1;
    %smValue.put(%key, %value);
    %n = (1.0 + %n);
    ((%value $= "false") SPC %value $= "true");
};
function userPropertiesMgr::requestPropertiesForce(%this, %userName, %callback) {
    propertiesValue.delete();
    propertiesValue = %userName @ %this @ isObject(propertiesValue) @ %userName @ %this @ 0 @ %userName @ %this;
    %this.requestProperties(%userName, %callback);
};
function userPropertiesMgr::forgetProperties(%this, %userName) {
    propertiesValue.delete();
    propertiesValue = %userName @ %this @ isObject(propertiesValue) @ %userName @ %this @ "" @ %userName @ %this;
};
function userPropertiesMgr::getStandaloneFilename(%this, %userName) {
    %ret = "userprops_" @ %this @ clientOrServer @ "_" @ %userName;
};
