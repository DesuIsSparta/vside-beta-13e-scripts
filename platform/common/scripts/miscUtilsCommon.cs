function SimSet::sortByInternalName(%this, %recurse)
{
    %chilluns = "";
    %delim = "";
    %n = %this.getCount() - 1;
    while (%n >= 0)
    {
        %obj = %this.getObject(%n);
        %chilluns = %chilluns @ %delim;
        %chilluns = %chilluns @ %obj.getInternalName() TAB %obj;
        %delim = "\n";
        %n = %n - 1;
    }
    %chilluns = SortRecords(%chilluns);
    %n = getRecordCount(%chilluns) - 1;
    while (%n >= 0)
    {
        %obj = getField(getRecord(%chilluns, %n), 1);
        %this.bringToFront(%obj);
        %n = %n - 1;
    }
    if (%recurse)
    {
        %n = %this.getCount() - 1;
        while (%n >= 0)
        {
            %obj = %this.getObject(%n);
            if (%obj.isClassSimSet())
            {
                %obj.sortByInternalName(1);
            }
            %n = %n - 1;
        }
    }
}

function echoDebug(%line)
{
    log("general", "debug", %line);
    return ;
}
function echoWarn(%line)
{
    log("general", "warn", %line);
    return ;
}
function echoError(%line)
{
    log("general", "error", %line);
    return ;
}
function FileObject::indent(%this)
{
    if (%this.indentString $= "")
    {
        %this.indentString = "   ";
    }
    %this.indent = %this.indent @ %this.indentString;
    return ;
}
function FileObject::unindent(%this)
{
    if (%this.indentString $= "")
    {
        %this.indentString = "   ";
    }
    %this.indent = getSubStr(%this.indent, strlen(%this.indentString), -1);
    return ;
}
function FileObject::writeLineIndented(%this, %line)
{
    %line = %this.indent @ %line;
    %line = strreplace(%line, "\n", "\n" @ %this.indent @ %this.indent);
    %this.writeLine(%line);
    return ;
}
function FileObject::writeOpenTag(%this, %tagName, %tagValues)
{
    %this.writeLineIndented("<" @ %tagName @ %tagValues $= "" ? "" : " " @ %tagValues @ ">");
    %this.indent();
    return ;
}
function FileObject::writeCloseTag(%this, %tagName)
{
    %this.unindent();
    %this.writeLineIndented("</" @ %tagName @ ">");
    return ;
}
function FileObject::writeShortTag(%this, %tagName, %tagValues, %tagContent)
{
    %line = "<" @ %tagName @ %tagValues $= "" ? "" : " " @ %tagValues @ ">";
    %line = %line @ %tagContent;
    %line = %line @ "</" @ %tagName @ ">";
    %this.writeLineIndented(%line);
    return ;
}
function FileObject::writeCommentTag(%this, %value)
{
    %line = "<!--" SPC %value SPC "-->";
    %this.writeLineIndented(%line);
    return ;
}
function SimObject::dumpParentContainers(%this)
{
    %this._dumpParentContainersRecursive(%this, 0);
    return ;
}
function SimObject::_dumpParentContainersRecursive(%this, %depth)
{
    echo(getDebugString(%this));
    %container = %this.getGroup();
    if (isObject(%container))
    {
        %container._dumpParentContainersRecursive(%depth + 1);
    }
    return ;
}
