function InfoPopupDlg::open(%this) {
    %this.init();
    if () {
        "affinity".selectTabWithName();
    }
};
function InfoPopupDlg::close(%this) {
    if (%this.isShowing()) {
        close();
    }
};
function InfoPopupDlg::onClose(%this) {
    %this.clear();
};
function InfoPopupDlg::isShowing(%this) {
    return (getCurrentTab() SPC name $= "affinity");
};
function InfoPopupDlg::init(%this) {
    if (!(initialized)) {
        waitIcon = %this @ AnimCtrl::newAnimCtrl("91 36", "18 18") @ %this;
        waitIcon.setDelay(60);
        waitIcon.addFrame("platform/client/ui/wait0.png");
        waitIcon.addFrame("platform/client/ui/wait1.png");
        waitIcon.addFrame("platform/client/ui/wait2.png");
        waitIcon.addFrame("platform/client/ui/wait3.png");
        waitIcon.addFrame("platform/client/ui/wait4.png");
        waitIcon.addFrame("platform/client/ui/wait5.png");
        waitIcon.addFrame("platform/client/ui/wait6.png");
        waitIcon.addFrame("platform/client/ui/wait7.png");
        %this.add(waitIcon);
        waitIcon.setVisible(0);
        initialized = %this @ 1 @ %this;
        %this;
        playerName = %this @ "" @ %this;
        %this;
        %this.clear();
    }
};
function InfoPopupDlg::clear(%this) {
    "".setText();
    "Click on a player or player's name to see information about them.".setText();
    0.setVisible();
    "".setText();
};
function InfoPopupDlg::showInfoFor(%this, %playerName) {
    if (rentabot_isRentabotName(%playerName)) {
        %this.open();
        "Sorry," @ " " @ %playerName @ " " @ "doesn't have info..".setText();
        "".setText();
        "".setText();
        waitIcon.stop();
        waitIcon.setVisible(0);
        return %this;
    }
    playerName = %playerName @ %this;
    %this.tryShowPlayerInfo();
};
function InfoPopupDlg::tryShowPlayerInfo(%this) {
    %this.init();
    %tableSettings = "<tab:88,215>";
    %fieldOpen = "" @ "\t" @ "<spush>";
    %fieldClose = "<spop>";
    if ((%this SPC playerName $= "")) {
        echoDebug("tryShowPlayerInfo(): No playerName set.");
        return;
    }
    %playerName = StripMLControlChars(playerName);
    %this;
    gSetField(%this, %playerName);
    %info = %playerName.get();
    PlayerInfoMap;
    if (isObject(%info)) {
        waitIcon.stop();
        waitIcon.setVisible(0);
        %age = StripMLControlChars(age);
        %info;
        if ((%this SPC %age $= "")) {
            %age = "hidden";
            %this;
        }
        %gender = gender;
        %info;
        if (!(playerNameDict SPC %gender $= "f")) {
        }
        if (!(%gender $= "m")) {
            %gender = "n";
        }
        playerGender = %gender @ %this;
        %location = StripMLControlChars(location);
        %info;
        if ((%location $= "")) {
            %location = "hidden";
        }
        %tags = tags;
        %info;
        %respektScore = StripMLControlChars(respekt);
        %info;
        if ((%respektScore $= "")) {
            %respektScore = "(unknown)";
            %respektLvl = "?";
            %respektLvlName = "(unknown)";
        }
        %respektLvl = respektScoreToLevel(%respektScore);
        %respektLvlName = respektLevelToNameWithoutArticle(%respektLvl);
        %respektRank = StripMLControlChars(respektRank);
        %info;
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
        %tagsText = %tagsText @ %this.splitTagsIntoLinks(%tags, 0);
        %activitiesText = "<br><spush><b>Activities: <spop>" @ %fieldOpen @ %info @ getUserActivityMgr().getActivitiesMLText(activities, 5) @ %fieldClose;
        %playerName.setAffinityName();
        InfoPopupDlg @ InfoPopupContents @ %tableSettings @ %ageText @ %locText @ %respektText @ %activitiesText @ %contentTagsText @ "".setText();
        %this.getInfoPopupBottomText(%playerName).setText();
        forceReflow();
        forceReflow();
        %tagsText.setText();
        %ypos = (getWord(getPosition(), 1) - (InfoPopupContents + getWord(getExtent(), 1)));
        InfoPopupContents;
        %yExt = (%ypos - (InfoPopupBottom - getWord(getPosition(), 1)));
        1.0;
        0.resize(%ypos, 225, %yExt);
        1.setVisible();
        playerName = InfoPopupTagsScroll @ "" @ %this;
        InfoPopupTagsScroll;
    }
    waitIcon.setVisible(1);
    waitIcon.start();
    %playerName.setAffinityName();
    "".setText();
    "".setText();
    requestPlayerInfoFor(playerName);
};
function InfoPopupDlg::setAffinityName(%this, %playerName) {
    %nameFieldString = %playerName;
    %visibleCharLimit = 17;
    if ((%visibleCharLimit > strlen(%nameFieldString))) {
        %nameFieldString = getSubStr(%playerName, 0, (3.0 - %visibleCharLimit));
        %nameFieldString = %nameFieldString @ "...";
    }
    InfoPopupNameField @ "(" @ %nameFieldString @ ")".setText();
};
function InfoPopupDlg::showPlayerNotFound(%this) {
    InfoPopupContents @ "Sorry, couldn't find anyone named " @ %this @ playerName @ ".".setText();
    "".setText();
    "".setText();
};
function InfoPopupDlg::splitTagsIntoLinks(%this, %tags, %includeCategory) {
    %ret = "";
    %num = getFieldCount(%tags);
    %n = 0;
    if ((%num < %n)) {
        %tag = getField(%tags, %n);
        if ((0.0 > %n)) {
            %ret = %ret @ " | ";
        }
        if (%includeCategory) {
            %dispTag = %tag;
        }
        %dispTag = getSubStr(strrchr(%tag, ":"), 1, 10000);
        %ret = %ret @ "<a:TAG" @ " " @ munge(%tag) @ ">" @ %dispTag @ "</a>";
        %n = (1.0 + %n);
    }
    return %ret;
};
function InfoPopupDlg::getInfoPopupBottomText(%this, %playerName) {
    %profileLink = "<a:PROFILE>Web Profile</a>";
    %addRemoveIgnoreUnignore = %this.getAddRemoveIgnoreUnignoreText(%playerName);
    %ret = %profileLink @ " | " @ %addRemoveIgnoreUnignore;
    return %ret;
};
function InfoPopupDlg::getAddRemoveIgnoreUnignoreText(%this, %playerName) {
    %ret = "";
    if (!(isObject())) {
        error(getTrace() @ " " @ "- hmm. not sure how this happened.");
        return "<just:center>(favorite status unknown)";
    }
    %friendStatus = %playerName.getFriendStatus();
    BuddyHudWin;
    %isIgnr = %playerName.getIgnoreStatus();
    BuddyHudWin;
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
    waitIcon.stop();
    waitIcon.setVisible(0);
};
function InfoPopupContents::onURL(%this, %url) {
    %first = getWord(%url, 0);
    %rest = getWords(%url, 1, 10000);
    if ((%first $= "PROFILE")) {
        doUserProfile(playerName);
    }
    if ((InfoPopupDlg SPC %first $= "EDIT_PROFILE")) {
        doEditProfile();
    }
    if ((%first $= "TAG")) {
        doViewTag(unmunge(%rest));
    }
    if ((%first $= "FRIEND_ADD")) {
        doUserFavorite(playerName, "add");
    }
    if ((InfoPopupDlg SPC %first $= "FRIEND_REM")) {
        doUserFavorite(playerName, "remove");
    }
    if ((InfoPopupDlg SPC %first $= "CANCEL_REQ")) {
        doUserFavorite(playerName, "cancel");
    }
    if ((InfoPopupDlg SPC %first $= "ACCEPT_REQ")) {
        doUserFavorite(playerName, "accept");
    }
    if ((InfoPopupDlg SPC %first $= "DECLINE_REQ")) {
        doUserFavorite(playerName, "decline");
    }
    if ((InfoPopupDlg SPC %first $= "ADDIGNR")) {
        doUserIgnore(playerName, "add");
    }
    if ((InfoPopupDlg SPC %first $= "REMIGNR")) {
        doUserIgnore(playerName, "remove");
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
        onRightClickPlayerName(playerName);
    }
};
function InfoPopupContents::sheduleBuddyRefreshIfNeeded(%this) {
};
function InfoPopupBottom::onURL(%this, %url) {
    %url.onURL();
};
