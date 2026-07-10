function respektHandle_FIRSTFEW(%user, %otherUser, %value, %dValue, %code, %isCurrent) {
    return respektHandle_Generic(%user, %otherUser, %value, %dValue, %code, %isCurrent);
};
function respektHandle_Generic(%user, %otherUser, %value, %dValue, %code, %isCurrent) {
    %msg = respektComposeMessage(%user, %otherUser, %value, %dValue, %code);
    if (!(hasSubString(%msg, "[NONOTIFY]"))) {
        if (%isCurrent) {
            4.startPulse();
        }
        handleSystemMessage("msgInfoMessage", %msg);
    }
    return "success";
};
function clientCmdUpdateRespekt(%otherUser, %value, %dValue, %code, %ranking, %vpoints, %revision) {
    %isCurrent = !(isOlderRevision(%revision, $gMyBalancesAndScoresRevision, $Player::Name));
    %msg = respektComposeMessage("", "", "", "", %code);
    %notify = !(hasSubString(%msg, "[NONOTIFY]"));
    respektHandle(%otherUser, %value, %dValue, %code, %ranking, %isCurrent);
    if (!(%isCurrent)) {
        return;
    }
    $gMyBalancesAndScoresRevision = %revision;
    setMyRespektPoints(%value, %notify);
    setMyRespektRank(%ranking);
    if (!(%vpoints $= "")) {
        clientCmdUpdateVPoints(%vpoints, %notify);
    }
};
function respektHandle(%otherUser, %value, %dValue, %code, %ranking, %isCurrent) {
    %handler = "RespektHandle_" @ %code;
    %user = $Player::Name;
    if (isFunction(%handler)) {
        %ret = call(%handler, %user, %otherUser, %value, %dValue, %code, %isCurrent);
    }
    %ret = "";
    if ((%ret $= "")) {
        %ret = respektHandle_Generic(%user, %otherUser, %value, %dValue, %code, %isCurrent);
    }
    if (!(%ret $= "success")) {
        error(getScopeName() @ " " @ "-" @ " " @ %ret);
    }
};
function respektComposeMessage(%user, %otherUser, %value, %dValue, %code) {
    %levelNum = respektScoreToLevel(%value);
    %levelName = respektLevelToNameWithoutArticle(%levelNum);
    %levelNameWithIndefiniteArticle = respektLevelToNameWithIndefiniteArticle(%levelNum);
    %userProfileURL = $Net::ProfileURL @ urlEncode(stripUnprintables(%user));
    %otherUserProfileURL = $Net::ProfileURL @ urlEncode(stripUnprintables(%otherUser));
    %userWet = %user.getPlayerMarkup("ffddeeff");
    pChat;
    %otherUserWet = %otherUser.getPlayerMarkup("ffddeeff");
    pChat;
    if ((0.0 > %dValue)) {
    }
    %dValueWet = %dValue;
    "+" @ %dValue;
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
};
function clientCmdInitialScores(%respektPoints, %respektRank) {
    if (isObject(HudScoresContent)) {
        %respektRank.setRespektRank();
        previousRespektPoints = %respektPoints @ HudScoresContent;
        HudScoresContent;
    }
    $gMyBalancesAndScoresRevision = 0;
    setMyRespektPoints(%respektPoints, 0);
    if (isObject(AccountBalanceHud)) {
        AccountBalanceHud.update();
    }
    setMyRespektRank(%respektRank);
};
$gMyRespektPoints = 0;
function setMyRespektPoints(%points, %notify) {
    $gMyRespektPoints = %points;
    if (isObject(HudScoresContent)) {
        %points.setRespektPoints(%notify);
    }
};
function getMyRespektPoints(%points) {
    return $gMyRespektPoints;
};
function setMyRespektRank(%rank) {
    if ((%rank $= "")) {
    }
    if ((0.0 <= %rank)) {
        return;
    }
    if (isObject(HudScoresContent)) {
        %rank.setRespektRank();
    }
};
$gGetBalancesAndScoresDelay = (1000.0 * 60.0);
$gGetBalancesAndScoresTimer = 0;
function getBalancesAndScores(%callback) {
    if (!(isDefined("%callback"))) {
        %callback = "";
    }
    cancel($gGetBalancesAndScoresTimer);
    $gGetBalancesAndScoresTimer = 0;
    if ($StandAlone) {
        $Player::VBux = 12345;
        $Player::VPoints = 67890;
        setMyRespektPoints(54321, 0);
        return;
    }
    if (($Token $= "")) {
        log("general", "debug", getScopeName() @ " " @ "- no token. skipping request.");
        return;
    }
    if (!(isObject(ServerConnection))) {
        log("general", "debug", getScopeName() @ " " @ "- no server connection." @ " " @ getTrace());
    }
    %request = sendRequest_GetBalancesAndScores($Player::Name, "OnGotDoneOrError_GetBalancesAndScores");
    %request.otherCallback = %callback;
};
$gMyBalancesAndScoresRevision = 0;
function OnGotDoneOrError_GetBalancesAndScores(%request) {
    cancel($gGetBalancesAndScoresTimer);
    $gGetBalancesAndScoresTimer = schedule($gGetBalancesAndScoresDelay, 0, "getBalancesAndScores");
    if (!(%request.checkSuccess())) {
        return;
    }
    %revision = %request.getValue("revision");
    if (isOlderRevision(%revision, $gMyBalancesAndScoresRevision, $Player::Name)) {
        return;
    }
    $gMyBalancesAndScoresRevision = %revision;
    $Player::VBux = mFloor(%request.getValue("vbux"));
    $Player::VPoints = mFloor(%request.getValue("vpoints"));
    setMyRespektPoints(mFloor(%request.getValue("respekt")), 0);
    updateAccountBalanceDisplays();
    if (!(%request.otherCallback $= "")) {
        echoDebug(getScopeName() @ " " @ "- eval(" @ %request.otherCallback @ "):");
        eval(%request.otherCallback);
    }
};
function checkPointsEarnedSinceLastLogin() {
    %dVP = ($Player::Name.getProperty("prevBalanceVPoints", 0) - $Player::VPoints);
    gUserPropMgrClient;
    %dVB = ($Player::Name.getProperty("prevBalanceVBux", 0) - $Player::VBux);
    gUserPropMgrClient;
    echo(getScopeName() @ " " @ "- offline earnings:" @ " " @ %dVP @ " " @ "vPoints and" @ " " @ %dVB @ " " @ "vBux");
    %firstLogin = !($Player::Name.hasProperty("prevBalanceVPoints"));
    gUserPropMgrClient;
    if (!(%firstLogin)) {
        if ((0.0 != %dVP)) {
        }
    }
    if ((0.0 != %dVB)) {
        %msg = %dVB[$MsgCat::TGF @ "currencyEarnedOffline"];
        if ((0.0 != %dVP)) {
        }
        %msg = " " @ %dVP @ " " @ "vPoints" @ "";
        %msg;
        if ((0.0 != %dVP)) {
        }
        if ((0.0 != %dVB)) {
        }
        %msg = " " @ "and" @ "";
        %msg;
        if ((0.0 != %dVB)) {
        }
        %msg = " " @ %dVB @ " " @ "vBux" @ "";
        %msg;
        %msg = %msg @ "!";
    }
    %msg = "";
    %msg.setTextWithStyle();
};
function moveAccountBalanceHud(%toWhere) {
    if (!(isObject(PlayGui))) {
        error(getScopeName() @ " " @ "- PlayGUI not instantiated!" @ " " @ getTrace());
        return;
    }
    if (!(isObject(AccountBalanceContents))) {
        error(getScopeName() @ " " @ "- AccountBalanceContents not instantiated!" @ " " @ getTrace());
        return;
    }
    if (!(isObject(geTGF_tabs))) {
        error(getScopeName() @ " " @ "- No TGF_tabs !" @ " " @ getTrace());
        return;
    }
    if (!(isObject(geTGF_main_BalancesContainer))) {
        error(getScopeName() @ " " @ "- No main tab !" @ " " @ getTrace());
        return;
    }
    if ((%toWhere $= "TGF")) {
        // unhandled opcode 1558 at 0x00000794
        %toWhere = geTGF_main_BalancesContainer;
        // unhandled opcode 1572 at 0x0000079A
        %toWhere = AccountBalanceContents;
        %newProfile = "";
        %newPosition = "108 0";
        %newExtent = "162 39";
    }
    if ((%toWhere $= "PLAYGUI")) {
        // unhandled opcode 1558 at 0x000007BD
        %toWhere = AccountBalanceHud;
        // unhandled opcode 1572 at 0x000007C3
        %toWhere = AccountBalanceContents;
        %newProfile = "";
        %newPosition = "0 0";
        %newExtent = "162 39";
    }
    error(getScopeName() @ " " @ "- invalid destination code:" @ " " @ %toWhere @ " " @ getTrace());
    return;
    %childCtrl.reparent(%dstContainer, %newPosition, %newExtent, %newProfile);
};
