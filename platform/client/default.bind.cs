function initActionMaps()
{
    if (isObject(moveMap))
    {
        moveMap.delete();
    }
    new ActionMap(moveMap);
    new ActionMap(functionMap);
    moveMap.bind(mouse, "xaxis", yaw);
    moveMap.bind(mouse, "yaxis", pitch);
    moveMap.bind(mouse, "zaxis", changeCameraDist);
    moveMap.bind(mouse, "shift zaxis", changeCameraDistFine);
    moveMap.bind(mouse, "ctrl zaxis", changeCameraFOV);
    moveMap.bind(mouse, "ctrl-shift zaxis", changeCameraFOVFine);
    moveMap.bind(mouse, "alt zaxis", changeCameraDistAndFOV);
    moveMap.bind(mouse, "shift-alt zaxis", changeCameraDistAndFOVFine);
    moveMap.bind(mouse, button0, mouseFire);
    moveMap.bind(keyboard, "ctrl space", onActionKey);
    moveMap.bind(keyboard, "space", onThrowBall);
    moveMap.bind(keyboard, "enter", onActionKey);
    moveMap.bind(keyboard, "numpadenter", onActionKey);
    moveMap.bind(keyboard, "up", moveforward);
    moveMap.bind(keyboard, "shift up", moveforwardFast);
    moveMap.bind(keyboard, "rshift", moveFaster);
    moveMap.bind(keyboard, "lshift", moveFaster);
    moveMap.bind(keyboard, "down", movebackward);
    moveMap.bind(keyboard, "right", turnRight);
    moveMap.bind(keyboard, "left", turnLeft);
    moveMap.bind(keyboard, "ctrl w", moveforward);
    moveMap.bind(keyboard, "ctrl-shift w", moveforwardFast);
    moveMap.bind(keyboard, "ctrl s", movebackward);
    moveMap.bind(keyboard, "ctrl a", turnLeft);
    moveMap.bind(keyboard, "ctrl d", turnRight);
    moveMap.bind(keyboard, "ctrl up", panUp);
    moveMap.bind(keyboard, "ctrl down", panDown);
    moveMap.bind(keyboard, "ctrl left", moveleft);
    moveMap.bind(keyboard, "ctrl right", moveright);
    moveMap.bind(keyboard, "ctrl x", toggleZoom);
    moveMap.bindCmd(keyboard, "ctrl =", "startDollyIn();", "stopZoom();");
    moveMap.bindCmd(keyboard, "ctrl-shift =", "startDollyIn(0.3);", "stopZoom();");
    moveMap.bindCmd(keyboard, "ctrl -", "startDollyOut();", "stopZoom();");
    moveMap.bindCmd(keyboard, "ctrl-shift -", "startDollyOut(0.3);", "stopZoom();");
    moveMap.bindCmd(keyboard, "ctrl numpadadd", "startDollyIn();", "stopZoom();");
    moveMap.bindCmd(keyboard, "ctrl-shift numpadadd", "startDollyIn(0.3);", "stopZoom();");
    moveMap.bindCmd(keyboard, "ctrl numpadminus", "startDollyOut();", "stopZoom();");
    moveMap.bindCmd(keyboard, "ctrl-shift numpadminus", "startDollyOut(0.3);", "stopZoom();");
    moveMap.bindCmd(keyboard, "ctrl-alt numpadadd", "startDollyZoomIn();", "stopZoom();");
    moveMap.bindCmd(keyboard, "shift-ctrl-alt numpadadd", "startDollyZoomIn(0.3);", "stopZoom();");
    moveMap.bindCmd(keyboard, "ctrl-alt numpadminus", "startDollyZoomOut();", "stopZoom();");
    moveMap.bindCmd(keyboard, "shift-ctrl-alt numpadminus", "startDollyZoomOut(0.3);", "stopZoom();");
    moveMap.bindCmd(keyboard, "alt =", "startZoomIn();", "stopZoom();");
    moveMap.bindCmd(keyboard, "shift-alt =", "startZoomIn(0.3);", "stopZoom();");
    moveMap.bindCmd(keyboard, "alt -", "startZoomOut();", "stopZoom();");
    moveMap.bindCmd(keyboard, "shift-alt -", "startZoomOut(0.3);", "stopZoom();");
    moveMap.bindCmd(keyboard, "ctrl-alt =", "startDollyZoomIn();", "stopZoom();");
    moveMap.bindCmd(keyboard, "shift-ctrl-alt =", "startDollyZoomIn(0.3);", "stopZoom();");
    moveMap.bindCmd(keyboard, "ctrl-alt -", "startDollyZoomOut();", "stopZoom();");
    moveMap.bindCmd(keyboard, "shift-ctrl-alt -", "startDollyZoomOut(0.3);", "stopZoom();");
    moveMap.bindCmd(keyboard, "ctrl n", "TutorialsCatalogClient::forceNextNag();", "");
    moveMap.bindCmd(keyboard, "ctrl tab", "nextPlayerCamMode();", "");
    moveMap.bindCmd(keyboard, "ctrl b", "BroadCastControlPanel.toggle();", "");
    moveMap.bindCmd(keyboard, "ctrl g", "toggleGameMgrHudWin();", "");
    moveMap.bindCmd(keyboard, "ctrl k", "toggleConversationDebug();", "");
    moveMap.bindCmd(keyboard, "ctrl o", "toggleWorldControlPanel();", "");
    moveMap.bindCmd(keyboard, "ctrl t", "toggleDanceTool();", "");
    moveMap.bindCmd(keyboard, "ctrl z", "toggleFreeLook();", "");
    moveMap.bindCmd(keyboard, "ctrl r", "playerTexturesReload();", "");
    moveMap.bindCmd(keyboard, "ctrl enter", "doPropAction(0);", "stopPropAction(0);");
    moveMap.bindCmd(keyboard, "ctrl numpadenter", "doPropAction(0);", "stopPropAction(0);");
    moveMap.bindCmd(keyboard, "ctrl \'", "doPropAction(1);", "stopPropAction(1);");
    moveMap.bindCmd(keyboard, "ctrl ;", "doPropAction(2);", "stopPropAction(2);");
    moveMap.bindCmd(keyboard, "alt z", "toggleSystemMessageDialog    ();", "");
    moveMap.bindCmd(keyboard, "alt b", "toggleBenchmarksDialog       ();", "");
    moveMap.bindCmd(keyboard, "alt a", "toggleAdminDialog            ();", "");
    moveMap.bindCmd(keyboard, "alt-shift a", "toggleAnimatorPanel          ();", "");
    moveMap.bindCmd(keyboard, "alt-shift s", "toggleSalonChairControlDialog();", "");
    moveMap.bindCmd(keyboard, "alt f", "toggleBuddyHud    ();", "");
    moveMap.bindCmd(keyboard, "alt m", "toggleMusicHud    ();", "");
    moveMap.bindCmd(keyboard, "ctrl m", "toggleMusicHud    ();", "");
    GlobalActionMap.bindCmd(keyboard, "alt c", "toggleClosetTab   ();", "");
    moveMap.bindCmd(keyboard, "alt s", "toggleOptionsPanel();", "");
    moveMap.bindCmd(keyboard, "alt e", "toggleEmoteHud    ();", "");
    moveMap.bindCmd(keyboard, "alt v", "nextPlayerCamMode ();", "");
    moveMap.bindCmd(keyboard, "alt n", "toggleTGF         ();", "");
    GlobalActionMap.bindCmd(keyboard, "alt F7", "dropPlayerAtCamera();", "");
    GlobalActionMap.bindCmd(keyboard, "alt F8", "dropCameraAtPlayer();", "");
    if ($ETS::devMode)
    {
        moveMap.bindCmd(keyboard, "ctrl F1", "toggleVisibleState(geActivitiesPanel);", "");
    }
    moveMap.bindCmd(keyboard, "F1", "toggleLocalMap    ();", "");
    moveMap.bindCmd(keyboard, "F2", "toggleTGF         ();", "");
    moveMap.bindCmd(keyboard, "F3", "toggleBuddyHud    ();", "");
    moveMap.bindCmd(keyboard, "F4", "toggleEmoteHud    ();", "");
    moveMap.bindCmd(keyboard, "F5", "toggleClosetGui   ();", "");
    moveMap.bindCmd(keyboard, "F6", "toggleOptionsPanel();", "");
    moveMap.bindCmd(keyboard, "F7", "nextPlayerCamMode ();", "");
    moveMap.bindCmd(keyboard, "alt 1", "oxe_CameraSpeed(\"1\"  );", "");
    moveMap.bindCmd(keyboard, "alt 2", "oxe_CameraSpeed(\"2\"  );", "");
    moveMap.bindCmd(keyboard, "alt 3", "oxe_CameraSpeed(\"3\"  );", "");
    moveMap.bindCmd(keyboard, "alt 4", "oxe_CameraSpeed(\"4\"  );", "");
    moveMap.bindCmd(keyboard, "alt 5", "oxe_CameraSpeed(\"6\"  );", "");
    moveMap.bindCmd(keyboard, "alt 6", "oxe_CameraSpeed(\"8\"  );", "");
    moveMap.bindCmd(keyboard, "alt 7", "oxe_CameraSpeed(\"10\" );", "");
    moveMap.bindCmd(keyboard, "alt 8", "oxe_CameraSpeed(\"20\" );", "");
    moveMap.bindCmd(keyboard, "alt 9", "oxe_CameraSpeed(\"50\" );", "");
    moveMap.bindCmd(keyboard, "alt 0", "oxe_CameraSpeed(\"100\");", "");
    return ;
}
function mapMessageKeys()
{
    %messageKeys = "a b c d e f g h i j k l m n o p q r s t u v w x y z 0 1 2 3 4 5 6 7 8 9 " @ "A B C D E F G H I J K L M N O P Q R S T U V W X Y Z " @ "exclamation doublequote pound ampersand apostrophe lparen rparen " @ "comma minus period slash colon semicolon lessthan equals morethan lbracket backslash rbracket circumflex underscore " @ "grave tilde vertbar";
    %messageKeys = NextToken(%messageKeys, key, " ");
    while (!(%key $= ""))
    {
        moveMap.bindCmd(keyboard, %key, "startTextEntry();", "");
        %messageKeys = NextToken(%messageKeys, key, " ");
    }
}

