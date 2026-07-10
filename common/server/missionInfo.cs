function clearLoadInfo() {
    if (isObject(MissionInfo)) {
        MissionInfo.delete();
    }
    return;
};
function buildLoadInfo(%mission) {
    clearLoadInfo();
    %infoObject = "";
    %file = new FileObject("");;
    0;
    if (%mission.openForRead(%file)) {
        %inInfoBlock = 0;
        while (!(%file.isEOF())) {
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
    echo("Mission Name: ", MissionInfo @ name);
    echo("Mission Description:");
    %i = 0;
    while (!(%i @ MissionInfo @ " " @ desc $= "")) {
        echo("   ", %i @ MissionInfo @ desc);
        %i = (%i + 1.0);
    }
};
function sendLoadInfoToClient(%client) {
    messageClient(%client, 'MsgLoadInfo', MissionInfo, name);
    %i = 0;
    while (!(%i @ MissionInfo @ " " @ desc $= "")) {
        messageClient(%client, 'MsgLoadDescripition', %i @ MissionInfo, desc);
        %i = (%i + 1.0);
    }
    messageClient(%client, 'MsgLoadInfoDone', "");
    return !(%i @ MissionInfo @ " " @ desc $= "");
};
