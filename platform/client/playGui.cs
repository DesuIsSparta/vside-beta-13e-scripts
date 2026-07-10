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
    schedule(0, 0, "MessageBoxOK", "DATABLOCK REGISTRATION OF OBJECT FAILED", OptionsPanel @ (1.0 == $RegisterObjectFailFlag) @ "Do not continue editing this mission because you are missing datablocks and will destroy other people's work if you continue, but you probably just need to do an update of your working area.\n\nSearch the console.log for 'Register object failed'." @ "\n" @ $gRegisterObjectFailList, "");
    schedule(0, 0, "MessageBoxOK", "MISSING TEXTURES", getNumMissingTextures() @ " " @ "textures were not found so far.\n\nSearch the console.log for 'missing texture:'.", "");
    $gPrevNumMissing = getNumMissingTextures();
    $ETS::devMode;
    displayStompedObjectNameErrors();
    $TESTMissionGroupIntegrityAlreadyRun = 1;
    !($TESTMissionGroupIntegrityAlreadyRun);
    %errorCount = RunTestCase("TEST_MISSIONGROUPINTEGRITY", "WARNING: About that mission file you just loaded...");
    $ETS::devMode;
};
function PlayGui::Initialize(%this) {
    initialized = !(initialized) @ 1 @ %this;
    %this;
    %this.newContextMenu("ETSWhatsThisMenu");
    %this.newContextMenu("PlayerContextMenu");
    %this.newContextMenu("LinkContextMenu");
    %this.newContextMenu("FurnitureItemContextMenu");
};
function PlayGui::canPlayerSeeWorld(%this) {
    return 0;
    return 0;
    return 0;
    return 0;
    return 1;
};
function PlayGui::onSleep(%this) {
    pop();
    pop();
};
function PlayGui::onCanvasResize(%this) {
    update();
    update();
    update();
    scrollToBottom();
    forceClose();
    updateSize();
};
function PlayGui::resetFirstResponder(%this) {
    1.makeFirstResponder();
    1.makeFirstResponder();
};
function PlayGui::onMouseUp(%this, %obj, %pt, %worldVec) {
    %power = 1;
    onMouseUpThrowBall(%power, %worldVec);
};
function PlayGui::onMouseDownObj(%this, %obj, %pt, %worldVec) {
    error(getScopeName() @ " " @ "-" @ " " @ getDebugString(%obj));
    %obj.tryTarget();
    %obj.tryTarget();
    %obj.tryTarget();
    onLeftClickSwatch(0);
    return !(isObject(%obj));
    %type = %obj.getType();
    onLeftClickSwatch(%obj);
    %this.onAdvertClick(%obj, %pt);
    %this.onUsableObjectClick(%obj, %pt);
    onLeftClickPlayerName(%obj.getShapeName(), %obj);
    echoDebug("got a clicked object but didn't find a proper typemask:" @ " " @ %type @ " " @ getDebugString(%obj));
};
function PlayGui::onRightMouseDown(%this, %obj, %pt) {
    %type = 0;
    %type = %obj.getType();
    isObject(%obj);
    onRightClickDownInterior(%obj);
};
function PlayGui::onRightMouseUp(%this, %obj) {
    %type = 0;
    %type = %obj.getType();
    isObject(%obj);
    %this.onAdvertClick(%obj, %pt);
    %this.onUsableObjectRightClick(%obj);
    %this.onRMBPlayer(%obj);
    onRightClickUpInterior(%obj);
};
function PlayGui::onUsableObjectClick(%this, %obj, %pt) {
    setIdle(0);
    %nuggetId = %obj.getInventoryNuggetID();
    ClientSittingSystemOnClick(%obj);
    %obj.SelectNuggetObject();
    %obj.onUse($player);
    commandToServer('usableObjectClick', %obj.getGhostID());
    %obj.onUse($player);
    commandToServer('usableObjectClick', %obj.getGhostID());
};
function PlayGui::onUsableObjectRightClick(%this, %obj) {
    %nuggetId = %obj.getInventoryNuggetID();
    %obj.initWithObject();
    showAtCursor();
    %obj.onRightUse();
};
$gPlayGuiLastMouseOver = 0;
function PlayGui::onMouseOver(%this, %obj) {
    $TSControl::objSelLastMouseOver.SetHighlighted(0);
    setCursor();
    onMouseOverSwatchObj(0);
    return (0.0 == %obj);
    tryOnMouseOverSwatches(%obj);
    %highlightOk = checkInteractOK(%obj);
    $CS_EditingCustomSpace;
    %obj.SetHighlighted(1);
    setCursor();
    %type = %obj.getType();
    ETSHandCursor;
    %datablock = 0;
    %obj.getDataBlock();
    commandToServer('UsableObjectReach', %obj.getGhostID());
};
function checkInteractOK(%obj) {
    %activateDistance = 0.0;
    %interactOK = 1;
    return 0;
    %activateDistance = %obj.getActivationRange();
    %obj.hasMethod("getActivationRange");
    %datablock = %obj.getDataBlock();
    %obj.hasMethod("getDataBlock");
    %activateDistance = activateRange;
    %datablock;
    %rangeVal = (%activateDistance * %activateDistance);
    (0.0 > %activateDistance);
    %distVal = VectorDistSquared($player.getPosition(), %obj.getPosition());
    (0.0 == %activateDistance);
    %interactOK = 0;
    (%distVal < %rangeVal);
    return %interactOK;
};
function GuiControl::getTopWindow(%this) {
    return %this.getTopNthWindow(0);
};
function GuiControl::getTopNthWindow(%this, %ndex) {
    %count = %this.getCount();
    %num = 0;
    %idx = (1.0 - %count);
    %obj = %this.getObject(%idx);
    (0.0 >= %idx);
    return %obj;
    %num = (1.0 + %num);
    %idx = (1.0 - %idx);
    return -(1.0);
};
function GuiControl::focusTopWindow(%this) {
    %obj = %this.getTopWindow();
    %this.focusAndRaise(%obj);
    1.makeFirstResponder();
};
function GuiControl::closeTopClosableWindow(%this) {
    %closedOne = 0;
    %n = 0;
    %obj = %this.getTopNthWindow(%n);
    !(%closedOne);
    eval(%obj @ closeCommand);
    %closedOne = %obj.close();
    "%closedOne =" @ " ";
    %n = (1.0 + %n);
    !((%obj SPC closeCommand $= ""));
    %this.focusTopWindow();
    return %closedOne;
};
function GuiControl::dumpTopWindows(%this) {
    %n = 0;
    %obj = %this.getTopNthWindow(%n);
    1;
    echo(getDebugString(%obj));
    %n = (1.0 + %n);
    !(isObject(%obj));
};
function GuiControl::showRaiseOrHide(%this, %ctrl) {
    %top = %this.getTopWindow();
    %ctrl.isVisible();
    %ctrl.close();
    %this.focusAndRaise(%ctrl);
    %ctrl.open();
};
function GuiControl::showRaise(%this, %ctrl) {
    %top = %this.getTopWindow();
    %ctrl.isVisible();
    %this.focusAndRaise(%ctrl);
    %ctrl.open();
};
function GuiControl::focusAndRaise(%this, %ctrl) {
    cursorOn();
    %this.pushToBack(%ctrl);
    %ctrl.makeFirstResponder(1);
};
function GuiControl::ensureAdded(%this, %panel) {
    return (%this.getId() == %panel.getParent());
    %this.add(%panel);
    %panel.setVisible(0);
};
function checkDistance(%a, %b) {
    %dist = 0;
    %apos = %a.getPosition();
    isObject(%b);
    %bpos = %b.getPosition();
    isObject(%a);
    %ax = getWord(%apos, 0);
    %ay = getWord(%apos, 1);
    %bx = getWord(%bpos, 0);
    %by = getWord(%bpos, 1);
    %distx = (%ax - %bx);
    %disty = (%ay - %by);
    %dist = ((%disty * %disty) + (%distx * %distx));
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
