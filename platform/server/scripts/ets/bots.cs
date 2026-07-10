function serverCmdAddBotArmy(%client) {
    if (!(isObject(%client.Player))) {
        return;
    }
    if (!(%client.Player.isStaff())) {
        return;
    }
    AIManager.SpawnArmyETS(%client.Player.getTransform());
    return;
};
function serverCmdAddBot(%client) {
    if (!(isObject(%client.Player))) {
        return;
    }
    if (!(%client.Player.isStaff())) {
        return;
    }
    AIManager.SpawnETS(%client.Player.getTransform());
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
    %client.BotsMove = %val @ AIManager;
    return;
};
function serverCmdToggleBotsMove(%client) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    %client.BotsMove = !(%client.BotsMove) @ AIManager;
    AIManager;
    return;
};
function serverCmdToggleBotsBlahBlah(%client) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    %client.BotsBlahBlah = !(%client.BotsBlahBlah) @ AIManager;
    AIManager;
    return;
};
function serverCmdOneShotBotsBlahBlah(%client, %toPlayer) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    if (%toPlayer) {
        AIManager.BotsBlahBlahOneShot(%client.Player);
    }
    AIManager.BotsBlahBlahOneShot();
    return;
};
function serverCmdToggleBotsEavesdrop(%client) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    %client.BotsEavesdrop = !(%client.BotsEavesdrop) @ AIManager;
    AIManager;
    return;
};
function serverCmdToggleBotsSurfing(%client) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    %client.BotsSurfing = !(%client.BotsSurfing) @ AIManager;
    AIManager;
    return;
};
function serverCmdOneLove(%client) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    %n = 0;
    if ((%client.numBots < %n)) {
        %n @ AIManager.wardrobeStock(%client.bots);
        %n = (1.0 + %n);
        AIManager;
    }
    %client.Player.wardrobeStock();
    return (%client.numBots < %n);
};
function serverCmdZombiesAttack(%client, %position) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    if ((%position $= "")) {
        %position = %client.Player.getTransform();
    }
    AIManager.zombiesAttack(%position, %client.Player);
    return;
};
function serverCmdZombiesDance(%client, %param) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    AIManager.zombiesDance(%param);
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
    if ((1.0 < %this.numBots)) {
        return;
    }
    %this.BotsMove = 0;
    %botRadius = 0.6;
    %radius = (((3.14159 * 2.0) / (%this.numBots * %botRadius)) + 1.0);
    %theta = 0;
    %dTheta = (%this.numBots / (3.14159 * 2.0));
    %i = 0;
    if ((%this.numBots < %i)) {
        %v1 = mCos(%theta) @ " " @ mSin(%theta) @ " " @ 0;
        %v1 = VectorScale(%v1, %radius);
        %v1 = VectorAdd(%v1, %position);
        %i.zombieAttack(%this.bots, %v1, %position, %obj, %theta);
        %theta = (%dTheta + %theta);
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
    if ((1.0 < %this.numBots)) {
        return;
    }
    %this.BotsMove = 0;
    %i = 0;
    if ((%this.numBots < %i)) {
        %i.playAnim(%this.bots, "dnc" @ getRandom(1, 4));
        %i = (1.0 + %i);
    }
};
function AIManager::zombiesEmote(%this) {
    if ((1.0 < %this.numBots)) {
        return;
    }
    %this.BotsMove = 0;
    %i = 0;
    if ((%this.numBots < %i)) {
        %i.doRandomEmote(%this.bots);
        %i = (1.0 + %i);
    }
};
function AIPlayer::doAutoMoveEntry(%this) {
    %this.playAnim("pwve");
    %pos = EntrySpawn.choosePointOnCenterPlane();
    %this.setAimObject(0);
    %this.schedule(2500, "setMoveDestination", %pos, 0);
    return;
};
function serverCmdKillBots(%client) {
    AIManager.killBots();
    return;
};
function AIManager::killBots(%this) {
    if ((1.0 < %this.numBots)) {
        return;
    }
    %i = 0;
    if ((%this.numBots < %i)) {
        %i.delete(%this.bots);
        %i = (1.0 + %i);
    }
    %this.numBots = (%this.numBots < %i) @ 0;
    return;
};
function serverCmdBotsIdlePercent(%client, %percent) {
    AIManager.IdleBots(%percent);
    return;
};
function AIManager::IdleBots(%this, %percent) {
    %i = 0;
    if ((%this.numBots < %i)) {
        %i.setAFK(%this.bots, (%percent < getRandom(1, 99)));
        %i = (1.0 + %i);
    }
};
