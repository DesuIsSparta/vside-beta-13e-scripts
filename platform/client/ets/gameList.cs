if (!(isObject(GameMgrHudTabs))) {
    echo(getScopeName());
    new ScriptObject(GameMgrHudTabs) {
        class = "TabControl";
    };
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(GameMgrHudTabs);
    }
}
function GameMgrHudTabs::setup(%this) {
    echo(getTrace());
    if (!(%this.initialized)) {
        %this.Initialize(GameMgrHudTabContainer, "25 25", "platform/client/ui/separator", "16 7", "horizontal");
        %this.newTab("MYGAMES", "platform/client/buttons/buddies");
        %this.newTab("INSPECT", "platform/client/buttons/aim_buddies");
        %this.InspectTab = %this.getTabWithName("INSPECT");
        %this.newTab("CREATE", "platform/client/buttons/aim_buddies");
        %this.CreateTab = %this.getTabWithName("CREATE");
        %this.CreateTab.setName("GameMgrCreateTab");
        %this.selectTabWithName("MYGAMES");
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
        extent = (1.0 - getWord(%theTab.getExtent(), 0)) @ " " @ 108;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = -1;
    };
    %theTab.UpperContent.bindClassName("GameMgrMLText");
    %theTab.add(%theTab.UpperContent);
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
    %theTab.PlayerListScroll.add(%theTab.PlayerList);
    %theTab.add(%theTab.PlayerListScroll);
    %theTab.LowerContent = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " " @ (getWord(%theTab.PlayerListScroll.getExtent(), 1) + getWord(%theTab.PlayerListScroll.getPosition(), 1));
        extent = (1.0 - getWord(%theTab.getExtent(), 0)) @ " " @ 108;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = -1;
    };
    %theTab.LowerContent.bindClassName("GameMgrMLText");
    %theTab.add(%theTab.LowerContent);
    gameMgrClient.inspectNothing();
};
$gameMgr::InspectTab::NO_INSPECTED_GAME_MESSAGE = "<spush><b>Inspect a game.<spop><br>You aren't inspecting a game right now. If you'd like to see how you or your friends are doing in a game, double click one on your list!";
function GameList::refreshInspectTab(%this) {
    %this = %theTab.InspectTab;
    GameMgrHudTabs;
    if (!(gameMgrClient.areWeInspecting())) {
        %this.UpperContent.setText($gameMgr::InspectTab::NO_INSPECTED_GAME_MESSAGE);
        %this.LowerContent.setText("");
        %this.PlayerList.clear();
        return;
    }
    %game = %this.inspectedGame;
    gameMgrClient;
    if (gameMgrClient.areWePlaying(%game)) {
        %ourRecord = %game.ourRecord;
    }
    %gameType = %game[$gameMgr::GAME_TYPES @ %game.gametype].title;
    %plyrStr = (1.0 > %game.playercount) ? " players" : " player";
    %upperText = "<spush><b>Inspect Game:<spop><br>" @ "name: " @ %game.gname @ "<br>" @ "game: " @ %gameType @ "<br>" @ "<spush><b>created by " @ %game.host @ "<spop><br>" @ %game.playercount @ %plyrStr @ "<br>";
    if (($gameMgr::GameStatus::STARTED == %game.gamestatus)) {
        if (!(gameMgrClient.areWePlaying(%game))) {
            %upperText = %upperText @ "You're not in this game. <a:game join " @ %game.serversideID @ ">Join it!</a><br>";
        }
        if (!(%ourRecord.status $= "")) {
            %upperText = %upperText @ "<spush><b>our status:<spop> " @ %statusStr @ "<br>";
        }
        %upperText = %upperText @ "<spush><b>our score:<spop> " @ %ourRecord.score @ "<br>";
        %upperText = %upperText @ "Top " @ $gameMgr::InspectTab::MAX_PLAYERS @ " Players:";
    }
    %upperText = %upperText @ "Waiting players (" @ %game.readyCount @ " ready):";
    if (($gameMgr::InspectTab::MAX_PLAYERS > %game.playercount)) {
        %upperText = %upperText @ "<br>(only showing " @ $gameMgr::InspectTab::MAX_PLAYERS @ ")";
    }
    %this.UpperContent.setText(%upperText);
    %this.PlayerList.clear();
    %recordCount = %game.PlayerRecords.getCount();
    %n = 0;
    if ((%recordCount < %n)) {
        %record = %game.PlayerRecords.getObject(%n);
        if (($gameMgr::GameStatus::STARTED == %game.gamestatus)) {
            %this.PlayerList.addRow(%n, "#" @ (1.0 + %n) @ "- " @ %record.name @ "\t" @ %record.status @ "\t" @ %record.score, %n);
        }
        if (%record.ready) {
            %ready = "ready";
        }
        %ready = "not ready";
        %this.PlayerList.addRow(%n, %record.name @ "\t" @ %ready @ "\t" @ "", %n);
        %n = (1.0 + %n);
    }
    %lowerText = "";
    (%recordCount < %n);
    %lowerText = %lowerText @ "Game status: ";
    if (($gameMgr::GameStatus::CANT_START == %game.gamestatus)) {
        %lowerText = %lowerText @ "Can't start.";
    }
    if (($gameMgr::GameStatus::WAITING == %game.gamestatus)) {
        %lowerText = %lowerText @ "Waiting on players.";
    }
    if (($gameMgr::GameStatus::STARTED == %game.gamestatus)) {
        %lowerText = %lowerText @ "Game started!";
    }
    %lowerText = %lowerText @ "<br>";
    %lowerText = %lowerText @ "<spush><b>Actions:<spop><br>";
    if (!(gameMgrClient.areWePlaying(%game))) {
        %lowerText = %lowerText @ "Not playing yet...<a:game join " @ %game.serversideID @ ">[Join game]</a><br>";
    }
    if (($gameMgr::GameStatus::STARTED != %game.gamestatus)) {
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
    %this.LowerContent.setText(%lowerText);
    %this.UpperContent.forceReflow();
    %newPLSPosX = getWord(%this.PlayerListScroll.getPosition, 0);
    %newPLSPosY = (getWord(%this.UpperContent.getExtent(), 1) + getWord(%this.UpperContent.getPosition(), 1));
    %this.PlayerListScroll.reposition(%newPLSPosX, %newPLSPosY);
    %newLCPosX = getWord(%this.LowerContent.getPosition, 0);
    %newLCPosY = (getWord(%this.PlayerListScroll.getExtent(), 1) + getWord(%this.PlayerListScroll.getPosition(), 1));
    %this.LowerContent.reposition(%newLCPosX, %newLCPosY);
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
        gameMgrClient.playerJoinGame(%arguments);
    }
    if ((%command $= "quit")) {
        gameMgrClient.playerQuitGame(%arguments);
    }
    if ((%command $= "changeReady")) {
        gameMgrClient.playerChangeReadyStatus(getWord(%arguments, 0), getWord(%arguments, 1));
    }
    if ((%command $= "startGame")) {
        gameMgrClient.playerRequestStartGame(%arguments);
    }
    if ((%command $= "stopInspecting")) {
        gameMgrClient.inspectNothing();
    }
    error("GameMgr action link with unrecognized action=" @ %command @ ". <- " @ getScopeName());
};
function GameMgrHudTabs::tabSelected(%this, %tab) {
    if ((%tab.name $= "INSPECT")) {
    }
    if ((1.0 == gameMgrClient.areWeInspecting())) {
        %tab.inspectedGame.deepUpdated = 0 @ gameMgrClient;
        GameList.refresh();
    }
};
function GameList::switchIfInspectEmpty(%this) {
    if (!(gameMgrClient.areWeInspecting())) {
        GameMgrHudTabs.selectTabWithName("MYGAMES");
    }
};
function GameMgrHudTabs::fillMYGAMESTab(%this) {
    echo(getScopeName());
    %theTab = %this.getTabWithName("MYGAMES");
    %theTab.add(new GuiMLTextCtrl(GameList) {
        profile = "ETSFavTextListProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = (1.0 - getWord(%theTab.getExtent(), 0)) @ " " @ 80;
        minExtent = "80 80";
        sluggishness = -1;
        visible = 1;
    };, new GuiScrollCtrl("") {
        profile = 0 @ "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) @ " " @ (24.0 - getWord(%theTab.getExtent(), 1));
        minExtent = "10 10";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    };);
    lists = new SimSet(""); @ GameList;
    0;
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(GameList.getId(lists));
    }
    GameList.refresh();
};
function GameMgrHudTabs::fillCREATEtab(%this) {
    echo(getScopeName());
    %theTab = %this.getTabWithName("CREATE");
    %ypos = 1;
    %theTab.UpperContent = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " " @ %ypos;
        extent = (1.0 - getWord(%theTab.getExtent(), 0)) @ " " @ 18;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "<spush><b>Create a game!<spop><br>Configure your game how you like here and then click create below to open it.<br>";
        maxLength = -1;
    };
    %theTab.add(%theTab.UpperContent);
    %theTab.UpperContent.forceReflow();
    %ypos = (%ypos + getWord(%theTab.UpperContent.getExtent(), 1));
    %theTab.add(new GuiTextCtrl("") {
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
    };);
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
    %theTab.add(%theTab.gameNameField);
    %ypos = (20.0 + %ypos);
    %theTab.add(new GuiTextCtrl("") {
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
    };);
    %ypos = (20.0 + %ypos);
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
    %theTab.add(%theTab.gameTypesDropdown);
    %ypos = (20.0 + %ypos);
    %theTab.add(new GuiTextCtrl("") {
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
    };);
    %ypos = (20.0 + %ypos);
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
    %theTab.add(%theTab.SettingWaitingRoom);
    %ypos = (20.0 + %ypos);
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
    %theTab.add(%theTab.SettingJoinInProgress);
    %ypos = (20.0 + %ypos);
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
    %theTab.add(%theTab.SettingAutoStartOnReady);
    %ypos = (20.0 + %ypos);
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
    %theTab.add(%theTab.SettingDropUnreadyPlayers);
    %ypos = (35.0 + %ypos);
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
    %theTab.add(%theTab.createGameButton);
    GameList.CreateTabResetDefaults();
};
function GameList::CreateTabSettingClicked(%this, %setting) {
    %this = %theTab.CreateTab;
    GameMgrHudTabs;
    if ((%setting $= "WaitingRoom")) {
        %onOff = %this.SettingWaitingRoom.getValue();
        %this.SettingAutoStartOnReady.setActive(%onOff);
        %this.SettingDropUnreadyPlayers.setActive(%onOff);
        %this.SettingJoinInProgress.setActive(%onOff);
    }
};
function GameList::CreateTabResetDefaults(%this) {
    %this = %this.CreateTab;
    GameMgrHudTabs;
    %this.SettingDropUnreadyPlayers.setValue(0);
    %this.SettingAutoStartOnReady.setValue(1);
    %this.SettingJoinInProgress.setValue(1);
    %this.SettingWaitingRoom.setValue(1);
    %this.gameNameField.setText(GameList.GetDefaultGameName());
    %this.gameTypesDropdown.SetSelected(0);
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
    if (($gameMgr::GAME_TYPES_COUNT < %n)) {
        if ((%n[$gameMgr::GAME_TYPES @ %n].INST_TITLE $= %gameType)) {
            %gameType = %n;
        }
        %n = (1.0 + %n);
    }
    if ((($gameMgr::GAME_TYPES_COUNT < %n) @ " " @ %gameType $= %this.gameTypesDropdown.getText())) {
        handleSystemMessage("msgInfoMessage", %errorMsgPrepend @ " " @ "we're having a problem with that game type.");
        error("Couldn't translate gametype text to commonID !! aborting create game<-" @ getScopeName());
        return;
    }
    %waitingRoom = %this.SettingWaitingRoom.getValue();
    %joinInProgress = %this.SettingJoinInProgress.getValue();
    %autoStartOnReady = %this.SettingAutoStartOnReady.getValue();
    %dropUnreadyPlayers = %this.SettingDropUnreadyPlayers.getValue();
    gameMgrClient.createGame(%gameName, %gameType, %waitingRoom, %joinInProgress, %autoStartOnReady, %dropUnreadyPlayers);
};
function GameList::CreateTabSetupGametypesDropdown(%this) {
    %this = %this.CreateTab;
    GameMgrHudTabs;
    %n = 0;
    if (($gameMgr::GAME_TYPES_COUNT < %n)) {
        if (%n[$gameMgr::GAME_TYPES @ %n].USER_CREATE) {
            %this.gameTypesDropdown.add(%n[$gameMgr::GAME_TYPES @ %n].INST_TITLE);
        }
        %n = (1.0 + %n);
    }
    %this.gameTypesDropdown.SetSelected(0);
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
    %this.getParent().scrollTo(0, (getWord(%pos, 1) - 1.0));
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
    if ((400.0 < ($gLastNameClickTime - %curTime))) {
    }
    if (($gLastNameClickName $= %SID)) {
        echo("Sending inspectGameRequest with SID==" @ %SID);
        gameMgrClient.requestToInspectGame(%SID);
        GameMgrHudTabs.selectTabWithName("INSPECT");
        if ((0.0 != $gLeftClickTimer)) {
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
        InfoPopupDlg.showInfoFor(%name);
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
    %numlists = GameList.getCount(%listName.lists);
    if ((0.0 == %numlists)) {
        %this.setText($gameMgr::GameList::NO_GAMES_MESSAGE);
        return;
    }
    %outString = %outString @ "<spush><color:FFFFFF><b>Your games:<spop><br>";
    %n = (1.0 - GameList.getCount(%listName.lists));
    if ((0.0 >= %n)) {
        %aList = GameList.getObject(%listName.lists, %n);
        %outString = %outString @ "<spush><linkcolor:" @ $gameMgr::ListColors::LIST_HEADER @ ">";
        if ((0.0 == %aList.collapsed)) {
            %listPrefix = "-";
        }
        %listPrefix = "+";
        %outString = %outString @ "<a:gamelink list " @ %aList @ " >" @ %listPrefix @ " " @ %aList[$gameMgr::GAME_TYPES @ %aList.gametype].title @ " " @ "(" @ %aList.getCount() @ " games)</a><spop><br>";
        if ((1.0 == %aList.collapsed)) {
            echo("the list is collapsed <-" @ getScopeName());
        }
        %i = (1.0 - %aList.getCount());
        if ((0.0 >= %i)) {
            echo("printing #" @ %i @ " game in the current list.<-" @ getScopeName());
            %aGame = %aList.getObject(%i);
            if (($gameMgr::GameStatus::CANT_START == %aGame.gamestatus)) {
                %color = $gameMgr::ListColors::CANT_START;
            }
            if (($gameMgr::GameStatus::WAITING == %aGame.gamestatus)) {
                %color = $gameMgr::ListColors::WAITING;
            }
            if (($gameMgr::GameStatus::STARTED == %aGame.gamestatus)) {
                %color = $gameMgr::ListColors::STARTED;
            }
            %color = $gameMgr::ListColors::ELSE;
            if (%aGame.deepUpdated) {
                %changed = "<spush><color:FF0000>*<spop>";
            }
            %changed = "";
            %outString = %outString @ %indent @ "<spush><linkcolor:" @ %color @ "><a:gamelink game " @ %aGame.serversideID @ " >" @ %aGame.gname @ " " @ "(" @ %aGame.playercount @ " players)</a><spop>" @ %changed @ "<br>";
            %i = (1.0 - %i);
        }
        %n = (1.0 - %n);
        (0.0 >= %i);
    }
    %this.setText(%outString);
};
