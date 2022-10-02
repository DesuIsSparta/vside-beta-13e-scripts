WorldMap.STATE_PARSE_RESULT = 1;
WorldMap.STATE_PARSE_SERVER_COUNT = 2;
WorldMap.STATE_PARSE_SERVER = 3;
WorldMap.STATE_PARSE_CONTINUE = 4;
WorldMap.buttonSize = 39;
WorldMap.halfButtonSize = 19;
WorldMap.urHereButtonSize = 15;
WorldMap.urHereHalfButtonSize = 7;
$CountdownTimer = "";
$lastJoinedServer = "";
$lastVURL = "";
function WorldMap::Initialize(%this)
{
    if (!%this.initialized)
    {
        %this.initialized = 1;
        %this.currentCity = "";
        if (getMapType() $= "two_layer")
        {
            %this.setUpCities();
            safeEnsureScriptObject("SimGroup", "WorldMapServerInfoGroup");
        }
    }
    return ;
}
function WorldMap::initCityMaps(%this)
{
    %this.currentCity = "";
    safeEnsureScriptObject("StringMap", "WorldMapCityInfoMap");
    fillCityInfoMap(WorldMapCityInfoMap);
    safeEnsureScriptObject("StringMap", "WorldMapCityNamesMap");
    fillCityNamesMap(WorldMapCityNamesMap);
    return ;
}
$WorldMapCityButtonGroup = 4369;
$TGFCityButtonGroup = 4370;
function WorldMap::getCityButton(%this, %cityName, %alt, %forTGF)
{
    %objName = %cityName @ %alt && %forTGF ? "_small_button" : "_large_button";
    if (%forTGF)
    {
        %objName = %objName @ "_tgf";
    }
    if (isObject(%objName))
    {
        return %objName.getId();
    }
    %info = WorldMapCityInfoMap.get(%cityName);
    if (!isObject(%info))
    {
        return 0;
    }
    %coords = %alt && %forTGF ? %info : %info;
    %bitmap = %alt && %forTGF ? %info : %info;
    %groupNum = -1;
    if (%alt)
    {
        %groupNum = $WorldMapCityButtonGroup;
    }
    else
    {
        if (%forTGF)
        {
            %groupNum = $TGFCityButtonGroup;
        }
    }
    %buttonType = %alt && %forTGF ? "RadioButton" : "PushButton";
    %command = %alt && %forTGF ? "WorldMap.selectCity(" : "WorldMap.getCityButton(";
    %button = new GuiBitmapButtonCtrl(%objName)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = getWords(%coords, 0, 1);
        extent = getWords(%coords, 2, 3);
        minExtent = "1 1";
        visible = 1;
        command = %command;
        text = "";
        groupNum = %groupNum;
        buttonType = %buttonType;
        bitmap = getPathOfButtonResource(%bitmap);
        drawText = 0;
        cityName = %cityName;
    };
    %statusX = getWord(%coords, 0);
    %statusY = (getWord(%coords, 1) + getWord(%coords, 3)) - 20;
    %statusLabel = new GuiMLTextCtrl()
    {
        profile = "ETSLoginMLTextProfile";
        extent = getWord(%coords, 2) SPC 14;
        position = %statusX SPC %statusY;
        visible = 1;
    };
    %button.statusLabel = %statusLabel;
    return %button;
}
function WorldMap::getCityPeepsCtrl(%this, %cityName)
{
    %objName = "geTGF_map_peeps_" @ %cityName;
    if (!isObject(%objName))
    {
        error(getScopeName() SPC "- invalid cityName -" SPC %cityName SPC getTrace());
        return "";
    }
    return %objName.getId();
}
function WorldMap::getVenueButton(%this, %cityName, %venueName)
{
    %cityInfo = WorldMapCityInfoMap.get(%cityName);
    if (!isObject(%cityInfo.venues))
    {
        return 0;
    }
    %venueInfo = %cityInfo.venues.get(%venueName);
    if (!isObject(%venueInfo))
    {
        return 0;
    }
    %objName = "geWorldMapVenueButton_" @ %cityName @ "_" @ %venueInfo.spawnName;
    if (isObject(%objName))
    {
        return %objName.getId();
    }
    %cmd = "WorldMap.selectVenue(\"" @ %cityName @ "\", \"" @ %venueName @ "\", " @ %venueInfo.spawnName @ ");";
    return new GuiBitmapButtonCtrl(%objName)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = getWords(%venueInfo.Coords, 0, 1);
        extent = getWords(%venueInfo.Coords, 2, 3);
        minExtent = "1 1";
        visible = 1;
        command = %cmd;
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = getPathOfButtonResource(%venueInfo.button);
        drawText = 0;
        class = "WorldMapVenueButton";
        venueInfo = %venueInfo;
    };
    return ;
}
function WorldMap::getVenueLabel(%this, %cityName, %venueName)
{
    %cityInfo = WorldMapCityInfoMap.get(%cityName);
    if (!isObject(%cityInfo.venues))
    {
        return 0;
    }
    %venueInfo = %cityInfo.venues.get(%venueName);
    if (!isObject(%venueInfo))
    {
        return 0;
    }
    %objName = %cityName @ "_" @ %venueInfo.spawnName @ "_label";
    if (isObject(%objName))
    {
        return %objName.getId();
    }
    %ctrl = new GuiControl(%objName)
    {
        profile = "GuiModelessDialogProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = "202 20";
        minExtent = "1 1";
        visible = 1;
    };
    %button = %this.getVenueButton(%cityName, %venueName);
    %trgX = (getWord(%button.getPosition(), 0) + (0.5 * getWord(%button.getExtent(), 0))) - (0.5 * getWord(%ctrl.getExtent(), 0));
    %trgY = (getWord(%button.getPosition(), 1) - getWord(%ctrl.getExtent(), 1)) + 8;
    %ctrl.setTrgPosition(%trgX, %trgY);
    return %ctrl;
}
function WorldMap::setUpCities(%this)
{
    %size = WorldMapCityInfoMap.size();
    %i = 0;
    while (%i < %size)
    {
        %cityName = WorldMapCityInfoMap.getValue(%i).name;
        %largeCityButton = %this.getCityButton(%cityName, 0, 0);
        %cityPeepsCtrl = %this.getCityPeepsCtrl(%cityName);
        WorldMapMultiCityLarge.add(%largeCityButton);
        WorldMapStatusPanel.add(%largeCityButton.statusLabel);
        %largeCityButton.statusLabel.setVisible(0);
        if (isObject(packageDownload) && $AutoDownloadPackages)
        {
            %status = packageDownload.getStatusForCity(%cityName);
            if (%status $= "done")
            {
                %largeCityButton.setActive(1);
                %statusText = %this.getFullnessDesc(%largeCityButton.load, %largeCityButton.capacity);
                %largeCityButton.statusLabel.setText("<spush><font:Arial:12>" @ "Status: " @ %statusText @ "<spop>");
                %cityPeepsCtrl.setTextWithStyle("-" SPC %statusText);
            }
        }
        %i = %i + 1;
    }
    WorldMap.UpdateCityStatuses();
    return ;
}
function WorldMap::selectCity(%this, %cityName)
{
    if (%this.currentCity $= %cityName)
    {
        return ;
    }
    %cityInfo = WorldMapCityInfoMap.get(%cityName);
    if (!isObject(%cityInfo))
    {
        warn(getScopeName() SPC "- No info for city" SPC %cityName SPC getTrace());
        return ;
    }
    %this.currentCity = %cityName;
    %this.setView("single_city");
    WorldMapCityBkgd.setBitmap(%cityInfo.background);
    LoadingGui.setBitmap(%cityInfo.background);
    CityDownloadGui.setBitmap(%cityInfo.background);
    WorldMapCityBkgd.clear();
    %cityInfo.venues.forEach("addVenueButton");
    WorldMapCityBkgd.venueButtons = "";
    %count = WorldMapCityBkgd.getCount();
    %i = 0;
    while (%i < %count)
    {
        WorldMapCityBkgd.venueButtons = WorldMapCityBkgd.venueButtons SPC WorldMapCityBkgd.getObject(%i);
        %i = %i + 1;
    }
    WorldMapCityBkgd.setButtonsEnabled(1);
    if (!isObject(CityMapLargeTitleText))
    {
    }
    new GuiMLTextCtrl(CityMapLargeTitleText)
        {
            profile = "MapLargeLabelProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "545 484";
            extent = "130 24";
            minExtent = "1 1";
            visible = 1;
            text = "";
            maxLength = 255;
        };
    WorldMapCityBkgd.add(CityMapLargeTitleText);
    if (roles::maskhaspermission($player::rolesMask, "dressingRoomSpawn"))
    {
        CityMapLargeTitleText.setText("<linkcolor:eeeeee><a:VENUESPAWN" SPC %this.currentCity SPC "DressingRoomSpawns>Go Backstage!</a>");
    }
    else
    {
        CityMapLargeTitleText.setText("");
    }
    if (%this.forTGF)
    {
        geTGF_tabs.Maps_changedCityFilter(%cityName);
    }
    %this.getCityButton(%cityName, 1, 1).performClick();
    %this.getCityButton(%cityName, 1, 0).performClick();
    return ;
}
function CityMapLargeTitleText::onURL(%this, %url)
{
    if (!(getWord(%url, 0) $= "VENUESPAWN"))
    {
        return ;
    }
    %city = getWord(%url, 1);
    %spawn = getWord(%url, 2);
    CityMapLargeTitleText.setText("Connecting...");
    WorldMap.selectVenue(%city, "", %spawn);
    return ;
}
function WorldMap::selectVenue(%this, %cityName, %venueName, %spawnName)
{
    %targetVurl = "vside:/location/" @ %cityName @ "/" @ %spawnName;
    if ($StandAlone)
    {
        commandToServer('TeleportToVURL', %targetVurl);
        geTGF.closeFully();
        return ;
    }
    if (WorldMapServerPopup.isVisible())
    {
        %idx = WorldMapServerPopup.GetSelected();
        if ((%idx < 0) && (%idx >= WorldMapServerInfoGroup.getCount()))
        {
            error("WorldMap::selectVenue(): invalid server selected");
        }
        %serverInfo = WorldMapServerInfoGroup.getObject(%idx);
        if (!(%cityName $= ""))
        {
            $UserPref::WorldMap::ServerChoice[%cityName] = %serverInfo.serverName ;
        }
        %targetVurl = %targetVurl @ "?server0=" @ %serverInfo.serverName;
    }
    vurlOperation(%targetVurl);
    return ;
}
function WorldMap::open(%this)
{
    %this.openTGF(0);
    return ;
}
function WorldMap::openTGF(%this, %forTGF)
{
    %this.Initialize();
    GuiTracker.updateLocation(%this);
    %this.setBitmap("platform/client/ui/worldmapBackground");
    if ((WorldMapCityInfoMap.size() > 1) && !(%this.loggedIn))
    {
        %this.setView("multi_city");
    }
    else
    {
        if (!%this.loggedIn)
        {
            %this.setView("single_city");
        }
        else
        {
            if (!($gContiguousSpaceName $= ""))
            {
                %this.getCityButton($gContiguousSpaceName, 1, 0).performClick();
            }
        }
    }
    %this.setVisible(1);
    if (!%forTGF)
    {
        DestroyMessageBoxes();
        Canvas.setContent(%this);
        pushScreenSize(960, 544, 0, 1, 0);
    }
    %this.forTGF = %forTGF;
    WorldMapCityBkgd.setButtonsEnabled(1);
    %this.refresh();
    return ;
}
function WorldMap::setLoggedIn(%this, %flag)
{
    %this.loggedIn = %flag;
    return ;
}
function WorldMap::setNotConnectedToServer(%this)
{
    disconnectedCleanup("");
    $ServerName = "";
    %this.server = 0;
    $gContiguousSpaceName = "";
    %this.setLoggedIn(0);
    return ;
}
function WorldMap::close(%this)
{
    %this.setVisible(0);
    if (%this.loggedIn)
    {
        Canvas.setContent(PlayGui);
    }
    popScreenSize();
    return ;
}
function WorldMap::setView(%this, %view)
{
    if (%view $= "multi_city")
    {
        WorldMapMultiCityLarge.setVisible(1);
        WorldMapStatusPanel.setVisible(1);
        WorldMapCityBkgd.setVisible(0);
        WorldMapExpandButton.setVisible(0);
        WorldMapChooseLocationLabel.setVisible(0);
        WorldMapServerPopup.setVisible(0);
        WorldMapServerPopupLabel.setVisible(0);
        geTGF_map_header.setVisible(1);
        DevModMapCtrls.setVisible($ETS::devMode);
        %this.currentCity = "";
        if (%this.forTGF)
        {
            geTGF_tabs.Maps_changedCityFilter("");
        }
    }
    else
    {
        if (%view $= "single_city")
        {
            WorldMapMultiCityLarge.setVisible(0);
            WorldMapStatusPanel.setVisible(0);
            WorldMapCityBkgd.setVisible(1);
            if (!%this.forTGF)
            {
                WorldMapExpandButton.setVisible(1);
            }
            WorldMapChooseLocationLabel.setVisible(1);
            WorldMapServerPopup.setVisible(1);
            WorldMapServerPopupLabel.setVisible(1);
            geTGF_map_header.setVisible(0);
            %this.fillServerList();
            DevModMapCtrls.setVisible(0);
        }
    }
    return ;
}
function WorldMap::fillDevModServerList(%this)
{
    %names = "";
    %n = WorldMapServers.getCount() - 1;
    while (%n >= 0)
    {
        %server = WorldMapServers.getObject(%n);
        %name = %server.get("name");
        %array[%name,server] = %server ;
        %array[%name,load] = %server.get("load") ;
        %array[%name,capacity] = %server.get("capacity") ;
        %array[%name,city] = %server.get("city") ;
        %names = %names @ %name @ "\t";
        %n = %n - 1;
    }
    %names = SortFields(%names, 1, 0);
    %list = "";
    %n = getFieldCount(%names) - 1;
    while (%n >= 0)
    {
        %name = getField(%names, %n);
        %load = %array[%name,load];
        %capacity = %array[%name,capacity];
        %city = %array[%name,city];
        %server = %array[%name,server];
        %line = "";
        %line = %line @ "<spush><linkcolor:bbffcc><just:left><a:gamelink " @ %server @ "> " @ %name @ "</a><spop>";
        %line = %line @ "<color:88eedd><just:right>" @ %load @ "/" @ %capacity;
        %list = %list @ %line @ "\n\t";
        %n = %n - 1;
    }
    devModServerListML.setText(%list);
    return ;
}
function WorldMap::fillServerList(%this)
{
    WorldMapServerPopup.clear();
    WorldMapServerInfoGroup.deleteMembers();
    %count = WorldMapServers.getCount();
    %i = 0;
    while (%i < %count)
    {
        %serverProps = WorldMapServers.getObject(%i);
        if (!(%serverProps.get("mappable") $= 0))
        {
            if ((%serverProps.get("city") $= %this.currentCity) && (%serverProps.get("city") $= ""))
            {
                %name = %serverProps.get("name");
                %load = %serverProps.get("load");
                %capacity = %serverProps.get("capacity");
                WorldMapServerPopup.add(%name @ " -- " @ %this.getFullnessDesc(%load, %capacity));
            }
        }
        %i = %i + 1;
    }
    WorldMapServerPopup.SetSelected(0);
    %serverChoice = $UserPref::WorldMap::ServerChoice[WorldMap.currentCity];
    if (!(%serverChoice $= ""))
    {
        %count = WorldMapServerInfoGroup.getCount();
        %i = 0;
        while (%i < %count)
        {
            if (WorldMapServerInfoGroup.getObject(%i).serverName $= %serverChoice)
            {
                WorldMapServerPopup.SetSelected(%i);
                break;
            }
            %i = %i + 1;
        }
    }
    if (WorldMapServerInfoGroup.getCount() > 1)
    {
        WorldMapServerPopup.setVisible(1);
        WorldMapServerPopupLabel.setVisible(1);
        geTGF_map_header.setVisible(0);
    }
    else
    {
        WorldMapServerPopup.setVisible(0);
        WorldMapServerPopupLabel.setVisible(0);
        geTGF_map_header.setVisible(1);
    }
    return ;
}
function WorldMap::onCanvasResize(%this)
{
    %this.update();
    return ;
}
function WorldMap::doServerJoin(%this, %targetVurl)
{
    echo("in doServerJoin: " @ %targetVurl);
    purgeResources();
    fmodShutdown();
    fmodInitialize();
    if (isObject(%this.server))
    {
        echo("in doServerJoin " @ %this);
        %conn = new GameConnection(ServerConnection);
        %conn.setCommonPreconnectClientSettings(%targetVurl);
        $GameConnection = %conn;
        $ServerName = %this.server.get("name");
        %address = %this.server.get("address");
        %port = %this.server.get("port");
        if (!(%port $= ""))
        {
            %address = %address @ ":" @ %port;
        }
        $lastJoinedServer = %address;
        $lastVURL = %targetVurl;
        %conn.connect(%address);
        if (isObject(geMapHud2DTheOrthoMap))
        {
            geMapHud2DTheOrthoMap.deleteMembers();
        }
        %analytic = getAnalytic();
        %analytic.trackPageView("/client/joincity/" @ %this.server.get("city"));
        %this.selectCity(%this.server.get("city"));
        $SpawnTargetSavedVURL = "";
    }
    else
    {
        TransitionCancel(1);
        echo("WorldMap::doServerJoin called when no server set");
    }
    return ;
}
$gWorldMapJoiningServer = 0;
function WorldMap::join(%this, %server, %isATransition, %targetVurl)
{
    if (!isObject(%server))
    {
        return ;
    }
    %a1 = %server.get("address");
    %p1 = %server.get("port");
    echo("connecting to: " @ %a1 @ ":" @ %p1);
    echo("using vurl: " @ %targetVurl);
    $lastJoinedServer = %server;
    $lastVURL = %targetVurl;
    if (isObject(%this.server))
    {
        %a2 = %this.server.get("address");
        %p2 = %this.server.get("port");
        echo("current server:" SPC %a2 @ ":" @ %p2);
        if ((%a1 $= %a2) && (%p1 $= %p2))
        {
            if (!%isATransition)
            {
                geTGF.closeFully();
            }
            commandToServer('TeleportToVURL', %targetVurl);
            return ;
        }
    }
    %this.server = %server;
    if ($gWorldMapJoiningServer)
    {
        warn("WorldMap::join(): multiple clicks on servers in the world map");
        return ;
    }
    $gWorldMapJoiningServer = 1;
    %i = 0;
    while (%i < %this.numServers)
    {
        %this.buttons[%i].setActive(0);
        %i = %i + 1;
    }
    ServerConnection.waitForDisconnect = 0;
    if (isObject(ServerConnection) && (GameConnection::getServerConnection() != -1))
    {
        echo("disconnecting...");
        ServerConnection.waitForDisconnect = 1;
        $SpawnTargetSavedVURL = %targetVurl;
        commandToServer('DisconnectRequest');
        return ;
    }
    %this.doServerJoin(%targetVurl);
    return ;
}
function clientCmdOpenBuildingDirectoryFromCustomSpace(%buildingName)
{
    if (%buildingName $= "")
    {
        %buildingName = $CSBuildingName;
    }
    Canvas.forceRightMouseUp();
    CustomSpacesSelector.open(%buildingName);
    return ;
}
function clientCmdOpenBuildingDirectoryFromCustomSpaceCancel()
{
    return ;
}
function showTransitionMessage(%description, %counter)
{
    TransitionMessage.extent = PlayGui.extent;
    if (%counter <= 0)
    {
        TransitionMessage.text = "";
    }
    else
    {
        TransitionMessage.text = "Wait here for a ride to" SPC %description SPC " in " SPC %counter @ "..\n";
    }
    TransitionMessage.setVisible(1);
    return ;
}
function clientCmdTransitionStartWithMessage(%description, %prompt, %counter, %vurl)
{
    if (%vurl $= "")
    {
        error(getScopeName() SPC "- Description:" SPC %description);
        error(getScopeName() SPC "- Prompt:     " SPC %prompt);
        error(getScopeName() SPC "- Counter:    " SPC %counter);
        error(getScopeName() SPC "- vurl:       " SPC %vurl);
        MessageBoxOK("Transition Error", "Couldn\'t figure out where to take you!\nPlease use the Map to go where you would like to go.", "toggleTGF();");
        return ;
    }
    %cmd = "vurlOperation(\"" @ %vurl @ "\");";
    %msg = "Would you like to take the train to" SPC %description @ "?";
    MessageBoxYesNo("vSide Transit Service", %msg, %cmd, "");
    return ;
}
function TransitionStartWithMessage(%description, %prompt, %counter, %vurl)
{
    showTransitionMessage(%description, %counter);
    $CountdownTimer = schedule(1000, 0, TransitionCountdown, %counter, %description, %vurl);
    return ;
}
function TransitionCountdown(%counter, %description, %vurl)
{
    %counter = %counter - 1;
    showTransitionMessage(%description, %counter);
    if (%counter == -1)
    {
        TransitionMessage.setVisible(0);
        if (!(%vurl $= ""))
        {
            if (geTGF.isVisible())
            {
                geTGF.closeFully();
            }
            if (ClosetGui.isVisible())
            {
                ClosetGui.close(0);
            }
            vurlOperation(%vurl);
        }
        else
        {
            MessageBoxOK("Transition Error", "Couldn\'t figure out where to take you!\nPlease use the Map to go where you would like to go.", "");
            geTGF.openToTabName("Map");
        }
        return ;
    }
    $CountdownTimer = schedule(1000, 0, TransitionCountdown, %counter, %description, %vurl);
    return ;
}
function clientCmdTransitionCancel()
{
    echo("Server calling transition cancel");
    TransitionCancel(0);
    return ;
}
function TransitionCancel(%retry)
{
    cancel($CountdownTimer);
    TransitionMessage.setVisible(0);
    if (isObject($VURL::curVURL))
    {
        if (%retry)
        {
            echo("VURL Transition canceld... retrying next server");
            if ($VURL::curVURL.execute() == 0)
            {
                log("network", "error", "Unable to retry vurl.");
                $VURL::curVURL.delete();
            }
        }
        else
        {
            $VURL::curVURL.delete();
        }
    }
    return ;
}
function clientCmdSetTransition(%destination, %spawnTargetVURL)
{
    prepareForTransition(%destination, %spawnTargetVURL, 0);
    return ;
}
function SetTransition(%destination, %spawnTargetVURL)
{
    prepareForTransition(%destination, %spawnTargetVURL, 1);
    return ;
}
function getServerInstance(%cityNameLongOrShort)
{
    if (gCityNamesShortToLongMap.hasKey(%cityNameLongOrShort))
    {
        %cityNameLong = gCityNamesShortToLongMap.get(%cityNameLongOrShort);
    }
    else
    {
        %cityNameLong = %cityNameLongOrShort;
    }
    %strLen = strlen(%cityNameLong);
    %mostSpaceSvr = 0;
    %mostSpaceAmt = 0;
    %n = WorldMapServers.getCount() - 1;
    while (%n >= 0)
    {
        %serverObj = WorldMapServers.getObject(%n);
        %ServerName = %serverObj.get("name");
        %matches = !strnicmp(%cityNameLong, %ServerName, %strLen);
        if (%matches)
        {
            %availSpace = %serverObj.get("capacity") - %serverObj.get("load");
            if (%availSpace > %mostSpaceAmt)
            {
                %mostSpaceAmt = %availSpace;
                %mostSpaceSvr = %serverObj;
            }
        }
        %n = %n - 1;
    }
    if (%mostSpaceSvr == 0)
    {
        warn("could not find server for" SPC %cityNameLong);
    }
    return %mostSpaceSvr;
}
function prepareForTransition(%destination, %spawnTargetVURL, %pauseForScreenshot)
{
    %serverObj = getServerInstance(%destination);
    if (!isObject(%serverObj))
    {
        TransitionCancel(1);
        error(getScopeName() @ ": did not find server for " @ %destination);
        return ;
    }
    if (%pauseForScreenshot)
    {
        doTransitionAfterFrames(2, %serverObj, %spawnTargetVURL);
    }
    else
    {
        doTransition(%serverObj, %spawnTargetVURL);
    }
    return ;
}
$gTransitionScreenshotSchedule = 0;
$gTransitionScreenshotLastFrame = -1;
function doTransitionAfterFrames(%frames, %ServerName, %spawnTargetVURL)
{
    cancel($gTransitionScreenshotSchedule);
    if ($gTransitionScreenshotLastFrame <= 0)
    {
        $gTransitionScreenshotLastFrame = $Canvas::frameCount;
    }
    %delta = $Canvas::frameCount - $gTransitionScreenshotLastFrame;
    if (%delta >= %frames)
    {
        $gTransitionScreenshotLastFrame = -1;
        doTransition(%ServerName, %spawnTargetVURL);
    }
    else
    {
        $gTransitionScreenshotSchedule = schedule(10, 0, doTransitionAfterFrames, %frames, %ServerName, %spawnTargetVURL);
    }
    return ;
}
function doTransition(%server, %spawnTargetVURL)
{
    Canvas.cursorOff();
    $TransitionScreenshot.shootMemory("GRAYSCALE");
    Canvas.cursorOn();
    WorldMap.join(%server, 1, %spawnTargetVURL);
    return ;
}
function WorldMap::cleanUpServers(%this)
{
    %curServerObjId = 0;
    %savedServer = "";
    if (isObject(%this.server))
    {
        %curServerObjId = %this.server.getId();
    }
    %i = 0;
    while (%i < WorldMapServers.getCount())
    {
        %server = WorldMapServers.getObject(%i);
        if (%server.getId() == %curServerObjId)
        {
            %savedServer = %server;
            WorldMapServers.remove(%server);
        }
        else
        {
            if (isObject(%server.buddies))
            {
                %server.buddies.delete();
            }
            %i = %i + 1;
        }
    }
    WorldMapServers.deleteMembers();
    if (isObject(%savedServer))
    {
        WorldMapServers.add(%savedServer);
    }
    return ;
}
function WorldMap::refresh(%this)
{
    if (!haveValidManagerHost())
    {
        return ;
    }
    %this.requestMapData();
    return ;
}
function WorldMap::exit(%this)
{
    if (isObject(ServerConnection))
    {
        ServerConnection.delete();
    }
    if (isFunction("Using_DF") && Using_DF())
    {
        endDFZone();
    }
    purgeResources();
    fmodShutdown();
    Canvas.setContent(LoginGui);
    return ;
}
function WorldMap::update(%this)
{
    %this.clearCities();
    WorldMapYouAreHereImg.setVisible(0);
    if (!isObject(WorldMapServers))
    {
        return ;
    }
    %sc = WorldMapServers.getCount();
    %i = 0;
    while (%i < %sc)
    {
        %server = WorldMapServers.getObject(%i);
        if (isObject(%server))
        {
            %ismappable = %server.get("mappable");
            if (!(%ismappable $= 0))
            {
                WorldMap.addCity(%server);
            }
        }
        %i = %i + 1;
    }
    %this.adjustButtons();
    MapCityHud.setVisible(0);
    if (WorldMapCityBkgd.isVisible())
    {
        %this.fillServerList();
    }
    if (isObject(devModServerListML))
    {
        %this.fillDevModServerList();
    }
    BuddyHudTabs.updateUserListUnknownServerName();
    return ;
}
function WorldMap::adjustButtons(%this)
{
    %winWidth = getWord($UserPref::Video::Resolution, 0);
    %size = WorldMap.buttonSize;
    %i = 0;
    while (%i < %this.numServers)
    {
        %button = %this.buttons[%i];
        %x = getWord(%button.getPosition(), 0);
        %y = getWord(%button.getPosition(), 1);
        %button.resize(%x, %y, %size, %size);
        %i = %i + 1;
    }
}

