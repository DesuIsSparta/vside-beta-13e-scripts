function ClosetTabs::fillProfileTab(%this) {
    %theTab = "SNAPSHOT".getTabWithName(%this);
    if (!(isObject(%theTab))) {
        return;
    }
    new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 26";
        extent = "571 37";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closet_tabs_bracket";
    };.add(%theTab);
    new GuiMLTextCtrl("") {
        profile = "ClosetLargeLinkProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 97";
        extent = "238 36";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        maxChars = -1;
        text = $MsgCat::profile["H-PHOTO"];
    };.add(%theTab);
    new GuiBitmapCtrl(ProfileCurrentPicture) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "82 158";
        extent = "126 126";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };.add(%theTab);
    new GuiWindowCtrl("") {
        profile = "CornersWindowProfile";
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
    };.add(%theTab);
    new GuiVariableWidthButtonCtrl("") {
        profile = "BracketButton15NonDefaultProfile";
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
    };.add(%theTab);
    new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "35 336";
        extent = "216 77";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/bulb_box";
    };.add(%theTab, new GuiMLTextCtrl("") {
        profile = "ClosetLargeLinkProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "23 12";
        extent = "189 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        maxChars = -1;
        text = $MsgCat::profile["H-PROFILE-TIP"];
    };);
    %background = new GuiBitmapCtrl(ProfileBackgroundImage) {
        profile = "GuiDefaultProfile";
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
    %background.add(%theTab);
    %maskFrame = new GuiControl("") {
        profile = "GuiDefaultProfile";
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
    %snapFrame = new GuiControl("") {
        profile = "GuiDefaultProfile";
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
        extent = 759 @ " " @ (859.0 * 2.0);
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
    2.4.setOrbitDist(%objView);
    "0 3 -2".setLightDirection(%objView);
    "0 0".moveBy(%objView);
    %theTab.objView = %objView;
    %objView.add(%snapFrame);
    %snapFrame.add(%maskFrame);
    %maskFrame.add(%theTab);
    new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "295 85";
        extent = "368 368";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/largeSnapFrame";
    };.add(%theTab);
    new GuiTextCtrl("") {
        profile = "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "297 464";
        extent = "98 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Choose Background";
    };.add(%theTab);
    new GuiVariableWidthButtonCtrl(ProfilePreviousBackgroundButton) {
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
    };.add(%theTab);
    0.setActive(ProfilePreviousBackgroundButton);
    %chooserScroll = new GuiScrollCtrl("") {
        profile = "DottedScrollProfile";
        position = "328 476";
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
    %backgroundChooser.add(%chooserScroll);
    %chooserScroll.add(%theTab);
    new GuiVariableWidthButtonCtrl(ProfileNextBackgroundButton) {
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
    };.add(%theTab);
    new GuiWindowCtrl("") {
        profile = "DottedWindowProfile";
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
    };.add(%theTab, new GuiTextEditCtrl(ProfileBackgroundURLField) {
        profile = "InfoWindowTextEditInvisibleOnWhiteProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "289 20";
        altCommand = "$ThisControl.OnEnterKey();";
        text = ".. or enter an HTTP image URL here ..";
        isDefault = 1;
    };);
    new GuiWindowCtrl("") {
        profile = "DottedWindowProfile";
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
    };.add(%theTab);
    "photo-pose-01".add(ProfilePosePopup);
    "photo-pose-02".add(ProfilePosePopup);
    "photo-pose-03".add(ProfilePosePopup);
    "photo-pose-04".add(ProfilePosePopup);
    "photo-pose-05".add(ProfilePosePopup);
    "photo-pose-06".add(ProfilePosePopup);
    "photo-pose-07".add(ProfilePosePopup);
    "photo-pose-08".add(ProfilePosePopup);
    "angry".add(ProfilePosePopup);
    "boo".add(ProfilePosePopup);
    "confused".add(ProfilePosePopup);
    "cry".add(ProfilePosePopup);
    "embarrassed".add(ProfilePosePopup);
    "flirt".add(ProfilePosePopup);
    "hmm".add(ProfilePosePopup);
    "in-love".add(ProfilePosePopup);
    "lol".add(ProfilePosePopup);
    "rotfl".add(ProfilePosePopup);
    "sad".add(ProfilePosePopup);
    "scared".add(ProfilePosePopup);
    "sleepy".add(ProfilePosePopup);
    "smile".add(ProfilePosePopup);
    "surprised".add(ProfilePosePopup);
    "thinking".add(ProfilePosePopup);
    "applause".add(ProfilePosePopup);
    "busy".add(ProfilePosePopup);
    "cool".add(ProfilePosePopup);
    "doh".add(ProfilePosePopup);
    "kiss".add(ProfilePosePopup);
    "not-listening".add(ProfilePosePopup);
    "shhh".add(ProfilePosePopup);
    "sit".add(ProfilePosePopup);
    "talk-to-the-hand".add(ProfilePosePopup);
    "vomit".add(ProfilePosePopup);
    "waiting".add(ProfilePosePopup);
    "wave".add(ProfilePosePopup);
    "whew".add(ProfilePosePopup);
    "vside".add(ProfilePosePopup);
    "Pose".setText(ProfilePosePopup);
    new GuiControl(ProfileSnapshotButton_Container) {
        horizSizing = new GuiBitmapButtonCtrl("") {
        position = new GuiBitmapButtonCtrl("") {
        position = new GuiMLTextCtrl(ProfilePoseReplay) {
        profile = new GuiPopUp2MenuCtrl(ProfilePosePopup) {
        profile = new GuiTextCtrl("") {
        profile = new GuiBitmapCtrl("") {
        profile = new GuiBitmapButtonCtrl("") {
        profile = new GuiBitmapButtonCtrl("") {
        profile = new GuiTextCtrl("") {
        profile = new GuiBitmapCtrl("") {
        profile = new GuiBitmapButtonCtrl("") {
        profile = new GuiBitmapButtonCtrl("") {
        profile = new GuiBitmapButtonCtrl("") {
        profile = new GuiBitmapButtonCtrl("") {
        profile = new GuiTextCtrl("") {
        profile = new GuiBitmapCtrl("") {
        profile = new GuiMLTextCtrl(ProfilePresetViews) {
        position = new GuiBitmapButtonCtrl("") {
        profile = new GuiBitmapButtonCtrl("") {
        profile = new GuiTextCtrl("") {
        profile = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "15 13";
        extent = "17 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/zoom_icon";
    }; @ "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "37 13";
        extent = "97 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Zoom (scroll wheel)";
        maxLength = 255;
    }; @ "GuiButtonProfile";
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
    }; @ "GuiButtonProfile";
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
    }; @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "13 85";
        extent = "26 26";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/move_icon";
    }; @ "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "43 89";
        extent = "93 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Move (arrow keys)";
        maxLength = 255;
    }; @ "GuiButtonProfile";
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
    }; @ "GuiButtonProfile";
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
    }; @ "GuiButtonProfile";
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
    } /* expression truncated */;
    new GuiBitmapButtonCtrl(ProfileSnapshotButton) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "241 42";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "ProfileSnapRegion.prepareSnapshot();";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "platform/client/buttons/take_snapshot";
        drawText = 0;
    };.add(ProfileSnapshotButton_Container);
    %wi = AnimCtrl::newAnimCtrl("31 13", "18 18");
    60.setDelay(%wi);
    "platform/client/ui/wait0.png".addFrame(%wi);
    "platform/client/ui/wait1.png".addFrame(%wi);
    "platform/client/ui/wait2.png".addFrame(%wi);
    "platform/client/ui/wait3.png".addFrame(%wi);
    "platform/client/ui/wait4.png".addFrame(%wi);
    "platform/client/ui/wait5.png".addFrame(%wi);
    "platform/client/ui/wait6.png".addFrame(%wi);
    "platform/client/ui/wait7.png".addFrame(%wi);
    %wi.add(ProfileSnapshotButton_Container);
    ProfileSnapshotButton_Container.waitIcon = %wi;
    0.setVisible(%wi);
    %doneButton = new GuiVariableWidthButtonCtrl("") {
        profile = "BracketButton19Profile";
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
    %cancelButton = new GuiVariableWidthButtonCtrl("") {
        profile = "BracketButton19NonDefaultProfile";
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
    %doneButton.add(%theTab);
    %theTab.doneButton = %doneButton;
    %cancelButton.add(%theTab);
    %theTab.cancelButton = %cancelButton;
    %this.tabSnapshotInitialized = 1;
};
function ProfilePosePopup::onSelect(%this, %unused, %entries) {
    if (!(%entries $= "")) {
        %anim = convertWordToAnim(%entries);
        %anim.playAnim($player);
        1.setVisible(ProfilePoseReplay);
    }
};
function ProfilePoseReplay::onURL(%this, %url) {
    %anim = ProfilePosePopup.getText();
    if (!(%anim $= "pose")) {
        %anim = convertWordToAnim(%anim);
        %anim.playAnim($player);
    }
};
function ProfileCurrentPicture::update(%this, %url) {
    if ($StandAlone) {
        return;
    }
    if (!(%url $= "")) {
        $Player::hasSeenTakeAvatarPhotoDialog = 1;
    }
    %curl = new URLPostObject("");
    "ProfileAvatarPictureRequest".setName(%curl);
    if ((%url $= "")) {
        %url = $Net::AvatarURL @ urlEncode($player.getShapeName());
    }
    %url.setURL(%curl);
    1.getLocalFileName(%this).setDownloadFile(%curl);
    1.setRecvData(%curl);
    "ProfileAvatarPictureRequestOnCompleted".setCompletedCallback(%curl);
    if (!(%curl.start())) {
        %curl.delete();
        warn("ProfileCurrentPicture::update(): couldn't start dynamic download of avatar pic.");
        return;
    }
    if (isObject(CURLSimGroup)) {
        %curl.add(CURLSimGroup);
    }
    echo("ProfileCurrentPicture::update(): started dynamic download of avatar pic");
};
function ProfileAvatarPictureRequestOnCompleted(%request, %result) {
    if ((%result == 0.0)) {
        1.setProperty(gUserPropMgrClient, $Player::Name, "hasTakenAvatarPhoto");
        %fileName = %request.getDownloadFile();
        removeFile(%fileName);
        addFile(%fileName);
        "".setBitmap(ProfileCurrentPicture);
        %fileName.setBitmap(ProfileCurrentPicture);
        %request.getURL().purgeCacheEntry(dlMgr);
        %request.getURL() @ "?size=S".purgeCacheEntry(dlMgr);
        %request.getURL() @ "?size=M".purgeCacheEntry(dlMgr);
        %request.getURL() @ "?size=L".purgeCacheEntry(dlMgr);
    }
    %retryCount = 1;
    if (!(0.getProperty(gUserPropMgrClient, $Player::Name, "hasTakenAvatarPhoto"))) {
    }
    if (($Player::attemptsToAutoUploadAvatarSnapshot < %retryCount)) {
    }
    if ((ClosetGui.lastTabOpened $= "SNAPSHOT")) {
    }
    if (ClosetGui.isVisible()) {
        prepareSnapshot.schedule(ProfileSnapRegion, 200);
        $Player::attemptsToAutoUploadAvatarSnapshot = ($Player::attemptsToAutoUploadAvatarSnapshot + 1.0);
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
        0.setVisible(ClosetGuiFUE);
        waitAFrameAndCall("ProfileSnapRegion_doSnapshot");
        return;
    }
    ProfileSnapRegion_doSnapshot();
};
function ProfileSnapRegion_doSnapshot() {
    ProfileSnapRegion.doSnapshot();
};
function ProfileSnapRegion::doSnapshot(%this) {
    0.setActive(ProfileSnapshotButton);
    1.setVisible(ProfileSnapshotButton_Container.waitIcon);
    ProfileSnapshotButton_Container.waitIcon.start();
    %reg = %this.getScreenPosition() @ " " @ %this.getExtent();
    %snapshot = snapshot::snapAndUpRegion(%reg, 0.getLocalFileName(ProfileCurrentPicture), "n");
    "ProfileSnapRegionOnCompleted".setCompletedCallback(%snapshot);
    %snapshot.saveObject = %this;
    if (%this.returnClosetGuiFUE) {
        1.setVisible(ClosetGuiFUE);
    }
};
function ProfileSnapRegion::onProgress(%this, %unused) {
};
function ProfileSnapRegionOnCompleted(%request, %result) {
    %snapRegion = %request.saveObject;
    if ((%result == 0.0)) {
        echo("Profile pic upload done -- downloading");
        echo("photoURL =" @ " " @ "photoURL".getResult(%request));
        ProfileSnapshotButton_Container.waitIcon.stop();
        0.setVisible(ProfileSnapshotButton_Container.waitIcon);
        1.setActive(ProfileSnapshotButton);
        "photoURL".getResult(%request).update(ProfileCurrentPicture);
    }
    warn("Profile pic upload error");
    ProfileSnapshotButton_Container.waitIcon.stop();
    0.setVisible(ProfileSnapshotButton_Container.waitIcon);
    1.setActive(ProfileSnapshotButton);
};
function ProfileBackgroundChooser::onCreatedChild(%this, %child) {
    %thumb = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "3 3";
        extent = "39 39";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };
    %frame = new GuiBitmapButtonCtrl("") {
        profile = "GuiDefaultProfile";
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
    %thumb.add(%child);
    %frame.add(%child);
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
        while ((%i < %count)) {
            %fileName = fileName(getField(%images, %i));
            if ((getSubStr(%fileName, 0, 3) $= "sm_")) {
                %extension = strrchr(%fileName, ".");
                if ((%extension $= ".jpg")) {
                    %numImages = (%numImages + 1.0);
                }
                if ((%extension $= ".png")) {
                    %numImages = (%numImages + 1.0);
                }
            }
            %i = (%i + 1.0);
        }
        %numImages.setNumChildren(%this);
        %i = 0;
        (%i < %count);
        while ((%i < %numImages)) {
            %cell = %i.getObject(%this);
            %cell.index = %i;
            %bgdPath @ "sm_" @ (%i + 1.0).setBitmap(%cell.thumb);
            %cell.thumbBitmapName = %bgdPath @ "sm_" @ (%i + 1.0);
            %cell.bitmapName = %bgdPath @ (%i + 1.0) @ ".jpg";
            %i = (%i + 1.0);
        }
        %this.selected = (%i < %numImages) @ -(1.0);
        0.selectThumbAtIndex(%this);
        %this.initialized = 1;
    }
};
function ProfileBackgroundChooser::selectThumbAtIndex(%this, %index) {
    if ((%this.selected == %index)) {
        return;
    }
    if ((%this.selected >= 0.0)) {
        %cell = %this.selected.getObject(%this);
        if (isObject(%cell)) {
            "platform/client/buttons/sm_frame".setBitmap(%cell.frame);
        }
    }
    %this.selected = %index;
    %cell = %this.selected.getObject(%this);
    "platform/client/buttons/sm_frame_selected".setBitmap(%cell.frame);
    if (isFile(%cell.bitmapName)) {
        %cell.bitmapName.setBitmap(ProfileBackgroundImage);
    }
    %url = $Net::downloadURL @ "/packages/" @ %cell.bitmapName;
    %cell.thumbBitmapName.setBitmap(ProfileBackgroundImage);
    %url.downloadAndApplyBitmap(ProfileBackgroundImage);
};
function ProfileBackgroundImage::onSystemDragDroppedEvent(%this, %text, %unused) {
    %text = strreplace(%text, "\\", "/");
    if (platformIsFile(%text)) {
        addFile(%text);
        "".setBitmap(%this);
        %text.setBitmap(%this);
    }
    %text.downloadAndApplyBitmap(%this);
};
function ProfileBackgroundChooser::thumbClicked(%this, %cell) {
    %cell.index.selectThumbAtIndex(%this);
};
function ProfileBackgroundChooser::moveBy(%this, %numSlots) {
    %pos = %this.getTrgPosition();
    %xPos = getWord(%pos, 0);
    %ypos = getWord(%pos, 1);
    %slotWidth = (getWord(%this.childrenExtent, 0) + %this.spacing);
    %min = (-((%this.getCount() - 6.0)) * %slotWidth);
    %max = 0;
    if (ProfilePreviousBackgroundButton.isActive()) {
    }
    if (((%xPos + (%slotWidth * %numSlots)) >= %max)) {
        0.setActive(ProfilePreviousBackgroundButton);
    }
    if (!(ProfilePreviousBackgroundButton.isActive())) {
        1.setActive(ProfilePreviousBackgroundButton);
    }
    if (ProfileNextBackgroundButton.isActive()) {
    }
    if (((%xPos + (%slotWidth * %numSlots)) <= %min)) {
        0.setActive(ProfileNextBackgroundButton);
    }
    if (!(ProfileNextBackgroundButton.isActive())) {
        1.setActive(ProfileNextBackgroundButton);
    }
    %xPos = mMin(%max, mMax(%min, (%xPos + (%slotWidth * %numSlots))));
    %ypos.setTrgPosition(%this, %xPos);
    %this.minExtent = (%slotWidth * %this.getCount()) @ " " @ %slotWidth;
};
function ProfileObjectView::moveBy(%this, %dx, %dy) {
    %nudge = ($player.getGender() $= "f") ? "0.4 -0.3 0.8" : "0 -0.1 0.8";
    %nudge.setLookAtNudge(%this);
    %dx = (%dx * 0.25);
    %dy = (%dy * 0.25);
    %x = mMin(3, mMax(-(3.0), (%this.xPos + %dx)));
    %y = mMin(4, mMax(-(13.0), (%this.yPos + %dy)));
    %x @ " " @ %y.setMove(%this);
};
function ProfileObjectView::setMove(%this, %pos) {
    %this.xPos = getWord(%pos, 0);
    %this.yPos = getWord(%pos, 1);
    (-(700.0) + (50.0 * %this.yPos)).setTrgPosition(%this, (-(195.0) + (50.0 * %this.xPos)));
};
$gProfileObjectView_Views["default","f"] = "-0 0 2.4";
$gProfileObjectView_Views["default","m"] = "-0 0 2.4";
$gProfileObjectView_Views["face","f"] = "-0 1 0.7";
$gProfileObjectView_Views["face","m"] = "-0 2.5 0.7";
$gProfileObjectView_Views["body","f"] = "-0 -2 4.7";
$gProfileObjectView_Views["body","m"] = "-0 -2 4.6";
$gProfileObjectView_Views["offscreen","f"] = "-0 -13 5.6";
$gProfileObjectView_Views["offscreen","m"] = "-0 -13 5.6";
function ProfileObjectView::setView(%this, %viewName) {
    %view = [$player.getGender()];
    $gProfileObjectView_Views @ %viewName;
    if ((%view $= "")) {
        error(getScopeName() @ " " @ "- unknown view" @ " " @ %viewName @ " " @ getTrace());
        %view = [$player.getGender()];
        $gProfileObjectView_Views @ "default";
    }
    %position = getWords(%view, 0, 1);
    %zoom = getWords(%view, 2, 2);
    %position.setMove(%this);
    %zoom.setOrbitDist(%this);
};
function ProfilePresetViews::onURL(%this, %url) {
    %cmd = firstWord(%url);
    if (!(%cmd $= "SETVIEW")) {
        error(getScopeName() @ " " @ "- unknown command" @ " " @ %cmd @ " " @ getTrace());
        return;
    }
    %viewName = restWords(%url);
    %viewName.setView(ProfileObjectView);
};
function ProfileBackgroundURLField::OnEnterKey(%this) {
    %url = %this.getValue();
    if (!(ImageFrameBase_IsPermittedURL(%url))) {
        MessageBoxOK($MsgCat::furniture["IMAGEFRAME-TITLENOTWLIST"], $MsgCat::furniture["IMAGEFRAME-NOTWHITELIST"], "");
        return;
    }
    %url.downloadAndApplyBitmap(ProfileBackgroundImage);
};
function ProfileBackgroundURLField::onSetFirstResponder(%this) {
    Parent::onSetFirstResponder(%this);
    if (%this.isDefault) {
        %this.isDefault = 0;
        "".setText(%this);
    }
    1000.setSelection(%this, 0);
};
function ProfileObjectView::resetLight(%this) {
    "0 3 -2".setLightDirection(%this);
    "1 1 1".setLightColor(%this);
};
