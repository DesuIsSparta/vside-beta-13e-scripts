$CSCurrentSpaceName = "";
$CSSpaceName = "";
$CSBuildingName = 0;
$CSSpaceInfo = 0;
$CSBuildingInfo = 0;
function CustomSpaceClient::SetCurrentSpaceName(%spaceName)
{
    $CSCurrentSpaceName = %spaceName;
    return ;
}
function CustomSpaceClient::GetCurrentSpaceName()
{
    return $CSCurrentSpaceName;
}
function CustomSpaceClient::GetSpaceImIn()
{
    return $CSSpaceName;
}
function CustomSpaceClient::SetSpaceImIn(%buildingName, %spaceName)
{
    if ($CSSpaceName $= %spaceName)
    {
        if (!($CSSpaceName $= ""))
        {
            error(getScopeName() SPC "you are already in this space:" SPC %spaceName);
        }
        return ;
    }
    $CSBuildingName = %buildingName;
    $CSSpaceName = %spaceName;
    $CSBlockedList = "";
    if ((%buildingName $= "") && (%spaceName $= ""))
    {
        return ;
    }
    if ($StandAlone)
    {
        CustomSpaceClient::SetUpOwnership(1);
        return ;
    }
    getBuildingSpaceInfo($CSBuildingName, $CSSpaceName, "GotCustomSpaceInfo", "");
    return ;
}
function CustomSpaceClient::OnClientDisconnect()
{
    CustomSpaceClient::SetupClientAsNotInSpace();
    return ;
}
function CustomSpaceClient::SetupClientAsNotInSpace()
{
    if (isObject(CSFurnitureMover))
    {
        CSFurnitureMover.close();
    }
    if (isObject(CSInventoryBrowserWindow))
    {
        CSInventoryBrowserWindow.close();
    }
    if (isObject(CSShoppingBrowserWindow))
    {
        CSShoppingBrowserWindow.close();
    }
    if (isObject(CSMediaDisplay))
    {
        CSMediaDisplay.close();
    }
    if (isObject(CSRulesAndDescWindow))
    {
        CSRulesAndDescWindow.close();
    }
    if (isObject(CSLayoutSelector))
    {
        CSLayoutSelector.close();
    }
    if (isObject(CSPaintingWindow))
    {
        CSPaintingWindow.close();
    }
    if (isObject(CSControlPanel))
    {
        CSControlPanel.close();
    }
    if (HudTabs.getCurrentTab().name $= "private space")
    {
        HudTabs.close();
    }
    HudTabs.hideTabWithName("private space");
    if ($CSSpaceInfo != 0)
    {
        destroySpaceInfo($CSSpaceInfo);
    }
    $CSSpaceInfo = 0;
    $CSBuildingInfo = 0;
    CustomSpaceClient::SetSpaceImIn("", "");
    CustomSpaceClient::SetUpOwnership(0);
    return ;
}
$CS_EditingCustomSpace = 0;
function CustomSpaceClient::startEditingSpace()
{
    if ($CS_EditingCustomSpace)
    {
        return ;
    }
    csRequestToEditSpace();
    $CS_EditingCustomSpace = 1;
    onCustomSpaceEditorEnable();
    return ;
}
function CustomSpaceClient::stopEditingSpace()
{
    if (!$CS_EditingCustomSpace)
    {
        return ;
    }
    csDoneEditingSpace();
    $CS_EditingCustomSpace = 0;
    onCustomSpaceEditorDisable();
    return ;
}
function CustomSpaceClient::checkEditingSpace()
{
    if (numCSPanelsOpen() > 0)
    {
        CustomSpaceClient::startEditingSpace();
    }
    else
    {
        CustomSpaceClient::stopEditingSpace();
    }
    return ;
}
function GotCustomSpaceInfo(%buildingInfo, %spaceGroup)
{
    echo("received custom space info");
    if (%spaceGroup.getCount() != 1)
    {
        log("network", "error", "GotCustomSpaceInfo returned a strange number of spaces (" @ %spaceGroup.getCount() @ ")");
    }
    if (($CSSpaceInfo != 0) && isObject($CSSpaceInfo))
    {
        destroySpaceInfo($CSSpaceInfo);
    }
    $CSBuildingInfo = %buildingInfo;
    $CSSpaceInfo = %spaceGroup.getObject(0);
    %spaceGroup.remove($CSSpaceInfo);
    %spaceGroup.delete();
    %isOwner = 0;
    if (isObject($player))
    {
        %isOwner = stricmp($CSSpaceInfo.owner, $player.getShapeName()) == 0;
    }
    else
    {
        error("$Player is not an object. Setting space I\'m in before I\'m there...");
    }
    CustomSpaceClient::SetUpOwnership(%isOwner);
    CSRulesAndDescWindow.updateSettings($CSSpaceInfo.access, $CSSpaceInfo.password, $CSSpaceInfo.description);
    if (stricmp($CSSpaceInfo.type, "model") == 0)
    {
        CSControlPanel.open();
        CSSpaceModelAptText.update();
        CSControlPanelTabs.selectTabWithName("MODEL_APT");
    }
    CustomSpacesClient::setMap2DText();
    CustomSpacesClient::InitializeVideoRequest();
    return ;
}
function CustomSpacesClient::setMap2DText()
{
    geLocalMapContainer.setMap2DForCustomSpacesMode("<color:ffffff><just:center><b>" @ TryFixBadWords($CSSpaceInfo.description), "<color:ffffff><tab:15,60>\tOwner:\t" @ $CSSpaceInfo.type $= "CELEBSPACE" ? $CSSpaceInfo : $CSSpaceInfo @ "\n\tBuilding:\t" @ Buildings::GetDescription($CSBuildingName) $= "" ? $CSBuildingName : Buildings::GetDescription($CSBuildingName) @ "\n\tCity:\t" @ getContiguousSpaceFullName(Buildings::GetContiguousSpace($CSBuildingName)));
    return ;
}
$CSSpaceOwner = 0;
function CustomSpaceClient::SetUpOwnership(%isOwner)
{
    %wasOwner = $CSSpaceOwner;
    log("network", "debug", "setting space ownership to " @ %isOwner);
    if (%isOwner == 0)
    {
        if (isObject(PrivSpaceHud))
        {
            PrivSpaceHud.hideOP();
            PrivSpaceHud.disableOPlink();
        }
        if (isObject(CSControlPanel))
        {
            if (($CSSpaceInfo != 0) && (stricmp($CSSpaceInfo.type, "model") == 0))
            {
                CSControlPanel.open();
                CSControlPanelTabs.selectTabWithName("MODEL_APT");
            }
            else
            {
                CSControlPanel.close();
            }
        }
        if (isObject(MusicHud))
        {
            MusicHud.setChangeStationAllowed(0);
        }
        ButtonBar.hideButton(PrivateSpacePopupMenuButton);
        $CSSpaceOwner = 0;
    }
    else
    {
        ButtonBar.showButton(PrivateSpacePopupMenuButton);
        %showSpaceOwnerTip = gUserPropMgrClient.getProperty($Player::Name, "ShowOwnerTip", 1);
        if (%showSpaceOwnerTip)
        {
            gUserPropMgrClient.setProperty($Player::Name, "ShowOwnerTip", 0);
            userTips::showNow("SpaceOwner");
        }
        $CSSpaceOwner = 1;
        $CSBlockedList = $CSSpaceInfo.blockedList;
        Music::createGetMusicStreamsRequest();
        MusicHud.setChangeStationAllowed(1);
        csLoadMediaFavorites();
        csRequestHotMedia();
        getOwnedFurniture();
    }
    return ;
}
function CustomSpaceClient::isOwner()
{
    return $CSSpaceOwner;
}
function CustomSpaceClient::placeSkuInWorld(%sku, %position, %orientation)
{
    if (%sku <= 0)
    {
        error(getScopeName() SPC "No sku selected");
        return ;
    }
    if (!isDefined("%position"))
    {
        %position = "";
    }
    if (!isDefined("%orientation"))
    {
        %orientation = "";
    }
    if (numUsingFurnitureSku(%sku) >= numOwnedFurnitureSku(%sku))
    {
        return ;
    }
    if (numUsingFurnitureAll() >= $CSMaximumSlots)
    {
        %title = "Sorry, Can\'t Do That";
        %body = "This space can only have" SPC $CSMaximumSlots SPC "items in it at a time.  Put something away to make more room.";
        MessageBoxOK(%title, %body, "");
        return ;
    }
    if (!useAnotherFurnitureSku(%sku))
    {
        return ;
    }
    $CSSelectedSku = %sku;
    if (!((%position $= "")) && !((%orientation $= "")))
    {
        commandToServer('CreateInventoryBySkuAt', CustomSpaceClient::GetSpaceImIn(), %sku, %position, %orientation);
    }
    else
    {
        commandToServer('CreateInventoryBySku', CustomSpaceClient::GetSpaceImIn(), %sku);
    }
    CSInventoryBrowser.update();
    CSShoppingBrowser.update();
    setIdle(0);
    return ;
}
function CustomSpacesClient::InitializeVideoRequest()
{
    echo("CSClientRequestVideoInit");
    commandToServer('CSClientRequestVideoInit', CustomSpaceClient::GetSpaceImIn());
    return ;
}
function csRequestToEditSpace()
{
    commandToServer('CSRequestToEdit', CustomSpaceClient::GetSpaceImIn());
    return ;
}
function csDoneEditingSpace()
{
    if (!(CustomSpaceClient::GetSpaceImIn() $= ""))
    {
        commandToServer('CSDoneEditing', CustomSpaceClient::GetSpaceImIn());
    }
    return ;
}
function clientCmdCSRequestToEditAccepted(%spaceName, %numberOfSlots)
{
    if (!(CustomSpaceClient::GetSpaceImIn() $= %spaceName))
    {
        error(getScopeName() SPC "how can you accept to edit a space that you are not in?");
        return ;
    }
    CSFurnitureMover.InitForSpace(%spaceName, %numberOfSlots);
    return ;
}
function clientCmdCSOnUnownedInventoryTimeOut(%referenceID)
{
    if (%referenceID == $CSSelectedID)
    {
        CSFurnitureMover.SelectNuggetID(-1);
        $CSInstaTestDrive = 0;
    }
    getNuggetGhostList("CSFurnitureMover::refreshGhostList");
    return ;
}
function clientCmdCSRequestToEditDenied(%spaceName, %msg)
{
    MessageBoxOK("Sorry", "your request to edit this space was denied" NL %msg, "");
    return ;
}
function customSpace::SetMusicStreamID(%streamID, %displayId)
{
    if (CustomSpaceClient::GetSpaceImIn() $= "")
    {
        return ;
    }
    if (!isDefined("%displayID"))
    {
        %displayId = "";
    }
    commandToServer('CSSetMusicStream', CustomSpaceClient::GetSpaceImIn(), %streamID, %displayId);
    return ;
}
function customSpace::SetVideoURL(%videoURL)
{
    if (CustomSpaceClient::GetSpaceImIn() $= "")
    {
        return ;
    }
    commandToServer('CSSetVideoURL', CustomSpaceClient::GetSpaceImIn(), %videoURL);
    return ;
}
function clientCmdCS_OnEnterSpace(%spaceName, %musicStreamId, %videoURL, %building, %CurrentLayout)
{
    if (!(CustomSpaceClient::GetCurrentSpaceName() $= ""))
    {
        clientCmdCS_OnLeaveSpace(CustomSpaceClient::GetCurrentSpaceName());
    }
    CSControlPanel.close();
    CustomSpaceClient::SetCurrentSpaceName(%spaceName);
    CSMediaDisplay.Initialize();
    %analyticSpace = strreplace(%spaceName, ".", " ");
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/apartment/" @ getWord(%analyticSpace, 0) @ "/enter");
    late_OnEnterSpace(%spaceName, %musicStreamId, %videoURL, %building, %CurrentLayout);
    return ;
}
function late_OnEnterSpace(%spaceName, %musicStreamId, %videoURL, %building, %CurrentLayout)
{
    if (!isObject($player))
    {
        schedule(500, 0, late_OnEnterSpace, %spaceName, %musicStreamId, %videoURL, %building, %CurrentLayout);
        return ;
    }
    CustomSpaceClient::SetSpaceImIn(%building, %spaceName);
    if (!(%musicStreamId $= ""))
    {
        CSMediaDisplay.syncPlayingAudioStream(%musicStreamId);
    }
    else
    {
        if (!(%videoURL $= "no-video"))
        {
            CSMediaDisplay.syncPlayingMediaStream(%videoURL);
        }
    }
    CSLayoutSelector.updateSettings($CSLayoutSelector::NumLayouts, %CurrentLayout);
    CSInventoryBrowserWindow.Initialize();
    CSShoppingBrowserWindow.Initialize();
    return ;
}
function clientCmdCS_OnLeaveSpace(%spaceName)
{
    if (CustomSpaceClient::GetCurrentSpaceName() $= %spaceName)
    {
        %analyticSpace = strreplace(%spaceName, ".", " ");
        %analytic = getAnalytic();
        %analytic.trackPageView("/client/apartment/" @ getWord(%analyticSpace, 0) @ "/exit");
        CustomSpaceClient::SetupClientAsNotInSpace();
        if (!(($CSSpaceInfo $= "")) && isObject($CSSpaceInfo))
        {
            destroySpaceInfo($CSSpaceInfo);
            $CSSpaceInfo = 0;
        }
        ButtonBar.hideButton(PrivateSpacePopupMenuButton);
        CustomSpaceClient::SetCurrentSpaceName("");
    }
    return ;
}
function destroySpaceInfo(%spaceInfo)
{
    if (!isObject(%spaceInfo))
    {
        return ;
    }
    if (!(%spaceInfo.videoplayer $= ""))
    {
        %spaceInfo.videoplayer.unloadVideoRenderer();
    }
    %spaceInfo.delete();
    return ;
}
$CSNewInventoryGhostRefreshEvent = 0;
function clientCmdCS_OnInventoryCreated(%sku, %referenceName, %isOwned, %freeRotate)
{
    CSFurnitureMover.SelectNuggetID(%referenceName);
    $CSSelectedIsOwned = %isOwned;
    $CSSelectedFreeRotate = %freeRotate;
    if (GrowingPlantClient::isPlant(%sku))
    {
        GrowingPlantClient::onPlantCreated(%referenceName);
    }
    if (isEventPending($CSNewInventoryGhostRefreshEvent))
    {
        cancel($CSNewInventoryGhostRefreshEvent);
    }
    $CSNewInventoryGhostRefreshEvent = schedule(500, 0, "getNuggetGhostList", "CSFurnitureMover::refreshGhostList");
    return ;
}
function clientCmdCS_OnInventoryCreationFailed(%sku, %isOwned)
{
    refreshActiveFurniture();
    CSFurnitureMover.SelectNuggetID(-1);
    return ;
}
function clientCmdCS_OnEnterEntryPortal(%buildingName)
{
    Canvas.forceRightMouseUp();
    CustomSpacesSelector.open(%buildingName);
    return ;
}
function CustomSpaceClient::CheckBlockUserFromSpace(%playerName, %unblock)
{
    %blockText = (%unblock $= "") && (%unblock == 0) ? "block" : "unblock";
    if (CustomSpaceClient::GetSpaceImIn() $= "")
    {
        handleSystemMessage("msgInfoMessage", "Sorry, you must be in a space to " @ %blockText @ " users from it.");
        return 0;
    }
    if (%playerName $= "")
    {
        handleSystemMessage("msgInfoMessage", "You didn\'t specify anyone to " @ %blockText);
        return 0;
    }
    if (!$player.rolesPermissionCheckNoWarn("manageUsers") && !CustomSpaceClient::isOwner())
    {
        handleSystemMessage("msgInfoMessage", "You must be the owner of a space to " @ %blockText @ " users from it.");
        return 0;
    }
    return 1;
}
function CustomSpaceClient::setCoHostHood(%playerName, %set, %confirm)
{
    if (!isDefined("%confirm"))
    {
        %confirm = 1;
    }
    if (%set && %confirm)
    {
        %msg = $MsgCat::custSpace["OWNER_ACTION","COHOST-CONFIRM"];
        %msg = strreplace(%msg, "[TARGET]", %playerName);
        MessageBoxYesNo("Make Co-Host", %msg, "CommandToServer(\'setCohostHood\', \"" @ %playerName @ "\", true);", "");
    }
    else
    {
        commandToServer('setCohostHood', %playerName, %set);
    }
    return ;
}
function CustomSpaceClient::toggleCoHostHood(%playerName)
{
    %player = Player::findPlayerInstance(%playerName);
    if (!isObject(%player))
    {
        error(getScopeName() SPC "- could not find player" SPC %playerName SPC getTrace());
        return ;
    }
    %state = %player.isCohost();
    CustomSpaceClient::setCoHostHood(%playerName, !%state, 0);
    return ;
}
function CustomSpaceClient::TryBlockUserFromSpace(%playerName, %unblock)
{
    if (!CustomSpaceClient::CheckBlockUserFromSpace(%playerName, %unblock))
    {
        return ;
    }
    if (%unblock)
    {
        CustomSpaceClient::ReallyTryBlockUserFromSpace(%playerName, %unblock);
        return ;
    }
    MessageBoxYesNo("Block" SPC %playerName SPC "from this space", "This will block" SPC %playerName SPC "from entering this space. You can unblock them later. Do you want to block" SPC %playerName SPC "?", "CustomSpaceClient::ReallyTryBlockUserFromSpace(\"" @ %playerName @ "\", " @ %unblock @ ");", "");
    return ;
}
function CustomSpaceClient::ReallyTryBlockUserFromSpace(%playerName, %unblock)
{
    if (!CustomSpaceClient::CheckBlockUserFromSpace(%playerName, %unblock))
    {
        return ;
    }
    %blockText = (%unblock $= "") && (%unblock == 0) ? "block" : "unblock";
    %space = CustomSpaceClient::GetSpaceImIn();
    %request = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %request.blockedPlayer = %playerName;
    %request.blockText = %blockText;
    %url = $Net::ClientServiceURL @ "/BanFromSpace" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token) @ "&space=" @ urlEncode(%space) @ "&userToBan=" @ urlEncode(%playerName);
    if ((%unblock == 1) && !((%unblock $= "")))
    {
        %url = %url @ "&unban=true";
    }
    echo("BanFromSpaceRequest: " @ %url);
    log("network", "debug", "BanFromSpaceRequest: " @ %url);
    %request.setURL(%url);
    %request.start();
    return ;
}
function BanFromSpaceRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    if (!(%status $= "success"))
    {
        handleSystemMessage("msgInfoMessage", "" @ %this.blockText @ " unsuccessful.");
    }
    else
    {
        handleSystemMessage("msgInfoMessage", "" @ getPlayerMarkup(%this.blockedPlayer, "", 1) @ " was " @ %this.blockText @ "ed from this space.");
        if (%this.blockText $= "block")
        {
            if (!($CSBlockedList $= ""))
            {
                $CSBlockedList = $CSBlockedList TAB %this.blockedPlayer;
            }
            else
            {
                $CSBlockedList = %this.blockedPlayer;
            }
        }
        else
        {
            $CSBlockedList = removeField($CSBlockedList, findField($CSBlockedList, %this.blockedPlayer));
        }
    }
    return ;
}
function CustomSpaceClient::TryBootAllUsersFromSpace(%space)
{
    if (!$player.rolesPermissionCheckNoWarn("manageUsersBasic") && !CustomSpaceClient::isOwner())
    {
        return ;
    }
    if ((%space $= "") && (CustomSpaceClient::GetSpaceImIn() $= ""))
    {
        handleSystemMessage("msgInfoMessage", "You must either specify a space or be in one to boot all users.");
        return ;
    }
    else
    {
        if (%space $= "")
        {
            %space = CustomSpaceClient::GetSpaceImIn();
        }
    }
    %request = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %request.spaceName = %space;
    %url = $Net::ClientServiceURL @ "/BootAllFromSpace" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token) @ "&space=" @ urlEncode(%space);
    log("network", "debug", "BootAllFromSpaceRequest: " @ %url);
    %request.setURL(%url);
    %request.start();
    return ;
}
function BootAllFromSpaceRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    if (!(%status $= "success"))
    {
        handleSystemMessage("msgInfoMessage", "Couldn\'t boot all from " @ %this.spaceName @ ".");
    }
    else
    {
        handleSystemMessage("msgInfoMessage", "Booted everyone from " @ %this.spaceName @ ".");
    }
    return ;
}
function CustomSpaceClient::doOwnerAction(%action, %target)
{
    commandToServer('OwnerAction', %action, %target);
    return ;
}
function teleportToAdjacentSpace(%next)
{
    if ($player.getControlObject() == $player.client.Camera)
    {
        if (%next)
        {
            commandToServer('GoToNextSpaceBasedOnCamera');
        }
        else
        {
            commandToServer('GoToPrevSpaceBasedOnCamera');
        }
        return ;
    }
    if (%next)
    {
        commandToServer('GoToNextSpaceBasedOnLogicalName', CustomSpaceClient::GetSpaceImIn());
    }
    else
    {
        commandToServer('GoToPrevSpaceBasedOnLogicalName', CustomSpaceClient::GetSpaceImIn());
    }
    return ;
}
function teleportToSpaceNumber(%number)
{
    commandToServer('GoToSpaceNumber', %number);
    return ;
}
function getBuildingDirectory(%buildingName, %callbackFn, %callbackFail)
{
    %BuildingDirRequest = new SimObject();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%BuildingDirRequest);
    }
    %BuildingDirRequest.callback = %callbackFn;
    %BuildingDirRequest.callbackFailure = %callbackFail;
    %BuildingDirRequest.buildingName = %buildingName;
    %BuildingDirRequest.spaceName = "";
    %BuildingDirRequest.doneBuildingInfo = 0;
    %BuildingDirRequest.doneCSList = 0;
    doCheckForBuildingInfo(%BuildingDirRequest, 0);
    doGetSpaceInfo(%BuildingDirRequest);
    return ;
}
function getBuildingSpaceInfo(%buildingName, %spaceName, %callbackFn, %callbackFail)
{
    %BuildingDirRequest = new SimObject();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%BuildingDirRequest);
    }
    %BuildingDirRequest.callback = %callbackFn;
    %BuildingDirRequest.callbackFailure = %callbackFail;
    %BuildingDirRequest.buildingName = %buildingName;
    %BuildingDirRequest.spaceName = %spaceName;
    %BuildingDirRequest.doneBuildingInfo = 0;
    %BuildingDirRequest.doneCSList = 0;
    doCheckForBuildingInfo(%BuildingDirRequest, 0);
    doGetSpaceInfo(%BuildingDirRequest);
    return ;
}
function doCheckForBuildingInfo(%BuildingDirRequest, %forceupdate)
{
    safeEnsureScriptObject("SimSet", "BuildingInfos");
    %buildingInfo = findBuildingInfo(%BuildingDirRequest.buildingName);
    if (%buildingInfo != 0)
    {
        if (!%forceupdate)
        {
            %BuildingDirRequest.buildingInfo = %buildingInfo;
            %BuildingDirRequest.doneBuildingInfo = 1;
            checkDoneBuildingDirectory(%BuildingDirRequest);
            return ;
        }
        else
        {
            clearBuildingInfo(%buildingInfo);
        }
    }
    GetBuildingInfoRequest(%BuildingDirRequest);
    return ;
}
function checkDoneBuildingDirectory(%BuildingDirRequest)
{
    if (!isObject(%BuildingDirRequest))
    {
        log("network", "error", "Directory tracker is not an object?!");
        return ;
    }
    log("network", "debug", "Checking directory request done.");
    log("network", "debug", "Building Info    = " @ %BuildingDirRequest.doneBuildingInfo);
    log("network", "debug", "CustomSpace List = " @ %BuildingDirRequest.doneCSList);
    if (%BuildingDirRequest.doneBuildingInfo && %BuildingDirRequest.doneCSList)
    {
        linkSpaces(%BuildingDirRequest.buildingInfo, %BuildingDirRequest.spaces);
        %command = %BuildingDirRequest.callback @ "( %BuildingDirRequest.buildingInfo, %BuildingDirRequest.spaces);";
        log("network", "debug", "About to eval callback: \"" @ %command @ "\"");
        eval(%command);
        %BuildingDirRequest.delete();
    }
    return ;
}
function linkSpaces(%buildingInfo, %spaceGroup)
{
    log("network", "debug", "linking " @ %spaceGroup.getCount() @ " apartments to building info and floor plans");
    %idx = 0;
    while (%idx < %spaceGroup.getCount())
    {
        %space = %spaceGroup.getObject(%idx);
        %space.buildingInfo = %buildingInfo;
        %space.floorplan = findFloorPlan(%buildingInfo, %space.floorPlanName);
        %idx = %idx + 1;
    }
}

