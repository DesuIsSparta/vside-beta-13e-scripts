$gBroadSnapshotUploadTimeOutSched = 0;
function BroadCastControlPanel::toggle(%this) {
    PlayGui.showRaiseOrHide(%this);
};
function BroadCastControlPanel::open(%this) {
    BroadCastCrossHairsFrame.resize((1.0 - getWord(BroadCastRegionControl.getExtent(), 0)), (1.0 - getWord(BroadCastRegionControl.getExtent(), 1)));
    BroadCastCrossHairsFrame.resize(getWord(BroadCastRegionControl.getExtent(), 0), getWord(BroadCastRegionControl.getExtent(), 1));
    %this.setVisible(1);
    %this.setConstrained(1);
    %this.currentlyUploading = 0;
    %this.hasError = 0;
    %this.enterFirstTimeMode();
    PlayGui.focusAndRaise(%this);
};
function BroadCastControlPanel::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    BroadCastPreview.setVisible(0);
    BroadCastPreview.setBitmap("");
    %this.photoFileName = "" @ BroadCastControlPanel;
    %this.photoFileNameExt = "" @ BroadCastControlPanel;
    %this.photoTransform = "" @ BroadCastControlPanel;
    %this.photoInhabitants = "" @ BroadCastControlPanel;
    return 1;
};
function BroadSnapshotButton_prepareForDoTakeSnapshot() {
    BroadcastCloseButtonContainer.setVisible(0);
    BroadCastCrossHairsFrame.setVisible(0);
    BroadcastHideHUDsCheckbox.setActive(0);
    BroadcastHideChatCheckbox.setActive(0);
    BroadcastHideSelfCheckbox.setActive(0);
    BroadcastFullScreenCheckbox.setActive(0);
    BroadSnapshotButton_HideSnoop();
    if (BroadcastHideHUDsCheckbox.getValue()) {
        if ((BroadCastControlPanel == %this.temporaryGUIControlContainer)) {
            %this.temporaryGUIControlContainer = new GuiControl(""); @ BroadCastControlPanel;
            0;
        }
        BroadCastControlPanel.setVisible(%this.temporaryGUIControlContainer, 0);
        %orderedChildren = PlayGui.getChildrenInOrder(BroadCastControlPanel, %this.playGuiControlsToHide);
        0.0;
        %i = (1.0 - getWordCount(%orderedChildren));
        if ((0.0 >= %i)) {
            %ctrl = getWord(%orderedChildren, %i);
            BroadCastControlPanel.add(%this.temporaryGUIControlContainer, %ctrl);
            %i = (1.0 - %i);
        }
    }
    if (BroadcastHideChatCheckbox.getValue()) {
        if ((BroadCastControlPanel == %this.temporaryGUIControlContainer)) {
            %this.temporaryGUIControlContainer = new GuiControl(""); @ BroadCastControlPanel;
            0;
        }
        BroadCastControlPanel.setVisible(%this.temporaryGUIControlContainer, 0);
        BroadCastControlPanel.add(%this.temporaryGUIControlContainer);
    }
    if (BroadcastHideSelfCheckbox.getValue()) {
    }
    if (BroadcastHideSelfCheckbox.isVisible()) {
        if (!($IN_ORBIT_CAM)) {
        }
        if (!($firstPerson)) {
            $player.MeshOff("*");
            $player.setShapeName("");
            if (isObject($player.hudCtrl)) {
                $player.hudCtrl.setVisible(0);
            }
            ThePointsFloaterHud.setVisible(0);
        }
    }
    if (BroadcastFullScreenCheckbox.getValue()) {
        BroadCastControlPanel.setVisible(0);
    }
    if ((ConvBub @ " " @ $Platform $= "windows")) {
    }
    if ((6.0 == $Platform::Version::Major)) {
        waitAFrameAndCall("waitAFrameAndCall(\"BroadSnapshotButton_doTakeSnapshot\");");
    }
    waitAFrameAndCall("BroadSnapshotButton_doTakeSnapshot");
};
function BroadSnapshotButton_doTakeSnapshot() {
    BroadSnapshotButton.setActive(0);
    %photoFileName = $DC::LocalAvatarFolder @ "/lastSnapshotTaken";
    if (($Pref::Video::screenShotFormat $= "JPEG")) {
        %ext = ".jpg";
    }
    if (($Pref::Video::screenShotFormat $= "PNG")) {
        %ext = ".png";
    }
    %ext = ".png";
    %regionControl = 0;
    if (BroadcastFullScreenCheckbox.getValue()) {
        %regionControl = PlayGui.getId();
    }
    %regionControl = BroadCastRegionControl.getId();
    if ((0.0 != %regionControl)) {
    }
    %tookPhoto = snapshotTool::snapControl(%regionControl, %photoFileName @ %ext);
    if (%tookPhoto) {
        %topMargin = 60;
        %bottomMargin = -(10.0);
        %leftMargin = 0;
        %rightMargin = 0;
        %playerIDs = TheShapeNameHud.getPlayerIDsInViewAndInRangeAndInFrame((%leftMargin - getWord(%regionControl.getScreenPosition(), 0)), (%topMargin - getWord(%regionControl.getScreenPosition(), 1)), (%rightMargin + (%leftMargin + getWord(%regionControl.getExtent(), 0))), (%bottomMargin + (%topMargin + getWord(%regionControl.getExtent(), 1))));
        %numPlayers = getWordCount(%playerIDs);
        %playerNames = "";
        %n = 0;
        if ((%numPlayers < %n)) {
            %playerNames = %playerNames @ "\t" @ getWord(%playerIDs, %n).getShapeName();
            %n = (1.0 + %n);
        }
        %playerNames = trim(%playerNames);
        (%numPlayers < %n);
        BroadCastControlPanel.enterFillCURLMode(%photoFileName, %ext, $player.getTransform(), %playerNames);
        BroadCastControlPanel.enterTookPhotoMode();
        removeFile(%photoFileName @ %ext);
        addFile(%photoFileName @ %ext);
        BroadCastPreview.setBitmap("");
        BroadCastPreview.setBitmap(%photoFileName);
        BroadCastPreview.setVisible(1);
        alxPlay(AudioProfile_Shutter);
        commandToServer('FireEventPlayerTakesAPicture');
    }
    BroadCastCrossHairsFrame.setVisible(1);
    BroadCastPreview.setVisible(0);
    BroadCastPreview.setBitmap("");
    MessageBoxOK("Can't take snapshot!", "Unable to create snapshot. Please let a Mod know, or post a note in the forums. Thank you!", "");
    BroadSnapshotButton.setActive(1);
    BroadcastCloseButtonContainer.setVisible(1);
    BroadSnapshotButton.setActive(1);
    BroadSnapshotButton_ShowSnoop();
    if (BroadcastHideHUDsCheckbox.getValue()) {
        %i = (1.0 - BroadCastControlPanel.getCount($player.temporaryGUIControlContainer));
        if ((0.0 >= %i)) {
            %ctrl = BroadCastControlPanel.getObject($player.temporaryGUIControlContainer, %i);
            PlayGui.add(%ctrl);
            %i = (1.0 - %i);
        }
    }
    if (BroadcastHideChatCheckbox.getValue()) {
        PlayGui.add(ConvBub);
    }
    if (BroadcastHideHUDsCheckbox.getValue()) {
    }
    if (BroadcastHideChatCheckbox.getValue()) {
        BroadCastControlPanel.delete($player.temporaryGUIControlContainer);
        $player.temporaryGUIControlContainer = 0 @ BroadCastControlPanel;
        (0.0 >= %i);
    }
    if (BroadcastHideSelfCheckbox.getValue()) {
    }
    if (BroadcastHideSelfCheckbox.isVisible()) {
        $player.setActiveSKUs($player.getActiveSKUs());
        $player.setShapeName($Player::Name);
        if (isObject($player.hudCtrl)) {
            $player.hudCtrl.setVisible(1);
        }
        ThePointsFloaterHud.setVisible(1);
    }
    if (BroadcastFullScreenCheckbox.getValue()) {
        BroadCastControlPanel.setVisible(1);
    }
    BroadcastHideHUDsCheckbox.setActive(1);
    BroadcastHideChatCheckbox.setActive(1);
    BroadcastHideSelfCheckbox.setActive(1);
    if (!($IN_ORBIT_CAM)) {
    }
    BroadcastHideSelfCheckbox.setVisible(!($firstPerson));
    BroadcastFullScreenCheckbox.setActive(1);
    PlayGui.focusAndRaise(BroadCastControlPanel);
    if (%tookPhoto) {
        BroadcastPhotoControls.focusAndRaise(BroadcastCaptionCtrl);
        BroadcastCaptionCtrl.selectAll();
    }
};
function BroadcastCaptionCtrl::doOnPressEnter(%this) {
    if (BroadSnapshotUploadButton.isVisible()) {
        // unhandled opcode 1824 at 0x0000071D
    }
    // unhandled opcode 1611 at 0x00000721
    %tookPhoto = BroadSnapshotUploadButtonApartment;
    BroadSnapshotUploadButton;
    %button.performClick();
};
function BroadSnapshotUploadButton::doBroadCastSnapshot(%this, %callbackSink) {
    BroadCastControlPanel.enterFillCURLMode(BroadCastControlPanel, $player.photoFileName, BroadCastControlPanel, $player.photoFileNameExt, BroadCastControlPanel, $player.photoTransform, BroadCastControlPanel, $player.photoInhabitants);
    %caption = BroadcastCaptionCtrl.getText();
    if ((%caption $= "enter caption here..")) {
        %caption = "";
    }
    BroadCastPreview.setURLParam($player.curl, "caption", %caption);
    BroadCastPreview.setURLParam($player.curl, "featured", "false");
    if ($player.visible) {
    }
    if (BroadcastCaptionSetBCastCtrl.getValue()) {
        BroadCastPreview.setURLParam($player.curl, "broadcast", "BroadcastScreens");
    }
    BroadCastPreview.setURLParam($player.curl, "broadcast", "");
    BroadCastPreview.setCompletedCallback($player.curl, "BroadSnapshotUploadButtonOnCompleted");
    if (!(BroadCastPreview.start($player.curl))) {
        BroadCastControlPanel.enterErrorUploadingMode();
    }
    if (isObject(CURLSimGroup)) {
        CURLSimGroup.add(BroadCastPreview, $player.curl);
    }
    BroadCastControlPanel.enterUploadingMode();
};
function BroadSnapshotUploadButton::onProgress(%this, %uploader) {
};
function BroadSnapshotUploadButtonOnCompleted(%request, %result) {
    %callbackSink = %request.callBackSink;
    if ((0.0 == %result)) {
        %callbackSink.onDone(%request);
    }
    %callbackSink.onError(%request);
};
function BroadSnapshotUploadButton::onError(%this, %uploader) {
    %request.currentlyUploading = 0 @ BroadCastControlPanel;
    %request.hasError = 1 @ BroadCastControlPanel;
    BroadCastControlPanel.enterErrorUploadingMode();
    BroadCastPreview.stop(%request.curl);
    error("Broadcast failed to upload");
};
function BroadSnapshotUploadButton::onDone(%this, %uploader) {
    %request.currentlyUploading = 0 @ BroadCastControlPanel;
    if ((0.0 != $gBroadSnapshotUploadTimeOutSched)) {
        cancel($gBroadSnapshotUploadTimeOutSched);
        $gBroadSnapshotUploadTimeOutSched = 0;
    }
    $gNumPhotosTaken = (1.0 + $gNumPhotosTaken);
    if (!(%uploader.getResult("status") $= "success")) {
        if (!(%request.hasError)) {
            %this.onError(%uploader);
        }
        return BroadCastControlPanel;
    }
    BroadCastControlPanel.enterTakePhotoMode();
    echo("Broadcast done. photoURL =" @ " " @ %uploader.getResult("photoURL"));
    BroadCastPreview.setVisible(0);
    BroadCastPreview.setBitmap("");
    %shareFB = BroadcastCaptionShareFcBookCtrl.getValue();
    if (%shareFB) {
        BroadCastControlPanel.shareFcBook(%uploader.getResult("photoURL"));
    }
    %gaURL = "/client/facebookShare/snapshot/" @ %shareFB ? "yes" : "no";
    getAnalytic().trackPageView(%gaURL);
};
function BroadSnapshotCancelButton::doCancel(%this) {
    if ((0.0 != $gBroadSnapshotUploadTimeOutSched)) {
        cancel($gBroadSnapshotUploadTimeOutSched);
        $gBroadSnapshotUploadTimeOutSched = 0;
    }
    BroadCastControlPanel.enterFirstTimeMode();
    echo("Broadcast cancelled");
    BroadCastPreview.setVisible(0);
    BroadCastPreview.setBitmap("");
    if (isObject(BroadCastPreview, %request.curl)) {
        BroadCastPreview.delete(%request.curl);
    }
    BroadSnapshotButton.setActive(1);
};
function BroadSnapshotButton_HideSnoop() {
    %n = (1.0 - TheBadgesHud.getCount());
    if ((0.0 >= %n)) {
        %projCtrl = TheBadgesHud.getObject(%n);
        %roleCtrl = %projCtrl.roleCtrl;
        if (isObject(%roleCtrl)) {
            if ((0.0 >= strpos(%roleCtrl.bitmap, "neighborhoodwatch"))) {
                %roleCtrl.setVisible(0);
            }
        }
        %n = (1.0 - %n);
    }
};
function BroadSnapshotButton_ShowSnoop() {
    %n = (1.0 - TheBadgesHud.getCount());
    if ((0.0 >= %n)) {
        %projCtrl = TheBadgesHud.getObject(%n);
        %roleCtrl = %projCtrl.roleCtrl;
        if (isObject(%roleCtrl)) {
            %roleCtrl.setVisible(1);
        }
        %n = (1.0 - %n);
    }
};
$gNumPhotosTaken = 0;
function BroadcastCaptionSetBCastCtrl::onMouseUp(%this) {
    if (!(%this.getValue())) {
        MessageBoxYesNo("Broadcast to billboards?", "Checking this box will cause your snapshot to be broadcast to in-world billboards. You can do this because you are a special user. Are you sure you want this snapshot on a billboard?", "BroadcastCaptionSetBCastCtrl.setValue(true);", "BroadcastCaptionSetBCastCtrl.setValue(false);");
    }
};
function BroadCastControlPanel::enterFirstTimeMode(%this) {
    BroadCastFrameForPreview.setVisible(0);
    BroadcastCloseButtonContainer.setVisible(1);
    BroadCastCrossHairsFrame.setVisible(1);
    %projCtrl.photoFileName = "" @ BroadCastControlPanel;
    %projCtrl.photoFileNameExt = "" @ BroadCastControlPanel;
    %projCtrl.photoTransform = "" @ BroadCastControlPanel;
    %projCtrl.photoInhabitants = "" @ BroadCastControlPanel;
    BroadcastTakePhotoLabel.setVisible(1);
    mlStyle(BroadcastViewAlbumLink, "plainOnBlack").setValue();
    BroadcastUploadSuccessfulLabel.setVisible(0);
    BroadcastPhotoControls.setVisible(0);
    BroadcastSnapshotControls.setVisible(1);
};
function BroadCastControlPanel::enterFillCURLMode(%this, %photoFileName, %ext, %transform, %playerNames) {
    if ((%photoFileName $= "")) {
        error("BroadCastControlPanel::enterFillCURLMode called with empty filename");
        %this.enterFirstTimeMode();
        return;
    }
    if ((%ext $= "")) {
        error("BroadCastControlPanel::enterFillCURLMode called with empty file extention");
        %this.enterFirstTimeMode();
        return;
    }
    if (isObject(BroadCastPreview, %projCtrl.curl)) {
        BroadCastPreview.delete(%projCtrl.curl);
    }
    %projCtrl.curl = new URLPostObject(""); @ BroadCastPreview;
    0;
    %projCtrl.curl.callBackSink = BroadSnapshotUploadButton @ BroadCastPreview;
    BroadCastPreview.setProgress(%projCtrl.curl.curl, 1);
    BroadCastPreview.setRecvData(%projCtrl.curl.curl, 1);
    BroadCastPreview.setURLParam(%projCtrl.curl.curl, "user", $Player::Name);
    BroadCastPreview.setURLParam(%projCtrl.curl.curl, "token", $Token);
    BroadCastPreview.setURLParam(%projCtrl.curl.curl, "type", "screenshot");
    BroadCastPreview.setURLParam(%projCtrl.curl.curl, "location", %transform);
    BroadCastPreview.setURLParam(%projCtrl.curl.curl, "inView", %playerNames);
    if (!(CustomSpaceClient::GetSpaceImIn() $= "")) {
        BroadCastPreview.setURLParam(%projCtrl.curl.curl, "apartmentOwner", $CSSpaceInfo.owner);
        BroadCastPreview.setURLParam($CSSpaceInfo.curl, "vurl", $CSSpaceInfo.vurl);
    }
    BroadCastPreview.setURLParam($CSSpaceInfo.curl, "vurl", "vside:/location/" @ $gContiguousSpaceName @ "/PlazaSpawns");
    BroadCastPreview.setURL($CSSpaceInfo.curl, $Net::UploadPhotoURL);
    BroadCastPreview.setPostFile($CSSpaceInfo.curl, "imageBody", %photoFileName @ %ext);
    $CSSpaceInfo.photoFileName = %photoFileName @ BroadCastControlPanel;
    $CSSpaceInfo.photoFileNameExt = %ext @ BroadCastControlPanel;
    $CSSpaceInfo.photoTransform = %transform @ BroadCastControlPanel;
    $CSSpaceInfo.photoInhabitants = %playerNames @ BroadCastControlPanel;
};
function BroadCastControlPanel::enterTookPhotoMode(%this) {
    BroadCastFrameForPreview.setVisible(1);
    BroadcastCaptionCtrl.setVisible(1);
    BroadcastCaptionCtrl.setText("Enter caption here..");
    BroadcastCaptionShareFcBookCtrl.setVisible(1);
    BroadcastCaptionShareFcBookIcon.setVisible(1);
    BroadcastUploadingLabel.setVisible(0);
    BroadcastUploadFailedLabel.setVisible(0);
    if (!(CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if (CustomSpaceClient::isOwner()) {
        BroadSnapshotUploadButton.setVisible(0);
        BroadSnapshotUploadButtonApartment.setVisible(1);
        BroadcastTakePhotoLabel.setText("Take a snapshot for your apartment album!");
    }
    BroadSnapshotUploadButtonApartment.setVisible(0);
    BroadSnapshotUploadButton.setVisible(1);
    BroadcastTakePhotoLabel.setText("Take a snapshot for your web album!");
    BroadSnapshotUploadButton.setActive(1);
    BroadSnapshotUploadButtonApartment.setActive(1);
    BroadSnapshotCancelButton.setVisible(1);
    BroadSnapshotCancelButton.setActive(1);
    BroadcastSnapshotControls.setVisible(0);
    BroadcastPhotoControls.setVisible(1);
};
function BroadCastControlPanel::enterUploadingMode(%this) {
    %this.currentlyUploading = 1;
    %this.hasError = 0;
    if ((0.0 != $gBroadSnapshotUploadTimeOutSched)) {
        cancel($gBroadSnapshotUploadTimeOutSched);
        $gBroadSnapshotUploadTimeOutSched = 0;
    }
    $gBroadSnapshotUploadTimeOutSched = %this.schedule(10000);
    enterErrorUploadingMode;
    BroadcastCaptionCtrl.setVisible(0);
    BroadcastCaptionShareFcBookCtrl.setVisible(0);
    BroadcastCaptionShareFcBookIcon.setVisible(0);
    BroadcastUploadingLabel.setVisible(1);
    BroadcastUploadFailedLabel.setVisible(0);
    BroadSnapshotUploadButton.setActive(0);
    BroadSnapshotUploadButtonApartment.setActive(0);
    BroadSnapshotCancelButton.setVisible(1);
    BroadSnapshotCancelButton.setActive(0);
    BroadcastSnapshotControls.setVisible(0);
    BroadcastPhotoControls.setVisible(1);
};
function BroadCastControlPanel::enterErrorUploadingMode(%this) {
    BroadcastCaptionCtrl.setVisible(0);
    BroadcastCaptionShareFcBookCtrl.setVisible(0);
    BroadcastCaptionShareFcBookIcon.setVisible(0);
    BroadcastUploadingLabel.setVisible(0);
    BroadcastUploadFailedLabel.setVisible(1);
    mlStyle(BroadcastUploadFailedLabel, "plainOnBlack").setValue();
    BroadSnapshotUploadButton.setActive(1);
    BroadSnapshotUploadButtonApartment.setActive(1);
    BroadSnapshotCancelButton.setVisible(1);
    BroadSnapshotCancelButton.setActive(1);
    BroadcastSnapshotControls.setVisible(0);
    BroadcastPhotoControls.setVisible(1);
};
function BroadCastControlPanel::enterTakePhotoMode(%this) {
    BroadCastFrameForPreview.setVisible(0);
    BroadcastCloseButtonContainer.setVisible(1);
    BroadCastCrossHairsFrame.setVisible(1);
    %this.photoFileName = "" @ BroadCastControlPanel;
    %this.photoFileNameExt = "" @ BroadCastControlPanel;
    %this.photoTransform = "" @ BroadCastControlPanel;
    %this.photoInhabitants = "" @ BroadCastControlPanel;
    BroadcastTakePhotoLabel.setVisible(0);
    BroadcastUploadSuccessfulLabel.setVisible(1);
    BroadcastPhotoControls.setVisible(0);
    BroadcastSnapshotControls.setVisible(1);
};
function BroadCastControlPanel::automateSnapshotUpload(%this) {
    %this.open();
    BroadSnapshotButton_doTakeSnapshot();
    BroadSnapshotUploadButton.doBroadCastSnapshot(BroadSnapshotUploadButton);
    %this.close();
};
function BroadCastControlPanel::shareFcBook(%this, %photoURL) {
    %preambleText = "/photoservice/";
    %preamblePos = strpos(%photoURL, %preambleText);
    if ((0.0 < %preamblePos)) {
        error(getScopeName() @ " " @ "- invalid photo URL." @ " " @ %photoURL @ " " @ getTrace());
    }
    %justPhotoID = getSubStr(%photoURL, (strlen(%preambleText) + %preamblePos), 1000);
    %viewPhotoURL = $Net::PhotoPageURL @ %justPhotoID @ "?ref=fb";
    %viewPhotoURLEncoded = %viewPhotoURL;
    %viewPhotoURLEncoded = urlEncode(%viewPhotoURLEncoded);
    %viewPhotoURLEncoded = strreplace(%viewPhotoURLEncoded, "/", "%2F");
    %sharerURL = "http://www.facebook.com/sharer.php";
    %sharerURL = %sharerURL @ "?u=" @ %viewPhotoURLEncoded;
    gotoWebPage(%sharerURL);
};
