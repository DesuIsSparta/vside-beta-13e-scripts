$TGEOpenMsg = "Looking at the Go Directory";
exec("./TGFTabMainClient.cs");
exec("./TGFTabHotSpotsClient.cs");
exec("./TGFTabFriendsClient.cs");
exec("./TGFTabMapClient.cs");
exec("./TGFTabMyPlacesClient.cs");
function geTGF::ResetAndOpen(%this) {
    1.setTabNeedsRefreshAll(%this);
    if (!(%this.mGeTabs $= "")) {
        "".Maps_filterDestinationsByType(%this.mGeTabs);
    }
    "multi_city".setView(WorldMap);
    "Main".openToTabName(%this);
};
function geTGF::shouldGoOnPlayGui(%this) {
    %ret = %this.loggedIn;
    WorldMap;
    return %ret;
};
function geTGF::adjustAppearanceForContainer(%this, %onPlayGui) {
    if (%onPlayGui) {
        "platform/client/ui/finelines".setBitmap(%this.mGeBackground);
        %this.mGeBackground.wrap = 1;
        %this.mGeBackground.modulationColor = "255 255 255 90";
        %this.mGeWindow.setProfile();
        %this.mGeWindow_BG.modulationColor = TGFSmallWindowProfile @ "255 255 255 255";
    }
    "".setBitmap(%this.mGeBackground);
    %this.mGeWindow.setProfile();
    544.resize(%this.mGeWindow, 0, 0, 960);
    %this.mGeWindow_BG.modulationColor = TGFBigWindowProfile @ "255 255 255 200";
    %this.mGeWindow.resizeWidth = 0;
    %this.mGeWindow.resizeHeight = 0;
    %this.mGeWindow.canMove = %onPlayGui;
    %this.mGeWindow.canClose = %onPlayGui;
    %this.mGeWindow.canMinimize = 0;
    %this.mGeWindow.canMaximize = 0;
    %onPlayGui.setVisible(%this.mGeClose);
};
function geTGF::open(%this) {
    %this.init();
    %this.doreopen = 0;
    %this.updateLocation(GuiTracker);
    setIdle(1, $TGEOpenMsg);
    tgfMapMap.push();
    1.setVisible(%this);
    %this.shouldGoOnPlayGui().adjustAppearanceForContainer(%this);
    if (%this.shouldGoOnPlayGui()) {
        %this.add(PlayGui);
        %this.focusAndRaise(PlayGui);
        getWord(PlayGui, %this.extent, 1).resize(%this, 0, 0, getWord(PlayGui, %this.extent, 0));
        %this.onPlayGui = 1;
        moveMap.pop();
        functionMap.pop();
        pushScreenSize(960, 544, 0, 1, 1);
    }
    %this.onPlayGui = 0;
    %this.setContent(Canvas);
    544.resize(%this, 0, 0, 960);
    mlStyle("<just:right>" @ $Player::Name, "tgfSmallText_large").setText(geTGF, %this.mUserName);
    WorldMap.refresh();
    moveAccountBalanceHud("TGF");
    %this.refreshIfNeeded();
};
function geTGF::reopen(%this) {
    if (!(%this.doreopen $= "")) {
    }
    if ((%this.doreopen == 1.0)) {
        %this.open();
    }
};
function geTGF::openToTabName(%this, %tabName) {
    if (!(%this.isVisible())) {
        %this.open();
    }
    %tabName.selectTabWithName(%this.mGeTabs);
    %this.refreshIfNeeded();
};
function geTGF::toggleToTabName(%this, %tabName) {
    if (%this.isVisible()) {
        %currentTab = %this.mGeTabs.getCurrentTab();
        if ((%currentTab.name $= %tabName)) {
            %this.close();
            return;
        }
    }
    1.setTabNeedsRefresh(%this, %tabName);
    %tabName.openToTabName(%this);
};
function geTGF::closeFully(%this) {
    %this.close();
    if (%this.isVisible()) {
        %this.close();
        0.setVisible(%this);
    }
};
function geTGF::close(%this) {
    if (tgfMapMap.isActive()) {
        tgfMapMap.pop();
    }
    if (geDeetsLayer.isVisible()) {
        eval(geDeetsWindow, %currentTab.closeCommand);
        return;
    }
    1.setTabNeedsRefreshAll(%this);
    if ((%this.getId() == Canvas.getContent())) {
        return 0;
    }
    0.setVisible(%this);
    if (%this.onPlayGui) {
        %this.focusAndRaise(PlayGui);
        popScreenSize();
        moveMap.push();
        functionMap.push();
    }
    setIdle(0);
    moveAccountBalanceHud("PLAYGUI");
    return 1;
};
function geTGF::onGroupRemove(%this) {
    moveAccountBalanceHud("PLAYGUI");
};
function geTGF::toggle(%this) {
    if (%this.isVisible()) {
        return %this.close();
    }
    return %this.open();
};
function geTGF::refreshIfNeeded(%this) {
    %curTabName = geTGF_tabs.getUpcomingTab().name;
    if (%curTabName.getTabNeedsRefresh(%this)) {
        %this.refresh();
    }
};
function geTGF::refresh(%this) {
    %curTabName = geTGF_tabs.getUpcomingTab().name;
    %cmd = "geTGF_Tabs.refreshTab" @ %curTabName @ "();";
    eval(%cmd);
    0.setTabNeedsRefresh(%this, %curTabName);
};
function geTGF::reinit(%this) {
    if (isObject(%this.mGeTabs)) {
        %this.mGeTabs.delete();
    }
    %this.init();
};
function geTGF::init(%this) {
    if (isObject(%this.mGeTabs)) {
        return;
    }
    %this.mGeTabs = new ScriptObject(geTGF_tabs) {
        class = "TabControl";
        tabsAlign = "near";
        tabsOffset = "-2 1";
    };
    if (isObject(MissionCleanup)) {
        %this.mGeTabs.add(MissionCleanup);
    }
    %this.mGeTabContainer.setup(%this.mGeTabs);
    WorldMap.initCityMaps();
    WorldMap.requestMapData();
    "initialize".schedule(WorldMap, 100);
    %this.mGeTabs.Maps_GetApartmentVURL(geTGF);
    1.setDoCallbackOnGroupAddRemove(%this);
};
function geTGF_tabs::setup(%this, %container) {
    if (%this.initialized) {
        return;
    }
    "horizontal".Initialize(%this, %container, "137 45", "", "0 0");
    %tabNames = "";
    %tabNames = %tabNames @ "main" @ " ";
    %tabNames = %tabNames @ "hotspots" @ " ";
    %tabNames = %tabNames @ "friends" @ " ";
    %tabNames = %tabNames @ "map" @ " ";
    %tabNames = %tabNames @ "myplace" @ " ";
    %tabNames[$MsgCat::TGF @ "tooltips_main"][%tooltips @ "main"] = %tabNames[$MsgCat::TGF @ "tooltips_main"];
    %tabNames[$MsgCat::TGF @ "tooltips_main"][%tooltips @ "main"][$MsgCat::TGF @ "tooltips_hotspots"][%tooltips @ "hotspots"] = %tabNames[$MsgCat::TGF @ "tooltips_main"][%tooltips @ "main"][$MsgCat::TGF @ "tooltips_hotspots"];
    %tabNames[$MsgCat::TGF @ "tooltips_main"][%tooltips @ "main"][$MsgCat::TGF @ "tooltips_hotspots"][%tooltips @ "hotspots"][$MsgCat::TGF @ "tooltips_friends"][%tooltips @ "friends"] = %tabNames[$MsgCat::TGF @ "tooltips_main"][%tooltips @ "main"][$MsgCat::TGF @ "tooltips_hotspots"][%tooltips @ "hotspots"][$MsgCat::TGF @ "tooltips_friends"];
    %tabNames[$MsgCat::TGF @ "tooltips_main"][%tooltips @ "main"][$MsgCat::TGF @ "tooltips_hotspots"][%tooltips @ "hotspots"][$MsgCat::TGF @ "tooltips_friends"][%tooltips @ "friends"][$MsgCat::TGF @ "tooltips_map"][%tooltips @ "map"] = %tabNames[$MsgCat::TGF @ "tooltips_main"][%tooltips @ "main"][$MsgCat::TGF @ "tooltips_hotspots"][%tooltips @ "hotspots"][$MsgCat::TGF @ "tooltips_friends"][%tooltips @ "friends"][$MsgCat::TGF @ "tooltips_map"];
    %tabNames[$MsgCat::TGF @ "tooltips_main"][%tooltips @ "main"][$MsgCat::TGF @ "tooltips_hotspots"][%tooltips @ "hotspots"][$MsgCat::TGF @ "tooltips_friends"][%tooltips @ "friends"][$MsgCat::TGF @ "tooltips_map"][%tooltips @ "map"][$MsgCat::TGF @ "tooltips_myplace"][%tooltips @ "myplace"] = %tabNames[$MsgCat::TGF @ "tooltips_main"][%tooltips @ "main"][$MsgCat::TGF @ "tooltips_hotspots"][%tooltips @ "hotspots"][$MsgCat::TGF @ "tooltips_friends"][%tooltips @ "friends"][$MsgCat::TGF @ "tooltips_map"][%tooltips @ "map"][$MsgCat::TGF @ "tooltips_myplace"];
    %num = getWordCount(%tabNames);
    %n = 0;
    while ((%n < %num)) {
        %tabName = getWord(%tabNames, %n);
        if (isDefined("%tooltips" @ %tabName)) {
        }
        %toolTip = "";
        %tabName[%tooltips @ %tabName];
        %tab = %toolTip.newTab(%this, %tabName, "platform/client/buttons/tgf/tgf_tab_" @ %tabName);
        "geTGF_Tab_" @ %tabName.setName(%tab);
        "geTGF_Tab_" @ %tabName.bindClassName(%tab);
        %n = (%n + 1.0);
    }
    %this.update();
    1.setTabNeedsRefreshAll(geTGF);
    "fillTabMap".schedule(%this, 100);
    "main".selectTabWithName(%this);
};
function geTGF::setTabNeedsRefreshAll(%this, %val) {
    %val.setTabNeedsRefresh(%this, "main");
    %val.setTabNeedsRefresh(%this, "hotspots");
    %val.setTabNeedsRefresh(%this, "friends");
    %val.setTabNeedsRefresh(%this, "map");
    %val.setTabNeedsRefresh(%this, "myplace");
};
function geTGF::setTabNeedsRefresh(%this, %tabName, %val) {
    if (!(isObject(geTGF_tabs))) {
        return;
    }
    %tabName.getTabWithName(geTGF_tabs).needsRefresh = %val;
};
function geTGF::getTabNeedsRefresh(%this, %tabName) {
    %val = %tabName.getTabWithName(geTGF_tabs).needsRefresh;
    return %val;
};
function geTGF_tabs::fillTabGeneric(%this, %tab) {
    %tab.setProfile();
    %tab.clear();
};
function geTGF_tabs::onShowTabGeneric(%this) {
    cancel(geTGF, %tabName.getTabWithName(geTGF_tabs).geTGF_Refresh_Schedule);
    1.setActive(geTGF_Refresh);
    1.setVisible(geTGF_Refresh);
};
function geTGF_tabs::onShowOrHideTab(%this, %tabObject, %show) {
    if (!(%show)) {
        return;
    }
    if ((%tabObject.name $= "main")) {
        %this.fillTabMain();
        %this.onShowTabMain();
    }
    if ((%tabObject.name $= "hotspots")) {
        %this.fillTabHotSpots();
        %this.onShowTabHotSpots();
    }
    if ((%tabObject.name $= "friends")) {
        %this.fillTabFriends();
        %this.onShowTabFriends();
    }
    if ((%tabObject.name $= "map")) {
        %this.fillTabMap();
        %this.onShowTabMap();
    }
    if ((%tabObject.name $= "myplace")) {
        %this.fillTabMyPlace();
        %this.onShowTabMyPlace();
    }
    error(getScopeName() @ " " @ "- unknown tab name:" @ " " @ %tabObject.name @ " " @ getTrace());
    geTGF.refreshIfNeeded();
};
function geTGF::onLogin(%this) {
    %this.ResetAndOpen();
};
function geTGF::onLogoutButton(%this) {
    MessageBoxYesNo("Log Out", "<br>Are you sure you want to log out?<br>", %this @ ".logoutReally();", "");
};
function geTGF::logoutReally(%this) {
    logout(0);
};
function geTGF::onRefresh(%this) {
    cancel(%this.geTGF_Refresh_Schedule);
    %this.geTGF_Refresh_Schedule = 1.schedule(geTGF_Refresh, (30.0 * 1000.0), setActive);
    0.setActive(geTGF_Refresh);
    hiliteControl(0);
    %this.refresh();
};
function geTGF::onMyPlace(%this) {
    geTGF_tabs::Maps_clickMyApartment();
};
$gTGF_Deets_Constructed = 0;
function geTGF::constructDeetsWindow(%this, %window, %item) {
    if (!($gTGF_Deets_Constructed)) {
        $gTGF_Deets_Constructed = 1;
        %window.deleteMembers();
        %ctrl = new GuiBitmapButtonCtrl("") {
            profile = 0 @ "GuiButtonProfile";
            horizSizing = "left";
            vertSizing = "bottom";
            position = (getWord(%window.getExtent(), 0) - 17.0) @ " " @ 5;
            extent = "13 13";
            command = geDeetsWindow @ closeCommand;
            bitmap = "platform/client/buttons/close_m";
        };
        %ctrl.add(%window);
        new GuiControl(geTGF_deets_pictureContainer) {
            profile = "ETSNonModalProfile";
            position = "3 3";
            extent = "341 197";
            horizSizing = "right";
            vertSizing = "bottom";
        };.add(%window);
        new GuiMLTextCtrl(geTGF_deets_Title) {
            internalName = new GuiBitmapCtrl(geTGF_deets_featured) {
            bitmap = new GuiBitmapCtrl(geTGF_deets_picture) {
            profile = "ETSNonModalProfile";
            position = "0 0";
            extent = "341 197";
            horizSizing = "width";
            vertSizing = "height";
            fitInParentAlign = 1;
        }; @ "platform/client/ui/tgf/tgf_featured";
            profile = "EtsNonModalProfile";
            extent = "82 19";
            position = (341.0 - 82.0) @ " " @ 0;
            horizSizing = "left";
            vertSizing = "bottom";
        }; @ "";
            profile = "ETSNonModalProfile";
            position = "3 1";
            extent = ((getWord(%window.getExtent(), 0) - 150.0) - 24.0) @ " " @ 21;
            text = mlStyle("Title", "tgfDeets_Title");
            horizSizing = "width";
            vertSizing = "bottom";
            style = "tgfDeets_Title";
            autoDetectLinks = 0;
        };.add(%window);
        new GuiMLTextCtrl(geTGF_deets_subType) {
            internalName = "";
            profile = "ETSNonModalProfile";
            position = ((getWord(%window.getExtent(), 0) - 150.0) - 24.0) @ " " @ 1;
            extent = "150 21";
            horizSizing = "width";
            vertSizing = "bottom";
            style = "tgfDeets_SubType";
        };.add(%window);
        new GuiControl(geTGF_deets_happening) {
            profile = "ETSNonModalProfile";
            position = "0 0";
            extent = "100 100";
            horizSizing = "width";
            vertSizing = "height";
        };.add(%window);
        new GuiControl(geTGF_deets_venue) {
            profile = new GuiBitmapCtrl(geTGF_deets_host_picture) {
            profile = new GuiMLTextCtrl(geTGF_deets_eventText) {
            profile = new GuiMLTextCtrl(geTGF_deets_hostName) {
            profile = "InfoWindowTextProfile";
            position = "3 0";
            extent = "45 45";
            horizSizing = "width";
            vertSizing = "bottom";
            style = "tgfDeets_host_name";
            text = "host:";
        }; @ "InfoWindowTextProfile";
            position = "3 55";
            extent = "97 5";
            horizSizing = "width";
            vertSizing = "bottom";
            style = "tgfDeets_host_name";
            text = "deets:";
            autoDetectLinks = 0;
        }; @ "ETSNonModalProfile";
            position = "50 3";
            extent = "50 50";
            horizSizing = "left";
            vertSizing = "bottom";
        }; @ "ETSNonModalProfile";
            position = "0 0";
            extent = %window.getExtent();
            horizSizing = "width";
            vertSizing = "height";
        };.add(%window, new GuiMLTextCtrl(geTGF_deets_venueText) {
            profile = "InfoWindowTextProfile";
            position = "3 24";
            extent = "10 10";
            horizSizing = "right";
            vertSizing = "bottom";
            style = "tgfDeets_Stats";
        };);
        new GuiControl(geTGF_deets_person) {
            profile = "ETSNonModalProfile";
            position = "0 0";
            extent = %window.getExtent();
            horizSizing = "width";
            vertSizing = "height";
        };.add(%window);
        new GuiMLTextCtrl(geTGF_deets_NavLinks) {
            position = new GuiMLTextCtrl(geTGF_deets_stats_headline) {
            profile = new GuiMLTextCtrl(geTGF_deets_stats) {
            profile = "InfoWindowTextProfile";
            position = "3 24";
            extent = "10 10";
            horizSizing = "right";
            vertSizing = "bottom";
            style = "tgfDeets_Stats";
        }; @ "InfoWindowTextProfile";
            position = "278 147";
            extent = "220 10";
            horizSizing = "right";
            vertSizing = "bottom";
            style = "tgfDeets_Stats";
            autoDetectLinks = 0;
        }; @ (getWord(%window.getExtent(), 0) - 39.0) @ " " @ (getWord(%window.getExtent(), 1) - 18.0);
            extent = "36 18";
            text = "navigation";
            horizSizing = "left";
            vertSizing = "top";
            style = "tgfDeets_NavLinks";
        };.add(%window);
    }
    if ((%window.itemType $= %item.type)) {
        return;
    }
    if ((%item.type $= "happening")) {
        %window.arrangeDeetsWindow_happening(%this);
    }
    if ((%item.type $= "venue")) {
        %window.arrangeDeetsWindow_venue(%this);
    }
    if ((%item.type $= "person")) {
        %window.arrangeDeetsWindow_person(%this);
    }
    error("unknown itemType:" @ " " @ %item.type @ " " @ getTrace());
};
function geTGF::arrangeDeetsWindow_happening(%this, %window) {
    200.resize(geDeetsWindow, 500);
    geDeetsWindow.alignToCenterXY();
    %drop = 21;
    380.arrangeDeetsPicture(%this, %window, %drop, 500);
    %sizX = ((getWord(%window.getExtent(), 0) - getWord(geTGF_deets_pictureContainer.getExtent(), 0)) - 5.0);
    %sizY = getWord(geTGF_deets_pictureContainer.getExtent(), 1);
    %posX = ((getWord(geTGF_deets_pictureContainer.getExtent(), 0) + getWord(geTGF_deets_pictureContainer.getPosition(), 0)) + 2.0);
    %posY = %drop;
    %sizY.resize(geTGF_deets_happening, %posX, %posY, %sizX);
    1.setVisible(geTGF_deets_happening);
    0.setVisible(geTGF_deets_venue);
    0.setVisible(geTGF_deets_person);
    1.setVisible(geTGF_deets_featured);
};
function geTGF::arrangeDeetsWindow_venue(%this, %window) {
    200.resize(geDeetsWindow, 450);
    geDeetsWindow.alignToCenterXY();
    %drop = 21;
    152.arrangeDeetsPicture(%this, %window, %drop, 125);
    %sizX = ((getWord(%window.getExtent(), 0) - getWord(geTGF_deets_pictureContainer.getExtent(), 0)) - 5.0);
    %sizY = getWord(geTGF_deets_pictureContainer.getExtent(), 1);
    %posX = ((getWord(geTGF_deets_pictureContainer.getExtent(), 0) + getWord(geTGF_deets_pictureContainer.getPosition(), 0)) + 2.0);
    %posY = %drop;
    %sizY.resize(geTGF_deets_venueText, %posX, %posY, %sizX);
    0.setVisible(geTGF_deets_happening);
    1.setVisible(geTGF_deets_venue);
    0.setVisible(geTGF_deets_person);
    0.setVisible(geTGF_deets_featured);
};
function geTGF::arrangeDeetsWindow_person(%this, %window) {
    200.resize(geDeetsWindow, 500);
    geDeetsWindow.alignToCenterXY();
    %drop = 21;
    1.arrangeDeetsPicture(%this, %window, %drop, 1);
    %sizX = ((getWord(%window.getExtent(), 0) - getWord(geTGF_deets_pictureContainer.getExtent(), 0)) - 5.0);
    %sizY = getWord(geTGF_deets_pictureContainer.getExtent(), 1);
    %posX = ((getWord(geTGF_deets_pictureContainer.getExtent(), 0) + getWord(geTGF_deets_pictureContainer.getPosition(), 0)) + 2.0);
    %posY = %drop;
    %sizY.resize(geTGF_deets_stats, %posX, %posY, %sizX);
    0.setVisible(geTGF_deets_happening);
    0.setVisible(geTGF_deets_venue);
    1.setVisible(geTGF_deets_person);
    0.setVisible(geTGF_deets_featured);
};
function geTGF::arrangeDeetsPicture(%this, %window, %drop, %aspectW, %aspectH) {
    %sizY = ((getWord(%window.getExtent(), 1) - %drop) - 2.0);
    %sizX = ((%sizY * %aspectW) / %aspectH);
    %posX = 3;
    %posY = %drop;
    %sizY.resize(geTGF_deets_pictureContainer, %posX, %posY, %sizX);
};
function geTGF::fillDetailsContainer(%this, %container, %item) {
    if ((%item.type $= "happening")) {
        %item.fillDetailsContainer_Happening(%this, %container);
    }
    if ((%item.type $= "venue")) {
        %item.fillDetailsContainer_Venue(%this, %container);
    }
    if ((%item.type $= "person")) {
        %item.fillDetailsContainer_Person(%this, %container);
    }
    error(getScopeName() @ " " @ "- unknown type:" @ " " @ %item.type @ " " @ getTrace());
    %item.type.getItemList(%this, %item.listName).currentItem = %item;
    %hasPrevItem = isObject(%item.type.getPrevItem(%this, %item.listName));
    %hasNextItem = isObject(%item.type.getNextItem(%this, %item.listName));
    if (%hasPrevItem) {
    }
    %prevLink = tgfDeets_NavLinkInactive @ mlStyle("<<");
    mlStyle("<a:gamelink prev><<</a>");
    if (%hasNextItem) {
    }
    %nextLink = tgfDeets_NavLinkInactive @ mlStyle(">>");
    mlStyle("<a:gamelink next>>></a>");
    %prevLink @ %nextLink.setText(geTGF_deets_NavLinks);
};
function geTGF::formatOccupancy(%this, %num, %interestingNumberFormat, %unknownString, %noneString) {
    if ((%num == -(1.0))) {
        %ret = %unknownString;
    }
    if ((%num == 0.0)) {
        %ret = %noneString;
    }
    %ret = %interestingNumberFormat @ %num;
    return %ret;
};
function geTGF::getEventTypePostItTagBitmap(%this, %item) {
    %ret = "";
    if (!(%item.eventID $= "")) {
        if ((%item.subType $= "publicLocationEvent")) {
            %ret = "platform/client/ui/tgf/tgf_publicEvent";
        }
        if (%item.featured) {
            %ret = "platform/client/ui/tgf/tgf_featuredEvent";
        }
        %ret = "platform/client/ui/tgf/tgf_event";
    }
    return %ret;
};
function geTGF::fillDetailsContainer_Happening(%this, %container, %item) {
    "".setTextWithStyle(geTGF_deets_Title, %item.headline);
    %profileURL = $Net::ProfileURL @ urlEncode(%item.hostUserName);
    %subTypeText = "";
    if ((%item.subType $= "apt")) {
        %mainPicUrl1 = $Net::BuildDirPhotoURL @ urlEncode(%item.hostUserName) @ "?size=S";
        %mainPicUrl2 = $Net::BuildDirPhotoURL @ urlEncode(%item.hostUserName) @ "?size=L";
        %hostUrl = $Net::AvatarURL @ urlEncode(%item.hostUserName) @ "?size=S";
        %detailsURL = %profileURL;
        %subTypeText = "personal space ";
    }
    if ((%item.subType $= "aptEvent")) {
        %mainPicUrl1 = %item.baseImageURL @ "?size=S";
        %mainPicUrl2 = %item.baseImageURL @ "?size=M";
        %hostUrl = $Net::AvatarURL @ urlEncode(%item.hostUserName) @ "?size=S";
        %detailsURL = $Net::EventDetailURL @ urlEncode(%item.eventID);
        %subTypeText = "event ";
    }
    if ((%item.subType $= "publicLocationEvent")) {
        %mainPicUrl1 = %item.baseImageURL @ "?size=S";
        %mainPicUrl2 = %item.baseImageURL @ "?size=M";
        %hostUrl = $Net::AvatarURL @ urlEncode(%item.hostUserName) @ "?size=S";
        %detailsURL = $Net::EventDetailURL @ urlEncode(%item.eventID);
        %subTypeText = "public event ";
    }
    if ((%item.subType $= "")) {
        error(getScopeName() @ " " @ "- empty happening subtype." @ " " @ "(" @ %item.type @ ")" @ " " @ getTrace());
    }
    error(getScopeName() @ " " @ "- unknown happening subtype:" @ " " @ %item.subType @ " " @ "(" @ %item.type @ ")" @ " " @ getTrace());
    %bitmap = %item.getEventTypePostItTagBitmap(%this);
    %bitmap.setBitmap(geTGF_deets_featured);
    if (%item.featured) {
        %subTypeText = "featured" @ " " @ %subTypeText;
    }
    %subTypeText = "-" @ " " @ %subTypeText;
    %subTypeText.setTextWithStyle(geTGF_deets_subType);
    "platform/client/ui/tgf/tgf_profile_default".setBitmap(geTGF_deets_picture);
    geTGF_deets_picture.fitInParentAsBitmap();
    %mainPicUrl1.downloadAndApplyBitmap(geTGF_deets_picture);
    %mainPicUrl2.downloadAndApplyBitmap(geTGF_deets_picture);
    "platform/client/ui/tgf/tgf_profile_default".setBitmap(geTGF_deets_host_picture);
    %hostUrl.downloadAndApplyBitmap(geTGF_deets_host_picture);
    %text = "<tab:45>";
    %text = %text @ "host" @ "\t" @ ":<just:right>" @ "<spush><b>" @ " <a:gamelink " @ %profileURL @ ">" @ %item.hostUserName @ "</a>" @ "<spop>";
    "".setTextWithStyle(geTGF_deets_hostName, %text);
    %occupancy = "<color:ffffff60>-".formatOccupancy(%this, %item.occupancy, "<b>", "<color:ffffff60>(unknown)");
    %friendOccupancy = "<color:ffffff60>-".formatOccupancy(%this, %item.friendOccupancy, "<b><color:40ff40>", "<color:ffffff60>(unknown)");
    %text = "<tab:45>";
    %text = %text @ "<just:left>" @ "Peeps" @ "\t" @ ": <spush>" @ %occupancy @ "<spop>";
    %text = %text @ "<br><just:left>" @ "Friends" @ "\t" @ ": <spush>" @ %friendOccupancy @ "<spop>";
    %text = %text @ "<br><just:left>" @ "Where" @ "\t" @ ": <spush>" @ DestinationList::GetAreaNameUserFacingName(%item.location_areaName) @ "<spop>";
    %text = %text @ "<br><just:left>" @ "Access" @ "\t" @ ": <spush>" @ %item.accessMode.getUserFacingAccessModeWithIcon(%this) @ "<spop>";
    %text = %text @ "<br>";
    %text = %text @ "<br><just:left>" @ "<spush><b>" @ "<a:gamelink " @ %detailsURL @ ">" @ "More Info" @ "</a>" @ "<spop>";
    %text = %text @ "<just:right>" @ "<spush>" @ mlStyle("<a:gamelink " @ %item.goThereVURL @ ">" @ "<b>Let's Go! " @ "</a>", "tgfDeets_Visit") @ "<spop>";
    "".setTextWithStyle(geTGF_deets_eventText, %text);
};
function geTGF::getUserFacingAccessModeWithIcon(%this, %accessMode) {
    %bitmap = "";
    %text = "";
    if ((%accessMode $= "OPEN")) {
        %bitmap = "";
        %text = "Open";
    }
    if ((%accessMode $= "FRIENDSONLY")) {
        %bitmap = "platform/client/ui/tgf/tgf_heart_white";
        %text = "Friends";
    }
    if ((%accessMode $= "PASSWORDPROTECTED")) {
        %bitmap = "platform/client/ui/tgf/tgf_key_white";
        %text = "Door Code";
    }
    error(getScopeName() @ " " @ "- unknown access mode '" @ %accessMode @ "' -" @ " " @ getTrace());
    %bitmap = "";
    %text = "(?)";
    if (!(%bitmap $= "")) {
        %bitmap = "<bitmap:" @ %bitmap @ "> ";
    }
    return %bitmap @ %text;
};
function geTGF::fillDetailsContainer_Venue(%this, %container, %item) {
    %item.fullName = %item[$gDestinationNames @ %item.codeName];
    %item.areaName = %item[$gDestinationSpaces @ %item.codeName];
    %item.headline = %item[$gDestinationDescsInCloset @ %item.codeName];
    %item.goThereVURL = %item[$gDestinationVurls @ %item.codeName];
    %item.cityName = DestinationList::GetAreaNameUserFacingName(DestinationList::GetAreaNameCity(%item.areaName));
    %whatIsIt = "A ";
    %delim = "";
    %n = (getWordCount(%item[$gDestinationFilters @ %item.codeName]) - 1.0);
    while ((%n >= 0.0)) {
        %whatIsIt = %whatIsIt @ %delim @ getWord(%item[$gDestinationFilters @ %item.codeName], %n);
        %delim = " and a ";
        %n = (%n - 1.0);
    }
    %subTypeText = "- venue ";
    (%n >= 0.0);
    "".setTextWithStyle(geTGF_deets_Title, "<clip:560>" @ %item.fullName);
    %subTypeText.setTextWithStyle(geTGF_deets_subType);
    %bitmapName = DestinationList::getBitmapLocation(%item.codeName);
    %bitmapName.setBitmap(geTGF_deets_picture);
    geTGF_deets_picture.fitInParentAsBitmap();
    %tableSettings = "<tab:67,215>";
    %text = %tableSettings;
    %text = %text @ %whatIsIt;
    %text = %text @ " in <spush><b><a:gamelink MAP_CITY " @ DestinationList::GetAreaNameCity(%item.areaName) @ ">" @ %item.cityName @ "</a><spop>.";
    %text = %text @ "<br>";
    %text = %text @ "<br>";
    %text = %text @ mlStyle(%item.headline, "tgfDeets_Headline");
    %text = %text @ "<br>";
    %text = %text @ "<br>";
    %text = %text @ "<br>";
    %text = %text @ "<br>";
    %text = %text @ "<br>";
    %text = %text @ "<just:right>" @ "<spush>" @ mlStyle("<a:gamelink " @ %item.goThereVURL @ ">" @ "<b>Let's Go! " @ "</a>", "tgfDeets_Visit") @ "<spop>";
    "".setTextWithStyle(geTGF_deets_venueText, %text);
};
function geTGF_deets_venueText::onURL(%this, %url) {
    0.setVisible(geDeetsLayer);
    if ((firstWord(%url) $= "gamelink")) {
    }
    %url = %url;
    restWords(%url);
    %s = firstWord(%url);
    if ((%s $= "MAP_CITY")) {
        %city = restWords(%url);
        "map".openToTabName(geTGF);
        %city.selectCity(WorldMap);
    }
    Parent::onURL(%this, %url);
};
$gTextAllTimeVPointsLink = "<spush><b><linkcolor:ffffff><a:gamelink " @ $Net::HelpURL_VPoints @ ">All-time <bitmap:platform/client/ui/vpoints_14></a><spop>";
function geTGF::fillDetailsContainer_Person(%this, %container, %item) {
    %friend = (%item.relationType $= "friend");
    %friendTag = %friend ? "<color:30dd30><shadowcolor:000080>" : "";
    "".setTextWithStyle(geTGF_deets_Title, %friendTag @ %item.userName);
    %subTypeText = %friend ? "<color:40ee40>- Friend " : "- vSider ";
    %subTypeText.setTextWithStyle(geTGF_deets_subType);
    "platform/client/ui/tgf/tgf_profile_default".setBitmap(geTGF_deets_picture);
    geTGF_deets_picture.fitInParentAsBitmap();
    %url = $Net::AvatarURL @ urlEncode(%item.userName) @ "?size=M";
    %url.downloadAndApplyBitmap(geTGF_deets_picture);
    %url = $Net::AvatarURL @ urlEncode(%item.userName) @ "?size=L";
    %url.downloadAndApplyBitmap(geTGF_deets_picture);
    %tableSettings = "<tab:67,215>";
    %profileURL = $Net::ProfileURL @ urlEncode(%item.userName);
    if ((%item.goThereVURL $= "")) {
        %item.goThereVURL = "vside:/user/" @ %item.userName;
    }
    %readMoreText = "<spush><b><a:gamelink " @ %profileURL @ ">Read More</a><spop>";
    %goThereText = ("<spush><b><a:gamelink " @ %item.goThereVURL @ ">Visit " @ " " @ %item.gender $= "m") ? "him" : "her" @ " now!</a><spop>";
    %goThereText = mlStyle(%goThereText, "tgfDeets_Visit");
    %text = %tableSettings;
    if ((%text @ "Age" @ "\t" @ ": " @ " " @ %item.age $= "")) {
    }
    %text = "(not shown)" @ %item.age;
    if ((%text @ "<br>" @ "Location" @ "\t" @ ": " @ " " @ %item.locationIRL $= "")) {
    }
    %text = "(not shown)" @ %item.locationIRL;
    %text = %text @ "<br>" @ "Level" @ "\t" @ ": " @ respektLevelToNameWithoutArticle(respektScoreToLevel(%item.score));
    %text = %text @ "<br>" @ $gTextAllTimeVPointsLink @ "\t" @ ": " @ %item.score;
    %text = %text @ "<br>" @ "vLocation" @ "\t" @ ": " @ DestinationList::GetAreaNameUserFacingName(%item.currentLocation_areaName);
    %text = %text @ "<br>" @ "Activities" @ "\t" @ ": " @ 5.getActivitiesMLText(getUserActivityMgr(), %item.currentActivities);
    %text = %text @ "<br>" @ "Status" @ "\t" @ ": ";
    %text = %text @ "<br>";
    %text = %text @ "<br>";
    %text = %text @ "<br>" @ "      " @ %readMoreText @ "    |    " @ %goThereText;
    "".setTextWithStyle(geTGF_deets_stats, %text);
    if ((geTGF_deets_stats_headline @ " " @ %item.headline $= "")) {
    }
    "".setTextWithStyle("(none)", %item.headline);
};
function geTGF::DoDetails(%this, %tabName, %item) {
    if ((%tabName $= "main")) {
        %container = %item.main_GetAndOpenDetailsContainer(%this);
    }
    if ((%tabName $= "hotspots")) {
        %container = %item.hotspots_GetAndOpenDetailsContainer(%this);
    }
    if ((%tabName $= "friends")) {
        %container = %item.friends_GetAndOpenDetailsContainer(%this);
    }
    if ((%tabName $= "map")) {
        %container = %item.map_GetAndOpenDetailsContainer(%this);
    }
    if ((%tabName $= "myplace")) {
        %container = %item.myplace_GetAndOpenDetailsContainer(%this);
    }
    error(getScopeName() @ " " @ "- unknown tabName:" @ " " @ %tabName @ " " @ getTrace());
    %itemList = %item.itemList;
    %itemList.currentItem = %item;
    %this.currentItem = %item;
    %item.fillDetailsContainer(%this, %container);
};
function geTGF::getPrevItem(%this, %listName, %type) {
    %itemList = %type.getItemList(%this, %listName);
    %curNdx = %itemList.currentItem.getIndexFromValue(%itemList);
    if ((%curNdx < 0.0)) {
        %curNdx = 0;
    }
    %newNdx = (%curNdx - 1.0);
    if ((%newNdx >= 0.0)) {
    }
    if ((%newNdx < %itemList.count())) {
        %ret = %newNdx.getValue(%itemList);
    }
    %ret = "";
    return %ret;
};
function geTGF::getNextItem(%this, %listName, %type) {
    %itemList = %type.getItemList(%this, %listName);
    %curNdx = %itemList.currentItem.getIndexFromValue(%itemList);
    if ((%curNdx < 0.0)) {
        %curNdx = 0;
    }
    %newNdx = (%curNdx + 1.0);
    if ((%newNdx >= 0.0)) {
    }
    if ((%newNdx < %itemList.count())) {
        %ret = %newNdx.getValue(%itemList);
    }
    %ret = "";
    return %ret;
};
function geTGF_deets_NavLinks::onURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
    }
    %url = %url;
    restWords(%url);
    %s = firstWord(%url);
    if ((%s $= "prev")) {
        %item = %itemList.currentItem.currentItem.type.getPrevItem(geTGF, geTGF, %itemList.currentItem.listName, geTGF);
    }
    if ((%s $= "next")) {
        %item = %itemList.currentItem.currentItem.currentItem.currentItem.type.getNextItem(geTGF, geTGF, %itemList.currentItem.currentItem.currentItem.listName, geTGF);
    }
    if (isObject(%item)) {
        %item.DoDetails(geTGF, geTGF_tabs.getCurrentTab().name);
    }
};
function geTGF::createNewItem(%this, %listName, %type, %id) {
    %itemList = %type.getItemList(%this, %listName);
    %item = new ScriptObject("");;
    0;
    %item.type = %type;
    %item.id = %id;
    %item.listName = %listName;
    %item.push_back(%itemList, %id);
    return %item;
};
function geTGF::clearItemList(%this, %listName, %type) {
    %itemList = %type.getItemList(%this, %listName);
    %n = (%itemList.count() - 1.0);
    while ((%n >= 0.0)) {
        %item = %n.getValue(%itemList);
        %item.delete();
        %n = (%n - 1.0);
    }
    %itemList.empty();
};
function geTGF::getItemList(%this, %listName, %type) {
    if (!(%type.testItemList(%this, %listName))) {
        %this.mainTabItems = 0 @ new Array(""); TAB %listName @ %type;
    }
    return %this.mainTabItems;
};
function geTGF::testItemList(%this, %listName, %type) {
    return isObject(%listName @ %type, %this.mainTabItems);
};
function geTGF::removeItemsWithFieldValueFromList(%this, %listName, %type, %fieldName, %fieldValue) {
    %itemList = %type.getItemList(%this, %listName);
    %n = (%itemList.count() - 1.0);
    while ((%n >= 0.0)) {
        %item = %n.getValue(%itemList);
        %itemFieldValue = %fieldName.getFieldValue(%item);
        if ((%itemFieldValue $= %fieldValue)) {
            %n.erase(%itemList);
            %item.delete();
        }
        %n = (%n - 1.0);
    }
};
function geTGF::removeItemsFromList1WithMatchingItemInList2(%this, %list1Name, %list1Type, %list2Name, %list2Type, %fieldName) {
    %itemList2 = %list2Type.getItemList(%this, %list2Name);
    %n = (%itemList2.count() - 1.0);
    while ((%n >= 0.0)) {
        %item = %n.getValue(%itemList2);
        %fieldValue = %fieldName.getFieldValue(%item);
        %fieldValue.removeItemsWithFieldValueFromList(%this, %list1Name, %list1Type, %fieldName);
        %n = (%n - 1.0);
    }
};
function geTGF::findItem(%this, %listName, %type, %id) {
    %list = %type.getItemList(%this, %listName);
    %list.moveFirst();
    %indx = %id.getIndexFromKey(%list);
    if ((%indx < 0.0)) {
        error(getScopeName() @ " " @ "- item not found:" @ " " @ %type @ " " @ %id @ " " @ getTrace());
        return "";
    }
    return %indx.getValue(%list);
};
function geTGF::dumpItemList(%this, %listName, %type) {
    %itemList = %type.getItemList(%this, %listName);
    %n = (%itemList.count() - 1.0);
    while ((%n >= 0.0)) {
        %item = %n.getValue(%itemList);
        %item.dumpFields();
        %n = (%n - 1.0);
    }
};
