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
    if (!(initialized)) {
        initialized = %this @ 1 @ %this;
        currentCity = "" @ %this;
        if ((getMapType() $= "two_layer")) {
            %this.setUpCities();
            safeEnsureScriptObject("SimGroup", "WorldMapServerInfoGroup");
        }
    }
};
function WorldMap::initCityMaps(%this) {
    currentCity = "" @ %this;
    safeEnsureScriptObject("StringMap", "WorldMapCityInfoMap");
    fillCityInfoMap();
    safeEnsureScriptObject("StringMap", "WorldMapCityNamesMap");
    fillCityNamesMap();
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
    %info = %cityName.get();
    WorldMapCityInfoMap;
    if (!(isObject(%info))) {
        return 0;
    }
    if (%alt) {
    }
    if (%forTGF) {
    }
    %coords = Coords;
    %info;
    if (%alt) {
    }
    if (%forTGF) {
    }
    %bitmap = button;
    %info;
    %groupNum = -1;
    altButton;
    if (%alt) {
        %groupNum = $WorldMapCityButtonGroup;
        %info;
    }
    if (%forTGF) {
        %groupNum = $TGFCityButtonGroup;
        altCoords;
    }
    if (%alt) {
    }
    %buttonType = %forTGF ? "RadioButton" : "PushButton";
    %info;
    if (%alt) {
    }
    if (%forTGF) {
    }
    %command = "WorldMap.selectCity(" @ %cityName @ ");" @ "WorldMap.getCityButton(" @ %cityName @ ", true, false).performClick();";
    profile = GuiBitmapButtonCtrl @ new %objName() @ "GuiDefaultProfile";
    0;
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
    %button = ;
    %statusX = getWord(%coords, 0);
    %statusY = (20.0 - (getWord(%coords, 3) + getWord(%coords, 1)));
    profile = GuiMLTextCtrl @ new ""() @ "ETSLoginMLTextProfile";
    0;
    extent = getWord(%coords, 2) @ " " @ 14;
    position = %statusX @ " " @ %statusY;
    visible = 1;
    %statusLabel = ;
    statusLabel = %statusLabel @ %button;
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
    %cityInfo = %cityName.get();
    WorldMapCityInfoMap;
    if (!(isObject(venues))) {
        return 0;
    }
    %venueInfo = venues.get(%venueName);
    %cityInfo;
    if (!(isObject(%venueInfo))) {
        return 0;
    }
    %objName = %venueInfo @ spawnName;
    "geWorldMapVenueButton_" @ %cityName @ "_";
    if (isObject(%objName)) {
        return %objName.getId();
    }
    %cmd = "WorldMap.selectVenue(\"" @ %cityName @ "\", \"" @ %venueName @ "\", " @ %venueInfo @ spawnName @ ");";
    profile = GuiBitmapButtonCtrl @ new %objName() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = %venueInfo @ getWords(Coords, 0, 1);
    extent = %venueInfo @ getWords(Coords, 2, 3);
    minExtent = "1 1";
    visible = 1;
    command = %cmd;
    text = "";
    groupNum = -1;
    buttonType = "PushButton";
    bitmap = %venueInfo @ getPathOfButtonResource(button);
    drawText = 0;
    class = "WorldMapVenueButton";
    venueInfo = %venueInfo;
    return;
};
function WorldMap::getVenueLabel(%this, %cityName, %venueName) {
    %cityInfo = %cityName.get();
    WorldMapCityInfoMap;
    if (!(isObject(venues))) {
        return 0;
    }
    %venueInfo = venues.get(%venueName);
    %cityInfo;
    if (!(isObject(%venueInfo))) {
        return 0;
    }
    %objName = %cityName @ "_" @ %venueInfo @ spawnName @ "_label";
    if (isObject(%objName)) {
        return %objName.getId();
    }
    profile = GuiControl @ new %objName() @ "GuiModelessDialogProfile";
    0;
    horizSizing = "width";
    vertSizing = "height";
    position = "0 0";
    extent = "202 20";
    minExtent = "1 1";
    visible = 1;
    profile = GuiMLTextCtrl @ new ""() @ "ETSVenueNameMLTextProfile";
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
    profile = GuiMLTextCtrl @ new ""() @ "ETSVenueNameMLTextProfile";
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
    %ctrl = ;
    %button = %this.getVenueButton(%cityName, %venueName);
    %trgX = ((getWord(%ctrl.getExtent(), 0) * 0.5) - ((getWord(%button.getExtent(), 0) * 0.5) + getWord(%button.getPosition(), 0)));
    %trgY = (8.0 + (getWord(%ctrl.getExtent(), 1) - getWord(%button.getPosition(), 1)));
    %ctrl.setTrgPosition(%trgX, %trgY);
    return %ctrl;
};
function WorldMap::setUpCities(%this) {
    %size = size();
    WorldMapCityInfoMap;
    %i = 0;
    if ((%size < %i)) {
        %cityName = name;
        %i.getValue();
        %largeCityButton = %this.getCityButton(%cityName, 0, 0);
        WorldMapCityInfoMap;
        %cityPeepsCtrl = %this.getCityPeepsCtrl(%cityName);
        %largeCityButton.add();
        statusLabel.add();
        statusLabel.setVisible(0);
        if (isObject()) {
        }
        if ($AutoDownloadPackages) {
            %status = %cityName.getStatusForCity();
            packageDownload;
            if ((packageDownload SPC %status $= "done")) {
                %largeCityButton.setActive(1);
                %statusText = %this.getFullnessDesc(load, capacity);
                %largeCityButton;
                statusLabel.setText(%largeCityButton @ %largeCityButton @ %largeCityButton @ "<spush><font:Arial:12>" @ "Status: " @ %statusText @ "<spop>");
                %cityPeepsCtrl.setTextWithStyle("-" @ " " @ %statusText);
            }
        }
        %i = (1.0 + %i);
        %largeCityButton;
    }
    UpdateCityStatuses();
};
function WorldMap::selectCity(%this, %cityName) {
    if ((%this SPC currentCity $= %cityName)) {
        return;
    }
    %cityInfo = %cityName.get();
    WorldMapCityInfoMap;
    if (!(isObject(%cityInfo))) {
        warn(getScopeName() @ " " @ "- No info for city" @ " " @ %cityName @ " " @ getTrace());
        return;
    }
    currentCity = %cityName @ %this;
    %this.setView("single_city");
    background.setBitmap();
    background.setBitmap();
    background.setBitmap();
    clear();
    venues.forEach("addVenueButton");
    venueButtons = %cityInfo @ "" @ WorldMapCityBkgd;
    WorldMapCityBkgd;
    %count = getCount();
    WorldMapCityBkgd;
    %i = 0;
    %cityInfo;
    if ((%count < %i)) {
        venueButtons = venueButtons @ " " @ WorldMapCityBkgd @ %i.getObject() @ WorldMapCityBkgd;
        WorldMapCityBkgd;
        %i = (1.0 + %i);
        CityDownloadGui;
    }
    1.setButtonsEnabled();
    if (!(isObject())) {
        profile = CityMapLargeTitleText @ new GuiMLTextCtrl(CityMapLargeTitleText) @ "MapLargeLabelProfile";
        WorldMapCityBkgd;
        horizSizing = %cityInfo @ (%count < %i) @ "right";
        LoadingGui;
        vertSizing = WorldMapCityBkgd @ %cityInfo @ "bottom";
        position = "545 484";
        extent = "130 24";
        minExtent = "1 1";
        visible = 1;
        text = "";
        maxLength = 255;
    }
    add();
    if (roles::maskhaspermission($player::rolesMask, "dressingRoomSpawn")) {
        %this @ currentCity @ " " @ "DressingRoomSpawns>Go Backstage!</a>".setText();
    }
    "".setText();
    if (forTGF) {
        %cityName.Maps_changedCityFilter();
    }
    %this.getCityButton(%cityName, 1, 1).performClick();
    %this.getCityButton(%cityName, 1, 0).performClick();
};
function CityMapLargeTitleText::onURL(%this, %url) {
    if (!(getWord(%url, 0) $= "VENUESPAWN")) {
        return;
    }
    %city = getWord(%url, 1);
    %spawn = getWord(%url, 2);
    "Connecting...".setText();
    %city.selectVenue("", %spawn);
};
function WorldMap::selectVenue(%this, %cityName, %venueName, %spawnName) {
    %targetVurl = "vside:/location/" @ %cityName @ "/" @ %spawnName;
    if ($StandAlone) {
        commandToServer('TeleportToVURL', %targetVurl);
        closeFully();
        return geTGF;
    }
    if (isVisible()) {
        %idx = GetSelected();
        WorldMapServerPopup;
        if ((0.0 < %idx)) {
        }
        if ((getCount() >= %idx)) {
            error("WorldMap::selectVenue(): invalid server selected");
        }
        %serverInfo = %idx.getObject();
        WorldMapServerInfoGroup;
        if (!(WorldMapServerInfoGroup SPC %cityName $= "")) {
            %cityName[$UserPref::WorldMap::ServerChoice @ %cityName] = WorldMapServerPopup @ %serverInfo @ serverName;
        }
        %targetVurl = %serverInfo @ serverName;
        %targetVurl @ "?server0=";
    }
    vurlOperation(%targetVurl);
};
function WorldMap::open(%this) {
    %this.openTGF(0);
};
function WorldMap::openTGF(%this, %forTGF) {
    %this.Initialize();
    %this.updateLocation();
    %this.setBitmap("platform/client/ui/worldmapBackground");
    if ((WorldMapCityInfoMap > size())) {
    }
    if (!(loggedIn)) {
        %this.setView("multi_city");
    }
    if (!(loggedIn)) {
        %this.setView("single_city");
    }
    if (!(%this SPC $gContiguousSpaceName $= "")) {
        %this.getCityButton($gContiguousSpaceName, 1, 0).performClick();
    }
    %this.setVisible(1);
    if (!(%forTGF)) {
        DestroyMessageBoxes();
        %this.setContent();
        pushScreenSize(960, 544, 0, 1, 0);
    }
    forTGF = Canvas @ %forTGF @ %this;
    %this;
    1.setButtonsEnabled();
    %this.refresh();
};
function WorldMap::setLoggedIn(%this, %flag) {
    loggedIn = %flag @ %this;
};
function WorldMap::setNotConnectedToServer(%this) {
    disconnectedCleanup("");
    $ServerName = "";
    server = 0 @ %this;
    $gContiguousSpaceName = "";
    %this.setLoggedIn(0);
};
function WorldMap::close(%this) {
    %this.setVisible(0);
    if (loggedIn) {
        setContent();
    }
    popScreenSize();
};
function WorldMap::setView(%this, %view) {
    if ((%view $= "multi_city")) {
        1.setVisible();
        1.setVisible();
        0.setVisible();
        0.setVisible();
        0.setVisible();
        0.setVisible();
        0.setVisible();
        1.setVisible();
        $ETS::devMode.setVisible();
        currentCity = DevModMapCtrls @ "" @ %this;
        geTGF_map_header;
        if (forTGF) {
            "".Maps_changedCityFilter();
        }
    }
    if ((geTGF_tabs SPC %view $= "single_city")) {
        0.setVisible();
        0.setVisible();
        1.setVisible();
        if (!(forTGF)) {
            1.setVisible();
        }
        1.setVisible();
        1.setVisible();
        1.setVisible();
        0.setVisible();
        %this.fillServerList();
        0.setVisible();
    }
};
function WorldMap::fillDevModServerList(%this) {
    %names = "";
    %n = (WorldMapServers - getCount());
    1.0;
    if ((0.0 >= %n)) {
        %server = %n.getObject();
        WorldMapServers;
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
    %list.setText();
};
function WorldMap::fillServerList(%this) {
    clear();
    deleteMembers();
    %count = getCount();
    WorldMapServers;
    %i = 0;
    WorldMapServerInfoGroup;
    if ((%count < %i)) {
        %serverProps = %i.getObject();
        WorldMapServers;
        if (!(WorldMapServerPopup SPC %serverProps.get("mappable") $= 0)) {
            if ((%this $= currentCity)) {
            }
            if ((%serverProps.get("city") SPC %serverProps.get("city") $= "")) {
                %name = %serverProps.get("name");
                %load = %serverProps.get("load");
                %capacity = %serverProps.get("capacity");
                server = SimObject @ new ""() @ %serverProps;
                0;
                serverName = WorldMapServerInfoGroup @ %name;
                .add();
                WorldMapServerPopup @ %name @ " -- " @ %this.getFullnessDesc(%load, %capacity).add();
            }
        }
        %i = (1.0 + %i);
    }
    0.SetSelected();
    %serverChoice = $UserPref::WorldMap::ServerChoice;
    WorldMapServerPopup;
    if (!((%count < %i) SPC %serverChoice $= "")) {
        %count = getCount();
        WorldMapServerInfoGroup;
        %i = 0;
        if ((%count < %i)) {
            if ((%i.getObject() SPC serverName $= %serverChoice)) {
                %i.SetSelected();
            }
            %i = (1.0 + %i);
            WorldMapServerPopup;
        }
    }
    if ((WorldMapServerInfoGroup > getCount())) {
        1.setVisible();
        1.setVisible();
        0.setVisible();
    }
    0.setVisible();
    0.setVisible();
    1.setVisible();
};
function WorldMap::onCanvasResize(%this) {
    %this.update();
};
function WorldMap::doServerJoin(%this, %targetVurl) {
    echo("in doServerJoin: " @ %targetVurl);
    purgeResources();
    fmodShutdown();
    fmodInitialize();
    if (isObject(server)) {
        echo(%this @ "in doServerJoin " @ %this);
        %conn = new GameConnection(ServerConnection);
        %conn.setCommonPreconnectClientSettings(%targetVurl);
        $GameConnection = %conn;
        $ServerName = server.get("name");
        %this;
        %address = server.get("address");
        %this;
        %port = server.get("port");
        %this;
        if (!(%port $= "")) {
            %address = %address @ ":" @ %port;
        }
        $lastJoinedServer = %address;
        $lastVURL = %targetVurl;
        %conn.connect(%address);
        if (isObject()) {
            deleteMembers();
        }
        %analytic = getAnalytic();
        geMapHud2DTheOrthoMap;
        %analytic.trackPageView(%this @ server.get("city"));
        %this.selectCity(server.get("city"));
        $SpawnTargetSavedVURL = "";
        %this;
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
    if (isObject(server)) {
        %a2 = server.get("address");
        %this;
        %p2 = server.get("port");
        %this;
        echo(%this @ "current server:" @ " " @ %a2 @ ":" @ %p2);
        if ((%a1 $= %a2)) {
        }
        if ((%p1 $= %p2)) {
            if (!(%isATransition)) {
                closeFully();
            }
            commandToServer('TeleportToVURL', %targetVurl);
            return geTGF;
        }
    }
    server = %server @ %this;
    if ($gWorldMapJoiningServer) {
        warn("WorldMap::join(): multiple clicks on servers in the world map");
        return;
    }
    $gWorldMapJoiningServer = 1;
    %i = 0;
    if ((numServers < %i)) {
        buttons.setActive(0);
        %i = (1.0 + %i);
        %this @ %i @ %this;
    }
    waitForDisconnect = (numServers < %i) @ 0 @ ServerConnection;
    %this;
    if (isObject()) {
    }
    if ((-(1.0) != GameConnection::getServerConnection())) {
        echo("disconnecting...");
        waitForDisconnect = ServerConnection @ 1 @ ServerConnection;
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
    forceRightMouseUp();
    %buildingName.open();
};
function clientCmdOpenBuildingDirectoryFromCustomSpaceCancel() {
};
function showTransitionMessage(%description, %counter) {
    extent = PlayGui @ extent @ TransitionMessage;
    if ((0.0 <= %counter)) {
        text = "" @ TransitionMessage;
    }
    text = "Wait here for a ride to" @ " " @ %description @ " " @ " in " @ " " @ %counter @ "..\n" @ TransitionMessage;
    1.setVisible();
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
    $CountdownTimer = schedule(1000, 0, %counter, %description, %vurl);
    TransitionCountdown;
};
function TransitionCountdown(%counter, %description, %vurl) {
    %counter = (1.0 - %counter);
    showTransitionMessage(%description, %counter);
    if ((-(1.0) == %counter)) {
        0.setVisible();
        if (!(TransitionMessage SPC %vurl $= "")) {
            if (isVisible()) {
                closeFully();
            }
            if (isVisible()) {
                0.close();
            }
            vurlOperation(%vurl);
        }
        MessageBoxOK("Transition Error", "Couldn't figure out where to take you!\nPlease use the Map to go where you would like to go.", "");
        "Map".openToTabName();
        return geTGF;
    }
    $CountdownTimer = schedule(1000, 0, %counter, %description, %vurl);
    TransitionCountdown;
};
function clientCmdTransitionCancel() {
    echo("Server calling transition cancel");
    TransitionCancel(0);
};
function TransitionCancel(%retry) {
    cancel($CountdownTimer);
    0.setVisible();
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
    if (%cityNameLongOrShort.hasKey()) {
        %cityNameLong = %cityNameLongOrShort.get();
        gCityNamesShortToLongMap;
    }
    %cityNameLong = %cityNameLongOrShort;
    gCityNamesShortToLongMap;
    %strLen = strlen(%cityNameLong);
    %mostSpaceSvr = 0;
    %mostSpaceAmt = 0;
    %n = (WorldMapServers - getCount());
    1.0;
    if ((0.0 >= %n)) {
        %serverObj = %n.getObject();
        WorldMapServers;
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
    $gTransitionScreenshotSchedule = schedule(10, 0, %frames, %ServerName, %spawnTargetVURL);
    doTransitionAfterFrames;
};
function doTransition(%server, %spawnTargetVURL) {
    cursorOff();
    $TransitionScreenshot.shootMemory("GRAYSCALE");
    cursorOn();
    %server.join(1, %spawnTargetVURL);
};
function WorldMap::cleanUpServers(%this) {
    %curServerObjId = 0;
    %savedServer = "";
    if (isObject(server)) {
        %curServerObjId = server.getId();
        %this;
    }
    %i = 0;
    %this;
    if ((getCount() < %i)) {
        %server = %i.getObject();
        WorldMapServers;
        if ((%curServerObjId == %server.getId())) {
            %savedServer = %server;
            WorldMapServers;
            %server.remove();
        }
        if (isObject(buddies)) {
            buddies.delete();
        }
        %i = (1.0 + %i);
        %server;
    }
    deleteMembers();
    if (isObject(%savedServer)) {
        %savedServer.add();
    }
};
function WorldMap::refresh(%this) {
    if (!(haveValidManagerHost())) {
        return;
    }
    %this.requestMapData();
};
function WorldMap::exit(%this) {
    if (isObject()) {
        delete();
    }
    if (isFunction("Using_DF")) {
    }
    if (Using_DF()) {
        endDFZone();
    }
    purgeResources();
    fmodShutdown();
    setContent();
};
function WorldMap::update(%this) {
    %this.clearCities();
    0.setVisible();
    if (!(isObject())) {
        return WorldMapServers;
    }
    %sc = getCount();
    WorldMapServers;
    %i = 0;
    if ((%sc < %i)) {
        %server = %i.getObject();
        WorldMapServers;
        if (isObject(%server)) {
            %ismappable = %server.get("mappable");
            if (!(%ismappable $= 0)) {
                %server.addCity();
            }
        }
        %i = (1.0 + %i);
        WorldMap;
    }
    %this.adjustButtons();
    0.setVisible();
    if (isVisible()) {
        %this.fillServerList();
    }
    if (isObject()) {
        %this.fillDevModServerList();
    }
    updateUserListUnknownServerName();
};
function WorldMap::adjustButtons(%this) {
    %winWidth = getWord($UserPref::Video::Resolution, 0);
    %size = buttonSize;
    WorldMap;
    %i = 0;
    if ((numServers < %i)) {
        %button = buttons;
        %this @ %i @ %this;
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
            if (!(%city SPC server $= "")) {
                %this.remove(%city);
                %city.delete();
            }
        }
        %i = (1.0 - %i);
    }
    numServers = (0.0 >= %i) @ 0 @ %this;
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
        if ((numServers < %n)) {
            if ((buttons < VectorDist(%loc, position))) {
                %valid = 0;
                8.0 @ %n @ %this;
            }
            %n = (1.0 + %n);
            %this;
        }
        if (!(%valid)) {
            %locX = (getRandom(-(15.0), 15) + getWord(%locOrig, 0));
            (numServers < %n);
            %locY = (getRandom(-(15.0), 15) + getWord(%locOrig, 1));
            %this;
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
    %buttonLoc = WorldMap @ (halfButtonSize - getWord(%centerLoc, 1));
    (halfButtonSize - getWord(%centerLoc, 0)) @ " ";
    %buttonLoc = %this.validateSpot(%buttonLoc);
    WorldMap;
    %load = %server.get("load");
    %capacity = %server.get("capacity");
    %fullness = mClamp(mFloor((%capacity / (%load * 8.0))), 0, 8);
    %size = buttonSize;
    WorldMap;
    profile = new GuiBitmapButtonCtrl(WorldMapServerButton) @ "GuiDefaultProfile";
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
    %button = ;
    buttons = %button @ %this @ numServers @ %this;
    numServers = (%this + numServers);
    1.0;
    %this.add(%button);
    if ((%server.get("name") $= $ServerName)) {
        %urHereLoc = WorldMap @ (urHereHalfButtonSize - getWord(%centerLoc, 1));
        (urHereHalfButtonSize - getWord(%centerLoc, 0)) @ " ";
        getWord(%urHereLoc, 0).reposition(getWord(%urHereLoc, 1));
        1.setVisible();
    }
};
function WorldMap::showPopup(%this, %city) {
    %server = server;
    %city;
    if (!(isObject(%server))) {
        return;
    }
    %winWidth = getWord($UserPref::Video::Resolution, 0);
    %scaleFactor = (960.0 / %winWidth);
    %destName = destName;
    %city;
    %address = %server.get("address");
    %capacity = %server.get("capacity");
    %load = %server.get("load");
    %port = %server.get("port");
    %destName.setText();
    if (isObject()) {
    }
    if ($AutoDownloadPackages) {
        %status = %city.getStatusForCity();
        packageDownload;
        if ((packageDownload SPC %status $= "done")) {
            %statusText = %this.getFullnessDesc(%load, %capacity);
            MapHudCityText;
        }
        %statusText = "Downloading!";
    }
    %statusText = %this.getFullnessDesc(%load, %capacity);
    MapHudMetaText @ "Status: " @ %statusText.setText();
    %top = getWord(%city.getPosition(), 1);
    %left = getWord(%city.getPosition(), 0);
    %bottom = (getWord(%city.getExtent(), 1) + %top);
    %right = (getWord(%city.getExtent(), 0) + %left);
    %hudwidth = getWord(getExtent(), 0);
    MapCityHud;
    %hudheight = getWord(getExtent(), 1);
    MapCityHud;
    %pos = (36.0 + %left) @ " " @ (5.0 + %top);
    %xPos = getWord(%pos, 0);
    %ypos = getWord(%pos, 1);
    %xPos.reposition(%ypos);
    1.setVisible();
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
    1.setDepressed();
};
function WorldMapServerButton::onMouseUp(%this) {
    0.setDepressed();
};
function WorldMapServerButton::onMouseEnter(%this, %unused, %unused, %unused) {
    %this.showPopup();
};
function WorldMapServerButton::onMouseLeave(%this) {
    0.setVisible();
};
function WorldMap::setCitiesActive(%this, %flag) {
    %count = getCount();
    WorldMapMultiCityLarge;
    %i = 0;
    if ((%count < %i)) {
        %i.getObject().setActive(%flag);
        %i = (1.0 + %i);
        WorldMapMultiCityLarge;
    }
};
function WorldMap::UpdateCityStatuses(%this) {
    if (!(isVisible())) {
        return WorldMap;
    }
    %csn = "gw";
    %hasAccess = 0;
    %hasAccess = ((10.0 < $Player::Name.getProperty(gUserPropMgrClient @ "level started count " @ %csn, 0)) | %hasAccess);
    %hasAccess = (roles::maskhaspermission($player::rolesMask, "gatewaySpawn") | %hasAccess);
    %hasAccess = ($ETS::devMode | %hasAccess);
    if (!(isObject())) {
        new SimGroup(WorldMapServers);
        add();
    }
    %n = (WorldMapServers - getCount());
    1.0;
    if ((0.0 >= %n)) {
        %serverProps = %n.getObject();
        WorldMapServers;
        if (!(%hasAccess)) {
        }
        if ((WorldMapServers SPC %serverProps.get("city") $= %csn)) {
            %serverProps.delete();
        }
        %n = (1.0 - %n);
        RootGroup;
    }
    %count = getCount();
    WorldMapMultiCityLarge;
    %i = 0;
    (0.0 >= %n);
    if ((%count < %i)) {
        %buttonBig = %i.getObject();
        WorldMapMultiCityLarge;
        %buttonSml = citybutton;
        %buttonBig @ cityName @ TGFWorldMapMultiCitySmall;
        %statusTxtCtrl = %buttonBig @ cityName;
        WorldMapServers @ "geTGF_map_peeps_";
        load = 0 @ %buttonBig;
        capacity = 0 @ %buttonBig;
        numServers = 0 @ %buttonBig;
        %buttonBig.setActive(0);
        style = "tgfMapCityPeepsInactive" @ %statusTxtCtrl;
        if (isObject(%buttonSml)) {
            %buttonSml.setActive(0);
        }
        if (!(isObject())) {
            new SimGroup(WorldMapServers);
            add();
        }
        %n = (WorldMapServers - getCount());
        1.0;
        if ((0.0 >= %n)) {
            %serverProps = %n.getObject();
            WorldMapServers;
            if ((%buttonBig SPC cityName $= %serverProps.get("city"))) {
                %buttonBig.setActive(1);
                style = WorldMapServers @ "tgfMapCityPeeps" @ %statusTxtCtrl;
                RootGroup;
                if (isObject(%buttonSml)) {
                    %buttonSml.setActive(1);
                }
                load = (%buttonBig + load);
                %serverProps.get("load");
                capacity = (%buttonBig + capacity);
                %serverProps.get("capacity");
                servers = WorldMapServers @ %serverProps @ %buttonBig @ numServers @ %buttonBig;
                numServers = (%buttonBig + numServers);
                1.0;
            }
            %n = (1.0 - %n);
        }
        if (isObject()) {
        }
        if ($AutoDownloadPackages) {
            %dlStatus = cityName.getStatusForCity();
            %buttonBig;
            if ((packageDownload SPC %dlStatus $= "done")) {
                %statusText = %this.getFullnessDesc(load, capacity);
                %buttonBig;
            }
            %statusText = "Downloading!";
            %buttonBig;
            %buttonBig.setActive(0);
            style = packageDownload @ "tgfMapCityPeepsInactive" @ %statusTxtCtrlCtrl;
            (0.0 >= %n);
            if (isObject(%buttonSml)) {
                %buttonSml.setActive(0);
            }
        }
        %statusText = %this.getFullnessDesc(load, capacity);
        %buttonBig;
        statusLabel.setText(%buttonBig @ %buttonBig @ "<spush><font:Arial:12>" @ "Status: " @ %statusText @ "<spop>");
        style = "tgfMapCityPeeps" @ %statusTxtCtrl;
        %statusTxtCtrl.setTextWithStyle("-" @ " " @ %statusText);
        %i = (1.0 + %i);
    }
};
function WorldMap::cityNameForServerName(%this, %name) {
    if (!(isObject())) {
        return "";
    }
    %count = getCount();
    WorldMapServers;
    %i = 0;
    if ((%count < %i)) {
        %server = %i.getObject();
        WorldMapServers;
        if ((%server.get("name") $= %name)) {
            return %server.get("city");
        }
        %i = (1.0 + %i);
    }
    return "";
};
function WorldMap::IsApartmentServerForServerName(%this, %name) {
    if (!(isObject())) {
        return 0;
    }
    %count = getCount();
    WorldMapServers;
    %i = 0;
    if ((%count < %i)) {
        %server = %i.getObject();
        WorldMapServers;
        if ((%server.get("name") $= %name)) {
            return !(%server.get("mappable"));
        }
        %i = (1.0 + %i);
    }
    return 0;
};
function WorldMap::isServerForCity(%this, %name) {
    if (!(isObject())) {
        return 0;
    }
    %count = getCount();
    WorldMapServers;
    %i = 0;
    if ((%count < %i)) {
        %server = %i.getObject();
        WorldMapServers;
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
    if (!(isObject())) {
    }
    if (!(isAwake())) {
        return WorldMap;
    }
    setUpCities();
    $gRefreshWorldMapTimer = schedule(2000, 0, "refreshWorldMap");
    WorldMap;
};
function WorldMap::requestMapData(%this) {
    if (!(isObject())) {
        new SimGroup(WorldMapServers);
        add();
    }
    %mapRequest = safeEnsureScriptObject("URLPostObject", "MapRequest");
    WorldMapServers;
    if (!(%mapRequest SPC isActive $= "")) {
    }
    if ((%mapRequest == isActive)) {
        return 1.0;
    }
    %mapRequest.setURL($Net::ClientServiceURL @ "/WorldMapRefresh");
    %mapRequest.setURLParam("user", $Player::Name);
    %mapRequest.setURLParam("token", $Token);
    %mapRequest.setURLParam("version", getProtocolVersion());
    %mapRequest.setCompletedCallback("MapRequestOnCompleted");
    log("communication", "debug", "sending request for map data.");
    isActive = 1 @ %mapRequest;
    %mapRequest.start();
};
function MapRequestOnCompleted(%request, %result) {
    isActive = 0 @ %request;
    if ((0.0 == %result)) {
        %request.parseResult();
        update();
    }
    if (($CURL::CouldNotConnect == %result)) {
        MessageBoxOK("Connection Error", WorldMap, "");
    }
    if (($CURL::CouldNotResolveHost == %result)) {
        MessageBoxOK("Could Not Find Server", WorldMap, "");
    }
    MessageBoxOK("Server Unavailable", , "");
    %request.schedule(0, "delete");
    %vurl = getSkipMapVurl(1);
    if (!(%vurl $= "")) {
        vurlOperation(%vurl);
    }
};
function WorldMap::parseResult(%this, %request) {
    cleanUpServers();
    if ((WorldMapServers == getCount())) {
        %savedServer = "";
        0.0;
        %savedName = "";
        WorldMap;
    }
    %savedServer = 0.getObject();
    WorldMapServers;
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
        %serverProps = new ""();
        StringMap;
        if (isObject()) {
            %serverProps.add();
        }
        %j = 0;
        MissionCleanup;
        if ((getWordCount(%fields) < %j)) {
            %field = getWord(%fields, %j);
            MissionCleanup;
            %name = 0 @ "server" @ %i @ "." @ %field;
            %value = %request.getResult(%name);
            log("communication", "debug", "adding server prop: " @ %field @ " = " @ %value);
            %serverProps.put(%field, %value);
            %j = (1.0 + %j);
        }
        if (((getWordCount(%fields) < %j) SPC %serverProps.get("city") $= "")) {
            %name = %serverProps.get("name");
            %count = size();
            WorldMapCityNamesMap;
            %j = 0;
            if ((%count < %j)) {
                %key = %j.getKey();
                WorldMapCityNamesMap;
                if ((0.0 == stricmp(%key, %name))) {
                    %value = %j.getValue();
                    WorldMapCityNamesMap;
                    %serverProps.put("city", %value);
                }
                %j = (1.0 + %j);
            }
        }
        %serverProps.add();
        %i = (1.0 + %i);
        WorldMapServers;
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
        if ((gUserPropMgrClient == $Player::Name.getProperty("level started count gw", 0))) {
        }
        if (!(roles::maskhaspermission($player::rolesMask, "gatewaySpawn"))) {
            if ($ETS::devMode) {
                if (%bChangeUI) {
                    MessageBoxOK("Not going to gateway..", "Ordinarily, you would have been\nautomatically take to gateway here,\nbut since you're devmode, you're not.", "");
                }
            }
            %ret = "vside:/location/gw/mapSpawns_entry";
            0.0;
            if (%bChangeUI) {
                "gw".selectCity();
                "Maps".selectTab();
            }
        }
    }
    log("communication", "debug", WorldMap @ geTGF @ getScopeName() @ " " @ "- \"" @ %ret @ "\".");
    return %ret;
};
function VenuesMap::addVenueButton(%this, %key, %value) {
    currentCity.getVenueButton(%key).add();
    currentCity.getVenueLabel(%key).add();
};
function WorldMapVenueButton::onMouseEnter(%this) {
    %bitmapName = strreplace(name, "'", "");
    venueInfo;
    %bitmapName = %this @ WorldMap @ currentCity @ "_" @ strreplace(%bitmapName, " ", "_");
    %bitmap = getPathOfButtonResource("platform/client/ui/spawn_info/" @ %bitmapName);
    if (!(%bitmap $= "")) {
        %bitmap.setBitmap();
        fitSize();
        (getWord(%this.getExtent(), 0) + getWord(%this.getPosition(), 0)).reposition((6.0 + getWord(%this.getPosition(), 1)));
        1.setVisible();
    }
};
function WorldMapVenueButton::onMouseLeave(%this) {
    0.setVisible();
};
function WorldMapCityBkgd::setButtonsEnabled(%this, %flag) {
    %count = getWordCount(venueButtons);
    %this;
    %i = 0;
    if ((%count < %i)) {
        %button = getWord(venueButtons, %i);
        %this;
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
    closeFully();
    %serverObj.join(0, "");
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
    log("communication", "debug", (%count < %i) @ "value of URL is " @ %url);
    log("communication", "debug", "now URL is " @ %url);
    $VURLcmd = %url;
    if (isObject()) {
    }
    if ((LoginGui SPC $Token $= "")) {
    }
    if (isObject()) {
    }
    if (!(loggedIn)) {
        0.setControlsActive();
        1.setVisible();
        $Player::Name = trim($Player::Name);
        LoginProgressBarCtrls;
        $Player::Name.setValue();
        if ($UserPref::Login::RememberMe) {
            $UserPref::Player::Name = $Player::Name;
            LoginUserNameField;
            $UserPref::Player::Password = $Player::Password;
            LoginGui;
        }
        $UserPref::Player::Name = "";
        WorldMap;
        $UserPref::Player::Password = "";
        WorldMap;
        envManagerLogin();
    }
    if (!(LoginGui SPC $VURLcmd $= "")) {
        vurlOperation($VURLcmd);
    }
};
