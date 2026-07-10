function new_ScriptArray(%name) {
    %obj = new ScriptObject("");
    "ScriptArray".bindClassName(%obj);
    %obj.numElements = 0;
    %name.setName(%obj);
    return %obj;
};
function ScriptArray::append(%this, %value) {
    %this.Array = %value @ %this.numElements;
    %this.numElements = (%this.numElements + 1.0);
};
function ScriptArray::get(%this, %index) {
    if ((%index < 0.0)) {
    }
    if ((%index >= %this.numElements)) {
        error("ScriptArray::get()" @ " " @ "- Subscript out of range:" @ " " @ %index @ " " @ getTrace());
        return "";
    }
    return %this.Array;
};
function ScriptArray::set(%this, %index, %value) {
    if ((%index > %this.numElements)) {
        error("ScriptArray::set()" @ " " @ "- Subscript out of range:" @ " " @ %index @ " " @ "value:" @ " " @ %value @ " " @ getTrace());
        return;
    }
    if ((%index == %this.numElements)) {
        %value.append(%this);
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
    while ((%n < %this.numElements)) {
        %element = %this.Array;
        %n;
        if (isObject(%element)) {
            %element.delete();
        }
        error(getScopeName() @ " " @ "- called on non-object member: \"" @ %element @ "\":" @ " " @ getDebugString(%this) @ " " @ getTrace());
        %n = (%n + 1.0);
    }
    %this.clear();
};
function ScriptArray::dumpValues(%this) {
    %n = 0;
    while ((%n < %this.numElements)) {
        echo(%n @ " " @ %n.get(%this));
        %n = (%n + 1.0);
    }
};
