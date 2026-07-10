datablock PlayerData(DemoPlayer : PlayerM) {
    shootingDelay = 2000;
};
function DemoPlayer::onReachDestination(%this, %obj) {
    if (!(%obj.Path $= "")) {
        if ((%obj.currentNode == %obj.targetNode)) {
            %this.onEndOfPath(%obj, %obj.Path);
        } else {
            %obj.moveToNextNode();
        }
    }
};
function DemoPlayer::onEndOfPath(%this, %obj, %path) {
    %obj.nextTask();
};
function DemoPlayer::onEndSequence(%this, %obj, %slot) {
    echo("Sequence Done!");
    %obj.stopThread(%slot);
    %obj.nextTask();
};
function AIPlayer::spawn(%name, %spawnPoint) {
    if ((getRandom(0, 1) == 0.0)) {
        %botDB = PlayerM;
    } else {
        %botDB = PlayerF;
    }
    %player = new AIPlayer("") {
        dataBlock = %botDB;
        Path = "";
    };
    MissionCleanup.add(%player);
    %player.setShapeName(%name);
    %player.setTransform(%spawnPoint);
    %rand = getRandom(0, 2);
    if ((%rand == 0.0)) {
        %genre = "h";
    } else {
        if ((%rand == 1.0)) {
            %genre = "i";
        } else {
            if ((%rand == 2.0)) {
                %genre = "p";
            }
        }
    }
    %player.setGenre(%genre);
    return %player;
};
function AIPlayer::spawnOnPath(%name, %path) {
    if (!isObject(%path)) {
        return;
    }
    %node = %path.getObject(0);
    %player = AIPlayer::spawn(%name, %node.getTransform());
    return %player;
};
function AIPlayer::followPath(%this, %path, %node) {
    %this.stopThread(0);
    if (!isObject(%path)) {
        %this.Path = "";
        return;
    }
    if ((%node > (%path.getCount() - 1.0))) {
        %this.targetNode = (%path.getCount() - 1.0);
    } else {
        %this.targetNode = %node;
    }
    if ((%this.Path $= %path)) {
        %this.moveToNode(%this.currentNode);
    } else {
        %this.Path = %path;
        %this.moveToNode(0);
    }
};
function AIPlayer::moveToNextNode(%this) {
    if ((%this.targetNode < 0.0) || (%this.currentNode < %this.targetNode)) {
        if ((%this.currentNode < (%this.Path.getCount() - 1.0))) {
            %this.moveToNode((%this.currentNode + 1.0));
        } else {
            %this.moveToNode(0);
        }
    } else {
        if ((%this.currentNode == 0.0)) {
            %this.moveToNode((%this.Path.getCount() - 1.0));
        } else {
            %this.moveToNode((%this.currentNode - 1.0));
        }
    }
};
function AIPlayer::moveToNode(%this, %index) {
    %this.currentNode = %index;
    %node = %this.Path.getObject(%index);
    %this.setMoveDestination(%node.getTransform(), (%index == %this.targetNode));
};
function AIPlayer::pushTask(%this, %method) {
    if ((%this.taskIndex $= "")) {
        %this.taskIndex = 0;
        %this.taskCurrent = -(1.0);
    }
    %this.task[%this.taskIndex] = %method;
    %this.taskIndex = (%this.taskIndex + 1.0);
    if ((%this.taskCurrent == -(1.0))) {
        %this.executeTask((%this.taskIndex - 1.0));
    }
};
function AIPlayer::clearTasks(%this) {
    %this.taskIndex = 0;
    %this.taskCurrent = -(1.0);
};
function AIPlayer::nextTask(%this) {
    if ((%this.taskCurrent != -(1.0))) {
        if ((%this.taskCurrent < (%this.taskIndex - 1.0))) {
            %this.taskCurrent = (%this.taskCurrent + 1.0);
            %this.executeTask();
        } else {
            %this.taskCurrent = -(1.0);
        }
    }
};
function AIPlayer::executeTask(%this, %index) {
    %this.taskCurrent = %index;
    eval(%this.getId() @ "." @ %this.task[%index] @ ";");
};
function AIPlayer::singleShot(%this) {
    %this.setImageTrigger(0, 1);
    %this.setImageTrigger(0, 0);
    %this.Trigger = %this.schedule(%this.shootingDelay, singleShot);
};
function AIPlayer::wait(%this, %time) {
    %this.schedule((%time * 1000.0), "nextTask");
};
function AIPlayer::done(%this, %time) {
    %this.schedule(0, "delete");
};
function AIPlayer::fire(%this, %bool) {
    if (%bool) {
        cancel(%this.Trigger);
        %this.singleShot();
    } else {
        cancel(%this.Trigger);
    }
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
    if ((%periodMS > getRandom(0, (1000.0 * %secondsBetweenEmotes)))) {
    }
    if ((%this.getMoveState() $= "stop")) {
        %this.doRandomEmote();
    }
    if (AIManager.BotsMove && (%periodMS > getRandom(0, (1000.0 * %secondsBetweenMoves)))) {
        %this.doAutoMoveEntry();
    }
    if (AIManager.BotsBlahBlah && (%periodMS > getRandom(0, (1000.0 * %secondsBetweenWords)))) {
        %this.doBlahBlah(%this.botEavesdropTarget);
    }
    if (AIManager.BotsEavesdrop && (%periodMS > getRandom(0, (1000.0 * %secondsBetweenEavesdrops)))) {
        %this.doEavesdropChange();
    }
};
function AIPlayer::doBlahBlah(%this, %target) {
    if ((AIManager.blahblahsNum == 0.0)) {
        AIManager.blahblahsNum = 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = "i am a bot !";
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs[AIManager.blahblahsNum] = "do you like cheese ?";
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs[AIManager.blahblahsNum] = ".. yeah.";
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs[AIManager.blahblahsNum] = "dancing is the BEST.";
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs[AIManager.blahblahsNum] = "ASL ?";
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs[AIManager.blahblahsNum] = "let's go dance.";
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs[AIManager.blahblahsNum] = "cool!";
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs[AIManager.blahblahsNum] = ";)";
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs[AIManager.blahblahsNum] = "this rocks.";
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs[AIManager.blahblahsNum] = "where's the party ?";
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs[AIManager.blahblahsNum] = "yawn zzzz..";
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs[AIManager.blahblahsNum] = "nice outfit.";
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs[AIManager.blahblahsNum] = "will you be my friend ?";
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs[AIManager.blahblahsNum] = "lol !";
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs[AIManager.blahblahsNum] = "rotfl !";
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs[AIManager.blahblahsNum] = "flirt";
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahsNum = (AIManager.blahblahsNum - 1.0);
    }
    %num = getRandom(1, AIManager.blahblahsNum);
    %msg = AIManager.blahblahs[%num];
    ServersideChatMessage(%this, %target, %msg);
    CONV_DEBUG("Bot" @ " " @ %this @ " " @ "said" @ " " @ %msg @ " " @ "(" @ " " @ %num @ " " @ "/" @ " " @ AIManager.blahblahsNum @ " " @ ")");
};
function AIManager::BotsBlahBlahOneShot(%this, %player) {
    %i = 0;
    while ((%i < %this.numBots)) {
        %this.bots[%i].doBlahBlah(%player);
        %i = (%i + 1.0);
    }
};
function AIPlayer::doEavesdropChange(%this) {
    %newEavesdropTarget = AIManager.bots[getRandom(0, (AIManager.numBots - 1.0))];
    if ((%newEavesdropTarget == %this)) {
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
    if ((%this.autoMoveList[0] == 0.0)) {
        %this.autoMoveList[" ","0 0",0] = %amount;
        %this.autoMoveList[" ",%amount," ",0,1] = 0;
        %this.autoMoveList[" ","0 0",2] = -(%amount);
        %this.autoMoveList[" ",-(%amount)," ",0,3] = 0;
        %this.autoMoveNum = 0;
    }
    %move = %this.autoMoveList[%this.autoMoveNum];
    %this.applyImpulse("0 0 0", %move);
    %this.autoMoveNum = (%this.autoMoveNum + 1.0);
    if ((%this.autoMoveNum > 3.0)) {
        %this.autoMoveNum = 0;
    }
};
function AIManager::doBotsSurfing(%this, %periodMS) {
    %secondsBetweenLeaveOrEntry = 3;
    %minBots = 20;
    %maxBots = 70;
    if ((%periodMS < getRandom(0, (1000.0 * %secondsBetweenLeaveOrEntry)))) {
        return;
    }
    %add = 1;
    if ((%this.numBots <= %minBots)) {
        %add = 1;
    } else {
        if ((%this.numBots >= %maxBots)) {
            %add = 0;
        } else {
            %add = getRandom(0, 1);
        }
    }
    if ((%add == 1.0)) {
        %this.addOneBot();
    } else {
        %this.delOneBot();
    }
    echo("NumBots is now" @ " " @ %this.numBots);
};
function AIManager::addOneBot(%this) {
    echo("adding one bot...");
    EntrySpawn.spawnBots(1, 1);
};
function AIManager::delOneBot(%this) {
    if ((%this.numBots <= 0.0)) {
        return;
    }
    echo("removing one bot...");
    %this.numBots = (%this.numBots - 1.0);
    %this.bots[%this.numBots].delete();
};
function AIManager::think(%this) {
    %period = 500;
    %i = 0;
    while ((%i < %this.numBots)) {
        %this.bots[%i].thinkETS(%period);
        %i = (%i + 1.0);
    }
    if (%this.BotsSurfing) {
        %this.doBotsSurfing(%period);
    }
    %this.schedule(%period, think);
};
function AIManager::spawn(%this) {
    %BotRows = 10;
    %BotCols = 5;
    %XPosition = -(288.0);
    %YPosition = -(410.0);
    %j = 0;
    while ((%j < %BotRows)) {
        %i = 0;
        while ((%i < %BotCols)) {
            %this.numBots = (%this.numBots + 1.0);
            %player = AIPlayer::spawn("hi!" @ " " @ %this.numBots, %XPosition @ " " @ %YPosition @ " " @ "216 0 0 1 3.14");
            %XPosition = (%XPosition + 1.0);
            %i = (%i + 1.0);
        }
        %YPosition = (%YPosition + 1.0);
        (%i < %BotCols);
        %XPosition = (%XPosition - (1.0 * %BotCols));
        if (((%YPosition % 2) == 1.0)) {
            %XPosition = (%XPosition - 0.5);
        } else {
            %XPosition = (%XPosition + 0.5);
        }
        %j = (%j + 1.0);
    }
    return %player;
};
function AIManager::SpawnETS(%this, %transform) {
    if (!%this.numBots) {
        %this.numBots = 0;
    }
    %name = "";
    %name = "bot" @ (%this.numBots + 1.0);
    %player = AIPlayer::spawn(%name, %transform);
    %this.bots[%this.numBots] = %player;
    %this.numBots = (%this.numBots + 1.0);
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
    %posX = (getWord(%transform, 0) - (%delt * mFloor((0.5 * %BotCols))));
    %posY = (getWord(%transform, 1) - (%delt * mFloor((0.5 * %BotRows))));
    %posZ = (getWord(%transform, 2) + 3.0);
    %j = 0;
    while ((%j < %BotRows)) {
        %i = 0;
        while ((%i < %BotCols)) {
            %trans = %posX @ " " @ %posY @ " " @ %posZ @ " " @ getWords(%transform, 3, 6);
            if ((%i != mFloor((%BotCols / 2.0))) || (%j != mFloor((%BotRows / 2.0)))) {
                %this.SpawnETS(%trans);
            }
            %posX = (%posX + %delt);
            %i = (%i + 1.0);
        }
        %posY = (%posY + %delt);
        (%i < %BotCols);
        %posX = (%posX - (%delt * %BotCols));
        %j = (%j + 1.0);
    }
};
function AIManager::RandomizeBots(%this) {
    %i = 0;
    while ((%i < %this.numBots)) {
        %this.bots[%i].randomizeOutfit();
        %rand = getRandom(0, 2);
        %this.bots[%i].setGenre(getSubStr(%this.bots[%i].getDataBlock().possibleGenres, %rand, 1));
        %this.bots[%i].setAwayMessage(getRandomAwayMessage());
        %i = (%i + 1.0);
    }
};
function ServerCmdNextToonModeBots(%client) {
    %i = 0;
    while ((%i < AIManager.numBots)) {
        %clientBotID = %client.getGhostID(AIManager.bots[%i]);
        commandToClient(%client, 'matchToonModeToPlayer', %clientBotID);
        %i = (%i + 1.0);
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
    if (($randomAwayMessagesNum == 0.0)) {
        $randomAwayMessage[$randomAwayMessagesNum] = "I'm cooking dinner.";
        $randomAwayMessagesNum = ($randomAwayMessagesNum + 1.0);
        $randomAwayMessage[$randomAwayMessagesNum] = "I'm on a date.";
        $randomAwayMessagesNum = ($randomAwayMessagesNum + 1.0);
        $randomAwayMessage[$randomAwayMessagesNum] = "away.";
        $randomAwayMessagesNum = ($randomAwayMessagesNum + 1.0);
        $randomAwayMessage[$randomAwayMessagesNum] = "lost.";
        $randomAwayMessagesNum = ($randomAwayMessagesNum + 1.0);
        $randomAwayMessage[$randomAwayMessagesNum] = $Pref::Player::defaultAwayMessage;
        $randomAwayMessagesNum = ($randomAwayMessagesNum + 1.0);
    }
    return $randomAwayMessage[getRandom(0, ($randomAwayMessagesNum - 1.0))];
};
