function initActionMaps() {
    if (isObject(moveMap)) {
        moveMap.delete();
    }
    new ActionMap(moveMap);
    new ActionMap(functionMap);
    yaw.bind(moveMap, mouse, "xaxis");
    pitch.bind(moveMap, mouse, "yaxis");
    changeCameraDist.bind(moveMap, mouse, "zaxis");
    changeCameraDistFine.bind(moveMap, mouse, "shift zaxis");
    changeCameraFOV.bind(moveMap, mouse, "ctrl zaxis");
    changeCameraFOVFine.bind(moveMap, mouse, "ctrl-shift zaxis");
    changeCameraDistAndFOV.bind(moveMap, mouse, "alt zaxis");
    changeCameraDistAndFOVFine.bind(moveMap, mouse, "shift-alt zaxis");
    mouseFire.bind(moveMap, mouse, button0);
    onActionKey.bind(moveMap, keyboard, "ctrl space");
    onThrowBall.bind(moveMap, keyboard, "space");
    onActionKey.bind(moveMap, keyboard, "enter");
    onActionKey.bind(moveMap, keyboard, "numpadenter");
    moveforward.bind(moveMap, keyboard, "up");
    moveforwardFast.bind(moveMap, keyboard, "shift up");
    moveFaster.bind(moveMap, keyboard, "rshift");
    moveFaster.bind(moveMap, keyboard, "lshift");
    movebackward.bind(moveMap, keyboard, "down");
    turnRight.bind(moveMap, keyboard, "right");
    turnLeft.bind(moveMap, keyboard, "left");
    moveforward.bind(moveMap, keyboard, "ctrl w");
    moveforwardFast.bind(moveMap, keyboard, "ctrl-shift w");
    movebackward.bind(moveMap, keyboard, "ctrl s");
    turnLeft.bind(moveMap, keyboard, "ctrl a");
    turnRight.bind(moveMap, keyboard, "ctrl d");
    panUp.bind(moveMap, keyboard, "ctrl up");
    panDown.bind(moveMap, keyboard, "ctrl down");
    moveleft.bind(moveMap, keyboard, "ctrl left");
    moveright.bind(moveMap, keyboard, "ctrl right");
    toggleZoom.bind(moveMap, keyboard, "ctrl x");
    "stopZoom();".bindCmd(moveMap, keyboard, "ctrl =", "startDollyIn();");
    "stopZoom();".bindCmd(moveMap, keyboard, "ctrl-shift =", "startDollyIn(0.3);");
    "stopZoom();".bindCmd(moveMap, keyboard, "ctrl -", "startDollyOut();");
    "stopZoom();".bindCmd(moveMap, keyboard, "ctrl-shift -", "startDollyOut(0.3);");
    "stopZoom();".bindCmd(moveMap, keyboard, "ctrl numpadadd", "startDollyIn();");
    "stopZoom();".bindCmd(moveMap, keyboard, "ctrl-shift numpadadd", "startDollyIn(0.3);");
    "stopZoom();".bindCmd(moveMap, keyboard, "ctrl numpadminus", "startDollyOut();");
    "stopZoom();".bindCmd(moveMap, keyboard, "ctrl-shift numpadminus", "startDollyOut(0.3);");
    "stopZoom();".bindCmd(moveMap, keyboard, "ctrl-alt numpadadd", "startDollyZoomIn();");
    "stopZoom();".bindCmd(moveMap, keyboard, "shift-ctrl-alt numpadadd", "startDollyZoomIn(0.3);");
    "stopZoom();".bindCmd(moveMap, keyboard, "ctrl-alt numpadminus", "startDollyZoomOut();");
    "stopZoom();".bindCmd(moveMap, keyboard, "shift-ctrl-alt numpadminus", "startDollyZoomOut(0.3);");
    "stopZoom();".bindCmd(moveMap, keyboard, "alt =", "startZoomIn();");
    "stopZoom();".bindCmd(moveMap, keyboard, "shift-alt =", "startZoomIn(0.3);");
    "stopZoom();".bindCmd(moveMap, keyboard, "alt -", "startZoomOut();");
    "stopZoom();".bindCmd(moveMap, keyboard, "shift-alt -", "startZoomOut(0.3);");
    "stopZoom();".bindCmd(moveMap, keyboard, "ctrl-alt =", "startDollyZoomIn();");
    "stopZoom();".bindCmd(moveMap, keyboard, "shift-ctrl-alt =", "startDollyZoomIn(0.3);");
    "stopZoom();".bindCmd(moveMap, keyboard, "ctrl-alt -", "startDollyZoomOut();");
    "stopZoom();".bindCmd(moveMap, keyboard, "shift-ctrl-alt -", "startDollyZoomOut(0.3);");
    "".bindCmd(moveMap, keyboard, "ctrl n", "TutorialsCatalogClient::forceNextNag();");
    "".bindCmd(moveMap, keyboard, "ctrl tab", "nextPlayerCamMode();");
    "".bindCmd(moveMap, keyboard, "ctrl b", "BroadCastControlPanel.toggle();");
    "".bindCmd(moveMap, keyboard, "ctrl g", "toggleGameMgrHudWin();");
    "".bindCmd(moveMap, keyboard, "ctrl k", "toggleConversationDebug();");
    "".bindCmd(moveMap, keyboard, "ctrl o", "toggleWorldControlPanel();");
    "".bindCmd(moveMap, keyboard, "ctrl t", "toggleDanceTool();");
    "".bindCmd(moveMap, keyboard, "ctrl z", "toggleFreeLook();");
    "".bindCmd(moveMap, keyboard, "ctrl r", "playerTexturesReload();");
    "stopPropAction(0);".bindCmd(moveMap, keyboard, "ctrl enter", "doPropAction(0);");
    "stopPropAction(0);".bindCmd(moveMap, keyboard, "ctrl numpadenter", "doPropAction(0);");
    "stopPropAction(1);".bindCmd(moveMap, keyboard, "ctrl '", "doPropAction(1);");
    "stopPropAction(2);".bindCmd(moveMap, keyboard, "ctrl ;", "doPropAction(2);");
    "".bindCmd(moveMap, keyboard, "alt z", "toggleSystemMessageDialog    ();");
    "".bindCmd(moveMap, keyboard, "alt b", "toggleBenchmarksDialog       ();");
    "".bindCmd(moveMap, keyboard, "alt a", "toggleAdminDialog            ();");
    "".bindCmd(moveMap, keyboard, "alt-shift a", "toggleAnimatorPanel          ();");
    "".bindCmd(moveMap, keyboard, "alt-shift s", "toggleSalonChairControlDialog();");
    "".bindCmd(moveMap, keyboard, "alt f", "toggleBuddyHud    ();");
    "".bindCmd(moveMap, keyboard, "alt m", "toggleMusicHud    ();");
    "".bindCmd(moveMap, keyboard, "ctrl m", "toggleMusicHud    ();");
    "".bindCmd(GlobalActionMap, keyboard, "alt c", "toggleClosetTab   ();");
    "".bindCmd(moveMap, keyboard, "alt s", "toggleOptionsPanel();");
    "".bindCmd(moveMap, keyboard, "alt e", "toggleEmoteHud    ();");
    "".bindCmd(moveMap, keyboard, "alt v", "nextPlayerCamMode ();");
    "".bindCmd(moveMap, keyboard, "alt n", "toggleTGF         ();");
    "".bindCmd(GlobalActionMap, keyboard, "alt F7", "dropPlayerAtCamera();");
    "".bindCmd(GlobalActionMap, keyboard, "alt F8", "dropCameraAtPlayer();");
    if ($ETS::devMode) {
        "".bindCmd(moveMap, keyboard, "ctrl F1", "toggleVisibleState(geActivitiesPanel);");
    }
    "".bindCmd(moveMap, keyboard, "F1", "toggleLocalMap    ();");
    "".bindCmd(moveMap, keyboard, "F2", "toggleTGF         ();");
    "".bindCmd(moveMap, keyboard, "F3", "toggleBuddyHud    ();");
    "".bindCmd(moveMap, keyboard, "F4", "toggleEmoteHud    ();");
    "".bindCmd(moveMap, keyboard, "F5", "toggleClosetGui   ();");
    "".bindCmd(moveMap, keyboard, "F6", "toggleOptionsPanel();");
    "".bindCmd(moveMap, keyboard, "F7", "nextPlayerCamMode ();");
    "".bindCmd(moveMap, keyboard, "alt 1", "oxe_CameraSpeed(\"1\"  );");
    "".bindCmd(moveMap, keyboard, "alt 2", "oxe_CameraSpeed(\"2\"  );");
    "".bindCmd(moveMap, keyboard, "alt 3", "oxe_CameraSpeed(\"3\"  );");
    "".bindCmd(moveMap, keyboard, "alt 4", "oxe_CameraSpeed(\"4\"  );");
    "".bindCmd(moveMap, keyboard, "alt 5", "oxe_CameraSpeed(\"6\"  );");
    "".bindCmd(moveMap, keyboard, "alt 6", "oxe_CameraSpeed(\"8\"  );");
    "".bindCmd(moveMap, keyboard, "alt 7", "oxe_CameraSpeed(\"10\" );");
    "".bindCmd(moveMap, keyboard, "alt 8", "oxe_CameraSpeed(\"20\" );");
    "".bindCmd(moveMap, keyboard, "alt 9", "oxe_CameraSpeed(\"50\" );");
    "".bindCmd(moveMap, keyboard, "alt 0", "oxe_CameraSpeed(\"100\");");
};
function mapMessageKeys() {
    %messageKeys = "a b c d e f g h i j k l m n o p q r s t u v w x y z 0 1 2 3 4 5 6 7 8 9 " @ "A B C D E F G H I J K L M N O P Q R S T U V W X Y Z " @ "exclamation doublequote pound ampersand apostrophe lparen rparen " @ "comma minus period slash colon semicolon lessthan equals morethan lbracket backslash rbracket circumflex underscore " @ "grave tilde vertbar";
    %messageKeys = NextToken(%messageKeys, key, " ");
    while (!(%key $= "")) {
        "".bindCmd(moveMap, keyboard, %key, "startTextEntry();");
        %messageKeys = NextToken(%messageKeys, key, " ");
    }
};
initActionMaps();
mapMessageKeys();
function escapeFromGame() {
    %dragCtrl = Canvas.getDragControl();
    if (isObject(%dragCtrl)) {
        Canvas.releaseDragControl();
        return;
    }
    %topGui = (Canvas.getCount() - 1.0).getObject(Canvas);
    %topName = %topGui.getName();
    if (!(%topName $= "playGui")) {
        if (!(%topName $= "ConsoleDlg")) {
            1.close(%topGui);
        }
        ToggleConsoleReally(1);
        return;
    }
    if ((HudTabs.currentTabIndex >= 0.0)) {
        HudTabs.overrideLockedOpen = 1;
        HudTabs.close();
        return;
    }
    %focused = Canvas.getFirstResponder();
    if (isObject(%focused)) {
        if (!(%focused.escCommand $= "")) {
            eval(%focused.escCommand);
            return;
        }
        if ((%focused == MessageHudEdit.getId())) {
            finishTextEntry();
            return;
        }
    }
    if (!(PlayGui.closeTopClosableWindow())) {
        if (MessageHud.isVisible()) {
            finishTextEntry();
        }
        if (ConvBub.isVisible()) {
            0.close(ConvBub);
        }
        -(1.0).selectConvAtIndex(AIMConvManager);
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
        0.lookUpDown($player);
        0.lookLeftRight($player);
    }
};
function standOrLeaveOrbitModeIfAppropriate() {
    if (($IN_FREEFLY_CAM != 1.0)) {
        SendStandCommand(1);
    }
    if (($IN_ORBIT_CAM == 1.0)) {
        togglePlayerCamMode();
    }
};
function moveleft(%val) {
    if (!($IN_ORBIT_CAM)) {
    }
    if (!($player.isSitting())) {
        setIdle(0);
    }
    $mvLeftAction = (%val * $movementSpeed);
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
    $mvRightAction = (%val * $movementSpeed);
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
        if ((%actionTag[$DoubleTapActionAlreadyDone @ %actionTag] == 0.0)) {
            %actionTag[$DoubleTapActionAlreadyDone @ %actionTag] = 1;
            return 0;
        }
        return 1;
    }
    if (!(%canDoubleTapInCamera)) {
    }
    if (($IN_FREEFLY_CAM == 1.0)) {
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
    if (isObject(BroadcastHideSelfCheckbox)) {
        if (!($IN_ORBIT_CAM)) {
        }
        !($firstPerson).setVisible(BroadcastHideSelfCheckbox);
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
    $mvBackwardAction = (%val * $movementSpeed);
    if (%val) {
        standOrLeaveOrbitModeIfAppropriate();
    }
};
function moveup(%val) {
    $mvUpAction = (%val * $movementSpeed);
};
function movedown(%val) {
    $mvDownAction = (%val * $movementSpeed);
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
    return ((%val * ($cameraFov / 90.0)) * 0.01);
};
function yaw(%val) {
    if (!($IN_ORBIT_CAM)) {
    }
    if (!($player.isSitting())) {
        setIdle(0);
    }
    $mvYaw = ($mvYaw + getMouseAdjustAmount(%val));
};
function pitch(%val) {
    if (!($IN_ORBIT_CAM)) {
    }
    if (!($player.isSitting())) {
        setIdle(0);
    }
    $mvPitch = ($mvPitch + getMouseAdjustAmount(%val));
};
function changeCameraFOV(%val) {
    if (onMouseWheelDifSkus(%val)) {
        return;
    }
    %fov = getFovCur();
    %fov = (%fov - (%val * 0.05));
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
    %val = (%val * -(0.001));
    %val = (%val + 1.0);
    %f = ($cameraDist * %val);
    %f = max(%f, $gCameraDistMin);
    %f = min(%f, $gCameraDistMax);
    $cameraDist = %f;
};
function changeCameraDistAndFOV(%val) {
    %val = (%val * -(0.001));
    %val = (%val + 1.0);
    %dollyMin = $gCameraDistMin;
    %dollyMax = $gCameraDistMax;
    %f = ($cameraDist * %val);
    %f = max(%f, %dollyMin);
    %f = min(%f, %dollyMax);
    %db = $player.getDataBlock();
    %fov = (((1.0 - ((%f - %dollyMin) / (%dollyMax - %dollyMin))) * ((%db.cameraMaxFov * 0.9) - %db.cameraMinFov)) + %db.cameraMinFov);
    $cameraDist = %f;
    setFOV(%fov);
};
function changeCameraFOVFine(%val) {
    changeCameraFOV((%val * 0.3));
};
function changeCameraDistFine(%val) {
    changeCameraDist((%val * 0.3));
};
function changeCameraDistAndFOVFine(%val) {
    changeCameraDistAndFOV((%val * 0.3));
};
function jump(%val) {
    setIdle(0);
    $mvTriggerCount2 = ($mvTriggerCount2 + 1.0);
};
function jumpOnce() {
    jump();
    jump();
};
function doPropAction(%actionNum) {
    if (!(isDefined("%actionNum"))) {
        %actionNum = 0;
    }
    if (ClosetGui.isVisible()) {
        if (!(ClosetGui.isDoingPropAction)) {
            ClosetGui.isDoingPropAction = 1;
            %propAnimation = %actionNum.getPropAnimationFromSkus($player, $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName]);
            if ((%propAnimation $= "")) {
                ClosetGui.isDoingPropAction = 0;
            }
            %propAnimation.playAnim($player);
        }
    }
    commandToServer('DoPropAction', %actionNum);
};
function stopPropAction() {
    if (ClosetGui.isVisible()) {
        if (ClosetGui.isDoingPropAction) {
            ClosetGui.isDoingPropAction = 0;
            %anim = $player.getGender() @ $player.getGenre() @ "idl1b";
            %anim.playAnim($player);
        }
    }
    commandToServer('StopPropAction');
};
function mouseFire(%val) {
    $mvTriggerCount0 = ($mvTriggerCount0 + 1.0);
};
function altTrigger(%val) {
    $mvTriggerCount1 = ($mvTriggerCount1 + 1.0);
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
    $cameraFOVAdjustment = (100.0 * %amt);
    zoomTick();
};
function startZoomOut(%amt) {
    if (!(isDefined("%amt"))) {
        %amt = 1.0;
    }
    $cameraFOVAdjustment = (-(100.0) * %amt);
    zoomTick();
};
function startDollyIn(%amt) {
    if (!(isDefined("%amt"))) {
        %amt = 1.0;
    }
    $cameraDistAdjustment = (100.0 * %amt);
    zoomTick();
};
function startDollyOut(%amt) {
    if (!(isDefined("%amt"))) {
        %amt = 1.0;
    }
    $cameraDistAdjustment = (-(100.0) * %amt);
    zoomTick();
};
function startDollyZoomIn(%amt) {
    if (!(isDefined("%amt"))) {
        %amt = 1.0;
    }
    $cameraFOVAdjustment = (100.0 * %amt);
    $cameraDistAdjustment = 1;
    zoomTick();
};
function startDollyZoomOut(%amt) {
    if (!(isDefined("%amt"))) {
        %amt = 1.0;
    }
    $cameraFOVAdjustment = (-(100.0) * %amt);
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
    if ((($cameraFOVAdjustment * $cameraDistAdjustment) != 0.0)) {
        changeCameraDistAndFOV($cameraFOVAdjustment);
    }
    changeCameraFOV($cameraFOVAdjustment);
    changeCameraDist($cameraDistAdjustment);
    if (($cameraFOVAdjustment != 0.0)) {
    }
    if (($cameraDistAdjustment != 0.0)) {
        $cameraFOVTimer = schedule(25, 0, "zoomTick");
    }
};
function buttonBarMenuLogout() {
    logout(0);
    WorldMap.exit();
};
function toggleVisibleState(%this) {
    if (%this.isVisible()) {
        0.close(%this);
    }
    %this.open();
};
safeEnsureScriptObject("StringMap", "CSPanelCategories");
"settings".put(CSPanelCategories, "CSMediaDisplay");
"settings".put(CSPanelCategories, "CSRulesAndDescWindow");
"furniture".put(CSPanelCategories, "CSFurnitureMover");
"furniture".put(CSPanelCategories, "CSInventoryBrowserWindow");
"furniture".put(CSPanelCategories, "CSShoppingBrowserWindow");
"painting".put(CSPanelCategories, "CSPaintingWindow");
function closeCSPanelsInOtherCategories(%panel) {
    if (!(isObject(%panel))) {
        return;
    }
    %category = %panel.getName().get(CSPanelCategories);
    %size = CSPanelCategories.size();
    %i = 0;
    while ((%i < %size)) {
        if (!(%i.getValue(CSPanelCategories) $= %category)) {
            %i.getKey(CSPanelCategories).close();
        }
        %i = (%i + 1.0);
    }
};
function numCSPanelsOpen() {
    %num = 0;
    %size = CSPanelCategories.size();
    %i = 0;
    while ((%i < %size)) {
        if (%i.getKey(CSPanelCategories).isVisible()) {
            %num = (%num + 1.0);
        }
        %i = (%i + 1.0);
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
    toggleVisibleState(BuddyHudWin);
};
function toggleBuddyHudForTab(%tabName) {
    %wasOpen = 1;
    if (!(BuddyHudWin.visible)) {
        %wasOpen = 0;
        BuddyHudWin.open();
    }
    %currentTabName = BuddyHudTabs.getCurrentTab().name;
    if (%wasOpen) {
    }
    if ((%tabName $= %currentTabName)) {
        BuddyHudWin.close();
        return;
    }
    %tabName.selectTabWithName(BuddyHudTabs);
};
function toggleSelfViewHud() {
    PlayerWin.showRaiseOrHide(PlayGui);
    if (!(PlayerWin.isVisible())) {
    }
    if ($IN_ORBIT_CAM) {
        togglePlayerCamMode();
    }
};
function toggleEmoteHud() {
    toggleVisibleState(EmoteHudWin);
};
function toggleMusicHud() {
    if (MusicHud.isShowing()) {
        MusicHud.hide();
    }
    MusicHud.show();
    1.keepOpen(MusicHud);
};
function toggleOptionsPanel() {
    toggleVisibleState(OptionsPanel);
};
function okToOpenClosetGui() {
    if (isObject(ApplauseMeterGui) && (ApplauseMeterGui.applauseMeterUse $= "sumo")) {
        if ((ApplauseMeterGui.sumoGameType $= "PillowFightGame")) {
            MessageBoxOK($MsgCat::applauseGui["MSG-PILLOW-WARN"], $MsgCat::applauseGui["MSG-PILLOW-USER-NO-OPEN-CLOSET"], "");
        }
        MessageBoxOK($MsgCat::applauseGui["MSG-SUMO-WARN"], $MsgCat::applauseGui["MSG-SUMO-USER-NO-OPEN-CLOSET"], "");
        return 0;
    }
    return 1;
};
function toggleWardrobe() {
    if (!(okToOpenClosetGui())) {
        return;
    }
    toggleVisibleState(wardrobeGui);
};
function toggleBodyTab() {
    if (!(okToOpenClosetGui())) {
        return;
    }
    toggleVisibleState(ClosetGui);
    "Body".selectTabWithName(ClosetTabs);
};
function toggleClosetTab() {
    if (!(okToOpenClosetGui())) {
        return;
    }
    toggleVisibleState(ClosetGui);
    "Closet".selectTabWithName(ClosetTabs);
};
function toggleClosetGui() {
    scriptProfiler_EnterScope();
    if (!(okToOpenClosetGui())) {
        return;
    }
    toggleVisibleState(ClosetGui);
    if (ClosetGui.isVisible()) {
        if (($gCurrentStoreName $= "")) {
            %tabToOpen = ClosetGui.lastTabOpened;
            if ((%tabToOpen $= "")) {
                %tabToOpen = "Closet";
            }
            %tabToOpen.selectTabWithName(ClosetTabs);
        }
        "Shops".selectTabWithName(ClosetTabs);
    }
    scriptProfiler_LeaveScope();
};
function refreshCSSelector() {
    if (CustomSpacesSelectorContainer.isVisible()) {
        CustomSpacesSelector.refresh();
    }
};
function toggleBuildingDirectory() {
    if (CustomSpacesSelectorContainer.visible) {
        CustomSpacesSelector.close();
    }
    if (!(BuildingDirectoryButton.lastBuildingEntered $= "")) {
        BuildingDirectoryButton.lastBuildingEntered.open(CustomSpacesSelector);
    }
};
function toggleClosetItemCategory(%category) {
    if (!(okToOpenClosetGui())) {
        return;
    }
    toggleVisibleState(ClosetGui);
    if (ClosetGui.visible) {
        "CLOSET".selectTabWithName(ClosetTabs);
        %category.onSelect(ClosetItemPopup, 0);
    }
};
function toggleStore() {
    if (!(okToOpenClosetGui())) {
        return;
    }
    if (($gCurrentStoreName $= "")) {
    }
    toggleVisibleState(ClosetGui);
    if (ClosetGui.visible) {
        "SHOPS".selectTabWithName(ClosetTabs);
    }
};
function toggleSnapshot() {
    if (!(okToOpenClosetGui())) {
        return;
    }
    toggleVisibleState(snapshotTool);
};
function toggleAIMHud() {
    toggleVisibleState(BuddyHudWin);
    if (BuddyHudWin.isVisible()) {
        "AIM".selectTabWithName(BuddyHudTabs);
    }
};
function toggleWorldControlPanel() {
    worldControlPanel.showRaiseOrHide(PlayGui);
};
function toggleDancePad() {
    DancePadGui.showRaiseOrHide(PlayGui);
};
function toggleBoneBlendGui() {
    if ($player.isDebugging()) {
        boneBlendGui.showRaiseOrHide(PlayGui);
    }
};
function toggleTGF() {
    if ((Canvas.getContent().getId() != geTGF.getId())) {
    }
    if ((geTGF.getParent().getId() != PlayGui.getId())) {
        0.setVisible(geTGF);
    }
    toggleVisibleState(geTGF);
};
function toggleWorldMap() {
    "".Maps_filterDestinationsByType(geTGF_tabs);
    "Map".toggleToTabName(geTGF);
    "multi_city".setView(WorldMap);
};
function toggleCityMap() {
    "Map".toggleToTabName(geTGF);
    "".Maps_filterDestinationsByType(geTGF_tabs);
    if (($CSBuildingInfo != 0.0)) {
        $CSBuildingInfo.city.selectCity(WorldMap);
    }
    $gContiguousSpaceName.selectCity(WorldMap);
};
function toggleTGFMapFiltered(%filterType) {
    toggleCityMap();
    %filterType.Maps_filterDestinationsByType(geTGF_tabs);
};
function togglePerformerPanel() {
    performerPanel.toggle();
};
function toggleLocalMap() {
    toggleVisibleState(geLocalMapContainer);
};
function toggleCameraImgBroadcast() {
    BroadCastControlPanel.toggle();
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
    $Camera::movementSpeed = (0.5 + ((%val - 1.0) * 1.0));
};
$MFDebugRenderMode = 0;
function cycleDebugRenderMode() {
    if (!("debugPassive".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    if ((getBuildString() $= "Debug")) {
        if (($MFDebugRenderMode == 0.0)) {
            $MFDebugRenderMode = 1;
            GLEnableOutline(1);
        }
        if (($MFDebugRenderMode == 1.0)) {
            $MFDebugRenderMode = 2;
            GLEnableOutline(0);
            setInteriorRenderMode(7);
            showInterior();
        }
        if (($MFDebugRenderMode == 2.0)) {
            $MFDebugRenderMode = 0;
            setInteriorRenderMode(0);
            GLEnableOutline(0);
            show();
        }
    }
    echo("Debug render modes only available when running a Debug build.");
};
ToggleConsoleReally.bind(GlobalActionMap, keyboard, "alt tilde");
ToggleConsoleReally.bind(GlobalActionMap, keyboard, "ctrl capslock");
"".bindCmd(GlobalActionMap, keyboard, "alt F9", "cycleDebugRenderMode();");
"escapeFromGame();".bindCmd(GlobalActionMap, keyboard, "escape", "");
"".bindCmd(GlobalActionMap, keyboard, "alt F4", "");
if (($Platform $= "macos")) {
    "onDragAndDropCtrl".bind(GlobalActionMap, keyboard, "alt");
}
"onDragAndDropCtrl".bind(GlobalActionMap, keyboard, "lcontrol");
"onDragAndDropCtrl".bind(GlobalActionMap, keyboard, "rcontrol");
"".bindCmd(functionMap, keyboard, "F8", "EmoteHudList.doFunc(\"F08\"   );");
"".bindCmd(functionMap, keyboard, "F9", "EmoteHudList.doFunc(\"F09\"   );");
"".bindCmd(functionMap, keyboard, "F10", "EmoteHudList.doFunc(\"F10\"   );");
"".bindCmd(functionMap, keyboard, "F11", "EmoteHudList.doFunc(\"F11\"   );");
"".bindCmd(functionMap, keyboard, "F12", "EmoteHudList.doFunc(\"F12\"   );");
"".bindCmd(functionMap, keyboard, "ctrl 1", "EmoteHudList.doFunc(\"ctrl1\");");
"".bindCmd(functionMap, keyboard, "ctrl 2", "EmoteHudList.doFunc(\"ctrl2\");");
"".bindCmd(functionMap, keyboard, "ctrl 3", "EmoteHudList.doFunc(\"ctrl3\");");
"".bindCmd(functionMap, keyboard, "ctrl 4", "EmoteHudList.doFunc(\"ctrl4\");");
"".bindCmd(functionMap, keyboard, "ctrl 5", "EmoteHudList.doFunc(\"ctrl5\");");
"".bindCmd(functionMap, keyboard, "ctrl 6", "EmoteHudList.doFunc(\"ctrl6\");");
"".bindCmd(functionMap, keyboard, "ctrl 7", "EmoteHudList.doFunc(\"ctrl7\");");
"".bindCmd(functionMap, keyboard, "ctrl 8", "EmoteHudList.doFunc(\"ctrl8\");");
"".bindCmd(functionMap, keyboard, "ctrl 9", "EmoteHudList.doFunc(\"ctrl9\");");
"".bindCmd(functionMap, keyboard, "ctrl 0", "EmoteHudList.doFunc(\"ctrl0\");");
toggleFirstPerson(1);
new ActionMap(buildingDirectoryMap);
"".bindCmd(buildingDirectoryMap, keyboard, "F5", "refreshCSSelector();");
"".bindCmd(buildingDirectoryMap, keyboard, "enter", "CustomSpacesSelector.doOnKeyDown(\"enter\");");
"CustomSpacesSelector.doOnKeyUp(\"up\");".bindCmd(buildingDirectoryMap, keyboard, "up", "CustomSpacesSelector.doOnKeyDown(\"up\");");
"CustomSpacesSelector.doOnKeyUp(\"down\");".bindCmd(buildingDirectoryMap, keyboard, "down", "CustomSpacesSelector.doOnKeyDown(\"down\");");
new ActionMap(closetMap);
"".bindCmd(closetMap, keyboard, "F5", "toggleClosetGui();");
"".bindCmd(closetMap, keyboard, "left", "ClosetGui.doArrow(-1, 0);");
"".bindCmd(closetMap, keyboard, "right", "ClosetGui.doArrow( 1, 0);");
"".bindCmd(closetMap, keyboard, "up", "ClosetGui.doArrow( 0, 1);");
"".bindCmd(closetMap, keyboard, "down", "ClosetGui.doArrow( 0,-1);");
"".bindCmd(closetMap, keyboard, "alt n", "");
"".bindCmd(closetMap, keyboard, "F2", "");
"".bindCmd(closetMap, keyboard, "ctrl r", "ClosetGUI_RefreshTextures();");
new ActionMap(optionsMap);
"".bindCmd(optionsMap, keyboard, "F6", "toggleOptionsPanel();");
new ActionMap(tgfMapMap);
"".bindCmd(tgfMapMap, keyboard, "alt n", "toggleTGF();");
"".bindCmd(tgfMapMap, keyboard, "F2", "geTGF.closeFully();");
"".bindCmd(tgfMapMap, keyboard, "F5", "geTGF.onRefresh();");
new ActionMap(csFurnitureMap);
"".bindCmd(csFurnitureMap, keyboard, "delete", "csTestFreeSelectedItem();");
if (($Platform $= "macos")) {
    "".bindCmd(csFurnitureMap, keyboard, "backspace", "csTestFreeSelectedItem();");
    "".bindCmd(csFurnitureMap, keyboard, "delete", "csTestFreeSelectedItem();");
    "".bindCmd(csFurnitureMap, keyboard, "opt x", "CSFurnitureMover.doCut();");
    "".bindCmd(csFurnitureMap, keyboard, "opt c", "CSFurnitureMover.doCopy();");
    "".bindCmd(csFurnitureMap, keyboard, "opt v", "CSFurnitureMover.doPaste();");
}
"".bindCmd(csFurnitureMap, keyboard, "delete", "csTestFreeSelectedItem();");
"".bindCmd(csFurnitureMap, keyboard, "ctrl x", "CSFurnitureMover.doCut();");
"".bindCmd(csFurnitureMap, keyboard, "ctrl c", "CSFurnitureMover.doCopy();");
"".bindCmd(csFurnitureMap, keyboard, "ctrl insert", "CSFurnitureMover.doCopy();");
"".bindCmd(csFurnitureMap, keyboard, "ctrl v", "CSFurnitureMover.doPaste();");
"".bindCmd(csFurnitureMap, keyboard, "shift insert", "CSFurnitureMover.doPaste();");
