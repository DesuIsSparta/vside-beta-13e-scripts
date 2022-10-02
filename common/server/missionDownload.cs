$Server::DatablockCRC = 0;
function GameConnection::loadMission(%this)
{
    log("network", "debug", "GameConnection::load mission" SPC $Server::MissionFile SPC "seq: " SPC $MissionSequence);
    if (%this.isAIControlled())
    {
        %this.onClientEnterGame();
    }
    else
    {
        commandToClient(%this, 'CheckCacheCRC', $MissionSequence, $Server::MissionFile);
    }
    return ;
}
function serverCmdMissionCRC(%client, %missionSequence, %unused, %crc, %gender, %hasStandaloneCache)
{
    log("network", "info", "client cache CRC:" SPC %crc SPC "seq: " SPC %missionSequence SPC "gender:" SPC %gender);
    if ((%missionSequence != $MissionSequence) && !$missionRunning)
    {
        log("network", "error", "premature exit from MissionCRC" SPC "client sequence:" SPC %missionSequence SPC "server sequence:" SPC $MissionSequence);
        return ;
    }
    %client.gender = %gender;
    %client.setMissionCRC($missionCRC);
    %client.setDatablockSequence(%missionSequence);
    %client.setGhostingSequence(%missionSequence);
    if ((%crc == $Server::DatablockCRC) && (%hasStandaloneCache == 1))
    {
        log("network", "debug", "tell client to load local cache");
        %client.readingCache = 1;
        commandToClient(%client, 'LoadLocalCache', $MissionSequence, $Server::MissionFile, MissionGroup.musicTrack);
    }
    else
    {
        log("network", "debug", "tell client to start caching our data");
        %client.readingCache = 0;
        commandToClient(%client, 'StartCache', $MissionSequence, $Server::MissionFile, MissionGroup.musicTrack);
    }
    return ;
}
function serverCmdStartCacheAck(%client, %missionSequence)
{
    log("network", "info", "sending mission load to client:" SPC $Server::MissionFile SPC "seq: " SPC %missionSequence);
    %client.transmitDataBlocks(%missionSequence);
    return ;
}
function GameConnection::onDataBlocksDone(%this, %missionSequence)
{
    log("network", "debug", "GameConnection::onDataBlocksDone seq:" SPC %missionSequence);
    if ((%missionSequence != $MissionSequence) && !$missionRunning)
    {
        log("network", "error", "premature exit from onDataBlocksDone" SPC "client sequence:" SPC %missionSequence SPC "server sequence:" SPC $MissionSequence);
        return ;
    }
    commandToClient(%this, 'StartGhostAlways', %missionSequence, $Server::MissionFile);
    if (%this.readingCache)
    {
        %this.activateGhosting(1);
    }
    return ;
}
function serverCmdStartGhostAlwaysAck(%client, %missionSequence)
{
    log("network", "debug", "starting GhostAlways seq:" SPC %missionSequence);
    if ((%missionSequence != $MissionSequence) && !$missionRunning)
    {
        log("network", "error", "premature exit from StartGhostAlwaysAck" SPC "client sequence:" SPC %missionSequence SPC "server sequence:" SPC $MissionSequence);
        return ;
    }
    %client.transmitPaths();
    %client.activateGhosting(0);
    return ;
}
function GameConnection::clientWantsGhostAlwaysRetry(%this)
{
    log("network", "debug", "GameConnection::ClientWantsGhostAlwaysRetry:" SPC %this);
    if ($missionRunning)
    {
        %this.activateGhosting();
    }
    return ;
}
function GameConnection::onGhostAlwaysFailed(%this)
{
    log("network", "debug", "GameConnection::onGhostAlwaysFailed:" SPC %this);
    return ;
}
function GameConnection::onGhostAlwaysObjectsReceived(%this, %crc)
{
    commandToClient(%this, 'MissionStartPhase3', $MissionSequence, $Server::MissionFile);
    return ;
}
function serverCmdMissionStartPhase3Ack(%client, %missionSequence)
{
    log("network", "debug", "client done loading:" SPC %client SPC "seq: " SPC %missionSequence);
    if ((%missionSequence != $MissionSequence) && !$missionRunning)
    {
        log("network", "debug", "premature exit from StartPhase3Ack" SPC "client sequence:" SPC %missionSequence SPC "server sequence:" SPC $MissionSequence);
        return ;
    }
    %client.startMission();
    %client.onClientEnterGame();
    return ;
}
