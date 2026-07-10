function SkuManager::addItem(%this, %skunum, %skuType, %rolesMask, %gender, %brand, %drwrName, %meshName, %txtrNames, %descShrt, %descLong, %stores, %bornWith, %price, %avail, %qtyMfr, %rspk, %expireTime_TAB_tags, %author) {
    %expireTime = getField(%expireTime_TAB_tags, 0);
    %tags = getField(%expireTime_TAB_tags, 1);
    %si = new SkuItem("");
    if (!%avail) {
        %rolesMask = 2147483648;
    }
    if (!(%stores $= "")) {
    }
    if (%bornWith) {
    }
    if (!$StandAlone) {
        warn("Wardrobe", getScopeName() @ " " @ "- bornWith sku in a store." @ " " @ %skunum @ " " @ %descShrt @ " " @ %stores);
    }
    if (($ETS::ProjectName $= "vmtv")) {
    }
    if ((%brand $= "myet")) {
        %brand = "";
    }
    if ((%expireTime <= 0.0)) {
        %expireTime = "";
    }
    %usageShrt = "";
    if ((%drwrName $= "props")) {
        %usageShrt = "ctrl-enter to use";
    }
    if ((%descLong $= %descShrt)) {
        %descLong = "";
    }
    %si.skuNumber = %skunum;
    %si.skuType = %skuType;
    %si.drwrName = %drwrName;
    %si.meshName = %meshName;
    %si.setTxtrNames(%txtrNames);
    %si.originalTxtrNames = %txtrNames;
    %si.rolesMask = %rolesMask;
    %si.gender = %gender;
    %si.descShrt = %descShrt;
    %si.brand = %brand;
    %si.descLong = %descLong;
    %si.stores = %stores;
    %si.bornWith = %bornWith;
    %si.price = %price;
    %si.qtyMfr = %qtyMfr;
    %si.qty = %qtyMfr;
    %si.rspk = %rspk;
    %si.expireTime = %expireTime;
    %si.usageShrt = %usageShrt;
    %si.tags = %tags;
    %si.salonStyleIndex = -(1.0);
    %si.author = %author;
    %prev = %this.findBySku(%si.skuNumber);
    if (isObject(%prev)) {
        error("SkuManager::addItem() - duplicate sku." @ " " @ %prev.skuNumber @ " " @ "\"" @ %si.descShrt @ "\"" @ " " @ "loses to" @ " " @ "\"" @ %prev.descShrt @ "\"");
    } else {
        %this.add(%si);
        %n = (getWordCount(%stores) - 1.0);
        while ((%n >= 0.0)) {
            %storeID = getWord(%stores, %n);
            if ($StandAlone) {
                %this.storeSkus[%skunum," ",%storeID] = %this.storeSkus[%storeID];
                %this.storeQtys[%qtyMfr," ",%storeID] = %this.storeQtys[%storeID];
            }
            if ((findWord(%this.storeIDs, %storeID) < 0.0)) {
                %this.storeIDs = %this.storeIDs @ %storeID @ " ";
            }
            %n = (%n - 1.0);
        }
        if (%bornWith) {
            %this.bornWithSkus = (%n >= 0.0) @ %this.bornWithSkus @ %skunum @ " ";
        } else {
            %this.notBornWithSkus = %this.notBornWithSkus @ %skunum @ " ";
        }
        %n = (getWordCount(%tags) - 1.0);
        while ((%n >= 0.0)) {
            %tag = getWord(%tags, %n);
            %skus = %this.skuTags.get(%tag);
            %skus = trim(%skus @ " " @ %skunum);
            %this.skuTags.put(%tag, %skus);
            %n = (%n - 1.0);
        }
        %n = (getWordCount(%meshName) - 1.0);
        (%n >= 0.0);
        while ((%n >= 0.0)) {
            %meshN = getWord(%meshName, %n);
            %this.addKnownMeshName(%meshN);
            %n = (%n - 1.0);
        }
    }
};
function skusAddItem2(%skunum, %skuType, %roleStrings, %gender, %brand, %drwrName, %meshName, %txtrNames, %desc, %descLong, %stores, %bornWith, %price, %avail, %qtyMfr, %rspk, %expireTime, %tags, %author) {
    SkuManager.addItem(%skunum, %skuType, roles::getRolesMaskFromStrings(%roleStrings), %gender, %brand, %drwrName, %meshName, %txtrNames, %desc, %descLong, %stores, %bornWith, %price, %avail, %qtyMfr, %rspk, %expireTime @ "\t" @ %tags, %author);
};
function SkuManager::init(%this) {
    %t1 = getSimTime();
    %this.clear();
    %n = 0;
    while ((%n < getWordCount(%this.storeIDs))) {
        %this.storeSkus[getWord(%this.storeIDs, %n)] = "";
        %n = (%n + 1.0);
    }
    %this.storeIDs = (%n < getWordCount(%this.storeIDs)) @ "";
    %this.bornWithSkus = "";
    %this.notBornWithSkus = "";
    %this.skuTags = safeNewScriptObject("StringMap", "", 0);
    skusInit();
    skusInitFurnishings();
    %this.clearValueCache();
    %this.sanityCheckStockOutfits();
    %t2 = getSimTime();
    %dt = (%t2 - %t1);
    echo(getScopeName() @ " " @ "-" @ " " @ (%dt / 1000.0) @ " " @ "seconds");
};
function SkuManager::sanityCheckStockOutfits(%this) {
    %genders = "m f";
    %outfits = "A B C D E F G H I J K L";
    %g = (getWordCount(%genders) - 1.0);
    while ((%g >= 0.0)) {
        %o = (getWordCount(%outfits) - 1.0);
        while ((%o >= 0.0)) {
            %skus = $gNewStockOutfits[getWord(%genders, %g),getWord(%outfits, %o)];
            %this.sanityCheckSkus(%skus);
            %o = (%o - 1.0);
        }
        %skus = $gDefaultBodyAttrs[getWord(%genders, %g)];
        (%o >= 0.0);
        %this.sanityCheckSkus(%skus);
        %g = (%g - 1.0);
    }
};
function SkuManager::sanityCheckSkus(%this, %skusDry) {
    %skus = SortNumbers(%skusDry);
    %skusRoles = SortNumbers(SkuManager.filterSkusRoles(%skus, 0, 1));
    %skusOwned = SortNumbers(SkuManager.filterSkusBornWith(%skus, 1));
    %badSkusRoles = %this.getMissingSkus(%skus, %skusRoles);
    %badSkusOwned = %this.getMissingSkus(%skus, %skusOwned);
    if (!(%badSkusRoles $= "") || !(%badSkusOwned $= "")) {
        error(getScopeName() @ " " @ "- trouble with these skus:" @ " " @ %skusDry);
    }
    if (!(%badSkusRoles $= "")) {
        error(getScopeName() @ " " @ "- some skus have roles:      " @ " " @ %badSkusRoles);
    }
    if (!(%badSkusOwned $= "")) {
        error(getScopeName() @ " " @ "- some skus are not bornwith:" @ " " @ %badSkusOwned);
    }
};
function SkuManager::getMissingSkus(%this, %skusFull, %skusSubset) {
    %ret = "";
    %n = (getWordCount(%skusFull) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%skusFull, %n);
        if ((findWord(%skusSubset, %sku) < 0.0)) {
            %ret = %ret @ %sku @ " ";
        }
        %n = (%n - 1.0);
    }
    return %ret;
};
function SkuManager::getStoreSkus(%this, %storeID) {
    if (!$StandAlone) {
        error(getScopeName() @ " " @ "- should only be called in standalone.");
    }
    return %this.storeSkus[%storeID];
};
function SkuManager::getStoreQtys(%this, %storeID) {
    if (!$StandAlone) {
        error(getScopeName() @ " " @ "- should only be called in standalone.");
    }
    return %this.storeQtys[%storeID];
};
function SkuManager::getBornWithSkus(%this) {
    return %this.bornWithSkus;
};
function SkuManager::getNotBornWithSkus(%this) {
    return %this.notBornWithSkus;
};
function SkuManager::isDrawerExclusive(%this, %drwr) {
    return !(%this.getTopExclusionLevelForDrawer(%drwr) $= "");
};
function SkuManager::getExclusionDrawerForSku(%this, %sku) {
    %si = %this.findBySku(%sku);
    if (!isObject(%si)) {
        error(getScopeName() @ "->unknown sku, returning empty string. trace = " @ getTrace());
        return "";
    }
    return %this.getTopExclusionLevelForDrawer(%si.drwrName);
};
function SkuManager::getTopExclusionLevelForDrawer(%this, %drwr) {
    %tabbedDrwr = strreplace(%drwr, "/", "\t");
    %cnt = getFieldCount(%tabbedDrwr);
    %incStr = "";
    if ((%this.getFieldValue("exclusiveDrwrs") $= "")) {
        return "";
    }
    %i = 0;
    while ((%i < %cnt)) {
        %folder = getField(%tabbedDrwr, %i);
        %incStr = %incStr @ %folder;
        if (((%idx = findRecord(%this.exclusiveDrwrs, %incStr)) != -(1.0))) {
            return getRecord(%this.exclusiveDrwrs, %idx);
        }
        %incStr = %incStr @ "/";
        %i = (%i + 1.0);
    }
    return "";
};
function SkuManager::setDrawerExclusive(%this, %drwr) {
    if (%this.isDrawerExclusive(%drwr)) {
        warn(getScopeName() @ "-> drawer \"" @ %drwr @ "\" already exclusive or - for furnishing - a subdrawer of an already exclusive drawer.");
    }
    %this.exclusiveDrwrs = %this.exclusiveDrwrs @ %drwr @ "\n";
};
%this[$gNewStockOutfits @ mA] = "400 554 600 701 850 875 900 950";
$gNewStockOutfits[mB] = "403 502 33763 31600 31034";
$gNewStockOutfits[mC] = "33424 32098 906 606 31018";
$gNewStockOutfits[mD] = "33402 32403 31522 31072";
$gNewStockOutfits[mE] = "32404 604 31088";
$gNewStockOutfits[mF] = "33406 32131 649 31048";
$gNewStockOutfits[mG] = "400 500 635 700 850 875 900 950";
$gNewStockOutfits[mH] = "400 525 619 701 850 875 900 950";
$gNewStockOutfits[mI] = "400 604 722 850 875 900 950 504";
$gNewStockOutfits[mJ] = "400 608 702 850 875 900 950 524";
$gNewStockOutfits[mK] = "400 513 619 702 850 875 900 950";
$gNewStockOutfits[mL] = "400 32092 635 722 850 875 900 950";
$gNewStockOutfits[fA] = "5400 5527 5600 5702 5850 5900 5950 5980";
$gNewStockOutfits[fB] = "5522 5876 5901 21519 5702";
$gNewStockOutfits[fC] = "5414 22350 5881 5907 21615 21628 21049";
$gNewStockOutfits[fD] = "5408 5894 22351 15917 21627 21071";
$gNewStockOutfits[fE] = "5863 22352 15918 6107 6108 21602 21068";
$gNewStockOutfits[fF] = "5412 5851 22166 15882 5963 21555 21049";
$gNewStockOutfits[fG] = "5400 5510 5626 5708 5850 5980";
$gNewStockOutfits[fH] = "5400 5519 5600 5702 5850 5900 5950 5980";
$gNewStockOutfits[fI] = "5400 5529 21519 5702 5850 5900 5950 5980 5903";
$gNewStockOutfits[fJ] = "5400 5504 5617 5718 5850 5903 5980";
$gNewStockOutfits[fK] = "5400 5517 5607 5702 5850 5980";
$gNewStockOutfits[fL] = "5400 5528 5617 5714 5850 5980";
$gDefaultBodyAttrs[m] = "121 200 303 801";
$gDefaultBodyAttrs[f] = "5100 5200 5303 5801";
SkuManager.init();
SkuManager.setDrawerExclusive("AV/Videoscreens");
SkuManager.setDrawerExclusive("Activities/Games/PlayAreas");
