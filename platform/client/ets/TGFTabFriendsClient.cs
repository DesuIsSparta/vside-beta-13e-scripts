function geTGF_tabs::fillTabFriends(%this)
{
    %tabName = "friends";
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
    %dataTable = new DataTable(geTGF_FriendsDataTable);
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
    %guiTable = new GuiTableCtrl(geTGF_FriendsGuiTable)
    {
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
        text = mlStyle($MsgCat::invitation["TEXT-TGF-FRIENDS"], "tgfTables_Invite");
    };
    %tab.add(%invite);
    %this.refreshTabFriends();
    return ;
}
function geTGF_tabs::onShowTabFriends(%this)
{
    cancel(geTGF.geTGF_Refresh_Schedule);
    geTGF_Refresh.setActive(1);
    geTGF_Refresh.setVisible(1);
    geTGF_FriendsGuiTable.makeFirstResponder(1);
    return ;
}
function geTGF_tabs::refreshTabFriends(%this)
{
    geTGF_FriendsGuiTable.alternativeTextCtrl.setVisible(1);
    geTGF_FriendsGuiTable.alternativeTextCtrl.setText(mlStyle("Fetching..", "tgfTables_DataCell_Text"));
    sendRequest_GetOnlineFriends("", "", "geTGF_OnGotDoneOrError_GetOnlineFriends");
    return ;
}
function geTGF_OnGotDoneOrError_GetOnlineFriends(%request)
{
    if (geTGF_tabs.getCurrentTab().name $= "friends")
    {
        cancel(geTGF.geTGF_Refresh_Schedule);
        geTGF_Refresh.setActive(1);
    }
    if (!isObject(%request))
    {
        return ;
    }
    geTGF.clearItemList("friends", "person");
    %listBase = "friends";
    %count = %request.getValue(%listBase @ "Count");
    %n = 0;
    while (%n < %count)
    {
        %friendLabel = "friends" @ %n;
        %id = %request.getValue(%friendLabel @ ".userName");
        %item = geTGF.createNewItem("friends", "person", %id);
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
        %n = %n + 1;
    }
    %itemList = geTGF.getItemList("friends", "person");
    %count = %itemList.count();
    geTGF_FriendsDataTable.removeRowsByIndex(0, geTGF_FriendsDataTable.getRowCount());
    geTGF_FriendsDataTable.addRows(%count);
    geTGF_FriendsGuiTable.alternativeTextCtrl.setVisible(%count == 0);
    geTGF_FriendsGuiTable.alternativeTextCtrl.setText(mlStyle("You\'re the first one here.  There are lots of new friends to meet on vSide.", "tgfTables_DataCell_Text"));
    %uaw = getUserActivityMgr();
    %n = 0;
    while (%n < %count)
    {
        %item = %itemList.getValue(%n);
        %isFriend = %item.relationType $= "friend";
        %sameServer = !(($ServerName $= "")) && (%item.currentLocation_serverName $= $ServerName) ? "true" : "false";
        %activities = %uaw.getActivitiesMLText(%item.currentActivities, 3);
        %rowData = "avatar" TAB "" TAB "platform/client/ui/tgf/tgf_profile_default";
        %rowData = %rowData NL "username" TAB %item.userName TAB geTGF_tabs::friendsTab_formatUserName(%item.userName, %isFriend);
        %rowData = %rowData NL "location" TAB %item.currentLocation_areaName TAB geTGF_tabs::friendsTab_formatLocation(%item.currentLocation_areaName);
        %rowData = %rowData NL "sameServer" TAB %sameServer TAB "[ICON]";
        %rowData = %rowData NL "activities" TAB %activities TAB %activities;
        %rowData = %rowData NL "statusmsg" TAB %item.headline TAB geTGF_tabs::friendsTab_formatStatusMsg(%item.headline);
        geTGF_FriendsDataTable.setRowDataByIndex(%n, %rowData);
        %n = %n + 1;
    }
    geTGF_FriendsDataTable.updateListeners();
    %n = 0;
    while (%n < %count)
    {
        %item = %itemList.getValue(%n);
        %avatar = $Net::AvatarURL @ urlEncode(%item.userName) @ "?size=S";
        %rowData = "avatar" TAB "" TAB %avatar;
        geTGF_FriendsDataTable.setRowDataByIndex(%n, %rowData);
        %n = %n + 1;
    }
    geTGF_FriendsDataTable.doFilter();
    geTGF_FriendsDataTable.updateListeners();
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
function geTGF::friends_GetAndOpenDetailsContainer(%this, %item)
{
    %dataRowIndex = geTGF_FriendsDataTable.getRowIndexByCriteria("username" TAB %item.userName);
    %guiRowIndex = geTGF_FriendsGuiTable.getGuiRowIndexForDataRowIndex(%dataRowIndex);
    if (%guiRowIndex >= 0)
    {
        geTGF_FriendsGuiTable.doHiliteRow(%guiRowIndex);
    }
    %this.constructDeetsWindow(geDeetsWindow, %item);
    geDeetsLayer.setVisible(1);
    return geDeetsWindow;
}
function geTGF_FriendsGuiTable::onRowSelected(%this, %guiRow, %rowIndex, %unused, %mouseClickCount)
{
    if (%rowIndex == -1)
    {
        error(getScopeName() SPC "- Gui Row" SPC %guiRow SPC "has no Data Row -" SPC getTrace());
        return ;
    }
    %this.makeFirstResponder(1);
    %cellIndex = geTGF_FriendsDataTable.getColumnIndex("username");
    %userName = geTGF_FriendsDataTable.getCellSortValue(%rowIndex, %cellIndex);
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
        %item = geTGF.findItem("friends", "person", %userName);
        geTGF.DoDetails("friends", %item);
    }
    return ;
}
function geTGF_FriendsGuiTable::onKeyDown(%this, %modifier, %keyCode)
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
function geTGF_FriendsFilterBox::onKeyUp(%this, %modifier, %keyCode)
{
    cancel(%this.filterDoItReallySchedule);
    %this.filterDoItReallySchedule = %this.schedule(%this.filterDoItReallyTimeoutMS, "doApplyFilterReally");
    return 0;
}
function geTGF_FriendsFilterBox::onKeyDown(%this, %modifier, %keyCode)
{
    %keyCodeStr = %this.getStringFromKeyCode(%keyCode);
    if (%keyCodeStr $= "\t")
    {
        geTGF_FriendsGuiTable.makeFirstResponder(1);
        return 1;
    }
    return 0;
}
function geTGF_FriendsFilterBox::doApplyFilterReally(%this)
{
    cancel(%this.filterDoItReallySchedule);
    %this.filterDoItReallySchedule = "";
    geTGF_FriendsDataTable.setFilterText(%this.getText());
    geTGF_FriendsDataTable.updateListeners();
    return ;
}
