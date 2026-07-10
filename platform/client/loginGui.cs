function LoginGui::onWake(%this) {
    sendStatusRequest();
    %this.setControlsActive(1);
    LoginProgressBarCtrls.setVisible(0);
    %this.displayPartnerInfo();
    clearScreenSizeStack();
    pushScreenSize(960, 544, 0, 1, 0);
    WorldMap.server = 0;
    $ServerName = "";
    loggedoutCleanup();
    if ($UserPref::Login::RememberMe) {
        LoginUserNameField.setText($UserPref::Player::Name);
        LoginPasswordField.setText($UserPref::Player::Password);
    }
    LoginUserNameField.setText("");
    LoginPasswordField.setText("");
    LoginUserNameField.makeFirstResponder(1);
    LoginUserNameField.selectAll();
    %colors = "<linkcolorhl:66aaff><linkcolor:ccddddff>";
    LoginHelpLinks.setText(%colors @ "<a:gamelink " @ $Net::HelpURL_Guidelines @ "><just:right>User Guidelines</a> | <a:gamelink " @ $Net::HelpURL_General @ ">Help</a>");
    %colors = "<linkcolorhl:66aaff><linkcolor:ccdddd80>";
    LoginCreditsLink.setText(%colors @ "<a:gamelink CREDITS>Credits</a>");
    if (RegistrationGui.haveIncompleteRegistration()) {
        %regText = "Complete Registration";
    }
    %regText = "Register";
    LoginRegistrationLinks.setText(%colors @ "<just:right><spush><b><a:gamelink REGISTER>" @ %regText @ "</a><spop> | <a:gamelink " @ $Net::ForgotPassURL @ ">Forgot Password</a>");
    if (!isObject(LoginPBController)) {
        new ScriptObject(LoginPBController) {
            class = "ProgressBarController";
        };
    }
    LoginPBController.Initialize(LoginProgressHolder, "platform/client/ui/progress_empty", "platform/client/ui/progress_fill", "", "");
    %this.update(0);
    if ($UserPref::Login::firstRun) {
        if (isValidHostAddress($Net::DownloadHost)) {
            sendFirstLaunchRequest();
        }
        $UserPref::Login::firstRun = 0;
    }
    if (!$gHasOpenedRegistrationGui) {
    }
    if (RegistrationGui.haveIncompleteRegistration()) {
        RegistrationGui.tryOpenOrWebPage();
    }
    checkForClientUpgrades();
    %analytic = getAnalytic();
    %analytic.requestNewSession();
};
function getStockPartnerShortcutText(%name) {
    %obj = gLoginPartnersInfo.getPartnerObj(%name);
    %vurl = %obj.vurl;
    return "Go straight to <spush><color:ddff55>" @ %obj.extraLongName @ "<spop>: <a:" @ %vurl @ ">Click Here</a>" @ ".";
};
function initLoginPartners() {
    safeEnsureScriptObject("StringMap", "gLoginPartnersInfo");
    gLoginPartnersInfo.clear();
    %name = "doppelganger";
    %obj = gLoginPartnersInfo.getOrMakePartnerObj(%name);
    %obj.shortName = %name;
    %obj.longName = "vSide";
    %obj.extraLongName = "vSide";
    %obj.vurl = "vside:/location/nv/MapSpawns_FUE";
    %obj.shortcutText = getStockPartnerShortcutText(%name);
    %obj.changesLoginScreen = 1;
    %obj.gatewayOptionBody = "<br>From here, you can check out a <spush><b>bumpin' party<spop><br>or chill out in <spush><b>your very own apartment<spop>.";
    %obj.gatewayOptionButton1 = "Take me Clubbing!";
    %obj.gatewayOptionButton2 = "Take me to my place!";
    %name = "degrassi";
    %obj = gLoginPartnersInfo.getOrMakePartnerObj(%name);
    %obj.shortName = %name;
    %obj.longName = "Degrassi";
    %obj.extraLongName = "The DOT Grill and DOT Dorms";
    %obj.vurl = "vside:/location/lga/MapSpawns_Degrassi";
    %obj.shortcutText = getStockPartnerShortcutText(%name);
    %obj.changesLoginScreen = 0;
    %obj.gatewayOptionBody = "<br>From here, you can check out <spush><b>the DOT Grill<spop><br>or chill out in your own room in <spush><b>the DOT Dorms<spop>.";
    %obj.gatewayOptionButton1 = "Take me to the Grill!";
    %obj.gatewayOptionButton2 = "Take me to my Dorm!";
    %name = "";
    %obj = gLoginPartnersInfo.getOrMakePartnerObj(%name);
    %obj.shortName = gLoginPartnersInfo.get("doppelganger").shortName;
    %obj.longName = gLoginPartnersInfo.get("doppelganger").longName;
    %obj.extraLongName = gLoginPartnersInfo.get("doppelganger").extraLongName;
    %obj.vurl = gLoginPartnersInfo.get("doppelganger").vurl;
    %obj.shortcutText = gLoginPartnersInfo.get("doppelganger").shortcutText;
    %obj.changesLoginScreen = gLoginPartnersInfo.get("doppelganger").changesLoginScreen;
    %obj.gatewayOptionBody = gLoginPartnersInfo.get("doppelganger").gatewayOptionBody;
    %obj.gatewayOptionButton1 = gLoginPartnersInfo.get("doppelganger").gatewayOptionButton1;
    %obj.gatewayOptionButton2 = gLoginPartnersInfo.get("doppelganger").gatewayOptionButton2;
};
function gLoginPartnersInfo::getOrMakePartnerObj(%this, %shortName) {
    if (!%this.hasKey(%shortName)) {
        %obj = safeNewScriptObject("ScriptObject", "", 0);
        %this.put(%shortName, %obj);
    }
    return %this.get(%shortName);
};
function gLoginPartnersInfo::getPartnerObj(%this, %shortName) {
    %obj = %this.get(%shortName);
    if (!isObject(%obj)) {
        error(getScopeName(1) @ " " @ "- DNE:" @ " " @ %shortName);
        %obj = %this.get("");
    }
    return %obj;
};
initLoginPartners();
$gEnableStartHereMenu = 0;
function LoginGui::displayPartnerInfo(%this) {
    %partnerObj = gLoginPartnersInfo.getPartnerObj($Net::userOwner);
    if (%partnerObj.changesLoginScreen) {
        LoginStartHereCtrls.setVisible($gEnableStartHereMenu);
        LoginPartnerLogo.setVisible(1);
        LoginPartnerLogo.setBitmap("platform/client/ui/with_" @ $Net::userOwner);
        LoginPartnerLogo.fitSize();
        LoginStartHerePopup.clear();
        LoginStartHerePopup.add(%partnerObj.longName);
        LoginStartHerePopup.add("Map");
        LoginStartHerePopup.selectUserPreferred();
    }
    LoginStartHereCtrls.setVisible(0);
    LoginPartnerLogo.setVisible(0);
    LoginStartHerePopup.clear();
};
function LoginStartHerePopup::selectUserPreferred(%this) {
    %size = %this.size();
    %i = 0;
    while ((%i < %size)) {
        if ((%this.getTextById(%i) $= $UserPref::Login::StartHere)) {
            %this.SetSelected(%i);
            return;
        }
        %i = (%i + 1.0);
    }
    %this.SetSelected(0);
};
function LoginStartHerePopup::onSelect(%this, %id, %entries) {
    $UserPref::Login::StartHere = %entries;
    if ((%entries $= "Degrassi")) {
        $VURLcmd = gLoginPartnersInfo.get("degrassi").vurl;
    }
    log("initialization", "debug", "Disabled removal of the vSide address URL.");
};
function LoginRegistrationLinks::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    if ((%url $= "REGISTER")) {
        RegistrationGui.tryOpenOrWebPage();
    }
    if ((getSubStr(%url, 0, 7) $= "vside:/")) {
        vurlOperation(%url);
    }
    gotoWebPage(%url);
};
function LoginHelpLinks::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    if ((%url $= "REGISTER")) {
        RegistrationGui.tryOpenOrWebPage();
    }
    if ((getSubStr(%url, 0, 7) $= "vside:/")) {
        vurlOperation(%url);
    }
    gotoWebPage(%url);
};
function LoginCreditsLink::onURL(%this, %url) {
    doCredits();
};
function LoginGui::setControlsActive(%this, %flag) {
    %this.controlsActive = %flag;
    LoginLoginButton.setActive(%flag);
    LoginRememberMeCheckbox.setActive(%flag);
    %profile = %flag ? ETSLoginEditProfile : ETSLoginNoEditProfile;
    LoginUserNameField.text = LoginUserNameField.getValue();
    LoginUserNameField.setProfile(%profile);
    LoginPasswordField.text = LoginPasswordField.getValue();
    LoginPasswordField.setProfile(%profile);
    %fr = Canvas.getFirstResponder();
    if (isObject(%fr)) {
        %fr.makeFirstResponder(0);
    }
};
function LoginGui::onCanvasResize(%this) {
    %this.update(0);
};
function LoginGui::doLoginButton(%this) {
    $Player::Name = LoginUserNameField.getValue();
    $Player::Password = LoginPasswordField.getValue();
    if (($Player::Name $= "")) {
        MessageBoxOK("No User Name", "Please enter a user name.", "");
        return;
    }
    if (($Player::Password $= "")) {
        MessageBoxOK("No Password", "Please enter a password.", "");
        return;
    }
    if ($ETS::devMode) {
    }
    if (($Player::Name $= "debug")) {
        loginDebugPanel.open();
        return;
    }
    %this.setControlsActive(0);
    LoginProgressBarCtrls.setVisible(1);
    if ($UserPref::Login::RememberMe) {
        $UserPref::Player::Name = $Player::Name;
        $UserPref::Player::Password = $Player::Password;
    }
    $UserPref::Player::Name = "";
    $UserPref::Player::Password = "";
    %this.envManagerLogin();
};
function LoginGui::envManagerLogin(%this) {
    BuddyHudWin.firstTime = 1;
    if (isObject(LoginRequest)) {
        LoginRequest.delete();
    }
    %loginRequest = new ManagerRequest(LoginRequest);
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%loginRequest);
    }
    %url = $Net::SecureClientServiceURL @ "/login?";
    %userValue = "user=" @ urlEncode($Player::Name);
    %passValue = "&password=" @ urlEncode(MD5($Player::Password));
    %version = "&version=" @ urlEncode(getProtocolVersion());
    %build = "&build=" @ urlEncode(getBuildVersion());
    %os = "&os=" @ urlEncode(getSystemVersion());
    %id = "&id=" @ $System::ID1;
    %id2 = "&id2=" @ $System::ID2;
    %id3 = "&id3=" @ $System::ID3;
    %id4 = "&id4=" @ $System::ID4;
    %url = %url @ %userValue @ %passValue @ %version @ %build @ %os @ %id @ %id2 @ %id3 @ %id4;
    log("login", "debug", "login: " @ %url);
    %loginRequest.setURL(%url);
    %loginRequest.setProgress(1);
    %loginRequest.originalName = $Player::Name;
    LoginProgressBarCtrls.setVisible(1);
    LoginPBController.setValue(0.1);
    $Login::loggedIn = 0;
    %loginRequest.start();
};
function LoginGui::onConnectFailed(%this, %msg) {
    sendStatusRequest();
    if ((%msg $= "")) {
        %msg = "Could not connect";
    }
    LoginProgressBarCtrls.setVisible(0);
    LoginPBController.setValue(0);
    %this.setControlsActive(1);
};
function LoginGui::nextControl(%this, %curControl) {
    %nextControl = "";
    if ((%curControl.getName() $= LoginUserNameField)) {
        %nextControl = LoginPasswordField;
    }
    if ((%curControl.getName() $= LoginPasswordField)) {
        %nextControl = LoginLoginButton;
    }
    if ((%nextControl $= "")) {
        error("nextControl got invalid arg" @ " " @ %curControl);
        return;
    }
    if ((%nextControl $= LoginLoginButton)) {
        $Login::newAccount = 0;
        LoginGui.doLoginButton();
    }
    %nextControl.makeFirstResponder(1);
    %nextControl.selectAll();
};
function LoginRequest::onError(%this, %errorNum, %unused) {
    if ((%errorNum == $CURL::CouldNotResolveHost)) {
        LoginGui.onConnectFailed("Could not reach server");
        MessageBoxOK("Could Not Find Server", $MsgCat::network["E-SERVER-DNS"], "");
    }
    LoginGui.onConnectFailed("Could not connect");
    MessageBoxOK("Could not connect", "Could not connect to " @ $ETS::AppName @ " servers.  " @ $ETS::AppName[$MsgCat::network @ "H-SYS-DOWN"] @ "  " @ $ETS::AppName[$MsgCat::network @ "H-SYS-DOWN"][$MsgCat::network @ "H-SEE-FORUMS"], "");
};
function LoginRequest::onConnected(%this) {
    LoginPBController.setValue(0.5);
};
function LoginRequest::onDone(%this) {
    LoginGui.setControlsActive(1);
    LoginPBController.setValue(1);
    if ((%this.statusCode() != $HTTP::StatusOK)) {
        LoginGui.onConnectFailed("Error communicating with server");
        log("communication", "error", "client HTTP code: " @ %this.statusCode());
        MessageBoxOK("Server Unavailable", $MsgCat::network["E-SERVER-UNAVAIL"], "");
        return;
    }
    %status = strlwr(findRequestStatus(%this));
    log("login", "debug", "LoginRequest::onDone status: " @ %status);
    if ((%status $= "fail") || (%status $= "error")) {
        %errorCode = %this.getValue("errorCode");
        %errorCode = strlwr(%errorCode);
        log("login", "error", "errorCode = " @ %errorCode);
        if ((%errorCode $= "invalid")) {
            LoginGui.onConnectFailed("Wrong name or password");
            MessageBoxOK("Invalid Login", $MsgCat::login["E-PASSWORD"], "");
        }
        if ((%errorCode $= "overloaded")) {
            LoginGui.onConnectFailed("Overcrowded");
            MessageBoxOK("No More Room", $ETS::AppName @ $ETS::AppName[$MsgCat::server @ "E-SERVER-FULL"], "");
        }
        if ((%errorCode $= "serverfail")) {
            LoginGui.onConnectFailed("There was a server error");
            MessageBoxOK("Server Error", $MsgCat::login["E-UNKNOWN"], "");
        }
        if ((%errorCode $= "inactive")) {
            LoginGui.onConnectFailed("Activation required");
            MessageBoxOK("Activation Required", "You have not yet activated your account.  Check your email for the message with the activation link.  If you have not received an activation message, you can get another copy sent to you at <a:" @ $Net::ActivationURL @ ">the registration site</a>.", "");
        }
        if ((%errorCode $= "alreadyloggedin")) {
            LoginGui.onConnectFailed("You are already logged in on another connection.");
            MessageBoxYesNo("Already Logged In", $MsgCat::login["E-ALREADY-IN"], "LoginRequest::handleBoot();", "LoginRequest::cancelBoot();");
        }
        if ((%errorCode $= "banned")) {
            LoginGui.onConnectFailed("Banned");
            MessageBoxOK("Banned", $MsgCat::login["E-BANNED"] @ $MsgCat::login["E-BANNED"][$MsgCat::login @ "E-DONT-KNOW-RULES"], "");
        }
        if ((%errorCode $= "suspended")) {
            LoginGui.onConnectFailed("Banned");
            %msg = "";
            %msg = %msg @ %msg[$MsgCat::login @ "E-SUSPENDED"];
            %msg = %msg @ "\n";
            %msg = %msg @ "\n";
            %msg = %msg @ %this.getValue("suspensionReason");
            %msg = strreplace(%msg, "[READTOU]", "");
            %msg = %msg @ "[READTOU]";
            %secs = %this.getValue("suspensionSecondsRemaining");
            %secs = ((%secs + 60.0) - (%secs % 60));
            %msg = %msg @ "\n";
            %msg = %msg @ "\n";
            %msg = %msg @ "Timeout Remaining:" @ " " @ secondsToDaysHoursMinutesSeconds(%secs);
            %msg = standardSubstitutions(%msg);
            MessageBoxOK("Suspended", %msg, "");
        }
        if ((%errorCode $= "upgrade_required")) {
            LoginGui.onConnectFailed("Upgrade required");
            MessageBoxOK("Upgrade Required", $MsgCat::login["E-UPGRADE-1"] @ $ETS::AppName @ ".  " @ $ETS::AppName[$MsgCat::login @ "E-UPGRADE-2"], "");
        }
        LoginGui.onConnectFailed("There was a server error");
        MessageBoxOK("Server Error", $MsgCat::login["E-UNKNOWN"], "");
        %analytic = getAnalytic();
        %analytic.trackPageView("/client/login/error/" @ %errorCode);
    }
    if ((%status $= "upgrade_available")) {
        LoginRequest::commonLogin(%this);
        MessageBoxOK("Upgrade Available", "There is a new version of" @ " " @ $ETS::AppName @ " " @ "available.  " @ $ETS::AppName[$MsgCat::login @ "E-UPGRADE-3"], "");
    }
    if ((%status $= "success")) {
        LoginRequest::commonLogin(%this);
    }
};
function LoginRequest::commonLogin(%this) {
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/login");
    %this.parseResponse();
    gUserPropMgrClient.forgetProperties($Player::Name);
    %cb = %this.getId() @ ".commonLogin_Part2();";
    gUserPropMgrClient.requestProperties($Player::Name, %cb);
    if (isObject(StoreShoppingList)) {
        StoreShoppingList.clear();
    }
    RegistrationGui.markCurrentRegistrationAsCompleted();
};
function LoginRequest::commonLogin_Part2(%this) {
    $Player::myPlaceVURL = "";
    %this.onGotUserProperties();
    outfits_init();
    outfits_retrieve();
    WorldMap.setNotConnectedToServer();
    geTGF.onLogin();
    $Login::loggedIn = 1;
    if (isObject(ClosetGui)) {
        ClosetGui.lastTabOpened = "";
    }
    if (isObject(ProfileCurrentPicture)) {
        ProfileCurrentPicture.setBitmap("");
    }
    $Player::attemptsToAutoUploadAvatarSnapshot = 0;
    $Player::hasSeenTakeAvatarPhotoDialog = 0;
    %vurl = getSkipMapVurl(0);
    if ((%vurl $= "") && ($Player::activated != 1.0)) {
        if (($Player::hasEmail == 1.0)) {
            if ($Login::newAccount) {
                MessageBoxOK("Confirmation Sent", $MsgCat::login["CONF-EMAIL"], "");
            }
            MessageBoxYesNo("Email Not Verified", $MsgCat::login["CONF-NAG"], "gotoWebPage(\"" @ $Net::ActivationURL @ "\");", "");
        }
        if (!$Login::newAccount) {
            MessageBoxYesNo("No Email Address", $MsgCat::login["NO-EMAIL-NAG"], "gotoWebPage(\"" @ $Net::AccountEditURL @ "\");", "");
        }
    }
    %validCharacters = "abcdefghijklmnopqrstuvwxyz" @ "ABCDEFGHIJKLMNOPQRSTUVWXYZ" @ "0123456789-_ ";
    if (0 || !(stripString($Player::Name, %validCharacters) $= $Player::Name)) {
        %msgBox = MessageBoxOK("USERNAME WARNING", "\n<b>" @ $MsgCat::login["LEGACY-USERNAME-NAG"] @ "\n", "");
        %msgBox.setWindowWidth(350);
    }
    destroySpaceInfo($CSSpaceInfo);
    $CSSpaceInfo = 0;
    if (isObject(CSRulesAndDescWindow)) {
        CSDescTaglineTextBox.setText("");
        CSRulesPasswordField.setText("");
        if (CSRulesAndDescWindow.initialized) {
            CSRulesDescSavedIndicator.reset();
            CSRulesPasswordSavedIndicator.reset();
            CSRulesAccessPopup.SetSelected(0);
        }
    }
    if (isObject(geTGF_HotSpotsDataTable)) {
        geTGF_HotSpotsDataTable.removeRowsByIndex(0, geTGF_HotSpotsDataTable.getRowCount());
        geTGF_HotSpotsDataTable.updateListeners();
    }
    if (isObject(geTGF_FriendsDataTable)) {
        geTGF_FriendsDataTable.removeRowsByIndex(0, geTGF_FriendsDataTable.getRowCount());
        geTGF_FriendsDataTable.updateListeners();
    }
    sendBuddyListRequest("onDoneOrErrorCallback_GetUserRelations_ForHotSpots");
    getBalancesAndScores("checkPointsEarnedSinceLastLogin();");
};
$gLastLoggedInThisSessionAs = "";
function LoginRequest::parseResponse(%this) {
    log("login", "debug", "LoginRequest::parseResponse");
    $Token = %this.getValue("token");
    log("login", "debug", "token: " @ $Token);
    %val = %this.getValue("gender");
    if (!(%val $= "")) {
        $UserPref::Player::gender = %val;
    }
    warn(getScopeName() @ " " @ "- gender not returned.");
    $Player::Name = %this.getValue("registered_user");
    if ($UserPref::Login::RememberMe) {
        $UserPref::Player::Name = $Player::Name;
    }
    $gLastLoggedInThisSessionAs = $Player::Name;
    eval("$Player::rolesMask      = " @ %this.getValue("rolesMask") @ ";");
    $Player::hasEmail = %this.getValueBool("hasemail");
    $Player::activated = %this.getValueBool("activated");
    $Player::inviter = %this.getValue("inviter");
    $Player::inviterOnline = "";
    $Player::inviterGender = "";
    clientHeartbeat();
};
function LoginRequest::onGotUserProperties(%this) {
    gUserPropMgrClient.clearPropertyIfExists($Player::Name, "favoriteActionsActionList");
    gUserPropMgrClient.clearPropertyIfExists($Player::Name, "favoriteActionsKeyComboList");
    $UserPref::Audio::masterVolume = gUserPropMgrClient.getProperty($Player::Name, "volumeMaster", $Defaults::UserPref::Audio::masterVolume);
    $UserPref::Audio::channelVolume1 = gUserPropMgrClient.getProperty($Player::Name, "volumeMusic", $Defaults::UserPref::Audio::channelVolume1);
    $UserPref::Audio::channelVolume2 = gUserPropMgrClient.getProperty($Player::Name, "volumeSfx", $Defaults::UserPref::Audio::channelVolume2);
    $UserPref::Audio::mute = gUserPropMgrClient.getProperty($Player::Name, "volumeMute", $Defaults::UserPref::Audio::mute);
    $UserPref::Audio::NotifyChat = gUserPropMgrClient.getProperty($Player::Name, "flashIncomingChat", $Defaults::UserPref::Audio::NotifyChat);
    $UserPref::Audio::NotifyWhisper = gUserPropMgrClient.getProperty($Player::Name, "flashIncomingWhisper", $Defaults::UserPref::Audio::NotifyWhisper);
    $UserPref::Chat::ShowTyping = gUserPropMgrClient.getProperty($Player::Name, "showTyping", $Defaults::UserPref::Chat::ShowTyping);
    $UserPref::Display::farNameOpacity = gUserPropMgrClient.getProperty($Player::Name, "farOpacity", $Defaults::UserPref::Display::farNameOpacity);
    $UserPref::Display::hideChat = gUserPropMgrClient.getProperty($Player::Name, "hideChat", $Defaults::UserPref::Display::hideChat);
    $UserPref::Display::hideNames = gUserPropMgrClient.getProperty($Player::Name, "hideNames", $Defaults::UserPref::Display::hideNames);
    $UserPref::debug::alertOnLogWarning = gUserPropMgrClient.getProperty($Player::Name, "alertOnLogWarning", $Defaults::UserPref::debug::alertOnLogWarning);
    $UserPref::debug::alertOnLogError = gUserPropMgrClient.getProperty($Player::Name, "alertOnLogError", $Defaults::UserPref::debug::alertOnLogError);
    $UserPref::ETS::ButtonBar::AutoHide = gUserPropMgrClient.getProperty($Player::Name, "hideButtonBar", $Defaults::UserPref::ETS::ButtonBar::AutoHide);
    $UserPref::HudTabs::AutoOpen["music"] = gUserPropMgrClient.getProperty($Player::Name, "autoOpenTabMusic", $Defaults::UserPref::HudTabs::AutoOpen["music"]);
    $UserPref::HudTabs::AutoClose["music"] = gUserPropMgrClient.getProperty($Player::Name, "autoCloseTabMusic", $Defaults::UserPref::HudTabs::AutoClose["music"]);
    $UserPref::HudTabs::AutoOpen["affinity"] = gUserPropMgrClient.getProperty($Player::Name, "autoOpenTabAffinity", $Defaults::UserPref::HudTabs::AutoOpen["affinity"]);
    $UserPref::HudTabs::AutoClose["affinity"] = gUserPropMgrClient.getProperty($Player::Name, "autoCloseTabAffinity", $Defaults::UserPref::HudTabs::AutoClose["affinity"]);
    $UserPref::HudTabs::AutoOpen["scores"] = gUserPropMgrClient.getProperty($Player::Name, "autoOpenTabScores", $Defaults::UserPref::HudTabs::AutoOpen["scores"]);
    $UserPref::HudTabs::AutoClose["scores"] = gUserPropMgrClient.getProperty($Player::Name, "autoCloseTabScores", $Defaults::UserPref::HudTabs::AutoClose["scores"]);
    $UserPref::HudTabs::AutoOpen["word"] = gUserPropMgrClient.getProperty($Player::Name, "autoOpenTabWord", $Defaults::UserPref::HudTabs::AutoOpen["word"]);
    $UserPref::HudTabs::AutoClose["word"] = gUserPropMgrClient.getProperty($Player::Name, "autoCloseTabWord", $Defaults::UserPref::HudTabs::AutoClose["word"]);
    $UserPref::Player::TeleportBlock = gUserPropMgrClient.getProperty($Player::Name, "refuseTeleports", $Defaults::UserPref::Player::TeleportBlock);
    $UserPref::Player::WhisperBlock = gUserPropMgrClient.getProperty($Player::Name, "refuseWhispers", $Defaults::UserPref::Player::WhisperBlock);
    $UserPref::Player::YellBlock = gUserPropMgrClient.getProperty($Player::Name, "refuseYells", $Defaults::UserPref::Player::YellBlock);
    $UserPref::Player::EmotesPermissionFriends = gUserPropMgrClient.getProperty($Player::Name, "emotesPermissionsFriends", $Defaults::UserPref::Player::EmotesPermissionFriends);
    $UserPref::Player::EmotesPermissionStrangers = gUserPropMgrClient.getProperty($Player::Name, "emotesPermissionsStrangers", $Defaults::UserPref::Player::EmotesPermissionStrangers);
    $UserPref::Player::GiftsPermissionFriends = gUserPropMgrClient.getProperty($Player::Name, "giftsPermissionsFriends", $Defaults::UserPref::Player::GiftsPermissionFriends);
    $UserPref::Player::GiftsPermissionStrangers = gUserPropMgrClient.getProperty($Player::Name, "giftsPermissionsStrangers", $Defaults::UserPref::Player::GiftsPermissionStrangers);
    $UserPref::Player::awayMessage = gUserPropMgrClient.getProperty($Player::Name, "awayMessage", $Pref::Player::defaultAwayMessage);
    if (isObject(DefaultAwayMsgEdit)) {
        DefaultAwayMsgEdit.setText("");
    }
    $UserPref::Player::autoReplyToWhispersWhenAway = gUserPropMgrClient.getProperty($Player::Name, "autoReplyToWhipsers", $Defaults::UserPref::Player::autoReplyToWhispersWhenAway);
    $UserPref::Player::filterProfanity = gUserPropMgrClient.getProperty($Player::Name, "filterProfanity", $Defaults::UserPref::Player::filterProfanity);
    $UserPref::Player::Genre = gUserPropMgrClient.getProperty($Player::Name, "playerMood", "");
    $UserPref::Player::height = gUserPropMgrClient.getProperty($Player::Name, "avatarHeight", $Defaults::UserPref::Player::height);
    $UserPref::Player::showOnRadar = gUserPropMgrClient.getProperty($Player::Name, "showOnRadar", $Defaults::UserPref::Player::showOnRadar);
    $UserPref::UI::FlashTaskBar = gUserPropMgrClient.getProperty($Player::Name, "flashTaskBar", $Defaults::UserPref::UI::FlashTaskBar);
    $UserPref::UI::Radar::AutoOpen = gUserPropMgrClient.getProperty($Player::Name, "radarAutoOpen", $Defaults::UserPref::UI::Radar::AutoOpen);
    $UserPref::UI::ShowAccountHud = gUserPropMgrClient.getProperty($Player::Name, "showAccountHud", $Defaults::UserPref::UI::ShowAccountHud);
    $UserPref::UI::ShowTooltips = gUserPropMgrClient.getProperty($Player::Name, "showTooltips", $Defaults::UserPref::UI::ShowTooltips);
    $UserPref::Video::Exposure = gUserPropMgrClient.getProperty($Player::Name, "videoExposure", $Defaults::UserPref::Video::Exposure);
    $UserPref::Video::exposureQualitySetting = gUserPropMgrClient.getProperty($Player::Name, "videoExposureQuality", $Defaults::UserPref::Video::exposureQuality);
    $UserPref::Video::shapeNameFontSize = gUserPropMgrClient.getProperty($Player::Name, "videoNameSize", $Defaults::UserPref::Video::shapeNameFontSize);
    $UserPref::Video::renderQualitySetting = gUserPropMgrClient.getProperty($Player::Name, "videoRenderQuality", $Defaults::UserPref::Video::renderQuality);
    $UserPref::Video::shadowQualitySetting = gUserPropMgrClient.getProperty($Player::Name, "videoShadowQuality", $Defaults::UserPref::Video::shadowQuality);
    $UserPref::Video::visibledistanceQualitySetting = gUserPropMgrClient.getProperty($Player::Name, "videoVisibleDistance", $Defaults::UserPref::Video::visibledistanceQuality);
    $UserPref::Video::waterreflectionQualitySetting = gUserPropMgrClient.getProperty($Player::Name, "videoWaterReflection", $Defaults::UserPref::Video::waterreflectionQuality);
    $UserPref::Video::ConstrainWindowDimensions = gUserPropMgrClient.getProperty($Player::Name, "videoConstrainWindowDimensions", $Defaults::UserPref::Video::ConstrainWindowDimensions);
    if (($UserPref::Player::Genre $= "")) {
    }
    if (isObject($player)) {
        %rand = getRandom(0, 2);
        $UserPref::Player::Genre = getSubStr($player.getDataBlock().possibleGenres, %rand, 1);
        echo("Chose random genre:" @ " " @ $UserPref::Player::Genre);
    }
    Music::setMuted($UserPref::Audio::mute);
    safeEnsureScriptObjectWithInit("StringMap", "EmoteBindingMap", "{ ignoreCase = true; }");
    EmoteBindingMap.clear();
    %maxNumberKeyCombos = getFieldCount($Defaults::UserPref::emotes::defaultKeyCombinations);
    %numberOfUnboundKeyCombinations["f"] = 0;
    %numberOfUnboundKeyCombinations["m"] = 0;
    %m = (%maxNumberKeyCombos - 1.0);
    while ((%m >= 0.0)) {
        %keyCombo = getField($Defaults::UserPref::emotes::defaultKeyCombinations, %m);
        %action = gUserPropMgrClient.getProperty($Player::Name, "favoriteActionsKey_f_" @ %keyCombo, -(1.0));
        if ((%action == -(1.0)) || (%action $= "")) {
            %action[%numberOfUnboundKeyCombinations @ "f"] = (%action[%numberOfUnboundKeyCombinations @ "f"] + 1.0);
            %keyCombo["" @ $UserPref::emotes TAB "f" @ %keyCombo] = ;
        }
        if (!(%keyCombo $= "")) {
            EmoteBindingMap.put(%action, %keyCombo);
            %keyCombo[%action @ $UserPref::emotes TAB "f" @ %keyCombo] = ;
        }
        %action = gUserPropMgrClient.getProperty($Player::Name, "favoriteActionsKey_m_" @ %keyCombo, -(1.0));
        if ((%action == -(1.0)) || (%action $= "")) {
            %action[%numberOfUnboundKeyCombinations @ "m"] = (%action[%numberOfUnboundKeyCombinations @ "m"] + 1.0);
            %keyCombo["" @ $UserPref::emotes TAB "m" @ %keyCombo] = ;
        }
        if (!(%keyCombo $= "")) {
            EmoteBindingMap.put(%action, %keyCombo);
            %keyCombo[%action @ $UserPref::emotes TAB "m" @ %keyCombo] = ;
        }
        %m = (%m - 1.0);
    }
    if ((EmoteBindingMap.size() == 0.0)) {
        EmoteHudList.setup();
    }
    EmoteHudList.populateLists();
    if ((%maxNumberKeyCombos[%numberOfUnboundKeyCombinations @ "f"] == %maxNumberKeyCombos)) {
        %m = (%maxNumberKeyCombos - 1.0);
        (%m >= 0.0);
        while ((%m >= 0.0)) {
            %keyCombo = getField($Defaults::UserPref::emotes::defaultKeyCombinations, %m);
            %keyCombo[%keyCombo[$Defaults::UserPref::emotes TAB "f" @ %keyCombo] @ $UserPref::emotes TAB "f" @ %keyCombo] = ;
            %m = (%m - 1.0);
        }
    }
    if ((%maxNumberKeyCombos[%numberOfUnboundKeyCombinations @ "m"] == %maxNumberKeyCombos)) {
        %m = (%maxNumberKeyCombos - 1.0);
        (%m >= 0.0);
        while ((%m >= 0.0)) {
            %keyCombo = getField($Defaults::UserPref::emotes::defaultKeyCombinations, %m);
            %keyCombo[%keyCombo[$Defaults::UserPref::emotes TAB "m" @ %keyCombo] @ $UserPref::emotes TAB "m" @ %keyCombo] = ;
            %m = (%m - 1.0);
        }
    }
};
function LoginRequest::handleBoot(%this) {
    sendRequest_BootNew("onDoneOrErrorCallback_Boot");
};
function onDoneOrErrorCallback_Boot(%request) {
    %status = %request.getResult("status");
    if ((%status $= "success")) {
        LoginGui.setControlsActive(0);
        LoginGui.envManagerLogin();
    }
    if ((%status $= "fail")) {
        error("client boot HTTP status: " @ %status);
        LoginGui.onConnectFailed("Could not disconnect");
        MessageBoxOK("Could Not Disconnect", $MsgCat::login["E-DISCONNECT"], "");
    }
    if ((%status $= "error")) {
        error("client boot HTTP status: " @ %status);
        LoginGui.onConnectFailed("There was a problem with the login server");
        MessageBoxOK("Problem Connecting", $MsgCat::login["E-CONNECT"], "");
    }
    error("client boot HTTP status: " @ %status);
    LoginGui.onConnectFailed("Error communicating with server");
    MessageBoxOK("Server Unavailable", $MsgCat::network["E-SERVER-UNAVAIL"], "");
};
function LoginGui::update(%this, %unused) {
    LoginSystemStatusText.setText($gLoginStatusMessage);
    LoginSystemStatusButton.setActive(0);
};
function LoginGui::getBitmap(%this, %set, %img) {
    return "LoginFader" @ %set @ "_" @ %img;
};
function LoginGui::fadeInBitmap(%this, %set, %img) {
    %bitmap = %this.getBitmap(%set, %img);
    if (%bitmap.done) {
        %bitmap.fadeInTime = 1000;
        %bitmap.waitTime = 0;
        %bitmap.fadeOutTime = 0;
        %bitmap.reset();
    }
};
function LoginGui::fadeOutBitmap(%this, %set, %img) {
    %bitmap = %this.getBitmap(%set, %img);
    if (%bitmap.done) {
        %bitmap.fadeInTime = 0;
        %bitmap.waitTime = 0;
        %bitmap.fadeOutTime = 1000;
        %bitmap.reset();
    }
};
function LoginGui::startAnimation(%this) {
    %this.firstAnimation = 1;
    %this.bitmapSet = 1;
    %this.schedule(1000, "animate");
};
function LoginGui::animate(%this) {
    if (!%this.firstAnimation) {
        %this.hideBitmapSet(%this.bitmapSet);
    }
    %this.firstAnimation = 0;
    %this.bitmapSet = (%this.bitmapSet == 0.0) ? 1 : 0;
    %this.schedule(500, "showBitmapSet", %this.bitmapSet);
    cancel(%this.animateTimer);
    %this.animateTimer = %this.schedule(45000, "animate");
};
function LoginGui::hideBitmap(%this, %set, %img) {
    %bitmap = %this.getBitmap(%set, %img);
    %bitmap.fadeInTime = 0;
    %bitmap.waitTime = 0;
    %bitmap.fadeOutTime = 1;
    %bitmap.reset();
};
function LoginGui::stopAnimation(%this) {
    %this.hideBitmap(0, 0);
    %this.hideBitmap(0, 1);
    %this.hideBitmap(0, 2);
    %this.hideBitmap(0, 3);
    %this.hideBitmap(1, 0);
    %this.hideBitmap(1, 1);
    %this.hideBitmap(1, 2);
    cancel(%this.animateTimer);
};
function LoginGui::showBitmapSet(%this, %set) {
    if ((%set == 0.0)) {
        %this.schedule(0, "fadeInBitmap", 0, 0);
        %this.schedule(1000, "fadeInBitmap", 0, 1);
        %this.schedule(2000, "fadeInBitmap", 0, 2);
        %this.schedule(3000, "fadeInBitmap", 0, 3);
    }
    %this.schedule(0, "fadeInBitmap", 1, 0);
    %this.schedule(1000, "fadeInBitmap", 1, 1);
    %this.schedule(2000, "fadeInBitmap", 1, 2);
};
function LoginGui::hideBitmapSet(%this, %set) {
    if ((%set == 0.0)) {
        %this.schedule(0, "fadeOutBitmap", 0, 0);
        %this.schedule(1000, "fadeOutBitmap", 0, 1);
        %this.schedule(2000, "fadeOutBitmap", 0, 2);
        %this.schedule(3000, "fadeOutBitmap", 0, 3);
    }
    %this.schedule(0, "fadeOutBitmap", 1, 0);
    %this.schedule(1000, "fadeOutBitmap", 1, 1);
    %this.schedule(2000, "fadeOutBitmap", 1, 2);
};
function checkForClientUpgrades() {
    if (($Platform $= "macos")) {
        log("login", "debug", "No support for starting upgrade from within OSX client.");
        return;
    }
    if (($Net::UpgradeToolAvailable == 0.0)) {
        return;
    }
    if (!isObject(LoginGui) || !LoginGui.isAwake()) {
        return;
    }
    if ($Net::upgradeAvailable) {
        MessageBoxOK("Upgrade is available ", "Press OK to start the upgrade process.", "clientVersion::startUpgrade();");
    }
    clientVersion::checkForUpgrades();
    schedule(120000, 0, checkForClientUpgrades);
};
