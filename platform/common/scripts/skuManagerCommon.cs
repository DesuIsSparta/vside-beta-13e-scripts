function DoesPlayerHaveItemActive(%player, %skulist)
{
    if (%skulist $= "")
    {
        return 1;
    }
    %num = getWordCount(%skulist);
    %n = 0;
    while (%n < %num)
    {
        %sku = getWord(%skulist, %n);
        %hasIt = %player.hasActiveSKU(%sku);
        if (%hasIt)
        {
            return 1;
        }
        %n = %n + 1;
    }
    return 0;
}
function SkuManager::vetByRoles(%this, %rolesMask, %skusDry, %skusDefault)
{
    %skusWet = %this.filterSkusRoles(%skusDry, %rolesMask);
    %skusWet = %this.overlaySkus(%skusDefault, %skusWet);
    return %skusWet;
}
function SkuManager::vetSkus(%this, %rolesMask, %gender, %skusDry, %skusDefault)
{
    %skusWet = %this.filterSkusRoles(%skusDry, %rolesMask);
    %skusWet = %this.filterSkusGender(%skusWet, %gender);
    %skusWet = %this.overlaySkus(%skusDefault, %skusWet);
    return %skusWet;
}
function SkuManager::clearValueCache(%this)
{
    if (isObject(%this.valueCache))
    {
        %this.valueCache.delete();
    }
    %this.valueCache = safeNewScriptObject("StringMap", "", 0);
    return ;
}
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
function SkuManager::allDrawers(%this)
{
    %allDrawers = %this.allClosetDrawers() SPC "gameplay";
    return %allDrawers;
}
function SkuManager::allClosetDrawers(%this)
{
    return "hair face faceb earl labret lftauricle lftconch lfteyebrow lftlobe lftorbital lftpinna lftrook lfttragus rghauricle rghconch rgheyebrow rghlobe rghorbital rghpinna rghrook rghtragus lowlip madonna medusa nostril septum eyes glasses torso torsob legs legsb feet skin ear neck neckb neckc chest wristleft wristleftb wristright wristrightb fingerleft fingerright toeleft toeright waist waistb purse back hat mask tail props badges";
}
function SkuManager::commonDrawers(%this)
{
    return "hair face faceb earl labret lftauricle lftconch lfteyebrow lftlobe lftorbital lftpinna lftrook lfttragus rghauricle rghconch rgheyebrow rghlobe rghorbital rghpinna rghrook rghtragus lowlip madonna medusa nostril septum eyes glasses torso torsob legs legsb feet skin ear neck neckb neckc chest wristleft wristleftb wristright wristrightb fingerleft fingerright toeleft toeright purse hat props badges";
}
SkuManager.userFacingDrawerNamesNum = 0;
function SkuManager::addUserFacingDrawerName(%this, %internalName, %userFacingName)
{
    %paddedNum = formatInt("%0.5d", %this.userFacingDrawerNamesNum);
    %this.userFacingDrawerName[%internalName] = %paddedNum TAB %internalName TAB %userFacingName;
    %this.userFacingDrawerNamesNum = %this.userFacingDrawerNamesNum + 1;
    return ;
}
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
function SkuManager::sortSkusByDrawer(%this, %drySkuList)
{
    %sortableList = "";
    %n = getWordCount(%drySkuList) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%drySkuList, %n);
        %thing = %this.userFacingDrawerName[SkuManager.findBySku(%sku).drwrName];
        %sortableList = %sortableList @ %thing TAB %sku @ "\n";
        %n = %n - 1;
    }
    %sortableList = trim(%sortableList);
    %sortedList = SortRecords(%sortableList);
    %wetSkuList = "";
    %n = getWordCount(%drySkuList) - 1;
    while (%n >= 0)
    {
        %thing = getRecord(%sortedList, %n);
        %wetSkuList = getField(%thing, 3) SPC %wetSkuList;
        %n = %n - 1;
    }
    %wetSkuList = trim(%wetSkuList);
    return %wetSkuList;
}
function SkuManager::getUserFacingDrawerName(%this, %internalDrawerName)
{
    %val = %this.userFacingDrawerName[%internalDrawerName];
    if (%val $= "")
    {
        error(getScopeName() SPC "- unknown drawer name:" SPC %internalDrawerName SPC getTrace());
    }
    return getField(%val, 2);
}
function SkuManager::getUserFacingDrawerNameFromSku(%this, %skunum)
{
    %si = %this.findBySku(%skunum);
    if (!isObject(%si))
    {
        return "";
    }
    return %this.getUserFacingDrawerName(%si.drwrName);
}
function SkuItem::getUserFacingDrawerName(%this)
{
    return SkuManager.getUserFacingDrawerName(%this.drwrName);
}
function SkuItem::getDescLong(%this)
{
    %ret = %this.descLong $= "" ? %this : %this;
    return %ret;
}
$gSwatchableSkuDrawers = "BuildingBlocks";
function SkuItem::isSwatchable(%this)
{
    %drawers = strreplace(%this.drwrName, "/", "\t");
    %n = getFieldCount(%drawers) - 1;
    while (%n >= 0)
    {
        %drawer = getField(%drawers, %n);
        if (hasField($gSwatchableSkuDrawers, %drawer))
        {
            return 1;
        }
        %n = %n - 1;
    }
    return 0;
}
function SkuManager::isSwatchableSku(%this, %skunum)
{
    return %this.findBySku(%skunum).isSwatchable();
}
function SkuManager::filterSkusDescription(%this, %skus, %userFilterText)
{
    %userFilterText = strlwr(%userFilterText);
    %userFilterText = trim(%userFilterText);
    if (%userFilterText $= "")
    {
        return %skus;
    }
    %wet = "";
    %n = getWordCount(%skus) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%skus, %n);
        %si = %this.findBySku(%sku);
        if (strstr(%si.searchText, %userFilterText) >= 0)
        {
            %wet = %sku SPC %wet;
        }
        %n = %n - 1;
    }
    %wet = trim(%wet);
    return %wet;
}
function SkuManager::buildSkusSearchText(%this)
{
    safeEnsureScriptObject("StringMap", "gReverseThumbCategories", 0);
    %n = ThumbCategories.size() - 1;
    while (%n >= 0)
    {
        %key = ThumbCategories.getKey(%n);
        %val = ThumbCategories.getValue(%n);
        %m = getWordCount(%val) - 1;
        while (%m >= 0)
        {
            %drwr = getWord(%val, %m);
            %cats = gReverseThumbCategories.get(%drwr);
            %cats = %cats SPC %key;
            gReverseThumbCategories.put(%drwr, %cats);
            %m = %m - 1;
        }
        %n = %n - 1;
    }
    %n = %this.getCount() - 1;
    while (%n >= 0)
    {
        %si = %this.getObject(%n);
        %st = "";
        %st = %st TAB %si.descLong;
        %st = %st TAB %si.descShrt;
        %st = %st TAB %si.tags;
        %st = %st TAB %si.brand;
        %st = %st TAB %si.drwrName;
        %st = %st TAB gReverseThumbCategories.get(%si.drwrName);
        %st = %st TAB %si.skuNumber;
        %st = %st TAB %si.author;
        %st = strlwr(%st);
        %st = trim(%st);
        %si.searchText = %st;
        %n = %n - 1;
    }
}

