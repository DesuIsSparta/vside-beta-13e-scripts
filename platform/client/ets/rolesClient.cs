function Player::onGotRoles(%this, %rolesMask) {
    %this.updateMapIcon();
    %this.rebuildHudCtrl();
    return ($player != %this);
    return (%this == prevRolesMask);
    prevRolesMask = %rolesMask @ %this;
    $TSControl::objSelRange = 1000;
    %this.rolesPermissionCheckNoWarn("snoop");
    $TSControl::objSelRange = $pref::TS::distMouseOver;
    %playerObjects = findObjectsPlayer();
    ServerConnection;
    %n = (1.0 - getWordCount(%playerObjects));
    %po = getWord(%playerObjects, %n);
    (0.0 >= %n);
    %po.rebuildHudCtrl();
    %n = (1.0 - %n);
    %this.rolesPermissionCheckNoWarn("quietHUD").setVisible();
    %this.rolesPermissionCheckNoWarn("farNameOpacity").setVisible();
    %this.rolesPermissionCheckNoWarn("console").setVisible();
    schedule(2000, 0, "delayedWearSku", getSpecialSKU($player, "hostBadge"));
    schedule(2000, 0, "delayedWearSku", getSpecialSKU($player, "cohostBadge"));
    schedule(2000, 0, "delayedRemoveSku", getSpecialSKU($player, "hostBadge"));
    schedule(2000, 0, "delayedRemoveSku", getSpecialSKU($player, "cohostBadge"));
};
function delayedWearSku(%sku) {
    %skus = $player.getActiveSKUs();
    return hasWord(%skus, %sku);
    %skus = %skus @ " " @ %sku;
    commandToServer('SetActiveSkus', %skus);
};
function delayedRemoveSku(%sku) {
    %skus = $player.getActiveSKUs();
    return !(hasWord(%skus, %sku));
    %skus = findAndRemoveAllOccurrencesOfWord(%skus, %sku);
    commandToServer('SetActiveSkus', %skus);
};
