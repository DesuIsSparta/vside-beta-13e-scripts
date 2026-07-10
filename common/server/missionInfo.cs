function clearLoadInfo() {
    if (isObject(MissionInfo)) {
        MissionInfo.delete();
    }
    return;
};
function buildLoadInfo(%mission) {
    clearLoadInfo();
    %infoObject = "";
    %file = new FileObject("");
    if (%file.openForRead(%mission)) {
        %inInfoBlock = 0;
        if (!%file.isEOF()) {
            %line = %file.readLine();
            %line = trim(%line);
            if ((%line $= "new ScriptObject(MissionInfo) {")) {
                %inInfoBlock = 1;
            } else {
                if (%inInfoBlock) {
                }
                if ((%line $= "};")) {
                    %inInfoBlock = 0;
                    %infoObject = %infoObject @ %line;
                } else {
                    if (%inInfoBlock) {
                        %infoObject = %infoObject @ %line @ " ";
                    }
                }
            }
        }
        %file.close();
    }
    eval(%infoObject);
    %file.delete();
    return !%file.isEOF();
};
function dumpLoadInfo() {
    echo("Mission Name: " @ MissionInfo.name);
    echo("Mission Description:");
    %i = 0;
    while (!(MissionInfo.desc[%i] $= "")) {
        echo("   " @ MissionInfo.desc[%i]);
        %i = (%i + 1.0);
    }
};
function sendLoadInfoToClient(%client) {
    messageClient(%client, 'MsgLoadInfo', MissionInfo.name);
    %i = 0;
    while (!(MissionInfo.desc[%i] $= "")) {
        messageClient(%client, 'MsgLoadDescripition', MissionInfo.desc[%i]);
        %i = (%i + 1.0);
    }
    messageClient(%client, 'MsgLoadInfoDone', "");
    return !(MissionInfo.desc[%i] $= "");
};
