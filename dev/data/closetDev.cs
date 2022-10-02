$gSnapping_SkuList = "";
$gSnapping_CurIndex = 0;
$gSnapping_MaxIndex = 0;
$gSnapping_ObjViewCtrl = 0;
$gSnapping_CurSku = 0;
$gSnapping_BaseSkus = "";
function ClosetStaffPanel::snapShotAll(%this)
{
    %skus = SkuManager.filterSkusGender(SkuManager.getSkus(), $player.getGender());
    ClosetMainObjectView.setSkus($gNewStockOutfits[$player.getGender() @ "F"]);
    %this.snapShotSkuList(%skus);
    return ;
}
function ClosetStaffPanel::snapShotSkuList(%this, %skus)
{
    ClosetStaffPanel.setVisible(0);
    $gSnapping_ObjViewCtrl = ClosetMainObjectView;
    $gSnapping_BaseSkus = ClosetMainObjectView.getSkus();
    %skusBody = "";
    %skusClothing = "";
    %n = getWordCount(%skus) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%skus, %n);
        if (SkuManager.isBodySku(%sku))
        {
            %skusBody = %sku SPC %skusBody;
        }
        else
        {
            %skusClothing = %sku SPC %skusClothing;
        }
        %n = %n - 1;
    }
    %skus = %skusBody SPC %skusClothing;
    $gSnapping_SkuList = %skus;
    $gSnapping_CurIndex = 0;
    $gSnapping_MaxIndex = getWordCount($gSnapping_SkuList) - 1;
    snapping_prepareNextSnapshot();
    return ;
}
function snapping_prepareNextSnapshot()
{
    $gSnapping_CurSku = getWord($gSnapping_SkuList, $gSnapping_CurIndex);
    %skus = SkuManager.overlaySkus($gSnapping_BaseSkus, $gSnapping_CurSku);
    $gSnapping_ObjViewCtrl.setSkus(%skus);
    ClosetMainObjectSnapshotBackdrop.setVisible(1);
    waitAFrameAndCall("snapping_callingTakeCurrentSnapshot");
    return ;
}
function snapping_callingTakeCurrentSnapshot()
{
    snapping_takeCurrentSnapshot();
    $gSnapping_CurIndex = $gSnapping_CurIndex + 1;
    if ($gSnapping_CurIndex < $gSnapping_MaxIndex)
    {
        snapping_prepareNextSnapshot();
    }
    else
    {
        $gSnapping_ObjViewCtrl.setSkus($gSnapping_BaseSkus);
        ClosetMainObjectSnapshotBackdrop.setVisible(0);
    }
    return ;
}
function snapping_takeCurrentSnapshot()
{
    %desc = SkuManager.findBySku($gSnapping_CurSku).descShrt;
    %curSku = formatInt("%0.5d", $gSnapping_CurSku);
    %index = formatInt("%0.5d", $gSnapping_CurIndex);
    %fileName = "";
    %fileName = %fileName @ "dev/data/clothing/";
    %fileName = %fileName @ $player.getGender() @ "/";
    %fileName = %fileName @ $player.getGender();
    %fileName = %fileName @ "_" @ %index;
    %fileName = %fileName @ "_" @ %curSku;
    %fileName = %fileName @ "_" @ %desc;
    %fileName = %fileName @ ".png";
    ClosetMainObjectSnapshotBackdrop.snapshot(%fileName);
    return ;
}
function ClosetStaffPanel::adjustLOD(%this, %direction)
{
    %val = ClosetMainObjectView.changeDetailLevel(%direction);
    closetStaffLODLabelButton.setText(%val * -1);
    if (!isObject(ClosetTabs.getCurrentTab().itemsScroll))
    {
        return ;
    }
    %array = ClosetTabs.getCurrentTab().itemsScroll.thumbnails;
    if (!isObject(%array))
    {
        error(getScopeName() SPC "- no array");
        return ;
    }
    %n = %array.getCount() - 1;
    while (%n >= 0)
    {
        %cell = %array.getObject(%n);
        %objectView = %cell.objectView;
        %objectView.changeDetailLevel(%direction);
        %n = %n - 1;
    }
}

function ClosetStaffPanel::viewAll(%this)
{
    $Player::inventory = SkuManager.getSkus();
    $Player::inventory = SkuManager.filterSkusGender($Player::inventory, $player.getGender());
    $Player::inventory = SkuManager.filterSkusRoles($Player::inventory, 4294967295 & ~2147483648);
    $gClosetThumbnailsDrawersPrevious = "";
    ClosetTabs.selectCurrentTab();
    if (ClosetTabs.getCurrentTab().name $= "BODY")
    {
        if (!ClosetTabs.tabBodyInitialized)
        {
            ClosetTabs.fillBodyTab();
        }
        BodyFeaturesPopup.rebuildPopupList();
        BodyItemsFrame.update();
    }
    if (ClosetTabs.getCurrentTab().name $= "CLOSET")
    {
        if (!ClosetTabs.tabClosetInitialized)
        {
            ClosetTabs.fillClosetTab();
        }
        ClosetItemsFrame.update();
    }
    if (ClosetTabs.getCurrentTab().name $= "SHOPS")
    {
        if (!ClosetTabs.tabShopsInitialized)
        {
            ClosetTabs.fillStoreTab();
        }
        ClosetTabs.refreshStoreTab();
    }
    return ;
}
