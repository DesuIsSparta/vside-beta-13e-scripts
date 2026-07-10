function ApplauseMeterGui::open(%this, %applauseMeterUse, %arg) {
    %applauseMeterUse = strlwr(%applauseMeterUse);
    if ((%this.applauseMeterUse $= "applause")) {
    }
    if (!(%applauseMeterUse $= "applause")) {
        ApplauseMeterGui.closeForApplause();
    }
    if ((%this.applauseMeterUse $= "instrument")) {
    }
    if (!(%applauseMeterUse $= "instrument")) {
        ApplauseMeterGui.closeForInstrument();
    }
    if ((%this.applauseMeterUse $= "sumo")) {
    }
    if (!(%applauseMeterUse $= "sumo")) {
        ApplauseMeterGui.closeForSumo();
    }
    if ((%this.applauseMeterUse $= "blockgame")) {
    }
    if (!(%applauseMeterUse $= "blockgame")) {
        ApplauseMeterGui.closeForBlockGame();
    }
    %this.applauseMeterUse = "";
    %focusAndRaise = 1;
    if ((%applauseMeterUse $= "applause")) {
        %arg.openForApplause(ApplauseMeterGui);
    }
    if ((%applauseMeterUse $= "instrument")) {
        %arg = strlwr(%arg);
        %arg.openForInstrument(ApplauseMeterGui);
        %focusAndRaise = 0;
    }
    if ((%applauseMeterUse $= "sumo")) {
        %arg = strlwr(%arg);
        %arg.openForSumo(ApplauseMeterGui);
    }
    if ((%applauseMeterUse $= "blockgame")) {
        %arg = strlwr(%arg);
        %arg.openForBlockGame(ApplauseMeterGui);
    }
    error(getScopeName() @ " " @ "- unknown use '" @ " " @ %applauseMeterUse @ " " @ "' for ApplauseMeterGui -" @ " " @ getTrace());
    return;
    %this.applauseMeterUse = %applauseMeterUse;
    %this.closingFromServer = 0;
    1.setVisible(%this);
    if (%focusAndRaise) {
        %this.focusAndRaise(PlayGui);
    }
};
function ApplauseMeterGui::scheduleApplaudMeterGuiClose(%this) {
    %sched = gGetFieldWithDefault(%this, "closeApplauseMeterGuiSched", "");
    cancel(%sched);
    %sched = close.schedule(%this, 5000);
    gSetField(%this, "closeApplauseMeterGuiSched", %sched);
};
function ApplauseMeterGui::closeByUser(%this) {
    if ((%this.applauseMeterUse $= "sumo")) {
        if ((%this.sumoGameType $= "PillowFightGame")) {
            MessageBoxOK(%this[$MsgCat::applauseGui @ "MSG-PILLOW-WARN"], $MsgCat::applauseGui["MSG-PILLOW-USERCLOSE"], "");
        }
        MessageBoxOK($MsgCat::applauseGui["MSG-SUMO-WARN"], $MsgCat::applauseGui["MSG-SUMO-USERCLOSE"], "");
        return 0;
    }
    return %this.close();
};
function ApplauseMeterGui::close(%this) {
    if ((%this.applauseMeterUse $= "applause")) {
        ApplauseMeterGui.closeForApplause();
    }
    if ((%this.applauseMeterUse $= "instrument")) {
        ApplauseMeterGui.closeForInstrument();
    }
    if ((%this.applauseMeterUse $= "sumo")) {
        ApplauseMeterGui.closeForSumo();
    }
    if ((%this.applauseMeterUse $= "blockgame")) {
        ApplauseMeterGui.closeForBlockGame();
    }
    if (!(%this.applauseMeterUse $= "")) {
    }
    if (%this.isVisible()) {
        error(getScopeName() @ " " @ "- unknown use '" @ %this.applauseMeterUse @ "' for ApplauseMeterGui -" @ " " @ getTrace());
    }
    %this.applauseMeterUse = "";
    %this.closingFromServer = 0;
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    return 1;
};
function ApplauseMeterGui::downplayChatBubble(%this) {
    if (!(%this.isVisible())) {
        return 0;
    }
    if ((%this.applauseMeterUse $= "blockgame")) {
        return 1;
    }
    return 0;
};
function ApplauseMeterGui::scheduleGoIdle(%this) {
    if ((%this.applauseMeterUse $= "applause")) {
        %sched = gGetFieldWithDefault(%this, "applaudeGoIdleSched", "");
        cancel(%sched);
        %sched = applaudSetIdleIcon.schedule(%this, 750);
        gSetField(%this, "applaudeGoIdleSched", %sched);
    }
    if ((%this.applauseMeterUse $= "instrument")) {
        %sched = gGetFieldWithDefault(%this, "instrumentGoIdleSched", "");
        cancel(%sched);
        %sched = instrumentSetIdleIcon.schedule(%this, 750);
        gSetField(%this, "instrumentGoIdleSched", %sched);
    }
    if ((%this.applauseMeterUse $= "sumo")) {
    }
    error(getScopeName() @ " " @ "- unknown use '" @ " " @ %this.applauseMeterUse @ " " @ "' for ApplauseMeterGui -" @ " " @ getTrace());
};
function ApplauseMeterGui::onSetFirstResponder(%this) {
    0.setGray(%this);
    Parent::onSetFirstResponder(%this);
};
function ApplauseMeterGui::onClearFirstResponder(%this) {
    %sched = gGetFieldWithDefault(%this, "applaudeGoIdleSched", "");
    cancel(%sched);
    gSetField(%this, "applaudeGoIdleSched", "");
    %sched = gGetFieldWithDefault(%this, "instrumentGoIdleSched", "");
    cancel(%sched);
    gSetField(%this, "instrumentGoIdleSched", "");
    1.setGray(%this);
    Parent::onClearFirstResponder(%this);
};
function ApplauseMeterGui::setGray(%this, %value) {
    if ((%this.applauseMeterUse $= "applause")) {
        %this.nonIdleStateA.setApplaudIcon(%this, !(%value), 1);
    }
    if ((%this.applauseMeterUse $= "instrument")) {
        %this.nonIdleStateA.setInstrumentIcon(%this, !(%value), 1);
    }
};
function ApplauseMeterGui::openForBlockGame(%this, %gameType) {
    %gameType[$MsgCat::applauseGui @ "TITLE-BLOCKGAME-" @ %gameType].setText(ApplauseMeterGui);
    %gameType[$MsgCat::applauseGui @ "BODYTEXT-BLOCKGAME-" @ %gameType].setText(ApplauseMeterInfoText);
    %this.nonIdleStateA = 0;
    ApplauseMeterGui.alignToBottom();
};
function ApplauseMeterGui::closeForBlockGame(%this) {
    "".setText(ApplauseMeterInfoText);
    if ($player.isSitting()) {
        SendStandCommand(1);
    }
};
$gBlockGameKeys = "" @ "\n" @ "I" @ "\n" @ "J" @ "\n" @ "K" @ "\n" @ "L" @ "\n" @ " " @ "\n" @ "left" @ "\n" @ "right" @ "\n" @ "up" @ "\n" @ "down" @ "\n" @ "lcontrol" @ "\n" @ "rcontrol";
function ApplauseMeterGui::onBlockGameKeys(%this, %keyCodeStr, %isKeyDown) {
    if (%isKeyDown) {
    }
    %wantIt = (findRecord($gBlockGameKeys, %keyCodeStr) < 0.0) ? 0 : 1;
    if (%wantIt) {
        if ((%keyCodeStr $= "I")) {
            %keyCodeStr = "rotatecw";
        }
        if ((%keyCodeStr $= "J")) {
            %keyCodeStr = "left";
        }
        if ((%keyCodeStr $= "K")) {
            %keyCodeStr = "rotateccw";
        }
        if ((%keyCodeStr $= "L")) {
            %keyCodeStr = "right";
        }
        if ((%keyCodeStr $= " ")) {
            %keyCodeStr = "harddrop";
        }
        commandToServer('BlockGameMove', %keyCodeStr);
    }
    return %isKeyDown;
};
function ApplauseMeterGui::openForSumo(%this, %gameType) {
    %this.sumoGameType = %gameType;
    if ((%this.sumoGameType $= "PillowFightGame")) {
        $MsgCat::applauseGui["TITLE-PILLOW"].setText(ApplauseMeterGui);
        $MsgCat::applauseGui["BODYTEXT-PILLOW"].setText(ApplauseMeterInfoText);
    }
    $MsgCat::applauseGui["TITLE-SUMO"].setText(ApplauseMeterGui);
    $MsgCat::applauseGui["BODYTEXT-SUMO"].setText(ApplauseMeterInfoText);
    %this.nonIdleStateA = 0;
    ApplauseMeterGui.alignToBottom();
    1.setActivityActive(getUserActivityMgr(), "wrestling");
};
function ApplauseMeterGui::closeForSumo(%this) {
    "".setText(ApplauseMeterInfoText);
    %this.sumoGameType = "";
    0.setActivityActive(getUserActivityMgr(), "wrestling");
};
function ApplauseMeterGui::onSumoKeys(%this, %keyCodeStr, %isKeyDown) {
    if ((%keyCodeStr $= "left")) {
        return 0;
    }
    if ((%keyCodeStr $= "right")) {
        return 0;
    }
    if ((%keyCodeStr $= "up")) {
        return 0;
    }
    if ((%keyCodeStr $= "down")) {
        return 0;
    }
    if ((%keyCodeStr $= "\r")) {
        return 0;
    }
    if (%isKeyDown && !(%this.sumoGameType $= "")) {
        if ((%keyCodeStr $= "q")) {
            commandToServer('SumoAction', %this.sumoGameType, 0);
        }
        if ((%keyCodeStr $= "w")) {
            commandToServer('SumoAction', %this.sumoGameType, 1);
        }
        if ((%keyCodeStr $= "e")) {
            commandToServer('SumoAction', %this.sumoGameType, 2);
        }
        if ((%keyCodeStr $= "a")) {
            commandToServer('SumoAction', %this.sumoGameType, 3);
        }
        if ((%keyCodeStr $= "s")) {
            commandToServer('SumoAction', %this.sumoGameType, 4);
        }
        if ((%keyCodeStr $= "z")) {
            commandToServer('SumoAction', %this.sumoGameType, 5);
        }
        if ((%keyCodeStr $= "x")) {
            commandToServer('SumoAction', %this.sumoGameType, 6);
        }
        if ((%keyCodeStr $= " ")) {
            jumpOnce();
        }
    }
    return 1;
};
function ApplauseMeterGui::openForApplause(%this, %playerName) {
    gSetField(%this, "applaudeGoIdleSched", "");
    gSetField(%this, "closeApplauseMeterGuiSched", "");
    $MsgCat::applauseGui["TITLE-APPLAUSE"].setText(ApplauseMeterGui);
    $MsgCat::applauseGui["BODYTEXT-APPLAUSE"] @ " " @ %playerName.setText(ApplauseMeterInfoText);
    commandToServer('SetMyApplauseTarget', %playerName);
    1.setVisible(ApplauseMeterActionIconContainer);
    %this.nonIdleStateA = 0;
    %this.nonIdleStateA.setApplaudIcon(%this, 1, 1);
};
function ApplauseMeterGui::closeForApplause(%this) {
    0.setVisible(ApplauseMeterActionIconContainer);
    "".setText(ApplauseMeterInfoText);
    %sched = gGetFieldWithDefault(%this, "applaudeGoIdleSched", "");
    cancel(%sched);
    gSetField(%this, "applaudeGoIdleSched", "");
    %sched = gGetFieldWithDefault(%this, "closeApplauseMeterGuiSched", "");
    cancel(%sched);
    gSetField(%this, "closeApplauseMeterGuiSched", "");
    commandToServer('SetMyApplauseTarget', "");
};
function ApplauseMeterGui::clap(%this) {
    if ($player.isSitting()) {
        SendStandCommand(1);
        return;
    }
    %this.animateApplaudIcon();
    sendAnimToServer("apls01");
    %this.scheduleGoIdle();
    %this.scheduleApplaudMeterGuiClose();
};
function ApplauseMeterGui::applaudSetIdleIcon(%this) {
    %this.nonIdleStateA = 0;
    %this.nonIdleStateA.setApplaudIcon(%this, 1, 1);
};
function ApplauseMeterGui::animateApplaudIcon(%this) {
    %this.nonIdleStateA = !(%this.nonIdleStateA);
    %this.nonIdleStateA.setApplaudIcon(%this, 1, 0);
};
function ApplauseMeterGui::setApplaudIcon(%this, %hasFocus, %isIdle, %nonIdleStateA) {
    if (!(%hasFocus)) {
        0.setVisible(ApplauseMeterActionActiveIconA);
        0.setVisible(ApplauseMeterActionActiveIconB);
        1.setVisible(ApplauseMeterActionUnfocusedIcon);
        0.setVisible(ApplauseMeterActionIdleIcon);
        return;
    }
    if (%isIdle) {
        0.setVisible(ApplauseMeterActionUnfocusedIcon);
        0.setVisible(ApplauseMeterActionActiveIconA);
        0.setVisible(ApplauseMeterActionActiveIconB);
        1.setVisible(ApplauseMeterActionIdleIcon);
        return;
    }
    if (%nonIdleStateA) {
        0.setVisible(ApplauseMeterActionUnfocusedIcon);
        0.setVisible(ApplauseMeterActionIdleIcon);
        0.setVisible(ApplauseMeterActionActiveIconB);
        1.setVisible(ApplauseMeterActionActiveIconA);
        return;
    }
    0.setVisible(ApplauseMeterActionUnfocusedIcon);
    0.setVisible(ApplauseMeterActionIdleIcon);
    0.setVisible(ApplauseMeterActionActiveIconA);
    1.setVisible(ApplauseMeterActionActiveIconB);
    return;
};
function ApplauseMeterGui::openForInstrument(%this, %instrumentName) {
    gSetField(%this, "instrumentGoIdleSched", "");
    gSetField(%this, "closeApplauseMeterGuiSched", "");
    %schedule = gGetFieldWithDefault(%this, "animateInstrumentIconSchedule", "");
    cancel(%schedule);
    gSetField(%this, "animateInstrumentIconSchedule", "");
    %instrument = %instrumentName.getInstrumentObject(InstrumentRegistryClient);
    if (!(isObject(%instrument))) {
        error(getScopeName() @ " " @ "- can't find instrument '" @ %instrumentName @ "', not opening instrument game -" @ " " @ getTrace());
        %this.close();
        return;
    }
    %this.instrumentInUse = %instrumentName;
    %instrument.titleText.setText(ApplauseMeterGui);
    %instrument.bodyText.setText(ApplauseMeterInfoText);
    %instrument.activeIconA.setBitmap(InstrumentActionActiveIconA);
    %instrument.activeIconB.setBitmap(InstrumentActionActiveIconB);
    %instrument.idleIcon.setBitmap(InstrumentActionIdleIcon);
    %instrument.unfocusedIcon.setBitmap(InstrumentActionUnfocusedIcon);
    1.setVisible(InstrumentActionIconContainer);
    %this.nonIdleStateA = 0;
    %this.nonIdleStateA.setInstrumentIcon(%this, 1, 1);
    %this.closedForInstrument = 0;
};
function ApplauseMeterGui::closeForInstrument(%this) {
    %this.instrumentInUse.getStopAnimation(InstrumentRegistryClient).rawk(%this);
    %this.closedForInstrument = 1;
    %this.instrumentInUse = "";
    %schedule = gGetFieldWithDefault(%this, "animateInstrumentIconSchedule", "");
    cancel(%schedule);
    gSetField(%this, "animateInstrumentIconSchedule", "");
    if (!(%this.closingFromServer)) {
        commandToServer('DropInstrumentOnClosingInstrumentInterface');
    }
    0.setVisible(InstrumentActionIconContainer);
    "".setText(ApplauseMeterInfoText);
    %sched = gGetFieldWithDefault(%this, "instrumentGoIdleSched", "");
    cancel(%sched);
    gSetField(%this, "instrumentGoIdleSched", "");
    %sched = gGetFieldWithDefault(%this, "closeApplauseMeterGuiSched", "");
    cancel(%sched);
    gSetField(%this, "closeApplauseMeterGuiSched", "");
};
function toggleInstrumentGame(%instrument) {
    if (ApplauseMeterGui.isVisible()) {
    }
    if ((ApplauseMeterGui.applauseMeterUse $= "instrument")) {
        ApplauseMeterGui.close();
    }
    %instrument.open(ApplauseMeterGui, "instrument");
};
function toggleGuitarGame() {
    toggleInstrumentGame("guitar");
};
function clientCmdOpenGameControls(%gameType, %arg) {
    if (!(isObject(ApplauseMeterGui))) {
        return;
    }
    if ((%gameType $= "")) {
        return;
    }
    if (!(ApplauseMeterGui.applauseMeterUse $= %gameType)) {
        %arg.open(ApplauseMeterGui, %gameType);
    }
};
function clientCmdCloseGameControls(%gameType, %arg) {
    if (!(isObject(ApplauseMeterGui))) {
        return;
    }
    if ((ApplauseMeterGui.applauseMeterUse $= %gameType)) {
        ApplauseMeterGui.close();
    }
};
function clientCmdDisableInstrumentGame() {
    if (!(ApplauseMeterGui.closedForInstrument)) {
        ApplauseMeterGui.closeForInstrument();
        0.setVisible(InstrumentActionIconContainer);
        %instrumentName.getInstrumentObject(InstrumentRegistryClient).disabledText.setText(ApplauseMeterInfoText);
    }
};
function ApplauseMeterGui::rawk(%this, %anim) {
    if ($player.isSitting()) {
        SendStandCommand(1);
        return;
    }
    if (!(%anim $= "")) {
        commandToServer('PlayInstrumentGameAnim', %this.instrumentInUse, %anim);
    }
    %schedule = gGetFieldWithDefault(%this, "animateInstrumentIconSchedule", "");
    if ((%schedule $= "")) {
        %schedule = animateInstrumentIcon.schedule(%this, 300);
        gSetField(%this, "animateInstrumentIconSchedule", %schedule);
    }
    %this.scheduleGoIdle();
};
function ApplauseMeterGui::instrumentSetIdleIcon(%this) {
    %this.nonIdleStateA = 0;
    %this.nonIdleStateA.setInstrumentIcon(%this, 1, 1);
};
function ApplauseMeterGui::animateInstrumentIcon(%this) {
    %schedule = gGetFieldWithDefault(%this, "animateInstrumentIconSchedule", "");
    cancel(%schedule);
    gSetField(%this, "animateInstrumentIconSchedule", "");
    %this.nonIdleStateA = !(%this.nonIdleStateA);
    %this.nonIdleStateA.setInstrumentIcon(%this, 1, 0);
};
function ApplauseMeterGui::setInstrumentIcon(%this, %hasFocus, %isIdle, %nonIdleStateA) {
    if (!(%hasFocus)) {
        0.setVisible(InstrumentActionActiveIconA);
        0.setVisible(InstrumentActionActiveIconB);
        1.setVisible(InstrumentActionUnfocusedIcon);
        0.setVisible(InstrumentActionIdleIcon);
        return;
    }
    if (%isIdle) {
        0.setVisible(InstrumentActionUnfocusedIcon);
        0.setVisible(InstrumentActionActiveIconA);
        0.setVisible(InstrumentActionActiveIconB);
        1.setVisible(InstrumentActionIdleIcon);
        return;
    }
    if (%nonIdleStateA) {
        0.setVisible(InstrumentActionUnfocusedIcon);
        0.setVisible(InstrumentActionIdleIcon);
        0.setVisible(InstrumentActionActiveIconB);
        1.setVisible(InstrumentActionActiveIconA);
        return;
    }
    0.setVisible(InstrumentActionUnfocusedIcon);
    0.setVisible(InstrumentActionIdleIcon);
    0.setVisible(InstrumentActionActiveIconA);
    1.setVisible(InstrumentActionActiveIconB);
    return;
};
function ApplauseMeterGui::onKeyDown(%this, %unused, %keyCode) {
    setIdle(0);
    %keyCodeStr = %keyCode.getStringFromKeyCode(%this);
    %this.lastKeyDown = %keyCodeStr;
    if ((%this.applauseMeterUse $= "instrument")) {
    }
    if (!(%this.instrumentInUse $= "")) {
        %keyCodeStr.getAnimation(InstrumentRegistryClient, %this.instrumentInUse).rawk(%this);
    }
    if ((%this.applauseMeterUse $= "sumo")) {
        return 1.onSumoKeys(%this, %keyCodeStr);
    }
    if ((%this.applauseMeterUse $= "blockgame")) {
        return 1.onBlockGameKeys(%this, %keyCodeStr);
    }
    return 1;
};
function ApplauseMeterGui::onKeyUp(%this, %unused, %keyCode) {
    %keyCodeStr = %keyCode.getStringFromKeyCode(%this);
    if ((%this.applauseMeterUse $= "applause")) {
        if ((%keyCodeStr $= " ")) {
            ApplauseMeterGui.clap();
        }
    }
    if ((%this.applauseMeterUse $= "instrument")) {
        if ((%this.lastKeyDown $= %keyCodeStr)) {
            if ((%this.instrumentInUse $= "")) {
                warn(getScopeName() @ " " @ "- instrument not specified, using default stop animation '" @ InstrumentRegistryClient.defaultStopAnimation @ "'");
                %anim = InstrumentRegistryClient.defaultStopAnimation;
                commandToServer('EtsPlayAnimName', %anim);
            }
            %anim = %this.instrumentInUse.getStopAnimation(InstrumentRegistryClient);
            commandToServer('EtsPlayAnimName', %anim);
        }
    }
    if ((%this.applauseMeterUse $= "sumo")) {
        return 0.onSumoKeys(%this, %keyCodeStr);
    }
    if ((%this.applauseMeterUse $= "blockgame")) {
        return 0.onBlockGameKeys(%this, %keyCodeStr);
    }
    return 1;
};
