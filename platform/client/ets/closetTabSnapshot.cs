function ClosetTabs::fillProfileTab(%this) {
    %theTab = %this.getTabWithName("SNAPSHOT");
    if (!(isObject(%theTab))) {
        return;
    }
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "26 26";
    extent = "571 37";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/closet_tabs_bracket";
    %theTab.add();
    profile = GuiMLTextCtrl @ new ""() @ "ClosetLargeLinkProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "26 97";
    extent = "238 36";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    maxChars = -1;
    text = ;
    %theTab.add();
    profile = new GuiBitmapCtrl(ProfileCurrentPicture) @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "82 158";
    extent = "126 126";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "";
    %theTab.add();
    profile = GuiWindowCtrl @ new ""() @ "CornersWindowProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "77 153";
    extent = "136 136";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    resizeWidth = 0;
    resizeHeight = 0;
    canMove = 0;
    canClose = 0;
    canMinimize = 0;
    canMaximize = 0;
    %theTab.add();
    profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton15NonDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "114 294";
    extent = "99 15";
    minExtent = "1 1";
    visible = 1;
    command = "doEditProfile();";
    text = "View Your Profile";
    buttonType = "PushButton";
    drawText = 1;
    %theTab.add();
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "35 336";
    extent = "216 77";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/bulb_box";
    profile = GuiMLTextCtrl @ new ""() @ "ClosetLargeLinkProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "23 12";
    extent = "189 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    maxChars = -1;
    text = ;
    %theTab.add();
    profile = new GuiBitmapCtrl(ProfileBackgroundImage) @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "299 89";
    extent = "360 360";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/backgrounds/1";
    systemDragDrop = 0;
    %background = ;
    background = %background @ %theTab;
    %theTab.add(%background);
    profile = GuiControl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "299 89";
    extent = "360 360";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    profile = new GuiControl(ProfileSnapRegion) @ "ETSNonModalProfile";
    horizSizing = "width";
    vertSizing = "height";
    position = "0 0";
    extent = "360 360";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 0;
    %maskFrame = ;
    profile = GuiControl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "-2 -2";
    extent = "364 364";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    %snapFrame = ;
    profile = new GuiObjectView(ProfileObjectView) @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "-195 -700";
    extent = 759 @ " " @ (2.0 * 859.0);
    minExtent = "1 1";
    sluggishness = 1;
    visible = 1;
    cameraZRot = 0;
    forceFOV = 0;
    cameraXRotMin = -0.1;
    cameraXRotDef = 0;
    cameraXRotMax = 0.3;
    cameraZRotMin = -10000;
    cameraZRotDef = 0.0;
    cameraZRotMax = 10000;
    orbitDistDef = 2.4;
    orbitDistMin = 0.8;
    orbitDistMax = 10.0;
    mouseWheelSpeed = 0.3;
    fov = 40;
    leftMouseFunc = "";
    rightMouseFunc = "rotate";
    %objView = ;
    %objView.setOrbitDist(2.4);
    %objView.setLightDirection("0 3 -2");
    %objView.moveBy("0 0");
    objView = %objView @ %theTab;
    %snapFrame.add(%objView);
    %maskFrame.add(%snapFrame);
    %theTab.add(%maskFrame);
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "295 85";
    extent = "368 368";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/largeSnapFrame";
    %theTab.add();
    profile = GuiTextCtrl @ new ""() @ "ClosetLeftInfoProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "297 464";
    extent = "98 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Choose Background";
    %theTab.add();
    profile = new GuiVariableWidthButtonCtrl(ProfilePreviousBackgroundButton) @ "BracketButton19NonFocusProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "295 493";
    extent = "30 19";
    minExtent = "1 1";
    visible = 1;
    command = "ProfileBackgroundChooser.moveBy(1);";
    text = "<<";
    buttonType = "PushButton";
    drawText = 1;
    repeatDelayMS = 360;
    tickPeriodMS = 120;
    %theTab.add();
    0.setActive();
    profile = GuiScrollCtrl @ new ""() @ "DottedScrollProfile";
    0;
    position = ProfilePreviousBackgroundButton @ "328 476";
    extent = "303 55";
    minExtent = "1 1";
    horizSizing = "right";
    vertSizing = "bottom";
    visible = 1;
    hScrollBar = "alwaysOff";
    vScrollBar = "alwaysOff";
    constantThumbHeight = 1;
    %chooserScroll = ;
    profile = new GuiArray2Ctrl(ProfileBackgroundChooser) @ "GuiDefaultProfile";
    childrenClassName = "GuiControl";
    childrenExtent = "47 47";
    spacing = 3;
    numRowsOrCols = 1;
    inRows = 1;
    sluggishness = 1;
    minExtent = "303 55";
    %backgroundChooser = ;
    %chooserScroll.add(%backgroundChooser);
    %theTab.add(%chooserScroll);
    profile = new GuiVariableWidthButtonCtrl(ProfileNextBackgroundButton) @ "BracketButton19NonFocusProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "633 493";
    extent = "30 19";
    minExtent = "1 1";
    visible = 1;
    command = "ProfileBackgroundChooser.moveBy(-1);";
    text = ">>";
    buttonType = "PushButton";
    drawText = 1;
    repeatDelayMS = 360;
    tickPeriodMS = 120;
    %theTab.add();
    profile = GuiWindowCtrl @ new ""() @ "DottedWindowProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "335 530";
    extent = "289 20";
    canHilite = 1;
    resizeWidth = 0;
    resizeHeight = 0;
    canMove = 0;
    canClose = 0;
    canMinimize = 0;
    canMaximize = 0;
    profile = new GuiTextEditCtrl(ProfileBackgroundURLField) @ "InfoWindowTextEditInvisibleOnWhiteProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = "289 20";
    altCommand = "$ThisControl.OnEnterKey();";
    text = ".. or enter an HTTP image URL here ..";
    isDefault = 1;
    %theTab.add();
    profile = GuiWindowCtrl @ new ""() @ "DottedWindowProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "688 85";
    extent = "241 325";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    resizeWidth = 0;
    resizeHeight = 0;
    canMove = 0;
    canClose = 0;
    canMinimize = 0;
    canMaximize = 0;
    closeCommand = "";
    canHilite = 0;
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "15 13";
    extent = "17 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/zoom_icon";
    profile = GuiTextCtrl @ new ""() @ "ClosetLeftInfoProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "37 13";
    extent = "97 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Zoom (scroll wheel)";
    maxLength = 255;
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiButtonProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "15 40";
    extent = "27 25";
    minExtent = "1 1";
    visible = 1;
    command = "ProfileObjectView.zoomBy(-0.3);";
    text = "";
    buttonType = "PushButton";
    bitmap = "platform/client/buttons/zoom_in";
    drawText = 0;
    repeatDelayMS = 360;
    tickPeriodMS = 120;
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiButtonProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "55 40";
    extent = "27 25";
    minExtent = "1 1";
    visible = 1;
    command = "ProfileObjectView.zoomBy(0.3);";
    text = "";
    buttonType = "PushButton";
    bitmap = "platform/client/buttons/zoom_out";
    drawText = 0;
    repeatDelayMS = 360;
    tickPeriodMS = 120;
    position = new GuiMLTextCtrl(ProfilePresetViews) @ "133 15";
    extent = "100 1";
    text = "<just:right>Views<br><linkcolor:dd00dd><linkcolorhl:3300ee><a:gamelink:SETVIEW default>default</a><br><a:gamelink:SETVIEW body>body</a><br><a:gamelink:SETVIEW face>face</a><br><a:gamelink:SETVIEW offscreen>offscreen</a>";
    stripGamelink = 1;
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "13 85";
    extent = "26 26";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/move_icon";
    profile = GuiTextCtrl @ new ""() @ "ClosetLeftInfoProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "43 89";
    extent = "93 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Move (arrow keys)";
    maxLength = 255;
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiButtonProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "35 120";
    extent = "17 15";
    minExtent = "1 1";
    visible = 1;
    command = "ProfileObjectView.moveBy(0, -1);";
    text = "";
    buttonType = "PushButton";
    bitmap = "platform/client/buttons/tri_u";
    drawText = 0;
    repeatDelayMS = 360;
    tickPeriodMS = 120;
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiButtonProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "15 132";
    extent = "15 17";
    minExtent = "1 1";
    visible = 1;
    command = "ProfileObjectView.moveBy(-1, 0);";
    text = "";
    buttonType = "PushButton";
    bitmap = "platform/client/buttons/tri_l";
    drawText = 0;
    repeatDelayMS = 360;
    tickPeriodMS = 120;
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiButtonProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "57 132";
    extent = "15 17";
    minExtent = "1 1";
    visible = 1;
    command = "ProfileObjectView.moveBy(1, 0);";
    text = "";
    buttonType = "PushButton";
    bitmap = "platform/client/buttons/tri_r";
    drawText = 0;
    repeatDelayMS = 360;
    tickPeriodMS = 120;
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiButtonProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "35 144";
    extent = "17 15";
    minExtent = "1 1";
    visible = 1;
    command = "ProfileObjectView.moveBy(0, 1);";
    text = "";
    buttonType = "PushButton";
    bitmap = "platform/client/buttons/tri_d";
    drawText = 0;
    repeatDelayMS = 360;
    tickPeriodMS = 120;
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "15 179";
    extent = "28 31";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/turn_icon";
    profile = GuiTextCtrl @ new ""() @ "ClosetLeftInfoProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "48 186";
    extent = "122 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Turn (right mouse button)";
    maxLength = 255;
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiButtonProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "15 213";
    extent = "27 25";
    minExtent = "1 1";
    visible = 1;
    command = "ProfileObjectView.rotateBy(0, 0, -0.2);";
    text = "";
    buttonType = "PushButton";
    bitmap = "platform/client/buttons/turn_right";
    drawText = 0;
    repeatDelayMS = 360;
    tickPeriodMS = 120;
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiButtonProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "55 213";
    extent = "27 25";
    minExtent = "1 1";
    visible = 1;
    command = "ProfileObjectView.rotateBy(0, 0, 0.2);";
    text = "";
    buttonType = "PushButton";
    bitmap = "platform/client/buttons/turn_left";
    drawText = 0;
    repeatDelayMS = 360;
    tickPeriodMS = 120;
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "10 255";
    extent = "27 34";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/pose_icon";
    profile = GuiTextCtrl @ new ""() @ "ClosetLeftInfoProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "44 263";
    extent = "24 18";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Pose";
    maxLength = 255;
    profile = new GuiPopUp2MenuCtrl(ProfilePosePopup) @ "ClosetPopupProfile";
    scrollProfile = "DottedScrollProfile";
    winProfile = "ClosetPopupWindowProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "14 292";
    extent = "152 24";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    maxLength = 255;
    maxPopupHeight = 200;
    allowReverse = 0;
    profile = new GuiMLTextCtrl(ProfilePoseReplay) @ "ClosetSmallLinkProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "181 292";
    extent = "152 80";
    text = "<a:REPLAY>Replay</a>";
    visible = 0;
    position = GuiBitmapButtonCtrl @ new ""() @ "185 284";
    extent = "32 38";
    command = "ProfileObjectView.setLightDirection(getRandom() * 2 - 1 SPC getRandom() * 2 - 1 SPC getRandom() * 2 - 1);";
    bitmap = "platform/client/buttons/light_dir";
    tooltip = "change light direction";
    position = GuiBitmapButtonCtrl @ new ""() @ "207 284";
    extent = "32 38";
    command = "ProfileObjectView.setLightColor    (getRandom() * 2 - 1 SPC getRandom() * 2 - 1 SPC getRandom() * 2 - 1);";
    bitmap = "platform/client/buttons/light_col";
    tooltip = "change light color";
    %theTab.add();
    "photo-pose-01".add();
    "photo-pose-02".add();
    "photo-pose-03".add();
    "photo-pose-04".add();
    "photo-pose-05".add();
    "photo-pose-06".add();
    "photo-pose-07".add();
    "photo-pose-08".add();
    "angry".add();
    "boo".add();
    "confused".add();
    "cry".add();
    "embarrassed".add();
    "flirt".add();
    "hmm".add();
    "in-love".add();
    "lol".add();
    "rotfl".add();
    "sad".add();
    "scared".add();
    "sleepy".add();
    "smile".add();
    "surprised".add();
    "thinking".add();
    "applause".add();
    "busy".add();
    "cool".add();
    "doh".add();
    "kiss".add();
    "not-listening".add();
    "shhh".add();
    "sit".add();
    "talk-to-the-hand".add();
    "vomit".add();
    "waiting".add();
    "wave".add();
    "whew".add();
    "vside".add();
    "Pose".setText();
    horizSizing = ProfilePosePopup @ new GuiControl(ProfileSnapshotButton_Container) @ "right";
    ProfilePosePopup;
    vertSizing = ProfilePosePopup @ ProfilePosePopup @ "bottom";
    ProfilePosePopup;
    position = ProfilePosePopup @ ProfilePosePopup @ "688 423";
    ProfilePosePopup;
    extent = ProfilePosePopup @ ProfilePosePopup @ "241 42";
    ProfilePosePopup;
    minExtent = ProfilePosePopup @ ProfilePosePopup @ "1 1";
    ProfilePosePopup;
    sluggishness = ProfilePosePopup @ ProfilePosePopup @ -1;
    ProfilePosePopup;
    visible = ProfilePosePopup @ ProfilePosePopup @ 1;
    ProfilePosePopup;
    %theTab.add(ProfilePosePopup);
    profile = ProfileSnapshotButton_Container @ new GuiBitmapButtonCtrl(ProfileSnapshotButton) @ "GuiDefaultProfile";
    ProfilePosePopup;
    horizSizing = ProfilePosePopup @ ProfilePosePopup @ "right";
    ProfilePosePopup;
    vertSizing = ProfilePosePopup @ ProfilePosePopup @ "bottom";
    ProfilePosePopup;
    position = ProfilePosePopup @ ProfilePosePopup @ "0 0";
    ProfilePosePopup;
    extent = ProfilePosePopup @ ProfilePosePopup @ "241 42";
    ProfilePosePopup;
    minExtent = ProfilePosePopup @ ProfilePosePopup @ "1 1";
    ProfilePosePopup;
    sluggishness = ProfilePosePopup @ ProfilePosePopup @ -1;
    visible = 1;
    command = "ProfileSnapRegion.prepareSnapshot();";
    text = "";
    groupNum = -1;
    buttonType = "PushButton";
    bitmap = "platform/client/buttons/take_snapshot";
    drawText = 0;
    .add();
    %wi = AnimCtrl::newAnimCtrl("31 13", "18 18");
    %wi.setDelay(60);
    %wi.addFrame("platform/client/ui/wait0.png");
    %wi.addFrame("platform/client/ui/wait1.png");
    %wi.addFrame("platform/client/ui/wait2.png");
    %wi.addFrame("platform/client/ui/wait3.png");
    %wi.addFrame("platform/client/ui/wait4.png");
    %wi.addFrame("platform/client/ui/wait5.png");
    %wi.addFrame("platform/client/ui/wait6.png");
    %wi.addFrame("platform/client/ui/wait7.png");
    %wi.add();
    waitIcon = ProfileSnapshotButton_Container @ %wi @ ProfileSnapshotButton_Container;
    %wi.setVisible(0);
    profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton19Profile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "829 519";
    extent = "43 19";
    minExtent = "1 1";
    visible = 1;
    command = "ClosetGui.close(false);";
    text = "Done";
    buttonType = "PushButton";
    drawText = 1;
    %doneButton = ;
    profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton19NonDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "882 519";
    extent = "52 19";
    minExtent = "1 1";
    visible = 1;
    command = "ClosetGui.close(true);";
    text = "Cancel";
    buttonType = "PushButton";
    drawText = 1;
    %cancelButton = ;
    %theTab.add(%doneButton);
    doneButton = %doneButton @ %theTab;
    %theTab.add(%cancelButton);
    cancelButton = %cancelButton @ %theTab;
    tabSnapshotInitialized = 1 @ %this;
};
function ProfilePosePopup::onSelect(%this, %unused, %entries) {
    if (!(%entries $= "")) {
        %anim = convertWordToAnim(%entries);
        $player.playAnim(%anim);
        1.setVisible();
    }
};
function ProfilePoseReplay::onURL(%this, %url) {
    %anim = getText();
    ProfilePosePopup;
    if (!(%anim $= "pose")) {
        %anim = convertWordToAnim(%anim);
        $player.playAnim(%anim);
    }
};
function ProfileCurrentPicture::update(%this, %url) {
    if ($StandAlone) {
        return;
    }
    if (!(%url $= "")) {
        $Player::hasSeenTakeAvatarPhotoDialog = 1;
    }
    %curl = new ""();
    URLPostObject;
    %curl.setName("ProfileAvatarPictureRequest");
    if ((0 SPC %url $= "")) {
        %url = $Net::AvatarURL @ urlEncode($player.getShapeName());
    }
    %curl.setURL(%url);
    %curl.setDownloadFile(%this.getLocalFileName(1));
    %curl.setRecvData(1);
    %curl.setCompletedCallback("ProfileAvatarPictureRequestOnCompleted");
    if (!(%curl.start())) {
        %curl.delete();
        warn("ProfileCurrentPicture::update(): couldn't start dynamic download of avatar pic.");
        return;
    }
    if (isObject()) {
        %curl.add();
    }
    echo("ProfileCurrentPicture::update(): started dynamic download of avatar pic");
};
function ProfileAvatarPictureRequestOnCompleted(%request, %result) {
    if ((0.0 == %result)) {
        $Player::Name.setProperty("hasTakenAvatarPhoto", 1);
        %fileName = %request.getDownloadFile();
        gUserPropMgrClient;
        removeFile(%fileName);
        addFile(%fileName);
        "".setBitmap();
        %fileName.setBitmap();
        %request.getURL().purgeCacheEntry();
        dlMgr @ %request.getURL() @ "?size=S".purgeCacheEntry();
        dlMgr @ %request.getURL() @ "?size=M".purgeCacheEntry();
        dlMgr @ %request.getURL() @ "?size=L".purgeCacheEntry();
    }
    %retryCount = 1;
    dlMgr;
    if (!($Player::Name.getProperty("hasTakenAvatarPhoto", 0))) {
    }
    if ((%retryCount < $Player::attemptsToAutoUploadAvatarSnapshot)) {
    }
    if ((ClosetGui SPC lastTabOpened $= "SNAPSHOT")) {
    }
    if (isVisible()) {
        200.schedule();
        $Player::attemptsToAutoUploadAvatarSnapshot = (1.0 + $Player::attemptsToAutoUploadAvatarSnapshot);
        prepareSnapshot;
    }
};
function ProfileCurrentPicture::getLocalFileName(%this, %includeExtention) {
    if (($Pref::Video::screenShotFormat $= "JPEG")) {
        %ext = ".jpg";
    }
    if (($Pref::Video::screenShotFormat $= "PNG")) {
        %ext = ".png";
    }
    %ext = ".png";
    if (%includeExtention) {
    }
    return %ext @ "";
};
function ProfileSnapRegion::prepareSnapshot(%this) {
    if (returnClosetGuiFUE) {
        0.setVisible();
        waitAFrameAndCall("ProfileSnapRegion_doSnapshot");
        return ClosetGuiFUE;
    }
    ProfileSnapRegion_doSnapshot();
};
function ProfileSnapRegion_doSnapshot() {
    doSnapshot();
};
function ProfileSnapRegion::doSnapshot(%this) {
    0.setActive();
    waitIcon.setVisible(1);
    waitIcon.start();
    %reg = %this.getScreenPosition() @ " " @ %this.getExtent();
    ProfileSnapshotButton_Container;
    %snapshot = snapshot::snapAndUpRegion(%reg, 0.getLocalFileName(), "n");
    ProfileCurrentPicture;
    %snapshot.setCompletedCallback("ProfileSnapRegionOnCompleted");
    saveObject = ProfileSnapshotButton_Container @ %this @ %snapshot;
    ProfileSnapshotButton;
    if (returnClosetGuiFUE) {
        1.setVisible();
    }
};
function ProfileSnapRegion::onProgress(%this, %unused) {
};
function ProfileSnapRegionOnCompleted(%request, %result) {
    %snapRegion = saveObject;
    %request;
    if ((0.0 == %result)) {
        echo("Profile pic upload done -- downloading");
        echo("photoURL =" @ " " @ %request.getResult("photoURL"));
        waitIcon.stop();
        waitIcon.setVisible(0);
        1.setActive();
        %request.getResult("photoURL").update();
    }
    warn("Profile pic upload error");
    waitIcon.stop();
    waitIcon.setVisible(0);
    1.setActive();
};
function ProfileBackgroundChooser::onCreatedChild(%this, %child) {
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "3 3";
    extent = "39 39";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "";
    %thumb = ;
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "3 3";
    extent = "42 42";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    command = "";
    text = "";
    groupNum = -1;
    buttonType = "PushButton";
    bitmap = "platform/client/buttons/sm_frame";
    drawText = 0;
    %frame = ;
    command = "ProfileBackgroundChooser.thumbClicked(" @ %child.getId() @ ");" @ %frame;
    %child.add(%thumb);
    %child.add(%frame);
    thumb = %thumb @ %child;
    frame = %frame @ %child;
};
function ProfileBackgroundChooser::Initialize(%this) {
    if (!(initialized)) {
        %bgdPath = "platform/client/ui/backgrounds/";
        %this;
        %images = getPathsMatchingPattern(%bgdPath @ "*");
        %count = getFieldCount(%images);
        %numImages = 0;
        %i = 0;
        if ((%count < %i)) {
            %fileName = fileName(getField(%images, %i));
            if ((getSubStr(%fileName, 0, 3) $= "sm_")) {
                %extension = strrchr(%fileName, ".");
                if ((%extension $= ".jpg")) {
                    %numImages = (1.0 + %numImages);
                }
                if ((%extension $= ".png")) {
                    %numImages = (1.0 + %numImages);
                }
            }
            %i = (1.0 + %i);
        }
        %this.setNumChildren(%numImages);
        %i = 0;
        (%count < %i);
        if ((%numImages < %i)) {
            %cell = %this.getObject(%i);
            index = %i @ %cell;
            thumb.setBitmap(%cell @ %bgdPath @ "sm_" @ (1.0 + %i));
            thumbBitmapName = %bgdPath @ "sm_" @ (1.0 + %i) @ %cell;
            bitmapName = %bgdPath @ (1.0 + %i) @ ".jpg" @ %cell;
            %i = (1.0 + %i);
        }
        selected = (%numImages < %i) @ -(1.0) @ %this;
        %this.selectThumbAtIndex(0);
        initialized = 1 @ %this;
    }
};
function ProfileBackgroundChooser::selectThumbAtIndex(%this, %index) {
    if ((%this == selected)) {
        return %index;
    }
    if ((%this >= selected)) {
        %cell = %this.getObject(selected);
        %this;
        if (isObject(%cell)) {
            frame.setBitmap("platform/client/buttons/sm_frame");
        }
    }
    selected = %cell @ %index @ %this;
    0.0;
    %cell = %this.getObject(selected);
    %this;
    frame.setBitmap("platform/client/buttons/sm_frame_selected");
    if (isFile(bitmapName)) {
        bitmapName.setBitmap();
    }
    %url = %cell @ bitmapName;
    ProfileBackgroundImage @ %cell @ $Net::downloadURL @ "/packages/";
    thumbBitmapName.setBitmap();
    %url.downloadAndApplyBitmap();
};
function ProfileBackgroundImage::onSystemDragDroppedEvent(%this, %text, %unused) {
    %text = strreplace(%text, "\\", "/");
    if (platformIsFile(%text)) {
        addFile(%text);
        %this.setBitmap("");
        %this.setBitmap(%text);
    }
    %this.downloadAndApplyBitmap(%text);
};
function ProfileBackgroundChooser::thumbClicked(%this, %cell) {
    %this.selectThumbAtIndex(index);
};
function ProfileBackgroundChooser::moveBy(%this, %numSlots) {
    %pos = %this.getTrgPosition();
    %xPos = getWord(%pos, 0);
    %ypos = getWord(%pos, 1);
    %slotWidth = (%this + getWord(childrenExtent, 0));
    spacing;
    %min = (%slotWidth * -((6.0 - %this.getCount())));
    %this;
    %max = 0;
    if (isActive()) {
    }
    if ((%max >= ((%numSlots * %slotWidth) + %xPos))) {
        0.setActive();
    }
    if (!(isActive())) {
        1.setActive();
    }
    if (isActive()) {
    }
    if ((%min <= ((%numSlots * %slotWidth) + %xPos))) {
        0.setActive();
    }
    if (!(isActive())) {
        1.setActive();
    }
    %xPos = mMin(%max, mMax(%min, ((%numSlots * %slotWidth) + %xPos)));
    ProfileNextBackgroundButton;
    %this.setTrgPosition(%xPos, %ypos);
    minExtent = ProfileNextBackgroundButton @ (%this.getCount() * %slotWidth) @ " " @ %slotWidth @ %this;
    ProfileNextBackgroundButton;
};
function ProfileObjectView::moveBy(%this, %dx, %dy) {
    %nudge = ($player.getGender() $= "f") ? "0.4 -0.3 0.8" : "0 -0.1 0.8";
    %this.setLookAtNudge(%nudge);
    %dx = (0.25 * %dx);
    %dy = (0.25 * %dy);
    %x = mMin(3, mMax(-(3.0), (%this + xPos)));
    %dx;
    %y = mMin(4, mMax(-(13.0), (%this + yPos)));
    %dy;
    %this.setMove(%x @ " " @ %y);
};
function ProfileObjectView::setMove(%this, %pos) {
    xPos = getWord(%pos, 0) @ %this;
    yPos = getWord(%pos, 1) @ %this;
    %this.setTrgPosition(((xPos * 50.0) + -(195.0)), ((yPos * 50.0) + -(700.0)));
};
function ProfileObjectView::setView(%this, %viewName) {
    %view = ;
    if ((%view $= "")) {
        error(getScopeName() @ " " @ "- unknown view" @ " " @ %viewName @ " " @ getTrace());
        %view = ;
    }
    %position = getWords(%view, 0, 1);
    %zoom = getWords(%view, 2, 2);
    %this.setMove(%position);
    %this.setOrbitDist(%zoom);
};
function ProfilePresetViews::onURL(%this, %url) {
    %cmd = firstWord(%url);
    if (!(%cmd $= "SETVIEW")) {
        error(getScopeName() @ " " @ "- unknown command" @ " " @ %cmd @ " " @ getTrace());
        return;
    }
    %viewName = restWords(%url);
    %viewName.setView();
};
function ProfileBackgroundURLField::OnEnterKey(%this) {
    %url = %this.getValue();
    if (!(ImageFrameBase_IsPermittedURL(%url))) {
        MessageBoxOK(, , "");
        return;
    }
    %url.downloadAndApplyBitmap();
};
function ProfileBackgroundURLField::onSetFirstResponder(%this) {
    Parent::onSetFirstResponder(%this);
    if (isDefault) {
        isDefault = %this @ 0 @ %this;
        %this.setText("");
    }
    %this.setSelection(0, 1000);
};
function ProfileObjectView::resetLight(%this) {
    %this.setLightDirection("0 3 -2");
    %this.setLightColor("1 1 1");
};
