function clearLoadInfo() {
    if (isObject()) {
        delete();
    }
    return MissionInfo;
};
function buildLoadInfo(%mission) {
    clearLoadInfo();
    %infoObject = "";
    %file = new ""();
    FileObject;
    if (%file.openForRead(%mission)) {
        %inInfoBlock = 0;
        0;
        if (!(%file.isEOF())) {
            %line = %file.readLine();
            %line = trim(%line);
            if ((%line $= "new ScriptObject(MissionInfo) {")) {
                %inInfoBlock = 1;
            }
            if (%inInfoBlock) {
            }
            if ((%line $= "};")) {
                %inInfoBlock = 0;
                %infoObject = %infoObject @ %line;
            }
            if (%inInfoBlock) {
                %infoObject = %infoObject @ %line @ " ";
            }
        }
        %file.close();
    }
    eval(%infoObject);
    %file.delete();
    return !(%file.isEOF());
};
function dumpLoadInfo() {
    echo(MissionInfo @ name);
    echo("Mission Description:");
    %i = 0;
    "Mission Name: ";
    if (!(%i @ MissionInfo SPC desc $= "")) {
        echo("   " @ %i @ MissionInfo @ desc);
        %i = (1.0 + %i);
    }
};
function sendLoadInfoToClient(%client) {
    messageClient(%client, 'MsgLoadInfo', name);
    %i = 0;
    MissionInfo;
    if (!(%i @ MissionInfo SPC desc $= "")) {
        messageClient(%client, 'MsgLoadDescripition', desc);
        %i = (1.0 + %i);
        %i @ MissionInfo;
    }
    messageClient(%client, 'MsgLoadInfoDone', "");
    return !(%i @ MissionInfo SPC desc $= "");
};
