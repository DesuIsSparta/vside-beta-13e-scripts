$Client::DatablockCRC = 0;
$Cache::ExtraNameTag = "";
function clientCmdCheckCacheCRC(%missionSequence, %missionName) {
    log("network", "info", "check client cache CRC:" @ " " @ %missionName @ " " @ "seq:" @ " " @ %missionSequence @ " " @ "gender:" @ " " @ $UserPref::Player::gender);
    if (!(isObject(ServerConnection))) {
        log("network", "warn", "ServerConnection not valid in clientCmdMissionCheckCacheCRC");
    }
    $GeneratingCacheNow = 0;
    $CurrentMission = %missionName;
    if ($CacheFlagIsSet) {
        %crc = ServerConnection.getCacheCRC(%missionName);
    }
    %crc = -(1.0);
    log("network", "debug", "client cache CRC:" @ " " @ %crc);
    if ($CacheFlagIsSet) {
    }
    if ($StandAlone) {
    }
    %hasStandaloneCache = (-(1.0) != %crc);
    $Client::TempMissionFile = %missionName;
    prepLighting();
    commandToServer('MissionCRC', %missionSequence, %missionName, %crc, $UserPref::Player::gender, %hasStandaloneCache);
};
function clientCmdStartCache(%missionSequence, %missionName, %musicTrack) {
    if (!($CacheFlagIsSet)) {
        log("network", "debug", "cache turned off, acking server");
        commandToServer('StartCacheAck', %missionSequence);
        return;
    }
    log("network", "info", "attempting client side load caching:" @ " " @ %missionName @ " " @ "seq:" @ " " @ %missionSequence);
    onMissionDownloadPhase1(%missionName, %musicTrack);
    $GeneratingCacheNow = 1;
    %success = ServerConnection.startCache(%missionName);
    if (%success) {
        log("network", "info", "cache writing started successfully");
    }
    log("network", "error", "failed to open cache file for write: " @ %missionName);
    commandToServer('StartCacheAck', %missionSequence);
};
function clientCmdLoadLocalCache(%missionSequence, %missionName, %musicTrack) {
    if (!(isObject(ServerConnection))) {
        log("network", "warn", "ServerConnection not valid in clientCmdMissionLoadLocalDatablocks");
    }
    log("network", "info", "loading local datablocks for mission:" @ " " @ %missionName @ " " @ "seq: " @ " " @ %missionSequence);
    onMissionDownloadPhase1(%missionName, %musicTrack);
    ServerConnection.setDatablockSequence(%missionSequence);
    ServerConnection.loadCachePhase1(%missionSequence, %missionName);
};
function onDataBlockObjectReceived(%index, %total) {
    onPhase1Progress((%total / %index));
};
function clientCmdStartGhostAlways(%missionSequence, %missionName) {
    onPhase1Complete();
    log("network", "info", "phase 2" @ " " @ %missionName @ " " @ "seq:" @ " " @ %missionSequence);
    purgeResources();
    onMissionDownloadPhase2(%missionName);
    echo("Starting texture downloads...");
    textureDownloadAllowDownloads(1);
    textureDownloadProcess();
    if ($CacheFlagIsSet) {
    }
    if (!($GeneratingCacheNow)) {
        ServerConnection.loadCachePhase2(%missionSequence, %missionName);
    }
    log("network", "debug", "not using cache, acking server to start ghost always phase");
    commandToServer('StartGhostAlwaysAck', %missionSequence);
};
function onCachePhase2Started(%missionSequence, %ghostCount) {
    log("network", "info", "loading cache phase2 started with" @ " " @ %ghostCount @ " " @ "ghosts in the cache");
    $GhostCount = %ghostCount;
    $GhostsRecvd = 0;
};
function onCachePhase2Done(%missionSequence) {
    log("network", "info", getScopeName() @ " " @ "called.");
    log("network", "debug", "acking server to start ghost always phase");
    commandToServer('StartGhostAlwaysAck', %missionSequence);
};
function onGhostAlwaysStarted(%ghostCount) {
    echo("onGhostAlwaysStarted: " @ %ghostCount);
    $GhostCount = %ghostCount;
    if ($CacheFlagIsSet) {
    }
    if (!($GeneratingCacheNow)) {
    }
    $GhostsRecvd = 0;
};
function onGhostAlwaysObjectReceived() {
    $GhostsRecvd = (1.0 + $GhostsRecvd);
    onPhase2ProgressUpdateStatusDisplay(($GhostCount / $GhostsRecvd));
};
function onGhostAlwaysDone() {
    log("network", "debug", "ghost always done");
    if ($CacheFlagIsSet) {
        if ($GeneratingCacheNow) {
            $Client::DatablockCRC = ServerConnection.stopCache();
            log("network", "debug", "ghost always done, computed CRC:" @ " " @ $Client::DatablockCRC);
        }
    }
};
function clientCmdMissionStartPhase3(%missionSequence, %missionName) {
    log("network", "debug", "clientCmdMissionStartPhase3:" @ " " @ %missionName @ " " @ "seq: " @ " " @ %missionSequence);
    onPhase2Complete();
    StartClientReplication();
    StartFoliageReplication();
    purgeResources();
    log("network", "info", "phase 3" @ " " @ %missionName);
    log("general", "info", "phase_3_memory=" @ (1024.0 / getCurrentMemoryUsage()));
    $MSeq = %missionSequence;
    $Client::MissionFile = %missionName;
    if ($NoDisplay) {
        log("initialization", "debug", "$NoDisplay set, not lighting scene");
        sceneLightingComplete();
    }
    if (lightScene("sceneLightingComplete", "")) {
        log("initialization", "info", "Lighting mission...");
        schedule(1, 0, "updateLightingProgress");
        onMissionDownloadPhase3(%missionName);
        $lightingMission = 1;
    }
};
function updateLightingProgress() {
    onPhase3Progress($SceneLighting::lightingProgress);
    if ($lightingMission) {
        $lightingProgressThread = schedule(500, 0, "updateLightingProgress");
    }
};
function sceneLightingComplete() {
    log("network", "info", "scene lighting complete");
    log("general", "info", "lighting_complete_memory=" @ (1024.0 / getCurrentMemoryUsage()));
    onPhase3Complete();
    onMissionDownloadComplete();
    commandToServer('MissionStartPhase3Ack', $MSeq);
    $GeneratingCacheNow = 0;
};
function connect(%server) {
    %conn = new GameConnection("");;
    0;
    %conn.setCommonPreconnectClientSettings("");
    %conn.connect(%server);
};
