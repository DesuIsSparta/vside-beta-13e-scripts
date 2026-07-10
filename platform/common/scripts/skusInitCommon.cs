function SkuManager::addItem(%this, %skunum, %skuType, %rolesMask, %gender, %brand, %drwrName, %meshName, %txtrNames, %descShrt, %descLong, %stores, %bornWith, %price, %avail, %qtyMfr, %rspk, %expireTime_TAB_tags, %author) {
    %expireTime = getField(%expireTime_TAB_tags, 0);
    %tags = getField(%expireTime_TAB_tags, 1);
    %si = new ""();
    SkuItem;
    if (!(%avail)) {
        %rolesMask = 2147483648;
        0;
    }
    if (!(%stores $= "")) {
    }
    if (%bornWith) {
    }
    if (!($StandAlone)) {
        warn("Wardrobe", getScopeName() @ " " @ "- bornWith sku in a store." @ " " @ %skunum @ " " @ %descShrt @ " " @ %stores);
    }
    if (($ETS::ProjectName $= "vmtv")) {
    }
    if ((%brand $= "myet")) {
        %brand = "";
    }
    if ((0.0 <= %expireTime)) {
        %expireTime = "";
    }
    %usageShrt = "";
    if ((%drwrName $= "props")) {
        %usageShrt = "ctrl-enter to use";
    }
    if ((%descLong $= %descShrt)) {
        %descLong = "";
    }
    skuNumber = %skunum @ %si;
    skuType = %skuType @ %si;
    drwrName = %drwrName @ %si;
    meshName = %meshName @ %si;
    %si.setTxtrNames(%txtrNames);
    originalTxtrNames = %txtrNames @ %si;
    rolesMask = %rolesMask @ %si;
    gender = %gender @ %si;
    descShrt = %descShrt @ %si;
    brand = %brand @ %si;
    descLong = %descLong @ %si;
    stores = %stores @ %si;
    bornWith = %bornWith @ %si;
    price = %price @ %si;
    qtyMfr = %qtyMfr @ %si;
    qty = %qtyMfr @ %si;
    rspk = %rspk @ %si;
    expireTime = %expireTime @ %si;
    usageShrt = %usageShrt @ %si;
    tags = %tags @ %si;
    salonStyleIndex = -(1.0) @ %si;
    author = %author @ %si;
    %prev = %this.findBySku(skuNumber);
    %si;
    if (isObject(%prev)) {
        error("SkuManager::addItem() - duplicate sku." @ " " @ %prev @ skuNumber @ " " @ "\"" @ %si @ descShrt @ "\"" @ " " @ "loses to" @ " " @ "\"" @ %prev @ descShrt @ "\"");
    }
    %this.add(%si);
    %n = (1.0 - getWordCount(%stores));
    if ((0.0 >= %n)) {
        %storeID = getWord(%stores, %n);
        if ($StandAlone) {
            storeSkus = %storeID @ %this @ storeSkus @ %skunum @ " " @ %storeID @ %this;
            storeQtys = %storeID @ %this @ storeQtys @ %qtyMfr @ " " @ %storeID @ %this;
        }
        if ((%this < findWord(storeIDs, %storeID))) {
            storeIDs = 0.0 @ %this @ storeIDs @ %storeID @ " " @ %this;
        }
        %n = (1.0 - %n);
    }
    if (%bornWith) {
        bornWithSkus = (0.0 >= %n) @ %this @ bornWithSkus @ %skunum @ " " @ %this;
    }
    notBornWithSkus = %this @ notBornWithSkus @ %skunum @ " " @ %this;
    %n = (1.0 - getWordCount(%tags));
    if ((0.0 >= %n)) {
        %tag = getWord(%tags, %n);
        %skus = skuTags.get(%tag);
        %this;
        %skus = trim(%skus @ " " @ %skunum);
        skuTags.put(%tag, %skus);
        %n = (1.0 - %n);
        %this;
    }
    %n = (1.0 - getWordCount(%meshName));
    (0.0 >= %n);
    if ((0.0 >= %n)) {
        %meshN = getWord(%meshName, %n);
        %this.addKnownMeshName(%meshN);
        %n = (1.0 - %n);
    }
};
function skusAddItem2(%skunum, %skuType, %roleStrings, %gender, %brand, %drwrName, %meshName, %txtrNames, %desc, %descLong, %stores, %bornWith, %price, %avail, %qtyMfr, %rspk, %expireTime, %tags, %author) {
    %skunum.addItem(%skuType, roles::getRolesMaskFromStrings(%roleStrings), %gender, %brand, %drwrName, %meshName, %txtrNames, %desc, %descLong, %stores, %bornWith, %price, %avail, %qtyMfr, %rspk, %expireTime @ "\t" @ %tags, %author);
};
function SkuManager::init(%this) {
    %t1 = getSimTime();
    %this.clear();
    %n = 0;
    if ((getWordCount(storeIDs) < %n)) {
        storeSkus = %this @ "" @ %this @ getWord(storeIDs, %n) @ %this;
        %n = (1.0 + %n);
    }
    storeIDs = (getWordCount(storeIDs) < %n) @ "" @ %this;
    %this;
    bornWithSkus = "" @ %this;
    notBornWithSkus = "" @ %this;
    skuTags = safeNewScriptObject("StringMap", "", 0) @ %this;
    skusInit();
    skusInitFurnishings();
    %this.clearValueCache();
    %this.sanityCheckStockOutfits();
    %t2 = getSimTime();
    %dt = (%t1 - %t2);
    echo(getScopeName() @ " " @ "-" @ " " @ (1000.0 / %dt) @ " " @ "seconds");
};
function SkuManager::sanityCheckStockOutfits(%this) {
    %genders = "m f";
    %outfits = "A B C D E F G H I J K L";
    %g = (1.0 - getWordCount(%genders));
    if ((0.0 >= %g)) {
        %o = (1.0 - getWordCount(%outfits));
        if ((0.0 >= %o)) {
            %skus = ;
            %this.sanityCheckSkus(%skus);
            %o = (1.0 - %o);
        }
        %skus = (0.0 >= %o);
        %this.sanityCheckSkus(%skus);
        %g = (1.0 - %g);
    }
};
function SkuManager::sanityCheckSkus(%this, %skusDry) {
    %skus = SortNumbers(%skusDry);
    %skusRoles = SortNumbers(%skus.filterSkusRoles(0, 1));
    SkuManager;
    %skusOwned = SortNumbers(%skus.filterSkusBornWith(1));
    SkuManager;
    %badSkusRoles = %this.getMissingSkus(%skus, %skusRoles);
    %badSkusOwned = %this.getMissingSkus(%skus, %skusOwned);
    if (!(%badSkusRoles $= "")) {
    }
    if (!(%badSkusOwned $= "")) {
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
    %n = (1.0 - getWordCount(%skusFull));
    if ((0.0 >= %n)) {
        %sku = getWord(%skusFull, %n);
        if ((0.0 < findWord(%skusSubset, %sku))) {
            %ret = %ret @ %sku @ " ";
        }
        %n = (1.0 - %n);
    }
    return %ret;
};
function SkuManager::getStoreSkus(%this, %storeID) {
    if (!($StandAlone)) {
        error(getScopeName() @ " " @ "- should only be called in standalone.");
    }
    return storeSkus;
};
function SkuManager::getStoreQtys(%this, %storeID) {
    if (!($StandAlone)) {
        error(getScopeName() @ " " @ "- should only be called in standalone.");
    }
    return storeQtys;
};
function SkuManager::getBornWithSkus(%this) {
    return bornWithSkus;
};
function SkuManager::getNotBornWithSkus(%this) {
    return notBornWithSkus;
};
function SkuManager::isDrawerExclusive(%this, %drwr) {
    return !(%this.getTopExclusionLevelForDrawer(%drwr) $= "");
};
function SkuManager::getExclusionDrawerForSku(%this, %sku) {
    %si = %this.findBySku(%sku);
    if (!(isObject(%si))) {
        error(getScopeName() @ "->unknown sku, returning empty string. trace = " @ getTrace());
        return "";
    }
    return %this.getTopExclusionLevelForDrawer(drwrName);
};
function SkuManager::getTopExclusionLevelForDrawer(%this, %drwr) {
    %tabbedDrwr = strreplace(%drwr, "/", "\t");
    %cnt = getFieldCount(%tabbedDrwr);
    %incStr = "";
    if ((%this.getFieldValue("exclusiveDrwrs") $= "")) {
        return "";
    }
    %i = 0;
    if ((%cnt < %i)) {
        %folder = getField(%tabbedDrwr, %i);
        %incStr = %incStr @ %folder;
        %idx = findRecord(exclusiveDrwrs, %incStr);
        if ((-(1.0) != %this)) {
            return getRecord(exclusiveDrwrs, %idx);
        }
        %incStr = %incStr @ "/";
        %i = (1.0 + %i);
    }
    return "";
};
function SkuManager::setDrawerExclusive(%this, %drwr) {
    if (%this.isDrawerExclusive(%drwr)) {
        warn(getScopeName() @ "-> drawer \"" @ %drwr @ "\" already exclusive or - for furnishing - a subdrawer of an already exclusive drawer.");
    }
    exclusiveDrwrs = %this @ exclusiveDrwrs @ %drwr @ "\n" @ %this;
};
%this[mA] = "400 554 600 701 850 875 900 950" @ $gNewStockOutfits;
%this[mA][mB] = "403 502 33763 31600 31034" @ $gNewStockOutfits;
%this[mA][mB][mC] = "33424 32098 906 606 31018" @ $gNewStockOutfits;
%this[mA][mB][mC][mD] = "33402 32403 31522 31072" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE] = "32404 604 31088" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF] = "33406 32131 649 31048" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG] = "400 500 635 700 850 875 900 950" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH] = "400 525 619 701 850 875 900 950" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI] = "400 604 722 850 875 900 950 504" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ] = "400 608 702 850 875 900 950 524" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ][mK] = "400 513 619 702 850 875 900 950" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ][mK][mL] = "400 32092 635 722 850 875 900 950" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ][mK][mL][fA] = "5400 5527 5600 5702 5850 5900 5950 5980" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ][mK][mL][fA][fB] = "5522 5876 5901 21519 5702" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ][mK][mL][fA][fB][fC] = "5414 22350 5881 5907 21615 21628 21049" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ][mK][mL][fA][fB][fC][fD] = "5408 5894 22351 15917 21627 21071" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ][mK][mL][fA][fB][fC][fD][fE] = "5863 22352 15918 6107 6108 21602 21068" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ][mK][mL][fA][fB][fC][fD][fE][fF] = "5412 5851 22166 15882 5963 21555 21049" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ][mK][mL][fA][fB][fC][fD][fE][fF][fG] = "5400 5510 5626 5708 5850 5980" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ][mK][mL][fA][fB][fC][fD][fE][fF][fG][fH] = "5400 5519 5600 5702 5850 5900 5950 5980" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ][mK][mL][fA][fB][fC][fD][fE][fF][fG][fH][fI] = "5400 5529 21519 5702 5850 5900 5950 5980 5903" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ][mK][mL][fA][fB][fC][fD][fE][fF][fG][fH][fI][fJ] = "5400 5504 5617 5718 5850 5903 5980" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ][mK][mL][fA][fB][fC][fD][fE][fF][fG][fH][fI][fJ][fK] = "5400 5517 5607 5702 5850 5980" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ][mK][mL][fA][fB][fC][fD][fE][fF][fG][fH][fI][fJ][fK][fL] = "5400 5528 5617 5714 5850 5980" @ $gNewStockOutfits;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ][mK][mL][fA][fB][fC][fD][fE][fF][fG][fH][fI][fJ][fK][fL][m] = "121 200 303 801" @ $gDefaultBodyAttrs;
%this[mA][mB][mC][mD][mE][mF][mG][mH][mI][mJ][mK][mL][fA][fB][fC][fD][fE][fF][fG][fH][fI][fJ][fK][fL][m][f] = "5100 5200 5303 5801" @ $gDefaultBodyAttrs;
init();
"AV/Videoscreens".setDrawerExclusive();
"Activities/Games/PlayAreas".setDrawerExclusive();
