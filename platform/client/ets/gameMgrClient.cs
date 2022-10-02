if (!isObject(gameMgrClient))
{
    $gameMgrClient = new ScriptObject(gameMgrClient);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add($gameMgrClient);
    }
    gameMgrClient.games = new SimSet();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(gameMgrClient.games.getId());
    }
}
function ClientCmdGameMgrClientNotify(%command, %arguments)
{
    echo(getScopeName() @ "->\"" @ detag(%command) @ "\" with %arguments==" @ %arguments);
    if (detag(%command) $= "playerInvited")
    {
        gameMgrClient.playerInvited(getField(%arguments, 0), getField(%arguments, 1), getField(%arguments, 2), getRecord(%arguments, 1), getRecords(%arguments, 2));
    }
    else
    {
        if (detag(%command) $= "playerJoined")
        {
            gameMgrClient.playerJoined(getField(%arguments, 0), getField(%arguments, 1), getField(%arguments, 2), getField(%arguments, 3), getField(%arguments, 4), getField(%arguments, 5), getField(%arguments, 6), getField(%arguments, 7), getField(%arguments, 8));
        }
        else
        {
            if (detag(%command) $= "playerLeft")
            {
                gameMgrClient.playerLeft(getField(%arguments, 0), getField(%arguments, 1));
            }
            else
            {
                if (detag(%command) $= "inspectNewGame")
                {
                    gameMgrClient.inspectNewGame(getField(%arguments, 0), getField(%arguments, 1), getField(%arguments, 2), getField(%arguments, 3), getField(%arguments, 4), getField(%arguments, 5), getField(%arguments, 6), getField(%arguments, 7), getField(%arguments, 8), getField(%arguments, 9));
                }
                else
                {
                    if (detag(%command) $= "inspectAddRemovePlayers")
                    {
                        gameMgrClient.inspectAddRemovePlayers(getField(%arguments, 0), getRecord(%arguments, 1), getRecord(%arguments, 2), getRecord(%arguments, 3), getRecord(%arguments, 4));
                    }
                    else
                    {
                        if (detag(%command) $= "inspectAddPlayers")
                        {
                            gameMgrClient.inspectAddRemovePlayers(getField(%arguments, 0), getRecord(%arguments, 1), "", getRecord(%arguments, 2), "");
                        }
                        else
                        {
                            if (detag(%command) $= "inspectRemovePlayers")
                            {
                                gameMgrClient.inspectAddRemovePlayers(getField(%arguments, 0), "", getRecord(%arguments, 1), getRecord(%arguments, 2), getRecord(%arguments, 3));
                            }
                            else
                            {
                                if (detag(%command) $= "inspectChangeReadyStatus")
                                {
                                    gameMgrClient.inspectChangeReadyStatus(getWord(%arguments, 0), getRecord(%arguments, 1), getRecord(%arguments, 2));
                                }
                                else
                                {
                                    if (detag(%command) $= "inspectUpdatePlayersStatus")
                                    {
                                        gameMgrClient.inspectUpdatePlayersStatus(getField(%arguments, 0), getRecord(%arguments, 1));
                                    }
                                    else
                                    {
                                        if (detag(%command) $= "superficialAddPlayers")
                                        {
                                            gameMgrClient.superficialAddPlayers(getField(%arguments, 0), getField(%arguments, 1), getField(%arguments, 2));
                                        }
                                        else
                                        {
                                            if (detag(%command) $= "superficialAddRemovePlayers")
                                            {
                                                gameMgrClient.superficialAddRemovePlayers(getField(%arguments, 0), getField(%arguments, 1));
                                            }
                                            else
                                            {
                                                if (detag(%command) $= "gamestatusChanged")
                                                {
                                                    gameMgrClient.gamestatusChanged(getField(%arguments, 0), getField(%arguments, 1));
                                                }
                                                else
                                                {
                                                    if (detag(%command) $= "deepDetailUpdated")
                                                    {
                                                        gameMgrClient.deepDetailUpdated(getField(%arguments, 0));
                                                    }
                                                    else
                                                    {
                                                        if (detag(%command) $= "inspectNothing")
                                                        {
                                                            gameMgrClient.inspectNothing();
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
    return ;
}
function putListIntoEnglish(%list)
{
    %ret = "";
    %fieldCount = getFieldCount(%list);
    if (%fieldCount == 2)
    {
        %ret = getField(%list, 0) @ " and " @ getField(%list, 1);
    }
    else
    {
        %n = 0;
        while (%n < %printedFieldCount)
        {
            %item = getField(%list, %n);
            if (%n == 0)
            {
                %ret = %item;
            }
            else
            {
                if (%n < (%fieldCount - 1))
                {
                    %ret = %ret @ ", " @ %item;
                }
                else
                {
                    %ret = %ret @ ", and " @ %item @ ".";
                }
            }
            %n = %n + 1;
        }
    }
}

function gameMgrClient::playerInvited(%this, %inviter, %aGameInstance, %gameType, %playerCount, %message)
{
    echo(getScopeName());
    echo("gameMgrClient: Game invitation from " @ %inviter @ ": come join " @ %aGameInstance @ ".");
    %msg = getPlayerMarkup(%inviter, "", 1) @ " invites you to play <a:game inspect " @ %aGameInstance @ ">" @ $gameMgr::GAME_TYPES[%gameType].title @ "</a>";
    if (%playerCount > 2)
    {
        %msg = %msg @ " with " @ %playerCount - 1 @ " others.";
    }
    else
    {
        %msg = %msg @ ".";
    }
    if (!(%message $= ""))
    {
        %msg = %msg SPC %message;
    }
    handleSystemMessage("msgInfoMessage", %msg);
    return ;
}
function gameMgrClient::superficialGameUpdate(%this, %serversideID, %playerCount, %gamestatus, %deepUpdate, %postponeUpdate)
{
    echo(getScopeName());
    %ourCopy = %this.getGameBySID(%serversideID);
    if (!isObject(%ourCopy))
    {
        warn("Server tried to send us a superficial update on a game we don\'t have a record for! <- " @ getScopeName());
        return ;
    }
    %ourCopy.playercount = %playerCount;
    %ourCopy.gamestatus = %gamestatus;
    %ourCopy.deepUpdate = %deepUpdate;
    if (%postponeUpdate != 1)
    {
        GameList.refresh();
    }
    return ;
}
function gameMgrClient::superficialAddPlayers(%this, %serversideID, %playerCount, %newTotal)
{
    echo(getScopeName());
    %ourCopy = %this.getGameBySID(%serversideID);
    if (isObject(%ourCopy))
    {
        %ourCopy.playercount = %newTotal;
        GameList.refresh();
    }
    else
    {
        error("Received superficial player add update for game we don\'t have a record of! <- " @ getScopeName());
    }
    return ;
}
function gameMgrClient::superficialAddRemovePlayers(%this, %serversideID, %newTotal)
{
    echo(getScopeName());
    %ourCopy = %this.getGameBySID(%serversideID);
    if (isObject(%ourCopy))
    {
        %ourCopy.playercount = %newTotal;
        GameList.refresh();
    }
    else
    {
        error("Received superficial player add/remove update for game we don\'t have a record of! <- " @ getScopeName());
    }
    return ;
}
function gameMgrClient::gamestatusChanged(%this, %serversideID, %newStatus)
{
    echo(getScopeName());
    %ourCopy = %this.getGameBySID(%serversideID);
    if (isObject(%ourCopy))
    {
        %ourCopy.gamestatus = %newStatus;
        GameList.refresh();
    }
    if (%this.inspectedGame.serversideID == %serversideID)
    {
        %this.inspectedGame.gamestatus = %newStatus;
        GameList.refreshInspectTab();
    }
    else
    {
        if (!isObject(%ourCopy))
        {
            error("Received gamestatus update for a game we don\'t have a record of and aren\'t inspecting! <- " @ getScopeName());
        }
    }
    return ;
}
function gameMgrClient::deepDetailUpdated(%this, %serversideID)
{
    echo(getScopeName());
    %ourCopy = %this.getGameBySID(%serversideID);
    if (isObject(%ourCopy))
    {
        %ourCopy.deepUpdated = 1;
        GameList.refresh();
    }
    return ;
}
function gameMgrClient::inspectNewGame(%this, %serversideID, %gname, %gameType, %gamestatus, %gamehost, %playerCount, %readyCount, %playerstatus, %playerRank, %playerScore)
{
    echo(getScopeName());
    if (isObject(%this.inspectedGame))
    {
        %this.inspectedGame.PlayerRecords.deleteMembers();
    }
    %ourCopy = %this.getGameBySID(%serversideID);
    if (isObject(%ourCopy))
    {
        if (!((%ourCopy.gname $= %gname)) && !((%ourCopy.gametype $= %gameType)))
        {
            error("Existing game record has different name or type than latest record! This should never happen and is bad news. Returning <-" @ getScopeName());
            return ;
        }
        if (isObject(%this.inspectedGame) && !%this.games.isMember(%this.inspectedGame))
        {
            %this.inspectedGame.PlayerRecords.delete();
            if (isObject(%this.inspectedGame.ourRecord))
            {
                %this.inspectedGame.ourRecord.delete();
            }
            %this.inspectedGame.delete();
        }
        %this.inspectedGame = %ourCopy;
        if (!isObject(%this.inspectedGame.PlayerRecords))
        {
            %this.inspectedGame.PlayerRecords = new SimSet();
        }
    }
    else
    {
        if (!isObject(%this.inspectedGame))
        {
            %this.inspectedGame = new ScriptObject();
            if (isObject(MissionCleanup))
            {
                MissionCleanup.add(%this.inspectedGame.getId());
                MissionCleanup.add(%this.inspectedGame.PlayerRecords.getId());
            }
        }
    }
    %this.inspectedGame.PlayerRecords.deleteMembers();
    %this.inspectedGame.serversideID = %serversideID;
    %this.inspectedGame.deepUpdated = 1;
    %this.inspectedGame.gname = %gname;
    %this.inspectedGame.gametype = %gameType;
    %this.inspectedGame.gamestatus = %gamestatus;
    %this.inspectedGame.playercount = %playerCount;
    %this.inspectedGame.readyCount = %readyCount;
    %this.inspectedGame.host = %gamehost;
    %this.inspectedGame.ourRecord = "";
    if (isObject(%ourCopy))
    {
        %this.inspectedGame.ourRecord = %this.newPlayerRecord($player.getShapeName(), %playerstatus, 0, %playerScore);
        %this.inspectedGame.ourRecord.rank = %playerRank;
    }
    GameList.refreshInspectTab();
    GameMgrHudTabs.selectTabWithName("INSPECT");
    commandToServer('gameMgrServerInvoke', 'sendAllPlayersFor', %serversideID);
    GameList.refresh();
    return ;
}
function gameMgrClient::inspectUpdatePlayersStatus(%this, %serversideID, %playerValues, %dontSortAndRefresh)
{
    echo(getScopeName());
    if (%this.inspectedGame.serversideID != %serversideID)
    {
        %ourCopy = %this.getGameBySID(%serversideID);
        if (isObject(%ourCopy))
        {
            %ourCopy.deepUpdated = 1;
        }
        warn("Received player(s) status update for game we\'re not inspecting! <- " @ getScopeName());
        return ;
    }
    %this.inspectedGame.deepUpdated = 1;
    %playersCount = getFieldCount(%playerValues) / 4;
    echo(getScopeName() @ "-> updating " @ %playersCount @ " players.");
    %i = 0;
    while (%i < %playersCount)
    {
        %playerIdx = %i * 4;
        %aPlayerName = getField(%playerValues, %playerIdx);
        %playerstatus = getField(%playerValues, %playerIdx + 1);
        %playerready = getField(%playerValues, %playerIdx + 2);
        %playerScore = getField(%playerValues, %playerIdx + 3);
        echo(getScopeName() @ "-> searching for record with name=" @ %aPlayerName);
        %theRecord = %this.inspectedGame.PlayerRecords.getByNameField(%aPlayerName);
        if (isObject(%theRecord))
        {
            if (!(%playerstatus $= ""))
            {
                %theRecord.status = %playerstatus;
            }
            if (!(%playerScore $= ""))
            {
                %theRecord.score = %playerScore;
            }
            if (!(%playerready $= ""))
            {
                %theRecord.ready = %playerready;
            }
        }
        else
        {
            %this.inspectedGame.PlayerRecords.add(gameMgrClient.newPlayerRecord(%aPlayerName, %playerstatus, %playerready, %playerScore));
        }
        if (%aPlayerName $= $player.getShapeName())
        {
            if (!isObject(%this.inspectedGame.ourRecord))
            {
                %this.inspectedGame.ourRecord = %this.newPlayerRecord(%aPlayerName, %playerstatus, %playerready, %playerScore);
            }
            else
            {
                if (!(%playerstatus $= ""))
                {
                    %this.inspectedGame.ourRecord.status = %playerstatus;
                }
                if (!(%playerScore $= ""))
                {
                    %this.inspectedGame.ourRecord.score = %playerScore;
                }
                if (!(%playerready $= ""))
                {
                    %this.inspectedGame.ourRecord.ready = %playerready;
                }
            }
        }
        %i = %i + 1;
    }
    if (%dontSortAndRefresh $= "")
    {
        %this.sortAndPurgePlayerRecords(%this.inspectedGame);
        GameList.refreshInspectTab();
    }
    return ;
}
function gameMgrClient::inspectAddRemovePlayers(%this, %serversideID, %playersAdded, %playersRemoved, %totalPlayersRemaining, %totalReadyPlayersRemaining)
{
    echo(getScopeName());
    if (%this.inspectedGame.serversideID != %serversideID)
    {
        %ourCopy = %this.getGameBySID(%serversideID);
        if (isObject(%ourCopy))
        {
            %ourCopy.playercount = %totalPlayersRemaining;
            GameList.refresh();
        }
        warn("Received inspect players update for a game we\'re not inspecting! <-" @ getScopeName());
        return ;
    }
    if (!isObject(%this.inspectedGame.PlayerRecords))
    {
        error("inspectedGame has no .PlayerRecords to modify! <-" @ getScopeName());
        return ;
    }
    %this.inspectedGame.playercount = %totalPlayersRemaining;
    GameList.refresh();
    if (!(%totalReadyPlayersRemaining $= ""))
    {
        %this.inspectedGame.readyCount = %totalReadyPlayersRemaining;
    }
    if ((%playersAdded $= "") && (%playersRemoved $= ""))
    {
        GameList.refreshInspectTab();
        return ;
    }
    if (!(%playersAdded $= ""))
    {
        %this.inspectUpdatePlayersStatus(%serversideID, %playersAdded, 1);
    }
    %i = getFieldCount(%playersRemoved) - 1;
    while (%i >= 0)
    {
        %aPlayerName = getField(%playersRemoved, %i);
        %aPlayer = %this.inspectedGame.PlayerRecords.getByNameField(%aPlayerName);
        if (isObject(%aPlayer))
        {
            %this.inspectedGame.PlayerRecords.remove(%aPlayer);
            %aPlayer.delete();
        }
        else
        {
            warn("Asked to remove a player that we didn\'t have a record of! <- " @ getScopeName());
        }
        %i = %i - 1;
    }
    %this.sortAndPurgePlayerRecords(%this.inspectedGame);
    GameList.refreshInspectTab();
    return ;
}
function gameMgrClient::inspectChangeReadyStatus(%this, %serversideID, %readyValues, %totalReady)
{
    echo(getScopeName());
    if (%this.inspectedGame.serversideID != %serversideID)
    {
        warn("Received inspect changeReadyStatus update for a game we\'re not inspecting! <-" @ getScopeName());
        return ;
    }
    %playerCount = getFieldCount(%readyValues) / 2;
    %i = 0;
    while (%i < %playerCount)
    {
        %playerIdx = %i * 2;
        %aPlayerName = getField(%readyValues, %playerIdx);
        %aReadyValue = getField(%readyValues, %playerIdx + 1);
        if (%aPlayerName $= $player.getShapeName())
        {
            %this.inspectedGame.ourRecord.ready = %aReadyValue;
        }
        %aPlayer = %this.inspectedGame.PlayerRecords.getByNameField(%aPlayerName);
        if (isObject(%aPlayer))
        {
            %aPlayer.ready = %aReadyValue;
        }
        else
        {
            if (!(%aPlayerName $= $player.getShapeName()))
            {
                warn("Received a request-to-start update on a player that we don\'t have a record for! <- " @ getScopeName());
            }
        }
        %i = %i + 1;
    }
    %this.inspectedGame.readyCount = %totalReady;
    GameList.refreshInspectTab();
    return ;
}
function gameMgrClient::inspectNothing(%this)
{
    if (isObject(%this.inspectedGame))
    {
        if (!%this.games.isMember(%this.inspectedGame))
        {
            %this.inspectedGame.serversideID = "";
        }
        else
        {
            %this.inspectedGame = "";
        }
    }
    GameList.refreshInspectTab();
    return ;
}
function gameMgrClient::areWeInspecting(%this)
{
    if (!isObject(%this.inspectedGame) && (%this.inspectedGame.serversideID $= ""))
    {
        return 0;
    }
    else
    {
        return 1;
    }
    return ;
}
function gameMgrClient::areWeHostOfInspectedGame(%this)
{
    if (!%this.areWeInspecting())
    {
        return 0;
    }
    if (%this.inspectedGame.host $= $player.getShapeName())
    {
        return 1;
    }
    else
    {
        return 0;
    }
    return ;
}
function gameMgrClient::inCustomGame(%this)
{
    if (!%this.areWeInspecting())
    {
        return 0;
    }
    if (%this.inspectedGame.gametype == $gameMgr::CUSTOM_GAME)
    {
        return 1;
    }
    else
    {
        return 0;
    }
    return ;
}
function gameMgrClient::playerJoined(%this, %serversideID, %gname, %gameType, %host, %playerCount, %gamestatus, %playerstatus, %playerRank, %playerScore)
{
    echo(getScopeName());
    echo("gameMgrClient: Joined game " @ %serversideID);
    %this.addGame(%serversideID, %gname, %gameType, %host, %playerCount, %gamestatus);
    GameList.refresh();
    GameList.switchIfInspectEmpty();
    if (%this.inspectedGame.serversideID == %serversideID)
    {
        if (!isObject(%this.inspectedGame.ourRecord))
        {
            if ((%playerRank $= "") && (%playerScore $= ""))
            {
                error("on playerJoined call for joining an inspected game, playerRank and playerScore were not passed!<-" @ getScopeName());
            }
            %this.inspectedGame.ourRecord = %this.newPlayerRecord($player.getShapeName(), %playerstatus, %playerRank, %playerScore);
        }
        else
        {
            error("playerJoined trying to create an ourRecord for inspected game we joined, but it already exists!<-" @ getScopeName());
        }
        GameList.refreshInspectTab();
    }
    if (%host $= $player.getShapeName())
    {
        if (%gameType == $gameMgr::CUSTOM_GAME)
        {
            userTips::showOnceThisSession("CustomGameHost");
        }
        handleSystemMessage("msgInfoMessage", "You created a game: <a:game inspect " @ %serversideID @ ">" @ %gname @ "</a>.");
    }
    else
    {
        handleSystemMessage("msgInfoMessage", "You joined " @ %host @ "\'s game: <a:game inspect " @ %serversideID @ ">" @ %gname @ "</a>.");
    }
    return ;
}
function gameMgrClient::playerLeft(%this, %serversideID, %message)
{
    echo(getScopeName());
    echo("gameMgrClient: left game " @ %serversideID);
    %game = %this.getGameBySID(%serversideID);
    %exitMessage = "You left ";
    if (%game.host $= $player.getShapeName())
    {
        %exitMessage = %exitMessage @ " your";
    }
    else
    {
        %exitMessage = %exitMessage @ %game.host @ "\'s";
    }
    %exitMessage = %exitMessage @ " game: <a:game inspect " @ %serversideID @ ">" @ %game.gname @ "</a>.";
    if (!(%message $= ""))
    {
        %exitMessage = %exitMessage SPC %message;
    }
    %this.removeGame(%serversideID);
    GameList.refresh();
    if (%this.inspectedGame.serversideID == %serversideID)
    {
        %this.inspectedGame.ourRecord.delete();
    }
    handleSystemMessage("msgInfoMessage", %exitMessage);
    return ;
}
function gameMgrClient::playerJoinGame(%this, %serversideID)
{
    commandToServer('gameMgrServerInvoke', 'playerJoinGame', %serversideID);
    return ;
}
function gameMgrClient::playerQuitGame(%this, %serversideID)
{
    commandToServer('gameMgrServerInvoke', 'playerQuitGame', %serversideID);
    return ;
}
function gameMgrClient::requestQuitGameWithName(%this, %gameName)
{
    %theGame = %this.games.getByField("gname", %gameName);
    if (!isObject(%theGame))
    {
        handleSystemMessage("msgInfoMessage", "Sorry, you aren\'t in a game named " @ %gameName @ ".");
        return ;
    }
    %this.playerQuitGame(%theGame.serversideID);
    return ;
}
function gameMgrClient::playerChangeReadyStatus(%this, %serversideID, %readyStatus)
{
    echo(getScopeName());
    commandToServer('gameMgrServerInvoke', 'playerChangeReadyStatus', %serversideID SPC %readyStatus);
    return ;
}
function gameMgrClient::playerRequestStartGame(%this, %serversideID)
{
    echo(getScopeName());
    commandToServer('gameMgrServerInvoke', 'playerRequestStartGame', %serversideID);
    return ;
}
function gameMgrClient::requestToInspectGame(%this, %serversideID)
{
    echo(getScopeName());
    commandToServer('gameMgrServerInvoke', 'inspectGameRequest', %serversideID);
    return ;
}
function gameMgrClient::requestStartGameWithName(%this, %gameName)
{
    %theGame = %this.games.getByField("gname", %gameName);
    if (!isObject(%theGame))
    {
        handleSystemMessage("msgInfoMessage", "Sorry, you aren\'t in a game named " @ %gameName @ ".");
        return ;
    }
    %this.playerRequestStartGame(%theGame.serversideID);
    return ;
}
function gameMgrClient::createGame(%this, %gameName, %gameType, %waitingRoom, %joinInProgress, %autoStartOnReady, %dropUnreadyPlayers)
{
    echo(getScopeName());
    %argString = %gameName TAB %gameType TAB %waitingRoom;
    if (%waitingRoom)
    {
        %argString = %argString TAB %joinInProgress TAB %autoStartOnReady TAB %dropUnreadyPlayers;
    }
    commandToServer('gameMgrServerInvoke', 'playerCreateGame', %argString);
    return ;
}
function gameMgrClient::playerStopInspectingGame(%this)
{
    commandToServer('gameMgrServerInvoke', 'playerStopInspectingGame', "");
    return ;
}
function gameMgrClient::invitePlayerToInspectedGame(%this, %playerName, %message)
{
    if (!%this.areWeInspecting())
    {
        handleSystemMessage("msgInfoMessage", "You\'ve got to have a game inspected to invite someone!");
        return ;
    }
    commandToServer('gameMgrServerInvoke', 'playerInvitePlayer', %this.inspectedGame.serversideID TAB %playerName TAB %message);
    return ;
}
function gameMgrClient::doHostPopupChangeScore(%this, %playerName)
{
    if (!%this.areWeHostOfInspectedGame())
    {
        handleSystemMessage("msgInfoMessage", "You must be inspecting a game you\'re the host of to change player scores!");
        return ;
    }
    gameMgrHostPopup.open(%playerName, "score");
    return ;
}
function gameMgrClient::doHostPopupChangeStatus(%this, %playerName)
{
    if (!%this.areWeHostOfInspectedGame())
    {
        handleSystemMessage("msgInfoMessage", "You must be inspecting a game you\'re the host of to change a player\'s status!");
        return ;
    }
    gameMgrHostPopup.open(%playerName, "status");
    return ;
}
function gameMgrClient::hostChangePlayerScore(%this, %playerName, %scoreDelta)
{
    if (!%this.areWeHostOfInspectedGame())
    {
        handleSystemMessage("msgInfoMessage", "You must be inspecting a game you\'re the host of to change player scores!");
        return ;
    }
    commandToServer('gameMgrServerInvoke', 'hostChangePlayerScore', %this.inspectedGame.serversideID TAB %playerName TAB %scoreDelta);
    return ;
}
function gameMgrClient::hostChangePlayerStatus(%this, %playerName, %newStatus)
{
    if (!%this.areWeHostOfInspectedGame())
    {
        handleSystemMessage("msgInfoMessage", "You must be inspecting a game you\'re the host of to change a player\'s status!");
        return ;
    }
    commandToServer('gameMgrServerInvoke', 'hostChangePlayerStatus', %this.inspectedGame.serversideID TAB %playerName TAB %newStatus);
    return ;
}
function gameMgrClient::areWePlaying(%this, %gameObj)
{
    return %this.games.isMember(%gameObj);
}
function gameMgrClient::getGameBySID(%this, %serversideID)
{
    echo(getScopeName());
    %n = %this.games.getCount() - 1;
    while (%n >= 0)
    {
        %aGame = %this.games.getObject(%n);
        if (%aGame.serversideID == %serversideID)
        {
            return %aGame;
        }
        %n = %n - 1;
    }
    return "";
}
function gameMgrClient::addGame(%this, %serversideID, %gname, %gameType, %host, %playerCount, %gamestatus)
{
    echo(getScopeName());
    if (isObject(%this.inspectedGame) && (%this.inspectedGame.serversideID == %serversideID))
    {
        %newGame = %this.inspectedGame;
        if (!((%gname $= %this.inspectedGame.gname)) && (%gameType != %this.inspectedGame.gametype))
        {
            error("Serious error! gameMgrClient::addGame called with same serversideID as inspectedGame but gname and gametype don\'t match! Not overriding!");
        }
        %newGame.playercount = %playerCount;
        %newGame.gamestatus = %gamestatus;
    }
    else
    {
        %newGame = new ScriptObject()
        {
            serversideID = %serversideID;
            gname = %gname;
            gametype = %gameType;
            host = %host;
            playercount = %playerCount;
            gamestatus = %gamestatus;
            deepUpdate = 0;
        };
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(%newGame);
        }
    }
    %this.games.add(%newGame);
    %listFound = 0;
    %n = GameList.lists.getCount() - 1;
    while (%n >= 0)
    {
        %aList = GameList.lists.getObject(%n);
        if (%aList.gametype == %newGame.gametype)
        {
            %aList.add(%newGame);
            %listFound = 1;
            break;
        }
        %n = %n - 1;
    }
    if (%listFound <= 0)
    {
        %newList = new SimSet()
        {
            gametype = %newGame.gametype;
            collapsed = 0;
        };
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(%newList);
        }
        %newList.add(%newGame);
        GameList.lists.add(%newList);
    }
    return ;
}
function gameMgrClient::removeGame(%this, %serversideID)
{
    echo(getScopeName());
    if (%this.games.getCount() == 0)
    {
        error("trying to delete a game but gameMgrClient.games is empty! <- " @ getScopeName());
    }
    %theGame = %this.getGameBySID(%serversideID);
    if (%index = %this.games.getObjectIndex(%theGame) == -1)
    {
        error("Trying to delete record of a game that we don\'t have! (not in gameMgrClient.games) <-" @ getScopeName);
        return ;
    }
    %this.games.remove(%theGame);
    %n = GameList.lists.getCount() - 1;
    while (%n >= 0)
    {
        %aList = GameList.lists.getObject(%n);
        if (%aList.gametype == %theGame.gametype)
        {
            if (%aList.getObjectIndex(%theGame) == -1)
            {
                error("Deleting a game who isn\'t listed in his gametype! <-" @ getScopeName());
            }
            %aList.remove(%theGame);
            if (%aList.getCount() == 0)
            {
                GameList.lists.remove(%aList);
                %aList.delete();
            }
            break;
        }
        else
        {
            if (%n == 0)
            {
                error("Deleting a game whose gametype didn\'t have a list <- " @ getScopeName());
            }
        }
        %n = %n - 1;
    }
    if (%theGame != %this.inspectedGame)
    {
        %theGame.delete();
    }
    return ;
}
function gameMgrClient::startFresh(%this)
{
    GameList.lists.deleteMembers();
    %this.games.deleteMembers();
    if (isObject(%this.inspectedGame))
    {
        %this.inspectedGame.PlayerRecords.deleteMembers();
        %this.inspectedGame.PlayerRecords.delete();
        %this.inspectedGame.delete();
    }
    return ;
}
function gameMgrClient::newPlayerRecord(%this, %playerName, %playerstatus, %playerready, %playerScore)
{
    %ret = new ScriptObject()
    {
        name = %playerName;
        status = %playerstatus;
        ready = %playerready;
        score = %playerScore;
    };
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%ret);
    }
    return %ret;
}
function gameMgrClient::sortAndPurgePlayerRecords(%this, %aGameObj)
{
    if (!isObject(%aGameObj) && !isObject(%aGameObj.PlayerRecords))
    {
        error("Tried to sort PlayerRecords on a game that didn\'t exist or didn\'t have PlayerRecords! <-" @ getScopeName());
        return ;
    }
    %numRecords = %aGameObj.PlayerRecords.getCount();
    if (%numRecords == 0)
    {
        return ;
    }
    %stringToSort = "";
    echo("sortAndPurgePlayerRecords working with PlayerRecords count = " @ %aGameObj.PlayerRecords.getCount());
    %n = %numRecords - 1;
    while (%n >= 0)
    {
        %curRecord = %aGameObj.PlayerRecords.getObject(%n);
        %stringToSort = trim(%stringToSort SPC formatInt("%0."$gameMgr::MAX_SCORE_DIGITS"d", %curRecord.score) TAB %curRecord.getId());
        %n = %n - 1;
    }
    %stringToSort = SortWords(%stringToSort);
    %count = getFieldCount(%stringToSort);
    if ((%count - 1) != %numRecords)
    {
        error("Serious error! All the player records didn\'t fit in the string to be sorted! Not sorting! <- " @ getScopeName());
        return ;
    }
    %aGameObj.PlayerRecords.clear(1);
    %n = %count - 1;
    while (%n >= 1)
    {
        %recordID = trim(getWord(getField(%stringToSort, %n), 0));
        if (isObject(%recordID))
        {
            if (%n >= (%count - $gameMgr::InspectTab::MAX_PLAYERS))
            {
                %aGameObj.PlayerRecords.add(%recordID);
                %aGameObj.PlayerRecords.pushToBack(%recordID);
            }
            else
            {
                %recordID.delete();
            }
        }
        else
        {
            error("Serious error! Record ID retrieved from sorted string invalid! Skipping and continuing <-" @ getScopeName());
        }
        %n = %n - 1;
    }
}

function gameMgrHostPopup::setup(%this)
{
    gameMgrHostPopup.variText = new GuiMLTextCtrl()
    {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "12 28";
        extent = "220 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = -1;
    };
    gameMgrHostPopup.add(gameMgrHostPopup.variText);
    gameMgrHostPopup.textField = new GuiTextEditCtrl()
    {
        profile = "ETSDarkTextEditProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "20 49";
        extent = "197 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        altCommand = "";
        maxLength = 32;
        historySize = 1;
        password = 0;
        tabComplete = 0;
        sinkAllKeyEvents = 0;
    };
    gameMgrHostPopup.add(gameMgrHostPopup.textField);
    gameMgrHostPopup.applyButton = new GuiVariableWidthButtonCtrl()
    {
        profile = "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "92 80";
        extent = "44 15";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "gameMgrHostPopup.apply();";
        text = "Apply";
        groupNum = -1;
        buttonType = "PushButton";
    };
    gameMgrHostPopup.add(gameMgrHostPopup.applyButton);
    gameMgrHostPopup.isSetup = 1;
    return ;
}
function gameMgrHostPopup::close(%this)
{
    %this.setVisible(0);
    return ;
}
function gameMgrHostPopup::open(%this, %playerName, %command)
{
    if (!%this.isSetup)
    {
        %this.setup();
    }
    if (%command $= "score")
    {
        %this.variText.setText("<spush><b>Change " @ %playerName @ "\'s score by:<spop>(e.g. +5, -10)");
    }
    else
    {
        if (%command $= "status")
        {
            %this.variText.setText("<spush><b>Set " @ %playerName @ "\'s status to:<spop>");
        }
        else
        {
            error("gameMgrHostPopup::apply was passed a unrecognized command " @ %command @ ".<-" @ getScopeName());
            return ;
        }
    }
    %this.command = %command;
    %this.playerName = %playerName;
    %this.setVisible(1);
    return ;
}
function gameMgrHostPopup::apply(%this)
{
    if (%this.command $= "score")
    {
        gameMgrClient.hostChangePlayerScore(%this.playerName, %this.textField.getText());
    }
    else
    {
        if (%this.command $= "status")
        {
            gameMgrClient.hostChangePlayerStatus(%this.playerName, %this.textField.getText());
        }
        else
        {
            error("gameMgrHostPopup::apply was passed a unrecognized command " @ %this.command @ ".<-" @ getScopeName());
        }
    }
    return ;
}