function WorldMap::clearCities(%this)
{
    %sc = %this.getCount();
    %i = %sc - 1;
    while (%i >= 0)
    {
        %city = %this.getObject(%i);
        if (isObject(%city))
        {
            if (!(%city.server $= ""))
            {
                %this.remove(%city);
                %city.delete();
            }
        }
        %i = %i - 1;
    }
    %this.numServers = 0;
    return ;
}
function WorldMap::unnormalize(%this, %location)
{
    %location = %location SPC 0;
    %box = "0 0 0" SPC $UserPref::Video::Resolution;
    %location = mUnnormalizePointFromBox(%location, %box);
    %location = getWords(%location, 0, 1);
    %location = mFloor(getWord(%location, 0)) SPC mFloor(getWord(%location, 1));
    return %location;
}
function WorldMap::validateSpot(%this, %locOrig)
{
    %retries = 5;
    %valid = 0;
    %loc = %locOrig;
    %try = 0;
    while (%try < %retries)
    {
        %valid = 1;
        %n = 0;
        while (%n < %this.numServers)
        {
            if (VectorDist(%loc, %this.buttons[%n].position) < 8)
            {
                %valid = 0;
            }
            %n = %n + 1;
        }
        if (!%valid)
        {
            %locX = getWord(%locOrig, 0) + getRandom(-15, 15);
            %locY = getWord(%locOrig, 1) + getRandom(-15, 15);
            %loc = %locX SPC %locY;
        }
        %try = %try + 1;
    }
    return %loc;
}
function WorldMap::addCity(%this, %server)
{
    if (getMapType() $= "single_spawnpoint")
    {
        %this.addCity1(%server);
    }
    return ;
}
function WorldMap::addCity1(%this, %server)
{
    %centerLoc = %this.unnormalize(%server.get("location"));
    %buttonLoc = getWord(%centerLoc, 0) - WorldMap.halfButtonSize SPC getWord(%centerLoc, 1) - WorldMap.halfButtonSize;
    %buttonLoc = %this.validateSpot(%buttonLoc);
    %load = %server.get("load");
    %capacity = %server.get("capacity");
    %fullness = mClamp(mFloor((8 * %load) / %capacity), 0, 8);
    %size = WorldMap.buttonSize;
    %button = new GuiBitmapButtonCtrl(WorldMapServerButton)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %buttonLoc;
        extent = %size SPC %size;
        minExtent = "8 8";
        visible = 1;
        command = "WorldMap.join(" @ %server @ ", false, \"\");";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "platform/client/buttons/UI_serverstate_0" @ %fullness;
        drawText = 0;
        server = %server;
        destName = %server.get("name");
    };
    %this.buttons[%this.numServers] = %button;
    %this.numServers = %this.numServers + 1;
    %this.add(%button);
    if (%server.get("name") $= $ServerName)
    {
        %urHereLoc = getWord(%centerLoc, 0) - WorldMap.urHereHalfButtonSize SPC getWord(%centerLoc, 1) - WorldMap.urHereHalfButtonSize;
        WorldMapYouAreHereImg.reposition(getWord(%urHereLoc, 0), getWord(%urHereLoc, 1));
        WorldMapYouAreHereImg.setVisible(1);
    }
    return ;
}
function WorldMap::showPopup(%this, %city)
{
    %server = %city.server;
    if (!isObject(%server))
    {
        return ;
    }
    %winWidth = getWord($UserPref::Video::Resolution, 0);
    %scaleFactor = %winWidth / 960;
    %destName = %city.destName;
    %address = %server.get("address");
    %capacity = %server.get("capacity");
    %load = %server.get("load");
    %port = %server.get("port");
    MapHudCityText.setText(%destName);
    if (isObject(packageDownload) && $AutoDownloadPackages)
    {
        %status = packageDownload.getStatusForCity(%city);
        if (%status $= "done")
        {
            %statusText = %this.getFullnessDesc(%load, %capacity);
        }
        else
        {
            %statusText = "Downloading!";
        }
    }
    else
    {
        %statusText = %this.getFullnessDesc(%load, %capacity);
    }
    MapHudMetaText.setText("Status: " @ %statusText);
    %top = getWord(%city.getPosition(), 1);
    %left = getWord(%city.getPosition(), 0);
    %bottom = %top + getWord(%city.getExtent(), 1);
    %right = %left + getWord(%city.getExtent(), 0);
    %hudwidth = getWord(MapCityHud.getExtent(), 0);
    %hudheight = getWord(MapCityHud.getExtent(), 1);
    %pos = %left + 36 SPC %top + 5;
    %xPos = getWord(%pos, 0);
    %ypos = getWord(%pos, 1);
    MapCityHud.reposition(%xPos, %ypos);
    MapCityHud.setVisible(1);
    %this.pushToBack(MapCityHud);
    %this.pushToBack(%city);
    return ;
}
function WorldMap::getFullnessDesc(%this, %load, %capacity)
{
    if (%capacity < 1)
    {
        %fullnessDesc = "offline";
    }
    else
    {
        if (%load <= 50)
        {
            %fullnessDesc = "Chillin\'";
        }
        else
        {
            if (%load <= 125)
            {
                %fullnessDesc = "Groovin\'";
            }
            else
            {
                if (%load <= 200)
                {
                    %fullnessDesc = "Hoppin\'";
                }
                else
                {
                    if (%load <= 249)
                    {
                        %fullnessDesc = "Packed";
                    }
                    else
                    {
                        if (%load <= 349)
                        {
                            %fullnessDesc = "Slammed";
                        }
                        else
                        {
                            %fullnessDesc = "Sold Out";
                        }
                    }
                }
            }
        }
    }
    if (%load > 50)
    {
        %fullnessDesc = %fullnessDesc @ " (" @ %load @ ")";
    }
    return %fullnessDesc;
}
function MapCityHud::setDepressed(%this, %flag)
{
    if (%flag)
    {
    }
    return ;
}
function WorldMapServerButton::onMouseDown(%this)
{
    MapCityHud.setDepressed(1);
    return ;
}
function WorldMapServerButton::onMouseUp(%this)
{
    MapCityHud.setDepressed(0);
    return ;
}
function WorldMapServerButton::onMouseEnter(%this, %unused, %unused, %unused)
{
    WorldMap.showPopup(%this);
    return ;
}
function WorldMapServerButton::onMouseLeave(%this)
{
    MapCityHud.setVisible(0);
    return ;
}
function WorldMap::setCitiesActive(%this, %flag)
{
    %count = WorldMapMultiCityLarge.getCount();
    %i = 0;
    while (%i < %count)
    {
        WorldMapMultiCityLarge.getObject(%i).setActive(%flag);
        %i = %i + 1;
    }
}

