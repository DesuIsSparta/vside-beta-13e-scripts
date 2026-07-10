$gCurrentStoreName = "";
$gStoreNameStack = "";
$gVHDUserNameFilter = "";
$gVHDUserNoStock = 0;
$gInventoryFetchFakeDelay = 1000;
$ClientIsAuthoritativeForInventory = 0;
function clientCmdOnEnterStore(%storename) {
    log("inventory", "info", "Entering store" @ " " @ %storename);
    %idx = findWord($gStoreNameStack, %storename);
    if ((-(1.0) != %idx)) {
        error("Entering same store twice:" @ " " @ %storename @ " " @ "(corrected)");
        $gStoreNameStack = removeWord($gStoreNameStack, %idx);
        %idx = findWord($gStoreNameStack, %storename);
    }
    $gStoreNameStack = %storename @ " " @ $gStoreNameStack;
    (-(1.0) != %idx);
    $gCurrentStoreName = %storename;
    resetStorePosition();
    Inventory::fetchStoreInventory(%storename);
    getUserActivityMgr().setActivityActive("shoppingForClothes", 1);
};
function clientCmdOnLeaveStore(%storename) {
    log("inventory", "info", "Leaving store: \"" @ %storename @ "\".");
    clientSideOnLeaveStore(%storename);
    if (($gCurrentStoreName $= "")) {
        ButtonBar.hideButton(storeButton);
    }
};
function clientSideOnLeaveStore(%storename) {
    getUserActivityMgr().setActivityActive("shoppingForClothes", 0);
    if ((%storename $= "")) {
        $gStoreNameStack = "";
        $gCurrentStoreName = "";
        $StoreSkusLayer = "";
        return;
    }
    %idx = findWord($gStoreNameStack, %storename);
    if ((-(1.0) != %idx)) {
        $gStoreNameStack = removeWord($gStoreNameStack, %idx);
    }
    $gCurrentStoreName = getWord($gStoreNameStack, 0);
    $StoreSkusLayer = "";
    resetStorePosition();
};
function clientCmdOnEnterVHDUserStore(%userName) {
    %spaceName = CustomSpaceClient::GetCurrentSpaceName();
    %seppos = strpos(%spaceName, ".");
    if ((-(1.0) > %seppos)) {
        %seppos = (1.0 + %seppos);
        %len = (%seppos - strlen(%spaceName));
        %userName = getSubStr(%spaceName, %seppos, %len);
    }
    %userName = "";
    $gVHDUserNameFilter = %userName;
    %storename = "vhd";
    %idx = findWord($gStoreNameStack, %storename);
    if ((-(1.0) != %idx)) {
        error("Entering same store twice:" @ " " @ %storename @ " " @ "(corrected)");
        $gStoreNameStack = removeWord($gStoreNameStack, %idx);
        %idx = findWord($gStoreNameStack, %storename);
    }
    $gStoreNameStack = %storename @ " " @ $gStoreNameStack;
    (-(1.0) != %idx);
    $gCurrentStoreName = %storename;
    resetStorePosition();
    Inventory::fetchVHDUserStoreInventory(%storename);
    getUserActivityMgr().setActivityActive("shoppingForClothes", 1);
};
function clientCmdOnLeaveVHDUserStore(%storename) {
    log("inventory", "info", "Leaving store: \"" @ %storename @ "\".");
    clientSideOnLeaveVHDUserStore(%storename);
    if (($gCurrentStoreName $= "")) {
        ButtonBar.hideButton(storeButton);
    }
};
function clientSideOnLeaveVHDUserStore(%storename) {
    getUserActivityMgr().setActivityActive("shoppingForClothes", 0);
    if ((%storename $= "")) {
        $gStoreNameStack = "";
        $gCurrentStoreName = "";
        $StoreSkusLayer = "";
        return;
    }
    %idx = findWord($gStoreNameStack, %storename);
    if ((-(1.0) != %idx)) {
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
        %storename[$gStoreStockRevision @ %storename] = %request.getValue("storeRevisionDate");
    }
    if ((%request.getValue("errorCode") $= "storeNotFound")) {
        Inventory::clearStore(%storename);
        Inventory::onGotStoreInventory(%storename);
    }
    return;
    %num = %request.getValue("itemsCount");
    %hasAuthoredInventory = 0;
    if ((0.0 > strlen($gVHDUserNameFilter))) {
        %n = 0;
        if ((%num < %n)) {
            %prefix = "items" @ %n @ ".";
            %sku = %request.getValue(%prefix @ "sku");
            %si = %sku.findBySku();
            SkuManager;
            if ((0.0 == stricmp(%si.author, $gVHDUserNameFilter))) {
                %hasAuthoredInventory = 1;
            }
            %n = (1.0 + %n);
        }
        $gVHDUserNoStock = !(%hasAuthoredInventory);
        (%num < %n);
    }
    %n = 0;
    if ((%num < %n)) {
        %prefix = "items" @ %n @ ".";
        %sku = %request.getValue(%prefix @ "sku");
        %authorMatch = 0;
        if (%hasAuthoredInventory) {
            %si = %sku.findBySku();
            SkuManager;
            %authorMatch = (0.0 == stricmp(%si.author, $gVHDUserNameFilter)) ? 1 : 0;
        }
        if (!(%hasAuthoredInventory)) {
        }
        if (%authorMatch) {
            %qty = %request.getValue(%prefix @ "quantity");
            %vpoints = %request.getValue(%prefix @ "priceVPoints");
            %vbux = %request.getValue(%prefix @ "priceVBux");
            Inventory::addItemToStore(%storename, %sku, %qty, %vpoints, %vbux);
        }
        %n = (1.0 + %n);
    }
    Inventory::sortStoreInventory(%storename);
    Inventory::onGotVHDUserStoreInventory(%storename);
    if (!((%num < %n) @ " " @ %request.shoppingCartSkus $= "")) {
    }
    if (isObject(StoreShoppingList)) {
        %request.shoppingCartSkus.addSkus();
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
        ButtonBar.showButton(storeButton);
        if (isObject(ClosetFilterField)) {
            "".setValue();
        }
    }
};
function fakeVHDUserStoreInventoryGotFetchResults(%storename) {
    echo("Fake store fetch results for" @ " " @ %storename);
    Inventory::clearStore(%storename);
    %skus = %storename.getStoreSkus();
    SkuManager;
    %qtys = %storename.getStoreQtys();
    SkuManager;
    %skusNum = getWordCount(%skus);
    %qtysNum = getWordCount(%qtys);
    if ((getWordCount(%qtys) != %skusNum)) {
        error(getScopeName() @ " " @ "- mismatched number of skus and quantities:" @ " " @ %skusNum @ " " @ %qtysNum);
    }
    %hasAuthoredInventory = 0;
    if (!($gVHDUserNameFilter $= "")) {
        %n = 0;
        if ((%num < %n)) {
            %sku = %request.getValue(%prefix @ "sku");
            %si = %sku.findBySku();
            SkuManager;
            if ((%si.author $= $gVHDUserNameFilter)) {
                %hasAuthoredInventory = 1;
            }
            %n = (1.0 + %n);
        }
        $gVHDUserNoStock = !(%hasAuthoredInventory);
        (%num < %n);
    }
    %n = 0;
    if ((%skusNum < %n)) {
        %sku = getWord(%skus, %n);
        if ((%qtysNum < %n)) {
            %qty = getWord(%qtys, %n);
        }
        %qty = -(1.0);
        %authorMatch = 0;
        if (%hasAuthoredInventory) {
            %si = %sku.findBySku();
            SkuManager;
            %authorMatch = (%si.author $= $gVHDUserNameFilter) ? 1 : 0;
        }
        if (!(%hasAuthoredInventory)) {
        }
        if (%authorMatch) {
            %vbux = %si.price;
            %vpoints = (2.0 * %si.price);
            Inventory::addItemToStore(%storename, %sku, %qty, %vpoints, %vbux);
        }
        %n = (1.0 + %n);
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
        0.SetSelected();
    }
    if (!(StoreCategoryPopup @ " " @ $gStoreCurrentCategory $= %presentCategory)) {
        %catIndex = $gStoreCurrentCategory.findText();
        StoreCategoryPopup;
        if ((0.0 >= %catIndex)) {
            %catIndex.SetSelected();
        }
        $gStoreCurrentCategory = %presentCategory;
        StoreCategoryPopup;
    }
    if (($gStoreScrollPos $= "")) {
        $gStoreScrollPos = 0;
    }
    "Shops".getTabWithName().itemsScroll.scrollTo(0, $gStoreScrollPos);
};
function saveStorePosition() {
    $gStoreScrollPos = (getWord("Shops".getTabWithName().thumbnails.getPosition(), 1) - 0.0);
    ClosetTabs;
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
    $player.addInventorySKUs(%skusAdded);
    $player.removeInventorySKUs(%skusRemoved);
    if (isObject(ClosetItemPopup)) {
        $Player::inventory.update();
    }
    if (%notify) {
        if (!(ClosetGui.isVisible())) {
        }
    }
    if (!(ClosetItemPopup @ " " @ ClosetTabs.getCurrentTab().name $= "Shops")) {
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
    $player.addInventorySKUs(%skus);
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
    if ((0.0 == %aboutToExpireCount)) {
    }
    if ((0.0 == %justExpiredCount)) {
        error(getScopeName() @ "-> received empty update... this shouldn't happen.");
        return;
    }
    if ((0.0 > %justExpiredCount)) {
        // unhandled opcode 2149 at 0x00000C49
        %justExpiredCount = AudioProfile_JustExpired;
    }
    if ((0.0 > %aboutToExpireCount)) {
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
        if ((%justExpiredCount < %i)) {
            %itemStr = getRecord(%skusJustExpired, %i);
            %num = getField(%itemStr, 1);
            %realExpiredCount = (%num + %realExpiredCount);
            %i = (1.0 + %i);
        }
        if ((1.0 > %realExpiredCount)) {
            %msg = %realExpiredCount @ " of your items just expired (";
            (%justExpiredCount < %i);
        }
        %msg = "Your ";
        %listOfSkusJustExpired = "";
        %listOfUniqueSkusJustExpired = "";
        %i = 0;
        if ((%justExpiredCount < %i)) {
            if ((0.0 > %i)) {
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
            %si = %sku.findBySku();
            SkuManager;
            if (isObject(%si)) {
                %name = %si.descShrt;
            }
            error(getScopeName() @ "-> unknown sku just expired");
            %name = "(oops, bug)";
            if ((1.0 > %num)) {
                %msg = %msg @ %num;
            }
            %msg = %msg @ %name @ (1.0 > %num) ? "s" : "";
            removeExpiredSkuFromOutfits(%sku, %replaceSKU, 0);
            %activeSkus = removeAndReplaceSkuFromSkuList(%activeSkus, %sku, %replaceSKU);
            if ((%listOfUniqueSkusJustExpired $= "")) {
                %listOfUniqueSkusJustExpired = %sku;
            }
            %listOfUniqueSkusJustExpired = %listOfUniqueSkusJustExpired @ " " @ %sku;
            %k = 0;
            if ((%num < %k)) {
                %listOfSkusJustExpired = %listOfSkusJustExpired @ %sku @ " ";
                %k = (1.0 + %k);
            }
            %i = (1.0 + %i);
            (%num < %k);
        }
        notifyUserOfSkusExpired(%listOfUniqueSkusJustExpired, "");
        if ((0.0 > %justExpiredCount)) {
            outfits_persist();
        }
        commandToServer('SetActiveSkus', %activeSkus);
        if ((1.0 > %justExpiredCount)) {
            %msg = %msg @ ")";
            (%justExpiredCount < %i);
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
        if ((%aboutToExpireCount < %i)) {
            %itemStr = getRecord(%skusAboutToExpire, %i);
            %num = getField(%itemStr, 1);
            if ((%num $= "")) {
                %num = 1;
            }
            %realAboutToExpireCount = (%num + %realAboutToExpireCount);
            %i = (1.0 + %i);
        }
        if ((1.0 > %realAboutToExpireCount)) {
            %msg = %msg @ %realAboutToExpireCount @ " of your items are about to expire! (";
            (%aboutToExpireCount < %i);
        }
        %msg = %msg @ (0.0 > %justExpiredCount) ? "y" : "Y" @ "our";
        %i = 0;
        if ((%aboutToExpireCount < %i)) {
            if ((0.0 > %i)) {
                %msg = %msg @ ", ";
            }
            %aboutToExpireSku = getRecord(%skusAboutToExpire, %i);
            %sku = getField(%aboutToExpireSku, 0);
            %num = getField(%aboutToExpireSku, 1);
            %remaining = getField(%aboutToExpireSku, 2);
            %reason = getField(%aboutToExpireSku, 3);
            %si = %sku.findBySku();
            SkuManager;
            if (isObject(%si)) {
                %name = %si.descShrt;
            }
            error(getScopeName() @ "-> unknown sku about to expire");
            %name = "(oops, bug)";
            %msg = %msg @ %num @ " " @ %name @ (1.0 > %num) ? "s" : "";
            if ((1.0 == %realAboutToExpireCount)) {
                %msg = %msg @ " expires";
            }
            %roundedRemaining = (60.0 * mFloor((60.0 / (30.0 + %remaining))));
            %msg = %msg @ " in " @ secondsToDaysHoursMinutesSeconds(%roundedRemaining);
            %i = (1.0 + %i);
        }
        if ((1.0 > %realAboutToExpireCount)) {
            %msg = %msg @ ").";
            (%aboutToExpireCount < %i);
        }
        %msg = %msg @ ".";
    }
    if (!(%msg $= "")) {
        handleSystemMessage('MsgInfoMessage', %msg);
    }
    if (!(%listOfSkusJustExpired $= "")) {
        log("inventory", "debug", "clientCmdInventoryExpiration(): removed from inventory: skusJustExpired=" @ %listOfSkusJustExpired);
        $player.removeInventorySKUs(%listOfSkusJustExpired);
    }
};
function removeAndReplaceSkuFromSkuList(%skusOutfit, %sku, %replaceSKU) {
    if ((%sku $= "")) {
        error(getScopeName() @ " " @ "sku is null can't remove from skusoutfits:" @ " " @ %skusOutfit @ " " @ "or replace with" @ " " @ %replaceSKU);
        return %skusOutfit;
    }
    %idx = findWord(%skusOutfit, %sku);
    if (( >= 0.0)) {
        %changes = (1.0 + %changes);
        %skusOutfit = removeWord(%skusOutfit, %idx);
        if (%replaceSKU) {
        }
        if (!(%replaceSKU $= "")) {
            if ((0.0 < findWord(%skusOutfit, %replaceSKU))) {
                if ((%skusOutfit $= "")) {
                    %skusOutfit = %replaceSKU;
                }
                %skusOutfit = %skusOutfit @ " " @ %replaceSKU;
            }
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
    if (($gClosetNumOutfits < %i)) {
        %name = getWord(%outfitNames, %i);
        %keyOutfit = %name;
        %keyBody = $player.getGender() @ "Body";
        %skusOutfit = $gOutfits.get(%keyOutfit);
        %skusOutfit = removeAndReplaceSkuFromSkuList(%skusOutfit, %oldSku, %newSku);
        %STOCKOutfit = %keyOutfit[$gNewStockOutfits @ %keyOutfit];
        %n = (1.0 - getWordCount(%STOCKOutfit));
        if ((0.0 >= %n)) {
            %sku = getWord(%STOCKOutfit, %n);
            %drawer = %sku.findBySku().drwrName;
            SkuManager;
            %optional = %drawer.isOptionalDrawer();
            SkuManager;
            if (%optional) {
                %STOCKOutfit = removeWord(%STOCKOutfit, %n);
            }
            %n = (1.0 - %n);
        }
        %skusOutfit = %STOCKOutfit.overlaySkus(%skusOutfit);
        SkuManager;
        %skusBody = $gOutfits.get(%keyBody);
        (0.0 >= %n);
        %skusBody = removeAndReplaceSkuFromSkuList(%skusBody, %oldSku, %newSku);
        %STOCKBody = ;
        %n = (1.0 - getWordCount(%STOCKBody));
        if ((0.0 >= %n)) {
            %sku = getWord(%STOCKBody, %n);
            %drawer = %sku.findBySku().drwrName;
            SkuManager;
            %optional = %drawer.isOptionalDrawer();
            SkuManager;
            if (%optional) {
                %STOCKBody = removeWord(%STOCKBody, %n);
            }
            %n = (1.0 - %n);
        }
        %skusBody = %STOCKBody.overlaySkus(%skusBody);
        SkuManager;
        $gOutfits.put(%keyBody, %skusBody);
        $gOutfits.put(%keyOutfit, %skusOutfit);
        %i = (1.0 + %i);
        (0.0 >= %n);
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
    %skus = %skus.filterSkusGender(%gender);
    SkuManager;
    %skus = %skus.filterSkusVisible(1);
    SkuManager;
    if ((%skus $= "")) {
        echo(getScopeName() @ " " @ "- no visible skus." @ " " @ %skus @ " " @ getTrace());
        return;
    }
    %skusWearable = %skus.filterSkusWearable(1);
    SkuManager;
    %skusUnwearable = %skus.filterSkusWearable(0);
    SkuManager;
    %skusFurnishing = %skus.filterSkusType("furnishing");
    SkuManager;
    %numWearable = getWordCount(%skusWearable);
    %numUnwearable = getWordCount(%skusUnwearable);
    %numFurnishing = getWordCount(%skusFurnishing);
    %numSkus = (%numUnwearable + %numWearable);
    %numNonFurnishing = (%numFurnishing - %numSkus);
    %listWearable = %skusWearable.getSkuShortDescriptions(", ", 1, 0);
    SkuManager;
    %listUnwearable = %skusUnwearable.getSkuShortDescriptions(", ", 1, 32);
    SkuManager;
    if ((0.0 > %numWearable)) {
        if ((%listWearable @ " " @ %listUnwearable $= "")) {
        }
        %list = "" @ " and" @ " " @ %listUnwearable;
    }
    %list = %listUnwearable;
    %wordNonFurnishingItThem = (1.0 == %numNonFurnishing) ? "it" : "them";
    %wordNonFurnishingItemItems = (1.0 == %numNonFurnishing) ? "item" : "items";
    %wordWearableItThem = (1.0 == %numWearable) ? "it" : "them";
    %wordWearableItemItems = (1.0 == %numWearable) ? "item" : "items";
    %wordFurnishingItThem = (1.0 == %numFurnishing) ? "it" : "them";
    if ((0.0 > %numFurnishing)) {
        %specialOrFurniture = (%numUnwearable != %numFurnishing) ? "special or furniture" : "furniture";
    }
    %specialOrFurniture = "special";
    %msgUnwearable = "";
    if ((0.0 > %numNonFurnishing)) {
        if ((0.0 > %numWearable)) {
            if ((0.0 == %numUnwearable)) {
            }
            if ((1.0 == %numUnwearable)) {
            }
            %msgUnwearable = "One is" @ %numUnwearable @ " " @ "are" @ " " @ %specialOrFurniture @ " and can't actually be worn)";
            "\n(";
        }
        %msgUnwearable = "\n(" @ (1.0 == %numUnwearable) ? "It's" : "They're" @ " " @ %specialOrFurniture @ " and can't actually be worn)";
        "";
    }
    %YoullFindStr = "";
    if (!(%autoEquipped)) {
        if ((0.0 > %numNonFurnishing)) {
            if ((0.0 > %numFurnishing)) {
            }
            %YoullFindStr = "your non-furnishing" @ " " @ %wordNonFurnishingItemItems @ %wordNonFurnishingItThem @ " " @ "in the closet (F5).";
            "You'll find" @ " ";
        }
        if ((0.0 > %numFurnishing)) {
            if ((0.0 > %numNonFurnishing)) {
            }
            if ((0.0 > %numNonFurnishing)) {
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
    if ((1.0 == %numSkus)) {
    }
    %msg = "a new item" @ %numSkus @ " " @ "new items" @ %fromString @ "!";
    "You just got" @ " ";
    %msg = %msg @ "\n" @ "(" @ %list @ ")";
    %msg = %msg @ %msgUnwearable;
    %msg = %msg @ "\n" @ %YoullFindStr;
    if (!(%autoEquipped)) {
    }
    if ((0.0 > %numWearable)) {
        if ((0.0 == %numFurnishing)) {
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
    %mb.text.setText(%mb.message);
};
function notifyUserOfSkusExpired(%skus, %srcName) {
    %skus = trim(%skus);
    if ((%skus $= "")) {
        return;
    }
    %count = getWordCount(%skus);
    if ((0.0 == %count)) {
        return;
    }
    %descriptions = %skus.getSkuShortDescriptions(", ", 0);
    SkuManager;
    %msg = (1.0 == %count) ? "An item in your outfits expired while you were away:" : "Some items in your outfits expired while you were away:";
    handleSystemMessage("msgInfoMessage", %msg @ " " @ %descriptions);
};
function Inventory::equipOrWearSkus(%skus) {
    if ((%skus $= "")) {
        return;
    }
    %skusNew = %skus.filterSkusGender($player.getGender());
    SkuManager;
    log("inventory", "debug", "Inventory::equipOrWearSkus(): %skus=" @ %skus @ ", %skusNew=" @ %skusNew);
    %skusDry = $player.getActiveSKUs();
    log("inventory", "debug", "Inventory::equipOrWearSkus(): %skusDry=" @ %skusDry);
    %skusWet = %skusDry.overlaySkus(%skusNew);
    SkuManager;
    log("inventory", "info", "Inventory::equipOrWearSkus(): %skusWet=" @ %skusWet);
    if (ClosetGui.isVisible()) {
        $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = SkuManager @ %skusWet.filterSkusForClothing();
        $ClosetSkusBody = %skusWet.filterSkusForBody();
        SkuManager;
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
        %storename[$gStoreStockRevision @ %storename] = %request.getValue("storeRevisionDate");
    }
    if ((%request.getValue("errorCode") $= "storeNotFound")) {
        Inventory::clearStore(%storename);
        Inventory::onGotStoreInventory(%storename);
    }
    return;
    %num = %request.getValue("itemsCount");
    %n = 0;
    if ((%num < %n)) {
        %prefix = "items" @ %n @ ".";
        %sku = %request.getValue(%prefix @ "sku");
        %qty = %request.getValue(%prefix @ "quantity");
        %vpoints = %request.getValue(%prefix @ "priceVPoints");
        %vbux = %request.getValue(%prefix @ "priceVBux");
        Inventory::addItemToStore(%storename, %sku, %qty, %vpoints, %vbux);
        %n = (1.0 + %n);
    }
    Inventory::sortStoreInventory(%storename);
    Inventory::onGotStoreInventory(%storename);
    if (!((%num < %n) @ " " @ %request.shoppingCartSkus $= "")) {
    }
    if (isObject(StoreShoppingList)) {
        %request.shoppingCartSkus.addSkus();
    }
};
function Inventory::sortStoreInventory(%storename) {
    warn(getScopeName() @ " " @ "- should be done server-side. ETS-3468");
    %allSkus = SkuManager.getSkus().filterSkusGender($UserPref::Player::gender);
    SkuManager;
    %storename[$gStoreStockCacheSkus @ %storename] = Inventory::sortSkus(%storename[$gStoreStockCacheSkus @ %storename], %allSkus);
};
function Inventory::sortSkus(%skusToSort, %orderToAppearIn) {
    %ret = "";
    %n = (1.0 - getWordCount(%orderToAppearIn));
    if ((0.0 >= %n)) {
        %sku = getWord(%orderToAppearIn, %n);
        if ((0.0 >= findWord(%skusToSort, %sku))) {
            %ret = %sku @ " " @ %ret;
        }
        %n = (1.0 - %n);
    }
    %ret = trim(%ret);
    (0.0 >= %n);
    return %ret;
};
function fakeStoreInventoryGotFetchResults(%storename) {
    echo("Fake store fetch results for" @ " " @ %storename);
    Inventory::clearStore(%storename);
    %skus = %storename.getStoreSkus();
    SkuManager;
    %qtys = %storename.getStoreQtys();
    SkuManager;
    %skusNum = getWordCount(%skus);
    %qtysNum = getWordCount(%qtys);
    if ((getWordCount(%qtys) != %skusNum)) {
        error(getScopeName() @ " " @ "- mismatched number of skus and quantities:" @ " " @ %skusNum @ " " @ %qtysNum);
    }
    %n = 0;
    if ((%skusNum < %n)) {
        %sku = getWord(%skus, %n);
        if ((%qtysNum < %n)) {
            %qty = getWord(%qtys, %n);
        }
        %qty = -(1.0);
        %si = %sku.findBySku();
        SkuManager;
        %vbux = %si.price;
        %vpoints = (2.0 * %si.price);
        Inventory::addItemToStore(%storename, %sku, %qty, %vpoints, %vbux);
        %n = (1.0 + %n);
    }
    Inventory::onGotStoreInventory(%storename);
};
function Inventory::addItemToStore(%storename, %sku, %qty, %vpoints, %vbux) {
    %storename[$gStoreStockCacheSkus @ %storename] = %storename[$gStoreStockCacheSkus @ %storename] @ %sku @ " ";
    %sku[$gStoreItemsQty @ %sku] = %qty;
    if ((0.0 < %vpoints)) {
    }
    %sku[$gStoreItemsVPoints @ %sku] = "-" @ mFloor(%vpoints);
    if ((0.0 < %vbux)) {
    }
    %sku[$gStoreItemsVBux @ %sku] = "-" @ mFloor(%vbux);
};
function Inventory::getVPointsPriceForSku(%sku) {
    %price = %sku[$gStoreItemsVPoints @ %sku];
    if (!(%price $= "")) {
        return %price;
    }
    %si = %sku.findBySku();
    SkuManager;
    if (isObject(%si)) {
    }
    if (!(%si.priceVPoints $= "")) {
        if ((-(1.0) == %si.priceVPoints)) {
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
    %si = %sku.findBySku();
    SkuManager;
    if (isObject(%si)) {
    }
    if (!(%si.priceVBux $= "")) {
        if ((-(1.0) == %si.priceVBux)) {
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
    if ((%count < %i)) {
        %sku = getWord(%skus, %i);
        if ((%currency $= "vPoints")) {
        }
        %price = Inventory::getVBuxPriceForSku(%sku);
        Inventory::getVPointsPriceForSku(%sku);
        if (!(%price $= "-")) {
            %total = (%price + %total);
            %hasValidSku = 1;
        }
        %i = (1.0 + %i);
    }
    if (%hasValidSku) {
    }
    return "-";
};
function Inventory::filterSkusByValidPrice(%currency, %skus) {
    %validSkus = "";
    %count = getWordCount(%skus);
    %i = 0;
    if ((%count < %i)) {
        %sku = getWord(%skus, %i);
        if ((%currency $= "vPoints")) {
        }
        %price = Inventory::getVBuxPriceForSku(%sku);
        Inventory::getVPointsPriceForSku(%sku);
        if (!(%price $= "-")) {
            %validSkus = %validSkus @ " " @ %sku;
        }
        %i = (1.0 + %i);
    }
    return trim(%validSkus);
};
function Inventory::dumpStore(%storename) {
    echo("store" @ " " @ %storename);
    %num = getWordCount(%storename[$gStoreStockCacheSkus @ %storename]);
    %n = 0;
    if ((%num < %n)) {
        %sku = getWord(%storename[$gStoreStockCacheSkus @ %storename], %n);
        %qty = %sku[$gStoreItemsQty @ %sku];
        %vps = %sku[$gStoreItemsVPoints @ %sku];
        %vbs = %sku[$gStoreItemsVBux @ %sku];
        %si = %sku.findBySku();
        SkuManager;
        echo("sku:" @ " " @ formatInt("%6d", %sku) @ " " @ "qty" @ " " @ formatInt("%4d", %qty) @ " " @ "vPoints" @ " " @ formatInt("%6d", %vps) @ " " @ "vBux" @ " " @ formatInt("%6d", %vbs) @ " " @ %si.descShrt);
        %n = (1.0 + %n);
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
        ButtonBar.showButton(storeButton);
        if (isObject(ClosetFilterField)) {
            "".setValue();
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
    %skus = "props".getSkusDrwr();
    SkuManager;
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
    if (($gClosetNumOutfits < %i)) {
        %name = getWord(%outfitNames, %i);
        %keyOutfit = %name;
        %keyBody = $player.getGender() @ "Body";
        %skusOutfit = $gOutfits.get(%keyOutfit);
        %skusBody = $gOutfits.get(%keyBody);
        %entireOutfit = %skusBody @ " " @ %skusOutfit;
        %count = getWordCount(%entireOutfit);
        %j = (1.0 - %count);
        if ((0.0 >= %j)) {
            %sku = getWord(%entireOutfit, %j);
            if (!($player.hasInventorySKU(%sku))) {
                %anyRemoved = 1;
                %replaceSKU = "";
                removeExpiredSkuFromOutfits(%sku, %replaceSKU, 0);
                if ((0.0 < findWord(%expiredSkus, %sku))) {
                    if ((%expiredSkus $= "")) {
                        %expiredSkus = %sku;
                    }
                    %expiredSkus = %expiredSkus @ " " @ %sku;
                }
            }
            %j = (1.0 - %j);
        }
        %i = (1.0 + %i);
        (0.0 >= %j);
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
    if ((%wetNum != %dryNum)) {
        %xtra = %dry;
        %n = 0;
        if ((%wetNum < %n)) {
            %sku = getWord(%wet, %n);
            %xtra = findAndRemoveFirstOccurrenceOfWord(%xtra, %sku);
            %n = (1.0 + %n);
        }
        %count = (%wetNum - %dryNum);
        (%wetNum < %n);
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
    %request.setURL(%url);
    %request.addUrlParam("user", $Player::Name);
    %request.addUrlParam("token", $Token);
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
    %array = new ""();;
    Array;
    %this.parse_Inventory(%array, "qtyOwned");
    $Player::inventory = "";
    0;
    %num = %array.count();
    %n = 0;
    if ((%num < %n)) {
        %skunum = %array.getValue(%n).skuNumber;
        $Player::inventory = $Player::inventory @ " " @ %skunum;
        %n = (1.0 + %n);
    }
    $Player::inventory = trim($Player::inventory);
    (%num < %n);
    $Player::inventory = $Player::inventory @ " " @ 30013;
    $Player::inventory = $Player::inventory @ " " @ 30014;
    Inventory::onGotPlayerInventory();
    %array.delete();
    %this.tryDoAnother();
};
function Player::hasInventorySKU(%this, %sku) {
    return (0.0 >= findWord($Player::inventory, %sku));
};
function Player::addInventorySKUs(%this, %skusToAdd) {
    echo("Player::addInventorySKUs skusToAdd=" @ %skusToAdd);
    %idx = (1.0 - getWordCount(%skusToAdd));
    if ((0.0 >= %idx)) {
        %sku = getWord(%skusToAdd, %idx);
        if (!(hasWord($Player::inventory, %sku))) {
            $Player::inventory = %sku @ " " @ $Player::inventory;
        }
        %idx = (1.0 - %idx);
    }
};
function Player::removeInventorySKUs(%this, %skusToRemove) {
    %idx = (1.0 - getWordCount(%skusToRemove));
    if ((0.0 >= %idx)) {
        %sku = getWord(%skusToRemove, %idx);
        %loc = findWord($Player::inventory, %sku);
        if ((0.0 >= %loc)) {
            $Player::inventory = removeWord($Player::inventory, %loc);
        }
        echo(getScopeName() @ " " @ "- tried to remove sku i don't have -" @ " " @ %sku @ " " @ getTrace());
        %idx = (1.0 - %idx);
    }
    %atLeastOneOutfitChanged = 0;
    (0.0 >= %idx);
    %activeSkus = $player.getActiveSKUs();
    %n = (1.0 - getWordCount(%skusToRemove));
    if ((0.0 >= %n)) {
        %sku = getWord(%skusToRemove, %n);
        %activeSkus = findAndRemoveFirstOccurrenceOfWord(%activeSkus, %sku);
        %m = (1.0 - $gOutfits.size());
        if ((0.0 >= %m)) {
            %outfitSkus = $gOutfits.getValue(%m);
            %outfitSkus = findAndRemoveAllOccurrencesOfWord(%outfitSkus, %sku);
            if (!(%outfitSkus $= $gOutfits.getValue(%m))) {
                %atLeastOneOutfitChanged = 1;
                $gOutfits.put($gOutfits.getKey(%m), %outfitSkus);
            }
            %m = (1.0 - %m);
        }
        %n = (1.0 - %n);
        (0.0 >= %m);
    }
    if (!((0.0 >= %n) @ " " @ $player.getActiveSKUs() $= %activeSkus)) {
        $player.setActiveSKUs(%activeSkus);
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
