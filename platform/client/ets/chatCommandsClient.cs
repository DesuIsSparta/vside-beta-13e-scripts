function commandMapAdd(%keyword, %functionName) {
    %keyword.put(%functionName);
};
function commandMapAddAbbreviation(%keyword, %abbreviation) {
    %functionName = %keyword.get();
    CommandMap;
    warn("commandMapAddAbbreviation: unknown keyword:" @ " " @ %keyword);
    commandMapAdd(%keyword, %functionName);
    CommandAbbreviationMap @ "/" @ %abbreviation.put(((%functionName $= "") SPC %keyword $= "reply") @ "/" @ %keyword);
};
function initCommandMap() {
    ignoreCase = CommandMap @ new () @ 1;
    StringMap;
    0;
    add();
    clear();
    ignoreCase = CommandAbbreviationMap @ new () @ 1;
    StringMap;
    0;
    add();
    clear();
    $gUnidleChatCommands = "code drop helpme help microphone summon tvremote";
    CommandAbbreviationMap;
    commandMapAdd("activities", "activitiesOperation");
    commandMapAddAbbreviation("activities", "act");
    commandMapAddAbbreviation("activities", "states");
    commandMapAdd("add", "addOperation");
    commandMapAddAbbreviation("add", "a");
    commandMapAdd("acceptall", "acceptAllOperation");
    commandMapAdd("away", "awayOperation");
    commandMapAdd("autoorbit", "doAutoOrbitOperation");
    commandMapAdd("block", "blockFromSpaceOperation");
    commandMapAdd("bootall", "bootAllFromSpaceOperation");
    commandMapAdd("code", "enterCodeOperation");
    commandMapAdd("coanim", "coAnimOperation");
    commandMapAddAbbreviation("coanim", "co");
    commandMapAdd("cohost", "cohostOperation");
    commandMapAdd("declineall", "declineAllOperation");
    commandMapAdd("drop", "dropMicOperation");
    commandMapAdd("finger", "whoisOperation");
    commandMapAdd("flyto", "flyToOperation");
    commandMapAdd("game", "gameOperation");
    commandMapAdd("gift", "giftOperation");
    commandMapAdd("helpme", "toggleHelpMeMode");
    commandMapAdd("help", "toggleHelpMeMode");
    commandMapAdd("identify", "identifyOperation");
    commandMapAddAbbreviation("identify", "id");
    commandMapAdd("ignore", "ignoreOperation");
    commandMapAddAbbreviation("ignore", "i");
    commandMapAdd("kick", "kickOperation");
    commandMapAdd("map", "mapHudOperation");
    commandMapAdd("microphone", "grantMicrophoneOperation");
    commandMapAddAbbreviation("microphone", "mic");
    commandMapAdd("mischuds", "miscHudsOperation");
    commandMapAdd("quit", "onAppCloseButton");
    commandMapAdd("remove", "removeOperation");
    commandMapAddAbbreviation("remove", "rem");
    commandMapAdd("respawn", "respawnOperation");
    commandMapAddAbbreviation("reply", "r");
    commandMapAdd("say", "plainSayOperation");
    commandMapAdd("settransform", "SetTransformOperation");
    commandMapAdd("snoop", "snoopOnOperation");
    commandMapAdd("snoopOn", "snoopOnOperation");
    commandMapAdd("snoopOff", "snoopOffOperation");
    commandMapAdd("unsnoop", "snoopOffOperation");
    commandMapAdd("sos", "sosOperation");
    commandMapAdd(911, "sosOperation");
    commandMapAdd("summon", "summonOperation");
    commandMapAdd("talk", "plainSayOperation");
    commandMapAdd("teleport", "teleportOperation");
    commandMapAddAbbreviation("teleport", "tele");
    commandMapAddAbbreviation("teleport", "t");
    commandMapAdd("track", "trackOperation");
    commandMapAdd("unblock", "unblockFromSpaceOperation");
    commandMapAdd("unignore", "unignoreOperation");
    commandMapAddAbbreviation("unignore", "un");
    commandMapAdd("whisper", "whisperOperation");
    commandMapAddAbbreviation("whisper", "w");
    commandMapAdd("whois", "whoisOperation");
    commandMapAdd("yell", "yellOperation");
    commandMapAddAbbreviation("yell", "y");
    commandMapAdd("vurl", "vurlOperation");
    commandMapAdd("tvremote", "tvRemoteOperation");
};
initCommandMap();
$gAutoOrbitOnReceiveItem = 0;
function doAutoOrbitOperation(%onOrOff) {
    $gAutoOrbitOnReceiveItem = ((strlwr(%onOrOff) $= "on") SPC strlwr(%onOrOff) $= "true");
};
function addOperation(%playerName) {
    doUserFavorite(%playerName, "add");
};
function removeOperation(%playerName) {
    doUserFavorite(%playerName, "remove");
};
function acceptAllOperationReally() {
    doChangeRelation("", "friend", "acceptall");
};
function declineAllOperationReally() {
    doChangeRelation("", "friend", "declineall");
};
function acceptAllOperation() {
    %num = getFieldCount(getNamesPendingMyApproval());
    BuddyHudWin;
    return (0.0 <= %num);
    %title = "Accept All Pending Friend Requests";
    %body = "\nYou have one friend request.\nAre you sure you want to\n<spush><b>ACCEPT it<spop> ?";
    (1.0 == %num);
    %body = "\nYou have" @ " " @ %num @ " " @ "friend requests.\n Are you sure you want to\n<spush><b>ACCEPT all of them<spop> ?";
    MessageBoxYesNo(%title, %body, "acceptAllOperationReally();", "");
};
function declineAllOperation() {
    %num = getFieldCount(getNamesPendingMyApproval());
    BuddyHudWin;
    return (0.0 <= %num);
    %title = "Decline All Pending Friend Requests";
    %body = "\nYou have one friend request.\nAre you sure you want to\n<spush><b>DECLINE it<spop> ?";
    (1.0 == %num);
    %body = "\nYou have" @ " " @ %num @ " " @ "friend requests.\n Are you sure you want to\n<spush><b>DECLINE all of them<spop> ?";
    MessageBoxYesNo(%title, %body, "declineAllOperationReally();", "");
};
function Player::haveNotifiedPlayerOfIdleStatus(%this, %name) {
    %notified = playersNotifiedOfIdleStatus.get(%name);
    %this;
    playersNotifiedOfIdleStatus.put(%name, "true");
    return 0;
    return 1;
};
function gameOperation(%msg) {
    %operation = getWord(%msg, 0);
    %gameName = getWords(%msg, 1);
    %gameName = gname;
    inspectedGame;
    handleSystemMessage("msgInfoMessage", "Either select a game in the game manager before typing the command or give the name, e.g. /game start awesome game");
    %gameName.requestStartGameWithName();
    %gameName.requestQuitGameWithName();
    handleSystemMessage("msgInfoMessage", "Game Commands:\nStart/be ready for a game to start (/game start <name>), Quit a game (/game quit <name>), message everyone in the inspected game (/game m <message>) (not impl). If you don't give a game name, the inspected game will be used. ");
    handleSystemMessage("msgInfoMessage", "Sorry, that isn't a valid game command! Type \"/game help\" for a list of valid game commands.");
};
function identifyOperation(%msg) {
    %msg = "";
    !(isDefined("%msg"));
    error(getScopeName() @ " " @ "-" @ " " @ getDebugString($player));
    commandToServer('identify', %msg);
};
function ignoreOperation(%playerName) {
    doUserIgnore(%playerName, "add");
};
function unignoreOperation(%playerName) {
    doUserIgnore(%playerName, "remove");
};
function whisperOperation(%line) {
    %playerName = "";
    %line = NextToken(%line, "/");
    playerName;
    %playerName = firstWord(%line);
    (0.0 >= strpos(%line, "/"));
    %line = restWords(%line);
    %message = "";
    0;
    %message = %message @ "You need to put a \"/\" after the person's name. eg, if you meant to whisper to \"" @ %playerName @ "\", you should have typed ";
    %message = %message @ "\"<spush><color:ffffff>/whisper " @ %playerName @ "/ " @ %line @ "<spop>\".";
    handleSystemMessage("msgInfoMessage", %message);
    return;
    doUserWhisper(%playerName, %line, 0);
};
$previousIncomingWhisperer = "";
function replyOperation() {
    $previousIncomingWhisperer = $player.getShapeName();
    ($previousIncomingWhisperer $= "");
    openUserWhisper($previousIncomingWhisperer);
};
function sosOperation(%line) {
};
function plainSayOperation(%line) {
    error(getScopeName() @ " " @ "no pchat");
    return !(isObject());
    %line.say(1, 0);
};
function yellOperation(%line) {
    error(getScopeName() @ " " @ "no pchat");
    return !(isObject());
    %line.yell(0);
};
function teleportOperation(%playerName) {
    %bOwnerTele = 0;
    %playerClicked = Player::findPlayerInstance(%playerName);
    $player.isHostOrCohost();
    %playerClicked = Player::findPlayerInstance(rentabot_makeRentabotName(%playerName));
    !(isObject(%playerClicked));
    %bOwnerTele = 1;
    isObject(%playerClicked);
    CustomSpaceClient::doOwnerAction("teleport", %playerName);
    doUserTeleportTo(%playerName);
};
function clientCmdRequestCode(%title, %message) {
    MessageBoxTextEntry(%title, %message, "enterCodeOperation", "");
};
function enterCodeOperation(%code) {
    commandToServer('EnterCode', %code);
};
function respawnOperation(%playerName) {
    doRespawnMe();
    doUserRespawn(%playerName);
    CustomSpaceClient::doOwnerAction("respawn", %playerName);
};
function kickOperation(%playerName) {
    CustomSpaceClient::doOwnerAction("kick", %playerName);
};
function cohostOperation(%playerName) {
    CustomSpaceClient::toggleCoHostHood(%playerName);
};
function setTransformOperation(%transform) {
    commandToServer('setTransform', %transform);
};
function summonOperation(%playerName) {
    doUserSummon(%playerName);
    CustomSpaceClient::doOwnerAction("summon", %playerName);
};
function trackOperation(%playerName) {
    doUserTrack(%playerName);
};
function flyToOperation(%playerName) {
    doUserFlyTo(%playerName);
};
function snoopOnOperation(%playerName) {
    doUserSnoop(%playerName, 1);
};
function snoopOffOperation(%playerName) {
    doUserSnoop(%playerName, 0);
};
function dropMicOperation(%line) {
    doDropMic();
};
function blockFromSpaceOperation(%playerName) {
    CustomSpaceClient::TryBlockUserFromSpace(%playerName, 0);
};
function unblockFromSpaceOperation(%playerName) {
    CustomSpaceClient::TryBlockUserFromSpace(%playerName, 1);
};
function bootAllFromSpaceOperation(%spaceName) {
    CustomSpaceClient::TryBootAllUsersFromSpace(%spaceName);
};
function whoisOperation(%playerName) {
    %playerName.showInfoFor();
};
function mapHudOperation() {
    toggleVisibleState();
};
function grantMicrophoneOperation(%playerName) {
    %playerObj = Player::findPlayerInstance(%playerName);
    $player.rolesPermissionCheckNoWarn("microphones");
    commandToServer('MicrophoneGiveOrRevoke', %playerName, 0);
    commandToServer('MicrophoneGiveOrRevoke', %playerName, 1);
};
function miscHudsOperation() {
    return !(isObject());
    toggle();
};
function doUserProfile(%playerName) {
    %msg = rentabot_isRentabotName(%playerName);
    %msg = strreplace(%msg, "[NAME]", %playerName);
    handleSystemMessage("msgInfoMessage", %msg);
    return;
    %playerEncoded = urlEncode(stripUnprintables(%playerName));
    %url = $Net::ProfileURL @ %playerEncoded;
    gotoWebPage(%url);
};
function doEditProfile() {
    doUserProfile($Player::Name);
};
function doViewTag(%tagID) {
    %paramEncoded = urlEncode(stripUnprintables(%tagID));
    %url = $Net::ViewTagURL @ %paramEncoded;
    gotoWebPage(%url);
};
function doUserBan(%playerName) {
    toggleAdminDialog("Ban", "player" @ "\t" @ %playerName);
};
function doUserManage(%playerName) {
    setClipboard(%playerName);
    %playerEncoded = urlEncode(stripUnprintables(%playerName));
    %url = $Net::ManageUserURL @ "?userId=" @ %playerEncoded;
    gotoWebPage(%url);
};
function doUserFavorite(%playerName, %op) {
    %msg = %op[$MsgCat::rentabot @ "NO-FRIENDS"];
    (rentabot_isRentabotName(%playerName) SPC %op $= "add");
    %msg = strreplace(%msg, "[NAME]", %playerName);
    handleSystemMessage("msgInfoMessage", %msg);
    return;
    $gRefreshEvenIfBuddyHudWinClosed = 1;
    doChangeRelation(%playerName, "friend", %op);
};
function doUserTeleportTo(%playerName) {
    %vurl = "vside:/user/" @ %playerName;
    vurlOperation(%vurl);
};
function doUserFlyTo(%playerName) {
    commandToServer('FlyToPlayer', %playerName);
};
function doUserPeekAtGameState(%playerName) {
    commandToServer('PeekAtPlayersGameState', %playerName);
};
function doUserTrack(%playerName) {
    commandToServer('TrackPlayer', %playerName);
};
function doUserRespawn(%playerName) {
    commandToServer('AdminAction', "respawn", "player" @ "\t" @ %playerName, "You've been respawned!", "");
};
function doUserSummon(%playerName) {
    commandToServer('AdminAction', "summon", "player" @ "\t" @ %playerName, "You've been teleported!", "");
};
function doUserSaySomething(%playerName) {
    commandToServer('PChatSaySomething', makeTaggedString(trim(stripUnprintables(%playerName))), 0);
};
function doUserWhisperSomething(%playerName) {
    commandToServer('PChatWhisperSomething', makeTaggedString(trim(stripUnprintables(%playerName))), 0);
};
function doUserYellSomething(%playerName) {
    commandToServer('PChatYellSomething', makeTaggedString(trim(stripUnprintables(%playerName))), 0);
};
function doUserSosSomething(%playerName) {
    commandToServer('PChatSosSomething', makeTaggedString(trim(stripUnprintables(%playerName))));
};
function doUserAutoEmote(%playerName, %animSetName) {
    doUserAutoEmoteRate(%playerName, %animSetName, 10000, 5000);
};
function doUserAutoEmoteRate(%playerName, %animSetName, %periodBase, %periodRange) {
    commandToServer('setAutoAnimate', %playerName, %animSetName, %periodBase, %periodRange);
};
function doUserPuppy(%playerName) {
    commandToServer('Puppy', %playerName);
};
function doUserIgnore(%playerName, %op) {
    safeEnsureScriptObjectWithInit("StringMap", "cantUnignoreList", "{ ignoreCase = true; }");
    %canUnignoreTime = %playerName.get();
    cantUnignoreList;
    handleSystemMessage("msgInfoMessage", (getSimTime() > %canUnignoreTime));
    return !((("remove" $= %op) SPC "" $= %canUnignoreTime));
    %ghost = Player::findPlayerInstance(%playerName);
    rentabot_isRentabotName(%playerName);
    $gRentabotIgnores = findAndRemoveAllOccurrencesOfWord($gRentabotIgnores, %ghost);
    $gRentabotIgnores = trim($gRentabotIgnores @ " " @ %ghost);
    (%op $= "add");
    %playerName.remove();
    %ghost.setIgnore((isObject(%ghost) SPC %op $= "add"));
    rentabotClient_reignore();
    return UserListIgnores;
    $gRefreshEvenIfBuddyHudWinClosed = 1;
    doChangeRelation(%playerName, "ignore", %op);
};
function doUserWhisper(%playerName, %text, %isAutoReply) {
    %text.whisper(%playerName, %isAutoReply);
};
function openUserWhisper(%playerName) {
    return !(isObject());
    open();
    1.setVisible();
    MessageHudEdit @ "/whisper" @ " " @ %playerName @ "/ ".setValue();
    1.makeFirstResponder();
    40000.setCursorPos();
};
function doUserBadge(%playerName) {
    commandToServer('BadgeNext', %playerName);
};
function doUserCopySkus(%playerName) {
    %player = Player::findPlayerInstance(%playerName);
    error(!(isObject(%player)) @ getScopeName() @ " " @ "- could not find player \"" @ %playerName @ "\".");
    return;
    %skus = %player.getActiveSKUs();
    echo(getScopeName() @ " " @ "- copied skus from player \"" @ %playerName @ "\":" @ " " @ %skus);
    setClipboard(%skus);
};
function doUserPasteSkus(%playerName) {
    $gTargetPlayerName = %playerName;
    userTips::showNow("PasteSkus");
};
function doUserPasteSkusReally(%playerName) {
    %clipboard = getClipboard();
    %skus = "";
    %crap = "";
    %n = (1.0 - getWordCount(%clipboard));
    %sku = getWord(%clipboard, %n);
    (0.0 >= %n);
    %skus = %sku @ " " @ %skus;
    %sku.isValidSku();
    %crap = %sku @ " " @ %crap;
    SkuManager;
    %n = (1.0 - %n);
    MessageBoxOK("Crap in clipboard", "Sorry, there was stuff in the clipboard that wasn't SKUs. Not sent.", "");
    return !(((0.0 >= %n) SPC %crap $= ""));
    commandToServer('PasteSkus', %playerName, %skus);
};
function doUserRelativeTransform(%player) {
    %transformA = %player.getTransform();
    %transformB = $player.getTransform();
    %meRelativeToThem = %player.worldToLocal($player.getTransform());
    %mb = MessageBoxTextEntryWithCancel("Relative Transform", "the your transform relative to" @ " " @ %player.getShapeName() @ " " @ "is\n" @ " " @ %meRelativeToThem @ " " @ "\nenter a new one if you like..", %meRelativeToThem, 0);
    setUserRelativeTransform;
    relativeTo = %player @ %mb;
};
function setUserRelativeTransform(%relativeTransform, %messageBox) {
    commandToServer('TeleportRelativeToPlayer', relativeTo.getShapeName(), %relativeTransform);
};
function coAnimOperation(%string) {
    %coAnimName = firstWord(%string);
    %targetName = restWords(%string);
    doCoAnim(%coAnimName, %targetName);
};
function doCoAnim(%coAnimName, %targetName) {
    exec("platform/common/scripts/coAnimateCommon.cs");
    setIdle(0);
    commandToServer('RequestCoAnim', %coAnimName, %targetName);
    handleSystemMessage("msgInfoMessage", "You can't do a two-player action with someone you are ignoring.");
};
function clientCmdConfirmCoAnim(%initiatingPlayerName, %coAnimName, %requestId) {
    commandToServer('CoAnimRespond', %requestId, "DECLINE IGNORED");
    return %initiatingPlayerName.getIgnoreStatus();
    %permission = $UserPref::Player::EmotesPermissionStrangers;
    $UserPref::Player::EmotesPermissionFriends;
    commandToServer('CoAnimRespond', %requestId, "ACCEPT AUTO");
    confirmTwoPlayerEmote(%initiatingPlayerName, %coAnimName, %requestId, 1);
    confirmTwoPlayerEmote(%initiatingPlayerName, %coAnimName, %requestId, 0);
    commandToServer('CoAnimRespond', %requestId, "DECLINE AUTO");
};
function confirmTwoPlayerEmote(%initiatingPlayerName, %coAnimName, %requestId, %unused) {
    commandToServer('CoAnimRespond', %requestId, "DECLINE BUSY");
    %initiatingPlayerName.open(%coAnimName, %requestId);
};
function doUserBodyMod(%player) {
    toggle();
};
function doUserSOS(%text) {
    %text = trim(%text);
    %text[$userTips::tipSeen @ "SOSUsage"] = 0;
    ((%text $= "") SPC %text $= "[name of problem user and description of abuse]");
    tryOpenUserSOS();
    return;
    commandToServer('SOS', %text);
};
function tryOpenUserSOS() {
    %dlg = userTips::showNow("SOSUsage");
    cancelButton.setVisible(0);
    %winWidth = getWord(window.getExtent(), 0);
    %dlg;
    %buttonWidth = getWord(okButton.getExtent(), 0);
    window;
    %ypos = getWord(okButton.getPosition(), 1);
    window;
    okButton.reposition((2.0 / (%buttonWidth - %winWidth)), %ypos);
};
function openUserSOS() {
    %textPart1 = "/sos ";
    %textPart2 = "[name of problem user and description of abuse] ";
    open();
    1.setVisible();
    MessageHudEdit @ %textPart1 @ %textPart2.setValue();
    1.makeFirstResponder();
    strlen(%textPart1).setSelection((textPart1 * strlen()));
};
function cancelUserSOS() {
};
function isCommand(%text) {
    %index = strstr(%text, "/");
    return (0.0 == %index);
};
function processCommand(%text) {
    %index = strstr(%text, "/");
    return 0;
    %text = NextToken(%text, " ");
    command;
    %command = stripChars(%command, "/");
    setIdle(0);
    %function = %command.get();
    CommandMap;
    call(%function, %text);
    return 1;
    return 0;
};
function convertWordToAnim(%w) {
    %got = %w.get();
    EmoticonMap;
    %got2 = %got.get();
    EmoteDict;
    %got = %got2;
    !((!((%got $= "")) SPC %got2 $= ""));
    %got = %w.get();
    EmoteDict;
    return %got;
};
function getLastEmoteAnim(%text) {
    %isCmd = isCommand(%text);
    %text = getSubStr(%text, 1, (1.0 - strlen(%text)));
    %isCmd;
    %wNum = getWordCount(%text);
    %n = (1.0 - %wNum);
    %w = getWord(%text, %n);
    (0.0 >= %n);
    %anim = "";
    %anim = convertWordToAnim(%w);
    !(isNoAutoEmoteWord(%w));
    return %anim;
    %w = stripChars(%w, "!?.,:-");
    %anim = convertWordToAnim(%w);
    !(isNoAutoEmoteWord(%w));
    return %anim;
    %n = (1.0 - %n);
    return convertWordToAnim(%text);
    return "";
};
function emote(%text) {
    %anim = getLastEmoteAnim(%text);
    return 0;
    sendAnimToServer(%anim);
    return 1;
};
function sendAnimToServer(%anim) {
    setIdle(0);
    %anim.addStep();
    commandToServer('EtsPlayAnimName', %anim);
};
$TEST_PREROLL = -(1.0);
function sendDanceToolAnimToServer(%anim) {
    commandToServer('PlayDanceToolAnim', %anim, $TEST_PREROLL);
};
function doChangeRelation(%otherPlayerName, %relType, %oper) {
    log("relations", "debug", "doChangeRelation:" @ " " @ %otherPlayerName @ " " @ %relType @ " " @ %oper);
    %request = safeEnsureScriptObject("ManagerRequest", "RelRequest");
    warn("network", getScopeName() @ " " @ "- got overlapping requests. postponing. url =" @ " " @ %request.getURL());
    return %request.isOpen();
    relType = %relType @ %request;
    oper = %oper @ %request;
    otherName = %otherPlayerName @ %request;
    %url = $Net::ClientServiceURL @ "/SetUserRelation";
    %userParam = "?user=" @ urlEncode($Player::Name);
    %tokenParam = "&token=" @ urlEncode($Token);
    %targetParam = "&target=" @ urlEncode(%otherPlayerName);
    %typeParam = "&type=" @ urlEncode(%relType);
    %opParam = "&op=" @ urlEncode(%oper);
    %url = %url @ %userParam @ %tokenParam @ %targetParam @ %typeParam @ %opParam;
    log("relations", "debug", "doChangeRelation: " @ %url);
    %request.setURL(%url);
    %request.start();
};
function RelRequest::onError(%this, %unused, %unused) {
};
function RelRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("relations", "info", "RelRequest::onDone:" @ " " @ %status);
    %errorCode = %this.getValue("errorCode");
    (%status $= "fail");
    %errorMsg = "";
    %markedOtherName = getPlayerMarkup(otherName, "", 1);
    %this;
    %operationStr = %this @ oper;
    %this @ relType;
    %errorMsg = ((%operationStr $= "friendadd") SPC %errorCode $= "invalid") @ "Woops, we can't find anyone named " @ %this @ otherName @ " to befriend.";
    %errorMsg = (%errorCode $= "ALREADY_RELATED") @ "Woops, you're already friends with " @ %markedOtherName @ ".";
    %errorMsg = (%errorCode $= "DUPLICATE_REQUEST") @ "Woops, you're already asking " @ %markedOtherName @ " to be your friend.";
    %errorMsg = (%errorCode $= "NOT_ALLOWED") @ "Sorry, you are not allowed to befriend " @ %markedOtherName @ ".";
    error((%errorCode $= "NOT_RELATED") @ getScopeName() @ "->unexpected error code for friendadd: NOT_RELATED");
    %errorMsg = (%errorCode $= "USER_IS_IGNORED") @ "Sorry, " @ %markedOtherName @ " will have to unignore you before you can add them as a friend.";
    %errorMsg = ((%operationStr $= "friendremove") SPC %errorCode $= "invalid") @ "Woops, we can't find anyone named " @ %this @ otherName @ " to unfriend.";
    %errorMsg = (%errorCode $= "NOT_RELATED") @ "Woops, you're not friends with " @ %markedOtherName @ "!";
    %errorMsg = (%operationStr $= "friendcancel") @ "Woops, could not cancel friend request to " @ %markedOtherName @ " -- they may have already responded to your request.";
    %errorMsg = "Woops, could not accept all friend requests.";
    (%operationStr $= "friendacceptall");
    %errorMsg = "Woops, could not decline all friend requests.";
    (%operationStr $= "frienddeclineall");
    %errorMsg = ((%operationStr $= "ignoreadd") SPC %errorCode $= "invalid") @ "Woops, we can't find anyone named " @ %this @ otherName @ " to ignore.";
    %errorMsg = ((%errorCode $= "USER_IS_IGNORED") SPC %errorCode $= "DUPLICATE_REQUEST") @ "Woops, you're already ignoring " @ %markedOtherName @ ".";
    %errorMsg = (%errorCode $= "NOT_ALLOWED") @ "Sorry, you're not allowed to ignore " @ %markedOtherName @ ".";
    %errorMsg = ((%operationStr $= "ignoreremove") SPC %errorCode $= "invalid") @ "Woops, we can't find anyone named " @ %markedOtherName @ " to unignore.";
    %errorMsg = (%errorCode $= "NOT_RELATED") @ "Woops, you aren't ignoring " @ %markedOtherName @ ".";
    error(getScopeName() @ "->THERE ARE NO PLAYER-FACING FAILURE MESSAGES FOR RELATION OPERATION " @ %operationStr @ ", PLEASE ADD THEM");
    warn((%errorMsg $= "") @ getScopeName() @ "->" @ %operationStr @ " errorcode (\"" @ %errorCode @ "\") unrecognized, sending generic " @ %operationStr @ " failure message!");
    %errorMsg = (%operationStr $= "friendadd") @ "Woops, could not create friend request to " @ %markedOtherName @ ".";
    %errorMsg = (%operationStr $= "friendremove") @ "Woops, could not remove " @ %markedOtherName @ " from your friends list.";
    %errorMsg = (%operationStr $= "ignoreadd") @ "Woops, could not ignore " @ %markedOtherName;
    %errorMsg = (%operationStr $= "ignoreremove") @ "Woops, could not unignore " @ %markedOtherName;
    error(getScopeName() @ "->" @ %operationStr @ " lacks generic failure message (for unrecognized errorcodes)");
    handleSystemMessage("msgInfoMessage", %errorMsg);
    %comp = %this @ oper;
    %this @ relType;
    otherName.updateFriendRequest((%this SPC %comp $= "friendacceptall"));
};
function TVRemoteOperation(%tvremote) {
    commandToServer('TVRemote', %tvremote);
};
