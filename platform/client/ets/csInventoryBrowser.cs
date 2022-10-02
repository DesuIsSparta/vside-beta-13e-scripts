function CSInventoryBrowserWindow::open(%this)
{
    %previouslyOpen = %this.isVisible();
    closeCSPanelsInOtherCategories(%this);
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    CSFurnitureMover.open();
    WindowManager.update();
    CustomSpaceClient::checkEditingSpace();
    if (!%previouslyOpen)
    {
        CSInventoryBrowser.focusCurrentFrame();
    }
    return ;
}
function CSInventoryBrowserWindow::close(%this)
{
    %this.setVisible(0);
    PlayGui.resetFirstResponder();
    CustomSpaceClient::checkEditingSpace();
    WindowManager.update();
    return 1;
}
function CSInventoryBrowserWindow::toggle(%this)
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
function CSInventoryBrowserWindow::Initialize(%this)
{
    if (!%this.initialized)
    {
        %ctrl = TreeBrowserControl::newControl(CSInventoryBrowserContainer, "CSBrowser");
        %ctrl.bindClassName("CSInventoryBrowser");
        %ctrl.setName("CSInventoryBrowser");
        CSInventoryBrowser.menuProfile = "ETSClearMenuProfile";
        CSInventoryBrowser.selectedProfile = "ETSSelectedMenuItemNoBorderProfile";
        CSInventoryBrowser.setNumChildren(1);
        CSInventoryBrowser.adjustMenuCellHeight = 1;
        CSInventoryBrowser.showMoreInfo = 1;
        CSInventoryBrowser.baseDir = "My Furnishings";
        CSInventoryBrowser.addNode(CSInventoryBrowser.baseDir);
        CSInventoryBrowser.loadAvailableSkus();
        $gGotFurnitureCallback = "CSInventoryBrowser.loadAvailableSkus();" @ "CSFurnitureMoverText.update();" @ "CSFurnitureMover.updateButtonStates();";
        %this.initialized = 1;
    }
    else
    {
        CSInventoryBrowser.Path = "";
        CSInventoryBrowser.loadAvailableSkus();
    }
    return ;
}
function CSInventoryBrowserWindow::onResized(%this)
{
    %extent = %this.getExtent();
    CSInventoryBrowserContainer.resize(getWord(%extent, 0) - 16, (getWord(%extent, 1) - 4) - 18);
    CSInventoryBrowser.onResized();
    return ;
}
function CSInventoryBrowserWindow::onReachedTarget(%this)
{
    WindowManager.update();
    return ;
}
function CSInventoryBrowser::loadAvailableSkus(%this)
{
    %this.clear();
    %skulist = getFurnitureSkus();
    %numSkus = getWordCount(%skulist);
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
function CSInventoryBrowser::fillLeafPane(%this, %pane)
{
    Parent::fillLeafPane(%this, %pane);
    %desc = getField(%this.Path, %this.level - 1);
    %desc = %this.getMenuText(%desc);
    %paneWidth = getWord(%pane.getExtent(), 0);
    %paneHeight = getWord(%pane.getExtent(), 1);
    if (!((%desc $= "")) && !((%desc $= %this.baseDir)))
    {
        %ypos = getWord(%pane.itemText.getPosition(), 1) + getWord(%pane.itemText.getExtent(), 1);
        %qtyText = new GuiMLTextCtrl()
        {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = 5 SPC %ypos = %ypos + 3;
            extent = %paneWidth - 5 SPC 18;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            lineSpacing = 0;
            allowColorChars = 1;
            maxChars = -1;
            text = "";
        };
        %ypos = %ypos + getWord(%qtyText.getExtent(), 1);
        %distributionText = new GuiMLTextCtrl()
        {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = 5 SPC %ypos;
            extent = %paneWidth - 5 SPC 18;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            lineSpacing = 0;
            allowColorChars = 1;
            maxChars = -1;
            text = "";
        };
        %ypos = %ypos + getWord(%distributionText.getExtent(), 1);
        %buttonWidth = 68;
        %xPos = (%paneWidth - %buttonWidth) - 3;
        %rightYPos = 18;
        %placeButton = new GuiVariableWidthButtonCtrl()
        {
            profile = "BracketButton15Profile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos SPC %rightYPos;
            extent = %buttonWidth SPC 15;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            command = "";
            text = "Place";
            groupNum = -1;
            buttonType = "PushButton";
        };
        %rightYPos = %rightYPos + getWord(%placeButton.getExtent(), 1);
        %putAwayButton = new GuiVariableWidthButtonCtrl()
        {
            profile = "BracketButton15Profile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos SPC %rightYPos = %rightYPos + 4;
            extent = %buttonWidth SPC 15;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            command = "";
            text = "Put Away";
            groupNum = -1;
            buttonType = "PushButton";
        };
        %rightYPos = %rightYPos + getWord(%putAwayButton.getExtent(), 1);
        %buyButton = new GuiVariableWidthButtonCtrl()
        {
            profile = "BracketButton15Profile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos SPC %rightYPos = %rightYPos + 4;
            extent = %buttonWidth SPC 15;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            command = "";
            text = "Buy More";
            groupNum = -1;
            buttonType = "PushButton";
        };
        %rightYPos = %rightYPos + getWord(%buyButton.getExtent(), 1);
        %si = "";
        %sku = getSubStr(strchr(getField(%this.Path, %this.level - 1), "|"), 1);
        %si = SkuManager.findBySku(%sku);
        if (!(%si.descLong $= ""))
        {
            %moreInfoButton = new GuiVariableWidthButtonCtrl()
            {
                profile = "BracketButton15RedProfile";
                horizSizing = "right";
                vertSizing = "bottom";
                position = %xPos SPC %rightYPos = %rightYPos + 4;
                extent = %buttonWidth SPC 15;
                minExtent = "1 1";
                sluggishness = -1;
                visible = 1;
                command = "CSInventoryBrowser.showMoreFor(" @ %sku @ ");";
                text = "More Info";
                groupNum = -1;
                buttonType = "PushButton";
            };
            %rightYPos = %rightYPos + getWord(%buyButton.getExtent(), 1);
            if (%this.getFieldValue("showMoreInfo"))
            {
                %moreInfoButton.text = "Less Info";
                %moreInfoButton.command = "CSInventoryBrowser.showMoreInfo = false; CSInventoryBrowser.goToCurrentPath();";
            }
            %pane.moreInfoButton = %moreInfoButton;
            %pane.add(%moreInfoButton);
        }
        %maxThumbnailDim = mMin((%paneHeight - %ypos) - 3, %xPos - 2);
        %thumbnail = new GuiBitmapCtrl()
        {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "top";
            position = 0 SPC %ypos + 2;
            extent = %maxThumbnailDim SPC %maxThumbnailDim;
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
        if (!(getWord(%pane.getNamespaceList(), 0) $= "CSInventoryItemPane"))
        {
            %pane.bindClassName("CSInventoryItemPane");
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
            text = "<color:ffffff>You don\'t own any furnishings.";
        };
        %pane.add(%noItemText);
    }
    return ;
}
function CSInventoryItemPane::update(%this)
{
    %sku = %this.node.sku;
    if (%sku $= "")
    {
        return ;
    }
    %this.placeButton.command = "CustomSpaceClient::placeSkuInWorld(" @ %sku @ ");";
    %this.putAwayButton.command = "csTestFreeSelectedItem();";
    %this.putAwayButton.setActive((%sku == $CSSelectedSku) && $CSSelectedIsOwned);
    %this.buyButton.command = "CSInventoryBrowser.switchToOtherBrowser(); CSShoppingBrowser.navigateToSku(" @ %sku @ ");";
    %numOwned = numOwnedFurnitureSku(%sku);
    %numPlaced = numUsingFurnitureSku(%sku);
    %numStored = %numOwned - %numPlaced;
    %omni = %numOwned == -1;
    %txtOwned = %omni ? "You own many of these," : "You own ";
    %txtPlaced = %omni ? %numPlaced : %numPlaced;
    %txtStored = %omni ? "" : %numStored;
    %this.qtyText.setText("<color:ffffff>" @ %txtOwned);
    %this.distributionText.setText("<color:ffffff>" @ %txtPlaced @ %txtStored);
    if (%omni)
    {
        %this.placeButton.setActive(1);
        %this.buyButton.setActive(0);
    }
    else
    {
        %this.placeButton.setActive(%numPlaced < %numOwned);
        %this.buyButton.setActive(1);
    }
    return ;
}
function CSInventoryBrowser::switchToOtherBrowser(%this)
{
    CSInventoryBrowserWindow.close();
    CSShoppingBrowserWindow.open();
    return ;
}
$gCSInventoryBrowserFilterFieldTimerID = "";
function CSInventoryBrowserFilterField::OnTextChanged(%this)
{
    cancel($gCSInventoryBrowserFilterFieldTimerID);
    $gCSInventoryBrowserFilterFieldTimerID = %this.schedule(%this.timeoutMS, "onTimer");
    CSShoppingBrowserFilterField.setValue(%this.getValue());
    CSShoppingBrowser.filterText = %this.getValue();
    return ;
}
function CSInventoryBrowserFilterField::OnEnterKey(%this)
{
    %this.refilter();
    return ;
}
function CSInventoryBrowserFilterField::onTimer(%this)
{
    %this.refilter();
    return ;
}
function CSInventoryBrowserFilterField::refilter(%this)
{
    cancel($gCSInventoryBrowserFilterFieldTimerID);
    $gCSInventoryBrowserFilterFieldTimerID = "";
    %filterText = %this.getValue();
    if (%filterText $= %this.prevFilterText)
    {
        return ;
    }
    %this.prevFilterText = %filterText;
    CSInventoryBrowser.filterText = %filterText;
    CSInventoryBrowser.goToCurrentPath();
    CSShoppingBrowserFilterField.setValue(%filterText);
    CSShoppingBrowser.filterText = %filterText;
    CSShoppingBrowser.goToCurrentPath();
    return ;
}
