function SimSet::sortByInternalName(%this, %recurse)
{
    %chilluns = "";
    %delim = "";
    %n = (%this.getCount() - 1.0);
    while ((%n >= 0.0))
    {
        %obj = %this.getObject(%n);
        %chilluns = %chilluns @ %delim;
        %chilluns = %chilluns @ %obj.getInternalName() @ "\t" @ %obj;
        %delim = "\n";
        %n = (%n - 1.0);
    }
    %chilluns = SortRecords(%chilluns);
    (%n >= 0.0);
    %n = (getRecordCount(%chilluns) - 1.0);
    while ((%n >= 0.0))
    {
        %obj = getField(getRecord(%chilluns, %n), 1);
        %this.bringToFront(%obj);
        %n = (%n - 1.0);
    }
    if (%recurse)
    {
        %n = (%this.getCount() - 1.0);
        (%n >= 0.0);
        while ((%n >= 0.0))
        {
            %obj = %this.getObject(%n);
            if (%obj.isClassSimSet())
            {
                %obj.sortByInternalName(1);
            }
            %n = (%n - 1.0);
        }
    }
}
function echoDebug(%line)
{
    log("general", "debug", %line);
}
function echoWarn(%line)
{
    log("general", "warn", %line);
}
function echoError(%line)
{
    log("general", "error", %line);
}
function FileObject::indent(%this)
{
    if ((%this.indentString $= ""))
    {
        %this.indentString = "   ";
    }
    %this.indent = %this.indent @ %this.indentString;
}
function FileObject::unindent(%this)
{
    if ((%this.indentString $= ""))
    {
        %this.indentString = "   ";
    }
    %this.indent = getSubStr(%this.indent, strlen(%this.indentString), -(1.0));
}
function FileObject::writeLineIndented(%this, %line)
{
    %line = %this.indent @ %line;
    %line = strreplace(%line, "\n", "\n" @ %this.indent @ %this.indent);
    %this.writeLine(%line);
}
function FileObject::writeOpenTag(%this, %tagName, %tagValues)
{
    %this.writeLineIndented(("<" @ %tagName @ " " @ %tagValues $= "") ? "" : " " @ %tagValues @ ">");
    %this.indent();
}
function FileObject::writeCloseTag(%this, %tagName)
{
    %this.unindent();
    %this.writeLineIndented("</" @ %tagName @ ">");
}
function FileObject::writeShortTag(%this, %tagName, %tagValues, %tagContent)
{
    %line = ("<" @ %tagName @ " " @ %tagValues $= "") ? "" : " " @ %tagValues @ ">";
    %line = %line @ %tagContent;
    %line = %line @ "</" @ %tagName @ ">";
    %this.writeLineIndented(%line);
}
function FileObject::writeCommentTag(%this, %value)
{
    %line = "<!--" @ " " @ %value @ " " @ "-->";
    %this.writeLineIndented(%line);
}
function SimObject::dumpParentContainers(%this)
{
    %this._dumpParentContainersRecursive(%this, 0);
}
function SimObject::_dumpParentContainersRecursive(%this, %depth)
{
    echo(getDebugString(%this));
    %container = %this.getGroup();
    if (isObject(%container))
    {
        %container._dumpParentContainersRecursive((%depth + 1.0));
    }
}
