$TGEOpenMsg = "Looking at the Go Directory";
exec("./TGFTabMainClient.cs");
exec("./TGFTabHotSpotsClient.cs");
exec("./TGFTabFriendsClient.cs");
exec("./TGFTabMapClient.cs");
exec("./TGFTabMyPlacesClient.cs");
function geTGF::ResetAndOpen(%this)
{
    %this.setTabNeedsRefreshAll(1);
    if (!(%this.mGeTabs $= ""))
    {
        %this.mGeTabs.Maps_filterDestinationsByType("");
    }
    WorldMap.setView("multi_city");
    %this.openToTabName("Main");
    return ;
}
function geTGF::shouldGoOnPlayGui(%this)
{
    %ret = WorldMap.loggedIn;
    return %ret;
}
function geTGF::adjustAppearanceForContainer(%this, %onPlayGui)
{
    if (%onPlayGui)
    {
        %this.mGeBackground.setBitmap("platform/client/ui/finelines");
        %this.mGeBackground.wrap = 1;
        %this.mGeBackground.modulationColor = "255 255 255 90";
        %this.mGeWindow.setProfile(TGFSmallWindowProfile);
        %this.mGeWindow_BG.modulationColor = "255 255 255 255";
    }
    else
    {
        %this.mGeBackground.setBitmap("");
        %this.mGeWindow.setProfile(TGFBigWindowProfile);
        %this.mGeWindow.resize(0, 0, 960, 544);
        %this.mGeWindow_BG.modulationColor = "255 255 255 200";
    }
    %this.mGeWindow.resizeWidth = 0;
    %this.mGeWindow.resizeHeight = 0;
    %this.mGeWindow.canMove = %onPlayGui;
    %this.mGeWindow.canClose = %onPlayGui;
    %this.mGeWindow.canMinimize = 0;
    %this.mGeWindow.canMaximize = 0;
    %this.mGeClose.setVisible(%onPlayGui);
    return ;
}
function geTGF::open(%this)
{
    %this.init();
    %this.doreopen = 0;
    GuiTracker.updateLocation(%this);
    setIdle(1, $TGEOpenMsg);
    tgfMapMap.push();
    %this.setVisible(1);
    %this.adjustAppearanceForContainer(%this.shouldGoOnPlayGui());
    if (%this.shouldGoOnPlayGui())
    {
        PlayGui.add(%this);
        PlayGui.focusAndRaise(%this);
        %this.resize(0, 0, getWord(PlayGui.extent, 0), getWord(PlayGui.extent, 1));
        %this.onPlayGui = 1;
        moveMap.pop();
        functionMap.pop();
        pushScreenSize(960, 544, 0, 1, 1);
    }
    else
    {
        %this.onPlayGui = 0;
        Canvas.setContent(%this);
        %this.resize(0, 0, 960, 544);
    }
    geTGF.mUserName.setText(mlStyle("<just:right>" @ $Player::Name, "tgfSmallText_large"));
    WorldMap.refresh();
    moveAccountBalanceHud("TGF");
    %this.refreshIfNeeded();
    return ;
}
function geTGF::reopen(%this)
{
    if (!((%this.doreopen $= "")) && (%this.doreopen == 1))
    {
        %this.open();
    }
    return ;
}
function geTGF::openToTabName(%this, %tabName)
{
    if (!%this.isVisible())
    {
        %this.open();
    }
    %this.mGeTabs.selectTabWithName(%tabName);
    %this.refreshIfNeeded();
    return ;
}
function geTGF::toggleToTabName(%this, %tabName)
{
    if (%this.isVisible())
    {
        %currentTab = %this.mGeTabs.getCurrentTab();
        if (%currentTab.name $= %tabName)
        {
            %this.close();
            return ;
        }
    }
    %this.setTabNeedsRefresh(%tabName, 1);
    %this.openToTabName(%tabName);
    return ;
}
function geTGF::closeFully(%this)
{
    %this.close();
    if (%this.isVisible())
    {
        %this.close();
        %this.setVisible(0);
    }
    return ;
}
function geTGF::close(%this)
{
    if (tgfMapMap.isActive())
    {
        tgfMapMap.pop();
    }
    if (geDeetsLayer.isVisible())
    {
        eval(geDeetsWindow.closeCommand);
        return ;
    }
    %this.setTabNeedsRefreshAll(1);
    if (%this.getId() == Canvas.getContent())
    {
        return 0;
    }
    %this.setVisible(0);
    if (%this.onPlayGui)
    {
        PlayGui.focusAndRaise(%this);
        popScreenSize();
        moveMap.push();
        functionMap.push();
    }
    setIdle(0);
    moveAccountBalanceHud("PLAYGUI");
    return 1;
}
function geTGF::onGroupRemove(%this)
{
    moveAccountBalanceHud("PLAYGUI");
    return ;
}
function geTGF::toggle(%this)
{
    if (%this.isVisible())
    {
        return %this.close();
    }
    else
    {
        return %this.open();
    }
    return ;
}
function geTGF::refreshIfNeeded(%this)
{
    %curTabName = geTGF_tabs.getUpcomingTab().name;
    if (%this.getTabNeedsRefresh(%curTabName))
    {
        %this.refresh();
    }
    return ;
}
function geTGF::refresh(%this)
{
    %curTabName = geTGF_tabs.getUpcomingTab().name;
    %cmd = "geTGF_Tabs.refreshTab" @ %curTabName @ "();";
    eval(%cmd);
    %this.setTabNeedsRefresh(%curTabName, 0);
    return ;
}
function geTGF::reinit(%this)
{
    if (isObject(%this.mGeTabs))
    {
        %this.mGeTabs.delete();
    }
    %this.init();
    return ;
}
function geTGF::init(%this)
{
    if (isObject(%this.mGeTabs))
    {
        return ;
    }
    %this.mGeTabs = new ScriptObject(geTGF_tabs)
    {
        class = "TabControl";
        tabsAlign = "near";
        tabsOffset = "-2 1";
    };
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%this.mGeTabs);
    }
    %this.mGeTabs.setup(%this.mGeTabContainer);
    WorldMap.initCityMaps();
    WorldMap.requestMapData();
    WorldMap.schedule(100, "initialize");
    geTGF.mGeTabs.Maps_GetApartmentVURL();
    %this.setDoCallbackOnGroupAddRemove(1);
    return ;
}
function geTGF_tabs::setup(%this, %container)
{
    if (%this.initialized)
    {
        return ;
    }
    %this.Initialize(%container, "137 45", "", "0 0", "horizontal");
    %tabNames = "";
    %tabNames = %tabNames @ "main" @ " ";
    %tabNames = %tabNames @ "hotspots" @ " ";
    %tabNames = %tabNames @ "friends" @ " ";
    %tabNames = %tabNames @ "map" @ " ";
    %tabNames = %tabNames @ "myplace" @ " ";
    %tooltips["main"] = $MsgCat::TGF["tooltips_main"] ;
    %tooltips["hotspots"] = $MsgCat::TGF["tooltips_hotspots"] ;
    %tooltips["friends"] = $MsgCat::TGF["tooltips_friends"] ;
    %tooltips["map"] = $MsgCat::TGF["tooltips_map"] ;
    %tooltips["myplace"] = $MsgCat::TGF["tooltips_myplace"] ;
    %num = getWordCount(%tabNames);
    %n = 0;
    while (%n < %num)
    {
        %tabName = getWord(%tabNames, %n);
        if (isDefined("%tooltips" @ %tabName))
        {
        }
        else
        {
        }
        %toolTip = ;
        %tab = %this.newTab(%tabName, "platform/client/buttons/tgf/tgf_tab_" @ %tabName, %toolTip);
        %tab.setName("geTGF_Tab_" @ %tabName);
        %tab.bindClassName("geTGF_Tab_" @ %tabName);
        %n = %n + 1;
    }
    %this.update();
    geTGF.setTabNeedsRefreshAll(1);
    %this.schedule(100, "fillTabMap");
    %this.selectTabWithName("main");
    return ;
}
function geTGF::setTabNeedsRefreshAll(%this, %val)
{
    %this.setTabNeedsRefresh("main", %val);
    %this.setTabNeedsRefresh("hotspots", %val);
    %this.setTabNeedsRefresh("friends", %val);
    %this.setTabNeedsRefresh("map", %val);
    %this.setTabNeedsRefresh("myplace", %val);
    return ;
}
function geTGF::setTabNeedsRefresh(%this, %tabName, %val)
{
    if (!isObject(geTGF_tabs))
    {
        return ;
    }
    geTGF_tabs.getTabWithName(%tabName).needsRefresh = %val;
    return ;
}
function geTGF::getTabNeedsRefresh(%this, %tabName)
{
    %val = geTGF_tabs.getTabWithName(%tabName).needsRefresh;
    return %val;
}
function geTGF_tabs::fillTabGeneric(%this, %tab)
{
    %tab.setProfile(ETSNonModalProfile);
    %tab.clear();
    return ;
}
function geTGF_tabs::onShowTabGeneric(%this)
{
    cancel(geTGF.geTGF_Refresh_Schedule);
    geTGF_Refresh.setActive(1);
    geTGF_Refresh.setVisible(1);
    return ;
}
function geTGF_tabs::onShowOrHideTab(%this, %tabObject, %show)
{
    if (!%show)
    {
        return ;
    }
    if (%tabObject.name $= "main")
    {
        %this.fillTabMain();
        %this.onShowTabMain();
    }
    else
    {
        if (%tabObject.name $= "hotspots")
        {
            %this.fillTabHotSpots();
            %this.onShowTabHotSpots();
        }
        else
        {
            if (%tabObject.name $= "friends")
            {
                %this.fillTabFriends();
                %this.onShowTabFriends();
            }
            else
            {
                if (%tabObject.name $= "map")
                {
                    %this.fillTabMap();
                    %this.onShowTabMap();
                }
                else
                {
                    if (%tabObject.name $= "myplace")
                    {
                        %this.fillTabMyPlace();
                        %this.onShowTabMyPlace();
                    }
                    else
                    {
                        error(getScopeName() SPC "- unknown tab name:" SPC %tabObject.name SPC getTrace());
                    }
                }
            }
        }
    }
    geTGF.refreshIfNeeded();
    return ;
}
function geTGF::onLogin(%this)
{
    %this.ResetAndOpen();
    return ;
}
function geTGF::onLogoutButton(%this)
{
    MessageBoxYesNo("Log Out", "<br>Are you sure you want to log out?<br>", %this @ ".logoutReally();", "");
    return ;
}
function geTGF::logoutReally(%this)
{
    logout(0);
    return ;
}
function geTGF::onRefresh(%this)
{
    cancel(%this.geTGF_Refresh_Schedule);
    %this.geTGF_Refresh_Schedule = geTGF_Refresh.schedule(30 * 1000, setActive, 1);
    geTGF_Refresh.setActive(0);
    hiliteControl(0);
    %this.refresh();
    return ;
}
function geTGF::onMyPlace(%this)
{
    geTGF_tabs::Maps_clickMyApartment();
    return ;
}
$gTGF_Deets_Constructed = 0;
function geTGF::constructDeetsWindow(%this, %window, %item)
{
    if (!$gTGF_Deets_Constructed)
    {
        $gTGF_Deets_Constructed = 1;
        %window.deleteMembers();
        %ctrl = new GuiBitmapButtonCtrl()
        {
            profile = "GuiButtonProfile";
            horizSizing = "left";
            vertSizing = "bottom";
            position = getWord(%window.getExtent(), 0) - 17 SPC 5;
            extent = "13 13";
            command = geDeetsWindow.closeCommand;
            bitmap = "platform/client/buttons/close_m";
        };
        %window.add(%ctrl);
        new GuiControl(geTGF_deets_pictureContainer)
        {
            profile = "ETSNonModalProfile";
            position = "3 3";
            extent = "341 197";
            horizSizing = "right";
            vertSizing = "bottom";
        }.add(new GuiControl(geTGF_deets_pictureContainer)
        {
            profile = "ETSNonModalProfile";
            position = "3 3";
            extent = "341 197";
            horizSizing = "right";
            vertSizing = "bottom";
        });
        new GuiControl(geTGF_deets_happening)
        {
            profile = "ETSNonModalProfile";
            position = "0 0";
            extent = "100 100";
            horizSizing = "width";
            vertSizing = "height";
        }.add(new GuiControl(geTGF_deets_happening)
        {
            profile = "ETSNonModalProfile";
            position = "0 0";
            extent = "100 100";
            horizSizing = "width";
            vertSizing = "height";
        });
        new GuiControl(geTGF_deets_venue)
        {
            profile = "ETSNonModalProfile";
            position = "0 0";
            extent = %window.getExtent();
            horizSizing = "width";
            vertSizing = "height";
        }.add(new GuiControl(geTGF_deets_venue)
        {
            profile = "ETSNonModalProfile";
            position = "0 0";
            extent = %window.getExtent();
            horizSizing = "width";
            vertSizing = "height";
        });
        new GuiControl(geTGF_deets_person)
        {
            profile = "ETSNonModalProfile";
            position = "0 0";
            extent = %window.getExtent();
            horizSizing = "width";
            vertSizing = "height";
        }.add(new GuiControl(geTGF_deets_person)
        {
            profile = "ETSNonModalProfile";
            position = "0 0";
            extent = %window.getExtent();
            horizSizing = "width";
            vertSizing = "height";
        });
    }
    if (%window.itemType $= %item.type)
    {
        return ;
    }
    if (%item.type $= "happening")
    {
        %this.arrangeDeetsWindow_happening(%window);
    }
    else
    {
        if (%item.type $= "venue")
        {
            %this.arrangeDeetsWindow_venue(%window);
        }
        else
        {
            if (%item.type $= "person")
            {
                %this.arrangeDeetsWindow_person(%window);
            }
            else
            {
                error("unknown itemType:" SPC %item.type SPC getTrace());
            }
        }
    }
    return ;
}
function geTGF::arrangeDeetsWindow_happening(%this, %window)
{
    geDeetsWindow.resize(500, 200);
    geDeetsWindow.alignToCenterXY();
    %drop = 21;
    %this.arrangeDeetsPicture(%window, %drop, 500, 380);
    %sizX = (getWord(%window.getExtent(), 0) - getWord(geTGF_deets_pictureContainer.getExtent(), 0)) - 5;
    %sizY = getWord(geTGF_deets_pictureContainer.getExtent(), 1);
    %posX = (getWord(geTGF_deets_pictureContainer.getExtent(), 0) + getWord(geTGF_deets_pictureContainer.getPosition(), 0)) + 2;
    %posY = %drop;
    geTGF_deets_happening.resize(%posX, %posY, %sizX, %sizY);
    geTGF_deets_happening.setVisible(1);
    geTGF_deets_venue.setVisible(0);
    geTGF_deets_person.setVisible(0);
    geTGF_deets_featured.setVisible(1);
    return ;
}
function geTGF::arrangeDeetsWindow_venue(%this, %window)
{
    geDeetsWindow.resize(450, 200);
    geDeetsWindow.alignToCenterXY();
    %drop = 21;
    %this.arrangeDeetsPicture(%window, %drop, 125, 152);
    %sizX = (getWord(%window.getExtent(), 0) - getWord(geTGF_deets_pictureContainer.getExtent(), 0)) - 5;
    %sizY = getWord(geTGF_deets_pictureContainer.getExtent(), 1);
    %posX = (getWord(geTGF_deets_pictureContainer.getExtent(), 0) + getWord(geTGF_deets_pictureContainer.getPosition(), 0)) + 2;
    %posY = %drop;
    geTGF_deets_venueText.resize(%posX, %posY, %sizX, %sizY);
    geTGF_deets_happening.setVisible(0);
    geTGF_deets_venue.setVisible(1);
    geTGF_deets_person.setVisible(0);
    geTGF_deets_featured.setVisible(0);
    return ;
}
function geTGF::arrangeDeetsWindow_person(%this, %window)
{
    geDeetsWindow.resize(500, 200);
    geDeetsWindow.alignToCenterXY();
    %drop = 21;
    %this.arrangeDeetsPicture(%window, %drop, 1, 1);
    %sizX = (getWord(%window.getExtent(), 0) - getWord(geTGF_deets_pictureContainer.getExtent(), 0)) - 5;
    %sizY = getWord(geTGF_deets_pictureContainer.getExtent(), 1);
    %posX = (getWord(geTGF_deets_pictureContainer.getExtent(), 0) + getWord(geTGF_deets_pictureContainer.getPosition(), 0)) + 2;
    %posY = %drop;
    geTGF_deets_stats.resize(%posX, %posY, %sizX, %sizY);
    geTGF_deets_happening.setVisible(0);
    geTGF_deets_venue.setVisible(0);
    geTGF_deets_person.setVisible(1);
    geTGF_deets_featured.setVisible(0);
    return ;
}
function geTGF::arrangeDeetsPicture(%this, %window, %drop, %aspectW, %aspectH)
{
    %sizY = (getWord(%window.getExtent(), 1) - %drop) - 2;
    %sizX = (%sizY * %aspectW) / %aspectH;
    %posX = 3;
    %posY = %drop;
    geTGF_deets_pictureContainer.resize(%posX, %posY, %sizX, %sizY);
    return ;
}
function geTGF::fillDetailsContainer(%this, %container, %item)
{
    if (%item.type $= "happening")
    {
        %this.fillDetailsContainer_Happening(%container, %item);
    }
    else
    {
        if (%item.type $= "venue")
        {
            %this.fillDetailsContainer_Venue(%container, %item);
        }
        else
        {
            if (%item.type $= "person")
            {
                %this.fillDetailsContainer_Person(%container, %item);
            }
            else
            {
                error(getScopeName() SPC "- unknown type:" SPC %item.type SPC getTrace());
            }
        }
    }
    %this.getItemList(%item.listName, %item.type).currentItem = %item;
    %hasPrevItem = isObject(%this.getPrevItem(%item.listName, %item.type));
    %hasNextItem = isObject(%this.getNextItem(%item.listName, %item.type));
    %prevLink = "<just:left>" @ %hasPrevItem ? mlStyle("<a:gamelink prev><<</a>", tgfDeets_NavLinkActive) : mlStyle("<<", tgfDeets_NavLinkInactive);
    %nextLink = "<just:right>" @ %hasNextItem ? mlStyle("<a:gamelink next>>></a>", tgfDeets_NavLinkActive) : mlStyle(">>", tgfDeets_NavLinkInactive);
    geTGF_deets_NavLinks.setText(%prevLink @ %nextLink);
    return ;
}
function geTGF::formatOccupancy(%this, %num, %interestingNumberFormat, %unknownString, %noneString)
{
    if (%num == -1)
    {
        %ret = %unknownString;
    }
    else
    {
        if (%num == 0)
        {
            %ret = %noneString;
        }
        else
        {
            %ret = %interestingNumberFormat @ %num;
        }
    }
    return %ret;
}
function geTGF::getEventTypePostItTagBitmap(%this, %item)
{
    %ret = "";
    if (!(%item.eventID $= ""))
    {
        if (%item.subType $= "publicLocationEvent")
        {
            %ret = "platform/client/ui/tgf/tgf_publicEvent";
        }
        else
        {
            if (%item.featured)
            {
                %ret = "platform/client/ui/tgf/tgf_featuredEvent";
            }
            else
            {
                %ret = "platform/client/ui/tgf/tgf_event";
            }
        }
    }
    return %ret;
}
function geTGF::fillDetailsContainer_Happening(%this, %container, %item)
{
    geTGF_deets_Title.setTextWithStyle(%item.headline, "");
    %profileURL = $Net::ProfileURL @ urlEncode(%item.hostUserName);
    %subTypeText = "";
    if (%item.subType $= "apt")
    {
        %mainPicUrl1 = $Net::BuildDirPhotoURL @ urlEncode(%item.hostUserName) @ "?size=S";
        %mainPicUrl2 = $Net::BuildDirPhotoURL @ urlEncode(%item.hostUserName) @ "?size=L";
        %hostUrl = $Net::AvatarURL @ urlEncode(%item.hostUserName) @ "?size=S";
        %detailsURL = %profileURL;
        %subTypeText = "personal space ";
    }
    else
    {
        if (%item.subType $= "aptEvent")
        {
            %mainPicUrl1 = %item.baseImageURL @ "?size=S";
            %mainPicUrl2 = %item.baseImageURL @ "?size=M";
            %hostUrl = $Net::AvatarURL @ urlEncode(%item.hostUserName) @ "?size=S";
            %detailsURL = $Net::EventDetailURL @ urlEncode(%item.eventID);
            %subTypeText = "event ";
        }
        else
        {
            if (%item.subType $= "publicLocationEvent")
            {
                %mainPicUrl1 = %item.baseImageURL @ "?size=S";
                %mainPicUrl2 = %item.baseImageURL @ "?size=M";
                %hostUrl = $Net::AvatarURL @ urlEncode(%item.hostUserName) @ "?size=S";
                %detailsURL = $Net::EventDetailURL @ urlEncode(%item.eventID);
                %subTypeText = "public event ";
            }
            else
            {
                if (%item.subType $= "")
                {
                    error(getScopeName() SPC "- empty happening subtype." SPC "(" @ %item.type @ ")" SPC getTrace());
                }
                else
                {
                    error(getScopeName() SPC "- unknown happening subtype:" SPC %item.subType SPC "(" @ %item.type @ ")" SPC getTrace());
                }
            }
        }
    }
    %bitmap = %this.getEventTypePostItTagBitmap(%item);
    geTGF_deets_featured.setBitmap(%bitmap);
    if (%item.featured)
    {
        %subTypeText = "featured" SPC %subTypeText;
    }
    %subTypeText = "-" SPC %subTypeText;
    geTGF_deets_subType.setTextWithStyle(%subTypeText);
    geTGF_deets_picture.setBitmap("platform/client/ui/tgf/tgf_profile_default");
    geTGF_deets_picture.fitInParentAsBitmap();
    geTGF_deets_picture.downloadAndApplyBitmap(%mainPicUrl1);
    geTGF_deets_picture.downloadAndApplyBitmap(%mainPicUrl2);
    geTGF_deets_host_picture.setBitmap("platform/client/ui/tgf/tgf_profile_default");
    geTGF_deets_host_picture.downloadAndApplyBitmap(%hostUrl);
    %text = "<tab:45>";
    %text = %text @ "host" TAB ":<just:right>" @ "<spush><b>" @ " <a:gamelink " @ %profileURL @ ">" @ %item.hostUserName @ "</a>" @ "<spop>";
    geTGF_deets_hostName.setTextWithStyle(%text, "");
    %occupancy = %this.formatOccupancy(%item.occupancy, "<b>", "<color:ffffff60>(unknown)", "<color:ffffff60>-");
    %friendOccupancy = %this.formatOccupancy(%item.friendOccupancy, "<b><color:40ff40>", "<color:ffffff60>(unknown)", "<color:ffffff60>-");
    %text = "<tab:45>";
    %text = %text @ "<just:left>" @ "Peeps" TAB ": <spush>" @ %occupancy @ "<spop>";
    %text = %text @ "<br><just:left>" @ "Friends" TAB ": <spush>" @ %friendOccupancy @ "<spop>";
    %text = %text @ "<br><just:left>" @ "Where" TAB ": <spush>" @ DestinationList::GetAreaNameUserFacingName(%item.location_areaName) @ "<spop>";
    %text = %text @ "<br><just:left>" @ "Access" TAB ": <spush>" @ %this.getUserFacingAccessModeWithIcon(%item.accessMode) @ "<spop>";
    %text = %text @ "<br>";
    %text = %text @ "<br><just:left>" @ "<spush><b>" @ "<a:gamelink " @ %detailsURL @ ">" @ "More Info" @ "</a>" @ "<spop>";
    %text = %text @ "<just:right>" @ "<spush>" @ mlStyle("<a:gamelink " @ %item.goThereVURL @ ">" @ "<b>Let\'s Go! " @ "</a>", "tgfDeets_Visit") @ "<spop>";
    geTGF_deets_eventText.setTextWithStyle(%text, "");
    return ;
}
function geTGF::getUserFacingAccessModeWithIcon(%this, %accessMode)
{
    %bitmap = "";
    %text = "";
    if (%accessMode $= "OPEN")
    {
        %bitmap = "";
        %text = "Open";
    }
    else
    {
        if (%accessMode $= "FRIENDSONLY")
        {
            %bitmap = "platform/client/ui/tgf/tgf_heart_white";
            %text = "Friends";
        }
        else
        {
            if (%accessMode $= "PASSWORDPROTECTED")
            {
                %bitmap = "platform/client/ui/tgf/tgf_key_white";
                %text = "Door Code";
            }
            else
            {
                error(getScopeName() SPC "- unknown access mode \'" @ %accessMode @ "\' -" SPC getTrace());
                %bitmap = "";
                %text = "(?)";
            }
        }
    }
    if (!(%bitmap $= ""))
    {
        %bitmap = "<bitmap:" @ %bitmap @ "> ";
    }
    return %bitmap @ %text;
}
function geTGF::fillDetailsContainer_Venue(%this, %container, %item)
{
    %item.fullName = $gDestinationNames[%item.codeName];
    %item.areaName = $gDestinationSpaces[%item.codeName];
    %item.headline = $gDestinationDescsInCloset[%item.codeName];
    %item.goThereVURL = $gDestinationVurls[%item.codeName];
    %item.cityName = DestinationList::GetAreaNameUserFacingName(DestinationList::GetAreaNameCity(%item.areaName));
    %whatIsIt = "A ";
    %delim = "";
    %n = getWordCount($gDestinationFilters[%item.codeName]) - 1;
    while (%n >= 0)
    {
        %whatIsIt = %whatIsIt @ %delim @ getWord($gDestinationFilters[%item.codeName], %n);
        %delim = " and a ";
        %n = %n - 1;
    }
    %subTypeText = "- venue ";
    geTGF_deets_Title.setTextWithStyle("<clip:560>" @ %item.fullName, "");
    geTGF_deets_subType.setTextWithStyle(%subTypeText);
    %bitmapName = DestinationList::getBitmapLocation(%item.codeName);
    geTGF_deets_picture.setBitmap(%bitmapName);
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
    %text = %text @ "<just:right>" @ "<spush>" @ mlStyle("<a:gamelink " @ %item.goThereVURL @ ">" @ "<b>Let\'s Go! " @ "</a>", "tgfDeets_Visit") @ "<spop>";
    geTGF_deets_venueText.setTextWithStyle(%text, "");
    return ;
}
function geTGF_deets_venueText::onURL(%this, %url)
{
    geDeetsLayer.setVisible(0);
    %url = firstWord(%url) $= "gamelink" ? restWords(%url) : %url;
    %s = firstWord(%url);
    if (%s $= "MAP_CITY")
    {
        %city = restWords(%url);
        geTGF.openToTabName("map");
        WorldMap.selectCity(%city);
    }
    else
    {
        Parent::onURL(%this, %url);
    }
    return ;
}
$gTextAllTimeVPointsLink = "<spush><b><linkcolor:ffffff><a:gamelink " @ $Net::HelpURL_VPoints @ ">All-time <bitmap:platform/client/ui/vpoints_14></a><spop>";
function geTGF::fillDetailsContainer_Person(%this, %container, %item)
{
    %friend = %item.relationType $= "friend";
    %friendTag = %friend ? "<color:30dd30><shadowcolor:000080>" : "";
    geTGF_deets_Title.setTextWithStyle(%friendTag @ %item.userName, "");
    %subTypeText = %friend ? "<color:40ee40>- Friend " : "- vSider ";
    geTGF_deets_subType.setTextWithStyle(%subTypeText);
    geTGF_deets_picture.setBitmap("platform/client/ui/tgf/tgf_profile_default");
    geTGF_deets_picture.fitInParentAsBitmap();
    %url = $Net::AvatarURL @ urlEncode(%item.userName) @ "?size=M";
    geTGF_deets_picture.downloadAndApplyBitmap(%url);
    %url = $Net::AvatarURL @ urlEncode(%item.userName) @ "?size=L";
    geTGF_deets_picture.downloadAndApplyBitmap(%url);
    %tableSettings = "<tab:67,215>";
    %profileURL = $Net::ProfileURL @ urlEncode(%item.userName);
    if (%item.goThereVURL $= "")
    {
        %item.goThereVURL = "vside:/user/" @ %item.userName;
    }
    %readMoreText = "<spush><b><a:gamelink " @ %profileURL @ ">Read More</a><spop>";
    %goThereText = "<spush><b><a:gamelink " @ %item.goThereVURL @ ">Visit " @ %item.gender $= "m" ? "him" : "her" @ " now!</a><spop>";
    %goThereText = mlStyle(%goThereText, "tgfDeets_Visit");
    %text = %tableSettings;
    %text = %text @ "Age" TAB ": " @ %item.age $= "" ? "(not shown)" : %item;
    %text = %text @ "<br>" @ "Location" TAB ": " @ %item.locationIRL $= "" ? "(not shown)" : %item;
    %text = %text @ "<br>" @ "Level" TAB ": " @ respektLevelToNameWithoutArticle(respektScoreToLevel(%item.score));
    %text = %text @ "<br>" @ $gTextAllTimeVPointsLink TAB ": " @ %item.score;
    %text = %text @ "<br>" @ "vLocation" TAB ": " @ DestinationList::GetAreaNameUserFacingName(%item.currentLocation_areaName);
    %text = %text @ "<br>" @ "Activities" TAB ": " @ getUserActivityMgr().getActivitiesMLText(%item.currentActivities, 5);
    %text = %text @ "<br>" @ "Status" TAB ": ";
    %text = %text @ "<br>";
    %text = %text @ "<br>";
    %text = %text @ "<br>" @ "      " @ %readMoreText @ "    |    " @ %goThereText;
    geTGF_deets_stats.setTextWithStyle(%text, "");
    geTGF_deets_stats_headline.setTextWithStyle(%item.headline $= "" ? "(none)" : %item, "");
    return ;
}
function geTGF::DoDetails(%this, %tabName, %item)
{
    if (%tabName $= "main")
    {
        %container = %this.main_GetAndOpenDetailsContainer(%item);
    }
    else
    {
        if (%tabName $= "hotspots")
        {
            %container = %this.hotspots_GetAndOpenDetailsContainer(%item);
        }
        else
        {
            if (%tabName $= "friends")
            {
                %container = %this.friends_GetAndOpenDetailsContainer(%item);
            }
            else
            {
                if (%tabName $= "map")
                {
                    %container = %this.map_GetAndOpenDetailsContainer(%item);
                }
                else
                {
                    if (%tabName $= "myplace")
                    {
                        %container = %this.myplace_GetAndOpenDetailsContainer(%item);
                    }
                    else
                    {
                        error(getScopeName() SPC "- unknown tabName:" SPC %tabName SPC getTrace());
                    }
                }
            }
        }
    }
    %itemList = %item.itemList;
    %itemList.currentItem = %item;
    %this.currentItem = %item;
    %this.fillDetailsContainer(%container, %item);
    return ;
}
function geTGF::getPrevItem(%this, %listName, %type)
{
    %itemList = %this.getItemList(%listName, %type);
    %curNdx = %itemList.getIndexFromValue(%itemList.currentItem);
    if (%curNdx < 0)
    {
        %curNdx = 0;
    }
    %newNdx = %curNdx - 1;
    if ((%newNdx >= 0) && (%newNdx < %itemList.count()))
    {
        %ret = %itemList.getValue(%newNdx);
    }
    else
    {
        %ret = "";
    }
    return %ret;
}
function geTGF::getNextItem(%this, %listName, %type)
{
    %itemList = %this.getItemList(%listName, %type);
    %curNdx = %itemList.getIndexFromValue(%itemList.currentItem);
    if (%curNdx < 0)
    {
        %curNdx = 0;
    }
    %newNdx = %curNdx + 1;
    if ((%newNdx >= 0) && (%newNdx < %itemList.count()))
    {
        %ret = %itemList.getValue(%newNdx);
    }
    else
    {
        %ret = "";
    }
    return %ret;
}
function geTGF_deets_NavLinks::onURL(%this, %url)
{
    %url = firstWord(%url) $= "gamelink" ? restWords(%url) : %url;
    %s = firstWord(%url);
    if (%s $= "prev")
    {
        %item = geTGF.getPrevItem(geTGF.currentItem.listName, geTGF.currentItem.type);
    }
    else
    {
        if (%s $= "next")
        {
            %item = geTGF.getNextItem(geTGF.currentItem.listName, geTGF.currentItem.type);
        }
    }
    if (isObject(%item))
    {
        geTGF.DoDetails(geTGF_tabs.getCurrentTab().name, %item);
    }
    return ;
}
function geTGF::createNewItem(%this, %listName, %type, %id)
{
    %itemList = %this.getItemList(%listName, %type);
    %item = new ScriptObject();
    %item.type = %type;
    %item.id = %id;
    %item.listName = %listName;
    %itemList.push_back(%id, %item);
    return %item;
}
function geTGF::clearItemList(%this, %listName, %type)
{
    %itemList = %this.getItemList(%listName, %type);
    %n = %itemList.count() - 1;
    while (%n >= 0)
    {
        %item = %itemList.getValue(%n);
        %item.delete();
        %n = %n - 1;
    }
    %itemList.empty();
    return ;
}
function geTGF::getItemList(%this, %listName, %type)
{
    if (!%this.testItemList(%listName, %type))
    {
        %this.mainTabItems[%listName,%type] = new Array();
    }
    return %this.mainTabItems[(%listName,%type)];
}
function geTGF::testItemList(%this, %listName, %type)
{
    return isObject(%this.mainTabItems[(%listName,%type)]);
}
function geTGF::removeItemsWithFieldValueFromList(%this, %listName, %type, %fieldName, %fieldValue)
{
    %itemList = %this.getItemList(%listName, %type);
    %n = %itemList.count() - 1;
    while (%n >= 0)
    {
        %item = %itemList.getValue(%n);
        %itemFieldValue = %item.getFieldValue(%fieldName);
        if (%itemFieldValue $= %fieldValue)
        {
            %itemList.erase(%n);
            %item.delete();
        }
        %n = %n - 1;
    }
}

