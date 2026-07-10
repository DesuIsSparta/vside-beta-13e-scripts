$gBroadSnapshotUploadTimeOutSched = 0;
function BroadCastControlPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function BroadCastControlPanel::open(%this) {
    (1.0 - getWord(BroadCastRegionControl.getExtent(), 0)).resize((1.0 - getWord(BroadCastRegionControl.getExtent(), 1)));
    getWord(BroadCastRegionControl.getExtent(), 0).resize(getWord(BroadCastRegionControl.getExtent(), 1));
    %this.setVisible(1);
    %this.setConstrained(1);
    %this.currentlyUploading = BroadCastCrossHairsFrame @ 0;
    BroadCastCrossHairsFrame;
    %this.hasError = 0;
    %this.enterFirstTimeMode();
    %this.focusAndRaise();
};
function BroadCastControlPanel::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    0.setVisible();
    "".setBitmap();
    %this.photoFileName = "" @ BroadCastControlPanel;
    BroadCastPreview;
    %this.photoFileNameExt = "" @ BroadCastControlPanel;
    BroadCastPreview;
    %this.photoTransform = "" @ BroadCastControlPanel;
    %this.photoInhabitants = "" @ BroadCastControlPanel;
    return 1;
};
function BroadSnapshotButton_prepareForDoTakeSnapshot() {
    0.setVisible();
    0.setVisible();
    0.setActive();
    0.setActive();
    0.setActive();
    0.setActive();
    BroadSnapshotButton_HideSnoop();
    if (BroadcastHideHUDsCheckbox.getValue()) {
        if ((BroadCastControlPanel == %this.temporaryGUIControlContainer)) {
            %this.temporaryGUIControlContainer = new ""(); @ BroadCastControlPanel;
            GuiControl;
        }
        %this.temporaryGUIControlContainer.setVisible(0);
        %orderedChildren = %this.playGuiControlsToHide.getChildrenInOrder();
        BroadCastControlPanel;
        %i = (1.0 - getWordCount(%orderedChildren));
        PlayGui;
        if ((0.0 >= %i)) {
            %ctrl = getWord(%orderedChildren, %i);
            BroadCastControlPanel;
            %this.temporaryGUIControlContainer.add(%ctrl);
            %i = (1.0 - %i);
            BroadCastControlPanel;
        }
    }
    if (BroadcastHideChatCheckbox.getValue()) {
        if ((BroadCastControlPanel == %this.temporaryGUIControlContainer)) {
            %this.temporaryGUIControlContainer = new ""(); @ BroadCastControlPanel;
            GuiControl;
        }
        %this.temporaryGUIControlContainer.setVisible(0);
        %this.temporaryGUIControlContainer.add();
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
            0.setVisible();
        }
    }
    if (BroadcastFullScreenCheckbox.getValue()) {
        0.setVisible();
    }
    if ((BroadCastControlPanel @ " " @ $Platform $= "windows")) {
    }
    if ((6.0 == $Platform::Version::Major)) {
        waitAFrameAndCall("waitAFrameAndCall(\"BroadSnapshotButton_doTakeSnapshot\");");
    }
    waitAFrameAndCall("BroadSnapshotButton_doTakeSnapshot");
};
function BroadSnapshotButton_doTakeSnapshot() {
    0.setActive();
    %photoFileName = $DC::LocalAvatarFolder @ "/lastSnapshotTaken";
    BroadSnapshotButton;
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
        %playerIDs = (%leftMargin - getWord(%regionControl.getScreenPosition(), 0)).getPlayerIDsInViewAndInRangeAndInFrame((%topMargin - getWord(%regionControl.getScreenPosition(), 1)), (%rightMargin + (%leftMargin + getWord(%regionControl.getExtent(), 0))), (%bottomMargin + (%topMargin + getWord(%regionControl.getExtent(), 1))));
        TheShapeNameHud;
        %numPlayers = getWordCount(%playerIDs);
        %playerNames = "";
        %n = 0;
        if ((%numPlayers < %n)) {
            %playerNames = %playerNames @ "\t" @ getWord(%playerIDs, %n).getShapeName();
            %n = (1.0 + %n);
        }
        %playerNames = trim(%playerNames);
        (%numPlayers < %n);
        %photoFileName.enterFillCURLMode(%ext, $player.getTransform(), %playerNames);
        BroadCastControlPanel.enterTookPhotoMode();
        removeFile(%photoFileName @ %ext);
        addFile(%photoFileName @ %ext);
        "".setBitmap();
        %photoFileName.setBitmap();
        1.setVisible();
        alxPlay(AudioProfile_Shutter);
        commandToServer('FireEventPlayerTakesAPicture');
    }
    1.setVisible();
    0.setVisible();
    "".setBitmap();
    MessageBoxOK("Can't take snapshot!", "Unable to create snapshot. Please let a Mod know, or post a note in the forums. Thank you!", "");
    1.setActive();
    1.setVisible();
    1.setActive();
    BroadSnapshotButton_ShowSnoop();
    if (BroadcastHideHUDsCheckbox.getValue()) {
        %i = (BroadCastControlPanel - $player.temporaryGUIControlContainer.getCount());
        1.0;
        if ((0.0 >= %i)) {
            %ctrl = $player.temporaryGUIControlContainer.getObject(%i);
            BroadCastControlPanel;
            %ctrl.add();
            %i = (1.0 - %i);
            PlayGui;
        }
    }
    if (BroadcastHideChatCheckbox.getValue()) {
        PlayGui.add(ConvBub);
    }
    if (BroadcastHideHUDsCheckbox.getValue()) {
    }
    if (BroadcastHideChatCheckbox.getValue()) {
        $player.temporaryGUIControlContainer.delete();
        $player.temporaryGUIControlContainer = 0 @ BroadCastControlPanel;
        BroadCastControlPanel;
    }
    if (BroadcastHideSelfCheckbox.getValue()) {
    }
    if (BroadcastHideSelfCheckbox.isVisible()) {
        $player.setActiveSKUs($player.getActiveSKUs());
        $player.setShapeName($Player::Name);
        if (isObject($player.hudCtrl)) {
            $player.hudCtrl.setVisible(1);
        }
        1.setVisible();
    }
    if (BroadcastFullScreenCheckbox.getValue()) {
        1.setVisible();
    }
    1.setActive();
    1.setActive();
    1.setActive();
    if (!($IN_ORBIT_CAM)) {
    }
    !($firstPerson).setVisible();
    1.setActive();
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
    $player.photoFileName.enterFillCURLMode($player.photoFileNameExt, $player.photoTransform, $player.photoInhabitants);
    %caption = BroadcastCaptionCtrl.getText();
    BroadCastControlPanel;
    if ((BroadCastControlPanel @ " " @ %caption $= "enter caption here..")) {
        %caption = "";
        BroadCastControlPanel;
    }
    $player.curl.setURLParam("caption", %caption);
    $player.curl.setURLParam("featured", "false");
    if ($player.visible) {
    }
    if (BroadcastCaptionSetBCastCtrl.getValue()) {
        $player.curl.setURLParam("broadcast", "BroadcastScreens");
    }
    $player.curl.setURLParam("broadcast", "");
    $player.curl.setCompletedCallback("BroadSnapshotUploadButtonOnCompleted");
    if (!($player.curl.start())) {
        BroadCastControlPanel.enterErrorUploadingMode();
    }
    if (isObject(CURLSimGroup)) {
        $player.curl.add();
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
    %request.curl.stop();
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
    0.setVisible();
    "".setBitmap();
    %shareFB = BroadcastCaptionShareFcBookCtrl.getValue();
    BroadCastPreview;
    if (%shareFB) {
        %uploader.getResult("photoURL").shareFcBook();
    }
    %gaURL = "/client/facebookShare/snapshot/" @ %shareFB ? "yes" : "no";
    BroadCastControlPanel;
    getAnalytic().trackPageView(%gaURL);
};
function BroadSnapshotCancelButton::doCancel(%this) {
    if ((0.0 != $gBroadSnapshotUploadTimeOutSched)) {
        cancel($gBroadSnapshotUploadTimeOutSched);
        $gBroadSnapshotUploadTimeOutSched = 0;
    }
    BroadCastControlPanel.enterFirstTimeMode();
    echo("Broadcast cancelled");
    0.setVisible();
    "".setBitmap();
    if (isObject(%request.curl)) {
        %request.curl.delete();
    }
    1.setActive();
};
function BroadSnapshotButton_HideSnoop() {
    %n = (1.0 - TheBadgesHud.getCount());
    if ((0.0 >= %n)) {
        %projCtrl = %n.getObject();
        TheBadgesHud;
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
        %projCtrl = %n.getObject();
        TheBadgesHud;
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
    0.setVisible();
    1.setVisible();
    1.setVisible();
    %projCtrl.photoFileName = "" @ BroadCastControlPanel;
    BroadCastCrossHairsFrame;
    %projCtrl.photoFileNameExt = "" @ BroadCastControlPanel;
    BroadcastCloseButtonContainer;
    %projCtrl.photoTransform = "" @ BroadCastControlPanel;
    BroadCastFrameForPreview;
    %projCtrl.photoInhabitants = "" @ BroadCastControlPanel;
    1.setVisible();
    mlStyle(BroadcastViewAlbumLink, "plainOnBlack").setValue();
    0.setVisible();
    0.setVisible();
    1.setVisible();
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
    if (isObject(%projCtrl.curl)) {
        %projCtrl.curl.delete();
    }
    %projCtrl.curl = new ""(); @ BroadCastPreview;
    URLPostObject;
    %projCtrl.curl.callBackSink = BroadSnapshotUploadButton @ BroadCastPreview;
    0;
    %projCtrl.curl.curl.setProgress(1);
    %projCtrl.curl.curl.setRecvData(1);
    %projCtrl.curl.curl.setURLParam("user", $Player::Name);
    %projCtrl.curl.curl.setURLParam("token", $Token);
    %projCtrl.curl.curl.setURLParam("type", "screenshot");
    %projCtrl.curl.curl.setURLParam("location", %transform);
    %projCtrl.curl.curl.setURLParam("inView", %playerNames);
    if (!(BroadCastPreview @ " " @ CustomSpaceClient::GetSpaceImIn() $= "")) {
        %projCtrl.curl.curl.setURLParam("apartmentOwner", $CSSpaceInfo.owner);
        $CSSpaceInfo.curl.setURLParam("vurl", $CSSpaceInfo.vurl);
    }
    $CSSpaceInfo.curl.setURLParam("vurl", "vside:/location/" @ $gContiguousSpaceName @ "/PlazaSpawns");
    $CSSpaceInfo.curl.setURL($Net::UploadPhotoURL);
    $CSSpaceInfo.curl.setPostFile("imageBody", %photoFileName @ %ext);
    $CSSpaceInfo.photoFileName = %photoFileName @ BroadCastControlPanel;
    BroadCastPreview;
    $CSSpaceInfo.photoFileNameExt = %ext @ BroadCastControlPanel;
    BroadCastPreview;
    $CSSpaceInfo.photoTransform = %transform @ BroadCastControlPanel;
    BroadCastPreview;
    $CSSpaceInfo.photoInhabitants = %playerNames @ BroadCastControlPanel;
    BroadCastPreview;
};
function BroadCastControlPanel::enterTookPhotoMode(%this) {
    1.setVisible();
    1.setVisible();
    "Enter caption here..".setText();
    1.setVisible();
    1.setVisible();
    0.setVisible();
    0.setVisible();
    if (!(BroadcastUploadFailedLabel @ " " @ CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if (CustomSpaceClient::isOwner()) {
        0.setVisible();
        1.setVisible();
        "Take a snapshot for your apartment album!".setText();
    }
    0.setVisible();
    1.setVisible();
    "Take a snapshot for your web album!".setText();
    1.setActive();
    1.setActive();
    1.setVisible();
    1.setActive();
    0.setVisible();
    1.setVisible();
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
    0.setVisible();
    0.setVisible();
    0.setVisible();
    1.setVisible();
    0.setVisible();
    0.setActive();
    0.setActive();
    1.setVisible();
    0.setActive();
    0.setVisible();
    1.setVisible();
};
function BroadCastControlPanel::enterErrorUploadingMode(%this) {
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    1.setVisible();
    mlStyle(BroadcastUploadFailedLabel, "plainOnBlack").setValue();
    1.setActive();
    1.setActive();
    1.setVisible();
    1.setActive();
    0.setVisible();
    1.setVisible();
};
function BroadCastControlPanel::enterTakePhotoMode(%this) {
    0.setVisible();
    1.setVisible();
    1.setVisible();
    %this.photoFileName = "" @ BroadCastControlPanel;
    BroadCastCrossHairsFrame;
    %this.photoFileNameExt = "" @ BroadCastControlPanel;
    BroadcastCloseButtonContainer;
    %this.photoTransform = "" @ BroadCastControlPanel;
    BroadCastFrameForPreview;
    %this.photoInhabitants = "" @ BroadCastControlPanel;
    0.setVisible();
    1.setVisible();
    0.setVisible();
    1.setVisible();
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
