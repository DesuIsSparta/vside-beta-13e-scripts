function geTGF_tabs::fillTabMyPlace(%this) {
    %tabName = "myplace";
    %tab = %tabName.getTabWithName(%this);
    if (%tab.filled) {
        if (isObject(%tab.GuiTable)) {
            1.makeFirstResponder(%tab.GuiTable);
        }
        return;
    }
    %tab.filled = 1;
    %tab.fillTabGeneric(%this);
    %dataTable = new DataTable(geTGF_MyPlaceDataTable);
    0.addColumn(%dataTable, "event", "", "icon", 20, 1, 1);
    1.addColumn(%dataTable, "description", mlStyle("Description", "tgfTables_ColumnHeader"), "string", 342, 1, 1);
    1.addColumn(%dataTable, "username", mlStyle("Host", "tgfTables_ColumnHeader"), "string", 150, 1, 1);
    1.addColumn(%dataTable, "location", mlStyle("Where", "tgfTables_ColumnHeader"), "string", 202, 1, 1);
    1.addColumn(%dataTable, "population", mlStyle("People", "tgfTables_ColumnHeader"), "number", 70, 1, 1);
    1.addColumn(%dataTable, "friends", mlStyle("Friends", "tgfTables_ColumnHeader"), "number", 70, 1, 1);
    0.addColumn(%dataTable, "access", mlStyle("Access", "tgfTables_ColumnHeader"), "icon", 70, 1, 1);
    "description".setUniqueIdentifierColumns(%dataTable);
    "platform/client/ui/tgf/tgf_calendar_19x17_blue".addIconToColumn(%dataTable, "event", "publicEvent");
    "platform/client/ui/tgf/tgf_calendar_19x17_green".addIconToColumn(%dataTable, "event", "featuredEvent");
    "platform/client/ui/tgf/tgf_calendar_19x17".addIconToColumn(%dataTable, "event", "regularEvent");
    "platform/client/ui/tgf/tgf_house_white".addIconToColumn(%dataTable, "event", "notAnEvent");
    "platform/client/ui/tgf/tgf_door_open_white".addIconToColumn(%dataTable, "access", "open");
    "platform/client/ui/tgf/tgf_door_heart_white".addIconToColumn(%dataTable, "access", "friendsonly");
    "platform/client/ui/tgf/tgf_door_key_white".addIconToColumn(%dataTable, "access", "passwordprotected");
    %container = new GuiControl("") {
        profile = ETSNonModalProfile;
        position = "0 7";
        extent = "946 240";
    };
    %container.add(%tab);
    %dottedWindow = new GuiWindowCtrl("") {
        profile = DottedWindowProfile;
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
    %dottedWindow.add(%container);
    %label = new GuiMLTextCtrl("") {
        position = "2 2";
        extent = "400 20";
        style = "tgfTables_Label";
    };
    "My Places".setTextWithStyle(%label);
    %label.add(%container);
    %guiTable = new GuiTableCtrl(geTGF_MyPlaceGuiTable) {
        position = "2 20";
        extent = "933 220";
        visible = 1;
        spacing = 2;
    };
    22.setHeaderCellUniformExtent(%guiTable);
    0.setHeaderMLTextBoxTopMargin(%guiTable);
    18.setChildrenExtents(%guiTable);
    %dataTable.setDataTable(%guiTable);
    %tab.GuiTable = %guiTable;
    %guiTable.add(%container);
    %guiTable.alternativeTextCtrl = new GuiMLTextCtrl("") {
        position = "226 4";
        extent = "717 18";
        noEntriesText = ".. Strange, something went wrong. Try pressing refresh in a few seconds.";
    };
    %guiTable.alternativeTextCtrl.add(%container);
    %dataTable = new DataTable(geTGF_OtherPlacesDataTable);
    0.addColumn(%dataTable, "event", "", "icon", 20, 1, 1);
    1.addColumn(%dataTable, "description", mlStyle("Description", "tgfTables_ColumnHeader"), "string", ((342.0 + 150.0) + %guiTable.spacing), 1, 1);
    1.addColumn(%dataTable, "location", mlStyle("Where", "tgfTables_ColumnHeader"), "string", 202, 1, 1);
    1.addColumn(%dataTable, "population", mlStyle("People", "tgfTables_ColumnHeader"), "number", 70, 1, 1);
    1.addColumn(%dataTable, "friends", mlStyle("Friends", "tgfTables_ColumnHeader"), "number", 70, 1, 1);
    0.addColumn(%dataTable, "access", mlStyle("Access", "tgfTables_ColumnHeader"), "icon", 70, 1, 1);
    "description".setUniqueIdentifierColumns(%dataTable);
    "platform/client/ui/tgf/tgf_calendar_19x17_blue".addIconToColumn(%dataTable, "event", "publicEvent");
    "platform/client/ui/tgf/tgf_calendar_19x17_green".addIconToColumn(%dataTable, "event", "featuredEvent");
    "platform/client/ui/tgf/tgf_calendar_19x17".addIconToColumn(%dataTable, "event", "regularEvent");
    "platform/client/ui/tgf/tgf_house_white".addIconToColumn(%dataTable, "event", "notAnEvent");
    "platform/client/ui/tgf/tgf_door_open_white".addIconToColumn(%dataTable, "access", "open");
    "platform/client/ui/tgf/tgf_door_heart_white".addIconToColumn(%dataTable, "access", "friendsonly");
    "platform/client/ui/tgf/tgf_door_key_white".addIconToColumn(%dataTable, "access", "passwordprotected");
    %container = new GuiControl("") {
        profile = ETSNonModalProfile;
        position = "0 250";
        extent = "946 240";
    };
    %container.add(%tab);
    %dottedWindow = new GuiWindowCtrl("") {
        profile = DottedWindowProfile;
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
    %dottedWindow.add(%container);
    %label = new GuiMLTextCtrl("") {
        position = "2 2";
        extent = "400 20";
        style = "tgfTables_Label";
    };
    "Other Available Places".setTextWithStyle(%label);
    %label.add(%container);
    %guiTable = new GuiTableCtrl(geTGF_OtherPlacesGuiTable) {
        position = "2 20";
        extent = "933 220";
        spacing = 2;
    };
    22.setHeaderCellUniformExtent(%guiTable);
    0.setHeaderMLTextBoxTopMargin(%guiTable);
    18.setChildrenExtents(%guiTable);
    %dataTable.setDataTable(%guiTable);
    %tab.GuiTable = %guiTable;
    %guiTable.add(%container);
    %guiTable.alternativeTextCtrl = new GuiMLTextCtrl("") {
        position = "226 4";
        extent = "717 18";
        noEntriesText = ".. You own them all!";
    };
    %guiTable.alternativeTextCtrl.add(%container);
    if (0) {
        %invite = new GuiMLTextCtrl("") {
            position = "250 473";
            extent = "600 30";
            text = mlStyle($MsgCat::invitation["TEXT-TGF-MYPLACE"], "tgfTables_Invite");
        };
        %invite.add(%tab);
    }
    %this.refreshTabMyPlace();
};
function geTGF_tabs::onShowTabMyPlace(%this) {
    cancel(geTGF.geTGF_Refresh_Schedule);
    1.setActive(geTGF_Refresh);
    1.setVisible(geTGF_Refresh);
    1.makeFirstResponder(geTGF_MyPlaceGuiTable);
};
$gHaveGottenManagerSpaces = 0;
function geTGF_tabs::refreshTabMyPlace(%this) {
    1.setVisible(geTGF_MyPlaceGuiTable.alternativeTextCtrl);
    mlStyle("Fetching..", "tgfTables_DataCell_Text").setText(geTGF_MyPlaceGuiTable.alternativeTextCtrl);
    getOwnerSpacesInfo($Player::Name, "geTGF_OnCompleted_MyPlace");
    1.setVisible(geTGF_OtherPlacesGuiTable.alternativeTextCtrl);
    mlStyle("Fetching..", "tgfTables_DataCell_Text").setText(geTGF_OtherPlacesGuiTable.alternativeTextCtrl);
    getOwnerSpacesInfo("The-Manager", "geTGF_OnCompleted_MyPlace");
};
function geTGF_OnCompleted_MyPlace(%tracker) {
    if ((geTGF_tabs.getCurrentTab().name $= "myplace")) {
        cancel(geTGF.geTGF_Refresh_Schedule);
        1.setActive(geTGF_Refresh);
    }
    if (!(isObject(%tracker))) {
        error(getScopeName() @ " " @ "- null tracker." @ " " @ getTrace());
        return;
    }
    if ((%tracker.ownerName $= $Player::Name)) {
        %listName = "myplace";
        %guiTable = geTGF_MyPlaceGuiTable;
        %properOwnerName = $Player::Name;
    }
    %listName = "otherplaces";
    %guiTable = geTGF_OtherPlacesGuiTable;
    %properOwnerName = "The-Manager";
    "happening".clearItemList(geTGF, %listName);
    %count = %tracker.getCount();
    %n = 0;
    while ((%n < %count)) {
        %itemObj = %n.getObject(%tracker);
        if (!("owner".get(%itemObj) $= %properOwnerName)) {
            error(getScopeName() @ " " @ "- improper owner. should be \"" @ %properOwnerName @ "\" but is \"" @ "owner".get(%itemObj) @ "\". skipping." @ " " @ getTrace());
        }
        if ((%guiTable.getId() == geTGF_OtherPlacesGuiTable.getId())) {
        }
        if (!("type".get(%itemObj) $= "MODEL")) {
            echo(getScopeName() @ " " @ "- skipping space" @ " " @ "description".get(%itemObj));
        }
        %id = "description".get(%itemObj) @ " " @ formatInt("%0.3d", %n);
        %item = %id.createNewItem(geTGF, %listName, "happening");
        %item.accessMode = "access".get(%itemObj);
        %item.baseImageURL = "baseImageURL".get(%itemObj);
        %item.eventID = "eventId".get(%itemObj);
        %item.featured = "featured".get(%itemObj);
        %item.occupancy = "occupancy".get(%itemObj);
        %item.friendOccupancy = "friendOccupancy".get(%itemObj);
        %item.goThereVURL = "URI".get(%itemObj);
        %item.headline = "description".get(%itemObj);
        %item.hostUserName = "owner".get(%itemObj);
        %item.location_areaName = "location.areaName".get(%itemObj);
        %item.location_buildingName = "location.buildingName".get(%itemObj);
        %item.location_serverName = "location.serverName".get(%itemObj);
        %item.moreInfoURL = "moreInfoURL".get(%itemObj);
        %item.subType = (%item.eventID $= "") ? "apt" : "aptEvent";
        %n = (%n + 1.0);
    }
    if ("happening".testItemList(geTGF, "myplace")) {
    }
    %bothListsExist = "happening".testItemList(geTGF, "otherplaces");
    (%n < %count);
    if (%bothListsExist) {
        geTGF_OnCompleted_MyPlaceRemoveOwnedSpacesFromAvailableList();
        if ((%guiTable.getId() != geTGF_OtherPlacesGuiTable.getId())) {
            populateMyPlaceTableFromItemList(geTGF_OtherPlacesGuiTable, "otherplaces", "happening");
        }
    }
    populateMyPlaceTableFromItemList(%guiTable, %listName, "happening");
};
function geTGF_OnCompleted_MyPlaceRemoveOwnedSpacesFromAvailableList() {
    "location_areaName".removeItemsFromList1WithMatchingItemInList2(geTGF, "otherplaces", "happening", "myplace", "happening");
};
function populateMyPlaceTableFromItemList(%guiTable, %listName, %listType) {
    %itemList = %listType.getItemList(geTGF, %listName);
    %count = %itemList.count();
    %dataTable = %guiTable.getDataTable();
    %dataTable.clear();
    %count.addRows(%dataTable);
    if ((%count == 0.0)) {
        1.setVisible(%guiTable.alternativeTextCtrl);
        mlStyle(%guiTable.alternativeTextCtrl.noEntriesText, "tgfTables_DataCell_Text").setText(%guiTable.alternativeTextCtrl);
        0.setVisible(%guiTable);
        return;
    }
    0.setVisible(%guiTable.alternativeTextCtrl);
    1.setVisible(%guiTable);
    %n = 0;
    while ((%n < %count)) {
        %item = %n.getValue(%itemList);
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
        if ((%item.hostUserName $= $Player::Name)) {
        }
        %hostUserName = %item.hostUserName;
        "you!";
        %rowData = "";
        %rowData = %rowData @ "\n" @ "event" @ "\t" @ %eventValue @ "\t" @ "<just:right>" @ %eventFmt @ "[ICON]";
        %rowData = %rowData @ "\n" @ "description" @ "\t" @ %item.id @ "\t" @ geTGF_tabs::hotSpotsTab_formatDescription(%item.headline);
        if ("userName".hasColumnNamed(%dataTable)) {
            %rowData = %rowData @ "\n" @ "username" @ "\t" @ %hostUserName @ "\t" @ geTGF_tabs::hotSpotsTab_formatUserName(%hostUserName, 0);
        }
        %rowData = %rowData @ "\n" @ "location" @ "\t" @ %item.location_areaName @ "\t" @ geTGF_tabs::hotSpotsTab_formatLocation2(%item.location_areaName);
        %rowData = %rowData @ "\n" @ "population" @ "\t" @ %occupancySortVal @ "\t" @ "<just:left>" @ geTGF_tabs::hotSpotsTab_formatDescription(%occupancyText);
        %rowData = %rowData @ "\n" @ "friends" @ "\t" @ %friendOccupancySortVal @ "\t" @ "<just:left>" @ geTGF_tabs::hotSpotsTab_formatDescription(%friendOccupancyText);
        %rowData = %rowData @ "\n" @ "access" @ "\t" @ %item.accessMode @ "\t" @ "<just:left>[ICON]";
        %rowData = trim(%rowData);
        %rowData.setRowDataByIndex(%dataTable, %n);
        %n = (%n + 1.0);
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
    %dataRowIndex = "description" @ "\t" @ %item.id.getRowIndexByCriteria(geTGF_MyPlaceDataTable);
    %guiRowIndex = %dataRowIndex.getGuiRowIndexForDataRowIndex(geTGF_MyPlaceGuiTable);
    if ((%guiRowIndex >= 0.0)) {
        %guiRowIndex.doHiliteRow(geTGF_MyPlaceGuiTable);
    }
    %item.constructDeetsWindow(%this, geDeetsWindow);
    1.setVisible(geDeetsLayer);
    return geDeetsWindow;
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
    if ((%rowIndex == -(1.0))) {
        error(getScopeName() @ " " @ "- Gui Row" @ " " @ %guiRow @ " " @ "has no Data Row -" @ " " @ getTrace());
        return;
    }
    1.makeFirstResponder(%this);
    %cellIndex = "description".getColumnIndex(%this.getDataTable());
    %itemID = %cellIndex.getCellSortValue(%this.getDataTable(), %rowIndex);
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
        %item = %itemID.findItem(geTGF, %listName, "happening");
        %item.DoDetails(geTGF, "myplace");
    }
};
function geTGF_MyPlaceGuiTable::onKeyDown(%this, %modifier, %keyCode) {
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
