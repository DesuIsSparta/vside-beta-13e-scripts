function drinks_confirmInitiateGift(%otherPlayerName)
{
    %sku = $player.getActiveDrinkSku();
    if (%sku $= "")
    {
        error(getScopeName() SPC "- no drink!" SPC getTrace());
        return ;
    }
    %si = SkuManager.findBySku(%sku);
    %msg = $MsgCat::giftingItems["DLG-BODY-GIVE-CONFIRM"];
    %msg = strreplace(%msg, "[ITEMNAME]", %si.descShrt);
    %msg = strreplace(%msg, "[OTHERPLAYER]", %otherPlayerName);
    %dlg = MessageBoxYesNo($MsgCat::giftingItems["DLG-TITLE-GIVE-CONFIRM"], %msg, "giftingItems_onInitiate($gThisDialog);", "");
    %dlg.otherPlayerName = %otherPlayerName;
    %dlg.giftSkus = %sku;
    %dlg.making = 0;
    return ;
}
function drinks_confirmInitiateMake(%otherPlayerName, %sku)
{
    %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
    if (!isObject(%otherPlayer))
    {
        error(getScopeName() SPC "- can\'t find other player" SPC %otherPlayerName SPC getTrace());
        return ;
    }
    %si = SkuManager.findBySku(%sku);
    if (%otherPlayerName $= $Player::Name)
    {
        %msg = $MsgCat::giftingItems["DLG-BODY-MAKE-SELF-CONFIRM"];
    }
    else
    {
        %msg = $MsgCat::giftingItems["DLG-BODY-MAKE-CONFIRM"];
    }
    %msg = strreplace(%msg, "[ITEMNAME]", %si.descShrt);
    %msg = strreplace(%msg, "[OTHERPLAYER]", %otherPlayerName);
    %dlg = MessageBoxYesNo($MsgCat::giftingItems["DLG-TITLE-MAKE-CONFIRM"], %msg, "giftingItems_onInitiate($gThisDialog);", "");
    %dlg.otherPlayerName = %otherPlayerName;
    %dlg.giftSkus = %sku;
    %dlg.making = 1;
    return ;
}
function giftingItems_onInitiate(%dlg)
{
    %transactionID = MD5(getRandom(0, 1000000));
    commandToServer('GiftingItems_Initiated', %dlg.otherPlayerName, %transactionID, %dlg.giftSkus, %dlg.making);
    if (%dlg.otherPlayerName $= $Player::Name)
    {
        %otherDlg = "";
    }
    else
    {
        %otherDlg = MessageBoxOK("The Gift of Libation", "<br>Checking with" SPC %dlg.otherPlayerName @ "..<br>", "");
    }
    giftingItems_registerPendingTransactionGiver(%transactionID, %dlg.otherPlayerName, %dlg.giftSkus, %otherDlg, %dlg.making);
    return ;
}
function ClientCmdGiftingItems_Initiated(%otherPlayerName, %skus, %transactionID, %making)
{
    %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
    if (!isObject(%otherPlayer))
    {
        error(getScopeName() SPC "- could not find other player:" SPC %otherPlayerName);
        GiftingItemsClient_DoAcceptOrDecline(%otherPlayerName, %transactionID, 0, "E-ENVSERVER-UNKNOWN");
        return ;
    }
    error("// oxe 20090219 - todo - decide if this is good or if we want a new one");
    %acceptModeStrangers = $gGiftAcceptModeStrings[$UserPref::Player::GiftsPermissionStrangers];
    %acceptModeFriends = $gGiftAcceptModeStrings[$UserPref::Player::GiftsPermissionFriends];
    %acceptMode = %otherPlayer.isFriend() ? %acceptModeFriends : %acceptModeStrangers;
    if (%acceptMode $= "accept")
    {
        giftingItems_registerPendingTransactionRecipient(%transactionID, %otherPlayerName, %skus, 1, %making);
        GiftingItemsClient_DoAcceptOrDecline(%otherPlayerName, %transactionID, 1, "ACCEPTED-AUTO");
    }
    else
    {
        if (%acceptMode $= "decline")
        {
            GiftingItemsClient_DoAcceptOrDecline(%otherPlayerName, %transactionID, 0, "DECLINED-AUTO");
        }
        else
        {
            if (%acceptMode $= "ask")
            {
                if (geGiftingPanel.isVisible())
                {
                    GiftingItemsClient_DoAcceptOrDecline(%otherPlayerName, %transactionID, 0, "DECLINED-BUSY");
                }
                else
                {
                    giftingItems_registerPendingTransactionRecipient(%transactionID, %otherPlayerName, %skus, 0, %making);
                    geGiftingPanel.giftTransactionID = %transactionID;
                    geGiftingPanel.personalMessage = "";
                    geGiftingPanel.skus = %skus;
                    geGiftingPanel.GiftType = "items";
                    geGiftingPanel.making = %making;
                    geGiftingPanel.open(%otherPlayerName, "items_acceptDecline");
                }
            }
        }
    }
    return ;
}
function GiftingItemsClient_DoAcceptOrDecline(%otherPlayerName, %transactionID, %accepted, %messageCode)
{
    commandToServer('GiftingItems_AcceptedOrDeclined', %otherPlayerName, %transactionID, %accepted, %messageCode);
    return ;
}
function ClientCmdGiftingItems_AcceptedOrDeclinedOrInvalid(%transactionID, %accepted, %messageCode)
{
    %pendingTransactionRecord = giftingItems_getPendingTransactionClient(%transactionID);
    if (!isObject(%pendingTransactionRecord))
    {
        error(getScopeName() SPC "- no such pending transaction:" SPC %transactionID);
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
            error(getScopeName() SPC "- can\'t find other player:" SPC %otherPlayerName SPC %transactionID);
            %messageCode = "E-TARGET-MISSING";
        }
        %text = strreplace($MsgCat::gifting[%messageCode], "[OTHERPLAYER]", "<linkcolor:ffddeeff><a:gamelink " @ munge(%otherPlayerName) @ ">" @ StripMLControlChars(%otherPlayerName) @ "</a>");
        %text = strreplace(%text, "[OTHERPLAYER_HIM_HER_IT]", getPronounHimHerIt(%otherPlayer));
        %text = strreplace(%text, "[OTHERPLAYER_HE_SHE_IT]", getPronounHeSheIt(%otherPlayer));
        MessageBoxOK("Woops..", %text, "");
    }
    giftingItems_deletePendingTransactionClient(%transactionID);
    return ;
}
function ClientCmdGiftingItems_Completed(%transactionID, %succeeded)
{
    %transactionRecord = giftingItems_getPendingTransactionClient(%transactionID);
    if (!isObject(%transactionRecord))
    {
        error(getScopeName() SPC "- no such pending transaction:" SPC %transactionID);
        return ;
    }
    %amSource = %transactionRecord.sourcePlayerName $= $Player::Name;
    %otherPlayerName = %amSource ? %transactionRecord : %transactionRecord;
    %amAlphaAndOmega = %otherPlayerName $= $Player::Name;
    %skus = %transactionRecord.skus;
    if (%succeeded)
    {
        if (%amAlphaAndOmega)
        {
            schedule(3000, 0, "updateInventorySkus", %skus, "", 1, 1, %otherPlayerName);
        }
        else
        {
            if (%amSource)
            {
                updateInventorySkus("", %skus, 0, 1, %otherPlayerName);
            }
            else
            {
                %autoAccepted = %transactionRecord.autoAccepted;
                updateInventorySkus(%skus, "", %autoAccepted, 1, %otherPlayerName);
            }
        }
    }
    else
    {
        %msg = $MsgCat::giftingItems["E-UNKNOWN"];
        MessageBoxOK("Woops...", %msg);
    }
    if (%amSource)
    {
        %dlg = %transactionRecord.dlg;
        if (isObject(%dlg))
        {
            %dlg.close();
        }
    }
    giftingItems_deletePendingTransactionClient(%transactionID);
    return ;
}
