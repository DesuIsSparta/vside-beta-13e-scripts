function CSPaintingWindow::toggle(%this) {
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
function CSPaintingWindow::open(%this) {
    init();
    closeCSPanelsInOtherCategories(%this);
    %this.setVisible(1);
    %this.focusAndRaise();
    update();
    $gSwatchPaintingModeOn = 1;
    WindowManager;
    hilitedCell.selectCell();
    CustomSpaceClient::checkEditingSpace();
};
function CSPaintingWindow::close(%this) {
    %this.setVisible(0);
    CustomSpaceClient::checkEditingSpace();
    focusTopWindow();
    update();
    $gSwatchPaintingModeOn = 0;
    WindowManager;
    $gDifSkusCurrentSwatch = skuNum;
    hilitedCell;
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
    headerBoxTop = 18 @ %this;
    headerBoxHeight = 17 @ %this;
    headerBoxLeft = 3 @ %this;
    cellArrayTop = 0 @ %this;
    cellArrayLeft = 0 @ %this;
    cellSpacing = 4 @ %this;
    mlTitleTextPrefix = "<linkcolor:ffffff>" @ "<linkcolorhl:ffffff>" @ "<color:ffffff>" @ %this;
    "<just:right><a:gamelink RANDOMIZE>[ Randomize! ]</a>    <a:gamelink RESET>[ Defaults ]</a> ".setText();
    if ((geSwatchesPanelMLOnOff SPC $gDifSkusSwatchSkus $= "")) {
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
    swatchDrawerNames = "" @ %this;
    %i = (1.0 - getWordCount($gDifSkusSwatchSkus));
    if ((0.0 >= %i)) {
        %sku = getWord($gDifSkusSwatchSkus, %i);
        %drawerName = drwrName;
        %sku.findBySku();
        %drawerName = trim(collapseWhiteSpace(%drawerName));
        SkuManager;
        if ((%this SPC swatchDrawerNames $= "")) {
            swatchDrawerNames = %drawerName @ %this;
        }
        if ((%this < findField(swatchDrawerNames, %drawerName))) {
            swatchDrawerNames = %drawerName @ "\t" @ %this @ swatchDrawerNames @ %this;
            0.0;
        }
        if ((0.0 @ %drawerName @ %this == getWordCount(swatchDrawers))) {
            swatchDrawers = %sku @ %drawerName @ %this;
        }
        swatchDrawers = %drawerName @ %this @ swatchDrawers @ " " @ %sku @ %drawerName @ %this;
        %i = (1.0 - %i);
    }
    swatchDrawerNames = %this @ collapseWhiteSpace(swatchDrawerNames) @ %this;
    (0.0 >= %i);
    %numberOfDrawers = getFieldCount(swatchDrawerNames);
    %this;
    if ((0.0 > %numberOfDrawers)) {
        profile = GuiMLTextCtrl @ new ""() @ "InfoTextSmallProfile";
        0;
        horizSizing = "width";
        vertSizing = "top";
        position = "7 328";
        extent = "225 17";
        minExtent = "80 17";
        sluggishness = -1;
        visible = 1;
        canHilite = 0;
        expandCollapse = %this;
        expandCollapse.bindClassName("geSwatchesPanelHeaderBox");
        %this.add(expandCollapse);
    }
    %i = 0;
    %this;
    if ((%numberOfDrawers < %i)) {
        %drawerName = getField(swatchDrawerNames, %i);
        %this;
        swatchDrawerNames = %this @ %drawerName @ %this @ collapseWhiteSpace(swatchDrawerNames) @ %drawerName @ %this;
        %this.putListIntoDrawer(%drawerName);
        %i = (1.0 + %i);
    }
    %this.selectFirstCell();
};
function geSwatchesPanel::putListIntoDrawer(%this, %drawerName) {
    if (isObject(swatchDrawerHeaderBoxes)) {
        swatchDrawerHeaderBoxes.delete();
        swatchDrawerHeaderBoxes = %drawerName @ %this @ %drawerName @ %this @ 0 @ %drawerName @ %this;
    }
    if (isObject(swatchDrawerCellArrays)) {
        swatchDrawerCellArrays.delete();
        swatchDrawerCellArrays = %drawerName @ %this @ %drawerName @ %this @ 0 @ %drawerName @ %this;
    }
    profile = GuiMLTextCtrl @ new ""() @ "ETSTextListProfile";
    0;
    horizSizing = "width";
    vertSizing = "bottom";
    position = "0 0";
    extent = geSwatchesPanelContainer @ getWord(getExtent(), 0) @ " " @ geSwatchesPanel @ headerBoxHeight;
    minExtent = 80 @ " " @ geSwatchesPanel @ headerBoxHeight;
    sluggishness = -1;
    visible = 1;
    canHilite = 0;
    %headerBox = ;
    %headerBox.bindClassName("geSwatchesPanelHeaderBox");
    profile = GuiArray2Ctrl @ new ""() @ "FocusableDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    childrenClassName = "GuiControl";
    childrenExtent = "35 35";
    spacing = %this @ cellSpacing;
    numRowsOrCols = 6;
    inRows = 0;
    canHilite = 0;
    %destDrawer = ;
    %headerBox.add();
    %destDrawer.add();
    swatchDrawerHeaderBoxes = geSwatchesPanelContainer @ geSwatchesPanelContainer @ %headerBox @ %drawerName @ %this;
    swatchDrawerCellArrays = %destDrawer @ %drawerName @ %this;
    %skus = swatchDrawers;
    %drawerName @ %this;
    %num = getWordCount(%skus);
    %destDrawer.setNumChildren(%num);
    %n = 0;
    if ((%num < %n)) {
        %this.initSwatchCell(%destDrawer.getObject(%n), getWord(%skus, (1.0 - (%n - %num))), %drawerName);
        %n = (1.0 + %n);
    }
    collapsedDrawers = (%num < %n) @ 0 @ %drawerName @ geSwatchesPanel;
};
function geSwatchesPanel::initSwatchCell(%this, %cell, %skunum, %drawerName) {
    %si = %skunum.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        error(getScopeName() @ " " @ "can't find sku object for" @ " " @ %skunum);
        return;
    }
    %swatchTexture = getBitmapFilename("swatch", %si.getTxtrNames());
    horizSizing = "right" @ %cell;
    position = GuiBitmapCtrl @ new ""() @ "0 0";
    0;
    extent = "35 35";
    bitmap = %swatchTexture;
    position = GuiBitmapButtonCtrl @ new ""() @ "0 0";
    extent = "35 35";
    bitmap = "platform/client/buttons/generic_35";
    command = "geSwatchesPanel.onClickSwatch(" @ %si @ ", " @ %cell @ ");";
    %swatch = ;
    %cell.add(%swatch);
    drawerName = %drawerName @ %cell;
    skuNum = %skunum @ %cell;
    %cell.bindClassName("geSwatchesPanelCell");
};
function geSwatchesPanel::refresh(%this) {
    $gDifSkusSwatchSkusViewable = "";
    %allAreExpanded = 1;
    %allAreCollapsed = 1;
    %numberOfDrawers = getFieldCount(swatchDrawerNames);
    %this;
    %i = 0;
    if ((%numberOfDrawers < %i)) {
        %drawerName = getField(swatchDrawerNames, %i);
        %this;
        %headerBox = swatchDrawerHeaderBoxes;
        %drawerName @ %this;
        %cellArray = swatchDrawerCellArrays;
        %drawerName @ %this;
        %collapsed = collapsedDrawers ? "+" : "- ";
        %drawerName @ geSwatchesPanel;
        %titleLine = %this @ mlTitleTextPrefix @ "<a:gamelink list " @ %drawerName @ ">" @ %collapsed @ %drawerName @ "</a>";
        %headerBox.setText(%titleLine);
        if ((0.0 == %i)) {
            %headerBox.reposition(headerBoxLeft, (geSwatchesPanel + headerBoxTop));
        }
        %previousDrawerName = getField(swatchDrawerNames, (1.0 - %i));
        %this;
        %previousHeaderBox = swatchDrawerHeaderBoxes;
        cellSpacing @ %previousDrawerName @ %this;
        %previousCellArray = swatchDrawerCellArrays;
        geSwatchesPanel @ %previousDrawerName @ %this;
        if (collapsedDrawers) {
            %previousVisibleControl = %previousHeaderBox;
            geSwatchesPanel @ %previousDrawerName @ geSwatchesPanel;
            %fudgeFactor = cellSpacing;
            %this;
        }
        %previousVisibleControl = %previousCellArray;
        %fudgeFactor = 0;
        %currentNewYPos = (geSwatchesPanel + (headerBoxTop + (getWord(%previousVisibleControl.getExtent(), 1) + getWord(%previousVisibleControl.getPosition(), 1))));
        %fudgeFactor;
        %headerBox.reposition(headerBoxLeft, %currentNewYPos);
        if (collapsedDrawers) {
            %allAreExpanded = 0;
            geSwatchesPanel @ %drawerName @ geSwatchesPanel;
            %cellArray.setVisible(0);
            if ((hilitedCell $= drawerName)) {
                -(1.0).selectCell();
                0.setVisible();
            }
        }
        %allAreCollapsed = 0;
        geSwatchesPanelSelected;
        %currentNewYPos = (cellArrayTop + (getWord(%headerBox.getExtent(), 1) + getWord(%headerBox.getPosition(), 1)));
        geSwatchesPanel;
        %cellArray.reposition(cellArrayLeft, %currentNewYPos);
        %cellArray.setVisible(1);
        offsetForOtherRows = geSwatchesPanel @ %currentNewYPos @ %cellArray;
        geSwatchesPanel;
        offsetForFirstRow = headerBoxHeight @ (%cellArray - offsetForOtherRows) @ %cellArray;
        geSwatchesPanel;
        if ((%this SPC $gDifSkusSwatchSkusViewable $= "")) {
            $gDifSkusSwatchSkusViewable = swatchDrawers;
            %drawerName @ %drawerName @ %this;
        }
        $gDifSkusSwatchSkusViewable = swatchDrawers @ " " @ $gDifSkusSwatchSkusViewable;
        %drawerName @ %this;
        %i = (1.0 + %i);
    }
    %expandAllText = (%numberOfDrawers < %i) @ "<color:999999>" @ %allAreExpanded ? "" : "<a:gamelink expandAll>" @ "[ Expand all ]" @ %allAreExpanded ? "" : "</a>";
    %collapseAllText = "<color:999999>" @ %allAreCollapsed ? "" : "<a:gamelink collapseAll>" @ "[ Collapse all ]" @ %allAreCollapsed ? "" : "</a>";
    expandCollapse.setText(%this @ %expandAllText @ "    " @ %collapseAllText);
    if (%cellArray.isVisible()) {
        %height = (getWord(%cellArray.getExtent(), 1) + getWord(%cellArray.getPosition(), 1));
    }
    %height = (getWord(%headerBox.getExtent(), 1) + getWord(%headerBox.getPosition(), 1));
    %width = getWord(getParent().getExtent(), 0);
    geSwatchesPanelContainer;
    %width.resize(%height);
    if ((geSwatchesPanelContainer SPC $gDifSkusSwatchSkusViewable $= "")) {
        -(1.0).selectCell();
    }
};
function geSwatchesPanelHeaderBox::onURL(%this, %url) {
    if (!(firstWord(%url) $= "gamelink")) {
        return;
    }
    if ((getWord(%url, 1) $= "list")) {
        %listName = getWords(%url, 2);
        collapsedDrawers = %listName @ geSwatchesPanel @ !(collapsedDrawers) @ %listName @ geSwatchesPanel;
        if (!(collapsedDrawers)) {
        }
        if ((hilitedCell SPC drawerName $= %listName)) {
            hilitedCell.selectCell();
        }
        refresh();
    }
    if ((geSwatchesPanel SPC getWord(%url, 1) $= "expandAll")) {
        1.expandOrCollapseAll();
        hilitedCell.selectCell();
    }
    if ((geSwatchesPanel SPC getWord(%url, 1) $= "collapseAll")) {
        0.expandOrCollapseAll();
    }
};
function geSwatchesPanel::expandOrCollapseAll(%this, %expand) {
    %i = (%this - getFieldCount(swatchDrawerNames));
    1.0;
    if ((0.0 >= %i)) {
        %listName = getField(swatchDrawerNames, %i);
        %this;
        collapsedDrawers = !(%expand) @ %listName @ geSwatchesPanel;
        %i = (1.0 - %i);
    }
    refresh();
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
    geSwatchesPanelInspectedDesc @ "<color:ffffff>" @ %si.getDescLong().setText();
};
function geSwatchesPanel::selectSwatch(%this, %skunum) {
    %this.init();
    %cell = -(1.0);
    %numberOfDrawers = getFieldCount(swatchDrawerNames);
    %this;
    %i = 0;
    if ((%numberOfDrawers < %i)) {
    }
    if ((-(1.0) == %cell)) {
        %drawerName = getField(swatchDrawerNames, %i);
        %this;
        %cellArray = swatchDrawerCellArrays;
        %drawerName @ %this;
        %n = (1.0 - %cellArray.getCount());
        if ((0.0 >= %n)) {
        }
        if ((-(1.0) == %cell)) {
            %cell = %cellArray.getObject(%n);
            if ((%cell != skuNum)) {
                %cell = -(1.0);
                %skunum;
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
    if (isObject(hilitedCell)) {
        hilitedCell.onUnhilite();
    }
    %scrollHeight = getWord(getExtent(), 1);
    geSwatchesPanelScroll;
    %containerYPos = getWord(getPosition(), 1);
    geSwatchesPanelContainer;
    %myYPosInContainer = (getWord(%this.getParent().getPosition(), 1) + getWord(%this.getPosition(), 1));
    geSwatchesPanel;
    %myHeight = getWord(%this.getExtent(), 1);
    geSwatchesPanel;
    if ((0.0 < (%myYPosInContainer + %containerYPos))) {
        0.scrollTo((spacing - %myYPosInContainer));
    }
    if (((%containerYPos - %scrollHeight) > (%myHeight + %myYPosInContainer))) {
        0.scrollTo((%this.getParent() - ((spacing * 1.5) + (%myHeight + %myYPosInContainer))));
    }
    %this.add();
    1.setVisible();
    0.reposition(0);
    $gDifSkusCurrentSwatch = skuNum;
    %this;
    skuNum.inspectSku();
    hilitedCell = %this @ %this @ geSwatchesPanel;
    geSwatchesPanel;
};
function geSwatchesPanelCell::onUnhilite(%this) {
    0.setVisible();
};
function geSwatchesPanelScroll::onMouseUp(%this) {
};
function geSwatchesPanel::selectFirstCell(%this) {
    %numberOfDrawers = getFieldCount(swatchDrawerNames);
    %this;
    %i = 0;
    if ((%numberOfDrawers < %i)) {
        %drawerName = getField(swatchDrawerNames, %i);
        %this;
        if (!(collapsedDrawers)) {
            %cellArray = swatchDrawerCellArrays;
            %drawerName @ geSwatchesPanel @ %drawerName @ %this;
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
    %numberOfDrawers = getFieldCount(swatchDrawerNames);
    %this;
    %i = 0;
    if ((%numberOfDrawers < %i)) {
        %drawerName = getField(swatchDrawerNames, %i);
        %this;
        %cellArray = swatchDrawerCellArrays;
        %drawerName @ %this;
        if (isObject(%cellArray)) {
        }
        if ((0.0 >= %cellArray.getObjectIndex(%cell))) {
            if (isObject(hilitedCell)) {
            }
            if ((%this != hilitedCell)) {
                hilitedCell.onUnhilite();
            }
            collapsedDrawers = %cell @ %this @ 0 @ %drawerName @ geSwatchesPanel;
            %this;
            %cell.onHilite();
            %this.refresh();
            return;
        }
        %i = (1.0 + %i);
    }
};
