function ClosetTabs::fillProfileTab(%this) {
    %theTab = %this.getTabWithName("SNAPSHOT");
    if (!(isObject(%theTab))) {
        return;
    }
    0;
    %theTab.add(new ""() {
        profile = GuiBitmapCtrl @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 26";
        extent = "571 37";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closet_tabs_bracket";
    };);
    0;
    %theTab.add(new ""() {
        profile = GuiMLTextCtrl @ "ClosetLargeLinkProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 97";
        extent = "238 36";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        maxChars = -1;
        text = ;
    };);
    %theTab.add(new GuiBitmapCtrl(ProfileCurrentPicture) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "82 158";
        extent = "126 126";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };);
    0;
    %theTab.add(new ""() {
        profile = GuiWindowCtrl @ "CornersWindowProfile";
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
    };);
    0;
    %theTab.add(new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton15NonDefaultProfile";
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
    };);
    0;
    %theTab.add(new ""() {
        profile = GuiBitmapCtrl @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "35 336";
        extent = "216 77";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/bulb_box";
    };);
    %background = new GuiBitmapCtrl(ProfileBackgroundImage) {
        profile = new ""() {
        profile = GuiMLTextCtrl @ "ClosetLargeLinkProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "23 12";
        extent = "189 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        maxChars = -1;
        text = ;
    }; @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "299 89";
        extent = "360 360";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/backgrounds/1";
        systemDragDrop = 0;
    };
    %theTab.background = %background;
    %theTab.add(%background);
    0;
    %maskFrame = new ""() {
        profile = GuiControl @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "299 89";
        extent = "360 360";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    new GuiControl(ProfileSnapRegion) {
        profile = "ETSNonModalProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = "360 360";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    };
    0;
    %snapFrame = new ""() {
        profile = GuiControl @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "-2 -2";
        extent = "364 364";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %objView = new GuiObjectView(ProfileObjectView) {
        profile = "GuiDefaultProfile";
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
    };
    %objView.setOrbitDist(2.4);
    %objView.setLightDirection("0 3 -2");
    %objView.moveBy("0 0");
    %theTab.objView = %objView;
    %snapFrame.add(%objView);
    %maskFrame.add(%snapFrame);
    %theTab.add(%maskFrame);
    0;
    %theTab.add(new ""() {
        profile = GuiBitmapCtrl @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "295 85";
        extent = "368 368";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/largeSnapFrame";
    };);
    0;
    %theTab.add(new ""() {
        profile = GuiTextCtrl @ "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "297 464";
        extent = "98 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Choose Background";
    };);
    %theTab.add(new GuiVariableWidthButtonCtrl(ProfilePreviousBackgroundButton) {
        profile = "BracketButton19NonFocusProfile";
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
    };);
    0.setActive();
    0;
    %chooserScroll = new ""() {
        profile = GuiScrollCtrl @ "DottedScrollProfile";
        position = ProfilePreviousBackgroundButton @ "328 476";
        extent = "303 55";
        minExtent = "1 1";
        horizSizing = "right";
        vertSizing = "bottom";
        visible = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "alwaysOff";
        constantThumbHeight = 1;
    };
    %backgroundChooser = new GuiArray2Ctrl(ProfileBackgroundChooser) {
        profile = "GuiDefaultProfile";
        childrenClassName = "GuiControl";
        childrenExtent = "47 47";
        spacing = 3;
        numRowsOrCols = 1;
        inRows = 1;
        sluggishness = 1;
        minExtent = "303 55";
    };
    %chooserScroll.add(%backgroundChooser);
    %theTab.add(%chooserScroll);
    %theTab.add(new GuiVariableWidthButtonCtrl(ProfileNextBackgroundButton) {
        profile = "BracketButton19NonFocusProfile";
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
    };);
    0;
    %theTab.add(new ""() {
        profile = GuiWindowCtrl @ "DottedWindowProfile";
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
    };);
    0;
    new ""() {
        profile = GuiBitmapCtrl @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "15 13";
        extent = "17 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/zoom_icon";
    };
    new ""() {
        profile = GuiTextCtrl @ "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "37 13";
        extent = "97 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Zoom (scroll wheel)";
        maxLength = 255;
    };
    new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiButtonProfile";
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
    };
    new GuiMLTextCtrl(ProfilePresetViews) {
        position = new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiButtonProfile";
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
    }; @ "133 15";
        extent = "100 1";
        text = "<just:right>Views<br><linkcolor:dd00dd><linkcolorhl:3300ee><a:gamelink:SETVIEW default>default</a><br><a:gamelink:SETVIEW body>body</a><br><a:gamelink:SETVIEW face>face</a><br><a:gamelink:SETVIEW offscreen>offscreen</a>";
        stripGamelink = 1;
    };
    new ""() {
        profile = GuiBitmapCtrl @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "13 85";
        extent = "26 26";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/move_icon";
    };
    new ""() {
        profile = GuiTextCtrl @ "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "43 89";
        extent = "93 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Move (arrow keys)";
        maxLength = 255;
    };
    new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiButtonProfile";
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
    };
    new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiButtonProfile";
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
    };
    new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiButtonProfile";
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
    };
    new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiButtonProfile";
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
    };
    new ""() {
        profile = GuiBitmapCtrl @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "15 179";
        extent = "28 31";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/turn_icon";
    };
    new ""() {
        profile = GuiTextCtrl @ "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "48 186";
        extent = "122 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Turn (right mouse button)";
        maxLength = 255;
    };
    new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiButtonProfile";
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
    };
    new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiButtonProfile";
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
    };
    new ""() {
        profile = GuiBitmapCtrl @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "10 255";
        extent = "27 34";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/pose_icon";
    };
    new GuiMLTextCtrl(ProfilePoseReplay) {
        profile = new GuiPopUp2MenuCtrl(ProfilePosePopup) {
        profile = new ""() {
        profile = GuiTextCtrl @ "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "44 263";
        extent = "24 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Pose";
        maxLength = 255;
    }; @ "ClosetPopupProfile";
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
    }; @ "ClosetSmallLinkProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "181 292";
        extent = "152 80";
        text = "<a:REPLAY>Replay</a>";
        visible = 0;
    };
    new ""() {
        position = GuiBitmapButtonCtrl @ "185 284";
        extent = "32 38";
        command = "ProfileObjectView.setLightDirection(getRandom() * 2 - 1 SPC getRandom() * 2 - 1 SPC getRandom() * 2 - 1);";
        bitmap = "platform/client/buttons/light_dir";
        tooltip = "change light direction";
    };
    %theTab.add(new ""() {
        profile = GuiWindowCtrl @ "DottedWindowProfile";
        horizSizing = new GuiTextEditCtrl(ProfileBackgroundURLField) {
        profile = "InfoWindowTextEditInvisibleOnWhiteProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "289 20";
        altCommand = "$ThisControl.OnEnterKey();";
        text = ".. or enter an HTTP image URL here ..";
        isDefault = 1;
    }; @ "right";
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
    };);
    ProfilePoseReplay.add("photo-pose-01");
    ProfilePoseReplay.add("photo-pose-02");
    ProfilePoseReplay.add("photo-pose-03");
    ProfilePoseReplay.add("photo-pose-04");
    ProfilePoseReplay.add("photo-pose-05");
    ProfilePoseReplay.add("photo-pose-06");
    ProfilePoseReplay.add("photo-pose-07");
    ProfilePoseReplay.add("photo-pose-08");
    ProfilePoseReplay.add("angry");
    ProfilePoseReplay.add("boo");
    ProfilePoseReplay.add("confused");
    ProfilePoseReplay.add("cry");
    ProfilePoseReplay.add("embarrassed");
    ProfilePoseReplay.add("flirt");
    ProfilePoseReplay.add("hmm");
    ProfilePoseReplay.add("in-love");
    ProfilePoseReplay.add("lol");
    ProfilePoseReplay.add("rotfl");
    ProfilePoseReplay.add("sad");
    ProfilePoseReplay.add("scared");
    ProfilePoseReplay.add("sleepy");
    ProfilePoseReplay.add("smile");
    ProfilePoseReplay.add("surprised");
    ProfilePoseReplay.add("thinking");
    ProfilePoseReplay.add("applause");
    ProfilePoseReplay.add("busy");
    ProfilePoseReplay.add("cool");
    ProfilePoseReplay.add("doh");
    ProfilePoseReplay.add("kiss");
    ProfilePoseReplay.add("not-listening");
    ProfilePoseReplay.add("shhh");
    ProfilePoseReplay.add("sit");
    ProfilePoseReplay.add("talk-to-the-hand");
    ProfilePoseReplay.add("vomit");
    ProfilePoseReplay.add("waiting");
    ProfilePoseReplay.add("wave");
    ProfilePoseReplay.add("whew");
    ProfilePoseReplay.add("vside");
    ProfilePoseReplay.setText("Pose");
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePosePopup;
    %theTab.add(new GuiControl(ProfileSnapshotButton_Container) {
        horizSizing = ProfilePosePopup @ "right";
        vertSizing = ProfilePosePopup @ "bottom";
        position = ProfilePosePopup @ "688 423";
        extent = ProfilePosePopup @ "241 42";
        minExtent = ProfilePosePopup @ "1 1";
        sluggishness = ProfilePosePopup @ -1;
        visible = ProfilePosePopup @ 1;
    };);
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePosePopup;
    ProfilePoseReplay.add(new GuiBitmapButtonCtrl(ProfileSnapshotButton) {
        profile = ProfileSnapshotButton_Container @ "GuiDefaultProfile";
        horizSizing = ProfilePosePopup @ "right";
        vertSizing = ProfilePosePopup @ "bottom";
        position = ProfilePosePopup @ "0 0";
        extent = ProfilePosePopup @ "241 42";
        minExtent = ProfilePosePopup @ "1 1";
        sluggishness = ProfilePosePopup @ -1;
        visible = ProfilePosePopup @ 1;
        command = ProfilePosePopup @ "ProfileSnapRegion.prepareSnapshot();";
        text = ProfilePosePopup @ "";
        groupNum = ProfilePosePopup @ -1;
        buttonType = ProfilePosePopup @ "PushButton";
        bitmap = ProfilePosePopup @ "platform/client/buttons/take_snapshot";
        drawText = new ""() {
        position = GuiBitmapButtonCtrl @ "207 284";
        extent = "32 38";
        command = "ProfileObjectView.setLightColor    (getRandom() * 2 - 1 SPC getRandom() * 2 - 1 SPC getRandom() * 2 - 1);";
        bitmap = "platform/client/buttons/light_col";
        tooltip = "change light color";
    }; @ 0;
    };);
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
    waitIcon = %wi @ ProfileSnapshotButton_Container;
    ProfileSnapshotButton_Container;
    %wi.setVisible(0);
    0;
    %doneButton = new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton19Profile";
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
    };
    0;
    %cancelButton = new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton19NonDefaultProfile";
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
    };
    %theTab.add(%doneButton);
    %theTab.doneButton = %doneButton;
    %theTab.add(%cancelButton);
    %theTab.cancelButton = %cancelButton;
    %this.tabSnapshotInitialized = 1;
};
function ProfilePosePopup::onSelect(%this, %unused, %entries) {
    if (!(%entries $= "")) {
        %anim = convertWordToAnim(%entries);
        $player.playAnim(%anim);
        1.setVisible();
    }
};
function ProfilePoseReplay::onURL(%this, %url) {
    %anim = ProfilePosePopup.getText();
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
    %curl = new ""();;
    URLPostObject;
    %curl.setName("ProfileAvatarPictureRequest");
    if ((0 @ " " @ %url $= "")) {
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
    if (isObject(CURLSimGroup)) {
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
        %request.getURL() @ "?size=S".purgeCacheEntry();
        %request.getURL() @ "?size=M".purgeCacheEntry();
        %request.getURL() @ "?size=L".purgeCacheEntry();
    }
    %retryCount = 1;
    dlMgr;
    if (!($Player::Name.getProperty("hasTakenAvatarPhoto", 0))) {
    }
    if ((%retryCount < $Player::attemptsToAutoUploadAvatarSnapshot)) {
    }
    if ((ClosetGui @ " " @ %this.lastTabOpened $= "SNAPSHOT")) {
    }
    if (ClosetGui.isVisible()) {
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
    if (%this.returnClosetGuiFUE) {
        0.setVisible();
        waitAFrameAndCall("ProfileSnapRegion_doSnapshot");
        return ClosetGuiFUE;
    }
    ProfileSnapRegion_doSnapshot();
};
function ProfileSnapRegion_doSnapshot() {
    ProfileSnapRegion.doSnapshot();
};
function ProfileSnapRegion::doSnapshot(%this) {
    0.setActive();
    %this.waitIcon.setVisible(1);
    %this.waitIcon.start();
    %reg = %this.getScreenPosition() @ " " @ %this.getExtent();
    ProfileSnapshotButton_Container;
    %snapshot = snapshot::snapAndUpRegion(%reg, 0.getLocalFileName(), "n");
    ProfileCurrentPicture;
    %snapshot.setCompletedCallback("ProfileSnapRegionOnCompleted");
    %snapshot.saveObject = ProfileSnapshotButton_Container @ %this;
    ProfileSnapshotButton;
    if (%this.returnClosetGuiFUE) {
        1.setVisible();
    }
};
function ProfileSnapRegion::onProgress(%this, %unused) {
};
function ProfileSnapRegionOnCompleted(%request, %result) {
    %snapRegion = %request.saveObject;
    if ((0.0 == %result)) {
        echo("Profile pic upload done -- downloading");
        echo("photoURL =" @ " " @ %request.getResult("photoURL"));
        %request.waitIcon.stop();
        %request.waitIcon.setVisible(0);
        1.setActive();
        %request.getResult("photoURL").update();
    }
    warn("Profile pic upload error");
    %request.waitIcon.stop();
    %request.waitIcon.setVisible(0);
    1.setActive();
};
function ProfileBackgroundChooser::onCreatedChild(%this, %child) {
    0;
    %thumb = new ""() {
        profile = GuiBitmapCtrl @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "3 3";
        extent = "39 39";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };
    0;
    %frame = new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiDefaultProfile";
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
    };
    %frame.command = "ProfileBackgroundChooser.thumbClicked(" @ %child.getId() @ ");";
    %child.add(%thumb);
    %child.add(%frame);
    %child.thumb = %thumb;
    %child.frame = %frame;
};
function ProfileBackgroundChooser::Initialize(%this) {
    if (!(%this.initialized)) {
        %bgdPath = "platform/client/ui/backgrounds/";
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
            %cell.index = %i;
            %cell.thumb.setBitmap(%bgdPath @ "sm_" @ (1.0 + %i));
            %cell.thumbBitmapName = %bgdPath @ "sm_" @ (1.0 + %i);
            %cell.bitmapName = %bgdPath @ (1.0 + %i) @ ".jpg";
            %i = (1.0 + %i);
        }
        %this.selected = (%numImages < %i) @ -(1.0);
        %this.selectThumbAtIndex(0);
        %this.initialized = 1;
    }
};
function ProfileBackgroundChooser::selectThumbAtIndex(%this, %index) {
    if ((%index == %this.selected)) {
        return;
    }
    if ((0.0 >= %this.selected)) {
        %cell = %this.getObject(%this.selected);
        if (isObject(%cell)) {
            %cell.frame.setBitmap("platform/client/buttons/sm_frame");
        }
    }
    %this.selected = %index;
    %cell = %this.getObject(%this.selected);
    %cell.frame.setBitmap("platform/client/buttons/sm_frame_selected");
    if (isFile(%cell.bitmapName)) {
        %cell.bitmapName.setBitmap();
    }
    %url = $Net::downloadURL @ "/packages/" @ %cell.bitmapName;
    ProfileBackgroundImage;
    %cell.thumbBitmapName.setBitmap();
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
    %this.selectThumbAtIndex(%cell.index);
};
function ProfileBackgroundChooser::moveBy(%this, %numSlots) {
    %pos = %this.getTrgPosition();
    %xPos = getWord(%pos, 0);
    %ypos = getWord(%pos, 1);
    %slotWidth = (%this.spacing + getWord(%this.childrenExtent, 0));
    %min = (%slotWidth * -((6.0 - %this.getCount())));
    %max = 0;
    if (ProfilePreviousBackgroundButton.isActive()) {
    }
    if ((%max >= ((%numSlots * %slotWidth) + %xPos))) {
        0.setActive();
    }
    if (!(ProfilePreviousBackgroundButton.isActive())) {
        1.setActive();
    }
    if (ProfileNextBackgroundButton.isActive()) {
    }
    if ((%min <= ((%numSlots * %slotWidth) + %xPos))) {
        0.setActive();
    }
    if (!(ProfileNextBackgroundButton.isActive())) {
        1.setActive();
    }
    %xPos = mMin(%max, mMax(%min, ((%numSlots * %slotWidth) + %xPos)));
    ProfileNextBackgroundButton;
    %this.setTrgPosition(%xPos, %ypos);
    %this.minExtent = ProfileNextBackgroundButton @ (%this.getCount() * %slotWidth) @ " " @ %slotWidth;
    ProfilePreviousBackgroundButton;
};
function ProfileObjectView::moveBy(%this, %dx, %dy) {
    %nudge = ($player.getGender() $= "f") ? "0.4 -0.3 0.8" : "0 -0.1 0.8";
    %this.setLookAtNudge(%nudge);
    %dx = (0.25 * %dx);
    %dy = (0.25 * %dy);
    %x = mMin(3, mMax(-(3.0), (%dx + %this.xPos)));
    %y = mMin(4, mMax(-(13.0), (%dy + %this.yPos)));
    %this.setMove(%x @ " " @ %y);
};
function ProfileObjectView::setMove(%this, %pos) {
    %this.xPos = getWord(%pos, 0);
    %this.yPos = getWord(%pos, 1);
    %this.setTrgPosition(((%this.xPos * 50.0) + -(195.0)), ((%this.yPos * 50.0) + -(700.0)));
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
    if (%this.isDefault) {
        %this.isDefault = 0;
        %this.setText("");
    }
    %this.setSelection(0, 1000);
};
function ProfileObjectView::resetLight(%this) {
    %this.setLightDirection("0 3 -2");
    %this.setLightColor("1 1 1");
};
