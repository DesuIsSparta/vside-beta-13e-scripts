function CSSpacePurchase(%space) {
    getCustomSpacePurchaseInfo(%space, "GotSpacePurchaseInfo");
};
function GotSpacePurchaseInfo(%space) {
    if (!(isObject(%space))) {
        error(getScopeName() @ " " @ "- bad space." @ " " @ getTrace());
        return;
    }
    CSSpacePurchasePriceConfirmation(%space);
};
function CSSpacePurchasePriceConfirmation(%space) {
    %title = "Get a Room (Step 1 of 2)";
    %finalVPoints = mFloor(%space.floorplan.priceVPoints);
    %finalVBux = mFloor(%space.floorplan.priceVBux);
    %text = "<just:left>" @ "\n" @ %finalVBux[$MsgCat::custSpace @ "PURCHASE_INTRO"];
    %price = CSSpacePurchasePriceFormatting(%space.floorplan.priceVPoints, %space.floorplan.priceVBux);
    %tradein = CSSpacePurchasePriceFormatting(%space.floorplan.tradeInValueVPoints, %space.floorplan.tradeInValueVBux);
    %final = CSSpacePurchasePriceFormatting(%finalVPoints, %finalVBux);
    %text = %text @ %text[$MsgCat::custSpace @ "PURCHASE_TRADEININTRO"] @ "\n<tab:30>" @ "\n" @ "\t" @ %text[$MsgCat::custSpace @ "PURCHASE_TRADEININTRO"][$MsgCat::custSpace @ "PURCHASE_SPACEPRICE"] @ " " @ %price @ "\n" @ " " @ "\n" @ "\t" @ %price[$MsgCat::custSpace @ "PURCHASE_NOTINCLUDED"] @ "\n" @ "<spop>";
    if ((%finalVPoints >= $Player::VPoints)) {
    }
    if ((0.0 >= %finalVPoints)) {
        if ((%finalVBux >= $Player::VBux)) {
        }
    }
    if ((0.0 >= %finalVBux)) {
        %text = %text @ "\n" @ %text[$MsgCat::custSpace @ "PURCHASE_CHOICE"] @ "\n";
    }
    if ((%finalVPoints >= $Player::VPoints)) {
    }
    if ((0.0 >= %finalVPoints)) {
        %text = %text @ "\n" @ %text[$MsgCat::custSpace @ "PURCHASE_VPOINTSONLY"] @ "\n";
    }
    if ((%finalVBux >= $Player::VBux)) {
    }
    if ((0.0 >= %finalVBux)) {
        %text = %text @ "\n" @ %text[$MsgCat::custSpace @ "PURCHASE_VBUXONLY"] @ "\n";
    }
    %text = %text @ "\n" @ %text[$MsgCat::custSpace @ "PURCHASE_NOTENOUGH"] @ "\n";
    MessageBoxOK(%title, %text, "");
    return;
    %buttons = "";
    %count = 0;
    if ((%finalVPoints >= $Player::VPoints)) {
    }
    if ((0.0 >= %finalVPoints)) {
        %buttons = %buttons @ "\t" @ "Buy with " @ commaify(%finalVPoints) @ " vPoints";
        %count[%callback @ %count] = "CSSpacePurchaseDoConfirm(" @ %space @ ", false);";
        %count = (1.0 + %count);
    }
    if ((%finalVBux >= $Player::VBux)) {
    }
    if ((0.0 >= %finalVBux)) {
        %buttons = %buttons @ "\t" @ "Buy with " @ commaify(%finalVBux) @ " vBux";
        %count[%callback @ %count] = "CSSpacePurchaseDoConfirm(" @ %space @ ", true );";
        %count = (1.0 + %count);
    }
    %buttons = %buttons @ "\t" @ "Cancel";
    %count[%callback @ %count] = "CSSpacePurchaseCancel();";
    %count = (1.0 + %count);
    %buttons = ltrim(%buttons);
    %dlg = MessageBoxCustom(%title, %text, %buttons);
    %index = 0;
    if ((%count < %index)) {
        %dlg.callback = %index[%callback @ %index] @ %index;
        %index = (1.0 + %index);
    }
};
function CSSpacePurchaseDoConfirm(%space, %useBux) {
    %title = "Get a Room (Step 2 of 2)";
    %text = %title[$MsgCat::custSpace @ "PURCHASE_CONFIRM"];
    if (%useBux) {
    }
    %priceFinal = %space.floorplan.priceVPoints;
    %space.floorplan.priceVBux;
    %currencyText = %useBux ? "vBux" : "vPoints";
    %text = strreplace(%text, "[PRICE]", %priceFinal @ " " @ %currencyText);
    %cmd = "purchaseApartmentRequest( " @ %space @ ", " @ %useBux @ ", " @ %priceFinal @ ",  \"CSSpacePurchaseSuccess\", \"CSSpacePurchaseFailed\");";
    MessageBoxYesNo(%title, %text, %cmd, "CSSpacePurchaseCancel();");
};
function CSSpacePurchaseDowngradeCheck(%space, %useBux, %priceFinal, %lossVPoints, %lossVBux) {
    if ((0.0 > %lossVPoints)) {
    }
    if ((0.0 > %lossVBux)) {
        %loss = CSSpacePurchasePriceFormatting(%lossVPoints, %lossVBux);
        %text = "<just:left>" @ "\n" @ %loss[$MsgCat::custSpace @ "TRADE_IN_DOWN_A"] @ " " @ %loss @ " " @ %loss[$MsgCat::custSpace @ "TRADE_IN_DOWN_B"] @ "\n";
        %buttons = "Yes - Trade in" @ "\t" @ "No - Cancel";
        %dlg = MessageBoxCustom("Warning", %text, %buttons);
        %dlg.callback = "purchaseApartmentRequest( " @ %space @ ", " @ %useBux @ ", " @ %priceFinal @ ",  \"CSSpacePurchaseSuccess\", \"CSSpacePurchaseFailed\");" @ 0;
        %dlg.callback = "CSSpacePurchaseCancel();" @ 1;
    }
    purchaseApartmentRequest(%space, %useBux, %priceFinal, "CSSpacePurchaseSuccess", "CSSpacePurchaseFailed");
};
function CSSpacePurchaseCancel() {
    %title = "Purchase Cancelled";
    %text = "<just:center>" @ "\n" @ %title[$MsgCat::custSpace @ "PURCHASE_ABORTED"] @ "\n";
    MessageBoxOK(%title, %text, "");
};
function CSSpacePurchaseSuccess(%unused, %unused, %vurl) {
    $Player::myPlaceVURL = %vurl;
    %title = "Get a Room (Complete!)";
    $Player::Name.setProperty("ShowOwnerTip", 1);
    %text = gUserPropMgrClient @ "<just:left>" @ "\n" @ "\n";
    %buttons = "Go there now" @ "\t" @ "Close";
    %dlg = MessageBoxCustom(%title, %text, %buttons);
    %dlg.callback = "vurlOperation( \"" @ %vurl @ "\");" @ 0;
    getOwnerSpacesInfo($Player::Name, "");
};
function CSSpacePurchaseFailed(%errorCode) {
    %errorCode = strupr(%errorCode);
    if ((%errorCode[$MsgCat::custSpace @ "ERROR_" @ %errorCode] $= "")) {
        %errorCode = "GENERIC";
    }
    %text = "<just:left>" @ "\n" @ "<spush><b>There was a problem!<spop>\n" @ "\n" @ "We could not purchase the apartment for you because " @ %errorCode[$MsgCat::custSpace @ "ERROR_" @ %errorCode] @ "\n";
    MessageBoxOK("Purchase a Space", %text, "");
};
function CSSpacePurchasePriceFormatting(%vpoints, %vbux) {
    %result = "";
    if ((0.0 >= %vpoints)) {
        %result = "<bitmap:platform/client/ui/vpoints_9> " @ commaify(%vpoints);
    }
    if ((0.0 >= %vpoints)) {
    }
    if ((0.0 >= %vbux)) {
        %result = %result @ "  or  ";
    }
    if ((0.0 >= %vbux)) {
        %result = %result @ "<bitmap:platform/client/ui/vbux_9> " @ commaify(%vbux);
    }
    return %result;
};