initActionMaps();
mapMessageKeys();
function escapeFromGame()
{
    %dragCtrl = Canvas.getDragControl();
    if (isObject(%dragCtrl))
    {
        Canvas.releaseDragControl();
        return ;
    }
    %topGui = Canvas.getObject(Canvas.getCount() - 1);
    %topName = %topGui.getName();
    if (!(%topName $= "playGui"))
    {
        if (!(%topName $= "ConsoleDlg"))
        {
            %topGui.close(1);
        }
        else
        {
            ToggleConsoleReally(1);
        }
        return ;
    }
    if (HudTabs.currentTabIndex >= 0)
    {
        HudTabs.overrideLockedOpen = 1;
        HudTabs.close();
        return ;
    }
    %focused = Canvas.getFirstResponder();
    if (isObject(%focused))
    {
        if (!(%focused.escCommand $= ""))
        {
            eval(%focused.escCommand);
            return ;
        }
        if (%focused == MessageHudEdit.getId())
        {
            finishTextEntry();
            return ;
        }
    }
    if (!PlayGui.closeTopClosableWindow())
    {
        if (MessageHud.isVisible())
        {
            finishTextEntry();
        }
        else
        {
            if (ConvBub.isVisible())
            {
                ConvBub.close(0);
            }
            else
            {
                AIMConvManager.selectConvAtIndex(-1);
            }
        }
    }
    return ;
}
$movementSpeed = 1;
function setSpeed(%speed)
{
    if (%speed)
    {
        $movementSpeed = %speed;
    }
    return ;
}
function stopMoving(%val)
{
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
    if (isObject($player))
    {
        $player.lookUpDown(0);
        $player.lookLeftRight(0);
    }
    return ;
}
function standOrLeaveOrbitModeIfAppropriate()
{
    if ($IN_FREEFLY_CAM != 1)
    {
        SendStandCommand(1);
    }
    if ($IN_ORBIT_CAM == 1)
    {
        togglePlayerCamMode();
    }
    return ;
}
function moveleft(%val)
{
    if (!$IN_ORBIT_CAM && !$player.isSitting())
    {
        setIdle(0);
    }
    $mvLeftAction = %val * $movementSpeed;
    if (%val)
    {
        standOrLeaveOrbitModeIfAppropriate();
    }
    return ;
}
function moveright(%val)
{
    if (!$IN_ORBIT_CAM && !$player.isSitting())
    {
        setIdle(0);
    }
    $mvRightAction = %val * $movementSpeed;
    if (%val)
    {
        standOrLeaveOrbitModeIfAppropriate();
    }
    return ;
}
$IN_FREEFLY_CAM = 0;
function clientCmdSetInFreeflyCam(%val)
{
    $IN_FREEFLY_CAM = %val;
    return ;
}
function moveforwardFast(%val)
{
    if (%val)
    {
        $mvForwardAction = $movementSpeed;
        commandToServer('goForwardAtFasterRate');
        commandToServer('goForwardAtFasterRate');
    }
    else
    {
        $mvForwardAction = 0;
    }
    return ;
}
function moveFaster(%val)
{
    if (!%val)
    {
        return ;
    }
    commandToServer('goFaster');
    return ;
}
function doubleTapActionStop(%actionTag)
{
    if ($DoubleTapStopTimer[%actionTag])
    {
        cancel($DoubleTapStopTimer[%actionTag]);
        $DoubleTapStopTimer[%actionTag] = 0;
    }
    $DoubleTapActionAlreadyDone[%actionTag] = 0;
    return ;
}
function doubleTapDeclareActionVariable(%actionTag)
{
    $DoubleTapStopTimer[%actionTag] = 0;
    $DoubleTapActionAlreadyDone[%actionTag] = 0;
    return ;
}
function doubleTapCheckOnAction(%actionTag, %keyDown, %canDoubleTapInCamera, %resetDelayMS)
{
    if (%keyDown)
    {
        if ($DoubleTapStopTimer[%actionTag])
        {
            cancel($DoubleTapStopTimer[%actionTag]);
            $DoubleTapStopTimer[%actionTag] = 0;
        }
        if ($DoubleTapActionAlreadyDone[%actionTag] == 0)
        {
            $DoubleTapActionAlreadyDone[%actionTag] = 1;
            return 0;
        }
        else
        {
            return 1;
        }
    }
    else
    {
        if (!%canDoubleTapInCamera && ($IN_FREEFLY_CAM == 1))
        {
            doubleTapActionStop(%actionTag);
        }
        else
        {
            $DoubleTapStopTimer[%actionTag] = schedule(%resetDelayMS, 0, "doubleTapActionStop", %actionTag) ;
        }
    }
    return ;
}
doubleTapDeclareActionVariable("forward");
doubleTapDeclareActionVariable("left");
doubleTapDeclareActionVariable("right");
function moveforward(%val)
{
    setIdle(0);
    %doubleTap = doubleTapCheckOnAction("forward", %val, 0, 250);
    if (%val)
    {
        if (!%doubleTap)
        {
            $mvForwardAction = $movementSpeed;
            standOrLeaveOrbitModeIfAppropriate();
        }
        else
        {
            $mvForwardAction = $movementSpeed;
            commandToServer('goForwardAtFasterRate');
        }
    }
    else
    {
        $mvForwardAction = 0;
    }
    return ;
}
$IN_ORBIT_CAM = 0;
function togglePlayerCamMode()
{
    commandToServer('nextCamMode');
    $IN_ORBIT_CAM = !$IN_ORBIT_CAM;
    return ;
}
function nextPlayerCamMode()
{
    if ($IN_ORBIT_CAM)
    {
        toggleFirstPerson();
        togglePlayerCamMode();
    }
    else
    {
        if ($firstPerson)
        {
            toggleFirstPerson();
        }
        else
        {
            togglePlayerCamMode();
        }
    }
    if (isObject(BroadcastHideSelfCheckbox))
    {
        BroadcastHideSelfCheckbox.setVisible(!$IN_ORBIT_CAM && !$firstPerson);
    }
    return ;
}
function ClientCmdOnOrbitMode(%orbitMode)
{
    $IN_ORBIT_CAM = %orbitMode;
    return ;
}
function movebackward(%val)
{
    if (!$IN_ORBIT_CAM && !$player.isSitting())
    {
        setIdle(0);
    }
    $mvBackwardAction = %val * $movementSpeed;
    if (%val)
    {
        standOrLeaveOrbitModeIfAppropriate();
    }
    return ;
}
function moveup(%val)
{
    $mvUpAction = %val * $movementSpeed;
    return ;
}
function movedown(%val)
{
    $mvDownAction = %val * $movementSpeed;
    return ;
}
function turnLeft(%val)
{
    %doubleTap = doubleTapCheckOnAction("left", %val, 0, 250);
    if (!$IN_ORBIT_CAM && !$player.isSitting())
    {
        setIdle(0);
    }
    $mvYawRightSpeed = %val ? $Pref::Input::KeyboardTurnSpeed : 0;
    $mvYawRightSpeedBase = $mvYawRightSpeed;
    return ;
}
function turnRight(%val)
{
    %doubleTap = doubleTapCheckOnAction("right", %val, 0, 250);
    if (!$IN_ORBIT_CAM && !$player.isSitting())
    {
        setIdle(0);
    }
    $mvYawLeftSpeed = %val ? $Pref::Input::KeyboardTurnSpeed : 0;
    $mvYawLeftSpeedBase = $mvYawLeftSpeed;
    return ;
}
function panUp(%val)
{
    if (!$IN_ORBIT_CAM && !$player.isSitting())
    {
        setIdle(0);
    }
    $mvPitchDownSpeed = %val ? $Pref::Input::KeyboardTurnSpeed : 0;
    return ;
}
function panDown(%val)
{
    if (!$IN_ORBIT_CAM && !$player.isSitting())
    {
        setIdle(0);
    }
    $mvPitchUpSpeed = %val ? $Pref::Input::KeyboardTurnSpeed : 0;
    return ;
}
function getMouseAdjustAmount(%val)
{
    return (%val * ($cameraFov / 90)) * 0.01;
}
function yaw(%val)
{
    if (!$IN_ORBIT_CAM && !$player.isSitting())
    {
        setIdle(0);
    }
    $mvYaw = $mvYaw + getMouseAdjustAmount(%val);
    return ;
}
function pitch(%val)
{
    if (!$IN_ORBIT_CAM && !$player.isSitting())
    {
        setIdle(0);
    }
    $mvPitch = $mvPitch + getMouseAdjustAmount(%val);
    return ;
}
function changeCameraFOV(%val)
{
    if (onMouseWheelDifSkus(%val))
    {
        return ;
    }
    %fov = getFovCur();
    %fov = %fov - (%val * 0.05);
    setFOV(%fov);
    $Pref::Player::CurrentFOV = %fov;
    $UserPref::Player::DefaultFOV = %fov;
    return ;
}
$gCameraDistMin = 0.5;
$gCameraDistStartFaceZoom = 2;
$gCameraDistMax = 4;
function changeCameraDist(%val)
{
    if ($firstPerson && !$GameConnection.isPresentAtBody())
    {
        changeCameraFOV(%val);
        return ;
    }
    %val = %val * -0.001;
    %val = %val + 1;
    %f = $cameraDist * %val;
    %f = max(%f, $gCameraDistMin);
    %f = min(%f, $gCameraDistMax);
    $cameraDist = %f;
    return ;
}
function changeCameraDistAndFOV(%val)
{
    %val = %val * -0.001;
    %val = %val + 1;
    %dollyMin = $gCameraDistMin;
    %dollyMax = $gCameraDistMax;
    %f = $cameraDist * %val;
    %f = max(%f, %dollyMin);
    %f = min(%f, %dollyMax);
    %db = $player.getDataBlock();
    %fov = ((1 - ((%f - %dollyMin) / (%dollyMax - %dollyMin))) * ((%db.cameraMaxFov * 0.9) - %db.cameraMinFov)) + %db.cameraMinFov;
    $cameraDist = %f;
    setFOV(%fov);
    return ;
}
function changeCameraFOVFine(%val)
{
    changeCameraFOV(%val * 0.3);
    return ;
}
function changeCameraDistFine(%val)
{
    changeCameraDist(%val * 0.3);
    return ;
}
function changeCameraDistAndFOVFine(%val)
{
    changeCameraDistAndFOV(%val * 0.3);
    return ;
}
function jump(%val)
{
    setIdle(0);
    $mvTriggerCount2 = $mvTriggerCount2 + 1;
    return ;
}
function jumpOnce()
{
    jump();
    jump();
    return ;
}
function doPropAction(%actionNum)
{
    if (!isDefined("%actionNum"))
    {
        %actionNum = 0;
    }
    if (ClosetGui.isVisible())
    {
        if (!ClosetGui.isDoingPropAction)
        {
            ClosetGui.isDoingPropAction = 1;
            %propAnimation = $player.getPropAnimationFromSkus($ClosetSkusOutfit[$ClosetOutfitName], %actionNum);
            if (%propAnimation $= "")
            {
                ClosetGui.isDoingPropAction = 0;
            }
            else
            {
                $player.playAnim(%propAnimation);
            }
        }
    }
    else
    {
        commandToServer('DoPropAction', %actionNum);
    }
    return ;
}
function stopPropAction()
{
    if (ClosetGui.isVisible())
    {
        if (ClosetGui.isDoingPropAction)
        {
            ClosetGui.isDoingPropAction = 0;
            %anim = $player.getGender() @ $player.getGenre() @ "idl1b";
            $player.playAnim(%anim);
        }
    }
    else
    {
        commandToServer('StopPropAction');
    }
    return ;
}
function mouseFire(%val)
{
    $mvTriggerCount0 = $mvTriggerCount0 + 1;
    return ;
}
function altTrigger(%val)
{
    $mvTriggerCount1 = $mvTriggerCount1 + 1;
    return ;
}
function toggleZoom(%val)
{
    if (%val)
    {
        $ZoomOn = 0;
        setFOV($UserPref::Player::DefaultFOV);
    }
    else
    {
        $ZoomOn = 1;
        setFOV($Pref::Player::CurrentFOV);
    }
    return ;
}
function toggleFreeLook(%val)
{
    if (%val)
    {
        $mvFreeLook = 1;
    }
    else
    {
        $mvFreeLook = 0;
    }
    return ;
}
function toggleFirstPerson()
{
    $firstPerson = !$firstPerson;
    return ;
}
function toggleCamera()
{
    commandToServer('ToggleCamera');
    return ;
}
$cameraFOVAdjustment = 0;
$cameraDistAdjustment = 0;
$cameraFOVTimer = 0;
function startZoomIn(%amt)
{
    if (!isDefined("%amt"))
    {
        %amt = 1;
    }
    $cameraFOVAdjustment = 100 * %amt;
    zoomTick();
    return ;
}
function startZoomOut(%amt)
{
    if (!isDefined("%amt"))
    {
        %amt = 1;
    }
    $cameraFOVAdjustment = -100 * %amt;
    zoomTick();
    return ;
}
function startDollyIn(%amt)
{
    if (!isDefined("%amt"))
    {
        %amt = 1;
    }
    $cameraDistAdjustment = 100 * %amt;
    zoomTick();
    return ;
}
function startDollyOut(%amt)
{
    if (!isDefined("%amt"))
    {
        %amt = 1;
    }
    $cameraDistAdjustment = -100 * %amt;
    zoomTick();
    return ;
}
function startDollyZoomIn(%amt)
{
    if (!isDefined("%amt"))
    {
        %amt = 1;
    }
    $cameraFOVAdjustment = 100 * %amt;
    $cameraDistAdjustment = 1;
    zoomTick();
    return ;
}
function startDollyZoomOut(%amt)
{
    if (!isDefined("%amt"))
    {
        %amt = 1;
    }
    $cameraFOVAdjustment = -100 * %amt;
    $cameraDistAdjustment = 1;
    zoomTick();
    return ;
}
function stopZoom()
{
    $cameraFOVAdjustment = 0;
    $cameraDistAdjustment = 0;
    zoomTick();
    return ;
}
function zoomTick()
{
    if ($cameraFOVTimer)
    {
        cancel($cameraFOVTimer);
    }
    if (($cameraFOVAdjustment * $cameraDistAdjustment) != 0)
    {
        changeCameraDistAndFOV($cameraFOVAdjustment);
    }
    else
    {
        changeCameraFOV($cameraFOVAdjustment);
        changeCameraDist($cameraDistAdjustment);
    }
    if (($cameraFOVAdjustment != 0) && ($cameraDistAdjustment != 0))
    {
        $cameraFOVTimer = schedule(25, 0, "zoomTick");
    }
    return ;
}
function buttonBarMenuLogout()
{
    logout(0);
    WorldMap.exit();
    return ;
}
function toggleVisibleState(%this)
{
    if (%this.isVisible())
    {
        %this.close(0);
    }
    else
    {
        %this.open();
    }
    return ;
}
safeEnsureScriptObject("StringMap", "CSPanelCategories");
CSPanelCategories.put("CSMediaDisplay", "settings");
CSPanelCategories.put("CSRulesAndDescWindow", "settings");
CSPanelCategories.put("CSFurnitureMover", "furniture");
CSPanelCategories.put("CSInventoryBrowserWindow", "furniture");
CSPanelCategories.put("CSShoppingBrowserWindow", "furniture");
CSPanelCategories.put("CSPaintingWindow", "painting");
function closeCSPanelsInOtherCategories(%panel)
{
    if (!isObject(%panel))
    {
        return ;
    }
    %category = CSPanelCategories.get(%panel.getName());
    %size = CSPanelCategories.size();
    %i = 0;
    while (%i < %size)
    {
        if (!(CSPanelCategories.getValue(%i) $= %category))
        {
            CSPanelCategories.getKey(%i).close();
        }
        %i = %i + 1;
    }
}

