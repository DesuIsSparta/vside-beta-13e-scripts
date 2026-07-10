$gGiftingEnabled_vPoints = 0;
$gGiftingEnabled_vBux = 0;
if ($StandAlone) {
    $gGiftingEnabled_vPoints = 1;
    $gGiftingEnabled_vBux = 1;
}
function geGiftingPanel::open(%this, %otherPlayerName, %whichScreen) {
    if ((%otherPlayerName $= $Player::Name)) {
        return;
    }
    if ($StandAlone) {
        %request = new ManagerRequest("");;
        0;
        parseGiftingSettings(%request);
        %request.delete();
        error(getScopeName() @ " " @ "- using hardcoded daily caps.");
        setMyRespektPoints(54321, 0);
    }
    %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
    if (!(isObject(%otherPlayer))) {
        error(getScopeName() @ " " @ "- could not find other player:" @ " " @ %otherPlayerName @ " " @ getTrace());
        return;
    }
    if ((%whichScreen $= "initiate")) {
        if (!(%otherPlayer.isInRange(%this))) {
            %text = ;
            %text = strreplace(%text, "[OTHERPLAYER]", %otherPlayerName);
            MessageBoxOK("Get Closer!", %text, "");
            return;
        }
        requestPlayerInfoFor(%otherPlayerName);
    }
    geGiftingPanelBackground.add(PlayGui);
    1.setVisible(geGiftingPanelBackground);
    getWord(PlayGui.getExtent(), 1).resize(geGiftingPanelBackground, getWord(PlayGui.getExtent(), 0));
    0.reposition(geGiftingPanelBackground, 0);
    geGiftingPanelBackground.focusAndRaise(PlayGui);
    %this.ensureAdded(PlayGui);
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    setActionMapsEnabled(0);
    %this.otherPlayerName = %otherPlayerName;
    if ((%whichScreen $= "acceptDecline")) {
        %this.sourcePlayerName = %otherPlayerName;
        %this.targetPlayerName = $Player::Name;
    }
    %this.sourcePlayerName = $Player::Name;
    %this.targetPlayerName = %otherPlayerName;
    0.setValue(geGiftingCurrencyType_vPoints);
    0.setValue(geGiftingCurrencyType_vBux);
    "".setValue(geGiftingEditAmt);
    "".setValue(geGiftingEditMsg);
    %this.currentScreen = %whichScreen;
    %this.refresh();
};
function geGiftingPanel::close(%this, %accepted, %messageCode) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    0.setVisible(geGiftingPanelBackground);
    setActionMapsEnabled(1);
    if ((%this.currentScreen $= "acceptDecline")) {
    }
    if ((%this.currentScreen $= "items_AcceptDecline")) {
        if (!(isDefined("%accepted"))) {
            %accepted = 0;
        }
        if (!(isDefined("%messageCode"))) {
            %messageCode = "DECLINED";
        }
        %messageCode.doAccept(%this, %this.sourcePlayerName, %this.giftTransactionID, %accepted);
    }
    cancel($gGiftingPanelAcceptDeclineTimerID);
    return 1;
};
function geGiftingPanel::openFromPendingTransactionRecord(%this, %pendingTransactionRecord) {
    "initiate".open(geGiftingPanel, %pendingTransactionRecord.targetPlayerName);
    (geGiftingCurrencyType_vPoints @ " " @ %pendingTransactionRecord.currencyType $= "vPoints").setValue();
    (geGiftingCurrencyType_vBux @ " " @ %pendingTransactionRecord.currencyType $= "vBux").setValue();
    %pendingTransactionRecord.currencyAmount.setValue(geGiftingEditAmt);
    %pendingTransactionRecord.personalMessage.setValue(geGiftingEditMsg);
    geGiftingPanel.refresh();
    1.makeFirstResponder(geGiftingEditMsg);
};
function geGiftingPanel::isInRange(%this, %otherPlayer) {
    if (!(isObject(%otherPlayer))) {
        return 0;
    }
    %coAnimEntry = findCoAnimEntry("gift");
    %range = getField(%coAnimEntry, 3);
    %ret = Math::isInRange($player.getPosition(), %otherPlayer.getPosition(), %range);
    return %ret;
};
function geGiftingPanel::refresh(%this) {
    %ghost = Player::findPlayerInstance(%this.otherPlayerName);
    if (!(isObject(%ghost))) {
        error(getScopeName() @ " " @ "- couldn't find target player:" @ " " @ %this.otherPlayerName);
        %this.close();
        return;
    }
    %text = "<just:right><clip:1000>" @ %this.otherPlayerName;
    %text.setTextWithStyle(geGiftingOtherPlayerName);
    %otherPlayerPortraitUrl = $Net::AvatarURL @ urlEncode(%this.otherPlayerName) @ "?size=M";
    "platform/client/ui/tgf/tgf_profile_default_" @ %ghost.getGender().setBitmap(geGiftingOtherPlayerPortrait);
    %otherPlayerPortraitUrl.downloadAndApplyBitmap(geGiftingOtherPlayerPortrait);
    if ((%this.currentScreen $= "initiate")) {
        1.setVisible(geGiftingScreen_Initiate);
        0.setVisible(geGiftingScreen_Confirmation);
        0.setVisible(geGiftingScreen_AcceptDecline);
        %this.refreshScreen_Initiate();
    }
    if ((%this.currentScreen $= "confirmation")) {
        0.setVisible(geGiftingScreen_Initiate);
        1.setVisible(geGiftingScreen_Confirmation);
        0.setVisible(geGiftingScreen_AcceptDecline);
        %this.refreshScreen_Confirmation();
    }
    if ((%this.currentScreen $= "acceptDecline")) {
        0.setVisible(geGiftingScreen_Initiate);
        0.setVisible(geGiftingScreen_Confirmation);
        1.setVisible(geGiftingScreen_AcceptDecline);
        %this.refreshScreen_AcceptDecline();
    }
    if ((%this.currentScreen $= "items_acceptDecline")) {
        0.setVisible(geGiftingScreen_Initiate);
        0.setVisible(geGiftingScreen_Confirmation);
        1.setVisible(geGiftingScreen_AcceptDecline);
        %this.refreshScreen_ItemsAcceptDecline();
    }
    error(getScopeName() @ " " @ "- unknown currentScreen:" @ " " @ %this.currentScreen @ " " @ getTrace());
};
function geGiftingPanel::refreshScreen_Initiate(%this) {
    %text = "The gift of cash for" @ " " @ %this.otherPlayerName;
    %text.setTextWithStyle(geGiftingTitle);
    %this.GiftType = "currency";
    %amountInTheBank = %this.getAmountInBankOfCurrentCurrency();
    %level = $player.getRespektLevel();
    %levelName = respektLevelToNameWithIndefiniteArticle(%level);
    %limitVpGive = getGiftingCapsForLevel(%level, "vPoints", "give");
    %limitVbGet = getGiftingCapsForLevel(%level, "vPoints", "recv");
    %limitVbGive = getGiftingCapsForLevel(%level, "vBux", "give");
    %limitVbGet = getGiftingCapsForLevel(%level, "vBux", "recv");
    %text = %this.textBody;
    geGiftingTextLimits;
    %text = strreplace(%text, "[LEVEL]", %level);
    %text = strreplace(%text, "[LEVELNAME]", %levelName);
    %text = strreplace(%text, "[VPGIVE]", %limitVpGive);
    %text = strreplace(%text, "[VBGIVE]", %limitVbGive);
    if (!($gGiftingEnabled_vPoints)) {
    }
    if (!($gGiftingEnabled_vBux)) {
        %text = %text @ "<br><color:ff3333>Gifting is temporarily disabled.";
    }
    if (!($gGiftingEnabled_vPoints)) {
        %text = %text @ "<br><color:ff3333>Gifting vPoints is temporarily disabled.";
    }
    if (!($gGiftingEnabled_vBux)) {
        %text = %text @ "<br><color:ff3333>Gifting vBux is temporarily disabled.";
    }
    %text.setTextWithStyle(geGiftingTextLimits);
    if ((%limitVpGive > 0.0)) {
    }
    %enableVP = $gGiftingEnabled_vPoints;
    if ((%limitVbGive > 0.0)) {
    }
    %enableVB = $gGiftingEnabled_vBux;
    %enableVP.setActive(geGiftingCurrencyType_vPoints);
    %enableVB.setActive(geGiftingCurrencyType_vBux);
    geGiftingTextType @ %this.textBody.setTextWithStyle(geGiftingTextType, "<spush><font:Arial Bold:20>1.<spop> ");
    if (%enableVP) {
    }
    %this.textBodyI.setTextWithStyle(geGiftingText2, geGiftingText2, %this.textBody, geGiftingText2);
    if (%enableVB) {
    }
    %this.textBodyI.setTextWithStyle(geGiftingText3, geGiftingText3, %this.textBody, geGiftingText3);
    if (geGiftingCurrencyType_vPoints.getValue()) {
    }
    if (geGiftingCurrencyType_vBux.getValue()) {
        %currencyType = geGiftingCurrencyType_vPoints.getValue() ? "vPoints" : "vBux";
        %vpText = "vPoints";
        %vbText = "vBux";
        if (geGiftingCurrencyType_vPoints.getValue()) {
        }
        %currencyText = "<spush><color:13b93c>" @ %vbText @ "<spop>";
        "<spush><color:159fe7>" @ %vpText @ "<spop> ";
        %text = %this.textBody;
        geGiftingTextAmt;
        %text = strreplace(%text, "[GIFTTYPE]", %currencyText);
        %text = "<spush><font:Arial Bold:20>2.<spop> " @ %text;
        %vpText = (geGiftingEditAmt.getValue() == 1.0) ? "vPoint" : "vPoints";
        %vbText = (geGiftingEditAmt.getValue() == 1.0) ? "vBuck" : "vBux";
        if (geGiftingCurrencyType_vPoints.getValue()) {
        }
        %currencyText = "<spush><color:13b93c>" @ %vbText @ "<spop>";
        "<spush><color:159fe7>" @ %vpText @ "<spop> ";
        if ((%amountInTheBank < geGiftingEditAmt.getValue())) {
            %currencyText = %currencyText @ " <spush><b><color:ff0000dd>.. you only have " @ %amountInTheBank @ "!";
        }
        %text.setTextWithStyle(geGiftingTextAmt);
        %currencyText.setTextWithStyle(geGiftingTextCurrency);
        1.setVisible(geGiftingEditAmt);
    }
    "<color:ffffff40><spush><font:Arial Bold:20>2.<spop> amount".setTextWithStyle(geGiftingTextAmt);
    "".setTextWithStyle(geGiftingTextCurrency);
    0.setVisible(geGiftingEditAmt);
    if ((geGiftingEditAmt.getValue() > 0.0)) {
    }
    if ((%amountInTheBank >= geGiftingEditAmt.getValue())) {
        geGiftingTextMsg @ %this.textBody.setTextWithStyle(geGiftingTextMsg, "<spush><font:Arial Bold:20>3.<spop> ");
        1.setVisible(geGiftingEditMsg);
        1.setActive(geGiftingButtonNext);
    }
    "<color:ffffff40><spush><font:Arial Bold:20>3.<spop> message".setTextWithStyle(geGiftingTextMsg);
    0.setVisible(geGiftingEditMsg);
    1.makeFirstResponder(geGiftingEditAmt);
    0.setActive(geGiftingButtonNext);
    1.setVisible(geGiftingButtonBack);
    0.setActive(geGiftingButtonBack);
    "< Back".setText(geGiftingButtonBack);
    "Next >".setText(geGiftingButtonNext);
    1.setVisible(geGiftingButtonCancel);
};
function geGiftingPanel::getGiftDescription(%this) {
    if ((%this.GiftType $= "currency")) {
        %ret = gifting_composeGiftDescriptionCurrency(%this.currencyType, %this.currencyAmount);
    }
    %ret = gifting_composeGiftDescriptionItems(%this.skus);
    return %ret;
};
function gifting_composeGiftDescriptionCurrency(%currencyType, %currencyAmount) {
    %vpText = (%currencyAmount == 1.0) ? "vPoint" : "vPoints";
    %vbText = (%currencyAmount == 1.0) ? "vBuck" : "vBux";
    if ((%currencyAmount @ " " @ " " @ %currencyType $= "VPOINTS")) {
    }
    %text = "<spush><color:159fe7>" @ %vpText @ "<spop>" @ "<spush><color:13b93c>" @ %vbText @ "<spop>";
    return %text;
};
function gifting_composeGiftDescriptionCurrency2(%currencyType, %currencyAmount) {
    %vpText = (%currencyAmount == 1.0) ? "vPoint" : "vPoints";
    %vbText = (%currencyAmount == 1.0) ? "vBuck" : "vBux";
    %aFew = (%currencyType $= "VPOINTS") ? 50 : 5;
    %sardonicism = (%currencyAmount < %aFew) ? "whole " : "";
    if ((%currencyAmount @ " " @ %sardonicism @ " " @ %currencyType $= "VPOINTS")) {
    }
    %text = "<spush><color:002288>" @ %vpText @ "<spop>" @ "<spush><color:005500>" @ %vbText @ "<spop>";
    return %text;
};
function gifting_composeGiftDescriptionItems(%skus) {
    %text = "a" @ " " @ 0.getSkuShortDescriptions(SkuManager, %skus, "and a ");
    return %text;
};
function geGiftingPanel::refreshScreen_Confirmation(%this) {
    1.setVisible(geGiftingButtonBack);
    1.setActive(geGiftingButtonBack);
    "< Back".setText(geGiftingButtonBack);
    %this.personalMessage = geGiftingEditMsg.getValue();
    %this.personalMessage = StripMLControlChars(%this.personalMessage);
    if ((%this.personalMessage $= "")) {
        %this.personalMessage = "(no message)";
    }
    %text = "<tab:80>";
    %text = %text @ %text[$MsgCat::gifting @ "CAVEAT-MUNEROR"];
    %text = %text @ "<br>";
    %text = %text @ "<br>Giving:" @ "\t" @ %this.getGiftDescription();
    %text = %text @ "<br>To:" @ "\t" @ %this.targetPlayerName;
    %text = %text @ "<br>With Message:" @ "\t" @ %this.personalMessage;
    %text = strreplace(%text, "[GIFTDESC]", %this.getGiftDescription());
    %text.setTextWithStyle(geGiftingTextConfirm);
    "Give!".setText(geGiftingButtonNext);
    1.makeFirstResponder(geGiftingButtonNext);
    1.setVisible(geGiftingButtonCancel);
};
$gGiftingPanelAcceptDeclineTimerID = 0;
function geGiftingPanel::refreshScreen_AcceptDecline(%this) {
    %text = "The gift of cash";
    %text.setTextWithStyle(geGiftingTitle);
    "DECLINE".setText(geGiftingButtonBack);
    1.setActive(geGiftingButtonBack);
    "ACCEPT".setText(geGiftingButtonNext);
    1.setActive(geGiftingButtonNext);
    0.setVisible(geGiftingButtonCancel);
    %otherPlayer = Player::findPlayerInstance(%this.otherPlayerName);
    %text = "";
    %text = %text @ %text[$MsgCat::gifting @ "ACCEPT-OR-DECLINE"];
    %text = strreplace(%text, "[OTHERPLAYER]", %this.otherPlayerName);
    %text = strreplace(%text, "[PERSONALMESSAGE]", %this.personalMessage);
    %text = strreplace(%text, "[GIFTAMOUNT]", %this.currencyAmount);
    %text = strreplace(%text, "[GIFTTYPE]", %this.currencyType);
    %text = strreplace(%text, "[GIFTDESC]", %this.getGiftDescription());
    %text = strreplace(%text, "[OTHERPLAYER_HE_SHE_IT]", getPronounHeSheIt(%otherPlayer));
    %text = strreplace(%text, "[OTHERPLAYER_HIM_HER_IT]", getPronounHimHerIt(%otherPlayer));
    %text = strreplace(%text, "[IT_THEM]", getPronounItThem(%this.currencyAmount));
    %text.setTextWithStyle(geGiftingTextAcceptDecline);
    (30.0 * 1000.0).startCountdownTimer(%this);
};
function geGiftingPanel::refreshScreen_ItemsAcceptDecline(%this) {
    %text = "The Gift of Libation";
    %text.setTextWithStyle(geGiftingTitle);
    %this.GiftType = "items";
    "DECLINE".setText(geGiftingButtonBack);
    1.setActive(geGiftingButtonBack);
    "ACCEPT".setText(geGiftingButtonNext);
    1.setActive(geGiftingButtonNext);
    0.setVisible(geGiftingButtonCancel);
    %otherPlayer = Player::findPlayerInstance(%this.otherPlayerName);
    %numItems = getWordCount(%this.skus);
    if (%this.making) {
    }
    %text = %this[$MsgCat::giftingItems @ "ACCEPT-OR-DECLINE-MAKING"][$MsgCat::giftingItems @ "ACCEPT-OR-DECLINE"];
    %this[$MsgCat::giftingItems @ "ACCEPT-OR-DECLINE-MAKING"];
    %text = strreplace(%text, "[OTHERPLAYER]", %this.otherPlayerName);
    %text = strreplace(%text, "[GIFTDESC]", %this.getGiftDescription());
    %text = strreplace(%text, "[OTHERPLAYER_HE_SHE_IT]", getPronounHeSheIt(%otherPlayer));
    %text = strreplace(%text, "[OTHERPLAYER_HIM_HER_IT]", getPronounHimHerIt(%otherPlayer));
    %text = strreplace(%text, "[IT_THEM]", getPronounItThem(%numItems));
    %text.setTextWithStyle(geGiftingTextAcceptDecline);
    (30.0 * 1000.0).startCountdownTimer(%this);
};
function geGiftingPanel::startCountdownTimer(%this, %milliseconds) {
    $gGiftingPanelCountdownMSRemaining = %milliseconds;
    %this.countdownTick();
};
function geGiftingPanel::countdownTick(%this) {
    %tickPeriod = 100;
    $gGiftingPanelCountdownMSRemaining = ($gGiftingPanelCountdownMSRemaining - %tickPeriod);
    %this.rotRadians = (($gGiftingPanelCountdownMSRemaining * 0.001) / 6.0) @ geGiftingAcceptDeclineClock_littleHand;
    %this.rotRadians = ($gGiftingPanelCountdownMSRemaining * 0.001) @ geGiftingAcceptDeclineClock_bigHand;
    %text = mFloor((($gGiftingPanelCountdownMSRemaining * 0.001) + 0.5));
    %text = %text @ "..";
    %text.setTextWithStyle(geGiftingAcceptDeclineClock_readout);
    cancel($gGiftingPanelAcceptDeclineTimerID);
    if (($gGiftingPanelCountdownMSRemaining > 0.0)) {
    }
    if (%this.isVisible()) {
        $gGiftingPanelAcceptDeclineTimerID = "countdownTick".schedule(%this, %tickPeriod);
    }
    "DECLINED-TIMEOUT".close(%this, 0);
    if (%this.making) {
    }
    %text = %this[$MsgCat::giftingItems @ "E-TOOSLOW"][$MsgCat::giftingItems @ "E-TOOSLOW"];
    %this[$MsgCat::giftingItems @ "E-TOOSLOW"];
    %text = strreplace(%text, "[OTHERPLAYER]", %this.otherPlayerName);
    %text = strreplace(%text, "[PERSONALMESSAGE]", %this.personalMessage);
    handleSystemMessage("msgInfoMessage", %text);
};
function geGiftingEditAmt::validate(%this) {
    geGiftingPanel.refresh();
};
function geGiftingEditAmt::onEnter(%this) {
    1.makeFirstResponder(geGiftingEditMsg);
};
function geGiftingEditMsg::validate(%this) {
    geGiftingPanel.refresh();
};
function geGiftingEditMsg::onEnter(%this) {
    if (geGiftingButtonNext.isActive()) {
        eval(geGiftingButtonNext, %this.command);
    }
};
function geGiftingPanel::onNext(%this) {
    if ((%this.currentScreen $= "initiate")) {
        %this.onNext_Initiate();
    }
    if ((%this.currentScreen $= "confirmation")) {
        %this.onNext_Confirmation();
    }
    if ((%this.currentScreen $= "acceptDecline")) {
        %this.onNext_AcceptDecline();
    }
    if ((%this.currentScreen $= "items_acceptDecline")) {
        %this.onNext_Items_AcceptDecline();
    }
    error(getScopeName() @ " " @ "- unknown currentScreen:" @ " " @ %this.currentScreen @ " " @ getTrace());
};
function geGiftingPanel::onBack(%this) {
    if ((%this.currentScreen $= "initiate")) {
        %this.onBack_Initiate();
    }
    if ((%this.currentScreen $= "confirmation")) {
        %this.onBack_Confirmation();
    }
    if ((%this.currentScreen $= "acceptDecline")) {
        %this.onBack_AcceptDecline();
    }
    if ((%this.currentScreen $= "items_AcceptDecline")) {
        %this.onBack_Items_AcceptDecline();
    }
    error(getScopeName() @ " " @ "- unknown currentScreen:" @ " " @ %this.currentScreen @ " " @ getTrace());
};
function geGiftingPanel::onNext_Initiate(%this) {
    %this.currencyType = geGiftingCurrencyType_vPoints.getValue() ? "vPoints" : "vBux";
    %this.currencyAmount = geGiftingEditAmt.getValue();
    %amountInTheBank = %this.getAmountInBankOfCurrentCurrency();
    if ((%this.currencyAmount > %amountInTheBank)) {
        "".setValue(geGiftingEditAmt);
        error(getScopeName() @ " " @ "- trying to give more money than owned!" @ " " @ %this.currencyAmount @ " " @ %amountInTheBank @ " " @ getTrace());
        MessageBoxOK("Something is Wrong", "Hm, something went wrong.\nPlease enter a new amount..", "");
        %this.refresh();
        return;
    }
    %this.currentScreen = "confirmation";
    %this.refresh();
};
function geGiftingPanel::onNext_Confirmation(%this) {
    %dlg = MessageBoxOK("The Gift of Cash", "<br>Sending" @ " " @ %this.getGiftDescription() @ " " @ "to" @ " " @ %this.targetPlayerName @ "..<br>", "");
    if ($StandAlone) {
        %dlg.onDryRunSuccess(geGiftingPanel);
    }
    %request = sendRequest_GiftCurrency(%this.targetPlayerName, %this.currencyType, %this.currencyAmount, 1);
    onDoneOrErrorCallback_GiftCurrency;
    %request.dlg = %dlg;
    %this.close();
};
function geGiftingPanel::onNext_AcceptDecline(%this) {
    "ACCEPTED".close(%this, 1);
};
function geGiftingPanel::onNext_Items_AcceptDecline(%this) {
    "ACCEPTED".close(%this, 1);
};
function geGiftingPanel::onBack_AcceptDecline(%this) {
    %this.close();
};
function geGiftingPanel::onBack_Items_AcceptDecline(%this) {
    %this.close();
};
function geGiftingPanel::onDryRunSuccess(%this, %dlg) {
    %giftTransactionID = MD5(getRandom(0, 1000000));
    commandToServer('GiftingCurrency_Initiated', %this.targetPlayerName, %giftTransactionID, %this.personalMessage, %this.currencyType, %this.currencyAmount);
    %this.personalMessage.registerPendingTransaction(%this, %giftTransactionID, %this.currencyType, %this.currencyAmount, %this.targetPlayerName, %dlg);
};
function geGiftingPanel::registerPendingTransaction(%this, %giftTransactionID, %currencyType, %currencyAmount, %targetPlayerName, %dlg, %personalMessage) {
    if (!(isObject(%this.PendingTransactionsList))) {
        %this.PendingTransactionsList = safeNewScriptObject("StringMap", "", 0);
    }
    if (!(%giftTransactionID.get(%this.PendingTransactionsList) $= "")) {
        error(getScopeName() @ " " @ "- transaction already exists!" @ " " @ %giftTransactionID @ " " @ getTrace());
        return;
    }
    %pendingTransactionRecord = safeNewScriptObject("ScriptObject", "", 0);
    %pendingTransactionRecord.giftTransactionID = %giftTransactionID;
    %pendingTransactionRecord.currencyType = %currencyType;
    %pendingTransactionRecord.currencyAmount = %currencyAmount;
    %pendingTransactionRecord.targetPlayerName = %targetPlayerName;
    %pendingTransactionRecord.dlg = %dlg;
    %pendingTransactionRecord.personalMessage = %personalMessage;
    %pendingTransactionRecord.put(%this.PendingTransactionsList, %giftTransactionID);
};
function geGiftingPanel::getPendingTransaction(%this, %giftTransactionID) {
    if (!(isObject(%this.PendingTransactionsList))) {
        error(getScopeName() @ " " @ "- no PendingTransactionsList!" @ " " @ getTrace());
        return "";
    }
    %pendingTransactionRecord = %giftTransactionID.get(%this.PendingTransactionsList);
    if ((%pendingTransactionRecord $= "")) {
        error(getScopeName() @ " " @ "- no such transaction:" @ " " @ %giftTransactionID @ " " @ getTrace());
        return "";
    }
    return %pendingTransactionRecord;
};
function geGiftingPanel::deletePendingTransaction(%this, %giftTransactionID) {
    if (!(isObject(%this.PendingTransactionsList))) {
        error(getScopeName() @ " " @ "- no PendingTransactionsList!" @ " " @ getTrace());
        return "";
    }
    %pendingTransactionRecord = %giftTransactionID.get(%this.PendingTransactionsList);
    if ((%pendingTransactionRecord $= "")) {
        error(getScopeName() @ " " @ "- no such transaction:" @ " " @ %giftTransactionID @ " " @ getTrace());
        return "";
    }
    %pendingTransactionRecord.delete();
    %giftTransactionID.remove(%this.PendingTransactionsList);
};
function ClientCmdGiftingCurrency_Initiated(%sourcePlayerName, %giftTransactionID, %personalMessage, %currencyType, %currencyAmount) {
    %sourcePlayer = Player::findPlayerInstance(%sourcePlayerName);
    if (!(isObject(%sourcePlayer))) {
        error(getScopeName() @ " " @ "- could not find source player:" @ " " @ %sourcePlayerName);
        "E-ENVSERVER-UNKNOWN".doAccept(geGiftingPanel, %sourcePlayerName, %giftTransactionID, 0);
        return;
    }
    %acceptModeStrangers = $UserPref::Player::GiftsPermissionStrangers[$gGiftAcceptModeStrings @ $UserPref::Player::GiftsPermissionStrangers];
    %acceptModeFriends = $UserPref::Player::GiftsPermissionFriends[$gGiftAcceptModeStrings @ $UserPref::Player::GiftsPermissionFriends];
    if (%sourcePlayer.isFriend()) {
    }
    %acceptMode = %acceptModeStrangers;
    %acceptModeFriends;
    if ((%acceptMode $= "accept")) {
        "ACCEPTED-AUTO".doAccept(geGiftingPanel, %sourcePlayerName, %giftTransactionID, 1);
    }
    if ((%acceptMode $= "decline")) {
        "DECLINED-AUTO".doAccept(geGiftingPanel, %sourcePlayerName, %giftTransactionID, 0);
    }
    if ((%acceptMode $= "ask")) {
        if (geGiftingPanel.isVisible()) {
            "DECLINED-BUSY".doAccept(geGiftingPanel, %sourcePlayerName, %giftTransactionID, 0);
        }
        %this.giftTransactionID = %giftTransactionID @ geGiftingPanel;
        %this.personalMessage = TryFixBadWords(%personalMessage) @ geGiftingPanel;
        %this.currencyType = %currencyType @ geGiftingPanel;
        %this.currencyAmount = %currencyAmount @ geGiftingPanel;
        %this.GiftType = "currency" @ geGiftingPanel;
        "acceptDecline".open(geGiftingPanel, %sourcePlayerName);
    }
};
function geGiftingPanel::doAccept(%this, %sourcePlayerName, %giftTransactionID, %accepted, %messageCode) {
    if ((%this.currentScreen $= "items_acceptDecline")) {
        commandToServer('GiftingItems_AcceptedOrDeclined', %sourcePlayerName, %giftTransactionID, %accepted, %messageCode);
    }
    commandToServer('GiftingCurrency_AcceptOrDecline', %sourcePlayerName, %giftTransactionID, %accepted, %messageCode);
};
function ClientCmdGiftingCurrency_AcceptedOrDeclinedOrInvalid(%giftTransactionID, %accepted, %messageCode) {
    %pendingTransactionRecord = %giftTransactionID.getPendingTransaction(geGiftingPanel);
    if (!(isObject(%pendingTransactionRecord))) {
        error(getScopeName() @ " " @ "- no such pending transaction:" @ " " @ %giftTransactionID);
        return;
    }
    if (isObject(%pendingTransactionRecord.dlg)) {
        %pendingTransactionRecord.dlg.close();
    }
    if (!(%accepted)) {
        %otherPlayerName = %pendingTransactionRecord.targetPlayerName;
        %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
        if (!(isObject(%otherPlayer))) {
            error(getScopeName() @ " " @ "- can't find other player:" @ " " @ %otherPlayerName @ " " @ %giftTransactionID);
            %messageCode = "E-TARGET-MISSING";
        }
        %text = strreplace(%messageCode[$MsgCat::gifting @ %messageCode], "[OTHERPLAYER]", "<linkcolor:ffddeeff><a:gamelink " @ munge(%otherPlayerName) @ ">" @ StripMLControlChars(%otherPlayerName) @ "</a>");
        %text = strreplace(%text, "[OTHERPLAYER_HE_SHE_IT]", getPronounHeSheIt(%otherPlayer));
        %text = strreplace(%text, "[OTHERPLAYER_HIM_HER_IT]", getPronounHimHerIt(%otherPlayer));
        MessageBoxOK("Woops..", %text, "");
        %giftTransactionID.deletePendingTransaction(geGiftingPanel);
        return;
    }
    %request = sendRequest_GiftCurrency(%pendingTransactionRecord.targetPlayerName, %pendingTransactionRecord.currencyType, %pendingTransactionRecord.currencyAmount, 0);
    onDoneOrErrorCallback_GiftCurrency;
    %request.giftTransactionID = %giftTransactionID;
    %request.personalMessage = %pendingTransactionRecord.personalMessage;
};
function onDoneOrErrorCallback_GiftCurrency(%request) {
    %otherPlayerName = "payee".getURLParam(%request);
    %currencyType = "currencyType".getURLParam(%request);
    %currencyAmount = "amount".getURLParam(%request);
    %dryRun = 1.getURLParam(%request, "dryRun");
    %succeeded = ("status".getResult(%request) $= "success");
    if (%succeeded) {
        if (%dryRun) {
            %request.dlg.onDryRunSuccess(geGiftingPanel);
            return;
        }
        getBalancesAndScores();
        commandToServer('GiftingCurrency_Notify', %otherPlayerName, %request.giftTransactionID, %request.personalMessage, %currencyType, %currencyAmount);
        %msg = ;
    }
    if (isObject(%request.dlg)) {
        %request.dlg.close();
    }
    %errorCode = "errorCode".getResult(%request);
    if ((%errorCode $= "")) {
        %errorCode = "UNKNOWN";
    }
    %msg = %errorCode[$MsgCat::gifting @ "E-BACKEND-" @ %errorCode];
    if ((%msg $= "")) {
        %msg = %msg[$MsgCat::gifting @ "E-BACKEND-UNKNOWN"];
    }
    error(getScopeName() @ " " @ "- failed with" @ " " @ %errorCode);
    %info = %otherPlayerName.get(PlayerInfoMap);
    if (!(isObject(%info))) {
        error(getScopeName() @ " " @ "- didn't receive player info for" @ " " @ %otherPlayerName);
        %levelText = "";
        %limitText = "a certain amount of";
    }
    %levelNum = respektScoreToLevel(%info.respekt);
    %levelText = "is level" @ " " @ %levelNum @ ", so ";
    %limitText = getGiftingCapsForLevel(%levelNum, %currencyType, "recv");
    %currencyText = (%currencyType $= "VPOINTS") ? "<spush><color:159fe7>vPoints<spop> " : "<spush><color:13b93c>vBux<spop>";
    %msg = strreplace(%msg, "[OTHERPLAYER]", "<linkcolor:ffddeeff><a:gamelink " @ munge(%otherPlayerName) @ ">" @ StripMLControlChars(%otherPlayerName) @ "</a>");
    %msg = strreplace(%msg, "[GIFTTYPE]", %currencyText);
    %msg = strreplace(%msg, "[OTHERPLAYERLEVELTEXT]", %levelText);
    %msg = strreplace(%msg, "[OTHERPLAYERRECVLIMIT]", %limitText);
    if (!(%succeeded)) {
        MessageBoxOK("woops", %msg, "");
    }
    if (!(%dryRun)) {
        %request.giftTransactionID.deletePendingTransaction(geGiftingPanel);
    }
};
function geGiftingPanel::onBack_Initiate(%this) {
    error(getScopeName() @ " " @ "- shouldn't be here." @ " " @ getTrace());
};
function geGiftingPanel::onBack_Confirmation(%this) {
    %this.currentScreen = "initiate";
    %this.refresh();
    1.makeFirstResponder(geGiftingEditAmt);
};
function geGiftingPanel::getAmountInBankOfCurrentCurrency(%this) {
    if (!(geGiftingCurrencyType_vPoints.getValue())) {
    }
    if (!(geGiftingCurrencyType_vBux.getValue())) {
        return 0;
    }
    if (geGiftingCurrencyType_vPoints.getValue()) {
    }
    %ret = $Player::VBux;
    $Player::VPoints;
    return %ret;
};
function giftOperation(%line) {
    %currencyAmount = getWord(%line, 0);
    %currencyType = getWord(%line, 1);
    if ((formatInt("%d", %currencyAmount) == %currencyAmount)) {
        if ((getSubStr(%currencyType, 0, 2) $= "vB")) {
            %currencyType = "vBux";
        }
        if ((getSubStr(%currencyType, 0, 2) $= "vP")) {
            %currencyType = "vPoints";
        }
        %currencyType = "";
        if (!(%currencyType $= "")) {
            %playerName = getWords(%line, 2, 100);
            "initiate".open(geGiftingPanel, %playerName);
            (geGiftingCurrencyType_vPoints @ " " @ %currencyType $= "vPoints").setValue();
            (geGiftingCurrencyType_vBux @ " " @ %currencyType $= "vBux").setValue();
            %currencyAmount.setValue(geGiftingEditAmt);
            geGiftingPanel.refresh();
            if ((%currencyAmount > 0.0)) {
                1.makeFirstResponder(geGiftingEditMsg);
            }
            1.makeFirstResponder(geGiftingEditAmt);
            return;
        }
    }
    "initiate".open(geGiftingPanel, %line);
};
function parseGiftingSettings(%request) {
    if (1) {
    }
    if ($StandAlone) {
        addFakeGiftingSettings(%request);
    }
    %key = "VBuxTransferEnabled";
    if (%key.hasKey(%request)) {
        $gGiftingEnabled_vBux = %key.getValueBool(%request);
    }
    error(getScopeName() @ " " @ "- setting not found:" @ " " @ %key);
    %key = "VPointsTransferEnabled";
    if (%key.hasKey(%request)) {
        $gGiftingEnabled_vPoints = %key.getValueBool(%request);
    }
    error(getScopeName() @ " " @ "- setting not found:" @ " " @ %key);
    safeEnsureScriptObject("StringMap", "gGiftingCapTable");
    gGiftingCapTable.clear();
    %num = "giftingCapLevelCount".getValue(%request);
    %n = 0;
    while ((%n < %num)) {
        %level = "giftingCap" @ %n @ ".level".getValue(%request);
        %vpGive = "giftingCap" @ %n @ ".vpGive".getValue(%request);
        %vpRecv = "giftingCap" @ %n @ ".vpRecv".getValue(%request);
        %vbGive = "giftingCap" @ %n @ ".vbGive".getValue(%request);
        %vbRecv = "giftingCap" @ %n @ ".vbRecv".getValue(%request);
        %vpGive @ " " @ %vpRecv @ " " @ %vbGive @ " " @ %vbRecv.put(gGiftingCapTable, %level);
        %n = (%n + 1.0);
    }
};
function addFakeGiftingSettings(%request) {
    11.putValue(%request, "giftingCapLevelCount");
    0.putValue(%request, "giftingCap0.level");
    0.putValue(%request, "giftingCap0.vpGive");
    1000.putValue(%request, "giftingCap0.vpRecv");
    0.putValue(%request, "giftingCap0.vbGive");
    100.putValue(%request, "giftingCap0.vbRecv");
    1.putValue(%request, "giftingCap1.level");
    0.putValue(%request, "giftingCap1.vpGive");
    1000.putValue(%request, "giftingCap1.vpRecv");
    0.putValue(%request, "giftingCap1.vbGive");
    150.putValue(%request, "giftingCap1.vbRecv");
    2.putValue(%request, "giftingCap2.level");
    0.putValue(%request, "giftingCap2.vpGive");
    1000.putValue(%request, "giftingCap2.vpRecv");
    0.putValue(%request, "giftingCap2.vbGive");
    200.putValue(%request, "giftingCap2.vbRecv");
    3.putValue(%request, "giftingCap3.level");
    5000.putValue(%request, "giftingCap3.vpGive");
    2500.putValue(%request, "giftingCap3.vpRecv");
    0.putValue(%request, "giftingCap3.vbGive");
    200.putValue(%request, "giftingCap3.vbRecv");
    4.putValue(%request, "giftingCap4.level");
    7500.putValue(%request, "giftingCap4.vpGive");
    5000.putValue(%request, "giftingCap4.vpRecv");
    0.putValue(%request, "giftingCap4.vbGive");
    200.putValue(%request, "giftingCap4.vbRecv");
    5.putValue(%request, "giftingCap5.level");
    10000.putValue(%request, "giftingCap5.vpGive");
    7500.putValue(%request, "giftingCap5.vpRecv");
    250.putValue(%request, "giftingCap5.vbGive");
    250.putValue(%request, "giftingCap5.vbRecv");
    6.putValue(%request, "giftingCap6.level");
    15000.putValue(%request, "giftingCap6.vpGive");
    10000.putValue(%request, "giftingCap6.vpRecv");
    500.putValue(%request, "giftingCap6.vbGive");
    500.putValue(%request, "giftingCap6.vbRecv");
    7.putValue(%request, "giftingCap7.level");
    20000.putValue(%request, "giftingCap7.vpGive");
    12500.putValue(%request, "giftingCap7.vpRecv");
    1000.putValue(%request, "giftingCap7.vbGive");
    700.putValue(%request, "giftingCap7.vbRecv");
    8.putValue(%request, "giftingCap8.level");
    25000.putValue(%request, "giftingCap8.vpGive");
    15000.putValue(%request, "giftingCap8.vpRecv");
    1250.putValue(%request, "giftingCap8.vbGive");
    800.putValue(%request, "giftingCap8.vbRecv");
    9.putValue(%request, "giftingCap9.level");
    35000.putValue(%request, "giftingCap9.vpGive");
    20000.putValue(%request, "giftingCap9.vpRecv");
    1500.putValue(%request, "giftingCap9.vbGive");
    900.putValue(%request, "giftingCap9.vbRecv");
    10.putValue(%request, "giftingCap10.level");
    50000.putValue(%request, "giftingCap10.vpGive");
    50000.putValue(%request, "giftingCap10.vpRecv");
    2000.putValue(%request, "giftingCap10.vbGive");
    1000.putValue(%request, "giftingCap10.vbRecv");
};
function getGiftingCapsForLevel(%level, %currency, %giveOrRecv) {
    %s = %level.get(gGiftingCapTable);
    if ((%s $= "")) {
        error(getScopeName() @ " " @ "- unknown level:" @ " " @ %level @ " " @ getTrace());
        return 0;
    }
    if ((%currency $= "vPoints")) {
        %idx = 0;
    }
    if ((%currency $= "vBux")) {
        %idx = 2;
    }
    error(getScopeName() @ " " @ "- unknown currency:" @ " " @ %currency @ " " @ getTrace());
    return 0;
    if ((%giveOrRecv $= "recv")) {
        %idx = (%idx + 1.0);
    }
    %ret = getWord(%s, %idx);
    return %ret;
};
function clientCmdGiftingCurrency_NotifySource(%otherPlayerName, %personalMessage, %currencyType, %currencyAmount) {
    %giftText = gifting_composeGiftDescriptionCurrency2(%currencyType, %currencyAmount);
    %encodedText = %giftText @ "\t" @ %personalMessage;
    pChat::ProcessIncomingLine(%encodedText, 0, $player.getShapeName(), %otherPlayerName, 0, "gift", 0);
    schedule(3000, 0, "floatBalanceChange", %currencyType, %currencyAmount, Player::findPlayerInstance(%otherPlayerName));
};
function clientCmdGiftingCurrency_NotifyTarget(%otherPlayerName, %personalMessage, %currencyType, %currencyAmount) {
    %giftText = gifting_composeGiftDescriptionCurrency2(%currencyType, %currencyAmount);
    %encodedText = %giftText @ "\t" @ %personalMessage;
    pChat::ProcessIncomingLine(%encodedText, 0, %otherPlayerName, $player.getShapeName(), 0, "gift", 0);
};
