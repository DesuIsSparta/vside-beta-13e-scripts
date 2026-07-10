function HudTabs::setup() {
    if (!(isObject(HudTabs))) {
        new ScriptObject(HudTabs) {
            class = "TabControl";
        };
        if (isObject(MissionCleanup)) {
            HudTabs.add(MissionCleanup);
        }
        "vertical".Initialize(HudTabs, HudContainer, "46 39", "", "");
        "music & videos".newTab(HudTabs, "music", "platform/client/buttons/hud_music");
        "".newTab(HudTabs, "affinity", "platform/client/buttons/hud_affinity");
        "".newTab(HudTabs, "scores", "platform/client/buttons/hud_scores");
        "".newTab(HudTabs, "word", "platform/client/buttons/hud_word");
        "".newTab(HudTabs, "tutorial", "platform/client/buttons/hud_tutorials");
        HudTabs.fillTabs();
        HudTabs.close();
        closeTimer = 0 @ HudTabs;
    }
};
function HudTabs::newTab(%this, %name, %bitmapName, %title) {
    if (!(isDefined("%title"))) {
        %title = "";
    }
    %tab = Parent::newTab(%this, %name, %bitmapName);
    %tab.title = %title;
    %tab.pulsar = AnimCtrl::newAnimCtrl("0 0", "54 43");
    60.setDelay(%tab.pulsar);
    "platform/client/ui/pulse/bracket_00.png".addFrame(%tab.pulsar);
    "platform/client/ui/pulse/bracket_01.png".addFrame(%tab.pulsar);
    "platform/client/ui/pulse/bracket_02.png".addFrame(%tab.pulsar);
    "platform/client/ui/pulse/bracket_03.png".addFrame(%tab.pulsar);
    "platform/client/ui/pulse/bracket_04.png".addFrame(%tab.pulsar);
    "platform/client/ui/pulse/bracket_05.png".addFrame(%tab.pulsar);
    "platform/client/ui/pulse/bracket_06.png".addFrame(%tab.pulsar);
    "platform/client/ui/pulse/bracket_07.png".addFrame(%tab.pulsar);
    "platform/client/ui/pulse/bracket_08.png".addFrame(%tab.pulsar);
    "platform/client/ui/pulse/bracket_09.png".addFrame(%tab.pulsar);
    "platform/client/ui/pulse/bracket_10.png".addFrame(%tab.pulsar);
    "platform/client/ui/pulse/bracket_11.png".addFrame(%tab.pulsar);
    "platform/client/ui/pulse/bracket_12.png".addFrame(%tab.pulsar);
    "platform/client/ui/pulse/bracket_13.png".addFrame(%tab.pulsar);
    %tab.pulsar.setProfile();
    0.setVisible(%tab.pulsar);
    %pos = %tab.button.getPosition();
    ETSNonModalProfile;
    %xPos = (getWord(%pos, 0) - 4.0);
    %ypos = (getWord(%pos, 1) - 2.0);
    %ypos.reposition(%tab.pulsar, %xPos);
    %tab.pulseTimer = 0;
};
function HudTabs::update(%this) {
    Parent::update(%this);
    %numVisibleButtons = 0;
    %padding = %this.getPadding();
    %n = 0;
    while ((%n < %this.numTabs)) {
        if (%this.buttons.isVisible(%n)) {
            %height = (%height + (getWord(%this.buttons.getExtent(), 1) + %padding @ %n));
        }
        %n = (%n + 1.0);
    }
    %height = (%height - %padding);
    (%n < %this.numTabs);
    %width = getWord(HudTabsCollapsed.getExtent(), 0);
    %height.resize(HudTabsCollapsed, %width);
};
function HudTabs::pulseTab(%this, %tabObject) {
    if (!(isObject(%tabObject))) {
    }
    if (!(isObject(%tabObject.pulsar))) {
        error(getScopeName() @ "->invalid tab object or tab without pulsar object passed! returning!");
        return;
    }
    %this.pausePulseOnAllTabs();
    %tabObject.pulsar.add(HudContainer);
    %tabObject.pulsar.pushToBack(HudContainer);
    1.setVisible(%tabObject.pulsar);
    %tabObject.pulsar.start();
    cancel(%tabObject.pulseTimer);
    %tabObject.pulseTimer = %tabObject.schedule(%this, 4000, "pausePulseOnTab");
};
function HudTabs::pulseTabWithName(%this, %tabName) {
    %tabObject = 0;
    if (!(%tabName $= "")) {
        %tabObject = %tabName.getTabWithName(%this);
    }
    %tabObject.pulseTab(%this);
};
function HudTabs::pausePulseOnTab(%this, %tabObject) {
    cancel(%tabObject.pulseTimer);
    %tabObject.pulsar.stop();
    0.setCurrentFrame(%tabObject.pulsar);
};
function HudTabs::pausePulseOnAllTabs(%this) {
    %i = 0;
    while ((%i < %this.numTabs)) {
        %this.tabs.pausePulseOnTab(%this, %i);
        %i = (%i + 1.0);
    }
};
function HudTabs::stopPulseOnTab(%this, %tabObject) {
    cancel(%tabObject.pulseTimer);
    %tabObject.pulsar.stop();
    0.setVisible(%tabObject.pulsar);
};
function HudTabs::getPadding(%this) {
    return 0;
};
function HudTabs::close(%this) {
    %tab = %this.getCurrentTab();
    if (isObject(%tab)) {
    }
    if (isObject(%tab.content)) {
        %tab.content.onClose();
    }
    1.setVisible(HudTabsCollapsed);
    -(1.0).selectTabAtIndex(%this);
};
function HudTabs::onHiddenButton(%this) {
    Parent::onHiddenButton(%this);
    %this.close();
};
function HudTabs::hideOrShowTab(%this, %tabObject, %show) {
    if (%this.currentTabIsLockedOpen()) {
        Parent::hideOrShowTab(%this, %tabObject, %show);
        return;
    }
    if ((%this.getCurrentTab() == %tabObject)) {
        %this.close();
    }
    Parent::hideOrShowTab(%this, %tabObject, %show);
    if (isObject(%tabObject.pulsar) && %tabObject.pulsar.isVisible()) {
        %tabObject.stopPulseOnTab(%this);
    }
};
function HudTabs::setTabAtIndexVisible(%this, %tabIndex, %visible) {
    %tab = %this.tabs;
    %tabIndex;
    if (!(isObject(%tab))) {
        return;
    }
    %posX = getWord(%this.tabPosition, 0);
    %posY = getWord(%this.tabPosition, 1);
    if (%visible) {
        1.setVisible(%tab);
        %posY.setTrgPosition(%tab, %posX);
    }
    %posY.setTrgPosition(%tab, (%posX - getWord(%tab.getExtent(), 0)));
};
function HudTabs::autoHide(%this) {
    %currentTab = %this.getCurrentTab();
    if ((%currentTab $= "")) {
    }
    %tabName = %currentTab.name;
    "";
    if (!(%tabName[$UserPref::HudTabs::AutoClose @ %tabName])) {
        return;
    }
    if (%this.container.cursorInControl()) {
        2000.autoHideSchedule(%this);
        return;
    }
    %this.close();
    if (%currentTab.pulsar.isVisible()) {
        %currentTab.pausePulseOnTab(%this);
    }
};
function HudTabs::autoHideSchedule(%this, %ms) {
    cancel(%this.closeTimer);
    %this.closeTimer = "autoHide".schedule(%this, %ms);
};
function HudTabs::dontCloseNextTime(%this) {
    cancel(%this.closeTimer);
    %this.closeTimer = 0;
};
function HudTabs::tabSelected(%this, %tab) {
    if (isObject(%tab)) {
        0.setVisible(HudTabsCollapsed);
        $Pref::ETS::HudTabs::timeout.autoHideSchedule(%this);
        if (isObject(%tab.pulsar)) {
            %tab.stopPulseOnTab(%this);
        }
    }
    if (!(%tab.autoHide)) {
        %this.dontCloseNextTime();
    }
    %prevTab = %this.getPreviousTab();
    if ((%prevTab != %tab)) {
        if (isObject(%prevTab)) {
        }
        if (isObject(%prevTab.content)) {
            %prevTab.content.onClose();
            if (%prevTab.pulsar.isVisible()) {
                %prevTab.pausePulseOnTab(%this);
            }
        }
    }
};
function HudTabs::getInitialButtonOffset(%this) {
    return "46 0";
};
function HudTabs::CreateTab(%this, %name) {
    %tab = Parent::CreateTab(%this, %name);
    %tab.sluggishness = 0.5;
    getWord(%this.tabPosition, 1).reposition(%tab, (getWord(%this.tabPosition, 0) - getWord(%tab.getExtent(), 0)));
    return %tab;
};
function HudTabs::fillTabs(%this) {
    %i = 0;
    while ((%i < %this.numTabs)) {
        %tab = %this.tabs;
        %i @ HudTabs;
        %tab.setProfile();
        %tab.clear();
        %tab.button.tooltip = GuiDefaultProfile @ %tab.name;
        HudTabs;
        new GuiControl("") {
            profile = 0 @ "ETSDarkBoxProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 0";
            extent = 291 @ " " @ getWord(HudContainer, extent, 1);
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            maxLength = 255;
        };.add(%tab);
        if ((%tab.title $= "")) {
        }
        %title = %tab.title;
        %tab.name;
        %titleText = new GuiMLTextCtrl("") {
            profile = 0 @ "ETSHudHeadingProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "47 5";
            extent = "170 20";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            maxLength = 255;
            text = %title;
        };
        %tab.titleText = %titleText;
        %titleText.add(%tab);
        %closeButton = new GuiBitmapButtonCtrl("") {
            profile = 0 @ "GuiDefaultProfile";
            horizSizing = "left";
            vertSizing = "bottom";
            position = "270 5";
            extent = "16 16";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = "platform/client/buttons/gray_close";
            command = "HudTabs.overrideLockedOpen = true; HudTabs.close();";
        };
        %tab.closeButton = %closeButton;
        %closeButton.add(%tab);
        %content = new GuiControl("") {
            profile = 0 @ "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "47 35";
            extent = "235 180";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
        };
        %tab.content = %content;
        %content.add(%tab);
        %i = (%i + 1.0);
    }
    %this.filledPrivateSpaceTab = (%i < %tab.numTabs) @ 0;
    HudTabs;
    HudTabs.fillMusicTab();
    HudTabs.fillAffinityTab();
    HudTabs.fillScoresTab();
    HudTabs.fillWordTab();
    HudTabs.fillTutorialTab();
};
function HudTabs::fillMusicTab(%this) {
    %theTab = "music".getTabWithName(%this);
    "MusicHud".setName(%theTab.content);
    "MusicHud".bindClassName(%theTab.content);
    %theTab.toggleSoundTxt = new GuiMLTextCtrl(MusicTabToggleSoundTxt) {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "227 11";
        extent = "28 18";
        text = "(on)";
    };
    %theTab.toggleSoundTxt.updateText();
    %theTab.toggleSoundTxt.add(%theTab);
    new GuiBitmapButtonCtrl(MuteButton) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "199 6";
        extent = "22 22";
        minExtent = "22 22";
        sluggishness = -1;
        visible = 1;
        command = "Music::toggleMute();";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "platform/client/buttons/unmuted";
        drawText = 0;
    };.add(%theTab);
    station = "" @ MusicHud;
    new GuiControl(MusicHudBasicView) {
        profile = MusicHud @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = MusicHud.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };.add(new GuiMLTextCtrl(MusicText) {
        profile = "MusicMLTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "1 1";
        extent = "223 20";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
    };, new GuiScrollCtrl(MusicTextScroll) {
        profile = new GuiVariableWidthButtonCtrl(MusicHudMyMediaButton) {
        profile = new GuiVariableWidthButtonCtrl(MusicHudChangeStationButton) {
        profile = "BracketButton15Profile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "156 3";
        extent = "82 15";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        command = "MusicHud.setView(\"change_station\");";
        text = "Change Station";
        groupNum = -1;
        buttonType = "PushButton";
    }; @ "BracketButton15Profile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "1 164";
        extent = "122 15";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        command = "toggleCSPanel(CSMediaDisplay);";
        text = "My Music & Videos";
        groupNum = -1;
        buttonType = "PushButton";
    }; @ "ETSScrollSmallProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = "234 101";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 0;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        childMargin = "0 0";
    };);
    %ratingLabel = new GuiMLTextCtrl("") {
        profile = 0 @ "MusicRatingTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 20";
        extent = "150 17";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        lineSpacing = -2;
        allowColorChars = 1;
    };
    ratingControl = new GuiControl("") {
        profile = 0 @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "2 115";
        extent = "150 44";
        minExtent = "1 1";
        visible = 1;
        label = %ratingLabel;
    }; @ MusicHud
    "RatingControl".bindClassName(MusicHud, ratingControl);
    "MusicRatingControl".bindClassName(MusicHud, ratingControl);
    "platform/client/buttons/star".Initialize(MusicHud, ratingControl, 5, "19 19");
    %ratingLabel.add(MusicHud, ratingControl);
    ratingControl.add(MusicHudBasicView, MusicHud);
    new GuiControl(MusicHudEditView) {
        profile = MusicHud @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = MusicHud.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    };.add();
};
function MusicTabToggleSoundTxt::updateText(%this) {
    if ($UserPref::Audio::mute) {
        %soundTxt = "(off)";
    }
    %soundTxt = "(on)";
    %soundTxt.setText(%this);
};
function MusicRatingControl::updatePosition(%this) {
    %musicTextBottomY = ((getWord(MusicText.getExtent(), 1) + getWord(MusicTextScroll.getPosition(), 1)) + getWord(MusicText.getPosition(), 1));
    %mTScrollBottomY = (getWord(MusicTextScroll.getExtent(), 1) + getWord(MusicTextScroll.getPosition(), 1));
    %padding = 10;
    (mMin(%musicTextBottomY, %mTScrollBottomY) + %padding).reposition(%this, getWord(%this.getPosition, 0));
};
function HudTabs::fillAffinityTab(%this) {
    %theTab = "affinity".getTabWithName(%this);
    if (!(showPlayerInfoPopup())) {
        0.setVisible(%theTab.button);
    }
    "InfoPopupDlg".setName(%theTab.content);
    "InfoPopupDlg".bindClassName(%theTab.content);
    new GuiMLTextCtrl(InfoPopupNameField) {
        profile = "InfoWindowTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "122 8";
        extent = "150 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 1;
        allowColorChars = 0;
        maxChars = -1;
    };.add(%theTab);
    new GuiMLTextCtrl(InfoPopupContents) {
        profile = InfoPopupDlg @ "InfoWindowTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = "235 69";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
    };.add();
    new GuiMLTextCtrl(InfoPopupBottom) {
        profile = InfoPopupDlg @ "InfoWindowTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 164";
        extent = "235 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 1;
        allowColorChars = 0;
        maxChars = -1;
    };.add();
    new GuiScrollCtrl(InfoPopupTagsScroll) {
        profile = InfoPopupDlg @ "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "1 122";
        extent = "229 38";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        childMargin = "0 0";
    };.add();
    new GuiMLTextCtrl(InfoPopupTagsText) {
        profile = "InfoWindowTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = "220 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 1;
        allowColorChars = 0;
        maxChars = -1;
    };
    InfoPopupTagsText.add(InfoPopupTagsScroll);
    InfoPopupTagsScroll.add(InfoPopupDlg);
    InfoPopupDlg.init();
};
function HudTabs::fillScoresTab(%this) {
    %theTab = "scores".getTabWithName(%this);
    "HudScoresContent".setName(%theTab.content);
    "HudScoresContent".bindClassName(%theTab.content);
    %ypos = 2;
    %fieldx = 110;
    HudScoresContent;
    new GuiMLTextCtrl("") {
        profile = 0 @ "HudScoresLabelTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " " @ %ypos;
        extent = "76 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "<b>Level:";
        maxLength = 64;
    };.add();
    respektLevelLabel = new GuiMLTextCtrl("") {
        profile = 0 @ "InfoWindowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %fieldx @ " " @ %ypos;
        extent = "134 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "1 - Freshman";
        maxLength = 64;
    }; @ HudScoresContent
    respektLevelLabel.add(HudScoresContent, HudScoresContent);
    HudScoresContent;
    %ypos = (%ypos + 20.0);
    new GuiMLTextCtrl("") {
        profile = 0 @ "HudScoresLabelTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "85 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "<b>Next Level:";
        maxLength = 64;
    };.add();
    if (!(isObject(HudScoresPBController))) {
        new ScriptObject(HudScoresPBController) {
            class = "ProgressBarController";
        };
        if (isObject(MissionCleanup)) {
            HudScoresPBController.add(MissionCleanup);
        }
    }
    respektBarContainer = new GuiControl("") {
        profile = 0 @ "ETSRespektLevelPBProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = (%fieldx - 2.0) @ " " @ (%ypos + 2.0);
        extent = "125 14";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    }; @ HudScoresContent
    respektBarHolder = new GuiControl("") {
        profile = 0 @ "BlankProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = "1 1";
        extent = "114 13";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    }; @ HudScoresContent
    respektBarHolder.add(HudScoresContent, respektBarContainer, HudScoresContent);
    respektBarContainer.add(HudScoresContent, HudScoresContent);
    "".Initialize(HudScoresPBController, HudScoresContent, respektBarHolder, "", "platform/client/ui/respektprogress_fill", "");
    HudScoresContent;
    %ypos = (%ypos + 20.0);
    new GuiMLTextCtrl("") {
        profile = 0 @ "HudScoresLabelTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "76 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "<b><a:gamelink " @ $Net::HelpURL_VPoints @ "?section=vPoints>All-time <bitmap:platform/client/ui/vpoints_14></a>:";
        maxLength = 64;
    };.add();
    respektScoreLabel = new GuiMLTextCtrl("") {
        profile = 0 @ "InfoWindowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %fieldx @ " " @ %ypos;
        extent = "100 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 64;
    }; @ HudScoresContent
    respektScoreLabel.add(HudScoresContent, HudScoresContent);
    if (0) {
        HudScoresContent;
        %ypos = (%ypos + 20.0);
        new GuiMLTextCtrl("") {
            profile = 0 @ "HudScoresLabelTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = 0 @ " ";
            extent = "76 18";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            text = "<b>Rank:";
            maxLength = 64;
        };.add();
        respektRankLabel = new GuiMLTextCtrl("") {
            profile = 0 @ "InfoWindowTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %fieldx @ " " @ %ypos;
            extent = "68 18";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            text = "#1";
            maxLength = 64;
        }; @ HudScoresContent
        respektRankLabel.add(HudScoresContent, HudScoresContent);
    }
    HudScoresContent;
    %ypos = (%ypos + 18.0);
    new GuiMLTextCtrl("") {
        profile = 0 @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "100 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "<b>Collections:";
        maxLength = 64;
    };.add();
    %ypos = (%ypos + 20.0);
    collectionsScroll = new GuiScrollCtrl("") {
        profile = 0 @ "ETSInviteMessageScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = 1 @ " ";
        extent = "233 99";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        childMargin = "2 2";
        saneDrag = 1;
        scrollMultiplier = 1;
        stickyBottom = 0;
    }; @ HudScoresContent
    collectionsList = new GuiMLTextCtrl("") {
        profile = 0 @ "InfoWindowTextListProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = "218 99";
        minExtent = "80 60";
        sluggishness = -1;
        visible = 1;
        command = "";
        altCommand = "";
        enumerate = 1;
        resizeCell = 1;
        fitParentWidth = 1;
        clipColumnText = 1;
    }; @ HudScoresContent
    collectionsList.add(HudScoresContent, collectionsScroll, HudScoresContent);
    collectionsScroll.add(HudScoresContent, HudScoresContent);
    if (!(isObject(HudScoresContent, collectionsSet))) {
        collectionsSet = new SimSet(ScoresHudCollectionsSet); @ HudScoresContent;
    }
    previousRespektPoints = 0 @ HudScoresContent;
    0.setRespektPoints(HudScoresContent, 0);
};
function HudScoresContent::setRespektPoints(%this, %points, %notify) {
    %points.setText(%this.respektScoreLabel);
    %level = respektScoreToLevel(%points);
    %levelName = respektLevelToNameWithoutArticle(%level);
    %level @ " - " @ %levelName.setText(%this.respektLevelLabel);
    (1.0 - respektPercentToNextLevel(%points)).setValue(HudScoresPBController);
    %levelPrev = respektScoreToLevel(%this.previousRespektPoints);
    if ((%level != %levelPrev)) {
    }
    if ((%this.previousRespektPoints != 0.0)) {
        if ((%level > %levelPrev)) {
            alxPlay(AudioRespektLevelGained);
        }
        if ((%level == 1.0)) {
            %code = "LEVELCHANGE1";
        }
        if ((%level == 2.0)) {
            %code = "LEVELCHANGE2";
        }
        %code = "LEVELCHANGE";
        schedule(5000, 0, "respektHandle", "", %points, (%points - %this.previousRespektPoints), %code, 0, 1);
        "scores".schedule(HudTabs, 5100, "pulseTabWithName");
    }
    if (%notify) {
    }
    if ((%points != %this.previousRespektPoints)) {
        "scores".pulseTabWithName(HudTabs);
    }
    %this.previousRespektPoints = %points;
};
function HudScoresContent::setRespektRank(%this, %rank) {
    if ((%rank $= "")) {
        %text = "(unknown)";
    }
    %text = "#" @ %rank;
    if (isObject(%this.respektRankLabel)) {
        %text.setText(%this.respektRankLabel);
    }
    if ((%rank != %this.previousRespektRank)) {
        "scores".pulseTabWithName(HudTabs);
    }
    %this.previousRespektRank = %rank;
};
function HudScoresContent::clearCollections(%this) {
    %count = %this.collectionsSet.getCount();
    %n = (%count - 1.0);
    while ((%n >= 0.0)) {
        %collection = %n.getObject(%this.collectionsSet);
        %collection.remove(%this.collectionsSet);
        %collection.delete();
        %n = (%n - 1.0);
    }
    %this.refreshCollections();
};
function clientCmdSetCollectionStatus(%name, %sofar, %total) {
    %total.setCollectionStatus(HudScoresContent, %name, %sofar);
};
function HudScoresContent::setCollectionStatus(%this, %name, %sofar, %total) {
    %ourCopy = %name.getCollectionObject(%this);
    if (!(isObject(%ourCopy))) {
        if ((%total == 0.0)) {
        }
        if ((%sofar == 0.0)) {
            return;
        }
        %ourCopy = new ScriptObject("") {
            name = 0 @ %name;
        };
        if (isObject(MissionCleanup)) {
            %ourCopy.add(MissionCleanup);
        }
        %ourCopy.add(%this.collectionsSet);
    }
    if ((%total == 0.0)) {
    }
    if ((%sofar == 0.0)) {
        %ourCopy.remove(%this.collectionsSet);
        %ourCopy.delete();
    }
    %ourCopy.sofar = %sofar;
    %ourCopy.total = %total;
    %this.refreshCollections();
};
function HudScoresContent::getCollectionObject(%this, %name) {
    %n = (%this.collectionsSet.getCount() - 1.0);
    while ((%n >= 0.0)) {
        %cur = %n.getObject(%this.collectionsSet);
        if ((%name $= %cur.name)) {
            return %cur;
        }
        %n = (%n - 1.0);
    }
    return -(1.0);
};
function HudScoresContent::refreshCollections(%this) {
    "".setText(%this.collectionsList);
    %count = %this.collectionsSet.getCount();
    %stringToSort = "";
    %completed = "";
    %i = (%count - 1.0);
    while ((%i >= 0.0)) {
        %collection = %i.getObject(%this.collectionsSet);
        %ratio = (%collection.sofar / %collection.total);
        if ((%ratio != 1.0)) {
            %append = formatFloat("%1.3f", %ratio) @ "\t" @ %collection.getId();
            if (!(%stringToSort $= "")) {
                %stringToSort = %stringToSort @ " " @ %append;
            }
            %stringToSort = %append;
        }
        if (!(%completed $= "")) {
            %completed = %completed @ " " @ %collection.getId();
        }
        %completed = %collection.getId();
        %i = (%i - 1.0);
    }
    %stringToSort = SortWords(%stringToSort);
    (%i >= 0.0);
    %count = getFieldCount(%stringToSort);
    0.addText(%this.collectionsList, "<spush><just:left><b> In Progress:<spop><br>");
    %i = (%count - 1.0);
    while ((%i > 0.0)) {
        %collection = getWord(getField(%stringToSort, %i), 0);
        0.addText(%this.collectionsList, "<spush><just:left>   " @ %collection.name @ " " @ "<just:right>(" @ %collection.sofar @ "/" @ %collection.total @ ")<spop><br>");
        %i = (%i - 1.0);
    }
    if (((%i > 0.0) @ " " @ %completed $= "")) {
        return;
    }
    0.addText(%this.collectionsList, "<spush><just:left><b> Completed:<spop><br>");
    %count = getWordCount(%completed);
    %i = 0;
    while ((%i < %count)) {
        %collection = getWord(%completed, %i);
        0.addText(%this.collectionsList, "<spush><just:left>   " @ %collection.name @ " " @ "<just:right>(" @ %collection.sofar @ "/" @ %collection.total @ ")<spop><br>");
        %i = (%i + 1.0);
    }
    if (%this.collectionsList.isAwake()) {
        %this.collectionsList.forceReflow();
    }
};
function HudScoresContent::onClose(%this) {
};
function HudTabs::fillWordTab(%this) {
    %theTab = "word".getTabWithName(%this);
    "SystemMessageDialog".setName(%theTab.content);
    "SystemMessageDialog".bindClassName(%theTab.content);
    new GuiScrollCtrl(SystemMessageScrollCtrl) {
        profile = SystemMessageDialog @ "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "3 0";
        extent = "229 180";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        childMargin = "0 0";
    };.add(new GuiMLTextCtrl(SystemMessageTextCtrl) {
        profile = "SystemMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "1 1";
        extent = "220 180";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        DefaultMessage = "<spush><b><color:ffffffff>This space tells you when you get friend invites, vPoint awards, system messages, etc..<spop>";
    };);
    DefaultMessage.setText(SystemMessageTextCtrl, SystemMessageTextCtrl);
};
function HudTabs::fillPrivateSpaceTab(%this) {
    "private space".hideTabWithName(HudTabs);
    %theTab = "private space".getTabWithName(%this);
    "PrivSpaceHud".setName(%theTab.content);
    "PrivSpaceHud".bindClassName(%theTab.content);
    toggleOP = new GuiMLTextCtrl(PrivSpaceHudToggleOP) {
        profile = "ETSShadowTextProfile";
        extent = "68 18";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "187 11";
        extent = "80 25";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        text = "";
        maxLength = 64;
    }; @ PrivSpaceHud
    toggleOP.add(%theTab, PrivSpaceHud);
    new GuiControl(OPSpaceHud) {
        profile = PrivSpaceHud @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = PrivSpaceHud.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };.add();
    new GuiControl(NonOPSpaceHud) {
        profile = PrivSpaceHud @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = PrivSpaceHud.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    };.add();
    OPSpaceHud.setup();
    NonOPSpaceHud.setup();
    PrivSpaceHud.hideOP();
    new GuiMLTextCtrl(SpaceSurfText) {
        profile = "InfoWindowTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "207 199";
        extent = "50 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        text = "<a:spaceP> << </a><a:spaceN> >> </a>";
        lineSpacing = 1;
        allowColorChars = 0;
        maxChars = -1;
    };
    %theTab.add();
    new GuiTextEditCtrl(SpaceSurfTE) {
        profile = SpaceSurfText @ "InfoWindowTextEditProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "261 200";
        extent = "25 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        altCommand = "SpaceSurfTE.onEnter();";
        maxLength = 32;
        historySize = 1;
        password = 0;
        tabComplete = 0;
        sinkAllKeyEvents = 0;
    };
    %theTab.add();
    %this.filledPrivateSpaceTab = SpaceSurfTE @ 1;
};
function PrivSpaceHud::onClose(%this) {
    OPSpaceHud.descriptionChanged();
};
function PrivSpaceHudToggleOP::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "OPon")) {
        PrivSpaceHud.showOP();
    }
    if ((getWord(%url, 0) $= "OPoff")) {
        PrivSpaceHud.hideOP();
    }
    error("Url in PrivSpaceHud.toggleOP is broken.<-" @ getScopeName());
};
function PrivSpaceHud::enableOPlink(%this) {
    1.setVisible(%this.toggleOP);
};
function PrivSpaceHud::disableOPlink(%this) {
    0.setVisible(%this.toggleOP);
};
function PrivSpaceHud::showOP(%this) {
    1.setVisible(OPSpaceHud);
    0.setVisible(NonOPSpaceHud);
    "<a:OPoff >(guest view)</a>".setText(%this.toggleOP);
    if ((HudTabs.getCurrentTab().name $= "private space")) {
        HudTabs.dontCloseNextTime();
    }
    CSControlPanel.open();
};
function PrivSpaceHud::hideOP(%this) {
    0.setVisible(OPSpaceHud);
    1.setVisible(NonOPSpaceHud);
    "<a:OPon >(host's view)</a>".setText(%this.toggleOP);
    if ((HudTabs.getCurrentTab().name $= "private space")) {
        $Pref::ETS::HudTabs::timeout.autoHideSchedule(HudTabs);
        OPSpaceHud.descriptionChanged();
    }
    CSControlPanel.close();
};
function PrivSpaceHud::updateMusic(%this, %newStreamID) {
    if (isObject($musicStreamIDMap)) {
        %newStreamName = %newStreamID.get($musicStreamIDMap);
        if ((%newStreamName $= "")) {
            error("Stream ID (\"" @ %newStreamID @ "\") not in music stream id -> name mapping! <-" @ getScopeName());
            %newStreamName = %newStreamID;
        }
    }
    warn("the music stream ID map was not initialized.  This should have been done in GameConnection::etsInit()");
    %newStreamName = %newStreamID;
    %newStreamName.updateMusic(OPSpaceHud);
    %newStreamName.updateMusic(NonOPSpaceHud);
};
function clientCmdPrivSpaceHudUpdateMusic(%newStreamID) {
};
function clientCmdPrivSpaceHudUpdateVideo(%unused) {
};
function OPSpaceHud::setup(%this) {
    %ypos = 0;
    new GuiTextCtrl("") {
        profile = 0 @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " " @ %ypos;
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Name: ";
        maxLength = 64;
    };.add(%this);
    %this.spaceNameField = new GuiTextCtrl("") {
        profile = 0 @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 60 @ " " @ %ypos;
        extent = "155 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 64;
    };
    %this.spaceNameField.add(%this);
    %ypos = (%ypos + 22.0);
    new GuiTextCtrl("") {
        profile = 0 @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "100 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Description: ";
        maxLength = 64;
    };.add(%this);
    %ypos = (%ypos + 20.0);
    %this.spaceDescField = new GuiTextEditCtrl("") {
        profile = 0 @ "InfoWindowTextEditProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "215 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "text";
        altCommand = "OPSpaceHud.descriptionChanged();";
        maxLength = 32;
        historySize = 1;
        password = 0;
        tabComplete = 0;
        sinkAllKeyEvents = 0;
    };
    %this.spaceDescField.add(%this);
    %ypos = (%ypos + 27.0);
    new GuiTextCtrl("") {
        profile = 0 @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Access:";
        maxLength = 64;
    };.add(%this);
    %ypos = (%ypos + 20.0);
    %this.AccessOptAnyone = new GuiRadioCtrl("") {
        profile = 0 @ "InfoWindowRadioButtonProfile";
        groupNum = 1;
        buttonType = "RadioButton";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Open";
        command = "OPSpaceHud.accessSelected(\"Open\");";
        maxLength = 64;
    };
    %this.AccessOptAnyone.add(%this);
    %ypos = (%ypos + 20.0);
    %this.AccessOptFriends = new GuiRadioCtrl("") {
        profile = 0 @ "InfoWindowRadioButtonProfile";
        groupNum = 1;
        buttonType = "RadioButton";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "120 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Friends Only";
        command = "OPSpaceHud.accessSelected(\"FriendsOnly\");";
        maxLength = 64;
    };
    %this.AccessOptFriends.add(%this);
    %ypos = (%ypos + 20.0);
    new GuiTextCtrl("") {
        profile = 0 @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Music: ";
        maxLength = 64;
    };.add(%this);
    %ypos = (%ypos + 20.0);
    %this.MusicStreamDropdown = new GuiPopUp2MenuCtrl("") {
        profile = 0 @ "InfoWindowPopupProfile";
        scrollProfile = "DottedScrollProfile";
        winProfile = "InfoWindowPopupWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "200 30";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "OPSpaceHud.MusicSelected();";
        text = "";
        maxLength = 255;
        maxPopupHeight = 200;
        allowReverse = 0;
    };
    %this.MusicStreamDropdown.add(%this);
};
function OPSpaceHud::accessSelected(%this, %accessLevel) {
    if (!(%accessLevel $= %this.accessLevel)) {
        CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), "", %accessLevel, "", "");
    }
    %this.accessLevel = %accessLevel;
};
function OPSpaceHud::descriptionChanged(%this) {
    %description = %this.spaceDescField.getValue();
    if (!(%this.description $= %description)) {
        CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), %description, "", "", "");
    }
    %this.description = %description;
};
function OPSpaceHud::MusicSelected(%this) {
    %selection = %this.MusicStreamDropdown.getText();
    if ((%selection $= "")) {
        error("Empty string was selected on MusicStreamDropdown. Not sending request. Check that the server sent StreamID was in the $musicStreamIDMap checked by PrivSpaceHud::updateMusic <-" @ getScopeName());
        return;
    }
    if (!(%selection $= %this.musicStream)) {
        if (isObject($musicStreamNameMap)) {
            log("communication", "debug", "getting the stream name from the musicStreamMap which is" @ " " @ $musicStreamNameMap);
            %streamID = %selection.get($musicStreamNameMap);
        }
        warn("the MusicStreamMap variable is not defined. We cannot get the music stream mapping.. so using PrivateSpace (the default)");
        %streamID = "PrivateSpace";
        echo("setting music stream id = " @ %streamID);
        customSpace::SetMusicStreamID(%streamID);
    }
    %this.musicStream = %selection;
};
function OPSpaceHud::updateMusic(%this, %newStreamName) {
    if ((%newStreamName $= "")) {
        warn(getScopeName() @ "-> received an empty string for stream name.");
    }
    %this.musicStream = %newStreamName;
    if ((%this.MusicStreamDropdown.size() != 0.0)) {
        %index = %newStreamName.findText(%this.MusicStreamDropdown);
        if ((%index < 0.0)) {
            warn("Some music streams are loaded, but the latest update is not in the dropdown!<-" @ getScopeName());
        }
        %index.SetSelected(%this.MusicStreamDropdown);
    }
    warn("Tried to set selected music stream on updating OPSpaceHud settings, but the music wasn't loaded!<-" @ getScopeName());
};
function OPSpaceHud::updateStreams(%this, %streamList) {
    %streamList.fillFromList(%this.MusicStreamDropdown);
    %selectedIndex = 0;
    if (!(%this.musicStream $= "")) {
        %selectedIndex = %this.musicStream.findText(%this.MusicStreamDropdown);
    }
    %selectedIndex.SetSelected(%this.MusicStreamDropdown);
};
function OPSpaceHud::updateSettings(%this, %name, %description, %accessMode) {
    %this.description = %description;
    %description.setText(%this.spaceDescField);
    %this.accessLevel = %accessMode;
    if ((%accessMode $= "Open")) {
        %this.AccessOptAnyone.performClick();
    }
    if ((%accessMode $= "FriendsOnly")) {
        %this.AccessOptFriends.performClick();
    }
    %this.accessLevel = "Open";
    %this.AccessOptAnyone.performClick();
    %name.setText(%this.spaceNameField);
};
function NonOPSpaceHud::setup(%this) {
    %ypos = 0;
    new GuiTextCtrl("") {
        profile = 0 @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " " @ %ypos;
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Name: ";
        maxLength = 64;
    };.add(%this);
    %this.spaceNameField = new GuiMLTextCtrl("") {
        profile = 0 @ "InfoWindowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 60 @ " " @ %ypos;
        extent = "170 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 64;
    };
    %this.spaceNameField.add(%this);
    %ypos = (%ypos + 20.0);
    new GuiTextCtrl("") {
        profile = 0 @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Host: ";
        maxLength = 64;
    };.add(%this);
    %this.spaceOwnerField = new GuiMLTextCtrl(NonOPSpaceHudOwnerField) {
        profile = "InfoWindowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 60 @ " " @ %ypos;
        extent = "170 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 64;
    };
    %this.spaceOwnerField.add(%this);
    %ypos = (%ypos + 35.0);
    new GuiTextCtrl("") {
        profile = 0 @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "85 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Description: ";
        maxLength = 64;
    };.add(%this);
    %ypos = (%ypos + 20.0);
    %this.spaceDescField = new GuiTextCtrl("") {
        profile = 0 @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "215 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "a description";
        maxLength = 64;
    };
    %this.spaceDescField.add(%this);
    %ypos = (%ypos + 40.0);
    new GuiTextCtrl("") {
        profile = 0 @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Music: ";
        maxLength = 64;
    };.add(%this);
    %ypos = (%ypos + 20.0);
    %this.musicStreamField = new GuiTextCtrl("") {
        profile = 0 @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "215 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "My apartment radio ";
        maxLength = 64;
    };
    %this.musicStreamField.add(%this);
    %ypos = (%ypos + 30.0);
    %this.bigMLText = new GuiMLTextCtrl("") {
        profile = 0 @ "InfoWindowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "215 200";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 64;
    };
    %this.bigMLText.add(%this);
};
function NonOPSpaceHud::updateSettings(%this, %name, %description, %owner) {
    if (!(%owner $= "")) {
        "<a:owner " @ munge(%owner) @ ">" @ %owner @ "</a>".setText(%this.spaceOwnerField);
    }
    "<a:noowner >Take Control</a>".setText(%this.spaceOwnerField);
    %description.setText(%this.spaceDescField);
    %name.setText(%this.spaceNameField);
};
function NonOPSpaceHud::updateMusic(%this, %newStreamName) {
    %newStreamName.setText(%this.musicStreamField);
};
function GuiPopUp2MenuCtrl::fillFromList(%this, %list) {
    %this.clear();
    %count = getFieldCount(%list);
    %i = 0;
    while ((%i < %count)) {
        getField(%list, %i).add(%this);
        %i = (%i + 1.0);
    }
};
function NonOPSpaceHudOwnerField::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "noowner")) {
        CustomSpaceSettings::changeSpaceOwnership(CustomSpaceClient::GetSpaceImIn(), 1);
    }
    if ((getWord(%url, 0) $= "owner")) {
        onLeftClickPlayerName(unmunge(getWords(%url, 1)), "");
    }
};
function NonOPSpaceHudOwnerField::onRightURL(%this, %url) {
    if ((getWord(%url, 0) $= "noowner")) {
    }
    if ((getWord(%url, 0) $= "owner")) {
        onRightClickPlayerName(unmunge(getWords(%url, 1)));
    }
};
function CustomSpaceSettings::saveSettings(%spaceName, %description, %accessMode, %password, %audioStream, %videoStream) {
    if (!(isObject($CSSpaceInfo))) {
    }
    if (!($CSSpaceInfo.owner $= $Player::Name)) {
        return;
    }
    if (!(%description $= "")) {
        $CSSpaceInfo.description = %description;
    }
    if (!(%accessMode $= "")) {
        $CSSpaceInfo.access = %accessMode;
    }
    if ($StandAlone) {
        echo(getScopeName() @ " " @ "pretending success using standalone");
        return;
    }
    if (!(haveValidManagerHost())) {
    }
    if (!(haveValidToken())) {
        echo(getScopeName() @ " " @ "No valid manager host or token.");
        return;
    }
    %request = new ManagerRequest("") {
        className = 0 @ "ModifyCustomSpaceSettingsRequest";
    };
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    %url = $Net::ClientServiceURL @ "/SaveCustomSpaceSettings?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "space=" @ urlEncode(%spaceName);
    if (!(%description $= "")) {
        %url = %url @ "&description=" @ urlEncode(%description);
    }
    if (!(%accessMode $= "")) {
        %url = %url @ "&accessMode=" @ urlEncode(%accessMode);
    }
    if (!(%password $= "")) {
        %url = %url @ "&password=" @ urlEncode(%password);
    }
    if (!(%audioStream $= "")) {
        %url = %url @ "&audioStream=" @ urlEncode(%audioStream);
    }
    if (!(%videoStream $= "")) {
        %url = %url @ "&videoStream=" @ urlEncode(%videoStream);
    }
    log("network", "info", getScopeName() @ ":" @ %url);
    %url.setURL(%request);
    %request.start();
    %request.requestDescription = %description;
    %request.requestAccessMode = %accessMode;
    if (!(%request.requestDescription $= "")) {
        CSRulesDescSavedIndicator.incrementRequestCount();
    }
    if (!(%request.requestAccessMode $= "")) {
        CSRulesPasswordSavedIndicator.incrementRequestCount();
    }
};
function ModifyCustomSpaceSettingsRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() @ ":" @ %status);
    if ((%status $= "fail")) {
        warn("network", getScopeName() @ " request failed: " @ "statusMessage".getValue(%this));
    }
    "delete".schedule(%this, 0);
    if (!(%this.requestDescription $= "")) {
        CSRulesDescSavedIndicator.decrementRequestCount();
    }
    if (!(%this.requestAccessMode $= "")) {
        CSRulesPasswordSavedIndicator.decrementRequestCount();
    }
};
function ModifyCustomSpaceSettingsRequest::onError(%this, %unused, %errMsg) {
    error("network", getScopeName() @ ":" @ %errMsg);
    "delete".schedule(%this, 0);
};
function CustomSpaceSettings::changeSpaceOwnership(%spaceName, %takeOwnershipBool) {
    %request = new ManagerRequest("") {
        className = 0 @ "ChangeSpaceOwnershipRequest";
    };
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    %url = $Net::BaseURL @ "?cmd=ChangeSpaceOwnership" @ "&user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token) @ "&space=" @ urlEncode(%spaceName) @ "&take=" @ urlEncode(%takeOwnershipBool ? 1 : 0);
    log("network", "info", getScopeName() @ ":" @ %url);
    %url.setURL(%request);
    %request.spaceName = %spaceName;
    %request.start();
};
function ChangeSpaceOwnershipRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    %statusMsg = "statusMsg".getValue(%this);
    log("network", "info", getScopeName() @ ":" @ %status @ " - msg: " @ %statusMsg);
    if ((%status $= "success")) {
        "private space".selectTabWithName(HudTabs);
    }
    if ((trim(getWords(%statusMsg, 0, 1)) $= "fail already-owned")) {
        handleSystemMessage("msgInfoMessage", "Sorry, the space is already owned by someone else.");
    }
    if ((trim(getWords(%statusMsg, 0, 1)) $= "fail respekt")) {
        handleSystemMessage("msgInfoMessage", "Sorry, you must be at least a " @ getWord(%statusMsg, 2) @ " to own this space.");
    }
    handleSystemMessage("msgInfoMessage", "Sorry, you couldn't change the ownership of the space.");
    "delete".schedule(%this, 0);
};
function CustomSpaceSettings::onError(%this, %unused, %errMsg) {
    error("network", getScopeName() @ ":" @ %errMsg);
    "delete".schedule(%this, 0);
};
function HudTabs::addPermissionBasedContent(%this) {
    if (!(%this.filledPrivateSpaceTab)) {
        return;
    }
    %hasPerm = "fly".rolesPermissionCheckNoWarn($player);
    %hasPerm.setVisible(SpaceSurfText);
    %hasPerm.setVisible(SpaceSurfTE);
    if (%hasPerm) {
    }
    if (($gMode $= "PrivateSpaceGrid")) {
        "private space".showTabWithName(%this);
    }
};
function SpaceSurfText::onURL(%this, %url) {
    teleportToAdjacentSpace((%url $= "spaceN"));
};
function SpaceSurfTE::onEnter(%this) {
    teleportToSpaceNumber(%this.getValue());
};
