function initActionMaps() {
    delete();
    new ();
    new ();
    "xaxis".bind();
    "yaxis".bind();
    "zaxis".bind();
    "shift zaxis".bind();
    "ctrl zaxis".bind();
    "ctrl-shift zaxis".bind();
    "alt zaxis".bind();
    "shift-alt zaxis".bind();
    bind();
    "ctrl space".bind();
    "space".bind();
    "enter".bind();
    "numpadenter".bind();
    "up".bind();
    "shift up".bind();
    "rshift".bind();
    "lshift".bind();
    "down".bind();
    "right".bind();
    "left".bind();
    "ctrl w".bind();
    "ctrl-shift w".bind();
    "ctrl s".bind();
    "ctrl a".bind();
    "ctrl d".bind();
    "ctrl up".bind();
    "ctrl down".bind();
    "ctrl left".bind();
    "ctrl right".bind();
    "ctrl x".bind();
    "ctrl =".bindCmd("startDollyIn();", "stopZoom();");
    "ctrl-shift =".bindCmd("startDollyIn(0.3);", "stopZoom();");
    "ctrl -".bindCmd("startDollyOut();", "stopZoom();");
    "ctrl-shift -".bindCmd("startDollyOut(0.3);", "stopZoom();");
    "ctrl numpadadd".bindCmd("startDollyIn();", "stopZoom();");
    "ctrl-shift numpadadd".bindCmd("startDollyIn(0.3);", "stopZoom();");
    "ctrl numpadminus".bindCmd("startDollyOut();", "stopZoom();");
    "ctrl-shift numpadminus".bindCmd("startDollyOut(0.3);", "stopZoom();");
    "ctrl-alt numpadadd".bindCmd("startDollyZoomIn();", "stopZoom();");
    "shift-ctrl-alt numpadadd".bindCmd("startDollyZoomIn(0.3);", "stopZoom();");
    "ctrl-alt numpadminus".bindCmd("startDollyZoomOut();", "stopZoom();");
    "shift-ctrl-alt numpadminus".bindCmd("startDollyZoomOut(0.3);", "stopZoom();");
    "alt =".bindCmd("startZoomIn();", "stopZoom();");
    "shift-alt =".bindCmd("startZoomIn(0.3);", "stopZoom();");
    "alt -".bindCmd("startZoomOut();", "stopZoom();");
    "shift-alt -".bindCmd("startZoomOut(0.3);", "stopZoom();");
    "ctrl-alt =".bindCmd("startDollyZoomIn();", "stopZoom();");
    "shift-ctrl-alt =".bindCmd("startDollyZoomIn(0.3);", "stopZoom();");
    "ctrl-alt -".bindCmd("startDollyZoomOut();", "stopZoom();");
    "shift-ctrl-alt -".bindCmd("startDollyZoomOut(0.3);", "stopZoom();");
    "ctrl n".bindCmd("TutorialsCatalogClient::forceNextNag();", "");
    "ctrl tab".bindCmd("nextPlayerCamMode();", "");
    "ctrl b".bindCmd("BroadCastControlPanel.toggle();", "");
    "ctrl g".bindCmd("toggleGameMgrHudWin();", "");
    "ctrl k".bindCmd("toggleConversationDebug();", "");
    "ctrl o".bindCmd("toggleWorldControlPanel();", "");
    "ctrl t".bindCmd("toggleDanceTool();", "");
    "ctrl z".bindCmd("toggleFreeLook();", "");
    "ctrl r".bindCmd("playerTexturesReload();", "");
    "ctrl enter".bindCmd("doPropAction(0);", "stopPropAction(0);");
    "ctrl numpadenter".bindCmd("doPropAction(0);", "stopPropAction(0);");
    "ctrl '".bindCmd("doPropAction(1);", "stopPropAction(1);");
    "ctrl ;".bindCmd("doPropAction(2);", "stopPropAction(2);");
    "alt z".bindCmd("toggleSystemMessageDialog    ();", "");
    "alt b".bindCmd("toggleBenchmarksDialog       ();", "");
    "alt a".bindCmd("toggleAdminDialog            ();", "");
    "alt-shift a".bindCmd("toggleAnimatorPanel          ();", "");
    "alt-shift s".bindCmd("toggleSalonChairControlDialog();", "");
    "alt f".bindCmd("toggleBuddyHud    ();", "");
    "alt m".bindCmd("toggleMusicHud    ();", "");
    "ctrl m".bindCmd("toggleMusicHud    ();", "");
    "alt c".bindCmd("toggleClosetTab   ();", "");
    "alt s".bindCmd("toggleOptionsPanel();", "");
    "alt e".bindCmd("toggleEmoteHud    ();", "");
    "alt v".bindCmd("nextPlayerCamMode ();", "");
    "alt n".bindCmd("toggleTGF         ();", "");
    "alt F7".bindCmd("dropPlayerAtCamera();", "");
    "alt F8".bindCmd("dropCameraAtPlayer();", "");
    "ctrl F1".bindCmd("toggleVisibleState(geActivitiesPanel);", "");
    "F1".bindCmd("toggleLocalMap    ();", "");
    "F2".bindCmd("toggleTGF         ();", "");
    "F3".bindCmd("toggleBuddyHud    ();", "");
    "F4".bindCmd("toggleEmoteHud    ();", "");
    "F5".bindCmd("toggleClosetGui   ();", "");
    "F6".bindCmd("toggleOptionsPanel();", "");
    "F7".bindCmd("nextPlayerCamMode ();", "");
    "alt 1".bindCmd("oxe_CameraSpeed(\"1\"  );", "");
    "alt 2".bindCmd("oxe_CameraSpeed(\"2\"  );", "");
    "alt 3".bindCmd("oxe_CameraSpeed(\"3\"  );", "");
    "alt 4".bindCmd("oxe_CameraSpeed(\"4\"  );", "");
    "alt 5".bindCmd("oxe_CameraSpeed(\"6\"  );", "");
    "alt 6".bindCmd("oxe_CameraSpeed(\"8\"  );", "");
    "alt 7".bindCmd("oxe_CameraSpeed(\"10\" );", "");
    "alt 8".bindCmd("oxe_CameraSpeed(\"20\" );", "");
    "alt 9".bindCmd("oxe_CameraSpeed(\"50\" );", "");
    "alt 0".bindCmd("oxe_CameraSpeed(\"100\");", "");
};
function mapMessageKeys() {
    %messageKeys = "a b c d e f g h i j k l m n o p q r s t u v w x y z 0 1 2 3 4 5 6 7 8 9 " @ "A B C D E F G H I J K L M N O P Q R S T U V W X Y Z " @ "exclamation doublequote pound ampersand apostrophe lparen rparen " @ "comma minus period slash colon semicolon lessthan equals morethan lbracket backslash rbracket circumflex underscore " @ "grave tilde vertbar";
    %messageKeys = NextToken(%messageKeys, " ");
    key;
    %key.bindCmd("startTextEntry();", "");
    %messageKeys = NextToken(%messageKeys, " ");
    key;
};
initActionMaps();
mapMessageKeys();
function escapeFromGame() {
    %dragCtrl = getDragControl();
    Canvas;
    releaseDragControl();
    return Canvas;
    %topGui = (Canvas - getCount()).getObject();
    1.0;
    %topName = %topGui.getName();
    Canvas;
    %topGui.close(1);
    ToggleConsoleReally(1);
    return !((!((%topName $= "playGui")) SPC %topName $= "ConsoleDlg"));
    overrideLockedOpen = (HudTabs >= currentTabIndex) @ 1 @ HudTabs;
    0.0;
    close();
    return HudTabs;
    %focused = getFirstResponder();
    Canvas;
    eval(escCommand);
    return %focused;
    finishTextEntry();
    return (getId() == %focused);
    finishTextEntry();
    0.close();
    -(1.0).selectConvAtIndex();
};
$movementSpeed = 1;
function setSpeed(%speed) {
    $movementSpeed = %speed;
    %speed;
};
function stopMoving(%val) {
    $mvForwardAction = 0;
    $mvBackwardAction = 0;
    $mvLeftAction = 0;
    $mvRightAction = 0;
    $mvUpAction = 0;
    $mvDownAction = 0;
    $mvYawLeftSpeed = 0;
    $mvYawLeftSpeedBase = 0;
    $mvYawRightSpeed = 0;
    $mvYawRightSpeedBase = 0;
    $mvPitchUpSpeed = 0;
    $mvPitchDownSpeed = 0;
    $player.lookUpDown(0);
    $player.lookLeftRight(0);
};
function standOrLeaveOrbitModeIfAppropriate() {
    SendStandCommand(1);
    togglePlayerCamMode();
};
function moveleft(%val) {
    setIdle(0);
    $mvLeftAction = ($movementSpeed * %val);
    !($player.isSitting());
    standOrLeaveOrbitModeIfAppropriate();
};
function moveright(%val) {
    setIdle(0);
    $mvRightAction = ($movementSpeed * %val);
    !($player.isSitting());
    standOrLeaveOrbitModeIfAppropriate();
};
$IN_FREEFLY_CAM = 0;
function clientCmdSetInFreeflyCam(%val) {
    $IN_FREEFLY_CAM = %val;
};
function moveforwardFast(%val) {
    $mvForwardAction = $movementSpeed;
    %val;
    commandToServer('goForwardAtFasterRate');
    commandToServer('goForwardAtFasterRate');
    $mvForwardAction = 0;
};
function moveFaster(%val) {
    return !(%val);
    commandToServer('goFaster');
};
function doubleTapActionStop(%actionTag) {
    cancel(%actionTag[$DoubleTapStopTimer @ %actionTag]);
    %actionTag[$DoubleTapStopTimer @ %actionTag] = 0;
    %actionTag[$DoubleTapStopTimer @ %actionTag];
    %actionTag[$DoubleTapActionAlreadyDone @ %actionTag] = 0;
};
function doubleTapDeclareActionVariable(%actionTag) {
    %actionTag[$DoubleTapStopTimer @ %actionTag] = 0;
    %actionTag[$DoubleTapActionAlreadyDone @ %actionTag] = 0;
};
function doubleTapCheckOnAction(%actionTag, %keyDown, %canDoubleTapInCamera, %resetDelayMS) {
    cancel(%actionTag[$DoubleTapStopTimer @ %actionTag]);
    %actionTag[$DoubleTapStopTimer @ %actionTag] = 0;
    %actionTag[$DoubleTapStopTimer @ %actionTag];
    %actionTag[$DoubleTapActionAlreadyDone @ %actionTag] = 1;
    (0.0 == %actionTag[$DoubleTapActionAlreadyDone @ %actionTag]);
    return 0;
    return 1;
    doubleTapActionStop(%actionTag);
    %actionTag[$DoubleTapStopTimer @ %actionTag] = !(%canDoubleTapInCamera) @ (1.0 == $IN_FREEFLY_CAM) @ schedule(%resetDelayMS, 0, "doubleTapActionStop", %actionTag);
};
doubleTapDeclareActionVariable("forward");
doubleTapDeclareActionVariable("left");
doubleTapDeclareActionVariable("right");
function moveforward(%val) {
    setIdle(0);
    %doubleTap = doubleTapCheckOnAction("forward", %val, 0, 250);
    $mvForwardAction = $movementSpeed;
    !(%doubleTap);
    standOrLeaveOrbitModeIfAppropriate();
    $mvForwardAction = $movementSpeed;
    %val;
    commandToServer('goForwardAtFasterRate');
    $mvForwardAction = 0;
};
$IN_ORBIT_CAM = 0;
function togglePlayerCamMode() {
    commandToServer('nextCamMode');
    $IN_ORBIT_CAM = !($IN_ORBIT_CAM);
};
function nextPlayerCamMode() {
    toggleFirstPerson();
    togglePlayerCamMode();
    toggleFirstPerson();
    togglePlayerCamMode();
    !($firstPerson).setVisible();
};
function ClientCmdOnOrbitMode(%orbitMode) {
    $IN_ORBIT_CAM = %orbitMode;
};
function movebackward(%val) {
    setIdle(0);
    $mvBackwardAction = ($movementSpeed * %val);
    !($player.isSitting());
    standOrLeaveOrbitModeIfAppropriate();
};
function moveup(%val) {
    $mvUpAction = ($movementSpeed * %val);
};
function movedown(%val) {
    $mvDownAction = ($movementSpeed * %val);
};
function turnLeft(%val) {
    %doubleTap = doubleTapCheckOnAction("left", %val, 0, 250);
    setIdle(0);
    $mvYawRightSpeed = 0;
    $Pref::Input::KeyboardTurnSpeed;
    $mvYawRightSpeedBase = $mvYawRightSpeed;
    %val;
};
function turnRight(%val) {
    %doubleTap = doubleTapCheckOnAction("right", %val, 0, 250);
    setIdle(0);
    $mvYawLeftSpeed = 0;
    $Pref::Input::KeyboardTurnSpeed;
    $mvYawLeftSpeedBase = $mvYawLeftSpeed;
    %val;
};
function panUp(%val) {
    setIdle(0);
    $mvPitchDownSpeed = 0;
    $Pref::Input::KeyboardTurnSpeed;
};
function panDown(%val) {
    setIdle(0);
    $mvPitchUpSpeed = 0;
    $Pref::Input::KeyboardTurnSpeed;
};
function getMouseAdjustAmount(%val) {
    return (0.01 * ((90.0 / $cameraFov) * %val));
};
function yaw(%val) {
    setIdle(0);
    $mvYaw = (getMouseAdjustAmount(%val) + $mvYaw);
    !($player.isSitting());
};
function pitch(%val) {
    setIdle(0);
    $mvPitch = (getMouseAdjustAmount(%val) + $mvPitch);
    !($player.isSitting());
};
function changeCameraFOV(%val) {
    return onMouseWheelDifSkus(%val);
    %fov = getFovCur();
    %fov = ((0.05 * %val) - %fov);
    setFOV(%fov);
    $Pref::Player::CurrentFOV = %fov;
    $UserPref::Player::DefaultFOV = %fov;
};
$gCameraDistMin = 0.5;
$gCameraDistStartFaceZoom = 2.0;
$gCameraDistMax = 4.0;
function changeCameraDist(%val) {
    changeCameraFOV(%val);
    return !($GameConnection.isPresentAtBody());
    %val = (-(0.001) * %val);
    %val = (1.0 + %val);
    %f = (%val * $cameraDist);
    %f = max(%f, $gCameraDistMin);
    %f = min(%f, $gCameraDistMax);
    $cameraDist = %f;
};
function changeCameraDistAndFOV(%val) {
    %val = (-(0.001) * %val);
    %val = (1.0 + %val);
    %dollyMin = $gCameraDistMin;
    %dollyMax = $gCameraDistMax;
    %f = (%val * $cameraDist);
    %f = max(%f, %dollyMin);
    %f = min(%f, %dollyMax);
    %db = $player.getDataBlock();
    %fov = (cameraMinFov + ((0.9 - (%db * cameraMaxFov)) * (((%dollyMin - %dollyMax) / (%dollyMin - %f)) - 1.0)));
    %db;
    $cameraDist = %f;
    cameraMinFov;
    setFOV(%fov);
};
function changeCameraFOVFine(%val) {
    changeCameraFOV((0.3 * %val));
};
function changeCameraDistFine(%val) {
    changeCameraDist((0.3 * %val));
};
function changeCameraDistAndFOVFine(%val) {
    changeCameraDistAndFOV((0.3 * %val));
};
function jump(%val) {
    setIdle(0);
    $mvTriggerCount2 = (1.0 + $mvTriggerCount2);
};
function jumpOnce() {
    jump();
    jump();
};
function doPropAction(%actionNum) {
    %actionNum = 0;
    !(isDefined("%actionNum"));
    isDoingPropAction = !(isDoingPropAction) @ 1 @ ClosetGui;
    ClosetGui;
    %propAnimation = $player.getPropAnimationFromSkus($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], %actionNum);
    isVisible();
    isDoingPropAction = (ClosetGui SPC %propAnimation $= "") @ 0 @ ClosetGui;
    $player.playAnim(%propAnimation);
    commandToServer('DoPropAction', %actionNum);
};
function stopPropAction() {
    isDoingPropAction = isDoingPropAction @ 0 @ ClosetGui;
    ClosetGui;
    %anim = ClosetGui @ isVisible() @ $player.getGender() @ $player.getGenre() @ "idl1b";
    $player.playAnim(%anim);
    commandToServer('StopPropAction');
};
function mouseFire(%val) {
    $mvTriggerCount0 = (1.0 + $mvTriggerCount0);
};
function altTrigger(%val) {
    $mvTriggerCount1 = (1.0 + $mvTriggerCount1);
};
function toggleZoom(%val) {
    $ZoomOn = 0;
    %val;
    setFOV($UserPref::Player::DefaultFOV);
    $ZoomOn = 1;
    setFOV($Pref::Player::CurrentFOV);
};
function toggleFreeLook(%val) {
    $mvFreeLook = 1;
    %val;
    $mvFreeLook = 0;
};
function toggleFirstPerson() {
    $firstPerson = !($firstPerson);
};
function toggleCamera() {
    commandToServer('ToggleCamera');
};
$cameraFOVAdjustment = 0;
$cameraDistAdjustment = 0;
$cameraFOVTimer = 0;
function startZoomIn(%amt) {
    %amt = 1.0;
    !(isDefined("%amt"));
    $cameraFOVAdjustment = (%amt * 100.0);
    zoomTick();
};
function startZoomOut(%amt) {
    %amt = 1.0;
    !(isDefined("%amt"));
    $cameraFOVAdjustment = (%amt * -(100.0));
    zoomTick();
};
function startDollyIn(%amt) {
    %amt = 1.0;
    !(isDefined("%amt"));
    $cameraDistAdjustment = (%amt * 100.0);
    zoomTick();
};
function startDollyOut(%amt) {
    %amt = 1.0;
    !(isDefined("%amt"));
    $cameraDistAdjustment = (%amt * -(100.0));
    zoomTick();
};
function startDollyZoomIn(%amt) {
    %amt = 1.0;
    !(isDefined("%amt"));
    $cameraFOVAdjustment = (%amt * 100.0);
    $cameraDistAdjustment = 1;
    zoomTick();
};
function startDollyZoomOut(%amt) {
    %amt = 1.0;
    !(isDefined("%amt"));
    $cameraFOVAdjustment = (%amt * -(100.0));
    $cameraDistAdjustment = 1;
    zoomTick();
};
function stopZoom() {
    $cameraFOVAdjustment = 0;
    $cameraDistAdjustment = 0;
    zoomTick();
};
function zoomTick() {
    cancel($cameraFOVTimer);
    changeCameraDistAndFOV($cameraFOVAdjustment);
    changeCameraFOV($cameraFOVAdjustment);
    changeCameraDist($cameraDistAdjustment);
    $cameraFOVTimer = schedule(25, 0, "zoomTick");
    (0.0 != $cameraDistAdjustment);
};
function buttonBarMenuLogout() {
    logout(0);
    exit();
};
function toggleVisibleState(%this) {
    %this.close(0);
    %this.open();
};
safeEnsureScriptObject("StringMap", "CSPanelCategories");
"CSMediaDisplay".put("settings");
"CSRulesAndDescWindow".put("settings");
"CSFurnitureMover".put("furniture");
"CSInventoryBrowserWindow".put("furniture");
"CSShoppingBrowserWindow".put("furniture");
"CSPaintingWindow".put("painting");
function closeCSPanelsInOtherCategories(%panel) {
    return !(isObject(%panel));
    %category = %panel.getName().get();
    CSPanelCategories;
    %size = size();
    CSPanelCategories;
    %i = 0;
    %i.getKey().close();
    %i = (1.0 + %i);
    CSPanelCategories;
};
function numCSPanelsOpen() {
    %num = 0;
    %size = size();
    CSPanelCategories;
    %i = 0;
    %num = (1.0 + %num);
    %i.getKey().isVisible();
    %i = (1.0 + %i);
    CSPanelCategories;
    return %num;
};
function toggleCSPanel(%panel) {
    return !(isObject(%panel));
    %panel.toggle();
};
function toggleBuddyHud() {
    toggleVisibleState();
};
function toggleBuddyHudForTab(%tabName) {
    %wasOpen = 1;
    %wasOpen = 0;
    !(visible);
    open();
    %currentTabName = name;
    getCurrentTab();
    close();
    return BuddyHudWin;
    %tabName.selectTabWithName();
};
function toggleSelfViewHud() {
    showRaiseOrHide();
    togglePlayerCamMode();
};
function toggleEmoteHud() {
    toggleVisibleState();
};
function toggleMusicHud() {
    hide();
    show();
    1.keepOpen();
};
function toggleOptionsPanel() {
    toggleVisibleState();
};
function okToOpenClosetGui() {
    MessageBoxOK((ApplauseMeterGui SPC sumoGameType $= "PillowFightGame"), (ApplauseMeterGui SPC applauseMeterUse $= "sumo"), "");
    MessageBoxOK(isObject(), ApplauseMeterGui, "");
    return 0;
    return 1;
};
function toggleWardrobe() {
    return !(okToOpenClosetGui());
    toggleVisibleState();
};
function toggleBodyTab() {
    return !(okToOpenClosetGui());
    toggleVisibleState();
    "Body".selectTabWithName();
};
function toggleClosetTab() {
    return !(okToOpenClosetGui());
    toggleVisibleState();
    "Closet".selectTabWithName();
};
function toggleClosetGui() {
    scriptProfiler_EnterScope();
    return !(okToOpenClosetGui());
    toggleVisibleState();
    %tabToOpen = lastTabOpened;
    ClosetGui;
    %tabToOpen = "Closet";
    ((isVisible() SPC $gCurrentStoreName $= "") SPC %tabToOpen $= "");
    %tabToOpen.selectTabWithName();
    "Shops".selectTabWithName();
    scriptProfiler_LeaveScope();
};
function refreshCSSelector() {
    refresh();
};
function toggleBuildingDirectory() {
    close();
    lastBuildingEntered.open();
};
function toggleClosetItemCategory(%category) {
    return !(okToOpenClosetGui());
    toggleVisibleState();
    "CLOSET".selectTabWithName();
    0.onSelect(%category);
};
function toggleStore() {
    return !(okToOpenClosetGui());
    toggleVisibleState();
    "SHOPS".selectTabWithName();
};
function toggleSnapshot() {
    return !(okToOpenClosetGui());
    toggleVisibleState();
};
function toggleAIMHud() {
    toggleVisibleState();
    "AIM".selectTabWithName();
};
function toggleWorldControlPanel() {
    showRaiseOrHide();
};
function toggleDancePad() {
    showRaiseOrHide();
};
function toggleBoneBlendGui() {
    showRaiseOrHide();
};
function toggleTGF() {
    0.setVisible();
    toggleVisibleState();
};
function toggleWorldMap() {
    "".Maps_filterDestinationsByType();
    "Map".toggleToTabName();
    "multi_city".setView();
};
function toggleCityMap() {
    "Map".toggleToTabName();
    "".Maps_filterDestinationsByType();
    city.selectCity();
    $gContiguousSpaceName.selectCity();
};
function toggleTGFMapFiltered(%filterType) {
    toggleCityMap();
    %filterType.Maps_filterDestinationsByType();
};
function togglePerformerPanel() {
    toggle();
};
function toggleLocalMap() {
    toggleVisibleState();
};
function toggleCameraImgBroadcast() {
    toggle();
};
function toggleConversationDebug() {
};
function openHelpURL() {
    gotoWebPage($Net::HelpURL_General);
};
function openEventsURL() {
    gotoWebPage($Net::EventsURL);
};
function openForumsURL() {
    gotoWebPage($Net::ForumsURL);
};
function startRecordingDemo() {
    startDemoRecord();
};
function stopRecordingDemo() {
    stopDemoRecord();
};
function dropCameraAtPlayer() {
    return !(isObjectAndHasPermission_NoWarn($player, "fly"));
    commandToServer('DropCameraAtPlayer');
};
function dropPlayerAtCamera() {
    return !(isObjectAndHasPermission_NoWarn($player, "fly"));
    commandToServer('DropPlayerAtCamera');
};
function oxe_CameraSpeed(%val) {
    $Camera::movementSpeed = ((1.0 * (1.0 - %val)) + 0.5);
};
$MFDebugRenderMode = 0;
function cycleDebugRenderMode() {
    return !($player.rolesPermissionCheckNoWarn("debugPassive"));
    $MFDebugRenderMode = 1;
    (0.0 == $MFDebugRenderMode);
    GLEnableOutline(1);
    $MFDebugRenderMode = 2;
    (1.0 == $MFDebugRenderMode);
    GLEnableOutline(0);
    setInteriorRenderMode(7);
    showInterior();
    $MFDebugRenderMode = 0;
    (2.0 == $MFDebugRenderMode);
    setInteriorRenderMode(0);
    GLEnableOutline(0);
    show();
    echo("Debug render modes only available when running a Debug build.");
};
"alt tilde".bind();
"ctrl capslock".bind();
"alt F9".bindCmd("cycleDebugRenderMode();", "");
"escape".bindCmd("", "escapeFromGame();");
"alt F4".bindCmd("", "");
"alt".bind("onDragAndDropCtrl");
"lcontrol".bind("onDragAndDropCtrl");
"rcontrol".bind("onDragAndDropCtrl");
"F8".bindCmd("EmoteHudList.doFunc(\"F08\"   );", "");
"F9".bindCmd("EmoteHudList.doFunc(\"F09\"   );", "");
"F10".bindCmd("EmoteHudList.doFunc(\"F10\"   );", "");
"F11".bindCmd("EmoteHudList.doFunc(\"F11\"   );", "");
"F12".bindCmd("EmoteHudList.doFunc(\"F12\"   );", "");
"ctrl 1".bindCmd("EmoteHudList.doFunc(\"ctrl1\");", "");
"ctrl 2".bindCmd("EmoteHudList.doFunc(\"ctrl2\");", "");
"ctrl 3".bindCmd("EmoteHudList.doFunc(\"ctrl3\");", "");
"ctrl 4".bindCmd("EmoteHudList.doFunc(\"ctrl4\");", "");
"ctrl 5".bindCmd("EmoteHudList.doFunc(\"ctrl5\");", "");
"ctrl 6".bindCmd("EmoteHudList.doFunc(\"ctrl6\");", "");
"ctrl 7".bindCmd("EmoteHudList.doFunc(\"ctrl7\");", "");
"ctrl 8".bindCmd("EmoteHudList.doFunc(\"ctrl8\");", "");
"ctrl 9".bindCmd("EmoteHudList.doFunc(\"ctrl9\");", "");
"ctrl 0".bindCmd("EmoteHudList.doFunc(\"ctrl0\");", "");
toggleFirstPerson(1);
new ();
"F5".bindCmd("refreshCSSelector();", "");
"enter".bindCmd("CustomSpacesSelector.doOnKeyDown(\"enter\");", "");
"up".bindCmd("CustomSpacesSelector.doOnKeyDown(\"up\");", "CustomSpacesSelector.doOnKeyUp(\"up\");");
"down".bindCmd("CustomSpacesSelector.doOnKeyDown(\"down\");", "CustomSpacesSelector.doOnKeyUp(\"down\");");
new ();
"F5".bindCmd("toggleClosetGui();", "");
"left".bindCmd("ClosetGui.doArrow(-1, 0);", "");
"right".bindCmd("ClosetGui.doArrow( 1, 0);", "");
"up".bindCmd("ClosetGui.doArrow( 0, 1);", "");
"down".bindCmd("ClosetGui.doArrow( 0,-1);", "");
"alt n".bindCmd("", "");
"F2".bindCmd("", "");
"ctrl r".bindCmd("ClosetGUI_RefreshTextures();", "");
new ();
"F6".bindCmd("toggleOptionsPanel();", "");
new ();
"alt n".bindCmd("toggleTGF();", "");
"F2".bindCmd("geTGF.closeFully();", "");
"F5".bindCmd("geTGF.onRefresh();", "");
new ();
"delete".bindCmd("csTestFreeSelectedItem();", "");
"backspace".bindCmd("csTestFreeSelectedItem();", "");
"delete".bindCmd("csTestFreeSelectedItem();", "");
"opt x".bindCmd("CSFurnitureMover.doCut();", "");
"opt c".bindCmd("CSFurnitureMover.doCopy();", "");
"opt v".bindCmd("CSFurnitureMover.doPaste();", "");
"delete".bindCmd("csTestFreeSelectedItem();", "");
"ctrl x".bindCmd("CSFurnitureMover.doCut();", "");
"ctrl c".bindCmd("CSFurnitureMover.doCopy();", "");
"ctrl insert".bindCmd("CSFurnitureMover.doCopy();", "");
"ctrl v".bindCmd("CSFurnitureMover.doPaste();", "");
"shift insert".bindCmd("CSFurnitureMover.doPaste();", "");
