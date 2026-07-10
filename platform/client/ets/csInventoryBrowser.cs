function CSInventoryBrowserWindow::open(%this) {
    %previouslyOpen = %this.isVisible();
    closeCSPanelsInOtherCategories(%this);
    %this.setVisible(1);
    %this.focusAndRaise();
    CSFurnitureMover.open();
    WindowManager.update();
    CustomSpaceClient::checkEditingSpace();
    if (!(%previouslyOpen)) {
        CSInventoryBrowser.focusCurrentFrame();
    }
};
function CSInventoryBrowserWindow::close(%this) {
    %this.setVisible(0);
    PlayGui.resetFirstResponder();
    CustomSpaceClient::checkEditingSpace();
    WindowManager.update();
    return 1;
};
function CSInventoryBrowserWindow::toggle(%this) {
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
function CSInventoryBrowserWindow::Initialize(%this) {
    if (!(%this.initialized)) {
        %ctrl = TreeBrowserControl::newControl("CSBrowser");
        CSInventoryBrowserContainer;
        %ctrl.bindClassName("CSInventoryBrowser");
        %ctrl.setName("CSInventoryBrowser");
        %this.menuProfile = "ETSClearMenuProfile" @ CSInventoryBrowser;
        %this.selectedProfile = "ETSSelectedMenuItemNoBorderProfile" @ CSInventoryBrowser;
        1.setNumChildren();
        %this.adjustMenuCellHeight = 1 @ CSInventoryBrowser;
        CSInventoryBrowser;
        %this.showMoreInfo = 1 @ CSInventoryBrowser;
        %this.baseDir = "My Furnishings" @ CSInventoryBrowser;
        %this.baseDir.addNode();
        CSInventoryBrowser.loadAvailableSkus();
        $gGotFurnitureCallback = "CSInventoryBrowser.loadAvailableSkus();" @ "CSFurnitureMoverText.update();" @ "CSFurnitureMover.updateButtonStates();";
        CSInventoryBrowser;
        %this.initialized = CSInventoryBrowser @ 1;
    }
    %this.Path = "" @ CSInventoryBrowser;
    CSInventoryBrowser.loadAvailableSkus();
};
function CSInventoryBrowserWindow::onResized(%this) {
    %extent = %this.getExtent();
    (16.0 - getWord(%extent, 0)).resize((18.0 - (4.0 - getWord(%extent, 1))));
    CSInventoryBrowser.onResized();
};
function CSInventoryBrowserWindow::onReachedTarget(%this) {
    WindowManager.update();
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
    if (((%numSkus < %i) @ " " @ %this.Path $= "")) {
    }
    if ((%this.Path $= %this.baseDir)) {
        %this.goToPath(%this.baseDir, 0);
    }
    %this.update();
};
function CSInventoryBrowser::fillLeafPane(%this, %pane) {
    Parent::fillLeafPane(%this, %pane);
    %desc = getField(%this.Path, (1.0 - %this.level));
    %desc = %this.getMenuText(%desc);
    %paneWidth = getWord(%pane.getExtent(), 0);
    %paneHeight = getWord(%pane.getExtent(), 1);
    if (!(%desc $= "")) {
    }
    if (!(%desc $= %this.baseDir)) {
        %ypos = (getWord(%pane.itemText.getExtent(), 1) + getWord(%pane.itemText.getPosition(), 1));
        0;
        %ypos = (3.0 + %ypos);
        %qtyText = new ""() {
            profile = GuiMLTextCtrl @ "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = 5 @ " ";
            extent = (5.0 - %paneWidth) @ " " @ 18;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            lineSpacing = 0;
            allowColorChars = 1;
            maxChars = -1;
            text = "";
        };
        %ypos = (getWord(%qtyText.getExtent(), 1) + %ypos);
        0;
        %distributionText = new ""() {
            profile = GuiMLTextCtrl @ "ETSNonModalProfile";
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
        };
        %ypos = (getWord(%distributionText.getExtent(), 1) + %ypos);
        %buttonWidth = 68;
        %xPos = (3.0 - (%buttonWidth - %paneWidth));
        %rightYPos = 18;
        0;
        %placeButton = new ""() {
            profile = GuiVariableWidthButtonCtrl @ "BracketButton15Profile";
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
        };
        %rightYPos = (getWord(%placeButton.getExtent(), 1) + %rightYPos);
        0;
        %rightYPos = (4.0 + %rightYPos);
        %putAwayButton = new ""() {
            profile = GuiVariableWidthButtonCtrl @ "BracketButton15Profile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos @ " ";
            extent = %buttonWidth @ " " @ 15;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            command = "";
            text = "Put Away";
            groupNum = -1;
            buttonType = "PushButton";
        };
        %rightYPos = (getWord(%putAwayButton.getExtent(), 1) + %rightYPos);
        0;
        %rightYPos = (4.0 + %rightYPos);
        %buyButton = new ""() {
            profile = GuiVariableWidthButtonCtrl @ "BracketButton15Profile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos @ " ";
            extent = %buttonWidth @ " " @ 15;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            command = "";
            text = "Buy More";
            groupNum = -1;
            buttonType = "PushButton";
        };
        %rightYPos = (getWord(%buyButton.getExtent(), 1) + %rightYPos);
        %si = "";
        %sku = getSubStr(strchr(getField(%this.Path, (1.0 - %this.level)), "|"), 1);
        %si = %sku.findBySku();
        SkuManager;
        if (!(%si.descLong $= "")) {
            0;
            %rightYPos = (4.0 + %rightYPos);
            %moreInfoButton = new ""() {
                profile = GuiVariableWidthButtonCtrl @ "BracketButton15RedProfile";
                horizSizing = "right";
                vertSizing = "bottom";
                position = %xPos @ " ";
                extent = %buttonWidth @ " " @ 15;
                minExtent = "1 1";
                sluggishness = -1;
                visible = 1;
                command = "CSInventoryBrowser.showMoreFor(" @ %sku @ ");";
                text = "More Info";
                groupNum = -1;
                buttonType = "PushButton";
            };
            %rightYPos = (getWord(%buyButton.getExtent(), 1) + %rightYPos);
            if (%this.getFieldValue("showMoreInfo")) {
                %moreInfoButton.text = "Less Info";
                %moreInfoButton.command = "CSInventoryBrowser.showMoreInfo = false; CSInventoryBrowser.goToCurrentPath();";
            }
            %pane.moreInfoButton = %moreInfoButton;
            %pane.add(%moreInfoButton);
        }
        %maxThumbnailDim = mMin((3.0 - (%ypos - %paneHeight)), (2.0 - %xPos));
        0;
        %thumbnail = new ""() {
            profile = GuiBitmapCtrl @ "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "top";
            position = 0 @ " " @ (2.0 + %ypos);
            extent = %maxThumbnailDim @ " " @ %maxThumbnailDim;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = %this.getThumbnailPathForSku(%sku, 128);
        };
        %pane.add(%qtyText);
        %pane.add(%distributionText);
        %pane.add(%placeButton);
        %pane.add(%putAwayButton);
        %pane.add(%buyButton);
        %pane.add(%thumbnail);
        %pane.qtyText = %qtyText;
        %pane.distributionText = %distributionText;
        %pane.placeButton = %placeButton;
        %pane.putAwayButton = %putAwayButton;
        %pane.buyButton = %buyButton;
        %pane.thumbnail = %thumbnail;
        %pane.nextPrevText.browser = CSInventoryBrowser;
        if (!(getWord(%pane.getNamespaceList(), 0) $= "CSInventoryItemPane")) {
            %pane.bindClassName("CSInventoryItemPane");
        }
        %pane.update();
    }
    0;
    %noItemText = new ""() {
        profile = GuiMLTextCtrl @ "ETSNonModalProfile";
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
    };
    %pane.add(%noItemText);
};
function CSInventoryItemPane::update(%this) {
    %sku = %this.node.sku;
    if ((%sku $= "")) {
        return;
    }
    %this.placeButton.command = "CustomSpaceClient::placeSkuInWorld(" @ %sku @ ");";
    %this.putAwayButton.command = "csTestFreeSelectedItem();";
    if (($CSSelectedSku == %sku)) {
    }
    %this.putAwayButton.setActive($CSSelectedIsOwned);
    %this.buyButton.command = "CSInventoryBrowser.switchToOtherBrowser(); CSShoppingBrowser.navigateToSku(" @ %sku @ ");";
    %numOwned = numOwnedFurnitureSku(%sku);
    %numPlaced = numUsingFurnitureSku(%sku);
    %numStored = (%numPlaced - %numOwned);
    %omni = (-(1.0) == %numOwned);
    if (%omni) {
    }
    %txtOwned = "You own " @ %numOwned @ " of these,";
    "You own many of these,";
    if (%omni) {
    }
    %txtPlaced = %numPlaced @ " in room, ";
    %numPlaced @ " in room.";
    if (%omni) {
    }
    %txtStored = %numStored @ " in storage.";
    "";
    %this.qtyText.setText("<color:ffffff>" @ %txtOwned);
    %this.distributionText.setText("<color:ffffff>" @ %txtPlaced @ %txtStored);
    if (%omni) {
        %this.placeButton.setActive(1);
        %this.buyButton.setActive(0);
    }
    %this.placeButton.setActive((%numOwned < %numPlaced));
    %this.buyButton.setActive(1);
};
function CSInventoryBrowser::switchToOtherBrowser(%this) {
    CSInventoryBrowserWindow.close();
    CSShoppingBrowserWindow.open();
};
$gCSInventoryBrowserFilterFieldTimerID = "";
function CSInventoryBrowserFilterField::OnTextChanged(%this) {
    cancel($gCSInventoryBrowserFilterFieldTimerID);
    $gCSInventoryBrowserFilterFieldTimerID = %this.schedule(%this.timeoutMS, "onTimer");
    %this.getValue().setValue();
    %this.filterText = %this.getValue() @ CSShoppingBrowser;
    CSShoppingBrowserFilterField;
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
    if ((%filterText $= %this.prevFilterText)) {
        return;
    }
    %this.prevFilterText = %filterText;
    %this.filterText = %filterText @ CSInventoryBrowser;
    CSInventoryBrowser.goToCurrentPath();
    %filterText.setValue();
    %this.filterText = %filterText @ CSShoppingBrowser;
    CSShoppingBrowserFilterField;
    CSShoppingBrowser.goToCurrentPath();
};
