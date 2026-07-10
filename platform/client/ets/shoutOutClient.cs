function geShoutOutWindow::toggle(%this) {
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
function geShoutOutWindow::open(%this) {
    %this.setVisible(1);
    %this.focusAndRaise();
    if (!(%this.alreadySeen)) {
        %this.alignToCenterXY();
        %this.alreadySeen = PlayGui @ 1;
    }
    shoutOut_action_GroundState();
    %pricePhraseTicker = shoutout_getPriceVBux_Ticker() @ " " @ "vBux";
    "vSide Ticker -" @ " " @ %pricePhraseTicker.setText();
    if ((shoutout_getPriceVBux_Ticker() < $Player::VBux)) {
        0.setValue();
        0.setActive();
    }
    1.setActive();
    MessageHudEdit.getText().setText();
    1.makeFirstResponder();
    geShoutout_Snapshot_Message.onKeystroke();
    if ((geShoutout_Snapshot_Message @ " " @ geShoutout_Credential_Twitter_Username.getText() $= "")) {
        $Player::Name.getProperty("twitter_un", "").setText();
    }
    if ((gUserPropMgrClient @ " " @ geShoutout_Credential_Twitter_Password.getText() $= "")) {
        $Player::Name.getProperty("twitter_pw", "").setText();
    }
    $Player::Name.getProperty("twitter_un_save", 1).setValue();
    $Player::Name.getProperty("twitter_pw_save", 1).setValue();
    shoutOut_action_testTwitterCredentials(1);
};
function geShoutOutWindow::close(%this) {
    %this.setVisible(0);
    if (!(MessageHudEdit.getText() $= "")) {
        1.setVisible();
        1.makeFirstResponder();
    }
    PlayGui.focusTopWindow();
    return 1;
};
function shoutout_open(%text) {
    %isGW = ($gContiguousSpaceName $= "gw");
    if (%isGW) {
        echo(getScopeName() @ " " @ "- no ticker in gateway");
        return;
    }
    0.setVisible();
    geShoutOutWindow.open();
    shoutout_setIncludeSnapshot($UserPref::UI::ShoutOut::Show::Pic);
    shoutout_takeSnapshot();
};
function shoutout_getPriceVBux_Ticker() {
    %sku = ;
    %si = %sku.findBySku();
    SkuManager;
    return %si.price;
};
function shoutout_setIncludeSnapshot(%includeIt) {
    %text = "Re-Take Snapshot";
    %text = "<color:ffffff><linkcolor:ffffff><linkcolorhl:e553ff><shadowcolor:000000><outline><a:gamelink:RETAKE>" @ %text @ "</a>";
    %text.setText();
    %includeIt.setVisible();
    %si.modulationColor = %includeIt ? "255 255 255 255" : "255 255 255 80" @ geShoutout_Snapshot;
    geShoutout_Snapshot_Opt_Pic_Options;
    $UserPref::UI::ShoutOut::Show::Pic = %includeIt;
    geShoutout_Snapshot_Retake;
    shoutout_setTwitterInclude($UserPref::UI::ShoutOut::Twitter::Include);
};
function geShoutout_Snapshot_Retake::onURL(%this, %url) {
    %cmd = firstWord(%url);
    if ((%cmd $= "RETAKE")) {
        shoutout_takeSnapshot();
    }
    error(getScopeName() @ " " @ "- unknown command" @ " " @ %cmd @ " " @ getTrace());
    return;
};
function shoutout_takeSnapshot() {
    %ctrlList = "";
    %ctrlList = BroadCastControlPanel @ %si.playGuiControlsToHide;
    %ctrlList @ " ";
    %ctrlList = geShoutOutWindow;
    %ctrlList @ " ";
    %ctrlList = ConsoleDlg;
    %ctrlList @ " ";
    %ctrlList = geTicker;
    %ctrlList @ " ";
    if ($UserPref::UI::ShoutOut::Show::Chat) {
    }
    %ctrlList = " " @ ConvBub;
    "";
    %si.snap_hiddenCtrlList = %ctrlList @ geShoutout_Snapshot;
    %ctrlList;
    BroadSnapshotButton_HideSnoop();
    if (!($UserPref::UI::ShoutOut::Show::Me)) {
        if (!($IN_ORBIT_CAM)) {
        }
        if (!($firstPerson)) {
            $player.MeshOff("*");
            $player.setShapeName("");
            if (isObject($player.hudCtrl)) {
                %ctrlList = %ctrlList @ " " @ $player.hudCtrl;
            }
            %ctrlList = ThePointsFloaterHud;
            %ctrlList @ " ";
        }
    }
    %ctrlList = trim(%ctrlList);
    hideABunchOfControls(%ctrlList);
    1.setVisible();
    $player.snap_regionCtrl = Canvas @ geShoutout_Snapshot;
    geVSideWatermark;
    $player.snap_fnBase = $DC::LocalAvatarFolder @ "/shoutout" @ geShoutout_Snapshot;
    $player.snap_fnExt = ".jpg" @ geShoutout_Snapshot;
    %cmd = "generic_takeSnapshotReally(geShoutout_Snapshot);";
    if (($Platform $= "windows")) {
    }
    if ((6.0 == $Platform::Version::Major)) {
        waitAFrameAndEval("waitAFrameAndEval(\"" @ %cmd @ "\");");
    }
    waitAFrameAndEval(%cmd);
};
function geShoutout_Snapshot::onSnapshotDone(%this, %unused) {
    0.setVisible();
    BroadSnapshotButton_ShowSnoop();
    restoreABunchOfControls($player.snap_hiddenCtrlList);
    if (!($UserPref::UI::ShoutOut::Show::Me)) {
        $player.setActiveSKUs($player.getActiveSKUs());
        $player.setShapeName($Player::Name);
    }
};
function shoutout_getCurrentMaxCharacters() {
    %max = $UserPref::UI::ShoutOut::Twitter::Include ? 140 : 200;
    if ($UserPref::UI::ShoutOut::Show::Pic) {
        %max = ((19.0 + strlen("(Sent from )")) - %max);
    }
    %max = (strlen("(Sent from" @ " " @ "http://" @ $Net::BaseDomain) @ ")" - %max);
    return %max;
};
function shoutout_setTickerInclude(%value) {
    shoutout_checkSendable();
};
function shoutout_setTwitterInclude(%value) {
    $player.maxLength = shoutout_getCurrentMaxCharacters() @ geShoutout_Snapshot_Message;
    geShoutout_Snapshot_Message.onKeystroke();
    if (%value) {
        300.schedule("setVisible", 1);
        0.setTrgPosition((getWord(geShoutout_Credentials_Twitter.getExtent(), 1) + getWord(geShoutout_Credentials_Twitter.getPosition(), 1)));
    }
    0.schedule("setVisible", 0);
    0.setTrgPosition((2.0 + getWord(geShoutout_Credentials_Twitter.getPosition(), 1)));
    shoutout_checkSendable();
};
function shoutout_setFBInclude(%value) {
    shoutout_checkSendable();
};
function shoutout_checkSendable() {
    %sendable = 0;
    %sendable = ($UserPref::UI::ShoutOut::Ticker::Include | %sendable);
    %sendable = ($UserPref::UI::ShoutOut::Twitter::Include | %sendable);
    %sendable = ($UserPref::UI::ShoutOut::FB::Include | %sendable);
    %sendable.setActive();
};
function geShoutout_Snapshot_Message::onKeystroke(%this) {
    %max = shoutout_getCurrentMaxCharacters();
    %used = strlen(%this.getText());
    %left = (%used - %max);
    %left.setTextWithStyle();
};
function geShoutout_Snapshot_Message::onEnter(%this) {
    geShoutout_Snapshot_SendButton.onClick();
};
function geShoutout_Snapshot_Message::onCtrlEnter(%this) {
    %this.onEnter();
};
function geShoutout_Credential_Twitter_Username_Save::onClick(%this) {
    %save = %this.getValue();
    $Player::Name.setProperty("twitter_un_save", %save);
    if (%save) {
    }
    $Player::Name.setProperty("twitter_un", "");
};
function geShoutout_Credential_Twitter_Password_Save::onClick(%this) {
    %save = %this.getValue();
    $Player::Name.setProperty("twitter_pw_save", %save);
    if (%save) {
    }
    $Player::Name.setProperty("twitter_pw", "");
};
function geShoutout_Snapshot_SendButton::onClick(%this) {
    if (!(%this.isActive())) {
        MessageBoxOK("No shoutout selected", "You need to choose at least one shoutout option");
        return;
    }
    $gShoutOut_PhotoURL = "";
    $gShoutOut_ShortPhotoURL = "";
    if ($UserPref::UI::ShoutOut::Ticker::Include) {
        MessageBoxYesNo("vSide Ticker", "Shouting out to the vSide ticker will display your message to everyone connected!<br>You'll be charged" @ " " @ shoutout_getPriceVBux_Ticker() @ " " @ "vBux.<br><br>Okay to pay " @ " " @ shoutout_getPriceVBux_Ticker() @ " " @ "vBux ?", "shoutOut_action_testPhase2();", "shoutOut_action_GroundState();", 1);
    }
    shoutOut_action_testPhase2();
};
function shoutOut_action_testPhase2() {
    shoutOut_action_testTwitterCredentialsIfNecessary();
};
function geShoutout_Snapshot_CancelButton::onClick(%this) {
    shoutOut_action_CleanupWithoutSend();
};
function shoutOut_action_testTwitterCredentialsIfNecessary() {
    shoutOut_SetStatus("Checking..", geShoutout_Avatar_Twitter.getBitmap());
    if (!(geShoutout_Twitter_Include.getValue())) {
        shoutOut_action_testFBCredentialsIfNecessary();
    }
    shoutOut_action_testTwitterCredentials(0);
};
function shoutOut_action_testTwitterCredentials(%oneShot) {
    if ((geShoutout_Credential_Twitter_Username.getText() $= "")) {
    }
    if ((geShoutout_Credential_Twitter_Password.getText() $= "")) {
        "".setBitmap();
        $player.tooltip = "" @ geShoutout_Avatar_Twitter;
        geShoutout_Avatar_Twitter;
        if (%oneShot) {
            shoutOut_action_GroundState();
        }
        MessageBoxOK("Need your Twitter 411", "Please enter your Twitter username and password..", "shoutOut_action_groundState();");
        return;
    }
    %user = geShoutout_Credential_Twitter_Username.getText();
    %pass = geShoutout_Credential_Twitter_Password.getText();
    %request = sendRequest_Twitter_verify_credentials(%user, %pass, "onDoneOrErrorCallback_Twitter_verify_credentials");
    %request.oneShot = %oneShot;
};
function onDoneOrErrorCallback_Twitter_verify_credentials(%request) {
    %xmlDoc = new ""();;
    XMLDoc;
    %xmlDoc.parseXML(%request.getResults());
    %xmlRoot = %xmlDoc.getRootElement();
    0;
    if (isObject(%xmlRoot)) {
    }
    %succ = (%xmlRoot.getValue() $= "user");
    "platform/client/ui/external_portrait_unknown".setBitmap();
    %request.tooltip = "problem accessing your twitter account" @ geShoutout_Avatar_Twitter;
    geShoutout_Avatar_Twitter;
    if (!(%succ)) {
        if (!(%request.oneShot)) {
            MessageBoxOK("Problem with twitter", "Your twitter username or password may be wrong.", "");
            shoutOut_action_GroundState();
        }
    }
    if (geShoutout_Credential_Twitter_Username_Save.getValue()) {
        $Player::Name.setProperty("twitter_un", geShoutout_Credential_Twitter_Username.getText());
    }
    if (geShoutout_Credential_Twitter_Password_Save.getValue()) {
        $Player::Name.setProperty("twitter_pw", geShoutout_Credential_Twitter_Password.getText());
    }
    %profile_image_url = %xmlRoot.getFirstChild("profile_image_url").getText();
    gUserPropMgrClient;
    if (!(gUserPropMgrClient @ " " @ %profile_image_url $= "")) {
        %profile_image_url.downloadAndApplyBitmap();
        %request.tooltip = "twitter account verified" @ geShoutout_Avatar_Twitter;
        geShoutout_Avatar_Twitter;
    }
    if (!(%request.oneShot)) {
        shoutOut_action_testFBCredentialsIfNecessary();
    }
    %xmlDoc.delete();
};
function shoutOut_action_testFBCredentialsIfNecessary() {
    shoutOut_action_sendPhase1();
};
function shoutOut_action_sendPhase1() {
    shoutOut_action_savePhotoIfNecessary();
};
function shoutOut_action_savePhotoIfNecessary() {
    if (!($UserPref::UI::ShoutOut::Show::Pic)) {
        shoutOut_action_sendPhase2();
        return;
    }
    shoutOut_SetStatus("Uploading..");
    %fileName = geShoutout_Snapshot @ %request.snap_fnExt;
    %request.snap_fnBase;
    %caption = geShoutout_Snapshot_Message.getText();
    geShoutout_Snapshot;
    %peopleInViewList = "";
    error(getScopeName() @ " " @ "- todo: determine people in view");
    %type = "screenshot";
    %location = $player.getTransform();
    sendRequest_UploadPhoto(%fileName, %caption, %peopleInViewList, %type, %location, 0, "onDoneOrErrorCallback_UploadPhoto_ShoutOut");
};
function onDoneOrErrorCallback_UploadPhoto_ShoutOut(%request) {
    if (!(%request.checkSuccess())) {
        MessageBoxOK("Uh Oh", "There was some problem saving your screenshot.<br><br>Try deleting some from <a:" @ $Net::PhotoAlbumURL @ ">your photo album</a>,<br>or try without a screenshot.", "");
        shoutOut_action_GroundState();
        return;
    }
    $gShoutOut_PhotoURL = %request.getResult("photoURL");
    %s = strreplace($gShoutOut_PhotoURL, "/", "\t");
    %id = getField(%s, 4);
    $gShoutOut_PhotoURL = $Net::PhotoPageURL @ %id;
    shoutOut_action_shortenPhotoURL();
};
function shoutOut_action_shortenPhotoURL() {
    shoutOut_SetStatus("Shortening..");
    %request = sendRequest_Bitly_shorten($gShoutOut_PhotoURL, "onDoneOrErrorCallback_Bitly_shorten");
    %request.photoURL = $gShoutOut_PhotoURL;
};
function onDoneOrErrorCallback_Bitly_shorten(%request) {
    %xmlDoc = new ""();;
    XMLDoc;
    %xmlDoc.parseXML(%request.getResults());
    %xmlRoot = %xmlDoc.getRootElement();
    0;
    if (!(%xmlRoot.getValue() $= "bitly")) {
        error(getScopeName() @ " " @ "- bad root." @ " " @ %request.getURL() @ " " @ %request.getResults());
        shoutOut_action_shortenPhotoURLFailed();
        return;
    }
    if (!(%xmlRoot.getFirstChild("errorCode").getText() $= 0)) {
        error(getScopeName() @ " " @ "- some error." @ " " @ %request.getURL() @ " " @ %request.getResults());
        shoutOut_action_shortenPhotoURLFailed();
        return;
    }
    %xmlNode = %xmlRoot.getFirstChild("results");
    %xmlNode = %xmlNode.getFirstChild("nodeKeyVal");
    %xmlNode = %xmlNode.getFirstChild("shortUrl");
    if (!(isObject(%xmlNode))) {
        error(getScopeName() @ " " @ "- can't find results." @ " " @ %request.getURL() @ " " @ %request.getResults());
        shoutOut_action_shortenPhotoURLFailed();
        return;
    }
    $gShoutOut_ShortPhotoURL = %xmlNode.getText();
    shoutOut_action_sendPhase2(%request.photoURL, $gShoutOut_ShortPhotoURL);
    %xmlDoc.delete();
};
function shoutOut_action_shortenPhotoURLFailed() {
    MessageBoxOK("Uh Oh", "There was some problem linking to your screenshot.<br><br>Try again in a little bit,<br>or try without a screenshot.", "");
    shoutOut_action_GroundState();
};
function shoutOut_action_sendPhase2() {
    if ($UserPref::UI::ShoutOut::FB::Include) {
    }
    if (($gShoutOut_PhotoURL $= "")) {
        MessageBoxOK("sorry, i forgot to mention..", "To share on Facebook you have to use a snapshot.<br>This will be fixed in a future release,<br>but for now Try again!", "schedule(500, 0, \"shoutOut_action_forceSnapshot\");");
        shoutOut_action_GroundState();
        return;
    }
    if ($UserPref::UI::ShoutOut::Ticker::Include) {
        shoutOut_action_sendTicker();
    }
    shoutOut_action_sendPhase3();
};
function shoutOut_action_sendTicker() {
    shoutOut_SetStatus("Ticking..", geTGF_profilePic.getBitmap());
    %messageText = geShoutout_Snapshot_Message.getText();
    if (!($gShoutOut_ShortPhotoURL $= "")) {
        %link = " " @ $gShoutOut_ShortPhotoURL;
    }
    %link = "";
    %messageText = %messageText @ %link;
    %priority = 2;
    %request = sendRequest_PublishToTicker(%messageText, %priority, "onDoneOrErrorCallback_PublishToTicker");
    %request.messageText = %messageText;
    %analytic = getAnalytic();
    %withPic = ($gShoutOut_ShortPhotoURL $= "") ? "" : "/photo";
    %analytic.trackPageView("/client/shoutout/ticker" @ %withPic);
};
function onDoneOrErrorCallback_PublishToTicker(%request) {
    if (!(%request.checkSuccess())) {
        MessageBoxOK("Uh Oh", "There was some problem sending to the ticker. You haven't been charged.", "");
        shoutOut_action_GroundState();
        return;
    }
    if ($StandAlone) {
        commandToServer('fakeTicker', %request.messageText);
    }
    shoutOut_action_sendPhase3();
};
function shoutOut_action_sendPhase3() {
    if ($UserPref::UI::ShoutOut::FB::Include) {
        shoutOut_action_sendFB();
    }
    if ($UserPref::UI::ShoutOut::Twitter::Include) {
        shoutOut_SetStatus("Tweeting..", geShoutout_Avatar_Twitter.getBitmap());
        shoutOut_action_sendTweet();
    }
    shoutOut_action_CleanupWithSend();
};
function shoutOut_action_sendFB() {
    %sharerURL = "http://www.facebook.com/sharer.php";
    %sharerURL = %sharerURL @ "?u=" @ urlEncode($gShoutOut_PhotoURL);
    gotoWebPage(%sharerURL);
    %analytic = getAnalytic();
    %withPic = ($gShoutOut_ShortPhotoURL $= "") ? "" : "/photo";
    %analytic.trackPageView("/client/shoutout/facebook" @ %withPic);
};
function shoutOut_action_sendTweet() {
    %tweetText = geShoutout_Snapshot_Message.getText();
    if (!($gShoutOut_ShortPhotoURL $= "")) {
        %link = "(Sent from vSide:" @ " " @ $gShoutOut_ShortPhotoURL @ ")";
    }
    %link = "(Sent from vSide:" @ " " @ "http://" @ $Net::BaseDomain @ ")";
    %tweetText = %tweetText @ " " @ %link;
    %user = geShoutout_Credential_Twitter_Username.getText();
    %pass = geShoutout_Credential_Twitter_Password.getText();
    %request = sendRequest_Twitter_statuses_update(%user, %pass, %tweetText, "onDoneOrErrorCallback_Twitter_statuses_update");
    %analytic = getAnalytic();
    %withPic = ($gShoutOut_ShortPhotoURL $= "") ? "" : "/photo";
    %analytic.trackPageView("/client/shoutout/twitter" @ %withPic);
};
function onDoneOrErrorCallback_Twitter_statuses_update(%request) {
    %xmlDoc = new ""();;
    XMLDoc;
    %xmlDoc.parseXML(%request.getResults());
    %xmlRoot = %xmlDoc.getRootElement();
    0;
    if (isObject(%xmlRoot)) {
    }
    %succ = (%xmlRoot.getValue() $= "status");
    if (%succ) {
        %tweetID = %xmlRoot.getFirstChild("id").getText();
        if ((%tweetID $= "")) {
            error(getScopeName() @ " " @ "- no tweet ID." @ " " @ %request.getResults());
            %msg = "<br>sent!";
        }
        %tweetURL = $Net::TwitterBaseInsecure @ "/" @ geShoutout_Credential_Twitter_Username.getText() @ "/status/" @ %tweetID;
        %msg = "<br><b><linkcolor:ffffffff><linkcolorhl:ffaaffdd><a:gamelink:" @ %tweetURL @ ">click here to view your tweet.</a><br>";
        MessageBoxOK("Success!", %msg, "", 1, "success" @ getScopeName());
        shoutOut_action_GroundState();
        shoutOut_action_CleanupWithSend();
    }
    shoutOut_SetStatus("");
    if (isObject(%xmlRoot)) {
    }
    %twitterErr = "(could not connect)";
    %xmlRoot.getFirstChild("error").getText();
    %yesCmd = "shoutOut_action_sendTweet();";
    %noCmd = "";
    MessageBoxYesNo("Problem with twitter", "Hmm, something went wrong.<br>Twitter says: \"" @ %twitterErr @ "\"<br>Would you like to re-send ?", %yesCmd, %noCmd);
    shoutOut_action_GroundState();
    %xmlDoc.delete();
};
function shoutOut_action_forceSnapshot() {
    1.setValue();
    shoutout_setIncludeSnapshot(1);
    shoutout_takeSnapshot();
};
function shoutOut_SetStatus(%status, %avatarBitmap) {
    isDefined("%avatarBitmap", "");
    if ((%status $= "")) {
        0.setVisible();
        return geShoutOut_Sending_Container;
    }
    1.setVisible();
    geShoutOut_Sending_Panel.alignToCenterXY();
    %status.setTextWithStyle();
    %avatarBitmap.setBitmap();
    !(geShoutout_Sending_Avatar_Container @ " " @ %avatarBitmap $= "").setVisible();
};
function shoutOut_action_GroundState() {
    shoutOut_SetStatus("");
};
function shoutOut_action_CleanupWithSend() {
    "".setText();
    geShoutOutWindow.close();
};
function shoutOut_action_CleanupWithoutSend() {
    geShoutOutWindow.close();
};
