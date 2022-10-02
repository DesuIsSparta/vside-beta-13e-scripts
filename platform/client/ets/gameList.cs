if (!isObject(GameMgrHudTabs))
{
    echo(getScopeName());
    new ScriptObject(GameMgrHudTabs);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(GameMgrHudTabs);
    }
}
function GameMgrHudTabs::setup(%this)
{
    echo(getTrace());
    if (!%this.initialized)
    {
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
    return ;
}
function GameMgrHudTabs::OnETSInviteFriends(%this)
{
    if (!isObject(EtsInviteDialog))
    {
        error("no EtsInviteDialog, this should not happen");
        return ;
    }
    if (!EtsInviteDialog.isVisible())
    {
        EtsInviteDialog.open();
    }
    return ;
}
function GameMgrHudTabs::fillINSPECTtab(%this)
{
    echo(getScopeName());
    %theTab = %this.InspectTab;
    %theTab.UpperContent = new GuiMLTextCtrl()
    {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) - 1 SPC 108;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = -1;
    };
    %theTab.UpperContent.bindClassName("GameMgrMLText");
    %theTab.add(%theTab.UpperContent);
    %theTab.PlayerListScroll = new GuiScrollCtrl()
    {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 109";
        extent = getWord(%theTab.getExtent(), 0) SPC 70;
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
    %theTab.PlayerList = new GuiTextListCtrl(PlayerList)
    {
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
    %theTab.LowerContent = new GuiMLTextCtrl()
    {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 SPC getWord(%theTab.PlayerListScroll.getPosition(), 1) + getWord(%theTab.PlayerListScroll.getExtent(), 1);
        extent = getWord(%theTab.getExtent(), 0) - 1 SPC 108;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = -1;
    };
    %theTab.LowerContent.bindClassName("GameMgrMLText");
    %theTab.add(%theTab.LowerContent);
    gameMgrClient.inspectNothing();
    return ;
}
$gameMgr::InspectTab::NO_INSPECTED_GAME_MESSAGE = "<spush><b>Inspect a game.<spop><br>You aren\'t inspecting a game right now. If you\'d like to see how you or your friends are doing in a game, double click one on your list!";
function GameList::refreshInspectTab(%this)
{
    %this = GameMgrHudTabs.InspectTab;
    if (!gameMgrClient.areWeInspecting())
    {
        %this.UpperContent.setText($gameMgr::InspectTab::NO_INSPECTED_GAME_MESSAGE);
        %this.LowerContent.setText("");
        %this.PlayerList.clear();
        return ;
    }
    %game = gameMgrClient.inspectedGame;
    if (gameMgrClient.areWePlaying(%game))
    {
        %ourRecord = %game.ourRecord;
    }
    %gameType = $gameMgr::GAME_TYPES[%game.gametype].title;
    %plyrStr = %game.playercount > 1 ? " players" : " player";
    %upperText = "<spush><b>Inspect Game:<spop><br>" @ "name: " @ %game.gname @ "<br>" @ "game: " @ %gameType @ "<br>" @ "<spush><b>created by " @ %game.host @ "<spop><br>" @ %game.playercount @ %plyrStr @ "<br>";
    if (%game.gamestatus == $gameMgr::GameStatus::STARTED)
    {
        if (!gameMgrClient.areWePlaying(%game))
        {
            %upperText = %upperText @ "You\'re not in this game. <a:game join " @ %game.serversideID @ ">Join it!</a><br>";
        }
        else
        {
            if (!(%ourRecord.status $= ""))
            {
                %upperText = %upperText @ "<spush><b>our status:<spop> " @ %statusStr @ "<br>";
            }
            %upperText = %upperText @ "<spush><b>our score:<spop> " @ %ourRecord.score @ "<br>";
        }
        %upperText = %upperText @ "Top " @ $gameMgr::InspectTab::MAX_PLAYERS @ " Players:";
    }
    else
    {
        %upperText = %upperText @ "Waiting players (" @ %game.readyCount @ " ready):";
        if (%game.playercount > $gameMgr::InspectTab::MAX_PLAYERS)
        {
            %upperText = %upperText @ "<br>(only showing " @ $gameMgr::InspectTab::MAX_PLAYERS @ ")";
        }
    }
    %this.UpperContent.setText(%upperText);
    %this.PlayerList.clear();
    %recordCount = %game.PlayerRecords.getCount();
    %n = 0;
    while (%n < %recordCount)
    {
        %record = %game.PlayerRecords.getObject(%n);
        if (%game.gamestatus == $gameMgr::GameStatus::STARTED)
        {
            %this.PlayerList.addRow(%n, "#" @ %n + 1 @ "- " @ %record.name TAB %record.status TAB %record.score, %n);
        }
        else
        {
            if (%record.ready)
            {
                %ready = "ready";
            }
            else
            {
                %ready = "not ready";
            }
            %this.PlayerList.addRow(%n, %record.name TAB %ready TAB "", %n);
        }
        %n = %n + 1;
    }
    %lowerText = "";
    %lowerText = %lowerText @ "Game status: ";
    if (%game.gamestatus == $gameMgr::GameStatus::CANT_START)
    {
        %lowerText = %lowerText @ "Can\'t start.";
    }
    else
    {
        if (%game.gamestatus == $gameMgr::GameStatus::WAITING)
        {
            %lowerText = %lowerText @ "Waiting on players.";
        }
        else
        {
            if (%game.gamestatus == $gameMgr::GameStatus::STARTED)
            {
                %lowerText = %lowerText @ "Game started!";
            }
        }
    }
    %lowerText = %lowerText @ "<br>";
    %lowerText = %lowerText @ "<spush><b>Actions:<spop><br>";
    if (!gameMgrClient.areWePlaying(%game))
    {
        %lowerText = %lowerText @ "Not playing yet...<a:game join " @ %game.serversideID @ ">[Join game]</a><br>";
    }
    else
    {
        if ((%game.gamestatus != $gameMgr::GameStatus::STARTED) && !(%this.postgameView))
        {
            %readyText = !%game.ourRecord.ready ? "[I\'m ready]" : "[I\'m not ready]";
            %lowerText = %lowerText @ "Change readiness:<a:game changeReady " @ %game.serversideID SPC !%game.ourRecord.ready @ ">" @ %readyText @ "</a><br>";
            if (%game.host $= $player.getShapeName())
            {
                %lowerText = %lowerText @ "You\'re the host. <a:game startGame " @ %game.serversideID @ ">" @ "[start game]" @ "</a><br>";
            }
        }
        %lowerText = %lowerText @ "<a:game quit " @ %game.serversideID @ ">[Quit game]</a><br>";
    }
    %lowerText = %lowerText @ "<br><br>" @ "<a:game stopInspecting>[stop inspecting]</a>";
    %lowerText = %lowerText @ "<br>Want more people to play in this game? When it\'s inspected like this, right-click people or their name-links and choose \"Invite to game.\"";
    %this.LowerContent.setText(%lowerText);
    %this.UpperContent.forceReflow();
    %newPLSPosX = getWord(%this.PlayerListScroll.getPosition, 0);
    %newPLSPosY = getWord(%this.UpperContent.getPosition(), 1) + getWord(%this.UpperContent.getExtent(), 1);
    %this.PlayerListScroll.reposition(%newPLSPosX, %newPLSPosY);
    %newLCPosX = getWord(%this.LowerContent.getPosition, 0);
    %newLCPosY = getWord(%this.PlayerListScroll.getPosition(), 1) + getWord(%this.PlayerListScroll.getExtent(), 1);
    %this.LowerContent.reposition(%newLCPosX, %newLCPosY);
    return ;
}
function GameMgrMLText::onURL(%this, %url)
{
    %firstWord = getWord(%url, 0);
    if (%firstWord $= "gamelink")
    {
        onLeftClickPlayerName(getWords(%url, 2), "");
    }
    else
    {
        if (!(%firstWord $= "game"))
        {
            warn("GameMgrMLText received an unrecognized link URL=" @ %url @ ". Returning!<-" @ getScopeName());
            return ;
        }
    }
    %command = getWord(%url, 1);
    %arguments = getWords(%url, 2);
    if (%command $= "join")
    {
        gameMgrClient.playerJoinGame(%arguments);
    }
    else
    {
        if (%command $= "quit")
        {
            gameMgrClient.playerQuitGame(%arguments);
        }
        else
        {
            if (%command $= "changeReady")
            {
                gameMgrClient.playerChangeReadyStatus(getWord(%arguments, 0), getWord(%arguments, 1));
            }
            else
            {
                if (%command $= "startGame")
                {
                    gameMgrClient.playerRequestStartGame(%arguments);
                }
                else
                {
                    if (%command $= "stopInspecting")
                    {
                        gameMgrClient.inspectNothing();
                    }
                    else
                    {
                        error("GameMgr action link with unrecognized action=" @ %command @ ". <- " @ getScopeName());
                    }
                }
            }
        }
    }
    return ;
}
function GameMgrHudTabs::tabSelected(%this, %tab)
{
    if ((%tab.name $= "INSPECT") && (gameMgrClient.areWeInspecting() == 1))
    {
        gameMgrClient.inspectedGame.deepUpdated = 0;
        GameList.refresh();
    }
    return ;
}
function GameList::switchIfInspectEmpty(%this)
{
    if (!gameMgrClient.areWeInspecting())
    {
        GameMgrHudTabs.selectTabWithName("MYGAMES");
    }
    return ;
}
function GameMgrHudTabs::fillMYGAMESTab(%this)
{
    echo(getScopeName());
    %theTab = %this.getTabWithName("MYGAMES");
    new GuiScrollCtrl()
    {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) SPC getWord(%theTab.getExtent(), 1) - 24;
        minExtent = "10 10";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    }.add(new GuiScrollCtrl()
    {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) SPC getWord(%theTab.getExtent(), 1) - 24;
        minExtent = "10 10";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    });
    GameList.lists = new SimSet();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(GameList.lists.getId());
    }
    GameList.refresh();
    return ;
}
function GameMgrHudTabs::fillCREATEtab(%this)
{
    echo(getScopeName());
    %theTab = %this.getTabWithName("CREATE");
    %ypos = 1;
    %theTab.UpperContent = new GuiMLTextCtrl()
    {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 SPC %ypos;
        extent = getWord(%theTab.getExtent(), 0) - 1 SPC 18;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "<spush><b>Create a game!<spop><br>Configure your game how you like here and then click create below to open it.<br>";
        maxLength = -1;
    };
    %theTab.add(%theTab.UpperContent);
    %theTab.UpperContent.forceReflow();
    %ypos = getWord(%theTab.UpperContent.getExtent(), 1) + %ypos;
    %theTab.gameNameField = new GuiTextEditCtrl()
    {
        profile = "ETSDarkTextEditProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 65 SPC %ypos;
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
    %theTab.gameTypesDropdown = new GuiPopUp2MenuCtrl()
    {
        profile = "InfoWindowPopupProfile";
        scrollProfile = "DottedScrollProfile";
        winProfile = "InfoWindowPopupWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 SPC %ypos = %ypos + 20;
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
    %theTab.SettingWaitingRoom = new GuiCheckBoxCtrl()
    {
        profile = "InfoWindowRadioButtonProfile";
        buttonType = "ToggleButton";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 10 SPC %ypos = %ypos + 20;
        extent = "150 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Waiting room";
        command = "GameList.CreateTabSettingClicked(\"WaitingRoom\");";
        maxLength = 64;
    };
    %theTab.add(%theTab.SettingWaitingRoom);
    %theTab.SettingJoinInProgress = new GuiCheckBoxCtrl()
    {
        profile = "InfoWindowRadioButtonProfile";
        buttonType = "ToggleButton";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 15 SPC %ypos = %ypos + 20;
        extent = "150 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Allow join in progress";
        command = "GameList.CreateTabSettingClicked(\"JoinInProgress\");";
        maxLength = 64;
    };
    %theTab.add(%theTab.SettingJoinInProgress);
    %theTab.SettingAutoStartOnReady = new GuiCheckBoxCtrl()
    {
        profile = "InfoWindowRadioButtonProfile";
        buttonType = "ToggleButton";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 15 SPC %ypos = %ypos + 20;
        extent = "150 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Auto-start when everyone is ready";
        command = "GameList.CreateTabSettingClicked(\"AutoStartOnReady\");";
        maxLength = 64;
    };
    %theTab.add(%theTab.SettingAutoStartOnReady);
    %theTab.SettingDropUnreadyPlayers = new GuiCheckBoxCtrl()
    {
        profile = "InfoWindowRadioButtonProfile";
        buttonType = "ToggleButton";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 15 SPC %ypos = %ypos + 20;
        extent = "150 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Drop non-ready players on start";
        command = "GameList.CreateTabSettingClicked(\"DropUnreadyPlayers\");";
        maxLength = 64;
    };
    %theTab.add(%theTab.SettingDropUnreadyPlayers);
    %theTab.createGameButton = new GuiVariableWidthButtonCtrl()
    {
        profile = "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 14 SPC %ypos = %ypos + 35;
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
    return ;
}
function GameList::CreateTabSettingClicked(%this, %setting)
{
    %this = GameMgrHudTabs.CreateTab;
    if (%setting $= "WaitingRoom")
    {
        %onOff = %this.SettingWaitingRoom.getValue();
        %this.SettingAutoStartOnReady.setActive(%onOff);
        %this.SettingDropUnreadyPlayers.setActive(%onOff);
        %this.SettingJoinInProgress.setActive(%onOff);
    }
    return ;
}
function GameList::CreateTabResetDefaults(%this)
{
    %this = GameMgrHudTabs.CreateTab;
    %this.SettingDropUnreadyPlayers.setValue(0);
    %this.SettingAutoStartOnReady.setValue(1);
    %this.SettingJoinInProgress.setValue(1);
    %this.SettingWaitingRoom.setValue(1);
    %this.gameNameField.setText(GameList.GetDefaultGameName());
    %this.gameTypesDropdown.SetSelected(0);
    return ;
}
function GameList::GetDefaultGameName(%this)
{
    if (isObject($player))
    {
        return $player.getShapeName() @ "\'s Game";
    }
    else
    {
        return "A Fun Game";
    }
    return ;
}
function GameList::CreateTabCreateGame(%this)
{
    %this = GameMgrHudTabs.CreateTab;
    %errorMsgPrepend = "Sorry, couldn\'t create game:";
    %gameName = %this.gameNameField.getText();
    if (%gameName $= "")
    {
        handleSystemMessage("msgInfoMessage", %errorMsgPrepend SPC "you didn\'t specify a game name.");
        return ;
    }
    GameList.CreateTabResetDefaults();
    %gameType = %this.gameTypesDropdown.getText();
    %n = 0;
    while (%n < $gameMgr::GAME_TYPES_COUNT)
    {
        if ($gameMgr::GAME_TYPES[%n].INST_TITLE $= %gameType)
        {
            %gameType = %n;
            break;
        }
        %n = %n + 1;
    }
    if (%gameType $= %this.gameTypesDropdown.getText())
    {
        handleSystemMessage("msgInfoMessage", %errorMsgPrepend SPC "we\'re having a problem with that game type.");
        error("Couldn\'t translate gametype text to commonID !! aborting create game<-" @ getScopeName());
        return ;
    }
    %waitingRoom = %this.SettingWaitingRoom.getValue();
    %joinInProgress = %this.SettingJoinInProgress.getValue();
    %autoStartOnReady = %this.SettingAutoStartOnReady.getValue();
    %dropUnreadyPlayers = %this.SettingDropUnreadyPlayers.getValue();
    gameMgrClient.createGame(%gameName, %gameType, %waitingRoom, %joinInProgress, %autoStartOnReady, %dropUnreadyPlayers);
    return ;
}
function GameList::CreateTabSetupGametypesDropdown(%this)
{
    %this = GameMgrHudTabs.CreateTab;
    %n = 0;
    while (%n < $gameMgr::GAME_TYPES_COUNT)
    {
        if ($gameMgr::GAME_TYPES[%n].USER_CREATE)
        {
            %this.gameTypesDropdown.add($gameMgr::GAME_TYPES[%n].INST_TITLE);
        }
        %n = %n + 1;
    }
    %this.gameTypesDropdown.SetSelected(0);
    return ;
}
function GameMgrHudTabs::wakeUp(%this)
{
    log("general", "debug", getScopeName() SPC "- gameMgr disabled.");
    return ;
    echo(getScopeName());
    %this.setup();
    %this.selectCurrentTab();
    return ;
}
function GameMgrHudWin::wakeUp(%this)
{
    echo(getScopeName());
    GameMgrHudTabs.wakeUp();
    return ;
}
function GameList::scrollToPos(%this, %pos)
{
    %this.getParent().scrollTo(0, 1 - getWord(%pos, 1));
    return ;
}
function GameList::onURL(%this, %url)
{
    echo(getScopeName());
    if (!(firstWord(%url) $= "gamelink"))
    {
        return ;
    }
    if (getWord(%url, 1) $= "game")
    {
        %SID = getWord(%url, 2);
        onLeftClickGameName(%SID);
    }
    else
    {
        if (getWord(%url, 1) $= "list")
        {
            %listName = getWords(%url, 2);
            echo("handling a list \"" @ %listName @ "\" <-" @ getScopeName());
            %listName.collapsed = !%listName.collapsed;
            GameList.refresh();
        }
    }
    return ;
}
function GameList::onRightURL(%this, %url)
{
    if (!(firstWord(%url) $= "gamelink"))
    {
        return ;
    }
    if (getWord(%url, 1) $= "game")
    {
        %SID = getWord(%url, 2);
        onRightClickGameName(%SID);
    }
    return ;
}
function onLeftClickGameName(%SID)
{
    %curTime = getSimTime();
    if (((%curTime - $gLastNameClickTime) < 400) && ($gLastNameClickName $= %SID))
    {
        echo("Sending inspectGameRequest with SID==" @ %SID);
        gameMgrClient.requestToInspectGame(%SID);
        GameMgrHudTabs.selectTabWithName("INSPECT");
        if ($gLeftClickTimer != 0)
        {
            cancel($gLeftClickTimer);
            $gLeftClickTimer = 0;
        }
    }
    else
    {
        $gLeftClickTimer = schedule(450, 0, "onSingleClickGameName", %SID);
    }
    $gLastNameClickTime = %curTime;
    $gLastNameClickName = %SID;
    return ;
}
function onRightClickGameName(%name)
{
    return ;
}
function onSingleClickGameName(%name)
{
    if (showPlayerInfoPopup() && !((%name $= $player.getShapeName())))
    {
        InfoPopupDlg.open();
        InfoPopupDlg.showInfoFor(%name);
    }
    return ;
}
$gameMgr::GameList::NO_GAMES_MESSAGE = "<spush><color:FFFFFF><b>Your games.<spop><spush><color:FFFFFF><br>This is where your games would be listed - but you\'re not playing any!<br>Join a game or start your own!<br><spop>";
function GameList::refresh(%this)
{
    %outString = "";
    %indent = "   ";
    %color = "";
    if (!isObject(GameList.lists))
    {
        error("GameList.Lists unavailable!<-" @ getScopeName());
        return ;
    }
    %numlists = GameList.lists.getCount();
    if (%numlists == 0)
    {
        %this.setText($gameMgr::GameList::NO_GAMES_MESSAGE);
        return ;
    }
    %outString = %outString @ "<spush><color:FFFFFF><b>Your games:<spop><br>";
    %n = GameList.lists.getCount() - 1;
    while (%n >= 0)
    {
        %aList = GameList.lists.getObject(%n);
        %outString = %outString @ "<spush><linkcolor:" @ $gameMgr::ListColors::LIST_HEADER @ ">";
        if (%aList.collapsed == 0)
        {
            %listPrefix = "-";
        }
        else
        {
            %listPrefix = "+";
        }
        %outString = %outString @ "<a:gamelink list " @ %aList @ " >" @ %listPrefix SPC $gameMgr::GAME_TYPES[%aList.gametype].title SPC "(" @ %aList.getCount() @ " games)</a><spop><br>";
        if (%aList.collapsed == 1)
        {
            echo("the list is collapsed <-" @ getScopeName());
        }
        else
        {
            %i = %aList.getCount() - 1;
            while (%i >= 0)
            {
                echo("printing #" @ %i @ " game in the current list.<-" @ getScopeName());
                %aGame = %aList.getObject(%i);
                if (%aGame.gamestatus == $gameMgr::GameStatus::CANT_START)
                {
                    %color = $gameMgr::ListColors::CANT_START;
                }
                else
                {
                    if (%aGame.gamestatus == $gameMgr::GameStatus::WAITING)
                    {
                        %color = $gameMgr::ListColors::WAITING;
                    }
                    else
                    {
                        if (%aGame.gamestatus == $gameMgr::GameStatus::STARTED)
                        {
                            %color = $gameMgr::ListColors::STARTED;
                        }
                        else
                        {
                            %color = $gameMgr::ListColors::ELSE;
                        }
                    }
                }
                if (%aGame.deepUpdated)
                {
                    %changed = "<spush><color:FF0000>*<spop>";
                }
                else
                {
                    %changed = "";
                }
                %outString = %outString @ %indent @ "<spush><linkcolor:" @ %color @ "><a:gamelink game " @ %aGame.serversideID @ " >" @ %aGame.gname SPC "(" @ %aGame.playercount @ " players)</a><spop>" @ %changed @ "<br>";
                %i = %i - 1;
            }
        }
        %n = %n - 1;
    }
    %this.setText(%outString);
    return ;
}
