function clientCmdTeleportSuccessful()
{
    if (isObject($VURL::curVURL))
    {
        $VURL::curVURL.doReportSuccess();
        $VURL::curVURL.delete();
        $VURL::curVURL = "";
    }
    else
    {
        echo("Teleport successful");
    }
    geTGF.doreopen = 0;
    setIdle(0);
    if (isObject($player))
    {
        $player.adjustHorizontalScale();
    }
    return ;
}
function clientCmdTeleportFailure(%retry)
{
    if (isObject($VURL::curVURL))
    {
        echo("VURL Teleport Failed. VURL=" @ $VURL::curVURL.vurl);
        if (%retry != 0)
        {
            log("network", "info", "Attempting Retry.");
            if ($VURL::curVURL.execute())
            {
                return ;
            }
            log("network", "warn", "VURL Teleportion faild, retries exausted.");
        }
        else
        {
            geTGF.reopen();
            $VURL::curVURL.doReportError("FAIL", "");
        }
        $VURL::curVURL.delete();
    }
    else
    {
        echo("Teleport failed");
        geTGF.reopen();
    }
    return ;
}
function clientCmdNotifyOfRefuseTeleport()
{
    handleSystemMessage("msgInfoMessage", $MsgCat::teleport["NOTIFY-REFUSING-TELEPORTS"]);
    return ;
}
if (!isObject($pi))
{
    $pi = 3.14159;
}
function Player::adjustHorizontalScale(%this)
{
    %hScale = getWord(%this.getScale(), 0);
    %vScale = getWord(%this.getScale(), 2);
    if (!gGetField(%this, isScaling))
    {
        gSetField(%this, isScaling, 1);
        gSetField(%this, baseHorizScale, %hScale);
        %hScale = 0.05;
    }
    %hScale = mMin(%hScale + 0.05, gGetField(%this, baseHorizScale));
    %this.setScale(%hScale SPC %hScale SPC %vScale);
    if (%hScale < gGetField(%this, baseHorizScale))
    {
        %this.schedule(25, "adjustHorizontalScale");
    }
    else
    {
        gSetField(%this, isScaling, 0);
    }
    return ;
}
function doTeleportToMyApartment(%ignoreDownloadStatus)
{
    if (!isDefined("%ignoreDownloadStatus"))
    {
        %ignoreDownloadStatus = 0;
    }
    getApartmentVURL("doTeleportToMyApartmentCallback", %ignoreDownloadStatus);
    return ;
}
function doTeleportToMyApartmentCallback(%status, %vurl, %ignoreDownloadStatus)
{
    if (%status $= "fail")
    {
        handleSystemMessage("msgInfoMessage", "We could not find your apartment at this time.");
    }
    else
    {
        if (%status $= "noOwnedSpace")
        {
            %statusMsg = GetMyApartmentVURLCommand.getValue("statusMsg");
            handleSystemMessage("msgInfoMessage", "We could not find your apartment." NL %statusMsg);
        }
        else
        {
            if (%vurl $= "")
            {
                handleSystemMessage("msgInfoMessage", "You do not appear to own an appartment.");
            }
            else
            {
                if (CustomSpacesSelector.isVisible())
                {
                    CustomSpacesSelector.close();
                }
                vurlOperation(%vurl, %ignoreDownloadStatus);
            }
        }
    }
    return ;
}
function getApartmentVURL(%callback, %ignoreDownloadStatus)
{
    %request = safeEnsureScriptObject("ManagerRequest", "GetMyApartmentVURLCommand");
    if (%request.isOpen())
    {
        return ;
    }
    %request.callback = %callback;
    %request.ignoreDownloadStatus = %ignoreDownloadStatus;
    %url = $Net::ClientServiceURL @ "/GetSpaceVURL" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token) @ "&owner=" @ urlEncode($Player::Name);
    log("network", "debug", "GetSpaceVURL: " @ %url);
    %request.setURL(%url);
    %request.start();
    return ;
}
function GetMyApartmentVURLCommand::onDone(%this)
{
    echo(getScopeName());
    %status = findRequestStatus(%this);
    log("network", "debug", "GetMyApartmentAddress status: " @ %status);
    if (%status $= "fail")
    {
        echo(getScopeName() @ "->failed");
        %statusMsg = %this.getValue("statusMsg");
        log("network", "error", "GetMyApartmentAddress failed due to: " @ %statusMsg);
    }
    else
    {
        echo(getScopeName() @ "->success");
        log("network", "debug", "GetMyApartmentAddress::onDone: " @ %status);
    }
    %vurl = %this.getValue("vurl");
    $Player::myPlaceVURL = %vurl;
    if (!(%this.callback $= ""))
    {
        %cmd = %this.callback @ "(\"" @ %status @ "\", \"" @ %vurl @ "\", \"" @ %this.ignoreDownloadStatus @ "\");";
        eval(%cmd);
    }
    return ;
}
function GetMyApartmentVURLCommand::onError(%this, %unused, %errMsg)
{
    log("network", "debug", "GetMyApartmentVURLCommand::onError: " @ %errMsg);
    $Player::myPlaceVURL = "";
    %this.schedule(0, "delete");
    return ;
}
