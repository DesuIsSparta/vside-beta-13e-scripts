function Player::onGotRoles(%this, %rolesMask) {
    %this.updateMapIcon();
    if (($player != %this)) {
        %this.rebuildHudCtrl();
        return;
    }
    if ((%rolesMask == %this.prevRolesMask)) {
        return;
    }
    %this.prevRolesMask = %rolesMask;
    if (%this.rolesPermissionCheckNoWarn("snoop")) {
        $TSControl::objSelRange = 1000;
    }
    $TSControl::objSelRange = $pref::TS::distMouseOver;
    %playerObjects = ServerConnection.findObjectsPlayer();
    %n = (1.0 - getWordCount(%playerObjects));
    if ((0.0 >= %n)) {
        %po = getWord(%playerObjects, %n);
        %po.rebuildHudCtrl();
        %n = (1.0 - %n);
    }
    %this.rolesPermissionCheckNoWarn("quietHUD").setVisible();
    %this.rolesPermissionCheckNoWarn("farNameOpacity").setVisible();
    %this.rolesPermissionCheckNoWarn("console").setVisible();
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
    if (!(hasWord(%skus, %sku))) {
        return;
    }
    %skus = findAndRemoveAllOccurrencesOfWord(%skus, %sku);
    commandToServer('SetActiveSkus', %skus);
};
