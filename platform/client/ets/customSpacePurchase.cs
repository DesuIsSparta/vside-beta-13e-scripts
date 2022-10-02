function CSSpacePurchase(%space)
{
    getCustomSpacePurchaseInfo(%space, "GotSpacePurchaseInfo");
    return ;
}
function GotSpacePurchaseInfo(%space)
{
    if (!isObject(%space))
    {
        error(getScopeName() SPC "- bad space." SPC getTrace());
        return ;
    }
    CSSpacePurchasePriceConfirmation(%space);
    return ;
}
function CSSpacePurchasePriceConfirmation(%space)
{
    %title = "Get a Room (Step 1 of 2)";
    %finalVPoints = mFloor(%space.floorplan.priceVPoints);
    %finalVBux = mFloor(%space.floorplan.priceVBux);
    %text = "<just:left>" NL $MsgCat::custSpace["PURCHASE_INTRO"];
    %price = CSSpacePurchasePriceFormatting(%space.floorplan.priceVPoints, %space.floorplan.priceVBux);
    %tradein = CSSpacePurchasePriceFormatting(%space.floorplan.tradeInValueVPoints, %space.floorplan.tradeInValueVBux);
    %final = CSSpacePurchasePriceFormatting(%finalVPoints, %finalVBux);
    %text = %text @ $MsgCat::custSpace["PURCHASE_TRADEININTRO"] @ "\n<tab:30>" NL "\t" @ $MsgCat::custSpace["PURCHASE_SPACEPRICE"] SPC %price NL " " NL "\t" @ $MsgCat::custSpace["PURCHASE_NOTINCLUDED"] NL "<spop>";
    if (((($Player::VPoints >= %finalVPoints) && (%finalVPoints >= 0)) && ($Player::VBux >= %finalVBux)) && (%finalVBux >= 0))
    {
        %text = %text NL $MsgCat::custSpace["PURCHASE_CHOICE"] @ "\n";
    }
    else
    {
        if (($Player::VPoints >= %finalVPoints) && (%finalVPoints >= 0))
        {
            %text = %text NL $MsgCat::custSpace["PURCHASE_VPOINTSONLY"] @ "\n";
        }
        else
        {
            if (($Player::VBux >= %finalVBux) && (%finalVBux >= 0))
            {
                %text = %text NL $MsgCat::custSpace["PURCHASE_VBUXONLY"] @ "\n";
            }
            else
            {
                %text = %text NL $MsgCat::custSpace["PURCHASE_NOTENOUGH"] @ "\n";
                MessageBoxOK(%title, %text, "");
                return ;
            }
        }
    }
    %buttons = "";
    %count = 0;
    if (($Player::VPoints >= %finalVPoints) && (%finalVPoints >= 0))
    {
        %buttons = %buttons TAB "Buy with " @ commaify(%finalVPoints) @ " vPoints";
        %callback[%count] = "CSSpacePurchaseDoConfirm(" @ %space @ ", false);";
        %count = %count + 1;
    }
    if (($Player::VBux >= %finalVBux) && (%finalVBux >= 0))
    {
        %buttons = %buttons TAB "Buy with " @ commaify(%finalVBux) @ " vBux";
        %callback[%count] = "CSSpacePurchaseDoConfirm(" @ %space @ ", true );";
        %count = %count + 1;
    }
    %buttons = %buttons TAB "Cancel";
    %callback[%count] = "CSSpacePurchaseCancel();";
    %count = %count + 1;
    %buttons = ltrim(%buttons);
    %dlg = MessageBoxCustom(%title, %text, %buttons);
    %index = 0;
    while (%index < %count)
    {
        %dlg.callback[%index] = %callback[%index];
        %index = %index + 1;
    }
}

function CSSpacePurchaseDoConfirm(%space, %useBux)
{
    %title = "Get a Room (Step 2 of 2)";
    %text = $MsgCat::custSpace["PURCHASE_CONFIRM"];
    %priceFinal = %useBux ? %space : %space;
    %currencyText = %useBux ? "vBux" : "vPoints";
    %text = strreplace(%text, "[PRICE]", %priceFinal SPC %currencyText);
    %cmd = "purchaseApartmentRequest( " @ %space @ ", " @ %useBux @ ", " @ %priceFinal @ ",  \"CSSpacePurchaseSuccess\", \"CSSpacePurchaseFailed\");";
    MessageBoxYesNo(%title, %text, %cmd, "CSSpacePurchaseCancel();");
    return ;
}
function CSSpacePurchaseDowngradeCheck(%space, %useBux, %priceFinal, %lossVPoints, %lossVBux)
{
    if ((%lossVPoints > 0) && (%lossVBux > 0))
    {
        %loss = CSSpacePurchasePriceFormatting(%lossVPoints, %lossVBux);
        %text = "<just:left>" NL $MsgCat::custSpace["TRADE_IN_DOWN_A"] SPC %loss SPC $MsgCat::custSpace["TRADE_IN_DOWN_B"] @ "\n";
        %buttons = "Yes - Trade in" TAB "No - Cancel";
        %dlg = MessageBoxCustom("Warning", %text, %buttons);
        %dlg.callback[0] = "purchaseApartmentRequest( " @ %space @ ", " @ %useBux @ ", " @ %priceFinal @ ",  \"CSSpacePurchaseSuccess\", \"CSSpacePurchaseFailed\");";
        %dlg.callback[1] = "CSSpacePurchaseCancel();";
    }
    else
    {
        purchaseApartmentRequest(%space, %useBux, %priceFinal, "CSSpacePurchaseSuccess", "CSSpacePurchaseFailed");
    }
    return ;
}
function CSSpacePurchaseCancel()
{
    %title = "Purchase Cancelled";
    %text = "<just:center>" NL $MsgCat::custSpace["PURCHASE_ABORTED"] @ "\n";
    MessageBoxOK(%title, %text, "");
    return ;
}
function CSSpacePurchaseSuccess(%unused, %unused, %vurl)
{
    $Player::myPlaceVURL = %vurl;
    %title = "Get a Room (Complete!)";
    gUserPropMgrClient.setProperty($Player::Name, "ShowOwnerTip", 1);
    %text = "<just:left>" NL $MsgCat::custSpace["PURCHASE_DONE"] @ "\n";
    %buttons = "Go there now" TAB "Close";
    %dlg = MessageBoxCustom(%title, %text, %buttons);
    %dlg.callback[0] = "vurlOperation( \"" @ %vurl @ "\");";
    getOwnerSpacesInfo($Player::Name, "");
    return ;
}
function CSSpacePurchaseFailed(%errorCode)
{
    %errorCode = strupr(%errorCode);
    if ($MsgCat::custSpace["ERROR_" @ %errorCode] $= "")
    {
        %errorCode = "GENERIC";
    }
    %text = "<just:left>" NL "<spush><b>There was a problem!<spop>\n" NL "We could not purchase the apartment for you because " @ $MsgCat::custSpace["ERROR_" @ %errorCode] @ "\n";
    MessageBoxOK("Purchase a Space", %text, "");
    return ;
}
function CSSpacePurchasePriceFormatting(%vpoints, %vbux)
{
    %result = "";
    if (%vpoints >= 0)
    {
        %result = "<bitmap:platform/client/ui/vpoints_9> " @ commaify(%vpoints);
    }
    if ((%vpoints >= 0) && (%vbux >= 0))
    {
        %result = %result @ "  or  ";
    }
    if (%vbux >= 0)
    {
        %result = %result @ "<bitmap:platform/client/ui/vbux_9> " @ commaify(%vbux);
    }
    return %result;
}
