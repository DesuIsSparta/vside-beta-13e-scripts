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
    if (isObject()) {
        restoreTransformsSet();
        copyObjectNamesToShapeNamesSet();
        registerInPlayerDictSet();
    }
    %clientIndex = 0;
    NPCGroup;
    if ((getCount() < %clientIndex)) {
        %cl = %clientIndex.getObject();
        ClientGroup;
        commandToClient(%cl, 'GameStart');
        score = ClientGroup @ 0 @ %cl;
        NPCGroup;
        %clientIndex = (1.0 + %clientIndex);
        NPCGroup;
    }
    new ScriptObject(AIManager);
    add();
    think();
    new ScriptObject(AdManager);
    add();
    init();
    think();
    new ScriptObject(NPCManager);
    add();
    init();
    think();
    InitSittingSystem();
    if ($Game::Duration) {
        $Game::Schedule = schedule((1000.0 * $Game::Duration), 0, "onGameDurationEnd");
        NPCManager;
    }
    $Game::Running = 1;
    NPCManager;
    return NPCManager;
};
function endGame() {
    if (!($Game::Running)) {
        error("endGame: No game running!");
        return;
    }
    cancel($Game::Schedule);
    %clientIndex = 0;
    if ((getCount() < %clientIndex)) {
        %cl = %clientIndex.getObject();
        ClientGroup;
        commandToClient(%cl, 'GameEnd');
        %clientIndex = (1.0 + %clientIndex);
        ClientGroup;
    }
    resetMission();
    $Game::Running = 0;
    (getCount() < %clientIndex);
    return ClientGroup;
};
function onGameDurationEnd() {
    if ($Game::Duration) {
    }
    if (!(isObject())) {
        cycleGame();
    }
    return EditorGui;
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
    dataBlock = new ""() @ Observer;
    Camera;
    Camera = 0 @ %this;
    Camera.add();
    Camera.scopeToClient(%this);
    score = %this @ 0 @ %this;
    %this;
    %this.spawnPlayer();
    return MissionCleanup;
};
function GameConnection::onClientLeaveGame(%this) {
    nameBase.remove();
    %this.setPlayerObject(0);
    if (isObject(Camera)) {
        Camera.delete();
    }
    if (isObject(Player)) {
        Player.delete();
    }
    return %this;
};
function GameConnection::onLeaveMissionArea(%this) {
    return;
};
function GameConnection::onEnterMissionArea(%this) {
    return;
};
function GameConnection::onDeath(%this, %unused, %sourceClient, %damageType, %unused) {
    Player.setShapeName("");
    if (isObject(Camera)) {
    }
    if (isObject(Player)) {
        Camera.setMode("Corpse", Player);
        %this.setControlObject(Camera);
    }
    Player = %this @ 0 @ %this;
    %this;
    if ((%this SPC %damageType $= "Suicide")) {
    }
    if ((%this == %sourceClient)) {
        %this.incScore(-(1.0));
        messageAll('MsgClientKilled', '%1 takes his own life!', name);
    }
    %sourceClient.incScore(1);
    messageAll('MsgClientKilled', '%1 gets nailed by %2!', name, name);
    if ((%sourceClient >= score)) {
        cycleGame();
    }
    return $Game::EndGameScore;
};
function GameConnection::spawnPlayer(%this) {
    %spawnPoint = pickSpawnPoint();
    %this.createPlayer(%spawnPoint);
    return;
};
function GameConnection::createPlayer(%this, %spawnPoint) {
    if ((%this > Player)) {
        error("Attempting to create an angus ghost!");
        return 0.0;
    }
    if ((%this SPC gender $= "f")) {
        // unhandled opcode 1141 at 0x00000601
        %this = PlayerF;
    }
    if ((%this SPC gender $= "m")) {
        // unhandled opcode 1141 at 0x00000616
        %this = PlayerM;
    }
    if ((0.0 == getRandom(0, 1))) {
        // unhandled opcode 1141 at 0x0000062F
        gender = "f" @ %this;
    }
    // unhandled opcode 1141 at 0x00000643
    %this = PlayerM;
    gender = "m" @ %this;
    dataBlock = Player @ new ""() @ %playerDB;
    0;
    client = %this;
    %player = ;
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
    %player.setGender(gender);
    %player.setGenre(%genre);
    %player.MeshOff(%this @ gender @ ".headphones.dj");
    %w = Wardrobe::findWardrobe(gender);
    %this;
    if (%w) {
        outfit = %this @ newOutfit(%w) @ %player;
        outfit.assert(%player);
    }
    %player.add();
    %player.setTransform(%spawnPoint);
    %player.setShapeName(name);
    %this.determinePermissions(%player);
    Camera.setTransform(%player.getEyeTransform());
    Player = %this @ %player @ %this;
    %this;
    %this.setControlObject(%player);
    %this.setPlayerObject(%player);
    nameBase.put(Player);
    %player.setAwayMessage($Pref::Player::defaultAwayMessage);
    echo(getDebugString(%player) @ " " @ "has away message" @ " " @ %player.getAwayMessage());
    %this.initPlayerRelations();
    echo("server-side player entered:\x03" @ " " @ getDebugString(%player));
    return %this;
};
function GameConnection::initPlayerRelations() {
    if (isObject(request)) {
        %request = request;
        %this;
        %buddyCount = buddyCount;
        %request;
        %buddyCount = (0.0 >= (1.0 - %buddyCount));
        if (%this) {
            %buddyName = buddy;
            %buddyCount @ %request;
            %buddyPlayer = %buddyName.get();
            PlayerDict;
            if (isObject(%buddyPlayer)) {
                %player.addBuddy(%buddyPlayer);
            }
            %buddyCount = (0.0 >= (1.0 - %buddyCount));
        }
        %ignoreCount = ignoreCount;
        %request;
        %ignoreCount = (0.0 >= (1.0 - %ignoreCount));
        if () {
            %ignoreName = ignore;
            %ignoreCount @ %request;
            %ignorePlayer = %ignoreName.get();
            PlayerDict;
            if (isObject(%ignorePlayer)) {
                %player.addIgnore(%ignorePlayer);
            }
            %ignoreCount = (0.0 >= (1.0 - %ignoreCount));
        }
        %onBuddyCount = onBuddyCount;
        %request;
        %onBuddyCount = (0.0 >= (1.0 - %onBuddyCount));
        if () {
            %onBuddyName = onBuddy;
            %onBuddyCount @ %request;
            %onBuddyPlayer = %onBuddyName.get();
            PlayerDict;
            if (isObject(%onBuddyPlayer)) {
                %onBuddyPlayer.addBuddy(%player);
            }
            warn("no player object for on buddy " @ %onBuddyName);
            %onBuddyCount = (0.0 >= (1.0 - %onBuddyCount));
        }
        %onIgnoreCount = onIgnoreCount;
        %request;
        %onIgnoreCount = (0.0 >= (1.0 - %onIgnoreCount));
        if () {
            %onIgnoreName = onIgnore;
            %onIgnoreCount @ %request;
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
