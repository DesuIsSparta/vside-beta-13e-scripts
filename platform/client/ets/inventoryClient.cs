$gCurrentStoreName = "";
$gStoreNameStack = "";
$gVHDUserNameFilter = "";
$gVHDUserNoStock = 0;
$gInventoryFetchFakeDelay = 1000;
$ClientIsAuthoritativeForInventory = 0;
function clientCmdOnEnterStore(%storename) {
    log("inventory", "info", "Entering store" @ " " @ %storename);
    %idx = findWord($gStoreNameStack, %storename);
    while ((%idx != -(1.0))) {
        error("Entering same store twice:" @ " " @ %storename @ " " @ "(corrected)");
        $gStoreNameStack = removeWord($gStoreNameStack, %idx);
        %idx = findWord($gStoreNameStack, %storename);
    }
    $gStoreNameStack = %storename @ " " @ $gStoreNameStack;
    (%idx != -(1.0));
    $gCurrentStoreName = %storename;
    resetStorePosition();
    Inventory::fetchStoreInventory(%storename);
    1.setActivityActive(getUserActivityMgr(), "shoppingForClothes");
};
function clientCmdOnLeaveStore(%storename) {
    log("inventory", "info", "Leaving store: \"" @ %storename @ "\".");
    clientSideOnLeaveStore(%storename);
    if (($gCurrentStoreName $= "")) {
        storeButton.hideButton(ButtonBar);
    }
};
function clientSideOnLeaveStore(%storename) {
    0.setActivityActive(getUserActivityMgr(), "shoppingForClothes");
    if ((%storename $= "")) {
        $gStoreNameStack = "";
        $gCurrentStoreName = "";
        $StoreSkusLayer = "";
        return;
    }
    %idx = findWord($gStoreNameStack, %storename);
    if ((%idx != -(1.0))) {
        $gStoreNameStack = removeWord($gStoreNameStack, %idx);
    }
    $gCurrentStoreName = getWord($gStoreNameStack, 0);
    $StoreSkusLayer = "";
    resetStorePosition();
};
function clientCmdOnEnterVHDUserStore(%userName) {
    %spaceName = CustomSpaceClient::GetCurrentSpaceName();
    %seppos = strpos(%spaceName, ".");
    if ((%seppos > -(1.0))) {
        %seppos = (%seppos + 1.0);
        %len = (strlen(%spaceName) - %seppos);
        %userName = getSubStr(%spaceName, %seppos, %len);
    }
    %userName = "";
    $gVHDUserNameFilter = %userName;
    %storename = "vhd";
    %idx = findWord($gStoreNameStack, %storename);
    while ((%idx != -(1.0))) {
        error("Entering same store twice:" @ " " @ %storename @ " " @ "(corrected)");
        $gStoreNameStack = removeWord($gStoreNameStack, %idx);
        %idx = findWord($gStoreNameStack, %storename);
    }
    $gStoreNameStack = %storename @ " " @ $gStoreNameStack;
    (%idx != -(1.0));
    $gCurrentStoreName = %storename;
    resetStorePosition();
    Inventory::fetchVHDUserStoreInventory(%storename);
    1.setActivityActive(getUserActivityMgr(), "shoppingForClothes");
};
function clientCmdOnLeaveVHDUserStore(%storename) {
    log("inventory", "info", "Leaving store: \"" @ %storename @ "\".");
    clientSideOnLeaveVHDUserStore(%storename);
    if (($gCurrentStoreName $= "")) {
        storeButton.hideButton(ButtonBar);
    }
};
function clientSideOnLeaveVHDUserStore(%storename) {
    0.setActivityActive(getUserActivityMgr(), "shoppingForClothes");
    if ((%storename $= "")) {
        $gStoreNameStack = "";
        $gCurrentStoreName = "";
        $StoreSkusLayer = "";
        return;
    }
    %idx = findWord($gStoreNameStack, %storename);
    if ((%idx != -(1.0))) {
        $gStoreNameStack = removeWord($gStoreNameStack, %idx);
    }
    $gCurrentStoreName = getWord($gStoreNameStack, 0);
    $StoreSkusLayer = "";
    resetStorePosition();
    $gVHDUserNameFilter = "";
};
function Inventory::fetchVHDUserStoreInventory(%storename) {
    if ($StandAlone) {
        schedule($gInventoryFetchFakeDelay, 0, "fakeVHDUserStoreInventoryGotFetchResults", %storename);
        return;
    }
    sendRequest_GetStoreInventory($Player::Name, %storename, "OnGotDoneOrError_GetVHDUserStoreInventory");
};
function OnGotDoneOrError_GetVHDUserStoreInventory(%request) {
    %storename = %request.storeName;
    if (%request.checkSuccess()) {
        Inventory::clearStore(%storename);
        %storename[$gStoreStockRevision @ %storename] = "storeRevisionDate".getValue(%request);
    }
    if (("errorCode".getValue(%request) $= "storeNotFound")) {
        Inventory::clearStore(%storename);
        Inventory::onGotStoreInventory(%storename);
    }
    return;
    %num = "itemsCount".getValue(%request);
    %hasAuthoredInventory = 0;
    if ((strlen($gVHDUserNameFilter) > 0.0)) {
        %n = 0;
        while ((%n < %num)) {
            %prefix = "items" @ %n @ ".";
            %sku = %prefix @ "sku".getValue(%request);
            %si = %sku.findBySku(SkuManager);
            if ((stricmp(%si.author, $gVHDUserNameFilter) == 0.0)) {
                %hasAuthoredInventory = 1;
            }
            %n = (%n + 1.0);
        }
        $gVHDUserNoStock = !(%hasAuthoredInventory);
        (%n < %num);
    }
    %n = 0;
    while ((%n < %num)) {
        %prefix = "items" @ %n @ ".";
        %sku = %prefix @ "sku".getValue(%request);
        %authorMatch = 0;
        if (%hasAuthoredInventory) {
            %si = %sku.findBySku(SkuManager);
            %authorMatch = (stricmp(%si.author, $gVHDUserNameFilter) == 0.0) ? 1 : 0;
        }
        if (!(%hasAuthoredInventory)) {
        }
        if (%authorMatch) {
            %qty = %prefix @ "quantity".getValue(%request);
            %vpoints = %prefix @ "priceVPoints".getValue(%request);
            %vbux = %prefix @ "priceVBux".getValue(%request);
            Inventory::addItemToStore(%storename, %sku, %qty, %vpoints, %vbux);
        }
        %n = (%n + 1.0);
    }
    Inventory::sortStoreInventory(%storename);
    Inventory::onGotVHDUserStoreInventory(%storename);
    if (!((%n < %num) @ " " @ %request.shoppingCartSkus $= "")) {
    }
    if (isObject(StoreShoppingList)) {
        %request.shoppingCartSkus.addSkus(StoreShoppingList);
    }
};
function Inventory::onGotVHDUserStoreInventory(%storename) {
    %hasUserNameFilter = !($gVHDUserNameFilter $= "") ? 1 : 0;
    %storename[$gStoreStockLoaded @ %storename] = 1;
    if (ClosetGui.isVisible()) {
    }
    if ((ClosetTabs.getCurrentTab().name $= "SHOPS")) {
    }
    if (($gCurrentStoreName $= %storename)) {
        saveStorePosition();
        ClosetTabs::refreshStoreTab();
    }
    if (%hasUserNameFilter) {
    }
    if (!($gVHDUserNoStock)) {
        %storeLongName = $gVHDUserNameFilter @ "'s vHD Store";
        %storeDesc = %storeLongName @ " " @ "- press F5 or click \"Shop\" to start shopping!";
    }
    if ($gVHDUserNoStock) {
        %storeLongName = $gVHDUserNameFilter @ " doesn't have any vHD Designs.";
        %storeDesc = %storeLongName @ " " @ "- Instead press F5 or click \"Shop\" to start browsing the full range of clothing from vSide House of Design!";
    }
    %storeLongName = %storename[$gDestinationNames @ %storename];
    %storeDesc = %storename[$gDestinationDescsInWorld @ %storename];
    if ((%storeLongName $= "")) {
        error(getScopeName() @ " " @ "- no store long name for" @ " " @ %storename);
    }
    if ((%storeDesc $= "")) {
        error(getScopeName() @ " " @ "- no store desc for" @ " " @ %storename);
    }
    handleSystemMessage("msgInfoMessage", %storeDesc);
    if (!($gCurrentStoreName $= "")) {
        storeButton.showButton(ButtonBar);
        if (isObject(ClosetFilterField)) {
            "".setValue(ClosetFilterField);
        }
    }
};
function fakeVHDUserStoreInventoryGotFetchResults(%storename) {
    echo("Fake store fetch results for" @ " " @ %storename);
    Inventory::clearStore(%storename);
    %skus = %storename.getStoreSkus(SkuManager);
    %qtys = %storename.getStoreQtys(SkuManager);
    %skusNum = getWordCount(%skus);
    %qtysNum = getWordCount(%qtys);
    if ((%skusNum != getWordCount(%qtys))) {
        error(getScopeName() @ " " @ "- mismatched number of skus and quantities:" @ " " @ %skusNum @ " " @ %qtysNum);
    }
    %hasAuthoredInventory = 0;
    if (!($gVHDUserNameFilter $= "")) {
        %n = 0;
        while ((%n < %num)) {
            %sku = %prefix @ "sku".getValue(%request);
            %si = %sku.findBySku(SkuManager);
            if ((%si.author $= $gVHDUserNameFilter)) {
                %hasAuthoredInventory = 1;
            }
            %n = (%n + 1.0);
        }
        $gVHDUserNoStock = !(%hasAuthoredInventory);
        (%n < %num);
    }
    %n = 0;
    while ((%n < %skusNum)) {
        %sku = getWord(%skus, %n);
        if ((%n < %qtysNum)) {
            %qty = getWord(%qtys, %n);
        }
        %qty = -(1.0);
        %authorMatch = 0;
        if (%hasAuthoredInventory) {
            %si = %sku.findBySku(SkuManager);
            %authorMatch = (%si.author $= $gVHDUserNameFilter) ? 1 : 0;
        }
        if (!(%hasAuthoredInventory)) {
        }
        if (%authorMatch) {
            %vbux = %si.price;
            %vpoints = (%si.price * 2.0);
            Inventory::addItemToStore(%storename, %sku, %qty, %vpoints, %vbux);
        }
        %n = (%n + 1.0);
    }
    Inventory::onGotVHDUserStoreInventory(%storename);
};
function resetStorePosition() {
    $gStoreScrollPos = 0;
    $gStoreCurrentCategory = "";
};
function loadStorePosition() {
    %presentCategory = StoreCategoryPopup.getText();
    if ((%presentCategory $= "")) {
        0.SetSelected(StoreCategoryPopup);
    }
    if (!($gStoreCurrentCategory $= %presentCategory)) {
        %catIndex = $gStoreCurrentCategory.findText(StoreCategoryPopup);
        if ((%catIndex >= 0.0)) {
            %catIndex.SetSelected(StoreCategoryPopup);
        }
        $gStoreCurrentCategory = %presentCategory;
    }
    if (($gStoreScrollPos $= "")) {
        $gStoreScrollPos = 0;
    }
    $gStoreScrollPos.scrollTo("Shops".getTabWithName(ClosetTabs).itemsScroll, 0);
};
function saveStorePosition() {
    $gStoreScrollPos = (0.0 - getWord("Shops".getTabWithName(ClosetTabs).thumbnails.getPosition(), 1));
    $gStoreCurrentCategory = StoreCategoryPopup.getText();
};
function clientCmdUpdateInventorySkus(%invChangedSkus, %notify, %autoEquip) {
    log("inventory", "info", "clientCmdUpdateInventorySkus(): invChangedSkus (<skus-added> | <skus-removed>) =" @ %invChangedSkus);
    %invChangedSkus = strreplace(%invChangedSkus, "\t", " ");
    %invChangedSkus = strreplace(%invChangedSkus, "|", "\t");
    %skusAdded = trim(getField(%invChangedSkus, 0));
    %skusRemoved = trim(getField(%invChangedSkus, 1));
    log("inventory", "debug", getScopeName() @ " " @ "- skus   Added=" @ %skusAdded);
    log("inventory", "debug", getScopeName() @ " " @ "- skus Removed=" @ %skusRemoved);
    updateInventorySkus(%skusAdded, %skusRemoved, %notify, %autoEquip, "");
};
function updateInventorySkus(%skusAdded, %skusRemoved, %notify, %autoEquip, %srcName) {
    %entryTime = getRealTime();
    echoDebug(getScopeName() @ " " @ "skus added  :" @ " " @ %skusAdded);
    echoDebug(getScopeName() @ " " @ "skus removed:" @ " " @ %skusRemoved);
    %skusAdded.addInventorySKUs($player);
    %skusRemoved.removeInventorySKUs($player);
    if (isObject(ClosetItemPopup)) {
        $Player::inventory.update(ClosetItemPopup);
    }
    if (%notify && !(ClosetGui.isVisible())) {
    }
    if (!(ClosetTabs.getCurrentTab().name $= "Shops")) {
        notifyUserOfSkusGained(%skusAdded, %srcName, %autoEquip);
    }
    if (%autoEquip) {
        Inventory::equipOrWearSkus(%skusAdded);
    }
    if (!(CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if (CustomSpaceClient::isOwner()) {
        getOwnedFurniture();
    }
    %exitTime = getRealTime();
    error(getScopeName() @ " " @ "- time:" @ " " @ mSubS32(%exitTime, %entryTime));
};
function clientCmdGiftReceived(%skus, %srcName) {
    %skus.addInventorySKUs($player);
    notifyUserOfSkusGained(%skus, %srcName, 0);
    if (ClosetGui.isVisible()) {
        ClosetTabs.selectCurrentTab();
    }
};
function clientCmdInventoryExpiration(%skusAboutToExpire, %skusJustExpired) {
    log("inventory", "info", "clientCmdInventoryExpiration(): skusAboutToExpire (<sku>|<numItems>|<remaining>|<reason>SPC<next>) =\"" @ %skusAboutToExpire @ "\", skusJustExpired (<sku>|<numItems>|<reason>|<replacementSKU>SPC<next>) = \"" @ %skusJustExpired @ "\"");
    %skusAboutToExpire = strreplace(%skusAboutToExpire, " ", "\n");
    %skusAboutToExpire = strreplace(%skusAboutToExpire, "|", "\t");
    %skusJustExpired = strreplace(%skusJustExpired, " ", "\n");
    %skusJustExpired = strreplace(%skusJustExpired, "|", "\t");
    %aboutToExpireCount = getRecordCount(%skusAboutToExpire);
    %justExpiredCount = getRecordCount(%skusJustExpired);
    if ((%aboutToExpireCount == 0.0)) {
    }
    if ((%justExpiredCount == 0.0)) {
        error(getScopeName() @ "-> received empty update... this shouldn't happen.");
        return;
    }
    if ((%justExpiredCount > 0.0)) {
        // unhandled opcode 2149 at 0x00000C49
        %justExpiredCount = AudioProfile_JustExpired;
    }
    if ((%aboutToExpireCount > 0.0)) {
        // unhandled opcode 2149 at 0x00000C59
        %aboutToExpireCount = AudioProfile_ExpiringSoon;
    }
    schedule(500, 0, "alxPlay", %sound);
    %msg = "";
    %listOfSkusJustExpired = "";
    if (%justExpiredCount) {
        %activeSkus = $player.getActiveSKUs();
        %realExpiredCount = 0;
        %i = 0;
        while ((%i < %justExpiredCount)) {
            %itemStr = getRecord(%skusJustExpired, %i);
            %num = getField(%itemStr, 1);
            %realExpiredCount = (%realExpiredCount + %num);
            %i = (%i + 1.0);
        }
        if ((%realExpiredCount > 1.0)) {
            %msg = %realExpiredCount @ " of your items just expired (";
            (%i < %justExpiredCount);
        }
        %msg = "Your ";
        %listOfSkusJustExpired = "";
        %listOfUniqueSkusJustExpired = "";
        %i = 0;
        while ((%i < %justExpiredCount)) {
            if ((%i > 0.0)) {
                %msg = %msg @ ", ";
            }
            %itemStr = getRecord(%skusJustExpired, %i);
            %sku = getField(%itemStr, 0);
            %num = getField(%itemStr, 1);
            %reason = getField(%itemStr, 2);
            %replaceSKU = getField(%itemStr, 3);
            if ((%num $= "")) {
                %num = 1;
            }
            %si = %sku.findBySku(SkuManager);
            if (isObject(%si)) {
                %name = %si.descShrt;
            }
            error(getScopeName() @ "-> unknown sku just expired");
            %name = "(oops, bug)";
            if ((%num > 1.0)) {
                %msg = %msg @ %num;
            }
            %msg = %msg @ %name @ (%num > 1.0) ? "s" : "";
            removeExpiredSkuFromOutfits(%sku, %replaceSKU, 0);
            %activeSkus = removeAndReplaceSkuFromSkuList(%activeSkus, %sku, %replaceSKU);
            if ((%listOfUniqueSkusJustExpired $= "")) {
                %listOfUniqueSkusJustExpired = %sku;
            }
            %listOfUniqueSkusJustExpired = %listOfUniqueSkusJustExpired @ " " @ %sku;
            %k = 0;
            while ((%k < %num)) {
                %listOfSkusJustExpired = %listOfSkusJustExpired @ %sku @ " ";
                %k = (%k + 1.0);
            }
            %i = (%i + 1.0);
            (%k < %num);
        }
        notifyUserOfSkusExpired(%listOfUniqueSkusJustExpired, "");
        if ((%justExpiredCount > 0.0)) {
            outfits_persist();
        }
        commandToServer('SetActiveSkus', %activeSkus);
        if ((%justExpiredCount > 1.0)) {
            %msg = %msg @ ")";
            (%i < %justExpiredCount);
        }
        %msg = %msg @ " just expired";
        if (%aboutToExpireCount) {
            %msg = %msg @ ", AND ";
        }
        %msg = %msg @ ".";
    }
    if (%aboutToExpireCount) {
        %realAboutToExpireCount = 0;
        %i = 0;
        while ((%i < %aboutToExpireCount)) {
            %itemStr = getRecord(%skusAboutToExpire, %i);
            %num = getField(%itemStr, 1);
            if ((%num $= "")) {
                %num = 1;
            }
            %realAboutToExpireCount = (%realAboutToExpireCount + %num);
            %i = (%i + 1.0);
        }
        if ((%realAboutToExpireCount > 1.0)) {
            %msg = %msg @ %realAboutToExpireCount @ " of your items are about to expire! (";
            (%i < %aboutToExpireCount);
        }
        %msg = %msg @ (%justExpiredCount > 0.0) ? "y" : "Y" @ "our";
        %i = 0;
        while ((%i < %aboutToExpireCount)) {
            if ((%i > 0.0)) {
                %msg = %msg @ ", ";
            }
            %aboutToExpireSku = getRecord(%skusAboutToExpire, %i);
            %sku = getField(%aboutToExpireSku, 0);
            %num = getField(%aboutToExpireSku, 1);
            %remaining = getField(%aboutToExpireSku, 2);
            %reason = getField(%aboutToExpireSku, 3);
            %si = %sku.findBySku(SkuManager);
            if (isObject(%si)) {
                %name = %si.descShrt;
            }
            error(getScopeName() @ "-> unknown sku about to expire");
            %name = "(oops, bug)";
            %msg = %msg @ %num @ " " @ %name @ (%num > 1.0) ? "s" : "";
            if ((%realAboutToExpireCount == 1.0)) {
                %msg = %msg @ " expires";
            }
            %roundedRemaining = (mFloor(((%remaining + 30.0) / 60.0)) * 60.0);
            %msg = %msg @ " in " @ secondsToDaysHoursMinutesSeconds(%roundedRemaining);
            %i = (%i + 1.0);
        }
        if ((%realAboutToExpireCount > 1.0)) {
            %msg = %msg @ ").";
            (%i < %aboutToExpireCount);
        }
        %msg = %msg @ ".";
    }
    if (!(%msg $= "")) {
        handleSystemMessage('MsgInfoMessage', %msg);
    }
    if (!(%listOfSkusJustExpired $= "")) {
        log("inventory", "debug", "clientCmdInventoryExpiration(): removed from inventory: skusJustExpired=" @ %listOfSkusJustExpired);
        %listOfSkusJustExpired.removeInventorySKUs($player);
    }
};
function removeAndReplaceSkuFromSkuList(%skusOutfit, %sku, %replaceSKU) {
    if ((%sku $= "")) {
        error(getScopeName() @ " " @ "sku is null can't remove from skusoutfits:" @ " " @ %skusOutfit @ " " @ "or replace with" @ " " @ %replaceSKU);
        return %skusOutfit;
    }
    %idx = findWord(%skusOutfit, %sku);
    if ((0.0 >= )) {
        %changes = (%changes + 1.0);
        %skusOutfit = removeWord(%skusOutfit, %idx);
        if (%replaceSKU) {
        }
        if (!(%replaceSKU $= "") && (findWord(%skusOutfit, %replaceSKU) < 0.0)) {
            if ((%skusOutfit $= "")) {
                %skusOutfit = %replaceSKU;
            }
            %skusOutfit = %skusOutfit @ " " @ %replaceSKU;
        }
    }
    return %skusOutfit;
};
function removeExpiredSkuFromOutfits(%oldSku, %newSku, %notifyUser) {
    if (!(isObject($player))) {
        error(getScopeName() @ " " @ "$player is not valid, cannot remove expired sku and replace with new one" @ " " @ %sku @ " " @ %newSku);
        return 0;
    }
    %outfitNames = ;
    %i = 0;
    while ((%i < $gClosetNumOutfits)) {
        %name = getWord(%outfitNames, %i);
        %keyOutfit = %name;
        %keyBody = $player.getGender() @ "Body";
        %skusOutfit = %keyOutfit.get($gOutfits);
        %skusOutfit = removeAndReplaceSkuFromSkuList(%skusOutfit, %oldSku, %newSku);
        %STOCKOutfit = %keyOutfit[$gNewStockOutfits @ %keyOutfit];
        %n = (getWordCount(%STOCKOutfit) - 1.0);
        while ((%n >= 0.0)) {
            %sku = getWord(%STOCKOutfit, %n);
            %drawer = %sku.findBySku(SkuManager).drwrName;
            %optional = %drawer.isOptionalDrawer(SkuManager);
            if (%optional) {
                %STOCKOutfit = removeWord(%STOCKOutfit, %n);
            }
            %n = (%n - 1.0);
        }
        %skusOutfit = %skusOutfit.overlaySkus(SkuManager, %STOCKOutfit);
        (%n >= 0.0);
        %skusBody = %keyBody.get($gOutfits);
        %skusBody = removeAndReplaceSkuFromSkuList(%skusBody, %oldSku, %newSku);
        %STOCKBody = ;
        %n = (getWordCount(%STOCKBody) - 1.0);
        while ((%n >= 0.0)) {
            %sku = getWord(%STOCKBody, %n);
            %drawer = %sku.findBySku(SkuManager).drwrName;
            %optional = %drawer.isOptionalDrawer(SkuManager);
            if (%optional) {
                %STOCKBody = removeWord(%STOCKBody, %n);
            }
            %n = (%n - 1.0);
        }
        %skusBody = %skusBody.overlaySkus(SkuManager, %STOCKBody);
        (%n >= 0.0);
        %skusBody.put($gOutfits, %keyBody);
        %skusOutfit.put($gOutfits, %keyOutfit);
        %i = (%i + 1.0);
    }
    if (%notifyUser) {
        notifyUserOfSkusExpired(%oldSku, "");
    }
};
function notifyUserOfSkusGained(%skus, %srcName, %autoEquipped) {
    echo(getScopeName() @ " " @ %skus @ " " @ %srcName);
    if (isObject($player)) {
        %gender = $player.getGender();
    }
    error(getScopeName() @ " " @ "- ERROR NO Player OBJECT using forced gender");
    %gender = "f";
    %skus = %gender.filterSkusGender(SkuManager, %skus);
    %skus = 1.filterSkusVisible(SkuManager, %skus);
    if ((%skus $= "")) {
        echo(getScopeName() @ " " @ "- no visible skus." @ " " @ %skus @ " " @ getTrace());
        return;
    }
    %skusWearable = 1.filterSkusWearable(SkuManager, %skus);
    %skusUnwearable = 0.filterSkusWearable(SkuManager, %skus);
    %skusFurnishing = "furnishing".filterSkusType(SkuManager, %skus);
    %numWearable = getWordCount(%skusWearable);
    %numUnwearable = getWordCount(%skusUnwearable);
    %numFurnishing = getWordCount(%skusFurnishing);
    %numSkus = (%numWearable + %numUnwearable);
    %numNonFurnishing = (%numSkus - %numFurnishing);
    %listWearable = 0.getSkuShortDescriptions(SkuManager, %skusWearable, ", ", 1);
    %listUnwearable = 32.getSkuShortDescriptions(SkuManager, %skusUnwearable, ", ", 1);
    if ((%numWearable > 0.0)) {
        if ((%listWearable @ " " @ %listUnwearable $= "")) {
        }
        %list = "" @ " and" @ " " @ %listUnwearable;
    }
    %list = %listUnwearable;
    %wordNonFurnishingItThem = (%numNonFurnishing == 1.0) ? "it" : "them";
    %wordNonFurnishingItemItems = (%numNonFurnishing == 1.0) ? "item" : "items";
    %wordWearableItThem = (%numWearable == 1.0) ? "it" : "them";
    %wordWearableItemItems = (%numWearable == 1.0) ? "item" : "items";
    %wordFurnishingItThem = (%numFurnishing == 1.0) ? "it" : "them";
    if ((%numFurnishing > 0.0)) {
        %specialOrFurniture = (%numFurnishing != %numUnwearable) ? "special or furniture" : "furniture";
    }
    %specialOrFurniture = "special";
    %msgUnwearable = "";
    if ((%numNonFurnishing > 0.0)) {
        if ((%numWearable > 0.0)) {
            if ((%numUnwearable == 0.0)) {
            }
            if ((%numUnwearable == 1.0)) {
            }
            %msgUnwearable = "One is" @ %numUnwearable @ " " @ "are" @ " " @ %specialOrFurniture @ " and can't actually be worn)";
            "\n(";
        }
        %msgUnwearable = "\n(" @ (%numUnwearable == 1.0) ? "It's" : "They're" @ " " @ %specialOrFurniture @ " and can't actually be worn)";
        "";
    }
    %YoullFindStr = "";
    if (!(%autoEquipped)) {
        if ((%numNonFurnishing > 0.0)) {
            if ((%numFurnishing > 0.0)) {
            }
            %YoullFindStr = "your non-furnishing" @ " " @ %wordNonFurnishingItemItems @ %wordNonFurnishingItThem @ " " @ "in the closet (F5).";
            "You'll find" @ " ";
        }
        if ((%numFurnishing > 0.0)) {
            if ((%numNonFurnishing > 0.0)) {
            }
            if ((%numNonFurnishing > 0.0)) {
            }
            %YoullFindStr = "your new furniture" @ %wordFurnishingItThem @ " " @ "in your apartment using the Space->My Furnishings panel.";
            "" @ "\n" @ "And you" @ "You" @ " " @ "can place" @ " ";
        }
    }
    if ((%YoullFindStr @ " " @ %srcName $= $Player::Name)) {
        %srcName = "yourself";
    }
    if ((%srcName $= "")) {
    }
    %fromString = " from" @ " " @ %srcName;
    "";
    if ((%numSkus == 1.0)) {
    }
    %msg = "a new item" @ %numSkus @ " " @ "new items" @ %fromString @ "!";
    "You just got" @ " ";
    %msg = %msg @ "\n" @ "(" @ %list @ ")";
    %msg = %msg @ %msgUnwearable;
    %msg = %msg @ "\n" @ %YoullFindStr;
    if (!(%autoEquipped)) {
    }
    if ((%numWearable > 0.0)) {
        if ((%numFurnishing == 0.0)) {
            %msg = %msg @ "\n" @ "" @ "\n" @ "Would you like to wear " @ %wordWearableItThem @ " now?";
            if (SalonDoesListContainAnySalonRewardSKUs(%skus)) {
                echo(getScopeName() @ " " @ "found a salon sku in the list, so we are wearing it immediately without asking: " @ " " @ %skus);
                Inventory::equipOrWearSkus(%skus);
                return;
            }
        }
        %msg = %msg @ "\n" @ "" @ "\n" @ "Would you like to put on your wearable " @ %wordWearableItemItems @ " now?";
        %mb = MessageBoxYesNo("Score!", %msg, "Inventory::equipOrWearSkus(\"" @ %skus @ "\"); if($gAutoOrbitOnReceiveItem && !$IN_ORBIT_CAM) nextPlayerCamMode();", "");
    }
    %mb = MessageBoxOK("Score!", %msg, "");
    %mb.message.setText(%mb.text);
};
function notifyUserOfSkusExpired(%skus, %srcName) {
    %skus = trim(%skus);
    if ((%skus $= "")) {
        return;
    }
    %count = getWordCount(%skus);
    if ((%count == 0.0)) {
        return;
    }
    %descriptions = 0.getSkuShortDescriptions(SkuManager, %skus, ", ");
    %msg = (%count == 1.0) ? "An item in your outfits expired while you were away:" : "Some items in your outfits expired while you were away:";
    handleSystemMessage("msgInfoMessage", %msg @ " " @ %descriptions);
};
function Inventory::equipOrWearSkus(%skus) {
    if ((%skus $= "")) {
        return;
    }
    %skusNew = $player.getGender().filterSkusGender(SkuManager, %skus);
    log("inventory", "debug", "Inventory::equipOrWearSkus(): %skus=" @ %skus @ ", %skusNew=" @ %skusNew);
    %skusDry = $player.getActiveSKUs();
    log("inventory", "debug", "Inventory::equipOrWearSkus(): %skusDry=" @ %skusDry);
    %skusWet = %skusNew.overlaySkus(SkuManager, %skusDry);
    log("inventory", "info", "Inventory::equipOrWearSkus(): %skusWet=" @ %skusWet);
    if (ClosetGui.isVisible()) {
        $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = %skusWet.filterSkusForClothing(SkuManager);
        $ClosetSkusBody = %skusWet.filterSkusForBody(SkuManager);
        ClosetTabs.selectCurrentTab();
        ClosetGui.updateVisibleAvatar();
    }
    SaveOutfitAndBodySkusAsCurrent(%skusWet);
};
function Inventory::fetchStoreInventory(%storename) {
    if ($StandAlone) {
        schedule($gInventoryFetchFakeDelay, 0, "fakeStoreInventoryGotFetchResults", %storename);
        return;
    }
    sendRequest_GetStoreInventory($Player::Name, %storename, "OnGotDoneOrError_GetStoreInventory");
};
function OnGotDoneOrError_GetStoreInventory(%request) {
    %storename = %request.storeName;
    if (%request.checkSuccess()) {
        Inventory::clearStore(%storename);
        %storename[$gStoreStockRevision @ %storename] = "storeRevisionDate".getValue(%request);
    }
    if (("errorCode".getValue(%request) $= "storeNotFound")) {
        Inventory::clearStore(%storename);
        Inventory::onGotStoreInventory(%storename);
    }
    return;
    %num = "itemsCount".getValue(%request);
    %n = 0;
    while ((%n < %num)) {
        %prefix = "items" @ %n @ ".";
        %sku = %prefix @ "sku".getValue(%request);
        %qty = %prefix @ "quantity".getValue(%request);
        %vpoints = %prefix @ "priceVPoints".getValue(%request);
        %vbux = %prefix @ "priceVBux".getValue(%request);
        Inventory::addItemToStore(%storename, %sku, %qty, %vpoints, %vbux);
        %n = (%n + 1.0);
    }
    Inventory::sortStoreInventory(%storename);
    Inventory::onGotStoreInventory(%storename);
    if (!((%n < %num) @ " " @ %request.shoppingCartSkus $= "")) {
    }
    if (isObject(StoreShoppingList)) {
        %request.shoppingCartSkus.addSkus(StoreShoppingList);
    }
};
function Inventory::sortStoreInventory(%storename) {
    warn(getScopeName() @ " " @ "- should be done server-side. ETS-3468");
    %allSkus = $UserPref::Player::gender.filterSkusGender(SkuManager, SkuManager.getSkus());
    %storename[$gStoreStockCacheSkus @ %storename] = Inventory::sortSkus(%storename[$gStoreStockCacheSkus @ %storename], %allSkus);
};
function Inventory::sortSkus(%skusToSort, %orderToAppearIn) {
    %ret = "";
    %n = (getWordCount(%orderToAppearIn) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%orderToAppearIn, %n);
        if ((findWord(%skusToSort, %sku) >= 0.0)) {
            %ret = %sku @ " " @ %ret;
        }
        %n = (%n - 1.0);
    }
    %ret = trim(%ret);
    (%n >= 0.0);
    return %ret;
};
function fakeStoreInventoryGotFetchResults(%storename) {
    echo("Fake store fetch results for" @ " " @ %storename);
    Inventory::clearStore(%storename);
    %skus = %storename.getStoreSkus(SkuManager);
    %qtys = %storename.getStoreQtys(SkuManager);
    %skusNum = getWordCount(%skus);
    %qtysNum = getWordCount(%qtys);
    if ((%skusNum != getWordCount(%qtys))) {
        error(getScopeName() @ " " @ "- mismatched number of skus and quantities:" @ " " @ %skusNum @ " " @ %qtysNum);
    }
    %n = 0;
    while ((%n < %skusNum)) {
        %sku = getWord(%skus, %n);
        if ((%n < %qtysNum)) {
            %qty = getWord(%qtys, %n);
        }
        %qty = -(1.0);
        %si = %sku.findBySku(SkuManager);
        %vbux = %si.price;
        %vpoints = (%si.price * 2.0);
        Inventory::addItemToStore(%storename, %sku, %qty, %vpoints, %vbux);
        %n = (%n + 1.0);
    }
    Inventory::onGotStoreInventory(%storename);
};
function Inventory::addItemToStore(%storename, %sku, %qty, %vpoints, %vbux) {
    %storename[$gStoreStockCacheSkus @ %storename] = %storename[$gStoreStockCacheSkus @ %storename] @ %sku @ " ";
    %sku[$gStoreItemsQty @ %sku] = %qty;
    if ((%vpoints < 0.0)) {
    }
    %sku[$gStoreItemsVPoints @ %sku] = "-" @ mFloor(%vpoints);
    if ((%vbux < 0.0)) {
    }
    %sku[$gStoreItemsVBux @ %sku] = "-" @ mFloor(%vbux);
};
function Inventory::getVPointsPriceForSku(%sku) {
    %price = %sku[$gStoreItemsVPoints @ %sku];
    if (!(%price $= "")) {
        return %price;
    }
    %si = %sku.findBySku(SkuManager);
    if (isObject(%si)) {
    }
    if (!(%si.priceVPoints $= "")) {
        if ((%si.priceVPoints == -(1.0))) {
        }
        return %si.priceVPoints;
    }
    return "-";
};
function Inventory::getVBuxPriceForSku(%sku) {
    %price = %sku[$gStoreItemsVBux @ %sku];
    if (!(%price $= "")) {
        return %price;
    }
    %si = %sku.findBySku(SkuManager);
    if (isObject(%si)) {
    }
    if (!(%si.priceVBux $= "")) {
        if ((%si.priceVBux == -(1.0))) {
        }
        return %si.priceVBux;
    }
    return "-";
};
function Inventory::getTotalPrice(%currency, %skus) {
    %hasValidSku = 0;
    %total = 0;
    %count = getWordCount(%skus);
    %i = 0;
    while ((%i < %count)) {
        %sku = getWord(%skus, %i);
        if ((%currency $= "vPoints")) {
        }
        %price = Inventory::getVBuxPriceForSku(%sku);
        Inventory::getVPointsPriceForSku(%sku);
        if (!(%price $= "-")) {
            %total = (%total + %price);
            %hasValidSku = 1;
        }
        %i = (%i + 1.0);
    }
    if (%hasValidSku) {
    }
    return "-";
};
function Inventory::filterSkusByValidPrice(%currency, %skus) {
    %validSkus = "";
    %count = getWordCount(%skus);
    %i = 0;
    while ((%i < %count)) {
        %sku = getWord(%skus, %i);
        if ((%currency $= "vPoints")) {
        }
        %price = Inventory::getVBuxPriceForSku(%sku);
        Inventory::getVPointsPriceForSku(%sku);
        if (!(%price $= "-")) {
            %validSkus = %validSkus @ " " @ %sku;
        }
        %i = (%i + 1.0);
    }
    return trim(%validSkus);
};
function Inventory::dumpStore(%storename) {
    echo("store" @ " " @ %storename);
    %num = getWordCount(%storename[$gStoreStockCacheSkus @ %storename]);
    %n = 0;
    while ((%n < %num)) {
        %sku = getWord(%storename[$gStoreStockCacheSkus @ %storename], %n);
        %qty = %sku[$gStoreItemsQty @ %sku];
        %vps = %sku[$gStoreItemsVPoints @ %sku];
        %vbs = %sku[$gStoreItemsVBux @ %sku];
        %si = %sku.findBySku(SkuManager);
        echo("sku:" @ " " @ formatInt("%6d", %sku) @ " " @ "qty" @ " " @ formatInt("%4d", %qty) @ " " @ "vPoints" @ " " @ formatInt("%6d", %vps) @ " " @ "vBux" @ " " @ formatInt("%6d", %vbs) @ " " @ %si.descShrt);
        %n = (%n + 1.0);
    }
};
function Inventory::onGotStoreInventory(%storename) {
    if ((%storename $= "furnishings")) {
        if (isObject(CSShoppingBrowser)) {
            CSShoppingBrowser.loadAvailableSkus();
        }
        return;
    }
    %storename[$gStoreStockLoaded @ %storename] = 1;
    if (ClosetGui.isVisible()) {
    }
    if ((ClosetTabs.getCurrentTab().name $= "SHOPS")) {
    }
    if (($gCurrentStoreName $= %storename)) {
        saveStorePosition();
        ClosetTabs::refreshStoreTab();
    }
    %storeLongName = %storename[$gDestinationNames @ %storename];
    %storeDesc = %storename[$gDestinationDescsInWorld @ %storename];
    if ((%storeLongName $= "")) {
        error(getScopeName() @ " " @ "- no store long name for" @ " " @ %storename);
    }
    if ((%storeDesc $= "")) {
        error(getScopeName() @ " " @ "- no store desc for" @ " " @ %storename);
    }
    handleSystemMessage("msgInfoMessage", %storeDesc);
    if (!($gCurrentStoreName $= "")) {
        storeButton.showButton(ButtonBar);
        if (isObject(ClosetFilterField)) {
            "".setValue(ClosetFilterField);
        }
    }
};
function Inventory::getCurrentStoreSkus() {
    if (($gCurrentStoreName $= "")) {
        return "no store";
    }
    return $gCurrentStoreName[$gStoreStockCacheSkus @ $gCurrentStoreName];
};
function Inventory::getStoreName(%storeID) {
    return %storeID[$gDestinationNames @ %storeID];
};
function Inventory::getCurrentStoreName() {
    if (($gCurrentStoreName $= "")) {
        return "";
    }
    return Inventory::getStoreName($gCurrentStoreName);
};
function Inventory::getStoreDescInCloset(%storeID) {
    return %storeID[$gDestinationDescsInCloset @ %storeID];
};
function Inventory::getStoreDescInWorld(%storeID) {
    return %storeID[$gDestinationDescsInWorld @ %storeID];
};
function Inventory::getCurrentStoreDescInCloset() {
    return Inventory::getStoreDescInCloset($gCurrentStoreName);
};
function Inventory::getCurrentStoreDescInWorld() {
    return Inventory::getStoreDescInWorld($gCurrentStoreName);
};
function Inventory::getStoreDrwrs(%storeID) {
    return Inventory::getStoreDrwrs($gCurrentStoreName);
};
function Inventory::getCurrentStoreDrwrs() {
    return Inventory::getStoreDrwrs($gCurrentStoreName);
};
function Inventory::fetchPlayerInventoryIfNeedTo(%playerObj) {
    echoDebug("inventory", "This should be more sophisticated. Something like if (!touched).");
    Inventory::fetchPlayerInventory(%playerObj);
};
function Inventory::fetchPlayerInventoryIfEmpty() {
    if (!(isDefined("$player::inventory"))) {
    }
    if (($Player::inventory $= "")) {
        Inventory::fetchPlayerInventory();
    }
};
function fakePlayerInventoryGotFetchResults() {
    echo("Fake player fetch inventory results");
    $Player::inventory = SkuManager.getBornWithSkus();
    $Player::inventory = trim($Player::inventory);
    %skus = "props".getSkusDrwr(SkuManager);
    $Player::inventory = $Player::inventory @ " " @ %skus;
    $Player::inventory = trim($Player::inventory);
    Inventory::onGotPlayerInventory();
};
function Inventory::onGotPlayerInventory() {
    echo("got player inventory");
    if (($Player::inventory $= "")) {
        error(getScopeName() @ "->got empty player inventory!");
        %notEmpty = 0;
    }
    %notEmpty = 1;
    Inventory::dedupeSkus($Player::inventory);
    if (($Player::inventory $= "")) {
    }
    if (%notEmpty) {
        error(getScopeName() @ "->got empty player inventory after deduping!");
    }
    if ($gClosetGuiNeedsOpen) {
        BodyItemsFrame.update();
    }
    if (!($Player::inventory $= "")) {
        Inventory::EnsurePlayerOwnsOutfitItems();
    }
};
function Inventory::EnsurePlayerOwnsOutfitItems() {
    %outfitNames = ;
    %anyRemoved = 0;
    %expiredSkus = "";
    %i = 0;
    while ((%i < $gClosetNumOutfits)) {
        %name = getWord(%outfitNames, %i);
        %keyOutfit = %name;
        %keyBody = $player.getGender() @ "Body";
        %skusOutfit = %keyOutfit.get($gOutfits);
        %skusBody = %keyBody.get($gOutfits);
        %entireOutfit = %skusBody @ " " @ %skusOutfit;
        %count = getWordCount(%entireOutfit);
        %j = (%count - 1.0);
        while ((%j >= 0.0)) {
            %sku = getWord(%entireOutfit, %j);
            if (!(%sku.hasInventorySKU($player))) {
                %anyRemoved = 1;
                %replaceSKU = "";
                removeExpiredSkuFromOutfits(%sku, %replaceSKU, 0);
                if ((findWord(%expiredSkus, %sku) < 0.0)) {
                    if ((%expiredSkus $= "")) {
                        %expiredSkus = %sku;
                    }
                    %expiredSkus = %expiredSkus @ " " @ %sku;
                }
            }
            %j = (%j - 1.0);
        }
        %i = (%i + 1.0);
        (%j >= 0.0);
    }
    notifyUserOfSkusExpired(%expiredSkus, "");
    if (%anyRemoved) {
        outfits_persist();
    }
};
function Inventory::dedupeSkus(%dry) {
    %dryNum = getWordCount(%dry);
    %wet = dedupeWords(%dry);
    %wetNum = getWordCount(%wet);
    if ((%dryNum != %wetNum)) {
        %xtra = %dry;
        %n = 0;
        while ((%n < %wetNum)) {
            %sku = getWord(%wet, %n);
            %xtra = findAndRemoveFirstOccurrenceOfWord(%xtra, %sku);
            %n = (%n + 1.0);
        }
        %count = (%dryNum - %wetNum);
        (%n < %wetNum);
        error(getScopeName() @ " " @ "- Got duplicate skus. Count: " @ " " @ %count @ " " @ "and here they are:" @ " " @ %xtra @ " " @ "dry:" @ " " @ %dry @ " " @ getTrace());
    }
    return %wet;
};
function Inventory::clearStore(%storename) {
    %storename[$gStoreStockCacheSkus @ %storename] = "";
    %storename[$gStoreStockRevision @ %storename] = "";
};
function Inventory::giftItemToPlayer(%unused, %unused) {
};
function Inventory::fetchPlayerInventory() {
    log("inventory", "info", getScopeName());
    if ($ClientIsAuthoritativeForInventory) {
    }
    if ($StandAlone) {
        schedule($gInventoryFetchFakeDelay, 0, "fakePlayerInventoryGotFetchResults");
        log("inventory", "info", "standalone - using fake player inventory fetch");
        return;
    }
    %request = safeNewScriptObject("ManagerRequest", "InventoryRequest");
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/GetUserInventory";
    %url.setURL(%request);
    $Player::Name.addUrlParam(%request, "user");
    $Token.addUrlParam(%request, "token");
    %request.doAnother = 0;
    %request.start();
    log("network", "debug", getScopeName() @ " " @ "-" @ " " @ %request.getURL());
};
function InventoryRequest::tryDoAnother(%this) {
    if (!(%this.doAnother)) {
        return;
    }
    log("network", "debug", getScopeName() @ " " @ "- doing another on" @ " " @ %this.getURL());
    Inventory::fetchPlayerInventory($player);
};
function InventoryRequest::onError(%this, %unused, %unused) {
    %this.tryDoAnother();
};
function InventoryRequest::onDone(%this) {
    if (!(%this.checkSuccess())) {
        error(getScopeName() @ "->InventoryRequest failed! Inventory = \"" @ $Player::inventory @ "\"");
        return;
    }
    %array = new Array("");;
    0;
    "qtyOwned".parse_Inventory(%this, %array);
    $Player::inventory = "";
    %num = %array.count();
    %n = 0;
    while ((%n < %num)) {
        %skunum = %n.getValue(%array).skuNumber;
        $Player::inventory = $Player::inventory @ " " @ %skunum;
        %n = (%n + 1.0);
    }
    $Player::inventory = trim($Player::inventory);
    (%n < %num);
    $Player::inventory = $Player::inventory @ " " @ 30013;
    $Player::inventory = $Player::inventory @ " " @ 30014;
    Inventory::onGotPlayerInventory();
    %array.delete();
    %this.tryDoAnother();
};
function Player::hasInventorySKU(%this, %sku) {
    return (findWord($Player::inventory, %sku) >= 0.0);
};
function Player::addInventorySKUs(%this, %skusToAdd) {
    echo("Player::addInventorySKUs skusToAdd=" @ %skusToAdd);
    %idx = (getWordCount(%skusToAdd) - 1.0);
    while ((%idx >= 0.0)) {
        %sku = getWord(%skusToAdd, %idx);
        if (!(hasWord($Player::inventory, %sku))) {
            $Player::inventory = %sku @ " " @ $Player::inventory;
        }
        %idx = (%idx - 1.0);
    }
};
function Player::removeInventorySKUs(%this, %skusToRemove) {
    %idx = (getWordCount(%skusToRemove) - 1.0);
    while ((%idx >= 0.0)) {
        %sku = getWord(%skusToRemove, %idx);
        %loc = findWord($Player::inventory, %sku);
        if ((%loc >= 0.0)) {
            $Player::inventory = removeWord($Player::inventory, %loc);
        }
        echo(getScopeName() @ " " @ "- tried to remove sku i don't have -" @ " " @ %sku @ " " @ getTrace());
        %idx = (%idx - 1.0);
    }
    %atLeastOneOutfitChanged = 0;
    (%idx >= 0.0);
    %activeSkus = $player.getActiveSKUs();
    %n = (getWordCount(%skusToRemove) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%skusToRemove, %n);
        %activeSkus = findAndRemoveFirstOccurrenceOfWord(%activeSkus, %sku);
        %m = ($gOutfits.size() - 1.0);
        while ((%m >= 0.0)) {
            %outfitSkus = %m.getValue($gOutfits);
            %outfitSkus = findAndRemoveAllOccurrencesOfWord(%outfitSkus, %sku);
            if (!(%outfitSkus $= %m.getValue($gOutfits))) {
                %atLeastOneOutfitChanged = 1;
                %outfitSkus.put($gOutfits, %m.getKey($gOutfits));
            }
            %m = (%m - 1.0);
        }
        %n = (%n - 1.0);
        (%m >= 0.0);
    }
    if (!((%n >= 0.0) @ " " @ $player.getActiveSKUs() $= %activeSkus)) {
        %activeSkus.setActiveSKUs($player);
        commandToServer('SetActiveSkus', %activeSkus);
    }
    if (%atLeastOneOutfitChanged) {
        outfits_persist();
    }
};
function doTakeFlower(%unused) {
    error("20080131 OBSOLETE FUNCTION - returning immediately. -" @ " " @ getTrace());
    return;
};
function doGiveFlower(%unused) {
    error("20080131 OBSOLETE FUNCTION - returning immediately. -" @ " " @ getTrace());
    return;
};
function sendGiftRequest(%unused, %skus) {
    error("20080131 OBSOLETE FUNCTION - returning immediately. -" @ " " @ getTrace());
    return;
};
