function geTGF_tabs::fillTabMain(%this) {
    %tabName = "main";
    %tab = %tabName.getTabWithName(%this);
    if (%tab.filled) {
        return;
    }
    %tab.filled = 1;
    %tab.fillTabGeneric(%this);
    %ctrl = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "685 4";
        extent = "275 495";
        bitmap = "platform/client/ui/tgf/tgf_bkgd_rightside";
    };
    %ctrl.add(%tab);
    %ctrl = new GuiBitmapCtrl(geTGF_profilePic) {
        profile = "GuiDefaultProfile";
        horizSizing = "left";
        vertSizing = "top";
        position = "708 310";
        extent = "100 100";
        bitmap = "platform/client/ui/tgf/tgf_profile_default";
    };
    new GuiMLTextCtrl("") {
        profile = new GuiBitmapButtonCtrl("") {
        profile = "GuiDefaultProfile";
        position = "0 0";
        extent = "100 100";
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x100";
        command = "geTGF.onProfile();";
    }; @ "ETSNonModalProfile";
        position = "3 81";
        extent = "100 21";
        text = mlStyle("My Profile", "tgfWebLink_Light");
    };
    %ctrl.add(%tab);
    %ctrl = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "left";
        vertSizing = "top";
        position = "828 310";
        extent = "100 100";
        bitmap = "platform/client/ui/tgf/tgf_events";
    };
    new GuiMLTextCtrl("") {
        profile = new GuiBitmapButtonCtrl("") {
        profile = "GuiDefaultProfile";
        position = "0 0";
        extent = "100 100";
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x100";
        command = "geTGF.onEventsCalendar();";
    }; @ "ETSNonModalProfile";
        position = "3 81";
        extent = "100 21";
        text = mlStyle("Calendar", "tgfWebLink_Light");
    };
    %ctrl.add(%tab);
    %ctrl = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "left";
        vertSizing = "top";
        position = "708 431";
        extent = "100 50";
        bitmap = "platform/client/ui/tgf/tgf_forums";
    };
    new GuiMLTextCtrl("") {
        profile = new GuiBitmapButtonCtrl("") {
        profile = "GuiDefaultProfile";
        position = "0 0";
        extent = "100 50";
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x50";
        command = "geTGF.onForums();";
    }; @ "ETSNonModalProfile";
        position = "3 31";
        extent = "100 21";
        text = mlStyle("Forums", "tgfWebLink_Light");
    };
    %ctrl.add(%tab);
    %ctrl = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "left";
        vertSizing = "top";
        position = "828 431";
        extent = "100 50";
        bitmap = "platform/client/ui/tgf/tgf_faq";
    };
    new GuiMLTextCtrl("") {
        profile = new GuiBitmapButtonCtrl("") {
        profile = "GuiDefaultProfile";
        position = "0 0";
        extent = "100 50";
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x50";
        command = "geTGF.onFaq();";
    }; @ "ETSNonModalProfile";
        position = "3 31";
        extent = "100 21";
        text = mlStyle("FAQs", "tgfWebLink_Light");
    };
    %ctrl.add(%tab);
    %ctrl = new GuiBitmapCtrl(geTGF_main_BalancesContainer) {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "687 8";
        extent = "268 68";
        bitmap = "platform/client/ui/tgf/tgf_main_balances_background";
    };
    new GuiControl(AccountBalanceContents) {
        profile = new GuiMLTextCtrl(geTGF_main_OfflineIncomeNotification) {
        horizSizing = new GuiMLTextCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "6 9";
        extent = "117 16";
        text = $gMlStyle["tgf_General_Medium"] @ "Your Balances:";
    }; @ "right";
        vertSizing = "bottom";
        position = "6 37";
        extent = "257 36";
        style = "tgf_General_Medium";
    }; @ ETSNonModalProfile;
        horizSizing = "left";
        vertSizing = "bottom";
        position = "108 0";
        extent = "162 39";
    };
    %ctrl.add(%tab);
    %ctrl = new GuiBitmapCtrl(MOTDHud) {
        profile = new GuiControl(AccountBalancePBContainer) {
        profile = new GuiBitmapCtrl("") {
        profile = new GuiTextCtrl(AccountBalanceVBuxText) {
        profile = new GuiVariableWidthButtonCtrl("") {
        profile = new GuiTextCtrl(AccountBalanceVPointsText) {
        profile = new GuiVariableWidthButtonCtrl("") {
        profile = "VPointsButtonProfile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "6 5";
        extent = "81 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "gotoWebPage(\"" @ $Net::HelpURL_VPoints @ "\");";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        tooltip = "vPoints balance";
    }; @ "VPointsTextProfile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "27 4";
        extent = "57 20";
        minExtent = "57 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 255;
    }; @ "VBuxButtonProfile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "93 5";
        extent = "63 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "gotoWebPage(\"" @ $Net::AddFundsURL @ "\");";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        tooltip = "vBux balance";
    }; @ "VBuxTextProfile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "114 4";
        extent = "39 20";
        minExtent = "39 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 255;
    }; @ "ETSNonModalProfile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "6 29";
        extent = "49 5";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/next_level";
    }; @ "GuiDefaultProfile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "58 29";
        extent = "98 5";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    }; @ "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "687 80";
        extent = "268 228";
        minExtent = "1 1";
        visible = 1;
        bitmap = "platform/client/ui/motdBackground";
    };
    new GuiScrollCtrl("") {
        profile = "DottedScrollDarkProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 -1";
        extent = "269 230";
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 0;
        childMargin = "0 0";
        saneDrag = 1;
        scrollMultiplier = 1;
        stickyBottom = 0;
    };
    %ctrl.add(%tab);
    if ($ETS::devMode) {
        %ctrl = new GuiVariableWidthButtonCtrl(EditMOTDStaffOnly) {
            profile = new GuiMLTextCtrl(MOTDText) {
            profile = "ETSLoginMLTextProfile";
            horizSizing = "width";
            vertSizing = "height";
            position = "1 1";
            extent = "254 18";
            minExtent = "1 1";
            visible = 1;
            lineSpacing = 2;
            allowColorChars = 1;
            maxChars = -1;
            text = "<color:646464><font:Arial:24>Welcome to " @ $ETS::AppName @ "!\n\n<font:Arial:15>- Choose a place to get started";
            stripTagsOnCopy = 1;
        }; @ "BracketButtonLt19Profile";
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
        %ctrl.add(%tab);
    }
    %ctrl = new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        bitmap = "platform/client/ui/tgf/tgf_main_sectionOutlines";
        position = "0 0";
        extent = "685 498";
    };
    %ctrl.add(%tab);
    %ctrl = new GuiScrollCtrl("") {
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
    new GuiMLTextCtrl("") {
        internalName = new GuiArray2Ctrl(geTGF_main_happenings) {
        profile = "EtsNonModalProfile";
        position = "0 0";
        extent = "580 340";
        inRows = 0;
        numRowsOrCols = 3;
        childrenClassName = "GuiControl";
        childrenExtent = "190 109";
        spacing = 4;
    }; @ "emptyText";
        profile = "ETSNonModalProfile";
        position = "5 5";
        extent = 300;
        textNothing = mlStyle("<just:left>" @ "More parties coming up soon!" @ "<just:right>", "tgfItem_Happening");
        textLoading = mlStyle("<just:left>" @ "fetching.." @ "<just:right>", "tgfItem_Happening");
    };
    %ctrl.add(%tab);
    new GuiMLTextCtrl(geTGF_main_people_locationsText) {
        position = "3 2";
        extent = "164 1";
        style = "tgfPeopleCounts";
        lineSpacing = -(1.0);
        stripGamelink = 1;
    };
    new GuiMLTextCtrl("") {
        position = "1 1";
        extent = "168 1";
        style = "";
        lineSpacing = -(1.0);
        stripGamelink = 1;
        text = mlStyle($MsgCat::invitation["TEXT-TGF-MAIN"], "tgfMainInvite");
    };
    %ctrl = new GuiControl(geTGF_main_people) {
        profile = "EtsNonModalProfile";
        position = "40 367";
        extent = "625 129";
    };
    new GuiMLTextCtrl("") {
        internalName = new GuiControl(geTGF_main_PeopleGoRound_Container) {
        profile = new GuiControl("") {
        profile = new GuiScrollCtrl("") {
        profile = "DottedScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "1 1";
        extent = "169 80";
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        childMargin = "0 0";
        saneDrag = 1;
        scrollMultiplier = 1;
        stickyBottom = 0;
    }; @ EtsNotQuiteSoDarkBorderlessBoxProfile;
        position = "1 96";
        extent = "170 31";
    }; @ "ETSNonModalProfile";
        position = "158 2";
        extent = "467 125";
    }; @ "emptyText";
        profile = "ETSNonModalProfile";
        position = "2 2";
        extent = "300 10";
        textNothing = mlStyle("<just:left>" @ "You're early! Come on in!", "tgfItem_Happening");
        textLoading = mlStyle("<just:left>" @ "fetching..", "tgfItem_Happening");
    };
    %ctrl.add(%tab);
    %ctrl = new GuiVariableWidthButtonCtrl("") {
        profile = "BracketButtonLt19Profile";
        position = "5 86";
        extent = "50 19";
        command = "geTGF.main_testData_happenings();";
        text = "test";
        visible = 0;
    };
    %ctrl.add(%tab);
};
function geTGF_tabs::onShowTabMain(%this) {
    cancel(geTGF.geTGF_Refresh_Schedule);
    1.setActive(geTGF_Refresh);
    1.setVisible(geTGF_Refresh);
};
function geTGF::main_testData_happenings(%this) {
    %requestName = "request_GetMainHappenings";
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    %requestName.setName(%request);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/finder/GetMainHappenings";
    %url.setURL(%request);
    $Player::Name.addUserAndToken(%request);
    "success".putValue(%request, "status");
    %n = 0;
    "open".putValue(%request, "happenings" @ %n @ ".accessMode");
    "https://s-website.doppelganger.com/photoservice/e5b50410-76cc-4627-b0bb-5508b311436d".putValue(%request, "happenings" @ %n @ ".baseImageURL");
    "dababydoll".putValue(%request, "happenings" @ %n @ ".customSpaceId");
    1164.putValue(%request, "happenings" @ %n @ ".eventId");
    "true".putValue(%request, "happenings" @ %n @ ".featured");
    -1.putValue(%request, "happenings" @ %n @ ".friendOccupancy");
    "vside:/location/lga/ShoppingSpawns_modpodz".putValue(%request, "happenings" @ %n @ ".goThereVURL");
    "HOWDY!".putValue(%request, "happenings" @ %n @ ".headline");
    "dababydoll".putValue(%request, "happenings" @ %n @ ".hostUserName");
    "true".putValue(%request, "happenings" @ %n @ ".initialized");
    "lga".putValue(%request, "happenings" @ %n @ ".location.areaName");
    "".putValue(%request, "happenings" @ %n @ ".location.buildingName");
    "LaGenoaAiresNorth".putValue(%request, "happenings" @ %n @ ".location.serverName");
    -1.putValue(%request, "happenings" @ %n @ ".occupancy");
    "publicLocationEvent".putValue(%request, "happenings" @ %n @ ".type");
    %n = (%n + 1.0);
    "open".putValue(%request, "happenings" @ %n @ ".accessMode");
    "https://s-website.doppelganger.com/photoservice/e5b50410-76cc-4627-b0bb-5508b311436d".putValue(%request, "happenings" @ %n @ ".baseImageURL");
    "dababydoll".putValue(%request, "happenings" @ %n @ ".customSpaceId");
    1164.putValue(%request, "happenings" @ %n @ ".eventId");
    "true".putValue(%request, "happenings" @ %n @ ".featured");
    3.putValue(%request, "happenings" @ %n @ ".friendOccupancy");
    "vside:/location/lga/ShoppingSpawns_modpodz".putValue(%request, "happenings" @ %n @ ".goThereVURL");
    "HOWDY!".putValue(%request, "happenings" @ %n @ ".headline");
    "dababydoll".putValue(%request, "happenings" @ %n @ ".hostUserName");
    "true".putValue(%request, "happenings" @ %n @ ".initialized");
    "lga".putValue(%request, "happenings" @ %n @ ".location.areaName");
    "".putValue(%request, "happenings" @ %n @ ".location.buildingName");
    "LaGenoaAiresNorth".putValue(%request, "happenings" @ %n @ ".location.serverName");
    17.putValue(%request, "happenings" @ %n @ ".occupancy");
    "aptEvent".putValue(%request, "happenings" @ %n @ ".type");
    %n = (%n + 1.0);
    "open".putValue(%request, "happenings" @ %n @ ".accessMode");
    "https://s-website.doppelganger.com/photoservice/e5b50410-76cc-4627-b0bb-5508b311436d".putValue(%request, "happenings" @ %n @ ".baseImageURL");
    "dababydoll".putValue(%request, "happenings" @ %n @ ".customSpaceId");
    1164.putValue(%request, "happenings" @ %n @ ".eventId");
    "false".putValue(%request, "happenings" @ %n @ ".featured");
    3.putValue(%request, "happenings" @ %n @ ".friendOccupancy");
    "vside:/location/lga/ShoppingSpawns_modpodz".putValue(%request, "happenings" @ %n @ ".goThereVURL");
    "HOWDY!".putValue(%request, "happenings" @ %n @ ".headline");
    "dababydoll".putValue(%request, "happenings" @ %n @ ".hostUserName");
    "true".putValue(%request, "happenings" @ %n @ ".initialized");
    "lga".putValue(%request, "happenings" @ %n @ ".location.areaName");
    "".putValue(%request, "happenings" @ %n @ ".location.buildingName");
    "LaGenoaAiresNorth".putValue(%request, "happenings" @ %n @ ".location.serverName");
    17.putValue(%request, "happenings" @ %n @ ".occupancy");
    "aptEvent".putValue(%request, "happenings" @ %n @ ".type");
    %n = (%n + 1.0);
    "open".putValue(%request, "happenings" @ %n @ ".accessMode");
    "".putValue(%request, "happenings" @ %n @ ".baseImageURL");
    "dababydoll".putValue(%request, "happenings" @ %n @ ".customSpaceId");
    "".putValue(%request, "happenings" @ %n @ ".eventId");
    "false".putValue(%request, "happenings" @ %n @ ".featured");
    3.putValue(%request, "happenings" @ %n @ ".friendOccupancy");
    "vside:/location/lga/ShoppingSpawns_modpodz".putValue(%request, "happenings" @ %n @ ".goThereVURL");
    "HOWDY!".putValue(%request, "happenings" @ %n @ ".headline");
    "dababydoll".putValue(%request, "happenings" @ %n @ ".hostUserName");
    "true".putValue(%request, "happenings" @ %n @ ".initialized");
    "lga".putValue(%request, "happenings" @ %n @ ".location.areaName");
    "".putValue(%request, "happenings" @ %n @ ".location.buildingName");
    "LaGenoaAiresNorth".putValue(%request, "happenings" @ %n @ ".location.serverName");
    17.putValue(%request, "happenings" @ %n @ ".occupancy");
    "apt".putValue(%request, "happenings" @ %n @ ".type");
    %n = (%n + 1.0);
    %n.putValue(%request, "happeningsCount");
    %statusTextCtrl = 1.child(geTGF_main_happenings.getParent(), "emptyText");
    %text = %statusTextCtrl.textLoading;
    %text.setText(%statusTextCtrl);
    schedule(500, 0, "onDoneOrErrorCallback_GetMainHappenings", %request);
};
function geTGF_tabs::refreshTabMain(%this) {
    %tabName = "main";
    %tab = %tabName.getTabWithName(%this);
    %this.refreshQOTD();
    if (!(geTGF_tabs.previousProfileName $= $Player::Name)) {
        "platform/client/ui/tgf/tgf_profile_default".setBitmap(geTGF_profilePic);
        geTGF_profilePic.fitInParentAsBitmap();
    }
    geTGF_tabs.previousProfileName = $Player::Name;
    %url = $Net::AvatarURL @ urlEncode($Player::Name) @ "?size=M";
    %url.downloadAndApplyBitmap(geTGF_profilePic);
    geTGF.main_sendRequests();
};
function geTGF::main_sendRequests(%this) {
    %statusTextCtrl = 1.child(geTGF_main_people, "emptyText");
    %text = %statusTextCtrl.textLoading;
    %text.setText(%statusTextCtrl);
    sendRequest_GetOnlineUsers(60, "onDoneOrErrorCallback_GetOnlineUsers");
    "".setText(geTGF_main_people_locationsText);
    %statusTextCtrl = 1.child(geTGF_main_happenings.getParent(), "emptyText");
    %text = %statusTextCtrl.textLoading;
    %text.setText(%statusTextCtrl);
    sendRequest_GetMainHappenings(9, "onDoneOrErrorCallback_GetMainHappenings");
    WorldMap.refresh();
};
function onDoneOrErrorCallback_GetMainHappenings(%request, %unused) {
    if ((geTGF_tabs.getCurrentTab().name $= "main")) {
        cancel(geTGF.geTGF_Refresh_Schedule);
        1.setActive(geTGF_Refresh);
    }
    log("network", "debug", getScopeName() @ " " @ "- url =" @ " " @ %request.getURL());
    %itemType = "happening";
    %itemType.clearItemList(geTGF, "main");
    if (!(%request.checkSuccess())) {
        %itemType.main_onGotDataOfType(geTGF);
        return;
    }
    %listBase = "happenings";
    %count = %listBase @ "Count".getResult(%request);
    %n = 0;
    while ((%n < %count)) {
        %listItem = %listBase @ %n;
        %id = %listItem @ ".hostUserName".getResult(%request);
        %item = %id.createNewItem(geTGF, "main", %itemType);
        "accessMode".copyListValueIntoObject(%request, %item, %listItem);
        "occupancy".copyListValueIntoObject(%request, %item, %listItem);
        "baseImageURL".copyListValueIntoObject(%request, %item, %listItem);
        "eventId".copyListValueIntoObject(%request, %item, %listItem);
        "featured".copyListValueIntoObject(%request, %item, %listItem);
        "friendOccupancy".copyListValueIntoObject(%request, %item, %listItem);
        "goThereVURL".copyListValueIntoObject(%request, %item, %listItem);
        "headline".copyListValueIntoObject(%request, %item, %listItem);
        "hostUserName".copyListValueIntoObject(%request, %item, %listItem);
        "moreInfoURL".copyListValueIntoObject(%request, %item, %listItem);
        "apt".copyListValueIntoObject(%request, %item, %listItem);
        "location.areaName".copyListValueIntoObject(%request, %item, %listItem);
        "location.buildingName".copyListValueIntoObject(%request, %item, %listItem);
        "location.serverName".copyListValueIntoObject(%request, %item, %listItem);
        %item.subType = %listItem @ ".type".getResult(%request);
        %item.goThereVURL = vurlClearResolution(%item.goThereVURL);
        %n = (%n + 1.0);
    }
    "The-Manager".removeItemsWithFieldValueFromList(geTGF, "main", %itemType, "hostUserName");
    %itemType.main_onGotDataOfType(geTGF);
};
function onDoneOrErrorCallback_GetOnlineFriends_Main(%request) {
    if ((geTGF_tabs.getCurrentTab().name $= "main")) {
        cancel(geTGF.geTGF_Refresh_Schedule);
        1.setActive(geTGF_Refresh);
    }
    log("network", "debug", getScopeName() @ " " @ "- url =" @ " " @ %request.getURL());
    %itemType = "person";
    %itemType.clearItemList(geTGF, "main");
    if (!(%request.checkSuccess())) {
        %itemType.main_onGotDataOfType(geTGF);
        return;
    }
    %listBase = "friends";
    %count = %listBase @ "Count".getValue(%request);
    %n = 0;
    while ((%n < %count)) {
        %listItem = %listBase @ %n;
        %id = %listItem @ ".userName".getValue(%request);
        %item = %id.createNewItem(geTGF, "main", %itemType);
        "age".copyListValueIntoObject(%request, %item, %listItem);
        "currentActivities".copyListValueIntoObject(%request, %item, %listItem);
        "currentLocation.areaName".copyListValueIntoObject(%request, %item, %listItem);
        "currentLocation.buildingName".copyListValueIntoObject(%request, %item, %listItem);
        "currentLocation.serverName".copyListValueIntoObject(%request, %item, %listItem);
        "gender".copyListValueIntoObject(%request, %item, %listItem);
        "headline".copyListValueIntoObject(%request, %item, %listItem);
        "homeLocation.areaName".copyListValueIntoObject(%request, %item, %listItem);
        "homeLocation.buildingName".copyListValueIntoObject(%request, %item, %listItem);
        "homeLocation.serverName".copyListValueIntoObject(%request, %item, %listItem);
        "ignored".copyListValueIntoObject(%request, %item, %listItem);
        "locationIRL".copyListValueIntoObject(%request, %item, %listItem);
        "onlineStatus".copyListValueIntoObject(%request, %item, %listItem);
        "permissionsMask".copyListValueIntoObject(%request, %item, %listItem);
        "profileViewCount".copyListValueIntoObject(%request, %item, %listItem);
        "profileViewRanking".copyListValueIntoObject(%request, %item, %listItem);
        "relationType".copyListValueIntoObject(%request, %item, %listItem);
        "score".copyListValueIntoObject(%request, %item, %listItem);
        "userName".copyListValueIntoObject(%request, %item, %listItem);
        %item.goThereVURL = "vside:/user/" @ %item.userName;
        %n = (%n + 1.0);
    }
    %itemType.main_onGotDataOfType(geTGF);
};
function onDoneOrErrorCallback_GetOnlineUsers(%request) {
    log("network", "debug", getScopeName() @ " " @ "- url =" @ " " @ %request.getURL());
    %statusTextCtrl = 1.child(geTGF_main_people, "emptyText");
    "".setText(%statusTextCtrl);
    %itemType = "person";
    %itemType.clearItemList(geTGF, "main");
    if (!(%request.checkSuccess())) {
        %itemType.main_onGotDataOfType(geTGF);
        return;
    }
    %listBase = "user";
    %count = %listBase @ "Count".getValue(%request);
    %n = 0;
    while ((%n < %count)) {
        %listItem = %listBase @ %n;
        %id = %listItem @ ".userName".getValue(%request);
        %item = %id.createNewItem(geTGF, "main", %itemType);
        "age".copyListValueIntoObject(%request, %item, %listItem);
        "currentActivities".copyListValueIntoObject(%request, %item, %listItem);
        "currentLocation.areaName".copyListValueIntoObject(%request, %item, %listItem);
        "currentLocation.buildingName".copyListValueIntoObject(%request, %item, %listItem);
        "currentLocation.serverName".copyListValueIntoObject(%request, %item, %listItem);
        "gender".copyListValueIntoObject(%request, %item, %listItem);
        "headline".copyListValueIntoObject(%request, %item, %listItem);
        "homeLocation.areaName".copyListValueIntoObject(%request, %item, %listItem);
        "homeLocation.buildingName".copyListValueIntoObject(%request, %item, %listItem);
        "homeLocation.serverName".copyListValueIntoObject(%request, %item, %listItem);
        "ignored".copyListValueIntoObject(%request, %item, %listItem);
        "locationIRL".copyListValueIntoObject(%request, %item, %listItem);
        "onlineStatus".copyListValueIntoObject(%request, %item, %listItem);
        "permissionsMask".copyListValueIntoObject(%request, %item, %listItem);
        "profileViewCount".copyListValueIntoObject(%request, %item, %listItem);
        "profileViewRanking".copyListValueIntoObject(%request, %item, %listItem);
        "relationType".copyListValueIntoObject(%request, %item, %listItem);
        "score".copyListValueIntoObject(%request, %item, %listItem);
        "userName".copyListValueIntoObject(%request, %item, %listItem);
        %item.goThereVURL = "vside:/user/" @ %item.userName;
        %n = (%n + 1.0);
    }
    %itemType.main_onGotDataOfType(geTGF);
};
function onDoneOrErrorCallback_GetMainVenues(%request) {
    if ((geTGF_tabs.getCurrentTab().name $= "main")) {
        cancel(geTGF.geTGF_Refresh_Schedule);
        1.setActive(geTGF_Refresh);
    }
    log("network", "debug", "FAKE REQUEST: " @ getScopeName() @ " " @ "- url =" @ " " @ %request.getURL());
    "fakeSuccess".putValue(%request, "statusMsg");
    %request.timeFinish = getSimTime();
    %itemType = "venue";
    %itemType.clearItemList(geTGF, "main");
    if (!(%request.checkSuccess())) {
        %itemType.main_onGotDataOfType(geTGF);
        return;
    }
    %listBase = "venues";
    %count = %listBase @ "Count".getValue(%request);
    %n = 0;
    while ((%n < %count)) {
        %listItem = %listBase @ %n;
        %id = %listItem @ ".name".getValue(%request);
        %item = %id.createNewItem(geTGF, "main", %itemType);
        "codeName".copyListValueIntoObject(%request, %item, %listItem);
        "fullName".copyListValueIntoObject(%request, %item, %listItem);
        "location.areaName".copyListValueIntoObject(%request, %item, %listItem);
        "location.buildingName".copyListValueIntoObject(%request, %item, %listItem);
        "location.serverName".copyListValueIntoObject(%request, %item, %listItem);
        "headline".copyListValueIntoObject(%request, %item, %listItem);
        "occupancy".copyListValueIntoObject(%request, %item, %listItem);
        "friendOccupancy".copyListValueIntoObject(%request, %item, %listItem);
        "goThereVURL".copyListValueIntoObject(%request, %item, %listItem);
        %n = (%n + 1.0);
    }
    %itemType.main_onGotDataOfType(geTGF);
};
function geTGF::main_onGotDataOfType(%this, %type) {
    if ((%type $= "happening")) {
        %control = geTGF_main_happenings;
    }
    if ((%type $= "person")) {
        %control = geTGF_main_people;
        %control.onGotData();
        return;
    }
    error("unknown type:" @ " " @ %type @ " " @ getTrace());
    return;
    %list = %type.getItemList(%this, "main");
    %num = %list.count();
    %num.setNumChildren(%control);
    %n = 0;
    while ((%n < %num)) {
        %cell = %n.getObject(%control);
        %item = %n.getValue(%list);
        %cell.Item = %item;
        ETSDarkBoxNonModalProfile.setProfile(%cell);
        %cell.isInviteFriendsButton = 0;
        %cell.updateCellFromItsItem(%control);
        %n = (%n + 1.0);
    }
    %statusTextCtrl = "emptyText".child(%control.getParent());
    (%n < %num);
    if ((%num <= 0.0)) {
        %text = %statusTextCtrl.textNothing;
    }
    %text = "";
    %text.setText(%statusTextCtrl);
};
function geTGF_main_happenings::updateCellFromItsItem(%this, %cell) {
    %item = %cell.Item;
    %cell.deleteMembers();
    %cont = new GuiControl("") {
        profile = "EtsNonModalProfile";
        extent = (getWord(%cell.getExtent(), 0) - 2.0) @ " " @ (getWord(%cell.getExtent(), 1) - 2.0);
        position = "1 1";
        horizSizing = "width";
        vertSizing = "top";
    };
    %cont.add(%cell);
    %w = getWord(%cell.getParent().childrenExtent, 0);
    %h = getWord(%cell.getParent().childrenExtent, 1);
    if ((%item.eventID $= "")) {
        %w = %h;
    }
    %ctrl = new GuiBitmapCtrl("") {
        profile = "EtsNonModalProfile";
        extent = %w @ " " @ %h;
        position = "0 0";
        horizSizing = "width";
        vertSizing = "top";
        fitInParentAlign = 1;
    };
    %ctrl.add(%cont);
    %url = %item.baseImageURL;
    if ((%url $= "")) {
        %url = $Net::BuildDirPhotoURL @ urlEncode(%item.hostUserName);
    }
    %url = !(%url @ "?size=" @ " " @ %item.eventID $= "") ? "M" : "M";
    "platform/client/ui/tgf/tgf_profile_default".setBitmap(%ctrl);
    %ctrl.fitInParentAsBitmap();
    %url.downloadAndApplyBitmap(%ctrl);
    %tmp = 20;
    %ctrl = new GuiControl("") {
        profile = "EtsDarkBorderlessBoxProfile";
        extent = getWord(%cont.getExtent(), 0) @ " " @ %tmp;
        position = 0 @ " " @ (getWord(%cont.getExtent(), 1) - %tmp);
        horizSizing = "width";
        vertSizing = "top";
    };
    %ctrl.add(%cont);
    %ctrl = new GuiBitmapButtonCtrl("") {
        profile = "GuiDefaultProfile";
        position = "-1 -1";
        extent = %cell.getExtent();
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_190x109";
        command = "geTGF.main_OnClickItem(\"" @ %item @ "\");";
    };
    %ctrl.add(%cont);
    %ctrl = new GuiMLTextCtrl("") {
        profile = "ETSNonModalProfile";
        position = 2 @ " " @ (getWord(%cont.getExtent(), 1) - 16.0);
        extent = (getWord(%cont.getExtent(), 0) - 9.0) @ " " @ 20;
        text = mlStyle("<just:left>" @ %item.headline @ "<just:right>", "tgfItem_headline");
        horizSizing = "width";
        vertSizing = "top";
        lineSpacing = 0;
    };
    %ctrl.add(%cont);
    if ((%item.occupancy == -(1.0))) {
        %occupancyText = "";
    }
    if ((%item.occupancy == 0.0)) {
        %occupancyText = "(empty) ";
    }
    %occupancyText = "<b>" @ %item.occupancy @ " P ";
    %ctrl = new GuiMLTextCtrl("") {
        profile = "ETSNonModalProfile";
        position = 0 @ " " @ (getWord(%cont.getExtent(), 1) - 38.0);
        extent = (getWord(%cont.getExtent(), 0) - 9.0) @ " " @ 20;
        text = mlStyle("<just:left>" @ %item.hostUserName @ "<just:right><font:Arial:14>" @ %occupancyText, "tgfItem_host");
        horizSizing = "width";
        vertSizing = "top";
    };
    %ctrl.add(%cont);
    if (!(%item.eventID $= "")) {
        %bitmap = %item.getEventTypePostItTagBitmap(geTGF);
        %ctrl = new GuiBitmapCtrl("") {
            bitmap = %bitmap;
            profile = "EtsNonModalProfile";
            extent = "82 19";
            position = ((getWord(%cont.getExtent(), 0) - 82.0) - 0.0) @ " " @ 0;
            horizSizing = "left";
            vertSizing = "bottom";
        };
        %ctrl.add(%cont);
    }
};
function geTGF_main_people::onGotData(%this) {
    %list = "person".getItemList(geTGF, "main");
    %num = %list.size();
    %statusTextCtrl = "emptyText".child(%this);
    if ((%num <= 0.0)) {
    }
    %text = "";
    %statusTextCtrl.textNothing;
    %text.setText(%statusTextCtrl);
    if (!(isObject(geTGFGoRound))) {
        newTGFGoRound(geTGFGoRound).add(geTGF_main_PeopleGoRound_Container);
        "".reparentSameSize(geTGFGoRound, geTGF_main_PeopleGoRound_Container);
        geTGFGoRound.mDeetsMinWidth = 0;
        geTGFGoRound.mLilThumbPadding = 5;
        geTGFGoRound.mLilThumbHeight = geTGFGoRound.calcMaximumThumbHeight();
        geTGFGoRound.mTickPeriodMS = 4500;
        geTGFGoRound.rebuild();
    }
    %list.setItemList(geTGFGoRound);
    %this.tryUpdateWorldmapSummaries();
};
function geTGF_main_people::tryUpdateWorldmapSummaries(%this) {
    "".setTextWithStyle(geTGF_main_people_locationsText);
    if (isObject(MapRequest)) {
        "tryUpdateWorldmapSummaries".schedule(%this, 1000);
    }
    %areaNames = "lga nv rj pvt";
    %total = 0;
    %text = "";
    %delim = "";
    %n = 0;
    while ((%n < getWordCount(%areaNames))) {
        %areaName = getWord(%areaNames, %n);
        if (!(%areaName $= "pvt")) {
            %userCount = %areaName.get(WorldAreaSummaries).occupancy;
        }
        %userCount = WorldAreaSummaries.totalOccupancy;
        %areaName;
        %link = "TGF_GOTO" @ " " @ %areaName;
        if ((%areaName $= "pvt")) {
            %areaName = "Personal Spaces";
        }
        %areaName = DestinationList::GetAreaNameUserFacingName(DestinationList::GetAreaNameCity(%areaName));
        %text = %text @ %delim @ "<spush><just:left><a:gamelink:" @ %link @ ">" @ %areaName @ "" @ "\t" @ "<just:right>" @ commaify(%userCount) @ "</a><spop>";
        %total = (%total + %userCount);
        %delim = "<br>";
        %n = (%n + 1.0);
    }
    %withS = (%total == 1.0) ? "" : "s";
    (%n < getWordCount(%areaNames));
    %text = "<spush><just:center><font:arial:16>" @ commaify(%total) @ " vSider" @ %withS @ " In-World:<spop><br>" @ %text;
    %text = "<tab:100>" @ %text;
    %text.setTextWithStyle(geTGF_main_people_locationsText);
};
function geTGF_main_people_locationsText::onURL(%this, %url) {
    %cmd = firstWord(%url);
    if (!(%cmd $= "TGF_GOTO")) {
        error(getScopeName() @ " " @ "- unknown command" @ " " @ %cmd @ " " @ getTrace());
        return;
    }
    %areaName = restWords(%url);
    if ((%areaName $= "pvt")) {
        "HOTSPOTS".openToTabName(geTGF);
    }
    if ((%areaName $= "lga")) {
        "MAP".openToTabName(geTGF);
        0.getCityButton(WorldMap, %areaName, 1).performClick();
    }
    if ((%areaName $= "nv")) {
        "MAP".openToTabName(geTGF);
        0.getCityButton(WorldMap, %areaName, 1).performClick();
    }
    if ((%areaName $= "rj")) {
        "MAP".openToTabName(geTGF);
        0.getCityButton(WorldMap, %areaName, 1).performClick();
    }
    error(getScopeName() @ " " @ "- unknown areaname" @ " " @ %areaName @ " " @ getTrace());
};
function encodeMOTDString(%text) {
    %text = strreplace(%text, "\n", "<br>");
    return urlEncode(%text);
};
function decodeMOTDString(%text) {
    %text = strreplace(%text, "<br>", "\n");
    return %text;
};
function geTGF_tabs::refreshMOTD(%this) {
    if (isObject(MOTDRequest)) {
        MOTDRequest.delete();
    }
    new ManagerRequest(MOTDRequest);
    if (isObject(MissionCleanup)) {
        MOTDRequest.add(MissionCleanup);
    }
    %url = $Net::ClientServiceURL @ "/GlobalMessage";
    %url = %url @ "?user=" @ urlEncode($Player::Name);
    %url = %url @ "&token=" @ urlEncode($Token);
    %url = %url @ "&type=" @ urlEncode("motd");
    log("communication", "debug", "sending request to get current motd: " @ %url);
    %url.setURL(MOTDRequest);
    MOTDRequest.start();
};
function MOTDRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        geTGF_tabs.onMOTDPostFailed();
        log("communication", "warn", "MOTDRequest::onDone status: " @ %status);
    }
    %text = "message".getValue(%this);
    if (!(%text $= "")) {
        %text = decodeMOTDString(%text);
        %text.setText(MOTDText);
        MOTDText.qotdID = "";
    }
    geTGF_tabs.onMOTDPostFailed();
};
function geTGF_tabs::refreshQOTD(%this) {
    if (isObject(QOTDRequest)) {
        QOTDRequest.delete();
    }
    new ManagerRequest(QOTDRequest);
    if (isObject(MissionCleanup)) {
        QOTDRequest.add(MissionCleanup);
    }
    %url = $Net::ClientServiceURL @ "/GlobalMessage";
    %url = %url @ "?user=" @ urlEncode($Player::Name);
    %url = %url @ "&token=" @ urlEncode($Token);
    %url = %url @ "&type=" @ urlEncode("qotd");
    log("communication", "debug", "sending request to get current qotd: " @ %url);
    %url.setURL(QOTDRequest);
    QOTDRequest.start();
};
function QOTDRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    %text = "";
    if ((%status $= "success")) {
        %text = "message".getValue(%this);
        %text = decodeMOTDString(%text);
        %qID = trim(getWords(%text, 0, 0));
        %text = getWords(%text, 1);
        %alreadyAnswered = hasSubString($UserPref::QOTD::answered, %qID);
        if (%alreadyAnswered) {
            echoDebug(getScopeName() @ " " @ "- already answered" @ " " @ %qID);
            %text = "";
        }
    }
    if (!(%text $= "")) {
        %text.setText(MOTDText);
        MOTDText.qotdID = %qID;
    }
    geTGF_tabs.refreshMOTD();
};
function MOTDText::onURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %url = restWords(%url);
    }
    if ((strstr(%url, "answer:") != 0.0)) {
        Parent::onURL(%this, %url);
        return;
    }
    %answer = getSubStr(%url, strlen("answer:"), 100000);
    %trackURL = "";
    %trackURL = %trackURL @ "/client/qotd";
    %trackURL = %trackURL @ "/" @ %this.qotdID;
    %trackURL = %trackURL @ "/" @ %answer;
    %analytic = getAnalytic();
    %trackURL.trackPageView(%analytic);
    $MsgCat::QOTD["thanks"].setText(%this);
    "refreshMOTD".schedule(geTGF_tabs, 1000);
    $UserPref::QOTD::answered = $UserPref::QOTD::answered @ " " @ %this.qotdID;
};
function geTGF_tabs::onMOTDPostFailed(%this) {
    "".setText(MOTDText);
    %fileName = "projects/common/defaultMOTD.txt";
    %fo = new FileObject("");
    if (%fileName.openForRead(%fo)) {
        %text = "";
        while (!(%fo.isEOF())) {
            %text = %text @ %fo.readLine() @ "\n";
        }
        %text.setText(MOTDText);
    }
    error("unable to find default motd: " @ %fileName);
    %fo.delete();
};
function geTGF::onProfile(%this) {
    doEditProfile();
};
function geTGF::onEventsCalendar(%this) {
    openEventsURL();
};
function geTGF::onForums(%this) {
    openForumsURL();
};
function geTGF::onFAQ(%this) {
    openHelpURL();
};
function geTGF::main_OnClickItem(%this, %item) {
    %item.DoDetails(%this, "main");
};
function geTGF::main_GetAndOpenDetailsContainer(%this, %item) {
    %item.constructDeetsWindow(%this, geDeetsWindow);
    1.setVisible(geDeetsLayer);
    return geDeetsWindow;
};
