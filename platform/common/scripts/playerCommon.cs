function Player::playCelAnimation(%this, %anim)
{
    if (!(%this.getState() $= "Dead"))
    {
        %this.setActionThread("emote_" @ %anim);
    }
    return ;
}
function Player::playAnim(%this, %anim)
{
    if (!(%this.getState() $= "Dead"))
    {
        %this.setActionThread(%anim);
    }
    return ;
}
function Player::playAnimPreRoll(%this, %anim, %preRollMS)
{
    if (!(%this.getState() $= "Dead"))
    {
        %this.setActionThreadPreRoll(%anim, %preRollMS);
    }
    return ;
}
function Player::initGlobalFields(%this)
{
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
    gSetField(%this, "lastActiveTime", -1);
    gSetField(%this, answeringHelpMe, 0);
    gSetField(%this, "mapCtrl", "");
    gSetField(%this, "IsNoLongerTypingTimer", "");
    gSetField(%this, "TimeoutChatPreviewTimer", "");
    gSetField(%this, "balancesAndScoresRevision", 0);
    return ;
}
function Player::destroyGlobalFields(%this)
{
    %this.globalFieldsInited = 0;
    gSetField(%this, previousAnimName, 0);
    gSetField(%this, lastTypingSomethingText, 0);
    gSetField(%this, puppyOwner, 0);
    gSetField(%this, puppyTimer, 0);
    gSetField(%this, reportTriggers, 0);
    %x = gGetField(%this, triggerSet);
    if (isObject(%x))
    {
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
    if (isObject(%x))
    {
        %x.delete();
    }
    gSetField(%this, "gameStateMap", "");
    gSetField(%this, answeringHelpMe, 0);
    gSetField(%this, "mapCtrl", "");
    gSetField(%this, "IsNoLongerTypingTimer", "");
    gSetField(%this, "balancesAndScoresRevision", "");
    return ;
}
function Player::onDelete(%this)
{
    if (%this.isServerObject())
    {
        if (isObject(%this.forceField))
        {
            %this.forceField.delete();
        }
        giftingCurrency_Server_OnPlayerDeleted(%this);
    }
    else
    {
        %this.removeFromPlayerInstanceDict();
        if (isObject(geMapHud2DTheOrthoMap))
        {
            geMapHud2DTheOrthoMap.playerRemove(%this);
        }
    }
    %this.destroyGlobalFields();
    if (isObject(gUserPropMgrServer))
    {
        gUserPropMgrServer.forgetProperties(%this.getShapeName());
    }
    return ;
}
function Player::isInHelpMeMode(%this)
{
    return %this.hasActiveSKU(getSpecialSKU(%this, "helpmebadge"));
}
function Player::isHostOrCohost(%this)
{
    return %this.isHost() || %this.isCohost();
}
function Player::isHost(%this)
{
    return %this.hasRoleString("host");
}
function Player::isCohost(%this)
{
    return %this.hasRoleString("cohost");
}
function Player::getOtherGender(%this)
{
    %g = %this.getGender();
    if (%g $= "f")
    {
        %g = "m";
    }
    else
    {
        if (%g $= "m")
        {
            %g = "f";
        }
    }
    return %g;
}
function Player::onAnimationDone(%this, %anim)
{
    if (%this.isServerObject())
    {
        return %this.onAnimationDoneServer(%anim);
    }
    else
    {
        return %this.onAnimationDoneClient(%anim);
    }
    return ;
}
