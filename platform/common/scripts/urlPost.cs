function URLPostObject::onComplete(%this, %unused)
{
    if (!%this.NoAutoDelete)
    {
        %this.schedule(0, delete);
    }
    return ;
}
function URLPostObject::checkSuccess(%this)
{
    if (%this.getResult("status") $= "success")
    {
        return 1;
    }
    return 0;
}
function URLPostObject::copyValueIntoObject(%this, %object, %requestFieldName, %objectFieldName)
{
    %value = %this.getResult(%requestFieldName);
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
function URLPostObject::copyListValueIntoObject(%this, %object, %listPrefix, %objectFieldName)
{
    %requestFieldName = %listPrefix @ "." @ %objectFieldName;
    %objectFieldName = strreplace(%objectFieldName, ".", "_");
    %this.copyValueIntoObject(%object, %requestFieldName, %objectFieldName);
    return ;
}
function URLPostObject::copyListValuesIntoMap(%this, %map, %listPrefix, %tabDelimitedListOfFieldNames)
{
    %tabDelimitedListOfFieldNames = trim(%tabDelimitedListOfFieldNames);
    %n = getFieldCount(%tabDelimitedListOfFieldNames) - 1;
    while (%n >= 0)
    {
        %fieldName = getField(%tabDelimitedListOfFieldNames, %n);
        %fieldValue = %this.getResult(%listPrefix @ "." @ %fieldName);
        if (%fieldValue $= "true")
        {
            %fieldValue = 1;
        }
        if (%fieldValue $= "false")
        {
            %fieldValue = 0;
        }
        %map.put(%fieldName, %fieldValue);
        %n = %n - 1;
    }
}

function URLPostObject::addUserAndToken(%this, %userName)
{
    if ($StandAlone)
    {
        echoDebug(getScopeName() SPC "- called in standalone. Setting token to \"" @ $TokenStandalone @ "\"." SPC getTrace());
        %this.setURLParam("user", %userName);
        %this.setURLParam("token", $TokenStandalone);
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
            %this.setURLParam("user", %userName);
            %this.setURLParam("token", $Token);
        }
        else
        {
            %this.setURLParam("user", %userName);
            %this.setURLParam("token", getClientToken(%userName));
        }
    }
    return ;
}
function URLPostObject::setURLParamIfNotEmpty(%this, %paramName, %paramValue)
{
    if (%paramValue $= "")
    {
        return ;
    }
    %isBool = (%paramValue $= "true") || (%paramValue $= "false");
    %this.setURLParam(%paramName, %paramValue, %isBool);
    return ;
}
