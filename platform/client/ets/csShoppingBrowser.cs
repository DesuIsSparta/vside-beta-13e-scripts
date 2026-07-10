function CSShoppingBrowserWindow::open(%this) {
    %previouslyOpen = %this.isVisible();
    closeCSPanelsInOtherCategories(%this);
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    if (!(%previouslyOpen)) {
        CSShoppingBrowser.refreshInventory();
    }
    CSFurnitureMover.open();
    WindowManager.update();
    CustomSpaceClient::checkEditingSpace();
    if (!(%previouslyOpen)) {
        CSShoppingBrowser.focusCurrentFrame();
    }
};
function CSShoppingBrowserWindow::close(%this) {
    if ((-(1.0) != $CSSelectedSku)) {
    }
    if (!($CSSelectedIsOwned)) {
        csTestFreeSelectedItem();
    }
    %this.setVisible(0);
    PlayGui.resetFirstResponder();
    CustomSpaceClient::checkEditingSpace();
    WindowManager.update();
    return 1;
};
function CSShoppingBrowserWindow::toggle(%this) {
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
function CSShoppingBrowserWindow::Initialize(%this) {
    if (!(%this.initialized)) {
        %ctrl = TreeBrowserControl::newControl(CSShoppingBrowserContainer, "CSBrowser");
        %ctrl.bindClassName("CSShoppingBrowser");
        %ctrl.setName("CSShoppingBrowser");
        %this.menuProfile = "ETSClearMenuProfile" @ CSShoppingBrowser;
        %this.selectedProfile = "ETSSelectedMenuItemNoBorderProfile" @ CSShoppingBrowser;
        CSShoppingBrowser.setNumChildren(1);
        %this.adjustMenuCellHeight = 1 @ CSShoppingBrowser;
        %this.showMoreInfo = 1 @ CSInventoryBrowser;
        if (isObject(CSShoppingBrowser.getFieldValue("button0"))) {
            0 @ CSShoppingBrowser.delete(%this.button);
        }
        %container = CSShoppingBrowser.getParent();
        button = new GuiBitmapButtonCtrl("") {
            profile = 0 @ "ETSShopVerticalButtonProfile";
            horizSizing = "right";
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
        }; @ 0 @ CSShoppingBrowser
        %container.add(0 @ CSShoppingBrowser, button);
        vBuxIcon = new GuiBitmapCtrl("") {
            profile = 0 @ "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "top";
            position = "3 20";
            extent = "14 14";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = "platform/client/ui/vbux_14";
        }; @ CSShoppingBrowser
        %container.add(CSShoppingBrowser, vBuxIcon);
        baseDir = "Shop" @ CSShoppingBrowser;
        CSShoppingBrowser.addNode(CSShoppingBrowser, baseDir);
        storeInfo = 0 @ CSShoppingBrowser;
        CSShoppingBrowser.refreshInventory();
        CSShoppingBrowser.loadAvailableSkus();
        %this.initialized = 1;
    }
};
function CSShoppingBrowser::loadAvailableSkus(%this) {
    %this.clear();
    %skulist = "";
    %numSkus = 0;
    if (isObject(%this.storeInfo)) {
        %skulist = %this.storeInfo.getSkus();
        %numSkus = getWordCount(%skulist);
        %this.statusText = (0.0 == %numSkus) ? "No furnishings available to buy." : "";
    }
    %this.statusText = "Getting Store Info...";
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
function CSShoppingBrowser::fillLeafPane(%this, %pane) {
    Parent::fillLeafPane(%this, %pane);
    %desc = getField(%this.Path, (1.0 - %this.level));
    %desc = %this.getMenuText(%desc);
    %paneWidth = getWord(%pane.getExtent(), 0);
    %paneHeight = getWord(%pane.getExtent(), 1);
    %pane.buyQuantity = 1;
    if (!(%desc $= "")) {
    }
    if (!(%desc $= %this.baseDir)) {
        %ypos = getWord(%pane.itemText.getExtent(), 1);
        %rightYPos = 18;
        %rmVPadding = 4;
        %buttonWidth = 62;
        %rightXPos = (%buttonWidth - %paneWidth);
        %sku = getSubStr(strchr(getField(%this.Path, (1.0 - %this.level)), "|"), 1);
        %si = SkuManager.findBySku(%sku);
        if (!(%si.descLong $= "")) {
            %rightYPos = (%rmVPadding + %rightYPos);
            %moreButton = new GuiVariableWidthButtonCtrl("") {
                profile = 0 @ "BracketButton15Profile";
                horizSizing = "right";
                vertSizing = "bottom";
                position = (4.0 - %rightXPos) @ " ";
                extent = %buttonWidth @ " " @ 15;
                minExtent = "1 1";
                sluggishness = -1;
                visible = 1;
                command = "CSShoppingBrowser.showMoreFor(" @ %sku @ ");";
                text = "More..";
                groupNum = -1;
                buttonType = "PushButton";
            };
            %rightYPos = (getWord(%moreButton.getExtent(), 1) + %rightYPos);
            if (%this.getFieldValue("showMoreInfo")) {
                %moreButton.text = "Less..";
                %moreButton.command = "CSShoppingBrowser.showMoreInfo = false; CSShoppingBrowser.goToCurrentPath();";
            }
            %pane.add(%moreButton);
            %pane.moreButton = %moreButton;
        }
        %testDriveButton = new GuiVariableWidthButtonCtrl("") {
            profile = 0 @ "BracketButton15Profile";
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
        };
        %rightYPos = (getWord(%testDriveButton.getExtent(), 1) + %rightYPos);
        %rightYPos = (%rmVPadding + %rightYPos);
        %buyButton = new GuiVariableWidthButtonCtrl("") {
            profile = 0 @ "BracketButton15Profile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = (4.0 - %rightXPos) @ " ";
            extent = %buttonWidth @ " " @ 15;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            command = "";
            text = "Buy It!";
            groupNum = -1;
            buttonType = "PushButton";
        };
        %rightYPos = (getWord(%buyButton.getExtent(), 1) + %rightYPos);
        %smallButtonWidth = 15;
        %buyFewerButton = new GuiVariableWidthButtonCtrl("") {
            profile = 0 @ "BracketButton15Profile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = ((5.0 + (2.0 * %smallButtonWidth)) - (%buttonWidth + (4.0 - %rightXPos))) @ " " @ (%rmVPadding + %rightYPos);
            extent = %smallButtonWidth @ " " @ 15;
            command = %pane @ ".buyFewer();";
            text = "-";
        };
        %buyMoreButton = new GuiVariableWidthButtonCtrl("") {
            profile = 0 @ "BracketButton15Profile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = (%smallButtonWidth - (%buttonWidth + (4.0 - %rightXPos))) @ " " @ (%rmVPadding + %rightYPos);
            extent = %smallButtonWidth @ " " @ 15;
            command = %pane @ ".buyMore();";
            text = "+";
        };
        %rightYPos = (getWord(%buyMoreButton.getExtent(), 1) + %rightYPos);
        %rightYPos = (%rmVPadding + %rightYPos);
        %priceTextVPoints = new GuiMLTextCtrl("") {
            profile = 0 @ "ETSNonModalProfile";
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
        };
        %pane.add(%priceTextVPoints);
        %pane.priceTextVPoints = %priceTextVPoints;
        %rightYPos = (getWord(%priceTextVPoints.getExtent(), 1) + %rightYPos);
        %priceTextVBux = new GuiMLTextCtrl("") {
            profile = 0 @ "ETSNonModalProfile";
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
        };
        %pane.add(%priceTextVBux);
        %pane.priceTextVBux = %priceTextVBux;
        %rightYPos = (getWord(%priceTextVBux.getExtent(), 1) + %rightYPos);
        %ypos = (2.0 + %ypos);
        %nextPrevTextY = getWord(%pane.nextPrevText.getPosition(), 1);
        %maxThumbnailHeight = (%ypos - %nextPrevTextY);
        %maxThumbnailHeight = mMin(%maxThumbnailHeight, (3.0 - %rightXPos));
        %ypos = (%ypos + (2.0 / ((%maxThumbnailHeight + %ypos) - %nextPrevTextY)));
        %ypos = mCeil(%ypos);
        %xPos = 0;
        %thumbnail = new GuiBitmapCtrl("") {
            profile = 0 @ "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "top";
            position = %xPos @ " " @ %ypos;
            extent = %maxThumbnailHeight @ " " @ %maxThumbnailHeight;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = %this.getThumbnailPathForSku(%sku, 128);
        };
        %pane.add(%thumbnail);
        %pane.add(%testDriveButton);
        %pane.add(%buyButton);
        %pane.add(%buyFewerButton);
        %pane.add(%buyMoreButton);
        %pane.thumbnail = %thumbnail;
        %pane.testDriveButton = %testDriveButton;
        %pane.buyButton = %buyButton;
        %pane.buyFewerButton = %buyFewerButton;
        %pane.buyMoreButton = %buyMoreButton;
        %pane.nextPrevText.browser = CSShoppingBrowser;
        if (!(getWord(%pane.getNamespaceList(), 0) $= "CSShoppingItemPane")) {
            %pane.bindClassName("CSShoppingItemPane");
        }
        %pane.update();
    }
    %noItemText = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
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
        text = "<color:ffffff>" @ %this.statusText;
    };
    %pane.add(%noItemText);
};
function CSShoppingItemPane::buyMore(%this) {
    %this.buyQuantity = (1.0 + %this.buyQuantity);
    %this.update();
};
function CSShoppingItemPane::buyFewer(%this) {
    %this.buyQuantity = (1.0 - %this.buyQuantity);
    if ((1.0 < %this.buyQuantity)) {
    }
    %this.buyQuantity = 1 @ %this.buyQuantity;
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
    if (!(isObject(%this.storeInfo))) {
        return;
    }
    %cbPoints = "CSShoppingBrowser.purchaseSkusVPoints(\"" @ %skus @ "\");";
    %cbBux = "CSShoppingBrowser.purchaseSkusVBux(\"" @ %skus @ "\");";
    %cbCancel = "";
    ShowPurchaseSkusConfirmationDialog(%skus, %cbPoints, %cbBux, %cbCancel);
};
function CSShoppingBrowser::purchaseSkusVPoints(%this, %skus) {
    if (isObject(%this.storeInfo)) {
        %totalPrice = Inventory::getTotalPrice("vPoints", %skus);
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
    if (isObject(%this.storeInfo)) {
        %totalPrice = Inventory::getTotalPrice("vBux", %skus);
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
                CSInventoryBrowser.loadAvailableSkus();
                if ((0.0 == %i)) {
                    CustomSpaceClient::placeSkuInWorld(%sku);
                }
            }
            %i = (1.0 + %i);
        }
    }
    MessageBoxOK("Purchase Unsuccessful", "There was a problem completing your purchase.", "");
};
function CSShoppingBrowser::goToPath(%this, %path, %focus) {
    if (!(isDefined("%focus"))) {
        %focus = 1;
    }
    %oldPath = %this.Path;
    Parent::goToPath(%this, %path, %focus);
    %curMenu = %this.getCurrentMenu();
    %count = %curMenu.getCount();
    %i = 0;
    if ((%count < %i)) {
        %menuItem = %curMenu.getObject(%i);
        %sku = strchr(%menuItem.name, "|");
        if (!(%sku $= "")) {
            if (!(getWord(%menuItem.getNamespaceList(), 0) $= "CSShoppingBrowserSKUItem")) {
                %menuItem.bindClassName("CSShoppingBrowserSKUItem");
            }
        }
        %i = (1.0 + %i);
    }
    CSShoppingBrowser.reposition(%menuItem.vBuxIcon, 3, (20.0 - getWord(%this.getExtent(), 1)));
    %pathSku = getSubStr(strchr(%path, "|"), 1);
    (%count < %i);
    if ((-(1.0) != $CSSelectedSku)) {
    }
    if (!($CSSelectedIsOwned)) {
    }
    if (!(%path $= %oldPath)) {
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
    if ((%this.name $= "")) {
        warn("CSShoppingBrowserSKUItem .name has gone missing?");
        return;
    }
    if ((-(1.0) == $CSSelectedSku)) {
    }
    if (!($CSSelectedIsOwned)) {
        if (isEventPending($CSShoppingBrowserSKUItem::deleteEvent)) {
            cancel($CSShoppingBrowserSKUItem::deleteEvent);
        }
        %sku = getSubStr(strchr(%this.name, "|"), 1);
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
    if ((%this.name $= "")) {
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
    if (isObject(%this.storeInfo)) {
        %this.storeInfo.refreshInventory("CSShoppingBrowser::onGotFurnishingsStore");
    }
    getFurnitureStore("CSShoppingBrowser::onGotFurnishingsStore");
};
function CSShoppingBrowser::onGotFurnishingsStore(%storeInfo, %status) {
    if ((%status $= "success")) {
        %this.storeInfo = %storeInfo @ CSShoppingBrowser;
        CSShoppingBrowser.loadAvailableSkus();
    }
};
function CSShoppingItemPane::update(%this) {
    %sku = %this.node.sku;
    if ((%sku $= "")) {
        return;
    }
    %si = SkuManager.findBySku(%sku);
    if (!(isObject(%si))) {
        log("inventory", "error", getScopeName() @ " " @ "- invalid sku: \"" @ %sku @ "\"." @ " " @ getTrace());
        return;
    }
    %skus = "";
    %delim = "";
    %n = 0;
    if ((%this.buyQuantity < %n)) {
        %skus = %skus @ %delim @ %sku;
        %delim = " ";
        %n = (1.0 + %n);
    }
    %this.testDriveButton.command = (%this.buyQuantity < %n) @ "CSShoppingBrowser.testDriveSku(\"" @ %sku @ "\");";
    %this.buyButton.command = "CSShoppingBrowser.purchaseSkus(\"" @ %skus @ "\");";
    if ((0.0 < %si.priceVPoints)) {
    }
    %vpoints = %si.priceVPoints;
    "-";
    if ((0.0 < %si.priceVBux)) {
    }
    %vbux = %si.priceVBux;
    "-";
    %vpoints = (%this.buyQuantity * %vpoints);
    %vbux = (%this.buyQuantity * %vbux);
    if ((1.0 == %this.buyQuantity)) {
        %this.buyButton.setText("Buy It!");
        %this.buyFewerButton.setActive(0);
    }
    %this.buyButton.setText("Buy" @ " " @ %this.buyQuantity @ "!");
    %this.buyFewerButton.setActive(1);
    %vpointsIcon = "<bitmap:platform/client/ui/vpoints_14>";
    %vbuxIcon = "<bitmap:platform/client/ui/vbux_14>";
    %this.priceTextVBux.setText("<color:ffffff>" @ %vbuxIcon @ "  " @ %vbux @ "");
    %this.priceTextVPoints.setText("<color:ffffff>" @ %vpointsIcon @ " " @ %vpoints @ "");
};
function CSShoppingBrowser::switchToOtherBrowser(%this) {
    CSShoppingBrowserWindow.close();
    CSInventoryBrowserWindow.open();
};
$gCSShoppingBrowserFilterFieldTimerID = "";
function CSShoppingBrowserFilterField::OnTextChanged(%this) {
    cancel($gCSShoppingBrowserFilterFieldTimerID);
    $gCSShoppingBrowserFilterFieldTimerID = %this.schedule(%this.timeoutMS, "onTimer");
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
    if ((%filterText $= %this.prevFilterText)) {
        return;
    }
    %this.prevFilterText = %filterText;
    %this.filterText = %filterText @ CSShoppingBrowser;
    CSShoppingBrowser.goToCurrentPath();
    CSInventoryBrowserFilterField.setValue(%filterText);
    %this.filterText = %filterText @ CSInventoryBrowser;
    CSInventoryBrowser.goToCurrentPath();
};
