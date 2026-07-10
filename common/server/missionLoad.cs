$MissionLoadPause = 5000;
function loadMission(%missionName, %isFirstMission) {
    endMission();
    echo("*** LOADING MISSION: " @ %missionName);
    echo("*** Stage 1 load");
    clearCenterPrintAll();
    clearBottomPrintAll();
    $MissionSequence = ($MissionSequence + 1.0);
    $missionRunning = 0;
    $Server::MissionFile = %missionName;
    buildLoadInfo(%missionName);
    %count = ClientGroup.getCount();
    %cl = 0;
    while ((%cl < %count)) {
        %client = %cl.getObject(ClientGroup);
        if (!(%client.isAIControlled())) {
            sendLoadInfoToClient(%client);
        }
        %cl = (%cl + 1.0);
    }
    if (%isFirstMission) {
    }
    if (((%cl < %count) @ " " @ $Server::ServerType $= "SinglePlayer")) {
        loadMissionStage2();
    }
    schedule($MissionLoadPause);
    return loadMissionStage2;
};
function loadMissionStage2() {
    echo("*** Stage 2 load");
    // unhandled opcode 329 at 0x000000E9
    %file = $Server::MissionFile;
    %ofile = %file;
    if (!(strchr(%file, "\\") $= "")) {
        %file = strreplace(%file, "\\", "/");
    }
    if (!(isFile(%file))) {
        error("initialization", "Mission file could not be found:" @ " " @ %ofile);
        if (!($StandAlone)) {
            quit();
        }
        return;
    }
    $missionCRC = 0;
    new SimGroup(MissionCleanup);
    exec(%file);
    if (!(isObject(MissionGroup))) {
        error("No 'MissionGroup' found in mission \"" @ $missionName @ "\".");
        schedule(3000);
        return CycleMissions;
    }
    // unhandled opcode 329 at 0x0000019F
    pathOnMissionLoadDone();
    echo("*** Mission loaded");
    $missionRunning = 1;
    %clientIndex = 0;
    while ((%clientIndex < ClientGroup.getCount())) {
        %clientIndex.getObject(ClientGroup).loadMission();
        %clientIndex = (%clientIndex + 1.0);
    }
    onMissionLoaded();
    purgeResources();
    return (%clientIndex < ClientGroup.getCount());
};
function endMission() {
    if (!(isObject(MissionGroup))) {
        return;
    }
    echo("*** ENDING MISSION");
    onMissionEnded();
    %clientIndex = 0;
    while ((%clientIndex < ClientGroup.getCount())) {
        %cl = %clientIndex.getObject(ClientGroup);
        %cl.endMission();
        %cl.resetGhosting();
        %cl.clearPaths();
        %clientIndex = (%clientIndex + 1.0);
    }
    MissionGroup.delete();
    MissionCleanup.delete();
    $ServerGroup.delete();
    $ServerGroup = new SimGroup(ServerGroup);;
    (%clientIndex < ClientGroup.getCount());
    return;
};
function resetMission() {
    echo("*** MISSION RESET");
    MissionCleanup.delete();
    // unhandled opcode 329 at 0x000002D4
    new SimGroup(MissionCleanup);
    // unhandled opcode 329 at 0x000002EC
    onMissionReset();
    return;
};
