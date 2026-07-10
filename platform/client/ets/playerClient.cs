function toggleHelpMeMode(%this) {
    MessageBoxOK((ApplauseMeterGui SPC sumoGameType $= "PillowFightGame"), (ApplauseMeterGui SPC applauseMeterUse $= "sumo"), "");
    MessageBoxOK(isObject(), ApplauseMeterGui, "");
    return;
    clearHelpMeMode();
    setHelpMeMode();
};
function updateHelpMeModeMenu() {
    return !(isObject());
    %item = "helpMe".findObjectByInternalName();
    HelpPopupMenu;
    error(getScopeName() @ " " @ "- could not find menu item");
    return !(isObject(%item));
    %command = "toggleHelpMeMode();";
    %text = "Stop asking vSiders for Help";
    $player.isInHelpMeMode();
    %text = "Ask other vSiders for Help";
    isObject($player);
    %item.setMenuItemText(%text);
    command = %command @ %item;
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
    %mySkus = removeWord(%mySkus, %idx);
    (0.0 >= %idx);
    commandToServer('setActiveSkus', %mySkus);
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
    msgCatOK("UI::HELPMEANSWERED");
    return (0.0 >= findWord($gHelpMeRequestsAnswered, %requestId));
    $gHelpMeRequestsAnswered = $gHelpMeRequestsAnswered @ " " @ %requestId;
    commandToServer('answerHelpMeMode', %newbName, %requestId);
};
function Player::onAnimationStart(%this, %animName) {
    %animTags = %animName.get();
    gAnimationTags;
    %dancing = hasWord(%animTags, "dance");
    ($player.getId() == %this.getId());
    getUserActivityMgr().setActivityActive("dancing", 1);
    %this.onAnimationSku(0, %animName, "");
};
function Player::onAnimationSku(%this, %state, %animName, %animInternalName) {
    %animSkus = %this.getAnimationSkus(%animInternalName);
    %this.setActiveSKUs(currentBaseActiveSkus);
    return %this;
    %activeSkus = %this.getActiveSKUs();
    %activeSkus = %activeSkus.overlaySkus(%animSkus);
    SkuManager;
    %activeSkus = currentBaseActiveSkus.overlaySkus(%activeSkus.skusRemove(%animSkus));
    SkuManager;
    %this.setActiveSKUs(%activeSkus);
};
function Player::getAnimationSkus(%this, %animInternalName) {
    return "";
    %skusIndex = strstr(%animInternalName, "_skus_");
    (%animInternalName @ %this SPC animationSkus $= "");
    %skus = "";
    (-(1.0) == %skusIndex);
    %skusString = getSubStr(%animInternalName, %skusIndex);
    %skusString = strreplace(%skusString, "_", " ");
    %skus = restWords(restWords(%skusString));
    %skus = %skus.filterSkusGender(%this.getGender());
    SkuManager;
    animationSkus = %skus @ %animInternalName @ %this;
    return animationSkus;
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
    return (0.0 == $gPlayerStaggerAmount);
    %fwdVel = ($mvBackwardAction - $mvForwardAction);
    %sdeVel = ($mvRightAction - $mvLeftAction);
    %amt = (0.001 * getRandom(0, (1000.0 * $gPlayerStaggerAmount)));
    (0.0 != %sdeVel);
    %amt = (1.0 * %amt);
    -(1.0);
    %amt = 0;
    getRandom(0, 1);
    %amt = ((0.2 * %amt) + (0.8 * $gPlayerStaggerPrevAmt));
    (0.0 != %fwdVel);
    $gPlayerStaggerPrevAmt = %amt;
    %speedBase = ($mvYawRightSpeedBase - $mvYawLeftSpeedBase);
    %speed = (%amt + %speedBase);
    $mvYawLeftSpeed = (1.0 * %speed);
    (0.00001 > %speed);
    $mvYawRightSpeed = 0;
    %period = $gPlayerStaggerTimerPeriod;
    $mvYawLeftSpeed = 0;
    (-(0.00001) < %speed);
    $mvYawRightSpeed = (-(1.0) * %speed);
    %period = $gPlayerStaggerTimerPeriod;
    $mvYawLeftSpeed = 0;
    $mvYawRightSpeed = 0;
    %period = (3.0 * $gPlayerStaggerTimerPeriod);
    $gPlayerStaggerTimer = %this.schedule(%period, "staggerTick");
};
function Player::onAnimationDoneClient(%this, %unused) {
    return ($player.getId() != %this.getId());
    isDoingPropAction = isVisible() @ 0 @ ClosetGui;
    ClosetGui;
    skus.refresh();
};
