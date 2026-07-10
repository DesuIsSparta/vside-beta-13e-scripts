if (!(isObject(gameMgrClient))) {
    $gameMgrClient = new ScriptObject(gameMgrClient);
    if (isObject(MissionCleanup)) {
        $gameMgrClient.add(MissionCleanup);
    }
    gameMgrClient.games = new SimSet("");
    if (isObject(MissionCleanup)) {
        gameMgrClient.games.getId().add(MissionCleanup);
    }
}
function ClientCmdGameMgrClientNotify(%command, %arguments) {
    echo(getScopeName() @ "->\"" @ detag(%command) @ "\" with %arguments==" @ %arguments);
    if ((detag(%command) $= "playerInvited")) {
        getRecords(%arguments, 2).playerInvited(gameMgrClient, getField(%arguments, 0), getField(%arguments, 1), getField(%arguments, 2), getRecord(%arguments, 1));
    }
    if ((detag(%command) $= "playerJoined")) {
        getField(%arguments, 8).playerJoined(gameMgrClient, getField(%arguments, 0), getField(%arguments, 1), getField(%arguments, 2), getField(%arguments, 3), getField(%arguments, 4), getField(%arguments, 5), getField(%arguments, 6), getField(%arguments, 7));
    }
    if ((detag(%command) $= "playerLeft")) {
        getField(%arguments, 1).playerLeft(gameMgrClient, getField(%arguments, 0));
    }
    if ((detag(%command) $= "inspectNewGame")) {
        getField(%arguments, 9).inspectNewGame(gameMgrClient, getField(%arguments, 0), getField(%arguments, 1), getField(%arguments, 2), getField(%arguments, 3), getField(%arguments, 4), getField(%arguments, 5), getField(%arguments, 6), getField(%arguments, 7), getField(%arguments, 8));
    }
    if ((detag(%command) $= "inspectAddRemovePlayers")) {
        getRecord(%arguments, 4).inspectAddRemovePlayers(gameMgrClient, getField(%arguments, 0), getRecord(%arguments, 1), getRecord(%arguments, 2), getRecord(%arguments, 3));
    }
    if ((detag(%command) $= "inspectAddPlayers")) {
        "".inspectAddRemovePlayers(gameMgrClient, getField(%arguments, 0), getRecord(%arguments, 1), "", getRecord(%arguments, 2));
    }
    if ((detag(%command) $= "inspectRemovePlayers")) {
        getRecord(%arguments, 3).inspectAddRemovePlayers(gameMgrClient, getField(%arguments, 0), "", getRecord(%arguments, 1), getRecord(%arguments, 2));
    }
    if ((detag(%command) $= "inspectChangeReadyStatus")) {
        getRecord(%arguments, 2).inspectChangeReadyStatus(gameMgrClient, getWord(%arguments, 0), getRecord(%arguments, 1));
    }
    if ((detag(%command) $= "inspectUpdatePlayersStatus")) {
        getRecord(%arguments, 1).inspectUpdatePlayersStatus(gameMgrClient, getField(%arguments, 0));
    }
    if ((detag(%command) $= "superficialAddPlayers")) {
        getField(%arguments, 2).superficialAddPlayers(gameMgrClient, getField(%arguments, 0), getField(%arguments, 1));
    }
    if ((detag(%command) $= "superficialAddRemovePlayers")) {
        getField(%arguments, 1).superficialAddRemovePlayers(gameMgrClient, getField(%arguments, 0));
    }
    if ((detag(%command) $= "gamestatusChanged")) {
        getField(%arguments, 1).gamestatusChanged(gameMgrClient, getField(%arguments, 0));
    }
    if ((detag(%command) $= "deepDetailUpdated")) {
        getField(%arguments, 0).deepDetailUpdated(gameMgrClient);
    }
    if ((detag(%command) $= "inspectNothing")) {
        gameMgrClient.inspectNothing();
    }
};
function putListIntoEnglish(%list) {
    %ret = "";
    %fieldCount = getFieldCount(%list);
    if ((%fieldCount == 2.0)) {
        %ret = getField(%list, 0) @ " and " @ getField(%list, 1);
    }
    %n = 0;
    while ((%n < %printedFieldCount)) {
        %item = getField(%list, %n);
        if ((%n == 0.0)) {
            %ret = %item;
        }
        if ((%n < (%fieldCount - 1.0))) {
            %ret = %ret @ ", " @ %item;
        }
        %ret = %ret @ ", and " @ %item @ ".";
        %n = (%n + 1.0);
    }
};
function gameMgrClient::playerInvited(%this, %inviter, %aGameInstance, %gameType, %playerCount, %message) {
    echo(getScopeName());
    echo("gameMgrClient: Game invitation from " @ %inviter @ ": come join " @ %aGameInstance @ ".");
    %msg = getPlayerMarkup(%inviter, "", 1) @ " invites you to play <a:game inspect " @ %aGameInstance @ ">" @ %gameType[$gameMgr::GAME_TYPES @ %gameType].title @ "</a>";
    if ((%playerCount > 2.0)) {
        %msg = %msg @ " with " @ (%playerCount - 1.0) @ " others.";
    }
    %msg = %msg @ ".";
    if (!(%message $= "")) {
        %msg = %msg @ " " @ %message;
    }
    handleSystemMessage("msgInfoMessage", %msg);
};
function gameMgrClient::superficialGameUpdate(%this, %serversideID, %playerCount, %gamestatus, %deepUpdate, %postponeUpdate) {
    echo(getScopeName());
    %ourCopy = %serversideID.getGameBySID(%this);
    if (!(isObject(%ourCopy))) {
        warn("Server tried to send us a superficial update on a game we don't have a record for! <- " @ getScopeName());
        return;
    }
    %ourCopy.playercount = %playerCount;
    %ourCopy.gamestatus = %gamestatus;
    %ourCopy.deepUpdate = %deepUpdate;
    if ((%postponeUpdate != 1.0)) {
        GameList.refresh();
    }
};
function gameMgrClient::superficialAddPlayers(%this, %serversideID, %playerCount, %newTotal) {
    echo(getScopeName());
    %ourCopy = %serversideID.getGameBySID(%this);
    if (isObject(%ourCopy)) {
        %ourCopy.playercount = %newTotal;
        GameList.refresh();
    }
    error("Received superficial player add update for game we don't have a record of! <- " @ getScopeName());
};
function gameMgrClient::superficialAddRemovePlayers(%this, %serversideID, %newTotal) {
    echo(getScopeName());
    %ourCopy = %serversideID.getGameBySID(%this);
    if (isObject(%ourCopy)) {
        %ourCopy.playercount = %newTotal;
        GameList.refresh();
    }
    error("Received superficial player add/remove update for game we don't have a record of! <- " @ getScopeName());
};
function gameMgrClient::gamestatusChanged(%this, %serversideID, %newStatus) {
    echo(getScopeName());
    %ourCopy = %serversideID.getGameBySID(%this);
    if (isObject(%ourCopy)) {
        %ourCopy.gamestatus = %newStatus;
        GameList.refresh();
    }
    if ((%this.inspectedGame.serversideID == %serversideID)) {
        %this.inspectedGame.gamestatus = %newStatus;
        GameList.refreshInspectTab();
    }
    if (!(isObject(%ourCopy))) {
        error("Received gamestatus update for a game we don't have a record of and aren't inspecting! <- " @ getScopeName());
    }
};
function gameMgrClient::deepDetailUpdated(%this, %serversideID) {
    echo(getScopeName());
    %ourCopy = %serversideID.getGameBySID(%this);
    if (isObject(%ourCopy)) {
        %ourCopy.deepUpdated = 1;
        GameList.refresh();
    }
};
function gameMgrClient::inspectNewGame(%this, %serversideID, %gname, %gameType, %gamestatus, %gamehost, %playerCount, %readyCount, %playerstatus, %playerRank, %playerScore) {
    echo(getScopeName());
    if (isObject(%this.inspectedGame)) {
        %this.inspectedGame.PlayerRecords.deleteMembers();
    }
    %ourCopy = %serversideID.getGameBySID(%this);
    if (isObject(%ourCopy)) {
        if (!(%ourCopy.gname $= %gname)) {
        }
        if (!(%ourCopy.gametype $= %gameType)) {
            error("Existing game record has different name or type than latest record! This should never happen and is bad news. Returning <-" @ getScopeName());
            return;
        }
        if (isObject(%this.inspectedGame)) {
        }
        if (!(%this.inspectedGame.isMember(%this.games))) {
            %this.inspectedGame.PlayerRecords.delete();
            if (isObject(%this.inspectedGame.ourRecord)) {
                %this.inspectedGame.ourRecord.delete();
            }
            %this.inspectedGame.delete();
        }
        %this.inspectedGame = %ourCopy;
        if (!(isObject(%this.inspectedGame.PlayerRecords))) {
            %this.inspectedGame.PlayerRecords = new SimSet("");
        }
    }
    if (!(isObject(%this.inspectedGame))) {
        %this.inspectedGame = new ScriptObject("") {
            PlayerRecords = new SimSet("");;
        };
        if (isObject(MissionCleanup)) {
            %this.inspectedGame.getId().add(MissionCleanup);
            %this.inspectedGame.PlayerRecords.getId().add(MissionCleanup);
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
    if (isObject(%ourCopy)) {
        %this.inspectedGame.ourRecord = %playerScore.newPlayerRecord(%this, $player.getShapeName(), %playerstatus, 0);
        %this.inspectedGame.ourRecord.rank = %playerRank;
    }
    GameList.refreshInspectTab();
    "INSPECT".selectTabWithName(GameMgrHudTabs);
    commandToServer('gameMgrServerInvoke', 'sendAllPlayersFor', %serversideID);
    GameList.refresh();
};
function gameMgrClient::inspectUpdatePlayersStatus(%this, %serversideID, %playerValues, %dontSortAndRefresh) {
    echo(getScopeName());
    if ((%this.inspectedGame.serversideID != %serversideID)) {
        %ourCopy = %serversideID.getGameBySID(%this);
        if (isObject(%ourCopy)) {
            %ourCopy.deepUpdated = 1;
        }
        warn("Received player(s) status update for game we're not inspecting! <- " @ getScopeName());
        return;
    }
    %this.inspectedGame.deepUpdated = 1;
    %playersCount = (getFieldCount(%playerValues) / 4.0);
    echo(getScopeName() @ "-> updating " @ %playersCount @ " players.");
    %i = 0;
    while ((%i < %playersCount)) {
        %playerIdx = (%i * 4.0);
        %aPlayerName = getField(%playerValues, %playerIdx);
        %playerstatus = getField(%playerValues, (%playerIdx + 1.0));
        %playerready = getField(%playerValues, (%playerIdx + 2.0));
        %playerScore = getField(%playerValues, (%playerIdx + 3.0));
        echo(getScopeName() @ "-> searching for record with name=" @ %aPlayerName);
        %theRecord = %aPlayerName.getByNameField(%this.inspectedGame.PlayerRecords);
        if (isObject(%theRecord)) {
            if (!(%playerstatus $= "")) {
                %theRecord.status = %playerstatus;
            }
            if (!(%playerScore $= "")) {
                %theRecord.score = %playerScore;
            }
            if (!(%playerready $= "")) {
                %theRecord.ready = %playerready;
            }
        }
        %playerScore.newPlayerRecord(gameMgrClient, %aPlayerName, %playerstatus, %playerready).add(%this.inspectedGame.PlayerRecords);
        if ((%aPlayerName $= $player.getShapeName())) {
            if (!(isObject(%this.inspectedGame.ourRecord))) {
                %this.inspectedGame.ourRecord = %playerScore.newPlayerRecord(%this, %aPlayerName, %playerstatus, %playerready);
            }
            if (!(%playerstatus $= "")) {
                %this.inspectedGame.ourRecord.status = %playerstatus;
            }
            if (!(%playerScore $= "")) {
                %this.inspectedGame.ourRecord.score = %playerScore;
            }
            if (!(%playerready $= "")) {
                %this.inspectedGame.ourRecord.ready = %playerready;
            }
        }
        %i = (%i + 1.0);
    }
    if (((%i < %playersCount) @ " " @ %dontSortAndRefresh $= "")) {
        %this.inspectedGame.sortAndPurgePlayerRecords(%this);
        GameList.refreshInspectTab();
    }
};
function gameMgrClient::inspectAddRemovePlayers(%this, %serversideID, %playersAdded, %playersRemoved, %totalPlayersRemaining, %totalReadyPlayersRemaining) {
    echo(getScopeName());
    if ((%this.inspectedGame.serversideID != %serversideID)) {
        %ourCopy = %serversideID.getGameBySID(%this);
        if (isObject(%ourCopy)) {
            %ourCopy.playercount = %totalPlayersRemaining;
            GameList.refresh();
        }
        warn("Received inspect players update for a game we're not inspecting! <-" @ getScopeName());
        return;
    }
    if (!(isObject(%this.inspectedGame.PlayerRecords))) {
        error("inspectedGame has no .PlayerRecords to modify! <-" @ getScopeName());
        return;
    }
    %this.inspectedGame.playercount = %totalPlayersRemaining;
    GameList.refresh();
    if (!(%totalReadyPlayersRemaining $= "")) {
        %this.inspectedGame.readyCount = %totalReadyPlayersRemaining;
    }
    if ((%playersAdded $= "")) {
    }
    if ((%playersRemoved $= "")) {
        GameList.refreshInspectTab();
        return;
    }
    if (!(%playersAdded $= "")) {
        1.inspectUpdatePlayersStatus(%this, %serversideID, %playersAdded);
    }
    %i = (getFieldCount(%playersRemoved) - 1.0);
    while ((%i >= 0.0)) {
        %aPlayerName = getField(%playersRemoved, %i);
        %aPlayer = %aPlayerName.getByNameField(%this.inspectedGame.PlayerRecords);
        if (isObject(%aPlayer)) {
            %aPlayer.remove(%this.inspectedGame.PlayerRecords);
            %aPlayer.delete();
        }
        warn("Asked to remove a player that we didn't have a record of! <- " @ getScopeName());
        %i = (%i - 1.0);
    }
    %this.inspectedGame.sortAndPurgePlayerRecords(%this);
    GameList.refreshInspectTab();
};
function gameMgrClient::inspectChangeReadyStatus(%this, %serversideID, %readyValues, %totalReady) {
    echo(getScopeName());
    if ((%this.inspectedGame.serversideID != %serversideID)) {
        warn("Received inspect changeReadyStatus update for a game we're not inspecting! <-" @ getScopeName());
        return;
    }
    %playerCount = (getFieldCount(%readyValues) / 2.0);
    %i = 0;
    while ((%i < %playerCount)) {
        %playerIdx = (%i * 2.0);
        %aPlayerName = getField(%readyValues, %playerIdx);
        %aReadyValue = getField(%readyValues, (%playerIdx + 1.0));
        if ((%aPlayerName $= $player.getShapeName())) {
            %this.inspectedGame.ourRecord.ready = %aReadyValue;
        }
        %aPlayer = %aPlayerName.getByNameField(%this.inspectedGame.PlayerRecords);
        if (isObject(%aPlayer)) {
            %aPlayer.ready = %aReadyValue;
        }
        if (!(%aPlayerName $= $player.getShapeName())) {
            warn("Received a request-to-start update on a player that we don't have a record for! <- " @ getScopeName());
        }
        %i = (%i + 1.0);
    }
    %this.inspectedGame.readyCount = (%i < %playerCount) @ %totalReady;
    GameList.refreshInspectTab();
};
function gameMgrClient::inspectNothing(%this) {
    if (isObject(%this.inspectedGame)) {
        if (!(%this.inspectedGame.isMember(%this.games))) {
            %this.inspectedGame.serversideID = "";
        }
        %this.inspectedGame = "";
    }
    GameList.refreshInspectTab();
};
function gameMgrClient::areWeInspecting(%this) {
    if (!(isObject(%this.inspectedGame))) {
    }
    if ((%this.inspectedGame.serversideID $= "")) {
        return 0;
    }
    return 1;
};
function gameMgrClient::areWeHostOfInspectedGame(%this) {
    if (!(%this.areWeInspecting())) {
        return 0;
    }
    if ((%this.inspectedGame.host $= $player.getShapeName())) {
        return 1;
    }
    return 0;
};
function gameMgrClient::inCustomGame(%this) {
    if (!(%this.areWeInspecting())) {
        return 0;
    }
    if ((%this.inspectedGame.gametype == $gameMgr::CUSTOM_GAME)) {
        return 1;
    }
    return 0;
};
function gameMgrClient::playerJoined(%this, %serversideID, %gname, %gameType, %host, %playerCount, %gamestatus, %playerstatus, %playerRank, %playerScore) {
    echo(getScopeName());
    echo("gameMgrClient: Joined game " @ %serversideID);
    %gamestatus.addGame(%this, %serversideID, %gname, %gameType, %host, %playerCount);
    GameList.refresh();
    GameList.switchIfInspectEmpty();
    if ((%this.inspectedGame.serversideID == %serversideID)) {
        if (!(isObject(%this.inspectedGame.ourRecord))) {
            if ((%playerRank $= "")) {
            }
            if ((%playerScore $= "")) {
                error("on playerJoined call for joining an inspected game, playerRank and playerScore were not passed!<-" @ getScopeName());
            }
            %this.inspectedGame.ourRecord = %playerScore.newPlayerRecord(%this, $player.getShapeName(), %playerstatus, %playerRank);
        }
        error("playerJoined trying to create an ourRecord for inspected game we joined, but it already exists!<-" @ getScopeName());
        GameList.refreshInspectTab();
    }
    if ((%host $= $player.getShapeName())) {
        if ((%gameType == $gameMgr::CUSTOM_GAME)) {
            userTips::showOnceThisSession("CustomGameHost");
        }
        handleSystemMessage("msgInfoMessage", "You created a game: <a:game inspect " @ %serversideID @ ">" @ %gname @ "</a>.");
    }
    handleSystemMessage("msgInfoMessage", "You joined " @ %host @ "'s game: <a:game inspect " @ %serversideID @ ">" @ %gname @ "</a>.");
};
function gameMgrClient::playerLeft(%this, %serversideID, %message) {
    echo(getScopeName());
    echo("gameMgrClient: left game " @ %serversideID);
    %game = %serversideID.getGameBySID(%this);
    %exitMessage = "You left ";
    if ((%game.host $= $player.getShapeName())) {
        %exitMessage = %exitMessage @ " your";
    }
    %exitMessage = %exitMessage @ %game.host @ "'s";
    %exitMessage = %exitMessage @ " game: <a:game inspect " @ %serversideID @ ">" @ %game.gname @ "</a>.";
    if (!(%message $= "")) {
        %exitMessage = %exitMessage @ " " @ %message;
    }
    %serversideID.removeGame(%this);
    GameList.refresh();
    if ((%this.inspectedGame.serversideID == %serversideID)) {
        %this.inspectedGame.ourRecord.delete();
    }
    handleSystemMessage("msgInfoMessage", %exitMessage);
};
function gameMgrClient::playerJoinGame(%this, %serversideID) {
    commandToServer('gameMgrServerInvoke', 'playerJoinGame', %serversideID);
};
function gameMgrClient::playerQuitGame(%this, %serversideID) {
    commandToServer('gameMgrServerInvoke', 'playerQuitGame', %serversideID);
};
function gameMgrClient::requestQuitGameWithName(%this, %gameName) {
    %theGame = %gameName.getByField(%this.games, "gname");
    if (!(isObject(%theGame))) {
        handleSystemMessage("msgInfoMessage", "Sorry, you aren't in a game named " @ %gameName @ ".");
        return;
    }
    %theGame.serversideID.playerQuitGame(%this);
};
function gameMgrClient::playerChangeReadyStatus(%this, %serversideID, %readyStatus) {
    echo(getScopeName());
    commandToServer('gameMgrServerInvoke', 'playerChangeReadyStatus', %serversideID @ " " @ %readyStatus);
};
function gameMgrClient::playerRequestStartGame(%this, %serversideID) {
    echo(getScopeName());
    commandToServer('gameMgrServerInvoke', 'playerRequestStartGame', %serversideID);
};
function gameMgrClient::requestToInspectGame(%this, %serversideID) {
    echo(getScopeName());
    commandToServer('gameMgrServerInvoke', 'inspectGameRequest', %serversideID);
};
function gameMgrClient::requestStartGameWithName(%this, %gameName) {
    %theGame = %gameName.getByField(%this.games, "gname");
    if (!(isObject(%theGame))) {
        handleSystemMessage("msgInfoMessage", "Sorry, you aren't in a game named " @ %gameName @ ".");
        return;
    }
    %theGame.serversideID.playerRequestStartGame(%this);
};
function gameMgrClient::createGame(%this, %gameName, %gameType, %waitingRoom, %joinInProgress, %autoStartOnReady, %dropUnreadyPlayers) {
    echo(getScopeName());
    %argString = %gameName @ "\t" @ %gameType @ "\t" @ %waitingRoom;
    if (%waitingRoom) {
        %argString = %argString @ "\t" @ %joinInProgress @ "\t" @ %autoStartOnReady @ "\t" @ %dropUnreadyPlayers;
    }
    commandToServer('gameMgrServerInvoke', 'playerCreateGame', %argString);
};
function gameMgrClient::playerStopInspectingGame(%this) {
    commandToServer('gameMgrServerInvoke', 'playerStopInspectingGame', "");
};
function gameMgrClient::invitePlayerToInspectedGame(%this, %playerName, %message) {
    if (!(%this.areWeInspecting())) {
        handleSystemMessage("msgInfoMessage", "You've got to have a game inspected to invite someone!");
        return;
    }
    commandToServer('gameMgrServerInvoke', 'playerInvitePlayer', %this.inspectedGame.serversideID @ "\t" @ %playerName @ "\t" @ %message);
};
function gameMgrClient::doHostPopupChangeScore(%this, %playerName) {
    if (!(%this.areWeHostOfInspectedGame())) {
        handleSystemMessage("msgInfoMessage", "You must be inspecting a game you're the host of to change player scores!");
        return;
    }
    "score".open(gameMgrHostPopup, %playerName);
};
function gameMgrClient::doHostPopupChangeStatus(%this, %playerName) {
    if (!(%this.areWeHostOfInspectedGame())) {
        handleSystemMessage("msgInfoMessage", "You must be inspecting a game you're the host of to change a player's status!");
        return;
    }
    "status".open(gameMgrHostPopup, %playerName);
};
function gameMgrClient::hostChangePlayerScore(%this, %playerName, %scoreDelta) {
    if (!(%this.areWeHostOfInspectedGame())) {
        handleSystemMessage("msgInfoMessage", "You must be inspecting a game you're the host of to change player scores!");
        return;
    }
    commandToServer('gameMgrServerInvoke', 'hostChangePlayerScore', %this.inspectedGame.serversideID @ "\t" @ %playerName @ "\t" @ %scoreDelta);
};
function gameMgrClient::hostChangePlayerStatus(%this, %playerName, %newStatus) {
    if (!(%this.areWeHostOfInspectedGame())) {
        handleSystemMessage("msgInfoMessage", "You must be inspecting a game you're the host of to change a player's status!");
        return;
    }
    commandToServer('gameMgrServerInvoke', 'hostChangePlayerStatus', %this.inspectedGame.serversideID @ "\t" @ %playerName @ "\t" @ %newStatus);
};
function gameMgrClient::areWePlaying(%this, %gameObj) {
    return %gameObj.isMember(%this.games);
};
function gameMgrClient::getGameBySID(%this, %serversideID) {
    echo(getScopeName());
    %n = (%this.games.getCount() - 1.0);
    while ((%n >= 0.0)) {
        %aGame = %n.getObject(%this.games);
        if ((%aGame.serversideID == %serversideID)) {
            return %aGame;
        }
        %n = (%n - 1.0);
    }
    return "";
};
function gameMgrClient::addGame(%this, %serversideID, %gname, %gameType, %host, %playerCount, %gamestatus) {
    echo(getScopeName());
    if (isObject(%this.inspectedGame)) {
    }
    if ((%this.inspectedGame.serversideID == %serversideID)) {
        %newGame = %this.inspectedGame;
        if (!(%gname $= %this.inspectedGame.gname)) {
        }
        if ((%gameType != %this.inspectedGame.gametype)) {
            error("Serious error! gameMgrClient::addGame called with same serversideID as inspectedGame but gname and gametype don't match! Not overriding!");
        }
        %newGame.playercount = %playerCount;
        %newGame.gamestatus = %gamestatus;
    }
    %newGame = new ScriptObject("") {
        serversideID = %serversideID;
        gname = %gname;
        gametype = %gameType;
        host = %host;
        playercount = %playerCount;
        gamestatus = %gamestatus;
        deepUpdate = 0;
    };
    if (isObject(MissionCleanup)) {
        %newGame.add(MissionCleanup);
    }
    %newGame.add(%this.games);
    %listFound = 0;
    %n = (GameList.lists.getCount() - 1.0);
    while ((%n >= 0.0)) {
        %aList = %n.getObject(GameList.lists);
        if ((%aList.gametype == %newGame.gametype)) {
            %newGame.add(%aList);
            %listFound = 1;
        }
        %n = (%n - 1.0);
    }
    if ((%listFound <= 0.0)) {
        %newList = new SimSet("") {
            gametype = (%n >= 0.0) @ %newGame.gametype;
            collapsed = 0;
        };
        if (isObject(MissionCleanup)) {
            %newList.add(MissionCleanup);
        }
        %newGame.add(%newList);
        %newList.add(GameList.lists);
    }
};
function gameMgrClient::removeGame(%this, %serversideID) {
    echo(getScopeName());
    if ((%this.games.getCount() == 0.0)) {
        error("trying to delete a game but gameMgrClient.games is empty! <- " @ getScopeName());
    }
    %theGame = %serversideID.getGameBySID(%this);
    if (((%index = %theGame.getObjectIndex(%this.games)) == -(1.0))) {
        error("Trying to delete record of a game that we don't have! (not in gameMgrClient.games) <-" @ getScopeName);
        return;
    }
    %theGame.remove(%this.games);
    %n = (GameList.lists.getCount() - 1.0);
    while ((%n >= 0.0)) {
        %aList = %n.getObject(GameList.lists);
        if ((%aList.gametype == %theGame.gametype)) {
            if ((%theGame.getObjectIndex(%aList) == -(1.0))) {
                error("Deleting a game who isn't listed in his gametype! <-" @ getScopeName());
            }
            %theGame.remove(%aList);
            if ((%aList.getCount() == 0.0)) {
                %aList.remove(GameList.lists);
                %aList.delete();
            }
        }
        if ((%n == 0.0)) {
            error("Deleting a game whose gametype didn't have a list <- " @ getScopeName());
        }
        %n = (%n - 1.0);
    }
    if ((%theGame != %this.inspectedGame)) {
        %theGame.delete();
    }
};
function gameMgrClient::startFresh(%this) {
    GameList.lists.deleteMembers();
    %this.games.deleteMembers();
    if (isObject(%this.inspectedGame)) {
        %this.inspectedGame.PlayerRecords.deleteMembers();
        %this.inspectedGame.PlayerRecords.delete();
        %this.inspectedGame.delete();
    }
};
function gameMgrClient::newPlayerRecord(%this, %playerName, %playerstatus, %playerready, %playerScore) {
    %ret = new ScriptObject("") {
        name = %playerName;
        status = %playerstatus;
        ready = %playerready;
        score = %playerScore;
    };
    if (isObject(MissionCleanup)) {
        %ret.add(MissionCleanup);
    }
    return %ret;
};
function gameMgrClient::sortAndPurgePlayerRecords(%this, %aGameObj) {
    if (!(isObject(%aGameObj))) {
    }
    if (!(isObject(%aGameObj.PlayerRecords))) {
        error("Tried to sort PlayerRecords on a game that didn't exist or didn't have PlayerRecords! <-" @ getScopeName());
        return;
    }
    %numRecords = %aGameObj.PlayerRecords.getCount();
    if ((%numRecords == 0.0)) {
        return;
    }
    %stringToSort = "";
    echo("sortAndPurgePlayerRecords working with PlayerRecords count = " @ %aGameObj.PlayerRecords.getCount());
    %n = (%numRecords - 1.0);
    while ((%n >= 0.0)) {
        %curRecord = %n.getObject(%aGameObj.PlayerRecords);
        %stringToSort = trim(%stringToSort @ " " @ formatInt("%0." @ $gameMgr::MAX_SCORE_DIGITS @ "d", %curRecord.score) @ "\t" @ %curRecord.getId());
        %n = (%n - 1.0);
    }
    %stringToSort = SortWords(%stringToSort);
    (%n >= 0.0);
    %count = getFieldCount(%stringToSort);
    if (((%count - 1.0) != %numRecords)) {
        error("Serious error! All the player records didn't fit in the string to be sorted! Not sorting! <- " @ getScopeName());
        return;
    }
    1.clear(%aGameObj.PlayerRecords);
    %n = (%count - 1.0);
    while ((%n >= 1.0)) {
        %recordID = trim(getWord(getField(%stringToSort, %n), 0));
        if (isObject(%recordID)) {
            if ((%n >= (%count - $gameMgr::InspectTab::MAX_PLAYERS))) {
                %recordID.add(%aGameObj.PlayerRecords);
                %recordID.pushToBack(%aGameObj.PlayerRecords);
            }
            %recordID.delete();
        }
        error("Serious error! Record ID retrieved from sorted string invalid! Skipping and continuing <-" @ getScopeName());
        %n = (%n - 1.0);
    }
};
function gameMgrHostPopup::setup(%this) {
    gameMgrHostPopup.variText = new GuiMLTextCtrl("") {
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
    gameMgrHostPopup.variText.add(gameMgrHostPopup);
    gameMgrHostPopup.textField = new GuiTextEditCtrl("") {
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
    gameMgrHostPopup.textField.add(gameMgrHostPopup);
    gameMgrHostPopup.applyButton = new GuiVariableWidthButtonCtrl("") {
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
    gameMgrHostPopup.applyButton.add(gameMgrHostPopup);
    gameMgrHostPopup.isSetup = 1;
};
function gameMgrHostPopup::close(%this) {
    0.setVisible(%this);
};
function gameMgrHostPopup::open(%this, %playerName, %command) {
    if (!(%this.isSetup)) {
        %this.setup();
    }
    if ((%command $= "score")) {
        "<spush><b>Change " @ %playerName @ "'s score by:<spop>(e.g. +5, -10)".setText(%this.variText);
    }
    if ((%command $= "status")) {
        "<spush><b>Set " @ %playerName @ "'s status to:<spop>".setText(%this.variText);
    }
    error("gameMgrHostPopup::apply was passed a unrecognized command " @ %command @ ".<-" @ getScopeName());
    return;
    %this.command = %command;
    %this.playerName = %playerName;
    1.setVisible(%this);
};
function gameMgrHostPopup::apply(%this) {
    if ((%this.command $= "score")) {
        %this.textField.getText().hostChangePlayerScore(gameMgrClient, %this.playerName);
    }
    if ((%this.command $= "status")) {
        %this.textField.getText().hostChangePlayerStatus(gameMgrClient, %this.playerName);
    }
    error("gameMgrHostPopup::apply was passed a unrecognized command " @ %this.command @ ".<-" @ getScopeName());
};
