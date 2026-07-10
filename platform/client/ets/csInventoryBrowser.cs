function CSInventoryBrowserWindow::open(%this) {
    %previouslyOpen = %this.isVisible();
    closeCSPanelsInOtherCategories(%this);
    %this.setVisible(1);
    %this.focusAndRaise();
    open();
    update();
    CustomSpaceClient::checkEditingSpace();
    if (!(%previouslyOpen)) {
        focusCurrentFrame();
    }
};
function CSInventoryBrowserWindow::close(%this) {
    %this.setVisible(0);
    resetFirstResponder();
    CustomSpaceClient::checkEditingSpace();
    update();
    return 1;
};
function CSInventoryBrowserWindow::toggle(%this) {
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
function CSInventoryBrowserWindow::Initialize(%this) {
    if (!(initialized)) {
        %ctrl = TreeBrowserControl::newControl("CSBrowser");
        CSInventoryBrowserContainer;
        %ctrl.bindClassName("CSInventoryBrowser");
        %ctrl.setName("CSInventoryBrowser");
        menuProfile = %this @ "ETSClearMenuProfile" @ CSInventoryBrowser;
        selectedProfile = "ETSSelectedMenuItemNoBorderProfile" @ CSInventoryBrowser;
        1.setNumChildren();
        adjustMenuCellHeight = CSInventoryBrowser @ 1 @ CSInventoryBrowser;
        showMoreInfo = 1 @ CSInventoryBrowser;
        baseDir = "My Furnishings" @ CSInventoryBrowser;
        baseDir.addNode();
        loadAvailableSkus();
        $gGotFurnitureCallback = CSInventoryBrowser @ CSInventoryBrowser @ "CSInventoryBrowser.loadAvailableSkus();" @ "CSFurnitureMoverText.update();" @ "CSFurnitureMover.updateButtonStates();";
        CSInventoryBrowser;
        initialized = 1 @ %this;
    }
    Path = "" @ CSInventoryBrowser;
    loadAvailableSkus();
};
function CSInventoryBrowserWindow::onResized(%this) {
    %extent = %this.getExtent();
    (16.0 - getWord(%extent, 0)).resize((18.0 - (4.0 - getWord(%extent, 1))));
    onResized();
};
function CSInventoryBrowserWindow::onReachedTarget(%this) {
    update();
};
function CSInventoryBrowser::loadAvailableSkus(%this) {
    %this.clear();
    %skulist = getFurnitureSkus();
    %numSkus = getWordCount(%skulist);
    %i = 0;
    if ((%numSkus < %i)) {
        %this.addSku(getWord(%skulist, %i));
        %i = (1.0 + %i);
    }
    if ((%this SPC Path $= "")) {
    }
    if ((%this $= baseDir)) {
        %this.goToPath(baseDir, 0);
    }
    %this.update();
};
function CSInventoryBrowser::fillLeafPane(%this, %pane) {
    Parent::fillLeafPane(%this, %pane);
    %desc = getField(Path, (%this - level));
    1.0;
    %desc = %this.getMenuText(%desc);
    %this;
    %paneWidth = getWord(%pane.getExtent(), 0);
    %paneHeight = getWord(%pane.getExtent(), 1);
    if (!(%desc $= "")) {
    }
    if (!(%this $= baseDir)) {
        %ypos = (%pane + getWord(itemText.getPosition(), 1));
        getWord(itemText.getExtent(), 1);
        profile = GuiMLTextCtrl @ new ""() @ "ETSNonModalProfile";
        0;
        horizSizing = %desc @ %pane @ "right";
        vertSizing = "bottom";
        %ypos = (3.0 + %ypos);
        position = 5 @ " ";
        extent = (5.0 - %paneWidth) @ " " @ 18;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "";
        %qtyText = ;
        %ypos = (getWord(%qtyText.getExtent(), 1) + %ypos);
        profile = GuiMLTextCtrl @ new ""() @ "ETSNonModalProfile";
        0;
        horizSizing = "right";
        vertSizing = "bottom";
        position = 5 @ " " @ %ypos;
        extent = (5.0 - %paneWidth) @ " " @ 18;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "";
        %distributionText = ;
        %ypos = (getWord(%distributionText.getExtent(), 1) + %ypos);
        %buttonWidth = 68;
        %xPos = (3.0 - (%buttonWidth - %paneWidth));
        %rightYPos = 18;
        profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton15Profile";
        0;
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ %rightYPos;
        extent = %buttonWidth @ " " @ 15;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "";
        text = "Place";
        groupNum = -1;
        buttonType = "PushButton";
        %placeButton = ;
        %rightYPos = (getWord(%placeButton.getExtent(), 1) + %rightYPos);
        profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton15Profile";
        0;
        horizSizing = "right";
        vertSizing = "bottom";
        %rightYPos = (4.0 + %rightYPos);
        position = %xPos @ " ";
        extent = %buttonWidth @ " " @ 15;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "";
        text = "Put Away";
        groupNum = -1;
        buttonType = "PushButton";
        %putAwayButton = ;
        %rightYPos = (getWord(%putAwayButton.getExtent(), 1) + %rightYPos);
        profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton15Profile";
        0;
        horizSizing = "right";
        vertSizing = "bottom";
        %rightYPos = (4.0 + %rightYPos);
        position = %xPos @ " ";
        extent = %buttonWidth @ " " @ 15;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "";
        text = "Buy More";
        groupNum = -1;
        buttonType = "PushButton";
        %buyButton = ;
        %rightYPos = (getWord(%buyButton.getExtent(), 1) + %rightYPos);
        %si = "";
        %sku = getSubStr(strchr(getField(Path, (%this - level)), "|"), 1);
        1.0;
        %si = %sku.findBySku();
        SkuManager;
        if (!(%si SPC descLong $= "")) {
            profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton15RedProfile";
            0;
            horizSizing = %this @ "right";
            vertSizing = "bottom";
            %rightYPos = (4.0 + %rightYPos);
            position = %xPos @ " ";
            extent = %buttonWidth @ " " @ 15;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            command = "CSInventoryBrowser.showMoreFor(" @ %sku @ ");";
            text = "More Info";
            groupNum = -1;
            buttonType = "PushButton";
            %moreInfoButton = ;
            %rightYPos = (getWord(%buyButton.getExtent(), 1) + %rightYPos);
            if (%this.getFieldValue("showMoreInfo")) {
                text = "Less Info" @ %moreInfoButton;
                command = "CSInventoryBrowser.showMoreInfo = false; CSInventoryBrowser.goToCurrentPath();" @ %moreInfoButton;
            }
            moreInfoButton = %moreInfoButton @ %pane;
            %pane.add(%moreInfoButton);
        }
        %maxThumbnailDim = mMin((3.0 - (%ypos - %paneHeight)), (2.0 - %xPos));
        profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
        0;
        horizSizing = "right";
        vertSizing = "top";
        position = 0 @ " " @ (2.0 + %ypos);
        extent = %maxThumbnailDim @ " " @ %maxThumbnailDim;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = %this.getThumbnailPathForSku(%sku, 128);
        %thumbnail = ;
        %pane.add(%qtyText);
        %pane.add(%distributionText);
        %pane.add(%placeButton);
        %pane.add(%putAwayButton);
        %pane.add(%buyButton);
        %pane.add(%thumbnail);
        qtyText = %qtyText @ %pane;
        distributionText = %distributionText @ %pane;
        placeButton = %placeButton @ %pane;
        putAwayButton = %putAwayButton @ %pane;
        buyButton = %buyButton @ %pane;
        thumbnail = %thumbnail @ %pane;
        browser = %pane @ nextPrevText;
        CSInventoryBrowser;
        if (!(getWord(%pane.getNamespaceList(), 0) $= "CSInventoryItemPane")) {
            %pane.bindClassName("CSInventoryItemPane");
        }
        %pane.update();
    }
    profile = GuiMLTextCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "25 20";
    extent = (5.0 - %paneWidth) @ " " @ 18;
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    lineSpacing = 0;
    allowColorChars = 1;
    maxChars = -1;
    text = "<color:ffffff>You don't own any furnishings.";
    %noItemText = ;
    %pane.add(%noItemText);
};
function CSInventoryItemPane::update(%this) {
    %sku = sku;
    node;
    if ((%this SPC %sku $= "")) {
        return;
    }
    command = %this @ placeButton;
    "CustomSpaceClient::placeSkuInWorld(" @ %sku @ ");";
    command = %this @ putAwayButton;
    "csTestFreeSelectedItem();";
    if (($CSSelectedSku == %sku)) {
    }
    putAwayButton.setActive($CSSelectedIsOwned);
    command = %this @ buyButton;
    %this @ "CSInventoryBrowser.switchToOtherBrowser(); CSShoppingBrowser.navigateToSku(" @ %sku @ ");";
    %numOwned = numOwnedFurnitureSku(%sku);
    %numPlaced = numUsingFurnitureSku(%sku);
    %numStored = (%numPlaced - %numOwned);
    %omni = (-(1.0) == %numOwned);
    if (%omni) {
    }
    %txtOwned = "You own many of these," @ "You own " @ %numOwned @ " of these,";
    if (%omni) {
    }
    %txtPlaced = %numPlaced @ " in room." @ %numPlaced @ " in room, ";
    if (%omni) {
    }
    %txtStored = "" @ %numStored @ " in storage.";
    qtyText.setText(%this @ "<color:ffffff>" @ %txtOwned);
    distributionText.setText(%this @ "<color:ffffff>" @ %txtPlaced @ %txtStored);
    if (%omni) {
        placeButton.setActive(1);
        buyButton.setActive(0);
    }
    placeButton.setActive((%numOwned < %numPlaced));
    buyButton.setActive(1);
};
function CSInventoryBrowser::switchToOtherBrowser(%this) {
    close();
    open();
};
$gCSInventoryBrowserFilterFieldTimerID = "";
function CSInventoryBrowserFilterField::OnTextChanged(%this) {
    cancel($gCSInventoryBrowserFilterFieldTimerID);
    $gCSInventoryBrowserFilterFieldTimerID = %this.schedule(timeoutMS, "onTimer");
    %this;
    %this.getValue().setValue();
    filterText = CSShoppingBrowserFilterField @ %this.getValue() @ CSShoppingBrowser;
};
function CSInventoryBrowserFilterField::OnEnterKey(%this) {
    %this.refilter();
};
function CSInventoryBrowserFilterField::onTimer(%this) {
    %this.refilter();
};
function CSInventoryBrowserFilterField::refilter(%this) {
    cancel($gCSInventoryBrowserFilterFieldTimerID);
    $gCSInventoryBrowserFilterFieldTimerID = "";
    %filterText = %this.getValue();
    if ((%this $= prevFilterText)) {
        return %filterText;
    }
    prevFilterText = %filterText @ %this;
    filterText = %filterText @ CSInventoryBrowser;
    goToCurrentPath();
    %filterText.setValue();
    filterText = CSShoppingBrowserFilterField @ %filterText @ CSShoppingBrowser;
    CSInventoryBrowser;
    goToCurrentPath();
};