function getOwnerSpacesInfo(%ownerName, %onCompleteFN)
{
    %tracker = getOwnerSpaceInfoTracker(%ownerName);
    %tracker.onCompleteFN = %onCompleteFN;
    %request = sendRequest_GetCustomSpaceInfo("", "", %ownerName, "onDoneOrErrorCallback_GetCustomSpaceInfo");
    %request.tracker = %tracker;
    return ;
}
function ownerHasSpaceWithFloorplan(%ownerName, %floorplanName)
{
    %tracker = getOwnerSpaceInfoTracker(%ownerName);
    %count = %tracker.getCount();
    if (%count < 1)
    {
        error(getScopeName() SPC "- user owns no spaces! (tracker not filled yet, probably)" SPC %ownerName SPC getTrace());
        return 0;
    }
    %n = 0;
    while (%n < %count)
    {
        %space = %tracker.getObject(%n);
        %space.dumpValues();
        %fpn = %space.get("floorPlan");
        if (%fpn $= %floorplanName)
        {
            return 1;
        }
        %n = %n + 1;
    }
    return 0;
}
function getOwnerSpaceInfoTracker(%ownerName)
{
    safeEnsureScriptObject("StringMap", "gOwnerSpaceInfoTrackers", 0);
    %tracker = gOwnerSpaceInfoTrackers.get(%ownerName);
    if (!isObject(%tracker))
    {
        %tracker = safeNewScriptObject("SimSet", "", 0);
        gOwnerSpaceInfoTrackers.put(%ownerName, %tracker);
        %tracker.ownerName = %ownerName;
    }
    return %tracker;
}
function onDoneOrErrorCallback_GetCustomSpaceInfo(%request, %result)
{
    log("network", "debug", getScopeName() SPC "- url =" SPC %request.getURL());
    %tracker = %request.tracker;
    if (!isObject(%request.tracker))
    {
        error(getScopeName() SPC "- no tracker. should be impossible.");
        return ;
    }
    if (!%request.checkSuccess())
    {
        %cb = %tracker.completionCallback;
        if (!(%cb $= ""))
        {
            call(%cb, %request);
        }
        return ;
    }
    %tracker.deleteMembers();
    %listBase = "space";
    %numSpaces = %request.getResult(%listBase @ "Count");
    %n = 0;
    while (%n < %numSpaces)
    {
        %map = safeNewScriptObject("StringMap", "", 0);
        %listItemNameBase = %listBase @ %n;
        %fields = "";
        %fields = %fields TAB "URI";
        %fields = %fields TAB "access";
        %fields = %fields TAB "audioStream";
        %fields = %fields TAB "building";
        %fields = %fields TAB "customSpaceId";
        %fields = %fields TAB "description";
        %fields = %fields TAB "featured";
        %fields = %fields TAB "floorPlan";
        %fields = %fields TAB "friendOccupancy";
        %fields = %fields TAB "location.areaName";
        %fields = %fields TAB "location.buildingName";
        %fields = %fields TAB "location.serverName";
        %fields = %fields TAB "longDescription";
        %fields = %fields TAB "name";
        %fields = %fields TAB "occupancy";
        %fields = %fields TAB "owner";
        %fields = %fields TAB "type";
        %fields = %fields TAB "videoStream";
        %fields = %fields TAB "banCount";
        %request.copyListValuesIntoMap(%map, %listItemNameBase, %fields);
        %fields = "";
        %m = 0;
        while (%m < %map.get("banCount"))
        {
            %fields = %fields TAB "ban" @ %m;
            %m = %m + 1;
        }
        %request.copyListValuesIntoMap(%map, %listItemNameBase, %fields);
        %map.URI = vurlClearResolution(%map.URI);
        %tracker.add(%map);
        %n = %n + 1;
    }
    if (isObject(CSSpaceModelAptText))
    {
        CSSpaceModelAptText.update();
    }
    if (!(%tracker.onCompleteFN $= ""))
    {
        call(%tracker.onCompleteFN, %tracker);
    }
    return ;
}
function addBuildingInfo(%buildingInfo)
{
    safeEnsureScriptObject("SimGroup", "BuildingInfos");
    log("network", "debug", "Adding building info for \"" @ %buildingInfo.name @ "\" to cache");
    BuildingInfos.add(%buildingInfo);
    BuildingInfos.bringToFront(%buildingInfo);
    log("network", "debug", "Cache now contains " @ BuildingInfos.getCount() @ " items.");
    return ;
}
function findBuildingInfo(%buildingName)
{
    safeEnsureScriptObject("SimGroup", "BuildingInfos");
    %count = BuildingInfos.getCount();
    log("network", "debug", "Searching for \"" @ %buildingName @ "\" in cache (" @ %count @ " items)");
    %idx = 0;
    while (%idx < %count)
    {
        %buildingInfo = BuildingInfos.getObject(%idx);
        if (stricmp(%buildingInfo.name, %buildingName) == 0)
        {
            log("network", "debug", "Found item at index " @ %idx);
            BuildingInfos.bringToFront(%buildingInfo);
            return %buildingInfo;
        }
        %idx = %idx + 1;
    }
    log("network", "debug", "Item not found in cache");
    return 0;
}
function clearBuildingInfo(%buildingInfo)
{
    safeEnsureScriptObject("SimGroup", "BuildingInfos");
    BuildingInfos.remove(%buildingInfo);
    %idx = 0;
    while (%idx < %buildingInfo.floorPlanCount)
    {
        %buildingInfo.floorplan[%idx].delete();
        %idx = %idx + 1;
    }
    %buildingInfo.schedule(0, "delete");
    return ;
}
function findFloorPlan(%buildingInfo, %floorplanName)
{
    %idx = 0;
    while (%idx < %buildingInfo.floorPlanCount)
    {
        %floorplan = %buildingInfo.floorplan[%idx];
        if (0 == stricmp(%floorplanName, %floorplan.name))
        {
            return %floorplan;
        }
        %idx = %idx + 1;
    }
    return 0;
}
function GetBuildingInfoRequest(%tracker)
{
    if ($StandAlone)
    {
        echo("we are in standalone, faking this");
        if (!(%tracker.callbackFailure $= ""))
        {
            %command = %tracker.callbackFailure @ "( %tracker.buildingName, \"we are in standalone mode so failing this\");";
            log("network", "debug", "About to eval callback: \"" @ %command @ "\"");
            eval(%command);
        }
        return ;
    }
    %request = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %url = $Net::ClientServiceURL @ "/GetBuildingInfo" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token);
    %url = %url @ "&building=" @ urlEncode(%tracker.buildingName);
    %request.tracker = %tracker;
    log("network", "debug", "GetBuildingInfoRequest: " @ %url);
    %request.setURL(%url);
    %request.start();
    return ;
}
function GetBuildingInfo::onDone(%this)
{
    echo(getScopeName());
    %status = findRequestStatus(%this);
    log("network", "debug", "GetBuildingInfo status: " @ %status);
    if (%status $= "fail")
    {
        echo(getScopeName() @ "->failed");
        %statusMsg = %this.getValue("statusMsg");
        log("network", "debug", "GetBuildingInfo failed due to: " @ %statusMsg);
        if (!(%this.tracker.callbackFailure $= ""))
        {
            %command = %this.tracker.callbackFailure @ "( %this.tracker.buildingName, %statusMsg);";
            log("network", "debug", "About to eval callback: \"" @ %command @ "\"");
            eval(%command);
        }
    }
    else
    {
        %buildingInfo = new SimObject();
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(%buildingInfo);
        }
        %buildingInfo.name = %this.getValue("name");
        %buildingInfo.city = %this.getValue("city");
        %buildingInfo.description = urlDecode(%this.getValue("description"));
        %buildingInfo.floorPlanCount = %this.getValue("floorPlansCount");
        %idx = 0;
        while (%idx < %buildingInfo.floorPlanCount)
        {
            %floorplan = new SimObject();
            if (isObject(MissionCleanup))
            {
                MissionCleanup.add(%floorplan);
            }
            %floorplan.name = %this.getValue("floorPlans" @ %idx @ ".name");
            %floorplan.description = %this.getValue("floorPlans" @ %idx @ ".description");
            %floorplan.capacity = %this.getValue("floorPlans" @ %idx @ ".capacity");
            %floorplan.minLevel = %this.getValue("floorPlans" @ %idx @ ".minLevel");
            %floorplan.priceVBux = %this.getValue("floorPlans" @ %idx @ ".priceVBux");
            %floorplan.priceVPoints = %this.getValue("floorPlans" @ %idx @ ".priceVPoints");
            %floorplan.isUpgrade = stricmp(%this.getValue("floorPlans" @ %idx @ ".upgrade"), "true") == 0;
            %floorplan.numAvailable = %this.getValue("floorPlans" @ %idx @ ".numAvailable");
            if (isObject(%buildingInfo.floorplan[%idx]))
            {
                %buildingInfo.floorplan[%idx].delete();
            }
            %buildingInfo.floorplan[%idx] = %floorplan;
            %idx = %idx + 1;
        }
        addBuildingInfo(%buildingInfo);
        log("network", "debug", "Got building info for \"" @ %buildingInfo.name @ "\" with " @ %buildingInfo.floorPlanCount @ " floor plans");
        if (isObject(%this.tracker))
        {
            %this.tracker.buildingInfo = %buildingInfo;
            %this.tracker.doneBuildingInfo = 1;
        }
        else
        {
            log("network", "warn", "%this.tracker is not an object.");
        }
    }
    if (isObject(%this.tracker))
    {
        checkDoneBuildingDirectory(%this.tracker);
    }
    %this.schedule(0, "delete");
    return ;
}
function GetBuildingInfo::onError(%this, %unused, %errMsg)
{
    if (!(%this.tracker.callbackFailure $= ""))
    {
        %command = %this.tracker.callbackFailure @ "( %this.tracker.buildingName, %errMsg);";
        log("network", "debug", "About to eval callback: \"" @ %command @ "\"");
        eval(%command);
    }
    log("network", "debug", "GetBuildingInfo::onError: " @ %errMsg);
    %this.schedule(0, "delete");
    return ;
}
function doGetSpaceInfo(%tracker)
{
    if ($StandAlone)
    {
        echo("we are in standalone, faking this");
        if (!(%tracker.callbackFailure $= ""))
        {
            %command = %tracker.callbackFailure @ "( %tracker.buildingName, \"we are in standalone mode so failing this\");";
            log("network", "debug", "About to eval fail callback: \"" @ %command @ "\"");
            eval(%command);
        }
        return ;
    }
    %request = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %url = $Net::ClientServiceURL;
    %url = %url @ "/GetCustomSpaceInfo";
    %url = %url @ "?user=" @ urlEncode($Player::Name);
    %url = %url @ "&token=" @ urlEncode($Token);
    if (!(%tracker.buildingName $= ""))
    {
        %url = %url @ "&building=" @ urlEncode(%tracker.buildingName);
    }
    if (!(%tracker.spaceName $= ""))
    {
        %url = %url @ "&space=" @ urlEncode(%tracker.spaceName);
    }
    if (!(%tracker.ownerName $= ""))
    {
        %url = %url @ "&owner=" @ urlEncode(%tracker.ownerName);
    }
    %request.tracker = %tracker;
    %request.setURL(%url);
    %request.start();
    return ;
}
function GetSpaceInfo::onDone(%this)
{
    %status = findRequestStatus(%this);
    log("network", "debug", "GetSpaceInfo status: " @ %status);
    if (%status $= "fail")
    {
        %statusMsg = %this.getValue("statusMsg");
        error(getScopeName() SPC "- failed w/" SPC %statusMsg);
        if (!(%this.tracker.callbackFailure $= ""))
        {
            %command = %this.tracker.callbackFailure @ "( %this.tracker.buildingName, %statusMsg);";
            log("network", "debug", "About to eval callback: \"" @ %command @ "\"");
            eval(%command);
        }
    }
    else
    {
        %this.tracker.spaceBuildingName = %this.getValue("building");
        %this.tracker.spaceCount = %this.getValue("spaceCount");
        %this.tracker.spaces = new SimGroup();
        %idx = 0;
        while (%idx < %this.tracker.spaceCount)
        {
            %space = new SimObject();
            if (isObject(MissionCleanup))
            {
                MissionCleanup.add(%space);
            }
            %space.access = %this.getValue("space" @ %idx @ ".access");
            %space.audioStream = %this.getValue("space" @ %idx @ ".audioStream");
            %space.description = %this.getValue("space" @ %idx @ ".description");
            %space.isFeatured = %this.getValue("space" @ %idx @ ".featured") $= "true";
            %space.floorPlanName = %this.getValue("space" @ %idx @ ".floorPlan");
            %space.longDescription = %this.getValue("space" @ %idx @ ".longDescription");
            %space.name = %this.getValue("space" @ %idx @ ".name");
            %space.occupancy = %this.getValue("space" @ %idx @ ".occupancy");
            %space.owner = %this.getValue("space" @ %idx @ ".owner");
            %space.password = %this.getValue("space" @ %idx @ ".password");
            %space.type = %this.getValue("space" @ %idx @ ".type");
            %space.vurl = %this.getValue("space" @ %idx @ ".URI");
            %space.videoStream = %this.getValue("space" @ %idx @ ".videoStream");
            %space.buildingName = %this.getValue("space" @ %idx @ ".building");
            %space.blockedList = "";
            %banCount = %this.getValue("space" @ %idx @ ".banCount");
            if (%banCount $= "")
            {
                %banCount = 0;
            }
            %k = 0;
            while (%k < %banCount)
            {
                %blockedUser = %this.getValue("space" @ %idx @ ".ban" @ %k);
                if (%blockedUser $= "")
                {
                    warn(getScopeName() @ "->banned user #" @ %k @ " out of " @ %banCount @ ", was NULL!");
                }
                else
                {
                    if (findField(%space.blockedList, %blockedUser) >= 0)
                    {
                        warn(getScopeName() @ "->banned user #" @ %k @ " OUT OF " @ %banCount @ ", is a duplicate entry! entry = " @ %blockedUser @ " .");
                    }
                    else
                    {
                        if (!(%space.blockedList $= ""))
                        {
                            %space.blockedList = %space.blockedList TAB %blockedUser;
                        }
                        else
                        {
                            %space.blockedList = %blockedUser;
                        }
                    }
                }
                %k = %k + 1;
            }
            %space.blockedList = trim(%space.blockedList);
            %this.tracker.spaces.add(%space);
            %idx = %idx + 1;
        }
    }
    if (isObject(%this.tracker))
    {
        %this.tracker.doneCSList = 1;
        checkDoneBuildingDirectory(%this.tracker);
    }
    %this.schedule(0, "delete");
    return ;
}
function GetSpaceInfo::onError(%this, %unused, %errMsg)
{
    log("network", "debug", "GetSpaceInfo::onError: " @ %errMsg);
    if (!(%this.tracker.callbackFailure $= ""))
    {
        %command = %this.tracker.callbackFailure @ "( %this.tracker.buildingName, %errMsg);";
        log("network", "debug", "About to eval callback: \"" @ %command @ "\"");
        eval(%command);
    }
    %this.schedule(0, "delete");
    return ;
}
function purchaseApartmentRequest(%space, %useBux, %unused, %callback, %callbackFail)
{
    %request = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %request.callback = %callback;
    %request.callbackFail = %callbackFail;
    %url = $Net::ClientServiceURL @ "/PurchaseCustomSpace?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "building=" @ urlEncode(%space.buildingInfo.name) @ "&" @ "floorPlan=" @ urlEncode(%space.floorPlanName) @ "&" @ "payWith=" @ %useBux ? "vbux" : "vpoints";
    log("network", "debug", "PurchaseSpace: " @ %url);
    %request.setURL(%url);
    %request.start();
    return ;
}
function PurchaseSpaceRequest::onDone(%this)
{
    echo(getScopeName());
    %status = findRequestStatus(%this);
    log("network", "info", "PurchaseSpaceRequest status: " @ %status);
    if (%status $= "success")
    {
        %name = %this.getValue("name");
        %building = %this.getValue("building");
        %vurl = %this.getValue("vurl");
        if (%vurl $= "")
        {
            warn("Server not returning space VURL in .vurl parameter.");
            %vurl = %this.getValue("URI");
        }
        if (!(%this.callback $= ""))
        {
            %command = %this.callback @ "( %building, %name, %vurl );";
            log("network", "debug", "About to eval callback: " @ %command);
            eval(%command);
        }
    }
    else
    {
        %result = %this.getValue("items0.validationResults");
        if (!(%this.callbackFail $= ""))
        {
            if (!(%result $= ""))
            {
                %command = %this.callbackFail @ "( %result );";
            }
            else
            {
                %command = %this.callbackFail @ "( \"error\" );";
            }
            eval(%command);
        }
    }
    %this.schedule(0, "delete");
    return ;
}
function PurchaseSpaceRequest::onError(%this, %unused, %errMsg)
{
    log("network", "debug", "PurchaseSpaceRequest::onError: " @ %errMsg);
    if (!(%this.callbackFail $= ""))
    {
        %command = %this.callbackFail @ "( %status );";
        eval(%command);
    }
    %this.schedule(0, "delete");
    return ;
}
function getCustomSpacePurchaseInfo(%space, %callback)
{
    %request = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %request.callback = %callback;
    %request.space = %space;
    %url = $Net::ClientServiceURL @ "/GetCustomSpacePurchaseInfo?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "building=" @ urlEncode(%space.buildingInfo.name) @ "&" @ "floorPlan=" @ urlEncode(%space.floorPlanName);
    log("network", "debug", "PurchaseSpace: " @ %url);
    %request.setURL(%url);
    %request.start();
    return ;
}
function CustomSpacePurchaseInfo::onDone(%this)
{
    %status = findRequestStatus(%this);
    if (!(%status $= "success"))
    {
        %statusMsg = %this.getValue("statusMsg");
        handleSystemMessage("msgInfoMessage", "" @ %this.blockText @ " unsuccessful.");
        return ;
    }
    %floorplan = %this.space.floorplan;
    %floorplan.sku = %this.getValue("sku");
    %floorplan.minLevel = %this.getValue("minLevel");
    %floorplan.quantity = %this.getValue("quantity");
    %floorplan.priceVBux = %this.getValue("priceVBux");
    %floorplan.priceVPoints = %this.getValue("priceVPoints");
    %floorplan.tradeInValueVBux = %this.getValue("tradeInCreditVBux");
    %floorplan.tradeInValueVPoints = %this.getValue("tradeInCreditVPoints");
    %floorplan.expectedError = %this.getValue("expectedError");
    %floorplan.isUpgrade = (%floorplan.tradeInValueVBux > 0) || (%floorplan.tradeInValueVPoints > 0);
    if (!(%this.callback $= ""))
    {
        %cmd = %this.callback @ "(" @ %this.space @ ");";
        eval(%cmd);
    }
    %this.delete();
    return ;
}
function CustomSpacePurchaseInfo::onError(%this, %unused, %errMsg)
{
    return ;
}
function csSelectLayout(%layoutToSelect)
{
    commandToServer('CSSelectLayout', CustomSpaceClient::GetSpaceImIn(), %layoutToSelect);
    return ;
}
function clientCmdCSLayoutSelected(%unused, %audioStream, %videoStream)
{
    refreshActiveFurniture();
    CSFurnitureMover.SelectNuggetID(-1);
    %envMgrVideoStr = %videoStream $= "" ? "no-video" : %videoStream;
    CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), "", "", "", %audioStream, %envMgrVideoStr);
    return ;
}
function csCopyLayoutFromTo(%from, %to)
{
    if (%from == %to)
    {
        error(getScopeName() @ "->being asked to copy current layout into itself! returning.");
        return ;
    }
    commandToServer('CSCopyLayoutFromTo', CustomSpaceClient::GetSpaceImIn(), %from, %to);
    return ;
}
function clientCmdCSGotLayoutVitals(%infoStr)
{
    %layoutNum = getField(%infoStr, 0);
    if (CSLayoutSelector.copyTarget == %layoutNum)
    {
        CSLayoutSelector.gotCopyTargetInfo(%infoStr);
    }
    return ;
}
function csGetLayoutVitals(%layoutNum)
{
    commandToServer('CSGetLayoutVitals', CustomSpaceClient::GetSpaceImIn(), %layoutNum);
    return ;
}
function csSaveMySpacePropertiesAsDefault()
{
    commandToServer('CSSaveSpacePropertiesAsDefault', CustomSpaceClient::GetSpaceImIn());
    return ;
}
function csSaveLayoutAsDefault(%layoutNum)
{
    commandToServer('CSSaveLayoutAsDefault', CustomSpaceClient::GetSpaceImIn(), %layoutNum);
    return ;
}
function csLoadMediaFavorites()
{
    %mediafavorites = gUserPropMgrClient.getProperty($Player::Name, "mediafavoritelist", "vside://radio/" @ $CSSpaceInfo.audioStream TAB $CSSpaceInfo.videoStream);
    echo("csLoadMediaFavorites - \"" @ %mediafavorites @ "\"");
    CSMediaDisplay.setMediaFavorites(%mediafavorites);
    return ;
}
function csSaveMediaFavorites()
{
    %mediafavorites = CSMediaDisplay.getMediaFavorites();
    gUserPropMgrClient.setProperty($Player::Name, "mediafavoritelist", %mediafavorites);
    return ;
}
function csRequestHotMedia()
{
    %request = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %url = $Net::ClientServiceURL @ "/GetUrlRatingList?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "type=VIDEO" @ "&" @ "order=BY_SHOWS" @ "&" @ "first=0" @ "&" @ "count=" @ $CSMediaDisplay::DefaultFavoriteCount;
    log("network", "debug", "requesting \'hot\' media");
    %request.setURL(%url);
    %request.start();
    return ;
}
function GetUrlRatingListRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    if (!(%status $= "success"))
    {
        %this.onError(%this, 0, %status);
        return ;
    }
    %mediaList = "";
    %count = %this.getValue("mediaCount");
    %idx = 0;
    while (%idx < $CSMediaDisplay::DefaultFavoriteCount)
    {
        %mediaURL = urlDecode(%this.getValue("media" @ %idx @ ".url"));
        %viewCount = %this.getValue("media" @ %idx @ ".viewCount");
        %showCount = %this.getValue("media" @ %idx @ ".showCount");
        if (%showCount > 0)
        {
            %mediaInfo = %mediaURL SPC %viewCount SPC %showCount;
            if (%mediaList $= "")
            {
                %mediaList = %mediaInfo;
            }
            else
            {
                %mediaList = %mediaList TAB %mediaInfo;
            }
        }
        %idx = %idx + 1;
    }
    CSMediaDisplay.setMediaHotlist(%mediaList);
    %this.schedule(0, "delete");
    return %idx;
}
function GetUrlRatingListRequest::onError(%this, %unused, %errMsg)
{
    log("network", "warn", "GetUrlRatingRequest::onError: " @ %errMsg);
    %this.schedule(0, "delete");
    return ;
}
function csRequestMediaStatistics(%mediaURL)
{
    %request = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %url = $Net::ClientServiceURL @ "/GetUrlRating?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "url=" @ urlEncode(%mediaURL);
    log("network", "debug", "UrlRating: request rating for: " @ %mediaURL);
    %request.mediaurl = %mediaURL;
    %request.setURL(%url);
    %request.start();
    return ;
}
function GetUrlRatingRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    if (!(%status $= "success"))
    {
        %this.onError(%this, 0, %status);
        return ;
    }
    else
    {
        %mediaURL = %this.getValue("url");
        %mediaviews = %this.getValue("viewCount");
        %mediaplays = %this.getValue("showCount");
        CSMediaDisplay.setMediaStatistics(%mediaURL, %mediaviews, %mediaplays);
    }
    %this.schedule(0, "delete");
    return ;
}
function GetUrlRatingRequest::onError(%this, %unused, %errMsg)
{
    log("network", "warn", "GetUrlRatingRequest::onError: " @ %errMsg);
    CSMediaDisplay.clearMediaStatistics(%this.mediaurl);
    %this.schedule(0, "delete");
    return ;
}
function csRecordMediaShow(%mediaURL, %type)
{
    %request = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %url = $Net::ClientServiceURL @ "/RecordUrlShow?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "url=" @ urlEncode(%mediaURL) @ "&" @ "type=" @ %type;
    log("network", "debug", "ShowRequest: request rating for: " @ %mediaURL);
    %request.mediaurl = %mediaURL;
    %request.setURL(%url);
    %request.start();
    return ;
}
function RecordUrlShowRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    if (!(%status $= "success"))
    {
        %this.onError(%this, 0, %status);
        return ;
    }
    %this.schedule(0, "delete");
    return ;
}
function RecordUrlShowRequest::onError(%this, %unused, %errMsg)
{
    log("network", "warn", "RecordUrlShowRequest::onError: " @ %errMsg);
    %this.schedule(0, "delete");
    return ;
}
function csRecordMediaView(%mediaURL, %type)
{
    %request = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %url = $Net::ClientServiceURL @ "/RecordUrlView?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "url=" @ urlEncode(%mediaURL) @ "&" @ "type=" @ %type;
    log("network", "debug", "ViewRequest: request rating for: " @ %mediaURL);
    %request.mediaurl = %mediaURL;
    %request.setURL(%url);
    %request.start();
    return ;
}
function RecordUrlViewRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    if (!(%status $= "success"))
    {
        %this.onError(%this, 0, %status);
        return ;
    }
    %this.schedule(0, "delete");
    return ;
}
function RecordUrlViewRequest::onError(%this, %unused, %errMsg)
{
    log("network", "warn", "RecordUrlViewRequest::onError: " @ %errMsg);
    %this.schedule(0, "delete");
    return ;
}
