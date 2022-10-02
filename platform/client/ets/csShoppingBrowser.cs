function CSShoppingBrowserWindow::open(%this)
{
    %previouslyOpen = %this.isVisible();
    closeCSPanelsInOtherCategories(%this);
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    if (!%previouslyOpen)
    {
        CSShoppingBrowser.refreshInventory();
    }
    CSFurnitureMover.open();
    WindowManager.update();
    CustomSpaceClient::checkEditingSpace();
    if (!%previouslyOpen)
    {
        CSShoppingBrowser.focusCurrentFrame();
    }
    return ;
}
function CSShoppingBrowserWindow::close(%this)
{
    if (($CSSelectedSku != -1) && !$CSSelectedIsOwned)
    {
        csTestFreeSelectedItem();
    }
    %this.setVisible(0);
    PlayGui.resetFirstResponder();
    CustomSpaceClient::checkEditingSpace();
    WindowManager.update();
    return 1;
}
function CSShoppingBrowserWindow::toggle(%this)
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
function CSShoppingBrowserWindow::Initialize(%this)
{
    if (!%this.initialized)
    {
        %ctrl = TreeBrowserControl::newControl(CSShoppingBrowserContainer, "CSBrowser");
        %ctrl.bindClassName("CSShoppingBrowser");
        %ctrl.setName("CSShoppingBrowser");
        CSShoppingBrowser.menuProfile = "ETSClearMenuProfile";
        CSShoppingBrowser.selectedProfile = "ETSSelectedMenuItemNoBorderProfile";
        CSShoppingBrowser.setNumChildren(1);
        CSShoppingBrowser.adjustMenuCellHeight = 1;
        CSInventoryBrowser.showMoreInfo = 1;
        if (isObject(CSShoppingBrowser.getFieldValue("button0")))
        {
            CSShoppingBrowser.button[0].delete();
        }
        %container = CSShoppingBrowser.getParent();
        CSShoppingBrowser.button[0] = new GuiBitmapButtonCtrl()
        {
            profile = "ETSShopVerticalButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 0";
            extent = CSShoppingBrowser.buttonWidth SPC getWord(%container.getExtent(), 1);
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
        };
        %container.add(CSShoppingBrowser.button[0]);
        CSShoppingBrowser.vBuxIcon = new GuiBitmapCtrl()
        {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "top";
            position = "3 20";
            extent = "14 14";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = "platform/client/ui/vbux_14";
        };
        %container.add(CSShoppingBrowser.vBuxIcon);
        CSShoppingBrowser.baseDir = "Shop";
        CSShoppingBrowser.addNode(CSShoppingBrowser.baseDir);
        CSShoppingBrowser.storeInfo = 0;
        CSShoppingBrowser.refreshInventory();
        CSShoppingBrowser.loadAvailableSkus();
        %this.initialized = 1;
    }
    return ;
}
function CSShoppingBrowser::loadAvailableSkus(%this)
{
    %this.clear();
    %skulist = "";
    %numSkus = 0;
    if (isObject(%this.storeInfo))
    {
        %skulist = %this.storeInfo.getSkus();
        %numSkus = getWordCount(%skulist);
        %this.statusText = %numSkus == 0 ? "No furnishings available to buy." : "";
    }
    else
    {
        %this.statusText = "Getting Store Info...";
    }
    %i = 0;
    while (%i < %numSkus)
    {
        %this.addSku(getWord(%skulist, %i));
        %i = %i + 1;
    }
    if ((%this.Path $= "") && (%this.Path $= %this.baseDir))
    {
        %this.goToPath(%this.baseDir, 0);
    }
    else
    {
        %this.update();
    }
    return ;
}
function CSShoppingBrowser::fillLeafPane(%this, %pane)
{
    Parent::fillLeafPane(%this, %pane);
    %desc = getField(%this.Path, %this.level - 1);
    %desc = %this.getMenuText(%desc);
    %paneWidth = getWord(%pane.getExtent(), 0);
    %paneHeight = getWord(%pane.getExtent(), 1);
    %pane.buyQuantity = 1;
    if (!((%desc $= "")) && !((%desc $= %this.baseDir)))
    {
        %ypos = getWord(%pane.itemText.getExtent(), 1);
        %rightYPos = 18;
        %rmVPadding = 4;
        %buttonWidth = 62;
        %rightXPos = %paneWidth - %buttonWidth;
        %sku = getSubStr(strchr(getField(%this.Path, %this.level - 1), "|"), 1);
        %si = SkuManager.findBySku(%sku);
        if (!(%si.descLong $= ""))
        {
            %moreButton = new GuiVariableWidthButtonCtrl()
            {
                profile = "BracketButton15Profile";
                horizSizing = "right";
                vertSizing = "bottom";
                position = %rightXPos - 4 SPC %rightYPos = %rightYPos + %rmVPadding;
                extent = %buttonWidth SPC 15;
                minExtent = "1 1";
                sluggishness = -1;
                visible = 1;
                command = "CSShoppingBrowser.showMoreFor(" @ %sku @ ");";
                text = "More..";
                groupNum = -1;
                buttonType = "PushButton";
            };
            %rightYPos = %rightYPos + getWord(%moreButton.getExtent(), 1);
            if (%this.getFieldValue("showMoreInfo"))
            {
                %moreButton.text = "Less..";
                %moreButton.command = "CSShoppingBrowser.showMoreInfo = false; CSShoppingBrowser.goToCurrentPath();";
            }
            %pane.add(%moreButton);
            %pane.moreButton = %moreButton;
        }
        %testDriveButton = new GuiVariableWidthButtonCtrl()
        {
            profile = "BracketButton15Profile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %rightXPos - 4 SPC %rightYPos;
            extent = %buttonWidth SPC 15;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            command = "";
            text = "Try It!";
            groupNum = -1;
            buttonType = "PushButton";
        };
        %rightYPos = %rightYPos + getWord(%testDriveButton.getExtent(), 1);
        %buyButton = new GuiVariableWidthButtonCtrl()
        {
            profile = "BracketButton15Profile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %rightXPos - 4 SPC %rightYPos = %rightYPos + %rmVPadding;
            extent = %buttonWidth SPC 15;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            command = "";
            text = "Buy It!";
            groupNum = -1;
            buttonType = "PushButton";
        };
        %rightYPos = %rightYPos + getWord(%buyButton.getExtent(), 1);
        %smallButtonWidth = 15;
        %buyFewerButton = new GuiVariableWidthButtonCtrl()
        {
            profile = "BracketButton15Profile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = ((%rightXPos - 4) + %buttonWidth) - ((%smallButtonWidth * 2) + 5) SPC %rightYPos + %rmVPadding;
            extent = %smallButtonWidth SPC 15;
            command = %pane @ ".buyFewer();";
            text = "-";
        };
        %buyMoreButton = new GuiVariableWidthButtonCtrl()
        {
            profile = "BracketButton15Profile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = ((%rightXPos - 4) + %buttonWidth) - %smallButtonWidth SPC %rightYPos + %rmVPadding;
            extent = %smallButtonWidth SPC 15;
            command = %pane @ ".buyMore();";
            text = "+";
        };
        %rightYPos = %rightYPos + getWord(%buyMoreButton.getExtent(), 1);
        %rightYPos = %rightYPos + %rmVPadding;
        %priceTextVPoints = new GuiMLTextCtrl()
        {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %rightXPos - 4 SPC %rightYPos;
            extent = %buttonWidth SPC 18;
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
        %rightYPos = %rightYPos + getWord(%priceTextVPoints.getExtent(), 1);
        %priceTextVBux = new GuiMLTextCtrl()
        {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %rightXPos - 4 SPC %rightYPos;
            extent = %buttonWidth SPC 18;
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
        %rightYPos = %rightYPos + getWord(%priceTextVBux.getExtent(), 1);
        %ypos = %ypos + 2;
        %nextPrevTextY = getWord(%pane.nextPrevText.getPosition(), 1);
        %maxThumbnailHeight = %nextPrevTextY - %ypos;
        %maxThumbnailHeight = mMin(%maxThumbnailHeight, %rightXPos - 3);
        %ypos = ((%nextPrevTextY - (%ypos + %maxThumbnailHeight)) / 2) + %ypos;
        %ypos = mCeil(%ypos);
        %xPos = 0;
        %thumbnail = new GuiBitmapCtrl()
        {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "top";
            position = %xPos SPC %ypos;
            extent = %maxThumbnailHeight SPC %maxThumbnailHeight;
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
        if (!(getWord(%pane.getNamespaceList(), 0) $= "CSShoppingItemPane"))
        {
            %pane.bindClassName("CSShoppingItemPane");
        }
        %pane.update();
    }
    else
    {
        %noItemText = new GuiMLTextCtrl()
        {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "25 20";
            extent = %paneWidth - 5 SPC 18;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            lineSpacing = 0;
            allowColorChars = 1;
            maxChars = -1;
            text = "<color:ffffff>" @ %this.statusText;
        };
        %pane.add(%noItemText);
    }
    return ;
}
function CSShoppingItemPane::buyMore(%this)
{
    %this.buyQuantity = %this.buyQuantity + 1;
    %this.update();
    return ;
}
function CSShoppingItemPane::buyFewer(%this)
{
    %this.buyQuantity = %this.buyQuantity - 1;
    %this.buyQuantity = %this.buyQuantity < 1 ? 1 : %this;
    %this.update();
    return ;
}
function CSShoppingBrowser::testDriveSku(%this, %sku)
{
    $CSInstaTestDrive = 0;
    if (%sku > 0)
    {
        $CSSelectedSku = %sku;
        commandToServer('CreateInventoryBySkuJustTestingItOut', CustomSpaceClient::GetSpaceImIn(), %sku);
    }
    else
    {
        error(getScopeName() SPC "No sku selected");
    }
    return ;
}
function CSShoppingBrowser::purchaseSkus(%this, %skus)
{
    if (!isObject(%this.storeInfo))
    {
        return ;
    }
    %cbPoints = "CSShoppingBrowser.purchaseSkusVPoints(\"" @ %skus @ "\");";
    %cbBux = "CSShoppingBrowser.purchaseSkusVBux(\"" @ %skus @ "\");";
    %cbCancel = "";
    ShowPurchaseSkusConfirmationDialog(%skus, %cbPoints, %cbBux, %cbCancel);
    return ;
}
function CSShoppingBrowser::purchaseSkusVPoints(%this, %skus)
{
    if (isObject(%this.storeInfo))
    {
        %totalPrice = Inventory::getTotalPrice("vPoints", %skus);
        %itemCount = getWordCount(%skus);
        %itemsStr = %itemCount == 1 ? "this item" : "these";
        if (%totalPrice <= $Player::VPoints)
        {
            %vpointsString = %totalPrice == 1 ? "vPoint" : "vPoints";
            %msg = "Do you wish to purchase " @ %itemsStr @ " for " @ %totalPrice @ " " @ %vpointsString @ "?";
            %cmd = "CSShoppingBrowser.storeInfo.purchase(\"" @ %skus @ "\", \"vPoints\", \"CSShoppingBrowser::onGotPurchaseResult\");";
            MessageBoxOkCancel("Confirm Purchase", %msg, %cmd, "");
        }
        else
        {
            MessageBoxOK("Not Enough vPoints", "You do not have enough vPoints to purchase " @ %itemsStr @ ".  Click <a:" @ $Net::HelpURL_VPoints @ ">here</a> for more information about earning vPoints.", "");
        }
    }
    return ;
}
function CSShoppingBrowser::purchaseSkusVBux(%this, %skus)
{
    if (isObject(%this.storeInfo))
    {
        %totalPrice = Inventory::getTotalPrice("vBux", %skus);
        %itemCount = getWordCount(%skus);
        %itemsStr = %itemCount == 1 ? "this item" : "these";
        if (%totalPrice <= $Player::VBux)
        {
            %msg = "Do you wish to purchase " @ %itemsStr @ " for " @ %totalPrice @ " vBux?";
            %cmd = "CSShoppingBrowser.storeInfo.purchase(\"" @ %skus @ "\", \"vBux\", \"CSShoppingBrowser::onGotPurchaseResult\");";
            MessageBoxOkCancel("Confirm Purchase", %msg, %cmd, "");
        }
        else
        {
            MessageBoxOK("Not Enough vBux", "You do not have enough vBux to purchase " @ %itemsStr @ ".  Click <a:" @ $Net::AddFundsURL @ ">here</a> to refill your account.", "");
        }
    }
    return ;
}
function CSShoppingBrowser::onGotPurchaseResult(%status, %results)
{
    if (%status $= "success")
    {
        %numResults = getWordCount(%results);
        %i = 0;
        while (%i < %numResults)
        {
            %result = getWord(%results, %i);
            %result = strreplace(%result, "|", " ");
            %sku = getWord(%result, 0);
            %resultCode = getWord(%result, 1);
            if (%resultCode $= "pass")
            {
                if (($CSSelectedSku == %sku) && !$CSSelectedIsOwned)
                {
                    csTestFreeSelectedItem();
                }
                addFurnitureSku(%sku, 1);
                CSInventoryBrowser.loadAvailableSkus();
                if (%i == 0)
                {
                    CustomSpaceClient::placeSkuInWorld(%sku);
                }
            }
            %i = %i + 1;
        }
    }
    else
    {
        MessageBoxOK("Purchase Unsuccessful", "There was a problem completing your purchase.", "");
    }
    return ;
}
function CSShoppingBrowser::goToPath(%this, %path, %focus)
{
    if (!isDefined("%focus"))
    {
        %focus = 1;
    }
    %oldPath = %this.Path;
    Parent::goToPath(%this, %path, %focus);
    %curMenu = %this.getCurrentMenu();
    %count = %curMenu.getCount();
    %i = 0;
    while (%i < %count)
    {
        %menuItem = %curMenu.getObject(%i);
        %sku = strchr(%menuItem.name, "|");
        if (!(%sku $= ""))
        {
            if (!(getWord(%menuItem.getNamespaceList(), 0) $= "CSShoppingBrowserSKUItem"))
            {
                %menuItem.bindClassName("CSShoppingBrowserSKUItem");
            }
        }
        %i = %i + 1;
    }
    CSShoppingBrowser.vBuxIcon.reposition(3, getWord(%this.getExtent(), 1) - 20);
    %pathSku = getSubStr(strchr(%path, "|"), 1);
    if (((($CSSelectedSku != -1) && !$CSSelectedIsOwned) && !((%path $= %oldPath))) && !((%pathSku $= $CSSelectedSku)))
    {
        csTestFreeSelectedItem();
    }
    return ;
}
$CSShoppingBrowser::InstaTestDriveEnabled = 0;
$CSInstaTestDrive = 0;
$CSShoppingBrowserSKUItem::deleteEvent = 0;
function CSShoppingBrowserSKUItem::onMouseEnterBounds(%this)
{
    Parent::onMouseEnterBounds(%this);
    if (!$CSShoppingBrowser::InstaTestDriveEnabled)
    {
        return ;
    }
    if (%this.name $= "")
    {
        warn("CSShoppingBrowserSKUItem .name has gone missing?");
        return ;
    }
    if (($CSSelectedSku == -1) && !$CSSelectedIsOwned)
    {
        if (isEventPending($CSShoppingBrowserSKUItem::deleteEvent))
        {
            cancel($CSShoppingBrowserSKUItem::deleteEvent);
        }
        %sku = getSubStr(strchr(%this.name, "|"), 1);
        $CSInstaTestDrive = 1;
        $CSSelectedSku = %sku;
        commandToServer('CreateInventoryBySkuJustTestingItOut', CustomSpaceClient::GetSpaceImIn(), %sku);
    }
    return ;
}
function CSShoppingBrowserSKUItem::onMouseLeaveBounds(%this)
{
    Parent::onMouseLeaveBounds(%this);
    if (!$CSShoppingBrowser::InstaTestDriveEnabled)
    {
        if ($CSInstaTestDrive && !isEventPending($CSShoppingBrowserSKUItem::deleteEvent))
        {
            $CSInstaTestDrive = 0;
            csTestFreeSelectedItem();
        }
        return ;
    }
    if (%this.name $= "")
    {
        warn("CSShoppingBrowserSKUItem .name has gone missing?");
        return ;
    }
    if ($CSInstaTestDrive)
    {
        if (isEventPending($CSShoppingBrowserSKUItem::deleteEvent))
        {
            cancel($CSShoppingBrowserSKUItem::deleteEvent);
        }
        %code = "$CSInstaTestDrive = false;" @ "csTestFreeSelectedItem();";
        $CSShoppingBrowserSKUItem::deleteEvent = schedule(500, 0, "eval", %code);
    }
    return ;
}
function CSShoppingBrowser::refreshInventory(%this)
{
    if (isObject(%this.storeInfo))
    {
        %this.storeInfo.refreshInventory("CSShoppingBrowser::onGotFurnishingsStore");
    }
    else
    {
        getFurnitureStore("CSShoppingBrowser::onGotFurnishingsStore");
    }
    return ;
}
function CSShoppingBrowser::onGotFurnishingsStore(%storeInfo, %status)
{
    if (%status $= "success")
    {
        CSShoppingBrowser.storeInfo = %storeInfo;
        CSShoppingBrowser.loadAvailableSkus();
    }
    return ;
}
function CSShoppingItemPane::update(%this)
{
    %sku = %this.node.sku;
    if (%sku $= "")
    {
        return ;
    }
    %si = SkuManager.findBySku(%sku);
    if (!isObject(%si))
    {
        log("inventory", "error", getScopeName() SPC "- invalid sku: \"" @ %sku @ "\"." SPC getTrace());
        return ;
    }
    %skus = "";
    %delim = "";
    %n = 0;
    while (%n < %this.buyQuantity)
    {
        %skus = %skus @ %delim @ %sku;
        %delim = " ";
        %n = %n + 1;
    }
    %this.testDriveButton.command = "CSShoppingBrowser.testDriveSku(\"" @ %sku @ "\");";
    %this.buyButton.command = "CSShoppingBrowser.purchaseSkus(\"" @ %skus @ "\");";
    %vpoints = %si.priceVPoints < 0 ? "-" : %si;
    %vbux = %si.priceVBux < 0 ? "-" : %si;
    %vpoints = %vpoints * %this.buyQuantity;
    %vbux = %vbux * %this.buyQuantity;
    if (%this.buyQuantity == 1)
    {
        %this.buyButton.setText("Buy It!");
        %this.buyFewerButton.setActive(0);
    }
    else
    {
        %this.buyButton.setText("Buy" SPC %this.buyQuantity @ "!");
        %this.buyFewerButton.setActive(1);
    }
    %vpointsIcon = "<bitmap:platform/client/ui/vpoints_14>";
    %vbuxIcon = "<bitmap:platform/client/ui/vbux_14>";
    %this.priceTextVBux.setText("<color:ffffff>" @ %vbuxIcon @ "  " @ %vbux @ "");
    %this.priceTextVPoints.setText("<color:ffffff>" @ %vpointsIcon @ " " @ %vpoints @ "");
    return ;
}
function CSShoppingBrowser::switchToOtherBrowser(%this)
{
    CSShoppingBrowserWindow.close();
    CSInventoryBrowserWindow.open();
    return ;
}
$gCSShoppingBrowserFilterFieldTimerID = "";
function CSShoppingBrowserFilterField::OnTextChanged(%this)
{
    cancel($gCSShoppingBrowserFilterFieldTimerID);
    $gCSShoppingBrowserFilterFieldTimerID = %this.schedule(%this.timeoutMS, "onTimer");
    return ;
}
function CSShoppingBrowserFilterField::OnEnterKey(%this)
{
    %this.refilter();
    return ;
}
function CSShoppingBrowserFilterField::onTimer(%this)
{
    %this.refilter();
    return ;
}
function CSShoppingBrowserFilterField::refilter(%this)
{
    cancel($gCSShoppingBrowserFilterFieldTimerID);
    $gCSShoppingBrowserFilterFieldTimerID = "";
    %filterText = %this.getValue();
    if (%filterText $= %this.prevFilterText)
    {
        return ;
    }
    %this.prevFilterText = %filterText;
    CSShoppingBrowser.filterText = %filterText;
    CSShoppingBrowser.goToCurrentPath();
    CSInventoryBrowserFilterField.setValue(%filterText);
    CSInventoryBrowser.filterText = %filterText;
    CSInventoryBrowser.goToCurrentPath();
    return ;
}
