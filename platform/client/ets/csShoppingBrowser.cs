function CSShoppingBrowserWindow::open(%this) {
    %previouslyOpen = %this.isVisible();
    closeCSPanelsInOtherCategories(%this);
    %this.setVisible(1);
    %this.focusAndRaise();
    if (!(%previouslyOpen)) {
        refreshInventory();
    }
    open();
    update();
    CustomSpaceClient::checkEditingSpace();
    if (!(%previouslyOpen)) {
        focusCurrentFrame();
    }
};
function CSShoppingBrowserWindow::close(%this) {
    if ((-(1.0) != $CSSelectedSku)) {
    }
    if (!($CSSelectedIsOwned)) {
        csTestFreeSelectedItem();
    }
    %this.setVisible(0);
    resetFirstResponder();
    CustomSpaceClient::checkEditingSpace();
    update();
    return 1;
};
function CSShoppingBrowserWindow::toggle(%this) {
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
function CSShoppingBrowserWindow::Initialize(%this) {
    if (!(initialized)) {
        %ctrl = TreeBrowserControl::newControl("CSBrowser");
        CSShoppingBrowserContainer;
        %ctrl.bindClassName("CSShoppingBrowser");
        %ctrl.setName("CSShoppingBrowser");
        menuProfile = %this @ "ETSClearMenuProfile" @ CSShoppingBrowser;
        selectedProfile = "ETSSelectedMenuItemNoBorderProfile" @ CSShoppingBrowser;
        1.setNumChildren();
        adjustMenuCellHeight = CSShoppingBrowser @ 1 @ CSShoppingBrowser;
        showMoreInfo = 1 @ CSInventoryBrowser;
        if (isObject("button0".getFieldValue())) {
            button.delete();
        }
        %container = getParent();
        CSShoppingBrowser;
        profile = GuiBitmapButtonCtrl @ new ""() @ "ETSShopVerticalButtonProfile";
        0;
        horizSizing = CSShoppingBrowser @ 0 @ CSShoppingBrowser @ "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = CSShoppingBrowser @ buttonWidth @ " " @ getWord(%container.getExtent(), 1);
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "";
        text = "Shop";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "platform/client/buttons/vbutton";
        drawText = 1;
        textRotation = 90;
        button = 0 @ CSShoppingBrowser;
        %container.add(button);
        profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
        0;
        horizSizing = 0 @ CSShoppingBrowser @ "right";
        vertSizing = "top";
        position = "3 20";
        extent = "14 14";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/vbux_14";
        vBuxIcon = CSShoppingBrowser;
        %container.add(vBuxIcon);
        baseDir = CSShoppingBrowser @ "Shop" @ CSShoppingBrowser;
        baseDir.addNode();
        storeInfo = CSShoppingBrowser @ 0 @ CSShoppingBrowser;
        CSShoppingBrowser;
        refreshInventory();
        loadAvailableSkus();
        initialized = CSShoppingBrowser @ 1 @ %this;
        CSShoppingBrowser;
    }
};
function CSShoppingBrowser::loadAvailableSkus(%this) {
    %this.clear();
    %skulist = "";
    %numSkus = 0;
    if (isObject(storeInfo)) {
        %skulist = storeInfo.getSkus();
        %this;
        %numSkus = getWordCount(%skulist);
        %this;
        statusText = (0.0 == %numSkus) ? "No furnishings available to buy." : "" @ %this;
    }
    statusText = "Getting Store Info..." @ %this;
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
function CSShoppingBrowser::fillLeafPane(%this, %pane) {
    Parent::fillLeafPane(%this, %pane);
    %desc = getField(Path, (%this - level));
    1.0;
    %desc = %this.getMenuText(%desc);
    %this;
    %paneWidth = getWord(%pane.getExtent(), 0);
    %paneHeight = getWord(%pane.getExtent(), 1);
    buyQuantity = 1 @ %pane;
    if (!(%desc $= "")) {
    }
    if (!(%this $= baseDir)) {
        %ypos = getWord(itemText.getExtent(), 1);
        %pane;
        %rightYPos = 18;
        %desc;
        %rmVPadding = 4;
        %buttonWidth = 62;
        %rightXPos = (%buttonWidth - %paneWidth);
        %sku = getSubStr(strchr(getField(Path, (%this - level)), "|"), 1);
        1.0;
        %si = %sku.findBySku();
        SkuManager;
        if (!(%si SPC descLong $= "")) {
            profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton15Profile";
            0;
            horizSizing = %this @ "right";
            vertSizing = "bottom";
            %rightYPos = (%rmVPadding + %rightYPos);
            position = (4.0 - %rightXPos) @ " ";
            extent = %buttonWidth @ " " @ 15;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            command = "CSShoppingBrowser.showMoreFor(" @ %sku @ ");";
            text = "More..";
            groupNum = -1;
            buttonType = "PushButton";
            %moreButton = ;
            %rightYPos = (getWord(%moreButton.getExtent(), 1) + %rightYPos);
            if (%this.getFieldValue("showMoreInfo")) {
                text = "Less.." @ %moreButton;
                command = "CSShoppingBrowser.showMoreInfo = false; CSShoppingBrowser.goToCurrentPath();" @ %moreButton;
            }
            %pane.add(%moreButton);
            moreButton = %moreButton @ %pane;
        }
        profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton15Profile";
        0;
        horizSizing = "right";
        vertSizing = "bottom";
        position = (4.0 - %rightXPos) @ " " @ %rightYPos;
        extent = %buttonWidth @ " " @ 15;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "";
        text = "Try It!";
        groupNum = -1;
        buttonType = "PushButton";
        %testDriveButton = ;
        %rightYPos = (getWord(%testDriveButton.getExtent(), 1) + %rightYPos);
        profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton15Profile";
        0;
        horizSizing = "right";
        vertSizing = "bottom";
        %rightYPos = (%rmVPadding + %rightYPos);
        position = (4.0 - %rightXPos) @ " ";
        extent = %buttonWidth @ " " @ 15;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "";
        text = "Buy It!";
        groupNum = -1;
        buttonType = "PushButton";
        %buyButton = ;
        %rightYPos = (getWord(%buyButton.getExtent(), 1) + %rightYPos);
        %smallButtonWidth = 15;
        profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton15Profile";
        0;
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((5.0 + (2.0 * %smallButtonWidth)) - (%buttonWidth + (4.0 - %rightXPos))) @ " " @ (%rmVPadding + %rightYPos);
        extent = %smallButtonWidth @ " " @ 15;
        command = %pane @ ".buyFewer();";
        text = "-";
        %buyFewerButton = ;
        profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton15Profile";
        0;
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%smallButtonWidth - (%buttonWidth + (4.0 - %rightXPos))) @ " " @ (%rmVPadding + %rightYPos);
        extent = %smallButtonWidth @ " " @ 15;
        command = %pane @ ".buyMore();";
        text = "+";
        %buyMoreButton = ;
        %rightYPos = (getWord(%buyMoreButton.getExtent(), 1) + %rightYPos);
        %rightYPos = (%rmVPadding + %rightYPos);
        profile = GuiMLTextCtrl @ new ""() @ "ETSNonModalProfile";
        0;
        horizSizing = "right";
        vertSizing = "bottom";
        position = (4.0 - %rightXPos) @ " " @ %rightYPos;
        extent = %buttonWidth @ " " @ 18;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "";
        %priceTextVPoints = ;
        %pane.add(%priceTextVPoints);
        priceTextVPoints = %priceTextVPoints @ %pane;
        %rightYPos = (getWord(%priceTextVPoints.getExtent(), 1) + %rightYPos);
        profile = GuiMLTextCtrl @ new ""() @ "ETSNonModalProfile";
        0;
        horizSizing = "right";
        vertSizing = "bottom";
        position = (4.0 - %rightXPos) @ " " @ %rightYPos;
        extent = %buttonWidth @ " " @ 18;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "";
        %priceTextVBux = ;
        %pane.add(%priceTextVBux);
        priceTextVBux = %priceTextVBux @ %pane;
        %rightYPos = (getWord(%priceTextVBux.getExtent(), 1) + %rightYPos);
        %ypos = (2.0 + %ypos);
        %nextPrevTextY = getWord(nextPrevText.getPosition(), 1);
        %pane;
        %maxThumbnailHeight = (%ypos - %nextPrevTextY);
        %maxThumbnailHeight = mMin(%maxThumbnailHeight, (3.0 - %rightXPos));
        %ypos = (%ypos + (2.0 / ((%maxThumbnailHeight + %ypos) - %nextPrevTextY)));
        %ypos = mCeil(%ypos);
        %xPos = 0;
        profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
        0;
        horizSizing = "right";
        vertSizing = "top";
        position = %xPos @ " " @ %ypos;
        extent = %maxThumbnailHeight @ " " @ %maxThumbnailHeight;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = %this.getThumbnailPathForSku(%sku, 128);
        %thumbnail = ;
        %pane.add(%thumbnail);
        %pane.add(%testDriveButton);
        %pane.add(%buyButton);
        %pane.add(%buyFewerButton);
        %pane.add(%buyMoreButton);
        thumbnail = %thumbnail @ %pane;
        testDriveButton = %testDriveButton @ %pane;
        buyButton = %buyButton @ %pane;
        buyFewerButton = %buyFewerButton @ %pane;
        buyMoreButton = %buyMoreButton @ %pane;
        browser = %pane @ nextPrevText;
        CSShoppingBrowser;
        if (!(getWord(%pane.getNamespaceList(), 0) $= "CSShoppingItemPane")) {
            %pane.bindClassName("CSShoppingItemPane");
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
    text = "<color:ffffff>" @ %this @ statusText;
    %noItemText = ;
    %pane.add(%noItemText);
};
function CSShoppingItemPane::buyMore(%this) {
    buyQuantity = (%this + buyQuantity);
    1.0;
    %this.update();
};
function CSShoppingItemPane::buyFewer(%this) {
    buyQuantity = (%this - buyQuantity);
    1.0;
    if ((%this < buyQuantity)) {
    }
    buyQuantity = %this @ buyQuantity @ %this;
    1;
    %this.update();
};
function CSShoppingBrowser::testDriveSku(%this, %sku) {
    $CSInstaTestDrive = 0;
    if ((0.0 > %sku)) {
        $CSSelectedSku = %sku;
        commandToServer('CreateInventoryBySkuJustTestingItOut', CustomSpaceClient::GetSpaceImIn(), %sku);
    }
    error(getScopeName() @ " " @ "No sku selected");
};
function CSShoppingBrowser::purchaseSkus(%this, %skus) {
    if (!(isObject(storeInfo))) {
        return %this;
    }
    %cbPoints = "CSShoppingBrowser.purchaseSkusVPoints(\"" @ %skus @ "\");";
    %cbBux = "CSShoppingBrowser.purchaseSkusVBux(\"" @ %skus @ "\");";
    %cbCancel = "";
    ShowPurchaseSkusConfirmationDialog(%skus, %cbPoints, %cbBux, %cbCancel);
};
function CSShoppingBrowser::purchaseSkusVPoints(%this, %skus) {
    if (isObject(storeInfo)) {
        %totalPrice = Inventory::getTotalPrice("vPoints", %skus);
        %this;
        %itemCount = getWordCount(%skus);
        if ((1.0 == %itemCount)) {
        }
        %itemsStr = "these" @ " " @ %itemCount @ " " @ "items";
        "this item";
        if (($Player::VPoints <= %totalPrice)) {
            %vpointsString = (1.0 == %totalPrice) ? "vPoint" : "vPoints";
            %msg = "Do you wish to purchase " @ %itemsStr @ " for " @ %totalPrice @ " " @ %vpointsString @ "?";
            %cmd = "CSShoppingBrowser.storeInfo.purchase(\"" @ %skus @ "\", \"vPoints\", \"CSShoppingBrowser::onGotPurchaseResult\");";
            MessageBoxOkCancel("Confirm Purchase", %msg, %cmd, "");
        }
        MessageBoxOK("Not Enough vPoints", "You do not have enough vPoints to purchase " @ %itemsStr @ ".  Click <a:" @ $Net::HelpURL_VPoints @ ">here</a> for more information about earning vPoints.", "");
    }
};
function CSShoppingBrowser::purchaseSkusVBux(%this, %skus) {
    if (isObject(storeInfo)) {
        %totalPrice = Inventory::getTotalPrice("vBux", %skus);
        %this;
        %itemCount = getWordCount(%skus);
        if ((1.0 == %itemCount)) {
        }
        %itemsStr = "these" @ " " @ %itemCount @ " " @ "items";
        "this item";
        if (($Player::VBux <= %totalPrice)) {
            %msg = "Do you wish to purchase " @ %itemsStr @ " for " @ %totalPrice @ " vBux?";
            %cmd = "CSShoppingBrowser.storeInfo.purchase(\"" @ %skus @ "\", \"vBux\", \"CSShoppingBrowser::onGotPurchaseResult\");";
            MessageBoxOkCancel("Confirm Purchase", %msg, %cmd, "");
        }
        MessageBoxOK("Not Enough vBux", "You do not have enough vBux to purchase " @ %itemsStr @ ".  Click <a:" @ $Net::AddFundsURL @ ">here</a> to refill your account.", "");
    }
};
function CSShoppingBrowser::onGotPurchaseResult(%status, %results) {
    if ((%status $= "success")) {
        %numResults = getWordCount(%results);
        %i = 0;
        if ((%numResults < %i)) {
            %result = getWord(%results, %i);
            %result = strreplace(%result, "|", " ");
            %sku = getWord(%result, 0);
            %resultCode = getWord(%result, 1);
            if ((%resultCode $= "pass")) {
                if ((%sku == $CSSelectedSku)) {
                }
                if (!($CSSelectedIsOwned)) {
                    csTestFreeSelectedItem();
                }
                addFurnitureSku(%sku, 1);
                loadAvailableSkus();
                if ((0.0 == %i)) {
                    CustomSpaceClient::placeSkuInWorld(%sku);
                }
            }
            %i = (1.0 + %i);
            CSInventoryBrowser;
        }
    }
    MessageBoxOK("Purchase Unsuccessful", "There was a problem completing your purchase.", "");
};
function CSShoppingBrowser::goToPath(%this, %path, %focus) {
    if (!(isDefined("%focus"))) {
        %focus = 1;
    }
    %oldPath = Path;
    %this;
    Parent::goToPath(%this, %path, %focus);
    %curMenu = %this.getCurrentMenu();
    %count = %curMenu.getCount();
    %i = 0;
    if ((%count < %i)) {
        %menuItem = %curMenu.getObject(%i);
        %sku = strchr(name, "|");
        %menuItem;
        if (!(%sku $= "")) {
            if (!(getWord(%menuItem.getNamespaceList(), 0) $= "CSShoppingBrowserSKUItem")) {
                %menuItem.bindClassName("CSShoppingBrowserSKUItem");
            }
        }
        %i = (1.0 + %i);
    }
    vBuxIcon.reposition(3, (20.0 - getWord(%this.getExtent(), 1)));
    %pathSku = getSubStr(strchr(%path, "|"), 1);
    CSShoppingBrowser;
    if ((-(1.0) != $CSSelectedSku)) {
    }
    if (!($CSSelectedIsOwned)) {
    }
    if (!((%count < %i) SPC %path $= %oldPath)) {
    }
    if (!(%pathSku $= $CSSelectedSku)) {
        csTestFreeSelectedItem();
    }
};
$CSShoppingBrowser::InstaTestDriveEnabled = 0;
$CSInstaTestDrive = 0;
$CSShoppingBrowserSKUItem::deleteEvent = 0;
function CSShoppingBrowserSKUItem::onMouseEnterBounds(%this) {
    Parent::onMouseEnterBounds(%this);
    if (!($CSShoppingBrowser::InstaTestDriveEnabled)) {
        return;
    }
    if ((%this SPC name $= "")) {
        warn("CSShoppingBrowserSKUItem .name has gone missing?");
        return;
    }
    if ((-(1.0) == $CSSelectedSku)) {
    }
    if (!($CSSelectedIsOwned)) {
        if (isEventPending($CSShoppingBrowserSKUItem::deleteEvent)) {
            cancel($CSShoppingBrowserSKUItem::deleteEvent);
        }
        %sku = getSubStr(strchr(name, "|"), 1);
        %this;
        $CSInstaTestDrive = 1;
        $CSSelectedSku = %sku;
        commandToServer('CreateInventoryBySkuJustTestingItOut', CustomSpaceClient::GetSpaceImIn(), %sku);
    }
};
function CSShoppingBrowserSKUItem::onMouseLeaveBounds(%this) {
    Parent::onMouseLeaveBounds(%this);
    if (!($CSShoppingBrowser::InstaTestDriveEnabled)) {
        if ($CSInstaTestDrive) {
        }
        if (!(isEventPending($CSShoppingBrowserSKUItem::deleteEvent))) {
            $CSInstaTestDrive = 0;
            csTestFreeSelectedItem();
        }
        return;
    }
    if ((%this SPC name $= "")) {
        warn("CSShoppingBrowserSKUItem .name has gone missing?");
        return;
    }
    if ($CSInstaTestDrive) {
        if (isEventPending($CSShoppingBrowserSKUItem::deleteEvent)) {
            cancel($CSShoppingBrowserSKUItem::deleteEvent);
        }
        %code = "$CSInstaTestDrive = false;" @ "csTestFreeSelectedItem();";
        $CSShoppingBrowserSKUItem::deleteEvent = schedule(500, 0, "eval", %code);
    }
};
function CSShoppingBrowser::refreshInventory(%this) {
    if (isObject(storeInfo)) {
        storeInfo.refreshInventory("CSShoppingBrowser::onGotFurnishingsStore");
    }
    getFurnitureStore("CSShoppingBrowser::onGotFurnishingsStore");
};
function CSShoppingBrowser::onGotFurnishingsStore(%storeInfo, %status) {
    if ((%status $= "success")) {
        storeInfo = %storeInfo @ CSShoppingBrowser;
        loadAvailableSkus();
    }
};
function CSShoppingItemPane::update(%this) {
    %sku = sku;
    node;
    if ((%this SPC %sku $= "")) {
        return;
    }
    %si = %sku.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        log("inventory", "error", getScopeName() @ " " @ "- invalid sku: \"" @ %sku @ "\"." @ " " @ getTrace());
        return;
    }
    %skus = "";
    %delim = "";
    %n = 0;
    if ((buyQuantity < %n)) {
        %skus = %this @ %skus @ %delim @ %sku;
        %delim = " ";
        %n = (1.0 + %n);
    }
    command = %this @ testDriveButton;
    %this @ (buyQuantity < %n) @ "CSShoppingBrowser.testDriveSku(\"" @ %sku @ "\");";
    command = %this @ buyButton;
    "CSShoppingBrowser.purchaseSkus(\"" @ %skus @ "\");";
    if ((%si < priceVPoints)) {
    }
    %vpoints = priceVPoints;
    %si;
    if ((%si < priceVBux)) {
    }
    %vbux = priceVBux;
    %si;
    %vpoints = (buyQuantity * %vpoints);
    %this;
    %vbux = (buyQuantity * %vbux);
    %this;
    if ((%this == buyQuantity)) {
        buyButton.setText("Buy It!");
        buyFewerButton.setActive(0);
    }
    buyButton.setText("Buy" @ " " @ %this @ buyQuantity @ "!");
    buyFewerButton.setActive(1);
    %vpointsIcon = "<bitmap:platform/client/ui/vpoints_14>";
    %this;
    %vbuxIcon = "<bitmap:platform/client/ui/vbux_14>";
    %this;
    priceTextVBux.setText(1.0 @ %this @ %this @ %this @ "<color:ffffff>" @ %vbuxIcon @ "  " @ %vbux @ "");
    priceTextVPoints.setText("-" @ 0.0 @ "-" @ %this @ "<color:ffffff>" @ %vpointsIcon @ " " @ %vpoints @ "");
};
function CSShoppingBrowser::switchToOtherBrowser(%this) {
    close();
    open();
};
$gCSShoppingBrowserFilterFieldTimerID = "";
function CSShoppingBrowserFilterField::OnTextChanged(%this) {
    cancel($gCSShoppingBrowserFilterFieldTimerID);
    $gCSShoppingBrowserFilterFieldTimerID = %this.schedule(timeoutMS, "onTimer");
    %this;
};
function CSShoppingBrowserFilterField::OnEnterKey(%this) {
    %this.refilter();
};
function CSShoppingBrowserFilterField::onTimer(%this) {
    %this.refilter();
};
function CSShoppingBrowserFilterField::refilter(%this) {
    cancel($gCSShoppingBrowserFilterFieldTimerID);
    $gCSShoppingBrowserFilterFieldTimerID = "";
    %filterText = %this.getValue();
    if ((%this $= prevFilterText)) {
        return %filterText;
    }
    prevFilterText = %filterText @ %this;
    filterText = %filterText @ CSShoppingBrowser;
    goToCurrentPath();
    %filterText.setValue();
    filterText = CSInventoryBrowserFilterField @ %filterText @ CSInventoryBrowser;
    CSShoppingBrowser;
    goToCurrentPath();
};
