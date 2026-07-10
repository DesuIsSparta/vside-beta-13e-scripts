function InfoPopupDlg::open(%this) {
    %this.init();
    if ($UserPref::HudTabs::AutoOpen["affinity"]) {
        "affinity".selectTabWithName(HudTabs);
    }
};
function InfoPopupDlg::close(%this) {
    if (%this.isShowing()) {
        HudTabs.close();
    }
};
function InfoPopupDlg::onClose(%this) {
    %this.clear();
};
function InfoPopupDlg::isShowing(%this) {
    return (HudTabs.getCurrentTab().name $= "affinity");
};
function InfoPopupDlg::init(%this) {
    if (!(%this.initialized)) {
        %this.waitIcon = AnimCtrl::newAnimCtrl("91 36", "18 18");
        60.setDelay(%this.waitIcon);
        "platform/client/ui/wait0.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait1.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait2.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait3.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait4.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait5.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait6.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait7.png".addFrame(%this.waitIcon);
        %this.waitIcon.add(%this);
        0.setVisible(%this.waitIcon);
        %this.initialized = 1;
        %this.playerName = "";
        %this.clear();
    }
};
function InfoPopupDlg::clear(%this) {
    "".setText(InfoPopupNameField);
    "Click on a player or player's name to see information about them.".setText(InfoPopupContents);
    0.setVisible(InfoPopupTagsScroll);
    "".setText(InfoPopupBottom);
};
function InfoPopupDlg::showInfoFor(%this, %playerName) {
    if (rentabot_isRentabotName(%playerName)) {
        %this.open();
        "Sorry," @ " " @ %playerName @ " " @ "doesn't have info..".setText(InfoPopupContents);
        "".setText(InfoPopupTagsText);
        "".setText(InfoPopupBottom);
        %this.waitIcon.stop();
        0.setVisible(%this.waitIcon);
        return;
    }
    %this.playerName = %playerName;
    %this.tryShowPlayerInfo();
};
function InfoPopupDlg::tryShowPlayerInfo(%this) {
    %this.init();
    %tableSettings = "<tab:88,215>";
    %fieldOpen = "" @ "\t" @ "<spush>";
    %fieldClose = "<spop>";
    if ((%this.playerName $= "")) {
        echoDebug("tryShowPlayerInfo(): No playerName set.");
        return;
    }
    %playerName = StripMLControlChars(%this.playerName);
    gSetField(%this, playerNameDict, %playerName);
    %info = %playerName.get(PlayerInfoMap);
    if (isObject(%info)) {
        %this.waitIcon.stop();
        0.setVisible(%this.waitIcon);
        %age = StripMLControlChars(%info.age);
        if ((%age $= "")) {
            %age = "hidden";
        }
        %gender = %info.gender;
        if (!(%gender $= "f")) {
        }
        if (!(%gender $= "m")) {
            %gender = "n";
        }
        %this.playerGender = %gender;
        %location = StripMLControlChars(%info.location);
        if ((%location $= "")) {
            %location = "hidden";
        }
        %tags = %info.tags;
        %respektScore = StripMLControlChars(%info.respekt);
        if ((%respektScore $= "")) {
            %respektScore = "(unknown)";
            %respektLvl = "?";
            %respektLvlName = "(unknown)";
        }
        %respektLvl = respektScoreToLevel(%respektScore);
        %respektLvlName = respektLevelToNameWithoutArticle(%respektLvl);
        %respektRank = StripMLControlChars(%info.respektRank);
        if ((%respektRank $= "")) {
            %respektRank = "?";
        }
        %ageText = "<spush><b>Age<spop>: " @ %fieldOpen @ %age @ %fieldClose;
        %locText = "<br><spush><b>Location<spop>: " @ %fieldOpen @ %location @ %fieldClose;
        %respektText = "<br><spush><b>Level:<spop>" @ %fieldOpen @ %respektLvl @ " - " @ %respektLvlName @ "<br><spush><b><a:gamelink " @ $Net::HelpURL_VPoints @ "?section=vPoints>All-time <bitmap:" @ "platform/client/ui/vpoints_14" @ "></a>:<spop>" @ %fieldOpen @ %respektScore;
        %contentTagsText = "<br><spush><b>Shared Interests (tags)<spop>:";
        %tagsText = "";
        if ($ETS::PlayerInfo::NoTags) {
            %tagsText = %tagsText @ "Interests you share with <a:PROFILE>" @ %gender[$genderPronounHimHerThem @ %gender] @ "</a> will show up here," @ "but you have none in your profile!" @ " " @ "<a:EDIT_PROFILE>Click here</a> to add some!";
        }
        if ((%tags $= "")) {
            %tagsText = %tagsText @ "You have no interests in common with <a:PROFILE>" @ %gender[$genderPronounHimHerThem @ %gender] @ "</a>.";
        }
        %tagsText = %tagsText @ 0.splitTagsIntoLinks(%this, %tags);
        %activitiesText = "<br><spush><b>Activities: <spop>" @ %fieldOpen @ 5.getActivitiesMLText(getUserActivityMgr(), %info.activities) @ %fieldClose;
        %playerName.setAffinityName(InfoPopupDlg);
        %tableSettings @ %ageText @ %locText @ %respektText @ %activitiesText @ %contentTagsText @ "".setText(InfoPopupContents);
        %playerName.getInfoPopupBottomText(%this).setText(InfoPopupBottom);
        InfoPopupBottom.forceReflow();
        InfoPopupContents.forceReflow();
        %tagsText.setText(InfoPopupTagsText);
        %ypos = ((getWord(InfoPopupContents.getExtent(), 1) + getWord(InfoPopupContents.getPosition(), 1)) - 1.0);
        %yExt = ((getWord(InfoPopupBottom.getPosition(), 1) - %ypos) - 1.0);
        %yExt.resize(InfoPopupTagsScroll, 0, %ypos, 225);
        1.setVisible(InfoPopupTagsScroll);
        %this.playerName = "";
    }
    1.setVisible(%this.waitIcon);
    %this.waitIcon.start();
    %playerName.setAffinityName(InfoPopupDlg);
    "".setText(InfoPopupTagsText);
    "".setText(InfoPopupBottom);
    requestPlayerInfoFor(%this.playerName);
};
function InfoPopupDlg::setAffinityName(%this, %playerName) {
    %nameFieldString = %playerName;
    %visibleCharLimit = 17;
    if ((strlen(%nameFieldString) > %visibleCharLimit)) {
        %nameFieldString = getSubStr(%playerName, 0, (%visibleCharLimit - 3.0));
        %nameFieldString = %nameFieldString @ "...";
    }
    "(" @ %nameFieldString @ ")".setText(InfoPopupNameField);
};
function InfoPopupDlg::showPlayerNotFound(%this) {
    "Sorry, couldn't find anyone named " @ %this.playerName @ ".".setText(InfoPopupContents);
    "".setText(InfoPopupTagsText);
    "".setText(InfoPopupBottom);
};
function InfoPopupDlg::splitTagsIntoLinks(%this, %tags, %includeCategory) {
    %ret = "";
    %num = getFieldCount(%tags);
    %n = 0;
    while ((%n < %num)) {
        %tag = getField(%tags, %n);
        if ((%n > 0.0)) {
            %ret = %ret @ " | ";
        }
        if (%includeCategory) {
            %dispTag = %tag;
        }
        %dispTag = getSubStr(strrchr(%tag, ":"), 1, 10000);
        %ret = %ret @ "<a:TAG" @ " " @ munge(%tag) @ ">" @ %dispTag @ "</a>";
        %n = (%n + 1.0);
    }
    return %ret;
};
function InfoPopupDlg::getInfoPopupBottomText(%this, %playerName) {
    %profileLink = "<a:PROFILE>Web Profile</a>";
    %addRemoveIgnoreUnignore = %playerName.getAddRemoveIgnoreUnignoreText(%this);
    %ret = %profileLink @ " | " @ %addRemoveIgnoreUnignore;
    return %ret;
};
function InfoPopupDlg::getAddRemoveIgnoreUnignoreText(%this, %playerName) {
    %ret = "";
    if (!(isObject(UserListFavorites))) {
        error(getTrace() @ " " @ "- hmm. not sure how this happened.");
        return "<just:center>(favorite status unknown)";
    }
    %friendStatus = %playerName.getFriendStatus(BuddyHudWin);
    %isIgnr = %playerName.getIgnoreStatus(BuddyHudWin);
    %faveLink = "";
    %faveLink2 = "";
    %faveText = "";
    %faveText2 = "";
    if ((%friendStatus $= "friends")) {
        %faveLink = "FRIEND_REM";
        %faveText = "Remove";
    }
    if ((%friendStatus $= "favorite")) {
        %faveLink = "CANCEL_REQ";
        %faveText = "Cancel";
    }
    if ((%friendStatus $= "fan")) {
        %faveLink = "ACCEPT_REQ";
        %faveText = "Accept";
        %faveLink2 = "DECLINE_REQ";
        %faveText2 = "Decline";
    }
    if ((%friendStatus $= "none")) {
        %faveLink = "FRIEND_ADD";
        %faveText = "Add";
    }
    if (%isIgnr) {
        %ignrLink = "REMIGNR";
        %ignrText = "Unignore";
    }
    %ignrLink = "ADDIGNR";
    %ignrText = "Ignore";
    %ret = "";
    if (!(%faveLink2 $= "")) {
        %ret = %ret @ "Friend request:<a:" @ %faveLink @ ">" @ %faveText @ "</a>";
        %ret = %ret @ " | <a:" @ %faveLink2 @ ">" @ %faveText2 @ "</a>";
    }
    %ret = %ret @ "<a:" @ %faveLink @ ">" @ %faveText @ "</a>";
    %ret = %ret @ " | <a:" @ %ignrLink @ ">" @ %ignrText @ "</a>";
    return %ret;
};
function InfoPopupDlg::stopAnimation(%this) {
    %this.init();
    %this.waitIcon.stop();
    0.setVisible(%this.waitIcon);
};
function InfoPopupContents::onURL(%this, %url) {
    %first = getWord(%url, 0);
    %rest = getWords(%url, 1, 10000);
    if ((%first $= "PROFILE")) {
        doUserProfile(InfoPopupDlg.playerName);
    }
    if ((%first $= "EDIT_PROFILE")) {
        doEditProfile();
    }
    if ((%first $= "TAG")) {
        doViewTag(unmunge(%rest));
    }
    if ((%first $= "FRIEND_ADD")) {
        doUserFavorite(InfoPopupDlg.playerName, "add");
    }
    if ((%first $= "FRIEND_REM")) {
        doUserFavorite(InfoPopupDlg.playerName, "remove");
    }
    if ((%first $= "CANCEL_REQ")) {
        doUserFavorite(InfoPopupDlg.playerName, "cancel");
    }
    if ((%first $= "ACCEPT_REQ")) {
        doUserFavorite(InfoPopupDlg.playerName, "accept");
    }
    if ((%first $= "DECLINE_REQ")) {
        doUserFavorite(InfoPopupDlg.playerName, "decline");
    }
    if ((%first $= "ADDIGNR")) {
        doUserIgnore(InfoPopupDlg.playerName, "add");
    }
    if ((%first $= "REMIGNR")) {
        doUserIgnore(InfoPopupDlg.playerName, "remove");
    }
};
function InfoPopupTagsText::onURL(%this, %url) {
    InfoPopupContents::onURL(%this, %url);
};
function InfoPopupNameField::onURL(%this, %url) {
    InfoPopupContents::onURL(%this, %url);
};
function InfoPopupNameField::onRightURL(%this, %url) {
    %first = getWord(%url, 0);
    if ((%first $= "PROFILE")) {
        onRightClickPlayerName(InfoPopupDlg.playerName);
    }
};
function InfoPopupContents::sheduleBuddyRefreshIfNeeded(%this) {
};
function InfoPopupBottom::onURL(%this, %url) {
    %url.onURL(InfoPopupContents);
};
