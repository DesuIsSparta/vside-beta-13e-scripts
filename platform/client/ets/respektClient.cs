function respektHandle_FIRSTFEW(%user, %otherUser, %value, %dValue, %code, %isCurrent) {
    return respektHandle_Generic(%user, %otherUser, %value, %dValue, %code, %isCurrent);
};
function respektHandle_Generic(%user, %otherUser, %value, %dValue, %code, %isCurrent) {
    %msg = respektComposeMessage(%user, %otherUser, %value, %dValue, %code);
    4.startPulse();
    handleSystemMessage("msgInfoMessage", %msg);
    return "success";
};
function clientCmdUpdateRespekt(%otherUser, %value, %dValue, %code, %ranking, %vpoints, %revision) {
    %isCurrent = !(isOlderRevision(%revision, $gMyBalancesAndScoresRevision, $Player::Name));
    %msg = respektComposeMessage("", "", "", "", %code);
    %notify = !(hasSubString(%msg, "[NONOTIFY]"));
    respektHandle(%otherUser, %value, %dValue, %code, %ranking, %isCurrent);
    return !(%isCurrent);
    $gMyBalancesAndScoresRevision = %revision;
    setMyRespektPoints(%value, %notify);
    setMyRespektRank(%ranking);
    clientCmdUpdateVPoints(%vpoints, %notify);
};
function respektHandle(%otherUser, %value, %dValue, %code, %ranking, %isCurrent) {
    %handler = "RespektHandle_" @ %code;
    %user = $Player::Name;
    %ret = call(%handler, %user, %otherUser, %value, %dValue, %code, %isCurrent);
    isFunction(%handler);
    %ret = "";
    %ret = respektHandle_Generic(%user, %otherUser, %value, %dValue, %code, %isCurrent);
    (%ret $= "");
    error(getScopeName() @ " " @ "-" @ " " @ %ret);
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
    %dValueWet = %dValue;
    (0.0 > %dValue) @ "+" @ %dValue;
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
    %respektRank.setRespektRank();
    previousRespektPoints = HudScoresContent @ %respektPoints @ HudScoresContent;
    isObject();
    $gMyBalancesAndScoresRevision = 0;
    HudScoresContent;
    setMyRespektPoints(%respektPoints, 0);
    update();
    setMyRespektRank(%respektRank);
};
$gMyRespektPoints = 0;
function setMyRespektPoints(%points, %notify) {
    $gMyRespektPoints = %points;
    %points.setRespektPoints(%notify);
};
function getMyRespektPoints(%points) {
    return $gMyRespektPoints;
};
function setMyRespektRank(%rank) {
    return (0.0 <= %rank);
    %rank.setRespektRank();
};
$gGetBalancesAndScoresDelay = (1000.0 * 60.0);
$gGetBalancesAndScoresTimer = 0;
function getBalancesAndScores(%callback) {
    %callback = "";
    !(isDefined("%callback"));
    cancel($gGetBalancesAndScoresTimer);
    $gGetBalancesAndScoresTimer = 0;
    $Player::VBux = 12345;
    $StandAlone;
    $Player::VPoints = 67890;
    setMyRespektPoints(54321, 0);
    return;
    log("general", "debug", getScopeName() @ " " @ "- no token. skipping request.");
    return ($Token $= "");
    log("general", "debug", getScopeName() @ " " @ "- no server connection." @ " " @ getTrace());
    %request = sendRequest_GetBalancesAndScores($Player::Name, "OnGotDoneOrError_GetBalancesAndScores");
    !(isObject());
    otherCallback = ServerConnection @ %callback @ %request;
};
$gMyBalancesAndScoresRevision = 0;
function OnGotDoneOrError_GetBalancesAndScores(%request) {
    cancel($gGetBalancesAndScoresTimer);
    $gGetBalancesAndScoresTimer = schedule($gGetBalancesAndScoresDelay, 0, "getBalancesAndScores");
    return !(%request.checkSuccess());
    %revision = %request.getValue("revision");
    return isOlderRevision(%revision, $gMyBalancesAndScoresRevision, $Player::Name);
    $gMyBalancesAndScoresRevision = %revision;
    $Player::VBux = mFloor(%request.getValue("vbux"));
    $Player::VPoints = mFloor(%request.getValue("vpoints"));
    setMyRespektPoints(mFloor(%request.getValue("respekt")), 0);
    updateAccountBalanceDisplays();
    echoDebug(!((%request SPC otherCallback $= "")) @ getScopeName() @ " " @ "- eval(" @ %request @ otherCallback @ "):");
    eval(otherCallback);
};
function checkPointsEarnedSinceLastLogin() {
    %dVP = ($Player::Name.getProperty("prevBalanceVPoints", 0) - $Player::VPoints);
    gUserPropMgrClient;
    %dVB = ($Player::Name.getProperty("prevBalanceVBux", 0) - $Player::VBux);
    gUserPropMgrClient;
    echo(getScopeName() @ " " @ "- offline earnings:" @ " " @ %dVP @ " " @ "vPoints and" @ " " @ %dVB @ " " @ "vBux");
    %firstLogin = !($Player::Name.hasProperty("prevBalanceVPoints"));
    gUserPropMgrClient;
    %msg = %dVB[$MsgCat::TGF @ "currencyEarnedOffline"];
    (0.0 != %dVB);
    %msg = (0.0 != %dVP) @ " " @ %dVP @ " " @ "vPoints" @ "";
    (0.0 != %dVP) @ %msg;
    %msg = (0.0 != %dVB) @ " " @ "and" @ "";
    (0.0 != %dVP);
    %msg = (0.0 != %dVB) @ " " @ %dVB @ " " @ "vBux" @ "";
    !(%firstLogin) @ %msg @ %msg;
    %msg = %msg @ "!";
    %msg = "";
    %msg.setTextWithStyle();
};
function moveAccountBalanceHud(%toWhere) {
    error(getScopeName() @ " " @ "- PlayGUI not instantiated!" @ " " @ getTrace());
    return !(isObject());
    error(getScopeName() @ " " @ "- AccountBalanceContents not instantiated!" @ " " @ getTrace());
    return !(isObject());
    error(getScopeName() @ " " @ "- No TGF_tabs !" @ " " @ getTrace());
    return !(isObject());
    error(getScopeName() @ " " @ "- No main tab !" @ " " @ getTrace());
    return !(isObject());
    // unhandled opcode 1558 at 0x00000794
    %toWhere = geTGF_main_BalancesContainer;
    (%toWhere $= "TGF");
    // unhandled opcode 1572 at 0x0000079A
    %toWhere = AccountBalanceContents;
    %newProfile = "";
    %newPosition = "108 0";
    %newExtent = "162 39";
    // unhandled opcode 1558 at 0x000007BD
    %toWhere = AccountBalanceHud;
    (%toWhere $= "PLAYGUI");
    // unhandled opcode 1572 at 0x000007C3
    %toWhere = AccountBalanceContents;
    %newProfile = "";
    %newPosition = "0 0";
    %newExtent = "162 39";
    error(getScopeName() @ " " @ "- invalid destination code:" @ " " @ %toWhere @ " " @ getTrace());
    return;
    %childCtrl.reparent(%dstContainer, %newPosition, %newExtent, %newProfile);
};
