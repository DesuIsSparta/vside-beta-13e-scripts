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
    if (!$gSwatchPaintingModeOn) {
        %obj = "";
    }
    if (isObject(%obj)) {
        if ((%obj.getType() & $TypeMasks::InteriorObjectType)) {
            $gDifSkusCurrentBaseSwatch = SkuManager.findByTexture(PlayGui.getLastRayCastTextureName());
        }
        if ((%obj.getInventoryNuggetSKU() > 0.0)) {
            $gDifSkusCurrentBaseSwatch = "obj" @ " " @ %obj;
        }
        $TSControl::objSelContinuous = 1;
    }
    $gDifSkusCurrentBaseSwatch = 0;
    $TSControl::objSelContinuous = 0;
    updateSwatchBrush();
};
function updateSwatchBrush() {
    if (!isObject(geSwatchBrushContainer)) {
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
    if ((!$gSwatchPaintingModeOn || !objectIsSwatchable($gDifSkusCurrentDif) || ((new GuiMLTextCtrl(geSwatchBrushText1) {
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
    }; @ " " @ $gDifSkusCurrentBaseSwatch) $= 0)) || ($gDifSkusCurrentSwatch $= 0)) {
        geSwatchBrushContainer.setVisible(0);
        Canvas.setCursor(ETSDefaultCursor);
        return;
    }
    %pos = ((getWord(Canvas.getCursorPos(), 0) - (getWord(geSwatchBrushContainer.getExtent(), 0) / 2.0)) + 2.0) @ " " @ (getWord(Canvas.getCursorPos(), 1) + 15.0);
    geSwatchBrushContainer.reposition(%pos);
    geSwatchBrushContainer.setVisible(1);
    geSwatchBrushBitmap.setBitmap(getBitmapFilename("swatch", SkuManager.findBySku($gDifSkusCurrentSwatch).getTxtrNames()));
    if ((firstWord($gDifSkusCurrentBaseSwatch) $= "obj")) {
        %sku = $gDifSkusCurrentDif.getInventoryNuggetSKU();
        %text = "";
        %bitmap = CSBrowser::getThumbnailPathForSku(0, %sku, 32);
    }
    %sku = $gDifSkusCurrentBaseSwatch;
    %siBase = SkuManager.findBySku(%sku);
    %text = %siBase.descShrt;
    %bitmap = "";
    geSwatchBrushText1.setText("<just:left> <color:ddff11>" @ %text);
    geSwatchBrushText2.setText("<just:left> <color:000000>" @ %text);
    geSwatchBrushObjectBitmap.setBitmap(%bitmap);
    geSwatchBrushObjectShadowBitmap.setBitmap(%bitmap);
    Canvas.setCursor(ETSHandCursor);
};
function objectIsSwatchable(%obj) {
    if (!isObject(%obj)) {
        return 0;
    }
    if ((%obj.getType() & $TypeMasks::InteriorObjectType)) {
        return 1;
    }
    %sku = %obj.getInventoryNuggetSKU();
    if ((%sku < 1.0)) {
        return 0;
    }
    return SkuManager.isSwatchableSku(%sku);
};
function onLeftClickSwatch(%obj) {
    if (!$gSwatchPaintingModeOn) {
        return;
    }
    if (!objectIsSwatchable(%obj)) {
        return;
    }
    if ((%obj.getType() & $TypeMasks::InteriorObjectType)) {
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
    if (!$gSwatchPaintingModeOn) {
        return 0;
    }
    if (($gDifSkusCurrentDif $= 0) || ($gDifSkusCurrentBaseSwatch $= 0)) {
        return 0;
    }
    %numSwatchSkus = getWordCount($gDifSkusSwatchSkusViewable);
    if ((%numSwatchSkus < 1.0)) {
        geSwatchesPanel.selectCell(-(1.0));
        return;
    }
    if (($gDifSkusCurrentSwatch != 0.0)) {
        %ndx = findWord($gDifSkusSwatchSkusViewable, $gDifSkusCurrentSwatch);
    }
    %ndx = -(1.0);
    if ((%val < 0.0)) {
    }
    %val = -(1.0);
    1;
    %ndx = (%ndx - %val);
    if ((%ndx < 0.0)) {
        %ndx = (%numSwatchSkus - 1.0);
    }
    if ((%ndx >= %numSwatchSkus)) {
        %ndx = 0;
    }
    $gDifSkusCurrentSwatch = getWord($gDifSkusSwatchSkusViewable, %ndx);
    if (isObject(geSwatchesPanel)) {
    }
    if (geSwatchesPanel.isVisible()) {
        geSwatchesPanel.selectSwatch($gDifSkusCurrentSwatch);
    }
    updateSwatchBrush();
    if (Canvas.getMouseButtonDown()) {
        onLeftClickSwatch($gDifSkusCurrentDif);
    }
    return 1;
};
function difSkusFixSkuPair(%obj, %base, %rplc) {
    if (!isObject(%obj)) {
        return;
    }
    if ((%base <= 0.0)) {
        return;
    }
    if ((CustomSpaceClient::GetSpaceImIn() $= "") || !CustomSpaceClient::isOwner()) {
        error(getScopeName() @ " " @ "- not owner, what are we doing here?");
        $gSwatchPaintingModeOn = 0;
        updateSwatchBrush();
        return;
    }
    %oldPairs = %obj.getActiveSkuPairs();
    %newPairs = SkuManager.setSkuPair(%oldPairs, %base, %rplc);
    $gDifSkusCurrentBaseSwatch = %base;
    difSkusPairsToServer(%obj, %newPairs);
    difSkusSetActiveSkuPairs(%obj, %newPairs);
};
function difSkusPairsToServer(%obj, %newPairs) {
    if (!(CustomSpaceClient::GetSpaceImIn() $= "")) {
        commandToServer('CSSetSwatches', CustomSpaceClient::GetSpaceImIn(), %newPairs);
        log("general", "debug", getScopeName() @ " " @ "setting swatches through custom space.." @ " " @ %newPairs);
    }
    %ghostID = ServerConnection.getGhostID(%obj);
    commandToServer('fixSwatches', %ghostID, %newPairs);
    log("general", "debug", getScopeName() @ " " @ "setting swatches through interior object itself, not through custom space.." @ " " @ %newPairs);
};
function difSkusSetActiveSkuPairs(%obj, %pairs) {
    %obj.setActiveSkuPairs(%pairs);
};
function objSkusFixSku(%obj, %sku) {
    if (!isObject(%obj)) {
        return;
    }
    if ((CustomSpaceClient::GetSpaceImIn() $= "") || !CustomSpaceClient::isOwner()) {
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
    %ghostID = ServerConnection.getGhostID(%obj);
    commandToServer('fixSwatch', %ghostID, %sku);
    log("general", "debug", getScopeName() @ " " @ "setting swatches through object itself, not through custom space.." @ " " @ %sku);
};
function difSkusResetConfirm() {
    MessageBoxYesNo("Default Materials & Surfaces", $MsgCat::custSpace["SWATCHES_RESET"], "difSkusResetDefaults();", "");
};
function difSkusReset() {
    %obj = $gDifSkusCurrentDif;
    if (!isObject(%obj)) {
        return;
    }
    %newPairs = "";
    difSkusPairsToServer(%obj, %newPairs);
    difSkusSetActiveSkuPairs(%obj, %newPairs);
};
function difSkusResetDefaults() {
    %obj = $gDifSkusCurrentDif;
    if (!isObject(%obj)) {
        return;
    }
    commandToServer('CSSetDefaultSwatches', CustomSpaceClient::GetSpaceImIn());
};
function difSkusRandomizeConfirm() {
    MessageBoxYesNo("Randomize Materials & Surfaces", $MsgCat::custSpace["SWATCHES_RANDOMIZE"], "difSkusRandomize();", "");
};
function difSkusRandomize() {
    %obj = $gDifSkusCurrentDif;
    if (!isObject(%obj)) {
        return;
    }
    %baseSkus = %obj.getBaseSkus();
    if (($gDifSkusSwatchSkus $= "")) {
        $gDifSkusSwatchSkus = SkuManager.getSkusType("swatch");
    }
    %newPairs = "";
    %delim = "";
    %n = (getWordCount(%baseSkus) - 1.0);
    while ((%n >= 0.0)) {
        %baseSku = getWord(%baseSkus, %n);
        %randSku = getRandomWord($gDifSkusSwatchSkus);
        %newPairs = %newPairs @ %delim @ %baseSku @ " " @ %randSku;
        %delim = " ";
        %n = (%n - 1.0);
    }
    difSkusPairsToServer(%obj, %newPairs);
    difSkusSetActiveSkuPairs(%obj, %newPairs);
};
