$gSnapping_SkuList = "";
$gSnapping_CurIndex = 0;
$gSnapping_MaxIndex = 0;
$gSnapping_ObjViewCtrl = 0;
$gSnapping_CurSku = 0;
$gSnapping_BaseSkus = "";
function ClosetStaffPanel::snapShotAll(%this) {
    %skus = SkuManager.filterSkusGender(SkuManager.getSkus(), $player.getGender());
    ClosetMainObjectView.setSkus();
    %this.snapShotSkuList(%skus);
};
function ClosetStaffPanel::snapShotSkuList(%this, %skus) {
    ClosetStaffPanel.setVisible(0);
    ClosetMainObjectView;
    $gSnapping_BaseSkus = ClosetMainObjectView.getSkus();
    %skusBody = "";
    %skusClothing = "";
    %n = (1.0 - getWordCount(%skus));
    if ((0.0 >= %n)) {
        %sku = getWord(%skus, %n);
        if (SkuManager.isBodySku(%sku)) {
            %skusBody = %sku @ " " @ %skusBody;
        }
        %skusClothing = %sku @ " " @ %skusClothing;
        %n = (1.0 - %n);
    }
    %skus = %skusBody @ " " @ %skusClothing;
    (0.0 >= %n);
    $gSnapping_SkuList = %skus;
    $gSnapping_CurIndex = 0;
    $gSnapping_MaxIndex = (1.0 - getWordCount($gSnapping_SkuList));
    snapping_prepareNextSnapshot();
};
function snapping_prepareNextSnapshot() {
    $gSnapping_CurSku = getWord($gSnapping_SkuList, $gSnapping_CurIndex);
    %skus = SkuManager.overlaySkus($gSnapping_BaseSkus, $gSnapping_CurSku);
    $gSnapping_ObjViewCtrl.setSkus(%skus);
    ClosetMainObjectSnapshotBackdrop.setVisible(1);
    waitAFrameAndCall("snapping_callingTakeCurrentSnapshot");
};
function snapping_callingTakeCurrentSnapshot() {
    snapping_takeCurrentSnapshot();
    $gSnapping_CurIndex = (1.0 + $gSnapping_CurIndex);
    if (($gSnapping_MaxIndex < $gSnapping_CurIndex)) {
        snapping_prepareNextSnapshot();
    }
    $gSnapping_ObjViewCtrl.setSkus($gSnapping_BaseSkus);
    ClosetMainObjectSnapshotBackdrop.setVisible(0);
};
function snapping_takeCurrentSnapshot() {
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
};
function ClosetStaffPanel::adjustLOD(%this, %direction) {
    %val = ClosetMainObjectView.changeDetailLevel(%direction);
    closetStaffLODLabelButton.setText((-(1.0) * %val));
    if (!(isObject(ClosetTabs.getCurrentTab().itemsScroll))) {
        return;
    }
    %array = ClosetTabs.getCurrentTab().itemsScroll.thumbnails;
    if (!(isObject(%array))) {
        error(getScopeName() @ " " @ "- no array");
        return;
    }
    %n = (1.0 - %array.getCount());
    if ((0.0 >= %n)) {
        %cell = %array.getObject(%n);
        %objectView = %cell.objectView;
        %objectView.changeDetailLevel(%direction);
        %n = (1.0 - %n);
    }
};
function ClosetStaffPanel::viewAll(%this) {
    $Player::inventory = SkuManager.getSkus();
    $Player::inventory = SkuManager.filterSkusGender($Player::inventory, $player.getGender());
    $Player::inventory = SkuManager.filterSkusRoles($Player::inventory, (~(2147483648) & 4294967295));
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
