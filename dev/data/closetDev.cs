$gSnapping_SkuList = "";
$gSnapping_CurIndex = 0;
$gSnapping_MaxIndex = 0;
$gSnapping_ObjViewCtrl = 0;
$gSnapping_CurSku = 0;
$gSnapping_BaseSkus = "";
function ClosetStaffPanel::snapShotAll(%this) {
    %skus = getSkus().filterSkusGender($player.getGender());
    SkuManager;
    SkuManager.setSkus();
    %this.snapShotSkuList(%skus);
};
function ClosetStaffPanel::snapShotSkuList(%this, %skus) {
    0.setVisible();
    ClosetMainObjectView;
    $gSnapping_BaseSkus = getSkus();
    ClosetMainObjectView;
    %skusBody = "";
    %skusClothing = "";
    %n = (1.0 - getWordCount(%skus));
    if ((0.0 >= %n)) {
        %sku = getWord(%skus, %n);
        if (%sku.isBodySku()) {
            %skusBody = %sku @ " " @ %skusBody;
            SkuManager;
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
    %skus = $gSnapping_BaseSkus.overlaySkus($gSnapping_CurSku);
    SkuManager;
    $gSnapping_ObjViewCtrl.setSkus(%skus);
    1.setVisible();
    waitAFrameAndCall("snapping_callingTakeCurrentSnapshot");
};
function snapping_callingTakeCurrentSnapshot() {
    snapping_takeCurrentSnapshot();
    $gSnapping_CurIndex = (1.0 + $gSnapping_CurIndex);
    if (($gSnapping_MaxIndex < $gSnapping_CurIndex)) {
        snapping_prepareNextSnapshot();
    }
    $gSnapping_ObjViewCtrl.setSkus($gSnapping_BaseSkus);
    0.setVisible();
};
function snapping_takeCurrentSnapshot() {
    %desc = descShrt;
    $gSnapping_CurSku.findBySku();
    %curSku = formatInt("%0.5d", $gSnapping_CurSku);
    SkuManager;
    %index = formatInt("%0.5d", $gSnapping_CurIndex);
    %fileName = "";
    %fileName = %fileName @ "dev/data/clothing/";
    %fileName = %fileName @ $player.getGender() @ "/";
    %fileName = %fileName @ $player.getGender();
    %fileName = %fileName @ "_" @ %index;
    %fileName = %fileName @ "_" @ %curSku;
    %fileName = %fileName @ "_" @ %desc;
    %fileName = %fileName @ ".png";
    %fileName.snapshot();
};
function ClosetStaffPanel::adjustLOD(%this, %direction) {
    %val = %direction.changeDetailLevel();
    ClosetMainObjectView;
    (-(1.0) * %val).setText();
    if (!(isObject(itemsScroll))) {
        return getCurrentTab();
    }
    %array = thumbnails;
    itemsScroll;
    if (!(isObject(%array))) {
        error(getScopeName() @ " " @ "- no array");
        return getCurrentTab();
    }
    %n = (1.0 - %array.getCount());
    if ((0.0 >= %n)) {
        %cell = %array.getObject(%n);
        %objectView = objectView;
        %cell;
        %objectView.changeDetailLevel(%direction);
        %n = (1.0 - %n);
    }
};
function ClosetStaffPanel::viewAll(%this) {
    $Player::inventory = getSkus();
    SkuManager;
    $Player::inventory = $Player::inventory.filterSkusGender($player.getGender());
    SkuManager;
    $Player::inventory = $Player::inventory.filterSkusRoles((~(2147483648) & 4294967295));
    SkuManager;
    $gClosetThumbnailsDrawersPrevious = "";
    selectCurrentTab();
    if ((getCurrentTab() SPC name $= "BODY")) {
        if (!(tabBodyInitialized)) {
            fillBodyTab();
        }
        rebuildPopupList();
        update();
    }
    if ((getCurrentTab() SPC name $= "CLOSET")) {
        if (!(tabClosetInitialized)) {
            fillClosetTab();
        }
        update();
    }
    if ((getCurrentTab() SPC name $= "SHOPS")) {
        if (!(tabShopsInitialized)) {
            fillStoreTab();
        }
        refreshStoreTab();
    }
};
