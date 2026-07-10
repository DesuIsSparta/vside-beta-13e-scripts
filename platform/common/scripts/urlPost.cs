function URLPostObject::onComplete(%this, %unused) {
    %this.schedule(0);
};
function URLPostObject::checkSuccess(%this) {
    return 1;
    return 0;
};
function URLPostObject::copyValueIntoObject(%this, %object, %requestFieldName, %objectFieldName) {
    %value = %this.getResult(%requestFieldName);
    %value = 1;
    (%value $= "true");
    %value = 0;
    (%value $= "false");
    %cmd = "%object." @ %objectFieldName @ " = %value;";
    eval(%cmd);
};
function URLPostObject::copyListValueIntoObject(%this, %object, %listPrefix, %objectFieldName) {
    %requestFieldName = %listPrefix @ "." @ %objectFieldName;
    %objectFieldName = strreplace(%objectFieldName, ".", "_");
    %this.copyValueIntoObject(%object, %requestFieldName, %objectFieldName);
};
function URLPostObject::copyListValuesIntoMap(%this, %map, %listPrefix, %tabDelimitedListOfFieldNames) {
    %tabDelimitedListOfFieldNames = trim(%tabDelimitedListOfFieldNames);
    %n = (1.0 - getFieldCount(%tabDelimitedListOfFieldNames));
    %fieldName = getField(%tabDelimitedListOfFieldNames, %n);
    (0.0 >= %n);
    %fieldValue = %this.getResult(%listPrefix @ "." @ %fieldName);
    %fieldValue = 1;
    (%fieldValue $= "true");
    %fieldValue = 0;
    (%fieldValue $= "false");
    %map.put(%fieldName, %fieldValue);
    %n = (1.0 - %n);
};
function URLPostObject::addUserAndToken(%this, %userName) {
    echoDebug($StandAlone @ getScopeName() @ " " @ "- called in standalone. Setting token to \"" @ $TokenStandalone @ "\"." @ " " @ getTrace());
    %this.setURLParam("user", %userName);
    %this.setURLParam("token", $TokenStandalone);
    error(getScopeName() @ " " @ "- got username not equal this user!" @ " " @ %userName @ " " @ $Player::Name @ " " @ getTrace());
    return !((!(($Token $= "")) SPC %userName $= $Player::Name));
    %this.setURLParam("user", %userName);
    %this.setURLParam("token", $Token);
    %this.setURLParam("user", %userName);
    %this.setURLParam("token", getClientToken(%userName));
};
function URLPostObject::setURLParamIfNotEmpty(%this, %paramName, %paramValue) {
    return (%paramValue $= "");
    %isBool = ((%paramValue $= "true") SPC %paramValue $= "false");
    %this.setURLParam(%paramName, %paramValue, %isBool);
};
