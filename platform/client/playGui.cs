$TESTMissionGroupIntegrityAlreadyRun = 0;
$gPrevNumMissing = 0;
function PlayGui::onWake(%this) {
    %this.Initialize();
    GuiTracker.updateLocation(%this);
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
    rolesVIP = roles::getRolesMaskFromStrings("staff moderator celeb") @ TheShapeNameHud;
    rolesCeleb = roles::getRolesMaskFromStrings("celeb") @ TheShapeNameHud;
    %this.schedule(100, "resetFirstResponder");
    if ((1.0 == $RegisterObjectFailFlag)) {
        schedule(0, 0, "MessageBoxOK", "DATABLOCK REGISTRATION OF OBJECT FAILED", "Do not continue editing this mission because you are missing datablocks and will destroy other people's work if you continue, but you probably just need to do an update of your working area.\n\nSearch the console.log for 'Register object failed'." @ "\n" @ $gRegisterObjectFailList, "");
    }
    if (($gPrevNumMissing > getNumMissingTextures())) {
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
        %this.newContextMenu("ETSWhatsThisMenu");
        %this.newContextMenu("PlayerContextMenu");
        %this.newContextMenu("LinkContextMenu");
        %this.newContextMenu("FurnitureItemContextMenu");
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
        MessageHudEdit.makeFirstResponder(1);
    }
    TheShapeNameHud.makeFirstResponder(1);
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
        adminGui.tryTarget(%obj);
    }
    if (isObject(animatorPanel)) {
        animatorPanel.tryTarget(%obj);
    }
    if (isObject(salonChairControlGui)) {
        salonChairControlGui.tryTarget(%obj);
    }
    if (!(isObject(%obj))) {
        onLeftClickSwatch(0);
        return;
    }
    %type = %obj.getType();
    if ($gSwatchPaintingModeOn) {
        onLeftClickSwatch(%obj);
    }
    if (($TypeMasks::AdvertObjectType & %type)) {
        %this.onAdvertClick(%obj, %pt);
    }
    if (($TypeMasks::UsableObjectType & %type)) {
        %this.onUsableObjectClick(%obj, %pt);
    }
    if (($TypeMasks::PlayerObjectType & %type)) {
        onLeftClickPlayerName(%obj.getShapeName(), %obj);
    }
    echoDebug("got a clicked object but didn't find a proper typemask:" @ " " @ %type @ " " @ getDebugString(%obj));
};
function PlayGui::onRightMouseDown(%this, %obj, %pt) {
    %type = 0;
    if (isObject(%obj)) {
        %type = %obj.getType();
    }
    if (($TypeMasks::InteriorObjectType & %type)) {
        onRightClickDownInterior(%obj);
    }
};
function PlayGui::onRightMouseUp(%this, %obj) {
    %type = 0;
    if (isObject(%obj)) {
        %type = %obj.getType();
    }
    if (($TypeMasks::AdvertObjectType & %type)) {
        %this.onAdvertClick(%obj, %pt);
    }
    if (($TypeMasks::UsableObjectType & %type)) {
        %this.onUsableObjectRightClick(%obj);
    }
    if (($TypeMasks::PlayerObjectType & %type)) {
        %this.onRMBPlayer(%obj);
    }
    if (($TypeMasks::InteriorObjectType & %type)) {
        onRightClickUpInterior(%obj);
    }
};
function PlayGui::onUsableObjectClick(%this, %obj, %pt) {
    setIdle(0);
    %nuggetId = %obj.getInventoryNuggetID();
    if (%obj.seatDisplay) {
        ClientSittingSystemOnClick(%obj);
    }
    if ((0.0 >= %nuggetId)) {
        if ($CS_EditingCustomSpace) {
        }
        if (!($EventModifier::CTRL & $Keyboard::modifierKeys)) {
            CSFurnitureMover.SelectNuggetObject(%obj);
        }
        if (checkInteractOK(%obj)) {
            if (%obj.hasMethod("onUse")) {
                %obj.onUse($player);
            }
            commandToServer('usableObjectClick', %obj.getGhostID());
        }
    }
    if (checkInteractOK(%obj)) {
        if (%obj.hasMethod("onUse")) {
            %obj.onUse($player);
        }
        commandToServer('usableObjectClick', %obj.getGhostID());
    }
};
function PlayGui::onUsableObjectRightClick(%this, %obj) {
    %nuggetId = %obj.getInventoryNuggetID();
    if ((0.0 >= %nuggetId)) {
    }
    if ($CS_EditingCustomSpace) {
        FurnitureItemContextMenu.initWithObject(%obj);
        FurnitureItemContextMenu.showAtCursor();
    }
    if ((0.0 != %obj)) {
        if (checkInteractOK(%obj)) {
        }
        if (%obj.hasMethod("onRightUse")) {
            %obj.onRightUse();
        }
    }
};
$gPlayGuiLastMouseOver = 0;
function PlayGui::onMouseOver(%this, %obj) {
    if (isObject($TSControl::objSelLastMouseOver)) {
        $TSControl::objSelLastMouseOver.SetHighlighted(0);
    }
    Canvas.setCursor(ETSDefaultCursor);
    if ((0.0 == %obj)) {
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
        %obj.SetHighlighted(1);
        Canvas.setCursor(ETSHandCursor);
        %type = %obj.getType();
        if (%obj.hasMethod("getDataBlock")) {
        }
        %datablock = 0;
        %obj.getDataBlock();
        if (1) {
        }
        if (%obj.isGhost()) {
        }
        if (($TypeMasks::UsableObjectType & %type)) {
            if (isObject(%datablock)) {
            }
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
    if (($TypeMasks::InteriorObjectType & %obj.getType())) {
        return 0;
    }
    if (%obj.hasMethod("getActivationRange")) {
        %activateDistance = %obj.getActivationRange();
    }
    if ((0.0 == %activateDistance)) {
    }
    if (%obj.hasMethod("getDataBlock")) {
        %datablock = %obj.getDataBlock();
        %activateDistance = %datablock.activateRange;
    }
    if ((0.0 > %activateDistance)) {
        %rangeVal = (%activateDistance * %activateDistance);
        %distVal = VectorDistSquared($player.getPosition(), %obj.getPosition());
        if ((%distVal < %rangeVal)) {
            %interactOK = 0;
        }
    }
    return %interactOK;
};
function GuiControl::getTopWindow(%this) {
    return %this.getTopNthWindow(0);
};
function GuiControl::getTopNthWindow(%this, %ndex) {
    %count = %this.getCount();
    %num = 0;
    %idx = (1.0 - %count);
    if ((0.0 >= %idx)) {
        %obj = %this.getObject(%idx);
        if (%obj.profile.canKeyFocus) {
        }
        if (%obj.isVisible()) {
        }
        if ((TheShapeNameHud.getId() != %obj.getId())) {
            if ((%ndex >= %num)) {
                return %obj;
            }
            %num = (1.0 + %num);
        }
        %idx = (1.0 - %idx);
    }
    return -(1.0);
};
function GuiControl::focusTopWindow(%this) {
    %obj = %this.getTopWindow();
    if (isObject(%obj)) {
        %this.focusAndRaise(%obj);
    }
    TheShapeNameHud.makeFirstResponder(1);
};
function GuiControl::closeTopClosableWindow(%this) {
    %closedOne = 0;
    %n = 0;
    if (!(%closedOne)) {
        %obj = %this.getTopNthWindow(%n);
        if (!(isObject(%obj))) {
        }
        if (!(%obj.closeCommand $= "")) {
            eval("%closedOne =" @ " " @ %obj.closeCommand);
        }
        %closedOne = %obj.close();
        %n = (1.0 + %n);
    }
    %this.focusTopWindow();
    return %closedOne;
};
function GuiControl::dumpTopWindows(%this) {
    %n = 0;
    if (1) {
        %obj = %this.getTopNthWindow(%n);
        if (!(isObject(%obj))) {
        }
        echo(getDebugString(%obj));
        %n = (1.0 + %n);
    }
};
function GuiControl::showRaiseOrHide(%this, %ctrl) {
    if (%ctrl.isVisible()) {
        %top = %this.getTopWindow();
        if ((%ctrl.getId() == %top.getId())) {
            %ctrl.close();
        }
        %this.focusAndRaise(%ctrl);
    }
    %ctrl.open();
};
function GuiControl::showRaise(%this, %ctrl) {
    if (%ctrl.isVisible()) {
        %top = %this.getTopWindow();
        if ((%ctrl.getId() != %top.getId())) {
            %this.focusAndRaise(%ctrl);
        }
    }
    %ctrl.open();
};
function GuiControl::focusAndRaise(%this, %ctrl) {
    Canvas.cursorOn();
    %this.pushToBack(%ctrl);
    %ctrl.makeFirstResponder(1);
};
function GuiControl::ensureAdded(%this, %panel) {
    if ((%this.getId() == %panel.getParent())) {
        return;
    }
    %this.add(%panel);
    %panel.setVisible(0);
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
        %distx = (%ax - %bx);
        %disty = (%ay - %by);
        %dist = ((%disty * %disty) + (%distx * %distx));
    }
    return %dist;
};
function onBuddyStateChange(%index) {
    BuddyHudWin.refreshAIMBuddyList();
    %buddyName = aimGetBuddyName(%index);
    %buddyState = aimGetBuddyState(%index);
    AIMConvManager.buddyStateChanged(stripUnprintables(%buddyName), %buddyState);
};
function SitHud::sitDown(%this) {
    commandToServer('SitDown');
};
function SitHud::standUp(%this) {
    commandToServer('StandUp');
};
function clientCmdShowSitHud(%val, %sitOrStand) {
    SitButton.setVisible(%sitOrStand);
    StandButton.setVisible(!(%sitOrStand));
    SitHud.setVisible(%val);
};
function clientCmdShowSitButton(%sitOrStand) {
    SitButton.setVisible(%sitOrStand);
    StandButton.setVisible(!(%sitOrStand));
};
function BitmapFullScreenFlasher::FlashImage(%this, %bitmapName, %fadeInTime, %waitTime, %fadeOutTime) {
    %this.setBitmap(%bitmapName);
    %this.setVisible(1);
    %this.fadeInTime = %fadeInTime;
    %this.waitTime = %waitTime;
    %this.fadeOutTime = %fadeOutTime;
    %this.fadeoutColor = "0 0 0 0";
    %this.reset();
};
function BitmapFullScreenFlasher::onFinishedFading(%this) {
    %this.setVisible(0);
};
function clientCmdFlashImage(%imageName, %fadeInTime, %waitTime, %fadeOutTime) {
    BitmapFullScreenFlasher.FlashImage(%imageName, %fadeInTime, %waitTime, %fadeOutTime);
};
