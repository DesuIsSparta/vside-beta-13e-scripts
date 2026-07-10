if (!(isObject())) {
    echo(getScopeName());
    class = GameMgrHudTabs @ new ScriptObject(GameMgrHudTabs) @ "TabControl";
    if (isObject()) {
        add();
    }
}
function GameMgrHudTabs::setup(%this) {
    echo(getTrace());
    if (!(initialized)) {
        %this.Initialize("25 25", "platform/client/ui/separator", "16 7", "horizontal");
        %this.newTab("MYGAMES", "platform/client/buttons/buddies");
        %this.newTab("INSPECT", "platform/client/buttons/aim_buddies");
        InspectTab = GameMgrHudTabContainer @ %this.getTabWithName("INSPECT") @ %this;
        %this;
        %this.newTab("CREATE", "platform/client/buttons/aim_buddies");
        CreateTab = GameMgrHudTabs @ %this.getTabWithName("CREATE") @ %this;
        MissionCleanup;
        CreateTab.setName("GameMgrCreateTab");
        %this.selectTabWithName("MYGAMES");
        %this.fillMYGAMESTab();
        %this.fillINSPECTtab();
        %this.fillCREATEtab();
    }
};
function GameMgrHudTabs::OnETSInviteFriends(%this) {
    if (!(isObject())) {
        error("no EtsInviteDialog, this should not happen");
        return EtsInviteDialog;
    }
    if (!(isVisible())) {
        open();
    }
};
function GameMgrHudTabs::fillINSPECTtab(%this) {
    echo(getScopeName());
    %theTab = InspectTab;
    %this;
    profile = GuiMLTextCtrl @ new ""() @ "ETSShadowTextProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = (1.0 - getWord(%theTab.getExtent(), 0)) @ " " @ 108;
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "";
    maxLength = -1;
    UpperContent = %theTab;
    UpperContent.bindClassName("GameMgrMLText");
    %theTab.add(UpperContent);
    profile = GuiScrollCtrl @ new ""() @ "ETSScrollProfile";
    0;
    horizSizing = %theTab @ %theTab @ "width";
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
    PlayerListScroll = %theTab;
    profile = new GuiTextListCtrl(PlayerList) @ "ETSTextListProfile";
    horizSizing = "width";
    vertSizing = "bottom";
    position = "0 0";
    extent = %theTab @ PlayerListScroll.getExtent();
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
    PlayerList = %theTab;
    PlayerListScroll.add(PlayerList);
    %theTab.add(PlayerListScroll);
    profile = GuiMLTextCtrl @ new ""() @ "ETSShadowTextProfile";
    0;
    horizSizing = %theTab @ %theTab @ "right";
    %theTab;
    vertSizing = "bottom";
    position = 0 @ " " @ %theTab @ getWord(PlayerListScroll.getExtent(), 1) @ (%theTab + getWord(PlayerListScroll.getPosition(), 1));
    extent = (1.0 - getWord(%theTab.getExtent(), 0)) @ " " @ 108;
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "";
    maxLength = -1;
    LowerContent = %theTab;
    LowerContent.bindClassName("GameMgrMLText");
    %theTab.add(LowerContent);
    inspectNothing();
};
$gameMgr::InspectTab::NO_INSPECTED_GAME_MESSAGE = "<spush><b>Inspect a game.<spop><br>You aren't inspecting a game right now. If you'd like to see how you or your friends are doing in a game, double click one on your list!";
function GameList::refreshInspectTab(%this) {
    %this = InspectTab;
    GameMgrHudTabs;
    if (!(areWeInspecting())) {
        UpperContent.setText($gameMgr::InspectTab::NO_INSPECTED_GAME_MESSAGE);
        LowerContent.setText("");
        PlayerList.clear();
        return %this;
    }
    %game = inspectedGame;
    gameMgrClient;
    if (%game.areWePlaying()) {
        %ourRecord = ourRecord;
        %game;
    }
    %gameType = title;
    %game[%game @ gametype];
    %plyrStr = (%game > playercount) ? " players" : " player";
    1.0;
    %upperText = gameMgrClient @ $gameMgr::GAME_TYPES @ "<spush><b>Inspect Game:<spop><br>" @ "name: " @ %game @ gname @ "<br>" @ "game: " @ %gameType @ "<br>" @ "<spush><b>created by " @ %game @ host @ "<spop><br>" @ %game @ playercount @ %plyrStr @ "<br>";
    if ((%game == gamestatus)) {
        if (!(%game.areWePlaying())) {
            %upperText = $gameMgr::GameStatus::STARTED @ gameMgrClient @ %upperText @ "You're not in this game. <a:game join " @ %game @ serversideID @ ">Join it!</a><br>";
        }
        if (!(%ourRecord SPC status $= "")) {
            %upperText = %upperText @ "<spush><b>our status:<spop> " @ %statusStr @ "<br>";
        }
        %upperText = %upperText @ "<spush><b>our score:<spop> " @ %ourRecord @ score @ "<br>";
        %upperText = %upperText @ "Top " @ $gameMgr::InspectTab::MAX_PLAYERS @ " Players:";
    }
    %upperText = %upperText @ "Waiting players (" @ %game @ readyCount @ " ready):";
    if ((%game > playercount)) {
        %upperText = $gameMgr::InspectTab::MAX_PLAYERS @ %upperText @ "<br>(only showing " @ $gameMgr::InspectTab::MAX_PLAYERS @ ")";
    }
    UpperContent.setText(%upperText);
    PlayerList.clear();
    %recordCount = PlayerRecords.getCount();
    %game;
    %n = 0;
    %this;
    if ((%recordCount < %n)) {
        %record = PlayerRecords.getObject(%n);
        %game;
        if ((%game == gamestatus)) {
            PlayerList.addRow(%n, %record @ score, %n);
        }
        if (ready) {
            %ready = "ready";
            %record;
        }
        %ready = "not ready";
        %record @ status @ "\t";
        PlayerList.addRow(%n, name @ "\t" @ %ready @ "\t" @ "", %n);
        %n = (1.0 + %n);
        %record;
    }
    %lowerText = "";
    (%recordCount < %n);
    %lowerText = %this @ %lowerText @ "Game status: ";
    %record @ name @ "\t";
    if ((%game == gamestatus)) {
        %lowerText = $gameMgr::GameStatus::CANT_START @ %lowerText @ "Can't start.";
        %this @ $gameMgr::GameStatus::STARTED @ %this @ "#" @ (1.0 + %n) @ "- ";
    }
    if ((%game == gamestatus)) {
        %lowerText = $gameMgr::GameStatus::WAITING @ %lowerText @ "Waiting on players.";
    }
    if ((%game == gamestatus)) {
        %lowerText = $gameMgr::GameStatus::STARTED @ %lowerText @ "Game started!";
    }
    %lowerText = %lowerText @ "<br>";
    %lowerText = %lowerText @ "<spush><b>Actions:<spop><br>";
    if (!(%game.areWePlaying())) {
        %lowerText = gameMgrClient @ %lowerText @ "Not playing yet...<a:game join " @ %game @ serversideID @ ">[Join game]</a><br>";
    }
    if ((%game != gamestatus)) {
    }
    if (!(postgameView)) {
        %readyText = !(ready) ? "[I'm ready]" : "[I'm not ready]";
        ourRecord;
        %lowerText = %this @ %game @ %lowerText @ "Change readiness:<a:game changeReady " @ %game @ serversideID @ " " @ %game @ ourRecord @ !(ready) @ ">" @ %readyText @ "</a><br>";
        $gameMgr::GameStatus::STARTED;
        if ((%game SPC host $= $player.getShapeName())) {
            %lowerText = %lowerText @ "You're the host. <a:game startGame " @ %game @ serversideID @ ">" @ "[start game]" @ "</a><br>";
        }
    }
    %lowerText = %lowerText @ "<a:game quit " @ %game @ serversideID @ ">[Quit game]</a><br>";
    %lowerText = %lowerText @ "<br><br>" @ "<a:game stopInspecting>[stop inspecting]</a>";
    %lowerText = %lowerText @ "<br>Want more people to play in this game? When it's inspected like this, right-click people or their name-links and choose \"Invite to game.\"";
    LowerContent.setText(%lowerText);
    UpperContent.forceReflow();
    %newPLSPosX = getWord(getPosition, 0);
    PlayerListScroll;
    %newPLSPosY = (%this + getWord(UpperContent.getPosition(), 1));
    getWord(UpperContent.getExtent(), 1);
    PlayerListScroll.reposition(%newPLSPosX, %newPLSPosY);
    %newLCPosX = getWord(getPosition, 0);
    LowerContent;
    %newLCPosY = (%this + getWord(PlayerListScroll.getPosition(), 1));
    getWord(PlayerListScroll.getExtent(), 1);
    LowerContent.reposition(%newLCPosX, %newLCPosY);
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
        %arguments.playerJoinGame();
    }
    if ((gameMgrClient SPC %command $= "quit")) {
        %arguments.playerQuitGame();
    }
    if ((gameMgrClient SPC %command $= "changeReady")) {
        getWord(%arguments, 0).playerChangeReadyStatus(getWord(%arguments, 1));
    }
    if ((gameMgrClient SPC %command $= "startGame")) {
        %arguments.playerRequestStartGame();
    }
    if ((gameMgrClient SPC %command $= "stopInspecting")) {
        inspectNothing();
    }
    error(gameMgrClient @ "GameMgr action link with unrecognized action=" @ %command @ ". <- " @ getScopeName());
};
function GameMgrHudTabs::tabSelected(%this, %tab) {
    if ((%tab SPC name $= "INSPECT")) {
    }
    if ((gameMgrClient == areWeInspecting())) {
        deepUpdated = gameMgrClient @ inspectedGame;
        1.0 @ 0;
        refresh();
    }
};
function GameList::switchIfInspectEmpty(%this) {
    if (!(areWeInspecting())) {
        "MYGAMES".selectTabWithName();
    }
};
function GameMgrHudTabs::fillMYGAMESTab(%this) {
    echo(getScopeName());
    %theTab = %this.getTabWithName("MYGAMES");
    profile = GuiScrollCtrl @ new ""() @ "ETSScrollProfile";
    0;
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
    profile = new GuiMLTextCtrl(GameList) @ "ETSFavTextListProfile";
    horizSizing = "width";
    vertSizing = "bottom";
    position = "0 0";
    extent = (1.0 - getWord(%theTab.getExtent(), 0)) @ " " @ 80;
    minExtent = "80 80";
    sluggishness = -1;
    visible = 1;
    %theTab.add();
    lists = SimSet @ new ""() @ GameList;
    0;
    if (isObject()) {
        lists.getId().add();
    }
    refresh();
};
function GameMgrHudTabs::fillCREATEtab(%this) {
    echo(getScopeName());
    %theTab = %this.getTabWithName("CREATE");
    %ypos = 1;
    profile = GuiMLTextCtrl @ new ""() @ "ETSShadowTextProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = 0 @ " " @ %ypos;
    extent = (1.0 - getWord(%theTab.getExtent(), 0)) @ " " @ 18;
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "<spush><b>Create a game!<spop><br>Configure your game how you like here and then click create below to open it.<br>";
    maxLength = -1;
    UpperContent = %theTab;
    %theTab.add(UpperContent);
    UpperContent.forceReflow();
    %ypos = (%theTab + getWord(UpperContent.getExtent(), 1));
    %ypos;
    profile = GuiTextCtrl @ new ""() @ "ETSShadowTextProfile";
    0;
    horizSizing = %theTab @ %theTab @ "right";
    vertSizing = "bottom";
    position = 0 @ " " @ %ypos;
    extent = "50 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Game name:";
    maxLength = -1;
    %theTab.add();
    profile = GuiTextEditCtrl @ new ""() @ "ETSDarkTextEditProfile";
    0;
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
    gameNameField = %theTab;
    %theTab.add(gameNameField);
    profile = GuiTextCtrl @ new ""() @ "ETSShadowTextProfile";
    0;
    horizSizing = %theTab @ "right";
    vertSizing = "bottom";
    %ypos = (20.0 + %ypos);
    position = 0 @ " ";
    extent = "150 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Choose a game type:";
    maxLength = 64;
    %theTab.add();
    profile = GuiPopUp2MenuCtrl @ new ""() @ "InfoWindowPopupProfile";
    0;
    scrollProfile = "DottedScrollProfile";
    winProfile = "InfoWindowPopupWindowProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    %ypos = (20.0 + %ypos);
    position = 0 @ " ";
    extent = "150 30";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "";
    maxLength = 255;
    maxPopupHeight = 200;
    allowReverse = 0;
    gameTypesDropdown = %theTab;
    CreateTabSetupGametypesDropdown();
    %theTab.add(gameTypesDropdown);
    profile = GuiTextCtrl @ new ""() @ "ETSShadowTextProfile";
    0;
    horizSizing = GameList @ %theTab @ "right";
    vertSizing = "bottom";
    %ypos = (20.0 + %ypos);
    position = 0 @ " ";
    extent = "150 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Game settings:";
    maxLength = 64;
    %theTab.add();
    profile = GuiCheckBoxCtrl @ new ""() @ "InfoWindowRadioButtonProfile";
    0;
    buttonType = "ToggleButton";
    horizSizing = "right";
    vertSizing = "bottom";
    %ypos = (20.0 + %ypos);
    position = 10 @ " ";
    extent = "150 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Waiting room";
    command = "GameList.CreateTabSettingClicked(\"WaitingRoom\");";
    maxLength = 64;
    SettingWaitingRoom = %theTab;
    %theTab.add(SettingWaitingRoom);
    profile = GuiCheckBoxCtrl @ new ""() @ "InfoWindowRadioButtonProfile";
    0;
    buttonType = %theTab @ "ToggleButton";
    horizSizing = "right";
    vertSizing = "bottom";
    %ypos = (20.0 + %ypos);
    position = 15 @ " ";
    extent = "150 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Allow join in progress";
    command = "GameList.CreateTabSettingClicked(\"JoinInProgress\");";
    maxLength = 64;
    SettingJoinInProgress = %theTab;
    %theTab.add(SettingJoinInProgress);
    profile = GuiCheckBoxCtrl @ new ""() @ "InfoWindowRadioButtonProfile";
    0;
    buttonType = %theTab @ "ToggleButton";
    horizSizing = "right";
    vertSizing = "bottom";
    %ypos = (20.0 + %ypos);
    position = 15 @ " ";
    extent = "150 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Auto-start when everyone is ready";
    command = "GameList.CreateTabSettingClicked(\"AutoStartOnReady\");";
    maxLength = 64;
    SettingAutoStartOnReady = %theTab;
    %theTab.add(SettingAutoStartOnReady);
    profile = GuiCheckBoxCtrl @ new ""() @ "InfoWindowRadioButtonProfile";
    0;
    buttonType = %theTab @ "ToggleButton";
    horizSizing = "right";
    vertSizing = "bottom";
    %ypos = (20.0 + %ypos);
    position = 15 @ " ";
    extent = "150 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Drop non-ready players on start";
    command = "GameList.CreateTabSettingClicked(\"DropUnreadyPlayers\");";
    maxLength = 64;
    SettingDropUnreadyPlayers = %theTab;
    %theTab.add(SettingDropUnreadyPlayers);
    profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton15Profile";
    0;
    horizSizing = %theTab @ "right";
    vertSizing = "bottom";
    %ypos = (35.0 + %ypos);
    position = 14 @ " ";
    extent = "134 15";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    command = "GameList.CreateTabCreateGame();";
    text = "Create Game";
    groupNum = -1;
    buttonType = "PushButton";
    createGameButton = %theTab;
    %theTab.add(createGameButton);
    CreateTabResetDefaults();
};
function GameList::CreateTabSettingClicked(%this, %setting) {
    %this = CreateTab;
    GameMgrHudTabs;
    if ((%setting $= "WaitingRoom")) {
        %onOff = SettingWaitingRoom.getValue();
        %this;
        SettingAutoStartOnReady.setActive(%onOff);
        SettingDropUnreadyPlayers.setActive(%onOff);
        SettingJoinInProgress.setActive(%onOff);
    }
};
function GameList::CreateTabResetDefaults(%this) {
    %this = CreateTab;
    GameMgrHudTabs;
    SettingDropUnreadyPlayers.setValue(0);
    SettingAutoStartOnReady.setValue(1);
    SettingJoinInProgress.setValue(1);
    SettingWaitingRoom.setValue(1);
    gameNameField.setText(GetDefaultGameName());
    gameTypesDropdown.SetSelected(0);
};
function GameList::GetDefaultGameName(%this) {
    if (isObject($player)) {
        return $player.getShapeName() @ "'s Game";
    }
    return "A Fun Game";
};
function GameList::CreateTabCreateGame(%this) {
    %this = CreateTab;
    GameMgrHudTabs;
    %errorMsgPrepend = "Sorry, couldn't create game:";
    %gameName = gameNameField.getText();
    %this;
    if ((%gameName $= "")) {
        handleSystemMessage("msgInfoMessage", %errorMsgPrepend @ " " @ "you didn't specify a game name.");
        return;
    }
    CreateTabResetDefaults();
    %gameType = gameTypesDropdown.getText();
    %this;
    %n = 0;
    GameList;
    if (($gameMgr::GAME_TYPES_COUNT < %n)) {
        if ((%n[$gameMgr::GAME_TYPES @ %n] SPC INST_TITLE $= %gameType)) {
            %gameType = %n;
        }
        %n = (1.0 + %n);
    }
    if ((%this $= gameTypesDropdown.getText())) {
        handleSystemMessage("msgInfoMessage", %errorMsgPrepend @ " " @ "we're having a problem with that game type.");
        error(($gameMgr::GAME_TYPES_COUNT < %n) SPC %gameType @ "Couldn't translate gametype text to commonID !! aborting create game<-" @ getScopeName());
        return;
    }
    %waitingRoom = SettingWaitingRoom.getValue();
    %this;
    %joinInProgress = SettingJoinInProgress.getValue();
    %this;
    %autoStartOnReady = SettingAutoStartOnReady.getValue();
    %this;
    %dropUnreadyPlayers = SettingDropUnreadyPlayers.getValue();
    %this;
    %gameName.createGame(%gameType, %waitingRoom, %joinInProgress, %autoStartOnReady, %dropUnreadyPlayers);
};
function GameList::CreateTabSetupGametypesDropdown(%this) {
    %this = CreateTab;
    GameMgrHudTabs;
    %n = 0;
    if (($gameMgr::GAME_TYPES_COUNT < %n)) {
        if (USER_CREATE) {
            gameTypesDropdown.add(INST_TITLE);
        }
        %n = (1.0 + %n);
        %n[$gameMgr::GAME_TYPES @ %n];
    }
    gameTypesDropdown.SetSelected(0);
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
    wakeUp();
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
        collapsed = %listName @ !(collapsed) @ %listName;
        refresh();
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
        %SID.requestToInspectGame();
        "INSPECT".selectTabWithName();
        if ((0.0 != $gLeftClickTimer)) {
            cancel($gLeftClickTimer);
            $gLeftClickTimer = 0;
            GameMgrHudTabs;
        }
    }
    $gLeftClickTimer = schedule(450, 0, "onSingleClickGameName", %SID);
    gameMgrClient;
    $gLastNameClickTime = %curTime;
    $gLastNameClickName = %SID;
};
function onRightClickGameName(%name) {
};
function onSingleClickGameName(%name) {
    if (showPlayerInfoPopup()) {
    }
    if (!(%name $= $player.getShapeName())) {
        open();
        %name.showInfoFor();
    }
};
$gameMgr::GameList::NO_GAMES_MESSAGE = "<spush><color:FFFFFF><b>Your games.<spop><spush><color:FFFFFF><br>This is where your games would be listed - but you're not playing any!<br>Join a game or start your own!<br><spop>";
function GameList::refresh(%this) {
    %outString = "";
    %indent = "   ";
    %color = "";
    if (!(isObject(lists))) {
        error(GameList @ "GameList.Lists unavailable!<-" @ getScopeName());
        return;
    }
    %numlists = lists.getCount();
    GameList;
    if ((0.0 == %numlists)) {
        %this.setText($gameMgr::GameList::NO_GAMES_MESSAGE);
        return;
    }
    %outString = %outString @ "<spush><color:FFFFFF><b>Your games:<spop><br>";
    %n = (GameList - lists.getCount());
    1.0;
    if ((0.0 >= %n)) {
        %aList = lists.getObject(%n);
        GameList;
        %outString = %outString @ "<spush><linkcolor:" @ $gameMgr::ListColors::LIST_HEADER @ ">";
        if ((%aList == collapsed)) {
            %listPrefix = "-";
            0.0;
        }
        %listPrefix = "+";
        %outString = %outString @ "<a:gamelink list " @ %aList @ " >" @ %listPrefix @ " " @ $gameMgr::GAME_TYPES @ %aList[%aList @ gametype] @ title @ " " @ "(" @ %aList.getCount() @ " games)</a><spop><br>";
        if ((%aList == collapsed)) {
            echo(1.0 @ "the list is collapsed <-" @ getScopeName());
        }
        %i = (1.0 - %aList.getCount());
        if ((0.0 >= %i)) {
            echo("printing #" @ %i @ " game in the current list.<-" @ getScopeName());
            %aGame = %aList.getObject(%i);
            if ((%aGame == gamestatus)) {
                %color = $gameMgr::ListColors::CANT_START;
                $gameMgr::GameStatus::CANT_START;
            }
            if ((%aGame == gamestatus)) {
                %color = $gameMgr::ListColors::WAITING;
                $gameMgr::GameStatus::WAITING;
            }
            if ((%aGame == gamestatus)) {
                %color = $gameMgr::ListColors::STARTED;
                $gameMgr::GameStatus::STARTED;
            }
            %color = $gameMgr::ListColors::ELSE;
            if (deepUpdated) {
                %changed = "<spush><color:FF0000>*<spop>";
                %aGame;
            }
            %changed = "";
            %outString = %outString @ %indent @ "<spush><linkcolor:" @ %color @ "><a:gamelink game " @ %aGame @ serversideID @ " >" @ %aGame @ gname @ " " @ "(" @ %aGame @ playercount @ " players)</a><spop>" @ %changed @ "<br>";
            %i = (1.0 - %i);
        }
        %n = (1.0 - %n);
        (0.0 >= %i);
    }
    %this.setText(%outString);
};
