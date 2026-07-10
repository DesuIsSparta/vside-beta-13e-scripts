if (!isObject(geHighScoresPanelTabs))
{
    new ScriptObject(geHighScoresPanelTabs) {
        class = "TabControl";
    };
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(geHighScoresPanelTabs);
    }
}
function toggleHighScoresPanel()
{
    toggleVisibleState(geHighScoresPanel);
}
safeEnsureScriptObject("StringMap", "HumanReadableGameNamesMap");
HumanReadableGameNamesMap.put("TheGrind", "The Grind");
HumanReadableGameNamesMap.put("Materiel", "Materiel");
HumanReadableGameNamesMap.put("Sumo", "Sumo");
function geHighScoresPanel::open(%this, %gameName, %gameStationId)
{
    %this.Initialize();
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    geHighScoresPanelTabs.selectCurrentTab();
    %this.gameName = %gameName;
    %this.gameStationId = %gameStationId;
    %humanReadableGameName = HumanReadableGameNamesMap.get(%this.gameName);
    %colon = (%humanReadableGameName $= "") ? "" : ": ";
    geHighScoresTitleText.setText("High Scores" @ %colon @ %humanReadableGameName);
    %request = sendRequest_GetHighGameScores($Player::Name, %this.gameName, 0, 25, "onDoneOrErrorCallback_GetHighGameScores");
    %request.global = 1;
    %this.requestStarted();
    %request = sendRequest_GetHighGameScoresForStation($Player::Name, %this.gameStationId, 0, 25, "onDoneOrErrorCallback_GetHighGameScores");
    %request.global = 0;
    %this.requestStarted();
}
function clientCmdOpenHighScoresFor(%gameName, %gameStationId)
{
    geHighScoresPanel.open(%gameName, %gameStationId);
}
function geHighScoresPanel::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
}
function geHighScoresPanel::Initialize(%this)
{
    geHighScoresPanelTabs.setup();
    if (!isObject(%this.waitIcon))
    {
        %this.requestsPending = 0;
        %this.waitIcon = AnimCtrl::newAnimCtrl("457 24", "18 18");
        %this.waitIcon.setDelay(60);
        %this.waitIcon.addFrame("platform/client/ui/wait0.png");
        %this.waitIcon.addFrame("platform/client/ui/wait1.png");
        %this.waitIcon.addFrame("platform/client/ui/wait2.png");
        %this.waitIcon.addFrame("platform/client/ui/wait3.png");
        %this.waitIcon.addFrame("platform/client/ui/wait4.png");
        %this.waitIcon.addFrame("platform/client/ui/wait5.png");
        %this.waitIcon.addFrame("platform/client/ui/wait6.png");
        %this.waitIcon.addFrame("platform/client/ui/wait7.png");
        %this.waitIcon.setVisible(0);
        %this.add(%this.waitIcon);
    }
}
function geHighScoresPanel::requestStarted(%this)
{
    %this.requestsPending = %this.requestsPending + 1;
    %this.waitIcon.setVisible(1);
    %this.waitIcon.start();
}
function geHighScoresPanel::requestStopped(%this)
{
    %this.requestsPending = %this.requestsPending - 1;
    if (%this.requestsPending <= 0)
    {
        %this.requestsPending = 0;
        %this.waitIcon.stop();
        %this.waitIcon.setVisible(0);
    }
}
function onDoneOrErrorCallback_GetHighGameScores(%request)
{
    geHighScoresPanel.requestStopped();
    if (%request.checkSuccess())
    {
        %global = %request.global;
        %tabName = %global ? "Global" : "This Machine";
        %tab = geHighScoresPanelTabs.getTabWithName(%tabName);
        if (!isObject(%tab))
        {
            error(getTrace() @ " " @ "tab with name" @ " " @ %tabName @ " " @ "not found!");
            return;
        }
        %dataTable = %tab.DataTable;
        %dataTable.removeRowsByIndex(0, %dataTable.getRowCount());
        %count = %request.getValue("scores.scoresCount");
        %dataTable.addRows(%count);
        %userRanking = "";
        %userScore = "";
        %userScoreDate = "";
        %i = 0;
        while (%i < %count)
        {
            %prefix = "scores.scores" @ %i;
            %score = %request.getValue(%prefix @ ".score");
            %scoreRanking = %request.getValue(%prefix @ ".scoreRanking");
            %userName = %request.getValue(%prefix @ ".userName");
            %dateAttained = %request.getValue(%prefix @ ".dateAttained");
            if (%userName $= $Player::Name)
            {
            }
            if (%userRanking $= "")
            {
                %userRanking = %scoreRanking;
                %userScore = %score;
                %userScoreDate = %dateAttained;
            }
            %style = (BuddyHudWin.getFriendStatus(%userName) $= "friends") ? "UserName_Friend" : "UserName_Normal";
            %rowData = "rank" @ "\t" @ %scoreRanking @ "\t" @ %scoreRanking @ "\n" @ "avatar" @ "\t" @ %userName @ "\t" @ "platform/client/ui/tgf/tgf_profile_default" @ "\n" @ "username" @ "\t" @ %userName @ "\t" @ mlStyle(%userName, %style) @ "\n" @ "date" @ "\t" @ %dateAttained @ "\t" @ %dateAttained @ "\n" @ "score" @ "\t" @ %score @ "\t" @ %score;
            %dataTable.setRowDataByIndex(%i, %rowData);
            %rowData = "avatar" @ "\t" @ %userName @ "\t" @ $Net::AvatarURL @ urlEncode(%userName) @ "?size=S";
            %dataTable.setRowDataByIndex(%i, %rowData);
            %i = %i + 1;
        }
        %dataTable.updateListeners();
        if ((%i < %count) @ " " @ %userRanking $= "")
        {
            %userRanking = %request.getValue("scores.userRanking");
            %userScore = %request.getValue("scores.userScore");
            %userScoreDate = %request.getValue("scores.userScoreDate");
        }
        if (%userRanking $= "")
        {
            if (%global)
            {
                %hrGameName = HumanReadableGameNamesMap.get(geHighScoresPanel.gameName);
                %text = "You have no score for " @ %hrGameName @ ".";
            }
            else
            {
                %text = "You have no score on this machine.";
            }
            %tab.noScoreText.setText(%text);
            %tab.noScoreText.setVisible(1);
            %tab.userScoresPanel.setVisible(0);
        }
        %tab.usernameField.setText("<clip:111>" @ $Player::Name);
        %tab.bestScoreField.setText(%userScore);
        %tab.rankField.setText(%userRanking);
        %tab.dateField.setText(%userScoreDate);
        %tab.noScoreText.setVisible(0);
        %tab.userScoresPanel.setVisible(1);
    }
}
function geHighScoresPanelTabs::createButton(%this, %bitmapName, %tab, %name)
{
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
}
function geHighScoresPanelTabs::setup(%this)
{
    if (!%this.initialized)
    {
        %this.Initialize(geHighScoresPanelTabContainer, "109 25", "", "0 0", "horizontal");
        %this.newTab("This Machine", "platform/client/buttons/clipboard_tab");
        %this.newTab("Global", "platform/client/buttons/clipboard_tab");
        %this.selectTabWithName("This Machine");
        %this.fillTabs();
    }
}
function geHighScoresPanelTabs::fillTabs(%this)
{
    %this.fillTabWithName("This Machine");
    %this.fillTabWithName("Global");
}
function geHighScoresPanelTabs::fillTabWithName(%this, %tabName)
{
    %tab = %this.getTabWithName(%tabName);
    if (!isObject(%tab))
    {
        return;
    }
    %tab.setProfile(ClipboardProfile);
    %dataTable = new DataTable("");
    %tab.DataTable = %dataTable;
    %dataTable.addColumn("rank", "Rank", "number", 30);
    %dataTable.addColumn("avatar", "", "image", 20, 0);
    %dataTable.addColumn("username", "Name", "string", 150);
    %dataTable.addColumn("date", "Date", "string", 180);
    %dataTable.addColumn("score", "High Score", "number", 70);
    %guiTable = new GuiTableCtrl("") {
        position = "5 5";
        extent = "460 372";
        horizSizing = "width";
        vertSizing = "height";
        visible = 1;
        spacing = 2;
    };
    %guiTable.bindClassName("geHighScoresGuiTable");
    %guiTable.setChildrenExtents(17);
    %guiTable.setDataTable(%dataTable);
    %tab.GuiTable = %guiTable;
    %tab.add(%guiTable);
    %guiTable.setHeaderCellProfile(ClipboardHeaderCellProfile);
    %guiTable.setHeaderCellButtonProfile(ClipboardHeaderCellButtonProfile);
    %guiTable.setHeaderCellMLTextProfile(ClipboardHeaderMLTextProfile);
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
    %tab.add(%userScoresPanel);
    %userScoresPanel.add(new GuiTextCtrl("") {
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
    };);
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
    %userScoresPanel.add(%usernameField);
    %userScoresPanel.add(new GuiTextCtrl("") {
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
    };);
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
    %userScoresPanel.add(%bestScoreField);
    %userScoresPanel.add(new GuiTextCtrl("") {
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
    };);
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
    %userScoresPanel.add(%rankField);
    %userScoresPanel.add(new GuiTextCtrl("") {
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
    };);
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
    %userScoresPanel.add(%dateField);
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
    %tab.add(%noScoreText);
}
function geHighScoresGuiTable::doSetupRowGuiArray(%this, %rowArray)
{
    Parent::doSetupRowGuiArray(%this, %rowArray);
    %rowArray.DataTable = %this.getDataTable();
    if (!(getWord(%child.getNamespaceList(), 0) $= "geHighScoresGuiTableRow"))
    {
        %rowArray.bindClassName("geHighScoresGuiTableRow");
    }
}
function geHighScoresGuiTable::onRowSelected(%this, %unused, %rowIndex, %unused, %unused)
{
    %cellIndex = %this.getDataTable().getColumnIndex("username");
    %userName = %this.getDataTable().getCellSortValue(%rowIndex, %cellIndex);
    onLeftClickPlayerName(%userName, "");
}
function geHighScoresGuiTableRow::onRightMouseUp(%this)
{
    %rowIndex = %this.getParent().getObjectIndex(%this);
    %cellIndex = %this.DataTable.getColumnIndex("username");
    %userName = %this.DataTable.getCellSortValue(%rowIndex, %cellIndex);
    onRightClickPlayerName(%userName);
}
