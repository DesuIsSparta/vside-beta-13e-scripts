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
        %request = new ""();
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
    if ((geGiftingPanelBackground SPC %whichScreen $= "acceptDecline")) {
        sourcePlayerName = PlayGui @ %otherPlayerName @ %this;
        geGiftingPanelBackground;
        targetPlayerName = PlayGui @ $Player::Name @ %this;
        PlayGui;
    }
    sourcePlayerName = geGiftingPanelBackground @ $Player::Name @ %this;
    geGiftingPanelBackground;
    targetPlayerName = geGiftingPanelBackground @ %otherPlayerName @ %this;
    PlayGui;
    0.setValue();
    0.setValue();
    "".setValue();
    "".setValue();
    currentScreen = geGiftingEditMsg @ %whichScreen @ %this;
    geGiftingEditAmt;
    %this.refresh();
};
function geGiftingPanel::close(%this, %accepted, %messageCode) {
    %this.setVisible(0);
    focusTopWindow();
    0.setVisible();
    setActionMapsEnabled(1);
    if ((%this SPC currentScreen $= "acceptDecline")) {
    }
    if ((%this SPC currentScreen $= "items_AcceptDecline")) {
        if (!(isDefined("%accepted"))) {
            %accepted = 0;
            geGiftingPanelBackground;
        }
        if (!(isDefined("%messageCode"))) {
            %messageCode = "DECLINED";
            PlayGui;
        }
        %this.doAccept(sourcePlayerName, giftTransactionID, %accepted, %messageCode);
    }
    cancel($gGiftingPanelAcceptDeclineTimerID);
    return 1;
};
function geGiftingPanel::openFromPendingTransactionRecord(%this, %pendingTransactionRecord) {
    targetPlayerName.open("initiate");
    (%pendingTransactionRecord SPC currencyType $= "vPoints").setValue();
    (%pendingTransactionRecord SPC currencyType $= "vBux").setValue();
    currencyAmount.setValue();
    personalMessage.setValue();
    refresh();
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
    %ghost = Player::findPlayerInstance(otherPlayerName);
    %this;
    if (!(isObject(%ghost))) {
        error(%this @ otherPlayerName);
        %this.close();
        return getScopeName() @ " " @ "- couldn't find target player:" @ " ";
    }
    %text = %this @ otherPlayerName;
    "<just:right><clip:1000>";
    %text.setTextWithStyle();
    %otherPlayerPortraitUrl = geGiftingOtherPlayerName @ $Net::AvatarURL @ %this @ urlEncode(otherPlayerName) @ "?size=M";
    geGiftingOtherPlayerPortrait @ "platform/client/ui/tgf/tgf_profile_default_" @ %ghost.getGender().setBitmap();
    %otherPlayerPortraitUrl.downloadAndApplyBitmap();
    if ((%this SPC currentScreen $= "initiate")) {
        1.setVisible();
        0.setVisible();
        0.setVisible();
        %this.refreshScreen_Initiate();
    }
    if ((%this SPC currentScreen $= "confirmation")) {
        0.setVisible();
        1.setVisible();
        0.setVisible();
        %this.refreshScreen_Confirmation();
    }
    if ((%this SPC currentScreen $= "acceptDecline")) {
        0.setVisible();
        0.setVisible();
        1.setVisible();
        %this.refreshScreen_AcceptDecline();
    }
    if ((%this SPC currentScreen $= "items_acceptDecline")) {
        0.setVisible();
        0.setVisible();
        1.setVisible();
        %this.refreshScreen_ItemsAcceptDecline();
    }
    error(%this @ currentScreen @ " " @ getTrace());
};
function geGiftingPanel::refreshScreen_Initiate(%this) {
    %text = %this @ otherPlayerName;
    "The gift of cash for" @ " ";
    %text.setTextWithStyle();
    GiftType = geGiftingTitle @ "currency" @ %this;
    %amountInTheBank = %this.getAmountInBankOfCurrentCurrency();
    %level = $player.getRespektLevel();
    %levelName = respektLevelToNameWithIndefiniteArticle(%level);
    %limitVpGive = getGiftingCapsForLevel(%level, "vPoints", "give");
    %limitVbGet = getGiftingCapsForLevel(%level, "vPoints", "recv");
    %limitVbGive = getGiftingCapsForLevel(%level, "vBux", "give");
    %limitVbGet = getGiftingCapsForLevel(%level, "vBux", "recv");
    %text = textBody;
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
    geGiftingTextType @ textBody.setTextWithStyle();
    if (%enableVP) {
    }
    textBodyI.setTextWithStyle();
    if (%enableVB) {
    }
    textBodyI.setTextWithStyle();
    if (getValue()) {
    }
    if (getValue()) {
        %currencyType = getValue() ? "vPoints" : "vBux";
        geGiftingCurrencyType_vPoints;
        %vpText = "vPoints";
        geGiftingCurrencyType_vBux;
        %vbText = "vBux";
        geGiftingCurrencyType_vPoints;
        if (getValue()) {
        }
        %currencyText = textBody @ geGiftingText3 @ geGiftingCurrencyType_vPoints @ "<spush><color:159fe7>" @ %vpText @ "<spop> " @ "<spush><color:13b93c>" @ %vbText @ "<spop>";
        geGiftingText3;
        %text = textBody;
        geGiftingTextAmt;
        %text = strreplace(%text, "[GIFTTYPE]", %currencyText);
        geGiftingText3;
        %text = geGiftingText2 @ "<spush><font:Arial Bold:20>2.<spop> " @ %text;
        textBody;
        %vpText = (geGiftingEditAmt == getValue()) ? "vPoint" : "vPoints";
        1.0;
        %vbText = (geGiftingEditAmt == getValue()) ? "vBuck" : "vBux";
        1.0;
        if (getValue()) {
        }
        %currencyText = geGiftingText2 @ geGiftingText2 @ geGiftingCurrencyType_vPoints @ "<spush><color:159fe7>" @ %vpText @ "<spop> " @ "<spush><color:13b93c>" @ %vbText @ "<spop>";
        geGiftingTextType @ "<spush><font:Arial Bold:20>1.<spop> ";
        if ((getValue() < %amountInTheBank)) {
            %currencyText = geGiftingCurrencyType_vPoints @ geGiftingCurrencyType_vBux @ geGiftingEditAmt @ %currencyText @ " <spush><b><color:ff0000dd>.. you only have " @ %amountInTheBank @ "!";
        }
        %text.setTextWithStyle();
        %currencyText.setTextWithStyle();
        1.setVisible();
    }
    "<color:ffffff40><spush><font:Arial Bold:20>2.<spop> amount".setTextWithStyle();
    "".setTextWithStyle();
    0.setVisible();
    if ((geGiftingEditAmt > getValue())) {
    }
    if ((getValue() >= %amountInTheBank)) {
        geGiftingTextMsg @ textBody.setTextWithStyle();
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
    if ((%this SPC GiftType $= "currency")) {
        %ret = gifting_composeGiftDescriptionCurrency(currencyType, currencyAmount);
        %this;
    }
    %ret = gifting_composeGiftDescriptionItems(skus);
    %this;
    return %ret;
};
function gifting_composeGiftDescriptionCurrency(%currencyType, %currencyAmount) {
    %vpText = (1.0 == %currencyAmount) ? "vPoint" : "vPoints";
    %vbText = (1.0 == %currencyAmount) ? "vBuck" : "vBux";
    if ((%currencyAmount @ " " SPC %currencyType $= "VPOINTS")) {
    }
    %text = "<spush><color:159fe7>" @ %vpText @ "<spop>" @ "<spush><color:13b93c>" @ %vbText @ "<spop>";
    return %text;
};
function gifting_composeGiftDescriptionCurrency2(%currencyType, %currencyAmount) {
    %vpText = (1.0 == %currencyAmount) ? "vPoint" : "vPoints";
    %vbText = (1.0 == %currencyAmount) ? "vBuck" : "vBux";
    %aFew = (%currencyType $= "VPOINTS") ? 50 : 5;
    %sardonicism = (%aFew < %currencyAmount) ? "whole " : "";
    if ((%currencyAmount @ " " @ %sardonicism SPC %currencyType $= "VPOINTS")) {
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
    personalMessage = geGiftingEditMsg @ getValue() @ %this;
    geGiftingButtonBack;
    personalMessage = %this @ StripMLControlChars(personalMessage) @ %this;
    geGiftingButtonBack;
    if ((%this SPC personalMessage $= "")) {
        personalMessage = geGiftingButtonBack @ "(no message)" @ %this;
    }
    %text = "<tab:80>";
    %text = %text @ %text[$MsgCat::gifting @ "CAVEAT-MUNEROR"];
    %text = %text @ "<br>";
    %text = %text @ "<br>Giving:" @ "\t" @ %this.getGiftDescription();
    %text = %this @ targetPlayerName;
    %text @ "<br>To:" @ "\t";
    %text = %this @ personalMessage;
    %text @ "<br>With Message:" @ "\t";
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
    %otherPlayer = Player::findPlayerInstance(otherPlayerName);
    %this;
    %text = "";
    geGiftingButtonCancel;
    %text = geGiftingButtonNext @ %text @ %text[$MsgCat::gifting @ "ACCEPT-OR-DECLINE"];
    geGiftingButtonNext;
    %text = strreplace(%text, "[OTHERPLAYER]", otherPlayerName);
    %this;
    %text = strreplace(%text, "[PERSONALMESSAGE]", personalMessage);
    %this;
    %text = strreplace(%text, "[GIFTAMOUNT]", currencyAmount);
    %this;
    %text = strreplace(%text, "[GIFTTYPE]", currencyType);
    %this;
    %text = strreplace(%text, "[GIFTDESC]", %this.getGiftDescription());
    geGiftingButtonBack;
    %text = strreplace(%text, "[OTHERPLAYER_HE_SHE_IT]", getPronounHeSheIt(%otherPlayer));
    geGiftingButtonBack;
    %text = strreplace(%text, "[OTHERPLAYER_HIM_HER_IT]", getPronounHimHerIt(%otherPlayer));
    geGiftingTitle;
    %text = strreplace(%text, "[IT_THEM]", getPronounItThem(currencyAmount));
    %this;
    %text.setTextWithStyle();
    %this.startCountdownTimer((1000.0 * 30.0));
};
function geGiftingPanel::refreshScreen_ItemsAcceptDecline(%this) {
    %text = "The Gift of Libation";
    %text.setTextWithStyle();
    GiftType = geGiftingTitle @ "items" @ %this;
    "DECLINE".setText();
    1.setActive();
    "ACCEPT".setText();
    1.setActive();
    0.setVisible();
    %otherPlayer = Player::findPlayerInstance(otherPlayerName);
    %this;
    %numItems = getWordCount(skus);
    %this;
    if (making) {
    }
    %text = %this[$MsgCat::giftingItems @ "ACCEPT-OR-DECLINE-MAKING"][$MsgCat::giftingItems @ "ACCEPT-OR-DECLINE"];
    %this[$MsgCat::giftingItems @ "ACCEPT-OR-DECLINE-MAKING"];
    %text = strreplace(%text, "[OTHERPLAYER]", otherPlayerName);
    %this;
    %text = strreplace(%text, "[GIFTDESC]", %this.getGiftDescription());
    %this;
    %text = strreplace(%text, "[OTHERPLAYER_HE_SHE_IT]", getPronounHeSheIt(%otherPlayer));
    geGiftingButtonCancel;
    %text = strreplace(%text, "[OTHERPLAYER_HIM_HER_IT]", getPronounHimHerIt(%otherPlayer));
    geGiftingButtonNext;
    %text = strreplace(%text, "[IT_THEM]", getPronounItThem(%numItems));
    geGiftingButtonNext;
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
    rotRadians = (6.0 / (0.001 * $gGiftingPanelCountdownMSRemaining)) @ geGiftingAcceptDeclineClock_littleHand;
    rotRadians = (0.001 * $gGiftingPanelCountdownMSRemaining) @ geGiftingAcceptDeclineClock_bigHand;
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
    if (making) {
    }
    %text = %this[$MsgCat::giftingItems @ "E-TOOSLOW"][$MsgCat::giftingItems @ "E-TOOSLOW"];
    %this[$MsgCat::giftingItems @ "E-TOOSLOW"];
    %text = strreplace(%text, "[OTHERPLAYER]", otherPlayerName);
    %this;
    %text = strreplace(%text, "[PERSONALMESSAGE]", personalMessage);
    %this;
    handleSystemMessage("msgInfoMessage", %text);
};
function geGiftingEditAmt::validate(%this) {
    refresh();
};
function geGiftingEditAmt::onEnter(%this) {
    1.makeFirstResponder();
};
function geGiftingEditMsg::validate(%this) {
    refresh();
};
function geGiftingEditMsg::onEnter(%this) {
    if (isActive()) {
        eval(command);
    }
};
function geGiftingPanel::onNext(%this) {
    if ((%this SPC currentScreen $= "initiate")) {
        %this.onNext_Initiate();
    }
    if ((%this SPC currentScreen $= "confirmation")) {
        %this.onNext_Confirmation();
    }
    if ((%this SPC currentScreen $= "acceptDecline")) {
        %this.onNext_AcceptDecline();
    }
    if ((%this SPC currentScreen $= "items_acceptDecline")) {
        %this.onNext_Items_AcceptDecline();
    }
    error(%this @ currentScreen @ " " @ getTrace());
};
function geGiftingPanel::onBack(%this) {
    if ((%this SPC currentScreen $= "initiate")) {
        %this.onBack_Initiate();
    }
    if ((%this SPC currentScreen $= "confirmation")) {
        %this.onBack_Confirmation();
    }
    if ((%this SPC currentScreen $= "acceptDecline")) {
        %this.onBack_AcceptDecline();
    }
    if ((%this SPC currentScreen $= "items_AcceptDecline")) {
        %this.onBack_Items_AcceptDecline();
    }
    error(%this @ currentScreen @ " " @ getTrace());
};
function geGiftingPanel::onNext_Initiate(%this) {
    currencyType = geGiftingCurrencyType_vPoints @ getValue() ? "vPoints" : "vBux" @ %this;
    currencyAmount = geGiftingEditAmt @ getValue() @ %this;
    %amountInTheBank = %this.getAmountInBankOfCurrentCurrency();
    if ((%this > currencyAmount)) {
        "".setValue();
        error(%this @ currencyAmount @ " " @ %amountInTheBank @ " " @ getTrace());
        MessageBoxOK("Something is Wrong", "Hm, something went wrong.\nPlease enter a new amount..", "");
        %this.refresh();
        return getScopeName() @ " " @ "- trying to give more money than owned!" @ " ";
    }
    currentScreen = "confirmation" @ %this;
    %this.refresh();
};
function geGiftingPanel::onNext_Confirmation(%this) {
    %dlg = MessageBoxOK("The Gift of Cash", "<br>Sending" @ " " @ %this.getGiftDescription() @ " " @ "to" @ " " @ %this @ targetPlayerName @ "..<br>", "");
    if ($StandAlone) {
        %dlg.onDryRunSuccess();
    }
    %request = sendRequest_GiftCurrency(targetPlayerName, currencyType, currencyAmount, 1);
    onDoneOrErrorCallback_GiftCurrency;
    dlg = %this @ %dlg @ %request;
    %this;
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
    commandToServer('GiftingCurrency_Initiated', targetPlayerName, %giftTransactionID, personalMessage, currencyType, currencyAmount);
    %this.registerPendingTransaction(%giftTransactionID, currencyType, currencyAmount, targetPlayerName, %dlg, personalMessage);
};
function geGiftingPanel::registerPendingTransaction(%this, %giftTransactionID, %currencyType, %currencyAmount, %targetPlayerName, %dlg, %personalMessage) {
    if (!(isObject(PendingTransactionsList))) {
        PendingTransactionsList = %this @ safeNewScriptObject("StringMap", "", 0) @ %this;
    }
    if (!(%this SPC PendingTransactionsList.get(%giftTransactionID) $= "")) {
        error(getScopeName() @ " " @ "- transaction already exists!" @ " " @ %giftTransactionID @ " " @ getTrace());
        return;
    }
    %pendingTransactionRecord = safeNewScriptObject("ScriptObject", "", 0);
    giftTransactionID = %giftTransactionID @ %pendingTransactionRecord;
    currencyType = %currencyType @ %pendingTransactionRecord;
    currencyAmount = %currencyAmount @ %pendingTransactionRecord;
    targetPlayerName = %targetPlayerName @ %pendingTransactionRecord;
    dlg = %dlg @ %pendingTransactionRecord;
    personalMessage = %personalMessage @ %pendingTransactionRecord;
    PendingTransactionsList.put(%giftTransactionID, %pendingTransactionRecord);
};
function geGiftingPanel::getPendingTransaction(%this, %giftTransactionID) {
    if (!(isObject(PendingTransactionsList))) {
        error(getScopeName() @ " " @ "- no PendingTransactionsList!" @ " " @ getTrace());
        return "";
    }
    %pendingTransactionRecord = PendingTransactionsList.get(%giftTransactionID);
    %this;
    if ((%pendingTransactionRecord $= "")) {
        error(getScopeName() @ " " @ "- no such transaction:" @ " " @ %giftTransactionID @ " " @ getTrace());
        return "";
    }
    return %pendingTransactionRecord;
};
function geGiftingPanel::deletePendingTransaction(%this, %giftTransactionID) {
    if (!(isObject(PendingTransactionsList))) {
        error(getScopeName() @ " " @ "- no PendingTransactionsList!" @ " " @ getTrace());
        return "";
    }
    %pendingTransactionRecord = PendingTransactionsList.get(%giftTransactionID);
    %this;
    if ((%pendingTransactionRecord $= "")) {
        error(getScopeName() @ " " @ "- no such transaction:" @ " " @ %giftTransactionID @ " " @ getTrace());
        return "";
    }
    %pendingTransactionRecord.delete();
    PendingTransactionsList.remove(%giftTransactionID);
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
    if ((geGiftingPanel SPC %acceptMode $= "decline")) {
        %sourcePlayerName.doAccept(%giftTransactionID, 0, "DECLINED-AUTO");
    }
    if ((geGiftingPanel SPC %acceptMode $= "ask")) {
        if (isVisible()) {
            %sourcePlayerName.doAccept(%giftTransactionID, 0, "DECLINED-BUSY");
        }
        giftTransactionID = geGiftingPanel @ %giftTransactionID @ geGiftingPanel;
        geGiftingPanel;
        personalMessage = TryFixBadWords(%personalMessage) @ geGiftingPanel;
        currencyType = %currencyType @ geGiftingPanel;
        currencyAmount = %currencyAmount @ geGiftingPanel;
        GiftType = "currency" @ geGiftingPanel;
        %sourcePlayerName.open("acceptDecline");
    }
};
function geGiftingPanel::doAccept(%this, %sourcePlayerName, %giftTransactionID, %accepted, %messageCode) {
    if ((%this SPC currentScreen $= "items_acceptDecline")) {
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
    if (isObject(dlg)) {
        dlg.close();
    }
    if (!(%accepted)) {
        %otherPlayerName = targetPlayerName;
        %pendingTransactionRecord;
        %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
        %pendingTransactionRecord;
        if (!(isObject(%otherPlayer))) {
            error(getScopeName() @ " " @ "- can't find other player:" @ " " @ %otherPlayerName @ " " @ %giftTransactionID);
            %messageCode = "E-TARGET-MISSING";
            %pendingTransactionRecord;
        }
        %text = strreplace(%messageCode[$MsgCat::gifting @ %messageCode], "[OTHERPLAYER]", "<linkcolor:ffddeeff><a:gamelink " @ munge(%otherPlayerName) @ ">" @ StripMLControlChars(%otherPlayerName) @ "</a>");
        %text = strreplace(%text, "[OTHERPLAYER_HE_SHE_IT]", getPronounHeSheIt(%otherPlayer));
        %text = strreplace(%text, "[OTHERPLAYER_HIM_HER_IT]", getPronounHimHerIt(%otherPlayer));
        MessageBoxOK("Woops..", %text, "");
        %giftTransactionID.deletePendingTransaction();
        return geGiftingPanel;
    }
    %request = sendRequest_GiftCurrency(targetPlayerName, currencyType, currencyAmount, 0);
    onDoneOrErrorCallback_GiftCurrency;
    giftTransactionID = %pendingTransactionRecord @ %giftTransactionID @ %request;
    %pendingTransactionRecord;
    personalMessage = %pendingTransactionRecord @ personalMessage @ %request;
    %pendingTransactionRecord;
};
function onDoneOrErrorCallback_GiftCurrency(%request) {
    %otherPlayerName = %request.getURLParam("payee");
    %currencyType = %request.getURLParam("currencyType");
    %currencyAmount = %request.getURLParam("amount");
    %dryRun = %request.getURLParam("dryRun", 1);
    %succeeded = (%request.getResult("status") $= "success");
    if (%succeeded) {
        if (%dryRun) {
            dlg.onDryRunSuccess();
            return %request;
        }
        getBalancesAndScores();
        commandToServer('GiftingCurrency_Notify', %otherPlayerName, giftTransactionID, personalMessage, %currencyType, %currencyAmount);
        %msg = %request;
        %request;
    }
    if (isObject(dlg)) {
        dlg.close();
    }
    %errorCode = %request.getResult("errorCode");
    %request;
    if ((%request SPC %errorCode $= "")) {
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
    %levelNum = respektScoreToLevel(respekt);
    %info;
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
        giftTransactionID.deletePendingTransaction();
    }
};
function geGiftingPanel::onBack_Initiate(%this) {
    error(getScopeName() @ " " @ "- shouldn't be here." @ " " @ getTrace());
};
function geGiftingPanel::onBack_Confirmation(%this) {
    currentScreen = "initiate" @ %this;
    %this.refresh();
    1.makeFirstResponder();
};
function geGiftingPanel::getAmountInBankOfCurrentCurrency(%this) {
    if (!(getValue())) {
    }
    if (!(getValue())) {
        return 0;
    }
    if (getValue()) {
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
            (geGiftingCurrencyType_vPoints SPC %currencyType $= "vPoints").setValue();
            (geGiftingCurrencyType_vBux SPC %currencyType $= "vBux").setValue();
            %currencyAmount.setValue();
            refresh();
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
    clear();
    %num = %request.getValue("giftingCapLevelCount");
    gGiftingCapTable;
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
