$gGiftingEnabled_vPoints = 0;
$gGiftingEnabled_vBux = 0;
if ($StandAlone)
{
    $gGiftingEnabled_vPoints = 1;
    $gGiftingEnabled_vBux = 1;
}
function geGiftingPanel::open(%this, %otherPlayerName, %whichScreen)
{
    if (%otherPlayerName $= $Player::Name)
    {
        return ;
    }
    if ($StandAlone)
    {
        %request = new ManagerRequest();
        parseGiftingSettings(%request);
        %request.delete();
        error(getScopeName() SPC "- using hardcoded daily caps.");
        setMyRespektPoints(54321, 0);
    }
    %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
    if (!isObject(%otherPlayer))
    {
        error(getScopeName() SPC "- could not find other player:" SPC %otherPlayerName SPC getTrace());
        return ;
    }
    if (%whichScreen $= "initiate")
    {
        if (!%this.isInRange(%otherPlayer))
        {
            %text = $MsgCat::gifting["E-TARGET-TOOFAR"];
            %text = strreplace(%text, "[OTHERPLAYER]", %otherPlayerName);
            MessageBoxOK("Get Closer!", %text, "");
            return ;
        }
        requestPlayerInfoFor(%otherPlayerName);
    }
    PlayGui.add(geGiftingPanelBackground);
    geGiftingPanelBackground.setVisible(1);
    geGiftingPanelBackground.resize(getWord(PlayGui.getExtent(), 0), getWord(PlayGui.getExtent(), 1));
    geGiftingPanelBackground.reposition(0, 0);
    PlayGui.focusAndRaise(geGiftingPanelBackground);
    PlayGui.ensureAdded(%this);
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    setActionMapsEnabled(0);
    %this.otherPlayerName = %otherPlayerName;
    if (%whichScreen $= "acceptDecline")
    {
        %this.sourcePlayerName = %otherPlayerName;
        %this.targetPlayerName = $Player::Name;
    }
    else
    {
        %this.sourcePlayerName = $Player::Name;
        %this.targetPlayerName = %otherPlayerName;
    }
    geGiftingCurrencyType_vPoints.setValue(0);
    geGiftingCurrencyType_vBux.setValue(0);
    geGiftingEditAmt.setValue("");
    geGiftingEditMsg.setValue("");
    %this.currentScreen = %whichScreen;
    %this.refresh();
    return ;
}
function geGiftingPanel::close(%this, %accepted, %messageCode)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    geGiftingPanelBackground.setVisible(0);
    setActionMapsEnabled(1);
    if ((%this.currentScreen $= "acceptDecline") && (%this.currentScreen $= "items_AcceptDecline"))
    {
        if (!isDefined("%accepted"))
        {
            %accepted = 0;
        }
        if (!isDefined("%messageCode"))
        {
            %messageCode = "DECLINED";
        }
        %this.doAccept(%this.sourcePlayerName, %this.giftTransactionID, %accepted, %messageCode);
    }
    cancel($gGiftingPanelAcceptDeclineTimerID);
    return 1;
}
function geGiftingPanel::openFromPendingTransactionRecord(%this, %pendingTransactionRecord)
{
    geGiftingPanel.open(%pendingTransactionRecord.targetPlayerName, "initiate");
    geGiftingCurrencyType_vPoints.setValue(%pendingTransactionRecord.currencyType $= "vPoints");
    geGiftingCurrencyType_vBux.setValue(%pendingTransactionRecord.currencyType $= "vBux");
    geGiftingEditAmt.setValue(%pendingTransactionRecord.currencyAmount);
    geGiftingEditMsg.setValue(%pendingTransactionRecord.personalMessage);
    geGiftingPanel.refresh();
    geGiftingEditMsg.makeFirstResponder(1);
    return ;
}
function geGiftingPanel::isInRange(%this, %otherPlayer)
{
    if (!isObject(%otherPlayer))
    {
        return 0;
    }
    %coAnimEntry = findCoAnimEntry("gift");
    %range = getField(%coAnimEntry, 3);
    %ret = Math::isInRange($player.getPosition(), %otherPlayer.getPosition(), %range);
    return %ret;
}
function geGiftingPanel::refresh(%this)
{
    %ghost = Player::findPlayerInstance(%this.otherPlayerName);
    if (!isObject(%ghost))
    {
        error(getScopeName() SPC "- couldn\'t find target player:" SPC %this.otherPlayerName);
        %this.close();
        return ;
    }
    %text = "<just:right><clip:1000>" @ %this.otherPlayerName;
    geGiftingOtherPlayerName.setTextWithStyle(%text);
    %otherPlayerPortraitUrl = $Net::AvatarURL @ urlEncode(%this.otherPlayerName) @ "?size=M";
    geGiftingOtherPlayerPortrait.setBitmap("platform/client/ui/tgf/tgf_profile_default_" @ %ghost.getGender());
    geGiftingOtherPlayerPortrait.downloadAndApplyBitmap(%otherPlayerPortraitUrl);
    if (%this.currentScreen $= "initiate")
    {
        geGiftingScreen_Initiate.setVisible(1);
        geGiftingScreen_Confirmation.setVisible(0);
        geGiftingScreen_AcceptDecline.setVisible(0);
        %this.refreshScreen_Initiate();
    }
    else
    {
        if (%this.currentScreen $= "confirmation")
        {
            geGiftingScreen_Initiate.setVisible(0);
            geGiftingScreen_Confirmation.setVisible(1);
            geGiftingScreen_AcceptDecline.setVisible(0);
            %this.refreshScreen_Confirmation();
        }
        else
        {
            if (%this.currentScreen $= "acceptDecline")
            {
                geGiftingScreen_Initiate.setVisible(0);
                geGiftingScreen_Confirmation.setVisible(0);
                geGiftingScreen_AcceptDecline.setVisible(1);
                %this.refreshScreen_AcceptDecline();
            }
            else
            {
                if (%this.currentScreen $= "items_acceptDecline")
                {
                    geGiftingScreen_Initiate.setVisible(0);
                    geGiftingScreen_Confirmation.setVisible(0);
                    geGiftingScreen_AcceptDecline.setVisible(1);
                    %this.refreshScreen_ItemsAcceptDecline();
                }
                else
                {
                    error(getScopeName() SPC "- unknown currentScreen:" SPC %this.currentScreen SPC getTrace());
                }
            }
        }
    }
    return ;
}
function geGiftingPanel::refreshScreen_Initiate(%this)
{
    %text = "The gift of cash for" SPC %this.otherPlayerName;
    geGiftingTitle.setTextWithStyle(%text);
    %this.GiftType = "currency";
    %amountInTheBank = %this.getAmountInBankOfCurrentCurrency();
    %level = $player.getRespektLevel();
    %levelName = respektLevelToNameWithIndefiniteArticle(%level);
    %limitVpGive = getGiftingCapsForLevel(%level, "vPoints", "give");
    %limitVbGet = getGiftingCapsForLevel(%level, "vPoints", "recv");
    %limitVbGive = getGiftingCapsForLevel(%level, "vBux", "give");
    %limitVbGet = getGiftingCapsForLevel(%level, "vBux", "recv");
    %text = geGiftingTextLimits.textBody;
    %text = strreplace(%text, "[LEVEL]", %level);
    %text = strreplace(%text, "[LEVELNAME]", %levelName);
    %text = strreplace(%text, "[VPGIVE]", %limitVpGive);
    %text = strreplace(%text, "[VBGIVE]", %limitVbGive);
    if (!$gGiftingEnabled_vPoints && !$gGiftingEnabled_vBux)
    {
        %text = %text @ "<br><color:ff3333>Gifting is temporarily disabled.";
    }
    else
    {
        if (!$gGiftingEnabled_vPoints)
        {
            %text = %text @ "<br><color:ff3333>Gifting vPoints is temporarily disabled.";
        }
        else
        {
            if (!$gGiftingEnabled_vBux)
            {
                %text = %text @ "<br><color:ff3333>Gifting vBux is temporarily disabled.";
            }
        }
    }
    geGiftingTextLimits.setTextWithStyle(%text);
    %enableVP = (%limitVpGive > 0) && $gGiftingEnabled_vPoints;
    %enableVB = (%limitVbGive > 0) && $gGiftingEnabled_vBux;
    geGiftingCurrencyType_vPoints.setActive(%enableVP);
    geGiftingCurrencyType_vBux.setActive(%enableVB);
    geGiftingTextType.setTextWithStyle("<spush><font:Arial Bold:20>1.<spop> " @ geGiftingTextType.textBody);
    geGiftingText2.setTextWithStyle(%enableVP ? geGiftingText2 : geGiftingText2);
    geGiftingText3.setTextWithStyle(%enableVB ? geGiftingText3 : geGiftingText3);
    if (geGiftingCurrencyType_vPoints.getValue() && geGiftingCurrencyType_vBux.getValue())
    {
        %currencyType = geGiftingCurrencyType_vPoints.getValue() ? "vPoints" : "vBux";
        %vpText = "vPoints";
        %vbText = "vBux";
        %currencyText = geGiftingCurrencyType_vPoints.getValue() ? "<spush><color:159fe7>" : "<spush><color:13b93c>";
        %text = geGiftingTextAmt.textBody;
        %text = strreplace(%text, "[GIFTTYPE]", %currencyText);
        %text = "<spush><font:Arial Bold:20>2.<spop> " @ %text;
        %vpText = geGiftingEditAmt.getValue() == 1 ? "vPoint" : "vPoints";
        %vbText = geGiftingEditAmt.getValue() == 1 ? "vBuck" : "vBux";
        %currencyText = geGiftingCurrencyType_vPoints.getValue() ? "<spush><color:159fe7>" : "<spush><color:13b93c>";
        if (%amountInTheBank < geGiftingEditAmt.getValue())
        {
            %currencyText = %currencyText @ " <spush><b><color:ff0000dd>.. you only have " @ %amountInTheBank @ "!";
        }
        geGiftingTextAmt.setTextWithStyle(%text);
        geGiftingTextCurrency.setTextWithStyle(%currencyText);
        geGiftingEditAmt.setVisible(1);
    }
    else
    {
        geGiftingTextAmt.setTextWithStyle("<color:ffffff40><spush><font:Arial Bold:20>2.<spop> amount");
        geGiftingTextCurrency.setTextWithStyle("");
        geGiftingEditAmt.setVisible(0);
    }
    if ((geGiftingEditAmt.getValue() > 0) && (%amountInTheBank >= geGiftingEditAmt.getValue()))
    {
        geGiftingTextMsg.setTextWithStyle("<spush><font:Arial Bold:20>3.<spop> " @ geGiftingTextMsg.textBody);
        geGiftingEditMsg.setVisible(1);
        geGiftingButtonNext.setActive(1);
    }
    else
    {
        geGiftingTextMsg.setTextWithStyle("<color:ffffff40><spush><font:Arial Bold:20>3.<spop> message");
        geGiftingEditMsg.setVisible(0);
        geGiftingEditAmt.makeFirstResponder(1);
        geGiftingButtonNext.setActive(0);
    }
    geGiftingButtonBack.setVisible(1);
    geGiftingButtonBack.setActive(0);
    geGiftingButtonBack.setText("< Back");
    geGiftingButtonNext.setText("Next >");
    geGiftingButtonCancel.setVisible(1);
    return ;
}
function geGiftingPanel::getGiftDescription(%this)
{
    if (%this.GiftType $= "currency")
    {
        %ret = gifting_composeGiftDescriptionCurrency(%this.currencyType, %this.currencyAmount);
    }
    else
    {
        %ret = gifting_composeGiftDescriptionItems(%this.skus);
    }
    return %ret;
}
function gifting_composeGiftDescriptionCurrency(%currencyType, %currencyAmount)
{
    %vpText = %currencyAmount == 1 ? "vPoint" : "vPoints";
    %vbText = %currencyAmount == 1 ? "vBuck" : "vBux";
    %text = %currencyAmount SPC %currencyType $= "VPOINTS" ? "<spush><color:159fe7>" : "<spush><color:13b93c>";
    return %text;
}
function gifting_composeGiftDescriptionCurrency2(%currencyType, %currencyAmount)
{
    %vpText = %currencyAmount == 1 ? "vPoint" : "vPoints";
    %vbText = %currencyAmount == 1 ? "vBuck" : "vBux";
    %aFew = %currencyType $= "VPOINTS" ? 50 : 5;
    %sardonicism = %currencyAmount < %aFew ? "whole " : "";
    %text = %currencyAmount SPC %sardonicism @ %currencyType $= "VPOINTS" ? "<spush><color:002288>" : "<spush><color:005500>";
    return %text;
}
function gifting_composeGiftDescriptionItems(%skus)
{
    %text = "a" SPC SkuManager.getSkuShortDescriptions(%skus, "and a ", 0);
    return %text;
}
function geGiftingPanel::refreshScreen_Confirmation(%this)
{
    geGiftingButtonBack.setVisible(1);
    geGiftingButtonBack.setActive(1);
    geGiftingButtonBack.setText("< Back");
    %this.personalMessage = geGiftingEditMsg.getValue();
    %this.personalMessage = StripMLControlChars(%this.personalMessage);
    if (%this.personalMessage $= "")
    {
        %this.personalMessage = "(no message)";
    }
    %text = "<tab:80>";
    %text = %text @ $MsgCat::gifting["CAVEAT-MUNEROR"];
    %text = %text @ "<br>";
    %text = %text @ "<br>Giving:" TAB %this.getGiftDescription();
    %text = %text @ "<br>To:" TAB %this.targetPlayerName;
    %text = %text @ "<br>With Message:" TAB %this.personalMessage;
    %text = strreplace(%text, "[GIFTDESC]", %this.getGiftDescription());
    geGiftingTextConfirm.setTextWithStyle(%text);
    geGiftingButtonNext.setText("Give!");
    geGiftingButtonNext.makeFirstResponder(1);
    geGiftingButtonCancel.setVisible(1);
    return ;
}
$gGiftingPanelAcceptDeclineTimerID = 0;
function geGiftingPanel::refreshScreen_AcceptDecline(%this)
{
    %text = "The gift of cash";
    geGiftingTitle.setTextWithStyle(%text);
    geGiftingButtonBack.setText("DECLINE");
    geGiftingButtonBack.setActive(1);
    geGiftingButtonNext.setText("ACCEPT");
    geGiftingButtonNext.setActive(1);
    geGiftingButtonCancel.setVisible(0);
    %otherPlayer = Player::findPlayerInstance(%this.otherPlayerName);
    %text = "";
    %text = %text @ $MsgCat::gifting["ACCEPT-OR-DECLINE"];
    %text = strreplace(%text, "[OTHERPLAYER]", %this.otherPlayerName);
    %text = strreplace(%text, "[PERSONALMESSAGE]", %this.personalMessage);
    %text = strreplace(%text, "[GIFTAMOUNT]", %this.currencyAmount);
    %text = strreplace(%text, "[GIFTTYPE]", %this.currencyType);
    %text = strreplace(%text, "[GIFTDESC]", %this.getGiftDescription());
    %text = strreplace(%text, "[OTHERPLAYER_HE_SHE_IT]", getPronounHeSheIt(%otherPlayer));
    %text = strreplace(%text, "[OTHERPLAYER_HIM_HER_IT]", getPronounHimHerIt(%otherPlayer));
    %text = strreplace(%text, "[IT_THEM]", getPronounItThem(%this.currencyAmount));
    geGiftingTextAcceptDecline.setTextWithStyle(%text);
    %this.startCountdownTimer(30 * 1000);
    return ;
}
function geGiftingPanel::refreshScreen_ItemsAcceptDecline(%this)
{
    %text = "The Gift of Libation";
    geGiftingTitle.setTextWithStyle(%text);
    %this.GiftType = "items";
    geGiftingButtonBack.setText("DECLINE");
    geGiftingButtonBack.setActive(1);
    geGiftingButtonNext.setText("ACCEPT");
    geGiftingButtonNext.setActive(1);
    geGiftingButtonCancel.setVisible(0);
    %otherPlayer = Player::findPlayerInstance(%this.otherPlayerName);
    %numItems = getWordCount(%this.skus);
    if (%this.making)
    {
    }
    else
    {
    }
    %text = ;
    %text = strreplace(%text, "[OTHERPLAYER]", %this.otherPlayerName);
    %text = strreplace(%text, "[GIFTDESC]", %this.getGiftDescription());
    %text = strreplace(%text, "[OTHERPLAYER_HE_SHE_IT]", getPronounHeSheIt(%otherPlayer));
    %text = strreplace(%text, "[OTHERPLAYER_HIM_HER_IT]", getPronounHimHerIt(%otherPlayer));
    %text = strreplace(%text, "[IT_THEM]", getPronounItThem(%numItems));
    geGiftingTextAcceptDecline.setTextWithStyle(%text);
    %this.startCountdownTimer(30 * 1000);
    return ;
}
function geGiftingPanel::startCountdownTimer(%this, %milliseconds)
{
    $gGiftingPanelCountdownMSRemaining = %milliseconds;
    %this.countdownTick();
    return ;
}
function geGiftingPanel::countdownTick(%this)
{
    %tickPeriod = 100;
    $gGiftingPanelCountdownMSRemaining = $gGiftingPanelCountdownMSRemaining - %tickPeriod;
    geGiftingAcceptDeclineClock_littleHand.rotRadians = ($gGiftingPanelCountdownMSRemaining * 0.001) / 6;
    geGiftingAcceptDeclineClock_bigHand.rotRadians = $gGiftingPanelCountdownMSRemaining * 0.001;
    %text = mFloor(($gGiftingPanelCountdownMSRemaining * 0.001) + 0.5);
    %text = %text @ "..";
    geGiftingAcceptDeclineClock_readout.setTextWithStyle(%text);
    cancel($gGiftingPanelAcceptDeclineTimerID);
    if (($gGiftingPanelCountdownMSRemaining > 0) && %this.isVisible())
    {
        $gGiftingPanelAcceptDeclineTimerID = %this.schedule(%tickPeriod, "countdownTick");
    }
    else
    {
        %this.close(0, "DECLINED-TIMEOUT");
        if (%this.making)
        {
        }
        else
        {
        }
        %text = ;
        %text = strreplace(%text, "[OTHERPLAYER]", %this.otherPlayerName);
        %text = strreplace(%text, "[PERSONALMESSAGE]", %this.personalMessage);
        handleSystemMessage("msgInfoMessage", %text);
    }
    return ;
}
function geGiftingEditAmt::validate(%this)
{
    geGiftingPanel.refresh();
    return ;
}
function geGiftingEditAmt::onEnter(%this)
{
    geGiftingEditMsg.makeFirstResponder(1);
    return ;
}
function geGiftingEditMsg::validate(%this)
{
    geGiftingPanel.refresh();
    return ;
}
function geGiftingEditMsg::onEnter(%this)
{
    if (geGiftingButtonNext.isActive())
    {
        eval(geGiftingButtonNext.command);
    }
    return ;
}
function geGiftingPanel::onNext(%this)
{
    if (%this.currentScreen $= "initiate")
    {
        %this.onNext_Initiate();
    }
    else
    {
        if (%this.currentScreen $= "confirmation")
        {
            %this.onNext_Confirmation();
        }
        else
        {
            if (%this.currentScreen $= "acceptDecline")
            {
                %this.onNext_AcceptDecline();
            }
            else
            {
                if (%this.currentScreen $= "items_acceptDecline")
                {
                    %this.onNext_Items_AcceptDecline();
                }
                else
                {
                    error(getScopeName() SPC "- unknown currentScreen:" SPC %this.currentScreen SPC getTrace());
                }
            }
        }
    }
    return ;
}
function geGiftingPanel::onBack(%this)
{
    if (%this.currentScreen $= "initiate")
    {
        %this.onBack_Initiate();
    }
    else
    {
        if (%this.currentScreen $= "confirmation")
        {
            %this.onBack_Confirmation();
        }
        else
        {
            if (%this.currentScreen $= "acceptDecline")
            {
                %this.onBack_AcceptDecline();
            }
            else
            {
                if (%this.currentScreen $= "items_AcceptDecline")
                {
                    %this.onBack_Items_AcceptDecline();
                }
                else
                {
                    error(getScopeName() SPC "- unknown currentScreen:" SPC %this.currentScreen SPC getTrace());
                }
            }
        }
    }
    return ;
}
function geGiftingPanel::onNext_Initiate(%this)
{
    %this.currencyType = geGiftingCurrencyType_vPoints.getValue() ? "vPoints" : "vBux";
    %this.currencyAmount = geGiftingEditAmt.getValue();
    %amountInTheBank = %this.getAmountInBankOfCurrentCurrency();
    if (%this.currencyAmount > %amountInTheBank)
    {
        geGiftingEditAmt.setValue("");
        error(getScopeName() SPC "- trying to give more money than owned!" SPC %this.currencyAmount SPC %amountInTheBank SPC getTrace());
        MessageBoxOK("Something is Wrong", "Hm, something went wrong.\nPlease enter a new amount..", "");
        %this.refresh();
        return ;
    }
    %this.currentScreen = "confirmation";
    %this.refresh();
    return ;
}
function geGiftingPanel::onNext_Confirmation(%this)
{
    %dlg = MessageBoxOK("The Gift of Cash", "<br>Sending" SPC %this.getGiftDescription() SPC "to" SPC %this.targetPlayerName @ "..<br>", "");
    if ($StandAlone)
    {
        geGiftingPanel.onDryRunSuccess(%dlg);
    }
    else
    {
        %request = sendRequest_GiftCurrency(%this.targetPlayerName, %this.currencyType, %this.currencyAmount, 1, onDoneOrErrorCallback_GiftCurrency);
        %request.dlg = %dlg;
    }
    %this.close();
    return ;
}
function geGiftingPanel::onNext_AcceptDecline(%this)
{
    %this.close(1, "ACCEPTED");
    return ;
}
function geGiftingPanel::onNext_Items_AcceptDecline(%this)
{
    %this.close(1, "ACCEPTED");
    return ;
}
function geGiftingPanel::onBack_AcceptDecline(%this)
{
    %this.close();
    return ;
}
function geGiftingPanel::onBack_Items_AcceptDecline(%this)
{
    %this.close();
    return ;
}
function geGiftingPanel::onDryRunSuccess(%this, %dlg)
{
    %giftTransactionID = MD5(getRandom(0, 1000000));
    commandToServer('GiftingCurrency_Initiated', %this.targetPlayerName, %giftTransactionID, %this.personalMessage, %this.currencyType, %this.currencyAmount);
    %this.registerPendingTransaction(%giftTransactionID, %this.currencyType, %this.currencyAmount, %this.targetPlayerName, %dlg, %this.personalMessage);
    return ;
}
function geGiftingPanel::registerPendingTransaction(%this, %giftTransactionID, %currencyType, %currencyAmount, %targetPlayerName, %dlg, %personalMessage)
{
    if (!isObject(%this.PendingTransactionsList))
    {
        %this.PendingTransactionsList = safeNewScriptObject("StringMap", "", 0);
    }
    if (!(%this.PendingTransactionsList.get(%giftTransactionID) $= ""))
    {
        error(getScopeName() SPC "- transaction already exists!" SPC %giftTransactionID SPC getTrace());
        return ;
    }
    %pendingTransactionRecord = safeNewScriptObject("ScriptObject", "", 0);
    %pendingTransactionRecord.giftTransactionID = %giftTransactionID;
    %pendingTransactionRecord.currencyType = %currencyType;
    %pendingTransactionRecord.currencyAmount = %currencyAmount;
    %pendingTransactionRecord.targetPlayerName = %targetPlayerName;
    %pendingTransactionRecord.dlg = %dlg;
    %pendingTransactionRecord.personalMessage = %personalMessage;
    %this.PendingTransactionsList.put(%giftTransactionID, %pendingTransactionRecord);
    return ;
}
function geGiftingPanel::getPendingTransaction(%this, %giftTransactionID)
{
    if (!isObject(%this.PendingTransactionsList))
    {
        error(getScopeName() SPC "- no PendingTransactionsList!" SPC getTrace());
        return "";
    }
    %pendingTransactionRecord = %this.PendingTransactionsList.get(%giftTransactionID);
    if (%pendingTransactionRecord $= "")
    {
        error(getScopeName() SPC "- no such transaction:" SPC %giftTransactionID SPC getTrace());
        return "";
    }
    return %pendingTransactionRecord;
}
function geGiftingPanel::deletePendingTransaction(%this, %giftTransactionID)
{
    if (!isObject(%this.PendingTransactionsList))
    {
        error(getScopeName() SPC "- no PendingTransactionsList!" SPC getTrace());
        return "";
    }
    %pendingTransactionRecord = %this.PendingTransactionsList.get(%giftTransactionID);
    if (%pendingTransactionRecord $= "")
    {
        error(getScopeName() SPC "- no such transaction:" SPC %giftTransactionID SPC getTrace());
        return "";
    }
    %pendingTransactionRecord.delete();
    %this.PendingTransactionsList.remove(%giftTransactionID);
    return ;
}
$gGiftAcceptModeStrings[0] = "accept";
$gGiftAcceptModeStrings[1] = "ask";
$gGiftAcceptModeStrings[2] = "decline";
function ClientCmdGiftingCurrency_Initiated(%sourcePlayerName, %giftTransactionID, %personalMessage, %currencyType, %currencyAmount)
{
    %sourcePlayer = Player::findPlayerInstance(%sourcePlayerName);
    if (!isObject(%sourcePlayer))
    {
        error(getScopeName() SPC "- could not find source player:" SPC %sourcePlayerName);
        geGiftingPanel.doAccept(%sourcePlayerName, %giftTransactionID, 0, "E-ENVSERVER-UNKNOWN");
        return ;
    }
    %acceptModeStrangers = $gGiftAcceptModeStrings[$UserPref::Player::GiftsPermissionStrangers];
    %acceptModeFriends = $gGiftAcceptModeStrings[$UserPref::Player::GiftsPermissionFriends];
    %acceptMode = %sourcePlayer.isFriend() ? %acceptModeFriends : %acceptModeStrangers;
    if (%acceptMode $= "accept")
    {
        geGiftingPanel.doAccept(%sourcePlayerName, %giftTransactionID, 1, "ACCEPTED-AUTO");
    }
    else
    {
        if (%acceptMode $= "decline")
        {
            geGiftingPanel.doAccept(%sourcePlayerName, %giftTransactionID, 0, "DECLINED-AUTO");
        }
        else
        {
            if (%acceptMode $= "ask")
            {
                if (geGiftingPanel.isVisible())
                {
                    geGiftingPanel.doAccept(%sourcePlayerName, %giftTransactionID, 0, "DECLINED-BUSY");
                }
                else
                {
                    geGiftingPanel.giftTransactionID = %giftTransactionID;
                    geGiftingPanel.personalMessage = TryFixBadWords(%personalMessage);
                    geGiftingPanel.currencyType = %currencyType;
                    geGiftingPanel.currencyAmount = %currencyAmount;
                    geGiftingPanel.GiftType = "currency";
                    geGiftingPanel.open(%sourcePlayerName, "acceptDecline");
                }
            }
        }
    }
    return ;
}
function geGiftingPanel::doAccept(%this, %sourcePlayerName, %giftTransactionID, %accepted, %messageCode)
{
    if (%this.currentScreen $= "items_acceptDecline")
    {
        commandToServer('GiftingItems_AcceptedOrDeclined', %sourcePlayerName, %giftTransactionID, %accepted, %messageCode);
    }
    else
    {
        commandToServer('GiftingCurrency_AcceptOrDecline', %sourcePlayerName, %giftTransactionID, %accepted, %messageCode);
    }
    return ;
}
function ClientCmdGiftingCurrency_AcceptedOrDeclinedOrInvalid(%giftTransactionID, %accepted, %messageCode)
{
    %pendingTransactionRecord = geGiftingPanel.getPendingTransaction(%giftTransactionID);
    if (!isObject(%pendingTransactionRecord))
    {
        error(getScopeName() SPC "- no such pending transaction:" SPC %giftTransactionID);
        return ;
    }
    if (isObject(%pendingTransactionRecord.dlg))
    {
        %pendingTransactionRecord.dlg.close();
    }
    if (!%accepted)
    {
        %otherPlayerName = %pendingTransactionRecord.targetPlayerName;
        %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
        if (!isObject(%otherPlayer))
        {
            error(getScopeName() SPC "- can\'t find other player:" SPC %otherPlayerName SPC %giftTransactionID);
            %messageCode = "E-TARGET-MISSING";
        }
        %text = strreplace($MsgCat::gifting[%messageCode], "[OTHERPLAYER]", "<linkcolor:ffddeeff><a:gamelink " @ munge(%otherPlayerName) @ ">" @ StripMLControlChars(%otherPlayerName) @ "</a>");
        %text = strreplace(%text, "[OTHERPLAYER_HE_SHE_IT]", getPronounHeSheIt(%otherPlayer));
        %text = strreplace(%text, "[OTHERPLAYER_HIM_HER_IT]", getPronounHimHerIt(%otherPlayer));
        MessageBoxOK("Woops..", %text, "");
        geGiftingPanel.deletePendingTransaction(%giftTransactionID);
        return ;
    }
    %request = sendRequest_GiftCurrency(%pendingTransactionRecord.targetPlayerName, %pendingTransactionRecord.currencyType, %pendingTransactionRecord.currencyAmount, 0, onDoneOrErrorCallback_GiftCurrency);
    %request.giftTransactionID = %giftTransactionID;
    %request.personalMessage = %pendingTransactionRecord.personalMessage;
    return ;
}
function onDoneOrErrorCallback_GiftCurrency(%request)
{
    %otherPlayerName = %request.getURLParam("payee");
    %currencyType = %request.getURLParam("currencyType");
    %currencyAmount = %request.getURLParam("amount");
    %dryRun = %request.getURLParam("dryRun", 1);
    %succeeded = %request.getResult("status") $= "success";
    if (%succeeded)
    {
        if (%dryRun)
        {
            geGiftingPanel.onDryRunSuccess(%request.dlg);
            return ;
        }
        else
        {
            getBalancesAndScores();
            commandToServer('GiftingCurrency_Notify', %otherPlayerName, %request.giftTransactionID, %request.personalMessage, %currencyType, %currencyAmount);
        }
        %msg = $MsgCat::gifting["ACCEPTED"];
    }
    else
    {
        if (isObject(%request.dlg))
        {
            %request.dlg.close();
        }
        %errorCode = %request.getResult("errorCode");
        if (%errorCode $= "")
        {
            %errorCode = "UNKNOWN";
        }
        %msg = $MsgCat::gifting["E-BACKEND-" @ %errorCode];
        if (%msg $= "")
        {
            %msg = $MsgCat::gifting["E-BACKEND-UNKNOWN"];
        }
        error(getScopeName() SPC "- failed with" SPC %errorCode);
    }
    %info = PlayerInfoMap.get(%otherPlayerName);
    if (!isObject(%info))
    {
        error(getScopeName() SPC "- didn\'t receive player info for" SPC %otherPlayerName);
        %levelText = "";
        %limitText = "a certain amount of";
    }
    else
    {
        %levelNum = respektScoreToLevel(%info.respekt);
        %levelText = "is level" SPC %levelNum @ ", so ";
        %limitText = getGiftingCapsForLevel(%levelNum, %currencyType, "recv");
    }
    %currencyText = %currencyType $= "VPOINTS" ? "<spush><color:159fe7>vPoints<spop> " : "<spush><color:13b93c>vBux<spop>";
    %msg = strreplace(%msg, "[OTHERPLAYER]", "<linkcolor:ffddeeff><a:gamelink " @ munge(%otherPlayerName) @ ">" @ StripMLControlChars(%otherPlayerName) @ "</a>");
    %msg = strreplace(%msg, "[GIFTTYPE]", %currencyText);
    %msg = strreplace(%msg, "[OTHERPLAYERLEVELTEXT]", %levelText);
    %msg = strreplace(%msg, "[OTHERPLAYERRECVLIMIT]", %limitText);
    if (!%succeeded)
    {
        MessageBoxOK("woops", %msg, "");
    }
    if (!%dryRun)
    {
        geGiftingPanel.deletePendingTransaction(%request.giftTransactionID);
    }
    return ;
}
function geGiftingPanel::onBack_Initiate(%this)
{
    error(getScopeName() SPC "- shouldn\'t be here." SPC getTrace());
    return ;
}
function geGiftingPanel::onBack_Confirmation(%this)
{
    %this.currentScreen = "initiate";
    %this.refresh();
    geGiftingEditAmt.makeFirstResponder(1);
    return ;
}
function geGiftingPanel::getAmountInBankOfCurrentCurrency(%this)
{
    if (!geGiftingCurrencyType_vPoints.getValue() && !geGiftingCurrencyType_vBux.getValue())
    {
        return 0;
    }
    %ret = geGiftingCurrencyType_vPoints.getValue() ? $Player::VPoints : $Player::VBux;
    return %ret;
}
function giftOperation(%line)
{
    %currencyAmount = getWord(%line, 0);
    %currencyType = getWord(%line, 1);
    if (formatInt("%d", %currencyAmount) == %currencyAmount)
    {
        if (getSubStr(%currencyType, 0, 2) $= "vB")
        {
            %currencyType = "vBux";
        }
        else
        {
            if (getSubStr(%currencyType, 0, 2) $= "vP")
            {
                %currencyType = "vPoints";
            }
            else
            {
                %currencyType = "";
            }
        }
        if (!(%currencyType $= ""))
        {
            %playerName = getWords(%line, 2, 100);
            geGiftingPanel.open(%playerName, "initiate");
            geGiftingCurrencyType_vPoints.setValue(%currencyType $= "vPoints");
            geGiftingCurrencyType_vBux.setValue(%currencyType $= "vBux");
            geGiftingEditAmt.setValue(%currencyAmount);
            geGiftingPanel.refresh();
            if (%currencyAmount > 0)
            {
                geGiftingEditMsg.makeFirstResponder(1);
            }
            else
            {
                geGiftingEditAmt.makeFirstResponder(1);
            }
            return ;
        }
    }
    geGiftingPanel.open(%line, "initiate");
    return ;
}
function parseGiftingSettings(%request)
{
    if (1 && $StandAlone)
    {
        addFakeGiftingSettings(%request);
    }
    %key = "VBuxTransferEnabled";
    if (%request.hasKey(%key))
    {
        $gGiftingEnabled_vBux = %request.getValueBool(%key);
    }
    else
    {
        error(getScopeName() SPC "- setting not found:" SPC %key);
    }
    %key = "VPointsTransferEnabled";
    if (%request.hasKey(%key))
    {
        $gGiftingEnabled_vPoints = %request.getValueBool(%key);
    }
    else
    {
        error(getScopeName() SPC "- setting not found:" SPC %key);
    }
    safeEnsureScriptObject("StringMap", "gGiftingCapTable");
    gGiftingCapTable.clear();
    %num = %request.getValue("giftingCapLevelCount");
    %n = 0;
    while (%n < %num)
    {
        %level = %request.getValue("giftingCap" @ %n @ ".level");
        %vpGive = %request.getValue("giftingCap" @ %n @ ".vpGive");
        %vpRecv = %request.getValue("giftingCap" @ %n @ ".vpRecv");
        %vbGive = %request.getValue("giftingCap" @ %n @ ".vbGive");
        %vbRecv = %request.getValue("giftingCap" @ %n @ ".vbRecv");
        gGiftingCapTable.put(%level, %vpGive SPC %vpRecv SPC %vbGive SPC %vbRecv);
        %n = %n + 1;
    }
}

