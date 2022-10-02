if (!isObject(BuddyHudTabs))
{
    new ScriptObject(BuddyHudTabs);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(BuddyHudTabs);
    }
}
BuddyHudTabs.vars["columnPadding"] = 0;
BuddyHudTabs.vars["rowPadding"] = 0;
BuddyHudTabs.vars["offlineFriendsTextboxHeight"] = 22;
BuddyHudTabs.vars["buddyListLegendTextboxHeight"] = 28;
BuddyHudTabs.vars["normalTextColor"] = ColorIToHex($NameColorFriend);
BuddyHudTabs.vars["idleTextColor"] = ColorFToHex(ColorConvolve($NameColorFriendF, $NameColorIdleModulationF));
BuddyHudTabs.vars["hilitedTextColor"] = ColorIToHex($NameColorHilite);
function BuddyHudTabs::setup(%this)
{
    if (!%this.initialized)
    {
        %this.Initialize(BuddyHudTabContainer, "25 25", "platform/client/ui/separator", "16 7", "horizontal");
        %this.newTab("friends", "platform/client/buttons/buddies");
        %this.newTab("requests", "platform/client/buttons/pending");
        %this.newTab("AIM", "platform/client/buttons/aim_buddies");
        %this.newTab("UhOh", "platform/client/buttons/uhoh");
        %this.selectTabWithName("friends");
        %this.fillFriendsTab();
        %this.fillRequestsTab();
        %this.fillAIMTab();
        %this.fillUhOhTab();
        gSetField(BuddyHudWin, favoritesTimer, 0);
        BuddyHudWin.refreshAIMBuddyList();
        new GuiBitmapButtonCtrl()
        {
            position = "115 4";
            extent = "42 21";
            bitmap = "platform/client/buttons/inviteFriends";
            command = "doInviteFriends();";
        }.add(new GuiBitmapButtonCtrl()
        {
            position = "115 4";
            extent = "42 21";
            bitmap = "platform/client/buttons/inviteFriends";
            command = "doInviteFriends();";
        });
        AIMLoginFrame.setup();
    }
    return ;
}
function BuddyHudTabs::OnETSInviteFriends(%this)
{
    if (!isObject(EtsInviteDialog))
    {
        error("no EtsInviteDialog, this should not happen");
        return ;
    }
    if (!EtsInviteDialog.isVisible())
    {
        EtsInviteDialog.open();
    }
    return ;
}
function BuddyHudTabs::fillFriendsTab(%this)
{
    %theTab = %this.getTabWithName("friends");
    %scroll = new GuiScrollCtrl()
    {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) SPC getWord(%theTab.getExtent(), 1) - (BuddyHudTabs.vars["offlineFriendsTextboxHeight"] + BuddyHudTabs.vars["buddyListLegendTextboxHeight"]);
        minExtent = "10 10";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    };
    %friendsList = new GuiArray2Ctrl()
    {
        profile = "CSProfileListBox";
        childrenClassName = "GuiMouseEventCtrl";
        childrenExtent = "16 18";
        spacing = BuddyHudTabs.vars["rowPadding"];
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
        position = 0 - BuddyHudTabs.vars["columnPadding"] SPC 0;
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
    %friendsList.setFieldWidths(%fieldWidths, BuddyHudTabs.vars["columnPadding"]);
    %friendsList.clear();
    %scroll.add(%friendsList);
    %theTab.add(%scroll);
    new GuiMLTextCtrl(BuddyHudFriendsListLegendContainer)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "top";
        position = 4 SPC getWord(%theTab.getExtent(), 1) - BuddyHudTabs.vars["buddyListLegendTextboxHeight"];
        extent = getWord(%theTab.getExtent(), 0) SPC BuddyHudTabs.vars["buddyListLegendTextboxHeight"];
        minExtent = BuddyHudTabs.vars["buddyListLegendTextboxHeight"] SPC BuddyHudTabs.vars["buddyListLegendTextboxHeight"];
    }.add(new GuiMLTextCtrl(BuddyHudFriendsListLegendContainer)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "top";
        position = 4 SPC getWord(%theTab.getExtent(), 1) - BuddyHudTabs.vars["buddyListLegendTextboxHeight"];
        extent = getWord(%theTab.getExtent(), 0) SPC BuddyHudTabs.vars["buddyListLegendTextboxHeight"];
        minExtent = BuddyHudTabs.vars["buddyListLegendTextboxHeight"] SPC BuddyHudTabs.vars["buddyListLegendTextboxHeight"];
    });
    new GuiScrollCtrl(BuddyHudFriendsInfo)
    {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "3 2";
        extent = getWord(%theTab.getExtent(), 0) - 3 SPC getWord(%theTab.getExtent(), 1) - 24;
        minExtent = "10 10";
        sluggishness = -1;
        visible = 0;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    }.add(new GuiScrollCtrl(BuddyHudFriendsInfo)
    {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "3 2";
        extent = getWord(%theTab.getExtent(), 0) - 3 SPC getWord(%theTab.getExtent(), 1) - 24;
        minExtent = "10 10";
        sluggishness = -1;
        visible = 0;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    });
    if (showInviteFriend())
    {
        %inviteButton = new GuiVariableWidthButtonCtrl(ETSInviteButton)
        {
            profile = "BracketButton15Profile";
            horizSizing = "right";
            vertSizing = "top";
            position = 8 SPC getWord(%theTab.getExtent(), 1) - 18;
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
    return ;
}
function BuddyHudTabs::fillRequestsTab(%this)
{
    %theTab = %this.getTabWithName("requests");
    %requestsList = new GuiMLTextCtrl()
    {
        profile = "ETSTextListProfile";
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
    %scroll = new GuiScrollCtrl()
    {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) SPC getWord(%theTab.getExtent(), 1) - 24;
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
    new GuiScrollCtrl(BuddyHudRequestsInfo)
    {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "3 2";
        extent = getWord(%theTab.getExtent(), 0) - 3 SPC getWord(%theTab.getExtent(), 1) - 24;
        minExtent = "10 10";
        sluggishness = -1;
        visible = 0;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    }.add(new GuiScrollCtrl(BuddyHudRequestsInfo)
    {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "3 2";
        extent = getWord(%theTab.getExtent(), 0) - 3 SPC getWord(%theTab.getExtent(), 1) - 24;
        minExtent = "10 10";
        sluggishness = -1;
        visible = 0;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    });
    return ;
}
function BuddyHudTabs::fillAIMTab(%this)
{
    %theTab = %this.getTabWithName("AIM");
    if (!isObject(%theTab))
    {
        echo("Didn\'t find AIM Buddies tab");
        return 0;
    }
    $Player::AIMName = "";
    $Player::AIMPassword = "";
    %loginFrame = new GuiControl(AIMLoginFrame)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "4 0";
        extent = "153 190";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %aimListScroll = new GuiScrollCtrl()
    {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) SPC getWord(%theTab.getExtent(), 1) - 24;
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
    %signOffButton = new GuiVariableWidthButtonCtrl(AIMSignOffButton)
    {
        profile = "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "top";
        position = 93 SPC getWord(%theTab.getExtent(), 1) - 22;
        extent = "54 15";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        command = "doAIMSignOff();";
        text = "Sign Off";
        groupNum = -1;
        buttonType = "PushButton";
    };
    %inviteButton = new GuiVariableWidthButtonCtrl(AIMInviteButton)
    {
        profile = "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "top";
        position = 8 SPC getWord(%theTab.getExtent(), 1) - 22;
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
    return ;
}
function BuddyHudTabs::wakeUp(%this)
{
    %this.setup();
    %this.selectCurrentTab();
    AIMSignInButton.setActive(1);
    return ;
}
function AIMLoginFrame::nextControl(%this, %curControl)
{
    %nextControl = "";
    %this.update();
    if (%curControl.getName() $= AIMScreenNameField)
    {
        %nextControl = AIMPasswordField;
    }
    else
    {
        if (%curControl.getName() $= AIMPasswordField)
        {
            %nextControl = AIMSignInButton;
        }
    }
    if (%nextControl $= "")
    {
        error("nextControl got invalid arg" SPC %curControl);
        return ;
    }
    if (%nextControl $= AIMSignInButton)
    {
        doAIMSignIn();
    }
    else
    {
        %nextControl.makeFirstResponder(1);
        %nextControl.selectAll();
    }
    return ;
}
function BuddyHudTabs::fillUhOhTab(%this)
{
    %theTab = %this.getTabWithName("UhOh");
    if (!isObject(%theTab))
    {
        echo("Didn\'t find UhOh tab");
        return 0;
    }
    %scroll = new GuiScrollCtrl()
    {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "top relative";
        position = "0 0";
        extent = getWord(%theTab.getExtent(), 0) SPC getWord(%theTab.getExtent(), 1) - 24;
        minExtent = "10 10";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        helpTag = 0;
    };
    %body = new GuiMLTextCtrl()
    {
        profile = "ETSTextListProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = getWord(%scroll.getExtent(), 0) - 10 SPC getWord(%scroll.getExtent(), 1) - 10;
        minExtent = "10 10";
        sluggishness = -1;
        visible = 1;
        text = $MsgCat::UI["TOOMANYFRIENDS"];
    };
    %theTab.add(%scroll);
    %scroll.add(%body);
    return ;
}
function BuddyHudTabs::setUhOhTabVisibility(%this)
{
    %showIt = UserListFriends.size() > 250;
    if (%showIt)
    {
        %this.showTabWithName("UhOh");
    }
    else
    {
        %this.hideTabWithName("UhOh");
    }
    return ;
}
function SimGroup::hasObjectWithName(%this, %name)
{
    %i = 0;
    while (%i < %this.getCount())
    {
        if (%this.getObject(%i).name $= %name)
        {
            return 1;
        }
        %i = %i + 1;
    }
    return 0;
}
function BuddyHudWin::wakeUp(%this)
{
    BuddyHudTabs.wakeUp();
    return ;
}
function BuddyHudWin::addBuddy(%this, %buddy)
{
    AIMBuddyList.addRow(0, %buddy);
    return ;
}
function BuddyHudWin::onlineBuddiesToString(%this)
{
    %count = aimBuddyCount();
    %onlineBuddies = "";
    %n = 0;
    while (%n < %count)
    {
        %state = aimGetBuddyState(%n);
        if ((%state < 1) && (%state > 3))
        {
            continue;
        }
        if (!(%onlineBuddies $= ""))
        {
            %onlineBuddies = %onlineBuddies TAB aimGetBuddyName(%n);
        }
        else
        {
            %onlineBuddies = aimGetBuddyName(%n);
        }
        %n = %n + 1;
    }
    return %onlineBuddies;
}
function BuddyMap::addToAIMList(%this, %key, %value)
{
    AIMBuddyList.addRow(AIMBuddyList.rowCount(), %value);
    return ;
}
function BuddyHudWin::refreshAIMBuddyList(%this)
{
    if (isObject(AIMBuddyList))
    {
        %onlineList = new StringMap()
        {
            class = "BuddyMap";
            ignoreCase = 1;
        };
        %idleAwayList = new StringMap()
        {
            class = "BuddyMap";
            ignoreCase = 1;
        };
        %offlineList = new StringMap()
        {
            class = "BuddyMap";
            ignoreCase = 1;
        };
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(%onlineList);
            MissionCleanup.add(%idleAwayList);
            MissionCleanup.add(%offlineList);
        }
        AIMBuddyList.clear();
        %buddyCount = aimBuddyCount();
        %i = 0;
        while (%i < %buddyCount)
        {
            %buddyName = aimGetBuddyName(%i);
            %buddyState = aimGetBuddyState(%i);
            if (%buddyState == -1)
            {
                %tag = "\x10\c5";
                %offlineList.put(%buddyName, %tag @ %buddyName @ "\x11");
            }
            else
            {
                if (%buddyState == 0)
                {
                    %tag = "\x10\c5";
                    %offlineList.put(%buddyName, %tag @ %buddyName @ "\x11");
                }
                else
                {
                    if (%buddyState == 1)
                    {
                        %tag = "\x10\c6";
                        %onlineList.put(%buddyName, %tag @ %buddyName @ "\x11");
                    }
                    else
                    {
                        if (%buddyState == 2)
                        {
                            %tag = "\x10\c7";
                            %idleAwayList.put(%buddyName, %tag @ %buddyName @ "\x11");
                        }
                        else
                        {
                            if (%buddyState == 3)
                            {
                                %tag = "\x10\c8";
                                %idleAwayList.put(%buddyName, %tag @ %buddyName @ "\x11");
                            }
                            else
                            {
                                %offlineList.put(%buddyName, %buddyName);
                            }
                        }
                    }
                }
            }
            %i = %i + 1;
        }
        %onlineList.forEach("addToAIMList");
        %idleAwayList.forEach("addToAIMList");
        %offlineList.forEach("addToAIMList");
        %onlineList.delete();
        %idleAwayList.delete();
        %offlineList.delete();
    }
    return ;
}
function BuddyHudWin::clearSelections(%this)
{
    if (isObject(AIMBuddyList))
    {
        AIMBuddyList.setSelectedRow(-1);
    }
    return ;
}
BuddyHudWin.STATE_PARSE_RESULT = 1;
BuddyHudWin.STATE_PARSE_BUDDIES = 2;
function BuddyHudFriendsList::onCreatedChild(%this, %child)
{
    Parent::onCreatedChild(%this, %child);
    %position = %child.getPosition();
    %extent = %child.getExtent();
    %newHeight = getWord(%extent, 1) + %this.spacing;
    %child.resize(getWord(%position, 0), getWord(%position, 1), getWord(%extent, 0), %newHeight);
    if (!(getWord(%child.getNamespaceList(), 0) $= "BuddyHudFriendsListLine"))
    {
        %child.bindClassName("BuddyHudFriendsListLine");
    }
    return ;
}
function BuddyHudFriendsListLine::updateText(%this, %newText)
{
    %fieldCount = getFieldCount(%newText);
    %objectCount = %this.getCount();
    if (%objectCount < %fieldCount)
    {
        %fieldCount = %objectCount;
    }
    %i = 0;
    while (%i < %fieldCount)
    {
        %this.getObject(%i).setText(getField(%newText, %i));
        %i = %i + 1;
    }
}

function BuddyHudFriendsListLine::onMouseEnter(%this)
{
    %this.updateText(%this.entryHilited);
    return ;
}
function BuddyHudFriendsListLine::onMouseLeave(%this)
{
    %this.updateText(%this.entryDefault);
    return ;
}
function BuddyHudFriendsListLine::onMouseDown(%this)
{
    return ;
}
function BuddyHudFriendsListLine::onMouseUp(%this)
{
    %name = unmunge(%this.linkText);
    onLeftClickPlayerName(%name, "");
    return ;
}
function BuddyHudFriendsListLine::onRightMouseUp(%this)
{
    %name = unmunge(%this.linkText);
    onRightClickPlayerName(%name);
    return ;
}
$gBuddyListTitles["FriendsOnline"] = "";
$gBuddyListTitles["WaitingForYourApproval"] = "Waiting For Your Approval";
$gBuddyListTitles["YourPendingRequests"] = "Requests by You";
$gBuddyListTitles["InTransit"] = "In Transit";
$gBuddyListLastPopulateTime = "";
$gBuddyListPopulateTimer = "";
$gBuddyListMinRepopulatePeriodMS = 4000;
function BuddyHudWin::populateBuddyLists(%this)
{
    %doit = 0;
    if ($gBuddyListLastPopulateTime $= "")
    {
        %doit = 1;
    }
    else
    {
        %timeSinceLastPopulate = mSubS32(getSimTime(), $gBuddyListLastPopulateTime);
        if (%timeSinceLastPopulate > $gBuddyListMinRepopulatePeriodMS)
        {
            %doit = 1;
        }
        else
        {
            if (!($gBuddyListPopulateTimer $= ""))
            {
                cancel($gBuddyListPopulateTimer);
            }
            $gBuddyListPopulateTimer = %this.schedule($gBuddyListMinRepopulatePeriodMS, "populateBuddyLists");
        }
    }
    if (%doit)
    {
        if (!($gBuddyListPopulateTimer $= ""))
        {
            cancel($gBuddyListPopulateTimer);
        }
        $gBuddyListPopulateTimer = "";
        %this.populateBuddyListsReally();
        $gBuddyListLastPopulateTime = getSimTime();
    }
    return ;
}
function BuddyHudWin::populateBuddyListsReally(%this)
{
    %timeIt = !1;
    if (%timeIt)
    {
        %startTime = getSimTime();
        %lastTime = getSimTime();
    }
    if (!isObject(BuddyHudFriendsList) && !isObject(BuddyHudRequestsList))
    {
        return ;
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
    if (%timeIt)
    {
        error(getScopeName() SPC "- A" SPC formatFloat("%8.3f", (getSimTime() - %startTime) / 1000) SPC formatFloat("%8.3f", (getSimTime() - %lastTime) / 1000));
        %lastTime = getSimTime();
    }
    %heightOfRowInFriendsList = 0;
    %offsetForVerticalPositionOfList = 0;
    if (BuddyHudFriendsList.getCount() > 0)
    {
        %verticalPositionOfList = mMax(0 - getWord(BuddyHudFriendsList.getPosition(), 1), 0);
        %heightOfRowInFriendsList = getWord(BuddyHudFriendsList.getObject(0).getExtent(), 1);
        %indexOfNameAtTopOfFriendsList = mCeil(%verticalPositionOfList / %heightOfRowInFriendsList);
        %offsetForVerticalPositionOfList = (%indexOfNameAtTopOfFriendsList * %heightOfRowInFriendsList) - %verticalPositionOfList;
        %nameAtTopOfFriendsList = StripMLControlChars(BuddyHudFriendsList.getObject(%indexOfNameAtTopOfFriendsList).getObject(1).getText());
        %nameAtTopOfFriendsListisNPC = BuddyHudFriendsList.getObject(%indexOfNameAtTopOfFriendsList).isNPCEntry;
    }
    else
    {
        %nameAtTopOfFriendsList = "";
        %nameAtTopOfFriendsListisNPC = 0;
    }
    %numberOfNamesInsertedAbove = 0;
    BuddyHudFriendsList.startingPos = BuddyHudFriendsList.getPosition();
    BuddyHudFriendsList.deleteMembers();
    %botsFriendsList = new StringMap();
    %botsFriendsList.clear();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%botsFriendsList);
    }
    BuddyHudRequestsList.startingPos = BuddyHudRequestsList.getPosition();
    BuddyHudRequestsList.setText("");
    if (%timeIt)
    {
        error(getScopeName() SPC "- B" SPC formatFloat("%8.3f", (getSimTime() - %startTime) / 1000) SPC formatFloat("%8.3f", (getSimTime() - %lastTime) / 1000));
        %lastTime = getSimTime();
    }
    %friendCount = UserListFriends.size();
    if (0)
    {
        %includeInListCount = 0;
        %i = 0;
        while (%i < %friendCount)
        {
            %friend = UserListFriends.getValue(%i);
            if (!((%friend.serverName $= "")) && %friend.loggedIn)
            {
                %includeInListCount = %includeInListCount + 1;
            }
            %i = %i + 1;
        }
        BuddyHudFriendsList.setNumChildren(%includeInListCount);
    }
    if (%timeIt)
    {
        error(getScopeName() SPC "- X" SPC formatFloat("%8.3f", (getSimTime() - %startTime) / 1000) SPC formatFloat("%8.3f", (getSimTime() - %lastTime) / 1000));
        %lastTime = getSimTime();
    }
    %friendCountOnline = 0;
    %i = 0;
    while (%i < %friendCount)
    {
        %friend = UserListFriends.getValue(%i);
        %includeInList = 1;
        %cityTagDefault = "";
        %cityTagHilited = "";
        %sameServerTagDefault = "";
        %sameServerTagHilited = "";
        %ghost = Player::findPlayerInstance(%friend.name);
        if (!((%ghost $= "")) && isObject(%ghost))
        {
            %ghost.setBuddy(1);
            %ghost.setAmFave(1);
        }
        if (%friend.isNPC)
        {
            %listName = "FrndsNPC";
        }
        else
        {
            if (%friend.serverName $= "")
            {
                if (%friend.loggedIn)
                {
                    %listName = "FrndsInTransit";
                    %cityTagDefault = "<bitmap:platform/client/ui/friendsHud_transition_n>";
                    %cityTagHilited = "<bitmap:platform/client/ui/friendsHud_transition_h>";
                }
                else
                {
                    %listName = "FrndsOffline";
                    %includeInList = 0;
                }
            }
            else
            {
                if (%friend.serverName $= $ServerName)
                {
                    %listName = "FrndsHere";
                }
                else
                {
                    %listName = "FrndsThere";
                }
            }
        }
        %list = %this.buddyLists[%listName];
        if (!isObject(%list))
        {
            log(relations, error, "list not defined:" SPC %listName);
        }
        else
        {
            %list.put(%friend.name, "placeholder");
        }
        if (%includeInList)
        {
            if (%friend.serverName $= $ServerName)
            {
                if (%friend.isIdle)
                {
                    %sameServerTagDefault = "<modulationColor:ffffffa0><bitmap:platform/client/ui/friendsHud_lightning_i>";
                }
                else
                {
                    %sameServerTagDefault = "<modulationColor:ffffffa0><bitmap:platform/client/ui/friendsHud_lightning_n>";
                }
                %sameServerTagHilited = "<modulationColor:ffffffa0><bitmap:platform/client/ui/friendsHud_lightning_h>";
            }
            else
            {
                %sameServerTagDefault = "";
                %sameServerTagHilited = "";
            }
            if (BuddyHudWin.getIgnoreStatus(%friend.name))
            {
                %ignoreTag = "<strikethrough>";
            }
            else
            {
                %ignoreTag = "";
            }
            if (%friend.isNPC)
            {
                %npcOpen = "";
                %npcClose = " (bot)";
            }
            else
            {
                %npcOpen = "";
                %npcClose = "";
            }
            if (%cityTagDefault $= "")
            {
                %cityBitmap = DestinationList::GetAreaNameIconPath(%friend.csn);
                %cityTagDefault = "<modulationColor:ffffffa0><bitmap:" @ %cityBitmap @ %friend.isIdle ? "_i" : "_n" @ ">";
                %cityTagHilited = "<modulationColor:ffffffc0><bitmap:" @ %cityBitmap @ "_h>";
            }
            %boldTag = "";
            %colorTagDefault = "<color:" @ %friend.isIdle ? "idleTextColor" : "normalTextColor" @ ">";
            %colorTagHilited = "<color:" @ BuddyHudTabs.vars["hilitedTextColor"] @ ">";
            %visibleName = %npcOpen @ %friend.name @ %npcClose;
            %friendNameTagDefault = "<clip:100>" @ %ignoreTag @ %boldTag @ %colorTagDefault @ %visibleName @ "</a></clip>";
            %friendNameTagHilited = "<clip:100>" @ %ignoreTag @ %boldTag @ %colorTagHilited @ %visibleName @ "</a></clip>";
            %activityName = %friend.activities $= "" ? "" : getField(%friend.activities, 0);
            %activityBitmapMLText = getUserActivityMgr().getActivityBitmapMLText(%activityName);
            %activityColorDefault = %friend.isIdle ? "ffffff70" : "ffffff80";
            %activityColorHilited = BuddyHudTabs.vars["hilitedTextColor"];
            %activityDefault = "<modulationColor:" @ %activityColorDefault @ ">" @ %activityBitmapMLText;
            %activityHilited = "<modulationColor:" @ %activityColorHilited @ ">" @ %activityBitmapMLText;
            %entryDefault = %sameServerTagDefault TAB %friendNameTagDefault TAB %cityTagDefault @ %activityDefault;
            %entryHilited = %sameServerTagHilited TAB %friendNameTagHilited TAB %cityTagHilited @ %activityHilited;
            %comparisonToNameAtTopOfList = stricmp(%visibleName, %nameAtTopOfFriendsList);
            if (%friend.isNPC)
            {
                %entryInfo = new SimObject();
                %entryInfo.entryDefault = %entryDefault;
                %entryInfo.entryHilited = %entryHilited;
                %entryInfo.name = %friend.name;
                %botsFriendsList.put(%friend.name, %entryInfo);
                if (%nameAtTopOfFriendsListisNPC && (%comparisonToNameAtTopOfList < 0))
                {
                    %numberOfNamesInsertedAbove = %numberOfNamesInsertedAbove + 1;
                }
            }
            else
            {
                %line = BuddyHudFriendsList.addLineNoReseat(%entryDefault);
                %line.entryDefault = %entryDefault;
                %line.entryHilited = %entryHilited;
                %line.linkText = munge(%friend.name);
                %line.isNPCEntry = 0;
                if (%nameAtTopOfFriendsListisNPC && (%comparisonToNameAtTopOfList < 0))
                {
                    %numberOfNamesInsertedAbove = %numberOfNamesInsertedAbove + 1;
                }
            }
            %friendCountOnline = %friendCountOnline + 1;
        }
        %i = %i + 1;
    }
    if (%timeIt)
    {
        error(getScopeName() SPC "- C" SPC formatFloat("%8.3f", (getSimTime() - %startTime) / 1000) SPC formatFloat("%8.3f", (getSimTime() - %lastTime) / 1000));
        %lastTime = getSimTime();
    }
    %botFriendsCount = %botsFriendsList.size();
    %i = 0;
    while (%i < %botFriendsCount)
    {
        %entryInfo = %botsFriendsList.getValue(%i);
        %line = BuddyHudFriendsList.addLineNoReseat(%entryInfo.entryDefault);
        %line.entryDefault = %entryInfo.entryDefault;
        %line.entryHilited = %entryInfo.entryHilited;
        %line.linkText = munge(%entryInfo.name);
        %line.isNPCEntry = 1;
        %entryInfo.delete();
        %i = %i + 1;
    }
    %botsFriendsList.clear();
    %botsFriendsList.delete();
    if (BuddyHudFriendsList.getCount() > 0)
    {
        %newVerticalPositionOfList = %offsetForVerticalPositionOfList - (%heightOfRowInFriendsList * %numberOfNamesInsertedAbove);
        BuddyHudFriendsList.startingPos = getWord(BuddyHudFriendsList.startingPos, 0) SPC %newVerticalPositionOfList;
    }
    if (%timeIt)
    {
        error(getScopeName() SPC "- D" SPC formatFloat("%8.3f", (getSimTime() - %startTime) / 1000) SPC formatFloat("%8.3f", (getSimTime() - %lastTime) / 1000));
        %lastTime = getSimTime();
    }
    %favoriteCount = UserListFavorites.size();
    %i = 0;
    while (%i < %favoriteCount)
    {
        %favorite = UserListFavorites.getValue(%i);
        %color = $NameColorNormal;
        %listName = "FavesOffline";
        %ghost = Player::findPlayerInstance(%favorite.name);
        if (!(%ghost $= ""))
        {
            %ghost.setBuddy(1);
        }
        if (!(%listName $= ""))
        {
            if (BuddyHudWin.getIgnoreStatus(%favorite.name))
            {
                %ignoreTag = "<strikethrough>";
            }
            else
            {
                %ignoreTag = "";
            }
            if (%favorite.isNPC)
            {
                %npcOpen = "[ ";
                %npcClose = " ]";
            }
            else
            {
                %npcOpen = "";
                %npcClose = "";
            }
            %boldTag = "";
            %colorTag = "<linkcolor:" @ ColorIToHex(%color) @ ">";
            %indent = "   ";
            %entry = "<spush>" @ %indent @ %ignoreTag @ %boldTag @ %colorTag @ "<a:gamelink player " @ munge(%favorite.name) @ ">" @ %npcOpen @ %favorite.name @ %npcClose @ "</a><spop>";
            %list = %this.buddyLists[%listName];
            if (!isObject(%list))
            {
                log(relations, error, "unknown list" SPC %listName);
            }
            else
            {
                %list.put(%favorite.name, %entry);
            }
        }
        else
        {
            log(relations, error, "unsortable fave buddy" SPC %favorite.name);
        }
        %i = %i + 1;
    }
    if (%timeIt)
    {
        error(getScopeName() SPC "- E" SPC formatFloat("%8.3f", (getSimTime() - %startTime) / 1000) SPC formatFloat("%8.3f", (getSimTime() - %lastTime) / 1000));
        %lastTime = getSimTime();
    }
    %fanCount = UserListFans.size();
    %i = 0;
    while (%i < %fanCount)
    {
        %fan = UserListFans.getValue(%i);
        %listName = "";
        %color = "";
        if (%fan.isNPC)
        {
            log("relations", "warn", "an NPC is a fan; weird." SPC %fan.name);
            %color = $NameColorNormal;
            %listName = "FansNPC";
        }
        else
        {
            if (%fan.serverName $= "")
            {
                %color = $NameColorOffline;
                %listName = "FansOffline";
            }
            else
            {
                if (%fan.serverName $= $ServerName)
                {
                    if ((%vipRoleMasks != 0) && ((%fan.roles & %vipRoleMasks) != 0))
                    {
                        %color = $NameColorStaff;
                        %listName = "FansHere";
                    }
                    else
                    {
                        %color = $NameColorNormal;
                        %listName = "FansHere";
                    }
                }
                else
                {
                    %color = $NameColorElsewhere;
                    %listName = "FansThere";
                }
            }
        }
        if (!(%listName $= ""))
        {
            if (BuddyHudWin.getIgnoreStatus(%fan.name))
            {
                %ignoreTag = "<strikethrough>";
            }
            else
            {
                %ignoreTag = "";
            }
            if (%fan.isNPC)
            {
                %npcOpen = "[ ";
                %npcClose = " ]";
            }
            else
            {
                %npcOpen = "";
                %npcClose = "";
            }
            %boldTag = "";
            %colorTag = "<linkcolor:" @ ColorIToHex(%color) @ ">";
            %indent = "   ";
            %entry = "<spush>" @ %indent @ %ignoreTag @ %boldTag @ %colorTag @ "<a:gamelink player " @ munge(%fan.name) @ ">" @ %npcOpen @ %fan.name @ %npcClose @ "</a><spop>";
            %list = %this.buddyLists[%listName];
            if (!isObject(%list))
            {
                log(relations, error, "unknown list" SPC %listName);
            }
            else
            {
                %list.put(%fan.name, %entry);
            }
        }
        else
        {
            log(relations, error, "unsortable fan buddy" SPC %fan.name);
        }
        %i = %i + 1;
    }
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    %playerCount = %dict.size();
    %i = 0;
    while (%i < %playerCount)
    {
        %ghost = %dict.getValue(%i);
        if (isObject(%ghost))
        {
            %ghost.setIgnore(BuddyHudWin.getIgnoreStatus(%ghost.getShapeName()));
        }
        %i = %i + 1;
    }
    rentabotClient_reignore();
    if (%timeIt)
    {
        error(getScopeName() SPC "- F" SPC formatFloat("%8.3f", (getSimTime() - %startTime) / 1000) SPC formatFloat("%8.3f", (getSimTime() - %lastTime) / 1000));
        %lastTime = getSimTime();
    }
    %bitmap = UserListFans.size() > 0 ? "platform/client/buttons/pending_active" : "platform/client/buttons/pending";
    %elButton = BuddyHudTabs.getTabWithName("requests").button;
    %elButton.setBitmap(%bitmap);
    if (%friendCount > 0)
    {
        BuddyHudFriendsListOfflineFriendsBox.setText("You have" SPC %friendCount - %friendCountOnline SPC "friends offline");
    }
    else
    {
        BuddyHudFriendsListOfflineFriendsBox.setText("");
    }
    %this.setCurListName("WaitingForYourApproval");
    %this.putListIntoList("FansHere", BuddyHudRequestsList);
    %this.putListIntoList("FansThere", BuddyHudRequestsList);
    %this.putListIntoList("FansOffline", BuddyHudRequestsList);
    %this.setCurListName("YourPendingRequests");
    %this.putListIntoList("FavesHere", BuddyHudRequestsList);
    %this.putListIntoList("FavesThere", BuddyHudRequestsList);
    %this.putListIntoList("FavesOffline", BuddyHudRequestsList);
    if (%timeIt)
    {
        error(getScopeName() SPC "- G" SPC formatFloat("%8.3f", (getSimTime() - %startTime) / 1000) SPC formatFloat("%8.3f", (getSimTime() - %lastTime) / 1000));
        %lastTime = getSimTime();
    }
    BuddyHudFriendsList.scrollToPos(BuddyHudFriendsList.startingPos);
    BuddyHudRequestsList.scrollToPos(BuddyHudRequestsList.startingPos);
    BuddyHudFriendsInfo.setVisible(%friendCount == 0);
    BuddyHudRequestsInfo.setVisible((%favoriteCount + %fanCount) == 0);
    BuddyHudTabs.setUhOhTabVisibility();
    BuddyHudFriendsList.reseatChildren();
    if (%timeIt)
    {
        error(getScopeName() SPC "- H" SPC formatFloat("%8.3f", (getSimTime() - %startTime) / 1000) SPC formatFloat("%8.3f", (getSimTime() - %lastTime) / 1000));
        %lastTime = getSimTime();
    }
    return ;
}
function BuddyHudWin::getNamesPendingMyApproval(%this)
{
    return %this.getNamesInLists("FansHere FansNPC FansThere FansOffline");
}
function BuddyHudWin::getNamesPendingTheirApproval(%this)
{
    return %this.getNamesInLists("FavesHere FavesNPC FavesThere FavesOffline");
}
function BuddyHudWin::getNamesInLists(%this, %lists)
{
    %ret = "";
    %delim = "";
    %n = getWordCount(%lists) - 1;
    while (%n >= 0)
    {
        %list = %this.buddyLists[getWord(%lists, %n)];
        if (isObject(%list))
        {
            %m = %list.size() - 1;
            while (%m >= 0)
            {
                %ret = %ret @ %delim @ %list.getKey(%m);
                %delim = "\t";
                %m = %m - 1;
            }
        }
        %n = %n - 1;
    }
    return %ret;
}
function BuddyHudWin::initializeBuddyList(%this, %listName)
{
    if (!isObject(%this.buddyLists[%listName]))
    {
        %this.buddyLists[%listName] = new StringMap();
    }
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%this.buddyLists[%listName]);
    }
    %list = %this.buddyLists[%listName];
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    %count = %list.size();
    %idx = 0;
    while (%idx < %count)
    {
        %ghost = %dict.get(%list.getKey(%idx));
        if (!((%ghost $= "")) && isObject(%ghost))
        {
            %ghost.setBuddy(0);
            %ghost.setAmFave(0);
            %ghost.setIgnore(0);
        }
        %idx = %idx + 1;
    }
    %list.clear();
    return ;
}
function BuddyHudWin::setCurListName(%this, %listName)
{
    %this.curListName = %listName;
    %this.listAdded[%listName] = 0;
    return ;
}
function BuddyHudWin::putListIntoList(%this, %srcList, %destList)
{
    %list = %this.buddyLists[%srcList];
    if (!isObject(%list))
    {
        log(relations, error, "unknown list" SPC %srcList);
        return ;
    }
    if ((%list.size() > 0) && !(%this.listAdded[%this.curListName]))
    {
        %this.listAdded[%this.curListName] = 1;
        %colorTag = "<linkcolor:ffffff>";
        if ($UserPref::buddies::collapsedLists[%this.curListName])
        {
            %collapsed = "+";
        }
        else
        {
            %collapsed = "- ";
        }
        %listTitle = $gBuddyListTitles[%this.curListName];
        %titleLine = "<color:ffffff>" @ %colorTag @ "<a:gamelink list " @ %this.curListName @ ">" @ %collapsed @ %listTitle @ "</a>";
        %destList.setText(%destList.getText() @ %titleLine @ "<br>");
        if ((%this.curListName $= "WaitingForYourApproval") && !$UserPref::buddies::collapsedLists[%this.curListName])
        {
            %formatStr = "<spush><linkcolor:" @ ColorIToHex("255 147 248") @ ">";
            %destList.setText(%destList.getText() @ %formatStr @ "  [<a:gamelink approveall>Approve All</a>]   [<a:gamelink declineall>Decline All</a>]<spop><br>");
        }
    }
    if (!$UserPref::buddies::collapsedLists[%this.curListName])
    {
        %this.putIntoList = %destList;
        %list.forEach("addToFavList");
    }
    return ;
}
function BuddyHudWin::putListIntoTab(%this, %listName, %destMLTextCtrl)
{
    %srcStringMap = %this.buddyLists[%listName];
    if (!isObject(%srcStringMap))
    {
        log(relations, error, "unknown list" SPC %listName);
        return ;
    }
    if (!isObject(%destMLTextCtrl))
    {
        log(relations, error, "unknown object" SPC %destMLTextCtrl);
    }
    %destMLTextCtrl.setText("");
    %outputText = "";
    %size = %srcStringMap.size();
    if (%size == 0)
    {
        %outputText = "";
    }
    else
    {
        %outputText = %srcStringMap.getValue(0);
        %i = 1;
        while (%i < %size)
        {
            %outputText = %outputText NL %srcStringMap.getValue(%i);
            %i = %i + 1;
        }
    }
    %destMLTextCtrl.setText(%outputText);
    return ;
}
function StringMap::addToFavList(%this, %key, %value)
{
    %destList = BuddyHudWin.putIntoList;
    %destList.setText(%destList.getText() @ %value @ "<br>");
    return ;
}
function BuddyHudWin::isFriendOrFavOnlineElsewhere(%this, %playerName)
{
    if (%this.isInBuddyList(%playerName, "FrndsThere"))
    {
        return 1;
    }
    if (%this.isInBuddyList(%playerName, "FavesThere"))
    {
        return 1;
    }
    return 0;
}
function BuddyHudWin::isFriendOrFavOnlineHere(%this, %playerName)
{
    if (%this.isInBuddyList(%playerName, "FrndsHere"))
    {
        return 1;
    }
    if (%this.isInBuddyList(%playerName, "FavesHere"))
    {
        return 1;
    }
    return 0;
}
function BuddyHudWin::isFriendOrFavOffline(%this, %playerName)
{
    if (%this.isInBuddyList(%playerName, "FrndsOffline"))
    {
        return 1;
    }
    if (%this.isInBuddyList(%playerName, "FavesOffline"))
    {
        return 1;
    }
    return 0;
}
function BuddyHudWin::isOnlineHereOrNotFavorite(%this, %playerName)
{
    if (%this.isFriendOrFavOnlineHere(%this, %playerName))
    {
        return 1;
    }
    if (%this.isInBuddyList(%playerName, "FrndsNPC"))
    {
        return 1;
    }
    if (%this.isInBuddyList(%playerName, "FavesNPC"))
    {
        return 1;
    }
    if (%this.isFriendOrFavOnlineElsewhere(%this, %playerName))
    {
        return 0;
    }
    if (%this.isFriendOrFavOffline(%this, %playerName))
    {
        return 0;
    }
    return 1;
}
function BuddyHudWin::isInBuddyList(%this, %playerName, %listName)
{
    %list = %this.buddyLists[%listName];
    if (!isObject(%list))
    {
        if (!$StandAlone)
        {
            log(relations, error, "isInBuddyList(): unknown list" SPC %listName);
        }
        return ;
    }
    return !(%list.get(%playerName) $= "");
}
function BuddyHudWin::getFriendStatus(%this, %playerName)
{
    safeEnsureScriptObjectWithInit("StringMap", "UserListFriends", "{ ignoreCase = true; }");
    safeEnsureScriptObjectWithInit("StringMap", "UserListFavorites", "{ ignoreCase = true; }");
    safeEnsureScriptObjectWithInit("StringMap", "UserListFans", "{ ignoreCase = true; }");
    if (UserListFriends.hasKey(%playerName))
    {
        return "friends";
    }
    else
    {
        if (UserListFavorites.hasKey(%playerName))
        {
            return "favorite";
        }
        else
        {
            if (UserListFans.hasKey(%playerName))
            {
                return "fan";
            }
            else
            {
                return "none";
            }
        }
    }
    return ;
}
function BuddyHudWin::getIgnoreStatus(%this, %playerName)
{
    safeEnsureScriptObjectWithInit("StringMap", "UserListIgnores", "{ ignoreCase = true; }");
    if (UserListIgnores.hasKey(%playerName))
    {
        return 1;
    }
    else
    {
        return 0;
    }
    return ;
}
function sendBuddyListRequest(%callback)
{
    log("relations", "debug", getTrace());
    if ($Token $= "")
    {
        log("general", "debug", getScopeName() SPC "- no token. skipping request.");
        return ;
    }
    %request = sendRequest_GetUserRelations($Player::Name, "", %callback);
    return ;
}
function onDoneOrErrorCallback_GetUserRelations_ProcessOnly(%request)
{
    if (!isObject(%request))
    {
        return ;
    }
    if (!%request.checkSuccess())
    {
        return ;
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
    while (%n < %num)
    {
        %ghost = %dict.get(UserListIgnores.getKey(%n));
        if (isObject(%ghost))
        {
            %ghost.setIgnore(0);
        }
        %n = %n + 1;
    }
    extractBuddyRecords(%request, UserListFriends, "friend", 0);
    extractBuddyRecords(%request, UserListFavorites, "favorite", 0);
    extractBuddyRecords(%request, UserListFans, "fan", 0);
    extractBuddyRecords(%request, UserListIgnores, "ignore", 1);
    markIgnoredInList(UserListFriends);
    markIgnoredInList(UserListFavorites);
    rentabotClient_reignore();
    return ;
}
function BuddyHudWin::refreshFavoritesList(%this)
{
    sendBuddyListRequest("onDoneOrErrorCallback_GetUserRelations_Full");
    return ;
}
function onDoneOrErrorCallback_GetUserRelations_Full(%request)
{
    log("network", "debug", getScopeName() SPC "- url =" SPC %request.getURL());
    if (!%request.checkSuccess())
    {
        return ;
    }
    onDoneOrErrorCallback_GetUserRelations_ProcessOnly(%request);
    BuddyHudWin.populateBuddyLists();
    if (InfoPopupDlg.isShowing())
    {
        InfoPopupDlg.tryShowPlayerInfo();
    }
    if (0)
    {
        if (BuddyHudWin.firstTime)
        {
            BuddyHudWin.firstTime = 0;
            if (%request.getValue("fanCount") > 0)
            {
                BuddyHudWin.open();
                BuddyHudTabs.selectTabWithName("requests");
            }
        }
    }
    if (UserListUnknownServerName.size() > 0)
    {
        WorldMap.refresh();
    }
    return ;
}
function extractBuddyRecords(%request, %list, %type, %doIgnore)
{
    %list.deleteValuesAsObjects();
    %num = %request.getValue(%type @ "Count");
    log("network", "debug", getScopeName() SPC "- " @ %type @ "Count =" SPC %num);
    %n = 0;
    while (%n < %num)
    {
        %key = %type @ %n;
        %val = %request.getValue(%key);
        log("network", "debug", getScopeName() SPC " -" SPC %key SPC "=" SPC %val);
        %record = new ScriptObject();
        parseBuddyRecord(%record, %val);
        %list.put(%record.name, %record);
        if (!((%record.serverName $= "")) && (%record.csn $= ""))
        {
            UserListUnknownServerName.put(%record.name, %record);
            warn(getScopeName() SPC "- unrecogized server name \'" @ %record.serverName @ "\' for " @ %type @ " user \'" @ %record.name @ "\'");
        }
        if (%doIgnore)
        {
            %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
            %ghost = %dict.get(%list.getKey(%n));
            if (isObject(%ghost))
            {
                %ghost.setIgnore(1);
            }
        }
        %n = %n + 1;
    }
}

function parseBuddyRecord(%record, %val)
{
    %fieldNdx = 0;
    %record.name = getField(%val, %fieldNdx);
    %fieldNdx = %fieldNdx + 1;
    %record.serverName = getField(%val, %fieldNdx);
    %fieldNdx = %fieldNdx + 1;
    %roles = getField(%val, %fieldNdx);
    %fieldNdx = %fieldNdx + 1;
    if (%roles $= "")
    {
        %roles = 0;
    }
    eval("%record.roles = " @ %roles @ ";");
    %record.loggedIn = getField(%val, %fieldNdx);
    %fieldNdx = %fieldNdx + 1;
    %record.isIdle = getField(%val, %fieldNdx);
    %fieldNdx = %fieldNdx + 1;
    %record.isNPC = isNPCName(%record.name);
    if (%record.isNPC)
    {
        %record.isIdle = 0;
    }
    else
    {
        if (%record.serverName $= "")
        {
            %record.isIdle = 1;
        }
        else
        {
            if (%record.isIdle $= "no")
            {
                %record.isIdle = 0;
            }
            else
            {
                %record.isIdle = 1;
            }
        }
    }
    %record.loggedIn = %record.loggedIn $= "yes";
    if (%record.isNPC && (%record.serverName $= ""))
    {
        %record.serverName = $ServerName;
    }
    %record.csn = BuddyHudTabs.getCityNameForServerName(%record.serverName);
    %record.activities = %record.isIdle ? "idle" : "";
    return ;
}
function BuddyHudTabs::getCityNameForServerName(%this, %ServerName)
{
    %csn = WorldMap.cityNameForServerName(%ServerName);
    return %csn;
}
function BuddyHudTabs::updateUserListUnknownServerName(%this)
{
    safeEnsureScriptObject("StringMap", "UserListUnknownServerName");
    %originalSize = UserListUnknownServerName.size();
    %i = %originalSize - 1;
    while (%i >= 0)
    {
        %record = UserListUnknownServerName.getValue(%i);
        %record.csn = BuddyHudTabs.getCityNameForServerName(%record.serverName);
        if (!(%record.csn $= ""))
        {
            UserListUnknownServerName.remove(UserListUnknownServerName.getKey(%i));
        }
        else
        {
            %record.csn = "?";
        }
        %i = %i - 1;
    }
    if (UserListUnknownServerName.size() != %originalSize)
    {
        BuddyHudWin.populateBuddyLists();
    }
    return ;
}
function markIgnoredInList(%list)
{
    %n = %list.size() - 1;
    while (%n >= 0)
    {
        %fave = %list.getValue(%n);
        %fave.ignored = BuddyHudWin.getIgnoreStatus(%fave.name);
        %n = %n - 1;
    }
}

$gRefreshEvenIfBuddyHudWinClosed = 1;
function clientCmdRefreshBuddies(%status)
{
    log("relations", "debug", "clientCmdRefreshBuddies(" @ %status @ ")");
    if ((%status && $gRefreshEvenIfBuddyHudWinClosed) || BuddyHudWin.isVisible())
    {
        BuddyHudWin.refreshFavoritesList();
        $gRefreshEvenIfBuddyHudWinClosed = 0;
    }
    return ;
}
function sendBuddyStatusRequest(%buddyName)
{
    log("relations", "debug", getTrace());
    if ($Token $= "")
    {
        log("general", "debug", getScopeName() SPC "- no token. skipping request.");
        return ;
    }
    %request = sendRequest_GetUserRelations($Player::Name, %buddyName, "onDoneOrErrorCallback_GetUserRelations_Single");
    return ;
}
function onDoneOrErrorCallback_GetUserRelations_Single(%request)
{
    log("network", "debug", getScopeName() SPC "- url =" SPC %request.getURL());
    if (!%request.checkSuccess())
    {
        return ;
    }
    %val = "";
    %list = "";
    if (%request.getValue("friendCount") > 0)
    {
        %key = "friend0";
        %val = %request.getValue(%key);
        %list = UserListFriends;
        log("network", "debug", getScopeName() SPC " -" SPC %key SPC "=" SPC %val);
    }
    else
    {
        if (%request.getValue("favoriteCount") > 0)
        {
            %key = "favorite0";
            %val = %request.getValue("favorite0");
            %list = UserListFavorites;
            log("network", "debug", getScopeName() SPC " -" SPC %key SPC "=" SPC %val);
        }
        else
        {
            if (%request.getValue("fanCount") > 0)
            {
                %key = "fan0";
                %val = %request.getValue("fan0");
                %list = UserListFans;
                log("network", "debug", getScopeName() SPC " -" SPC %key SPC "=" SPC %val);
            }
            else
            {
                log("network", "debug", getScopeName() SPC "- lost relation" SPC %val);
            }
        }
    }
    if (!(%val $= ""))
    {
        %name = getField(%val, 0);
    }
    else
    {
        %name = %request.singleUserName;
    }
    %record = findRelatedPlayerRecord(%name);
    if (isObject(%record))
    {
        %oldList = findRelatedPlayerRecordList(%name);
        if (isObject(%oldList))
        {
            %oldList.remove(%name);
        }
        %record.delete();
    }
    if (!((%val $= "")) && isObject(%list))
    {
        %record = new ScriptObject();
        parseBuddyRecord(%record, %val);
        %list.put(%record.name, %record);
        if (!((%record.serverName $= "")) && (%record.csn $= ""))
        {
            safeEnsureScriptObject("StringMap", "UserListUnknownServerName");
            UserListUnknownServerName.put(%record.name, %record);
            warn(getScopeName() SPC "- unrecogized server name \'" @ %record.serverName @ "\' for relation with single user \'" @ %record.name @ "\'");
            WorldMap.refresh();
        }
    }
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    %ghost = %dict.get(%record.name);
    if (isObject(%ghost))
    {
        %ghost.setIgnore(0);
    }
    %oldIgnoreRecord = findRelatedPlayerRecordInList(%name, UserListIgnores);
    if (isObject(%oldIgnoreRecord))
    {
        UserListIgnores.remove(%name);
        %oldIgnoreRecord.delete();
    }
    if (%request.getValue("ignoreCount") > 0)
    {
        echo("have an ignore record...");
        %key = "ignore0";
        %val = %request.getValue("ignore0");
        %record = new ScriptObject();
        parseBuddyRecord(%record, %val);
        UserListIgnores.put(%record.name, %record);
        if (isObject(%ghost))
        {
            echo("setting ignore to true for" SPC %record.name);
            %ghost.setIgnore(1);
        }
    }
    markIgnoredInList(UserListFriends);
    markIgnoredInList(UserListFavorites);
    BuddyHudWin.populateBuddyLists();
    return ;
}
function BuddyHudTabs::setBuddyIdleStatus(%this, %buddyName, %isIdle)
{
    %entry = UserListFriends.get(%buddyName);
    if (!isObject(%entry))
    {
        return ;
    }
    %entry.isIdle = %isIdle;
    if (%isIdle)
    {
        if (!hasField(%entry.activities, "idle"))
        {
            %entry.activities = "idle" TAB %entry.activities;
        }
    }
    BuddyHudWin.populateBuddyLists();
    return ;
}
function BuddyHudTabs::setBuddyActivities(%this, %buddyName, %activitiesList)
{
    %entry = UserListFriends.get(%buddyName);
    if (!isObject(%entry))
    {
        return ;
    }
    %entry.activities = %activitiesList;
    BuddyHudWin.populateBuddyLists();
    return ;
}
function clientCmdUpdateBuddy(%source, %target, %action)
{
    log("relations", "debug", "clientCmdUpdateBuddy(source=" @ %source @ ", target=" @ %target @ ", action=" @ %action @ ")");
    %other = %target $= $player.getShapeName() ? %source : %target;
    %self = %target $= $player.getShapeName() ? %target : %source;
    if (%action $= "userJoined")
    {
        sendBuddyStatusRequest(%other);
    }
    else
    {
        if (%action $= "userDropped")
        {
            sendBuddyStatusRequest(%other);
        }
        else
        {
            if (%action $= "userToIdle")
            {
                BuddyHudTabs.setBuddyIdleStatus(%source, 1);
            }
            else
            {
                if (%action $= "userToNonIdle")
                {
                    BuddyHudTabs.setBuddyIdleStatus(%source, 0);
                }
                else
                {
                    if (%action $= "friendRequestCreated")
                    {
                        sendBuddyStatusRequest(%other);
                        if (%other $= %source)
                        {
                            handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff><a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a> has just asked to be your friend!  <a:ACCEPT " @ munge(%other) @ ">Accept</a> | <a:DECLINE " @ munge(%other) @ ">Decline</a>");
                        }
                        else
                        {
                            handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff>You have asked <a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a> to be your friend.");
                        }
                    }
                    else
                    {
                        if (%action $= "friendsCreated")
                        {
                            sendBuddyStatusRequest(%other);
                            handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff><a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a> is now your friend.");
                            SystemMessageTextCtrl.updateFriendRequest(%other, 1);
                        }
                        else
                        {
                            if (%action $= "friendsRemoved")
                            {
                                sendBuddyStatusRequest(%other);
                                handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff><a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a> is no longer your friend.");
                            }
                            else
                            {
                                if (%action $= "friendRequestDenied")
                                {
                                    sendBuddyStatusRequest(%other);
                                    if (%self $= %source)
                                    {
                                        handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff>You declined <a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a>\'s friend request.");
                                        SystemMessageTextCtrl.updateFriendRequest(%other, 0);
                                    }
                                }
                                else
                                {
                                    if (%action $= "friendRequestCancelled")
                                    {
                                        sendBuddyStatusRequest(%other);
                                        if (%self $= %source)
                                        {
                                            handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff>You canceled your friend request to <a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a>.");
                                        }
                                        else
                                        {
                                            SystemMessageTextCtrl.updateFriendRequest(%other, 2);
                                        }
                                    }
                                    else
                                    {
                                        if (%action $= "ignoreAdded")
                                        {
                                            sendBuddyStatusRequest(%other);
                                            handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff>You ignored <a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a>.");
                                        }
                                        else
                                        {
                                            if (%action $= "ignoreRemoved")
                                            {
                                                sendBuddyStatusRequest(%other);
                                                handleSystemMessage("msgInfoMessage", "<linkcolor:ffddeeff>You stopped ignoring <a:gamelink " @ munge(%other) @ ">" @ StripMLControlChars(%other) @ "</a>.");
                                            }
                                            else
                                            {
                                                log("relations", "error", "Unknown action: " @ %action);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return ;
}
function BuddyHudFriendsList::scrollToPos(%this, %pos)
{
    BuddyHudPlayerList::scrollToPos(%this, %pos);
    return ;
}
function BuddyHudFriendsList::onURL(%this, %url)
{
    BuddyHudPlayerList::onURL(%this, %url);
    return ;
}
function BuddyHudFriendsList::onRightURL(%this, %url)
{
    BuddyHudPlayerList::onRightURL(%this, %url);
    return ;
}
function BuddyHudPlayerList::scrollToPos(%this, %pos)
{
    %this.getParent().scrollTo(0, 1 - getWord(%pos, 1));
    return ;
}
function BuddyHudPlayerList::onURL(%this, %url)
{
    if (!(firstWord(%url) $= "gamelink"))
    {
        return ;
    }
    if (getWord(%url, 1) $= "player")
    {
        %name = unmunge(getWords(%url, 2));
        onLeftClickPlayerName(%name, "");
    }
    else
    {
        if (getWord(%url, 1) $= "list")
        {
            %listName = getWords(%url, 2);
            $UserPref::buddies::collapsedLists[%listName] = !$UserPref::buddies::collapsedLists[%listName];
            BuddyHudWin.populateBuddyListsReally();
        }
        else
        {
            if (getWord(%url, 1) $= "approveall")
            {
                acceptAllOperation();
            }
            else
            {
                if (getWord(%url, 1) $= "declineall")
                {
                    declineAllOperation();
                }
            }
        }
    }
    return ;
}
function BuddyHudPlayerList::onRightURL(%this, %url)
{
    if (!(firstWord(%url) $= "gamelink"))
    {
        return ;
    }
    if (getWord(%url, 1) $= "player")
    {
        %name = unmunge(getWords(%url, 2));
        onRightClickPlayerName(%name);
    }
    return ;
}
$gLastNameClickTime = 0;
$gLastNameClickName = "";
$gLeftClickTimer = 0;
function onLeftClickPlayerName(%name, %objID)
{
    %curTime = getSimTime();
    if (((%curTime - $gLastNameClickTime) < 400) && ($gLastNameClickName $= %name))
    {
        openUserWhisper(%name);
        if ($gLeftClickTimer != 0)
        {
            cancel($gLeftClickTimer);
            $gLeftClickTimer = 0;
        }
    }
    else
    {
        $gLeftClickTimer = schedule(450, 0, "onSingleClickPlayerName", %name, %objID);
    }
    $gLastNameClickTime = %curTime;
    $gLastNameClickName = %name;
    return ;
}
function onRightClickPlayerName(%name)
{
    PlayerContextMenu.initWithPlayerName(%name);
    PlayerContextMenu.showAtPoint(Canvas.getCursorPos());
    return ;
}
function onSingleClickPlayerName(%name, %objID)
{
    if (showPlayerInfoPopup() && !((%name $= $player.getShapeName())))
    {
        InfoPopupDlg.open();
        InfoPopupDlg.showInfoFor(%name);
        if (0 && isFunction(afxSelectAvatarByName))
        {
            afxSelectAvatarByName(%name);
        }
    }
    if ((isDefined("%objID") && isObject(%objID)) && %objID.isClassAIPlayer())
    {
        rentabotClient_customizeBot(%objID);
    }
    return ;
}
function AIMBuddyList::messageSelected(%this)
{
    %id = %this.getSelectedId();
    %buddyName = stripUnprintables(%this.getRowTextById(%id));
    AIMConvManager.talkTo(%buddyName);
    return ;
}
function AIMBuddyList::inviteSelected(%this)
{
    %id = %this.getSelectedId();
    %buddyName = stripUnprintables(%this.getRowTextById(%id));
    AimInviteDialog.open(%buddyName);
    return ;
}
function AIMBuddyList::onRightMouseDown(%this, %unused, %unused, %mousePt)
{
    if (%mousePt $= "")
    {
        return ;
    }
    %aimName = stripUnprintables(%this.getRowText(%this.getMouseOverRow()));
    PlayerContextMenu.initForAIM(%aimName);
    PlayerContextMenu.showAtPoint(%mousePt);
    return ;
}
function clientCmdBeginNPCList()
{
    safeEnsureScriptObjectWithInit("StringMap", "NPCList", "{ ignoreCase = true; }");
    NPCList.clear();
    return ;
}
function clientCmdItsAnNPC(%npcName)
{
    safeEnsureScriptObjectWithInit("StringMap", "NPCList", "{ ignoreCase = true; }");
    NPCList.put(%npcName, %npcName);
    return ;
}
function isNPCName(%name)
{
    if (!isObject(NPCList))
    {
        if (isObject(ServerConnection))
        {
            error(getScopeName() SPC "- NPC list not initialized." SPC %name SPC getTrace());
        }
        return 0;
    }
    %ret = !(NPCList.get(%name) $= "");
    if (!%ret)
    {
        %ret = rentabot_isRentabotName(%name);
    }
    return %ret;
}
function findRelatedPlayerRecordInList(%playerName, %list)
{
    if (!isObject(%list))
    {
        error(getScopeName() SPC "- no list!" SPC %playerName SPC getDebugString(%list) SPC getTrace());
        return 0;
    }
    %rec = %list.get(%playerName);
    if (isObject(%rec))
    {
        return %rec;
    }
    return 0;
}
function isRelatedPlayerRecordInList(%playerName, %list)
{
    if (!isObject(%list))
    {
        return 0;
    }
    %rec = findRelatedPlayerRecordInList(%playerName, %list);
    return isObject(%rec);
}
function findRelatedPlayerRecordList(%playerName)
{
    %list = UserListFriends;
    if (isRelatedPlayerRecordInList(%playerName, %list))
    {
        return %list;
    }
    %list = UserListFavorites;
    if (isRelatedPlayerRecordInList(%playerName, %list))
    {
        return %list;
    }
    %list = UserListFans;
    if (isRelatedPlayerRecordInList(%playerName, %list))
    {
        return %list;
    }
    %list = UserListIgnores;
    if (isRelatedPlayerRecordInList(%playerName, %list))
    {
        return %list;
    }
    return 0;
}
function findRelatedPlayerRecord(%playerName)
{
    %list = findRelatedPlayerRecordList(%playerName);
    if (!isObject(%list))
    {
        return 0;
    }
    return findRelatedPlayerRecordInList(%playerName, %list);
}
function AIMInviteButton::do(%this)
{
    AIMBuddyList.inviteSelected();
    %selected = AIMBuddyList.getSelectedId();
    %row = AIMBuddyList.getRowNumById(%selected);
    %row = %row + 1;
    if (%row >= AIMBuddyList.rowCount())
    {
        %row = 0;
    }
    AIMBuddyList.setSelectedRow(%row);
    return ;
}
function Player::onGotBuddyStatus(%this)
{
    %this.schedule(0, "updateMapIcon");
    return ;
}
function Player::onGotFaveStatus(%this)
{
    %this.schedule(0, "updateMapIcon");
    return ;
}
