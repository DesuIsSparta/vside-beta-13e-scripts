function geTGF_tabs::fillTabHotSpots(%this) {
    %tabName = "hotspots";
    %tab = %this.getTabWithName(%tabName);
    if (filled) {
        if (isObject(GuiTable)) {
            GuiTable.makeFirstResponder(1);
        }
        return %tab;
    }
    filled = 1 @ %tab;
    %this.fillTabGeneric(%tab);
    %dataTable = new DataTable(geTGF_HotSpotsDataTable);
    %dataTable.addColumn("event", "", "icon", 20, 1, 1, 0);
    %dataTable.addColumn("description", mlStyle("Description", "tgfTables_ColumnHeader"), "string", 290, 1, 1, 1);
    %dataTable.addColumn("poster", "", "image", 50, 0, 1, 0);
    %dataTable.addColumn("username", mlStyle("Host", "tgfTables_ColumnHeader"), "string", 150, 1, 1, 1);
    %dataTable.addColumn("location", mlStyle("Where", "tgfTables_ColumnHeader"), "string", 100, 1, 1, 1);
    %dataTable.addColumn("sameServer", mlStyle("Load Time", "tgfTables_ColumnHeader"), "icon", 100, 1, 1, 0);
    %dataTable.addColumn("population", mlStyle("People", "tgfTables_ColumnHeader"), "number", 70, 1, 1, 1);
    %dataTable.addColumn("friends", mlStyle("Friends", "tgfTables_ColumnHeader"), "number", 70, 1, 1, 1);
    %dataTable.addColumn("access", mlStyle("Access", "tgfTables_ColumnHeader"), "icon", 70, 1, 1, 0);
    %dataTable.setUniqueIdentifierColumns("username");
    %dataTable.addIconToColumn("event", "publicEvent", "platform/client/ui/tgf/tgf_calendar_19x17_blue");
    %dataTable.addIconToColumn("event", "featuredEvent", "platform/client/ui/tgf/tgf_calendar_19x17_green");
    %dataTable.addIconToColumn("event", "regularEvent", "platform/client/ui/tgf/tgf_calendar_19x17");
    %dataTable.addIconToColumn("event", "notAnEvent", "platform/client/ui/tgf/tgf_house_white");
    %dataTable.addIconToColumn("sameServer", "true", "platform/client/ui/tgf/tgf_tele_lightning_white");
    %dataTable.addIconToColumn("sameServer", "false", "platform/client/ui/tgf/tgf_tele_subway_white");
    %dataTable.addIconToColumn("access", "open", "platform/client/ui/tgf/tgf_door_open_white");
    %dataTable.addIconToColumn("access", "friendsonly", "platform/client/ui/tgf/tgf_door_heart_white");
    %dataTable.addIconToColumn("access", "passwordprotected", "platform/client/ui/tgf/tgf_door_key_white");
    %dataTable.doSort("event");
    position = new GuiTableCtrl(geTGF_HotSpotsGuiTable) @ "2 20";
    extent = "955 430";
    visible = 1;
    spacing = 2;
    %guiTable = ;
    %guiTable.setHeaderCellUniformExtent(22);
    %guiTable.setHeaderMLTextBoxTopMargin(0);
    %guiTable.setChildrenExtents(18);
    %guiTable.setDataTable(%dataTable);
    GuiTable = %guiTable @ %tab;
    %tab.add(%guiTable);
    profile = GuiMLTextCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "75 49";
    extent = "800 40";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    alternativeTextCtrl = %guiTable;
    %tab.add(alternativeTextCtrl);
    alternativeTextCtrl.setText(mlStyle("Fetching...", "tgfTables_DataCell_Text"));
    profile = GuiMLTextCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = %guiTable @ %guiTable @ "right";
    vertSizing = "bottom";
    position = "7 473";
    extent = "30 20";
    text = mlStyle("Find:", "tgfTables_DataCell_Text");
    %filterLabel = ;
    %tab.add(%filterLabel);
    profile = GuiControl @ new ""() @ "ETSLightBoxProfile";
    0;
    horizSizing = "right";
    vertSizing = "top";
    position = "37 470";
    extent = "186 22";
    minExtent = "186 22";
    sluggishness = -1;
    visible = 1;
    canHilite = 0;
    allowAutoFirstResponderUpdates = 0;
    %filterBox = ;
    %tab.add(%filterBox);
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    bitmap = "platform/client/ui/magnifying_glass";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "3 4";
    extent = "18 17";
    %filterBox.add();
    profile = new GuiTextEditCtrl(geTGF_HotSpotsFilterBox) @ "InfoWindowTextEditInvisibleProfile";
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
    %filterBox.add();
    position = GuiMLTextCtrl @ new ""() @ "250 473";
    0;
    extent = "600 30";
    text = mlStyle(, "tgfTables_Invite");
    %invite = ;
    %tab.add(%invite);
    %this.refreshTabHotSpots();
};
function geTGF_tabs::onShowTabHotSpots(%this) {
    cancel(geTGF_Refresh_Schedule);
    1.setActive();
    1.setVisible();
    1.makeFirstResponder();
};
function geTGF_tabs::refreshTabHotSpots(%this) {
    alternativeTextCtrl.setVisible(1);
    alternativeTextCtrl.setText(mlStyle("Fetching..", "tgfTables_DataCell_Text"));
    %request = sendRequest_GetHappeningsInProgress($Player::Name, "geTGF_OnGotDoneOrError_GetHappeningsInProgress");
    geTGF_HotSpotsGuiTable;
};
function geTGF_OnGotDoneOrError_GetHappeningsInProgress(%request) {
    if ((getCurrentTab() SPC name $= "hotspots")) {
        cancel(geTGF_Refresh_Schedule);
        1.setActive();
    }
    if (!(isObject(%request))) {
        return geTGF_Refresh;
    }
    "hotspots".clearItemList("happening");
    %listBase = "happenings";
    geTGF;
    %count = %request.getValue(%listBase @ "Count");
    %n = 0;
    if ((%count < %n)) {
        %listItem = %listBase @ %n;
        %id = %request.getValue(%listItem @ ".hostUserName");
        %item = "hotspots".createNewItem("happening", %id);
        geTGF;
        %request.copyListValueIntoObject(%item, %listItem, "accessMode");
        %request.copyListValueIntoObject(%item, %listItem, "occupancy");
        %request.copyListValueIntoObject(%item, %listItem, "baseImageURL");
        %request.copyListValueIntoObject(%item, %listItem, "eventId");
        %request.copyListValueIntoObject(%item, %listItem, "featured");
        %request.copyListValueIntoObject(%item, %listItem, "friendOccupancy");
        %request.copyListValueIntoObject(%item, %listItem, "goThereVURL");
        %request.copyListValueIntoObject(%item, %listItem, "headline");
        %request.copyListValueIntoObject(%item, %listItem, "hostUserName");
        %request.copyListValueIntoObject(%item, %listItem, "location.areaName");
        %request.copyListValueIntoObject(%item, %listItem, "location.serverName");
        %request.copyListValueIntoObject(%item, %listItem, "moreInfoURL");
        %request.copyListValueIntoObject(%item, %listItem, "apt");
        subType = %request.getValue(%listItem @ ".type") @ %item;
        if (!(1)) {
            occupancy = -(1.0) @ %item;
            friendOccupancy = -(1.0) @ %item;
            subType = "publicLocationEvent" @ %item;
            featured = 1 @ %item;
            eventID = 1234 @ %item;
        }
        goThereVURL = %item @ vurlClearResolution(goThereVURL) @ %item;
        %n = (1.0 + %n);
    }
    "hotspots".removeItemsWithFieldValueFromList("happening", "hostUserName", "The-Manager");
    %itemList = "hotspots".getItemList("happening");
    geTGF;
    %count = %itemList.count();
    geTGF;
    0.removeRowsByIndex(getRowCount());
    %count.addRows();
    alternativeTextCtrl.setVisible((0.0 == %count));
    alternativeTextCtrl.setText(mlStyle("More parties and events coming soon!", "tgfTables_DataCell_Text"));
    %n = 0;
    geTGF_HotSpotsGuiTable;
    if ((%count < %n)) {
        %item = %itemList.getValue(%n);
        geTGF_HotSpotsGuiTable;
        %isFriend = (%item SPC hostUserName.getFriendStatus() $= "friends");
        BuddyHudWin;
        if ((%item SPC baseImageURL $= "")) {
        }
        %imageURL = %item @ baseImageURL @ "?size=S";
        "";
        if (!(geTGF_HotSpotsDataTable SPC $ServerName $= "")) {
        }
        %sameServer = (%item SPC location_serverName $= $ServerName) ? "true" : "false";
        geTGF_HotSpotsDataTable;
        if ((%item SPC eventID $= "")) {
            %eventValue = "notAnEvent";
            geTGF_HotSpotsDataTable;
            %eventFmt = "<modulationColor:ffffff60>";
            (%count < %n);
        }
        if ((%item SPC subType $= "publicLocationEvent")) {
            %eventValue = "publicEvent";
            %eventFmt = "";
        }
        if (featured) {
            %eventValue = "featuredEvent";
            %item;
            %eventFmt = "";
        }
        %eventValue = "regularEvent";
        %eventFmt = "";
        %occupancyText = occupancy.formatOccupancy("<b>", "<color:ffffff60>(unknown)", "<color:ffffff60>-");
        %item;
        %friendOccupancyText = friendOccupancy.formatOccupancy("<b><color:40ff40>", "<color:ffffff60>(unknown)", "<color:ffffff60>-");
        %item;
        if ((%item >= occupancy)) {
        }
        %occupancySortVal = 99999;
        occupancy;
        if ((%item >= friendOccupancy)) {
        }
        %friendOccupancySortVal = 99999;
        friendOccupancy;
        %rowData = "poster" @ "\t" @ "" @ "\t" @ %imageURL;
        %item;
        %rowData = %item @ geTGF_tabs::hotSpotsTab_formatDescription(headline);
        %item @ headline @ "\t";
        %rowData = %item @ geTGF_tabs::hotSpotsTab_formatUserName(hostUserName, %isFriend);
        %item @ hostUserName @ "\t";
        %rowData = %item @ geTGF_tabs::hotSpotsTab_formatLocation(location_areaName);
        %item @ location_areaName @ "\t";
        %rowData = %rowData @ "\n" @ "location" @ "\t" @ %rowData @ "\n" @ "population" @ "\t" @ %occupancySortVal @ "\t" @ "<just:left>" @ geTGF_tabs::hotSpotsTab_formatDescription(%occupancyText);
        %rowData @ "\n" @ "username" @ "\t";
        %rowData = %rowData @ "\n" @ "description" @ "\t" @ %rowData @ "\n" @ "friends" @ "\t" @ %friendOccupancySortVal @ "\t" @ "<just:left>" @ geTGF_tabs::hotSpotsTab_formatDescription(%friendOccupancyText);
        0.0;
        %rowData = %rowData @ "\n" @ "sameServer" @ "\t" @ %sameServer @ "\t" @ "<just:left>[ICON]";
        %item;
        %rowData = %item @ accessMode @ "\t" @ "<just:left>[ICON]";
        %rowData @ "\n" @ "access" @ "\t";
        %rowData = geTGF @ 0.0 @ %rowData @ "\n" @ "event" @ "\t" @ %eventValue @ "\t" @ "<just:right>" @ %eventFmt @ "[ICON]";
        geTGF;
        %n.setRowDataByIndex(%rowData);
        %n = (1.0 + %n);
        geTGF_HotSpotsDataTable;
    }
    doFilter();
    updateListeners();
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
    %dataRowIndex = %item @ hostUserName.getRowIndexByCriteria();
    "username" @ "\t";
    %guiRowIndex = %dataRowIndex.getGuiRowIndexForDataRowIndex();
    geTGF_HotSpotsGuiTable;
    if ((0.0 >= %guiRowIndex)) {
        %guiRowIndex.doHiliteRow();
    }
    %this.constructDeetsWindow(%item);
    1.setVisible();
};
function geTGF_HotSpotsGuiTable::onRowSelected(%this, %guiRow, %rowIndex, %unused, %mouseClickCount) {
    if ((-(1.0) == %rowIndex)) {
        error(getScopeName() @ " " @ "- Gui Row" @ " " @ %guiRow @ " " @ "has no Data Row -" @ " " @ getTrace());
        return;
    }
    %this.makeFirstResponder(1);
    %cellIndex = "username".getColumnIndex();
    geTGF_HotSpotsDataTable;
    %userName = %rowIndex.getCellSortValue(%cellIndex);
    geTGF_HotSpotsDataTable;
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
        %item = "hotspots".findItem("happening", %userName);
        geTGF;
        "hotspots".DoDetails(%item);
    }
};
function geTGF_HotSpotsGuiTable::onKeyDown(%this, %modifier, %keyCode) {
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
function geTGF_HotSpotsFilterBox::onKeyUp(%this, %modifier, %keyCode) {
    cancel(filterDoItReallySchedule);
    filterDoItReallySchedule = %this @ %this.schedule(filterDoItReallyTimeoutMS, "doApplyFilterReally") @ %this;
    %this;
    return 0;
};
function geTGF_HotSpotsFilterBox::onKeyDown(%this, %modifier, %keyCode) {
    %keyCodeStr = %this.getStringFromKeyCode(%keyCode);
    if ((%keyCodeStr $= "\t")) {
        1.makeFirstResponder();
        return 1;
    }
    return 0;
};
function geTGF_HotSpotsFilterBox::doApplyFilterReally(%this) {
    cancel(filterDoItReallySchedule);
    filterDoItReallySchedule = %this @ "" @ %this;
    %this.getText().setFilterText();
    updateListeners();
};
function geTGF::openForBuildingDirectory(%this, %buildingName) {
    %buildingName = trim(%buildingName);
    if ((%buildingName $= "")) {
        return;
    }
    %areaName = Buildings::GetAreaName(%buildingName);
    %fitlerText = DestinationList::GetAreaNameUserFacingNameCityAndBuildingShort(%areaName, "");
    %updateListeners = isObject();
    geTGF_HotSpotsDataTable;
    "hotspots".openToTabName();
    %fitlerText.setText();
    %fitlerText.setFilterText();
    if (%updateListeners) {
        doFilter();
        updateListeners();
    }
};
function onDoneOrErrorCallback_GetUserRelations_ForHotSpots(%request) {
    onDoneOrErrorCallback_GetUserRelations_ProcessOnly(%request);
    if (!(isObject())) {
        return geTGF_HotSpotsDataTable;
    }
    %itemList = "hotspots".getItemList("happening");
    geTGF;
    %count = %itemList.count();
    %n = 0;
    if ((%count < %n)) {
        %item = %itemList.getValue(%n);
        %isFriend = (%item SPC hostUserName.getFriendStatus() $= "friends");
        BuddyHudWin;
        if (%isFriend) {
            %oldRowData = %item @ hostUserName;
            "username" @ "\t";
            %newRowData = %item @ geTGF_tabs::hotSpotsTab_formatUserName(hostUserName, %isFriend);
            %item @ hostUserName @ "\t";
            %oldRowData.setRowDataByCriteria(%newRowData);
        }
        %n = (1.0 + %n);
        geTGF_HotSpotsDataTable;
    }
    updateListeners();
};
