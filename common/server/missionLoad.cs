$MissionLoadPause = 5000;
function loadMission(%missionName, %isFirstMission)
{
    endMission();
    echo("*** LOADING MISSION: " @ %missionName);
    echo("*** Stage 1 load");
    clearCenterPrintAll();
    clearBottomPrintAll();
    $MissionSequence = $MissionSequence + 1;
    $missionRunning = 0;
    $Server::MissionFile = %missionName;
    buildLoadInfo(%missionName);
    %count = ClientGroup.getCount();
    %cl = 0;
    while (%cl < %count)
    {
        %client = ClientGroup.getObject(%cl);
        if (!%client.isAIControlled())
        {
            sendLoadInfoToClient(%client);
        }
        %cl = %cl + 1;
    }
    if (%isFirstMission && ($Server::ServerType $= "SinglePlayer"))
    {
        loadMissionStage2();
    }
    else
    {
        schedule($MissionLoadPause, ServerGroup, loadMissionStage2);
    }
    return ;
}
function loadMissionStage2()
{
    echo("*** Stage 2 load");
    $instantGroup = ServerGroup;
    %file = $Server::MissionFile;
    %ofile = %file;
    if (!(strchr(%file, "\\") $= ""))
    {
        %file = strreplace(%file, "\\", "/");
    }
    if (!isFile(%file))
    {
        error("initialization", "Mission file could not be found:" SPC %ofile);
        if (!$StandAlone)
        {
            quit();
        }
        return ;
    }
    $missionCRC = 0;
    new SimGroup(MissionCleanup);
    exec(%file);
    if (!isObject(MissionGroup))
    {
        error("No \'MissionGroup\' found in mission \"" @ $missionName @ "\".");
        schedule(3000, ServerGroup, CycleMissions);
        return ;
    }
    $instantGroup = MissionCleanup;
    pathOnMissionLoadDone();
    echo("*** Mission loaded");
    $missionRunning = 1;
    %clientIndex = 0;
    while (%clientIndex < ClientGroup.getCount())
    {
        ClientGroup.getObject(%clientIndex).loadMission();
        %clientIndex = %clientIndex + 1;
    }
    onMissionLoaded();
    purgeResources();
    return ;
}
function endMission()
{
    if (!isObject(MissionGroup))
    {
        return ;
    }
    echo("*** ENDING MISSION");
    onMissionEnded();
    %clientIndex = 0;
    while (%clientIndex < ClientGroup.getCount())
    {
        %cl = ClientGroup.getObject(%clientIndex);
        %cl.endMission();
        %cl.resetGhosting();
        %cl.clearPaths();
        %clientIndex = %clientIndex + 1;
    }
    MissionGroup.delete();
    MissionCleanup.delete();
    $ServerGroup.delete();
    $ServerGroup = new SimGroup(ServerGroup);
    return ;
}
function resetMission()
{
    echo("*** MISSION RESET");
    MissionCleanup.delete();
    $instantGroup = ServerGroup;
    new SimGroup(MissionCleanup);
    $instantGroup = MissionCleanup;
    onMissionReset();
    return ;
}
