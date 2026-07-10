$gDifSkusCurrentDif = "";
$gDifSkusCurrentBaseSwatch = "";
$gDifSkusCurrentSwatch = "";
$gSwatchPaintingModeOn = 0;
function tryOnMouseOverSwatches(%obj) {
    %swallowed = 0;
    if (objectIsSwatchable(%obj)) {
        %swallowed = 1;
        onMouseOverSwatchObj(%obj);
    }
    onMouseOverSwatchObj(0);
    return %swallowed;
};
function onMouseOverSwatchObj(%obj) {
    $gDifSkusCurrentDif = %obj;
    if (!($gSwatchPaintingModeOn)) {
        %obj = "";
    }
    if (isObject(%obj)) {
        if (($TypeMasks::InteriorObjectType & %obj.getType())) {
            $gDifSkusCurrentBaseSwatch = PlayGui.getLastRayCastTextureName().findByTexture();
            SkuManager;
        }
        if ((0.0 > %obj.getInventoryNuggetSKU())) {
            $gDifSkusCurrentBaseSwatch = "obj" @ " " @ %obj;
        }
        $TSControl::objSelContinuous = 1;
    }
    $gDifSkusCurrentBaseSwatch = 0;
    $TSControl::objSelContinuous = 0;
    updateSwatchBrush();
};
function updateSwatchBrush() {
    if (!(isObject(geSwatchBrushContainer))) {
        new GuiBitmapCtrl(geSwatchBrushObjectBitmap) {
            profile = new GuiBitmapCtrl(geSwatchBrushObjectShadowBitmap) {
            profile = "ETSNonModalProfile";
            position = "1 34";
            extent = "33 33";
            modulationColor = "0 0 0 200";
        }; @ "ETSNonModalProfile";
            position = "0 34";
            extent = "32 32";
            modulationColor = "255 255 255 255";
        };
        new GuiControl(geSwatchBrushContainer) {
            profile = "SwatchBrushProfile";
            extent = "72 72";
        };
        PlayGui.add(geSwatchBrushContainer);
    }
    if (!($gSwatchPaintingModeOn)) {
    }
    if (!(objectIsSwatchable($gDifSkusCurrentDif))) {
    }
    if ((new GuiMLTextCtrl(geSwatchBrushText1) {
        profile = new GuiMLTextCtrl(geSwatchBrushText2) {
        profile = new GuiBitmapCtrl(geSwatchBrushBitmap) {
        profile = "ETSNonModalProfile";
        extent = "70 70";
        position = "1 1";
        modulationColor = "255 255 255 200";
    }; @ "GuiMLTextModelessProfile";
        position = "2 57";
        extent = "70 16";
    }; @ "GuiMLTextModelessProfile";
        position = "1 58";
        extent = "70 16";
    }; @ " " @ $gDifSkusCurrentBaseSwatch $= 0)) {
    }
    if (($gDifSkusCurrentSwatch $= 0)) {
        geSwatchBrushContainer.setVisible(0);
        Canvas.setCursor(ETSDefaultCursor);
        return geSwatchBrushContainer;
    }
    %pos = (2.0 + ((2.0 / getWord(geSwatchBrushContainer.getExtent(geSwatchBrushContainer), 0)) - getWord(geSwatchBrushContainer.getCursorPos(Canvas), 0))) @ " " @ (15.0 + getWord(geSwatchBrushContainer.getCursorPos(Canvas), 1));
    %pos.reposition();
    1.setVisible();
    getBitmapFilename("swatch", $gDifSkusCurrentSwatch.findBySku().getTxtrNames()).setBitmap();
    if ((SkuManager @ " " @ firstWord($gDifSkusCurrentBaseSwatch) $= "obj")) {
        %sku = $gDifSkusCurrentDif.getInventoryNuggetSKU();
        geSwatchBrushBitmap;
        %text = "";
        geSwatchBrushContainer;
        %bitmap = CSBrowser::getThumbnailPathForSku(0, %sku, 32);
        geSwatchBrushContainer;
    }
    %sku = $gDifSkusCurrentBaseSwatch;
    %siBase = %sku.findBySku();
    SkuManager;
    %text = %siBase.descShrt;
    %bitmap = "";
    "<just:left> <color:ddff11>" @ %text.setText();
    "<just:left> <color:000000>" @ %text.setText();
    %bitmap.setBitmap();
    %bitmap.setBitmap();
    Canvas.setCursor(ETSHandCursor);
};
function objectIsSwatchable(%obj) {
    if (!(isObject(%obj))) {
        return 0;
    }
    if (($TypeMasks::InteriorObjectType & %obj.getType())) {
        return 1;
    }
    %sku = %obj.getInventoryNuggetSKU();
    if ((1.0 < %sku)) {
        return 0;
    }
    return %sku.isSwatchableSku();
};
function onLeftClickSwatch(%obj) {
    if (!($gSwatchPaintingModeOn)) {
        return;
    }
    if (!(objectIsSwatchable(%obj))) {
        return;
    }
    if (($TypeMasks::InteriorObjectType & %obj.getType())) {
        difSkusFixSkuPair(%obj, $gDifSkusCurrentBaseSwatch, $gDifSkusCurrentSwatch);
    }
    objSkusFixSku(%obj, $gDifSkusCurrentSwatch);
};
function onRightClickDownInterior(%obj) {
};
function onRightClickUpInterior(%obj) {
    if (CSFurnitureMover.isInEditMode()) {
        FurnitureItemContextMenu.initWithObject();
        FurnitureItemContextMenu.showAtCursor();
    }
};
function onMouseWheelDifSkus(%val) {
    if (!($gSwatchPaintingModeOn)) {
        return 0;
    }
    if (($gDifSkusCurrentDif $= 0)) {
    }
    if (($gDifSkusCurrentBaseSwatch $= 0)) {
        return 0;
    }
    %numSwatchSkus = getWordCount($gDifSkusSwatchSkusViewable);
    if ((1.0 < %numSwatchSkus)) {
        -(1.0).selectCell();
        return geSwatchesPanel;
    }
    if ((0.0 != $gDifSkusCurrentSwatch)) {
        %ndx = findWord($gDifSkusSwatchSkusViewable, $gDifSkusCurrentSwatch);
    }
    %ndx = -(1.0);
    if ((0.0 < %val)) {
    }
    %val = -(1.0);
    1;
    %ndx = (%val - %ndx);
    if ((0.0 < %ndx)) {
        %ndx = (1.0 - %numSwatchSkus);
    }
    if ((%numSwatchSkus >= %ndx)) {
        %ndx = 0;
    }
    $gDifSkusCurrentSwatch = getWord($gDifSkusSwatchSkusViewable, %ndx);
    if (isObject(geSwatchesPanel)) {
    }
    if (geSwatchesPanel.isVisible()) {
        $gDifSkusCurrentSwatch.selectSwatch();
    }
    updateSwatchBrush();
    if (Canvas.getMouseButtonDown()) {
        onLeftClickSwatch($gDifSkusCurrentDif);
    }
    return 1;
};
function difSkusFixSkuPair(%obj, %base, %rplc) {
    if (!(isObject(%obj))) {
        return;
    }
    if ((0.0 <= %base)) {
        return;
    }
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if (!(CustomSpaceClient::isOwner())) {
        error(getScopeName() @ " " @ "- not owner, what are we doing here?");
        $gSwatchPaintingModeOn = 0;
        updateSwatchBrush();
        return;
    }
    %oldPairs = %obj.getActiveSkuPairs();
    %newPairs = %oldPairs.setSkuPair(%base, %rplc);
    SkuManager;
    $gDifSkusCurrentBaseSwatch = %base;
    difSkusPairsToServer(%obj, %newPairs);
    difSkusSetActiveSkuPairs(%obj, %newPairs);
};
function difSkusPairsToServer(%obj, %newPairs) {
    if (!(CustomSpaceClient::GetSpaceImIn() $= "")) {
        commandToServer('CSSetSwatches', CustomSpaceClient::GetSpaceImIn(), %newPairs);
        log("general", "debug", getScopeName() @ " " @ "setting swatches through custom space.." @ " " @ %newPairs);
    }
    %ghostID = %obj.getGhostID();
    ServerConnection;
    commandToServer('fixSwatches', %ghostID, %newPairs);
    log("general", "debug", getScopeName() @ " " @ "setting swatches through interior object itself, not through custom space.." @ " " @ %newPairs);
};
function difSkusSetActiveSkuPairs(%obj, %pairs) {
    %obj.setActiveSkuPairs(%pairs);
};
function objSkusFixSku(%obj, %sku) {
    if (!(isObject(%obj))) {
        return;
    }
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if (!(CustomSpaceClient::isOwner())) {
        error(getScopeName() @ " " @ "- not owner, what are we doing here?");
        $gSwatchPaintingModeOn = 0;
        updateSwatchBrush();
        return;
    }
    objSkusToServer(%obj, %sku);
    %obj.setActiveSku(%sku);
};
function objSkusToServer(%obj, %sku) {
    if (!(CustomSpaceClient::GetSpaceImIn() $= "")) {
        commandToServer('CSSetFurnishingSku', CustomSpaceClient::GetSpaceImIn(), %obj.getGhostID(), %sku);
        log("general", "debug", getScopeName() @ " " @ "setting swatches through custom space.." @ " " @ %sku);
    }
    %ghostID = %obj.getGhostID();
    ServerConnection;
    commandToServer('fixSwatch', %ghostID, %sku);
    log("general", "debug", getScopeName() @ " " @ "setting swatches through object itself, not through custom space.." @ " " @ %sku);
};
function difSkusResetConfirm() {
    MessageBoxYesNo("Default Materials & Surfaces", , "difSkusResetDefaults();", "");
};
function difSkusReset() {
    %obj = $gDifSkusCurrentDif;
    if (!(isObject(%obj))) {
        return;
    }
    %newPairs = "";
    difSkusPairsToServer(%obj, %newPairs);
    difSkusSetActiveSkuPairs(%obj, %newPairs);
};
function difSkusResetDefaults() {
    %obj = $gDifSkusCurrentDif;
    if (!(isObject(%obj))) {
        return;
    }
    commandToServer('CSSetDefaultSwatches', CustomSpaceClient::GetSpaceImIn());
};
function difSkusRandomizeConfirm() {
    MessageBoxYesNo("Randomize Materials & Surfaces", , "difSkusRandomize();", "");
};
function difSkusRandomize() {
    %obj = $gDifSkusCurrentDif;
    if (!(isObject(%obj))) {
        return;
    }
    %baseSkus = %obj.getBaseSkus();
    if (($gDifSkusSwatchSkus $= "")) {
        $gDifSkusSwatchSkus = "swatch".getSkusType();
        SkuManager;
    }
    %newPairs = "";
    %delim = "";
    %n = (1.0 - getWordCount(%baseSkus));
    if ((0.0 >= %n)) {
        %baseSku = getWord(%baseSkus, %n);
        %randSku = getRandomWord($gDifSkusSwatchSkus);
        %newPairs = %newPairs @ %delim @ %baseSku @ " " @ %randSku;
        %delim = " ";
        %n = (1.0 - %n);
    }
    difSkusPairsToServer(%obj, %newPairs);
    difSkusSetActiveSkuPairs(%obj, %newPairs);
};
