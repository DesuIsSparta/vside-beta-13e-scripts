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
ignoreCase = new StringMap(ThumbCategories) @ 1;
if (isObject()) {
    add();
}
"all items".put("torso torsob legs legsb feet ear neck neckb neckc chest waist waistb wristleft wristleftb wristright wristrightb fingerleft fingerright toeleft toeright glasses back hat mask tail purse props badges tokens");
"all garments".put("torso torsob chest legs legsb feet");
"all accessories".put("ear neck neckb neckc waist waistb wristleft wristleftb wristright wristrightb fingerleft fingerright toeleft toeright chest back hat tail mask purse props badges");
"all features".put("face faceb eyes skin hair");
"tops".put("torso torsob chest");
"bottoms".put("legs legsb");
"hair".put("hair hat");
"shoes".put("feet toeleft toeright");
"ear".put("ear");
"neck".put("neck neckb neckc");
"waist".put("waist waistb");
"hands".put("wristleft wristleftb wristright wristrightb fingerleft fingerright");
"bags".put("purse");
"misc".put("chest back hat tail mask");
"bodymod".put("earl labret lftauricle lftconch lfteyebrow lftlobe lftorbital lftpinna lftrook lfttragus rghauricle rghconch rgheyebrow rghlobe rghorbital rghpinna rghrook rghtragus lowlip madonna medusa nostril septum");
"glasses".put("glasses");
"face".put("face faceb");
"eyes".put("eyes");
"skin".put("skin");
"props".put("props");
"badges".put("badges");
"tokens".put("tokens");
buildSkusSearchText();
new StringMap(ThumbCategoriesOrder);
if (isObject()) {
    add();
}
%n = 0;
ThumbCategoriesOrder;
%n.put("tops");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("bottoms");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("hair");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("shoes");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("ear");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("neck");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("waist");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("hands");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("bags");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("props");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("misc");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("bodymod");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("glasses");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("face");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("eyes");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("skin");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("badges");
%n = (1.0 + %n);
ThumbCategoriesOrder;
%n.put("tokens");
%n = (1.0 + %n);
ThumbCategoriesOrder;
$tmpGender = "f";
MissionCleanup;
$tmpGender[MissionCleanup @ "0 0 0.0 1.7 35" @ $ThumbCamParams TAB $tmpGender @ "fullbody"] = ThumbCategories @ SkuManager;
ThumbCategories;
$tmpGender[ThumbCategories @ "0.4 -0.3 0.8 1.0 20" @ $ThumbCamParams TAB $tmpGender @ "hair"] = ThumbCategories @ ThumbCategories;
ThumbCategories;
$tmpGender[ThumbCategories @ "0.4 -0.3 0.8 1.0 15" @ $ThumbCamParams TAB $tmpGender @ "face"] = ThumbCategories @ ThumbCategories;
ThumbCategories;
$tmpGender[ThumbCategories @ $tmpGender[ThumbCategories @ $ThumbCamParams TAB $tmpGender @ "face"] @ $ThumbCamParams TAB $tmpGender @ "faceb"] = ThumbCategories @ ThumbCategories;
ThumbCategories;
$tmpGender[ThumbCategories @ "0.4 -0.3 0.8 1.0 10" @ $ThumbCamParams TAB $tmpGender @ "eyes"] = ThumbCategories @ ThumbCategories;
ThumbCategories;
$tmpGender[ThumbCategories @ $tmpGender[ThumbCategories @ $ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "ear"] = ThumbCategories @ ThumbCategories;
MissionCleanup;
$tmpGender[$tmpGender[MissionCleanup @ $ThumbCamParams TAB $tmpGender @ "eyes"] @ $ThumbCamParams TAB $tmpGender @ "earl"] = ;
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
if (!(isObject())) {
    class = ClosetTabs @ new ScriptObject(ClosetTabs) @ "TabControl";
    if (isObject()) {
        add();
    }
}
function Closet::skuListHasCategory(%list, %category) {
    %drawers = %category.get();
    ThumbCategories;
    %n = (1.0 - getWordCount(%drawers));
    ClosetTabs;
    if ((0.0 >= %n)) {
        if (%list.skuListHasDrawer(getWord(%drawers, %n))) {
            return 1;
        }
        %n = (1.0 - %n);
    }
    return 0;
};
function ClosetTabs::setup(%this) {
    if (!(initialized)) {
        initializing = %this @ 1 @ %this;
        %this.Initialize("103 21", "", "", "horizontal");
        %this.newTab("Shops", "platform/client/buttons/closet_tab");
        %this.newTab("Closet", "platform/client/buttons/closet_tab");
        %this.newTab("Body", "platform/client/buttons/closet_tab");
        %this.newTab("Snapshot", "platform/client/buttons/closet_tab");
        %this.newTab("My Designs", "platform/client/buttons/closet_tab");
        lastTabOpened = ClosetTabContainer @ "" @ ClosetGui;
        numberOfPurchasesAwaitingCompletion = 0 @ ClosetGui;
        numberOfPurchasesPastTimeout = 0 @ ClosetGui;
        initialized = 1 @ %this;
    }
    initializing = 0 @ %this;
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
    profile = GuiBitmapButtonCtrl @ new ""() @ "ClosetTabButtonProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = %this @ buttonSize;
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
    return;
};
$ClosetCategoryGroup = 286331153;
$ClosetBrandGroup = 286331154;
$BodyFeaturesGroup = 286331155;
$StoreCategoryGroup = 286331156;
$BodyStanceGroup = 286331157;
$ClosetHangersGroup = 286331158;
function ClosetTabs::tabSelected(%this, %tab) {
    lastTabOpened = %tab @ name @ ClosetGui;
    if ((%tab SPC name $= "CLOSET")) {
    }
    if ((%tab SPC name $= "BODY")) {
    }
    if ((%tab SPC name $= "SHOPS")) {
    }
    if ((%tab SPC name $= "MY DESIGNS")) {
        1.setVisible();
        3.2.setOrbitDist();
        "0 0 0.1".setLookAtNudge();
        "0 3 -2".setLightDirection();
        %tab.add();
        %tab.bringToFront();
        1.setVisible();
        %tab.add();
        %tab.bringToFront();
        %tab.add();
        %tab.bringToFront();
    }
    0.setVisible();
    1.setVisible();
    if (isObject(hiliteStrip)) {
        %offset = %this.getInitialButtonOffset();
        %tab;
        %xoffset = getWord(%offset, 0);
        ClosetMainBadgeView;
        %yoffset = (2.0 - getWord(%offset, 1));
        ClosetMainObjectView;
        hiliteStrip.resize(%xoffset, %yoffset, visibleTabsWidth, 2);
    }
    if ((%tab SPC name $= "BODY")) {
        $player.setGenre("p");
        if (!(tabBodyInitialized)) {
            %this.fillBodyTab();
        }
        rebuildPopupList();
        update();
        %tab.add(%this.createFilterWidget());
        systemDragDrop = BodyItemsFrame @ 0 @ ClosetMainObjectView;
        BodyFeaturesPopup;
        updateBodyTabDisplay();
    }
    if ((%tab SPC name $= "CLOSET")) {
        $player.setGenre("p");
        if (!(tabClosetInitialized)) {
            %this.fillClosetTab();
        }
        getFilteredInventoryForSetDrawers().update();
        if ((ClosetBrandPopup > size())) {
            0.SetSelected();
        }
        update();
        %tab.add(%this.createFilterWidget());
        %tab.add(%this.createAuthorWidget());
        %this.createWhatYourWearingPanel().reparentSameSize("");
        "You Are Wearing".setTextWithStyle();
        filterByRemovable = ClosetWhatYoureWearingTitle @ 1 @ ClosetWhatYoureWearingList;
        ClosetWhatYourWearingContainer;
        systemDragDrop = ClosetItemsFrame @ 0 @ ClosetMainObjectView;
        ClosetBrandPopup;
        %outfitNames = 0.0;
        ClosetBrandPopup;
        %i = 0;
        %this;
        if (($gClosetNumOutfits < %i)) {
            %name = getWord(%outfitNames, %i);
            ClosetTabs;
            %objectView = %i.getOutfitObjectView();
            ClosetTabs;
            %objectView.setSimObject($player);
            %objectView.setSkus($ClosetSkusBody @ " " @ %name[$ClosetSkusOutfit @ %name]);
            %i = (1.0 + %i);
            %this;
        }
        %outfitNum = findWord(($gClosetNumOutfits < %i), $ClosetOutfitName);
        %this;
        %outfitNum.getOutfitButton().performClick();
    }
    if ((%tab SPC name $= "SHOPS")) {
        $player.setGenre("p");
        if (!(tabShopsInitialized)) {
            %this.fillStoreTab();
        }
        if (!(StoreExpirationLegend SPC lastStore $= $gCurrentStoreName)) {
            lastStore = %this @ $gCurrentStoreName @ StoreExpirationLegend;
            ClosetTabs;
            0.setVisible();
        }
        %this.showTabWithName("Shops");
        update();
        "".setBaseDesc();
        "".setBaseDesc();
        if (!(StoreLongDescText SPC $gCurrentStoreName $= "")) {
            0.setLeaveStoreControlsVisible();
            1.setStoreControlsVisible();
            nameCtrl.setText(Inventory::getCurrentStoreName());
            descCtrl.setText(Inventory::getCurrentStoreDescInCloset());
            %storename = getCurrentStoreID();
            StoreNameDescFrame;
            %bannerRsrc = "";
            StoreNameDescFrame;
            if (!(ClosetTabs SPC %storename $= "")) {
                removeShopBannerCache(%storename);
                %bannerRsrc = ClosetTabs @ "platform/client/buttons/banners/store_" @ %storename;
                StoreShortDescText;
                %bannerRsrc.applyUrl("dlMgrCallback_ShopTexture", "dlMgrCallback_ShopError", %this, "storeads");
            }
            if (!(dlMgr SPC %bannerRsrc $= "")) {
                1.setVisible();
                %bannerRsrc.setBitmap();
            }
            0.setVisible();
            %bgResource = "";
            StoreBannerBrackets;
            if (!(StoreBanner SPC %storename $= "")) {
                %bgResource = StoreBannerBrackets @ "platform/client/ui/store_backgrounds/store_bg_" @ %storename;
                StoreBalanceText;
            }
            if (!(StoreExpirationLegend SPC %bgResource $= "")) {
                %bgResource.setBitmap();
                1.setVisible();
                %tab.bringToFront();
            }
            0.setVisible();
        }
        0.setStoreControlsVisible();
        !(isInFUE()).setLeaveStoreControlsVisible();
        0.setVisible();
        %tab.add(%this.createFilterWidget());
        if ((StoreSpecificBackground SPC $gCurrentStoreName $= "")) {
            %this.createFilterWidget().setVisible(0);
        }
        %tab.add(%this.createAuthorWidget());
        systemDragDrop = ClosetTabs @ 0 @ ClosetMainObjectView;
        ClosetTabs;
        refreshStoreTab();
    }
    if ((%tab SPC name $= "SNAPSHOT")) {
        doResetGenre();
        if (!(tabSnapshotInitialized)) {
            %this.fillProfileTab();
        }
        %objView = objView;
        "SNAPSHOT".getTabWithName();
        %objView.setSimObject($player);
        %objView.setSkus(getSkus());
        if (isObject()) {
        }
        returnClosetGuiFUE = ClosetGuiFUE @ visible @ ProfileSnapRegion;
        ClosetGuiFUE;
        Initialize();
        "0 3 -2".setLightDirection();
        2.4.setOrbitDist();
        systemDragDrop = ProfileObjectView @ 0 @ ClosetMainObjectView;
        ProfileObjectView;
    }
    if ((%tab SPC name $= "MY DESIGNS")) {
        $player.setGenre("p");
        if (!(tabMyShopInitialized)) {
            %this.fillMyShopTab();
        }
        %this.showTabWithName("MY DESIGNS");
        %tab.add(%this.createFilterWidget());
        getGroup().pushToBack();
        systemDragDrop = MyShopTextureInspector @ 1 @ ClosetMainObjectView;
        MyShopTextureInspector;
        %this.createWhatYourWearingPanel().reparentSameSize("");
        "Custom Items".setTextWithStyle();
        filterByRemovable = ClosetWhatYoureWearingTitle @ 0 @ ClosetWhatYoureWearingList;
        MyShopWhatYourWearingContainer;
    }
    doneButton.setActive(!(isWaitingForPurchaseCompletion()));
    cancelButton.setActive(!(isWaitingForPurchaseCompletion()));
    if (0) {
        if (isObject(thumbnails)) {
            thumbnails.makeFirstResponder(1);
        }
        %fr = getFirstResponder();
        Canvas;
        if (isObject(%fr)) {
            %fr.makeFirstResponder(0);
        }
    }
    if (isObject()) {
        1.makeFirstResponder();
    }
    if (!(%tab SPC name $= "CLOSET")) {
        updateVisibleAvatar();
    }
    "".zoomToSKU();
    if (isInFUE()) {
    }
    if (!(initializing)) {
        name.goToStepByName();
    }
};
function dlMgrCallback_ShopTexture(%dlItem, %unused) {
    localFilename.setBitmap();
};
function dlMgrCallback_ShopError(%dlItem) {
    log("network", "debug", "Image Download Error!! " @ " " @ %dlItem);
};
function ClosetTabs::updateRangeText(%this) {
    %currentTab = %this.getCurrentTab();
    if (!(isObject(%currentTab))) {
        return;
    }
    %rangeText = rangeText;
    %currentTab;
    %thumbnails = thumbnails;
    %currentTab;
    if (!(isObject(%thumbnails))) {
        return;
    }
    %cellHeight = (%thumbnails + getWord(childrenExtent, 1));
    spacing;
    %ypos = (getWord(%thumbnails.getPosition(), 1) - 1.0);
    %thumbnails;
    %closestRow = mFloor((0.5 + (%cellHeight / %ypos)));
    %count = %thumbnails.getCount();
    %min = mMin(((numRowsOrCols * %closestRow) + 1.0), %count);
    %thumbnails;
    %max = mMin((7.0 + %min), %count);
    if ((0.0 > %count)) {
    }
    %rangeText.setText("");
    return %closestRow;
};
function ClosetTabs::getShortSkuDesc(%this, %sku) {
    if ((0.0 <= %sku)) {
        return "";
    }
    %skuInfo = %sku.findBySku();
    SkuManager;
    %ret = "";
    %ret = %ret @ "<spush><b>" @ %skuInfo @ descShrt @ "<spop>";
    return %ret;
};
function ClosetTabs::getLongSkuDesc(%this, %sku) {
    if ((0.0 <= %sku)) {
        return "";
    }
    %skuInfo = %sku.findBySku();
    SkuManager;
    %ret = "";
    if (!(%skuInfo $= trim(descShrt))) {
        %ret = %skuInfo @ descLong;
        %skuInfo SPC trim(descLong) @ %ret;
    }
    if (!(%skuInfo SPC expireTime $= "")) {
        %ret = %skuInfo @ secondsToDaysHoursMinutesSeconds(expireTime) @ " " @ "after you get it.";
        %ret @ "<br><bitmap:platform/client/ui/expiring_icon_small> - expires" @ " ";
    }
    return %ret;
};
function ClosetThumbnails::onCreatedChild(%this, %child) {
    profile = GuiControl @ new ""() @ "ClosetLtBackgroundProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "4 3";
    extent = "95 83";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 0;
    %background = ;
    profile = GuiObjectView @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "4 3";
    extent = "95 83";
    minExtent = "1 1";
    sluggishness = -1;
    CamSluggishness = 0.0000001;
    visible = 1;
    %objectView = ;
    if (isObject($player)) {
        %objectView.setSimObject($player);
    }
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "19 11";
    extent = "64 64";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "";
    %badgeView = ;
    profile = GuiBitmapCtrl @ new ""() @ "GuiModelessDialogProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "38 4";
    extent = "60 60";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 0;
    bitmap = "";
    %buyStatus = ;
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "3 60";
    extent = "25 25";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "";
    %rarityBitmap = ;
    profile = GuiBitmapButtonCtrl @ new ""() @ "ClosetFrameButtonProfile";
    0;
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
    %frameButton = ;
    command = %this.getId() @ ".buttonClicked(" @ %frameButton.getId() @ ");" @ %frameButton;
    %frameButton.bindClassName("ClosetFrameButton");
    profile = GuiControl @ new ""() @ "ETSWhiteProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "2 86";
    extent = "99 42";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 0;
    %buttonBacking = ;
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiButtonProfile";
    0;
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
    %toggleCartButton = ;
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiButtonProfile";
    0;
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
    %buyNowButton = ;
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "5 4";
    extent = "93 81";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 0;
    bitmap = "platform/client/ui/thumbnailFader";
    modulationColor = "255 255 255 30";
    %frameFader = ;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "64 5";
    extent = "32 32";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "";
    modulationColor = "255 255 255 100";
    %logo = ;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "68 53";
    extent = "32 32";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "";
    modulationColor = "255 255 255 115";
    %expiringIcon = ;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "69 4";
    extent = "32 32";
    bitmap = "";
    modulationColor = "255 255 255 80";
    %ugcStatusIcon = ;
    profile = GuiMLTextCtrl @ new ""() @ "ClosetSmallInfoProfile";
    0;
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
    %desc = ;
    profile = GuiMLTextCtrl @ new ""() @ "ClosetPointsProfile";
    0;
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
    %vpointsPrice = ;
    profile = GuiMLTextCtrl @ new ""() @ "ClosetBuxProfile";
    0;
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
    %vbuxPrice = ;
    profile = GuiVariableWidthButtonCtrl @ new ""() @ "HiddenBracketButton15Profile";
    0;
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
    %totalButton = ;
    profile = GuiMLTextCtrl @ new ""() @ "ClosetInStockProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "3 146";
    extent = "97 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    maxChars = -1;
    text = "";
    %inStockText = ;
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "3 132";
    extent = "97 26";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 0;
    bitmap = "platform/client/ui/priceFader";
    modulationColor = "255 255 255 190";
    %priceFader = ;
    profile = GuiMLTextCtrl @ new ""() @ "ClosetAvailabilityProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "3 132";
    extent = "95 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 0;
    maxChars = -1;
    text = "";
    %availabilityText = ;
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
    background = %background @ %child;
    objectView = %objectView @ %child;
    badgeView = %badgeView @ %child;
    buyStatus = %buyStatus @ %child;
    rarityBitmap = %rarityBitmap @ %child;
    frameButton = %frameButton @ %child;
    buttonBacking = %buttonBacking @ %child;
    toggleCartButton = %toggleCartButton @ %child;
    buyNowButton = %buyNowButton @ %child;
    frameFader = %frameFader @ %child;
    logo = %logo @ %child;
    expiringIcon = %expiringIcon @ %child;
    ugcStatusIcon = %ugcStatusIcon @ %child;
    descCtrl = %desc @ %child;
    vpointsCtrl = %vpointsPrice @ %child;
    vbuxCtrl = %vbuxPrice @ %child;
    totalButton = %totalButton @ %child;
    priceFader = %priceFader @ %child;
    availabilityText = %availabilityText @ %child;
    inStockText = %inStockText @ %child;
    thumbnails = %this @ %child;
    selected = 0 @ %child;
    hilited = 0 @ %child;
    available = 1 @ %child;
    if (!(getWord(%child.getNamespaceList(), 0) $= "ClosetThumbnailCtrl")) {
        %child.bindClassName("ClosetThumbnailCtrl");
    }
};
function ClosetThumbnails::buttonClicked(%this, %button) {
    sku.toggleSku();
    %this.makeFirstResponder(1);
};
function ClosetThumbnails::getAllSkusInDrawers(%this, %drwrNames) {
    %skus = "";
    %n = 0;
    if ((getWordCount(%drwrNames) < %n)) {
        %s = getWord(%drwrNames, %n).getSkusDrwr().filterSkusGender($player.getGender());
        SkuManager;
        %skus = %skus @ " " @ %s;
        SkuManager;
        %n = (1.0 + %n);
    }
    return %skus;
};
function getFilteredInventoryForSetDrawers() {
    %inventory = "";
    if ((getCurrentTab() SPC name $= "BODY")) {
        %inventory = $Player::inventory;
        ClosetTabs;
    }
    if ((getCurrentTab() SPC name $= "CLOSET")) {
        %inventory = $Player::inventory;
        ClosetTabs;
    }
    if ((getCurrentTab() SPC name $= "SHOPS")) {
        %inventory = Inventory::getCurrentStoreSkus();
        ClosetTabs;
    }
    if ((getCurrentTab() SPC name $= "MY DESIGNS")) {
        %inventory = "";
        ClosetTabs;
        error(getScopeName() @ " " @ "- unimplemented." @ " " @ getTrace());
    }
    if ((%inventory $= "no store")) {
        %inventory = "";
    }
    %skus = %inventory.filterSkusGender($player.getGender());
    SkuManager;
    %skus = %skus.filterSkusRoles($player.getRolesMask());
    SkuManager;
    return %skus;
};
$gClosetThumbnailsDrawersPrevious = "";
$gUpdatingClosetItemPopupFromThumbnailsSetDrawers = 0;
function ClosetThumbnails::setDrawers(%this, %drwrNames) {
    %skus = getFilteredInventoryForSetDrawers();
    %currentTabName = name;
    getCurrentTab();
    if ((ClosetTabs SPC %currentTabName $= "CLOSET")) {
    }
    if (isObject()) {
        if (!(ClosetItemsFrame SPC brand $= "")) {
            %skus = %skus.filterSkusBrand($gClosetBrandsIntrnl);
            SkuManager;
        }
        $gUpdatingClosetItemPopupFromThumbnailsSetDrawers = 1;
        ClosetBrandPopup;
        %skus.update();
        $gUpdatingClosetItemPopupFromThumbnailsSetDrawers = 0;
        ClosetItemPopup;
        if ((%drwrNames $= "")) {
            if ((ClosetItemsFrame SPC category $= "")) {
                category = "All Items" @ ClosetItemsFrame;
            }
            %category = strlwr(category);
            ClosetItemsFrame;
            %drwrNames = %category.get();
            ThumbCategories;
        }
    }
    %skusTmp = "";
    %n = (1.0 - getWordCount(%drwrNames));
    if ((0.0 >= %n)) {
        %s = %skus.filterSkusDrwr(getWord(%drwrNames, %n));
        SkuManager;
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
    unfilteredSkus = %skus @ %this;
    %this.refilter();
};
function ClosetThumbnails::refilter(%this) {
    %this.setSkus(unfilteredSkus);
};
function ClosetThumbnails::setSkus(%this, %skus) {
    %startingPos = %this.getPosition();
    %currentTabName = name;
    getCurrentTab();
    %skus = filterOutSkusToHideInCloset(%skus);
    ClosetTabs;
    if ((%currentTabName $= "SHOPS")) {
        %skus = %skus.filterSkusNonZeroManufactured();
        SkuManager;
    }
    %skus = trim(%skus);
    if ((%currentTabName $= "CLOSET")) {
    }
    if ((%currentTabName $= "SHOPS")) {
    }
    if ((%currentTabName $= "BODY")) {
    }
    if ((%currentTabName $= "MY DESIGNS")) {
        if (isObject()) {
        }
        %userFilterText = "";
        getValue();
        %skus = %skus.filterSkusDescription(%userFilterText);
        SkuManager;
    }
    if (isObject(otherGenderText)) {
        %numSkusOtherGender = getWordCount(%skus);
        %this;
        %skus = %skus.filterSkusGender($player.getGender());
        SkuManager;
        %numSkus = getWordCount(%skus);
        ClosetFilterField;
        %numSkusOtherGender = (%numSkus - %numSkusOtherGender);
        ClosetFilterField;
        if ((0.0 == %numSkusOtherGender)) {
        }
        %text = "" @ "<just:right>(" @ %numSkusOtherGender @ " in other gender)";
        otherGenderText.setText(%text);
    }
    %numSkus = getWordCount(%skus);
    %this;
    %this.setNumChildren(%numSkus);
    if (isObject(infoText)) {
        if ((0.0 != %numSkus)) {
            if ((getCurrentTab() SPC name $= "BODY")) {
                if (!(BodyItemsFrame SPC features $= "Height")) {
                }
            }
        }
        if (!(BodyItemsFrame SPC features $= "Stance")) {
            infoText.setVisible(0);
            scroll.setVisible(1);
        }
        scroll.setVisible(0);
        infoText.setVisible(1);
        infoText.setText("no matching items");
    }
    if (!(isObject())) {
        new StringMap(ClosetCurrentCamParams);
        if (isObject()) {
            add();
        }
    }
    adjustForHeight();
    if ((ClosetCurrentCamParams SPC $ClosetOutfitName $= "")) {
        warn("wardrobe", ClosetCurrentCamParams @ getScopeName() @ ": $ClosetOutfitName is empty");
        return MissionCleanup;
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
        %skuItem = %skunum.findBySku();
        SkuManager;
        descCtrl.setText(descShrt);
        if ((%skuItem SPC brand $= "roca")) {
            logo.setBitmap("platform/client/ui/roca_logo_small");
        }
        if ((%skuItem SPC brand $= "myet")) {
            logo.setBitmap("platform/client/ui/myet_logo_small");
        }
        if ((%skuItem SPC brand $= "pcd")) {
            logo.setBitmap("platform/client/ui/pcd_logo_small");
        }
        if ((%skuItem SPC brand $= "staff")) {
            logo.setBitmap("platform/client/ui/staff_logo_small");
        }
        if ((%skuItem SPC brand $= "new")) {
            logo.setBitmap("platform/client/ui/new_logo_small");
        }
        logo.setBitmap("");
        if (%skuItem.hasTag("new")) {
            logo.setBitmap("platform/client/ui/new_logo_small");
        }
        if (!(%skuItem SPC expireTime $= "")) {
            expiringIcon.setBitmap("platform/client/ui/expiring_icon");
            expiringIcon.setVisible(1);
            %expiringItemsCount = (1.0 + %expiringItemsCount);
            %cell;
        }
        expiringIcon.setVisible(0);
        if ((%cell SPC %currentTabName $= "MY DESIGNS")) {
            ugcStatusIcon.setBitmap(ClosetGui_MyShop_GetSkuUGCStatusIcon(%skunum));
            ugcStatusIcon.setVisible(1);
        }
        ugcStatusIcon.setVisible(0);
        rarityBitmap.setBitmap(%this.getRarityBitmap(qty));
        frameButton.setActive(skuType.isWearableSkuType());
        if ((tab SPC name $= "SHOPS")) {
            %vpointsSym = "platform/client/ui/vpoints_9";
            %this;
            %vbuxSym = "platform/client/ui/vbux_9";
            %skuItem;
            %vpointsPrice = Inventory::getVPointsPriceForSku(sku);
            %cell;
            %vbuxPrice = Inventory::getVBuxPriceForSku(sku);
            %cell;
            if ((0.0 == %vpointsPrice)) {
            }
            if ((0.0 == %vbuxPrice)) {
                vpointsCtrl.setText("<just:right>free!");
                vbuxCtrl.setText("");
            }
            vpointsCtrl.setText("");
            vbuxCtrl.setText("");
            if ((0.0 > %vpointsPrice)) {
                vpointsCtrl.setText(%cell @ %cell @ %cell @ "<bitmap:" @ %vpointsSym @ "> " @ %vpointsPrice);
            }
            if ((0.0 > %vbuxPrice)) {
                vbuxCtrl.setText(%cell @ %cell @ %cell @ "<bitmap:" @ %vbuxSym @ "> " @ %vbuxPrice);
            }
            totalButton.setVisible(1);
            inStockText.setText(%this.GetInStockText(%cell[%cell @ sku]));
            if ((%cell >= findWord($Player::inventory, sku))) {
                %this.SetCellAvailability(%cell, 0, 1, "<just:right><color:00bb00>0wn3d!", "platform/client/ui/owned");
            }
            if (($gStoreItemsQty == %cell[%cell @ sku])) {
                %this.SetCellAvailability(%cell, 0, 0, "<just:right><color:dd0000>Sold Out!", "");
            }
            if ((%skuItem > rspk)) {
                %this.SetCellAvailability(%cell, 0, 0, "<just:right><color:bb0000>More Levels!", "platform/client/ui/cantbuy2");
            }
            if ((%skuItem > rspk)) {
                %this.SetCellAvailability(%cell, 0, 0, "<just:right><color:dd0000>Next Level!", "platform/client/ui/cantbuy");
            }
            %this.SetCellAvailability(%cell, 1, 1, "", "");
        }
        vpointsCtrl.setText("");
        vbuxCtrl.setText("");
        available = %cell @ 0 @ %cell;
        %cell;
        if (!(%skuItem $= drwrName)) {
            %currentDrwrName = drwrName;
            %skuItem;
            %dkBackground = !(%dkBackground);
            respektScoreToLevel($gMyRespektPoints) SPC %currentDrwrName;
        }
        if (%dkBackground) {
            // unhandled opcode 13445 at 0x00003482
        }
        background.setProfile();
        %n = (1.0 + %n);
        ClosetLtBackgroundProfile;
    }
    if (((%numSkus < %n) SPC %currentTabName $= "SHOPS")) {
    }
    if ((0.0 > %expiringItemsCount)) {
        1.setVisible();
    }
    %this.setSelectedThumbs();
    %this.getParent().scrollTo(0, (getWord(%startingPos, 1) - 1.0));
};
function ClosetThumbnails::SetCellAvailability(%this, %cell, %showPrice, %canTryOn, %subText, %overlayBitmapName) {
    if (!(%overlayBitmapName $= "")) {
        buyStatus.setBitmap(%overlayBitmapName);
        buyStatus.setVisible(1);
    }
    buyStatus.setVisible(0);
    if (!(%cell SPC %subText $= "")) {
        availabilityText.setVisible(1);
        availabilityText.setText(%subText);
        available = %cell @ 0 @ %cell;
        %cell;
    }
    availabilityText.setVisible(0);
    available = %cell @ 1 @ %cell;
    %cell;
    priceFader.setVisible(!(%showPrice));
    if (%canTryOn) {
    }
    frameButton.setActive(skuType.isWearableSkuType());
    frameFader.setVisible(!(%canTryOn));
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
    %skuItem = %skunum.findBySku();
    SkuManager;
    %thumb = objectView;
    %cell;
    %badge = badgeView;
    %cell;
    sku = %skunum @ %cell;
    SkuItem = %skuItem @ %cell;
    if ((%skuItem SPC skuType $= "mesh")) {
        %thumb.setVisible(1);
        %badge.setVisible(0);
        %params = strlwr(drwrName).get();
        %skuItem;
        %dist = getWord(%params, 3);
        ClosetCurrentCamParams;
        %fov = getWord(%params, 4);
        %lookAtNudge = getWords(%params, 0, 2);
        %pskus = %bodyAndOutfitSkus.overlaySkus(%skunum);
        SkuManager;
        layerSku = %skunum @ %thumb;
        consumeMouseWheel = 0 @ %thumb;
        %thumb.setSkus(%pskus);
        %thumb.makeSlaveOf();
        %thumb.setSimObject($player);
        %thumb.setLightDirection("0 3 -2");
        %thumb.setLookAtNudge(%lookAtNudge);
        %thumb.setOrbitDist(%dist);
        %thumb.setFOV(%fov);
    }
    if ((%skuItem SPC skuType $= "badge")) {
        %thumb.setVisible(0);
        %badge.setVisible(1);
        %bitmapName = %skuItem.getBitmapPath();
        ClosetMainObjectView;
        %badge.setBitmap(%bitmapName);
    }
    if ((%skuItem SPC skuType $= "token")) {
        %thumb.setVisible(0);
        %badge.setVisible(1);
        %bitmapName = %skuItem.getBitmapPath();
        %badge.setBitmap(%bitmapName);
    }
    if ((%skuItem SPC skuType $= "swatch")) {
        error("swatch in the closet!" @ " " @ %skunum);
    }
};
function ClosetCurrentCamParams::adjustForHeight(%this) {
    %allDrawers = allClosetDrawers();
    SkuManager;
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
        0.setVisible();
    }
    %drawer = drwrName;
    %sku.findBySku();
    1.setVisible();
    %params = %drawer.get();
    ClosetCurrentCamParams;
    %this.setCamParams(%params);
};
function GuiObjectView::setCamParams(%this, %params) {
    if ((%params $= "")) {
        return;
    }
    %dist = getWord(%params, 3);
    %fov = getWord(%params, 4);
    %lookAtNudge = getWords(%params, 0, 2);
    if (!(%this SPC fovFac $= "")) {
        %fov = (fovFac * %fov);
        %this;
    }
    %this.setLightDirection("0 3 -2");
    %this.setLookAtNudge(%lookAtNudge);
    %this.setOrbitDist(%dist);
    %this.setFOV(%fov);
};
function ClosetThumbnails::SetSelected(%this, %cell, %selected) {
    selected = %selected @ %cell;
    %buttonsDir = "platform/client/buttons/";
    %selString = selected ? "_sel" : "";
    %cell;
    %hiString = hilited ? "_hi" : "";
    %cell;
    frameButton.setBitmap(%cell @ %buttonsDir @ "frame" @ %selString @ %hiString);
};
function ClosetThumbnails::setSelectedThumbs(%this) {
    if ((getCurrentTab() SPC name $= "SHOPS")) {
        %selectedSkus = $StoreSkusLayer;
        ClosetTabs;
    }
    %selectedSkus = $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] @ " " @ $ClosetSkusBody;
    %numThumbs = %this.getCount();
    %n = 0;
    if ((%numThumbs < %n)) {
        %cell = %this.getObject(%n);
        %selected = (%cell >= findWord(%selectedSkus, sku));
        0.0;
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
    mouseOver = %this @ frameButton;
    1;
    scroll.scrollToCell(%this);
    %si = sku.findBySku();
    %this;
    %descShort = sku.getShortSkuDesc();
    %this;
    %descLong = sku.getLongSkuDesc();
    %this;
    if ($DevPref::Closet::skuDeets) {
        %descLong = ClosetTabs @ %descLong @ "<font:Courier New:14>";
        ClosetTabs;
        %descLong = %si @ skuNumber;
        %descLong @ "\n" @ "sku    = " @ " ";
        %descLong = %si @ skuType;
        %descLong @ "\n" @ "type   = " @ " ";
        %descLong = %si @ meshName;
        %descLong @ "\n" @ "mesh   = " @ " ";
        %descLong = %descLong @ "\n" @ "txtrs  = " @ " " @ %si.getTxtrNames();
        SkuManager;
        %descLong = %si @ roles::getRoleStrings(rolesMask);
        %descLong @ "\n" @ "roles  = " @ " ";
        %descLong = %si @ born;
        %descLong @ "\n" @ "bornW/ = " @ " ";
        %descLong = %si @ rspk;
        %descLong @ "\n" @ "rspkt  = " @ " ";
        %descLong = %si @ skuNumber.getSkuTags();
        SkuManager;
    }
    if (isObject()) {
        %descShort.setText();
        %descLong.setText();
    }
    if (isObject()) {
        %descShort.setText();
        %descLong.setText();
    }
    if (isObject()) {
        %descShort.setText();
        %descLong.setText();
    }
    sku.updateAuthorWidget();
    if (available) {
    }
    if ((%this == thumbnails)) {
        if (!(getId() SPC $gClosetThumbnailStoreHighlightTimer $= "")) {
            cancel($gClosetThumbnailStoreHighlightTimer);
        }
        $gClosetThumbnailStoreHighlightTimer = %this.schedule($gClosetThumbnailStoreHighlightDelayMS, "onHiliteStore");
        ClosetThumbnailsShop;
    }
};
function ClosetThumbnailCtrl::onHiliteStore(%this) {
    if (!($gClosetThumbnailStoreHighlightTimer $= "")) {
        cancel($gClosetThumbnailStoreHighlightTimer);
        $gClosetThumbnailStoreHighlightTimer = "";
    }
    hilited = 1 @ %this;
    %buttonsDir = "platform/client/buttons/";
    %frameBitmap = selected ? "frame_sel" : "frame";
    %this;
    %cartBitmap = sku.containsSku() ? "removeFromCart" : "add2cart";
    %this;
    frameButton.setBitmap(StoreShoppingList @ %this @ %buttonsDir @ %frameBitmap @ "_hi");
    toggleCartButton.setVisible(1);
    toggleCartButton.setBitmap(%this @ %buttonsDir @ %cartBitmap);
    buyNowButton.setVisible(1);
    buttonBacking.setVisible(1);
    if (isObject()) {
        1.setVisible();
    }
    if (isObject()) {
        1.setVisible();
        %screenPos = getScreenPosition();
        StoreFloatingHiliteFrame;
        %pos = getPosition();
        StoreFloatingHiliteFrame;
        %offsetX = (getWord(%pos, 0) - getWord(%screenPos, 0));
        StoreFloatingHiliteFrame;
        %offsetY = (getWord(%pos, 1) - getWord(%screenPos, 1));
        StoreFloatingHiliteFrame;
        %newPosX = (9.0 - (%offsetX - getWord(%this.getScreenPosition(), 0)));
        StoreItemDescHiliteFrame;
        %newPosY = (4.0 - (%offsetY - getWord(%this.getScreenPosition(), 1)));
        StoreItemDescHiliteFrame;
        %newPosX.reposition(%newPosY);
        %scroll = scroll;
        thumbnails;
        %padding = 10;
        %this;
        %minx = (%padding - getWord(%scroll.getScreenPosition(), 0));
        StoreFloatingHiliteFrame;
        %minY = (%padding - getWord(%scroll.getScreenPosition(), 1));
        %this;
        %maxX = ((%padding * 2.0) + (getWord(%scroll.getExtent(), 0) + %minx));
        %this;
        %maxy = ((%padding * 2.0) + (getWord(%scroll.getExtent(), 1) + %minY));
        %this;
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
        (%maxy <= (%height + %posY)).setVisible();
    }
};
function ClosetThumbnailCtrl::onUnhilite(%this) {
    mouseOver = %this @ frameButton;
    0;
    if (0) {
        if (isObject()) {
            "".setText();
            "".setText();
        }
        if (isObject()) {
            "".setText();
            "".setText();
        }
        if (isObject()) {
            showBaseDesc();
            showBaseDesc();
        }
        "".updateAuthorWidget();
    }
    if (tabShopsInitialized) {
    }
    if ((%this == thumbnails)) {
        hilited = getId() @ 0 @ %this;
        ClosetThumbnailsShop;
        %buttonsDir = "platform/client/buttons/";
        ClosetTabs;
        %frameBitmap = selected ? "frame_sel" : "frame";
        %this;
        %cartBitmap = sku.containsSku() ? "removeFromCart" : "add2cart";
        %this;
        frameButton.setBitmap(%this @ %buttonsDir @ %frameBitmap);
        toggleCartButton.setVisible(0);
        toggleCartButton.setBitmap(%this @ %buttonsDir @ %cartBitmap);
        buyNowButton.setVisible(0);
        buttonBacking.setVisible(0);
        if (isObject()) {
            0.setVisible();
        }
        if (isObject()) {
            0.setVisible();
        }
    }
};
function ClosetThumbnailCtrl::onSelect(%this) {
    frameButton.performClick();
};
function ClosetThumbnailCtrl::onMouseLeaveBounds(%this) {
    %this.onUnhilite();
};
function ClosetThumbnailCtrl::addToCart(%this) {
    sku.addSku();
};
function ClosetThumbnailCtrl::toggleInCart(%this) {
    if (sku.containsSku()) {
        sku.removeSku();
    }
    sku.addSku();
};
function ClosetThumbnailCtrl::buyNow(%this) {
    if (sku) {
        sku.purchaseSkus();
    }
};
function ClosetFrameButton::onMouseEnter(%this) {
    %thumbnails = thumbnails;
    %this;
    %i = thumbnails.getObjectIndex(ctrl);
    %this;
    %row = mFloor((numRowsOrCols / %i));
    %thumbnails;
    %col = (numRowsOrCols % %i);
    %thumbnails;
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
    oldHeight = $UserPref::Player::height @ %this;
    oldStance = $UserPref::Player::Genre @ %this;
    currentOverrideGenre = $player.getGenre() @ %this;
    wasInHelpmode = $player.isInHelpMeMode() @ %this;
    oldAnimation = $player.getCurrActionName() @ %this;
    %this.updateLocation();
    push();
    DestroyMessageBoxes();
    $ClosetOutfitName = closetMap @ $player.getGender() @ $gOutfits.get("currentOutfit");
    GuiTracker;
    $ClosetSkusBody = $gOutfits.get($player.getGender() @ "Body");
    %outfitNames = ;
    %i = 0;
    if (($gClosetNumOutfits < %i)) {
        %name = getWord(%outfitNames, %i);
        %name[$ClosetSkusOutfit @ %name] = $gOutfits.get(%name);
        %i = (1.0 + %i);
    }
    checkOutfitCorruption(1);
    setup();
    if ((1.0 == $player.isSitting())) {
        if ((0.0 == $IN_ORBIT_CAM)) {
            togglePlayerCamMode();
        }
        if (($player == isKissSeat)) {
            commandToServer('RequestToStand', 0, 0);
        }
    }
    if ((1.0 == $IN_ORBIT_CAM)) {
        togglePlayerCamMode();
    }
    $player.setSimObject();
    %this.setContent();
    %this.setVisible(1);
    setIdle(1, $ClosetGuiOpenMessage);
    getUserActivityMgr().setActivityActive("dressing", 1);
    updateVisibleAvatar();
    "".zoomToSKU();
    $player.rolesPermissionCheckNoWarn("debugPassive").setVisible();
    if ((ClosetStaffPanelContainer SPC $UserPref::Player::Genre $= "h")) {
    }
    if ((ClosetMainObjectView SPC $UserPref::Player::Genre $= "i")) {
    }
    if ((ClosetGui SPC $UserPref::Player::Genre $= "p")) {
    }
    if ((Canvas SPC $UserPref::Player::Genre $= "t")) {
    }
    if ((ClosetMainObjectView SPC $UserPref::Player::Genre $= "s")) {
        $UserPref::Player::Genre.selectGenre();
    }
    pushScreenSize(960, 544, 0, 1, 1);
    %closetGuiFUEIsObject = isObject();
    ClosetGuiFUE;
    if (isInFUE()) {
        if (!(%closetGuiFUEIsObject)) {
            "./closetGuiFUE.gui".execHideAndAddChild("");
        }
        open();
    }
    if (%closetGuiFUEIsObject) {
    }
    if (isVisible()) {
        close();
    }
    if (!($Player::hasSeenTakeAvatarPhotoDialog)) {
        if (!(tabSnapshotInitialized)) {
            fillProfileTab();
        }
        "".update();
    }
    if (isObject()) {
        resetLight();
    }
};
function ClosetGui::close(%this, %cancel) {
    %this.doClose(%cancel, 1);
};
function ClosetGui::doClose(%this, %cancel, %allowMsgBoxOnExit) {
    if (%this.isWaitingForPurchaseCompletion()) {
        return 0;
    }
    if ((getCurrentTab() SPC name $= "MY DESIGNS")) {
    }
    if (isVisible()) {
        close();
        return 0;
    }
    if ((getCurrentTab() SPC name $= "Shops")) {
        %i = (1.0 - getWordCount($StoreSkusLayer));
        ClosetTabs;
        if ((0.0 >= %i)) {
            %aTriedOnSku = getWord($StoreSkusLayer, %i);
            %skuIndex = findWord($Player::inventory, %aTriedOnSku);
            if (( >= 0.0)) {
                if (%aTriedOnSku.isBodySku()) {
                }
                if ((0.0 < findWord($ClosetSkusBody, %aTriedOnSku))) {
                    $ClosetSkusBody = $ClosetSkusBody.overlaySkus(%aTriedOnSku);
                    SkuManager;
                }
                if (%aTriedOnSku.isOutfitSku()) {
                }
                if ((0.0 < findWord($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], %aTriedOnSku))) {
                    $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = SkuManager @ SkuManager @ $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].overlaySkus(%aTriedOnSku);
                    SkuManager;
                }
            }
            %i = (1.0 - %i);
        }
        saveStorePosition();
    }
    if (%allowMsgBoxOnExit) {
    }
    if ((getCurrentTab() SPC name $= "SHOPS")) {
    }
    if ((StoreShoppingList > getCount())) {
    }
    if (!(0.0 SPC Inventory::getCurrentStoreName() $= "")) {
        MessageBoxYesNo("Leave the Store?", "You still have items in your shopping cart that you haven't bought." @ "\n" @ "<spush><b>Do you really want to return to the world?<spop>" @ "\n" @ "(The items will stay in your cart.)", (0.0 >= %i) @ ClosetTabs @ "ClosetGui.reallyClose(" @ %cancel @ ");", "");
        return 0;
    }
    if (%this.askUserToDropProp()) {
        return 0;
    }
    if (%allowMsgBoxOnExit) {
    }
    if (!(%cancel)) {
    }
    if (!($Player::Name.getProperty("hasTakenAvatarPhoto", 0))) {
    }
    if (!($Player::hasSeenTakeAvatarPhotoDialog)) {
    }
    if (!($StandAlone)) {
        $Player::hasSeenTakeAvatarPhotoDialog = 1;
        gUserPropMgrClient;
        MessageBoxYesNo($Player::hasSeenTakeAvatarPhotoDialog[$MsgCat::closet @ "MSG-NO-AVATAR-PHOTO-TITLE"], , "ClosetTabs.selectTabWithName(\"SNAPSHOT\");", "ClosetGui.reallyClose(" @ %cancel @ ");");
        return 0;
    }
    %this.reallyClose(%cancel);
};
function ClosetGui::reallyClose(%this, %cancel) {
    stopPropAction();
    pop();
    if (isObject()) {
        0.setVisible();
    }
    %this.doResetGenre();
    if ((StoreSpecificBackground SPC %cancel $= "")) {
        %cancel = 0;
        StoreSpecificBackground;
    }
    if (%cancel) {
        %this.doCancel();
    }
    %this.doOkay();
    if (inTransit) {
        previouslyOpened.setContent();
    }
    setContent();
    %this.setVisible(0);
    nextPlayerCamMode();
    setIdle(0);
    if ((0.0 != $gSalonChairCurrent)) {
        if (!(%this SPC oldAnimation $= "")) {
            $player.playAnim(oldAnimation);
        }
    }
    $player.playAnim(PlayGui @ %this @ $player.getGender() @ $player.getGenre() @ "idl1a");
    oldAnimation = Canvas @ "" @ %this;
    GuiTracker;
    getUserActivityMgr().setActivityActive("dressing", 0);
    popScreenSize();
    if (!($Player::Name.getProperty("hasSeenPropUseAdvisory", 0))) {
    }
    if ($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].hasPropSku()) {
        $Player::Name.setProperty("hasSeenPropUseAdvisory", 1);
        MessageBoxOK(gUserPropMgrClient, SkuManager, "");
    }
    Inventory::fetchPlayerInventoryIfEmpty();
};
function ClosetGui::doResetCurrent(%this) {
    checkOutfitCorruption(1);
    $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = $gOutfits.get($ClosetOutfitName);
    %this.updateVisibleAvatar();
    "".zoomToSKU();
    update();
};
function ClosetGui::doResetGenre(%this) {
    $UserPref::Player::Genre = oldStance;
    %this;
    if (!(%this SPC currentOverrideGenre $= $UserPref::Player::Genre)) {
        $player.setGenre(currentOverrideGenre);
    }
    $player.setGenre($UserPref::Player::Genre);
};
function ClosetGui::doResetAll(%this) {
    $UserPref::Player::height = oldHeight;
    %this;
    $ClosetOutfitName = $player.getGender() @ $gOutfits.get("currentOutfit");
    %outfitNames = ;
    %i = 0;
    if (($gClosetNumOutfits < %i)) {
        %name = getWord(%outfitNames, %i);
        %name[$ClosetSkusOutfit @ %name] = $gOutfits.get(%name);
        %i = (1.0 + %i);
    }
    $ClosetSkusBody = $gOutfits.get(($gClosetNumOutfits < %i) @ $player.getGender() @ "Body");
    %this.updateVisibleAvatar();
    "".zoomToSKU();
    updateBodyTabDisplay();
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
    if (wasInHelpmode) {
        %activeSkus = %activeSkus @ " " @ getSpecialSKU(0, "helpmebadge");
        %this;
    }
    commandToServer('SetActiveSkus', %activeSkus);
    commandToServer('setHeight', $UserPref::Player::height);
    $Player::Name.setProperty("avatarHeight", $UserPref::Player::height);
    %playerActiveSKUs = $player.getActiveSKUs();
    gUserPropMgrClient;
    if (wasInHelpmode) {
        %playerActiveSKUs = %playerActiveSKUs @ " " @ getSpecialSKU(0, "helpmebadge");
        %this;
    }
    $player.schedule(0, "setActiveSkus", %playerActiveSKUs);
    if (isObject()) {
        1.setValue();
        onAction();
    }
};
function ClosetGui::askUserToDropProp(%this) {
    %propSku = $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].filterSkusDrwr("props");
    SkuManager;
    if ((%propSku $= "")) {
        return 0;
    }
    if (canHavePropsInGenre(currentOverrideGenre)) {
        return 0;
    }
    %index = findWord($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], %propSku);
    %outfitWithoutProp = removeWord($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], %index);
    %activity = "engaging in this activity";
    if ((%this SPC currentOverrideGenre $= "k")) {
        %activity = "skating";
    }
    if ((%this SPC currentOverrideGenre $= "w")) {
        %activity = "swimming";
    }
    if ((%this SPC currentOverrideGenre $= "o")) {
        %activity = "sumo wrestling";
    }
    if ((%this SPC currentOverrideGenre $= "l")) {
        %activity = "pillow fighting";
    }
    if ((%this SPC currentOverrideGenre $= "s")) {
        %activity = "strutting your stuff";
    }
    %body = %activity[$MsgCat::closet @ "MSG-NO-PROP-IN-THIS-GENRE-BODY1"] @ " " @ %activity @ %activity[$MsgCat::closet @ "MSG-NO-PROP-IN-THIS-GENRE-BODY2"];
    MessageBoxYesNo(%body[$MsgCat::closet @ "MSG-NO-PROP-IN-THIS-GENRE-TITLE"], %body, "$ClosetSkusOutfit[$ClosetOutfitName] = \"" @ %outfitWithoutProp @ "\"; ClosetGui.reallyClose(false);", "");
    return 1;
};
function ClosetGui::userHasChangedBodyOrOutfit(%this) {
    if (!( $= $gOutfits.get($ClosetSkusBody @ $player.getGender() @ "Body"))) {
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
        callback = %mbCBPoints @ %buttonIndex @ %dialog;
        profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
        0;
        horizSizing = %buttonIndex @ %dialog @ "right";
        vertSizing = "bottom";
        position = "7 2";
        extent = "7 13";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/vpoints_9";
        button.add();
        %buttonIndex = (1.0 + %buttonIndex);
    }
    if (!(%mbCBBux $= "")) {
        callback = %mbCBBux @ %buttonIndex @ %dialog;
        profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
        0;
        horizSizing = %buttonIndex @ %dialog @ "right";
        vertSizing = "bottom";
        position = "7 2";
        extent = "7 13";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/vbux_9";
        button.add();
        %buttonIndex = (1.0 + %buttonIndex);
    }
    callback = %cbCancel @ %buttonIndex @ %dialog;
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
        %si = %skus.findBySku();
        SkuManager;
        if (!(isObject(%si))) {
            return;
        }
        %desc = descShrt;
        %si;
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
        %si = %skus.findBySku();
        SkuManager;
        if (!(isObject(%si))) {
            return;
        }
        %desc = descShrt;
        %si;
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
    %this.purchaseSkus(getSkus());
};
function ClosetGui::purchaseSkusReally(%this, %skus, %currency) {
    %array = new ""();
    Array;
    %n = (1.0 - getWordCount(%skus));
    0;
    if ((0.0 >= %n)) {
        %sku = getWord(%skus, %n);
        %array.push_front(%sku, 1);
        %n = (1.0 - %n);
    }
    %request = sendRequest_PurchaseInventory($Player::Name, %array, %currency, $gCurrentStoreName, "closet_onDoneOrErrorCallback_PurchaseInventory");
    (0.0 >= %n);
    currency = %currency @ %request;
    callbackData = %this @ %request;
    timedOutAlready = 0 @ %request;
    %array.delete();
    waitIcon.setVisible(1);
    waitIcon.start();
    numberOfPurchasesAwaitingCompletion = (%this + numberOfPurchasesAwaitingCompletion);
    1.0;
    %tab = "SHOPS".getTabWithName();
    ClosetTabs;
    doneButton.setActive(!(%this.isWaitingForPurchaseCompletion()));
    cancelButton.setActive(!(%this.isWaitingForPurchaseCompletion()));
    checkoutPopup = StoreShoppingBag @ MessageBoxOK(%tab, %tab, "") @ %this;
    StoreShoppingBag;
    %this.schedule(30000, %request);
};
function ClosetGui::purchaseSkusRequestTimedOut(%this, %request) {
    if (!(isObject(%request))) {
    }
    if ((%request.statusCode() $= "")) {
        return;
    }
    timedOutAlready = 1 @ %request;
    numberOfPurchasesPastTimeout = (%this + numberOfPurchasesPastTimeout);
    1.0;
    %tab = "SHOPS".getTabWithName();
    ClosetTabs;
    doneButton.setActive(!(%this.isWaitingForPurchaseCompletion()));
    cancelButton.setActive(!(%this.isWaitingForPurchaseCompletion()));
    processingTimeoutPopup = MessageBoxOK(%tab, %tab, "") @ %this;
};
function ClosetGui::isWaitingForPurchaseCompletion(%this) {
    return (%this > numberOfPurchasesAwaitingCompletion);
};
function closet_onDoneOrErrorCallback_PurchaseInventory(%request) {
    callbackData.onDoneOrErrorCallback_PurchaseInventory(%request);
};
function ClosetGui::onDoneOrErrorCallback_PurchaseInventory(%this, %request) {
    waitIcon.stop();
    waitIcon.setVisible(0);
    numberOfPurchasesAwaitingCompletion = (%this - numberOfPurchasesAwaitingCompletion);
    1.0;
    if (timedOutAlready) {
        numberOfPurchasesPastTimeout = (%this - numberOfPurchasesPastTimeout);
        1.0;
    }
    %tab = "SHOPS".getTabWithName();
    ClosetTabs;
    doneButton.setActive(!(%this.isWaitingForPurchaseCompletion()));
    cancelButton.setActive(!(%this.isWaitingForPurchaseCompletion()));
    if (isObject(checkoutPopup)) {
        checkoutPopup.close();
    }
    if (isObject(processingTimeoutPopup)) {
        processingTimeoutPopup.close();
    }
    if (!(isObject(%request))) {
        error(getScopeName() @ " " @ "- no request! this may be because we didn't send it in alpha 1");
        return %this;
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
            shoppingCartSkus = StoreShoppingList @ getSkus() @ %request;
            clear();
        }
        if ((StoreShoppingList SPC %errorCode $= "insufficientTotalFunds")) {
            %msgName = (%request SPC currency $= "vpoints") ? "E-NO-VPOINTS" : "E-NO-VBUX";
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
    %this.handleAnyPurchasedSkus(, timedOutAlready);
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
    if (!((0.0 >= %n) SPC %skulist $= "")) {
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
        %skusToFlattenClothing = %skusToFlatten.filterSkusForClothing();
        SkuManager;
        %skusToFlattenBody = %skusToFlatten.filterSkusForBody();
        SkuManager;
        $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = SkuManager @ $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].overlaySkus(%skusToFlattenClothing);
        $ClosetSkusBody = $ClosetSkusBody.overlaySkus(%skusToFlattenBody);
        SkuManager;
    }
    update();
};
function CheckoutRequest::onClosed(%this) {
};
function CheckoutRequest::onError(%this, %unused, %unused) {
    waitIcon.stop();
    waitIcon.setVisible(0);
    if (isObject(checkoutPopup)) {
        checkoutPopup.close();
    }
    MessageBoxOK("Connection Error", ClosetGui, "");
    %this.onClosed();
};
function CheckoutRequest::onDone(%this) {
    log("network", "debug", getScopeName() @ " " @ "- url =" @ " " @ %this.getURL());
    waitIcon.stop();
    waitIcon.setVisible(0);
    if (isObject(checkoutPopup)) {
        checkoutPopup.close();
    }
    %status = findRequestStatus(%this);
    ClosetGui;
    %ownsAlready = 0;
    ClosetGui;
    %buyFailedInsufVBux = 0;
    StoreShoppingBag;
    %buyFailedInsufVPoints = 0;
    StoreShoppingBag;
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
            %skusToFlattenClothing = %skusToFlatten.filterSkusForClothing();
            SkuManager;
            %skusToFlattenBody = %skusToFlatten.filterSkusForBody();
            SkuManager;
            $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = SkuManager @ $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].overlaySkus(%skusToFlattenClothing);
            $ClosetSkusBody = $ClosetSkusBody.overlaySkus(%skusToFlattenBody);
            SkuManager;
        }
        update();
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
    if ((getCurrentTab() SPC name $= "SHOPS")) {
        %merged = $ClosetSkusBody @ " " @ $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].overlaySkus($StoreSkusLayer);
        SkuManager;
    }
    if ((getCurrentTab() SPC name $= "CLOSET")) {
        $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].refresh();
    }
    if ((getCurrentTab() SPC name $= "MY DESIGNS")) {
        %merged = $ClosetSkusBody @ " " @ $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].overlaySkus($gSkusMyShopLayer);
        SkuManager;
        $gSkusMyShopLayer.refresh();
    }
    %merged.setSkus();
    %snapTab = "SNAPSHOT".getTabWithName();
    ClosetTabs;
    if (%snapTab) {
    }
    if (isObject(objView)) {
        objView.setSkus(%merged);
    }
    %badge = %merged.filterSkusDrwr("badges");
    SkuManager;
    %si = %badge.findBySku();
    SkuManager;
    %bitmap = "";
    %snapTab;
    if (isObject(%si)) {
        %bitmap = %si.getBitmapPath();
        %snapTab;
    }
    %bitmap.setBitmap();
    if (isObject()) {
        updateSkus();
    }
};
function ClosetGui::toggleSku(%this, %sku) {
    if ((getCurrentTab() SPC name $= "SHOPS")) {
        ClosetGUI_ToggleSku_Shops(%sku);
    }
    if ((getCurrentTab() SPC name $= "CLOSET")) {
        ClosetGUI_ToggleSku_Closet(%sku);
    }
    if ((getCurrentTab() SPC name $= "BODY")) {
        ClosetGUI_ToggleSku_Body(%sku);
    }
    if ((getCurrentTab() SPC name $= "SNAPSHOT")) {
        ClosetGUI_ToggleSku_Snapshot(%sku);
    }
    if ((getCurrentTab() SPC name $= "MY DESIGNS")) {
        ClosetGUI_ToggleSku_MyShop(%sku);
    }
    error(getCurrentTab() @ name @ " " @ getTrace());
    return ClosetTabs;
    updateVisibleAvatar();
    %sku.zoomToSKU();
    %thumbnails = thumbnails;
    getCurrentTab();
    if (isObject(%thumbnails)) {
        %thumbnails.setSelectedThumbs();
        %count = %thumbnails.getCount();
        ClosetTabs;
        %i = 0;
        ClosetMainObjectView;
        if ((%count < %i)) {
            %cell = %thumbnails.getObject(%i);
            ClosetGui;
            %thumbnails.setCellSkus(%cell, sku);
            %i = (1.0 + %i);
            %cell;
        }
    }
};
function ClosetGui::doArrow(%this, %dx, %dy) {
    if ((getCurrentTab() SPC name $= "SNAPSHOT")) {
        %dx.moveBy(-(%dy));
    }
};
function ClosetLink::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    if ((getWord(%url, 0) $= "SAVE_OUTFIT")) {
        saveOrCancel();
    }
    if ((ClosetMyOutfitsFrame SPC getWord(%url, 0) $= "DONE")) {
        0.close();
    }
    if ((ClosetGui SPC getWord(%url, 0) $= "CANCEL")) {
        1.close();
    }
    if ((ClosetGui SPC getWord(%url, 0) $= "TOGGLE_SKU")) {
        getWord(%url, 1).toggleSku();
    }
};
function ClosetItemsScroll::getIndexForSku(%this, %sku) {
    %thumbnails = thumbnails;
    %this;
    %count = %thumbnails.getCount();
    %i = 0;
    if ((%count < %i)) {
        if ((%thumbnails.getObject(%i) == sku)) {
            return %i;
        }
        %i = (1.0 + %i);
    }
    return -(1.0);
};
function ClosetItemsScroll::scrollToSku(%this, %sku) {
    %thumbnails = thumbnails;
    %this;
    %idx = %this.getIndexForSku(%sku);
    if ((0.0 < %idx)) {
        if ((getCurrentTab() SPC name $= "CLOSET")) {
            brand = ClosetBrandPopup @ 0.getTextById() @ ClosetItemsFrame;
            ClosetTabs;
            category = ClosetItemPopup @ 0.getTextById() @ ClosetItemsFrame;
            update();
            0.SetSelected();
            0.SetSelected();
            %idx = %this.getIndexForSku(%sku);
            ClosetItemPopup;
        }
        if ((getCurrentTab() SPC name $= "SHOPS")) {
            if ((StoreCategoryPopup != GetSelected())) {
                0.SetSelected();
            }
            %idx = %this.getIndexForSku(%sku);
            StoreCategoryPopup;
        }
    }
    if ((0.0 < %idx)) {
        return 0.0;
    }
    %row = mFloor((numRowsOrCols / %idx));
    %thumbnails;
    %col = (numRowsOrCols % %idx);
    %thumbnails;
    %thumbnails.hiliteCell(%col, %row);
    %this.scrollToCellIndex(%idx);
    %sku.zoomToSKU();
};
function ClosetItemsScroll::scrollToCell(%this, %cell) {
    %thumbnails = thumbnails;
    %this;
    %cellIdx = %thumbnails.getObjectIndex(%cell);
    %this.scrollToCellIndex(%cellIdx);
};
function ClosetItemsScroll::scrollToCellIndex(%this, %cellIdx) {
    %thumbnails = thumbnails;
    %this;
    %cellHeight = (%thumbnails + getWord(childrenExtent, 1));
    spacing;
    %ypos = (getWord(%thumbnails.getPosition(), 1) - 1.0);
    %thumbnails;
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
    updateRangeText();
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
    if (( < findWord(%outfitNames, 0.0 @ %plyrGendr @ %currentOutfit))) {
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
            error((0.0 >= %n) @ getScopeName() @ "-> " @ %outfitsCorrupted @ " blank outfits detected!");
            %errMsg = %errMsg @ " " @ "(" @ %outfitsCorrupted @ " blank outfits)";
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
    if (isObject()) {
        1.setVisible();
        1.makeFirstResponder();
    }
    profile = ClosetFilterContainer @ new GuiControl(ClosetFilterContainer) @ "ETSNonModalProfile";
    ClosetFilterField;
    horizSizing = ClosetFilterContainer @ ClosetFilterContainer @ "right";
    vertSizing = "bottom";
    position = "335 64";
    extent = "148 40";
    profile = GuiTextCtrl @ new ""() @ "ClosetTitleProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "1 0";
    extent = "104 20";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Filter";
    maxLength = 255;
    profile = GuiWindowCtrl @ new ""() @ "DottedWindowProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 20";
    extent = "148 17";
    resizeWidth = 0;
    resizeHeight = 0;
    canMove = 0;
    canClose = 0;
    canMinimize = 0;
    canMaximize = 0;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    bitmap = "platform/client/ui/magnifying_glass";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "2 1";
    extent = "18 17";
    canHilite = 0;
    profile = new GuiTextEditCtrl(ClosetFilterField) @ "InfoWindowTextEditInvisibleOnWhiteProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "20 -1";
    extent = "120 18";
    maxLength = 20;
    timeoutMS = 800;
    command = "$ThisControl.OnTextChanged();";
    altCommand = "$ThisControl.OnEnterKey   ();";
    canHilite = 0;
    horizSizing = GuiBitmapButtonCtrl @ new ""() @ "right";
    vertSizing = "bottom";
    position = "135 4";
    extent = "10 10";
    command = "ClosetFilterField.setText(\"\"); ClosetFilterField.onEnterKey();";
    canHilite = 1;
    bitmap = "platform/client/buttons/closet_close";
    modulationColor = "255 255 255 40";
    1.makeFirstResponder();
};
$gClosetFilterFieldTimerID = "";
function ClosetFilterField::OnTextChanged(%this) {
    cancel($gClosetFilterFieldTimerID);
    $gClosetFilterFieldTimerID = %this.schedule(timeoutMS, "onTimer");
    %this;
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
    if ((%this $= prevFilterText)) {
        return %filterText;
    }
    prevFilterText = %filterText @ %this;
    %tab = getCurrentTab();
    ClosetTabs;
    if ((%tab SPC name $= "BODY")) {
        update();
    }
    if ((%tab SPC name $= "CLOSET")) {
        update();
    }
    if ((%tab SPC name $= "SHOPS")) {
        update();
    }
    if ((%tab SPC name $= "SNAPSHOT")) {
    }
    if ((%tab SPC name $= "MY DESIGNS")) {
        update();
    }
};
function ClosetTabs::createAuthorWidget(%this) {
    if (isObject()) {
        1.setVisible();
    }
    profile = ClosetAuthorContainer @ new GuiControl(ClosetAuthorContainer) @ "ETSNonModalProfile";
    ClosetAuthorContainer;
    horizSizing = ClosetAuthorContainer @ "right";
    vertSizing = "bottom";
    position = "689 84";
    extent = "245 110";
    horizSizing = new GuiBitmapCtrl(ClosetAuthorPicture) @ "right";
    vertSizing = "bottom";
    position = "177 42";
    extent = "66 66";
    bitmap = "platform/client/ui/vside_icon_38x38";
    modulationColor = "255 255 255 20";
    profile = new GuiWindowCtrl(ClosetAuthorPictureOutline) @ "NonModalDottedWindowProfile";
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
    profile = new GuiMLTextCtrl(ClosetAuthorText) @ "ClosetSmallLinkProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 86";
    extent = "176 22";
    lineSpacing = -2;
};
function ClosetTabs::updateAuthorWidget(%this, %sku) {
    if (!(isObject())) {
        return ClosetAuthorContainer;
    }
    if (!(%sku $= "")) {
    }
    %si = "";
    %sku.findBySku();
    %filled = 0;
    SkuManager;
    if (isObject(%si)) {
        if (!(%si SPC author $= "")) {
            %filled = 1;
            if ((%si SPC author $= "?")) {
                ClosetAuthorPicture @ "platform/client/ui/tgf/tgf_profile_default_" @ $player.getGender().setBitmap();
                modulationColor = "255 255 255 50" @ ClosetAuthorPicture;
                1.setVisible();
                ClosetAuthorPictureOutline @ ClosetAuthorText @ "<just:right><font:Arial:12><color:00000044><linkcolor:00000066>" @ "oh nos!<br>" @ "we've lost track of who made this!<br>".setText();
            }
            %playerEncoded = urlEncode(stripUnprintables(author));
            %si;
            %profileURL = $Net::ProfileURL @ %playerEncoded;
            %pictureURL_M = $Net::AvatarURL @ %playerEncoded @ "?size=M";
            %pictureURL_L = $Net::AvatarURL @ %playerEncoded @ "?size=L";
            "".setBitmap();
            %pictureURL_M.downloadAndApplyBitmap();
            %pictureURL_L.downloadAndApplyBitmap();
            modulationColor = ClosetAuthorPicture @ "255 255 255 255" @ ClosetAuthorPicture;
            ClosetAuthorPicture;
            1.setVisible();
            ClosetAuthorPicture @ ClosetAuthorPictureOutline @ ClosetAuthorText @ "<just:right><font:Arial:12><color:00000044><linkcolor:00000066>" @ "design by<br><a:" @ %profileURL @ ">" @ %si @ author @ "</a>".setText();
        }
        if (!(%si SPC brand $= "")) {
        }
        if (!(%si SPC brand $= "new")) {
        }
        if (!(%si SPC brand $= "vhdtemplate")) {
            %fullBrand = %si[%si @ brand];
            $gClosetBrandsExtrnl;
            if ((%fullBrand $= "")) {
                error(%si @ brand @ " " @ %sku @ " " @ getTrace());
            }
            %filled = 1;
            getScopeName() @ " " @ "- unknown brand" @ " ";
            "platform/client/ui/vside_icon_38x38".setBitmap();
            modulationColor = ClosetAuthorPicture @ "255 255 255 20" @ ClosetAuthorPicture;
            0.setVisible();
            ClosetAuthorPictureOutline @ ClosetAuthorText @ "<just:right><font:Arial:12><color:00000044><linkcolor:00000066>" @ "brand:<br>" @ %fullBrand.setText();
        }
    }
    if (!(%filled)) {
        "platform/client/ui/vside_icon_38x38".setBitmap();
        modulationColor = ClosetAuthorPicture @ "255 255 255 20" @ ClosetAuthorPicture;
        0.setVisible();
        "".setText();
    }
};
function ClosetTabs::createWhatYourWearingPanel(%this) {
    if (isObject()) {
    }
    profile = ClosetWhatYoureWearingPanel @ new GuiWindowCtrl(ClosetWhatYoureWearingPanel) @ "DottedWindowProfile";
    ClosetWhatYoureWearingPanel;
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
    profile = new GuiMLTextCtrl(ClosetWhatYoureWearingTitle) @ "ClosetTitleProfile";
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
    horizSizing = new GuiMLTextCtrl(ClosetWhatYoureWearingNone) @ "right";
    vertSizing = "bottom";
    position = "10 23";
    extent = "230 20";
    text = "";
    style = "faintOnWhite";
    lineSpacing = -(1.0);
    stripGamelink = 1;
    %whatYoureWearingPanel = ;
    profile = GuiScrollCtrl @ new ""() @ "ETSScrollProfile";
    0;
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
    %whatYoureWearingScroll = ;
    horizSizing = new GuiArray2Ctrl(ClosetWhatYoureWearingList) @ "width";
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
    %whatYoureWearingList = ;
    %whatYoureWearingScroll.add(%whatYoureWearingList);
    %whatYoureWearingPanel.add(%whatYoureWearingScroll);
    return %whatYoureWearingPanel;
};
function ClosetMainObjectView::onSystemDragDroppedEvent(%this, %text, %pt) {
    if ((getCurrentTab() SPC name $= "BODY")) {
        error(getCurrentTab() @ name @ " " @ getTrace());
    }
    if ((getCurrentTab() SPC name $= "CLOSET")) {
        error(getCurrentTab() @ name @ " " @ getTrace());
    }
    if ((getCurrentTab() SPC name $= "SHOPS")) {
        error(getCurrentTab() @ name @ " " @ getTrace());
    }
    if ((getCurrentTab() SPC name $= "MY DESIGNS")) {
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
