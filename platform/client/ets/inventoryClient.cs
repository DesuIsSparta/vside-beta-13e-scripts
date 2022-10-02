$gCurrentStoreName = "";
$gStoreNameStack = "";
$gVHDUserNameFilter = "";
$gVHDUserNoStock = 0;
$gInventoryFetchFakeDelay = 1000;
$ClientIsAuthoritativeForInventory = 0;
function clientCmdOnEnterStore(%storename)
{
    log("inventory", "info", "Entering store" SPC %storename);
    %idx = findWord($gStoreNameStack, %storename);
    while (%idx != -1)
    {
        error("Entering same store twice:" SPC %storename SPC "(corrected)");
        $gStoreNameStack = removeWord($gStoreNameStack, %idx);
        %idx = findWord($gStoreNameStack, %storename);
    }
    $gStoreNameStack = %storename SPC $gStoreNameStack;
    $gCurrentStoreName = %storename;
    resetStorePosition();
    Inventory::fetchStoreInventory(%storename);
    getUserActivityMgr().setActivityActive("shoppingForClothes", 1);
    return ;
}
function clientCmdOnLeaveStore(%storename)
{
    log("inventory", "info", "Leaving store: \"" @ %storename @ "\".");
    clientSideOnLeaveStore(%storename);
    if ($gCurrentStoreName $= "")
    {
        ButtonBar.hideButton(storeButton);
    }
    return ;
}
function clientSideOnLeaveStore(%storename)
{
    getUserActivityMgr().setActivityActive("shoppingForClothes", 0);
    if (%storename $= "")
    {
        $gStoreNameStack = "";
        $gCurrentStoreName = "";
        $StoreSkusLayer = "";
        return ;
    }
    %idx = findWord($gStoreNameStack, %storename);
    if (%idx != -1)
    {
        $gStoreNameStack = removeWord($gStoreNameStack, %idx);
    }
    $gCurrentStoreName = getWord($gStoreNameStack, 0);
    $StoreSkusLayer = "";
    resetStorePosition();
    return ;
}
function clientCmdOnEnterVHDUserStore(%userName)
{
    %spaceName = CustomSpaceClient::GetCurrentSpaceName();
    %seppos = strpos(%spaceName, ".");
    if (%seppos > -1)
    {
        %seppos = %seppos + 1;
        %len = strlen(%spaceName) - %seppos;
        %userName = getSubStr(%spaceName, %seppos, %len);
    }
    else
    {
        %userName = "";
    }
    $gVHDUserNameFilter = %userName;
    %storename = "vhd";
    %idx = findWord($gStoreNameStack, %storename);
    while (%idx != -1)
    {
        error("Entering same store twice:" SPC %storename SPC "(corrected)");
        $gStoreNameStack = removeWord($gStoreNameStack, %idx);
        %idx = findWord($gStoreNameStack, %storename);
    }
    $gStoreNameStack = %storename SPC $gStoreNameStack;
    $gCurrentStoreName = %storename;
    resetStorePosition();
    Inventory::fetchVHDUserStoreInventory(%storename);
    getUserActivityMgr().setActivityActive("shoppingForClothes", 1);
    return ;
}
function clientCmdOnLeaveVHDUserStore(%storename)
{
    log("inventory", "info", "Leaving store: \"" @ %storename @ "\".");
    clientSideOnLeaveVHDUserStore(%storename);
    if ($gCurrentStoreName $= "")
    {
        ButtonBar.hideButton(storeButton);
    }
    return ;
}
function clientSideOnLeaveVHDUserStore(%storename)
{
    getUserActivityMgr().setActivityActive("shoppingForClothes", 0);
    if (%storename $= "")
    {
        $gStoreNameStack = "";
        $gCurrentStoreName = "";
        $StoreSkusLayer = "";
        return ;
    }
    %idx = findWord($gStoreNameStack, %storename);
    if (%idx != -1)
    {
        $gStoreNameStack = removeWord($gStoreNameStack, %idx);
    }
    $gCurrentStoreName = getWord($gStoreNameStack, 0);
    $StoreSkusLayer = "";
    resetStorePosition();
    $gVHDUserNameFilter = "";
    return ;
}
function Inventory::fetchVHDUserStoreInventory(%storename)
{
    if ($StandAlone)
    {
        schedule($gInventoryFetchFakeDelay, 0, "fakeVHDUserStoreInventoryGotFetchResults", %storename);
        return ;
    }
    sendRequest_GetStoreInventory($Player::Name, %storename, "OnGotDoneOrError_GetVHDUserStoreInventory");
    return ;
}
function OnGotDoneOrError_GetVHDUserStoreInventory(%request)
{
    %storename = %request.storeName;
    if (%request.checkSuccess())
    {
        Inventory::clearStore(%storename);
        $gStoreStockRevision[%storename] = %request.getValue("storeRevisionDate") ;
    }
    else
    {
        if (%request.getValue("errorCode") $= "storeNotFound")
        {
            Inventory::clearStore(%storename);
            Inventory::onGotStoreInventory(%storename);
        }
        return ;
    }
    %num = %request.getValue("itemsCount");
    %hasAuthoredInventory = 0;
    if (strlen($gVHDUserNameFilter) > 0)
    {
        %n = 0;
        while (%n < %num)
        {
            %prefix = "items" @ %n @ ".";
            %sku = %request.getValue(%prefix @ "sku");
            %si = SkuManager.findBySku(%sku);
            if (stricmp(%si.author, $gVHDUserNameFilter) == 0)
            {
                %hasAuthoredInventory = 1;
                break;
            }
            %n = %n + 1;
        }
        $gVHDUserNoStock = !%hasAuthoredInventory;
    }
    %n = 0;
    while (%n < %num)
    {
        %prefix = "items" @ %n @ ".";
        %sku = %request.getValue(%prefix @ "sku");
        %authorMatch = 0;
        if (%hasAuthoredInventory)
        {
            %si = SkuManager.findBySku(%sku);
            %authorMatch = stricmp(%si.author, $gVHDUserNameFilter) == 0 ? 1 : 0;
        }
        if (!%hasAuthoredInventory && %authorMatch)
        {
            %qty = %request.getValue(%prefix @ "quantity");
            %vpoints = %request.getValue(%prefix @ "priceVPoints");
            %vbux = %request.getValue(%prefix @ "priceVBux");
            Inventory::addItemToStore(%storename, %sku, %qty, %vpoints, %vbux);
        }
        %n = %n + 1;
    }
    Inventory::sortStoreInventory(%storename);
    Inventory::onGotVHDUserStoreInventory(%storename);
    if (!((%request.shoppingCartSkus $= "")) && isObject(StoreShoppingList))
    {
        StoreShoppingList.addSkus(%request.shoppingCartSkus);
    }
    return ;
}
function Inventory::onGotVHDUserStoreInventory(%storename)
{
    %hasUserNameFilter = !($gVHDUserNameFilter $= "") ? 1 : 0;
    $gStoreStockLoaded[%storename] = 1;
    if ((ClosetGui.isVisible() && (ClosetTabs.getCurrentTab().name $= "SHOPS")) && ($gCurrentStoreName $= %storename))
    {
        saveStorePosition();
        ClosetTabs::refreshStoreTab();
    }
    if (%hasUserNameFilter && !$gVHDUserNoStock)
    {
        %storeLongName = $gVHDUserNameFilter @ "\'s vHD Store";
        %storeDesc = %storeLongName SPC "- press F5 or click \"Shop\" to start shopping!";
    }
    else
    {
        if ($gVHDUserNoStock)
        {
            %storeLongName = $gVHDUserNameFilter @ " doesn\'t have any vHD Designs.";
            %storeDesc = %storeLongName SPC "- Instead press F5 or click \"Shop\" to start browsing the full range of clothing from vSide House of Design!";
        }
        else
        {
            %storeLongName = $gDestinationNames[%storename];
            %storeDesc = $gDestinationDescsInWorld[%storename];
        }
    }
    if (%storeLongName $= "")
    {
        error(getScopeName() SPC "- no store long name for" SPC %storename);
    }
    if (%storeDesc $= "")
    {
        error(getScopeName() SPC "- no store desc for" SPC %storename);
    }
    handleSystemMessage("msgInfoMessage", %storeDesc);
    if (!($gCurrentStoreName $= ""))
    {
        ButtonBar.showButton(storeButton);
        if (isObject(ClosetFilterField))
        {
            ClosetFilterField.setValue("");
        }
    }
    return ;
}
function fakeVHDUserStoreInventoryGotFetchResults(%storename)
{
    echo("Fake store fetch results for" SPC %storename);
    Inventory::clearStore(%storename);
    %skus = SkuManager.getStoreSkus(%storename);
    %qtys = SkuManager.getStoreQtys(%storename);
    %skusNum = getWordCount(%skus);
    %qtysNum = getWordCount(%qtys);
    if (%skusNum != getWordCount(%qtys))
    {
        error(getScopeName() SPC "- mismatched number of skus and quantities:" SPC %skusNum SPC %qtysNum);
    }
    %hasAuthoredInventory = 0;
    if (!($gVHDUserNameFilter $= ""))
    {
        %n = 0;
        while (%n < %num)
        {
            %sku = %request.getValue(%prefix @ "sku");
            %si = SkuManager.findBySku(%sku);
            if (%si.author $= $gVHDUserNameFilter)
            {
                %hasAuthoredInventory = 1;
                break;
            }
            %n = %n + 1;
        }
        $gVHDUserNoStock = !%hasAuthoredInventory;
    }
    %n = 0;
    while (%n < %skusNum)
    {
        %sku = getWord(%skus, %n);
        if (%n < %qtysNum)
        {
            %qty = getWord(%qtys, %n);
        }
        else
        {
            %qty = -1;
        }
        %authorMatch = 0;
        if (%hasAuthoredInventory)
        {
            %si = SkuManager.findBySku(%sku);
            %authorMatch = %si.author $= $gVHDUserNameFilter ? 1 : 0;
        }
        if (!%hasAuthoredInventory && %authorMatch)
        {
            %vbux = %si.price;
            %vpoints = %si.price * 2;
            Inventory::addItemToStore(%storename, %sku, %qty, %vpoints, %vbux);
        }
        %n = %n + 1;
    }
    Inventory::onGotVHDUserStoreInventory(%storename);
    return ;
}
function resetStorePosition()
{
    $gStoreScrollPos = 0;
    $gStoreCurrentCategory = "";
    return ;
}
function loadStorePosition()
{
    %presentCategory = StoreCategoryPopup.getText();
    if (%presentCategory $= "")
    {
        StoreCategoryPopup.SetSelected(0);
    }
    else
    {
        if (!($gStoreCurrentCategory $= %presentCategory))
        {
            %catIndex = StoreCategoryPopup.findText($gStoreCurrentCategory);
            if (%catIndex >= 0)
            {
                StoreCategoryPopup.SetSelected(%catIndex);
            }
            else
            {
                $gStoreCurrentCategory = %presentCategory;
            }
        }
    }
    if ($gStoreScrollPos $= "")
    {
        $gStoreScrollPos = 0;
    }
    ClosetTabs.getTabWithName("Shops").itemsScroll.scrollTo(0, $gStoreScrollPos);
    return ;
}
function saveStorePosition()
{
    $gStoreScrollPos = 0 - getWord(ClosetTabs.getTabWithName("Shops").thumbnails.getPosition(), 1);
    $gStoreCurrentCategory = StoreCategoryPopup.getText();
    return ;
}
function clientCmdUpdateInventorySkus(%invChangedSkus, %notify, %autoEquip)
{
    log("inventory", "info", "clientCmdUpdateInventorySkus(): invChangedSkus (<skus-added> | <skus-removed>) =" @ %invChangedSkus);
    %invChangedSkus = strreplace(%invChangedSkus, "\t", " ");
    %invChangedSkus = strreplace(%invChangedSkus, "|", "\t");
    %skusAdded = trim(getField(%invChangedSkus, 0));
    %skusRemoved = trim(getField(%invChangedSkus, 1));
    log("inventory", "debug", getScopeName() SPC "- skus   Added=" @ %skusAdded);
    log("inventory", "debug", getScopeName() SPC "- skus Removed=" @ %skusRemoved);
    updateInventorySkus(%skusAdded, %skusRemoved, %notify, %autoEquip, "");
    return ;
}
function updateInventorySkus(%skusAdded, %skusRemoved, %notify, %autoEquip, %srcName)
{
    %entryTime = getRealTime();
    echoDebug(getScopeName() SPC "skus added  :" SPC %skusAdded);
    echoDebug(getScopeName() SPC "skus removed:" SPC %skusRemoved);
    $player.addInventorySKUs(%skusAdded);
    $player.removeInventorySKUs(%skusRemoved);
    if (isObject(ClosetItemPopup))
    {
        ClosetItemPopup.update($Player::inventory);
    }
    if ((%notify && !ClosetGui.isVisible()) || !((ClosetTabs.getCurrentTab().name $= "Shops")))
    {
        notifyUserOfSkusGained(%skusAdded, %srcName, %autoEquip);
    }
    if (%autoEquip)
    {
        Inventory::equipOrWearSkus(%skusAdded);
    }
    if (!((CustomSpaceClient::GetSpaceImIn() $= "")) && CustomSpaceClient::isOwner())
    {
        getOwnedFurniture();
    }
    %exitTime = getRealTime();
    error(getScopeName() SPC "- time:" SPC mSubS32(%exitTime, %entryTime));
    return ;
}
function clientCmdGiftReceived(%skus, %srcName)
{
    $player.addInventorySKUs(%skus);
    notifyUserOfSkusGained(%skus, %srcName, 0);
    if (ClosetGui.isVisible())
    {
        ClosetTabs.selectCurrentTab();
    }
    return ;
}
function clientCmdInventoryExpiration(%skusAboutToExpire, %skusJustExpired)
{
    log("inventory", "info", "clientCmdInventoryExpiration(): skusAboutToExpire (<sku>|<numItems>|<remaining>|<reason>SPC<next>) =\"" @ %skusAboutToExpire @ "\", skusJustExpired (<sku>|<numItems>|<reason>|<replacementSKU>SPC<next>) = \"" @ %skusJustExpired @ "\"");
    %skusAboutToExpire = strreplace(%skusAboutToExpire, " ", "\n");
    %skusAboutToExpire = strreplace(%skusAboutToExpire, "|", "\t");
    %skusJustExpired = strreplace(%skusJustExpired, " ", "\n");
    %skusJustExpired = strreplace(%skusJustExpired, "|", "\t");
    %aboutToExpireCount = getRecordCount(%skusAboutToExpire);
    %justExpiredCount = getRecordCount(%skusJustExpired);
    if ((%aboutToExpireCount == 0) && (%justExpiredCount == 0))
    {
        error(getScopeName() @ "-> received empty update... this shouldn\'t happen.");
        return ;
    }
    if (%justExpiredCount > 0)
    {
        %sound = AudioProfile_JustExpired;
    }
    else
    {
        if (%aboutToExpireCount > 0)
        {
            %sound = AudioProfile_ExpiringSoon;
        }
    }
    schedule(500, 0, "alxPlay", %sound);
    %msg = "";
    %listOfSkusJustExpired = "";
    if (%justExpiredCount)
    {
        %activeSkus = $player.getActiveSKUs();
        %realExpiredCount = 0;
        %i = 0;
        while (%i < %justExpiredCount)
        {
            %itemStr = getRecord(%skusJustExpired, %i);
            %num = getField(%itemStr, 1);
            %realExpiredCount = %realExpiredCount + %num;
            %i = %i + 1;
        }
        if (%realExpiredCount > 1)
        {
            %msg = %realExpiredCount @ " of your items just expired (";
        }
        else
        {
            %msg = "Your ";
        }
        %listOfSkusJustExpired = "";
        %listOfUniqueSkusJustExpired = "";
        %i = 0;
        while (%i < %justExpiredCount)
        {
            if (%i > 0)
            {
                %msg = %msg @ ", ";
            }
            %itemStr = getRecord(%skusJustExpired, %i);
            %sku = getField(%itemStr, 0);
            %num = getField(%itemStr, 1);
            %reason = getField(%itemStr, 2);
            %replaceSKU = getField(%itemStr, 3);
            if (%num $= "")
            {
                %num = 1;
            }
            %si = SkuManager.findBySku(%sku);
            if (isObject(%si))
            {
                %name = %si.descShrt;
            }
            else
            {
                error(getScopeName() @ "-> unknown sku just expired");
                %name = "(oops, bug)";
            }
            if (%num > 1)
            {
                %msg = %msg @ %num;
            }
            %msg = %msg @ %name @ %num > 1 ? "s" : "";
            removeExpiredSkuFromOutfits(%sku, %replaceSKU, 0);
            %activeSkus = removeAndReplaceSkuFromSkuList(%activeSkus, %sku, %replaceSKU);
            if (%listOfUniqueSkusJustExpired $= "")
            {
                %listOfUniqueSkusJustExpired = %sku;
            }
            else
            {
                %listOfUniqueSkusJustExpired = %listOfUniqueSkusJustExpired SPC %sku;
            }
            %k = 0;
            while (%k < %num)
            {
                %listOfSkusJustExpired = %listOfSkusJustExpired @ %sku @ " ";
                %k = %k + 1;
            }
            %i = %i + 1;
        }
        notifyUserOfSkusExpired(%listOfUniqueSkusJustExpired, "");
        if (%justExpiredCount > 0)
        {
            outfits_persist();
        }
        commandToServer('SetActiveSkus', %activeSkus);
        if (%justExpiredCount > 1)
        {
            %msg = %msg @ ")";
        }
        else
        {
            %msg = %msg @ " just expired";
        }
        if (%aboutToExpireCount)
        {
            %msg = %msg @ ", AND ";
        }
        else
        {
            %msg = %msg @ ".";
        }
    }
    if (%aboutToExpireCount)
    {
        %realAboutToExpireCount = 0;
        %i = 0;
        while (%i < %aboutToExpireCount)
        {
            %itemStr = getRecord(%skusAboutToExpire, %i);
            %num = getField(%itemStr, 1);
            if (%num $= "")
            {
                %num = 1;
            }
            %realAboutToExpireCount = %realAboutToExpireCount + %num;
            %i = %i + 1;
        }
        if (%realAboutToExpireCount > 1)
        {
            %msg = %msg @ %realAboutToExpireCount @ " of your items are about to expire! (";
        }
        else
        {
            %msg = %msg @ %justExpiredCount > 0 ? "y" : "Y" @ "our";
        }
        %i = 0;
        while (%i < %aboutToExpireCount)
        {
            if (%i > 0)
            {
                %msg = %msg @ ", ";
            }
            %aboutToExpireSku = getRecord(%skusAboutToExpire, %i);
            %sku = getField(%aboutToExpireSku, 0);
            %num = getField(%aboutToExpireSku, 1);
            %remaining = getField(%aboutToExpireSku, 2);
            %reason = getField(%aboutToExpireSku, 3);
            %si = SkuManager.findBySku(%sku);
            if (isObject(%si))
            {
                %name = %si.descShrt;
            }
            else
            {
                error(getScopeName() @ "-> unknown sku about to expire");
                %name = "(oops, bug)";
            }
            %msg = %msg @ %num SPC %name @ %num > 1 ? "s" : "";
            if (%realAboutToExpireCount == 1)
            {
                %msg = %msg @ " expires";
            }
            %roundedRemaining = mFloor((%remaining + 30) / 60) * 60;
            %msg = %msg @ " in " @ secondsToDaysHoursMinutesSeconds(%roundedRemaining);
            %i = %i + 1;
        }
        if (%realAboutToExpireCount > 1)
        {
            %msg = %msg @ ").";
        }
        else
        {
            %msg = %msg @ ".";
        }
    }
    if (!(%msg $= ""))
    {
        handleSystemMessage('MsgInfoMessage', %msg);
    }
    if (!(%listOfSkusJustExpired $= ""))
    {
        log("inventory", "debug", "clientCmdInventoryExpiration(): removed from inventory: skusJustExpired=" @ %listOfSkusJustExpired);
        $player.removeInventorySKUs(%listOfSkusJustExpired);
    }
    return ;
}
function removeAndReplaceSkuFromSkuList(%skusOutfit, %sku, %replaceSKU)
{
    if (%sku $= "")
    {
        error(getScopeName() SPC "sku is null can\'t remove from skusoutfits:" SPC %skusOutfit SPC "or replace with" SPC %replaceSKU);
        return %skusOutfit;
    }
    if (%idx = findWord(%skusOutfit, %sku) >= 0)
    {
        %changes = %changes + 1;
        %skusOutfit = removeWord(%skusOutfit, %idx);
        if (%replaceSKU && !((%replaceSKU $= "")))
        {
            if (findWord(%skusOutfit, %replaceSKU) < 0)
            {
                if (%skusOutfit $= "")
                {
                    %skusOutfit = %replaceSKU;
                }
                else
                {
                    %skusOutfit = %skusOutfit SPC %replaceSKU;
                }
            }
        }
    }
    return %skusOutfit;
}
function removeExpiredSkuFromOutfits(%oldSku, %newSku, %notifyUser)
{
    if (!isObject($player))
    {
        error(getScopeName() SPC "$player is not valid, cannot remove expired sku and replace with new one" SPC %sku SPC %newSku);
        return 0;
    }
    %outfitNames = $Player::HangerNames[$player.getGender()];
    %i = 0;
    while (%i < $gClosetNumOutfits)
    {
        %name = getWord(%outfitNames, %i);
        %keyOutfit = %name;
        %keyBody = $player.getGender() @ "Body";
        %skusOutfit = $gOutfits.get(%keyOutfit);
        %skusOutfit = removeAndReplaceSkuFromSkuList(%skusOutfit, %oldSku, %newSku);
        %STOCKOutfit = $gNewStockOutfits[%keyOutfit];
        %n = getWordCount(%STOCKOutfit) - 1;
        while (%n >= 0)
        {
            %sku = getWord(%STOCKOutfit, %n);
            %drawer = SkuManager.findBySku(%sku).drwrName;
            %optional = SkuManager.isOptionalDrawer(%drawer);
            if (%optional)
            {
                %STOCKOutfit = removeWord(%STOCKOutfit, %n);
            }
            %n = %n - 1;
        }
        %skusOutfit = SkuManager.overlaySkus(%STOCKOutfit, %skusOutfit);
        %skusBody = $gOutfits.get(%keyBody);
        %skusBody = removeAndReplaceSkuFromSkuList(%skusBody, %oldSku, %newSku);
        %STOCKBody = $gDefaultBodyAttrs[$player.getGender()];
        %n = getWordCount(%STOCKBody) - 1;
        while (%n >= 0)
        {
            %sku = getWord(%STOCKBody, %n);
            %drawer = SkuManager.findBySku(%sku).drwrName;
            %optional = SkuManager.isOptionalDrawer(%drawer);
            if (%optional)
            {
                %STOCKBody = removeWord(%STOCKBody, %n);
            }
            %n = %n - 1;
        }
        %skusBody = SkuManager.overlaySkus(%STOCKBody, %skusBody);
        $gOutfits.put(%keyBody, %skusBody);
        $gOutfits.put(%keyOutfit, %skusOutfit);
        %i = %i + 1;
    }
    if (%notifyUser)
    {
        notifyUserOfSkusExpired(%oldSku, "");
    }
    return ;
}
function notifyUserOfSkusGained(%skus, %srcName, %autoEquipped)
{
    echo(getScopeName() SPC %skus SPC %srcName);
    if (isObject($player))
    {
        %gender = $player.getGender();
    }
    else
    {
        error(getScopeName() SPC "- ERROR NO Player OBJECT using forced gender");
        %gender = "f";
    }
    %skus = SkuManager.filterSkusGender(%skus, %gender);
    %skus = SkuManager.filterSkusVisible(%skus, 1);
    if (%skus $= "")
    {
        echo(getScopeName() SPC "- no visible skus." SPC %skus SPC getTrace());
        return ;
    }
    %skusWearable = SkuManager.filterSkusWearable(%skus, 1);
    %skusUnwearable = SkuManager.filterSkusWearable(%skus, 0);
    %skusFurnishing = SkuManager.filterSkusType(%skus, "furnishing");
    %numWearable = getWordCount(%skusWearable);
    %numUnwearable = getWordCount(%skusUnwearable);
    %numFurnishing = getWordCount(%skusFurnishing);
    %numSkus = %numWearable + %numUnwearable;
    %numNonFurnishing = %numSkus - %numFurnishing;
    %listWearable = SkuManager.getSkuShortDescriptions(%skusWearable, ", ", 1, 0);
    %listUnwearable = SkuManager.getSkuShortDescriptions(%skusUnwearable, ", ", 1, 32);
    if (%numWearable > 0)
    {
        %list = %listWearable @ %listUnwearable $= "" ? "" : " and";
    }
    else
    {
        %list = %listUnwearable;
    }
    %wordNonFurnishingItThem = %numNonFurnishing == 1 ? "it" : "them";
    %wordNonFurnishingItemItems = %numNonFurnishing == 1 ? "item" : "items";
    %wordWearableItThem = %numWearable == 1 ? "it" : "them";
    %wordWearableItemItems = %numWearable == 1 ? "item" : "items";
    %wordFurnishingItThem = %numFurnishing == 1 ? "it" : "them";
    if (%numFurnishing > 0)
    {
        %specialOrFurniture = %numFurnishing != %numUnwearable ? "special or furniture" : "furniture";
    }
    else
    {
        %specialOrFurniture = "special";
    }
    %msgUnwearable = "";
    if (%numNonFurnishing > 0)
    {
        if (%numWearable > 0)
        {
            %msgUnwearable = %numUnwearable == 0 ? "" : "\n(";
        }
        else
        {
            %msgUnwearable = "\n(" @ %numUnwearable == 1 ? "It\'s" : "They\'re" SPC %specialOrFurniture @ " and can\'t actually be worn)";
        }
    }
    %YoullFindStr = "";
    if (!%autoEquipped)
    {
        if (%numNonFurnishing > 0)
        {
            %YoullFindStr = "You\'ll find" SPC %numFurnishing > 0 ? "your non-furnishing" : %wordNonFurnishingItThem SPC "in the closet (F5).";
        }
        if (%numFurnishing > 0)
        {
            %YoullFindStr = %YoullFindStr @ %numNonFurnishing > 0 ? "" : "You" SPC "can place" SPC %numNonFurnishing > 0 ? "your new furniture" : %wordFurnishingItThem SPC "in your apartment using the Space->My Furnishings panel.";
        }
    }
    if (%srcName $= $Player::Name)
    {
        %srcName = "yourself";
    }
    %fromString = %srcName $= "" ? "" : " from";
    %msg = "You just got" SPC %numSkus == 1 ? "a new item" : %numSkus @ %fromString @ "!";
    %msg = %msg NL "(" @ %list @ ")";
    %msg = %msg @ %msgUnwearable;
    %msg = %msg NL %YoullFindStr;
    if (!%autoEquipped && (%numWearable > 0))
    {
        if (%numFurnishing == 0)
        {
            %msg = %msg NL "" NL "Would you like to wear " @ %wordWearableItThem @ " now?";
            if (SalonDoesListContainAnySalonRewardSKUs(%skus))
            {
                echo(getScopeName() SPC "found a salon sku in the list, so we are wearing it immediately without asking: " SPC %skus);
                Inventory::equipOrWearSkus(%skus);
                return ;
            }
        }
        else
        {
            %msg = %msg NL "" NL "Would you like to put on your wearable " @ %wordWearableItemItems @ " now?";
        }
        %mb = MessageBoxYesNo("Score!", %msg, "Inventory::equipOrWearSkus(\"" @ %skus @ "\"); if($gAutoOrbitOnReceiveItem && !$IN_ORBIT_CAM) nextPlayerCamMode();", "");
    }
    else
    {
        %mb = MessageBoxOK("Score!", %msg, "");
    }
    %mb.text.setText(%mb.message);
    return ;
}
function notifyUserOfSkusExpired(%skus, %srcName)
{
    %skus = trim(%skus);
    if (%skus $= "")
    {
        return ;
    }
    %count = getWordCount(%skus);
    if (%count == 0)
    {
        return ;
    }
    %descriptions = SkuManager.getSkuShortDescriptions(%skus, ", ", 0);
    %msg = %count == 1 ? "An item in your outfits expired while you were away:" : "Some items in your outfits expired while you were away:";
    handleSystemMessage("msgInfoMessage", %msg SPC %descriptions);
    return ;
}
function Inventory::equipOrWearSkus(%skus)
{
    if (%skus $= "")
    {
        return ;
    }
    %skusNew = SkuManager.filterSkusGender(%skus, $player.getGender());
    log("inventory", "debug", "Inventory::equipOrWearSkus(): %skus=" @ %skus @ ", %skusNew=" @ %skusNew);
    %skusDry = $player.getActiveSKUs();
    log("inventory", "debug", "Inventory::equipOrWearSkus(): %skusDry=" @ %skusDry);
    %skusWet = SkuManager.overlaySkus(%skusDry, %skusNew);
    log("inventory", "info", "Inventory::equipOrWearSkus(): %skusWet=" @ %skusWet);
    if (ClosetGui.isVisible())
    {
        $ClosetSkusOutfit[$ClosetOutfitName] = SkuManager.filterSkusForClothing(%skusWet) ;
        $ClosetSkusBody = SkuManager.filterSkusForBody(%skusWet);
        ClosetTabs.selectCurrentTab();
        ClosetGui.updateVisibleAvatar();
    }
    else
    {
        SaveOutfitAndBodySkusAsCurrent(%skusWet);
    }
    return ;
}
function Inventory::fetchStoreInventory(%storename)
{
    if ($StandAlone)
    {
        schedule($gInventoryFetchFakeDelay, 0, "fakeStoreInventoryGotFetchResults", %storename);
        return ;
    }
    sendRequest_GetStoreInventory($Player::Name, %storename, "OnGotDoneOrError_GetStoreInventory");
    return ;
}
function OnGotDoneOrError_GetStoreInventory(%request)
{
    %storename = %request.storeName;
    if (%request.checkSuccess())
    {
        Inventory::clearStore(%storename);
        $gStoreStockRevision[%storename] = %request.getValue("storeRevisionDate") ;
    }
    else
    {
        if (%request.getValue("errorCode") $= "storeNotFound")
        {
            Inventory::clearStore(%storename);
            Inventory::onGotStoreInventory(%storename);
        }
        return ;
    }
    %num = %request.getValue("itemsCount");
    %n = 0;
    while (%n < %num)
    {
        %prefix = "items" @ %n @ ".";
        %sku = %request.getValue(%prefix @ "sku");
        %qty = %request.getValue(%prefix @ "quantity");
        %vpoints = %request.getValue(%prefix @ "priceVPoints");
        %vbux = %request.getValue(%prefix @ "priceVBux");
        Inventory::addItemToStore(%storename, %sku, %qty, %vpoints, %vbux);
        %n = %n + 1;
    }
    Inventory::sortStoreInventory(%storename);
    Inventory::onGotStoreInventory(%storename);
    if (!((%request.shoppingCartSkus $= "")) && isObject(StoreShoppingList))
    {
        StoreShoppingList.addSkus(%request.shoppingCartSkus);
    }
    return ;
}
function Inventory::sortStoreInventory(%storename)
{
    warn(getScopeName() SPC "- should be done server-side. ETS-3468");
    %allSkus = SkuManager.filterSkusGender(SkuManager.getSkus(), $UserPref::Player::gender);
    $gStoreStockCacheSkus[%storename] = Inventory::sortSkus($gStoreStockCacheSkus[%storename], %allSkus) ;
    return ;
}
function Inventory::sortSkus(%skusToSort, %orderToAppearIn)
{
    %ret = "";
    %n = getWordCount(%orderToAppearIn) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%orderToAppearIn, %n);
        if (findWord(%skusToSort, %sku) >= 0)
        {
            %ret = %sku SPC %ret;
        }
        %n = %n - 1;
    }
    %ret = trim(%ret);
    return %ret;
}
function fakeStoreInventoryGotFetchResults(%storename)
{
    echo("Fake store fetch results for" SPC %storename);
    Inventory::clearStore(%storename);
    %skus = SkuManager.getStoreSkus(%storename);
    %qtys = SkuManager.getStoreQtys(%storename);
    %skusNum = getWordCount(%skus);
    %qtysNum = getWordCount(%qtys);
    if (%skusNum != getWordCount(%qtys))
    {
        error(getScopeName() SPC "- mismatched number of skus and quantities:" SPC %skusNum SPC %qtysNum);
    }
    %n = 0;
    while (%n < %skusNum)
    {
        %sku = getWord(%skus, %n);
        if (%n < %qtysNum)
        {
            %qty = getWord(%qtys, %n);
        }
        else
        {
            %qty = -1;
        }
        %si = SkuManager.findBySku(%sku);
        %vbux = %si.price;
        %vpoints = %si.price * 2;
        Inventory::addItemToStore(%storename, %sku, %qty, %vpoints, %vbux);
        %n = %n + 1;
    }
    Inventory::onGotStoreInventory(%storename);
    return ;
}
function Inventory::addItemToStore(%storename, %sku, %qty, %vpoints, %vbux)
{
    $gStoreStockCacheSkus[%storename] = $gStoreStockCacheSkus[%storename] @ %sku @ " ";
    $gStoreItemsQty[%sku] = %qty ;
    $gStoreItemsVPoints[%sku] = %vpoints < 0 ? "-" : mFloor(%vpoints) ;
    $gStoreItemsVBux[%sku] = %vbux < 0 ? "-" : mFloor(%vbux) ;
    return ;
}
function Inventory::getVPointsPriceForSku(%sku)
{
    %price = $gStoreItemsVPoints[%sku];
    if (!(%price $= ""))
    {
        return %price;
    }
    %si = SkuManager.findBySku(%sku);
    if (isObject(%si) && !((%si.priceVPoints $= "")))
    {
        return %si.priceVPoints == -1 ? "-" : %si;
    }
    return "-";
}
function Inventory::getVBuxPriceForSku(%sku)
{
    %price = $gStoreItemsVBux[%sku];
    if (!(%price $= ""))
    {
        return %price;
    }
    %si = SkuManager.findBySku(%sku);
    if (isObject(%si) && !((%si.priceVBux $= "")))
    {
        return %si.priceVBux == -1 ? "-" : %si;
    }
    return "-";
}
function Inventory::getTotalPrice(%currency, %skus)
{
    %hasValidSku = 0;
    %total = 0;
    %count = getWordCount(%skus);
    %i = 0;
    while (%i < %count)
    {
        %sku = getWord(%skus, %i);
        %price = %currency $= "vPoints" ? Inventory::getVPointsPriceForSku(%sku) : Inventory::getVBuxPriceForSku(%sku);
        if (!(%price $= "-"))
        {
            %total = %total + %price;
            %hasValidSku = 1;
        }
        %i = %i + 1;
    }
    return %hasValidSku ? %total : "-";
}
function Inventory::filterSkusByValidPrice(%currency, %skus)
{
    %validSkus = "";
    %count = getWordCount(%skus);
    %i = 0;
    while (%i < %count)
    {
        %sku = getWord(%skus, %i);
        %price = %currency $= "vPoints" ? Inventory::getVPointsPriceForSku(%sku) : Inventory::getVBuxPriceForSku(%sku);
        if (!(%price $= "-"))
        {
            %validSkus = %validSkus SPC %sku;
        }
        %i = %i + 1;
    }
    return trim(%validSkus);
}
function Inventory::dumpStore(%storename)
{
    echo("store" SPC %storename);
    %num = getWordCount($gStoreStockCacheSkus[%storename]);
    %n = 0;
    while (%n < %num)
    {
        %sku = getWord($gStoreStockCacheSkus[%storename], %n);
        %qty = $gStoreItemsQty[%sku];
        %vps = $gStoreItemsVPoints[%sku];
        %vbs = $gStoreItemsVBux[%sku];
        %si = SkuManager.findBySku(%sku);
        echo("sku:" SPC formatInt("%6d", %sku) SPC "qty" SPC formatInt("%4d", %qty) SPC "vPoints" SPC formatInt("%6d", %vps) SPC "vBux" SPC formatInt("%6d", %vbs) SPC %si.descShrt);
        %n = %n + 1;
    }
}

