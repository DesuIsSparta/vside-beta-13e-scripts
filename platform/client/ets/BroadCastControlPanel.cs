$gBroadSnapshotUploadTimeOutSched = 0;
function BroadCastControlPanel::toggle(%this) {
    %this.showRaiseOrHide(PlayGui);
};
function BroadCastControlPanel::open(%this) {
    (getWord(BroadCastRegionControl.getExtent(), 1) - 1.0).resize(BroadCastCrossHairsFrame, (getWord(BroadCastRegionControl.getExtent(), 0) - 1.0));
    getWord(BroadCastRegionControl.getExtent(), 1).resize(BroadCastCrossHairsFrame, getWord(BroadCastRegionControl.getExtent(), 0));
    1.setVisible(%this);
    1.setConstrained(%this);
    %this.currentlyUploading = 0;
    %this.hasError = 0;
    %this.enterFirstTimeMode();
    %this.focusAndRaise(PlayGui);
};
function BroadCastControlPanel::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    0.setVisible(BroadCastPreview);
    "".setBitmap(BroadCastPreview);
    BroadCastControlPanel.photoFileName = "";
    BroadCastControlPanel.photoFileNameExt = "";
    BroadCastControlPanel.photoTransform = "";
    BroadCastControlPanel.photoInhabitants = "";
    return 1;
};
function BroadSnapshotButton_prepareForDoTakeSnapshot() {
    0.setVisible(BroadcastCloseButtonContainer);
    0.setVisible(BroadCastCrossHairsFrame);
    0.setActive(BroadcastHideHUDsCheckbox);
    0.setActive(BroadcastHideChatCheckbox);
    0.setActive(BroadcastHideSelfCheckbox);
    0.setActive(BroadcastFullScreenCheckbox);
    BroadSnapshotButton_HideSnoop();
    if (BroadcastHideHUDsCheckbox.getValue()) {
        if ((BroadCastControlPanel.temporaryGUIControlContainer == 0.0)) {
            BroadCastControlPanel.temporaryGUIControlContainer = new GuiControl("");
        }
        0.setVisible(BroadCastControlPanel.temporaryGUIControlContainer);
        %orderedChildren = BroadCastControlPanel.playGuiControlsToHide.getChildrenInOrder(PlayGui);
        %i = (getWordCount(%orderedChildren) - 1.0);
        while ((%i >= 0.0)) {
            %ctrl = getWord(%orderedChildren, %i);
            %ctrl.add(BroadCastControlPanel.temporaryGUIControlContainer);
            %i = (%i - 1.0);
        }
    }
    if (BroadcastHideChatCheckbox.getValue()) {
        if ((BroadCastControlPanel.temporaryGUIControlContainer == 0.0)) {
            BroadCastControlPanel.temporaryGUIControlContainer = (%i >= 0.0) @ new GuiControl("");
        }
        0.setVisible(BroadCastControlPanel.temporaryGUIControlContainer);
        ConvBub.add(BroadCastControlPanel.temporaryGUIControlContainer);
    }
    if (BroadcastHideSelfCheckbox.getValue()) {
    }
    if (BroadcastHideSelfCheckbox.isVisible()) {
        if (!($IN_ORBIT_CAM)) {
        }
        if (!($firstPerson)) {
            "*".MeshOff($player);
            "".setShapeName($player);
            if (isObject($player.hudCtrl)) {
                0.setVisible($player.hudCtrl);
            }
            0.setVisible(ThePointsFloaterHud);
        }
    }
    if (BroadcastFullScreenCheckbox.getValue()) {
        0.setVisible(BroadCastControlPanel);
    }
    if (($Platform $= "windows")) {
    }
    if (($Platform::Version::Major == 6.0)) {
        waitAFrameAndCall("waitAFrameAndCall(\"BroadSnapshotButton_doTakeSnapshot\");");
    }
    waitAFrameAndCall("BroadSnapshotButton_doTakeSnapshot");
};
function BroadSnapshotButton_doTakeSnapshot() {
    0.setActive(BroadSnapshotButton);
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
    if ((%regionControl != 0.0)) {
    }
    %tookPhoto = snapshotTool::snapControl(%regionControl, %photoFileName @ %ext);
    if (%tookPhoto) {
        %topMargin = 60;
        %bottomMargin = -(10.0);
        %leftMargin = 0;
        %rightMargin = 0;
        %playerIDs = ((getWord(%regionControl.getExtent(), 1) + %topMargin) + %bottomMargin).getPlayerIDsInViewAndInRangeAndInFrame(TheShapeNameHud, (getWord(%regionControl.getScreenPosition(), 0) - %leftMargin), (getWord(%regionControl.getScreenPosition(), 1) - %topMargin), ((getWord(%regionControl.getExtent(), 0) + %leftMargin) + %rightMargin));
        %numPlayers = getWordCount(%playerIDs);
        %playerNames = "";
        %n = 0;
        while ((%n < %numPlayers)) {
            %playerNames = %playerNames @ "\t" @ getWord(%playerIDs, %n).getShapeName();
            %n = (%n + 1.0);
        }
        %playerNames = trim(%playerNames);
        (%n < %numPlayers);
        %playerNames.enterFillCURLMode(BroadCastControlPanel, %photoFileName, %ext, $player.getTransform());
        BroadCastControlPanel.enterTookPhotoMode();
        removeFile(%photoFileName @ %ext);
        addFile(%photoFileName @ %ext);
        "".setBitmap(BroadCastPreview);
        %photoFileName.setBitmap(BroadCastPreview);
        1.setVisible(BroadCastPreview);
        alxPlay(AudioProfile_Shutter);
        commandToServer('FireEventPlayerTakesAPicture');
    }
    1.setVisible(BroadCastCrossHairsFrame);
    0.setVisible(BroadCastPreview);
    "".setBitmap(BroadCastPreview);
    MessageBoxOK("Can't take snapshot!", "Unable to create snapshot. Please let a Mod know, or post a note in the forums. Thank you!", "");
    1.setActive(BroadSnapshotButton);
    1.setVisible(BroadcastCloseButtonContainer);
    1.setActive(BroadSnapshotButton);
    BroadSnapshotButton_ShowSnoop();
    if (BroadcastHideHUDsCheckbox.getValue()) {
        %i = (BroadCastControlPanel.temporaryGUIControlContainer.getCount() - 1.0);
        while ((%i >= 0.0)) {
            %ctrl = %i.getObject(BroadCastControlPanel.temporaryGUIControlContainer);
            %ctrl.add(PlayGui);
            %i = (%i - 1.0);
        }
    }
    if (BroadcastHideChatCheckbox.getValue()) {
        ConvBub.add(PlayGui);
    }
    if (BroadcastHideHUDsCheckbox.getValue()) {
    }
    if (BroadcastHideChatCheckbox.getValue()) {
        BroadCastControlPanel.temporaryGUIControlContainer.delete();
        BroadCastControlPanel.temporaryGUIControlContainer = (%i >= 0.0) @ 0;
    }
    if (BroadcastHideSelfCheckbox.getValue()) {
    }
    if (BroadcastHideSelfCheckbox.isVisible()) {
        $player.getActiveSKUs().setActiveSKUs($player);
        $Player::Name.setShapeName($player);
        if (isObject($player.hudCtrl)) {
            1.setVisible($player.hudCtrl);
        }
        1.setVisible(ThePointsFloaterHud);
    }
    if (BroadcastFullScreenCheckbox.getValue()) {
        1.setVisible(BroadCastControlPanel);
    }
    1.setActive(BroadcastHideHUDsCheckbox);
    1.setActive(BroadcastHideChatCheckbox);
    1.setActive(BroadcastHideSelfCheckbox);
    if (!($IN_ORBIT_CAM)) {
    }
    !($firstPerson).setVisible(BroadcastHideSelfCheckbox);
    1.setActive(BroadcastFullScreenCheckbox);
    BroadCastControlPanel.focusAndRaise(PlayGui);
    if (%tookPhoto) {
        BroadcastCaptionCtrl.focusAndRaise(BroadcastPhotoControls);
        BroadcastCaptionCtrl.selectAll();
    }
};
function BroadcastCaptionCtrl::doOnPressEnter(%this) {
    %button = BroadSnapshotUploadButton.isVisible() ? BroadSnapshotUploadButton : BroadSnapshotUploadButtonApartment;
    %button.performClick();
};
function BroadSnapshotUploadButton::doBroadCastSnapshot(%this, %callbackSink) {
    BroadCastControlPanel.photoInhabitants.enterFillCURLMode(BroadCastControlPanel, BroadCastControlPanel.photoFileName, BroadCastControlPanel.photoFileNameExt, BroadCastControlPanel.photoTransform);
    %caption = BroadcastCaptionCtrl.getText();
    if ((%caption $= "enter caption here..")) {
        %caption = "";
    }
    %caption.setURLParam(BroadCastPreview.curl, "caption");
    "false".setURLParam(BroadCastPreview.curl, "featured");
    if (BroadcastCaptionSetBCastCtrl.visible) {
    }
    if (BroadcastCaptionSetBCastCtrl.getValue()) {
        "BroadcastScreens".setURLParam(BroadCastPreview.curl, "broadcast");
    }
    "".setURLParam(BroadCastPreview.curl, "broadcast");
    "BroadSnapshotUploadButtonOnCompleted".setCompletedCallback(BroadCastPreview.curl);
    if (!(BroadCastPreview.curl.start())) {
        BroadCastControlPanel.enterErrorUploadingMode();
    }
    if (isObject(CURLSimGroup)) {
        BroadCastPreview.curl.add(CURLSimGroup);
    }
    BroadCastControlPanel.enterUploadingMode();
};
function BroadSnapshotUploadButton::onProgress(%this, %uploader) {
};
function BroadSnapshotUploadButtonOnCompleted(%request, %result) {
    %callbackSink = %request.callBackSink;
    if ((%result == 0.0)) {
        %request.onDone(%callbackSink);
    }
    %request.onError(%callbackSink);
};
function BroadSnapshotUploadButton::onError(%this, %uploader) {
    BroadCastControlPanel.currentlyUploading = 0;
    BroadCastControlPanel.hasError = 1;
    BroadCastControlPanel.enterErrorUploadingMode();
    BroadCastPreview.curl.stop();
    error("Broadcast failed to upload");
};
function BroadSnapshotUploadButton::onDone(%this, %uploader) {
    BroadCastControlPanel.currentlyUploading = 0;
    if (($gBroadSnapshotUploadTimeOutSched != 0.0)) {
        cancel($gBroadSnapshotUploadTimeOutSched);
        $gBroadSnapshotUploadTimeOutSched = 0;
    }
    $gNumPhotosTaken = ($gNumPhotosTaken + 1.0);
    if (!("status".getResult(%uploader) $= "success")) {
        if (!(BroadCastControlPanel.hasError)) {
            %uploader.onError(%this);
        }
        return;
    }
    BroadCastControlPanel.enterTakePhotoMode();
    echo("Broadcast done. photoURL =" @ " " @ "photoURL".getResult(%uploader));
    0.setVisible(BroadCastPreview);
    "".setBitmap(BroadCastPreview);
    %shareFB = BroadcastCaptionShareFcBookCtrl.getValue();
    if (%shareFB) {
        "photoURL".getResult(%uploader).shareFcBook(BroadCastControlPanel);
    }
    %gaURL = "/client/facebookShare/snapshot/" @ %shareFB ? "yes" : "no";
    %gaURL.trackPageView(getAnalytic());
};
function BroadSnapshotCancelButton::doCancel(%this) {
    if (($gBroadSnapshotUploadTimeOutSched != 0.0)) {
        cancel($gBroadSnapshotUploadTimeOutSched);
        $gBroadSnapshotUploadTimeOutSched = 0;
    }
    BroadCastControlPanel.enterFirstTimeMode();
    echo("Broadcast cancelled");
    0.setVisible(BroadCastPreview);
    "".setBitmap(BroadCastPreview);
    if (isObject(BroadCastPreview.curl)) {
        BroadCastPreview.curl.delete();
    }
    1.setActive(BroadSnapshotButton);
};
function BroadSnapshotButton_HideSnoop() {
    %n = (TheBadgesHud.getCount() - 1.0);
    while ((%n >= 0.0)) {
        %projCtrl = %n.getObject(TheBadgesHud);
        %roleCtrl = %projCtrl.roleCtrl;
        if (isObject(%roleCtrl) && (strpos(%roleCtrl.bitmap, "neighborhoodwatch") >= 0.0)) {
            0.setVisible(%roleCtrl);
        }
        %n = (%n - 1.0);
    }
};
function BroadSnapshotButton_ShowSnoop() {
    %n = (TheBadgesHud.getCount() - 1.0);
    while ((%n >= 0.0)) {
        %projCtrl = %n.getObject(TheBadgesHud);
        %roleCtrl = %projCtrl.roleCtrl;
        if (isObject(%roleCtrl)) {
            1.setVisible(%roleCtrl);
        }
        %n = (%n - 1.0);
    }
};
$gNumPhotosTaken = 0;
function BroadcastCaptionSetBCastCtrl::onMouseUp(%this) {
    if (!(%this.getValue())) {
        MessageBoxYesNo("Broadcast to billboards?", "Checking this box will cause your snapshot to be broadcast to in-world billboards. You can do this because you are a special user. Are you sure you want this snapshot on a billboard?", "BroadcastCaptionSetBCastCtrl.setValue(true);", "BroadcastCaptionSetBCastCtrl.setValue(false);");
    }
};
function BroadCastControlPanel::enterFirstTimeMode(%this) {
    0.setVisible(BroadCastFrameForPreview);
    1.setVisible(BroadcastCloseButtonContainer);
    1.setVisible(BroadCastCrossHairsFrame);
    BroadCastControlPanel.photoFileName = "";
    BroadCastControlPanel.photoFileNameExt = "";
    BroadCastControlPanel.photoTransform = "";
    BroadCastControlPanel.photoInhabitants = "";
    1.setVisible(BroadcastTakePhotoLabel);
    mlStyle($MsgCat::photo["N-LINK-ALBUM"], "plainOnBlack").setValue(BroadcastViewAlbumLink);
    0.setVisible(BroadcastUploadSuccessfulLabel);
    0.setVisible(BroadcastPhotoControls);
    1.setVisible(BroadcastSnapshotControls);
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
    if (isObject(BroadCastPreview.curl)) {
        BroadCastPreview.curl.delete();
    }
    BroadCastPreview.curl = new URLPostObject("");
    BroadCastPreview.curl.callBackSink = BroadSnapshotUploadButton;
    1.setProgress(BroadCastPreview.curl);
    1.setRecvData(BroadCastPreview.curl);
    $Player::Name.setURLParam(BroadCastPreview.curl, "user");
    $Token.setURLParam(BroadCastPreview.curl, "token");
    "screenshot".setURLParam(BroadCastPreview.curl, "type");
    %transform.setURLParam(BroadCastPreview.curl, "location");
    %playerNames.setURLParam(BroadCastPreview.curl, "inView");
    if (!(CustomSpaceClient::GetSpaceImIn() $= "")) {
        $CSSpaceInfo.owner.setURLParam(BroadCastPreview.curl, "apartmentOwner");
        $CSSpaceInfo.vurl.setURLParam(BroadCastPreview.curl, "vurl");
    }
    "vside:/location/" @ $gContiguousSpaceName @ "/PlazaSpawns".setURLParam(BroadCastPreview.curl, "vurl");
    $Net::UploadPhotoURL.setURL(BroadCastPreview.curl);
    %photoFileName @ %ext.setPostFile(BroadCastPreview.curl, "imageBody");
    BroadCastControlPanel.photoFileName = %photoFileName;
    BroadCastControlPanel.photoFileNameExt = %ext;
    BroadCastControlPanel.photoTransform = %transform;
    BroadCastControlPanel.photoInhabitants = %playerNames;
};
function BroadCastControlPanel::enterTookPhotoMode(%this) {
    1.setVisible(BroadCastFrameForPreview);
    1.setVisible(BroadcastCaptionCtrl);
    "Enter caption here..".setText(BroadcastCaptionCtrl);
    1.setVisible(BroadcastCaptionShareFcBookCtrl);
    1.setVisible(BroadcastCaptionShareFcBookIcon);
    0.setVisible(BroadcastUploadingLabel);
    0.setVisible(BroadcastUploadFailedLabel);
    if (!(CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if (CustomSpaceClient::isOwner()) {
        0.setVisible(BroadSnapshotUploadButton);
        1.setVisible(BroadSnapshotUploadButtonApartment);
        "Take a snapshot for your apartment album!".setText(BroadcastTakePhotoLabel);
    }
    0.setVisible(BroadSnapshotUploadButtonApartment);
    1.setVisible(BroadSnapshotUploadButton);
    "Take a snapshot for your web album!".setText(BroadcastTakePhotoLabel);
    1.setActive(BroadSnapshotUploadButton);
    1.setActive(BroadSnapshotUploadButtonApartment);
    1.setVisible(BroadSnapshotCancelButton);
    1.setActive(BroadSnapshotCancelButton);
    0.setVisible(BroadcastSnapshotControls);
    1.setVisible(BroadcastPhotoControls);
};
function BroadCastControlPanel::enterUploadingMode(%this) {
    %this.currentlyUploading = 1;
    %this.hasError = 0;
    if (($gBroadSnapshotUploadTimeOutSched != 0.0)) {
        cancel($gBroadSnapshotUploadTimeOutSched);
        $gBroadSnapshotUploadTimeOutSched = 0;
    }
    $gBroadSnapshotUploadTimeOutSched = enterErrorUploadingMode.schedule(%this, 10000);
    0.setVisible(BroadcastCaptionCtrl);
    0.setVisible(BroadcastCaptionShareFcBookCtrl);
    0.setVisible(BroadcastCaptionShareFcBookIcon);
    1.setVisible(BroadcastUploadingLabel);
    0.setVisible(BroadcastUploadFailedLabel);
    0.setActive(BroadSnapshotUploadButton);
    0.setActive(BroadSnapshotUploadButtonApartment);
    1.setVisible(BroadSnapshotCancelButton);
    0.setActive(BroadSnapshotCancelButton);
    0.setVisible(BroadcastSnapshotControls);
    1.setVisible(BroadcastPhotoControls);
};
function BroadCastControlPanel::enterErrorUploadingMode(%this) {
    0.setVisible(BroadcastCaptionCtrl);
    0.setVisible(BroadcastCaptionShareFcBookCtrl);
    0.setVisible(BroadcastCaptionShareFcBookIcon);
    0.setVisible(BroadcastUploadingLabel);
    1.setVisible(BroadcastUploadFailedLabel);
    mlStyle($MsgCat::photo["E-UPLOAD-UNKNOWN"], "plainOnBlack").setValue(BroadcastUploadFailedLabel);
    1.setActive(BroadSnapshotUploadButton);
    1.setActive(BroadSnapshotUploadButtonApartment);
    1.setVisible(BroadSnapshotCancelButton);
    1.setActive(BroadSnapshotCancelButton);
    0.setVisible(BroadcastSnapshotControls);
    1.setVisible(BroadcastPhotoControls);
};
function BroadCastControlPanel::enterTakePhotoMode(%this) {
    0.setVisible(BroadCastFrameForPreview);
    1.setVisible(BroadcastCloseButtonContainer);
    1.setVisible(BroadCastCrossHairsFrame);
    BroadCastControlPanel.photoFileName = "";
    BroadCastControlPanel.photoFileNameExt = "";
    BroadCastControlPanel.photoTransform = "";
    BroadCastControlPanel.photoInhabitants = "";
    0.setVisible(BroadcastTakePhotoLabel);
    1.setVisible(BroadcastUploadSuccessfulLabel);
    0.setVisible(BroadcastPhotoControls);
    1.setVisible(BroadcastSnapshotControls);
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
    if ((%preamblePos < 0.0)) {
        error(getScopeName() @ " " @ "- invalid photo URL." @ " " @ %photoURL @ " " @ getTrace());
    }
    %justPhotoID = getSubStr(%photoURL, (%preamblePos + strlen(%preambleText)), 1000);
    %viewPhotoURL = $Net::PhotoPageURL @ %justPhotoID @ "?ref=fb";
    %viewPhotoURLEncoded = %viewPhotoURL;
    %viewPhotoURLEncoded = urlEncode(%viewPhotoURLEncoded);
    %viewPhotoURLEncoded = strreplace(%viewPhotoURLEncoded, "/", "%2F");
    %sharerURL = "http://www.facebook.com/sharer.php";
    %sharerURL = %sharerURL @ "?u=" @ %viewPhotoURLEncoded;
    gotoWebPage(%sharerURL);
};
