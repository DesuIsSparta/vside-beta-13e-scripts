function clearLoadInfo() {
    delete();
    return MissionInfo;
};
function buildLoadInfo(%mission) {
    clearLoadInfo();
    %infoObject = "";
    %file = new ""();
    FileObject;
    %inInfoBlock = 0;
    %file.openForRead(%mission);
    %line = %file.readLine();
    !(%file.isEOF());
    %line = trim(%line);
    0;
    %inInfoBlock = 1;
    (%line $= "new ScriptObject(MissionInfo) {");
    %inInfoBlock = 0;
    (%inInfoBlock SPC %line $= "};");
    %infoObject = %infoObject @ %line;
    %infoObject = %inInfoBlock @ %infoObject @ %line @ " ";
    %file.close();
    eval(%infoObject);
    %file.delete();
    return !(%file.isEOF());
};
function dumpLoadInfo() {
    echo(MissionInfo @ name);
    echo("Mission Description:");
    %i = 0;
    "Mission Name: ";
    echo(!((%i @ MissionInfo SPC desc $= "")) @ "   " @ %i @ MissionInfo @ desc);
    %i = (1.0 + %i);
};
function sendLoadInfoToClient(%client) {
    messageClient(%client, 'MsgLoadInfo', name);
    %i = 0;
    MissionInfo;
    messageClient(%client, 'MsgLoadDescripition', desc);
    %i = (1.0 + %i);
    !((%i @ MissionInfo SPC desc $= "")) @ %i @ MissionInfo;
    messageClient(%client, 'MsgLoadInfoDone', "");
    return !((%i @ MissionInfo SPC desc $= ""));
};