function Inventory::onGotStoreInventory(%storename)
{
    if (%storename $= "furnishings")
    {
        if (isObject(CSShoppingBrowser))
        {
            CSShoppingBrowser.loadAvailableSkus();
        }
        return ;
    }
    $gStoreStockLoaded[%storename] = 1;
    if ((ClosetGui.isVisible() && (ClosetTabs.getCurrentTab().name $= "SHOPS")) && ($gCurrentStoreName $= %storename))
    {
        saveStorePosition();
        ClosetTabs::refreshStoreTab();
    }
    %storeLongName = $gDestinationNames[%storename];
    %storeDesc = $gDestinationDescsInWorld[%storename];
    if (%storeLongName $= "")
    {
        error(getScopeName() SPC "- no store long name for" SPC %storename);
    }
    if (%storeDesc $= "")
    {
        error(getScopeName() SPC "- no store desc for" SPC %storename);
    }
    handleSystemMessage("msgInfoMessage", %storeDesc);
    if (!($gCurrentStoreName $= ""))
    {
        ButtonBar.showButton(storeButton);
        if (isObject(ClosetFilterField))
        {
            ClosetFilterField.setValue("");
        }
    }
    return ;
}
function Inventory::getCurrentStoreSkus()
{
    if ($gCurrentStoreName $= "")
    {
        return "no store";
    }
    return $gStoreStockCacheSkus[$gCurrentStoreName];
}
function Inventory::getStoreName(%storeID)
{
    return $gDestinationNames[%storeID];
}
function Inventory::getCurrentStoreName()
{
    if ($gCurrentStoreName $= "")
    {
        return "";
    }
    return Inventory::getStoreName($gCurrentStoreName);
}
function Inventory::getStoreDescInCloset(%storeID)
{
    return $gDestinationDescsInCloset[%storeID];
}
function Inventory::getStoreDescInWorld(%storeID)
{
    return $gDestinationDescsInWorld[%storeID];
}
function Inventory::getCurrentStoreDescInCloset()
{
    return Inventory::getStoreDescInCloset($gCurrentStoreName);
}
function Inventory::getCurrentStoreDescInWorld()
{
    return Inventory::getStoreDescInWorld($gCurrentStoreName);
}
function Inventory::getStoreDrwrs(%storeID)
{
    return Inventory::getStoreDrwrs($gCurrentStoreName);
}
function Inventory::getCurrentStoreDrwrs()
{
    return Inventory::getStoreDrwrs($gCurrentStoreName);
}
function Inventory::fetchPlayerInventoryIfNeedTo(%playerObj)
{
    echoDebug("inventory", "This should be more sophisticated. Something like if (!touched).");
    Inventory::fetchPlayerInventory(%playerObj);
    return ;
}
function Inventory::fetchPlayerInventoryIfEmpty()
{
    if (!isDefined("$player::inventory") && ($Player::inventory $= ""))
    {
        Inventory::fetchPlayerInventory();
    }
    return ;
}
function fakePlayerInventoryGotFetchResults()
{
    echo("Fake player fetch inventory results");
    $Player::inventory = SkuManager.getBornWithSkus();
    $Player::inventory = trim($Player::inventory);
    %skus = SkuManager.getSkusDrwr("props");
    $Player::inventory = $Player::inventory SPC %skus;
    $Player::inventory = trim($Player::inventory);
    Inventory::onGotPlayerInventory();
    return ;
}
function Inventory::onGotPlayerInventory()
{
    echo("got player inventory");
    if ($Player::inventory $= "")
    {
        error(getScopeName() @ "->got empty player inventory!");
        %notEmpty = 0;
    }
    else
    {
        %notEmpty = 1;
    }
    Inventory::dedupeSkus($Player::inventory);
    if (($Player::inventory $= "") && %notEmpty)
    {
        error(getScopeName() @ "->got empty player inventory after deduping!");
    }
    if ($gClosetGuiNeedsOpen)
    {
        BodyItemsFrame.update();
    }
    if (!($Player::inventory $= ""))
    {
        Inventory::EnsurePlayerOwnsOutfitItems();
    }
    return ;
}
function Inventory::EnsurePlayerOwnsOutfitItems()
{
    %outfitNames = $Player::HangerNames[$player.getGender()];
    %anyRemoved = 0;
    %expiredSkus = "";
    %i = 0;
    while (%i < $gClosetNumOutfits)
    {
        %name = getWord(%outfitNames, %i);
        %keyOutfit = %name;
        %keyBody = $player.getGender() @ "Body";
        %skusOutfit = $gOutfits.get(%keyOutfit);
        %skusBody = $gOutfits.get(%keyBody);
        %entireOutfit = %skusBody SPC %skusOutfit;
        %count = getWordCount(%entireOutfit);
        %j = %count - 1;
        while (%j >= 0)
        {
            %sku = getWord(%entireOutfit, %j);
            if (!$player.hasInventorySKU(%sku))
            {
                %anyRemoved = 1;
                %replaceSKU = "";
                removeExpiredSkuFromOutfits(%sku, %replaceSKU, 0);
                if (findWord(%expiredSkus, %sku) < 0)
                {
                    if (%expiredSkus $= "")
                    {
                        %expiredSkus = %sku;
                    }
                    else
                    {
                        %expiredSkus = %expiredSkus SPC %sku;
                    }
                }
            }
            %j = %j - 1;
        }
        %i = %i + 1;
    }
    notifyUserOfSkusExpired(%expiredSkus, "");
    if (%anyRemoved)
    {
        outfits_persist();
    }
    return ;
}
function Inventory::dedupeSkus(%dry)
{
    %dryNum = getWordCount(%dry);
    %wet = dedupeWords(%dry);
    %wetNum = getWordCount(%wet);
    if (%dryNum != %wetNum)
    {
        %xtra = %dry;
        %n = 0;
        while (%n < %wetNum)
        {
            %sku = getWord(%wet, %n);
            %xtra = findAndRemoveFirstOccurrenceOfWord(%xtra, %sku);
            %n = %n + 1;
        }
        %count = %dryNum - %wetNum;
        error(getScopeName() SPC "- Got duplicate skus. Count: " SPC %count SPC "and here they are:" SPC %xtra SPC "dry:" SPC %dry SPC getTrace());
    }
    return %wet;
}
function Inventory::clearStore(%storename)
{
    $gStoreStockCacheSkus[%storename] = "";
    $gStoreStockRevision[%storename] = "";
    return ;
}
function Inventory::giftItemToPlayer(%unused, %unused)
{
    return ;
}
function Inventory::fetchPlayerInventory()
{
    log("inventory", "info", getScopeName());
    if ($ClientIsAuthoritativeForInventory && $StandAlone)
    {
        schedule($gInventoryFetchFakeDelay, 0, "fakePlayerInventoryGotFetchResults");
        log("inventory", "info", "standalone - using fake player inventory fetch");
        return ;
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
    log("network", "debug", getScopeName() SPC "-" SPC %request.getURL());
    return ;
}
function InventoryRequest::tryDoAnother(%this)
{
    if (!%this.doAnother)
    {
        return ;
    }
    log("network", "debug", getScopeName() SPC "- doing another on" SPC %this.getURL());
    Inventory::fetchPlayerInventory($player);
    return ;
}
function InventoryRequest::onError(%this, %unused, %unused)
{
    %this.tryDoAnother();
    return ;
}
function InventoryRequest::onDone(%this)
{
    if (!%this.checkSuccess())
    {
        error(getScopeName() @ "->InventoryRequest failed! Inventory = \"" @ $Player::inventory @ "\"");
        return ;
    }
    %array = new Array();
    %this.parse_Inventory(%array, "qtyOwned");
    $Player::inventory = "";
    %num = %array.count();
    %n = 0;
    while (%n < %num)
    {
        %skunum = %array.getValue(%n).skuNumber;
        $Player::inventory = $Player::inventory SPC %skunum;
        %n = %n + 1;
    }
    $Player::inventory = trim($Player::inventory);
    $Player::inventory = $Player::inventory SPC 30013;
    $Player::inventory = $Player::inventory SPC 30014;
    Inventory::onGotPlayerInventory();
    %array.delete();
    %this.tryDoAnother();
    return ;
}
function Player::hasInventorySKU(%this, %sku)
{
    return findWord($Player::inventory, %sku) >= 0;
}
function Player::addInventorySKUs(%this, %skusToAdd)
{
    echo("Player::addInventorySKUs skusToAdd=" @ %skusToAdd);
    %idx = getWordCount(%skusToAdd) - 1;
    while (%idx >= 0)
    {
        %sku = getWord(%skusToAdd, %idx);
        if (!hasWord($Player::inventory, %sku))
        {
            $Player::inventory = %sku SPC $Player::inventory;
        }
        %idx = %idx - 1;
    }
}

function Player::removeInventorySKUs(%this, %skusToRemove)
{
    %idx = getWordCount(%skusToRemove) - 1;
    while (%idx >= 0)
    {
        %sku = getWord(%skusToRemove, %idx);
        %loc = findWord($Player::inventory, %sku);
        if (%loc >= 0)
        {
            $Player::inventory = removeWord($Player::inventory, %loc);
        }
        else
        {
            echo(getScopeName() SPC "- tried to remove sku i don\'t have -" SPC %sku SPC getTrace());
        }
        %idx = %idx - 1;
    }
    %atLeastOneOutfitChanged = 0;
    %activeSkus = $player.getActiveSKUs();
    %n = getWordCount(%skusToRemove) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%skusToRemove, %n);
        %activeSkus = findAndRemoveFirstOccurrenceOfWord(%activeSkus, %sku);
        %m = $gOutfits.size() - 1;
        while (%m >= 0)
        {
            %outfitSkus = $gOutfits.getValue(%m);
            %outfitSkus = findAndRemoveAllOccurrencesOfWord(%outfitSkus, %sku);
            if (!(%outfitSkus $= $gOutfits.getValue(%m)))
            {
                %atLeastOneOutfitChanged = 1;
                $gOutfits.put($gOutfits.getKey(%m), %outfitSkus);
            }
            %m = %m - 1;
        }
        %n = %n - 1;
    }
    if (!($player.getActiveSKUs() $= %activeSkus))
    {
        $player.setActiveSKUs(%activeSkus);
        commandToServer('SetActiveSkus', %activeSkus);
    }
    if (%atLeastOneOutfitChanged)
    {
        outfits_persist();
    }
    return ;
}
function doTakeFlower(%unused)
{
    error("20080131 OBSOLETE FUNCTION - returning immediately. -" SPC getTrace());
    return ;
}
function doGiveFlower(%unused)
{
    error("20080131 OBSOLETE FUNCTION - returning immediately. -" SPC getTrace());
    return ;
}
function sendGiftRequest(%unused, %skus)
{
    error("20080131 OBSOLETE FUNCTION - returning immediately. -" SPC getTrace());
    return ;
}
