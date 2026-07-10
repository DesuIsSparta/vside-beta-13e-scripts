function StringMap::hasKey(%this, %key) {
    return (0.0 >= %this.findKey(%key));
};
function StringMap::hasValue(%this, %value) {
    %idx = %this.findValue(%value);
    if ((0.0 < %idx)) {
        return 0;
    }
    return 1;
};
function StringMap::saveToLocalStorage(%this, %fileName) {
    %fileName = %this.getLocalStorageFilename(%fileName);
    return %this.saveTo(%fileName);
};
function StringMap::saveTo(%this, %fileName) {
    %file = new FileObject("");;
    0;
    %ret = 0;
    if (%file.openForWrite(%fileName)) {
        %n = 0;
        if ((%this.size() < %n)) {
            %key = %this.getKey(%n);
            %value = %this.getValue(%n);
            %line = urlEncode(%key) @ "\t" @ urlEncode(%value);
            %file.writeLine(%line);
            %n = (1.0 + %n);
        }
        %file.close();
        %ret = 1;
        (%this.size() < %n);
    }
    error(getScopeName() @ " " @ "- can't open file for write:" @ " " @ %fileName);
    %file.delete();
    return %ret;
};
function StringMap::loadFromLocalStorage(%this, %fileName, %errorLogLevel) {
    %fileName = %this.getLocalStorageFilename(%fileName);
    return %this.loadFrom(%fileName, %errorLogLevel);
};
function StringMap::loadFrom(%this, %fileName, %errorLogLevel) {
    %this.clear();
    %file = new FileObject("");;
    0;
    %ret = 0;
    if (%file.openForRead(%fileName)) {
        if (!(%file.isEOF())) {
            %line = trim(%file.readLine());
            %key = urlDecode(getField(%line, 0));
            %value = urlDecode(getField(%line, 1));
            %this.put(%key, %value);
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
    %i = (1.0 - %this.size());
    if ((0.0 >= %i)) {
        %value = %this.getValue(%i);
        if (isObject(%value)) {
            %value.delete();
        }
        %i = (1.0 - %i);
    }
    %this.clear();
};