function SkuManager::filterSkusInList(%this, %skusDry, %list)
{
    %skusWet = "";
    %sep = "";
    %num = getWordCount(%skusDry);
    %n = 0;
    while (%n < %num)
    {
        %sku = getWord(%skusDry, %n);
        if (%this.skuListHasSku(%list, %sku))
        {
            %skusWet = %skusWet @ %sep @ %sku;
            %sep = " ";
        }
        %n = %n + 1;
    }
    return %skusWet;
}
function SkuManager::filterSkusVisible(%this, %skusDry, %unused)
{
    %skusWet = %skusDry;
    return %skusWet;
}
function SkuManager::getRandomSkusFromList(%this, %skulist, %drawersList)
{
    %skus = "";
    %delim = "";
    %n = getWordCount(%drawersList) - 1;
    while (%n >= 0)
    {
        %drwrName = getWord(%drawersList, %n);
        %drwrSkus = %this.filterSkusDrwr(%skulist, %drwrName);
        %numSkus = getWordCount(%drwrSkus);
        if (%numSkus > 0)
        {
            %sku = getWord(%drwrSkus, getRandom(0, %numSkus - 1));
            %skus = %skus @ %delim @ %sku;
            %delim = " ";
        }
        %n = %n - 1;
    }
    return %skus;
}
function SkuManager::getRandomSkus(%this, %player, %drawersList)
{
    %skus = "";
    %n = getWordCount(%drawersList) - 1;
    while (%n >= 0)
    {
        %drwrName = getWord(%drawersList, %n);
        %drwrSkus = SkuManager.getSkusDrwr(%drwrName);
        %drwrSkus = SkuManager.filterSkusGender(%drwrSkus, %player.getGender());
        %drwrSkus = SkuManager.filterSkusRoles(%drwrSkus, %player.getRolesMask());
        %numSkus = getWordCount(%drwrSkus);
        if (%numSkus < 1)
        {
            log("wardrobe", "error", "Closet::getRandomSkus() - no skus in drawer" SPC %drwrName SPC getDebugString(%player));
        }
        else
        {
            %skus = %skus @ getWord(%drwrSkus, getRandom(0, %numSkus - 1)) @ " ";
        }
        %n = %n - 1;
    }
    return %skus;
}
function SkuManager::getRandomSku(%this, %player, %drawersList)
{
    %sku = "";
    %rnd = getRandom(0, getWordCount(%drawersList) - 1);
    %drwrName = getWord(%drawersList, %rnd);
    %drwrSkus = SkuManager.getSkusDrwr(%drwrName);
    %drwrSkus = SkuManager.filterSkusGender(%drwrSkus, %player.getGender());
    %drwrSkus = SkuManager.filterSkusRoles(%drwrSkus, %player.getRolesMask());
    %numSkus = getWordCount(%drwrSkus);
    if (%numSkus < 1)
    {
        log("wardrobe", "error", "Closet::getRandomSku() - no skus in drawer" SPC %drwrName SPC getDebugString(%player));
    }
    else
    {
        %sku = getWord(%drwrSkus, getRandom(0, %numSkus - 1)) @ " ";
    }
    return %sku;
}
function SkuManager::skuListHasSku(%this, %list, %sku)
{
    return findWord(%list, %sku) >= 0;
}
function SkuManager::getSkuShortDescriptions(%this, %skus, %delimiter, %includeUsage, %thumbnailWidth)
{
    %thumbnailSize = isDefined("%thumbnailSize") ? %thumbnailSize : 0;
    %ret = "";
    %delim = "";
    %n = getWordCount(%skus) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%skus, %n);
        %si = %this.findBySku(%sku);
        if (!isObject(%si))
        {
            error(getScopeName() SPC "- unknown sku:" SPC %sku);
        }
        else
        {
            %usage = "";
            if (%includeUsage && !((%si.usageShrt $= "")))
            {
                %usage = " -" SPC %si.usageShrt;
            }
            %thumbnailText = "";
            if ((%thumbnailWidth > 0) && (%si.skuType $= "furnishing"))
            {
                %thumbnailImage = CSBrowser::getThumbnailPathForSku(0, %sku, 32);
                %thumbnailText = " <bitmap:" @ %thumbnailImage @ ":true:middle:width=" @ %thumbnailWidth @ ">";
            }
            %ret = %ret @ %delim @ %si.descShrt @ %thumbnailText @ %usage;
            %delim = %delimiter;
        }
        %n = %n - 1;
    }
    return %ret;
}
function SkuManager::dumpSkuList(%this, %skus)
{
    %skus = SortNumbers(%skus);
    %line = "sku num";
    %line = %line @ " - " @ drawer;
    %line = %line @ " - " @ desc;
    %line = %line @ " - " @ mesh;
    %line = %line @ " - " @ textures;
    %line = %line @ " - " @ roles;
    echo("wardrobe", %line);
    %num = getWordCount(%skus);
    %n = 0;
    while (%n < %num)
    {
        %sn = getWord(%skus, %n);
        %si = %this.findBySku(%sn);
        if (!isObject(%si))
        {
            error("wardrobe", "dumpSkuList: unknown sku" SPC %sn);
        }
        else
        {
            %line = %sn;
            %line = %line @ " - " @ %si.drwrName;
            %line = %line @ " - " @ %si.descShrt;
            %line = %line @ " - " @ %si.meshName;
            %line = %line @ " - " @ %si.getTxtrNames();
            %line = %line @ " - " @ roles::getRoleStrings(%si.rolesMask);
            echo("wardrobe", %line);
        }
        %n = %n + 1;
    }
}

