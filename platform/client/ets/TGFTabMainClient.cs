function geTGF_tabs::fillTabMain(%this) {
    %tabName = "main";
    %tab = %this.getTabWithName(%tabName);
    if (%tab.filled) {
        return;
    }
    %tab.filled = 1;
    %this.fillTabGeneric(%tab);
    0;
    %ctrl = new ""() {
        profile = GuiBitmapCtrl @ "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "685 4";
        extent = "275 495";
        bitmap = "platform/client/ui/tgf/tgf_bkgd_rightside";
    };
    %tab.add(%ctrl);
    new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiDefaultProfile";
        position = "0 0";
        extent = "100 100";
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x100";
        command = "geTGF.onProfile();";
    };
    %ctrl = new GuiBitmapCtrl(geTGF_profilePic) {
        profile = "GuiDefaultProfile";
        horizSizing = "left";
        vertSizing = "top";
        position = "708 310";
        extent = "100 100";
        bitmap = "platform/client/ui/tgf/tgf_profile_default";
    };
    new ""() {
        profile = GuiMLTextCtrl @ "ETSNonModalProfile";
        position = "3 81";
        extent = "100 21";
        text = mlStyle("My Profile", "tgfWebLink_Light");
    };
    %tab.add(%ctrl);
    0;
    new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiDefaultProfile";
        position = "0 0";
        extent = "100 100";
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x100";
        command = "geTGF.onEventsCalendar();";
    };
    %ctrl = new ""() {
        profile = GuiBitmapCtrl @ "GuiDefaultProfile";
        horizSizing = "left";
        vertSizing = "top";
        position = "828 310";
        extent = "100 100";
        bitmap = "platform/client/ui/tgf/tgf_events";
    };
    new ""() {
        profile = GuiMLTextCtrl @ "ETSNonModalProfile";
        position = "3 81";
        extent = "100 21";
        text = mlStyle("Calendar", "tgfWebLink_Light");
    };
    %tab.add(%ctrl);
    0;
    new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiDefaultProfile";
        position = "0 0";
        extent = "100 50";
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x50";
        command = "geTGF.onForums();";
    };
    %ctrl = new ""() {
        profile = GuiBitmapCtrl @ "GuiDefaultProfile";
        horizSizing = "left";
        vertSizing = "top";
        position = "708 431";
        extent = "100 50";
        bitmap = "platform/client/ui/tgf/tgf_forums";
    };
    new ""() {
        profile = GuiMLTextCtrl @ "ETSNonModalProfile";
        position = "3 31";
        extent = "100 21";
        text = mlStyle("Forums", "tgfWebLink_Light");
    };
    %tab.add(%ctrl);
    0;
    new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiDefaultProfile";
        position = "0 0";
        extent = "100 50";
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x50";
        command = "geTGF.onFaq();";
    };
    %ctrl = new ""() {
        profile = GuiBitmapCtrl @ "GuiDefaultProfile";
        horizSizing = "left";
        vertSizing = "top";
        position = "828 431";
        extent = "100 50";
        bitmap = "platform/client/ui/tgf/tgf_faq";
    };
    new ""() {
        profile = GuiMLTextCtrl @ "ETSNonModalProfile";
        position = "3 31";
        extent = "100 21";
        text = mlStyle("FAQs", "tgfWebLink_Light");
    };
    %tab.add(%ctrl);
    new GuiTextCtrl(AccountBalanceVPointsText) {
        profile = new ""() {
        profile = GuiVariableWidthButtonCtrl @ "VPointsButtonProfile";
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
    };
    new GuiTextCtrl(AccountBalanceVBuxText) {
        profile = new ""() {
        profile = GuiVariableWidthButtonCtrl @ "VBuxButtonProfile";
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
    };
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
        horizSizing = new ""() {
        profile = GuiMLTextCtrl @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "6 9";
        extent = "117 16";
        text = "Your Balances:";
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
    %tab.add(%ctrl);
    %ctrl = new GuiBitmapCtrl(MOTDHud) {
        profile = new GuiControl(AccountBalancePBContainer) {
        profile = new ""() {
        profile = GuiBitmapCtrl @ "ETSNonModalProfile";
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
    new ""() {
        profile = GuiScrollCtrl @ "DottedScrollDarkProfile";
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
    %tab.add(%ctrl);
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
        %tab.add(%ctrl);
    }
    0;
    %ctrl = new ""() {
        profile = GuiBitmapCtrl @ "ETSNonModalProfile";
        bitmap = "platform/client/ui/tgf/tgf_main_sectionOutlines";
        position = "0 0";
        extent = "685 498";
    };
    %tab.add(%ctrl);
    0;
    new GuiArray2Ctrl(geTGF_main_happenings) {
        profile = "EtsNonModalProfile";
        position = "0 0";
        extent = "580 340";
        inRows = 0;
        numRowsOrCols = 3;
        childrenClassName = "GuiControl";
        childrenExtent = "190 109";
        spacing = 4;
    };
    %ctrl = new ""() {
        profile = GuiScrollCtrl @ "DottedScrollProfile";
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
    new ""() {
        internalName = GuiMLTextCtrl @ "emptyText";
        profile = "ETSNonModalProfile";
        position = "5 5";
        extent = 300;
        textNothing = mlStyle("<just:left>" @ "More parties coming up soon!" @ "<just:right>", "tgfItem_Happening");
        textLoading = mlStyle("<just:left>" @ "fetching.." @ "<just:right>", "tgfItem_Happening");
    };
    %tab.add(%ctrl);
    new ""() {
        profile = GuiScrollCtrl @ "DottedScrollProfile";
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
    };
    new ""() {
        position = GuiMLTextCtrl @ "1 1";
        extent = "168 1";
        style = "";
        lineSpacing = -(1.0);
        stripGamelink = 1;
        text = mlStyle(, "tgfMainInvite");
    };
    new GuiControl(geTGF_main_PeopleGoRound_Container) {
        profile = new ""() {
        profile = GuiControl @ EtsNotQuiteSoDarkBorderlessBoxProfile;
        position = new GuiMLTextCtrl(geTGF_main_people_locationsText) {
        position = "3 2";
        extent = "164 1";
        style = "tgfPeopleCounts";
        lineSpacing = -(1.0);
        stripGamelink = 1;
    }; @ "1 96";
        extent = "170 31";
    }; @ "ETSNonModalProfile";
        position = "158 2";
        extent = "467 125";
    };
    %ctrl = new GuiControl(geTGF_main_people) {
        profile = "EtsNonModalProfile";
        position = "40 367";
        extent = "625 129";
    };
    new ""() {
        internalName = GuiMLTextCtrl @ "emptyText";
        profile = "ETSNonModalProfile";
        position = "2 2";
        extent = "300 10";
        textNothing = mlStyle("<just:left>" @ "You're early! Come on in!", "tgfItem_Happening");
        textLoading = mlStyle("<just:left>" @ "fetching..", "tgfItem_Happening");
    };
    %tab.add(%ctrl);
    0;
    %ctrl = new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButtonLt19Profile";
        position = "5 86";
        extent = "50 19";
        command = "geTGF.main_testData_happenings();";
        text = "test";
        visible = 0;
    };
    %tab.add(%ctrl);
};
function geTGF_tabs::onShowTabMain(%this) {
    cancel(geTGF_Refresh_Schedule);
    1.setActive();
    1.setVisible();
};
function geTGF::main_testData_happenings(%this) {
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
    %n = (1.0 + %n);
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
    %n = (1.0 + %n);
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
    %n = (1.0 + %n);
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
    %n = (1.0 + %n);
    %request.putValue("happeningsCount", %n);
    %statusTextCtrl = geTGF_main_happenings.getParent().child("emptyText", 1);
    %text = %statusTextCtrl.textLoading;
    %statusTextCtrl.setText(%text);
    schedule(500, 0, "onDoneOrErrorCallback_GetMainHappenings", %request);
};
function geTGF_tabs::refreshTabMain(%this) {
    %tabName = "main";
    %tab = %this.getTabWithName(%tabName);
    %this.refreshQOTD();
    if (!(geTGF_tabs @ " " @ %statusTextCtrl.previousProfileName $= $Player::Name)) {
        "platform/client/ui/tgf/tgf_profile_default".setBitmap();
        geTGF_profilePic.fitInParentAsBitmap();
    }
    %statusTextCtrl.previousProfileName = $Player::Name @ geTGF_tabs;
    geTGF_profilePic;
    %url = $Net::AvatarURL @ urlEncode($Player::Name) @ "?size=M";
    %url.downloadAndApplyBitmap();
    geTGF.main_sendRequests();
};
function geTGF::main_sendRequests(%this) {
    %statusTextCtrl = "emptyText".child(1);
    geTGF_main_people;
    %text = %statusTextCtrl.textLoading;
    %statusTextCtrl.setText(%text);
    sendRequest_GetOnlineUsers(60, "onDoneOrErrorCallback_GetOnlineUsers");
    "".setText();
    %statusTextCtrl = geTGF_main_happenings.getParent().child("emptyText", 1);
    geTGF_main_people_locationsText;
    %text = %statusTextCtrl.textLoading;
    %statusTextCtrl.setText(%text);
    sendRequest_GetMainHappenings(9, "onDoneOrErrorCallback_GetMainHappenings");
    WorldMap.refresh();
};
function onDoneOrErrorCallback_GetMainHappenings(%request, %unused) {
    if ((geTGF_tabs.getCurrentTab().name $= "main")) {
        cancel(geTGF_tabs.getCurrentTab().geTGF_Refresh_Schedule);
        1.setActive();
    }
    log("network", "debug", getScopeName() @ " " @ "- url =" @ " " @ %request.getURL());
    %itemType = "happening";
    geTGF_Refresh;
    "main".clearItemList(%itemType);
    if (!(%request.checkSuccess())) {
        %itemType.main_onGotDataOfType();
        return geTGF;
    }
    %listBase = "happenings";
    %count = %request.getResult(%listBase @ "Count");
    %n = 0;
    if ((%count < %n)) {
        %listItem = %listBase @ %n;
        %id = %request.getResult(%listItem @ ".hostUserName");
        %item = "main".createNewItem(%itemType, %id);
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
        %request.copyListValueIntoObject(%item, %listItem, "moreInfoURL");
        %request.copyListValueIntoObject(%item, %listItem, "apt");
        %request.copyListValueIntoObject(%item, %listItem, "location.areaName");
        %request.copyListValueIntoObject(%item, %listItem, "location.buildingName");
        %request.copyListValueIntoObject(%item, %listItem, "location.serverName");
        %item.subType = %request.getResult(%listItem @ ".type");
        %item.goThereVURL = vurlClearResolution(%item.goThereVURL);
        %n = (1.0 + %n);
    }
    "main".removeItemsWithFieldValueFromList(%itemType, "hostUserName", "The-Manager");
    %itemType.main_onGotDataOfType();
};
function onDoneOrErrorCallback_GetOnlineFriends_Main(%request) {
    if ((geTGF_tabs.getCurrentTab().name $= "main")) {
        cancel(geTGF_tabs.getCurrentTab().geTGF_Refresh_Schedule);
        1.setActive();
    }
    log("network", "debug", getScopeName() @ " " @ "- url =" @ " " @ %request.getURL());
    %itemType = "person";
    geTGF_Refresh;
    "main".clearItemList(%itemType);
    if (!(%request.checkSuccess())) {
        %itemType.main_onGotDataOfType();
        return geTGF;
    }
    %listBase = "friends";
    %count = %request.getValue(%listBase @ "Count");
    %n = 0;
    if ((%count < %n)) {
        %listItem = %listBase @ %n;
        %id = %request.getValue(%listItem @ ".userName");
        %item = "main".createNewItem(%itemType, %id);
        geTGF;
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
        %n = (1.0 + %n);
    }
    %itemType.main_onGotDataOfType();
};
function onDoneOrErrorCallback_GetOnlineUsers(%request) {
    log("network", "debug", getScopeName() @ " " @ "- url =" @ " " @ %request.getURL());
    %statusTextCtrl = "emptyText".child(1);
    geTGF_main_people;
    %statusTextCtrl.setText("");
    %itemType = "person";
    "main".clearItemList(%itemType);
    if (!(%request.checkSuccess())) {
        %itemType.main_onGotDataOfType();
        return geTGF;
    }
    %listBase = "user";
    %count = %request.getValue(%listBase @ "Count");
    %n = 0;
    if ((%count < %n)) {
        %listItem = %listBase @ %n;
        %id = %request.getValue(%listItem @ ".userName");
        %item = "main".createNewItem(%itemType, %id);
        geTGF;
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
        %n = (1.0 + %n);
    }
    %itemType.main_onGotDataOfType();
};
function onDoneOrErrorCallback_GetMainVenues(%request) {
    if ((geTGF_tabs.getCurrentTab().name $= "main")) {
        cancel(geTGF_tabs.getCurrentTab().geTGF_Refresh_Schedule);
        1.setActive();
    }
    log("network", "debug", "FAKE REQUEST: " @ getScopeName() @ " " @ "- url =" @ " " @ %request.getURL());
    %request.putValue("statusMsg", "fakeSuccess");
    %request.timeFinish = geTGF_Refresh @ getSimTime();
    geTGF;
    %itemType = "venue";
    "main".clearItemList(%itemType);
    if (!(%request.checkSuccess())) {
        %itemType.main_onGotDataOfType();
        return geTGF;
    }
    %listBase = "venues";
    %count = %request.getValue(%listBase @ "Count");
    %n = 0;
    if ((%count < %n)) {
        %listItem = %listBase @ %n;
        %id = %request.getValue(%listItem @ ".name");
        %item = "main".createNewItem(%itemType, %id);
        geTGF;
        %request.copyListValueIntoObject(%item, %listItem, "codeName");
        %request.copyListValueIntoObject(%item, %listItem, "fullName");
        %request.copyListValueIntoObject(%item, %listItem, "location.areaName");
        %request.copyListValueIntoObject(%item, %listItem, "location.buildingName");
        %request.copyListValueIntoObject(%item, %listItem, "location.serverName");
        %request.copyListValueIntoObject(%item, %listItem, "headline");
        %request.copyListValueIntoObject(%item, %listItem, "occupancy");
        %request.copyListValueIntoObject(%item, %listItem, "friendOccupancy");
        %request.copyListValueIntoObject(%item, %listItem, "goThereVURL");
        %n = (1.0 + %n);
    }
    %itemType.main_onGotDataOfType();
};
function geTGF::main_onGotDataOfType(%this, %type) {
    if ((%type $= "happening")) {
        // unhandled opcode 2109 at 0x00001F67
        %type = geTGF_main_happenings;
    }
    if ((%type $= "person")) {
        // unhandled opcode 2109 at 0x00001F78
        %type = geTGF_main_people;
        %control.onGotData();
        return;
    }
    error("unknown type:" @ " " @ %type @ " " @ getTrace());
    return;
    %list = %this.getItemList("main", %type);
    %num = %list.count();
    %control.setNumChildren(%num);
    %n = 0;
    if ((%num < %n)) {
        %cell = %control.getObject(%n);
        %item = %list.getValue(%n);
        %cell.Item = %item;
        %cell.setProfile();
        %cell.isInviteFriendsButton = ETSDarkBoxNonModalProfile @ 0;
        %control.updateCellFromItsItem(%cell);
        %n = (1.0 + %n);
    }
    %statusTextCtrl = %control.getParent().child("emptyText");
    (%num < %n);
    if ((0.0 <= %num)) {
        %text = %statusTextCtrl.textNothing;
    }
    %text = "";
    %statusTextCtrl.setText(%text);
};
function geTGF_main_happenings::updateCellFromItsItem(%this, %cell) {
    %item = %cell.Item;
    %cell.deleteMembers();
    0;
    %cont = new ""() {
        profile = GuiControl @ "EtsNonModalProfile";
        extent = (2.0 - getWord(%cell.getExtent(), 0)) @ " " @ (2.0 - getWord(%cell.getExtent(), 1));
        position = "1 1";
        horizSizing = "width";
        vertSizing = "top";
    };
    %cell.add(%cont);
    %w = getWord(%cell.getParent().childrenExtent, 0);
    %h = getWord(%cell.getParent().childrenExtent, 1);
    if ((%item.eventID $= "")) {
        %w = %h;
    }
    0;
    %ctrl = new ""() {
        profile = GuiBitmapCtrl @ "EtsNonModalProfile";
        extent = %w @ " " @ %h;
        position = "0 0";
        horizSizing = "width";
        vertSizing = "top";
        fitInParentAlign = 1;
    };
    %cont.add(%ctrl);
    %url = %item.baseImageURL;
    if ((%url $= "")) {
        %url = $Net::BuildDirPhotoURL @ urlEncode(%item.hostUserName);
    }
    %url = !(%url @ "?size=" @ " " @ %item.eventID $= "") ? "M" : "M";
    %ctrl.setBitmap("platform/client/ui/tgf/tgf_profile_default");
    %ctrl.fitInParentAsBitmap();
    %ctrl.downloadAndApplyBitmap(%url);
    %tmp = 20;
    0;
    %ctrl = new ""() {
        profile = GuiControl @ "EtsDarkBorderlessBoxProfile";
        extent = getWord(%cont.getExtent(), 0) @ " " @ %tmp;
        position = 0 @ " " @ (%tmp - getWord(%cont.getExtent(), 1));
        horizSizing = "width";
        vertSizing = "top";
    };
    %cont.add(%ctrl);
    0;
    %ctrl = new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiDefaultProfile";
        position = "-1 -1";
        extent = %cell.getExtent();
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_190x109";
        command = "geTGF.main_OnClickItem(\"" @ %item @ "\");";
    };
    %cont.add(%ctrl);
    0;
    %ctrl = new ""() {
        profile = GuiMLTextCtrl @ "ETSNonModalProfile";
        position = 2 @ " " @ (16.0 - getWord(%cont.getExtent(), 1));
        extent = (9.0 - getWord(%cont.getExtent(), 0)) @ " " @ 20;
        text = mlStyle("<just:left>" @ %item.headline @ "<just:right>", "tgfItem_headline");
        horizSizing = "width";
        vertSizing = "top";
        lineSpacing = 0;
    };
    %cont.add(%ctrl);
    if ((-(1.0) == %item.occupancy)) {
        %occupancyText = "";
    }
    if ((0.0 == %item.occupancy)) {
        %occupancyText = "(empty) ";
    }
    %occupancyText = "<b>" @ %item.occupancy @ " P ";
    0;
    %ctrl = new ""() {
        profile = GuiMLTextCtrl @ "ETSNonModalProfile";
        position = 0 @ " " @ (38.0 - getWord(%cont.getExtent(), 1));
        extent = (9.0 - getWord(%cont.getExtent(), 0)) @ " " @ 20;
        text = mlStyle("<just:left>" @ %item.hostUserName @ "<just:right><font:Arial:14>" @ %occupancyText, "tgfItem_host");
        horizSizing = "width";
        vertSizing = "top";
    };
    %cont.add(%ctrl);
    if (!(%item.eventID $= "")) {
        %bitmap = %item.getEventTypePostItTagBitmap();
        geTGF;
        0;
        %ctrl = new ""() {
            bitmap = GuiBitmapCtrl @ %bitmap;
            profile = "EtsNonModalProfile";
            extent = "82 19";
            position = (0.0 - (82.0 - getWord(%cont.getExtent(), 0))) @ " " @ 0;
            horizSizing = "left";
            vertSizing = "bottom";
        };
        %cont.add(%ctrl);
    }
};
function geTGF_main_people::onGotData(%this) {
    %list = "main".getItemList("person");
    geTGF;
    %num = %list.size();
    %statusTextCtrl = %this.child("emptyText");
    if ((0.0 <= %num)) {
    }
    %text = "";
    %statusTextCtrl.textNothing;
    %statusTextCtrl.setText(%text);
    if (!(isObject(geTGFGoRound))) {
        newTGFGoRound(geTGFGoRound).add();
        "".reparentSameSize();
        %statusTextCtrl.mDeetsMinWidth = 0 @ geTGFGoRound;
        geTGF_main_PeopleGoRound_Container;
        %statusTextCtrl.mLilThumbPadding = 5 @ geTGFGoRound;
        geTGFGoRound;
        %statusTextCtrl.mLilThumbHeight = geTGFGoRound.calcMaximumThumbHeight() @ geTGFGoRound;
        geTGF_main_PeopleGoRound_Container;
        %statusTextCtrl.mTickPeriodMS = 4500 @ geTGFGoRound;
        geTGFGoRound.rebuild();
    }
    %list.setItemList();
    %this.tryUpdateWorldmapSummaries();
};
function geTGF_main_people::tryUpdateWorldmapSummaries(%this) {
    "".setTextWithStyle();
    if (isObject(MapRequest)) {
        %this.schedule(1000, "tryUpdateWorldmapSummaries");
    }
    %areaNames = "lga nv rj pvt";
    geTGF_main_people_locationsText;
    %total = 0;
    %text = "";
    %delim = "";
    %n = 0;
    if ((getWordCount(%areaNames) < %n)) {
        %areaName = getWord(%areaNames, %n);
        if (!(%areaName $= "pvt")) {
            %userCount = %areaName.get().occupancy;
            WorldAreaSummaries;
        }
        %userCount = %areaName.get().totalOccupancy;
        %areaName @ WorldAreaSummaries;
        %link = "TGF_GOTO" @ " " @ %areaName;
        if ((%areaName $= "pvt")) {
            %areaName = "Personal Spaces";
        }
        %areaName = DestinationList::GetAreaNameUserFacingName(DestinationList::GetAreaNameCity(%areaName));
        %text = %text @ %delim @ "<spush><just:left><a:gamelink:" @ %link @ ">" @ %areaName @ "" @ "\t" @ "<just:right>" @ commaify(%userCount) @ "</a><spop>";
        %total = (%userCount + %total);
        %delim = "<br>";
        %n = (1.0 + %n);
    }
    %withS = (1.0 == %total) ? "" : "s";
    (getWordCount(%areaNames) < %n);
    %text = "<spush><just:center><font:arial:16>" @ commaify(%total) @ " vSider" @ %withS @ " In-World:<spop><br>" @ %text;
    %text = "<tab:100>" @ %text;
    %text.setTextWithStyle();
};
function geTGF_main_people_locationsText::onURL(%this, %url) {
    %cmd = firstWord(%url);
    if (!(%cmd $= "TGF_GOTO")) {
        error(getScopeName() @ " " @ "- unknown command" @ " " @ %cmd @ " " @ getTrace());
        return;
    }
    %areaName = restWords(%url);
    if ((%areaName $= "pvt")) {
        "HOTSPOTS".openToTabName();
    }
    if ((geTGF @ " " @ %areaName $= "lga")) {
        "MAP".openToTabName();
        %areaName.getCityButton(1, 0).performClick();
    }
    if ((WorldMap @ " " @ %areaName $= "nv")) {
        "MAP".openToTabName();
        %areaName.getCityButton(1, 0).performClick();
    }
    if ((WorldMap @ " " @ %areaName $= "rj")) {
        "MAP".openToTabName();
        %areaName.getCityButton(1, 0).performClick();
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
        MissionCleanup.add(MOTDRequest);
    }
    %url = $Net::ClientServiceURL @ "/GlobalMessage";
    %url = %url @ "?user=" @ urlEncode($Player::Name);
    %url = %url @ "&token=" @ urlEncode($Token);
    %url = %url @ "&type=" @ urlEncode("motd");
    log("communication", "debug", "sending request to get current motd: " @ %url);
    %url.setURL();
    MOTDRequest.start();
};
function MOTDRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        geTGF_tabs.onMOTDPostFailed();
        log("communication", "warn", "MOTDRequest::onDone status: " @ %status);
    }
    %text = %this.getValue("message");
    if (!(%text $= "")) {
        %text = decodeMOTDString(%text);
        %text.setText();
        %areaName.get().qotdID = "" @ MOTDText;
        MOTDText;
    }
    geTGF_tabs.onMOTDPostFailed();
};
function geTGF_tabs::refreshQOTD(%this) {
    if (isObject(QOTDRequest)) {
        QOTDRequest.delete();
    }
    new ManagerRequest(QOTDRequest);
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(QOTDRequest);
    }
    %url = $Net::ClientServiceURL @ "/GlobalMessage";
    %url = %url @ "?user=" @ urlEncode($Player::Name);
    %url = %url @ "&token=" @ urlEncode($Token);
    %url = %url @ "&type=" @ urlEncode("qotd");
    log("communication", "debug", "sending request to get current qotd: " @ %url);
    %url.setURL();
    QOTDRequest.start();
};
function QOTDRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    %text = "";
    if ((%status $= "success")) {
        %text = %this.getValue("message");
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
        %text.setText();
        %areaName.get().qotdID = %qID @ MOTDText;
        MOTDText;
    }
    geTGF_tabs.refreshMOTD();
};
function MOTDText::onURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %url = restWords(%url);
    }
    if ((0.0 != strstr(%url, "answer:"))) {
        Parent::onURL(%this, %url);
        return;
    }
    %answer = getSubStr(%url, strlen("answer:"), 100000);
    %trackURL = "";
    %trackURL = %trackURL @ "/client/qotd";
    %trackURL = %trackURL @ "/" @ %this.qotdID;
    %trackURL = %trackURL @ "/" @ %answer;
    %analytic = getAnalytic();
    %analytic.trackPageView(%trackURL);
    %this.setText();
    1000.schedule("refreshMOTD");
    $UserPref::QOTD::answered = $UserPref::QOTD::answered @ " " @ %this.qotdID;
    geTGF_tabs;
};
function geTGF_tabs::onMOTDPostFailed(%this) {
    "".setText();
    %fileName = "projects/common/defaultMOTD.txt";
    MOTDText;
    %fo = new ""();;
    FileObject;
    if (%fo.openForRead(%fileName)) {
        %text = "";
        0;
        if (!(%fo.isEOF())) {
            %text = %text @ %fo.readLine() @ "\n";
        }
        %text.setText();
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
    %this.DoDetails("main", %item);
};
function geTGF::main_GetAndOpenDetailsContainer(%this, %item) {
    %this.constructDeetsWindow(%item);
    1.setVisible();
};
