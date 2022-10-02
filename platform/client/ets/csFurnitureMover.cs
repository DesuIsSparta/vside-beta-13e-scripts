$gCSFurnitureMoverClipboard = "";
function CSFurnitureMover::toggle(%this)
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
$CSMaximumSlots = 0;
function CSFurnitureMover::open(%this)
{
    %wasOpen = %this.isVisible();
    closeCSPanelsInOtherCategories(%this);
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    %this.updateButtonStates();
    %this.updateClickText();
    if ((!$CSSelectedFreeRotate || (%this.rotationAxis $= "")) || !%wasOpen)
    {
        %prevState = CSRotateZButton.isActive();
        if (!%prevState)
        {
            CSRotateZButton.setActive(1);
        }
        CSRotateZButton.performClick();
        CSRotateZButton.setActive(%prevState);
    }
    WindowManager.update();
    CustomSpaceClient::checkEditingSpace();
    csFurnitureMap.push();
    getUserActivityMgr().setActivityActive("decorating", 1);
    return ;
}
function CSFurnitureMover::close(%this)
{
    %wasOpen = %this.isVisible();
    %this.SelectNuggetID(-1);
    %this.setVisible(0);
    CustomSpaceClient::checkEditingSpace();
    PlayGui.focusTopWindow();
    WindowManager.update();
    if (%wasOpen)
    {
        csFurnitureMap.pop();
    }
    getUserActivityMgr().setActivityActive("decorating", 0);
    return 1;
}
function CSFurnitureMover::isInEditMode(%this)
{
    return %this.isVisible();
}
function CSFurnitureMover::updateButtonStates(%this)
{
    %itemSelected = $CSSelectedID != -1;
    customSpaceRaiseButton.setActive(%itemSelected);
    customSpaceLowerButton.setActive(%itemSelected);
    customSpaceRotateCWButton.setActive(%itemSelected);
    customSpaceRotateCCWButton.setActive(%itemSelected);
    customSpacePickupToggleButton.setActive(%itemSelected);
    customSpaceMoveLeftButton.setActive(%itemSelected);
    customSpaceMoveRightButton.setActive(%itemSelected);
    customSpaceMoveInButton.setActive(%itemSelected);
    customSpaceMoveOutButton.setActive(%itemSelected);
    CSRotateXButton.setActive(%itemSelected && $CSSelectedFreeRotate);
    CSRotateYButton.setActive(%itemSelected && $CSSelectedFreeRotate);
    CSRotateZButton.setActive(%itemSelected);
    customSpacePutAllAwayButton.setActive(numUsingFurnitureAll() > 0);
    if ($CSboolPickedUp)
    {
        customSpacePickupToggleButton.setText("Drop");
    }
    else
    {
        customSpacePickupToggleButton.setText("Pick Up");
    }
    return ;
}
function CSFurnitureMover::updateClickText(%this)
{
    %prefix = "<color:cccccc>Click = ";
    %moveBy = mRoundTo(csGetMoveClickSize(), 0.001);
    %moveUnit = %moveBy == 1 ? " foot" : " feet";
    CSMoveClickText.setText(%prefix @ "<color:ffffff>" @ %moveBy @ %moveUnit);
    %rotateBy = mRoundTo(csGetRotateClickSize(), 0.01);
    %rotateUnit = %rotateBy == 1 ? " degree" : " degrees";
    CSRotateClickText.setText(%prefix @ "<color:ffffff>" @ %rotateBy @ %rotateUnit);
    return ;
}
function CSFurnitureMover::preSelectedNuggetChanged(%this)
{
    if ($CSSelectedID != -1)
    {
        if ($CSboolPickedUp)
        {
            commandToServer('SlotPickUp', CustomSpaceClient::GetSpaceImIn(), $CSSelectedID);
            $CSboolPickedUp = 0;
        }
    }
    else
    {
        $CSboolPickedUp = 0;
    }
    return ;
}
function CSFurnitureMover::SelectNuggetObject(%this, %obj)
{
    if (isObject($CSSelectedGhost))
    {
        $CSSelectedGhost.SetSelected(0);
        $CSSelectedGhost = 0;
    }
    if (!isObject(%obj))
    {
        if (%obj != 0)
        {
            getNuggetGhostList("CSFurnitureMover::refreshGhostList");
        }
        return ;
    }
    %nuggetId = %obj.getInventoryNuggetID();
    if (%nuggetId < 0)
    {
        return ;
    }
    $CSSelectedGhost = %obj;
    $CSSelectedIsOwned = %obj.getInventoryNuggetIsOwned();
    $CSSelectedSku = %obj.getInventoryNuggetSKU();
    $CSSelectedFreeRotate = %obj.getInventoryNuggetFreeRotate();
    $CSSelectedGhost.SetSelected(1);
    %this.SelectNuggetID(%nuggetId);
    if ($CSSelectedIsOwned)
    {
        CSInventoryBrowserWindow.open();
        CSInventoryBrowser.navigateToSku($CSSelectedSku);
    }
    else
    {
        if (!$CSInstaTestDrive)
        {
            CSShoppingBrowserWindow.open();
            CSShoppingBrowser.navigateToSku($CSSelectedSku);
        }
    }
    if ($UserPref::Spaces::FaceSelected && !$CSInstaTestDrive)
    {
        commandToServer('orientTowards', ServerConnection.getGhostID($CSSelectedGhost), 400);
    }
    return ;
}
function CSFurnitureMover::SelectNuggetID(%this, %id)
{
    %lastSelectedID = $CSSelectedID;
    %wasSelectedOwned = $CSSelectedIsOwned;
    CSFurnitureMover.preSelectedNuggetChanged();
    $CSSelectedID = %id;
    CSFurnitureMoverText.update();
    %this.updateButtonStates();
    if (%id >= 0)
    {
        %this.open();
    }
    else
    {
        %this.SelectNuggetObject(0);
        $CSSelectedSku = -1;
        $CSSelectedIsOwned = 0;
        $CSSelectedGhost = 0;
        $CSSelectedFreeRotate = 0;
    }
    if (!CSInventoryBrowserWindow.initialized)
    {
        CSInventoryBrowserWindow.Initialize();
    }
    if ((((isObject(CSInventoryBrowser) && !$CSSelectedIsOwned) || !%wasSelectedOwned) && (%lastSelectedID != -1)) || ($CSSelectedSku != -1))
    {
        CSInventoryBrowser.update();
    }
    return ;
}
$gCSGhostList = "";
function CSFurnitureMover::refreshGhostList(%ghostlist)
{
    if (!(%ghostlist $= $gCSGhostList))
    {
        $gCSGhostList = %ghostlist;
        if ((!isObject($CSSelectedGhost) || ($CSSelectedGhost.getInventoryNuggetID() != $CSSelectedID)) && ($CSSelectedID > 0))
        {
            %numGhosts = getWordCount($gCSGhostList);
            %i = 0;
            while (%i < %numGhosts)
            {
                %ghost = getWord($gCSGhostList, %i);
                if (isObject(%ghost) && (%ghost.getInventoryNuggetID() == $CSSelectedID))
                {
                    CSFurnitureMover.SelectNuggetObject(%ghost);
                    if (%ghost.isClassAIPlayer())
                    {
                        rentabotClient_customizeBot(%ghost);
                    }
                    break;
                }
                %i = %i + 1;
            }
        }
        CSFurnitureMoverText.update();
    }
    return ;
}
function CSFurnitureMover::doCopyOrCut(%this, %obj, %operation)
{
    if (!isDefined("%obj"))
    {
        %obj = $CSSelectedGhost;
    }
    if (!isObject(%obj))
    {
        return ;
    }
    commandToServer('CSGetTransformForID', CustomSpaceClient::GetSpaceImIn(), %obj.getInventoryNuggetID(), %obj, %operation);
    return ;
}
function CSFurnitureMover::doCopy(%this, %obj)
{
    if (!isDefined("%obj"))
    {
        %obj = $CSSelectedGhost;
    }
    %this.doCopyOrCut(%obj, "copy");
    return ;
}
function CSFurnitureMover::doCut(%this, %obj)
{
    if (!isDefined("%obj"))
    {
        %obj = $CSSelectedGhost;
    }
    %this.doCopyOrCut(%obj, "cut");
    return ;
}
function clientCmdCSReturnTransformForID(%id, %position, %orientation, %obj, %operation)
{
    if (!isObject(%obj))
    {
        return ;
    }
    $gCSFurnitureMoverClipboard = %obj.getInventoryNuggetSKU() TAB %position TAB %orientation;
    if (%operation $= "cut")
    {
        CSFurnitureMover.SelectNuggetObject(%obj);
        csTestFreeSelectedItem();
    }
    return ;
}
$gCSFurnitureClipboardFieldCount = 3;
function CSFurnitureMover::canPaste(%this)
{
    %toPaste = $gCSFurnitureMoverClipboard;
    %record = getRecord(%toPaste, 0);
    if (getFieldCount(%record) != $gCSFurnitureClipboardFieldCount)
    {
        return 0;
    }
    %sku = getField(%record, 0);
    return numOwnedFurnitureSku(%sku) > numUsingFurnitureSku(%sku);
}
function CSFurnitureMover::doPaste(%this)
{
    %this.pasteString($gCSFurnitureMoverClipboard);
    return ;
}
function CSFurnitureMover::pasteString(%this, %toPaste)
{
    if (trim(%toPaste) $= "")
    {
        return ;
    }
    %record = getRecord(%toPaste, 0);
    if (getFieldCount(%record) == $gCSFurnitureClipboardFieldCount)
    {
        %sku = getField(%record, 0);
        %position = getField(%record, 1);
        %orientation = getField(%record, 2);
        CustomSpaceClient::placeSkuInWorld(%sku, %position, %orientation);
    }
    %this.schedule(0, "pasteString", getRecords(%toPaste, 1));
    return ;
}
function CSFurnitureMoverText::update(%this)
{
    %color = "<color:ffffff>";
    %linkcolor = "<linkcolor:e553ff>";
    %linkcolorhl = "<linkcolorhl:ff93f8>";
    %startTags = %color @ %linkcolor @ %linkcolorhl;
    %endTags = "";
    %nextPrevLinks = "<just:right><a:gamelink PREV><<</a>  <a:gamelink NEXT>>></a>";
    if ($CSSelectedID > 0)
    {
        %clipStartTag = "<clip:190>";
        %clipEndTag = "</clip>";
        %startTags = %startTags @ %clipStartTag;
        %endTags = %endTags @ %clipEndTag;
        %si = SkuManager.findBySku($CSSelectedSku);
        if ($CSboolPickedUp)
        {
            CSFurnitureMoverText.setText(%startTags @ %si.descShrt @ " (following me)" @ %endTags);
        }
        else
        {
            if (!$CSSelectedIsOwned)
            {
                CSFurnitureMoverText.setText(%startTags @ %si.descShrt @ " (test drive)" @ %endTags @ %nextPrevLinks);
            }
            else
            {
                %whichOne = 0;
                %howMany = numUsingFurnitureSku($CSSelectedSku);
                if (isObject($CSSelectedGhost) && !(($gCSGhostList $= "")))
                {
                    %numGhosts = getWordCount($gCSGhostList);
                    %i = 0;
                    while (%i < %numGhosts)
                    {
                        %ghost = getWord($gCSGhostList, %i);
                        if (!isObject(%ghost))
                        {
                            getNuggetGhostList("CSFurnitureMover::refreshGhostList");
                            %whichOne = 0;
                            break;
                        }
                        %sku = %ghost.getInventoryNuggetSKU();
                        if (%sku == $CSSelectedSku)
                        {
                            %whichOne = %whichOne + 1;
                        }
                        if (%ghost == $CSSelectedGhost)
                        {
                            continue;
                        }
                        %i = %i + 1;
                    }
                }
                if ((%whichOne == 1) && (%howMany == 1))
                {
                    CSFurnitureMoverText.setText(%startTags @ %si.descShrt @ %endTags @ %nextPrevLinks);
                }
                else
                {
                    if (%whichOne > 0)
                    {
                        CSFurnitureMoverText.setText(%startTags @ %si.descShrt @ " (" @ %whichOne @ "/" @ %howMany @ ")" @ %endTags @ %nextPrevLinks);
                    }
                    else
                    {
                        CSFurnitureMoverText.setText(%startTags @ %si.descShrt @ %endTags);
                        schedule(500, 0, "getNuggetGhostList", "CSFurnitureMover::refreshGhostList");
                    }
                }
            }
        }
    }
    else
    {
        CSFurnitureMoverText.setText(%startTags @ "Click on an item in the room to select it" @ %endTags);
    }
    return ;
}
function CSFurnitureMoverText::onURL(%this, %url)
{
    if (getWord(%url, 0) $= "gamelink")
    {
        %url = getWords(%url, 1);
    }
    %selectedObj = "";
    if (getWord(%url, 0) $= "NEXT")
    {
        %numGhosts = getWordCount($gCSGhostList);
        %i = 0;
        while (%i < %numGhosts)
        {
            %ghost = getWord($gCSGhostList, %i);
            if (%ghost.getInventoryNuggetID() == $CSSelectedGhost.getInventoryNuggetID())
            {
                %selectedObj = getWord($gCSGhostList, (%i + 1) % %numGhosts);
                break;
            }
            %i = %i + 1;
        }
    }
    else
    {
        if (getWord(%url, 0) $= "PREV")
        {
            %numGhosts = getWordCount($gCSGhostList);
            %i = 0;
            while (%i < %numGhosts)
            {
                %ghost = getWord($gCSGhostList, %i);
                if (%ghost.getInventoryNuggetID() == $CSSelectedGhost.getInventoryNuggetID())
                {
                    %selectedObj = getWord($gCSGhostList, ((%i - 1) + %numGhosts) % %numGhosts);
                    break;
                }
                %i = %i + 1;
            }
        }
    }
    if (isObject(%selectedObj))
    {
        CSFurnitureMover.SelectNuggetObject(%selectedObj);
    }
    return ;
}
function CSRotationResetText::onURL(%this, %url)
{
    if (getWord(%url, 0) $= "gamelink")
    {
        %url = getWords(%url, 1);
    }
    if (getWord(%url, 0) $= "RESET")
    {
        %ref = $CSSelectedID;
        if (%ref == -1)
        {
            return ;
        }
        commandToServer('SlotResetRotation', CustomSpaceClient::GetSpaceImIn(), %ref);
    }
    return ;
}
function csGetMoveClickSize()
{
    $UserPref::Spaces::Granularity = mClampF($UserPref::Spaces::Granularity, 0, 1);
    %min = 0.125;
    return %min * mPow(2, 6 * $UserPref::Spaces::Granularity);
}
function csGetRotateClickSize()
{
    $UserPref::Spaces::Granularity = mClampF($UserPref::Spaces::Granularity, 0, 1);
    %min = 1.40625;
    return %min * mPow(2, 6 * $UserPref::Spaces::Granularity);
}
$CSSelectedGhost = 0;
$CSSelectedID = -1;
$CSSelectedSku = -1;
$CSSelectedIsOwned = 0;
$CSSelectedFreeRotate = 0;
$CSboolPickedUp = 0;
function csTestRotateCW()
{
    %ref = $CSSelectedID;
    if (%ref == -1)
    {
        return ;
    }
    commandToServer('SlotRotate', CustomSpaceClient::GetSpaceImIn(), %ref, CSFurnitureMover.rotationAxis, (csGetRotateClickSize() * $pi) / 180);
    setIdle(0);
    return ;
}
function csTestRotateCCW()
{
    %ref = $CSSelectedID;
    if (%ref == -1)
    {
        return ;
    }
    commandToServer('SlotRotate', CustomSpaceClient::GetSpaceImIn(), %ref, CSFurnitureMover.rotationAxis, (-csGetRotateClickSize() * $pi) / 180);
    setIdle(0);
    return ;
}
function csTestMoveLeft()
{
    %ref = $CSSelectedID;
    if (%ref == -1)
    {
        return ;
    }
    commandToServer('SlotMoveLeft', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
    return ;
}
function csTestMoveRight()
{
    %ref = $CSSelectedID;
    if (%ref == -1)
    {
        return ;
    }
    commandToServer('SlotMoveRight', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
    return ;
}
function csTestMoveIn()
{
    %ref = $CSSelectedID;
    if (%ref == -1)
    {
        return ;
    }
    commandToServer('SlotMoveIn', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
    return ;
}
function csTestMoveOut()
{
    %ref = $CSSelectedID;
    if (%ref == -1)
    {
        return ;
    }
    commandToServer('SlotMoveOut', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
    return ;
}
function csTestRaise()
{
    %ref = $CSSelectedID;
    if (%ref == -1)
    {
        return ;
    }
    commandToServer('SlotRaise', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
    return ;
}
function csTestLower()
{
    %ref = $CSSelectedID;
    if (%ref == -1)
    {
        return ;
    }
    commandToServer('SlotLower', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
    return ;
}
function csTestTogglePickUp()
{
    %ref = $CSSelectedID;
    if (%ref == -1)
    {
        error("Attempting to pick up an item, but we have nothing selected currently.");
        return ;
    }
    $CSboolPickedUp = !$CSboolPickedUp;
    commandToServer('SlotPickUp', CustomSpaceClient::GetSpaceImIn(), %ref);
    CSFurnitureMoverText.update();
    CSFurnitureMover.updateButtonStates();
    setIdle(0);
    return ;
}
function csTestFreeSelectedItem()
{
    %ref = $CSSelectedID;
    if (%ref == -1)
    {
        return ;
    }
    if ($CSSelectedIsOwned)
    {
        putAwayAnotherFurnitureSku($CSSelectedSku);
        CSInventoryBrowser.update();
    }
    getNuggetGhostList("CSFurnitureMover::refreshGhostList");
    CSFurnitureMover.SelectNuggetID(-1);
    commandToServer('CSDeleteRefID', CustomSpaceClient::GetSpaceImIn(), %ref);
    CSFurnitureMover.updateButtonStates();
    setIdle(0);
    return ;
}
function customSpace::ConfirmFreeAllItems()
{
    %title = "Put Everything Away";
    %body = "\nThis will put <spush><b>everything<spop> into storage!\n\n<spush><b>THERE IS NO UNDO!<spop>\n\nAre you sure?";
    %cbOkay = "CustomSpace::FreeAllItems();";
    %cbCancel = "handleSystemMessage(\"msgInfoMessage\", \"Chicken!\");";
    MessageBoxOkCancel(%title, %body, %cbOkay, %cbCancel);
    return ;
}
function customSpace::FreeAllItems()
{
    CSFurnitureMover.SelectNuggetID(-1);
    commandToServer('CSDeleteAllInventory', CustomSpaceClient::GetSpaceImIn());
    getNuggetGhostList("CSFurnitureMover::refreshGhostList");
    putAwayAllFurniture();
    CSInventoryBrowser.update();
    CSFurnitureMover.updateButtonStates();
    setIdle(0);
    return ;
}
function CSFurnitureMover::InitForSpace(%this, %unused, %numberOfSlots)
{
    $CSMaximumSlots = %numberOfSlots;
    %this.SelectNuggetID(-1);
    %this.updateButtonStates();
    return ;
}
function FurnitureItemContextMenu::initWithObject(%this, %obj)
{
    if (!isDefined("%obj"))
    {
        %obj = "";
    }
    %this.clear();
    %this.obj = %obj;
    %grey = "255 255 255 128";
    %white = "255 255 255 255";
    %this.addScheme(1, %grey, %grey, %grey);
    %this.addScheme(2, %white, %white, %white);
    %schemeNormal = 0;
    %schemeDisabled = 1;
    if (isObject(%obj))
    {
        %sku = %obj.getInventoryNuggetSKU();
        %si = SkuManager.findBySku(%sku);
        %this.setText(%si.descShrt);
        if ($CSSelectedGhost == %obj)
        {
            %this.add("Deselect", %n = %n + 1, %schemeNormal);
        }
        else
        {
            %this.add("Select", %n = %n + 1, %schemeNormal);
        }
        if (($CSSelectedGhost == %obj) && $CSboolPickedUp)
        {
            %this.add("Drop", %n = %n + 1, %schemeNormal);
        }
        else
        {
            %this.add("Pick Up", %n = %n + 1, %schemeNormal);
        }
        if (%obj.getInventoryNuggetIsOwned())
        {
            %this.add("Put Away", %n = %n + 1, %schemeNormal);
            %this.add("Cut", %n = %n + 1, %schemeNormal);
            %this.add("Copy", %n = %n + 1, %schemeNormal);
            %schemeMoreAvailable = numOwnedFurnitureSku(%sku) > numUsingFurnitureSku(%sku) ? 0 : 1;
            %this.add("Place Another", %n = %n + 1, %schemeMoreAvailable);
            %schemeCanBuy = numOwnedFurnitureSku(%sku) != -1 ? 0 : 1;
            %this.add("Buy Another", %n = %n + 1, %schemeCanBuy);
            %schemeReset = %obj.getActiveSku() > 0 ? 0 : 1;
            %this.add("Reset Material", %n = %n + 1, %schemeReset);
        }
        else
        {
            %this.add("Buy Now", %n = %n + 1, %schemeNormal);
            %this.add("Done Test Driving", %n = %n + 1, %schemeNormal);
        }
    }
    else
    {
        %schemeCanPaste = CSFurnitureMover.canPaste() ? %schemeNormal : %schemeDisabled;
        %this.add("Paste", %n = %n + 1, %schemeCanPaste);
    }
    return ;
}
function FurnitureItemContextMenu::onSelect(%this, %id, %text)
{
    if (isObject(%this.obj))
    {
        if (%text $= "Deselect")
        {
            CSFurnitureMover.SelectNuggetID(-1);
        }
        else
        {
            if (%text $= "Select")
            {
                CSFurnitureMover.SelectNuggetObject(%this.obj);
            }
            else
            {
                if (%text $= "Drop")
                {
                    csTestTogglePickUp();
                }
                else
                {
                    if (%text $= "Pick Up")
                    {
                        CSFurnitureMover.SelectNuggetObject(%this.obj);
                        csTestTogglePickUp();
                    }
                    else
                    {
                        if (%text $= "Put Away")
                        {
                            CSFurnitureMover.SelectNuggetObject(%this.obj);
                            csTestFreeSelectedItem();
                        }
                        else
                        {
                            if (%text $= "Cut")
                            {
                                CSFurnitureMover.doCut(%this.obj);
                            }
                            else
                            {
                                if (%text $= "Copy")
                                {
                                    CSFurnitureMover.doCopy(%this.obj);
                                }
                                else
                                {
                                    if (%text $= "Place Another")
                                    {
                                        CustomSpaceClient::placeSkuInWorld(%this.obj.getInventoryNuggetSKU());
                                    }
                                    else
                                    {
                                        if (%text $= "Buy Another")
                                        {
                                            CSShoppingBrowser.purchaseSkus(%this.obj.getInventoryNuggetSKU());
                                        }
                                        else
                                        {
                                            if (%text $= "Buy Now")
                                            {
                                                CSShoppingBrowser.purchaseSkus(%this.obj.getInventoryNuggetSKU());
                                            }
                                            else
                                            {
                                                if (%text $= "Done Test Driving")
                                                {
                                                    CSFurnitureMover.SelectNuggetObject(%this.obj);
                                                    csTestFreeSelectedItem();
                                                }
                                                else
                                                {
                                                    if (%text $= "Reset Material")
                                                    {
                                                        objSkusFixSku(%this.obj, 0);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    else
    {
        if (%text $= "Paste")
        {
            CSFurnitureMover.doPaste();
        }
    }
    return ;
}
