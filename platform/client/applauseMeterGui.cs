function ApplauseMeterGui::open(%this, %applauseMeterUse, %arg) {
    %applauseMeterUse = strlwr(%applauseMeterUse);
    if ((%this SPC applauseMeterUse $= "applause")) {
    }
    if (!(%applauseMeterUse $= "applause")) {
        closeForApplause();
    }
    if ((%this SPC applauseMeterUse $= "instrument")) {
    }
    if (!(ApplauseMeterGui SPC %applauseMeterUse $= "instrument")) {
        closeForInstrument();
    }
    if ((%this SPC applauseMeterUse $= "sumo")) {
    }
    if (!(ApplauseMeterGui SPC %applauseMeterUse $= "sumo")) {
        closeForSumo();
    }
    if ((%this SPC applauseMeterUse $= "blockgame")) {
    }
    if (!(ApplauseMeterGui SPC %applauseMeterUse $= "blockgame")) {
        closeForBlockGame();
    }
    applauseMeterUse = ApplauseMeterGui @ "" @ %this;
    %focusAndRaise = 1;
    if ((%applauseMeterUse $= "applause")) {
        %arg.openForApplause();
    }
    if ((ApplauseMeterGui SPC %applauseMeterUse $= "instrument")) {
        %arg = strlwr(%arg);
        %arg.openForInstrument();
        %focusAndRaise = 0;
        ApplauseMeterGui;
    }
    if ((%applauseMeterUse $= "sumo")) {
        %arg = strlwr(%arg);
        %arg.openForSumo();
    }
    if ((ApplauseMeterGui SPC %applauseMeterUse $= "blockgame")) {
        %arg = strlwr(%arg);
        %arg.openForBlockGame();
    }
    error(getScopeName() @ " " @ "- unknown use '" @ " " @ %applauseMeterUse @ " " @ "' for ApplauseMeterGui -" @ " " @ getTrace());
    return ApplauseMeterGui;
    applauseMeterUse = %applauseMeterUse @ %this;
    closingFromServer = 0 @ %this;
    %this.setVisible(1);
    if (%focusAndRaise) {
        %this.focusAndRaise();
    }
};
function ApplauseMeterGui::scheduleApplaudMeterGuiClose(%this) {
    %sched = gGetFieldWithDefault(%this, "closeApplauseMeterGuiSched", "");
    cancel(%sched);
    %sched = %this.schedule(5000);
    close;
    gSetField(%this, "closeApplauseMeterGuiSched", %sched);
};
function ApplauseMeterGui::closeByUser(%this) {
    if ((%this SPC applauseMeterUse $= "sumo")) {
        if ((%this SPC sumoGameType $= "PillowFightGame")) {
            MessageBoxOK(%this[$MsgCat::applauseGui @ "MSG-PILLOW-WARN"], , "");
        }
        MessageBoxOK(, , "");
        return 0;
    }
    return %this.close();
};
function ApplauseMeterGui::close(%this) {
    if ((%this SPC applauseMeterUse $= "applause")) {
        closeForApplause();
    }
    if ((%this SPC applauseMeterUse $= "instrument")) {
        closeForInstrument();
    }
    if ((%this SPC applauseMeterUse $= "sumo")) {
        closeForSumo();
    }
    if ((%this SPC applauseMeterUse $= "blockgame")) {
        closeForBlockGame();
    }
    if (!(%this SPC applauseMeterUse $= "")) {
    }
    if (%this.isVisible()) {
        error(ApplauseMeterGui @ getScopeName() @ " " @ "- unknown use '" @ %this @ applauseMeterUse @ "' for ApplauseMeterGui -" @ " " @ getTrace());
    }
    applauseMeterUse = ApplauseMeterGui @ "" @ %this;
    ApplauseMeterGui;
    closingFromServer = ApplauseMeterGui @ 0 @ %this;
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
function ApplauseMeterGui::downplayChatBubble(%this) {
    if (!(%this.isVisible())) {
        return 0;
    }
    if ((%this SPC applauseMeterUse $= "blockgame")) {
        return 1;
    }
    return 0;
};
function ApplauseMeterGui::scheduleGoIdle(%this) {
    if ((%this SPC applauseMeterUse $= "applause")) {
        %sched = gGetFieldWithDefault(%this, "applaudeGoIdleSched", "");
        cancel(%sched);
        %sched = %this.schedule(750);
        applaudSetIdleIcon;
        gSetField(%this, "applaudeGoIdleSched", %sched);
    }
    if ((%this SPC applauseMeterUse $= "instrument")) {
        %sched = gGetFieldWithDefault(%this, "instrumentGoIdleSched", "");
        cancel(%sched);
        %sched = %this.schedule(750);
        instrumentSetIdleIcon;
        gSetField(%this, "instrumentGoIdleSched", %sched);
    }
    if ((%this SPC applauseMeterUse $= "sumo")) {
    }
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
    if ((%this SPC applauseMeterUse $= "applause")) {
        %this.setApplaudIcon(!(%value), 1, nonIdleStateA);
    }
    if ((%this SPC applauseMeterUse $= "instrument")) {
        %this.setInstrumentIcon(!(%value), 1, nonIdleStateA);
    }
};
function ApplauseMeterGui::openForBlockGame(%this, %gameType) {
    %gameType[ApplauseMeterGui @ $MsgCat::applauseGui @ "TITLE-BLOCKGAME-" @ %gameType].setText();
    %gameType[ApplauseMeterInfoText @ $MsgCat::applauseGui @ "BODYTEXT-BLOCKGAME-" @ %gameType].setText();
    nonIdleStateA = 0 @ %this;
    alignToBottom();
};
function ApplauseMeterGui::closeForBlockGame(%this) {
    "".setText();
    if ($player.isSitting()) {
        SendStandCommand(1);
    }
};
$gBlockGameKeys = "" @ "\n" @ "I" @ "\n" @ "J" @ "\n" @ "K" @ "\n" @ "L" @ "\n" @ " " @ "\n" @ "left" @ "\n" @ "right" @ "\n" @ "up" @ "\n" @ "down" @ "\n" @ "lcontrol" @ "\n" @ "rcontrol";
function ApplauseMeterGui::onBlockGameKeys(%this, %keyCodeStr, %isKeyDown) {
    if (%isKeyDown) {
    }
    %wantIt = (0.0 < findRecord($gBlockGameKeys, %keyCodeStr)) ? 0 : 1;
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
    sumoGameType = %gameType @ %this;
    if ((%this SPC sumoGameType $= "PillowFightGame")) {
        %this[$MsgCat::applauseGui @ "TITLE-PILLOW"].setText();
        ApplauseMeterInfoText.setText();
    }
    ApplauseMeterGui.setText();
    ApplauseMeterInfoText.setText();
    nonIdleStateA = ApplauseMeterGui @ 0 @ %this;
    alignToBottom();
    getUserActivityMgr().setActivityActive("wrestling", 1);
};
function ApplauseMeterGui::closeForSumo(%this) {
    "".setText();
    sumoGameType = ApplauseMeterInfoText @ "" @ %this;
    getUserActivityMgr().setActivityActive("wrestling", 0);
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
    if (%isKeyDown) {
        if (!(%this SPC sumoGameType $= "")) {
            if ((%keyCodeStr $= "q")) {
                commandToServer('SumoAction', sumoGameType, 0);
            }
            if ((%this SPC %keyCodeStr $= "w")) {
                commandToServer('SumoAction', sumoGameType, 1);
            }
            if ((%this SPC %keyCodeStr $= "e")) {
                commandToServer('SumoAction', sumoGameType, 2);
            }
            if ((%this SPC %keyCodeStr $= "a")) {
                commandToServer('SumoAction', sumoGameType, 3);
            }
            if ((%this SPC %keyCodeStr $= "s")) {
                commandToServer('SumoAction', sumoGameType, 4);
            }
            if ((%this SPC %keyCodeStr $= "z")) {
                commandToServer('SumoAction', sumoGameType, 5);
            }
            if ((%this SPC %keyCodeStr $= "x")) {
                commandToServer('SumoAction', sumoGameType, 6);
            }
            if ((%this SPC %keyCodeStr $= " ")) {
                jumpOnce();
            }
        }
    }
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
    nonIdleStateA = 0 @ %this;
    %this.setApplaudIcon(1, 1, nonIdleStateA);
};
function ApplauseMeterGui::animateApplaudIcon(%this) {
    nonIdleStateA = %this @ !(nonIdleStateA) @ %this;
    %this.setApplaudIcon(1, 0, nonIdleStateA);
};
function ApplauseMeterGui::setApplaudIcon(%this, %hasFocus, %isIdle, %nonIdleStateA) {
    if (!(%hasFocus)) {
        0.setVisible();
        0.setVisible();
        1.setVisible();
        0.setVisible();
        return ApplauseMeterActionIdleIcon;
    }
    if (%isIdle) {
        0.setVisible();
        0.setVisible();
        0.setVisible();
        1.setVisible();
        return ApplauseMeterActionIdleIcon;
    }
    if (%nonIdleStateA) {
        0.setVisible();
        0.setVisible();
        0.setVisible();
        1.setVisible();
        return ApplauseMeterActionActiveIconA;
    }
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
    if (!(isObject(%instrument))) {
        error(getScopeName() @ " " @ "- can't find instrument '" @ %instrumentName @ "', not opening instrument game -" @ " " @ getTrace());
        %this.close();
        return;
    }
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
    if (!(closingFromServer)) {
        commandToServer('DropInstrumentOnClosingInstrumentInterface');
    }
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
    if (isVisible()) {
    }
    if ((ApplauseMeterGui SPC applauseMeterUse $= "instrument")) {
        close();
    }
    "instrument".open(%instrument);
};
function toggleGuitarGame() {
    toggleInstrumentGame("guitar");
};
function clientCmdOpenGameControls(%gameType, %arg) {
    if (!(isObject())) {
        return ApplauseMeterGui;
    }
    if ((%gameType $= "")) {
        return;
    }
    if (!(ApplauseMeterGui SPC applauseMeterUse $= %gameType)) {
        %gameType.open(%arg);
    }
};
function clientCmdCloseGameControls(%gameType, %arg) {
    if (!(isObject())) {
        return ApplauseMeterGui;
    }
    if ((ApplauseMeterGui SPC applauseMeterUse $= %gameType)) {
        close();
    }
};
function clientCmdDisableInstrumentGame() {
    if (!(closedForInstrument)) {
        closeForInstrument();
        0.setVisible();
        disabledText.setText();
    }
};
function ApplauseMeterGui::rawk(%this, %anim) {
    if ($player.isSitting()) {
        SendStandCommand(1);
        return;
    }
    if (!(%anim $= "")) {
        commandToServer('PlayInstrumentGameAnim', instrumentInUse, %anim);
    }
    %schedule = gGetFieldWithDefault(%this, "animateInstrumentIconSchedule", "");
    %this;
    if ((%schedule $= "")) {
        %schedule = %this.schedule(300);
        animateInstrumentIcon;
        gSetField(%this, "animateInstrumentIconSchedule", %schedule);
    }
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
    if (!(%hasFocus)) {
        0.setVisible();
        0.setVisible();
        1.setVisible();
        0.setVisible();
        return InstrumentActionIdleIcon;
    }
    if (%isIdle) {
        0.setVisible();
        0.setVisible();
        0.setVisible();
        1.setVisible();
        return InstrumentActionIdleIcon;
    }
    if (%nonIdleStateA) {
        0.setVisible();
        0.setVisible();
        0.setVisible();
        1.setVisible();
        return InstrumentActionActiveIconA;
    }
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
    if ((%this SPC applauseMeterUse $= "instrument")) {
    }
    if (!(%this SPC instrumentInUse $= "")) {
        %this.rawk(instrumentInUse.getAnimation(%keyCodeStr));
    }
    if ((%this SPC applauseMeterUse $= "sumo")) {
        return %this.onSumoKeys(%keyCodeStr, 1);
    }
    if ((%this SPC applauseMeterUse $= "blockgame")) {
        return %this.onBlockGameKeys(%keyCodeStr, 1);
    }
    return 1;
};
function ApplauseMeterGui::onKeyUp(%this, %unused, %keyCode) {
    %keyCodeStr = %this.getStringFromKeyCode(%keyCode);
    if ((%this SPC applauseMeterUse $= "applause")) {
        if ((%keyCodeStr $= " ")) {
            clap();
        }
    }
    if ((%this SPC applauseMeterUse $= "instrument")) {
        if ((%this SPC lastKeyDown $= %keyCodeStr)) {
            if ((%this SPC instrumentInUse $= "")) {
                warn(ApplauseMeterGui @ getScopeName() @ " " @ "- instrument not specified, using default stop animation '" @ InstrumentRegistryClient @ defaultStopAnimation @ "'");
                %anim = defaultStopAnimation;
                InstrumentRegistryClient;
                commandToServer('EtsPlayAnimName', %anim);
            }
            %anim = instrumentInUse.getStopAnimation();
            %this;
            commandToServer('EtsPlayAnimName', %anim);
        }
    }
    if ((%this SPC applauseMeterUse $= "sumo")) {
        return %this.onSumoKeys(%keyCodeStr, 0);
    }
    if ((%this SPC applauseMeterUse $= "blockgame")) {
        return %this.onBlockGameKeys(%keyCodeStr, 0);
    }
    return 1;
};
