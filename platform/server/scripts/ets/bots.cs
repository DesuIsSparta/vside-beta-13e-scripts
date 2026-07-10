function serverCmdAddBotArmy(%client) {
    if (!(isObject(Player))) {
        return %client;
    }
    if (!(Player.isStaff())) {
        return %client;
    }
    Player.getTransform().SpawnArmyETS();
    return %client;
};
function serverCmdAddBot(%client) {
    if (!(isObject(Player))) {
        return %client;
    }
    if (!(Player.isStaff())) {
        return %client;
    }
    Player.getTransform().SpawnETS();
    return %client;
};
function serverCmdRandomizeBots(%client) {
    if (!(Player.isStaff())) {
        return %client;
    }
    RandomizeBots();
    return AIManager;
};
function serverCmdBotsMove(%client, %val) {
    if (!(Player.isStaff())) {
        return %client;
    }
    BotsMove = %val @ AIManager;
    return;
};
function serverCmdToggleBotsMove(%client) {
    if (!(Player.isStaff())) {
        return %client;
    }
    BotsMove = AIManager @ !(BotsMove) @ AIManager;
    return;
};
function serverCmdToggleBotsBlahBlah(%client) {
    if (!(Player.isStaff())) {
        return %client;
    }
    BotsBlahBlah = AIManager @ !(BotsBlahBlah) @ AIManager;
    return;
};
function serverCmdOneShotBotsBlahBlah(%client, %toPlayer) {
    if (!(Player.isStaff())) {
        return %client;
    }
    if (%toPlayer) {
        Player.BotsBlahBlahOneShot();
    }
    BotsBlahBlahOneShot();
    return AIManager;
};
function serverCmdToggleBotsEavesdrop(%client) {
    if (!(Player.isStaff())) {
        return %client;
    }
    BotsEavesdrop = AIManager @ !(BotsEavesdrop) @ AIManager;
    return;
};
function serverCmdToggleBotsSurfing(%client) {
    if (!(Player.isStaff())) {
        return %client;
    }
    BotsSurfing = AIManager @ !(BotsSurfing) @ AIManager;
    return;
};
function serverCmdOneLove(%client) {
    if (!(Player.isStaff())) {
        return %client;
    }
    %n = 0;
    if ((numBots < %n)) {
        bots.wardrobeStock();
        %n = (1.0 + %n);
        AIManager @ %n @ AIManager;
    }
    Player.wardrobeStock();
    return %client;
};
function serverCmdZombiesAttack(%client, %position) {
    if (!(Player.isStaff())) {
        return %client;
    }
    if ((%position $= "")) {
        %position = Player.getTransform();
        %client;
    }
    %position.zombiesAttack(Player);
    return %client;
};
function serverCmdZombiesDance(%client, %param) {
    if (!(Player.isStaff())) {
        return %client;
    }
    %param.zombiesDance();
    return AIManager;
};
function serverCmdZombiesEmote(%client) {
    if (!(Player.isStaff())) {
        return %client;
    }
    zombiesEmote();
    return AIManager;
};
function AIManager::zombiesAttack(%this, %position, %obj) {
    if ((%this < numBots)) {
        return 1.0;
    }
    BotsMove = 0 @ %this;
    %botRadius = 0.6;
    %radius = ((%this / (numBots * %botRadius)) + 1.0);
    (3.14159 * 2.0);
    %theta = 0;
    %dTheta = (numBots / (3.14159 * 2.0));
    %this;
    %i = 0;
    if ((numBots < %i)) {
        %v1 = mCos(%theta) @ " " @ mSin(%theta) @ " " @ 0;
        %this;
        %v1 = VectorScale(%v1, %radius);
        %v1 = VectorAdd(%v1, %position);
        bots.zombieAttack(%v1, %position, %obj, %theta);
        %theta = (%dTheta + %theta);
        %i @ %this;
        %i = (1.0 + %i);
    }
};
function AIPlayer::zombieAttack(%this, %position, %aimAt, %obj, %theta) {
    %this.playAnim("dnc" @ getRandom(1, 2));
    %this.setMoveDestination(%position, 0);
    if (!(%obj $= "")) {
        %this.setAimObject(%obj);
    }
    %this.setAimLocation(%aimAt);
    return;
};
function AIManager::zombiesDance(%this, %param) {
    if ((%this < numBots)) {
        return 1.0;
    }
    BotsMove = 0 @ %this;
    %i = 0;
    if ((numBots < %i)) {
        bots.playAnim(%this @ %i @ %this @ "dnc" @ getRandom(1, 4));
        %i = (1.0 + %i);
    }
};
function AIManager::zombiesEmote(%this) {
    if ((%this < numBots)) {
        return 1.0;
    }
    BotsMove = 0 @ %this;
    %i = 0;
    if ((numBots < %i)) {
        bots.doRandomEmote();
        %i = (1.0 + %i);
        %this @ %i @ %this;
    }
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
    if ((%this < numBots)) {
        return 1.0;
    }
    %i = 0;
    if ((numBots < %i)) {
        bots.delete();
        %i = (1.0 + %i);
        %this @ %i @ %this;
    }
    numBots = (numBots < %i) @ 0 @ %this;
    %this;
    return;
};
function serverCmdBotsIdlePercent(%client, %percent) {
    %percent.IdleBots();
    return AIManager;
};
function AIManager::IdleBots(%this, %percent) {
    %i = 0;
    if ((numBots < %i)) {
        bots.setAFK((%percent < getRandom(1, 99)));
        %i = (1.0 + %i);
        %this @ %i @ %this;
    }
};
