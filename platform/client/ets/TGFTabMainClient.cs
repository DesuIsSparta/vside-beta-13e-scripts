function geTGF_tabs::fillTabMain(%this)
{
    %tabName = "main";
    %tab = %this.getTabWithName(%tabName);
    if (%tab.filled)
    {
        return ;
    }
    %tab.filled = 1;
    %this.fillTabGeneric(%tab);
    %ctrl = new GuiBitmapCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "685 4";
        extent = "275 495";
        bitmap = "platform/client/ui/tgf/tgf_bkgd_rightside";
    };
    %tab.add(%ctrl);
    %ctrl = new GuiBitmapCtrl(geTGF_profilePic)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "left";
        vertSizing = "top";
        position = "708 310";
        extent = "100 100";
        bitmap = "platform/client/ui/tgf/tgf_profile_default";
    };
    %tab.add(%ctrl);
    %ctrl = new GuiBitmapCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "left";
        vertSizing = "top";
        position = "828 310";
        extent = "100 100";
        bitmap = "platform/client/ui/tgf/tgf_events";
    };
    %tab.add(%ctrl);
    %ctrl = new GuiBitmapCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "left";
        vertSizing = "top";
        position = "708 431";
        extent = "100 50";
        bitmap = "platform/client/ui/tgf/tgf_forums";
    };
    %tab.add(%ctrl);
    %ctrl = new GuiBitmapCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "left";
        vertSizing = "top";
        position = "828 431";
        extent = "100 50";
        bitmap = "platform/client/ui/tgf/tgf_faq";
    };
    %tab.add(%ctrl);
    %ctrl = new GuiBitmapCtrl(geTGF_main_BalancesContainer)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "687 8";
        extent = "268 68";
        bitmap = "platform/client/ui/tgf/tgf_main_balances_background";
    };
    %tab.add(%ctrl);
    %ctrl = new GuiBitmapCtrl(MOTDHud)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "687 80";
        extent = "268 228";
        minExtent = "1 1";
        visible = 1;
        bitmap = "platform/client/ui/motdBackground";
    };
    %tab.add(%ctrl);
    if ($ETS::devMode)
    {
        %ctrl = new GuiVariableWidthButtonCtrl(EditMOTDStaffOnly)
        {
            profile = "BracketButtonLt19Profile";
            horizSizing = "left";
            vertSizing = "top";
            position = "691 292";
            extent = "190 21";
            minExtent = "8 2";
            visible = 1;
            command = "toggleMOTDEditDialog();";
            text = "devmod: Edit MOTD/QOTD";
            groupNum = -1;
            buttonType = "PushButton";
        };
        %tab.add(%ctrl);
    }
    %ctrl = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        bitmap = "platform/client/ui/tgf/tgf_main_sectionOutlines";
        position = "0 0";
        extent = "685 498";
    };
    %tab.add(%ctrl);
    %ctrl = new GuiScrollCtrl()
    {
        profile = "DottedScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "71 15";
        extent = "593 345";
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        childMargin = "0 0";
        saneDrag = 1;
        scrollMultiplier = 1;
    };
    %tab.add(%ctrl);
    %ctrl = new GuiControl(geTGF_main_people)
    {
        profile = "EtsNonModalProfile";
        position = "40 367";
        extent = "625 129";
    };
    %tab.add(%ctrl);
    %ctrl = new GuiVariableWidthButtonCtrl()
    {
        profile = "BracketButtonLt19Profile";
        position = "5 86";
        extent = "50 19";
        command = "geTGF.main_testData_happenings();";
        text = "test";
        visible = 0;
    };
    %tab.add(%ctrl);
    return ;
}
function geTGF_tabs::onShowTabMain(%this)
{
    cancel(geTGF.geTGF_Refresh_Schedule);
    geTGF_Refresh.setActive(1);
    geTGF_Refresh.setVisible(1);
    return ;
}
function geTGF::main_testData_happenings(%this)
{
    %requestName = "request_GetMainHappenings";
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setName(%requestName);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/finder/GetMainHappenings";
    %request.setURL(%url);
    %request.addUserAndToken($Player::Name);
    %request.putValue("status", "success");
    %n = 0;
    %request.putValue("happenings" @ %n @ ".accessMode", "open");
    %request.putValue("happenings" @ %n @ ".baseImageURL", "https://s-website.doppelganger.com/photoservice/e5b50410-76cc-4627-b0bb-5508b311436d");
    %request.putValue("happenings" @ %n @ ".customSpaceId", "dababydoll");
    %request.putValue("happenings" @ %n @ ".eventId", 1164);
    %request.putValue("happenings" @ %n @ ".featured", "true");
    %request.putValue("happenings" @ %n @ ".friendOccupancy", -1);
    %request.putValue("happenings" @ %n @ ".goThereVURL", "vside:/location/lga/ShoppingSpawns_modpodz");
    %request.putValue("happenings" @ %n @ ".headline", "HOWDY!");
    %request.putValue("happenings" @ %n @ ".hostUserName", "dababydoll");
    %request.putValue("happenings" @ %n @ ".initialized", "true");
    %request.putValue("happenings" @ %n @ ".location.areaName", "lga");
    %request.putValue("happenings" @ %n @ ".location.buildingName", "");
    %request.putValue("happenings" @ %n @ ".location.serverName", "LaGenoaAiresNorth");
    %request.putValue("happenings" @ %n @ ".occupancy", -1);
    %request.putValue("happenings" @ %n @ ".type", "publicLocationEvent");
    %n = %n + 1;
    %request.putValue("happenings" @ %n @ ".accessMode", "open");
    %request.putValue("happenings" @ %n @ ".baseImageURL", "https://s-website.doppelganger.com/photoservice/e5b50410-76cc-4627-b0bb-5508b311436d");
    %request.putValue("happenings" @ %n @ ".customSpaceId", "dababydoll");
    %request.putValue("happenings" @ %n @ ".eventId", 1164);
    %request.putValue("happenings" @ %n @ ".featured", "true");
    %request.putValue("happenings" @ %n @ ".friendOccupancy", 3);
    %request.putValue("happenings" @ %n @ ".goThereVURL", "vside:/location/lga/ShoppingSpawns_modpodz");
    %request.putValue("happenings" @ %n @ ".headline", "HOWDY!");
    %request.putValue("happenings" @ %n @ ".hostUserName", "dababydoll");
    %request.putValue("happenings" @ %n @ ".initialized", "true");
    %request.putValue("happenings" @ %n @ ".location.areaName", "lga");
    %request.putValue("happenings" @ %n @ ".location.buildingName", "");
    %request.putValue("happenings" @ %n @ ".location.serverName", "LaGenoaAiresNorth");
    %request.putValue("happenings" @ %n @ ".occupancy", 17);
    %request.putValue("happenings" @ %n @ ".type", "aptEvent");
    %n = %n + 1;
    %request.putValue("happenings" @ %n @ ".accessMode", "open");
    %request.putValue("happenings" @ %n @ ".baseImageURL", "https://s-website.doppelganger.com/photoservice/e5b50410-76cc-4627-b0bb-5508b311436d");
    %request.putValue("happenings" @ %n @ ".customSpaceId", "dababydoll");
    %request.putValue("happenings" @ %n @ ".eventId", 1164);
    %request.putValue("happenings" @ %n @ ".featured", "false");
    %request.putValue("happenings" @ %n @ ".friendOccupancy", 3);
    %request.putValue("happenings" @ %n @ ".goThereVURL", "vside:/location/lga/ShoppingSpawns_modpodz");
    %request.putValue("happenings" @ %n @ ".headline", "HOWDY!");
    %request.putValue("happenings" @ %n @ ".hostUserName", "dababydoll");
    %request.putValue("happenings" @ %n @ ".initialized", "true");
    %request.putValue("happenings" @ %n @ ".location.areaName", "lga");
    %request.putValue("happenings" @ %n @ ".location.buildingName", "");
    %request.putValue("happenings" @ %n @ ".location.serverName", "LaGenoaAiresNorth");
    %request.putValue("happenings" @ %n @ ".occupancy", 17);
    %request.putValue("happenings" @ %n @ ".type", "aptEvent");
    %n = %n + 1;
    %request.putValue("happenings" @ %n @ ".accessMode", "open");
    %request.putValue("happenings" @ %n @ ".baseImageURL", "");
    %request.putValue("happenings" @ %n @ ".customSpaceId", "dababydoll");
    %request.putValue("happenings" @ %n @ ".eventId", "");
    %request.putValue("happenings" @ %n @ ".featured", "false");
    %request.putValue("happenings" @ %n @ ".friendOccupancy", 3);
    %request.putValue("happenings" @ %n @ ".goThereVURL", "vside:/location/lga/ShoppingSpawns_modpodz");
    %request.putValue("happenings" @ %n @ ".headline", "HOWDY!");
    %request.putValue("happenings" @ %n @ ".hostUserName", "dababydoll");
    %request.putValue("happenings" @ %n @ ".initialized", "true");
    %request.putValue("happenings" @ %n @ ".location.areaName", "lga");
    %request.putValue("happenings" @ %n @ ".location.buildingName", "");
    %request.putValue("happenings" @ %n @ ".location.serverName", "LaGenoaAiresNorth");
    %request.putValue("happenings" @ %n @ ".occupancy", 17);
    %request.putValue("happenings" @ %n @ ".type", "apt");
    %n = %n + 1;
    %request.putValue("happeningsCount", %n);
    %statusTextCtrl = geTGF_main_happenings.getParent().child("emptyText", 1);
    %text = %statusTextCtrl.textLoading;
    %statusTextCtrl.setText(%text);
    schedule(500, 0, "onDoneOrErrorCallback_GetMainHappenings", %request);
    return ;
}
function geTGF_tabs::refreshTabMain(%this)
{
    %tabName = "main";
    %tab = %this.getTabWithName(%tabName);
    %this.refreshQOTD();
    if (!(geTGF_tabs.previousProfileName $= $Player::Name))
    {
        geTGF_profilePic.setBitmap("platform/client/ui/tgf/tgf_profile_default");
        geTGF_profilePic.fitInParentAsBitmap();
    }
    geTGF_tabs.previousProfileName = $Player::Name;
    %url = $Net::AvatarURL @ urlEncode($Player::Name) @ "?size=M";
    geTGF_profilePic.downloadAndApplyBitmap(%url);
    geTGF.main_sendRequests();
    return ;
}
function geTGF::main_sendRequests(%this)
{
    %statusTextCtrl = geTGF_main_people.child("emptyText", 1);
    %text = %statusTextCtrl.textLoading;
    %statusTextCtrl.setText(%text);
    sendRequest_GetOnlineUsers(60, "onDoneOrErrorCallback_GetOnlineUsers");
    geTGF_main_people_locationsText.setText("");
    %statusTextCtrl = geTGF_main_happenings.getParent().child("emptyText", 1);
    %text = %statusTextCtrl.textLoading;
    %statusTextCtrl.setText(%text);
    sendRequest_GetMainHappenings(9, "onDoneOrErrorCallback_GetMainHappenings");
    WorldMap.refresh();
    return ;
}
function onDoneOrErrorCallback_GetMainHappenings(%request, %unused)
{
    if (geTGF_tabs.getCurrentTab().name $= "main")
    {
        cancel(geTGF.geTGF_Refresh_Schedule);
        geTGF_Refresh.setActive(1);
    }
    log("network", "debug", getScopeName() SPC "- url =" SPC %request.getURL());
    %itemType = "happening";
    geTGF.clearItemList("main", %itemType);
    if (!%request.checkSuccess())
    {
        geTGF.main_onGotDataOfType(%itemType);
        return ;
    }
    %listBase = "happenings";
    %count = %request.getResult(%listBase @ "Count");
    %n = 0;
    while (%n < %count)
    {
        %listItem = %listBase @ %n;
        %id = %request.getResult(%listItem @ ".hostUserName");
        %item = geTGF.createNewItem("main", %itemType, %id);
        %request.copyListValueIntoObject(%item, %listItem, "accessMode");
        %request.copyListValueIntoObject(%item, %listItem, "occupancy");
        %request.copyListValueIntoObject(%item, %listItem, "baseImageURL");
        %request.copyListValueIntoObject(%item, %listItem, "eventId");
        %request.copyListValueIntoObject(%item, %listItem, "featured");
        %request.copyListValueIntoObject(%item, %listItem, "friendOccupancy");
        %request.copyListValueIntoObject(%item, %listItem, "goThereVURL");
        %request.copyListValueIntoObject(%item, %listItem, "headline");
        %request.copyListValueIntoObject(%item, %listItem, "hostUserName");
        %request.copyListValueIntoObject(%item, %listItem, "moreInfoURL");
        %request.copyListValueIntoObject(%item, %listItem, "apt");
        %request.copyListValueIntoObject(%item, %listItem, "location.areaName");
        %request.copyListValueIntoObject(%item, %listItem, "location.buildingName");
        %request.copyListValueIntoObject(%item, %listItem, "location.serverName");
        %item.subType = %request.getResult(%listItem @ ".type");
        %item.goThereVURL = vurlClearResolution(%item.goThereVURL);
        %n = %n + 1;
    }
    geTGF.removeItemsWithFieldValueFromList("main", %itemType, "hostUserName", "The-Manager");
    geTGF.main_onGotDataOfType(%itemType);
    return ;
}
function onDoneOrErrorCallback_GetOnlineFriends_Main(%request)
{
    if (geTGF_tabs.getCurrentTab().name $= "main")
    {
        cancel(geTGF.geTGF_Refresh_Schedule);
        geTGF_Refresh.setActive(1);
    }
    log("network", "debug", getScopeName() SPC "- url =" SPC %request.getURL());
    %itemType = "person";
    geTGF.clearItemList("main", %itemType);
    if (!%request.checkSuccess())
    {
        geTGF.main_onGotDataOfType(%itemType);
        return ;
    }
    %listBase = "friends";
    %count = %request.getValue(%listBase @ "Count");
    %n = 0;
    while (%n < %count)
    {
        %listItem = %listBase @ %n;
        %id = %request.getValue(%listItem @ ".userName");
        %item = geTGF.createNewItem("main", %itemType, %id);
        %request.copyListValueIntoObject(%item, %listItem, "age");
        %request.copyListValueIntoObject(%item, %listItem, "currentActivities");
        %request.copyListValueIntoObject(%item, %listItem, "currentLocation.areaName");
        %request.copyListValueIntoObject(%item, %listItem, "currentLocation.buildingName");
        %request.copyListValueIntoObject(%item, %listItem, "currentLocation.serverName");
        %request.copyListValueIntoObject(%item, %listItem, "gender");
        %request.copyListValueIntoObject(%item, %listItem, "headline");
        %request.copyListValueIntoObject(%item, %listItem, "homeLocation.areaName");
        %request.copyListValueIntoObject(%item, %listItem, "homeLocation.buildingName");
        %request.copyListValueIntoObject(%item, %listItem, "homeLocation.serverName");
        %request.copyListValueIntoObject(%item, %listItem, "ignored");
        %request.copyListValueIntoObject(%item, %listItem, "locationIRL");
        %request.copyListValueIntoObject(%item, %listItem, "onlineStatus");
        %request.copyListValueIntoObject(%item, %listItem, "permissionsMask");
        %request.copyListValueIntoObject(%item, %listItem, "profileViewCount");
        %request.copyListValueIntoObject(%item, %listItem, "profileViewRanking");
        %request.copyListValueIntoObject(%item, %listItem, "relationType");
        %request.copyListValueIntoObject(%item, %listItem, "score");
        %request.copyListValueIntoObject(%item, %listItem, "userName");
        %item.goThereVURL = "vside:/user/" @ %item.userName;
        %n = %n + 1;
    }
    geTGF.main_onGotDataOfType(%itemType);
    return ;
}
function onDoneOrErrorCallback_GetOnlineUsers(%request)
{
    log("network", "debug", getScopeName() SPC "- url =" SPC %request.getURL());
    %statusTextCtrl = geTGF_main_people.child("emptyText", 1);
    %statusTextCtrl.setText("");
    %itemType = "person";
    geTGF.clearItemList("main", %itemType);
    if (!%request.checkSuccess())
    {
        geTGF.main_onGotDataOfType(%itemType);
        return ;
    }
    %listBase = "user";
    %count = %request.getValue(%listBase @ "Count");
    %n = 0;
    while (%n < %count)
    {
        %listItem = %listBase @ %n;
        %id = %request.getValue(%listItem @ ".userName");
        %item = geTGF.createNewItem("main", %itemType, %id);
        %request.copyListValueIntoObject(%item, %listItem, "age");
        %request.copyListValueIntoObject(%item, %listItem, "currentActivities");
        %request.copyListValueIntoObject(%item, %listItem, "currentLocation.areaName");
        %request.copyListValueIntoObject(%item, %listItem, "currentLocation.buildingName");
        %request.copyListValueIntoObject(%item, %listItem, "currentLocation.serverName");
        %request.copyListValueIntoObject(%item, %listItem, "gender");
        %request.copyListValueIntoObject(%item, %listItem, "headline");
        %request.copyListValueIntoObject(%item, %listItem, "homeLocation.areaName");
        %request.copyListValueIntoObject(%item, %listItem, "homeLocation.buildingName");
        %request.copyListValueIntoObject(%item, %listItem, "homeLocation.serverName");
        %request.copyListValueIntoObject(%item, %listItem, "ignored");
        %request.copyListValueIntoObject(%item, %listItem, "locationIRL");
        %request.copyListValueIntoObject(%item, %listItem, "onlineStatus");
        %request.copyListValueIntoObject(%item, %listItem, "permissionsMask");
        %request.copyListValueIntoObject(%item, %listItem, "profileViewCount");
        %request.copyListValueIntoObject(%item, %listItem, "profileViewRanking");
        %request.copyListValueIntoObject(%item, %listItem, "relationType");
        %request.copyListValueIntoObject(%item, %listItem, "score");
        %request.copyListValueIntoObject(%item, %listItem, "userName");
        %item.goThereVURL = "vside:/user/" @ %item.userName;
        %n = %n + 1;
    }
    geTGF.main_onGotDataOfType(%itemType);
    return ;
}
function onDoneOrErrorCallback_GetMainVenues(%request)
{
    if (geTGF_tabs.getCurrentTab().name $= "main")
    {
        cancel(geTGF.geTGF_Refresh_Schedule);
        geTGF_Refresh.setActive(1);
    }
    log("network", "debug", "FAKE REQUEST: " @ getScopeName() SPC "- url =" SPC %request.getURL());
    %request.putValue("statusMsg", "fakeSuccess");
    %request.timeFinish = getSimTime();
    %itemType = "venue";
    geTGF.clearItemList("main", %itemType);
    if (!%request.checkSuccess())
    {
        geTGF.main_onGotDataOfType(%itemType);
        return ;
    }
    %listBase = "venues";
    %count = %request.getValue(%listBase @ "Count");
    %n = 0;
    while (%n < %count)
    {
        %listItem = %listBase @ %n;
        %id = %request.getValue(%listItem @ ".name");
        %item = geTGF.createNewItem("main", %itemType, %id);
        %request.copyListValueIntoObject(%item, %listItem, "codeName");
        %request.copyListValueIntoObject(%item, %listItem, "fullName");
        %request.copyListValueIntoObject(%item, %listItem, "location.areaName");
        %request.copyListValueIntoObject(%item, %listItem, "location.buildingName");
        %request.copyListValueIntoObject(%item, %listItem, "location.serverName");
        %request.copyListValueIntoObject(%item, %listItem, "headline");
        %request.copyListValueIntoObject(%item, %listItem, "occupancy");
        %request.copyListValueIntoObject(%item, %listItem, "friendOccupancy");
        %request.copyListValueIntoObject(%item, %listItem, "goThereVURL");
        %n = %n + 1;
    }
    geTGF.main_onGotDataOfType(%itemType);
    return ;
}
function geTGF::main_onGotDataOfType(%this, %type)
{
    if (%type $= "happening")
    {
        %control = geTGF_main_happenings;
    }
    else
    {
        if (%type $= "person")
        {
            %control = geTGF_main_people;
            %control.onGotData();
            return ;
        }
        else
        {
            error("unknown type:" SPC %type SPC getTrace());
            return ;
        }
    }
    %list = %this.getItemList("main", %type);
    %num = %list.count();
    %control.setNumChildren(%num);
    %n = 0;
    while (%n < %num)
    {
        %cell = %control.getObject(%n);
        %item = %list.getValue(%n);
        %cell.Item = %item;
        %cell.setProfile(ETSDarkBoxNonModalProfile);
        %cell.isInviteFriendsButton = 0;
        %control.updateCellFromItsItem(%cell);
        %n = %n + 1;
    }
    %statusTextCtrl = %control.getParent().child("emptyText");
    if (%num <= 0)
    {
        %text = %statusTextCtrl.textNothing;
    }
    else
    {
        %text = "";
    }
    %statusTextCtrl.setText(%text);
    return ;
}
function geTGF_main_happenings::updateCellFromItsItem(%this, %cell)
{
    %item = %cell.Item;
    %cell.deleteMembers();
    %cont = new GuiControl()
    {
        profile = "EtsNonModalProfile";
        extent = getWord(%cell.getExtent(), 0) - 2 SPC getWord(%cell.getExtent(), 1) - 2;
        position = "1 1";
        horizSizing = "width";
        vertSizing = "top";
    };
    %cell.add(%cont);
    %w = getWord(%cell.getParent().childrenExtent, 0);
    %h = getWord(%cell.getParent().childrenExtent, 1);
    if (%item.eventID $= "")
    {
        %w = %h;
    }
    %ctrl = new GuiBitmapCtrl()
    {
        profile = "EtsNonModalProfile";
        extent = %w SPC %h;
        position = "0 0";
        horizSizing = "width";
        vertSizing = "top";
        fitInParentAlign = 1;
    };
    %cont.add(%ctrl);
    %url = %item.baseImageURL;
    if (%url $= "")
    {
        %url = $Net::BuildDirPhotoURL @ urlEncode(%item.hostUserName);
    }
    %url = %url @ "?size=" @ !(%item.eventID $= "") ? "M" : "M";
    %ctrl.setBitmap("platform/client/ui/tgf/tgf_profile_default");
    %ctrl.fitInParentAsBitmap();
    %ctrl.downloadAndApplyBitmap(%url);
    %tmp = 20;
    %ctrl = new GuiControl()
    {
        profile = "EtsDarkBorderlessBoxProfile";
        extent = getWord(%cont.getExtent(), 0) SPC %tmp;
        position = 0 SPC getWord(%cont.getExtent(), 1) - %tmp;
        horizSizing = "width";
        vertSizing = "top";
    };
    %cont.add(%ctrl);
    %ctrl = new GuiBitmapButtonCtrl()
    {
        profile = "GuiDefaultProfile";
        position = "-1 -1";
        extent = %cell.getExtent();
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_190x109";
        command = "geTGF.main_OnClickItem(\"" @ %item @ "\");";
    };
    %cont.add(%ctrl);
    %ctrl = new GuiMLTextCtrl()
    {
        profile = "ETSNonModalProfile";
        position = 2 SPC getWord(%cont.getExtent(), 1) - 16;
        extent = getWord(%cont.getExtent(), 0) - 9 SPC 20;
        text = mlStyle("<just:left>" @ %item.headline @ "<just:right>", "tgfItem_headline");
        horizSizing = "width";
        vertSizing = "top";
        lineSpacing = 0;
    };
    %cont.add(%ctrl);
    if (%item.occupancy == -1)
    {
        %occupancyText = "";
    }
    else
    {
        if (%item.occupancy == 0)
        {
            %occupancyText = "(empty) ";
        }
        else
        {
            %occupancyText = "<b>" @ %item.occupancy @ " P ";
        }
    }
    %ctrl = new GuiMLTextCtrl()
    {
        profile = "ETSNonModalProfile";
        position = 0 SPC getWord(%cont.getExtent(), 1) - 38;
        extent = getWord(%cont.getExtent(), 0) - 9 SPC 20;
        text = mlStyle("<just:left>" @ %item.hostUserName @ "<just:right><font:Arial:14>" @ %occupancyText, "tgfItem_host");
        horizSizing = "width";
        vertSizing = "top";
    };
    %cont.add(%ctrl);
    if (!(%item.eventID $= ""))
    {
        %bitmap = geTGF.getEventTypePostItTagBitmap(%item);
        %ctrl = new GuiBitmapCtrl()
        {
            bitmap = %bitmap;
            profile = "EtsNonModalProfile";
            extent = "82 19";
            position = (getWord(%cont.getExtent(), 0) - 82) - 0 SPC 0;
            horizSizing = "left";
            vertSizing = "bottom";
        };
        %cont.add(%ctrl);
    }
    return ;
}
function geTGF_main_people::onGotData(%this)
{
    %list = geTGF.getItemList("main", "person");
    %num = %list.size();
    %statusTextCtrl = %this.child("emptyText");
    %text = %num <= 0 ? %statusTextCtrl : "";
    %statusTextCtrl.setText(%text);
    if (!isObject(geTGFGoRound))
    {
        geTGF_main_PeopleGoRound_Container.add(newTGFGoRound(geTGFGoRound));
        geTGFGoRound.reparentSameSize(geTGF_main_PeopleGoRound_Container, "");
        geTGFGoRound.mDeetsMinWidth = 0;
        geTGFGoRound.mLilThumbPadding = 5;
        geTGFGoRound.mLilThumbHeight = geTGFGoRound.calcMaximumThumbHeight();
        geTGFGoRound.mTickPeriodMS = 4500;
        geTGFGoRound.rebuild();
    }
    geTGFGoRound.setItemList(%list);
    %this.tryUpdateWorldmapSummaries();
    return ;
}
function geTGF_main_people::tryUpdateWorldmapSummaries(%this)
{
    geTGF_main_people_locationsText.setTextWithStyle("");
    if (isObject(MapRequest))
    {
        %this.schedule(1000, "tryUpdateWorldmapSummaries");
    }
    %areaNames = "lga nv rj pvt";
    %total = 0;
    %text = "";
    %delim = "";
    %n = 0;
    while (%n < getWordCount(%areaNames))
    {
        %areaName = getWord(%areaNames, %n);
        if (!(%areaName $= "pvt"))
        {
            %userCount = WorldAreaSummaries.get(%areaName).occupancy;
        }
        else
        {
            %userCount = WorldAreaSummaries.totalOccupancy[%areaName];
        }
        %link = "TGF_GOTO" SPC %areaName;
        if (%areaName $= "pvt")
        {
            %areaName = "Personal Spaces";
        }
        else
        {
            %areaName = DestinationList::GetAreaNameUserFacingName(DestinationList::GetAreaNameCity(%areaName));
        }
        %text = %text @ %delim @ "<spush><just:left><a:gamelink:" @ %link @ ">" @ %areaName @ "" TAB "<just:right>" @ commaify(%userCount) @ "</a><spop>";
        %total = %total + %userCount;
        %delim = "<br>";
        %n = %n + 1;
    }
    %withS = %total == 1 ? "" : "s";
    %text = "<spush><just:center><font:arial:16>" @ commaify(%total) @ " vSider" @ %withS @ " In-World:<spop><br>" @ %text;
    %text = "<tab:100>" @ %text;
    geTGF_main_people_locationsText.setTextWithStyle(%text);
    return ;
}
function geTGF_main_people_locationsText::onURL(%this, %url)
{
    %cmd = firstWord(%url);
    if (!(%cmd $= "TGF_GOTO"))
    {
        error(getScopeName() SPC "- unknown command" SPC %cmd SPC getTrace());
        return ;
    }
    %areaName = restWords(%url);
    if (%areaName $= "pvt")
    {
        geTGF.openToTabName("HOTSPOTS");
    }
    else
    {
        if (%areaName $= "lga")
        {
            geTGF.openToTabName("MAP");
            WorldMap.getCityButton(%areaName, 1, 0).performClick();
        }
        else
        {
            if (%areaName $= "nv")
            {
                geTGF.openToTabName("MAP");
                WorldMap.getCityButton(%areaName, 1, 0).performClick();
            }
            else
            {
                if (%areaName $= "rj")
                {
                    geTGF.openToTabName("MAP");
                    WorldMap.getCityButton(%areaName, 1, 0).performClick();
                }
                else
                {
                    error(getScopeName() SPC "- unknown areaname" SPC %areaName SPC getTrace());
                }
            }
        }
    }
    return ;
}
function encodeMOTDString(%text)
{
    %text = strreplace(%text, "\n", "<br>");
    return urlEncode(%text);
}
function decodeMOTDString(%text)
{
    %text = strreplace(%text, "<br>", "\n");
    return %text;
}
function geTGF_tabs::refreshMOTD(%this)
{
    if (isObject(MOTDRequest))
    {
        MOTDRequest.delete();
    }
    new ManagerRequest(MOTDRequest);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(MOTDRequest);
    }
    %url = $Net::ClientServiceURL @ "/GlobalMessage";
    %url = %url @ "?user=" @ urlEncode($Player::Name);
    %url = %url @ "&token=" @ urlEncode($Token);
    %url = %url @ "&type=" @ urlEncode("motd");
    log("communication", "debug", "sending request to get current motd: " @ %url);
    MOTDRequest.setURL(%url);
    MOTDRequest.start();
    return ;
}
function MOTDRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    if (!(%status $= "success"))
    {
        geTGF_tabs.onMOTDPostFailed();
        log("communication", "warn", "MOTDRequest::onDone status: " @ %status);
    }
    else
    {
        %text = %this.getValue("message");
        if (!(%text $= ""))
        {
            %text = decodeMOTDString(%text);
            MOTDText.setText(%text);
            MOTDText.qotdID = "";
        }
        else
        {
            geTGF_tabs.onMOTDPostFailed();
        }
    }
    return ;
}
function geTGF_tabs::refreshQOTD(%this)
{
    if (isObject(QOTDRequest))
    {
        QOTDRequest.delete();
    }
    new ManagerRequest(QOTDRequest);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(QOTDRequest);
    }
    %url = $Net::ClientServiceURL @ "/GlobalMessage";
    %url = %url @ "?user=" @ urlEncode($Player::Name);
    %url = %url @ "&token=" @ urlEncode($Token);
    %url = %url @ "&type=" @ urlEncode("qotd");
    log("communication", "debug", "sending request to get current qotd: " @ %url);
    QOTDRequest.setURL(%url);
    QOTDRequest.start();
    return ;
}
function QOTDRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    %text = "";
    if (%status $= "success")
    {
        %text = %this.getValue("message");
        %text = decodeMOTDString(%text);
        %qID = trim(getWords(%text, 0, 0));
        %text = getWords(%text, 1);
        %alreadyAnswered = hasSubString($UserPref::QOTD::answered, %qID);
        if (%alreadyAnswered)
        {
            echoDebug(getScopeName() SPC "- already answered" SPC %qID);
            %text = "";
        }
    }
    if (!(%text $= ""))
    {
        MOTDText.setText(%text);
        MOTDText.qotdID = %qID;
    }
    else
    {
        geTGF_tabs.refreshMOTD();
    }
    return ;
}
function MOTDText::onURL(%this, %url)
{
    if (firstWord(%url) $= "gamelink")
    {
        %url = restWords(%url);
    }
    if (strstr(%url, "answer:") != 0)
    {
        Parent::onURL(%this, %url);
        return ;
    }
    %answer = getSubStr(%url, strlen("answer:"), 100000);
    %trackURL = "";
    %trackURL = %trackURL @ "/client/qotd";
    %trackURL = %trackURL @ "/" @ %this.qotdID;
    %trackURL = %trackURL @ "/" @ %answer;
    %analytic = getAnalytic();
    %analytic.trackPageView(%trackURL);
    %this.setText($MsgCat::QOTD["thanks"]);
    geTGF_tabs.schedule(1000, "refreshMOTD");
    $UserPref::QOTD::answered = $UserPref::QOTD::answered SPC %this.qotdID;
    return ;
}
function geTGF_tabs::onMOTDPostFailed(%this)
{
    MOTDText.setText("");
    %fileName = "projects/common/defaultMOTD.txt";
    %fo = new FileObject();
    if (%fo.openForRead(%fileName))
    {
        %text = "";
        while (!%fo.isEOF())
        {
            %text = %text @ %fo.readLine() @ "\n";
        }
        MOTDText.setText(%text);
    }
    else
    {
        error("unable to find default motd: " @ %fileName);
    }
    %fo.delete();
    return ;
}
function geTGF::onProfile(%this)
{
    doEditProfile();
    return ;
}
function geTGF::onEventsCalendar(%this)
{
    openEventsURL();
    return ;
}
function geTGF::onForums(%this)
{
    openForumsURL();
    return ;
}
function geTGF::onFAQ(%this)
{
    openHelpURL();
    return ;
}
function geTGF::main_OnClickItem(%this, %item)
{
    %this.DoDetails("main", %item);
    return ;
}
function geTGF::main_GetAndOpenDetailsContainer(%this, %item)
{
    %this.constructDeetsWindow(geDeetsWindow, %item);
    geDeetsLayer.setVisible(1);
    return geDeetsWindow;
}
