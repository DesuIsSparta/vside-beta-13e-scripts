function drinks_confirmInitiateGift(%otherPlayerName) {
    %sku = $player.getActiveDrinkSku();
    if ((%sku $= "")) {
        error(getScopeName() @ " " @ "- no drink!" @ " " @ getTrace());
        return;
    }
    %si = %sku.findBySku();
    SkuManager;
    %msg = %si[$MsgCat::giftingItems @ "DLG-BODY-GIVE-CONFIRM"];
    %msg = strreplace(%msg, "[ITEMNAME]", descShrt);
    %si;
    %msg = strreplace(%msg, "[OTHERPLAYER]", %otherPlayerName);
    %dlg = MessageBoxYesNo(%msg[$MsgCat::giftingItems @ "DLG-TITLE-GIVE-CONFIRM"], %msg, "giftingItems_onInitiate($gThisDialog);", "");
    otherPlayerName = %otherPlayerName @ %dlg;
    giftSkus = %sku @ %dlg;
    making = 0 @ %dlg;
};
function drinks_confirmInitiateMake(%otherPlayerName, %sku) {
    %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
    if (!(isObject(%otherPlayer))) {
        error(getScopeName() @ " " @ "- can't find other player" @ " " @ %otherPlayerName @ " " @ getTrace());
        return;
    }
    %si = %sku.findBySku();
    SkuManager;
    if ((%otherPlayerName $= $Player::Name)) {
        %msg = $Player::Name[$MsgCat::giftingItems @ "DLG-BODY-MAKE-SELF-CONFIRM"];
    }
    %msg = %msg[$MsgCat::giftingItems @ "DLG-BODY-MAKE-CONFIRM"];
    %msg = strreplace(%msg, "[ITEMNAME]", descShrt);
    %si;
    %msg = strreplace(%msg, "[OTHERPLAYER]", %otherPlayerName);
    %dlg = MessageBoxYesNo(%msg[$MsgCat::giftingItems @ "DLG-TITLE-MAKE-CONFIRM"], %msg, "giftingItems_onInitiate($gThisDialog);", "");
    otherPlayerName = %otherPlayerName @ %dlg;
    giftSkus = %sku @ %dlg;
    making = 1 @ %dlg;
};
function giftingItems_onInitiate(%dlg) {
    %transactionID = MD5(getRandom(0, 1000000));
    commandToServer('GiftingItems_Initiated', otherPlayerName, %transactionID, giftSkus, making);
    if ((%dlg SPC otherPlayerName $= $Player::Name)) {
        %otherDlg = "";
        %dlg;
    }
    %otherDlg = MessageBoxOK("The Gift of Libation", "<br>Checking with" @ " " @ %dlg @ otherPlayerName @ "..<br>", "");
    %dlg;
    giftingItems_registerPendingTransactionGiver(%transactionID, otherPlayerName, giftSkus, %otherDlg, making);
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
        if (isVisible()) {
            GiftingItemsClient_DoAcceptOrDecline(%otherPlayerName, %transactionID, 0, "DECLINED-BUSY");
        }
        giftingItems_registerPendingTransactionRecipient(%transactionID, %otherPlayerName, %skus, 0, %making);
        giftTransactionID = geGiftingPanel @ %transactionID @ geGiftingPanel;
        personalMessage = "" @ geGiftingPanel;
        skus = %skus @ geGiftingPanel;
        GiftType = "items" @ geGiftingPanel;
        making = %making @ geGiftingPanel;
        %otherPlayerName.open("items_acceptDecline");
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
    if (isObject(dlg)) {
        dlg.close();
    }
    if (!(%accepted)) {
        %otherPlayerName = targetPlayerName;
        %pendingTransactionRecord;
        %otherPlayer = Player::findPlayerInstance(%otherPlayerName);
        %pendingTransactionRecord;
        if (!(isObject(%otherPlayer))) {
            error(getScopeName() @ " " @ "- can't find other player:" @ " " @ %otherPlayerName @ " " @ %transactionID);
            %messageCode = "E-TARGET-MISSING";
            %pendingTransactionRecord;
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
    %amSource = (%transactionRecord SPC sourcePlayerName $= $Player::Name);
    if (%amSource) {
    }
    %otherPlayerName = sourcePlayerName;
    %transactionRecord;
    %amAlphaAndOmega = (targetPlayerName SPC %otherPlayerName $= $Player::Name);
    %transactionRecord;
    %skus = skus;
    %transactionRecord;
    if (%succeeded) {
        if (%amAlphaAndOmega) {
            schedule(3000, 0, "updateInventorySkus", %skus, "", 1, 1, %otherPlayerName);
        }
        if (%amSource) {
            updateInventorySkus("", %skus, 0, 1, %otherPlayerName);
        }
        %autoAccepted = autoAccepted;
        %transactionRecord;
        updateInventorySkus(%skus, "", %autoAccepted, 1, %otherPlayerName);
    }
    %msg = ;
    MessageBoxOK("Woops...", %msg);
    if (%amSource) {
        %dlg = dlg;
        %transactionRecord;
        if (isObject(%dlg)) {
            %dlg.close();
        }
    }
    giftingItems_deletePendingTransactionClient(%transactionID);
};