function WorldMap::UpdateCityStatuses(%this)
{
    if (!WorldMap.isVisible())
    {
        return ;
    }
    %csn = "gw";
    %hasAccess = 0;
    %hasAccess = %hasAccess | (gUserPropMgrClient.getProperty($Player::Name, "level started count " @ %csn, 0) < 10);
    %hasAccess = %hasAccess | roles::maskhaspermission($player::rolesMask, "gatewaySpawn");
    %hasAccess = %hasAccess | $ETS::devMode;
    if (!isObject(WorldMapServers))
    {
        new SimGroup(WorldMapServers);
        RootGroup.add(WorldMapServers);
    }
    %n = WorldMapServers.getCount() - 1;
    while (%n >= 0)
    {
        %serverProps = WorldMapServers.getObject(%n);
        if (!%hasAccess && (%serverProps.get("city") $= %csn))
        {
            %serverProps.delete();
        }
        %n = %n - 1;
    }
    %count = WorldMapMultiCityLarge.getCount();
    %i = 0;
    while (%i < %count)
    {
        %buttonBig = WorldMapMultiCityLarge.getObject(%i);
        %buttonSml = TGFWorldMapMultiCitySmall.citybutton[%buttonBig.cityName];
        %statusTxtCtrl = "geTGF_map_peeps_" @ %buttonBig.cityName;
        %buttonBig.load = 0;
        %buttonBig.capacity = 0;
        %buttonBig.numServers = 0;
        %buttonBig.setActive(0);
        %statusTxtCtrl.style = "tgfMapCityPeepsInactive";
        if (isObject(%buttonSml))
        {
            %buttonSml.setActive(0);
        }
        if (!isObject(WorldMapServers))
        {
            new SimGroup(WorldMapServers);
            RootGroup.add(WorldMapServers);
        }
        %n = WorldMapServers.getCount() - 1;
        while (%n >= 0)
        {
            %serverProps = WorldMapServers.getObject(%n);
            if (%buttonBig.cityName $= %serverProps.get("city"))
            {
                %buttonBig.setActive(1);
                %statusTxtCtrl.style = "tgfMapCityPeeps";
                if (isObject(%buttonSml))
                {
                    %buttonSml.setActive(1);
                }
                %buttonBig.load = %buttonBig.load + %serverProps.get("load");
                %buttonBig.capacity = %buttonBig.capacity + %serverProps.get("capacity");
                %buttonBig.servers[%buttonBig.numServers] = %serverProps;
                %buttonBig.numServers = %buttonBig.numServers + 1;
            }
            %n = %n - 1;
        }
        if (isObject(packageDownload) && $AutoDownloadPackages)
        {
            %dlStatus = packageDownload.getStatusForCity(%buttonBig.cityName);
            if (%dlStatus $= "done")
            {
                %statusText = %this.getFullnessDesc(%buttonBig.load, %buttonBig.capacity);
            }
            else
            {
                %statusText = "Downloading!";
                %buttonBig.setActive(0);
                %statusTxtCtrlCtrl.style = "tgfMapCityPeepsInactive";
                if (isObject(%buttonSml))
                {
                    %buttonSml.setActive(0);
                }
            }
        }
        else
        {
            %statusText = %this.getFullnessDesc(%buttonBig.load, %buttonBig.capacity);
        }
        %buttonBig.statusLabel.setText("<spush><font:Arial:12>" @ "Status: " @ %statusText @ "<spop>");
        %statusTxtCtrl.style = "tgfMapCityPeeps";
        %statusTxtCtrl.setTextWithStyle("-" SPC %statusText);
        %i = %i + 1;
    }
}

