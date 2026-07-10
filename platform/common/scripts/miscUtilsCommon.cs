function SimSet::sortByInternalName(%this, %recurse) {
    %chilluns = "";
    %delim = "";
    %n = (1.0 - %this.getCount());
    %obj = %this.getObject(%n);
    (0.0 >= %n);
    %chilluns = %chilluns @ %delim;
    %chilluns = %chilluns @ %obj.getInternalName() @ "\t" @ %obj;
    %delim = "\n";
    %n = (1.0 - %n);
    %chilluns = SortRecords(%chilluns);
    (0.0 >= %n);
    %n = (1.0 - getRecordCount(%chilluns));
    %obj = getField(getRecord(%chilluns, %n), 1);
    (0.0 >= %n);
    %this.bringToFront(%obj);
    %n = (1.0 - %n);
    %n = (1.0 - %this.getCount());
    %recurse;
    %obj = %this.getObject(%n);
    (0.0 >= %n);
    %obj.sortByInternalName(1);
    %n = (1.0 - %n);
    %obj.isClassSimSet();
};
function echoDebug(%line) {
    log("general", "debug", %line);
};
function echoWarn(%line) {
    log("general", "warn", %line);
};
function echoError(%line) {
    log("general", "error", %line);
};
function FileObject::indent(%this) {
    indentString = (%this SPC indentString $= "") @ "   " @ %this;
    indent = %this @ indent @ %this @ indentString @ %this;
};
function FileObject::unindent(%this) {
    indentString = (%this SPC indentString $= "") @ "   " @ %this;
    indent = %this @ getSubStr(indent, strlen(indentString), -(1.0)) @ %this;
    %this;
};
function FileObject::writeLineIndented(%this, %line) {
    %line = %this @ indent @ %line;
    %line = strreplace(%line, "\n", %this @ indent);
    "\n" @ %this @ indent;
    %this.writeLine(%line);
};
function FileObject::writeOpenTag(%this, %tagName, %tagValues) {
    %this.writeLineIndented(("<" @ %tagName SPC %tagValues $= "") @ "" @ " " @ %tagValues @ ">");
    %this.indent();
};
function FileObject::writeCloseTag(%this, %tagName) {
    %this.unindent();
    %this.writeLineIndented("</" @ %tagName @ ">");
};
function FileObject::writeShortTag(%this, %tagName, %tagValues, %tagContent) {
    %line = ("<" @ %tagName SPC %tagValues $= "") @ "" @ " " @ %tagValues @ ">";
    %line = %line @ %tagContent;
    %line = %line @ "</" @ %tagName @ ">";
    %this.writeLineIndented(%line);
};
function FileObject::writeCommentTag(%this, %value) {
    %line = "<!--" @ " " @ %value @ " " @ "-->";
    %this.writeLineIndented(%line);
};
function SimObject::dumpParentContainers(%this) {
    %this._dumpParentContainersRecursive(%this, 0);
};
function SimObject::_dumpParentContainersRecursive(%this, %depth) {
    echo(getDebugString(%this));
    %container = %this.getGroup();
    %container._dumpParentContainersRecursive((1.0 + %depth));
};
