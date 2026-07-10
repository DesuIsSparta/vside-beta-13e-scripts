if (!(isObject(BuddyHudTabs))) {
    new ScriptObject(BuddyHudTabs) {
        class = "TabControl";
    };
    if (isObject(MissionCleanup)) {
        BuddyHudTabs.add(MissionCleanup);
    }
}
BuddyHudTabs.vars = 0 @ "columnPadding";
BuddyHudTabs.vars = 0 @ "rowPadding";
BuddyHudTabs.vars = 22 @ "offlineFriendsTextboxHeight";
BuddyHudTabs.vars = 28 @ "buddyListLegendTextboxHeight";
BuddyHudTabs.vars = ColorIToHex($NameColorFriend) @ "normalTextColor";
BuddyHudTabs.vars = ColorFToHex(ColorConvolve($NameColorFriendF, $NameColorIdleModulationF)) @ "idleTextColor";
BuddyHudTabs.vars = ColorIToHex($NameColorHilite) @ "hilitedTextColor";
function BuddyHudTabs::setup(%this) {
    if (!(%this.initialized)) {
        "horizontal".Initialize(%this, BuddyHudTabContainer, "25 25", "platform/client/ui/separator", "16 7");
        "platform/client/buttons/buddies".newTab(%this, "friends");
        "platform/client/buttons/pending".newTab(%this, "requests");
        "platform/client/buttons/aim_buddies".newTab(%this, "AIM");
        "platform/client/buttons/uhoh".newTab(%this, "UhOh");
        "friends".selectTabWithName(%this);
        %this.fillFriendsTab();
        %this.fillRequestsTab();
        %this.fillAIMTab();
        %this.fillUhOhTab();
        gSetField(BuddyHudWin, favoritesTimer, 0);
        BuddyHudWin.refreshAIMBuddyList();
        new GuiBitmapButtonCtrl("") {
            position = "115 4";
            extent = "42 21";
            bitmap = "platform/client/buttons/inviteFriends";
            command = "doInviteFriends();";
        };.add(BuddyHudTabContainer, new GuiMLTextCtrl("") {
            position = "5 2";
            extent = "36 16";
            profile = "ETSNonModalProfile";
            text = "<color:ddffdd><font:arial:16>Invite!</a>";
        };);
        AIMLoginFrame.setup();
    }
};
function BuddyHudTabs::OnETSInviteFriends(%this) {
    if (!(isObject(EtsInviteDialog))) {
        error("no EtsInviteDialog, this should not happen");
        return;
    }
    if (!(EtsInviteDialog.isVisible())) {
        EtsInviteDialog.open();
    }
};
function BuddyHudTabs::fillFriendsTab(%this) {
    %theTab = "friends".getTabWithName(%this);
    %scroll = new GuiScrollCtrl("") {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) @ " " @ "buddyListLegendTextboxHeight" @ (getWord(%theTab.getExtent(), 1) - (BuddyHudTabs.vars + BuddyHudTabs.vars @ "offlineFriendsTextboxHeight"));
        minExtent = "10 10";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    };
    %friendsList = new GuiArray2Ctrl("") {
        profile = "CSProfileListBox";
        childrenClassName = "GuiMouseEventCtrl";
        childrenExtent = "16 18";
        spacing = "rowPadding" @ BuddyHudTabs.vars;
        numRowsOrCols = 1;
        inRows = 0;
        hilited = 0;
        unselectedProfile = "CSProfileModelListingUnselected";
        selectedProfile = "CSProfileModelListingSelected";
        menuTextProfile = "ETSSmallTextNonModalListProfile";
        menuTextSelectedProfile = "ETSSmallTextNonModalListProfile";
        lastClicked = 0;
        paddingAboveText = 0;
        horizSizing = "width";
        vertSizing = "bottom";
        position = "columnPadding" @ (0.0 - BuddyHudTabs.vars) @ " " @ 0;
        extent = %scroll.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        scroll = %scroll;
    };
    "MenuControl".bindClassName(%friendsList);
    "TabbedTextControl".bindClassName(%friendsList);
    "BuddyHudFriendsList".bindClassName(%friendsList);
    "BuddyHudFriendsList".setName(%friendsList);
    %fieldWidths = "10 94 46";
    %friendsList.teleFieldIndex = 0;
    %friendsList.nameFieldIndex = 1;
    %friendsList.cityFieldIndex = 2;
    BuddyHudTabs.vars.setFieldWidths(%friendsList, %fieldWidths, "columnPadding");
    %friendsList.clear();
    %friendsList.add(%scroll);
    %scroll.add(%theTab);
    new GuiMLTextCtrl(BuddyHudFriendsListOfflineFriendsBox) {
        profile = "ETSSmallTextListProfile";
        horizSizing = "width";
        vertSizing = "top";
        position = 4 @ " " @ "buddyListLegendTextboxHeight" @ (getWord(%theTab.getExtent(), 1) - (BuddyHudTabs.vars + BuddyHudTabs.vars @ "offlineFriendsTextboxHeight"));
        extent = getWord(%theTab.getExtent(), 0) @ " " @ "offlineFriendsTextboxHeight" @ BuddyHudTabs.vars;
        minExtent = "offlineFriendsTextboxHeight" @ BuddyHudTabs.vars @ " " @ "offlineFriendsTextboxHeight" @ BuddyHudTabs.vars;
        sluggishness = -1;
        visible = 1;
        text = "";
    };.add(%theTab);
    new GuiMLTextCtrl(BuddyHudFriendsListLegendContainer) {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "top";
        position = 4 @ " " @ "buddyListLegendTextboxHeight" @ (getWord(%theTab.getExtent(), 1) - BuddyHudTabs.vars);
        extent = getWord(%theTab.getExtent(), 0) @ " " @ "buddyListLegendTextboxHeight" @ BuddyHudTabs.vars;
        minExtent = "buddyListLegendTextboxHeight" @ BuddyHudTabs.vars @ " " @ "buddyListLegendTextboxHeight" @ BuddyHudTabs.vars;
    };.add(%theTab);
    new GuiScrollCtrl(BuddyHudFriendsInfo) {
        profile = new GuiBitmapCtrl("") {
        profile = new GuiMLTextCtrl(BuddyHudFriendsListLegend) {
        profile = "ETSTinyTextListProfile";
        horizSizing = "width";
        vertSizing = "top";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) @ " " @ "buddyListLegendTextboxHeight" @ BuddyHudTabs.vars;
        minExtent = "buddyListLegendTextboxHeight" @ BuddyHudTabs.vars @ " " @ "buddyListLegendTextboxHeight" @ BuddyHudTabs.vars;
        sluggishness = -1;
        visible = 1;
        text = "<tab:20,90>" @ "<spush><color:" @ "idleTextColor" @ BuddyHudTabs.vars @ ">Dim<spop>" @ "\t" @ "= Idle" @ "\t" @ "... = In Transit" @ "\n" @ " " @ "\t" @ "= Fast Teleport";
    }; @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "6 15";
        extent = "6 9";
        minExtent = "6 9";
        visible = 1;
        bitmap = "platform/client/ui/friendsHud_lightning_n";
    }; @ "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "3 2";
        extent = (getWord(%theTab.getExtent(), 0) - 3.0) @ " " @ (getWord(%theTab.getExtent(), 1) - 24.0);
        minExtent = "10 10";
        sluggishness = -1;
        visible = 0;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    };.add(%theTab, new GuiMLTextCtrl("") {
        profile = "ETSTextListProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "146 68";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = $MsgCat::favorites["NO-FAVS-MSG"];
    };);
    if (showInviteFriend()) {
        %inviteButton = new GuiVariableWidthButtonCtrl(ETSInviteButton) {
            profile = "BracketButton15Profile";
            horizSizing = "right";
            vertSizing = "top";
            position = 8 @ " " @ (getWord(%theTab.getExtent(), 1) - 18.0);
            extent = "140 15";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            command = "BuddyHudTabs.OnETSInviteFriends();";
            text = "Invite Friends To Join";
            groupNum = -1;
            buttonType = "PushButton";
        };
        %inviteButton.add(%theTab);
    }
};
function BuddyHudTabs::fillRequestsTab(%this) {
    %theTab = "requests".getTabWithName(%this);
    %requestsList = new GuiMLTextCtrl("") {
        profile = "ETSTextListProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = "500 80";
        minExtent = "80 80";
        sluggishness = -1;
        visible = 1;
    };
    "BuddyHudPlayerList".bindClassName(%requestsList);
    "BuddyHudRequestsList".setName(%requestsList);
    "BuddyHudRequestsList".bindClassName(%requestsList);
    %scroll = new GuiScrollCtrl("") {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) @ " " @ (getWord(%theTab.getExtent(), 1) - 24.0);
        minExtent = "10 10";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    };
    %requestsList.add(%scroll);
    %scroll.add(%theTab);
    new GuiScrollCtrl(BuddyHudRequestsInfo) {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "3 2";
        extent = (getWord(%theTab.getExtent(), 0) - 3.0) @ " " @ (getWord(%theTab.getExtent(), 1) - 24.0);
        minExtent = "10 10";
        sluggishness = -1;
        visible = 0;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    };.add(%theTab, new GuiMLTextCtrl("") {
        profile = "ETSTextListProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "146 68";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = $MsgCat::favorites["NO-FRIEND-REQ-MSG"];
    };);
};
function BuddyHudTabs::fillAIMTab(%this) {
    %theTab = "AIM".getTabWithName(%this);
    if (!(isObject(%theTab))) {
        echo("Didn't find AIM Buddies tab");
        return 0;
    }
    $Player::AIMName = "";
    $Player::AIMPassword = "";
    %loginFrame = new GuiControl(AIMLoginFrame) {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "4 0";
        extent = "153 190";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    new GuiVariableWidthButtonCtrl(AIMSignInButton) {
        profile = new GuiCheckBoxCtrl(AIMAutoSigninCheckbox) {
        profile = new GuiCheckBoxCtrl(AIMSavePasswordCheckbox) {
        profile = new GuiCheckBoxCtrl(AIMRememberMeCheckbox) {
        profile = new GuiTextEditCtrl(AIMPasswordField) {
        profile = new GuiTextCtrl("") {
        profile = new GuiTextEditCtrl(AIMScreenNameField) {
        profile = new GuiTextCtrl("") {
        profile = new GuiMLTextCtrl("") {
        profile = "ETSShadowTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = "153 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "<a:http://www.aim.com/>Get an AIM Screen Name</a>";
    }; @ "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 20";
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Screen Name:";
        maxLength = 64;
    }; @ "ETSDarkTabbableTextEditProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 40";
        extent = "150 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "Player::AIMName";
        altCommand = "AIMLoginFrame.nextControl(AIMScreenNameField);";
        maxLength = 32;
        historySize = 0;
        password = 0;
        tabComplete = 0;
        sinkAllKeyEvents = 0;
    }; @ "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 60";
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Password:";
        maxLength = 64;
    }; @ "ETSDarkTabbableTextEditProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 80";
        extent = "150 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "Player::AIMPassword";
        altCommand = "AIMLoginFrame.nextControl(AIMPasswordField);";
        maxLength = 32;
        historySize = 0;
        password = 1;
        tabComplete = 0;
        sinkAllKeyEvents = 0;
    }; @ "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 105";
        extent = "94 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "UserPref::AIM::RememberMe";
        command = "AIMLoginFrame.update();";
        text = "Remember Me";
        groupNum = -1;
        buttonType = "ToggleButton";
    }; @ "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 125";
        extent = "94 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "UserPref::AIM::SavePassword";
        command = "AIMLoginFrame.update();";
        text = "Save Password";
        groupNum = -1;
        buttonType = "ToggleButton";
    }; @ "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 145";
        extent = "94 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "UserPref::AIM::AutoSignin";
        command = "AIMLoginFrame.update();";
        text = "Auto Sign In";
        groupNum = -1;
        buttonType = "ToggleButton";
    }; @ "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "95 169";
        extent = "54 15";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "doAIMSignIn();";
        text = "Sign In";
        groupNum = -1;
        buttonType = "PushButton";
    };
    %aimListScroll = new GuiScrollCtrl("") {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) @ " " @ (getWord(%theTab.getExtent(), 1) - 24.0);
        minExtent = "10 10";
        sluggishness = -1;
        visible = 0;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        childMargin = "-4 -1";
        helpTag = 0;
    };
    new GuiTextListCtrl(AIMBuddyList) {
        profile = "AIMTextListProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = "150 80";
        minExtent = "80 80";
        sluggishness = -1;
        visible = 1;
        command = "";
        altCommand = "AIMBuddyList.messageSelected();";
        enumerate = 1;
        resizeCell = 1;
        columns = 1;
        fitParentWidth = 1;
        clipColumnText = 1;
    };
    %signOffButton = new GuiVariableWidthButtonCtrl(AIMSignOffButton) {
        profile = "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "top";
        position = 93 @ " " @ (getWord(%theTab.getExtent(), 1) - 22.0);
        extent = "54 15";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        command = "doAIMSignOff();";
        text = "Sign Off";
        groupNum = -1;
        buttonType = "PushButton";
    };
    %inviteButton = new GuiVariableWidthButtonCtrl(AIMInviteButton) {
        profile = "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "top";
        position = 8 @ " " @ (getWord(%theTab.getExtent(), 1) - 22.0);
        extent = "79 15";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        command = "AIMInviteButton.do();";
        text = "Invite Buddy";
        groupNum = -1;
        buttonType = "PushButton";
    };
    %theTab.loginFrame = %loginFrame;
    %loginFrame.add(%theTab);
    %theTab.aimListScroll = %aimListScroll;
    %aimListScroll.add(%theTab);
    %theTab.signOffButton = %signOffButton;
    %signOffButton.add(%theTab);
    %theTab.inviteButton = %inviteButton;
    %inviteButton.add(%theTab);
};
function BuddyHudTabs::wakeUp(%this) {
    %this.setup();
    %this.selectCurrentTab();
    1.setActive(AIMSignInButton);
};
function AIMLoginFrame::nextControl(%this, %curControl) {
    %nextControl = "";
    %this.update();
    if ((%curControl.getName() $= AIMScreenNameField)) {
        %nextControl = AIMPasswordField;
    }
    if ((%curControl.getName() $= AIMPasswordField)) {
        %nextControl = AIMSignInButton;
    }
    if ((%nextControl $= "")) {
        error("nextControl got invalid arg" @ " " @ %curControl);
        return;
    }
    if ((%nextControl $= AIMSignInButton)) {
        doAIMSignIn();
    }
    1.makeFirstResponder(%nextControl);
    %nextControl.selectAll();
};
function BuddyHudTabs::fillUhOhTab(%this) {
    %theTab = "UhOh".getTabWithName(%this);
    if (!(isObject(%theTab))) {
        echo("Didn't find UhOh tab");
        return 0;
    }
    %scroll = new GuiScrollCtrl("") {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) @ " " @ (getWord(%theTab.getExtent(), 1) - 24.0);
        minExtent = "10 10";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    };
    %body = new GuiMLTextCtrl("") {
        profile = "ETSTextListProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = (getWord(%scroll.getExtent(), 0) - 10.0) @ " " @ (getWord(%scroll.getExtent(), 1) - 10.0);
        minExtent = "10 10";
        sluggishness = -1;
        visible = 1;
        text = $MsgCat::UI["TOOMANYFRIENDS"];
    };
    %scroll.add(%theTab);
    %body.add(%scroll);
};
function BuddyHudTabs::setUhOhTabVisibility(%this) {
    %showIt = (UserListFriends.size() > 250.0);
    if (%showIt) {
        "UhOh".showTabWithName(%this);
    }
    "UhOh".hideTabWithName(%this);
};
function SimGroup::hasObjectWithName(%this, %name) {
    %i = 0;
    while ((%i < %this.getCount())) {
        if ((%i.getObject(%this).name $= %name)) {
            return 1;
        }
        %i = (%i + 1.0);
    }
    return 0;
};
function BuddyHudWin::wakeUp(%this) {
    BuddyHudTabs.wakeUp();
};
function BuddyHudWin::addBuddy(%this, %buddy) {
    %buddy.addRow(AIMBuddyList, 0);
};
function BuddyHudWin::onlineBuddiesToString(%this) {
    %count = aimBuddyCount();
    %onlineBuddies = "";
    %n = 0;
    while ((%n < %count)) {
        %state = aimGetBuddyState(%n);
        if ((%state < 1.0)) {
        }
        if ((%state > 3.0)) {
        }
        if (!(%onlineBuddies $= "")) {
            %onlineBuddies = %onlineBuddies @ "\t" @ aimGetBuddyName(%n);
        }
        %onlineBuddies = aimGetBuddyName(%n);
        %n = (%n + 1.0);
    }
    return %onlineBuddies;
};
function BuddyMap::addToAIMList(%this, %key, %value) {
    %value.addRow(AIMBuddyList, AIMBuddyList.rowCount());
};
function BuddyHudWin::refreshAIMBuddyList(%this) {
    if (isObject(AIMBuddyList)) {
        %onlineList = new StringMap("") {
            class = "BuddyMap";
            ignoreCase = 1;
        };
        %idleAwayList = new StringMap("") {
            class = "BuddyMap";
            ignoreCase = 1;
        };
        %offlineList = new StringMap("") {
            class = "BuddyMap";
            ignoreCase = 1;
        };
        if (isObject(MissionCleanup)) {
            %onlineList.add(MissionCleanup);
            %idleAwayList.add(MissionCleanup);
            %offlineList.add(MissionCleanup);
        }
        AIMBuddyList.clear();
        %buddyCount = aimBuddyCount();
        %i = 0;
        while ((%i < %buddyCount)) {
            %buddyName = aimGetBuddyName(%i);
            %buddyState = aimGetBuddyState(%i);
            if ((%buddyState == -(1.0))) {
                %tag = "\x10\x06";
                %tag @ %buddyName @ "\x11".put(%offlineList, %buddyName);
            }
            if ((%buddyState == 0.0)) {
                %tag = "\x10\x06";
                %tag @ %buddyName @ "\x11".put(%offlineList, %buddyName);
            }
            if ((%buddyState == 1.0)) {
                %tag = "\x10\x07";
                %tag @ %buddyName @ "\x11".put(%onlineList, %buddyName);
            }
            if ((%buddyState == 2.0)) {
                %tag = "\x10\x0B";
                %tag @ %buddyName @ "\x11".put(%idleAwayList, %buddyName);
            }
            if ((%buddyState == 3.0)) {
                %tag = "\x10\x0C";
                %tag @ %buddyName @ "\x11".put(%idleAwayList, %buddyName);
            }
            %buddyName.put(%offlineList, %buddyName);
            %i = (%i + 1.0);
        }
        "addToAIMList".forEach(%onlineList);
        "addToAIMList".forEach(%idleAwayList);
        "addToAIMList".forEach(%offlineList);
        %onlineList.delete();
        %idleAwayList.delete();
        %offlineList.delete();
    }
};
function BuddyHudWin::clearSelections(%this) {
    if (isObject(AIMBuddyList)) {
        -(1.0).setSelectedRow(AIMBuddyList);
    }
};
BuddyHudWin.STATE_PARSE_RESULT = 1;
BuddyHudWin.STATE_PARSE_BUDDIES = 2;
function BuddyHudFriendsList::onCreatedChild(%this, %child) {
    Parent::onCreatedChild(%this, %child);
    %position = %child.getPosition();
    %extent = %child.getExtent();
    %newHeight = (getWord(%extent, 1) + %this.spacing);
    %newHeight.resize(%child, getWord(%position, 0), getWord(%position, 1), getWord(%extent, 0));
    if (!(getWord(%child.getNamespaceList(), 0) $= "BuddyHudFriendsListLine")) {
        "BuddyHudFriendsListLine".bindClassName(%child);
    }
};
function BuddyHudFriendsListLine::updateText(%this, %newText) {
    %fieldCount = getFieldCount(%newText);
    %objectCount = %this.getCount();
    if ((%objectCount < %fieldCount)) {
        %fieldCount = %objectCount;
    }
    %i = 0;
    while ((%i < %fieldCount)) {
        getField(%newText, %i).setText(%i.getObject(%this));
        %i = (%i + 1.0);
    }
};
function BuddyHudFriendsListLine::onMouseEnter(%this) {
    %this.entryHilited.updateText(%this);
};
function BuddyHudFriendsListLine::onMouseLeave(%this) {
    %this.entryDefault.updateText(%this);
};
function BuddyHudFriendsListLine::onMouseDown(%this) {
};
function BuddyHudFriendsListLine::onMouseUp(%this) {
    %name = unmunge(%this.linkText);
    onLeftClickPlayerName(%name, "");
};
function BuddyHudFriendsListLine::onRightMouseUp(%this) {
    %name = unmunge(%this.linkText);
    onRightClickPlayerName(%name);
};
$gBuddyListTitles["FriendsOnline"] = "";
$gBuddyListTitles["WaitingForYourApproval"] = "Waiting For Your Approval";
$gBuddyListTitles["YourPendingRequests"] = "Requests by You";
$gBuddyListTitles["InTransit"] = "In Transit";
$gBuddyListLastPopulateTime = "";
$gBuddyListPopulateTimer = "";
$gBuddyListMinRepopulatePeriodMS = 4000;
function BuddyHudWin::populateBuddyLists(%this) {
    %doit = 0;
    if (($gBuddyListLastPopulateTime $= "")) {
        %doit = 1;
    }
    %timeSinceLastPopulate = mSubS32(getSimTime(), $gBuddyListLastPopulateTime);
    if ((%timeSinceLastPopulate > $gBuddyListMinRepopulatePeriodMS)) {
        %doit = 1;
    }
    if (!($gBuddyListPopulateTimer $= "")) {
        cancel($gBuddyListPopulateTimer);
    }
    $gBuddyListPopulateTimer = "populateBuddyLists".schedule(%this, $gBuddyListMinRepopulatePeriodMS);
    if (%doit) {
        if (!($gBuddyListPopulateTimer $= "")) {
            cancel($gBuddyListPopulateTimer);
        }
        $gBuddyListPopulateTimer = "";
        %this.populateBuddyListsReally();
        $gBuddyListLastPopulateTime = getSimTime();
    }
};
function BuddyHudWin::populateBuddyListsReally(%this) {
    %timeIt = !(1);
    if (%timeIt) {
        %startTime = getSimTime();
        %lastTime = getSimTime();
    }
    if (!(isObject(BuddyHudFriendsList))) {
    }
    if (!(isObject(BuddyHudRequestsList))) {
        return;
    }
    %vipRoleMasks = roles::getRolesMaskFromStrings("staff moderator celeb");
    "FrndsOnline".initializeBuddyList(%this);
    "FrndsHere".initializeBuddyList(%this);
    "FrndsNPC".initializeBuddyList(%this);
    "FrndsThere".initializeBuddyList(%this);
    "FrndsInTransit".initializeBuddyList(%this);
    "FrndsOffline".initializeBuddyList(%this);
    "FavesHere".initializeBuddyList(%this);
    "FavesNPC".initializeBuddyList(%this);
    "FavesThere".initializeBuddyList(%this);
    "FavesOffline".initializeBuddyList(%this);
    "FansHere".initializeBuddyList(%this);
    "FansNPC".initializeBuddyList(%this);
    "FansThere".initializeBuddyList(%this);
    "FansOffline".initializeBuddyList(%this);
    if (%timeIt) {
        error(getScopeName() @ " " @ "- A" @ " " @ formatFloat("%8.3f", ((getSimTime() - %startTime) / 1000.0)) @ " " @ formatFloat("%8.3f", ((getSimTime() - %lastTime) / 1000.0)));
        %lastTime = getSimTime();
    }
    %heightOfRowInFriendsList = 0;
    %offsetForVerticalPositionOfList = 0;
    if ((BuddyHudFriendsList.getCount() > 0.0)) {
        %verticalPositionOfList = mMax((0.0 - getWord(BuddyHudFriendsList.getPosition(), 1)), 0);
        %heightOfRowInFriendsList = getWord(0.getObject(BuddyHudFriendsList).getExtent(), 1);
        %indexOfNameAtTopOfFriendsList = mCeil((%verticalPositionOfList / %heightOfRowInFriendsList));
        %offsetForVerticalPositionOfList = ((%indexOfNameAtTopOfFriendsList * %heightOfRowInFriendsList) - %verticalPositionOfList);
        %nameAtTopOfFriendsList = StripMLControlChars(1.getObject(%indexOfNameAtTopOfFriendsList.getObject(BuddyHudFriendsList)).getText());
        %nameAtTopOfFriendsListisNPC = %indexOfNameAtTopOfFriendsList.getObject(BuddyHudFriendsList).isNPCEntry;
    }
    %nameAtTopOfFriendsList = "";
    %nameAtTopOfFriendsListisNPC = 0;
    %numberOfNamesInsertedAbove = 0;
    BuddyHudFriendsList.startingPos = BuddyHudFriendsList.getPosition();
    BuddyHudFriendsList.deleteMembers();
    %botsFriendsList = new StringMap("") {
        ignoreCase = "true";
    };
    %botsFriendsList.clear();
    if (isObject(MissionCleanup)) {
        %botsFriendsList.add(MissionCleanup);
    }
    BuddyHudRequestsList.startingPos = BuddyHudRequestsList.getPosition();
    "".setText(BuddyHudRequestsList);
    if (%timeIt) {
        error(getScopeName() @ " " @ "- B" @ " " @ formatFloat("%8.3f", ((getSimTime() - %startTime) / 1000.0)) @ " " @ formatFloat("%8.3f", ((getSimTime() - %lastTime) / 1000.0)));
        %lastTime = getSimTime();
    }
    %friendCount = UserListFriends.size();
    if (0) {
        %includeInListCount = 0;
        %i = 0;
        while ((%i < %friendCount)) {
            %friend = %i.getValue(UserListFriends);
            if (!(%friend.serverName $= "")) {
            }
            if (%friend.loggedIn) {
                %includeInListCount = (%includeInListCount + 1.0);
            }
            %i = (%i + 1.0);
        }
        %includeInListCount.setNumChildren(BuddyHudFriendsList);
    }
    if (%timeIt) {
        error(getScopeName() @ " " @ "- X" @ " " @ formatFloat("%8.3f", ((getSimTime() - %startTime) / 1000.0)) @ " " @ formatFloat("%8.3f", ((getSimTime() - %lastTime) / 1000.0)));
        %lastTime = getSimTime();
        (%i < %friendCount);
    }
    %friendCountOnline = 0;
    %i = 0;
    while ((%i < %friendCount)) {
        %friend = %i.getValue(UserListFriends);
        %includeInList = 1;
        %cityTagDefault = "";
        %cityTagHilited = "";
        %sameServerTagDefault = "";
        %sameServerTagHilited = "";
        %ghost = Player::findPlayerInstance(%friend.name);
        if (!(%ghost $= "")) {
        }
        if (isObject(%ghost)) {
            1.setBuddy(%ghost);
            1.setAmFave(%ghost);
        }
        if (%friend.isNPC) {
            %listName = "FrndsNPC";
        }
        if ((%friend.serverName $= "")) {
            if (%friend.loggedIn) {
                %listName = "FrndsInTransit";
                %cityTagDefault = "<bitmap:platform/client/ui/friendsHud_transition_n>";
                %cityTagHilited = "<bitmap:platform/client/ui/friendsHud_transition_h>";
            }
            %listName = "FrndsOffline";
            %includeInList = 0;
        }
        if ((%friend.serverName $= $ServerName)) {
            %listName = "FrndsHere";
        }
        %listName = "FrndsThere";
        %list = %this.buddyLists;
        %listName;
        if (!(isObject(%list))) {
            log(relations, error, "list not defined:" @ " " @ %listName);
        }
        "placeholder".put(%list, %friend.name);
        if (%includeInList) {
            if ((%friend.serverName $= $ServerName)) {
                if (%friend.isIdle) {
                    %sameServerTagDefault = "<modulationColor:ffffffa0><bitmap:platform/client/ui/friendsHud_lightning_i>";
                }
                %sameServerTagDefault = "<modulationColor:ffffffa0><bitmap:platform/client/ui/friendsHud_lightning_n>";
                %sameServerTagHilited = "<modulationColor:ffffffa0><bitmap:platform/client/ui/friendsHud_lightning_h>";
            }
            %sameServerTagDefault = "";
            %sameServerTagHilited = "";
            if (%friend.name.getIgnoreStatus(BuddyHudWin)) {
                %ignoreTag = "<strikethrough>";
            }
            %ignoreTag = "";
            if (%friend.isNPC) {
                %npcOpen = "";
                %npcClose = " (bot)";
            }
            %npcOpen = "";
            %npcClose = "";
            if ((%cityTagDefault $= "")) {
                %cityBitmap = DestinationList::GetAreaNameIconPath(%friend.csn);
                %cityTagDefault = "<modulationColor:ffffffa0><bitmap:" @ %cityBitmap @ %friend.isIdle ? "_i" : "_n" @ ">";
                %cityTagHilited = "<modulationColor:ffffffc0><bitmap:" @ %cityBitmap @ "_h>";
            }
            %boldTag = "";
            if (%friend.isIdle) {
            }
            %colorTagDefault = BuddyHudTabs.vars @ "normalTextColor" @ BuddyHudTabs.vars @ ">";
            "<color:" @ "idleTextColor";
            %colorTagHilited = "<color:" @ "hilitedTextColor" @ BuddyHudTabs.vars @ ">";
            %visibleName = %npcOpen @ %friend.name @ %npcClose;
            %friendNameTagDefault = "<clip:100>" @ %ignoreTag @ %boldTag @ %colorTagDefault @ %visibleName @ "</a></clip>";
            %friendNameTagHilited = "<clip:100>" @ %ignoreTag @ %boldTag @ %colorTagHilited @ %visibleName @ "</a></clip>";
            if ((%friend.activities $= "")) {
            }
            %activityName = getField(%friend.activities, 0);
            "";
            %activityBitmapMLText = %activityName.getActivityBitmapMLText(getUserActivityMgr());
            %activityColorDefault = %friend.isIdle ? "ffffff70" : "ffffff80";
            %activityColorHilited = BuddyHudTabs.vars;
            "hilitedTextColor";
            %activityDefault = "<modulationColor:" @ %activityColorDefault @ ">" @ %activityBitmapMLText;
            %activityHilited = "<modulationColor:" @ %activityColorHilited @ ">" @ %activityBitmapMLText;
            %entryDefault = %sameServerTagDefault @ "\t" @ %friendNameTagDefault @ "\t" @ %cityTagDefault @ %activityDefault;
            %entryHilited = %sameServerTagHilited @ "\t" @ %friendNameTagHilited @ "\t" @ %cityTagHilited @ %activityHilited;
            %comparisonToNameAtTopOfList = stricmp(%visibleName, %nameAtTopOfFriendsList);
            if (%friend.isNPC) {
                %entryInfo = new SimObject("");
                %entryInfo.entryDefault = %entryDefault;
                %entryInfo.entryHilited = %entryHilited;
                %entryInfo.name = %friend.name;
                %entryInfo.put(%botsFriendsList, %friend.name);
                if (%nameAtTopOfFriendsListisNPC) {
                }
                if ((%comparisonToNameAtTopOfList < 0.0)) {
                    %numberOfNamesInsertedAbove = (%numberOfNamesInsertedAbove + 1.0);
                }
            }
            %line = %entryDefault.addLineNoReseat(BuddyHudFriendsList);
            %line.entryDefault = %entryDefault;
            %line.entryHilited = %entryHilited;
            %line.linkText = munge(%friend.name);
            %line.isNPCEntry = 0;
            if (%nameAtTopOfFriendsListisNPC) {
            }
            if ((%comparisonToNameAtTopOfList < 0.0)) {
                %numberOfNamesInsertedAbove = (%numberOfNamesInsertedAbove + 1.0);
            }
            %friendCountOnline = (%friendCountOnline + 1.0);
        }
        %i = (%i + 1.0);
    }
    if (%timeIt) {
        error(getScopeName() @ " " @ "- C" @ " " @ formatFloat("%8.3f", ((getSimTime() - %startTime) / 1000.0)) @ " " @ formatFloat("%8.3f", ((getSimTime() - %lastTime) / 1000.0)));
        %lastTime = getSimTime();
        (%i < %friendCount);
    }
    %botFriendsCount = %botsFriendsList.size();
    %i = 0;
    while ((%i < %botFriendsCount)) {
        %entryInfo = %i.getValue(%botsFriendsList);
        %line = %entryInfo.entryDefault.addLineNoReseat(BuddyHudFriendsList);
        %line.entryDefault = %entryInfo.entryDefault;
        %line.entryHilited = %entryInfo.entryHilited;
        %line.linkText = munge(%entryInfo.name);
        %line.isNPCEntry = 1;
        %entryInfo.delete();
        %i = (%i + 1.0);
    }
    %botsFriendsList.clear();
    %botsFriendsList.delete();
    if ((BuddyHudFriendsList.getCount() > 0.0)) {
        %newVerticalPositionOfList = (%offsetForVerticalPositionOfList - (%heightOfRowInFriendsList * %numberOfNamesInsertedAbove));
        (%i < %botFriendsCount);
        BuddyHudFriendsList.startingPos = getWord(BuddyHudFriendsList.startingPos, 0) @ " " @ %newVerticalPositionOfList;
    }
    if (%timeIt) {
        error(getScopeName() @ " " @ "- D" @ " " @ formatFloat("%8.3f", ((getSimTime() - %startTime) / 1000.0)) @ " " @ formatFloat("%8.3f", ((getSimTime() - %lastTime) / 1000.0)));
        %lastTime = getSimTime();
    }
    %favoriteCount = UserListFavorites.size();
    %i = 0;
    while ((%i < %favoriteCount)) {
        %favorite = %i.getValue(UserListFavorites);
        %color = $NameColorNormal;
        %listName = "FavesOffline";
        %ghost = Player::findPlayerInstance(%favorite.name);
        if (!(%ghost $= "")) {
            1.setBuddy(%ghost);
        }
        if (!(%listName $= "")) {
            if (%favorite.name.getIgnoreStatus(BuddyHudWin)) {
                %ignoreTag = "<strikethrough>";
            }
            %ignoreTag = "";
            if (%favorite.isNPC) {
                %npcOpen = "[ ";
                %npcClose = " ]";
            }
            %npcOpen = "";
            %npcClose = "";
            %boldTag = "";
            %colorTag = "<linkcolor:" @ ColorIToHex(%color) @ ">";
            %indent = "   ";
            %entry = "<spush>" @ %indent @ %ignoreTag @ %boldTag @ %colorTag @ "<a:gamelink player " @ munge(%favorite.name) @ ">" @ %npcOpen @ %favorite.name @ %npcClose @ "</a><spop>";
            %list = %this.buddyLists;
            %listName;
            if (!(isObject(%list))) {
                log(relations, error, "unknown list" @ " " @ %listName);
            }
            %entry.put(%list, %favorite.name);
        }
        log(relations, error, "unsortable fave buddy" @ " " @ %favorite.name);
        %i = (%i + 1.0);
    }
    if (%timeIt) {
        error(getScopeName() @ " " @ "- E" @ " " @ formatFloat("%8.3f", ((getSimTime() - %startTime) / 1000.0)) @ " " @ formatFloat("%8.3f", ((getSimTime() - %lastTime) / 1000.0)));
        %lastTime = getSimTime();
        (%i < %favoriteCount);
    }
    %fanCount = UserListFans.size();
    %i = 0;
    while ((%i < %fanCount)) {
        %fan = %i.getValue(UserListFans);
        %listName = "";
        %color = "";
        if (%fan.isNPC) {
            log("relations", "warn", "an NPC is a fan; weird." @ " " @ %fan.name);
            %color = $NameColorNormal;
            %listName = "FansNPC";
        }
        if ((%fan.serverName $= "")) {
            %color = $NameColorOffline;
            %listName = "FansOffline";
        }
        if ((%fan.serverName $= $ServerName)) {
            if ((%vipRoleMasks != 0.0)) {
            }
            if (((%fan.roles & %vipRoleMasks) != 0.0)) {
                %color = $NameColorStaff;
                %listName = "FansHere";
            }
            %color = $NameColorNormal;
            %listName = "FansHere";
        }
        %color = $NameColorElsewhere;
        %listName = "FansThere";
        if (!(%listName $= "")) {
            if (%fan.name.getIgnoreStatus(BuddyHudWin)) {
                %ignoreTag = "<strikethrough>";
            }
            %ignoreTag = "";
            if (%fan.isNPC) {
                %npcOpen = "[ ";
                %npcClose = " ]";
            }
            %npcOpen = "";
            %npcClose = "";
            %boldTag = "";
            %colorTag = "<linkcolor:" @ ColorIToHex(%color) @ ">";
            %indent = "   ";
            %entry = "<spush>" @ %indent @ %ignoreTag @ %boldTag @ %colorTag @ "<a:gamelink player " @ munge(%fan.name) @ ">" @ %npcOpen @ %fan.name @ %npcClose @ "</a><spop>";
            %list = %this.buddyLists;
            %listName;
            if (!(isObject(%list))) {
                log(relations, error, "unknown list" @ " " @ %listName);
            }
            %entry.put(%list, %fan.name);
        }
        log(relations, error, "unsortable fan buddy" @ " " @ %fan.name);
        %i = (%i + 1.0);
    }
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    (%i < %fanCount);
    %playerCount = %dict.size();
    %i = 0;
    while ((%i < %playerCount)) {
        %ghost = %i.getValue(%dict);
        if (isObject(%ghost)) {
            %ghost.getShapeName().getIgnoreStatus(BuddyHudWin).setIgnore(%ghost);
        }
        %i = (%i + 1.0);
    }
    rentabotClient_reignore();
    if (%timeIt) {
        error(getScopeName() @ " " @ "- F" @ " " @ formatFloat("%8.3f", ((getSimTime() - %startTime) / 1000.0)) @ " " @ formatFloat("%8.3f", ((getSimTime() - %lastTime) / 1000.0)));
        %lastTime = getSimTime();
        (%i < %playerCount);
    }
    %bitmap = (UserListFans.size() > 0.0) ? "platform/client/buttons/pending_active" : "platform/client/buttons/pending";
    %elButton = "requests".getTabWithName(BuddyHudTabs).button;
    %bitmap.setBitmap(%elButton);
    if ((%friendCount > 0.0)) {
        "You have" @ " " @ (%friendCount - %friendCountOnline) @ " " @ "friends offline".setText(BuddyHudFriendsListOfflineFriendsBox);
    }
    "".setText(BuddyHudFriendsListOfflineFriendsBox);
    "WaitingForYourApproval".setCurListName(%this);
    BuddyHudRequestsList.putListIntoList(%this, "FansHere");
    BuddyHudRequestsList.putListIntoList(%this, "FansThere");
    BuddyHudRequestsList.putListIntoList(%this, "FansOffline");
    "YourPendingRequests".setCurListName(%this);
    BuddyHudRequestsList.putListIntoList(%this, "FavesHere");
    BuddyHudRequestsList.putListIntoList(%this, "FavesThere");
    BuddyHudRequestsList.putListIntoList(%this, "FavesOffline");
    if (%timeIt) {
        error(getScopeName() @ " " @ "- G" @ " " @ formatFloat("%8.3f", ((getSimTime() - %startTime) / 1000.0)) @ " " @ formatFloat("%8.3f", ((getSimTime() - %lastTime) / 1000.0)));
        %lastTime = getSimTime();
    }
    BuddyHudFriendsList.startingPos.scrollToPos(BuddyHudFriendsList);
    BuddyHudRequestsList.startingPos.scrollToPos(BuddyHudRequestsList);
    (%friendCount == 0.0).setVisible(BuddyHudFriendsInfo);
    ((%favoriteCount + %fanCount) == 0.0).setVisible(BuddyHudRequestsInfo);
    BuddyHudTabs.setUhOhTabVisibility();
    BuddyHudFriendsList.reseatChildren();
    if (%timeIt) {
        error(getScopeName() @ " " @ "- H" @ " " @ formatFloat("%8.3f", ((getSimTime() - %startTime) / 1000.0)) @ " " @ formatFloat("%8.3f", ((getSimTime() - %lastTime) / 1000.0)));
        %lastTime = getSimTime();
    }
};
function BuddyHudWin::getNamesPendingMyApproval(%this) {
    return "FansHere FansNPC FansThere FansOffline".getNamesInLists(%this);
};
function BuddyHudWin::getNamesPendingTheirApproval(%this) {
    return "FavesHere FavesNPC FavesThere FavesOffline".getNamesInLists(%this);
};
function BuddyHudWin::getNamesInLists(%this, %lists) {
    %ret = "";
    %delim = "";
    %n = (getWordCount(%lists) - 1.0);
    while ((%n >= 0.0)) {
        %list = %this.buddyLists;
        getWord(%lists, %n);
        if (isObject(%list)) {
            %m = (%list.size() - 1.0);
            while ((%m >= 0.0)) {
                %ret = %ret @ %delim @ %m.getKey(%list);
                %delim = "\t";
                %m = (%m - 1.0);
            }
        }
        %n = (%n - 1.0);
        (%m >= 0.0);
    }
    return %ret;
};
function BuddyHudWin::initializeBuddyList(%this, %listName) {
    if (!(isObject(%listName, %this.buddyLists))) {
        %this.buddyLists = new StringMap("") {
            ignoreCase = "true";
        }; @ %listName;
    }
    if (isObject(MissionCleanup)) {
        %this.buddyLists.add(MissionCleanup, %listName);
    }
    %list = %this.buddyLists;
    %listName;
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    %count = %list.size();
    %idx = 0;
    while ((%idx < %count)) {
        %ghost = %idx.getKey(%list).get(%dict);
        if (!(%ghost $= "")) {
        }
        if (isObject(%ghost)) {
            0.setBuddy(%ghost);
            0.setAmFave(%ghost);
            0.setIgnore(%ghost);
        }
        %idx = (%idx + 1.0);
    }
    %list.clear();
};
function BuddyHudWin::setCurListName(%this, %listName) {
    %this.curListName = %listName;
    %this.listAdded = 0 @ %listName;
};
function BuddyHudWin::putListIntoList(%this, %srcList, %destList) {
    %list = %this.buddyLists;
    %srcList;
    if (!(isObject(%list))) {
        log(relations, error, "unknown list" @ " " @ %srcList);
        return;
    }
    if ((%list.size() > 0.0)) {
    }
    if (!(%this.listAdded)) {
        %this.listAdded = %this.curListName @ 1 @ %this.curListName;
        %colorTag = "<linkcolor:ffffff>";
        if (%this[$UserPref::buddies::collapsedLists @ %this.curListName]) {
            %collapsed = "+";
        }
        %collapsed = "- ";
        %listTitle = %this[$gBuddyListTitles @ %this.curListName];
        %titleLine = "<color:ffffff>" @ %colorTag @ "<a:gamelink list " @ %this.curListName @ ">" @ %collapsed @ %listTitle @ "</a>";
        %destList.getText() @ %titleLine @ "<br>".setText(%destList);
        if ((%this.curListName $= "WaitingForYourApproval")) {
        }
        if (!(%this[$UserPref::buddies::collapsedLists @ %this.curListName])) {
            %formatStr = "<spush><linkcolor:" @ ColorIToHex("255 147 248") @ ">";
            %destList.getText() @ %formatStr @ "  [<a:gamelink approveall>Approve All</a>]   [<a:gamelink declineall>Decline All</a>]<spop><br>".setText(%destList);
        }
    }
    if (!(%this[$UserPref::buddies::collapsedLists @ %this.curListName])) {
        %this.putIntoList = %destList;
        "addToFavList".forEach(%list);
    }
};
function BuddyHudWin::putListIntoTab(%this, %listName, %destMLTextCtrl) {
    %srcStringMap = %this.buddyLists;
    %listName;
    if (!(isObject(%srcStringMap))) {
        log(relations, error, "unknown list" @ " " @ %listName);
        return;
    }
    if (!(isObject(%destMLTextCtrl))) {
        log(relations, error, "unknown object" @ " " @ %destMLTextCtrl);
    }
    "".setText(%destMLTextCtrl);
    %outputText = "";
    %size = %srcStringMap.size();
    if ((%size == 0.0)) {
        %outputText = "";
    }
    %outputText = 0.getValue(%srcStringMap);
    %i = 1;
    while ((%i < %size)) {
        %outputText = %outputText @ "\n" @ %i.getValue(%srcStringMap);
        %i = (%i + 1.0);
    }
    %outputText.setText(%destMLTextCtrl);
};
function StringMap::addToFavList(%this, %key, %value) {
    %destList = BuddyHudWin.putIntoList;
    %destList.getText() @ %value @ "<br>".setText(%destList);
};
function BuddyHudWin::isFriendOrFavOnlineElsewhere(%this, %playerName) {
    if ("FrndsThere".isInBuddyList(%this, %playerName)) {
        return 1;
    }
    if ("FavesThere".isInBuddyList(%this, %playerName)) {
        return 1;
    }
    return 0;
};
function BuddyHudWin::isFriendOrFavOnlineHere(%this, %playerName) {
    if ("FrndsHere".isInBuddyList(%this, %playerName)) {
        return 1;
    }
    if ("FavesHere".isInBuddyList(%this, %playerName)) {
        return 1;
    }
    return 0;
};
function BuddyHudWin::isFriendOrFavOffline(%this, %playerName) {
    if ("FrndsOffline".isInBuddyList(%this, %playerName)) {
        return 1;
    }
    if ("FavesOffline".isInBuddyList(%this, %playerName)) {
        return 1;
    }
    return 0;
};
function BuddyHudWin::isOnlineHereOrNotFavorite(%this, %playerName) {
    if (%playerName.isFriendOrFavOnlineHere(%this, %this)) {
        return 1;
    }
    if ("FrndsNPC".isInBuddyList(%this, %playerName)) {
        return 1;
    }
    if ("FavesNPC".isInBuddyList(%this, %playerName)) {
        return 1;
    }
    if (%playerName.isFriendOrFavOnlineElsewhere(%this, %this)) {
        return 0;
    }
    if (%playerName.isFriendOrFavOffline(%this, %this)) {
        return 0;
    }
    return 1;
};
function BuddyHudWin::isInBuddyList(%this, %playerName, %listName) {
    %list = %this.buddyLists;
    %listName;
    if (!(isObject(%list))) {
        if (!($StandAlone)) {
            log(relations, error, "isInBuddyList(): unknown list" @ " " @ %listName);
        }
        return;
    }
    return !(%playerName.get(%list) $= "");
};
function BuddyHudWin::getFriendStatus(%this, %playerName) {
    safeEnsureScriptObjectWithInit("StringMap", "UserListFriends", "{ ignoreCase = true; }");
    safeEnsureScriptObjectWithInit("StringMap", "UserListFavorites", "{ ignoreCase = true; }");
    safeEnsureScriptObjectWithInit("StringMap", "UserListFans", "{ ignoreCase = true; }");
    if (%playerName.hasKey(UserListFriends)) {
        return "friends";
    }
    if (%playerName.hasKey(UserListFavorites)) {
        return "favorite";
    }
    if (%playerName.hasKey(UserListFans)) {
        return "fan";
    }
    return "none";
};
function BuddyHudWin::getIgnoreStatus(%this, %playerName) {
    safeEnsureScriptObjectWithInit("StringMap", "UserListIgnores", "{ ignoreCase = true; }");
    if (%playerName.hasKey(UserListIgnores)) {
        return 1;
    }
    return 0;
};
function sendBuddyListRequest(%callback) {
    log("relations", "debug", getTrace());
    if (($Token $= "")) {
        log("general", "debug", getScopeName() @ " " @ "- no token. skipping request.");
        return;
    }
    %request = sendRequest_GetUserRelations($Player::Name, "", %callback);
};
function onDoneOrErrorCallback_GetUserRelations_ProcessOnly(%request) {
    if (!(isObject(%request))) {
        return;
    }
    if (!(%request.checkSuccess())) {
        return;
    }
    safeEnsureScriptObjectWithInit("StringMap", "UserListFriends", "{ ignoreCase = true; }");
    safeEnsureScriptObjectWithInit("StringMap", "UserListFavorites", "{ ignoreCase = true; }");
    safeEnsureScriptObjectWithInit("StringMap", "UserListFans", "{ ignoreCase = true; }");
    safeEnsureScriptObjectWithInit("StringMap", "UserListIgnores", "{ ignoreCase = true; }");
    safeEnsureScriptObjectWithInit("StringMap", "UserRelationships", "{ ignoreCase = true; }");
    safeEnsureScriptObject("StringMap", "UserListUnknownServerName");
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    %num = UserListIgnores.size();
    %n = 0;
    while ((%n < %num)) {
        %ghost = %n.getKey(UserListIgnores).get(%dict);
        if (isObject(%ghost)) {
            0.setIgnore(%ghost);
        }
        %n = (%n + 1.0);
    }
    extractBuddyRecords(%request, UserListFriends, "friend", 0);
    extractBuddyRecords(%request, UserListFavorites, "favorite", 0);
    extractBuddyRecords(%request, UserListFans, "fan", 0);
    extractBuddyRecords(%request, UserListIgnores, "ignore", 1);
    markIgnoredInList(UserListFriends);
    markIgnoredInList(UserListFavorites);
    rentabotClient_reignore();
};
function BuddyHudWin::refreshFavoritesList(%this) {
    sendBuddyListRequest("onDoneOrErrorCallback_GetUserRelations_Full");
};
function onDoneOrErrorCallback_GetUserRelations_Full(%request) {
    log("network", "debug", getScopeName() @ " " @ "- url =" @ " " @ %request.getURL());
    if (!(%request.checkSuccess())) {
        return;
    }
    onDoneOrErrorCallback_GetUserRelations_ProcessOnly(%request);
    BuddyHudWin.populateBuddyLists();
    if (InfoPopupDlg.isShowing()) {
        InfoPopupDlg.tryShowPlayerInfo();
    }
    if (0 && BuddyHudWin.firstTime) {
        BuddyHudWin.firstTime = 0;
        if (("fanCount".getValue(%request) > 0.0)) {
            BuddyHudWin.open();
            "requests".selectTabWithName(BuddyHudTabs);
        }
    }
    if ((UserListUnknownServerName.size() > 0.0)) {
        WorldMap.refresh();
    }
};
function extractBuddyRecords(%request, %list, %type, %doIgnore) {
    %list.deleteValuesAsObjects();
    %num = %type @ "Count".getValue(%request);
    log("network", "debug", getScopeName() @ " " @ "- " @ %type @ "Count =" @ " " @ %num);
    %n = 0;
    while ((%n < %num)) {
        %key = %type @ %n;
        %val = %key.getValue(%request);
        log("network", "debug", getScopeName() @ " " @ " -" @ " " @ %key @ " " @ "=" @ " " @ %val);
        %record = new ScriptObject("");
        parseBuddyRecord(%record, %val);
        %record.put(%list, %record.name);
        if (!(%record.serverName $= "")) {
        }
        if ((%record.csn $= "")) {
            %record.put(UserListUnknownServerName, %record.name);
            warn(getScopeName() @ " " @ "- unrecogized server name '" @ %record.serverName @ "' for " @ %type @ " user '" @ %record.name @ "'");
        }
        if (%doIgnore) {
            %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
            %ghost = %n.getKey(%list).get(%dict);
            if (isObject(%ghost)) {
                1.setIgnore(%ghost);
            }
        }
        %n = (%n + 1.0);
    }
};
function parseBuddyRecord(%record, %val) {
    %fieldNdx = 0;
    %record.name = getField(%val, %fieldNdx);
    %fieldNdx = (%fieldNdx + 1.0);
    %record.serverName = getField(%val, %fieldNdx);
    %fieldNdx = (%fieldNdx + 1.0);
    %roles = getField(%val, %fieldNdx);
    %fieldNdx = (%fieldNdx + 1.0);
    if ((%roles $= "")) {
        %roles = 0;
    }
    eval("%record.roles = " @ %roles @ ";");
    %record.loggedIn = getField(%val, %fieldNdx);
    %fieldNdx = (%fieldNdx + 1.0);
    %record.isIdle = getField(%val, %fieldNdx);
    %fieldNdx = (%fieldNdx + 1.0);
    %record.isNPC = isNPCName(%record.name);
    if (%record.isNPC) {
        %record.isIdle = 0;
    }
    if ((%record.serverName $= "")) {
        %record.isIdle = 1;
    }
    if ((%record.isIdle $= "no")) {
        %record.isIdle = 0;
    }
    %record.isIdle = 1;
    %record.loggedIn = (%record.loggedIn $= "yes");
    if (%record.isNPC) {
    }
    if ((%record.serverName $= "")) {
        %record.serverName = $ServerName;
    }
    %record.csn = %record.serverName.getCityNameForServerName(BuddyHudTabs);
    %record.activities = %record.isIdle ? "idle" : "";
};
function BuddyHudTabs::getCityNameForServerName(%this, %ServerName) {
    %csn = %ServerName.cityNameForServerName(WorldMap);
    return %csn;
};
function BuddyHudTabs::updateUserListUnknownServerName(%this) {
    safeEnsureScriptObject("StringMap", "UserListUnknownServerName");
    %originalSize = UserListUnknownServerName.size();
    %i = (%originalSize - 1.0);
    while ((%i >= 0.0)) {
        %record = %i.getValue(UserListUnknownServerName);
        %record.csn = %record.serverName.getCityNameForServerName(BuddyHudTabs);
        if (!(%record.csn $= "")) {
            %i.getKey(UserListUnknownServerName).remove(UserListUnknownServerName);
        }
        %record.csn = "?";
        %i = (%i - 1.0);
    }
    if ((UserListUnknownServerName.size() != %originalSize)) {
        BuddyHudWin.populateBuddyLists();
    }
};
function markIgnoredInList(%list) {
    %n = (%list.size() - 1.0);
    while ((%n >= 0.0)) {
        %fave = %n.getValue(%list);
        %fave.ignored = %fave.name.getIgnoreStatus(BuddyHudWin);
        %n = (%n - 1.0);
    }
};
$gRefreshEvenIfBuddyHudWinClosed = 1;
function clientCmdRefreshBuddies(%status) {
    log("relations", "debug", "clientCmdRefreshBuddies(" @ %status @ ")");
    if (%status && $gRefreshEvenIfBuddyHudWinClosed) {
    }
    if (BuddyHudWin.isVisible()) {
        BuddyHudWin.refreshFavoritesList();
        $gRefreshEvenIfBuddyHudWinClosed = 0;
    }
};
function sendBuddyStatusRequest(%buddyName) {
    log("relations", "debug", getTrace());
    if (($Token $= "")) {
        log("general", "debug", getScopeName() @ " " @ "- no token. skipping request.");
        return;
    }
    %request = sendRequest_GetUserRelations($Player::Name, %buddyName, "onDoneOrErrorCallback_GetUserRelations_Single");
};
function onDoneOrErrorCallback_GetUserRelations_Single(%request) {
    log("network", "debug", getScopeName() @ " " @ "- url =" @ " " @ %request.getURL());
    if (!(%request.checkSuccess())) {
        return;
    }
    %val = "";
    %list = "";
    if (("friendCount".getValue(%request) > 0.0)) {
        %key = "friend0";
        %val = %key.getValue(%request);
        %list = UserListFriends;
        log("network", "debug", getScopeName() @ " " @ " -" @ " " @ %key @ " " @ "=" @ " " @ %val);
    }
    if (("favoriteCount".getValue(%request) > 0.0)) {
        %key = "favorite0";
        %val = "favorite0".getValue(%request);
        %list = UserListFavorites;
        log("network", "debug", getScopeName() @ " " @ " -" @ " " @ %key @ " " @ "=" @ " " @ %val);
    }
    if (("fanCount".getValue(%request) > 0.0)) {
        %key = "fan0";
        %val = "fan0".getValue(%request);
        %list = UserListFans;
        log("network", "debug", getScopeName() @ " " @ " -" @ " " @ %key @ " " @ "=" @ " " @ %val);
    }
    log("network", "debug", getScopeName() @ " " @ "- lost relation" @ " " @ %val);
    if (!(%val $= "")) {
        %name = getField(%val, 0);
    }
    %name = %request.singleUserName;
    %record = findRelatedPlayerRecord(%name);
    if (isObject(%record)) {
        %oldList = findRelatedPlayerRecordList(%name);
        if (isObject(%oldList)) {
            %name.remove(%oldList);
        }
        %record.delete();
    }
    if (!(%val $= "")) {
    }
    if (isObject(%list)) {
        %record = new ScriptObject("");
        parseBuddyRecord(%record, %val);
        %record.put(%list, %record.name);
        if (!(%record.serverName $= "")) {
        }
        if ((%record.csn $= "")) {
            safeEnsureScriptObject("StringMap", "UserListUnknownServerName");
            %record.put(UserListUnknownServerName, %record.name);
            warn(getScopeName() @ " " @ "- unrecogized server name '" @ %record.serverName @ "' for relation with single user '" @ %record.name @ "'");
            WorldMap.refresh();
        }
    }
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    %ghost = %record.name.get(%dict);
    if (isObject(%ghost)) {
        0.setIgnore(%ghost);
    }
    %oldIgnoreRecord = findRelatedPlayerRecordInList(%name, UserListIgnores);
    if (isObject(%oldIgnoreRecord)) {
        %name.remove(UserListIgnores);
        %oldIgnoreRecord.delete();
    }
    if (("ignoreCount".getValue(%request) > 0.0)) {
        echo("have an ignore record...");
        %key = "ignore0";
        %val = "ignore0".getValue(%request);
        %record = new ScriptObject("");
        parseBuddyRecord(%record, %val);
        %record.put(UserListIgnores, %record.name);
        if (isObject(%ghost)) {
            echo("setting ignore to true for" @ " " @ %record.name);
            1.setIgnore(%ghost);
        }
    }
    markIgnoredInList(UserListFriends);
    markIgnoredInList(UserListFavorites);
    BuddyHudWin.populateBuddyLists();
};
function BuddyHudTabs::setBuddyIdleStatus(%this, %buddyName, %isIdle) {
    %entry = %buddyName.get(UserListFriends);
    if (!(isObject(%entry))) {
        return;
    }
    %entry.isIdle = %isIdle;
    if (%isIdle && !(hasField(%entry.activities, "idle"))) {
        %entry.activities = "idle" @ "\t" @ %entry.activities;
    }
    BuddyHudWin.populateBuddyLists();
};
function BuddyHudTabs::setBuddyActivities(%this, %buddyName, %activitiesList) {
    %entry = %buddyName.get(UserListFriends);
    if (!(isObject(%entry))) {
        return;
    }
    %entry.activities = %activitiesList;
    BuddyHudWin.populateBuddyLists();
};
function clientCmdUpdateBuddy(%source, %target, %action) {
    log("relations", "debug", "clientCmdUpdateBuddy(source=" @ %source @ ", target=" @ %target @ ", action=" @ %action @ ")");
    if ((%target $= $player.getShapeName())) {
    }
    %other = %target;
    %source;
    if ((%target $= $player.getShapeName())) {
    }
    %self = %source;
    %target;
    if ((%action $= "userJoined")) {
        sendBuddyStatusRequest(%other);
    }
    if ((%action $= "userDropped")) {
        sendBuddyStatusRequest(%other);
    }
    if ((%action $= "userToIdle")) {
        1.setBuddyIdleStatus(BuddyHudTabs, %source);
    }
    if ((%action $= "userToNonIdle")) {
        0.setBuddyIdleStatus(BuddyHudTabs, %source);
    }
    if ((%action $= "friendRequestCreated")) {
        sendBuddyStatusRequest(%other);
        if ((%other $= %source)) {
            handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff><a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a> has just asked to be your friend!  <a:ACCEPT " @ munge(%other) @ ">Accept</a> | <a:DECLINE " @ munge(%other) @ ">Decline</a>");
        }
        handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff>You have asked <a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a> to be your friend.");
    }
    if ((%action $= "friendsCreated")) {
        sendBuddyStatusRequest(%other);
        handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff><a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a> is now your friend.");
        1.updateFriendRequest(SystemMessageTextCtrl, %other);
    }
    if ((%action $= "friendsRemoved")) {
        sendBuddyStatusRequest(%other);
        handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff><a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a> is no longer your friend.");
    }
    if ((%action $= "friendRequestDenied")) {
        sendBuddyStatusRequest(%other);
        if ((%self $= %source)) {
            handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff>You declined <a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a>'s friend request.");
            0.updateFriendRequest(SystemMessageTextCtrl, %other);
        }
    }
    if ((%action $= "friendRequestCancelled")) {
        sendBuddyStatusRequest(%other);
        if ((%self $= %source)) {
            handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff>You canceled your friend request to <a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a>.");
        }
        2.updateFriendRequest(SystemMessageTextCtrl, %other);
    }
    if ((%action $= "ignoreAdded")) {
        sendBuddyStatusRequest(%other);
        handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff>You ignored <a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a>.");
    }
    if ((%action $= "ignoreRemoved")) {
        sendBuddyStatusRequest(%other);
        handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff>You stopped ignoring <a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a>.");
    }
    log("relations", "error", "Unknown action: " @ %action);
};
function BuddyHudFriendsList::scrollToPos(%this, %pos) {
    BuddyHudPlayerList::scrollToPos(%this, %pos);
};
function BuddyHudFriendsList::onURL(%this, %url) {
    BuddyHudPlayerList::onURL(%this, %url);
};
function BuddyHudFriendsList::onRightURL(%this, %url) {
    BuddyHudPlayerList::onRightURL(%this, %url);
};
function BuddyHudPlayerList::scrollToPos(%this, %pos) {
    (1.0 - getWord(%pos, 1)).scrollTo(%this.getParent(), 0);
};
function BuddyHudPlayerList::onURL(%this, %url) {
    if (!(firstWord(%url) $= "gamelink")) {
        return;
    }
    if ((getWord(%url, 1) $= "player")) {
        %name = unmunge(getWords(%url, 2));
        onLeftClickPlayerName(%name, "");
    }
    if ((getWord(%url, 1) $= "list")) {
        %listName = getWords(%url, 2);
        %listName[$UserPref::buddies::collapsedLists @ %listName] = !(%listName[$UserPref::buddies::collapsedLists @ %listName]);
        BuddyHudWin.populateBuddyListsReally();
    }
    if ((getWord(%url, 1) $= "approveall")) {
        acceptAllOperation();
    }
    if ((getWord(%url, 1) $= "declineall")) {
        declineAllOperation();
    }
};
function BuddyHudPlayerList::onRightURL(%this, %url) {
    if (!(firstWord(%url) $= "gamelink")) {
        return;
    }
    if ((getWord(%url, 1) $= "player")) {
        %name = unmunge(getWords(%url, 2));
        onRightClickPlayerName(%name);
    }
};
$gLastNameClickTime = 0;
$gLastNameClickName = "";
$gLeftClickTimer = 0;
function onLeftClickPlayerName(%name, %objID) {
    %curTime = getSimTime();
    if (((%curTime - $gLastNameClickTime) < 400.0)) {
    }
    if (($gLastNameClickName $= %name)) {
        openUserWhisper(%name);
        if (($gLeftClickTimer != 0.0)) {
            cancel($gLeftClickTimer);
            $gLeftClickTimer = 0;
        }
    }
    $gLeftClickTimer = schedule(450, 0, "onSingleClickPlayerName", %name, %objID);
    $gLastNameClickTime = %curTime;
    $gLastNameClickName = %name;
};
function onRightClickPlayerName(%name) {
    %name.initWithPlayerName(PlayerContextMenu);
    Canvas.getCursorPos().showAtPoint(PlayerContextMenu);
};
function onSingleClickPlayerName(%name, %objID) {
    if (showPlayerInfoPopup()) {
    }
    if (!(%name $= $player.getShapeName())) {
        InfoPopupDlg.open();
        %name.showInfoFor(InfoPopupDlg);
        if (0) {
        }
        if (isFunction(afxSelectAvatarByName)) {
            afxSelectAvatarByName(%name);
        }
    }
    if (isDefined("%objID")) {
    }
    if (isObject(%objID)) {
    }
    if (%objID.isClassAIPlayer()) {
        rentabotClient_customizeBot(%objID);
    }
};
function AIMBuddyList::messageSelected(%this) {
    %id = %this.getSelectedId();
    %buddyName = stripUnprintables(%id.getRowTextById(%this));
    %buddyName.talkTo(AIMConvManager);
};
function AIMBuddyList::inviteSelected(%this) {
    %id = %this.getSelectedId();
    %buddyName = stripUnprintables(%id.getRowTextById(%this));
    %buddyName.open(AimInviteDialog);
};
function AIMBuddyList::onRightMouseDown(%this, %unused, %unused, %mousePt) {
    if ((%mousePt $= "")) {
        return;
    }
    %aimName = stripUnprintables(%this.getMouseOverRow().getRowText(%this));
    %aimName.initForAIM(PlayerContextMenu);
    %mousePt.showAtPoint(PlayerContextMenu);
};
function clientCmdBeginNPCList() {
    safeEnsureScriptObjectWithInit("StringMap", "NPCList", "{ ignoreCase = true; }");
    NPCList.clear();
};
function clientCmdItsAnNPC(%npcName) {
    safeEnsureScriptObjectWithInit("StringMap", "NPCList", "{ ignoreCase = true; }");
    %npcName.put(NPCList, %npcName);
};
function isNPCName(%name) {
    if (!(isObject(NPCList))) {
        if (isObject(ServerConnection)) {
            error(getScopeName() @ " " @ "- NPC list not initialized." @ " " @ %name @ " " @ getTrace());
        }
        return 0;
    }
    %ret = !(%name.get(NPCList) $= "");
    if (!(%ret)) {
        %ret = rentabot_isRentabotName(%name);
    }
    return %ret;
};
function findRelatedPlayerRecordInList(%playerName, %list) {
    if (!(isObject(%list))) {
        error(getScopeName() @ " " @ "- no list!" @ " " @ %playerName @ " " @ getDebugString(%list) @ " " @ getTrace());
        return 0;
    }
    %rec = %playerName.get(%list);
    if (isObject(%rec)) {
        return %rec;
    }
    return 0;
};
function isRelatedPlayerRecordInList(%playerName, %list) {
    if (!(isObject(%list))) {
        return 0;
    }
    %rec = findRelatedPlayerRecordInList(%playerName, %list);
    return isObject(%rec);
};
function findRelatedPlayerRecordList(%playerName) {
    %list = UserListFriends;
    if (isRelatedPlayerRecordInList(%playerName, %list)) {
        return %list;
    }
    %list = UserListFavorites;
    if (isRelatedPlayerRecordInList(%playerName, %list)) {
        return %list;
    }
    %list = UserListFans;
    if (isRelatedPlayerRecordInList(%playerName, %list)) {
        return %list;
    }
    %list = UserListIgnores;
    if (isRelatedPlayerRecordInList(%playerName, %list)) {
        return %list;
    }
    return 0;
};
function findRelatedPlayerRecord(%playerName) {
    %list = findRelatedPlayerRecordList(%playerName);
    if (!(isObject(%list))) {
        return 0;
    }
    return findRelatedPlayerRecordInList(%playerName, %list);
};
function AIMInviteButton::do(%this) {
    AIMBuddyList.inviteSelected();
    %selected = AIMBuddyList.getSelectedId();
    %row = %selected.getRowNumById(AIMBuddyList);
    %row = (%row + 1.0);
    if ((%row >= AIMBuddyList.rowCount())) {
        %row = 0;
    }
    %row.setSelectedRow(AIMBuddyList);
};
function Player::onGotBuddyStatus(%this) {
    "updateMapIcon".schedule(%this, 0);
};
function Player::onGotFaveStatus(%this) {
    "updateMapIcon".schedule(%this, 0);
};
