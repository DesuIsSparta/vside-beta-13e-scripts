function ManagerRequest::parse_Inventory(%this, %array, %qtyFieldInterpretation)
{
    if (!%this.checkSuccess())
    {
        return 0;
    }
    %num = %this.getValue("itemsCount");
    %n = 0;
    while (%n < %num)
    {
        %sku = %this.getValue("items" @ %n @ ".sku");
        %qty = %this.getValue("items" @ %n @ ".quantity");
        %si = SkuManager.findBySku(%sku);
        if (!isObject(%si))
        {
            error(getScopeName() @ " " @ "- could not find sku" @ " " @ %sku @ " " @ getTrace());
        }
        else
        {
            if (!(%qtyFieldInterpretation $= ""))
            {
                %si.setFieldValue(%qtyFieldInterpretation, %qty);
            }
            %array.push_back(%n, %si);
            if (%qty > 1)
            {
                if (!(%si.skuType $= "furnishing"))
                {
                    error(getScopeName() @ " " @ "- more than one non-furnishing SKU owned::" @ " " @ %sku @ " " @ %qty @ " " @ %si.skuType);
                }
            }
            if ((%qty < 1) && (%qty != -(1)))
            {
                error(getScopeName() @ " " @ "- invalid sku quantity:" @ " " @ %sku @ " " @ %qty);
            }
        }
        %n = %n + 1;
    }
    return 1;
}
function ManagerRequest::checkSuccess(%this)
{
    %status = findRequestStatus(%this);
    if (%this.hasKey("statusMsg"))
    {
    }
    else
    {
    }
    %statusMsg = "(unknown)";
    %this.getValue("statusMsg");
    log("network", "debug", getScopeName(1) @ " " @ "- status =" @ " " @ %status @ " " @ "statusMsg =" @ " " @ %statusMsg @ " " @ "url =" @ " " @ %this.getURL());
    if (!(%status $= "success"))
    {
        error(getScopeName() @ " " @ "- status    =" @ " " @ %status);
        error(getScopeName() @ " " @ "- statusMsg =" @ " " @ %statusMsg);
        return 0;
    }
    return 1;
}
function ManagerRequest::addUrlParam(%this, %name, %value)
{
    %url = %this.getURL();
    %name = urlEncode(%name);
    %value = urlEncode(%value);
    %delimiter = strhaschr(%url, "?") ? "&" : "?";
    %url = %url @ %delimiter @ %name @ "=" @ %value;
    %this.setURL(%url);
}
function ManagerRequest::addBodyParam(%this, %name, %value)
{
    %this.addPostField(%name, %value);
}
function ManagerRequest::onDoneOrError(%this)
{
    if (!(%this.callbackHandler $= ""))
    {
        if (isObject(%this.callbackHandler))
        {
            %this.callbackHandler.onDoneOrErrorCallback_GetStoreInventory(%this);
        }
        %cmd = %this.callbackHandler @ "(" @ %this.getId() @ ");";
        log("Communication", "debug", getScopeName() @ " " @ "-" @ " " @ getDebugString(%this) @ " " @ "executing callback" @ " " @ %cmd);
        eval(%cmd);
    }
    %this.schedule(0, "delete");
}
function ManagerRequest::addUserAndToken(%this, %userName)
{
    if ($StandAlone)
    {
        echoDebug(getScopeName() @ " " @ "- called in standalone. Setting token to \"" @ $TokenStandalone @ "\"." @ " " @ getTrace());
        %this.addUrlParam("user", %userName);
        %this.addUrlParam("token", $TokenStandalone);
    }
    else
    {
        if (!($Token $= ""))
        {
            if (!(%userName $= $Player::Name))
            {
                error(getScopeName() @ " " @ "- got username not equal this user!" @ " " @ %userName @ " " @ $Player::Name @ " " @ getTrace());
                return;
            }
            %this.addUrlParam("user", %userName);
            %this.addUrlParam("token", $Token);
        }
        %this.addUrlParam("user", %userName);
        %this.addUrlParam("token", getClientToken(%userName));
    }
}
function UniformManagerRequest::start(%this)
{
    %this.timeStart = getSimTime();
    if (%this.retryTotal $= "")
    {
    }
    else
    {
    }
    %this.retryTotal = 0 @ %this.retryTotal;
    if (%this.retryDelay $= "")
    {
    }
    else
    {
    }
    %this.retryDelay = 200 @ %this.retryDelay;
    if (!haveValidManagerHost())
    {
        %this.putValue("status", "error");
        %this.putValue("statusMsg", "haveValidManagerHost() failed");
        %this.onError(0, "No Manager Host");
    }
    else
    {
        if (!(%this.retryCount $= ""))
        {
            log("Communication", "warn", "Retry number" @ " " @ %this.retryCount @ " " @ "-" @ " " @ %this.getURL());
        }
        Parent::start(%this);
    }
}
function UniformManagerRequest::onDoneOrError(%this)
{
    %this.timeFinish = getSimTime();
    %this.duration = %this.timeFinish - %this.timeStart;
    %level = (%this.duration < 1000) ? "debug" : "warn";
    log("Communication", "debug", "Request duration" @ " " @ formatFloat("%7.3f", (%this.duration / 1000)) @ " " @ "seconds:" @ " " @ %this.getURL());
    if (%this.retryCount $= "")
    {
    }
    else
    {
    }
    %this.retryCount = 0 @ %this.retryCount;
    if (!(findRequestStatus(%this) $= "success"))
    {
        log("Communication", "debug", getScopeName() @ " " @ "checking retries.." @ " " @ %this.retryCount @ "/" @ %this.retryTotal @ " " @ %this.getURL());
        if (%this.retryCount < %this.retryTotal)
        {
            %this.retryCount = %this.retryCount + 1;
            %this.schedule(%this.retryDelay, "start");
            return;
        }
        else
        {
            log("Communication", "error", getScopeName() @ " " @ "- failed after" @ " " @ %this.retryCount @ " " @ "retries." @ " " @ %this.getURL());
        }
    }
    else
    {
        if (%this.retryCount > 0)
        {
            log("Communication", "warn", getScopeName() @ " " @ "- succeeded after" @ " " @ %this.retryCount @ " " @ "retries." @ " " @ %this.getURL());
        }
    }
    Parent::onDoneOrError(%this);
    if (%this.doAnother)
    {
        log("Communication", "info", getScopeName() @ " " @ "- serialization: doing another." @ " " @ %this.getURL());
        %this.doAnother = 0;
        %this.retryCount = 0;
        %this.start();
    }
}
function UniformManagerRequest::onError(%this, %unused, %errorName)
{
    error(getScopeName() @ " " @ getDebugString(%this) @ " " @ "- error=" @ %errorName @ " " @ "status=" @ %this.getValue("status") @ " " @ "statusMsg=" @ %this.getValue("statusMsg") @ " " @ "url=" @ %this.getURL());
    %this.onDoneOrError();
}
function UniformManagerRequest::onDone(%this)
{
    %this.onDoneOrError();
}
function UniformManagerRequest::copyValueIntoObject(%this, %object, %requestFieldName, %objectFieldName)
{
    %value = %this.getValue(%requestFieldName);
    if (%value $= "true")
    {
        %value = 1;
    }
    if (%value $= "false")
    {
        %value = 0;
    }
    %cmd = "%object." @ %objectFieldName @ " = %value;";
    eval(%cmd);
}
function UniformManagerRequest::copyListValueIntoObject(%this, %object, %listPrefix, %objectFieldName)
{
    %requestFieldName = %listPrefix @ "." @ %objectFieldName;
    %objectFieldName = strreplace(%objectFieldName, ".", "_");
    %this.copyValueIntoObject(%object, %requestFieldName, %objectFieldName);
}
