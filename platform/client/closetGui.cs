$ClosetGuiOpenMessage = "Changing Clothes";
$gSkusToHideInCloset = getSpecialSKU(0, "helpmebadge");
$gClosetStanceEmotesNum = 0;
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "cool";
$gClosetStanceEmotesNum = ($gClosetStanceEmotesNum + 1.0);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "cool";
$gClosetStanceEmotesNum = ($gClosetStanceEmotesNum + 1.0);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "cool";
$gClosetStanceEmotesNum = ($gClosetStanceEmotesNum + 1.0);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "cool";
$gClosetStanceEmotesNum = ($gClosetStanceEmotesNum + 1.0);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "cool";
$gClosetStanceEmotesNum = ($gClosetStanceEmotesNum + 1.0);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "wve";
$gClosetStanceEmotesNum = ($gClosetStanceEmotesNum + 1.0);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "wve";
$gClosetStanceEmotesNum = ($gClosetStanceEmotesNum + 1.0);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "wve";
$gClosetStanceEmotesNum = ($gClosetStanceEmotesNum + 1.0);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "flr";
$gClosetStanceEmotesNum = ($gClosetStanceEmotesNum + 1.0);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "flr";
$gClosetStanceEmotesNum = ($gClosetStanceEmotesNum + 1.0);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "ttth";
$gClosetStanceEmotesNum = ($gClosetStanceEmotesNum + 1.0);
$gClosetStanceEmotesLast = "";
$gClosetNeutralHeightInches["f"] = ((5.0 * 12.0) + 7.0);
$gClosetNeutralHeightInches["m"] = ((5.0 * 12.0) + 7.0);
new StringMap(ThumbCategories) {
    ignoreCase = 1;
};
if (isObject(MissionCleanup)) {
    ThumbCategories.add(MissionCleanup);
}
"torso torsob legs legsb feet ear neck neckb neckc chest waist waistb wristleft wristleftb wristright wristrightb fingerleft fingerright toeleft toeright glasses back hat mask tail purse props badges tokens".put(ThumbCategories, "all items");
"torso torsob chest legs legsb feet".put(ThumbCategories, "all garments");
"ear neck neckb neckc waist waistb wristleft wristleftb wristright wristrightb fingerleft fingerright toeleft toeright chest back hat tail mask purse props badges".put(ThumbCategories, "all accessories");
"face faceb eyes skin hair".put(ThumbCategories, "all features");
"torso torsob chest".put(ThumbCategories, "tops");
"legs legsb".put(ThumbCategories, "bottoms");
"hair hat".put(ThumbCategories, "hair");
"feet toeleft toeright".put(ThumbCategories, "shoes");
"ear".put(ThumbCategories, "ear");
"neck neckb neckc".put(ThumbCategories, "neck");
"waist waistb".put(ThumbCategories, "waist");
"wristleft wristleftb wristright wristrightb fingerleft fingerright".put(ThumbCategories, "hands");
"purse".put(ThumbCategories, "bags");
"chest back hat tail mask".put(ThumbCategories, "misc");
"earl labret lftauricle lftconch lfteyebrow lftlobe lftorbital lftpinna lftrook lfttragus rghauricle rghconch rgheyebrow rghlobe rghorbital rghpinna rghrook rghtragus lowlip madonna medusa nostril septum".put(ThumbCategories, "bodymod");
"glasses".put(ThumbCategories, "glasses");
"face faceb".put(ThumbCategories, "face");
"eyes".put(ThumbCategories, "eyes");
"skin".put(ThumbCategories, "skin");
"props".put(ThumbCategories, "props");
"badges".put(ThumbCategories, "badges");
"tokens".put(ThumbCategories, "tokens");
SkuManager.buildSkusSearchText();
new StringMap(ThumbCategoriesOrder);
if (isObject(MissionCleanup)) {
    ThumbCategoriesOrder.add(MissionCleanup);
}
%n = 0;
"tops".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"bottoms".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"hair".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"shoes".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"ear".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"neck".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"waist".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"hands".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"bags".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"props".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"misc".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"bodymod".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"glasses".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"face".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"eyes".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"skin".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"badges".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
"tokens".put(ThumbCategoriesOrder, %n);
%n = (%n + 1.0);
$tmpGender = "f";
$tmpGender["0 0 0.0 1.7 35" @ $ThumbCamParams TAB $tmpGender @ "fullbody"] = ;
$tmpGender["0.4 -0.3 0.8 1.0 20" @ $ThumbCamParams TAB $tmpGender @ "hair"] = ;
$tmpGender["0.4 -0.3 0.8 1.0 15" @ $ThumbCamParams TAB $tmpGender @ "face"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "face"] @ $ThumbCamParams TAB $tmpGender @ "faceb"] = ;
$tmpGender["0.4 -0.3 0.8 1.0 10" @ $ThumbCamParams TAB $tmpGender @ "eyes"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "ear"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "earl"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "labret"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lftauricle"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lftconch"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lfteyebrow"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lftlobe"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lftorbital"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lftpinna"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lftrook"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lfttragus"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "rghauricle"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "rghconch"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "rgheyebrow"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "rghlobe"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "rghorbital"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "rghpinna"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "rghrook"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "rghtragus"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lowlip"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "madonna"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "medusa"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "nostril"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "septum"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "glasses"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "face"] @ $ThumbCamParams TAB $tmpGender @ "skin"] = ;
$tmpGender["0.4 -0.3 0.4 1.8 20" @ $ThumbCamParams TAB $tmpGender @ "torso"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "torso"] @ $ThumbCamParams TAB $tmpGender @ "torsob"] = ;
$tmpGender["0 0 -0.4 1.7 35" @ $ThumbCamParams TAB $tmpGender @ "legs"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "legs"] @ $ThumbCamParams TAB $tmpGender @ "legsb"] = ;
$tmpGender["0.1 -0.2 -0.85 1.2 20" @ $ThumbCamParams TAB $tmpGender @ "feet"] = ;
$tmpGender["0.4 -0.4 0.65 1.0 11" @ $ThumbCamParams TAB $tmpGender @ "neck"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "neck"] @ $ThumbCamParams TAB $tmpGender @ "neckb"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "neck"] @ $ThumbCamParams TAB $tmpGender @ "neckc"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "neck"] @ $ThumbCamParams TAB $tmpGender @ "chest"] = ;
$tmpGender["-0.2 -0.5 0.04 1.5 8" @ $ThumbCamParams TAB $tmpGender @ "wristleft"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "wristleft"] @ $ThumbCamParams TAB $tmpGender @ "wristleftb"] = ;
$tmpGender["1.1 -0.6 0.04 1.5 8" @ $ThumbCamParams TAB $tmpGender @ "wristright"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "wristright"] @ $ThumbCamParams TAB $tmpGender @ "wristrightb"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "wristleft"] @ $ThumbCamParams TAB $tmpGender @ "fingerleft"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "wristright"] @ $ThumbCamParams TAB $tmpGender @ "fingerright"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "feet"] @ $ThumbCamParams TAB $tmpGender @ "toeleft"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "feet"] @ $ThumbCamParams TAB $tmpGender @ "toeright"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "torso"] @ $ThumbCamParams TAB $tmpGender @ "purse"] = ;
$tmpGender["0.5 -0.4 0.1 1.0 18" @ $ThumbCamParams TAB $tmpGender @ "waist"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "waist"] @ $ThumbCamParams TAB $tmpGender @ "waistb"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "face"] @ $ThumbCamParams TAB $tmpGender @ "mask"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "hair"] @ $ThumbCamParams TAB $tmpGender @ "hat"] = ;
$tmpGender["0.4 -1.1 0.4 1.8 22" @ $ThumbCamParams TAB $tmpGender @ "back"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "waist"] @ $ThumbCamParams TAB $tmpGender @ "tail"] = ;
$tmpGender["1.1 -0.4 0.04 1.5 18" @ $ThumbCamParams TAB $tmpGender @ "props"] = ;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "badges"] = ;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"] = ;
$tmpGender = "m";
$tmpGender["0 0 0.0 1.7 35" @ $ThumbCamParams TAB $tmpGender @ "fullbody"] = ;
$tmpGender["0.0 0.0 0.9 1.0 15" @ $ThumbCamParams TAB $tmpGender @ "hair"] = ;
$tmpGender["0.0 0.0 0.9 1.0 6" @ $ThumbCamParams TAB $tmpGender @ "eyes"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "hair"] @ $ThumbCamParams TAB $tmpGender @ "face"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "hair"] @ $ThumbCamParams TAB $tmpGender @ "faceb"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "earl"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "labret"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lftauricle"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lftconch"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lfteyebrow"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lftlobe"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lftorbital"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lftpinna"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lftrook"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lfttragus"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "rghauricle"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "rghconch"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "rgheyebrow"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "rghlobe"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "rghorbital"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "rghpinna"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "rghrook"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "rghtragus"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "lowlip"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "madonna"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "medusa"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "nostril"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "septum"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "glasses"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "hair"] @ $ThumbCamParams TAB $tmpGender @ "skin"] = ;
$tmpGender["0 0 0.4 1.8 20" @ $ThumbCamParams TAB $tmpGender @ "torso"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "torso"] @ $ThumbCamParams TAB $tmpGender @ "torsob"] = ;
$tmpGender["0 0 -0.4 1.7 35" @ $ThumbCamParams TAB $tmpGender @ "legs"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "legs"] @ $ThumbCamParams TAB $tmpGender @ "legsb"] = ;
$tmpGender["0 0 -0.85 1.2 20" @ $ThumbCamParams TAB $tmpGender @ "feet"] = ;
$tmpGender["0.0 -0.2 0.76 1.0 12" @ $ThumbCamParams TAB $tmpGender @ "neck"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "neck"] @ $ThumbCamParams TAB $tmpGender @ "neckb"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "neck"] @ $ThumbCamParams TAB $tmpGender @ "neckc"] = ;
$tmpGender["0.0 -0.2 0.66 1.0 20" @ $ThumbCamParams TAB $tmpGender @ "chest"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "ear"] = ;
$tmpGender["-0.6 -0.1 0.15 1.5 10" @ $ThumbCamParams TAB $tmpGender @ "wristleft"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "wristleft"] @ $ThumbCamParams TAB $tmpGender @ "wristleftb"] = ;
$tmpGender["0.7 -0.1 0.15 1.5 10" @ $ThumbCamParams TAB $tmpGender @ "wristright"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "wristright"] @ $ThumbCamParams TAB $tmpGender @ "wristrightb"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "wristleft"] @ $ThumbCamParams TAB $tmpGender @ "fingerleft"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "wristright"] @ $ThumbCamParams TAB $tmpGender @ "fingerright"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "feet"] @ $ThumbCamParams TAB $tmpGender @ "toeleft"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "feet"] @ $ThumbCamParams TAB $tmpGender @ "toeright"] = ;
$tmpGender["0 -0.1 0.45 3 18" @ $ThumbCamParams TAB $tmpGender @ "purse"] = ;
$tmpGender["0 0 0.1 1.0 20" @ $ThumbCamParams TAB $tmpGender @ "waist"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "waist"] @ $ThumbCamParams TAB $tmpGender @ "waistb"] = ;
$tmpGender["0 -0.8 0.4 1.8 14" @ $ThumbCamParams TAB $tmpGender @ "back"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "hair"] @ $ThumbCamParams TAB $tmpGender @ "hat"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "face"] @ $ThumbCamParams TAB $tmpGender @ "mask"] = ;
$tmpGender[$tmpGender[$ThumbCamParams TAB $tmpGender @ "waist"] @ $ThumbCamParams TAB $tmpGender @ "tail"] = ;
$tmpGender["0.7 -0.1 0.15 1.5 18" @ $ThumbCamParams TAB $tmpGender @ "props"] = ;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "badges"] = ;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"] = ;
$shopBannerCacheCleared[121] = 0;
$shopBannerCacheCleared["aar"] = 0;
$shopBannerCacheCleared["amap"] = 0;
$shopBannerCacheCleared["amidol"] = 0;
$shopBannerCacheCleared["clover"] = 0;
$shopBannerCacheCleared["cos"] = 0;
$shopBannerCacheCleared["dega"] = 0;
$shopBannerCacheCleared["downtown"] = 0;
$shopBannerCacheCleared["drezz"] = 0;
$shopBannerCacheCleared["kitson"] = 0;
$shopBannerCacheCleared["goth"] = 0;
$shopBannerCacheCleared["kawaii"] = 0;
$shopBannerCacheCleared["kenna"] = 0;
$shopBannerCacheCleared["kong"] = 0;
$shopBannerCacheCleared["leet"] = 0;
$shopBannerCacheCleared["myet"] = 0;
$shopBannerCacheCleared["modpodz"] = 0;
$shopBannerCacheCleared["pcd"] = 0;
$shopBannerCacheCleared["roca"] = 0;
$shopBannerCacheCleared["salon"] = 0;
$shopBannerCacheCleared["starstyle"] = 0;
$shopBannerCacheCleared["threezee"] = 0;
$shopBannerCacheCleared["yjl"] = 0;
$shopBannerCacheCleared["vhd"] = 0;
$shopBannerCacheCleared["vbar"] = 0;
if (!(isObject(ClosetTabs))) {
    new ScriptObject(ClosetTabs) {
        class = "TabControl";
    };
    if (isObject(MissionCleanup)) {
        ClosetTabs.add(MissionCleanup);
    }
}
function Closet::skuListHasCategory(%list, %category) {
    %drawers = %category.get(ThumbCategories);
    %n = (getWordCount(%drawers) - 1.0);
    while ((%n >= 0.0)) {
        if (getWord(%drawers, %n).skuListHasDrawer(SkuManager, %list)) {
            return 1;
        }
        %n = (%n - 1.0);
    }
    return 0;
};
function ClosetTabs::setup(%this) {
    if (!(%this.initialized)) {
        %this.initializing = 1;
        "horizontal".Initialize(%this, ClosetTabContainer, "103 21", "", "");
        "platform/client/buttons/closet_tab".newTab(%this, "Shops");
        "platform/client/buttons/closet_tab".newTab(%this, "Closet");
        "platform/client/buttons/closet_tab".newTab(%this, "Body");
        "platform/client/buttons/closet_tab".newTab(%this, "Snapshot");
        "platform/client/buttons/closet_tab".newTab(%this, "My Designs");
        ClosetGui.lastTabOpened = "";
        ClosetGui.numberOfPurchasesAwaitingCompletion = 0;
        ClosetGui.numberOfPurchasesPastTimeout = 0;
        %this.initialized = 1;
    }
    %this.initializing = 0;
};
function removeShopBannerCache(%shop) {
    log("network", "debug", "DELETING SHOP BANNER CACHE!!! Shop: " @ %shop);
    if (!(%shop[$shopBannerCacheCleared @ %shop])) {
        %shop[$shopBannerCacheCleared @ %shop] = 1;
        deleteFile("dc/cache/platform/client/buttons/banners/store_" @ %shop @ "_n.jpg");
        deleteFile("dc/cache/platform/client/buttons/banners/store_" @ %shop @ "_d.jpg");
        deleteFile("dc/cache/platform/client/buttons/banners/store_" @ %shop @ "_h.jpg");
        deleteFile("dc/cache/platform/client/buttons/banners/store_" @ %shop @ "_i.jpg");
        log("network", "debug", "VAR: AFTER: " @ " " @ %shop[$shopBannerCacheCleared @ %shop]);
    }
};
function ClosetTabs::getInitialButtonOffset(%this) {
    return "34 55";
};
function ClosetTabs::getPadding(%this) {
    return 10;
};
function ClosetTabs::createButton(%this, %bitmapName, %tab, %name) {
    return new GuiBitmapButtonCtrl("") {
        profile = "ClosetTabButtonProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = %this.buttonSize;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = %this.getId() @ ".selectTab(" @ %tab.getId() @ ");";
        text = %name;
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = %bitmapName;
        helpTag = 0;
        drawText = 1;
    };;
};
$ClosetCategoryGroup = 286331153;
$ClosetBrandGroup = 286331154;
$BodyFeaturesGroup = 286331155;
$StoreCategoryGroup = 286331156;
$BodyStanceGroup = 286331157;
$ClosetHangersGroup = 286331158;
function ClosetTabs::tabSelected(%this, %tab) {
    ClosetGui.lastTabOpened = %tab.name;
    if ((%tab.name $= "CLOSET")) {
    }
    if ((%tab.name $= "BODY")) {
    }
    if ((%tab.name $= "SHOPS")) {
    }
    if ((%tab.name $= "MY DESIGNS")) {
        1.setVisible(ClosetMainObjectView);
        3.2.setOrbitDist(ClosetMainObjectView);
        "0 0 0.1".setLookAtNudge(ClosetMainObjectView);
        "0 3 -2".setLightDirection(ClosetMainObjectView);
        ClosetMainBadgeView.add(%tab);
        ClosetMainBadgeView.bringToFront(%tab);
        1.setVisible(ClosetMainBadgeView);
        ClosetMainObjectViewContainer.add(%tab);
        ClosetMainObjectViewContainer.bringToFront(%tab);
        ClosetMainObjectZoomOutButton.add(%tab);
        ClosetMainObjectZoomOutButton.bringToFront(%tab);
    }
    0.setVisible(ClosetMainObjectView);
    1.setVisible(ClosetMainBadgeView);
    if (isObject(%tab.hiliteStrip)) {
        %offset = %this.getInitialButtonOffset();
        %xoffset = getWord(%offset, 0);
        %yoffset = (getWord(%offset, 1) - 2.0);
        2.resize(%tab.hiliteStrip, %xoffset, %yoffset, %this.visibleTabsWidth);
    }
    if ((%tab.name $= "BODY")) {
        "p".setGenre($player);
        if (!(%this.tabBodyInitialized)) {
            %this.fillBodyTab();
        }
        BodyFeaturesPopup.rebuildPopupList();
        BodyItemsFrame.update();
        %this.createFilterWidget().add(%tab);
        ClosetMainObjectView.systemDragDrop = 0;
        ClosetTabs.updateBodyTabDisplay();
    }
    if ((%tab.name $= "CLOSET")) {
        "p".setGenre($player);
        if (!(%this.tabClosetInitialized)) {
            %this.fillClosetTab();
        }
        getFilteredInventoryForSetDrawers().update(ClosetBrandPopup);
        if ((ClosetBrandPopup.size() > 0.0)) {
            0.SetSelected(ClosetBrandPopup);
        }
        ClosetItemsFrame.update();
        %this.createFilterWidget().add(%tab);
        %this.createAuthorWidget().add(%tab);
        "".reparentSameSize(%this.createWhatYourWearingPanel(), ClosetWhatYourWearingContainer);
        "You Are Wearing".setTextWithStyle(ClosetWhatYoureWearingTitle);
        ClosetWhatYoureWearingList.filterByRemovable = 1;
        ClosetMainObjectView.systemDragDrop = 0;
        %outfitNames = [$player.getGender()];
        $Player::HangerNames;
        %i = 0;
        while ((%i < $gClosetNumOutfits)) {
            %name = getWord(%outfitNames, %i);
            %objectView = %i.getOutfitObjectView(ClosetTabs);
            $player.setSimObject(%objectView);
            $ClosetSkusBody @ " " @ %name[$ClosetSkusOutfit @ %name].setSkus(%objectView);
            %i = (%i + 1.0);
        }
        %outfitNum = findWord($Player::HangerNames, [$player.getGender()], $ClosetOutfitName);
        (%i < $gClosetNumOutfits);
        %outfitNum.getOutfitButton(ClosetTabs).performClick();
    }
    if ((%tab.name $= "SHOPS")) {
        "p".setGenre($player);
        if (!(%this.tabShopsInitialized)) {
            %this.fillStoreTab();
        }
        if (!(StoreExpirationLegend.lastStore $= $gCurrentStoreName)) {
            StoreExpirationLegend.lastStore = $gCurrentStoreName;
            0.setVisible(StoreExpirationLegend);
        }
        "Shops".showTabWithName(%this);
        StoreBalanceText.update();
        "".setBaseDesc(StoreShortDescText);
        "".setBaseDesc(StoreLongDescText);
        if (!($gCurrentStoreName $= "")) {
            0.setLeaveStoreControlsVisible(ClosetTabs);
            1.setStoreControlsVisible(ClosetTabs);
            Inventory::getCurrentStoreName().setText(StoreNameDescFrame.nameCtrl);
            Inventory::getCurrentStoreDescInCloset().setText(StoreNameDescFrame.descCtrl);
            %storename = getCurrentStoreID();
            %bannerRsrc = "";
            if (!(%storename $= "")) {
                removeShopBannerCache(%storename);
                %bannerRsrc = "platform/client/buttons/banners/store_" @ %storename;
                "storeads".applyUrl(dlMgr, %bannerRsrc, "dlMgrCallback_ShopTexture", "dlMgrCallback_ShopError", %this);
            }
            if (!(%bannerRsrc $= "")) {
                1.setVisible(StoreBannerBrackets);
                %bannerRsrc.setBitmap(StoreBanner);
            }
            0.setVisible(StoreBannerBrackets);
            %bgResource = "";
            if (!(%storename $= "")) {
                %bgResource = "platform/client/ui/store_backgrounds/store_bg_" @ %storename;
            }
            if (!(%bgResource $= "")) {
                %bgResource.setBitmap(StoreSpecificBackground);
                1.setVisible(StoreSpecificBackground);
                StoreSpecificBackground.bringToFront(%tab);
            }
            0.setVisible(StoreSpecificBackground);
        }
        0.setStoreControlsVisible(ClosetTabs);
        !(isInFUE()).setLeaveStoreControlsVisible(ClosetTabs);
        0.setVisible(StoreSpecificBackground);
        %this.createFilterWidget().add(%tab);
        if (($gCurrentStoreName $= "")) {
            0.setVisible(%this.createFilterWidget());
        }
        %this.createAuthorWidget().add(%tab);
        ClosetMainObjectView.systemDragDrop = 0;
        ClosetTabs.refreshStoreTab();
    }
    if ((%tab.name $= "SNAPSHOT")) {
        ClosetGui.doResetGenre();
        if (!(%this.tabSnapshotInitialized)) {
            %this.fillProfileTab();
        }
        %objView = "SNAPSHOT".getTabWithName(ClosetTabs).objView;
        $player.setSimObject(%objView);
        ClosetMainObjectView.getSkus().setSkus(%objView);
        if (isObject(ClosetGuiFUE)) {
        }
        ProfileSnapRegion.returnClosetGuiFUE = ClosetGuiFUE.visible;
        ProfileBackgroundChooser.Initialize();
        "0 3 -2".setLightDirection(ProfileObjectView);
        2.4.setOrbitDist(ProfileObjectView);
        ClosetMainObjectView.systemDragDrop = 0;
    }
    if ((%tab.name $= "MY DESIGNS")) {
        "p".setGenre($player);
        if (!(%this.tabMyShopInitialized)) {
            %this.fillMyShopTab();
        }
        "MY DESIGNS".showTabWithName(%this);
        %this.createFilterWidget().add(%tab);
        MyShopTextureInspector.pushToBack(MyShopTextureInspector.getGroup());
        ClosetMainObjectView.systemDragDrop = 1;
        "".reparentSameSize(%this.createWhatYourWearingPanel(), MyShopWhatYourWearingContainer);
        "Custom Items".setTextWithStyle(ClosetWhatYoureWearingTitle);
        ClosetWhatYoureWearingList.filterByRemovable = 0;
    }
    !(ClosetGui.isWaitingForPurchaseCompletion()).setActive(%tab.doneButton);
    !(ClosetGui.isWaitingForPurchaseCompletion()).setActive(%tab.cancelButton);
    if (0) {
        if (isObject(%tab.thumbnails)) {
            1.makeFirstResponder(%tab.thumbnails);
        }
        %fr = Canvas.getFirstResponder();
        if (isObject(%fr)) {
            0.makeFirstResponder(%fr);
        }
    }
    if (isObject(ClosetFilterContainer)) {
        1.makeFirstResponder(ClosetFilterField);
    }
    if (!(%tab.name $= "CLOSET")) {
        ClosetGui.updateVisibleAvatar();
    }
    "".zoomToSKU(ClosetMainObjectView);
    if (isInFUE()) {
    }
    if (!(%this.initializing)) {
        %tab.name.goToStepByName(ClosetGuiFUE);
    }
};
function dlMgrCallback_ShopTexture(%dlItem, %unused) {
    %dlItem.localFilename.setBitmap(StoreBanner);
};
function dlMgrCallback_ShopError(%dlItem) {
    log("network", "debug", "Image Download Error!! " @ " " @ %dlItem);
};
function ClosetTabs::updateRangeText(%this) {
    %currentTab = %this.getCurrentTab();
    if (!(isObject(%currentTab))) {
        return;
    }
    %rangeText = %currentTab.rangeText;
    %thumbnails = %currentTab.thumbnails;
    if (!(isObject(%thumbnails))) {
        return;
    }
    %cellHeight = (getWord(%thumbnails.childrenExtent, 1) + %thumbnails.spacing);
    %ypos = (1.0 - getWord(%thumbnails.getPosition(), 1));
    %closestRow = mFloor(((%ypos / %cellHeight) + 0.5));
    %count = %thumbnails.getCount();
    %min = mMin((1.0 + (%closestRow * %thumbnails.numRowsOrCols)), %count);
    %max = mMin((%min + 7.0), %count);
    if ((%count > 0.0)) {
    }
    "".setText(%rangeText, %min @ " - " @ %max @ " of " @ %count);
    return %closestRow;
};
function ClosetTabs::getShortSkuDesc(%this, %sku) {
    if ((%sku <= 0.0)) {
        return "";
    }
    %skuInfo = %sku.findBySku(SkuManager);
    %ret = "";
    %ret = %ret @ "<spush><b>" @ %skuInfo.descShrt @ "<spop>";
    return %ret;
};
function ClosetTabs::getLongSkuDesc(%this, %sku) {
    if ((%sku <= 0.0)) {
        return "";
    }
    %skuInfo = %sku.findBySku(SkuManager);
    %ret = "";
    if (!(trim(%skuInfo.descLong) $= trim(%skuInfo.descShrt))) {
        %ret = %ret @ %skuInfo.descLong;
    }
    if (!(%skuInfo.expireTime $= "")) {
        %ret = %ret @ "<br><bitmap:platform/client/ui/expiring_icon_small> - expires" @ " " @ secondsToDaysHoursMinutesSeconds(%skuInfo.expireTime) @ " " @ "after you get it.";
    }
    return %ret;
};
function ClosetThumbnails::onCreatedChild(%this, %child) {
    %background = new GuiControl("") {
        profile = "ClosetLtBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "4 3";
        extent = "95 83";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    };
    %objectView = new GuiObjectView("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "4 3";
        extent = "95 83";
        minExtent = "1 1";
        sluggishness = -1;
        CamSluggishness = 0.0000001;
        visible = 1;
    };
    if (isObject($player)) {
        $player.setSimObject(%objectView);
    }
    %badgeView = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "19 11";
        extent = "64 64";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };
    %buyStatus = new GuiBitmapCtrl("") {
        profile = "GuiModelessDialogProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "38 4";
        extent = "60 60";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        bitmap = "";
    };
    %rarityBitmap = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "3 60";
        extent = "25 25";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };
    %frameButton = new GuiBitmapButtonCtrl("") {
        profile = "ClosetFrameButtonProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "1 0";
        extent = "103 131";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "platform/client/buttons/frame";
        drawText = 1;
        thumbnails = %this;
        ctrl = %child;
    };
    %frameButton.command = %this.getId() @ ".buttonClicked(" @ %frameButton.getId() @ ");";
    "ClosetFrameButton".bindClassName(%frameButton);
    %buttonBacking = new GuiControl("") {
        profile = "ETSWhiteProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "2 86";
        extent = "99 42";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    };
    %toggleCartButton = new GuiBitmapButtonCtrl("") {
        profile = "GuiButtonProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "8 87";
        extent = "87 20";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        command = %child.getId() @ ".toggleInCart();";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "platform/client/buttons/add2cart";
        drawText = 0;
    };
    %buyNowButton = new GuiBitmapButtonCtrl("") {
        profile = "GuiButtonProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "8 107";
        extent = "87 20";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        command = %child.getId() @ ".buyNow();";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "platform/client/buttons/buyNow";
        drawText = 0;
    };
    %frameFader = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "5 4";
        extent = "93 81";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        bitmap = "platform/client/ui/thumbnailFader";
        modulationColor = "255 255 255 30";
    };
    %logo = new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "64 5";
        extent = "32 32";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
        modulationColor = "255 255 255 100";
    };
    %expiringIcon = new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "68 53";
        extent = "32 32";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
        modulationColor = "255 255 255 115";
    };
    %ugcStatusIcon = new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "69 4";
        extent = "32 32";
        bitmap = "";
        modulationColor = "255 255 255 80";
    };
    %desc = new GuiMLTextCtrl("") {
        profile = "ClosetSmallInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "3 86";
        extent = "95 14";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        allowColorChars = 0;
        maxChars = -1;
        text = "";
        lineSpacing = -(1.0);
    };
    %vpointsPrice = new GuiMLTextCtrl("") {
        profile = "ClosetPointsProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "3 132";
        extent = "50 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        allowColorChars = 0;
        maxChars = -1;
        text = "";
    };
    %vbuxPrice = new GuiMLTextCtrl("") {
        profile = "ClosetBuxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "58 132";
        extent = "50 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        allowColorChars = 0;
        maxChars = -1;
        text = "";
    };
    %totalButton = new GuiVariableWidthButtonCtrl("") {
        profile = "HiddenBracketButton15Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 132";
        extent = "104 16";
        minExtent = "1 1";
        visible = 0;
        command = "ClosetGui.purchaseSkus(" @ %child @ ".sku);";
        text = "";
        buttonType = "PushButton";
        drawText = 0;
    };
    %inStockText = new GuiMLTextCtrl("") {
        profile = "ClosetInStockProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "3 146";
        extent = "97 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        maxChars = -1;
        text = "";
    };
    %priceFader = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "3 132";
        extent = "97 26";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        bitmap = "platform/client/ui/priceFader";
        modulationColor = "255 255 255 190";
    };
    %availabilityText = new GuiMLTextCtrl("") {
        profile = "ClosetAvailabilityProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "3 132";
        extent = "95 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        maxChars = -1;
        text = "";
    };
    %background.add(%child);
    %ugcStatusIcon.add(%child);
    %objectView.add(%child);
    %expiringIcon.add(%child);
    %badgeView.add(%child);
    %rarityBitmap.add(%child);
    %frameButton.add(%child);
    %logo.add(%child);
    %frameFader.add(%child);
    %buyStatus.add(%child);
    %desc.add(%child);
    %vpointsPrice.add(%child);
    %vbuxPrice.add(%child);
    %totalButton.add(%child);
    %inStockText.add(%child);
    %priceFader.add(%child);
    %availabilityText.add(%child);
    %buttonBacking.add(%child);
    %toggleCartButton.add(%child);
    %buyNowButton.add(%child);
    %child.background = %background;
    %child.objectView = %objectView;
    %child.badgeView = %badgeView;
    %child.buyStatus = %buyStatus;
    %child.rarityBitmap = %rarityBitmap;
    %child.frameButton = %frameButton;
    %child.buttonBacking = %buttonBacking;
    %child.toggleCartButton = %toggleCartButton;
    %child.buyNowButton = %buyNowButton;
    %child.frameFader = %frameFader;
    %child.logo = %logo;
    %child.expiringIcon = %expiringIcon;
    %child.ugcStatusIcon = %ugcStatusIcon;
    %child.descCtrl = %desc;
    %child.vpointsCtrl = %vpointsPrice;
    %child.vbuxCtrl = %vbuxPrice;
    %child.totalButton = %totalButton;
    %child.priceFader = %priceFader;
    %child.availabilityText = %availabilityText;
    %child.inStockText = %inStockText;
    %child.thumbnails = %this;
    %child.selected = 0;
    %child.hilited = 0;
    %child.available = 1;
    if (!(getWord(%child.getNamespaceList(), 0) $= "ClosetThumbnailCtrl")) {
        "ClosetThumbnailCtrl".bindClassName(%child);
    }
};
function ClosetThumbnails::buttonClicked(%this, %button) {
    %button.ctrl.sku.toggleSku(ClosetGui);
    1.makeFirstResponder(%this);
};
function ClosetThumbnails::getAllSkusInDrawers(%this, %drwrNames) {
    %skus = "";
    %n = 0;
    while ((%n < getWordCount(%drwrNames))) {
        %s = $player.getGender().filterSkusGender(SkuManager, getWord(%drwrNames, %n).getSkusDrwr(SkuManager));
        %skus = %skus @ " " @ %s;
        %n = (%n + 1.0);
    }
    return %skus;
};
function getFilteredInventoryForSetDrawers() {
    %inventory = "";
    if ((ClosetTabs.getCurrentTab().name $= "BODY")) {
        %inventory = $Player::inventory;
    }
    if ((ClosetTabs.getCurrentTab().name $= "CLOSET")) {
        %inventory = $Player::inventory;
    }
    if ((ClosetTabs.getCurrentTab().name $= "SHOPS")) {
        %inventory = Inventory::getCurrentStoreSkus();
    }
    if ((ClosetTabs.getCurrentTab().name $= "MY DESIGNS")) {
        %inventory = "";
        error(getScopeName() @ " " @ "- unimplemented." @ " " @ getTrace());
    }
    if ((%inventory $= "no store")) {
        %inventory = "";
    }
    %skus = $player.getGender().filterSkusGender(SkuManager, %inventory);
    %skus = $player.getRolesMask().filterSkusRoles(SkuManager, %skus);
    return %skus;
};
$gClosetThumbnailsDrawersPrevious = "";
$gUpdatingClosetItemPopupFromThumbnailsSetDrawers = 0;
function ClosetThumbnails::setDrawers(%this, %drwrNames) {
    %skus = getFilteredInventoryForSetDrawers();
    %currentTabName = ClosetTabs.getCurrentTab().name;
    if ((%currentTabName $= "CLOSET")) {
    }
    if (isObject(ClosetBrandPopup)) {
        if (!(ClosetItemsFrame.brand $= "")) {
            %skus = $gClosetBrandsIntrnl[ClosetItemsFrame.brand].filterSkusBrand(SkuManager, %skus);
        }
        $gUpdatingClosetItemPopupFromThumbnailsSetDrawers = 1;
        %skus.update(ClosetItemPopup);
        $gUpdatingClosetItemPopupFromThumbnailsSetDrawers = 0;
        if ((%drwrNames $= "")) {
            if ((ClosetItemsFrame.category $= "")) {
                ClosetItemsFrame.category = "All Items";
            }
            %category = strlwr(ClosetItemsFrame.category);
            %drwrNames = %category.get(ThumbCategories);
        }
    }
    %skusTmp = "";
    %n = (getWordCount(%drwrNames) - 1.0);
    while ((%n >= 0.0)) {
        %s = getWord(%drwrNames, %n).filterSkusDrwr(SkuManager, %skus);
        if (!(%s $= "")) {
            %skusTmp = %s @ " " @ %skusTmp;
        }
        %n = (%n - 1.0);
    }
    %skus = trim(%skusTmp);
    (%n >= 0.0);
    %skus.setSkus(%this);
};
function ClosetThumbnails::setUnfilteredSkus(%this, %skus) {
    %this.unfilteredSkus = %skus;
    %this.refilter();
};
function ClosetThumbnails::refilter(%this) {
    %this.unfilteredSkus.setSkus(%this);
};
function ClosetThumbnails::setSkus(%this, %skus) {
    %startingPos = %this.getPosition();
    %currentTabName = ClosetTabs.getCurrentTab().name;
    %skus = filterOutSkusToHideInCloset(%skus);
    if ((%currentTabName $= "SHOPS")) {
        %skus = %skus.filterSkusNonZeroManufactured(SkuManager);
    }
    %skus = trim(%skus);
    if ((%currentTabName $= "CLOSET")) {
    }
    if ((%currentTabName $= "SHOPS")) {
    }
    if ((%currentTabName $= "BODY")) {
    }
    if ((%currentTabName $= "MY DESIGNS")) {
        if (isObject(ClosetFilterField)) {
        }
        %userFilterText = "";
        ClosetFilterField.getValue();
        %skus = %userFilterText.filterSkusDescription(SkuManager, %skus);
    }
    if (isObject(%this.otherGenderText)) {
        %numSkusOtherGender = getWordCount(%skus);
        %skus = $player.getGender().filterSkusGender(SkuManager, %skus);
        %numSkus = getWordCount(%skus);
        %numSkusOtherGender = (%numSkusOtherGender - %numSkus);
        if ((%numSkusOtherGender == 0.0)) {
        }
        %text = "<just:right>(" @ %numSkusOtherGender @ " in other gender)";
        "";
        %text.setText(%this.otherGenderText);
    }
    %numSkus = getWordCount(%skus);
    %numSkus.setNumChildren(%this);
    if (isObject(%this.infoText)) {
        if ((%numSkus != 0.0) && (ClosetTabs.getCurrentTab().name $= "BODY") && !(BodyItemsFrame.features $= "Height")) {
        }
        if (!(BodyItemsFrame.features $= "Stance")) {
            0.setVisible(%this.infoText);
            1.setVisible(%this.scroll);
        }
        0.setVisible(%this.scroll);
        1.setVisible(%this.infoText);
        "no matching items".setText(%this.infoText);
    }
    if (!(isObject(ClosetCurrentCamParams))) {
        new StringMap(ClosetCurrentCamParams);
        if (isObject(MissionCleanup)) {
            ClosetCurrentCamParams.add(MissionCleanup);
        }
    }
    ClosetCurrentCamParams.adjustForHeight();
    if (($ClosetOutfitName $= "")) {
        warn("wardrobe", getScopeName() @ ": $ClosetOutfitName is empty");
        return;
    }
    %n = 0;
    while ((%n < %numSkus)) {
        %cell = %n.getObject(%this);
        %skunum = getWord(%skus, %n);
        if ((getWordCount(%skunum) < 1.0)) {
        }
        if ((getWordCount(%skunum) > 1.0)) {
        }
        if ((%skunum == 0.0)) {
        }
        if ((%skunum $= 0)) {
        }
        if ((%numSkus != getWordCount(%skus))) {
            error(getScopeName() @ " " @ "- cell#" @ %cell.getId() @ " " @ "- sku #" @ %n @ " " @ "of" @ " " @ %numSkus @ "/" @ getWordCount(%skus) @ " " @ "- skus:" @ " " @ %skunum @ " " @ "- end.");
            error(getScopeName() @ " " @ "- skus:" @ " " @ %skus @ " " @ "- end.");
        }
        %skunum.setCellSkus(%this, %cell);
        %n = (%n + 1.0);
    }
    %dkBackground = 0;
    (%n < %numSkus);
    %currentDrwrName = "";
    %expiringItemsCount = 0;
    %n = 0;
    while ((%n < %numSkus)) {
        %cell = %n.getObject(%this);
        %skunum = getWord(%skus, %n);
        %skuItem = %skunum.findBySku(SkuManager);
        %skuItem.descShrt.setText(%cell.descCtrl);
        if ((%skuItem.brand $= "roca")) {
            "platform/client/ui/roca_logo_small".setBitmap(%cell.logo);
        }
        if ((%skuItem.brand $= "myet")) {
            "platform/client/ui/myet_logo_small".setBitmap(%cell.logo);
        }
        if ((%skuItem.brand $= "pcd")) {
            "platform/client/ui/pcd_logo_small".setBitmap(%cell.logo);
        }
        if ((%skuItem.brand $= "staff")) {
            "platform/client/ui/staff_logo_small".setBitmap(%cell.logo);
        }
        if ((%skuItem.brand $= "new")) {
            "platform/client/ui/new_logo_small".setBitmap(%cell.logo);
        }
        "".setBitmap(%cell.logo);
        if ("new".hasTag(%skuItem)) {
            "platform/client/ui/new_logo_small".setBitmap(%cell.logo);
        }
        if (!(%skuItem.expireTime $= "")) {
            "platform/client/ui/expiring_icon".setBitmap(%cell.expiringIcon);
            1.setVisible(%cell.expiringIcon);
            %expiringItemsCount = (%expiringItemsCount + 1.0);
        }
        0.setVisible(%cell.expiringIcon);
        if ((%currentTabName $= "MY DESIGNS")) {
            ClosetGui_MyShop_GetSkuUGCStatusIcon(%skunum).setBitmap(%cell.ugcStatusIcon);
            1.setVisible(%cell.ugcStatusIcon);
        }
        0.setVisible(%cell.ugcStatusIcon);
        %skuItem.qty.getRarityBitmap(%this).setBitmap(%cell.rarityBitmap);
        %skuItem.skuType.isWearableSkuType(SkuManager).setActive(%cell.frameButton);
        if ((%this.tab.name $= "SHOPS")) {
            %vpointsSym = "platform/client/ui/vpoints_9";
            %vbuxSym = "platform/client/ui/vbux_9";
            %vpointsPrice = Inventory::getVPointsPriceForSku(%cell.sku);
            %vbuxPrice = Inventory::getVBuxPriceForSku(%cell.sku);
            if ((%vpointsPrice == 0.0)) {
            }
            if ((%vbuxPrice == 0.0)) {
                "<just:right>free!".setText(%cell.vpointsCtrl);
                "".setText(%cell.vbuxCtrl);
            }
            "".setText(%cell.vpointsCtrl);
            "".setText(%cell.vbuxCtrl);
            if ((%vpointsPrice > 0.0)) {
                "<bitmap:" @ %vpointsSym @ "> " @ %vpointsPrice.setText(%cell.vpointsCtrl);
            }
            if ((%vbuxPrice > 0.0)) {
                "<bitmap:" @ %vbuxSym @ "> " @ %vbuxPrice.setText(%cell.vbuxCtrl);
            }
            1.setVisible(%cell.totalButton);
            %cell[$gStoreItemsQty @ %cell.sku].GetInStockText(%this).setText(%cell.inStockText);
            if ((findWord($Player::inventory, %cell.sku) >= 0.0)) {
                "platform/client/ui/owned".SetCellAvailability(%this, %cell, 0, 1, "<just:right><color:00bb00>0wn3d!");
            }
            if ((%cell[$gStoreItemsQty @ %cell.sku] == 0.0)) {
                "".SetCellAvailability(%this, %cell, 0, 0, "<just:right><color:dd0000>Sold Out!");
            }
            if ((%skuItem.rspk > (respektScoreToLevel($gMyRespektPoints) + 1.0))) {
                "platform/client/ui/cantbuy2".SetCellAvailability(%this, %cell, 0, 0, "<just:right><color:bb0000>More Levels!");
            }
            if ((%skuItem.rspk > respektScoreToLevel($gMyRespektPoints))) {
                "platform/client/ui/cantbuy".SetCellAvailability(%this, %cell, 0, 0, "<just:right><color:dd0000>Next Level!");
            }
            "".SetCellAvailability(%this, %cell, 1, 1, "");
        }
        "".setText(%cell.vpointsCtrl);
        "".setText(%cell.vbuxCtrl);
        %cell.available = 0;
        if (!(%currentDrwrName $= %skuItem.drwrName)) {
            %currentDrwrName = %skuItem.drwrName;
            %dkBackground = !(%dkBackground);
        }
        %dkBackground ? ClosetDkBackgroundProfile : ClosetLtBackgroundProfile.setProfile(%cell.background);
        %n = (%n + 1.0);
    }
    if (((%n < %numSkus) @ " " @ %currentTabName $= "SHOPS")) {
    }
    if ((%expiringItemsCount > 0.0)) {
        1.setVisible(StoreExpirationLegend);
    }
    %this.setSelectedThumbs();
    (1.0 - getWord(%startingPos, 1)).scrollTo(%this.getParent(), 0);
};
function ClosetThumbnails::SetCellAvailability(%this, %cell, %showPrice, %canTryOn, %subText, %overlayBitmapName) {
    if (!(%overlayBitmapName $= "")) {
        %overlayBitmapName.setBitmap(%cell.buyStatus);
        1.setVisible(%cell.buyStatus);
    }
    0.setVisible(%cell.buyStatus);
    if (!(%subText $= "")) {
        1.setVisible(%cell.availabilityText);
        %subText.setText(%cell.availabilityText);
        %cell.available = 0;
    }
    0.setVisible(%cell.availabilityText);
    %cell.available = 1;
    !(%showPrice).setVisible(%cell.priceFader);
    if (%canTryOn) {
    }
    %cell.SkuItem.skuType.isWearableSkuType(SkuManager).setActive(%cell.frameButton);
    !(%canTryOn).setVisible(%cell.frameFader);
};
function ClosetThumbnails::getRarityBitmap(%this, %qty) {
    %base = "platform/client/ui/";
    if ((%qty < 0.0)) {
        return "";
    }
    if ((%qty < 1000.0)) {
        return %base @ "rarity_superrare";
    }
    if ((%qty < 5000.0)) {
        return %base @ "rarity_reallyrare";
    }
    if ((%qty < 10000.0)) {
        return %base @ "rarity_rare";
    }
    return "";
};
function ClosetThumbnails::GetInStockText(%this, %qty) {
    if ((%qty <= 0.0)) {
        return "";
    }
    if ((%qty < 25.0)) {
        return "in stock: <color:dd0000>almost gone!";
    }
    if ((%qty < 50.0)) {
        return "in stock: <color:ee8800>not many";
    }
    if ((%qty < 100.0)) {
        return "in stock: <color:ee8800>a few";
    }
    if ((%qty < 500.0)) {
        return "in stock: <color:00bb00>enough";
    }
    return "in stock: <color:00bb00>yes!";
};
function ClosetThumbnails::setCellSkus(%this, %cell, %skus) {
    if ((getWordCount(%skus) < 1.0)) {
        error(getScopeName() @ " " @ "no skus passed in!" @ " " @ getTrace());
        return;
    }
    if ((getWordCount(%skus) != 1.0)) {
        error(getScopeName() @ " " @ "sorry, only 1 sku is currently supported." @ " " @ %skus);
        %skus = getWord(%skus, 0);
    }
    %skunum = %skus;
    %bodyAndOutfitSkus = $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] @ " " @ $ClosetSkusBody;
    %skuItem = %skunum.findBySku(SkuManager);
    %thumb = %cell.objectView;
    %badge = %cell.badgeView;
    %cell.sku = %skunum;
    %cell.SkuItem = %skuItem;
    if ((%skuItem.skuType $= "mesh")) {
        1.setVisible(%thumb);
        0.setVisible(%badge);
        %params = strlwr(%skuItem.drwrName).get(ClosetCurrentCamParams);
        %dist = getWord(%params, 3);
        %fov = getWord(%params, 4);
        %lookAtNudge = getWords(%params, 0, 2);
        %pskus = %skunum.overlaySkus(SkuManager, %bodyAndOutfitSkus);
        %thumb.layerSku = %skunum;
        %thumb.consumeMouseWheel = 0;
        %pskus.setSkus(%thumb);
        ClosetMainObjectView.makeSlaveOf(%thumb);
        $player.setSimObject(%thumb);
        "0 3 -2".setLightDirection(%thumb);
        %lookAtNudge.setLookAtNudge(%thumb);
        %dist.setOrbitDist(%thumb);
        %fov.setFOV(%thumb);
    }
    if ((%skuItem.skuType $= "badge")) {
        0.setVisible(%thumb);
        1.setVisible(%badge);
        %bitmapName = %skuItem.getBitmapPath();
        %bitmapName.setBitmap(%badge);
    }
    if ((%skuItem.skuType $= "token")) {
        0.setVisible(%thumb);
        1.setVisible(%badge);
        %bitmapName = %skuItem.getBitmapPath();
        %bitmapName.setBitmap(%badge);
    }
    if ((%skuItem.skuType $= "swatch")) {
        error("swatch in the closet!" @ " " @ %skunum);
    }
};
function ClosetCurrentCamParams::adjustForHeight(%this) {
    %allDrawers = SkuManager.allClosetDrawers();
    %allDrawers = %allDrawers @ " " @ "fullbody";
    %numDrawers = getWordCount(%allDrawers);
    %i = 0;
    while ((%i < %numDrawers)) {
        %drawer = strlwr(getWord(%allDrawers, %i));
        %params = %drawer[$ThumbCamParams TAB $player.getGender() @ %drawer];
        if (!(%params $= "")) {
            %vNudge = getWord(%params, 2);
            %vNudge = (%vNudge + ((%vNudge + 1.0) * ($UserPref::Player::height - 1.0)));
            %params = setWord(%params, 2, %vNudge);
        }
        %params.put(%this, %drawer);
        %i = (%i + 1.0);
    }
};
function ClosetMainObjectView::zoomToSKU(%this, %sku) {
    if ((%sku $= "")) {
        %drawer = "fullbody";
        0.setVisible(ClosetMainObjectZoomOutButton);
    }
    %drawer = %sku.findBySku(SkuManager).drwrName;
    1.setVisible(ClosetMainObjectZoomOutButton);
    %params = %drawer.get(ClosetCurrentCamParams);
    %params.setCamParams(%this);
};
function GuiObjectView::setCamParams(%this, %params) {
    if ((%params $= "")) {
        return;
    }
    %dist = getWord(%params, 3);
    %fov = getWord(%params, 4);
    %lookAtNudge = getWords(%params, 0, 2);
    if (!(%this.fovFac $= "")) {
        %fov = (%fov * %this.fovFac);
    }
    "0 3 -2".setLightDirection(%this);
    %lookAtNudge.setLookAtNudge(%this);
    %dist.setOrbitDist(%this);
    %fov.setFOV(%this);
};
function ClosetThumbnails::SetSelected(%this, %cell, %selected) {
    %cell.selected = %selected;
    %buttonsDir = "platform/client/buttons/";
    %selString = %cell.selected ? "_sel" : "";
    %hiString = %cell.hilited ? "_hi" : "";
    %buttonsDir @ "frame" @ %selString @ %hiString.setBitmap(%cell.frameButton);
};
function ClosetThumbnails::setSelectedThumbs(%this) {
    if ((ClosetTabs.getCurrentTab().name $= "SHOPS")) {
        %selectedSkus = $StoreSkusLayer;
    }
    %selectedSkus = $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] @ " " @ $ClosetSkusBody;
    %numThumbs = %this.getCount();
    %n = 0;
    while ((%n < %numThumbs)) {
        %cell = %n.getObject(%this);
        %selected = (findWord(%selectedSkus, %cell.sku) >= 0.0);
        %selected.SetSelected(%this, %cell);
        %n = (%n + 1.0);
    }
};
$gClosetThumbnailStoreHighlightTimer = "";
$gClosetThumbnailStoreHighlightDelayMS = 0;
function ClosetThumbnailCtrl::onHilite(%this) {
    if ((getCurrentStoreID() $= "")) {
        return;
    }
    %this.frameButton.mouseOver = 1;
    %this.scrollToCell(%this.thumbnails.scroll);
    %si = %this.sku.findBySku(SkuManager);
    %descShort = %this.sku.getShortSkuDesc(ClosetTabs);
    %descLong = %this.sku.getLongSkuDesc(ClosetTabs);
    if ($DevPref::Closet::skuDeets) {
        %descLong = %descLong @ "<font:Courier New:14>";
        %descLong = %descLong @ "\n" @ "sku    = " @ " " @ %si.skuNumber;
        %descLong = %descLong @ "\n" @ "type   = " @ " " @ %si.skuType;
        %descLong = %descLong @ "\n" @ "mesh   = " @ " " @ %si.meshName;
        %descLong = %descLong @ "\n" @ "txtrs  = " @ " " @ %si.getTxtrNames();
        %descLong = %descLong @ "\n" @ "roles  = " @ " " @ roles::getRoleStrings(%si.rolesMask);
        %descLong = %descLong @ "\n" @ "bornW/ = " @ " " @ %si.born;
        %descLong = %descLong @ "\n" @ "rspkt  = " @ " " @ %si.rspk;
        %descLong = %descLong @ "\n" @ "tags   = " @ " " @ %si.skuNumber.getSkuTags(SkuManager);
    }
    if (isObject(BodyLongDescText)) {
        %descShort.setText(BodyShortDescText);
        %descLong.setText(BodyLongDescText);
    }
    if (isObject(ClosetLongDescText)) {
        %descShort.setText(ClosetShortDescText);
        %descLong.setText(ClosetLongDescText);
    }
    if (isObject(StoreLongDescText)) {
        %descShort.setText(StoreShortDescText);
        %descLong.setText(StoreLongDescText);
    }
    %this.sku.updateAuthorWidget(ClosetTabs);
    if (%this.available) {
    }
    if ((%this.thumbnails == ClosetThumbnailsShop.getId())) {
        if (!($gClosetThumbnailStoreHighlightTimer $= "")) {
            cancel($gClosetThumbnailStoreHighlightTimer);
        }
        $gClosetThumbnailStoreHighlightTimer = "onHiliteStore".schedule(%this, $gClosetThumbnailStoreHighlightDelayMS);
    }
};
function ClosetThumbnailCtrl::onHiliteStore(%this) {
    if (!($gClosetThumbnailStoreHighlightTimer $= "")) {
        cancel($gClosetThumbnailStoreHighlightTimer);
        $gClosetThumbnailStoreHighlightTimer = "";
    }
    %this.hilited = 1;
    %buttonsDir = "platform/client/buttons/";
    %frameBitmap = %this.selected ? "frame_sel" : "frame";
    %cartBitmap = %this.sku.containsSku(StoreShoppingList) ? "removeFromCart" : "add2cart";
    %buttonsDir @ %frameBitmap @ "_hi".setBitmap(%this.frameButton);
    1.setVisible(%this.toggleCartButton);
    %buttonsDir @ %cartBitmap.setBitmap(%this.toggleCartButton);
    1.setVisible(%this.buyNowButton);
    1.setVisible(%this.buttonBacking);
    if (isObject(StoreItemDescHiliteFrame)) {
        1.setVisible(StoreItemDescHiliteFrame);
    }
    if (isObject(StoreFloatingHiliteFrame)) {
        1.setVisible(StoreFloatingHiliteFrame);
        %screenPos = StoreFloatingHiliteFrame.getScreenPosition();
        %pos = StoreFloatingHiliteFrame.getPosition();
        %offsetX = (getWord(%screenPos, 0) - getWord(%pos, 0));
        %offsetY = (getWord(%screenPos, 1) - getWord(%pos, 1));
        %newPosX = ((getWord(%this.getScreenPosition(), 0) - %offsetX) - 9.0);
        %newPosY = ((getWord(%this.getScreenPosition(), 1) - %offsetY) - 4.0);
        %newPosY.reposition(StoreFloatingHiliteFrame, %newPosX);
        %scroll = %this.thumbnails.scroll;
        %padding = 10;
        %minx = (getWord(%scroll.getScreenPosition(), 0) - %padding);
        %minY = (getWord(%scroll.getScreenPosition(), 1) - %padding);
        %maxX = ((%minx + getWord(%scroll.getExtent(), 0)) + (2.0 * %padding));
        %maxy = ((%minY + getWord(%scroll.getExtent(), 1)) + (2.0 * %padding));
        %posX = getWord(%this.getScreenPosition(), 0);
        %posY = getWord(%this.getScreenPosition(), 1);
        %width = getWord(%this.getExtent(), 0);
        %height = getWord(%this.getExtent(), 1);
        if ((%posX >= %minx)) {
        }
        if (((%posX + %width) <= %maxX)) {
        }
        if ((%posY >= %minY)) {
        }
        ((%posY + %height) <= %maxy).setVisible(StoreFloatingHiliteFrame);
    }
};
function ClosetThumbnailCtrl::onUnhilite(%this) {
    %this.frameButton.mouseOver = 0;
    if (0) {
        if (isObject(BodyLongDescText)) {
            "".setText(BodyShortDescText);
            "".setText(BodyLongDescText);
        }
        if (isObject(ClosetLongDescText)) {
            "".setText(ClosetShortDescText);
            "".setText(ClosetLongDescText);
        }
        if (isObject(StoreLongDescText)) {
            StoreShortDescText.showBaseDesc();
            StoreLongDescText.showBaseDesc();
        }
        "".updateAuthorWidget(ClosetTabs);
    }
    if (ClosetTabs.tabShopsInitialized) {
    }
    if ((%this.thumbnails == ClosetThumbnailsShop.getId())) {
        %this.hilited = 0;
        %buttonsDir = "platform/client/buttons/";
        %frameBitmap = %this.selected ? "frame_sel" : "frame";
        %cartBitmap = %this.sku.containsSku(StoreShoppingList) ? "removeFromCart" : "add2cart";
        %buttonsDir @ %frameBitmap.setBitmap(%this.frameButton);
        0.setVisible(%this.toggleCartButton);
        %buttonsDir @ %cartBitmap.setBitmap(%this.toggleCartButton);
        0.setVisible(%this.buyNowButton);
        0.setVisible(%this.buttonBacking);
        if (isObject(StoreItemDescHiliteFrame)) {
            0.setVisible(StoreItemDescHiliteFrame);
        }
        if (isObject(StoreFloatingHiliteFrame)) {
            0.setVisible(StoreFloatingHiliteFrame);
        }
    }
};
function ClosetThumbnailCtrl::onSelect(%this) {
    %this.frameButton.performClick();
};
function ClosetThumbnailCtrl::onMouseLeaveBounds(%this) {
    %this.onUnhilite();
};
function ClosetThumbnailCtrl::addToCart(%this) {
    %this.sku.addSku(StoreShoppingList);
};
function ClosetThumbnailCtrl::toggleInCart(%this) {
    if (%this.sku.containsSku(StoreShoppingList)) {
        %this.sku.removeSku(StoreShoppingList);
    }
    %this.sku.addSku(StoreShoppingList);
};
function ClosetThumbnailCtrl::buyNow(%this) {
    if (%this.sku) {
        %this.sku.purchaseSkus(ClosetGui);
    }
};
function ClosetFrameButton::onMouseEnter(%this) {
    %thumbnails = %this.thumbnails;
    %i = %this.ctrl.getObjectIndex(%this.thumbnails);
    %row = mFloor((%i / %thumbnails.numRowsOrCols));
    %col = (%i % %thumbnails.numRowsOrCols);
    %row.hiliteCell(%thumbnails, %col);
};
$gAllOutfits = "A B C D E F G H I J K L";
$Player::HangerNames["f"] = "fA fB fC fD fE fF fG fH fI fJ fK fL";
$Player::HangerNames["m"] = "mA mB mC mD mE mF mG mH mI mJ mK mL";
$gClosetNumOutfits = getWordCount($Player::HangerNames["f"]);
$ClosetOutfitName = "";
function ClosetGui::open(%this) {
    echo(getScopeName() @ "->debug for ETS-8039, $ClosetOutfitName at closet opening is: " @ $ClosetOutfitName);
    if (!(isObject($player))) {
        error(getScopeName() @ " " @ "- no player" @ " " @ getTrace());
        return;
    }
    %this.oldHeight = $UserPref::Player::height;
    %this.oldStance = $UserPref::Player::Genre;
    %this.currentOverrideGenre = $player.getGenre();
    %this.wasInHelpmode = $player.isInHelpMeMode();
    %this.oldAnimation = $player.getCurrActionName();
    %this.updateLocation(GuiTracker);
    closetMap.push();
    DestroyMessageBoxes();
    $ClosetOutfitName = $player.getGender() @ "currentOutfit".get($gOutfits);
    $ClosetSkusBody = $player.getGender() @ "Body".get($gOutfits);
    %outfitNames = [$player.getGender()];
    $Player::HangerNames;
    %i = 0;
    while ((%i < $gClosetNumOutfits)) {
        %name = getWord(%outfitNames, %i);
        %name[$ClosetSkusOutfit @ %name] = %name.get($gOutfits);
        %i = (%i + 1.0);
    }
    checkOutfitCorruption(1);
    ClosetTabs.setup();
    if (($player.isSitting() == 1.0)) {
        if (($IN_ORBIT_CAM == 0.0)) {
            togglePlayerCamMode();
        }
        if (($player.isKissSeat == 1.0)) {
            commandToServer('RequestToStand', 0, 0);
        }
    }
    if (($IN_ORBIT_CAM == 1.0)) {
        togglePlayerCamMode();
    }
    $player.setSimObject(ClosetMainObjectView);
    %this.setContent(Canvas);
    1.setVisible(%this);
    setIdle(1, $ClosetGuiOpenMessage);
    1.setActivityActive(getUserActivityMgr(), "dressing");
    ClosetGui.updateVisibleAvatar();
    "".zoomToSKU(ClosetMainObjectView);
    "debugPassive".rolesPermissionCheckNoWarn($player).setVisible(ClosetStaffPanelContainer);
    if (((%i < $gClosetNumOutfits) @ " " @ $UserPref::Player::Genre $= "h")) {
    }
    if (($UserPref::Player::Genre $= "i")) {
    }
    if (($UserPref::Player::Genre $= "p")) {
    }
    if (($UserPref::Player::Genre $= "t")) {
    }
    if (($UserPref::Player::Genre $= "s")) {
        $UserPref::Player::Genre.selectGenre(ClosetGui);
    }
    pushScreenSize(960, 544, 0, 1, 1);
    %closetGuiFUEIsObject = isObject(ClosetGuiFUE);
    if (isInFUE()) {
        if (!(%closetGuiFUEIsObject)) {
            "".execHideAndAddChild(ClosetGuiPositioner, "./closetGuiFUE.gui");
        }
        ClosetGuiFUE.open();
    }
    if (%closetGuiFUEIsObject) {
    }
    if (ClosetGuiFUE.isVisible()) {
        ClosetGuiFUE.close();
    }
    if (!($Player::hasSeenTakeAvatarPhotoDialog)) {
        if (!(ClosetTabs.tabSnapshotInitialized)) {
            ClosetTabs.fillProfileTab();
        }
        "".update(ProfileCurrentPicture);
    }
    if (isObject(ProfileObjectView)) {
        ProfileObjectView.resetLight();
    }
};
function ClosetGui::close(%this, %cancel) {
    1.doClose(%this, %cancel);
};
function ClosetGui::doClose(%this, %cancel, %allowMsgBoxOnExit) {
    if (%this.isWaitingForPurchaseCompletion()) {
        return 0;
    }
    if ((ClosetTabs.getCurrentTab().name $= "MY DESIGNS")) {
    }
    if (MyShopTextureInspector.isVisible()) {
        MyShopTextureInspector.close();
        return 0;
    }
    if ((ClosetTabs.getCurrentTab().name $= "Shops")) {
        %i = (getWordCount($StoreSkusLayer) - 1.0);
        while ((%i >= 0.0)) {
            %aTriedOnSku = getWord($StoreSkusLayer, %i);
            if (((%skuIndex = findWord($Player::inventory, %aTriedOnSku)) >= 0.0)) {
                if (%aTriedOnSku.isBodySku(SkuManager)) {
                }
                if ((findWord($ClosetSkusBody, %aTriedOnSku) < 0.0)) {
                    $ClosetSkusBody = %aTriedOnSku.overlaySkus(SkuManager, $ClosetSkusBody);
                }
                if (%aTriedOnSku.isOutfitSku(SkuManager)) {
                }
                if ((findWord($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], %aTriedOnSku) < 0.0)) {
                    $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = %aTriedOnSku.overlaySkus(SkuManager, $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName]);
                }
            }
            %i = (%i - 1.0);
        }
        saveStorePosition();
    }
    if (%allowMsgBoxOnExit) {
    }
    if (((%i >= 0.0) @ " " @ ClosetTabs.getCurrentTab().name $= "SHOPS")) {
    }
    if ((StoreShoppingList.getCount() > 0.0)) {
    }
    if (!(Inventory::getCurrentStoreName() $= "")) {
        MessageBoxYesNo("Leave the Store?", "You still have items in your shopping cart that you haven't bought." @ "\n" @ "<spush><b>Do you really want to return to the world?<spop>" @ "\n" @ "(The items will stay in your cart.)", "ClosetGui.reallyClose(" @ %cancel @ ");", "");
        return 0;
    }
    if (%this.askUserToDropProp()) {
        return 0;
    }
    if (%allowMsgBoxOnExit) {
    }
    if (!(%cancel)) {
    }
    if (!(0.getProperty(gUserPropMgrClient, $Player::Name, "hasTakenAvatarPhoto"))) {
    }
    if (!($Player::hasSeenTakeAvatarPhotoDialog)) {
    }
    if (!($StandAlone)) {
        $Player::hasSeenTakeAvatarPhotoDialog = 1;
        MessageBoxYesNo($MsgCat::closet["MSG-NO-AVATAR-PHOTO-TITLE"], $MsgCat::closet["MSG-NO-AVATAR-PHOTO"], "ClosetTabs.selectTabWithName(\"SNAPSHOT\");", "ClosetGui.reallyClose(" @ %cancel @ ");");
        return 0;
    }
    %cancel.reallyClose(%this);
};
function ClosetGui::reallyClose(%this, %cancel) {
    stopPropAction();
    closetMap.pop();
    if (isObject(StoreSpecificBackground)) {
        0.setVisible(StoreSpecificBackground);
    }
    %this.doResetGenre();
    if ((%cancel $= "")) {
        %cancel = 0;
    }
    if (%cancel) {
        %this.doCancel();
    }
    %this.doOkay();
    if (GuiTracker.inTransit) {
        GuiTracker.previouslyOpened.setContent(Canvas);
    }
    PlayGui.setContent(Canvas);
    0.setVisible(%this);
    nextPlayerCamMode();
    setIdle(0);
    if (($gSalonChairCurrent != 0.0)) {
        if (!(%this.oldAnimation $= "")) {
            %this.oldAnimation.playAnim($player);
        }
    }
    $player.getGender() @ $player.getGenre() @ "idl1a".playAnim($player);
    %this.oldAnimation = "";
    0.setActivityActive(getUserActivityMgr(), "dressing");
    popScreenSize();
    if (!(0.getProperty(gUserPropMgrClient, $Player::Name, "hasSeenPropUseAdvisory"))) {
    }
    if ($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].hasPropSku(SkuManager)) {
        1.setProperty(gUserPropMgrClient, $Player::Name, "hasSeenPropUseAdvisory");
        MessageBoxOK($MsgCat::closet["MSG-USE-PROP-TITLE"], $MsgCat::closet["MSG-USE-PROP-BODY"], "");
    }
    Inventory::fetchPlayerInventoryIfEmpty();
};
function ClosetGui::doResetCurrent(%this) {
    checkOutfitCorruption(1);
    $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = $ClosetOutfitName.get($gOutfits);
    %this.updateVisibleAvatar();
    "".zoomToSKU(ClosetMainObjectView);
    ClosetItemsFrame.update();
};
function ClosetGui::doResetGenre(%this) {
    $UserPref::Player::Genre = %this.oldStance;
    if (!(%this.currentOverrideGenre $= $UserPref::Player::Genre)) {
        %this.currentOverrideGenre.setGenre($player);
    }
    $UserPref::Player::Genre.setGenre($player);
};
function ClosetGui::doResetAll(%this) {
    $UserPref::Player::height = %this.oldHeight;
    $ClosetOutfitName = $player.getGender() @ "currentOutfit".get($gOutfits);
    %outfitNames = [$player.getGender()];
    $Player::HangerNames;
    %i = 0;
    while ((%i < $gClosetNumOutfits)) {
        %name = getWord(%outfitNames, %i);
        %name[$ClosetSkusOutfit @ %name] = %name.get($gOutfits);
        %i = (%i + 1.0);
    }
    $ClosetSkusBody = $player.getGender() @ "Body".get($gOutfits);
    (%i < $gClosetNumOutfits);
    %this.updateVisibleAvatar();
    "".zoomToSKU(ClosetMainObjectView);
    ClosetTabs.updateBodyTabDisplay();
};
function ClosetGui::doCancel(%this) {
    if (checkOutfitCorruption(1)) {
        outfitsCorruptedNotify();
    }
    %this.doResetAll();
    $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] @ " " @ $ClosetSkusBody.setActiveSKUs($player);
};
function ClosetGui::doOkay(%this) {
    strupr(getSubStr($ClosetOutfitName, 1, 1)).put($gOutfits, "currentOutfit");
    $ClosetSkusBody.put($gOutfits, $player.getGender() @ "Body");
    %outfitNames = [$player.getGender()];
    $Player::HangerNames;
    %n = ($gClosetNumOutfits - 1.0);
    while ((%n >= 0.0)) {
        %name = getWord(%outfitNames, %n);
        %name[$ClosetSkusOutfit @ %name] = outfits_filterSKUList(%name[$ClosetSkusOutfit @ %name]);
        %name[$ClosetSkusOutfit @ %name].put($gOutfits, %name);
        %n = (%n - 1.0);
    }
    if (checkOutfitCorruption(1)) {
        outfitsCorruptedNotify();
        return 0;
    }
    outfits_persist();
    %activeSkus = $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] @ " " @ $ClosetSkusBody;
    if (%this.wasInHelpmode) {
        %activeSkus = %activeSkus @ " " @ getSpecialSKU(0, "helpmebadge");
    }
    commandToServer('SetActiveSkus', %activeSkus);
    commandToServer('setHeight', $UserPref::Player::height);
    $UserPref::Player::height.setProperty(gUserPropMgrClient, $Player::Name, "avatarHeight");
    %playerActiveSKUs = $player.getActiveSKUs();
    if (%this.wasInHelpmode) {
        %playerActiveSKUs = %playerActiveSKUs @ " " @ getSpecialSKU(0, "helpmebadge");
    }
    %playerActiveSKUs.schedule($player, 0, "setActiveSkus");
    if (isObject(closetGuiFUEHideTipsCtrl)) {
        1.setValue(closetGuiFUEHideTipsCtrl);
        closetGuiFUEHideTipsCtrl.onAction();
    }
};
function ClosetGui::askUserToDropProp(%this) {
    %propSku = "props".filterSkusDrwr(SkuManager, $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName]);
    if ((%propSku $= "")) {
        return 0;
    }
    if (canHavePropsInGenre(%this.currentOverrideGenre)) {
        return 0;
    }
    %index = findWord($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], %propSku);
    %outfitWithoutProp = removeWord($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], %index);
    %activity = "engaging in this activity";
    if ((%this.currentOverrideGenre $= "k")) {
        %activity = "skating";
    }
    if ((%this.currentOverrideGenre $= "w")) {
        %activity = "swimming";
    }
    if ((%this.currentOverrideGenre $= "o")) {
        %activity = "sumo wrestling";
    }
    if ((%this.currentOverrideGenre $= "l")) {
        %activity = "pillow fighting";
    }
    if ((%this.currentOverrideGenre $= "s")) {
        %activity = "strutting your stuff";
    }
    %body = $MsgCat::closet["MSG-NO-PROP-IN-THIS-GENRE-BODY1"] @ " " @ %activity @ %activity[$MsgCat::closet @ "MSG-NO-PROP-IN-THIS-GENRE-BODY2"];
    MessageBoxYesNo($MsgCat::closet["MSG-NO-PROP-IN-THIS-GENRE-TITLE"], %body, "$ClosetSkusOutfit[$ClosetOutfitName] = \"" @ %outfitWithoutProp @ "\"; ClosetGui.reallyClose(false);", "");
    return 1;
};
function ClosetGui::userHasChangedBodyOrOutfit(%this) {
    if (!($ClosetSkusBody $= $player.getGender() @ "Body".get($gOutfits))) {
        return 1;
    }
    if (!("currentOutfit".get($gOutfits) $= strupr(getSubStr($ClosetOutfitName, 1, 1)))) {
        return 1;
    }
    %outfitNames = [$player.getGender()];
    $Player::HangerNames;
    %n = ($gClosetNumOutfits - 1.0);
    while ((%n >= 0.0)) {
        %name = getWord(%outfitNames, %n);
        %newOutfit = outfits_filterSKUList(%name[$ClosetSkusOutfit @ %name]);
        %oldOutfit = %name.get($gOutfits);
        if (!(%newOutfit $= %oldOutfit)) {
            return 1;
        }
        %n = (%n - 1.0);
    }
    return 0;
};
function ClosetGui::purchaseSkus(%this, %skus) {
    if (%this.isWaitingForPurchaseCompletion()) {
        MessageBoxOK("Processing Previous Purchase", "We're working hard to process the purchase you just made. Please wait a minute and try again.", "");
        return;
    }
    %cbPoints = "ClosetGui.purchaseSkusVPoints(\"" @ %skus @ "\");";
    %cbBux = "ClosetGui.purchaseSkusVBux   (\"" @ %skus @ "\");";
    %cbCancel = "";
    ShowPurchaseSkusConfirmationDialog(%skus, %cbPoints, %cbBux, %cbCancel);
};
function ShowPurchaseSkusConfirmationDialog(%skus, %cbPoints, %cbBux, %cbCancel) {
    %numSkus = getWordCount(%skus);
    if ((%numSkus == 0.0)) {
    }
    if ((%skus $= 0)) {
        error(getScopeName() @ " " @ "- no skus!" @ " " @ getTrace());
        return 0;
    }
    %skusValidPoints = Inventory::filterSkusByValidPrice("vPoints", %skus);
    %skusValidBux = Inventory::filterSkusByValidPrice("vBux", %skus);
    %numSkusValidPoints = getWordCount(%skusValidPoints);
    %numSkusValidBux = getWordCount(%skusValidBux);
    if ((%numSkus == 1.0)) {
    }
    %itemsStr = "these" @ " " @ %numSkus @ " " @ "items";
    "this item";
    %pointsTotal = Inventory::getTotalPrice("vPoints", %skus);
    %buxTotal = Inventory::getTotalPrice("vBux", %skus);
    if ((%pointsTotal == 0.0)) {
    }
    if ((%numSkusValidPoints > 0.0)) {
        eval(%cbBux);
        return;
    }
    if ((%buxTotal == 0.0)) {
    }
    if ((%numSkusValidBux > 0.0)) {
        eval(%cbPoints);
        return;
    }
    %pointsTotal = "   " @ %pointsTotal;
    %buxTotal = "   " @ %buxTotal;
    %mbTitle = "Choose Currency";
    %mbBody = "Do you want to buy " @ %itemsStr @ "<br>with vPoints or with vBux?";
    %mbPointsNote = "";
    %mbBuxNote = "";
    %mbButtons = %pointsTotal @ "\t" @ %buxTotal @ "\t" @ "Cancel";
    %mbCBPoints = %cbPoints;
    %mbCBBux = %cbBux;
    if ((%numSkusValidPoints < %numSkus)) {
    }
    if ((%numSkusValidBux < %numSkus)) {
        if ((%numSkus == 1.0)) {
            %mbTitle = "Can't Buy This Item";
            %mbBody = "This item is not currently available for purchase.";
            %mbButtons = "OK";
            %mbCBPoints = "";
            %mbCBBux = "";
        }
        %mbTitle = "Can't Buy All Items";
        %mbBody = "In order to purchase all items in your cart at once, they must <spush><b>all<spop> be available for either vPoints or vBux (or both!).<br><br>You can purchase the items in your cart individually, or you can remove some items and try again.";
        %mbButtons = "OK";
        %mbCBPoints = "";
        %mbCBBux = "";
    }
    if ((%numSkusValidPoints < %numSkus)) {
        %mbTitle = "Confirm Currency";
        %mbBody = "Do you want to buy " @ %itemsStr @ " with vBux?";
        if ((%numSkus > 1.0)) {
            %mbPointsNote = "<br><br>(Some or all are not available for vPoints.)";
        }
        %mbPointsNote = "<br><br>(This item is not available for vPoints.)";
        %mbButtons = %buxTotal @ "\t" @ "Cancel";
        %mbCBPoints = "";
    }
    if ((%numSkusValidBux < %numSkus)) {
        %mbTitle = "Confirm Currency";
        %mbBody = "Do you want to buy " @ %itemsStr @ " with vPoints?";
        if ((%numSkus > 1.0)) {
            %mbBuxNote = "<br><br>(Some or all are not available for vBux.)";
        }
        %mbBuxNote = "<br><br>(This item is not available for vBux.)";
        %mbButtons = %pointsTotal @ "\t" @ "Cancel";
        %mbCBBux = "";
    }
    %dialog = MessageBoxCustom(%mbTitle, %mbBody @ %mbPointsNote @ %mbBuxNote, %mbButtons);
    %buttonIndex = 0;
    if (!(%mbCBPoints $= "")) {
        %dialog.callback = %mbCBPoints @ %buttonIndex;
        new GuiBitmapCtrl("") {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "7 2";
            extent = "7 13";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = "platform/client/ui/vpoints_9";
        };.add(%buttonIndex, %dialog.button);
        %buttonIndex = (%buttonIndex + 1.0);
    }
    if (!(%mbCBBux $= "")) {
        %dialog.callback = %mbCBBux @ %buttonIndex;
        new GuiBitmapCtrl("") {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "7 2";
            extent = "7 13";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = "platform/client/ui/vbux_9";
        };.add(%buttonIndex, %dialog.button);
        %buttonIndex = (%buttonIndex + 1.0);
    }
    %dialog.callback = %cbCancel @ %buttonIndex;
    return %dialog;
};
function ClosetGui::doInsufficientVPoints() {
    MessageBoxOK("Not Enough vPoints", "You do not have enough vPoints to purchase all the items in your shopping cart.  Click <a:" @ $Net::HelpURL_VPoints @ ">here</a> for more information about earning vPoints.", "");
};
function ClosetGui::doInsufficientVBux() {
    MessageBoxOK("Not Enough vBux", "You do not have enough vBux to purchase all the items in your shopping cart.  Click <a:" @ $Net::AddFundsURL @ ">here</a> to refill your account.", "");
};
function ClosetGui::purchaseSkusVPoints(%this, %skus) {
    %numSkus = getWordCount(%skus);
    %skus = Inventory::filterSkusByValidPrice("vPoints", %skus);
    %numValidSkus = getWordCount(%skus);
    if ((%numValidSkus == 0.0)) {
        if ((%numSkus == 1.0)) {
            MessageBoxOK("Not Available", "This item is not available for vPoints.", "");
        }
        MessageBoxOK("Not Available", "These items are not available for vPoints.", "");
        return;
    }
    %totalPrice = Inventory::getTotalPrice("vPoints", %skus);
    if ((%totalPrice > $Player::VPoints)) {
        %this.doInsufficientVPoints();
        return;
    }
    %desc = "these " @ %numValidSkus @ " items";
    if ((%numValidSkus == 1.0)) {
        %si = %skus.findBySku(SkuManager);
        if (!(isObject(%si))) {
            return;
        }
        %desc = %si.descShrt;
    }
    %vpointsString = (%totalPrice == 1.0) ? "vPoint" : "vPoints";
    %note = "";
    if ((%numValidSkus > %numSkus)) {
        %diff = (%numSkus - %numValidSkus);
        %itemsString = (%diff == 1.0) ? "item is" : "items are";
        %note = "<br><br>Note: " @ %diff @ " " @ %itemsString @ " not available for vPoints.";
    }
    %msg = "Do you wish to purchase " @ %desc @ " for " @ %totalPrice @ " " @ %vpointsString @ "?" @ %note;
    %cmd = "ClosetGui.purchaseSkusReally(\"" @ %skus @ "\", \"vPoints\");";
    MessageBoxOkCancel("Confirm Purchase", %msg, %cmd, "");
};
function ClosetGui::purchaseSkusVBux(%this, %skus) {
    %numSkus = getWordCount(%skus);
    %skus = Inventory::filterSkusByValidPrice("vBux", %skus);
    %numValidSkus = getWordCount(%skus);
    if ((%numValidSkus == 0.0)) {
        if ((%numSkus == 1.0)) {
            MessageBoxOK("Not Available", "This item is not available for vBux.", "");
        }
        MessageBoxOK("Not Available", "These items are not available for vBux.", "");
        return;
    }
    %totalPrice = Inventory::getTotalPrice("vBux", %skus);
    if ((%totalPrice > $Player::VBux)) {
        %this.doInsufficientVBux();
        return;
    }
    %desc = "these " @ %numValidSkus @ " items";
    if ((%numValidSkus == 1.0)) {
        %si = %skus.findBySku(SkuManager);
        if (!(isObject(%si))) {
            return;
        }
        %desc = %si.descShrt;
    }
    %note = "";
    if ((%numValidSkus > %numSkus)) {
        %diff = (%numSkus - %numValidSkus);
        %itemsString = (%diff == 1.0) ? "item is" : "items are";
        %note = "<br><br>Note: " @ %diff @ " " @ %itemsString @ " not available for vBux.";
    }
    %msg = "Do you wish to purchase " @ %desc @ " for " @ %totalPrice @ " vBux?" @ %note;
    %cmd = "ClosetGui.purchaseSkusReally(\"" @ %skus @ "\", \"vBux\");";
    MessageBoxOkCancel("Confirm Purchase", %msg, %cmd, "");
};
function ClosetGui::doCheckout(%this) {
    StoreShoppingList.getSkus().purchaseSkus(%this);
};
function ClosetGui::purchaseSkusReally(%this, %skus, %currency) {
    %array = new Array("");
    %n = (getWordCount(%skus) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%skus, %n);
        1.push_front(%array, %sku);
        %n = (%n - 1.0);
    }
    %request = sendRequest_PurchaseInventory($Player::Name, %array, %currency, $gCurrentStoreName, "closet_onDoneOrErrorCallback_PurchaseInventory");
    (%n >= 0.0);
    %request.currency = %currency;
    %request.callbackData = %this;
    %request.timedOutAlready = 0;
    %array.delete();
    1.setVisible(StoreShoppingBag.waitIcon);
    StoreShoppingBag.waitIcon.start();
    %this.numberOfPurchasesAwaitingCompletion = (%this.numberOfPurchasesAwaitingCompletion + 1.0);
    %tab = "SHOPS".getTabWithName(ClosetTabs);
    !(%this.isWaitingForPurchaseCompletion()).setActive(%tab.doneButton);
    !(%this.isWaitingForPurchaseCompletion()).setActive(%tab.cancelButton);
    %this.checkoutPopup = MessageBoxOK($MsgCat::closet["MSG-PROCESSING-PURCHASE-TITLE"], $MsgCat::closet["MSG-PROCESSING-PURCHASE"], "");
    %request.schedule(%this, 30000, purchaseSkusRequestTimedOut);
};
function ClosetGui::purchaseSkusRequestTimedOut(%this, %request) {
    if (!(isObject(%request))) {
    }
    if ((%request.statusCode() $= "")) {
        return;
    }
    %request.timedOutAlready = 1;
    %this.numberOfPurchasesPastTimeout = (%this.numberOfPurchasesPastTimeout + 1.0);
    %tab = "SHOPS".getTabWithName(ClosetTabs);
    !(%this.isWaitingForPurchaseCompletion()).setActive(%tab.doneButton);
    !(%this.isWaitingForPurchaseCompletion()).setActive(%tab.cancelButton);
    %this.processingTimeoutPopup = MessageBoxOK($MsgCat::closet["MSG-PROCESSING-TIMEOUT-TITLE"], $MsgCat::closet["MSG-PROCESSING-TIMEOUT"], "");
};
function ClosetGui::isWaitingForPurchaseCompletion(%this) {
    return (%this.numberOfPurchasesAwaitingCompletion > %this.numberOfPurchasesPastTimeout);
};
function closet_onDoneOrErrorCallback_PurchaseInventory(%request) {
    %request.onDoneOrErrorCallback_PurchaseInventory(%request.callbackData);
};
function ClosetGui::onDoneOrErrorCallback_PurchaseInventory(%this, %request) {
    StoreShoppingBag.waitIcon.stop();
    0.setVisible(StoreShoppingBag.waitIcon);
    %this.numberOfPurchasesAwaitingCompletion = (%this.numberOfPurchasesAwaitingCompletion - 1.0);
    if (%request.timedOutAlready) {
        %this.numberOfPurchasesPastTimeout = (%this.numberOfPurchasesPastTimeout - 1.0);
    }
    %tab = "SHOPS".getTabWithName(ClosetTabs);
    !(%this.isWaitingForPurchaseCompletion()).setActive(%tab.doneButton);
    !(%this.isWaitingForPurchaseCompletion()).setActive(%tab.cancelButton);
    if (isObject(%this.checkoutPopup)) {
        %this.checkoutPopup.close();
    }
    if (isObject(%this.processingTimeoutPopup)) {
        %this.processingTimeoutPopup.close();
    }
    if (!(isObject(%request))) {
        error(getScopeName() @ " " @ "- no request! this may be because we didn't send it in alpha 1");
        return;
    }
    %skuResults["pass"] = "";
    %skuResults["NotForSale"] = "";
    %skuResults["OutOfStock"] = "";
    %skuResults["NoUsageRights"] = "";
    %skuResults["InsufficientFunds"] = "";
    %n = ("itemsCount".getValue(%request) - 1.0);
    while ((%n >= 0.0)) {
        %sku = "items" @ %n @ ".sku".getValue(%request);
        %validationResults = "items" @ %n @ ".validationResults".getValue(%request);
        %m = (getFieldCount(%validationResults) - 1.0);
        while ((%m >= 0.0)) {
            %validationResult = getField(%validationResults, %m);
            %validationResult[%skuResults @ %validationResult] = %validationResult[%skuResults @ %validationResult] @ %sku @ " ";
            %m = (%m - 1.0);
        }
        %n = (%n - 1.0);
        (%m >= 0.0);
    }
    if (!(%request.checkSuccess())) {
        %errorCode = "errorCode".getValue(%request);
        (%n >= 0.0);
        if ((%errorCode $= "staleInventory")) {
            %request = sendRequest_GetStoreInventory($Player::Name, $gCurrentStoreName, "OnGotDoneOrError_GetStoreInventory");
            %request.shoppingCartSkus = StoreShoppingList.getSkus();
            StoreShoppingList.clear();
        }
        if ((%errorCode $= "insufficientTotalFunds")) {
            %msgName = (%request.currency $= "vpoints") ? "E-NO-VPOINTS" : "E-NO-VBUX";
            MessageBoxOK($MsgCat::commerce["E-TITLE"], %msgName[$MsgCat::commerce @ %msgName], "");
        }
        if ((%errorCode $= "unacquirableItems")) {
            if (!(%errorCode[%skuResults @ "OutOfStock"] $= "")) {
                MessageBoxYesNo(%errorCode[%skuResults @ "OutOfStock"][$MsgCat::commerce @ "E-TITLE"], $MsgCat::commerce["E-SOLDOUT"], "StoreShoppingList.removeSkus(\"" @ %skuResults["OutOfStock"] @ "\");", "");
            }
            MessageBoxOK($MsgCat::commerce["E-TITLE"], $MsgCat::commerce["E-UNKNOWN"], "");
        }
        MessageBoxOK($MsgCat::commerce["E-TITLE"], $MsgCat::commerce["E-UNKNOWN"], "");
    }
    %request.timedOutAlready.handleAnyPurchasedSkus(%this, %skuResults["pass"]);
};
function ClosetGui::handleAnyPurchasedSkus(%this, %skulist, %delayed) {
    %skusToFlatten = "";
    %skusPurchased = %skulist;
    %n = (getWordCount(%skulist) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%skulist, %n);
        if ((findWord($Player::inventory, %sku) == -(1.0))) {
            $Player::inventory = %sku @ " " @ $Player::inventory;
        }
        error(getScopeName() @ " " @ "- already have SKU:" @ " " @ %sku);
        if ((%sku[$gStoreItemsQty @ %sku] > 0.0)) {
            %sku[$gStoreItemsQty @ %sku] = (%sku[$gStoreItemsQty @ %sku] - 1.0);
        }
        if ((findWord($StoreSkusLayer, %sku) != -(1.0))) {
            %skusToFlatten = %skusToFlatten @ " " @ %sku;
        }
        %n = (%n - 1.0);
    }
    if (!((%n >= 0.0) @ " " @ %skulist $= "")) {
        %callback = "StoreShoppingList.removeSkus(\"" @ %skusPurchased @ "\");";
        if (%delayed) {
            MessageBoxOK("Purchase Complete", $MsgCat::commerce["S-PURCHASE-DELAYED"], %callback);
        }
        MessageBoxOK("Purchase Complete", $MsgCat::commerce["S-PURCHASE"], %callback);
    }
    if (!(%skusToFlatten $= "")) {
        %skusToFlatten = trim(%skusToFlatten);
        %newStoreSkus = "";
        %n = (getWordCount($StoreSkusLayer) - 1.0);
        while ((%n >= 0.0)) {
            %sku = getWord($StoreSkusLayer, %n);
            if (!(hasWord(%skusToFlatten, %sku))) {
                %newStoreSkus = %newStoreSkus @ " " @ %sku;
            }
            %n = (%n - 1.0);
        }
        $StoreSkusLayer = trim(%newStoreSkus);
        (%n >= 0.0);
        %skusToFlattenClothing = %skusToFlatten.filterSkusForClothing(SkuManager);
        %skusToFlattenBody = %skusToFlatten.filterSkusForBody(SkuManager);
        $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = %skusToFlattenClothing.overlaySkus(SkuManager, $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName]);
        $ClosetSkusBody = %skusToFlattenBody.overlaySkus(SkuManager, $ClosetSkusBody);
    }
    StoreItemsFrame.update();
};
function CheckoutRequest::onClosed(%this) {
};
function CheckoutRequest::onError(%this, %unused, %unused) {
    StoreShoppingBag.waitIcon.stop();
    0.setVisible(StoreShoppingBag.waitIcon);
    if (isObject(ClosetGui.checkoutPopup)) {
        ClosetGui.checkoutPopup.close();
    }
    MessageBoxOK("Connection Error", $MsgCat::network["E-SERVER-CONNECT"], "");
    %this.onClosed();
};
function CheckoutRequest::onDone(%this) {
    log("network", "debug", getScopeName() @ " " @ "- url =" @ " " @ %this.getURL());
    StoreShoppingBag.waitIcon.stop();
    0.setVisible(StoreShoppingBag.waitIcon);
    if (isObject(ClosetGui.checkoutPopup)) {
        ClosetGui.checkoutPopup.close();
    }
    %status = findRequestStatus(%this);
    %ownsAlready = 0;
    %buyFailedInsufVBux = 0;
    %buyFailedInsufVPoints = 0;
    %buyFailed = 0;
    if ((%status $= "connect-failed")) {
        MessageBoxOK("Could not connect", "Could not connect to " @ $ETS::AppName @ " servers.  " @ $ETS::AppName[$MsgCat::network @ "H-SYS-DOWN"] @ $ETS::AppName[$MsgCat::network @ "H-SYS-DOWN"][$MsgCat::network @ "H-SEE-FORUMS"], "");
    }
    if ((%status $= "fail")) {
        MessageBoxOK("Error With Account Data", "There was an error with your request.  If you continue to see this error, try logging out and logging back in again.", "");
    }
    if ((%status $= "success")) {
        %skusToFlatten = "";
        %skusSoldOut = "";
        %skusPurchased = "";
        %skusAborted = "";
        %i = 1;
        while (1) {
            %line = "sku" @ %i.getValue(%this);
            if ((%line $= "")) {
            }
            %sku = getField(%line, 0);
            %result = getField(%line, 1);
            if ((%result $= "buy_ok")) {
                %skusPurchased = %skusPurchased @ " " @ %sku;
                if ((findWord($Player::inventory, %sku) == -(1.0))) {
                    $Player::inventory = %sku @ " " @ $Player::inventory;
                }
                error(getScopeName() @ " " @ "- already have SKU:" @ " " @ %sku);
                if ((%sku[$gStoreItemsQty @ %sku] > 0.0)) {
                    %sku[$gStoreItemsQty @ %sku] = (%sku[$gStoreItemsQty @ %sku] - 1.0);
                }
                if ((findWord($StoreSkusLayer, %sku) != -(1.0))) {
                    %skusToFlatten = %skusToFlatten @ " " @ %sku;
                }
            }
            if ((%result $= "buy_aborted")) {
                %skusAborted = %skusAborted @ " " @ %sku;
            }
            if ((%result $= "buy_owns_already")) {
                %ownsAlready = 1;
            }
            if ((%result $= "buy_failed_insufficient_vbux")) {
                %buyFailedInsufVBux = 1;
            }
            if ((%result $= "buy_failed_insufficient_vpoints")) {
                %buyFailedInsufVPoints = 1;
            }
            if ((%result $= "buy_failed_sold_out")) {
                %skusSoldOut = %skusSoldOut @ " " @ %sku;
            }
            %buyFailed = 1;
            %i = (%i + 1.0);
        }
        %msg = "";
        1;
        if (%ownsAlready) {
            %msg = %msg @ %msg[$MsgCat::commerce @ "E-ALREADYOWN"] @ "\n\n";
        }
        if (%buyFailedInsufVBux) {
            %msg = %msg @ %msg[$MsgCat::commerce @ "E-NO-VBUX"] @ "\n\n";
        }
        if (%buyFailedInsufVPoints) {
            %msg = %msg @ %msg[$MsgCat::commerce @ "E-NO-VPOINTS"] @ "\n\n";
        }
        if (%buyFailed) {
            %msg = %msg @ %msg[$MsgCat::commerce @ "F-PURCHASE"] @ "\n\n";
        }
        if (!(%skusAborted $= "")) {
            %msg = %msg @ %msg[$MsgCat::commerce @ "E-ABORTED"] @ "\n\n";
        }
        if (!(%msg $= "")) {
            MessageBoxOK("Notice", %msg, "");
        }
        if (!(%skusSoldOut $= "")) {
            MessageBoxYesNo("Sold Out", $MsgCat::commerce["E-SOLDOUT"], "StoreShoppingList.removeSkus(\"" @ %skusSoldOut @ "\");", "");
        }
        if (!(%skusPurchased $= "")) {
            MessageBoxOK("Purchase Complete", $MsgCat::commerce["S-PURCHASE"], "StoreShoppingList.removeSkus(\"" @ %skusPurchased @ "\");");
        }
        if (!(%skusToFlatten $= "")) {
            %skusToFlatten = trim(%skusToFlatten);
            %newStoreSkus = "";
            %i = 0;
            while ((%i < getWordCount($StoreSkusLayer))) {
                %sku = getWord($StoreSkusLayer, %i);
                if ((findWord(%skusToFlatten, %sku) == -(1.0))) {
                    %newStoreSkus = %newStoreSkus @ " " @ %sku;
                }
                %i = (%i + 1.0);
            }
            $StoreSkusLayer = trim(%newStoreSkus);
            (%i < getWordCount($StoreSkusLayer));
            %skusToFlattenClothing = %skusToFlatten.filterSkusForClothing(SkuManager);
            %skusToFlattenBody = %skusToFlatten.filterSkusForBody(SkuManager);
            $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = %skusToFlattenClothing.overlaySkus(SkuManager, $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName]);
            $ClosetSkusBody = %skusToFlattenBody.overlaySkus(SkuManager, $ClosetSkusBody);
        }
        StoreItemsFrame.update();
    }
    %this.onClosed();
};
function ClosetGui::selectGenre(%this, %val) {
    $UserPref::Player::Genre = %val;
    %anim = $gClosetStanceEmotes[getRandom(0, ($gClosetStanceEmotesNum - 1.0))];
    %triesLeft = 10;
    if ((%triesLeft > 0.0)) {
    }
    while ((%anim $= $gClosetStanceEmotesLast)) {
        %anim = $gClosetStanceEmotes[getRandom(0, ($gClosetStanceEmotesNum - 1.0))];
        %triesLeft = (%triesLeft - 1.0);
        if ((%triesLeft > 0.0)) {
        }
    }
    $gClosetStanceEmotesLast = %anim;
    (%anim $= $gClosetStanceEmotesLast);
    $player.getGender() @ %val @ %anim.playAnim($player);
};
function ClosetGui::updateVisibleAvatar(%this) {
    %merged = $ClosetSkusBody @ " " @ $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName];
    if ((ClosetTabs.getCurrentTab().name $= "SHOPS")) {
        %merged = $StoreSkusLayer.overlaySkus(SkuManager, $ClosetSkusBody @ " " @ $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName]);
    }
    if ((ClosetTabs.getCurrentTab().name $= "CLOSET")) {
        $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].refresh(ClosetWhatYoureWearingList);
    }
    if ((ClosetTabs.getCurrentTab().name $= "MY DESIGNS")) {
        %merged = $gSkusMyShopLayer.overlaySkus(SkuManager, $ClosetSkusBody @ " " @ $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName]);
        $gSkusMyShopLayer.refresh(ClosetWhatYoureWearingList);
    }
    %merged.setSkus(ClosetMainObjectView);
    %snapTab = "SNAPSHOT".getTabWithName(ClosetTabs);
    if (%snapTab) {
    }
    if (isObject(%snapTab.objView)) {
        %merged.setSkus(%snapTab.objView);
    }
    %badge = "badges".filterSkusDrwr(SkuManager, %merged);
    %si = %badge.findBySku(SkuManager);
    %bitmap = "";
    if (isObject(%si)) {
        %bitmap = %si.getBitmapPath();
    }
    %bitmap.setBitmap(ClosetMainBadgeView);
    if (isObject(ClosetStaffPanel)) {
        ClosetStaffPanel.updateSkus();
    }
};
function ClosetGui::toggleSku(%this, %sku) {
    if ((ClosetTabs.getCurrentTab().name $= "SHOPS")) {
        ClosetGUI_ToggleSku_Shops(%sku);
    }
    if ((ClosetTabs.getCurrentTab().name $= "CLOSET")) {
        ClosetGUI_ToggleSku_Closet(%sku);
    }
    if ((ClosetTabs.getCurrentTab().name $= "BODY")) {
        ClosetGUI_ToggleSku_Body(%sku);
    }
    if ((ClosetTabs.getCurrentTab().name $= "SNAPSHOT")) {
        ClosetGUI_ToggleSku_Snapshot(%sku);
    }
    if ((ClosetTabs.getCurrentTab().name $= "MY DESIGNS")) {
        ClosetGUI_ToggleSku_MyShop(%sku);
    }
    error(getScopeName() @ " " @ "- unknown tab:" @ " " @ ClosetTabs.getCurrentTab().name @ " " @ getTrace());
    return;
    ClosetGui.updateVisibleAvatar();
    %sku.zoomToSKU(ClosetMainObjectView);
    %thumbnails = ClosetTabs.getCurrentTab().thumbnails;
    if (isObject(%thumbnails)) {
        %thumbnails.setSelectedThumbs();
        %count = %thumbnails.getCount();
        %i = 0;
        while ((%i < %count)) {
            %cell = %i.getObject(%thumbnails);
            %cell.sku.setCellSkus(%thumbnails, %cell);
            %i = (%i + 1.0);
        }
    }
};
function ClosetGui::doArrow(%this, %dx, %dy) {
    if ((ClosetTabs.getCurrentTab().name $= "SNAPSHOT")) {
        -(%dy).moveBy(ProfileObjectView, %dx);
    }
};
function ClosetLink::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    if ((getWord(%url, 0) $= "SAVE_OUTFIT")) {
        ClosetMyOutfitsFrame.saveOrCancel();
    }
    if ((getWord(%url, 0) $= "DONE")) {
        0.close(ClosetGui);
    }
    if ((getWord(%url, 0) $= "CANCEL")) {
        1.close(ClosetGui);
    }
    if ((getWord(%url, 0) $= "TOGGLE_SKU")) {
        getWord(%url, 1).toggleSku(ClosetGui);
    }
};
function ClosetItemsScroll::getIndexForSku(%this, %sku) {
    %thumbnails = %this.thumbnails;
    %count = %thumbnails.getCount();
    %i = 0;
    while ((%i < %count)) {
        if ((%i.getObject(%thumbnails).sku == %sku)) {
            return %i;
        }
        %i = (%i + 1.0);
    }
    return -(1.0);
};
function ClosetItemsScroll::scrollToSku(%this, %sku) {
    %thumbnails = %this.thumbnails;
    %idx = %sku.getIndexForSku(%this);
    if ((%idx < 0.0)) {
        if ((ClosetTabs.getCurrentTab().name $= "CLOSET")) {
            ClosetItemsFrame.brand = 0.getTextById(ClosetBrandPopup);
            ClosetItemsFrame.category = 0.getTextById(ClosetItemPopup);
            ClosetItemsFrame.update();
            0.SetSelected(ClosetBrandPopup);
            0.SetSelected(ClosetItemPopup);
            %idx = %sku.getIndexForSku(%this);
        }
        if ((ClosetTabs.getCurrentTab().name $= "SHOPS")) {
            if ((StoreCategoryPopup.GetSelected() != 0.0)) {
                0.SetSelected(StoreCategoryPopup);
            }
            %idx = %sku.getIndexForSku(%this);
        }
    }
    if ((%idx < 0.0)) {
        return;
    }
    %row = mFloor((%idx / %thumbnails.numRowsOrCols));
    %col = (%idx % %thumbnails.numRowsOrCols);
    %row.hiliteCell(%thumbnails, %col);
    %idx.scrollToCellIndex(%this);
    %sku.zoomToSKU(ClosetMainObjectView);
};
function ClosetItemsScroll::scrollToCell(%this, %cell) {
    %thumbnails = %this.thumbnails;
    %cellIdx = %cell.getObjectIndex(%thumbnails);
    %cellIdx.scrollToCellIndex(%this);
};
function ClosetItemsScroll::scrollToCellIndex(%this, %cellIdx) {
    %thumbnails = %this.thumbnails;
    %cellHeight = (getWord(%thumbnails.childrenExtent, 1) + %thumbnails.spacing);
    %ypos = (1.0 - getWord(%thumbnails.getPosition(), 1));
    %closestRow = mFloor(((%ypos / %cellHeight) + 0.5));
    %targetRow = mFloor((%cellIdx / 4.0));
    if ((%cellIdx < 0.0)) {
        %targetRow = %closestRow;
    }
    if ((%targetRow >= (%closestRow + 1.0))) {
        (%cellHeight * (%targetRow - 1.0)).scrollTo(%thumbnails.getParent(), 0);
    }
    (%cellHeight * %targetRow).scrollTo(%thumbnails.getParent(), 0);
};
function ClosetItemsScroll::onMouseUp(%this) {
    -(1.0).scrollToCellIndex(%this);
};
function ClosetItemsScroll::onScroll(%this) {
    ClosetTabs.updateRangeText();
};
function checkOutfitCorruption(%checkClosetVariables) {
    if (isObject($player)) {
        %plyrGendr = $player.getGender();
    }
    %plyrGendr = $UserPref::Player::gender;
    %outfitNames = %plyrGendr[$Player::HangerNames @ %plyrGendr];
    %numOutfitNames = getWordCount(%outfitNames);
    %numOutfitNamesBroken = 0;
    %outfitsCorrupted = 0;
    %noCurrentOutfit = 0;
    %noClosetOutfitName = 0;
    %playerObjNullInCloset = 0;
    %errMsg = "";
    if (!(isObject($player))) {
    }
    if (%checkClosetVariables) {
        %playerObjNullInCloset = 1;
        error(getScopeName() @ "-> player object not available in a closet context - can cause errors ($player.getGender() will return \"\" and foul array indices.)");
        %errMsg = %errMsg @ " " @ "(player obj null in closet, fails $player.getGender)";
    }
    if ((%numOutfitNames != $gClosetNumOutfits)) {
        %numOutfitNamesBroken = 1;
        error(getScopeName() @ "->Number of outfits named in Player::HangerNames for player gender is not equal to $gClosetNumOutfits! Will cause errors!");
        %errMsg = %errMsg @ " " @ "(getWordCount($Player::HangerNames[gender]) != $gClosetNumOutfits)";
    }
    %currentOutfit = "currentOutfit".get($gOutfits);
    if ((%currentOutfit $= "")) {
    }
    if ((findWord(%outfitNames, %plyrGendr @ %currentOutfit) < 0.0)) {
        %noCurrentOutfit = 1;
        error(getScopeName() @ "-> gOutfits->currentOutfit is blank or invalid! should NEVER happen! currentOutfit = \"" @ %currentOutfit @ "\"");
        %errMsg = %errMsg @ " " @ "(gOutfits->currentOutfit = " @ %currentOutfit @ ")";
    }
    if (%numOutfitNamesBroken) {
        %max = %numOutfitNames;
    }
    %max = $gClosetNumOutfits;
    if (%checkClosetVariables) {
        if ((findWord($Player::HangerNames, [$player.getGender()], $ClosetOutfitName) < 0.0)) {
            error(getScopeName() @ "-> can't find $ClosetOutfitName in $Player::HangerNames for this gender! $ClosetOutfitName = \"" @ $ClosetOutfitName @ "\"");
            %errMsg = %errMsg @ " " @ "($ClosetOutfitName = \"" @ $ClosetOutfitName @ "\")";
        }
        %n = (%max - 1.0);
        while ((%n >= 0.0)) {
            %name = getWord(%outfitNames, %n);
            %curOutfit = outfits_filterSKUList(%name[$ClosetSkusOutfit @ %name]);
            if ((%curOutfit $= "")) {
                %outfitsCorrupted = (%outfitsCorrupted + 1.0);
            }
            %n = (%n - 1.0);
        }
        if ((%outfitsCorrupted > 0.0)) {
            error(getScopeName() @ "-> " @ %outfitsCorrupted @ " blank outfits detected!");
            %errMsg = %errMsg @ " " @ "(" @ %outfitsCorrupted @ " blank outfits)";
            (%n >= 0.0);
        }
    }
    if ((%outfitsCorrupted > 0.0)) {
    }
    if (%numOutfitNamesBroken) {
    }
    if (%noCurrentOutfit) {
    }
    if (%playerObjNullInCloset) {
        error(getScopeName() @ "->one or more outfits tests failed. posting trace and doing full debug print. trace=" @ getTrace());
        commandToServer('OutfitsCorruptedOnClient', %errMsg, getTrace());
        outfitsAndInventoryDebugLog();
        return 1;
    }
    return 0;
};
function outfitsCorruptedNotify() {
    error(getScopeName() @ "->outfit data is corrupted. aborting, notifying user");
    %msg = "Wow, sorry, it looks like your outfits have become corrupted, so we're not saving the changes, and we advise you to close and reopen vSide. You can help us fix this problem by posting your console.log on the vSide forums before restarting.\n(Press OK to QUIT). ";
    MessageBoxOkCancel("Outfit Error", %msg, "cleanUpAndQuit();", "");
};
function outfitsAndInventoryDebugLog() {
    warn(getScopeName() @ "->gOutfits:");
    $gOutfits.dumpValues();
    warn("->$Player::HangerNames[$player.getGender]:", $Player::HangerNames @ [$player.getGender()]);
    warn(getScopeName() @ "->player inventory: " @ $Player::inventory);
};
function filterOutSkusToHideInCloset(%skus) {
    if ((%skus $= "")) {
        return %skus;
    }
    if (($gSkusToHideInCloset $= "")) {
        return %skus;
    }
    %i = (getWordCount($gSkusToHideInCloset) - 1.0);
    while ((%i >= 0.0)) {
        %skuToHide = getWord($gSkusToHideInCloset, %i);
        %skus = findAndRemoveAllOccurrencesOfWord(%skus, %skuToHide);
        %i = (%i - 1.0);
    }
    return %skus;
};
function ClosetTabs::createFilterWidget(%this) {
    if (isObject(ClosetFilterContainer)) {
        1.setVisible(ClosetFilterContainer);
        1.makeFirstResponder(ClosetFilterField);
        return ClosetFilterContainer;
    }
    new GuiControl(ClosetFilterContainer) {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "335 64";
        extent = "148 40";
    };
    1.makeFirstResponder(ClosetFilterField);
    return ClosetFilterContainer;
};
$gClosetFilterFieldTimerID = "";
function ClosetFilterField::OnTextChanged(%this) {
    cancel($gClosetFilterFieldTimerID);
    $gClosetFilterFieldTimerID = "onTimer".schedule(%this, %this.timeoutMS);
};
function ClosetFilterField::OnEnterKey(%this) {
    %this.refilter();
};
function ClosetFilterField::onTimer(%this) {
    %this.refilter();
};
function ClosetFilterField::refilter(%this) {
    cancel($gClosetFilterFieldTimerID);
    $gClosetFilterFieldTimerID = "";
    %filterText = %this.getValue();
    if ((%filterText $= %this.prevFilterText)) {
        return;
    }
    %this.prevFilterText = %filterText;
    %tab = ClosetTabs.getCurrentTab();
    if ((%tab.name $= "BODY")) {
        BodyItemsFrame.update();
    }
    if ((%tab.name $= "CLOSET")) {
        ClosetItemsFrame.update();
    }
    if ((%tab.name $= "SHOPS")) {
        StoreItemsFrame.update();
    }
    if ((%tab.name $= "SNAPSHOT")) {
    }
    if ((%tab.name $= "MY DESIGNS")) {
        MyShopItemsFrame.update();
    }
};
function ClosetTabs::createAuthorWidget(%this) {
    if (isObject(ClosetAuthorContainer)) {
        1.setVisible(ClosetAuthorContainer);
        return ClosetAuthorContainer;
    }
    new GuiWindowCtrl(ClosetAuthorPictureOutline) {
        profile = "NonModalDottedWindowProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = "66 66";
        resizeWidth = 0;
        resizeHeight = 0;
        canMove = 0;
        canClose = 0;
        canMinimize = 0;
        canMaximize = 0;
        closeCommand = "";
        visible = 0;
    };
    new GuiControl(ClosetAuthorContainer) {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "689 84";
        extent = "245 110";
    };
    return ClosetAuthorContainer;
};
function ClosetTabs::updateAuthorWidget(%this, %sku) {
    if (!(isObject(ClosetAuthorContainer))) {
        return;
    }
    if (!(%sku $= "")) {
    }
    %si = "";
    %sku.findBySku(SkuManager);
    %filled = 0;
    if (isObject(%si)) {
        if (!(%si.author $= "")) {
            %filled = 1;
            if ((%si.author $= "?")) {
                "platform/client/ui/tgf/tgf_profile_default_" @ $player.getGender().setBitmap(ClosetAuthorPicture);
                ClosetAuthorPicture.modulationColor = "255 255 255 50";
                1.setVisible(ClosetAuthorPictureOutline);
                "<just:right><font:Arial:12><color:00000044><linkcolor:00000066>" @ "oh nos!<br>" @ "we've lost track of who made this!<br>".setText(ClosetAuthorText);
            }
            %playerEncoded = urlEncode(stripUnprintables(%si.author));
            %profileURL = $Net::ProfileURL @ %playerEncoded;
            %pictureURL_M = $Net::AvatarURL @ %playerEncoded @ "?size=M";
            %pictureURL_L = $Net::AvatarURL @ %playerEncoded @ "?size=L";
            "".setBitmap(ClosetAuthorPicture);
            %pictureURL_M.downloadAndApplyBitmap(ClosetAuthorPicture);
            %pictureURL_L.downloadAndApplyBitmap(ClosetAuthorPicture);
            ClosetAuthorPicture.modulationColor = "255 255 255 255";
            1.setVisible(ClosetAuthorPictureOutline);
            "<just:right><font:Arial:12><color:00000044><linkcolor:00000066>" @ "design by<br><a:" @ %profileURL @ ">" @ %si.author @ "</a>".setText(ClosetAuthorText);
        }
        if (!(%si.brand $= "")) {
        }
        if (!(%si.brand $= "new")) {
        }
        if (!(%si.brand $= "vhdtemplate")) {
            %fullBrand = %si[$gClosetBrandsExtrnl @ %si.brand];
            if ((%fullBrand $= "")) {
                error(getScopeName() @ " " @ "- unknown brand" @ " " @ %si.brand @ " " @ %sku @ " " @ getTrace());
            }
            %filled = 1;
            "platform/client/ui/vside_icon_38x38".setBitmap(ClosetAuthorPicture);
            ClosetAuthorPicture.modulationColor = "255 255 255 20";
            0.setVisible(ClosetAuthorPictureOutline);
            "<just:right><font:Arial:12><color:00000044><linkcolor:00000066>" @ "brand:<br>" @ %fullBrand.setText(ClosetAuthorText);
        }
    }
    if (!(%filled)) {
        "platform/client/ui/vside_icon_38x38".setBitmap(ClosetAuthorPicture);
        ClosetAuthorPicture.modulationColor = "255 255 255 20";
        0.setVisible(ClosetAuthorPictureOutline);
        "".setText(ClosetAuthorText);
    }
};
function ClosetTabs::createWhatYourWearingPanel(%this) {
    if (isObject(ClosetWhatYoureWearingPanel)) {
        return ClosetWhatYoureWearingPanel;
    }
    %whatYoureWearingPanel = new GuiWindowCtrl(ClosetWhatYoureWearingPanel) {
        profile = "DottedWindowProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = "245 281";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        resizeWidth = 0;
        resizeHeight = 0;
        canMove = 0;
        canClose = 0;
        canMinimize = 0;
        canMaximize = 0;
        closeCommand = "";
    };
    new GuiMLTextCtrl(ClosetWhatYoureWearingNone) {
        horizSizing = new GuiMLTextCtrl(ClosetWhatYoureWearingTitle) {
        profile = "ClosetTitleProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "4 1";
        extent = "240 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "You Are Wearing";
        style = "plainOnWhiteSmallBold";
        maxLength = 255;
    }; @ "right";
        vertSizing = "bottom";
        position = "10 23";
        extent = "230 20";
        text = "";
        style = "faintOnWhite";
        lineSpacing = -(1.0);
        stripGamelink = 1;
    };
    %whatYoureWearingScroll = new GuiScrollCtrl("") {
        profile = "ETSScrollProfile";
        position = "3 18";
        extent = "239 259";
        minExtent = "1 1";
        horizSizing = "width";
        vertSizing = "height";
        visible = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        scrollMultiplier = 2.5;
    };
    %whatYoureWearingList = new GuiArray2Ctrl(ClosetWhatYoureWearingList) {
        horizSizing = "width";
        vertSizing = "height";
        profile = "GuiDefaultProfile";
        childrenClassName = "GuiMouseEventCtrl";
        childrenExtent = "228 36";
        spacing = 2;
        numRowsOrCols = 1;
        inRows = 0;
        canHilite = 0;
        scroll = %whatYoureWearingScroll;
        lastPropSku = "";
    };
    %whatYoureWearingList.add(%whatYoureWearingScroll);
    %whatYoureWearingScroll.add(%whatYoureWearingPanel);
    return %whatYoureWearingPanel;
};
function ClosetMainObjectView::onSystemDragDroppedEvent(%this, %text, %pt) {
    if ((ClosetTabs.getCurrentTab().name $= "BODY")) {
        error(getScopeName() @ " " @ "- not implemented for" @ " " @ ClosetTabs.getCurrentTab().name @ " " @ getTrace());
    }
    if ((ClosetTabs.getCurrentTab().name $= "CLOSET")) {
        error(getScopeName() @ " " @ "- not implemented for" @ " " @ ClosetTabs.getCurrentTab().name @ " " @ getTrace());
    }
    if ((ClosetTabs.getCurrentTab().name $= "SHOPS")) {
        error(getScopeName() @ " " @ "- not implemented for" @ " " @ ClosetTabs.getCurrentTab().name @ " " @ getTrace());
    }
    if ((ClosetTabs.getCurrentTab().name $= "MY DESIGNS")) {
        %pt.onSystemDragDroppedEvent_MyShop(%this, %text);
    }
};
function ClosetGui_About(%section, %topic) {
    %title = %topic[$MsgCat::closetAbout TAB "TITLE" @ %section @ %topic];
    %body = %topic[$MsgCat::closetAbout TAB "BODY" @ %section @ %topic];
    if ((%title $= "")) {
        %title = "about..";
    }
    if ((%body $= "")) {
        error(getScopeName() @ " " @ "- no about body!" @ " " @ %section @ " " @ %topic @ " " @ getTrace());
        return;
    }
    MessageBoxOK(%title, %body, "");
};
