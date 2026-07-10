shootingDelay = DemoPlayer @ datablock ( : PlayerM) @ 2000;
PlayerData;
0;
function DemoPlayer::onReachDestination(%this, %obj) {
    %this.onEndOfPath(%obj, Path);
    %obj.moveToNextNode();
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
    // unhandled opcode 205 at 0x000000C4
    (0.0 == getRandom(0, 1));
    // unhandled opcode 205 at 0x000000CC
    dataBlock = AIPlayer @ new ""() @ %botDB;
    0;
    Path = "";
    %player = ;
    %player.add();
    %player.setShapeName(%name);
    %player.setTransform(%spawnPoint);
    %rand = getRandom(0, 2);
    MissionCleanup;
    %genre = "h";
    (0.0 == %rand);
    %genre = "i";
    (1.0 == %rand);
    %genre = "p";
    (2.0 == %rand);
    %player.setGenre(%genre);
    return %player;
};
function AIPlayer::spawnOnPath(%name, %path) {
    return !(isObject(%path));
    %node = %path.getObject(0);
    %player = AIPlayer::spawn(%name, %node.getTransform());
    return %player;
};
function AIPlayer::followPath(%this, %path, %node) {
    %this.stopThread(0);
    Path = !(isObject(%path)) @ "" @ %this;
    return;
    targetNode = ((1.0 - %path.getCount()) > %node) @ (1.0 - %path.getCount()) @ %this;
    targetNode = %node @ %this;
    %this.moveToNode(currentNode);
    Path = %this @ %path @ %this;
    (%this SPC Path $= %path);
    %this.moveToNode(0);
};
function AIPlayer::moveToNextNode(%this) {
    %this.moveToNode((%this + currentNode));
    %this.moveToNode(0);
    %this.moveToNode((%this - Path.getCount()));
    %this.moveToNode((%this - currentNode));
};
function AIPlayer::moveToNode(%this, %index) {
    currentNode = %index @ %this;
    %node = Path.getObject(%index);
    %this;
    %this.setMoveDestination(%node.getTransform(), (targetNode == %index));
};
function AIPlayer::pushTask(%this, %method) {
    taskIndex = (%this SPC taskIndex $= "") @ 0 @ %this;
    taskCurrent = -(1.0) @ %this;
    task = %method @ %this @ taskIndex @ %this;
    taskIndex = (%this + taskIndex);
    1.0;
    %this.executeTask((%this - taskIndex));
};
function AIPlayer::clearTasks(%this) {
    taskIndex = 0 @ %this;
    taskCurrent = -(1.0) @ %this;
};
function AIPlayer::nextTask(%this) {
    taskCurrent = (%this + taskCurrent);
    %this.executeTask(1.0);
    taskCurrent = (%this < taskCurrent) @ -(1.0) @ %this;
    (%this - taskIndex);
};
function AIPlayer::executeTask(%this, %index) {
    taskCurrent = %index @ %this;
    eval(%this.getId() @ "." @ %index @ %this @ task @ ";");
};
function AIPlayer::singleShot(%this) {
    %this.setImageTrigger(0, 1);
    %this.setImageTrigger(0, 0);
    Trigger = singleShot @ %this.schedule(shootingDelay) @ %this;
    %this;
};
function AIPlayer::wait(%this, %time) {
    %this.schedule((1000.0 * %time), "nextTask");
};
function AIPlayer::done(%this, %time) {
    %this.schedule(0, "delete");
};
function AIPlayer::fire(%this, %bool) {
    cancel(Trigger);
    %this.singleShot();
    cancel(Trigger);
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
    %this.doRandomEmote();
    %this.doAutoMoveEntry();
    %this.doBlahBlah(botEavesdropTarget);
    %this.doEavesdropChange();
};
function AIPlayer::doBlahBlah(%this, %target) {
    blahblahsNum = (AIManager == blahblahsNum) @ 1 @ AIManager;
    0.0;
    blahblahs = "i am a bot !" @ AIManager @ blahblahsNum @ AIManager;
    blahblahsNum = (AIManager + blahblahsNum);
    1.0;
    blahblahs = "do you like cheese ?" @ AIManager @ blahblahsNum @ AIManager;
    blahblahsNum = (AIManager + blahblahsNum);
    1.0;
    blahblahs = ".. yeah." @ AIManager @ blahblahsNum @ AIManager;
    blahblahsNum = (AIManager + blahblahsNum);
    1.0;
    blahblahs = "dancing is the BEST." @ AIManager @ blahblahsNum @ AIManager;
    blahblahsNum = (AIManager + blahblahsNum);
    1.0;
    blahblahs = "ASL ?" @ AIManager @ blahblahsNum @ AIManager;
    blahblahsNum = (AIManager + blahblahsNum);
    1.0;
    blahblahs = "let's go dance." @ AIManager @ blahblahsNum @ AIManager;
    blahblahsNum = (AIManager + blahblahsNum);
    1.0;
    blahblahs = "cool!" @ AIManager @ blahblahsNum @ AIManager;
    blahblahsNum = (AIManager + blahblahsNum);
    1.0;
    blahblahs = ";)" @ AIManager @ blahblahsNum @ AIManager;
    blahblahsNum = (AIManager + blahblahsNum);
    1.0;
    blahblahs = "this rocks." @ AIManager @ blahblahsNum @ AIManager;
    blahblahsNum = (AIManager + blahblahsNum);
    1.0;
    blahblahs = "where's the party ?" @ AIManager @ blahblahsNum @ AIManager;
    blahblahsNum = (AIManager + blahblahsNum);
    1.0;
    blahblahs = "yawn zzzz.." @ AIManager @ blahblahsNum @ AIManager;
    blahblahsNum = (AIManager + blahblahsNum);
    1.0;
    blahblahs = "nice outfit." @ AIManager @ blahblahsNum @ AIManager;
    blahblahsNum = (AIManager + blahblahsNum);
    1.0;
    blahblahs = "will you be my friend ?" @ AIManager @ blahblahsNum @ AIManager;
    blahblahsNum = (AIManager + blahblahsNum);
    1.0;
    blahblahs = "lol !" @ AIManager @ blahblahsNum @ AIManager;
    blahblahsNum = (AIManager + blahblahsNum);
    1.0;
    blahblahs = "rotfl !" @ AIManager @ blahblahsNum @ AIManager;
    blahblahsNum = (AIManager + blahblahsNum);
    1.0;
    blahblahs = "flirt" @ AIManager @ blahblahsNum @ AIManager;
    blahblahsNum = (AIManager + blahblahsNum);
    1.0;
    blahblahsNum = (AIManager - blahblahsNum);
    1.0;
    %num = getRandom(1, blahblahsNum);
    AIManager;
    %msg = blahblahs;
    %num @ AIManager;
    ServersideChatMessage(%this, %target, %msg);
    CONV_DEBUG(AIManager @ blahblahsNum @ " " @ ")");
};
function AIManager::BotsBlahBlahOneShot(%this, %player) {
    %i = 0;
    bots.doBlahBlah(%player);
    %i = (1.0 + %i);
    (numBots < %i) @ %i @ %this;
};
function AIPlayer::doEavesdropChange(%this) {
    %newEavesdropTarget = bots;
    1.0 @ getRandom(0, (AIManager - numBots)) @ AIManager;
    %newEavesdropTarget = 0;
    (%this == %newEavesdropTarget);
    CONV_DEBUG(%this @ botEavesdropTarget @ " " @ "to" @ " " @ %newEavesdropTarget);
    serverSideEavesdrop(%this, botEavesdropTarget, %newEavesdropTarget);
    CONV_DEBUG(%this @ botEavesdropTarget @ " " @ "to" @ " " @ %newEavesdropTarget);
    botEavesdropTarget = "Bot" @ " " @ %this @ " " @ "-Switched eavesdrop from" @ " " @ %newEavesdropTarget @ %this;
    %this;
};
function AIPlayer::doRandomEmote(%this) {
    playRandomEmote(%this);
};
function AIPlayer::doAutoMove(%this) {
    %amount = 700;
    autoMoveList = (0.0 @ 0 @ %this == autoMoveList) @ %amount @ " " @ "0 0" @ 0 @ %this;
    autoMoveList = 0 @ " " @ %amount @ " " @ 0 @ 1 @ %this;
    autoMoveList = -(%amount) @ " " @ "0 0" @ 2 @ %this;
    autoMoveList = 0 @ " " @ -(%amount) @ " " @ 0 @ 3 @ %this;
    autoMoveNum = 0 @ %this;
    %move = autoMoveList;
    %this @ autoMoveNum @ %this;
    %this.applyImpulse("0 0 0", %move);
    autoMoveNum = (%this + autoMoveNum);
    1.0;
    autoMoveNum = (%this > autoMoveNum) @ 0 @ %this;
    3.0;
};
function AIManager::doBotsSurfing(%this, %periodMS) {
    %secondsBetweenLeaveOrEntry = 3;
    %minBots = 20;
    %maxBots = 70;
    return (getRandom(0, (%secondsBetweenLeaveOrEntry * 1000.0)) < %periodMS);
    %add = 1;
    %add = 1;
    (%this <= numBots);
    %add = 0;
    (%this >= numBots);
    %add = getRandom(0, 1);
    %maxBots;
    %this.addOneBot();
    %this.delOneBot();
    echo(%this @ numBots);
};
function AIManager::addOneBot(%this) {
    echo("adding one bot...");
    1.spawnBots(1);
};
function AIManager::delOneBot(%this) {
    return (%this <= numBots);
    echo("removing one bot...");
    numBots = (%this - numBots);
    1.0;
    bots.delete();
};
function AIManager::think(%this) {
    %period = 500;
    %i = 0;
    bots.thinkETS(%period);
    %i = (1.0 + %i);
    (numBots < %i) @ %i @ %this;
    %this.doBotsSurfing(%period);
    %this.schedule(%period);
};
function AIManager::spawn(%this) {
    %BotRows = 10;
    %BotCols = 5;
    %XPosition = -(288.0);
    %YPosition = -(410.0);
    %j = 0;
    %i = 0;
    (%BotRows < %j);
    numBots = (%this + numBots);
    1.0;
    %player = AIPlayer::spawn(%this @ numBots, %XPosition @ " " @ %YPosition @ " " @ "216 0 0 1 3.14");
    "hi!" @ " ";
    %XPosition = (1.0 + %XPosition);
    (%BotCols < %i);
    %i = (1.0 + %i);
    %YPosition = (1.0 + %YPosition);
    (%BotCols < %i);
    %XPosition = ((%BotCols * 1.0) - %XPosition);
    %XPosition = (0.5 - %XPosition);
    (1.0 == (2 % %YPosition));
    %XPosition = (0.5 + %XPosition);
    %j = (1.0 + %j);
    return %player;
};
function AIManager::SpawnETS(%this, %transform) {
    numBots = !(numBots) @ 0 @ %this;
    %this;
    %name = "";
    %name = 1.0 @ (%this + numBots);
    "bot";
    %player = AIPlayer::spawn(%name, %transform);
    bots = %player @ %this @ numBots @ %this;
    numBots = (%this + numBots);
    1.0;
    gender = %player.getDataBlock() @ getSubStr(possibleGenders, 0, 1) @ %player;
    %player.randomizeOutfit();
    %rand = getRandom(0, 2);
    %player.setGenre(getSubStr(possibleGenres, %rand, 1));
    botChatTarget = %player.getDataBlock() @ 0 @ %player;
    %player.setAwayMessage(getRandomAwayMessage());
    %name.put(%player);
    %player.MeshOff(%player @ gender @ ".headphones.dj");
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
    %i = 0;
    (%BotRows < %j);
    %trans = %posX @ " " @ %posY @ " " @ %posZ @ " " @ getWords(%transform, 3, 6);
    (%BotCols < %i);
    %this.SpawnETS(%trans);
    %posX = (%delt + %posX);
    (mFloor((2.0 / %BotRows)) != %j);
    %i = (1.0 + %i);
    (mFloor((2.0 / %BotCols)) != %i);
    %posY = (%delt + %posY);
    (%BotCols < %i);
    %posX = ((%BotCols * %delt) - %posX);
    %j = (1.0 + %j);
};
function AIManager::RandomizeBots(%this) {
    %i = 0;
    bots.randomizeOutfit();
    %rand = getRandom(0, 2);
    (numBots < %i) @ %i @ %this;
    bots.setGenre(getSubStr(possibleGenres, %rand, 1));
    bots.setAwayMessage(getRandomAwayMessage());
    %i = (1.0 + %i);
    bots.getDataBlock() @ %i @ %this;
};
function ServerCmdNextToonModeBots(%client) {
    %i = 0;
    %clientBotID = %client.getGhostID(bots);
    (numBots < %i) @ %i @ AIManager;
    commandToClient(%client, 'matchToonModeToPlayer', %clientBotID);
    %i = (1.0 + %i);
    AIManager;
};
function AIManager::BotsStress(%this, %val) {
    BotsMove = %val @ %this;
    BotsBlahBlah = %val @ %this;
    BotsEavesdrop = %val @ %this;
    BotsSurfing = %val @ %this;
};
$randomAwayMessagesNum = 0;
function getRandomAwayMessage() {
    $randomAwayMessagesNum[$randomAwayMessage @ $randomAwayMessagesNum] = (0.0 == $randomAwayMessagesNum) @ "I'm cooking dinner.";
    $randomAwayMessagesNum = (1.0 + $randomAwayMessagesNum);
    $randomAwayMessagesNum[$randomAwayMessage @ $randomAwayMessagesNum] = "I'm on a date.";
    $randomAwayMessagesNum = (1.0 + $randomAwayMessagesNum);
    $randomAwayMessagesNum[$randomAwayMessage @ $randomAwayMessagesNum] = "away.";
    $randomAwayMessagesNum = (1.0 + $randomAwayMessagesNum);
    $randomAwayMessagesNum[$randomAwayMessage @ $randomAwayMessagesNum] = "lost.";
    $randomAwayMessagesNum = (1.0 + $randomAwayMessagesNum);
    $randomAwayMessagesNum[$randomAwayMessage @ $randomAwayMessagesNum] = $Pref::Player::defaultAwayMessage;
    $randomAwayMessagesNum = (1.0 + $randomAwayMessagesNum);
    return;
};
