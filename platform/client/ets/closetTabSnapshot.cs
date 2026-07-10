function ClosetTabs::fillProfileTab(%this) {
    %theTab = %this.getTabWithName("SNAPSHOT");
    if (!isObject(%theTab)) {
        return;
    }
    %theTab.add(new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 26";
        extent = "571 37";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closet_tabs_bracket";
    };);
    %theTab.add(new GuiMLTextCtrl("") {
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
    %theTab.add(new GuiWindowCtrl("") {
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
    };);
    %theTab.add(new GuiVariableWidthButtonCtrl("") {
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
    };);
    %theTab.add(new GuiMLTextCtrl("") {
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
    };, new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
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
    %theTab.add(%background);
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
    %objView.setOrbitDist(2.4);
    %objView.setLightDirection("0 3 -2");
    %objView.moveBy("0 0");
    %theTab.objView = %objView;
    %snapFrame.add(%objView);
    %maskFrame.add(%snapFrame);
    %theTab.add(%maskFrame);
    %theTab.add(new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "295 85";
        extent = "368 368";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/largeSnapFrame";
    };);
    %theTab.add(new GuiTextCtrl("") {
        profile = "ClosetLeftInfoProfile";
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
    ProfilePreviousBackgroundButton.setActive(0);
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
    %theTab.add(new GuiTextEditCtrl(ProfileBackgroundURLField) {
        profile = "InfoWindowTextEditInvisibleOnWhiteProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "289 20";
        altCommand = "$ThisControl.OnEnterKey();";
        text = ".. or enter an HTTP image URL here ..";
        isDefault = 1;
    };, new GuiWindowCtrl("") {
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
    };);
    %theTab.add(new GuiWindowCtrl("") {
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
    };);
    ProfilePosePopup.add("photo-pose-01");
    ProfilePosePopup.add("photo-pose-02");
    ProfilePosePopup.add("photo-pose-03");
    ProfilePosePopup.add("photo-pose-04");
    ProfilePosePopup.add("photo-pose-05");
    ProfilePosePopup.add("photo-pose-06");
    ProfilePosePopup.add("photo-pose-07");
    ProfilePosePopup.add("photo-pose-08");
    ProfilePosePopup.add("angry");
    ProfilePosePopup.add("boo");
    ProfilePosePopup.add("confused");
    ProfilePosePopup.add("cry");
    ProfilePosePopup.add("embarrassed");
    ProfilePosePopup.add("flirt");
    ProfilePosePopup.add("hmm");
    ProfilePosePopup.add("in-love");
    ProfilePosePopup.add("lol");
    ProfilePosePopup.add("rotfl");
    ProfilePosePopup.add("sad");
    ProfilePosePopup.add("scared");
    ProfilePosePopup.add("sleepy");
    ProfilePosePopup.add("smile");
    ProfilePosePopup.add("surprised");
    ProfilePosePopup.add("thinking");
    ProfilePosePopup.add("applause");
    ProfilePosePopup.add("busy");
    ProfilePosePopup.add("cool");
    ProfilePosePopup.add("doh");
    ProfilePosePopup.add("kiss");
    ProfilePosePopup.add("not-listening");
    ProfilePosePopup.add("shhh");
    ProfilePosePopup.add("sit");
    ProfilePosePopup.add("talk-to-the-hand");
    ProfilePosePopup.add("vomit");
    ProfilePosePopup.add("waiting");
    ProfilePosePopup.add("wave");
    ProfilePosePopup.add("whew");
    ProfilePosePopup.add("vside");
    ProfilePosePopup.setText("Pose");
    %theTab.add(new GuiControl(ProfileSnapshotButton_Container) {
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
        tickPerio /* expression truncated */;
    ProfileSnapshotButton_Container.add(new GuiBitmapButtonCtrl(ProfileSnapshotButton) {
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
    ProfileSnapshotButton_Container.add(%wi);
    ProfileSnapshotButton_Container.waitIcon = %wi;
    %wi.setVisible(0);
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
        ProfilePoseReplay.setVisible(1);
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
    %curl = new URLPostObject("");
    %curl.setName("ProfileAvatarPictureRequest");
    if ((%url $= "")) {
        %url = $Net::AvatarURL @ urlEncode($player.getShapeName());
    }
    %curl.setURL(%url);
    %curl.setDownloadFile(%this.getLocalFileName(1));
    %curl.setRecvData(1);
    %curl.setCompletedCallback("ProfileAvatarPictureRequestOnCompleted");
    if (!%curl.start()) {
        %curl.delete();
        warn("ProfileCurrentPicture::update(): couldn't start dynamic download of avatar pic.");
        return;
    }
    if (isObject(CURLSimGroup)) {
        CURLSimGroup.add(%curl);
    }
    echo("ProfileCurrentPicture::update(): started dynamic download of avatar pic");
};
function ProfileAvatarPictureRequestOnCompleted(%request, %result) {
    if ((%result == 0.0)) {
        gUserPropMgrClient.setProperty($Player::Name, "hasTakenAvatarPhoto", 1);
        %fileName = %request.getDownloadFile();
        removeFile(%fileName);
        addFile(%fileName);
        ProfileCurrentPicture.setBitmap("");
        ProfileCurrentPicture.setBitmap(%fileName);
        dlMgr.purgeCacheEntry(%request.getURL());
        dlMgr.purgeCacheEntry(%request.getURL() @ "?size=S");
        dlMgr.purgeCacheEntry(%request.getURL() @ "?size=M");
        dlMgr.purgeCacheEntry(%request.getURL() @ "?size=L");
    }
    %retryCount = 1;
    if (!gUserPropMgrClient.getProperty($Player::Name, "hasTakenAvatarPhoto", 0)) {
    }
    if (($Player::attemptsToAutoUploadAvatarSnapshot < %retryCount)) {
    }
    if ((ClosetGui.lastTabOpened $= "SNAPSHOT")) {
    }
    if (ClosetGui.isVisible()) {
        ProfileSnapRegion.schedule(200, prepareSnapshot);
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
        ClosetGuiFUE.setVisible(0);
        waitAFrameAndCall("ProfileSnapRegion_doSnapshot");
        return;
    }
    ProfileSnapRegion_doSnapshot();
};
function ProfileSnapRegion_doSnapshot() {
    ProfileSnapRegion.doSnapshot();
};
function ProfileSnapRegion::doSnapshot(%this) {
    ProfileSnapshotButton.setActive(0);
    ProfileSnapshotButton_Container.waitIcon.setVisible(1);
    ProfileSnapshotButton_Container.waitIcon.start();
    %reg = %this.getScreenPosition() @ " " @ %this.getExtent();
    %snapshot = snapshot::snapAndUpRegion(%reg, ProfileCurrentPicture.getLocalFileName(0), "n");
    %snapshot.setCompletedCallback("ProfileSnapRegionOnCompleted");
    %snapshot.saveObject = %this;
    if (%this.returnClosetGuiFUE) {
        ClosetGuiFUE.setVisible(1);
    }
};
function ProfileSnapRegion::onProgress(%this, %unused) {
};
function ProfileSnapRegionOnCompleted(%request, %result) {
    %snapRegion = %request.saveObject;
    if ((%result == 0.0)) {
        echo("Profile pic upload done -- downloading");
        echo("photoURL =" @ " " @ %request.getResult("photoURL"));
        ProfileSnapshotButton_Container.waitIcon.stop();
        ProfileSnapshotButton_Container.waitIcon.setVisible(0);
        ProfileSnapshotButton.setActive(1);
        ProfileCurrentPicture.update(%request.getResult("photoURL"));
    }
    warn("Profile pic upload error");
    ProfileSnapshotButton_Container.waitIcon.stop();
    ProfileSnapshotButton_Container.waitIcon.setVisible(0);
    ProfileSnapshotButton.setActive(1);
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
    %child.add(%thumb);
    %child.add(%frame);
    %child.thumb = %thumb;
    %child.frame = %frame;
};
function ProfileBackgroundChooser::Initialize(%this) {
    if (!%this.initialized) {
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
        %this.setNumChildren(%numImages);
        %i = 0;
        (%i < %count);
        while ((%i < %numImages)) {
            %cell = %this.getObject(%i);
            %cell.index = %i;
            %cell.thumb.setBitmap(%bgdPath @ "sm_" @ (%i + 1.0));
            %cell.thumbBitmapName = %bgdPath @ "sm_" @ (%i + 1.0);
            %cell.bitmapName = %bgdPath @ (%i + 1.0) @ ".jpg";
            %i = (%i + 1.0);
        }
        %this.selected = (%i < %numImages) @ -(1.0);
        %this.selectThumbAtIndex(0);
        %this.initialized = 1;
    }
};
function ProfileBackgroundChooser::selectThumbAtIndex(%this, %index) {
    if ((%this.selected == %index)) {
        return;
    }
    if ((%this.selected >= 0.0)) {
        %cell = %this.getObject(%this.selected);
        if (isObject(%cell)) {
            %cell.frame.setBitmap("platform/client/buttons/sm_frame");
        }
    }
    %this.selected = %index;
    %cell = %this.getObject(%this.selected);
    %cell.frame.setBitmap("platform/client/buttons/sm_frame_selected");
    if (isFile(%cell.bitmapName)) {
        ProfileBackgroundImage.setBitmap(%cell.bitmapName);
    }
    %url = $Net::downloadURL @ "/packages/" @ %cell.bitmapName;
    ProfileBackgroundImage.setBitmap(%cell.thumbBitmapName);
    ProfileBackgroundImage.downloadAndApplyBitmap(%url);
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
    %slotWidth = (getWord(%this.childrenExtent, 0) + %this.spacing);
    %min = (-((%this.getCount() - 6.0)) * %slotWidth);
    %max = 0;
    if (ProfilePreviousBackgroundButton.isActive()) {
    }
    if (((%xPos + (%slotWidth * %numSlots)) >= %max)) {
        ProfilePreviousBackgroundButton.setActive(0);
    }
    if (!ProfilePreviousBackgroundButton.isActive()) {
        ProfilePreviousBackgroundButton.setActive(1);
    }
    if (ProfileNextBackgroundButton.isActive()) {
    }
    if (((%xPos + (%slotWidth * %numSlots)) <= %min)) {
        ProfileNextBackgroundButton.setActive(0);
    }
    if (!ProfileNextBackgroundButton.isActive()) {
        ProfileNextBackgroundButton.setActive(1);
    }
    %xPos = mMin(%max, mMax(%min, (%xPos + (%slotWidth * %numSlots))));
    %this.setTrgPosition(%xPos, %ypos);
    %this.minExtent = (%slotWidth * %this.getCount()) @ " " @ %slotWidth;
};
function ProfileObjectView::moveBy(%this, %dx, %dy) {
    %nudge = ($player.getGender() $= "f") ? "0.4 -0.3 0.8" : "0 -0.1 0.8";
    %this.setLookAtNudge(%nudge);
    %dx = (%dx * 0.25);
    %dy = (%dy * 0.25);
    %x = mMin(3, mMax(-(3.0), (%this.xPos + %dx)));
    %y = mMin(4, mMax(-(13.0), (%this.yPos + %dy)));
    %this.setMove(%x @ " " @ %y);
};
function ProfileObjectView::setMove(%this, %pos) {
    %this.xPos = getWord(%pos, 0);
    %this.yPos = getWord(%pos, 1);
    %this.setTrgPosition((-(195.0) + (50.0 * %this.xPos)), (-(700.0) + (50.0 * %this.yPos)));
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
    ProfileObjectView.setView(%viewName);
};
function ProfileBackgroundURLField::OnEnterKey(%this) {
    %url = %this.getValue();
    if (!ImageFrameBase_IsPermittedURL(%url)) {
        MessageBoxOK($MsgCat::furniture["IMAGEFRAME-TITLENOTWLIST"], $MsgCat::furniture["IMAGEFRAME-NOTWHITELIST"], "");
        return;
    }
    ProfileBackgroundImage.downloadAndApplyBitmap(%url);
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
