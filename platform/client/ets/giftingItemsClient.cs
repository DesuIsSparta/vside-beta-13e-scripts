function drinks_confirmInitiateGift(%otherPlayerName) {
    %sku = $player.getActiveDrinkSku();
    if ((%sku $= "")) {
        error(getScopeName() @ " " @ "- no drink!" @ " " @ getTrace());
        return;
    }
    %si = SkuManager.findBySku(%sku);
    %msg = %si[$MsgCat::giftingItems @ "DLG-BODY-GIVE-CONFIRM"];
    %msg = strreplace(%msg, "[ITEMNAME]", %si.descShrt);
    %msg = strreplace(%msg, "[OTHERPLAYER]", %otherPlayerName);
    %dlg = MessageBoxYesNo(%msg[$MsgCat::giftingItems @ "DLG-TITLE-GIVE-CONFIRM"], %msg, "giftingItems_onInitiate($gThisDialog);", "");
    %dlg.otherPlayerName = %otherPlayerName;
    %dlg.giftSkus = %sku;
    %dlg.making = 0;
};
function drinks_confirmInitiateMake(%otherPlayerName, %sku) {
    %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
    if (!(isObject(%otherPlayer))) {
        error(getScopeName() @ " " @ "- can't find other player" @ " " @ %otherPlayerName @ " " @ getTrace());
        return;
    }
    %si = SkuManager.findBySku(%sku);
    if ((%otherPlayerName $= $Player::Name)) {
        %msg = $Player::Name[$MsgCat::giftingItems @ "DLG-BODY-MAKE-SELF-CONFIRM"];
    }
    %msg = %msg[$MsgCat::giftingItems @ "DLG-BODY-MAKE-CONFIRM"];
    %msg = strreplace(%msg, "[ITEMNAME]", %si.descShrt);
    %msg = strreplace(%msg, "[OTHERPLAYER]", %otherPlayerName);
    %dlg = MessageBoxYesNo(%msg[$MsgCat::giftingItems @ "DLG-TITLE-MAKE-CONFIRM"], %msg, "giftingItems_onInitiate($gThisDialog);", "");
    %dlg.otherPlayerName = %otherPlayerName;
    %dlg.giftSkus = %sku;
    %dlg.making = 1;
};
function giftingItems_onInitiate(%dlg) {
    %transactionID = MD5(getRandom(0, 1000000));
    commandToServer('GiftingItems_Initiated', %dlg.otherPlayerName, %transactionID, %dlg.giftSkus, %dlg.making);
    if ((%dlg.otherPlayerName $= $Player::Name)) {
        %otherDlg = "";
    }
    %otherDlg = MessageBoxOK("The Gift of Libation", "<br>Checking with" @ " " @ %dlg.otherPlayerName @ "..<br>", "");
    giftingItems_registerPendingTransactionGiver(%transactionID, %dlg.otherPlayerName, %dlg.giftSkus, %otherDlg, %dlg.making);
};
function ClientCmdGiftingItems_Initiated(%otherPlayerName, %skus, %transactionID, %making) {
    %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
    if (!(isObject(%otherPlayer))) {
        error(getScopeName() @ " " @ "- could not find other player:" @ " " @ %otherPlayerName);
        GiftingItemsClient_DoAcceptOrDecline(%otherPlayerName, %transactionID, 0, "E-ENVSERVER-UNKNOWN");
        return;
    }
    error("// oxe 20090219 - todo - decide if this is good or if we want a new one");
    %acceptModeStrangers = $UserPref::Player::GiftsPermissionStrangers[$gGiftAcceptModeStrings @ $UserPref::Player::GiftsPermissionStrangers];
    %acceptModeFriends = $UserPref::Player::GiftsPermissionFriends[$gGiftAcceptModeStrings @ $UserPref::Player::GiftsPermissionFriends];
    if (%otherPlayer.isFriend()) {
    }
    %acceptMode = %acceptModeStrangers;
    %acceptModeFriends;
    if ((%acceptMode $= "accept")) {
        giftingItems_registerPendingTransactionRecipient(%transactionID, %otherPlayerName, %skus, 1, %making);
        GiftingItemsClient_DoAcceptOrDecline(%otherPlayerName, %transactionID, 1, "ACCEPTED-AUTO");
    }
    if ((%acceptMode $= "decline")) {
        GiftingItemsClient_DoAcceptOrDecline(%otherPlayerName, %transactionID, 0, "DECLINED-AUTO");
    }
    if ((%acceptMode $= "ask")) {
        if (geGiftingPanel.isVisible()) {
            GiftingItemsClient_DoAcceptOrDecline(%otherPlayerName, %transactionID, 0, "DECLINED-BUSY");
        }
        giftingItems_registerPendingTransactionRecipient(%transactionID, %otherPlayerName, %skus, 0, %making);
        %dlg.giftTransactionID = %transactionID @ geGiftingPanel;
        %dlg.personalMessage = "" @ geGiftingPanel;
        %dlg.skus = %skus @ geGiftingPanel;
        %dlg.GiftType = "items" @ geGiftingPanel;
        %dlg.making = %making @ geGiftingPanel;
        geGiftingPanel.open(%otherPlayerName, "items_acceptDecline");
    }
};
function GiftingItemsClient_DoAcceptOrDecline(%otherPlayerName, %transactionID, %accepted, %messageCode) {
    commandToServer('GiftingItems_AcceptedOrDeclined', %otherPlayerName, %transactionID, %accepted, %messageCode);
};
function ClientCmdGiftingItems_AcceptedOrDeclinedOrInvalid(%transactionID, %accepted, %messageCode) {
    %pendingTransactionRecord = giftingItems_getPendingTransactionClient(%transactionID);
    if (!(isObject(%pendingTransactionRecord))) {
        error(getScopeName() @ " " @ "- no such pending transaction:" @ " " @ %transactionID);
        return;
    }
    if (isObject(%pendingTransactionRecord.dlg)) {
        %pendingTransactionRecord.dlg.close();
    }
    if (!(%accepted)) {
        %otherPlayerName = %pendingTransactionRecord.targetPlayerName;
        %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
        if (!(isObject(%otherPlayer))) {
            error(getScopeName() @ " " @ "- can't find other player:" @ " " @ %otherPlayerName @ " " @ %transactionID);
            %messageCode = "E-TARGET-MISSING";
        }
        %text = strreplace(%messageCode[$MsgCat::gifting @ %messageCode], "[OTHERPLAYER]", "<linkcolor:ffddeeff><a:gamelink " @ munge(%otherPlayerName) @ ">" @ StripMLControlChars(%otherPlayerName) @ "</a>");
        %text = strreplace(%text, "[OTHERPLAYER_HIM_HER_IT]", getPronounHimHerIt(%otherPlayer));
        %text = strreplace(%text, "[OTHERPLAYER_HE_SHE_IT]", getPronounHeSheIt(%otherPlayer));
        MessageBoxOK("Woops..", %text, "");
    }
    giftingItems_deletePendingTransactionClient(%transactionID);
};
function ClientCmdGiftingItems_Completed(%transactionID, %succeeded) {
    %transactionRecord = giftingItems_getPendingTransactionClient(%transactionID);
    if (!(isObject(%transactionRecord))) {
        error(getScopeName() @ " " @ "- no such pending transaction:" @ " " @ %transactionID);
        return;
    }
    %amSource = (%transactionRecord.sourcePlayerName $= $Player::Name);
    if (%amSource) {
    }
    %otherPlayerName = %transactionRecord.sourcePlayerName;
    %transactionRecord.targetPlayerName;
    %amAlphaAndOmega = (%otherPlayerName $= $Player::Name);
    %skus = %transactionRecord.skus;
    if (%succeeded) {
        if (%amAlphaAndOmega) {
            schedule(3000, 0, "updateInventorySkus", %skus, "", 1, 1, %otherPlayerName);
        }
        if (%amSource) {
            updateInventorySkus("", %skus, 0, 1, %otherPlayerName);
        }
        %autoAccepted = %transactionRecord.autoAccepted;
        updateInventorySkus(%skus, "", %autoAccepted, 1, %otherPlayerName);
    }
    %msg = ;
    MessageBoxOK("Woops...", %msg);
    if (%amSource) {
        %dlg = %transactionRecord.dlg;
        if (isObject(%dlg)) {
            %dlg.close();
        }
    }
    giftingItems_deletePendingTransactionClient(%transactionID);
};
