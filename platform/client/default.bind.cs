function initActionMaps() {
    if (isObject()) {
        delete();
    }
    new ActionMap(moveMap);
    new ActionMap(functionMap);
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
    if ($ETS::devMode) {
        "ctrl F1".bindCmd("toggleVisibleState(geActivitiesPanel);", "");
    }
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
    if (!(%key $= "")) {
        %key.bindCmd("startTextEntry();", "");
        %messageKeys = NextToken(%messageKeys, " ");
        key;
    }
};
initActionMaps();
mapMessageKeys();
function escapeFromGame() {
    %dragCtrl = getDragControl();
    Canvas;
    if (isObject(%dragCtrl)) {
        releaseDragControl();
        return Canvas;
    }
    %topGui = (Canvas - getCount()).getObject();
    1.0;
    %topName = %topGui.getName();
    Canvas;
    if (!(%topName $= "playGui")) {
        if (!(%topName $= "ConsoleDlg")) {
            %topGui.close(1);
        }
        ToggleConsoleReally(1);
        return;
    }
    if ((HudTabs >= currentTabIndex)) {
        overrideLockedOpen = 0.0 @ 1 @ HudTabs;
        close();
        return HudTabs;
    }
    %focused = getFirstResponder();
    Canvas;
    if (isObject(%focused)) {
        if (!(%focused SPC escCommand $= "")) {
            eval(escCommand);
            return %focused;
        }
        if ((getId() == %focused)) {
            finishTextEntry();
            return MessageHudEdit;
        }
    }
    if (!(closeTopClosableWindow())) {
        if (isVisible()) {
            finishTextEntry();
        }
        if (isVisible()) {
            0.close();
        }
        -(1.0).selectConvAtIndex();
    }
};
$movementSpeed = 1;
function setSpeed(%speed) {
    if (%speed) {
        $movementSpeed = %speed;
    }
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
    if (isObject($player)) {
        $player.lookUpDown(0);
        $player.lookLeftRight(0);
    }
};
function standOrLeaveOrbitModeIfAppropriate() {
    if ((1.0 != $IN_FREEFLY_CAM)) {
        SendStandCommand(1);
    }
    if ((1.0 == $IN_ORBIT_CAM)) {
        togglePlayerCamMode();
    }
};
function moveleft(%val) {
    if (!($IN_ORBIT_CAM)) {
    }
    if (!($player.isSitting())) {
        setIdle(0);
    }
    $mvLeftAction = ($movementSpeed * %val);
    if (%val) {
        standOrLeaveOrbitModeIfAppropriate();
    }
};
function moveright(%val) {
    if (!($IN_ORBIT_CAM)) {
    }
    if (!($player.isSitting())) {
        setIdle(0);
    }
    $mvRightAction = ($movementSpeed * %val);
    if (%val) {
        standOrLeaveOrbitModeIfAppropriate();
    }
};
$IN_FREEFLY_CAM = 0;
function clientCmdSetInFreeflyCam(%val) {
    $IN_FREEFLY_CAM = %val;
};
function moveforwardFast(%val) {
    if (%val) {
        $mvForwardAction = $movementSpeed;
        commandToServer('goForwardAtFasterRate');
        commandToServer('goForwardAtFasterRate');
    }
    $mvForwardAction = 0;
};
function moveFaster(%val) {
    if (!(%val)) {
        return;
    }
    commandToServer('goFaster');
};
function doubleTapActionStop(%actionTag) {
    if (%actionTag[$DoubleTapStopTimer @ %actionTag]) {
        cancel(%actionTag[$DoubleTapStopTimer @ %actionTag]);
        %actionTag[$DoubleTapStopTimer @ %actionTag] = 0;
    }
    %actionTag[$DoubleTapActionAlreadyDone @ %actionTag] = 0;
};
function doubleTapDeclareActionVariable(%actionTag) {
    %actionTag[$DoubleTapStopTimer @ %actionTag] = 0;
    %actionTag[$DoubleTapActionAlreadyDone @ %actionTag] = 0;
};
function doubleTapCheckOnAction(%actionTag, %keyDown, %canDoubleTapInCamera, %resetDelayMS) {
    if (%keyDown) {
        if (%actionTag[$DoubleTapStopTimer @ %actionTag]) {
            cancel(%actionTag[$DoubleTapStopTimer @ %actionTag]);
            %actionTag[$DoubleTapStopTimer @ %actionTag] = 0;
        }
        if ((0.0 == %actionTag[$DoubleTapActionAlreadyDone @ %actionTag])) {
            %actionTag[$DoubleTapActionAlreadyDone @ %actionTag] = 1;
            return 0;
        }
        return 1;
    }
    if (!(%canDoubleTapInCamera)) {
    }
    if ((1.0 == $IN_FREEFLY_CAM)) {
        doubleTapActionStop(%actionTag);
    }
    %actionTag[$DoubleTapStopTimer @ %actionTag] = schedule(%resetDelayMS, 0, "doubleTapActionStop", %actionTag);
};
doubleTapDeclareActionVariable("forward");
doubleTapDeclareActionVariable("left");
doubleTapDeclareActionVariable("right");
function moveforward(%val) {
    setIdle(0);
    %doubleTap = doubleTapCheckOnAction("forward", %val, 0, 250);
    if (%val) {
        if (!(%doubleTap)) {
            $mvForwardAction = $movementSpeed;
            standOrLeaveOrbitModeIfAppropriate();
        }
        $mvForwardAction = $movementSpeed;
        commandToServer('goForwardAtFasterRate');
    }
    $mvForwardAction = 0;
};
$IN_ORBIT_CAM = 0;
function togglePlayerCamMode() {
    commandToServer('nextCamMode');
    $IN_ORBIT_CAM = !($IN_ORBIT_CAM);
};
function nextPlayerCamMode() {
    if ($IN_ORBIT_CAM) {
        toggleFirstPerson();
        togglePlayerCamMode();
    }
    if ($firstPerson) {
        toggleFirstPerson();
    }
    togglePlayerCamMode();
    if (isObject()) {
        if (!($IN_ORBIT_CAM)) {
        }
        !($firstPerson).setVisible();
    }
};
function ClientCmdOnOrbitMode(%orbitMode) {
    $IN_ORBIT_CAM = %orbitMode;
};
function movebackward(%val) {
    if (!($IN_ORBIT_CAM)) {
    }
    if (!($player.isSitting())) {
        setIdle(0);
    }
    $mvBackwardAction = ($movementSpeed * %val);
    if (%val) {
        standOrLeaveOrbitModeIfAppropriate();
    }
};
function moveup(%val) {
    $mvUpAction = ($movementSpeed * %val);
};
function movedown(%val) {
    $mvDownAction = ($movementSpeed * %val);
};
function turnLeft(%val) {
    %doubleTap = doubleTapCheckOnAction("left", %val, 0, 250);
    if (!($IN_ORBIT_CAM)) {
    }
    if (!($player.isSitting())) {
        setIdle(0);
    }
    if (%val) {
    }
    $mvYawRightSpeed = 0;
    $Pref::Input::KeyboardTurnSpeed;
    $mvYawRightSpeedBase = $mvYawRightSpeed;
};
function turnRight(%val) {
    %doubleTap = doubleTapCheckOnAction("right", %val, 0, 250);
    if (!($IN_ORBIT_CAM)) {
    }
    if (!($player.isSitting())) {
        setIdle(0);
    }
    if (%val) {
    }
    $mvYawLeftSpeed = 0;
    $Pref::Input::KeyboardTurnSpeed;
    $mvYawLeftSpeedBase = $mvYawLeftSpeed;
};
function panUp(%val) {
    if (!($IN_ORBIT_CAM)) {
    }
    if (!($player.isSitting())) {
        setIdle(0);
    }
    if (%val) {
    }
    $mvPitchDownSpeed = 0;
    $Pref::Input::KeyboardTurnSpeed;
};
function panDown(%val) {
    if (!($IN_ORBIT_CAM)) {
    }
    if (!($player.isSitting())) {
        setIdle(0);
    }
    if (%val) {
    }
    $mvPitchUpSpeed = 0;
    $Pref::Input::KeyboardTurnSpeed;
};
function getMouseAdjustAmount(%val) {
    return (0.01 * ((90.0 / $cameraFov) * %val));
};
function yaw(%val) {
    if (!($IN_ORBIT_CAM)) {
    }
    if (!($player.isSitting())) {
        setIdle(0);
    }
    $mvYaw = (getMouseAdjustAmount(%val) + $mvYaw);
};
function pitch(%val) {
    if (!($IN_ORBIT_CAM)) {
    }
    if (!($player.isSitting())) {
        setIdle(0);
    }
    $mvPitch = (getMouseAdjustAmount(%val) + $mvPitch);
};
function changeCameraFOV(%val) {
    if (onMouseWheelDifSkus(%val)) {
        return;
    }
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
    if ($firstPerson) {
    }
    if (!($GameConnection.isPresentAtBody())) {
        changeCameraFOV(%val);
        return;
    }
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
    if (!(isDefined("%actionNum"))) {
        %actionNum = 0;
    }
    if (isVisible()) {
        if (!(isDoingPropAction)) {
            isDoingPropAction = ClosetGui @ 1 @ ClosetGui;
            ClosetGui;
            %propAnimation = $player.getPropAnimationFromSkus($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], %actionNum);
            if ((%propAnimation $= "")) {
                isDoingPropAction = 0 @ ClosetGui;
            }
            $player.playAnim(%propAnimation);
        }
    }
    commandToServer('DoPropAction', %actionNum);
};
function stopPropAction() {
    if (isVisible()) {
        if (isDoingPropAction) {
            isDoingPropAction = ClosetGui @ 0 @ ClosetGui;
            ClosetGui;
            %anim = $player.getGender() @ $player.getGenre() @ "idl1b";
            $player.playAnim(%anim);
        }
    }
    commandToServer('StopPropAction');
};
function mouseFire(%val) {
    $mvTriggerCount0 = (1.0 + $mvTriggerCount0);
};
function altTrigger(%val) {
    $mvTriggerCount1 = (1.0 + $mvTriggerCount1);
};
function toggleZoom(%val) {
    if (%val) {
        $ZoomOn = 0;
        setFOV($UserPref::Player::DefaultFOV);
    }
    $ZoomOn = 1;
    setFOV($Pref::Player::CurrentFOV);
};
function toggleFreeLook(%val) {
    if (%val) {
        $mvFreeLook = 1;
    }
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
    if (!(isDefined("%amt"))) {
        %amt = 1.0;
    }
    $cameraFOVAdjustment = (%amt * 100.0);
    zoomTick();
};
function startZoomOut(%amt) {
    if (!(isDefined("%amt"))) {
        %amt = 1.0;
    }
    $cameraFOVAdjustment = (%amt * -(100.0));
    zoomTick();
};
function startDollyIn(%amt) {
    if (!(isDefined("%amt"))) {
        %amt = 1.0;
    }
    $cameraDistAdjustment = (%amt * 100.0);
    zoomTick();
};
function startDollyOut(%amt) {
    if (!(isDefined("%amt"))) {
        %amt = 1.0;
    }
    $cameraDistAdjustment = (%amt * -(100.0));
    zoomTick();
};
function startDollyZoomIn(%amt) {
    if (!(isDefined("%amt"))) {
        %amt = 1.0;
    }
    $cameraFOVAdjustment = (%amt * 100.0);
    $cameraDistAdjustment = 1;
    zoomTick();
};
function startDollyZoomOut(%amt) {
    if (!(isDefined("%amt"))) {
        %amt = 1.0;
    }
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
    if ($cameraFOVTimer) {
        cancel($cameraFOVTimer);
    }
    if ((0.0 != ($cameraDistAdjustment * $cameraFOVAdjustment))) {
        changeCameraDistAndFOV($cameraFOVAdjustment);
    }
    changeCameraFOV($cameraFOVAdjustment);
    changeCameraDist($cameraDistAdjustment);
    if ((0.0 != $cameraFOVAdjustment)) {
    }
    if ((0.0 != $cameraDistAdjustment)) {
        $cameraFOVTimer = schedule(25, 0, "zoomTick");
    }
};
function buttonBarMenuLogout() {
    logout(0);
    exit();
};
function toggleVisibleState(%this) {
    if (%this.isVisible()) {
        %this.close(0);
    }
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
    if (!(isObject(%panel))) {
        return CSPanelCategories;
    }
    %category = %panel.getName().get();
    CSPanelCategories;
    %size = size();
    CSPanelCategories;
    %i = 0;
    if ((%size < %i)) {
        if (!(CSPanelCategories SPC %i.getValue() $= %category)) {
            %i.getKey().close();
        }
        %i = (1.0 + %i);
        CSPanelCategories;
    }
};
function numCSPanelsOpen() {
    %num = 0;
    %size = size();
    CSPanelCategories;
    %i = 0;
    if ((%size < %i)) {
        if (%i.getKey().isVisible()) {
            %num = (1.0 + %num);
            CSPanelCategories;
        }
        %i = (1.0 + %i);
    }
    return %num;
};
function toggleCSPanel(%panel) {
    if (!(isObject(%panel))) {
        return;
    }
    %panel.toggle();
};
function toggleBuddyHud() {
    toggleVisibleState();
};
function toggleBuddyHudForTab(%tabName) {
    %wasOpen = 1;
    if (!(visible)) {
        %wasOpen = 0;
        BuddyHudWin;
        open();
    }
    %currentTabName = name;
    getCurrentTab();
    if (%wasOpen) {
    }
    if ((BuddyHudTabs SPC %tabName $= %currentTabName)) {
        close();
        return BuddyHudWin;
    }
    %tabName.selectTabWithName();
};
function toggleSelfViewHud() {
    showRaiseOrHide();
    if (!(isVisible())) {
    }
    if ($IN_ORBIT_CAM) {
        togglePlayerCamMode();
    }
};
function toggleEmoteHud() {
    toggleVisibleState();
};
function toggleMusicHud() {
    if (isShowing()) {
        hide();
    }
    show();
    1.keepOpen();
};
function toggleOptionsPanel() {
    toggleVisibleState();
};
function okToOpenClosetGui() {
    if (isObject()) {
        if ((ApplauseMeterGui SPC applauseMeterUse $= "sumo")) {
            if ((ApplauseMeterGui SPC sumoGameType $= "PillowFightGame")) {
                MessageBoxOK(ApplauseMeterGui, , "");
            }
            MessageBoxOK(, , "");
            return 0;
        }
    }
    return 1;
};
function toggleWardrobe() {
    if (!(okToOpenClosetGui())) {
        return;
    }
    toggleVisibleState();
};
function toggleBodyTab() {
    if (!(okToOpenClosetGui())) {
        return;
    }
    toggleVisibleState();
    "Body".selectTabWithName();
};
function toggleClosetTab() {
    if (!(okToOpenClosetGui())) {
        return;
    }
    toggleVisibleState();
    "Closet".selectTabWithName();
};
function toggleClosetGui() {
    scriptProfiler_EnterScope();
    if (!(okToOpenClosetGui())) {
        return;
    }
    toggleVisibleState();
    if (isVisible()) {
        if ((ClosetGui SPC $gCurrentStoreName $= "")) {
            %tabToOpen = lastTabOpened;
            ClosetGui;
            if ((ClosetGui SPC %tabToOpen $= "")) {
                %tabToOpen = "Closet";
            }
            %tabToOpen.selectTabWithName();
        }
        "Shops".selectTabWithName();
    }
    scriptProfiler_LeaveScope();
};
function refreshCSSelector() {
    if (isVisible()) {
        refresh();
    }
};
function toggleBuildingDirectory() {
    if (visible) {
        close();
    }
    if (!(BuildingDirectoryButton SPC lastBuildingEntered $= "")) {
        lastBuildingEntered.open();
    }
};
function toggleClosetItemCategory(%category) {
    if (!(okToOpenClosetGui())) {
        return;
    }
    toggleVisibleState();
    if (visible) {
        "CLOSET".selectTabWithName();
        0.onSelect(%category);
    }
};
function toggleStore() {
    if (!(okToOpenClosetGui())) {
        return;
    }
    if (($gCurrentStoreName $= "")) {
    }
    toggleVisibleState();
    if (visible) {
        "SHOPS".selectTabWithName();
    }
};
function toggleSnapshot() {
    if (!(okToOpenClosetGui())) {
        return;
    }
    toggleVisibleState();
};
function toggleAIMHud() {
    toggleVisibleState();
    if (isVisible()) {
        "AIM".selectTabWithName();
    }
};
function toggleWorldControlPanel() {
    showRaiseOrHide();
};
function toggleDancePad() {
    showRaiseOrHide();
};
function toggleBoneBlendGui() {
    if ($player.isDebugging()) {
        showRaiseOrHide();
    }
};
function toggleTGF() {
    if ((Canvas != getContent().getId())) {
    }
    if ((geTGF != getParent().getId())) {
        0.setVisible();
    }
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
    if ((0.0 != $CSBuildingInfo)) {
        city.selectCity();
    }
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
    if (!(isObjectAndHasPermission_NoWarn($player, "fly"))) {
        return;
    }
    commandToServer('DropCameraAtPlayer');
};
function dropPlayerAtCamera() {
    if (!(isObjectAndHasPermission_NoWarn($player, "fly"))) {
        return;
    }
    commandToServer('DropPlayerAtCamera');
};
function oxe_CameraSpeed(%val) {
    $Camera::movementSpeed = ((1.0 * (1.0 - %val)) + 0.5);
};
$MFDebugRenderMode = 0;
function cycleDebugRenderMode() {
    if (!($player.rolesPermissionCheckNoWarn("debugPassive"))) {
        return;
    }
    if ((getBuildString() $= "Debug")) {
        if ((0.0 == $MFDebugRenderMode)) {
            $MFDebugRenderMode = 1;
            GLEnableOutline(1);
        }
        if ((1.0 == $MFDebugRenderMode)) {
            $MFDebugRenderMode = 2;
            GLEnableOutline(0);
            setInteriorRenderMode(7);
            showInterior();
        }
        if ((2.0 == $MFDebugRenderMode)) {
            $MFDebugRenderMode = 0;
            setInteriorRenderMode(0);
            GLEnableOutline(0);
            show();
        }
    }
    echo("Debug render modes only available when running a Debug build.");
};
"alt tilde".bind();
"ctrl capslock".bind();
"alt F9".bindCmd("cycleDebugRenderMode();", "");
"escape".bindCmd("", "escapeFromGame();");
"alt F4".bindCmd("", "");
if ((keyboard SPC $Platform $= "macos")) {
    "alt".bind("onDragAndDropCtrl");
}
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
new ActionMap(buildingDirectoryMap);
"F5".bindCmd("refreshCSSelector();", "");
"enter".bindCmd("CustomSpacesSelector.doOnKeyDown(\"enter\");", "");
"up".bindCmd("CustomSpacesSelector.doOnKeyDown(\"up\");", "CustomSpacesSelector.doOnKeyUp(\"up\");");
"down".bindCmd("CustomSpacesSelector.doOnKeyDown(\"down\");", "CustomSpacesSelector.doOnKeyUp(\"down\");");
new ActionMap(closetMap);
"F5".bindCmd("toggleClosetGui();", "");
"left".bindCmd("ClosetGui.doArrow(-1, 0);", "");
"right".bindCmd("ClosetGui.doArrow( 1, 0);", "");
"up".bindCmd("ClosetGui.doArrow( 0, 1);", "");
"down".bindCmd("ClosetGui.doArrow( 0,-1);", "");
"alt n".bindCmd("", "");
"F2".bindCmd("", "");
"ctrl r".bindCmd("ClosetGUI_RefreshTextures();", "");
new ActionMap(optionsMap);
"F6".bindCmd("toggleOptionsPanel();", "");
new ActionMap(tgfMapMap);
"alt n".bindCmd("toggleTGF();", "");
"F2".bindCmd("geTGF.closeFully();", "");
"F5".bindCmd("geTGF.onRefresh();", "");
new ActionMap(csFurnitureMap);
"delete".bindCmd("csTestFreeSelectedItem();", "");
if ((keyboard SPC $Platform $= "macos")) {
    "backspace".bindCmd("csTestFreeSelectedItem();", "");
    "delete".bindCmd("csTestFreeSelectedItem();", "");
    "opt x".bindCmd("CSFurnitureMover.doCut();", "");
    "opt c".bindCmd("CSFurnitureMover.doCopy();", "");
    "opt v".bindCmd("CSFurnitureMover.doPaste();", "");
}
"delete".bindCmd("csTestFreeSelectedItem();", "");
"ctrl x".bindCmd("CSFurnitureMover.doCut();", "");
"ctrl c".bindCmd("CSFurnitureMover.doCopy();", "");
"ctrl insert".bindCmd("CSFurnitureMover.doCopy();", "");
"ctrl v".bindCmd("CSFurnitureMover.doPaste();", "");
"shift insert".bindCmd("CSFurnitureMover.doPaste();", "");
