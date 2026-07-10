function HudTabs::setup() {
    if (!(isObject())) {
        class = HudTabs @ new ScriptObject(HudTabs) @ "TabControl";
        if (isObject()) {
            add();
        }
        "46 39".Initialize("", "", "vertical");
        "music".newTab("platform/client/buttons/hud_music", "music & videos");
        "affinity".newTab("platform/client/buttons/hud_affinity", "");
        "scores".newTab("platform/client/buttons/hud_scores", "");
        "word".newTab("platform/client/buttons/hud_word", "");
        "tutorial".newTab("platform/client/buttons/hud_tutorials", "");
        fillTabs();
        close();
        closeTimer = HudTabs @ 0 @ HudTabs;
        HudTabs;
    }
};
function HudTabs::newTab(%this, %name, %bitmapName, %title) {
    if (!(isDefined("%title"))) {
        %title = "";
    }
    %tab = Parent::newTab(%this, %name, %bitmapName);
    title = %title @ %tab;
    pulsar = AnimCtrl::newAnimCtrl("0 0", "54 43") @ %tab;
    pulsar.setDelay(60);
    pulsar.addFrame("platform/client/ui/pulse/bracket_00.png");
    pulsar.addFrame("platform/client/ui/pulse/bracket_01.png");
    pulsar.addFrame("platform/client/ui/pulse/bracket_02.png");
    pulsar.addFrame("platform/client/ui/pulse/bracket_03.png");
    pulsar.addFrame("platform/client/ui/pulse/bracket_04.png");
    pulsar.addFrame("platform/client/ui/pulse/bracket_05.png");
    pulsar.addFrame("platform/client/ui/pulse/bracket_06.png");
    pulsar.addFrame("platform/client/ui/pulse/bracket_07.png");
    pulsar.addFrame("platform/client/ui/pulse/bracket_08.png");
    pulsar.addFrame("platform/client/ui/pulse/bracket_09.png");
    pulsar.addFrame("platform/client/ui/pulse/bracket_10.png");
    pulsar.addFrame("platform/client/ui/pulse/bracket_11.png");
    pulsar.addFrame("platform/client/ui/pulse/bracket_12.png");
    pulsar.addFrame("platform/client/ui/pulse/bracket_13.png");
    pulsar.setProfile();
    pulsar.setVisible(0);
    %pos = button.getPosition();
    %tab;
    %xPos = (4.0 - getWord(%pos, 0));
    %tab;
    %ypos = (2.0 - getWord(%pos, 1));
    ETSNonModalProfile;
    pulsar.reposition(%xPos, %ypos);
    pulseTimer = %tab @ 0 @ %tab;
    %tab;
};
function HudTabs::update(%this) {
    Parent::update(%this);
    %numVisibleButtons = 0;
    %padding = %this.getPadding();
    %n = 0;
    if ((numTabs < %n)) {
        if (buttons.isVisible()) {
            %height = ((%padding @ %n @ %this + getWord(buttons.getExtent(), 1)) + %height);
            %this @ %n @ %this;
        }
        %n = (1.0 + %n);
    }
    %height = (%padding - %height);
    (numTabs < %n);
    %width = getWord(getExtent(), 0);
    HudTabsCollapsed;
    %width.resize(%height);
};
function HudTabs::pulseTab(%this, %tabObject) {
    if (!(isObject(%tabObject))) {
    }
    if (!(isObject(pulsar))) {
        error(%tabObject @ getScopeName() @ "->invalid tab object or tab without pulsar object passed! returning!");
        return;
    }
    %this.pausePulseOnAllTabs();
    pulsar.add();
    pulsar.pushToBack();
    pulsar.setVisible(1);
    pulsar.start();
    cancel(pulseTimer);
    pulseTimer = %tabObject @ %this.schedule(4000, "pausePulseOnTab", %tabObject) @ %tabObject;
    %tabObject;
};
function HudTabs::pulseTabWithName(%this, %tabName) {
    %tabObject = 0;
    if (!(%tabName $= "")) {
        %tabObject = %this.getTabWithName(%tabName);
    }
    %this.pulseTab(%tabObject);
};
function HudTabs::pausePulseOnTab(%this, %tabObject) {
    cancel(pulseTimer);
    pulsar.stop();
    pulsar.setCurrentFrame(0);
};
function HudTabs::pausePulseOnAllTabs(%this) {
    %i = 0;
    if ((numTabs < %i)) {
        %this.pausePulseOnTab(tabs);
        %i = (1.0 + %i);
        %this @ %i @ %this;
    }
};
function HudTabs::stopPulseOnTab(%this, %tabObject) {
    cancel(pulseTimer);
    pulsar.stop();
    pulsar.setVisible(0);
};
function HudTabs::getPadding(%this) {
    return 0;
};
function HudTabs::close(%this) {
    %tab = %this.getCurrentTab();
    if (isObject(%tab)) {
    }
    if (isObject(content)) {
        content.onClose();
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
    if (isObject(pulsar)) {
        if (pulsar.isVisible()) {
            %this.stopPulseOnTab(%tabObject);
        }
    }
};
function HudTabs::setTabAtIndexVisible(%this, %tabIndex, %visible) {
    %tab = tabs;
    %tabIndex @ %this;
    if (!(isObject(%tab))) {
        return;
    }
    %posX = getWord(tabPosition, 0);
    %this;
    %posY = getWord(tabPosition, 1);
    %this;
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
    %tabName = name;
    %currentTab;
    if (!(%tabName[$UserPref::HudTabs::AutoClose @ %tabName])) {
        return "";
    }
    if (container.cursorInControl()) {
        %this.autoHideSchedule(2000);
        return %this;
    }
    %this.close();
    if (pulsar.isVisible()) {
        %this.pausePulseOnTab(%currentTab);
    }
};
function HudTabs::autoHideSchedule(%this, %ms) {
    cancel(closeTimer);
    closeTimer = %this @ %this.schedule(%ms, "autoHide") @ %this;
};
function HudTabs::dontCloseNextTime(%this) {
    cancel(closeTimer);
    closeTimer = %this @ 0 @ %this;
};
function HudTabs::tabSelected(%this, %tab) {
    if (isObject(%tab)) {
        0.setVisible();
        %this.autoHideSchedule($Pref::ETS::HudTabs::timeout);
        if (isObject(pulsar)) {
            %this.stopPulseOnTab(%tab);
        }
    }
    if (!(autoHide)) {
        %this.dontCloseNextTime();
    }
    %prevTab = %this.getPreviousTab();
    %tab;
    if ((%tab != %prevTab)) {
        if (isObject(%prevTab)) {
        }
        if (isObject(content)) {
            content.onClose();
            if (pulsar.isVisible()) {
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
    sluggishness = 0.5 @ %tab;
    %tab.reposition((%this - getWord(tabPosition, 0)), getWord(tabPosition, 1));
    return %tab;
};
function HudTabs::fillTabs(%this) {
    %i = 0;
    if ((numTabs < %i)) {
        %tab = tabs;
        HudTabs @ %i @ HudTabs;
        %tab.setProfile();
        %tab.clear();
        tooltip = %tab @ button;
        %tab @ name;
        profile = GuiControl @ new ""() @ "ETSDarkBoxProfile";
        0;
        horizSizing = GuiDefaultProfile @ "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = 291 @ " " @ HudContainer @ getWord(extent, 1);
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        %tab.add();
        if ((%tab SPC title $= "")) {
        }
        %title = title;
        %tab;
        profile = GuiMLTextCtrl @ new ""() @ "ETSHudHeadingProfile";
        0;
        horizSizing = %tab @ name @ "right";
        vertSizing = "bottom";
        position = "47 5";
        extent = "170 20";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        text = %title;
        %titleText = ;
        titleText = %titleText @ %tab;
        %tab.add(%titleText);
        profile = GuiBitmapButtonCtrl @ new ""() @ "GuiDefaultProfile";
        0;
        horizSizing = "left";
        vertSizing = "bottom";
        position = "270 5";
        extent = "16 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/buttons/gray_close";
        command = "HudTabs.overrideLockedOpen = true; HudTabs.close();";
        %closeButton = ;
        closeButton = %closeButton @ %tab;
        %tab.add(%closeButton);
        profile = GuiControl @ new ""() @ "GuiDefaultProfile";
        0;
        horizSizing = "right";
        vertSizing = "bottom";
        position = "47 35";
        extent = "235 180";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        %content = ;
        content = %content @ %tab;
        %tab.add(%content);
        %i = (1.0 + %i);
    }
    filledPrivateSpaceTab = (numTabs < %i) @ 0 @ %this;
    HudTabs;
    fillMusicTab();
    fillAffinityTab();
    fillScoresTab();
    fillWordTab();
    fillTutorialTab();
};
function HudTabs::fillMusicTab(%this) {
    %theTab = %this.getTabWithName("music");
    content.setName("MusicHud");
    content.bindClassName("MusicHud");
    profile = %theTab @ new GuiMLTextCtrl(MusicTabToggleSoundTxt) @ "ETSShadowTextProfile";
    %theTab;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "227 11";
    extent = "28 18";
    text = "(on)";
    toggleSoundTxt = %theTab;
    toggleSoundTxt.updateText();
    %theTab.add(toggleSoundTxt);
    profile = %theTab @ new GuiBitmapButtonCtrl(MuteButton) @ "GuiDefaultProfile";
    %theTab;
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
    %theTab.add();
    station = "" @ MusicHud;
    profile = MusicHud @ new GuiControl(MusicHudBasicView) @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = MusicHud @ getExtent();
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    profile = new GuiVariableWidthButtonCtrl(MusicHudChangeStationButton) @ "BracketButton15Profile";
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
    profile = new GuiVariableWidthButtonCtrl(MusicHudMyMediaButton) @ "BracketButton15Profile";
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
    profile = new GuiScrollCtrl(MusicTextScroll) @ "ETSScrollSmallProfile";
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
    profile = new GuiMLTextCtrl(MusicText) @ "MusicMLTextProfile";
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
    .add();
    profile = GuiMLTextCtrl @ new ""() @ "MusicRatingTextProfile";
    0;
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
    %ratingLabel = ;
    profile = GuiControl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "2 115";
    extent = "150 44";
    minExtent = "1 1";
    visible = 1;
    label = %ratingLabel;
    ratingControl = MusicHud;
    ratingControl.bindClassName("RatingControl");
    ratingControl.bindClassName("MusicRatingControl");
    ratingControl.Initialize(5, "19 19", "platform/client/buttons/star");
    ratingControl.add(%ratingLabel);
    ratingControl.add();
    profile = MusicHud @ new GuiControl(MusicHudEditView) @ "GuiDefaultProfile";
    MusicHud;
    horizSizing = MusicHud @ MusicHudBasicView @ "right";
    MusicHud;
    vertSizing = MusicHud @ MusicHud @ "bottom";
    position = "0 0";
    extent = MusicHud @ getExtent();
    minExtent = "1 1";
    sluggishness = -1;
    visible = 0;
    profile = new GuiPopUp2MenuCtrl(MusicHudStationPopup) @ "ClosetPopupProfile";
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
    profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton15Profile";
    horizSizing = "left";
    vertSizing = "bottom";
    position = "186 3";
    extent = "52 15";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    command = "MusicHud.setView(\"basic\");";
    text = "Done";
    groupNum = -1;
    buttonType = "PushButton";
    .add();
};
function MusicTabToggleSoundTxt::updateText(%this) {
    if ($UserPref::Audio::mute) {
        %soundTxt = "(off)";
    }
    %soundTxt = "(on)";
    %this.setText(%soundTxt);
};
function MusicRatingControl::updatePosition(%this) {
    %musicTextBottomY = (getWord(getPosition(), 1) + (MusicText + getWord(getExtent(), 1)));
    MusicTextScroll;
    %mTScrollBottomY = (MusicTextScroll + getWord(getExtent(), 1));
    getWord(getPosition(), 1);
    %padding = 10;
    MusicTextScroll;
    %this.reposition(getWord(getPosition, 0), (%padding + mMin(%musicTextBottomY, %mTScrollBottomY)));
};
function HudTabs::fillAffinityTab(%this) {
    %theTab = %this.getTabWithName("affinity");
    if (!(showPlayerInfoPopup())) {
        button.setVisible(0);
    }
    content.setName("InfoPopupDlg");
    content.bindClassName("InfoPopupDlg");
    profile = %theTab @ new GuiMLTextCtrl(InfoPopupNameField) @ "InfoWindowTextProfile";
    %theTab;
    horizSizing = %theTab @ "width";
    vertSizing = "bottom";
    position = "122 8";
    extent = "150 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    lineSpacing = 1;
    allowColorChars = 0;
    maxChars = -1;
    %theTab.add();
    profile = InfoPopupDlg @ new GuiMLTextCtrl(InfoPopupContents) @ "InfoWindowTextProfile";
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
    .add();
    profile = InfoPopupDlg @ new GuiMLTextCtrl(InfoPopupBottom) @ "InfoWindowTextProfile";
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
    .add();
    profile = InfoPopupDlg @ new GuiScrollCtrl(InfoPopupTagsScroll) @ "ETSScrollProfile";
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
    .add();
    profile = new GuiMLTextCtrl(InfoPopupTagsText) @ "InfoWindowTextProfile";
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
    add();
    add();
    init();
};
function HudTabs::fillScoresTab(%this) {
    %theTab = %this.getTabWithName("scores");
    content.setName("HudScoresContent");
    content.bindClassName("HudScoresContent");
    %ypos = 2;
    %theTab;
    %fieldx = 110;
    %theTab;
    profile = GuiMLTextCtrl @ new ""() @ "HudScoresLabelTextProfile";
    0;
    horizSizing = HudScoresContent @ "right";
    vertSizing = "bottom";
    position = 0 @ " " @ %ypos;
    extent = "76 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "<b>Level:";
    maxLength = 64;
    .add();
    profile = GuiMLTextCtrl @ new ""() @ "InfoWindowTextProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = %fieldx @ " " @ %ypos;
    extent = "134 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "1 - Freshman";
    maxLength = 64;
    respektLevelLabel = HudScoresContent;
    respektLevelLabel.add();
    profile = GuiMLTextCtrl @ new ""() @ "HudScoresLabelTextProfile";
    0;
    horizSizing = HudScoresContent @ HudScoresContent @ "right";
    HudScoresContent;
    vertSizing = "bottom";
    %ypos = (20.0 + %ypos);
    position = 0 @ " ";
    extent = "85 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "<b>Next Level:";
    maxLength = 64;
    .add();
    if (!(isObject())) {
        class = HudScoresPBController @ new ScriptObject(HudScoresPBController) @ "ProgressBarController";
        if (isObject()) {
            add();
        }
    }
    profile = GuiControl @ new ""() @ "ETSRespektLevelPBProfile";
    0;
    horizSizing = MissionCleanup @ HudScoresPBController @ "center";
    MissionCleanup;
    vertSizing = "top";
    position = (2.0 - %fieldx) @ " " @ (2.0 + %ypos);
    extent = "125 14";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    respektBarContainer = HudScoresContent;
    profile = GuiControl @ new ""() @ "BlankProfile";
    0;
    horizSizing = "center";
    vertSizing = "top";
    position = "1 1";
    extent = "114 13";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    respektBarHolder = HudScoresContent;
    respektBarContainer.add(respektBarHolder);
    respektBarContainer.add();
    respektBarHolder.Initialize("", "platform/client/ui/respektprogress_fill", "", "");
    profile = GuiMLTextCtrl @ new ""() @ "HudScoresLabelTextProfile";
    0;
    horizSizing = HudScoresContent @ HudScoresContent @ "right";
    HudScoresPBController;
    vertSizing = HudScoresContent @ HudScoresContent @ "bottom";
    HudScoresContent;
    %ypos = (20.0 + %ypos);
    position = HudScoresContent @ 0 @ " ";
    extent = "76 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "<b><a:gamelink " @ $Net::HelpURL_VPoints @ "?section=vPoints>All-time <bitmap:platform/client/ui/vpoints_14></a>:";
    maxLength = 64;
    .add();
    profile = GuiMLTextCtrl @ new ""() @ "InfoWindowTextProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = %fieldx @ " " @ %ypos;
    extent = "100 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "";
    maxLength = 64;
    respektScoreLabel = HudScoresContent;
    respektScoreLabel.add();
    if (0) {
        profile = GuiMLTextCtrl @ new ""() @ "HudScoresLabelTextProfile";
        0;
        horizSizing = HudScoresContent @ HudScoresContent @ "right";
        HudScoresContent;
        vertSizing = "bottom";
        %ypos = (20.0 + %ypos);
        position = 0 @ " ";
        extent = "76 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "<b>Rank:";
        maxLength = 64;
        .add();
        profile = GuiMLTextCtrl @ new ""() @ "InfoWindowTextProfile";
        0;
        horizSizing = "right";
        vertSizing = "bottom";
        position = %fieldx @ " " @ %ypos;
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "#1";
        maxLength = 64;
        respektRankLabel = HudScoresContent;
        respektRankLabel.add();
    }
    profile = GuiMLTextCtrl @ new ""() @ "InfoWindowNonModalTextProfile";
    0;
    horizSizing = HudScoresContent @ HudScoresContent @ "right";
    HudScoresContent;
    vertSizing = "bottom";
    %ypos = (18.0 + %ypos);
    position = 0 @ " ";
    extent = "100 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "<b>Collections:";
    maxLength = 64;
    .add();
    profile = GuiScrollCtrl @ new ""() @ "ETSInviteMessageScrollProfile";
    0;
    horizSizing = "width";
    vertSizing = "height";
    %ypos = (20.0 + %ypos);
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
    collectionsScroll = HudScoresContent;
    profile = GuiMLTextCtrl @ new ""() @ "InfoWindowTextListProfile";
    0;
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
    collectionsList = HudScoresContent;
    collectionsScroll.add(collectionsList);
    collectionsScroll.add();
    if (!(isObject(collectionsSet))) {
        collectionsSet = HudScoresContent @ new SimSet(ScoresHudCollectionsSet) @ HudScoresContent;
        HudScoresContent;
    }
    previousRespektPoints = HudScoresContent @ 0 @ HudScoresContent;
    HudScoresContent;
    0.setRespektPoints(0);
};
function HudScoresContent::setRespektPoints(%this, %points, %notify) {
    respektScoreLabel.setText(%points);
    %level = respektScoreToLevel(%points);
    %this;
    %levelName = respektLevelToNameWithoutArticle(%level);
    respektLevelLabel.setText(%this @ %level @ " - " @ %levelName);
    (respektPercentToNextLevel(%points) - 1.0).setValue();
    %levelPrev = respektScoreToLevel(previousRespektPoints);
    %this;
    if ((%levelPrev != %level)) {
    }
    if ((%this != previousRespektPoints)) {
        if ((%levelPrev > %level)) {
            alxPlay();
        }
        if ((1.0 == %level)) {
            %code = "LEVELCHANGE1";
            AudioRespektLevelGained;
        }
        if ((2.0 == %level)) {
            %code = "LEVELCHANGE2";
            0.0;
        }
        %code = "LEVELCHANGE";
        HudScoresPBController;
        schedule(5000, 0, "respektHandle", "", %points, (previousRespektPoints - %points), %code, 0, 1);
        5100.schedule("pulseTabWithName", "scores");
    }
    if (%notify) {
    }
    if ((previousRespektPoints != %points)) {
        "scores".pulseTabWithName();
    }
    previousRespektPoints = HudTabs @ %points @ %this;
    %this;
};
function HudScoresContent::setRespektRank(%this, %rank) {
    if ((%rank $= "")) {
        %text = "(unknown)";
    }
    %text = "#" @ %rank;
    if (isObject(respektRankLabel)) {
        respektRankLabel.setText(%text);
    }
    if ((previousRespektRank != %rank)) {
        "scores".pulseTabWithName();
    }
    previousRespektRank = HudTabs @ %rank @ %this;
    %this;
};
function HudScoresContent::clearCollections(%this) {
    %count = collectionsSet.getCount();
    %this;
    %n = (1.0 - %count);
    if ((0.0 >= %n)) {
        %collection = collectionsSet.getObject(%n);
        %this;
        collectionsSet.remove(%collection);
        %collection.delete();
        %n = (1.0 - %n);
        %this;
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
        name = ScriptObject @ new ""() @ %name;
        0;
        %ourCopy = ;
        if (isObject()) {
            %ourCopy.add();
        }
        collectionsSet.add(%ourCopy);
    }
    if ((0.0 == %total)) {
    }
    if ((0.0 == %sofar)) {
        collectionsSet.remove(%ourCopy);
        %ourCopy.delete();
    }
    sofar = %this @ %sofar @ %ourCopy;
    %this;
    total = MissionCleanup @ %total @ %ourCopy;
    MissionCleanup;
    %this.refreshCollections();
};
function HudScoresContent::getCollectionObject(%this, %name) {
    %n = (%this - collectionsSet.getCount());
    1.0;
    if ((0.0 >= %n)) {
        %cur = collectionsSet.getObject(%n);
        %this;
        if ((%cur $= name)) {
            return %cur;
        }
        %n = (1.0 - %n);
    }
    return -(1.0);
};
function HudScoresContent::refreshCollections(%this) {
    collectionsList.setText("");
    %count = collectionsSet.getCount();
    %this;
    %stringToSort = "";
    %this;
    %completed = "";
    %i = (1.0 - %count);
    if ((0.0 >= %i)) {
        %collection = collectionsSet.getObject(%i);
        %this;
        %ratio = (%collection / sofar);
        total;
        if ((1.0 != %ratio)) {
            %append = formatFloat("%1.3f", %ratio) @ "\t" @ %collection.getId();
            %collection;
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
    collectionsList.addText("<spush><just:left><b> In Progress:<spop><br>", 0);
    %i = (1.0 - %count);
    %this;
    if ((0.0 > %i)) {
        %collection = getWord(getField(%stringToSort, %i), 0);
        collectionsList.addText(%this @ "<spush><just:left>   " @ %collection @ name @ " " @ "<just:right>(" @ %collection @ sofar @ "/" @ %collection @ total @ ")<spop><br>", 0);
        %i = (1.0 - %i);
    }
    if (((0.0 > %i) SPC %completed $= "")) {
        return;
    }
    collectionsList.addText("<spush><just:left><b> Completed:<spop><br>", 0);
    %count = getWordCount(%completed);
    %this;
    %i = 0;
    if ((%count < %i)) {
        %collection = getWord(%completed, %i);
        collectionsList.addText(%this @ "<spush><just:left>   " @ %collection @ name @ " " @ "<just:right>(" @ %collection @ sofar @ "/" @ %collection @ total @ ")<spop><br>", 0);
        %i = (1.0 + %i);
    }
    if (collectionsList.isAwake()) {
        collectionsList.forceReflow();
    }
};
function HudScoresContent::onClose(%this) {
};
function HudTabs::fillWordTab(%this) {
    %theTab = %this.getTabWithName("word");
    content.setName("SystemMessageDialog");
    content.bindClassName("SystemMessageDialog");
    profile = SystemMessageDialog @ new GuiScrollCtrl(SystemMessageScrollCtrl) @ "ETSScrollProfile";
    %theTab;
    horizSizing = %theTab @ "width";
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
    profile = new GuiMLTextCtrl(SystemMessageTextCtrl) @ "SystemMessageTextProfile";
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
    .add();
    DefaultMessage.setText();
};
function HudTabs::fillPrivateSpaceTab(%this) {
    "private space".hideTabWithName();
    %theTab = %this.getTabWithName("private space");
    HudTabs;
    content.setName("PrivSpaceHud");
    content.bindClassName("PrivSpaceHud");
    profile = %theTab @ new GuiMLTextCtrl(PrivSpaceHudToggleOP) @ "ETSShadowTextProfile";
    %theTab;
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
    toggleOP = PrivSpaceHud;
    %theTab.add(toggleOP);
    profile = PrivSpaceHud @ new GuiControl(OPSpaceHud) @ "GuiDefaultProfile";
    PrivSpaceHud;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = PrivSpaceHud @ getExtent();
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    .add();
    profile = PrivSpaceHud @ new GuiControl(NonOPSpaceHud) @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = PrivSpaceHud @ getExtent();
    minExtent = "1 1";
    sluggishness = -1;
    visible = 0;
    .add();
    setup();
    setup();
    hideOP();
    profile = PrivSpaceHud @ new GuiMLTextCtrl(SpaceSurfText) @ "InfoWindowTextProfile";
    NonOPSpaceHud;
    horizSizing = OPSpaceHud @ "width";
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
    %theTab.add();
    profile = SpaceSurfText @ new GuiTextEditCtrl(SpaceSurfTE) @ "InfoWindowTextEditProfile";
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
    %theTab.add();
    filledPrivateSpaceTab = SpaceSurfTE @ 1 @ %this;
};
function PrivSpaceHud::onClose(%this) {
    descriptionChanged();
};
function PrivSpaceHudToggleOP::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "OPon")) {
        showOP();
    }
    if ((PrivSpaceHud SPC getWord(%url, 0) $= "OPoff")) {
        hideOP();
    }
    error(PrivSpaceHud @ "Url in PrivSpaceHud.toggleOP is broken.<-" @ getScopeName());
};
function PrivSpaceHud::enableOPlink(%this) {
    toggleOP.setVisible(1);
};
function PrivSpaceHud::disableOPlink(%this) {
    toggleOP.setVisible(0);
};
function PrivSpaceHud::showOP(%this) {
    1.setVisible();
    0.setVisible();
    toggleOP.setText("<a:OPoff >(guest view)</a>");
    if ((getCurrentTab() SPC name $= "private space")) {
        dontCloseNextTime();
    }
    open();
};
function PrivSpaceHud::hideOP(%this) {
    0.setVisible();
    1.setVisible();
    toggleOP.setText("<a:OPon >(host's view)</a>");
    if ((getCurrentTab() SPC name $= "private space")) {
        $Pref::ETS::HudTabs::timeout.autoHideSchedule();
        descriptionChanged();
    }
    close();
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
    profile = GuiTextCtrl @ new ""() @ "InfoWindowNonModalTextProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = 0 @ " " @ %ypos;
    extent = "68 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Name: ";
    maxLength = 64;
    %this.add();
    profile = GuiTextCtrl @ new ""() @ "InfoWindowNonModalTextProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = 60 @ " " @ %ypos;
    extent = "155 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "";
    maxLength = 64;
    spaceNameField = %this;
    %this.add(spaceNameField);
    profile = GuiTextCtrl @ new ""() @ "InfoWindowNonModalTextProfile";
    0;
    horizSizing = %this @ "right";
    vertSizing = "bottom";
    %ypos = (22.0 + %ypos);
    position = 0 @ " ";
    extent = "100 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Description: ";
    maxLength = 64;
    %this.add();
    profile = GuiTextEditCtrl @ new ""() @ "InfoWindowTextEditProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    %ypos = (20.0 + %ypos);
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
    spaceDescField = %this;
    %this.add(spaceDescField);
    profile = GuiTextCtrl @ new ""() @ "InfoWindowNonModalTextProfile";
    0;
    horizSizing = %this @ "right";
    vertSizing = "bottom";
    %ypos = (27.0 + %ypos);
    position = 0 @ " ";
    extent = "68 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Access:";
    maxLength = 64;
    %this.add();
    profile = GuiRadioCtrl @ new ""() @ "InfoWindowRadioButtonProfile";
    0;
    groupNum = 1;
    buttonType = "RadioButton";
    horizSizing = "right";
    vertSizing = "bottom";
    %ypos = (20.0 + %ypos);
    position = 0 @ " ";
    extent = "68 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Open";
    command = "OPSpaceHud.accessSelected(\"Open\");";
    maxLength = 64;
    AccessOptAnyone = %this;
    %this.add(AccessOptAnyone);
    profile = GuiRadioCtrl @ new ""() @ "InfoWindowRadioButtonProfile";
    0;
    groupNum = %this @ 1;
    buttonType = "RadioButton";
    horizSizing = "right";
    vertSizing = "bottom";
    %ypos = (20.0 + %ypos);
    position = 0 @ " ";
    extent = "120 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Friends Only";
    command = "OPSpaceHud.accessSelected(\"FriendsOnly\");";
    maxLength = 64;
    AccessOptFriends = %this;
    %this.add(AccessOptFriends);
    profile = GuiTextCtrl @ new ""() @ "InfoWindowNonModalTextProfile";
    0;
    horizSizing = %this @ "right";
    vertSizing = "bottom";
    %ypos = (20.0 + %ypos);
    position = 0 @ " ";
    extent = "68 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Music: ";
    maxLength = 64;
    %this.add();
    profile = GuiPopUp2MenuCtrl @ new ""() @ "InfoWindowPopupProfile";
    0;
    scrollProfile = "DottedScrollProfile";
    winProfile = "InfoWindowPopupWindowProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    %ypos = (20.0 + %ypos);
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
    MusicStreamDropdown = %this;
    %this.add(MusicStreamDropdown);
};
function OPSpaceHud::accessSelected(%this, %accessLevel) {
    if (!(%this $= accessLevel)) {
        CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), "", %accessLevel, "", "");
    }
    accessLevel = %accessLevel @ %accessLevel @ %this;
};
function OPSpaceHud::descriptionChanged(%this) {
    %description = spaceDescField.getValue();
    %this;
    if (!(%this SPC description $= %description)) {
        CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), %description, "", "", "");
    }
    description = %description @ %this;
};
function OPSpaceHud::MusicSelected(%this) {
    %selection = MusicStreamDropdown.getText();
    %this;
    if ((%selection $= "")) {
        error("Empty string was selected on MusicStreamDropdown. Not sending request. Check that the server sent StreamID was in the $musicStreamIDMap checked by PrivSpaceHud::updateMusic <-" @ getScopeName());
        return;
    }
    if (!(%this $= musicStream)) {
        if (isObject($musicStreamNameMap)) {
            log("communication", "debug", "getting the stream name from the musicStreamMap which is" @ " " @ $musicStreamNameMap);
            %streamID = $musicStreamNameMap.get(%selection);
            %selection;
        }
        warn("the MusicStreamMap variable is not defined. We cannot get the music stream mapping.. so using PrivateSpace (the default)");
        %streamID = "PrivateSpace";
        echo("setting music stream id = " @ %streamID);
        customSpace::SetMusicStreamID(%streamID);
    }
    musicStream = %selection @ %this;
};
function OPSpaceHud::updateMusic(%this, %newStreamName) {
    if ((%newStreamName $= "")) {
        warn(getScopeName() @ "-> received an empty string for stream name.");
    }
    musicStream = %newStreamName @ %this;
    if ((%this != MusicStreamDropdown.size())) {
        %index = MusicStreamDropdown.findText(%newStreamName);
        %this;
        if ((0.0 < %index)) {
            warn(0.0 @ "Some music streams are loaded, but the latest update is not in the dropdown!<-" @ getScopeName());
        }
        MusicStreamDropdown.SetSelected(%index);
    }
    warn(%this @ "Tried to set selected music stream on updating OPSpaceHud settings, but the music wasn't loaded!<-" @ getScopeName());
};
function OPSpaceHud::updateStreams(%this, %streamList) {
    MusicStreamDropdown.fillFromList(%streamList);
    %selectedIndex = 0;
    %this;
    if (!(%this SPC musicStream $= "")) {
        %selectedIndex = MusicStreamDropdown.findText(musicStream);
        %this;
    }
    MusicStreamDropdown.SetSelected(%selectedIndex);
};
function OPSpaceHud::updateSettings(%this, %name, %description, %accessMode) {
    description = %description @ %this;
    spaceDescField.setText(%description);
    accessLevel = %this @ %accessMode @ %this;
    if ((%accessMode $= "Open")) {
        AccessOptAnyone.performClick();
    }
    if ((%this SPC %accessMode $= "FriendsOnly")) {
        AccessOptFriends.performClick();
    }
    accessLevel = %this @ "Open" @ %this;
    AccessOptAnyone.performClick();
    spaceNameField.setText(%name);
};
function NonOPSpaceHud::setup(%this) {
    %ypos = 0;
    profile = GuiTextCtrl @ new ""() @ "InfoWindowNonModalTextProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = 0 @ " " @ %ypos;
    extent = "68 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Name: ";
    maxLength = 64;
    %this.add();
    profile = GuiMLTextCtrl @ new ""() @ "InfoWindowTextProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = 60 @ " " @ %ypos;
    extent = "170 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "";
    maxLength = 64;
    spaceNameField = %this;
    %this.add(spaceNameField);
    profile = GuiTextCtrl @ new ""() @ "InfoWindowNonModalTextProfile";
    0;
    horizSizing = %this @ "right";
    vertSizing = "bottom";
    %ypos = (20.0 + %ypos);
    position = 0 @ " ";
    extent = "68 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Host: ";
    maxLength = 64;
    %this.add();
    profile = new GuiMLTextCtrl(NonOPSpaceHudOwnerField) @ "InfoWindowTextProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = 60 @ " " @ %ypos;
    extent = "170 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "";
    maxLength = 64;
    spaceOwnerField = %this;
    %this.add(spaceOwnerField);
    profile = GuiTextCtrl @ new ""() @ "InfoWindowNonModalTextProfile";
    0;
    horizSizing = %this @ "right";
    vertSizing = "bottom";
    %ypos = (35.0 + %ypos);
    position = 0 @ " ";
    extent = "85 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Description: ";
    maxLength = 64;
    %this.add();
    profile = GuiTextCtrl @ new ""() @ "InfoWindowNonModalTextProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    %ypos = (20.0 + %ypos);
    position = 0 @ " ";
    extent = "215 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "a description";
    maxLength = 64;
    spaceDescField = %this;
    %this.add(spaceDescField);
    profile = GuiTextCtrl @ new ""() @ "InfoWindowNonModalTextProfile";
    0;
    horizSizing = %this @ "right";
    vertSizing = "bottom";
    %ypos = (40.0 + %ypos);
    position = 0 @ " ";
    extent = "68 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Music: ";
    maxLength = 64;
    %this.add();
    profile = GuiTextCtrl @ new ""() @ "InfoWindowNonModalTextProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    %ypos = (20.0 + %ypos);
    position = 0 @ " ";
    extent = "215 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "My apartment radio ";
    maxLength = 64;
    musicStreamField = %this;
    %this.add(musicStreamField);
    profile = GuiMLTextCtrl @ new ""() @ "InfoWindowTextProfile";
    0;
    horizSizing = %this @ "right";
    vertSizing = "bottom";
    %ypos = (30.0 + %ypos);
    position = 0 @ " ";
    extent = "215 200";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "";
    maxLength = 64;
    bigMLText = %this;
    %this.add(bigMLText);
};
function NonOPSpaceHud::updateSettings(%this, %name, %description, %owner) {
    if (!(%owner $= "")) {
        spaceOwnerField.setText(%this @ "<a:owner " @ munge(%owner) @ ">" @ %owner @ "</a>");
    }
    spaceOwnerField.setText("<a:noowner >Take Control</a>");
    spaceDescField.setText(%description);
    spaceNameField.setText(%name);
};
function NonOPSpaceHud::updateMusic(%this, %newStreamName) {
    musicStreamField.setText(%newStreamName);
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
    if (!($CSSpaceInfo SPC owner $= $Player::Name)) {
        return;
    }
    if (!(%description $= "")) {
        description = %description @ $CSSpaceInfo;
    }
    if (!(%accessMode $= "")) {
        access = %accessMode @ $CSSpaceInfo;
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
    className = ManagerRequest @ new ""() @ "ModifyCustomSpaceSettingsRequest";
    0;
    %request = ;
    if (isObject()) {
        %request.add();
    }
    %url = MissionCleanup @ MissionCleanup @ $Net::ClientServiceURL @ "/SaveCustomSpaceSettings?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "space=" @ urlEncode(%spaceName);
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
    requestDescription = %description @ %request;
    requestAccessMode = %accessMode @ %request;
    if (!(%request SPC requestDescription $= "")) {
        incrementRequestCount();
    }
    if (!(%request SPC requestAccessMode $= "")) {
        incrementRequestCount();
    }
};
function ModifyCustomSpaceSettingsRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() @ ":" @ %status);
    if ((%status $= "fail")) {
        warn("network", getScopeName() @ " request failed: " @ %this.getValue("statusMessage"));
    }
    %this.schedule(0, "delete");
    if (!(%this SPC requestDescription $= "")) {
        decrementRequestCount();
    }
    if (!(%this SPC requestAccessMode $= "")) {
        decrementRequestCount();
    }
};
function ModifyCustomSpaceSettingsRequest::onError(%this, %unused, %errMsg) {
    error("network", getScopeName() @ ":" @ %errMsg);
    %this.schedule(0, "delete");
};
function CustomSpaceSettings::changeSpaceOwnership(%spaceName, %takeOwnershipBool) {
    className = ManagerRequest @ new ""() @ "ChangeSpaceOwnershipRequest";
    0;
    %request = ;
    if (isObject()) {
        %request.add();
    }
    %url = MissionCleanup @ MissionCleanup @ $Net::BaseURL @ "?cmd=ChangeSpaceOwnership" @ "&user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token) @ "&space=" @ urlEncode(%spaceName) @ "&take=" @ urlEncode(%takeOwnershipBool ? 1 : 0);
    log("network", "info", getScopeName() @ ":" @ %url);
    %request.setURL(%url);
    spaceName = %spaceName @ %request;
    %request.start();
};
function ChangeSpaceOwnershipRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    %statusMsg = %this.getValue("statusMsg");
    log("network", "info", getScopeName() @ ":" @ %status @ " - msg: " @ %statusMsg);
    if ((%status $= "success")) {
        "private space".selectTabWithName();
    }
    if ((HudTabs SPC trim(getWords(%statusMsg, 0, 1)) $= "fail already-owned")) {
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
    if (!(filledPrivateSpaceTab)) {
        return %this;
    }
    %hasPerm = $player.rolesPermissionCheckNoWarn("fly");
    %hasPerm.setVisible();
    %hasPerm.setVisible();
    if (%hasPerm) {
    }
    if ((SpaceSurfTE SPC $gMode $= "PrivateSpaceGrid")) {
        %this.showTabWithName("private space");
    }
};
function SpaceSurfText::onURL(%this, %url) {
    teleportToAdjacentSpace((%url $= "spaceN"));
};
function SpaceSurfTE::onEnter(%this) {
    teleportToSpaceNumber(%this.getValue());
};
