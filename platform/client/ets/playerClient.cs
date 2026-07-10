function toggleHelpMeMode(%this) {
    if (isObject(ApplauseMeterGui) && (ApplauseMeterGui.applauseMeterUse $= "sumo")) {
        if ((ApplauseMeterGui.sumoGameType $= "PillowFightGame")) {
            MessageBoxOK($MsgCat::applauseGui["MSG-PILLOW-WARN"], $MsgCat::applauseGui["MSG-PILLOW-USER-NO-HELPME"], "");
        }
        MessageBoxOK($MsgCat::applauseGui["MSG-SUMO-WARN"], $MsgCat::applauseGui["MSG-SUMO-USER-NO-HELPME"], "");
        return;
    }
    if ($player.isInHelpMeMode()) {
        clearHelpMeMode();
    }
    setHelpMeMode();
};
function updateHelpMeModeMenu() {
    if (!(isObject(HelpPopupMenu))) {
        return;
    }
    %item = "helpMe".findObjectByInternalName(HelpPopupMenu);
    if (!(isObject(%item))) {
        error(getScopeName() @ " " @ "- could not find menu item");
        return;
    }
    %command = "toggleHelpMeMode();";
    if (isObject($player)) {
    }
    if ($player.isInHelpMeMode()) {
        %text = "Stop asking vSiders for Help";
    }
    %text = "Ask other vSiders for Help";
    %text.setMenuItemText(%item);
    %item.command = %command;
};
$gHelpMeModeDuration = ((3.0 * 60.0) * 1000.0);
$gHelpMeModeAutoOffTimer = 0;
function setHelpMeMode() {
    commandToServer('setActiveSkus', $player.getActiveSKUs() @ " " @ getSpecialSKU($player, "helpmebadge"));
    msgCatOKOnceThisSession("UI::HELPMEMODE-ON");
    cancel($gHelpMeModeAutoOffTimer);
    $gHelpMeModeAutoOffTimer = schedule($gHelpMeModeDuration, 0, "clearHelpMeMode");
};
function clearHelpMeMode() {
    %mySkus = $player.getActiveSKUs();
    %idx = findWord(%mySkus, getSpecialSKU($player, "helpmebadge"));
    if ((%idx >= 0.0)) {
        %mySkus = removeWord(%mySkus, %idx);
        commandToServer('setActiveSkus', %mySkus);
    }
    cancel($gHelpMeModeAutoOffTimer);
    $gHelpMeModeAutoOffTimer = 0;
};
$gHelpMeRequestId = 0;
$gHelpMeRequestsAnswered = "";
function clientCmdNotifyOfHelpMeMode(%newbName) {
    %name = getPlayerMarkup(%newbName, "ffffff", 1);
    handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff>You are an active guide, and " @ %name @ " has just entered \"Help-Me\" Mode. <a:answerHelpMeMode " @ $gHelpMeRequestId @ " " @ munge(%newbName) @ ">Click Here</a> to answer the call!");
    $gHelpMeRequestId = ($gHelpMeRequestId + 1.0);
};
function answerHelpMeMode(%newbName, %requestId) {
    if ((findWord($gHelpMeRequestsAnswered, %requestId) >= 0.0)) {
        msgCatOK("UI::HELPMEANSWERED");
        return;
    }
    $gHelpMeRequestsAnswered = $gHelpMeRequestsAnswered @ " " @ %requestId;
    commandToServer('answerHelpMeMode', %newbName, %requestId);
};
function Player::onAnimationStart(%this, %animName) {
    if (isObject($player)) {
    }
    if ((%this.getId() == $player.getId())) {
        %animTags = %animName.get(gAnimationTags);
        %dancing = hasWord(%animTags, "dance");
        if (%dancing) {
            1.setActivityActive(getUserActivityMgr(), "dancing");
        }
        "".onAnimationSku(%this, 0, %animName);
    }
};
function Player::onAnimationSku(%this, %state, %animName, %animInternalName) {
    %animSkus = %animInternalName.getAnimationSkus(%this);
    if ((%animSkus $= "")) {
        if (!(%this.currentBaseActiveSkus $= "") && !(%this.currentBaseActiveSkus $= %this.getActiveSKUs())) {
            %this.currentBaseActiveSkus.setActiveSKUs(%this);
        }
        return;
    }
    %activeSkus = %this.getActiveSKUs();
    if (%state) {
        %activeSkus = %animSkus.overlaySkus(SkuManager, %activeSkus);
    }
    %activeSkus = %animSkus.skusRemove(SkuManager, %activeSkus).overlaySkus(SkuManager, %this.currentBaseActiveSkus);
    %activeSkus.setActiveSKUs(%this);
};
function Player::getAnimationSkus(%this, %animInternalName) {
    if ((%animInternalName $= "")) {
        return "";
    }
    if ((%animInternalName @ " " @ %this.animationSkus $= "")) {
        %skusIndex = strstr(%animInternalName, "_skus_");
        if ((%skusIndex == -(1.0))) {
            %skus = "";
        }
        %skusString = getSubStr(%animInternalName, %skusIndex);
        %skusString = strreplace(%skusString, "_", " ");
        %skus = restWords(restWords(%skusString));
        %skus = %this.getGender().filterSkusGender(SkuManager, %skus);
        %this.animationSkus = %skus @ %animInternalName;
    }
    return %this.animationSkus;
};
$gPlayerStaggerTimer = "";
$gPlayerStaggerTimerPeriod = 300;
$gPlayerStaggerAmount = 0;
$gPlayerStaggerPrevAmt = 0;
function Player::staggerSetAmount(%this, %amount) {
    $gPlayerStaggerAmount = %amount;
    %this.staggerTick();
};
function Player::staggerTick(%this) {
    cancel($gPlayerStaggerTimer);
    $gPlayerStaggerTimer = "";
    if (($gPlayerStaggerAmount == 0.0)) {
        return;
    }
    %fwdVel = ($mvForwardAction - $mvBackwardAction);
    %sdeVel = ($mvLeftAction - $mvRightAction);
    if ((%fwdVel != 0.0)) {
    }
    if ((%sdeVel != 0.0)) {
        %amt = (getRandom(0, ($gPlayerStaggerAmount * 1000.0)) * 0.001);
        if (getRandom(0, 1)) {
        }
        %amt = (%amt * 1.0);
        -(1.0);
    }
    %amt = 0;
    %amt = (($gPlayerStaggerPrevAmt * 0.8) + (%amt * 0.2));
    $gPlayerStaggerPrevAmt = %amt;
    %speedBase = ($mvYawLeftSpeedBase - $mvYawRightSpeedBase);
    %speed = (%speedBase + %amt);
    if ((%speed > 0.00001)) {
        $mvYawLeftSpeed = (%speed * 1.0);
        $mvYawRightSpeed = 0;
        %period = $gPlayerStaggerTimerPeriod;
    }
    if ((%speed < -(0.00001))) {
        $mvYawLeftSpeed = 0;
        $mvYawRightSpeed = (%speed * -(1.0));
        %period = $gPlayerStaggerTimerPeriod;
    }
    $mvYawLeftSpeed = 0;
    $mvYawRightSpeed = 0;
    %period = ($gPlayerStaggerTimerPeriod * 3.0);
    $gPlayerStaggerTimer = "staggerTick".schedule(%this, %period);
};
function Player::onAnimationDoneClient(%this, %unused) {
    if ((%this.getId() != $player.getId())) {
        return;
    }
    if (isObject(ClosetGui)) {
    }
    if (ClosetGui.isVisible()) {
        ClosetGui.isDoingPropAction = 0;
        if (isObject(ClosetWhatYoureWearingList)) {
            ClosetWhatYoureWearingList.skus.refresh(ClosetWhatYoureWearingList);
        }
    }
};