function Player::dumpActiveSkus(%this)
{
    %skus = %this.getActiveSKUs();
    SkuManager.dumpSkuList(%skus);
    return ;
}
function SkuManager::setSkuPair(%this, %list, %first, %second)
{
    %ndx = findWord(%list, %first);
    if (%ndx < 0)
    {
        if (%second > 0)
        {
            %list = %list SPC %first SPC %second;
        }
    }
    else
    {
        if (%second > 0)
        {
            %list = setWord(%list, %ndx + 1, %second);
        }
        else
        {
            %list = removeWord(removeWord(%list, %ndx + 1), %ndx);
        }
    }
    return %list;
}
function SkuManager::skusRemove(%this, %listA, %listB)
{
    %num = getWordCount(%listB);
    %n = 0;
    while (%n < %num)
    {
        %sku = getWord(%listB, %n);
        %listA = findAndRemoveAllOccurrencesOfWord(%listA, %sku);
        %n = %n + 1;
    }
    return %listA;
}
function SkuManager::addSkuTags(%this, %sku, %tags)
{
    %si = %this.findBySku(%sku);
    if (!isObject(%si))
    {
        error(getScopeName() SPC "- no such sku:" SPC %sku SPC %tags SPC getTrace());
        return ;
    }
    %si.tags = mergeWords(%si.tags, %tags);
    %n = getWordCount(%tags) - 1;
    while (%n >= 0)
    {
        %tag = getWord(%tags, %n);
        %skus = %this.skuTags.get(%tag);
        %skus = trim(%skus SPC %sku);
        %this.skuTags.put(%tag, %skus);
        %n = %n - 1;
    }
}

