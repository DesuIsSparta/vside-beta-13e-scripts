function toggleHelpMeMode(%this)
{
    if (isObject(ApplauseMeterGui))
    {
        if (ApplauseMeterGui.applauseMeterUse $= "sumo")
        {
            if (ApplauseMeterGui.sumoGameType $= "PillowFightGame")
            {
                MessageBoxOK($MsgCat::applauseGui["MSG-PILLOW-WARN"], $MsgCat::applauseGui["MSG-PILLOW-USER-NO-HELPME"], "");
            }
            else
            {
                MessageBoxOK($MsgCat::applauseGui["MSG-SUMO-WARN"], $MsgCat::applauseGui["MSG-SUMO-USER-NO-HELPME"], "");
            }
            return ;
        }
    }
    if ($player.isInHelpMeMode())
    {
        clearHelpMeMode();
    }
    else
    {
        setHelpMeMode();
    }
    return ;
}
function updateHelpMeModeMenu()
{
    if (!isObject(HelpPopupMenu))
    {
        return ;
    }
    %item = HelpPopupMenu.findObjectByInternalName("helpMe");
    if (!isObject(%item))
    {
        error(getScopeName() SPC "- could not find menu item");
        return ;
    }
    %command = "toggleHelpMeMode();";
    if (isObject($player) && $player.isInHelpMeMode())
    {
        %text = "Stop asking vSiders for Help";
    }
    else
    {
        %text = "Ask other vSiders for Help";
    }
    %item.setMenuItemText(%text);
    %item.command = %command;
    return ;
}
$gHelpMeModeDuration = (3 * 60) * 1000;
$gHelpMeModeAutoOffTimer = 0;
function setHelpMeMode()
{
    commandToServer('setActiveSkus', $player.getActiveSKUs() SPC getSpecialSKU($player, "helpmebadge"));
    msgCatOKOnceThisSession("UI::HELPMEMODE-ON");
    cancel($gHelpMeModeAutoOffTimer);
    $gHelpMeModeAutoOffTimer = schedule($gHelpMeModeDuration, 0, "clearHelpMeMode");
    return ;
}
function clearHelpMeMode()
{
    %mySkus = $player.getActiveSKUs();
    %idx = findWord(%mySkus, getSpecialSKU($player, "helpmebadge"));
    if (%idx >= 0)
    {
        %mySkus = removeWord(%mySkus, %idx);
        commandToServer('setActiveSkus', %mySkus);
    }
    cancel($gHelpMeModeAutoOffTimer);
    $gHelpMeModeAutoOffTimer = 0;
    return ;
}
$gHelpMeRequestId = 0;
$gHelpMeRequestsAnswered = "";
function clientCmdNotifyOfHelpMeMode(%newbName)
{
    %name = getPlayerMarkup(%newbName, "ffffff", 1);
    handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff>You are an active guide, and " @ %name @ " has just entered \"Help-Me\" Mode. <a:answerHelpMeMode " @ $gHelpMeRequestId SPC munge(%newbName) @ ">Click Here</a> to answer the call!");
    $gHelpMeRequestId = $gHelpMeRequestId + 1;
    return ;
}
function answerHelpMeMode(%newbName, %requestId)
{
    if (findWord($gHelpMeRequestsAnswered, %requestId) >= 0)
    {
        msgCatOK("UI::HELPMEANSWERED");
        return ;
    }
    $gHelpMeRequestsAnswered = $gHelpMeRequestsAnswered SPC %requestId;
    commandToServer('answerHelpMeMode', %newbName, %requestId);
    return ;
}
function Player::onAnimationStart(%this, %animName)
{
    if (isObject($player) && (%this.getId() == $player.getId()))
    {
        %animTags = gAnimationTags.get(%animName);
        %dancing = hasWord(%animTags, "dance");
        if (%dancing)
        {
            getUserActivityMgr().setActivityActive("dancing", 1);
        }
        %this.onAnimationSku(0, %animName, "");
    }
    return ;
}
function Player::onAnimationSku(%this, %state, %animName, %animInternalName)
{
    %animSkus = %this.getAnimationSkus(%animInternalName);
    if (%animSkus $= "")
    {
        if (!(%this.currentBaseActiveSkus $= ""))
        {
            if (!(%this.currentBaseActiveSkus $= %this.getActiveSKUs()))
            {
                %this.setActiveSKUs(%this.currentBaseActiveSkus);
            }
        }
        return ;
    }
    %activeSkus = %this.getActiveSKUs();
    if (%state)
    {
        %activeSkus = SkuManager.overlaySkus(%activeSkus, %animSkus);
    }
    else
    {
        %activeSkus = SkuManager.overlaySkus(%this.currentBaseActiveSkus, SkuManager.skusRemove(%activeSkus, %animSkus));
    }
    %this.setActiveSKUs(%activeSkus);
    return ;
}
function Player::getAnimationSkus(%this, %animInternalName)
{
    if (%animInternalName $= "")
    {
        return "";
    }
    if (%this.animationSkus[%animInternalName] $= "")
    {
        %skusIndex = strstr(%animInternalName, "_skus_");
        if (%skusIndex == -1)
        {
            %skus = "";
        }
        else
        {
            %skusString = getSubStr(%animInternalName, %skusIndex);
            %skusString = strreplace(%skusString, "_", " ");
            %skus = restWords(restWords(%skusString));
        }
        %skus = SkuManager.filterSkusGender(%skus, %this.getGender());
        %this.animationSkus[%animInternalName] = %skus;
    }
    return %this.animationSkus[%animInternalName];
}
$gPlayerStaggerTimer = "";
$gPlayerStaggerTimerPeriod = 300;
$gPlayerStaggerAmount = 0;
$gPlayerStaggerPrevAmt = 0;
function Player::staggerSetAmount(%this, %amount)
{
    $gPlayerStaggerAmount = %amount;
    %this.staggerTick();
    return ;
}
function Player::staggerTick(%this)
{
    cancel($gPlayerStaggerTimer);
    $gPlayerStaggerTimer = "";
    if ($gPlayerStaggerAmount == 0)
    {
        return ;
    }
    %fwdVel = $mvForwardAction - $mvBackwardAction;
    %sdeVel = $mvLeftAction - $mvRightAction;
    if ((%fwdVel != 0) && (%sdeVel != 0))
    {
        %amt = getRandom(0, $gPlayerStaggerAmount * 1000) * 0.001;
        %amt = %amt * getRandom(0, 1) ? 1 : 1;
    }
    else
    {
        %amt = 0;
    }
    %amt = ($gPlayerStaggerPrevAmt * 0.8) + (%amt * 0.2);
    $gPlayerStaggerPrevAmt = %amt;
    %speedBase = $mvYawLeftSpeedBase - $mvYawRightSpeedBase;
    %speed = %speedBase + %amt;
    if (%speed > 1e-05)
    {
        $mvYawLeftSpeed = %speed * 1;
        $mvYawRightSpeed = 0;
        %period = $gPlayerStaggerTimerPeriod;
    }
    else
    {
        if (%speed < -1e-05)
        {
            $mvYawLeftSpeed = 0;
            $mvYawRightSpeed = %speed * -1;
            %period = $gPlayerStaggerTimerPeriod;
        }
        else
        {
            $mvYawLeftSpeed = 0;
            $mvYawRightSpeed = 0;
            %period = $gPlayerStaggerTimerPeriod * 3;
        }
    }
    $gPlayerStaggerTimer = %this.schedule(%period, "staggerTick");
    return ;
}
function Player::onAnimationDoneClient(%this, %unused)
{
    if (%this.getId() != $player.getId())
    {
        return ;
    }
    if (isObject(ClosetGui) && ClosetGui.isVisible())
    {
        ClosetGui.isDoingPropAction = 0;
        if (isObject(ClosetWhatYoureWearingList))
        {
            ClosetWhatYoureWearingList.refresh(ClosetWhatYoureWearingList.skus);
        }
    }
    return ;
}
