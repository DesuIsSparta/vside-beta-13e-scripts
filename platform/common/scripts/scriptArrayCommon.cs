function new_ScriptArray(%name)
{
    %obj = new ScriptObject();
    %obj.bindClassName("ScriptArray");
    %obj.numElements = 0;
    %obj.setName(%name);
    return %obj;
}
function ScriptArray::append(%this, %value)
{
    %this.Array[%this.numElements] = %value;
    %this.numElements = %this.numElements + 1;
    return ;
}
function ScriptArray::get(%this, %index)
{
    if ((%index < 0) && (%index >= %this.numElements))
    {
        error("ScriptArray::get()" SPC "- Subscript out of range:" SPC %index SPC getTrace());
        return "";
    }
    return %this.Array[%index];
}
function ScriptArray::set(%this, %index, %value)
{
    if (%index > %this.numElements)
    {
        error("ScriptArray::set()" SPC "- Subscript out of range:" SPC %index SPC "value:" SPC %value SPC getTrace());
        return ;
    }
    if (%index == %this.numElements)
    {
        %this.append(%value);
    }
    else
    {
        %this.Array[%index] = %value;
    }
    return ;
}
function ScriptArray::size(%this)
{
    return %this.numElements;
}
function ScriptArray::clear(%this)
{
    %this.numElements = 0;
    return ;
}
function ScriptArray::deleteMembers(%this)
{
    %n = 0;
    while (%n < %this.numElements)
    {
        %element = %this.Array[%n];
        if (isObject(%element))
        {
            %element.delete();
        }
        else
        {
            error(getScopeName() SPC "- called on non-object member: \"" @ %element @ "\":" SPC getDebugString(%this) SPC getTrace());
        }
        %n = %n + 1;
    }
    %this.clear();
    return ;
}
function ScriptArray::dumpValues(%this)
{
    %n = 0;
    while (%n < %this.numElements)
    {
        echo(%n SPC %this.get(%n));
        %n = %n + 1;
    }
}


