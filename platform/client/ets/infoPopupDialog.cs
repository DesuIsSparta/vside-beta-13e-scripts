function InfoPopupDlg::open(%this)
{
    %this.init();
    if ($UserPref::HudTabs::AutoOpen["affinity"])
    {
        HudTabs.selectTabWithName("affinity");
    }
    return ;
}
function InfoPopupDlg::close(%this)
{
    if (%this.isShowing())
    {
        HudTabs.close();
    }
    return ;
}
function InfoPopupDlg::onClose(%this)
{
    %this.clear();
    return ;
}
function InfoPopupDlg::isShowing(%this)
{
    return HudTabs.getCurrentTab().name $= "affinity";
}
function InfoPopupDlg::init(%this)
{
    if (!%this.initialized)
    {
        %this.waitIcon = AnimCtrl::newAnimCtrl("91 36", "18 18");
        %this.waitIcon.setDelay(60);
        %this.waitIcon.addFrame("platform/client/ui/wait0.png");
        %this.waitIcon.addFrame("platform/client/ui/wait1.png");
        %this.waitIcon.addFrame("platform/client/ui/wait2.png");
        %this.waitIcon.addFrame("platform/client/ui/wait3.png");
        %this.waitIcon.addFrame("platform/client/ui/wait4.png");
        %this.waitIcon.addFrame("platform/client/ui/wait5.png");
        %this.waitIcon.addFrame("platform/client/ui/wait6.png");
        %this.waitIcon.addFrame("platform/client/ui/wait7.png");
        %this.add(%this.waitIcon);
        %this.waitIcon.setVisible(0);
        %this.initialized = 1;
        %this.playerName = "";
        %this.clear();
    }
    return ;
}
function InfoPopupDlg::clear(%this)
{
    InfoPopupNameField.setText("");
    InfoPopupContents.setText("Click on a player or player\'s name to see information about them.");
    InfoPopupTagsScroll.setVisible(0);
    InfoPopupBottom.setText("");
    return ;
}
function InfoPopupDlg::showInfoFor(%this, %playerName)
{
    if (rentabot_isRentabotName(%playerName))
    {
        %this.open();
        InfoPopupContents.setText("Sorry," SPC %playerName SPC "doesn\'t have info..");
        InfoPopupTagsText.setText("");
        InfoPopupBottom.setText("");
        %this.waitIcon.stop();
        %this.waitIcon.setVisible(0);
        return ;
    }
    %this.playerName = %playerName;
    %this.tryShowPlayerInfo();
    return ;
}
function InfoPopupDlg::tryShowPlayerInfo(%this)
{
    %this.init();
    %tableSettings = "<tab:88,215>";
    %fieldOpen = "" TAB "<spush>";
    %fieldClose = "<spop>";
    if (%this.playerName $= "")
    {
        echoDebug("tryShowPlayerInfo(): No playerName set.");
        return ;
    }
    %playerName = StripMLControlChars(%this.playerName);
    gSetField(%this, playerNameDict, %playerName);
    %info = PlayerInfoMap.get(%playerName);
    if (isObject(%info))
    {
        %this.waitIcon.stop();
        %this.waitIcon.setVisible(0);
        %age = StripMLControlChars(%info.age);
        if (%age $= "")
        {
            %age = "hidden";
        }
        %gender = %info.gender;
        if (!((%gender $= "f")) && !((%gender $= "m")))
        {
            %gender = "n";
        }
        %this.playerGender = %gender;
        %location = StripMLControlChars(%info.location);
        if (%location $= "")
        {
            %location = "hidden";
        }
        %tags = %info.tags;
        %respektScore = StripMLControlChars(%info.respekt);
        if (%respektScore $= "")
        {
            %respektScore = "(unknown)";
            %respektLvl = "?";
            %respektLvlName = "(unknown)";
        }
        else
        {
            %respektLvl = respektScoreToLevel(%respektScore);
            %respektLvlName = respektLevelToNameWithoutArticle(%respektLvl);
        }
        %respektRank = StripMLControlChars(%info.respektRank);
        if (%respektRank $= "")
        {
            %respektRank = "?";
        }
        %ageText = "<spush><b>Age<spop>: " @ %fieldOpen @ %age @ %fieldClose;
        %locText = "<br><spush><b>Location<spop>: " @ %fieldOpen @ %location @ %fieldClose;
        %respektText = "<br><spush><b>Level:<spop>" @ %fieldOpen @ %respektLvl @ " - " @ %respektLvlName @ "<br><spush><b><a:gamelink " @ $Net::HelpURL_VPoints @ "?section=vPoints>All-time <bitmap:" @ "platform/client/ui/vpoints_14" @ "></a>:<spop>" @ %fieldOpen @ %respektScore;
        %contentTagsText = "<br><spush><b>Shared Interests (tags)<spop>:";
        %tagsText = "";
        if ($ETS::PlayerInfo::NoTags)
        {
            %tagsText = %tagsText @ "Interests you share with <a:PROFILE>" @ $genderPronounHimHerThem[%gender] @ "</a> will show up here," @ "but you have none in your profile!" SPC "<a:EDIT_PROFILE>Click here</a> to add some!";
        }
        else
        {
            if (%tags $= "")
            {
                %tagsText = %tagsText @ "You have no interests in common with <a:PROFILE>" @ $genderPronounHimHerThem[%gender] @ "</a>.";
            }
            else
            {
                %tagsText = %tagsText @ %this.splitTagsIntoLinks(%tags, 0);
            }
        }
        %activitiesText = "<br><spush><b>Activities: <spop>" @ %fieldOpen @ getUserActivityMgr().getActivitiesMLText(%info.activities, 5) @ %fieldClose;
        InfoPopupDlg.setAffinityName(%playerName);
        InfoPopupContents.setText(%tableSettings @ %ageText @ %locText @ %respektText @ %activitiesText @ %contentTagsText @ "");
        InfoPopupBottom.setText(%this.getInfoPopupBottomText(%playerName));
        InfoPopupBottom.forceReflow();
        InfoPopupContents.forceReflow();
        InfoPopupTagsText.setText(%tagsText);
        %ypos = (getWord(InfoPopupContents.getExtent(), 1) + getWord(InfoPopupContents.getPosition(), 1)) - 1;
        %yExt = (getWord(InfoPopupBottom.getPosition(), 1) - %ypos) - 1;
        InfoPopupTagsScroll.resize(0, %ypos, 225, %yExt);
        InfoPopupTagsScroll.setVisible(1);
        %this.playerName = "";
    }
    else
    {
        %this.waitIcon.setVisible(1);
        %this.waitIcon.start();
        InfoPopupDlg.setAffinityName(%playerName);
        InfoPopupTagsText.setText("");
        InfoPopupBottom.setText("");
        requestPlayerInfoFor(%this.playerName);
    }
    return ;
}
function InfoPopupDlg::setAffinityName(%this, %playerName)
{
    %nameFieldString = %playerName;
    %visibleCharLimit = 17;
    if (strlen(%nameFieldString) > %visibleCharLimit)
    {
        %nameFieldString = getSubStr(%playerName, 0, %visibleCharLimit - 3);
        %nameFieldString = %nameFieldString @ "...";
    }
    InfoPopupNameField.setText("(" @ %nameFieldString @ ")");
    return ;
}
function InfoPopupDlg::showPlayerNotFound(%this)
{
    InfoPopupContents.setText("Sorry, couldn\'t find anyone named " @ %this.playerName @ ".");
    InfoPopupTagsText.setText("");
    InfoPopupBottom.setText("");
    return ;
}
function InfoPopupDlg::splitTagsIntoLinks(%this, %tags, %includeCategory)
{
    %ret = "";
    %num = getFieldCount(%tags);
    %n = 0;
    while (%n < %num)
    {
        %tag = getField(%tags, %n);
        if (%n > 0)
        {
            %ret = %ret @ " | ";
        }
        if (%includeCategory)
        {
            %dispTag = %tag;
        }
        else
        {
            %dispTag = getSubStr(strrchr(%tag, ":"), 1, 10000);
        }
        %ret = %ret @ "<a:TAG" SPC munge(%tag) @ ">" @ %dispTag @ "</a>";
        %n = %n + 1;
    }
    return %ret;
}
function InfoPopupDlg::getInfoPopupBottomText(%this, %playerName)
{
    %profileLink = "<a:PROFILE>Web Profile</a>";
    %addRemoveIgnoreUnignore = %this.getAddRemoveIgnoreUnignoreText(%playerName);
    %ret = %profileLink @ " | " @ %addRemoveIgnoreUnignore;
    return %ret;
}
function InfoPopupDlg::getAddRemoveIgnoreUnignoreText(%this, %playerName)
{
    %ret = "";
    if (!isObject(UserListFavorites))
    {
        error(getTrace() SPC "- hmm. not sure how this happened.");
        return "<just:center>(favorite status unknown)";
    }
    %friendStatus = BuddyHudWin.getFriendStatus(%playerName);
    %isIgnr = BuddyHudWin.getIgnoreStatus(%playerName);
    %faveLink = "";
    %faveLink2 = "";
    %faveText = "";
    %faveText2 = "";
    if (%friendStatus $= "friends")
    {
        %faveLink = "FRIEND_REM";
        %faveText = "Remove";
    }
    else
    {
        if (%friendStatus $= "favorite")
        {
            %faveLink = "CANCEL_REQ";
            %faveText = "Cancel";
        }
        else
        {
            if (%friendStatus $= "fan")
            {
                %faveLink = "ACCEPT_REQ";
                %faveText = "Accept";
                %faveLink2 = "DECLINE_REQ";
                %faveText2 = "Decline";
            }
            else
            {
                if (%friendStatus $= "none")
                {
                    %faveLink = "FRIEND_ADD";
                    %faveText = "Add";
                }
            }
        }
    }
    if (%isIgnr)
    {
        %ignrLink = "REMIGNR";
        %ignrText = "Unignore";
    }
    else
    {
        %ignrLink = "ADDIGNR";
        %ignrText = "Ignore";
    }
    %ret = "";
    if (!(%faveLink2 $= ""))
    {
        %ret = %ret @ "Friend request:<a:" @ %faveLink @ ">" @ %faveText @ "</a>";
        %ret = %ret @ " | <a:" @ %faveLink2 @ ">" @ %faveText2 @ "</a>";
    }
    else
    {
        %ret = %ret @ "<a:" @ %faveLink @ ">" @ %faveText @ "</a>";
        %ret = %ret @ " | <a:" @ %ignrLink @ ">" @ %ignrText @ "</a>";
    }
    return %ret;
}
function InfoPopupDlg::stopAnimation(%this)
{
    %this.init();
    %this.waitIcon.stop();
    %this.waitIcon.setVisible(0);
    return ;
}
function InfoPopupContents::onURL(%this, %url)
{
    %first = getWord(%url, 0);
    %rest = getWords(%url, 1, 10000);
    if (%first $= "PROFILE")
    {
        doUserProfile(InfoPopupDlg.playerName);
    }
    else
    {
        if (%first $= "EDIT_PROFILE")
        {
            doEditProfile();
        }
        else
        {
            if (%first $= "TAG")
            {
                doViewTag(unmunge(%rest));
            }
            else
            {
                if (%first $= "FRIEND_ADD")
                {
                    doUserFavorite(InfoPopupDlg.playerName, "add");
                }
                else
                {
                    if (%first $= "FRIEND_REM")
                    {
                        doUserFavorite(InfoPopupDlg.playerName, "remove");
                    }
                    else
                    {
                        if (%first $= "CANCEL_REQ")
                        {
                            doUserFavorite(InfoPopupDlg.playerName, "cancel");
                        }
                        else
                        {
                            if (%first $= "ACCEPT_REQ")
                            {
                                doUserFavorite(InfoPopupDlg.playerName, "accept");
                            }
                            else
                            {
                                if (%first $= "DECLINE_REQ")
                                {
                                    doUserFavorite(InfoPopupDlg.playerName, "decline");
                                }
                                else
                                {
                                    if (%first $= "ADDIGNR")
                                    {
                                        doUserIgnore(InfoPopupDlg.playerName, "add");
                                    }
                                    else
                                    {
                                        if (%first $= "REMIGNR")
                                        {
                                            doUserIgnore(InfoPopupDlg.playerName, "remove");
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
function InfoPopupTagsText::onURL(%this, %url)
{
    InfoPopupContents::onURL(%this, %url);
    return ;
}
function InfoPopupNameField::onURL(%this, %url)
{
    InfoPopupContents::onURL(%this, %url);
    return ;
}
function InfoPopupNameField::onRightURL(%this, %url)
{
    %first = getWord(%url, 0);
    if (%first $= "PROFILE")
    {
        onRightClickPlayerName(InfoPopupDlg.playerName);
    }
    return ;
}
function InfoPopupContents::sheduleBuddyRefreshIfNeeded(%this)
{
    return ;
}
function InfoPopupBottom::onURL(%this, %url)
{
    InfoPopupContents.onURL(%url);
    return ;
}
