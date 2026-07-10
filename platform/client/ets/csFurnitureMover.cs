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
    %this.setVisible(1);
    %this.focusAndRaise();
    %this.updateButtonStates();
    %this.updateClickText();
    if (!($CSSelectedFreeRotate)) {
    }
    if ((PlayGui @ " " @ %this.rotationAxis $= "")) {
    }
    if (!(%wasOpen)) {
        %prevState = CSRotateZButton.isActive();
        if (!(%prevState)) {
            1.setActive();
        }
        CSRotateZButton.performClick();
        %prevState.setActive();
    }
    WindowManager.update();
    CustomSpaceClient::checkEditingSpace();
    csFurnitureMap.push();
    getUserActivityMgr().setActivityActive("decorating", 1);
};
function CSFurnitureMover::close(%this) {
    %wasOpen = %this.isVisible();
    %this.SelectNuggetID(-(1.0));
    %this.setVisible(0);
    CustomSpaceClient::checkEditingSpace();
    PlayGui.focusTopWindow();
    WindowManager.update();
    if (%wasOpen) {
        csFurnitureMap.pop();
    }
    getUserActivityMgr().setActivityActive("decorating", 0);
    return 1;
};
function CSFurnitureMover::isInEditMode(%this) {
    return %this.isVisible();
};
function CSFurnitureMover::updateButtonStates(%this) {
    %itemSelected = (-(1.0) != $CSSelectedID);
    %itemSelected.setActive();
    %itemSelected.setActive();
    %itemSelected.setActive();
    %itemSelected.setActive();
    %itemSelected.setActive();
    %itemSelected.setActive();
    %itemSelected.setActive();
    %itemSelected.setActive();
    %itemSelected.setActive();
    if (%itemSelected) {
    }
    $CSSelectedFreeRotate.setActive();
    if (%itemSelected) {
    }
    $CSSelectedFreeRotate.setActive();
    %itemSelected.setActive();
    (0.0 > numUsingFurnitureAll()).setActive();
    if ($CSboolPickedUp) {
        "Drop".setText();
    }
    "Pick Up".setText();
};
function CSFurnitureMover::updateClickText(%this) {
    %prefix = "<color:cccccc>Click = ";
    %moveBy = mRoundTo(csGetMoveClickSize(), 0.001);
    %moveUnit = (1.0 == %moveBy) ? " foot" : " feet";
    %prefix @ "<color:ffffff>" @ %moveBy @ %moveUnit.setText();
    %rotateBy = mRoundTo(csGetRotateClickSize(), 0.01);
    CSMoveClickText;
    %rotateUnit = (1.0 == %rotateBy) ? " degree" : " degrees";
    %prefix @ "<color:ffffff>" @ %rotateBy @ %rotateUnit.setText();
};
function CSFurnitureMover::preSelectedNuggetChanged(%this) {
    if ((-(1.0) != $CSSelectedID)) {
        if ($CSboolPickedUp) {
            commandToServer('SlotPickUp', CustomSpaceClient::GetSpaceImIn(), $CSSelectedID);
            $CSboolPickedUp = 0;
        }
    }
    $CSboolPickedUp = 0;
};
function CSFurnitureMover::SelectNuggetObject(%this, %obj) {
    if (isObject($CSSelectedGhost)) {
        $CSSelectedGhost.SetSelected(0);
        $CSSelectedGhost = 0;
    }
    if (!(isObject(%obj))) {
        if ((0.0 != %obj)) {
            getNuggetGhostList("CSFurnitureMover::refreshGhostList");
        }
        return;
    }
    %nuggetId = %obj.getInventoryNuggetID();
    if ((0.0 < %nuggetId)) {
        return;
    }
    $CSSelectedGhost = %obj;
    $CSSelectedIsOwned = %obj.getInventoryNuggetIsOwned();
    $CSSelectedSku = %obj.getInventoryNuggetSKU();
    $CSSelectedFreeRotate = %obj.getInventoryNuggetFreeRotate();
    $CSSelectedGhost.SetSelected(1);
    %this.SelectNuggetID(%nuggetId);
    if ($CSSelectedIsOwned) {
        CSInventoryBrowserWindow.open();
        $CSSelectedSku.navigateToSku();
    }
    if (!($CSInstaTestDrive)) {
        CSShoppingBrowserWindow.open();
        $CSSelectedSku.navigateToSku();
    }
    if ($UserPref::Spaces::FaceSelected) {
    }
    if (!($CSInstaTestDrive)) {
        commandToServer('orientTowards', $CSSelectedGhost.getGhostID(), 400);
    }
};
function CSFurnitureMover::SelectNuggetID(%this, %id) {
    %lastSelectedID = $CSSelectedID;
    %wasSelectedOwned = $CSSelectedIsOwned;
    CSFurnitureMover.preSelectedNuggetChanged();
    $CSSelectedID = %id;
    CSFurnitureMoverText.update();
    %this.updateButtonStates();
    if ((0.0 >= %id)) {
        %this.open();
    }
    %this.SelectNuggetObject(0);
    $CSSelectedSku = -(1.0);
    $CSSelectedIsOwned = 0;
    $CSSelectedGhost = 0;
    $CSSelectedFreeRotate = 0;
    if (!(%this.initialized)) {
        CSInventoryBrowserWindow.Initialize();
    }
    if (isObject(CSInventoryBrowser)) {
        if (!($CSSelectedIsOwned)) {
        }
    }
    if (!(%wasSelectedOwned)) {
        if ((-(1.0) != %lastSelectedID)) {
        }
    }
    if ((-(1.0) != $CSSelectedSku)) {
        CSInventoryBrowser.update();
    }
};
$gCSGhostList = "";
function CSFurnitureMover::refreshGhostList(%ghostlist) {
    if (!(%ghostlist $= $gCSGhostList)) {
        $gCSGhostList = %ghostlist;
        if (!(isObject($CSSelectedGhost))) {
        }
        if (($CSSelectedID != $CSSelectedGhost.getInventoryNuggetID())) {
        }
        if ((0.0 > $CSSelectedID)) {
            %numGhosts = getWordCount($gCSGhostList);
            %i = 0;
            if ((%numGhosts < %i)) {
                %ghost = getWord($gCSGhostList, %i);
                if (isObject(%ghost)) {
                }
                if (($CSSelectedID == %ghost.getInventoryNuggetID())) {
                    %ghost.SelectNuggetObject();
                    if (%ghost.isClassAIPlayer()) {
                        rentabotClient_customizeBot(%ghost);
                    }
                }
                %i = (1.0 + %i);
                CSFurnitureMover;
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
    %this.doCopyOrCut(%obj, "copy");
};
function CSFurnitureMover::doCut(%this, %obj) {
    if (!(isDefined("%obj"))) {
        %obj = $CSSelectedGhost;
    }
    %this.doCopyOrCut(%obj, "cut");
};
function clientCmdCSReturnTransformForID(%id, %position, %orientation, %obj, %operation) {
    if (!(isObject(%obj))) {
        return;
    }
    $gCSFurnitureMoverClipboard = %obj.getInventoryNuggetSKU() @ "\t" @ %position @ "\t" @ %orientation;
    if ((%operation $= "cut")) {
        %obj.SelectNuggetObject();
        csTestFreeSelectedItem();
    }
};
$gCSFurnitureClipboardFieldCount = 3;
function CSFurnitureMover::canPaste(%this) {
    %toPaste = $gCSFurnitureMoverClipboard;
    %record = getRecord(%toPaste, 0);
    if (($gCSFurnitureClipboardFieldCount != getFieldCount(%record))) {
        return 0;
    }
    %sku = getField(%record, 0);
    return (numUsingFurnitureSku(%sku) > numOwnedFurnitureSku(%sku));
};
function CSFurnitureMover::doPaste(%this) {
    %this.pasteString($gCSFurnitureMoverClipboard);
};
function CSFurnitureMover::pasteString(%this, %toPaste) {
    if ((trim(%toPaste) $= "")) {
        return;
    }
    %record = getRecord(%toPaste, 0);
    if (($gCSFurnitureClipboardFieldCount == getFieldCount(%record))) {
        %sku = getField(%record, 0);
        %position = getField(%record, 1);
        %orientation = getField(%record, 2);
        CustomSpaceClient::placeSkuInWorld(%sku, %position, %orientation);
    }
    %this.schedule(0, "pasteString", getRecords(%toPaste, 1));
};
function CSFurnitureMoverText::update(%this) {
    %color = "<color:ffffff>";
    %linkcolor = "<linkcolor:e553ff>";
    %linkcolorhl = "<linkcolorhl:ff93f8>";
    %startTags = %color @ %linkcolor @ %linkcolorhl;
    %endTags = "";
    %nextPrevLinks = "<just:right><a:gamelink PREV><<</a>  <a:gamelink NEXT>>></a>";
    if ((0.0 > $CSSelectedID)) {
        %clipStartTag = "<clip:190>";
        %clipEndTag = "</clip>";
        %startTags = %startTags @ %clipStartTag;
        %endTags = %endTags @ %clipEndTag;
        %si = $CSSelectedSku.findBySku();
        SkuManager;
        if ($CSboolPickedUp) {
            %startTags @ %si.descShrt @ " (following me)" @ %endTags.setText();
        }
        if (!($CSSelectedIsOwned)) {
            %startTags @ %si.descShrt @ " (test drive)" @ %endTags @ %nextPrevLinks.setText();
        }
        %whichOne = 0;
        CSFurnitureMoverText;
        %howMany = numUsingFurnitureSku($CSSelectedSku);
        CSFurnitureMoverText;
        if (isObject($CSSelectedGhost)) {
        }
        if (!($gCSGhostList $= "")) {
            %numGhosts = getWordCount($gCSGhostList);
            %i = 0;
            if ((%numGhosts < %i)) {
                %ghost = getWord($gCSGhostList, %i);
                if (!(isObject(%ghost))) {
                    getNuggetGhostList("CSFurnitureMover::refreshGhostList");
                    %whichOne = 0;
                }
                %sku = %ghost.getInventoryNuggetSKU();
                if (($CSSelectedSku == %sku)) {
                    %whichOne = (1.0 + %whichOne);
                }
                if (($CSSelectedGhost == %ghost)) {
                }
                %i = (1.0 + %i);
            }
        }
        if ((1.0 == %whichOne)) {
        }
        if ((1.0 == %howMany)) {
            %startTags @ %si.descShrt @ %endTags @ %nextPrevLinks.setText();
        }
        if ((0.0 > %whichOne)) {
            %startTags @ %si.descShrt @ " (" @ %whichOne @ "/" @ %howMany @ ")" @ %endTags @ %nextPrevLinks.setText();
        }
        %startTags @ %si.descShrt @ %endTags.setText();
        schedule(500, 0, "getNuggetGhostList", "CSFurnitureMover::refreshGhostList");
    }
    %startTags @ "Click on an item in the room to select it" @ %endTags.setText();
};
function CSFurnitureMoverText::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    %selectedObj = "";
    if ((getWord(%url, 0) $= "NEXT")) {
        %numGhosts = getWordCount($gCSGhostList);
        %i = 0;
        if ((%numGhosts < %i)) {
            %ghost = getWord($gCSGhostList, %i);
            if (($CSSelectedGhost.getInventoryNuggetID() == %ghost.getInventoryNuggetID())) {
                %selectedObj = getWord($gCSGhostList, (%numGhosts % (1.0 + %i)));
            }
            %i = (1.0 + %i);
        }
    }
    if (((%numGhosts < %i) @ " " @ getWord(%url, 0) $= "PREV")) {
        %numGhosts = getWordCount($gCSGhostList);
        %i = 0;
        if ((%numGhosts < %i)) {
            %ghost = getWord($gCSGhostList, %i);
            if (($CSSelectedGhost.getInventoryNuggetID() == %ghost.getInventoryNuggetID())) {
                %selectedObj = getWord($gCSGhostList, (%numGhosts % (%numGhosts + (1.0 - %i))));
            }
            %i = (1.0 + %i);
        }
    }
    if (isObject(%selectedObj)) {
        %selectedObj.SelectNuggetObject();
    }
};
function CSRotationResetText::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    if ((getWord(%url, 0) $= "RESET")) {
        %ref = $CSSelectedID;
        if ((-(1.0) == %ref)) {
            return;
        }
        commandToServer('SlotResetRotation', CustomSpaceClient::GetSpaceImIn(), %ref);
    }
};
function csGetMoveClickSize() {
    $UserPref::Spaces::Granularity = mClampF($UserPref::Spaces::Granularity, 0, 1);
    %min = 0.125;
    return (mPow(2, ($UserPref::Spaces::Granularity * 6.0)) * %min);
};
function csGetRotateClickSize() {
    $UserPref::Spaces::Granularity = mClampF($UserPref::Spaces::Granularity, 0, 1);
    %min = 1.40625;
    return (mPow(2, ($UserPref::Spaces::Granularity * 6.0)) * %min);
};
$CSSelectedGhost = 0;
$CSSelectedID = -(1.0);
$CSSelectedSku = -(1.0);
$CSSelectedIsOwned = 0;
$CSSelectedFreeRotate = 0;
$CSboolPickedUp = 0;
function csTestRotateCW() {
    %ref = $CSSelectedID;
    if ((-(1.0) == %ref)) {
        return;
    }
    commandToServer('SlotRotate', CustomSpaceClient::GetSpaceImIn(), %ref, %si.rotationAxis, (180.0 / ($pi * csGetRotateClickSize())));
    setIdle(0);
};
function csTestRotateCCW() {
    %ref = $CSSelectedID;
    if ((-(1.0) == %ref)) {
        return;
    }
    commandToServer('SlotRotate', CustomSpaceClient::GetSpaceImIn(), %ref, %si.rotationAxis, (180.0 / ($pi * -(csGetRotateClickSize()))));
    setIdle(0);
};
function csTestMoveLeft() {
    %ref = $CSSelectedID;
    if ((-(1.0) == %ref)) {
        return;
    }
    commandToServer('SlotMoveLeft', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
};
function csTestMoveRight() {
    %ref = $CSSelectedID;
    if ((-(1.0) == %ref)) {
        return;
    }
    commandToServer('SlotMoveRight', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
};
function csTestMoveIn() {
    %ref = $CSSelectedID;
    if ((-(1.0) == %ref)) {
        return;
    }
    commandToServer('SlotMoveIn', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
};
function csTestMoveOut() {
    %ref = $CSSelectedID;
    if ((-(1.0) == %ref)) {
        return;
    }
    commandToServer('SlotMoveOut', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
};
function csTestRaise() {
    %ref = $CSSelectedID;
    if ((-(1.0) == %ref)) {
        return;
    }
    commandToServer('SlotRaise', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
};
function csTestLower() {
    %ref = $CSSelectedID;
    if ((-(1.0) == %ref)) {
        return;
    }
    commandToServer('SlotLower', CustomSpaceClient::GetSpaceImIn(), %ref, csGetMoveClickSize());
    setIdle(0);
};
function csTestTogglePickUp() {
    %ref = $CSSelectedID;
    if ((-(1.0) == %ref)) {
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
    if ((-(1.0) == %ref)) {
        return;
    }
    if ($CSSelectedIsOwned) {
        putAwayAnotherFurnitureSku($CSSelectedSku);
        CSInventoryBrowser.update();
    }
    getNuggetGhostList("CSFurnitureMover::refreshGhostList");
    -(1.0).SelectNuggetID();
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
    -(1.0).SelectNuggetID();
    commandToServer('CSDeleteAllInventory', CustomSpaceClient::GetSpaceImIn());
    getNuggetGhostList("CSFurnitureMover::refreshGhostList");
    putAwayAllFurniture();
    CSInventoryBrowser.update();
    CSFurnitureMover.updateButtonStates();
    setIdle(0);
};
function CSFurnitureMover::InitForSpace(%this, %unused, %numberOfSlots) {
    $CSMaximumSlots = %numberOfSlots;
    %this.SelectNuggetID(-(1.0));
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
    %this.addScheme(1, %grey, %grey, %grey);
    %this.addScheme(2, %white, %white, %white);
    %schemeNormal = 0;
    %schemeDisabled = 1;
    if (isObject(%obj)) {
        %sku = %obj.getInventoryNuggetSKU();
        %si = %sku.findBySku();
        SkuManager;
        %this.setText(%si.descShrt);
        if ((%obj == $CSSelectedGhost)) {
            %n = (1.0 + %n);
            %this.add("Deselect", , %schemeNormal);
        }
        %n = (1.0 + %n);
        %this.add("Select", , %schemeNormal);
        if ((%obj == $CSSelectedGhost)) {
        }
        if ($CSboolPickedUp) {
            %n = (1.0 + %n);
            %this.add("Drop", , %schemeNormal);
        }
        %n = (1.0 + %n);
        %this.add("Pick Up", , %schemeNormal);
        if (%obj.getInventoryNuggetIsOwned()) {
            %n = (1.0 + %n);
            %this.add("Put Away", , %schemeNormal);
            %n = (1.0 + %n);
            %this.add("Cut", , %schemeNormal);
            %n = (1.0 + %n);
            %this.add("Copy", , %schemeNormal);
            %schemeMoreAvailable = (numUsingFurnitureSku(%sku) > numOwnedFurnitureSku(%sku)) ? 0 : 1;
            %n = (1.0 + %n);
            %this.add("Place Another", , %schemeMoreAvailable);
            %schemeCanBuy = (-(1.0) != numOwnedFurnitureSku(%sku)) ? 0 : 1;
            %n = (1.0 + %n);
            %this.add("Buy Another", , %schemeCanBuy);
            %schemeReset = (0.0 > %obj.getActiveSku()) ? 0 : 1;
            %n = (1.0 + %n);
            %this.add("Reset Material", , %schemeReset);
        }
        %n = (1.0 + %n);
        %this.add("Buy Now", , %schemeNormal);
        %n = (1.0 + %n);
        %this.add("Done Test Driving", , %schemeNormal);
    }
    if (CSFurnitureMover.canPaste()) {
    }
    %schemeCanPaste = %schemeDisabled;
    %schemeNormal;
    %n = (1.0 + %n);
    %this.add("Paste", , %schemeCanPaste);
};
function FurnitureItemContextMenu::onSelect(%this, %id, %text) {
    if (isObject(%this.obj)) {
        if ((%text $= "Deselect")) {
            -(1.0).SelectNuggetID();
        }
        if ((CSFurnitureMover @ " " @ %text $= "Select")) {
            %this.obj.SelectNuggetObject();
        }
        if ((CSFurnitureMover @ " " @ %text $= "Drop")) {
            csTestTogglePickUp();
        }
        if ((%text $= "Pick Up")) {
            %this.obj.SelectNuggetObject();
            csTestTogglePickUp();
        }
        if ((CSFurnitureMover @ " " @ %text $= "Put Away")) {
            %this.obj.SelectNuggetObject();
            csTestFreeSelectedItem();
        }
        if ((CSFurnitureMover @ " " @ %text $= "Cut")) {
            %this.obj.doCut();
        }
        if ((CSFurnitureMover @ " " @ %text $= "Copy")) {
            %this.obj.doCopy();
        }
        if ((CSFurnitureMover @ " " @ %text $= "Place Another")) {
            CustomSpaceClient::placeSkuInWorld(%this.obj.getInventoryNuggetSKU());
        }
        if ((%text $= "Buy Another")) {
            %this.obj.getInventoryNuggetSKU().purchaseSkus();
        }
        if ((CSShoppingBrowser @ " " @ %text $= "Buy Now")) {
            %this.obj.getInventoryNuggetSKU().purchaseSkus();
        }
        if ((CSShoppingBrowser @ " " @ %text $= "Done Test Driving")) {
            %this.obj.SelectNuggetObject();
            csTestFreeSelectedItem();
        }
        if ((CSFurnitureMover @ " " @ %text $= "Reset Material")) {
            objSkusFixSku(%this.obj, 0);
        }
    }
    if ((%text $= "Paste")) {
        CSFurnitureMover.doPaste();
    }
};