function addFakeGiftingSettings(%request)
{
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
    return ;
}
function getGiftingCapsForLevel(%level, %currency, %giveOrRecv)
{
    %s = gGiftingCapTable.get(%level);
    if (%s $= "")
    {
        error(getScopeName() SPC "- unknown level:" SPC %level SPC getTrace());
        return 0;
    }
    if (%currency $= "vPoints")
    {
        %idx = 0;
    }
    else
    {
        if (%currency $= "vBux")
        {
            %idx = 2;
        }
        else
        {
            error(getScopeName() SPC "- unknown currency:" SPC %currency SPC getTrace());
            return 0;
        }
    }
    if (%giveOrRecv $= "recv")
    {
        %idx = %idx + 1;
    }
    %ret = getWord(%s, %idx);
    return %ret;
}
function clientCmdGiftingCurrency_NotifySource(%otherPlayerName, %personalMessage, %currencyType, %currencyAmount)
{
    %giftText = gifting_composeGiftDescriptionCurrency2(%currencyType, %currencyAmount);
    %encodedText = %giftText TAB %personalMessage;
    pChat::ProcessIncomingLine(%encodedText, 0, $player.getShapeName(), %otherPlayerName, 0, "gift", 0);
    schedule(3000, 0, "floatBalanceChange", %currencyType, %currencyAmount, Player::findPlayerInstance(%otherPlayerName));
    return ;
}
function clientCmdGiftingCurrency_NotifyTarget(%otherPlayerName, %personalMessage, %currencyType, %currencyAmount)
{
    %giftText = gifting_composeGiftDescriptionCurrency2(%currencyType, %currencyAmount);
    %encodedText = %giftText TAB %personalMessage;
    pChat::ProcessIncomingLine(%encodedText, 0, %otherPlayerName, $player.getShapeName(), 0, "gift", 0);
    return ;
}
