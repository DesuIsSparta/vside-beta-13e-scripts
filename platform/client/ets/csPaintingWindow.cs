function CSPaintingWindow::toggle(%this) {
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
function CSPaintingWindow::open(%this) {
    geSwatchesPanel.init();
    closeCSPanelsInOtherCategories(%this);
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    WindowManager.update();
    $gSwatchPaintingModeOn = 1;
    hilitedCell.selectCell(geSwatchesPanel, geSwatchesPanel);
    CustomSpaceClient::checkEditingSpace();
};
function CSPaintingWindow::close(%this) {
    0.setVisible(%this);
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
    "<just:right><a:gamelink RANDOMIZE>[ Randomize! ]</a>    <a:gamelink RESET>[ Defaults ]</a> ".setText(geSwatchesPanelMLOnOff);
    if (($gDifSkusSwatchSkus $= "")) {
        $gDifSkusSwatchSkus = "swatch".getSkusType(SkuManager);
        %numberOfNondisplayedSkus = getWordCount($gDifSkusNotToDisplay);
        %n = 0;
        while ((%n < %numberOfNondisplayedSkus)) {
            %elide = getWord($gDifSkusNotToDisplay, %n);
            $gDifSkusSwatchSkus = strreplace($gDifSkusSwatchSkus, %elide, "");
            %elide = ;
            %n = (%n + 1.0);
        }
        $gDifSkusSwatchSkus = collapseWhiteSpace($gDifSkusSwatchSkus);
        (%n < %numberOfNondisplayedSkus);
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
    %i = (getWordCount($gDifSkusSwatchSkus) - 1.0);
    while ((%i >= 0.0)) {
        %sku = getWord($gDifSkusSwatchSkus, %i);
        %drawerName = %sku.findBySku(SkuManager).drwrName;
        %drawerName = trim(collapseWhiteSpace(%drawerName));
        if ((%this.swatchDrawerNames $= "")) {
            %this.swatchDrawerNames = %drawerName;
        }
        if ((findField(%this.swatchDrawerNames, %drawerName) < 0.0)) {
            %this.swatchDrawerNames = %drawerName @ "\t" @ %this.swatchDrawerNames;
        }
        if ((getWordCount(%this.swatchDrawers) == 0.0 @ %drawerName)) {
            %this.swatchDrawers = %sku @ %drawerName;
        }
        %this.swatchDrawers = %drawerName @ %this.swatchDrawers @ " " @ %sku @ %drawerName;
        %i = (%i - 1.0);
    }
    %this.swatchDrawerNames = (%i >= 0.0) @ collapseWhiteSpace(%this.swatchDrawerNames);
    %numberOfDrawers = getFieldCount(%this.swatchDrawerNames);
    if ((%numberOfDrawers > 0.0)) {
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
        "geSwatchesPanelHeaderBox".bindClassName(%this.expandCollapse);
        %this.expandCollapse.add(%this);
    }
    %i = 0;
    while ((%i < %numberOfDrawers)) {
        %drawerName = getField(%this.swatchDrawerNames, %i);
        %this.swatchDrawerNames = collapseWhiteSpace(%drawerName, %this.swatchDrawerNames) @ %drawerName;
        %drawerName.putListIntoDrawer(%this);
        %i = (%i + 1.0);
    }
    %this.selectFirstCell();
};
function geSwatchesPanel::putListIntoDrawer(%this, %drawerName) {
    if (isObject(%drawerName, %this.swatchDrawerHeaderBoxes)) {
        %this.swatchDrawerHeaderBoxes.delete(%drawerName);
        %this.swatchDrawerHeaderBoxes = 0 @ %drawerName;
    }
    if (isObject(%drawerName, %this.swatchDrawerCellArrays)) {
        %this.swatchDrawerCellArrays.delete(%drawerName);
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
    "geSwatchesPanelHeaderBox".bindClassName(%headerBox);
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
    %headerBox.add(geSwatchesPanelContainer);
    %destDrawer.add(geSwatchesPanelContainer);
    %this.swatchDrawerHeaderBoxes = %headerBox @ %drawerName;
    %this.swatchDrawerCellArrays = %destDrawer @ %drawerName;
    %skus = %this.swatchDrawers;
    %drawerName;
    %num = getWordCount(%skus);
    %num.setNumChildren(%destDrawer);
    %n = 0;
    while ((%n < %num)) {
        %drawerName.initSwatchCell(%this, %n.getObject(%destDrawer), getWord(%skus, ((%num - %n) - 1.0)));
        %n = (%n + 1.0);
    }
    %this.collapsedDrawers = 0 @ %drawerName @ geSwatchesPanel;
    (%n < %num);
};
function geSwatchesPanel::initSwatchCell(%this, %cell, %skunum, %drawerName) {
    %si = %skunum.findBySku(SkuManager);
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
    %swatch.add(%cell);
    %cell.drawerName = %drawerName;
    %cell.skuNum = %skunum;
    "geSwatchesPanelCell".bindClassName(%cell);
};
function geSwatchesPanel::refresh(%this) {
    $gDifSkusSwatchSkusViewable = "";
    %allAreExpanded = 1;
    %allAreCollapsed = 1;
    %numberOfDrawers = getFieldCount(%this.swatchDrawerNames);
    %i = 0;
    while ((%i < %numberOfDrawers)) {
        %drawerName = getField(%this.swatchDrawerNames, %i);
        %headerBox = %this.swatchDrawerHeaderBoxes;
        %drawerName;
        %cellArray = %this.swatchDrawerCellArrays;
        %drawerName;
        %collapsed = %this.collapsedDrawers ? "+" : "- ";
        %drawerName @ geSwatchesPanel;
        %titleLine = %this.mlTitleTextPrefix @ "<a:gamelink list " @ %drawerName @ ">" @ %collapsed @ %drawerName @ "</a>";
        %titleLine.setText(%headerBox);
        if ((%i == 0.0)) {
            (%this.headerBoxTop + geSwatchesPanel).reposition(%headerBox, geSwatchesPanel, %this.headerBoxLeft, geSwatchesPanel, %this.cellSpacing);
        }
        %previousDrawerName = getField(%this.swatchDrawerNames, (%i - 1.0));
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
        %currentNewYPos = (((getWord(%previousVisibleControl.getPosition(), 1) + getWord(%previousVisibleControl.getExtent(), 1)) + %this.headerBoxTop) + geSwatchesPanel);
        %fudgeFactor;
        %currentNewYPos.reposition(%headerBox, geSwatchesPanel, %this.headerBoxLeft);
        if (%this.collapsedDrawers) {
            %allAreExpanded = 0;
            %drawerName @ geSwatchesPanel;
            0.setVisible(%cellArray);
            if ((%drawerName $= %this.hilitedCell.drawerName)) {
                -(1.0).selectCell(geSwatchesPanel);
                0.setVisible(geSwatchesPanelSelected);
            }
        }
        %allAreCollapsed = 0;
        %currentNewYPos = ((getWord(%headerBox.getPosition(), 1) + getWord(%headerBox.getExtent(), 1)) + %this.hilitedCell.cellArrayTop);
        geSwatchesPanel;
        %currentNewYPos.reposition(%cellArray, geSwatchesPanel, %this.hilitedCell.cellArrayLeft);
        1.setVisible(%cellArray);
        %cellArray.offsetForOtherRows = %currentNewYPos;
        %cellArray.offsetForFirstRow = geSwatchesPanel @ (%cellArray.offsetForOtherRows - %cellArray.headerBoxHeight);
        if (($gDifSkusSwatchSkusViewable $= "")) {
            $gDifSkusSwatchSkusViewable = %this.swatchDrawers;
            %drawerName;
        }
        $gDifSkusSwatchSkusViewable = %this.swatchDrawers @ " " @ $gDifSkusSwatchSkusViewable;
        %drawerName;
        %i = (%i + 1.0);
    }
    %expandAllText = "<color:999999>" @ %allAreExpanded ? "" : "<a:gamelink expandAll>" @ "[ Expand all ]" @ %allAreExpanded ? "" : "</a>";
    (%i < %numberOfDrawers);
    %collapseAllText = "<color:999999>" @ %allAreCollapsed ? "" : "<a:gamelink collapseAll>" @ "[ Collapse all ]" @ %allAreCollapsed ? "" : "</a>";
    %expandAllText @ "    " @ %collapseAllText.setText(%this.expandCollapse);
    if (%cellArray.isVisible()) {
        %height = (getWord(%cellArray.getPosition(), 1) + getWord(%cellArray.getExtent(), 1));
    }
    %height = (getWord(%headerBox.getPosition(), 1) + getWord(%headerBox.getExtent(), 1));
    %width = getWord(geSwatchesPanelContainer.getParent().getExtent(), 0);
    %height.resize(geSwatchesPanelContainer, %width);
    if (($gDifSkusSwatchSkusViewable $= "")) {
        -(1.0).selectCell(geSwatchesPanel);
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
            %this.hilitedCell.hilitedCell.selectCell(geSwatchesPanel, geSwatchesPanel);
        }
        geSwatchesPanel.refresh();
    }
    if ((%listName @ geSwatchesPanel @ " " @ getWord(%url, 1) $= "expandAll")) {
        1.expandOrCollapseAll(geSwatchesPanel);
        %this.hilitedCell.hilitedCell.selectCell(geSwatchesPanel, geSwatchesPanel);
    }
    if ((getWord(%url, 1) $= "collapseAll")) {
        0.expandOrCollapseAll(geSwatchesPanel);
    }
};
function geSwatchesPanel::expandOrCollapseAll(%this, %expand) {
    %i = (getFieldCount(%this.swatchDrawerNames) - 1.0);
    while ((%i >= 0.0)) {
        %listName = getField(%this.swatchDrawerNames, %i);
        %this.collapsedDrawers = !(%expand) @ %listName @ geSwatchesPanel;
        %i = (%i - 1.0);
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
    %cell.selectCell(%this);
};
function geSwatchesPanel::inspectSku(%this, %skunum) {
    %si = %skunum.findBySku(SkuManager);
    getBitmapFilename("swatch", %si.getTxtrNames()).setBitmap(geSwatchesPanelInspectedBitmap);
    "<color:ffffff>" @ %si.getDescLong().setText(geSwatchesPanelInspectedDesc);
};
function geSwatchesPanel::selectSwatch(%this, %skunum) {
    %this.init();
    %cell = -(1.0);
    %numberOfDrawers = getFieldCount(%this.swatchDrawerNames);
    %i = 0;
    if ((%i < %numberOfDrawers)) {
    }
    while ((%cell == -(1.0))) {
        %drawerName = getField(%this.swatchDrawerNames, %i);
        %cellArray = %this.swatchDrawerCellArrays;
        %drawerName;
        %n = (%cellArray.getCount() - 1.0);
        if ((%n >= 0.0)) {
        }
        while ((%cell == -(1.0))) {
            %cell = %n.getObject(%cellArray);
            if ((%cell.skuNum != %skunum)) {
                %cell = -(1.0);
            }
            %n = (%n - 1.0);
            if ((%n >= 0.0)) {
            }
        }
        %i = (%i + 1.0);
        (%cell == -(1.0));
        if ((%i < %numberOfDrawers)) {
        }
    }
    if (isObject(%cell)) {
        %cell.selectCell(%this);
    }
    error(getScopeName() @ " " @ "- could not find cell for sku" @ " " @ %skunum);
    0.setVisible(geSwatchesPanelSelected);
};
function geSwatchesPanel::selectCell(%this, %cell) {
    if (!(isObject(%cell))) {
        %cell = -(1.0);
        $gSwatchPaintingModeOn = 0;
        "platform/client/ui/nobrush".setBitmap(geSwatchesPanelInspectedBitmap);
        "".setText(geSwatchesPanelInspectedDesc);
    }
    $gSwatchPaintingModeOn = 1;
    $TSControl::objSelContinuous = 1;
    %cell.hiliteCell(%this);
    updateSwatchBrush();
};
function geSwatchesPanelCell::onHilite(%this) {
    if (isObject(geSwatchesPanel, %cell.hilitedCell)) {
        %cell.hilitedCell.onUnhilite(geSwatchesPanel);
    }
    %scrollHeight = getWord(geSwatchesPanelScroll.getExtent(), 1);
    %containerYPos = getWord(geSwatchesPanelContainer.getPosition(), 1);
    %myYPosInContainer = (getWord(%this.getPosition(), 1) + getWord(%this.getParent().getPosition(), 1));
    %myHeight = getWord(%this.getExtent(), 1);
    if (((%containerYPos + %myYPosInContainer) < 0.0)) {
        (%myYPosInContainer - %this.getParent().spacing).scrollTo(geSwatchesPanelScroll, 0);
    }
    if (((%myYPosInContainer + %myHeight) > (%scrollHeight - %containerYPos))) {
        (((%myYPosInContainer + %myHeight) + (1.5 * %this.getParent().spacing)) - %scrollHeight).scrollTo(geSwatchesPanelScroll, 0);
    }
    %this.add();
    1.setVisible(geSwatchesPanelSelected);
    0.reposition(geSwatchesPanelSelected, 0);
    $gDifSkusCurrentSwatch = %this.skuNum;
    geSwatchesPanelSelected;
    %this.skuNum.inspectSku(geSwatchesPanel);
    %this.hilitedCell = %this @ geSwatchesPanel;
};
function geSwatchesPanelCell::onUnhilite(%this) {
    0.setVisible(geSwatchesPanelSelected);
};
function geSwatchesPanelScroll::onMouseUp(%this) {
};
function geSwatchesPanel::selectFirstCell(%this) {
    %numberOfDrawers = getFieldCount(%this.swatchDrawerNames);
    %i = 0;
    while ((%i < %numberOfDrawers)) {
        %drawerName = getField(%this.swatchDrawerNames, %i);
        if (!(%this.collapsedDrawers)) {
            %cellArray = %this.swatchDrawerCellArrays;
            %drawerName @ geSwatchesPanel @ %drawerName;
            0.getObject(%cellArray).hiliteCell(%this);
            return;
        }
        %i = (%i + 1.0);
    }
};
function geSwatchesPanel::hiliteCell(%this, %cell) {
    if (!(isObject(%cell))) {
        error(getTrace() @ " " @ "- cell '" @ %cell @ "' is not an object");
        return;
    }
    %numberOfDrawers = getFieldCount(%this.swatchDrawerNames);
    %i = 0;
    while ((%i < %numberOfDrawers)) {
        %drawerName = getField(%this.swatchDrawerNames, %i);
        %cellArray = %this.swatchDrawerCellArrays;
        %drawerName;
        if (isObject(%cellArray)) {
        }
        if ((%cell.getObjectIndex(%cellArray) >= 0.0)) {
            if (isObject(%this.hilitedCell)) {
            }
            if ((%this.hilitedCell != %cell)) {
                %this.hilitedCell.onUnhilite();
            }
            %this.collapsedDrawers = 0 @ %drawerName @ geSwatchesPanel;
            %cell.onHilite();
            %this.refresh();
            return;
        }
        %i = (%i + 1.0);
    }
};
