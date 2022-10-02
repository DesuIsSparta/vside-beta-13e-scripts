$Game::Duration = $Pref::Server::TimeLimit * 60;
$Game::EndGameScore = 30;
$Game::EndGamePause = 10;
function onServerCreated()
{
    $Server::GameType = "PCD";
    $Server::MissionType = "Lounge";
    $Game::StartTime = 0;
    exec("./audioProfiles.cs");
    exec("./camera.cs");
    exec("./markers.cs");
    exec("./shapeBase.cs");
    exec("./item.cs");
    exec("./staticShape.cs");
    exec("./player.cs");
    exec("./aiPlayer.cs");
    exec("./triggers.cs");
    exec("projects/common/shapes/adverts/adverts.cs");
    exec("projects/common/worlds/props.cs");
    exec("./ets/sitting.cs");
    exec("./ets/doors.cs");
    exec("./ets/ambientAnimateShape.cs");
    exec("./ets/particles.cs");
    exec("./ets/NPC_Datablocks.cs");
    exec("common/synapseGaming/contentPacks/lightingPack/sgDeployServer.cs");
    $Game::StartTime = $Sim::Time;
    if (!($Pref::Net::DisplayOnMaster $= "Never"))
    {
        schedule(0, 0, serverStart);
    }
    return ;
}
function onServerDestroyed()
{
    echo("server exiting");
    return ;
}
function onMissionLoaded()
{
    startGame();
    return ;
}
function onMissionEnded()
{
    cancel($Game::Schedule);
    $Game::Running = 0;
    $Game::Cycling = 0;
    return ;
}
function startGame()
{
    if ($Game::Running)
    {
        error("startGame: End the game first!");
        return ;
    }
    if (isObject(NPCGroup))
    {
        restoreTransformsSet(NPCGroup);
        copyObjectNamesToShapeNamesSet(NPCGroup);
        registerInPlayerDictSet(NPCGroup);
    }
    %clientIndex = 0;
    while (%clientIndex < ClientGroup.getCount())
    {
        %cl = ClientGroup.getObject(%clientIndex);
        commandToClient(%cl, 'GameStart');
        %cl.score = 0;
        %clientIndex = %clientIndex + 1;
    }
    new ScriptObject(AIManager);
    MissionCleanup.add(AIManager);
    AIManager.think();
    new ScriptObject(AdManager);
    MissionCleanup.add(AdManager);
    AdManager.init();
    AdManager.think();
    new ScriptObject(NPCManager);
    MissionCleanup.add(NPCManager);
    NPCManager.init();
    NPCManager.think();
    InitSittingSystem();
    if ($Game::Duration)
    {
        $Game::Schedule = schedule($Game::Duration * 1000, 0, "onGameDurationEnd");
    }
    $Game::Running = 1;
    return ;
}
function endGame()
{
    if (!$Game::Running)
    {
        error("endGame: No game running!");
        return ;
    }
    cancel($Game::Schedule);
    %clientIndex = 0;
    while (%clientIndex < ClientGroup.getCount())
    {
        %cl = ClientGroup.getObject(%clientIndex);
        commandToClient(%cl, 'GameEnd');
        %clientIndex = %clientIndex + 1;
    }
    resetMission();
    $Game::Running = 0;
    return ;
}
function onGameDurationEnd()
{
    if ($Game::Duration && !isObject(EditorGui))
    {
        cycleGame();
    }
    return ;
}
function cycleGame()
{
    if (!$Game::Cycling)
    {
        $Game::Cycling = 1;
        $Game::Schedule = schedule(0, 0, "onCycleExec");
    }
    return ;
}
function onCycleExec()
{
    endGame();
    $Game::Schedule = schedule($Game::EndGamePause * 1000, 0, "onCyclePauseEnd");
    return ;
}
function onCyclePauseEnd()
{
    $Game::Cycling = 0;
    %search = $Server::MissionFileSpec;
    %file = findFirstFile(%search);
    while (!(%file $= ""))
    {
        if (%file $= $Server::MissionFile)
        {
            %file = findNextFile(%search);
            if (%file $= "")
            {
                %file = findFirstFile(%search);
            }
            break;
        }
        %file = findNextFile(%search);
    }
    loadMission(%file);
    return ;
}
function GameConnection::onClientEnterGame(%this)
{
    commandToClient(%this, 'SyncClock', $Sim::Time - $Game::StartTime);
    %this.Camera = new Camera();
    MissionCleanup.add(%this.Camera);
    %this.Camera.scopeToClient(%this);
    %this.score = 0;
    %this.spawnPlayer();
    return ;
}
function GameConnection::onClientLeaveGame(%this)
{
    PlayerDict.remove(%this.nameBase);
    %this.setPlayerObject(0);
    if (isObject(%this.Camera))
    {
        %this.Camera.delete();
    }
    if (isObject(%this.Player))
    {
        %this.Player.delete();
    }
    return ;
}
function GameConnection::onLeaveMissionArea(%this)
{
    return ;
}
function GameConnection::onEnterMissionArea(%this)
{
    return ;
}
function GameConnection::onDeath(%this, %unused, %sourceClient, %damageType, %unused)
{
    %this.Player.setShapeName("");
    if (isObject(%this.Camera) && isObject(%this.Player))
    {
        %this.Camera.setMode("Corpse", %this.Player);
        %this.setControlObject(%this.Camera);
    }
    %this.Player = 0;
    if ((%damageType $= "Suicide") && (%sourceClient == %this))
    {
        %this.incScore(-1);
        messageAll('MsgClientKilled', '%1 takes his own life!', %this.name);
    }
    else
    {
        %sourceClient.incScore(1);
        messageAll('MsgClientKilled', '%1 gets nailed by %2!', %this.name, %sourceClient.name);
        if (%sourceClient.score >= $Game::EndGameScore)
        {
            cycleGame();
        }
    }
    return ;
}
function GameConnection::spawnPlayer(%this)
{
    %spawnPoint = pickSpawnPoint();
    %this.createPlayer(%spawnPoint);
    return ;
}
function GameConnection::createPlayer(%this, %spawnPoint)
{
    if (%this.Player > 0)
    {
        error("Attempting to create an angus ghost!");
        return ;
    }
    if (%this.gender $= "f")
    {
        %playerDB = PlayerF;
    }
    else
    {
        if (%this.gender $= "m")
        {
            %playerDB = PlayerM;
        }
        else
        {
            if (getRandom(0, 1) == 0)
            {
                %playerDB = PlayerF;
                %this.gender = "f";
            }
            else
            {
                %playerDB = PlayerM;
                %this.gender = "m";
            }
        }
    }
    %player = new Player()
    {
        dataBlock = %playerDB;
        client = %this;
    };
    %rand = getRandom(0, 2);
    if (%rand == 0)
    {
        %genre = "h";
    }
    else
    {
        if (%rand == 1)
        {
            %genre = "i";
        }
        else
        {
            if (%rand == 2)
            {
                %genre = "p";
            }
        }
    }
    %player.setGender(%this.gender);
    %player.setGenre(%genre);
    %player.MeshOff(%this.gender @ ".headphones.dj");
    %w = Wardrobe::findWardrobe(%this.gender);
    if (%w)
    {
        %player.outfit = newOutfit(%w);
        %player.outfit.assert(%player);
    }
    MissionCleanup.add(%player);
    %player.setTransform(%spawnPoint);
    %player.setShapeName(%this.name);
    %this.determinePermissions(%player);
    %this.Camera.setTransform(%player.getEyeTransform());
    %this.Player = %player;
    %this.setControlObject(%player);
    %this.setPlayerObject(%player);
    PlayerDict.put(%this.nameBase, %this.Player);
    %player.setAwayMessage($Pref::Player::defaultAwayMessage);
    echo(getDebugString(%player) SPC "has away message" SPC %player.getAwayMessage());
    %this.initPlayerRelations();
    echo("server-side player entered:\c2" SPC getDebugString(%player));
    return ;
}
function GameConnection::initPlayerRelations()
{
    if (isObject(%this.request))
    {
        %request = %this.request;
        %buddyCount = %request.buddyCount;
        while (%buddyCount = (%buddyCount - 1) >= 0)
        {
            %buddyName = %request.buddy[%buddyCount];
            %buddyPlayer = PlayerDict.get(%buddyName);
            if (isObject(%buddyPlayer))
            {
                %player.addBuddy(%buddyPlayer);
            }
        }
        %ignoreCount = %request.ignoreCount;
        while (%ignoreCount = (%ignoreCount - 1) >= 0)
        {
            %ignoreName = %request.ignore[%ignoreCount];
            %ignorePlayer = PlayerDict.get(%ignoreName);
            if (isObject(%ignorePlayer))
            {
                %player.addIgnore(%ignorePlayer);
            }
        }
        %onBuddyCount = %request.onBuddyCount;
        while (%onBuddyCount = (%onBuddyCount - 1) >= 0)
        {
            %onBuddyName = %request.onBuddy[%onBuddyCount];
            %onBuddyPlayer = PlayerDict.get(%onBuddyName);
            if (isObject(%onBuddyPlayer))
            {
                %onBuddyPlayer.addBuddy(%player);
            }
            else
            {
                warn("no player object for on buddy " @ %onBuddyName);
            }
        }
        %onIgnoreCount = %request.onIgnoreCount;
        while (%onIgnoreCount = (%onIgnoreCount - 1) >= 0)
        {
            %onIgnoreName = %request.onIgnore[%onIgnoreCount];
            %onIgnorePlayer = PlayerDict.get(%onIgnoreName);
            if (isObject(%onIgnorePlayer))
            {
                %onIgnorePlayer.addIgnore(%player);
            }
            else
            {
                warn("no player object for on ignore " @ %onIgnorePlayer);
            }
        }
    }
}

function pickSpawnPoint()
{
    %groupName = "MissionGroup/PlayerDropPoints";
    %group = nameToID(%groupName);
    if (%group != -1)
    {
        %count = %group.getCount();
        if (%count != 0)
        {
            %index = getRandom(%count - 1);
            %spawn = %group.getObject(%index);
            return %spawn.getEmptySpot(1.5, 0, 1);
        }
        else
        {
            error("No spawn points found in " @ %groupName);
        }
    }
    else
    {
        error("Missing spawn points group " @ %groupName);
    }
    return "0 0 300 1 0 0 0";
}
