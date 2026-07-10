if (!(isObject(geHighScoresPanelTabs))) {
    new ScriptObject(geHighScoresPanelTabs) {
        class = "TabControl";
    };
    if (isObject(MissionCleanup)) {
        geHighScoresPanelTabs.add(MissionCleanup);
    }
}
function toggleHighScoresPanel() {
    toggleVisibleState(geHighScoresPanel);
};
safeEnsureScriptObject("StringMap", "HumanReadableGameNamesMap");
"The Grind".put(HumanReadableGameNamesMap, "TheGrind");
"Materiel".put(HumanReadableGameNamesMap, "Materiel");
"Sumo".put(HumanReadableGameNamesMap, "Sumo");
function geHighScoresPanel::open(%this, %gameName, %gameStationId) {
    %this.Initialize();
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    geHighScoresPanelTabs.selectCurrentTab();
    %this.gameName = %gameName;
    %this.gameStationId = %gameStationId;
    %humanReadableGameName = %this.gameName.get(HumanReadableGameNamesMap);
    %colon = (%humanReadableGameName $= "") ? "" : ": ";
    "High Scores" @ %colon @ %humanReadableGameName.setText(geHighScoresTitleText);
    %request = sendRequest_GetHighGameScores($Player::Name, %this.gameName, 0, 25, "onDoneOrErrorCallback_GetHighGameScores");
    %request.global = 1;
    %this.requestStarted();
    %request = sendRequest_GetHighGameScoresForStation($Player::Name, %this.gameStationId, 0, 25, "onDoneOrErrorCallback_GetHighGameScores");
    %request.global = 0;
    %this.requestStarted();
};
function clientCmdOpenHighScoresFor(%gameName, %gameStationId) {
    %gameStationId.open(geHighScoresPanel, %gameName);
};
function geHighScoresPanel::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    return 1;
};
function geHighScoresPanel::Initialize(%this) {
    geHighScoresPanelTabs.setup();
    if (!(isObject(%this.waitIcon))) {
        %this.requestsPending = 0;
        %this.waitIcon = AnimCtrl::newAnimCtrl("457 24", "18 18");
        60.setDelay(%this.waitIcon);
        "platform/client/ui/wait0.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait1.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait2.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait3.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait4.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait5.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait6.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait7.png".addFrame(%this.waitIcon);
        0.setVisible(%this.waitIcon);
        %this.waitIcon.add(%this);
    }
};
function geHighScoresPanel::requestStarted(%this) {
    %this.requestsPending = (%this.requestsPending + 1.0);
    1.setVisible(%this.waitIcon);
    %this.waitIcon.start();
};
function geHighScoresPanel::requestStopped(%this) {
    %this.requestsPending = (%this.requestsPending - 1.0);
    if ((%this.requestsPending <= 0.0)) {
        %this.requestsPending = 0;
        %this.waitIcon.stop();
        0.setVisible(%this.waitIcon);
    }
};
function onDoneOrErrorCallback_GetHighGameScores(%request) {
    geHighScoresPanel.requestStopped();
    if (%request.checkSuccess()) {
        %global = %request.global;
        %tabName = %global ? "Global" : "This Machine";
        %tab = %tabName.getTabWithName(geHighScoresPanelTabs);
        if (!(isObject(%tab))) {
            error(getTrace() @ " " @ "tab with name" @ " " @ %tabName @ " " @ "not found!");
            return;
        }
        %dataTable = %tab.DataTable;
        %dataTable.getRowCount().removeRowsByIndex(%dataTable, 0);
        %count = "scores.scoresCount".getValue(%request);
        %count.addRows(%dataTable);
        %userRanking = "";
        %userScore = "";
        %userScoreDate = "";
        %i = 0;
        while ((%i < %count)) {
            %prefix = "scores.scores" @ %i;
            %score = %prefix @ ".score".getValue(%request);
            %scoreRanking = %prefix @ ".scoreRanking".getValue(%request);
            %userName = %prefix @ ".userName".getValue(%request);
            %dateAttained = %prefix @ ".dateAttained".getValue(%request);
            if ((%userName $= $Player::Name)) {
            }
            if ((%userRanking $= "")) {
                %userRanking = %scoreRanking;
                %userScore = %score;
                %userScoreDate = %dateAttained;
            }
            %style = (%userName.getFriendStatus(BuddyHudWin) $= "friends") ? "UserName_Friend" : "UserName_Normal";
            %rowData = "rank" @ "\t" @ %scoreRanking @ "\t" @ %scoreRanking @ "\n" @ "avatar" @ "\t" @ %userName @ "\t" @ "platform/client/ui/tgf/tgf_profile_default" @ "\n" @ "username" @ "\t" @ %userName @ "\t" @ mlStyle(%userName, %style) @ "\n" @ "date" @ "\t" @ %dateAttained @ "\t" @ %dateAttained @ "\n" @ "score" @ "\t" @ %score @ "\t" @ %score;
            %rowData.setRowDataByIndex(%dataTable, %i);
            %rowData = "avatar" @ "\t" @ %userName @ "\t" @ $Net::AvatarURL @ urlEncode(%userName) @ "?size=S";
            %rowData.setRowDataByIndex(%dataTable, %i);
            %i = (%i + 1.0);
        }
        %dataTable.updateListeners();
        if (((%i < %count) @ " " @ %userRanking $= "")) {
            %userRanking = "scores.userRanking".getValue(%request);
            %userScore = "scores.userScore".getValue(%request);
            %userScoreDate = "scores.userScoreDate".getValue(%request);
        }
        if ((%userRanking $= "")) {
            if (%global) {
                %hrGameName = geHighScoresPanel.gameName.get(HumanReadableGameNamesMap);
                %text = "You have no score for " @ %hrGameName @ ".";
            }
            %text = "You have no score on this machine.";
            %text.setText(%tab.noScoreText);
            1.setVisible(%tab.noScoreText);
            0.setVisible(%tab.userScoresPanel);
        }
        "<clip:111>" @ $Player::Name.setText(%tab.usernameField);
        %userScore.setText(%tab.bestScoreField);
        %userRanking.setText(%tab.rankField);
        %userScoreDate.setText(%tab.dateField);
        0.setVisible(%tab.noScoreText);
        1.setVisible(%tab.userScoresPanel);
    }
};
function geHighScoresPanelTabs::createButton(%this, %bitmapName, %tab, %name) {
    return new GuiBitmapButtonCtrl("") {
        profile = "ClipboardTabButtonProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = %this.buttonSize;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = %this.getId() @ ".selectTab(" @ %tab.getId() @ ");";
        text = %name;
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = %bitmapName;
        helpTag = 0;
        drawText = 1;
    };;
};
function geHighScoresPanelTabs::setup(%this) {
    if (!(%this.initialized)) {
        "horizontal".Initialize(%this, geHighScoresPanelTabContainer, "109 25", "", "0 0");
        "platform/client/buttons/clipboard_tab".newTab(%this, "This Machine");
        "platform/client/buttons/clipboard_tab".newTab(%this, "Global");
        "This Machine".selectTabWithName(%this);
        %this.fillTabs();
    }
};
function geHighScoresPanelTabs::fillTabs(%this) {
    "This Machine".fillTabWithName(%this);
    "Global".fillTabWithName(%this);
};
function geHighScoresPanelTabs::fillTabWithName(%this, %tabName) {
    %tab = %tabName.getTabWithName(%this);
    if (!(isObject(%tab))) {
        return;
    }
    ClipboardProfile.setProfile(%tab);
    %dataTable = new DataTable("");
    %tab.DataTable = %dataTable;
    30.addColumn(%dataTable, "rank", "Rank", "number");
    0.addColumn(%dataTable, "avatar", "", "image", 20);
    150.addColumn(%dataTable, "username", "Name", "string");
    180.addColumn(%dataTable, "date", "Date", "string");
    70.addColumn(%dataTable, "score", "High Score", "number");
    %guiTable = new GuiTableCtrl("") {
        position = "5 5";
        extent = "460 372";
        horizSizing = "width";
        vertSizing = "height";
        visible = 1;
        spacing = 2;
    };
    "geHighScoresGuiTable".bindClassName(%guiTable);
    17.setChildrenExtents(%guiTable);
    %dataTable.setDataTable(%guiTable);
    %tab.GuiTable = %guiTable;
    %guiTable.add(%tab);
    ClipboardHeaderCellProfile.setHeaderCellProfile(%guiTable);
    ClipboardHeaderCellButtonProfile.setHeaderCellButtonProfile(%guiTable);
    ClipboardHeaderMLTextProfile.setHeaderCellMLTextProfile(%guiTable);
    %userScoresPanel = new GuiControl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "top";
        position = "5 380";
        extent = "460 50";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %tab.userScoresPanel = %userScoresPanel;
    %userScoresPanel.add(%tab);
    new GuiTextCtrl("") {
        profile = "ClipboardTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "55 18";
        minExtent = "l l";
        sluggishness = -1;
        visible = 1;
        text = "Player Name: ";
        maxLength = 255;
    };.add(%userScoresPanel);
    %usernameField = new GuiMLTextCtrl("") {
        profile = "ClipboardTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "70 2";
        extent = "111 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
    };
    %tab.usernameField = %usernameField;
    %usernameField.add(%userScoresPanel);
    new GuiTextCtrl("") {
        profile = "ClipboardTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "200 0";
        extent = "55 18";
        minExtent = "l l";
        sluggishness = -1;
        visible = 1;
        text = "Best Score: ";
        maxLength = 255;
    };.add(%userScoresPanel);
    %bestScoreField = new GuiTextCtrl("") {
        profile = "ClipboardTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "305 0";
        extent = "55 18";
        minExtent = "l l";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 255;
    };
    %tab.bestScoreField = %bestScoreField;
    %bestScoreField.add(%userScoresPanel);
    new GuiTextCtrl("") {
        profile = "ClipboardTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 25";
        extent = "55 18";
        minExtent = "l l";
        sluggishness = -1;
        visible = 1;
        text = "Rank: ";
        maxLength = 255;
    };.add(%userScoresPanel);
    %rankField = new GuiTextCtrl("") {
        profile = "ClipboardTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "70 25";
        extent = "55 18";
        minExtent = "l l";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 255;
    };
    %tab.rankField = %rankField;
    %rankField.add(%userScoresPanel);
    new GuiTextCtrl("") {
        profile = "ClipboardTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "200 25";
        extent = "55 18";
        minExtent = "l l";
        sluggishness = -1;
        visible = 1;
        text = "Date of Best Score: ";
        maxLength = 255;
    };.add(%userScoresPanel);
    %dateField = new GuiTextCtrl("") {
        profile = "ClipboardTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "305 25";
        extent = "55 18";
        minExtent = "l l";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 255;
    };
    %tab.dateField = %dateField;
    %dateField.add(%userScoresPanel);
    %noScoreText = new GuiTextCtrl("") {
        profile = "ClipboardTextProfile";
        horizSizing = "right";
        vertSizing = "top";
        position = "5 380";
        extent = "55 18";
        minExtent = "l l";
        sluggishness = -1;
        visible = 0;
        text = "";
        maxLength = 255;
    };
    %tab.noScoreText = %noScoreText;
    %noScoreText.add(%tab);
};
function geHighScoresGuiTable::doSetupRowGuiArray(%this, %rowArray) {
    Parent::doSetupRowGuiArray(%this, %rowArray);
    %rowArray.DataTable = %this.getDataTable();
    if (!(getWord(%child.getNamespaceList(), 0) $= "geHighScoresGuiTableRow")) {
        "geHighScoresGuiTableRow".bindClassName(%rowArray);
    }
};
function geHighScoresGuiTable::onRowSelected(%this, %unused, %rowIndex, %unused, %unused) {
    %cellIndex = "username".getColumnIndex(%this.getDataTable());
    %userName = %cellIndex.getCellSortValue(%this.getDataTable(), %rowIndex);
    onLeftClickPlayerName(%userName, "");
};
function geHighScoresGuiTableRow::onRightMouseUp(%this) {
    %rowIndex = %this.getObjectIndex(%this.getParent());
    %cellIndex = "username".getColumnIndex(%this.DataTable);
    %userName = %cellIndex.getCellSortValue(%this.DataTable, %rowIndex);
    onRightClickPlayerName(%userName);
};
