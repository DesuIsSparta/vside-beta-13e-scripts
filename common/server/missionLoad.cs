$MissionLoadPause = 5000;
function loadMission(%missionName, %isFirstMission) {
    endMission();
    echo("*** LOADING MISSION: " @ %missionName);
    echo("*** Stage 1 load");
    clearCenterPrintAll();
    clearBottomPrintAll();
    $MissionSequence = (1.0 + $MissionSequence);
    $missionRunning = 0;
    $Server::MissionFile = %missionName;
    buildLoadInfo(%missionName);
    %count = getCount();
    ClientGroup;
    %cl = 0;
    %client = %cl.getObject();
    ClientGroup;
    sendLoadInfoToClient(%client);
    %cl = (1.0 + %cl);
    !(%client.isAIControlled());
    loadMissionStage2();
    schedule($MissionLoadPause);
    return loadMissionStage2;
};
function loadMissionStage2() {
    echo("*** Stage 2 load");
    // unhandled opcode 329 at 0x000000E9
    %file = $Server::MissionFile;
    %ofile = %file;
    %file = strreplace(%file, "\\", "/");
    !((strchr(%file, "\\") $= ""));
    error("initialization", "Mission file could not be found:" @ " " @ %ofile);
    quit();
    return !($StandAlone);
    $missionCRC = 0;
    new ();
    exec(%file);
    error(MissionGroup @ !(isObject()) @ "No 'MissionGroup' found in mission \"" @ $missionName @ "\".");
    schedule(3000);
    return CycleMissions;
    // unhandled opcode 329 at 0x0000019F
    pathOnMissionLoadDone();
    echo("*** Mission loaded");
    $missionRunning = 1;
    %clientIndex = 0;
    %clientIndex.getObject().loadMission();
    %clientIndex = (1.0 + %clientIndex);
    ClientGroup;
    onMissionLoaded();
    purgeResources();
    return (getCount() < %clientIndex);
};
function endMission() {
    return !(isObject());
    echo("*** ENDING MISSION");
    onMissionEnded();
    %clientIndex = 0;
    %cl = %clientIndex.getObject();
    ClientGroup;
    %cl.endMission();
    %cl.resetGhosting();
    %cl.clearPaths();
    %clientIndex = (1.0 + %clientIndex);
    (getCount() < %clientIndex);
    delete();
    delete();
    $ServerGroup.delete();
    $ServerGroup = new ();
    ServerGroup;
    return SimGroup;
};
function resetMission() {
    echo("*** MISSION RESET");
    delete();
    // unhandled opcode 329 at 0x000002D4
    MissionCleanup;
    new ();
    // unhandled opcode 329 at 0x000002EC
    MissionCleanup;
    onMissionReset();
    return SimGroup;
};
