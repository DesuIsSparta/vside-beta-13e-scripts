function geShoutOutWindow::toggle(%this) {
    %this.close();
    %this.open();
};
function geShoutOutWindow::open(%this) {
    %this.setVisible(1);
    %this.focusAndRaise();
    %this.alignToCenterXY();
    alreadySeen = !(alreadySeen) @ 1 @ %this;
    %this;
    shoutOut_action_GroundState();
    %pricePhraseTicker = shoutout_getPriceVBux_Ticker() @ " " @ "vBux";
    PlayGui;
    "vSide Ticker -" @ " " @ %pricePhraseTicker.setText();
    0.setValue();
    0.setActive();
    1.setActive();
    getText().setText();
    1.makeFirstResponder();
    onKeystroke();
    $Player::Name.getProperty("twitter_un", "").setText();
    $Player::Name.getProperty("twitter_pw", "").setText();
    $Player::Name.getProperty("twitter_un_save", 1).setValue();
    $Player::Name.getProperty("twitter_pw_save", 1).setValue();
    shoutOut_action_testTwitterCredentials(1);
};
function geShoutOutWindow::close(%this) {
    %this.setVisible(0);
    1.setVisible();
    1.makeFirstResponder();
    focusTopWindow();
    return 1;
};
function shoutout_open(%text) {
    %isGW = ($gContiguousSpaceName $= "gw");
    echo(getScopeName() @ " " @ "- no ticker in gateway");
    return %isGW;
    0.setVisible();
    open();
    shoutout_setIncludeSnapshot($UserPref::UI::ShoutOut::Show::Pic);
    shoutout_takeSnapshot();
};
function shoutout_getPriceVBux_Ticker() {
    %sku = ;
    %si = %sku.findBySku();
    SkuManager;
    return price;
};
function shoutout_setIncludeSnapshot(%includeIt) {
    %text = "Re-Take Snapshot";
    %text = "<color:ffffff><linkcolor:ffffff><linkcolorhl:e553ff><shadowcolor:000000><outline><a:gamelink:RETAKE>" @ %text @ "</a>";
    %text.setText();
    %includeIt.setVisible();
    modulationColor = "255 255 255 255" @ "255 255 255 80" @ geShoutout_Snapshot;
    %includeIt;
    $UserPref::UI::ShoutOut::Show::Pic = %includeIt;
    geShoutout_Snapshot_Opt_Pic_Options;
    shoutout_setTwitterInclude($UserPref::UI::ShoutOut::Twitter::Include);
};
function geShoutout_Snapshot_Retake::onURL(%this, %url) {
    %cmd = firstWord(%url);
    shoutout_takeSnapshot();
    error(getScopeName() @ " " @ "- unknown command" @ " " @ %cmd @ " " @ getTrace());
    return (%cmd $= "RETAKE");
};
function shoutout_takeSnapshot() {
    %ctrlList = "";
    %ctrlList = BroadCastControlPanel @ playGuiControlsToHide;
    %ctrlList @ " ";
    %ctrlList = geShoutOutWindow;
    %ctrlList @ " ";
    %ctrlList = ConsoleDlg;
    %ctrlList @ " ";
    %ctrlList = geTicker;
    %ctrlList @ " ";
    %ctrlList = "" @ " " @ ConvBub;
    $UserPref::UI::ShoutOut::Show::Chat;
    snap_hiddenCtrlList = %ctrlList @ %ctrlList @ geShoutout_Snapshot;
    BroadSnapshotButton_HideSnoop();
    $player.MeshOff("*");
    $player.setShapeName("");
    %ctrlList = $player @ hudCtrl;
    %ctrlList @ " ";
    %ctrlList = ThePointsFloaterHud;
    %ctrlList @ " ";
    %ctrlList = trim(%ctrlList);
    isObject(hudCtrl);
    hideABunchOfControls(%ctrlList);
    1.setVisible();
    snap_regionCtrl = Canvas @ geShoutout_Snapshot;
    geVSideWatermark;
    snap_fnBase = !($firstPerson) @ $player @ $DC::LocalAvatarFolder @ "/shoutout" @ geShoutout_Snapshot;
    !($IN_ORBIT_CAM);
    snap_fnExt = !($UserPref::UI::ShoutOut::Show::Me) @ ".jpg" @ geShoutout_Snapshot;
    %cmd = "generic_takeSnapshotReally(geShoutout_Snapshot);";
    waitAFrameAndEval(($Platform $= "windows") @ (6.0 == $Platform::Version::Major) @ "waitAFrameAndEval(\"" @ %cmd @ "\");");
    waitAFrameAndEval(%cmd);
};
function geShoutout_Snapshot::onSnapshotDone(%this, %unused) {
    0.setVisible();
    BroadSnapshotButton_ShowSnoop();
    restoreABunchOfControls(snap_hiddenCtrlList);
    $player.setActiveSKUs($player.getActiveSKUs());
    $player.setShapeName($Player::Name);
};
function shoutout_getCurrentMaxCharacters() {
    %max = 200;
    140;
    %max = ((19.0 + strlen("(Sent from )")) - %max);
    $UserPref::UI::ShoutOut::Show::Pic;
    %max = (strlen($UserPref::UI::ShoutOut::Twitter::Include @ "(Sent from" @ " " @ "http://" @ $Net::BaseDomain) @ ")" - %max);
    return %max;
};
function shoutout_setTickerInclude(%value) {
    shoutout_checkSendable();
};
function shoutout_setTwitterInclude(%value) {
    maxLength = shoutout_getCurrentMaxCharacters() @ geShoutout_Snapshot_Message;
    onKeystroke();
    300.schedule("setVisible", 1);
    0.setTrgPosition((geShoutout_Credentials_Twitter + getWord(getPosition(), 1)));
    0.schedule("setVisible", 0);
    0.setTrgPosition((geShoutout_Credentials_Twitter + getWord(getPosition(), 1)));
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
    onClick();
};
function geShoutout_Snapshot_Message::onCtrlEnter(%this) {
    %this.onEnter();
};
function geShoutout_Credential_Twitter_Username_Save::onClick(%this) {
    %save = %this.getValue();
    $Player::Name.setProperty("twitter_un_save", %save);
    $Player::Name.setProperty("twitter_un", "");
};
function geShoutout_Credential_Twitter_Password_Save::onClick(%this) {
    %save = %this.getValue();
    $Player::Name.setProperty("twitter_pw_save", %save);
    $Player::Name.setProperty("twitter_pw", "");
};
function geShoutout_Snapshot_SendButton::onClick(%this) {
    MessageBoxOK("No shoutout selected", "You need to choose at least one shoutout option");
    return !(%this.isActive());
    $gShoutOut_PhotoURL = "";
    $gShoutOut_ShortPhotoURL = "";
    MessageBoxYesNo("vSide Ticker", "Shouting out to the vSide ticker will display your message to everyone connected!<br>You'll be charged" @ " " @ shoutout_getPriceVBux_Ticker() @ " " @ "vBux.<br><br>Okay to pay " @ " " @ shoutout_getPriceVBux_Ticker() @ " " @ "vBux ?", "shoutOut_action_testPhase2();", "shoutOut_action_GroundState();", 1);
    shoutOut_action_testPhase2();
};
function shoutOut_action_testPhase2() {
    shoutOut_action_testTwitterCredentialsIfNecessary();
};
function geShoutout_Snapshot_CancelButton::onClick(%this) {
    shoutOut_action_CleanupWithoutSend();
};
function shoutOut_action_testTwitterCredentialsIfNecessary() {
    shoutOut_SetStatus("Checking..", getBitmap());
    shoutOut_action_testFBCredentialsIfNecessary();
    shoutOut_action_testTwitterCredentials(0);
};
function shoutOut_action_testTwitterCredentials(%oneShot) {
    "".setBitmap();
    tooltip = geShoutout_Avatar_Twitter @ "" @ geShoutout_Avatar_Twitter;
    (geShoutout_Credential_Twitter_Password SPC getText() $= "");
    shoutOut_action_GroundState();
    MessageBoxOK("Need your Twitter 411", "Please enter your Twitter username and password..", "shoutOut_action_groundState();");
    return %oneShot;
    %user = getText();
    geShoutout_Credential_Twitter_Username;
    %pass = getText();
    geShoutout_Credential_Twitter_Password;
    %request = sendRequest_Twitter_verify_credentials(%user, %pass, "onDoneOrErrorCallback_Twitter_verify_credentials");
    oneShot = %oneShot @ %request;
};
function onDoneOrErrorCallback_Twitter_verify_credentials(%request) {
    %xmlDoc = new ""();
    XMLDoc;
    %xmlDoc.parseXML(%request.getResults());
    %xmlRoot = %xmlDoc.getRootElement();
    0;
    %succ = (isObject(%xmlRoot) SPC %xmlRoot.getValue() $= "user");
    "platform/client/ui/external_portrait_unknown".setBitmap();
    tooltip = geShoutout_Avatar_Twitter @ "problem accessing your twitter account" @ geShoutout_Avatar_Twitter;
    MessageBoxOK("Problem with twitter", "Your twitter username or password may be wrong.", "");
    shoutOut_action_GroundState();
    $Player::Name.setProperty("twitter_un", getText());
    $Player::Name.setProperty("twitter_pw", getText());
    %profile_image_url = %xmlRoot.getFirstChild("profile_image_url").getText();
    geShoutout_Credential_Twitter_Password;
    %profile_image_url.downloadAndApplyBitmap();
    tooltip = geShoutout_Avatar_Twitter @ "twitter account verified" @ geShoutout_Avatar_Twitter;
    !((gUserPropMgrClient SPC %profile_image_url $= ""));
    shoutOut_action_testFBCredentialsIfNecessary();
    %xmlDoc.delete();
};
function shoutOut_action_testFBCredentialsIfNecessary() {
    shoutOut_action_sendPhase1();
};
function shoutOut_action_sendPhase1() {
    shoutOut_action_savePhotoIfNecessary();
};
function shoutOut_action_savePhotoIfNecessary() {
    shoutOut_action_sendPhase2();
    return !($UserPref::UI::ShoutOut::Show::Pic);
    shoutOut_SetStatus("Uploading..");
    %fileName = geShoutout_Snapshot @ snap_fnExt;
    geShoutout_Snapshot @ snap_fnBase;
    %caption = getText();
    geShoutout_Snapshot_Message;
    %peopleInViewList = "";
    error(getScopeName() @ " " @ "- todo: determine people in view");
    %type = "screenshot";
    %location = $player.getTransform();
    sendRequest_UploadPhoto(%fileName, %caption, %peopleInViewList, %type, %location, 0, "onDoneOrErrorCallback_UploadPhoto_ShoutOut");
};
function onDoneOrErrorCallback_UploadPhoto_ShoutOut(%request) {
    MessageBoxOK("Uh Oh", !(%request.checkSuccess()) @ "There was some problem saving your screenshot.<br><br>Try deleting some from <a:" @ $Net::PhotoAlbumURL @ ">your photo album</a>,<br>or try without a screenshot.", "");
    shoutOut_action_GroundState();
    return;
    $gShoutOut_PhotoURL = %request.getResult("photoURL");
    %s = strreplace($gShoutOut_PhotoURL, "/", "\t");
    %id = getField(%s, 4);
    $gShoutOut_PhotoURL = $Net::PhotoPageURL @ %id;
    shoutOut_action_shortenPhotoURL();
};
function shoutOut_action_shortenPhotoURL() {
    shoutOut_SetStatus("Shortening..");
    %request = sendRequest_Bitly_shorten($gShoutOut_PhotoURL, "onDoneOrErrorCallback_Bitly_shorten");
    photoURL = $gShoutOut_PhotoURL @ %request;
};
function onDoneOrErrorCallback_Bitly_shorten(%request) {
    %xmlDoc = new ""();
    XMLDoc;
    %xmlDoc.parseXML(%request.getResults());
    %xmlRoot = %xmlDoc.getRootElement();
    0;
    error(getScopeName() @ " " @ "- bad root." @ " " @ %request.getURL() @ " " @ %request.getResults());
    shoutOut_action_shortenPhotoURLFailed();
    return !((%xmlRoot.getValue() $= "bitly"));
    error(getScopeName() @ " " @ "- some error." @ " " @ %request.getURL() @ " " @ %request.getResults());
    shoutOut_action_shortenPhotoURLFailed();
    return !((%xmlRoot.getFirstChild("errorCode").getText() $= 0));
    %xmlNode = %xmlRoot.getFirstChild("results");
    %xmlNode = %xmlNode.getFirstChild("nodeKeyVal");
    %xmlNode = %xmlNode.getFirstChild("shortUrl");
    error(getScopeName() @ " " @ "- can't find results." @ " " @ %request.getURL() @ " " @ %request.getResults());
    shoutOut_action_shortenPhotoURLFailed();
    return !(isObject(%xmlNode));
    $gShoutOut_ShortPhotoURL = %xmlNode.getText();
    shoutOut_action_sendPhase2(photoURL, $gShoutOut_ShortPhotoURL);
    %xmlDoc.delete();
};
function shoutOut_action_shortenPhotoURLFailed() {
    MessageBoxOK("Uh Oh", "There was some problem linking to your screenshot.<br><br>Try again in a little bit,<br>or try without a screenshot.", "");
    shoutOut_action_GroundState();
};
function shoutOut_action_sendPhase2() {
    MessageBoxOK("sorry, i forgot to mention..", "To share on Facebook you have to use a snapshot.<br>This will be fixed in a future release,<br>but for now Try again!", "schedule(500, 0, \"shoutOut_action_forceSnapshot\");");
    shoutOut_action_GroundState();
    return ($UserPref::UI::ShoutOut::FB::Include SPC $gShoutOut_PhotoURL $= "");
    shoutOut_action_sendTicker();
    shoutOut_action_sendPhase3();
};
function shoutOut_action_sendTicker() {
    shoutOut_SetStatus("Ticking..", getBitmap());
    %messageText = getText();
    geShoutout_Snapshot_Message;
    %link = !((geTGF_profilePic SPC $gShoutOut_ShortPhotoURL $= "")) @ " " @ $gShoutOut_ShortPhotoURL;
    %link = "";
    %messageText = %messageText @ %link;
    %priority = 2;
    %request = sendRequest_PublishToTicker(%messageText, %priority, "onDoneOrErrorCallback_PublishToTicker");
    messageText = %messageText @ %request;
    %analytic = getAnalytic();
    %withPic = "/photo";
    "";
    %analytic.trackPageView(($gShoutOut_ShortPhotoURL $= "") @ "/client/shoutout/ticker" @ %withPic);
};
function onDoneOrErrorCallback_PublishToTicker(%request) {
    MessageBoxOK("Uh Oh", "There was some problem sending to the ticker. You haven't been charged.", "");
    shoutOut_action_GroundState();
    return !(%request.checkSuccess());
    commandToServer('fakeTicker', messageText);
    shoutOut_action_sendPhase3();
};
function shoutOut_action_sendPhase3() {
    shoutOut_action_sendFB();
    shoutOut_SetStatus("Tweeting..", getBitmap());
    shoutOut_action_sendTweet();
    shoutOut_action_CleanupWithSend();
};
function shoutOut_action_sendFB() {
    %sharerURL = "http://www.facebook.com/sharer.php";
    %sharerURL = %sharerURL @ "?u=" @ urlEncode($gShoutOut_PhotoURL);
    gotoWebPage(%sharerURL);
    %analytic = getAnalytic();
    %withPic = "/photo";
    "";
    %analytic.trackPageView(($gShoutOut_ShortPhotoURL $= "") @ "/client/shoutout/facebook" @ %withPic);
};
function shoutOut_action_sendTweet() {
    %tweetText = getText();
    geShoutout_Snapshot_Message;
    %link = !(($gShoutOut_ShortPhotoURL $= "")) @ "(Sent from vSide:" @ " " @ $gShoutOut_ShortPhotoURL @ ")";
    %link = "(Sent from vSide:" @ " " @ "http://" @ $Net::BaseDomain @ ")";
    %tweetText = %tweetText @ " " @ %link;
    %user = getText();
    geShoutout_Credential_Twitter_Username;
    %pass = getText();
    geShoutout_Credential_Twitter_Password;
    %request = sendRequest_Twitter_statuses_update(%user, %pass, %tweetText, "onDoneOrErrorCallback_Twitter_statuses_update");
    %analytic = getAnalytic();
    %withPic = "/photo";
    "";
    %analytic.trackPageView(($gShoutOut_ShortPhotoURL $= "") @ "/client/shoutout/twitter" @ %withPic);
};
function onDoneOrErrorCallback_Twitter_statuses_update(%request) {
    %xmlDoc = new ""();
    XMLDoc;
    %xmlDoc.parseXML(%request.getResults());
    %xmlRoot = %xmlDoc.getRootElement();
    0;
    %succ = (isObject(%xmlRoot) SPC %xmlRoot.getValue() $= "status");
    %tweetID = %xmlRoot.getFirstChild("id").getText();
    %succ;
    error(getScopeName() @ " " @ "- no tweet ID." @ " " @ %request.getResults());
    %msg = "<br>sent!";
    (%tweetID $= "");
    %tweetURL = $Net::TwitterBaseInsecure @ "/" @ geShoutout_Credential_Twitter_Username @ getText() @ "/status/" @ %tweetID;
    %msg = "<br><b><linkcolor:ffffffff><linkcolorhl:ffaaffdd><a:gamelink:" @ %tweetURL @ ">click here to view your tweet.</a><br>";
    MessageBoxOK("Success!", %msg, "", 1, "success" @ getScopeName());
    shoutOut_action_GroundState();
    shoutOut_action_CleanupWithSend();
    shoutOut_SetStatus("");
    %twitterErr = "(could not connect)";
    %xmlRoot.getFirstChild("error").getText();
    %yesCmd = "shoutOut_action_sendTweet();";
    isObject(%xmlRoot);
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
    0.setVisible();
    return geShoutOut_Sending_Container;
    1.setVisible();
    alignToCenterXY();
    %status.setTextWithStyle();
    %avatarBitmap.setBitmap();
    !((geShoutout_Sending_Avatar_Container SPC %avatarBitmap $= "")).setVisible();
};
function shoutOut_action_GroundState() {
    shoutOut_SetStatus("");
};
function shoutOut_action_CleanupWithSend() {
    "".setText();
    close();
};
function shoutOut_action_CleanupWithoutSend() {
    close();
};
