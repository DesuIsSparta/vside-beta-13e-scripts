function CSSpacePurchase(%space)
{
    getCustomSpacePurchaseInfo(%space, "GotSpacePurchaseInfo");
}
function GotSpacePurchaseInfo(%space)
{
    if (!isObject(%space))
    {
        error(getScopeName() @ " " @ "- bad space." @ " " @ getTrace());
        return;
    }
    CSSpacePurchasePriceConfirmation(%space);
}
function CSSpacePurchasePriceConfirmation(%space)
{
    %title = "Get a Room (Step 1 of 2)";
    %finalVPoints = mFloor(%space.floorplan.priceVPoints);
    %finalVBux = mFloor(%space.floorplan.priceVBux);
    %text = "<just:left>" @ "\n" @ $MsgCat::custSpace["PURCHASE_INTRO"];
    %price = CSSpacePurchasePriceFormatting(%space.floorplan.priceVPoints, %space.floorplan.priceVBux);
    %tradein = CSSpacePurchasePriceFormatting(%space.floorplan.tradeInValueVPoints, %space.floorplan.tradeInValueVBux);
    %final = CSSpacePurchasePriceFormatting(%finalVPoints, %finalVBux);
    %text = %text @ %text[$MsgCat::custSpace @ "PURCHASE_TRADEININTRO"] @ "\n<tab:30>" @ "\n" @ "\t" @ %text[$MsgCat::custSpace @ "PURCHASE_TRADEININTRO"][$MsgCat::custSpace @ "PURCHASE_SPACEPRICE"] @ " " @ %price @ "\n" @ " " @ "\n" @ "\t" @ %price[$MsgCat::custSpace @ "PURCHASE_NOTINCLUDED"] @ "\n" @ "<spop>";
    if (($Player::VBux >= %finalVBux) && ($Player::VPoints >= %finalVPoints) && (%finalVPoints >= 0) && (%finalVBux >= 0))
    {
        %text = %text @ "\n" @ %text[$MsgCat::custSpace @ "PURCHASE_CHOICE"] @ "\n";
    }
    else
    {
        if (($Player::VPoints >= %finalVPoints) && (%finalVPoints >= 0))
        {
            %text = %text @ "\n" @ %text[$MsgCat::custSpace @ "PURCHASE_VPOINTSONLY"] @ "\n";
        }
        if (($Player::VBux >= %finalVBux) && (%finalVBux >= 0))
        {
            %text = %text @ "\n" @ %text[$MsgCat::custSpace @ "PURCHASE_VBUXONLY"] @ "\n";
        }
        %text = %text @ "\n" @ %text[$MsgCat::custSpace @ "PURCHASE_NOTENOUGH"] @ "\n";
        MessageBoxOK(%title, %text, "");
        return;
    }
    %buttons = "";
    %count = 0;
    if (($Player::VPoints >= %finalVPoints) && (%finalVPoints >= 0))
    {
        %buttons = %buttons @ "\t" @ "Buy with " @ commaify(%finalVPoints) @ " vPoints";
        %callback[%count] = "CSSpacePurchaseDoConfirm(" @ %space @ ", false);";
        %count = %count + 1;
    }
    if (($Player::VBux >= %finalVBux) && (%finalVBux >= 0))
    {
        %buttons = %buttons @ "\t" @ "Buy with " @ commaify(%finalVBux) @ " vBux";
        %callback[%count] = "CSSpacePurchaseDoConfirm(" @ %space @ ", true );";
        %count = %count + 1;
    }
    %buttons = %buttons @ "\t" @ "Cancel";
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
    if (%useBux)
    {
    }
    else
    {
    }
    %priceFinal = %space.floorplan.priceVPoints;
    %space.floorplan.priceVBux;
    %currencyText = %useBux ? "vBux" : "vPoints";
    %text = strreplace(%text, "[PRICE]", %priceFinal @ " " @ %currencyText);
    %cmd = "purchaseApartmentRequest( " @ %space @ ", " @ %useBux @ ", " @ %priceFinal @ ",  \"CSSpacePurchaseSuccess\", \"CSSpacePurchaseFailed\");";
    MessageBoxYesNo(%title, %text, %cmd, "CSSpacePurchaseCancel();");
}
function CSSpacePurchaseDowngradeCheck(%space, %useBux, %priceFinal, %lossVPoints, %lossVBux)
{
    if ((%lossVPoints > 0) || (%lossVBux > 0))
    {
        %loss = CSSpacePurchasePriceFormatting(%lossVPoints, %lossVBux);
        %text = "<just:left>" @ "\n" @ $MsgCat::custSpace["TRADE_IN_DOWN_A"] @ " " @ %loss @ " " @ %loss[$MsgCat::custSpace @ "TRADE_IN_DOWN_B"] @ "\n";
        %buttons = "Yes - Trade in" @ "\t" @ "No - Cancel";
        %dlg = MessageBoxCustom("Warning", %text, %buttons);
        %dlg.callback[%space,", ",%useBux,", ",%priceFinal,",  \"CSSpacePurchaseSuccess\", \"CSSpacePurchaseFailed\");",0] = "purchaseApartmentRequest( ";
        %dlg.callback[1] = "CSSpacePurchaseCancel();";
    }
    else
    {
        purchaseApartmentRequest(%space, %useBux, %priceFinal, "CSSpacePurchaseSuccess", "CSSpacePurchaseFailed");
    }
}
function CSSpacePurchaseCancel()
{
    %title = "Purchase Cancelled";
    %text = "<just:center>" @ "\n" @ $MsgCat::custSpace["PURCHASE_ABORTED"] @ "\n";
    MessageBoxOK(%title, %text, "");
}
function CSSpacePurchaseSuccess(%unused, %unused, %vurl)
{
    $Player::myPlaceVURL = %vurl;
    %title = "Get a Room (Complete!)";
    gUserPropMgrClient.setProperty($Player::Name, "ShowOwnerTip", 1);
    %text = "<just:left>" @ "\n" @ $MsgCat::custSpace["PURCHASE_DONE"] @ "\n";
    %buttons = "Go there now" @ "\t" @ "Close";
    %dlg = MessageBoxCustom(%title, %text, %buttons);
    %dlg.callback[%vurl,"\");",0] = "vurlOperation( \"";
    getOwnerSpacesInfo($Player::Name, "");
}
function CSSpacePurchaseFailed(%errorCode)
{
    %errorCode = strupr(%errorCode);
    if ($MsgCat::custSpace["ERROR_",%errorCode] $= "")
    {
        %errorCode = "GENERIC";
    }
    %text = "<just:left>" @ "\n" @ "<spush><b>There was a problem!<spop>\n" @ "\n" @ "We could not purchase the apartment for you because " @ $MsgCat::custSpace["ERROR_",%errorCode] @ "\n";
    MessageBoxOK("Purchase a Space", %text, "");
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