function SkuManager::getSkuTags(%this, %sku)
{
    return %this.findBySku(%sku).tags;
}
function SkuManager::filterSkusAnyTags(%this, %skus, %tags)
{
    %ret = "";
    %n = getWordCount(%skus) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%skus, %n);
        %skuTags = %this.getSkuTags(%sku);
        %num = numWordsInWords(%skuTags, %tags);
        if (%num > 0)
        {
            %ret = %ret SPC %sku;
        }
        %n = %n - 1;
    }
    %ret = trim(%ret);
    return %ret;
}
function SkuManager::filterSkusTag(%this, %skus, %tag)
{
    %ret = "";
    %n = getWordCount(%skus) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%skus, %n);
        %skuTags = %this.getSkuTags(%sku);
        if (hasWord(%skuTags, %tag))
        {
            %ret = %ret SPC %sku;
        }
        %n = %n - 1;
    }
    %ret = trim(%ret);
    return %ret;
}
function SkuManager::getSkuWithAnyTags(%this, %skus, %tags)
{
    %n = getWordCount(%skus) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%skus, %n);
        %skuTags = %this.getSkuTags(%sku);
        %num = numWordsInWords(%skuTags, %tags);
        if (%num > 0)
        {
            return %sku;
        }
        %n = %n - 1;
    }
    return "";
}
function SkuManager::hasSkuWithAnyTags(%this, %skus, %tags)
{
    return !(%this.getSkuWithAnyTags(%skus, %tags) $= "");
}
function SkuManager::hasSkuWithTag(%this, %skus, %tags)
{
    return %this.hasSkuWithAnyTags(%skus, %tags);
}
function SkuManager::filterSkusAllTags(%this, %skus, %tags)
{
    %ret = "";
    %numTags = getWordCount(%tags);
    %n = getWordCount(%skus) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%skus, %n);
        %skuTags = %this.getSkuTags(%sku);
        %num = numWordsInWords(%skuTags, %tags);
        if (%num == %numTags)
        {
            %ret = %ret SPC %sku;
        }
        %n = %n - 1;
    }
    %ret = trim(%ret);
    return %ret;
}
function SkuManager::getSkuWithAllTags(%this, %skus, %tags)
{
    %numTags = getWordCount(%tags);
    %n = getWordCount(%skus) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%skus, %n);
        %skuTags = %this.getSkuTags(%sku);
        %num = numWordsInWords(%skuTags, %tags);
        if (%num == %numTags)
        {
            return %sku;
        }
        %n = %n - 1;
    }
    return "";
}
function SkuManager::hasSkuWithAllTags(%this, %skus, %tags)
{
    return !(%this.getSkuWithAllTags(%skus, %tags) $= "");
}
function SkuManager::dumpSkuTags(%this)
{
    %this.skuTags.dumpValues();
    return ;
}
function SkuManager::getSkusTag(%this, %tag, %gender)
{
    %key = "skuTag_" @ %gender @ "_" @ %tag;
    if (!%this.valueCache.hasKey(%key))
    {
        %skus = %this.getSkusGender(%gender);
        %skus = %this.filterSkusTag(%skus, %tag);
        %this.valueCache.put(%key, %skus);
    }
    return %this.valueCache.get(%key);
}
function SkuManager::filterSkusDrwrs(%this, %skus, %drwrs)
{
    %ret = "";
    %n = getWordCount(%skus) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%skus, %n);
        %si = %this.findBySku(%sku);
        if (hasWord(%drwrs, %si.drwrName))
        {
            %ret = %ret SPC %sku;
        }
        %n = %n - 1;
    }
    %ret = trim(%ret);
    return %ret;
}
function SkuManager::filterSkusStore(%this, %skus, %storename)
{
    %ret = "";
    %n = getWordCount(%skus) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%skus, %n);
        %si = %this.findBySku(%sku);
        if (hasWord(%si.stores, %storename))
        {
            %ret = %ret SPC %sku;
        }
        %n = %n - 1;
    }
    %ret = trim(%ret);
    return %ret;
}
function SkuManager::getPropSkus(%this, %skulist)
{
    %propSkus = %this.filterSkusDrwr(%skulist, "props");
    return trim(%propSkus);
}
function SkuManager::getFirstPropSku(%this, %skus)
{
    %propSkus = %this.getPropSkus(%skus);
    %propSku = firstWord(%propSkus);
    return %propSku;
}
function SkuManager::hasPropSku(%this, %skus)
{
    return !(%this.getPropSkus(%skus) $= "");
}
function Player::hasPropActive(%this)
{
    return SkuManager.hasPropSku(%this.getActiveSKUs());
}
function Player::EnsureActiveSkus(%this, %skus)
{
    %activeSkus = %this.getActiveSKUs();
    %n = getWordCount(%skus) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%skus, %n);
        if (!hasWord(%activeSkus, %sku))
        {
            %currentSkus = %activeSkus SPC %sku;
        }
        %n = %n - 1;
    }
    %currentSkus = trim(%activeSkus);
    %this.setActiveSKUs(%activeSkus);
    return ;
}
function SkuItem::hasTag(%this, %tag)
{
    return hasWord(%this.tags, %tag);
}
function SkuItem::replaceTextureName(%this, %newTextureName)
{
    %explode = strreplace(%newTextureName, ".", "\t");
    %base = removeField(%explode, 0);
    %oldTextures = %this.getTxtrNames();
    %num = getWordCount(%oldTextures);
    %newTextures = "";
    %n = 0;
    while (%n < %num)
    {
        %texture = getWord(%oldTextures, %n);
        %explode = strreplace(%texture, ".", "\t");
        %explode = removeField(%explode, 0);
        if (%explode $= %base)
        {
            %texture = %newTextureName;
        }
        %newTextures = %newTextures SPC %texture;
        %n = %n + 1;
    }
    %newTextures = trim(%newTextures);
    %this.setTxtrNames(%newTextures);
    return ;
}
function SkuManager::findTemplateSku(%this, %sku)
{
    %si = %this.findBySku(%sku);
    %candidates = %this.getSkusDrwr(%si.drwrName);
    %candidates = %this.filterSkusGender(%candidates, %si.gender);
    %candidates = %this.filterSkusTag(%candidates, "TEMPLATE");
    %candidates = findAndRemoveFirstOccurrenceOfWord(%candidates, %sku);
    %found = "";
    %n = getWordCount(%candidates) - 1;
    while (%found $= "")
    {
        %candidateSku = getWord(%candidates, %n);
        %candidateSI = %this.findBySku(%candidateSku);
        if (%si.meshName $= %candidateSI.meshName)
        {
            %found = %candidateSku;
        }
        %n = %n - 1;
    }
    return %found;
}
