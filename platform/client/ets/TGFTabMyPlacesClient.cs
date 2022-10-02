function geTGF_tabs::fillTabMyPlace(%this)
{
    %tabName = "myplace";
    %tab = %this.getTabWithName(%tabName);
    if (%tab.filled)
    {
        if (isObject(%tab.GuiTable))
        {
            %tab.GuiTable.makeFirstResponder(1);
        }
        return ;
    }
    %tab.filled = 1;
    %this.fillTabGeneric(%tab);
    %dataTable = new DataTable(geTGF_MyPlaceDataTable);
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
    %container = new GuiControl()
    {
        profile = ETSNonModalProfile;
        position = "0 7";
        extent = "946 240";
    };
    %tab.add(%container);
    %dottedWindow = new GuiWindowCtrl()
    {
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
    %container.add(%dottedWindow);
    %label = new GuiMLTextCtrl()
    {
        position = "2 2";
        extent = "400 20";
        style = "tgfTables_Label";
    };
    %label.setTextWithStyle("My Places");
    %container.add(%label);
    %guiTable = new GuiTableCtrl(geTGF_MyPlaceGuiTable)
    {
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
    %guiTable.alternativeTextCtrl = new GuiMLTextCtrl()
    {
        position = "226 4";
        extent = "717 18";
        noEntriesText = ".. Strange, something went wrong. Try pressing refresh in a few seconds.";
    };
    %container.add(%guiTable.alternativeTextCtrl);
    %dataTable = new DataTable(geTGF_OtherPlacesDataTable);
    %dataTable.addColumn("event", "", "icon", 20, 1, 1, 0);
    %dataTable.addColumn("description", mlStyle("Description", "tgfTables_ColumnHeader"), "string", (342 + 150) + %guiTable.spacing, 1, 1, 1);
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
    %container = new GuiControl()
    {
        profile = ETSNonModalProfile;
        position = "0 250";
        extent = "946 240";
    };
    %tab.add(%container);
    %dottedWindow = new GuiWindowCtrl()
    {
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
    %container.add(%dottedWindow);
    %label = new GuiMLTextCtrl()
    {
        position = "2 2";
        extent = "400 20";
        style = "tgfTables_Label";
    };
    %label.setTextWithStyle("Other Available Places");
    %container.add(%label);
    %guiTable = new GuiTableCtrl(geTGF_OtherPlacesGuiTable)
    {
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
    %guiTable.alternativeTextCtrl = new GuiMLTextCtrl()
    {
        position = "226 4";
        extent = "717 18";
        noEntriesText = ".. You own them all!";
    };
    %container.add(%guiTable.alternativeTextCtrl);
    if (0)
    {
        %invite = new GuiMLTextCtrl()
        {
            position = "250 473";
            extent = "600 30";
            text = mlStyle($MsgCat::invitation["TEXT-TGF-MYPLACE"], "tgfTables_Invite");
        };
        %tab.add(%invite);
    }
    %this.refreshTabMyPlace();
    return ;
}
function geTGF_tabs::onShowTabMyPlace(%this)
{
    cancel(geTGF.geTGF_Refresh_Schedule);
    geTGF_Refresh.setActive(1);
    geTGF_Refresh.setVisible(1);
    geTGF_MyPlaceGuiTable.makeFirstResponder(1);
    return ;
}
$gHaveGottenManagerSpaces = 0;
function geTGF_tabs::refreshTabMyPlace(%this)
{
    geTGF_MyPlaceGuiTable.alternativeTextCtrl.setVisible(1);
    geTGF_MyPlaceGuiTable.alternativeTextCtrl.setText(mlStyle("Fetching..", "tgfTables_DataCell_Text"));
    getOwnerSpacesInfo($Player::Name, "geTGF_OnCompleted_MyPlace");
    geTGF_OtherPlacesGuiTable.alternativeTextCtrl.setVisible(1);
    geTGF_OtherPlacesGuiTable.alternativeTextCtrl.setText(mlStyle("Fetching..", "tgfTables_DataCell_Text"));
    getOwnerSpacesInfo("The-Manager", "geTGF_OnCompleted_MyPlace");
    return ;
}
function geTGF_OnCompleted_MyPlace(%tracker)
{
    if (geTGF_tabs.getCurrentTab().name $= "myplace")
    {
        cancel(geTGF.geTGF_Refresh_Schedule);
        geTGF_Refresh.setActive(1);
    }
    if (!isObject(%tracker))
    {
        error(getScopeName() SPC "- null tracker." SPC getTrace());
        return ;
    }
    if (%tracker.ownerName $= $Player::Name)
    {
        %listName = "myplace";
        %guiTable = geTGF_MyPlaceGuiTable;
        %properOwnerName = $Player::Name;
    }
    else
    {
        %listName = "otherplaces";
        %guiTable = geTGF_OtherPlacesGuiTable;
        %properOwnerName = "The-Manager";
    }
    geTGF.clearItemList(%listName, "happening");
    %count = %tracker.getCount();
    %n = 0;
    while (%n < %count)
    {
        %itemObj = %tracker.getObject(%n);
        if (!(%itemObj.get("owner") $= %properOwnerName))
        {
            error(getScopeName() SPC "- improper owner. should be \"" @ %properOwnerName @ "\" but is \"" @ %itemObj.get("owner") @ "\". skipping." SPC getTrace());
        }
        else
        {
            if ((%guiTable.getId() == geTGF_OtherPlacesGuiTable.getId()) && !((%itemObj.get("type") $= "MODEL")))
            {
                echo(getScopeName() SPC "- skipping space" SPC %itemObj.get("description"));
            }
            else
            {
                %id = %itemObj.get("description") SPC formatInt("%0.3d", %n);
                %item = geTGF.createNewItem(%listName, "happening", %id);
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
                %item.subType = %item.eventID $= "" ? "apt" : "aptEvent";
            }
        }
        %n = %n + 1;
    }
    %bothListsExist = geTGF.testItemList("myplace", "happening") && geTGF.testItemList("otherplaces", "happening");
    if (%bothListsExist)
    {
        geTGF_OnCompleted_MyPlaceRemoveOwnedSpacesFromAvailableList();
        if (%guiTable.getId() != geTGF_OtherPlacesGuiTable.getId())
        {
            populateMyPlaceTableFromItemList(geTGF_OtherPlacesGuiTable, "otherplaces", "happening");
        }
    }
    populateMyPlaceTableFromItemList(%guiTable, %listName, "happening");
    return ;
}
function geTGF_OnCompleted_MyPlaceRemoveOwnedSpacesFromAvailableList()
{
    geTGF.removeItemsFromList1WithMatchingItemInList2("otherplaces", "happening", "myplace", "happening", "location_areaName");
    return ;
}
function populateMyPlaceTableFromItemList(%guiTable, %listName, %listType)
{
    %itemList = geTGF.getItemList(%listName, %listType);
    %count = %itemList.count();
    %dataTable = %guiTable.getDataTable();
    %dataTable.clear();
    %dataTable.addRows(%count);
    if (%count == 0)
    {
        %guiTable.alternativeTextCtrl.setVisible(1);
        %guiTable.alternativeTextCtrl.setText(mlStyle(%guiTable.alternativeTextCtrl.noEntriesText, "tgfTables_DataCell_Text"));
        %guiTable.setVisible(0);
        return ;
    }
    %guiTable.alternativeTextCtrl.setVisible(0);
    %guiTable.setVisible(1);
    %n = 0;
    while (%n < %count)
    {
        %item = %itemList.getValue(%n);
        %imageURL = %item.baseImageURL $= "" ? "" : %item;
        %sameServer = !(($ServerName $= "")) && (%item.location_serverName $= $ServerName) ? "true" : "false";
        if (%item.eventID $= "")
        {
            %eventValue = "notAnEvent";
            %eventFmt = "<modulationColor:ffffff60>";
        }
        else
        {
            if (%item.subType $= "publicLocationEvent")
            {
                error(getScopeName() SPC "- public location. odd.");
                %eventValue = "publicEvent";
                %eventFmt = "";
            }
            else
            {
                if (%item.featured)
                {
                    %eventValue = "featuredEvent";
                    %eventFmt = "";
                }
                else
                {
                    %eventValue = "regularEvent";
                    %eventFmt = "";
                }
            }
        }
        %occupancyText = geTGF.formatOccupancy(%item.occupancy, "<b>", "<color:ffffff60>(unknown)", "<color:ffffff60>-");
        %friendOccupancyText = geTGF.formatOccupancy(%item.friendOccupancy, "<b><color:40ff40>", "<color:ffffff60>(unknown)", "<color:ffffff60>-");
        %occupancySortVal = %item.occupancy >= 0 ? %item : 99999;
        %friendOccupancySortVal = %item.friendOccupancy >= 0 ? %item : 99999;
        %hostUserName = %item.hostUserName $= $Player::Name ? "you!" : %item;
        %rowData = "";
        %rowData = %rowData NL "event" TAB %eventValue TAB "<just:right>" @ %eventFmt @ "[ICON]";
        %rowData = %rowData NL "description" TAB %item.id TAB geTGF_tabs::hotSpotsTab_formatDescription(%item.headline);
        if (%dataTable.hasColumnNamed("userName"))
        {
            %rowData = %rowData NL "username" TAB %hostUserName TAB geTGF_tabs::hotSpotsTab_formatUserName(%hostUserName, 0);
        }
        %rowData = %rowData NL "location" TAB %item.location_areaName TAB geTGF_tabs::hotSpotsTab_formatLocation2(%item.location_areaName);
        %rowData = %rowData NL "population" TAB %occupancySortVal TAB "<just:left>" @ geTGF_tabs::hotSpotsTab_formatDescription(%occupancyText);
        %rowData = %rowData NL "friends" TAB %friendOccupancySortVal TAB "<just:left>" @ geTGF_tabs::hotSpotsTab_formatDescription(%friendOccupancyText);
        %rowData = %rowData NL "access" TAB %item.accessMode TAB "<just:left>[ICON]";
        %rowData = trim(%rowData);
        %dataTable.setRowDataByIndex(%n, %rowData);
        %n = %n + 1;
    }
    %dataTable.doFilter();
    %dataTable.updateListeners();
    return ;
}
function geTGF_tabs::friendsTab_formatUserName(%name, %isFriend)
{
    return geTGF_tabs::hotSpotsTab_formatUserName(%name, %isFriend);
}
function geTGF_tabs::friendsTab_formatLocation(%location)
{
    return geTGF_tabs::hotSpotsTab_formatLocation(%location);
}
function geTGF_tabs::friendsTab_formatStatusMsg(%msg)
{
    return geTGF_tabs::hotSpotsTab_formatDescription(%msg);
}
function geTGF::myplace_GetAndOpenDetailsContainer(%this, %item)
{
    %dataRowIndex = geTGF_MyPlaceDataTable.getRowIndexByCriteria("description" TAB %item.id);
    %guiRowIndex = geTGF_MyPlaceGuiTable.getGuiRowIndexForDataRowIndex(%dataRowIndex);
    if (%guiRowIndex >= 0)
    {
        geTGF_MyPlaceGuiTable.doHiliteRow(%guiRowIndex);
    }
    %this.constructDeetsWindow(geDeetsWindow, %item);
    geDeetsLayer.setVisible(1);
    return geDeetsWindow;
}
function geTGF_MyPlaceGuiTable::onRowSelected(%this, %guiRow, %rowIndex, %alreadySelected, %mouseClickCount)
{
    %listName = "myplace";
    geTGF_MyPlaceEitherGuiTable::onRowSelected(%this, %guiRow, %rowIndex, %alreadySelected, %mouseClickCount, %listName);
    return ;
}
function geTGF_OtherPlacesGuiTable::onRowSelected(%this, %guiRow, %rowIndex, %alreadySelected, %mouseClickCount)
{
    %listName = "otherplaces";
    geTGF_MyPlaceEitherGuiTable::onRowSelected(%this, %guiRow, %rowIndex, %alreadySelected, %mouseClickCount, %listName);
    return ;
}
function geTGF_MyPlaceEitherGuiTable::onRowSelected(%this, %guiRow, %rowIndex, %alreadySelected, %mouseClickCount, %listName)
{
    if (%rowIndex == -1)
    {
        error(getScopeName() SPC "- Gui Row" SPC %guiRow SPC "has no Data Row -" SPC getTrace());
        return ;
    }
    %this.makeFirstResponder(1);
    %cellIndex = %this.getDataTable().getColumnIndex("description");
    %itemID = %this.getDataTable().getCellSortValue(%rowIndex, %cellIndex);
    %showDeets = 0;
    if (%mouseClickCount == -1)
    {
        %showDeets = 0;
    }
    else
    {
        if (%mouseClickCount == 0)
        {
            %showDeets = 1;
        }
        else
        {
            if (%mouseClickCount == 1)
            {
                %showDeets = 1;
            }
            else
            {
                if (%mouseClickCount == 2)
                {
                    %showDeets = 1;
                }
                else
                {
                    %showDeets = 0;
                }
            }
        }
    }
    if (%showDeets)
    {
        %item = geTGF.findItem(%listName, "happening", %itemID);
        geTGF.DoDetails("myplace", %item);
    }
    return ;
}
function geTGF_MyPlaceGuiTable::onKeyDown(%this, %modifier, %keyCode)
{
    %modifierStr = %this.getStringFromModifier(%modifier);
    %keyCodeStr = %this.getStringFromKeyCode(%keyCode);
    if (%modifierStr @ %keyCodeStr $= "\t")
    {
    }
    else
    {
        if (%modifierStr @ %keyCodeStr $= "ctrl F")
        {
            geTGF_FriendsFilterBox.makeFirstResponder(1);
            return 1;
        }
    }
    return 0;
}
function geTGF_tabs::Maps_clickMyApartment()
{
    geTGF.closeFully();
    doTeleportToMyApartment();
    return ;
}
function geTGF_tabs::Maps_GetApartmentVURL(%this)
{
    getApartmentVURL("geTGF_tabs::Maps_GotApartmentVURL");
    return ;
}
function geTGF_tabs::Maps_GotApartmentVURL(%status, %vurl)
{
    %active = (%status $= "noOwnedSpace") || !((%vurl $= ""));
    $geTGF::Map_ApartmentVURL = %vurl;
    return ;
}
