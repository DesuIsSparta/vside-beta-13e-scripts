function new_ScriptArray(%name) {
    %obj = new ""();
    ScriptObject;
    %obj.bindClassName("ScriptArray");
    numElements = 0 @ 0 @ %obj;
    %obj.setName(%name);
    return %obj;
};
function ScriptArray::append(%this, %value) {
    Array = %value @ %this @ numElements @ %this;
    numElements = (%this + numElements);
    1.0;
};
function ScriptArray::get(%this, %index) {
    if ((0.0 < %index)) {
    }
    if ((numElements >= %index)) {
        error("ScriptArray::get()" @ " " @ "- Subscript out of range:" @ " " @ %index @ " " @ getTrace());
        return "";
    }
    return Array;
};
function ScriptArray::set(%this, %index, %value) {
    if ((numElements > %index)) {
        error("ScriptArray::set()" @ " " @ "- Subscript out of range:" @ " " @ %index @ " " @ "value:" @ " " @ %value @ " " @ getTrace());
        return %this;
    }
    if ((numElements == %index)) {
        %this.append(%value);
    }
    Array = %this @ %value @ %index @ %this;
};
function ScriptArray::size(%this) {
    return numElements;
};
function ScriptArray::clear(%this) {
    numElements = 0 @ %this;
};
function ScriptArray::deleteMembers(%this) {
    %n = 0;
    if ((numElements < %n)) {
        %element = Array;
        %this @ %n @ %this;
        if (isObject(%element)) {
            %element.delete();
        }
        error(getScopeName() @ " " @ "- called on non-object member: \"" @ %element @ "\":" @ " " @ getDebugString(%this) @ " " @ getTrace());
        %n = (1.0 + %n);
    }
    %this.clear();
};
function ScriptArray::dumpValues(%this) {
    %n = 0;
    if ((numElements < %n)) {
        echo(%n @ " " @ %this.get(%n));
        %n = (1.0 + %n);
        %this;
    }
};
