function Player::onGotRoles(%this, %rolesMask) {
    %this.updateMapIcon();
    if ((%this != $player)) {
        %this.rebuildHudCtrl();
        return;
    }
    if ((%this.prevRolesMask == %rolesMask)) {
        return;
    }
    %this.prevRolesMask = %rolesMask;
    if ("snoop".rolesPermissionCheckNoWarn(%this)) {
        $TSControl::objSelRange = 1000;
    }
    $TSControl::objSelRange = $pref::TS::distMouseOver;
    %playerObjects = ServerConnection.findObjectsPlayer();
    %n = (getWordCount(%playerObjects) - 1.0);
    while ((%n >= 0.0)) {
        %po = getWord(%playerObjects, %n);
        %po.rebuildHudCtrl();
        %n = (%n - 1.0);
    }
    "quietHUD".rolesPermissionCheckNoWarn(%this).setVisible(HUDHideChatCheckBox);
    "farNameOpacity".rolesPermissionCheckNoWarn(%this).setVisible(FarNameOpacityCtrl);
    "console".rolesPermissionCheckNoWarn(%this).setVisible(optionsPanelAlertOnLogCtrl);
    if ("host".hasRoleString(%this)) {
        schedule(2000, 0, "delayedWearSku", getSpecialSKU($player, "hostBadge"));
    }
    if ("cohost".hasRoleString(%this)) {
        schedule(2000, 0, "delayedWearSku", getSpecialSKU($player, "cohostBadge"));
    }
    schedule(2000, 0, "delayedRemoveSku", getSpecialSKU($player, "hostBadge"));
    schedule(2000, 0, "delayedRemoveSku", getSpecialSKU($player, "cohostBadge"));
};
function delayedWearSku(%sku) {
    %skus = $player.getActiveSKUs();
    if (hasWord(%skus, %sku)) {
        return;
    }
    %skus = %skus @ " " @ %sku;
    commandToServer('SetActiveSkus', %skus);
};
function delayedRemoveSku(%sku) {
    %skus = $player.getActiveSKUs();
    if (!(hasWord(%skus, %sku))) {
        return;
    }
    %skus = findAndRemoveAllOccurrencesOfWord(%skus, %sku);
    commandToServer('SetActiveSkus', %skus);
};
