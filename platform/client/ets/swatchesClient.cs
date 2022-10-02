$gDifSkusCurrentDif = "";
$gDifSkusCurrentBaseSwatch = "";
$gDifSkusCurrentSwatch = "";
$gSwatchPaintingModeOn = 0;
function tryOnMouseOverSwatches(%obj)
{
    %swallowed = 0;
    if (objectIsSwatchable(%obj))
    {
        %swallowed = 1;
        onMouseOverSwatchObj(%obj);
    }
    else
    {
        onMouseOverSwatchObj(0);
    }
    return %swallowed;
}
function onMouseOverSwatchObj(%obj)
{
    $gDifSkusCurrentDif = %obj;
    if (!$gSwatchPaintingModeOn)
    {
        %obj = "";
    }
    if (isObject(%obj))
    {
        if (%obj.getType() & $TypeMasks::InteriorObjectType)
        {
            $gDifSkusCurrentBaseSwatch = SkuManager.findByTexture(PlayGui.getLastRayCastTextureName());
        }
        else
        {
            if (%obj.getInventoryNuggetSKU() > 0)
            {
                $gDifSkusCurrentBaseSwatch = "obj" SPC %obj;
            }
        }
        $TSControl::objSelContinuous = 1;
    }
    else
    {
        $gDifSkusCurrentBaseSwatch = 0;
        $TSControl::objSelContinuous = 0;
    }
    updateSwatchBrush();
    return ;
}
function updateSwatchBrush()
{
    if (!isObject(geSwatchBrushContainer))
    {
        new GuiControl(geSwatchBrushContainer)
        {
            profile = "SwatchBrushProfile";
            extent = "72 72";
        };
        PlayGui.add(geSwatchBrushContainer);
    }
    if (((!$gSwatchPaintingModeOn || !objectIsSwatchable($gDifSkusCurrentDif)) || ($gDifSkusCurrentBaseSwatch $= 0)) || ($gDifSkusCurrentSwatch $= 0))
    {
        geSwatchBrushContainer.setVisible(0);
        Canvas.setCursor(ETSDefaultCursor);
        return ;
    }
    %pos = (getWord(Canvas.getCursorPos(), 0) - (getWord(geSwatchBrushContainer.getExtent(), 0) / 2)) + 2 SPC getWord(Canvas.getCursorPos(), 1) + 15;
    geSwatchBrushContainer.reposition(%pos);
    geSwatchBrushContainer.setVisible(1);
    geSwatchBrushBitmap.setBitmap(getBitmapFilename("swatch", SkuManager.findBySku($gDifSkusCurrentSwatch).getTxtrNames()));
    if (firstWord($gDifSkusCurrentBaseSwatch) $= "obj")
    {
        %sku = $gDifSkusCurrentDif.getInventoryNuggetSKU();
        %text = "";
        %bitmap = CSBrowser::getThumbnailPathForSku(0, %sku, 32);
    }
    else
    {
        %sku = $gDifSkusCurrentBaseSwatch;
        %siBase = SkuManager.findBySku(%sku);
        %text = %siBase.descShrt;
        %bitmap = "";
    }
    geSwatchBrushText1.setText("<just:left> <color:ddff11>" @ %text);
    geSwatchBrushText2.setText("<just:left> <color:000000>" @ %text);
    geSwatchBrushObjectBitmap.setBitmap(%bitmap);
    geSwatchBrushObjectShadowBitmap.setBitmap(%bitmap);
    Canvas.setCursor(ETSHandCursor);
    return ;
}
function objectIsSwatchable(%obj)
{
    if (!isObject(%obj))
    {
        return 0;
    }
    if (%obj.getType() & $TypeMasks::InteriorObjectType)
    {
        return 1;
    }
    %sku = %obj.getInventoryNuggetSKU();
    if (%sku < 1)
    {
        return 0;
    }
    return SkuManager.isSwatchableSku(%sku);
}
function onLeftClickSwatch(%obj)
{
    if (!$gSwatchPaintingModeOn)
    {
        return ;
    }
    if (!objectIsSwatchable(%obj))
    {
        return ;
    }
    if (%obj.getType() & $TypeMasks::InteriorObjectType)
    {
        difSkusFixSkuPair(%obj, $gDifSkusCurrentBaseSwatch, $gDifSkusCurrentSwatch);
    }
    else
    {
        objSkusFixSku(%obj, $gDifSkusCurrentSwatch);
    }
    return ;
}
function onRightClickDownInterior(%obj)
{
    return ;
}
function onRightClickUpInterior(%obj)
{
    if (CSFurnitureMover.isInEditMode())
    {
        FurnitureItemContextMenu.initWithObject();
        FurnitureItemContextMenu.showAtCursor();
    }
    return ;
}
function onMouseWheelDifSkus(%val)
{
    if (!$gSwatchPaintingModeOn)
    {
        return 0;
    }
    if (($gDifSkusCurrentDif $= 0) && ($gDifSkusCurrentBaseSwatch $= 0))
    {
        return 0;
    }
    %numSwatchSkus = getWordCount($gDifSkusSwatchSkusViewable);
    if (%numSwatchSkus < 1)
    {
        geSwatchesPanel.selectCell(-1);
        return ;
    }
    if ($gDifSkusCurrentSwatch != 0)
    {
        %ndx = findWord($gDifSkusSwatchSkusViewable, $gDifSkusCurrentSwatch);
    }
    else
    {
        %ndx = -1;
    }
    %val = %val < 0 ? 1 : 1;
    %ndx = %ndx - %val;
    if (%ndx < 0)
    {
        %ndx = %numSwatchSkus - 1;
    }
    else
    {
        if (%ndx >= %numSwatchSkus)
        {
            %ndx = 0;
        }
    }
    $gDifSkusCurrentSwatch = getWord($gDifSkusSwatchSkusViewable, %ndx);
    if (isObject(geSwatchesPanel) && geSwatchesPanel.isVisible())
    {
        geSwatchesPanel.selectSwatch($gDifSkusCurrentSwatch);
    }
    updateSwatchBrush();
    if (Canvas.getMouseButtonDown())
    {
        onLeftClickSwatch($gDifSkusCurrentDif);
    }
    return 1;
}
function difSkusFixSkuPair(%obj, %base, %rplc)
{
    if (!isObject(%obj))
    {
        return ;
    }
    if (%base <= 0)
    {
        return ;
    }
    if ((CustomSpaceClient::GetSpaceImIn() $= "") && !CustomSpaceClient::isOwner())
    {
        error(getScopeName() SPC "- not owner, what are we doing here?");
        $gSwatchPaintingModeOn = 0;
        updateSwatchBrush();
        return ;
    }
    %oldPairs = %obj.getActiveSkuPairs();
    %newPairs = SkuManager.setSkuPair(%oldPairs, %base, %rplc);
    $gDifSkusCurrentBaseSwatch = %base;
    difSkusPairsToServer(%obj, %newPairs);
    difSkusSetActiveSkuPairs(%obj, %newPairs);
    return ;
}
function difSkusPairsToServer(%obj, %newPairs)
{
    if (!(CustomSpaceClient::GetSpaceImIn() $= ""))
    {
        commandToServer('CSSetSwatches', CustomSpaceClient::GetSpaceImIn(), %newPairs);
        log("general", "debug", getScopeName() SPC "setting swatches through custom space.." SPC %newPairs);
    }
    else
    {
        %ghostID = ServerConnection.getGhostID(%obj);
        commandToServer('fixSwatches', %ghostID, %newPairs);
        log("general", "debug", getScopeName() SPC "setting swatches through interior object itself, not through custom space.." SPC %newPairs);
    }
    return ;
}
function difSkusSetActiveSkuPairs(%obj, %pairs)
{
    %obj.setActiveSkuPairs(%pairs);
    return ;
}
function objSkusFixSku(%obj, %sku)
{
    if (!isObject(%obj))
    {
        return ;
    }
    if ((CustomSpaceClient::GetSpaceImIn() $= "") && !CustomSpaceClient::isOwner())
    {
        error(getScopeName() SPC "- not owner, what are we doing here?");
        $gSwatchPaintingModeOn = 0;
        updateSwatchBrush();
        return ;
    }
    objSkusToServer(%obj, %sku);
    %obj.setActiveSku(%sku);
    return ;
}
function objSkusToServer(%obj, %sku)
{
    if (!(CustomSpaceClient::GetSpaceImIn() $= ""))
    {
        commandToServer('CSSetFurnishingSku', CustomSpaceClient::GetSpaceImIn(), %obj.getGhostID(), %sku);
        log("general", "debug", getScopeName() SPC "setting swatches through custom space.." SPC %sku);
    }
    else
    {
        %ghostID = ServerConnection.getGhostID(%obj);
        commandToServer('fixSwatch', %ghostID, %sku);
        log("general", "debug", getScopeName() SPC "setting swatches through object itself, not through custom space.." SPC %sku);
    }
    return ;
}
function difSkusResetConfirm()
{
    MessageBoxYesNo("Default Materials & Surfaces", $MsgCat::custSpace["SWATCHES_RESET"], "difSkusResetDefaults();", "");
    return ;
}
function difSkusReset()
{
    %obj = $gDifSkusCurrentDif;
    if (!isObject(%obj))
    {
        return ;
    }
    %newPairs = "";
    difSkusPairsToServer(%obj, %newPairs);
    difSkusSetActiveSkuPairs(%obj, %newPairs);
    return ;
}
function difSkusResetDefaults()
{
    %obj = $gDifSkusCurrentDif;
    if (!isObject(%obj))
    {
        return ;
    }
    commandToServer('CSSetDefaultSwatches', CustomSpaceClient::GetSpaceImIn());
    return ;
}
function difSkusRandomizeConfirm()
{
    MessageBoxYesNo("Randomize Materials & Surfaces", $MsgCat::custSpace["SWATCHES_RANDOMIZE"], "difSkusRandomize();", "");
    return ;
}
function difSkusRandomize()
{
    %obj = $gDifSkusCurrentDif;
    if (!isObject(%obj))
    {
        return ;
    }
    %baseSkus = %obj.getBaseSkus();
    if ($gDifSkusSwatchSkus $= "")
    {
        $gDifSkusSwatchSkus = SkuManager.getSkusType("swatch");
    }
    %newPairs = "";
    %delim = "";
    %n = getWordCount(%baseSkus) - 1;
    while (%n >= 0)
    {
        %baseSku = getWord(%baseSkus, %n);
        %randSku = getRandomWord($gDifSkusSwatchSkus);
        %newPairs = %newPairs @ %delim @ %baseSku SPC %randSku;
        %delim = " ";
        %n = %n - 1;
    }
    difSkusPairsToServer(%obj, %newPairs);
    difSkusSetActiveSkuPairs(%obj, %newPairs);
    return ;
}
