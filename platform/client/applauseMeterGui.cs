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
        ApplauseMeterGui.openForApplause(%arg);
    }
    if ((%applauseMeterUse $= "instrument")) {
        %arg = strlwr(%arg);
        ApplauseMeterGui.openForInstrument(%arg);
        %focusAndRaise = 0;
    }
    if ((%applauseMeterUse $= "sumo")) {
        %arg = strlwr(%arg);
        ApplauseMeterGui.openForSumo(%arg);
    }
    if ((%applauseMeterUse $= "blockgame")) {
        %arg = strlwr(%arg);
        ApplauseMeterGui.openForBlockGame(%arg);
    }
    error(getScopeName() @ " " @ "- unknown use '" @ " " @ %applauseMeterUse @ " " @ "' for ApplauseMeterGui -" @ " " @ getTrace());
    return;
    %this.applauseMeterUse = %applauseMeterUse;
    %this.closingFromServer = 0;
    %this.setVisible(1);
    if (%focusAndRaise) {
        PlayGui.focusAndRaise(%this);
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
    if ((%this.applauseMeterUse $= "sumo")) {
        if ((%this.sumoGameType $= "PillowFightGame")) {
            MessageBoxOK(%this[$MsgCat::applauseGui @ "MSG-PILLOW-WARN"], , "");
        }
        MessageBoxOK(, , "");
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
    %this.setVisible(0);
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
        %sched = %this.schedule(750);
        applaudSetIdleIcon;
        gSetField(%this, "applaudeGoIdleSched", %sched);
    }
    if ((%this.applauseMeterUse $= "instrument")) {
        %sched = gGetFieldWithDefault(%this, "instrumentGoIdleSched", "");
        cancel(%sched);
        %sched = %this.schedule(750);
        instrumentSetIdleIcon;
        gSetField(%this, "instrumentGoIdleSched", %sched);
    }
    if ((%this.applauseMeterUse $= "sumo")) {
    }
    error(getScopeName() @ " " @ "- unknown use '" @ " " @ %this.applauseMeterUse @ " " @ "' for ApplauseMeterGui -" @ " " @ getTrace());
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
    if ((%this.applauseMeterUse $= "applause")) {
        %this.setApplaudIcon(!(%value), 1, %this.nonIdleStateA);
    }
    if ((%this.applauseMeterUse $= "instrument")) {
        %this.setInstrumentIcon(!(%value), 1, %this.nonIdleStateA);
    }
};
function ApplauseMeterGui::openForBlockGame(%this, %gameType) {
    ApplauseMeterGui.setText(%gameType[$MsgCat::applauseGui @ "TITLE-BLOCKGAME-" @ %gameType]);
    ApplauseMeterInfoText.setText(%gameType[$MsgCat::applauseGui @ "BODYTEXT-BLOCKGAME-" @ %gameType]);
    %this.nonIdleStateA = 0;
    ApplauseMeterGui.alignToBottom();
};
function ApplauseMeterGui::closeForBlockGame(%this) {
    ApplauseMeterInfoText.setText("");
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
    %this.sumoGameType = %gameType;
    if ((%this.sumoGameType $= "PillowFightGame")) {
        ApplauseMeterGui.setText(%this[$MsgCat::applauseGui @ "TITLE-PILLOW"]);
        ApplauseMeterInfoText.setText();
    }
    ApplauseMeterGui.setText();
    ApplauseMeterInfoText.setText();
    %this.nonIdleStateA = 0;
    ApplauseMeterGui.alignToBottom();
    getUserActivityMgr().setActivityActive("wrestling", 1);
};
function ApplauseMeterGui::closeForSumo(%this) {
    ApplauseMeterInfoText.setText("");
    %this.sumoGameType = "";
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
        if (!(%this.sumoGameType $= "")) {
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
    }
    return 1;
};
function ApplauseMeterGui::openForApplause(%this, %playerName) {
    gSetField(%this, "applaudeGoIdleSched", "");
    gSetField(%this, "closeApplauseMeterGuiSched", "");
    ApplauseMeterGui.setText();
    ApplauseMeterInfoText @ " " @ %playerName.setText();
    commandToServer('SetMyApplauseTarget', %playerName);
    ApplauseMeterActionIconContainer.setVisible(1);
    %this.nonIdleStateA = 0;
    %this.setApplaudIcon(1, 1, %this.nonIdleStateA);
};
function ApplauseMeterGui::closeForApplause(%this) {
    ApplauseMeterActionIconContainer.setVisible(0);
    ApplauseMeterInfoText.setText("");
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
    %this.setApplaudIcon(1, 1, %this.nonIdleStateA);
};
function ApplauseMeterGui::animateApplaudIcon(%this) {
    %this.nonIdleStateA = !(%this.nonIdleStateA);
    %this.setApplaudIcon(1, 0, %this.nonIdleStateA);
};
function ApplauseMeterGui::setApplaudIcon(%this, %hasFocus, %isIdle, %nonIdleStateA) {
    if (!(%hasFocus)) {
        ApplauseMeterActionActiveIconA.setVisible(0);
        ApplauseMeterActionActiveIconB.setVisible(0);
        ApplauseMeterActionUnfocusedIcon.setVisible(1);
        ApplauseMeterActionIdleIcon.setVisible(0);
        return;
    }
    if (%isIdle) {
        ApplauseMeterActionUnfocusedIcon.setVisible(0);
        ApplauseMeterActionActiveIconA.setVisible(0);
        ApplauseMeterActionActiveIconB.setVisible(0);
        ApplauseMeterActionIdleIcon.setVisible(1);
        return;
    }
    if (%nonIdleStateA) {
        ApplauseMeterActionUnfocusedIcon.setVisible(0);
        ApplauseMeterActionIdleIcon.setVisible(0);
        ApplauseMeterActionActiveIconB.setVisible(0);
        ApplauseMeterActionActiveIconA.setVisible(1);
        return;
    }
    ApplauseMeterActionUnfocusedIcon.setVisible(0);
    ApplauseMeterActionIdleIcon.setVisible(0);
    ApplauseMeterActionActiveIconA.setVisible(0);
    ApplauseMeterActionActiveIconB.setVisible(1);
    return;
};
function ApplauseMeterGui::openForInstrument(%this, %instrumentName) {
    gSetField(%this, "instrumentGoIdleSched", "");
    gSetField(%this, "closeApplauseMeterGuiSched", "");
    %schedule = gGetFieldWithDefault(%this, "animateInstrumentIconSchedule", "");
    cancel(%schedule);
    gSetField(%this, "animateInstrumentIconSchedule", "");
    %instrument = InstrumentRegistryClient.getInstrumentObject(%instrumentName);
    if (!(isObject(%instrument))) {
        error(getScopeName() @ " " @ "- can't find instrument '" @ %instrumentName @ "', not opening instrument game -" @ " " @ getTrace());
        %this.close();
        return;
    }
    %this.instrumentInUse = %instrumentName;
    ApplauseMeterGui.setText(%instrument.titleText);
    ApplauseMeterInfoText.setText(%instrument.bodyText);
    InstrumentActionActiveIconA.setBitmap(%instrument.activeIconA);
    InstrumentActionActiveIconB.setBitmap(%instrument.activeIconB);
    InstrumentActionIdleIcon.setBitmap(%instrument.idleIcon);
    InstrumentActionUnfocusedIcon.setBitmap(%instrument.unfocusedIcon);
    InstrumentActionIconContainer.setVisible(1);
    %this.nonIdleStateA = 0;
    %this.setInstrumentIcon(1, 1, %this.nonIdleStateA);
    %this.closedForInstrument = 0;
};
function ApplauseMeterGui::closeForInstrument(%this) {
    %this.rawk(InstrumentRegistryClient.getStopAnimation(%this.instrumentInUse));
    %this.closedForInstrument = 1;
    %this.instrumentInUse = "";
    %schedule = gGetFieldWithDefault(%this, "animateInstrumentIconSchedule", "");
    cancel(%schedule);
    gSetField(%this, "animateInstrumentIconSchedule", "");
    if (!(%this.closingFromServer)) {
        commandToServer('DropInstrumentOnClosingInstrumentInterface');
    }
    InstrumentActionIconContainer.setVisible(0);
    ApplauseMeterInfoText.setText("");
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
    if ((ApplauseMeterGui @ " " @ %this.applauseMeterUse $= "instrument")) {
        ApplauseMeterGui.close();
    }
    ApplauseMeterGui.open("instrument", %instrument);
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
    if (!(ApplauseMeterGui @ " " @ %this.applauseMeterUse $= %gameType)) {
        ApplauseMeterGui.open(%gameType, %arg);
    }
};
function clientCmdCloseGameControls(%gameType, %arg) {
    if (!(isObject(ApplauseMeterGui))) {
        return;
    }
    if ((ApplauseMeterGui @ " " @ %this.applauseMeterUse $= %gameType)) {
        ApplauseMeterGui.close();
    }
};
function clientCmdDisableInstrumentGame() {
    if (!(%this.closedForInstrument)) {
        ApplauseMeterGui.closeForInstrument();
        InstrumentActionIconContainer.setVisible(0);
        ApplauseMeterInfoText.setText(InstrumentRegistryClient.getInstrumentObject(%instrumentName).disabledText);
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
        %schedule = %this.schedule(300);
        animateInstrumentIcon;
        gSetField(%this, "animateInstrumentIconSchedule", %schedule);
    }
    %this.scheduleGoIdle();
};
function ApplauseMeterGui::instrumentSetIdleIcon(%this) {
    %this.nonIdleStateA = 0;
    %this.setInstrumentIcon(1, 1, %this.nonIdleStateA);
};
function ApplauseMeterGui::animateInstrumentIcon(%this) {
    %schedule = gGetFieldWithDefault(%this, "animateInstrumentIconSchedule", "");
    cancel(%schedule);
    gSetField(%this, "animateInstrumentIconSchedule", "");
    %this.nonIdleStateA = !(%this.nonIdleStateA);
    %this.setInstrumentIcon(1, 0, %this.nonIdleStateA);
};
function ApplauseMeterGui::setInstrumentIcon(%this, %hasFocus, %isIdle, %nonIdleStateA) {
    if (!(%hasFocus)) {
        InstrumentActionActiveIconA.setVisible(0);
        InstrumentActionActiveIconB.setVisible(0);
        InstrumentActionUnfocusedIcon.setVisible(1);
        InstrumentActionIdleIcon.setVisible(0);
        return;
    }
    if (%isIdle) {
        InstrumentActionUnfocusedIcon.setVisible(0);
        InstrumentActionActiveIconA.setVisible(0);
        InstrumentActionActiveIconB.setVisible(0);
        InstrumentActionIdleIcon.setVisible(1);
        return;
    }
    if (%nonIdleStateA) {
        InstrumentActionUnfocusedIcon.setVisible(0);
        InstrumentActionIdleIcon.setVisible(0);
        InstrumentActionActiveIconB.setVisible(0);
        InstrumentActionActiveIconA.setVisible(1);
        return;
    }
    InstrumentActionUnfocusedIcon.setVisible(0);
    InstrumentActionIdleIcon.setVisible(0);
    InstrumentActionActiveIconA.setVisible(0);
    InstrumentActionActiveIconB.setVisible(1);
    return;
};
function ApplauseMeterGui::onKeyDown(%this, %unused, %keyCode) {
    setIdle(0);
    %keyCodeStr = %this.getStringFromKeyCode(%keyCode);
    %this.lastKeyDown = %keyCodeStr;
    if ((%this.applauseMeterUse $= "instrument")) {
    }
    if (!(%this.instrumentInUse $= "")) {
        %this.rawk(InstrumentRegistryClient.getAnimation(%this.instrumentInUse, %keyCodeStr));
    }
    if ((%this.applauseMeterUse $= "sumo")) {
        return %this.onSumoKeys(%keyCodeStr, 1);
    }
    if ((%this.applauseMeterUse $= "blockgame")) {
        return %this.onBlockGameKeys(%keyCodeStr, 1);
    }
    return 1;
};
function ApplauseMeterGui::onKeyUp(%this, %unused, %keyCode) {
    %keyCodeStr = %this.getStringFromKeyCode(%keyCode);
    if ((%this.applauseMeterUse $= "applause")) {
        if ((%keyCodeStr $= " ")) {
            ApplauseMeterGui.clap();
        }
    }
    if ((%this.applauseMeterUse $= "instrument")) {
        if ((%this.lastKeyDown $= %keyCodeStr)) {
            if ((%this.instrumentInUse $= "")) {
                warn(getScopeName() @ " " @ "- instrument not specified, using default stop animation '", InstrumentRegistryClient @ %this.defaultStopAnimation @ "'");
                %anim = %this.defaultStopAnimation;
                InstrumentRegistryClient;
                commandToServer('EtsPlayAnimName', %anim);
            }
            %anim = InstrumentRegistryClient.getStopAnimation(%this.instrumentInUse);
            commandToServer('EtsPlayAnimName', %anim);
        }
    }
    if ((%this.applauseMeterUse $= "sumo")) {
        return %this.onSumoKeys(%keyCodeStr, 0);
    }
    if ((%this.applauseMeterUse $= "blockgame")) {
        return %this.onBlockGameKeys(%keyCodeStr, 0);
    }
    return 1;
};
