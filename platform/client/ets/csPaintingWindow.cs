function CSPaintingWindow::toggle(%this)
{
    if (%this.isVisible())
    {
        %this.close();
    }
    else
    {
        %this.open();
    }
    return ;
}
function CSPaintingWindow::open(%this)
{
    geSwatchesPanel.init();
    closeCSPanelsInOtherCategories(%this);
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    WindowManager.update();
    $gSwatchPaintingModeOn = 1;
    geSwatchesPanel.selectCell(geSwatchesPanel.hilitedCell);
    CustomSpaceClient::checkEditingSpace();
    return ;
}
function CSPaintingWindow::close(%this)
{
    %this.setVisible(0);
    CustomSpaceClient::checkEditingSpace();
    PlayGui.focusTopWindow();
    WindowManager.update();
    $gSwatchPaintingModeOn = 0;
    $gDifSkusCurrentSwatch = geSwatchesPanel.hilitedCell.skuNum;
    updateSwatchBrush();
    return 1;
}
$gGeSwatchesPanelInited = 0;
$gDifSkusSwatchSkus = "";
$gDifSkusNotToDisplay = "35001 35002 35006 35007 35202 35203 35277 35278 35401 35402 35478 35479 35480 35601 35602 35656 35657";
function geSwatchesPanel::init(%this)
{
    if ($gGeSwatchesPanelInited)
    {
        return ;
    }
    $gGeSwatchesPanelInited = 1;
    %this.headerBoxTop = 18;
    %this.headerBoxHeight = 17;
    %this.headerBoxLeft = 3;
    %this.cellArrayTop = 0;
    %this.cellArrayLeft = 0;
    %this.cellSpacing = 4;
    %this.mlTitleTextPrefix = "<linkcolor:ffffff>" @ "<linkcolorhl:ffffff>" @ "<color:ffffff>";
    geSwatchesPanelMLOnOff.setText("<just:right><a:gamelink RANDOMIZE>[ Randomize! ]</a>    <a:gamelink RESET>[ Defaults ]</a> ");
    if ($gDifSkusSwatchSkus $= "")
    {
        $gDifSkusSwatchSkus = SkuManager.getSkusType("swatch");
        %numberOfNondisplayedSkus = getWordCount($gDifSkusNotToDisplay);
        %n = 0;
        while (%n < %numberOfNondisplayedSkus)
        {
            %elide = getWord($gDifSkusNotToDisplay, %n);
            %elide = $gDifSkusSwatchSkus = strreplace($gDifSkusSwatchSkus, %elide, "");
            %n = %n + 1;
        }
        $gDifSkusSwatchSkus = collapseWhiteSpace($gDifSkusSwatchSkus);
    }
    %this.populateSwatchPanel();
    %this.refresh();
    return ;
}
function geSwatchesPanel::populateSwatchPanel(%this)
{
    if ($gDifSkusSwatchSkus $= "")
    {
        error(getScopeName() SPC "- no swatches to display!");
        return ;
    }
    %this.swatchDrawerNames = "";
    %i = getWordCount($gDifSkusSwatchSkus) - 1;
    while (%i >= 0)
    {
        %sku = getWord($gDifSkusSwatchSkus, %i);
        %drawerName = SkuManager.findBySku(%sku).drwrName;
        %drawerName = trim(collapseWhiteSpace(%drawerName));
        if (%this.swatchDrawerNames $= "")
        {
            %this.swatchDrawerNames = %drawerName;
        }
        else
        {
            if (findField(%this.swatchDrawerNames, %drawerName) < 0)
            {
                %this.swatchDrawerNames = %drawerName TAB %this.swatchDrawerNames;
            }
        }
        if (getWordCount(%this.swatchDrawers[%drawerName]) == 0)
        {
            %this.swatchDrawers[%drawerName] = %sku;
        }
        else
        {
            %this.swatchDrawers[%drawerName] = %this.swatchDrawers[%drawerName] SPC %sku;
        }
        %i = %i - 1;
    }
    %this.swatchDrawerNames = collapseWhiteSpace(%this.swatchDrawerNames);
    %numberOfDrawers = getFieldCount(%this.swatchDrawerNames);
    if (%numberOfDrawers > 0)
    {
        %this.expandCollapse = new GuiMLTextCtrl()
        {
            profile = "InfoTextSmallProfile";
            horizSizing = "width";
            vertSizing = "top";
            position = "7 328";
            extent = "225 17";
            minExtent = "80 17";
            sluggishness = -1;
            visible = 1;
            canHilite = 0;
        };
        %this.expandCollapse.bindClassName("geSwatchesPanelHeaderBox");
        %this.add(%this.expandCollapse);
    }
    %i = 0;
    while (%i < %numberOfDrawers)
    {
        %drawerName = getField(%this.swatchDrawerNames, %i);
        %this.swatchDrawerNames[%drawerName] = collapseWhiteSpace(%this.swatchDrawerNames[%drawerName]);
        %this.putListIntoDrawer(%drawerName);
        %i = %i + 1;
    }
    %this.selectFirstCell();
    return ;
}
function geSwatchesPanel::putListIntoDrawer(%this, %drawerName)
{
    if (isObject(%this.swatchDrawerHeaderBoxes[%drawerName]))
    {
        %this.swatchDrawerHeaderBoxes[%drawerName].delete();
        %this.swatchDrawerHeaderBoxes[%drawerName] = 0;
    }
    if (isObject(%this.swatchDrawerCellArrays[%drawerName]))
    {
        %this.swatchDrawerCellArrays[%drawerName].delete();
        %this.swatchDrawerCellArrays[%drawerName] = 0;
    }
    %headerBox = new GuiMLTextCtrl()
    {
        profile = "ETSTextListProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = getWord(geSwatchesPanelContainer.getExtent(), 0) SPC geSwatchesPanel.headerBoxHeight;
        minExtent = 80 SPC geSwatchesPanel.headerBoxHeight;
        sluggishness = -1;
        visible = 1;
        canHilite = 0;
    };
    %headerBox.bindClassName("geSwatchesPanelHeaderBox");
    %destDrawer = new GuiArray2Ctrl()
    {
        profile = "FocusableDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        childrenClassName = "GuiControl";
        childrenExtent = "35 35";
        spacing = %this.cellSpacing;
        numRowsOrCols = 6;
        inRows = 0;
        canHilite = 0;
    };
    geSwatchesPanelContainer.add(%headerBox);
    geSwatchesPanelContainer.add(%destDrawer);
    %this.swatchDrawerHeaderBoxes[%drawerName] = %headerBox;
    %this.swatchDrawerCellArrays[%drawerName] = %destDrawer;
    %skus = %this.swatchDrawers[%drawerName];
    %num = getWordCount(%skus);
    %destDrawer.setNumChildren(%num);
    %n = 0;
    while (%n < %num)
    {
        %this.initSwatchCell(%destDrawer.getObject(%n), getWord(%skus, (%num - %n) - 1), %drawerName);
        %n = %n + 1;
    }
    geSwatchesPanel.collapsedDrawers[%drawerName] = 0;
    return ;
}
function geSwatchesPanel::initSwatchCell(%this, %cell, %skunum, %drawerName)
{
    %si = SkuManager.findBySku(%skunum);
    if (!isObject(%si))
    {
        error(getScopeName() SPC "can\'t find sku object for" SPC %skunum);
        return ;
    }
    %swatchTexture = getBitmapFilename("swatch", %si.getTxtrNames());
    %cell.horizSizing = "right";
    %swatch = new GuiBitmapCtrl()
    {
        position = "0 0";
        extent = "35 35";
        bitmap = %swatchTexture;
    };
    %cell.add(%swatch);
    %cell.drawerName = %drawerName;
    %cell.skuNum = %skunum;
    %cell.bindClassName("geSwatchesPanelCell");
    return ;
}
function geSwatchesPanel::refresh(%this)
{
    $gDifSkusSwatchSkusViewable = "";
    %allAreExpanded = 1;
    %allAreCollapsed = 1;
    %numberOfDrawers = getFieldCount(%this.swatchDrawerNames);
    %i = 0;
    while (%i < %numberOfDrawers)
    {
        %drawerName = getField(%this.swatchDrawerNames, %i);
        %headerBox = %this.swatchDrawerHeaderBoxes[%drawerName];
        %cellArray = %this.swatchDrawerCellArrays[%drawerName];
        %collapsed = geSwatchesPanel.collapsedDrawers[%drawerName] ? "+" : "- ";
        %titleLine = %this.mlTitleTextPrefix @ "<a:gamelink list " @ %drawerName @ ">" @ %collapsed @ %drawerName @ "</a>";
        %headerBox.setText(%titleLine);
        if (%i == 0)
        {
            %headerBox.reposition(geSwatchesPanel.headerBoxLeft, geSwatchesPanel.headerBoxTop + geSwatchesPanel.cellSpacing);
        }
        else
        {
            %previousDrawerName = getField(%this.swatchDrawerNames, %i - 1);
            %previousHeaderBox = %this.swatchDrawerHeaderBoxes[%previousDrawerName];
            %previousCellArray = %this.swatchDrawerCellArrays[%previousDrawerName];
            if (geSwatchesPanel.collapsedDrawers[%previousDrawerName])
            {
                %previousVisibleControl = %previousHeaderBox;
                %fudgeFactor = %this.cellSpacing;
            }
            else
            {
                %previousVisibleControl = %previousCellArray;
                %fudgeFactor = 0;
            }
            %currentNewYPos = ((getWord(%previousVisibleControl.getPosition(), 1) + getWord(%previousVisibleControl.getExtent(), 1)) + geSwatchesPanel.headerBoxTop) + %fudgeFactor;
            %headerBox.reposition(geSwatchesPanel.headerBoxLeft, %currentNewYPos);
        }
        if (geSwatchesPanel.collapsedDrawers[%drawerName])
        {
            %allAreExpanded = 0;
            %cellArray.setVisible(0);
            if (%drawerName $= %this.hilitedCell.drawerName)
            {
                geSwatchesPanel.selectCell(-1);
                geSwatchesPanelSelected.setVisible(0);
            }
        }
        else
        {
            %allAreCollapsed = 0;
            %currentNewYPos = (getWord(%headerBox.getPosition(), 1) + getWord(%headerBox.getExtent(), 1)) + geSwatchesPanel.cellArrayTop;
            %cellArray.reposition(geSwatchesPanel.cellArrayLeft, %currentNewYPos);
            %cellArray.setVisible(1);
            %cellArray.offsetForOtherRows = %currentNewYPos;
            %cellArray.offsetForFirstRow = %cellArray.offsetForOtherRows - geSwatchesPanel.headerBoxHeight;
            if ($gDifSkusSwatchSkusViewable $= "")
            {
                $gDifSkusSwatchSkusViewable = %this.swatchDrawers[%drawerName];
            }
            else
            {
                $gDifSkusSwatchSkusViewable = %this.swatchDrawers[%drawerName] SPC $gDifSkusSwatchSkusViewable;
            }
        }
        %i = %i + 1;
    }
    %expandAllText = "<color:999999>" @ %allAreExpanded ? "" : "<a:gamelink expandAll>" @ "[ Expand all ]" @ %allAreExpanded ? "" : "</a>";
    %collapseAllText = "<color:999999>" @ %allAreCollapsed ? "" : "<a:gamelink collapseAll>" @ "[ Collapse all ]" @ %allAreCollapsed ? "" : "</a>";
    %this.expandCollapse.setText(%expandAllText @ "    " @ %collapseAllText);
    if (%cellArray.isVisible())
    {
        %height = getWord(%cellArray.getPosition(), 1) + getWord(%cellArray.getExtent(), 1);
    }
    else
    {
        %height = getWord(%headerBox.getPosition(), 1) + getWord(%headerBox.getExtent(), 1);
    }
    %width = getWord(geSwatchesPanelContainer.getParent().getExtent(), 0);
    geSwatchesPanelContainer.resize(%width, %height);
    if ($gDifSkusSwatchSkusViewable $= "")
    {
        geSwatchesPanel.selectCell(-1);
    }
    return ;
}
function geSwatchesPanelHeaderBox::onURL(%this, %url)
{
    if (!(firstWord(%url) $= "gamelink"))
    {
        return ;
    }
    if (getWord(%url, 1) $= "list")
    {
        %listName = getWords(%url, 2);
        geSwatchesPanel.collapsedDrawers[%listName] = !geSwatchesPanel.collapsedDrawers[%listName];
        if (!(geSwatchesPanel.collapsedDrawers[%listName]) && (geSwatchesPanel.hilitedCell.drawerName $= %listName))
        {
            geSwatchesPanel.selectCell(geSwatchesPanel.hilitedCell);
        }
        else
        {
            geSwatchesPanel.refresh();
        }
    }
    else
    {
        if (getWord(%url, 1) $= "expandAll")
        {
            geSwatchesPanel.expandOrCollapseAll(1);
            geSwatchesPanel.selectCell(geSwatchesPanel.hilitedCell);
        }
        else
        {
            if (getWord(%url, 1) $= "collapseAll")
            {
                geSwatchesPanel.expandOrCollapseAll(0);
            }
        }
    }
    return ;
}
function geSwatchesPanel::expandOrCollapseAll(%this, %expand)
{
    %i = getFieldCount(%this.swatchDrawerNames) - 1;
    while (%i >= 0)
    {
        %listName = getField(%this.swatchDrawerNames, %i);
        geSwatchesPanel.collapsedDrawers[%listName] = !%expand;
        %i = %i - 1;
    }
    geSwatchesPanel.refresh();
    return ;
}
function geSwatchesPanelMLOnOff::onURL(%this, %url)
{
    if (firstWord(%url) $= "gamelink")
    {
        %url = restWords(%url);
    }
    if (firstWord(%url) $= "PAINTINGMODE")
    {
        $gSwatchPaintingModeOn = restWords(%url) $= "on" ? 1 : 0;
        %this.update();
    }
    else
    {
        if (firstWord(%url) $= "RESET")
        {
            difSkusResetConfirm();
        }
        else
        {
            if (firstWord(%url) $= "RANDOMIZE")
            {
                difSkusRandomizeConfirm();
            }
        }
    }
    return ;
}
function geSwatchesPanel::onClickSwatch(%this, %unused, %cell)
{
    %this.selectCell(%cell);
    return ;
}
function geSwatchesPanel::inspectSku(%this, %skunum)
{
    %si = SkuManager.findBySku(%skunum);
    geSwatchesPanelInspectedBitmap.setBitmap(getBitmapFilename("swatch", %si.getTxtrNames()));
    geSwatchesPanelInspectedDesc.setText("<color:ffffff>" @ %si.getDescLong());
    return ;
}
function geSwatchesPanel::selectSwatch(%this, %skunum)
{
    %this.init();
    %cell = -1;
    %numberOfDrawers = getFieldCount(%this.swatchDrawerNames);
    %i = 0;
    while (%cell == -1)
    {
        %drawerName = getField(%this.swatchDrawerNames, %i);
        %cellArray = %this.swatchDrawerCellArrays[%drawerName];
        %n = %cellArray.getCount() - 1;
        while (%cell == -1)
        {
            %cell = %cellArray.getObject(%n);
            if (%cell.skuNum != %skunum)
            {
                %cell = -1;
            }
            %n = %n - 1;
        }
        %i = %i + 1;
    }
    if (isObject(%cell))
    {
        %this.selectCell(%cell);
    }
    else
    {
        error(getScopeName() SPC "- could not find cell for sku" SPC %skunum);
        geSwatchesPanelSelected.setVisible(0);
    }
    return %i;
}
function geSwatchesPanel::selectCell(%this, %cell)
{
    if (!isObject(%cell))
    {
        %cell = -1;
        $gSwatchPaintingModeOn = 0;
        geSwatchesPanelInspectedBitmap.setBitmap("platform/client/ui/nobrush");
        geSwatchesPanelInspectedDesc.setText("");
    }
    else
    {
        $gSwatchPaintingModeOn = 1;
        $TSControl::objSelContinuous = 1;
        %this.hiliteCell(%cell);
    }
    updateSwatchBrush();
    return ;
}
function geSwatchesPanelCell::onHilite(%this)
{
    if (isObject(geSwatchesPanel.hilitedCell))
    {
        geSwatchesPanel.hilitedCell.onUnhilite();
    }
    %scrollHeight = getWord(geSwatchesPanelScroll.getExtent(), 1);
    %containerYPos = getWord(geSwatchesPanelContainer.getPosition(), 1);
    %myYPosInContainer = getWord(%this.getPosition(), 1) + getWord(%this.getParent().getPosition(), 1);
    %myHeight = getWord(%this.getExtent(), 1);
    if ((%containerYPos + %myYPosInContainer) < 0)
    {
        geSwatchesPanelScroll.scrollTo(0, %myYPosInContainer - %this.getParent().spacing);
    }
    else
    {
        if ((%myYPosInContainer + %myHeight) > (%scrollHeight - %containerYPos))
        {
            geSwatchesPanelScroll.scrollTo(0, ((%myYPosInContainer + %myHeight) + (1.5 * %this.getParent().spacing)) - %scrollHeight);
        }
    }
    %this.add(geSwatchesPanelSelected);
    geSwatchesPanelSelected.setVisible(1);
    geSwatchesPanelSelected.reposition(0, 0);
    $gDifSkusCurrentSwatch = %this.skuNum;
    geSwatchesPanel.inspectSku(%this.skuNum);
    geSwatchesPanel.hilitedCell = %this;
    return ;
}
function geSwatchesPanelCell::onUnhilite(%this)
{
    geSwatchesPanelSelected.setVisible(0);
    return ;
}
function geSwatchesPanelScroll::onMouseUp(%this)
{
    return ;
}
function geSwatchesPanel::selectFirstCell(%this)
{
    %numberOfDrawers = getFieldCount(%this.swatchDrawerNames);
    %i = 0;
    while (%i < %numberOfDrawers)
    {
        %drawerName = getField(%this.swatchDrawerNames, %i);
        if (!geSwatchesPanel.collapsedDrawers[%drawerName])
        {
            %cellArray = %this.swatchDrawerCellArrays[%drawerName];
            %this.hiliteCell(%cellArray.getObject(0));
            return ;
        }
        %i = %i + 1;
    }
}

function geSwatchesPanel::hiliteCell(%this, %cell)
{
    if (!isObject(%cell))
    {
        error(getTrace() SPC "- cell \'" @ %cell @ "\' is not an object");
        return ;
    }
    %numberOfDrawers = getFieldCount(%this.swatchDrawerNames);
    %i = 0;
    while (%i < %numberOfDrawers)
    {
        %drawerName = getField(%this.swatchDrawerNames, %i);
        %cellArray = %this.swatchDrawerCellArrays[%drawerName];
        if (isObject(%cellArray) && (%cellArray.getObjectIndex(%cell) >= 0))
        {
            if (isObject(%this.hilitedCell) && (%this.hilitedCell != %cell))
            {
                %this.hilitedCell.onUnhilite();
            }
            geSwatchesPanel.collapsedDrawers[%drawerName] = 0;
            %cell.onHilite();
            %this.refresh();
            return ;
        }
        %i = %i + 1;
    }
}


