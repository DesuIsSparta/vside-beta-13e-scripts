if (!(isObject())) {
    class = BuddyHudTabs @ new ScriptObject(BuddyHudTabs) @ "TabControl";
    if (isObject()) {
        add();
    }
}
vars = MissionCleanup @ BuddyHudTabs @ 0 @ "columnPadding" @ BuddyHudTabs;
MissionCleanup;
vars = 0 @ "rowPadding" @ BuddyHudTabs;
vars = 22 @ "offlineFriendsTextboxHeight" @ BuddyHudTabs;
vars = 28 @ "buddyListLegendTextboxHeight" @ BuddyHudTabs;
vars = ColorIToHex($NameColorFriend) @ "normalTextColor" @ BuddyHudTabs;
vars = ColorFToHex(ColorConvolve($NameColorFriendF, $NameColorIdleModulationF)) @ "idleTextColor" @ BuddyHudTabs;
vars = ColorIToHex($NameColorHilite) @ "hilitedTextColor" @ BuddyHudTabs;
function BuddyHudTabs::setup(%this) {
    if (!(initialized)) {
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
        refreshAIMBuddyList();
        position = GuiBitmapButtonCtrl @ new ""() @ "115 4";
        0;
        extent = BuddyHudWin @ BuddyHudTabContainer @ "42 21";
        favoritesTimer;
        bitmap = BuddyHudTabContainer @ BuddyHudWin @ "platform/client/buttons/inviteFriends";
        %this;
        command = "doInviteFriends();";
        position = GuiMLTextCtrl @ new ""() @ "5 2";
        extent = "36 16";
        profile = "ETSNonModalProfile";
        text = "<color:ddffdd><font:arial:16>Invite!</a>";
        .add();
        setup();
    }
};
function BuddyHudTabs::OnETSInviteFriends(%this) {
    if (!(isObject())) {
        error("no EtsInviteDialog, this should not happen");
        return EtsInviteDialog;
    }
    if (!(isVisible())) {
        open();
    }
};
function BuddyHudTabs::fillFriendsTab(%this) {
    %theTab = %this.getTabWithName("friends");
    profile = GuiScrollCtrl @ new ""() @ "ETSScrollProfile";
    0;
    horizSizing = "width";
    vertSizing = "top relative";
    position = "0 0";
    extent = getWord(%theTab.getExtent(), 0) @ " " @ "buddyListLegendTextboxHeight" @ BuddyHudTabs @ ((vars @ "offlineFriendsTextboxHeight" @ BuddyHudTabs + vars) - getWord(%theTab.getExtent(), 1));
    minExtent = "10 10";
    sluggishness = -1;
    visible = 1;
    willFirstRespond = 1;
    hScrollBar = "alwaysOff";
    vScrollBar = "dynamic";
    constantThumbHeight = 1;
    helpTag = 0;
    %scroll = ;
    profile = GuiArray2Ctrl @ new ""() @ "CSProfileListBox";
    0;
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
    %friendsList = ;
    %friendsList.bindClassName("MenuControl");
    %friendsList.bindClassName("TabbedTextControl");
    %friendsList.bindClassName("BuddyHudFriendsList");
    %friendsList.setName("BuddyHudFriendsList");
    %fieldWidths = "10 94 46";
    teleFieldIndex = 0 @ %friendsList;
    nameFieldIndex = 1 @ %friendsList;
    cityFieldIndex = 2 @ %friendsList;
    %friendsList.setFieldWidths(%fieldWidths, vars);
    %friendsList.clear();
    %scroll.add(%friendsList);
    %theTab.add(%scroll);
    profile = "columnPadding" @ BuddyHudTabs @ new GuiMLTextCtrl(BuddyHudFriendsListOfflineFriendsBox) @ "ETSSmallTextListProfile";
    horizSizing = "width";
    vertSizing = "top";
    position = 4 @ " " @ "buddyListLegendTextboxHeight" @ BuddyHudTabs @ ((vars @ "offlineFriendsTextboxHeight" @ BuddyHudTabs + vars) - getWord(%theTab.getExtent(), 1));
    extent = getWord(%theTab.getExtent(), 0) @ " " @ "offlineFriendsTextboxHeight" @ BuddyHudTabs @ vars;
    minExtent = "offlineFriendsTextboxHeight" @ BuddyHudTabs @ vars @ " " @ "offlineFriendsTextboxHeight" @ BuddyHudTabs @ vars;
    sluggishness = -1;
    visible = 1;
    text = "";
    %theTab.add();
    profile = new GuiMLTextCtrl(BuddyHudFriendsListLegendContainer) @ "GuiDefaultProfile";
    horizSizing = "width";
    vertSizing = "top";
    position = 4 @ " " @ "buddyListLegendTextboxHeight" @ BuddyHudTabs @ (vars - getWord(%theTab.getExtent(), 1));
    extent = getWord(%theTab.getExtent(), 0) @ " " @ "buddyListLegendTextboxHeight" @ BuddyHudTabs @ vars;
    minExtent = "buddyListLegendTextboxHeight" @ BuddyHudTabs @ vars @ " " @ "buddyListLegendTextboxHeight" @ BuddyHudTabs @ vars;
    profile = new GuiMLTextCtrl(BuddyHudFriendsListLegend) @ "ETSTinyTextListProfile";
    horizSizing = "width";
    vertSizing = "top";
    position = "0 0";
    extent = getWord(%theTab.getExtent(), 0) @ " " @ "buddyListLegendTextboxHeight" @ BuddyHudTabs @ vars;
    minExtent = "buddyListLegendTextboxHeight" @ BuddyHudTabs @ vars @ " " @ "buddyListLegendTextboxHeight" @ BuddyHudTabs @ vars;
    sluggishness = -1;
    visible = 1;
    text = "<tab:20,90>" @ "<spush><color:" @ "idleTextColor" @ BuddyHudTabs @ vars @ ">Dim<spop>" @ "\t" @ "= Idle" @ "\t" @ "... = In Transit" @ "\n" @ " " @ "\t" @ "= Fast Teleport";
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "6 15";
    extent = "6 9";
    minExtent = "6 9";
    visible = 1;
    bitmap = "platform/client/ui/friendsHud_lightning_n";
    %theTab.add();
    profile = new GuiScrollCtrl(BuddyHudFriendsInfo) @ "ETSScrollProfile";
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
    profile = GuiMLTextCtrl @ new ""() @ "ETSTextListProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = "146 68";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = ;
    %theTab.add();
    if (showInviteFriend()) {
        profile = new GuiVariableWidthButtonCtrl(ETSInviteButton) @ "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "top";
        position = 8 @ " " @ (18.0 - getWord(%theTab.getExtent(), 1));
        extent = "140 15";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "BuddyHudTabs.OnETSInviteFriends();";
        text = "Invite Friends To Join";
        groupNum = -1;
        buttonType = "PushButton";
        %inviteButton = ;
        %theTab.add(%inviteButton);
    }
};
function BuddyHudTabs::fillRequestsTab(%this) {
    %theTab = %this.getTabWithName("requests");
    profile = GuiMLTextCtrl @ new ""() @ "ETSTextListProfile";
    0;
    horizSizing = "width";
    vertSizing = "bottom";
    position = "0 0";
    extent = "500 80";
    minExtent = "80 80";
    sluggishness = -1;
    visible = 1;
    %requestsList = ;
    %requestsList.bindClassName("BuddyHudPlayerList");
    %requestsList.setName("BuddyHudRequestsList");
    %requestsList.bindClassName("BuddyHudRequestsList");
    profile = GuiScrollCtrl @ new ""() @ "ETSScrollProfile";
    0;
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
    %scroll = ;
    %scroll.add(%requestsList);
    %theTab.add(%scroll);
    profile = new GuiScrollCtrl(BuddyHudRequestsInfo) @ "ETSScrollProfile";
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
    profile = GuiMLTextCtrl @ new ""() @ "ETSTextListProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = "146 68";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = ;
    %theTab.add();
};
function BuddyHudTabs::fillAIMTab(%this) {
    %theTab = %this.getTabWithName("AIM");
    if (!(isObject(%theTab))) {
        echo("Didn't find AIM Buddies tab");
        return 0;
    }
    $Player::AIMName = "";
    $Player::AIMPassword = "";
    profile = new GuiControl(AIMLoginFrame) @ "GuiDefaultProfile";
    horizSizing = "width";
    vertSizing = "bottom";
    position = "4 0";
    extent = "153 190";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    profile = GuiMLTextCtrl @ new ""() @ "ETSShadowTextProfile";
    horizSizing = "width";
    vertSizing = "bottom";
    position = "0 0";
    extent = "153 16";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "<a:http://www.aim.com/>Get an AIM Screen Name</a>";
    profile = GuiTextCtrl @ new ""() @ "ETSShadowTextProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 20";
    extent = "68 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Screen Name:";
    maxLength = 64;
    profile = new GuiTextEditCtrl(AIMScreenNameField) @ "ETSDarkTabbableTextEditProfile";
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
    profile = GuiTextCtrl @ new ""() @ "ETSShadowTextProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 60";
    extent = "68 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Password:";
    maxLength = 64;
    profile = new GuiTextEditCtrl(AIMPasswordField) @ "ETSDarkTabbableTextEditProfile";
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
    profile = new GuiCheckBoxCtrl(AIMRememberMeCheckbox) @ "ETSCheckBoxProfile";
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
    profile = new GuiCheckBoxCtrl(AIMSavePasswordCheckbox) @ "ETSCheckBoxProfile";
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
    profile = new GuiCheckBoxCtrl(AIMAutoSigninCheckbox) @ "ETSCheckBoxProfile";
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
    profile = new GuiVariableWidthButtonCtrl(AIMSignInButton) @ "BracketButton15Profile";
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
    %loginFrame = ;
    profile = GuiScrollCtrl @ new ""() @ "ETSScrollProfile";
    0;
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
    profile = new GuiTextListCtrl(AIMBuddyList) @ "AIMTextListProfile";
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
    %aimListScroll = ;
    profile = new GuiVariableWidthButtonCtrl(AIMSignOffButton) @ "BracketButton15Profile";
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
    %signOffButton = ;
    profile = new GuiVariableWidthButtonCtrl(AIMInviteButton) @ "BracketButton15Profile";
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
    %inviteButton = ;
    loginFrame = %loginFrame @ %theTab;
    %theTab.add(%loginFrame);
    aimListScroll = %aimListScroll @ %theTab;
    %theTab.add(%aimListScroll);
    signOffButton = %signOffButton @ %theTab;
    %theTab.add(%signOffButton);
    inviteButton = %inviteButton @ %theTab;
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
    profile = GuiScrollCtrl @ new ""() @ "ETSScrollProfile";
    0;
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
    %scroll = ;
    profile = GuiMLTextCtrl @ new ""() @ "ETSTextListProfile";
    0;
    horizSizing = "width";
    vertSizing = "bottom";
    position = "0 0";
    extent = (10.0 - getWord(%scroll.getExtent(), 0)) @ " " @ (10.0 - getWord(%scroll.getExtent(), 1));
    minExtent = "10 10";
    sluggishness = -1;
    visible = 1;
    text = ;
    %body = ;
    %theTab.add(%scroll);
    %scroll.add(%body);
};
function BuddyHudTabs::setUhOhTabVisibility(%this) {
    %showIt = (UserListFriends > size());
    250.0;
    if (%showIt) {
        %this.showTabWithName("UhOh");
    }
    %this.hideTabWithName("UhOh");
};
function SimGroup::hasObjectWithName(%this, %name) {
    %i = 0;
    if ((%this.getCount() < %i)) {
        if ((%this.getObject(%i) SPC name $= %name)) {
            return 1;
        }
        %i = (1.0 + %i);
    }
    return 0;
};
function BuddyHudWin::wakeUp(%this) {
    wakeUp();
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
    rowCount().addRow(%value);
};
function BuddyHudWin::refreshAIMBuddyList(%this) {
    if (isObject()) {
        class = StringMap @ new ""() @ "BuddyMap";
        0;
        ignoreCase = AIMBuddyList @ 1;
        %onlineList = ;
        class = StringMap @ new ""() @ "BuddyMap";
        0;
        ignoreCase = 1;
        %idleAwayList = ;
        class = StringMap @ new ""() @ "BuddyMap";
        0;
        ignoreCase = 1;
        %offlineList = ;
        if (isObject()) {
            %onlineList.add();
            %idleAwayList.add();
            %offlineList.add();
        }
        clear();
        %buddyCount = aimBuddyCount();
        AIMBuddyList;
        %i = 0;
        MissionCleanup;
        if ((%buddyCount < %i)) {
            %buddyName = aimGetBuddyName(%i);
            MissionCleanup;
            %buddyState = aimGetBuddyState(%i);
            MissionCleanup;
            if ((-(1.0) == %buddyState)) {
                %tag = "\x10\x06";
                MissionCleanup;
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
    if (isObject()) {
        -(1.0).setSelectedRow();
    }
};
STATE_PARSE_RESULT = 1 @ BuddyHudWin;
STATE_PARSE_BUDDIES = 2 @ BuddyHudWin;
function BuddyHudFriendsList::onCreatedChild(%this, %child) {
    Parent::onCreatedChild(%this, %child);
    %position = %child.getPosition();
    %extent = %child.getExtent();
    %newHeight = (spacing + getWord(%extent, 1));
    %this;
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
    %this.updateText(entryHilited);
};
function BuddyHudFriendsListLine::onMouseLeave(%this) {
    %this.updateText(entryDefault);
};
function BuddyHudFriendsListLine::onMouseDown(%this) {
};
function BuddyHudFriendsListLine::onMouseUp(%this) {
    %name = unmunge(linkText);
    %this;
    onLeftClickPlayerName(%name, "");
};
function BuddyHudFriendsListLine::onRightMouseUp(%this) {
    %name = unmunge(linkText);
    %this;
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
    if (!(isObject())) {
    }
    if (!(isObject())) {
        return BuddyHudRequestsList;
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
    if ((BuddyHudFriendsList > getCount())) {
        %verticalPositionOfList = mMax((getWord(getPosition(), 1) - 0.0), 0);
        BuddyHudFriendsList;
        %heightOfRowInFriendsList = getWord(0.getObject().getExtent(), 1);
        BuddyHudFriendsList;
        %indexOfNameAtTopOfFriendsList = mCeil((%heightOfRowInFriendsList / %verticalPositionOfList));
        0.0;
        %offsetForVerticalPositionOfList = (%verticalPositionOfList - (%heightOfRowInFriendsList * %indexOfNameAtTopOfFriendsList));
        %nameAtTopOfFriendsList = StripMLControlChars(%indexOfNameAtTopOfFriendsList.getObject().getObject(1).getText());
        BuddyHudFriendsList;
        %nameAtTopOfFriendsListisNPC = isNPCEntry;
        %indexOfNameAtTopOfFriendsList.getObject();
    }
    %nameAtTopOfFriendsList = "";
    BuddyHudFriendsList;
    %nameAtTopOfFriendsListisNPC = 0;
    %numberOfNamesInsertedAbove = 0;
    startingPos = BuddyHudFriendsList @ getPosition() @ BuddyHudFriendsList;
    deleteMembers();
    ignoreCase = StringMap @ new ""() @ "true";
    0;
    %botsFriendsList = BuddyHudFriendsList;
    %botsFriendsList.clear();
    if (isObject()) {
        %botsFriendsList.add();
    }
    startingPos = BuddyHudRequestsList @ getPosition() @ BuddyHudRequestsList;
    MissionCleanup;
    "".setText();
    if (%timeIt) {
        error(getScopeName() @ " " @ "- B" @ " " @ formatFloat("%8.3f", (1000.0 / (%startTime - getSimTime()))) @ " " @ formatFloat("%8.3f", (1000.0 / (%lastTime - getSimTime()))));
        %lastTime = getSimTime();
        BuddyHudRequestsList;
    }
    %friendCount = size();
    UserListFriends;
    if (0) {
        %includeInListCount = 0;
        MissionCleanup;
        %i = 0;
        if ((%friendCount < %i)) {
            %friend = %i.getValue();
            UserListFriends;
            if (!(%friend SPC serverName $= "")) {
            }
            if (loggedIn) {
                %includeInListCount = (1.0 + %includeInListCount);
                %friend;
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
        %ghost = Player::findPlayerInstance(name);
        %friend;
        if (!(%ghost $= "")) {
        }
        if (isObject(%ghost)) {
            %ghost.setBuddy(1);
            %ghost.setAmFave(1);
        }
        if (isNPC) {
            %listName = "FrndsNPC";
            %friend;
        }
        if ((%friend SPC serverName $= "")) {
            if (loggedIn) {
                %listName = "FrndsInTransit";
                %friend;
                %cityTagDefault = "<bitmap:platform/client/ui/friendsHud_transition_n>";
                %cityTagHilited = "<bitmap:platform/client/ui/friendsHud_transition_h>";
            }
            %listName = "FrndsOffline";
            %includeInList = 0;
        }
        if ((%friend SPC serverName $= $ServerName)) {
            %listName = "FrndsHere";
        }
        %listName = "FrndsThere";
        %list = buddyLists;
        %listName @ %this;
        if (!(isObject(%list))) {
            log("list not defined:" @ " " @ %listName);
        }
        %list.put(name, "placeholder");
        if (%includeInList) {
            if ((%friend SPC serverName $= $ServerName)) {
                if (isIdle) {
                    %sameServerTagDefault = "<modulationColor:ffffffa0><bitmap:platform/client/ui/friendsHud_lightning_i>";
                    %friend;
                }
                %sameServerTagDefault = "<modulationColor:ffffffa0><bitmap:platform/client/ui/friendsHud_lightning_n>";
                %friend;
                %sameServerTagHilited = "<modulationColor:ffffffa0><bitmap:platform/client/ui/friendsHud_lightning_h>";
                error;
            }
            %sameServerTagDefault = "";
            relations;
            %sameServerTagHilited = "";
            if (name.getIgnoreStatus()) {
                %ignoreTag = "<strikethrough>";
                %friend;
            }
            %ignoreTag = "";
            BuddyHudWin;
            if (isNPC) {
                %npcOpen = "";
                %friend;
                %npcClose = " (bot)";
            }
            %npcOpen = "";
            %npcClose = "";
            if ((%cityTagDefault $= "")) {
                %cityBitmap = DestinationList::GetAreaNameIconPath(csn);
                %friend;
                %cityTagDefault = "<modulationColor:ffffffa0><bitmap:" @ %cityBitmap @ %friend @ isIdle ? "_i" : "_n" @ ">";
                %cityTagHilited = "<modulationColor:ffffffc0><bitmap:" @ %cityBitmap @ "_h>";
            }
            %boldTag = "";
            if (isIdle) {
            }
            %colorTagDefault = %friend @ "idleTextColor" @ BuddyHudTabs @ vars @ "normalTextColor" @ BuddyHudTabs @ vars @ ">";
            "<color:";
            %colorTagHilited = "<color:" @ "hilitedTextColor" @ BuddyHudTabs @ vars @ ">";
            %visibleName = %npcOpen @ %friend @ name @ %npcClose;
            %friendNameTagDefault = "<clip:100>" @ %ignoreTag @ %boldTag @ %colorTagDefault @ %visibleName @ "</a></clip>";
            %friendNameTagHilited = "<clip:100>" @ %ignoreTag @ %boldTag @ %colorTagHilited @ %visibleName @ "</a></clip>";
            if ((%friend SPC activities $= "")) {
            }
            %activityName = getField(activities, 0);
            %friend;
            %activityBitmapMLText = getUserActivityMgr().getActivityBitmapMLText(%activityName);
            "";
            %activityColorDefault = isIdle ? "ffffff70" : "ffffff80";
            %friend;
            %activityColorHilited = vars;
            "hilitedTextColor" @ BuddyHudTabs;
            %activityDefault = "<modulationColor:" @ %activityColorDefault @ ">" @ %activityBitmapMLText;
            %activityHilited = "<modulationColor:" @ %activityColorHilited @ ">" @ %activityBitmapMLText;
            %entryDefault = %sameServerTagDefault @ "\t" @ %friendNameTagDefault @ "\t" @ %cityTagDefault @ %activityDefault;
            %entryHilited = %sameServerTagHilited @ "\t" @ %friendNameTagHilited @ "\t" @ %cityTagHilited @ %activityHilited;
            %comparisonToNameAtTopOfList = stricmp(%visibleName, %nameAtTopOfFriendsList);
            if (isNPC) {
                %entryInfo = new ""();
                SimObject;
                entryDefault = 0 @ %entryDefault @ %entryInfo;
                %friend;
                entryHilited = %entryHilited @ %entryInfo;
                name = %friend @ name @ %entryInfo;
                %botsFriendsList.put(name, %entryInfo);
                if (%nameAtTopOfFriendsListisNPC) {
                }
                if ((0.0 < %comparisonToNameAtTopOfList)) {
                    %numberOfNamesInsertedAbove = (1.0 + %numberOfNamesInsertedAbove);
                    %friend;
                }
            }
            %line = %entryDefault.addLineNoReseat();
            BuddyHudFriendsList;
            entryDefault = %entryDefault @ %line;
            entryHilited = %entryHilited @ %line;
            linkText = %friend @ munge(name) @ %line;
            isNPCEntry = 0 @ %line;
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
        %line = entryDefault.addLineNoReseat();
        %entryInfo;
        entryDefault = %entryInfo @ entryDefault @ %line;
        BuddyHudFriendsList;
        entryHilited = %entryInfo @ entryHilited @ %line;
        linkText = %entryInfo @ munge(name) @ %line;
        isNPCEntry = 1 @ %line;
        %entryInfo.delete();
        %i = (1.0 + %i);
    }
    %botsFriendsList.clear();
    %botsFriendsList.delete();
    if ((BuddyHudFriendsList > getCount())) {
        %newVerticalPositionOfList = ((%numberOfNamesInsertedAbove * %heightOfRowInFriendsList) - %offsetForVerticalPositionOfList);
        0.0;
        startingPos = BuddyHudFriendsList @ getWord(startingPos, 0) @ " " @ %newVerticalPositionOfList @ BuddyHudFriendsList;
        (%botFriendsCount < %i);
    }
    if (%timeIt) {
        error(getScopeName() @ " " @ "- D" @ " " @ formatFloat("%8.3f", (1000.0 / (%startTime - getSimTime()))) @ " " @ formatFloat("%8.3f", (1000.0 / (%lastTime - getSimTime()))));
        %lastTime = getSimTime();
    }
    %favoriteCount = size();
    UserListFavorites;
    %i = 0;
    if ((%favoriteCount < %i)) {
        %favorite = %i.getValue();
        UserListFavorites;
        %color = $NameColorNormal;
        %listName = "FavesOffline";
        %ghost = Player::findPlayerInstance(name);
        %favorite;
        if (!(%ghost $= "")) {
            %ghost.setBuddy(1);
        }
        if (!(%listName $= "")) {
            if (name.getIgnoreStatus()) {
                %ignoreTag = "<strikethrough>";
                %favorite;
            }
            %ignoreTag = "";
            BuddyHudWin;
            if (isNPC) {
                %npcOpen = "[ ";
                %favorite;
                %npcClose = " ]";
            }
            %npcOpen = "";
            %npcClose = "";
            %boldTag = "";
            %colorTag = "<linkcolor:" @ ColorIToHex(%color) @ ">";
            %indent = "   ";
            %entry = "<spush>" @ %indent @ %ignoreTag @ %boldTag @ %colorTag @ "<a:gamelink player " @ %favorite @ munge(name) @ ">" @ %npcOpen @ %favorite @ name @ %npcClose @ "</a><spop>";
            %list = buddyLists;
            %listName @ %this;
            if (!(isObject(%list))) {
                log("unknown list" @ " " @ %listName);
            }
            %list.put(name, %entry);
        }
        log(%favorite @ name);
        %i = (1.0 + %i);
        "unsortable fave buddy" @ " ";
    }
    if (%timeIt) {
        error(getScopeName() @ " " @ "- E" @ " " @ formatFloat("%8.3f", (1000.0 / (%startTime - getSimTime()))) @ " " @ formatFloat("%8.3f", (1000.0 / (%lastTime - getSimTime()))));
        %lastTime = getSimTime();
        (%favoriteCount < %i);
    }
    %fanCount = size();
    UserListFans;
    %i = 0;
    error;
    if ((%fanCount < %i)) {
        %fan = %i.getValue();
        UserListFans;
        %listName = "";
        relations;
        %color = "";
        %favorite;
        if (isNPC) {
            log("relations", "warn", %fan @ name);
            %color = $NameColorNormal;
            "an NPC is a fan; weird." @ " ";
            %listName = "FansNPC";
            %fan;
        }
        if ((%fan SPC serverName $= "")) {
            %color = $NameColorOffline;
            error;
            %listName = "FansOffline";
            relations;
        }
        if ((%fan SPC serverName $= $ServerName)) {
            if ((0.0 != %vipRoleMasks)) {
            }
            if ((%vipRoleMasks != (%fan & roles))) {
                %color = $NameColorStaff;
                0.0;
                %listName = "FansHere";
            }
            %color = $NameColorNormal;
            %listName = "FansHere";
        }
        %color = $NameColorElsewhere;
        %listName = "FansThere";
        if (!(%listName $= "")) {
            if (name.getIgnoreStatus()) {
                %ignoreTag = "<strikethrough>";
                %fan;
            }
            %ignoreTag = "";
            BuddyHudWin;
            if (isNPC) {
                %npcOpen = "[ ";
                %fan;
                %npcClose = " ]";
            }
            %npcOpen = "";
            %npcClose = "";
            %boldTag = "";
            %colorTag = "<linkcolor:" @ ColorIToHex(%color) @ ">";
            %indent = "   ";
            %entry = "<spush>" @ %indent @ %ignoreTag @ %boldTag @ %colorTag @ "<a:gamelink player " @ %fan @ munge(name) @ ">" @ %npcOpen @ %fan @ name @ %npcClose @ "</a><spop>";
            %list = buddyLists;
            %listName @ %this;
            if (!(isObject(%list))) {
                log("unknown list" @ " " @ %listName);
            }
            %list.put(name, %entry);
        }
        log(%fan @ name);
        %i = (1.0 + %i);
        "unsortable fan buddy" @ " ";
    }
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    (%fanCount < %i);
    %playerCount = %dict.size();
    error;
    %i = 0;
    relations;
    if ((%playerCount < %i)) {
        %ghost = %dict.getValue(%i);
        %fan;
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
    %bitmap = (UserListFans > size()) ? "platform/client/buttons/pending_active" : "platform/client/buttons/pending";
    0.0;
    %elButton = button;
    "requests".getTabWithName();
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
    startingPos.scrollToPos();
    startingPos.scrollToPos();
    (0.0 == %friendCount).setVisible();
    (0.0 == (%fanCount + %favoriteCount)).setVisible();
    setUhOhTabVisibility();
    reseatChildren();
    if (%timeIt) {
        error(getScopeName() @ " " @ "- H" @ " " @ formatFloat("%8.3f", (1000.0 / (%startTime - getSimTime()))) @ " " @ formatFloat("%8.3f", (1000.0 / (%lastTime - getSimTime()))));
        %lastTime = getSimTime();
        BuddyHudFriendsList;
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
        %list = buddyLists;
        getWord(%lists, %n) @ %this;
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
    if (!(isObject(buddyLists))) {
        ignoreCase = StringMap @ new ""() @ "true";
        0;
        buddyLists = %listName @ %this @ %listName @ %this;
    }
    if (isObject()) {
        buddyLists.add();
    }
    %list = buddyLists;
    MissionCleanup @ %listName @ %this @ %listName @ %this;
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    MissionCleanup;
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
    curListName = %listName @ %this;
    listAdded = 0 @ %listName @ %this;
};
function BuddyHudWin::putListIntoList(%this, %srcList, %destList) {
    %list = buddyLists;
    %srcList @ %this;
    if (!(isObject(%list))) {
        log("unknown list" @ " " @ %srcList);
        return error;
    }
    if ((0.0 > %list.size())) {
    }
    if (!(listAdded)) {
        listAdded = %this @ curListName @ %this @ 1 @ %this @ curListName @ %this;
        %colorTag = "<linkcolor:ffffff>";
        if (%this[%this @ curListName]) {
            %collapsed = "+";
            $UserPref::buddies::collapsedLists;
        }
        %collapsed = "- ";
        %listTitle = %this[%this @ curListName];
        $gBuddyListTitles;
        %titleLine = "<color:ffffff>" @ %colorTag @ "<a:gamelink list " @ %this @ curListName @ ">" @ %collapsed @ %listTitle @ "</a>";
        %destList.setText(%destList.getText() @ %titleLine @ "<br>");
        if ((%this SPC curListName $= "WaitingForYourApproval")) {
        }
        if (!(%this[%this @ curListName])) {
            %formatStr = $UserPref::buddies::collapsedLists @ "<spush><linkcolor:" @ ColorIToHex("255 147 248") @ ">";
            %destList.setText(%destList.getText() @ %formatStr @ "  [<a:gamelink approveall>Approve All</a>]   [<a:gamelink declineall>Decline All</a>]<spop><br>");
        }
    }
    if (!(%this[%this @ curListName])) {
        putIntoList = $UserPref::buddies::collapsedLists @ %destList @ %this;
        %list.forEach("addToFavList");
    }
};
function BuddyHudWin::putListIntoTab(%this, %listName, %destMLTextCtrl) {
    %srcStringMap = buddyLists;
    %listName @ %this;
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
    %destList = putIntoList;
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
    %list = buddyLists;
    %listName @ %this;
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
    %num = size();
    UserListIgnores;
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
    markIgnoredInList();
    markIgnoredInList();
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
    populateBuddyLists();
    if (isShowing()) {
        tryShowPlayerInfo();
    }
    if (0) {
        if (firstTime) {
            firstTime = BuddyHudWin @ 0 @ BuddyHudWin;
            InfoPopupDlg;
            if ((0.0 > %request.getValue("fanCount"))) {
                open();
                "requests".selectTabWithName();
            }
        }
    }
    if ((UserListUnknownServerName > size())) {
        refresh();
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
        %record = new ""();
        ScriptObject;
        parseBuddyRecord(%record, %val);
        %list.put(name, %record);
        if (!(%record SPC serverName $= "")) {
        }
        if ((%record SPC csn $= "")) {
            name.put(%record);
            warn(0 @ %record @ UserListUnknownServerName @ %record @ getScopeName() @ " " @ "- unrecogized server name '" @ %record @ serverName @ "' for " @ %type @ " user '" @ %record @ name @ "'");
        }
        if (%doIgnore) {
            %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
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
    name = getField(%val, %fieldNdx) @ %record;
    %fieldNdx = (1.0 + %fieldNdx);
    serverName = getField(%val, %fieldNdx) @ %record;
    %fieldNdx = (1.0 + %fieldNdx);
    %roles = getField(%val, %fieldNdx);
    %fieldNdx = (1.0 + %fieldNdx);
    if ((%roles $= "")) {
        %roles = 0;
    }
    eval("%record.roles = " @ %roles @ ";");
    loggedIn = getField(%val, %fieldNdx) @ %record;
    %fieldNdx = (1.0 + %fieldNdx);
    isIdle = getField(%val, %fieldNdx) @ %record;
    %fieldNdx = (1.0 + %fieldNdx);
    isNPC = %record @ isNPCName(name) @ %record;
    if (isNPC) {
        isIdle = %record @ 0 @ %record;
    }
    if ((%record SPC serverName $= "")) {
        isIdle = 1 @ %record;
    }
    if ((%record SPC isIdle $= "no")) {
        isIdle = 0 @ %record;
    }
    isIdle = 1 @ %record;
    loggedIn = (%record SPC loggedIn $= "yes") @ %record;
    if (isNPC) {
    }
    if ((%record SPC serverName $= "")) {
        serverName = %record @ $ServerName @ %record;
    }
    csn = %record @ serverName.getCityNameForServerName() @ %record;
    BuddyHudTabs;
    activities = %record @ isIdle ? "idle" : "" @ %record;
};
function BuddyHudTabs::getCityNameForServerName(%this, %ServerName) {
    %csn = %ServerName.cityNameForServerName();
    WorldMap;
    return %csn;
};
function BuddyHudTabs::updateUserListUnknownServerName(%this) {
    safeEnsureScriptObject("StringMap", "UserListUnknownServerName");
    %originalSize = size();
    UserListUnknownServerName;
    %i = (1.0 - %originalSize);
    if ((0.0 >= %i)) {
        %record = %i.getValue();
        UserListUnknownServerName;
        csn = %record @ serverName.getCityNameForServerName() @ %record;
        BuddyHudTabs;
        if (!(%record SPC csn $= "")) {
            %i.getKey().remove();
        }
        csn = UserListUnknownServerName @ "?" @ %record;
        UserListUnknownServerName;
        %i = (1.0 - %i);
    }
    if ((UserListUnknownServerName != size())) {
        populateBuddyLists();
    }
};
function markIgnoredInList(%list) {
    %n = (1.0 - %list.size());
    if ((0.0 >= %n)) {
        %fave = %list.getValue(%n);
        ignored = %fave @ name.getIgnoreStatus() @ %fave;
        BuddyHudWin;
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
    if (isVisible()) {
        refreshFavoritesList();
        $gRefreshEvenIfBuddyHudWinClosed = 0;
        BuddyHudWin;
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
    %name = singleUserName;
    %request;
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
        %record = new ""();
        ScriptObject;
        parseBuddyRecord(%record, %val);
        %list.put(name, %record);
        if (!(%record SPC serverName $= "")) {
        }
        if ((%record SPC csn $= "")) {
            safeEnsureScriptObject("StringMap", "UserListUnknownServerName");
            name.put(%record);
            warn(UserListUnknownServerName @ %record @ getScopeName() @ " " @ "- unrecogized server name '" @ %record @ serverName @ "' for relation with single user '" @ %record @ name @ "'");
            refresh();
        }
    }
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    WorldMap;
    %ghost = %dict.get(name);
    %record;
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
        %record;
        %record = new ""();
        ScriptObject;
        parseBuddyRecord(%record, %val);
        name.put(%record);
        if (isObject(%ghost)) {
            echo(%record @ name);
            %ghost.setIgnore(1);
        }
    }
    markIgnoredInList();
    markIgnoredInList();
    populateBuddyLists();
};
function BuddyHudTabs::setBuddyIdleStatus(%this, %buddyName, %isIdle) {
    %entry = %buddyName.get();
    UserListFriends;
    if (!(isObject(%entry))) {
        return;
    }
    isIdle = %isIdle @ %entry;
    if (%isIdle) {
        if (!(hasField(activities, "idle"))) {
            activities = "idle" @ "\t" @ %entry @ activities @ %entry;
            %entry;
        }
    }
    populateBuddyLists();
};
function BuddyHudTabs::setBuddyActivities(%this, %buddyName, %activitiesList) {
    %entry = %buddyName.get();
    UserListFriends;
    if (!(isObject(%entry))) {
        return;
    }
    activities = %activitiesList @ %entry;
    populateBuddyLists();
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
    if ((BuddyHudTabs SPC %action $= "userToNonIdle")) {
        %source.setBuddyIdleStatus(0);
    }
    if ((BuddyHudTabs SPC %action $= "friendRequestCreated")) {
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
    if ((SystemMessageTextCtrl SPC %action $= "friendsRemoved")) {
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
    if ((SystemMessageTextCtrl SPC %action $= "friendRequestCancelled")) {
        sendBuddyStatusRequest(%other);
        if ((%self $= %source)) {
            handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff>You canceled your friend request to <a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a>.");
        }
        %other.updateFriendRequest(2);
    }
    if ((SystemMessageTextCtrl SPC %action $= "ignoreAdded")) {
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
        populateBuddyListsReally();
    }
    if ((BuddyHudWin SPC getWord(%url, 1) $= "approveall")) {
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
    getCursorPos().showAtPoint();
};
function onSingleClickPlayerName(%name, %objID) {
    if (showPlayerInfoPopup()) {
    }
    if (!(%name $= $player.getShapeName())) {
        open();
        %name.showInfoFor();
        if (0) {
        }
        if (isFunction()) {
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
    clear();
};
function clientCmdItsAnNPC(%npcName) {
    safeEnsureScriptObjectWithInit("StringMap", "NPCList", "{ ignoreCase = true; }");
    %npcName.put(%npcName);
};
function isNPCName(%name) {
    if (!(isObject())) {
        if (isObject()) {
            error(getScopeName() @ " " @ "- NPC list not initialized." @ " " @ %name @ " " @ getTrace());
        }
        return 0;
    }
    %ret = !(NPCList SPC %name.get() $= "");
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
    inviteSelected();
    %selected = getSelectedId();
    AIMBuddyList;
    %row = %selected.getRowNumById();
    AIMBuddyList;
    %row = (1.0 + %row);
    AIMBuddyList;
    if ((rowCount() >= %row)) {
        %row = 0;
        AIMBuddyList;
    }
    %row.setSelectedRow();
};
function Player::onGotBuddyStatus(%this) {
    %this.schedule(0, "updateMapIcon");
};
function Player::onGotFaveStatus(%this) {
    %this.schedule(0, "updateMapIcon");
};
