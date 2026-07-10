STATE_PARSE_RESULT = 1 @ WorldMap;
STATE_PARSE_SERVER_COUNT = 2 @ WorldMap;
STATE_PARSE_SERVER = 3 @ WorldMap;
STATE_PARSE_CONTINUE = 4 @ WorldMap;
buttonSize = 39 @ WorldMap;
halfButtonSize = 19 @ WorldMap;
urHereButtonSize = 15 @ WorldMap;
urHereHalfButtonSize = 7 @ WorldMap;
$CountdownTimer = "";
$lastJoinedServer = "";
$lastVURL = "";
function WorldMap::Initialize(%this) {
    if (!(%this.initialized)) {
        %this.initialized = 1;
        %this.currentCity = "";
        if ((getMapType() $= "two_layer")) {
            %this.setUpCities();
            safeEnsureScriptObject("SimGroup", "WorldMapServerInfoGroup");
        }
    }
};
function WorldMap::initCityMaps(%this) {
    %this.currentCity = "";
    safeEnsureScriptObject("StringMap", "WorldMapCityInfoMap");
    fillCityInfoMap(WorldMapCityInfoMap);
    safeEnsureScriptObject("StringMap", "WorldMapCityNamesMap");
    fillCityNamesMap(WorldMapCityNamesMap);
};
$WorldMapCityButtonGroup = 4369;
$TGFCityButtonGroup = 4370;
function WorldMap::getCityButton(%this, %cityName, %alt, %forTGF) {
    if (%alt) {
    }
    %objName = %cityName @ %forTGF ? "_small_button" : "_large_button";
    if (%forTGF) {
        %objName = %objName @ "_tgf";
    }
    if (isObject(%objName)) {
        return %objName.getId();
    }
    %info = WorldMapCityInfoMap.get(%cityName);
    if (!(isObject(%info))) {
        return 0;
    }
    if (%alt) {
    }
    if (%forTGF) {
    }
    %coords = %info.Coords;
    %info.altCoords;
    if (%alt) {
    }
    if (%forTGF) {
    }
    %bitmap = %info.button;
    %info.altButton;
    %groupNum = -1;
    if (%alt) {
        %groupNum = $WorldMapCityButtonGroup;
    }
    if (%forTGF) {
        %groupNum = $TGFCityButtonGroup;
    }
    if (%alt) {
    }
    %buttonType = %forTGF ? "RadioButton" : "PushButton";
    if (%alt) {
    }
    if (%forTGF) {
    }
    %command = "WorldMap.getCityButton(" @ %cityName @ ", true, false).performClick();";
    "WorldMap.selectCity(" @ %cityName @ ");";
    %button = new GuiBitmapButtonCtrl(%objName) {
        profile = 0 @ "GuiDefaultProfile";
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
    %statusY = (20.0 - (getWord(%coords, 3) + getWord(%coords, 1)));
    %statusLabel = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSLoginMLTextProfile";
        extent = getWord(%coords, 2) @ " " @ 14;
        position = %statusX @ " " @ %statusY;
        visible = 1;
    };
    %button.statusLabel = %statusLabel;
    return %button;
};
function WorldMap::getCityPeepsCtrl(%this, %cityName) {
    %objName = "geTGF_map_peeps_" @ %cityName;
    if (!(isObject(%objName))) {
        error(getScopeName() @ " " @ "- invalid cityName -" @ " " @ %cityName @ " " @ getTrace());
        return "";
    }
    return %objName.getId();
};
function WorldMap::getVenueButton(%this, %cityName, %venueName) {
    %cityInfo = WorldMapCityInfoMap.get(%cityName);
    if (!(isObject(%cityInfo.venues))) {
        return 0;
    }
    %venueInfo = %cityInfo.venues.get(%venueName);
    if (!(isObject(%venueInfo))) {
        return 0;
    }
    %objName = "geWorldMapVenueButton_" @ %cityName @ "_" @ %venueInfo.spawnName;
    if (isObject(%objName)) {
        return %objName.getId();
    }
    %cmd = "WorldMap.selectVenue(\"" @ %cityName @ "\", \"" @ %venueName @ "\", " @ %venueInfo.spawnName @ ");";
    return new GuiBitmapButtonCtrl(%objName) {
        profile = 0 @ "GuiDefaultProfile";
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
    };;
};
function WorldMap::getVenueLabel(%this, %cityName, %venueName) {
    %cityInfo = WorldMapCityInfoMap.get(%cityName);
    if (!(isObject(%cityInfo.venues))) {
        return 0;
    }
    %venueInfo = %cityInfo.venues.get(%venueName);
    if (!(isObject(%venueInfo))) {
        return 0;
    }
    %objName = %cityName @ "_" @ %venueInfo.spawnName @ "_label";
    if (isObject(%objName)) {
        return %objName.getId();
    }
    %ctrl = new GuiControl(%objName) {
        profile = 0 @ "GuiModelessDialogProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = "202 20";
        minExtent = "1 1";
        visible = 1;
    };
    new GuiMLTextCtrl("") {
        profile = new GuiMLTextCtrl("") {
        profile = "ETSVenueNameMLTextProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "1 1";
        extent = "200 18";
        minExtent = "1 1";
        visible = 1;
        text = "<just:center><color:000000ff><font:BauhausStd-Demi:19>" @ %venueName;
        lineSpacing = 2;
        allowColorChars = 1;
        maxChars = -1;
        stripTagsOnCopy = 1;
    }; @ "ETSVenueNameMLTextProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = "200 18";
        minExtent = "1 1";
        visible = 1;
        text = "<just:center><color:ffffffc8><font:BauhausStd-Demi:19>" @ %venueName;
        lineSpacing = 2;
        allowColorChars = 1;
        maxChars = -1;
        stripTagsOnCopy = 1;
    };
    %button = %this.getVenueButton(%cityName, %venueName);
    %trgX = ((getWord(%ctrl.getExtent(), 0) * 0.5) - ((getWord(%button.getExtent(), 0) * 0.5) + getWord(%button.getPosition(), 0)));
    %trgY = (8.0 + (getWord(%ctrl.getExtent(), 1) - getWord(%button.getPosition(), 1)));
    %ctrl.setTrgPosition(%trgX, %trgY);
    return %ctrl;
};
function WorldMap::setUpCities(%this) {
    %size = WorldMapCityInfoMap.size();
    %i = 0;
    if ((%size < %i)) {
        %cityName = WorldMapCityInfoMap.getValue(%i).name;
        %largeCityButton = %this.getCityButton(%cityName, 0, 0);
        %cityPeepsCtrl = %this.getCityPeepsCtrl(%cityName);
        WorldMapMultiCityLarge.add(%largeCityButton);
        WorldMapStatusPanel.add(%largeCityButton.statusLabel);
        %largeCityButton.statusLabel.setVisible(0);
        if (isObject(packageDownload)) {
        }
        if ($AutoDownloadPackages) {
            %status = packageDownload.getStatusForCity(%cityName);
            if ((%status $= "done")) {
                %largeCityButton.setActive(1);
                %statusText = %this.getFullnessDesc(%largeCityButton.load, %largeCityButton.capacity);
                %largeCityButton.statusLabel.setText("<spush><font:Arial:12>" @ "Status: " @ %statusText @ "<spop>");
                %cityPeepsCtrl.setTextWithStyle("-" @ " " @ %statusText);
            }
        }
        %i = (1.0 + %i);
    }
    WorldMap.UpdateCityStatuses();
};
function WorldMap::selectCity(%this, %cityName) {
    if ((%this.currentCity $= %cityName)) {
        return;
    }
    %cityInfo = WorldMapCityInfoMap.get(%cityName);
    if (!(isObject(%cityInfo))) {
        warn(getScopeName() @ " " @ "- No info for city" @ " " @ %cityName @ " " @ getTrace());
        return;
    }
    %this.currentCity = %cityName;
    %this.setView("single_city");
    WorldMapCityBkgd.setBitmap(%cityInfo.background);
    LoadingGui.setBitmap(%cityInfo.background);
    CityDownloadGui.setBitmap(%cityInfo.background);
    WorldMapCityBkgd.clear();
    %cityInfo.venues.forEach("addVenueButton");
    %cityInfo.venueButtons = "" @ WorldMapCityBkgd;
    %count = WorldMapCityBkgd.getCount();
    %i = 0;
    if ((%count < %i)) {
        %cityInfo.venueButtons = %cityInfo.venueButtons @ " " @ WorldMapCityBkgd.getObject(%i) @ WorldMapCityBkgd;
        WorldMapCityBkgd;
        %i = (1.0 + %i);
    }
    WorldMapCityBkgd.setButtonsEnabled(1);
    if (!(isObject(CityMapLargeTitleText))) {
        new GuiMLTextCtrl(CityMapLargeTitleText) {
            profile = (%count < %i) @ "MapLargeLabelProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "545 484";
            extent = "130 24";
            minExtent = "1 1";
            visible = 1;
            text = "";
            maxLength = 255;
        };
    }
    WorldMapCityBkgd.add(CityMapLargeTitleText);
    if (roles::maskhaspermission($player::rolesMask, "dressingRoomSpawn")) {
        CityMapLargeTitleText.setText("<linkcolor:eeeeee><a:VENUESPAWN" @ " " @ %this.currentCity @ " " @ "DressingRoomSpawns>Go Backstage!</a>");
    }
    CityMapLargeTitleText.setText("");
    if (%this.forTGF) {
        geTGF_tabs.Maps_changedCityFilter(%cityName);
    }
    CityMapLargeTitleText.performClick(%this.getCityButton(%cityName, 1, 1));
    CityMapLargeTitleText.performClick(%this.getCityButton(%cityName, 1, 0));
};
function CityMapLargeTitleText::onURL(%this, %url) {
    if (!(getWord(%url, 0) $= "VENUESPAWN")) {
        return;
    }
    %city = getWord(%url, 1);
    %spawn = getWord(%url, 2);
    CityMapLargeTitleText.setText("Connecting...");
    WorldMap.selectVenue(%city, "", %spawn);
};
function WorldMap::selectVenue(%this, %cityName, %venueName, %spawnName) {
    %targetVurl = "vside:/location/" @ %cityName @ "/" @ %spawnName;
    if ($StandAlone) {
        commandToServer('TeleportToVURL', %targetVurl);
        geTGF.closeFully();
        return;
    }
    if (WorldMapServerPopup.isVisible()) {
        %idx = WorldMapServerPopup.GetSelected();
        if ((0.0 < %idx)) {
        }
        if ((WorldMapServerInfoGroup.getCount() >= %idx)) {
            error("WorldMap::selectVenue(): invalid server selected");
        }
        %serverInfo = WorldMapServerInfoGroup.getObject(%idx);
        if (!(%cityName $= "")) {
            %cityName[$UserPref::WorldMap::ServerChoice @ %cityName] = %serverInfo.serverName;
        }
        %targetVurl = %targetVurl @ "?server0=" @ %serverInfo.serverName;
    }
    vurlOperation(%targetVurl);
};
function WorldMap::open(%this) {
    %this.openTGF(0);
};
function WorldMap::openTGF(%this, %forTGF) {
    %this.Initialize();
    GuiTracker.updateLocation(%this);
    %this.setBitmap("platform/client/ui/worldmapBackground");
    if ((1.0 > WorldMapCityInfoMap.size())) {
    }
    if (!(%this.loggedIn)) {
        %this.setView("multi_city");
    }
    if (!(%this.loggedIn)) {
        %this.setView("single_city");
    }
    if (!($gContiguousSpaceName $= "")) {
        %this.getCityButton($gContiguousSpaceName, 1, 0).performClick();
    }
    %this.setVisible(1);
    if (!(%forTGF)) {
        DestroyMessageBoxes();
        Canvas.setContent(%this);
        pushScreenSize(960, 544, 0, 1, 0);
    }
    %this.forTGF = %forTGF;
    WorldMapCityBkgd.setButtonsEnabled(1);
    %this.refresh();
};
function WorldMap::setLoggedIn(%this, %flag) {
    %this.loggedIn = %flag;
};
function WorldMap::setNotConnectedToServer(%this) {
    disconnectedCleanup("");
    $ServerName = "";
    %this.server = 0;
    $gContiguousSpaceName = "";
    %this.setLoggedIn(0);
};
function WorldMap::close(%this) {
    %this.setVisible(0);
    if (%this.loggedIn) {
        Canvas.setContent(PlayGui);
    }
    popScreenSize();
};
function WorldMap::setView(%this, %view) {
    if ((%view $= "multi_city")) {
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
        if (%this.forTGF) {
            geTGF_tabs.Maps_changedCityFilter("");
        }
    }
    if ((%view $= "single_city")) {
        WorldMapMultiCityLarge.setVisible(0);
        WorldMapStatusPanel.setVisible(0);
        WorldMapCityBkgd.setVisible(1);
        if (!(%this.forTGF)) {
            WorldMapExpandButton.setVisible(1);
        }
        WorldMapChooseLocationLabel.setVisible(1);
        WorldMapServerPopup.setVisible(1);
        WorldMapServerPopupLabel.setVisible(1);
        geTGF_map_header.setVisible(0);
        %this.fillServerList();
        DevModMapCtrls.setVisible(0);
    }
};
function WorldMap::fillDevModServerList(%this) {
    %names = "";
    %n = (1.0 - WorldMapServers.getCount());
    if ((0.0 >= %n)) {
        %server = WorldMapServers.getObject(%n);
        %name = %server.get("name");
        %name[%array TAB %name @ server] = %server;
        %name[%array TAB %name @ load] = %server.get("load");
        %name[%array TAB %name @ capacity] = %server.get("capacity");
        %name[%array TAB %name @ city] = %server.get("city");
        %names = %names @ %name @ "\t";
        %n = (1.0 - %n);
    }
    %names = SortFields(%names, 1, 0);
    (0.0 >= %n);
    %list = "";
    %n = (1.0 - getFieldCount(%names));
    if ((0.0 >= %n)) {
        %name = getField(%names, %n);
        %load = %name[%array TAB %name @ load];
        %capacity = %name[%array TAB %name @ capacity];
        %city = %name[%array TAB %name @ city];
        %server = %name[%array TAB %name @ server];
        %line = "";
        %line = %line @ "<spush><linkcolor:bbffcc><just:left><a:gamelink " @ %server @ "> " @ %name @ "</a><spop>";
        %line = %line @ "<color:88eedd><just:right>" @ %load @ "/" @ %capacity;
        %list = %list @ %line @ "\n\t";
        %n = (1.0 - %n);
    }
    devModServerListML.setText(%list);
};
function WorldMap::fillServerList(%this) {
    WorldMapServerPopup.clear();
    WorldMapServerInfoGroup.deleteMembers();
    %count = WorldMapServers.getCount();
    %i = 0;
    if ((%count < %i)) {
        %serverProps = WorldMapServers.getObject(%i);
        if (!(%serverProps.get("mappable") $= 0)) {
            if ((%serverProps.get("city") $= %this.currentCity)) {
            }
            if ((%serverProps.get("city") $= "")) {
                %name = %serverProps.get("name");
                %load = %serverProps.get("load");
                %capacity = %serverProps.get("capacity");
                WorldMapServerInfoGroup;
                new SimObject("") {
                    server = 0 @ %serverProps;
                    serverName = %name;
                };.add();
                WorldMapServerPopup.add(%name @ " -- " @ %this.getFullnessDesc(%load, %capacity));
            }
        }
        %i = (1.0 + %i);
    }
    WorldMapServerPopup.SetSelected(0);
    %serverChoice = $UserPref::WorldMap::ServerChoice;
    (%count < %i);
    if (!(%serverChoice $= "")) {
        %count = WorldMapServerInfoGroup.getCount();
        %i = 0;
        if ((%count < %i)) {
            if ((WorldMapServerInfoGroup.getObject(%i).serverName $= %serverChoice)) {
                WorldMapServerPopup.SetSelected(%i);
            }
            %i = (1.0 + %i);
        }
    }
    if ((1.0 > WorldMapServerInfoGroup.getCount())) {
        WorldMapServerPopup.setVisible(1);
        WorldMapServerPopupLabel.setVisible(1);
        geTGF_map_header.setVisible(0);
    }
    WorldMapServerPopup.setVisible(0);
    WorldMapServerPopupLabel.setVisible(0);
    geTGF_map_header.setVisible(1);
};
function WorldMap::onCanvasResize(%this) {
    %this.update();
};
function WorldMap::doServerJoin(%this, %targetVurl) {
    echo("in doServerJoin: " @ %targetVurl);
    purgeResources();
    fmodShutdown();
    fmodInitialize();
    if (isObject(%this.server)) {
        echo("in doServerJoin " @ %this);
        %conn = new GameConnection(ServerConnection);;
        %conn.setCommonPreconnectClientSettings(%targetVurl);
        $GameConnection = %conn;
        $ServerName = %this.server.get("name");
        %address = %this.server.get("address");
        %port = %this.server.get("port");
        if (!(%port $= "")) {
            %address = %address @ ":" @ %port;
        }
        $lastJoinedServer = %address;
        $lastVURL = %targetVurl;
        %conn.connect(%address);
        if (isObject(geMapHud2DTheOrthoMap)) {
            geMapHud2DTheOrthoMap.deleteMembers();
        }
        %analytic = getAnalytic();
        %analytic.trackPageView("/client/joincity/" @ %this.server.get("city"));
        %this.selectCity(%this.server.get("city"));
        $SpawnTargetSavedVURL = "";
    }
    TransitionCancel(1);
    echo("WorldMap::doServerJoin called when no server set");
};
$gWorldMapJoiningServer = 0;
function WorldMap::join(%this, %server, %isATransition, %targetVurl) {
    if (!(isObject(%server))) {
        return;
    }
    %a1 = %server.get("address");
    %p1 = %server.get("port");
    echo("connecting to: " @ %a1 @ ":" @ %p1);
    echo("using vurl: " @ %targetVurl);
    $lastJoinedServer = %server;
    $lastVURL = %targetVurl;
    if (isObject(%this.server)) {
        %a2 = %this.server.get("address");
        %p2 = %this.server.get("port");
        echo("current server:" @ " " @ %a2 @ ":" @ %p2);
        if ((%a1 $= %a2)) {
        }
        if ((%p1 $= %p2)) {
            if (!(%isATransition)) {
                geTGF.closeFully();
            }
            commandToServer('TeleportToVURL', %targetVurl);
            return;
        }
    }
    %this.server = %server;
    if ($gWorldMapJoiningServer) {
        warn("WorldMap::join(): multiple clicks on servers in the world map");
        return;
    }
    $gWorldMapJoiningServer = 1;
    %i = 0;
    if ((%this.numServers < %i)) {
        %i.setActive(%this.buttons, 0);
        %i = (1.0 + %i);
    }
    %this.waitForDisconnect = 0 @ ServerConnection;
    (%this.numServers < %i);
    if (isObject(ServerConnection)) {
    }
    if ((-(1.0) != GameConnection::getServerConnection())) {
        echo("disconnecting...");
        %this.waitForDisconnect = 1 @ ServerConnection;
        $SpawnTargetSavedVURL = %targetVurl;
        commandToServer('DisconnectRequest');
        return;
    }
    %this.doServerJoin(%targetVurl);
};
function clientCmdOpenBuildingDirectoryFromCustomSpace(%buildingName) {
    if ((%buildingName $= "")) {
        %buildingName = $CSBuildingName;
    }
    Canvas.forceRightMouseUp();
    CustomSpacesSelector.open(%buildingName);
};
function clientCmdOpenBuildingDirectoryFromCustomSpaceCancel() {
};
function showTransitionMessage(%description, %counter) {
    %this.extent = %this.extent @ TransitionMessage;
    PlayGui;
    if ((0.0 <= %counter)) {
        %this.text = "" @ TransitionMessage;
    }
    %this.text = "Wait here for a ride to" @ " " @ %description @ " " @ " in " @ " " @ %counter @ "..\n" @ TransitionMessage;
    TransitionMessage.setVisible(1);
};
function clientCmdTransitionStartWithMessage(%description, %prompt, %counter, %vurl) {
    if ((%vurl $= "")) {
        error(getScopeName() @ " " @ "- Description:" @ " " @ %description);
        error(getScopeName() @ " " @ "- Prompt:     " @ " " @ %prompt);
        error(getScopeName() @ " " @ "- Counter:    " @ " " @ %counter);
        error(getScopeName() @ " " @ "- vurl:       " @ " " @ %vurl);
        MessageBoxOK("Transition Error", "Couldn't figure out where to take you!\nPlease use the Map to go where you would like to go.", "toggleTGF();");
        return;
    }
    %cmd = "vurlOperation(\"" @ %vurl @ "\");";
    %msg = "Would you like to take the train to" @ " " @ %description @ "?";
    MessageBoxYesNo("vSide Transit Service", %msg, %cmd, "");
};
function TransitionStartWithMessage(%description, %prompt, %counter, %vurl) {
    showTransitionMessage(%description, %counter);
    $CountdownTimer = schedule(1000, 0, TransitionCountdown, %counter, %description, %vurl);
};
function TransitionCountdown(%counter, %description, %vurl) {
    %counter = (1.0 - %counter);
    showTransitionMessage(%description, %counter);
    if ((-(1.0) == %counter)) {
        TransitionMessage.setVisible(0);
        if (!(%vurl $= "")) {
            if (geTGF.isVisible()) {
                geTGF.closeFully();
            }
            if (ClosetGui.isVisible()) {
                ClosetGui.close(0);
            }
            vurlOperation(%vurl);
        }
        MessageBoxOK("Transition Error", "Couldn't figure out where to take you!\nPlease use the Map to go where you would like to go.", "");
        geTGF.openToTabName("Map");
        return;
    }
    $CountdownTimer = schedule(1000, 0, TransitionCountdown, %counter, %description, %vurl);
};
function clientCmdTransitionCancel() {
    echo("Server calling transition cancel");
    TransitionCancel(0);
};
function TransitionCancel(%retry) {
    cancel($CountdownTimer);
    TransitionMessage.setVisible(0);
    if (isObject($VURL::curVURL)) {
        if (%retry) {
            echo("VURL Transition canceld... retrying next server");
            if ((0.0 == $VURL::curVURL.execute())) {
                log("network", "error", "Unable to retry vurl.");
                $VURL::curVURL.delete();
            }
        }
        $VURL::curVURL.delete();
    }
};
function clientCmdSetTransition(%destination, %spawnTargetVURL) {
    prepareForTransition(%destination, %spawnTargetVURL, 0);
};
function SetTransition(%destination, %spawnTargetVURL) {
    prepareForTransition(%destination, %spawnTargetVURL, 1);
};
function getServerInstance(%cityNameLongOrShort) {
    if (gCityNamesShortToLongMap.hasKey(%cityNameLongOrShort)) {
        %cityNameLong = gCityNamesShortToLongMap.get(%cityNameLongOrShort);
    }
    %cityNameLong = %cityNameLongOrShort;
    %strLen = strlen(%cityNameLong);
    %mostSpaceSvr = 0;
    %mostSpaceAmt = 0;
    %n = (1.0 - WorldMapServers.getCount());
    if ((0.0 >= %n)) {
        %serverObj = WorldMapServers.getObject(%n);
        %ServerName = %serverObj.get("name");
        %matches = !(strnicmp(%cityNameLong, %ServerName, %strLen));
        if (%matches) {
            %availSpace = (%serverObj.get("load") - %serverObj.get("capacity"));
            if ((%mostSpaceAmt > %availSpace)) {
                %mostSpaceAmt = %availSpace;
                %mostSpaceSvr = %serverObj;
            }
        }
        %n = (1.0 - %n);
    }
    if ((0.0 == %mostSpaceSvr)) {
        warn("could not find server for" @ " " @ %cityNameLong);
    }
    return %mostSpaceSvr;
};
function prepareForTransition(%destination, %spawnTargetVURL, %pauseForScreenshot) {
    %serverObj = getServerInstance(%destination);
    if (!(isObject(%serverObj))) {
        TransitionCancel(1);
        error(getScopeName() @ ": did not find server for " @ %destination);
        return;
    }
    if (%pauseForScreenshot) {
        doTransitionAfterFrames(2, %serverObj, %spawnTargetVURL);
    }
    doTransition(%serverObj, %spawnTargetVURL);
};
$gTransitionScreenshotSchedule = 0;
$gTransitionScreenshotLastFrame = -(1.0);
function doTransitionAfterFrames(%frames, %ServerName, %spawnTargetVURL) {
    cancel($gTransitionScreenshotSchedule);
    if ((0.0 <= $gTransitionScreenshotLastFrame)) {
        $gTransitionScreenshotLastFrame = $Canvas::frameCount;
    }
    %delta = ($gTransitionScreenshotLastFrame - $Canvas::frameCount);
    if ((%frames >= %delta)) {
        $gTransitionScreenshotLastFrame = -(1.0);
        doTransition(%ServerName, %spawnTargetVURL);
    }
    $gTransitionScreenshotSchedule = schedule(10, 0, doTransitionAfterFrames, %frames, %ServerName, %spawnTargetVURL);
};
function doTransition(%server, %spawnTargetVURL) {
    Canvas.cursorOff();
    $TransitionScreenshot.shootMemory("GRAYSCALE");
    Canvas.cursorOn();
    WorldMap.join(%server, 1, %spawnTargetVURL);
};
function WorldMap::cleanUpServers(%this) {
    %curServerObjId = 0;
    %savedServer = "";
    if (isObject(%this.server)) {
        %curServerObjId = %this.server.getId();
    }
    %i = 0;
    if ((WorldMapServers.getCount() < %i)) {
        %server = WorldMapServers.getObject(%i);
        if ((%curServerObjId == %server.getId())) {
            %savedServer = %server;
            WorldMapServers.remove(%server);
        }
        if (isObject(%server.buddies)) {
            %server.buddies.delete();
        }
        %i = (1.0 + %i);
    }
    WorldMapServers.deleteMembers();
    if (isObject(%savedServer)) {
        WorldMapServers.add(%savedServer);
    }
};
function WorldMap::refresh(%this) {
    if (!(haveValidManagerHost())) {
        return;
    }
    %this.requestMapData();
};
function WorldMap::exit(%this) {
    if (isObject(ServerConnection)) {
        ServerConnection.delete();
    }
    if (isFunction("Using_DF")) {
    }
    if (Using_DF()) {
        endDFZone();
    }
    purgeResources();
    fmodShutdown();
    Canvas.setContent(LoginGui);
};
function WorldMap::update(%this) {
    %this.clearCities();
    WorldMapYouAreHereImg.setVisible(0);
    if (!(isObject(WorldMapServers))) {
        return;
    }
    %sc = WorldMapServers.getCount();
    %i = 0;
    if ((%sc < %i)) {
        %server = WorldMapServers.getObject(%i);
        if (isObject(%server)) {
            %ismappable = %server.get("mappable");
            if (!(%ismappable $= 0)) {
                WorldMap.addCity(%server);
            }
        }
        %i = (1.0 + %i);
    }
    %this.adjustButtons();
    MapCityHud.setVisible(0);
    if (WorldMapCityBkgd.isVisible()) {
        %this.fillServerList();
    }
    if (isObject(devModServerListML)) {
        %this.fillDevModServerList();
    }
    BuddyHudTabs.updateUserListUnknownServerName();
};
function WorldMap::adjustButtons(%this) {
    %winWidth = getWord($UserPref::Video::Resolution, 0);
    %size = %server.buttonSize;
    WorldMap;
    %i = 0;
    if ((%this.numServers < %i)) {
        %button = %this.buttons;
        %i;
        %x = getWord(%button.getPosition(), 0);
        %y = getWord(%button.getPosition(), 1);
        %button.resize(%x, %y, %size, %size);
        %i = (1.0 + %i);
    }
};
function WorldMap::clearCities(%this) {
    %sc = %this.getCount();
    %i = (1.0 - %sc);
    if ((0.0 >= %i)) {
        %city = %this.getObject(%i);
        if (isObject(%city)) {
            if (!(%city.server $= "")) {
                %this.remove(%city);
                %city.delete();
            }
        }
        %i = (1.0 - %i);
    }
    %this.numServers = (0.0 >= %i) @ 0;
};
function WorldMap::unnormalize(%this, %location) {
    %location = %location @ " " @ 0;
    %box = "0 0 0" @ " " @ $UserPref::Video::Resolution;
    %location = mUnnormalizePointFromBox(%location, %box);
    %location = getWords(%location, 0, 1);
    %location = mFloor(getWord(%location, 0)) @ " " @ mFloor(getWord(%location, 1));
    return %location;
};
function WorldMap::validateSpot(%this, %locOrig) {
    %retries = 5;
    %valid = 0;
    %loc = %locOrig;
    %try = 0;
    if (!(%valid)) {
    }
    if ((%retries < %try)) {
        %valid = 1;
        %n = 0;
        if ((%this.numServers < %n)) {
            if ((8.0 @ %n < VectorDist(%loc, %this.buttons.position))) {
                %valid = 0;
            }
            %n = (1.0 + %n);
        }
        if (!(%valid)) {
            %locX = (getRandom(-(15.0), 15) + getWord(%locOrig, 0));
            (%this.numServers < %n);
            %locY = (getRandom(-(15.0), 15) + getWord(%locOrig, 1));
            %loc = %locX @ " " @ %locY;
        }
        %try = (1.0 + %try);
        if (!(%valid)) {
        }
    }
    return %loc;
};
function WorldMap::addCity(%this, %server) {
    if ((getMapType() $= "single_spawnpoint")) {
        %this.addCity1(%server);
    }
};
function WorldMap::addCity1(%this, %server) {
    %centerLoc = %this.unnormalize(%server.get("location"));
    %buttonLoc = WorldMap @ (%this.halfButtonSize - getWord(%centerLoc, 1));
    (%this.halfButtonSize - getWord(%centerLoc, 0)) @ " ";
    %buttonLoc = %this.validateSpot(%buttonLoc);
    WorldMap;
    %load = %server.get("load");
    %capacity = %server.get("capacity");
    %fullness = mClamp(mFloor((%capacity / (%load * 8.0))), 0, 8);
    %size = %this.buttonSize;
    WorldMap;
    %button = new GuiBitmapButtonCtrl(WorldMapServerButton) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %buttonLoc;
        extent = %size @ " " @ %size;
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
    %this.buttons = %button @ %this.numServers;
    %this.numServers = (1.0 + %this.numServers);
    %this.add(%button);
    if ((%server.get("name") $= $ServerName)) {
        %urHereLoc = WorldMap @ (%this.urHereHalfButtonSize - getWord(%centerLoc, 1));
        (%this.urHereHalfButtonSize - getWord(%centerLoc, 0)) @ " ";
        WorldMapYouAreHereImg.reposition(getWord(%urHereLoc, 0), getWord(%urHereLoc, 1));
        WorldMapYouAreHereImg.setVisible(1);
    }
};
function WorldMap::showPopup(%this, %city) {
    %server = %city.server;
    if (!(isObject(%server))) {
        return;
    }
    %winWidth = getWord($UserPref::Video::Resolution, 0);
    %scaleFactor = (960.0 / %winWidth);
    %destName = %city.destName;
    %address = %server.get("address");
    %capacity = %server.get("capacity");
    %load = %server.get("load");
    %port = %server.get("port");
    MapHudCityText.setText(%destName);
    if (isObject(packageDownload)) {
    }
    if ($AutoDownloadPackages) {
        %status = packageDownload.getStatusForCity(%city);
        if ((%status $= "done")) {
            %statusText = %this.getFullnessDesc(%load, %capacity);
        }
        %statusText = "Downloading!";
    }
    %statusText = %this.getFullnessDesc(%load, %capacity);
    MapHudMetaText.setText("Status: " @ %statusText);
    %top = getWord(%city.getPosition(), 1);
    %left = getWord(%city.getPosition(), 0);
    %bottom = (getWord(%city.getExtent(), 1) + %top);
    %right = (getWord(%city.getExtent(), 0) + %left);
    %hudwidth = getWord(MapCityHud.getExtent(), 0);
    %hudheight = getWord(MapCityHud.getExtent(), 1);
    %pos = (36.0 + %left) @ " " @ (5.0 + %top);
    %xPos = getWord(%pos, 0);
    %ypos = getWord(%pos, 1);
    MapCityHud.reposition(%xPos, %ypos);
    MapCityHud.setVisible(1);
    %this.pushToBack();
    %this.pushToBack(%city);
};
function WorldMap::getFullnessDesc(%this, %load, %capacity) {
    if ((1.0 < %capacity)) {
        %fullnessDesc = "offline";
    }
    if ((50.0 <= %load)) {
        %fullnessDesc = "Chillin'";
    }
    if ((125.0 <= %load)) {
        %fullnessDesc = "Groovin'";
    }
    if ((200.0 <= %load)) {
        %fullnessDesc = "Hoppin'";
    }
    if ((249.0 <= %load)) {
        %fullnessDesc = "Packed";
    }
    if ((349.0 <= %load)) {
        %fullnessDesc = "Slammed";
    }
    %fullnessDesc = "Sold Out";
    if ((50.0 > %load)) {
        %fullnessDesc = %fullnessDesc @ " (" @ %load @ ")";
    }
    return %fullnessDesc;
};
function MapCityHud::setDepressed(%this, %flag) {
};
function WorldMapServerButton::onMouseDown(%this) {
    MapCityHud.setDepressed(1);
};
function WorldMapServerButton::onMouseUp(%this) {
    MapCityHud.setDepressed(0);
};
function WorldMapServerButton::onMouseEnter(%this, %unused, %unused, %unused) {
    WorldMap.showPopup(%this);
};
function WorldMapServerButton::onMouseLeave(%this) {
    MapCityHud.setVisible(0);
};
function WorldMap::setCitiesActive(%this, %flag) {
    %count = WorldMapMultiCityLarge.getCount();
    %i = 0;
    if ((%count < %i)) {
        WorldMapMultiCityLarge.getObject(%i).setActive(%flag);
        %i = (1.0 + %i);
    }
};
function WorldMap::UpdateCityStatuses(%this) {
    if (!(WorldMap.isVisible())) {
        return;
    }
    %csn = "gw";
    %hasAccess = 0;
    %hasAccess = ((10.0 < gUserPropMgrClient.getProperty($Player::Name, "level started count " @ %csn, 0)) | %hasAccess);
    %hasAccess = (roles::maskhaspermission($player::rolesMask, "gatewaySpawn") | %hasAccess);
    %hasAccess = ($ETS::devMode | %hasAccess);
    if (!(isObject(WorldMapServers))) {
        new SimGroup(WorldMapServers);
        RootGroup.add(WorldMapServers);
    }
    %n = (1.0 - WorldMapServers.getCount(WorldMapServers));
    if ((0.0 >= %n)) {
        %serverProps = WorldMapServers.getObject(%n);
        if (!(%hasAccess)) {
        }
        if ((%serverProps.get("city") $= %csn)) {
            %serverProps.delete();
        }
        %n = (1.0 - %n);
    }
    %count = WorldMapMultiCityLarge.getCount();
    (0.0 >= %n);
    %i = 0;
    if ((%count < %i)) {
        %buttonBig = WorldMapMultiCityLarge.getObject(%i);
        %buttonSml = %buttonBig.citybutton;
        %buttonBig.cityName @ TGFWorldMapMultiCitySmall;
        %statusTxtCtrl = "geTGF_map_peeps_" @ %buttonBig.cityName;
        %buttonBig.load = 0;
        %buttonBig.capacity = 0;
        %buttonBig.numServers = 0;
        %buttonBig.setActive(0);
        %statusTxtCtrl.style = "tgfMapCityPeepsInactive";
        if (isObject(%buttonSml)) {
            %buttonSml.setActive(0);
        }
        if (!(isObject(WorldMapServers))) {
            new SimGroup(WorldMapServers);
            RootGroup.add(WorldMapServers);
        }
        %n = (1.0 - WorldMapServers.getCount(WorldMapServers));
        if ((0.0 >= %n)) {
            %serverProps = WorldMapServers.getObject(%n);
            if ((%buttonBig.cityName $= %serverProps.get("city"))) {
                %buttonBig.setActive(1);
                %statusTxtCtrl.style = "tgfMapCityPeeps";
                if (isObject(%buttonSml)) {
                    %buttonSml.setActive(1);
                }
                %buttonBig.load = (%serverProps.get("load") + %buttonBig.load);
                %buttonBig.capacity = (%serverProps.get("capacity") + %buttonBig.capacity);
                %buttonBig.servers = %serverProps @ %buttonBig.numServers;
                %buttonBig.numServers = (1.0 + %buttonBig.numServers);
            }
            %n = (1.0 - %n);
        }
        if (isObject(packageDownload)) {
        }
        if ($AutoDownloadPackages) {
            %dlStatus = packageDownload.getStatusForCity(%buttonBig.cityName);
            (0.0 >= %n);
            if ((%dlStatus $= "done")) {
                %statusText = %this.getFullnessDesc(%buttonBig.load, %buttonBig.capacity);
            }
            %statusText = "Downloading!";
            %buttonBig.setActive(0);
            %statusTxtCtrlCtrl.style = "tgfMapCityPeepsInactive";
            if (isObject(%buttonSml)) {
                %buttonSml.setActive(0);
            }
        }
        %statusText = %this.getFullnessDesc(%buttonBig.load, %buttonBig.capacity);
        %buttonBig.statusLabel.setText("<spush><font:Arial:12>" @ "Status: " @ %statusText @ "<spop>");
        %statusTxtCtrl.style = "tgfMapCityPeeps";
        %statusTxtCtrl.setTextWithStyle("-" @ " " @ %statusText);
        %i = (1.0 + %i);
    }
};
function WorldMap::cityNameForServerName(%this, %name) {
    if (!(isObject(WorldMapServers))) {
        return "";
    }
    %count = WorldMapServers.getCount();
    %i = 0;
    if ((%count < %i)) {
        %server = WorldMapServers.getObject(%i);
        if ((%server.get("name") $= %name)) {
            return %server.get("city");
        }
        %i = (1.0 + %i);
    }
    return "";
};
function WorldMap::IsApartmentServerForServerName(%this, %name) {
    if (!(isObject(WorldMapServers))) {
        return 0;
    }
    %count = WorldMapServers.getCount();
    %i = 0;
    if ((%count < %i)) {
        %server = WorldMapServers.getObject(%i);
        if ((%server.get("name") $= %name)) {
            return !(%server.get("mappable"));
        }
        %i = (1.0 + %i);
    }
    return 0;
};
function WorldMap::isServerForCity(%this, %name) {
    if (!(isObject(WorldMapServers))) {
        return 0;
    }
    %count = WorldMapServers.getCount();
    %i = 0;
    if ((%count < %i)) {
        %server = WorldMapServers.getObject(%i);
        if ((%server.get("city") $= %name)) {
            return 1;
        }
        %i = (1.0 + %i);
    }
    return 0;
};
function WorldMap::onWake(%this) {
};
$gRefreshWorldMapTimer = "";
function refreshWorldMap() {
    cancel($gRefreshWorldMapTimer);
    $gRefreshWorldMapTimer = "";
    if (!(isObject(WorldMap))) {
    }
    if (!(WorldMap.isAwake())) {
        return;
    }
    WorldMap.setUpCities();
    $gRefreshWorldMapTimer = schedule(2000, 0, "refreshWorldMap");
};
function WorldMap::requestMapData(%this) {
    if (!(isObject(WorldMapServers))) {
        new SimGroup(WorldMapServers);
        RootGroup.add(WorldMapServers);
    }
    %mapRequest = safeEnsureScriptObject("URLPostObject", "MapRequest");
    if (!(%mapRequest.isActive $= "")) {
    }
    if ((1.0 == %mapRequest.isActive)) {
        return;
    }
    %mapRequest.setURL($Net::ClientServiceURL @ "/WorldMapRefresh");
    %mapRequest.setURLParam("user", $Player::Name);
    %mapRequest.setURLParam("token", $Token);
    %mapRequest.setURLParam("version", getProtocolVersion());
    %mapRequest.setCompletedCallback("MapRequestOnCompleted");
    log("communication", "debug", "sending request for map data.");
    %mapRequest.isActive = 1;
    %mapRequest.start();
};
function MapRequestOnCompleted(%request, %result) {
    %request.isActive = 0;
    if ((0.0 == %result)) {
        WorldMap.parseResult(%request);
        WorldMap.update();
    }
    if (($CURL::CouldNotConnect == %result)) {
        MessageBoxOK("Connection Error", , "");
    }
    if (($CURL::CouldNotResolveHost == %result)) {
        MessageBoxOK("Could Not Find Server", , "");
    }
    MessageBoxOK("Server Unavailable", , "");
    %request.schedule(0, "delete");
    %vurl = getSkipMapVurl(1);
    if (!(%vurl $= "")) {
        vurlOperation(%vurl);
    }
};
function WorldMap::parseResult(%this, %request) {
    WorldMap.cleanUpServers();
    if ((0.0 == WorldMapServers.getCount())) {
        %savedServer = "";
        %savedName = "";
    }
    %savedServer = WorldMapServers.getObject(0);
    %savedName = %savedServer.get("name");
    %numServers = %request.getResult("serverCount");
    if ((0.0 == %numServers)) {
        log("communication", "error", "no servers returned in map response");
        MessageBoxOK("Server Unavailable", , "");
        return;
    }
    %fields = "address capacity city description load location mappable name port version";
    %i = 0;
    if ((%numServers < %i)) {
        %ServerName = %request.getResult("server" @ %i @ ".name");
        if ((%ServerName $= %savedName)) {
        }
        if (!(%savedServer $= "")) {
            %serverProps = %savedServer;
        }
        %serverProps = new StringMap("");;
        0;
        if (isObject(MissionCleanup)) {
            MissionCleanup.add(%serverProps);
        }
        %j = 0;
        if ((getWordCount(%fields) < %j)) {
            %field = getWord(%fields, %j);
            %name = "server" @ %i @ "." @ %field;
            %value = %request.getResult(%name);
            log("communication", "debug", "adding server prop: " @ %field @ " = " @ %value);
            %serverProps.put(%field, %value);
            %j = (1.0 + %j);
        }
        if (((getWordCount(%fields) < %j) @ " " @ %serverProps.get("city") $= "")) {
            %name = %serverProps.get("name");
            %count = WorldMapCityNamesMap.size();
            %j = 0;
            if ((%count < %j)) {
                %key = WorldMapCityNamesMap.getKey(%j);
                if ((0.0 == stricmp(%key, %name))) {
                    %value = WorldMapCityNamesMap.getValue(%j);
                    %serverProps.put("city", %value);
                }
                %j = (1.0 + %j);
            }
        }
        WorldMapServers.add(%serverProps);
        %i = (1.0 + %i);
        (%count < %j);
    }
    %this.TabulateWorldAreaSummary();
    %this.UpdateCityStatuses();
};
function getSkipMapVurl(%bChangeUI) {
    %ret = "";
    if (!($VURLcmd $= "")) {
        %ret = $VURLcmd;
        if (%bChangeUI) {
            $VURLcmd = "";
        }
    }
    if (!(isDefined("$gTriedToAutoConnectOnceAlready" @ $Player::Name))) {
        $Player::Name[$gTriedToAutoConnectOnceAlready @ $Player::Name] = 0;
    }
    if (!($Player::Name[$gTriedToAutoConnectOnceAlready @ $Player::Name])) {
        log("communication", "debug", "Checking for autodest.");
        if (%bChangeUI) {
            $Player::Name[$gTriedToAutoConnectOnceAlready @ $Player::Name] = 1;
        }
        if ((0.0 == gUserPropMgrClient.getProperty($Player::Name, "level started count gw", 0))) {
        }
        if (!(roles::maskhaspermission($player::rolesMask, "gatewaySpawn"))) {
            if ($ETS::devMode) {
                if (%bChangeUI) {
                    MessageBoxOK("Not going to gateway..", "Ordinarily, you would have been\nautomatically take to gateway here,\nbut since you're devmode, you're not.", "");
                }
            }
            %ret = "vside:/location/gw/mapSpawns_entry";
            if (%bChangeUI) {
                WorldMap.selectCity("gw");
                geTGF.selectTab("Maps");
            }
        }
    }
    log("communication", "debug", getScopeName() @ " " @ "- \"" @ %ret @ "\".");
    return %ret;
};
function VenuesMap::addVenueButton(%this, %key, %value) {
    WorldMapCityBkgd.add(WorldMap.getVenueButton(WorldMap, %request.currentCity, %key));
    WorldMapCityBkgd.add(WorldMap.getVenueLabel(WorldMap, %request.currentCity, %key));
};
function WorldMapVenueButton::onMouseEnter(%this) {
    %bitmapName = strreplace(%this.venueInfo.name, "'", "");
    %bitmapName = %this.venueInfo.currentCity @ "_" @ strreplace(%bitmapName, " ", "_");
    WorldMap;
    %bitmap = getPathOfButtonResource("platform/client/ui/spawn_info/" @ %bitmapName);
    if (!(%bitmap $= "")) {
        WorldMapDetails.setBitmap(%bitmap);
        WorldMapDetails.fitSize();
        WorldMapDetails.reposition((getWord(%this.getExtent(), 0) + getWord(%this.getPosition(), 0)), (6.0 + getWord(%this.getPosition(), 1)));
        WorldMapDetails.setVisible(1);
    }
};
function WorldMapVenueButton::onMouseLeave(%this) {
    WorldMapDetails.setVisible(0);
};
function WorldMapCityBkgd::setButtonsEnabled(%this, %flag) {
    %count = getWordCount(%this.venueButtons);
    %i = 0;
    if ((%count < %i)) {
        %button = getWord(%this.venueButtons, %i);
        if (isObject(%button)) {
            %button.setActive(%flag);
        }
        %i = (1.0 + %i);
    }
};
function devModServerListML::onURL(%this, %url) {
    if (!(firstWord(%url) $= "gamelink")) {
        return;
    }
    %serverObj = getWord(%url, 1);
    geTGF.closeFully();
    WorldMap.join(%serverObj, 0, "");
};
function devModServerListML::onRightURL(%this, %url) {
    if (!(firstWord(%url) $= "gamelink")) {
        return;
    }
    %serverObj = getWord(%url, 1);
    %serverObj.dumpValues();
};
function gotVURLCommandLineList(%arg) {
    log("communication", "debug", "found the VURL argument list and it is " @ %arg);
    %url = "";
    if (($Platform $= "macos")) {
        %url = %arg;
    }
    %count = getWordCount(%arg);
    %i = 0;
    if ((%count < %i)) {
        %value = getWord(%arg, %i);
        if ((%value $= "-url")) {
            %url = getWord(%arg, (1.0 + %i));
        }
        %i = (1.0 + %i);
    }
    log("communication", "debug", "value of URL is " @ %url);
    log("communication", "debug", "now URL is " @ %url);
    $VURLcmd = %url;
    (%count < %i);
    if (isObject(LoginGui)) {
    }
    if (($Token $= "")) {
    }
    if (isObject(WorldMap)) {
    }
    if (!(%this.loggedIn)) {
        LoginGui.setControlsActive(0);
        LoginProgressBarCtrls.setVisible(1);
        $Player::Name = trim($Player::Name);
        WorldMap;
        LoginUserNameField.setValue($Player::Name);
        if ($UserPref::Login::RememberMe) {
            $UserPref::Player::Name = $Player::Name;
            $UserPref::Player::Password = $Player::Password;
        }
        $UserPref::Player::Name = "";
        $UserPref::Player::Password = "";
        LoginGui.envManagerLogin();
    }
    if (!($VURLcmd $= "")) {
        vurlOperation($VURLcmd);
    }
};
