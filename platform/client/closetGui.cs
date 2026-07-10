$ClosetGuiOpenMessage = "Changing Clothes";
$gSkusToHideInCloset = getSpecialSKU(0, "helpmebadge");
$gClosetStanceEmotesNum = 0;
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "cool";
$gClosetStanceEmotesNum = (1.0 + $gClosetStanceEmotesNum);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "cool";
$gClosetStanceEmotesNum = (1.0 + $gClosetStanceEmotesNum);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "cool";
$gClosetStanceEmotesNum = (1.0 + $gClosetStanceEmotesNum);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "cool";
$gClosetStanceEmotesNum = (1.0 + $gClosetStanceEmotesNum);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "cool";
$gClosetStanceEmotesNum = (1.0 + $gClosetStanceEmotesNum);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "wve";
$gClosetStanceEmotesNum = (1.0 + $gClosetStanceEmotesNum);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "wve";
$gClosetStanceEmotesNum = (1.0 + $gClosetStanceEmotesNum);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "wve";
$gClosetStanceEmotesNum = (1.0 + $gClosetStanceEmotesNum);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "flr";
$gClosetStanceEmotesNum = (1.0 + $gClosetStanceEmotesNum);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "flr";
$gClosetStanceEmotesNum = (1.0 + $gClosetStanceEmotesNum);
$gClosetStanceEmotesNum[$gClosetStanceEmotes @ $gClosetStanceEmotesNum] = "ttth";
$gClosetStanceEmotesNum = (1.0 + $gClosetStanceEmotesNum);
$gClosetStanceEmotesLast = "";
$gClosetStanceEmotesLast[$gClosetNeutralHeightInches @ "f"] = (7.0 + (12.0 * 5.0));
$gClosetStanceEmotesLast[$gClosetNeutralHeightInches @ "f"][$gClosetNeutralHeightInches @ "m"] = (7.0 + (12.0 * 5.0));
new StringMap(ThumbCategories) {
    ignoreCase = 1;
};
if (isObject(MissionCleanup)) {
    MissionCleanup.add(ThumbCategories);
}
ThumbCategories.put("all items", "torso torsob legs legsb feet ear neck neckb neckc chest waist waistb wristleft wristleftb wristright wristrightb fingerleft fingerright toeleft toeright glasses back hat mask tail purse props badges tokens");
ThumbCategories.put("all garments", "torso torsob chest legs legsb feet");
ThumbCategories.put("all accessories", "ear neck neckb neckc waist waistb wristleft wristleftb wristright wristrightb fingerleft fingerright toeleft toeright chest back hat tail mask purse props badges");
ThumbCategories.put("all features", "face faceb eyes skin hair");
ThumbCategories.put("tops", "torso torsob chest");
ThumbCategories.put("bottoms", "legs legsb");
ThumbCategories.put("hair", "hair hat");
ThumbCategories.put("shoes", "feet toeleft toeright");
ThumbCategories.put("ear", "ear");
ThumbCategories.put("neck", "neck neckb neckc");
ThumbCategories.put("waist", "waist waistb");
ThumbCategories.put("hands", "wristleft wristleftb wristright wristrightb fingerleft fingerright");
ThumbCategories.put("bags", "purse");
ThumbCategories.put("misc", "chest back hat tail mask");
ThumbCategories.put("bodymod", "earl labret lftauricle lftconch lfteyebrow lftlobe lftorbital lftpinna lftrook lfttragus rghauricle rghconch rgheyebrow rghlobe rghorbital rghpinna rghrook rghtragus lowlip madonna medusa nostril septum");
ThumbCategories.put("glasses", "glasses");
ThumbCategories.put("face", "face faceb");
ThumbCategories.put("eyes", "eyes");
ThumbCategories.put("skin", "skin");
ThumbCategories.put("props", "props");
ThumbCategories.put("badges", "badges");
ThumbCategories.put("tokens", "tokens");
ThumbCategories.buildSkusSearchText(SkuManager);
new StringMap(ThumbCategoriesOrder);
if (isObject(MissionCleanup)) {
    MissionCleanup.add(ThumbCategoriesOrder);
}
%n = 0;
ThumbCategoriesOrder.put(%n, "tops");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "bottoms");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "hair");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "shoes");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "ear");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "neck");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "waist");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "hands");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "bags");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "props");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "misc");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "bodymod");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "glasses");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "face");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "eyes");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "skin");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "badges");
%n = (1.0 + %n);
ThumbCategoriesOrder.put(%n, "tokens");
%n = (1.0 + %n);
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
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"][$shopBannerCacheCleared @ "kitson"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"][$shopBannerCacheCleared @ "kitson"][$shopBannerCacheCleared @ "goth"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"][$shopBannerCacheCleared @ "kitson"][$shopBannerCacheCleared @ "goth"][$shopBannerCacheCleared @ "kawaii"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"][$shopBannerCacheCleared @ "kitson"][$shopBannerCacheCleared @ "goth"][$shopBannerCacheCleared @ "kawaii"][$shopBannerCacheCleared @ "kenna"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"][$shopBannerCacheCleared @ "kitson"][$shopBannerCacheCleared @ "goth"][$shopBannerCacheCleared @ "kawaii"][$shopBannerCacheCleared @ "kenna"][$shopBannerCacheCleared @ "kong"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"][$shopBannerCacheCleared @ "kitson"][$shopBannerCacheCleared @ "goth"][$shopBannerCacheCleared @ "kawaii"][$shopBannerCacheCleared @ "kenna"][$shopBannerCacheCleared @ "kong"][$shopBannerCacheCleared @ "leet"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"][$shopBannerCacheCleared @ "kitson"][$shopBannerCacheCleared @ "goth"][$shopBannerCacheCleared @ "kawaii"][$shopBannerCacheCleared @ "kenna"][$shopBannerCacheCleared @ "kong"][$shopBannerCacheCleared @ "leet"][$shopBannerCacheCleared @ "myet"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"][$shopBannerCacheCleared @ "kitson"][$shopBannerCacheCleared @ "goth"][$shopBannerCacheCleared @ "kawaii"][$shopBannerCacheCleared @ "kenna"][$shopBannerCacheCleared @ "kong"][$shopBannerCacheCleared @ "leet"][$shopBannerCacheCleared @ "myet"][$shopBannerCacheCleared @ "modpodz"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"][$shopBannerCacheCleared @ "kitson"][$shopBannerCacheCleared @ "goth"][$shopBannerCacheCleared @ "kawaii"][$shopBannerCacheCleared @ "kenna"][$shopBannerCacheCleared @ "kong"][$shopBannerCacheCleared @ "leet"][$shopBannerCacheCleared @ "myet"][$shopBannerCacheCleared @ "modpodz"][$shopBannerCacheCleared @ "pcd"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"][$shopBannerCacheCleared @ "kitson"][$shopBannerCacheCleared @ "goth"][$shopBannerCacheCleared @ "kawaii"][$shopBannerCacheCleared @ "kenna"][$shopBannerCacheCleared @ "kong"][$shopBannerCacheCleared @ "leet"][$shopBannerCacheCleared @ "myet"][$shopBannerCacheCleared @ "modpodz"][$shopBannerCacheCleared @ "pcd"][$shopBannerCacheCleared @ "roca"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"][$shopBannerCacheCleared @ "kitson"][$shopBannerCacheCleared @ "goth"][$shopBannerCacheCleared @ "kawaii"][$shopBannerCacheCleared @ "kenna"][$shopBannerCacheCleared @ "kong"][$shopBannerCacheCleared @ "leet"][$shopBannerCacheCleared @ "myet"][$shopBannerCacheCleared @ "modpodz"][$shopBannerCacheCleared @ "pcd"][$shopBannerCacheCleared @ "roca"][$shopBannerCacheCleared @ "salon"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"][$shopBannerCacheCleared @ "kitson"][$shopBannerCacheCleared @ "goth"][$shopBannerCacheCleared @ "kawaii"][$shopBannerCacheCleared @ "kenna"][$shopBannerCacheCleared @ "kong"][$shopBannerCacheCleared @ "leet"][$shopBannerCacheCleared @ "myet"][$shopBannerCacheCleared @ "modpodz"][$shopBannerCacheCleared @ "pcd"][$shopBannerCacheCleared @ "roca"][$shopBannerCacheCleared @ "salon"][$shopBannerCacheCleared @ "starstyle"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"][$shopBannerCacheCleared @ "kitson"][$shopBannerCacheCleared @ "goth"][$shopBannerCacheCleared @ "kawaii"][$shopBannerCacheCleared @ "kenna"][$shopBannerCacheCleared @ "kong"][$shopBannerCacheCleared @ "leet"][$shopBannerCacheCleared @ "myet"][$shopBannerCacheCleared @ "modpodz"][$shopBannerCacheCleared @ "pcd"][$shopBannerCacheCleared @ "roca"][$shopBannerCacheCleared @ "salon"][$shopBannerCacheCleared @ "starstyle"][$shopBannerCacheCleared @ "threezee"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"][$shopBannerCacheCleared @ "kitson"][$shopBannerCacheCleared @ "goth"][$shopBannerCacheCleared @ "kawaii"][$shopBannerCacheCleared @ "kenna"][$shopBannerCacheCleared @ "kong"][$shopBannerCacheCleared @ "leet"][$shopBannerCacheCleared @ "myet"][$shopBannerCacheCleared @ "modpodz"][$shopBannerCacheCleared @ "pcd"][$shopBannerCacheCleared @ "roca"][$shopBannerCacheCleared @ "salon"][$shopBannerCacheCleared @ "starstyle"][$shopBannerCacheCleared @ "threezee"][$shopBannerCacheCleared @ "yjl"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"][$shopBannerCacheCleared @ "kitson"][$shopBannerCacheCleared @ "goth"][$shopBannerCacheCleared @ "kawaii"][$shopBannerCacheCleared @ "kenna"][$shopBannerCacheCleared @ "kong"][$shopBannerCacheCleared @ "leet"][$shopBannerCacheCleared @ "myet"][$shopBannerCacheCleared @ "modpodz"][$shopBannerCacheCleared @ "pcd"][$shopBannerCacheCleared @ "roca"][$shopBannerCacheCleared @ "salon"][$shopBannerCacheCleared @ "starstyle"][$shopBannerCacheCleared @ "threezee"][$shopBannerCacheCleared @ "yjl"][$shopBannerCacheCleared @ "vhd"] = 0;
$tmpGender["" @ $ThumbCamParams TAB $tmpGender @ "tokens"][$shopBannerCacheCleared @ 121][$shopBannerCacheCleared @ "aar"][$shopBannerCacheCleared @ "amap"][$shopBannerCacheCleared @ "amidol"][$shopBannerCacheCleared @ "clover"][$shopBannerCacheCleared @ "cos"][$shopBannerCacheCleared @ "dega"][$shopBannerCacheCleared @ "downtown"][$shopBannerCacheCleared @ "drezz"][$shopBannerCacheCleared @ "kitson"][$shopBannerCacheCleared @ "goth"][$shopBannerCacheCleared @ "kawaii"][$shopBannerCacheCleared @ "kenna"][$shopBannerCacheCleared @ "kong"][$shopBannerCacheCleared @ "leet"][$shopBannerCacheCleared @ "myet"][$shopBannerCacheCleared @ "modpodz"][$shopBannerCacheCleared @ "pcd"][$shopBannerCacheCleared @ "roca"][$shopBannerCacheCleared @ "salon"][$shopBannerCacheCleared @ "starstyle"][$shopBannerCacheCleared @ "threezee"][$shopBannerCacheCleared @ "yjl"][$shopBannerCacheCleared @ "vhd"][$shopBannerCacheCleared @ "vbar"] = 0;
if (!(isObject(ClosetTabs))) {
    new ScriptObject(ClosetTabs) {
        class = "TabControl";
    };
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(ClosetTabs);
    }
}
function Closet::skuListHasCategory(%list, %category) {
    %drawers = ThumbCategories.get(%category);
    %n = (1.0 - getWordCount(%drawers));
    if ((0.0 >= %n)) {
        if (SkuManager.skuListHasDrawer(%list, getWord(%drawers, %n))) {
            return 1;
        }
        %n = (1.0 - %n);
    }
    return 0;
};
function ClosetTabs::setup(%this) {
    if (!(%this.initialized)) {
        %this.initializing = 1;
        %this.Initialize(ClosetTabContainer, "103 21", "", "", "horizontal");
        %this.newTab("Shops", "platform/client/buttons/closet_tab");
        %this.newTab("Closet", "platform/client/buttons/closet_tab");
        %this.newTab("Body", "platform/client/buttons/closet_tab");
        %this.newTab("Snapshot", "platform/client/buttons/closet_tab");
        %this.newTab("My Designs", "platform/client/buttons/closet_tab");
        %this.lastTabOpened = "" @ ClosetGui;
        %this.numberOfPurchasesAwaitingCompletion = 0 @ ClosetGui;
        %this.numberOfPurchasesPastTimeout = 0 @ ClosetGui;
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
        profile = 0 @ "ClosetTabButtonProfile";
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
    %tab.lastTabOpened = %tab.name @ ClosetGui;
    if ((%tab.name $= "CLOSET")) {
    }
    if ((%tab.name $= "BODY")) {
    }
    if ((%tab.name $= "SHOPS")) {
    }
    if ((%tab.name $= "MY DESIGNS")) {
        ClosetMainObjectView.setVisible(1);
        ClosetMainObjectView.setOrbitDist(3.2);
        ClosetMainObjectView.setLookAtNudge("0 0 0.1");
        ClosetMainObjectView.setLightDirection("0 3 -2");
        %tab.add();
        %tab.bringToFront();
        ClosetMainBadgeView.setVisible(1);
        %tab.add();
        %tab.bringToFront();
        %tab.add();
        %tab.bringToFront();
    }
    ClosetMainObjectView.setVisible(0);
    ClosetMainBadgeView.setVisible(1);
    if (isObject(%tab.hiliteStrip)) {
        %offset = %this.getInitialButtonOffset();
        ClosetMainObjectZoomOutButton;
        %xoffset = getWord(%offset, 0);
        ClosetMainObjectZoomOutButton;
        %yoffset = (2.0 - getWord(%offset, 1));
        ClosetMainObjectViewContainer;
        %tab.hiliteStrip.resize(%xoffset, %yoffset, %this.visibleTabsWidth, 2);
    }
    if ((ClosetMainObjectViewContainer @ " " @ %tab.name $= "BODY")) {
        $player.setGenre("p");
        if (!(%this.tabBodyInitialized)) {
            %this.fillBodyTab();
        }
        BodyFeaturesPopup.rebuildPopupList();
        BodyItemsFrame.update();
        %tab.add(%this.createFilterWidget());
        %this.systemDragDrop = 0 @ ClosetMainObjectView;
        ClosetMainBadgeView;
        ClosetTabs.updateBodyTabDisplay();
    }
    if ((ClosetMainBadgeView @ " " @ %tab.name $= "CLOSET")) {
        $player.setGenre("p");
        if (!(%this.tabClosetInitialized)) {
            %this.fillClosetTab();
        }
        ClosetBrandPopup.update(getFilteredInventoryForSetDrawers());
        if ((0.0 > ClosetBrandPopup.size())) {
            ClosetBrandPopup.SetSelected(0);
        }
        ClosetItemsFrame.update();
        %tab.add(%this.createFilterWidget());
        %tab.add(%this.createAuthorWidget());
        %this.createWhatYourWearingPanel().reparentSameSize(ClosetWhatYourWearingContainer, "");
        ClosetWhatYoureWearingTitle.setTextWithStyle("You Are Wearing");
        %this.filterByRemovable = 1 @ ClosetWhatYoureWearingList;
        %this.systemDragDrop = 0 @ ClosetMainObjectView;
        %outfitNames = ;
        %i = 0;
        if (($gClosetNumOutfits < %i)) {
            %name = getWord(%outfitNames, %i);
            %objectView = ClosetTabs.getOutfitObjectView(%i);
            %objectView.setSimObject($player);
            %objectView.setSkus($ClosetSkusBody @ " " @ %name[$ClosetSkusOutfit @ %name]);
            %i = (1.0 + %i);
        }
        %outfitNum = findWord(($gClosetNumOutfits < %i), $ClosetOutfitName);
        ClosetTabs.getOutfitButton(%outfitNum).performClick();
    }
    if ((%tab.name $= "SHOPS")) {
        $player.setGenre("p");
        if (!(%this.tabShopsInitialized)) {
            %this.fillStoreTab();
        }
        if (!(StoreExpirationLegend @ " " @ %this.lastStore $= $gCurrentStoreName)) {
            %this.lastStore = $gCurrentStoreName @ StoreExpirationLegend;
            StoreExpirationLegend.setVisible(0);
        }
        %this.showTabWithName("Shops");
        StoreBalanceText.update();
        StoreShortDescText.setBaseDesc("");
        StoreLongDescText.setBaseDesc("");
        if (!($gCurrentStoreName $= "")) {
            ClosetTabs.setLeaveStoreControlsVisible(0);
            ClosetTabs.setStoreControlsVisible(1);
            StoreNameDescFrame.setText(%this.nameCtrl, Inventory::getCurrentStoreName());
            StoreNameDescFrame.setText(%this.descCtrl, Inventory::getCurrentStoreDescInCloset());
            %storename = getCurrentStoreID();
            %bannerRsrc = "";
            if (!(%storename $= "")) {
                removeShopBannerCache(%storename);
                %bannerRsrc = "platform/client/buttons/banners/store_" @ %storename;
                dlMgr.applyUrl(%bannerRsrc, "dlMgrCallback_ShopTexture", "dlMgrCallback_ShopError", %this, "storeads");
            }
            if (!(%bannerRsrc $= "")) {
                StoreBannerBrackets.setVisible(1);
                StoreBanner.setBitmap(%bannerRsrc);
            }
            StoreBannerBrackets.setVisible(0);
            %bgResource = "";
            if (!(%storename $= "")) {
                %bgResource = "platform/client/ui/store_backgrounds/store_bg_" @ %storename;
            }
            if (!(%bgResource $= "")) {
                StoreSpecificBackground.setBitmap(%bgResource);
                StoreSpecificBackground.setVisible(1);
                %tab.bringToFront();
            }
            StoreSpecificBackground.setVisible(0);
        }
        ClosetTabs.setStoreControlsVisible(0);
        ClosetTabs.setLeaveStoreControlsVisible(!(isInFUE()));
        StoreSpecificBackground.setVisible(0);
        %tab.add(%this.createFilterWidget());
        if ((StoreSpecificBackground @ " " @ $gCurrentStoreName $= "")) {
            %this.createFilterWidget().setVisible(0);
        }
        %tab.add(%this.createAuthorWidget());
        %this.systemDragDrop = 0 @ ClosetMainObjectView;
        ClosetTabs.refreshStoreTab();
    }
    if ((%tab.name $= "SNAPSHOT")) {
        ClosetGui.doResetGenre();
        if (!(%this.tabSnapshotInitialized)) {
            %this.fillProfileTab();
        }
        %objView = ClosetTabs.getTabWithName("SNAPSHOT").objView;
        %objView.setSimObject($player);
        %objView.setSkus(ClosetMainObjectView.getSkus());
        if (isObject(ClosetGuiFUE)) {
        }
        ClosetTabs.getTabWithName("SNAPSHOT").returnClosetGuiFUE = ClosetTabs.getTabWithName("SNAPSHOT").visible @ ProfileSnapRegion;
        ClosetGuiFUE;
        ProfileBackgroundChooser.Initialize();
        ProfileObjectView.setLightDirection("0 3 -2");
        ProfileObjectView.setOrbitDist(2.4);
        ClosetTabs.getTabWithName("SNAPSHOT").systemDragDrop = 0 @ ClosetMainObjectView;
    }
    if ((%tab.name $= "MY DESIGNS")) {
        $player.setGenre("p");
        if (!(%this.tabMyShopInitialized)) {
            %this.fillMyShopTab();
        }
        %this.showTabWithName("MY DESIGNS");
        %tab.add(%this.createFilterWidget());
        MyShopTextureInspector.getGroup().pushToBack();
        %this.systemDragDrop = 1 @ ClosetMainObjectView;
        MyShopTextureInspector;
        %this.createWhatYourWearingPanel().reparentSameSize(MyShopWhatYourWearingContainer, "");
        ClosetWhatYoureWearingTitle.setTextWithStyle("Custom Items");
        %this.filterByRemovable = 0 @ ClosetWhatYoureWearingList;
    }
    %tab.doneButton.setActive(!(ClosetGui.isWaitingForPurchaseCompletion()));
    %tab.cancelButton.setActive(!(ClosetGui.isWaitingForPurchaseCompletion()));
    if (0) {
        if (isObject(%tab.thumbnails)) {
            %tab.thumbnails.makeFirstResponder(1);
        }
        %fr = Canvas.getFirstResponder();
        if (isObject(%fr)) {
            %fr.makeFirstResponder(0);
        }
    }
    if (isObject(ClosetFilterContainer)) {
        ClosetFilterField.makeFirstResponder(1);
    }
    if (!(%tab.name $= "CLOSET")) {
        ClosetGui.updateVisibleAvatar();
    }
    ClosetMainObjectView.zoomToSKU("");
    if (isInFUE()) {
    }
    if (!(%this.initializing)) {
        ClosetGuiFUE.goToStepByName(%tab.name);
    }
};
function dlMgrCallback_ShopTexture(%dlItem, %unused) {
    StoreBanner.setBitmap(%dlItem.localFilename);
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
    %cellHeight = (%thumbnails.spacing + getWord(%thumbnails.childrenExtent, 1));
    %ypos = (getWord(%thumbnails.getPosition(), 1) - 1.0);
    %closestRow = mFloor((0.5 + (%cellHeight / %ypos)));
    %count = %thumbnails.getCount();
    %min = mMin(((%thumbnails.numRowsOrCols * %closestRow) + 1.0), %count);
    %max = mMin((7.0 + %min), %count);
    if ((0.0 > %count)) {
    }
    %rangeText.setText(%min @ " - " @ %max @ " of " @ %count, "");
    return %closestRow;
};
function ClosetTabs::getShortSkuDesc(%this, %sku) {
    if ((0.0 <= %sku)) {
        return "";
    }
    %skuInfo = SkuManager.findBySku(%sku);
    %ret = "";
    %ret = %ret @ "<spush><b>" @ %skuInfo.descShrt @ "<spop>";
    return %ret;
};
function ClosetTabs::getLongSkuDesc(%this, %sku) {
    if ((0.0 <= %sku)) {
        return "";
    }
    %skuInfo = SkuManager.findBySku(%sku);
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
        profile = 0 @ "ClosetLtBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "4 3";
        extent = "95 83";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    };
    %objectView = new GuiObjectView("") {
        profile = 0 @ "GuiDefaultProfile";
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
        %objectView.setSimObject($player);
    }
    %badgeView = new GuiBitmapCtrl("") {
        profile = 0 @ "GuiDefaultProfile";
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
        profile = 0 @ "GuiModelessDialogProfile";
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
        profile = 0 @ "GuiDefaultProfile";
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
        profile = 0 @ "ClosetFrameButtonProfile";
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
    %frameButton.bindClassName("ClosetFrameButton");
    %buttonBacking = new GuiControl("") {
        profile = 0 @ "ETSWhiteProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "2 86";
        extent = "99 42";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    };
    %toggleCartButton = new GuiBitmapButtonCtrl("") {
        profile = 0 @ "GuiButtonProfile";
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
        profile = 0 @ "GuiButtonProfile";
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
        profile = 0 @ "GuiDefaultProfile";
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
        profile = 0 @ "ETSNonModalProfile";
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
        profile = 0 @ "ETSNonModalProfile";
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
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "69 4";
        extent = "32 32";
        bitmap = "";
        modulationColor = "255 255 255 80";
    };
    %desc = new GuiMLTextCtrl("") {
        profile = 0 @ "ClosetSmallInfoProfile";
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
        profile = 0 @ "ClosetPointsProfile";
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
        profile = 0 @ "ClosetBuxProfile";
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
        profile = 0 @ "HiddenBracketButton15Profile";
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
        profile = 0 @ "ClosetInStockProfile";
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
        profile = 0 @ "GuiDefaultProfile";
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
        profile = 0 @ "ClosetAvailabilityProfile";
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
    %child.add(%background);
    %child.add(%ugcStatusIcon);
    %child.add(%objectView);
    %child.add(%expiringIcon);
    %child.add(%badgeView);
    %child.add(%rarityBitmap);
    %child.add(%frameButton);
    %child.add(%logo);
    %child.add(%frameFader);
    %child.add(%buyStatus);
    %child.add(%desc);
    %child.add(%vpointsPrice);
    %child.add(%vbuxPrice);
    %child.add(%totalButton);
    %child.add(%inStockText);
    %child.add(%priceFader);
    %child.add(%availabilityText);
    %child.add(%buttonBacking);
    %child.add(%toggleCartButton);
    %child.add(%buyNowButton);
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
        %child.bindClassName("ClosetThumbnailCtrl");
    }
};
function ClosetThumbnails::buttonClicked(%this, %button) {
    ClosetGui.toggleSku(%button.ctrl.sku);
    %this.makeFirstResponder(1);
};
function ClosetThumbnails::getAllSkusInDrawers(%this, %drwrNames) {
    %skus = "";
    %n = 0;
    if ((getWordCount(%drwrNames) < %n)) {
        %s = SkuManager.filterSkusGender(SkuManager.getSkusDrwr(getWord(%drwrNames, %n)), $player.getGender());
        %skus = %skus @ " " @ %s;
        %n = (1.0 + %n);
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
    %skus = SkuManager.filterSkusGender(%inventory, $player.getGender());
    %skus = SkuManager.filterSkusRoles(%skus, $player.getRolesMask());
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
        if (!(ClosetItemsFrame @ " " @ ClosetTabs.getCurrentTab().brand $= "")) {
            %skus = SkuManager.filterSkusBrand(%skus, $gClosetBrandsIntrnl);
        }
        $gUpdatingClosetItemPopupFromThumbnailsSetDrawers = 1;
        ClosetItemPopup.update(%skus);
        $gUpdatingClosetItemPopupFromThumbnailsSetDrawers = 0;
        if ((%drwrNames $= "")) {
            if ((ClosetItemsFrame @ " " @ ClosetTabs.getCurrentTab().category $= "")) {
                ClosetTabs.getCurrentTab().category = "All Items" @ ClosetItemsFrame;
            }
            %category = strlwr(ClosetItemsFrame, ClosetTabs.getCurrentTab().category);
            %drwrNames = ThumbCategories.get(%category);
        }
    }
    %skusTmp = "";
    %n = (1.0 - getWordCount(%drwrNames));
    if ((0.0 >= %n)) {
        %s = SkuManager.filterSkusDrwr(%skus, getWord(%drwrNames, %n));
        if (!(%s $= "")) {
            %skusTmp = %s @ " " @ %skusTmp;
        }
        %n = (1.0 - %n);
    }
    %skus = trim(%skusTmp);
    (0.0 >= %n);
    %this.setSkus(%skus);
};
function ClosetThumbnails::setUnfilteredSkus(%this, %skus) {
    %this.unfilteredSkus = %skus;
    %this.refilter();
};
function ClosetThumbnails::refilter(%this) {
    %this.setSkus(%this.unfilteredSkus);
};
function ClosetThumbnails::setSkus(%this, %skus) {
    %startingPos = %this.getPosition();
    %currentTabName = ClosetTabs.getCurrentTab().name;
    %skus = filterOutSkusToHideInCloset(%skus);
    if ((%currentTabName $= "SHOPS")) {
        %skus = SkuManager.filterSkusNonZeroManufactured(%skus);
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
        %skus = SkuManager.filterSkusDescription(%skus, %userFilterText);
    }
    if (isObject(%this.otherGenderText)) {
        %numSkusOtherGender = getWordCount(%skus);
        %skus = SkuManager.filterSkusGender(%skus, $player.getGender());
        %numSkus = getWordCount(%skus);
        %numSkusOtherGender = (%numSkus - %numSkusOtherGender);
        if ((0.0 == %numSkusOtherGender)) {
        }
        %text = "<just:right>(" @ %numSkusOtherGender @ " in other gender)";
        "";
        %this.otherGenderText.setText(%text);
    }
    %numSkus = getWordCount(%skus);
    %this.setNumChildren(%numSkus);
    if (isObject(%this.infoText)) {
        if ((0.0 != %numSkus)) {
            if ((ClosetTabs.getCurrentTab().name $= "BODY")) {
                if (!(BodyItemsFrame @ " " @ ClosetTabs.getCurrentTab().features $= "Height")) {
                }
            }
        }
        if (!(BodyItemsFrame @ " " @ ClosetTabs.getCurrentTab().features $= "Stance")) {
            %this.infoText.setVisible(0);
            %this.scroll.setVisible(1);
        }
        %this.scroll.setVisible(0);
        %this.infoText.setVisible(1);
        %this.infoText.setText("no matching items");
    }
    if (!(isObject(ClosetCurrentCamParams))) {
        new StringMap(ClosetCurrentCamParams);
        if (isObject(MissionCleanup)) {
            MissionCleanup.add(ClosetCurrentCamParams);
        }
    }
    ClosetCurrentCamParams.adjustForHeight(ClosetCurrentCamParams);
    if (($ClosetOutfitName $= "")) {
        warn("wardrobe", getScopeName() @ ": $ClosetOutfitName is empty");
        return;
    }
    %n = 0;
    if ((%numSkus < %n)) {
        %cell = %this.getObject(%n);
        %skunum = getWord(%skus, %n);
        if ((1.0 < getWordCount(%skunum))) {
        }
        if ((1.0 > getWordCount(%skunum))) {
        }
        if ((0.0 == %skunum)) {
        }
        if ((%skunum $= 0)) {
        }
        if ((getWordCount(%skus) != %numSkus)) {
            error(getScopeName() @ " " @ "- cell#" @ %cell.getId() @ " " @ "- sku #" @ %n @ " " @ "of" @ " " @ %numSkus @ "/" @ getWordCount(%skus) @ " " @ "- skus:" @ " " @ %skunum @ " " @ "- end.");
            error(getScopeName() @ " " @ "- skus:" @ " " @ %skus @ " " @ "- end.");
        }
        %this.setCellSkus(%cell, %skunum);
        %n = (1.0 + %n);
    }
    %dkBackground = 0;
    (%numSkus < %n);
    %currentDrwrName = "";
    %expiringItemsCount = 0;
    %n = 0;
    if ((%numSkus < %n)) {
        %cell = %this.getObject(%n);
        %skunum = getWord(%skus, %n);
        %skuItem = SkuManager.findBySku(%skunum);
        %cell.descCtrl.setText(%skuItem.descShrt);
        if ((%skuItem.brand $= "roca")) {
            %cell.logo.setBitmap("platform/client/ui/roca_logo_small");
        }
        if ((%skuItem.brand $= "myet")) {
            %cell.logo.setBitmap("platform/client/ui/myet_logo_small");
        }
        if ((%skuItem.brand $= "pcd")) {
            %cell.logo.setBitmap("platform/client/ui/pcd_logo_small");
        }
        if ((%skuItem.brand $= "staff")) {
            %cell.logo.setBitmap("platform/client/ui/staff_logo_small");
        }
        if ((%skuItem.brand $= "new")) {
            %cell.logo.setBitmap("platform/client/ui/new_logo_small");
        }
        %cell.logo.setBitmap("");
        if (%skuItem.hasTag("new")) {
            %cell.logo.setBitmap("platform/client/ui/new_logo_small");
        }
        if (!(%skuItem.expireTime $= "")) {
            %cell.expiringIcon.setBitmap("platform/client/ui/expiring_icon");
            %cell.expiringIcon.setVisible(1);
            %expiringItemsCount = (1.0 + %expiringItemsCount);
        }
        %cell.expiringIcon.setVisible(0);
        if ((%currentTabName $= "MY DESIGNS")) {
            %cell.ugcStatusIcon.setBitmap(ClosetGui_MyShop_GetSkuUGCStatusIcon(%skunum));
            %cell.ugcStatusIcon.setVisible(1);
        }
        %cell.ugcStatusIcon.setVisible(0);
        %cell.rarityBitmap.setBitmap(%this.getRarityBitmap(%skuItem.qty));
        %cell.frameButton.setActive(SkuManager.isWearableSkuType(%skuItem.skuType));
        if ((%this.tab.name $= "SHOPS")) {
            %vpointsSym = "platform/client/ui/vpoints_9";
            %vbuxSym = "platform/client/ui/vbux_9";
            %vpointsPrice = Inventory::getVPointsPriceForSku(%cell.sku);
            %vbuxPrice = Inventory::getVBuxPriceForSku(%cell.sku);
            if ((0.0 == %vpointsPrice)) {
            }
            if ((0.0 == %vbuxPrice)) {
                %cell.vpointsCtrl.setText("<just:right>free!");
                %cell.vbuxCtrl.setText("");
            }
            %cell.vpointsCtrl.setText("");
            %cell.vbuxCtrl.setText("");
            if ((0.0 > %vpointsPrice)) {
                %cell.vpointsCtrl.setText("<bitmap:" @ %vpointsSym @ "> " @ %vpointsPrice);
            }
            if ((0.0 > %vbuxPrice)) {
                %cell.vbuxCtrl.setText("<bitmap:" @ %vbuxSym @ "> " @ %vbuxPrice);
            }
            %cell.totalButton.setVisible(1);
            %cell.inStockText.setText(%this.GetInStockText(%cell[$gStoreItemsQty @ %cell.sku]));
            if ((0.0 >= findWord($Player::inventory, %cell.sku))) {
                %this.SetCellAvailability(%cell, 0, 1, "<just:right><color:00bb00>0wn3d!", "platform/client/ui/owned");
            }
            if ((0.0 == %cell[$gStoreItemsQty @ %cell.sku])) {
                %this.SetCellAvailability(%cell, 0, 0, "<just:right><color:dd0000>Sold Out!", "");
            }
            if (((1.0 + respektScoreToLevel($gMyRespektPoints)) > %skuItem.rspk)) {
                %this.SetCellAvailability(%cell, 0, 0, "<just:right><color:bb0000>More Levels!", "platform/client/ui/cantbuy2");
            }
            if ((respektScoreToLevel($gMyRespektPoints) > %skuItem.rspk)) {
                %this.SetCellAvailability(%cell, 0, 0, "<just:right><color:dd0000>Next Level!", "platform/client/ui/cantbuy");
            }
            %this.SetCellAvailability(%cell, 1, 1, "", "");
        }
        %cell.vpointsCtrl.setText("");
        %cell.vbuxCtrl.setText("");
        %cell.available = 0;
        if (!(%currentDrwrName $= %skuItem.drwrName)) {
            %currentDrwrName = %skuItem.drwrName;
            %dkBackground = !(%dkBackground);
        }
        if (%dkBackground) {
            // unhandled opcode 13445 at 0x00003482
        }
        %cell.background.setProfile();
        %n = (1.0 + %n);
        ClosetLtBackgroundProfile;
    }
    if (((%numSkus < %n) @ " " @ %currentTabName $= "SHOPS")) {
    }
    if ((0.0 > %expiringItemsCount)) {
        StoreExpirationLegend.setVisible(1);
    }
    %this.setSelectedThumbs();
    %this.getParent().scrollTo(0, (getWord(%startingPos, 1) - 1.0));
};
function ClosetThumbnails::SetCellAvailability(%this, %cell, %showPrice, %canTryOn, %subText, %overlayBitmapName) {
    if (!(%overlayBitmapName $= "")) {
        %cell.buyStatus.setBitmap(%overlayBitmapName);
        %cell.buyStatus.setVisible(1);
    }
    %cell.buyStatus.setVisible(0);
    if (!(%subText $= "")) {
        %cell.availabilityText.setVisible(1);
        %cell.availabilityText.setText(%subText);
        %cell.available = 0;
    }
    %cell.availabilityText.setVisible(0);
    %cell.available = 1;
    %cell.priceFader.setVisible(!(%showPrice));
    if (%canTryOn) {
    }
    %cell.frameButton.setActive(SkuManager.isWearableSkuType(%cell.SkuItem.skuType));
    %cell.frameFader.setVisible(!(%canTryOn));
};
function ClosetThumbnails::getRarityBitmap(%this, %qty) {
    %base = "platform/client/ui/";
    if ((0.0 < %qty)) {
        return "";
    }
    if ((1000.0 < %qty)) {
        return %base @ "rarity_superrare";
    }
    if ((5000.0 < %qty)) {
        return %base @ "rarity_reallyrare";
    }
    if ((10000.0 < %qty)) {
        return %base @ "rarity_rare";
    }
    return "";
};
function ClosetThumbnails::GetInStockText(%this, %qty) {
    if ((0.0 <= %qty)) {
        return "";
    }
    if ((25.0 < %qty)) {
        return "in stock: <color:dd0000>almost gone!";
    }
    if ((50.0 < %qty)) {
        return "in stock: <color:ee8800>not many";
    }
    if ((100.0 < %qty)) {
        return "in stock: <color:ee8800>a few";
    }
    if ((500.0 < %qty)) {
        return "in stock: <color:00bb00>enough";
    }
    return "in stock: <color:00bb00>yes!";
};
function ClosetThumbnails::setCellSkus(%this, %cell, %skus) {
    if ((1.0 < getWordCount(%skus))) {
        error(getScopeName() @ " " @ "no skus passed in!" @ " " @ getTrace());
        return;
    }
    if ((1.0 != getWordCount(%skus))) {
        error(getScopeName() @ " " @ "sorry, only 1 sku is currently supported." @ " " @ %skus);
        %skus = getWord(%skus, 0);
    }
    %skunum = %skus;
    %bodyAndOutfitSkus = $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] @ " " @ $ClosetSkusBody;
    %skuItem = SkuManager.findBySku(%skunum);
    %thumb = %cell.objectView;
    %badge = %cell.badgeView;
    %cell.sku = %skunum;
    %cell.SkuItem = %skuItem;
    if ((%skuItem.skuType $= "mesh")) {
        %thumb.setVisible(1);
        %badge.setVisible(0);
        %params = ClosetCurrentCamParams.get(strlwr(%skuItem.drwrName));
        %dist = getWord(%params, 3);
        %fov = getWord(%params, 4);
        %lookAtNudge = getWords(%params, 0, 2);
        %pskus = SkuManager.overlaySkus(%bodyAndOutfitSkus, %skunum);
        %thumb.layerSku = %skunum;
        %thumb.consumeMouseWheel = 0;
        %thumb.setSkus(%pskus);
        %thumb.makeSlaveOf();
        %thumb.setSimObject($player);
        %thumb.setLightDirection("0 3 -2");
        %thumb.setLookAtNudge(%lookAtNudge);
        %thumb.setOrbitDist(%dist);
        %thumb.setFOV(%fov);
    }
    if ((ClosetMainObjectView @ " " @ %skuItem.skuType $= "badge")) {
        %thumb.setVisible(0);
        %badge.setVisible(1);
        %bitmapName = %skuItem.getBitmapPath();
        %badge.setBitmap(%bitmapName);
    }
    if ((%skuItem.skuType $= "token")) {
        %thumb.setVisible(0);
        %badge.setVisible(1);
        %bitmapName = %skuItem.getBitmapPath();
        %badge.setBitmap(%bitmapName);
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
    if ((%numDrawers < %i)) {
        %drawer = strlwr(getWord(%allDrawers, %i));
        %params = %drawer[$ThumbCamParams TAB $player.getGender() @ %drawer];
        if (!(%params $= "")) {
            %vNudge = getWord(%params, 2);
            %vNudge = (((1.0 - $UserPref::Player::height) * (1.0 + %vNudge)) + %vNudge);
            %params = setWord(%params, 2, %vNudge);
        }
        %this.put(%drawer, %params);
        %i = (1.0 + %i);
    }
};
function ClosetMainObjectView::zoomToSKU(%this, %sku) {
    if ((%sku $= "")) {
        %drawer = "fullbody";
        ClosetMainObjectZoomOutButton.setVisible(0);
    }
    %drawer = SkuManager.findBySku(%sku).drwrName;
    ClosetMainObjectZoomOutButton.setVisible(1);
    %params = ClosetCurrentCamParams.get(%drawer);
    %this.setCamParams(%params);
};
function GuiObjectView::setCamParams(%this, %params) {
    if ((%params $= "")) {
        return;
    }
    %dist = getWord(%params, 3);
    %fov = getWord(%params, 4);
    %lookAtNudge = getWords(%params, 0, 2);
    if (!(%this.fovFac $= "")) {
        %fov = (%this.fovFac * %fov);
    }
    %this.setLightDirection("0 3 -2");
    %this.setLookAtNudge(%lookAtNudge);
    %this.setOrbitDist(%dist);
    %this.setFOV(%fov);
};
function ClosetThumbnails::SetSelected(%this, %cell, %selected) {
    %cell.selected = %selected;
    %buttonsDir = "platform/client/buttons/";
    %selString = %cell.selected ? "_sel" : "";
    %hiString = %cell.hilited ? "_hi" : "";
    %cell.frameButton.setBitmap(%buttonsDir @ "frame" @ %selString @ %hiString);
};
function ClosetThumbnails::setSelectedThumbs(%this) {
    if ((ClosetTabs.getCurrentTab().name $= "SHOPS")) {
        %selectedSkus = $StoreSkusLayer;
    }
    %selectedSkus = $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] @ " " @ $ClosetSkusBody;
    %numThumbs = %this.getCount();
    %n = 0;
    if ((%numThumbs < %n)) {
        %cell = %this.getObject(%n);
        %selected = (0.0 >= findWord(%selectedSkus, %cell.sku));
        %this.SetSelected(%cell, %selected);
        %n = (1.0 + %n);
    }
};
$gClosetThumbnailStoreHighlightTimer = "";
$gClosetThumbnailStoreHighlightDelayMS = 0;
function ClosetThumbnailCtrl::onHilite(%this) {
    if ((getCurrentStoreID() $= "")) {
        return;
    }
    %this.frameButton.mouseOver = 1;
    %this.thumbnails.scroll.scrollToCell(%this);
    %si = SkuManager.findBySku(%this.sku);
    %descShort = ClosetTabs.getShortSkuDesc(%this.sku);
    %descLong = ClosetTabs.getLongSkuDesc(%this.sku);
    if ($DevPref::Closet::skuDeets) {
        %descLong = %descLong @ "<font:Courier New:14>";
        %descLong = %descLong @ "\n" @ "sku    = " @ " " @ %si.skuNumber;
        %descLong = %descLong @ "\n" @ "type   = " @ " " @ %si.skuType;
        %descLong = %descLong @ "\n" @ "mesh   = " @ " " @ %si.meshName;
        %descLong = %descLong @ "\n" @ "txtrs  = " @ " " @ %si.getTxtrNames();
        %descLong = %descLong @ "\n" @ "roles  = " @ " " @ roles::getRoleStrings(%si.rolesMask);
        %descLong = %descLong @ "\n" @ "bornW/ = " @ " " @ %si.born;
        %descLong = %descLong @ "\n" @ "rspkt  = " @ " " @ %si.rspk;
        %descLong = %descLong @ "\n" @ "tags   = " @ " " @ SkuManager.getSkuTags(%si.skuNumber);
    }
    if (isObject(BodyLongDescText)) {
        BodyShortDescText.setText(%descShort);
        BodyLongDescText.setText(%descLong);
    }
    if (isObject(ClosetLongDescText)) {
        ClosetShortDescText.setText(%descShort);
        ClosetLongDescText.setText(%descLong);
    }
    if (isObject(StoreLongDescText)) {
        StoreShortDescText.setText(%descShort);
        StoreLongDescText.setText(%descLong);
    }
    ClosetTabs.updateAuthorWidget(%this.sku);
    if (%this.available) {
    }
    if ((ClosetThumbnailsShop.getId() == %this.thumbnails)) {
        if (!($gClosetThumbnailStoreHighlightTimer $= "")) {
            cancel($gClosetThumbnailStoreHighlightTimer);
        }
        $gClosetThumbnailStoreHighlightTimer = %this.schedule($gClosetThumbnailStoreHighlightDelayMS, "onHiliteStore");
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
    %cartBitmap = StoreShoppingList.containsSku(%this.sku) ? "removeFromCart" : "add2cart";
    %this.frameButton.setBitmap(%buttonsDir @ %frameBitmap @ "_hi");
    %this.toggleCartButton.setVisible(1);
    %this.toggleCartButton.setBitmap(%buttonsDir @ %cartBitmap);
    %this.buyNowButton.setVisible(1);
    %this.buttonBacking.setVisible(1);
    if (isObject(StoreItemDescHiliteFrame)) {
        StoreItemDescHiliteFrame.setVisible(1);
    }
    if (isObject(StoreFloatingHiliteFrame)) {
        StoreFloatingHiliteFrame.setVisible(1);
        %screenPos = StoreFloatingHiliteFrame.getScreenPosition();
        %pos = StoreFloatingHiliteFrame.getPosition();
        %offsetX = (getWord(%pos, 0) - getWord(%screenPos, 0));
        %offsetY = (getWord(%pos, 1) - getWord(%screenPos, 1));
        %newPosX = (9.0 - (%offsetX - getWord(%this.getScreenPosition(), 0)));
        %newPosY = (4.0 - (%offsetY - getWord(%this.getScreenPosition(), 1)));
        StoreFloatingHiliteFrame.reposition(%newPosX, %newPosY);
        %scroll = %this.thumbnails.scroll;
        %padding = 10;
        %minx = (%padding - getWord(%scroll.getScreenPosition(), 0));
        %minY = (%padding - getWord(%scroll.getScreenPosition(), 1));
        %maxX = ((%padding * 2.0) + (getWord(%scroll.getExtent(), 0) + %minx));
        %maxy = ((%padding * 2.0) + (getWord(%scroll.getExtent(), 1) + %minY));
        %posX = getWord(%this.getScreenPosition(), 0);
        %posY = getWord(%this.getScreenPosition(), 1);
        %width = getWord(%this.getExtent(), 0);
        %height = getWord(%this.getExtent(), 1);
        if ((%minx >= %posX)) {
        }
        if ((%maxX <= (%width + %posX))) {
        }
        if ((%minY >= %posY)) {
        }
        StoreFloatingHiliteFrame.setVisible((%maxy <= (%height + %posY)));
    }
};
function ClosetThumbnailCtrl::onUnhilite(%this) {
    %this.frameButton.mouseOver = 0;
    if (0) {
        if (isObject(BodyLongDescText)) {
            BodyShortDescText.setText("");
            BodyLongDescText.setText("");
        }
        if (isObject(ClosetLongDescText)) {
            ClosetShortDescText.setText("");
            ClosetLongDescText.setText("");
        }
        if (isObject(StoreLongDescText)) {
            StoreShortDescText.showBaseDesc();
            StoreLongDescText.showBaseDesc();
        }
        ClosetTabs.updateAuthorWidget("");
    }
    if (%this.frameButton.tabShopsInitialized) {
    }
    if ((ClosetThumbnailsShop.getId() == %this.thumbnails)) {
        %this.hilited = ClosetTabs @ 0;
        %buttonsDir = "platform/client/buttons/";
        %frameBitmap = %this.selected ? "frame_sel" : "frame";
        %cartBitmap = StoreShoppingList.containsSku(%this.sku) ? "removeFromCart" : "add2cart";
        %this.frameButton.setBitmap(%buttonsDir @ %frameBitmap);
        %this.toggleCartButton.setVisible(0);
        %this.toggleCartButton.setBitmap(%buttonsDir @ %cartBitmap);
        %this.buyNowButton.setVisible(0);
        %this.buttonBacking.setVisible(0);
        if (isObject(StoreItemDescHiliteFrame)) {
            StoreItemDescHiliteFrame.setVisible(0);
        }
        if (isObject(StoreFloatingHiliteFrame)) {
            StoreFloatingHiliteFrame.setVisible(0);
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
    StoreShoppingList.addSku(%this.sku);
};
function ClosetThumbnailCtrl::toggleInCart(%this) {
    if (StoreShoppingList.containsSku(%this.sku)) {
        StoreShoppingList.removeSku(%this.sku);
    }
    StoreShoppingList.addSku(%this.sku);
};
function ClosetThumbnailCtrl::buyNow(%this) {
    if (%this.sku) {
        ClosetGui.purchaseSkus(%this.sku);
    }
};
function ClosetFrameButton::onMouseEnter(%this) {
    %thumbnails = %this.thumbnails;
    %i = %this.thumbnails.getObjectIndex(%this.ctrl);
    %row = mFloor((%thumbnails.numRowsOrCols / %i));
    %col = (%thumbnails.numRowsOrCols % %i);
    %thumbnails.hiliteCell(%col, %row);
};
$gAllOutfits = "A B C D E F G H I J K L";
$gAllOutfits[$Player::HangerNames @ "f"] = "fA fB fC fD fE fF fG fH fI fJ fK fL";
$gAllOutfits[$Player::HangerNames @ "f"][$Player::HangerNames @ "m"] = "mA mB mC mD mE mF mG mH mI mJ mK mL";
$gClosetNumOutfits = getWordCount($gAllOutfits[$Player::HangerNames @ "f"][$Player::HangerNames @ "m"][$Player::HangerNames @ "f"]);
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
    GuiTracker.updateLocation(%this);
    closetMap.push();
    DestroyMessageBoxes();
    $ClosetOutfitName = $player.getGender() @ $gOutfits.get("currentOutfit");
    $ClosetSkusBody = $gOutfits.get($player.getGender() @ "Body");
    %outfitNames = ;
    %i = 0;
    if (($gClosetNumOutfits < %i)) {
        %name = getWord(%outfitNames, %i);
        %name[$ClosetSkusOutfit @ %name] = $gOutfits.get(%name);
        %i = (1.0 + %i);
    }
    checkOutfitCorruption(1);
    ClosetTabs.setup();
    if ((1.0 == $player.isSitting())) {
        if ((0.0 == $IN_ORBIT_CAM)) {
            togglePlayerCamMode();
        }
        if ((1.0 == $player.isKissSeat)) {
            commandToServer('RequestToStand', 0, 0);
        }
    }
    if ((1.0 == $IN_ORBIT_CAM)) {
        togglePlayerCamMode();
    }
    ClosetMainObjectView.setSimObject($player);
    Canvas.setContent(%this);
    %this.setVisible(1);
    setIdle(1, $ClosetGuiOpenMessage);
    getUserActivityMgr().setActivityActive("dressing", 1);
    ClosetGui.updateVisibleAvatar();
    ClosetMainObjectView.zoomToSKU("");
    ClosetStaffPanelContainer.setVisible($player.rolesPermissionCheckNoWarn("debugPassive"));
    if ((($gClosetNumOutfits < %i) @ " " @ $UserPref::Player::Genre $= "h")) {
    }
    if (($UserPref::Player::Genre $= "i")) {
    }
    if (($UserPref::Player::Genre $= "p")) {
    }
    if (($UserPref::Player::Genre $= "t")) {
    }
    if (($UserPref::Player::Genre $= "s")) {
        ClosetGui.selectGenre($UserPref::Player::Genre);
    }
    pushScreenSize(960, 544, 0, 1, 1);
    %closetGuiFUEIsObject = isObject(ClosetGuiFUE);
    if (isInFUE()) {
        if (!(%closetGuiFUEIsObject)) {
            ClosetGuiPositioner.execHideAndAddChild("./closetGuiFUE.gui", "");
        }
        ClosetGuiFUE.open();
    }
    if (%closetGuiFUEIsObject) {
    }
    if (ClosetGuiFUE.isVisible()) {
        ClosetGuiFUE.close();
    }
    if (!($Player::hasSeenTakeAvatarPhotoDialog)) {
        if (!($player.tabSnapshotInitialized)) {
            ClosetTabs.fillProfileTab();
        }
        ProfileCurrentPicture.update("");
    }
    if (isObject(ProfileObjectView)) {
        ProfileObjectView.resetLight();
    }
};
function ClosetGui::close(%this, %cancel) {
    %this.doClose(%cancel, 1);
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
        %i = (1.0 - getWordCount($StoreSkusLayer));
        if ((0.0 >= %i)) {
            %aTriedOnSku = getWord($StoreSkusLayer, %i);
            %skuIndex = findWord($Player::inventory, %aTriedOnSku);
            if (( >= 0.0)) {
                if (SkuManager.isBodySku(%aTriedOnSku)) {
                }
                if ((0.0 < findWord($ClosetSkusBody, %aTriedOnSku))) {
                    $ClosetSkusBody = SkuManager.overlaySkus($ClosetSkusBody, %aTriedOnSku);
                }
                if (SkuManager.isOutfitSku(%aTriedOnSku)) {
                }
                if ((0.0 < findWord($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], %aTriedOnSku))) {
                    $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = SkuManager.overlaySkus($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], %aTriedOnSku);
                }
            }
            %i = (1.0 - %i);
        }
        saveStorePosition();
    }
    if (%allowMsgBoxOnExit) {
    }
    if (((0.0 >= %i) @ " " @ ClosetTabs.getCurrentTab().name $= "SHOPS")) {
    }
    if ((0.0 > StoreShoppingList.getCount())) {
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
    if (!(gUserPropMgrClient.getProperty($Player::Name, "hasTakenAvatarPhoto", 0))) {
    }
    if (!($Player::hasSeenTakeAvatarPhotoDialog)) {
    }
    if (!($StandAlone)) {
        $Player::hasSeenTakeAvatarPhotoDialog = 1;
        MessageBoxYesNo($Player::hasSeenTakeAvatarPhotoDialog[$MsgCat::closet @ "MSG-NO-AVATAR-PHOTO-TITLE"], , "ClosetTabs.selectTabWithName(\"SNAPSHOT\");", "ClosetGui.reallyClose(" @ %cancel @ ");");
        return 0;
    }
    %this.reallyClose(%cancel);
};
function ClosetGui::reallyClose(%this, %cancel) {
    stopPropAction();
    closetMap.pop();
    if (isObject(StoreSpecificBackground)) {
        StoreSpecificBackground.setVisible(0);
    }
    %this.doResetGenre();
    if ((%cancel $= "")) {
        %cancel = 0;
    }
    if (%cancel) {
        %this.doCancel();
    }
    %this.doOkay();
    if (ClosetTabs.getCurrentTab().inTransit) {
        Canvas.setContent(GuiTracker, ClosetTabs.getCurrentTab().previouslyOpened);
    }
    Canvas.setContent(PlayGui);
    %this.setVisible(0);
    nextPlayerCamMode();
    setIdle(0);
    if ((0.0 != $gSalonChairCurrent)) {
        if (!(GuiTracker @ " " @ %this.oldAnimation $= "")) {
            $player.playAnim(%this.oldAnimation);
        }
    }
    $player.playAnim($player.getGender() @ $player.getGenre() @ "idl1a");
    %this.oldAnimation = "";
    getUserActivityMgr().setActivityActive("dressing", 0);
    popScreenSize();
    if (!(gUserPropMgrClient.getProperty($Player::Name, "hasSeenPropUseAdvisory", 0))) {
    }
    if (SkuManager.hasPropSku($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName])) {
        gUserPropMgrClient.setProperty($Player::Name, "hasSeenPropUseAdvisory", 1);
        MessageBoxOK(, , "");
    }
    Inventory::fetchPlayerInventoryIfEmpty();
};
function ClosetGui::doResetCurrent(%this) {
    checkOutfitCorruption(1);
    $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = $gOutfits.get($ClosetOutfitName);
    %this.updateVisibleAvatar();
    ClosetMainObjectView.zoomToSKU("");
    ClosetItemsFrame.update();
};
function ClosetGui::doResetGenre(%this) {
    $UserPref::Player::Genre = %this.oldStance;
    if (!(%this.currentOverrideGenre $= $UserPref::Player::Genre)) {
        $player.setGenre(%this.currentOverrideGenre);
    }
    $player.setGenre($UserPref::Player::Genre);
};
function ClosetGui::doResetAll(%this) {
    $UserPref::Player::height = %this.oldHeight;
    $ClosetOutfitName = $player.getGender() @ $gOutfits.get("currentOutfit");
    %outfitNames = ;
    %i = 0;
    if (($gClosetNumOutfits < %i)) {
        %name = getWord(%outfitNames, %i);
        %name[$ClosetSkusOutfit @ %name] = $gOutfits.get(%name);
        %i = (1.0 + %i);
    }
    $ClosetSkusBody = $gOutfits.get($player.getGender() @ "Body");
    ($gClosetNumOutfits < %i);
    %this.updateVisibleAvatar();
    ClosetMainObjectView.zoomToSKU("");
    ClosetTabs.updateBodyTabDisplay();
};
function ClosetGui::doCancel(%this) {
    if (checkOutfitCorruption(1)) {
        outfitsCorruptedNotify();
    }
    %this.doResetAll();
    $player.setActiveSKUs($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] @ " " @ $ClosetSkusBody);
};
function ClosetGui::doOkay(%this) {
    $gOutfits.put("currentOutfit", strupr(getSubStr($ClosetOutfitName, 1, 1)));
    $gOutfits.put($player.getGender() @ "Body", $ClosetSkusBody);
    %outfitNames = ;
    %n = (1.0 - $gClosetNumOutfits);
    if ((0.0 >= %n)) {
        %name = getWord(%outfitNames, %n);
        %name[$ClosetSkusOutfit @ %name] = outfits_filterSKUList(%name[$ClosetSkusOutfit @ %name]);
        $gOutfits.put(%name, %name[$ClosetSkusOutfit @ %name]);
        %n = (1.0 - %n);
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
    gUserPropMgrClient.setProperty($Player::Name, "avatarHeight", $UserPref::Player::height);
    %playerActiveSKUs = $player.getActiveSKUs();
    if (%this.wasInHelpmode) {
        %playerActiveSKUs = %playerActiveSKUs @ " " @ getSpecialSKU(0, "helpmebadge");
    }
    $player.schedule(0, "setActiveSkus", %playerActiveSKUs);
    if (isObject(closetGuiFUEHideTipsCtrl)) {
        closetGuiFUEHideTipsCtrl.setValue(1);
        closetGuiFUEHideTipsCtrl.onAction();
    }
};
function ClosetGui::askUserToDropProp(%this) {
    %propSku = SkuManager.filterSkusDrwr($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], "props");
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
    %body = %activity[$MsgCat::closet @ "MSG-NO-PROP-IN-THIS-GENRE-BODY1"] @ " " @ %activity @ %activity[$MsgCat::closet @ "MSG-NO-PROP-IN-THIS-GENRE-BODY2"];
    MessageBoxYesNo(%body[$MsgCat::closet @ "MSG-NO-PROP-IN-THIS-GENRE-TITLE"], %body, "$ClosetSkusOutfit[$ClosetOutfitName] = \"" @ %outfitWithoutProp @ "\"; ClosetGui.reallyClose(false);", "");
    return 1;
};
function ClosetGui::userHasChangedBodyOrOutfit(%this) {
    if (!($ClosetSkusBody $= $gOutfits.get($player.getGender() @ "Body"))) {
        return 1;
    }
    if (!($gOutfits.get("currentOutfit") $= strupr(getSubStr($ClosetOutfitName, 1, 1)))) {
        return 1;
    }
    %outfitNames = ;
    %n = (1.0 - $gClosetNumOutfits);
    if ((0.0 >= %n)) {
        %name = getWord(%outfitNames, %n);
        %newOutfit = outfits_filterSKUList(%name[$ClosetSkusOutfit @ %name]);
        %oldOutfit = $gOutfits.get(%name);
        if (!(%newOutfit $= %oldOutfit)) {
            return 1;
        }
        %n = (1.0 - %n);
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
    if ((0.0 == %numSkus)) {
    }
    if ((%skus $= 0)) {
        error(getScopeName() @ " " @ "- no skus!" @ " " @ getTrace());
        return 0;
    }
    %skusValidPoints = Inventory::filterSkusByValidPrice("vPoints", %skus);
    %skusValidBux = Inventory::filterSkusByValidPrice("vBux", %skus);
    %numSkusValidPoints = getWordCount(%skusValidPoints);
    %numSkusValidBux = getWordCount(%skusValidBux);
    if ((1.0 == %numSkus)) {
    }
    %itemsStr = "these" @ " " @ %numSkus @ " " @ "items";
    "this item";
    %pointsTotal = Inventory::getTotalPrice("vPoints", %skus);
    %buxTotal = Inventory::getTotalPrice("vBux", %skus);
    if ((0.0 == %pointsTotal)) {
    }
    if ((0.0 > %numSkusValidPoints)) {
        eval(%cbBux);
        return;
    }
    if ((0.0 == %buxTotal)) {
    }
    if ((0.0 > %numSkusValidBux)) {
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
    if ((%numSkus < %numSkusValidPoints)) {
    }
    if ((%numSkus < %numSkusValidBux)) {
        if ((1.0 == %numSkus)) {
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
    if ((%numSkus < %numSkusValidPoints)) {
        %mbTitle = "Confirm Currency";
        %mbBody = "Do you want to buy " @ %itemsStr @ " with vBux?";
        if ((1.0 > %numSkus)) {
            %mbPointsNote = "<br><br>(Some or all are not available for vPoints.)";
        }
        %mbPointsNote = "<br><br>(This item is not available for vPoints.)";
        %mbButtons = %buxTotal @ "\t" @ "Cancel";
        %mbCBPoints = "";
    }
    if ((%numSkus < %numSkusValidBux)) {
        %mbTitle = "Confirm Currency";
        %mbBody = "Do you want to buy " @ %itemsStr @ " with vPoints?";
        if ((1.0 > %numSkus)) {
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
        %buttonIndex.add(%dialog.button, new GuiBitmapCtrl("") {
            profile = 0 @ "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "7 2";
            extent = "7 13";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = "platform/client/ui/vpoints_9";
        };);
        %buttonIndex = (1.0 + %buttonIndex);
    }
    if (!(%mbCBBux $= "")) {
        %dialog.callback = %mbCBBux @ %buttonIndex;
        %buttonIndex.add(%dialog.button, new GuiBitmapCtrl("") {
            profile = 0 @ "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "7 2";
            extent = "7 13";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = "platform/client/ui/vbux_9";
        };);
        %buttonIndex = (1.0 + %buttonIndex);
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
    if ((0.0 == %numValidSkus)) {
        if ((1.0 == %numSkus)) {
            MessageBoxOK("Not Available", "This item is not available for vPoints.", "");
        }
        MessageBoxOK("Not Available", "These items are not available for vPoints.", "");
        return;
    }
    %totalPrice = Inventory::getTotalPrice("vPoints", %skus);
    if (($Player::VPoints > %totalPrice)) {
        %this.doInsufficientVPoints();
        return;
    }
    %desc = "these " @ %numValidSkus @ " items";
    if ((1.0 == %numValidSkus)) {
        %si = SkuManager.findBySku(%skus);
        if (!(isObject(%si))) {
            return;
        }
        %desc = %si.descShrt;
    }
    %vpointsString = (1.0 == %totalPrice) ? "vPoint" : "vPoints";
    %note = "";
    if ((%numSkus > %numValidSkus)) {
        %diff = (%numValidSkus - %numSkus);
        %itemsString = (1.0 == %diff) ? "item is" : "items are";
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
    if ((0.0 == %numValidSkus)) {
        if ((1.0 == %numSkus)) {
            MessageBoxOK("Not Available", "This item is not available for vBux.", "");
        }
        MessageBoxOK("Not Available", "These items are not available for vBux.", "");
        return;
    }
    %totalPrice = Inventory::getTotalPrice("vBux", %skus);
    if (($Player::VBux > %totalPrice)) {
        %this.doInsufficientVBux();
        return;
    }
    %desc = "these " @ %numValidSkus @ " items";
    if ((1.0 == %numValidSkus)) {
        %si = SkuManager.findBySku(%skus);
        if (!(isObject(%si))) {
            return;
        }
        %desc = %si.descShrt;
    }
    %note = "";
    if ((%numSkus > %numValidSkus)) {
        %diff = (%numValidSkus - %numSkus);
        %itemsString = (1.0 == %diff) ? "item is" : "items are";
        %note = "<br><br>Note: " @ %diff @ " " @ %itemsString @ " not available for vBux.";
    }
    %msg = "Do you wish to purchase " @ %desc @ " for " @ %totalPrice @ " vBux?" @ %note;
    %cmd = "ClosetGui.purchaseSkusReally(\"" @ %skus @ "\", \"vBux\");";
    MessageBoxOkCancel("Confirm Purchase", %msg, %cmd, "");
};
function ClosetGui::doCheckout(%this) {
    %this.purchaseSkus(StoreShoppingList.getSkus());
};
function ClosetGui::purchaseSkusReally(%this, %skus, %currency) {
    %array = new Array("");;
    0;
    %n = (1.0 - getWordCount(%skus));
    if ((0.0 >= %n)) {
        %sku = getWord(%skus, %n);
        %array.push_front(%sku, 1);
        %n = (1.0 - %n);
    }
    %request = sendRequest_PurchaseInventory($Player::Name, %array, %currency, $gCurrentStoreName, "closet_onDoneOrErrorCallback_PurchaseInventory");
    (0.0 >= %n);
    %request.currency = %currency;
    %request.callbackData = %this;
    %request.timedOutAlready = 0;
    %array.delete();
    StoreShoppingBag.setVisible(%request.waitIcon, 1);
    StoreShoppingBag.start(%request.waitIcon);
    %this.numberOfPurchasesAwaitingCompletion = (1.0 + %this.numberOfPurchasesAwaitingCompletion);
    %tab = ClosetTabs.getTabWithName("SHOPS");
    %tab.doneButton.setActive(!(%this.isWaitingForPurchaseCompletion()));
    %tab.cancelButton.setActive(!(%this.isWaitingForPurchaseCompletion()));
    %this.checkoutPopup = MessageBoxOK(, , "");
    %this.schedule(30000, purchaseSkusRequestTimedOut, %request);
};
function ClosetGui::purchaseSkusRequestTimedOut(%this, %request) {
    if (!(isObject(%request))) {
    }
    if ((%request.statusCode() $= "")) {
        return;
    }
    %request.timedOutAlready = 1;
    %this.numberOfPurchasesPastTimeout = (1.0 + %this.numberOfPurchasesPastTimeout);
    %tab = ClosetTabs.getTabWithName("SHOPS");
    %tab.doneButton.setActive(!(%this.isWaitingForPurchaseCompletion()));
    %tab.cancelButton.setActive(!(%this.isWaitingForPurchaseCompletion()));
    %this.processingTimeoutPopup = MessageBoxOK(, , "");
};
function ClosetGui::isWaitingForPurchaseCompletion(%this) {
    return (%this.numberOfPurchasesPastTimeout > %this.numberOfPurchasesAwaitingCompletion);
};
function closet_onDoneOrErrorCallback_PurchaseInventory(%request) {
    %request.callbackData.onDoneOrErrorCallback_PurchaseInventory(%request);
};
function ClosetGui::onDoneOrErrorCallback_PurchaseInventory(%this, %request) {
    StoreShoppingBag.stop(%request.waitIcon);
    StoreShoppingBag.setVisible(%request.waitIcon, 0);
    %this.numberOfPurchasesAwaitingCompletion = (1.0 - %this.numberOfPurchasesAwaitingCompletion);
    if (%request.timedOutAlready) {
        %this.numberOfPurchasesPastTimeout = (1.0 - %this.numberOfPurchasesPastTimeout);
    }
    %tab = ClosetTabs.getTabWithName("SHOPS");
    %tab.doneButton.setActive(!(%this.isWaitingForPurchaseCompletion()));
    %tab.cancelButton.setActive(!(%this.isWaitingForPurchaseCompletion()));
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
    %n = (1.0 - %request.getValue("itemsCount"));
    if ((0.0 >= %n)) {
        %sku = %request.getValue("items" @ %n @ ".sku");
        %validationResults = %request.getValue("items" @ %n @ ".validationResults");
        %m = (1.0 - getFieldCount(%validationResults));
        if ((0.0 >= %m)) {
            %validationResult = getField(%validationResults, %m);
            %validationResult[%skuResults @ %validationResult] = %validationResult[%skuResults @ %validationResult] @ %sku @ " ";
            %m = (1.0 - %m);
        }
        %n = (1.0 - %n);
        (0.0 >= %m);
    }
    if (!(%request.checkSuccess())) {
        %errorCode = %request.getValue("errorCode");
        (0.0 >= %n);
        if ((%errorCode $= "staleInventory")) {
            %request = sendRequest_GetStoreInventory($Player::Name, $gCurrentStoreName, "OnGotDoneOrError_GetStoreInventory");
            %request.shoppingCartSkus = StoreShoppingList.getSkus();
            StoreShoppingList.clear();
        }
        if ((%errorCode $= "insufficientTotalFunds")) {
            %msgName = (%request.currency $= "vpoints") ? "E-NO-VPOINTS" : "E-NO-VBUX";
            MessageBoxOK(%msgName[$MsgCat::commerce @ "E-TITLE"], %msgName[$MsgCat::commerce @ %msgName], "");
        }
        if ((%errorCode $= "unacquirableItems")) {
            if (!(%errorCode[%skuResults @ "OutOfStock"] $= "")) {
                MessageBoxYesNo(%errorCode[%skuResults @ "OutOfStock"][$MsgCat::commerce @ "E-TITLE"], , "StoreShoppingList.removeSkus(\"" @ "\");", "");
            }
            MessageBoxOK(, , "");
        }
        MessageBoxOK(, , "");
    }
    %this.handleAnyPurchasedSkus(, %request.timedOutAlready);
};
function ClosetGui::handleAnyPurchasedSkus(%this, %skulist, %delayed) {
    %skusToFlatten = "";
    %skusPurchased = %skulist;
    %n = (1.0 - getWordCount(%skulist));
    if ((0.0 >= %n)) {
        %sku = getWord(%skulist, %n);
        if ((-(1.0) == findWord($Player::inventory, %sku))) {
            $Player::inventory = %sku @ " " @ $Player::inventory;
        }
        error(getScopeName() @ " " @ "- already have SKU:" @ " " @ %sku);
        if ((0.0 > %sku[$gStoreItemsQty @ %sku])) {
            %sku[$gStoreItemsQty @ %sku] = (1.0 - %sku[$gStoreItemsQty @ %sku]);
        }
        if ((-(1.0) != findWord($StoreSkusLayer, %sku))) {
            %skusToFlatten = %skusToFlatten @ " " @ %sku;
        }
        %n = (1.0 - %n);
    }
    if (!((0.0 >= %n) @ " " @ %skulist $= "")) {
        %callback = "StoreShoppingList.removeSkus(\"" @ %skusPurchased @ "\");";
        if (%delayed) {
            MessageBoxOK("Purchase Complete", , %callback);
        }
        MessageBoxOK("Purchase Complete", , %callback);
    }
    if (!(%skusToFlatten $= "")) {
        %skusToFlatten = trim(%skusToFlatten);
        %newStoreSkus = "";
        %n = (1.0 - getWordCount($StoreSkusLayer));
        if ((0.0 >= %n)) {
            %sku = getWord($StoreSkusLayer, %n);
            if (!(hasWord(%skusToFlatten, %sku))) {
                %newStoreSkus = %newStoreSkus @ " " @ %sku;
            }
            %n = (1.0 - %n);
        }
        $StoreSkusLayer = trim(%newStoreSkus);
        (0.0 >= %n);
        %skusToFlattenClothing = SkuManager.filterSkusForClothing(%skusToFlatten);
        %skusToFlattenBody = SkuManager.filterSkusForBody(%skusToFlatten);
        $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = SkuManager.overlaySkus($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], %skusToFlattenClothing);
        $ClosetSkusBody = SkuManager.overlaySkus($ClosetSkusBody, %skusToFlattenBody);
    }
    StoreItemsFrame.update();
};
function CheckoutRequest::onClosed(%this) {
};
function CheckoutRequest::onError(%this, %unused, %unused) {
    StoreShoppingBag.stop(%request.waitIcon);
    StoreShoppingBag.setVisible(%request.waitIcon, 0);
    if (isObject(ClosetGui, %request.checkoutPopup)) {
        ClosetGui.close(%request.checkoutPopup);
    }
    MessageBoxOK("Connection Error", , "");
    %this.onClosed();
};
function CheckoutRequest::onDone(%this) {
    log("network", "debug", getScopeName() @ " " @ "- url =" @ " " @ %this.getURL());
    StoreShoppingBag.stop(%request.waitIcon);
    StoreShoppingBag.setVisible(%request.waitIcon, 0);
    if (isObject(ClosetGui, %request.checkoutPopup)) {
        ClosetGui.close(%request.checkoutPopup);
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
        if (1) {
            %line = %this.getValue("sku" @ %i);
            if ((%line $= "")) {
            }
            %sku = getField(%line, 0);
            %result = getField(%line, 1);
            if ((%result $= "buy_ok")) {
                %skusPurchased = %skusPurchased @ " " @ %sku;
                if ((-(1.0) == findWord($Player::inventory, %sku))) {
                    $Player::inventory = %sku @ " " @ $Player::inventory;
                }
                error(getScopeName() @ " " @ "- already have SKU:" @ " " @ %sku);
                if ((0.0 > %sku[$gStoreItemsQty @ %sku])) {
                    %sku[$gStoreItemsQty @ %sku] = (1.0 - %sku[$gStoreItemsQty @ %sku]);
                }
                if ((-(1.0) != findWord($StoreSkusLayer, %sku))) {
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
            %i = (1.0 + %i);
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
            MessageBoxYesNo("Sold Out", , "StoreShoppingList.removeSkus(\"" @ %skusSoldOut @ "\");", "");
        }
        if (!(%skusPurchased $= "")) {
            MessageBoxOK("Purchase Complete", , "StoreShoppingList.removeSkus(\"" @ %skusPurchased @ "\");");
        }
        if (!(%skusToFlatten $= "")) {
            %skusToFlatten = trim(%skusToFlatten);
            %newStoreSkus = "";
            %i = 0;
            if ((getWordCount($StoreSkusLayer) < %i)) {
                %sku = getWord($StoreSkusLayer, %i);
                if ((-(1.0) == findWord(%skusToFlatten, %sku))) {
                    %newStoreSkus = %newStoreSkus @ " " @ %sku;
                }
                %i = (1.0 + %i);
            }
            $StoreSkusLayer = trim(%newStoreSkus);
            (getWordCount($StoreSkusLayer) < %i);
            %skusToFlattenClothing = SkuManager.filterSkusForClothing(%skusToFlatten);
            %skusToFlattenBody = SkuManager.filterSkusForBody(%skusToFlatten);
            $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = SkuManager.overlaySkus($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], %skusToFlattenClothing);
            $ClosetSkusBody = SkuManager.overlaySkus($ClosetSkusBody, %skusToFlattenBody);
        }
        StoreItemsFrame.update();
    }
    %this.onClosed();
};
function ClosetGui::selectGenre(%this, %val) {
    $UserPref::Player::Genre = %val;
    %anim = ;
    %triesLeft = 10;
    if ((0.0 > %triesLeft)) {
    }
    if ((%anim $= $gClosetStanceEmotesLast)) {
        %anim = ;
        %triesLeft = (1.0 - %triesLeft);
        if ((0.0 > %triesLeft)) {
        }
    }
    $gClosetStanceEmotesLast = %anim;
    (%anim $= $gClosetStanceEmotesLast);
    $player.playAnim($player.getGender() @ %val @ %anim);
};
function ClosetGui::updateVisibleAvatar(%this) {
    %merged = $ClosetSkusBody @ " " @ $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName];
    if ((ClosetTabs.getCurrentTab().name $= "SHOPS")) {
        %merged = SkuManager.overlaySkus($ClosetSkusBody @ " " @ $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], $StoreSkusLayer);
    }
    if ((ClosetTabs.getCurrentTab().name $= "CLOSET")) {
        ClosetWhatYoureWearingList.refresh($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName]);
    }
    if ((ClosetTabs.getCurrentTab().name $= "MY DESIGNS")) {
        %merged = SkuManager.overlaySkus($ClosetSkusBody @ " " @ $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], $gSkusMyShopLayer);
        ClosetWhatYoureWearingList.refresh($gSkusMyShopLayer);
    }
    ClosetMainObjectView.setSkus(%merged);
    %snapTab = ClosetTabs.getTabWithName("SNAPSHOT");
    if (%snapTab) {
    }
    if (isObject(%snapTab.objView)) {
        %snapTab.objView.setSkus(%merged);
    }
    %badge = SkuManager.filterSkusDrwr(%merged, "badges");
    %si = SkuManager.findBySku(%badge);
    %bitmap = "";
    if (isObject(%si)) {
        %bitmap = %si.getBitmapPath();
    }
    ClosetMainBadgeView.setBitmap(%bitmap);
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
    ClosetMainObjectView.zoomToSKU(%sku);
    %thumbnails = ClosetTabs.getCurrentTab().thumbnails;
    if (isObject(%thumbnails)) {
        %thumbnails.setSelectedThumbs();
        %count = %thumbnails.getCount();
        %i = 0;
        if ((%count < %i)) {
            %cell = %thumbnails.getObject(%i);
            %thumbnails.setCellSkus(%cell, %cell.sku);
            %i = (1.0 + %i);
        }
    }
};
function ClosetGui::doArrow(%this, %dx, %dy) {
    if ((ClosetTabs.getCurrentTab().name $= "SNAPSHOT")) {
        ProfileObjectView.moveBy(%dx, -(%dy));
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
        ClosetGui.close(0);
    }
    if ((getWord(%url, 0) $= "CANCEL")) {
        ClosetGui.close(1);
    }
    if ((getWord(%url, 0) $= "TOGGLE_SKU")) {
        ClosetGui.toggleSku(getWord(%url, 1));
    }
};
function ClosetItemsScroll::getIndexForSku(%this, %sku) {
    %thumbnails = %this.thumbnails;
    %count = %thumbnails.getCount();
    %i = 0;
    if ((%count < %i)) {
        if ((%sku == %thumbnails.getObject(%i).sku)) {
            return %i;
        }
        %i = (1.0 + %i);
    }
    return -(1.0);
};
function ClosetItemsScroll::scrollToSku(%this, %sku) {
    %thumbnails = %this.thumbnails;
    %idx = %this.getIndexForSku(%sku);
    if ((0.0 < %idx)) {
        if ((ClosetTabs.getCurrentTab().name $= "CLOSET")) {
            ClosetTabs.getCurrentTab().brand = ClosetBrandPopup.getTextById(0) @ ClosetItemsFrame;
            ClosetTabs.getCurrentTab().category = ClosetItemPopup.getTextById(0) @ ClosetItemsFrame;
            ClosetItemsFrame.update();
            ClosetBrandPopup.SetSelected(0);
            ClosetItemPopup.SetSelected(0);
            %idx = %this.getIndexForSku(%sku);
        }
        if ((ClosetTabs.getCurrentTab().name $= "SHOPS")) {
            if ((0.0 != StoreCategoryPopup.GetSelected())) {
                StoreCategoryPopup.SetSelected(0);
            }
            %idx = %this.getIndexForSku(%sku);
        }
    }
    if ((0.0 < %idx)) {
        return;
    }
    %row = mFloor((%thumbnails.numRowsOrCols / %idx));
    %col = (%thumbnails.numRowsOrCols % %idx);
    %thumbnails.hiliteCell(%col, %row);
    %this.scrollToCellIndex(%idx);
    ClosetMainObjectView.zoomToSKU(%sku);
};
function ClosetItemsScroll::scrollToCell(%this, %cell) {
    %thumbnails = %this.thumbnails;
    %cellIdx = %thumbnails.getObjectIndex(%cell);
    %this.scrollToCellIndex(%cellIdx);
};
function ClosetItemsScroll::scrollToCellIndex(%this, %cellIdx) {
    %thumbnails = %this.thumbnails;
    %cellHeight = (%thumbnails.spacing + getWord(%thumbnails.childrenExtent, 1));
    %ypos = (getWord(%thumbnails.getPosition(), 1) - 1.0);
    %closestRow = mFloor((0.5 + (%cellHeight / %ypos)));
    %targetRow = mFloor((4.0 / %cellIdx));
    if ((0.0 < %cellIdx)) {
        %targetRow = %closestRow;
    }
    if (((1.0 + %closestRow) >= %targetRow)) {
        %thumbnails.getParent().scrollTo(0, ((1.0 - %targetRow) * %cellHeight));
    }
    %thumbnails.getParent().scrollTo(0, (%targetRow * %cellHeight));
};
function ClosetItemsScroll::onMouseUp(%this) {
    %this.scrollToCellIndex(-(1.0));
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
    if (($gClosetNumOutfits != %numOutfitNames)) {
        %numOutfitNamesBroken = 1;
        error(getScopeName() @ "->Number of outfits named in Player::HangerNames for player gender is not equal to $gClosetNumOutfits! Will cause errors!");
        %errMsg = %errMsg @ " " @ "(getWordCount($Player::HangerNames[gender]) != $gClosetNumOutfits)";
    }
    %currentOutfit = $gOutfits.get("currentOutfit");
    if ((%currentOutfit $= "")) {
    }
    if ((0.0 < findWord(%outfitNames, %plyrGendr @ %currentOutfit))) {
        %noCurrentOutfit = 1;
        error(getScopeName() @ "-> gOutfits->currentOutfit is blank or invalid! should NEVER happen! currentOutfit = \"" @ %currentOutfit @ "\"");
        %errMsg = %errMsg @ " " @ "(gOutfits->currentOutfit = " @ %currentOutfit @ ")";
    }
    if (%numOutfitNamesBroken) {
        %max = %numOutfitNames;
    }
    %max = $gClosetNumOutfits;
    if (%checkClosetVariables) {
        if (( < findWord(0.0, $ClosetOutfitName))) {
            error(getScopeName() @ "-> can't find $ClosetOutfitName in $Player::HangerNames for this gender! $ClosetOutfitName = \"" @ $ClosetOutfitName @ "\"");
            %errMsg = %errMsg @ " " @ "($ClosetOutfitName = \"" @ $ClosetOutfitName @ "\")";
        }
        %n = (1.0 - %max);
        if ((0.0 >= %n)) {
            %name = getWord(%outfitNames, %n);
            %curOutfit = outfits_filterSKUList(%name[$ClosetSkusOutfit @ %name]);
            if ((%curOutfit $= "")) {
                %outfitsCorrupted = (1.0 + %outfitsCorrupted);
            }
            %n = (1.0 - %n);
        }
        if ((0.0 > %outfitsCorrupted)) {
            error(getScopeName() @ "-> " @ %outfitsCorrupted @ " blank outfits detected!");
            %errMsg = %errMsg @ " " @ "(" @ %outfitsCorrupted @ " blank outfits)";
            (0.0 >= %n);
        }
    }
    if ((0.0 > %outfitsCorrupted)) {
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
    warn("->$Player::HangerNames[$player.getGender]:");
    warn(getScopeName() @ "->player inventory: " @ $Player::inventory);
};
function filterOutSkusToHideInCloset(%skus) {
    if ((%skus $= "")) {
        return %skus;
    }
    if (($gSkusToHideInCloset $= "")) {
        return %skus;
    }
    %i = (1.0 - getWordCount($gSkusToHideInCloset));
    if ((0.0 >= %i)) {
        %skuToHide = getWord($gSkusToHideInCloset, %i);
        %skus = findAndRemoveAllOccurrencesOfWord(%skus, %skuToHide);
        %i = (1.0 - %i);
    }
    return %skus;
};
function ClosetTabs::createFilterWidget(%this) {
    if (isObject(ClosetFilterContainer)) {
        ClosetFilterContainer.setVisible(1);
        ClosetFilterField.makeFirstResponder(1);
    }
    new GuiControl(ClosetFilterContainer) {
        profile = ClosetFilterContainer @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "335 64";
        extent = "148 40";
    };
    ClosetFilterField.makeFirstResponder(1);
};
$gClosetFilterFieldTimerID = "";
function ClosetFilterField::OnTextChanged(%this) {
    cancel($gClosetFilterFieldTimerID);
    $gClosetFilterFieldTimerID = %this.schedule(%this.timeoutMS, "onTimer");
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
        ClosetAuthorContainer.setVisible(1);
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
        profile = ClosetAuthorContainer @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "689 84";
        extent = "245 110";
    };
};
function ClosetTabs::updateAuthorWidget(%this, %sku) {
    if (!(isObject(ClosetAuthorContainer))) {
        return;
    }
    if (!(%sku $= "")) {
    }
    %si = "";
    SkuManager.findBySku(%sku);
    %filled = 0;
    if (isObject(%si)) {
        if (!(%si.author $= "")) {
            %filled = 1;
            if ((%si.author $= "?")) {
                ClosetAuthorPicture.setBitmap("platform/client/ui/tgf/tgf_profile_default_" @ $player.getGender());
                %si.modulationColor = "255 255 255 50" @ ClosetAuthorPicture;
                ClosetAuthorPictureOutline.setVisible(1);
                ClosetAuthorText.setText("<just:right><font:Arial:12><color:00000044><linkcolor:00000066>" @ "oh nos!<br>" @ "we've lost track of who made this!<br>");
            }
            %playerEncoded = urlEncode(stripUnprintables(%si.author));
            %profileURL = $Net::ProfileURL @ %playerEncoded;
            %pictureURL_M = $Net::AvatarURL @ %playerEncoded @ "?size=M";
            %pictureURL_L = $Net::AvatarURL @ %playerEncoded @ "?size=L";
            ClosetAuthorPicture.setBitmap("");
            ClosetAuthorPicture.downloadAndApplyBitmap(%pictureURL_M);
            ClosetAuthorPicture.downloadAndApplyBitmap(%pictureURL_L);
            %si.modulationColor = "255 255 255 255" @ ClosetAuthorPicture;
            ClosetAuthorPictureOutline.setVisible(1);
            ClosetAuthorText.setText("<just:right><font:Arial:12><color:00000044><linkcolor:00000066>" @ "design by<br><a:" @ %profileURL @ ">" @ %si.author @ "</a>");
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
            ClosetAuthorPicture.setBitmap("platform/client/ui/vside_icon_38x38");
            %si.modulationColor = "255 255 255 20" @ ClosetAuthorPicture;
            ClosetAuthorPictureOutline.setVisible(0);
            ClosetAuthorText.setText("<just:right><font:Arial:12><color:00000044><linkcolor:00000066>" @ "brand:<br>" @ %fullBrand);
        }
    }
    if (!(%filled)) {
        ClosetAuthorPicture.setBitmap("platform/client/ui/vside_icon_38x38");
        %si.modulationColor = "255 255 255 20" @ ClosetAuthorPicture;
        ClosetAuthorPictureOutline.setVisible(0);
        ClosetAuthorText.setText("");
    }
};
function ClosetTabs::createWhatYourWearingPanel(%this) {
    if (isObject(ClosetWhatYoureWearingPanel)) {
    }
    %whatYoureWearingPanel = new GuiWindowCtrl(ClosetWhatYoureWearingPanel) {
        profile = ClosetWhatYoureWearingPanel @ "DottedWindowProfile";
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
        profile = 0 @ "ETSScrollProfile";
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
    %whatYoureWearingScroll.add(%whatYoureWearingList);
    %whatYoureWearingPanel.add(%whatYoureWearingScroll);
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
        %this.onSystemDragDroppedEvent_MyShop(%text, %pt);
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
