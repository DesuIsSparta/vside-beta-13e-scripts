function geTGF_tabs::fillTabFriends(%this) {
    %tabName = "friends";
    %tab = %tabName.getTabWithName(%this);
    if (%tab.filled) {
        if (isObject(%tab.GuiTable)) {
            1.makeFirstResponder(%tab.GuiTable);
        }
        return;
    }
    %tab.filled = 1;
    %tab.fillTabGeneric(%this);
    %dataTable = new DataTable(geTGF_FriendsDataTable);
    1.addColumn(%dataTable, "statusmsg", mlStyle("Status", "tgfTables_ColumnHeader"), "string", 290, 1, 1);
    0.addColumn(%dataTable, "avatar", "", "image", 50, 0, 1);
    1.addColumn(%dataTable, "username", mlStyle("Name", "tgfTables_ColumnHeader"), "string", 150, 1, 1);
    1.addColumn(%dataTable, "location", mlStyle("Where", "tgfTables_ColumnHeader"), "string", 100, 1, 1);
    0.addColumn(%dataTable, "sameServer", mlStyle("Load Time", "tgfTables_ColumnHeader"), "icon", 100, 1, 1);
    0.addColumn(%dataTable, "activities", mlStyle("Activities", "tgfTables_ColumnHeader"), "string", 180, 1, 1);
    "username".setUniqueIdentifierColumns(%dataTable);
    "platform/client/ui/tgf/tgf_tele_lightning_white".addIconToColumn(%dataTable, "sameServer", "true");
    "platform/client/ui/tgf/tgf_tele_subway_white".addIconToColumn(%dataTable, "sameServer", "false");
    "username".doSort(%dataTable);
    %guiTable = new GuiTableCtrl(geTGF_FriendsGuiTable) {
        position = "24 20";
        extent = "933 430";
        visible = 1;
        spacing = 2;
    };
    22.setHeaderCellUniformExtent(%guiTable);
    0.setHeaderMLTextBoxTopMargin(%guiTable);
    18.setChildrenExtents(%guiTable);
    %dataTable.setDataTable(%guiTable);
    %tab.GuiTable = %guiTable;
    %guiTable.add(%tab);
    %guiTable.alternativeTextCtrl = new GuiMLTextCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "75 49";
        extent = "800 40";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %guiTable.alternativeTextCtrl.add(%tab);
    mlStyle("Fetching...", "tgfTables_DataCell_Text").setText(%guiTable.alternativeTextCtrl);
    %filterLabel = new GuiMLTextCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "7 473";
        extent = "30 20";
        text = mlStyle("Find:", "tgfTables_DataCell_Text");
    };
    %filterLabel.add(%tab);
    %filterBox = new GuiControl("") {
        profile = "ETSLightBoxProfile";
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
    %filterBox.add(%tab);
    new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        bitmap = "platform/client/ui/magnifying_glass";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "3 4";
        extent = "18 17";
    };.add(%filterBox);
    new GuiTextEditCtrl(geTGF_FriendsFilterBox) {
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
    };.add(%filterBox);
    %invite = new GuiMLTextCtrl("") {
        position = "250 473";
        extent = "600 30";
        text = mlStyle($MsgCat::invitation["TEXT-TGF-FRIENDS"], "tgfTables_Invite");
    };
    %invite.add(%tab);
    %this.refreshTabFriends();
};
function geTGF_tabs::onShowTabFriends(%this) {
    cancel(geTGF.geTGF_Refresh_Schedule);
    1.setActive(geTGF_Refresh);
    1.setVisible(geTGF_Refresh);
    1.makeFirstResponder(geTGF_FriendsGuiTable);
};
function geTGF_tabs::refreshTabFriends(%this) {
    1.setVisible(geTGF_FriendsGuiTable.alternativeTextCtrl);
    mlStyle("Fetching..", "tgfTables_DataCell_Text").setText(geTGF_FriendsGuiTable.alternativeTextCtrl);
    sendRequest_GetOnlineFriends("", "", "geTGF_OnGotDoneOrError_GetOnlineFriends");
};
function geTGF_OnGotDoneOrError_GetOnlineFriends(%request) {
    if ((geTGF_tabs.getCurrentTab().name $= "friends")) {
        cancel(geTGF.geTGF_Refresh_Schedule);
        1.setActive(geTGF_Refresh);
    }
    if (!(isObject(%request))) {
        return;
    }
    "person".clearItemList(geTGF, "friends");
    %listBase = "friends";
    %count = %listBase @ "Count".getValue(%request);
    %n = 0;
    while ((%n < %count)) {
        %friendLabel = "friends" @ %n;
        %id = %friendLabel @ ".userName".getValue(%request);
        %item = %id.createNewItem(geTGF, "friends", "person");
        %item.userName = %friendLabel @ ".userName".getValue(%request);
        %item.currentActivities = %friendLabel @ ".currentActivities".getValue(%request);
        %item.currentLocation_areaName = %friendLabel @ ".currentLocation.areaName".getValue(%request);
        %item.currentLocation_serverName = %friendLabel @ ".currentLocation.serverName".getValue(%request);
        %item.headline = %friendLabel @ ".headline".getValue(%request);
        %item.relationType = %friendLabel @ ".relationType".getValue(%request);
        %item.age = %friendLabel @ ".age".getValue(%request);
        %item.currentLocation_buildingName = %friendLabel @ ".currentLocation.buildingName".getValue(%request);
        %item.gender = %friendLabel @ ".gender".getValue(%request);
        %item.homeLocation_areaName = %friendLabel @ ".homeLocation.areaName".getValue(%request);
        %item.homeLocation_buildingName = %friendLabel @ ".homeLocation.buildingName".getValue(%request);
        %item.homeLocation_serverName = %friendLabel @ ".homeLocation.serverName".getValue(%request);
        %item.ignored = %friendLabel @ ".ignored".getValue(%request);
        %item.onlineStatus = %friendLabel @ ".onlineStatus".getValue(%request);
        %item.profileViewCount = %friendLabel @ ".profileViewCount".getValue(%request);
        %item.profileViewRanking = %friendLabel @ ".profileViewRanking".getValue(%request);
        %item.score = %friendLabel @ ".score".getValue(%request);
        %n = (%n + 1.0);
    }
    %itemList = "person".getItemList(geTGF, "friends");
    (%n < %count);
    %count = %itemList.count();
    geTGF_FriendsDataTable.getRowCount().removeRowsByIndex(geTGF_FriendsDataTable, 0);
    %count.addRows(geTGF_FriendsDataTable);
    (%count == 0.0).setVisible(geTGF_FriendsGuiTable.alternativeTextCtrl);
    mlStyle("You're the first one here.  There are lots of new friends to meet on vSide.", "tgfTables_DataCell_Text").setText(geTGF_FriendsGuiTable.alternativeTextCtrl);
    %uaw = getUserActivityMgr();
    %n = 0;
    while ((%n < %count)) {
        %item = %n.getValue(%itemList);
        %isFriend = (%item.relationType $= "friend");
        if (!($ServerName $= "")) {
        }
        %sameServer = (%item.currentLocation_serverName $= $ServerName) ? "true" : "false";
        %activities = 3.getActivitiesMLText(%uaw, %item.currentActivities);
        %rowData = "avatar" @ "\t" @ "" @ "\t" @ "platform/client/ui/tgf/tgf_profile_default";
        %rowData = %rowData @ "\n" @ "username" @ "\t" @ %item.userName @ "\t" @ geTGF_tabs::friendsTab_formatUserName(%item.userName, %isFriend);
        %rowData = %rowData @ "\n" @ "location" @ "\t" @ %item.currentLocation_areaName @ "\t" @ geTGF_tabs::friendsTab_formatLocation(%item.currentLocation_areaName);
        %rowData = %rowData @ "\n" @ "sameServer" @ "\t" @ %sameServer @ "\t" @ "[ICON]";
        %rowData = %rowData @ "\n" @ "activities" @ "\t" @ %activities @ "\t" @ %activities;
        %rowData = %rowData @ "\n" @ "statusmsg" @ "\t" @ %item.headline @ "\t" @ geTGF_tabs::friendsTab_formatStatusMsg(%item.headline);
        %rowData.setRowDataByIndex(geTGF_FriendsDataTable, %n);
        %n = (%n + 1.0);
    }
    geTGF_FriendsDataTable.updateListeners();
    %n = 0;
    (%n < %count);
    while ((%n < %count)) {
        %item = %n.getValue(%itemList);
        %avatar = $Net::AvatarURL @ urlEncode(%item.userName) @ "?size=S";
        %rowData = "avatar" @ "\t" @ "" @ "\t" @ %avatar;
        %rowData.setRowDataByIndex(geTGF_FriendsDataTable, %n);
        %n = (%n + 1.0);
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
    %dataRowIndex = "username" @ "\t" @ %item.userName.getRowIndexByCriteria(geTGF_FriendsDataTable);
    %guiRowIndex = %dataRowIndex.getGuiRowIndexForDataRowIndex(geTGF_FriendsGuiTable);
    if ((%guiRowIndex >= 0.0)) {
        %guiRowIndex.doHiliteRow(geTGF_FriendsGuiTable);
    }
    %item.constructDeetsWindow(%this, geDeetsWindow);
    1.setVisible(geDeetsLayer);
    return geDeetsWindow;
};
function geTGF_FriendsGuiTable::onRowSelected(%this, %guiRow, %rowIndex, %unused, %mouseClickCount) {
    if ((%rowIndex == -(1.0))) {
        error(getScopeName() @ " " @ "- Gui Row" @ " " @ %guiRow @ " " @ "has no Data Row -" @ " " @ getTrace());
        return;
    }
    1.makeFirstResponder(%this);
    %cellIndex = "username".getColumnIndex(geTGF_FriendsDataTable);
    %userName = %cellIndex.getCellSortValue(geTGF_FriendsDataTable, %rowIndex);
    %showDeets = 0;
    if ((%mouseClickCount == -(1.0))) {
        %showDeets = 0;
    }
    if ((%mouseClickCount == 0.0)) {
        %showDeets = 1;
    }
    if ((%mouseClickCount == 1.0)) {
        %showDeets = 1;
    }
    if ((%mouseClickCount == 2.0)) {
        %showDeets = 1;
    }
    %showDeets = 0;
    if (%showDeets) {
        %item = %userName.findItem(geTGF, "friends", "person");
        %item.DoDetails(geTGF, "friends");
    }
};
function geTGF_FriendsGuiTable::onKeyDown(%this, %modifier, %keyCode) {
    %modifierStr = %modifier.getStringFromModifier(%this);
    %keyCodeStr = %keyCode.getStringFromKeyCode(%this);
    if ((%modifierStr @ %keyCodeStr $= "\t")) {
    }
    if ((%modifierStr @ %keyCodeStr $= "ctrl F")) {
        1.makeFirstResponder(geTGF_FriendsFilterBox);
        return 1;
    }
    return 0;
};
function geTGF_FriendsFilterBox::onKeyUp(%this, %modifier, %keyCode) {
    cancel(%this.filterDoItReallySchedule);
    %this.filterDoItReallySchedule = "doApplyFilterReally".schedule(%this, %this.filterDoItReallyTimeoutMS);
    return 0;
};
function geTGF_FriendsFilterBox::onKeyDown(%this, %modifier, %keyCode) {
    %keyCodeStr = %keyCode.getStringFromKeyCode(%this);
    if ((%keyCodeStr $= "\t")) {
        1.makeFirstResponder(geTGF_FriendsGuiTable);
        return 1;
    }
    return 0;
};
function geTGF_FriendsFilterBox::doApplyFilterReally(%this) {
    cancel(%this.filterDoItReallySchedule);
    %this.filterDoItReallySchedule = "";
    %this.getText().setFilterText(geTGF_FriendsDataTable);
    geTGF_FriendsDataTable.updateListeners();
};
