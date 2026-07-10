function geTwoPlayerEmotesConfirmPanel::open(%this, %otherPlayerName, %coAnimName, %requestId) {
    %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
    if (!(isObject(%otherPlayer))) {
        error(getScopeName() @ " " @ "- could not find other player:" @ " " @ %otherPlayerName @ " " @ getTrace());
        return;
    }
    add();
    1.setVisible();
    getWord(getExtent(), 0).resize(getWord(getExtent(), 1));
    0.reposition(0);
    focusAndRaise();
    %this.ensureAdded();
    %this.setVisible(1);
    %this.focusAndRaise();
    setActionMapsEnabled(0);
    otherPlayerName = PlayGui @ %otherPlayerName @ %this;
    PlayGui;
    coAnimName = geTwoPlayerEmotesConfirmPanelBackground @ %coAnimName @ %this;
    PlayGui;
    requestID = geTwoPlayerEmotesConfirmPanelBackground @ %requestId @ %this;
    PlayGui;
    %this.countdownTick((1000.0 * 15.0));
    %this.refresh();
};
function geTwoPlayerEmotesConfirmPanel::close(%this, %accepted, %messageCode) {
    %this.setVisible(0);
    focusTopWindow();
    0.setVisible();
    setActionMapsEnabled(1);
    if (!(isDefined("%accepted"))) {
        %accepted = 0;
        geTwoPlayerEmotesConfirmPanelBackground;
    }
    if (!(isDefined("%messageCode"))) {
        %messageCode = "DECLINE MANUAL";
        PlayGui;
    }
    %this.doAccept(%accepted, %messageCode);
    return 1;
};
function geTwoPlayerEmotesConfirmPanel::countdownTick(%this, %resetTimeRemainingMS) {
    if (isDefined("%resetTimeRemainingMS")) {
        countdownMSRemaining = %resetTimeRemainingMS @ %this;
    }
    %tickPeriod = 100;
    countdownMSRemaining = (%this - countdownMSRemaining);
    %tickPeriod;
    rotRadians = 6.0 @ (0.001 / (%this * countdownMSRemaining)) @ geTwoPlayerEmotesConfirmClock_littleHand;
    rotRadians = 0.001 @ (%this * countdownMSRemaining) @ geTwoPlayerEmotesConfirmClock_bigHand;
    %text = mFloor((0.001 + (%this * countdownMSRemaining)));
    0.5;
    %text = %text @ "..";
    %text.setTextWithStyle();
    cancel(countdownTimerID);
    if ((%this > countdownMSRemaining)) {
    }
    if (%this.isVisible()) {
        countdownTimerID = 0.0 @ %this.schedule(%tickPeriod, "countdownTick") @ %this;
        %this;
    }
    %this.close(0, "DECLINE TIMEOUT");
    %coAnimEntry = findCoAnimEntry(coAnimName);
    %this;
    %actionDesc = getField(%coAnimEntry, 6);
    geTwoPlayerEmotesConfirmClock_readout;
    %text = %actionDesc[$MsgCat::coanim @ "E-TOOSLOW"];
    %text = strreplace(%text, "[OTHERPLAYER]", otherPlayerName);
    %this;
    %text = strreplace(%text, "[ACTIONDESC]", %actionDesc);
    handleSystemMessage("msgInfoMessage", %text);
};
function geTwoPlayerEmotesConfirmPanel::doAccept(%this, %accepted, %messageCode) {
    cancel(countdownTimerID);
    commandToServer('CoAnimRespond', requestID, %messageCode);
};
function geTwoPlayerEmotesConfirmPanel::refresh(%this) {
    %otherPlayer = Player::findPlayerInstance(otherPlayerName);
    %this;
    if (!(isObject(%otherPlayer))) {
        error(%this @ otherPlayerName);
        %this.close();
        return getScopeName() @ " " @ "- couldn't find target player:" @ " ";
    }
    %text = %this @ otherPlayerName;
    "<just:right><clip:1000>";
    %text.setTextWithStyle();
    %otherPlayerPortraitUrl = geTwoPlayerEmotesConfirmOtherPlayerName @ $Net::AvatarURL @ %this @ urlEncode(otherPlayerName) @ "?size=M";
    geTwoPlayerEmotesConfirmOtherPlayerPortrait @ "platform/client/ui/tgf/tgf_profile_default_" @ %otherPlayer.getGender().setBitmap();
    %otherPlayerPortraitUrl.downloadAndApplyBitmap();
    %text = %this @ coAnimName;
    "Two-Player Action -" @ " ";
    %text.setTextWithStyle();
    %coAnimEntry = findCoAnimEntry(coAnimName);
    %this;
    %actionDesc = getField(%coAnimEntry, 6);
    geTwoPlayerEmotesConfirmTitle;
    %text = %actionDesc[$MsgCat::coanim @ "ACCEPT-OR-DECLINE"];
    geTwoPlayerEmotesConfirmOtherPlayerPortrait;
    %text = strreplace(%text, "[OTHERPLAYER]", otherPlayerName);
    %this;
    %text = strreplace(%text, "[ACTIONDESC]", %actionDesc);
    %text.setTextWithStyle();
};
