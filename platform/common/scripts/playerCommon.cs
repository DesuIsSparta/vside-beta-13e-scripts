function Player::playCelAnimation(%this, %anim) {
    %this.setActionThread(!((%this.getState() $= "Dead")) @ "emote_" @ %anim);
};
function Player::playAnim(%this, %anim) {
    %this.setActionThread(%anim);
};
function Player::playAnimPreRoll(%this, %anim, %preRollMS) {
    %this.setActionThreadPreRoll(%anim, %preRollMS);
};
function Player::initGlobalFields(%this) {
    globalFieldsInited = 1 @ %this;
    gSetField(%this, "");
    gSetField(%this, "");
    gSetField(%this, 0);
    gSetField(%this, 0);
    gSetField(%this, 0);
    gSetField(%this, 0);
    %snoopers = safeNewScriptObject("SimSet", "", 0);
    triggerSet;
    gSetField(%this, %snoopers);
    gSetField(%this, 0);
    gSetField(%this, 0);
    gSetField(%this, 0);
    gSetField(%this, "");
    gSetField(%this, 0);
    gSetField(%this, "gameStateMap", "");
    gSetField(%this, "notifyRefuseWhispers", 1);
    gSetField(%this, "lastActiveTime", -(1.0));
    gSetField(%this, 0);
    gSetField(%this, "mapCtrl", "");
    gSetField(%this, "IsNoLongerTypingTimer", "");
    gSetField(%this, "TimeoutChatPreviewTimer", "");
    gSetField(%this, "balancesAndScoresRevision", 0);
};
function Player::destroyGlobalFields(%this) {
    globalFieldsInited = 0 @ %this;
    gSetField(%this, 0);
    gSetField(%this, 0);
    gSetField(%this, 0);
    gSetField(%this, 0);
    gSetField(%this, 0);
    %x = gGetField(%this);
    triggerSet;
    %x.delete();
    gSetField(%this, 0);
    gGetField(%this).delete();
    gSetField(%this, 0);
    gSetField(%this, 0);
    gSetField(%this, 0);
    gSetField(%this, 0);
    gSetField(%this, "");
    gSetField(%this, 0);
    gSetField(%this, "notifyRefuseWhispers", 0);
    %x = gGetField(%this, "gameStateMap");
    respektPoints;
    %x.delete();
    gSetField(%this, "gameStateMap", "");
    gSetField(%this, 0);
    gSetField(%this, "mapCtrl", "");
    gSetField(%this, "IsNoLongerTypingTimer", "");
    gSetField(%this, "balancesAndScoresRevision", "");
};
function Player::onDelete(%this) {
    forceField.delete();
    giftingCurrency_Server_OnPlayerDeleted(%this);
    %this.removeFromPlayerInstanceDict();
    %this.playerRemove();
    %this.destroyGlobalFields();
    %this.getShapeName().forgetProperties();
};
function Player::isInHelpMeMode(%this) {
    return %this.hasActiveSKU(getSpecialSKU(%this, "helpmebadge"));
};
function Player::isHostOrCohost(%this) {
    return %this.isCohost();
};
function Player::isHost(%this) {
    return %this.hasRoleString("host");
};
function Player::isCohost(%this) {
    return %this.hasRoleString("cohost");
};
function Player::getOtherGender(%this) {
    %g = %this.getGender();
    %g = "m";
    (%g $= "f");
    %g = "f";
    (%g $= "m");
    return %g;
};
function Player::onAnimationDone(%this, %anim) {
    return %this.onAnimationDoneServer(%anim);
    return %this.onAnimationDoneClient(%anim);
};
