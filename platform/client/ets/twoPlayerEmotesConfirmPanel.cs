function geTwoPlayerEmotesConfirmPanel::open(%this, %otherPlayerName, %coAnimName, %requestId) {
    %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
    if (!(isObject(%otherPlayer))) {
        error(getScopeName() @ " " @ "- could not find other player:" @ " " @ %otherPlayerName @ " " @ getTrace());
        return;
    }
    PlayGui.add(geTwoPlayerEmotesConfirmPanelBackground);
    1.setVisible();
    getWord(PlayGui.getExtent(), 0).resize(getWord(PlayGui.getExtent(), 1));
    0.reposition(0);
    PlayGui.focusAndRaise(geTwoPlayerEmotesConfirmPanelBackground);
    %this.ensureAdded();
    %this.setVisible(1);
    %this.focusAndRaise();
    setActionMapsEnabled(0);
    %this.otherPlayerName = PlayGui @ %otherPlayerName;
    PlayGui;
    %this.coAnimName = geTwoPlayerEmotesConfirmPanelBackground @ %coAnimName;
    geTwoPlayerEmotesConfirmPanelBackground;
    %this.requestID = geTwoPlayerEmotesConfirmPanelBackground @ %requestId;
    %this.countdownTick((1000.0 * 15.0));
    %this.refresh();
};
function geTwoPlayerEmotesConfirmPanel::close(%this, %accepted, %messageCode) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    0.setVisible();
    setActionMapsEnabled(1);
    if (!(isDefined("%accepted"))) {
        %accepted = 0;
        geTwoPlayerEmotesConfirmPanelBackground;
    }
    if (!(isDefined("%messageCode"))) {
        %messageCode = "DECLINE MANUAL";
    }
    %this.doAccept(%accepted, %messageCode);
    return 1;
};
function geTwoPlayerEmotesConfirmPanel::countdownTick(%this, %resetTimeRemainingMS) {
    if (isDefined("%resetTimeRemainingMS")) {
        %this.countdownMSRemaining = %resetTimeRemainingMS;
    }
    %tickPeriod = 100;
    %this.countdownMSRemaining = (%tickPeriod - %this.countdownMSRemaining);
    %this.rotRadians = (6.0 / (0.001 * %this.countdownMSRemaining)) @ geTwoPlayerEmotesConfirmClock_littleHand;
    %this.rotRadians = (0.001 * %this.countdownMSRemaining) @ geTwoPlayerEmotesConfirmClock_bigHand;
    %text = mFloor((0.5 + (0.001 * %this.countdownMSRemaining)));
    %text = %text @ "..";
    %text.setTextWithStyle();
    cancel(%this.countdownTimerID);
    if ((0.0 > %this.countdownMSRemaining)) {
    }
    if (%this.isVisible()) {
        %this.countdownTimerID = geTwoPlayerEmotesConfirmClock_readout @ %this.schedule(%tickPeriod, "countdownTick");
    }
    %this.close(0, "DECLINE TIMEOUT");
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
    %text.setTextWithStyle();
    %otherPlayerPortraitUrl = $Net::AvatarURL @ urlEncode(%this.otherPlayerName) @ "?size=M";
    geTwoPlayerEmotesConfirmOtherPlayerName;
    "platform/client/ui/tgf/tgf_profile_default_" @ %otherPlayer.getGender().setBitmap();
    %otherPlayerPortraitUrl.downloadAndApplyBitmap();
    %text = "Two-Player Action -" @ " " @ %this.coAnimName;
    geTwoPlayerEmotesConfirmOtherPlayerPortrait;
    %text.setTextWithStyle();
    %coAnimEntry = findCoAnimEntry(%this.coAnimName);
    geTwoPlayerEmotesConfirmTitle;
    %actionDesc = getField(%coAnimEntry, 6);
    geTwoPlayerEmotesConfirmOtherPlayerPortrait;
    %text = %actionDesc[$MsgCat::coanim @ "ACCEPT-OR-DECLINE"];
    %text = strreplace(%text, "[OTHERPLAYER]", %this.otherPlayerName);
    %text = strreplace(%text, "[ACTIONDESC]", %actionDesc);
    %text.setTextWithStyle();
};
