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
        %request = new ""();;
        ManagerRequest;
        parseGiftingSettings(%request);
        %request.delete();
        error(getScopeName() @ " " @ "- using hardcoded daily caps.");
        setMyRespektPoints(54321, 0);
    }
    %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
    0;
    if (!(isObject(%otherPlayer))) {
        error(getScopeName() @ " " @ "- could not find other player:" @ " " @ %otherPlayerName @ " " @ getTrace());
        return;
    }
    if ((%whichScreen $= "initiate")) {
        if (!(%this.isInRange(%otherPlayer))) {
            %text = ;
            %text = strreplace(%text, "[OTHERPLAYER]", %otherPlayerName);
            MessageBoxOK("Get Closer!", %text, "");
            return;
        }
        requestPlayerInfoFor(%otherPlayerName);
    }
    PlayGui.add(geGiftingPanelBackground);
    1.setVisible();
    getWord(PlayGui.getExtent(), 0).resize(getWord(PlayGui.getExtent(), 1));
    0.reposition(0);
    PlayGui.focusAndRaise(geGiftingPanelBackground);
    %this.ensureAdded();
    %this.setVisible(1);
    %this.focusAndRaise();
    setActionMapsEnabled(0);
    %this.otherPlayerName = PlayGui @ %otherPlayerName;
    PlayGui;
    if ((geGiftingPanelBackground @ " " @ %whichScreen $= "acceptDecline")) {
        %this.sourcePlayerName = geGiftingPanelBackground @ %otherPlayerName;
        geGiftingPanelBackground;
        %this.targetPlayerName = $Player::Name;
    }
    %this.sourcePlayerName = $Player::Name;
    %this.targetPlayerName = %otherPlayerName;
    0.setValue();
    0.setValue();
    "".setValue();
    "".setValue();
    %this.currentScreen = geGiftingEditMsg @ %whichScreen;
    geGiftingEditAmt;
    %this.refresh();
};
function geGiftingPanel::close(%this, %accepted, %messageCode) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    0.setVisible();
    setActionMapsEnabled(1);
    if ((geGiftingPanelBackground @ " " @ %this.currentScreen $= "acceptDecline")) {
    }
    if ((%this.currentScreen $= "items_AcceptDecline")) {
        if (!(isDefined("%accepted"))) {
            %accepted = 0;
        }
        if (!(isDefined("%messageCode"))) {
            %messageCode = "DECLINED";
        }
        %this.doAccept(%this.sourcePlayerName, %this.giftTransactionID, %accepted, %messageCode);
    }
    cancel($gGiftingPanelAcceptDeclineTimerID);
    return 1;
};
function geGiftingPanel::openFromPendingTransactionRecord(%this, %pendingTransactionRecord) {
    %pendingTransactionRecord.targetPlayerName.open("initiate");
    (geGiftingCurrencyType_vPoints @ " " @ %pendingTransactionRecord.currencyType $= "vPoints").setValue();
    (geGiftingCurrencyType_vBux @ " " @ %pendingTransactionRecord.currencyType $= "vBux").setValue();
    %pendingTransactionRecord.currencyAmount.setValue();
    %pendingTransactionRecord.personalMessage.setValue();
    geGiftingPanel.refresh();
    1.makeFirstResponder();
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
    %text.setTextWithStyle();
    %otherPlayerPortraitUrl = $Net::AvatarURL @ urlEncode(%this.otherPlayerName) @ "?size=M";
    geGiftingOtherPlayerName;
    "platform/client/ui/tgf/tgf_profile_default_" @ %ghost.getGender().setBitmap();
    %otherPlayerPortraitUrl.downloadAndApplyBitmap();
    if ((geGiftingOtherPlayerPortrait @ " " @ %this.currentScreen $= "initiate")) {
        1.setVisible();
        0.setVisible();
        0.setVisible();
        %this.refreshScreen_Initiate();
    }
    if ((geGiftingScreen_AcceptDecline @ " " @ %this.currentScreen $= "confirmation")) {
        0.setVisible();
        1.setVisible();
        0.setVisible();
        %this.refreshScreen_Confirmation();
    }
    if ((geGiftingScreen_AcceptDecline @ " " @ %this.currentScreen $= "acceptDecline")) {
        0.setVisible();
        0.setVisible();
        1.setVisible();
        %this.refreshScreen_AcceptDecline();
    }
    if ((geGiftingScreen_AcceptDecline @ " " @ %this.currentScreen $= "items_acceptDecline")) {
        0.setVisible();
        0.setVisible();
        1.setVisible();
        %this.refreshScreen_ItemsAcceptDecline();
    }
    error(getScopeName() @ " " @ "- unknown currentScreen:" @ " " @ %this.currentScreen @ " " @ getTrace());
};
function geGiftingPanel::refreshScreen_Initiate(%this) {
    %text = "The gift of cash for" @ " " @ %this.otherPlayerName;
    %text.setTextWithStyle();
    %this.GiftType = geGiftingTitle @ "currency";
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
    %text.setTextWithStyle();
    if ((0.0 > %limitVpGive)) {
    }
    %enableVP = $gGiftingEnabled_vPoints;
    geGiftingTextLimits;
    if ((0.0 > %limitVbGive)) {
    }
    %enableVB = $gGiftingEnabled_vBux;
    %enableVP.setActive();
    %enableVB.setActive();
    geGiftingTextType @ %this.textBody.setTextWithStyle();
    if (%enableVP) {
    }
    %this.textBodyI.setTextWithStyle();
    if (%enableVB) {
    }
    %this.textBodyI.setTextWithStyle();
    if (geGiftingCurrencyType_vPoints.getValue()) {
    }
    if (geGiftingCurrencyType_vBux.getValue()) {
        %currencyType = geGiftingCurrencyType_vPoints.getValue() ? "vPoints" : "vBux";
        geGiftingText3;
        %vpText = "vPoints";
        %this.textBody;
        %vbText = "vBux";
        geGiftingText3;
        if (geGiftingCurrencyType_vPoints.getValue()) {
        }
        %currencyText = "<spush><color:13b93c>" @ %vbText @ "<spop>";
        "<spush><color:159fe7>" @ %vpText @ "<spop> ";
        %text = %this.textBody;
        geGiftingTextAmt;
        %text = strreplace(%text, "[GIFTTYPE]", %currencyText);
        geGiftingText3;
        %text = "<spush><font:Arial Bold:20>2.<spop> " @ %text;
        geGiftingText2;
        %vpText = (1.0 == geGiftingEditAmt.getValue()) ? "vPoint" : "vPoints";
        %this.textBody;
        %vbText = (1.0 == geGiftingEditAmt.getValue()) ? "vBuck" : "vBux";
        geGiftingText2;
        if (geGiftingCurrencyType_vPoints.getValue()) {
        }
        %currencyText = "<spush><color:13b93c>" @ %vbText @ "<spop>";
        "<spush><color:159fe7>" @ %vpText @ "<spop> ";
        if ((geGiftingEditAmt.getValue() < %amountInTheBank)) {
            %currencyText = %currencyText @ " <spush><b><color:ff0000dd>.. you only have " @ %amountInTheBank @ "!";
            geGiftingText2;
        }
        %text.setTextWithStyle();
        %currencyText.setTextWithStyle();
        1.setVisible();
    }
    "<color:ffffff40><spush><font:Arial Bold:20>2.<spop> amount".setTextWithStyle();
    "".setTextWithStyle();
    0.setVisible();
    if ((0.0 > geGiftingEditAmt.getValue())) {
    }
    if ((geGiftingEditAmt.getValue() >= %amountInTheBank)) {
        geGiftingTextMsg @ %this.textBody.setTextWithStyle();
        1.setVisible();
        1.setActive();
    }
    "<color:ffffff40><spush><font:Arial Bold:20>3.<spop> message".setTextWithStyle();
    0.setVisible();
    1.makeFirstResponder();
    0.setActive();
    1.setVisible();
    0.setActive();
    "< Back".setText();
    "Next >".setText();
    1.setVisible();
};
function geGiftingPanel::getGiftDescription(%this) {
    if ((%this.GiftType $= "currency")) {
        %ret = gifting_composeGiftDescriptionCurrency(%this.currencyType, %this.currencyAmount);
    }
    %ret = gifting_composeGiftDescriptionItems(%this.skus);
    return %ret;
};
function gifting_composeGiftDescriptionCurrency(%currencyType, %currencyAmount) {
    %vpText = (1.0 == %currencyAmount) ? "vPoint" : "vPoints";
    %vbText = (1.0 == %currencyAmount) ? "vBuck" : "vBux";
    if ((%currencyAmount @ " " @ " " @ %currencyType $= "VPOINTS")) {
    }
    %text = "<spush><color:159fe7>" @ %vpText @ "<spop>" @ "<spush><color:13b93c>" @ %vbText @ "<spop>";
    return %text;
};
function gifting_composeGiftDescriptionCurrency2(%currencyType, %currencyAmount) {
    %vpText = (1.0 == %currencyAmount) ? "vPoint" : "vPoints";
    %vbText = (1.0 == %currencyAmount) ? "vBuck" : "vBux";
    %aFew = (%currencyType $= "VPOINTS") ? 50 : 5;
    %sardonicism = (%aFew < %currencyAmount) ? "whole " : "";
    if ((%currencyAmount @ " " @ %sardonicism @ " " @ %currencyType $= "VPOINTS")) {
    }
    %text = "<spush><color:002288>" @ %vpText @ "<spop>" @ "<spush><color:005500>" @ %vbText @ "<spop>";
    return %text;
};
function gifting_composeGiftDescriptionItems(%skus) {
    %text = SkuManager @ %skus.getSkuShortDescriptions("and a ", 0);
    "a" @ " ";
    return %text;
};
function geGiftingPanel::refreshScreen_Confirmation(%this) {
    1.setVisible();
    1.setActive();
    "< Back".setText();
    %this.personalMessage = geGiftingButtonBack @ geGiftingEditMsg.getValue();
    geGiftingButtonBack;
    %this.personalMessage = geGiftingButtonBack @ StripMLControlChars(%this.personalMessage);
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
    %text.setTextWithStyle();
    "Give!".setText();
    1.makeFirstResponder();
    1.setVisible();
};
$gGiftingPanelAcceptDeclineTimerID = 0;
function geGiftingPanel::refreshScreen_AcceptDecline(%this) {
    %text = "The gift of cash";
    %text.setTextWithStyle();
    "DECLINE".setText();
    1.setActive();
    "ACCEPT".setText();
    1.setActive();
    0.setVisible();
    %otherPlayer = Player::findPlayerInstance(%this.otherPlayerName);
    geGiftingButtonCancel;
    %text = "";
    geGiftingButtonNext;
    %text = %text @ %text[$MsgCat::gifting @ "ACCEPT-OR-DECLINE"];
    geGiftingButtonNext;
    %text = strreplace(%text, "[OTHERPLAYER]", %this.otherPlayerName);
    geGiftingButtonBack;
    %text = strreplace(%text, "[PERSONALMESSAGE]", %this.personalMessage);
    geGiftingButtonBack;
    %text = strreplace(%text, "[GIFTAMOUNT]", %this.currencyAmount);
    geGiftingTitle;
    %text = strreplace(%text, "[GIFTTYPE]", %this.currencyType);
    %text = strreplace(%text, "[GIFTDESC]", %this.getGiftDescription());
    %text = strreplace(%text, "[OTHERPLAYER_HE_SHE_IT]", getPronounHeSheIt(%otherPlayer));
    %text = strreplace(%text, "[OTHERPLAYER_HIM_HER_IT]", getPronounHimHerIt(%otherPlayer));
    %text = strreplace(%text, "[IT_THEM]", getPronounItThem(%this.currencyAmount));
    %text.setTextWithStyle();
    %this.startCountdownTimer((1000.0 * 30.0));
};
function geGiftingPanel::refreshScreen_ItemsAcceptDecline(%this) {
    %text = "The Gift of Libation";
    %text.setTextWithStyle();
    %this.GiftType = geGiftingTitle @ "items";
    "DECLINE".setText();
    1.setActive();
    "ACCEPT".setText();
    1.setActive();
    0.setVisible();
    %otherPlayer = Player::findPlayerInstance(%this.otherPlayerName);
    geGiftingButtonCancel;
    %numItems = getWordCount(%this.skus);
    geGiftingButtonNext;
    if (%this.making) {
    }
    %text = %this[$MsgCat::giftingItems @ "ACCEPT-OR-DECLINE-MAKING"][$MsgCat::giftingItems @ "ACCEPT-OR-DECLINE"];
    %this[$MsgCat::giftingItems @ "ACCEPT-OR-DECLINE-MAKING"];
    %text = strreplace(%text, "[OTHERPLAYER]", %this.otherPlayerName);
    geGiftingButtonNext;
    %text = strreplace(%text, "[GIFTDESC]", %this.getGiftDescription());
    geGiftingButtonBack;
    %text = strreplace(%text, "[OTHERPLAYER_HE_SHE_IT]", getPronounHeSheIt(%otherPlayer));
    geGiftingButtonBack;
    %text = strreplace(%text, "[OTHERPLAYER_HIM_HER_IT]", getPronounHimHerIt(%otherPlayer));
    %text = strreplace(%text, "[IT_THEM]", getPronounItThem(%numItems));
    %text.setTextWithStyle();
    %this.startCountdownTimer((1000.0 * 30.0));
};
function geGiftingPanel::startCountdownTimer(%this, %milliseconds) {
    $gGiftingPanelCountdownMSRemaining = %milliseconds;
    %this.countdownTick();
};
function geGiftingPanel::countdownTick(%this) {
    %tickPeriod = 100;
    $gGiftingPanelCountdownMSRemaining = (%tickPeriod - $gGiftingPanelCountdownMSRemaining);
    %this.rotRadians = (6.0 / (0.001 * $gGiftingPanelCountdownMSRemaining)) @ geGiftingAcceptDeclineClock_littleHand;
    %this.rotRadians = (0.001 * $gGiftingPanelCountdownMSRemaining) @ geGiftingAcceptDeclineClock_bigHand;
    %text = mFloor((0.5 + (0.001 * $gGiftingPanelCountdownMSRemaining)));
    %text = %text @ "..";
    %text.setTextWithStyle();
    cancel($gGiftingPanelAcceptDeclineTimerID);
    if ((0.0 > $gGiftingPanelCountdownMSRemaining)) {
    }
    if (%this.isVisible()) {
        $gGiftingPanelAcceptDeclineTimerID = %this.schedule(%tickPeriod, "countdownTick");
        geGiftingAcceptDeclineClock_readout;
    }
    %this.close(0, "DECLINED-TIMEOUT");
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
    1.makeFirstResponder();
};
function geGiftingEditMsg::validate(%this) {
    geGiftingPanel.refresh();
};
function geGiftingEditMsg::onEnter(%this) {
    if (geGiftingButtonNext.isActive()) {
        eval(%this.command);
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
    if ((%amountInTheBank > %this.currencyAmount)) {
        "".setValue();
        error(getScopeName() @ " " @ "- trying to give more money than owned!" @ " " @ %this.currencyAmount @ " " @ %amountInTheBank @ " " @ getTrace());
        MessageBoxOK("Something is Wrong", "Hm, something went wrong.\nPlease enter a new amount..", "");
        %this.refresh();
        return geGiftingEditAmt;
    }
    %this.currentScreen = "confirmation";
    %this.refresh();
};
function geGiftingPanel::onNext_Confirmation(%this) {
    %dlg = MessageBoxOK("The Gift of Cash", "<br>Sending" @ " " @ %this.getGiftDescription() @ " " @ "to" @ " " @ %this.targetPlayerName @ "..<br>", "");
    if ($StandAlone) {
        %dlg.onDryRunSuccess();
    }
    %request = sendRequest_GiftCurrency(%this.targetPlayerName, %this.currencyType, %this.currencyAmount, 1);
    onDoneOrErrorCallback_GiftCurrency;
    %request.dlg = geGiftingPanel @ %dlg;
    %this.close();
};
function geGiftingPanel::onNext_AcceptDecline(%this) {
    %this.close(1, "ACCEPTED");
};
function geGiftingPanel::onNext_Items_AcceptDecline(%this) {
    %this.close(1, "ACCEPTED");
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
    %this.registerPendingTransaction(%giftTransactionID, %this.currencyType, %this.currencyAmount, %this.targetPlayerName, %dlg, %this.personalMessage);
};
function geGiftingPanel::registerPendingTransaction(%this, %giftTransactionID, %currencyType, %currencyAmount, %targetPlayerName, %dlg, %personalMessage) {
    if (!(isObject(%this.PendingTransactionsList))) {
        %this.PendingTransactionsList = safeNewScriptObject("StringMap", "", 0);
    }
    if (!(%this.PendingTransactionsList.get(%giftTransactionID) $= "")) {
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
    %this.PendingTransactionsList.put(%giftTransactionID, %pendingTransactionRecord);
};
function geGiftingPanel::getPendingTransaction(%this, %giftTransactionID) {
    if (!(isObject(%this.PendingTransactionsList))) {
        error(getScopeName() @ " " @ "- no PendingTransactionsList!" @ " " @ getTrace());
        return "";
    }
    %pendingTransactionRecord = %this.PendingTransactionsList.get(%giftTransactionID);
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
    %pendingTransactionRecord = %this.PendingTransactionsList.get(%giftTransactionID);
    if ((%pendingTransactionRecord $= "")) {
        error(getScopeName() @ " " @ "- no such transaction:" @ " " @ %giftTransactionID @ " " @ getTrace());
        return "";
    }
    %pendingTransactionRecord.delete();
    %this.PendingTransactionsList.remove(%giftTransactionID);
};
function ClientCmdGiftingCurrency_Initiated(%sourcePlayerName, %giftTransactionID, %personalMessage, %currencyType, %currencyAmount) {
    %sourcePlayer = Player::findPlayerInstance(%sourcePlayerName);
    if (!(isObject(%sourcePlayer))) {
        error(getScopeName() @ " " @ "- could not find source player:" @ " " @ %sourcePlayerName);
        %sourcePlayerName.doAccept(%giftTransactionID, 0, "E-ENVSERVER-UNKNOWN");
        return geGiftingPanel;
    }
    %acceptModeStrangers = $UserPref::Player::GiftsPermissionStrangers[$gGiftAcceptModeStrings @ $UserPref::Player::GiftsPermissionStrangers];
    %acceptModeFriends = $UserPref::Player::GiftsPermissionFriends[$gGiftAcceptModeStrings @ $UserPref::Player::GiftsPermissionFriends];
    if (%sourcePlayer.isFriend()) {
    }
    %acceptMode = %acceptModeStrangers;
    %acceptModeFriends;
    if ((%acceptMode $= "accept")) {
        %sourcePlayerName.doAccept(%giftTransactionID, 1, "ACCEPTED-AUTO");
    }
    if ((geGiftingPanel @ " " @ %acceptMode $= "decline")) {
        %sourcePlayerName.doAccept(%giftTransactionID, 0, "DECLINED-AUTO");
    }
    if ((geGiftingPanel @ " " @ %acceptMode $= "ask")) {
        if (geGiftingPanel.isVisible()) {
            %sourcePlayerName.doAccept(%giftTransactionID, 0, "DECLINED-BUSY");
        }
        %this.giftTransactionID = %giftTransactionID @ geGiftingPanel;
        geGiftingPanel;
        %this.personalMessage = TryFixBadWords(%personalMessage) @ geGiftingPanel;
        %this.currencyType = %currencyType @ geGiftingPanel;
        %this.currencyAmount = %currencyAmount @ geGiftingPanel;
        %this.GiftType = "currency" @ geGiftingPanel;
        %sourcePlayerName.open("acceptDecline");
    }
};
function geGiftingPanel::doAccept(%this, %sourcePlayerName, %giftTransactionID, %accepted, %messageCode) {
    if ((%this.currentScreen $= "items_acceptDecline")) {
        commandToServer('GiftingItems_AcceptedOrDeclined', %sourcePlayerName, %giftTransactionID, %accepted, %messageCode);
    }
    commandToServer('GiftingCurrency_AcceptOrDecline', %sourcePlayerName, %giftTransactionID, %accepted, %messageCode);
};
function ClientCmdGiftingCurrency_AcceptedOrDeclinedOrInvalid(%giftTransactionID, %accepted, %messageCode) {
    %pendingTransactionRecord = %giftTransactionID.getPendingTransaction();
    geGiftingPanel;
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
        %giftTransactionID.deletePendingTransaction();
        return geGiftingPanel;
    }
    %request = sendRequest_GiftCurrency(%pendingTransactionRecord.targetPlayerName, %pendingTransactionRecord.currencyType, %pendingTransactionRecord.currencyAmount, 0);
    onDoneOrErrorCallback_GiftCurrency;
    %request.giftTransactionID = %giftTransactionID;
    %request.personalMessage = %pendingTransactionRecord.personalMessage;
};
function onDoneOrErrorCallback_GiftCurrency(%request) {
    %otherPlayerName = %request.getURLParam("payee");
    %currencyType = %request.getURLParam("currencyType");
    %currencyAmount = %request.getURLParam("amount");
    %dryRun = %request.getURLParam("dryRun", 1);
    %succeeded = (%request.getResult("status") $= "success");
    if (%succeeded) {
        if (%dryRun) {
            %request.dlg.onDryRunSuccess();
            return geGiftingPanel;
        }
        getBalancesAndScores();
        commandToServer('GiftingCurrency_Notify', %otherPlayerName, %request.giftTransactionID, %request.personalMessage, %currencyType, %currencyAmount);
        %msg = ;
    }
    if (isObject(%request.dlg)) {
        %request.dlg.close();
    }
    %errorCode = %request.getResult("errorCode");
    if ((%errorCode $= "")) {
        %errorCode = "UNKNOWN";
    }
    %msg = %errorCode[$MsgCat::gifting @ "E-BACKEND-" @ %errorCode];
    if ((%msg $= "")) {
        %msg = %msg[$MsgCat::gifting @ "E-BACKEND-UNKNOWN"];
    }
    error(getScopeName() @ " " @ "- failed with" @ " " @ %errorCode);
    %info = %otherPlayerName.get();
    PlayerInfoMap;
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
        %request.giftTransactionID.deletePendingTransaction();
    }
};
function geGiftingPanel::onBack_Initiate(%this) {
    error(getScopeName() @ " " @ "- shouldn't be here." @ " " @ getTrace());
};
function geGiftingPanel::onBack_Confirmation(%this) {
    %this.currentScreen = "initiate";
    %this.refresh();
    1.makeFirstResponder();
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
    if ((%currencyAmount == formatInt("%d", %currencyAmount))) {
        if ((getSubStr(%currencyType, 0, 2) $= "vB")) {
            %currencyType = "vBux";
        }
        if ((getSubStr(%currencyType, 0, 2) $= "vP")) {
            %currencyType = "vPoints";
        }
        %currencyType = "";
        if (!(%currencyType $= "")) {
            %playerName = getWords(%line, 2, 100);
            %playerName.open("initiate");
            (geGiftingCurrencyType_vPoints @ " " @ %currencyType $= "vPoints").setValue();
            (geGiftingCurrencyType_vBux @ " " @ %currencyType $= "vBux").setValue();
            %currencyAmount.setValue();
            geGiftingPanel.refresh();
            if ((0.0 > %currencyAmount)) {
                1.makeFirstResponder();
            }
            1.makeFirstResponder();
            return geGiftingEditAmt;
        }
    }
    %line.open("initiate");
};
function parseGiftingSettings(%request) {
    if (1) {
    }
    if ($StandAlone) {
        addFakeGiftingSettings(%request);
    }
    %key = "VBuxTransferEnabled";
    if (%request.hasKey(%key)) {
        $gGiftingEnabled_vBux = %request.getValueBool(%key);
    }
    error(getScopeName() @ " " @ "- setting not found:" @ " " @ %key);
    %key = "VPointsTransferEnabled";
    if (%request.hasKey(%key)) {
        $gGiftingEnabled_vPoints = %request.getValueBool(%key);
    }
    error(getScopeName() @ " " @ "- setting not found:" @ " " @ %key);
    safeEnsureScriptObject("StringMap", "gGiftingCapTable");
    gGiftingCapTable.clear();
    %num = %request.getValue("giftingCapLevelCount");
    %n = 0;
    if ((%num < %n)) {
        %level = %request.getValue("giftingCap" @ %n @ ".level");
        %vpGive = %request.getValue("giftingCap" @ %n @ ".vpGive");
        %vpRecv = %request.getValue("giftingCap" @ %n @ ".vpRecv");
        %vbGive = %request.getValue("giftingCap" @ %n @ ".vbGive");
        %vbRecv = %request.getValue("giftingCap" @ %n @ ".vbRecv");
        %level.put(%vpGive @ " " @ %vpRecv @ " " @ %vbGive @ " " @ %vbRecv);
        %n = (1.0 + %n);
        gGiftingCapTable;
    }
};
function addFakeGiftingSettings(%request) {
    %request.putValue("giftingCapLevelCount", 11);
    %request.putValue("giftingCap0.level", 0);
    %request.putValue("giftingCap0.vpGive", 0);
    %request.putValue("giftingCap0.vpRecv", 1000);
    %request.putValue("giftingCap0.vbGive", 0);
    %request.putValue("giftingCap0.vbRecv", 100);
    %request.putValue("giftingCap1.level", 1);
    %request.putValue("giftingCap1.vpGive", 0);
    %request.putValue("giftingCap1.vpRecv", 1000);
    %request.putValue("giftingCap1.vbGive", 0);
    %request.putValue("giftingCap1.vbRecv", 150);
    %request.putValue("giftingCap2.level", 2);
    %request.putValue("giftingCap2.vpGive", 0);
    %request.putValue("giftingCap2.vpRecv", 1000);
    %request.putValue("giftingCap2.vbGive", 0);
    %request.putValue("giftingCap2.vbRecv", 200);
    %request.putValue("giftingCap3.level", 3);
    %request.putValue("giftingCap3.vpGive", 5000);
    %request.putValue("giftingCap3.vpRecv", 2500);
    %request.putValue("giftingCap3.vbGive", 0);
    %request.putValue("giftingCap3.vbRecv", 200);
    %request.putValue("giftingCap4.level", 4);
    %request.putValue("giftingCap4.vpGive", 7500);
    %request.putValue("giftingCap4.vpRecv", 5000);
    %request.putValue("giftingCap4.vbGive", 0);
    %request.putValue("giftingCap4.vbRecv", 200);
    %request.putValue("giftingCap5.level", 5);
    %request.putValue("giftingCap5.vpGive", 10000);
    %request.putValue("giftingCap5.vpRecv", 7500);
    %request.putValue("giftingCap5.vbGive", 250);
    %request.putValue("giftingCap5.vbRecv", 250);
    %request.putValue("giftingCap6.level", 6);
    %request.putValue("giftingCap6.vpGive", 15000);
    %request.putValue("giftingCap6.vpRecv", 10000);
    %request.putValue("giftingCap6.vbGive", 500);
    %request.putValue("giftingCap6.vbRecv", 500);
    %request.putValue("giftingCap7.level", 7);
    %request.putValue("giftingCap7.vpGive", 20000);
    %request.putValue("giftingCap7.vpRecv", 12500);
    %request.putValue("giftingCap7.vbGive", 1000);
    %request.putValue("giftingCap7.vbRecv", 700);
    %request.putValue("giftingCap8.level", 8);
    %request.putValue("giftingCap8.vpGive", 25000);
    %request.putValue("giftingCap8.vpRecv", 15000);
    %request.putValue("giftingCap8.vbGive", 1250);
    %request.putValue("giftingCap8.vbRecv", 800);
    %request.putValue("giftingCap9.level", 9);
    %request.putValue("giftingCap9.vpGive", 35000);
    %request.putValue("giftingCap9.vpRecv", 20000);
    %request.putValue("giftingCap9.vbGive", 1500);
    %request.putValue("giftingCap9.vbRecv", 900);
    %request.putValue("giftingCap10.level", 10);
    %request.putValue("giftingCap10.vpGive", 50000);
    %request.putValue("giftingCap10.vpRecv", 50000);
    %request.putValue("giftingCap10.vbGive", 2000);
    %request.putValue("giftingCap10.vbRecv", 1000);
};
function getGiftingCapsForLevel(%level, %currency, %giveOrRecv) {
    %s = %level.get();
    gGiftingCapTable;
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
        %idx = (1.0 + %idx);
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
