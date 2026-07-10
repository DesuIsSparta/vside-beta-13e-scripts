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
    %this.focusAndRaise();
    WindowManager.update();
    $gSwatchPaintingModeOn = 1;
    PlayGui;
    hilitedCell.selectCell();
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
    "<just:right><a:gamelink RANDOMIZE>[ Randomize! ]</a>    <a:gamelink RESET>[ Defaults ]</a> ".setText();
    if ((geSwatchesPanelMLOnOff @ " " @ $gDifSkusSwatchSkus $= "")) {
        $gDifSkusSwatchSkus = "swatch".getSkusType();
        SkuManager;
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
        %drawerName = %sku.findBySku().drwrName;
        SkuManager;
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
        0;
        %this.expandCollapse = new ""() {
            profile = GuiMLTextCtrl @ "InfoTextSmallProfile";
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
        %this.swatchDrawerNames = %drawerName @ collapseWhiteSpace(%this.swatchDrawerNames) @ %drawerName;
        %this.putListIntoDrawer(%drawerName);
        %i = (1.0 + %i);
    }
    %this.selectFirstCell();
};
function geSwatchesPanel::putListIntoDrawer(%this, %drawerName) {
    if (isObject(%this.swatchDrawerHeaderBoxes)) {
        %this.swatchDrawerHeaderBoxes.delete();
        %this.swatchDrawerHeaderBoxes = %drawerName @ %drawerName @ 0 @ %drawerName;
    }
    if (isObject(%this.swatchDrawerCellArrays)) {
        %this.swatchDrawerCellArrays.delete();
        %this.swatchDrawerCellArrays = %drawerName @ %drawerName @ 0 @ %drawerName;
    }
    0;
    %headerBox = new ""() {
        profile = GuiMLTextCtrl @ "ETSTextListProfile";
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
    0;
    %destDrawer = new ""() {
        profile = GuiArray2Ctrl @ "FocusableDefaultProfile";
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
    %headerBox.add();
    %destDrawer.add();
    %this.swatchDrawerHeaderBoxes = geSwatchesPanelContainer @ %headerBox @ %drawerName;
    geSwatchesPanelContainer;
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
    %si = %skunum.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        error(getScopeName() @ " " @ "can't find sku object for" @ " " @ %skunum);
        return;
    }
    %swatchTexture = getBitmapFilename("swatch", %si.getTxtrNames());
    %cell.horizSizing = "right";
    0;
    %swatch = new ""() {
        position = GuiBitmapCtrl @ "0 0";
        extent = "35 35";
        bitmap = %swatchTexture;
    };
    new ""() {
        position = GuiBitmapButtonCtrl @ "0 0";
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
            %headerBox.reposition(%this.headerBoxLeft, (geSwatchesPanel + %this.headerBoxTop));
        }
        %previousDrawerName = getField(%this.swatchDrawerNames, (1.0 - %i));
        %this.cellSpacing;
        %previousHeaderBox = %this.swatchDrawerHeaderBoxes;
        geSwatchesPanel @ %previousDrawerName;
        %previousCellArray = %this.swatchDrawerCellArrays;
        geSwatchesPanel @ %previousDrawerName;
        if (%this.collapsedDrawers) {
            %previousVisibleControl = %previousHeaderBox;
            %previousDrawerName @ geSwatchesPanel;
            %fudgeFactor = %this.cellSpacing;
        }
        %previousVisibleControl = %previousCellArray;
        %fudgeFactor = 0;
        %currentNewYPos = (geSwatchesPanel + (%this.headerBoxTop + (getWord(%previousVisibleControl.getExtent(), 1) + getWord(%previousVisibleControl.getPosition(), 1))));
        %fudgeFactor;
        %headerBox.reposition(%this.headerBoxLeft, %currentNewYPos);
        if (%this.collapsedDrawers) {
            %allAreExpanded = 0;
            %drawerName @ geSwatchesPanel;
            %cellArray.setVisible(0);
            if ((geSwatchesPanel @ " " @ %drawerName $= %this.hilitedCell.drawerName)) {
                -(1.0).selectCell();
                0.setVisible();
            }
        }
        %allAreCollapsed = 0;
        geSwatchesPanelSelected;
        %currentNewYPos = (%this.hilitedCell.cellArrayTop + (getWord(%headerBox.getExtent(), 1) + getWord(%headerBox.getPosition(), 1)));
        geSwatchesPanel;
        %cellArray.reposition(%this.hilitedCell.cellArrayLeft, %currentNewYPos);
        %cellArray.setVisible(1);
        %cellArray.offsetForOtherRows = geSwatchesPanel @ %currentNewYPos;
        geSwatchesPanel;
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
    %width.resize(%height);
    if ((geSwatchesPanelContainer @ " " @ $gDifSkusSwatchSkusViewable $= "")) {
        -(1.0).selectCell();
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
            %this.hilitedCell.hilitedCell.selectCell();
        }
        geSwatchesPanel.refresh();
    }
    if ((geSwatchesPanel @ " " @ getWord(%url, 1) $= "expandAll")) {
        1.expandOrCollapseAll();
        %this.hilitedCell.hilitedCell.selectCell();
    }
    if ((geSwatchesPanel @ " " @ getWord(%url, 1) $= "collapseAll")) {
        0.expandOrCollapseAll();
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
    %si = %skunum.findBySku();
    SkuManager;
    getBitmapFilename("swatch", %si.getTxtrNames()).setBitmap();
    "<color:ffffff>" @ %si.getDescLong().setText();
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
    0.setVisible();
};
function geSwatchesPanel::selectCell(%this, %cell) {
    if (!(isObject(%cell))) {
        %cell = -(1.0);
        $gSwatchPaintingModeOn = 0;
        "platform/client/ui/nobrush".setBitmap();
        "".setText();
    }
    $gSwatchPaintingModeOn = 1;
    geSwatchesPanelInspectedDesc;
    $TSControl::objSelContinuous = 1;
    geSwatchesPanelInspectedBitmap;
    %this.hiliteCell(%cell);
    updateSwatchBrush();
};
function geSwatchesPanelCell::onHilite(%this) {
    if (isObject(%cell.hilitedCell)) {
        %cell.hilitedCell.onUnhilite();
    }
    %scrollHeight = getWord(geSwatchesPanelScroll.getExtent(), 1);
    geSwatchesPanel;
    %containerYPos = getWord(geSwatchesPanelContainer.getPosition(), 1);
    geSwatchesPanel;
    %myYPosInContainer = (getWord(%this.getParent().getPosition(), 1) + getWord(%this.getPosition(), 1));
    %myHeight = getWord(%this.getExtent(), 1);
    if ((0.0 < (%myYPosInContainer + %containerYPos))) {
        0.scrollTo((%this.getParent().spacing - %myYPosInContainer));
    }
    if (((%containerYPos - %scrollHeight) > (%myHeight + %myYPosInContainer))) {
        0.scrollTo((%scrollHeight - ((%this.getParent().spacing * 1.5) + (%myHeight + %myYPosInContainer))));
    }
    %this.add();
    1.setVisible();
    0.reposition(0);
    $gDifSkusCurrentSwatch = %this.skuNum;
    geSwatchesPanelSelected;
    %this.skuNum.inspectSku();
    %this.hilitedCell = %this @ geSwatchesPanel;
    geSwatchesPanel;
};
function geSwatchesPanelCell::onUnhilite(%this) {
    0.setVisible();
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
