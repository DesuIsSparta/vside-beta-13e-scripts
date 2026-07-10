$TESTMissionGroupIntegrityAlreadyRun = 0;
$gPrevNumMissing = 0;
function PlayGui::onWake(%this) {
    %this.Initialize();
    %this.updateLocation(GuiTracker);
    $enableDirectInput = 1;
    activateDirectInput();
    moveMap.push();
    functionMap.push();
    ShowAllMessageBoxes();
    AIMConvManager.wakeUp();
    BuddyHudWin.wakeUp();
    GameMgrHudWin.wakeUp();
    OptionsPanel.wakeUp();
    EmoteHudWin.wakeUp();
    WindowManager.wakeUp();
    AccountBalanceHud.Initialize();
    AccountBalanceHud.open();
    TheShapeNameHud.rolesVIP = roles::getRolesMaskFromStrings("staff moderator celeb");
    TheShapeNameHud.rolesCeleb = roles::getRolesMaskFromStrings("celeb");
    "resetFirstResponder".schedule(%this, 100);
    if (($RegisterObjectFailFlag == 1.0)) {
        schedule(0, 0, "MessageBoxOK", "DATABLOCK REGISTRATION OF OBJECT FAILED", "Do not continue editing this mission because you are missing datablocks and will destroy other people's work if you continue, but you probably just need to do an update of your working area.\n\nSearch the console.log for 'Register object failed'." @ "\n" @ $gRegisterObjectFailList, "");
    }
    if ((getNumMissingTextures() > $gPrevNumMissing)) {
    }
    if ($ETS::devMode) {
        schedule(0, 0, "MessageBoxOK", "MISSING TEXTURES", getNumMissingTextures() @ " " @ "textures were not found so far.\n\nSearch the console.log for 'missing texture:'.", "");
        $gPrevNumMissing = getNumMissingTextures();
    }
    displayStompedObjectNameErrors();
    if ($StandAlone) {
    }
    if ($ETS::devMode) {
    }
    if (!($TESTMissionGroupIntegrityAlreadyRun)) {
        $TESTMissionGroupIntegrityAlreadyRun = 1;
        %errorCount = RunTestCase("TEST_MISSIONGROUPINTEGRITY", "WARNING: About that mission file you just loaded...");
    }
};
function PlayGui::Initialize(%this) {
    if (!(%this.initialized)) {
        %this.initialized = 1;
        "ETSWhatsThisMenu".newContextMenu(%this);
        "PlayerContextMenu".newContextMenu(%this);
        "LinkContextMenu".newContextMenu(%this);
        "FurnitureItemContextMenu".newContextMenu(%this);
    }
};
function PlayGui::canPlayerSeeWorld(%this) {
    if (!(%this.isVisible())) {
        return 0;
    }
    if (geTGF.isVisible()) {
        return 0;
    }
    if (ClosetGui.isVisible()) {
        return 0;
    }
    if (WorldMap.isVisible()) {
        return 0;
    }
    return 1;
};
function PlayGui::onSleep(%this) {
    functionMap.pop();
    moveMap.pop();
};
function PlayGui::onCanvasResize(%this) {
    if (isObject(ButtonBar)) {
        ButtonBar.update();
    }
    if (isObject(MusicHud)) {
        MusicHud.update();
    }
    if (isObject(WindowManager)) {
        WindowManager.update();
    }
    if (isObject(ConvBubScroll)) {
        ConvBubScroll.scrollToBottom();
    }
    if (isObject(PlayerContextMenu)) {
        PlayerContextMenu.forceClose();
    }
    if (isObject(MLScrollInspectPanel)) {
        MLScrollInspectPanel.updateSize();
    }
};
function PlayGui::resetFirstResponder(%this) {
    if (MessageHud.isVisible()) {
        1.makeFirstResponder(MessageHudEdit);
    }
    1.makeFirstResponder(TheShapeNameHud);
};
function PlayGui::onMouseUp(%this, %obj, %pt, %worldVec) {
    %power = 1;
    onMouseUpThrowBall(%power, %worldVec);
};
function PlayGui::onMouseDownObj(%this, %obj, %pt, %worldVec) {
    if ($ETS::devMode) {
    }
    if ($DevPref::Debug::PrintClickedOn) {
        error(getScopeName() @ " " @ "-" @ " " @ getDebugString(%obj));
    }
    if (isObject(adminGui)) {
        %obj.tryTarget(adminGui);
    }
    if (isObject(animatorPanel)) {
        %obj.tryTarget(animatorPanel);
    }
    if (isObject(salonChairControlGui)) {
        %obj.tryTarget(salonChairControlGui);
    }
    if (!(isObject(%obj))) {
        onLeftClickSwatch(0);
        return;
    }
    %type = %obj.getType();
    if ($gSwatchPaintingModeOn) {
        onLeftClickSwatch(%obj);
    }
    if ((%type & $TypeMasks::AdvertObjectType)) {
        %pt.onAdvertClick(%this, %obj);
    }
    if ((%type & $TypeMasks::UsableObjectType)) {
        %pt.onUsableObjectClick(%this, %obj);
    }
    if ((%type & $TypeMasks::PlayerObjectType)) {
        onLeftClickPlayerName(%obj.getShapeName(), %obj);
    }
    echoDebug("got a clicked object but didn't find a proper typemask:" @ " " @ %type @ " " @ getDebugString(%obj));
};
function PlayGui::onRightMouseDown(%this, %obj, %pt) {
    %type = 0;
    if (isObject(%obj)) {
        %type = %obj.getType();
    }
    if ((%type & $TypeMasks::InteriorObjectType)) {
        onRightClickDownInterior(%obj);
    }
};
function PlayGui::onRightMouseUp(%this, %obj) {
    %type = 0;
    if (isObject(%obj)) {
        %type = %obj.getType();
    }
    if ((%type & $TypeMasks::AdvertObjectType)) {
        %pt.onAdvertClick(%this, %obj);
    }
    if ((%type & $TypeMasks::UsableObjectType)) {
        %obj.onUsableObjectRightClick(%this);
    }
    if ((%type & $TypeMasks::PlayerObjectType)) {
        %obj.onRMBPlayer(%this);
    }
    if ((%type & $TypeMasks::InteriorObjectType)) {
        onRightClickUpInterior(%obj);
    }
};
function PlayGui::onUsableObjectClick(%this, %obj, %pt) {
    setIdle(0);
    %nuggetId = %obj.getInventoryNuggetID();
    if (%obj.seatDisplay) {
        ClientSittingSystemOnClick(%obj);
    }
    if ((%nuggetId >= 0.0)) {
        if ($CS_EditingCustomSpace) {
        }
        if (!($Keyboard::modifierKeys & $EventModifier::CTRL)) {
            %obj.SelectNuggetObject(CSFurnitureMover);
        }
        if (checkInteractOK(%obj)) {
            if ("onUse".hasMethod(%obj)) {
                $player.onUse(%obj);
            }
            commandToServer('usableObjectClick', %obj.getGhostID());
        }
    }
    if (checkInteractOK(%obj)) {
        if ("onUse".hasMethod(%obj)) {
            $player.onUse(%obj);
        }
        commandToServer('usableObjectClick', %obj.getGhostID());
    }
};
function PlayGui::onUsableObjectRightClick(%this, %obj) {
    %nuggetId = %obj.getInventoryNuggetID();
    if ((%nuggetId >= 0.0)) {
    }
    if ($CS_EditingCustomSpace) {
        %obj.initWithObject(FurnitureItemContextMenu);
        FurnitureItemContextMenu.showAtCursor();
    }
    if ((%obj != 0.0)) {
        if (checkInteractOK(%obj)) {
        }
        if ("onRightUse".hasMethod(%obj)) {
            %obj.onRightUse();
        }
    }
};
$gPlayGuiLastMouseOver = 0;
function PlayGui::onMouseOver(%this, %obj) {
    if (isObject($TSControl::objSelLastMouseOver)) {
        0.SetHighlighted($TSControl::objSelLastMouseOver);
    }
    ETSDefaultCursor.setCursor(Canvas);
    if ((%obj == 0.0)) {
        onMouseOverSwatchObj(0);
        return;
    }
    if ($gSwatchPaintingModeOn) {
        tryOnMouseOverSwatches(%obj);
    }
    if ($CS_EditingCustomSpace) {
    }
    %highlightOk = checkInteractOK(%obj);
    if (%highlightOk) {
        1.SetHighlighted(%obj);
        ETSHandCursor.setCursor(Canvas);
        %type = %obj.getType();
        if ("getDataBlock".hasMethod(%obj)) {
        }
        %datablock = 0;
        %obj.getDataBlock();
        if (1) {
        }
        if (%obj.isGhost()) {
        }
        if ((%type & $TypeMasks::UsableObjectType) && isObject(%datablock)) {
        }
        if (!(%datablock.playerAnimReach $= "")) {
        }
        if (!($CS_EditingCustomSpace)) {
            commandToServer('UsableObjectReach', %obj.getGhostID());
        }
    }
};
function checkInteractOK(%obj) {
    %activateDistance = 0.0;
    %interactOK = 1;
    if ((%obj.getType() & $TypeMasks::InteriorObjectType)) {
        return 0;
    }
    if ("getActivationRange".hasMethod(%obj)) {
        %activateDistance = %obj.getActivationRange();
    }
    if ((%activateDistance == 0.0)) {
    }
    if ("getDataBlock".hasMethod(%obj)) {
        %datablock = %obj.getDataBlock();
        %activateDistance = %datablock.activateRange;
    }
    if ((%activateDistance > 0.0)) {
        %rangeVal = (%activateDistance * %activateDistance);
        %distVal = VectorDistSquared($player.getPosition(), %obj.getPosition());
        if ((%rangeVal < %distVal)) {
            %interactOK = 0;
        }
    }
    return %interactOK;
};
function GuiControl::getTopWindow(%this) {
    return 0.getTopNthWindow(%this);
};
function GuiControl::getTopNthWindow(%this, %ndex) {
    %count = %this.getCount();
    %num = 0;
    %idx = (%count - 1.0);
    while ((%idx >= 0.0)) {
        %obj = %idx.getObject(%this);
        if (%obj.profile.canKeyFocus) {
        }
        if (%obj.isVisible()) {
        }
        if ((%obj.getId() != TheShapeNameHud.getId())) {
            if ((%num >= %ndex)) {
                return %obj;
            }
            %num = (%num + 1.0);
        }
        %idx = (%idx - 1.0);
    }
    return -(1.0);
};
function GuiControl::focusTopWindow(%this) {
    %obj = %this.getTopWindow();
    if (isObject(%obj)) {
        %obj.focusAndRaise(%this);
    }
    1.makeFirstResponder(TheShapeNameHud);
};
function GuiControl::closeTopClosableWindow(%this) {
    %closedOne = 0;
    %n = 0;
    while (!(%closedOne)) {
        %obj = %n.getTopNthWindow(%this);
        if (!(isObject(%obj))) {
        }
        if (!(%obj.closeCommand $= "")) {
            eval("%closedOne =" @ " " @ %obj.closeCommand);
        }
        %closedOne = %obj.close();
        %n = (%n + 1.0);
    }
    %this.focusTopWindow();
    return %closedOne;
};
function GuiControl::dumpTopWindows(%this) {
    %n = 0;
    while (1) {
        %obj = %n.getTopNthWindow(%this);
        if (!(isObject(%obj))) {
        }
        echo(getDebugString(%obj));
        %n = (%n + 1.0);
    }
};
function GuiControl::showRaiseOrHide(%this, %ctrl) {
    if (%ctrl.isVisible()) {
        %top = %this.getTopWindow();
        if ((%top.getId() == %ctrl.getId())) {
            %ctrl.close();
        }
        %ctrl.focusAndRaise(%this);
    }
    %ctrl.open();
};
function GuiControl::showRaise(%this, %ctrl) {
    if (%ctrl.isVisible()) {
        %top = %this.getTopWindow();
        if ((%top.getId() != %ctrl.getId())) {
            %ctrl.focusAndRaise(%this);
        }
    }
    %ctrl.open();
};
function GuiControl::focusAndRaise(%this, %ctrl) {
    Canvas.cursorOn();
    %ctrl.pushToBack(%this);
    1.makeFirstResponder(%ctrl);
};
function GuiControl::ensureAdded(%this, %panel) {
    if ((%panel.getParent() == %this.getId())) {
        return;
    }
    %panel.add(%this);
    0.setVisible(%panel);
};
function checkDistance(%a, %b) {
    %dist = 0;
    if (isObject(%a)) {
    }
    if (isObject(%b)) {
        %apos = %a.getPosition();
        %bpos = %b.getPosition();
        %ax = getWord(%apos, 0);
        %ay = getWord(%apos, 1);
        %bx = getWord(%bpos, 0);
        %by = getWord(%bpos, 1);
        %distx = (%bx - %ax);
        %disty = (%by - %ay);
        %dist = ((%distx * %distx) + (%disty * %disty));
    }
    return %dist;
};
function onBuddyStateChange(%index) {
    BuddyHudWin.refreshAIMBuddyList();
    %buddyName = aimGetBuddyName(%index);
    %buddyState = aimGetBuddyState(%index);
    %buddyState.buddyStateChanged(AIMConvManager, stripUnprintables(%buddyName));
};
function SitHud::sitDown(%this) {
    commandToServer('SitDown');
};
function SitHud::standUp(%this) {
    commandToServer('StandUp');
};
function clientCmdShowSitHud(%val, %sitOrStand) {
    %sitOrStand.setVisible(SitButton);
    !(%sitOrStand).setVisible(StandButton);
    %val.setVisible(SitHud);
};
function clientCmdShowSitButton(%sitOrStand) {
    %sitOrStand.setVisible(SitButton);
    !(%sitOrStand).setVisible(StandButton);
};
function BitmapFullScreenFlasher::FlashImage(%this, %bitmapName, %fadeInTime, %waitTime, %fadeOutTime) {
    %bitmapName.setBitmap(%this);
    1.setVisible(%this);
    %this.fadeInTime = %fadeInTime;
    %this.waitTime = %waitTime;
    %this.fadeOutTime = %fadeOutTime;
    %this.fadeoutColor = "0 0 0 0";
    %this.reset();
};
function BitmapFullScreenFlasher::onFinishedFading(%this) {
    0.setVisible(%this);
};
function clientCmdFlashImage(%imageName, %fadeInTime, %waitTime, %fadeOutTime) {
    %fadeOutTime.FlashImage(BitmapFullScreenFlasher, %imageName, %fadeInTime, %waitTime);
};
