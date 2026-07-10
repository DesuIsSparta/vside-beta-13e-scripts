function HudTabs::setup() {
    if (!(isObject(HudTabs))) {
        new ScriptObject(HudTabs) {
            class = "TabControl";
        };
        if (isObject(MissionCleanup)) {
            MissionCleanup.add(HudTabs);
        }
        "46 39".Initialize("", "", "vertical");
        "music".newTab("platform/client/buttons/hud_music", "music & videos");
        "affinity".newTab("platform/client/buttons/hud_affinity", "");
        "scores".newTab("platform/client/buttons/hud_scores", "");
        "word".newTab("platform/client/buttons/hud_word", "");
        "tutorial".newTab("platform/client/buttons/hud_tutorials", "");
        HudTabs.fillTabs(HudTabs);
        HudTabs.close(HudTabs);
        closeTimer = 0 @ HudTabs;
        HudTabs;
    }
};
function HudTabs::newTab(%this, %name, %bitmapName, %title) {
    if (!(isDefined("%title"))) {
        %title = "";
    }
    %tab = Parent::newTab(%this, %name, %bitmapName);
    %tab.title = %title;
    %tab.pulsar = AnimCtrl::newAnimCtrl("0 0", "54 43");
    %tab.pulsar.setDelay(60);
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_00.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_01.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_02.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_03.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_04.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_05.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_06.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_07.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_08.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_09.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_10.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_11.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_12.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_13.png");
    %tab.pulsar.setProfile();
    %tab.pulsar.setVisible(0);
    %pos = %tab.button.getPosition();
    ETSNonModalProfile;
    %xPos = (4.0 - getWord(%pos, 0));
    %ypos = (2.0 - getWord(%pos, 1));
    %tab.pulsar.reposition(%xPos, %ypos);
    %tab.pulseTimer = 0;
};
function HudTabs::update(%this) {
    Parent::update(%this);
    %numVisibleButtons = 0;
    %padding = %this.getPadding();
    %n = 0;
    if ((%this.numTabs < %n)) {
        if (%this.buttons.isVisible()) {
            %height = ((%padding @ %n + getWord(%this.buttons.getExtent(), 1)) + %height);
            %n;
        }
        %n = (1.0 + %n);
    }
    %height = (%padding - %height);
    (%this.numTabs < %n);
    %width = getWord(HudTabsCollapsed.getExtent(), 0);
    %width.resize(%height);
};
function HudTabs::pulseTab(%this, %tabObject) {
    if (!(isObject(%tabObject))) {
    }
    if (!(isObject(%tabObject.pulsar))) {
        error(getScopeName() @ "->invalid tab object or tab without pulsar object passed! returning!");
        return;
    }
    %this.pausePulseOnAllTabs();
    %tabObject.pulsar.add();
    %tabObject.pulsar.pushToBack();
    %tabObject.pulsar.setVisible(1);
    %tabObject.pulsar.start();
    cancel(%tabObject.pulseTimer);
    %tabObject.pulseTimer = HudContainer @ %this.schedule(4000, "pausePulseOnTab", %tabObject);
    HudContainer;
};
function HudTabs::pulseTabWithName(%this, %tabName) {
    %tabObject = 0;
    if (!(%tabName $= "")) {
        %tabObject = %this.getTabWithName(%tabName);
    }
    %this.pulseTab(%tabObject);
};
function HudTabs::pausePulseOnTab(%this, %tabObject) {
    cancel(%tabObject.pulseTimer);
    %tabObject.pulsar.stop();
    %tabObject.pulsar.setCurrentFrame(0);
};
function HudTabs::pausePulseOnAllTabs(%this) {
    %i = 0;
    if ((%this.numTabs < %i)) {
        %this.pausePulseOnTab(%this.tabs);
        %i = (1.0 + %i);
        %i;
    }
};
function HudTabs::stopPulseOnTab(%this, %tabObject) {
    cancel(%tabObject.pulseTimer);
    %tabObject.pulsar.stop();
    %tabObject.pulsar.setVisible(0);
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
    1.setVisible();
    %this.selectTabAtIndex(-(1.0));
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
    if ((%tabObject == %this.getCurrentTab())) {
        %this.close();
    }
    Parent::hideOrShowTab(%this, %tabObject, %show);
    if (isObject(%tabObject.pulsar)) {
        if (%tabObject.pulsar.isVisible()) {
            %this.stopPulseOnTab(%tabObject);
        }
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
        %tab.setVisible(1);
        %tab.setTrgPosition(%posX, %posY);
    }
    %tab.setTrgPosition((getWord(%tab.getExtent(), 0) - %posX), %posY);
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
        %this.autoHideSchedule(2000);
        return;
    }
    %this.close();
    if (%currentTab.pulsar.isVisible()) {
        %this.pausePulseOnTab(%currentTab);
    }
};
function HudTabs::autoHideSchedule(%this, %ms) {
    cancel(%this.closeTimer);
    %this.closeTimer = %this.schedule(%ms, "autoHide");
};
function HudTabs::dontCloseNextTime(%this) {
    cancel(%this.closeTimer);
    %this.closeTimer = 0;
};
function HudTabs::tabSelected(%this, %tab) {
    if (isObject(%tab)) {
        0.setVisible();
        %this.autoHideSchedule($Pref::ETS::HudTabs::timeout);
        if (isObject(%tab.pulsar)) {
            %this.stopPulseOnTab(%tab);
        }
    }
    if (!(%tab.autoHide)) {
        %this.dontCloseNextTime();
    }
    %prevTab = %this.getPreviousTab();
    HudTabsCollapsed;
    if ((%tab != %prevTab)) {
        if (isObject(%prevTab)) {
        }
        if (isObject(%prevTab.content)) {
            %prevTab.content.onClose();
            if (%prevTab.pulsar.isVisible()) {
                %this.pausePulseOnTab(%prevTab);
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
    %tab.reposition((getWord(%tab.getExtent(), 0) - getWord(%this.tabPosition, 0)), getWord(%this.tabPosition, 1));
    return %tab;
};
function HudTabs::fillTabs(%this) {
    %i = 0;
    if ((%this.numTabs < %i)) {
        %tab = %this.tabs;
        %i @ HudTabs;
        %tab.setProfile();
        %tab.clear();
        %tab.button.tooltip = GuiDefaultProfile @ %tab.name;
        HudTabs;
        0;
        %tab.add(new ""() {
            profile = GuiControl @ "ETSDarkBoxProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 0";
            extent = 291 @ " " @ HudContainer @ getWord(extent, 1);
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            maxLength = 255;
        };);
        if ((%tab.title $= "")) {
        }
        %title = %tab.title;
        %tab.name;
        0;
        %titleText = new ""() {
            profile = GuiMLTextCtrl @ "ETSHudHeadingProfile";
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
        %tab.add(%titleText);
        0;
        %closeButton = new ""() {
            profile = GuiBitmapButtonCtrl @ "GuiDefaultProfile";
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
        %tab.add(%closeButton);
        0;
        %content = new ""() {
            profile = GuiControl @ "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "47 35";
            extent = "235 180";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
        };
        %tab.content = %content;
        %tab.add(%content);
        %i = (1.0 + %i);
    }
    %this.filledPrivateSpaceTab = (%tab.numTabs < %i) @ 0;
    HudTabs;
    HudTabs.fillMusicTab();
    HudTabs.fillAffinityTab();
    HudTabs.fillScoresTab();
    HudTabs.fillWordTab();
    HudTabs.fillTutorialTab();
};
function HudTabs::fillMusicTab(%this) {
    %theTab = %this.getTabWithName("music");
    %theTab.content.setName("MusicHud");
    %theTab.content.bindClassName("MusicHud");
    %theTab.toggleSoundTxt = new GuiMLTextCtrl(MusicTabToggleSoundTxt) {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "227 11";
        extent = "28 18";
        text = "(on)";
    };
    %theTab.toggleSoundTxt.updateText();
    %theTab.add(%theTab.toggleSoundTxt);
    %theTab.add(new GuiBitmapButtonCtrl(MuteButton) {
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
    };);
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
    };.add();
    0;
    new GuiMLTextCtrl(MusicText) {
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
    };
    %ratingLabel = new ""() {
        profile = GuiMLTextCtrl @ "MusicRatingTextProfile";
        horizSizing = new GuiScrollCtrl(MusicTextScroll) {
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
    }; @ "right";
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
    0;
    ratingControl = new ""() {
        profile = GuiControl @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "2 115";
        extent = "150 44";
        minExtent = "1 1";
        visible = 1;
        label = %ratingLabel;
    }; @ MusicHud
    ratingControl.bindClassName("RatingControl");
    ratingControl.bindClassName("MusicRatingControl");
    ratingControl.Initialize(5, "19 19", "platform/client/buttons/star");
    ratingControl.add(%ratingLabel);
    ratingControl.add();
    MusicHud;
    MusicHud;
    MusicHud;
    new GuiPopUp2MenuCtrl(MusicHudStationPopup) {
        profile = "ClosetPopupProfile";
        scrollProfile = "DottedScrollProfile";
        winProfile = "ClosetPopupWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "176 24";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "MusicHud.stationSelected();";
        text = "";
        maxLength = 255;
        maxPopupHeight = 200;
        allowReverse = 0;
    };
    MusicHudStationPopup.add(new GuiControl(MusicHudEditView) {
        profile = MusicHud @ "GuiDefaultProfile";
        horizSizing = MusicHudBasicView @ "right";
        vertSizing = MusicHud @ "bottom";
        position = MusicHud @ "0 0";
        extent = MusicHud.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    };);
};
function MusicTabToggleSoundTxt::updateText(%this) {
    if ($UserPref::Audio::mute) {
        %soundTxt = "(off)";
    }
    %soundTxt = "(on)";
    %this.setText(%soundTxt);
};
function MusicRatingControl::updatePosition(%this) {
    %musicTextBottomY = (getWord(MusicText.getPosition(), 1) + (getWord(MusicTextScroll.getPosition(), 1) + getWord(MusicText.getExtent(), 1)));
    %mTScrollBottomY = (getWord(MusicTextScroll.getPosition(), 1) + getWord(MusicTextScroll.getExtent(), 1));
    %padding = 10;
    %this.reposition(getWord(%this.getPosition, 0), (%padding + mMin(%musicTextBottomY, %mTScrollBottomY)));
};
function HudTabs::fillAffinityTab(%this) {
    %theTab = %this.getTabWithName("affinity");
    if (!(showPlayerInfoPopup())) {
        %theTab.button.setVisible(0);
    }
    %theTab.content.setName("InfoPopupDlg");
    %theTab.content.bindClassName("InfoPopupDlg");
    %theTab.add(new GuiMLTextCtrl(InfoPopupNameField) {
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
    };);
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
    InfoPopupTagsScroll.add(InfoPopupTagsText);
    InfoPopupDlg.add(InfoPopupTagsScroll);
    InfoPopupTagsText.init(InfoPopupDlg);
};
function HudTabs::fillScoresTab(%this) {
    %theTab = %this.getTabWithName("scores");
    %theTab.content.setName("HudScoresContent");
    %theTab.content.bindClassName("HudScoresContent");
    %ypos = 2;
    %fieldx = 110;
    0;
    new ""() {
        profile = GuiMLTextCtrl @ "HudScoresLabelTextProfile";
        horizSizing = HudScoresContent @ "right";
        vertSizing = "bottom";
        position = 0 @ " " @ %ypos;
        extent = "76 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "<b>Level:";
        maxLength = 64;
    };.add();
    0;
    respektLevelLabel = new ""() {
        profile = GuiMLTextCtrl @ "InfoWindowTextProfile";
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
    respektLevelLabel.add();
    0;
    HudScoresContent;
    %ypos = (20.0 + %ypos);
    new ""() {
        profile = GuiMLTextCtrl @ "HudScoresLabelTextProfile";
        horizSizing = HudScoresContent @ "right";
        vertSizing = HudScoresContent @ "bottom";
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
            MissionCleanup.add(HudScoresPBController);
        }
    }
    0;
    respektBarContainer = new ""() {
        profile = GuiControl @ "ETSRespektLevelPBProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = (2.0 - %fieldx) @ " " @ (2.0 + %ypos);
        extent = "125 14";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    }; @ HudScoresContent
    0;
    respektBarHolder = new ""() {
        profile = GuiControl @ "BlankProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = "1 1";
        extent = "114 13";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    }; @ HudScoresContent
    respektBarContainer.add(respektBarHolder);
    respektBarContainer.add();
    respektBarHolder.Initialize("", "platform/client/ui/respektprogress_fill", "", "");
    0;
    HudScoresContent;
    HudScoresContent;
    %ypos = (20.0 + %ypos);
    HudScoresContent;
    new ""() {
        profile = GuiMLTextCtrl @ "HudScoresLabelTextProfile";
        horizSizing = HudScoresContent @ "right";
        vertSizing = HudScoresPBController @ "bottom";
        position = HudScoresContent @ HudScoresContent @ 0 @ " ";
        extent = "76 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "<b><a:gamelink " @ $Net::HelpURL_VPoints @ "?section=vPoints>All-time <bitmap:platform/client/ui/vpoints_14></a>:";
        maxLength = 64;
    };.add();
    0;
    respektScoreLabel = new ""() {
        profile = GuiMLTextCtrl @ "InfoWindowTextProfile";
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
    respektScoreLabel.add();
    if (0) {
        0;
        HudScoresContent;
        %ypos = (20.0 + %ypos);
        new ""() {
            profile = GuiMLTextCtrl @ "HudScoresLabelTextProfile";
            horizSizing = HudScoresContent @ "right";
            vertSizing = HudScoresContent @ "bottom";
            position = 0 @ " ";
            extent = "76 18";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            text = "<b>Rank:";
            maxLength = 64;
        };.add();
        0;
        respektRankLabel = new ""() {
            profile = GuiMLTextCtrl @ "InfoWindowTextProfile";
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
        respektRankLabel.add();
    }
    0;
    HudScoresContent;
    %ypos = (18.0 + %ypos);
    new ""() {
        profile = GuiMLTextCtrl @ "InfoWindowNonModalTextProfile";
        horizSizing = HudScoresContent @ "right";
        vertSizing = HudScoresContent @ "bottom";
        position = 0 @ " ";
        extent = "100 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "<b>Collections:";
        maxLength = 64;
    };.add();
    0;
    %ypos = (20.0 + %ypos);
    collectionsScroll = new ""() {
        profile = GuiScrollCtrl @ "ETSInviteMessageScrollProfile";
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
    0;
    collectionsList = new ""() {
        profile = GuiMLTextCtrl @ "InfoWindowTextListProfile";
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
    collectionsScroll.add(collectionsList);
    collectionsScroll.add();
    if (!(isObject(collectionsSet))) {
        collectionsSet = new SimSet(ScoresHudCollectionsSet); @ HudScoresContent;
        HudScoresContent;
    }
    previousRespektPoints = 0 @ HudScoresContent;
    HudScoresContent;
    0.setRespektPoints(0);
};
function HudScoresContent::setRespektPoints(%this, %points, %notify) {
    %this.respektScoreLabel.setText(%points);
    %level = respektScoreToLevel(%points);
    %levelName = respektLevelToNameWithoutArticle(%level);
    %this.respektLevelLabel.setText(%level @ " - " @ %levelName);
    (respektPercentToNextLevel(%points) - 1.0).setValue();
    %levelPrev = respektScoreToLevel(%this.previousRespektPoints);
    HudScoresPBController;
    if ((%levelPrev != %level)) {
    }
    if ((0.0 != %this.previousRespektPoints)) {
        if ((%levelPrev > %level)) {
            alxPlay(AudioRespektLevelGained);
        }
        if ((1.0 == %level)) {
            %code = "LEVELCHANGE1";
        }
        if ((2.0 == %level)) {
            %code = "LEVELCHANGE2";
        }
        %code = "LEVELCHANGE";
        schedule(5000, 0, "respektHandle", "", %points, (%this.previousRespektPoints - %points), %code, 0, 1);
        5100.schedule("pulseTabWithName", "scores");
    }
    if (%notify) {
    }
    if ((%this.previousRespektPoints != %points)) {
        "scores".pulseTabWithName();
    }
    %this.previousRespektPoints = HudTabs @ %points;
    HudTabs;
};
function HudScoresContent::setRespektRank(%this, %rank) {
    if ((%rank $= "")) {
        %text = "(unknown)";
    }
    %text = "#" @ %rank;
    if (isObject(%this.respektRankLabel)) {
        %this.respektRankLabel.setText(%text);
    }
    if ((%this.previousRespektRank != %rank)) {
        "scores".pulseTabWithName();
    }
    %this.previousRespektRank = HudTabs @ %rank;
};
function HudScoresContent::clearCollections(%this) {
    %count = %this.collectionsSet.getCount();
    %n = (1.0 - %count);
    if ((0.0 >= %n)) {
        %collection = %this.collectionsSet.getObject(%n);
        %this.collectionsSet.remove(%collection);
        %collection.delete();
        %n = (1.0 - %n);
    }
    %this.refreshCollections();
};
function clientCmdSetCollectionStatus(%name, %sofar, %total) {
    %name.setCollectionStatus(%sofar, %total);
};
function HudScoresContent::setCollectionStatus(%this, %name, %sofar, %total) {
    %ourCopy = %this.getCollectionObject(%name);
    if (!(isObject(%ourCopy))) {
        if ((0.0 == %total)) {
        }
        if ((0.0 == %sofar)) {
            return;
        }
        0;
        %ourCopy = new ""() {
            name = ScriptObject @ %name;
        };
        if (isObject(MissionCleanup)) {
            %ourCopy.add();
        }
        %this.collectionsSet.add(%ourCopy);
    }
    if ((0.0 == %total)) {
    }
    if ((0.0 == %sofar)) {
        %this.collectionsSet.remove(%ourCopy);
        %ourCopy.delete();
    }
    %ourCopy.sofar = MissionCleanup @ %sofar;
    %ourCopy.total = %total;
    %this.refreshCollections();
};
function HudScoresContent::getCollectionObject(%this, %name) {
    %n = (1.0 - %this.collectionsSet.getCount());
    if ((0.0 >= %n)) {
        %cur = %this.collectionsSet.getObject(%n);
        if ((%name $= %cur.name)) {
            return %cur;
        }
        %n = (1.0 - %n);
    }
    return -(1.0);
};
function HudScoresContent::refreshCollections(%this) {
    %this.collectionsList.setText("");
    %count = %this.collectionsSet.getCount();
    %stringToSort = "";
    %completed = "";
    %i = (1.0 - %count);
    if ((0.0 >= %i)) {
        %collection = %this.collectionsSet.getObject(%i);
        %ratio = (%collection.total / %collection.sofar);
        if ((1.0 != %ratio)) {
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
        %i = (1.0 - %i);
    }
    %stringToSort = SortWords(%stringToSort);
    (0.0 >= %i);
    %count = getFieldCount(%stringToSort);
    %this.collectionsList.addText("<spush><just:left><b> In Progress:<spop><br>", 0);
    %i = (1.0 - %count);
    if ((0.0 > %i)) {
        %collection = getWord(getField(%stringToSort, %i), 0);
        %this.collectionsList.addText("<spush><just:left>   " @ %collection.name @ " " @ "<just:right>(" @ %collection.sofar @ "/" @ %collection.total @ ")<spop><br>", 0);
        %i = (1.0 - %i);
    }
    if (((0.0 > %i) @ " " @ %completed $= "")) {
        return;
    }
    %this.collectionsList.addText("<spush><just:left><b> Completed:<spop><br>", 0);
    %count = getWordCount(%completed);
    %i = 0;
    if ((%count < %i)) {
        %collection = getWord(%completed, %i);
        %this.collectionsList.addText("<spush><just:left>   " @ %collection.name @ " " @ "<just:right>(" @ %collection.sofar @ "/" @ %collection.total @ ")<spop><br>", 0);
        %i = (1.0 + %i);
    }
    if (%this.collectionsList.isAwake()) {
        %this.collectionsList.forceReflow();
    }
};
function HudScoresContent::onClose(%this) {
};
function HudTabs::fillWordTab(%this) {
    %theTab = %this.getTabWithName("word");
    %theTab.content.setName("SystemMessageDialog");
    %theTab.content.bindClassName("SystemMessageDialog");
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
    };.add();
    DefaultMessage.setText();
};
function HudTabs::fillPrivateSpaceTab(%this) {
    "private space".hideTabWithName();
    %theTab = %this.getTabWithName("private space");
    HudTabs;
    %theTab.content.setName("PrivSpaceHud");
    %theTab.content.bindClassName("PrivSpaceHud");
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
    %theTab.add(toggleOP);
    PrivSpaceHud;
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
    SpaceSurfText.add(%theTab);
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
    SpaceSurfTE.add(%theTab);
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
    %this.toggleOP.setVisible(1);
};
function PrivSpaceHud::disableOPlink(%this) {
    %this.toggleOP.setVisible(0);
};
function PrivSpaceHud::showOP(%this) {
    1.setVisible();
    0.setVisible();
    %this.toggleOP.setText("<a:OPoff >(guest view)</a>");
    if ((NonOPSpaceHud @ " " @ HudTabs.getCurrentTab().name $= "private space")) {
        HudTabs.dontCloseNextTime();
    }
    CSControlPanel.open();
};
function PrivSpaceHud::hideOP(%this) {
    0.setVisible();
    1.setVisible();
    %this.toggleOP.setText("<a:OPon >(host's view)</a>");
    if ((NonOPSpaceHud @ " " @ HudTabs.getCurrentTab().name $= "private space")) {
        $Pref::ETS::HudTabs::timeout.autoHideSchedule();
        OPSpaceHud.descriptionChanged();
    }
    CSControlPanel.close();
};
function PrivSpaceHud::updateMusic(%this, %newStreamID) {
    if (isObject($musicStreamIDMap)) {
        %newStreamName = $musicStreamIDMap.get(%newStreamID);
        if ((%newStreamName $= "")) {
            error("Stream ID (\"" @ %newStreamID @ "\") not in music stream id -> name mapping! <-" @ getScopeName());
            %newStreamName = %newStreamID;
        }
    }
    warn("the music stream ID map was not initialized.  This should have been done in GameConnection::etsInit()");
    %newStreamName = %newStreamID;
    %newStreamName.updateMusic();
    %newStreamName.updateMusic();
};
function clientCmdPrivSpaceHudUpdateMusic(%newStreamID) {
};
function clientCmdPrivSpaceHudUpdateVideo(%unused) {
};
function OPSpaceHud::setup(%this) {
    %ypos = 0;
    0;
    %this.add(new ""() {
        profile = GuiTextCtrl @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " " @ %ypos;
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Name: ";
        maxLength = 64;
    };);
    0;
    %this.spaceNameField = new ""() {
        profile = GuiTextCtrl @ "InfoWindowNonModalTextProfile";
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
    %this.add(%this.spaceNameField);
    0;
    %ypos = (22.0 + %ypos);
    %this.add(new ""() {
        profile = GuiTextCtrl @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "100 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Description: ";
        maxLength = 64;
    };);
    0;
    %ypos = (20.0 + %ypos);
    %this.spaceDescField = new ""() {
        profile = GuiTextEditCtrl @ "InfoWindowTextEditProfile";
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
    %this.add(%this.spaceDescField);
    0;
    %ypos = (27.0 + %ypos);
    %this.add(new ""() {
        profile = GuiTextCtrl @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Access:";
        maxLength = 64;
    };);
    0;
    %ypos = (20.0 + %ypos);
    %this.AccessOptAnyone = new ""() {
        profile = GuiRadioCtrl @ "InfoWindowRadioButtonProfile";
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
    %this.add(%this.AccessOptAnyone);
    0;
    %ypos = (20.0 + %ypos);
    %this.AccessOptFriends = new ""() {
        profile = GuiRadioCtrl @ "InfoWindowRadioButtonProfile";
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
    %this.add(%this.AccessOptFriends);
    0;
    %ypos = (20.0 + %ypos);
    %this.add(new ""() {
        profile = GuiTextCtrl @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Music: ";
        maxLength = 64;
    };);
    0;
    %ypos = (20.0 + %ypos);
    %this.MusicStreamDropdown = new ""() {
        profile = GuiPopUp2MenuCtrl @ "InfoWindowPopupProfile";
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
    %this.add(%this.MusicStreamDropdown);
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
            %streamID = $musicStreamNameMap.get(%selection);
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
    if ((0.0 != %this.MusicStreamDropdown.size())) {
        %index = %this.MusicStreamDropdown.findText(%newStreamName);
        if ((0.0 < %index)) {
            warn("Some music streams are loaded, but the latest update is not in the dropdown!<-" @ getScopeName());
        }
        %this.MusicStreamDropdown.SetSelected(%index);
    }
    warn("Tried to set selected music stream on updating OPSpaceHud settings, but the music wasn't loaded!<-" @ getScopeName());
};
function OPSpaceHud::updateStreams(%this, %streamList) {
    %this.MusicStreamDropdown.fillFromList(%streamList);
    %selectedIndex = 0;
    if (!(%this.musicStream $= "")) {
        %selectedIndex = %this.MusicStreamDropdown.findText(%this.musicStream);
    }
    %this.MusicStreamDropdown.SetSelected(%selectedIndex);
};
function OPSpaceHud::updateSettings(%this, %name, %description, %accessMode) {
    %this.description = %description;
    %this.spaceDescField.setText(%description);
    %this.accessLevel = %accessMode;
    if ((%accessMode $= "Open")) {
        %this.AccessOptAnyone.performClick();
    }
    if ((%accessMode $= "FriendsOnly")) {
        %this.AccessOptFriends.performClick();
    }
    %this.accessLevel = "Open";
    %this.AccessOptAnyone.performClick();
    %this.spaceNameField.setText(%name);
};
function NonOPSpaceHud::setup(%this) {
    %ypos = 0;
    0;
    %this.add(new ""() {
        profile = GuiTextCtrl @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " " @ %ypos;
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Name: ";
        maxLength = 64;
    };);
    0;
    %this.spaceNameField = new ""() {
        profile = GuiMLTextCtrl @ "InfoWindowTextProfile";
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
    %this.add(%this.spaceNameField);
    0;
    %ypos = (20.0 + %ypos);
    %this.add(new ""() {
        profile = GuiTextCtrl @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Host: ";
        maxLength = 64;
    };);
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
    %this.add(%this.spaceOwnerField);
    0;
    %ypos = (35.0 + %ypos);
    %this.add(new ""() {
        profile = GuiTextCtrl @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "85 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Description: ";
        maxLength = 64;
    };);
    0;
    %ypos = (20.0 + %ypos);
    %this.spaceDescField = new ""() {
        profile = GuiTextCtrl @ "InfoWindowNonModalTextProfile";
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
    %this.add(%this.spaceDescField);
    0;
    %ypos = (40.0 + %ypos);
    %this.add(new ""() {
        profile = GuiTextCtrl @ "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 @ " ";
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Music: ";
        maxLength = 64;
    };);
    0;
    %ypos = (20.0 + %ypos);
    %this.musicStreamField = new ""() {
        profile = GuiTextCtrl @ "InfoWindowNonModalTextProfile";
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
    %this.add(%this.musicStreamField);
    0;
    %ypos = (30.0 + %ypos);
    %this.bigMLText = new ""() {
        profile = GuiMLTextCtrl @ "InfoWindowTextProfile";
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
    %this.add(%this.bigMLText);
};
function NonOPSpaceHud::updateSettings(%this, %name, %description, %owner) {
    if (!(%owner $= "")) {
        %this.spaceOwnerField.setText("<a:owner " @ munge(%owner) @ ">" @ %owner @ "</a>");
    }
    %this.spaceOwnerField.setText("<a:noowner >Take Control</a>");
    %this.spaceDescField.setText(%description);
    %this.spaceNameField.setText(%name);
};
function NonOPSpaceHud::updateMusic(%this, %newStreamName) {
    %this.musicStreamField.setText(%newStreamName);
};
function GuiPopUp2MenuCtrl::fillFromList(%this, %list) {
    %this.clear();
    %count = getFieldCount(%list);
    %i = 0;
    if ((%count < %i)) {
        %this.add(getField(%list, %i));
        %i = (1.0 + %i);
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
    0;
    %request = new ""() {
        className = ManagerRequest @ "ModifyCustomSpaceSettingsRequest";
    };
    if (isObject(MissionCleanup)) {
        %request.add();
    }
    %url = $Net::ClientServiceURL @ "/SaveCustomSpaceSettings?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "space=" @ urlEncode(%spaceName);
    MissionCleanup;
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
    %request.setURL(%url);
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
        warn("network", getScopeName() @ " request failed: " @ %this.getValue("statusMessage"));
    }
    %this.schedule(0, "delete");
    if (!(%this.requestDescription $= "")) {
        CSRulesDescSavedIndicator.decrementRequestCount();
    }
    if (!(%this.requestAccessMode $= "")) {
        CSRulesPasswordSavedIndicator.decrementRequestCount();
    }
};
function ModifyCustomSpaceSettingsRequest::onError(%this, %unused, %errMsg) {
    error("network", getScopeName() @ ":" @ %errMsg);
    %this.schedule(0, "delete");
};
function CustomSpaceSettings::changeSpaceOwnership(%spaceName, %takeOwnershipBool) {
    0;
    %request = new ""() {
        className = ManagerRequest @ "ChangeSpaceOwnershipRequest";
    };
    if (isObject(MissionCleanup)) {
        %request.add();
    }
    %url = $Net::BaseURL @ "?cmd=ChangeSpaceOwnership" @ "&user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token) @ "&space=" @ urlEncode(%spaceName) @ "&take=" @ urlEncode(%takeOwnershipBool ? 1 : 0);
    MissionCleanup;
    log("network", "info", getScopeName() @ ":" @ %url);
    %request.setURL(%url);
    %request.spaceName = %spaceName;
    %request.start();
};
function ChangeSpaceOwnershipRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    %statusMsg = %this.getValue("statusMsg");
    log("network", "info", getScopeName() @ ":" @ %status @ " - msg: " @ %statusMsg);
    if ((%status $= "success")) {
        "private space".selectTabWithName();
    }
    if ((HudTabs @ " " @ trim(getWords(%statusMsg, 0, 1)) $= "fail already-owned")) {
        handleSystemMessage("msgInfoMessage", "Sorry, the space is already owned by someone else.");
    }
    if ((trim(getWords(%statusMsg, 0, 1)) $= "fail respekt")) {
        handleSystemMessage("msgInfoMessage", "Sorry, you must be at least a " @ getWord(%statusMsg, 2) @ " to own this space.");
    }
    handleSystemMessage("msgInfoMessage", "Sorry, you couldn't change the ownership of the space.");
    %this.schedule(0, "delete");
};
function CustomSpaceSettings::onError(%this, %unused, %errMsg) {
    error("network", getScopeName() @ ":" @ %errMsg);
    %this.schedule(0, "delete");
};
function HudTabs::addPermissionBasedContent(%this) {
    if (!(%this.filledPrivateSpaceTab)) {
        return;
    }
    %hasPerm = $player.rolesPermissionCheckNoWarn("fly");
    %hasPerm.setVisible();
    %hasPerm.setVisible();
    if (%hasPerm) {
    }
    if ((SpaceSurfTE @ " " @ $gMode $= "PrivateSpaceGrid")) {
        %this.showTabWithName("private space");
    }
};
function SpaceSurfText::onURL(%this, %url) {
    teleportToAdjacentSpace((%url $= "spaceN"));
};
function SpaceSurfTE::onEnter(%this) {
    teleportToSpaceNumber(%this.getValue());
};
