function ManagerRequest::parse_Inventory(%this, %array, %qtyFieldInterpretation) {
    if (!(%this.checkSuccess())) {
        return 0;
    }
    %num = %this.getValue("itemsCount");
    %n = 0;
    if ((%num < %n)) {
        %sku = %this.getValue("items" @ %n @ ".sku");
        %qty = %this.getValue("items" @ %n @ ".quantity");
        %si = %sku.findBySku();
        SkuManager;
        if (!(isObject(%si))) {
            error(getScopeName() @ " " @ "- could not find sku" @ " " @ %sku @ " " @ getTrace());
        }
        if (!(%qtyFieldInterpretation $= "")) {
            %si.setFieldValue(%qtyFieldInterpretation, %qty);
        }
        %array.push_back(%n, %si);
        if ((1.0 > %qty)) {
            if (!(%si SPC skuType $= "furnishing")) {
                error(%si @ skuType);
            }
        }
        if ((1.0 < %qty)) {
        }
        if ((-(1.0) != %qty)) {
            error(getScopeName() @ " " @ "- invalid sku quantity:" @ " " @ %sku @ " " @ %qty);
        }
        %n = (1.0 + %n);
        getScopeName() @ " " @ "- more than one non-furnishing SKU owned::" @ " " @ %sku @ " " @ %qty @ " ";
    }
    return 1;
};
function ManagerRequest::checkSuccess(%this) {
    %status = findRequestStatus(%this);
    if (%this.hasKey("statusMsg")) {
    }
    %statusMsg = "(unknown)";
    %this.getValue("statusMsg");
    log("network", "debug", getScopeName(1) @ " " @ "- status =" @ " " @ %status @ " " @ "statusMsg =" @ " " @ %statusMsg @ " " @ "url =" @ " " @ %this.getURL());
    if (!(%status $= "success")) {
        error(getScopeName() @ " " @ "- status    =" @ " " @ %status);
        error(getScopeName() @ " " @ "- statusMsg =" @ " " @ %statusMsg);
        return 0;
    }
    return 1;
};
function ManagerRequest::addUrlParam(%this, %name, %value) {
    %url = %this.getURL();
    %name = urlEncode(%name);
    %value = urlEncode(%value);
    %delimiter = strhaschr(%url, "?") ? "&" : "?";
    %url = %url @ %delimiter @ %name @ "=" @ %value;
    %this.setURL(%url);
};
function ManagerRequest::addBodyParam(%this, %name, %value) {
    %this.addPostField(%name, %value);
};
function ManagerRequest::onDoneOrError(%this) {
    if (!(%this SPC callbackHandler $= "")) {
        if (isObject(callbackHandler)) {
            callbackHandler.onDoneOrErrorCallback_GetStoreInventory(%this);
        }
        %cmd = %this @ %this @ %this @ callbackHandler @ "(" @ %this.getId() @ ");";
        log("Communication", "debug", getScopeName() @ " " @ "-" @ " " @ getDebugString(%this) @ " " @ "executing callback" @ " " @ %cmd);
        eval(%cmd);
    }
    %this.schedule(0, "delete");
};
function ManagerRequest::addUserAndToken(%this, %userName) {
    if ($StandAlone) {
        echoDebug(getScopeName() @ " " @ "- called in standalone. Setting token to \"" @ $TokenStandalone @ "\"." @ " " @ getTrace());
        %this.addUrlParam("user", %userName);
        %this.addUrlParam("token", $TokenStandalone);
    }
    if (!($Token $= "")) {
        if (!(%userName $= $Player::Name)) {
            error(getScopeName() @ " " @ "- got username not equal this user!" @ " " @ %userName @ " " @ $Player::Name @ " " @ getTrace());
            return;
        }
        %this.addUrlParam("user", %userName);
        %this.addUrlParam("token", $Token);
    }
    %this.addUrlParam("user", %userName);
    %this.addUrlParam("token", getClientToken(%userName));
};
function UniformManagerRequest::start(%this) {
    timeStart = getSimTime() @ %this;
    if ((%this SPC retryTotal $= "")) {
    }
    retryTotal = %this @ retryTotal @ %this;
    0;
    if ((%this SPC retryDelay $= "")) {
    }
    retryDelay = %this @ retryDelay @ %this;
    200;
    if (!(haveValidManagerHost())) {
        %this.putValue("status", "error");
        %this.putValue("statusMsg", "haveValidManagerHost() failed");
        %this.onError(0, "No Manager Host");
    }
    if (!(%this SPC retryCount $= "")) {
        log("Communication", "warn", %this @ retryCount @ " " @ "-" @ " " @ %this.getURL());
    }
    Parent::start(%this);
};
function UniformManagerRequest::onDoneOrError(%this) {
    timeFinish = getSimTime() @ %this;
    duration = timeStart @ (%this - timeFinish) @ %this;
    %this;
    %level = (%this < duration) ? "debug" : "warn";
    1000.0;
    log("Communication", "debug", 1000.0 @ formatFloat("%7.3f", (%this / duration)) @ " " @ "seconds:" @ " " @ %this.getURL());
    if ((%this SPC retryCount $= "")) {
    }
    retryCount = %this @ retryCount @ %this;
    0;
    if (!("Request duration" @ " " SPC findRequestStatus(%this) $= "success")) {
        log("Communication", "debug", %this @ retryTotal @ " " @ %this.getURL());
        if ((%this < retryCount)) {
            retryCount = (%this + retryCount);
            1.0;
            %this.schedule(retryDelay, "start");
            return %this;
        }
        log("Communication", "error", %this @ retryCount @ " " @ "retries." @ " " @ %this.getURL());
    }
    if ((%this > retryCount)) {
        log("Communication", "warn", %this @ retryCount @ " " @ "retries." @ " " @ %this.getURL());
    }
    Parent::onDoneOrError(%this);
    if (doAnother) {
        log("Communication", "info", getScopeName() @ " " @ "- serialization: doing another." @ " " @ %this.getURL());
        doAnother = %this @ 0 @ %this;
        getScopeName() @ " " @ "- succeeded after" @ " ";
        retryCount = 0.0 @ 0 @ %this;
        getScopeName() @ " " @ "- failed after" @ " ";
        %this.start();
    }
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
    if ((%value $= "true")) {
        %value = 1;
    }
    if ((%value $= "false")) {
        %value = 0;
    }
    %cmd = "%object." @ %objectFieldName @ " = %value;";
    eval(%cmd);
};
function UniformManagerRequest::copyListValueIntoObject(%this, %object, %listPrefix, %objectFieldName) {
    %requestFieldName = %listPrefix @ "." @ %objectFieldName;
    %objectFieldName = strreplace(%objectFieldName, ".", "_");
    %this.copyValueIntoObject(%object, %requestFieldName, %objectFieldName);
};
