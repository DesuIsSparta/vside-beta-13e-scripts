function geTGF_tabs::fillTabHotSpots(%this)
{
    %tabName = "hotspots";
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
    %guiTable = new GuiTableCtrl(geTGF_HotSpotsGuiTable)
    {
        position = "2 20";
        extent = "955 430";
        visible = 1;
        spacing = 2;
    };
    %guiTable.setHeaderCellUniformExtent(22);
    %guiTable.setHeaderMLTextBoxTopMargin(0);
    %guiTable.setChildrenExtents(18);
    %guiTable.setDataTable(%dataTable);
    %tab.GuiTable = %guiTable;
    %tab.add(%guiTable);
    %guiTable.alternativeTextCtrl = new GuiMLTextCtrl()
    {
        profile = "GuiDefaultProfile";
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
    %filterLabel = new GuiMLTextCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "7 473";
        extent = "30 20";
        text = mlStyle("Find:", "tgfTables_DataCell_Text");
    };
    %tab.add(%filterLabel);
    %filterBox = new GuiControl()
    {
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
    %tab.add(%filterBox);
    %invite = new GuiMLTextCtrl()
    {
        position = "250 473";
        extent = "600 30";
        text = mlStyle($MsgCat::invitation["TEXT-TGF-HOTSPOTS"], "tgfTables_Invite");
    };
    %tab.add(%invite);
    %this.refreshTabHotSpots();
    return ;
}
function geTGF_tabs::onShowTabHotSpots(%this)
{
    cancel(geTGF.geTGF_Refresh_Schedule);
    geTGF_Refresh.setActive(1);
    geTGF_Refresh.setVisible(1);
    geTGF_HotSpotsGuiTable.makeFirstResponder(1);
    return ;
}
function geTGF_tabs::refreshTabHotSpots(%this)
{
    geTGF_HotSpotsGuiTable.alternativeTextCtrl.setVisible(1);
    geTGF_HotSpotsGuiTable.alternativeTextCtrl.setText(mlStyle("Fetching..", "tgfTables_DataCell_Text"));
    %request = sendRequest_GetHappeningsInProgress($Player::Name, "geTGF_OnGotDoneOrError_GetHappeningsInProgress");
    return ;
}
function geTGF_OnGotDoneOrError_GetHappeningsInProgress(%request)
{
    if (geTGF_tabs.getCurrentTab().name $= "hotspots")
    {
        cancel(geTGF.geTGF_Refresh_Schedule);
        geTGF_Refresh.setActive(1);
    }
    if (!isObject(%request))
    {
        return ;
    }
    geTGF.clearItemList("hotspots", "happening");
    %listBase = "happenings";
    %count = %request.getValue(%listBase @ "Count");
    %n = 0;
    while (%n < %count)
    {
        %listItem = %listBase @ %n;
        %id = %request.getValue(%listItem @ ".hostUserName");
        %item = geTGF.createNewItem("hotspots", "happening", %id);
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
        %item.subType = %request.getValue(%listItem @ ".type");
        if (!1)
        {
            %item.occupancy = -1;
            %item.friendOccupancy = -1;
            %item.subType = "publicLocationEvent";
            %item.featured = 1;
            %item.eventID = 1234;
        }
        %item.goThereVURL = vurlClearResolution(%item.goThereVURL);
        %n = %n + 1;
    }
    geTGF.removeItemsWithFieldValueFromList("hotspots", "happening", "hostUserName", "The-Manager");
    %itemList = geTGF.getItemList("hotspots", "happening");
    %count = %itemList.count();
    geTGF_HotSpotsDataTable.removeRowsByIndex(0, geTGF_HotSpotsDataTable.getRowCount());
    geTGF_HotSpotsDataTable.addRows(%count);
    geTGF_HotSpotsGuiTable.alternativeTextCtrl.setVisible(%count == 0);
    geTGF_HotSpotsGuiTable.alternativeTextCtrl.setText(mlStyle("More parties and events coming soon!", "tgfTables_DataCell_Text"));
    %n = 0;
    while (%n < %count)
    {
        %item = %itemList.getValue(%n);
        %isFriend = BuddyHudWin.getFriendStatus(%item.hostUserName) $= "friends";
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
        %rowData = "poster" TAB "" TAB %imageURL;
        %rowData = %rowData NL "description" TAB %item.headline TAB geTGF_tabs::hotSpotsTab_formatDescription(%item.headline);
        %rowData = %rowData NL "username" TAB %item.hostUserName TAB geTGF_tabs::hotSpotsTab_formatUserName(%item.hostUserName, %isFriend);
        %rowData = %rowData NL "location" TAB %item.location_areaName TAB geTGF_tabs::hotSpotsTab_formatLocation(%item.location_areaName);
        %rowData = %rowData NL "population" TAB %occupancySortVal TAB "<just:left>" @ geTGF_tabs::hotSpotsTab_formatDescription(%occupancyText);
        %rowData = %rowData NL "friends" TAB %friendOccupancySortVal TAB "<just:left>" @ geTGF_tabs::hotSpotsTab_formatDescription(%friendOccupancyText);
        %rowData = %rowData NL "sameServer" TAB %sameServer TAB "<just:left>[ICON]";
        %rowData = %rowData NL "access" TAB %item.accessMode TAB "<just:left>[ICON]";
        %rowData = %rowData NL "event" TAB %eventValue TAB "<just:right>" @ %eventFmt @ "[ICON]";
        geTGF_HotSpotsDataTable.setRowDataByIndex(%n, %rowData);
        %n = %n + 1;
    }
    geTGF_HotSpotsDataTable.doFilter();
    geTGF_HotSpotsDataTable.updateListeners();
    return ;
}
function geTGF_tabs::hotSpotsTab_formatDescription(%desc)
{
    return mlStyle(%desc, "tgfTables_DataCell_Text");
}
function geTGF_tabs::hotSpotsTab_formatUserName(%name, %isFriend)
{
    if (%isFriend)
    {
        return mlStyle(%name, "tgfTables_DataCell_UserName_Friend");
    }
    return mlStyle(%name, "tgfTables_DataCell_UserName_Normal");
}
function geTGF_tabs::hotSpotsTab_formatLocation(%location)
{
    %text = "";
    %text = %text @ "<tab:30>";
    %text = %text @ DestinationList::GetAreaNameUserFacingNameCityAndBuildingShort(%location, "\t");
    return mlStyle(%text, "tgfTables_DataCell_Text");
}
function geTGF_tabs::hotSpotsTab_formatLocation2(%location)
{
    %text = "";
    %text = %text @ "<tab:30>";
    %text = %text @ DestinationList::GetAreaNameUserFacingNameCityAndBuilding(%location, "\t- ");
    return mlStyle(%text, "tgfTables_DataCell_Text");
}
function geTGF_tabs::hotSpotsTab_formatAccess(%access, %isFriend)
{
    if (%access $= "OPEN")
    {
        return "open";
    }
    else
    {
        if ((%access $= "FRIENDSONLY") && %isFriend)
        {
            return "friendsOnlyOfFriend";
        }
        else
        {
            if (%access $= "FRIENDSONLY")
            {
                return "friendsOnlyOfNonFriend";
            }
            else
            {
                if ((%access $= "PASSWORDPROTECTED") && %isFriend)
                {
                    return "doorcodeOfFriend";
                }
                else
                {
                    if (%access $= "PASSWORDPROTECTED")
                    {
                        return "doorcodeOfNonFriend";
                    }
                    else
                    {
                        return "";
                    }
                }
            }
        }
    }
    return ;
}
function geTGF::hotspots_GetAndOpenDetailsContainer(%this, %item)
{
    %dataRowIndex = geTGF_HotSpotsDataTable.getRowIndexByCriteria("username" TAB %item.hostUserName);
    %guiRowIndex = geTGF_HotSpotsGuiTable.getGuiRowIndexForDataRowIndex(%dataRowIndex);
    if (%guiRowIndex >= 0)
    {
        geTGF_HotSpotsGuiTable.doHiliteRow(%guiRowIndex);
    }
    %this.constructDeetsWindow(geDeetsWindow, %item);
    geDeetsLayer.setVisible(1);
    return geDeetsWindow;
}
function geTGF_HotSpotsGuiTable::onRowSelected(%this, %guiRow, %rowIndex, %unused, %mouseClickCount)
{
    if (%rowIndex == -1)
    {
        error(getScopeName() SPC "- Gui Row" SPC %guiRow SPC "has no Data Row -" SPC getTrace());
        return ;
    }
    %this.makeFirstResponder(1);
    %cellIndex = geTGF_HotSpotsDataTable.getColumnIndex("username");
    %userName = geTGF_HotSpotsDataTable.getCellSortValue(%rowIndex, %cellIndex);
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
        %item = geTGF.findItem("hotspots", "happening", %userName);
        geTGF.DoDetails("hotspots", %item);
    }
    return ;
}
function geTGF_HotSpotsGuiTable::onKeyDown(%this, %modifier, %keyCode)
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
            geTGF_HotSpotsFilterBox.makeFirstResponder(1);
            return 1;
        }
    }
    return 0;
}
function geTGF_HotSpotsFilterBox::onKeyUp(%this, %modifier, %keyCode)
{
    cancel(%this.filterDoItReallySchedule);
    %this.filterDoItReallySchedule = %this.schedule(%this.filterDoItReallyTimeoutMS, "doApplyFilterReally");
    return 0;
}
function geTGF_HotSpotsFilterBox::onKeyDown(%this, %modifier, %keyCode)
{
    %keyCodeStr = %this.getStringFromKeyCode(%keyCode);
    if (%keyCodeStr $= "\t")
    {
        geTGF_HotSpotsGuiTable.makeFirstResponder(1);
        return 1;
    }
    return 0;
}
function geTGF_HotSpotsFilterBox::doApplyFilterReally(%this)
{
    cancel(%this.filterDoItReallySchedule);
    %this.filterDoItReallySchedule = "";
    geTGF_HotSpotsDataTable.setFilterText(%this.getText());
    geTGF_HotSpotsDataTable.updateListeners();
    return ;
}
function geTGF::openForBuildingDirectory(%this, %buildingName)
{
    %buildingName = trim(%buildingName);
    if (%buildingName $= "")
    {
        return ;
    }
    %areaName = Buildings::GetAreaName(%buildingName);
    %fitlerText = DestinationList::GetAreaNameUserFacingNameCityAndBuildingShort(%areaName, "");
    %updateListeners = isObject(geTGF_HotSpotsDataTable);
    geTGF.openToTabName("hotspots");
    geTGF_HotSpotsFilterBox.setText(%fitlerText);
    geTGF_HotSpotsDataTable.setFilterText(%fitlerText);
    if (%updateListeners)
    {
        geTGF_HotSpotsDataTable.doFilter();
        geTGF_HotSpotsDataTable.updateListeners();
    }
    return ;
}
function onDoneOrErrorCallback_GetUserRelations_ForHotSpots(%request)
{
    onDoneOrErrorCallback_GetUserRelations_ProcessOnly(%request);
    if (!isObject(geTGF_HotSpotsDataTable))
    {
        return ;
    }
    %itemList = geTGF.getItemList("hotspots", "happening");
    %count = %itemList.count();
    %n = 0;
    while (%n < %count)
    {
        %item = %itemList.getValue(%n);
        %isFriend = BuddyHudWin.getFriendStatus(%item.hostUserName) $= "friends";
        if (%isFriend)
        {
            %oldRowData = "username" TAB %item.hostUserName;
            %newRowData = "username" TAB %item.hostUserName TAB geTGF_tabs::hotSpotsTab_formatUserName(%item.hostUserName, %isFriend);
            geTGF_HotSpotsDataTable.setRowDataByCriteria(%oldRowData, %newRowData);
        }
        %n = %n + 1;
    }
    geTGF_HotSpotsDataTable.updateListeners();
    return ;
}
