function StringMap::hasKey(%this, %key)
{
    return %this.findKey(%key) >= 0;
}
function StringMap::hasValue(%this, %value)
{
    %idx = %this.findValue(%value);
    if (%idx < 0)
    {
        return 0;
    }
    else
    {
        return 1;
    }
}
function StringMap::saveToLocalStorage(%this, %fileName)
{
    %fileName = %this.getLocalStorageFilename(%fileName);
    return %this.saveTo(%fileName);
}
function StringMap::saveTo(%this, %fileName)
{
    %file = new FileObject("");
    %ret = 0;
    if (%file.openForWrite(%fileName))
    {
        %n = 0;
        while (%n < %this.size())
        {
            %key = %this.getKey(%n);
            %value = %this.getValue(%n);
            %line = urlEncode(%key) @ "\t" @ urlEncode(%value);
            %file.writeLine(%line);
            %n = %n + 1;
        }
        %file.close();
        %ret = 1;
    }
    else
    {
        error(getScopeName() @ " " @ "- can't open file for write:" @ " " @ %fileName);
    }
    %file.delete();
    return %ret;
}
function StringMap::loadFromLocalStorage(%this, %fileName, %errorLogLevel)
{
    %fileName = %this.getLocalStorageFilename(%fileName);
    return %this.loadFrom(%fileName, %errorLogLevel);
}
function StringMap::loadFrom(%this, %fileName, %errorLogLevel)
{
    %this.clear();
    %file = new FileObject("");
    %ret = 0;
    if (%file.openForRead(%fileName))
    {
        while (!%file.isEOF())
        {
            %line = trim(%file.readLine());
            %key = urlDecode(getField(%line, 0));
            %value = urlDecode(getField(%line, 1));
            %this.put(%key, %value);
        }
        %file.close();
        %ret = 1;
    }
    else
    {
        log("general", %errorLogLevel, getScopeName() @ " " @ "- can't open file for read:" @ " " @ %fileName @ " " @ getTrace());
    }
    %file.delete();
    return %ret;
}
function StringMap::getLocalStorageFilename(%this, %fileName)
{
    %ret = "common/localStorage/";
    %ret = %ret @ urlEncode(%fileName @ ".txt");
    return %ret;
}
function StringMap::deleteValuesAsObjects(%this)
{
    %i = %this.size() - 1;
    while (%i >= 0)
    {
        %value = %this.getValue(%i);
        if (isObject(%value))
        {
            %value.delete();
        }
        %i = %i - 1;
    }
    %this.clear();
}
