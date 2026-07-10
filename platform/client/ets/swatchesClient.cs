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
            $gDifSkusCurrentBaseSwatch = getLastRayCastTextureName().findByTexture();
            PlayGui;
        }
        if ((0.0 > %obj.getInventoryNuggetSKU())) {
            $gDifSkusCurrentBaseSwatch = "obj" @ " " @ %obj;
            SkuManager;
        }
        $TSControl::objSelContinuous = 1;
    }
    $gDifSkusCurrentBaseSwatch = 0;
    $TSControl::objSelContinuous = 0;
    updateSwatchBrush();
};
function updateSwatchBrush() {
    if (!(isObject())) {
        profile = geSwatchBrushContainer @ new GuiControl(geSwatchBrushContainer) @ "SwatchBrushProfile";
        extent = "72 72";
        profile = new GuiBitmapCtrl(geSwatchBrushBitmap) @ "ETSNonModalProfile";
        extent = "70 70";
        position = "1 1";
        modulationColor = "255 255 255 200";
        profile = new GuiBitmapCtrl(geSwatchBrushObjectShadowBitmap) @ "ETSNonModalProfile";
        position = "1 34";
        extent = "33 33";
        modulationColor = "0 0 0 200";
        profile = new GuiBitmapCtrl(geSwatchBrushObjectBitmap) @ "ETSNonModalProfile";
        position = "0 34";
        extent = "32 32";
        modulationColor = "255 255 255 255";
        profile = new GuiMLTextCtrl(geSwatchBrushText2) @ "GuiMLTextModelessProfile";
        position = "2 57";
        extent = "70 16";
        profile = new GuiMLTextCtrl(geSwatchBrushText1) @ "GuiMLTextModelessProfile";
        position = "1 58";
        extent = "70 16";
        add();
    }
    if (!($gSwatchPaintingModeOn)) {
    }
    if (!(objectIsSwatchable($gDifSkusCurrentDif))) {
    }
    if ((geSwatchBrushContainer SPC $gDifSkusCurrentBaseSwatch $= 0)) {
    }
    if ((PlayGui SPC $gDifSkusCurrentSwatch $= 0)) {
        0.setVisible();
        setCursor();
        return ETSDefaultCursor;
    }
    %pos = 15.0 @ (Canvas + getWord(getCursorPos(), 1));
    ((geSwatchBrushContainer / getWord(getExtent(), 0)) + (Canvas - getWord(getCursorPos(), 0))) @ " ";
    %pos.reposition();
    1.setVisible();
    getBitmapFilename("swatch", $gDifSkusCurrentSwatch.findBySku().getTxtrNames()).setBitmap();
    if ((SkuManager SPC firstWord($gDifSkusCurrentBaseSwatch) $= "obj")) {
        %sku = $gDifSkusCurrentDif.getInventoryNuggetSKU();
        geSwatchBrushBitmap;
        %text = "";
        geSwatchBrushContainer;
        %bitmap = CSBrowser::getThumbnailPathForSku(0, %sku, 32);
        geSwatchBrushContainer;
    }
    %sku = $gDifSkusCurrentBaseSwatch;
    2.0;
    %siBase = %sku.findBySku();
    SkuManager;
    %text = descShrt;
    %siBase;
    %bitmap = "";
    2.0;
    geSwatchBrushText1 @ "<just:left> <color:ddff11>" @ %text.setText();
    geSwatchBrushText2 @ "<just:left> <color:000000>" @ %text.setText();
    %bitmap.setBitmap();
    %bitmap.setBitmap();
    setCursor();
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
    if (isInEditMode()) {
        initWithObject();
        showAtCursor();
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
    if (isObject()) {
    }
    if (isVisible()) {
        $gDifSkusCurrentSwatch.selectSwatch();
    }
    updateSwatchBrush();
    if (getMouseButtonDown()) {
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
