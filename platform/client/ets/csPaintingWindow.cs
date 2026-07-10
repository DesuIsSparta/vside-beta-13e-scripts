function CSPaintingWindow::toggle(%this) {
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
function CSPaintingWindow::open(%this) {
    geSwatchesPanel.init();
    closeCSPanelsInOtherCategories(%this);
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    WindowManager.update();
    $gSwatchPaintingModeOn = 1;
    geSwatchesPanel.selectCell(geSwatchesPanel, hilitedCell);
    CustomSpaceClient::checkEditingSpace();
};
function CSPaintingWindow::close(%this) {
    %this.setVisible(0);
    CustomSpaceClient::checkEditingSpace();
    PlayGui.focusTopWindow();
    WindowManager.update();
    $gSwatchPaintingModeOn = 0;
    $gDifSkusCurrentSwatch = hilitedCell.skuNum;
    geSwatchesPanel;
    updateSwatchBrush();
    return 1;
};
$gGeSwatchesPanelInited = 0;
$gDifSkusSwatchSkus = "";
$gDifSkusNotToDisplay = "35001 35002 35006 35007 35202 35203 35277 35278 35401 35402 35478 35479 35480 35601 35602 35656 35657";
function geSwatchesPanel::init(%this) {
    if ($gGeSwatchesPanelInited) {
        return;
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
    if (($gDifSkusSwatchSkus $= "")) {
        $gDifSkusSwatchSkus = SkuManager.getSkusType("swatch");
        %numberOfNondisplayedSkus = getWordCount($gDifSkusNotToDisplay);
        %n = 0;
        if ((%numberOfNondisplayedSkus < %n)) {
            %elide = getWord($gDifSkusNotToDisplay, %n);
            $gDifSkusSwatchSkus = strreplace($gDifSkusSwatchSkus, %elide, "");
            %elide = ;
            %n = (1.0 + %n);
        }
        $gDifSkusSwatchSkus = collapseWhiteSpace($gDifSkusSwatchSkus);
        (%numberOfNondisplayedSkus < %n);
    }
    %this.populateSwatchPanel();
    %this.refresh();
};
function geSwatchesPanel::populateSwatchPanel(%this) {
    if (($gDifSkusSwatchSkus $= "")) {
        error(getScopeName() @ " " @ "- no swatches to display!");
        return;
    }
    %this.swatchDrawerNames = "";
    %i = (1.0 - getWordCount($gDifSkusSwatchSkus));
    if ((0.0 >= %i)) {
        %sku = getWord($gDifSkusSwatchSkus, %i);
        %drawerName = SkuManager.findBySku(%sku).drwrName;
        %drawerName = trim(collapseWhiteSpace(%drawerName));
        if ((%this.swatchDrawerNames $= "")) {
            %this.swatchDrawerNames = %drawerName;
        }
        if ((0.0 < findField(%this.swatchDrawerNames, %drawerName))) {
            %this.swatchDrawerNames = %drawerName @ "\t" @ %this.swatchDrawerNames;
        }
        if ((0.0 @ %drawerName == getWordCount(%this.swatchDrawers))) {
            %this.swatchDrawers = %sku @ %drawerName;
        }
        %this.swatchDrawers = %drawerName @ %this.swatchDrawers @ " " @ %sku @ %drawerName;
        %i = (1.0 - %i);
    }
    %this.swatchDrawerNames = (0.0 >= %i) @ collapseWhiteSpace(%this.swatchDrawerNames);
    %numberOfDrawers = getFieldCount(%this.swatchDrawerNames);
    if ((0.0 > %numberOfDrawers)) {
        %this.expandCollapse = new GuiMLTextCtrl("") {
            profile = 0 @ "InfoTextSmallProfile";
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
    if ((%numberOfDrawers < %i)) {
        %drawerName = getField(%this.swatchDrawerNames, %i);
        %this.swatchDrawerNames = collapseWhiteSpace(%drawerName, %this.swatchDrawerNames) @ %drawerName;
        %this.putListIntoDrawer(%drawerName);
        %i = (1.0 + %i);
    }
    %this.selectFirstCell();
};
function geSwatchesPanel::putListIntoDrawer(%this, %drawerName) {
    if (isObject(%drawerName, %this.swatchDrawerHeaderBoxes)) {
        %drawerName.delete(%this.swatchDrawerHeaderBoxes);
        %this.swatchDrawerHeaderBoxes = 0 @ %drawerName;
    }
    if (isObject(%drawerName, %this.swatchDrawerCellArrays)) {
        %drawerName.delete(%this.swatchDrawerCellArrays);
        %this.swatchDrawerCellArrays = 0 @ %drawerName;
    }
    %headerBox = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSTextListProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = getWord(geSwatchesPanelContainer.getExtent(), 0) @ " " @ geSwatchesPanel @ headerBoxHeight;
        minExtent = 80 @ " " @ geSwatchesPanel @ headerBoxHeight;
        sluggishness = -1;
        visible = 1;
        canHilite = 0;
    };
    %headerBox.bindClassName("geSwatchesPanelHeaderBox");
    %destDrawer = new GuiArray2Ctrl("") {
        profile = 0 @ "FocusableDefaultProfile";
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
    %this.swatchDrawerHeaderBoxes = %headerBox @ %drawerName;
    %this.swatchDrawerCellArrays = %destDrawer @ %drawerName;
    %skus = %this.swatchDrawers;
    %drawerName;
    %num = getWordCount(%skus);
    %destDrawer.setNumChildren(%num);
    %n = 0;
    if ((%num < %n)) {
        %this.initSwatchCell(%destDrawer.getObject(%n), getWord(%skus, (1.0 - (%n - %num))), %drawerName);
        %n = (1.0 + %n);
    }
    %this.collapsedDrawers = 0 @ %drawerName @ geSwatchesPanel;
    (%num < %n);
};
function geSwatchesPanel::initSwatchCell(%this, %cell, %skunum, %drawerName) {
    %si = SkuManager.findBySku(%skunum);
    if (!(isObject(%si))) {
        error(getScopeName() @ " " @ "can't find sku object for" @ " " @ %skunum);
        return;
    }
    %swatchTexture = getBitmapFilename("swatch", %si.getTxtrNames());
    %cell.horizSizing = "right";
    %swatch = new GuiBitmapCtrl("") {
        position = 0 @ "0 0";
        extent = "35 35";
        bitmap = %swatchTexture;
    };
    new GuiBitmapButtonCtrl("") {
        position = "0 0";
        extent = "35 35";
        bitmap = "platform/client/buttons/generic_35";
        command = "geSwatchesPanel.onClickSwatch(" @ %si @ ", " @ %cell @ ");";
    };
    %cell.add(%swatch);
    %cell.drawerName = %drawerName;
    %cell.skuNum = %skunum;
    %cell.bindClassName("geSwatchesPanelCell");
};
function geSwatchesPanel::refresh(%this) {
    $gDifSkusSwatchSkusViewable = "";
    %allAreExpanded = 1;
    %allAreCollapsed = 1;
    %numberOfDrawers = getFieldCount(%this.swatchDrawerNames);
    %i = 0;
    if ((%numberOfDrawers < %i)) {
        %drawerName = getField(%this.swatchDrawerNames, %i);
        %headerBox = %this.swatchDrawerHeaderBoxes;
        %drawerName;
        %cellArray = %this.swatchDrawerCellArrays;
        %drawerName;
        %collapsed = %this.collapsedDrawers ? "+" : "- ";
        %drawerName @ geSwatchesPanel;
        %titleLine = %this.mlTitleTextPrefix @ "<a:gamelink list " @ %drawerName @ ">" @ %collapsed @ %drawerName @ "</a>";
        %headerBox.setText(%titleLine);
        if ((0.0 == %i)) {
            %headerBox.reposition(geSwatchesPanel, %this.headerBoxLeft, geSwatchesPanel, %this.cellSpacing, (geSwatchesPanel + %this.headerBoxTop));
        }
        %previousDrawerName = getField(%this.swatchDrawerNames, (1.0 - %i));
        %previousHeaderBox = %this.swatchDrawerHeaderBoxes;
        %previousDrawerName;
        %previousCellArray = %this.swatchDrawerCellArrays;
        %previousDrawerName;
        if (%this.collapsedDrawers) {
            %previousVisibleControl = %previousHeaderBox;
            %previousDrawerName @ geSwatchesPanel;
            %fudgeFactor = %this.cellSpacing;
        }
        %previousVisibleControl = %previousCellArray;
        %fudgeFactor = 0;
        %currentNewYPos = (geSwatchesPanel + (%this.headerBoxTop + (getWord(%previousVisibleControl.getExtent(), 1) + getWord(%previousVisibleControl.getPosition(), 1))));
        %fudgeFactor;
        %headerBox.reposition(geSwatchesPanel, %this.headerBoxLeft, %currentNewYPos);
        if (%this.collapsedDrawers) {
            %allAreExpanded = 0;
            %drawerName @ geSwatchesPanel;
            %cellArray.setVisible(0);
            if ((%drawerName $= %this.hilitedCell.drawerName)) {
                geSwatchesPanel.selectCell(-(1.0));
                geSwatchesPanelSelected.setVisible(0);
            }
        }
        %allAreCollapsed = 0;
        %currentNewYPos = (%this.hilitedCell.cellArrayTop + (getWord(%headerBox.getExtent(), 1) + getWord(%headerBox.getPosition(), 1)));
        geSwatchesPanel;
        %cellArray.reposition(geSwatchesPanel, %this.hilitedCell.cellArrayLeft, %currentNewYPos);
        %cellArray.setVisible(1);
        %cellArray.offsetForOtherRows = %currentNewYPos;
        %cellArray.offsetForFirstRow = geSwatchesPanel @ (%cellArray.headerBoxHeight - %cellArray.offsetForOtherRows);
        if (($gDifSkusSwatchSkusViewable $= "")) {
            $gDifSkusSwatchSkusViewable = %this.swatchDrawers;
            %drawerName;
        }
        $gDifSkusSwatchSkusViewable = %this.swatchDrawers @ " " @ $gDifSkusSwatchSkusViewable;
        %drawerName;
        %i = (1.0 + %i);
    }
    %expandAllText = "<color:999999>" @ %allAreExpanded ? "" : "<a:gamelink expandAll>" @ "[ Expand all ]" @ %allAreExpanded ? "" : "</a>";
    (%numberOfDrawers < %i);
    %collapseAllText = "<color:999999>" @ %allAreCollapsed ? "" : "<a:gamelink collapseAll>" @ "[ Collapse all ]" @ %allAreCollapsed ? "" : "</a>";
    %this.expandCollapse.setText(%expandAllText @ "    " @ %collapseAllText);
    if (%cellArray.isVisible()) {
        %height = (getWord(%cellArray.getExtent(), 1) + getWord(%cellArray.getPosition(), 1));
    }
    %height = (getWord(%headerBox.getExtent(), 1) + getWord(%headerBox.getPosition(), 1));
    %width = getWord(geSwatchesPanelContainer.getParent().getExtent(), 0);
    geSwatchesPanelContainer.resize(%width, %height);
    if (($gDifSkusSwatchSkusViewable $= "")) {
        geSwatchesPanel.selectCell(-(1.0));
    }
};
function geSwatchesPanelHeaderBox::onURL(%this, %url) {
    if (!(firstWord(%url) $= "gamelink")) {
        return;
    }
    if ((getWord(%url, 1) $= "list")) {
        %listName = getWords(%url, 2);
        %this.collapsedDrawers = !(%this.collapsedDrawers) @ %listName @ geSwatchesPanel;
        %listName @ geSwatchesPanel;
        if (!(%this.collapsedDrawers)) {
        }
        if ((geSwatchesPanel @ " " @ %this.hilitedCell.drawerName $= %listName)) {
            geSwatchesPanel.selectCell(geSwatchesPanel, %this.hilitedCell.hilitedCell);
        }
        geSwatchesPanel.refresh();
    }
    if ((%listName @ geSwatchesPanel @ " " @ getWord(%url, 1) $= "expandAll")) {
        geSwatchesPanel.expandOrCollapseAll(1);
        geSwatchesPanel.selectCell(geSwatchesPanel, %this.hilitedCell.hilitedCell);
    }
    if ((getWord(%url, 1) $= "collapseAll")) {
        geSwatchesPanel.expandOrCollapseAll(0);
    }
};
function geSwatchesPanel::expandOrCollapseAll(%this, %expand) {
    %i = (1.0 - getFieldCount(%this.swatchDrawerNames));
    if ((0.0 >= %i)) {
        %listName = getField(%this.swatchDrawerNames, %i);
        %this.collapsedDrawers = !(%expand) @ %listName @ geSwatchesPanel;
        %i = (1.0 - %i);
    }
    geSwatchesPanel.refresh();
};
function geSwatchesPanelMLOnOff::onURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %url = restWords(%url);
    }
    if ((firstWord(%url) $= "PAINTINGMODE")) {
        $gSwatchPaintingModeOn = (restWords(%url) $= "on") ? 1 : 0;
        %this.update();
    }
    if ((firstWord(%url) $= "RESET")) {
        difSkusResetConfirm();
    }
    if ((firstWord(%url) $= "RANDOMIZE")) {
        difSkusRandomizeConfirm();
    }
};
function geSwatchesPanel::onClickSwatch(%this, %unused, %cell) {
    %this.selectCell(%cell);
};
function geSwatchesPanel::inspectSku(%this, %skunum) {
    %si = SkuManager.findBySku(%skunum);
    geSwatchesPanelInspectedBitmap.setBitmap(getBitmapFilename("swatch", %si.getTxtrNames()));
    geSwatchesPanelInspectedDesc.setText("<color:ffffff>" @ %si.getDescLong());
};
function geSwatchesPanel::selectSwatch(%this, %skunum) {
    %this.init();
    %cell = -(1.0);
    %numberOfDrawers = getFieldCount(%this.swatchDrawerNames);
    %i = 0;
    if ((%numberOfDrawers < %i)) {
    }
    if ((-(1.0) == %cell)) {
        %drawerName = getField(%this.swatchDrawerNames, %i);
        %cellArray = %this.swatchDrawerCellArrays;
        %drawerName;
        %n = (1.0 - %cellArray.getCount());
        if ((0.0 >= %n)) {
        }
        if ((-(1.0) == %cell)) {
            %cell = %cellArray.getObject(%n);
            if ((%skunum != %cell.skuNum)) {
                %cell = -(1.0);
            }
            %n = (1.0 - %n);
            if ((0.0 >= %n)) {
            }
        }
        %i = (1.0 + %i);
        (-(1.0) == %cell);
        if ((%numberOfDrawers < %i)) {
        }
    }
    if (isObject(%cell)) {
        %this.selectCell(%cell);
    }
    error(getScopeName() @ " " @ "- could not find cell for sku" @ " " @ %skunum);
    geSwatchesPanelSelected.setVisible(0);
};
function geSwatchesPanel::selectCell(%this, %cell) {
    if (!(isObject(%cell))) {
        %cell = -(1.0);
        $gSwatchPaintingModeOn = 0;
        geSwatchesPanelInspectedBitmap.setBitmap("platform/client/ui/nobrush");
        geSwatchesPanelInspectedDesc.setText("");
    }
    $gSwatchPaintingModeOn = 1;
    $TSControl::objSelContinuous = 1;
    %this.hiliteCell(%cell);
    updateSwatchBrush();
};
function geSwatchesPanelCell::onHilite(%this) {
    if (isObject(geSwatchesPanel, %cell.hilitedCell)) {
        geSwatchesPanel.onUnhilite(%cell.hilitedCell);
    }
    %scrollHeight = getWord(geSwatchesPanelScroll.getExtent(), 1);
    %containerYPos = getWord(geSwatchesPanelContainer.getPosition(), 1);
    %myYPosInContainer = (getWord(%this.getParent().getPosition(), 1) + getWord(%this.getPosition(), 1));
    %myHeight = getWord(%this.getExtent(), 1);
    if ((0.0 < (%myYPosInContainer + %containerYPos))) {
        geSwatchesPanelScroll.scrollTo(0, (%this.getParent().spacing - %myYPosInContainer));
    }
    if (((%containerYPos - %scrollHeight) > (%myHeight + %myYPosInContainer))) {
        geSwatchesPanelScroll.scrollTo(0, (%scrollHeight - ((%this.getParent().spacing * 1.5) + (%myHeight + %myYPosInContainer))));
    }
    %this.add();
    geSwatchesPanelSelected.setVisible(1);
    geSwatchesPanelSelected.reposition(0, 0);
    $gDifSkusCurrentSwatch = %this.skuNum;
    geSwatchesPanelSelected;
    geSwatchesPanel.inspectSku(%this.skuNum);
    %this.hilitedCell = %this @ geSwatchesPanel;
};
function geSwatchesPanelCell::onUnhilite(%this) {
    geSwatchesPanelSelected.setVisible(0);
};
function geSwatchesPanelScroll::onMouseUp(%this) {
};
function geSwatchesPanel::selectFirstCell(%this) {
    %numberOfDrawers = getFieldCount(%this.swatchDrawerNames);
    %i = 0;
    if ((%numberOfDrawers < %i)) {
        %drawerName = getField(%this.swatchDrawerNames, %i);
        if (!(%this.collapsedDrawers)) {
            %cellArray = %this.swatchDrawerCellArrays;
            %drawerName @ geSwatchesPanel @ %drawerName;
            %this.hiliteCell(%cellArray.getObject(0));
            return;
        }
        %i = (1.0 + %i);
    }
};
function geSwatchesPanel::hiliteCell(%this, %cell) {
    if (!(isObject(%cell))) {
        error(getTrace() @ " " @ "- cell '" @ %cell @ "' is not an object");
        return;
    }
    %numberOfDrawers = getFieldCount(%this.swatchDrawerNames);
    %i = 0;
    if ((%numberOfDrawers < %i)) {
        %drawerName = getField(%this.swatchDrawerNames, %i);
        %cellArray = %this.swatchDrawerCellArrays;
        %drawerName;
        if (isObject(%cellArray)) {
        }
        if ((0.0 >= %cellArray.getObjectIndex(%cell))) {
            if (isObject(%this.hilitedCell)) {
            }
            if ((%cell != %this.hilitedCell)) {
                %this.hilitedCell.onUnhilite();
            }
            %this.collapsedDrawers = 0 @ %drawerName @ geSwatchesPanel;
            %cell.onHilite();
            %this.refresh();
            return;
        }
        %i = (1.0 + %i);
    }
};
