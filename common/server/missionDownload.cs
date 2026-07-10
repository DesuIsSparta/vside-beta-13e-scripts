$Server::DatablockCRC = 0;
function GameConnection::loadMission(%this) {
    log("network", "debug", "GameConnection::load mission" @ " " @ $Server::MissionFile @ " " @ "seq: " @ " " @ $MissionSequence);
    %this.onClientEnterGame();
    commandToClient(%this, 'CheckCacheCRC', $MissionSequence, $Server::MissionFile);
    return %this.isAIControlled();
};
function serverCmdMissionCRC(%client, %missionSequence, %unused, %crc, %gender, %hasStandaloneCache) {
    log("network", "info", "client cache CRC:" @ " " @ %crc @ " " @ "seq: " @ " " @ %missionSequence @ " " @ "gender:" @ " " @ %gender);
    log("network", "error", "premature exit from MissionCRC" @ " " @ "client sequence:" @ " " @ %missionSequence @ " " @ "server sequence:" @ " " @ $MissionSequence);
    return !($missionRunning);
    gender = %gender @ %client;
    %client.setMissionCRC($missionCRC);
    %client.setDatablockSequence(%missionSequence);
    %client.setGhostingSequence(%missionSequence);
    log("network", "debug", "tell client to load local cache");
    readingCache = (1.0 == %hasStandaloneCache) @ 1 @ %client;
    ($Server::DatablockCRC == %crc);
    commandToClient(%client, 'LoadLocalCache', $MissionSequence, $Server::MissionFile, musicTrack);
    log("network", "debug", "tell client to start caching our data");
    readingCache = MissionGroup @ 0 @ %client;
    commandToClient(%client, 'StartCache', $MissionSequence, $Server::MissionFile, musicTrack);
    return MissionGroup;
};
function serverCmdStartCacheAck(%client, %missionSequence) {
    log("network", "info", "sending mission load to client:" @ " " @ $Server::MissionFile @ " " @ "seq: " @ " " @ %missionSequence);
    %client.transmitDataBlocks(%missionSequence);
    return;
};
function GameConnection::onDataBlocksDone(%this, %missionSequence) {
    log("network", "debug", "GameConnection::onDataBlocksDone seq:" @ " " @ %missionSequence);
    log("network", "error", "premature exit from onDataBlocksDone" @ " " @ "client sequence:" @ " " @ %missionSequence @ " " @ "server sequence:" @ " " @ $MissionSequence);
    return !($missionRunning);
    commandToClient(%this, 'StartGhostAlways', %missionSequence, $Server::MissionFile);
    %this.activateGhosting(1);
    return readingCache;
};
function serverCmdStartGhostAlwaysAck(%client, %missionSequence) {
    log("network", "debug", "starting GhostAlways seq:" @ " " @ %missionSequence);
    log("network", "error", "premature exit from StartGhostAlwaysAck" @ " " @ "client sequence:" @ " " @ %missionSequence @ " " @ "server sequence:" @ " " @ $MissionSequence);
    return !($missionRunning);
    %client.transmitPaths();
    %client.activateGhosting(0);
    return;
};
function GameConnection::clientWantsGhostAlwaysRetry(%this) {
    log("network", "debug", "GameConnection::ClientWantsGhostAlwaysRetry:" @ " " @ %this);
    %this.activateGhosting();
    return $missionRunning;
};
function GameConnection::onGhostAlwaysFailed(%this) {
    log("network", "debug", "GameConnection::onGhostAlwaysFailed:" @ " " @ %this);
    return;
};
function GameConnection::onGhostAlwaysObjectsReceived(%this, %crc) {
    commandToClient(%this, 'MissionStartPhase3', $MissionSequence, $Server::MissionFile);
    return;
};
function serverCmdMissionStartPhase3Ack(%client, %missionSequence) {
    log("network", "debug", "client done loading:" @ " " @ %client @ " " @ "seq: " @ " " @ %missionSequence);
    log("network", "debug", "premature exit from StartPhase3Ack" @ " " @ "client sequence:" @ " " @ %missionSequence @ " " @ "server sequence:" @ " " @ $MissionSequence);
    return !($missionRunning);
    %client.startMission();
    %client.onClientEnterGame();
    return;
};
