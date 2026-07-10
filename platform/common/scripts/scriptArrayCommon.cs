function new_ScriptArray(%name) {
    %obj = new ScriptObject("");;
    0;
    %obj.bindClassName("ScriptArray");
    %obj.numElements = 0;
    %obj.setName(%name);
    return %obj;
};
function ScriptArray::append(%this, %value) {
    %this.Array = %value @ %this.numElements;
    %this.numElements = (1.0 + %this.numElements);
};
function ScriptArray::get(%this, %index) {
    if ((0.0 < %index)) {
    }
    if ((%this.numElements >= %index)) {
        error("ScriptArray::get()" @ " " @ "- Subscript out of range:" @ " " @ %index @ " " @ getTrace());
        return "";
    }
    return %this.Array;
};
function ScriptArray::set(%this, %index, %value) {
    if ((%this.numElements > %index)) {
        error("ScriptArray::set()" @ " " @ "- Subscript out of range:" @ " " @ %index @ " " @ "value:" @ " " @ %value @ " " @ getTrace());
        return;
    }
    if ((%this.numElements == %index)) {
        %this.append(%value);
    }
    %this.Array = %value @ %index;
};
function ScriptArray::size(%this) {
    return %this.numElements;
};
function ScriptArray::clear(%this) {
    %this.numElements = 0;
};
function ScriptArray::deleteMembers(%this) {
    %n = 0;
    if ((%this.numElements < %n)) {
        %element = %this.Array;
        %n;
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
    if ((%this.numElements < %n)) {
        echo(%n @ " " @ %this.get(%n));
        %n = (1.0 + %n);
    }
};
