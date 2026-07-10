function geTGF_tabs::fillTabMyPlace(%this) {
    %tabName = "myplace";
    %tab = %this.getTabWithName(%tabName);
    if (%tab.filled) {
        if (isObject(%tab.GuiTable)) {
            %tab.GuiTable.makeFirstResponder(1);
        }
        return;
    }
    %tab.filled = 1;
    %this.fillTabGeneric(%tab);
    %dataTable = new DataTable(geTGF_MyPlaceDataTable);;
    %dataTable.addColumn("event", "", "icon", 20, 1, 1, 0);
    %dataTable.addColumn("description", mlStyle("Description", "tgfTables_ColumnHeader"), "string", 342, 1, 1, 1);
    %dataTable.addColumn("username", mlStyle("Host", "tgfTables_ColumnHeader"), "string", 150, 1, 1, 1);
    %dataTable.addColumn("location", mlStyle("Where", "tgfTables_ColumnHeader"), "string", 202, 1, 1, 1);
    %dataTable.addColumn("population", mlStyle("People", "tgfTables_ColumnHeader"), "number", 70, 1, 1, 1);
    %dataTable.addColumn("friends", mlStyle("Friends", "tgfTables_ColumnHeader"), "number", 70, 1, 1, 1);
    %dataTable.addColumn("access", mlStyle("Access", "tgfTables_ColumnHeader"), "icon", 70, 1, 1, 0);
    %dataTable.setUniqueIdentifierColumns("description");
    %dataTable.addIconToColumn("event", "publicEvent", "platform/client/ui/tgf/tgf_calendar_19x17_blue");
    %dataTable.addIconToColumn("event", "featuredEvent", "platform/client/ui/tgf/tgf_calendar_19x17_green");
    %dataTable.addIconToColumn("event", "regularEvent", "platform/client/ui/tgf/tgf_calendar_19x17");
    %dataTable.addIconToColumn("event", "notAnEvent", "platform/client/ui/tgf/tgf_house_white");
    %dataTable.addIconToColumn("access", "open", "platform/client/ui/tgf/tgf_door_open_white");
    %dataTable.addIconToColumn("access", "friendsonly", "platform/client/ui/tgf/tgf_door_heart_white");
    %dataTable.addIconToColumn("access", "passwordprotected", "platform/client/ui/tgf/tgf_door_key_white");
    0;
    %container = new ""() {
        profile = GuiControl @ ETSNonModalProfile;
        position = "0 7";
        extent = "946 240";
    };
    %tab.add(%container);
    0;
    %dottedWindow = new ""() {
        profile = GuiWindowCtrl @ DottedWindowProfile;
        position = "7 16";
        extent = "936 222";
        canHilite = 0;
        canMove = 0;
        canClose = 0;
        canMinimize = 0;
        canMaximize = 0;
        resizeWidth = 0;
        resizeHeight = 0;
    };
    %container.add(%dottedWindow);
    0;
    %label = new ""() {
        position = GuiMLTextCtrl @ "2 2";
        extent = "400 20";
        style = "tgfTables_Label";
    };
    %label.setTextWithStyle("My Places");
    %container.add(%label);
    %guiTable = new GuiTableCtrl(geTGF_MyPlaceGuiTable) {
        position = "2 20";
        extent = "933 220";
        visible = 1;
        spacing = 2;
    };
    %guiTable.setHeaderCellUniformExtent(22);
    %guiTable.setHeaderMLTextBoxTopMargin(0);
    %guiTable.setChildrenExtents(18);
    %guiTable.setDataTable(%dataTable);
    %tab.GuiTable = %guiTable;
    %container.add(%guiTable);
    0;
    %guiTable.alternativeTextCtrl = new ""() {
        position = GuiMLTextCtrl @ "226 4";
        extent = "717 18";
        noEntriesText = ".. Strange, something went wrong. Try pressing refresh in a few seconds.";
    };
    %container.add(%guiTable.alternativeTextCtrl);
    %dataTable = new DataTable(geTGF_OtherPlacesDataTable);;
    %dataTable.addColumn("event", "", "icon", 20, 1, 1, 0);
    %dataTable.addColumn("description", mlStyle("Description", "tgfTables_ColumnHeader"), "string", (%guiTable.spacing + (150.0 + 342.0)), 1, 1, 1);
    %dataTable.addColumn("location", mlStyle("Where", "tgfTables_ColumnHeader"), "string", 202, 1, 1, 1);
    %dataTable.addColumn("population", mlStyle("People", "tgfTables_ColumnHeader"), "number", 70, 1, 1, 1);
    %dataTable.addColumn("friends", mlStyle("Friends", "tgfTables_ColumnHeader"), "number", 70, 1, 1, 1);
    %dataTable.addColumn("access", mlStyle("Access", "tgfTables_ColumnHeader"), "icon", 70, 1, 1, 0);
    %dataTable.setUniqueIdentifierColumns("description");
    %dataTable.addIconToColumn("event", "publicEvent", "platform/client/ui/tgf/tgf_calendar_19x17_blue");
    %dataTable.addIconToColumn("event", "featuredEvent", "platform/client/ui/tgf/tgf_calendar_19x17_green");
    %dataTable.addIconToColumn("event", "regularEvent", "platform/client/ui/tgf/tgf_calendar_19x17");
    %dataTable.addIconToColumn("event", "notAnEvent", "platform/client/ui/tgf/tgf_house_white");
    %dataTable.addIconToColumn("access", "open", "platform/client/ui/tgf/tgf_door_open_white");
    %dataTable.addIconToColumn("access", "friendsonly", "platform/client/ui/tgf/tgf_door_heart_white");
    %dataTable.addIconToColumn("access", "passwordprotected", "platform/client/ui/tgf/tgf_door_key_white");
    0;
    %container = new ""() {
        profile = GuiControl @ ETSNonModalProfile;
        position = "0 250";
        extent = "946 240";
    };
    %tab.add(%container);
    0;
    %dottedWindow = new ""() {
        profile = GuiWindowCtrl @ DottedWindowProfile;
        position = "7 16";
        extent = "936 224";
        canHilite = 0;
        canMove = 0;
        canClose = 0;
        canMinimize = 0;
        canMaximize = 0;
        resizeWidth = 0;
        resizeHeight = 0;
    };
    %container.add(%dottedWindow);
    0;
    %label = new ""() {
        position = GuiMLTextCtrl @ "2 2";
        extent = "400 20";
        style = "tgfTables_Label";
    };
    %label.setTextWithStyle("Other Available Places");
    %container.add(%label);
    %guiTable = new GuiTableCtrl(geTGF_OtherPlacesGuiTable) {
        position = "2 20";
        extent = "933 220";
        spacing = 2;
    };
    %guiTable.setHeaderCellUniformExtent(22);
    %guiTable.setHeaderMLTextBoxTopMargin(0);
    %guiTable.setChildrenExtents(18);
    %guiTable.setDataTable(%dataTable);
    %tab.GuiTable = %guiTable;
    %container.add(%guiTable);
    0;
    %guiTable.alternativeTextCtrl = new ""() {
        position = GuiMLTextCtrl @ "226 4";
        extent = "717 18";
        noEntriesText = ".. You own them all!";
    };
    %container.add(%guiTable.alternativeTextCtrl);
    if (0) {
        0;
        %invite = new ""() {
            position = GuiMLTextCtrl @ "250 473";
            extent = "600 30";
            text = mlStyle(, "tgfTables_Invite");
        };
        %tab.add(%invite);
    }
    %this.refreshTabMyPlace();
};
function geTGF_tabs::onShowTabMyPlace(%this) {
    cancel(geTGF_Refresh_Schedule);
    1.setActive();
    1.setVisible();
    1.makeFirstResponder();
};
$gHaveGottenManagerSpaces = 0;
function geTGF_tabs::refreshTabMyPlace(%this) {
    alternativeTextCtrl.setVisible(1);
    alternativeTextCtrl.setText(mlStyle("Fetching..", "tgfTables_DataCell_Text"));
    getOwnerSpacesInfo($Player::Name, "geTGF_OnCompleted_MyPlace");
    alternativeTextCtrl.setVisible(1);
    alternativeTextCtrl.setText(mlStyle("Fetching..", "tgfTables_DataCell_Text"));
    getOwnerSpacesInfo("The-Manager", "geTGF_OnCompleted_MyPlace");
};
function geTGF_OnCompleted_MyPlace(%tracker) {
    if ((geTGF_tabs.getCurrentTab().name $= "myplace")) {
        cancel(geTGF_tabs.getCurrentTab().geTGF_Refresh_Schedule);
        1.setActive();
    }
    if (!(isObject(%tracker))) {
        error(getScopeName() @ " " @ "- null tracker." @ " " @ getTrace());
        return geTGF_Refresh;
    }
    if ((%tracker.ownerName $= $Player::Name)) {
        %listName = "myplace";
        // unhandled opcode 515 at 0x000008DC
        %listName = geTGF_MyPlaceGuiTable;
        %properOwnerName = $Player::Name;
    }
    %listName = "otherplaces";
    // unhandled opcode 515 at 0x000008F1
    %listName = geTGF_OtherPlacesGuiTable;
    %properOwnerName = "The-Manager";
    %listName.clearItemList("happening");
    %count = %tracker.getCount();
    geTGF;
    %n = 0;
    if ((%count < %n)) {
        %itemObj = %tracker.getObject(%n);
        if (!(%itemObj.get("owner") $= %properOwnerName)) {
            error(getScopeName() @ " " @ "- improper owner. should be \"" @ %properOwnerName @ "\" but is \"" @ %itemObj.get("owner") @ "\". skipping." @ " " @ getTrace());
        }
        if ((geTGF_OtherPlacesGuiTable.getId() == %guiTable.getId())) {
        }
        if (!(%itemObj.get("type") $= "MODEL")) {
            echo(getScopeName() @ " " @ "- skipping space" @ " " @ %itemObj.get("description"));
        }
        %id = %itemObj.get("description") @ " " @ formatInt("%0.3d", %n);
        %item = %listName.createNewItem("happening", %id);
        geTGF;
        %item.accessMode = %itemObj.get("access");
        %item.baseImageURL = %itemObj.get("baseImageURL");
        %item.eventID = %itemObj.get("eventId");
        %item.featured = %itemObj.get("featured");
        %item.occupancy = %itemObj.get("occupancy");
        %item.friendOccupancy = %itemObj.get("friendOccupancy");
        %item.goThereVURL = %itemObj.get("URI");
        %item.headline = %itemObj.get("description");
        %item.hostUserName = %itemObj.get("owner");
        %item.location_areaName = %itemObj.get("location.areaName");
        %item.location_buildingName = %itemObj.get("location.buildingName");
        %item.location_serverName = %itemObj.get("location.serverName");
        %item.moreInfoURL = %itemObj.get("moreInfoURL");
        %item.subType = (%item.eventID $= "") ? "apt" : "aptEvent";
        %n = (1.0 + %n);
    }
    if ("myplace".testItemList("happening")) {
    }
    %bothListsExist = "otherplaces".testItemList("happening");
    geTGF;
    if (%bothListsExist) {
        geTGF_OnCompleted_MyPlaceRemoveOwnedSpacesFromAvailableList();
        if ((geTGF_OtherPlacesGuiTable.getId() != %guiTable.getId())) {
            populateMyPlaceTableFromItemList("otherplaces", "happening");
        }
    }
    populateMyPlaceTableFromItemList(%guiTable, %listName, "happening");
};
function geTGF_OnCompleted_MyPlaceRemoveOwnedSpacesFromAvailableList() {
    "otherplaces".removeItemsFromList1WithMatchingItemInList2("happening", "myplace", "happening", "location_areaName");
};
function populateMyPlaceTableFromItemList(%guiTable, %listName, %listType) {
    %itemList = %listName.getItemList(%listType);
    geTGF;
    %count = %itemList.count();
    %dataTable = %guiTable.getDataTable();
    %dataTable.clear();
    %dataTable.addRows(%count);
    if ((0.0 == %count)) {
        %guiTable.alternativeTextCtrl.setVisible(1);
        %guiTable.alternativeTextCtrl.setText(mlStyle(%guiTable.alternativeTextCtrl.noEntriesText, "tgfTables_DataCell_Text"));
        %guiTable.setVisible(0);
        return;
    }
    %guiTable.alternativeTextCtrl.setVisible(0);
    %guiTable.setVisible(1);
    %n = 0;
    if ((%count < %n)) {
        %item = %itemList.getValue(%n);
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
            error(getScopeName() @ " " @ "- public location. odd.");
            %eventValue = "publicEvent";
            %eventFmt = "";
        }
        if (%item.featured) {
            %eventValue = "featuredEvent";
            %eventFmt = "";
        }
        %eventValue = "regularEvent";
        %eventFmt = "";
        %occupancyText = %item.occupancy.formatOccupancy("<b>", "<color:ffffff60>(unknown)", "<color:ffffff60>-");
        geTGF;
        %friendOccupancyText = %item.friendOccupancy.formatOccupancy("<b><color:40ff40>", "<color:ffffff60>(unknown)", "<color:ffffff60>-");
        geTGF;
        if ((0.0 >= %item.occupancy)) {
        }
        %occupancySortVal = 99999;
        %item.occupancy;
        if ((0.0 >= %item.friendOccupancy)) {
        }
        %friendOccupancySortVal = 99999;
        %item.friendOccupancy;
        if ((%item.hostUserName $= $Player::Name)) {
        }
        %hostUserName = %item.hostUserName;
        "you!";
        %rowData = "";
        %rowData = %rowData @ "\n" @ "event" @ "\t" @ %eventValue @ "\t" @ "<just:right>" @ %eventFmt @ "[ICON]";
        %rowData = %rowData @ "\n" @ "description" @ "\t" @ %item.id @ "\t" @ geTGF_tabs::hotSpotsTab_formatDescription(%item.headline);
        if (%dataTable.hasColumnNamed("userName")) {
            %rowData = %rowData @ "\n" @ "username" @ "\t" @ %hostUserName @ "\t" @ geTGF_tabs::hotSpotsTab_formatUserName(%hostUserName, 0);
        }
        %rowData = %rowData @ "\n" @ "location" @ "\t" @ %item.location_areaName @ "\t" @ geTGF_tabs::hotSpotsTab_formatLocation2(%item.location_areaName);
        %rowData = %rowData @ "\n" @ "population" @ "\t" @ %occupancySortVal @ "\t" @ "<just:left>" @ geTGF_tabs::hotSpotsTab_formatDescription(%occupancyText);
        %rowData = %rowData @ "\n" @ "friends" @ "\t" @ %friendOccupancySortVal @ "\t" @ "<just:left>" @ geTGF_tabs::hotSpotsTab_formatDescription(%friendOccupancyText);
        %rowData = %rowData @ "\n" @ "access" @ "\t" @ %item.accessMode @ "\t" @ "<just:left>[ICON]";
        %rowData = trim(%rowData);
        %dataTable.setRowDataByIndex(%n, %rowData);
        %n = (1.0 + %n);
    }
    %dataTable.doFilter();
    %dataTable.updateListeners();
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
function geTGF::myplace_GetAndOpenDetailsContainer(%this, %item) {
    %dataRowIndex = "description" @ "\t" @ %item.id.getRowIndexByCriteria();
    geTGF_MyPlaceDataTable;
    %guiRowIndex = %dataRowIndex.getGuiRowIndexForDataRowIndex();
    geTGF_MyPlaceGuiTable;
    if ((0.0 >= %guiRowIndex)) {
        %guiRowIndex.doHiliteRow();
    }
    %this.constructDeetsWindow(%item);
    1.setVisible();
};
function geTGF_MyPlaceGuiTable::onRowSelected(%this, %guiRow, %rowIndex, %alreadySelected, %mouseClickCount) {
    %listName = "myplace";
    geTGF_MyPlaceEitherGuiTable::onRowSelected(%this, %guiRow, %rowIndex, %alreadySelected, %mouseClickCount, %listName);
};
function geTGF_OtherPlacesGuiTable::onRowSelected(%this, %guiRow, %rowIndex, %alreadySelected, %mouseClickCount) {
    %listName = "otherplaces";
    geTGF_MyPlaceEitherGuiTable::onRowSelected(%this, %guiRow, %rowIndex, %alreadySelected, %mouseClickCount, %listName);
};
function geTGF_MyPlaceEitherGuiTable::onRowSelected(%this, %guiRow, %rowIndex, %alreadySelected, %mouseClickCount, %listName) {
    if ((-(1.0) == %rowIndex)) {
        error(getScopeName() @ " " @ "- Gui Row" @ " " @ %guiRow @ " " @ "has no Data Row -" @ " " @ getTrace());
        return;
    }
    %this.makeFirstResponder(1);
    %cellIndex = %this.getDataTable().getColumnIndex("description");
    %itemID = %this.getDataTable().getCellSortValue(%rowIndex, %cellIndex);
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
        %item = %listName.findItem("happening", %itemID);
        geTGF;
        "myplace".DoDetails(%item);
    }
};
function geTGF_MyPlaceGuiTable::onKeyDown(%this, %modifier, %keyCode) {
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
function geTGF_tabs::Maps_clickMyApartment() {
    geTGF.closeFully();
    doTeleportToMyApartment();
};
function geTGF_tabs::Maps_GetApartmentVURL(%this) {
    getApartmentVURL("geTGF_tabs::Maps_GotApartmentVURL");
};
function geTGF_tabs::Maps_GotApartmentVURL(%status, %vurl) {
    if ((%status $= "noOwnedSpace")) {
    }
    %active = !(%vurl $= "");
    $geTGF::Map_ApartmentVURL = %vurl;
};
