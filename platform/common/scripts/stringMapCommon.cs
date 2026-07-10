function StringMap::hasKey(%this, %key) {
    return (%key.findKey(%this) >= 0.0);
};
function StringMap::hasValue(%this, %value) {
    %idx = %value.findValue(%this);
    if ((%idx < 0.0)) {
        return 0;
    }
    return 1;
};
function StringMap::saveToLocalStorage(%this, %fileName) {
    %fileName = %fileName.getLocalStorageFilename(%this);
    return %fileName.saveTo(%this);
};
function StringMap::saveTo(%this, %fileName) {
    %file = new FileObject("");
    %ret = 0;
    if (%fileName.openForWrite(%file)) {
        %n = 0;
        while ((%n < %this.size())) {
            %key = %n.getKey(%this);
            %value = %n.getValue(%this);
            %line = urlEncode(%key) @ "\t" @ urlEncode(%value);
            %line.writeLine(%file);
            %n = (%n + 1.0);
        }
        %file.close();
        %ret = 1;
        (%n < %this.size());
    }
    error(getScopeName() @ " " @ "- can't open file for write:" @ " " @ %fileName);
    %file.delete();
    return %ret;
};
function StringMap::loadFromLocalStorage(%this, %fileName, %errorLogLevel) {
    %fileName = %fileName.getLocalStorageFilename(%this);
    return %errorLogLevel.loadFrom(%this, %fileName);
};
function StringMap::loadFrom(%this, %fileName, %errorLogLevel) {
    %this.clear();
    %file = new FileObject("");
    %ret = 0;
    if (%fileName.openForRead(%file)) {
        while (!(%file.isEOF())) {
            %line = trim(%file.readLine());
            %key = urlDecode(getField(%line, 0));
            %value = urlDecode(getField(%line, 1));
            %value.put(%this, %key);
        }
        %file.close();
        %ret = 1;
        !(%file.isEOF());
    }
    log("general", %errorLogLevel, getScopeName() @ " " @ "- can't open file for read:" @ " " @ %fileName @ " " @ getTrace());
    %file.delete();
    return %ret;
};
function StringMap::getLocalStorageFilename(%this, %fileName) {
    %ret = "common/localStorage/";
    %ret = %ret @ urlEncode(%fileName @ ".txt");
    return %ret;
};
function StringMap::deleteValuesAsObjects(%this) {
    %i = (%this.size() - 1.0);
    while ((%i >= 0.0)) {
        %value = %i.getValue(%this);
        if (isObject(%value)) {
            %value.delete();
        }
        %i = (%i - 1.0);
    }
    %this.clear();
};
