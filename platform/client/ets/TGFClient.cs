$TGEOpenMsg = "Looking at the Go Directory";
exec("./TGFTabMainClient.cs");
exec("./TGFTabHotSpotsClient.cs");
exec("./TGFTabFriendsClient.cs");
exec("./TGFTabMapClient.cs");
exec("./TGFTabMyPlacesClient.cs");
function geTGF::ResetAndOpen(%this) {
    %this.setTabNeedsRefreshAll(1);
    if (!(%this SPC mGeTabs $= "")) {
        mGeTabs.Maps_filterDestinationsByType("");
    }
    "multi_city".setView();
    %this.openToTabName("Main");
};
function geTGF::shouldGoOnPlayGui(%this) {
    %ret = loggedIn;
    WorldMap;
    return %ret;
};
function geTGF::adjustAppearanceForContainer(%this, %onPlayGui) {
    if (%onPlayGui) {
        mGeBackground.setBitmap("platform/client/ui/finelines");
        wrap = %this @ mGeBackground;
        %this @ 1;
        modulationColor = %this @ mGeBackground;
        "255 255 255 90";
        mGeWindow.setProfile();
        modulationColor = %this @ mGeWindow_BG;
        TGFSmallWindowProfile @ "255 255 255 255";
    }
    mGeBackground.setBitmap("");
    mGeWindow.setProfile();
    mGeWindow.resize(0, 0, 960, 544);
    modulationColor = %this @ mGeWindow_BG;
    %this @ "255 255 255 200";
    resizeWidth = %this @ mGeWindow;
    TGFBigWindowProfile @ 0;
    resizeHeight = %this @ mGeWindow;
    %this @ 0;
    canMove = %this @ mGeWindow;
    %this @ %onPlayGui;
    canClose = %this @ mGeWindow;
    %this @ %onPlayGui;
    canMinimize = %this @ mGeWindow;
    0;
    canMaximize = %this @ mGeWindow;
    0;
    mGeClose.setVisible(%onPlayGui);
};
function geTGF::open(%this) {
    %this.init();
    doreopen = 0 @ %this;
    %this.updateLocation();
    setIdle(1, $TGEOpenMsg);
    push();
    %this.setVisible(1);
    %this.adjustAppearanceForContainer(%this.shouldGoOnPlayGui());
    if (%this.shouldGoOnPlayGui()) {
        %this.add();
        %this.focusAndRaise();
        %this.resize(0, 0, getWord(extent, 0), getWord(extent, 1));
        onPlayGui = PlayGui @ 1 @ %this;
        PlayGui;
        pop();
        pop();
        pushScreenSize(960, 544, 0, 1, 1);
    }
    onPlayGui = functionMap @ 0 @ %this;
    moveMap;
    %this.setContent();
    %this.resize(0, 0, 960, 544);
    mUserName.setText(mlStyle(geTGF @ "<just:right>" @ $Player::Name, "tgfSmallText_large"));
    refresh();
    moveAccountBalanceHud("TGF");
    %this.refreshIfNeeded();
};
function geTGF::reopen(%this) {
    if (!(%this SPC doreopen $= "")) {
    }
    if ((%this == doreopen)) {
        %this.open();
    }
};
function geTGF::openToTabName(%this, %tabName) {
    if (!(%this.isVisible())) {
        %this.open();
    }
    mGeTabs.selectTabWithName(%tabName);
    %this.refreshIfNeeded();
};
function geTGF::toggleToTabName(%this, %tabName) {
    if (%this.isVisible()) {
        %currentTab = mGeTabs.getCurrentTab();
        %this;
        if ((%currentTab SPC name $= %tabName)) {
            %this.close();
            return;
        }
    }
    %this.setTabNeedsRefresh(%tabName, 1);
    %this.openToTabName(%tabName);
};
function geTGF::closeFully(%this) {
    %this.close();
    if (%this.isVisible()) {
        %this.close();
        %this.setVisible(0);
    }
};
function geTGF::close(%this) {
    if (isActive()) {
        pop();
    }
    if (isVisible()) {
        eval(closeCommand);
        return geDeetsWindow;
    }
    %this.setTabNeedsRefreshAll(1);
    if ((getContent() == %this.getId())) {
        return 0;
    }
    %this.setVisible(0);
    if (onPlayGui) {
        %this.focusAndRaise();
        popScreenSize();
        push();
        push();
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
    %curTabName = name;
    getUpcomingTab();
    if (%this.getTabNeedsRefresh(%curTabName)) {
        %this.refresh();
    }
};
function geTGF::refresh(%this) {
    %curTabName = name;
    getUpcomingTab();
    %cmd = geTGF_tabs @ "geTGF_Tabs.refreshTab" @ %curTabName @ "();";
    eval(%cmd);
    %this.setTabNeedsRefresh(%curTabName, 0);
};
function geTGF::reinit(%this) {
    if (isObject(mGeTabs)) {
        mGeTabs.delete();
    }
    %this.init();
};
function geTGF::init(%this) {
    if (isObject(mGeTabs)) {
        return %this;
    }
    class = new ScriptObject(geTGF_tabs) @ "TabControl";
    tabsAlign = "near";
    tabsOffset = "-2 1";
    mGeTabs = %this;
    if (isObject()) {
        mGeTabs.add();
    }
    mGeTabs.setup(mGeTabContainer);
    initCityMaps();
    requestMapData();
    100.schedule("initialize");
    mGeTabs.Maps_GetApartmentVURL();
    %this.setDoCallbackOnGroupAddRemove(1);
};
function geTGF_tabs::setup(%this, %container) {
    if (initialized) {
        return %this;
    }
    %this.Initialize(%container, "137 45", "", "0 0", "horizontal");
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
    if ((%num < %n)) {
        %tabName = getWord(%tabNames, %n);
        if (isDefined("%tooltips" @ %tabName)) {
        }
        %toolTip = "";
        %tabName[%tooltips @ %tabName];
        %tab = %this.newTab(%tabName, "platform/client/buttons/tgf/tgf_tab_" @ %tabName, %toolTip);
        %tab.setName("geTGF_Tab_" @ %tabName);
        %tab.bindClassName("geTGF_Tab_" @ %tabName);
        %n = (1.0 + %n);
    }
    %this.update();
    1.setTabNeedsRefreshAll();
    %this.schedule(100, "fillTabMap");
    %this.selectTabWithName("main");
};
function geTGF::setTabNeedsRefreshAll(%this, %val) {
    %this.setTabNeedsRefresh("main", %val);
    %this.setTabNeedsRefresh("hotspots", %val);
    %this.setTabNeedsRefresh("friends", %val);
    %this.setTabNeedsRefresh("map", %val);
    %this.setTabNeedsRefresh("myplace", %val);
};
function geTGF::setTabNeedsRefresh(%this, %tabName, %val) {
    if (!(isObject())) {
        return geTGF_tabs;
    }
    needsRefresh = geTGF_tabs @ %tabName.getTabWithName();
    %val;
};
function geTGF::getTabNeedsRefresh(%this, %tabName) {
    %val = needsRefresh;
    %tabName.getTabWithName();
    return %val;
};
function geTGF_tabs::fillTabGeneric(%this, %tab) {
    %tab.setProfile();
    %tab.clear();
};
function geTGF_tabs::onShowTabGeneric(%this) {
    cancel(geTGF_Refresh_Schedule);
    1.setActive();
    1.setVisible();
};
function geTGF_tabs::onShowOrHideTab(%this, %tabObject, %show) {
    if (!(%show)) {
        return;
    }
    if ((%tabObject SPC name $= "main")) {
        %this.fillTabMain();
        %this.onShowTabMain();
    }
    if ((%tabObject SPC name $= "hotspots")) {
        %this.fillTabHotSpots();
        %this.onShowTabHotSpots();
    }
    if ((%tabObject SPC name $= "friends")) {
        %this.fillTabFriends();
        %this.onShowTabFriends();
    }
    if ((%tabObject SPC name $= "map")) {
        %this.fillTabMap();
        %this.onShowTabMap();
    }
    if ((%tabObject SPC name $= "myplace")) {
        %this.fillTabMyPlace();
        %this.onShowTabMyPlace();
    }
    error(%tabObject @ name @ " " @ getTrace());
    refreshIfNeeded();
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
    cancel(geTGF_Refresh_Schedule);
    geTGF_Refresh_Schedule = setActive @ (1000.0 * 30.0).schedule(1) @ %this;
    geTGF_Refresh;
    0.setActive();
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
        profile = GuiBitmapButtonCtrl @ new ""() @ "GuiButtonProfile";
        0;
        horizSizing = "left";
        vertSizing = "bottom";
        position = (17.0 - getWord(%window.getExtent(), 0)) @ " " @ 5;
        extent = "13 13";
        command = geDeetsWindow @ closeCommand;
        bitmap = "platform/client/buttons/close_m";
        %ctrl = ;
        %window.add(%ctrl);
        profile = new GuiControl(geTGF_deets_pictureContainer) @ "ETSNonModalProfile";
        position = "3 3";
        extent = "341 197";
        horizSizing = "right";
        vertSizing = "bottom";
        profile = new GuiBitmapCtrl(geTGF_deets_picture) @ "ETSNonModalProfile";
        position = "0 0";
        extent = "341 197";
        horizSizing = "width";
        vertSizing = "height";
        fitInParentAlign = 1;
        bitmap = new GuiBitmapCtrl(geTGF_deets_featured) @ "platform/client/ui/tgf/tgf_featured";
        profile = "EtsNonModalProfile";
        extent = "82 19";
        position = (82.0 - 341.0) @ " " @ 0;
        horizSizing = "left";
        vertSizing = "bottom";
        %window.add();
        internalName = new GuiMLTextCtrl(geTGF_deets_Title) @ "";
        profile = "ETSNonModalProfile";
        position = "3 1";
        extent = (24.0 - (150.0 - getWord(%window.getExtent(), 0))) @ " " @ 21;
        text = mlStyle("Title", "tgfDeets_Title");
        horizSizing = "width";
        vertSizing = "bottom";
        style = "tgfDeets_Title";
        autoDetectLinks = 0;
        %window.add();
        internalName = new GuiMLTextCtrl(geTGF_deets_subType) @ "";
        profile = "ETSNonModalProfile";
        position = (24.0 - (150.0 - getWord(%window.getExtent(), 0))) @ " " @ 1;
        extent = "150 21";
        horizSizing = "width";
        vertSizing = "bottom";
        style = "tgfDeets_SubType";
        %window.add();
        profile = new GuiControl(geTGF_deets_happening) @ "ETSNonModalProfile";
        position = "0 0";
        extent = "100 100";
        horizSizing = "width";
        vertSizing = "height";
        profile = new GuiMLTextCtrl(geTGF_deets_hostName) @ "InfoWindowTextProfile";
        position = "3 0";
        extent = "45 45";
        horizSizing = "width";
        vertSizing = "bottom";
        style = "tgfDeets_host_name";
        text = "host:";
        profile = new GuiMLTextCtrl(geTGF_deets_eventText) @ "InfoWindowTextProfile";
        position = "3 55";
        extent = "97 5";
        horizSizing = "width";
        vertSizing = "bottom";
        style = "tgfDeets_host_name";
        text = "deets:";
        autoDetectLinks = 0;
        profile = new GuiBitmapCtrl(geTGF_deets_host_picture) @ "ETSNonModalProfile";
        position = "50 3";
        extent = "50 50";
        horizSizing = "left";
        vertSizing = "bottom";
        %window.add();
        profile = new GuiControl(geTGF_deets_venue) @ "ETSNonModalProfile";
        position = "0 0";
        extent = %window.getExtent();
        horizSizing = "width";
        vertSizing = "height";
        profile = new GuiMLTextCtrl(geTGF_deets_venueText) @ "InfoWindowTextProfile";
        position = "3 24";
        extent = "10 10";
        horizSizing = "right";
        vertSizing = "bottom";
        style = "tgfDeets_Stats";
        %window.add();
        profile = new GuiControl(geTGF_deets_person) @ "ETSNonModalProfile";
        position = "0 0";
        extent = %window.getExtent();
        horizSizing = "width";
        vertSizing = "height";
        profile = new GuiMLTextCtrl(geTGF_deets_stats) @ "InfoWindowTextProfile";
        position = "3 24";
        extent = "10 10";
        horizSizing = "right";
        vertSizing = "bottom";
        style = "tgfDeets_Stats";
        profile = new GuiMLTextCtrl(geTGF_deets_stats_headline) @ "InfoWindowTextProfile";
        position = "278 147";
        extent = "220 10";
        horizSizing = "right";
        vertSizing = "bottom";
        style = "tgfDeets_Stats";
        autoDetectLinks = 0;
        %window.add();
        position = new GuiMLTextCtrl(geTGF_deets_NavLinks) @ (39.0 - getWord(%window.getExtent(), 0)) @ " " @ (18.0 - getWord(%window.getExtent(), 1));
        extent = "36 18";
        text = "navigation";
        horizSizing = "left";
        vertSizing = "top";
        style = "tgfDeets_NavLinks";
        %window.add();
    }
    if ((%item $= type)) {
        return %window SPC itemType;
    }
    if ((%item SPC type $= "happening")) {
        %this.arrangeDeetsWindow_happening(%window);
    }
    if ((%item SPC type $= "venue")) {
        %this.arrangeDeetsWindow_venue(%window);
    }
    if ((%item SPC type $= "person")) {
        %this.arrangeDeetsWindow_person(%window);
    }
    error(%item @ type @ " " @ getTrace());
};
function geTGF::arrangeDeetsWindow_happening(%this, %window) {
    500.resize(200);
    alignToCenterXY();
    %drop = 21;
    geDeetsWindow;
    %this.arrangeDeetsPicture(%window, %drop, 500, 380);
    %sizX = (geTGF_deets_pictureContainer - (getWord(getExtent(), 0) - getWord(%window.getExtent(), 0)));
    5.0;
    %sizY = getWord(getExtent(), 1);
    geTGF_deets_pictureContainer;
    %posX = (getWord(getPosition(), 0) + (geTGF_deets_pictureContainer + getWord(getExtent(), 0)));
    geTGF_deets_pictureContainer;
    %posY = %drop;
    2.0;
    %posX.resize(%posY, %sizX, %sizY);
    1.setVisible();
    0.setVisible();
    0.setVisible();
    1.setVisible();
};
function geTGF::arrangeDeetsWindow_venue(%this, %window) {
    450.resize(200);
    alignToCenterXY();
    %drop = 21;
    geDeetsWindow;
    %this.arrangeDeetsPicture(%window, %drop, 125, 152);
    %sizX = (geTGF_deets_pictureContainer - (getWord(getExtent(), 0) - getWord(%window.getExtent(), 0)));
    5.0;
    %sizY = getWord(getExtent(), 1);
    geTGF_deets_pictureContainer;
    %posX = (getWord(getPosition(), 0) + (geTGF_deets_pictureContainer + getWord(getExtent(), 0)));
    geTGF_deets_pictureContainer;
    %posY = %drop;
    2.0;
    %posX.resize(%posY, %sizX, %sizY);
    0.setVisible();
    1.setVisible();
    0.setVisible();
    0.setVisible();
};
function geTGF::arrangeDeetsWindow_person(%this, %window) {
    500.resize(200);
    alignToCenterXY();
    %drop = 21;
    geDeetsWindow;
    %this.arrangeDeetsPicture(%window, %drop, 1, 1);
    %sizX = (geTGF_deets_pictureContainer - (getWord(getExtent(), 0) - getWord(%window.getExtent(), 0)));
    5.0;
    %sizY = getWord(getExtent(), 1);
    geTGF_deets_pictureContainer;
    %posX = (getWord(getPosition(), 0) + (geTGF_deets_pictureContainer + getWord(getExtent(), 0)));
    geTGF_deets_pictureContainer;
    %posY = %drop;
    2.0;
    %posX.resize(%posY, %sizX, %sizY);
    0.setVisible();
    0.setVisible();
    1.setVisible();
    0.setVisible();
};
function geTGF::arrangeDeetsPicture(%this, %window, %drop, %aspectW, %aspectH) {
    %sizY = (2.0 - (%drop - getWord(%window.getExtent(), 1)));
    %sizX = (%aspectH / (%aspectW * %sizY));
    %posX = 3;
    %posY = %drop;
    %posX.resize(%posY, %sizX, %sizY);
};
function geTGF::fillDetailsContainer(%this, %container, %item) {
    if ((%item SPC type $= "happening")) {
        %this.fillDetailsContainer_Happening(%container, %item);
    }
    if ((%item SPC type $= "venue")) {
        %this.fillDetailsContainer_Venue(%container, %item);
    }
    if ((%item SPC type $= "person")) {
        %this.fillDetailsContainer_Person(%container, %item);
    }
    error(%item @ type @ " " @ getTrace());
    currentItem = %item @ %this.getItemList(listName, type);
    %item;
    %hasPrevItem = isObject(%this.getPrevItem(listName, type));
    %item;
    %hasNextItem = isObject(%this.getNextItem(listName, type));
    %item;
    if (%hasPrevItem) {
    }
    %prevLink = tgfDeets_NavLinkInactive @ mlStyle("<<");
    mlStyle("<a:gamelink prev><<</a>");
    if (%hasNextItem) {
    }
    %nextLink = tgfDeets_NavLinkInactive @ mlStyle(">>");
    mlStyle("<a:gamelink next>>></a>");
    geTGF_deets_NavLinks @ %prevLink @ %nextLink.setText();
};
function geTGF::formatOccupancy(%this, %num, %interestingNumberFormat, %unknownString, %noneString) {
    if ((-(1.0) == %num)) {
        %ret = %unknownString;
    }
    if ((0.0 == %num)) {
        %ret = %noneString;
    }
    %ret = %interestingNumberFormat @ %num;
    return %ret;
};
function geTGF::getEventTypePostItTagBitmap(%this, %item) {
    %ret = "";
    if (!(%item SPC eventID $= "")) {
        if ((%item SPC subType $= "publicLocationEvent")) {
            %ret = "platform/client/ui/tgf/tgf_publicEvent";
        }
        if (featured) {
            %ret = "platform/client/ui/tgf/tgf_featuredEvent";
            %item;
        }
        %ret = "platform/client/ui/tgf/tgf_event";
    }
    return %ret;
};
function geTGF::fillDetailsContainer_Happening(%this, %container, %item) {
    headline.setTextWithStyle("");
    %profileURL = %item @ urlEncode(hostUserName);
    %item @ $Net::ProfileURL;
    %subTypeText = "";
    geTGF_deets_Title;
    if ((%item SPC subType $= "apt")) {
        %mainPicUrl1 = $Net::BuildDirPhotoURL @ %item @ urlEncode(hostUserName) @ "?size=S";
        %mainPicUrl2 = $Net::BuildDirPhotoURL @ %item @ urlEncode(hostUserName) @ "?size=L";
        %hostUrl = $Net::AvatarURL @ %item @ urlEncode(hostUserName) @ "?size=S";
        %detailsURL = %profileURL;
        %subTypeText = "personal space ";
    }
    if ((%item SPC subType $= "aptEvent")) {
        %mainPicUrl1 = %item @ baseImageURL @ "?size=S";
        %mainPicUrl2 = %item @ baseImageURL @ "?size=M";
        %hostUrl = $Net::AvatarURL @ %item @ urlEncode(hostUserName) @ "?size=S";
        %detailsURL = %item @ urlEncode(eventID);
        $Net::EventDetailURL;
        %subTypeText = "event ";
    }
    if ((%item SPC subType $= "publicLocationEvent")) {
        %mainPicUrl1 = %item @ baseImageURL @ "?size=S";
        %mainPicUrl2 = %item @ baseImageURL @ "?size=M";
        %hostUrl = $Net::AvatarURL @ %item @ urlEncode(hostUserName) @ "?size=S";
        %detailsURL = %item @ urlEncode(eventID);
        $Net::EventDetailURL;
        %subTypeText = "public event ";
    }
    if ((%item SPC subType $= "")) {
        error(getScopeName() @ " " @ "- empty happening subtype." @ " " @ "(" @ %item @ type @ ")" @ " " @ getTrace());
    }
    error(getScopeName() @ " " @ "- unknown happening subtype:" @ " " @ %item @ subType @ " " @ "(" @ %item @ type @ ")" @ " " @ getTrace());
    %bitmap = %this.getEventTypePostItTagBitmap(%item);
    %bitmap.setBitmap();
    if (featured) {
        %subTypeText = "featured" @ " " @ %subTypeText;
        %item;
    }
    %subTypeText = "-" @ " " @ %subTypeText;
    geTGF_deets_featured;
    %subTypeText.setTextWithStyle();
    "platform/client/ui/tgf/tgf_profile_default".setBitmap();
    fitInParentAsBitmap();
    %mainPicUrl1.downloadAndApplyBitmap();
    %mainPicUrl2.downloadAndApplyBitmap();
    "platform/client/ui/tgf/tgf_profile_default".setBitmap();
    %hostUrl.downloadAndApplyBitmap();
    %text = "<tab:45>";
    geTGF_deets_host_picture;
    %text = geTGF_deets_subType @ geTGF_deets_picture @ geTGF_deets_picture @ geTGF_deets_picture @ geTGF_deets_picture @ geTGF_deets_host_picture @ %text @ "host" @ "\t" @ ":<just:right>" @ "<spush><b>" @ " <a:gamelink " @ %profileURL @ ">" @ %item @ hostUserName @ "</a>" @ "<spop>";
    %text.setTextWithStyle("");
    %occupancy = %this.formatOccupancy(occupancy, "<b>", "<color:ffffff60>(unknown)", "<color:ffffff60>-");
    %item;
    %friendOccupancy = %this.formatOccupancy(friendOccupancy, "<b><color:40ff40>", "<color:ffffff60>(unknown)", "<color:ffffff60>-");
    %item;
    %text = "<tab:45>";
    geTGF_deets_hostName;
    %text = %text @ "<just:left>" @ "Peeps" @ "\t" @ ": <spush>" @ %occupancy @ "<spop>";
    %text = %text @ "<br><just:left>" @ "Friends" @ "\t" @ ": <spush>" @ %friendOccupancy @ "<spop>";
    %text = %text @ "<br><just:left>" @ "Where" @ "\t" @ ": <spush>" @ %item @ DestinationList::GetAreaNameUserFacingName(location_areaName) @ "<spop>";
    %text = %text @ "<br><just:left>" @ "Access" @ "\t" @ ": <spush>" @ %item @ %this.getUserFacingAccessModeWithIcon(accessMode) @ "<spop>";
    %text = %text @ "<br>";
    %text = %text @ "<br><just:left>" @ "<spush><b>" @ "<a:gamelink " @ %detailsURL @ ">" @ "More Info" @ "</a>" @ "<spop>";
    %text = mlStyle(%text @ "<just:right>" @ "<spush>" @ "<a:gamelink " @ %item @ goThereVURL @ ">" @ "<b>Let's Go! " @ "</a>", "tgfDeets_Visit") @ "<spop>";
    %text.setTextWithStyle("");
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
    fullName = $gDestinationNames @ %item[%item @ codeName] @ %item;
    areaName = $gDestinationSpaces @ %item[%item @ codeName] @ %item;
    headline = $gDestinationDescsInCloset @ %item[%item @ codeName] @ %item;
    goThereVURL = $gDestinationVurls @ %item[%item @ codeName] @ %item;
    cityName = %item @ DestinationList::GetAreaNameUserFacingName(DestinationList::GetAreaNameCity(areaName)) @ %item;
    %whatIsIt = "A ";
    %delim = "";
    %n = ($gDestinationFilters - getWordCount(%item[%item @ codeName]));
    1.0;
    if ((0.0 >= %n)) {
        %whatIsIt = $gDestinationFilters @ getWord(%item[%item @ codeName], %n);
        %whatIsIt @ %delim;
        %delim = " and a ";
        %n = (1.0 - %n);
    }
    %subTypeText = "- venue ";
    (0.0 >= %n);
    %item @ fullName.setTextWithStyle("");
    %subTypeText.setTextWithStyle();
    %bitmapName = DestinationList::getBitmapLocation(codeName);
    %item;
    %bitmapName.setBitmap();
    fitInParentAsBitmap();
    %tableSettings = "<tab:67,215>";
    geTGF_deets_picture;
    %text = %tableSettings;
    geTGF_deets_picture;
    %text = geTGF_deets_subType @ %text @ %whatIsIt;
    geTGF_deets_Title @ "<clip:560>";
    %text = %text @ " in <spush><b><a:gamelink MAP_CITY " @ %item @ DestinationList::GetAreaNameCity(areaName) @ ">" @ %item @ cityName @ "</a><spop>.";
    %text = %text @ "<br>";
    %text = %text @ "<br>";
    %text = %item @ mlStyle(headline, "tgfDeets_Headline");
    %text;
    %text = %text @ "<br>";
    %text = %text @ "<br>";
    %text = %text @ "<br>";
    %text = %text @ "<br>";
    %text = %text @ "<br>";
    %text = mlStyle(%text @ "<just:right>" @ "<spush>" @ "<a:gamelink " @ %item @ goThereVURL @ ">" @ "<b>Let's Go! " @ "</a>", "tgfDeets_Visit") @ "<spop>";
    %text.setTextWithStyle("");
};
function geTGF_deets_venueText::onURL(%this, %url) {
    0.setVisible();
    if ((geDeetsLayer SPC firstWord(%url) $= "gamelink")) {
    }
    %url = %url;
    restWords(%url);
    %s = firstWord(%url);
    if ((%s $= "MAP_CITY")) {
        %city = restWords(%url);
        "map".openToTabName();
        %city.selectCity();
    }
    Parent::onURL(%this, %url);
};
$gTextAllTimeVPointsLink = "<spush><b><linkcolor:ffffff><a:gamelink " @ $Net::HelpURL_VPoints @ ">All-time <bitmap:platform/client/ui/vpoints_14></a><spop>";
function geTGF::fillDetailsContainer_Person(%this, %container, %item) {
    %friend = (%item SPC relationType $= "friend");
    %friendTag = %friend ? "<color:30dd30><shadowcolor:000080>" : "";
    %item @ userName.setTextWithStyle("");
    %subTypeText = %friend ? "<color:40ee40>- Friend " : "- vSider ";
    geTGF_deets_Title @ %friendTag;
    %subTypeText.setTextWithStyle();
    "platform/client/ui/tgf/tgf_profile_default".setBitmap();
    fitInParentAsBitmap();
    %url = geTGF_deets_picture @ $Net::AvatarURL @ %item @ urlEncode(userName) @ "?size=M";
    geTGF_deets_picture;
    %url.downloadAndApplyBitmap();
    %url = geTGF_deets_picture @ $Net::AvatarURL @ %item @ urlEncode(userName) @ "?size=L";
    geTGF_deets_subType;
    %url.downloadAndApplyBitmap();
    %tableSettings = "<tab:67,215>";
    geTGF_deets_picture;
    %profileURL = %item @ urlEncode(userName);
    $Net::ProfileURL;
    if ((%item SPC goThereVURL $= "")) {
        goThereVURL = "vside:/user/" @ %item @ userName @ %item;
    }
    %readMoreText = "<spush><b><a:gamelink " @ %profileURL @ ">Read More</a><spop>";
    %goThereText = "<spush><b><a:gamelink " @ %item @ goThereVURL @ ">Visit " @ (%item SPC gender $= "m") ? "him" : "her" @ " now!</a><spop>";
    %goThereText = mlStyle(%goThereText, "tgfDeets_Visit");
    %text = %tableSettings;
    if ((%item SPC age $= "")) {
    }
    %text = %item @ age;
    "(not shown)";
    if ((%item SPC locationIRL $= "")) {
    }
    %text = %item @ locationIRL;
    "(not shown)";
    %text = %item @ respektLevelToNameWithoutArticle(respektScoreToLevel(score));
    %text @ "Age" @ "\t" @ ": " @ %text @ "<br>" @ "Location" @ "\t" @ ": " @ %text @ "<br>" @ "Level" @ "\t" @ ": ";
    %text = %item @ score;
    %text @ "<br>" @ $gTextAllTimeVPointsLink @ "\t" @ ": ";
    %text = %item @ DestinationList::GetAreaNameUserFacingName(currentLocation_areaName);
    %text @ "<br>" @ "vLocation" @ "\t" @ ": ";
    %text = %item @ getUserActivityMgr().getActivitiesMLText(currentActivities, 5);
    %text @ "<br>" @ "Activities" @ "\t" @ ": ";
    %text = %text @ "<br>" @ "Status" @ "\t" @ ": ";
    %text = %text @ "<br>";
    %text = %text @ "<br>";
    %text = %text @ "<br>" @ "      " @ %readMoreText @ "    |    " @ %goThereText;
    %text.setTextWithStyle("");
    if ((%item SPC headline $= "")) {
    }
    headline.setTextWithStyle("");
};
function geTGF::DoDetails(%this, %tabName, %item) {
    if ((%tabName $= "main")) {
        %container = %this.main_GetAndOpenDetailsContainer(%item);
    }
    if ((%tabName $= "hotspots")) {
        %container = %this.hotspots_GetAndOpenDetailsContainer(%item);
    }
    if ((%tabName $= "friends")) {
        %container = %this.friends_GetAndOpenDetailsContainer(%item);
    }
    if ((%tabName $= "map")) {
        %container = %this.map_GetAndOpenDetailsContainer(%item);
    }
    if ((%tabName $= "myplace")) {
        %container = %this.myplace_GetAndOpenDetailsContainer(%item);
    }
    error(getScopeName() @ " " @ "- unknown tabName:" @ " " @ %tabName @ " " @ getTrace());
    %itemList = itemList;
    %item;
    currentItem = %item @ %itemList;
    currentItem = %item @ %this;
    %this.fillDetailsContainer(%container, %item);
};
function geTGF::getPrevItem(%this, %listName, %type) {
    %itemList = %this.getItemList(%listName, %type);
    %curNdx = %itemList.getIndexFromValue(currentItem);
    %itemList;
    if ((0.0 < %curNdx)) {
        %curNdx = 0;
    }
    %newNdx = (1.0 - %curNdx);
    if ((0.0 >= %newNdx)) {
    }
    if ((%itemList.count() < %newNdx)) {
        %ret = %itemList.getValue(%newNdx);
    }
    %ret = "";
    return %ret;
};
function geTGF::getNextItem(%this, %listName, %type) {
    %itemList = %this.getItemList(%listName, %type);
    %curNdx = %itemList.getIndexFromValue(currentItem);
    %itemList;
    if ((0.0 < %curNdx)) {
        %curNdx = 0;
    }
    %newNdx = (1.0 + %curNdx);
    if ((0.0 >= %newNdx)) {
    }
    if ((%itemList.count() < %newNdx)) {
        %ret = %itemList.getValue(%newNdx);
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
        %item = listName.getPrevItem(type);
        currentItem;
    }
    if ((geTGF SPC %s $= "next")) {
        %item = listName.getNextItem(type);
        currentItem;
    }
    if (isObject(%item)) {
        name.DoDetails(%item);
    }
};
function geTGF::createNewItem(%this, %listName, %type, %id) {
    %itemList = %this.getItemList(%listName, %type);
    %item = new ""();
    ScriptObject;
    type = 0 @ %type @ %item;
    id = %id @ %item;
    listName = %listName @ %item;
    %itemList.push_back(%id, %item);
    return %item;
};
function geTGF::clearItemList(%this, %listName, %type) {
    %itemList = %this.getItemList(%listName, %type);
    %n = (1.0 - %itemList.count());
    if ((0.0 >= %n)) {
        %item = %itemList.getValue(%n);
        %item.delete();
        %n = (1.0 - %n);
    }
    %itemList.empty();
};
function geTGF::getItemList(%this, %listName, %type) {
    if (!(%this.testItemList(%listName, %type))) {
        mainTabItems = 0 @ Array @ new ""() TAB %listName @ %type @ %this;
    }
    return mainTabItems;
};
function geTGF::testItemList(%this, %listName, %type) {
    return isObject(mainTabItems);
};
function geTGF::removeItemsWithFieldValueFromList(%this, %listName, %type, %fieldName, %fieldValue) {
    %itemList = %this.getItemList(%listName, %type);
    %n = (1.0 - %itemList.count());
    if ((0.0 >= %n)) {
        %item = %itemList.getValue(%n);
        %itemFieldValue = %item.getFieldValue(%fieldName);
        if ((%itemFieldValue $= %fieldValue)) {
            %itemList.erase(%n);
            %item.delete();
        }
        %n = (1.0 - %n);
    }
};
function geTGF::removeItemsFromList1WithMatchingItemInList2(%this, %list1Name, %list1Type, %list2Name, %list2Type, %fieldName) {
    %itemList2 = %this.getItemList(%list2Name, %list2Type);
    %n = (1.0 - %itemList2.count());
    if ((0.0 >= %n)) {
        %item = %itemList2.getValue(%n);
        %fieldValue = %item.getFieldValue(%fieldName);
        %this.removeItemsWithFieldValueFromList(%list1Name, %list1Type, %fieldName, %fieldValue);
        %n = (1.0 - %n);
    }
};
function geTGF::findItem(%this, %listName, %type, %id) {
    %list = %this.getItemList(%listName, %type);
    %list.moveFirst();
    %indx = %list.getIndexFromKey(%id);
    if ((0.0 < %indx)) {
        error(getScopeName() @ " " @ "- item not found:" @ " " @ %type @ " " @ %id @ " " @ getTrace());
        return "";
    }
    return %list.getValue(%indx);
};
function geTGF::dumpItemList(%this, %listName, %type) {
    %itemList = %this.getItemList(%listName, %type);
    %n = (1.0 - %itemList.count());
    if ((0.0 >= %n)) {
        %item = %itemList.getValue(%n);
        %item.dumpFields();
        %n = (1.0 - %n);
    }
};
