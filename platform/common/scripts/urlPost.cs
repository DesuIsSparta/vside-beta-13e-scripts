function URLPostObject::onComplete(%this, %unused) {
    if (!(%this.NoAutoDelete)) {
        delete.schedule(%this, 0);
    }
};
function URLPostObject::checkSuccess(%this) {
    if (("status".getResult(%this) $= "success")) {
        return 1;
    }
    return 0;
};
function URLPostObject::copyValueIntoObject(%this, %object, %requestFieldName, %objectFieldName) {
    %value = %requestFieldName.getResult(%this);
    if ((%value $= "true")) {
        %value = 1;
    }
    if ((%value $= "false")) {
        %value = 0;
    }
    %cmd = "%object." @ %objectFieldName @ " = %value;";
    eval(%cmd);
};
function URLPostObject::copyListValueIntoObject(%this, %object, %listPrefix, %objectFieldName) {
    %requestFieldName = %listPrefix @ "." @ %objectFieldName;
    %objectFieldName = strreplace(%objectFieldName, ".", "_");
    %objectFieldName.copyValueIntoObject(%this, %object, %requestFieldName);
};
function URLPostObject::copyListValuesIntoMap(%this, %map, %listPrefix, %tabDelimitedListOfFieldNames) {
    %tabDelimitedListOfFieldNames = trim(%tabDelimitedListOfFieldNames);
    %n = (getFieldCount(%tabDelimitedListOfFieldNames) - 1.0);
    while ((%n >= 0.0)) {
        %fieldName = getField(%tabDelimitedListOfFieldNames, %n);
        %fieldValue = %listPrefix @ "." @ %fieldName.getResult(%this);
        if ((%fieldValue $= "true")) {
            %fieldValue = 1;
        }
        if ((%fieldValue $= "false")) {
            %fieldValue = 0;
        }
        %fieldValue.put(%map, %fieldName);
        %n = (%n - 1.0);
    }
};
function URLPostObject::addUserAndToken(%this, %userName) {
    if ($StandAlone) {
        echoDebug(getScopeName() @ " " @ "- called in standalone. Setting token to \"" @ $TokenStandalone @ "\"." @ " " @ getTrace());
        %userName.setURLParam(%this, "user");
        $TokenStandalone.setURLParam(%this, "token");
    }
    if (!($Token $= "")) {
        if (!(%userName $= $Player::Name)) {
            error(getScopeName() @ " " @ "- got username not equal this user!" @ " " @ %userName @ " " @ $Player::Name @ " " @ getTrace());
            return;
        }
        %userName.setURLParam(%this, "user");
        $Token.setURLParam(%this, "token");
    }
    %userName.setURLParam(%this, "user");
    getClientToken(%userName).setURLParam(%this, "token");
};
function URLPostObject::setURLParamIfNotEmpty(%this, %paramName, %paramValue) {
    if ((%paramValue $= "")) {
        return;
    }
    if ((%paramValue $= "true")) {
    }
    %isBool = (%paramValue $= "false");
    %isBool.setURLParam(%this, %paramName, %paramValue);
};
