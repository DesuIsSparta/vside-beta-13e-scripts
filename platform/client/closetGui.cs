$ClosetGuiOpenMessage = "Changing Clothes";
$gSkusToHideInCloset = getSpecialSKU(0, "helpmebadge");
$gClosetStanceEmotesNum = 0;
$gClosetStanceEmotes[$gClosetStanceEmotesNum] = "cool";
$gClosetStanceEmotesNum = $gClosetStanceEmotesNum + 1;
$gClosetStanceEmotes[$gClosetStanceEmotesNum] = "cool";
$gClosetStanceEmotesNum = $gClosetStanceEmotesNum + 1;
$gClosetStanceEmotes[$gClosetStanceEmotesNum] = "cool";
$gClosetStanceEmotesNum = $gClosetStanceEmotesNum + 1;
$gClosetStanceEmotes[$gClosetStanceEmotesNum] = "cool";
$gClosetStanceEmotesNum = $gClosetStanceEmotesNum + 1;
$gClosetStanceEmotes[$gClosetStanceEmotesNum] = "cool";
$gClosetStanceEmotesNum = $gClosetStanceEmotesNum + 1;
$gClosetStanceEmotes[$gClosetStanceEmotesNum] = "wve";
$gClosetStanceEmotesNum = $gClosetStanceEmotesNum + 1;
$gClosetStanceEmotes[$gClosetStanceEmotesNum] = "wve";
$gClosetStanceEmotesNum = $gClosetStanceEmotesNum + 1;
$gClosetStanceEmotes[$gClosetStanceEmotesNum] = "wve";
$gClosetStanceEmotesNum = $gClosetStanceEmotesNum + 1;
$gClosetStanceEmotes[$gClosetStanceEmotesNum] = "flr";
$gClosetStanceEmotesNum = $gClosetStanceEmotesNum + 1;
$gClosetStanceEmotes[$gClosetStanceEmotesNum] = "flr";
$gClosetStanceEmotesNum = $gClosetStanceEmotesNum + 1;
$gClosetStanceEmotes[$gClosetStanceEmotesNum] = "ttth";
$gClosetStanceEmotesNum = $gClosetStanceEmotesNum + 1;
$gClosetStanceEmotesLast = "";
$gClosetNeutralHeightInches["f"] = (5 * 12) + 7;
$gClosetNeutralHeightInches["m"] = (5 * 12) + 7;
new StringMap(ThumbCategories);
if (isObject(MissionCleanup))
{
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
SkuManager.buildSkusSearchText();
new StringMap(ThumbCategoriesOrder);
if (isObject(MissionCleanup))
{
    MissionCleanup.add(ThumbCategoriesOrder);
}
%n = 0;
ThumbCategoriesOrder.put(%n, "tops");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "bottoms");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "hair");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "shoes");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "ear");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "neck");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "waist");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "hands");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "bags");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "props");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "misc");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "bodymod");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "glasses");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "face");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "eyes");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "skin");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "badges");
%n = %n + 1;
ThumbCategoriesOrder.put(%n, "tokens");
%n = %n + 1;
$tmpGender = "f";
$ThumbCamParams[$tmpGender,"fullbody"] = "0 0 0.0 1.7 35";
$ThumbCamParams[$tmpGender,"hair"] = "0.4 -0.3 0.8 1.0 20";
$ThumbCamParams[$tmpGender,"face"] = "0.4 -0.3 0.8 1.0 15";
$ThumbCamParams[$tmpGender,"faceb"] = $ThumbCamParams[$tmpGender,"face"] ;
$ThumbCamParams[$tmpGender,"eyes"] = "0.4 -0.3 0.8 1.0 10";
$ThumbCamParams[$tmpGender,"ear"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"earl"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"labret"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lftauricle"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lftconch"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lfteyebrow"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lftlobe"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lftorbital"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lftpinna"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lftrook"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lfttragus"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"rghauricle"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"rghconch"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"rgheyebrow"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"rghlobe"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"rghorbital"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"rghpinna"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"rghrook"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"rghtragus"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lowlip"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"madonna"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"medusa"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"nostril"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"septum"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"glasses"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"skin"] = $ThumbCamParams[$tmpGender,"face"] ;
$ThumbCamParams[$tmpGender,"torso"] = "0.4 -0.3 0.4 1.8 20";
$ThumbCamParams[$tmpGender,"torsob"] = $ThumbCamParams[$tmpGender,"torso"] ;
$ThumbCamParams[$tmpGender,"legs"] = "0 0 -0.4 1.7 35";
$ThumbCamParams[$tmpGender,"legsb"] = $ThumbCamParams[$tmpGender,"legs"] ;
$ThumbCamParams[$tmpGender,"feet"] = "0.1 -0.2 -0.85 1.2 20";
$ThumbCamParams[$tmpGender,"neck"] = "0.4 -0.4 0.65 1.0 11";
$ThumbCamParams[$tmpGender,"neckb"] = $ThumbCamParams[$tmpGender,"neck"] ;
$ThumbCamParams[$tmpGender,"neckc"] = $ThumbCamParams[$tmpGender,"neck"] ;
$ThumbCamParams[$tmpGender,"chest"] = $ThumbCamParams[$tmpGender,"neck"] ;
$ThumbCamParams[$tmpGender,"wristleft"] = "-0.2 -0.5 0.04 1.5 8";
$ThumbCamParams[$tmpGender,"wristleftb"] = $ThumbCamParams[$tmpGender,"wristleft"] ;
$ThumbCamParams[$tmpGender,"wristright"] = "1.1 -0.6 0.04 1.5 8";
$ThumbCamParams[$tmpGender,"wristrightb"] = $ThumbCamParams[$tmpGender,"wristright"] ;
$ThumbCamParams[$tmpGender,"fingerleft"] = $ThumbCamParams[$tmpGender,"wristleft"] ;
$ThumbCamParams[$tmpGender,"fingerright"] = $ThumbCamParams[$tmpGender,"wristright"] ;
$ThumbCamParams[$tmpGender,"toeleft"] = $ThumbCamParams[$tmpGender,"feet"] ;
$ThumbCamParams[$tmpGender,"toeright"] = $ThumbCamParams[$tmpGender,"feet"] ;
$ThumbCamParams[$tmpGender,"purse"] = $ThumbCamParams[$tmpGender,"torso"] ;
$ThumbCamParams[$tmpGender,"waist"] = "0.5 -0.4 0.1 1.0 18";
$ThumbCamParams[$tmpGender,"waistb"] = $ThumbCamParams[$tmpGender,"waist"] ;
$ThumbCamParams[$tmpGender,"mask"] = $ThumbCamParams[$tmpGender,"face"] ;
$ThumbCamParams[$tmpGender,"hat"] = $ThumbCamParams[$tmpGender,"hair"] ;
$ThumbCamParams[$tmpGender,"back"] = "0.4 -1.1 0.4 1.8 22";
$ThumbCamParams[$tmpGender,"tail"] = $ThumbCamParams[$tmpGender,"waist"] ;
$ThumbCamParams[$tmpGender,"props"] = "1.1 -0.4 0.04 1.5 18";
$ThumbCamParams[$tmpGender,"badges"] = "";
$ThumbCamParams[$tmpGender,"tokens"] = "";
$tmpGender = "m";
$ThumbCamParams[$tmpGender,"fullbody"] = "0 0 0.0 1.7 35";
$ThumbCamParams[$tmpGender,"hair"] = "0.0 0.0 0.9 1.0 15";
$ThumbCamParams[$tmpGender,"eyes"] = "0.0 0.0 0.9 1.0 6";
$ThumbCamParams[$tmpGender,"face"] = $ThumbCamParams[$tmpGender,"hair"] ;
$ThumbCamParams[$tmpGender,"faceb"] = $ThumbCamParams[$tmpGender,"hair"] ;
$ThumbCamParams[$tmpGender,"earl"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"labret"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lftauricle"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lftconch"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lfteyebrow"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lftlobe"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lftorbital"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lftpinna"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lftrook"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lfttragus"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"rghauricle"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"rghconch"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"rgheyebrow"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"rghlobe"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"rghorbital"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"rghpinna"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"rghrook"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"rghtragus"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"lowlip"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"madonna"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"medusa"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"nostril"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"septum"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"glasses"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"skin"] = $ThumbCamParams[$tmpGender,"hair"] ;
$ThumbCamParams[$tmpGender,"torso"] = "0 0 0.4 1.8 20";
$ThumbCamParams[$tmpGender,"torsob"] = $ThumbCamParams[$tmpGender,"torso"] ;
$ThumbCamParams[$tmpGender,"legs"] = "0 0 -0.4 1.7 35";
$ThumbCamParams[$tmpGender,"legsb"] = $ThumbCamParams[$tmpGender,"legs"] ;
$ThumbCamParams[$tmpGender,"feet"] = "0 0 -0.85 1.2 20";
$ThumbCamParams[$tmpGender,"neck"] = "0.0 -0.2 0.76 1.0 12";
$ThumbCamParams[$tmpGender,"neckb"] = $ThumbCamParams[$tmpGender,"neck"] ;
$ThumbCamParams[$tmpGender,"neckc"] = $ThumbCamParams[$tmpGender,"neck"] ;
$ThumbCamParams[$tmpGender,"chest"] = "0.0 -0.2 0.66 1.0 20";
$ThumbCamParams[$tmpGender,"ear"] = $ThumbCamParams[$tmpGender,"eyes"] ;
$ThumbCamParams[$tmpGender,"wristleft"] = "-0.6 -0.1 0.15 1.5 10";
$ThumbCamParams[$tmpGender,"wristleftb"] = $ThumbCamParams[$tmpGender,"wristleft"] ;
$ThumbCamParams[$tmpGender,"wristright"] = "0.7 -0.1 0.15 1.5 10";
$ThumbCamParams[$tmpGender,"wristrightb"] = $ThumbCamParams[$tmpGender,"wristright"] ;
$ThumbCamParams[$tmpGender,"fingerleft"] = $ThumbCamParams[$tmpGender,"wristleft"] ;
$ThumbCamParams[$tmpGender,"fingerright"] = $ThumbCamParams[$tmpGender,"wristright"] ;
$ThumbCamParams[$tmpGender,"toeleft"] = $ThumbCamParams[$tmpGender,"feet"] ;
$ThumbCamParams[$tmpGender,"toeright"] = $ThumbCamParams[$tmpGender,"feet"] ;
$ThumbCamParams[$tmpGender,"purse"] = "0 -0.1 0.45 3 18";
$ThumbCamParams[$tmpGender,"waist"] = "0 0 0.1 1.0 20";
$ThumbCamParams[$tmpGender,"waistb"] = $ThumbCamParams[$tmpGender,"waist"] ;
$ThumbCamParams[$tmpGender,"back"] = "0 -0.8 0.4 1.8 14";
$ThumbCamParams[$tmpGender,"hat"] = $ThumbCamParams[$tmpGender,"hair"] ;
$ThumbCamParams[$tmpGender,"mask"] = $ThumbCamParams[$tmpGender,"face"] ;
$ThumbCamParams[$tmpGender,"tail"] = $ThumbCamParams[$tmpGender,"waist"] ;
$ThumbCamParams[$tmpGender,"props"] = "0.7 -0.1 0.15 1.5 18";
$ThumbCamParams[$tmpGender,"badges"] = "";
$ThumbCamParams[$tmpGender,"tokens"] = "";
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
if (!isObject(ClosetTabs))
{
    new ScriptObject(ClosetTabs);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(ClosetTabs);
    }
}
function Closet::skuListHasCategory(%list, %category)
{
    %drawers = ThumbCategories.get(%category);
    %n = getWordCount(%drawers) - 1;
    while (%n >= 0)
    {
        if (SkuManager.skuListHasDrawer(%list, getWord(%drawers, %n)))
        {
            return 1;
        }
        %n = %n - 1;
    }
    return 0;
}
function ClosetTabs::setup(%this)
{
    if (!%this.initialized)
    {
        %this.initializing = 1;
        %this.Initialize(ClosetTabContainer, "103 21", "", "", "horizontal");
        %this.newTab("Shops", "platform/client/buttons/closet_tab");
        %this.newTab("Closet", "platform/client/buttons/closet_tab");
        %this.newTab("Body", "platform/client/buttons/closet_tab");
        %this.newTab("Snapshot", "platform/client/buttons/closet_tab");
        %this.newTab("My Designs", "platform/client/buttons/closet_tab");
        ClosetGui.lastTabOpened = "";
        ClosetGui.numberOfPurchasesAwaitingCompletion = 0;
        ClosetGui.numberOfPurchasesPastTimeout = 0;
        %this.initialized = 1;
    }
    %this.initializing = 0;
    return ;
}
function removeShopBannerCache(%shop)
{
    log("network", "debug", "DELETING SHOP BANNER CACHE!!! Shop: " @ %shop);
    if (!$shopBannerCacheCleared[%shop])
    {
        $shopBannerCacheCleared[%shop] = 1;
        deleteFile("dc/cache/platform/client/buttons/banners/store_" @ %shop @ "_n.jpg");
        deleteFile("dc/cache/platform/client/buttons/banners/store_" @ %shop @ "_d.jpg");
        deleteFile("dc/cache/platform/client/buttons/banners/store_" @ %shop @ "_h.jpg");
        deleteFile("dc/cache/platform/client/buttons/banners/store_" @ %shop @ "_i.jpg");
        log("network", "debug", "VAR: AFTER: " SPC $shopBannerCacheCleared[%shop]);
    }
    return ;
}
function ClosetTabs::getInitialButtonOffset(%this)
{
    return "34 55";
}
function ClosetTabs::getPadding(%this)
{
    return 10;
}
function ClosetTabs::createButton(%this, %bitmapName, %tab, %name)
{
    return new GuiBitmapButtonCtrl()
    {
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
    };
    return ;
}
$ClosetCategoryGroup = 286331153;
$ClosetBrandGroup = 286331154;
$BodyFeaturesGroup = 286331155;
$StoreCategoryGroup = 286331156;
$BodyStanceGroup = 286331157;
$ClosetHangersGroup = 286331158;
function ClosetTabs::tabSelected(%this, %tab)
{
    ClosetGui.lastTabOpened = %tab.name;
    if ((((%tab.name $= "CLOSET") || (%tab.name $= "BODY")) || (%tab.name $= "SHOPS")) || (%tab.name $= "MY DESIGNS"))
    {
        ClosetMainObjectView.setVisible(1);
        ClosetMainObjectView.setOrbitDist(3.2);
        ClosetMainObjectView.setLookAtNudge("0 0 0.1");
        ClosetMainObjectView.setLightDirection("0 3 -2");
        %tab.add(ClosetMainBadgeView);
        %tab.bringToFront(ClosetMainBadgeView);
        ClosetMainBadgeView.setVisible(1);
        %tab.add(ClosetMainObjectViewContainer);
        %tab.bringToFront(ClosetMainObjectViewContainer);
        %tab.add(ClosetMainObjectZoomOutButton);
        %tab.bringToFront(ClosetMainObjectZoomOutButton);
    }
    else
    {
        ClosetMainObjectView.setVisible(0);
        ClosetMainBadgeView.setVisible(1);
    }
    if (isObject(%tab.hiliteStrip))
    {
        %offset = %this.getInitialButtonOffset();
        %xoffset = getWord(%offset, 0);
        %yoffset = getWord(%offset, 1) - 2;
        %tab.hiliteStrip.resize(%xoffset, %yoffset, %this.visibleTabsWidth, 2);
    }
    if (%tab.name $= "BODY")
    {
        $player.setGenre("p");
        if (!%this.tabBodyInitialized)
        {
            %this.fillBodyTab();
        }
        else
        {
            BodyFeaturesPopup.rebuildPopupList();
            BodyItemsFrame.update();
        }
        %tab.add(%this.createFilterWidget());
        ClosetMainObjectView.systemDragDrop = 0;
        ClosetTabs.updateBodyTabDisplay();
    }
    else
    {
        if (%tab.name $= "CLOSET")
        {
            $player.setGenre("p");
            if (!%this.tabClosetInitialized)
            {
                %this.fillClosetTab();
            }
            else
            {
                ClosetBrandPopup.update(getFilteredInventoryForSetDrawers());
                if (ClosetBrandPopup.size() > 0)
                {
                    ClosetBrandPopup.SetSelected(0);
                }
                else
                {
                    ClosetItemsFrame.update();
                }
            }
            %tab.add(%this.createFilterWidget());
            %tab.add(%this.createAuthorWidget());
            %this.createWhatYourWearingPanel().reparentSameSize(ClosetWhatYourWearingContainer, "");
            ClosetWhatYoureWearingTitle.setTextWithStyle("You Are Wearing");
            ClosetWhatYoureWearingList.filterByRemovable = 1;
            ClosetMainObjectView.systemDragDrop = 0;
            %outfitNames = $Player::HangerNames[$player.getGender()];
            %i = 0;
            while (%i < $gClosetNumOutfits)
            {
                %name = getWord(%outfitNames, %i);
                %objectView = ClosetTabs.getOutfitObjectView(%i);
                %objectView.setSimObject($player);
                %objectView.setSkus($ClosetSkusBody SPC $ClosetSkusOutfit[%name]);
                %i = %i + 1;
            }
            %outfitNum = findWord($Player::HangerNames[$player.getGender()], $ClosetOutfitName);
            ClosetTabs.getOutfitButton(%outfitNum).performClick();
        }
        else
        {
            if (%tab.name $= "SHOPS")
            {
                $player.setGenre("p");
                if (!%this.tabShopsInitialized)
                {
                    %this.fillStoreTab();
                }
                if (!(StoreExpirationLegend.lastStore $= $gCurrentStoreName))
                {
                    StoreExpirationLegend.lastStore = $gCurrentStoreName;
                    StoreExpirationLegend.setVisible(0);
                }
                %this.showTabWithName("Shops");
                StoreBalanceText.update();
                StoreShortDescText.setBaseDesc("");
                StoreLongDescText.setBaseDesc("");
                if (!($gCurrentStoreName $= ""))
                {
                    ClosetTabs.setLeaveStoreControlsVisible(0);
                    ClosetTabs.setStoreControlsVisible(1);
                    StoreNameDescFrame.nameCtrl.setText(Inventory::getCurrentStoreName());
                    StoreNameDescFrame.descCtrl.setText(Inventory::getCurrentStoreDescInCloset());
                    %storename = getCurrentStoreID();
                    %bannerRsrc = "";
                    if (!(%storename $= ""))
                    {
                        removeShopBannerCache(%storename);
                        %bannerRsrc = "platform/client/buttons/banners/store_" @ %storename;
                        dlMgr.applyUrl(%bannerRsrc, "dlMgrCallback_ShopTexture", "dlMgrCallback_ShopError", %this, "storeads");
                    }
                    if (!(%bannerRsrc $= ""))
                    {
                        StoreBannerBrackets.setVisible(1);
                        StoreBanner.setBitmap(%bannerRsrc);
                    }
                    else
                    {
                        StoreBannerBrackets.setVisible(0);
                    }
                    %bgResource = "";
                    if (!(%storename $= ""))
                    {
                        %bgResource = "platform/client/ui/store_backgrounds/store_bg_" @ %storename;
                    }
                    if (!(%bgResource $= ""))
                    {
                        StoreSpecificBackground.setBitmap(%bgResource);
                        StoreSpecificBackground.setVisible(1);
                        %tab.bringToFront(StoreSpecificBackground);
                    }
                    else
                    {
                        StoreSpecificBackground.setVisible(0);
                    }
                }
                else
                {
                    ClosetTabs.setStoreControlsVisible(0);
                    ClosetTabs.setLeaveStoreControlsVisible(!isInFUE());
                    StoreSpecificBackground.setVisible(0);
                }
                %tab.add(%this.createFilterWidget());
                if ($gCurrentStoreName $= "")
                {
                    %this.createFilterWidget().setVisible(0);
                }
                %tab.add(%this.createAuthorWidget());
                ClosetMainObjectView.systemDragDrop = 0;
                ClosetTabs.refreshStoreTab();
            }
            else
            {
                if (%tab.name $= "SNAPSHOT")
                {
                    ClosetGui.doResetGenre();
                    if (!%this.tabSnapshotInitialized)
                    {
                        %this.fillProfileTab();
                    }
                    %objView = ClosetTabs.getTabWithName("SNAPSHOT").objView;
                    %objView.setSimObject($player);
                    %objView.setSkus(ClosetMainObjectView.getSkus());
                    ProfileSnapRegion.returnClosetGuiFUE = isObject(ClosetGuiFUE) && ClosetGuiFUE.visible;
                    ProfileBackgroundChooser.Initialize();
                    ProfileObjectView.setLightDirection("0 3 -2");
                    ProfileObjectView.setOrbitDist(2.4);
                    ClosetMainObjectView.systemDragDrop = 0;
                }
                else
                {
                    if (%tab.name $= "MY DESIGNS")
                    {
                        $player.setGenre("p");
                        if (!%this.tabMyShopInitialized)
                        {
                            %this.fillMyShopTab();
                        }
                        %this.showTabWithName("MY DESIGNS");
                        %tab.add(%this.createFilterWidget());
                        MyShopTextureInspector.getGroup().pushToBack(MyShopTextureInspector);
                        ClosetMainObjectView.systemDragDrop = 1;
                        %this.createWhatYourWearingPanel().reparentSameSize(MyShopWhatYourWearingContainer, "");
                        ClosetWhatYoureWearingTitle.setTextWithStyle("Custom Items");
                        ClosetWhatYoureWearingList.filterByRemovable = 0;
                    }
                }
            }
        }
    }
    %tab.doneButton.setActive(!ClosetGui.isWaitingForPurchaseCompletion());
    %tab.cancelButton.setActive(!ClosetGui.isWaitingForPurchaseCompletion());
    if (0)
    {
        if (isObject(%tab.thumbnails))
        {
            %tab.thumbnails.makeFirstResponder(1);
        }
        else
        {
            %fr = Canvas.getFirstResponder();
            if (isObject(%fr))
            {
                %fr.makeFirstResponder(0);
            }
        }
    }
    if (isObject(ClosetFilterContainer))
    {
        ClosetFilterField.makeFirstResponder(1);
    }
    if (!(%tab.name $= "CLOSET"))
    {
        ClosetGui.updateVisibleAvatar();
    }
    ClosetMainObjectView.zoomToSKU("");
    if (isInFUE() && !(%this.initializing))
    {
        ClosetGuiFUE.goToStepByName(%tab.name);
    }
    return ;
}
function dlMgrCallback_ShopTexture(%dlItem, %unused)
{
    StoreBanner.setBitmap(%dlItem.localFilename);
    return ;
}
function dlMgrCallback_ShopError(%dlItem)
{
    log("network", "debug", "Image Download Error!! " SPC %dlItem);
    return ;
}
function ClosetTabs::updateRangeText(%this)
{
    %currentTab = %this.getCurrentTab();
    if (!isObject(%currentTab))
    {
        return ;
    }
    %rangeText = %currentTab.rangeText;
    %thumbnails = %currentTab.thumbnails;
    if (!isObject(%thumbnails))
    {
        return ;
    }
    %cellHeight = getWord(%thumbnails.childrenExtent, 1) + %thumbnails.spacing;
    %ypos = 1 - getWord(%thumbnails.getPosition(), 1);
    %closestRow = mFloor((%ypos / %cellHeight) + 0.5);
    %count = %thumbnails.getCount();
    %min = mMin(1 + (%closestRow * %thumbnails.numRowsOrCols), %count);
    %max = mMin(%min + 7, %count);
    %rangeText.setText(%count > 0 ? %min : "");
    return %closestRow;
}
function ClosetTabs::getShortSkuDesc(%this, %sku)
{
    if (%sku <= 0)
    {
        return "";
    }
    else
    {
        %skuInfo = SkuManager.findBySku(%sku);
        %ret = "";
        %ret = %ret @ "<spush><b>" @ %skuInfo.descShrt @ "<spop>";
        return %ret;
    }
    return ;
}
function ClosetTabs::getLongSkuDesc(%this, %sku)
{
    if (%sku <= 0)
    {
        return "";
    }
    else
    {
        %skuInfo = SkuManager.findBySku(%sku);
        %ret = "";
        if (!(trim(%skuInfo.descLong) $= trim(%skuInfo.descShrt)))
        {
            %ret = %ret @ %skuInfo.descLong;
        }
        if (!(%skuInfo.expireTime $= ""))
        {
            %ret = %ret @ "<br><bitmap:platform/client/ui/expiring_icon_small> - expires" SPC secondsToDaysHoursMinutesSeconds(%skuInfo.expireTime) SPC "after you get it.";
        }
        return %ret;
    }
    return ;
}
function ClosetThumbnails::onCreatedChild(%this, %child)
{
    %background = new GuiControl()
    {
        profile = "ClosetLtBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "4 3";
        extent = "95 83";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    };
    %objectView = new GuiObjectView()
    {
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
    if (isObject($player))
    {
        %objectView.setSimObject($player);
    }
    %badgeView = new GuiBitmapCtrl()
    {
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
    %buyStatus = new GuiBitmapCtrl()
    {
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
    %rarityBitmap = new GuiBitmapCtrl()
    {
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
    %frameButton = new GuiBitmapButtonCtrl()
    {
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
    %frameButton.bindClassName("ClosetFrameButton");
    %buttonBacking = new GuiControl()
    {
        profile = "ETSWhiteProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "2 86";
        extent = "99 42";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    };
    %toggleCartButton = new GuiBitmapButtonCtrl()
    {
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
    %buyNowButton = new GuiBitmapButtonCtrl()
    {
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
    %frameFader = new GuiBitmapCtrl()
    {
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
    %logo = new GuiBitmapCtrl()
    {
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
    %expiringIcon = new GuiBitmapCtrl()
    {
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
    %ugcStatusIcon = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "69 4";
        extent = "32 32";
        bitmap = "";
        modulationColor = "255 255 255 80";
    };
    %desc = new GuiMLTextCtrl()
    {
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
        lineSpacing = -1;
    };
    %vpointsPrice = new GuiMLTextCtrl()
    {
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
    %vbuxPrice = new GuiMLTextCtrl()
    {
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
    %totalButton = new GuiVariableWidthButtonCtrl()
    {
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
    %inStockText = new GuiMLTextCtrl()
    {
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
    %priceFader = new GuiBitmapCtrl()
    {
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
    %availabilityText = new GuiMLTextCtrl()
    {
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
    if (!(getWord(%child.getNamespaceList(), 0) $= "ClosetThumbnailCtrl"))
    {
        %child.bindClassName("ClosetThumbnailCtrl");
    }
    return ;
}
function ClosetThumbnails::buttonClicked(%this, %button)
{
    ClosetGui.toggleSku(%button.ctrl.sku);
    %this.makeFirstResponder(1);
    return ;
}
function ClosetThumbnails::getAllSkusInDrawers(%this, %drwrNames)
{
    %skus = "";
    %n = 0;
    while (%n < getWordCount(%drwrNames))
    {
        %s = SkuManager.filterSkusGender(SkuManager.getSkusDrwr(getWord(%drwrNames, %n)), $player.getGender());
        %skus = %skus SPC %s;
        %n = %n + 1;
    }
    return %skus;
}
function getFilteredInventoryForSetDrawers()
{
    %inventory = "";
    if (ClosetTabs.getCurrentTab().name $= "BODY")
    {
        %inventory = $Player::inventory;
    }
    else
    {
        if (ClosetTabs.getCurrentTab().name $= "CLOSET")
        {
            %inventory = $Player::inventory;
        }
        else
        {
            if (ClosetTabs.getCurrentTab().name $= "SHOPS")
            {
                %inventory = Inventory::getCurrentStoreSkus();
            }
            else
            {
                if (ClosetTabs.getCurrentTab().name $= "MY DESIGNS")
                {
                    %inventory = "";
                    error(getScopeName() SPC "- unimplemented." SPC getTrace());
                }
            }
        }
    }
    if (%inventory $= "no store")
    {
        %inventory = "";
    }
    %skus = SkuManager.filterSkusGender(%inventory, $player.getGender());
    %skus = SkuManager.filterSkusRoles(%skus, $player.getRolesMask());
    return %skus;
}
$gClosetThumbnailsDrawersPrevious = "";
$gUpdatingClosetItemPopupFromThumbnailsSetDrawers = 0;
function ClosetThumbnails::setDrawers(%this, %drwrNames)
{
    %skus = getFilteredInventoryForSetDrawers();
    %currentTabName = ClosetTabs.getCurrentTab().name;
    if ((%currentTabName $= "CLOSET") && isObject(ClosetBrandPopup))
    {
        if (!(ClosetItemsFrame.brand $= ""))
        {
            %skus = SkuManager.filterSkusBrand(%skus, $gClosetBrandsIntrnl[ClosetItemsFrame.brand]);
        }
        $gUpdatingClosetItemPopupFromThumbnailsSetDrawers = 1;
        ClosetItemPopup.update(%skus);
        $gUpdatingClosetItemPopupFromThumbnailsSetDrawers = 0;
        if (%drwrNames $= "")
        {
            %category = strlwr(ClosetItemsFrame.category $= "" ? "All Items" : ClosetItemsFrame);
            %drwrNames = ThumbCategories.get(%category);
        }
    }
    %skusTmp = "";
    %n = getWordCount(%drwrNames) - 1;
    while (%n >= 0)
    {
        %s = SkuManager.filterSkusDrwr(%skus, getWord(%drwrNames, %n));
        if (!(%s $= ""))
        {
            %skusTmp = %s SPC %skusTmp;
        }
        %n = %n - 1;
    }
    %skus = trim(%skusTmp);
    %this.setSkus(%skus);
    return ;
}
function ClosetThumbnails::setUnfilteredSkus(%this, %skus)
{
    %this.unfilteredSkus = %skus;
    %this.refilter();
    return ;
}
function ClosetThumbnails::refilter(%this)
{
    %this.setSkus(%this.unfilteredSkus);
    return ;
}
function ClosetThumbnails::setSkus(%this, %skus)
{
    %startingPos = %this.getPosition();
    %currentTabName = ClosetTabs.getCurrentTab().name;
    %skus = filterOutSkusToHideInCloset(%skus);
    if (%currentTabName $= "SHOPS")
    {
        %skus = SkuManager.filterSkusNonZeroManufactured(%skus);
    }
    %skus = trim(%skus);
    if ((((%currentTabName $= "CLOSET") || (%currentTabName $= "SHOPS")) || (%currentTabName $= "BODY")) || (%currentTabName $= "MY DESIGNS"))
    {
        %userFilterText = isObject(ClosetFilterField) ? ClosetFilterField.getValue() : "";
        %skus = SkuManager.filterSkusDescription(%skus, %userFilterText);
    }
    if (isObject(%this.otherGenderText))
    {
        %numSkusOtherGender = getWordCount(%skus);
        %skus = SkuManager.filterSkusGender(%skus, $player.getGender());
        %numSkus = getWordCount(%skus);
        %numSkusOtherGender = %numSkusOtherGender - %numSkus;
        %text = %numSkusOtherGender == 0 ? "" : "<just:right>(";
        %this.otherGenderText.setText(%text);
    }
    else
    {
        %numSkus = getWordCount(%skus);
    }
    %this.setNumChildren(%numSkus);
    if (isObject(%this.infoText))
    {
        if ((((%numSkus != 0) || (ClosetTabs.getCurrentTab().name $= "BODY")) && !((BodyItemsFrame.features $= "Height"))) || !((BodyItemsFrame.features $= "Stance")))
        {
            %this.infoText.setVisible(0);
            %this.scroll.setVisible(1);
        }
        else
        {
            %this.scroll.setVisible(0);
            %this.infoText.setVisible(1);
            %this.infoText.setText("no matching items");
        }
    }
    if (!isObject(ClosetCurrentCamParams))
    {
        new StringMap(ClosetCurrentCamParams);
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(ClosetCurrentCamParams);
        }
    }
    ClosetCurrentCamParams.adjustForHeight();
    if ($ClosetOutfitName $= "")
    {
        warn("wardrobe", getScopeName() @ ": $ClosetOutfitName is empty");
        return ;
    }
    %n = 0;
    while (%n < %numSkus)
    {
        %cell = %this.getObject(%n);
        %skunum = getWord(%skus, %n);
        if (((((getWordCount(%skunum) < 1) || (getWordCount(%skunum) > 1)) || (%skunum == 0)) || (%skunum $= 0)) || (%numSkus != getWordCount(%skus)))
        {
            error(getScopeName() SPC "- cell#" @ %cell.getId() SPC "- sku #" @ %n SPC "of" SPC %numSkus @ "/" @ getWordCount(%skus) SPC "- skus:" SPC %skunum SPC "- end.");
            error(getScopeName() SPC "- skus:" SPC %skus SPC "- end.");
        }
        %this.setCellSkus(%cell, %skunum);
        %n = %n + 1;
    }
    %dkBackground = 0;
    %currentDrwrName = "";
    %expiringItemsCount = 0;
    %n = 0;
    while (%n < %numSkus)
    {
        %cell = %this.getObject(%n);
        %skunum = getWord(%skus, %n);
        %skuItem = SkuManager.findBySku(%skunum);
        %cell.descCtrl.setText(%skuItem.descShrt);
        if (%skuItem.brand $= "roca")
        {
            %cell.logo.setBitmap("platform/client/ui/roca_logo_small");
        }
        else
        {
            if (%skuItem.brand $= "myet")
            {
                %cell.logo.setBitmap("platform/client/ui/myet_logo_small");
            }
            else
            {
                if (%skuItem.brand $= "pcd")
                {
                    %cell.logo.setBitmap("platform/client/ui/pcd_logo_small");
                }
                else
                {
                    if (%skuItem.brand $= "staff")
                    {
                        %cell.logo.setBitmap("platform/client/ui/staff_logo_small");
                    }
                    else
                    {
                        if (%skuItem.brand $= "new")
                        {
                            %cell.logo.setBitmap("platform/client/ui/new_logo_small");
                        }
                        else
                        {
                            %cell.logo.setBitmap("");
                        }
                    }
                }
            }
        }
        if (%skuItem.hasTag("new"))
        {
            %cell.logo.setBitmap("platform/client/ui/new_logo_small");
        }
        if (!(%skuItem.expireTime $= ""))
        {
            %cell.expiringIcon.setBitmap("platform/client/ui/expiring_icon");
            %cell.expiringIcon.setVisible(1);
            %expiringItemsCount = %expiringItemsCount + 1;
        }
        else
        {
            %cell.expiringIcon.setVisible(0);
        }
        if (%currentTabName $= "MY DESIGNS")
        {
            %cell.ugcStatusIcon.setBitmap(ClosetGui_MyShop_GetSkuUGCStatusIcon(%skunum));
            %cell.ugcStatusIcon.setVisible(1);
        }
        else
        {
            %cell.ugcStatusIcon.setVisible(0);
        }
        %cell.rarityBitmap.setBitmap(%this.getRarityBitmap(%skuItem.qty));
        %cell.frameButton.setActive(SkuManager.isWearableSkuType(%skuItem.skuType));
        if (%this.tab.name $= "SHOPS")
        {
            %vpointsSym = "platform/client/ui/vpoints_9";
            %vbuxSym = "platform/client/ui/vbux_9";
            %vpointsPrice = Inventory::getVPointsPriceForSku(%cell.sku);
            %vbuxPrice = Inventory::getVBuxPriceForSku(%cell.sku);
            if ((%vpointsPrice == 0) && (%vbuxPrice == 0))
            {
                %cell.vpointsCtrl.setText("<just:right>free!");
                %cell.vbuxCtrl.setText("");
            }
            else
            {
                %cell.vpointsCtrl.setText("");
                %cell.vbuxCtrl.setText("");
                if (%vpointsPrice > 0)
                {
                    %cell.vpointsCtrl.setText("<bitmap:" @ %vpointsSym @ "> " @ %vpointsPrice);
                }
                if (%vbuxPrice > 0)
                {
                    %cell.vbuxCtrl.setText("<bitmap:" @ %vbuxSym @ "> " @ %vbuxPrice);
                }
            }
            %cell.totalButton.setVisible(1);
            %cell.inStockText.setText(%this.GetInStockText($gStoreItemsQty[%cell.sku]));
            if (findWord($Player::inventory, %cell.sku) >= 0)
            {
                %this.SetCellAvailability(%cell, 0, 1, "<just:right><color:00bb00>0wn3d!", "platform/client/ui/owned");
            }
            else
            {
                if ($gStoreItemsQty[%cell.sku] == 0)
                {
                    %this.SetCellAvailability(%cell, 0, 0, "<just:right><color:dd0000>Sold Out!", "");
                }
                else
                {
                    if (%skuItem.rspk > (respektScoreToLevel($gMyRespektPoints) + 1))
                    {
                        %this.SetCellAvailability(%cell, 0, 0, "<just:right><color:bb0000>More Levels!", "platform/client/ui/cantbuy2");
                    }
                    else
                    {
                        if (%skuItem.rspk > respektScoreToLevel($gMyRespektPoints))
                        {
                            %this.SetCellAvailability(%cell, 0, 0, "<just:right><color:dd0000>Next Level!", "platform/client/ui/cantbuy");
                        }
                        else
                        {
                            %this.SetCellAvailability(%cell, 1, 1, "", "");
                        }
                    }
                }
            }
        }
        else
        {
            %cell.vpointsCtrl.setText("");
            %cell.vbuxCtrl.setText("");
            %cell.available = 0;
        }
        if (!(%currentDrwrName $= %skuItem.drwrName))
        {
            %currentDrwrName = %skuItem.drwrName;
            %dkBackground = !%dkBackground;
        }
        %cell.background.setProfile(%dkBackground ? ClosetDkBackgroundProfile : ClosetLtBackgroundProfile);
        %n = %n + 1;
    }
    if ((%currentTabName $= "SHOPS") && (%expiringItemsCount > 0))
    {
        StoreExpirationLegend.setVisible(1);
    }
    %this.setSelectedThumbs();
    %this.getParent().scrollTo(0, 1 - getWord(%startingPos, 1));
    return ;
}
function ClosetThumbnails::SetCellAvailability(%this, %cell, %showPrice, %canTryOn, %subText, %overlayBitmapName)
{
    if (!(%overlayBitmapName $= ""))
    {
        %cell.buyStatus.setBitmap(%overlayBitmapName);
        %cell.buyStatus.setVisible(1);
    }
    else
    {
        %cell.buyStatus.setVisible(0);
    }
    if (!(%subText $= ""))
    {
        %cell.availabilityText.setVisible(1);
        %cell.availabilityText.setText(%subText);
        %cell.available = 0;
    }
    else
    {
        %cell.availabilityText.setVisible(0);
        %cell.available = 1;
    }
    %cell.priceFader.setVisible(!%showPrice);
    %cell.frameButton.setActive(%canTryOn && SkuManager.isWearableSkuType(%cell.SkuItem.skuType));
    %cell.frameFader.setVisible(!%canTryOn);
    return ;
}
function ClosetThumbnails::getRarityBitmap(%this, %qty)
{
    %base = "platform/client/ui/";
    if (%qty < 0)
    {
        return "";
    }
    if (%qty < 1000)
    {
        return %base @ "rarity_superrare";
    }
    if (%qty < 5000)
    {
        return %base @ "rarity_reallyrare";
    }
    if (%qty < 10000)
    {
        return %base @ "rarity_rare";
    }
    return "";
}
function ClosetThumbnails::GetInStockText(%this, %qty)
{
    if (%qty <= 0)
    {
        return "";
    }
    if (%qty < 25)
    {
        return "in stock: <color:dd0000>almost gone!";
    }
    if (%qty < 50)
    {
        return "in stock: <color:ee8800>not many";
    }
    if (%qty < 100)
    {
        return "in stock: <color:ee8800>a few";
    }
    if (%qty < 500)
    {
        return "in stock: <color:00bb00>enough";
    }
    return "in stock: <color:00bb00>yes!";
}
function ClosetThumbnails::setCellSkus(%this, %cell, %skus)
{
    if (getWordCount(%skus) < 1)
    {
        error(getScopeName() SPC "no skus passed in!" SPC getTrace());
        return ;
    }
    if (getWordCount(%skus) != 1)
    {
        error(getScopeName() SPC "sorry, only 1 sku is currently supported." SPC %skus);
        %skus = getWord(%skus, 0);
    }
    %skunum = %skus;
    %bodyAndOutfitSkus = $ClosetSkusOutfit[$ClosetOutfitName] SPC $ClosetSkusBody;
    %skuItem = SkuManager.findBySku(%skunum);
    %thumb = %cell.objectView;
    %badge = %cell.badgeView;
    %cell.sku = %skunum;
    %cell.SkuItem = %skuItem;
    if (%skuItem.skuType $= "mesh")
    {
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
        %thumb.makeSlaveOf(ClosetMainObjectView);
        %thumb.setSimObject($player);
        %thumb.setLightDirection("0 3 -2");
        %thumb.setLookAtNudge(%lookAtNudge);
        %thumb.setOrbitDist(%dist);
        %thumb.setFOV(%fov);
    }
    else
    {
        if (%skuItem.skuType $= "badge")
        {
            %thumb.setVisible(0);
            %badge.setVisible(1);
            %bitmapName = %skuItem.getBitmapPath();
            %badge.setBitmap(%bitmapName);
        }
        else
        {
            if (%skuItem.skuType $= "token")
            {
                %thumb.setVisible(0);
                %badge.setVisible(1);
                %bitmapName = %skuItem.getBitmapPath();
                %badge.setBitmap(%bitmapName);
            }
            else
            {
                if (%skuItem.skuType $= "swatch")
                {
                    error("swatch in the closet!" SPC %skunum);
                }
            }
        }
    }
    return ;
}
function ClosetCurrentCamParams::adjustForHeight(%this)
{
    %allDrawers = SkuManager.allClosetDrawers();
    %allDrawers = %allDrawers SPC "fullbody";
    %numDrawers = getWordCount(%allDrawers);
    %i = 0;
    while (%i < %numDrawers)
    {
        %drawer = strlwr(getWord(%allDrawers, %i));
        %params = $ThumbCamParams[$player.getGender(),%drawer];
        if (!(%params $= ""))
        {
            %vNudge = getWord(%params, 2);
            %vNudge = %vNudge + ((%vNudge + 1) * ($UserPref::Player::height - 1));
            %params = setWord(%params, 2, %vNudge);
        }
        %this.put(%drawer, %params);
        %i = %i + 1;
    }
}

function ClosetMainObjectView::zoomToSKU(%this, %sku)
{
    if (%sku $= "")
    {
        %drawer = "fullbody";
        ClosetMainObjectZoomOutButton.setVisible(0);
    }
    else
    {
        %drawer = SkuManager.findBySku(%sku).drwrName;
        ClosetMainObjectZoomOutButton.setVisible(1);
    }
    %params = ClosetCurrentCamParams.get(%drawer);
    %this.setCamParams(%params);
    return ;
}
function GuiObjectView::setCamParams(%this, %params)
{
    if (%params $= "")
    {
        return ;
    }
    else
    {
        %dist = getWord(%params, 3);
        %fov = getWord(%params, 4);
        %lookAtNudge = getWords(%params, 0, 2);
        if (!(%this.fovFac $= ""))
        {
            %fov = %fov * %this.fovFac;
        }
    }
    %this.setLightDirection("0 3 -2");
    %this.setLookAtNudge(%lookAtNudge);
    %this.setOrbitDist(%dist);
    %this.setFOV(%fov);
    return ;
}
function ClosetThumbnails::SetSelected(%this, %cell, %selected)
{
    %cell.selected = %selected;
    %buttonsDir = "platform/client/buttons/";
    %selString = %cell.selected ? "_sel" : "";
    %hiString = %cell.hilited ? "_hi" : "";
    %cell.frameButton.setBitmap(%buttonsDir @ "frame" @ %selString @ %hiString);
    return ;
}
function ClosetThumbnails::setSelectedThumbs(%this)
{
    if (ClosetTabs.getCurrentTab().name $= "SHOPS")
    {
        %selectedSkus = $StoreSkusLayer;
    }
    else
    {
        %selectedSkus = $ClosetSkusOutfit[$ClosetOutfitName] SPC $ClosetSkusBody;
    }
    %numThumbs = %this.getCount();
    %n = 0;
    while (%n < %numThumbs)
    {
        %cell = %this.getObject(%n);
        %selected = findWord(%selectedSkus, %cell.sku) >= 0;
        %this.SetSelected(%cell, %selected);
        %n = %n + 1;
    }
}

$gClosetThumbnailStoreHighlightTimer = "";
$gClosetThumbnailStoreHighlightDelayMS = 0;
function ClosetThumbnailCtrl::onHilite(%this)
{
    if (getCurrentStoreID() $= "")
    {
        return ;
    }
    %this.frameButton.mouseOver = 1;
    %this.thumbnails.scroll.scrollToCell(%this);
    %si = SkuManager.findBySku(%this.sku);
    %descShort = ClosetTabs.getShortSkuDesc(%this.sku);
    %descLong = ClosetTabs.getLongSkuDesc(%this.sku);
    if ($DevPref::Closet::skuDeets)
    {
        %descLong = %descLong @ "<font:Courier New:14>";
        %descLong = %descLong NL "sku    = " SPC %si.skuNumber;
        %descLong = %descLong NL "type   = " SPC %si.skuType;
        %descLong = %descLong NL "mesh   = " SPC %si.meshName;
        %descLong = %descLong NL "txtrs  = " SPC %si.getTxtrNames();
        %descLong = %descLong NL "roles  = " SPC roles::getRoleStrings(%si.rolesMask);
        %descLong = %descLong NL "bornW/ = " SPC %si.born;
        %descLong = %descLong NL "rspkt  = " SPC %si.rspk;
        %descLong = %descLong NL "tags   = " SPC SkuManager.getSkuTags(%si.skuNumber);
    }
    if (isObject(BodyLongDescText))
    {
        BodyShortDescText.setText(%descShort);
        BodyLongDescText.setText(%descLong);
    }
    if (isObject(ClosetLongDescText))
    {
        ClosetShortDescText.setText(%descShort);
        ClosetLongDescText.setText(%descLong);
    }
    if (isObject(StoreLongDescText))
    {
        StoreShortDescText.setText(%descShort);
        StoreLongDescText.setText(%descLong);
    }
    ClosetTabs.updateAuthorWidget(%this.sku);
    if (%this.available && (%this.thumbnails == ClosetThumbnailsShop.getId()))
    {
        if (!($gClosetThumbnailStoreHighlightTimer $= ""))
        {
            cancel($gClosetThumbnailStoreHighlightTimer);
        }
        $gClosetThumbnailStoreHighlightTimer = %this.schedule($gClosetThumbnailStoreHighlightDelayMS, "onHiliteStore");
    }
    return ;
}
function ClosetThumbnailCtrl::onHiliteStore(%this)
{
    if (!($gClosetThumbnailStoreHighlightTimer $= ""))
    {
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
    if (isObject(StoreItemDescHiliteFrame))
    {
        StoreItemDescHiliteFrame.setVisible(1);
    }
    if (isObject(StoreFloatingHiliteFrame))
    {
        StoreFloatingHiliteFrame.setVisible(1);
        %screenPos = StoreFloatingHiliteFrame.getScreenPosition();
        %pos = StoreFloatingHiliteFrame.getPosition();
        %offsetX = getWord(%screenPos, 0) - getWord(%pos, 0);
        %offsetY = getWord(%screenPos, 1) - getWord(%pos, 1);
        %newPosX = (getWord(%this.getScreenPosition(), 0) - %offsetX) - 9;
        %newPosY = (getWord(%this.getScreenPosition(), 1) - %offsetY) - 4;
        StoreFloatingHiliteFrame.reposition(%newPosX, %newPosY);
        %scroll = %this.thumbnails.scroll;
        %padding = 10;
        %minx = getWord(%scroll.getScreenPosition(), 0) - %padding;
        %minY = getWord(%scroll.getScreenPosition(), 1) - %padding;
        %maxX = (%minx + getWord(%scroll.getExtent(), 0)) + (2 * %padding);
        %maxy = (%minY + getWord(%scroll.getExtent(), 1)) + (2 * %padding);
        %posX = getWord(%this.getScreenPosition(), 0);
        %posY = getWord(%this.getScreenPosition(), 1);
        %width = getWord(%this.getExtent(), 0);
        %height = getWord(%this.getExtent(), 1);
        StoreFloatingHiliteFrame.setVisible((((%posX >= %minx) && ((%posX + %width) <= %maxX)) && (%posY >= %minY)) && ((%posY + %height) <= %maxy));
    }
    return ;
}
function ClosetThumbnailCtrl::onUnhilite(%this)
{
    %this.frameButton.mouseOver = 0;
    if (0)
    {
        if (isObject(BodyLongDescText))
        {
            BodyShortDescText.setText("");
            BodyLongDescText.setText("");
        }
        if (isObject(ClosetLongDescText))
        {
            ClosetShortDescText.setText("");
            ClosetLongDescText.setText("");
        }
        if (isObject(StoreLongDescText))
        {
            StoreShortDescText.showBaseDesc();
            StoreLongDescText.showBaseDesc();
        }
        ClosetTabs.updateAuthorWidget("");
    }
    if (ClosetTabs.tabShopsInitialized && (%this.thumbnails == ClosetThumbnailsShop.getId()))
    {
        %this.hilited = 0;
        %buttonsDir = "platform/client/buttons/";
        %frameBitmap = %this.selected ? "frame_sel" : "frame";
        %cartBitmap = StoreShoppingList.containsSku(%this.sku) ? "removeFromCart" : "add2cart";
        %this.frameButton.setBitmap(%buttonsDir @ %frameBitmap);
        %this.toggleCartButton.setVisible(0);
        %this.toggleCartButton.setBitmap(%buttonsDir @ %cartBitmap);
        %this.buyNowButton.setVisible(0);
        %this.buttonBacking.setVisible(0);
        if (isObject(StoreItemDescHiliteFrame))
        {
            StoreItemDescHiliteFrame.setVisible(0);
        }
        if (isObject(StoreFloatingHiliteFrame))
        {
            StoreFloatingHiliteFrame.setVisible(0);
        }
    }
    return ;
}
function ClosetThumbnailCtrl::onSelect(%this)
{
    %this.frameButton.performClick();
    return ;
}
function ClosetThumbnailCtrl::onMouseLeaveBounds(%this)
{
    %this.onUnhilite();
    return ;
}
function ClosetThumbnailCtrl::addToCart(%this)
{
    StoreShoppingList.addSku(%this.sku);
    return ;
}
function ClosetThumbnailCtrl::toggleInCart(%this)
{
    if (StoreShoppingList.containsSku(%this.sku))
    {
        StoreShoppingList.removeSku(%this.sku);
    }
    else
    {
        StoreShoppingList.addSku(%this.sku);
    }
    return ;
}
function ClosetThumbnailCtrl::buyNow(%this)
{
    if (%this.sku)
    {
        ClosetGui.purchaseSkus(%this.sku);
    }
    return ;
}
function ClosetFrameButton::onMouseEnter(%this)
{
    %thumbnails = %this.thumbnails;
    %i = %this.thumbnails.getObjectIndex(%this.ctrl);
    %row = mFloor(%i / %thumbnails.numRowsOrCols);
    %col = %i % %thumbnails.numRowsOrCols;
    %thumbnails.hiliteCell(%col, %row);
    return ;
}
$gAllOutfits = "A B C D E F G H I J K L";
$Player::HangerNames["f"] = "fA fB fC fD fE fF fG fH fI fJ fK fL";
$Player::HangerNames["m"] = "mA mB mC mD mE mF mG mH mI mJ mK mL";
$gClosetNumOutfits = getWordCount($Player::HangerNames["f"]);
$ClosetOutfitName = "";
function ClosetGui::open(%this)
{
    echo(getScopeName() @ "->debug for ETS-8039, $ClosetOutfitName at closet opening is: " @ $ClosetOutfitName);
    if (!isObject($player))
    {
        error(getScopeName() SPC "- no player" SPC getTrace());
        return ;
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
    %outfitNames = $Player::HangerNames[$player.getGender()];
    %i = 0;
    while (%i < $gClosetNumOutfits)
    {
        %name = getWord(%outfitNames, %i);
        $ClosetSkusOutfit[%name] = $gOutfits.get(%name) ;
        %i = %i + 1;
    }
    checkOutfitCorruption(1);
    ClosetTabs.setup();
    if ($player.isSitting() == 1)
    {
        if ($IN_ORBIT_CAM == 0)
        {
            togglePlayerCamMode();
        }
        if ($player.isKissSeat == 1)
        {
            commandToServer('RequestToStand', 0, 0);
        }
    }
    if ($IN_ORBIT_CAM == 1)
    {
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
    if ((((($UserPref::Player::Genre $= "h") || ($UserPref::Player::Genre $= "i")) || ($UserPref::Player::Genre $= "p")) || ($UserPref::Player::Genre $= "t")) || ($UserPref::Player::Genre $= "s"))
    {
        ClosetGui.selectGenre($UserPref::Player::Genre);
    }
    pushScreenSize(960, 544, 0, 1, 1);
    %closetGuiFUEIsObject = isObject(ClosetGuiFUE);
    if (isInFUE())
    {
        if (!%closetGuiFUEIsObject)
        {
            ClosetGuiPositioner.execHideAndAddChild("./closetGuiFUE.gui", "");
        }
        ClosetGuiFUE.open();
    }
    else
    {
        if (%closetGuiFUEIsObject && ClosetGuiFUE.isVisible())
        {
            ClosetGuiFUE.close();
        }
    }
    if (!$Player::hasSeenTakeAvatarPhotoDialog)
    {
        if (!ClosetTabs.tabSnapshotInitialized)
        {
            ClosetTabs.fillProfileTab();
        }
        ProfileCurrentPicture.update("");
    }
    if (isObject(ProfileObjectView))
    {
        ProfileObjectView.resetLight();
    }
    return ;
}
function ClosetGui::close(%this, %cancel)
{
    %this.doClose(%cancel, 1);
    return ;
}
function ClosetGui::doClose(%this, %cancel, %allowMsgBoxOnExit)
{
    if (%this.isWaitingForPurchaseCompletion())
    {
        return 0;
    }
    if ((ClosetTabs.getCurrentTab().name $= "MY DESIGNS") && MyShopTextureInspector.isVisible())
    {
        MyShopTextureInspector.close();
        return 0;
    }
    if (ClosetTabs.getCurrentTab().name $= "Shops")
    {
        %i = getWordCount($StoreSkusLayer) - 1;
        while (%i >= 0)
        {
            %aTriedOnSku = getWord($StoreSkusLayer, %i);
            if (%skuIndex = findWord($Player::inventory, %aTriedOnSku) >= 0)
            {
                if (SkuManager.isBodySku(%aTriedOnSku) && (findWord($ClosetSkusBody, %aTriedOnSku) < 0))
                {
                    $ClosetSkusBody = SkuManager.overlaySkus($ClosetSkusBody, %aTriedOnSku);
                }
                else
                {
                    if (SkuManager.isOutfitSku(%aTriedOnSku) && (findWord($ClosetSkusOutfit[$ClosetOutfitName], %aTriedOnSku) < 0))
                    {
                        $ClosetSkusOutfit[$ClosetOutfitName] = SkuManager.overlaySkus($ClosetSkusOutfit[$ClosetOutfitName], %aTriedOnSku) ;
                    }
                }
            }
            %i = %i - 1;
        }
        saveStorePosition();
    }
    if (((%allowMsgBoxOnExit && (ClosetTabs.getCurrentTab().name $= "SHOPS")) && (StoreShoppingList.getCount() > 0)) && !((Inventory::getCurrentStoreName() $= "")))
    {
        MessageBoxYesNo("Leave the Store?", "You still have items in your shopping cart that you haven\'t bought." NL "<spush><b>Do you really want to return to the world?<spop>" NL "(The items will stay in your cart.)", "ClosetGui.reallyClose(" @ %cancel @ ");", "");
    }
    if (%this.askUserToDropProp())
    {
        return 0;
    }
    if ((((%allowMsgBoxOnExit && !%cancel) && !gUserPropMgrClient.getProperty($Player::Name, "hasTakenAvatarPhoto", 0)) && !$Player::hasSeenTakeAvatarPhotoDialog) && !$StandAlone)
    {
        $Player::hasSeenTakeAvatarPhotoDialog = 1;
        MessageBoxYesNo($MsgCat::closet["MSG-NO-AVATAR-PHOTO-TITLE"], $MsgCat::closet["MSG-NO-AVATAR-PHOTO"], "ClosetTabs.selectTabWithName(\"SNAPSHOT\");", "ClosetGui.reallyClose(" @ %cancel @ ");");
        return 0;
    }
    %this.reallyClose(%cancel);
    return ;
}
function ClosetGui::reallyClose(%this, %cancel)
{
    stopPropAction();
    closetMap.pop();
    if (isObject(StoreSpecificBackground))
    {
        StoreSpecificBackground.setVisible(0);
    }
    %this.doResetGenre();
    if (%cancel $= "")
    {
        %cancel = 0;
    }
    if (%cancel)
    {
        %this.doCancel();
    }
    else
    {
        %this.doOkay();
    }
    if (GuiTracker.inTransit)
    {
        Canvas.setContent(GuiTracker.previouslyOpened);
    }
    else
    {
        Canvas.setContent(PlayGui);
    }
    %this.setVisible(0);
    nextPlayerCamMode();
    setIdle(0);
    if ($gSalonChairCurrent != 0)
    {
        if (!(%this.oldAnimation $= ""))
        {
            $player.playAnim(%this.oldAnimation);
        }
    }
    else
    {
        $player.playAnim($player.getGender() @ $player.getGenre() @ "idl1a");
    }
    %this.oldAnimation = "";
    getUserActivityMgr().setActivityActive("dressing", 0);
    popScreenSize();
    if (!gUserPropMgrClient.getProperty($Player::Name, "hasSeenPropUseAdvisory", 0) && SkuManager.hasPropSku($ClosetSkusOutfit[$ClosetOutfitName]))
    {
        gUserPropMgrClient.setProperty($Player::Name, "hasSeenPropUseAdvisory", 1);
        MessageBoxOK($MsgCat::closet["MSG-USE-PROP-TITLE"], $MsgCat::closet["MSG-USE-PROP-BODY"], "");
    }
    Inventory::fetchPlayerInventoryIfEmpty();
    return ;
}
function ClosetGui::doResetCurrent(%this)
{
    checkOutfitCorruption(1);
    $ClosetSkusOutfit[$ClosetOutfitName] = $gOutfits.get($ClosetOutfitName) ;
    %this.updateVisibleAvatar();
    ClosetMainObjectView.zoomToSKU("");
    ClosetItemsFrame.update();
    return ;
}
function ClosetGui::doResetGenre(%this)
{
    $UserPref::Player::Genre = %this.oldStance;
    if (!(%this.currentOverrideGenre $= $UserPref::Player::Genre))
    {
        $player.setGenre(%this.currentOverrideGenre);
    }
    else
    {
        $player.setGenre($UserPref::Player::Genre);
    }
    return ;
}
function ClosetGui::doResetAll(%this)
{
    $UserPref::Player::height = %this.oldHeight;
    $ClosetOutfitName = $player.getGender() @ $gOutfits.get("currentOutfit");
    %outfitNames = $Player::HangerNames[$player.getGender()];
    %i = 0;
    while (%i < $gClosetNumOutfits)
    {
        %name = getWord(%outfitNames, %i);
        $ClosetSkusOutfit[%name] = $gOutfits.get(%name) ;
        %i = %i + 1;
    }
    $ClosetSkusBody = $gOutfits.get($player.getGender() @ "Body");
    %this.updateVisibleAvatar();
    ClosetMainObjectView.zoomToSKU("");
    ClosetTabs.updateBodyTabDisplay();
    return ;
}
function ClosetGui::doCancel(%this)
{
    if (checkOutfitCorruption(1))
    {
        outfitsCorruptedNotify();
    }
    %this.doResetAll();
    $player.setActiveSKUs($ClosetSkusOutfit[$ClosetOutfitName] SPC $ClosetSkusBody);
    return ;
}
function ClosetGui::doOkay(%this)
{
    $gOutfits.put("currentOutfit", strupr(getSubStr($ClosetOutfitName, 1, 1)));
    $gOutfits.put($player.getGender() @ "Body", $ClosetSkusBody);
    %outfitNames = $Player::HangerNames[$player.getGender()];
    %n = $gClosetNumOutfits - 1;
    while (%n >= 0)
    {
        %name = getWord(%outfitNames, %n);
        $ClosetSkusOutfit[%name] = outfits_filterSKUList($ClosetSkusOutfit[%name]) ;
        $gOutfits.put(%name, $ClosetSkusOutfit[%name]);
        %n = %n - 1;
    }
    if (checkOutfitCorruption(1))
    {
        outfitsCorruptedNotify();
        return 0;
    }
    outfits_persist();
    %activeSkus = $ClosetSkusOutfit[$ClosetOutfitName] SPC $ClosetSkusBody;
    if (%this.wasInHelpmode)
    {
        %activeSkus = %activeSkus SPC getSpecialSKU(0, "helpmebadge");
    }
    commandToServer('SetActiveSkus', %activeSkus);
    commandToServer('setHeight', $UserPref::Player::height);
    gUserPropMgrClient.setProperty($Player::Name, "avatarHeight", $UserPref::Player::height);
    %playerActiveSKUs = $player.getActiveSKUs();
    if (%this.wasInHelpmode)
    {
        %playerActiveSKUs = %playerActiveSKUs SPC getSpecialSKU(0, "helpmebadge");
    }
    $player.schedule(0, "setActiveSkus", %playerActiveSKUs);
    if (isObject(closetGuiFUEHideTipsCtrl))
    {
        closetGuiFUEHideTipsCtrl.setValue(1);
        closetGuiFUEHideTipsCtrl.onAction();
    }
    return ;
}
function ClosetGui::askUserToDropProp(%this)
{
    %propSku = SkuManager.filterSkusDrwr($ClosetSkusOutfit[$ClosetOutfitName], "props");
    if (%propSku $= "")
    {
        return 0;
    }
    else
    {
        if (canHavePropsInGenre(%this.currentOverrideGenre))
        {
            return 0;
        }
    }
    %index = findWord($ClosetSkusOutfit[$ClosetOutfitName], %propSku);
    %outfitWithoutProp = removeWord($ClosetSkusOutfit[$ClosetOutfitName], %index);
    %activity = "engaging in this activity";
    if (%this.currentOverrideGenre $= "k")
    {
        %activity = "skating";
    }
    else
    {
        if (%this.currentOverrideGenre $= "w")
        {
            %activity = "swimming";
        }
        else
        {
            if (%this.currentOverrideGenre $= "o")
            {
                %activity = "sumo wrestling";
            }
            else
            {
                if (%this.currentOverrideGenre $= "l")
                {
                    %activity = "pillow fighting";
                }
                else
                {
                    if (%this.currentOverrideGenre $= "s")
                    {
                        %activity = "strutting your stuff";
                    }
                }
            }
        }
    }
    %body = $MsgCat::closet["MSG-NO-PROP-IN-THIS-GENRE-BODY1"] SPC %activity @ $MsgCat::closet["MSG-NO-PROP-IN-THIS-GENRE-BODY2"];
    MessageBoxYesNo($MsgCat::closet["MSG-NO-PROP-IN-THIS-GENRE-TITLE"], %body, "$ClosetSkusOutfit[$ClosetOutfitName] = \"" @ %outfitWithoutProp @ "\"; ClosetGui.reallyClose(false);", "");
    return 1;
}
function ClosetGui::userHasChangedBodyOrOutfit(%this)
{
    if (!($ClosetSkusBody $= $gOutfits.get($player.getGender() @ "Body")))
    {
        return 1;
    }
    if (!($gOutfits.get("currentOutfit") $= strupr(getSubStr($ClosetOutfitName, 1, 1))))
    {
        return 1;
    }
    %outfitNames = $Player::HangerNames[$player.getGender()];
    %n = $gClosetNumOutfits - 1;
    while (%n >= 0)
    {
        %name = getWord(%outfitNames, %n);
        %newOutfit = outfits_filterSKUList($ClosetSkusOutfit[%name]);
        %oldOutfit = $gOutfits.get(%name);
        if (!(%newOutfit $= %oldOutfit))
        {
            return 1;
        }
        %n = %n - 1;
    }
    return 0;
}
function ClosetGui::purchaseSkus(%this, %skus)
{
    if (%this.isWaitingForPurchaseCompletion())
    {
        MessageBoxOK("Processing Previous Purchase", "We\'re working hard to process the purchase you just made. Please wait a minute and try again.", "");
        return ;
    }
    %cbPoints = "ClosetGui.purchaseSkusVPoints(\"" @ %skus @ "\");";
    %cbBux = "ClosetGui.purchaseSkusVBux   (\"" @ %skus @ "\");";
    %cbCancel = "";
    ShowPurchaseSkusConfirmationDialog(%skus, %cbPoints, %cbBux, %cbCancel);
    return ;
}
function ShowPurchaseSkusConfirmationDialog(%skus, %cbPoints, %cbBux, %cbCancel)
{
    %numSkus = getWordCount(%skus);
    if ((%numSkus == 0) && (%skus $= 0))
    {
        error(getScopeName() SPC "- no skus!" SPC getTrace());
        return 0;
    }
    %skusValidPoints = Inventory::filterSkusByValidPrice("vPoints", %skus);
    %skusValidBux = Inventory::filterSkusByValidPrice("vBux", %skus);
    %numSkusValidPoints = getWordCount(%skusValidPoints);
    %numSkusValidBux = getWordCount(%skusValidBux);
    %itemsStr = %numSkus == 1 ? "this item" : "these";
    %pointsTotal = Inventory::getTotalPrice("vPoints", %skus);
    %buxTotal = Inventory::getTotalPrice("vBux", %skus);
    if ((%pointsTotal == 0) && (%numSkusValidPoints > 0))
    {
        eval(%cbBux);
        return ;
    }
    if ((%buxTotal == 0) && (%numSkusValidBux > 0))
    {
        eval(%cbPoints);
        return ;
    }
    %pointsTotal = "   " @ %pointsTotal;
    %buxTotal = "   " @ %buxTotal;
    %mbTitle = "Choose Currency";
    %mbBody = "Do you want to buy " @ %itemsStr @ "<br>with vPoints or with vBux?";
    %mbPointsNote = "";
    %mbBuxNote = "";
    %mbButtons = %pointsTotal TAB %buxTotal TAB "Cancel";
    %mbCBPoints = %cbPoints;
    %mbCBBux = %cbBux;
    if ((%numSkusValidPoints < %numSkus) && (%numSkusValidBux < %numSkus))
    {
        if (%numSkus == 1)
        {
            %mbTitle = "Can\'t Buy This Item";
            %mbBody = "This item is not currently available for purchase.";
            %mbButtons = "OK";
            %mbCBPoints = "";
            %mbCBBux = "";
        }
        else
        {
            %mbTitle = "Can\'t Buy All Items";
            %mbBody = "In order to purchase all items in your cart at once, they must <spush><b>all<spop> be available for either vPoints or vBux (or both!).<br><br>You can purchase the items in your cart individually, or you can remove some items and try again.";
            %mbButtons = "OK";
            %mbCBPoints = "";
            %mbCBBux = "";
        }
    }
    else
    {
        if (%numSkusValidPoints < %numSkus)
        {
            %mbTitle = "Confirm Currency";
            %mbBody = "Do you want to buy " @ %itemsStr @ " with vBux?";
            if (%numSkus > 1)
            {
                %mbPointsNote = "<br><br>(Some or all are not available for vPoints.)";
            }
            else
            {
                %mbPointsNote = "<br><br>(This item is not available for vPoints.)";
            }
            %mbButtons = %buxTotal TAB "Cancel";
            %mbCBPoints = "";
        }
        else
        {
            if (%numSkusValidBux < %numSkus)
            {
                %mbTitle = "Confirm Currency";
                %mbBody = "Do you want to buy " @ %itemsStr @ " with vPoints?";
                if (%numSkus > 1)
                {
                    %mbBuxNote = "<br><br>(Some or all are not available for vBux.)";
                }
                else
                {
                    %mbBuxNote = "<br><br>(This item is not available for vBux.)";
                }
                %mbButtons = %pointsTotal TAB "Cancel";
                %mbCBBux = "";
            }
        }
    }
    %dialog = MessageBoxCustom(%mbTitle, %mbBody @ %mbPointsNote @ %mbBuxNote, %mbButtons);
    %buttonIndex = 0;
    if (!(%mbCBPoints $= ""))
    {
        %dialog.callback[%buttonIndex] = %mbCBPoints;
        %buttonIndex = %buttonIndex + 1;
    }
    if (!(%mbCBBux $= ""))
    {
        %dialog.callback[%buttonIndex] = %mbCBBux;
        %buttonIndex = %buttonIndex + 1;
    }
    %dialog.callback[%buttonIndex] = %cbCancel;
    return %dialog;
}
function ClosetGui::doInsufficientVPoints()
{
    MessageBoxOK("Not Enough vPoints", "You do not have enough vPoints to purchase all the items in your shopping cart.  Click <a:" @ $Net::HelpURL_VPoints @ ">here</a> for more information about earning vPoints.", "");
    return ;
}
function ClosetGui::doInsufficientVBux()
{
    MessageBoxOK("Not Enough vBux", "You do not have enough vBux to purchase all the items in your shopping cart.  Click <a:" @ $Net::AddFundsURL @ ">here</a> to refill your account.", "");
    return ;
}
function ClosetGui::purchaseSkusVPoints(%this, %skus)
{
    %numSkus = getWordCount(%skus);
    %skus = Inventory::filterSkusByValidPrice("vPoints", %skus);
    %numValidSkus = getWordCount(%skus);
    if (%numValidSkus == 0)
    {
        if (%numSkus == 1)
        {
            MessageBoxOK("Not Available", "This item is not available for vPoints.", "");
        }
        else
        {
            MessageBoxOK("Not Available", "These items are not available for vPoints.", "");
        }
        return ;
    }
    %totalPrice = Inventory::getTotalPrice("vPoints", %skus);
    if (%totalPrice > $Player::VPoints)
    {
        %this.doInsufficientVPoints();
        return ;
    }
    %desc = "these " @ %numValidSkus @ " items";
    if (%numValidSkus == 1)
    {
        %si = SkuManager.findBySku(%skus);
        if (!isObject(%si))
        {
            return ;
        }
        %desc = %si.descShrt;
    }
    %vpointsString = %totalPrice == 1 ? "vPoint" : "vPoints";
    %note = "";
    if (%numValidSkus > %numSkus)
    {
        %diff = %numSkus - %numValidSkus;
        %itemsString = %diff == 1 ? "item is" : "items are";
        %note = "<br><br>Note: " @ %diff SPC %itemsString @ " not available for vPoints.";
    }
    %msg = "Do you wish to purchase " @ %desc @ " for " @ %totalPrice @ " " @ %vpointsString @ "?" @ %note;
    %cmd = "ClosetGui.purchaseSkusReally(\"" @ %skus @ "\", \"vPoints\");";
    MessageBoxOkCancel("Confirm Purchase", %msg, %cmd, "");
    return ;
}
function ClosetGui::purchaseSkusVBux(%this, %skus)
{
    %numSkus = getWordCount(%skus);
    %skus = Inventory::filterSkusByValidPrice("vBux", %skus);
    %numValidSkus = getWordCount(%skus);
    if (%numValidSkus == 0)
    {
        if (%numSkus == 1)
        {
            MessageBoxOK("Not Available", "This item is not available for vBux.", "");
        }
        else
        {
            MessageBoxOK("Not Available", "These items are not available for vBux.", "");
        }
        return ;
    }
    %totalPrice = Inventory::getTotalPrice("vBux", %skus);
    if (%totalPrice > $Player::VBux)
    {
        %this.doInsufficientVBux();
        return ;
    }
    %desc = "these " @ %numValidSkus @ " items";
    if (%numValidSkus == 1)
    {
        %si = SkuManager.findBySku(%skus);
        if (!isObject(%si))
        {
            return ;
        }
        %desc = %si.descShrt;
    }
    %note = "";
    if (%numValidSkus > %numSkus)
    {
        %diff = %numSkus - %numValidSkus;
        %itemsString = %diff == 1 ? "item is" : "items are";
        %note = "<br><br>Note: " @ %diff SPC %itemsString @ " not available for vBux.";
    }
    %msg = "Do you wish to purchase " @ %desc @ " for " @ %totalPrice @ " vBux?" @ %note;
    %cmd = "ClosetGui.purchaseSkusReally(\"" @ %skus @ "\", \"vBux\");";
    MessageBoxOkCancel("Confirm Purchase", %msg, %cmd, "");
    return ;
}
function ClosetGui::doCheckout(%this)
{
    %this.purchaseSkus(StoreShoppingList.getSkus());
    return ;
}
function ClosetGui::purchaseSkusReally(%this, %skus, %currency)
{
    %array = new Array();
    %n = getWordCount(%skus) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%skus, %n);
        %array.push_front(%sku, 1);
        %n = %n - 1;
    }
    %request = sendRequest_PurchaseInventory($Player::Name, %array, %currency, $gCurrentStoreName, "closet_onDoneOrErrorCallback_PurchaseInventory");
    %request.currency = %currency;
    %request.callbackData = %this;
    %request.timedOutAlready = 0;
    %array.delete();
    StoreShoppingBag.waitIcon.setVisible(1);
    StoreShoppingBag.waitIcon.start();
    %this.numberOfPurchasesAwaitingCompletion = %this.numberOfPurchasesAwaitingCompletion + 1;
    %tab = ClosetTabs.getTabWithName("SHOPS");
    %tab.doneButton.setActive(!%this.isWaitingForPurchaseCompletion());
    %tab.cancelButton.setActive(!%this.isWaitingForPurchaseCompletion());
    %this.checkoutPopup = MessageBoxOK($MsgCat::closet["MSG-PROCESSING-PURCHASE-TITLE"], $MsgCat::closet["MSG-PROCESSING-PURCHASE"], "");
    %this.schedule(30000, purchaseSkusRequestTimedOut, %request);
    return ;
}
function ClosetGui::purchaseSkusRequestTimedOut(%this, %request)
{
    if (!isObject(%request) && (%request.statusCode() $= ""))
    {
        return ;
    }
    %request.timedOutAlready = 1;
    %this.numberOfPurchasesPastTimeout = %this.numberOfPurchasesPastTimeout + 1;
    %tab = ClosetTabs.getTabWithName("SHOPS");
    %tab.doneButton.setActive(!%this.isWaitingForPurchaseCompletion());
    %tab.cancelButton.setActive(!%this.isWaitingForPurchaseCompletion());
    %this.processingTimeoutPopup = MessageBoxOK($MsgCat::closet["MSG-PROCESSING-TIMEOUT-TITLE"], $MsgCat::closet["MSG-PROCESSING-TIMEOUT"], "");
    return ;
}
function ClosetGui::isWaitingForPurchaseCompletion(%this)
{
    return %this.numberOfPurchasesAwaitingCompletion > %this.numberOfPurchasesPastTimeout;
}
function closet_onDoneOrErrorCallback_PurchaseInventory(%request)
{
    %request.callbackData.onDoneOrErrorCallback_PurchaseInventory(%request);
    return ;
}
function ClosetGui::onDoneOrErrorCallback_PurchaseInventory(%this, %request)
{
    StoreShoppingBag.waitIcon.stop();
    StoreShoppingBag.waitIcon.setVisible(0);
    %this.numberOfPurchasesAwaitingCompletion = %this.numberOfPurchasesAwaitingCompletion - 1;
    if (%request.timedOutAlready)
    {
        %this.numberOfPurchasesPastTimeout = %this.numberOfPurchasesPastTimeout - 1;
    }
    %tab = ClosetTabs.getTabWithName("SHOPS");
    %tab.doneButton.setActive(!%this.isWaitingForPurchaseCompletion());
    %tab.cancelButton.setActive(!%this.isWaitingForPurchaseCompletion());
    if (isObject(%this.checkoutPopup))
    {
        %this.checkoutPopup.close();
    }
    if (isObject(%this.processingTimeoutPopup))
    {
        %this.processingTimeoutPopup.close();
    }
    if (!isObject(%request))
    {
        error(getScopeName() SPC "- no request! this may be because we didn\'t send it in alpha 1");
        return ;
    }
    %skuResults["pass"] = "";
    %skuResults["NotForSale"] = "";
    %skuResults["OutOfStock"] = "";
    %skuResults["NoUsageRights"] = "";
    %skuResults["InsufficientFunds"] = "";
    %n = %request.getValue("itemsCount") - 1;
    while (%n >= 0)
    {
        %sku = %request.getValue("items" @ %n @ ".sku");
        %validationResults = %request.getValue("items" @ %n @ ".validationResults");
        %m = getFieldCount(%validationResults) - 1;
        while (%m >= 0)
        {
            %validationResult = getField(%validationResults, %m);
            %skuResults[%validationResult] = %skuResults[%validationResult] @ %sku @ " ";
            %m = %m - 1;
        }
        %n = %n - 1;
    }
    if (!%request.checkSuccess())
    {
        %errorCode = %request.getValue("errorCode");
        if (%errorCode $= "staleInventory")
        {
            %request = sendRequest_GetStoreInventory($Player::Name, $gCurrentStoreName, "OnGotDoneOrError_GetStoreInventory");
            %request.shoppingCartSkus = StoreShoppingList.getSkus();
            StoreShoppingList.clear();
        }
        else
        {
            if (%errorCode $= "insufficientTotalFunds")
            {
                %msgName = %request.currency $= "vpoints" ? "E-NO-VPOINTS" : "E-NO-VBUX";
                MessageBoxOK($MsgCat::commerce["E-TITLE"], $MsgCat::commerce[%msgName], "");
            }
            else
            {
                if (%errorCode $= "unacquirableItems")
                {
                    if (!(%skuResults["OutOfStock"] $= ""))
                    {
                        MessageBoxYesNo($MsgCat::commerce["E-TITLE"], $MsgCat::commerce["E-SOLDOUT"], "StoreShoppingList.removeSkus(\"" @ %skuResults["OutOfStock"] @ "\");", "");
                    }
                    else
                    {
                        MessageBoxOK($MsgCat::commerce["E-TITLE"], $MsgCat::commerce["E-UNKNOWN"], "");
                    }
                }
                else
                {
                    MessageBoxOK($MsgCat::commerce["E-TITLE"], $MsgCat::commerce["E-UNKNOWN"], "");
                }
            }
        }
    }
    %this.handleAnyPurchasedSkus(%skuResults["pass"], %request.timedOutAlready);
    return ;
}
function ClosetGui::handleAnyPurchasedSkus(%this, %skulist, %delayed)
{
    %skusToFlatten = "";
    %skusPurchased = %skulist;
    %n = getWordCount(%skulist) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%skulist, %n);
        if (findWord($Player::inventory, %sku) == -1)
        {
            $Player::inventory = %sku SPC $Player::inventory;
        }
        else
        {
            error(getScopeName() SPC "- already have SKU:" SPC %sku);
        }
        if ($gStoreItemsQty[%sku] > 0)
        {
            $gStoreItemsQty[%sku] = $gStoreItemsQty[%sku] - 1;
        }
        if (findWord($StoreSkusLayer, %sku) != -1)
        {
            %skusToFlatten = %skusToFlatten SPC %sku;
        }
        %n = %n - 1;
    }
    if (!(%skulist $= ""))
    {
        %callback = "StoreShoppingList.removeSkus(\"" @ %skusPurchased @ "\");";
        if (%delayed)
        {
            MessageBoxOK("Purchase Complete", $MsgCat::commerce["S-PURCHASE-DELAYED"], %callback);
        }
        else
        {
            MessageBoxOK("Purchase Complete", $MsgCat::commerce["S-PURCHASE"], %callback);
        }
    }
    if (!(%skusToFlatten $= ""))
    {
        %skusToFlatten = trim(%skusToFlatten);
        %newStoreSkus = "";
        %n = getWordCount($StoreSkusLayer) - 1;
        while (%n >= 0)
        {
            %sku = getWord($StoreSkusLayer, %n);
            if (!hasWord(%skusToFlatten, %sku))
            {
                %newStoreSkus = %newStoreSkus SPC %sku;
            }
            %n = %n - 1;
        }
        $StoreSkusLayer = trim(%newStoreSkus);
        %skusToFlattenClothing = SkuManager.filterSkusForClothing(%skusToFlatten);
        %skusToFlattenBody = SkuManager.filterSkusForBody(%skusToFlatten);
        $ClosetSkusOutfit[$ClosetOutfitName] = SkuManager.overlaySkus($ClosetSkusOutfit[$ClosetOutfitName], %skusToFlattenClothing) ;
        $ClosetSkusBody = SkuManager.overlaySkus($ClosetSkusBody, %skusToFlattenBody);
    }
    StoreItemsFrame.update();
    return ;
}
function CheckoutRequest::onClosed(%this)
{
    return ;
}
function CheckoutRequest::onError(%this, %unused, %unused)
{
    StoreShoppingBag.waitIcon.stop();
    StoreShoppingBag.waitIcon.setVisible(0);
    if (isObject(ClosetGui.checkoutPopup))
    {
        ClosetGui.checkoutPopup.close();
    }
    MessageBoxOK("Connection Error", $MsgCat::network["E-SERVER-CONNECT"], "");
    %this.onClosed();
    return ;
}
function CheckoutRequest::onDone(%this)
{
    log("network", "debug", getScopeName() SPC "- url =" SPC %this.getURL());
    StoreShoppingBag.waitIcon.stop();
    StoreShoppingBag.waitIcon.setVisible(0);
    if (isObject(ClosetGui.checkoutPopup))
    {
        ClosetGui.checkoutPopup.close();
    }
    %status = findRequestStatus(%this);
    %ownsAlready = 0;
    %buyFailedInsufVBux = 0;
    %buyFailedInsufVPoints = 0;
    %buyFailed = 0;
    if (%status $= "connect-failed")
    {
        MessageBoxOK("Could not connect", "Could not connect to " @ $ETS::AppName @ " servers.  " @ $MsgCat::network["H-SYS-DOWN"] @ $MsgCat::network["H-SEE-FORUMS"], "");
    }
    else
    {
        if (%status $= "fail")
        {
            MessageBoxOK("Error With Account Data", "There was an error with your request.  If you continue to see this error, try logging out and logging back in again.", "");
        }
        else
        {
            if (%status $= "success")
            {
                %skusToFlatten = "";
                %skusSoldOut = "";
                %skusPurchased = "";
                %skusAborted = "";
                %i = 1;
                while (1)
                {
                    %line = %this.getValue("sku" @ %i);
                    if (%line $= "")
                    {
                        continue;
                    }
                    %sku = getField(%line, 0);
                    %result = getField(%line, 1);
                    if (%result $= "buy_ok")
                    {
                        %skusPurchased = %skusPurchased SPC %sku;
                        if (findWord($Player::inventory, %sku) == -1)
                        {
                            $Player::inventory = %sku SPC $Player::inventory;
                        }
                        else
                        {
                            error(getScopeName() SPC "- already have SKU:" SPC %sku);
                        }
                        if ($gStoreItemsQty[%sku] > 0)
                        {
                            $gStoreItemsQty[%sku] = $gStoreItemsQty[%sku] - 1;
                        }
                        if (findWord($StoreSkusLayer, %sku) != -1)
                        {
                            %skusToFlatten = %skusToFlatten SPC %sku;
                        }
                    }
                    else
                    {
                        if (%result $= "buy_aborted")
                        {
                            %skusAborted = %skusAborted SPC %sku;
                        }
                        else
                        {
                            if (%result $= "buy_owns_already")
                            {
                                %ownsAlready = 1;
                            }
                            else
                            {
                                if (%result $= "buy_failed_insufficient_vbux")
                                {
                                    %buyFailedInsufVBux = 1;
                                }
                                else
                                {
                                    if (%result $= "buy_failed_insufficient_vpoints")
                                    {
                                        %buyFailedInsufVPoints = 1;
                                    }
                                    else
                                    {
                                        if (%result $= "buy_failed_sold_out")
                                        {
                                            %skusSoldOut = %skusSoldOut SPC %sku;
                                        }
                                        else
                                        {
                                            %buyFailed = 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    %i = %i + 1;
                }
                %msg = "";
                if (%ownsAlready)
                {
                    %msg = %msg @ $MsgCat::commerce["E-ALREADYOWN"] @ "\n\n";
                }
                if (%buyFailedInsufVBux)
                {
                    %msg = %msg @ $MsgCat::commerce["E-NO-VBUX"] @ "\n\n";
                }
                if (%buyFailedInsufVPoints)
                {
                    %msg = %msg @ $MsgCat::commerce["E-NO-VPOINTS"] @ "\n\n";
                }
                if (%buyFailed)
                {
                    %msg = %msg @ $MsgCat::commerce["F-PURCHASE"] @ "\n\n";
                }
                if (!(%skusAborted $= ""))
                {
                    %msg = %msg @ $MsgCat::commerce["E-ABORTED"] @ "\n\n";
                }
                if (!(%msg $= ""))
                {
                    MessageBoxOK("Notice", %msg, "");
                }
                if (!(%skusSoldOut $= ""))
                {
                    MessageBoxYesNo("Sold Out", $MsgCat::commerce["E-SOLDOUT"], "StoreShoppingList.removeSkus(\"" @ %skusSoldOut @ "\");", "");
                }
                if (!(%skusPurchased $= ""))
                {
                    MessageBoxOK("Purchase Complete", $MsgCat::commerce["S-PURCHASE"], "StoreShoppingList.removeSkus(\"" @ %skusPurchased @ "\");");
                }
                if (!(%skusToFlatten $= ""))
                {
                    %skusToFlatten = trim(%skusToFlatten);
                    %newStoreSkus = "";
                    %i = 0;
                    while (%i < getWordCount($StoreSkusLayer))
                    {
                        %sku = getWord($StoreSkusLayer, %i);
                        if (findWord(%skusToFlatten, %sku) == -1)
                        {
                            %newStoreSkus = %newStoreSkus SPC %sku;
                        }
                        %i = %i + 1;
                    }
                    $StoreSkusLayer = trim(%newStoreSkus);
                    %skusToFlattenClothing = SkuManager.filterSkusForClothing(%skusToFlatten);
                    %skusToFlattenBody = SkuManager.filterSkusForBody(%skusToFlatten);
                    $ClosetSkusOutfit[$ClosetOutfitName] = SkuManager.overlaySkus($ClosetSkusOutfit[$ClosetOutfitName], %skusToFlattenClothing) ;
                    $ClosetSkusBody = SkuManager.overlaySkus($ClosetSkusBody, %skusToFlattenBody);
                }
                StoreItemsFrame.update();
            }
        }
    }
    %this.onClosed();
    return ;
}
function ClosetGui::selectGenre(%this, %val)
{
    $UserPref::Player::Genre = %val;
    %anim = $gClosetStanceEmotes[getRandom(0, $gClosetStanceEmotesNum - 1)];
    %triesLeft = 10;
    while (%anim $= $gClosetStanceEmotesLast)
    {
        %anim = $gClosetStanceEmotes[getRandom(0, $gClosetStanceEmotesNum - 1)];
        %triesLeft = %triesLeft - 1;
    }
    $gClosetStanceEmotesLast = %anim;
    $player.playAnim($player.getGender() @ %val @ %anim);
    return %triesLeft;
}
function ClosetGui::updateVisibleAvatar(%this)
{
    %merged = $ClosetSkusBody SPC $ClosetSkusOutfit[$ClosetOutfitName];
    if (ClosetTabs.getCurrentTab().name $= "SHOPS")
    {
        %merged = SkuManager.overlaySkus($ClosetSkusBody SPC $ClosetSkusOutfit[$ClosetOutfitName], $StoreSkusLayer);
    }
    else
    {
        if (ClosetTabs.getCurrentTab().name $= "CLOSET")
        {
            ClosetWhatYoureWearingList.refresh($ClosetSkusOutfit[$ClosetOutfitName]);
        }
        else
        {
            if (ClosetTabs.getCurrentTab().name $= "MY DESIGNS")
            {
                %merged = SkuManager.overlaySkus($ClosetSkusBody SPC $ClosetSkusOutfit[$ClosetOutfitName], $gSkusMyShopLayer);
                ClosetWhatYoureWearingList.refresh($gSkusMyShopLayer);
            }
        }
    }
    ClosetMainObjectView.setSkus(%merged);
    %snapTab = ClosetTabs.getTabWithName("SNAPSHOT");
    if (%snapTab && isObject(%snapTab.objView))
    {
        %snapTab.objView.setSkus(%merged);
    }
    %badge = SkuManager.filterSkusDrwr(%merged, "badges");
    %si = SkuManager.findBySku(%badge);
    %bitmap = "";
    if (isObject(%si))
    {
        %bitmap = %si.getBitmapPath();
    }
    ClosetMainBadgeView.setBitmap(%bitmap);
    if (isObject(ClosetStaffPanel))
    {
        ClosetStaffPanel.updateSkus();
    }
    return ;
}
function ClosetGui::toggleSku(%this, %sku)
{
    if (ClosetTabs.getCurrentTab().name $= "SHOPS")
    {
        ClosetGUI_ToggleSku_Shops(%sku);
    }
    else
    {
        if (ClosetTabs.getCurrentTab().name $= "CLOSET")
        {
            ClosetGUI_ToggleSku_Closet(%sku);
        }
        else
        {
            if (ClosetTabs.getCurrentTab().name $= "BODY")
            {
                ClosetGUI_ToggleSku_Body(%sku);
            }
            else
            {
                if (ClosetTabs.getCurrentTab().name $= "SNAPSHOT")
                {
                    ClosetGUI_ToggleSku_Snapshot(%sku);
                }
                else
                {
                    if (ClosetTabs.getCurrentTab().name $= "MY DESIGNS")
                    {
                        ClosetGUI_ToggleSku_MyShop(%sku);
                    }
                    else
                    {
                        error(getScopeName() SPC "- unknown tab:" SPC ClosetTabs.getCurrentTab().name SPC getTrace());
                        return ;
                    }
                }
            }
        }
    }
    ClosetGui.updateVisibleAvatar();
    ClosetMainObjectView.zoomToSKU(%sku);
    %thumbnails = ClosetTabs.getCurrentTab().thumbnails;
    if (isObject(%thumbnails))
    {
        %thumbnails.setSelectedThumbs();
        %count = %thumbnails.getCount();
        %i = 0;
        while (%i < %count)
        {
            %cell = %thumbnails.getObject(%i);
            %thumbnails.setCellSkus(%cell, %cell.sku);
            %i = %i + 1;
        }
    }
}

function ClosetGui::doArrow(%this, %dx, %dy)
{
    if (ClosetTabs.getCurrentTab().name $= "SNAPSHOT")
    {
        ProfileObjectView.moveBy(%dx, -%dy);
    }
    return ;
}
function ClosetLink::onURL(%this, %url)
{
    if (getWord(%url, 0) $= "gamelink")
    {
        %url = getWords(%url, 1);
    }
    if (getWord(%url, 0) $= "SAVE_OUTFIT")
    {
        ClosetMyOutfitsFrame.saveOrCancel();
    }
    else
    {
        if (getWord(%url, 0) $= "DONE")
        {
            ClosetGui.close(0);
        }
        else
        {
            if (getWord(%url, 0) $= "CANCEL")
            {
                ClosetGui.close(1);
            }
            else
            {
                if (getWord(%url, 0) $= "TOGGLE_SKU")
                {
                    ClosetGui.toggleSku(getWord(%url, 1));
                }
            }
        }
    }
    return ;
}
function ClosetItemsScroll::getIndexForSku(%this, %sku)
{
    %thumbnails = %this.thumbnails;
    %count = %thumbnails.getCount();
    %i = 0;
    while (%i < %count)
    {
        if (%thumbnails.getObject(%i).sku == %sku)
        {
            return %i;
        }
        %i = %i + 1;
    }
    return -1;
}
function ClosetItemsScroll::scrollToSku(%this, %sku)
{
    %thumbnails = %this.thumbnails;
    %idx = %this.getIndexForSku(%sku);
    if (%idx < 0)
    {
        if (ClosetTabs.getCurrentTab().name $= "CLOSET")
        {
            ClosetItemsFrame.brand = ClosetBrandPopup.getTextById(0);
            ClosetItemsFrame.category = ClosetItemPopup.getTextById(0);
            ClosetItemsFrame.update();
            ClosetBrandPopup.SetSelected(0);
            ClosetItemPopup.SetSelected(0);
            %idx = %this.getIndexForSku(%sku);
        }
        else
        {
            if (ClosetTabs.getCurrentTab().name $= "SHOPS")
            {
                if (StoreCategoryPopup.GetSelected() != 0)
                {
                    StoreCategoryPopup.SetSelected(0);
                }
                %idx = %this.getIndexForSku(%sku);
            }
        }
    }
    if (%idx < 0)
    {
        return ;
    }
    %row = mFloor(%idx / %thumbnails.numRowsOrCols);
    %col = %idx % %thumbnails.numRowsOrCols;
    %thumbnails.hiliteCell(%col, %row);
    %this.scrollToCellIndex(%idx);
    ClosetMainObjectView.zoomToSKU(%sku);
    return ;
}
function ClosetItemsScroll::scrollToCell(%this, %cell)
{
    %thumbnails = %this.thumbnails;
    %cellIdx = %thumbnails.getObjectIndex(%cell);
    %this.scrollToCellIndex(%cellIdx);
    return ;
}
function ClosetItemsScroll::scrollToCellIndex(%this, %cellIdx)
{
    %thumbnails = %this.thumbnails;
    %cellHeight = getWord(%thumbnails.childrenExtent, 1) + %thumbnails.spacing;
    %ypos = 1 - getWord(%thumbnails.getPosition(), 1);
    %closestRow = mFloor((%ypos / %cellHeight) + 0.5);
    %targetRow = mFloor(%cellIdx / 4);
    if (%cellIdx < 0)
    {
        %targetRow = %closestRow;
    }
    if (%targetRow >= (%closestRow + 1))
    {
        %thumbnails.getParent().scrollTo(0, %cellHeight * (%targetRow - 1));
    }
    else
    {
        %thumbnails.getParent().scrollTo(0, %cellHeight * %targetRow);
    }
    return ;
}
function ClosetItemsScroll::onMouseUp(%this)
{
    %this.scrollToCellIndex(-1);
    return ;
}
function ClosetItemsScroll::onScroll(%this)
{
    ClosetTabs.updateRangeText();
    return ;
}
function checkOutfitCorruption(%checkClosetVariables)
{
    if (isObject($player))
    {
        %plyrGendr = $player.getGender();
    }
    else
    {
        %plyrGendr = $UserPref::Player::gender;
    }
    %outfitNames = $Player::HangerNames[%plyrGendr];
    %numOutfitNames = getWordCount(%outfitNames);
    %numOutfitNamesBroken = 0;
    %outfitsCorrupted = 0;
    %noCurrentOutfit = 0;
    %noClosetOutfitName = 0;
    %playerObjNullInCloset = 0;
    %errMsg = "";
    if (!isObject($player) && %checkClosetVariables)
    {
        %playerObjNullInCloset = 1;
        error(getScopeName() @ "-> player object not available in a closet context - can cause errors ($player.getGender() will return \"\" and foul array indices.)");
        %errMsg = %errMsg SPC "(player obj null in closet, fails $player.getGender)";
    }
    if (%numOutfitNames != $gClosetNumOutfits)
    {
        %numOutfitNamesBroken = 1;
        error(getScopeName() @ "->Number of outfits named in Player::HangerNames for player gender is not equal to $gClosetNumOutfits! Will cause errors!");
        %errMsg = %errMsg SPC "(getWordCount($Player::HangerNames[gender]) != $gClosetNumOutfits)";
    }
    %currentOutfit = $gOutfits.get("currentOutfit");
    if ((%currentOutfit $= "") && (findWord(%outfitNames, %plyrGendr @ %currentOutfit) < 0))
    {
        %noCurrentOutfit = 1;
        error(getScopeName() @ "-> gOutfits->currentOutfit is blank or invalid! should NEVER happen! currentOutfit = \"" @ %currentOutfit @ "\"");
        %errMsg = %errMsg SPC "(gOutfits->currentOutfit = " @ %currentOutfit @ ")";
    }
    if (%numOutfitNamesBroken)
    {
        %max = %numOutfitNames;
    }
    else
    {
        %max = $gClosetNumOutfits;
    }
    if (%checkClosetVariables)
    {
        if (findWord($Player::HangerNames[$player.getGender()], $ClosetOutfitName) < 0)
        {
            error(getScopeName() @ "-> can\'t find $ClosetOutfitName in $Player::HangerNames for this gender! $ClosetOutfitName = \"" @ $ClosetOutfitName @ "\"");
            %errMsg = %errMsg SPC "($ClosetOutfitName = \"" @ $ClosetOutfitName @ "\")";
        }
        %n = %max - 1;
        while (%n >= 0)
        {
            %name = getWord(%outfitNames, %n);
            %curOutfit = outfits_filterSKUList($ClosetSkusOutfit[%name]);
            if (%curOutfit $= "")
            {
                %outfitsCorrupted = %outfitsCorrupted + 1;
            }
            %n = %n - 1;
        }
        if (%outfitsCorrupted > 0)
        {
            error(getScopeName() @ "-> " @ %outfitsCorrupted @ " blank outfits detected!");
            %errMsg = %errMsg SPC "(" @ %outfitsCorrupted @ " blank outfits)";
        }
    }
    if ((((%outfitsCorrupted > 0) || %numOutfitNamesBroken) || %noCurrentOutfit) || %playerObjNullInCloset)
    {
        error(getScopeName() @ "->one or more outfits tests failed. posting trace and doing full debug print. trace=" @ getTrace());
        commandToServer('OutfitsCorruptedOnClient', %errMsg, getTrace());
        outfitsAndInventoryDebugLog();
        return 1;
    }
    return 0;
}
function outfitsCorruptedNotify()
{
    error(getScopeName() @ "->outfit data is corrupted. aborting, notifying user");
    %msg = "Wow, sorry, it looks like your outfits have become corrupted, so we\'re not saving the changes, and we advise you to close and reopen vSide. You can help us fix this problem by posting your console.log on the vSide forums before restarting.\n(Press OK to QUIT). ";
    MessageBoxOkCancel("Outfit Error", %msg, "cleanUpAndQuit();", "");
    return ;
}
function outfitsAndInventoryDebugLog()
{
    warn(getScopeName() @ "->gOutfits:");
    $gOutfits.dumpValues();
    warn("->$Player::HangerNames[$player.getGender]:" @ $Player::HangerNames[$player.getGender()]);
    warn(getScopeName() @ "->player inventory: " @ $Player::inventory);
    return ;
}
function filterOutSkusToHideInCloset(%skus)
{
    if (%skus $= "")
    {
        return %skus;
    }
    if ($gSkusToHideInCloset $= "")
    {
        return %skus;
    }
    %i = getWordCount($gSkusToHideInCloset) - 1;
    while (%i >= 0)
    {
        %skuToHide = getWord($gSkusToHideInCloset, %i);
        %skus = findAndRemoveAllOccurrencesOfWord(%skus, %skuToHide);
        %i = %i - 1;
    }
    return %skus;
}
function ClosetTabs::createFilterWidget(%this)
{
    if (isObject(ClosetFilterContainer))
    {
        ClosetFilterContainer.setVisible(1);
        ClosetFilterField.makeFirstResponder(1);
        return ClosetFilterContainer;
    }
    new GuiControl(ClosetFilterContainer)
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "335 64";
        extent = "148 40";
    };
    ClosetFilterField.makeFirstResponder(1);
    return ClosetFilterContainer;
}
$gClosetFilterFieldTimerID = "";
function ClosetFilterField::OnTextChanged(%this)
{
    cancel($gClosetFilterFieldTimerID);
    $gClosetFilterFieldTimerID = %this.schedule(%this.timeoutMS, "onTimer");
    return ;
}
function ClosetFilterField::OnEnterKey(%this)
{
    %this.refilter();
    return ;
}
function ClosetFilterField::onTimer(%this)
{
    %this.refilter();
    return ;
}
function ClosetFilterField::refilter(%this)
{
    cancel($gClosetFilterFieldTimerID);
    $gClosetFilterFieldTimerID = "";
    %filterText = %this.getValue();
    if (%filterText $= %this.prevFilterText)
    {
        return ;
    }
    %this.prevFilterText = %filterText;
    %tab = ClosetTabs.getCurrentTab();
    if (%tab.name $= "BODY")
    {
        BodyItemsFrame.update();
    }
    else
    {
        if (%tab.name $= "CLOSET")
        {
            ClosetItemsFrame.update();
        }
        else
        {
            if (%tab.name $= "SHOPS")
            {
                StoreItemsFrame.update();
            }
            else
            {
                if (%tab.name $= "SNAPSHOT")
                {
                }
                else
                {
                    if (%tab.name $= "MY DESIGNS")
                    {
                        MyShopItemsFrame.update();
                    }
                }
            }
        }
    }
    return ;
}
function ClosetTabs::createAuthorWidget(%this)
{
    if (isObject(ClosetAuthorContainer))
    {
        ClosetAuthorContainer.setVisible(1);
        return ClosetAuthorContainer;
    }
    new GuiControl(ClosetAuthorContainer)
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "689 84";
        extent = "245 110";
    };
    return ClosetAuthorContainer;
}
function ClosetTabs::updateAuthorWidget(%this, %sku)
{
    if (!isObject(ClosetAuthorContainer))
    {
        return ;
    }
    %si = !(%sku $= "") ? SkuManager.findBySku(%sku) : "";
    %filled = 0;
    if (isObject(%si))
    {
        if (!(%si.author $= ""))
        {
            %filled = 1;
            if (%si.author $= "?")
            {
                ClosetAuthorPicture.setBitmap("platform/client/ui/tgf/tgf_profile_default_" @ $player.getGender());
                ClosetAuthorPicture.modulationColor = "255 255 255 50";
                ClosetAuthorPictureOutline.setVisible(1);
                ClosetAuthorText.setText("<just:right><font:Arial:12><color:00000044><linkcolor:00000066>" @ "oh nos!<br>" @ "we\'ve lost track of who made this!<br>");
            }
            else
            {
                %playerEncoded = urlEncode(stripUnprintables(%si.author));
                %profileURL = $Net::ProfileURL @ %playerEncoded;
                %pictureURL_M = $Net::AvatarURL @ %playerEncoded @ "?size=M";
                %pictureURL_L = $Net::AvatarURL @ %playerEncoded @ "?size=L";
                ClosetAuthorPicture.setBitmap("");
                ClosetAuthorPicture.downloadAndApplyBitmap(%pictureURL_M);
                ClosetAuthorPicture.downloadAndApplyBitmap(%pictureURL_L);
                ClosetAuthorPicture.modulationColor = "255 255 255 255";
                ClosetAuthorPictureOutline.setVisible(1);
                ClosetAuthorText.setText("<just:right><font:Arial:12><color:00000044><linkcolor:00000066>" @ "design by<br><a:" @ %profileURL @ ">" @ %si.author @ "</a>");
            }
        }
        else
        {
            if ((!((%si.brand $= "")) && !((%si.brand $= "new"))) && !((%si.brand $= "vhdtemplate")))
            {
                %fullBrand = $gClosetBrandsExtrnl[%si.brand];
                if (%fullBrand $= "")
                {
                    error(getScopeName() SPC "- unknown brand" SPC %si.brand SPC %sku SPC getTrace());
                }
                else
                {
                    %filled = 1;
                    ClosetAuthorPicture.setBitmap("platform/client/ui/vside_icon_38x38");
                    ClosetAuthorPicture.modulationColor = "255 255 255 20";
                    ClosetAuthorPictureOutline.setVisible(0);
                    ClosetAuthorText.setText("<just:right><font:Arial:12><color:00000044><linkcolor:00000066>" @ "brand:<br>" @ %fullBrand);
                }
            }
        }
    }
    if (!%filled)
    {
        ClosetAuthorPicture.setBitmap("platform/client/ui/vside_icon_38x38");
        ClosetAuthorPicture.modulationColor = "255 255 255 20";
        ClosetAuthorPictureOutline.setVisible(0);
        ClosetAuthorText.setText("");
    }
    return ;
}
function ClosetTabs::createWhatYourWearingPanel(%this)
{
    if (isObject(ClosetWhatYoureWearingPanel))
    {
        return ClosetWhatYoureWearingPanel;
    }
    %whatYoureWearingPanel = new GuiWindowCtrl(ClosetWhatYoureWearingPanel)
    {
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
    %whatYoureWearingScroll = new GuiScrollCtrl()
    {
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
    %whatYoureWearingList = new GuiArray2Ctrl(ClosetWhatYoureWearingList)
    {
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
}
function ClosetMainObjectView::onSystemDragDroppedEvent(%this, %text, %pt)
{
    if (ClosetTabs.getCurrentTab().name $= "BODY")
    {
        error(getScopeName() SPC "- not implemented for" SPC ClosetTabs.getCurrentTab().name SPC getTrace());
    }
    else
    {
        if (ClosetTabs.getCurrentTab().name $= "CLOSET")
        {
            error(getScopeName() SPC "- not implemented for" SPC ClosetTabs.getCurrentTab().name SPC getTrace());
        }
        else
        {
            if (ClosetTabs.getCurrentTab().name $= "SHOPS")
            {
                error(getScopeName() SPC "- not implemented for" SPC ClosetTabs.getCurrentTab().name SPC getTrace());
            }
            else
            {
                if (ClosetTabs.getCurrentTab().name $= "MY DESIGNS")
                {
                    %this.onSystemDragDroppedEvent_MyShop(%text, %pt);
                }
            }
        }
    }
    return ;
}
function ClosetGui_About(%section, %topic)
{
    %title = $MsgCat::closetAbout["TITLE",%section,%topic];
    %body = $MsgCat::closetAbout["BODY",%section,%topic];
    if (%title $= "")
    {
        %title = "about..";
    }
    if (%body $= "")
    {
        error(getScopeName() SPC "- no about body!" SPC %section SPC %topic SPC getTrace());
        return ;
    }
    MessageBoxOK(%title, %body, "");
    return ;
}
