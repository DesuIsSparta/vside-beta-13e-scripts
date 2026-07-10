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
    if (isObject()) {
        close();
    }
    if (isObject()) {
        close();
    }
    if (isObject()) {
        close();
    }
    if (isObject()) {
        close();
    }
    if (isObject()) {
        close();
    }
    if (isObject()) {
        close();
    }
    if (isObject()) {
        close();
    }
    if (isObject()) {
        close();
    }
    if ((getCurrentTab() SPC name $= "private space")) {
        close();
    }
    "private space".hideTabWithName();
    if ((0.0 != $CSSpaceInfo)) {
        destroySpaceInfo($CSSpaceInfo);
    }
    $CSSpaceInfo = 0;
    HudTabs;
    $CSBuildingInfo = 0;
    HudTabs;
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
    if ((0.0 > numCSPanelsOpen())) {
        CustomSpaceClient::startEditingSpace();
    }
    CustomSpaceClient::stopEditingSpace();
};
function GotCustomSpaceInfo(%buildingInfo, %spaceGroup) {
    echo("received custom space info");
    if ((1.0 != %spaceGroup.getCount())) {
        log("network", "error", "GotCustomSpaceInfo returned a strange number of spaces (" @ %spaceGroup.getCount() @ ")");
    }
    if ((0.0 != $CSSpaceInfo)) {
    }
    if (isObject($CSSpaceInfo)) {
        destroySpaceInfo($CSSpaceInfo);
    }
    $CSBuildingInfo = %buildingInfo;
    $CSSpaceInfo = %spaceGroup.getObject(0);
    %spaceGroup.remove($CSSpaceInfo);
    %spaceGroup.delete();
    %isOwner = 0;
    if (isObject($player)) {
        %isOwner = ($CSSpaceInfo == stricmp(owner, $player.getShapeName()));
        0.0;
    }
    error("$Player is not an object. Setting space I'm in before I'm there...");
    CustomSpaceClient::SetUpOwnership(%isOwner);
    access.updateSettings(password, description);
    if (($CSSpaceInfo == stricmp(type, "model"))) {
        open();
        update();
        "MODEL_APT".selectTabWithName();
    }
    CustomSpacesClient::setMap2DText();
    CustomSpacesClient::InitializeVideoRequest();
};
function CustomSpacesClient::setMap2DText() {
    if (($CSSpaceInfo SPC type $= "CELEBSPACE")) {
    }
    if (($CSSpaceInfo @ name @ $CSSpaceInfo @ owner @ "\n\tBuilding:\t" SPC Buildings::GetDescription($CSBuildingName) $= "")) {
    }
    $CSSpaceInfo @ TryFixBadWords(description).setMap2DForCustomSpacesMode(geLocalMapContainer @ "<color:ffffff><just:center><b>" @ "<color:ffffff><tab:15,60>\tOwner:\t" @ $CSBuildingName @ Buildings::GetDescription($CSBuildingName) @ "\n\tCity:\t" @ getContiguousSpaceFullName(Buildings::GetContiguousSpace($CSBuildingName)));
};
$CSSpaceOwner = 0;
function CustomSpaceClient::SetUpOwnership(%isOwner) {
    %wasOwner = $CSSpaceOwner;
    log("network", "debug", "setting space ownership to " @ %isOwner);
    if ((0.0 == %isOwner)) {
        if (isObject()) {
            hideOP();
            disableOPlink();
        }
        if (isObject()) {
            if ((0.0 != $CSSpaceInfo)) {
            }
            if (($CSSpaceInfo == stricmp(type, "model"))) {
                open();
                "MODEL_APT".selectTabWithName();
            }
            close();
        }
        if (isObject()) {
            0.setChangeStationAllowed();
        }
        hideButton();
        $CSSpaceOwner = 0;
        PrivateSpacePopupMenuButton;
    }
    showButton();
    %showSpaceOwnerTip = $Player::Name.getProperty("ShowOwnerTip", 1);
    gUserPropMgrClient;
    if (%showSpaceOwnerTip) {
        $Player::Name.setProperty("ShowOwnerTip", 0);
        userTips::showNow("SpaceOwner");
    }
    $CSSpaceOwner = 1;
    gUserPropMgrClient;
    $CSBlockedList = blockedList;
    $CSSpaceInfo;
    Music::createGetMusicStreamsRequest();
    1.setChangeStationAllowed();
    csLoadMediaFavorites();
    csRequestHotMedia();
    getOwnedFurniture();
};
function CustomSpaceClient::isOwner() {
    return $CSSpaceOwner;
};
function CustomSpaceClient::placeSkuInWorld(%sku, %position, %orientation) {
    if ((0.0 <= %sku)) {
        error(getScopeName() @ " " @ "No sku selected");
        return;
    }
    if (!(isDefined("%position"))) {
        %position = "";
    }
    if (!(isDefined("%orientation"))) {
        %orientation = "";
    }
    if ((numOwnedFurnitureSku(%sku) >= numUsingFurnitureSku(%sku))) {
        return;
    }
    if (($CSMaximumSlots >= numUsingFurnitureAll())) {
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
    update();
    update();
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
    %spaceName.InitForSpace(%numberOfSlots);
};
function clientCmdCSOnUnownedInventoryTimeOut(%referenceID) {
    if (($CSSelectedID == %referenceID)) {
        -(1.0).SelectNuggetID();
        $CSInstaTestDrive = 0;
        CSFurnitureMover;
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
    close();
    CustomSpaceClient::SetCurrentSpaceName(%spaceName);
    Initialize();
    %analyticSpace = strreplace(%spaceName, ".", " ");
    CSMediaDisplay;
    %analytic = getAnalytic();
    CSControlPanel;
    %analytic.trackPageView("/client/apartment/" @ getWord(%analyticSpace, 0) @ "/enter");
    late_OnEnterSpace(%spaceName, %musicStreamId, %videoURL, %building, %CurrentLayout);
};
function late_OnEnterSpace(%spaceName, %musicStreamId, %videoURL, %building, %CurrentLayout) {
    if (!(isObject($player))) {
        schedule(500, 0, %spaceName, %musicStreamId, %videoURL, %building, %CurrentLayout);
        return late_OnEnterSpace;
    }
    CustomSpaceClient::SetSpaceImIn(%building, %spaceName);
    if (!(%musicStreamId $= "")) {
        %musicStreamId.syncPlayingAudioStream();
    }
    if (!(CSMediaDisplay SPC %videoURL $= "no-video")) {
        %videoURL.syncPlayingMediaStream();
    }
    $CSLayoutSelector::NumLayouts.updateSettings(%CurrentLayout);
    Initialize();
    Initialize();
};
function clientCmdCS_OnLeaveSpace(%spaceName) {
    if ((CustomSpaceClient::GetCurrentSpaceName() $= %spaceName)) {
        %analyticSpace = strreplace(%spaceName, ".", " ");
        %analytic = getAnalytic();
        %analytic.trackPageView("/client/apartment/" @ getWord(%analyticSpace, 0) @ "/exit");
        CustomSpaceClient::SetupClientAsNotInSpace();
        if (!($CSSpaceInfo $= "")) {
        }
        if (isObject($CSSpaceInfo)) {
            destroySpaceInfo($CSSpaceInfo);
            $CSSpaceInfo = 0;
        }
        hideButton();
        CustomSpaceClient::SetCurrentSpaceName("");
    }
};
function destroySpaceInfo(%spaceInfo) {
    if (!(isObject(%spaceInfo))) {
        return;
    }
    if (!(%spaceInfo SPC videoplayer $= "")) {
        videoplayer.unloadVideoRenderer();
    }
    %spaceInfo.delete();
};
$CSNewInventoryGhostRefreshEvent = 0;
function clientCmdCS_OnInventoryCreated(%sku, %referenceName, %isOwned, %freeRotate) {
    %referenceName.SelectNuggetID();
    $CSSelectedIsOwned = %isOwned;
    CSFurnitureMover;
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
    -(1.0).SelectNuggetID();
};
function clientCmdCS_OnEnterEntryPortal(%buildingName) {
    forceRightMouseUp();
    %buildingName.open();
};
function CustomSpaceClient::CheckBlockUserFromSpace(%playerName, %unblock) {
    if ((%unblock $= "")) {
    }
    %blockText = (0.0 == %unblock) ? "block" : "unblock";
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
        handleSystemMessage("msgInfoMessage", "Sorry, you must be in a space to " @ %blockText @ " users from it.");
        return 0;
    }
    if ((%playerName $= "")) {
        handleSystemMessage("msgInfoMessage", "You didn't specify anyone to " @ %blockText);
        return 0;
    }
    if (!($player.rolesPermissionCheckNoWarn("manageUsers"))) {
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
    %blockText = (0.0 == %unblock) ? "block" : "unblock";
    %space = CustomSpaceClient::GetSpaceImIn();
    className = ManagerRequest @ new ""() @ "BanFromSpaceRequest";
    0;
    %request = ;
    if (isObject()) {
        %request.add();
    }
    blockedPlayer = MissionCleanup @ %playerName @ %request;
    MissionCleanup;
    blockText = %blockText @ %request;
    %url = $Net::ClientServiceURL @ "/BanFromSpace" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token) @ "&space=" @ urlEncode(%space) @ "&userToBan=" @ urlEncode(%playerName);
    if ((1.0 == %unblock)) {
    }
    if (!(%unblock $= "")) {
        %url = %url @ "&unban=true";
    }
    echo("BanFromSpaceRequest: " @ %url);
    log("network", "debug", "BanFromSpaceRequest: " @ %url);
    %request.setURL(%url);
    %request.start();
};
function BanFromSpaceRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        handleSystemMessage("msgInfoMessage", "" @ %this @ blockText @ " unsuccessful.");
    }
    handleSystemMessage("msgInfoMessage", "" @ %this @ getPlayerMarkup(blockedPlayer, "", 1) @ " was " @ %this @ blockText @ "ed from this space.");
    if ((%this SPC blockText $= "block")) {
        if (!($CSBlockedList $= "")) {
            $CSBlockedList = %this @ blockedPlayer;
            $CSBlockedList @ "\t";
        }
        $CSBlockedList = blockedPlayer;
        %this;
    }
    $CSBlockedList = removeField($CSBlockedList, findField($CSBlockedList, blockedPlayer));
    %this;
};
function CustomSpaceClient::TryBootAllUsersFromSpace(%space) {
    if (!($player.rolesPermissionCheckNoWarn("manageUsersBasic"))) {
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
    className = ManagerRequest @ new ""() @ "BootAllFromSpaceRequest";
    0;
    %request = ;
    if (isObject()) {
        %request.add();
    }
    spaceName = MissionCleanup @ %space @ %request;
    MissionCleanup;
    %url = $Net::ClientServiceURL @ "/BootAllFromSpace" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token) @ "&space=" @ urlEncode(%space);
    log("network", "debug", "BootAllFromSpaceRequest: " @ %url);
    %request.setURL(%url);
    %request.start();
};
function BootAllFromSpaceRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        handleSystemMessage("msgInfoMessage", "Couldn't boot all from " @ %this @ spaceName @ ".");
    }
    handleSystemMessage("msgInfoMessage", "Booted everyone from " @ %this @ spaceName @ ".");
};
function CustomSpaceClient::doOwnerAction(%action, %target) {
    commandToServer('OwnerAction', %action, %target);
};
function teleportToAdjacentSpace(%next) {
    if ((Camera == $player.getControlObject())) {
        if (%next) {
            commandToServer('GoToNextSpaceBasedOnCamera');
        }
        commandToServer('GoToPrevSpaceBasedOnCamera');
        return client;
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
    %BuildingDirRequest = new ""();
    SimObject;
    if (isObject()) {
        %BuildingDirRequest.add();
    }
    callback = MissionCleanup @ %callbackFn @ %BuildingDirRequest;
    MissionCleanup;
    callbackFailure = 0 @ %callbackFail @ %BuildingDirRequest;
    buildingName = %buildingName @ %BuildingDirRequest;
    spaceName = "" @ %BuildingDirRequest;
    doneBuildingInfo = 0 @ %BuildingDirRequest;
    doneCSList = 0 @ %BuildingDirRequest;
    doCheckForBuildingInfo(%BuildingDirRequest, 0);
    doGetSpaceInfo(%BuildingDirRequest);
};
function getBuildingSpaceInfo(%buildingName, %spaceName, %callbackFn, %callbackFail) {
    %BuildingDirRequest = new ""();
    SimObject;
    if (isObject()) {
        %BuildingDirRequest.add();
    }
    callback = MissionCleanup @ %callbackFn @ %BuildingDirRequest;
    MissionCleanup;
    callbackFailure = 0 @ %callbackFail @ %BuildingDirRequest;
    buildingName = %buildingName @ %BuildingDirRequest;
    spaceName = %spaceName @ %BuildingDirRequest;
    doneBuildingInfo = 0 @ %BuildingDirRequest;
    doneCSList = 0 @ %BuildingDirRequest;
    doCheckForBuildingInfo(%BuildingDirRequest, 0);
    doGetSpaceInfo(%BuildingDirRequest);
};
function doCheckForBuildingInfo(%BuildingDirRequest, %forceupdate) {
    safeEnsureScriptObject("SimSet", "BuildingInfos");
    %buildingInfo = findBuildingInfo(buildingName);
    %BuildingDirRequest;
    if ((0.0 != %buildingInfo)) {
        if (!(%forceupdate)) {
            buildingInfo = %buildingInfo @ %BuildingDirRequest;
            doneBuildingInfo = 1 @ %BuildingDirRequest;
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
    log("network", "debug", %BuildingDirRequest @ doneBuildingInfo);
    log("network", "debug", %BuildingDirRequest @ doneCSList);
    if (doneBuildingInfo) {
    }
    if (doneCSList) {
        linkSpaces(buildingInfo, spaces);
        %command = %BuildingDirRequest @ callback @ "( %BuildingDirRequest.buildingInfo, %BuildingDirRequest.spaces);";
        %BuildingDirRequest;
        log("network", "debug", %BuildingDirRequest @ %BuildingDirRequest @ "About to eval callback: \"" @ %command @ "\"");
        eval(%command);
        %BuildingDirRequest.delete();
    }
};
function linkSpaces(%buildingInfo, %spaceGroup) {
    log("network", "debug", "linking " @ %spaceGroup.getCount() @ " apartments to building info and floor plans");
    %idx = 0;
    if ((%spaceGroup.getCount() < %idx)) {
        %space = %spaceGroup.getObject(%idx);
        buildingInfo = %buildingInfo @ %space;
        floorplan = %space @ findFloorPlan(%buildingInfo, floorPlanName) @ %space;
        %idx = (1.0 + %idx);
    }
};
function getOwnerSpacesInfo(%ownerName, %onCompleteFN) {
    %tracker = getOwnerSpaceInfoTracker(%ownerName);
    onCompleteFN = %onCompleteFN @ %tracker;
    %request = sendRequest_GetCustomSpaceInfo("", "", %ownerName, "onDoneOrErrorCallback_GetCustomSpaceInfo");
    tracker = %tracker @ %request;
};
function ownerHasSpaceWithFloorplan(%ownerName, %floorplanName) {
    %tracker = getOwnerSpaceInfoTracker(%ownerName);
    %count = %tracker.getCount();
    if ((1.0 < %count)) {
        error(getScopeName() @ " " @ "- user owns no spaces! (tracker not filled yet, probably)" @ " " @ %ownerName @ " " @ getTrace());
        return 0;
    }
    %n = 0;
    if ((%count < %n)) {
        %space = %tracker.getObject(%n);
        %space.dumpValues();
        %fpn = %space.get("floorPlan");
        if ((%fpn $= %floorplanName)) {
            return 1;
        }
        %n = (1.0 + %n);
    }
    return 0;
};
function getOwnerSpaceInfoTracker(%ownerName) {
    safeEnsureScriptObject("StringMap", "gOwnerSpaceInfoTrackers", 0);
    %tracker = %ownerName.get();
    gOwnerSpaceInfoTrackers;
    if (!(isObject(%tracker))) {
        %tracker = safeNewScriptObject("SimSet", "", 0);
        %ownerName.put(%tracker);
        ownerName = gOwnerSpaceInfoTrackers @ %ownerName @ %tracker;
    }
    return %tracker;
};
function onDoneOrErrorCallback_GetCustomSpaceInfo(%request, %result) {
    log("network", "debug", getScopeName() @ " " @ "- url =" @ " " @ %request.getURL());
    %tracker = tracker;
    %request;
    if (!(isObject(tracker))) {
        error(getScopeName() @ " " @ "- no tracker. should be impossible.");
        return %request;
    }
    if (!(%request.checkSuccess())) {
        %cb = completionCallback;
        %tracker;
        if (!(%cb $= "")) {
            call(%cb, %request);
        }
        return;
    }
    %tracker.deleteMembers();
    %listBase = "space";
    %numSpaces = %request.getResult(%listBase @ "Count");
    %n = 0;
    if ((%numSpaces < %n)) {
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
        %request.copyListValuesIntoMap(%map, %listItemNameBase, %fields);
        %fields = "";
        %m = 0;
        if ((%map.get("banCount") < %m)) {
            %fields = %fields @ "\t" @ "ban" @ %m;
            %m = (1.0 + %m);
        }
        %request.copyListValuesIntoMap(%map, %listItemNameBase, %fields);
        URI = %map @ vurlClearResolution(URI) @ %map;
        (%map.get("banCount") < %m);
        %tracker.add(%map);
        %n = (1.0 + %n);
    }
    if (isObject()) {
        update();
    }
    if (!(%tracker SPC onCompleteFN $= "")) {
        call(onCompleteFN, %tracker);
    }
};
function addBuildingInfo(%buildingInfo) {
    safeEnsureScriptObject("SimGroup", "BuildingInfos");
    log("network", "debug", "Adding building info for \"" @ %buildingInfo @ name @ "\" to cache");
    %buildingInfo.add();
    %buildingInfo.bringToFront();
    log("network", "debug", BuildingInfos @ "Cache now contains " @ BuildingInfos @ getCount() @ " items.");
};
function findBuildingInfo(%buildingName) {
    safeEnsureScriptObject("SimGroup", "BuildingInfos");
    %count = getCount();
    BuildingInfos;
    log("network", "debug", "Searching for \"" @ %buildingName @ "\" in cache (" @ %count @ " items)");
    %idx = 0;
    if ((%count < %idx)) {
        %buildingInfo = %idx.getObject();
        BuildingInfos;
        if ((%buildingInfo == stricmp(name, %buildingName))) {
            log("network", "debug", 0.0 @ "Found item at index " @ %idx);
            %buildingInfo.bringToFront();
            return %buildingInfo;
        }
        %idx = (1.0 + %idx);
    }
    log("network", "debug", "Item not found in cache");
    return 0;
};
function clearBuildingInfo(%buildingInfo) {
    safeEnsureScriptObject("SimGroup", "BuildingInfos");
    %buildingInfo.remove();
    %idx = 0;
    BuildingInfos;
    if ((floorPlanCount < %idx)) {
        floorplan.delete();
        %idx = (1.0 + %idx);
        %buildingInfo @ %idx @ %buildingInfo;
    }
    %buildingInfo.schedule(0, "delete");
};
function findFloorPlan(%buildingInfo, %floorplanName) {
    %idx = 0;
    if ((floorPlanCount < %idx)) {
        %floorplan = floorplan;
        %buildingInfo @ %idx @ %buildingInfo;
        if ((stricmp(%floorplanName, name) == 0.0)) {
            return %floorplan;
        }
        %idx = (1.0 + %idx);
    }
    return 0;
};
function GetBuildingInfoRequest(%tracker) {
    if ($StandAlone) {
        echo("we are in standalone, faking this");
        if (!(%tracker SPC callbackFailure $= "")) {
            %command = %tracker @ callbackFailure @ "( %tracker.buildingName, \"we are in standalone mode so failing this\");";
            log("network", "debug", "About to eval callback: \"" @ %command @ "\"");
            eval(%command);
        }
        return;
    }
    className = ManagerRequest @ new ""() @ "GetBuildingInfo";
    0;
    %request = ;
    if (isObject()) {
        %request.add();
    }
    %url = MissionCleanup @ MissionCleanup @ $Net::ClientServiceURL @ "/GetBuildingInfo" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token);
    %url = %tracker @ urlEncode(buildingName);
    %url @ "&building=";
    tracker = %tracker @ %request;
    log("network", "debug", "GetBuildingInfoRequest: " @ %url);
    %request.setURL(%url);
    %request.start();
};
function GetBuildingInfo::onDone(%this) {
    echo(getScopeName());
    %status = findRequestStatus(%this);
    log("network", "debug", "GetBuildingInfo status: " @ %status);
    if ((%status $= "fail")) {
        echo(getScopeName() @ "->failed");
        %statusMsg = %this.getValue("statusMsg");
        log("network", "debug", "GetBuildingInfo failed due to: " @ %statusMsg);
        if (!(tracker SPC callbackFailure $= "")) {
            %command = tracker @ callbackFailure @ "( %this.tracker.buildingName, %statusMsg);";
            %this;
            log("network", "debug", %this @ "About to eval callback: \"" @ %command @ "\"");
            eval(%command);
        }
    }
    %buildingInfo = new ""();
    SimObject;
    if (isObject()) {
        %buildingInfo.add();
    }
    name = MissionCleanup @ %this.getValue("name") @ %buildingInfo;
    MissionCleanup;
    city = 0 @ %this.getValue("city") @ %buildingInfo;
    description = urlDecode(%this.getValue("description")) @ %buildingInfo;
    floorPlanCount = %this.getValue("floorPlansCount") @ %buildingInfo;
    %idx = 0;
    if ((floorPlanCount < %idx)) {
        %floorplan = new ""();
        SimObject;
        if (isObject()) {
            %floorplan.add();
        }
        name = 0 @ %this.getValue(MissionCleanup @ MissionCleanup @ "floorPlans" @ %idx @ ".name") @ %floorplan;
        %buildingInfo;
        description = %this.getValue("floorPlans" @ %idx @ ".description") @ %floorplan;
        capacity = %this.getValue("floorPlans" @ %idx @ ".capacity") @ %floorplan;
        minLevel = %this.getValue("floorPlans" @ %idx @ ".minLevel") @ %floorplan;
        priceVBux = %this.getValue("floorPlans" @ %idx @ ".priceVBux") @ %floorplan;
        priceVPoints = %this.getValue("floorPlans" @ %idx @ ".priceVPoints") @ %floorplan;
        isUpgrade = ( == stricmp(%this.getValue(0.0 @ "floorPlans" @ %idx @ ".upgrade"), "true")) @ %floorplan;
        numAvailable = %this.getValue("floorPlans" @ %idx @ ".numAvailable") @ %floorplan;
        if (isObject(floorplan)) {
            floorplan.delete();
        }
        floorplan = %idx @ %buildingInfo @ %idx @ %buildingInfo @ %floorplan @ %idx @ %buildingInfo;
        %idx = (1.0 + %idx);
    }
    addBuildingInfo(%buildingInfo);
    log("network", "debug", %buildingInfo @ (floorPlanCount < %idx) @ "Got building info for \"" @ %buildingInfo @ name @ "\" with " @ %buildingInfo @ floorPlanCount @ " floor plans");
    if (isObject(tracker)) {
        buildingInfo = %this @ tracker;
        %this @ %buildingInfo;
        doneBuildingInfo = %this @ tracker;
        1;
    }
    log("network", "warn", "%this.tracker is not an object.");
    if (isObject(tracker)) {
        checkDoneBuildingDirectory(tracker);
    }
    %this.schedule(0, "delete");
};
function GetBuildingInfo::onError(%this, %unused, %errMsg) {
    if (!(tracker SPC callbackFailure $= "")) {
        %command = tracker @ callbackFailure @ "( %this.tracker.buildingName, %errMsg);";
        %this;
        log("network", "debug", %this @ "About to eval callback: \"" @ %command @ "\"");
        eval(%command);
    }
    log("network", "debug", "GetBuildingInfo::onError: " @ %errMsg);
    %this.schedule(0, "delete");
};
function doGetSpaceInfo(%tracker) {
    if ($StandAlone) {
        echo("we are in standalone, faking this");
        if (!(%tracker SPC callbackFailure $= "")) {
            %command = %tracker @ callbackFailure @ "( %tracker.buildingName, \"we are in standalone mode so failing this\");";
            log("network", "debug", "About to eval fail callback: \"" @ %command @ "\"");
            eval(%command);
        }
        return;
    }
    className = ManagerRequest @ new ""() @ "GetSpaceInfo";
    0;
    %request = ;
    if (isObject()) {
        %request.add();
    }
    %url = $Net::ClientServiceURL;
    MissionCleanup;
    %url = MissionCleanup @ %url @ "/GetCustomSpaceInfo";
    %url = %url @ "?user=" @ urlEncode($Player::Name);
    %url = %url @ "&token=" @ urlEncode($Token);
    if (!(%tracker SPC buildingName $= "")) {
        %url = %tracker @ urlEncode(buildingName);
        %url @ "&building=";
    }
    if (!(%tracker SPC spaceName $= "")) {
        %url = %tracker @ urlEncode(spaceName);
        %url @ "&space=";
    }
    if (!(%tracker SPC ownerName $= "")) {
        %url = %tracker @ urlEncode(ownerName);
        %url @ "&owner=";
    }
    tracker = %tracker @ %request;
    %request.setURL(%url);
    %request.start();
};
function GetSpaceInfo::onDone(%this) {
    %status = findRequestStatus(%this);
    log("network", "debug", "GetSpaceInfo status: " @ %status);
    if ((%status $= "fail")) {
        %statusMsg = %this.getValue("statusMsg");
        error(getScopeName() @ " " @ "- failed w/" @ " " @ %statusMsg);
        if (!(tracker SPC callbackFailure $= "")) {
            %command = tracker @ callbackFailure @ "( %this.tracker.buildingName, %statusMsg);";
            %this;
            log("network", "debug", %this @ "About to eval callback: \"" @ %command @ "\"");
            eval(%command);
        }
    }
    spaceBuildingName = %this @ tracker;
    %this.getValue("building");
    spaceCount = %this @ tracker;
    %this.getValue("spaceCount");
    spaces = %this @ tracker;
    SimGroup @ new ""();
    %idx = 0;
    0;
    if ((spaceCount < %idx)) {
        %space = new ""();
        SimObject;
        if (isObject()) {
            %space.add();
        }
        access = 0 @ %this.getValue(MissionCleanup @ MissionCleanup @ "space" @ %idx @ ".access") @ %space;
        tracker;
        audioStream = %this.getValue(%this @ "space" @ %idx @ ".audioStream") @ %space;
        description = %this.getValue("space" @ %idx @ ".description") @ %space;
        isFeatured = (%this.getValue("space" @ %idx @ ".featured") $= "true") @ %space;
        floorPlanName = %this.getValue("space" @ %idx @ ".floorPlan") @ %space;
        longDescription = %this.getValue("space" @ %idx @ ".longDescription") @ %space;
        name = %this.getValue("space" @ %idx @ ".name") @ %space;
        occupancy = %this.getValue("space" @ %idx @ ".occupancy") @ %space;
        owner = %this.getValue("space" @ %idx @ ".owner") @ %space;
        password = %this.getValue("space" @ %idx @ ".password") @ %space;
        type = %this.getValue("space" @ %idx @ ".type") @ %space;
        vurl = %this.getValue("space" @ %idx @ ".URI") @ %space;
        videoStream = %this.getValue("space" @ %idx @ ".videoStream") @ %space;
        buildingName = %this.getValue("space" @ %idx @ ".building") @ %space;
        blockedList = "" @ %space;
        %banCount = %this.getValue("space" @ %idx @ ".banCount");
        if ((%banCount $= "")) {
            %banCount = 0;
        }
        %k = 0;
        if ((%banCount < %k)) {
            %blockedUser = %this.getValue("space" @ %idx @ ".ban" @ %k);
            if ((%blockedUser $= "")) {
                warn(getScopeName() @ "->banned user #" @ %k @ " out of " @ %banCount @ ", was NULL!");
            }
            if ((%space >= findField(blockedList, %blockedUser))) {
                warn(0.0 @ getScopeName() @ "->banned user #" @ %k @ " OUT OF " @ %banCount @ ", is a duplicate entry! entry = " @ %blockedUser @ " .");
            }
            if (!(%space SPC blockedList $= "")) {
                blockedList = %space @ blockedList @ "\t" @ %blockedUser @ %space;
            }
            blockedList = %blockedUser @ %space;
            %k = (1.0 + %k);
        }
        blockedList = %space @ trim(blockedList) @ %space;
        (%banCount < %k);
        spaces.add(%space);
        %idx = (1.0 + %idx);
        tracker;
    }
    if (isObject(tracker)) {
        doneCSList = %this @ tracker;
        %this @ 1;
        checkDoneBuildingDirectory(tracker);
    }
    %this.schedule(0, "delete");
};
function GetSpaceInfo::onError(%this, %unused, %errMsg) {
    log("network", "debug", "GetSpaceInfo::onError: " @ %errMsg);
    if (!(tracker SPC callbackFailure $= "")) {
        %command = tracker @ callbackFailure @ "( %this.tracker.buildingName, %errMsg);";
        %this;
        log("network", "debug", %this @ "About to eval callback: \"" @ %command @ "\"");
        eval(%command);
    }
    %this.schedule(0, "delete");
};
function purchaseApartmentRequest(%space, %useBux, %unused, %callback, %callbackFail) {
    className = ManagerRequest @ new ""() @ "PurchaseSpaceRequest";
    0;
    %request = ;
    if (isObject()) {
        %request.add();
    }
    callback = MissionCleanup @ %callback @ %request;
    MissionCleanup;
    callbackFail = %callbackFail @ %request;
    %url = $Net::ClientServiceURL @ "/PurchaseCustomSpace?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "building=" @ %space @ buildingInfo @ urlEncode(name) @ "&" @ "floorPlan=" @ %space @ urlEncode(floorPlanName) @ "&" @ "payWith=" @ %useBux ? "vbux" : "vpoints";
    log("network", "debug", "PurchaseSpace: " @ %url);
    %request.setURL(%url);
    %request.start();
};
function PurchaseSpaceRequest::onDone(%this) {
    echo(getScopeName());
    %status = findRequestStatus(%this);
    log("network", "info", "PurchaseSpaceRequest status: " @ %status);
    if ((%status $= "success")) {
        %name = %this.getValue("name");
        %building = %this.getValue("building");
        %vurl = %this.getValue("vurl");
        if ((%vurl $= "")) {
            warn("Server not returning space VURL in .vurl parameter.");
            %vurl = %this.getValue("URI");
        }
        if (!(%this SPC callback $= "")) {
            %command = %this @ callback @ "( %building, %name, %vurl );";
            log("network", "debug", "About to eval callback: " @ %command);
            eval(%command);
        }
    }
    %result = %this.getValue("items0.validationResults");
    if (!(%this SPC callbackFail $= "")) {
        if (!(%result $= "")) {
            %command = %this @ callbackFail @ "( %result );";
        }
        %command = %this @ callbackFail @ "( \"error\" );";
        eval(%command);
    }
    %this.schedule(0, "delete");
};
function PurchaseSpaceRequest::onError(%this, %unused, %errMsg) {
    log("network", "debug", "PurchaseSpaceRequest::onError: " @ %errMsg);
    if (!(%this SPC callbackFail $= "")) {
        %command = %this @ callbackFail @ "( %status );";
        eval(%command);
    }
    %this.schedule(0, "delete");
};
function getCustomSpacePurchaseInfo(%space, %callback) {
    className = ManagerRequest @ new ""() @ "CustomSpacePurchaseInfo";
    0;
    %request = ;
    if (isObject()) {
        %request.add();
    }
    callback = MissionCleanup @ %callback @ %request;
    MissionCleanup;
    space = %space @ %request;
    %url = %space @ urlEncode(floorPlanName);
    $Net::ClientServiceURL @ "/GetCustomSpacePurchaseInfo?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "building=" @ %space @ buildingInfo @ urlEncode(name) @ "&" @ "floorPlan=";
    log("network", "debug", "PurchaseSpace: " @ %url);
    %request.setURL(%url);
    %request.start();
};
function CustomSpacePurchaseInfo::onDone(%this) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        %statusMsg = %this.getValue("statusMsg");
        handleSystemMessage("msgInfoMessage", "" @ %this @ blockText @ " unsuccessful.");
        return;
    }
    %floorplan = floorplan;
    space;
    sku = %this @ %this.getValue("sku") @ %floorplan;
    minLevel = %this.getValue("minLevel") @ %floorplan;
    quantity = %this.getValue("quantity") @ %floorplan;
    priceVBux = %this.getValue("priceVBux") @ %floorplan;
    priceVPoints = %this.getValue("priceVPoints") @ %floorplan;
    tradeInValueVBux = %this.getValue("tradeInCreditVBux") @ %floorplan;
    tradeInValueVPoints = %this.getValue("tradeInCreditVPoints") @ %floorplan;
    expectedError = %this.getValue("expectedError") @ %floorplan;
    if ((%floorplan > tradeInValueVBux)) {
    }
    isUpgrade = 0.0 @ (%floorplan > tradeInValueVPoints) @ %floorplan;
    0.0;
    if (!(%this SPC callback $= "")) {
        %cmd = %this @ callback @ "(" @ %this @ space @ ");";
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
    -(1.0).SelectNuggetID();
    if ((CSFurnitureMover SPC %videoStream $= "")) {
    }
    %envMgrVideoStr = %videoStream;
    "no-video";
    CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), "", "", "", %audioStream, %envMgrVideoStr);
};
function csCopyLayoutFromTo(%from, %to) {
    if ((%to == %from)) {
        error(getScopeName() @ "->being asked to copy current layout into itself! returning.");
        return;
    }
    commandToServer('CSCopyLayoutFromTo', CustomSpaceClient::GetSpaceImIn(), %from, %to);
};
function clientCmdCSGotLayoutVitals(%infoStr) {
    %layoutNum = getField(%infoStr, 0);
    if ((CSLayoutSelector == copyTarget)) {
        %infoStr.gotCopyTargetInfo();
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
    %mediafavorites = $Player::Name.getProperty("mediafavoritelist", $CSSpaceInfo @ videoStream);
    $CSSpaceInfo @ audioStream @ "\t";
    echo(gUserPropMgrClient @ "vside://radio/" @ "csLoadMediaFavorites - \"" @ %mediafavorites @ "\"");
    %mediafavorites.setMediaFavorites();
};
function csSaveMediaFavorites() {
    %mediafavorites = getMediaFavorites();
    CSMediaDisplay;
    $Player::Name.setProperty("mediafavoritelist", %mediafavorites);
};
function csRequestHotMedia() {
    className = ManagerRequest @ new ""() @ "GetUrlRatingListRequest";
    0;
    %request = ;
    if (isObject()) {
        %request.add();
    }
    %url = MissionCleanup @ MissionCleanup @ $Net::ClientServiceURL @ "/GetUrlRatingList?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "type=VIDEO" @ "&" @ "order=BY_SHOWS" @ "&" @ "first=0" @ "&" @ "count=" @ $CSMediaDisplay::DefaultFavoriteCount;
    log("network", "debug", "requesting 'hot' media");
    %request.setURL(%url);
    %request.start();
};
function GetUrlRatingListRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        %this.onError(%this, 0, %status);
        return;
    }
    %mediaList = "";
    %count = %this.getValue("mediaCount");
    %idx = 0;
    if ((%count < %idx)) {
    }
    if (($CSMediaDisplay::DefaultFavoriteCount < %idx)) {
        %mediaURL = urlDecode(%this.getValue("media" @ %idx @ ".url"));
        %viewCount = %this.getValue("media" @ %idx @ ".viewCount");
        %showCount = %this.getValue("media" @ %idx @ ".showCount");
        if ((0.0 > %showCount)) {
            %mediaInfo = %mediaURL @ " " @ %viewCount @ " " @ %showCount;
            if ((%mediaList $= "")) {
                %mediaList = %mediaInfo;
            }
            %mediaList = %mediaList @ "\t" @ %mediaInfo;
        }
        %idx = (1.0 + %idx);
        if ((%count < %idx)) {
        }
    }
    %mediaList.setMediaHotlist();
    %this.schedule(0, "delete");
};
function GetUrlRatingListRequest::onError(%this, %unused, %errMsg) {
    log("network", "warn", "GetUrlRatingRequest::onError: " @ %errMsg);
    %this.schedule(0, "delete");
};
function csRequestMediaStatistics(%mediaURL) {
    className = ManagerRequest @ new ""() @ "GetUrlRatingRequest";
    0;
    %request = ;
    if (isObject()) {
        %request.add();
    }
    %url = MissionCleanup @ MissionCleanup @ $Net::ClientServiceURL @ "/GetUrlRating?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "url=" @ urlEncode(%mediaURL);
    log("network", "debug", "UrlRating: request rating for: " @ %mediaURL);
    mediaurl = %mediaURL @ %request;
    %request.setURL(%url);
    %request.start();
};
function GetUrlRatingRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        %this.onError(%this, 0, %status);
        return;
    }
    %mediaURL = %this.getValue("url");
    %mediaviews = %this.getValue("viewCount");
    %mediaplays = %this.getValue("showCount");
    %mediaURL.setMediaStatistics(%mediaviews, %mediaplays);
    %this.schedule(0, "delete");
};
function GetUrlRatingRequest::onError(%this, %unused, %errMsg) {
    log("network", "warn", "GetUrlRatingRequest::onError: " @ %errMsg);
    mediaurl.clearMediaStatistics();
    %this.schedule(0, "delete");
};
function csRecordMediaShow(%mediaURL, %type) {
    className = ManagerRequest @ new ""() @ "RecordUrlShowRequest";
    0;
    %request = ;
    if (isObject()) {
        %request.add();
    }
    %url = MissionCleanup @ MissionCleanup @ $Net::ClientServiceURL @ "/RecordUrlShow?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "url=" @ urlEncode(%mediaURL) @ "&" @ "type=" @ %type;
    log("network", "debug", "ShowRequest: request rating for: " @ %mediaURL);
    mediaurl = %mediaURL @ %request;
    %request.setURL(%url);
    %request.start();
};
function RecordUrlShowRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        %this.onError(%this, 0, %status);
        return;
    }
    %this.schedule(0, "delete");
};
function RecordUrlShowRequest::onError(%this, %unused, %errMsg) {
    log("network", "warn", "RecordUrlShowRequest::onError: " @ %errMsg);
    %this.schedule(0, "delete");
};
function csRecordMediaView(%mediaURL, %type) {
    className = ManagerRequest @ new ""() @ "RecordUrlViewRequest";
    0;
    %request = ;
    if (isObject()) {
        %request.add();
    }
    %url = MissionCleanup @ MissionCleanup @ $Net::ClientServiceURL @ "/RecordUrlView?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "url=" @ urlEncode(%mediaURL) @ "&" @ "type=" @ %type;
    log("network", "debug", "ViewRequest: request rating for: " @ %mediaURL);
    mediaurl = %mediaURL @ %request;
    %request.setURL(%url);
    %request.start();
};
function RecordUrlViewRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        %this.onError(%this, 0, %status);
        return;
    }
    %this.schedule(0, "delete");
};
function RecordUrlViewRequest::onError(%this, %unused, %errMsg) {
    log("network", "warn", "RecordUrlViewRequest::onError: " @ %errMsg);
    %this.schedule(0, "delete");
};
