$TESTMissionGroupIntegrityAlreadyRun = 0;
$gPrevNumMissing = 0;
function PlayGui::onWake(%this) {
    %this.Initialize();
    %this.updateLocation();
    $enableDirectInput = 1;
    GuiTracker;
    activateDirectInput();
    push();
    push();
    ShowAllMessageBoxes();
    wakeUp();
    wakeUp();
    wakeUp();
    wakeUp();
    wakeUp();
    wakeUp();
    Initialize();
    open();
    rolesVIP = AccountBalanceHud @ roles::getRolesMaskFromStrings("staff moderator celeb") @ TheShapeNameHud;
    AccountBalanceHud;
    rolesCeleb = WindowManager @ roles::getRolesMaskFromStrings("celeb") @ TheShapeNameHud;
    EmoteHudWin;
    %this.schedule(100, "resetFirstResponder");
    if ((1.0 == $RegisterObjectFailFlag)) {
        schedule(0, 0, "MessageBoxOK", "DATABLOCK REGISTRATION OF OBJECT FAILED", GameMgrHudWin @ OptionsPanel @ "Do not continue editing this mission because you are missing datablocks and will destroy other people's work if you continue, but you probably just need to do an update of your working area.\n\nSearch the console.log for 'Register object failed'." @ "\n" @ $gRegisterObjectFailList, "");
    }
    if (($gPrevNumMissing > getNumMissingTextures())) {
    }
    if ($ETS::devMode) {
        schedule(0, 0, "MessageBoxOK", "MISSING TEXTURES", getNumMissingTextures() @ " " @ "textures were not found so far.\n\nSearch the console.log for 'missing texture:'.", "");
        $gPrevNumMissing = getNumMissingTextures();
        BuddyHudWin;
    }
    displayStompedObjectNameErrors();
    if ($StandAlone) {
    }
    if ($ETS::devMode) {
    }
    if (!($TESTMissionGroupIntegrityAlreadyRun)) {
        $TESTMissionGroupIntegrityAlreadyRun = 1;
        AIMConvManager;
        %errorCount = RunTestCase("TEST_MISSIONGROUPINTEGRITY", "WARNING: About that mission file you just loaded...");
        functionMap;
    }
};
function PlayGui::Initialize(%this) {
    if (!(initialized)) {
        initialized = %this @ 1 @ %this;
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
    if (isVisible()) {
        return 0;
    }
    if (isVisible()) {
        return 0;
    }
    if (isVisible()) {
        return 0;
    }
    return 1;
};
function PlayGui::onSleep(%this) {
    pop();
    pop();
};
function PlayGui::onCanvasResize(%this) {
    if (isObject()) {
        update();
    }
    if (isObject()) {
        update();
    }
    if (isObject()) {
        update();
    }
    if (isObject()) {
        scrollToBottom();
    }
    if (isObject()) {
        forceClose();
    }
    if (isObject()) {
        updateSize();
    }
};
function PlayGui::resetFirstResponder(%this) {
    if (isVisible()) {
        1.makeFirstResponder();
    }
    1.makeFirstResponder();
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
    if (isObject()) {
        %obj.tryTarget();
    }
    if (isObject()) {
        %obj.tryTarget();
    }
    if (isObject()) {
        %obj.tryTarget();
    }
    if (!(isObject(%obj))) {
        onLeftClickSwatch(0);
        return salonChairControlGui;
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
    if (seatDisplay) {
        ClientSittingSystemOnClick(%obj);
    }
    if ((0.0 >= %nuggetId)) {
        if ($CS_EditingCustomSpace) {
        }
        if (!($EventModifier::CTRL & $Keyboard::modifierKeys)) {
            %obj.SelectNuggetObject();
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
        %obj.initWithObject();
        showAtCursor();
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
    setCursor();
    if ((0.0 == %obj)) {
        onMouseOverSwatchObj(0);
        return ETSDefaultCursor;
    }
    if ($gSwatchPaintingModeOn) {
        tryOnMouseOverSwatches(%obj);
    }
    if ($CS_EditingCustomSpace) {
    }
    %highlightOk = checkInteractOK(%obj);
    if (%highlightOk) {
        %obj.SetHighlighted(1);
        setCursor();
        %type = %obj.getType();
        ETSHandCursor;
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
        if (!(%datablock SPC playerAnimReach $= "")) {
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
        %activateDistance = activateRange;
        %datablock;
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
        if (canKeyFocus) {
        }
        if (%obj.isVisible()) {
        }
        if ((getId() != %obj.getId())) {
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
    1.makeFirstResponder();
};
function GuiControl::closeTopClosableWindow(%this) {
    %closedOne = 0;
    %n = 0;
    if (!(%closedOne)) {
        %obj = %this.getTopNthWindow(%n);
        if (!(isObject(%obj))) {
        }
        if (!(%obj SPC closeCommand $= "")) {
            eval(%obj @ closeCommand);
        }
        %closedOne = %obj.close();
        "%closedOne =" @ " ";
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
    cursorOn();
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
    refreshAIMBuddyList();
    %buddyName = aimGetBuddyName(%index);
    BuddyHudWin;
    %buddyState = aimGetBuddyState(%index);
    stripUnprintables(%buddyName).buddyStateChanged(%buddyState);
};
function SitHud::sitDown(%this) {
    commandToServer('SitDown');
};
function SitHud::standUp(%this) {
    commandToServer('StandUp');
};
function clientCmdShowSitHud(%val, %sitOrStand) {
    %sitOrStand.setVisible();
    !(%sitOrStand).setVisible();
    %val.setVisible();
};
function clientCmdShowSitButton(%sitOrStand) {
    %sitOrStand.setVisible();
    !(%sitOrStand).setVisible();
};
function BitmapFullScreenFlasher::FlashImage(%this, %bitmapName, %fadeInTime, %waitTime, %fadeOutTime) {
    %this.setBitmap(%bitmapName);
    %this.setVisible(1);
    fadeInTime = %fadeInTime @ %this;
    waitTime = %waitTime @ %this;
    fadeOutTime = %fadeOutTime @ %this;
    fadeoutColor = "0 0 0 0" @ %this;
    %this.reset();
};
function BitmapFullScreenFlasher::onFinishedFading(%this) {
    %this.setVisible(0);
};
function clientCmdFlashImage(%imageName, %fadeInTime, %waitTime, %fadeOutTime) {
    %imageName.FlashImage(%fadeInTime, %waitTime, %fadeOutTime);
};
