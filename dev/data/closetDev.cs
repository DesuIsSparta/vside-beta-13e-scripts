$gSnapping_SkuList = "";
$gSnapping_CurIndex = 0;
$gSnapping_MaxIndex = 0;
$gSnapping_ObjViewCtrl = 0;
$gSnapping_CurSku = 0;
$gSnapping_BaseSkus = "";
function ClosetStaffPanel::snapShotAll(%this) {
    %skus = $player.getGender().filterSkusGender(SkuManager, SkuManager.getSkus());
    ClosetMainObjectView.setSkus();
    %skus.snapShotSkuList(%this);
};
function ClosetStaffPanel::snapShotSkuList(%this, %skus) {
    0.setVisible(ClosetStaffPanel);
    ClosetMainObjectView;
    $gSnapping_BaseSkus = ClosetMainObjectView.getSkus();
    %skusBody = "";
    %skusClothing = "";
    %n = (getWordCount(%skus) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%skus, %n);
        if (%sku.isBodySku(SkuManager)) {
            %skusBody = %sku @ " " @ %skusBody;
        }
        %skusClothing = %sku @ " " @ %skusClothing;
        %n = (%n - 1.0);
    }
    %skus = %skusBody @ " " @ %skusClothing;
    (%n >= 0.0);
    $gSnapping_SkuList = %skus;
    $gSnapping_CurIndex = 0;
    $gSnapping_MaxIndex = (getWordCount($gSnapping_SkuList) - 1.0);
    snapping_prepareNextSnapshot();
};
function snapping_prepareNextSnapshot() {
    $gSnapping_CurSku = getWord($gSnapping_SkuList, $gSnapping_CurIndex);
    %skus = $gSnapping_CurSku.overlaySkus(SkuManager, $gSnapping_BaseSkus);
    %skus.setSkus($gSnapping_ObjViewCtrl);
    1.setVisible(ClosetMainObjectSnapshotBackdrop);
    waitAFrameAndCall("snapping_callingTakeCurrentSnapshot");
};
function snapping_callingTakeCurrentSnapshot() {
    snapping_takeCurrentSnapshot();
    $gSnapping_CurIndex = ($gSnapping_CurIndex + 1.0);
    if (($gSnapping_CurIndex < $gSnapping_MaxIndex)) {
        snapping_prepareNextSnapshot();
    }
    $gSnapping_BaseSkus.setSkus($gSnapping_ObjViewCtrl);
    0.setVisible(ClosetMainObjectSnapshotBackdrop);
};
function snapping_takeCurrentSnapshot() {
    %desc = $gSnapping_CurSku.findBySku(SkuManager).descShrt;
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
    %fileName.snapshot(ClosetMainObjectSnapshotBackdrop);
};
function ClosetStaffPanel::adjustLOD(%this, %direction) {
    %val = %direction.changeDetailLevel(ClosetMainObjectView);
    (%val * -(1.0)).setText(closetStaffLODLabelButton);
    if (!(isObject(ClosetTabs.getCurrentTab().itemsScroll))) {
        return;
    }
    %array = ClosetTabs.getCurrentTab().itemsScroll.thumbnails;
    if (!(isObject(%array))) {
        error(getScopeName() @ " " @ "- no array");
        return;
    }
    %n = (%array.getCount() - 1.0);
    while ((%n >= 0.0)) {
        %cell = %n.getObject(%array);
        %objectView = %cell.objectView;
        %direction.changeDetailLevel(%objectView);
        %n = (%n - 1.0);
    }
};
function ClosetStaffPanel::viewAll(%this) {
    $Player::inventory = SkuManager.getSkus();
    $Player::inventory = $player.getGender().filterSkusGender(SkuManager, $Player::inventory);
    $Player::inventory = (4294967295 & ~(2147483648)).filterSkusRoles(SkuManager, $Player::inventory);
    $gClosetThumbnailsDrawersPrevious = "";
    ClosetTabs.selectCurrentTab();
    if ((ClosetTabs.getCurrentTab().name $= "BODY")) {
        if (!(ClosetTabs.getCurrentTab().tabBodyInitialized)) {
            ClosetTabs.fillBodyTab();
        }
        BodyFeaturesPopup.rebuildPopupList();
        BodyItemsFrame.update();
    }
    if ((ClosetTabs @ " " @ ClosetTabs.getCurrentTab().name $= "CLOSET")) {
        if (!(ClosetTabs.getCurrentTab().tabClosetInitialized)) {
            ClosetTabs.fillClosetTab();
        }
        ClosetItemsFrame.update();
    }
    if ((ClosetTabs @ " " @ ClosetTabs.getCurrentTab().name $= "SHOPS")) {
        if (!(ClosetTabs.getCurrentTab().tabShopsInitialized)) {
            ClosetTabs.fillStoreTab();
        }
        ClosetTabs.refreshStoreTab();
    }
};
