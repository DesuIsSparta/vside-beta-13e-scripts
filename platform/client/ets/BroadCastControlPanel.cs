$gBroadSnapshotUploadTimeOutSched = 0;
function BroadCastControlPanel::toggle(%this)
{
    PlayGui.showRaiseOrHide(%this);
    return ;
}
function BroadCastControlPanel::open(%this)
{
    BroadCastCrossHairsFrame.resize(getWord(BroadCastRegionControl.getExtent(), 0) - 1, getWord(BroadCastRegionControl.getExtent(), 1) - 1);
    BroadCastCrossHairsFrame.resize(getWord(BroadCastRegionControl.getExtent(), 0), getWord(BroadCastRegionControl.getExtent(), 1));
    %this.setVisible(1);
    %this.setConstrained(1);
    %this.currentlyUploading = 0;
    %this.hasError = 0;
    %this.enterFirstTimeMode();
    PlayGui.focusAndRaise(%this);
    return ;
}
function BroadCastControlPanel::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    BroadCastPreview.setVisible(0);
    BroadCastPreview.setBitmap("");
    BroadCastControlPanel.photoFileName = "";
    BroadCastControlPanel.photoFileNameExt = "";
    BroadCastControlPanel.photoTransform = "";
    BroadCastControlPanel.photoInhabitants = "";
    return 1;
}
function BroadSnapshotButton_prepareForDoTakeSnapshot()
{
    BroadcastCloseButtonContainer.setVisible(0);
    BroadCastCrossHairsFrame.setVisible(0);
    BroadcastHideHUDsCheckbox.setActive(0);
    BroadcastHideChatCheckbox.setActive(0);
    BroadcastHideSelfCheckbox.setActive(0);
    BroadcastFullScreenCheckbox.setActive(0);
    BroadSnapshotButton_HideSnoop();
    if (BroadcastHideHUDsCheckbox.getValue())
    {
        if (BroadCastControlPanel.temporaryGUIControlContainer == 0)
        {
            BroadCastControlPanel.temporaryGUIControlContainer = new GuiControl();
        }
        BroadCastControlPanel.temporaryGUIControlContainer.setVisible(0);
        %orderedChildren = PlayGui.getChildrenInOrder(BroadCastControlPanel.playGuiControlsToHide);
        %i = getWordCount(%orderedChildren) - 1;
        while (%i >= 0)
        {
            %ctrl = getWord(%orderedChildren, %i);
            BroadCastControlPanel.temporaryGUIControlContainer.add(%ctrl);
            %i = %i - 1;
        }
    }
    if (BroadcastHideChatCheckbox.getValue())
    {
        if (BroadCastControlPanel.temporaryGUIControlContainer == 0)
        {
            BroadCastControlPanel.temporaryGUIControlContainer = new GuiControl();
        }
        BroadCastControlPanel.temporaryGUIControlContainer.setVisible(0);
        BroadCastControlPanel.temporaryGUIControlContainer.add(ConvBub);
    }
    if (BroadcastHideSelfCheckbox.getValue() && BroadcastHideSelfCheckbox.isVisible())
    {
        if (!$IN_ORBIT_CAM && !$firstPerson)
        {
            $player.MeshOff("*");
            $player.setShapeName("");
            if (isObject($player.hudCtrl))
            {
                $player.hudCtrl.setVisible(0);
            }
            ThePointsFloaterHud.setVisible(0);
        }
    }
    if (BroadcastFullScreenCheckbox.getValue())
    {
        BroadCastControlPanel.setVisible(0);
    }
    if (($Platform $= "windows") && ($Platform::Version::Major == 6))
    {
        waitAFrameAndCall("waitAFrameAndCall(\"BroadSnapshotButton_doTakeSnapshot\");");
    }
    else
    {
        waitAFrameAndCall("BroadSnapshotButton_doTakeSnapshot");
    }
    return ;
}
function BroadSnapshotButton_doTakeSnapshot()
{
    BroadSnapshotButton.setActive(0);
    %photoFileName = $DC::LocalAvatarFolder @ "/lastSnapshotTaken";
    if ($Pref::Video::screenShotFormat $= "JPEG")
    {
        %ext = ".jpg";
    }
    else
    {
        if ($Pref::Video::screenShotFormat $= "PNG")
        {
            %ext = ".png";
        }
        else
        {
            %ext = ".png";
        }
    }
    %regionControl = 0;
    if (BroadcastFullScreenCheckbox.getValue())
    {
        %regionControl = PlayGui.getId();
    }
    else
    {
        %regionControl = BroadCastRegionControl.getId();
    }
    %tookPhoto = (%regionControl != 0) && snapshotTool::snapControl(%regionControl, %photoFileName @ %ext);
    if (%tookPhoto)
    {
        %topMargin = 60;
        %bottomMargin = -10;
        %leftMargin = 0;
        %rightMargin = 0;
        %playerIDs = TheShapeNameHud.getPlayerIDsInViewAndInRangeAndInFrame(getWord(%regionControl.getScreenPosition(), 0) - %leftMargin, getWord(%regionControl.getScreenPosition(), 1) - %topMargin, (getWord(%regionControl.getExtent(), 0) + %leftMargin) + %rightMargin, (getWord(%regionControl.getExtent(), 1) + %topMargin) + %bottomMargin);
        %numPlayers = getWordCount(%playerIDs);
        %playerNames = "";
        %n = 0;
        while (%n < %numPlayers)
        {
            %playerNames = %playerNames TAB getWord(%playerIDs, %n).getShapeName();
            %n = %n + 1;
        }
        %playerNames = trim(%playerNames);
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
    else
    {
        BroadCastCrossHairsFrame.setVisible(1);
        BroadCastPreview.setVisible(0);
        BroadCastPreview.setBitmap("");
        MessageBoxOK("Can\'t take snapshot!", "Unable to create snapshot. Please let a Mod know, or post a note in the forums. Thank you!", "");
        BroadSnapshotButton.setActive(1);
    }
    BroadcastCloseButtonContainer.setVisible(1);
    BroadSnapshotButton.setActive(1);
    BroadSnapshotButton_ShowSnoop();
    if (BroadcastHideHUDsCheckbox.getValue())
    {
        %i = BroadCastControlPanel.temporaryGUIControlContainer.getCount() - 1;
        while (%i >= 0)
        {
            %ctrl = BroadCastControlPanel.temporaryGUIControlContainer.getObject(%i);
            PlayGui.add(%ctrl);
            %i = %i - 1;
        }
    }
    if (BroadcastHideChatCheckbox.getValue())
    {
        PlayGui.add(ConvBub);
    }
    if (BroadcastHideHUDsCheckbox.getValue() && BroadcastHideChatCheckbox.getValue())
    {
        BroadCastControlPanel.temporaryGUIControlContainer.delete();
        BroadCastControlPanel.temporaryGUIControlContainer = 0;
    }
    if (BroadcastHideSelfCheckbox.getValue() && BroadcastHideSelfCheckbox.isVisible())
    {
        $player.setActiveSKUs($player.getActiveSKUs());
        $player.setShapeName($Player::Name);
        if (isObject($player.hudCtrl))
        {
            $player.hudCtrl.setVisible(1);
        }
        ThePointsFloaterHud.setVisible(1);
    }
    if (BroadcastFullScreenCheckbox.getValue())
    {
        BroadCastControlPanel.setVisible(1);
    }
    BroadcastHideHUDsCheckbox.setActive(1);
    BroadcastHideChatCheckbox.setActive(1);
    BroadcastHideSelfCheckbox.setActive(1);
    BroadcastHideSelfCheckbox.setVisible(!$IN_ORBIT_CAM && !$firstPerson);
    BroadcastFullScreenCheckbox.setActive(1);
    PlayGui.focusAndRaise(BroadCastControlPanel);
    if (%tookPhoto)
    {
        BroadcastPhotoControls.focusAndRaise(BroadcastCaptionCtrl);
        BroadcastCaptionCtrl.selectAll();
    }
    return ;
}
function BroadcastCaptionCtrl::doOnPressEnter(%this)
{
    %button = BroadSnapshotUploadButton.isVisible() ? BroadSnapshotUploadButton : BroadSnapshotUploadButtonApartment;
    %button.performClick();
    return ;
}
function BroadSnapshotUploadButton::doBroadCastSnapshot(%this, %callbackSink)
{
    BroadCastControlPanel.enterFillCURLMode(BroadCastControlPanel.photoFileName, BroadCastControlPanel.photoFileNameExt, BroadCastControlPanel.photoTransform, BroadCastControlPanel.photoInhabitants);
    %caption = BroadcastCaptionCtrl.getText();
    if (%caption $= "enter caption here..")
    {
        %caption = "";
    }
    BroadCastPreview.curl.setURLParam("caption", %caption);
    BroadCastPreview.curl.setURLParam("featured", "false");
    if (BroadcastCaptionSetBCastCtrl.visible && BroadcastCaptionSetBCastCtrl.getValue())
    {
        BroadCastPreview.curl.setURLParam("broadcast", "BroadcastScreens");
    }
    else
    {
        BroadCastPreview.curl.setURLParam("broadcast", "");
    }
    BroadCastPreview.curl.setCompletedCallback("BroadSnapshotUploadButtonOnCompleted");
    if (!BroadCastPreview.curl.start())
    {
        BroadCastControlPanel.enterErrorUploadingMode();
    }
    else
    {
        if (isObject(CURLSimGroup))
        {
            CURLSimGroup.add(BroadCastPreview.curl);
        }
        BroadCastControlPanel.enterUploadingMode();
    }
    return ;
}
function BroadSnapshotUploadButton::onProgress(%this, %uploader)
{
    return ;
}
function BroadSnapshotUploadButtonOnCompleted(%request, %result)
{
    %callbackSink = %request.callBackSink;
    if (%result == 0)
    {
        %callbackSink.onDone(%request);
    }
    else
    {
        %callbackSink.onError(%request);
    }
    return ;
}
function BroadSnapshotUploadButton::onError(%this, %uploader)
{
    BroadCastControlPanel.currentlyUploading = 0;
    BroadCastControlPanel.hasError = 1;
    BroadCastControlPanel.enterErrorUploadingMode();
    BroadCastPreview.curl.stop();
    error("Broadcast failed to upload");
    return ;
}
function BroadSnapshotUploadButton::onDone(%this, %uploader)
{
    BroadCastControlPanel.currentlyUploading = 0;
    if ($gBroadSnapshotUploadTimeOutSched != 0)
    {
        cancel($gBroadSnapshotUploadTimeOutSched);
        $gBroadSnapshotUploadTimeOutSched = 0;
    }
    $gNumPhotosTaken = $gNumPhotosTaken + 1;
    if (!(%uploader.getResult("status") $= "success"))
    {
        if (!BroadCastControlPanel.hasError)
        {
            %this.onError(%uploader);
        }
        return ;
    }
    BroadCastControlPanel.enterTakePhotoMode();
    echo("Broadcast done. photoURL =" SPC %uploader.getResult("photoURL"));
    BroadCastPreview.setVisible(0);
    BroadCastPreview.setBitmap("");
    %shareFB = BroadcastCaptionShareFcBookCtrl.getValue();
    if (%shareFB)
    {
        BroadCastControlPanel.shareFcBook(%uploader.getResult("photoURL"));
    }
    %gaURL = "/client/facebookShare/snapshot/" @ %shareFB ? "yes" : "no";
    getAnalytic().trackPageView(%gaURL);
    return ;
}
function BroadSnapshotCancelButton::doCancel(%this)
{
    if ($gBroadSnapshotUploadTimeOutSched != 0)
    {
        cancel($gBroadSnapshotUploadTimeOutSched);
        $gBroadSnapshotUploadTimeOutSched = 0;
    }
    BroadCastControlPanel.enterFirstTimeMode();
    echo("Broadcast cancelled");
    BroadCastPreview.setVisible(0);
    BroadCastPreview.setBitmap("");
    if (isObject(BroadCastPreview.curl))
    {
        BroadCastPreview.curl.delete();
    }
    BroadSnapshotButton.setActive(1);
    return ;
}
function BroadSnapshotButton_HideSnoop()
{
    %n = TheBadgesHud.getCount() - 1;
    while (%n >= 0)
    {
        %projCtrl = TheBadgesHud.getObject(%n);
        %roleCtrl = %projCtrl.roleCtrl;
        if (isObject(%roleCtrl))
        {
            if (strpos(%roleCtrl.bitmap, "neighborhoodwatch") >= 0)
            {
                %roleCtrl.setVisible(0);
            }
        }
        %n = %n - 1;
    }
}

function BroadSnapshotButton_ShowSnoop()
{
    %n = TheBadgesHud.getCount() - 1;
    while (%n >= 0)
    {
        %projCtrl = TheBadgesHud.getObject(%n);
        %roleCtrl = %projCtrl.roleCtrl;
        if (isObject(%roleCtrl))
        {
            %roleCtrl.setVisible(1);
        }
        %n = %n - 1;
    }
}

$gNumPhotosTaken = 0;
function BroadcastCaptionSetBCastCtrl::onMouseUp(%this)
{
    if (!%this.getValue())
    {
        MessageBoxYesNo("Broadcast to billboards?", "Checking this box will cause your snapshot to be broadcast to in-world billboards. You can do this because you are a special user. Are you sure you want this snapshot on a billboard?", "BroadcastCaptionSetBCastCtrl.setValue(true);", "BroadcastCaptionSetBCastCtrl.setValue(false);");
    }
    return ;
}
function BroadCastControlPanel::enterFirstTimeMode(%this)
{
    BroadCastFrameForPreview.setVisible(0);
    BroadcastCloseButtonContainer.setVisible(1);
    BroadCastCrossHairsFrame.setVisible(1);
    BroadCastControlPanel.photoFileName = "";
    BroadCastControlPanel.photoFileNameExt = "";
    BroadCastControlPanel.photoTransform = "";
    BroadCastControlPanel.photoInhabitants = "";
    BroadcastTakePhotoLabel.setVisible(1);
    BroadcastViewAlbumLink.setValue(mlStyle($MsgCat::photo["N-LINK-ALBUM"], "plainOnBlack"));
    BroadcastUploadSuccessfulLabel.setVisible(0);
    BroadcastPhotoControls.setVisible(0);
    BroadcastSnapshotControls.setVisible(1);
    return ;
}
function BroadCastControlPanel::enterFillCURLMode(%this, %photoFileName, %ext, %transform, %playerNames)
{
    if (%photoFileName $= "")
    {
        error("BroadCastControlPanel::enterFillCURLMode called with empty filename");
        %this.enterFirstTimeMode();
        return ;
    }
    if (%ext $= "")
    {
        error("BroadCastControlPanel::enterFillCURLMode called with empty file extention");
        %this.enterFirstTimeMode();
        return ;
    }
    if (isObject(BroadCastPreview.curl))
    {
        BroadCastPreview.curl.delete();
    }
    BroadCastPreview.curl = new URLPostObject();
    BroadCastPreview.curl.callBackSink = BroadSnapshotUploadButton;
    BroadCastPreview.curl.setProgress(1);
    BroadCastPreview.curl.setRecvData(1);
    BroadCastPreview.curl.setURLParam("user", $Player::Name);
    BroadCastPreview.curl.setURLParam("token", $Token);
    BroadCastPreview.curl.setURLParam("type", "screenshot");
    BroadCastPreview.curl.setURLParam("location", %transform);
    BroadCastPreview.curl.setURLParam("inView", %playerNames);
    if (!(CustomSpaceClient::GetSpaceImIn() $= ""))
    {
        BroadCastPreview.curl.setURLParam("apartmentOwner", $CSSpaceInfo.owner);
        BroadCastPreview.curl.setURLParam("vurl", $CSSpaceInfo.vurl);
    }
    else
    {
        BroadCastPreview.curl.setURLParam("vurl", "vside:/location/" @ $gContiguousSpaceName @ "/PlazaSpawns");
    }
    BroadCastPreview.curl.setURL($Net::UploadPhotoURL);
    BroadCastPreview.curl.setPostFile("imageBody", %photoFileName @ %ext);
    BroadCastControlPanel.photoFileName = %photoFileName;
    BroadCastControlPanel.photoFileNameExt = %ext;
    BroadCastControlPanel.photoTransform = %transform;
    BroadCastControlPanel.photoInhabitants = %playerNames;
    return ;
}
function BroadCastControlPanel::enterTookPhotoMode(%this)
{
    BroadCastFrameForPreview.setVisible(1);
    BroadcastCaptionCtrl.setVisible(1);
    BroadcastCaptionCtrl.setText("Enter caption here..");
    BroadcastCaptionShareFcBookCtrl.setVisible(1);
    BroadcastCaptionShareFcBookIcon.setVisible(1);
    BroadcastUploadingLabel.setVisible(0);
    BroadcastUploadFailedLabel.setVisible(0);
    if (!((CustomSpaceClient::GetSpaceImIn() $= "")) && CustomSpaceClient::isOwner())
    {
        BroadSnapshotUploadButton.setVisible(0);
        BroadSnapshotUploadButtonApartment.setVisible(1);
        BroadcastTakePhotoLabel.setText("Take a snapshot for your apartment album!");
    }
    else
    {
        BroadSnapshotUploadButtonApartment.setVisible(0);
        BroadSnapshotUploadButton.setVisible(1);
        BroadcastTakePhotoLabel.setText("Take a snapshot for your web album!");
    }
    BroadSnapshotUploadButton.setActive(1);
    BroadSnapshotUploadButtonApartment.setActive(1);
    BroadSnapshotCancelButton.setVisible(1);
    BroadSnapshotCancelButton.setActive(1);
    BroadcastSnapshotControls.setVisible(0);
    BroadcastPhotoControls.setVisible(1);
    return ;
}
function BroadCastControlPanel::enterUploadingMode(%this)
{
    %this.currentlyUploading = 1;
    %this.hasError = 0;
    if ($gBroadSnapshotUploadTimeOutSched != 0)
    {
        cancel($gBroadSnapshotUploadTimeOutSched);
        $gBroadSnapshotUploadTimeOutSched = 0;
    }
    $gBroadSnapshotUploadTimeOutSched = %this.schedule(10000, enterErrorUploadingMode);
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
    return ;
}
function BroadCastControlPanel::enterErrorUploadingMode(%this)
{
    BroadcastCaptionCtrl.setVisible(0);
    BroadcastCaptionShareFcBookCtrl.setVisible(0);
    BroadcastCaptionShareFcBookIcon.setVisible(0);
    BroadcastUploadingLabel.setVisible(0);
    BroadcastUploadFailedLabel.setVisible(1);
    BroadcastUploadFailedLabel.setValue(mlStyle($MsgCat::photo["E-UPLOAD-UNKNOWN"], "plainOnBlack"));
    BroadSnapshotUploadButton.setActive(1);
    BroadSnapshotUploadButtonApartment.setActive(1);
    BroadSnapshotCancelButton.setVisible(1);
    BroadSnapshotCancelButton.setActive(1);
    BroadcastSnapshotControls.setVisible(0);
    BroadcastPhotoControls.setVisible(1);
    return ;
}
function BroadCastControlPanel::enterTakePhotoMode(%this)
{
    BroadCastFrameForPreview.setVisible(0);
    BroadcastCloseButtonContainer.setVisible(1);
    BroadCastCrossHairsFrame.setVisible(1);
    BroadCastControlPanel.photoFileName = "";
    BroadCastControlPanel.photoFileNameExt = "";
    BroadCastControlPanel.photoTransform = "";
    BroadCastControlPanel.photoInhabitants = "";
    BroadcastTakePhotoLabel.setVisible(0);
    BroadcastUploadSuccessfulLabel.setVisible(1);
    BroadcastPhotoControls.setVisible(0);
    BroadcastSnapshotControls.setVisible(1);
    return ;
}
function BroadCastControlPanel::automateSnapshotUpload(%this)
{
    %this.open();
    BroadSnapshotButton_doTakeSnapshot();
    BroadSnapshotUploadButton.doBroadCastSnapshot(BroadSnapshotUploadButton);
    %this.close();
    return ;
}
function BroadCastControlPanel::shareFcBook(%this, %photoURL)
{
    %preambleText = "/photoservice/";
    %preamblePos = strpos(%photoURL, %preambleText);
    if (%preamblePos < 0)
    {
        error(getScopeName() SPC "- invalid photo URL." SPC %photoURL SPC getTrace());
    }
    %justPhotoID = getSubStr(%photoURL, %preamblePos + strlen(%preambleText), 1000);
    %viewPhotoURL = $Net::PhotoPageURL @ %justPhotoID @ "?ref=fb";
    %viewPhotoURLEncoded = %viewPhotoURL;
    %viewPhotoURLEncoded = urlEncode(%viewPhotoURLEncoded);
    %viewPhotoURLEncoded = strreplace(%viewPhotoURLEncoded, "/", "%2F");
    %sharerURL = "http://www.facebook.com/sharer.php";
    %sharerURL = %sharerURL @ "?u=" @ %viewPhotoURLEncoded;
    gotoWebPage(%sharerURL);
    return ;
}
