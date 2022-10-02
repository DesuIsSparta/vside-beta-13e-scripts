$gUserProperties_BackendImplemented = 1;
function userPropertiesMgr::getProperty(%this, %userName, %propertyName, %default)
{
    %smValue = %this.propertiesValue[%userName];
    if (!isObject(%smValue))
    {
        error(getScopeName() SPC "- properties not fetched yet:" SPC %userName SPC %propertyName SPC getTrace());
        return %default;
    }
    if (!%smValue.hasKey(%propertyName))
    {
        log("general", "debug", getScopeName() SPC "- asked for unknown property: \"" @ %propertyName @ "\"" SPC getTrace());
        return %default;
    }
    return %smValue.get(%propertyName);
}
function userPropertiesMgr::setProperty(%this, %userName, %propertyName, %propertyValue)
{
    %smValue = %this.propertiesValue[%userName];
    if (!isObject(%smValue))
    {
        error(getScopeName() SPC "- not initialized for" SPC %userName SPC getTrace());
        return ;
    }
    %propertyValue = %propertyValue $= "false" ? 0 : %propertyValue;
    %propertyValue = %propertyValue $= "true" ? 1 : %propertyValue;
    if (%smValue.hasKey(%propertyName) && (%smValue.get(%propertyName) $= %propertyValue))
    {
        return ;
    }
    %smValue.put(%propertyName, %propertyValue);
    %this.persistSchedule(%userName);
    return ;
}
function userPropertiesMgr::hasProperty(%this, %userName, %propertyName)
{
    %smValue = %this.propertiesValue[%userName];
    if (!isObject(%smValue))
    {
        error(getScopeName() SPC "- properties not fetched yet:" SPC %userName SPC %propertyName SPC getTrace());
        return %default;
    }
    return %smValue.hasKey(%propertyName);
}
function userPropertiesMgr::dumpProperties(%this, %userName)
{
    %smValue = %this.propertiesValue[%userName];
    if (!isObject(%smValue))
    {
        error(getScopeName() SPC "- not initialized for" SPC %userName SPC getTrace());
        return ;
    }
    %smValue.dumpValues();
    return ;
}
function userPropertiesMgr::clearProperty(%this, %userName, %propertyName)
{
    %this._clearProperty(%userName, %propertyName, 1);
    return ;
}
function userPropertiesMgr::clearPropertyIfExists(%this, %userName, %propertyName)
{
    %this._clearProperty(%userName, %propertyName, 0);
    return ;
}
function userPropertiesMgr::_clearProperty(%this, %userName, %propertyName, %warn)
{
    %smValue = %this.propertiesValue[%userName];
    if (!isObject(%smValue))
    {
        error(getScopeName() SPC "- not initialized for" SPC %userName SPC getTrace());
        return ;
    }
    if (%smValue.hasKey(%propertyName))
    {
        %smValue.remove(%propertyName);
        %this.persistSchedule(%userName);
    }
    else
    {
        if (%warn)
        {
            warn(getScopeName() SPC "- property does not exist:" SPC %propertyName SPC %userName SPC getTrace());
        }
    }
    return ;
}
function userPropertiesMgr::incrementIntegerProperty(%this, %userName, %propertyName, %incrementAmount)
{
    %curVal = %this.getProperty(%userName, %propertyName, 0);
    %newVal = %curVal + %incrementAmount;
    %this.setProperty(%userName, %propertyName, %newVal);
    return %newVal;
}
function userPropertiesMgr::haveProperties(%this, %userName)
{
    %smValue = %this.propertiesValue[%userName];
    return isObject(%smValue);
}
function userPropertiesMgr::persistSchedule(%this, %userName)
{
    if (!(%this.propertiesPersistSchedule[%userName] $= ""))
    {
        cancel(%this.propertiesPersistSchedule[%userName]);
    }
    %this.propertiesPersistSchedule[%userName] = %this.schedule(%this.persistPeriodMS, "persistReally", %userName);
    return ;
}
function userPropertiesMgr::persistReally(%this, %userName, %callback)
{
    if (!isDefined("%callback"))
    {
        %callback = "";
    }
    if (!(%this.propertiesPersistSchedule[%userName] $= ""))
    {
        cancel(%this.propertiesPersistSchedule[%userName]);
        %this.propertiesPersistSchedule[%userName] = "";
    }
    %smValue = %this.propertiesValue[%userName];
    if (!isObject(%smValue))
    {
        error(getScopeName() SPC "- not initialized!" SPC %userName SPC getTrace());
        return ;
    }
    if ($StandAlone)
    {
        %fileName = %this.getStandaloneFilename(%userName);
        %smValue.saveToLocalStorage(%fileName);
        if (!(%callback $= ""))
        {
            schedule(200, 0, "eval", %callback);
        }
        echo(getScopeName() SPC "- standalone! persisted to" SPC %fileName);
        return ;
    }
    if (!haveValidManagerHost())
    {
        warn(getScopeName() SPC "- not connected to backend - properties not persisted." SPC %this.clientOrServer SPC %userName);
        return ;
    }
    if (isObject(%this.saveUserPropertiesRequest[%userName]))
    {
        warn(getScopeName() SPC "- already have a post outstanding!" SPC getTrace());
        %this.persistSchedule(%userName);
    }
    if (%this.isClient())
    {
        %request = sendRequest_SaveClientUserProperties(%userName, %smValue, "onDoneOrErrorCallback_SetClientOrServerUserProperties");
    }
    else
    {
        %request = sendRequest_SaveServerUserProperties(%userName, %smValue, "onDoneOrErrorCallback_SetClientOrServerUserProperties");
    }
    %request.userPropertiesMgr = %this;
    %request.userName = %userName;
    %request.otherCallback = %callback;
    %this.saveUserPropertiesRequest[%userName] = %request;
    return ;
}
function onDoneOrErrorCallback_SetClientOrServerUserProperties(%request)
{
    %request.userPropertiesMgr.saveUserPropertiesRequest[%request.userName] = "";
    if (!(%request.otherCallback $= ""))
    {
        echoDebug(getScopeName() SPC "- eval(" @ %request.otherCallback @ "):");
        eval(%request.otherCallback);
    }
    return ;
}
function userProperties_makeManager(%name, %isClient)
{
    if (isObject(%name))
    {
        return %name;
    }
    %mgr = safeNewScriptObject("ScriptObject", "", 0);
    %mgr.bindClassName("userPropertiesMgr");
    %mgr.setName(%name);
    %mgr.persistPeriodMS = 4000;
    %mgr.clientOrServer = %isClient ? "client" : "server";
    return %mgr;
}
function userPropertiesMgr::isClient(%this)
{
    return %this.clientOrServer $= "client";
}
function userPropertiesMgr::isServer()
{
    return !%this.isClient();
}
function userPropertiesMgr::requestProperties(%this, %userName, %callback)
{
    if (isObject(%this.propertiesValue[%userName]))
    {
        warn(getScopeName() SPC "- Requesting user properties when we\'ve already got them." SPC %userName SPC getTrace());
        if (!(%callback $= ""))
        {
            eval(%callback);
        }
        return ;
    }
    if ($StandAlone)
    {
        %fileName = %this.getStandaloneFilename(%userName);
        %this.propertiesValue[%userName] = safeNewScriptObject("StringMap", "", 0);
        %this.propertiesValue[%userName].loadFromLocalStorage(%fileName, "debug");
        if (!(%callback $= ""))
        {
            schedule(200, 0, "eval", %callback);
        }
        echo(getScopeName() SPC "- standalone! loaded from" SPC %fileName);
        return ;
    }
    if ((!haveValidManagerHost() || %this.isClient()) && !haveValidToken())
    {
        warn(getScopeName() SPC "- not connected to backend - properties not retrieved." SPC %this.clientOrServer SPC %userName);
        if (!isObject(%this.propertiesValue[%userName]))
        {
            %this.propertiesValue[%userName] = safeNewScriptObject("StringMap", "", 0);
        }
        eval(%callback);
        return ;
    }
    if (isObject(%this.getUserPropertiesRequest[%userName]))
    {
        return ;
    }
    if (%this.isClient())
    {
        %request = sendRequest_GetClientUserProperties(%userName, "onDoneOrErrorCallback_GetClientOrServerUserProperties");
    }
    else
    {
        %request = sendRequest_GetServerUserProperties(%userName, "onDoneOrErrorCallback_GetClientOrServerUserProperties");
    }
    %request.userPropertiesMgr = %this;
    %request.userName = %userName;
    %request.otherCallback = %callback;
    %request.retryTotal = 4;
    %request.retryDelay = 500;
    return ;
}
function onDoneOrErrorCallback_GetClientOrServerUserProperties(%request)
{
    if (%request.checkSuccess())
    {
        %request.userPropertiesMgr.parseRequest(%request);
    }
    if (!(%request.otherCallback $= ""))
    {
        echoDebug(getScopeName() SPC "- eval(" @ %request.otherCallback @ "):");
        eval(%request.otherCallback);
    }
    return ;
}
function userPropertiesMgr::parseRequest(%this, %request)
{
    if (!isObject(%this.propertiesValue[%request.userName]))
    {
        %this.propertiesValue[%request.userName] = safeNewScriptObject("StringMap", "", 0);
    }
    %smValue = %this.propertiesValue[%request.userName];
    %smValue.clear();
    %num = %request.getValue("propertyCount");
    %n = 0;
    while (%n < %num)
    {
        %key = utf8Decode(%request.getValue("property" @ %n @ ".key"));
        %value = utf8Decode(%request.getValue("property" @ %n @ ".value"));
        %value = %value $= "false" ? 0 : %value;
        %value = %value $= "true" ? 1 : %value;
        %smValue.put(%key, %value);
        %n = %n + 1;
    }
}

function userPropertiesMgr::requestPropertiesForce(%this, %userName, %callback)
{
    if (isObject(%this.propertiesValue[%userName]))
    {
        %this.propertiesValue[%userName].delete();
        %this.propertiesValue[%userName] = 0;
    }
    %this.requestProperties(%userName, %callback);
    return ;
}
function userPropertiesMgr::forgetProperties(%this, %userName)
{
    if (isObject(%this.propertiesValue[%userName]))
    {
        %this.propertiesValue[%userName].delete();
        %this.propertiesValue[%userName] = "";
    }
    return ;
}
function userPropertiesMgr::getStandaloneFilename(%this, %userName)
{
    %ret = "userprops_" @ %this.clientOrServer @ "_" @ %userName;
    return ;
}
