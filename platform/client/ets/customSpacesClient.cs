$CSCurrentSpaceName = "";
$CSSpaceName = "";
$CSBuildingName = 0;
$CSSpaceInfo = 0;
$CSBuildingInfo = 0;
function CustomSpaceClient::SetCurrentSpaceName(%spaceName) {
    $CSCurrentSpaceName = %spaceName;
};
function CustomSpaceClient::GetCurrentSpaceName() {
    return $CSCurrentSpaceName;
};
function CustomSpaceClient::GetSpaceImIn() {
    return $CSSpaceName;
};
function CustomSpaceClient::SetSpaceImIn(%buildingName, %spaceName) {
    if (($CSSpaceName $= %spaceName)) {
        if (!($CSSpaceName $= "")) {
            error(getScopeName() @ " " @ "you are already in this space:" @ " " @ %spaceName);
        }
        return;
    }
    $CSBuildingName = %buildingName;
    $CSSpaceName = %spaceName;
    $CSBlockedList = "";
    if ((%buildingName $= "")) {
    }
    if ((%spaceName $= "")) {
        return;
    }
    if ($StandAlone) {
        CustomSpaceClient::SetUpOwnership(1);
        return;
    }
    getBuildingSpaceInfo($CSBuildingName, $CSSpaceName, "GotCustomSpaceInfo", "");
};
function CustomSpaceClient::OnClientDisconnect() {
    CustomSpaceClient::SetupClientAsNotInSpace();
};
function CustomSpaceClient::SetupClientAsNotInSpace() {
    if (isObject(CSFurnitureMover)) {
        CSFurnitureMover.close();
    }
    if (isObject(CSInventoryBrowserWindow)) {
        CSInventoryBrowserWindow.close();
    }
    if (isObject(CSShoppingBrowserWindow)) {
        CSShoppingBrowserWindow.close();
    }
    if (isObject(CSMediaDisplay)) {
        CSMediaDisplay.close();
    }
    if (isObject(CSRulesAndDescWindow)) {
        CSRulesAndDescWindow.close();
    }
    if (isObject(CSLayoutSelector)) {
        CSLayoutSelector.close();
    }
    if (isObject(CSPaintingWindow)) {
        CSPaintingWindow.close();
    }
    if (isObject(CSControlPanel)) {
        CSControlPanel.close();
    }
    if ((HudTabs.getCurrentTab().name $= "private space")) {
        HudTabs.close();
    }
    "private space".hideTabWithName(HudTabs);
    if (($CSSpaceInfo != 0.0)) {
        destroySpaceInfo($CSSpaceInfo);
    }
    $CSSpaceInfo = 0;
    $CSBuildingInfo = 0;
    CustomSpaceClient::SetSpaceImIn("", "");
    CustomSpaceClient::SetUpOwnership(0);
};
$CS_EditingCustomSpace = 0;
function CustomSpaceClient::startEditingSpace() {
    if ($CS_EditingCustomSpace) {
        return;
    }
    csRequestToEditSpace();
    $CS_EditingCustomSpace = 1;
    onCustomSpaceEditorEnable();
};
function CustomSpaceClient::stopEditingSpace() {
    if (!($CS_EditingCustomSpace)) {
        return;
    }
    csDoneEditingSpace();
    $CS_EditingCustomSpace = 0;
    onCustomSpaceEditorDisable();
};
function CustomSpaceClient::checkEditingSpace() {
    if ((numCSPanelsOpen() > 0.0)) {
        CustomSpaceClient::startEditingSpace();
    }
    CustomSpaceClient::stopEditingSpace();
};
function GotCustomSpaceInfo(%buildingInfo, %spaceGroup) {
    echo("received custom space info");
    if ((%spaceGroup.getCount() != 1.0)) {
        log("network", "error", "GotCustomSpaceInfo returned a strange number of spaces (" @ %spaceGroup.getCount() @ ")");
    }
    if (($CSSpaceInfo != 0.0)) {
    }
    if (isObject($CSSpaceInfo)) {
        destroySpaceInfo($CSSpaceInfo);
    }
    $CSBuildingInfo = %buildingInfo;
    $CSSpaceInfo = 0.getObject(%spaceGroup);
    $CSSpaceInfo.remove(%spaceGroup);
    %spaceGroup.delete();
    %isOwner = 0;
    if (isObject($player)) {
        %isOwner = (stricmp($CSSpaceInfo.owner, $player.getShapeName()) == 0.0);
    }
    error("$Player is not an object. Setting space I'm in before I'm there...");
    CustomSpaceClient::SetUpOwnership(%isOwner);
    $CSSpaceInfo.description.updateSettings(CSRulesAndDescWindow, $CSSpaceInfo.access, $CSSpaceInfo.password);
    if ((stricmp($CSSpaceInfo.type, "model") == 0.0)) {
        CSControlPanel.open();
        CSSpaceModelAptText.update();
        "MODEL_APT".selectTabWithName(CSControlPanelTabs);
    }
    CustomSpacesClient::setMap2DText();
    CustomSpacesClient::InitializeVideoRequest();
};
function CustomSpacesClient::setMap2DText() {
    if (("<color:ffffff><tab:15,60>\tOwner:\t" @ " " @ $CSSpaceInfo.type $= "CELEBSPACE")) {
    }
    if (($CSSpaceInfo.name @ $CSSpaceInfo.owner @ "\n\tBuilding:\t" @ " " @ Buildings::GetDescription($CSBuildingName) $= "")) {
    }
    $CSBuildingName @ Buildings::GetDescription($CSBuildingName) @ "\n\tCity:\t" @ getContiguousSpaceFullName(Buildings::GetContiguousSpace($CSBuildingName)).setMap2DForCustomSpacesMode(geLocalMapContainer, "<color:ffffff><just:center><b>" @ TryFixBadWords($CSSpaceInfo.description));
};
$CSSpaceOwner = 0;
function CustomSpaceClient::SetUpOwnership(%isOwner) {
    %wasOwner = $CSSpaceOwner;
    log("network", "debug", "setting space ownership to " @ %isOwner);
    if ((%isOwner == 0.0)) {
        if (isObject(PrivSpaceHud)) {
            PrivSpaceHud.hideOP();
            PrivSpaceHud.disableOPlink();
        }
        if (isObject(CSControlPanel)) {
            if (($CSSpaceInfo != 0.0)) {
            }
            if ((stricmp($CSSpaceInfo.type, "model") == 0.0)) {
                CSControlPanel.open();
                "MODEL_APT".selectTabWithName(CSControlPanelTabs);
            }
            CSControlPanel.close();
        }
        if (isObject(MusicHud)) {
            0.setChangeStationAllowed(MusicHud);
        }
        PrivateSpacePopupMenuButton.hideButton(ButtonBar);
        $CSSpaceOwner = 0;
    }
    PrivateSpacePopupMenuButton.showButton(ButtonBar);
    %showSpaceOwnerTip = 1.getProperty(gUserPropMgrClient, $Player::Name, "ShowOwnerTip");
    if (%showSpaceOwnerTip) {
        0.setProperty(gUserPropMgrClient, $Player::Name, "ShowOwnerTip");
        userTips::showNow("SpaceOwner");
    }
    $CSSpaceOwner = 1;
    $CSBlockedList = $CSSpaceInfo.blockedList;
    Music::createGetMusicStreamsRequest();
    1.setChangeStationAllowed(MusicHud);
    csLoadMediaFavorites();
    csRequestHotMedia();
    getOwnedFurniture();
};
function CustomSpaceClient::isOwner() {
    return $CSSpaceOwner;
};
function CustomSpaceClient::placeSkuInWorld(%sku, %position, %orientation) {
    if ((%sku <= 0.0)) {
        error(getScopeName() @ " " @ "No sku selected");
        return;
    }
    if (!(isDefined("%position"))) {
        %position = "";
    }
    if (!(isDefined("%orientation"))) {
        %orientation = "";
    }
    if ((numUsingFurnitureSku(%sku) >= numOwnedFurnitureSku(%sku))) {
        return;
    }
    if ((numUsingFurnitureAll() >= $CSMaximumSlots)) {
        %title = "Sorry, Can't Do That";
        %body = "This space can only have" @ " " @ $CSMaximumSlots @ " " @ "items in it at a time.  Put something away to make more room.";
        MessageBoxOK(%title, %body, "");
        return;
    }
    if (!(useAnotherFurnitureSku(%sku))) {
        return;
    }
    $CSSelectedSku = %sku;
    if (!(%position $= "")) {
    }
    if (!(%orientation $= "")) {
        commandToServer('CreateInventoryBySkuAt', CustomSpaceClient::GetSpaceImIn(), %sku, %position, %orientation);
    }
    commandToServer('CreateInventoryBySku', CustomSpaceClient::GetSpaceImIn(), %sku);
    CSInventoryBrowser.update();
    CSShoppingBrowser.update();
    setIdle(0);
};
function CustomSpacesClient::InitializeVideoRequest() {
    echo("CSClientRequestVideoInit");
    commandToServer('CSClientRequestVideoInit', CustomSpaceClient::GetSpaceImIn());
};
function csRequestToEditSpace() {
    commandToServer('CSRequestToEdit', CustomSpaceClient::GetSpaceImIn());
};
function csDoneEditingSpace() {
    if (!(CustomSpaceClient::GetSpaceImIn() $= "")) {
        commandToServer('CSDoneEditing', CustomSpaceClient::GetSpaceImIn());
    }
};
function clientCmdCSRequestToEditAccepted(%spaceName, %numberOfSlots) {
    if (!(CustomSpaceClient::GetSpaceImIn() $= %spaceName)) {
        error(getScopeName() @ " " @ "how can you accept to edit a space that you are not in?");
        return;
    }
    %numberOfSlots.InitForSpace(CSFurnitureMover, %spaceName);
};
function clientCmdCSOnUnownedInventoryTimeOut(%referenceID) {
    if ((%referenceID == $CSSelectedID)) {
        -(1.0).SelectNuggetID(CSFurnitureMover);
        $CSInstaTestDrive = 0;
    }
    getNuggetGhostList("CSFurnitureMover::refreshGhostList");
};
function clientCmdCSRequestToEditDenied(%spaceName, %msg) {
    MessageBoxOK("Sorry", "your request to edit this space was denied" @ "\n" @ %msg, "");
};
function customSpace::SetMusicStreamID(%streamID, %displayId) {
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
        return;
    }
    if (!(isDefined("%displayID"))) {
        %displayId = "";
    }
    commandToServer('CSSetMusicStream', CustomSpaceClient::GetSpaceImIn(), %streamID, %displayId);
};
function customSpace::SetVideoURL(%videoURL) {
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
        return;
    }
    commandToServer('CSSetVideoURL', CustomSpaceClient::GetSpaceImIn(), %videoURL);
};
function clientCmdCS_OnEnterSpace(%spaceName, %musicStreamId, %videoURL, %building, %CurrentLayout) {
    if (!(CustomSpaceClient::GetCurrentSpaceName() $= "")) {
        clientCmdCS_OnLeaveSpace(CustomSpaceClient::GetCurrentSpaceName());
    }
    CSControlPanel.close();
    CustomSpaceClient::SetCurrentSpaceName(%spaceName);
    CSMediaDisplay.Initialize();
    %analyticSpace = strreplace(%spaceName, ".", " ");
    %analytic = getAnalytic();
    "/client/apartment/" @ getWord(%analyticSpace, 0) @ "/enter".trackPageView(%analytic);
    late_OnEnterSpace(%spaceName, %musicStreamId, %videoURL, %building, %CurrentLayout);
};
function late_OnEnterSpace(%spaceName, %musicStreamId, %videoURL, %building, %CurrentLayout) {
    if (!(isObject($player))) {
        schedule(500, 0, late_OnEnterSpace, %spaceName, %musicStreamId, %videoURL, %building, %CurrentLayout);
        return;
    }
    CustomSpaceClient::SetSpaceImIn(%building, %spaceName);
    if (!(%musicStreamId $= "")) {
        %musicStreamId.syncPlayingAudioStream(CSMediaDisplay);
    }
    if (!(%videoURL $= "no-video")) {
        %videoURL.syncPlayingMediaStream(CSMediaDisplay);
    }
    %CurrentLayout.updateSettings(CSLayoutSelector, $CSLayoutSelector::NumLayouts);
    CSInventoryBrowserWindow.Initialize();
    CSShoppingBrowserWindow.Initialize();
};
function clientCmdCS_OnLeaveSpace(%spaceName) {
    if ((CustomSpaceClient::GetCurrentSpaceName() $= %spaceName)) {
        %analyticSpace = strreplace(%spaceName, ".", " ");
        %analytic = getAnalytic();
        "/client/apartment/" @ getWord(%analyticSpace, 0) @ "/exit".trackPageView(%analytic);
        CustomSpaceClient::SetupClientAsNotInSpace();
        if (!($CSSpaceInfo $= "")) {
        }
        if (isObject($CSSpaceInfo)) {
            destroySpaceInfo($CSSpaceInfo);
            $CSSpaceInfo = 0;
        }
        PrivateSpacePopupMenuButton.hideButton(ButtonBar);
        CustomSpaceClient::SetCurrentSpaceName("");
    }
};
function destroySpaceInfo(%spaceInfo) {
    if (!(isObject(%spaceInfo))) {
        return;
    }
    if (!(%spaceInfo.videoplayer $= "")) {
        %spaceInfo.videoplayer.unloadVideoRenderer();
    }
    %spaceInfo.delete();
};
$CSNewInventoryGhostRefreshEvent = 0;
function clientCmdCS_OnInventoryCreated(%sku, %referenceName, %isOwned, %freeRotate) {
    %referenceName.SelectNuggetID(CSFurnitureMover);
    $CSSelectedIsOwned = %isOwned;
    $CSSelectedFreeRotate = %freeRotate;
    if (GrowingPlantClient::isPlant(%sku)) {
        GrowingPlantClient::onPlantCreated(%referenceName);
    }
    if (isEventPending($CSNewInventoryGhostRefreshEvent)) {
        cancel($CSNewInventoryGhostRefreshEvent);
    }
    $CSNewInventoryGhostRefreshEvent = schedule(500, 0, "getNuggetGhostList", "CSFurnitureMover::refreshGhostList");
};
function clientCmdCS_OnInventoryCreationFailed(%sku, %isOwned) {
    refreshActiveFurniture();
    -(1.0).SelectNuggetID(CSFurnitureMover);
};
function clientCmdCS_OnEnterEntryPortal(%buildingName) {
    Canvas.forceRightMouseUp();
    %buildingName.open(CustomSpacesSelector);
};
function CustomSpaceClient::CheckBlockUserFromSpace(%playerName, %unblock) {
    if ((%unblock $= "")) {
    }
    %blockText = (%unblock == 0.0) ? "block" : "unblock";
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
        handleSystemMessage("msgInfoMessage", "Sorry, you must be in a space to " @ %blockText @ " users from it.");
        return 0;
    }
    if ((%playerName $= "")) {
        handleSystemMessage("msgInfoMessage", "You didn't specify anyone to " @ %blockText);
        return 0;
    }
    if (!("manageUsers".rolesPermissionCheckNoWarn($player))) {
    }
    if (!(CustomSpaceClient::isOwner())) {
        handleSystemMessage("msgInfoMessage", "You must be the owner of a space to " @ %blockText @ " users from it.");
        return 0;
    }
    return 1;
};
function CustomSpaceClient::setCoHostHood(%playerName, %set, %confirm) {
    if (!(isDefined("%confirm"))) {
        %confirm = 1;
    }
    if (%set) {
    }
    if (%confirm) {
        %msg = %confirm[$MsgCat::custSpace TAB "OWNER_ACTION" @ "COHOST-CONFIRM"];
        %msg = strreplace(%msg, "[TARGET]", %playerName);
        MessageBoxYesNo("Make Co-Host", %msg, "CommandToServer('setCohostHood', \"" @ %playerName @ "\", true);", "");
    }
    commandToServer('setCohostHood', %playerName, %set);
};
function CustomSpaceClient::toggleCoHostHood(%playerName) {
    %player = Player::findPlayerInstance(%playerName);
    if (!(isObject(%player))) {
        error(getScopeName() @ " " @ "- could not find player" @ " " @ %playerName @ " " @ getTrace());
        return;
    }
    %state = %player.isCohost();
    CustomSpaceClient::setCoHostHood(%playerName, !(%state), 0);
};
function CustomSpaceClient::TryBlockUserFromSpace(%playerName, %unblock) {
    if (!(CustomSpaceClient::CheckBlockUserFromSpace(%playerName, %unblock))) {
        return;
    }
    if (%unblock) {
        CustomSpaceClient::ReallyTryBlockUserFromSpace(%playerName, %unblock);
        return;
    }
    MessageBoxYesNo("Block" @ " " @ %playerName @ " " @ "from this space", "This will block" @ " " @ %playerName @ " " @ "from entering this space. You can unblock them later. Do you want to block" @ " " @ %playerName @ " " @ "?", "CustomSpaceClient::ReallyTryBlockUserFromSpace(\"" @ %playerName @ "\", " @ %unblock @ ");", "");
};
function CustomSpaceClient::ReallyTryBlockUserFromSpace(%playerName, %unblock) {
    if (!(CustomSpaceClient::CheckBlockUserFromSpace(%playerName, %unblock))) {
        return;
    }
    if ((%unblock $= "")) {
    }
    %blockText = (%unblock == 0.0) ? "block" : "unblock";
    %space = CustomSpaceClient::GetSpaceImIn();
    %request = new ManagerRequest("") {
        className = "BanFromSpaceRequest";
    };
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    %request.blockedPlayer = %playerName;
    %request.blockText = %blockText;
    %url = $Net::ClientServiceURL @ "/BanFromSpace" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token) @ "&space=" @ urlEncode(%space) @ "&userToBan=" @ urlEncode(%playerName);
    if ((%unblock == 1.0)) {
    }
    if (!(%unblock $= "")) {
        %url = %url @ "&unban=true";
    }
    echo("BanFromSpaceRequest: " @ %url);
    log("network", "debug", "BanFromSpaceRequest: " @ %url);
    %url.setURL(%request);
    %request.start();
};
function BanFromSpaceRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        handleSystemMessage("msgInfoMessage", "" @ %this.blockText @ " unsuccessful.");
    }
    handleSystemMessage("msgInfoMessage", "" @ getPlayerMarkup(%this.blockedPlayer, "", 1) @ " was " @ %this.blockText @ "ed from this space.");
    if ((%this.blockText $= "block")) {
        if (!($CSBlockedList $= "")) {
            $CSBlockedList = $CSBlockedList @ "\t" @ %this.blockedPlayer;
        }
        $CSBlockedList = %this.blockedPlayer;
    }
    $CSBlockedList = removeField($CSBlockedList, findField($CSBlockedList, %this.blockedPlayer));
};
function CustomSpaceClient::TryBootAllUsersFromSpace(%space) {
    if (!("manageUsersBasic".rolesPermissionCheckNoWarn($player))) {
    }
    if (!(CustomSpaceClient::isOwner())) {
        return;
    }
    if ((%space $= "")) {
    }
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
        handleSystemMessage("msgInfoMessage", "You must either specify a space or be in one to boot all users.");
        return;
    }
    if ((%space $= "")) {
        %space = CustomSpaceClient::GetSpaceImIn();
    }
    %request = new ManagerRequest("") {
        className = "BootAllFromSpaceRequest";
    };
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    %request.spaceName = %space;
    %url = $Net::ClientServiceURL @ "/BootAllFromSpace" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token) @ "&space=" @ urlEncode(%space);
    log("network", "debug", "BootAllFromSpaceRequest: " @ %url);
    %url.setURL(%request);
    %request.start();
};
function BootAllFromSpaceRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        handleSystemMessage("msgInfoMessage", "Couldn't boot all from " @ %this.spaceName @ ".");
    }
    handleSystemMessage("msgInfoMessage", "Booted everyone from " @ %this.spaceName @ ".");
};
function CustomSpaceClient::doOwnerAction(%action, %target) {
    commandToServer('OwnerAction', %action, %target);
};
function teleportToAdjacentSpace(%next) {
    if (($player.getControlObject() == $player.client.Camera)) {
        if (%next) {
            commandToServer('GoToNextSpaceBasedOnCamera');
        }
        commandToServer('GoToPrevSpaceBasedOnCamera');
        return;
    }
    if (%next) {
        commandToServer('GoToNextSpaceBasedOnLogicalName', CustomSpaceClient::GetSpaceImIn());
    }
    commandToServer('GoToPrevSpaceBasedOnLogicalName', CustomSpaceClient::GetSpaceImIn());
};
function teleportToSpaceNumber(%number) {
    commandToServer('GoToSpaceNumber', %number);
};
function getBuildingDirectory(%buildingName, %callbackFn, %callbackFail) {
    %BuildingDirRequest = new SimObject("");
    if (isObject(MissionCleanup)) {
        %BuildingDirRequest.add(MissionCleanup);
    }
    %BuildingDirRequest.callback = %callbackFn;
    %BuildingDirRequest.callbackFailure = %callbackFail;
    %BuildingDirRequest.buildingName = %buildingName;
    %BuildingDirRequest.spaceName = "";
    %BuildingDirRequest.doneBuildingInfo = 0;
    %BuildingDirRequest.doneCSList = 0;
    doCheckForBuildingInfo(%BuildingDirRequest, 0);
    doGetSpaceInfo(%BuildingDirRequest);
};
function getBuildingSpaceInfo(%buildingName, %spaceName, %callbackFn, %callbackFail) {
    %BuildingDirRequest = new SimObject("");
    if (isObject(MissionCleanup)) {
        %BuildingDirRequest.add(MissionCleanup);
    }
    %BuildingDirRequest.callback = %callbackFn;
    %BuildingDirRequest.callbackFailure = %callbackFail;
    %BuildingDirRequest.buildingName = %buildingName;
    %BuildingDirRequest.spaceName = %spaceName;
    %BuildingDirRequest.doneBuildingInfo = 0;
    %BuildingDirRequest.doneCSList = 0;
    doCheckForBuildingInfo(%BuildingDirRequest, 0);
    doGetSpaceInfo(%BuildingDirRequest);
};
function doCheckForBuildingInfo(%BuildingDirRequest, %forceupdate) {
    safeEnsureScriptObject("SimSet", "BuildingInfos");
    %buildingInfo = findBuildingInfo(%BuildingDirRequest.buildingName);
    if ((%buildingInfo != 0.0)) {
        if (!(%forceupdate)) {
            %BuildingDirRequest.buildingInfo = %buildingInfo;
            %BuildingDirRequest.doneBuildingInfo = 1;
            checkDoneBuildingDirectory(%BuildingDirRequest);
            return;
        }
        clearBuildingInfo(%buildingInfo);
    }
    GetBuildingInfoRequest(%BuildingDirRequest);
};
function checkDoneBuildingDirectory(%BuildingDirRequest) {
    if (!(isObject(%BuildingDirRequest))) {
        log("network", "error", "Directory tracker is not an object?!");
        return;
    }
    log("network", "debug", "Checking directory request done.");
    log("network", "debug", "Building Info    = " @ %BuildingDirRequest.doneBuildingInfo);
    log("network", "debug", "CustomSpace List = " @ %BuildingDirRequest.doneCSList);
    if (%BuildingDirRequest.doneBuildingInfo) {
    }
    if (%BuildingDirRequest.doneCSList) {
        linkSpaces(%BuildingDirRequest.buildingInfo, %BuildingDirRequest.spaces);
        %command = %BuildingDirRequest.callback @ "( %BuildingDirRequest.buildingInfo, %BuildingDirRequest.spaces);";
        log("network", "debug", "About to eval callback: \"" @ %command @ "\"");
        eval(%command);
        %BuildingDirRequest.delete();
    }
};
function linkSpaces(%buildingInfo, %spaceGroup) {
    log("network", "debug", "linking " @ %spaceGroup.getCount() @ " apartments to building info and floor plans");
    %idx = 0;
    while ((%idx < %spaceGroup.getCount())) {
        %space = %idx.getObject(%spaceGroup);
        %space.buildingInfo = %buildingInfo;
        %space.floorplan = findFloorPlan(%buildingInfo, %space.floorPlanName);
        %idx = (%idx + 1.0);
    }
};
function getOwnerSpacesInfo(%ownerName, %onCompleteFN) {
    %tracker = getOwnerSpaceInfoTracker(%ownerName);
    %tracker.onCompleteFN = %onCompleteFN;
    %request = sendRequest_GetCustomSpaceInfo("", "", %ownerName, "onDoneOrErrorCallback_GetCustomSpaceInfo");
    %request.tracker = %tracker;
};
function ownerHasSpaceWithFloorplan(%ownerName, %floorplanName) {
    %tracker = getOwnerSpaceInfoTracker(%ownerName);
    %count = %tracker.getCount();
    if ((%count < 1.0)) {
        error(getScopeName() @ " " @ "- user owns no spaces! (tracker not filled yet, probably)" @ " " @ %ownerName @ " " @ getTrace());
        return 0;
    }
    %n = 0;
    while ((%n < %count)) {
        %space = %n.getObject(%tracker);
        %space.dumpValues();
        %fpn = "floorPlan".get(%space);
        if ((%fpn $= %floorplanName)) {
            return 1;
        }
        %n = (%n + 1.0);
    }
    return 0;
};
function getOwnerSpaceInfoTracker(%ownerName) {
    safeEnsureScriptObject("StringMap", "gOwnerSpaceInfoTrackers", 0);
    %tracker = %ownerName.get(gOwnerSpaceInfoTrackers);
    if (!(isObject(%tracker))) {
        %tracker = safeNewScriptObject("SimSet", "", 0);
        %tracker.put(gOwnerSpaceInfoTrackers, %ownerName);
        %tracker.ownerName = %ownerName;
    }
    return %tracker;
};
function onDoneOrErrorCallback_GetCustomSpaceInfo(%request, %result) {
    log("network", "debug", getScopeName() @ " " @ "- url =" @ " " @ %request.getURL());
    %tracker = %request.tracker;
    if (!(isObject(%request.tracker))) {
        error(getScopeName() @ " " @ "- no tracker. should be impossible.");
        return;
    }
    if (!(%request.checkSuccess())) {
        %cb = %tracker.completionCallback;
        if (!(%cb $= "")) {
            call(%cb, %request);
        }
        return;
    }
    %tracker.deleteMembers();
    %listBase = "space";
    %numSpaces = %listBase @ "Count".getResult(%request);
    %n = 0;
    while ((%n < %numSpaces)) {
        %map = safeNewScriptObject("StringMap", "", 0);
        %listItemNameBase = %listBase @ %n;
        %fields = "";
        %fields = %fields @ "\t" @ "URI";
        %fields = %fields @ "\t" @ "access";
        %fields = %fields @ "\t" @ "audioStream";
        %fields = %fields @ "\t" @ "building";
        %fields = %fields @ "\t" @ "customSpaceId";
        %fields = %fields @ "\t" @ "description";
        %fields = %fields @ "\t" @ "featured";
        %fields = %fields @ "\t" @ "floorPlan";
        %fields = %fields @ "\t" @ "friendOccupancy";
        %fields = %fields @ "\t" @ "location.areaName";
        %fields = %fields @ "\t" @ "location.buildingName";
        %fields = %fields @ "\t" @ "location.serverName";
        %fields = %fields @ "\t" @ "longDescription";
        %fields = %fields @ "\t" @ "name";
        %fields = %fields @ "\t" @ "occupancy";
        %fields = %fields @ "\t" @ "owner";
        %fields = %fields @ "\t" @ "type";
        %fields = %fields @ "\t" @ "videoStream";
        %fields = %fields @ "\t" @ "banCount";
        %fields.copyListValuesIntoMap(%request, %map, %listItemNameBase);
        %fields = "";
        %m = 0;
        while ((%m < "banCount".get(%map))) {
            %fields = %fields @ "\t" @ "ban" @ %m;
            %m = (%m + 1.0);
        }
        %fields.copyListValuesIntoMap(%request, %map, %listItemNameBase);
        %map.URI = (%m < "banCount".get(%map)) @ vurlClearResolution(%map.URI);
        %map.add(%tracker);
        %n = (%n + 1.0);
    }
    if (isObject(CSSpaceModelAptText)) {
        CSSpaceModelAptText.update();
    }
    if (!((%n < %numSpaces) @ " " @ %tracker.onCompleteFN $= "")) {
        call(%tracker.onCompleteFN, %tracker);
    }
};
function addBuildingInfo(%buildingInfo) {
    safeEnsureScriptObject("SimGroup", "BuildingInfos");
    log("network", "debug", "Adding building info for \"" @ %buildingInfo.name @ "\" to cache");
    %buildingInfo.add(BuildingInfos);
    %buildingInfo.bringToFront(BuildingInfos);
    log("network", "debug", "Cache now contains " @ BuildingInfos.getCount() @ " items.");
};
function findBuildingInfo(%buildingName) {
    safeEnsureScriptObject("SimGroup", "BuildingInfos");
    %count = BuildingInfos.getCount();
    log("network", "debug", "Searching for \"" @ %buildingName @ "\" in cache (" @ %count @ " items)");
    %idx = 0;
    while ((%idx < %count)) {
        %buildingInfo = %idx.getObject(BuildingInfos);
        if ((stricmp(%buildingInfo.name, %buildingName) == 0.0)) {
            log("network", "debug", "Found item at index " @ %idx);
            %buildingInfo.bringToFront(BuildingInfos);
            return %buildingInfo;
        }
        %idx = (%idx + 1.0);
    }
    log("network", "debug", "Item not found in cache");
    return 0;
};
function clearBuildingInfo(%buildingInfo) {
    safeEnsureScriptObject("SimGroup", "BuildingInfos");
    %buildingInfo.remove(BuildingInfos);
    %idx = 0;
    while ((%idx < %buildingInfo.floorPlanCount)) {
        %buildingInfo.floorplan.delete(%idx);
        %idx = (%idx + 1.0);
    }
    "delete".schedule(%buildingInfo, 0);
};
function findFloorPlan(%buildingInfo, %floorplanName) {
    %idx = 0;
    while ((%idx < %buildingInfo.floorPlanCount)) {
        %floorplan = %buildingInfo.floorplan;
        %idx;
        if ((0.0 == stricmp(%floorplanName, %floorplan.name))) {
            return %floorplan;
        }
        %idx = (%idx + 1.0);
    }
    return 0;
};
function GetBuildingInfoRequest(%tracker) {
    if ($StandAlone) {
        echo("we are in standalone, faking this");
        if (!(%tracker.callbackFailure $= "")) {
            %command = %tracker.callbackFailure @ "( %tracker.buildingName, \"we are in standalone mode so failing this\");";
            log("network", "debug", "About to eval callback: \"" @ %command @ "\"");
            eval(%command);
        }
        return;
    }
    %request = new ManagerRequest("") {
        className = "GetBuildingInfo";
    };
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    %url = $Net::ClientServiceURL @ "/GetBuildingInfo" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token);
    %url = %url @ "&building=" @ urlEncode(%tracker.buildingName);
    %request.tracker = %tracker;
    log("network", "debug", "GetBuildingInfoRequest: " @ %url);
    %url.setURL(%request);
    %request.start();
};
function GetBuildingInfo::onDone(%this) {
    echo(getScopeName());
    %status = findRequestStatus(%this);
    log("network", "debug", "GetBuildingInfo status: " @ %status);
    if ((%status $= "fail")) {
        echo(getScopeName() @ "->failed");
        %statusMsg = "statusMsg".getValue(%this);
        log("network", "debug", "GetBuildingInfo failed due to: " @ %statusMsg);
        if (!(%this.tracker.callbackFailure $= "")) {
            %command = %this.tracker.callbackFailure @ "( %this.tracker.buildingName, %statusMsg);";
            log("network", "debug", "About to eval callback: \"" @ %command @ "\"");
            eval(%command);
        }
    }
    %buildingInfo = new SimObject("");
    if (isObject(MissionCleanup)) {
        %buildingInfo.add(MissionCleanup);
    }
    %buildingInfo.name = "name".getValue(%this);
    %buildingInfo.city = "city".getValue(%this);
    %buildingInfo.description = urlDecode("description".getValue(%this));
    %buildingInfo.floorPlanCount = "floorPlansCount".getValue(%this);
    %idx = 0;
    while ((%idx < %buildingInfo.floorPlanCount)) {
        %floorplan = new SimObject("");
        if (isObject(MissionCleanup)) {
            %floorplan.add(MissionCleanup);
        }
        %floorplan.name = "floorPlans" @ %idx @ ".name".getValue(%this);
        %floorplan.description = "floorPlans" @ %idx @ ".description".getValue(%this);
        %floorplan.capacity = "floorPlans" @ %idx @ ".capacity".getValue(%this);
        %floorplan.minLevel = "floorPlans" @ %idx @ ".minLevel".getValue(%this);
        %floorplan.priceVBux = "floorPlans" @ %idx @ ".priceVBux".getValue(%this);
        %floorplan.priceVPoints = "floorPlans" @ %idx @ ".priceVPoints".getValue(%this);
        %floorplan.isUpgrade = (stricmp("floorPlans" @ %idx @ ".upgrade".getValue(%this), "true") == 0.0);
        %floorplan.numAvailable = "floorPlans" @ %idx @ ".numAvailable".getValue(%this);
        if (isObject(%idx, %buildingInfo.floorplan)) {
            %buildingInfo.floorplan.delete(%idx);
        }
        %buildingInfo.floorplan = %floorplan @ %idx;
        %idx = (%idx + 1.0);
    }
    addBuildingInfo(%buildingInfo);
    log("network", "debug", "Got building info for \"" @ %buildingInfo.name @ "\" with " @ %buildingInfo.floorPlanCount @ " floor plans");
    if (isObject(%this.tracker)) {
        %this.tracker.buildingInfo = (%idx < %buildingInfo.floorPlanCount) @ %buildingInfo;
        %this.tracker.doneBuildingInfo = 1;
    }
    log("network", "warn", "%this.tracker is not an object.");
    if (isObject(%this.tracker)) {
        checkDoneBuildingDirectory(%this.tracker);
    }
    "delete".schedule(%this, 0);
};
function GetBuildingInfo::onError(%this, %unused, %errMsg) {
    if (!(%this.tracker.callbackFailure $= "")) {
        %command = %this.tracker.callbackFailure @ "( %this.tracker.buildingName, %errMsg);";
        log("network", "debug", "About to eval callback: \"" @ %command @ "\"");
        eval(%command);
    }
    log("network", "debug", "GetBuildingInfo::onError: " @ %errMsg);
    "delete".schedule(%this, 0);
};
function doGetSpaceInfo(%tracker) {
    if ($StandAlone) {
        echo("we are in standalone, faking this");
        if (!(%tracker.callbackFailure $= "")) {
            %command = %tracker.callbackFailure @ "( %tracker.buildingName, \"we are in standalone mode so failing this\");";
            log("network", "debug", "About to eval fail callback: \"" @ %command @ "\"");
            eval(%command);
        }
        return;
    }
    %request = new ManagerRequest("") {
        className = "GetSpaceInfo";
    };
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    %url = $Net::ClientServiceURL;
    %url = %url @ "/GetCustomSpaceInfo";
    %url = %url @ "?user=" @ urlEncode($Player::Name);
    %url = %url @ "&token=" @ urlEncode($Token);
    if (!(%tracker.buildingName $= "")) {
        %url = %url @ "&building=" @ urlEncode(%tracker.buildingName);
    }
    if (!(%tracker.spaceName $= "")) {
        %url = %url @ "&space=" @ urlEncode(%tracker.spaceName);
    }
    if (!(%tracker.ownerName $= "")) {
        %url = %url @ "&owner=" @ urlEncode(%tracker.ownerName);
    }
    %request.tracker = %tracker;
    %url.setURL(%request);
    %request.start();
};
function GetSpaceInfo::onDone(%this) {
    %status = findRequestStatus(%this);
    log("network", "debug", "GetSpaceInfo status: " @ %status);
    if ((%status $= "fail")) {
        %statusMsg = "statusMsg".getValue(%this);
        error(getScopeName() @ " " @ "- failed w/" @ " " @ %statusMsg);
        if (!(%this.tracker.callbackFailure $= "")) {
            %command = %this.tracker.callbackFailure @ "( %this.tracker.buildingName, %statusMsg);";
            log("network", "debug", "About to eval callback: \"" @ %command @ "\"");
            eval(%command);
        }
    }
    %this.tracker.spaceBuildingName = "building".getValue(%this);
    %this.tracker.spaceCount = "spaceCount".getValue(%this);
    %this.tracker.spaces = new SimGroup("");
    %idx = 0;
    while ((%idx < %this.tracker.spaceCount)) {
        %space = new SimObject("");
        if (isObject(MissionCleanup)) {
            %space.add(MissionCleanup);
        }
        %space.access = "space" @ %idx @ ".access".getValue(%this);
        %space.audioStream = "space" @ %idx @ ".audioStream".getValue(%this);
        %space.description = "space" @ %idx @ ".description".getValue(%this);
        %space.isFeatured = ("space" @ %idx @ ".featured".getValue(%this) $= "true");
        %space.floorPlanName = "space" @ %idx @ ".floorPlan".getValue(%this);
        %space.longDescription = "space" @ %idx @ ".longDescription".getValue(%this);
        %space.name = "space" @ %idx @ ".name".getValue(%this);
        %space.occupancy = "space" @ %idx @ ".occupancy".getValue(%this);
        %space.owner = "space" @ %idx @ ".owner".getValue(%this);
        %space.password = "space" @ %idx @ ".password".getValue(%this);
        %space.type = "space" @ %idx @ ".type".getValue(%this);
        %space.vurl = "space" @ %idx @ ".URI".getValue(%this);
        %space.videoStream = "space" @ %idx @ ".videoStream".getValue(%this);
        %space.buildingName = "space" @ %idx @ ".building".getValue(%this);
        %space.blockedList = "";
        %banCount = "space" @ %idx @ ".banCount".getValue(%this);
        if ((%banCount $= "")) {
            %banCount = 0;
        }
        %k = 0;
        while ((%k < %banCount)) {
            %blockedUser = "space" @ %idx @ ".ban" @ %k.getValue(%this);
            if ((%blockedUser $= "")) {
                warn(getScopeName() @ "->banned user #" @ %k @ " out of " @ %banCount @ ", was NULL!");
            }
            if ((findField(%space.blockedList, %blockedUser) >= 0.0)) {
                warn(getScopeName() @ "->banned user #" @ %k @ " OUT OF " @ %banCount @ ", is a duplicate entry! entry = " @ %blockedUser @ " .");
            }
            if (!(%space.blockedList $= "")) {
                %space.blockedList = %space.blockedList @ "\t" @ %blockedUser;
            }
            %space.blockedList = %blockedUser;
            %k = (%k + 1.0);
        }
        %space.blockedList = (%k < %banCount) @ trim(%space.blockedList);
        %space.add(%this.tracker.spaces);
        %idx = (%idx + 1.0);
    }
    if (isObject(%this.tracker)) {
        %this.tracker.doneCSList = (%idx < %this.tracker.spaceCount) @ 1;
        checkDoneBuildingDirectory(%this.tracker);
    }
    "delete".schedule(%this, 0);
};
function GetSpaceInfo::onError(%this, %unused, %errMsg) {
    log("network", "debug", "GetSpaceInfo::onError: " @ %errMsg);
    if (!(%this.tracker.callbackFailure $= "")) {
        %command = %this.tracker.callbackFailure @ "( %this.tracker.buildingName, %errMsg);";
        log("network", "debug", "About to eval callback: \"" @ %command @ "\"");
        eval(%command);
    }
    "delete".schedule(%this, 0);
};
function purchaseApartmentRequest(%space, %useBux, %unused, %callback, %callbackFail) {
    %request = new ManagerRequest("") {
        className = "PurchaseSpaceRequest";
    };
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    %request.callback = %callback;
    %request.callbackFail = %callbackFail;
    %url = $Net::ClientServiceURL @ "/PurchaseCustomSpace?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "building=" @ urlEncode(%space.buildingInfo.name) @ "&" @ "floorPlan=" @ urlEncode(%space.floorPlanName) @ "&" @ "payWith=" @ %useBux ? "vbux" : "vpoints";
    log("network", "debug", "PurchaseSpace: " @ %url);
    %url.setURL(%request);
    %request.start();
};
function PurchaseSpaceRequest::onDone(%this) {
    echo(getScopeName());
    %status = findRequestStatus(%this);
    log("network", "info", "PurchaseSpaceRequest status: " @ %status);
    if ((%status $= "success")) {
        %name = "name".getValue(%this);
        %building = "building".getValue(%this);
        %vurl = "vurl".getValue(%this);
        if ((%vurl $= "")) {
            warn("Server not returning space VURL in .vurl parameter.");
            %vurl = "URI".getValue(%this);
        }
        if (!(%this.callback $= "")) {
            %command = %this.callback @ "( %building, %name, %vurl );";
            log("network", "debug", "About to eval callback: " @ %command);
            eval(%command);
        }
    }
    %result = "items0.validationResults".getValue(%this);
    if (!(%this.callbackFail $= "")) {
        if (!(%result $= "")) {
            %command = %this.callbackFail @ "( %result );";
        }
        %command = %this.callbackFail @ "( \"error\" );";
        eval(%command);
    }
    "delete".schedule(%this, 0);
};
function PurchaseSpaceRequest::onError(%this, %unused, %errMsg) {
    log("network", "debug", "PurchaseSpaceRequest::onError: " @ %errMsg);
    if (!(%this.callbackFail $= "")) {
        %command = %this.callbackFail @ "( %status );";
        eval(%command);
    }
    "delete".schedule(%this, 0);
};
function getCustomSpacePurchaseInfo(%space, %callback) {
    %request = new ManagerRequest("") {
        className = "CustomSpacePurchaseInfo";
    };
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    %request.callback = %callback;
    %request.space = %space;
    %url = $Net::ClientServiceURL @ "/GetCustomSpacePurchaseInfo?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "building=" @ urlEncode(%space.buildingInfo.name) @ "&" @ "floorPlan=" @ urlEncode(%space.floorPlanName);
    log("network", "debug", "PurchaseSpace: " @ %url);
    %url.setURL(%request);
    %request.start();
};
function CustomSpacePurchaseInfo::onDone(%this) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        %statusMsg = "statusMsg".getValue(%this);
        handleSystemMessage("msgInfoMessage", "" @ %this.blockText @ " unsuccessful.");
        return;
    }
    %floorplan = %this.space.floorplan;
    %floorplan.sku = "sku".getValue(%this);
    %floorplan.minLevel = "minLevel".getValue(%this);
    %floorplan.quantity = "quantity".getValue(%this);
    %floorplan.priceVBux = "priceVBux".getValue(%this);
    %floorplan.priceVPoints = "priceVPoints".getValue(%this);
    %floorplan.tradeInValueVBux = "tradeInCreditVBux".getValue(%this);
    %floorplan.tradeInValueVPoints = "tradeInCreditVPoints".getValue(%this);
    %floorplan.expectedError = "expectedError".getValue(%this);
    if ((%floorplan.tradeInValueVBux > 0.0)) {
    }
    %floorplan.isUpgrade = (%floorplan.tradeInValueVPoints > 0.0);
    if (!(%this.callback $= "")) {
        %cmd = %this.callback @ "(" @ %this.space @ ");";
        eval(%cmd);
    }
    %this.delete();
};
function CustomSpacePurchaseInfo::onError(%this, %unused, %errMsg) {
};
function csSelectLayout(%layoutToSelect) {
    commandToServer('CSSelectLayout', CustomSpaceClient::GetSpaceImIn(), %layoutToSelect);
};
function clientCmdCSLayoutSelected(%unused, %audioStream, %videoStream) {
    refreshActiveFurniture();
    -(1.0).SelectNuggetID(CSFurnitureMover);
    if ((%videoStream $= "")) {
    }
    %envMgrVideoStr = %videoStream;
    "no-video";
    CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), "", "", "", %audioStream, %envMgrVideoStr);
};
function csCopyLayoutFromTo(%from, %to) {
    if ((%from == %to)) {
        error(getScopeName() @ "->being asked to copy current layout into itself! returning.");
        return;
    }
    commandToServer('CSCopyLayoutFromTo', CustomSpaceClient::GetSpaceImIn(), %from, %to);
};
function clientCmdCSGotLayoutVitals(%infoStr) {
    %layoutNum = getField(%infoStr, 0);
    if ((CSLayoutSelector.copyTarget == %layoutNum)) {
        %infoStr.gotCopyTargetInfo(CSLayoutSelector);
    }
};
function csGetLayoutVitals(%layoutNum) {
    commandToServer('CSGetLayoutVitals', CustomSpaceClient::GetSpaceImIn(), %layoutNum);
};
function csSaveMySpacePropertiesAsDefault() {
    commandToServer('CSSaveSpacePropertiesAsDefault', CustomSpaceClient::GetSpaceImIn());
};
function csSaveLayoutAsDefault(%layoutNum) {
    commandToServer('CSSaveLayoutAsDefault', CustomSpaceClient::GetSpaceImIn(), %layoutNum);
};
function csLoadMediaFavorites() {
    %mediafavorites = "vside://radio/" @ $CSSpaceInfo.audioStream @ "\t" @ $CSSpaceInfo.videoStream.getProperty(gUserPropMgrClient, $Player::Name, "mediafavoritelist");
    echo("csLoadMediaFavorites - \"" @ %mediafavorites @ "\"");
    %mediafavorites.setMediaFavorites(CSMediaDisplay);
};
function csSaveMediaFavorites() {
    %mediafavorites = CSMediaDisplay.getMediaFavorites();
    %mediafavorites.setProperty(gUserPropMgrClient, $Player::Name, "mediafavoritelist");
};
function csRequestHotMedia() {
    %request = new ManagerRequest("") {
        className = "GetUrlRatingListRequest";
    };
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    %url = $Net::ClientServiceURL @ "/GetUrlRatingList?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "type=VIDEO" @ "&" @ "order=BY_SHOWS" @ "&" @ "first=0" @ "&" @ "count=" @ $CSMediaDisplay::DefaultFavoriteCount;
    log("network", "debug", "requesting 'hot' media");
    %url.setURL(%request);
    %request.start();
};
function GetUrlRatingListRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        %status.onError(%this, %this, 0);
        return;
    }
    %mediaList = "";
    %count = "mediaCount".getValue(%this);
    %idx = 0;
    if ((%idx < %count)) {
    }
    while ((%idx < $CSMediaDisplay::DefaultFavoriteCount)) {
        %mediaURL = urlDecode("media" @ %idx @ ".url".getValue(%this));
        %viewCount = "media" @ %idx @ ".viewCount".getValue(%this);
        %showCount = "media" @ %idx @ ".showCount".getValue(%this);
        if ((%showCount > 0.0)) {
            %mediaInfo = %mediaURL @ " " @ %viewCount @ " " @ %showCount;
            if ((%mediaList $= "")) {
                %mediaList = %mediaInfo;
            }
            %mediaList = %mediaList @ "\t" @ %mediaInfo;
        }
        %idx = (%idx + 1.0);
        if ((%idx < %count)) {
        }
    }
    %mediaList.setMediaHotlist(CSMediaDisplay);
    "delete".schedule(%this, 0);
};
function GetUrlRatingListRequest::onError(%this, %unused, %errMsg) {
    log("network", "warn", "GetUrlRatingRequest::onError: " @ %errMsg);
    "delete".schedule(%this, 0);
};
function csRequestMediaStatistics(%mediaURL) {
    %request = new ManagerRequest("") {
        className = "GetUrlRatingRequest";
    };
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    %url = $Net::ClientServiceURL @ "/GetUrlRating?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "url=" @ urlEncode(%mediaURL);
    log("network", "debug", "UrlRating: request rating for: " @ %mediaURL);
    %request.mediaurl = %mediaURL;
    %url.setURL(%request);
    %request.start();
};
function GetUrlRatingRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        %status.onError(%this, %this, 0);
        return;
    }
    %mediaURL = "url".getValue(%this);
    %mediaviews = "viewCount".getValue(%this);
    %mediaplays = "showCount".getValue(%this);
    %mediaplays.setMediaStatistics(CSMediaDisplay, %mediaURL, %mediaviews);
    "delete".schedule(%this, 0);
};
function GetUrlRatingRequest::onError(%this, %unused, %errMsg) {
    log("network", "warn", "GetUrlRatingRequest::onError: " @ %errMsg);
    %this.mediaurl.clearMediaStatistics(CSMediaDisplay);
    "delete".schedule(%this, 0);
};
function csRecordMediaShow(%mediaURL, %type) {
    %request = new ManagerRequest("") {
        className = "RecordUrlShowRequest";
    };
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    %url = $Net::ClientServiceURL @ "/RecordUrlShow?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "url=" @ urlEncode(%mediaURL) @ "&" @ "type=" @ %type;
    log("network", "debug", "ShowRequest: request rating for: " @ %mediaURL);
    %request.mediaurl = %mediaURL;
    %url.setURL(%request);
    %request.start();
};
function RecordUrlShowRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        %status.onError(%this, %this, 0);
        return;
    }
    "delete".schedule(%this, 0);
};
function RecordUrlShowRequest::onError(%this, %unused, %errMsg) {
    log("network", "warn", "RecordUrlShowRequest::onError: " @ %errMsg);
    "delete".schedule(%this, 0);
};
function csRecordMediaView(%mediaURL, %type) {
    %request = new ManagerRequest("") {
        className = "RecordUrlViewRequest";
    };
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    %url = $Net::ClientServiceURL @ "/RecordUrlView?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "url=" @ urlEncode(%mediaURL) @ "&" @ "type=" @ %type;
    log("network", "debug", "ViewRequest: request rating for: " @ %mediaURL);
    %request.mediaurl = %mediaURL;
    %url.setURL(%request);
    %request.start();
};
function RecordUrlViewRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        %status.onError(%this, %this, 0);
        return;
    }
    "delete".schedule(%this, 0);
};
function RecordUrlViewRequest::onError(%this, %unused, %errMsg) {
    log("network", "warn", "RecordUrlViewRequest::onError: " @ %errMsg);
    "delete".schedule(%this, 0);
};
