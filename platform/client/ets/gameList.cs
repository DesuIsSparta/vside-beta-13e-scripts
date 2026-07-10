if (!(isObject(GameMgrHudTabs))) {
    echo(getScopeName());
    new ScriptObject(GameMgrHudTabs) {
        class = "TabControl";
    };
    if (isObject(MissionCleanup)) {
        GameMgrHudTabs.add(MissionCleanup);
    }
}
function GameMgrHudTabs::setup(%this) {
    echo(getTrace());
    if (!(%this.initialized)) {
        "horizontal".Initialize(%this, GameMgrHudTabContainer, "25 25", "platform/client/ui/separator", "16 7");
        "platform/client/buttons/buddies".newTab(%this, "MYGAMES");
        "platform/client/buttons/aim_buddies".newTab(%this, "INSPECT");
        %this.InspectTab = "INSPECT".getTabWithName(%this);
        "platform/client/buttons/aim_buddies".newTab(%this, "CREATE");
        %this.CreateTab = "CREATE".getTabWithName(%this);
        "GameMgrCreateTab".setName(%this.CreateTab);
        "MYGAMES".selectTabWithName(%this);
        %this.fillMYGAMESTab();
        %this.fillINSPECTtab();
        %this.fillCREATEtab();
    }
};
function GameMgrHudTabs::OnETSInviteFriends(%this) {
    if (!(isObject(EtsInviteDialog))) {
        error("no EtsInviteDialog, this should not happen");
        return;
    }
    if (!(EtsInviteDialog.isVisible())) {
        EtsInviteDialog.open();
    }
};
function GameMgrHudTabs::fillINSPECTtab(%this) {
    echo(getScopeName());
    %theTab = %this.InspectTab;
    %theTab.UpperContent = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = (getWord(%theTab.getExtent(), 0) - 1.0) @ " " @ 108;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = -1;
    };
    "GameMgrMLText".bindClassName(%theTab.UpperContent);
    %theTab.UpperContent.add(%theTab);
    %theTab.PlayerListScroll = new GuiScrollCtrl("") {
        profile = 0 @ "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 109";
        extent = getWord(%theTab.getExtent(), 0) @ " " @ 70;
        minExtent = "10 10";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "dynamic";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        childMargin = "-4 -1";
        helpTag = 0;
    };
    %theTab.PlayerList = new GuiTextListCtrl(PlayerList) {
        profile = "ETSTextListProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = %theTab.PlayerListScroll.getExtent();
        minExtent = "80 30";
        sluggishness = -1;
        visible = 1;
        command = "";
        altCommand = "";
        enumerate = 1;
        resizeCell = 1;
        columns = "0 60 120";
        fitParentWidth = 1;
        clipColumnText = "true";
    };
    %theTab.PlayerList.add(%theTab.PlayerListScroll);
    %theTab.PlayerListScroll.add(%theTab);
    %theTab.LowerContent = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " " @ (getWord(%theTab.PlayerListScroll.getPosition(), 1) + getWord(%theTab.PlayerListScroll.getExtent(), 1));
        extent = (getWord(%theTab.getExtent(), 0) - 1.0) @ " " @ 108;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = -1;
    };
    "GameMgrMLText".bindClassName(%theTab.LowerContent);
    %theTab.LowerContent.add(%theTab);
    gameMgrClient.inspectNothing();
};
$gameMgr::InspectTab::NO_INSPECTED_GAME_MESSAGE = "<spush><b>Inspect a game.<spop><br>You aren't inspecting a game right now. If you'd like to see how you or your friends are doing in a game, double click one on your list!";
function GameList::refreshInspectTab(%this) {
    %this = %theTab.InspectTab;
    GameMgrHudTabs;
    if (!(gameMgrClient.areWeInspecting())) {
        $gameMgr::InspectTab::NO_INSPECTED_GAME_MESSAGE.setText(%this.UpperContent);
        "".setText(%this.LowerContent);
        %this.PlayerList.clear();
        return;
    }
    %game = %this.inspectedGame;
    gameMgrClient;
    if (%game.areWePlaying(gameMgrClient)) {
        %ourRecord = %game.ourRecord;
    }
    %gameType = %game[$gameMgr::GAME_TYPES @ %game.gametype].title;
    %plyrStr = (%game.playercount > 1.0) ? " players" : " player";
    %upperText = "<spush><b>Inspect Game:<spop><br>" @ "name: " @ %game.gname @ "<br>" @ "game: " @ %gameType @ "<br>" @ "<spush><b>created by " @ %game.host @ "<spop><br>" @ %game.playercount @ %plyrStr @ "<br>";
    if ((%game.gamestatus == $gameMgr::GameStatus::STARTED)) {
        if (!(%game.areWePlaying(gameMgrClient))) {
            %upperText = %upperText @ "You're not in this game. <a:game join " @ %game.serversideID @ ">Join it!</a><br>";
        }
        if (!(%ourRecord.status $= "")) {
            %upperText = %upperText @ "<spush><b>our status:<spop> " @ %statusStr @ "<br>";
        }
        %upperText = %upperText @ "<spush><b>our score:<spop> " @ %ourRecord.score @ "<br>";
        %upperText = %upperText @ "Top " @ $gameMgr::InspectTab::MAX_PLAYERS @ " Players:";
    }
    %upperText = %upperText @ "Waiting players (" @ %game.readyCount @ " ready):";
    if ((%game.playercount > $gameMgr::InspectTab::MAX_PLAYERS)) {
        %upperText = %upperText @ "<br>(only showing " @ $gameMgr::InspectTab::MAX_PLAYERS @ ")";
    }
    %upperText.setText(%this.UpperContent);
    %this.PlayerList.clear();
    %recordCount = %game.PlayerRecords.getCount();
    %n = 0;
    while ((%n < %recordCount)) {
        %record = %n.getObject(%game.PlayerRecords);
        if ((%game.gamestatus == $gameMgr::GameStatus::STARTED)) {
            %n.addRow(%this.PlayerList, %n, "#" @ (%n + 1.0) @ "- " @ %record.name @ "\t" @ %record.status @ "\t" @ %record.score);
        }
        if (%record.ready) {
            %ready = "ready";
        }
        %ready = "not ready";
        %n.addRow(%this.PlayerList, %n, %record.name @ "\t" @ %ready @ "\t" @ "");
        %n = (%n + 1.0);
    }
    %lowerText = "";
    (%n < %recordCount);
    %lowerText = %lowerText @ "Game status: ";
    if ((%game.gamestatus == $gameMgr::GameStatus::CANT_START)) {
        %lowerText = %lowerText @ "Can't start.";
    }
    if ((%game.gamestatus == $gameMgr::GameStatus::WAITING)) {
        %lowerText = %lowerText @ "Waiting on players.";
    }
    if ((%game.gamestatus == $gameMgr::GameStatus::STARTED)) {
        %lowerText = %lowerText @ "Game started!";
    }
    %lowerText = %lowerText @ "<br>";
    %lowerText = %lowerText @ "<spush><b>Actions:<spop><br>";
    if (!(%game.areWePlaying(gameMgrClient))) {
        %lowerText = %lowerText @ "Not playing yet...<a:game join " @ %game.serversideID @ ">[Join game]</a><br>";
    }
    if ((%game.gamestatus != $gameMgr::GameStatus::STARTED)) {
    }
    if (!(%this.postgameView)) {
        %readyText = !(%game.ourRecord.ready) ? "[I'm ready]" : "[I'm not ready]";
        %lowerText = %lowerText @ "Change readiness:<a:game changeReady " @ %game.serversideID @ " " @ !(%game.ourRecord.ready) @ ">" @ %readyText @ "</a><br>";
        if ((%game.host $= $player.getShapeName())) {
            %lowerText = %lowerText @ "You're the host. <a:game startGame " @ %game.serversideID @ ">" @ "[start game]" @ "</a><br>";
        }
    }
    %lowerText = %lowerText @ "<a:game quit " @ %game.serversideID @ ">[Quit game]</a><br>";
    %lowerText = %lowerText @ "<br><br>" @ "<a:game stopInspecting>[stop inspecting]</a>";
    %lowerText = %lowerText @ "<br>Want more people to play in this game? When it's inspected like this, right-click people or their name-links and choose \"Invite to game.\"";
    %lowerText.setText(%this.LowerContent);
    %this.UpperContent.forceReflow();
    %newPLSPosX = getWord(%this.PlayerListScroll.getPosition, 0);
    %newPLSPosY = (getWord(%this.UpperContent.getPosition(), 1) + getWord(%this.UpperContent.getExtent(), 1));
    %newPLSPosY.reposition(%this.PlayerListScroll, %newPLSPosX);
    %newLCPosX = getWord(%this.LowerContent.getPosition, 0);
    %newLCPosY = (getWord(%this.PlayerListScroll.getPosition(), 1) + getWord(%this.PlayerListScroll.getExtent(), 1));
    %newLCPosY.reposition(%this.LowerContent, %newLCPosX);
};
function GameMgrMLText::onURL(%this, %url) {
    %firstWord = getWord(%url, 0);
    if ((%firstWord $= "gamelink")) {
        onLeftClickPlayerName(getWords(%url, 2), "");
    }
    if (!(%firstWord $= "game")) {
        warn("GameMgrMLText received an unrecognized link URL=" @ %url @ ". Returning!<-" @ getScopeName());
        return;
    }
    %command = getWord(%url, 1);
    %arguments = getWords(%url, 2);
    if ((%command $= "join")) {
        %arguments.playerJoinGame(gameMgrClient);
    }
    if ((%command $= "quit")) {
        %arguments.playerQuitGame(gameMgrClient);
    }
    if ((%command $= "changeReady")) {
        getWord(%arguments, 1).playerChangeReadyStatus(gameMgrClient, getWord(%arguments, 0));
    }
    if ((%command $= "startGame")) {
        %arguments.playerRequestStartGame(gameMgrClient);
    }
    if ((%command $= "stopInspecting")) {
        gameMgrClient.inspectNothing();
    }
    error("GameMgr action link with unrecognized action=" @ %command @ ". <- " @ getScopeName());
};
function GameMgrHudTabs::tabSelected(%this, %tab) {
    if ((%tab.name $= "INSPECT")) {
    }
    if ((gameMgrClient.areWeInspecting() == 1.0)) {
        %tab.inspectedGame.deepUpdated = 0 @ gameMgrClient;
        GameList.refresh();
    }
};
function GameList::switchIfInspectEmpty(%this) {
    if (!(gameMgrClient.areWeInspecting())) {
        "MYGAMES".selectTabWithName(GameMgrHudTabs);
    }
};
function GameMgrHudTabs::fillMYGAMESTab(%this) {
    echo(getScopeName());
    %theTab = "MYGAMES".getTabWithName(%this);
    new GuiScrollCtrl("") {
        profile = 0 @ "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) @ " " @ (getWord(%theTab.getExtent(), 1) - 24.0);
        minExtent = "10 10";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    };.add(%theTab, new GuiMLTextCtrl(GameList) {
        profile = "ETSFavTextListProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = (getWord(%theTab.getExtent(), 0) - 1.0) @ " " @ 80;
        minExtent = "80 80";
        sluggishness = -1;
        visible = 1;
    };);
    lists = new SimSet(""); @ GameList;
    0;
    if (isObject(MissionCleanup)) {
        lists.getId(GameList).add(MissionCleanup);
    }
    GameList.refresh();
};
function GameMgrHudTabs::fillCREATEtab(%this) {
    echo(getScopeName());
    %theTab = "CREATE".getTabWithName(%this);
    %ypos = 1;
    %theTab.UpperContent = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " " @ %ypos;
        extent = (getWord(%theTab.getExtent(), 0) - 1.0) @ " " @ 18;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "<spush><b>Create a game!<spop><br>Configure your game how you like here and then click create below to open it.<br>";
        maxLength = -1;
    };
    %theTab.UpperContent.add(%theTab);
    %theTab.UpperContent.forceReflow();
    %ypos = (getWord(%theTab.UpperContent.getExtent(), 1) + %ypos);
    new GuiTextCtrl("") {
        profile = 0 @ "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " " @ %ypos;
        extent = "50 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Game name:";
        maxLength = -1;
    };.add(%theTab);
    %theTab.gameNameField = new GuiTextEditCtrl("") {
        profile = 0 @ "ETSDarkTextEditProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 65 @ " " @ %ypos;
        extent = "85 16";
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
    %theTab.gameNameField.add(%theTab);
    %ypos = (%ypos + 20.0);
    new GuiTextCtrl("") {
        profile = 0 @ "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "150 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Choose a game type:";
        maxLength = 64;
    };.add(%theTab);
    %ypos = (%ypos + 20.0);
    %theTab.gameTypesDropdown = new GuiPopUp2MenuCtrl("") {
        profile = 0 @ "InfoWindowPopupProfile";
        scrollProfile = "DottedScrollProfile";
        winProfile = "InfoWindowPopupWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "150 30";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 255;
        maxPopupHeight = 200;
        allowReverse = 0;
    };
    GameList.CreateTabSetupGametypesDropdown();
    %theTab.gameTypesDropdown.add(%theTab);
    %ypos = (%ypos + 20.0);
    new GuiTextCtrl("") {
        profile = 0 @ "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "150 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Game settings:";
        maxLength = 64;
    };.add(%theTab);
    %ypos = (%ypos + 20.0);
    %theTab.SettingWaitingRoom = new GuiCheckBoxCtrl("") {
        profile = 0 @ "InfoWindowRadioButtonProfile";
        buttonType = "ToggleButton";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 10 @ " ";
        extent = "150 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Waiting room";
        command = "GameList.CreateTabSettingClicked(\"WaitingRoom\");";
        maxLength = 64;
    };
    %theTab.SettingWaitingRoom.add(%theTab);
    %ypos = (%ypos + 20.0);
    %theTab.SettingJoinInProgress = new GuiCheckBoxCtrl("") {
        profile = 0 @ "InfoWindowRadioButtonProfile";
        buttonType = "ToggleButton";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 15 @ " ";
        extent = "150 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Allow join in progress";
        command = "GameList.CreateTabSettingClicked(\"JoinInProgress\");";
        maxLength = 64;
    };
    %theTab.SettingJoinInProgress.add(%theTab);
    %ypos = (%ypos + 20.0);
    %theTab.SettingAutoStartOnReady = new GuiCheckBoxCtrl("") {
        profile = 0 @ "InfoWindowRadioButtonProfile";
        buttonType = "ToggleButton";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 15 @ " ";
        extent = "150 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Auto-start when everyone is ready";
        command = "GameList.CreateTabSettingClicked(\"AutoStartOnReady\");";
        maxLength = 64;
    };
    %theTab.SettingAutoStartOnReady.add(%theTab);
    %ypos = (%ypos + 20.0);
    %theTab.SettingDropUnreadyPlayers = new GuiCheckBoxCtrl("") {
        profile = 0 @ "InfoWindowRadioButtonProfile";
        buttonType = "ToggleButton";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 15 @ " ";
        extent = "150 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Drop non-ready players on start";
        command = "GameList.CreateTabSettingClicked(\"DropUnreadyPlayers\");";
        maxLength = 64;
    };
    %theTab.SettingDropUnreadyPlayers.add(%theTab);
    %ypos = (%ypos + 35.0);
    %theTab.createGameButton = new GuiVariableWidthButtonCtrl("") {
        profile = 0 @ "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 14 @ " ";
        extent = "134 15";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "GameList.CreateTabCreateGame();";
        text = "Create Game";
        groupNum = -1;
        buttonType = "PushButton";
    };
    %theTab.createGameButton.add(%theTab);
    GameList.CreateTabResetDefaults();
};
function GameList::CreateTabSettingClicked(%this, %setting) {
    %this = %theTab.CreateTab;
    GameMgrHudTabs;
    if ((%setting $= "WaitingRoom")) {
        %onOff = %this.SettingWaitingRoom.getValue();
        %onOff.setActive(%this.SettingAutoStartOnReady);
        %onOff.setActive(%this.SettingDropUnreadyPlayers);
        %onOff.setActive(%this.SettingJoinInProgress);
    }
};
function GameList::CreateTabResetDefaults(%this) {
    %this = %this.CreateTab;
    GameMgrHudTabs;
    0.setValue(%this.SettingDropUnreadyPlayers);
    1.setValue(%this.SettingAutoStartOnReady);
    1.setValue(%this.SettingJoinInProgress);
    1.setValue(%this.SettingWaitingRoom);
    GameList.GetDefaultGameName().setText(%this.gameNameField);
    0.SetSelected(%this.gameTypesDropdown);
};
function GameList::GetDefaultGameName(%this) {
    if (isObject($player)) {
        return $player.getShapeName() @ "'s Game";
    }
    return "A Fun Game";
};
function GameList::CreateTabCreateGame(%this) {
    %this = %this.CreateTab;
    GameMgrHudTabs;
    %errorMsgPrepend = "Sorry, couldn't create game:";
    %gameName = %this.gameNameField.getText();
    if ((%gameName $= "")) {
        handleSystemMessage("msgInfoMessage", %errorMsgPrepend @ " " @ "you didn't specify a game name.");
        return;
    }
    GameList.CreateTabResetDefaults();
    %gameType = %this.gameTypesDropdown.getText();
    %n = 0;
    while ((%n < $gameMgr::GAME_TYPES_COUNT)) {
        if ((%n[$gameMgr::GAME_TYPES @ %n].INST_TITLE $= %gameType)) {
            %gameType = %n;
        }
        %n = (%n + 1.0);
    }
    if (((%n < $gameMgr::GAME_TYPES_COUNT) @ " " @ %gameType $= %this.gameTypesDropdown.getText())) {
        handleSystemMessage("msgInfoMessage", %errorMsgPrepend @ " " @ "we're having a problem with that game type.");
        error("Couldn't translate gametype text to commonID !! aborting create game<-" @ getScopeName());
        return;
    }
    %waitingRoom = %this.SettingWaitingRoom.getValue();
    %joinInProgress = %this.SettingJoinInProgress.getValue();
    %autoStartOnReady = %this.SettingAutoStartOnReady.getValue();
    %dropUnreadyPlayers = %this.SettingDropUnreadyPlayers.getValue();
    %dropUnreadyPlayers.createGame(gameMgrClient, %gameName, %gameType, %waitingRoom, %joinInProgress, %autoStartOnReady);
};
function GameList::CreateTabSetupGametypesDropdown(%this) {
    %this = %this.CreateTab;
    GameMgrHudTabs;
    %n = 0;
    while ((%n < $gameMgr::GAME_TYPES_COUNT)) {
        if (%n[$gameMgr::GAME_TYPES @ %n].USER_CREATE) {
            %n[$gameMgr::GAME_TYPES @ %n].INST_TITLE.add(%this.gameTypesDropdown);
        }
        %n = (%n + 1.0);
    }
    0.SetSelected(%this.gameTypesDropdown);
};
function GameMgrHudTabs::wakeUp(%this) {
    log("general", "debug", getScopeName() @ " " @ "- gameMgr disabled.");
    return;
    echo(getScopeName());
    %this.setup();
    %this.selectCurrentTab();
};
function GameMgrHudWin::wakeUp(%this) {
    echo(getScopeName());
    GameMgrHudTabs.wakeUp();
};
function GameList::scrollToPos(%this, %pos) {
    (1.0 - getWord(%pos, 1)).scrollTo(%this.getParent(), 0);
};
function GameList::onURL(%this, %url) {
    echo(getScopeName());
    if (!(firstWord(%url) $= "gamelink")) {
        return;
    }
    if ((getWord(%url, 1) $= "game")) {
        %SID = getWord(%url, 2);
        onLeftClickGameName(%SID);
    }
    if ((getWord(%url, 1) $= "list")) {
        %listName = getWords(%url, 2);
        echo("handling a list \"" @ %listName @ "\" <-" @ getScopeName());
        %listName.collapsed = !(%listName.collapsed);
        GameList.refresh();
    }
};
function GameList::onRightURL(%this, %url) {
    if (!(firstWord(%url) $= "gamelink")) {
        return;
    }
    if ((getWord(%url, 1) $= "game")) {
        %SID = getWord(%url, 2);
        onRightClickGameName(%SID);
    }
};
function onLeftClickGameName(%SID) {
    %curTime = getSimTime();
    if (((%curTime - $gLastNameClickTime) < 400.0)) {
    }
    if (($gLastNameClickName $= %SID)) {
        echo("Sending inspectGameRequest with SID==" @ %SID);
        %SID.requestToInspectGame(gameMgrClient);
        "INSPECT".selectTabWithName(GameMgrHudTabs);
        if (($gLeftClickTimer != 0.0)) {
            cancel($gLeftClickTimer);
            $gLeftClickTimer = 0;
        }
    }
    $gLeftClickTimer = schedule(450, 0, "onSingleClickGameName", %SID);
    $gLastNameClickTime = %curTime;
    $gLastNameClickName = %SID;
};
function onRightClickGameName(%name) {
};
function onSingleClickGameName(%name) {
    if (showPlayerInfoPopup()) {
    }
    if (!(%name $= $player.getShapeName())) {
        InfoPopupDlg.open();
        %name.showInfoFor(InfoPopupDlg);
    }
};
$gameMgr::GameList::NO_GAMES_MESSAGE = "<spush><color:FFFFFF><b>Your games.<spop><spush><color:FFFFFF><br>This is where your games would be listed - but you're not playing any!<br>Join a game or start your own!<br><spop>";
function GameList::refresh(%this) {
    %outString = "";
    %indent = "   ";
    %color = "";
    if (!(isObject(GameList, %listName.lists))) {
        error("GameList.Lists unavailable!<-" @ getScopeName());
        return;
    }
    %numlists = %listName.lists.getCount(GameList);
    if ((%numlists == 0.0)) {
        $gameMgr::GameList::NO_GAMES_MESSAGE.setText(%this);
        return;
    }
    %outString = %outString @ "<spush><color:FFFFFF><b>Your games:<spop><br>";
    %n = (%listName.lists.getCount(GameList) - 1.0);
    while ((%n >= 0.0)) {
        %aList = %n.getObject(GameList, %listName.lists);
        %outString = %outString @ "<spush><linkcolor:" @ $gameMgr::ListColors::LIST_HEADER @ ">";
        if ((%aList.collapsed == 0.0)) {
            %listPrefix = "-";
        }
        %listPrefix = "+";
        %outString = %outString @ "<a:gamelink list " @ %aList @ " >" @ %listPrefix @ " " @ %aList[$gameMgr::GAME_TYPES @ %aList.gametype].title @ " " @ "(" @ %aList.getCount() @ " games)</a><spop><br>";
        if ((%aList.collapsed == 1.0)) {
            echo("the list is collapsed <-" @ getScopeName());
        }
        %i = (%aList.getCount() - 1.0);
        while ((%i >= 0.0)) {
            echo("printing #" @ %i @ " game in the current list.<-" @ getScopeName());
            %aGame = %i.getObject(%aList);
            if ((%aGame.gamestatus == $gameMgr::GameStatus::CANT_START)) {
                %color = $gameMgr::ListColors::CANT_START;
            }
            if ((%aGame.gamestatus == $gameMgr::GameStatus::WAITING)) {
                %color = $gameMgr::ListColors::WAITING;
            }
            if ((%aGame.gamestatus == $gameMgr::GameStatus::STARTED)) {
                %color = $gameMgr::ListColors::STARTED;
            }
            %color = $gameMgr::ListColors::ELSE;
            if (%aGame.deepUpdated) {
                %changed = "<spush><color:FF0000>*<spop>";
            }
            %changed = "";
            %outString = %outString @ %indent @ "<spush><linkcolor:" @ %color @ "><a:gamelink game " @ %aGame.serversideID @ " >" @ %aGame.gname @ " " @ "(" @ %aGame.playercount @ " players)</a><spop>" @ %changed @ "<br>";
            %i = (%i - 1.0);
        }
        %n = (%n - 1.0);
        (%i >= 0.0);
    }
    %outString.setText(%this);
};
