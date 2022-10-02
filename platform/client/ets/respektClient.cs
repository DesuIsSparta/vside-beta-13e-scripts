function respektHandle_FIRSTFEW(%user, %otherUser, %value, %dValue, %code, %isCurrent)
{
    return respektHandle_Generic(%user, %otherUser, %value, %dValue, %code, %isCurrent);
}
function respektHandle_Generic(%user, %otherUser, %value, %dValue, %code, %isCurrent)
{
    %msg = respektComposeMessage(%user, %otherUser, %value, %dValue, %code);
    if (!hasSubString(%msg, "[NONOTIFY]"))
    {
        if (%isCurrent)
        {
            AccountBalanceHud.startPulse(4);
        }
        handleSystemMessage("msgInfoMessage", %msg);
    }
    return "success";
}
function clientCmdUpdateRespekt(%otherUser, %value, %dValue, %code, %ranking, %vpoints, %revision)
{
    %isCurrent = !isOlderRevision(%revision, $gMyBalancesAndScoresRevision, $Player::Name);
    %msg = respektComposeMessage("", "", "", "", %code);
    %notify = !hasSubString(%msg, "[NONOTIFY]");
    respektHandle(%otherUser, %value, %dValue, %code, %ranking, %isCurrent);
    if (!%isCurrent)
    {
        return ;
    }
    $gMyBalancesAndScoresRevision = %revision;
    setMyRespektPoints(%value, %notify);
    setMyRespektRank(%ranking);
    if (!(%vpoints $= ""))
    {
        clientCmdUpdateVPoints(%vpoints, %notify);
    }
    return ;
}
function respektHandle(%otherUser, %value, %dValue, %code, %ranking, %isCurrent)
{
    %handler = "RespektHandle_" @ %code;
    %user = $Player::Name;
    if (isFunction(%handler))
    {
        %ret = call(%handler, %user, %otherUser, %value, %dValue, %code, %isCurrent);
    }
    else
    {
        %ret = "";
    }
    if (%ret $= "")
    {
        %ret = respektHandle_Generic(%user, %otherUser, %value, %dValue, %code, %isCurrent);
    }
    else
    {
        if (!(%ret $= "success"))
        {
            error(getScopeName() SPC "-" SPC %ret);
        }
    }
    return ;
}
function respektComposeMessage(%user, %otherUser, %value, %dValue, %code)
{
    %levelNum = respektScoreToLevel(%value);
    %levelName = respektLevelToNameWithoutArticle(%levelNum);
    %levelNameWithIndefiniteArticle = respektLevelToNameWithIndefiniteArticle(%levelNum);
    %userProfileURL = $Net::ProfileURL @ urlEncode(stripUnprintables(%user));
    %otherUserProfileURL = $Net::ProfileURL @ urlEncode(stripUnprintables(%otherUser));
    %userWet = pChat.getPlayerMarkup(%user, "ffddeeff");
    %otherUserWet = pChat.getPlayerMarkup(%otherUser, "ffddeeff");
    %dValueWet = %dValue > 0 ? "+" : %dValue;
    %msg = getRespektMessage(%dValue, %code);
    %msg = strreplace(%msg, "[USER]", %userWet);
    %msg = strreplace(%msg, "[OTHERUSER]", %otherUserWet);
    %msg = strreplace(%msg, "[VALUE]", %value);
    %msg = strreplace(%msg, "[DVALUE]", %dValueWet);
    %msg = strreplace(%msg, "[LEVELNUM]", %levelNum);
    %msg = strreplace(%msg, "[LEVELNAME]", %levelName);
    %msg = strreplace(%msg, "[LEVELNAME_WITH_INDEFINITE_ARTICLE]", %levelNameWithIndefiniteArticle);
    %msg = strreplace(%msg, "[USER_PROFILE_URL]", %userProfileURL);
    %msg = strreplace(%msg, "[OTHERUSER_PROFILE_URL]", %otherUserProfileURL);
    return %msg;
}
function clientCmdInitialScores(%respektPoints, %respektRank)
{
    if (isObject(HudScoresContent))
    {
        HudScoresContent.setRespektRank(%respektRank);
        HudScoresContent.previousRespektPoints = %respektPoints;
    }
    $gMyBalancesAndScoresRevision = 0;
    setMyRespektPoints(%respektPoints, 0);
    if (isObject(AccountBalanceHud))
    {
        AccountBalanceHud.update();
    }
    setMyRespektRank(%respektRank);
    return ;
}
$gMyRespektPoints = 0;
function setMyRespektPoints(%points, %notify)
{
    $gMyRespektPoints = %points;
    if (isObject(HudScoresContent))
    {
        HudScoresContent.setRespektPoints(%points, %notify);
    }
    return ;
}
function getMyRespektPoints(%points)
{
    return $gMyRespektPoints;
}
function setMyRespektRank(%rank)
{
    if ((%rank $= "") && (%rank <= 0))
    {
        return ;
    }
    if (isObject(HudScoresContent))
    {
        HudScoresContent.setRespektRank(%rank);
    }
    return ;
}
$gGetBalancesAndScoresDelay = 60 * 1000;
$gGetBalancesAndScoresTimer = 0;
function getBalancesAndScores(%callback)
{
    if (!isDefined("%callback"))
    {
        %callback = "";
    }
    cancel($gGetBalancesAndScoresTimer);
    $gGetBalancesAndScoresTimer = 0;
    if ($StandAlone)
    {
        $Player::VBux = 12345;
        $Player::VPoints = 67890;
        setMyRespektPoints(54321, 0);
        return ;
    }
    if ($Token $= "")
    {
        log("general", "debug", getScopeName() SPC "- no token. skipping request.");
        return ;
    }
    if (!isObject(ServerConnection))
    {
        log("general", "debug", getScopeName() SPC "- no server connection." SPC getTrace());
    }
    %request = sendRequest_GetBalancesAndScores($Player::Name, "OnGotDoneOrError_GetBalancesAndScores");
    %request.otherCallback = %callback;
    return ;
}
$gMyBalancesAndScoresRevision = 0;
function OnGotDoneOrError_GetBalancesAndScores(%request)
{
    cancel($gGetBalancesAndScoresTimer);
    $gGetBalancesAndScoresTimer = schedule($gGetBalancesAndScoresDelay, 0, "getBalancesAndScores");
    if (!%request.checkSuccess())
    {
        return ;
    }
    %revision = %request.getValue("revision");
    if (isOlderRevision(%revision, $gMyBalancesAndScoresRevision, $Player::Name))
    {
        return ;
    }
    $gMyBalancesAndScoresRevision = %revision;
    $Player::VBux = mFloor(%request.getValue("vbux"));
    $Player::VPoints = mFloor(%request.getValue("vpoints"));
    setMyRespektPoints(mFloor(%request.getValue("respekt")), 0);
    updateAccountBalanceDisplays();
    if (!(%request.otherCallback $= ""))
    {
        echoDebug(getScopeName() SPC "- eval(" @ %request.otherCallback @ "):");
        eval(%request.otherCallback);
    }
    return ;
}
function checkPointsEarnedSinceLastLogin()
{
    %dVP = $Player::VPoints - gUserPropMgrClient.getProperty($Player::Name, "prevBalanceVPoints", 0);
    %dVB = $Player::VBux - gUserPropMgrClient.getProperty($Player::Name, "prevBalanceVBux", 0);
    echo(getScopeName() SPC "- offline earnings:" SPC %dVP SPC "vPoints and" SPC %dVB SPC "vBux");
    %firstLogin = !gUserPropMgrClient.hasProperty($Player::Name, "prevBalanceVPoints");
    if ((!%firstLogin && (%dVP != 0)) || (%dVB != 0))
    {
        %msg = $MsgCat::TGF["currencyEarnedOffline"];
        %msg = %msg @ %dVP != 0 ? " " : "";
        %msg = %msg @ (%dVP != 0) && (%dVB != 0) ? " " : "";
        %msg = %msg @ %dVB != 0 ? " " : "";
        %msg = %msg @ "!";
    }
    else
    {
        %msg = "";
    }
    geTGF_main_OfflineIncomeNotification.setTextWithStyle(%msg);
    return ;
}
function moveAccountBalanceHud(%toWhere)
{
    if (!isObject(PlayGui))
    {
        error(getScopeName() SPC "- PlayGUI not instantiated!" SPC getTrace());
        return ;
    }
    if (!isObject(AccountBalanceContents))
    {
        error(getScopeName() SPC "- AccountBalanceContents not instantiated!" SPC getTrace());
        return ;
    }
    if (!isObject(geTGF_tabs))
    {
        error(getScopeName() SPC "- No TGF_tabs !" SPC getTrace());
        return ;
    }
    if (!isObject(geTGF_main_BalancesContainer))
    {
        error(getScopeName() SPC "- No main tab !" SPC getTrace());
        return ;
    }
    if (%toWhere $= "TGF")
    {
        %dstContainer = geTGF_main_BalancesContainer;
        %childCtrl = AccountBalanceContents;
        %newProfile = "";
        %newPosition = "108 0";
        %newExtent = "162 39";
    }
    else
    {
        if (%toWhere $= "PLAYGUI")
        {
            %dstContainer = AccountBalanceHud;
            %childCtrl = AccountBalanceContents;
            %newProfile = "";
            %newPosition = "0 0";
            %newExtent = "162 39";
        }
        else
        {
            error(getScopeName() SPC "- invalid destination code:" SPC %toWhere SPC getTrace());
            return ;
        }
    }
    %childCtrl.reparent(%dstContainer, %newPosition, %newExtent, %newProfile);
    return ;
}
