if (!(isObject(gameMgrClient))) {
    $gameMgrClient = new ScriptObject(gameMgrClient);;
    if (isObject(MissionCleanup)) {
        $gameMgrClient.add();
    }
    games = new ""(); @ gameMgrClient;
    SimSet;
    if (isObject(MissionCleanup)) {
        games.getId().add();
    }
}
function ClientCmdGameMgrClientNotify(%command, %arguments) {
    echo(getScopeName() @ "->\"" @ detag(%command) @ "\" with %arguments==" @ %arguments);
    if ((gameMgrClient @ " " @ detag(%command) $= "playerInvited")) {
        getField(%arguments, 0).playerInvited(getField(%arguments, 1), getField(%arguments, 2), getRecord(%arguments, 1), getRecords(%arguments, 2));
    }
    if ((gameMgrClient @ " " @ detag(%command) $= "playerJoined")) {
        getField(%arguments, 0).playerJoined(getField(%arguments, 1), getField(%arguments, 2), getField(%arguments, 3), getField(%arguments, 4), getField(%arguments, 5), getField(%arguments, 6), getField(%arguments, 7), getField(%arguments, 8));
    }
    if ((gameMgrClient @ " " @ detag(%command) $= "playerLeft")) {
        getField(%arguments, 0).playerLeft(getField(%arguments, 1));
    }
    if ((gameMgrClient @ " " @ detag(%command) $= "inspectNewGame")) {
        getField(%arguments, 0).inspectNewGame(getField(%arguments, 1), getField(%arguments, 2), getField(%arguments, 3), getField(%arguments, 4), getField(%arguments, 5), getField(%arguments, 6), getField(%arguments, 7), getField(%arguments, 8), getField(%arguments, 9));
    }
    if ((gameMgrClient @ " " @ detag(%command) $= "inspectAddRemovePlayers")) {
        getField(%arguments, 0).inspectAddRemovePlayers(getRecord(%arguments, 1), getRecord(%arguments, 2), getRecord(%arguments, 3), getRecord(%arguments, 4));
    }
    if ((gameMgrClient @ " " @ detag(%command) $= "inspectAddPlayers")) {
        getField(%arguments, 0).inspectAddRemovePlayers(getRecord(%arguments, 1), "", getRecord(%arguments, 2), "");
    }
    if ((gameMgrClient @ " " @ detag(%command) $= "inspectRemovePlayers")) {
        getField(%arguments, 0).inspectAddRemovePlayers("", getRecord(%arguments, 1), getRecord(%arguments, 2), getRecord(%arguments, 3));
    }
    if ((gameMgrClient @ " " @ detag(%command) $= "inspectChangeReadyStatus")) {
        getWord(%arguments, 0).inspectChangeReadyStatus(getRecord(%arguments, 1), getRecord(%arguments, 2));
    }
    if ((gameMgrClient @ " " @ detag(%command) $= "inspectUpdatePlayersStatus")) {
        getField(%arguments, 0).inspectUpdatePlayersStatus(getRecord(%arguments, 1));
    }
    if ((gameMgrClient @ " " @ detag(%command) $= "superficialAddPlayers")) {
        getField(%arguments, 0).superficialAddPlayers(getField(%arguments, 1), getField(%arguments, 2));
    }
    if ((gameMgrClient @ " " @ detag(%command) $= "superficialAddRemovePlayers")) {
        getField(%arguments, 0).superficialAddRemovePlayers(getField(%arguments, 1));
    }
    if ((gameMgrClient @ " " @ detag(%command) $= "gamestatusChanged")) {
        getField(%arguments, 0).gamestatusChanged(getField(%arguments, 1));
    }
    if ((gameMgrClient @ " " @ detag(%command) $= "deepDetailUpdated")) {
        getField(%arguments, 0).deepDetailUpdated();
    }
    if ((gameMgrClient @ " " @ detag(%command) $= "inspectNothing")) {
        gameMgrClient.inspectNothing();
    }
};
function putListIntoEnglish(%list) {
    %ret = "";
    %fieldCount = getFieldCount(%list);
    if ((2.0 == %fieldCount)) {
        %ret = getField(%list, 0) @ " and " @ getField(%list, 1);
    }
    %n = 0;
    if ((%printedFieldCount < %n)) {
        %item = getField(%list, %n);
        if ((0.0 == %n)) {
            %ret = %item;
        }
        if (((1.0 - %fieldCount) < %n)) {
            %ret = %ret @ ", " @ %item;
        }
        %ret = %ret @ ", and " @ %item @ ".";
        %n = (1.0 + %n);
    }
};
function gameMgrClient::playerInvited(%this, %inviter, %aGameInstance, %gameType, %playerCount, %message) {
    echo(getScopeName());
    echo("gameMgrClient: Game invitation from " @ %inviter @ ": come join " @ %aGameInstance @ ".");
    %msg = getPlayerMarkup(%inviter, "", 1) @ " invites you to play <a:game inspect " @ %aGameInstance @ ">" @ %gameType[$gameMgr::GAME_TYPES @ %gameType].title @ "</a>";
    if ((2.0 > %playerCount)) {
        %msg = %msg @ " with " @ (1.0 - %playerCount) @ " others.";
    }
    %msg = %msg @ ".";
    if (!(%message $= "")) {
        %msg = %msg @ " " @ %message;
    }
    handleSystemMessage("msgInfoMessage", %msg);
};
function gameMgrClient::superficialGameUpdate(%this, %serversideID, %playerCount, %gamestatus, %deepUpdate, %postponeUpdate) {
    echo(getScopeName());
    %ourCopy = %this.getGameBySID(%serversideID);
    if (!(isObject(%ourCopy))) {
        warn("Server tried to send us a superficial update on a game we don't have a record for! <- " @ getScopeName());
        return;
    }
    %ourCopy.playercount = %playerCount;
    %ourCopy.gamestatus = %gamestatus;
    %ourCopy.deepUpdate = %deepUpdate;
    if ((1.0 != %postponeUpdate)) {
        GameList.refresh();
    }
};
function gameMgrClient::superficialAddPlayers(%this, %serversideID, %playerCount, %newTotal) {
    echo(getScopeName());
    %ourCopy = %this.getGameBySID(%serversideID);
    if (isObject(%ourCopy)) {
        %ourCopy.playercount = %newTotal;
        GameList.refresh();
    }
    error("Received superficial player add update for game we don't have a record of! <- " @ getScopeName());
};
function gameMgrClient::superficialAddRemovePlayers(%this, %serversideID, %newTotal) {
    echo(getScopeName());
    %ourCopy = %this.getGameBySID(%serversideID);
    if (isObject(%ourCopy)) {
        %ourCopy.playercount = %newTotal;
        GameList.refresh();
    }
    error("Received superficial player add/remove update for game we don't have a record of! <- " @ getScopeName());
};
function gameMgrClient::gamestatusChanged(%this, %serversideID, %newStatus) {
    echo(getScopeName());
    %ourCopy = %this.getGameBySID(%serversideID);
    if (isObject(%ourCopy)) {
        %ourCopy.gamestatus = %newStatus;
        GameList.refresh();
    }
    if ((%serversideID == %this.inspectedGame.serversideID)) {
        %this.inspectedGame.gamestatus = %newStatus;
        GameList.refreshInspectTab();
    }
    if (!(isObject(%ourCopy))) {
        error("Received gamestatus update for a game we don't have a record of and aren't inspecting! <- " @ getScopeName());
    }
};
function gameMgrClient::deepDetailUpdated(%this, %serversideID) {
    echo(getScopeName());
    %ourCopy = %this.getGameBySID(%serversideID);
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
    %ourCopy = %this.getGameBySID(%serversideID);
    if (isObject(%ourCopy)) {
        if (!(%ourCopy.gname $= %gname)) {
        }
        if (!(%ourCopy.gametype $= %gameType)) {
            error("Existing game record has different name or type than latest record! This should never happen and is bad news. Returning <-" @ getScopeName());
            return;
        }
        if (isObject(%this.inspectedGame)) {
        }
        if (!(%this.games.isMember(%this.inspectedGame))) {
            %this.inspectedGame.PlayerRecords.delete();
            if (isObject(%this.inspectedGame.ourRecord)) {
                %this.inspectedGame.ourRecord.delete();
            }
            %this.inspectedGame.delete();
        }
        %this.inspectedGame = %ourCopy;
        if (!(isObject(%this.inspectedGame.PlayerRecords))) {
            %this.inspectedGame.PlayerRecords = SimSet @ new ""();;
            0;
        }
    }
    if (!(isObject(%this.inspectedGame))) {
        0;
        %this.inspectedGame = ScriptObject @ new ""() {
            PlayerRecords = SimSet @ new ""();;
        };
        0;
        if (isObject(MissionCleanup)) {
            %this.inspectedGame.getId().add();
            %this.inspectedGame.PlayerRecords.getId().add();
        }
    }
    %this.inspectedGame.PlayerRecords.deleteMembers();
    %this.inspectedGame.serversideID = MissionCleanup @ %serversideID;
    MissionCleanup;
    %this.inspectedGame.deepUpdated = 1;
    %this.inspectedGame.gname = %gname;
    %this.inspectedGame.gametype = %gameType;
    %this.inspectedGame.gamestatus = %gamestatus;
    %this.inspectedGame.playercount = %playerCount;
    %this.inspectedGame.readyCount = %readyCount;
    %this.inspectedGame.host = %gamehost;
    %this.inspectedGame.ourRecord = "";
    if (isObject(%ourCopy)) {
        %this.inspectedGame.ourRecord = %this.newPlayerRecord($player.getShapeName(), %playerstatus, 0, %playerScore);
        %this.inspectedGame.ourRecord.rank = %playerRank;
    }
    GameList.refreshInspectTab();
    "INSPECT".selectTabWithName();
    commandToServer('gameMgrServerInvoke', 'sendAllPlayersFor', %serversideID);
    GameList.refresh();
};
function gameMgrClient::inspectUpdatePlayersStatus(%this, %serversideID, %playerValues, %dontSortAndRefresh) {
    echo(getScopeName());
    if ((%serversideID != %this.inspectedGame.serversideID)) {
        %ourCopy = %this.getGameBySID(%serversideID);
        if (isObject(%ourCopy)) {
            %ourCopy.deepUpdated = 1;
        }
        warn("Received player(s) status update for game we're not inspecting! <- " @ getScopeName());
        return;
    }
    %this.inspectedGame.deepUpdated = 1;
    %playersCount = (4.0 / getFieldCount(%playerValues));
    echo(getScopeName() @ "-> updating " @ %playersCount @ " players.");
    %i = 0;
    if ((%playersCount < %i)) {
        %playerIdx = (4.0 * %i);
        %aPlayerName = getField(%playerValues, %playerIdx);
        %playerstatus = getField(%playerValues, (1.0 + %playerIdx));
        %playerready = getField(%playerValues, (2.0 + %playerIdx));
        %playerScore = getField(%playerValues, (3.0 + %playerIdx));
        echo(getScopeName() @ "-> searching for record with name=" @ %aPlayerName);
        %theRecord = %this.inspectedGame.PlayerRecords.getByNameField(%aPlayerName);
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
        %this.inspectedGame.PlayerRecords.add(%aPlayerName.newPlayerRecord(%playerstatus, %playerready, %playerScore));
        if ((gameMgrClient @ " " @ %aPlayerName $= $player.getShapeName())) {
            if (!(isObject(%this.inspectedGame.ourRecord))) {
                %this.inspectedGame.ourRecord = %this.newPlayerRecord(%aPlayerName, %playerstatus, %playerready, %playerScore);
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
        %i = (1.0 + %i);
    }
    if (((%playersCount < %i) @ " " @ %dontSortAndRefresh $= "")) {
        %this.sortAndPurgePlayerRecords(%this.inspectedGame);
        GameList.refreshInspectTab();
    }
};
function gameMgrClient::inspectAddRemovePlayers(%this, %serversideID, %playersAdded, %playersRemoved, %totalPlayersRemaining, %totalReadyPlayersRemaining) {
    echo(getScopeName());
    if ((%serversideID != %this.inspectedGame.serversideID)) {
        %ourCopy = %this.getGameBySID(%serversideID);
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
        %this.inspectUpdatePlayersStatus(%serversideID, %playersAdded, 1);
    }
    %i = (1.0 - getFieldCount(%playersRemoved));
    if ((0.0 >= %i)) {
        %aPlayerName = getField(%playersRemoved, %i);
        %aPlayer = %this.inspectedGame.PlayerRecords.getByNameField(%aPlayerName);
        if (isObject(%aPlayer)) {
            %this.inspectedGame.PlayerRecords.remove(%aPlayer);
            %aPlayer.delete();
        }
        warn("Asked to remove a player that we didn't have a record of! <- " @ getScopeName());
        %i = (1.0 - %i);
    }
    %this.sortAndPurgePlayerRecords(%this.inspectedGame);
    GameList.refreshInspectTab();
};
function gameMgrClient::inspectChangeReadyStatus(%this, %serversideID, %readyValues, %totalReady) {
    echo(getScopeName());
    if ((%serversideID != %this.inspectedGame.serversideID)) {
        warn("Received inspect changeReadyStatus update for a game we're not inspecting! <-" @ getScopeName());
        return;
    }
    %playerCount = (2.0 / getFieldCount(%readyValues));
    %i = 0;
    if ((%playerCount < %i)) {
        %playerIdx = (2.0 * %i);
        %aPlayerName = getField(%readyValues, %playerIdx);
        %aReadyValue = getField(%readyValues, (1.0 + %playerIdx));
        if ((%aPlayerName $= $player.getShapeName())) {
            %this.inspectedGame.ourRecord.ready = %aReadyValue;
        }
        %aPlayer = %this.inspectedGame.PlayerRecords.getByNameField(%aPlayerName);
        if (isObject(%aPlayer)) {
            %aPlayer.ready = %aReadyValue;
        }
        if (!(%aPlayerName $= $player.getShapeName())) {
            warn("Received a request-to-start update on a player that we don't have a record for! <- " @ getScopeName());
        }
        %i = (1.0 + %i);
    }
    %this.inspectedGame.readyCount = (%playerCount < %i) @ %totalReady;
    GameList.refreshInspectTab();
};
function gameMgrClient::inspectNothing(%this) {
    if (isObject(%this.inspectedGame)) {
        if (!(%this.games.isMember(%this.inspectedGame))) {
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
    if (($gameMgr::CUSTOM_GAME == %this.inspectedGame.gametype)) {
        return 1;
    }
    return 0;
};
function gameMgrClient::playerJoined(%this, %serversideID, %gname, %gameType, %host, %playerCount, %gamestatus, %playerstatus, %playerRank, %playerScore) {
    echo(getScopeName());
    echo("gameMgrClient: Joined game " @ %serversideID);
    %this.addGame(%serversideID, %gname, %gameType, %host, %playerCount, %gamestatus);
    GameList.refresh();
    GameList.switchIfInspectEmpty();
    if ((%serversideID == %this.inspectedGame.serversideID)) {
        if (!(isObject(%this.inspectedGame.ourRecord))) {
            if ((%playerRank $= "")) {
            }
            if ((%playerScore $= "")) {
                error("on playerJoined call for joining an inspected game, playerRank and playerScore were not passed!<-" @ getScopeName());
            }
            %this.inspectedGame.ourRecord = %this.newPlayerRecord($player.getShapeName(), %playerstatus, %playerRank, %playerScore);
        }
        error("playerJoined trying to create an ourRecord for inspected game we joined, but it already exists!<-" @ getScopeName());
        GameList.refreshInspectTab();
    }
    if ((%host $= $player.getShapeName())) {
        if (($gameMgr::CUSTOM_GAME == %gameType)) {
            userTips::showOnceThisSession("CustomGameHost");
        }
        handleSystemMessage("msgInfoMessage", "You created a game: <a:game inspect " @ %serversideID @ ">" @ %gname @ "</a>.");
    }
    handleSystemMessage("msgInfoMessage", "You joined " @ %host @ "'s game: <a:game inspect " @ %serversideID @ ">" @ %gname @ "</a>.");
};
function gameMgrClient::playerLeft(%this, %serversideID, %message) {
    echo(getScopeName());
    echo("gameMgrClient: left game " @ %serversideID);
    %game = %this.getGameBySID(%serversideID);
    %exitMessage = "You left ";
    if ((%game.host $= $player.getShapeName())) {
        %exitMessage = %exitMessage @ " your";
    }
    %exitMessage = %exitMessage @ %game.host @ "'s";
    %exitMessage = %exitMessage @ " game: <a:game inspect " @ %serversideID @ ">" @ %game.gname @ "</a>.";
    if (!(%message $= "")) {
        %exitMessage = %exitMessage @ " " @ %message;
    }
    %this.removeGame(%serversideID);
    GameList.refresh();
    if ((%serversideID == %this.inspectedGame.serversideID)) {
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
    %theGame = %this.games.getByField("gname", %gameName);
    if (!(isObject(%theGame))) {
        handleSystemMessage("msgInfoMessage", "Sorry, you aren't in a game named " @ %gameName @ ".");
        return;
    }
    %this.playerQuitGame(%theGame.serversideID);
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
    %theGame = %this.games.getByField("gname", %gameName);
    if (!(isObject(%theGame))) {
        handleSystemMessage("msgInfoMessage", "Sorry, you aren't in a game named " @ %gameName @ ".");
        return;
    }
    %this.playerRequestStartGame(%theGame.serversideID);
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
    %playerName.open("score");
};
function gameMgrClient::doHostPopupChangeStatus(%this, %playerName) {
    if (!(%this.areWeHostOfInspectedGame())) {
        handleSystemMessage("msgInfoMessage", "You must be inspecting a game you're the host of to change a player's status!");
        return;
    }
    %playerName.open("status");
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
    return %this.games.isMember(%gameObj);
};
function gameMgrClient::getGameBySID(%this, %serversideID) {
    echo(getScopeName());
    %n = (1.0 - %this.games.getCount());
    if ((0.0 >= %n)) {
        %aGame = %this.games.getObject(%n);
        if ((%serversideID == %aGame.serversideID)) {
            return %aGame;
        }
        %n = (1.0 - %n);
    }
    return "";
};
function gameMgrClient::addGame(%this, %serversideID, %gname, %gameType, %host, %playerCount, %gamestatus) {
    echo(getScopeName());
    if (isObject(%this.inspectedGame)) {
    }
    if ((%serversideID == %this.inspectedGame.serversideID)) {
        %newGame = %this.inspectedGame;
        if (!(%gname $= %this.inspectedGame.gname)) {
        }
        if ((%this.inspectedGame.gametype != %gameType)) {
            error("Serious error! gameMgrClient::addGame called with same serversideID as inspectedGame but gname and gametype don't match! Not overriding!");
        }
        %newGame.playercount = %playerCount;
        %newGame.gamestatus = %gamestatus;
    }
    0;
    %newGame = new ""() {
        serversideID = ScriptObject @ %serversideID;
        gname = %gname;
        gametype = %gameType;
        host = %host;
        playercount = %playerCount;
        gamestatus = %gamestatus;
        deepUpdate = 0;
    };
    if (isObject(MissionCleanup)) {
        %newGame.add();
    }
    %this.games.add(%newGame);
    %listFound = 0;
    MissionCleanup;
    %n = (GameList - %this.lists.getCount());
    1.0;
    if ((0.0 >= %n)) {
        %aList = %this.lists.getObject(%n);
        GameList;
        if ((%newGame.gametype == %aList.gametype)) {
            %aList.add(%newGame);
            %listFound = 1;
        }
        %n = (1.0 - %n);
    }
    if ((0.0 <= %listFound)) {
        0;
        %newList = new ""() {
            gametype = SimSet @ %newGame.gametype;
            collapsed = (0.0 >= %n) @ 0;
        };
        if (isObject(MissionCleanup)) {
            %newList.add();
        }
        %newList.add(%newGame);
        lists.add(%newList);
    }
};
function gameMgrClient::removeGame(%this, %serversideID) {
    echo(getScopeName());
    if ((0.0 == %this.games.getCount())) {
        error("trying to delete a game but gameMgrClient.games is empty! <- " @ getScopeName());
    }
    %theGame = %this.getGameBySID(%serversideID);
    %index = %this.games.getObjectIndex(%theGame);
    if (( == -(1.0))) {
        error(getScopeName);
        return "Trying to delete record of a game that we don't have! (not in gameMgrClient.games) <-";
    }
    %this.games.remove(%theGame);
    %n = (GameList - %this.lists.getCount());
    1.0;
    if ((0.0 >= %n)) {
        %aList = %this.lists.getObject(%n);
        GameList;
        if ((%theGame.gametype == %aList.gametype)) {
            if ((-(1.0) == %aList.getObjectIndex(%theGame))) {
                error("Deleting a game who isn't listed in his gametype! <-" @ getScopeName());
            }
            %aList.remove(%theGame);
            if ((0.0 == %aList.getCount())) {
                %aList.lists.remove(%aList);
                %aList.delete();
            }
        }
        if ((0.0 == %n)) {
            error("Deleting a game whose gametype didn't have a list <- " @ getScopeName());
        }
        %n = (1.0 - %n);
        GameList;
    }
    if ((%this.inspectedGame != %theGame)) {
        %theGame.delete();
    }
};
function gameMgrClient::startFresh(%this) {
    %this.lists.deleteMembers();
    %this.games.deleteMembers();
    if (isObject(%this.inspectedGame)) {
        %this.inspectedGame.PlayerRecords.deleteMembers();
        %this.inspectedGame.PlayerRecords.delete();
        %this.inspectedGame.delete();
    }
};
function gameMgrClient::newPlayerRecord(%this, %playerName, %playerstatus, %playerready, %playerScore) {
    0;
    %ret = new ""() {
        name = ScriptObject @ %playerName;
        status = %playerstatus;
        ready = %playerready;
        score = %playerScore;
    };
    if (isObject(MissionCleanup)) {
        %ret.add();
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
    if ((0.0 == %numRecords)) {
        return;
    }
    %stringToSort = "";
    echo("sortAndPurgePlayerRecords working with PlayerRecords count = " @ %aGameObj.PlayerRecords.getCount());
    %n = (1.0 - %numRecords);
    if ((0.0 >= %n)) {
        %curRecord = %aGameObj.PlayerRecords.getObject(%n);
        %stringToSort = trim(%stringToSort @ " " @ formatInt("%0." @ $gameMgr::MAX_SCORE_DIGITS @ "d", %curRecord.score) @ "\t" @ %curRecord.getId());
        %n = (1.0 - %n);
    }
    %stringToSort = SortWords(%stringToSort);
    (0.0 >= %n);
    %count = getFieldCount(%stringToSort);
    if ((%numRecords != (1.0 - %count))) {
        error("Serious error! All the player records didn't fit in the string to be sorted! Not sorting! <- " @ getScopeName());
        return;
    }
    %aGameObj.PlayerRecords.clear(1);
    %n = (1.0 - %count);
    if ((1.0 >= %n)) {
        %recordID = trim(getWord(getField(%stringToSort, %n), 0));
        if (isObject(%recordID)) {
            if ((($gameMgr::InspectTab::MAX_PLAYERS - %count) >= %n)) {
                %aGameObj.PlayerRecords.add(%recordID);
                %aGameObj.PlayerRecords.pushToBack(%recordID);
            }
            %recordID.delete();
        }
        error("Serious error! Record ID retrieved from sorted string invalid! Skipping and continuing <-" @ getScopeName());
        %n = (1.0 - %n);
    }
};
function gameMgrHostPopup::setup(%this) {
    0;
    variText = new ""() {
        profile = GuiMLTextCtrl @ "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "12 28";
        extent = "220 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = -1;
    }; @ gameMgrHostPopup
    variText.add();
    0;
    gameMgrHostPopup;
    textField = new ""() {
        profile = GuiTextEditCtrl @ "ETSDarkTextEditProfile";
        horizSizing = gameMgrHostPopup @ "right";
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
    }; @ gameMgrHostPopup
    textField.add();
    0;
    gameMgrHostPopup;
    applyButton = new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton15Profile";
        horizSizing = gameMgrHostPopup @ "right";
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
    }; @ gameMgrHostPopup
    applyButton.add();
    isSetup = 1 @ gameMgrHostPopup;
    gameMgrHostPopup;
};
function gameMgrHostPopup::close(%this) {
    %this.setVisible(0);
};
function gameMgrHostPopup::open(%this, %playerName, %command) {
    if (!(%this.isSetup)) {
        %this.setup();
    }
    if ((%command $= "score")) {
        %this.variText.setText("<spush><b>Change " @ %playerName @ "'s score by:<spop>(e.g. +5, -10)");
    }
    if ((%command $= "status")) {
        %this.variText.setText("<spush><b>Set " @ %playerName @ "'s status to:<spop>");
    }
    error("gameMgrHostPopup::apply was passed a unrecognized command " @ %command @ ".<-" @ getScopeName());
    return;
    %this.command = %command;
    %this.playerName = %playerName;
    %this.setVisible(1);
};
function gameMgrHostPopup::apply(%this) {
    if ((%this.command $= "score")) {
        %this.playerName.hostChangePlayerScore(%this.textField.getText());
    }
    if ((gameMgrClient @ " " @ %this.command $= "status")) {
        %this.playerName.hostChangePlayerStatus(%this.textField.getText());
    }
    error("gameMgrHostPopup::apply was passed a unrecognized command " @ %this.command @ ".<-" @ getScopeName());
};
