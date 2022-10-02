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
            error(getScopeName() SPC "- could not find sku" SPC %sku SPC getTrace());
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
                    error(getScopeName() SPC "- more than one non-furnishing SKU owned::" SPC %sku SPC %qty SPC %si.skuType);
                }
            }
            else
            {
                if ((%qty < 1) && (%qty != -1))
                {
                    error(getScopeName() SPC "- invalid sku quantity:" SPC %sku SPC %qty);
                }
            }
        }
        %n = %n + 1;
    }
    return 1;
}
function ManagerRequest::checkSuccess(%this)
{
    %status = findRequestStatus(%this);
    %statusMsg = %this.hasKey("statusMsg") ? %this.getValue("statusMsg") : "(unknown)";
    log("network", "debug", getScopeName(1) SPC "- status =" SPC %status SPC "statusMsg =" SPC %statusMsg SPC "url =" SPC %this.getURL());
    if (!(%status $= "success"))
    {
        error(getScopeName() SPC "- status    =" SPC %status);
        error(getScopeName() SPC "- statusMsg =" SPC %statusMsg);
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
    return ;
}
function ManagerRequest::addBodyParam(%this, %name, %value)
{
    %this.addPostField(%name, %value);
    return ;
}
function ManagerRequest::onDoneOrError(%this)
{
    if (!(%this.callbackHandler $= ""))
    {
        if (isObject(%this.callbackHandler))
        {
            %this.callbackHandler.onDoneOrErrorCallback_GetStoreInventory(%this);
        }
        else
        {
            %cmd = %this.callbackHandler @ "(" @ %this.getId() @ ");";
            log("Communication", "debug", getScopeName() SPC "-" SPC getDebugString(%this) SPC "executing callback" SPC %cmd);
            eval(%cmd);
        }
    }
    %this.schedule(0, "delete");
    return ;
}
function ManagerRequest::addUserAndToken(%this, %userName)
{
    if ($StandAlone)
    {
        echoDebug(getScopeName() SPC "- called in standalone. Setting token to \"" @ $TokenStandalone @ "\"." SPC getTrace());
        %this.addUrlParam("user", %userName);
        %this.addUrlParam("token", $TokenStandalone);
    }
    else
    {
        if (!($Token $= ""))
        {
            if (!(%userName $= $Player::Name))
            {
                error(getScopeName() SPC "- got username not equal this user!" SPC %userName SPC $Player::Name SPC getTrace());
                return ;
            }
            %this.addUrlParam("user", %userName);
            %this.addUrlParam("token", $Token);
        }
        else
        {
            %this.addUrlParam("user", %userName);
            %this.addUrlParam("token", getClientToken(%userName));
        }
    }
    return ;
}
function UniformManagerRequest::start(%this)
{
    %this.timeStart = getSimTime();
    %this.retryTotal = %this.retryTotal $= "" ? 0 : %this;
    %this.retryDelay = %this.retryDelay $= "" ? 200 : %this;
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
            log("Communication", "warn", "Retry number" SPC %this.retryCount SPC "-" SPC %this.getURL());
        }
        Parent::start(%this);
    }
    return ;
}
function UniformManagerRequest::onDoneOrError(%this)
{
    %this.timeFinish = getSimTime();
    %this.duration = %this.timeFinish - %this.timeStart;
    %level = %this.duration < 1000 ? "debug" : "warn";
    log("Communication", "debug", "Request duration" SPC formatFloat("%7.3f", %this.duration / 1000) SPC "seconds:" SPC %this.getURL());
    %this.retryCount = %this.retryCount $= "" ? 0 : %this;
    if (!(findRequestStatus(%this) $= "success"))
    {
        log("Communication", "debug", getScopeName() SPC "checking retries.." SPC %this.retryCount @ "/" @ %this.retryTotal SPC %this.getURL());
        if (%this.retryCount < %this.retryTotal)
        {
            %this.retryCount = %this.retryCount + 1;
            %this.schedule(%this.retryDelay, "start");
            return ;
        }
        else
        {
            log("Communication", "error", getScopeName() SPC "- failed after" SPC %this.retryCount SPC "retries." SPC %this.getURL());
        }
    }
    else
    {
        if (%this.retryCount > 0)
        {
            log("Communication", "warn", getScopeName() SPC "- succeeded after" SPC %this.retryCount SPC "retries." SPC %this.getURL());
        }
    }
    Parent::onDoneOrError(%this);
    if (%this.doAnother)
    {
        log("Communication", "info", getScopeName() SPC "- serialization: doing another." SPC %this.getURL());
        %this.doAnother = 0;
        %this.retryCount = 0;
        %this.start();
    }
    return ;
}
function UniformManagerRequest::onError(%this, %unused, %errorName)
{
    error(getScopeName() SPC getDebugString(%this) SPC "- error=" @ %errorName SPC "status=" @ %this.getValue("status") SPC "statusMsg=" @ %this.getValue("statusMsg") SPC "url=" @ %this.getURL());
    %this.onDoneOrError();
    return ;
}
function UniformManagerRequest::onDone(%this)
{
    %this.onDoneOrError();
    return ;
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
    return ;
}
function UniformManagerRequest::copyListValueIntoObject(%this, %object, %listPrefix, %objectFieldName)
{
    %requestFieldName = %listPrefix @ "." @ %objectFieldName;
    %objectFieldName = strreplace(%objectFieldName, ".", "_");
    %this.copyValueIntoObject(%object, %requestFieldName, %objectFieldName);
    return ;
}
