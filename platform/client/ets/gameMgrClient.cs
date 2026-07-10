if (!(isObject())) {
    $gameMgrClient = new ScriptObject(gameMgrClient);
    gameMgrClient;
    if (isObject()) {
        $gameMgrClient.add();
    }
    games = SimSet @ new ""() @ gameMgrClient;
    0;
    if (isObject()) {
        games.getId().add();
    }
}
function ClientCmdGameMgrClientNotify(%command, %arguments) {
    echo(MissionCleanup @ MissionCleanup @ MissionCleanup @ gameMgrClient @ getScopeName() @ "->\"" @ detag(%command) @ "\" with %arguments==" @ %arguments);
    if ((MissionCleanup SPC detag(%command) $= "playerInvited")) {
        getField(%arguments, 0).playerInvited(getField(%arguments, 1), getField(%arguments, 2), getRecord(%arguments, 1), getRecords(%arguments, 2));
    }
    if ((gameMgrClient SPC detag(%command) $= "playerJoined")) {
        getField(%arguments, 0).playerJoined(getField(%arguments, 1), getField(%arguments, 2), getField(%arguments, 3), getField(%arguments, 4), getField(%arguments, 5), getField(%arguments, 6), getField(%arguments, 7), getField(%arguments, 8));
    }
    if ((gameMgrClient SPC detag(%command) $= "playerLeft")) {
        getField(%arguments, 0).playerLeft(getField(%arguments, 1));
    }
    if ((gameMgrClient SPC detag(%command) $= "inspectNewGame")) {
        getField(%arguments, 0).inspectNewGame(getField(%arguments, 1), getField(%arguments, 2), getField(%arguments, 3), getField(%arguments, 4), getField(%arguments, 5), getField(%arguments, 6), getField(%arguments, 7), getField(%arguments, 8), getField(%arguments, 9));
    }
    if ((gameMgrClient SPC detag(%command) $= "inspectAddRemovePlayers")) {
        getField(%arguments, 0).inspectAddRemovePlayers(getRecord(%arguments, 1), getRecord(%arguments, 2), getRecord(%arguments, 3), getRecord(%arguments, 4));
    }
    if ((gameMgrClient SPC detag(%command) $= "inspectAddPlayers")) {
        getField(%arguments, 0).inspectAddRemovePlayers(getRecord(%arguments, 1), "", getRecord(%arguments, 2), "");
    }
    if ((gameMgrClient SPC detag(%command) $= "inspectRemovePlayers")) {
        getField(%arguments, 0).inspectAddRemovePlayers("", getRecord(%arguments, 1), getRecord(%arguments, 2), getRecord(%arguments, 3));
    }
    if ((gameMgrClient SPC detag(%command) $= "inspectChangeReadyStatus")) {
        getWord(%arguments, 0).inspectChangeReadyStatus(getRecord(%arguments, 1), getRecord(%arguments, 2));
    }
    if ((gameMgrClient SPC detag(%command) $= "inspectUpdatePlayersStatus")) {
        getField(%arguments, 0).inspectUpdatePlayersStatus(getRecord(%arguments, 1));
    }
    if ((gameMgrClient SPC detag(%command) $= "superficialAddPlayers")) {
        getField(%arguments, 0).superficialAddPlayers(getField(%arguments, 1), getField(%arguments, 2));
    }
    if ((gameMgrClient SPC detag(%command) $= "superficialAddRemovePlayers")) {
        getField(%arguments, 0).superficialAddRemovePlayers(getField(%arguments, 1));
    }
    if ((gameMgrClient SPC detag(%command) $= "gamestatusChanged")) {
        getField(%arguments, 0).gamestatusChanged(getField(%arguments, 1));
    }
    if ((gameMgrClient SPC detag(%command) $= "deepDetailUpdated")) {
        getField(%arguments, 0).deepDetailUpdated();
    }
    if ((gameMgrClient SPC detag(%command) $= "inspectNothing")) {
        inspectNothing();
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
    %msg = getPlayerMarkup(%inviter, "", 1) @ " invites you to play <a:game inspect " @ %aGameInstance @ ">" @ %gameType[$gameMgr::GAME_TYPES @ %gameType] @ title @ "</a>";
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
    playercount = %playerCount @ %ourCopy;
    gamestatus = %gamestatus @ %ourCopy;
    deepUpdate = %deepUpdate @ %ourCopy;
    if ((1.0 != %postponeUpdate)) {
        refresh();
    }
};
function gameMgrClient::superficialAddPlayers(%this, %serversideID, %playerCount, %newTotal) {
    echo(getScopeName());
    %ourCopy = %this.getGameBySID(%serversideID);
    if (isObject(%ourCopy)) {
        playercount = %newTotal @ %ourCopy;
        refresh();
    }
    error(GameList @ "Received superficial player add update for game we don't have a record of! <- " @ getScopeName());
};
function gameMgrClient::superficialAddRemovePlayers(%this, %serversideID, %newTotal) {
    echo(getScopeName());
    %ourCopy = %this.getGameBySID(%serversideID);
    if (isObject(%ourCopy)) {
        playercount = %newTotal @ %ourCopy;
        refresh();
    }
    error(GameList @ "Received superficial player add/remove update for game we don't have a record of! <- " @ getScopeName());
};
function gameMgrClient::gamestatusChanged(%this, %serversideID, %newStatus) {
    echo(getScopeName());
    %ourCopy = %this.getGameBySID(%serversideID);
    if (isObject(%ourCopy)) {
        gamestatus = %newStatus @ %ourCopy;
        refresh();
    }
    if ((inspectedGame == serversideID)) {
        gamestatus = %this @ inspectedGame;
        %this @ %newStatus;
        refreshInspectTab();
    }
    if (!(isObject(%ourCopy))) {
        error(GameList @ "Received gamestatus update for a game we don't have a record of and aren't inspecting! <- " @ getScopeName());
    }
};
function gameMgrClient::deepDetailUpdated(%this, %serversideID) {
    echo(getScopeName());
    %ourCopy = %this.getGameBySID(%serversideID);
    if (isObject(%ourCopy)) {
        deepUpdated = 1 @ %ourCopy;
        refresh();
    }
};
function gameMgrClient::inspectNewGame(%this, %serversideID, %gname, %gameType, %gamestatus, %gamehost, %playerCount, %readyCount, %playerstatus, %playerRank, %playerScore) {
    echo(getScopeName());
    if (isObject(inspectedGame)) {
        PlayerRecords.deleteMembers();
    }
    %ourCopy = %this.getGameBySID(%serversideID);
    inspectedGame;
    if (isObject(%ourCopy)) {
        if (!(%ourCopy SPC gname $= %gname)) {
        }
        if (!(%ourCopy SPC gametype $= %gameType)) {
            error(%this @ "Existing game record has different name or type than latest record! This should never happen and is bad news. Returning <-" @ getScopeName());
            return %this;
        }
        if (isObject(inspectedGame)) {
        }
        if (!(games.isMember(inspectedGame))) {
            PlayerRecords.delete();
            if (isObject(ourRecord)) {
                ourRecord.delete();
            }
            inspectedGame.delete();
        }
        inspectedGame = %this @ %ourCopy @ %this;
        inspectedGame;
        if (!(isObject(PlayerRecords))) {
            PlayerRecords = %this @ inspectedGame;
            SimSet @ new ""();
        }
    }
    if (!(isObject(inspectedGame))) {
        PlayerRecords = 0 @ SimSet @ new ""();
        new ""();
        inspectedGame = 0 @ ScriptObject @ %this;
        %this;
        if (isObject()) {
            inspectedGame.getId().add();
            PlayerRecords.getId().add();
        }
    }
    PlayerRecords.deleteMembers();
    serversideID = %this @ inspectedGame;
    inspectedGame @ %serversideID;
    deepUpdated = %this @ inspectedGame;
    %this @ 1;
    gname = %this @ inspectedGame;
    inspectedGame @ %gname;
    gametype = %this @ inspectedGame;
    %this @ %gameType;
    gamestatus = %this @ inspectedGame;
    MissionCleanup @ %gamestatus;
    playercount = %this @ inspectedGame;
    %this @ %playerCount;
    readyCount = %this @ inspectedGame;
    MissionCleanup @ %readyCount;
    host = %this @ inspectedGame;
    MissionCleanup @ %gamehost;
    ourRecord = %this @ inspectedGame;
    0 @ "";
    if (isObject(%ourCopy)) {
        ourRecord = %this @ inspectedGame;
        inspectedGame @ %this.newPlayerRecord($player.getShapeName(), %playerstatus, 0, %playerScore);
        rank = inspectedGame @ ourRecord;
        %this;
    }
    refreshInspectTab();
    "INSPECT".selectTabWithName();
    commandToServer('gameMgrServerInvoke', 'sendAllPlayersFor', %serversideID);
    refresh();
};
function gameMgrClient::inspectUpdatePlayersStatus(%this, %serversideID, %playerValues, %dontSortAndRefresh) {
    echo(getScopeName());
    if ((inspectedGame != serversideID)) {
        %ourCopy = %this.getGameBySID(%serversideID);
        %this;
        if (isObject(%ourCopy)) {
            deepUpdated = %serversideID @ 1 @ %ourCopy;
        }
        warn("Received player(s) status update for game we're not inspecting! <- " @ getScopeName());
        return;
    }
    deepUpdated = %this @ inspectedGame;
    1;
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
        %theRecord = PlayerRecords.getByNameField(%aPlayerName);
        inspectedGame;
        if (isObject(%theRecord)) {
            if (!(%this SPC %playerstatus $= "")) {
                status = %playerstatus @ %theRecord;
            }
            if (!(%playerScore $= "")) {
                score = %playerScore @ %theRecord;
            }
            if (!(%playerready $= "")) {
                ready = %playerready @ %theRecord;
            }
        }
        PlayerRecords.add(%aPlayerName.newPlayerRecord(%playerstatus, %playerready, %playerScore));
        if ((gameMgrClient SPC %aPlayerName $= $player.getShapeName())) {
            if (!(isObject(ourRecord))) {
                ourRecord = %this @ inspectedGame;
                inspectedGame @ %this.newPlayerRecord(%aPlayerName, %playerstatus, %playerready, %playerScore);
            }
            if (!(%this SPC %playerstatus $= "")) {
                status = inspectedGame @ ourRecord;
                %this;
            }
            if (!(inspectedGame @ %playerstatus SPC %playerScore $= "")) {
                score = inspectedGame @ ourRecord;
                %this;
            }
            if (!(%this @ %playerScore SPC %playerready $= "")) {
                ready = inspectedGame @ ourRecord;
                %this;
            }
        }
        %i = (1.0 + %i);
        %playerready;
    }
    if (((%playersCount < %i) SPC %dontSortAndRefresh $= "")) {
        %this.sortAndPurgePlayerRecords(inspectedGame);
        refreshInspectTab();
    }
};
function gameMgrClient::inspectAddRemovePlayers(%this, %serversideID, %playersAdded, %playersRemoved, %totalPlayersRemaining, %totalReadyPlayersRemaining) {
    echo(getScopeName());
    if ((inspectedGame != serversideID)) {
        %ourCopy = %this.getGameBySID(%serversideID);
        %this;
        if (isObject(%ourCopy)) {
            playercount = %serversideID @ %totalPlayersRemaining @ %ourCopy;
            refresh();
        }
        warn(GameList @ "Received inspect players update for a game we're not inspecting! <-" @ getScopeName());
        return;
    }
    if (!(isObject(PlayerRecords))) {
        error(inspectedGame @ "inspectedGame has no .PlayerRecords to modify! <-" @ getScopeName());
        return %this;
    }
    playercount = %this @ inspectedGame;
    %totalPlayersRemaining;
    refresh();
    if (!(GameList SPC %totalReadyPlayersRemaining $= "")) {
        readyCount = %this @ inspectedGame;
        %totalReadyPlayersRemaining;
    }
    if ((%playersAdded $= "")) {
    }
    if ((%playersRemoved $= "")) {
        refreshInspectTab();
        return GameList;
    }
    if (!(%playersAdded $= "")) {
        %this.inspectUpdatePlayersStatus(%serversideID, %playersAdded, 1);
    }
    %i = (1.0 - getFieldCount(%playersRemoved));
    if ((0.0 >= %i)) {
        %aPlayerName = getField(%playersRemoved, %i);
        %aPlayer = PlayerRecords.getByNameField(%aPlayerName);
        inspectedGame;
        if (isObject(%aPlayer)) {
            PlayerRecords.remove(%aPlayer);
            %aPlayer.delete();
        }
        warn(inspectedGame @ "Asked to remove a player that we didn't have a record of! <- " @ getScopeName());
        %i = (1.0 - %i);
        %this;
    }
    %this.sortAndPurgePlayerRecords(inspectedGame);
    refreshInspectTab();
};
function gameMgrClient::inspectChangeReadyStatus(%this, %serversideID, %readyValues, %totalReady) {
    echo(getScopeName());
    if ((inspectedGame != serversideID)) {
        warn(%this @ "Received inspect changeReadyStatus update for a game we're not inspecting! <-" @ getScopeName());
        return %serversideID;
    }
    %playerCount = (2.0 / getFieldCount(%readyValues));
    %i = 0;
    if ((%playerCount < %i)) {
        %playerIdx = (2.0 * %i);
        %aPlayerName = getField(%readyValues, %playerIdx);
        %aReadyValue = getField(%readyValues, (1.0 + %playerIdx));
        if ((%aPlayerName $= $player.getShapeName())) {
            ready = inspectedGame @ ourRecord;
            %this;
        }
        %aPlayer = PlayerRecords.getByNameField(%aPlayerName);
        inspectedGame;
        if (isObject(%aPlayer)) {
            ready = %this @ %aReadyValue @ %aPlayer;
            %aReadyValue;
        }
        if (!(%aPlayerName $= $player.getShapeName())) {
            warn("Received a request-to-start update on a player that we don't have a record for! <- " @ getScopeName());
        }
        %i = (1.0 + %i);
    }
    readyCount = %this @ inspectedGame;
    (%playerCount < %i) @ %totalReady;
    refreshInspectTab();
};
function gameMgrClient::inspectNothing(%this) {
    if (isObject(inspectedGame)) {
        if (!(games.isMember(inspectedGame))) {
            serversideID = %this @ inspectedGame;
            %this @ "";
        }
        inspectedGame = %this @ "" @ %this;
        %this;
    }
    refreshInspectTab();
};
function gameMgrClient::areWeInspecting(%this) {
    if (!(isObject(inspectedGame))) {
    }
    if ((inspectedGame SPC serversideID $= "")) {
        return 0;
    }
    return 1;
};
function gameMgrClient::areWeHostOfInspectedGame(%this) {
    if (!(%this.areWeInspecting())) {
        return 0;
    }
    if ((inspectedGame SPC host $= $player.getShapeName())) {
        return 1;
    }
    return 0;
};
function gameMgrClient::inCustomGame(%this) {
    if (!(%this.areWeInspecting())) {
        return 0;
    }
    if ((inspectedGame == gametype)) {
        return 1;
    }
    return 0;
};
function gameMgrClient::playerJoined(%this, %serversideID, %gname, %gameType, %host, %playerCount, %gamestatus, %playerstatus, %playerRank, %playerScore) {
    echo(getScopeName());
    echo("gameMgrClient: Joined game " @ %serversideID);
    %this.addGame(%serversideID, %gname, %gameType, %host, %playerCount, %gamestatus);
    refresh();
    switchIfInspectEmpty();
    if ((inspectedGame == serversideID)) {
        if (!(isObject(ourRecord))) {
            if ((inspectedGame SPC %playerRank $= "")) {
            }
            if ((%this SPC %playerScore $= "")) {
                error(%this @ "on playerJoined call for joining an inspected game, playerRank and playerScore were not passed!<-" @ getScopeName());
            }
            ourRecord = %this @ inspectedGame;
            %serversideID @ %this.newPlayerRecord($player.getShapeName(), %playerstatus, %playerRank, %playerScore);
        }
        error(GameList @ "playerJoined trying to create an ourRecord for inspected game we joined, but it already exists!<-" @ getScopeName());
        refreshInspectTab();
    }
    if ((GameList SPC %host $= $player.getShapeName())) {
        if (($gameMgr::CUSTOM_GAME == %gameType)) {
            userTips::showOnceThisSession("CustomGameHost");
        }
        handleSystemMessage("msgInfoMessage", GameList @ "You created a game: <a:game inspect " @ %serversideID @ ">" @ %gname @ "</a>.");
    }
    handleSystemMessage("msgInfoMessage", "You joined " @ %host @ "'s game: <a:game inspect " @ %serversideID @ ">" @ %gname @ "</a>.");
};
function gameMgrClient::playerLeft(%this, %serversideID, %message) {
    echo(getScopeName());
    echo("gameMgrClient: left game " @ %serversideID);
    %game = %this.getGameBySID(%serversideID);
    %exitMessage = "You left ";
    if ((%game SPC host $= $player.getShapeName())) {
        %exitMessage = %exitMessage @ " your";
    }
    %exitMessage = %exitMessage @ %game @ host @ "'s";
    %exitMessage = %exitMessage @ " game: <a:game inspect " @ %serversideID @ ">" @ %game @ gname @ "</a>.";
    if (!(%message $= "")) {
        %exitMessage = %exitMessage @ " " @ %message;
    }
    %this.removeGame(%serversideID);
    refresh();
    if ((inspectedGame == serversideID)) {
        ourRecord.delete();
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
    %theGame = games.getByField("gname", %gameName);
    %this;
    if (!(isObject(%theGame))) {
        handleSystemMessage("msgInfoMessage", "Sorry, you aren't in a game named " @ %gameName @ ".");
        return;
    }
    %this.playerQuitGame(serversideID);
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
    %theGame = games.getByField("gname", %gameName);
    %this;
    if (!(isObject(%theGame))) {
        handleSystemMessage("msgInfoMessage", "Sorry, you aren't in a game named " @ %gameName @ ".");
        return;
    }
    %this.playerRequestStartGame(serversideID);
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
    commandToServer('gameMgrServerInvoke', 'playerInvitePlayer', serversideID @ "\t" @ %playerName @ "\t" @ %message);
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
    commandToServer('gameMgrServerInvoke', 'hostChangePlayerScore', serversideID @ "\t" @ %playerName @ "\t" @ %scoreDelta);
};
function gameMgrClient::hostChangePlayerStatus(%this, %playerName, %newStatus) {
    if (!(%this.areWeHostOfInspectedGame())) {
        handleSystemMessage("msgInfoMessage", "You must be inspecting a game you're the host of to change a player's status!");
        return;
    }
    commandToServer('gameMgrServerInvoke', 'hostChangePlayerStatus', serversideID @ "\t" @ %playerName @ "\t" @ %newStatus);
};
function gameMgrClient::areWePlaying(%this, %gameObj) {
    return games.isMember(%gameObj);
};
function gameMgrClient::getGameBySID(%this, %serversideID) {
    echo(getScopeName());
    %n = (%this - games.getCount());
    1.0;
    if ((0.0 >= %n)) {
        %aGame = games.getObject(%n);
        %this;
        if ((%aGame == serversideID)) {
            return %aGame;
        }
        %n = (1.0 - %n);
    }
    return "";
};
function gameMgrClient::addGame(%this, %serversideID, %gname, %gameType, %host, %playerCount, %gamestatus) {
    echo(getScopeName());
    if (isObject(inspectedGame)) {
    }
    if ((inspectedGame == serversideID)) {
        %newGame = inspectedGame;
        %this;
        if (!(inspectedGame $= gname)) {
        }
        if ((gametype != %gameType)) {
            error("Serious error! gameMgrClient::addGame called with same serversideID as inspectedGame but gname and gametype don't match! Not overriding!");
        }
        playercount = inspectedGame @ %playerCount @ %newGame;
        %this;
        gamestatus = %this @ %gamestatus @ %newGame;
        %this SPC %gname;
    }
    serversideID = ScriptObject @ new ""() @ %serversideID;
    0;
    gname = %this @ %serversideID @ %gname;
    gametype = %gameType;
    host = %host;
    playercount = %playerCount;
    gamestatus = %gamestatus;
    deepUpdate = 0;
    %newGame = ;
    if (isObject()) {
        %newGame.add();
    }
    games.add(%newGame);
    %listFound = 0;
    %this;
    %n = (GameList - lists.getCount());
    1.0;
    if ((0.0 >= %n)) {
        %aList = lists.getObject(%n);
        GameList;
        if ((%aList == gametype)) {
            %aList.add(%newGame);
            %listFound = 1;
            gametype;
        }
        %n = (1.0 - %n);
        %newGame;
    }
    if ((0.0 <= %listFound)) {
        gametype = new ""() @ %newGame @ gametype;
        SimSet;
        collapsed = (0.0 >= %n) @ 0 @ 0;
        MissionCleanup;
        %newList = MissionCleanup;
        if (isObject()) {
            %newList.add();
        }
        %newList.add(%newGame);
        lists.add(%newList);
    }
};
function gameMgrClient::removeGame(%this, %serversideID) {
    echo(getScopeName());
    if ((%this == games.getCount())) {
        error(0.0 @ "trying to delete a game but gameMgrClient.games is empty! <- " @ getScopeName());
    }
    %theGame = %this.getGameBySID(%serversideID);
    %index = games.getObjectIndex(%theGame);
    if ((-(1.0) == %this)) {
        error(getScopeName);
        return "Trying to delete record of a game that we don't have! (not in gameMgrClient.games) <-";
    }
    games.remove(%theGame);
    %n = (GameList - lists.getCount());
    1.0;
    if ((0.0 >= %n)) {
        %aList = lists.getObject(%n);
        GameList;
        if ((%aList == gametype)) {
            if ((-(1.0) == %aList.getObjectIndex(%theGame))) {
                error(gametype @ "Deleting a game who isn't listed in his gametype! <-" @ getScopeName());
            }
            %aList.remove(%theGame);
            if ((0.0 == %aList.getCount())) {
                lists.remove(%aList);
                %aList.delete();
            }
        }
        if ((0.0 == %n)) {
            error(GameList @ "Deleting a game whose gametype didn't have a list <- " @ getScopeName());
        }
        %n = (1.0 - %n);
        %theGame;
    }
    if ((inspectedGame != %theGame)) {
        %theGame.delete();
    }
};
function gameMgrClient::startFresh(%this) {
    lists.deleteMembers();
    games.deleteMembers();
    if (isObject(inspectedGame)) {
        PlayerRecords.deleteMembers();
        PlayerRecords.delete();
        inspectedGame.delete();
    }
};
function gameMgrClient::newPlayerRecord(%this, %playerName, %playerstatus, %playerready, %playerScore) {
    name = ScriptObject @ new ""() @ %playerName;
    0;
    status = %playerstatus;
    ready = %playerready;
    score = %playerScore;
    %ret = ;
    if (isObject()) {
        %ret.add();
    }
    return %ret;
};
function gameMgrClient::sortAndPurgePlayerRecords(%this, %aGameObj) {
    if (!(isObject(%aGameObj))) {
    }
    if (!(isObject(PlayerRecords))) {
        error(%aGameObj @ "Tried to sort PlayerRecords on a game that didn't exist or didn't have PlayerRecords! <-" @ getScopeName());
        return;
    }
    %numRecords = PlayerRecords.getCount();
    %aGameObj;
    if ((0.0 == %numRecords)) {
        return;
    }
    %stringToSort = "";
    echo(%aGameObj @ PlayerRecords.getCount());
    %n = (1.0 - %numRecords);
    "sortAndPurgePlayerRecords working with PlayerRecords count = ";
    if ((0.0 >= %n)) {
        %curRecord = PlayerRecords.getObject(%n);
        %aGameObj;
        %stringToSort = trim(%curRecord @ formatInt(%stringToSort @ " " @ "%0." @ $gameMgr::MAX_SCORE_DIGITS @ "d", score) @ "\t" @ %curRecord.getId());
        %n = (1.0 - %n);
    }
    %stringToSort = SortWords(%stringToSort);
    (0.0 >= %n);
    %count = getFieldCount(%stringToSort);
    if ((%numRecords != (1.0 - %count))) {
        error("Serious error! All the player records didn't fit in the string to be sorted! Not sorting! <- " @ getScopeName());
        return;
    }
    PlayerRecords.clear(1);
    %n = (1.0 - %count);
    %aGameObj;
    if ((1.0 >= %n)) {
        %recordID = trim(getWord(getField(%stringToSort, %n), 0));
        if (isObject(%recordID)) {
            if ((($gameMgr::InspectTab::MAX_PLAYERS - %count) >= %n)) {
                PlayerRecords.add(%recordID);
                PlayerRecords.pushToBack(%recordID);
            }
            %recordID.delete();
        }
        error(%aGameObj @ "Serious error! Record ID retrieved from sorted string invalid! Skipping and continuing <-" @ getScopeName());
        %n = (1.0 - %n);
        %aGameObj;
    }
};
function gameMgrHostPopup::setup(%this) {
    profile = GuiMLTextCtrl @ new ""() @ "ETSShadowTextProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "12 28";
    extent = "220 16";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "";
    maxLength = -1;
    variText = gameMgrHostPopup;
    variText.add();
    profile = GuiTextEditCtrl @ new ""() @ "ETSDarkTextEditProfile";
    0;
    horizSizing = gameMgrHostPopup @ gameMgrHostPopup @ "right";
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
    textField = gameMgrHostPopup;
    textField.add();
    profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton15Profile";
    0;
    horizSizing = gameMgrHostPopup @ gameMgrHostPopup @ "right";
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
    applyButton = gameMgrHostPopup;
    applyButton.add();
    isSetup = gameMgrHostPopup @ 1 @ gameMgrHostPopup;
    gameMgrHostPopup;
};
function gameMgrHostPopup::close(%this) {
    %this.setVisible(0);
};
function gameMgrHostPopup::open(%this, %playerName, %command) {
    if (!(isSetup)) {
        %this.setup();
    }
    if ((%this SPC %command $= "score")) {
        variText.setText(%this @ "<spush><b>Change " @ %playerName @ "'s score by:<spop>(e.g. +5, -10)");
    }
    if ((%command $= "status")) {
        variText.setText(%this @ "<spush><b>Set " @ %playerName @ "'s status to:<spop>");
    }
    error("gameMgrHostPopup::apply was passed a unrecognized command " @ %command @ ".<-" @ getScopeName());
    return;
    command = %command @ %this;
    playerName = %playerName @ %this;
    %this.setVisible(1);
};
function gameMgrHostPopup::apply(%this) {
    if ((%this SPC command $= "score")) {
        playerName.hostChangePlayerScore(textField.getText());
    }
    if ((%this SPC command $= "status")) {
        playerName.hostChangePlayerStatus(textField.getText());
    }
    error(%this @ %this @ "gameMgrHostPopup::apply was passed a unrecognized command " @ %this @ command @ ".<-" @ getScopeName());
};
