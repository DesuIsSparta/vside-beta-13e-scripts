function geShoutOutWindow::toggle(%this)
{
    if (%this.isVisible())
    {
        %this.close();
    }
    else
    {
        %this.open();
    }
    return ;
}
function geShoutOutWindow::open(%this)
{
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    if (!%this.alreadySeen)
    {
        %this.alignToCenterXY();
        %this.alreadySeen = 1;
    }
    shoutOut_action_GroundState();
    %pricePhraseTicker = shoutout_getPriceVBux_Ticker() SPC "vBux";
    geShoutout_Ticker_Include.setText("vSide Ticker -" SPC %pricePhraseTicker);
    if ($Player::VBux < shoutout_getPriceVBux_Ticker())
    {
        geShoutout_Ticker_Include.setValue(0);
        geShoutout_Ticker_Include.setActive(0);
    }
    else
    {
        geShoutout_Ticker_Include.setActive(1);
    }
    geShoutout_Snapshot_Message.setText(MessageHudEdit.getText());
    geShoutout_Snapshot_Message.makeFirstResponder(1);
    geShoutout_Snapshot_Message.onKeystroke();
    if (geShoutout_Credential_Twitter_Username.getText() $= "")
    {
        geShoutout_Credential_Twitter_Username.setText(gUserPropMgrClient.getProperty($Player::Name, "twitter_un", ""));
    }
    if (geShoutout_Credential_Twitter_Password.getText() $= "")
    {
        geShoutout_Credential_Twitter_Password.setText(gUserPropMgrClient.getProperty($Player::Name, "twitter_pw", ""));
    }
    geShoutout_Credential_Twitter_Username_Save.setValue(gUserPropMgrClient.getProperty($Player::Name, "twitter_un_save", 1));
    geShoutout_Credential_Twitter_Password_Save.setValue(gUserPropMgrClient.getProperty($Player::Name, "twitter_pw_save", 1));
    shoutOut_action_testTwitterCredentials(1);
    return ;
}
function geShoutOutWindow::close(%this)
{
    %this.setVisible(0);
    if (!(MessageHudEdit.getText() $= ""))
    {
        MessageHud.setVisible(1);
        MessageHudEdit.makeFirstResponder(1);
    }
    PlayGui.focusTopWindow();
    return 1;
}
function shoutout_open(%text)
{
    %isGW = $gContiguousSpaceName $= "gw";
    if (%isGW)
    {
        echo(getScopeName() SPC "- no ticker in gateway");
        return ;
    }
    MessageHud.setVisible(0);
    geShoutOutWindow.open();
    shoutout_setIncludeSnapshot($UserPref::UI::ShoutOut::Show::Pic);
    shoutout_takeSnapshot();
    return ;
}
function shoutout_getPriceVBux_Ticker()
{
    %sku = $specialSKUs["tickerPri2"];
    %si = SkuManager.findBySku(%sku);
    return %si.price;
}
function shoutout_setIncludeSnapshot(%includeIt)
{
    %text = "Re-Take Snapshot";
    %text = "<color:ffffff><linkcolor:ffffff><linkcolorhl:e553ff><shadowcolor:000000><outline><a:gamelink:RETAKE>" @ %text @ "</a>";
    geShoutout_Snapshot_Retake.setText(%text);
    geShoutout_Snapshot_Opt_Pic_Options.setVisible(%includeIt);
    geShoutout_Snapshot.modulationColor = %includeIt ? "255 255 255 255" : "255 255 255 80";
    $UserPref::UI::ShoutOut::Show::Pic = %includeIt;
    shoutout_setTwitterInclude($UserPref::UI::ShoutOut::Twitter::Include);
    return ;
}
function geShoutout_Snapshot_Retake::onURL(%this, %url)
{
    %cmd = firstWord(%url);
    if (%cmd $= "RETAKE")
    {
        shoutout_takeSnapshot();
    }
    else
    {
        error(getScopeName() SPC "- unknown command" SPC %cmd SPC getTrace());
        return ;
    }
    return ;
}
function shoutout_takeSnapshot()
{
    %ctrlList = "";
    %ctrlList = %ctrlList SPC BroadCastControlPanel.playGuiControlsToHide;
    %ctrlList = %ctrlList SPC geShoutOutWindow;
    %ctrlList = %ctrlList SPC ConsoleDlg;
    %ctrlList = %ctrlList SPC geTicker;
    %ctrlList = %ctrlList @ $UserPref::UI::ShoutOut::Show::Chat ? "" : " ";
    geShoutout_Snapshot.snap_hiddenCtrlList = %ctrlList;
    BroadSnapshotButton_HideSnoop();
    if (!$UserPref::UI::ShoutOut::Show::Me)
    {
        if (!$IN_ORBIT_CAM && !$firstPerson)
        {
            $player.MeshOff("*");
            $player.setShapeName("");
            if (isObject($player.hudCtrl))
            {
                %ctrlList = %ctrlList SPC $player.hudCtrl;
            }
            %ctrlList = %ctrlList SPC ThePointsFloaterHud;
        }
    }
    %ctrlList = trim(%ctrlList);
    hideABunchOfControls(%ctrlList);
    geVSideWatermark.setVisible(1);
    geShoutout_Snapshot.snap_regionCtrl = Canvas;
    geShoutout_Snapshot.snap_fnBase = $DC::LocalAvatarFolder @ "/shoutout";
    geShoutout_Snapshot.snap_fnExt = ".jpg";
    %cmd = "generic_takeSnapshotReally(geShoutout_Snapshot);";
    if (($Platform $= "windows") && ($Platform::Version::Major == 6))
    {
        waitAFrameAndEval("waitAFrameAndEval(\"" @ %cmd @ "\");");
    }
    else
    {
        waitAFrameAndEval(%cmd);
    }
    return ;
}
function geShoutout_Snapshot::onSnapshotDone(%this, %unused)
{
    geVSideWatermark.setVisible(0);
    BroadSnapshotButton_ShowSnoop();
    restoreABunchOfControls(geShoutout_Snapshot.snap_hiddenCtrlList);
    if (!$UserPref::UI::ShoutOut::Show::Me)
    {
        $player.setActiveSKUs($player.getActiveSKUs());
        $player.setShapeName($Player::Name);
    }
    return ;
}
function shoutout_getCurrentMaxCharacters()
{
    %max = $UserPref::UI::ShoutOut::Twitter::Include ? 140 : 200;
    if ($UserPref::UI::ShoutOut::Show::Pic)
    {
        %max = %max - (strlen("(Sent from )") + 19);
    }
    else
    {
        %max = %max - (strlen("(Sent from" SPC "http://" @ $Net::BaseDomain) @ ")");
    }
    return %max;
}
function shoutout_setTickerInclude(%value)
{
    shoutout_checkSendable();
    return ;
}
function shoutout_setTwitterInclude(%value)
{
    geShoutout_Snapshot_Message.maxLength = shoutout_getCurrentMaxCharacters();
    geShoutout_Snapshot_Message.onKeystroke();
    if (%value)
    {
        geShoutout_Credentials_Twitter.schedule(300, "setVisible", 1);
        geShoutout_Container_BelowTwitter.setTrgPosition(0, getWord(geShoutout_Credentials_Twitter.getPosition(), 1) + getWord(geShoutout_Credentials_Twitter.getExtent(), 1));
    }
    else
    {
        geShoutout_Credentials_Twitter.schedule(0, "setVisible", 0);
        geShoutout_Container_BelowTwitter.setTrgPosition(0, getWord(geShoutout_Credentials_Twitter.getPosition(), 1) + 2);
    }
    shoutout_checkSendable();
    return ;
}
function shoutout_setFBInclude(%value)
{
    shoutout_checkSendable();
    return ;
}
function shoutout_checkSendable()
{
    %sendable = 0;
    %sendable = %sendable | $UserPref::UI::ShoutOut::Ticker::Include;
    %sendable = %sendable | $UserPref::UI::ShoutOut::Twitter::Include;
    %sendable = %sendable | $UserPref::UI::ShoutOut::FB::Include;
    geShoutout_Snapshot_SendButton.setActive(%sendable);
    return ;
}
function geShoutout_Snapshot_Message::onKeystroke(%this)
{
    %max = shoutout_getCurrentMaxCharacters();
    %used = strlen(%this.getText());
    %left = %max - %used;
    geShoutout_Snapshot_Message_CharacterCount.setTextWithStyle(%left);
    return ;
}
function geShoutout_Snapshot_Message::onEnter(%this)
{
    geShoutout_Snapshot_SendButton.onClick();
    return ;
}
function geShoutout_Snapshot_Message::onCtrlEnter(%this)
{
    %this.onEnter();
    return ;
}
function geShoutout_Credential_Twitter_Username_Save::onClick(%this)
{
    %save = %this.getValue();
    gUserPropMgrClient.setProperty($Player::Name, "twitter_un_save", %save);
    gUserPropMgrClient.setProperty($Player::Name, "twitter_un", %save ? geShoutout_Credential_Twitter_Username.getText() : "");
    return ;
}
function geShoutout_Credential_Twitter_Password_Save::onClick(%this)
{
    %save = %this.getValue();
    gUserPropMgrClient.setProperty($Player::Name, "twitter_pw_save", %save);
    gUserPropMgrClient.setProperty($Player::Name, "twitter_pw", %save ? geShoutout_Credential_Twitter_Password.getText() : "");
    return ;
}
function geShoutout_Snapshot_SendButton::onClick(%this)
{
    if (!%this.isActive())
    {
        MessageBoxOK("No shoutout selected", "You need to choose at least one shoutout option");
        return ;
    }
    $gShoutOut_PhotoURL = "";
    $gShoutOut_ShortPhotoURL = "";
    if ($UserPref::UI::ShoutOut::Ticker::Include)
    {
        MessageBoxYesNo("vSide Ticker", "Shouting out to the vSide ticker will display your message to everyone connected!<br>You\'ll be charged" SPC shoutout_getPriceVBux_Ticker() SPC "vBux.<br><br>Okay to pay " SPC shoutout_getPriceVBux_Ticker() SPC "vBux ?", "shoutOut_action_testPhase2();", "shoutOut_action_GroundState();", 1);
    }
    else
    {
        shoutOut_action_testPhase2();
    }
    return ;
}
function shoutOut_action_testPhase2()
{
    shoutOut_action_testTwitterCredentialsIfNecessary();
    return ;
}
function geShoutout_Snapshot_CancelButton::onClick(%this)
{
    shoutOut_action_CleanupWithoutSend();
    return ;
}
function shoutOut_action_testTwitterCredentialsIfNecessary()
{
    shoutOut_SetStatus("Checking..", geShoutout_Avatar_Twitter.getBitmap());
    if (!geShoutout_Twitter_Include.getValue())
    {
        shoutOut_action_testFBCredentialsIfNecessary();
    }
    else
    {
        shoutOut_action_testTwitterCredentials(0);
    }
    return ;
}
function shoutOut_action_testTwitterCredentials(%oneShot)
{
    if ((geShoutout_Credential_Twitter_Username.getText() $= "") && (geShoutout_Credential_Twitter_Password.getText() $= ""))
    {
        geShoutout_Avatar_Twitter.setBitmap("");
        geShoutout_Avatar_Twitter.tooltip = "";
        if (%oneShot)
        {
            shoutOut_action_GroundState();
        }
        else
        {
            MessageBoxOK("Need your Twitter 411", "Please enter your Twitter username and password..", "shoutOut_action_groundState();");
        }
        return ;
    }
    %user = geShoutout_Credential_Twitter_Username.getText();
    %pass = geShoutout_Credential_Twitter_Password.getText();
    %request = sendRequest_Twitter_verify_credentials(%user, %pass, "onDoneOrErrorCallback_Twitter_verify_credentials");
    %request.oneShot = %oneShot;
    return ;
}
function onDoneOrErrorCallback_Twitter_verify_credentials(%request)
{
    %xmlDoc = new XMLDoc();
    %xmlDoc.parseXML(%request.getResults());
    %xmlRoot = %xmlDoc.getRootElement();
    %succ = isObject(%xmlRoot) && (%xmlRoot.getValue() $= "user");
    geShoutout_Avatar_Twitter.setBitmap("platform/client/ui/external_portrait_unknown");
    geShoutout_Avatar_Twitter.tooltip = "problem accessing your twitter account";
    if (!%succ)
    {
        if (!%request.oneShot)
        {
            MessageBoxOK("Problem with twitter", "Your twitter username or password may be wrong.", "");
            shoutOut_action_GroundState();
        }
    }
    else
    {
        if (geShoutout_Credential_Twitter_Username_Save.getValue())
        {
            gUserPropMgrClient.setProperty($Player::Name, "twitter_un", geShoutout_Credential_Twitter_Username.getText());
        }
        if (geShoutout_Credential_Twitter_Password_Save.getValue())
        {
            gUserPropMgrClient.setProperty($Player::Name, "twitter_pw", geShoutout_Credential_Twitter_Password.getText());
        }
        %profile_image_url = %xmlRoot.getFirstChild("profile_image_url").getText();
        if (!(%profile_image_url $= ""))
        {
            geShoutout_Avatar_Twitter.downloadAndApplyBitmap(%profile_image_url);
            geShoutout_Avatar_Twitter.tooltip = "twitter account verified";
        }
        if (!%request.oneShot)
        {
            shoutOut_action_testFBCredentialsIfNecessary();
        }
    }
    %xmlDoc.delete();
    return ;
}
function shoutOut_action_testFBCredentialsIfNecessary()
{
    shoutOut_action_sendPhase1();
    return ;
}
function shoutOut_action_sendPhase1()
{
    shoutOut_action_savePhotoIfNecessary();
    return ;
}
function shoutOut_action_savePhotoIfNecessary()
{
    if (!$UserPref::UI::ShoutOut::Show::Pic)
    {
        shoutOut_action_sendPhase2();
        return ;
    }
    shoutOut_SetStatus("Uploading..");
    %fileName = geShoutout_Snapshot.snap_fnBase @ geShoutout_Snapshot.snap_fnExt;
    %caption = geShoutout_Snapshot_Message.getText();
    %peopleInViewList = "";
    error(getScopeName() SPC "- todo: determine people in view");
    %type = "screenshot";
    %location = $player.getTransform();
    sendRequest_UploadPhoto(%fileName, %caption, %peopleInViewList, %type, %location, 0, "onDoneOrErrorCallback_UploadPhoto_ShoutOut");
    return ;
}
function onDoneOrErrorCallback_UploadPhoto_ShoutOut(%request)
{
    if (!%request.checkSuccess())
    {
        MessageBoxOK("Uh Oh", "There was some problem saving your screenshot.<br><br>Try deleting some from <a:" @ $Net::PhotoAlbumURL @ ">your photo album</a>,<br>or try without a screenshot.", "");
        shoutOut_action_GroundState();
        return ;
    }
    $gShoutOut_PhotoURL = %request.getResult("photoURL");
    %s = strreplace($gShoutOut_PhotoURL, "/", "\t");
    %id = getField(%s, 4);
    $gShoutOut_PhotoURL = $Net::PhotoPageURL @ %id;
    shoutOut_action_shortenPhotoURL();
    return ;
}
function shoutOut_action_shortenPhotoURL()
{
    shoutOut_SetStatus("Shortening..");
    %request = sendRequest_Bitly_shorten($gShoutOut_PhotoURL, "onDoneOrErrorCallback_Bitly_shorten");
    %request.photoURL = $gShoutOut_PhotoURL;
    return ;
}
function onDoneOrErrorCallback_Bitly_shorten(%request)
{
    %xmlDoc = new XMLDoc();
    %xmlDoc.parseXML(%request.getResults());
    %xmlRoot = %xmlDoc.getRootElement();
    if (!(%xmlRoot.getValue() $= "bitly"))
    {
        error(getScopeName() SPC "- bad root." SPC %request.getURL() SPC %request.getResults());
        shoutOut_action_shortenPhotoURLFailed();
        return ;
    }
    if (!(%xmlRoot.getFirstChild("errorCode").getText() $= 0))
    {
        error(getScopeName() SPC "- some error." SPC %request.getURL() SPC %request.getResults());
        shoutOut_action_shortenPhotoURLFailed();
        return ;
    }
    %xmlNode = %xmlRoot.getFirstChild("results");
    %xmlNode = %xmlNode.getFirstChild("nodeKeyVal");
    %xmlNode = %xmlNode.getFirstChild("shortUrl");
    if (!isObject(%xmlNode))
    {
        error(getScopeName() SPC "- can\'t find results." SPC %request.getURL() SPC %request.getResults());
        shoutOut_action_shortenPhotoURLFailed();
        return ;
    }
    $gShoutOut_ShortPhotoURL = %xmlNode.getText();
    shoutOut_action_sendPhase2(%request.photoURL, $gShoutOut_ShortPhotoURL);
    %xmlDoc.delete();
    return ;
}
function shoutOut_action_shortenPhotoURLFailed()
{
    MessageBoxOK("Uh Oh", "There was some problem linking to your screenshot.<br><br>Try again in a little bit,<br>or try without a screenshot.", "");
    shoutOut_action_GroundState();
    return ;
}
function shoutOut_action_sendPhase2()
{
    if ($UserPref::UI::ShoutOut::FB::Include && ($gShoutOut_PhotoURL $= ""))
    {
        MessageBoxOK("sorry, i forgot to mention..", "To share on Facebook you have to use a snapshot.<br>This will be fixed in a future release,<br>but for now Try again!", "schedule(500, 0, \"shoutOut_action_forceSnapshot\");");
        shoutOut_action_GroundState();
        return ;
    }
    if ($UserPref::UI::ShoutOut::Ticker::Include)
    {
        shoutOut_action_sendTicker();
    }
    else
    {
        shoutOut_action_sendPhase3();
    }
    return ;
}
function shoutOut_action_sendTicker()
{
    shoutOut_SetStatus("Ticking..", geTGF_profilePic.getBitmap());
    %messageText = geShoutout_Snapshot_Message.getText();
    if (!($gShoutOut_ShortPhotoURL $= ""))
    {
        %link = " " @ $gShoutOut_ShortPhotoURL;
    }
    else
    {
        %link = "";
    }
    %messageText = %messageText @ %link;
    %priority = 2;
    %request = sendRequest_PublishToTicker(%messageText, %priority, "onDoneOrErrorCallback_PublishToTicker");
    %request.messageText = %messageText;
    %analytic = getAnalytic();
    %withPic = $gShoutOut_ShortPhotoURL $= "" ? "" : "/photo";
    %analytic.trackPageView("/client/shoutout/ticker" @ %withPic);
    return ;
}
function onDoneOrErrorCallback_PublishToTicker(%request)
{
    if (!%request.checkSuccess())
    {
        MessageBoxOK("Uh Oh", "There was some problem sending to the ticker. You haven\'t been charged.", "");
        shoutOut_action_GroundState();
        return ;
    }
    if ($StandAlone)
    {
        commandToServer('fakeTicker', %request.messageText);
    }
    shoutOut_action_sendPhase3();
    return ;
}
function shoutOut_action_sendPhase3()
{
    if ($UserPref::UI::ShoutOut::FB::Include)
    {
        shoutOut_action_sendFB();
    }
    if ($UserPref::UI::ShoutOut::Twitter::Include)
    {
        shoutOut_SetStatus("Tweeting..", geShoutout_Avatar_Twitter.getBitmap());
        shoutOut_action_sendTweet();
    }
    else
    {
        shoutOut_action_CleanupWithSend();
    }
    return ;
}
function shoutOut_action_sendFB()
{
    %sharerURL = "http://www.facebook.com/sharer.php";
    %sharerURL = %sharerURL @ "?u=" @ urlEncode($gShoutOut_PhotoURL);
    gotoWebPage(%sharerURL);
    %analytic = getAnalytic();
    %withPic = $gShoutOut_ShortPhotoURL $= "" ? "" : "/photo";
    %analytic.trackPageView("/client/shoutout/facebook" @ %withPic);
    return ;
}
function shoutOut_action_sendTweet()
{
    %tweetText = geShoutout_Snapshot_Message.getText();
    if (!($gShoutOut_ShortPhotoURL $= ""))
    {
        %link = "(Sent from vSide:" SPC $gShoutOut_ShortPhotoURL @ ")";
    }
    else
    {
        %link = "(Sent from vSide:" SPC "http://" @ $Net::BaseDomain @ ")";
    }
    %tweetText = %tweetText SPC %link;
    %user = geShoutout_Credential_Twitter_Username.getText();
    %pass = geShoutout_Credential_Twitter_Password.getText();
    %request = sendRequest_Twitter_statuses_update(%user, %pass, %tweetText, "onDoneOrErrorCallback_Twitter_statuses_update");
    %analytic = getAnalytic();
    %withPic = $gShoutOut_ShortPhotoURL $= "" ? "" : "/photo";
    %analytic.trackPageView("/client/shoutout/twitter" @ %withPic);
    return ;
}
function onDoneOrErrorCallback_Twitter_statuses_update(%request)
{
    %xmlDoc = new XMLDoc();
    %xmlDoc.parseXML(%request.getResults());
    %xmlRoot = %xmlDoc.getRootElement();
    %succ = isObject(%xmlRoot) && (%xmlRoot.getValue() $= "status");
    if (%succ)
    {
        %tweetID = %xmlRoot.getFirstChild("id").getText();
        if (%tweetID $= "")
        {
            error(getScopeName() SPC "- no tweet ID." SPC %request.getResults());
            %msg = "<br>sent!";
        }
        else
        {
            %tweetURL = $Net::TwitterBaseInsecure @ "/" @ geShoutout_Credential_Twitter_Username.getText() @ "/status/" @ %tweetID;
            %msg = "<br><b><linkcolor:ffffffff><linkcolorhl:ffaaffdd><a:gamelink:" @ %tweetURL @ ">click here to view your tweet.</a><br>";
        }
        MessageBoxOK("Success!", %msg, "", 1, "success" @ getScopeName());
        shoutOut_action_GroundState();
        shoutOut_action_CleanupWithSend();
    }
    else
    {
        shoutOut_SetStatus("");
        %twitterErr = isObject(%xmlRoot) ? %xmlRoot.getFirstChild("error").getText() : "(could not connect)";
        %yesCmd = "shoutOut_action_sendTweet();";
        %noCmd = "";
        MessageBoxYesNo("Problem with twitter", "Hmm, something went wrong.<br>Twitter says: \"" @ %twitterErr @ "\"<br>Would you like to re-send ?", %yesCmd, %noCmd);
        shoutOut_action_GroundState();
    }
    %xmlDoc.delete();
    return ;
}
function shoutOut_action_forceSnapshot()
{
    geShoutout_Snapshot_Opt_Pic.setValue(1);
    shoutout_setIncludeSnapshot(1);
    shoutout_takeSnapshot();
    return ;
}
function shoutOut_SetStatus(%status, %avatarBitmap)
{
    isDefined("%avatarBitmap", "");
    if (%status $= "")
    {
        geShoutOut_Sending_Container.setVisible(0);
        return ;
    }
    geShoutOut_Sending_Container.setVisible(1);
    geShoutOut_Sending_Panel.alignToCenterXY();
    geShoutOut_Sending_Status_Text.setTextWithStyle(%status);
    geShoutout_Sending_Avatar.setBitmap(%avatarBitmap);
    geShoutout_Sending_Avatar_Container.setVisible(!(%avatarBitmap $= ""));
    return ;
}
function shoutOut_action_GroundState()
{
    shoutOut_SetStatus("");
    return ;
}
function shoutOut_action_CleanupWithSend()
{
    MessageHudEdit.setText("");
    geShoutOutWindow.close();
    return ;
}
function shoutOut_action_CleanupWithoutSend()
{
    geShoutOutWindow.close();
    return ;
}
