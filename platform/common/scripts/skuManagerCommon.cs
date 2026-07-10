function DoesPlayerHaveItemActive(%player, %skulist) {
    if ((%skulist $= "")) {
        return 1;
    }
    %num = getWordCount(%skulist);
    %n = 0;
    while ((%n < %num)) {
        %sku = getWord(%skulist, %n);
        %hasIt = %sku.hasActiveSKU(%player);
        if (%hasIt) {
            return 1;
        }
        %n = (%n + 1.0);
    }
    return 0;
};
function SkuManager::vetByRoles(%this, %rolesMask, %skusDry, %skusDefault) {
    %skusWet = %rolesMask.filterSkusRoles(%this, %skusDry);
    %skusWet = %skusWet.overlaySkus(%this, %skusDefault);
    return %skusWet;
};
function SkuManager::vetSkus(%this, %rolesMask, %gender, %skusDry, %skusDefault) {
    %skusWet = %rolesMask.filterSkusRoles(%this, %skusDry);
    %skusWet = %gender.filterSkusGender(%this, %skusWet);
    %skusWet = %skusWet.overlaySkus(%this, %skusDefault);
    return %skusWet;
};
function SkuManager::clearValueCache(%this) {
    if (isObject(%this.valueCache)) {
        %this.valueCache.delete();
    }
    %this.valueCache = safeNewScriptObject("StringMap", "", 0);
};
SkuManager.clearValueCache();
"hair".addBodyDrawer(SkuManager);
"face".addBodyDrawer(SkuManager);
"faceb".addBodyDrawer(SkuManager);
"eyes".addBodyDrawer(SkuManager);
"skin".addBodyDrawer(SkuManager);
"glasses".addOutfitDrawer(SkuManager);
"torso".addOutfitDrawer(SkuManager);
"torsob".addOutfitDrawer(SkuManager);
"legs".addOutfitDrawer(SkuManager);
"legsb".addOutfitDrawer(SkuManager);
"feet".addOutfitDrawer(SkuManager);
"ear".addOutfitDrawer(SkuManager);
"neck".addOutfitDrawer(SkuManager);
"neckb".addOutfitDrawer(SkuManager);
"neckc".addOutfitDrawer(SkuManager);
"wristleft".addOutfitDrawer(SkuManager);
"wristleftb".addOutfitDrawer(SkuManager);
"wristright".addOutfitDrawer(SkuManager);
"wristrightb".addOutfitDrawer(SkuManager);
"fingerleft".addOutfitDrawer(SkuManager);
"fingerright".addOutfitDrawer(SkuManager);
"toeleft".addOutfitDrawer(SkuManager);
"toeright".addOutfitDrawer(SkuManager);
"purse".addOutfitDrawer(SkuManager);
"waist".addOutfitDrawer(SkuManager);
"waistb".addOutfitDrawer(SkuManager);
"back".addOutfitDrawer(SkuManager);
"hat".addOutfitDrawer(SkuManager);
"mask".addOutfitDrawer(SkuManager);
"earl".addOutfitDrawer(SkuManager);
"labret".addOutfitDrawer(SkuManager);
"lftauricle".addOutfitDrawer(SkuManager);
"lftconch".addOutfitDrawer(SkuManager);
"lfteyebrow".addOutfitDrawer(SkuManager);
"lftlobe".addOutfitDrawer(SkuManager);
"lftorbital".addOutfitDrawer(SkuManager);
"lftpinna".addOutfitDrawer(SkuManager);
"lftrook".addOutfitDrawer(SkuManager);
"lfttragus".addOutfitDrawer(SkuManager);
"rghauricle".addOutfitDrawer(SkuManager);
"rghconch".addOutfitDrawer(SkuManager);
"rgheyebrow".addOutfitDrawer(SkuManager);
"rghlobe".addOutfitDrawer(SkuManager);
"rghorbital".addOutfitDrawer(SkuManager);
"rghpinna".addOutfitDrawer(SkuManager);
"rghrook".addOutfitDrawer(SkuManager);
"rghtragus".addOutfitDrawer(SkuManager);
"lowlip".addOutfitDrawer(SkuManager);
"madonna".addOutfitDrawer(SkuManager);
"medusa".addOutfitDrawer(SkuManager);
"nostril".addOutfitDrawer(SkuManager);
"septum".addOutfitDrawer(SkuManager);
"tail".addOutfitDrawer(SkuManager);
"chest".addOutfitDrawer(SkuManager);
"props".addOutfitDrawer(SkuManager);
"badges".addOutfitDrawer(SkuManager);
"tokens".addOutfitDrawer(SkuManager);
"gameplay".addOutfitDrawer(SkuManager);
"deprecated_gl".addOutfitDrawer(SkuManager);
"deprecated_ea".addOutfitDrawer(SkuManager);
"deprecated_ne".addOutfitDrawer(SkuManager);
"deprecated_wl".addOutfitDrawer(SkuManager);
"deprecated_wr".addOutfitDrawer(SkuManager);
"deprecated_wa".addOutfitDrawer(SkuManager);
"microphone".addOutfitDrawer(SkuManager);
"faceb".addOptionalDrawer(SkuManager);
"glasses".addOptionalDrawer(SkuManager);
"torsob".addOptionalDrawer(SkuManager);
"legsb".addOptionalDrawer(SkuManager);
"ear".addOptionalDrawer(SkuManager);
"neck".addOptionalDrawer(SkuManager);
"neckb".addOptionalDrawer(SkuManager);
"neckc".addOptionalDrawer(SkuManager);
"wristleft".addOptionalDrawer(SkuManager);
"wristleftb".addOptionalDrawer(SkuManager);
"wristright".addOptionalDrawer(SkuManager);
"wristrightb".addOptionalDrawer(SkuManager);
"fingerleft".addOptionalDrawer(SkuManager);
"fingerright".addOptionalDrawer(SkuManager);
"toeleft".addOptionalDrawer(SkuManager);
"toeright".addOptionalDrawer(SkuManager);
"purse".addOptionalDrawer(SkuManager);
"waist".addOptionalDrawer(SkuManager);
"waistb".addOptionalDrawer(SkuManager);
"back".addOptionalDrawer(SkuManager);
"hat".addOptionalDrawer(SkuManager);
"mask".addOptionalDrawer(SkuManager);
"earl".addOptionalDrawer(SkuManager);
"labret".addOptionalDrawer(SkuManager);
"lftauricle".addOptionalDrawer(SkuManager);
"lftconch".addOptionalDrawer(SkuManager);
"lfteyebrow".addOptionalDrawer(SkuManager);
"lftlobe".addOptionalDrawer(SkuManager);
"lftorbital".addOptionalDrawer(SkuManager);
"lftpinna".addOptionalDrawer(SkuManager);
"lftrook".addOptionalDrawer(SkuManager);
"lfttragus".addOptionalDrawer(SkuManager);
"rghauricle".addOptionalDrawer(SkuManager);
"rghconch".addOptionalDrawer(SkuManager);
"rgheyebrow".addOptionalDrawer(SkuManager);
"rghlobe".addOptionalDrawer(SkuManager);
"rghorbital".addOptionalDrawer(SkuManager);
"rghpinna".addOptionalDrawer(SkuManager);
"rghrook".addOptionalDrawer(SkuManager);
"rghtragus".addOptionalDrawer(SkuManager);
"lowlip".addOptionalDrawer(SkuManager);
"madonna".addOptionalDrawer(SkuManager);
"medusa".addOptionalDrawer(SkuManager);
"nostril".addOptionalDrawer(SkuManager);
"septum".addOptionalDrawer(SkuManager);
"tail".addOptionalDrawer(SkuManager);
"chest".addOptionalDrawer(SkuManager);
"props".addOptionalDrawer(SkuManager);
"badges".addOptionalDrawer(SkuManager);
"tokens".addOptionalDrawer(SkuManager);
"gameplay".addOptionalDrawer(SkuManager);
"deprecated_gl".addOptionalDrawer(SkuManager);
"deprecated_ea".addOptionalDrawer(SkuManager);
"deprecated_ne".addOptionalDrawer(SkuManager);
"deprecated_wl".addOptionalDrawer(SkuManager);
"deprecated_wr".addOptionalDrawer(SkuManager);
"deprecated_wa".addOptionalDrawer(SkuManager);
"microphone".addOptionalDrawer(SkuManager);
"mesh".addWearableSkuType(SkuManager);
"badge".addWearableSkuType(SkuManager);
function SkuManager::allDrawers(%this) {
    %allDrawers = %this.allClosetDrawers() @ " " @ "gameplay";
    return %allDrawers;
};
function SkuManager::allClosetDrawers(%this) {
    return "hair face faceb earl labret lftauricle lftconch lfteyebrow lftlobe lftorbital lftpinna lftrook lfttragus rghauricle rghconch rgheyebrow rghlobe rghorbital rghpinna rghrook rghtragus lowlip madonna medusa nostril septum eyes glasses torso torsob legs legsb feet skin ear neck neckb neckc chest wristleft wristleftb wristright wristrightb fingerleft fingerright toeleft toeright waist waistb purse back hat mask tail props badges";
};
function SkuManager::commonDrawers(%this) {
    return "hair face faceb earl labret lftauricle lftconch lfteyebrow lftlobe lftorbital lftpinna lftrook lfttragus rghauricle rghconch rgheyebrow rghlobe rghorbital rghpinna rghrook rghtragus lowlip madonna medusa nostril septum eyes glasses torso torsob legs legsb feet skin ear neck neckb neckc chest wristleft wristleftb wristright wristrightb fingerleft fingerright toeleft toeright purse hat props badges";
};
%this.userFacingDrawerNamesNum = 0 @ SkuManager;
function SkuManager::addUserFacingDrawerName(%this, %internalName, %userFacingName) {
    %paddedNum = formatInt("%0.5d", %this.userFacingDrawerNamesNum);
    %this.userFacingDrawerName = %paddedNum @ "\t" @ %internalName @ "\t" @ %userFacingName @ %internalName;
    %this.userFacingDrawerNamesNum = (%this.userFacingDrawerNamesNum + 1.0);
};
"Top".addUserFacingDrawerName(SkuManager, "torso");
"Top".addUserFacingDrawerName(SkuManager, "torsob");
"Bottom".addUserFacingDrawerName(SkuManager, "legs");
"Bottom".addUserFacingDrawerName(SkuManager, "legsb");
"Feet".addUserFacingDrawerName(SkuManager, "feet");
"Neck".addUserFacingDrawerName(SkuManager, "neck");
"Neck".addUserFacingDrawerName(SkuManager, "neckb");
"Neck".addUserFacingDrawerName(SkuManager, "neckc");
"Glasses".addUserFacingDrawerName(SkuManager, "glasses");
"Ear".addUserFacingDrawerName(SkuManager, "ear");
"Waist".addUserFacingDrawerName(SkuManager, "waist");
"Waist".addUserFacingDrawerName(SkuManager, "waistb");
"Left Hand".addUserFacingDrawerName(SkuManager, "wristleft");
"Left Hand".addUserFacingDrawerName(SkuManager, "wristleftb");
"Right Hand".addUserFacingDrawerName(SkuManager, "wristright");
"Right Hand".addUserFacingDrawerName(SkuManager, "wristrightb");
"Left Fingers".addUserFacingDrawerName(SkuManager, "fingerleft");
"Right Finger".addUserFacingDrawerName(SkuManager, "fingerright");
"Left Toes".addUserFacingDrawerName(SkuManager, "toeleft");
"Right toes".addUserFacingDrawerName(SkuManager, "toeright");
"Chest".addUserFacingDrawerName(SkuManager, "chest");
"Hat".addUserFacingDrawerName(SkuManager, "hat");
"Mask".addUserFacingDrawerName(SkuManager, "mask");
"Purse".addUserFacingDrawerName(SkuManager, "purse");
"Back".addUserFacingDrawerName(SkuManager, "back");
"Tail".addUserFacingDrawerName(SkuManager, "tail");
"Badge".addUserFacingDrawerName(SkuManager, "badges");
"Eyes".addUserFacingDrawerName(SkuManager, "eyes");
"Face".addUserFacingDrawerName(SkuManager, "face");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "faceb");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "earl");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "labret");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "lftauricle");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "lftconch");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "lfteyebrow");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "lftlobe");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "lftorbital");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "lftpinna");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "lftrook");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "lfttragus");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "rghauricle");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "rghconch");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "rgheyebrow");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "rghlobe");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "rghorbital");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "rghpinna");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "rghrook");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "rghtragus");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "lowlip");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "madonna");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "medusa");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "nostril");
"Facial Enhancement".addUserFacingDrawerName(SkuManager, "septum");
"Hair".addUserFacingDrawerName(SkuManager, "hair");
"Skin".addUserFacingDrawerName(SkuManager, "skin");
"Props".addUserFacingDrawerName(SkuManager, "props");
"Token".addUserFacingDrawerName(SkuManager, "tokens");
"Ceiling".addUserFacingDrawerName(SkuManager, "ceiling");
"Wall".addUserFacingDrawerName(SkuManager, "wall");
"Fixtures".addUserFacingDrawerName(SkuManager, "fixtures");
"Floor".addUserFacingDrawerName(SkuManager, "floor");
"General".addUserFacingDrawerName(SkuManager, "general");
"".addUserFacingDrawerName(SkuManager, "deprecated_ea");
"".addUserFacingDrawerName(SkuManager, "deprecated_gl");
"".addUserFacingDrawerName(SkuManager, "deprecated_ne");
"".addUserFacingDrawerName(SkuManager, "deprecated_wa");
"".addUserFacingDrawerName(SkuManager, "deprecated_wl");
"".addUserFacingDrawerName(SkuManager, "deprecated_wr");
function SkuManager::sortSkusByDrawer(%this, %drySkuList) {
    %sortableList = "";
    %n = (getWordCount(%drySkuList) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%drySkuList, %n);
        %thing = %this.userFacingDrawerName;
        %sku.findBySku(SkuManager).drwrName;
        %sortableList = %sortableList @ %thing @ "\t" @ %sku @ "\n";
        %n = (%n - 1.0);
    }
    %sortableList = trim(%sortableList);
    (%n >= 0.0);
    %sortedList = SortRecords(%sortableList);
    %wetSkuList = "";
    %n = (getWordCount(%drySkuList) - 1.0);
    while ((%n >= 0.0)) {
        %thing = getRecord(%sortedList, %n);
        %wetSkuList = getField(%thing, 3) @ " " @ %wetSkuList;
        %n = (%n - 1.0);
    }
    %wetSkuList = trim(%wetSkuList);
    (%n >= 0.0);
    return %wetSkuList;
};
function SkuManager::getUserFacingDrawerName(%this, %internalDrawerName) {
    %val = %this.userFacingDrawerName;
    %internalDrawerName;
    if ((%val $= "")) {
        error(getScopeName() @ " " @ "- unknown drawer name:" @ " " @ %internalDrawerName @ " " @ getTrace());
    }
    return getField(%val, 2);
};
function SkuManager::getUserFacingDrawerNameFromSku(%this, %skunum) {
    %si = %skunum.findBySku(%this);
    if (!(isObject(%si))) {
        return "";
    }
    return %si.drwrName.getUserFacingDrawerName(%this);
};
function SkuItem::getUserFacingDrawerName(%this) {
    return %this.drwrName.getUserFacingDrawerName(SkuManager);
};
function SkuItem::getDescLong(%this) {
    if ((%this.descLong $= "")) {
    }
    %ret = %this.descLong;
    %this.descShrt;
    return %ret;
};
$gSwatchableSkuDrawers = "BuildingBlocks";
function SkuItem::isSwatchable(%this) {
    %drawers = strreplace(%this.drwrName, "/", "\t");
    %n = (getFieldCount(%drawers) - 1.0);
    while ((%n >= 0.0)) {
        %drawer = getField(%drawers, %n);
        if (hasField($gSwatchableSkuDrawers, %drawer)) {
            return 1;
        }
        %n = (%n - 1.0);
    }
    return 0;
};
function SkuManager::isSwatchableSku(%this, %skunum) {
    return %skunum.findBySku(%this).isSwatchable();
};
function SkuManager::filterSkusDescription(%this, %skus, %userFilterText) {
    %userFilterText = strlwr(%userFilterText);
    %userFilterText = trim(%userFilterText);
    if ((%userFilterText $= "")) {
        return %skus;
    }
    %wet = "";
    %n = (getWordCount(%skus) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%skus, %n);
        %si = %sku.findBySku(%this);
        if ((strstr(%si.searchText, %userFilterText) >= 0.0)) {
            %wet = %sku @ " " @ %wet;
        }
        %n = (%n - 1.0);
    }
    %wet = trim(%wet);
    (%n >= 0.0);
    return %wet;
};
function SkuManager::buildSkusSearchText(%this) {
    safeEnsureScriptObject("StringMap", "gReverseThumbCategories", 0);
    %n = (ThumbCategories.size() - 1.0);
    while ((%n >= 0.0)) {
        %key = %n.getKey(ThumbCategories);
        %val = %n.getValue(ThumbCategories);
        %m = (getWordCount(%val) - 1.0);
        while ((%m >= 0.0)) {
            %drwr = getWord(%val, %m);
            %cats = %drwr.get(gReverseThumbCategories);
            %cats = %cats @ " " @ %key;
            %cats.put(gReverseThumbCategories, %drwr);
            %m = (%m - 1.0);
        }
        %n = (%n - 1.0);
        (%m >= 0.0);
    }
    %n = (%this.getCount() - 1.0);
    (%n >= 0.0);
    while ((%n >= 0.0)) {
        %si = %n.getObject(%this);
        %st = "";
        %st = %st @ "\t" @ %si.descLong;
        %st = %st @ "\t" @ %si.descShrt;
        %st = %st @ "\t" @ %si.tags;
        %st = %st @ "\t" @ %si.brand;
        %st = %st @ "\t" @ %si.drwrName;
        %st = %st @ "\t" @ %si.drwrName.get(gReverseThumbCategories);
        %st = %st @ "\t" @ %si.skuNumber;
        %st = %st @ "\t" @ %si.author;
        %st = strlwr(%st);
        %st = trim(%st);
        %si.searchText = %st;
        %n = (%n - 1.0);
    }
};
function SkuManager::filterSkusInList(%this, %skusDry, %list) {
    %skusWet = "";
    %sep = "";
    %num = getWordCount(%skusDry);
    %n = 0;
    while ((%n < %num)) {
        %sku = getWord(%skusDry, %n);
        if (%sku.skuListHasSku(%this, %list)) {
            %skusWet = %skusWet @ %sep @ %sku;
            %sep = " ";
        }
        %n = (%n + 1.0);
    }
    return %skusWet;
};
function SkuManager::filterSkusVisible(%this, %skusDry, %unused) {
    %skusWet = %skusDry;
    return %skusWet;
};
function SkuManager::getRandomSkusFromList(%this, %skulist, %drawersList) {
    %skus = "";
    %delim = "";
    %n = (getWordCount(%drawersList) - 1.0);
    while ((%n >= 0.0)) {
        %drwrName = getWord(%drawersList, %n);
        %drwrSkus = %drwrName.filterSkusDrwr(%this, %skulist);
        %numSkus = getWordCount(%drwrSkus);
        if ((%numSkus > 0.0)) {
            %sku = getWord(%drwrSkus, getRandom(0, (%numSkus - 1.0)));
            %skus = %skus @ %delim @ %sku;
            %delim = " ";
        }
        %n = (%n - 1.0);
    }
    return %skus;
};
function SkuManager::getRandomSkus(%this, %player, %drawersList) {
    %skus = "";
    %n = (getWordCount(%drawersList) - 1.0);
    while ((%n >= 0.0)) {
        %drwrName = getWord(%drawersList, %n);
        %drwrSkus = %drwrName.getSkusDrwr(SkuManager);
        %drwrSkus = %player.getGender().filterSkusGender(SkuManager, %drwrSkus);
        %drwrSkus = %player.getRolesMask().filterSkusRoles(SkuManager, %drwrSkus);
        %numSkus = getWordCount(%drwrSkus);
        if ((%numSkus < 1.0)) {
            log("wardrobe", "error", "Closet::getRandomSkus() - no skus in drawer" @ " " @ %drwrName @ " " @ getDebugString(%player));
        }
        %skus = %skus @ getWord(%drwrSkus, getRandom(0, (%numSkus - 1.0))) @ " ";
        %n = (%n - 1.0);
    }
    return %skus;
};
function SkuManager::getRandomSku(%this, %player, %drawersList) {
    %sku = "";
    %rnd = getRandom(0, (getWordCount(%drawersList) - 1.0));
    %drwrName = getWord(%drawersList, %rnd);
    %drwrSkus = %drwrName.getSkusDrwr(SkuManager);
    %drwrSkus = %player.getGender().filterSkusGender(SkuManager, %drwrSkus);
    %drwrSkus = %player.getRolesMask().filterSkusRoles(SkuManager, %drwrSkus);
    %numSkus = getWordCount(%drwrSkus);
    if ((%numSkus < 1.0)) {
        log("wardrobe", "error", "Closet::getRandomSku() - no skus in drawer" @ " " @ %drwrName @ " " @ getDebugString(%player));
    }
    %sku = getWord(%drwrSkus, getRandom(0, (%numSkus - 1.0))) @ " ";
    return %sku;
};
function SkuManager::skuListHasSku(%this, %list, %sku) {
    return (findWord(%list, %sku) >= 0.0);
};
function SkuManager::getSkuShortDescriptions(%this, %skus, %delimiter, %includeUsage, %thumbnailWidth) {
    if (isDefined("%thumbnailSize")) {
    }
    %thumbnailSize = 0;
    %thumbnailSize;
    %ret = "";
    %delim = "";
    %n = (getWordCount(%skus) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%skus, %n);
        %si = %sku.findBySku(%this);
        if (!(isObject(%si))) {
            error(getScopeName() @ " " @ "- unknown sku:" @ " " @ %sku);
        }
        %usage = "";
        if (%includeUsage) {
        }
        if (!(%si.usageShrt $= "")) {
            %usage = " -" @ " " @ %si.usageShrt;
        }
        %thumbnailText = "";
        if ((%thumbnailWidth > 0.0)) {
        }
        if ((%si.skuType $= "furnishing")) {
            %thumbnailImage = CSBrowser::getThumbnailPathForSku(0, %sku, 32);
            %thumbnailText = " <bitmap:" @ %thumbnailImage @ ":true:middle:width=" @ %thumbnailWidth @ ">";
        }
        %ret = %ret @ %delim @ %si.descShrt @ %thumbnailText @ %usage;
        %delim = %delimiter;
        %n = (%n - 1.0);
    }
    return %ret;
};
function SkuManager::dumpSkuList(%this, %skus) {
    %skus = SortNumbers(%skus);
    %line = "sku num";
    %line = drawer;
    %line @ " - ";
    %line = desc;
    %line @ " - ";
    %line = mesh;
    %line @ " - ";
    %line = textures;
    %line @ " - ";
    %line = roles;
    %line @ " - ";
    echo("wardrobe", %line);
    %num = getWordCount(%skus);
    %n = 0;
    while ((%n < %num)) {
        %sn = getWord(%skus, %n);
        %si = %sn.findBySku(%this);
        if (!(isObject(%si))) {
            error("wardrobe", "dumpSkuList: unknown sku" @ " " @ %sn);
        }
        %line = %sn;
        %line = %line @ " - " @ %si.drwrName;
        %line = %line @ " - " @ %si.descShrt;
        %line = %line @ " - " @ %si.meshName;
        %line = %line @ " - " @ %si.getTxtrNames();
        %line = %line @ " - " @ roles::getRoleStrings(%si.rolesMask);
        echo("wardrobe", %line);
        %n = (%n + 1.0);
    }
};
function Player::dumpActiveSkus(%this) {
    %skus = %this.getActiveSKUs();
    %skus.dumpSkuList(SkuManager);
};
function SkuManager::setSkuPair(%this, %list, %first, %second) {
    %ndx = findWord(%list, %first);
    if ((%ndx < 0.0)) {
        if ((%second > 0.0)) {
            %list = %list @ " " @ %first @ " " @ %second;
        }
    }
    if ((%second > 0.0)) {
        %list = setWord(%list, (%ndx + 1.0), %second);
    }
    %list = removeWord(removeWord(%list, (%ndx + 1.0)), %ndx);
    return %list;
};
function SkuManager::skusRemove(%this, %listA, %listB) {
    %num = getWordCount(%listB);
    %n = 0;
    while ((%n < %num)) {
        %sku = getWord(%listB, %n);
        %listA = findAndRemoveAllOccurrencesOfWord(%listA, %sku);
        %n = (%n + 1.0);
    }
    return %listA;
};
function SkuManager::addSkuTags(%this, %sku, %tags) {
    %si = %sku.findBySku(%this);
    if (!(isObject(%si))) {
        error(getScopeName() @ " " @ "- no such sku:" @ " " @ %sku @ " " @ %tags @ " " @ getTrace());
        return;
    }
    %si.tags = mergeWords(%si.tags, %tags);
    %n = (getWordCount(%tags) - 1.0);
    while ((%n >= 0.0)) {
        %tag = getWord(%tags, %n);
        %skus = %tag.get(%this.skuTags);
        %skus = trim(%skus @ " " @ %sku);
        %skus.put(%this.skuTags, %tag);
        %n = (%n - 1.0);
    }
};
function SkuManager::getSkuTags(%this, %sku) {
    return %sku.findBySku(%this).tags;
};
function SkuManager::filterSkusAnyTags(%this, %skus, %tags) {
    %ret = "";
    %n = (getWordCount(%skus) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%skus, %n);
        %skuTags = %sku.getSkuTags(%this);
        %num = numWordsInWords(%skuTags, %tags);
        if ((%num > 0.0)) {
            %ret = %ret @ " " @ %sku;
        }
        %n = (%n - 1.0);
    }
    %ret = trim(%ret);
    (%n >= 0.0);
    return %ret;
};
function SkuManager::filterSkusTag(%this, %skus, %tag) {
    %ret = "";
    %n = (getWordCount(%skus) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%skus, %n);
        %skuTags = %sku.getSkuTags(%this);
        if (hasWord(%skuTags, %tag)) {
            %ret = %ret @ " " @ %sku;
        }
        %n = (%n - 1.0);
    }
    %ret = trim(%ret);
    (%n >= 0.0);
    return %ret;
};
function SkuManager::getSkuWithAnyTags(%this, %skus, %tags) {
    %n = (getWordCount(%skus) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%skus, %n);
        %skuTags = %sku.getSkuTags(%this);
        %num = numWordsInWords(%skuTags, %tags);
        if ((%num > 0.0)) {
            return %sku;
        }
        %n = (%n - 1.0);
    }
    return "";
};
function SkuManager::hasSkuWithAnyTags(%this, %skus, %tags) {
    return !(%tags.getSkuWithAnyTags(%this, %skus) $= "");
};
function SkuManager::hasSkuWithTag(%this, %skus, %tags) {
    return %tags.hasSkuWithAnyTags(%this, %skus);
};
function SkuManager::filterSkusAllTags(%this, %skus, %tags) {
    %ret = "";
    %numTags = getWordCount(%tags);
    %n = (getWordCount(%skus) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%skus, %n);
        %skuTags = %sku.getSkuTags(%this);
        %num = numWordsInWords(%skuTags, %tags);
        if ((%num == %numTags)) {
            %ret = %ret @ " " @ %sku;
        }
        %n = (%n - 1.0);
    }
    %ret = trim(%ret);
    (%n >= 0.0);
    return %ret;
};
function SkuManager::getSkuWithAllTags(%this, %skus, %tags) {
    %numTags = getWordCount(%tags);
    %n = (getWordCount(%skus) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%skus, %n);
        %skuTags = %sku.getSkuTags(%this);
        %num = numWordsInWords(%skuTags, %tags);
        if ((%num == %numTags)) {
            return %sku;
        }
        %n = (%n - 1.0);
    }
    return "";
};
function SkuManager::hasSkuWithAllTags(%this, %skus, %tags) {
    return !(%tags.getSkuWithAllTags(%this, %skus) $= "");
};
function SkuManager::dumpSkuTags(%this) {
    %this.skuTags.dumpValues();
};
function SkuManager::getSkusTag(%this, %tag, %gender) {
    %key = "skuTag_" @ %gender @ "_" @ %tag;
    if (!(%key.hasKey(%this.valueCache))) {
        %skus = %gender.getSkusGender(%this);
        %skus = %tag.filterSkusTag(%this, %skus);
        %skus.put(%this.valueCache, %key);
    }
    return %key.get(%this.valueCache);
};
function SkuManager::filterSkusDrwrs(%this, %skus, %drwrs) {
    %ret = "";
    %n = (getWordCount(%skus) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%skus, %n);
        %si = %sku.findBySku(%this);
        if (hasWord(%drwrs, %si.drwrName)) {
            %ret = %ret @ " " @ %sku;
        }
        %n = (%n - 1.0);
    }
    %ret = trim(%ret);
    (%n >= 0.0);
    return %ret;
};
function SkuManager::filterSkusStore(%this, %skus, %storename) {
    %ret = "";
    %n = (getWordCount(%skus) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%skus, %n);
        %si = %sku.findBySku(%this);
        if (hasWord(%si.stores, %storename)) {
            %ret = %ret @ " " @ %sku;
        }
        %n = (%n - 1.0);
    }
    %ret = trim(%ret);
    (%n >= 0.0);
    return %ret;
};
function SkuManager::getPropSkus(%this, %skulist) {
    %propSkus = "props".filterSkusDrwr(%this, %skulist);
    return trim(%propSkus);
};
function SkuManager::getFirstPropSku(%this, %skus) {
    %propSkus = %skus.getPropSkus(%this);
    %propSku = firstWord(%propSkus);
    return %propSku;
};
function SkuManager::hasPropSku(%this, %skus) {
    return !(%skus.getPropSkus(%this) $= "");
};
function Player::hasPropActive(%this) {
    return %this.getActiveSKUs().hasPropSku(SkuManager);
};
function Player::EnsureActiveSkus(%this, %skus) {
    %activeSkus = %this.getActiveSKUs();
    %n = (getWordCount(%skus) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%skus, %n);
        if (!(hasWord(%activeSkus, %sku))) {
            %currentSkus = %activeSkus @ " " @ %sku;
        }
        %n = (%n - 1.0);
    }
    %currentSkus = trim(%activeSkus);
    (%n >= 0.0);
    %activeSkus.setActiveSKUs(%this);
};
function SkuItem::hasTag(%this, %tag) {
    return hasWord(%this.tags, %tag);
};
function SkuItem::replaceTextureName(%this, %newTextureName) {
    %explode = strreplace(%newTextureName, ".", "\t");
    %base = removeField(%explode, 0);
    %oldTextures = %this.getTxtrNames();
    %num = getWordCount(%oldTextures);
    %newTextures = "";
    %n = 0;
    while ((%n < %num)) {
        %texture = getWord(%oldTextures, %n);
        %explode = strreplace(%texture, ".", "\t");
        %explode = removeField(%explode, 0);
        if ((%explode $= %base)) {
            %texture = %newTextureName;
        }
        %newTextures = %newTextures @ " " @ %texture;
        %n = (%n + 1.0);
    }
    %newTextures = trim(%newTextures);
    (%n < %num);
    %newTextures.setTxtrNames(%this);
};
function SkuManager::findTemplateSku(%this, %sku) {
    %si = %sku.findBySku(%this);
    %candidates = %si.drwrName.getSkusDrwr(%this);
    %candidates = %si.gender.filterSkusGender(%this, %candidates);
    %candidates = "TEMPLATE".filterSkusTag(%this, %candidates);
    %candidates = findAndRemoveFirstOccurrenceOfWord(%candidates, %sku);
    %found = "";
    %n = (getWordCount(%candidates) - 1.0);
    if ((%n >= 0.0)) {
    }
    while ((%found $= "")) {
        %candidateSku = getWord(%candidates, %n);
        %candidateSI = %candidateSku.findBySku(%this);
        if ((%si.meshName $= %candidateSI.meshName)) {
            %found = %candidateSku;
        }
        %n = (%n - 1.0);
        if ((%n >= 0.0)) {
        }
    }
    return %found;
};