function WorldMap::cityNameForServerName(%this, %name)
{
    if (!isObject(WorldMapServers))
    {
        return "";
    }
    %count = WorldMapServers.getCount();
    %i = 0;
    while (%i < %count)
    {
        %server = WorldMapServers.getObject(%i);
        if (%server.get("name") $= %name)
        {
            return %server.get("city");
        }
        %i = %i + 1;
    }
    return "";
}
function WorldMap::IsApartmentServerForServerName(%this, %name)
{
    if (!isObject(WorldMapServers))
    {
        return 0;
    }
    %count = WorldMapServers.getCount();
    %i = 0;
    while (%i < %count)
    {
        %server = WorldMapServers.getObject(%i);
        if (%server.get("name") $= %name)
        {
            return !%server.get("mappable");
        }
        %i = %i + 1;
    }
    return 0;
}
function WorldMap::isServerForCity(%this, %name)
{
    if (!isObject(WorldMapServers))
    {
        return 0;
    }
    %count = WorldMapServers.getCount();
    %i = 0;
    while (%i < %count)
    {
        %server = WorldMapServers.getObject(%i);
        if (%server.get("city") $= %name)
        {
            return 1;
        }
        %i = %i + 1;
    }
    return 0;
}
function WorldMap::onWake(%this)
{
    return ;
}
$gRefreshWorldMapTimer = "";
function refreshWorldMap()
{
    cancel($gRefreshWorldMapTimer);
    $gRefreshWorldMapTimer = "";
    if (!isObject(WorldMap) && !WorldMap.isAwake())
    {
        return ;
    }
    WorldMap.setUpCities();
    $gRefreshWorldMapTimer = schedule(2000, 0, "refreshWorldMap");
    return ;
}
function WorldMap::requestMapData(%this)
{
    if (!isObject(WorldMapServers))
    {
        new SimGroup(WorldMapServers);
        RootGroup.add(WorldMapServers);
    }
    %mapRequest = safeEnsureScriptObject("URLPostObject", "MapRequest");
    if (!((%mapRequest.isActive $= "")) && (%mapRequest.isActive == 1))
    {
        return ;
    }
    %mapRequest.setURL($Net::ClientServiceURL @ "/WorldMapRefresh");
    %mapRequest.setURLParam("user", $Player::Name);
    %mapRequest.setURLParam("token", $Token);
    %mapRequest.setURLParam("version", getProtocolVersion());
    %mapRequest.setCompletedCallback("MapRequestOnCompleted");
    log("communication", "debug", "sending request for map data.");
    %mapRequest.isActive = 1;
    %mapRequest.start();
    return ;
}
function MapRequestOnCompleted(%request, %result)
{
    %request.isActive = 0;
    if (%result == 0)
    {
        WorldMap.parseResult(%request);
        WorldMap.update();
    }
    else
    {
        if (%result == $CURL::CouldNotConnect)
        {
            MessageBoxOK("Connection Error", $MsgCat::network["E-SERVER-CONNECT"], "");
        }
        else
        {
            if (%result == $CURL::CouldNotResolveHost)
            {
                MessageBoxOK("Could Not Find Server", $MsgCat::network["E-SERVER-DNS"], "");
            }
            else
            {
                MessageBoxOK("Server Unavailable", $MsgCat::network["E-SERVER-UNAVAIL"], "");
            }
        }
    }
    %request.schedule(0, "delete");
    %vurl = getSkipMapVurl(1);
    if (!(%vurl $= ""))
    {
        vurlOperation(%vurl);
    }
    return ;
}
function WorldMap::parseResult(%this, %request)
{
    WorldMap.cleanUpServers();
    if (WorldMapServers.getCount() == 0)
    {
        %savedServer = "";
        %savedName = "";
    }
    else
    {
        %savedServer = WorldMapServers.getObject(0);
        %savedName = %savedServer.get("name");
    }
    %numServers = %request.getResult("serverCount");
    if (%numServers == 0)
    {
        log("communication", "error", "no servers returned in map response");
        MessageBoxOK("Server Unavailable", $MsgCat::network["E-SERVER-UNAVAIL"], "");
        return ;
    }
    else
    {
        %fields = "address capacity city description load location mappable name port version";
        %i = 0;
        while (%i < %numServers)
        {
            %ServerName = %request.getResult("server" @ %i @ ".name");
            if ((%ServerName $= %savedName) && !((%savedServer $= "")))
            {
                %serverProps = %savedServer;
            }
            else
            {
                %serverProps = new StringMap();
                if (isObject(MissionCleanup))
                {
                    MissionCleanup.add(%serverProps);
                }
            }
            %j = 0;
            while (%j < getWordCount(%fields))
            {
                %field = getWord(%fields, %j);
                %name = "server" @ %i @ "." @ %field;
                %value = %request.getResult(%name);
                log("communication", "debug", "adding server prop: " @ %field @ " = " @ %value);
                %serverProps.put(%field, %value);
                %j = %j + 1;
            }
            if (%serverProps.get("city") $= "")
            {
                %name = %serverProps.get("name");
                %count = WorldMapCityNamesMap.size();
                %j = 0;
                while (%j < %count)
                {
                    %key = WorldMapCityNamesMap.getKey(%j);
                    if (stricmp(%key, %name) == 0)
                    {
                        %value = WorldMapCityNamesMap.getValue(%j);
                        %serverProps.put("city", %value);
                        break;
                    }
                    %j = %j + 1;
                }
            }
            WorldMapServers.add(%serverProps);
            %i = %i + 1;
        }
    }
    %this.TabulateWorldAreaSummary();
    %this.UpdateCityStatuses();
    return ;
}
function getSkipMapVurl(%bChangeUI)
{
    %ret = "";
    if (!($VURLcmd $= ""))
    {
        %ret = $VURLcmd;
        if (%bChangeUI)
        {
            $VURLcmd = "";
        }
    }
    if (!isDefined("$gTriedToAutoConnectOnceAlready" @ $Player::Name))
    {
        $gTriedToAutoConnectOnceAlready[$Player::Name] = 0;
    }
    if (!$gTriedToAutoConnectOnceAlready[$Player::Name])
    {
        log("communication", "debug", "Checking for autodest.");
        if (%bChangeUI)
        {
            $gTriedToAutoConnectOnceAlready[$Player::Name] = 1;
        }
        if ((gUserPropMgrClient.getProperty($Player::Name, "level started count gw", 0) == 0) && !roles::maskhaspermission($player::rolesMask, "gatewaySpawn"))
        {
            if ($ETS::devMode)
            {
                if (%bChangeUI)
                {
                    MessageBoxOK("Not going to gateway..", "Ordinarily, you would have been\nautomatically take to gateway here,\nbut since you\'re devmode, you\'re not.", "");
                }
            }
            else
            {
                %ret = "vside:/location/gw/mapSpawns_entry";
                if (%bChangeUI)
                {
                    WorldMap.selectCity("gw");
                    geTGF.selectTab("Maps");
                }
            }
        }
    }
    log("communication", "debug", getScopeName() SPC "- \"" @ %ret @ "\".");
    return %ret;
}
function VenuesMap::addVenueButton(%this, %key, %value)
{
    WorldMapCityBkgd.add(WorldMap.getVenueButton(WorldMap.currentCity, %key));
    WorldMapCityBkgd.add(WorldMap.getVenueLabel(WorldMap.currentCity, %key));
    return ;
}
function WorldMapVenueButton::onMouseEnter(%this)
{
    %bitmapName = strreplace(%this.venueInfo.name, "\'", "");
    %bitmapName = WorldMap.currentCity @ "_" @ strreplace(%bitmapName, " ", "_");
    %bitmap = getPathOfButtonResource("platform/client/ui/spawn_info/" @ %bitmapName);
    if (!(%bitmap $= ""))
    {
        WorldMapDetails.setBitmap(%bitmap);
        WorldMapDetails.fitSize();
        WorldMapDetails.reposition(getWord(%this.getPosition(), 0) + getWord(%this.getExtent(), 0), getWord(%this.getPosition(), 1) + 6);
        WorldMapDetails.setVisible(1);
    }
    return ;
}
function WorldMapVenueButton::onMouseLeave(%this)
{
    WorldMapDetails.setVisible(0);
    return ;
}
function WorldMapCityBkgd::setButtonsEnabled(%this, %flag)
{
    %count = getWordCount(%this.venueButtons);
    %i = 0;
    while (%i < %count)
    {
        %button = getWord(%this.venueButtons, %i);
        if (isObject(%button))
        {
            %button.setActive(%flag);
        }
        %i = %i + 1;
    }
}

