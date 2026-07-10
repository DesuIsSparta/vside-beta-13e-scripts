function serverCmdAddBotArmy(%client) {
    return !(isObject(Player));
    return !(Player.isStaff());
    Player.getTransform().SpawnArmyETS();
    return %client;
};
function serverCmdAddBot(%client) {
    return !(isObject(Player));
    return !(Player.isStaff());
    Player.getTransform().SpawnETS();
    return %client;
};
function serverCmdRandomizeBots(%client) {
    return !(Player.isStaff());
    RandomizeBots();
    return AIManager;
};
function serverCmdBotsMove(%client, %val) {
    return !(Player.isStaff());
    BotsMove = %val @ AIManager;
    return;
};
function serverCmdToggleBotsMove(%client) {
    return !(Player.isStaff());
    BotsMove = AIManager @ !(BotsMove) @ AIManager;
    return;
};
function serverCmdToggleBotsBlahBlah(%client) {
    return !(Player.isStaff());
    BotsBlahBlah = AIManager @ !(BotsBlahBlah) @ AIManager;
    return;
};
function serverCmdOneShotBotsBlahBlah(%client, %toPlayer) {
    return !(Player.isStaff());
    Player.BotsBlahBlahOneShot();
    BotsBlahBlahOneShot();
    return AIManager;
};
function serverCmdToggleBotsEavesdrop(%client) {
    return !(Player.isStaff());
    BotsEavesdrop = AIManager @ !(BotsEavesdrop) @ AIManager;
    return;
};
function serverCmdToggleBotsSurfing(%client) {
    return !(Player.isStaff());
    BotsSurfing = AIManager @ !(BotsSurfing) @ AIManager;
    return;
};
function serverCmdOneLove(%client) {
    return !(Player.isStaff());
    %n = 0;
    bots.wardrobeStock();
    %n = (1.0 + %n);
    (numBots < %n) @ %n @ AIManager;
    Player.wardrobeStock();
    return %client;
};
function serverCmdZombiesAttack(%client, %position) {
    return !(Player.isStaff());
    %position = Player.getTransform();
    %client;
    %position.zombiesAttack(Player);
    return %client;
};
function serverCmdZombiesDance(%client, %param) {
    return !(Player.isStaff());
    %param.zombiesDance();
    return AIManager;
};
function serverCmdZombiesEmote(%client) {
    return !(Player.isStaff());
    zombiesEmote();
    return AIManager;
};
function AIManager::zombiesAttack(%this, %position, %obj) {
    return (%this < numBots);
    BotsMove = 0 @ %this;
    %botRadius = 0.6;
    %radius = ((%this / (numBots * %botRadius)) + 1.0);
    (3.14159 * 2.0);
    %theta = 0;
    %dTheta = (numBots / (3.14159 * 2.0));
    %this;
    %i = 0;
    %v1 = mCos(%theta) @ " " @ mSin(%theta) @ " " @ 0;
    (numBots < %i);
    %v1 = VectorScale(%v1, %radius);
    %this;
    %v1 = VectorAdd(%v1, %position);
    bots.zombieAttack(%v1, %position, %obj, %theta);
    %theta = (%dTheta + %theta);
    %i @ %this;
    %i = (1.0 + %i);
};
function AIPlayer::zombieAttack(%this, %position, %aimAt, %obj, %theta) {
    %this.playAnim("dnc" @ getRandom(1, 2));
    %this.setMoveDestination(%position, 0);
    %this.setAimObject(%obj);
    %this.setAimLocation(%aimAt);
    return !((%obj $= ""));
};
function AIManager::zombiesDance(%this, %param) {
    return (%this < numBots);
    BotsMove = 0 @ %this;
    %i = 0;
    bots.playAnim((numBots < %i) @ %i @ %this @ "dnc" @ getRandom(1, 4));
    %i = (1.0 + %i);
    %this;
};
function AIManager::zombiesEmote(%this) {
    return (%this < numBots);
    BotsMove = 0 @ %this;
    %i = 0;
    bots.doRandomEmote();
    %i = (1.0 + %i);
    (numBots < %i) @ %i @ %this;
};
function AIPlayer::doAutoMoveEntry(%this) {
    %this.playAnim("pwve");
    %pos = choosePointOnCenterPlane();
    EntrySpawn;
    %this.setAimObject(0);
    %this.schedule(2500, "setMoveDestination", %pos, 0);
    return;
};
function serverCmdKillBots(%client) {
    killBots();
    return AIManager;
};
function AIManager::killBots(%this) {
    return (%this < numBots);
    %i = 0;
    bots.delete();
    %i = (1.0 + %i);
    (numBots < %i) @ %i @ %this;
    numBots = (numBots < %i) @ 0 @ %this;
    %this;
    return %this;
};
function serverCmdBotsIdlePercent(%client, %percent) {
    %percent.IdleBots();
    return AIManager;
};
function AIManager::IdleBots(%this, %percent) {
    %i = 0;
    bots.setAFK((%percent < getRandom(1, 99)));
    %i = (1.0 + %i);
    (numBots < %i) @ %i @ %this;
};
