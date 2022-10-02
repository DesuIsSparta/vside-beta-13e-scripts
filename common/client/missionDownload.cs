$Client::DatablockCRC = 0;
$Cache::ExtraNameTag = "";
function clientCmdCheckCacheCRC(%missionSequence, %missionName)
{
    log("network", "info", "check client cache CRC:" SPC %missionName SPC "seq:" SPC %missionSequence SPC "gender:" SPC $UserPref::Player::gender);
    if (!isObject(ServerConnection))
    {
        log("network", "warn", "ServerConnection not valid in clientCmdMissionCheckCacheCRC");
    }
    $GeneratingCacheNow = 0;
    $CurrentMission = %missionName;
    if ($CacheFlagIsSet)
    {
        %crc = ServerConnection.getCacheCRC(%missionName);
    }
    else
    {
        %crc = -1;
    }
    log("network", "debug", "client cache CRC:" SPC %crc);
    %hasStandaloneCache = ($CacheFlagIsSet && $StandAlone) && (%crc != -1);
    $Client::TempMissionFile = %missionName;
    prepLighting();
    commandToServer('MissionCRC', %missionSequence, %missionName, %crc, $UserPref::Player::gender, %hasStandaloneCache);
    return ;
}
function clientCmdStartCache(%missionSequence, %missionName, %musicTrack)
{
    if (!$CacheFlagIsSet)
    {
        log("network", "debug", "cache turned off, acking server");
        commandToServer('StartCacheAck', %missionSequence);
        return ;
    }
    log("network", "info", "attempting client side load caching:" SPC %missionName SPC "seq:" SPC %missionSequence);
    onMissionDownloadPhase1(%missionName, %musicTrack);
    $GeneratingCacheNow = 1;
    %success = ServerConnection.startCache(%missionName);
    if (%success)
    {
        log("network", "info", "cache writing started successfully");
    }
    else
    {
        log("network", "error", "failed to open cache file for write: " @ %missionName);
    }
    commandToServer('StartCacheAck', %missionSequence);
    return ;
}
function clientCmdLoadLocalCache(%missionSequence, %missionName, %musicTrack)
{
    if (!isObject(ServerConnection))
    {
        log("network", "warn", "ServerConnection not valid in clientCmdMissionLoadLocalDatablocks");
    }
    log("network", "info", "loading local datablocks for mission:" SPC %missionName SPC "seq: " SPC %missionSequence);
    onMissionDownloadPhase1(%missionName, %musicTrack);
    ServerConnection.setDatablockSequence(%missionSequence);
    ServerConnection.loadCachePhase1(%missionSequence, %missionName);
    return ;
}
function onDataBlockObjectReceived(%index, %total)
{
    onPhase1Progress(%index / %total);
    return ;
}
function clientCmdStartGhostAlways(%missionSequence, %missionName)
{
    onPhase1Complete();
    log("network", "info", "phase 2" SPC %missionName SPC "seq:" SPC %missionSequence);
    purgeResources();
    onMissionDownloadPhase2(%missionName);
    echo("Starting texture downloads...");
    textureDownloadAllowDownloads(1);
    textureDownloadProcess();
    if ($CacheFlagIsSet && !$GeneratingCacheNow)
    {
        ServerConnection.loadCachePhase2(%missionSequence, %missionName);
    }
    else
    {
        log("network", "debug", "not using cache, acking server to start ghost always phase");
        commandToServer('StartGhostAlwaysAck', %missionSequence);
    }
    return ;
}
function onCachePhase2Started(%missionSequence, %ghostCount)
{
    log("network", "info", "loading cache phase2 started with" SPC %ghostCount SPC "ghosts in the cache");
    $GhostCount = %ghostCount;
    $GhostsRecvd = 0;
    return ;
}
function onCachePhase2Done(%missionSequence)
{
    log("network", "info", getScopeName() SPC "called.");
    log("network", "debug", "acking server to start ghost always phase");
    commandToServer('StartGhostAlwaysAck', %missionSequence);
    return ;
}
function onGhostAlwaysStarted(%ghostCount)
{
    echo("onGhostAlwaysStarted: " @ %ghostCount);
    $GhostCount = %ghostCount;
    if ($CacheFlagIsSet && !$GeneratingCacheNow)
    {
    }
    else
    {
        $GhostsRecvd = 0;
    }
    return ;
}
function onGhostAlwaysObjectReceived()
{
    $GhostsRecvd = $GhostsRecvd + 1;
    onPhase2ProgressUpdateStatusDisplay($GhostsRecvd / $GhostCount);
    return ;
}
function onGhostAlwaysDone()
{
    log("network", "debug", "ghost always done");
    if ($CacheFlagIsSet)
    {
        if ($GeneratingCacheNow)
        {
            $Client::DatablockCRC = ServerConnection.stopCache();
            log("network", "debug", "ghost always done, computed CRC:" SPC $Client::DatablockCRC);
        }
    }
    return ;
}
function clientCmdMissionStartPhase3(%missionSequence, %missionName)
{
    log("network", "debug", "clientCmdMissionStartPhase3:" SPC %missionName SPC "seq: " SPC %missionSequence);
    onPhase2Complete();
    StartClientReplication();
    StartFoliageReplication();
    purgeResources();
    log("network", "info", "phase 3" SPC %missionName);
    log("general", "info", "phase_3_memory=" @ getCurrentMemoryUsage() / 1024);
    $MSeq = %missionSequence;
    $Client::MissionFile = %missionName;
    if ($NoDisplay)
    {
        log("initialization", "debug", "$NoDisplay set, not lighting scene");
        sceneLightingComplete();
    }
    else
    {
        if (lightScene("sceneLightingComplete", ""))
        {
            log("initialization", "info", "Lighting mission...");
            schedule(1, 0, "updateLightingProgress");
            onMissionDownloadPhase3(%missionName);
            $lightingMission = 1;
        }
    }
    return ;
}
function updateLightingProgress()
{
    onPhase3Progress($SceneLighting::lightingProgress);
    if ($lightingMission)
    {
        $lightingProgressThread = schedule(500, 0, "updateLightingProgress");
    }
    return ;
}
function sceneLightingComplete()
{
    log("network", "info", "scene lighting complete");
    log("general", "info", "lighting_complete_memory=" @ getCurrentMemoryUsage() / 1024);
    onPhase3Complete();
    onMissionDownloadComplete();
    commandToServer('MissionStartPhase3Ack', $MSeq);
    $GeneratingCacheNow = 0;
    return ;
}
function connect(%server)
{
    %conn = new GameConnection();
    %conn.setCommonPreconnectClientSettings("");
    %conn.connect(%server);
    return ;
}
