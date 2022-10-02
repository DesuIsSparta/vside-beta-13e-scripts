function geTwoPlayerEmotesConfirmPanel::open(%this, %otherPlayerName, %coAnimName, %requestId)
{
    %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
    if (!isObject(%otherPlayer))
    {
        error(getScopeName() SPC "- could not find other player:" SPC %otherPlayerName SPC getTrace());
        return ;
    }
    PlayGui.add(geTwoPlayerEmotesConfirmPanelBackground);
    geTwoPlayerEmotesConfirmPanelBackground.setVisible(1);
    geTwoPlayerEmotesConfirmPanelBackground.resize(getWord(PlayGui.getExtent(), 0), getWord(PlayGui.getExtent(), 1));
    geTwoPlayerEmotesConfirmPanelBackground.reposition(0, 0);
    PlayGui.focusAndRaise(geTwoPlayerEmotesConfirmPanelBackground);
    PlayGui.ensureAdded(%this);
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    setActionMapsEnabled(0);
    %this.otherPlayerName = %otherPlayerName;
    %this.coAnimName = %coAnimName;
    %this.requestID = %requestId;
    %this.countdownTick(15 * 1000);
    %this.refresh();
    return ;
}
function geTwoPlayerEmotesConfirmPanel::close(%this, %accepted, %messageCode)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    geTwoPlayerEmotesConfirmPanelBackground.setVisible(0);
    setActionMapsEnabled(1);
    if (!isDefined("%accepted"))
    {
        %accepted = 0;
    }
    if (!isDefined("%messageCode"))
    {
        %messageCode = "DECLINE MANUAL";
    }
    %this.doAccept(%accepted, %messageCode);
    return 1;
}
function geTwoPlayerEmotesConfirmPanel::countdownTick(%this, %resetTimeRemainingMS)
{
    if (isDefined("%resetTimeRemainingMS"))
    {
        %this.countdownMSRemaining = %resetTimeRemainingMS;
    }
    %tickPeriod = 100;
    %this.countdownMSRemaining = %this.countdownMSRemaining - %tickPeriod;
    geTwoPlayerEmotesConfirmClock_littleHand.rotRadians = (%this.countdownMSRemaining * 0.001) / 6;
    geTwoPlayerEmotesConfirmClock_bigHand.rotRadians = %this.countdownMSRemaining * 0.001;
    %text = mFloor((%this.countdownMSRemaining * 0.001) + 0.5);
    %text = %text @ "..";
    geTwoPlayerEmotesConfirmClock_readout.setTextWithStyle(%text);
    cancel(%this.countdownTimerID);
    if ((%this.countdownMSRemaining > 0) && %this.isVisible())
    {
        %this.countdownTimerID = %this.schedule(%tickPeriod, "countdownTick");
    }
    else
    {
        %this.close(0, "DECLINE TIMEOUT");
        %coAnimEntry = findCoAnimEntry(%this.coAnimName);
        %actionDesc = getField(%coAnimEntry, 6);
        %text = $MsgCat::coanim["E-TOOSLOW"];
        %text = strreplace(%text, "[OTHERPLAYER]", %this.otherPlayerName);
        %text = strreplace(%text, "[ACTIONDESC]", %actionDesc);
        handleSystemMessage("msgInfoMessage", %text);
    }
    return ;
}
function geTwoPlayerEmotesConfirmPanel::doAccept(%this, %accepted, %messageCode)
{
    cancel(%this.countdownTimerID);
    commandToServer('CoAnimRespond', %this.requestID, %messageCode);
    return ;
}
function geTwoPlayerEmotesConfirmPanel::refresh(%this)
{
    %otherPlayer = Player::findPlayerInstance(%this.otherPlayerName);
    if (!isObject(%otherPlayer))
    {
        error(getScopeName() SPC "- couldn\'t find target player:" SPC %this.otherPlayerName);
        %this.close();
        return ;
    }
    %text = "<just:right><clip:1000>" @ %this.otherPlayerName;
    geTwoPlayerEmotesConfirmOtherPlayerName.setTextWithStyle(%text);
    %otherPlayerPortraitUrl = $Net::AvatarURL @ urlEncode(%this.otherPlayerName) @ "?size=M";
    geTwoPlayerEmotesConfirmOtherPlayerPortrait.setBitmap("platform/client/ui/tgf/tgf_profile_default_" @ %otherPlayer.getGender());
    geTwoPlayerEmotesConfirmOtherPlayerPortrait.downloadAndApplyBitmap(%otherPlayerPortraitUrl);
    %text = "Two-Player Action -" SPC %this.coAnimName;
    geTwoPlayerEmotesConfirmTitle.setTextWithStyle(%text);
    %coAnimEntry = findCoAnimEntry(%this.coAnimName);
    %actionDesc = getField(%coAnimEntry, 6);
    %text = $MsgCat::coanim["ACCEPT-OR-DECLINE"];
    %text = strreplace(%text, "[OTHERPLAYER]", %this.otherPlayerName);
    %text = strreplace(%text, "[ACTIONDESC]", %actionDesc);
    geTwoPlayerEmotesConfirmTextAcceptDecline.setTextWithStyle(%text);
    return ;
}
