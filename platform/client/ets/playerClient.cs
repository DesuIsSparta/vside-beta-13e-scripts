function toggleHelpMeMode(%this) {
    if (isObject(ApplauseMeterGui)) {
        if ((ApplauseMeterGui @ " " @ applauseMeterUse $= "sumo")) {
            if ((ApplauseMeterGui @ " " @ sumoGameType $= "PillowFightGame")) {
                MessageBoxOK(, , "");
            }
            MessageBoxOK(, , "");
            return;
        }
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
    %item = HelpPopupMenu.findObjectByInternalName("helpMe");
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
    %item.setMenuItemText(%text);
    %item.command = %command;
};
$gHelpMeModeDuration = (1000.0 * (60.0 * 3.0));
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
    if ((0.0 >= %idx)) {
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
    $gHelpMeRequestId = (1.0 + $gHelpMeRequestId);
};
function answerHelpMeMode(%newbName, %requestId) {
    if ((0.0 >= findWord($gHelpMeRequestsAnswered, %requestId))) {
        msgCatOK("UI::HELPMEANSWERED");
        return;
    }
    $gHelpMeRequestsAnswered = $gHelpMeRequestsAnswered @ " " @ %requestId;
    commandToServer('answerHelpMeMode', %newbName, %requestId);
};
function Player::onAnimationStart(%this, %animName) {
    if (isObject($player)) {
    }
    if (($player.getId() == %this.getId())) {
        %animTags = gAnimationTags.get(%animName);
        %dancing = hasWord(%animTags, "dance");
        if (%dancing) {
            getUserActivityMgr().setActivityActive("dancing", 1);
        }
        %this.onAnimationSku(0, %animName, "");
    }
};
function Player::onAnimationSku(%this, %state, %animName, %animInternalName) {
    %animSkus = %this.getAnimationSkus(%animInternalName);
    if ((%animSkus $= "")) {
        if (!(%this.currentBaseActiveSkus $= "")) {
            if (!(%this.currentBaseActiveSkus $= %this.getActiveSKUs())) {
                %this.setActiveSKUs(%this.currentBaseActiveSkus);
            }
        }
        return;
    }
    %activeSkus = %this.getActiveSKUs();
    if (%state) {
        %activeSkus = SkuManager.overlaySkus(%activeSkus, %animSkus);
    }
    %activeSkus = SkuManager.overlaySkus(%this.currentBaseActiveSkus, SkuManager.skusRemove(%activeSkus, %animSkus));
    %this.setActiveSKUs(%activeSkus);
};
function Player::getAnimationSkus(%this, %animInternalName) {
    if ((%animInternalName $= "")) {
        return "";
    }
    if ((%animInternalName @ " " @ %this.animationSkus $= "")) {
        %skusIndex = strstr(%animInternalName, "_skus_");
        if ((-(1.0) == %skusIndex)) {
            %skus = "";
        }
        %skusString = getSubStr(%animInternalName, %skusIndex);
        %skusString = strreplace(%skusString, "_", " ");
        %skus = restWords(restWords(%skusString));
        %skus = SkuManager.filterSkusGender(%skus, %this.getGender());
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
    if ((0.0 == $gPlayerStaggerAmount)) {
        return;
    }
    %fwdVel = ($mvBackwardAction - $mvForwardAction);
    %sdeVel = ($mvRightAction - $mvLeftAction);
    if ((0.0 != %fwdVel)) {
    }
    if ((0.0 != %sdeVel)) {
        %amt = (0.001 * getRandom(0, (1000.0 * $gPlayerStaggerAmount)));
        if (getRandom(0, 1)) {
        }
        %amt = (1.0 * %amt);
        -(1.0);
    }
    %amt = 0;
    %amt = ((0.2 * %amt) + (0.8 * $gPlayerStaggerPrevAmt));
    $gPlayerStaggerPrevAmt = %amt;
    %speedBase = ($mvYawRightSpeedBase - $mvYawLeftSpeedBase);
    %speed = (%amt + %speedBase);
    if ((0.00001 > %speed)) {
        $mvYawLeftSpeed = (1.0 * %speed);
        $mvYawRightSpeed = 0;
        %period = $gPlayerStaggerTimerPeriod;
    }
    if ((-(0.00001) < %speed)) {
        $mvYawLeftSpeed = 0;
        $mvYawRightSpeed = (-(1.0) * %speed);
        %period = $gPlayerStaggerTimerPeriod;
    }
    $mvYawLeftSpeed = 0;
    $mvYawRightSpeed = 0;
    %period = (3.0 * $gPlayerStaggerTimerPeriod);
    $gPlayerStaggerTimer = %this.schedule(%period, "staggerTick");
};
function Player::onAnimationDoneClient(%this, %unused) {
    if (($player.getId() != %this.getId())) {
        return;
    }
    if (isObject(ClosetGui)) {
    }
    if (ClosetGui.isVisible()) {
        %this.isDoingPropAction = 0 @ ClosetGui;
        if (isObject(ClosetWhatYoureWearingList)) {
            ClosetWhatYoureWearingList.refresh(ClosetWhatYoureWearingList, %this.skus);
        }
    }
};
