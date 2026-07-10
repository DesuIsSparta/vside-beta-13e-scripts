function commandMapAdd(%keyword, %functionName) {
    %keyword.put(%functionName);
};
function commandMapAddAbbreviation(%keyword, %abbreviation) {
    %functionName = %keyword.get();
    CommandMap;
    if ((%functionName $= "")) {
        if ((%keyword $= "reply")) {
        }
        warn("commandMapAddAbbreviation: unknown keyword:" @ " " @ %keyword);
    }
    commandMapAdd(%keyword, %functionName);
    CommandAbbreviationMap @ "/" @ %abbreviation.put("/" @ %keyword);
};
function initCommandMap() {
    if (!(isObject())) {
        ignoreCase = CommandMap @ new StringMap(CommandMap) @ 1;
    }
    if (isObject()) {
        add();
    }
    clear();
    if (!(isObject())) {
        ignoreCase = CommandAbbreviationMap @ new StringMap(CommandAbbreviationMap) @ 1;
        CommandMap;
        CommandMap;
    }
    if (isObject()) {
        add();
    }
    clear();
    $gUnidleChatCommands = "code drop helpme help microphone summon tvremote";
    CommandAbbreviationMap;
    if ($ETS::devMode) {
        commandMapAdd("activities", "activitiesOperation");
        commandMapAddAbbreviation("activities", "act");
        commandMapAddAbbreviation("activities", "states");
    }
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
    if ((strlwr(%onOrOff) $= "on")) {
    }
    $gAutoOrbitOnReceiveItem = (strlwr(%onOrOff) $= "true");
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
    if ((0.0 <= %num)) {
        return;
    }
    %title = "Accept All Pending Friend Requests";
    if ((1.0 == %num)) {
        %body = "\nYou have one friend request.\nAre you sure you want to\n<spush><b>ACCEPT it<spop> ?";
    }
    %body = "\nYou have" @ " " @ %num @ " " @ "friend requests.\n Are you sure you want to\n<spush><b>ACCEPT all of them<spop> ?";
    MessageBoxYesNo(%title, %body, "acceptAllOperationReally();", "");
};
function declineAllOperation() {
    %num = getFieldCount(getNamesPendingMyApproval());
    BuddyHudWin;
    if ((0.0 <= %num)) {
        return;
    }
    %title = "Decline All Pending Friend Requests";
    if ((1.0 == %num)) {
        %body = "\nYou have one friend request.\nAre you sure you want to\n<spush><b>DECLINE it<spop> ?";
    }
    %body = "\nYou have" @ " " @ %num @ " " @ "friend requests.\n Are you sure you want to\n<spush><b>DECLINE all of them<spop> ?";
    MessageBoxYesNo(%title, %body, "declineAllOperationReally();", "");
};
function Player::haveNotifiedPlayerOfIdleStatus(%this, %name) {
    %notified = playersNotifiedOfIdleStatus.get(%name);
    %this;
    if ((%notified $= "")) {
        playersNotifiedOfIdleStatus.put(%name, "true");
        return 0;
    }
    return 1;
};
function gameOperation(%msg) {
    %operation = getWord(%msg, 0);
    %gameName = getWords(%msg, 1);
    if ((%gameName $= "")) {
        if (areWeInspecting()) {
            %gameName = gname;
            inspectedGame;
        }
        if (!(gameMgrClient SPC %operation $= "help")) {
            handleSystemMessage("msgInfoMessage", "Either select a game in the game manager before typing the command or give the name, e.g. /game start awesome game");
        }
    }
    if ((gameMgrClient SPC %operation $= "start")) {
        %gameName.requestStartGameWithName();
    }
    if ((gameMgrClient SPC %operation $= "join")) {
    }
    if ((%operation $= "quit")) {
        %gameName.requestQuitGameWithName();
    }
    if ((gameMgrClient SPC %operation $= "m")) {
    }
    if ((%operation $= "help")) {
        handleSystemMessage("msgInfoMessage", "Game Commands:\nStart/be ready for a game to start (/game start <name>), Quit a game (/game quit <name>), message everyone in the inspected game (/game m <message>) (not impl). If you don't give a game name, the inspected game will be used. ");
    }
    handleSystemMessage("msgInfoMessage", "Sorry, that isn't a valid game command! Type \"/game help\" for a list of valid game commands.");
};
function identifyOperation(%msg) {
    if (!(isDefined("%msg"))) {
        %msg = "";
    }
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
    if ((0.0 >= strpos(%line, "/"))) {
        %line = NextToken(%line, "/");
        playerName;
    }
    %playerName = firstWord(%line);
    %line = restWords(%line);
    if (0) {
        %message = "";
        %message = %message @ "You need to put a \"/\" after the person's name. eg, if you meant to whisper to \"" @ %playerName @ "\", you should have typed ";
        %message = %message @ "\"<spush><color:ffffff>/whisper " @ %playerName @ "/ " @ %line @ "<spop>\".";
        handleSystemMessage("msgInfoMessage", %message);
        return;
    }
    doUserWhisper(%playerName, %line, 0);
};
$previousIncomingWhisperer = "";
function replyOperation() {
    if (($previousIncomingWhisperer $= "")) {
        $previousIncomingWhisperer = $player.getShapeName();
    }
    openUserWhisper($previousIncomingWhisperer);
};
function sosOperation(%line) {
};
function plainSayOperation(%line) {
    if (!(isObject())) {
        error(getScopeName() @ " " @ "no pchat");
        return pChat;
    }
    %line.say(1, 0);
};
function yellOperation(%line) {
    if (!(isObject())) {
        error(getScopeName() @ " " @ "no pchat");
        return pChat;
    }
    %line.yell(0);
};
function teleportOperation(%playerName) {
    %bOwnerTele = 0;
    if (!(CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if ($player.isHostOrCohost()) {
        %playerClicked = Player::findPlayerInstance(%playerName);
        if (!(isObject(%playerClicked))) {
            %playerClicked = Player::findPlayerInstance(rentabot_makeRentabotName(%playerName));
        }
        if (isObject(%playerClicked)) {
            %bOwnerTele = 1;
        }
    }
    if (%bOwnerTele) {
        CustomSpaceClient::doOwnerAction("teleport", %playerName);
    }
    doUserTeleportTo(%playerName);
};
function clientCmdRequestCode(%title, %message) {
    MessageBoxTextEntry(%title, %message, "enterCodeOperation", "");
};
function enterCodeOperation(%code) {
    commandToServer('EnterCode', %code);
};
function respawnOperation(%playerName) {
    if ((%playerName $= "")) {
        doRespawnMe();
    }
    if ($player.rolesPermissionCheckNoWarn("manageUsersBasic")) {
        doUserRespawn(%playerName);
    }
    if (!(CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if ($player.isHostOrCohost()) {
        CustomSpaceClient::doOwnerAction("respawn", %playerName);
    }
};
function kickOperation(%playerName) {
    if (!(CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if ($player.isHostOrCohost()) {
        CustomSpaceClient::doOwnerAction("kick", %playerName);
    }
};
function cohostOperation(%playerName) {
    if (!(CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if ($player.isHostOrCohost()) {
        CustomSpaceClient::toggleCoHostHood(%playerName);
    }
};
function setTransformOperation(%transform) {
    commandToServer('setTransform', %transform);
};
function summonOperation(%playerName) {
    if ($player.rolesPermissionCheckNoWarn("manageUsersBasic")) {
        doUserSummon(%playerName);
    }
    if (!(CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if ($player.isHostOrCohost()) {
        CustomSpaceClient::doOwnerAction("summon", %playerName);
    }
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
    if (CustomSpaceClient::isOwner()) {
    }
    if ($player.rolesPermissionCheckNoWarn("microphones")) {
        %playerObj = Player::findPlayerInstance(%playerName);
        if (%playerObj.hasMicrophone()) {
            commandToServer('MicrophoneGiveOrRevoke', %playerName, 0);
        }
        commandToServer('MicrophoneGiveOrRevoke', %playerName, 1);
    }
};
function miscHudsOperation() {
    if (!(isObject())) {
        return geMiscHudsPanel;
    }
    toggle();
};
function doUserProfile(%playerName) {
    if (rentabot_isRentabotName(%playerName)) {
        %msg = ;
        %msg = strreplace(%msg, "[NAME]", %playerName);
        handleSystemMessage("msgInfoMessage", %msg);
        return;
    }
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
    if (rentabot_isRentabotName(%playerName)) {
        if ((%op $= "add")) {
            %msg = %op[$MsgCat::rentabot @ "NO-FRIENDS"];
            %msg = strreplace(%msg, "[NAME]", %playerName);
            handleSystemMessage("msgInfoMessage", %msg);
            return;
        }
    }
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
    if (("remove" $= %op)) {
        safeEnsureScriptObjectWithInit("StringMap", "cantUnignoreList", "{ ignoreCase = true; }");
        %canUnignoreTime = %playerName.get();
        cantUnignoreList;
        if (!("" $= %canUnignoreTime)) {
        }
        if ((getSimTime() > %canUnignoreTime)) {
            handleSystemMessage("msgInfoMessage", );
            return;
        }
    }
    if (rentabot_isRentabotName(%playerName)) {
        %ghost = Player::findPlayerInstance(%playerName);
        $gRentabotIgnores = findAndRemoveAllOccurrencesOfWord($gRentabotIgnores, %ghost);
        if ((%op $= "add")) {
            $gRentabotIgnores = trim($gRentabotIgnores @ " " @ %ghost);
        }
        %playerName.remove();
        if (isObject(%ghost)) {
            %ghost.setIgnore((UserListIgnores SPC %op $= "add"));
        }
        rentabotClient_reignore();
        return;
    }
    $gRefreshEvenIfBuddyHudWinClosed = 1;
    doChangeRelation(%playerName, "ignore", %op);
};
function doUserWhisper(%playerName, %text, %isAutoReply) {
    if (isObject()) {
        %text.whisper(%playerName, %isAutoReply);
    }
};
function openUserWhisper(%playerName) {
    if (!(isObject())) {
        return pChat;
    }
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
    if (!(isObject(%player))) {
        error(getScopeName() @ " " @ "- could not find player \"" @ %playerName @ "\".");
        return;
    }
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
    if ((0.0 >= %n)) {
        %sku = getWord(%clipboard, %n);
        if (%sku.isValidSku()) {
            %skus = %sku @ " " @ %skus;
            SkuManager;
        }
        %crap = %sku @ " " @ %crap;
        %n = (1.0 - %n);
    }
    if (!((0.0 >= %n) SPC %crap $= "")) {
        MessageBoxOK("Crap in clipboard", "Sorry, there was stuff in the clipboard that wasn't SKUs. Not sent.", "");
        return;
    }
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
    if ($ETS::devMode) {
    }
    if ($StandAlone) {
        exec("platform/common/scripts/coAnimateCommon.cs");
    }
    if (!(%targetName.getIgnoreStatus())) {
        setIdle(0);
        commandToServer('RequestCoAnim', %coAnimName, %targetName);
    }
    handleSystemMessage("msgInfoMessage", "You can't do a two-player action with someone you are ignoring.");
};
function clientCmdConfirmCoAnim(%initiatingPlayerName, %coAnimName, %requestId) {
    if (%initiatingPlayerName.getIgnoreStatus()) {
        commandToServer('CoAnimRespond', %requestId, "DECLINE IGNORED");
        return BuddyHudWin;
    }
    if ((BuddyHudWin SPC %initiatingPlayerName.getFriendStatus() $= "friends")) {
    }
    %permission = $UserPref::Player::EmotesPermissionStrangers;
    $UserPref::Player::EmotesPermissionFriends;
    if ((0.0 == %permission)) {
        if (!(isIdle())) {
            commandToServer('CoAnimRespond', %requestId, "ACCEPT AUTO");
        }
        confirmTwoPlayerEmote(%initiatingPlayerName, %coAnimName, %requestId, 1);
    }
    if ((1.0 == %permission)) {
        confirmTwoPlayerEmote(%initiatingPlayerName, %coAnimName, %requestId, 0);
    }
    commandToServer('CoAnimRespond', %requestId, "DECLINE AUTO");
};
function confirmTwoPlayerEmote(%initiatingPlayerName, %coAnimName, %requestId, %unused) {
    if (isVisible()) {
        commandToServer('CoAnimRespond', %requestId, "DECLINE BUSY");
    }
    %initiatingPlayerName.open(%coAnimName, %requestId);
};
function doUserBodyMod(%player) {
    toggle();
};
function doUserSOS(%text) {
    %text = trim(%text);
    if ((%text $= "")) {
    }
    if ((%text $= "[name of problem user and description of abuse]")) {
        %text[$userTips::tipSeen @ "SOSUsage"] = 0;
        tryOpenUserSOS();
        return;
    }
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
    if ((0.0 != %index)) {
        return 0;
    }
    %text = NextToken(%text, " ");
    command;
    %command = stripChars(%command, "/");
    if (!(%command $= "")) {
        if ((-(1.0) != findWord($gUnidleChatCommands, %command))) {
            setIdle(0);
        }
        %function = %command.get();
        CommandMap;
        if (!(%function $= "")) {
            call(%function, %text);
            return 1;
        }
    }
    return 0;
};
function convertWordToAnim(%w) {
    %got = %w.get();
    EmoticonMap;
    if (!(%got $= "")) {
        %got2 = %got.get();
        EmoteDict;
        if (!(%got2 $= "")) {
            %got = %got2;
        }
    }
    %got = %w.get();
    EmoteDict;
    return %got;
};
function getLastEmoteAnim(%text) {
    %isCmd = isCommand(%text);
    if (%isCmd) {
        %text = getSubStr(%text, 1, (1.0 - strlen(%text)));
    }
    %wNum = getWordCount(%text);
    %n = (1.0 - %wNum);
    if ((0.0 >= %n)) {
        %w = getWord(%text, %n);
        %anim = "";
        if (%isCmd) {
        }
        if (!(isNoAutoEmoteWord(%w))) {
            %anim = convertWordToAnim(%w);
        }
        if (!(%anim $= "")) {
            return %anim;
        }
        %w = stripChars(%w, "!?.,:-");
        if (%isCmd) {
        }
        if (!(isNoAutoEmoteWord(%w))) {
            %anim = convertWordToAnim(%w);
        }
        if (!(%anim $= "")) {
            return %anim;
        }
        %n = (1.0 - %n);
    }
    if (%isCmd) {
    }
    if (!(isNoAutoEmoteWord(%w))) {
        return convertWordToAnim(%text);
    }
    return "";
};
function emote(%text) {
    %anim = getLastEmoteAnim(%text);
    if ((%anim $= "")) {
        return 0;
    }
    sendAnimToServer(%anim);
    return 1;
};
function sendAnimToServer(%anim) {
    setIdle(0);
    if (isObject()) {
        %anim.addStep();
    }
    commandToServer('EtsPlayAnimName', %anim);
};
$TEST_PREROLL = -(1.0);
function sendDanceToolAnimToServer(%anim) {
    commandToServer('PlayDanceToolAnim', %anim, $TEST_PREROLL);
};
function doChangeRelation(%otherPlayerName, %relType, %oper) {
    log("relations", "debug", "doChangeRelation:" @ " " @ %otherPlayerName @ " " @ %relType @ " " @ %oper);
    %request = safeEnsureScriptObject("ManagerRequest", "RelRequest");
    if (%request.isOpen()) {
        warn("network", getScopeName() @ " " @ "- got overlapping requests. postponing. url =" @ " " @ %request.getURL());
        return;
    }
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
    if ((%status $= "fail")) {
        %errorCode = %this.getValue("errorCode");
        %errorMsg = "";
        %markedOtherName = getPlayerMarkup(otherName, "", 1);
        %this;
        %operationStr = %this @ oper;
        %this @ relType;
        if ((%operationStr $= "friendadd")) {
            if ((%errorCode $= "invalid")) {
                %errorMsg = "Woops, we can't find anyone named " @ %this @ otherName @ " to befriend.";
            }
            if ((%errorCode $= "ALREADY_RELATED")) {
                %errorMsg = "Woops, you're already friends with " @ %markedOtherName @ ".";
            }
            if ((%errorCode $= "DUPLICATE_REQUEST")) {
                %errorMsg = "Woops, you're already asking " @ %markedOtherName @ " to be your friend.";
            }
            if ((%errorCode $= "NOT_ALLOWED")) {
                %errorMsg = "Sorry, you are not allowed to befriend " @ %markedOtherName @ ".";
            }
            if ((%errorCode $= "NOT_RELATED")) {
                error(getScopeName() @ "->unexpected error code for friendadd: NOT_RELATED");
            }
            if ((%errorCode $= "USER_IS_IGNORED")) {
                %errorMsg = "Sorry, " @ %markedOtherName @ " will have to unignore you before you can add them as a friend.";
            }
        }
        if ((%operationStr $= "friendremove")) {
            if ((%errorCode $= "invalid")) {
                %errorMsg = "Woops, we can't find anyone named " @ %this @ otherName @ " to unfriend.";
            }
            if ((%errorCode $= "NOT_RELATED")) {
                %errorMsg = "Woops, you're not friends with " @ %markedOtherName @ "!";
            }
        }
        if ((%operationStr $= "friendcancel")) {
            %errorMsg = "Woops, could not cancel friend request to " @ %markedOtherName @ " -- they may have already responded to your request.";
        }
        if ((%operationStr $= "friendacceptall")) {
            %errorMsg = "Woops, could not accept all friend requests.";
        }
        if ((%operationStr $= "frienddeclineall")) {
            %errorMsg = "Woops, could not decline all friend requests.";
        }
        if ((%operationStr $= "ignoreadd")) {
            if ((%errorCode $= "invalid")) {
                %errorMsg = "Woops, we can't find anyone named " @ %this @ otherName @ " to ignore.";
            }
            if ((%errorCode $= "USER_IS_IGNORED")) {
            }
            if ((%errorCode $= "DUPLICATE_REQUEST")) {
                %errorMsg = "Woops, you're already ignoring " @ %markedOtherName @ ".";
            }
            if ((%errorCode $= "NOT_ALLOWED")) {
                %errorMsg = "Sorry, you're not allowed to ignore " @ %markedOtherName @ ".";
            }
        }
        if ((%operationStr $= "ignoreremove")) {
            if ((%errorCode $= "invalid")) {
                %errorMsg = "Woops, we can't find anyone named " @ %markedOtherName @ " to unignore.";
            }
            if ((%errorCode $= "NOT_RELATED")) {
                %errorMsg = "Woops, you aren't ignoring " @ %markedOtherName @ ".";
            }
        }
        error(getScopeName() @ "->THERE ARE NO PLAYER-FACING FAILURE MESSAGES FOR RELATION OPERATION " @ %operationStr @ ", PLEASE ADD THEM");
        if ((%errorMsg $= "")) {
            warn(getScopeName() @ "->" @ %operationStr @ " errorcode (\"" @ %errorCode @ "\") unrecognized, sending generic " @ %operationStr @ " failure message!");
            if ((%operationStr $= "friendadd")) {
                %errorMsg = "Woops, could not create friend request to " @ %markedOtherName @ ".";
            }
            if ((%operationStr $= "friendremove")) {
                %errorMsg = "Woops, could not remove " @ %markedOtherName @ " from your friends list.";
            }
            if ((%operationStr $= "ignoreadd")) {
                %errorMsg = "Woops, could not ignore " @ %markedOtherName;
            }
            if ((%operationStr $= "ignoreremove")) {
                %errorMsg = "Woops, could not unignore " @ %markedOtherName;
            }
            error(getScopeName() @ "->" @ %operationStr @ " lacks generic failure message (for unrecognized errorcodes)");
        }
        if (!(%errorMsg $= "")) {
            handleSystemMessage("msgInfoMessage", %errorMsg);
        }
    }
    if ((%status $= "success")) {
        %comp = %this @ oper;
        %this @ relType;
        if ((%comp $= "friendacceptall")) {
        }
        if ((%comp $= "frienddeclineall")) {
        }
        if ((%this SPC otherName $= "")) {
            otherName.updateFriendRequest((%this SPC %comp $= "friendacceptall"));
        }
    }
};
function TVRemoteOperation(%tvremote) {
    commandToServer('TVRemote', %tvremote);
};
