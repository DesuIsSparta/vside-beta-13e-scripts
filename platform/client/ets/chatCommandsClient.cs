function commandMapAdd(%keyword, %functionName)
{
    CommandMap.put(%keyword, %functionName);
    return ;
}
function commandMapAddAbbreviation(%keyword, %abbreviation)
{
    %functionName = CommandMap.get(%keyword);
    if (%functionName $= "")
    {
        if (%keyword $= "reply")
        {
        }
        else
        {
            warn("commandMapAddAbbreviation: unknown keyword:" SPC %keyword);
        }
    }
    else
    {
        commandMapAdd(%keyword, %functionName);
    }
    CommandAbbreviationMap.put("/" @ %abbreviation, "/" @ %keyword);
    return ;
}
function initCommandMap()
{
    if (!isObject(CommandMap))
    {
    }
    new StringMap(CommandMap);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(CommandMap);
    }
    CommandMap.clear();
    if (!isObject(CommandAbbreviationMap))
    {
    }
    new StringMap(CommandAbbreviationMap);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(CommandAbbreviationMap);
    }
    CommandAbbreviationMap.clear();
    $gUnidleChatCommands = "code drop helpme help microphone summon tvremote";
    if ($ETS::devMode)
    {
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
    return ;
}
initCommandMap();
$gAutoOrbitOnReceiveItem = 0;
function doAutoOrbitOperation(%onOrOff)
{
    $gAutoOrbitOnReceiveItem = (strlwr(%onOrOff) $= "on") || (strlwr(%onOrOff) $= "true");
    return ;
}
function addOperation(%playerName)
{
    doUserFavorite(%playerName, "add");
    return ;
}
function removeOperation(%playerName)
{
    doUserFavorite(%playerName, "remove");
    return ;
}
function acceptAllOperationReally()
{
    doChangeRelation("", "friend", "acceptall");
    return ;
}
function declineAllOperationReally()
{
    doChangeRelation("", "friend", "declineall");
    return ;
}
function acceptAllOperation()
{
    %num = getFieldCount(BuddyHudWin.getNamesPendingMyApproval());
    if (%num <= 0)
    {
        return ;
    }
    %title = "Accept All Pending Friend Requests";
    if (%num == 1)
    {
        %body = "\nYou have one friend request.\nAre you sure you want to\n<spush><b>ACCEPT it<spop> ?";
    }
    else
    {
        %body = "\nYou have" SPC %num SPC "friend requests.\n Are you sure you want to\n<spush><b>ACCEPT all of them<spop> ?";
    }
    MessageBoxYesNo(%title, %body, "acceptAllOperationReally();", "");
    return ;
}
function declineAllOperation()
{
    %num = getFieldCount(BuddyHudWin.getNamesPendingMyApproval());
    if (%num <= 0)
    {
        return ;
    }
    %title = "Decline All Pending Friend Requests";
    if (%num == 1)
    {
        %body = "\nYou have one friend request.\nAre you sure you want to\n<spush><b>DECLINE it<spop> ?";
    }
    else
    {
        %body = "\nYou have" SPC %num SPC "friend requests.\n Are you sure you want to\n<spush><b>DECLINE all of them<spop> ?";
    }
    MessageBoxYesNo(%title, %body, "declineAllOperationReally();", "");
    return ;
}
function Player::haveNotifiedPlayerOfIdleStatus(%this, %name)
{
    %notified = %this.playersNotifiedOfIdleStatus.get(%name);
    if (%notified $= "")
    {
        %this.playersNotifiedOfIdleStatus.put(%name, "true");
        return 0;
    }
    return 1;
}
function gameOperation(%msg)
{
    %operation = getWord(%msg, 0);
    %gameName = getWords(%msg, 1);
    if (%gameName $= "")
    {
        if (gameMgrClient.areWeInspecting())
        {
            %gameName = gameMgrClient.inspectedGame.gname;
        }
        else
        {
            if (!(%operation $= "help"))
            {
                handleSystemMessage("msgInfoMessage", "Either select a game in the game manager before typing the command or give the name, e.g. /game start awesome game");
            }
        }
    }
    if (%operation $= "start")
    {
        gameMgrClient.requestStartGameWithName(%gameName);
    }
    else
    {
        if (%operation $= "join")
        {
        }
        else
        {
            if (%operation $= "quit")
            {
                gameMgrClient.requestQuitGameWithName(%gameName);
            }
            else
            {
                if (%operation $= "m")
                {
                }
                else
                {
                    if (%operation $= "help")
                    {
                        handleSystemMessage("msgInfoMessage", "Game Commands:\nStart/be ready for a game to start (/game start <name>), Quit a game (/game quit <name>), message everyone in the inspected game (/game m <message>) (not impl). If you don\'t give a game name, the inspected game will be used. ");
                    }
                    else
                    {
                        handleSystemMessage("msgInfoMessage", "Sorry, that isn\'t a valid game command! Type \"/game help\" for a list of valid game commands.");
                    }
                }
            }
        }
    }
    return ;
}
function identifyOperation(%msg)
{
    if (!isDefined("%msg"))
    {
        %msg = "";
    }
    error(getScopeName() SPC "-" SPC getDebugString($player));
    commandToServer('identify', %msg);
    return ;
}
function ignoreOperation(%playerName)
{
    doUserIgnore(%playerName, "add");
    return ;
}
function unignoreOperation(%playerName)
{
    doUserIgnore(%playerName, "remove");
    return ;
}
function whisperOperation(%line)
{
    %playerName = "";
    if (strpos(%line, "/") >= 0)
    {
        %line = NextToken(%line, playerName, "/");
    }
    else
    {
        %playerName = firstWord(%line);
        %line = restWords(%line);
        if (0)
        {
            %message = "";
            %message = %message @ "You need to put a \"/\" after the person\'s name. eg, if you meant to whisper to \"" @ %playerName @ "\", you should have typed ";
            %message = %message @ "\"<spush><color:ffffff>/whisper " @ %playerName @ "/ " @ %line @ "<spop>\".";
            handleSystemMessage("msgInfoMessage", %message);
            return ;
        }
    }
    doUserWhisper(%playerName, %line, 0);
    return ;
}
$previousIncomingWhisperer = "";
function replyOperation()
{
    if ($previousIncomingWhisperer $= "")
    {
        $previousIncomingWhisperer = $player.getShapeName();
    }
    openUserWhisper($previousIncomingWhisperer);
    return ;
}
function sosOperation(%line)
{
    return ;
}
function plainSayOperation(%line)
{
    if (!isObject(pChat))
    {
        error(getScopeName() SPC "no pchat");
        return ;
    }
    pChat.say(%line, 1, 0);
    return ;
}
function yellOperation(%line)
{
    if (!isObject(pChat))
    {
        error(getScopeName() SPC "no pchat");
        return ;
    }
    pChat.yell(%line, 0);
    return ;
}
function teleportOperation(%playerName)
{
    %bOwnerTele = 0;
    if (!((CustomSpaceClient::GetSpaceImIn() $= "")) && $player.isHostOrCohost())
    {
        %playerClicked = Player::findPlayerInstance(%playerName);
        if (!isObject(%playerClicked))
        {
            %playerClicked = Player::findPlayerInstance(rentabot_makeRentabotName(%playerName));
        }
        if (isObject(%playerClicked))
        {
            %bOwnerTele = 1;
        }
    }
    if (%bOwnerTele)
    {
        CustomSpaceClient::doOwnerAction("teleport", %playerName);
    }
    else
    {
        doUserTeleportTo(%playerName);
    }
    return ;
}
function clientCmdRequestCode(%title, %message)
{
    MessageBoxTextEntry(%title, %message, "enterCodeOperation", "");
    return ;
}
function enterCodeOperation(%code)
{
    commandToServer('EnterCode', %code);
    return ;
}
function respawnOperation(%playerName)
{
    if (%playerName $= "")
    {
        doRespawnMe();
    }
    else
    {
        if ($player.rolesPermissionCheckNoWarn("manageUsersBasic"))
        {
            doUserRespawn(%playerName);
        }
        else
        {
            if (!((CustomSpaceClient::GetSpaceImIn() $= "")) && $player.isHostOrCohost())
            {
                CustomSpaceClient::doOwnerAction("respawn", %playerName);
            }
        }
    }
    return ;
}
function kickOperation(%playerName)
{
    if (!((CustomSpaceClient::GetSpaceImIn() $= "")) && $player.isHostOrCohost())
    {
        CustomSpaceClient::doOwnerAction("kick", %playerName);
    }
    return ;
}
function cohostOperation(%playerName)
{
    if (!((CustomSpaceClient::GetSpaceImIn() $= "")) && $player.isHostOrCohost())
    {
        CustomSpaceClient::toggleCoHostHood(%playerName);
    }
    return ;
}
function setTransformOperation(%transform)
{
    commandToServer('setTransform', %transform);
    return ;
}
function summonOperation(%playerName)
{
    if ($player.rolesPermissionCheckNoWarn("manageUsersBasic"))
    {
        doUserSummon(%playerName);
    }
    else
    {
        if (!((CustomSpaceClient::GetSpaceImIn() $= "")) && $player.isHostOrCohost())
        {
            CustomSpaceClient::doOwnerAction("summon", %playerName);
        }
    }
    return ;
}
function trackOperation(%playerName)
{
    doUserTrack(%playerName);
    return ;
}
function flyToOperation(%playerName)
{
    doUserFlyTo(%playerName);
    return ;
}
function snoopOnOperation(%playerName)
{
    doUserSnoop(%playerName, 1);
    return ;
}
function snoopOffOperation(%playerName)
{
    doUserSnoop(%playerName, 0);
    return ;
}
function dropMicOperation(%line)
{
    doDropMic();
    return ;
}
function blockFromSpaceOperation(%playerName)
{
    CustomSpaceClient::TryBlockUserFromSpace(%playerName, 0);
    return ;
}
function unblockFromSpaceOperation(%playerName)
{
    CustomSpaceClient::TryBlockUserFromSpace(%playerName, 1);
    return ;
}
function bootAllFromSpaceOperation(%spaceName)
{
    CustomSpaceClient::TryBootAllUsersFromSpace(%spaceName);
    return ;
}
function whoisOperation(%playerName)
{
    InfoPopupDlg.showInfoFor(%playerName);
    return ;
}
function mapHudOperation()
{
    toggleVisibleState(geLocalMapContainer);
    return ;
}
function grantMicrophoneOperation(%playerName)
{
    if (CustomSpaceClient::isOwner() && $player.rolesPermissionCheckNoWarn("microphones"))
    {
        %playerObj = Player::findPlayerInstance(%playerName);
        if (%playerObj.hasMicrophone())
        {
            commandToServer('MicrophoneGiveOrRevoke', %playerName, 0);
        }
        else
        {
            commandToServer('MicrophoneGiveOrRevoke', %playerName, 1);
        }
    }
    return ;
}
function miscHudsOperation()
{
    if (!isObject(geMiscHudsPanel))
    {
        return ;
    }
    geMiscHudsPanel.toggle();
    return ;
}
function doUserProfile(%playerName)
{
    if (rentabot_isRentabotName(%playerName))
    {
        %msg = $MsgCat::rentabot["NO-PROFILE"];
        %msg = strreplace(%msg, "[NAME]", %playerName);
        handleSystemMessage("msgInfoMessage", %msg);
        return ;
    }
    %playerEncoded = urlEncode(stripUnprintables(%playerName));
    %url = $Net::ProfileURL @ %playerEncoded;
    gotoWebPage(%url);
    return ;
}
function doEditProfile()
{
    doUserProfile($Player::Name);
    return ;
}
function doViewTag(%tagID)
{
    %paramEncoded = urlEncode(stripUnprintables(%tagID));
    %url = $Net::ViewTagURL @ %paramEncoded;
    gotoWebPage(%url);
    return ;
}
function doUserBan(%playerName)
{
    toggleAdminDialog("Ban", "player" @ "\t" @ %playerName);
    return ;
}
function doUserManage(%playerName)
{
    setClipboard(%playerName);
    %playerEncoded = urlEncode(stripUnprintables(%playerName));
    %url = $Net::ManageUserURL @ "?userId=" @ %playerEncoded;
    gotoWebPage(%url);
    return ;
}
function doUserFavorite(%playerName, %op)
{
    if (rentabot_isRentabotName(%playerName))
    {
        if (%op $= "add")
        {
            %msg = $MsgCat::rentabot["NO-FRIENDS"];
            %msg = strreplace(%msg, "[NAME]", %playerName);
            handleSystemMessage("msgInfoMessage", %msg);
            return ;
        }
    }
    $gRefreshEvenIfBuddyHudWinClosed = 1;
    doChangeRelation(%playerName, "friend", %op);
    return ;
}
function doUserTeleportTo(%playerName)
{
    %vurl = "vside:/user/" @ %playerName;
    vurlOperation(%vurl);
    return ;
}
function doUserFlyTo(%playerName)
{
    commandToServer('FlyToPlayer', %playerName);
    return ;
}
function doUserPeekAtGameState(%playerName)
{
    commandToServer('PeekAtPlayersGameState', %playerName);
    return ;
}
function doUserTrack(%playerName)
{
    commandToServer('TrackPlayer', %playerName);
    return ;
}
function doUserRespawn(%playerName)
{
    commandToServer('AdminAction', "respawn", "player" TAB %playerName, "You\'ve been respawned!", "");
    return ;
}
function doUserSummon(%playerName)
{
    commandToServer('AdminAction', "summon", "player" TAB %playerName, "You\'ve been teleported!", "");
    return ;
}
function doUserSaySomething(%playerName)
{
    commandToServer('PChatSaySomething', makeTaggedString(trim(stripUnprintables(%playerName))), 0);
    return ;
}
function doUserWhisperSomething(%playerName)
{
    commandToServer('PChatWhisperSomething', makeTaggedString(trim(stripUnprintables(%playerName))), 0);
    return ;
}
function doUserYellSomething(%playerName)
{
    commandToServer('PChatYellSomething', makeTaggedString(trim(stripUnprintables(%playerName))), 0);
    return ;
}
function doUserSosSomething(%playerName)
{
    commandToServer('PChatSosSomething', makeTaggedString(trim(stripUnprintables(%playerName))));
    return ;
}
function doUserAutoEmote(%playerName, %animSetName)
{
    doUserAutoEmoteRate(%playerName, %animSetName, 10000, 5000);
    return ;
}
function doUserAutoEmoteRate(%playerName, %animSetName, %periodBase, %periodRange)
{
    commandToServer('setAutoAnimate', %playerName, %animSetName, %periodBase, %periodRange);
    return ;
}
function doUserPuppy(%playerName)
{
    commandToServer('Puppy', %playerName);
    return ;
}
function doUserIgnore(%playerName, %op)
{
    if ("remove" $= %op)
    {
        safeEnsureScriptObjectWithInit("StringMap", "cantUnignoreList", "{ ignoreCase = true; }");
        %canUnignoreTime = cantUnignoreList.get(%playerName);
        if (!(("" $= %canUnignoreTime)) && (%canUnignoreTime > getSimTime()))
        {
            handleSystemMessage("msgInfoMessage", $MsgCat::abuse["WAIT-TO-UNIGNORE"]);
            return ;
        }
    }
    if (rentabot_isRentabotName(%playerName))
    {
        %ghost = Player::findPlayerInstance(%playerName);
        $gRentabotIgnores = findAndRemoveAllOccurrencesOfWord($gRentabotIgnores, %ghost);
        if (%op $= "add")
        {
            $gRentabotIgnores = trim($gRentabotIgnores SPC %ghost);
        }
        else
        {
            UserListIgnores.remove(%playerName);
        }
        if (isObject(%ghost))
        {
            %ghost.setIgnore(%op $= "add");
        }
        rentabotClient_reignore();
        return ;
    }
    $gRefreshEvenIfBuddyHudWinClosed = 1;
    doChangeRelation(%playerName, "ignore", %op);
    return ;
}
function doUserWhisper(%playerName, %text, %isAutoReply)
{
    if (isObject(pChat))
    {
        pChat.whisper(%text, %playerName, %isAutoReply);
    }
    return ;
}
function openUserWhisper(%playerName)
{
    if (!isObject(pChat))
    {
        return ;
    }
    MessageHud.open();
    MessageHud.setVisible(1);
    MessageHudEdit.setValue("/whisper" SPC %playerName @ "/ ");
    MessageHudEdit.makeFirstResponder(1);
    MessageHudEdit.setCursorPos(40000);
    return ;
}
function doUserBadge(%playerName)
{
    commandToServer('BadgeNext', %playerName);
    return ;
}
function doUserCopySkus(%playerName)
{
    %player = Player::findPlayerInstance(%playerName);
    if (!isObject(%player))
    {
        error(getScopeName() SPC "- could not find player \"" @ %playerName @ "\".");
        return ;
    }
    %skus = %player.getActiveSKUs();
    echo(getScopeName() SPC "- copied skus from player \"" @ %playerName @ "\":" SPC %skus);
    setClipboard(%skus);
    return ;
}
function doUserPasteSkus(%playerName)
{
    $gTargetPlayerName = %playerName;
    userTips::showNow("PasteSkus");
    return ;
}
function doUserPasteSkusReally(%playerName)
{
    %clipboard = getClipboard();
    %skus = "";
    %crap = "";
    %n = getWordCount(%clipboard) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%clipboard, %n);
        if (SkuManager.isValidSku(%sku))
        {
            %skus = %sku SPC %skus;
        }
        else
        {
            %crap = %sku SPC %crap;
        }
        %n = %n - 1;
    }
    if (!(%crap $= ""))
    {
        MessageBoxOK("Crap in clipboard", "Sorry, there was stuff in the clipboard that wasn\'t SKUs. Not sent.", "");
        return ;
    }
    commandToServer('PasteSkus', %playerName, %skus);
    return ;
}
function doUserRelativeTransform(%player)
{
    %transformA = %player.getTransform();
    %transformB = $player.getTransform();
    %meRelativeToThem = %player.worldToLocal($player.getTransform());
    %mb = MessageBoxTextEntryWithCancel("Relative Transform", "the your transform relative to" SPC %player.getShapeName() SPC "is\n" SPC %meRelativeToThem SPC "\nenter a new one if you like..", setUserRelativeTransform, %meRelativeToThem, 0);
    %mb.relativeTo = %player;
    return ;
}
function setUserRelativeTransform(%relativeTransform, %messageBox)
{
    commandToServer('TeleportRelativeToPlayer', %messageBox.relativeTo.getShapeName(), %relativeTransform);
    return ;
}
function coAnimOperation(%string)
{
    %coAnimName = firstWord(%string);
    %targetName = restWords(%string);
    doCoAnim(%coAnimName, %targetName);
    return ;
}
function doCoAnim(%coAnimName, %targetName)
{
    if ($ETS::devMode && $StandAlone)
    {
        exec("platform/common/scripts/coAnimateCommon.cs");
    }
    if (!BuddyHudWin.getIgnoreStatus(%targetName))
    {
        setIdle(0);
        commandToServer('RequestCoAnim', %coAnimName, %targetName);
    }
    else
    {
        handleSystemMessage("msgInfoMessage", "You can\'t do a two-player action with someone you are ignoring.");
    }
    return ;
}
function clientCmdConfirmCoAnim(%initiatingPlayerName, %coAnimName, %requestId)
{
    if (BuddyHudWin.getIgnoreStatus(%initiatingPlayerName))
    {
        commandToServer('CoAnimRespond', %requestId, "DECLINE IGNORED");
        return ;
    }
    %permission = BuddyHudWin.getFriendStatus(%initiatingPlayerName) $= "friends" ? $UserPref::Player::EmotesPermissionFriends : $UserPref::Player::EmotesPermissionStrangers;
    if (%permission == 0)
    {
        if (!isIdle())
        {
            commandToServer('CoAnimRespond', %requestId, "ACCEPT AUTO");
        }
        else
        {
            confirmTwoPlayerEmote(%initiatingPlayerName, %coAnimName, %requestId, 1);
        }
    }
    else
    {
        if (%permission == 1)
        {
            confirmTwoPlayerEmote(%initiatingPlayerName, %coAnimName, %requestId, 0);
        }
        else
        {
            commandToServer('CoAnimRespond', %requestId, "DECLINE AUTO");
        }
    }
    return ;
}
function confirmTwoPlayerEmote(%initiatingPlayerName, %coAnimName, %requestId, %unused)
{
    if (geTwoPlayerEmotesConfirmPanel.isVisible())
    {
        commandToServer('CoAnimRespond', %requestId, "DECLINE BUSY");
    }
    else
    {
        geTwoPlayerEmotesConfirmPanel.open(%initiatingPlayerName, %coAnimName, %requestId);
    }
    return ;
}
function doUserBodyMod(%player)
{
    bodyModPanel.toggle();
    return ;
}
$userTips::tipSeen["SOSUsage"] = 0;
function doUserSOS(%text)
{
    %text = trim(%text);
    if ((%text $= "") && (%text $= "[name of problem user and description of abuse]"))
    {
        $userTips::tipSeen["SOSUsage"] = 0;
        tryOpenUserSOS();
        return ;
    }
    commandToServer('SOS', %text);
    return ;
}
function tryOpenUserSOS()
{
    %dlg = userTips::showNow("SOSUsage");
    %dlg.window.cancelButton.setVisible(0);
    %winWidth = getWord(%dlg.window.getExtent(), 0);
    %buttonWidth = getWord(%dlg.window.okButton.getExtent(), 0);
    %ypos = getWord(%dlg.window.okButton.getPosition(), 1);
    %dlg.window.okButton.reposition((%winWidth - %buttonWidth) / 2, %ypos);
    return ;
}
function openUserSOS()
{
    %textPart1 = "/sos ";
    %textPart2 = "[name of problem user and description of abuse] ";
    MessageHud.open();
    MessageHud.setVisible(1);
    MessageHudEdit.setValue(%textPart1 @ %textPart2);
    MessageHudEdit.makeFirstResponder(1);
    MessageHudEdit.setSelection(strlen(%textPart1), strlen(textPart1) * strlen(%textPart2));
    return ;
}
function cancelUserSOS()
{
    return ;
}
function isCommand(%text)
{
    %index = strstr(%text, "/");
    return %index == 0;
}
function processCommand(%text)
{
    %index = strstr(%text, "/");
    if (%index != 0)
    {
        return 0;
    }
    %text = NextToken(%text, command, " ");
    %command = stripChars(%command, "/");
    if (!(%command $= ""))
    {
        if (findWord($gUnidleChatCommands, %command) != -1)
        {
            setIdle(0);
        }
        %function = CommandMap.get(%command);
        if (!(%function $= ""))
        {
            call(%function, %text);
            return 1;
        }
    }
    return 0;
}
function convertWordToAnim(%w)
{
    %got = EmoticonMap.get(%w);
    if (!(%got $= ""))
    {
        %got2 = EmoteDict.get(%got);
        if (!(%got2 $= ""))
        {
            %got = %got2;
        }
    }
    else
    {
        %got = EmoteDict.get(%w);
    }
    return %got;
}
function getLastEmoteAnim(%text)
{
    %isCmd = isCommand(%text);
    if (%isCmd)
    {
        %text = getSubStr(%text, 1, strlen(%text) - 1);
    }
    %wNum = getWordCount(%text);
    %n = %wNum - 1;
    while (%n >= 0)
    {
        %w = getWord(%text, %n);
        %anim = "";
        if (%isCmd && !isNoAutoEmoteWord(%w))
        {
            %anim = convertWordToAnim(%w);
        }
        if (!(%anim $= ""))
        {
            return %anim;
        }
        %w = stripChars(%w, "!?.,:-");
        if (%isCmd && !isNoAutoEmoteWord(%w))
        {
            %anim = convertWordToAnim(%w);
        }
        if (!(%anim $= ""))
        {
            return %anim;
        }
        %n = %n - 1;
    }
    if (%isCmd && !isNoAutoEmoteWord(%w))
    {
        return convertWordToAnim(%text);
    }
    return "";
}
function emote(%text)
{
    %anim = getLastEmoteAnim(%text);
    if (%anim $= "")
    {
        return 0;
    }
    sendAnimToServer(%anim);
    return 1;
}
function sendAnimToServer(%anim)
{
    setIdle(0);
    if (isObject(danceTool))
    {
        danceTool.addStep(%anim);
    }
    commandToServer('EtsPlayAnimName', %anim);
    return ;
}
$TEST_PREROLL = -1;
function sendDanceToolAnimToServer(%anim)
{
    commandToServer('PlayDanceToolAnim', %anim, $TEST_PREROLL);
    return ;
}
function doChangeRelation(%otherPlayerName, %relType, %oper)
{
    log("relations", "debug", "doChangeRelation:" SPC %otherPlayerName SPC %relType SPC %oper);
    %request = safeEnsureScriptObject("ManagerRequest", "RelRequest");
    if (%request.isOpen())
    {
        warn("network", getScopeName() SPC "- got overlapping requests. postponing. url =" SPC %request.getURL());
        return ;
    }
    %request.relType = %relType;
    %request.oper = %oper;
    %request.otherName = %otherPlayerName;
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
    return ;
}
function RelRequest::onError(%this, %unused, %unused)
{
    return ;
}
function RelRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    log("relations", "info", "RelRequest::onDone:" SPC %status);
    if (%status $= "fail")
    {
        %errorCode = %this.getValue("errorCode");
        %errorMsg = "";
        %markedOtherName = getPlayerMarkup(%this.otherName, "", 1);
        %operationStr = %this.relType @ %this.oper;
        if (%operationStr $= "friendadd")
        {
            if (%errorCode $= "invalid")
            {
                %errorMsg = "Woops, we can\'t find anyone named " @ %this.otherName @ " to befriend.";
            }
            else
            {
                if (%errorCode $= "ALREADY_RELATED")
                {
                    %errorMsg = "Woops, you\'re already friends with " @ %markedOtherName @ ".";
                }
                else
                {
                    if (%errorCode $= "DUPLICATE_REQUEST")
                    {
                        %errorMsg = "Woops, you\'re already asking " @ %markedOtherName @ " to be your friend.";
                    }
                    else
                    {
                        if (%errorCode $= "NOT_ALLOWED")
                        {
                            %errorMsg = "Sorry, you are not allowed to befriend " @ %markedOtherName @ ".";
                        }
                        else
                        {
                            if (%errorCode $= "NOT_RELATED")
                            {
                                error(getScopeName() @ "->unexpected error code for friendadd: NOT_RELATED");
                            }
                            else
                            {
                                if (%errorCode $= "USER_IS_IGNORED")
                                {
                                    %errorMsg = "Sorry, " @ %markedOtherName @ " will have to unignore you before you can add them as a friend.";
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (%operationStr $= "friendremove")
            {
                if (%errorCode $= "invalid")
                {
                    %errorMsg = "Woops, we can\'t find anyone named " @ %this.otherName @ " to unfriend.";
                }
                else
                {
                    if (%errorCode $= "NOT_RELATED")
                    {
                        %errorMsg = "Woops, you\'re not friends with " @ %markedOtherName @ "!";
                    }
                }
            }
            else
            {
                if (%operationStr $= "friendcancel")
                {
                    %errorMsg = "Woops, could not cancel friend request to " @ %markedOtherName @ " -- they may have already responded to your request.";
                }
                else
                {
                    if (%operationStr $= "friendacceptall")
                    {
                        %errorMsg = "Woops, could not accept all friend requests.";
                    }
                    else
                    {
                        if (%operationStr $= "frienddeclineall")
                        {
                            %errorMsg = "Woops, could not decline all friend requests.";
                        }
                        else
                        {
                            if (%operationStr $= "ignoreadd")
                            {
                                if (%errorCode $= "invalid")
                                {
                                    %errorMsg = "Woops, we can\'t find anyone named " @ %this.otherName @ " to ignore.";
                                }
                                else
                                {
                                    if ((%errorCode $= "USER_IS_IGNORED") && (%errorCode $= "DUPLICATE_REQUEST"))
                                    {
                                        %errorMsg = "Woops, you\'re already ignoring " @ %markedOtherName @ ".";
                                    }
                                    else
                                    {
                                        if (%errorCode $= "NOT_ALLOWED")
                                        {
                                            %errorMsg = "Sorry, you\'re not allowed to ignore " @ %markedOtherName @ ".";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (%operationStr $= "ignoreremove")
                                {
                                    if (%errorCode $= "invalid")
                                    {
                                        %errorMsg = "Woops, we can\'t find anyone named " @ %markedOtherName @ " to unignore.";
                                    }
                                    else
                                    {
                                        if (%errorCode $= "NOT_RELATED")
                                        {
                                            %errorMsg = "Woops, you aren\'t ignoring " @ %markedOtherName @ ".";
                                        }
                                    }
                                }
                                else
                                {
                                    error(getScopeName() @ "->THERE ARE NO PLAYER-FACING FAILURE MESSAGES FOR RELATION OPERATION " @ %operationStr @ ", PLEASE ADD THEM");
                                }
                            }
                        }
                    }
                }
            }
        }
        if (%errorMsg $= "")
        {
            warn(getScopeName() @ "->" @ %operationStr @ " errorcode (\"" @ %errorCode @ "\") unrecognized, sending generic " @ %operationStr @ " failure message!");
            if (%operationStr $= "friendadd")
            {
                %errorMsg = "Woops, could not create friend request to " @ %markedOtherName @ ".";
            }
            else
            {
                if (%operationStr $= "friendremove")
                {
                    %errorMsg = "Woops, could not remove " @ %markedOtherName @ " from your friends list.";
                }
                else
                {
                    if (%operationStr $= "ignoreadd")
                    {
                        %errorMsg = "Woops, could not ignore " @ %markedOtherName;
                    }
                    else
                    {
                        if (%operationStr $= "ignoreremove")
                        {
                            %errorMsg = "Woops, could not unignore " @ %markedOtherName;
                        }
                        else
                        {
                            error(getScopeName() @ "->" @ %operationStr @ " lacks generic failure message (for unrecognized errorcodes)");
                        }
                    }
                }
            }
        }
        if (!(%errorMsg $= ""))
        {
            handleSystemMessage("msgInfoMessage", %errorMsg);
        }
    }
    else
    {
        if (%status $= "success")
        {
            %comp = %this.relType @ %this.oper;
            if (((%comp $= "friendacceptall") || (%comp $= "frienddeclineall")) && (%this.otherName $= ""))
            {
                SystemMessageTextCtrl.updateFriendRequest(%this.otherName, %comp $= "friendacceptall");
            }
        }
    }
    return ;
}
function TVRemoteOperation(%tvremote)
{
    commandToServer('TVRemote', %tvremote);
    return ;
}
