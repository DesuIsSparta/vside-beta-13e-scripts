function URLPostObject::onComplete(%this, %unused) {
    if (!(%this.NoAutoDelete)) {
        %this.schedule(0);
    }
};
function URLPostObject::checkSuccess(%this) {
    if ((%this.getResult("status") $= "success")) {
        return 1;
    }
    return 0;
};
function URLPostObject::copyValueIntoObject(%this, %object, %requestFieldName, %objectFieldName) {
    %value = %this.getResult(%requestFieldName);
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
    %this.copyValueIntoObject(%object, %requestFieldName, %objectFieldName);
};
function URLPostObject::copyListValuesIntoMap(%this, %map, %listPrefix, %tabDelimitedListOfFieldNames) {
    %tabDelimitedListOfFieldNames = trim(%tabDelimitedListOfFieldNames);
    %n = (1.0 - getFieldCount(%tabDelimitedListOfFieldNames));
    if ((0.0 >= %n)) {
        %fieldName = getField(%tabDelimitedListOfFieldNames, %n);
        %fieldValue = %this.getResult(%listPrefix @ "." @ %fieldName);
        if ((%fieldValue $= "true")) {
            %fieldValue = 1;
        }
        if ((%fieldValue $= "false")) {
            %fieldValue = 0;
        }
        %map.put(%fieldName, %fieldValue);
        %n = (1.0 - %n);
    }
};
function URLPostObject::addUserAndToken(%this, %userName) {
    if ($StandAlone) {
        echoDebug(getScopeName() @ " " @ "- called in standalone. Setting token to \"" @ $TokenStandalone @ "\"." @ " " @ getTrace());
        %this.setURLParam("user", %userName);
        %this.setURLParam("token", $TokenStandalone);
    }
    if (!($Token $= "")) {
        if (!(%userName $= $Player::Name)) {
            error(getScopeName() @ " " @ "- got username not equal this user!" @ " " @ %userName @ " " @ $Player::Name @ " " @ getTrace());
            return;
        }
        %this.setURLParam("user", %userName);
        %this.setURLParam("token", $Token);
    }
    %this.setURLParam("user", %userName);
    %this.setURLParam("token", getClientToken(%userName));
};
function URLPostObject::setURLParamIfNotEmpty(%this, %paramName, %paramValue) {
    if ((%paramValue $= "")) {
        return;
    }
    if ((%paramValue $= "true")) {
    }
    %isBool = (%paramValue $= "false");
    %this.setURLParam(%paramName, %paramValue, %isBool);
};