function devModServerListML::onURL(%this, %url)
{
    if (!(firstWord(%url) $= "gamelink"))
    {
        return ;
    }
    %serverObj = getWord(%url, 1);
    geTGF.closeFully();
    WorldMap.join(%serverObj, 0, "");
    return ;
}
function devModServerListML::onRightURL(%this, %url)
{
    if (!(firstWord(%url) $= "gamelink"))
    {
        return ;
    }
    %serverObj = getWord(%url, 1);
    %serverObj.dumpValues();
    return ;
}
function gotVURLCommandLineList(%arg)
{
    log("communication", "debug", "found the VURL argument list and it is " @ %arg);
    %url = "";
    if ($Platform $= "macos")
    {
        %url = %arg;
    }
    %count = getWordCount(%arg);
    %i = 0;
    while (%i < %count)
    {
        %value = getWord(%arg, %i);
        if (%value $= "-url")
        {
            %url = getWord(%arg, %i + 1);
            break;
        }
        %i = %i + 1;
    }
    log("communication", "debug", "value of URL is " @ %url);
    log("communication", "debug", "now URL is " @ %url);
    $VURLcmd = %url;
    if (((isObject(LoginGui) && ($Token $= "")) && isObject(WorldMap)) && !(WorldMap.loggedIn))
    {
        LoginGui.setControlsActive(0);
        LoginProgressBarCtrls.setVisible(1);
        $Player::Name = trim($Player::Name);
        LoginUserNameField.setValue($Player::Name);
        if ($UserPref::Login::RememberMe)
        {
            $UserPref::Player::Name = $Player::Name;
            $UserPref::Player::Password = $Player::Password;
        }
        else
        {
            $UserPref::Player::Name = "";
            $UserPref::Player::Password = "";
        }
        LoginGui.envManagerLogin();
    }
    if (!($VURLcmd $= ""))
    {
        vurlOperation($VURLcmd);
    }
    return ;
}