function geTGF::removeItemsFromList1WithMatchingItemInList2(%this, %list1Name, %list1Type, %list2Name, %list2Type, %fieldName)
{
    %itemList2 = %this.getItemList(%list2Name, %list2Type);
    %n = %itemList2.count() - 1;
    while (%n >= 0)
    {
        %item = %itemList2.getValue(%n);
        %fieldValue = %item.getFieldValue(%fieldName);
        %this.removeItemsWithFieldValueFromList(%list1Name, %list1Type, %fieldName, %fieldValue);
        %n = %n - 1;
    }
}

function geTGF::findItem(%this, %listName, %type, %id)
{
    %list = %this.getItemList(%listName, %type);
    %list.moveFirst();
    %indx = %list.getIndexFromKey(%id);
    if (%indx < 0)
    {
        error(getScopeName() SPC "- item not found:" SPC %type SPC %id SPC getTrace());
        return "";
    }
    else
    {
        return %list.getValue(%indx);
    }
    return ;
}
function geTGF::dumpItemList(%this, %listName, %type)
{
    %itemList = %this.getItemList(%listName, %type);
    %n = %itemList.count() - 1;
    while (%n >= 0)
    {
        %item = %itemList.getValue(%n);
        %item.dumpFields();
        %n = %n - 1;
    }
}


