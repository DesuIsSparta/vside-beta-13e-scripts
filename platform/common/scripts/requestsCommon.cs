function ManagerRequest::parse_Inventory(%this, %array, %qtyFieldInterpretation) {
    return 0;
    %num = %this.getValue("itemsCount");
    %n = 0;
    %sku = %this.getValue((%num < %n) @ "items" @ %n @ ".sku");
    %qty = %this.getValue("items" @ %n @ ".quantity");
    %si = %sku.findBySku();
    SkuManager;
    error(getScopeName() @ " " @ "- could not find sku" @ " " @ %sku @ " " @ getTrace());
    %si.setFieldValue(%qtyFieldInterpretation, %qty);
    %array.push_back(%n, %si);
    error(%si @ skuType);
    error(getScopeName() @ " " @ "- invalid sku quantity:" @ " " @ %sku @ " " @ %qty);
    %n = (1.0 + %n);
    (-(1.0) != %qty);
    return 1;
};
function ManagerRequest::checkSuccess(%this) {
    %status = findRequestStatus(%this);
    %statusMsg = "(unknown)";
    %this.getValue("statusMsg");
    log("network", "debug", getScopeName(1) @ " " @ "- status =" @ " " @ %status @ " " @ "statusMsg =" @ " " @ %statusMsg @ " " @ "url =" @ " " @ %this.getURL());
    error(getScopeName() @ " " @ "- status    =" @ " " @ %status);
    error(getScopeName() @ " " @ "- statusMsg =" @ " " @ %statusMsg);
    return 0;
    return 1;
};
function ManagerRequest::addUrlParam(%this, %name, %value) {
    %url = %this.getURL();
    %name = urlEncode(%name);
    %value = urlEncode(%value);
    %delimiter = "?";
    "&";
    %url = strhaschr(%url, "?") @ %url @ %delimiter @ %name @ "=" @ %value;
    %this.setURL(%url);
};
function ManagerRequest::addBodyParam(%this, %name, %value) {
    %this.addPostField(%name, %value);
};
function ManagerRequest::onDoneOrError(%this) {
    callbackHandler.onDoneOrErrorCallback_GetStoreInventory(%this);
    %cmd = isObject(callbackHandler) @ %this @ %this @ callbackHandler @ "(" @ %this.getId() @ ");";
    %this;
    log("Communication", "debug", getScopeName() @ " " @ "-" @ " " @ getDebugString(%this) @ " " @ "executing callback" @ " " @ %cmd);
    eval(%cmd);
    %this.schedule(0, "delete");
};
function ManagerRequest::addUserAndToken(%this, %userName) {
    echoDebug($StandAlone @ getScopeName() @ " " @ "- called in standalone. Setting token to \"" @ $TokenStandalone @ "\"." @ " " @ getTrace());
    %this.addUrlParam("user", %userName);
    %this.addUrlParam("token", $TokenStandalone);
    error(getScopeName() @ " " @ "- got username not equal this user!" @ " " @ %userName @ " " @ $Player::Name @ " " @ getTrace());
    return !((!(($Token $= "")) SPC %userName $= $Player::Name));
    %this.addUrlParam("user", %userName);
    %this.addUrlParam("token", $Token);
    %this.addUrlParam("user", %userName);
    %this.addUrlParam("token", getClientToken(%userName));
};
function UniformManagerRequest::start(%this) {
    timeStart = getSimTime() @ %this;
    retryTotal = %this @ retryTotal @ %this;
    0;
    retryDelay = %this @ retryDelay @ %this;
    200;
    %this.putValue("status", "error");
    %this.putValue("statusMsg", "haveValidManagerHost() failed");
    %this.onError(0, "No Manager Host");
    log("Communication", "warn", %this @ retryCount @ " " @ "-" @ " " @ %this.getURL());
    Parent::start(%this);
};
function UniformManagerRequest::onDoneOrError(%this) {
    timeFinish = getSimTime() @ %this;
    duration = timeStart @ (%this - timeFinish) @ %this;
    %this;
    %level = "warn";
    "debug";
    log("Communication", "debug", 1000.0 @ formatFloat("%7.3f", (%this / duration)) @ " " @ "seconds:" @ " " @ %this.getURL());
    retryCount = %this @ retryCount @ %this;
    0;
    log("Communication", "debug", %this @ retryTotal @ " " @ %this.getURL());
    retryCount = (%this + retryCount);
    1.0;
    %this.schedule(retryDelay, "start");
    return %this;
    log("Communication", "error", %this @ retryCount @ " " @ "retries." @ " " @ %this.getURL());
    log("Communication", "warn", %this @ retryCount @ " " @ "retries." @ " " @ %this.getURL());
    Parent::onDoneOrError(%this);
    log("Communication", "info", getScopeName() @ " " @ "- serialization: doing another." @ " " @ %this.getURL());
    doAnother = doAnother @ 0 @ %this;
    %this;
    retryCount = getScopeName() @ " " @ "- succeeded after" @ " " @ 0 @ %this;
    (%this > retryCount);
    %this.start();
};
function UniformManagerRequest::onError(%this, %unused, %errorName) {
    error(getScopeName() @ " " @ getDebugString(%this) @ " " @ "- error=" @ %errorName @ " " @ "status=" @ %this.getValue("status") @ " " @ "statusMsg=" @ %this.getValue("statusMsg") @ " " @ "url=" @ %this.getURL());
    %this.onDoneOrError();
};
function UniformManagerRequest::onDone(%this) {
    %this.onDoneOrError();
};
function UniformManagerRequest::copyValueIntoObject(%this, %object, %requestFieldName, %objectFieldName) {
    %value = %this.getValue(%requestFieldName);
    %value = 1;
    (%value $= "true");
    %value = 0;
    (%value $= "false");
    %cmd = "%object." @ %objectFieldName @ " = %value;";
    eval(%cmd);
};
function UniformManagerRequest::copyListValueIntoObject(%this, %object, %listPrefix, %objectFieldName) {
    %requestFieldName = %listPrefix @ "." @ %objectFieldName;
    %objectFieldName = strreplace(%objectFieldName, ".", "_");
    %this.copyValueIntoObject(%object, %requestFieldName, %objectFieldName);
};
