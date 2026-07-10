function geTwoPlayerEmotesConfirmPanel::open(%this, %otherPlayerName, %coAnimName, %requestId) {
    %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
    if (!(isObject(%otherPlayer))) {
        error(getScopeName() @ " " @ "- could not find other player:" @ " " @ %otherPlayerName @ " " @ getTrace());
        return;
    }
    geTwoPlayerEmotesConfirmPanelBackground.add(PlayGui);
    1.setVisible(geTwoPlayerEmotesConfirmPanelBackground);
    getWord(PlayGui.getExtent(), 1).resize(geTwoPlayerEmotesConfirmPanelBackground, getWord(PlayGui.getExtent(), 0));
    0.reposition(geTwoPlayerEmotesConfirmPanelBackground, 0);
    geTwoPlayerEmotesConfirmPanelBackground.focusAndRaise(PlayGui);
    %this.ensureAdded(PlayGui);
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    setActionMapsEnabled(0);
    %this.otherPlayerName = %otherPlayerName;
    %this.coAnimName = %coAnimName;
    %this.requestID = %requestId;
    (15.0 * 1000.0).countdownTick(%this);
    %this.refresh();
};
function geTwoPlayerEmotesConfirmPanel::close(%this, %accepted, %messageCode) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    0.setVisible(geTwoPlayerEmotesConfirmPanelBackground);
    setActionMapsEnabled(1);
    if (!(isDefined("%accepted"))) {
        %accepted = 0;
    }
    if (!(isDefined("%messageCode"))) {
        %messageCode = "DECLINE MANUAL";
    }
    %messageCode.doAccept(%this, %accepted);
    return 1;
};
function geTwoPlayerEmotesConfirmPanel::countdownTick(%this, %resetTimeRemainingMS) {
    if (isDefined("%resetTimeRemainingMS")) {
        %this.countdownMSRemaining = %resetTimeRemainingMS;
    }
    %tickPeriod = 100;
    %this.countdownMSRemaining = (%this.countdownMSRemaining - %tickPeriod);
    %this.rotRadians = ((%this.countdownMSRemaining * 0.001) / 6.0) @ geTwoPlayerEmotesConfirmClock_littleHand;
    %this.rotRadians = (%this.countdownMSRemaining * 0.001) @ geTwoPlayerEmotesConfirmClock_bigHand;
    %text = mFloor(((%this.countdownMSRemaining * 0.001) + 0.5));
    %text = %text @ "..";
    %text.setTextWithStyle(geTwoPlayerEmotesConfirmClock_readout);
    cancel(%this.countdownTimerID);
    if ((%this.countdownMSRemaining > 0.0)) {
    }
    if (%this.isVisible()) {
        %this.countdownTimerID = "countdownTick".schedule(%this, %tickPeriod);
    }
    "DECLINE TIMEOUT".close(%this, 0);
    %coAnimEntry = findCoAnimEntry(%this.coAnimName);
    %actionDesc = getField(%coAnimEntry, 6);
    %text = %actionDesc[$MsgCat::coanim @ "E-TOOSLOW"];
    %text = strreplace(%text, "[OTHERPLAYER]", %this.otherPlayerName);
    %text = strreplace(%text, "[ACTIONDESC]", %actionDesc);
    handleSystemMessage("msgInfoMessage", %text);
};
function geTwoPlayerEmotesConfirmPanel::doAccept(%this, %accepted, %messageCode) {
    cancel(%this.countdownTimerID);
    commandToServer('CoAnimRespond', %this.requestID, %messageCode);
};
function geTwoPlayerEmotesConfirmPanel::refresh(%this) {
    %otherPlayer = Player::findPlayerInstance(%this.otherPlayerName);
    if (!(isObject(%otherPlayer))) {
        error(getScopeName() @ " " @ "- couldn't find target player:" @ " " @ %this.otherPlayerName);
        %this.close();
        return;
    }
    %text = "<just:right><clip:1000>" @ %this.otherPlayerName;
    %text.setTextWithStyle(geTwoPlayerEmotesConfirmOtherPlayerName);
    %otherPlayerPortraitUrl = $Net::AvatarURL @ urlEncode(%this.otherPlayerName) @ "?size=M";
    "platform/client/ui/tgf/tgf_profile_default_" @ %otherPlayer.getGender().setBitmap(geTwoPlayerEmotesConfirmOtherPlayerPortrait);
    %otherPlayerPortraitUrl.downloadAndApplyBitmap(geTwoPlayerEmotesConfirmOtherPlayerPortrait);
    %text = "Two-Player Action -" @ " " @ %this.coAnimName;
    %text.setTextWithStyle(geTwoPlayerEmotesConfirmTitle);
    %coAnimEntry = findCoAnimEntry(%this.coAnimName);
    %actionDesc = getField(%coAnimEntry, 6);
    %text = %actionDesc[$MsgCat::coanim @ "ACCEPT-OR-DECLINE"];
    %text = strreplace(%text, "[OTHERPLAYER]", %this.otherPlayerName);
    %text = strreplace(%text, "[ACTIONDESC]", %actionDesc);
    %text.setTextWithStyle(geTwoPlayerEmotesConfirmTextAcceptDecline);
};
