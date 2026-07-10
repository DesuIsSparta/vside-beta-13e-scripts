function geTGF_tabs::fillTabHotSpots(%this) {
    %tabName = "hotspots";
    %tab = %tabName.getTabWithName(%this);
    if (%tab.filled) {
        if (isObject(%tab.GuiTable)) {
            1.makeFirstResponder(%tab.GuiTable);
        }
        return;
    }
    %tab.filled = 1;
    %tab.fillTabGeneric(%this);
    %dataTable = new DataTable(geTGF_HotSpotsDataTable);
    0.addColumn(%dataTable, "event", "", "icon", 20, 1, 1);
    1.addColumn(%dataTable, "description", mlStyle("Description", "tgfTables_ColumnHeader"), "string", 290, 1, 1);
    0.addColumn(%dataTable, "poster", "", "image", 50, 0, 1);
    1.addColumn(%dataTable, "username", mlStyle("Host", "tgfTables_ColumnHeader"), "string", 150, 1, 1);
    1.addColumn(%dataTable, "location", mlStyle("Where", "tgfTables_ColumnHeader"), "string", 100, 1, 1);
    0.addColumn(%dataTable, "sameServer", mlStyle("Load Time", "tgfTables_ColumnHeader"), "icon", 100, 1, 1);
    1.addColumn(%dataTable, "population", mlStyle("People", "tgfTables_ColumnHeader"), "number", 70, 1, 1);
    1.addColumn(%dataTable, "friends", mlStyle("Friends", "tgfTables_ColumnHeader"), "number", 70, 1, 1);
    0.addColumn(%dataTable, "access", mlStyle("Access", "tgfTables_ColumnHeader"), "icon", 70, 1, 1);
    "username".setUniqueIdentifierColumns(%dataTable);
    "platform/client/ui/tgf/tgf_calendar_19x17_blue".addIconToColumn(%dataTable, "event", "publicEvent");
    "platform/client/ui/tgf/tgf_calendar_19x17_green".addIconToColumn(%dataTable, "event", "featuredEvent");
    "platform/client/ui/tgf/tgf_calendar_19x17".addIconToColumn(%dataTable, "event", "regularEvent");
    "platform/client/ui/tgf/tgf_house_white".addIconToColumn(%dataTable, "event", "notAnEvent");
    "platform/client/ui/tgf/tgf_tele_lightning_white".addIconToColumn(%dataTable, "sameServer", "true");
    "platform/client/ui/tgf/tgf_tele_subway_white".addIconToColumn(%dataTable, "sameServer", "false");
    "platform/client/ui/tgf/tgf_door_open_white".addIconToColumn(%dataTable, "access", "open");
    "platform/client/ui/tgf/tgf_door_heart_white".addIconToColumn(%dataTable, "access", "friendsonly");
    "platform/client/ui/tgf/tgf_door_key_white".addIconToColumn(%dataTable, "access", "passwordprotected");
    "event".doSort(%dataTable);
    %guiTable = new GuiTableCtrl(geTGF_HotSpotsGuiTable) {
        position = "2 20";
        extent = "955 430";
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
    new GuiTextEditCtrl(geTGF_HotSpotsFilterBox) {
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
        text = mlStyle($MsgCat::invitation["TEXT-TGF-HOTSPOTS"], "tgfTables_Invite");
    };
    %invite.add(%tab);
    %this.refreshTabHotSpots();
};
function geTGF_tabs::onShowTabHotSpots(%this) {
    cancel(geTGF.geTGF_Refresh_Schedule);
    1.setActive(geTGF_Refresh);
    1.setVisible(geTGF_Refresh);
    1.makeFirstResponder(geTGF_HotSpotsGuiTable);
};
function geTGF_tabs::refreshTabHotSpots(%this) {
    1.setVisible(geTGF_HotSpotsGuiTable.alternativeTextCtrl);
    mlStyle("Fetching..", "tgfTables_DataCell_Text").setText(geTGF_HotSpotsGuiTable.alternativeTextCtrl);
    %request = sendRequest_GetHappeningsInProgress($Player::Name, "geTGF_OnGotDoneOrError_GetHappeningsInProgress");
};
function geTGF_OnGotDoneOrError_GetHappeningsInProgress(%request) {
    if ((geTGF_tabs.getCurrentTab().name $= "hotspots")) {
        cancel(geTGF.geTGF_Refresh_Schedule);
        1.setActive(geTGF_Refresh);
    }
    if (!(isObject(%request))) {
        return;
    }
    "happening".clearItemList(geTGF, "hotspots");
    %listBase = "happenings";
    %count = %listBase @ "Count".getValue(%request);
    %n = 0;
    while ((%n < %count)) {
        %listItem = %listBase @ %n;
        %id = %listItem @ ".hostUserName".getValue(%request);
        %item = %id.createNewItem(geTGF, "hotspots", "happening");
        "accessMode".copyListValueIntoObject(%request, %item, %listItem);
        "occupancy".copyListValueIntoObject(%request, %item, %listItem);
        "baseImageURL".copyListValueIntoObject(%request, %item, %listItem);
        "eventId".copyListValueIntoObject(%request, %item, %listItem);
        "featured".copyListValueIntoObject(%request, %item, %listItem);
        "friendOccupancy".copyListValueIntoObject(%request, %item, %listItem);
        "goThereVURL".copyListValueIntoObject(%request, %item, %listItem);
        "headline".copyListValueIntoObject(%request, %item, %listItem);
        "hostUserName".copyListValueIntoObject(%request, %item, %listItem);
        "location.areaName".copyListValueIntoObject(%request, %item, %listItem);
        "location.serverName".copyListValueIntoObject(%request, %item, %listItem);
        "moreInfoURL".copyListValueIntoObject(%request, %item, %listItem);
        "apt".copyListValueIntoObject(%request, %item, %listItem);
        %item.subType = %listItem @ ".type".getValue(%request);
        if (!(1)) {
            %item.occupancy = -(1.0);
            %item.friendOccupancy = -(1.0);
            %item.subType = "publicLocationEvent";
            %item.featured = 1;
            %item.eventID = 1234;
        }
        %item.goThereVURL = vurlClearResolution(%item.goThereVURL);
        %n = (%n + 1.0);
    }
    "The-Manager".removeItemsWithFieldValueFromList(geTGF, "hotspots", "happening", "hostUserName");
    %itemList = "happening".getItemList(geTGF, "hotspots");
    (%n < %count);
    %count = %itemList.count();
    geTGF_HotSpotsDataTable.getRowCount().removeRowsByIndex(geTGF_HotSpotsDataTable, 0);
    %count.addRows(geTGF_HotSpotsDataTable);
    (%count == 0.0).setVisible(geTGF_HotSpotsGuiTable.alternativeTextCtrl);
    mlStyle("More parties and events coming soon!", "tgfTables_DataCell_Text").setText(geTGF_HotSpotsGuiTable.alternativeTextCtrl);
    %n = 0;
    while ((%n < %count)) {
        %item = %n.getValue(%itemList);
        %isFriend = (%item.hostUserName.getFriendStatus(BuddyHudWin) $= "friends");
        if ((%item.baseImageURL $= "")) {
        }
        %imageURL = %item.baseImageURL @ "?size=S";
        "";
        if (!($ServerName $= "")) {
        }
        %sameServer = (%item.location_serverName $= $ServerName) ? "true" : "false";
        if ((%item.eventID $= "")) {
            %eventValue = "notAnEvent";
            %eventFmt = "<modulationColor:ffffff60>";
        }
        if ((%item.subType $= "publicLocationEvent")) {
            %eventValue = "publicEvent";
            %eventFmt = "";
        }
        if (%item.featured) {
            %eventValue = "featuredEvent";
            %eventFmt = "";
        }
        %eventValue = "regularEvent";
        %eventFmt = "";
        %occupancyText = "<color:ffffff60>-".formatOccupancy(geTGF, %item.occupancy, "<b>", "<color:ffffff60>(unknown)");
        %friendOccupancyText = "<color:ffffff60>-".formatOccupancy(geTGF, %item.friendOccupancy, "<b><color:40ff40>", "<color:ffffff60>(unknown)");
        if ((%item.occupancy >= 0.0)) {
        }
        %occupancySortVal = 99999;
        %item.occupancy;
        if ((%item.friendOccupancy >= 0.0)) {
        }
        %friendOccupancySortVal = 99999;
        %item.friendOccupancy;
        %rowData = "poster" @ "\t" @ "" @ "\t" @ %imageURL;
        %rowData = %rowData @ "\n" @ "description" @ "\t" @ %item.headline @ "\t" @ geTGF_tabs::hotSpotsTab_formatDescription(%item.headline);
        %rowData = %rowData @ "\n" @ "username" @ "\t" @ %item.hostUserName @ "\t" @ geTGF_tabs::hotSpotsTab_formatUserName(%item.hostUserName, %isFriend);
        %rowData = %rowData @ "\n" @ "location" @ "\t" @ %item.location_areaName @ "\t" @ geTGF_tabs::hotSpotsTab_formatLocation(%item.location_areaName);
        %rowData = %rowData @ "\n" @ "population" @ "\t" @ %occupancySortVal @ "\t" @ "<just:left>" @ geTGF_tabs::hotSpotsTab_formatDescription(%occupancyText);
        %rowData = %rowData @ "\n" @ "friends" @ "\t" @ %friendOccupancySortVal @ "\t" @ "<just:left>" @ geTGF_tabs::hotSpotsTab_formatDescription(%friendOccupancyText);
        %rowData = %rowData @ "\n" @ "sameServer" @ "\t" @ %sameServer @ "\t" @ "<just:left>[ICON]";
        %rowData = %rowData @ "\n" @ "access" @ "\t" @ %item.accessMode @ "\t" @ "<just:left>[ICON]";
        %rowData = %rowData @ "\n" @ "event" @ "\t" @ %eventValue @ "\t" @ "<just:right>" @ %eventFmt @ "[ICON]";
        %rowData.setRowDataByIndex(geTGF_HotSpotsDataTable, %n);
        %n = (%n + 1.0);
    }
    geTGF_HotSpotsDataTable.doFilter();
    geTGF_HotSpotsDataTable.updateListeners();
};
function geTGF_tabs::hotSpotsTab_formatDescription(%desc) {
    return mlStyle(%desc, "tgfTables_DataCell_Text");
};
function geTGF_tabs::hotSpotsTab_formatUserName(%name, %isFriend) {
    if (%isFriend) {
        return mlStyle(%name, "tgfTables_DataCell_UserName_Friend");
    }
    return mlStyle(%name, "tgfTables_DataCell_UserName_Normal");
};
function geTGF_tabs::hotSpotsTab_formatLocation(%location) {
    %text = "";
    %text = %text @ "<tab:30>";
    %text = %text @ DestinationList::GetAreaNameUserFacingNameCityAndBuildingShort(%location, "\t");
    return mlStyle(%text, "tgfTables_DataCell_Text");
};
function geTGF_tabs::hotSpotsTab_formatLocation2(%location) {
    %text = "";
    %text = %text @ "<tab:30>";
    %text = %text @ DestinationList::GetAreaNameUserFacingNameCityAndBuilding(%location, "\t- ");
    return mlStyle(%text, "tgfTables_DataCell_Text");
};
function geTGF_tabs::hotSpotsTab_formatAccess(%access, %isFriend) {
    if ((%access $= "OPEN")) {
        return "open";
    }
    if ((%access $= "FRIENDSONLY")) {
    }
    if (%isFriend) {
        return "friendsOnlyOfFriend";
    }
    if ((%access $= "FRIENDSONLY")) {
        return "friendsOnlyOfNonFriend";
    }
    if ((%access $= "PASSWORDPROTECTED")) {
    }
    if (%isFriend) {
        return "doorcodeOfFriend";
    }
    if ((%access $= "PASSWORDPROTECTED")) {
        return "doorcodeOfNonFriend";
    }
    return "";
};
function geTGF::hotspots_GetAndOpenDetailsContainer(%this, %item) {
    %dataRowIndex = "username" @ "\t" @ %item.hostUserName.getRowIndexByCriteria(geTGF_HotSpotsDataTable);
    %guiRowIndex = %dataRowIndex.getGuiRowIndexForDataRowIndex(geTGF_HotSpotsGuiTable);
    if ((%guiRowIndex >= 0.0)) {
        %guiRowIndex.doHiliteRow(geTGF_HotSpotsGuiTable);
    }
    %item.constructDeetsWindow(%this, geDeetsWindow);
    1.setVisible(geDeetsLayer);
    return geDeetsWindow;
};
function geTGF_HotSpotsGuiTable::onRowSelected(%this, %guiRow, %rowIndex, %unused, %mouseClickCount) {
    if ((%rowIndex == -(1.0))) {
        error(getScopeName() @ " " @ "- Gui Row" @ " " @ %guiRow @ " " @ "has no Data Row -" @ " " @ getTrace());
        return;
    }
    1.makeFirstResponder(%this);
    %cellIndex = "username".getColumnIndex(geTGF_HotSpotsDataTable);
    %userName = %cellIndex.getCellSortValue(geTGF_HotSpotsDataTable, %rowIndex);
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
        %item = %userName.findItem(geTGF, "hotspots", "happening");
        %item.DoDetails(geTGF, "hotspots");
    }
};
function geTGF_HotSpotsGuiTable::onKeyDown(%this, %modifier, %keyCode) {
    %modifierStr = %modifier.getStringFromModifier(%this);
    %keyCodeStr = %keyCode.getStringFromKeyCode(%this);
    if ((%modifierStr @ %keyCodeStr $= "\t")) {
    }
    if ((%modifierStr @ %keyCodeStr $= "ctrl F")) {
        1.makeFirstResponder(geTGF_HotSpotsFilterBox);
        return 1;
    }
    return 0;
};
function geTGF_HotSpotsFilterBox::onKeyUp(%this, %modifier, %keyCode) {
    cancel(%this.filterDoItReallySchedule);
    %this.filterDoItReallySchedule = "doApplyFilterReally".schedule(%this, %this.filterDoItReallyTimeoutMS);
    return 0;
};
function geTGF_HotSpotsFilterBox::onKeyDown(%this, %modifier, %keyCode) {
    %keyCodeStr = %keyCode.getStringFromKeyCode(%this);
    if ((%keyCodeStr $= "\t")) {
        1.makeFirstResponder(geTGF_HotSpotsGuiTable);
        return 1;
    }
    return 0;
};
function geTGF_HotSpotsFilterBox::doApplyFilterReally(%this) {
    cancel(%this.filterDoItReallySchedule);
    %this.filterDoItReallySchedule = "";
    %this.getText().setFilterText(geTGF_HotSpotsDataTable);
    geTGF_HotSpotsDataTable.updateListeners();
};
function geTGF::openForBuildingDirectory(%this, %buildingName) {
    %buildingName = trim(%buildingName);
    if ((%buildingName $= "")) {
        return;
    }
    %areaName = Buildings::GetAreaName(%buildingName);
    %fitlerText = DestinationList::GetAreaNameUserFacingNameCityAndBuildingShort(%areaName, "");
    %updateListeners = isObject(geTGF_HotSpotsDataTable);
    "hotspots".openToTabName(geTGF);
    %fitlerText.setText(geTGF_HotSpotsFilterBox);
    %fitlerText.setFilterText(geTGF_HotSpotsDataTable);
    if (%updateListeners) {
        geTGF_HotSpotsDataTable.doFilter();
        geTGF_HotSpotsDataTable.updateListeners();
    }
};
function onDoneOrErrorCallback_GetUserRelations_ForHotSpots(%request) {
    onDoneOrErrorCallback_GetUserRelations_ProcessOnly(%request);
    if (!(isObject(geTGF_HotSpotsDataTable))) {
        return;
    }
    %itemList = "happening".getItemList(geTGF, "hotspots");
    %count = %itemList.count();
    %n = 0;
    while ((%n < %count)) {
        %item = %n.getValue(%itemList);
        %isFriend = (%item.hostUserName.getFriendStatus(BuddyHudWin) $= "friends");
        if (%isFriend) {
            %oldRowData = "username" @ "\t" @ %item.hostUserName;
            %newRowData = "username" @ "\t" @ %item.hostUserName @ "\t" @ geTGF_tabs::hotSpotsTab_formatUserName(%item.hostUserName, %isFriend);
            %newRowData.setRowDataByCriteria(geTGF_HotSpotsDataTable, %oldRowData);
        }
        %n = (%n + 1.0);
    }
    geTGF_HotSpotsDataTable.updateListeners();
};
