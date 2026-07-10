function ManagerRequest::parse_Inventory(%this, %array, %qtyFieldInterpretation) {
    if (!(%this.checkSuccess())) {
        return 0;
    }
    %num = "itemsCount".getValue(%this);
    %n = 0;
    while ((%n < %num)) {
        %sku = "items" @ %n @ ".sku".getValue(%this);
        %qty = "items" @ %n @ ".quantity".getValue(%this);
        %si = %sku.findBySku(SkuManager);
        if (!(isObject(%si))) {
            error(getScopeName() @ " " @ "- could not find sku" @ " " @ %sku @ " " @ getTrace());
        }
        if (!(%qtyFieldInterpretation $= "")) {
            %qty.setFieldValue(%si, %qtyFieldInterpretation);
        }
        %si.push_back(%array, %n);
        if ((%qty > 1.0)) {
            if (!(%si.skuType $= "furnishing")) {
                error(getScopeName() @ " " @ "- more than one non-furnishing SKU owned::" @ " " @ %sku @ " " @ %qty @ " " @ %si.skuType);
            }
        }
        if ((%qty < 1.0)) {
        }
        if ((%qty != -(1.0))) {
            error(getScopeName() @ " " @ "- invalid sku quantity:" @ " " @ %sku @ " " @ %qty);
        }
        %n = (%n + 1.0);
    }
    return 1;
};
function ManagerRequest::checkSuccess(%this) {
    %status = findRequestStatus(%this);
    if ("statusMsg".hasKey(%this)) {
    }
    %statusMsg = "(unknown)";
    "statusMsg".getValue(%this);
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
    %url.setURL(%this);
};
function ManagerRequest::addBodyParam(%this, %name, %value) {
    %value.addPostField(%this, %name);
};
function ManagerRequest::onDoneOrError(%this) {
    if (!(%this.callbackHandler $= "")) {
        if (isObject(%this.callbackHandler)) {
            %this.onDoneOrErrorCallback_GetStoreInventory(%this.callbackHandler);
        }
        %cmd = %this.callbackHandler @ "(" @ %this.getId() @ ");";
        log("Communication", "debug", getScopeName() @ " " @ "-" @ " " @ getDebugString(%this) @ " " @ "executing callback" @ " " @ %cmd);
        eval(%cmd);
    }
    "delete".schedule(%this, 0);
};
function ManagerRequest::addUserAndToken(%this, %userName) {
    if ($StandAlone) {
        echoDebug(getScopeName() @ " " @ "- called in standalone. Setting token to \"" @ $TokenStandalone @ "\"." @ " " @ getTrace());
        %userName.addUrlParam(%this, "user");
        $TokenStandalone.addUrlParam(%this, "token");
    }
    if (!($Token $= "")) {
        if (!(%userName $= $Player::Name)) {
            error(getScopeName() @ " " @ "- got username not equal this user!" @ " " @ %userName @ " " @ $Player::Name @ " " @ getTrace());
            return;
        }
        %userName.addUrlParam(%this, "user");
        $Token.addUrlParam(%this, "token");
    }
    %userName.addUrlParam(%this, "user");
    getClientToken(%userName).addUrlParam(%this, "token");
};
function UniformManagerRequest::start(%this) {
    %this.timeStart = getSimTime();
    if ((%this.retryTotal $= "")) {
    }
    %this.retryTotal = 0 @ %this.retryTotal;
    if ((%this.retryDelay $= "")) {
    }
    %this.retryDelay = 200 @ %this.retryDelay;
    if (!(haveValidManagerHost())) {
        "error".putValue(%this, "status");
        "haveValidManagerHost() failed".putValue(%this, "statusMsg");
        "No Manager Host".onError(%this, 0);
    }
    if (!(%this.retryCount $= "")) {
        log("Communication", "warn", "Retry number" @ " " @ %this.retryCount @ " " @ "-" @ " " @ %this.getURL());
    }
    Parent::start(%this);
};
function UniformManagerRequest::onDoneOrError(%this) {
    %this.timeFinish = getSimTime();
    %this.duration = (%this.timeFinish - %this.timeStart);
    %level = (%this.duration < 1000.0) ? "debug" : "warn";
    log("Communication", "debug", "Request duration" @ " " @ formatFloat("%7.3f", (%this.duration / 1000.0)) @ " " @ "seconds:" @ " " @ %this.getURL());
    if ((%this.retryCount $= "")) {
    }
    %this.retryCount = 0 @ %this.retryCount;
    if (!(findRequestStatus(%this) $= "success")) {
        log("Communication", "debug", getScopeName() @ " " @ "checking retries.." @ " " @ %this.retryCount @ "/" @ %this.retryTotal @ " " @ %this.getURL());
        if ((%this.retryCount < %this.retryTotal)) {
            %this.retryCount = (%this.retryCount + 1.0);
            "start".schedule(%this, %this.retryDelay);
            return;
        }
        log("Communication", "error", getScopeName() @ " " @ "- failed after" @ " " @ %this.retryCount @ " " @ "retries." @ " " @ %this.getURL());
    }
    if ((%this.retryCount > 0.0)) {
        log("Communication", "warn", getScopeName() @ " " @ "- succeeded after" @ " " @ %this.retryCount @ " " @ "retries." @ " " @ %this.getURL());
    }
    Parent::onDoneOrError(%this);
    if (%this.doAnother) {
        log("Communication", "info", getScopeName() @ " " @ "- serialization: doing another." @ " " @ %this.getURL());
        %this.doAnother = 0;
        %this.retryCount = 0;
        %this.start();
    }
};
function UniformManagerRequest::onError(%this, %unused, %errorName) {
    error(getScopeName() @ " " @ getDebugString(%this) @ " " @ "- error=" @ %errorName @ " " @ "status=" @ "status".getValue(%this) @ " " @ "statusMsg=" @ "statusMsg".getValue(%this) @ " " @ "url=" @ %this.getURL());
    %this.onDoneOrError();
};
function UniformManagerRequest::onDone(%this) {
    %this.onDoneOrError();
};
function UniformManagerRequest::copyValueIntoObject(%this, %object, %requestFieldName, %objectFieldName) {
    %value = %requestFieldName.getValue(%this);
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
    %objectFieldName.copyValueIntoObject(%this, %object, %requestFieldName);
};
