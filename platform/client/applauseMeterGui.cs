function ApplauseMeterGui::open(%this, %applauseMeterUse, %arg) {
    %applauseMeterUse = strlwr(%applauseMeterUse);
    closeForApplause();
    closeForInstrument();
    closeForSumo();
    closeForBlockGame();
    applauseMeterUse = ApplauseMeterGui @ "" @ %this;
    !(((%this SPC applauseMeterUse $= "blockgame") SPC %applauseMeterUse $= "blockgame"));
    %focusAndRaise = 1;
    ApplauseMeterGui;
    %arg.openForApplause();
    %arg = strlwr(%arg);
    (ApplauseMeterGui SPC %applauseMeterUse $= "instrument");
    %arg.openForInstrument();
    %focusAndRaise = 0;
    ApplauseMeterGui;
    %arg = strlwr(%arg);
    ((!(((%this SPC applauseMeterUse $= "sumo") SPC %applauseMeterUse $= "sumo")) SPC %applauseMeterUse $= "applause") SPC %applauseMeterUse $= "sumo");
    %arg.openForSumo();
    %arg = strlwr(%arg);
    (ApplauseMeterGui SPC %applauseMeterUse $= "blockgame");
    %arg.openForBlockGame();
    error(getScopeName() @ " " @ "- unknown use '" @ " " @ %applauseMeterUse @ " " @ "' for ApplauseMeterGui -" @ " " @ getTrace());
    return ApplauseMeterGui;
    applauseMeterUse = %applauseMeterUse @ %this;
    closingFromServer = 0 @ %this;
    %this.setVisible(1);
    %this.focusAndRaise();
};
function ApplauseMeterGui::scheduleApplaudMeterGuiClose(%this) {
    %sched = gGetFieldWithDefault(%this, "closeApplauseMeterGuiSched", "");
    cancel(%sched);
    %sched = %this.schedule(5000);
    close;
    gSetField(%this, "closeApplauseMeterGuiSched", %sched);
};
function ApplauseMeterGui::closeByUser(%this) {
    MessageBoxOK(%this[$MsgCat::applauseGui @ "MSG-PILLOW-WARN"], (%this SPC sumoGameType $= "PillowFightGame"), "");
    MessageBoxOK((%this SPC applauseMeterUse $= "sumo"), , "");
    return 0;
    return %this.close();
};
function ApplauseMeterGui::close(%this) {
    closeForApplause();
    closeForInstrument();
    closeForSumo();
    closeForBlockGame();
    error(%this.isVisible() @ getScopeName() @ " " @ "- unknown use '" @ %this @ applauseMeterUse @ "' for ApplauseMeterGui -" @ " " @ getTrace());
    applauseMeterUse = !((%this SPC applauseMeterUse $= "")) @ "" @ %this;
    ApplauseMeterGui;
    closingFromServer = (%this SPC applauseMeterUse $= "blockgame") @ 0 @ %this;
    ApplauseMeterGui;
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
function ApplauseMeterGui::downplayChatBubble(%this) {
    return 0;
    return 1;
    return 0;
};
function ApplauseMeterGui::scheduleGoIdle(%this) {
    %sched = gGetFieldWithDefault(%this, "applaudeGoIdleSched", "");
    (%this SPC applauseMeterUse $= "applause");
    cancel(%sched);
    %sched = %this.schedule(750);
    applaudSetIdleIcon;
    gSetField(%this, "applaudeGoIdleSched", %sched);
    %sched = gGetFieldWithDefault(%this, "instrumentGoIdleSched", "");
    (%this SPC applauseMeterUse $= "instrument");
    cancel(%sched);
    %sched = %this.schedule(750);
    instrumentSetIdleIcon;
    gSetField(%this, "instrumentGoIdleSched", %sched);
    error(%this @ applauseMeterUse @ " " @ "' for ApplauseMeterGui -" @ " " @ getTrace());
};
function ApplauseMeterGui::onSetFirstResponder(%this) {
    %this.setGray(0);
    Parent::onSetFirstResponder(%this);
};
function ApplauseMeterGui::onClearFirstResponder(%this) {
    %sched = gGetFieldWithDefault(%this, "applaudeGoIdleSched", "");
    cancel(%sched);
    gSetField(%this, "applaudeGoIdleSched", "");
    %sched = gGetFieldWithDefault(%this, "instrumentGoIdleSched", "");
    cancel(%sched);
    gSetField(%this, "instrumentGoIdleSched", "");
    %this.setGray(1);
    Parent::onClearFirstResponder(%this);
};
function ApplauseMeterGui::setGray(%this, %value) {
    %this.setApplaudIcon(!(%value), 1, nonIdleStateA);
    %this.setInstrumentIcon(!(%value), 1, nonIdleStateA);
};
function ApplauseMeterGui::openForBlockGame(%this, %gameType) {
    %gameType[ApplauseMeterGui @ $MsgCat::applauseGui @ "TITLE-BLOCKGAME-" @ %gameType].setText();
    %gameType[ApplauseMeterInfoText @ $MsgCat::applauseGui @ "BODYTEXT-BLOCKGAME-" @ %gameType].setText();
    nonIdleStateA = 0 @ %this;
    alignToBottom();
};
function ApplauseMeterGui::closeForBlockGame(%this) {
    "".setText();
    SendStandCommand(1);
};
$gBlockGameKeys = "" @ "\n" @ "I" @ "\n" @ "J" @ "\n" @ "K" @ "\n" @ "L" @ "\n" @ " " @ "\n" @ "left" @ "\n" @ "right" @ "\n" @ "up" @ "\n" @ "down" @ "\n" @ "lcontrol" @ "\n" @ "rcontrol";
function ApplauseMeterGui::onBlockGameKeys(%this, %keyCodeStr, %isKeyDown) {
    %wantIt = 1;
    0;
    %keyCodeStr = "rotatecw";
    (%wantIt SPC %keyCodeStr $= "I");
    %keyCodeStr = "left";
    ((0.0 < findRecord($gBlockGameKeys, %keyCodeStr)) SPC %keyCodeStr $= "J");
    %keyCodeStr = "rotateccw";
    (%isKeyDown SPC %keyCodeStr $= "K");
    %keyCodeStr = "right";
    (%keyCodeStr $= "L");
    %keyCodeStr = "harddrop";
    (%keyCodeStr $= " ");
    commandToServer('BlockGameMove', %keyCodeStr);
    return %isKeyDown;
};
function ApplauseMeterGui::openForSumo(%this, %gameType) {
    sumoGameType = %gameType @ %this;
    %this[$MsgCat::applauseGui @ "TITLE-PILLOW"].setText();
    ApplauseMeterInfoText.setText();
    ApplauseMeterGui.setText();
    ApplauseMeterInfoText.setText();
    nonIdleStateA = ApplauseMeterGui @ 0 @ %this;
    (%this SPC sumoGameType $= "PillowFightGame");
    alignToBottom();
    getUserActivityMgr().setActivityActive("wrestling", 1);
};
function ApplauseMeterGui::closeForSumo(%this) {
    "".setText();
    sumoGameType = ApplauseMeterInfoText @ "" @ %this;
    getUserActivityMgr().setActivityActive("wrestling", 0);
};
function ApplauseMeterGui::onSumoKeys(%this, %keyCodeStr, %isKeyDown) {
    return 0;
    return 0;
    return 0;
    return 0;
    return 0;
    commandToServer('SumoAction', sumoGameType, 0);
    commandToServer('SumoAction', sumoGameType, 1);
    commandToServer('SumoAction', sumoGameType, 2);
    commandToServer('SumoAction', sumoGameType, 3);
    commandToServer('SumoAction', sumoGameType, 4);
    commandToServer('SumoAction', sumoGameType, 5);
    commandToServer('SumoAction', sumoGameType, 6);
    jumpOnce();
    return 1;
};
function ApplauseMeterGui::openForApplause(%this, %playerName) {
    gSetField(%this, "applaudeGoIdleSched", "");
    gSetField(%this, "closeApplauseMeterGuiSched", "");
    ApplauseMeterGui.setText();
    ApplauseMeterInfoText @ " " @ %playerName.setText();
    commandToServer('SetMyApplauseTarget', %playerName);
    1.setVisible();
    nonIdleStateA = ApplauseMeterActionIconContainer @ 0 @ %this;
    %this.setApplaudIcon(1, 1, nonIdleStateA);
};
function ApplauseMeterGui::closeForApplause(%this) {
    0.setVisible();
    "".setText();
    %sched = gGetFieldWithDefault(%this, "applaudeGoIdleSched", "");
    ApplauseMeterInfoText;
    cancel(%sched);
    gSetField(%this, "applaudeGoIdleSched", "");
    %sched = gGetFieldWithDefault(%this, "closeApplauseMeterGuiSched", "");
    ApplauseMeterActionIconContainer;
    cancel(%sched);
    gSetField(%this, "closeApplauseMeterGuiSched", "");
    commandToServer('SetMyApplauseTarget', "");
};
function ApplauseMeterGui::clap(%this) {
    SendStandCommand(1);
    return $player.isSitting();
    %this.animateApplaudIcon();
    sendAnimToServer("apls01");
    %this.scheduleGoIdle();
    %this.scheduleApplaudMeterGuiClose();
};
function ApplauseMeterGui::applaudSetIdleIcon(%this) {
    nonIdleStateA = 0 @ %this;
    %this.setApplaudIcon(1, 1, nonIdleStateA);
};
function ApplauseMeterGui::animateApplaudIcon(%this) {
    nonIdleStateA = %this @ !(nonIdleStateA) @ %this;
    %this.setApplaudIcon(1, 0, nonIdleStateA);
};
function ApplauseMeterGui::setApplaudIcon(%this, %hasFocus, %isIdle, %nonIdleStateA) {
    0.setVisible();
    0.setVisible();
    1.setVisible();
    0.setVisible();
    return ApplauseMeterActionIdleIcon;
    0.setVisible();
    0.setVisible();
    0.setVisible();
    1.setVisible();
    return ApplauseMeterActionIdleIcon;
    0.setVisible();
    0.setVisible();
    0.setVisible();
    1.setVisible();
    return ApplauseMeterActionActiveIconA;
    0.setVisible();
    0.setVisible();
    0.setVisible();
    1.setVisible();
    return ApplauseMeterActionActiveIconB;
};
function ApplauseMeterGui::openForInstrument(%this, %instrumentName) {
    gSetField(%this, "instrumentGoIdleSched", "");
    gSetField(%this, "closeApplauseMeterGuiSched", "");
    %schedule = gGetFieldWithDefault(%this, "animateInstrumentIconSchedule", "");
    cancel(%schedule);
    gSetField(%this, "animateInstrumentIconSchedule", "");
    %instrument = %instrumentName.getInstrumentObject();
    InstrumentRegistryClient;
    error(!(isObject(%instrument)) @ getScopeName() @ " " @ "- can't find instrument '" @ %instrumentName @ "', not opening instrument game -" @ " " @ getTrace());
    %this.close();
    return;
    instrumentInUse = %instrumentName @ %this;
    titleText.setText();
    bodyText.setText();
    activeIconA.setBitmap();
    activeIconB.setBitmap();
    idleIcon.setBitmap();
    unfocusedIcon.setBitmap();
    1.setVisible();
    nonIdleStateA = InstrumentActionIconContainer @ 0 @ %this;
    %instrument;
    %this.setInstrumentIcon(1, 1, nonIdleStateA);
    closedForInstrument = %this @ 0 @ %this;
    InstrumentActionUnfocusedIcon;
};
function ApplauseMeterGui::closeForInstrument(%this) {
    %this.rawk(instrumentInUse.getStopAnimation());
    closedForInstrument = %this @ 1 @ %this;
    InstrumentRegistryClient;
    instrumentInUse = "" @ %this;
    %schedule = gGetFieldWithDefault(%this, "animateInstrumentIconSchedule", "");
    cancel(%schedule);
    gSetField(%this, "animateInstrumentIconSchedule", "");
    commandToServer('DropInstrumentOnClosingInstrumentInterface');
    0.setVisible();
    "".setText();
    %sched = gGetFieldWithDefault(%this, "instrumentGoIdleSched", "");
    ApplauseMeterInfoText;
    cancel(%sched);
    gSetField(%this, "instrumentGoIdleSched", "");
    %sched = gGetFieldWithDefault(%this, "closeApplauseMeterGuiSched", "");
    InstrumentActionIconContainer;
    cancel(%sched);
    gSetField(%this, "closeApplauseMeterGuiSched", "");
};
function toggleInstrumentGame(%instrument) {
    close();
    "instrument".open(%instrument);
};
function toggleGuitarGame() {
    toggleInstrumentGame("guitar");
};
function clientCmdOpenGameControls(%gameType, %arg) {
    return !(isObject());
    return (%gameType $= "");
    %gameType.open(%arg);
};
function clientCmdCloseGameControls(%gameType, %arg) {
    return !(isObject());
    close();
};
function clientCmdDisableInstrumentGame() {
    closeForInstrument();
    0.setVisible();
    disabledText.setText();
};
function ApplauseMeterGui::rawk(%this, %anim) {
    SendStandCommand(1);
    return $player.isSitting();
    commandToServer('PlayInstrumentGameAnim', instrumentInUse, %anim);
    %schedule = gGetFieldWithDefault(%this, "animateInstrumentIconSchedule", "");
    %this;
    %schedule = %this.schedule(300);
    animateInstrumentIcon;
    gSetField(%this, "animateInstrumentIconSchedule", %schedule);
    %this.scheduleGoIdle();
};
function ApplauseMeterGui::instrumentSetIdleIcon(%this) {
    nonIdleStateA = 0 @ %this;
    %this.setInstrumentIcon(1, 1, nonIdleStateA);
};
function ApplauseMeterGui::animateInstrumentIcon(%this) {
    %schedule = gGetFieldWithDefault(%this, "animateInstrumentIconSchedule", "");
    cancel(%schedule);
    gSetField(%this, "animateInstrumentIconSchedule", "");
    nonIdleStateA = %this @ !(nonIdleStateA) @ %this;
    %this.setInstrumentIcon(1, 0, nonIdleStateA);
};
function ApplauseMeterGui::setInstrumentIcon(%this, %hasFocus, %isIdle, %nonIdleStateA) {
    0.setVisible();
    0.setVisible();
    1.setVisible();
    0.setVisible();
    return InstrumentActionIdleIcon;
    0.setVisible();
    0.setVisible();
    0.setVisible();
    1.setVisible();
    return InstrumentActionIdleIcon;
    0.setVisible();
    0.setVisible();
    0.setVisible();
    1.setVisible();
    return InstrumentActionActiveIconA;
    0.setVisible();
    0.setVisible();
    0.setVisible();
    1.setVisible();
    return InstrumentActionActiveIconB;
};
function ApplauseMeterGui::onKeyDown(%this, %unused, %keyCode) {
    setIdle(0);
    %keyCodeStr = %this.getStringFromKeyCode(%keyCode);
    lastKeyDown = %keyCodeStr @ %this;
    %this.rawk(instrumentInUse.getAnimation(%keyCodeStr));
    return %this.onSumoKeys(%keyCodeStr, 1);
    return %this.onBlockGameKeys(%keyCodeStr, 1);
    return 1;
};
function ApplauseMeterGui::onKeyUp(%this, %unused, %keyCode) {
    %keyCodeStr = %this.getStringFromKeyCode(%keyCode);
    clap();
    warn((%this SPC instrumentInUse $= "") @ getScopeName() @ " " @ "- instrument not specified, using default stop animation '" @ InstrumentRegistryClient @ defaultStopAnimation @ "'");
    %anim = defaultStopAnimation;
    InstrumentRegistryClient;
    commandToServer('EtsPlayAnimName', %anim);
    %anim = instrumentInUse.getStopAnimation();
    %this;
    commandToServer('EtsPlayAnimName', %anim);
    return %this.onSumoKeys(%keyCodeStr, 0);
    return %this.onBlockGameKeys(%keyCodeStr, 0);
    return 1;
};