function numCSPanelsOpen()
{
    %num = 0;
    %size = CSPanelCategories.size();
    %i = 0;
    while (%i < %size)
    {
        if (CSPanelCategories.getKey(%i).isVisible())
        {
            %num = %num + 1;
        }
        %i = %i + 1;
    }
    return %num;
}
function toggleCSPanel(%panel)
{
    if (!isObject(%panel))
    {
        return ;
    }
    %panel.toggle();
    return ;
}
function toggleBuddyHud()
{
    toggleVisibleState(BuddyHudWin);
    return ;
}
function toggleBuddyHudForTab(%tabName)
{
    %wasOpen = 1;
    if (!BuddyHudWin.visible)
    {
        %wasOpen = 0;
        BuddyHudWin.open();
    }
    %currentTabName = BuddyHudTabs.getCurrentTab().name;
    if (%wasOpen && (%tabName $= %currentTabName))
    {
        BuddyHudWin.close();
        return ;
    }
    BuddyHudTabs.selectTabWithName(%tabName);
    return ;
}
function toggleSelfViewHud()
{
    PlayGui.showRaiseOrHide(PlayerWin);
    if (!PlayerWin.isVisible() && $IN_ORBIT_CAM)
    {
        togglePlayerCamMode();
    }
    return ;
}
function toggleEmoteHud()
{
    toggleVisibleState(EmoteHudWin);
    return ;
}
function toggleMusicHud()
{
    if (MusicHud.isShowing())
    {
        MusicHud.hide();
    }
    else
    {
        MusicHud.show();
        MusicHud.keepOpen(1);
    }
    return ;
}
function toggleOptionsPanel()
{
    toggleVisibleState(OptionsPanel);
    return ;
}
function okToOpenClosetGui()
{
    if (isObject(ApplauseMeterGui))
    {
        if (ApplauseMeterGui.applauseMeterUse $= "sumo")
        {
            if (ApplauseMeterGui.sumoGameType $= "PillowFightGame")
            {
                MessageBoxOK($MsgCat::applauseGui["MSG-PILLOW-WARN"], $MsgCat::applauseGui["MSG-PILLOW-USER-NO-OPEN-CLOSET"], "");
            }
            else
            {
                MessageBoxOK($MsgCat::applauseGui["MSG-SUMO-WARN"], $MsgCat::applauseGui["MSG-SUMO-USER-NO-OPEN-CLOSET"], "");
            }
            return 0;
        }
    }
    return 1;
}
function toggleWardrobe()
{
    if (!okToOpenClosetGui())
    {
        return ;
    }
    toggleVisibleState(wardrobeGui);
    return ;
}
function toggleBodyTab()
{
    if (!okToOpenClosetGui())
    {
        return ;
    }
    toggleVisibleState(ClosetGui);
    ClosetTabs.selectTabWithName("Body");
    return ;
}
function toggleClosetTab()
{
    if (!okToOpenClosetGui())
    {
        return ;
    }
    toggleVisibleState(ClosetGui);
    ClosetTabs.selectTabWithName("Closet");
    return ;
}
function toggleClosetGui()
{
    scriptProfiler_EnterScope();
    if (!okToOpenClosetGui())
    {
        return ;
    }
    toggleVisibleState(ClosetGui);
    if (ClosetGui.isVisible())
    {
        if ($gCurrentStoreName $= "")
        {
            %tabToOpen = ClosetGui.lastTabOpened;
            if (%tabToOpen $= "")
            {
                %tabToOpen = "Closet";
            }
            ClosetTabs.selectTabWithName(%tabToOpen);
        }
        else
        {
            ClosetTabs.selectTabWithName("Shops");
        }
    }
    scriptProfiler_LeaveScope();
    return ;
}
function refreshCSSelector()
{
    if (CustomSpacesSelectorContainer.isVisible())
    {
        CustomSpacesSelector.refresh();
    }
    return ;
}
function toggleBuildingDirectory()
{
    if (CustomSpacesSelectorContainer.visible)
    {
        CustomSpacesSelector.close();
    }
    else
    {
        if (!(BuildingDirectoryButton.lastBuildingEntered $= ""))
        {
            CustomSpacesSelector.open(BuildingDirectoryButton.lastBuildingEntered);
        }
    }
    return ;
}
function toggleClosetItemCategory(%category)
{
    if (!okToOpenClosetGui())
    {
        return ;
    }
    toggleVisibleState(ClosetGui);
    if (ClosetGui.visible)
    {
        ClosetTabs.selectTabWithName("CLOSET");
        ClosetItemPopup.onSelect(0, %category);
    }
    return ;
}
function toggleStore()
{
    if (!okToOpenClosetGui())
    {
        return ;
    }
    if ($gCurrentStoreName $= "")
    {
    }
    else
    {
        toggleVisibleState(ClosetGui);
        if (ClosetGui.visible)
        {
            ClosetTabs.selectTabWithName("SHOPS");
        }
    }
    return ;
}
function toggleSnapshot()
{
    if (!okToOpenClosetGui())
    {
        return ;
    }
    toggleVisibleState(snapshotTool);
    return ;
}
function toggleAIMHud()
{
    toggleVisibleState(BuddyHudWin);
    if (BuddyHudWin.isVisible())
    {
        BuddyHudTabs.selectTabWithName("AIM");
    }
    return ;
}
function toggleWorldControlPanel()
{
    PlayGui.showRaiseOrHide(worldControlPanel);
    return ;
}
function toggleDancePad()
{
    PlayGui.showRaiseOrHide(DancePadGui);
    return ;
}
function toggleBoneBlendGui()
{
    if ($player.isDebugging())
    {
        PlayGui.showRaiseOrHide(boneBlendGui);
    }
    return ;
}
function toggleTGF()
{
    if ((Canvas.getContent().getId() != geTGF.getId()) && (geTGF.getParent().getId() != PlayGui.getId()))
    {
        geTGF.setVisible(0);
    }
    toggleVisibleState(geTGF);
    return ;
}
function toggleWorldMap()
{
    geTGF_tabs.Maps_filterDestinationsByType("");
    geTGF.toggleToTabName("Map");
    WorldMap.setView("multi_city");
    return ;
}
function toggleCityMap()
{
    geTGF.toggleToTabName("Map");
    geTGF_tabs.Maps_filterDestinationsByType("");
    if ($CSBuildingInfo != 0)
    {
        WorldMap.selectCity($CSBuildingInfo.city);
    }
    else
    {
        WorldMap.selectCity($gContiguousSpaceName);
    }
    return ;
}
function toggleTGFMapFiltered(%filterType)
{
    toggleCityMap();
    geTGF_tabs.Maps_filterDestinationsByType(%filterType);
    return ;
}
function togglePerformerPanel()
{
    performerPanel.toggle();
    return ;
}
function toggleLocalMap()
{
    toggleVisibleState(geLocalMapContainer);
    return ;
}
function toggleCameraImgBroadcast()
{
    BroadCastControlPanel.toggle();
    return ;
}
function toggleConversationDebug()
{
    return ;
}
function openHelpURL()
{
    gotoWebPage($Net::HelpURL_General);
    return ;
}
function openEventsURL()
{
    gotoWebPage($Net::EventsURL);
    return ;
}
function openForumsURL()
{
    gotoWebPage($Net::ForumsURL);
    return ;
}
function startRecordingDemo()
{
    startDemoRecord();
    return ;
}
function stopRecordingDemo()
{
    stopDemoRecord();
    return ;
}
function dropCameraAtPlayer()
{
    if (!isObjectAndHasPermission_NoWarn($player, "fly"))
    {
        return ;
    }
    commandToServer('DropCameraAtPlayer');
    return ;
}
function dropPlayerAtCamera()
{
    if (!isObjectAndHasPermission_NoWarn($player, "fly"))
    {
        return ;
    }
    commandToServer('DropPlayerAtCamera');
    return ;
}
function oxe_CameraSpeed(%val)
{
    $Camera::movementSpeed = 0.5 + ((%val - 1) * 1);
    return ;
}
$MFDebugRenderMode = 0;
function cycleDebugRenderMode()
{
    if (!$player.rolesPermissionCheckNoWarn("debugPassive"))
    {
        return ;
    }
    if (getBuildString() $= "Debug")
    {
        if ($MFDebugRenderMode == 0)
        {
            $MFDebugRenderMode = 1;
            GLEnableOutline(1);
        }
        else
        {
            if ($MFDebugRenderMode == 1)
            {
                $MFDebugRenderMode = 2;
                GLEnableOutline(0);
                setInteriorRenderMode(7);
                showInterior();
            }
            else
            {
                if ($MFDebugRenderMode == 2)
                {
                    $MFDebugRenderMode = 0;
                    setInteriorRenderMode(0);
                    GLEnableOutline(0);
                    show();
                }
            }
        }
    }
    else
    {
        echo("Debug render modes only available when running a Debug build.");
    }
    return ;
}
GlobalActionMap.bind(keyboard, "alt tilde", ToggleConsoleReally);
GlobalActionMap.bind(keyboard, "ctrl capslock", ToggleConsoleReally);
GlobalActionMap.bindCmd(keyboard, "alt F9", "cycleDebugRenderMode();", "");
GlobalActionMap.bindCmd(keyboard, "escape", "", "escapeFromGame();");
GlobalActionMap.bindCmd(keyboard, "alt F4", "", "");
if ($Platform $= "macos")
{
    GlobalActionMap.bind(keyboard, "alt", "onDragAndDropCtrl");
}
else
{
    GlobalActionMap.bind(keyboard, "lcontrol", "onDragAndDropCtrl");
    GlobalActionMap.bind(keyboard, "rcontrol", "onDragAndDropCtrl");
}
functionMap.bindCmd(keyboard, "F8", "EmoteHudList.doFunc(\"F08\"   );", "");
functionMap.bindCmd(keyboard, "F9", "EmoteHudList.doFunc(\"F09\"   );", "");
functionMap.bindCmd(keyboard, "F10", "EmoteHudList.doFunc(\"F10\"   );", "");
functionMap.bindCmd(keyboard, "F11", "EmoteHudList.doFunc(\"F11\"   );", "");
functionMap.bindCmd(keyboard, "F12", "EmoteHudList.doFunc(\"F12\"   );", "");
functionMap.bindCmd(keyboard, "ctrl 1", "EmoteHudList.doFunc(\"ctrl1\");", "");
functionMap.bindCmd(keyboard, "ctrl 2", "EmoteHudList.doFunc(\"ctrl2\");", "");
functionMap.bindCmd(keyboard, "ctrl 3", "EmoteHudList.doFunc(\"ctrl3\");", "");
functionMap.bindCmd(keyboard, "ctrl 4", "EmoteHudList.doFunc(\"ctrl4\");", "");
functionMap.bindCmd(keyboard, "ctrl 5", "EmoteHudList.doFunc(\"ctrl5\");", "");
functionMap.bindCmd(keyboard, "ctrl 6", "EmoteHudList.doFunc(\"ctrl6\");", "");
functionMap.bindCmd(keyboard, "ctrl 7", "EmoteHudList.doFunc(\"ctrl7\");", "");
functionMap.bindCmd(keyboard, "ctrl 8", "EmoteHudList.doFunc(\"ctrl8\");", "");
functionMap.bindCmd(keyboard, "ctrl 9", "EmoteHudList.doFunc(\"ctrl9\");", "");
functionMap.bindCmd(keyboard, "ctrl 0", "EmoteHudList.doFunc(\"ctrl0\");", "");
toggleFirstPerson(1);
new ActionMap(buildingDirectoryMap);
buildingDirectoryMap.bindCmd(keyboard, "F5", "refreshCSSelector();", "");
buildingDirectoryMap.bindCmd(keyboard, "enter", "CustomSpacesSelector.doOnKeyDown(\"enter\");", "");
buildingDirectoryMap.bindCmd(keyboard, "up", "CustomSpacesSelector.doOnKeyDown(\"up\");", "CustomSpacesSelector.doOnKeyUp(\"up\");");
buildingDirectoryMap.bindCmd(keyboard, "down", "CustomSpacesSelector.doOnKeyDown(\"down\");", "CustomSpacesSelector.doOnKeyUp(\"down\");");
new ActionMap(closetMap);
closetMap.bindCmd(keyboard, "F5", "toggleClosetGui();", "");
closetMap.bindCmd(keyboard, "left", "ClosetGui.doArrow(-1, 0);", "");
closetMap.bindCmd(keyboard, "right", "ClosetGui.doArrow( 1, 0);", "");
closetMap.bindCmd(keyboard, "up", "ClosetGui.doArrow( 0, 1);", "");
closetMap.bindCmd(keyboard, "down", "ClosetGui.doArrow( 0,-1);", "");
closetMap.bindCmd(keyboard, "alt n", "", "");
closetMap.bindCmd(keyboard, "F2", "", "");
closetMap.bindCmd(keyboard, "ctrl r", "ClosetGUI_RefreshTextures();", "");
new ActionMap(optionsMap);
optionsMap.bindCmd(keyboard, "F6", "toggleOptionsPanel();", "");
new ActionMap(tgfMapMap);
tgfMapMap.bindCmd(keyboard, "alt n", "toggleTGF();", "");
tgfMapMap.bindCmd(keyboard, "F2", "geTGF.closeFully();", "");
tgfMapMap.bindCmd(keyboard, "F5", "geTGF.onRefresh();", "");
new ActionMap(csFurnitureMap);
csFurnitureMap.bindCmd(keyboard, "delete", "csTestFreeSelectedItem();", "");
if ($Platform $= "macos")
{
    csFurnitureMap.bindCmd(keyboard, "backspace", "csTestFreeSelectedItem();", "");
    csFurnitureMap.bindCmd(keyboard, "delete", "csTestFreeSelectedItem();", "");
    csFurnitureMap.bindCmd(keyboard, "opt x", "CSFurnitureMover.doCut();", "");
    csFurnitureMap.bindCmd(keyboard, "opt c", "CSFurnitureMover.doCopy();", "");
    csFurnitureMap.bindCmd(keyboard, "opt v", "CSFurnitureMover.doPaste();", "");
}
else
{
    csFurnitureMap.bindCmd(keyboard, "delete", "csTestFreeSelectedItem();", "");
    csFurnitureMap.bindCmd(keyboard, "ctrl x", "CSFurnitureMover.doCut();", "");
    csFurnitureMap.bindCmd(keyboard, "ctrl c", "CSFurnitureMover.doCopy();", "");
    csFurnitureMap.bindCmd(keyboard, "ctrl insert", "CSFurnitureMover.doCopy();", "");
    csFurnitureMap.bindCmd(keyboard, "ctrl v", "CSFurnitureMover.doPaste();", "");
    csFurnitureMap.bindCmd(keyboard, "shift insert", "CSFurnitureMover.doPaste();", "");
}
