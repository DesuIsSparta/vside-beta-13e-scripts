datablock PlayerData(DemoPlayer : PlayerM) {
    shootingDelay = 2000;
};
function DemoPlayer::onReachDestination(%this, %obj) {
    if (!(%obj.Path $= "")) {
        if ((%obj.currentNode == %obj.targetNode)) {
            %obj.Path.onEndOfPath(%this, %obj);
        }
        %obj.moveToNextNode();
    }
};
function DemoPlayer::onEndOfPath(%this, %obj, %path) {
    %obj.nextTask();
};
function DemoPlayer::onEndSequence(%this, %obj, %slot) {
    echo("Sequence Done!");
    %slot.stopThread(%obj);
    %obj.nextTask();
};
function AIPlayer::spawn(%name, %spawnPoint) {
    if ((getRandom(0, 1) == 0.0)) {
        %botDB = PlayerM;
    }
    %botDB = PlayerF;
    %player = new AIPlayer("") {
        dataBlock = %botDB;
        Path = "";
    };
    %player.add(MissionCleanup);
    %name.setShapeName(%player);
    %spawnPoint.setTransform(%player);
    %rand = getRandom(0, 2);
    if ((%rand == 0.0)) {
        %genre = "h";
    }
    if ((%rand == 1.0)) {
        %genre = "i";
    }
    if ((%rand == 2.0)) {
        %genre = "p";
    }
    %genre.setGenre(%player);
    return %player;
};
function AIPlayer::spawnOnPath(%name, %path) {
    if (!(isObject(%path))) {
        return;
    }
    %node = 0.getObject(%path);
    %player = AIPlayer::spawn(%name, %node.getTransform());
    return %player;
};
function AIPlayer::followPath(%this, %path, %node) {
    0.stopThread(%this);
    if (!(isObject(%path))) {
        %this.Path = "";
        return;
    }
    if ((%node > (%path.getCount() - 1.0))) {
        %this.targetNode = (%path.getCount() - 1.0);
    }
    %this.targetNode = %node;
    if ((%this.Path $= %path)) {
        %this.currentNode.moveToNode(%this);
    }
    %this.Path = %path;
    0.moveToNode(%this);
};
function AIPlayer::moveToNextNode(%this) {
    if ((%this.targetNode < 0.0)) {
    }
    if ((%this.currentNode < %this.targetNode)) {
        if ((%this.currentNode < (%this.Path.getCount() - 1.0))) {
            (%this.currentNode + 1.0).moveToNode(%this);
        }
        0.moveToNode(%this);
    }
    if ((%this.currentNode == 0.0)) {
        (%this.Path.getCount() - 1.0).moveToNode(%this);
    }
    (%this.currentNode - 1.0).moveToNode(%this);
};
function AIPlayer::moveToNode(%this, %index) {
    %this.currentNode = %index;
    %node = %index.getObject(%this.Path);
    (%index == %this.targetNode).setMoveDestination(%this, %node.getTransform());
};
function AIPlayer::pushTask(%this, %method) {
    if ((%this.taskIndex $= "")) {
        %this.taskIndex = 0;
        %this.taskCurrent = -(1.0);
    }
    %this.task = %method @ %this.taskIndex;
    %this.taskIndex = (%this.taskIndex + 1.0);
    if ((%this.taskCurrent == -(1.0))) {
        (%this.taskIndex - 1.0).executeTask(%this);
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
            .executeTask(%this);
        }
        %this.taskCurrent = -(1.0);
    }
};
function AIPlayer::executeTask(%this, %index) {
    %this.taskCurrent = %index;
    eval(%this.getId() @ "." @ %index @ %this.task @ ";");
};
function AIPlayer::singleShot(%this) {
    1.setImageTrigger(%this, 0);
    0.setImageTrigger(%this, 0);
    %this.Trigger = singleShot.schedule(%this, %this.shootingDelay);
};
function AIPlayer::wait(%this, %time) {
    "nextTask".schedule(%this, (%time * 1000.0));
};
function AIPlayer::done(%this, %time) {
    "delete".schedule(%this, 0);
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
    %object.setAimObject(%this);
    %this.nextTask();
};
function AIPlayer::animate(%this, %seq) {
    %seq.setActionThread(%this);
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
        %this.botEavesdropTarget.doBlahBlah(%this);
    }
    if (AIManager.BotsEavesdrop && (%periodMS > getRandom(0, (1000.0 * %secondsBetweenEavesdrops)))) {
        %this.doEavesdropChange();
    }
};
function AIPlayer::doBlahBlah(%this, %target) {
    if ((AIManager.blahblahsNum == 0.0)) {
        AIManager.blahblahsNum = 1;
        AIManager.blahblahs = "i am a bot !" @ AIManager.blahblahsNum;
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs = "do you like cheese ?" @ AIManager.blahblahsNum;
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs = ".. yeah." @ AIManager.blahblahsNum;
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs = "dancing is the BEST." @ AIManager.blahblahsNum;
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs = "ASL ?" @ AIManager.blahblahsNum;
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs = "let's go dance." @ AIManager.blahblahsNum;
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs = "cool!" @ AIManager.blahblahsNum;
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs = ";)" @ AIManager.blahblahsNum;
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs = "this rocks." @ AIManager.blahblahsNum;
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs = "where's the party ?" @ AIManager.blahblahsNum;
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs = "yawn zzzz.." @ AIManager.blahblahsNum;
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs = "nice outfit." @ AIManager.blahblahsNum;
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs = "will you be my friend ?" @ AIManager.blahblahsNum;
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs = "lol !" @ AIManager.blahblahsNum;
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs = "rotfl !" @ AIManager.blahblahsNum;
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahs = "flirt" @ AIManager.blahblahsNum;
        AIManager.blahblahsNum = (AIManager.blahblahsNum + 1.0);
        AIManager.blahblahsNum = (AIManager.blahblahsNum - 1.0);
    }
    %num = getRandom(1, AIManager.blahblahsNum);
    %msg = AIManager.blahblahs;
    %num;
    ServersideChatMessage(%this, %target, %msg);
    CONV_DEBUG("Bot" @ " " @ %this @ " " @ "said" @ " " @ %msg @ " " @ "(" @ " " @ %num @ " " @ "/" @ " " @ AIManager.blahblahsNum @ " " @ ")");
};
function AIManager::BotsBlahBlahOneShot(%this, %player) {
    %i = 0;
    while ((%i < %this.numBots)) {
        %player.doBlahBlah(%i, %this.bots);
        %i = (%i + 1.0);
    }
};
function AIPlayer::doEavesdropChange(%this) {
    %newEavesdropTarget = AIManager.bots;
    getRandom(0, (AIManager.numBots - 1.0));
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
    if ((%this.autoMoveList == 0.0 @ 0)) {
        %this.autoMoveList = %amount @ " " @ "0 0" @ 0;
        %this.autoMoveList = 0 @ " " @ %amount @ " " @ 0 @ 1;
        %this.autoMoveList = -(%amount) @ " " @ "0 0" @ 2;
        %this.autoMoveList = 0 @ " " @ -(%amount) @ " " @ 0 @ 3;
        %this.autoMoveNum = 0;
    }
    %move = %this.autoMoveList;
    %this.autoMoveNum;
    %move.applyImpulse(%this, "0 0 0");
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
    }
    if ((%this.numBots >= %maxBots)) {
        %add = 0;
    }
    %add = getRandom(0, 1);
    if ((%add == 1.0)) {
        %this.addOneBot();
    }
    %this.delOneBot();
    echo("NumBots is now" @ " " @ %this.numBots);
};
function AIManager::addOneBot(%this) {
    echo("adding one bot...");
    1.spawnBots(EntrySpawn, 1);
};
function AIManager::delOneBot(%this) {
    if ((%this.numBots <= 0.0)) {
        return;
    }
    echo("removing one bot...");
    %this.numBots = (%this.numBots - 1.0);
    %this.bots.delete(%this.numBots);
};
function AIManager::think(%this) {
    %period = 500;
    %i = 0;
    while ((%i < %this.numBots)) {
        %period.thinkETS(%i, %this.bots);
        %i = (%i + 1.0);
    }
    if (%this.BotsSurfing) {
        %period.doBotsSurfing(%this);
    }
    think.schedule(%this, %period);
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
        }
        %XPosition = (%XPosition + 0.5);
        %j = (%j + 1.0);
    }
    return %player;
};
function AIManager::SpawnETS(%this, %transform) {
    if (!(%this.numBots)) {
        %this.numBots = 0;
    }
    %name = "";
    %name = "bot" @ (%this.numBots + 1.0);
    %player = AIPlayer::spawn(%name, %transform);
    %this.bots = %player @ %this.numBots;
    %this.numBots = (%this.numBots + 1.0);
    %player.gender = getSubStr(%player.getDataBlock().possibleGenders, 0, 1);
    %player.randomizeOutfit();
    %rand = getRandom(0, 2);
    getSubStr(%player.getDataBlock().possibleGenres, %rand, 1).setGenre(%player);
    %player.botChatTarget = 0;
    getRandomAwayMessage().setAwayMessage(%player);
    %player.put(PlayerDict, %name);
    %player.gender @ ".headphones.dj".MeshOff(%player);
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
            if ((%i != mFloor((%BotCols / 2.0)))) {
            }
            if ((%j != mFloor((%BotRows / 2.0)))) {
                %trans.SpawnETS(%this);
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
        %this.bots.randomizeOutfit(%i);
        %rand = getRandom(0, 2);
        getSubStr(%this.bots.getDataBlock(%i).possibleGenres, %rand, 1).setGenre(%i, %this.bots);
        getRandomAwayMessage().setAwayMessage(%i, %this.bots);
        %i = (%i + 1.0);
    }
};
function ServerCmdNextToonModeBots(%client) {
    %i = 0;
    while ((%i < AIManager.numBots)) {
        %clientBotID = AIManager.bots.getGhostID(%client, %i);
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
        $randomAwayMessagesNum[$randomAwayMessage @ $randomAwayMessagesNum] = "I'm cooking dinner.";
        $randomAwayMessagesNum = ($randomAwayMessagesNum + 1.0);
        $randomAwayMessagesNum[$randomAwayMessage @ $randomAwayMessagesNum] = "I'm on a date.";
        $randomAwayMessagesNum = ($randomAwayMessagesNum + 1.0);
        $randomAwayMessagesNum[$randomAwayMessage @ $randomAwayMessagesNum] = "away.";
        $randomAwayMessagesNum = ($randomAwayMessagesNum + 1.0);
        $randomAwayMessagesNum[$randomAwayMessage @ $randomAwayMessagesNum] = "lost.";
        $randomAwayMessagesNum = ($randomAwayMessagesNum + 1.0);
        $randomAwayMessagesNum[$randomAwayMessage @ $randomAwayMessagesNum] = $Pref::Player::defaultAwayMessage;
        $randomAwayMessagesNum = ($randomAwayMessagesNum + 1.0);
    }
    return $randomAwayMessage[getRandom(0, ($randomAwayMessagesNum - 1.0))];
};
