addMessageCallback('MsgConnectionError', handleConnectionErrorMessage);
function handleConnectionErrorMessage(%unused, %msgString)
{
    $ServerConnectionErrorMessage = %msgString;
    return ;
}
function GameConnection::initialControlSet(%this)
{
    echo("*** Initial Control Object");
    if (!isObject(EditorGui) && !Editor::checkActiveLoadDone())
    {
        if (Canvas.getContent() != PlayGui.getId())
        {
            Canvas.setContent(PlayGui);
        }
    }
    %this.etsInit();
    return ;
}
function GameConnection::setLagIcon(%this, %state)
{
    if (%this.getAddress() $= "local")
    {
        return ;
    }
    LagIcon.setVisible(%state $= "true");
    return ;
}
function GameConnection::onConnectionAccepted(%this)
{
    LagIcon.setVisible(0);
    $GameConnection = %this;
    $VURLcmd = "";
    getUserActivityMgr().setActivityActive("traveling", 1);
    return ;
}
function GameConnection::onServerConnectionPossiblyTimingOut(%this)
{
    warn("Possibly losing connection to server..");
    return ;
}
function GameConnection::onServerConnectionRestored(%this)
{
    warn("Restored connection to server.");
    return ;
}
function GameConnection::onServerConnectionTimedOut(%this)
{
    disconnectedCleanup(geTGF);
    MessageBoxOK("TIMED OUT", $MsgCat::network["E-SERVER-TIMEOUT"], "");
    return ;
}
function GameConnection::onConnectionDropped(%this, %msg)
{
    %msg = standardSubstitutions(%msg);
    if (%this.waitForDisconnect)
    {
        %this.waitForDisconnect = 0;
        disconnectedCleanup("");
        WorldMap.schedule(1, "doServerJoin", $SpawnTargetSavedVURL);
        return ;
    }
    if (getField(%msg, 0) $= "bootToMap")
    {
        %currentCity = WorldMap.currentCity;
        WorldMap.setNotConnectedToServer();
        geTGF.openToTabName("map");
        %levelOrCityName = getField(%msg, 1);
        if (%levelOrCityName $= 0)
        {
        }
        else
        {
            if (%levelOrCityName $= 1)
            {
                WorldMap.selectCity(%currentCity);
            }
            else
            {
                WorldMap.selectCity(%levelOrCityName);
            }
        }
        MessageBoxOK("BOOTED TO MAP", getFields(%msg, 2), "");
    }
    else
    {
        logout(0);
        disconnectedCleanup(LoginGui);
        MessageBoxOK("DISCONNECT", $MsgCat::network["E-DROPPED"] @ %msg, "");
    }
    return ;
}
function GameConnection::onConnectionError(%this, %msg)
{
    if ($CacheFlagIsSet)
    {
        ServerConnection.deleteCacheFile($CurrentMission);
        $CurrentMission = "";
    }
    disconnectedCleanup(geTGF);
    MessageBoxOK("DISCONNECT", $ServerConnectionErrorMessage @ " (" @ %msg @ ")", "");
    return ;
}
function GameConnection::onConnectRequestRejected(%this, %msg, %extra)
{
    %destGui = LoginGui;
    if (%msg $= "CR_INVALID_PROTOCOL_VERSION")
    {
        %error = $MsgCat::network["E-PROTOCOL-VER"];
        %destGui = geTGF;
    }
    else
    {
        if (%msg $= "CR_INVALID_CONNECT_PACKET")
        {
            %error = "Internal Error: badly formed network packet";
            %destGui = geTGF;
        }
        else
        {
            if (%msg $= "CR_YOUAREBANNED")
            {
                %error = "You are not allowed to play on this server.";
            }
            else
            {
                if (%msg $= "CR_TOKEN")
                {
                    %error = $ETS::AppName @ " " @ $MsgCat::network["E-SERVICE-UNAVAIL"];
                }
                else
                {
                    if (%msg $= "CR_SERVERFULL")
                    {
                        %error = $MsgCat::login["E-SERVER-FULL"];
                        %destGui = geTGF;
                    }
                    else
                    {
                        if (%msg $= "CR_BAD_TARGET")
                        {
                            %error = $MsgCat::login["E-BAD-TARGET"];
                            %destGui = geTGF;
                        }
                        else
                        {
                            if (%msg $= "CR_CANNOT_ACTIVATE_APARTMENT")
                            {
                                %error = $MsgCat::login["E-CANNOT-ACTIVATE-APARTMENT"];
                                %destGui = geTGF;
                            }
                            else
                            {
                                if (%msg $= "CR_APARTMENT_ACTIVATION_DENIED")
                                {
                                    %error = $MsgCat::login["E-APARTMENT-ACTIVATION-DENIED"];
                                    %destGui = geTGF;
                                }
                                else
                                {
                                    if (%msg $= "CR_APARTMENT_ACTIVE_ELSEWHERE")
                                    {
                                        %error = $MsgCat::login["E-APARTMENT-ACTIVATE-ELSEWHERE"];
                                        %destGui = geTGF;
                                    }
                                    else
                                    {
                                        if (%msg $= "CR_LEVEL_COMPLETED")
                                        {
                                            %error = $MsgCat::login["E-LEVEL-COMPLETED"];
                                            %destGui = geTGF;
                                        }
                                        else
                                        {
                                            if (%msg $= "CHR_PASSWORD")
                                            {
                                                if ($Client::Password $= "")
                                                {
                                                    MessageBoxOK("REJECTED", $MsgCat::login["PASSWORD-REQD"], "");
                                                }
                                                else
                                                {
                                                    $Client::Password = "";
                                                    MessageBoxOK("REJECTED", $MsgCat::login["PASSWORD-BAD"], "");
                                                }
                                                return ;
                                            }
                                            else
                                            {
                                                if (%msg $= "CHR_PROTOCOL")
                                                {
                                                    %error = $MsgCat::network["E-PROTOCOL-VER"];
                                                    %error = %error NL $MsgCat::login["E-UPGRADE-2"];
                                                    %destGui = geTGF;
                                                }
                                                else
                                                {
                                                    if (%msg $= "CHR_CLASSCRC")
                                                    {
                                                        %error = $MsgCat::login["E-UPGRADE-1"] @ $ETS::AppName @ ".";
                                                        %error = %error NL $MsgCat::login["E-UPGRADE-2"];
                                                        %destGui = geTGF;
                                                    }
                                                    else
                                                    {
                                                        if (%msg $= "CHR_CLASSCRCROOTDIRVAL")
                                                        {
                                                            %error = $MsgCat::login["E-UPGRADE-1"] @ $ETS::AppName @ ".";
                                                            %error = %error NL $MsgCat::login["E-UPGRADE-2"];
                                                            %destGui = geTGF;
                                                        }
                                                        else
                                                        {
                                                            if (%msg $= "CHR_INVALID_CHALLENGE_PACKET")
                                                            {
                                                                %error = $MsgCat::login["E-UPGRADE-1"] @ $ETS::AppName @ ".";
                                                                %error = %error NL $MsgCat::login["E-UPGRADE-2"];
                                                                %error = %error NL "(assets)";
                                                                %destGui = geTGF;
                                                            }
                                                            else
                                                            {
                                                                if (%msg $= "CR_ASSETS_MISSING")
                                                                {
                                                                    %error = "Cities/packages are missing or out of date: " @ %extra;
                                                                    %destGui = CityDownloadGui;
                                                                    queuePackageUpdatesByString(%extra);
                                                                }
                                                                else
                                                                {
                                                                    %error = "Connection error.  Please try another server.  Error code: (" @ %msg @ ")";
                                                                    %destGui = geTGF;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/connectionRejected/" @ %msg);
    if ((%destGui.getId() == LoginGui.getId()) && !((%msg $= "CR_ASSETS_MISSING")))
    {
        logout(0);
    }
    disconnectedCleanup(%destGui);
    error("Could Not Connect: " @ strreplace(%error, "\n", " "));
    if (!(%msg $= "CR_ASSETS_MISSING"))
    {
        MessageBoxOK("Could Not Connect", %error, "");
    }
    return ;
}
function GameConnection::onConnectRequestTimedOut(%this)
{
    disconnectedCleanup(geTGF);
    MessageBoxOK("TIMED OUT", $MsgCat::network["E-SERVER-TIMEOUT"], "");
    return ;
}
function disconnect(%screen)
{
    if (isObject(ServerConnection))
    {
        ServerConnection.delete();
    }
    disconnectedCleanup(%screen);
    destroyServer();
    return ;
}
function disconnectedStop()
{
    ConvBub.close(0);
    alxStopAll();
    if (isObject(MusicPlayer))
    {
        MusicPlayer.stop();
    }
    return ;
}
function disconnectedCleanup(%screen)
{
    $gWorldMapJoiningServer = 0;
    disconnectedStop();
    LagIcon.setVisible(0);
    if (isObject(%screen))
    {
        if (%screen.getId() == geTGF.getId())
        {
            WorldMap.setNotConnectedToServer();
            geTGF.open();
        }
        else
        {
            Canvas.setContent(%screen);
        }
    }
    else
    {
        geTGF.closeFully();
    }
    clearTextureHolds();
    purgeResources();
    textureDownloadPurgeCallbacks();
    setDoneRendering();
    fmodClose();
    TransitionCancel(0);
    CustomSpaceClient::OnClientDisconnect();
    if (isObject(ApplauseMeterGui))
    {
        ApplauseMeterGui.close();
    }
    if (isObject(SalonStyleSelector))
    {
        $gSalonChairCurrent = 0;
        SalonStyleSelector.close();
    }
    if (isObject(PlantDetailsGui))
    {
        PlantDetailsGui.close();
    }
    $StoreSkusLayer = "";
    clientCmdOnLeaveStore("");
    leaveAllTutorialSpaces();
    afxEndMissionNotify();
    return ;
}
function loggedoutCleanup()
{
    $Token = "";
    return ;
}
