function DoesPlayerHaveItemActive(%player, %skulist) {
    if ((%skulist $= "")) {
        return 1;
    }
    %num = getWordCount(%skulist);
    %n = 0;
    if ((%num < %n)) {
        %sku = getWord(%skulist, %n);
        %hasIt = %player.hasActiveSKU(%sku);
        if (%hasIt) {
            return 1;
        }
        %n = (1.0 + %n);
    }
    return 0;
};
function SkuManager::vetByRoles(%this, %rolesMask, %skusDry, %skusDefault) {
    %skusWet = %this.filterSkusRoles(%skusDry, %rolesMask);
    %skusWet = %this.overlaySkus(%skusDefault, %skusWet);
    return %skusWet;
};
function SkuManager::vetSkus(%this, %rolesMask, %gender, %skusDry, %skusDefault) {
    %skusWet = %this.filterSkusRoles(%skusDry, %rolesMask);
    %skusWet = %this.filterSkusGender(%skusWet, %gender);
    %skusWet = %this.overlaySkus(%skusDefault, %skusWet);
    return %skusWet;
};
function SkuManager::clearValueCache(%this) {
    if (isObject(%this.valueCache)) {
        %this.valueCache.delete();
    }
    %this.valueCache = safeNewScriptObject("StringMap", "", 0);
};
SkuManager.clearValueCache();
SkuManager.addBodyDrawer("hair");
SkuManager.addBodyDrawer("face");
SkuManager.addBodyDrawer("faceb");
SkuManager.addBodyDrawer("eyes");
SkuManager.addBodyDrawer("skin");
SkuManager.addOutfitDrawer("glasses");
SkuManager.addOutfitDrawer("torso");
SkuManager.addOutfitDrawer("torsob");
SkuManager.addOutfitDrawer("legs");
SkuManager.addOutfitDrawer("legsb");
SkuManager.addOutfitDrawer("feet");
SkuManager.addOutfitDrawer("ear");
SkuManager.addOutfitDrawer("neck");
SkuManager.addOutfitDrawer("neckb");
SkuManager.addOutfitDrawer("neckc");
SkuManager.addOutfitDrawer("wristleft");
SkuManager.addOutfitDrawer("wristleftb");
SkuManager.addOutfitDrawer("wristright");
SkuManager.addOutfitDrawer("wristrightb");
SkuManager.addOutfitDrawer("fingerleft");
SkuManager.addOutfitDrawer("fingerright");
SkuManager.addOutfitDrawer("toeleft");
SkuManager.addOutfitDrawer("toeright");
SkuManager.addOutfitDrawer("purse");
SkuManager.addOutfitDrawer("waist");
SkuManager.addOutfitDrawer("waistb");
SkuManager.addOutfitDrawer("back");
SkuManager.addOutfitDrawer("hat");
SkuManager.addOutfitDrawer("mask");
SkuManager.addOutfitDrawer("earl");
SkuManager.addOutfitDrawer("labret");
SkuManager.addOutfitDrawer("lftauricle");
SkuManager.addOutfitDrawer("lftconch");
SkuManager.addOutfitDrawer("lfteyebrow");
SkuManager.addOutfitDrawer("lftlobe");
SkuManager.addOutfitDrawer("lftorbital");
SkuManager.addOutfitDrawer("lftpinna");
SkuManager.addOutfitDrawer("lftrook");
SkuManager.addOutfitDrawer("lfttragus");
SkuManager.addOutfitDrawer("rghauricle");
SkuManager.addOutfitDrawer("rghconch");
SkuManager.addOutfitDrawer("rgheyebrow");
SkuManager.addOutfitDrawer("rghlobe");
SkuManager.addOutfitDrawer("rghorbital");
SkuManager.addOutfitDrawer("rghpinna");
SkuManager.addOutfitDrawer("rghrook");
SkuManager.addOutfitDrawer("rghtragus");
SkuManager.addOutfitDrawer("lowlip");
SkuManager.addOutfitDrawer("madonna");
SkuManager.addOutfitDrawer("medusa");
SkuManager.addOutfitDrawer("nostril");
SkuManager.addOutfitDrawer("septum");
SkuManager.addOutfitDrawer("tail");
SkuManager.addOutfitDrawer("chest");
SkuManager.addOutfitDrawer("props");
SkuManager.addOutfitDrawer("badges");
SkuManager.addOutfitDrawer("tokens");
SkuManager.addOutfitDrawer("gameplay");
SkuManager.addOutfitDrawer("deprecated_gl");
SkuManager.addOutfitDrawer("deprecated_ea");
SkuManager.addOutfitDrawer("deprecated_ne");
SkuManager.addOutfitDrawer("deprecated_wl");
SkuManager.addOutfitDrawer("deprecated_wr");
SkuManager.addOutfitDrawer("deprecated_wa");
SkuManager.addOutfitDrawer("microphone");
SkuManager.addOptionalDrawer("faceb");
SkuManager.addOptionalDrawer("glasses");
SkuManager.addOptionalDrawer("torsob");
SkuManager.addOptionalDrawer("legsb");
SkuManager.addOptionalDrawer("ear");
SkuManager.addOptionalDrawer("neck");
SkuManager.addOptionalDrawer("neckb");
SkuManager.addOptionalDrawer("neckc");
SkuManager.addOptionalDrawer("wristleft");
SkuManager.addOptionalDrawer("wristleftb");
SkuManager.addOptionalDrawer("wristright");
SkuManager.addOptionalDrawer("wristrightb");
SkuManager.addOptionalDrawer("fingerleft");
SkuManager.addOptionalDrawer("fingerright");
SkuManager.addOptionalDrawer("toeleft");
SkuManager.addOptionalDrawer("toeright");
SkuManager.addOptionalDrawer("purse");
SkuManager.addOptionalDrawer("waist");
SkuManager.addOptionalDrawer("waistb");
SkuManager.addOptionalDrawer("back");
SkuManager.addOptionalDrawer("hat");
SkuManager.addOptionalDrawer("mask");
SkuManager.addOptionalDrawer("earl");
SkuManager.addOptionalDrawer("labret");
SkuManager.addOptionalDrawer("lftauricle");
SkuManager.addOptionalDrawer("lftconch");
SkuManager.addOptionalDrawer("lfteyebrow");
SkuManager.addOptionalDrawer("lftlobe");
SkuManager.addOptionalDrawer("lftorbital");
SkuManager.addOptionalDrawer("lftpinna");
SkuManager.addOptionalDrawer("lftrook");
SkuManager.addOptionalDrawer("lfttragus");
SkuManager.addOptionalDrawer("rghauricle");
SkuManager.addOptionalDrawer("rghconch");
SkuManager.addOptionalDrawer("rgheyebrow");
SkuManager.addOptionalDrawer("rghlobe");
SkuManager.addOptionalDrawer("rghorbital");
SkuManager.addOptionalDrawer("rghpinna");
SkuManager.addOptionalDrawer("rghrook");
SkuManager.addOptionalDrawer("rghtragus");
SkuManager.addOptionalDrawer("lowlip");
SkuManager.addOptionalDrawer("madonna");
SkuManager.addOptionalDrawer("medusa");
SkuManager.addOptionalDrawer("nostril");
SkuManager.addOptionalDrawer("septum");
SkuManager.addOptionalDrawer("tail");
SkuManager.addOptionalDrawer("chest");
SkuManager.addOptionalDrawer("props");
SkuManager.addOptionalDrawer("badges");
SkuManager.addOptionalDrawer("tokens");
SkuManager.addOptionalDrawer("gameplay");
SkuManager.addOptionalDrawer("deprecated_gl");
SkuManager.addOptionalDrawer("deprecated_ea");
SkuManager.addOptionalDrawer("deprecated_ne");
SkuManager.addOptionalDrawer("deprecated_wl");
SkuManager.addOptionalDrawer("deprecated_wr");
SkuManager.addOptionalDrawer("deprecated_wa");
SkuManager.addOptionalDrawer("microphone");
SkuManager.addWearableSkuType("mesh");
SkuManager.addWearableSkuType("badge");
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
    %this.userFacingDrawerNamesNum = (1.0 + %this.userFacingDrawerNamesNum);
};
SkuManager.addUserFacingDrawerName("torso", "Top");
SkuManager.addUserFacingDrawerName("torsob", "Top");
SkuManager.addUserFacingDrawerName("legs", "Bottom");
SkuManager.addUserFacingDrawerName("legsb", "Bottom");
SkuManager.addUserFacingDrawerName("feet", "Feet");
SkuManager.addUserFacingDrawerName("neck", "Neck");
SkuManager.addUserFacingDrawerName("neckb", "Neck");
SkuManager.addUserFacingDrawerName("neckc", "Neck");
SkuManager.addUserFacingDrawerName("glasses", "Glasses");
SkuManager.addUserFacingDrawerName("ear", "Ear");
SkuManager.addUserFacingDrawerName("waist", "Waist");
SkuManager.addUserFacingDrawerName("waistb", "Waist");
SkuManager.addUserFacingDrawerName("wristleft", "Left Hand");
SkuManager.addUserFacingDrawerName("wristleftb", "Left Hand");
SkuManager.addUserFacingDrawerName("wristright", "Right Hand");
SkuManager.addUserFacingDrawerName("wristrightb", "Right Hand");
SkuManager.addUserFacingDrawerName("fingerleft", "Left Fingers");
SkuManager.addUserFacingDrawerName("fingerright", "Right Finger");
SkuManager.addUserFacingDrawerName("toeleft", "Left Toes");
SkuManager.addUserFacingDrawerName("toeright", "Right toes");
SkuManager.addUserFacingDrawerName("chest", "Chest");
SkuManager.addUserFacingDrawerName("hat", "Hat");
SkuManager.addUserFacingDrawerName("mask", "Mask");
SkuManager.addUserFacingDrawerName("purse", "Purse");
SkuManager.addUserFacingDrawerName("back", "Back");
SkuManager.addUserFacingDrawerName("tail", "Tail");
SkuManager.addUserFacingDrawerName("badges", "Badge");
SkuManager.addUserFacingDrawerName("eyes", "Eyes");
SkuManager.addUserFacingDrawerName("face", "Face");
SkuManager.addUserFacingDrawerName("faceb", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("earl", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("labret", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("lftauricle", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("lftconch", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("lfteyebrow", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("lftlobe", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("lftorbital", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("lftpinna", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("lftrook", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("lfttragus", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("rghauricle", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("rghconch", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("rgheyebrow", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("rghlobe", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("rghorbital", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("rghpinna", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("rghrook", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("rghtragus", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("lowlip", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("madonna", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("medusa", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("nostril", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("septum", "Facial Enhancement");
SkuManager.addUserFacingDrawerName("hair", "Hair");
SkuManager.addUserFacingDrawerName("skin", "Skin");
SkuManager.addUserFacingDrawerName("props", "Props");
SkuManager.addUserFacingDrawerName("tokens", "Token");
SkuManager.addUserFacingDrawerName("ceiling", "Ceiling");
SkuManager.addUserFacingDrawerName("wall", "Wall");
SkuManager.addUserFacingDrawerName("fixtures", "Fixtures");
SkuManager.addUserFacingDrawerName("floor", "Floor");
SkuManager.addUserFacingDrawerName("general", "General");
SkuManager.addUserFacingDrawerName("deprecated_ea", "");
SkuManager.addUserFacingDrawerName("deprecated_gl", "");
SkuManager.addUserFacingDrawerName("deprecated_ne", "");
SkuManager.addUserFacingDrawerName("deprecated_wa", "");
SkuManager.addUserFacingDrawerName("deprecated_wl", "");
SkuManager.addUserFacingDrawerName("deprecated_wr", "");
function SkuManager::sortSkusByDrawer(%this, %drySkuList) {
    %sortableList = "";
    %n = (1.0 - getWordCount(%drySkuList));
    if ((0.0 >= %n)) {
        %sku = getWord(%drySkuList, %n);
        %thing = %this.userFacingDrawerName;
        SkuManager.findBySku(%sku).drwrName;
        %sortableList = %sortableList @ %thing @ "\t" @ %sku @ "\n";
        %n = (1.0 - %n);
    }
    %sortableList = trim(%sortableList);
    (0.0 >= %n);
    %sortedList = SortRecords(%sortableList);
    %wetSkuList = "";
    %n = (1.0 - getWordCount(%drySkuList));
    if ((0.0 >= %n)) {
        %thing = getRecord(%sortedList, %n);
        %wetSkuList = getField(%thing, 3) @ " " @ %wetSkuList;
        %n = (1.0 - %n);
    }
    %wetSkuList = trim(%wetSkuList);
    (0.0 >= %n);
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
    %si = %this.findBySku(%skunum);
    if (!(isObject(%si))) {
        return "";
    }
    return %this.getUserFacingDrawerName(%si.drwrName);
};
function SkuItem::getUserFacingDrawerName(%this) {
    return SkuManager.getUserFacingDrawerName(%this.drwrName);
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
    %n = (1.0 - getFieldCount(%drawers));
    if ((0.0 >= %n)) {
        %drawer = getField(%drawers, %n);
        if (hasField($gSwatchableSkuDrawers, %drawer)) {
            return 1;
        }
        %n = (1.0 - %n);
    }
    return 0;
};
function SkuManager::isSwatchableSku(%this, %skunum) {
    return %this.findBySku(%skunum).isSwatchable();
};
function SkuManager::filterSkusDescription(%this, %skus, %userFilterText) {
    %userFilterText = strlwr(%userFilterText);
    %userFilterText = trim(%userFilterText);
    if ((%userFilterText $= "")) {
        return %skus;
    }
    %wet = "";
    %n = (1.0 - getWordCount(%skus));
    if ((0.0 >= %n)) {
        %sku = getWord(%skus, %n);
        %si = %this.findBySku(%sku);
        if ((0.0 >= strstr(%si.searchText, %userFilterText))) {
            %wet = %sku @ " " @ %wet;
        }
        %n = (1.0 - %n);
    }
    %wet = trim(%wet);
    (0.0 >= %n);
    return %wet;
};
function SkuManager::buildSkusSearchText(%this) {
    safeEnsureScriptObject("StringMap", "gReverseThumbCategories", 0);
    %n = (1.0 - ThumbCategories.size());
    if ((0.0 >= %n)) {
        %key = ThumbCategories.getKey(%n);
        %val = ThumbCategories.getValue(%n);
        %m = (1.0 - getWordCount(%val));
        if ((0.0 >= %m)) {
            %drwr = getWord(%val, %m);
            %cats = gReverseThumbCategories.get(%drwr);
            %cats = %cats @ " " @ %key;
            gReverseThumbCategories.put(%drwr, %cats);
            %m = (1.0 - %m);
        }
        %n = (1.0 - %n);
        (0.0 >= %m);
    }
    %n = (1.0 - %this.getCount());
    (0.0 >= %n);
    if ((0.0 >= %n)) {
        %si = %this.getObject(%n);
        %st = "";
        %st = %st @ "\t" @ %si.descLong;
        %st = %st @ "\t" @ %si.descShrt;
        %st = %st @ "\t" @ %si.tags;
        %st = %st @ "\t" @ %si.brand;
        %st = %st @ "\t" @ %si.drwrName;
        %st = %st @ "\t" @ gReverseThumbCategories.get(%si.drwrName);
        %st = %st @ "\t" @ %si.skuNumber;
        %st = %st @ "\t" @ %si.author;
        %st = strlwr(%st);
        %st = trim(%st);
        %si.searchText = %st;
        %n = (1.0 - %n);
    }
};
function SkuManager::filterSkusInList(%this, %skusDry, %list) {
    %skusWet = "";
    %sep = "";
    %num = getWordCount(%skusDry);
    %n = 0;
    if ((%num < %n)) {
        %sku = getWord(%skusDry, %n);
        if (%this.skuListHasSku(%list, %sku)) {
            %skusWet = %skusWet @ %sep @ %sku;
            %sep = " ";
        }
        %n = (1.0 + %n);
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
    %n = (1.0 - getWordCount(%drawersList));
    if ((0.0 >= %n)) {
        %drwrName = getWord(%drawersList, %n);
        %drwrSkus = %this.filterSkusDrwr(%skulist, %drwrName);
        %numSkus = getWordCount(%drwrSkus);
        if ((0.0 > %numSkus)) {
            %sku = getWord(%drwrSkus, getRandom(0, (1.0 - %numSkus)));
            %skus = %skus @ %delim @ %sku;
            %delim = " ";
        }
        %n = (1.0 - %n);
    }
    return %skus;
};
function SkuManager::getRandomSkus(%this, %player, %drawersList) {
    %skus = "";
    %n = (1.0 - getWordCount(%drawersList));
    if ((0.0 >= %n)) {
        %drwrName = getWord(%drawersList, %n);
        %drwrSkus = SkuManager.getSkusDrwr(%drwrName);
        %drwrSkus = SkuManager.filterSkusGender(%drwrSkus, %player.getGender());
        %drwrSkus = SkuManager.filterSkusRoles(%drwrSkus, %player.getRolesMask());
        %numSkus = getWordCount(%drwrSkus);
        if ((1.0 < %numSkus)) {
            log("wardrobe", "error", "Closet::getRandomSkus() - no skus in drawer" @ " " @ %drwrName @ " " @ getDebugString(%player));
        }
        %skus = %skus @ getWord(%drwrSkus, getRandom(0, (1.0 - %numSkus))) @ " ";
        %n = (1.0 - %n);
    }
    return %skus;
};
function SkuManager::getRandomSku(%this, %player, %drawersList) {
    %sku = "";
    %rnd = getRandom(0, (1.0 - getWordCount(%drawersList)));
    %drwrName = getWord(%drawersList, %rnd);
    %drwrSkus = SkuManager.getSkusDrwr(%drwrName);
    %drwrSkus = SkuManager.filterSkusGender(%drwrSkus, %player.getGender());
    %drwrSkus = SkuManager.filterSkusRoles(%drwrSkus, %player.getRolesMask());
    %numSkus = getWordCount(%drwrSkus);
    if ((1.0 < %numSkus)) {
        log("wardrobe", "error", "Closet::getRandomSku() - no skus in drawer" @ " " @ %drwrName @ " " @ getDebugString(%player));
    }
    %sku = getWord(%drwrSkus, getRandom(0, (1.0 - %numSkus))) @ " ";
    return %sku;
};
function SkuManager::skuListHasSku(%this, %list, %sku) {
    return (0.0 >= findWord(%list, %sku));
};
function SkuManager::getSkuShortDescriptions(%this, %skus, %delimiter, %includeUsage, %thumbnailWidth) {
    if (isDefined("%thumbnailSize")) {
    }
    %thumbnailSize = 0;
    %thumbnailSize;
    %ret = "";
    %delim = "";
    %n = (1.0 - getWordCount(%skus));
    if ((0.0 >= %n)) {
        %sku = getWord(%skus, %n);
        %si = %this.findBySku(%sku);
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
        if ((0.0 > %thumbnailWidth)) {
        }
        if ((%si.skuType $= "furnishing")) {
            %thumbnailImage = CSBrowser::getThumbnailPathForSku(0, %sku, 32);
            %thumbnailText = " <bitmap:" @ %thumbnailImage @ ":true:middle:width=" @ %thumbnailWidth @ ">";
        }
        %ret = %ret @ %delim @ %si.descShrt @ %thumbnailText @ %usage;
        %delim = %delimiter;
        %n = (1.0 - %n);
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
    if ((%num < %n)) {
        %sn = getWord(%skus, %n);
        %si = %this.findBySku(%sn);
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
        %n = (1.0 + %n);
    }
};
function Player::dumpActiveSkus(%this) {
    %skus = %this.getActiveSKUs();
    SkuManager.dumpSkuList(%skus);
};
function SkuManager::setSkuPair(%this, %list, %first, %second) {
    %ndx = findWord(%list, %first);
    if ((0.0 < %ndx)) {
        if ((0.0 > %second)) {
            %list = %list @ " " @ %first @ " " @ %second;
        }
    }
    if ((0.0 > %second)) {
        %list = setWord(%list, (1.0 + %ndx), %second);
    }
    %list = removeWord(removeWord(%list, (1.0 + %ndx)), %ndx);
    return %list;
};
function SkuManager::skusRemove(%this, %listA, %listB) {
    %num = getWordCount(%listB);
    %n = 0;
    if ((%num < %n)) {
        %sku = getWord(%listB, %n);
        %listA = findAndRemoveAllOccurrencesOfWord(%listA, %sku);
        %n = (1.0 + %n);
    }
    return %listA;
};
function SkuManager::addSkuTags(%this, %sku, %tags) {
    %si = %this.findBySku(%sku);
    if (!(isObject(%si))) {
        error(getScopeName() @ " " @ "- no such sku:" @ " " @ %sku @ " " @ %tags @ " " @ getTrace());
        return;
    }
    %si.tags = mergeWords(%si.tags, %tags);
    %n = (1.0 - getWordCount(%tags));
    if ((0.0 >= %n)) {
        %tag = getWord(%tags, %n);
        %skus = %this.skuTags.get(%tag);
        %skus = trim(%skus @ " " @ %sku);
        %this.skuTags.put(%tag, %skus);
        %n = (1.0 - %n);
    }
};
function SkuManager::getSkuTags(%this, %sku) {
    return %this.findBySku(%sku).tags;
};
function SkuManager::filterSkusAnyTags(%this, %skus, %tags) {
    %ret = "";
    %n = (1.0 - getWordCount(%skus));
    if ((0.0 >= %n)) {
        %sku = getWord(%skus, %n);
        %skuTags = %this.getSkuTags(%sku);
        %num = numWordsInWords(%skuTags, %tags);
        if ((0.0 > %num)) {
            %ret = %ret @ " " @ %sku;
        }
        %n = (1.0 - %n);
    }
    %ret = trim(%ret);
    (0.0 >= %n);
    return %ret;
};
function SkuManager::filterSkusTag(%this, %skus, %tag) {
    %ret = "";
    %n = (1.0 - getWordCount(%skus));
    if ((0.0 >= %n)) {
        %sku = getWord(%skus, %n);
        %skuTags = %this.getSkuTags(%sku);
        if (hasWord(%skuTags, %tag)) {
            %ret = %ret @ " " @ %sku;
        }
        %n = (1.0 - %n);
    }
    %ret = trim(%ret);
    (0.0 >= %n);
    return %ret;
};
function SkuManager::getSkuWithAnyTags(%this, %skus, %tags) {
    %n = (1.0 - getWordCount(%skus));
    if ((0.0 >= %n)) {
        %sku = getWord(%skus, %n);
        %skuTags = %this.getSkuTags(%sku);
        %num = numWordsInWords(%skuTags, %tags);
        if ((0.0 > %num)) {
            return %sku;
        }
        %n = (1.0 - %n);
    }
    return "";
};
function SkuManager::hasSkuWithAnyTags(%this, %skus, %tags) {
    return !(%this.getSkuWithAnyTags(%skus, %tags) $= "");
};
function SkuManager::hasSkuWithTag(%this, %skus, %tags) {
    return %this.hasSkuWithAnyTags(%skus, %tags);
};
function SkuManager::filterSkusAllTags(%this, %skus, %tags) {
    %ret = "";
    %numTags = getWordCount(%tags);
    %n = (1.0 - getWordCount(%skus));
    if ((0.0 >= %n)) {
        %sku = getWord(%skus, %n);
        %skuTags = %this.getSkuTags(%sku);
        %num = numWordsInWords(%skuTags, %tags);
        if ((%numTags == %num)) {
            %ret = %ret @ " " @ %sku;
        }
        %n = (1.0 - %n);
    }
    %ret = trim(%ret);
    (0.0 >= %n);
    return %ret;
};
function SkuManager::getSkuWithAllTags(%this, %skus, %tags) {
    %numTags = getWordCount(%tags);
    %n = (1.0 - getWordCount(%skus));
    if ((0.0 >= %n)) {
        %sku = getWord(%skus, %n);
        %skuTags = %this.getSkuTags(%sku);
        %num = numWordsInWords(%skuTags, %tags);
        if ((%numTags == %num)) {
            return %sku;
        }
        %n = (1.0 - %n);
    }
    return "";
};
function SkuManager::hasSkuWithAllTags(%this, %skus, %tags) {
    return !(%this.getSkuWithAllTags(%skus, %tags) $= "");
};
function SkuManager::dumpSkuTags(%this) {
    %this.skuTags.dumpValues();
};
function SkuManager::getSkusTag(%this, %tag, %gender) {
    %key = "skuTag_" @ %gender @ "_" @ %tag;
    if (!(%this.valueCache.hasKey(%key))) {
        %skus = %this.getSkusGender(%gender);
        %skus = %this.filterSkusTag(%skus, %tag);
        %this.valueCache.put(%key, %skus);
    }
    return %this.valueCache.get(%key);
};
function SkuManager::filterSkusDrwrs(%this, %skus, %drwrs) {
    %ret = "";
    %n = (1.0 - getWordCount(%skus));
    if ((0.0 >= %n)) {
        %sku = getWord(%skus, %n);
        %si = %this.findBySku(%sku);
        if (hasWord(%drwrs, %si.drwrName)) {
            %ret = %ret @ " " @ %sku;
        }
        %n = (1.0 - %n);
    }
    %ret = trim(%ret);
    (0.0 >= %n);
    return %ret;
};
function SkuManager::filterSkusStore(%this, %skus, %storename) {
    %ret = "";
    %n = (1.0 - getWordCount(%skus));
    if ((0.0 >= %n)) {
        %sku = getWord(%skus, %n);
        %si = %this.findBySku(%sku);
        if (hasWord(%si.stores, %storename)) {
            %ret = %ret @ " " @ %sku;
        }
        %n = (1.0 - %n);
    }
    %ret = trim(%ret);
    (0.0 >= %n);
    return %ret;
};
function SkuManager::getPropSkus(%this, %skulist) {
    %propSkus = %this.filterSkusDrwr(%skulist, "props");
    return trim(%propSkus);
};
function SkuManager::getFirstPropSku(%this, %skus) {
    %propSkus = %this.getPropSkus(%skus);
    %propSku = firstWord(%propSkus);
    return %propSku;
};
function SkuManager::hasPropSku(%this, %skus) {
    return !(%this.getPropSkus(%skus) $= "");
};
function Player::hasPropActive(%this) {
    return SkuManager.hasPropSku(%this.getActiveSKUs());
};
function Player::EnsureActiveSkus(%this, %skus) {
    %activeSkus = %this.getActiveSKUs();
    %n = (1.0 - getWordCount(%skus));
    if ((0.0 >= %n)) {
        %sku = getWord(%skus, %n);
        if (!(hasWord(%activeSkus, %sku))) {
            %currentSkus = %activeSkus @ " " @ %sku;
        }
        %n = (1.0 - %n);
    }
    %currentSkus = trim(%activeSkus);
    (0.0 >= %n);
    %this.setActiveSKUs(%activeSkus);
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
    if ((%num < %n)) {
        %texture = getWord(%oldTextures, %n);
        %explode = strreplace(%texture, ".", "\t");
        %explode = removeField(%explode, 0);
        if ((%explode $= %base)) {
            %texture = %newTextureName;
        }
        %newTextures = %newTextures @ " " @ %texture;
        %n = (1.0 + %n);
    }
    %newTextures = trim(%newTextures);
    (%num < %n);
    %this.setTxtrNames(%newTextures);
};
function SkuManager::findTemplateSku(%this, %sku) {
    %si = %this.findBySku(%sku);
    %candidates = %this.getSkusDrwr(%si.drwrName);
    %candidates = %this.filterSkusGender(%candidates, %si.gender);
    %candidates = %this.filterSkusTag(%candidates, "TEMPLATE");
    %candidates = findAndRemoveFirstOccurrenceOfWord(%candidates, %sku);
    %found = "";
    %n = (1.0 - getWordCount(%candidates));
    if ((0.0 >= %n)) {
    }
    if ((%found $= "")) {
        %candidateSku = getWord(%candidates, %n);
        %candidateSI = %this.findBySku(%candidateSku);
        if ((%si.meshName $= %candidateSI.meshName)) {
            %found = %candidateSku;
        }
        %n = (1.0 - %n);
        if ((0.0 >= %n)) {
        }
    }
    return %found;
};
