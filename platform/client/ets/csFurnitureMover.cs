$gCSFurnitureMoverClipboard = "";
function CSFurnitureMover::toggle(%this) {
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
$CSMaximumSlots = 0;
function CSFurnitureMover::open(%this) {
    %wasOpen = %this.isVisible();
    closeCSPanelsInOtherCategories(%this);
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    %this.updateButtonStates();
    %this.updateClickText();
    if (!($CSSelectedFreeRotate)) {
    }
    if ((%this.rotationAxis $= "")) {
    }
    if (!(%wasOpen)) {
        %prevState = CSRotateZButton.isActive();
        if (!(%prevState)) {
            1.setActive(CSRotateZButton);
        }
        CSRotateZButton.performClick();
        %prevState.setActive(CSRotateZButton);
    }
    WindowManager.update();
    CustomSpaceClient::checkEditingSpace();
    csFurnitureMap.push();
    1.setActivityActive(getUserActivityMgr(), "decorating");
};
function CSFurnitureMover::close(%this) {
    %wasOpen = %this.isVisible();
    -(1.0).SelectNuggetID(%this);
    0.setVisible(%this);
    CustomSpaceClient::checkEditingSpace();
    PlayGui.focusTopWindow();
    WindowManager.update();
    if (%wasOpen) {
        csFurnitureMap.pop();
    }
    0.setActivityActive(getUserActivityMgr(), "decorating");
    return 1;
};
function CSFurnitureMover::isInEditMode(%this) {
    return %this.isVisible();
};
function CSFurnitureMover::updateButtonStates(%this) {
    %itemSelected = ($CSSelectedID != -(1.0));
    %itemSelected.setActive(customSpaceRaiseButton);
    %itemSelected.setActive(customSpaceLowerButton);
    %itemSelected.setActive(customSpaceRotateCWButton);
    %itemSelected.setActive(customSpaceRotateCCWButton);
    %itemSelected.setActive(customSpacePickupToggleButton);
    %itemSelected.setActive(customSpaceMoveLeftButton);
    %itemSelected.setActive(customSpaceMoveRightButton);
    %itemSelected.setActive(customSpaceMoveInButton);
    %itemSelected.setActive(customSpaceMoveOutButton);
    if (%itemSelected) {
    }
    $CSSelectedFreeRotate.setActive(CSRotateXButton);
    if (%itemSelected) {
    }
    $CSSelectedFreeRotate.setActive(CSRotateYButton);
    %itemSelected.setActive(CSRotateZButton);
    (numUsingFurnitureAll() > 0.0).setActive(customSpacePutAllAwayButton);
    if ($CSboolPickedUp) {
        "Drop".setText(customSpacePickupToggleButton);
    }
    "Pick Up".setText(customSpacePickupToggleButton);
};
function CSFurnitureMover::updateClickText(%this) {
    %prefix = "<color:cccccc>Click = ";
    %moveBy = mRoundTo(csGetMoveClickSize(), 0.001);
    %moveUnit = (%moveBy == 1.0) ? " foot" : " feet";
    %prefix @ "<color:ffffff>" @ %moveBy @ %moveUnit.setText(CSMoveClickText);
    %rotateBy = mRoundTo(csGetRotateClickSize(), 0.01);
    %rotateUnit = (%rotateBy == 1.0) ? " degree" : " degrees";
    %prefix @ "<color:ffffff>" @ %rotateBy @ %rotateUnit.setText(CSRotateClickText);
};
function CSFurnitureMover::preSelectedNuggetChanged(%this) {
    if (($CSSelectedID != -(1.0))) {
        if ($CSboolPickedUp) {
            commandToServer('SlotPickUp', CustomSpaceClient::GetSpaceImIn(), $CSSelectedID);
            $CSboolPickedUp = 0;
        }
    }
    $CSboolPickedUp = 0;
};
function CSFurnitureMover::SelectNuggetObject(%this, %obj) {
    if (isObject($CSSelectedGhost)) {
        0.SetSelected($CSSelectedGhost);
        $CSSelectedGhost = 0;
    }
    if (!(isObject(%obj))) {
        if ((%obj != 0.0)) {
            getNuggetGhostList("CSFurnitureMover::refreshGhostList");
        }
        return;
    }
    %nuggetId = %obj.getInventoryNuggetID();
    if ((%nuggetId < 0.0)) {
        return;
    }
    $CSSelectedGhost = %obj;
    $CSSelectedIsOwned = %obj.getInventoryNuggetIsOwned();
    $CSSelectedSku = %obj.getInventoryNuggetSKU();
    $CSSelectedFreeRotate = %obj.getInventoryNuggetFreeRotate();
    1.SetSelected($CSSelectedGhost);
    %nuggetId.SelectNuggetID(%this);
    if ($CSSelectedIsOwned) {
        CSInventoryBrowserWindow.open();
        $CSSelectedSku.navigateToSku(CSInventoryBrowser);
    }
    if (!($CSInstaTestDrive)) {
        CSShoppingBrowserWindow.open();
        $CSSelectedSku.navigateToSku(CSShoppingBrowser);
    }
    if ($UserPref::Spaces::FaceSelected) {
    }
    if (!($CSInstaTestDrive)) {
        commandToServer('orientTowards', $CSSelectedGhost.getGhostID(ServerConnection), 400);
    }
};
function CSFurnitureMover::SelectNuggetID(%this, %id) {
    %lastSelectedID = $CSSelectedID;
    %wasSelectedOwned = $CSSelectedIsOwned;
    CSFurnitureMover.preSelectedNuggetChanged();
    $CSSelectedID = %id;
    CSFurnitureMoverText.update();
    %this.updateButtonStates();
    if ((%id >= 0.0)) {
        %this.open();
    }
    0.SelectNuggetObject(%this);
    $CSSelectedSku = -(1.0);
    $CSSelectedIsOwned = 0;
    $CSSelectedGhost = 0;
    $CSSelectedFreeRotate = 0;
    if (!(CSInventoryBrowserWindow.initialized)) {
        CSInventoryBrowserWindow.Initialize();
    }
    if (isObject(CSInventoryBrowser) && !($CSSelectedIsOwned)) {
    }
    if (!(%wasSelectedOwned) && (%lastSelectedID != -(1.0))) {
    }
    if (($CSSelectedSku != -(1.0))) {
        CSInventoryBrowser.update();
    }
};
$gCSGhostList = "";
function CSFurnitureMover::refreshGhostList(%ghostlist) {
    if (!(%ghostlist $= $gCSGhostList)) {
        $gCSGhostList = %ghostlist;
        if (!(isObject($CSSelectedGhost))) {
        }
        if (($CSSelectedGhost.getInventoryNuggetID() != $CSSelectedID)) {
        }
        if (($CSSelectedID > 0.0)) {
            %numGhosts = getWordCount($gCSGhostList);
            %i = 0;
            while ((%i < %numGhosts)) {
                %ghost = getWord($gCSGhostList, %i);
                if (isObject(%ghost)) {
                }
                if ((%ghost.getInventoryNuggetID() == $CSSelectedID)) {
                    %ghost.SelectNuggetObject(CSFurnitureMover);
                    if (%ghost.isClassAIPlayer()) {
                        rentabotClient_customizeBot(%ghost);
                    }
                }
                %i = (%i + 1.0);
            }
        }
        CSFurnitureMoverText.update();
    }
};
function CSFurnitureMover::doCopyOrCut(%this, %obj, %operation) {
    if (!(isDefined("%obj"))) {
        %obj = $CSSelectedGhost;
    }
    if (!(isObject(%obj))) {
        return;
    }
    commandToServer('CSGetTransformForID', CustomSpaceClient::GetSpaceImIn(), %obj.getInventoryNuggetID(), %obj, %operation);
};
function CSFurnitureMover::doCopy(%this, %obj) {
    if (!(isDefined("%obj"))) {
        %obj = $CSSelectedGhost;
    }
    "copy".doCopyOrCut(%this, %obj);
};
function CSFurnitureMover::doCut(%this, %obj) {
    if (!(isDefined("%obj"))) {
        %obj = $CSSelectedGhost;
    }
    "cut".doCopyOrCut(%this, %obj);
};
function clientCmdCSReturnTransformForID(%id, %position, %orientation, %obj, %operation) {
    if (!(isObject(%obj))) {
        return;
    }
    $gCSFurnitureMoverClipboard = %obj.getInventoryNuggetSKU() @ "\t" @ %position @ "\t" @ %orientation;
    if ((%operation $= "cut")) {
        %obj.SelectNuggetObject(CSFurnitureMover);
        csTestFreeSelectedItem();
    }
};
$gCSFurnitureClipboardFieldCount = 3;
function CSFurnitureMover::canPaste(%this) {
    %toPaste = $gCSFurnitureMoverClipboard;
    %record = getRecord(%toPaste, 0);
    if ((getFieldCount(%record) != $gCSFurnitureClipboardFieldCount)) {
        return 0;
    }
    %sku = getField(%record, 0);
    return (numOwnedFurnitureSku(%sku) > numUsingFurnitureSku(%sku));
};
function CSFurnitureMover::doPaste(%this) {
    $gCSFurnitureMoverClipboard.pasteString(%this);
};
function CSFurnitureMover::pasteString(%this, %toPaste) {
    if ((trim(%toPaste) $= "")) {
        return;
    }
    %record = getRecord(%toPaste, 0);
    if ((getFieldCount(%record) == $gCSFurnitureClipboardFieldCount)) {
        %sku = getField(%record, 0);
        %position = getField(%record, 1);
        %orientation = getField(%record, 2);
        CustomSpaceClient::placeSkuInWorld(%sku, %position, %orientation);
    }
    getRecords(%toPaste, 1).schedule(%this, 0, "pasteString");
};
function CSFurnitureMoverText::update(%this) {
    %color = "<color:ffffff>";
    %linkcolor = "<linkcolor:e553ff>";
    %linkcolorhl = "<linkcolorhl:ff93f8>";
    %startTags = %color @ %linkcolor @ %linkcolorhl;
    %endTags = "";
    %nextPrevLinks = "<just:right><a:gamelink PREV><<</a>  <a:gamelink NEXT>>></a>";
    if (($CSSelectedID > 0.0)) {
        %clipStartTag = "<clip:190>";
        %clipEndTag = "</clip>";
        %startTags = %startTags @ %clipStartTag;
        %endTags = %endTags @ %clipEndTag;
        %si = $CSSelectedSku.findBySku(SkuManager);
        if ($CSboolPickedUp) {
            %startTags @ %si.descShrt @ " (following me)" @ %endTags.setText(CSFurnitureMoverText);
        }
        if (!($CSSelectedIsOwned)) {
            %startTags @ %si.descShrt @ " (test drive)" @ %endTags @ %nextPrevLinks.setText(CSFurnitureMoverText);
        }
        %whichOne = 0;
        %howMany = numUsingFurnitureSku($CSSelectedSku);
        if (isObject($CSSelectedGhost)) {
        }
        if (!($gCSGhostList $= "")) {
            %numGhosts = getWordCount($gCSGhostList);
            %i = 0;
            while ((%i < %numGhosts)) {
                %ghost = getWord($gCSGhostList, %i);
                if (!(isObject(%ghost))) {
                    getNuggetGhostList("CSFurnitureMover::refreshGhostList");
                    %whichOne = 0;
                }
                %sku = %ghost.getInventoryNuggetSKU();
                if ((%sku == $CSSelectedSku)) {
                    %whichOne = (%whichOne + 1.0);
                }
                if ((%ghost == $CSSelectedGhost)) {
                }
                %i = (%i + 1.0);
            }
        }
        if ((%whichOne == 1.0)) {
        }
        if ((%howMany == 1.0)) {
            %startTags @ %si.descShrt @ %endTags @ %nextPrevLinks.setText(CSFurnitureMoverText);
        }
        if ((%whichOne > 0.0)) {
            %startTags @ %si.descShrt @ " (" @ %whichOne @ "/" @ %howMany @ ")" @ %endTags @ %nextPrevLinks.setText(CSFurnitureMoverText);
        }
        %startTags @ %si.descShrt @ %endTags.setText(CSFurnitureMoverText);
        schedule(500, 0, "getNuggetGhostList", "CSFurnitureMover::refreshGhostList");
    }
    %startTags @ "Click on an item in the room to select it" @ %endTags.setText(CSFurnitureMoverText);
};
function CSFurnitureMoverText::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    %selectedObj = "";
    if ((getWord(%url, 0) $= "NEXT")) {
        %numGhosts = getWordCount($gCSGhostList);
        %i = 0;
        while ((%i < %numGhosts)) {
            %ghost = getWord($gCSGhostList, %i);
            if ((%ghost.getInventoryNuggetID() == $CSSelectedGhost.getInventoryNuggetID())) {
                %selectedObj = getWord($gCSGhostList, ((%i + 1.0) % %numGhosts));
            }
            %i = (%i + 1.0);
        }
    }
    if (((%i < %numGhosts) @ " " @ getWord(%url, 0) $= "PREV")) {
        %numGhosts = getWordCount($gCSGhostList);
        %i = 0;
        while ((%i < %numGhosts)) {
            %ghost = getWord($gCSGhostList, %i);
            if ((%ghost.getInventoryNuggetID() == $CSSelectedGhost.getInventoryNuggetID())) {
                %selectedObj = getWord($gCSGhostList, (((%i - 1.0) + %numGhosts) % %numGhosts));
            }
            %i = (%i + 1.0);
        }
    }
    if (isObject(%selectedObj)) {
        %selectedObj.SelectNuggetObject(CSFurnitureMover);
    }
};
function CSRotationResetText::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    if ((getWord(%url, 0) $= "RESET")) {
        %ref = $CSSelectedID;
        if ((%ref == -(1.0))) {
            return;
        }
        commandToServer('SlotResetRotation', CustomSpaceClient::GetSpaceImIn(), %ref);
    }
};
function csGetMoveClickSize() {
    $UserPref::Spaces::Granularity = mClampF($UserPref::Spaces::Granularity, 0, 1);
    %min = 0.125;
    return (%min * mPow(2, (6.0 * $UserPref::Spaces::Granularity)));
};
function csGetRotateClickSize() {
    $UserPref::Spaces::Granularity = mClampF($UserPref::Spaces::Granularity, 0, 1);
    %min = 1.40625;
    return (%min * mPow(2, (6.0 * $UserPref::Spaces::Granularity)));
};
$CSSelectedGhost = 0;
$CSSelectedID = -(1.0);
$CSSelectedSku = -(1.0);
$CSSelectedIsOwned = 0;
$CSSelectedFreeRotate = 0;
$CSboolPickedUp = 0;
function csTestRotateCW() {
    %ref = $CSSelectedID;
    if ((%ref == -(1.0))) {
        return;
    }
    commandToServer('SlotRotate', CustomSpaceClient::GetSpaceImIn(), %ref, CSFurnitureMover.rotationAxis, ((csGetRotateClickSize() * $pi) / 180.0));
    setIdle(0);
};
function csTestRotateCCW() {
    %ref = $CSSelectedID;
    if ((%ref == -(1.0))) {
        return;
    }
    commandToServer('SlotRotate', CustomSpaceClient::GetSpaceImIn(), %ref, CSFurnitureMover.rotationAxis, ((-(csGetRotateClickSize()) * $pi) / 180.0));
    setIdle(0);
};
function csTestMoveLeft() {
    %ref = $CSSelectedID;
    if ((%ref == -(1.0))) {
        return;
    }
    commandToServer('SlotMoveLeft', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
};
function csTestMoveRight() {
    %ref = $CSSelectedID;
    if ((%ref == -(1.0))) {
        return;
    }
    commandToServer('SlotMoveRight', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
};
function csTestMoveIn() {
    %ref = $CSSelectedID;
    if ((%ref == -(1.0))) {
        return;
    }
    commandToServer('SlotMoveIn', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
};
function csTestMoveOut() {
    %ref = $CSSelectedID;
    if ((%ref == -(1.0))) {
        return;
    }
    commandToServer('SlotMoveOut', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
};
function csTestRaise() {
    %ref = $CSSelectedID;
    if ((%ref == -(1.0))) {
        return;
    }
    commandToServer('SlotRaise', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
};
function csTestLower() {
    %ref = $CSSelectedID;
    if ((%ref == -(1.0))) {
        return;
    }
    commandToServer('SlotLower', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
};
function csTestTogglePickUp() {
    %ref = $CSSelectedID;
    if ((%ref == -(1.0))) {
        error("Attempting to pick up an item, but we have nothing selected currently.");
        return;
    }
    $CSboolPickedUp = !($CSboolPickedUp);
    commandToServer('SlotPickUp', CustomSpaceClient::GetSpaceImIn(), %ref);
    CSFurnitureMoverText.update();
    CSFurnitureMover.updateButtonStates();
    setIdle(0);
};
function csTestFreeSelectedItem() {
    %ref = $CSSelectedID;
    if ((%ref == -(1.0))) {
        return;
    }
    if ($CSSelectedIsOwned) {
        putAwayAnotherFurnitureSku($CSSelectedSku);
        CSInventoryBrowser.update();
    }
    getNuggetGhostList("CSFurnitureMover::refreshGhostList");
    -(1.0).SelectNuggetID(CSFurnitureMover);
    commandToServer('CSDeleteRefID', CustomSpaceClient::GetSpaceImIn(), %ref);
    CSFurnitureMover.updateButtonStates();
    setIdle(0);
};
function customSpace::ConfirmFreeAllItems() {
    %title = "Put Everything Away";
    %body = "\nThis will put <spush><b>everything<spop> into storage!\n\n<spush><b>THERE IS NO UNDO!<spop>\n\nAre you sure?";
    %cbOkay = "CustomSpace::FreeAllItems();";
    %cbCancel = "handleSystemMessage(\"msgInfoMessage\", \"Chicken!\");";
    MessageBoxOkCancel(%title, %body, %cbOkay, %cbCancel);
};
function customSpace::FreeAllItems() {
    -(1.0).SelectNuggetID(CSFurnitureMover);
    commandToServer('CSDeleteAllInventory', CustomSpaceClient::GetSpaceImIn());
    getNuggetGhostList("CSFurnitureMover::refreshGhostList");
    putAwayAllFurniture();
    CSInventoryBrowser.update();
    CSFurnitureMover.updateButtonStates();
    setIdle(0);
};
function CSFurnitureMover::InitForSpace(%this, %unused, %numberOfSlots) {
    $CSMaximumSlots = %numberOfSlots;
    -(1.0).SelectNuggetID(%this);
    %this.updateButtonStates();
};
function FurnitureItemContextMenu::initWithObject(%this, %obj) {
    if (!(isDefined("%obj"))) {
        %obj = "";
    }
    %this.clear();
    %this.obj = %obj;
    %grey = "255 255 255 128";
    %white = "255 255 255 255";
    %grey.addScheme(%this, 1, %grey, %grey);
    %white.addScheme(%this, 2, %white, %white);
    %schemeNormal = 0;
    %schemeDisabled = 1;
    if (isObject(%obj)) {
        %sku = %obj.getInventoryNuggetSKU();
        %si = %sku.findBySku(SkuManager);
        %si.descShrt.setText(%this);
        if (($CSSelectedGhost == %obj)) {
            %schemeNormal.add(%this, "Deselect", %n = (%n + 1.0));
        }
        %schemeNormal.add(%this, "Select", %n = (%n + 1.0));
        if (($CSSelectedGhost == %obj)) {
        }
        if ($CSboolPickedUp) {
            %schemeNormal.add(%this, "Drop", %n = (%n + 1.0));
        }
        %schemeNormal.add(%this, "Pick Up", %n = (%n + 1.0));
        if (%obj.getInventoryNuggetIsOwned()) {
            %schemeNormal.add(%this, "Put Away", %n = (%n + 1.0));
            %schemeNormal.add(%this, "Cut", %n = (%n + 1.0));
            %schemeNormal.add(%this, "Copy", %n = (%n + 1.0));
            %schemeMoreAvailable = (numOwnedFurnitureSku(%sku) > numUsingFurnitureSku(%sku)) ? 0 : 1;
            %schemeMoreAvailable.add(%this, "Place Another", %n = (%n + 1.0));
            %schemeCanBuy = (numOwnedFurnitureSku(%sku) != -(1.0)) ? 0 : 1;
            %schemeCanBuy.add(%this, "Buy Another", %n = (%n + 1.0));
            %schemeReset = (%obj.getActiveSku() > 0.0) ? 0 : 1;
            %schemeReset.add(%this, "Reset Material", %n = (%n + 1.0));
        }
        %schemeNormal.add(%this, "Buy Now", %n = (%n + 1.0));
        %schemeNormal.add(%this, "Done Test Driving", %n = (%n + 1.0));
    }
    if (CSFurnitureMover.canPaste()) {
    }
    %schemeCanPaste = %schemeDisabled;
    %schemeNormal;
    %schemeCanPaste.add(%this, "Paste", %n = (%n + 1.0));
};
function FurnitureItemContextMenu::onSelect(%this, %id, %text) {
    if (isObject(%this.obj)) {
        if ((%text $= "Deselect")) {
            -(1.0).SelectNuggetID(CSFurnitureMover);
        }
        if ((%text $= "Select")) {
            %this.obj.SelectNuggetObject(CSFurnitureMover);
        }
        if ((%text $= "Drop")) {
            csTestTogglePickUp();
        }
        if ((%text $= "Pick Up")) {
            %this.obj.SelectNuggetObject(CSFurnitureMover);
            csTestTogglePickUp();
        }
        if ((%text $= "Put Away")) {
            %this.obj.SelectNuggetObject(CSFurnitureMover);
            csTestFreeSelectedItem();
        }
        if ((%text $= "Cut")) {
            %this.obj.doCut(CSFurnitureMover);
        }
        if ((%text $= "Copy")) {
            %this.obj.doCopy(CSFurnitureMover);
        }
        if ((%text $= "Place Another")) {
            CustomSpaceClient::placeSkuInWorld(%this.obj.getInventoryNuggetSKU());
        }
        if ((%text $= "Buy Another")) {
            %this.obj.getInventoryNuggetSKU().purchaseSkus(CSShoppingBrowser);
        }
        if ((%text $= "Buy Now")) {
            %this.obj.getInventoryNuggetSKU().purchaseSkus(CSShoppingBrowser);
        }
        if ((%text $= "Done Test Driving")) {
            %this.obj.SelectNuggetObject(CSFurnitureMover);
            csTestFreeSelectedItem();
        }
        if ((%text $= "Reset Material")) {
            objSkusFixSku(%this.obj, 0);
        }
    }
    if ((%text $= "Paste")) {
        CSFurnitureMover.doPaste();
    }
};
