function serverCmdAddBotArmy(%client) {
    if (!(isObject(%client.Player))) {
        return;
    }
    if (!(%client.Player.isStaff())) {
        return;
    }
    %client.Player.getTransform().SpawnArmyETS(AIManager);
    return;
};
function serverCmdAddBot(%client) {
    if (!(isObject(%client.Player))) {
        return;
    }
    if (!(%client.Player.isStaff())) {
        return;
    }
    %client.Player.getTransform().SpawnETS(AIManager);
    return;
};
function serverCmdRandomizeBots(%client) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    AIManager.RandomizeBots();
    return;
};
function serverCmdBotsMove(%client, %val) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    AIManager.BotsMove = %val;
    return;
};
function serverCmdToggleBotsMove(%client) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    AIManager.BotsMove = !(AIManager.BotsMove);
    return;
};
function serverCmdToggleBotsBlahBlah(%client) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    AIManager.BotsBlahBlah = !(AIManager.BotsBlahBlah);
    return;
};
function serverCmdOneShotBotsBlahBlah(%client, %toPlayer) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    if (%toPlayer) {
        %client.Player.BotsBlahBlahOneShot(AIManager);
    }
    AIManager.BotsBlahBlahOneShot();
    return;
};
function serverCmdToggleBotsEavesdrop(%client) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    AIManager.BotsEavesdrop = !(AIManager.BotsEavesdrop);
    return;
};
function serverCmdToggleBotsSurfing(%client) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    AIManager.BotsSurfing = !(AIManager.BotsSurfing);
    return;
};
function serverCmdOneLove(%client) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    %n = 0;
    while ((%n < AIManager.numBots)) {
        AIManager.bots.wardrobeStock(%n);
        %n = (%n + 1.0);
    }
    %client.Player.wardrobeStock();
    return (%n < AIManager.numBots);
};
function serverCmdZombiesAttack(%client, %position) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    if ((%position $= "")) {
        %position = %client.Player.getTransform();
    }
    %client.Player.zombiesAttack(AIManager, %position);
    return;
};
function serverCmdZombiesDance(%client, %param) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    %param.zombiesDance(AIManager);
    return;
};
function serverCmdZombiesEmote(%client) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    AIManager.zombiesEmote();
    return;
};
function AIManager::zombiesAttack(%this, %position, %obj) {
    if ((%this.numBots < 1.0)) {
        return;
    }
    %this.BotsMove = 0;
    %botRadius = 0.6;
    %radius = (1.0 + ((%botRadius * %this.numBots) / (2.0 * 3.14159)));
    %theta = 0;
    %dTheta = ((2.0 * 3.14159) / %this.numBots);
    %i = 0;
    while ((%i < %this.numBots)) {
        %v1 = mCos(%theta) @ " " @ mSin(%theta) @ " " @ 0;
        %v1 = VectorScale(%v1, %radius);
        %v1 = VectorAdd(%v1, %position);
        %theta.zombieAttack(%i, %this.bots, %v1, %position, %obj);
        %theta = (%theta + %dTheta);
        %i = (%i + 1.0);
    }
};
function AIPlayer::zombieAttack(%this, %position, %aimAt, %obj, %theta) {
    "dnc" @ getRandom(1, 2).playAnim(%this);
    0.setMoveDestination(%this, %position);
    if (!(%obj $= "")) {
        %obj.setAimObject(%this);
    }
    %aimAt.setAimLocation(%this);
    return;
};
function AIManager::zombiesDance(%this, %param) {
    if ((%this.numBots < 1.0)) {
        return;
    }
    %this.BotsMove = 0;
    %i = 0;
    while ((%i < %this.numBots)) {
        "dnc" @ getRandom(1, 4).playAnim(%i, %this.bots);
        %i = (%i + 1.0);
    }
};
function AIManager::zombiesEmote(%this) {
    if ((%this.numBots < 1.0)) {
        return;
    }
    %this.BotsMove = 0;
    %i = 0;
    while ((%i < %this.numBots)) {
        %this.bots.doRandomEmote(%i);
        %i = (%i + 1.0);
    }
};
function AIPlayer::doAutoMoveEntry(%this) {
    "pwve".playAnim(%this);
    %pos = EntrySpawn.choosePointOnCenterPlane();
    0.setAimObject(%this);
    0.schedule(%this, 2500, "setMoveDestination", %pos);
    return;
};
function serverCmdKillBots(%client) {
    AIManager.killBots();
    return;
};
function AIManager::killBots(%this) {
    if ((%this.numBots < 1.0)) {
        return;
    }
    %i = 0;
    while ((%i < %this.numBots)) {
        %this.bots.delete(%i);
        %i = (%i + 1.0);
    }
    %this.numBots = (%i < %this.numBots) @ 0;
    return;
};
function serverCmdBotsIdlePercent(%client, %percent) {
    %percent.IdleBots(AIManager);
    return;
};
function AIManager::IdleBots(%this, %percent) {
    %i = 0;
    while ((%i < %this.numBots)) {
        (getRandom(1, 99) < %percent).setAFK(%i, %this.bots);
        %i = (%i + 1.0);
    }
};
