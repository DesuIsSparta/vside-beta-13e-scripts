function geTGF_tabs::fillTabFriends(%this) {
    %tabName = "friends";
    %tab = %this.getTabWithName(%tabName);
    if (%tab.filled) {
        if (isObject(%tab.GuiTable)) {
            %tab.GuiTable.makeFirstResponder(1);
        }
        return;
    }
    %tab.filled = 1;
    %this.fillTabGeneric(%tab);
    %dataTable = new DataTable(geTGF_FriendsDataTable);;
    %dataTable.addColumn("statusmsg", mlStyle("Status", "tgfTables_ColumnHeader"), "string", 290, 1, 1, 1);
    %dataTable.addColumn("avatar", "", "image", 50, 0, 1, 0);
    %dataTable.addColumn("username", mlStyle("Name", "tgfTables_ColumnHeader"), "string", 150, 1, 1, 1);
    %dataTable.addColumn("location", mlStyle("Where", "tgfTables_ColumnHeader"), "string", 100, 1, 1, 1);
    %dataTable.addColumn("sameServer", mlStyle("Load Time", "tgfTables_ColumnHeader"), "icon", 100, 1, 1, 0);
    %dataTable.addColumn("activities", mlStyle("Activities", "tgfTables_ColumnHeader"), "string", 180, 1, 1, 0);
    %dataTable.setUniqueIdentifierColumns("username");
    %dataTable.addIconToColumn("sameServer", "true", "platform/client/ui/tgf/tgf_tele_lightning_white");
    %dataTable.addIconToColumn("sameServer", "false", "platform/client/ui/tgf/tgf_tele_subway_white");
    %dataTable.doSort("username");
    %guiTable = new GuiTableCtrl(geTGF_FriendsGuiTable) {
        position = "24 20";
        extent = "933 430";
        visible = 1;
        spacing = 2;
    };
    %guiTable.setHeaderCellUniformExtent(22);
    %guiTable.setHeaderMLTextBoxTopMargin(0);
    %guiTable.setChildrenExtents(18);
    %guiTable.setDataTable(%dataTable);
    %tab.GuiTable = %guiTable;
    %tab.add(%guiTable);
    0;
    %guiTable.alternativeTextCtrl = new ""() {
        profile = GuiMLTextCtrl @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "75 49";
        extent = "800 40";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %tab.add(%guiTable.alternativeTextCtrl);
    %guiTable.alternativeTextCtrl.setText(mlStyle("Fetching...", "tgfTables_DataCell_Text"));
    0;
    %filterLabel = new ""() {
        profile = GuiMLTextCtrl @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "7 473";
        extent = "30 20";
        text = mlStyle("Find:", "tgfTables_DataCell_Text");
    };
    %tab.add(%filterLabel);
    0;
    %filterBox = new ""() {
        profile = GuiControl @ "ETSLightBoxProfile";
        horizSizing = "right";
        vertSizing = "top";
        position = "37 470";
        extent = "186 22";
        minExtent = "186 22";
        sluggishness = -1;
        visible = 1;
        canHilite = 0;
        allowAutoFirstResponderUpdates = 0;
    };
    %tab.add(%filterBox);
    0;
    %filterBox.add(new ""() {
        profile = GuiBitmapCtrl @ "ETSNonModalProfile";
        bitmap = "platform/client/ui/magnifying_glass";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "3 4";
        extent = "18 17";
    };);
    %filterBox.add(new GuiTextEditCtrl(geTGF_FriendsFilterBox) {
        profile = "InfoWindowTextEditInvisibleProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "20 1";
        extent = "168 20";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 29;
        canHilite = 0;
        filterDoItReallySchedule = "";
        filterDoItReallyTimeoutMS = 400;
    };);
    0;
    %invite = new ""() {
        position = GuiMLTextCtrl @ "250 473";
        extent = "600 30";
        text = mlStyle(, "tgfTables_Invite");
    };
    %tab.add(%invite);
    %this.refreshTabFriends();
};
function geTGF_tabs::onShowTabFriends(%this) {
    cancel(geTGF_Refresh_Schedule);
    1.setActive();
    1.setVisible();
    1.makeFirstResponder();
};
function geTGF_tabs::refreshTabFriends(%this) {
    alternativeTextCtrl.setVisible(1);
    alternativeTextCtrl.setText(mlStyle("Fetching..", "tgfTables_DataCell_Text"));
    sendRequest_GetOnlineFriends("", "", "geTGF_OnGotDoneOrError_GetOnlineFriends");
};
function geTGF_OnGotDoneOrError_GetOnlineFriends(%request) {
    if ((geTGF_tabs.getCurrentTab().name $= "friends")) {
        cancel(geTGF_tabs.getCurrentTab().geTGF_Refresh_Schedule);
        1.setActive();
    }
    if (!(isObject(%request))) {
        return geTGF_Refresh;
    }
    "friends".clearItemList("person");
    %listBase = "friends";
    geTGF;
    %count = %request.getValue(%listBase @ "Count");
    %n = 0;
    if ((%count < %n)) {
        %friendLabel = "friends" @ %n;
        %id = %request.getValue(%friendLabel @ ".userName");
        %item = "friends".createNewItem("person", %id);
        geTGF;
        %item.userName = %request.getValue(%friendLabel @ ".userName");
        %item.currentActivities = %request.getValue(%friendLabel @ ".currentActivities");
        %item.currentLocation_areaName = %request.getValue(%friendLabel @ ".currentLocation.areaName");
        %item.currentLocation_serverName = %request.getValue(%friendLabel @ ".currentLocation.serverName");
        %item.headline = %request.getValue(%friendLabel @ ".headline");
        %item.relationType = %request.getValue(%friendLabel @ ".relationType");
        %item.age = %request.getValue(%friendLabel @ ".age");
        %item.currentLocation_buildingName = %request.getValue(%friendLabel @ ".currentLocation.buildingName");
        %item.gender = %request.getValue(%friendLabel @ ".gender");
        %item.homeLocation_areaName = %request.getValue(%friendLabel @ ".homeLocation.areaName");
        %item.homeLocation_buildingName = %request.getValue(%friendLabel @ ".homeLocation.buildingName");
        %item.homeLocation_serverName = %request.getValue(%friendLabel @ ".homeLocation.serverName");
        %item.ignored = %request.getValue(%friendLabel @ ".ignored");
        %item.onlineStatus = %request.getValue(%friendLabel @ ".onlineStatus");
        %item.profileViewCount = %request.getValue(%friendLabel @ ".profileViewCount");
        %item.profileViewRanking = %request.getValue(%friendLabel @ ".profileViewRanking");
        %item.score = %request.getValue(%friendLabel @ ".score");
        %n = (1.0 + %n);
    }
    %itemList = "friends".getItemList("person");
    geTGF;
    %count = %itemList.count();
    (%count < %n);
    0.removeRowsByIndex(geTGF_FriendsDataTable.getRowCount());
    %count.addRows();
    %item.alternativeTextCtrl.setVisible((0.0 == %count));
    %item.alternativeTextCtrl.setText(mlStyle("You're the first one here.  There are lots of new friends to meet on vSide.", "tgfTables_DataCell_Text"));
    %uaw = getUserActivityMgr();
    geTGF_FriendsGuiTable;
    %n = 0;
    geTGF_FriendsGuiTable;
    if ((%count < %n)) {
        %item = %itemList.getValue(%n);
        geTGF_FriendsDataTable;
        %isFriend = (geTGF_FriendsDataTable @ " " @ %item.relationType $= "friend");
        if (!($ServerName $= "")) {
        }
        %sameServer = (%item.currentLocation_serverName $= $ServerName) ? "true" : "false";
        %activities = %uaw.getActivitiesMLText(%item.currentActivities, 3);
        %rowData = "avatar" @ "\t" @ "" @ "\t" @ "platform/client/ui/tgf/tgf_profile_default";
        %rowData = %rowData @ "\n" @ "username" @ "\t" @ %item.userName @ "\t" @ geTGF_tabs::friendsTab_formatUserName(%item.userName, %isFriend);
        %rowData = %rowData @ "\n" @ "location" @ "\t" @ %item.currentLocation_areaName @ "\t" @ geTGF_tabs::friendsTab_formatLocation(%item.currentLocation_areaName);
        %rowData = %rowData @ "\n" @ "sameServer" @ "\t" @ %sameServer @ "\t" @ "[ICON]";
        %rowData = %rowData @ "\n" @ "activities" @ "\t" @ %activities @ "\t" @ %activities;
        %rowData = %rowData @ "\n" @ "statusmsg" @ "\t" @ %item.headline @ "\t" @ geTGF_tabs::friendsTab_formatStatusMsg(%item.headline);
        %n.setRowDataByIndex(%rowData);
        %n = (1.0 + %n);
        geTGF_FriendsDataTable;
    }
    geTGF_FriendsDataTable.updateListeners();
    %n = 0;
    (%count < %n);
    if ((%count < %n)) {
        %item = %itemList.getValue(%n);
        %avatar = $Net::AvatarURL @ urlEncode(%item.userName) @ "?size=S";
        %rowData = "avatar" @ "\t" @ "" @ "\t" @ %avatar;
        %n.setRowDataByIndex(%rowData);
        %n = (1.0 + %n);
        geTGF_FriendsDataTable;
    }
    geTGF_FriendsDataTable.doFilter();
    geTGF_FriendsDataTable.updateListeners();
};
function geTGF_tabs::friendsTab_formatUserName(%name, %isFriend) {
    return geTGF_tabs::hotSpotsTab_formatUserName(%name, %isFriend);
};
function geTGF_tabs::friendsTab_formatLocation(%location) {
    return geTGF_tabs::hotSpotsTab_formatLocation(%location);
};
function geTGF_tabs::friendsTab_formatStatusMsg(%msg) {
    return geTGF_tabs::hotSpotsTab_formatDescription(%msg);
};
function geTGF::friends_GetAndOpenDetailsContainer(%this, %item) {
    %dataRowIndex = "username" @ "\t" @ %item.userName.getRowIndexByCriteria();
    geTGF_FriendsDataTable;
    %guiRowIndex = %dataRowIndex.getGuiRowIndexForDataRowIndex();
    geTGF_FriendsGuiTable;
    if ((0.0 >= %guiRowIndex)) {
        %guiRowIndex.doHiliteRow();
    }
    %this.constructDeetsWindow(%item);
    1.setVisible();
};
function geTGF_FriendsGuiTable::onRowSelected(%this, %guiRow, %rowIndex, %unused, %mouseClickCount) {
    if ((-(1.0) == %rowIndex)) {
        error(getScopeName() @ " " @ "- Gui Row" @ " " @ %guiRow @ " " @ "has no Data Row -" @ " " @ getTrace());
        return;
    }
    %this.makeFirstResponder(1);
    %cellIndex = "username".getColumnIndex();
    geTGF_FriendsDataTable;
    %userName = %rowIndex.getCellSortValue(%cellIndex);
    geTGF_FriendsDataTable;
    %showDeets = 0;
    if ((-(1.0) == %mouseClickCount)) {
        %showDeets = 0;
    }
    if ((0.0 == %mouseClickCount)) {
        %showDeets = 1;
    }
    if ((1.0 == %mouseClickCount)) {
        %showDeets = 1;
    }
    if ((2.0 == %mouseClickCount)) {
        %showDeets = 1;
    }
    %showDeets = 0;
    if (%showDeets) {
        %item = "friends".findItem("person", %userName);
        geTGF;
        "friends".DoDetails(%item);
    }
};
function geTGF_FriendsGuiTable::onKeyDown(%this, %modifier, %keyCode) {
    %modifierStr = %this.getStringFromModifier(%modifier);
    %keyCodeStr = %this.getStringFromKeyCode(%keyCode);
    if ((%modifierStr @ %keyCodeStr $= "\t")) {
    }
    if ((%modifierStr @ %keyCodeStr $= "ctrl F")) {
        1.makeFirstResponder();
        return 1;
    }
    return 0;
};
function geTGF_FriendsFilterBox::onKeyUp(%this, %modifier, %keyCode) {
    cancel(%this.filterDoItReallySchedule);
    %this.filterDoItReallySchedule = %this.schedule(%this.filterDoItReallyTimeoutMS, "doApplyFilterReally");
    return 0;
};
function geTGF_FriendsFilterBox::onKeyDown(%this, %modifier, %keyCode) {
    %keyCodeStr = %this.getStringFromKeyCode(%keyCode);
    if ((%keyCodeStr $= "\t")) {
        1.makeFirstResponder();
        return 1;
    }
    return 0;
};
function geTGF_FriendsFilterBox::doApplyFilterReally(%this) {
    cancel(%this.filterDoItReallySchedule);
    %this.filterDoItReallySchedule = "";
    %this.getText().setFilterText();
    geTGF_FriendsDataTable.updateListeners();
};
