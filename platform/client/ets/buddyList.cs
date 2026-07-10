if (!(isObject(BuddyHudTabs))) {
    new ScriptObject(BuddyHudTabs) {
        class = "TabControl";
    };
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(BuddyHudTabs);
    }
}
vars = 0 @ "columnPadding" @ BuddyHudTabs;
vars = 0 @ "rowPadding" @ BuddyHudTabs;
vars = 22 @ "offlineFriendsTextboxHeight" @ BuddyHudTabs;
vars = 28 @ "buddyListLegendTextboxHeight" @ BuddyHudTabs;
vars = ColorIToHex($NameColorFriend) @ "normalTextColor" @ BuddyHudTabs;
vars = ColorFToHex(ColorConvolve($NameColorFriendF, $NameColorIdleModulationF)) @ "idleTextColor" @ BuddyHudTabs;
vars = ColorIToHex($NameColorHilite) @ "hilitedTextColor" @ BuddyHudTabs;
function BuddyHudTabs::setup(%this) {
    if (!(%this.initialized)) {
        %this.Initialize("25 25", "platform/client/ui/separator", "16 7", "horizontal");
        %this.newTab("friends", "platform/client/buttons/buddies");
        %this.newTab("requests", "platform/client/buttons/pending");
        %this.newTab("AIM", "platform/client/buttons/aim_buddies");
        %this.newTab("UhOh", "platform/client/buttons/uhoh");
        %this.selectTabWithName("friends");
        %this.fillFriendsTab();
        %this.fillRequestsTab();
        %this.fillAIMTab();
        %this.fillUhOhTab();
        gSetField(0);
        BuddyHudWin.refreshAIMBuddyList();
        0;
        favoritesTimer;
        BuddyHudTabContainer;
        new ""() {
            position = GuiBitmapButtonCtrl @ "115 4";
            extent = BuddyHudTabContainer @ "42 21";
            bitmap = BuddyHudWin @ "platform/client/buttons/inviteFriends";
            command = "doInviteFriends();";
        };.add();
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
    %theTab = %this.getTabWithName("friends");
    0;
    getWord(%theTab.getExtent(), 0) @ " ";
    %scroll = new ""() {
        profile = GuiScrollCtrl @ "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "0 0";
        extent = "buddyListLegendTextboxHeight" @ BuddyHudTabs @ vars @ (("offlineFriendsTextboxHeight" @ BuddyHudTabs + vars) - getWord(%theTab.getExtent(), 1));
        minExtent = "10 10";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    };
    0;
    %friendsList = new ""() {
        profile = GuiArray2Ctrl @ "CSProfileListBox";
        childrenClassName = "GuiMouseEventCtrl";
        childrenExtent = "16 18";
        spacing = "rowPadding" @ BuddyHudTabs @ vars;
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
        position = "columnPadding" @ BuddyHudTabs @ (vars - 0.0) @ " " @ 0;
        extent = %scroll.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        scroll = %scroll;
    };
    %friendsList.bindClassName("MenuControl");
    %friendsList.bindClassName("TabbedTextControl");
    %friendsList.bindClassName("BuddyHudFriendsList");
    %friendsList.setName("BuddyHudFriendsList");
    %fieldWidths = "10 94 46";
    %friendsList.teleFieldIndex = 0;
    %friendsList.nameFieldIndex = 1;
    %friendsList.cityFieldIndex = 2;
    %friendsList.setFieldWidths(%fieldWidths, %friendsList.vars);
    %friendsList.clear();
    %scroll.add(%friendsList);
    %theTab.add(%scroll);
    4 @ " ";
    "offlineFriendsTextboxHeight" @ BuddyHudTabs;
    %theTab.add(new GuiMLTextCtrl(BuddyHudFriendsListOfflineFriendsBox) {
        profile = "columnPadding" @ BuddyHudTabs @ "ETSSmallTextListProfile";
        horizSizing = "width";
        vertSizing = "top";
        position = "buddyListLegendTextboxHeight" @ BuddyHudTabs @ vars @ (("offlineFriendsTextboxHeight" @ BuddyHudTabs + vars) - getWord(%theTab.getExtent(), 1));
        extent = getWord(%theTab.getExtent(), 0) @ " " @ "offlineFriendsTextboxHeight" @ BuddyHudTabs @ vars;
        minExtent = vars @ " " @ "offlineFriendsTextboxHeight" @ BuddyHudTabs @ vars;
        sluggishness = -1;
        visible = 1;
        text = "";
    };);
    "buddyListLegendTextboxHeight" @ BuddyHudTabs;
    "buddyListLegendTextboxHeight" @ BuddyHudTabs;
    new GuiMLTextCtrl(BuddyHudFriendsListLegend) {
        profile = "ETSTinyTextListProfile";
        horizSizing = "width";
        vertSizing = "top";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) @ " " @ "buddyListLegendTextboxHeight" @ BuddyHudTabs @ vars;
        minExtent = vars @ " " @ "buddyListLegendTextboxHeight" @ BuddyHudTabs @ vars;
        sluggishness = -1;
        visible = 1;
        text = "<tab:20,90>" @ "<spush><color:" @ "idleTextColor" @ BuddyHudTabs @ vars @ ">Dim<spop>" @ "\t" @ "= Idle" @ "\t" @ "... = In Transit" @ "\n" @ " " @ "\t" @ "= Fast Teleport";
    };
    %theTab.add(new GuiMLTextCtrl(BuddyHudFriendsListLegendContainer) {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "top";
        position = 4 @ " " @ "buddyListLegendTextboxHeight" @ BuddyHudTabs @ (vars - getWord(%theTab.getExtent(), 1));
        extent = getWord(%theTab.getExtent(), 0) @ " " @ "buddyListLegendTextboxHeight" @ BuddyHudTabs @ vars;
        minExtent = vars @ " " @ "buddyListLegendTextboxHeight" @ BuddyHudTabs @ vars;
    };);
    %theTab.add(new GuiScrollCtrl(BuddyHudFriendsInfo) {
        profile = new ""() {
        profile = GuiBitmapCtrl @ "ETSNonModalProfile";
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
        extent = (3.0 - getWord(BuddyHudFriendsListLegend.getExtent(%theTab), 0)) @ " " @ (24.0 - getWord(BuddyHudFriendsListLegend.getExtent(%theTab), 1));
        minExtent = "10 10";
        sluggishness = -1;
        visible = 0;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    };);
    if (showInviteFriend()) {
        %inviteButton = new GuiVariableWidthButtonCtrl(ETSInviteButton) {
            profile = new ""() {
            profile = GuiMLTextCtrl @ "ETSTextListProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 0";
            extent = "146 68";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            text = ;
        }; @ "BracketButton15Profile";
            horizSizing = "right";
            vertSizing = "top";
            position = 8 @ " " @ (18.0 - getWord(BuddyHudFriendsListLegend.getExtent(%theTab), 1));
            extent = "140 15";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            command = "BuddyHudTabs.OnETSInviteFriends();";
            text = "Invite Friends To Join";
            groupNum = -1;
            buttonType = "PushButton";
        };
        %theTab.add(%inviteButton);
    }
};
function BuddyHudTabs::fillRequestsTab(%this) {
    %theTab = %this.getTabWithName("requests");
    0;
    %requestsList = new ""() {
        profile = GuiMLTextCtrl @ "ETSTextListProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = "500 80";
        minExtent = "80 80";
        sluggishness = -1;
        visible = 1;
    };
    %requestsList.bindClassName("BuddyHudPlayerList");
    %requestsList.setName("BuddyHudRequestsList");
    %requestsList.bindClassName("BuddyHudRequestsList");
    0;
    %scroll = new ""() {
        profile = GuiScrollCtrl @ "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) @ " " @ (24.0 - getWord(%theTab.getExtent(), 1));
        minExtent = "10 10";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    };
    %scroll.add(%requestsList);
    %theTab.add(%scroll);
    %theTab.add(new GuiScrollCtrl(BuddyHudRequestsInfo) {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "3 2";
        extent = (3.0 - getWord(%theTab.getExtent(), 0)) @ " " @ (24.0 - getWord(%theTab.getExtent(), 1));
        minExtent = "10 10";
        sluggishness = -1;
        visible = 0;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    };);
};
function BuddyHudTabs::fillAIMTab(%this) {
    %theTab = %this.getTabWithName("AIM");
    if (!(isObject(%theTab))) {
        echo("Didn't find AIM Buddies tab");
        return 0;
    }
    $Player::AIMName = "";
    $Player::AIMPassword = "";
    new ""() {
        profile = GuiMLTextCtrl @ "ETSShadowTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = "153 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "<a:http://www.aim.com/>Get an AIM Screen Name</a>";
    };
    new GuiTextEditCtrl(AIMScreenNameField) {
        profile = new ""() {
        profile = GuiTextCtrl @ "ETSShadowTextProfile";
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
    };
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
        profile = new ""() {
        profile = GuiTextCtrl @ "ETSShadowTextProfile";
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
    0;
    %aimListScroll = new ""() {
        profile = GuiScrollCtrl @ "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) @ " " @ (24.0 - getWord(%theTab.getExtent(), 1));
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
        position = 93 @ " " @ (22.0 - getWord(%theTab.getExtent(), 1));
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
        position = 8 @ " " @ (22.0 - getWord(%theTab.getExtent(), 1));
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
    %theTab.add(%loginFrame);
    %theTab.aimListScroll = %aimListScroll;
    %theTab.add(%aimListScroll);
    %theTab.signOffButton = %signOffButton;
    %theTab.add(%signOffButton);
    %theTab.inviteButton = %inviteButton;
    %theTab.add(%inviteButton);
};
function BuddyHudTabs::wakeUp(%this) {
    %this.setup();
    %this.selectCurrentTab();
    1.setActive();
};
function AIMLoginFrame::nextControl(%this, %curControl) {
    %nextControl = "";
    %this.update();
    if (AIMScreenNameField) {
        // unhandled opcode 2014 at 0x000013F2
        %curControl.getName();
    }
    if (AIMPasswordField) {
        // unhandled opcode 2014 at 0x00001409
        %curControl.getName();
    }
    if ((%nextControl $= "")) {
        error("nextControl got invalid arg" @ " " @ %curControl);
        return;
    }
    if (AIMSignInButton) {
        doAIMSignIn();
    }
    %nextControl.makeFirstResponder(1);
    %nextControl.selectAll();
};
function BuddyHudTabs::fillUhOhTab(%this) {
    %theTab = %this.getTabWithName("UhOh");
    if (!(isObject(%theTab))) {
        echo("Didn't find UhOh tab");
        return 0;
    }
    0;
    %scroll = new ""() {
        profile = GuiScrollCtrl @ "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) @ " " @ (24.0 - getWord(%theTab.getExtent(), 1));
        minExtent = "10 10";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    };
    0;
    %body = new ""() {
        profile = GuiMLTextCtrl @ "ETSTextListProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = (10.0 - getWord(%scroll.getExtent(), 0)) @ " " @ (10.0 - getWord(%scroll.getExtent(), 1));
        minExtent = "10 10";
        sluggishness = -1;
        visible = 1;
        text = ;
    };
    %theTab.add(%scroll);
    %scroll.add(%body);
};
function BuddyHudTabs::setUhOhTabVisibility(%this) {
    %showIt = (250.0 > UserListFriends.size());
    if (%showIt) {
        %this.showTabWithName("UhOh");
    }
    %this.hideTabWithName("UhOh");
};
function SimGroup::hasObjectWithName(%this, %name) {
    %i = 0;
    if ((%this.getCount() < %i)) {
        if ((%this.getObject(%i).name $= %name)) {
            return 1;
        }
        %i = (1.0 + %i);
    }
    return 0;
};
function BuddyHudWin::wakeUp(%this) {
    BuddyHudTabs.wakeUp();
};
function BuddyHudWin::addBuddy(%this, %buddy) {
    0.addRow(%buddy);
};
function BuddyHudWin::onlineBuddiesToString(%this) {
    %count = aimBuddyCount();
    %onlineBuddies = "";
    %n = 0;
    if ((%count < %n)) {
        %state = aimGetBuddyState(%n);
        if ((1.0 < %state)) {
        }
        if ((3.0 > %state)) {
        }
        if (!(%onlineBuddies $= "")) {
            %onlineBuddies = %onlineBuddies @ "\t" @ aimGetBuddyName(%n);
        }
        %onlineBuddies = aimGetBuddyName(%n);
        %n = (1.0 + %n);
    }
    return %onlineBuddies;
};
function BuddyMap::addToAIMList(%this, %key, %value) {
    AIMBuddyList.rowCount().addRow(%value);
};
function BuddyHudWin::refreshAIMBuddyList(%this) {
    if (isObject(AIMBuddyList)) {
        0;
        %onlineList = new ""() {
            class = StringMap @ "BuddyMap";
            ignoreCase = 1;
        };
        0;
        %idleAwayList = new ""() {
            class = StringMap @ "BuddyMap";
            ignoreCase = 1;
        };
        0;
        %offlineList = new ""() {
            class = StringMap @ "BuddyMap";
            ignoreCase = 1;
        };
        if (isObject(MissionCleanup)) {
            %onlineList.add();
            %idleAwayList.add();
            %offlineList.add();
        }
        AIMBuddyList.clear();
        %buddyCount = aimBuddyCount();
        MissionCleanup;
        %i = 0;
        MissionCleanup;
        if ((%buddyCount < %i)) {
            %buddyName = aimGetBuddyName(%i);
            MissionCleanup;
            %buddyState = aimGetBuddyState(%i);
            if ((-(1.0) == %buddyState)) {
                %tag = "\x10\x06";
                %offlineList.put(%buddyName, %tag @ %buddyName @ "\x11");
            }
            if ((0.0 == %buddyState)) {
                %tag = "\x10\x06";
                %offlineList.put(%buddyName, %tag @ %buddyName @ "\x11");
            }
            if ((1.0 == %buddyState)) {
                %tag = "\x10\x07";
                %onlineList.put(%buddyName, %tag @ %buddyName @ "\x11");
            }
            if ((2.0 == %buddyState)) {
                %tag = "\x10\x0B";
                %idleAwayList.put(%buddyName, %tag @ %buddyName @ "\x11");
            }
            if ((3.0 == %buddyState)) {
                %tag = "\x10\x0C";
                %idleAwayList.put(%buddyName, %tag @ %buddyName @ "\x11");
            }
            %offlineList.put(%buddyName, %buddyName);
            %i = (1.0 + %i);
        }
        %onlineList.forEach("addToAIMList");
        %idleAwayList.forEach("addToAIMList");
        %offlineList.forEach("addToAIMList");
        %onlineList.delete();
        %idleAwayList.delete();
        %offlineList.delete();
    }
};
function BuddyHudWin::clearSelections(%this) {
    if (isObject(AIMBuddyList)) {
        -(1.0).setSelectedRow();
    }
};
STATE_PARSE_RESULT = 1 @ BuddyHudWin;
STATE_PARSE_BUDDIES = 2 @ BuddyHudWin;
function BuddyHudFriendsList::onCreatedChild(%this, %child) {
    Parent::onCreatedChild(%this, %child);
    %position = %child.getPosition();
    %extent = %child.getExtent();
    %newHeight = (%this.spacing + getWord(%extent, 1));
    %child.resize(getWord(%position, 0), getWord(%position, 1), getWord(%extent, 0), %newHeight);
    if (!(getWord(%child.getNamespaceList(), 0) $= "BuddyHudFriendsListLine")) {
        %child.bindClassName("BuddyHudFriendsListLine");
    }
};
function BuddyHudFriendsListLine::updateText(%this, %newText) {
    %fieldCount = getFieldCount(%newText);
    %objectCount = %this.getCount();
    if ((%fieldCount < %objectCount)) {
        %fieldCount = %objectCount;
    }
    %i = 0;
    if ((%fieldCount < %i)) {
        %this.getObject(%i).setText(getField(%newText, %i));
        %i = (1.0 + %i);
    }
};
function BuddyHudFriendsListLine::onMouseEnter(%this) {
    %this.updateText(%this.entryHilited);
};
function BuddyHudFriendsListLine::onMouseLeave(%this) {
    %this.updateText(%this.entryDefault);
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
$gBuddyListLastPopulateTime = "";
$gBuddyListPopulateTimer = "";
$gBuddyListMinRepopulatePeriodMS = 4000;
function BuddyHudWin::populateBuddyLists(%this) {
    %doit = 0;
    if (($gBuddyListLastPopulateTime $= "")) {
        %doit = 1;
    }
    %timeSinceLastPopulate = mSubS32(getSimTime(), $gBuddyListLastPopulateTime);
    if (($gBuddyListMinRepopulatePeriodMS > %timeSinceLastPopulate)) {
        %doit = 1;
    }
    if (!($gBuddyListPopulateTimer $= "")) {
        cancel($gBuddyListPopulateTimer);
    }
    $gBuddyListPopulateTimer = %this.schedule($gBuddyListMinRepopulatePeriodMS, "populateBuddyLists");
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
    %this.initializeBuddyList("FrndsOnline");
    %this.initializeBuddyList("FrndsHere");
    %this.initializeBuddyList("FrndsNPC");
    %this.initializeBuddyList("FrndsThere");
    %this.initializeBuddyList("FrndsInTransit");
    %this.initializeBuddyList("FrndsOffline");
    %this.initializeBuddyList("FavesHere");
    %this.initializeBuddyList("FavesNPC");
    %this.initializeBuddyList("FavesThere");
    %this.initializeBuddyList("FavesOffline");
    %this.initializeBuddyList("FansHere");
    %this.initializeBuddyList("FansNPC");
    %this.initializeBuddyList("FansThere");
    %this.initializeBuddyList("FansOffline");
    if (%timeIt) {
        error(getScopeName() @ " " @ "- A" @ " " @ formatFloat("%8.3f", (1000.0 / (%startTime - getSimTime()))) @ " " @ formatFloat("%8.3f", (1000.0 / (%lastTime - getSimTime()))));
        %lastTime = getSimTime();
    }
    %heightOfRowInFriendsList = 0;
    %offsetForVerticalPositionOfList = 0;
    if ((0.0 > BuddyHudFriendsList.getCount())) {
        %verticalPositionOfList = mMax((getWord(BuddyHudFriendsList.getPosition(), 1) - 0.0), 0);
        %heightOfRowInFriendsList = getWord(0.getObject().getExtent(), 1);
        BuddyHudFriendsList;
        %indexOfNameAtTopOfFriendsList = mCeil((%heightOfRowInFriendsList / %verticalPositionOfList));
        %offsetForVerticalPositionOfList = (%verticalPositionOfList - (%heightOfRowInFriendsList * %indexOfNameAtTopOfFriendsList));
        %nameAtTopOfFriendsList = StripMLControlChars(%indexOfNameAtTopOfFriendsList.getObject().getObject(1).getText());
        BuddyHudFriendsList;
        %nameAtTopOfFriendsListisNPC = %indexOfNameAtTopOfFriendsList.getObject().isNPCEntry;
        BuddyHudFriendsList;
    }
    %nameAtTopOfFriendsList = "";
    %nameAtTopOfFriendsListisNPC = 0;
    %numberOfNamesInsertedAbove = 0;
    %indexOfNameAtTopOfFriendsList.getObject().startingPos = BuddyHudFriendsList.getPosition() @ BuddyHudFriendsList;
    BuddyHudFriendsList.deleteMembers();
    0;
    %botsFriendsList = new ""() {
        ignoreCase = StringMap @ "true";
    };
    %botsFriendsList.clear();
    if (isObject(MissionCleanup)) {
        %botsFriendsList.add();
    }
    startingPos = BuddyHudRequestsList.getPosition() @ BuddyHudRequestsList;
    MissionCleanup;
    "".setText();
    if (%timeIt) {
        error(getScopeName() @ " " @ "- B" @ " " @ formatFloat("%8.3f", (1000.0 / (%startTime - getSimTime()))) @ " " @ formatFloat("%8.3f", (1000.0 / (%lastTime - getSimTime()))));
        %lastTime = getSimTime();
        BuddyHudRequestsList;
    }
    %friendCount = UserListFriends.size();
    if (0) {
        %includeInListCount = 0;
        %i = 0;
        if ((%friendCount < %i)) {
            %friend = %i.getValue();
            UserListFriends;
            if (!(%friend.serverName $= "")) {
            }
            if (%friend.loggedIn) {
                %includeInListCount = (1.0 + %includeInListCount);
            }
            %i = (1.0 + %i);
        }
        %includeInListCount.setNumChildren();
    }
    if (%timeIt) {
        error(getScopeName() @ " " @ "- X" @ " " @ formatFloat("%8.3f", (1000.0 / (%startTime - getSimTime()))) @ " " @ formatFloat("%8.3f", (1000.0 / (%lastTime - getSimTime()))));
        %lastTime = getSimTime();
        BuddyHudFriendsList;
    }
    %friendCountOnline = 0;
    (%friendCount < %i);
    %i = 0;
    if ((%friendCount < %i)) {
        %friend = %i.getValue();
        UserListFriends;
        %includeInList = 1;
        %cityTagDefault = "";
        %cityTagHilited = "";
        %sameServerTagDefault = "";
        %sameServerTagHilited = "";
        %ghost = Player::findPlayerInstance(%friend.name);
        if (!(%ghost $= "")) {
        }
        if (isObject(%ghost)) {
            %ghost.setBuddy(1);
            %ghost.setAmFave(1);
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
            log("list not defined:" @ " " @ %listName);
        }
        %list.put(%friend.name, "placeholder");
        if (%includeInList) {
            if ((error @ " " @ %friend.serverName $= $ServerName)) {
                if (%friend.isIdle) {
                    %sameServerTagDefault = "<modulationColor:ffffffa0><bitmap:platform/client/ui/friendsHud_lightning_i>";
                    relations;
                }
                %sameServerTagDefault = "<modulationColor:ffffffa0><bitmap:platform/client/ui/friendsHud_lightning_n>";
                %sameServerTagHilited = "<modulationColor:ffffffa0><bitmap:platform/client/ui/friendsHud_lightning_h>";
            }
            %sameServerTagDefault = "";
            %sameServerTagHilited = "";
            if (%friend.name.getIgnoreStatus()) {
                %ignoreTag = "<strikethrough>";
                BuddyHudWin;
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
            %colorTagDefault = "normalTextColor" @ BuddyHudTabs @ %friend.vars @ ">";
            %friend.vars;
            %colorTagHilited = "hilitedTextColor" @ BuddyHudTabs @ %friend.vars @ ">";
            "<color:";
            %visibleName = %npcOpen @ %friend.name @ %npcClose;
            "idleTextColor" @ BuddyHudTabs;
            %friendNameTagDefault = "<clip:100>" @ %ignoreTag @ %boldTag @ %colorTagDefault @ %visibleName @ "</a></clip>";
            "<color:";
            %friendNameTagHilited = "<clip:100>" @ %ignoreTag @ %boldTag @ %colorTagHilited @ %visibleName @ "</a></clip>";
            if ((%friend.activities $= "")) {
            }
            %activityName = getField(%friend.activities, 0);
            "";
            %activityBitmapMLText = getUserActivityMgr().getActivityBitmapMLText(%activityName);
            %activityColorDefault = %friend.isIdle ? "ffffff70" : "ffffff80";
            %activityColorHilited = %friend.vars;
            "hilitedTextColor" @ BuddyHudTabs;
            %activityDefault = "<modulationColor:" @ %activityColorDefault @ ">" @ %activityBitmapMLText;
            %activityHilited = "<modulationColor:" @ %activityColorHilited @ ">" @ %activityBitmapMLText;
            %entryDefault = %sameServerTagDefault @ "\t" @ %friendNameTagDefault @ "\t" @ %cityTagDefault @ %activityDefault;
            %entryHilited = %sameServerTagHilited @ "\t" @ %friendNameTagHilited @ "\t" @ %cityTagHilited @ %activityHilited;
            %comparisonToNameAtTopOfList = stricmp(%visibleName, %nameAtTopOfFriendsList);
            if (%friend.isNPC) {
                %entryInfo = new ""();;
                SimObject;
                %entryInfo.entryDefault = 0 @ %entryDefault;
                %entryInfo.entryHilited = %entryHilited;
                %entryInfo.name = %friend.name;
                %botsFriendsList.put(%friend.name, %entryInfo);
                if (%nameAtTopOfFriendsListisNPC) {
                }
                if ((0.0 < %comparisonToNameAtTopOfList)) {
                    %numberOfNamesInsertedAbove = (1.0 + %numberOfNamesInsertedAbove);
                }
            }
            %line = %entryDefault.addLineNoReseat();
            BuddyHudFriendsList;
            %line.entryDefault = %entryDefault;
            %line.entryHilited = %entryHilited;
            %line.linkText = munge(%friend.name);
            %line.isNPCEntry = 0;
            if (%nameAtTopOfFriendsListisNPC) {
            }
            if ((0.0 < %comparisonToNameAtTopOfList)) {
                %numberOfNamesInsertedAbove = (1.0 + %numberOfNamesInsertedAbove);
            }
            %friendCountOnline = (1.0 + %friendCountOnline);
        }
        %i = (1.0 + %i);
    }
    if (%timeIt) {
        error(getScopeName() @ " " @ "- C" @ " " @ formatFloat("%8.3f", (1000.0 / (%startTime - getSimTime()))) @ " " @ formatFloat("%8.3f", (1000.0 / (%lastTime - getSimTime()))));
        %lastTime = getSimTime();
        (%friendCount < %i);
    }
    %botFriendsCount = %botsFriendsList.size();
    %i = 0;
    if ((%botFriendsCount < %i)) {
        %entryInfo = %botsFriendsList.getValue(%i);
        %line = %entryInfo.entryDefault.addLineNoReseat();
        BuddyHudFriendsList;
        %line.entryDefault = %entryInfo.entryDefault;
        %line.entryHilited = %entryInfo.entryHilited;
        %line.linkText = munge(%entryInfo.name);
        %line.isNPCEntry = 1;
        %entryInfo.delete();
        %i = (1.0 + %i);
    }
    %botsFriendsList.clear();
    %botsFriendsList.delete();
    if ((0.0 > BuddyHudFriendsList.getCount())) {
        %newVerticalPositionOfList = ((%numberOfNamesInsertedAbove * %heightOfRowInFriendsList) - %offsetForVerticalPositionOfList);
        (%botFriendsCount < %i);
        %line.startingPos = getWord(%line.startingPos, 0) @ " " @ %newVerticalPositionOfList @ BuddyHudFriendsList;
        BuddyHudFriendsList;
    }
    if (%timeIt) {
        error(getScopeName() @ " " @ "- D" @ " " @ formatFloat("%8.3f", (1000.0 / (%startTime - getSimTime()))) @ " " @ formatFloat("%8.3f", (1000.0 / (%lastTime - getSimTime()))));
        %lastTime = getSimTime();
    }
    %favoriteCount = UserListFavorites.size();
    %i = 0;
    if ((%favoriteCount < %i)) {
        %favorite = %i.getValue();
        UserListFavorites;
        %color = $NameColorNormal;
        %listName = "FavesOffline";
        %ghost = Player::findPlayerInstance(%favorite.name);
        if (!(%ghost $= "")) {
            %ghost.setBuddy(1);
        }
        if (!(%listName $= "")) {
            if (%favorite.name.getIgnoreStatus()) {
                %ignoreTag = "<strikethrough>";
                BuddyHudWin;
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
                log("unknown list" @ " " @ %listName);
            }
            %list.put(%favorite.name, %entry);
        }
        log("unsortable fave buddy" @ " " @ %favorite.name);
        %i = (1.0 + %i);
        error;
    }
    if (%timeIt) {
        error(getScopeName() @ " " @ "- E" @ " " @ formatFloat("%8.3f", (1000.0 / (%startTime - getSimTime()))) @ " " @ formatFloat("%8.3f", (1000.0 / (%lastTime - getSimTime()))));
        %lastTime = getSimTime();
        (%favoriteCount < %i);
    }
    %fanCount = UserListFans.size();
    relations;
    %i = 0;
    error;
    if ((%fanCount < %i)) {
        %fan = %i.getValue();
        UserListFans;
        %listName = "";
        relations;
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
            if ((0.0 != %vipRoleMasks)) {
            }
            if ((0.0 != (%vipRoleMasks & %fan.roles))) {
                %color = $NameColorStaff;
                %listName = "FansHere";
            }
            %color = $NameColorNormal;
            %listName = "FansHere";
        }
        %color = $NameColorElsewhere;
        %listName = "FansThere";
        if (!(%listName $= "")) {
            if (%fan.name.getIgnoreStatus()) {
                %ignoreTag = "<strikethrough>";
                BuddyHudWin;
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
                log("unknown list" @ " " @ %listName);
            }
            %list.put(%fan.name, %entry);
        }
        log("unsortable fan buddy" @ " " @ %fan.name);
        %i = (1.0 + %i);
        error;
    }
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    (%fanCount < %i);
    %playerCount = %dict.size();
    relations;
    %i = 0;
    error;
    if ((%playerCount < %i)) {
        %ghost = %dict.getValue(%i);
        relations;
        if (isObject(%ghost)) {
            %ghost.setIgnore(%ghost.getShapeName().getIgnoreStatus());
        }
        %i = (1.0 + %i);
        BuddyHudWin;
    }
    rentabotClient_reignore();
    if (%timeIt) {
        error(getScopeName() @ " " @ "- F" @ " " @ formatFloat("%8.3f", (1000.0 / (%startTime - getSimTime()))) @ " " @ formatFloat("%8.3f", (1000.0 / (%lastTime - getSimTime()))));
        %lastTime = getSimTime();
        (%playerCount < %i);
    }
    %bitmap = (0.0 > UserListFans.size()) ? "platform/client/buttons/pending_active" : "platform/client/buttons/pending";
    %elButton = "requests".getTabWithName().button;
    BuddyHudTabs;
    %elButton.setBitmap(%bitmap);
    if ((0.0 > %friendCount)) {
        "You have" @ " " @ (%friendCountOnline - %friendCount) @ " " @ "friends offline".setText();
    }
    "".setText();
    %this.setCurListName("WaitingForYourApproval");
    %this.putListIntoList("FansHere");
    %this.putListIntoList("FansThere");
    %this.putListIntoList("FansOffline");
    %this.setCurListName("YourPendingRequests");
    %this.putListIntoList("FavesHere");
    %this.putListIntoList("FavesThere");
    %this.putListIntoList("FavesOffline");
    if (%timeIt) {
        error(getScopeName() @ " " @ "- G" @ " " @ formatFloat("%8.3f", (1000.0 / (%startTime - getSimTime()))) @ " " @ formatFloat("%8.3f", (1000.0 / (%lastTime - getSimTime()))));
        %lastTime = getSimTime();
        BuddyHudRequestsList;
    }
    "requests".getTabWithName().startingPos.scrollToPos();
    "requests".getTabWithName().startingPos.scrollToPos();
    (0.0 == %friendCount).setVisible();
    (0.0 == (%fanCount + %favoriteCount)).setVisible();
    BuddyHudTabs.setUhOhTabVisibility();
    BuddyHudFriendsList.reseatChildren();
    if (%timeIt) {
        error(getScopeName() @ " " @ "- H" @ " " @ formatFloat("%8.3f", (1000.0 / (%startTime - getSimTime()))) @ " " @ formatFloat("%8.3f", (1000.0 / (%lastTime - getSimTime()))));
        %lastTime = getSimTime();
        BuddyHudRequestsInfo;
    }
};
function BuddyHudWin::getNamesPendingMyApproval(%this) {
    return %this.getNamesInLists("FansHere FansNPC FansThere FansOffline");
};
function BuddyHudWin::getNamesPendingTheirApproval(%this) {
    return %this.getNamesInLists("FavesHere FavesNPC FavesThere FavesOffline");
};
function BuddyHudWin::getNamesInLists(%this, %lists) {
    %ret = "";
    %delim = "";
    %n = (1.0 - getWordCount(%lists));
    if ((0.0 >= %n)) {
        %list = %this.buddyLists;
        getWord(%lists, %n);
        if (isObject(%list)) {
            %m = (1.0 - %list.size());
            if ((0.0 >= %m)) {
                %ret = %ret @ %delim @ %list.getKey(%m);
                %delim = "\t";
                %m = (1.0 - %m);
            }
        }
        %n = (1.0 - %n);
        (0.0 >= %m);
    }
    return %ret;
};
function BuddyHudWin::initializeBuddyList(%this, %listName) {
    if (!(isObject(%this.buddyLists))) {
        0;
        %this.buddyLists = %listName @ new ""() {
            ignoreCase = StringMap @ "true";
        }; @ %listName
    }
    if (isObject(MissionCleanup)) {
        %this.buddyLists.add();
    }
    %list = %this.buddyLists;
    MissionCleanup @ %listName @ %listName;
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    %count = %list.size();
    %idx = 0;
    if ((%count < %idx)) {
        %ghost = %dict.get(%list.getKey(%idx));
        if (!(%ghost $= "")) {
        }
        if (isObject(%ghost)) {
            %ghost.setBuddy(0);
            %ghost.setAmFave(0);
            %ghost.setIgnore(0);
        }
        %idx = (1.0 + %idx);
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
        log("unknown list" @ " " @ %srcList);
        return error;
    }
    if ((0.0 > %list.size())) {
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
        %destList.setText(%destList.getText() @ %titleLine @ "<br>");
        if ((%this.curListName $= "WaitingForYourApproval")) {
        }
        if (!(%this[$UserPref::buddies::collapsedLists @ %this.curListName])) {
            %formatStr = "<spush><linkcolor:" @ ColorIToHex("255 147 248") @ ">";
            %destList.setText(%destList.getText() @ %formatStr @ "  [<a:gamelink approveall>Approve All</a>]   [<a:gamelink declineall>Decline All</a>]<spop><br>");
        }
    }
    if (!(%this[$UserPref::buddies::collapsedLists @ %this.curListName])) {
        %this.putIntoList = %destList;
        %list.forEach("addToFavList");
    }
};
function BuddyHudWin::putListIntoTab(%this, %listName, %destMLTextCtrl) {
    %srcStringMap = %this.buddyLists;
    %listName;
    if (!(isObject(%srcStringMap))) {
        log("unknown list" @ " " @ %listName);
        return error;
    }
    if (!(isObject(%destMLTextCtrl))) {
        log("unknown object" @ " " @ %destMLTextCtrl);
    }
    %destMLTextCtrl.setText("");
    %outputText = "";
    error;
    %size = %srcStringMap.size();
    relations;
    if ((0.0 == %size)) {
        %outputText = "";
    }
    %outputText = %srcStringMap.getValue(0);
    %i = 1;
    if ((%size < %i)) {
        %outputText = %outputText @ "\n" @ %srcStringMap.getValue(%i);
        %i = (1.0 + %i);
    }
    %destMLTextCtrl.setText(%outputText);
};
function StringMap::addToFavList(%this, %key, %value) {
    %destList = %this.putIntoList;
    BuddyHudWin;
    %destList.setText(%destList.getText() @ %value @ "<br>");
};
function BuddyHudWin::isFriendOrFavOnlineElsewhere(%this, %playerName) {
    if (%this.isInBuddyList(%playerName, "FrndsThere")) {
        return 1;
    }
    if (%this.isInBuddyList(%playerName, "FavesThere")) {
        return 1;
    }
    return 0;
};
function BuddyHudWin::isFriendOrFavOnlineHere(%this, %playerName) {
    if (%this.isInBuddyList(%playerName, "FrndsHere")) {
        return 1;
    }
    if (%this.isInBuddyList(%playerName, "FavesHere")) {
        return 1;
    }
    return 0;
};
function BuddyHudWin::isFriendOrFavOffline(%this, %playerName) {
    if (%this.isInBuddyList(%playerName, "FrndsOffline")) {
        return 1;
    }
    if (%this.isInBuddyList(%playerName, "FavesOffline")) {
        return 1;
    }
    return 0;
};
function BuddyHudWin::isOnlineHereOrNotFavorite(%this, %playerName) {
    if (%this.isFriendOrFavOnlineHere(%this, %playerName)) {
        return 1;
    }
    if (%this.isInBuddyList(%playerName, "FrndsNPC")) {
        return 1;
    }
    if (%this.isInBuddyList(%playerName, "FavesNPC")) {
        return 1;
    }
    if (%this.isFriendOrFavOnlineElsewhere(%this, %playerName)) {
        return 0;
    }
    if (%this.isFriendOrFavOffline(%this, %playerName)) {
        return 0;
    }
    return 1;
};
function BuddyHudWin::isInBuddyList(%this, %playerName, %listName) {
    %list = %this.buddyLists;
    %listName;
    if (!(isObject(%list))) {
        if (!($StandAlone)) {
            log("isInBuddyList(): unknown list" @ " " @ %listName);
        }
        return error;
    }
    return !(%list.get(%playerName) $= "");
};
function BuddyHudWin::getFriendStatus(%this, %playerName) {
    safeEnsureScriptObjectWithInit("StringMap", "UserListFriends", "{ ignoreCase = true; }");
    safeEnsureScriptObjectWithInit("StringMap", "UserListFavorites", "{ ignoreCase = true; }");
    safeEnsureScriptObjectWithInit("StringMap", "UserListFans", "{ ignoreCase = true; }");
    if (%playerName.hasKey()) {
        return "friends";
    }
    if (%playerName.hasKey()) {
        return "favorite";
    }
    if (%playerName.hasKey()) {
        return "fan";
    }
    return "none";
};
function BuddyHudWin::getIgnoreStatus(%this, %playerName) {
    safeEnsureScriptObjectWithInit("StringMap", "UserListIgnores", "{ ignoreCase = true; }");
    if (%playerName.hasKey()) {
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
    if ((%num < %n)) {
        %ghost = %dict.get(%n.getKey());
        UserListIgnores;
        if (isObject(%ghost)) {
            %ghost.setIgnore(0);
        }
        %n = (1.0 + %n);
    }
    extractBuddyRecords(%request, "friend", 0);
    extractBuddyRecords(%request, "favorite", 0);
    extractBuddyRecords(%request, "fan", 0);
    extractBuddyRecords(%request, "ignore", 1);
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
    if (0) {
        if (%this.firstTime) {
            %this.firstTime = 0 @ BuddyHudWin;
            BuddyHudWin;
            if ((0.0 > %request.getValue("fanCount"))) {
                BuddyHudWin.open();
                "requests".selectTabWithName();
            }
        }
    }
    if ((0.0 > UserListUnknownServerName.size())) {
        WorldMap.refresh();
    }
};
function extractBuddyRecords(%request, %list, %type, %doIgnore) {
    %list.deleteValuesAsObjects();
    %num = %request.getValue(%type @ "Count");
    log("network", "debug", getScopeName() @ " " @ "- " @ %type @ "Count =" @ " " @ %num);
    %n = 0;
    if ((%num < %n)) {
        %key = %type @ %n;
        %val = %request.getValue(%key);
        log("network", "debug", getScopeName() @ " " @ " -" @ " " @ %key @ " " @ "=" @ " " @ %val);
        %record = new ""();;
        ScriptObject;
        parseBuddyRecord(%record, %val);
        %list.put(%record.name, %record);
        if (!(0 @ " " @ %record.serverName $= "")) {
        }
        if ((%record.csn $= "")) {
            %record.name.put(%record);
            warn(getScopeName() @ " " @ "- unrecogized server name '" @ %record.serverName @ "' for " @ %type @ " user '" @ %record.name @ "'");
        }
        if (%doIgnore) {
            %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
            UserListUnknownServerName;
            %ghost = %dict.get(%list.getKey(%n));
            if (isObject(%ghost)) {
                %ghost.setIgnore(1);
            }
        }
        %n = (1.0 + %n);
    }
};
function parseBuddyRecord(%record, %val) {
    %fieldNdx = 0;
    %record.name = getField(%val, %fieldNdx);
    %fieldNdx = (1.0 + %fieldNdx);
    %record.serverName = getField(%val, %fieldNdx);
    %fieldNdx = (1.0 + %fieldNdx);
    %roles = getField(%val, %fieldNdx);
    %fieldNdx = (1.0 + %fieldNdx);
    if ((%roles $= "")) {
        %roles = 0;
    }
    eval("%record.roles = " @ %roles @ ";");
    %record.loggedIn = getField(%val, %fieldNdx);
    %fieldNdx = (1.0 + %fieldNdx);
    %record.isIdle = getField(%val, %fieldNdx);
    %fieldNdx = (1.0 + %fieldNdx);
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
    %record.csn = BuddyHudTabs @ %record.serverName.getCityNameForServerName();
    %record.activities = %record.isIdle ? "idle" : "";
};
function BuddyHudTabs::getCityNameForServerName(%this, %ServerName) {
    %csn = %ServerName.cityNameForServerName();
    WorldMap;
    return %csn;
};
function BuddyHudTabs::updateUserListUnknownServerName(%this) {
    safeEnsureScriptObject("StringMap", "UserListUnknownServerName");
    %originalSize = UserListUnknownServerName.size();
    %i = (1.0 - %originalSize);
    if ((0.0 >= %i)) {
        %record = %i.getValue();
        UserListUnknownServerName;
        %record.csn = BuddyHudTabs @ %record.serverName.getCityNameForServerName();
        if (!(%record.csn $= "")) {
            %i.getKey().remove();
        }
        %record.csn = UserListUnknownServerName @ "?";
        UserListUnknownServerName;
        %i = (1.0 - %i);
    }
    if ((%originalSize != UserListUnknownServerName.size())) {
        BuddyHudWin.populateBuddyLists();
    }
};
function markIgnoredInList(%list) {
    %n = (1.0 - %list.size());
    if ((0.0 >= %n)) {
        %fave = %list.getValue(%n);
        %fave.ignored = BuddyHudWin @ %fave.name.getIgnoreStatus();
        %n = (1.0 - %n);
    }
};
$gRefreshEvenIfBuddyHudWinClosed = 1;
function clientCmdRefreshBuddies(%status) {
    log("relations", "debug", "clientCmdRefreshBuddies(" @ %status @ ")");
    if (%status) {
        if ($gRefreshEvenIfBuddyHudWinClosed) {
        }
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
    if ((0.0 > %request.getValue("friendCount"))) {
        %key = "friend0";
        %val = %request.getValue(%key);
        // unhandled opcode 4020 at 0x00003B2C
        %val = UserListFriends;
        log("network", "debug", getScopeName() @ " " @ " -" @ " " @ %key @ " " @ "=" @ " " @ %val);
    }
    if ((0.0 > %request.getValue("favoriteCount"))) {
        %key = "favorite0";
        %val = %request.getValue("favorite0");
        // unhandled opcode 4020 at 0x00003B84
        %val = UserListFavorites;
        log("network", "debug", getScopeName() @ " " @ " -" @ " " @ %key @ " " @ "=" @ " " @ %val);
    }
    if ((0.0 > %request.getValue("fanCount"))) {
        %key = "fan0";
        %val = %request.getValue("fan0");
        // unhandled opcode 4020 at 0x00003BDC
        %val = UserListFans;
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
            %oldList.remove(%name);
        }
        %record.delete();
    }
    if (!(%val $= "")) {
    }
    if (isObject(%list)) {
        %record = new ""();;
        ScriptObject;
        parseBuddyRecord(%record, %val);
        %list.put(%record.name, %record);
        if (!(0 @ " " @ %record.serverName $= "")) {
        }
        if ((%record.csn $= "")) {
            safeEnsureScriptObject("StringMap", "UserListUnknownServerName");
            %record.name.put(%record);
            warn(getScopeName() @ " " @ "- unrecogized server name '" @ %record.serverName @ "' for relation with single user '" @ %record.name @ "'");
            WorldMap.refresh();
        }
    }
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    UserListUnknownServerName;
    %ghost = %dict.get(%record.name);
    if (isObject(%ghost)) {
        %ghost.setIgnore(0);
    }
    %oldIgnoreRecord = findRelatedPlayerRecordInList(%name);
    UserListIgnores;
    if (isObject(%oldIgnoreRecord)) {
        %name.remove();
        %oldIgnoreRecord.delete();
    }
    if ((0.0 > %request.getValue("ignoreCount"))) {
        echo("have an ignore record...");
        %key = "ignore0";
        UserListIgnores;
        %val = %request.getValue("ignore0");
        %record = new ""();;
        ScriptObject;
        parseBuddyRecord(%record, %val);
        %record.name.put(%record);
        if (isObject(%ghost)) {
            echo("setting ignore to true for" @ " " @ %record.name);
            %ghost.setIgnore(1);
        }
    }
    markIgnoredInList(UserListFriends);
    markIgnoredInList(UserListFavorites);
    BuddyHudWin.populateBuddyLists();
};
function BuddyHudTabs::setBuddyIdleStatus(%this, %buddyName, %isIdle) {
    %entry = %buddyName.get();
    UserListFriends;
    if (!(isObject(%entry))) {
        return;
    }
    %entry.isIdle = %isIdle;
    if (%isIdle) {
        if (!(hasField(%entry.activities, "idle"))) {
            %entry.activities = "idle" @ "\t" @ %entry.activities;
        }
    }
    BuddyHudWin.populateBuddyLists();
};
function BuddyHudTabs::setBuddyActivities(%this, %buddyName, %activitiesList) {
    %entry = %buddyName.get();
    UserListFriends;
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
        %source.setBuddyIdleStatus(1);
    }
    if ((BuddyHudTabs @ " " @ %action $= "userToNonIdle")) {
        %source.setBuddyIdleStatus(0);
    }
    if ((BuddyHudTabs @ " " @ %action $= "friendRequestCreated")) {
        sendBuddyStatusRequest(%other);
        if ((%other $= %source)) {
            handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff><a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a> has just asked to be your friend!  <a:ACCEPT " @ munge(%other) @ ">Accept</a> | <a:DECLINE " @ munge(%other) @ ">Decline</a>");
        }
        handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff>You have asked <a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a> to be your friend.");
    }
    if ((%action $= "friendsCreated")) {
        sendBuddyStatusRequest(%other);
        handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff><a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a> is now your friend.");
        %other.updateFriendRequest(1);
    }
    if ((SystemMessageTextCtrl @ " " @ %action $= "friendsRemoved")) {
        sendBuddyStatusRequest(%other);
        handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff><a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a> is no longer your friend.");
    }
    if ((%action $= "friendRequestDenied")) {
        sendBuddyStatusRequest(%other);
        if ((%self $= %source)) {
            handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff>You declined <a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a>'s friend request.");
            %other.updateFriendRequest(0);
        }
    }
    if ((SystemMessageTextCtrl @ " " @ %action $= "friendRequestCancelled")) {
        sendBuddyStatusRequest(%other);
        if ((%self $= %source)) {
            handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff>You canceled your friend request to <a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a>.");
        }
        %other.updateFriendRequest(2);
    }
    if ((SystemMessageTextCtrl @ " " @ %action $= "ignoreAdded")) {
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
    %this.getParent().scrollTo(0, (getWord(%pos, 1) - 1.0));
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
    if ((400.0 < ($gLastNameClickTime - %curTime))) {
    }
    if (($gLastNameClickName $= %name)) {
        openUserWhisper(%name);
        if ((0.0 != $gLeftClickTimer)) {
            cancel($gLeftClickTimer);
            $gLeftClickTimer = 0;
        }
    }
    $gLeftClickTimer = schedule(450, 0, "onSingleClickPlayerName", %name, %objID);
    $gLastNameClickTime = %curTime;
    $gLastNameClickName = %name;
};
function onRightClickPlayerName(%name) {
    %name.initWithPlayerName();
    Canvas.getCursorPos().showAtPoint();
};
function onSingleClickPlayerName(%name, %objID) {
    if (showPlayerInfoPopup()) {
    }
    if (!(%name $= $player.getShapeName())) {
        InfoPopupDlg.open();
        %name.showInfoFor();
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
    %buddyName = stripUnprintables(%this.getRowTextById(%id));
    %buddyName.talkTo();
};
function AIMBuddyList::inviteSelected(%this) {
    %id = %this.getSelectedId();
    %buddyName = stripUnprintables(%this.getRowTextById(%id));
    %buddyName.open();
};
function AIMBuddyList::onRightMouseDown(%this, %unused, %unused, %mousePt) {
    if ((%mousePt $= "")) {
        return;
    }
    %aimName = stripUnprintables(%this.getRowText(%this.getMouseOverRow()));
    %aimName.initForAIM();
    %mousePt.showAtPoint();
};
function clientCmdBeginNPCList() {
    safeEnsureScriptObjectWithInit("StringMap", "NPCList", "{ ignoreCase = true; }");
    NPCList.clear();
};
function clientCmdItsAnNPC(%npcName) {
    safeEnsureScriptObjectWithInit("StringMap", "NPCList", "{ ignoreCase = true; }");
    %npcName.put(%npcName);
};
function isNPCName(%name) {
    if (!(isObject(NPCList))) {
        if (isObject(ServerConnection)) {
            error(getScopeName() @ " " @ "- NPC list not initialized." @ " " @ %name @ " " @ getTrace());
        }
        return 0;
    }
    %ret = !(NPCList @ " " @ %name.get() $= "");
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
    %rec = %list.get(%playerName);
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
    // unhandled opcode 4020 at 0x00004733
    if (isRelatedPlayerRecordInList(%playerName, %list)) {
        return %list;
    }
    // unhandled opcode 4020 at 0x0000474D
    %list = UserListFavorites;
    if (isRelatedPlayerRecordInList(%playerName, %list)) {
        return %list;
    }
    // unhandled opcode 4020 at 0x00004767
    %list = UserListFans;
    if (isRelatedPlayerRecordInList(%playerName, %list)) {
        return %list;
    }
    // unhandled opcode 4020 at 0x00004781
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
    %row = %selected.getRowNumById();
    AIMBuddyList;
    %row = (1.0 + %row);
    if ((AIMBuddyList.rowCount() >= %row)) {
        %row = 0;
    }
    %row.setSelectedRow();
};
function Player::onGotBuddyStatus(%this) {
    %this.schedule(0, "updateMapIcon");
};
function Player::onGotFaveStatus(%this) {
    %this.schedule(0, "updateMapIcon");
};
