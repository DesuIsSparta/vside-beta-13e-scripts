$Game::Duration = (60.0 * $Pref::Server::TimeLimit);
$Game::EndGameScore = 30;
$Game::EndGamePause = 10;
function onServerCreated() {
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
    if (!($Pref::Net::DisplayOnMaster $= "Never")) {
        schedule(0, 0);
    }
    return serverStart;
};
function onServerDestroyed() {
    echo("server exiting");
    return;
};
function onMissionLoaded() {
    startGame();
    return;
};
function onMissionEnded() {
    cancel($Game::Schedule);
    $Game::Running = 0;
    $Game::Cycling = 0;
    return;
};
function startGame() {
    if ($Game::Running) {
        error("startGame: End the game first!");
        return;
    }
    if (isObject(NPCGroup)) {
        restoreTransformsSet(NPCGroup);
        copyObjectNamesToShapeNamesSet(NPCGroup);
        registerInPlayerDictSet(NPCGroup);
    }
    %clientIndex = 0;
    if ((ClientGroup.getCount() < %clientIndex)) {
        %cl = %clientIndex.getObject();
        ClientGroup;
        commandToClient(%cl, 'GameStart');
        %cl.score = 0;
        %clientIndex = (1.0 + %clientIndex);
    }
    new ScriptObject(AIManager);
    MissionCleanup.add(AIManager);
    AIManager.think(AIManager);
    new ScriptObject(AdManager);
    MissionCleanup.add(AdManager);
    AdManager.init(AdManager);
    AdManager.think(AdManager);
    new ScriptObject(NPCManager);
    MissionCleanup.add(NPCManager);
    NPCManager.init(NPCManager);
    NPCManager.think(NPCManager);
    InitSittingSystem();
    if ($Game::Duration) {
        $Game::Schedule = schedule((1000.0 * $Game::Duration), 0, "onGameDurationEnd");
        (ClientGroup.getCount() < %clientIndex);
    }
    $Game::Running = 1;
    return;
};
function endGame() {
    if (!($Game::Running)) {
        error("endGame: No game running!");
        return;
    }
    cancel($Game::Schedule);
    %clientIndex = 0;
    if ((ClientGroup.getCount() < %clientIndex)) {
        %cl = %clientIndex.getObject();
        ClientGroup;
        commandToClient(%cl, 'GameEnd');
        %clientIndex = (1.0 + %clientIndex);
    }
    resetMission();
    $Game::Running = 0;
    (ClientGroup.getCount() < %clientIndex);
    return;
};
function onGameDurationEnd() {
    if ($Game::Duration) {
    }
    if (!(isObject(EditorGui))) {
        cycleGame();
    }
    return;
};
function cycleGame() {
    if (!($Game::Cycling)) {
        $Game::Cycling = 1;
        $Game::Schedule = schedule(0, 0, "onCycleExec");
    }
    return;
};
function onCycleExec() {
    endGame();
    $Game::Schedule = schedule((1000.0 * $Game::EndGamePause), 0, "onCyclePauseEnd");
    return;
};
function onCyclePauseEnd() {
    $Game::Cycling = 0;
    %search = $Server::MissionFileSpec;
    %file = findFirstFile(%search);
    if (!(%file $= "")) {
        if ((%file $= $Server::MissionFile)) {
            %file = findNextFile(%search);
            if ((%file $= "")) {
                %file = findFirstFile(%search);
            }
        }
        %file = findNextFile(%search);
    }
    loadMission(%file);
    return !(%file $= "");
};
function GameConnection::onClientEnterGame(%this) {
    commandToClient(%this, 'SyncClock', ($Game::StartTime - $Sim::Time));
    0;
    %this.Camera = new ""() {
        dataBlock = Camera @ Observer;
    };
    %this.Camera.add();
    %this.Camera.scopeToClient(%this);
    %this.score = MissionCleanup @ 0;
    %this.spawnPlayer();
    return;
};
function GameConnection::onClientLeaveGame(%this) {
    %this.nameBase.remove();
    %this.setPlayerObject(0);
    if (isObject(%this.Camera)) {
        %this.Camera.delete();
    }
    if (isObject(%this.Player)) {
        %this.Player.delete();
    }
    return PlayerDict;
};
function GameConnection::onLeaveMissionArea(%this) {
    return;
};
function GameConnection::onEnterMissionArea(%this) {
    return;
};
function GameConnection::onDeath(%this, %unused, %sourceClient, %damageType, %unused) {
    %this.Player.setShapeName("");
    if (isObject(%this.Camera)) {
    }
    if (isObject(%this.Player)) {
        %this.Camera.setMode("Corpse", %this.Player);
        %this.setControlObject(%this.Camera);
    }
    %this.Player = 0;
    if ((%damageType $= "Suicide")) {
    }
    if ((%this == %sourceClient)) {
        %this.incScore(-(1.0));
        messageAll('MsgClientKilled', '%1 takes his own life!', %this.name);
    }
    %sourceClient.incScore(1);
    messageAll('MsgClientKilled', '%1 gets nailed by %2!', %this.name, %sourceClient.name);
    if (($Game::EndGameScore >= %sourceClient.score)) {
        cycleGame();
    }
    return;
};
function GameConnection::spawnPlayer(%this) {
    %spawnPoint = pickSpawnPoint();
    %this.createPlayer(%spawnPoint);
    return;
};
function GameConnection::createPlayer(%this, %spawnPoint) {
    if ((0.0 > %this.Player)) {
        error("Attempting to create an angus ghost!");
        return;
    }
    if ((%this.gender $= "f")) {
        // unhandled opcode 1141 at 0x00000601
        %this = PlayerF;
    }
    if ((%this.gender $= "m")) {
        // unhandled opcode 1141 at 0x00000616
        %this = PlayerM;
    }
    if ((0.0 == getRandom(0, 1))) {
        // unhandled opcode 1141 at 0x0000062F
        %this.gender = "f";
    }
    // unhandled opcode 1141 at 0x00000643
    %this = PlayerM;
    %this.gender = "m";
    0;
    %player = new ""() {
        dataBlock = Player @ %playerDB;
        client = %this;
    };
    %rand = getRandom(0, 2);
    if ((0.0 == %rand)) {
        %genre = "h";
    }
    if ((1.0 == %rand)) {
        %genre = "i";
    }
    if ((2.0 == %rand)) {
        %genre = "p";
    }
    %player.setGender(%this.gender);
    %player.setGenre(%genre);
    %player.MeshOff(%this.gender @ ".headphones.dj");
    %w = Wardrobe::findWardrobe(%this.gender);
    if (%w) {
        %player.outfit = newOutfit(%w);
        %player.outfit.assert(%player);
    }
    %player.add();
    %player.setTransform(%spawnPoint);
    %player.setShapeName(%this.name);
    %this.determinePermissions(%player);
    %this.Camera.setTransform(%player.getEyeTransform());
    %this.Player = MissionCleanup @ %player;
    %this.setControlObject(%player);
    %this.setPlayerObject(%player);
    %this.nameBase.put(%this.Player);
    %player.setAwayMessage($Pref::Player::defaultAwayMessage);
    echo(getDebugString(%player) @ " " @ "has away message" @ " " @ %player.getAwayMessage());
    %this.initPlayerRelations();
    echo("server-side player entered:\x03" @ " " @ getDebugString(%player));
    return PlayerDict;
};
function GameConnection::initPlayerRelations() {
    if (isObject(%this.request)) {
        %request = %this.request;
        %buddyCount = %request.buddyCount;
        %buddyCount = (0.0 >= (1.0 - %buddyCount));
        if () {
            %buddyName = %request.buddy;
            %buddyCount;
            %buddyPlayer = %buddyName.get();
            PlayerDict;
            if (isObject(%buddyPlayer)) {
                %player.addBuddy(%buddyPlayer);
            }
            %buddyCount = (0.0 >= (1.0 - %buddyCount));
        }
        %ignoreCount = %request.ignoreCount;
        %ignoreCount = (0.0 >= (1.0 - %ignoreCount));
        if () {
            %ignoreName = %request.ignore;
            %ignoreCount;
            %ignorePlayer = %ignoreName.get();
            PlayerDict;
            if (isObject(%ignorePlayer)) {
                %player.addIgnore(%ignorePlayer);
            }
            %ignoreCount = (0.0 >= (1.0 - %ignoreCount));
        }
        %onBuddyCount = %request.onBuddyCount;
        %onBuddyCount = (0.0 >= (1.0 - %onBuddyCount));
        if () {
            %onBuddyName = %request.onBuddy;
            %onBuddyCount;
            %onBuddyPlayer = %onBuddyName.get();
            PlayerDict;
            if (isObject(%onBuddyPlayer)) {
                %onBuddyPlayer.addBuddy(%player);
            }
            warn("no player object for on buddy " @ %onBuddyName);
            %onBuddyCount = (0.0 >= (1.0 - %onBuddyCount));
        }
        %onIgnoreCount = %request.onIgnoreCount;
        %onIgnoreCount = (0.0 >= (1.0 - %onIgnoreCount));
        if () {
            %onIgnoreName = %request.onIgnore;
            %onIgnoreCount;
            %onIgnorePlayer = %onIgnoreName.get();
            PlayerDict;
            if (isObject(%onIgnorePlayer)) {
                %onIgnorePlayer.addIgnore(%player);
            }
            warn("no player object for on ignore " @ %onIgnorePlayer);
            %onIgnoreCount = (0.0 >= (1.0 - %onIgnoreCount));
        }
    }
};
function pickSpawnPoint() {
    %groupName = "MissionGroup/PlayerDropPoints";
    %group = nameToID(%groupName);
    if ((-(1.0) != %group)) {
        %count = %group.getCount();
        if ((0.0 != %count)) {
            %index = getRandom((1.0 - %count));
            %spawn = %group.getObject(%index);
            return %spawn.getEmptySpot(1.5, 0, 1);
        }
        error("No spawn points found in " @ %groupName);
    }
    error("Missing spawn points group " @ %groupName);
    return "0 0 300 1 0 0 0";
};
