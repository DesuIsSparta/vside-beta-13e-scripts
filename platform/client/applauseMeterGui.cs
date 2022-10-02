function ApplauseMeterGui::open(%this, %applauseMeterUse, %arg)
{
    %applauseMeterUse = strlwr(%applauseMeterUse);
    if ((%this.applauseMeterUse $= "applause") && !((%applauseMeterUse $= "applause")))
    {
        ApplauseMeterGui.closeForApplause();
    }
    else
    {
        if ((%this.applauseMeterUse $= "instrument") && !((%applauseMeterUse $= "instrument")))
        {
            ApplauseMeterGui.closeForInstrument();
        }
        else
        {
            if ((%this.applauseMeterUse $= "sumo") && !((%applauseMeterUse $= "sumo")))
            {
                ApplauseMeterGui.closeForSumo();
            }
            else
            {
                if ((%this.applauseMeterUse $= "blockgame") && !((%applauseMeterUse $= "blockgame")))
                {
                    ApplauseMeterGui.closeForBlockGame();
                }
            }
        }
    }
    %this.applauseMeterUse = "";
    %focusAndRaise = 1;
    if (%applauseMeterUse $= "applause")
    {
        ApplauseMeterGui.openForApplause(%arg);
    }
    else
    {
        if (%applauseMeterUse $= "instrument")
        {
            %arg = strlwr(%arg);
            ApplauseMeterGui.openForInstrument(%arg);
            %focusAndRaise = 0;
        }
        else
        {
            if (%applauseMeterUse $= "sumo")
            {
                %arg = strlwr(%arg);
                ApplauseMeterGui.openForSumo(%arg);
            }
            else
            {
                if (%applauseMeterUse $= "blockgame")
                {
                    %arg = strlwr(%arg);
                    ApplauseMeterGui.openForBlockGame(%arg);
                }
                else
                {
                    error(getScopeName() SPC "- unknown use \'" SPC %applauseMeterUse SPC "\' for ApplauseMeterGui -" SPC getTrace());
                    return ;
                }
            }
        }
    }
    %this.applauseMeterUse = %applauseMeterUse;
    %this.closingFromServer = 0;
    %this.setVisible(1);
    if (%focusAndRaise)
    {
        PlayGui.focusAndRaise(%this);
    }
    return ;
}
function ApplauseMeterGui::scheduleApplaudMeterGuiClose(%this)
{
    %sched = gGetFieldWithDefault(%this, "closeApplauseMeterGuiSched", "");
    cancel(%sched);
    %sched = %this.schedule(5000, close);
    gSetField(%this, "closeApplauseMeterGuiSched", %sched);
    return ;
}
function ApplauseMeterGui::closeByUser(%this)
{
    if (%this.applauseMeterUse $= "sumo")
    {
        if (%this.sumoGameType $= "PillowFightGame")
        {
            MessageBoxOK($MsgCat::applauseGui["MSG-PILLOW-WARN"], $MsgCat::applauseGui["MSG-PILLOW-USERCLOSE"], "");
        }
        else
        {
            MessageBoxOK($MsgCat::applauseGui["MSG-SUMO-WARN"], $MsgCat::applauseGui["MSG-SUMO-USERCLOSE"], "");
        }
        return 0;
    }
    return %this.close();
}
function ApplauseMeterGui::close(%this)
{
    if (%this.applauseMeterUse $= "applause")
    {
        ApplauseMeterGui.closeForApplause();
    }
    else
    {
        if (%this.applauseMeterUse $= "instrument")
        {
            ApplauseMeterGui.closeForInstrument();
        }
        else
        {
            if (%this.applauseMeterUse $= "sumo")
            {
                ApplauseMeterGui.closeForSumo();
            }
            else
            {
                if (%this.applauseMeterUse $= "blockgame")
                {
                    ApplauseMeterGui.closeForBlockGame();
                }
                else
                {
                    if (!((%this.applauseMeterUse $= "")) && %this.isVisible())
                    {
                        error(getScopeName() SPC "- unknown use \'" @ %this.applauseMeterUse @ "\' for ApplauseMeterGui -" SPC getTrace());
                    }
                }
            }
        }
    }
    %this.applauseMeterUse = "";
    %this.closingFromServer = 0;
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
}
function ApplauseMeterGui::downplayChatBubble(%this)
{
    if (!%this.isVisible())
    {
        return 0;
    }
    if (%this.applauseMeterUse $= "blockgame")
    {
        return 1;
    }
    return 0;
}
function ApplauseMeterGui::scheduleGoIdle(%this)
{
    if (%this.applauseMeterUse $= "applause")
    {
        %sched = gGetFieldWithDefault(%this, "applaudeGoIdleSched", "");
        cancel(%sched);
        %sched = %this.schedule(750, applaudSetIdleIcon);
        gSetField(%this, "applaudeGoIdleSched", %sched);
    }
    else
    {
        if (%this.applauseMeterUse $= "instrument")
        {
            %sched = gGetFieldWithDefault(%this, "instrumentGoIdleSched", "");
            cancel(%sched);
            %sched = %this.schedule(750, instrumentSetIdleIcon);
            gSetField(%this, "instrumentGoIdleSched", %sched);
        }
        else
        {
            if (%this.applauseMeterUse $= "sumo")
            {
            }
            else
            {
                error(getScopeName() SPC "- unknown use \'" SPC %this.applauseMeterUse SPC "\' for ApplauseMeterGui -" SPC getTrace());
            }
        }
    }
    return ;
}
function ApplauseMeterGui::onSetFirstResponder(%this)
{
    %this.setGray(0);
    Parent::onSetFirstResponder(%this);
    return ;
}
function ApplauseMeterGui::onClearFirstResponder(%this)
{
    %sched = gGetFieldWithDefault(%this, "applaudeGoIdleSched", "");
    cancel(%sched);
    gSetField(%this, "applaudeGoIdleSched", "");
    %sched = gGetFieldWithDefault(%this, "instrumentGoIdleSched", "");
    cancel(%sched);
    gSetField(%this, "instrumentGoIdleSched", "");
    %this.setGray(1);
    Parent::onClearFirstResponder(%this);
    return ;
}
function ApplauseMeterGui::setGray(%this, %value)
{
    if (%this.applauseMeterUse $= "applause")
    {
        %this.setApplaudIcon(!%value, 1, %this.nonIdleStateA);
    }
    else
    {
        if (%this.applauseMeterUse $= "instrument")
        {
            %this.setInstrumentIcon(!%value, 1, %this.nonIdleStateA);
        }
        else
        {
            if (%this.applauseMeterUse $= "sumo")
            {
            }
        }
    }
    return ;
}
function ApplauseMeterGui::openForBlockGame(%this, %gameType)
{
    ApplauseMeterGui.setText($MsgCat::applauseGui["TITLE-BLOCKGAME-" @ %gameType]);
    ApplauseMeterInfoText.setText($MsgCat::applauseGui["BODYTEXT-BLOCKGAME-" @ %gameType]);
    %this.nonIdleStateA = 0;
    ApplauseMeterGui.alignToBottom();
    return ;
}
function ApplauseMeterGui::closeForBlockGame(%this)
{
    ApplauseMeterInfoText.setText("");
    if ($player.isSitting())
    {
        SendStandCommand(1);
    }
    return ;
}
$gBlockGameKeys = "" NL "I" NL "J" NL "K" NL "L" NL " " NL "left" NL "right" NL "up" NL "down" NL "lcontrol" NL "rcontrol";
function ApplauseMeterGui::onBlockGameKeys(%this, %keyCodeStr, %isKeyDown)
{
    %wantIt = %isKeyDown && (findRecord($gBlockGameKeys, %keyCodeStr) < 0) ? 0 : 1;
    if (%wantIt)
    {
        if (%keyCodeStr $= "I")
        {
            %keyCodeStr = "rotatecw";
        }
        else
        {
            if (%keyCodeStr $= "J")
            {
                %keyCodeStr = "left";
            }
            else
            {
                if (%keyCodeStr $= "K")
                {
                    %keyCodeStr = "rotateccw";
                }
                else
                {
                    if (%keyCodeStr $= "L")
                    {
                        %keyCodeStr = "right";
                    }
                    else
                    {
                        if (%keyCodeStr $= " ")
                        {
                            %keyCodeStr = "harddrop";
                        }
                    }
                }
            }
        }
        commandToServer('BlockGameMove', %keyCodeStr);
    }
    return %isKeyDown;
}
function ApplauseMeterGui::openForSumo(%this, %gameType)
{
    %this.sumoGameType = %gameType;
    if (%this.sumoGameType $= "PillowFightGame")
    {
        ApplauseMeterGui.setText($MsgCat::applauseGui["TITLE-PILLOW"]);
        ApplauseMeterInfoText.setText($MsgCat::applauseGui["BODYTEXT-PILLOW"]);
    }
    else
    {
        ApplauseMeterGui.setText($MsgCat::applauseGui["TITLE-SUMO"]);
        ApplauseMeterInfoText.setText($MsgCat::applauseGui["BODYTEXT-SUMO"]);
    }
    %this.nonIdleStateA = 0;
    ApplauseMeterGui.alignToBottom();
    getUserActivityMgr().setActivityActive("wrestling", 1);
    return ;
}
function ApplauseMeterGui::closeForSumo(%this)
{
    ApplauseMeterInfoText.setText("");
    %this.sumoGameType = "";
    getUserActivityMgr().setActivityActive("wrestling", 0);
    return ;
}
function ApplauseMeterGui::onSumoKeys(%this, %keyCodeStr, %isKeyDown)
{
    if (%keyCodeStr $= "left")
    {
        return 0;
    }
    else
    {
        if (%keyCodeStr $= "right")
        {
            return 0;
        }
        else
        {
            if (%keyCodeStr $= "up")
            {
                return 0;
            }
            else
            {
                if (%keyCodeStr $= "down")
                {
                    return 0;
                }
                else
                {
                    if (%keyCodeStr $= "\r")
                    {
                        return 0;
                    }
                }
            }
        }
    }
    if (%isKeyDown)
    {
        if (!(%this.sumoGameType $= ""))
        {
            if (%keyCodeStr $= "q")
            {
                commandToServer('SumoAction', %this.sumoGameType, 0);
            }
            else
            {
                if (%keyCodeStr $= "w")
                {
                    commandToServer('SumoAction', %this.sumoGameType, 1);
                }
                else
                {
                    if (%keyCodeStr $= "e")
                    {
                        commandToServer('SumoAction', %this.sumoGameType, 2);
                    }
                    else
                    {
                        if (%keyCodeStr $= "a")
                        {
                            commandToServer('SumoAction', %this.sumoGameType, 3);
                        }
                        else
                        {
                            if (%keyCodeStr $= "s")
                            {
                                commandToServer('SumoAction', %this.sumoGameType, 4);
                            }
                            else
                            {
                                if (%keyCodeStr $= "z")
                                {
                                    commandToServer('SumoAction', %this.sumoGameType, 5);
                                }
                                else
                                {
                                    if (%keyCodeStr $= "x")
                                    {
                                        commandToServer('SumoAction', %this.sumoGameType, 6);
                                    }
                                    else
                                    {
                                        if (%keyCodeStr $= " ")
                                        {
                                            jumpOnce();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return 1;
}
function ApplauseMeterGui::openForApplause(%this, %playerName)
{
    gSetField(%this, "applaudeGoIdleSched", "");
    gSetField(%this, "closeApplauseMeterGuiSched", "");
    ApplauseMeterGui.setText($MsgCat::applauseGui["TITLE-APPLAUSE"]);
    ApplauseMeterInfoText.setText($MsgCat::applauseGui["BODYTEXT-APPLAUSE"] SPC %playerName);
    commandToServer('SetMyApplauseTarget', %playerName);
    ApplauseMeterActionIconContainer.setVisible(1);
    %this.nonIdleStateA = 0;
    %this.setApplaudIcon(1, 1, %this.nonIdleStateA);
    return ;
}
function ApplauseMeterGui::closeForApplause(%this)
{
    ApplauseMeterActionIconContainer.setVisible(0);
    ApplauseMeterInfoText.setText("");
    %sched = gGetFieldWithDefault(%this, "applaudeGoIdleSched", "");
    cancel(%sched);
    gSetField(%this, "applaudeGoIdleSched", "");
    %sched = gGetFieldWithDefault(%this, "closeApplauseMeterGuiSched", "");
    cancel(%sched);
    gSetField(%this, "closeApplauseMeterGuiSched", "");
    commandToServer('SetMyApplauseTarget', "");
    return ;
}
function ApplauseMeterGui::clap(%this)
{
    if ($player.isSitting())
    {
        SendStandCommand(1);
        return ;
    }
    %this.animateApplaudIcon();
    sendAnimToServer("apls01");
    %this.scheduleGoIdle();
    %this.scheduleApplaudMeterGuiClose();
    return ;
}
function ApplauseMeterGui::applaudSetIdleIcon(%this)
{
    %this.nonIdleStateA = 0;
    %this.setApplaudIcon(1, 1, %this.nonIdleStateA);
    return ;
}
function ApplauseMeterGui::animateApplaudIcon(%this)
{
    %this.nonIdleStateA = !%this.nonIdleStateA;
    %this.setApplaudIcon(1, 0, %this.nonIdleStateA);
    return ;
}
function ApplauseMeterGui::setApplaudIcon(%this, %hasFocus, %isIdle, %nonIdleStateA)
{
    if (!%hasFocus)
    {
        ApplauseMeterActionActiveIconA.setVisible(0);
        ApplauseMeterActionActiveIconB.setVisible(0);
        ApplauseMeterActionUnfocusedIcon.setVisible(1);
        ApplauseMeterActionIdleIcon.setVisible(0);
        return ;
    }
    else
    {
        if (%isIdle)
        {
            ApplauseMeterActionUnfocusedIcon.setVisible(0);
            ApplauseMeterActionActiveIconA.setVisible(0);
            ApplauseMeterActionActiveIconB.setVisible(0);
            ApplauseMeterActionIdleIcon.setVisible(1);
            return ;
        }
        else
        {
            if (%nonIdleStateA)
            {
                ApplauseMeterActionUnfocusedIcon.setVisible(0);
                ApplauseMeterActionIdleIcon.setVisible(0);
                ApplauseMeterActionActiveIconB.setVisible(0);
                ApplauseMeterActionActiveIconA.setVisible(1);
                return ;
            }
            else
            {
                ApplauseMeterActionUnfocusedIcon.setVisible(0);
                ApplauseMeterActionIdleIcon.setVisible(0);
                ApplauseMeterActionActiveIconA.setVisible(0);
                ApplauseMeterActionActiveIconB.setVisible(1);
                return ;
            }
        }
    }
    return ;
}
function ApplauseMeterGui::openForInstrument(%this, %instrumentName)
{
    gSetField(%this, "instrumentGoIdleSched", "");
    gSetField(%this, "closeApplauseMeterGuiSched", "");
    %schedule = gGetFieldWithDefault(%this, "animateInstrumentIconSchedule", "");
    cancel(%schedule);
    gSetField(%this, "animateInstrumentIconSchedule", "");
    %instrument = InstrumentRegistryClient.getInstrumentObject(%instrumentName);
    if (!isObject(%instrument))
    {
        error(getScopeName() SPC "- can\'t find instrument \'" @ %instrumentName @ "\', not opening instrument game -" SPC getTrace());
        %this.close();
        return ;
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
    return ;
}
function ApplauseMeterGui::closeForInstrument(%this)
{
    %this.rawk(InstrumentRegistryClient.getStopAnimation(%this.instrumentInUse));
    %this.closedForInstrument = 1;
    %this.instrumentInUse = "";
    %schedule = gGetFieldWithDefault(%this, "animateInstrumentIconSchedule", "");
    cancel(%schedule);
    gSetField(%this, "animateInstrumentIconSchedule", "");
    if (!%this.closingFromServer)
    {
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
    return ;
}
function toggleInstrumentGame(%instrument)
{
    if (ApplauseMeterGui.isVisible() && (ApplauseMeterGui.applauseMeterUse $= "instrument"))
    {
        ApplauseMeterGui.close();
    }
    else
    {
        ApplauseMeterGui.open("instrument", %instrument);
    }
    return ;
}
function toggleGuitarGame()
{
    toggleInstrumentGame("guitar");
    return ;
}
function clientCmdOpenGameControls(%gameType, %arg)
{
    if (!isObject(ApplauseMeterGui))
    {
        return ;
    }
    if (%gameType $= "")
    {
        return ;
    }
    if (!(ApplauseMeterGui.applauseMeterUse $= %gameType))
    {
        ApplauseMeterGui.open(%gameType, %arg);
    }
    return ;
}
function clientCmdCloseGameControls(%gameType, %arg)
{
    if (!isObject(ApplauseMeterGui))
    {
        return ;
    }
    if (ApplauseMeterGui.applauseMeterUse $= %gameType)
    {
        ApplauseMeterGui.close();
    }
    return ;
}
function clientCmdDisableInstrumentGame()
{
    if (!ApplauseMeterGui.closedForInstrument)
    {
        ApplauseMeterGui.closeForInstrument();
        InstrumentActionIconContainer.setVisible(0);
        ApplauseMeterInfoText.setText(InstrumentRegistryClient.getInstrumentObject(%instrumentName).disabledText);
    }
    return ;
}
function ApplauseMeterGui::rawk(%this, %anim)
{
    if ($player.isSitting())
    {
        SendStandCommand(1);
        return ;
    }
    if (!(%anim $= ""))
    {
        commandToServer('PlayInstrumentGameAnim', %this.instrumentInUse, %anim);
    }
    %schedule = gGetFieldWithDefault(%this, "animateInstrumentIconSchedule", "");
    if (%schedule $= "")
    {
        %schedule = %this.schedule(300, animateInstrumentIcon);
        gSetField(%this, "animateInstrumentIconSchedule", %schedule);
    }
    %this.scheduleGoIdle();
    return ;
}
function ApplauseMeterGui::instrumentSetIdleIcon(%this)
{
    %this.nonIdleStateA = 0;
    %this.setInstrumentIcon(1, 1, %this.nonIdleStateA);
    return ;
}
function ApplauseMeterGui::animateInstrumentIcon(%this)
{
    %schedule = gGetFieldWithDefault(%this, "animateInstrumentIconSchedule", "");
    cancel(%schedule);
    gSetField(%this, "animateInstrumentIconSchedule", "");
    %this.nonIdleStateA = !%this.nonIdleStateA;
    %this.setInstrumentIcon(1, 0, %this.nonIdleStateA);
    return ;
}
function ApplauseMeterGui::setInstrumentIcon(%this, %hasFocus, %isIdle, %nonIdleStateA)
{
    if (!%hasFocus)
    {
        InstrumentActionActiveIconA.setVisible(0);
        InstrumentActionActiveIconB.setVisible(0);
        InstrumentActionUnfocusedIcon.setVisible(1);
        InstrumentActionIdleIcon.setVisible(0);
        return ;
    }
    else
    {
        if (%isIdle)
        {
            InstrumentActionUnfocusedIcon.setVisible(0);
            InstrumentActionActiveIconA.setVisible(0);
            InstrumentActionActiveIconB.setVisible(0);
            InstrumentActionIdleIcon.setVisible(1);
            return ;
        }
        else
        {
            if (%nonIdleStateA)
            {
                InstrumentActionUnfocusedIcon.setVisible(0);
                InstrumentActionIdleIcon.setVisible(0);
                InstrumentActionActiveIconB.setVisible(0);
                InstrumentActionActiveIconA.setVisible(1);
                return ;
            }
            else
            {
                InstrumentActionUnfocusedIcon.setVisible(0);
                InstrumentActionIdleIcon.setVisible(0);
                InstrumentActionActiveIconA.setVisible(0);
                InstrumentActionActiveIconB.setVisible(1);
                return ;
            }
        }
    }
    return ;
}
function ApplauseMeterGui::onKeyDown(%this, %unused, %keyCode)
{
    setIdle(0);
    %keyCodeStr = %this.getStringFromKeyCode(%keyCode);
    %this.lastKeyDown = %keyCodeStr;
    if ((%this.applauseMeterUse $= "instrument") && !((%this.instrumentInUse $= "")))
    {
        %this.rawk(InstrumentRegistryClient.getAnimation(%this.instrumentInUse, %keyCodeStr));
    }
    else
    {
        if (%this.applauseMeterUse $= "sumo")
        {
            return %this.onSumoKeys(%keyCodeStr, 1);
        }
        else
        {
            if (%this.applauseMeterUse $= "blockgame")
            {
                return %this.onBlockGameKeys(%keyCodeStr, 1);
            }
        }
    }
    return 1;
}
function ApplauseMeterGui::onKeyUp(%this, %unused, %keyCode)
{
    %keyCodeStr = %this.getStringFromKeyCode(%keyCode);
    if (%this.applauseMeterUse $= "applause")
    {
        if (%keyCodeStr $= " ")
        {
            ApplauseMeterGui.clap();
        }
    }
    else
    {
        if (%this.applauseMeterUse $= "instrument")
        {
            if (%this.lastKeyDown $= %keyCodeStr)
            {
                if (%this.instrumentInUse $= "")
                {
                    warn(getScopeName() SPC "- instrument not specified, using default stop animation \'" @ InstrumentRegistryClient.defaultStopAnimation @ "\'");
                    %anim = InstrumentRegistryClient.defaultStopAnimation;
                    commandToServer('EtsPlayAnimName', %anim);
                }
                else
                {
                    %anim = InstrumentRegistryClient.getStopAnimation(%this.instrumentInUse);
                    commandToServer('EtsPlayAnimName', %anim);
                }
            }
        }
        else
        {
            if (%this.applauseMeterUse $= "sumo")
            {
                return %this.onSumoKeys(%keyCodeStr, 0);
            }
            else
            {
                if (%this.applauseMeterUse $= "blockgame")
                {
                    return %this.onBlockGameKeys(%keyCodeStr, 0);
                }
            }
        }
    }
    return 1;
}
