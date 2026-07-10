datablock PlayerData(DemoPlayer : PlayerM) {
    shootingDelay = 2000;
};
function DemoPlayer::onReachDestination(%this, %obj) {
    if (!(%obj.Path $= "")) {
        if ((%obj.targetNode == %obj.currentNode)) {
            %this.onEndOfPath(%obj, %obj.Path);
        }
        DemoPlayer.moveToNextNode(%obj);
    }
};
function DemoPlayer::onEndOfPath(%this, %obj, %path) {
    DemoPlayer.nextTask(%obj);
};
function DemoPlayer::onEndSequence(%this, %obj, %slot) {
    echo("Sequence Done!");
    %obj.stopThread(%slot);
    DemoPlayer.nextTask(%obj);
};
function AIPlayer::spawn(%name, %spawnPoint) {
    if ((0.0 == getRandom(0, 1))) {
        // unhandled opcode 205 at 0x000000C4
    }
    // unhandled opcode 205 at 0x000000CC
    %player = new AIPlayer("") {
        dataBlock = 0 @ %botDB;
        Path = "";
    };
    MissionCleanup.add(%player);
    %player.setShapeName(%name);
    %player.setTransform(%spawnPoint);
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
    %player.setGenre(%genre);
    return %player;
};
function AIPlayer::spawnOnPath(%name, %path) {
    if (!(isObject(%path))) {
        return;
    }
    %node = %path.getObject(0);
    %player = AIPlayer::spawn(%name, %node.getTransform());
    return %player;
};
function AIPlayer::followPath(%this, %path, %node) {
    %this.stopThread(0);
    if (!(isObject(%path))) {
        %this.Path = "";
        return;
    }
    if (((1.0 - %path.getCount()) > %node)) {
        %this.targetNode = (1.0 - %path.getCount());
    }
    %this.targetNode = %node;
    if ((%this.Path $= %path)) {
        %this.moveToNode(%this.currentNode);
    }
    %this.Path = %path;
    %this.moveToNode(0);
};
function AIPlayer::moveToNextNode(%this) {
    if ((0.0 < %this.targetNode)) {
    }
    if ((%this.targetNode < %this.currentNode)) {
        if (((1.0 - %this.Path.getCount()) < %this.currentNode)) {
            %this.moveToNode((1.0 + %this.currentNode));
        }
        %this.moveToNode(0);
    }
    if ((0.0 == %this.currentNode)) {
        %this.moveToNode((1.0 - %this.Path.getCount()));
    }
    %this.moveToNode((1.0 - %this.currentNode));
};
function AIPlayer::moveToNode(%this, %index) {
    %this.currentNode = %index;
    %node = %this.Path.getObject(%index);
    %this.setMoveDestination(%node.getTransform(), (%this.targetNode == %index));
};
function AIPlayer::pushTask(%this, %method) {
    if ((%this.taskIndex $= "")) {
        %this.taskIndex = 0;
        %this.taskCurrent = -(1.0);
    }
    %this.task = %method @ %this.taskIndex;
    %this.taskIndex = (1.0 + %this.taskIndex);
    if ((-(1.0) == %this.taskCurrent)) {
        %this.executeTask((1.0 - %this.taskIndex));
    }
};
function AIPlayer::clearTasks(%this) {
    %this.taskIndex = 0;
    %this.taskCurrent = -(1.0);
};
function AIPlayer::nextTask(%this) {
    if ((-(1.0) != %this.taskCurrent)) {
        if (((1.0 - %this.taskIndex) < %this.taskCurrent)) {
            %this.taskCurrent = (1.0 + %this.taskCurrent);
            %this.executeTask();
        }
        %this.taskCurrent = -(1.0);
    }
};
function AIPlayer::executeTask(%this, %index) {
    %this.taskCurrent = %index;
    eval(%this.getId() @ "." @ %index @ %this.task @ ";");
};
function AIPlayer::singleShot(%this) {
    %this.setImageTrigger(0, 1);
    %this.setImageTrigger(0, 0);
    %this.Trigger = singleShot @ %this.schedule(%this.shootingDelay);
};
function AIPlayer::wait(%this, %time) {
    %this.schedule((1000.0 * %time), "nextTask");
};
function AIPlayer::done(%this, %time) {
    %this.schedule(0, "delete");
};
function AIPlayer::fire(%this, %bool) {
    if (%bool) {
        cancel(%this.Trigger);
        %this.singleShot();
    }
    cancel(%this.Trigger);
    %this.nextTask();
};
function AIPlayer::aimAt(%this, %object) {
    echo("Aim: " @ %object);
    %this.setAimObject(%object);
    %this.nextTask();
};
function AIPlayer::animate(%this, %seq) {
    %this.setActionThread(%seq);
};
function AIPlayer::thinkETS(%this, %periodMS) {
    %secondsBetweenEmotes = 20;
    %secondsBetweenMoves = 10;
    %secondsBetweenWords = 13;
    %secondsBetweenEavesdrops = 7;
    if ((getRandom(0, (%secondsBetweenEmotes * 1000.0)) > %periodMS)) {
    }
    if ((%this.getMoveState() $= "stop")) {
        %this.doRandomEmote();
    }
    if (%this.BotsMove) {
        if ((getRandom(0, (%secondsBetweenMoves * 1000.0)) > %periodMS)) {
            %this.doAutoMoveEntry();
        }
    }
    if (%this.BotsBlahBlah) {
        if ((getRandom(0, (%secondsBetweenWords * 1000.0)) > %periodMS)) {
            %this.doBlahBlah(%this.botEavesdropTarget);
        }
    }
    if (%this.BotsEavesdrop) {
        if ((getRandom(0, (%secondsBetweenEavesdrops * 1000.0)) > %periodMS)) {
            %this.doEavesdropChange();
        }
    }
};
function AIPlayer::doBlahBlah(%this, %target) {
    if ((AIManager == %this.blahblahsNum)) {
        %this.blahblahsNum = 1 @ AIManager;
        0.0;
        %this.blahblahs = AIManager @ %this.blahblahsNum @ AIManager;
        "i am a bot !";
        %this.blahblahsNum = (AIManager + %this.blahblahsNum);
        1.0;
        %this.blahblahs = AIManager @ %this.blahblahsNum @ AIManager;
        "do you like cheese ?";
        %this.blahblahsNum = (AIManager + %this.blahblahsNum);
        1.0;
        %this.blahblahs = AIManager @ %this.blahblahsNum @ AIManager;
        ".. yeah.";
        %this.blahblahsNum = (AIManager + %this.blahblahsNum);
        1.0;
        %this.blahblahs = AIManager @ %this.blahblahsNum @ AIManager;
        "dancing is the BEST.";
        %this.blahblahsNum = (AIManager + %this.blahblahsNum);
        1.0;
        %this.blahblahs = AIManager @ %this.blahblahsNum @ AIManager;
        "ASL ?";
        %this.blahblahsNum = (AIManager + %this.blahblahsNum);
        1.0;
        %this.blahblahs = AIManager @ %this.blahblahsNum @ AIManager;
        "let's go dance.";
        %this.blahblahsNum = (AIManager + %this.blahblahsNum);
        1.0;
        %this.blahblahs = AIManager @ %this.blahblahsNum @ AIManager;
        "cool!";
        %this.blahblahsNum = (AIManager + %this.blahblahsNum);
        1.0;
        %this.blahblahs = AIManager @ %this.blahblahsNum @ AIManager;
        ";)";
        %this.blahblahsNum = (AIManager + %this.blahblahsNum);
        1.0;
        %this.blahblahs = AIManager @ %this.blahblahsNum @ AIManager;
        "this rocks.";
        %this.blahblahsNum = (AIManager + %this.blahblahsNum);
        1.0;
        %this.blahblahs = AIManager @ %this.blahblahsNum @ AIManager;
        "where's the party ?";
        %this.blahblahsNum = (AIManager + %this.blahblahsNum);
        1.0;
        %this.blahblahs = AIManager @ %this.blahblahsNum @ AIManager;
        "yawn zzzz..";
        %this.blahblahsNum = (AIManager + %this.blahblahsNum);
        1.0;
        %this.blahblahs = AIManager @ %this.blahblahsNum @ AIManager;
        "nice outfit.";
        %this.blahblahsNum = (AIManager + %this.blahblahsNum);
        1.0;
        %this.blahblahs = AIManager @ %this.blahblahsNum @ AIManager;
        "will you be my friend ?";
        %this.blahblahsNum = (AIManager + %this.blahblahsNum);
        1.0;
        %this.blahblahs = AIManager @ %this.blahblahsNum @ AIManager;
        "lol !";
        %this.blahblahsNum = (AIManager + %this.blahblahsNum);
        1.0;
        %this.blahblahs = AIManager @ %this.blahblahsNum @ AIManager;
        "rotfl !";
        %this.blahblahsNum = (AIManager + %this.blahblahsNum);
        1.0;
        %this.blahblahs = AIManager @ %this.blahblahsNum @ AIManager;
        "flirt";
        %this.blahblahsNum = (AIManager + %this.blahblahsNum);
        1.0;
        %this.blahblahsNum = (AIManager - %this.blahblahsNum);
        1.0;
    }
    %num = getRandom(1, AIManager, %this.blahblahsNum);
    %msg = %this.blahblahs;
    %num @ AIManager;
    ServersideChatMessage(%this, %target, %msg);
    CONV_DEBUG("Bot" @ " " @ %this @ " " @ "said" @ " " @ %msg @ " " @ "(" @ " " @ %num @ " " @ "/" @ " ", AIManager @ %this.blahblahsNum @ " " @ ")");
};
function AIManager::BotsBlahBlahOneShot(%this, %player) {
    %i = 0;
    if ((%this.numBots < %i)) {
        %i.doBlahBlah(%this.bots, %player);
        %i = (1.0 + %i);
    }
};
function AIPlayer::doEavesdropChange(%this) {
    %newEavesdropTarget = %this.bots;
    getRandom(0, 1.0, (AIManager - %this.numBots)) @ AIManager;
    if ((%this == %newEavesdropTarget)) {
        %newEavesdropTarget = 0;
    }
    CONV_DEBUG("Bot" @ " " @ %this @ " " @ "Switching eavesdrop from" @ " " @ %this.botEavesdropTarget @ " " @ "to" @ " " @ %newEavesdropTarget);
    serverSideEavesdrop(%this, %this.botEavesdropTarget, %newEavesdropTarget);
    CONV_DEBUG("Bot" @ " " @ %this @ " " @ "-Switched eavesdrop from" @ " " @ %this.botEavesdropTarget @ " " @ "to" @ " " @ %newEavesdropTarget);
    %this.botEavesdropTarget = %newEavesdropTarget;
};
function AIPlayer::doRandomEmote(%this) {
    playRandomEmote(%this);
};
function AIPlayer::doAutoMove(%this) {
    %amount = 700;
    if ((0.0 @ 0 == %this.autoMoveList)) {
        %this.autoMoveList = %amount @ " " @ "0 0" @ 0;
        %this.autoMoveList = 0 @ " " @ %amount @ " " @ 0 @ 1;
        %this.autoMoveList = -(%amount) @ " " @ "0 0" @ 2;
        %this.autoMoveList = 0 @ " " @ -(%amount) @ " " @ 0 @ 3;
        %this.autoMoveNum = 0;
    }
    %move = %this.autoMoveList;
    %this.autoMoveNum;
    %this.applyImpulse("0 0 0", %move);
    %this.autoMoveNum = (1.0 + %this.autoMoveNum);
    if ((3.0 > %this.autoMoveNum)) {
        %this.autoMoveNum = 0;
    }
};
function AIManager::doBotsSurfing(%this, %periodMS) {
    %secondsBetweenLeaveOrEntry = 3;
    %minBots = 20;
    %maxBots = 70;
    if ((getRandom(0, (%secondsBetweenLeaveOrEntry * 1000.0)) < %periodMS)) {
        return;
    }
    %add = 1;
    if ((%minBots <= %this.numBots)) {
        %add = 1;
    }
    if ((%maxBots >= %this.numBots)) {
        %add = 0;
    }
    %add = getRandom(0, 1);
    if ((1.0 == %add)) {
        %this.addOneBot();
    }
    %this.delOneBot();
    echo("NumBots is now" @ " " @ %this.numBots);
};
function AIManager::addOneBot(%this) {
    echo("adding one bot...");
    EntrySpawn.spawnBots(1, 1);
};
function AIManager::delOneBot(%this) {
    if ((0.0 <= %this.numBots)) {
        return;
    }
    echo("removing one bot...");
    %this.numBots = (1.0 - %this.numBots);
    %this.numBots.delete(%this.bots);
};
function AIManager::think(%this) {
    %period = 500;
    %i = 0;
    if ((%this.numBots < %i)) {
        %i.thinkETS(%this.bots, %period);
        %i = (1.0 + %i);
    }
    if (%this.BotsSurfing) {
        %this.doBotsSurfing(%period);
    }
    %this.schedule(%period);
};
function AIManager::spawn(%this) {
    %BotRows = 10;
    %BotCols = 5;
    %XPosition = -(288.0);
    %YPosition = -(410.0);
    %j = 0;
    if ((%BotRows < %j)) {
        %i = 0;
        if ((%BotCols < %i)) {
            %this.numBots = (1.0 + %this.numBots);
            %player = AIPlayer::spawn("hi!" @ " " @ %this.numBots, %XPosition @ " " @ %YPosition @ " " @ "216 0 0 1 3.14");
            %XPosition = (1.0 + %XPosition);
            %i = (1.0 + %i);
        }
        %YPosition = (1.0 + %YPosition);
        (%BotCols < %i);
        %XPosition = ((%BotCols * 1.0) - %XPosition);
        if ((1.0 == (2 % %YPosition))) {
            %XPosition = (0.5 - %XPosition);
        }
        %XPosition = (0.5 + %XPosition);
        %j = (1.0 + %j);
    }
    return %player;
};
function AIManager::SpawnETS(%this, %transform) {
    if (!(%this.numBots)) {
        %this.numBots = 0;
    }
    %name = "";
    %name = "bot" @ (1.0 + %this.numBots);
    %player = AIPlayer::spawn(%name, %transform);
    %this.bots = %player @ %this.numBots;
    %this.numBots = (1.0 + %this.numBots);
    %player.gender = getSubStr(%player.getDataBlock().possibleGenders, 0, 1);
    %player.randomizeOutfit();
    %rand = getRandom(0, 2);
    %player.setGenre(getSubStr(%player.getDataBlock().possibleGenres, %rand, 1));
    %player.botChatTarget = 0;
    %player.setAwayMessage(getRandomAwayMessage());
    PlayerDict.put(%name, %player);
    %player.MeshOff(%player.gender @ ".headphones.dj");
    echo("bot entered:   \x03" @ " " @ getDebugString(%player));
    return %player;
};
function AIManager::SpawnArmyETS(%this, %transform) {
    %delt = 1.5;
    %BotRows = 3;
    %BotCols = 3;
    %posX = ((mFloor((%BotCols * 0.5)) * %delt) - getWord(%transform, 0));
    %posY = ((mFloor((%BotRows * 0.5)) * %delt) - getWord(%transform, 1));
    %posZ = (3.0 + getWord(%transform, 2));
    %j = 0;
    if ((%BotRows < %j)) {
        %i = 0;
        if ((%BotCols < %i)) {
            %trans = %posX @ " " @ %posY @ " " @ %posZ @ " " @ getWords(%transform, 3, 6);
            if ((mFloor((2.0 / %BotCols)) != %i)) {
            }
            if ((mFloor((2.0 / %BotRows)) != %j)) {
                %this.SpawnETS(%trans);
            }
            %posX = (%delt + %posX);
            %i = (1.0 + %i);
        }
        %posY = (%delt + %posY);
        (%BotCols < %i);
        %posX = ((%BotCols * %delt) - %posX);
        %j = (1.0 + %j);
    }
};
function AIManager::RandomizeBots(%this) {
    %i = 0;
    if ((%this.numBots < %i)) {
        %i.randomizeOutfit(%this.bots);
        %rand = getRandom(0, 2);
        %i.setGenre(%this.bots, getSubStr(%i.getDataBlock(%this.bots).possibleGenres, %rand, 1));
        %i.setAwayMessage(%this.bots, getRandomAwayMessage());
        %i = (1.0 + %i);
    }
};
function ServerCmdNextToonModeBots(%client) {
    %i = 0;
    if ((%this.numBots < %i)) {
        %clientBotID = %client.getGhostID(%i @ AIManager, %this.bots);
        AIManager;
        commandToClient(%client, 'matchToonModeToPlayer', %clientBotID);
        %i = (1.0 + %i);
    }
};
function AIManager::BotsStress(%this, %val) {
    %this.BotsMove = %val;
    %this.BotsBlahBlah = %val;
    %this.BotsEavesdrop = %val;
    %this.BotsSurfing = %val;
};
$randomAwayMessagesNum = 0;
function getRandomAwayMessage() {
    if ((0.0 == $randomAwayMessagesNum)) {
        $randomAwayMessagesNum[$randomAwayMessage @ $randomAwayMessagesNum] = "I'm cooking dinner.";
        $randomAwayMessagesNum = (1.0 + $randomAwayMessagesNum);
        $randomAwayMessagesNum[$randomAwayMessage @ $randomAwayMessagesNum] = "I'm on a date.";
        $randomAwayMessagesNum = (1.0 + $randomAwayMessagesNum);
        $randomAwayMessagesNum[$randomAwayMessage @ $randomAwayMessagesNum] = "away.";
        $randomAwayMessagesNum = (1.0 + $randomAwayMessagesNum);
        $randomAwayMessagesNum[$randomAwayMessage @ $randomAwayMessagesNum] = "lost.";
        $randomAwayMessagesNum = (1.0 + $randomAwayMessagesNum);
        $randomAwayMessagesNum[$randomAwayMessage @ $randomAwayMessagesNum] = $Pref::Player::defaultAwayMessage;
        $randomAwayMessagesNum = (1.0 + $randomAwayMessagesNum);
    }
    return;
};
