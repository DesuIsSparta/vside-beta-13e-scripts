function Player::onGotRoles(%this, %rolesMask)
{
    %this.updateMapIcon();
    if (%this != $player)
    {
        %this.rebuildHudCtrl();
        return ;
    }
    if (%this.prevRolesMask == %rolesMask)
    {
        return ;
    }
    %this.prevRolesMask = %rolesMask;
    if (%this.rolesPermissionCheckNoWarn("snoop"))
    {
        $TSControl::objSelRange = 1000;
    }
    else
    {
        $TSControl::objSelRange = $pref::TS::distMouseOver;
    }
    %playerObjects = ServerConnection.findObjectsPlayer();
    %n = getWordCount(%playerObjects) - 1;
    while (%n >= 0)
    {
        %po = getWord(%playerObjects, %n);
        %po.rebuildHudCtrl();
        %n = %n - 1;
    }
    HUDHideChatCheckBox.setVisible(%this.rolesPermissionCheckNoWarn("quietHUD"));
    FarNameOpacityCtrl.setVisible(%this.rolesPermissionCheckNoWarn("farNameOpacity"));
    optionsPanelAlertOnLogCtrl.setVisible(%this.rolesPermissionCheckNoWarn("console"));
    if (%this.hasRoleString("host"))
    {
        schedule(2000, 0, "delayedWearSku", getSpecialSKU($player, "hostBadge"));
    }
    else
    {
        if (%this.hasRoleString("cohost"))
        {
            schedule(2000, 0, "delayedWearSku", getSpecialSKU($player, "cohostBadge"));
        }
        else
        {
            schedule(2000, 0, "delayedRemoveSku", getSpecialSKU($player, "hostBadge"));
            schedule(2000, 0, "delayedRemoveSku", getSpecialSKU($player, "cohostBadge"));
        }
    }
    return ;
}
function delayedWearSku(%sku)
{
    %skus = $player.getActiveSKUs();
    if (hasWord(%skus, %sku))
    {
        return ;
    }
    %skus = %skus SPC %sku;
    commandToServer('SetActiveSkus', %skus);
    return ;
}
function delayedRemoveSku(%sku)
{
    %skus = $player.getActiveSKUs();
    if (!hasWord(%skus, %sku))
    {
        return ;
    }
    %skus = findAndRemoveAllOccurrencesOfWord(%skus, %sku);
    commandToServer('SetActiveSkus', %skus);
    return ;
}
