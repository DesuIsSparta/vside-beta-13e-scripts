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
    if (%this.rolesPermissionCheckNoWarn("snoop")) {
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
    HUDHideChatCheckBox.setVisible(%this.rolesPermissionCheckNoWarn("quietHUD"));
    FarNameOpacityCtrl.setVisible(%this.rolesPermissionCheckNoWarn("farNameOpacity"));
    optionsPanelAlertOnLogCtrl.setVisible(%this.rolesPermissionCheckNoWarn("console"));
    if (%this.hasRoleString("host")) {
        schedule(2000, 0, "delayedWearSku", getSpecialSKU($player, "hostBadge"));
    }
    if (%this.hasRoleString("cohost")) {
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
    if (!hasWord(%skus, %sku)) {
        return;
    }
    %skus = findAndRemoveAllOccurrencesOfWord(%skus, %sku);
    commandToServer('SetActiveSkus', %skus);
};
