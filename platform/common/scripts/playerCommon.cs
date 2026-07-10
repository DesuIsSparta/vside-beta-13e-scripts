function Player::playCelAnimation(%this, %anim) {
    if (!(%this.getState() $= "Dead")) {
        "emote_" @ %anim.setActionThread(%this);
    }
};
function Player::playAnim(%this, %anim) {
    if (!(%this.getState() $= "Dead")) {
        %anim.setActionThread(%this);
    }
};
function Player::playAnimPreRoll(%this, %anim, %preRollMS) {
    if (!(%this.getState() $= "Dead")) {
        %preRollMS.setActionThreadPreRoll(%this, %anim);
    }
};
function Player::initGlobalFields(%this) {
    %this.globalFieldsInited = 1;
    gSetField(%this, previousAnimName, "");
    gSetField(%this, lastTypingSomethingText, "");
    gSetField(%this, puppyOwner, 0);
    gSetField(%this, puppyTimer, 0);
    gSetField(%this, reportTriggers, 0);
    gSetField(%this, triggerSet, 0);
    %snoopers = safeNewScriptObject("SimSet", "", 0);
    gSetField(%this, snoopers, %snoopers);
    gSetField(%this, isScaling, 0);
    gSetField(%this, SEAT_IDLE_SCHEDULE, 0);
    gSetField(%this, SEAT_MAXUSE_SCHEDULE, 0);
    gSetField(%this, genreOverride, "");
    gSetField(%this, respektPoints, 0);
    gSetField(%this, "gameStateMap", "");
    gSetField(%this, "notifyRefuseWhispers", 1);
    gSetField(%this, "lastActiveTime", -(1.0));
    gSetField(%this, answeringHelpMe, 0);
    gSetField(%this, "mapCtrl", "");
    gSetField(%this, "IsNoLongerTypingTimer", "");
    gSetField(%this, "TimeoutChatPreviewTimer", "");
    gSetField(%this, "balancesAndScoresRevision", 0);
};
function Player::destroyGlobalFields(%this) {
    %this.globalFieldsInited = 0;
    gSetField(%this, previousAnimName, 0);
    gSetField(%this, lastTypingSomethingText, 0);
    gSetField(%this, puppyOwner, 0);
    gSetField(%this, puppyTimer, 0);
    gSetField(%this, reportTriggers, 0);
    %x = gGetField(%this, triggerSet);
    if (isObject(%x)) {
        %x.delete();
    }
    gSetField(%this, triggerSet, 0);
    gGetField(%this, snoopers).delete();
    gSetField(%this, snoopers, 0);
    gSetField(%this, isScaling, 0);
    gSetField(%this, SEAT_IDLE_SCHEDULE, 0);
    gSetField(%this, SEAT_MAXUSE_SCHEDULE, 0);
    gSetField(%this, genreOverride, "");
    gSetField(%this, respektPoints, 0);
    gSetField(%this, "notifyRefuseWhispers", 0);
    %x = gGetField(%this, "gameStateMap");
    if (isObject(%x)) {
        %x.delete();
    }
    gSetField(%this, "gameStateMap", "");
    gSetField(%this, answeringHelpMe, 0);
    gSetField(%this, "mapCtrl", "");
    gSetField(%this, "IsNoLongerTypingTimer", "");
    gSetField(%this, "balancesAndScoresRevision", "");
};
function Player::onDelete(%this) {
    if (%this.isServerObject()) {
        if (isObject(%this.forceField)) {
            %this.forceField.delete();
        }
        giftingCurrency_Server_OnPlayerDeleted(%this);
    }
    %this.removeFromPlayerInstanceDict();
    if (isObject(geMapHud2DTheOrthoMap)) {
        %this.playerRemove(geMapHud2DTheOrthoMap);
    }
    %this.destroyGlobalFields();
    if (isObject(gUserPropMgrServer)) {
        %this.getShapeName().forgetProperties(gUserPropMgrServer);
    }
};
function Player::isInHelpMeMode(%this) {
    return getSpecialSKU(%this, "helpmebadge").hasActiveSKU(%this);
};
function Player::isHostOrCohost(%this) {
    if (%this.isHost()) {
    }
    return %this.isCohost();
};
function Player::isHost(%this) {
    return "host".hasRoleString(%this);
};
function Player::isCohost(%this) {
    return "cohost".hasRoleString(%this);
};
function Player::getOtherGender(%this) {
    %g = %this.getGender();
    if ((%g $= "f")) {
        %g = "m";
    }
    if ((%g $= "m")) {
        %g = "f";
    }
    return %g;
};
function Player::onAnimationDone(%this, %anim) {
    if (%this.isServerObject()) {
        return %anim.onAnimationDoneServer(%this);
    }
    return %anim.onAnimationDoneClient(%this);
};
