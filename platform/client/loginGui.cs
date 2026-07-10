function LoginGui::onWake(%this) {
    sendStatusRequest();
    1.setControlsActive(%this);
    0.setVisible(LoginProgressBarCtrls);
    %this.displayPartnerInfo();
    clearScreenSizeStack();
    pushScreenSize(960, 544, 0, 1, 0);
    WorldMap.server = 0;
    $ServerName = "";
    loggedoutCleanup();
    if ($UserPref::Login::RememberMe) {
        $UserPref::Player::Name.setText(LoginUserNameField);
        $UserPref::Player::Password.setText(LoginPasswordField);
    }
    "".setText(LoginUserNameField);
    "".setText(LoginPasswordField);
    1.makeFirstResponder(LoginUserNameField);
    LoginUserNameField.selectAll();
    %colors = "<linkcolorhl:66aaff><linkcolor:ccddddff>";
    %colors @ "<a:gamelink " @ $Net::HelpURL_Guidelines @ "><just:right>User Guidelines</a> | <a:gamelink " @ $Net::HelpURL_General @ ">Help</a>".setText(LoginHelpLinks);
    %colors = "<linkcolorhl:66aaff><linkcolor:ccdddd80>";
    %colors @ "<a:gamelink CREDITS>Credits</a>".setText(LoginCreditsLink);
    if (RegistrationGui.haveIncompleteRegistration()) {
        %regText = "Complete Registration";
    }
    %regText = "Register";
    %colors @ "<just:right><spush><b><a:gamelink REGISTER>" @ %regText @ "</a><spop> | <a:gamelink " @ $Net::ForgotPassURL @ ">Forgot Password</a>".setText(LoginRegistrationLinks);
    if (!(isObject(LoginPBController))) {
        new ScriptObject(LoginPBController) {
            class = "ProgressBarController";
        };
    }
    "".Initialize(LoginPBController, LoginProgressHolder, "platform/client/ui/progress_empty", "platform/client/ui/progress_fill", "");
    0.update(%this);
    if ($UserPref::Login::firstRun) {
        if (isValidHostAddress($Net::DownloadHost)) {
            sendFirstLaunchRequest();
        }
        $UserPref::Login::firstRun = 0;
    }
    if (!($gHasOpenedRegistrationGui)) {
    }
    if (RegistrationGui.haveIncompleteRegistration()) {
        RegistrationGui.tryOpenOrWebPage();
    }
    checkForClientUpgrades();
    %analytic = getAnalytic();
    %analytic.requestNewSession();
};
function getStockPartnerShortcutText(%name) {
    %obj = %name.getPartnerObj(gLoginPartnersInfo);
    %vurl = %obj.vurl;
    return "Go straight to <spush><color:ddff55>" @ %obj.extraLongName @ "<spop>: <a:" @ %vurl @ ">Click Here</a>" @ ".";
};
function initLoginPartners() {
    safeEnsureScriptObject("StringMap", "gLoginPartnersInfo");
    gLoginPartnersInfo.clear();
    %name = "doppelganger";
    %obj = %name.getOrMakePartnerObj(gLoginPartnersInfo);
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
    %obj = %name.getOrMakePartnerObj(gLoginPartnersInfo);
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
    %obj = %name.getOrMakePartnerObj(gLoginPartnersInfo);
    %obj.shortName = "doppelganger".get(gLoginPartnersInfo).shortName;
    %obj.longName = "doppelganger".get(gLoginPartnersInfo).longName;
    %obj.extraLongName = "doppelganger".get(gLoginPartnersInfo).extraLongName;
    %obj.vurl = "doppelganger".get(gLoginPartnersInfo).vurl;
    %obj.shortcutText = "doppelganger".get(gLoginPartnersInfo).shortcutText;
    %obj.changesLoginScreen = "doppelganger".get(gLoginPartnersInfo).changesLoginScreen;
    %obj.gatewayOptionBody = "doppelganger".get(gLoginPartnersInfo).gatewayOptionBody;
    %obj.gatewayOptionButton1 = "doppelganger".get(gLoginPartnersInfo).gatewayOptionButton1;
    %obj.gatewayOptionButton2 = "doppelganger".get(gLoginPartnersInfo).gatewayOptionButton2;
};
function gLoginPartnersInfo::getOrMakePartnerObj(%this, %shortName) {
    if (!(%shortName.hasKey(%this))) {
        %obj = safeNewScriptObject("ScriptObject", "", 0);
        %obj.put(%this, %shortName);
    }
    return %shortName.get(%this);
};
function gLoginPartnersInfo::getPartnerObj(%this, %shortName) {
    %obj = %shortName.get(%this);
    if (!(isObject(%obj))) {
        error(getScopeName(1) @ " " @ "- DNE:" @ " " @ %shortName);
        %obj = "".get(%this);
    }
    return %obj;
};
initLoginPartners();
$gEnableStartHereMenu = 0;
function LoginGui::displayPartnerInfo(%this) {
    %partnerObj = $Net::userOwner.getPartnerObj(gLoginPartnersInfo);
    if (%partnerObj.changesLoginScreen) {
        $gEnableStartHereMenu.setVisible(LoginStartHereCtrls);
        1.setVisible(LoginPartnerLogo);
        "platform/client/ui/with_" @ $Net::userOwner.setBitmap(LoginPartnerLogo);
        LoginPartnerLogo.fitSize();
        LoginStartHerePopup.clear();
        %partnerObj.longName.add(LoginStartHerePopup);
        "Map".add(LoginStartHerePopup);
        LoginStartHerePopup.selectUserPreferred();
    }
    0.setVisible(LoginStartHereCtrls);
    0.setVisible(LoginPartnerLogo);
    LoginStartHerePopup.clear();
};
function LoginStartHerePopup::selectUserPreferred(%this) {
    %size = %this.size();
    %i = 0;
    while ((%i < %size)) {
        if ((%i.getTextById(%this) $= $UserPref::Login::StartHere)) {
            %i.SetSelected(%this);
            return;
        }
        %i = (%i + 1.0);
    }
    0.SetSelected(%this);
};
function LoginStartHerePopup::onSelect(%this, %id, %entries) {
    $UserPref::Login::StartHere = %entries;
    if ((%entries $= "Degrassi")) {
        $VURLcmd = "degrassi".get(gLoginPartnersInfo).vurl;
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
    %flag.setActive(LoginLoginButton);
    %flag.setActive(LoginRememberMeCheckbox);
    %profile = %flag ? ETSLoginEditProfile : ETSLoginNoEditProfile;
    LoginUserNameField.text = LoginUserNameField.getValue();
    %profile.setProfile(LoginUserNameField);
    LoginPasswordField.text = LoginPasswordField.getValue();
    %profile.setProfile(LoginPasswordField);
    %fr = Canvas.getFirstResponder();
    if (isObject(%fr)) {
        0.makeFirstResponder(%fr);
    }
};
function LoginGui::onCanvasResize(%this) {
    0.update(%this);
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
    0.setControlsActive(%this);
    1.setVisible(LoginProgressBarCtrls);
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
        %loginRequest.add(MissionCleanup);
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
    %url.setURL(%loginRequest);
    1.setProgress(%loginRequest);
    %loginRequest.originalName = $Player::Name;
    1.setVisible(LoginProgressBarCtrls);
    0.1.setValue(LoginPBController);
    $Login::loggedIn = 0;
    %loginRequest.start();
};
function LoginGui::onConnectFailed(%this, %msg) {
    sendStatusRequest();
    if ((%msg $= "")) {
        %msg = "Could not connect";
    }
    0.setVisible(LoginProgressBarCtrls);
    0.setValue(LoginPBController);
    1.setControlsActive(%this);
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
    1.makeFirstResponder(%nextControl);
    %nextControl.selectAll();
};
function LoginRequest::onError(%this, %errorNum, %unused) {
    if ((%errorNum == $CURL::CouldNotResolveHost)) {
        "Could not reach server".onConnectFailed(LoginGui);
        MessageBoxOK("Could Not Find Server", $MsgCat::network["E-SERVER-DNS"], "");
    }
    "Could not connect".onConnectFailed(LoginGui);
    MessageBoxOK("Could not connect", "Could not connect to " @ $ETS::AppName @ " servers.  " @ $ETS::AppName[$MsgCat::network @ "H-SYS-DOWN"] @ "  " @ $ETS::AppName[$MsgCat::network @ "H-SYS-DOWN"][$MsgCat::network @ "H-SEE-FORUMS"], "");
};
function LoginRequest::onConnected(%this) {
    0.5.setValue(LoginPBController);
};
function LoginRequest::onDone(%this) {
    1.setControlsActive(LoginGui);
    1.setValue(LoginPBController);
    if ((%this.statusCode() != $HTTP::StatusOK)) {
        "Error communicating with server".onConnectFailed(LoginGui);
        log("communication", "error", "client HTTP code: " @ %this.statusCode());
        MessageBoxOK("Server Unavailable", $MsgCat::network["E-SERVER-UNAVAIL"], "");
        return;
    }
    %status = strlwr(findRequestStatus(%this));
    log("login", "debug", "LoginRequest::onDone status: " @ %status);
    if ((%status $= "fail")) {
    }
    if ((%status $= "error")) {
        %errorCode = "errorCode".getValue(%this);
        %errorCode = strlwr(%errorCode);
        log("login", "error", "errorCode = " @ %errorCode);
        if ((%errorCode $= "invalid")) {
            "Wrong name or password".onConnectFailed(LoginGui);
            MessageBoxOK("Invalid Login", $MsgCat::login["E-PASSWORD"], "");
        }
        if ((%errorCode $= "overloaded")) {
            "Overcrowded".onConnectFailed(LoginGui);
            MessageBoxOK("No More Room", $ETS::AppName @ $ETS::AppName[$MsgCat::server @ "E-SERVER-FULL"], "");
        }
        if ((%errorCode $= "serverfail")) {
            "There was a server error".onConnectFailed(LoginGui);
            MessageBoxOK("Server Error", $MsgCat::login["E-UNKNOWN"], "");
        }
        if ((%errorCode $= "inactive")) {
            "Activation required".onConnectFailed(LoginGui);
            MessageBoxOK("Activation Required", "You have not yet activated your account.  Check your email for the message with the activation link.  If you have not received an activation message, you can get another copy sent to you at <a:" @ $Net::ActivationURL @ ">the registration site</a>.", "");
        }
        if ((%errorCode $= "alreadyloggedin")) {
            "You are already logged in on another connection.".onConnectFailed(LoginGui);
            MessageBoxYesNo("Already Logged In", $MsgCat::login["E-ALREADY-IN"], "LoginRequest::handleBoot();", "LoginRequest::cancelBoot();");
        }
        if ((%errorCode $= "banned")) {
            "Banned".onConnectFailed(LoginGui);
            MessageBoxOK("Banned", $MsgCat::login["E-BANNED"] @ $MsgCat::login["E-BANNED"][$MsgCat::login @ "E-DONT-KNOW-RULES"], "");
        }
        if ((%errorCode $= "suspended")) {
            "Banned".onConnectFailed(LoginGui);
            %msg = "";
            %msg = %msg @ %msg[$MsgCat::login @ "E-SUSPENDED"];
            %msg = %msg @ "\n";
            %msg = %msg @ "\n";
            %msg = %msg @ "suspensionReason".getValue(%this);
            %msg = strreplace(%msg, "[READTOU]", "");
            %msg = %msg @ "[READTOU]";
            %secs = "suspensionSecondsRemaining".getValue(%this);
            %secs = ((%secs + 60.0) - (%secs % 60));
            %msg = %msg @ "\n";
            %msg = %msg @ "\n";
            %msg = %msg @ "Timeout Remaining:" @ " " @ secondsToDaysHoursMinutesSeconds(%secs);
            %msg = standardSubstitutions(%msg);
            MessageBoxOK("Suspended", %msg, "");
        }
        if ((%errorCode $= "upgrade_required")) {
            "Upgrade required".onConnectFailed(LoginGui);
            MessageBoxOK("Upgrade Required", $MsgCat::login["E-UPGRADE-1"] @ $ETS::AppName @ ".  " @ $ETS::AppName[$MsgCat::login @ "E-UPGRADE-2"], "");
        }
        "There was a server error".onConnectFailed(LoginGui);
        MessageBoxOK("Server Error", $MsgCat::login["E-UNKNOWN"], "");
        %analytic = getAnalytic();
        "/client/login/error/" @ %errorCode.trackPageView(%analytic);
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
    "/client/login".trackPageView(%analytic);
    %this.parseResponse();
    $Player::Name.forgetProperties(gUserPropMgrClient);
    %cb = %this.getId() @ ".commonLogin_Part2();";
    %cb.requestProperties(gUserPropMgrClient, $Player::Name);
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
        "".setBitmap(ProfileCurrentPicture);
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
        if (!($Login::newAccount)) {
            MessageBoxYesNo("No Email Address", $MsgCat::login["NO-EMAIL-NAG"], "gotoWebPage(\"" @ $Net::AccountEditURL @ "\");", "");
        }
    }
    %validCharacters = "abcdefghijklmnopqrstuvwxyz" @ "ABCDEFGHIJKLMNOPQRSTUVWXYZ" @ "0123456789-_ ";
    if (0) {
    }
    if (!(stripString($Player::Name, %validCharacters) $= $Player::Name)) {
        %msgBox = MessageBoxOK("USERNAME WARNING", "\n<b>" @ $MsgCat::login["LEGACY-USERNAME-NAG"] @ "\n", "");
        350.setWindowWidth(%msgBox);
    }
    destroySpaceInfo($CSSpaceInfo);
    $CSSpaceInfo = 0;
    if (isObject(CSRulesAndDescWindow)) {
        "".setText(CSDescTaglineTextBox);
        "".setText(CSRulesPasswordField);
        if (CSRulesAndDescWindow.initialized) {
            CSRulesDescSavedIndicator.reset();
            CSRulesPasswordSavedIndicator.reset();
            0.SetSelected(CSRulesAccessPopup);
        }
    }
    if (isObject(geTGF_HotSpotsDataTable)) {
        geTGF_HotSpotsDataTable.getRowCount().removeRowsByIndex(geTGF_HotSpotsDataTable, 0);
        geTGF_HotSpotsDataTable.updateListeners();
    }
    if (isObject(geTGF_FriendsDataTable)) {
        geTGF_FriendsDataTable.getRowCount().removeRowsByIndex(geTGF_FriendsDataTable, 0);
        geTGF_FriendsDataTable.updateListeners();
    }
    sendBuddyListRequest("onDoneOrErrorCallback_GetUserRelations_ForHotSpots");
    getBalancesAndScores("checkPointsEarnedSinceLastLogin();");
};
$gLastLoggedInThisSessionAs = "";
function LoginRequest::parseResponse(%this) {
    log("login", "debug", "LoginRequest::parseResponse");
    $Token = "token".getValue(%this);
    log("login", "debug", "token: " @ $Token);
    %val = "gender".getValue(%this);
    if (!(%val $= "")) {
        $UserPref::Player::gender = %val;
    }
    warn(getScopeName() @ " " @ "- gender not returned.");
    $Player::Name = "registered_user".getValue(%this);
    if ($UserPref::Login::RememberMe) {
        $UserPref::Player::Name = $Player::Name;
    }
    $gLastLoggedInThisSessionAs = $Player::Name;
    eval("$Player::rolesMask      = " @ "rolesMask".getValue(%this) @ ";");
    $Player::hasEmail = "hasemail".getValueBool(%this);
    $Player::activated = "activated".getValueBool(%this);
    $Player::inviter = "inviter".getValue(%this);
    $Player::inviterOnline = "";
    $Player::inviterGender = "";
    clientHeartbeat();
};
function LoginRequest::onGotUserProperties(%this) {
    "favoriteActionsActionList".clearPropertyIfExists(gUserPropMgrClient, $Player::Name);
    "favoriteActionsKeyComboList".clearPropertyIfExists(gUserPropMgrClient, $Player::Name);
    $UserPref::Audio::masterVolume = $Defaults::UserPref::Audio::masterVolume.getProperty(gUserPropMgrClient, $Player::Name, "volumeMaster");
    $UserPref::Audio::channelVolume1 = $Defaults::UserPref::Audio::channelVolume1.getProperty(gUserPropMgrClient, $Player::Name, "volumeMusic");
    $UserPref::Audio::channelVolume2 = $Defaults::UserPref::Audio::channelVolume2.getProperty(gUserPropMgrClient, $Player::Name, "volumeSfx");
    $UserPref::Audio::mute = $Defaults::UserPref::Audio::mute.getProperty(gUserPropMgrClient, $Player::Name, "volumeMute");
    $UserPref::Audio::NotifyChat = $Defaults::UserPref::Audio::NotifyChat.getProperty(gUserPropMgrClient, $Player::Name, "flashIncomingChat");
    $UserPref::Audio::NotifyWhisper = $Defaults::UserPref::Audio::NotifyWhisper.getProperty(gUserPropMgrClient, $Player::Name, "flashIncomingWhisper");
    $UserPref::Chat::ShowTyping = $Defaults::UserPref::Chat::ShowTyping.getProperty(gUserPropMgrClient, $Player::Name, "showTyping");
    $UserPref::Display::farNameOpacity = $Defaults::UserPref::Display::farNameOpacity.getProperty(gUserPropMgrClient, $Player::Name, "farOpacity");
    $UserPref::Display::hideChat = $Defaults::UserPref::Display::hideChat.getProperty(gUserPropMgrClient, $Player::Name, "hideChat");
    $UserPref::Display::hideNames = $Defaults::UserPref::Display::hideNames.getProperty(gUserPropMgrClient, $Player::Name, "hideNames");
    $UserPref::debug::alertOnLogWarning = $Defaults::UserPref::debug::alertOnLogWarning.getProperty(gUserPropMgrClient, $Player::Name, "alertOnLogWarning");
    $UserPref::debug::alertOnLogError = $Defaults::UserPref::debug::alertOnLogError.getProperty(gUserPropMgrClient, $Player::Name, "alertOnLogError");
    $UserPref::ETS::ButtonBar::AutoHide = $Defaults::UserPref::ETS::ButtonBar::AutoHide.getProperty(gUserPropMgrClient, $Player::Name, "hideButtonBar");
    $UserPref::HudTabs::AutoOpen["music"] = $Defaults::UserPref::HudTabs::AutoOpen["music"].getProperty(gUserPropMgrClient, $Player::Name, "autoOpenTabMusic");
    $UserPref::HudTabs::AutoClose["music"] = $Defaults::UserPref::HudTabs::AutoClose["music"].getProperty(gUserPropMgrClient, $Player::Name, "autoCloseTabMusic");
    $UserPref::HudTabs::AutoOpen["affinity"] = $Defaults::UserPref::HudTabs::AutoOpen["affinity"].getProperty(gUserPropMgrClient, $Player::Name, "autoOpenTabAffinity");
    $UserPref::HudTabs::AutoClose["affinity"] = $Defaults::UserPref::HudTabs::AutoClose["affinity"].getProperty(gUserPropMgrClient, $Player::Name, "autoCloseTabAffinity");
    $UserPref::HudTabs::AutoOpen["scores"] = $Defaults::UserPref::HudTabs::AutoOpen["scores"].getProperty(gUserPropMgrClient, $Player::Name, "autoOpenTabScores");
    $UserPref::HudTabs::AutoClose["scores"] = $Defaults::UserPref::HudTabs::AutoClose["scores"].getProperty(gUserPropMgrClient, $Player::Name, "autoCloseTabScores");
    $UserPref::HudTabs::AutoOpen["word"] = $Defaults::UserPref::HudTabs::AutoOpen["word"].getProperty(gUserPropMgrClient, $Player::Name, "autoOpenTabWord");
    $UserPref::HudTabs::AutoClose["word"] = $Defaults::UserPref::HudTabs::AutoClose["word"].getProperty(gUserPropMgrClient, $Player::Name, "autoCloseTabWord");
    $UserPref::Player::TeleportBlock = $Defaults::UserPref::Player::TeleportBlock.getProperty(gUserPropMgrClient, $Player::Name, "refuseTeleports");
    $UserPref::Player::WhisperBlock = $Defaults::UserPref::Player::WhisperBlock.getProperty(gUserPropMgrClient, $Player::Name, "refuseWhispers");
    $UserPref::Player::YellBlock = $Defaults::UserPref::Player::YellBlock.getProperty(gUserPropMgrClient, $Player::Name, "refuseYells");
    $UserPref::Player::EmotesPermissionFriends = $Defaults::UserPref::Player::EmotesPermissionFriends.getProperty(gUserPropMgrClient, $Player::Name, "emotesPermissionsFriends");
    $UserPref::Player::EmotesPermissionStrangers = $Defaults::UserPref::Player::EmotesPermissionStrangers.getProperty(gUserPropMgrClient, $Player::Name, "emotesPermissionsStrangers");
    $UserPref::Player::GiftsPermissionFriends = $Defaults::UserPref::Player::GiftsPermissionFriends.getProperty(gUserPropMgrClient, $Player::Name, "giftsPermissionsFriends");
    $UserPref::Player::GiftsPermissionStrangers = $Defaults::UserPref::Player::GiftsPermissionStrangers.getProperty(gUserPropMgrClient, $Player::Name, "giftsPermissionsStrangers");
    $UserPref::Player::awayMessage = $Pref::Player::defaultAwayMessage.getProperty(gUserPropMgrClient, $Player::Name, "awayMessage");
    if (isObject(DefaultAwayMsgEdit)) {
        "".setText(DefaultAwayMsgEdit);
    }
    $UserPref::Player::autoReplyToWhispersWhenAway = $Defaults::UserPref::Player::autoReplyToWhispersWhenAway.getProperty(gUserPropMgrClient, $Player::Name, "autoReplyToWhipsers");
    $UserPref::Player::filterProfanity = $Defaults::UserPref::Player::filterProfanity.getProperty(gUserPropMgrClient, $Player::Name, "filterProfanity");
    $UserPref::Player::Genre = "".getProperty(gUserPropMgrClient, $Player::Name, "playerMood");
    $UserPref::Player::height = $Defaults::UserPref::Player::height.getProperty(gUserPropMgrClient, $Player::Name, "avatarHeight");
    $UserPref::Player::showOnRadar = $Defaults::UserPref::Player::showOnRadar.getProperty(gUserPropMgrClient, $Player::Name, "showOnRadar");
    $UserPref::UI::FlashTaskBar = $Defaults::UserPref::UI::FlashTaskBar.getProperty(gUserPropMgrClient, $Player::Name, "flashTaskBar");
    $UserPref::UI::Radar::AutoOpen = $Defaults::UserPref::UI::Radar::AutoOpen.getProperty(gUserPropMgrClient, $Player::Name, "radarAutoOpen");
    $UserPref::UI::ShowAccountHud = $Defaults::UserPref::UI::ShowAccountHud.getProperty(gUserPropMgrClient, $Player::Name, "showAccountHud");
    $UserPref::UI::ShowTooltips = $Defaults::UserPref::UI::ShowTooltips.getProperty(gUserPropMgrClient, $Player::Name, "showTooltips");
    $UserPref::Video::Exposure = $Defaults::UserPref::Video::Exposure.getProperty(gUserPropMgrClient, $Player::Name, "videoExposure");
    $UserPref::Video::exposureQualitySetting = $Defaults::UserPref::Video::exposureQuality.getProperty(gUserPropMgrClient, $Player::Name, "videoExposureQuality");
    $UserPref::Video::shapeNameFontSize = $Defaults::UserPref::Video::shapeNameFontSize.getProperty(gUserPropMgrClient, $Player::Name, "videoNameSize");
    $UserPref::Video::renderQualitySetting = $Defaults::UserPref::Video::renderQuality.getProperty(gUserPropMgrClient, $Player::Name, "videoRenderQuality");
    $UserPref::Video::shadowQualitySetting = $Defaults::UserPref::Video::shadowQuality.getProperty(gUserPropMgrClient, $Player::Name, "videoShadowQuality");
    $UserPref::Video::visibledistanceQualitySetting = $Defaults::UserPref::Video::visibledistanceQuality.getProperty(gUserPropMgrClient, $Player::Name, "videoVisibleDistance");
    $UserPref::Video::waterreflectionQualitySetting = $Defaults::UserPref::Video::waterreflectionQuality.getProperty(gUserPropMgrClient, $Player::Name, "videoWaterReflection");
    $UserPref::Video::ConstrainWindowDimensions = $Defaults::UserPref::Video::ConstrainWindowDimensions.getProperty(gUserPropMgrClient, $Player::Name, "videoConstrainWindowDimensions");
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
        %action = -(1.0).getProperty(gUserPropMgrClient, $Player::Name, "favoriteActionsKey_f_" @ %keyCombo);
        if ((%action == -(1.0))) {
        }
        if ((%action $= "")) {
            %action[%numberOfUnboundKeyCombinations @ "f"] = (%action[%numberOfUnboundKeyCombinations @ "f"] + 1.0);
            %keyCombo["" @ $UserPref::emotes TAB "f" @ %keyCombo] = ;
        }
        if (!(%keyCombo $= "")) {
            %keyCombo.put(EmoteBindingMap, %action);
            %keyCombo[%action @ $UserPref::emotes TAB "f" @ %keyCombo] = ;
        }
        %action = -(1.0).getProperty(gUserPropMgrClient, $Player::Name, "favoriteActionsKey_m_" @ %keyCombo);
        if ((%action == -(1.0))) {
        }
        if ((%action $= "")) {
            %action[%numberOfUnboundKeyCombinations @ "m"] = (%action[%numberOfUnboundKeyCombinations @ "m"] + 1.0);
            %keyCombo["" @ $UserPref::emotes TAB "m" @ %keyCombo] = ;
        }
        if (!(%keyCombo $= "")) {
            %keyCombo.put(EmoteBindingMap, %action);
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
    %status = "status".getResult(%request);
    if ((%status $= "success")) {
        0.setControlsActive(LoginGui);
        LoginGui.envManagerLogin();
    }
    if ((%status $= "fail")) {
        error("client boot HTTP status: " @ %status);
        "Could not disconnect".onConnectFailed(LoginGui);
        MessageBoxOK("Could Not Disconnect", $MsgCat::login["E-DISCONNECT"], "");
    }
    if ((%status $= "error")) {
        error("client boot HTTP status: " @ %status);
        "There was a problem with the login server".onConnectFailed(LoginGui);
        MessageBoxOK("Problem Connecting", $MsgCat::login["E-CONNECT"], "");
    }
    error("client boot HTTP status: " @ %status);
    "Error communicating with server".onConnectFailed(LoginGui);
    MessageBoxOK("Server Unavailable", $MsgCat::network["E-SERVER-UNAVAIL"], "");
};
function LoginGui::update(%this, %unused) {
    $gLoginStatusMessage.setText(LoginSystemStatusText);
    0.setActive(LoginSystemStatusButton);
};
function LoginGui::getBitmap(%this, %set, %img) {
    return "LoginFader" @ %set @ "_" @ %img;
};
function LoginGui::fadeInBitmap(%this, %set, %img) {
    %bitmap = %img.getBitmap(%this, %set);
    if (%bitmap.done) {
        %bitmap.fadeInTime = 1000;
        %bitmap.waitTime = 0;
        %bitmap.fadeOutTime = 0;
        %bitmap.reset();
    }
};
function LoginGui::fadeOutBitmap(%this, %set, %img) {
    %bitmap = %img.getBitmap(%this, %set);
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
    "animate".schedule(%this, 1000);
};
function LoginGui::animate(%this) {
    if (!(%this.firstAnimation)) {
        %this.bitmapSet.hideBitmapSet(%this);
    }
    %this.firstAnimation = 0;
    %this.bitmapSet = (%this.bitmapSet == 0.0) ? 1 : 0;
    %this.bitmapSet.schedule(%this, 500, "showBitmapSet");
    cancel(%this.animateTimer);
    %this.animateTimer = "animate".schedule(%this, 45000);
};
function LoginGui::hideBitmap(%this, %set, %img) {
    %bitmap = %img.getBitmap(%this, %set);
    %bitmap.fadeInTime = 0;
    %bitmap.waitTime = 0;
    %bitmap.fadeOutTime = 1;
    %bitmap.reset();
};
function LoginGui::stopAnimation(%this) {
    0.hideBitmap(%this, 0);
    1.hideBitmap(%this, 0);
    2.hideBitmap(%this, 0);
    3.hideBitmap(%this, 0);
    0.hideBitmap(%this, 1);
    1.hideBitmap(%this, 1);
    2.hideBitmap(%this, 1);
    cancel(%this.animateTimer);
};
function LoginGui::showBitmapSet(%this, %set) {
    if ((%set == 0.0)) {
        0.schedule(%this, 0, "fadeInBitmap", 0);
        1.schedule(%this, 1000, "fadeInBitmap", 0);
        2.schedule(%this, 2000, "fadeInBitmap", 0);
        3.schedule(%this, 3000, "fadeInBitmap", 0);
    }
    0.schedule(%this, 0, "fadeInBitmap", 1);
    1.schedule(%this, 1000, "fadeInBitmap", 1);
    2.schedule(%this, 2000, "fadeInBitmap", 1);
};
function LoginGui::hideBitmapSet(%this, %set) {
    if ((%set == 0.0)) {
        0.schedule(%this, 0, "fadeOutBitmap", 0);
        1.schedule(%this, 1000, "fadeOutBitmap", 0);
        2.schedule(%this, 2000, "fadeOutBitmap", 0);
        3.schedule(%this, 3000, "fadeOutBitmap", 0);
    }
    0.schedule(%this, 0, "fadeOutBitmap", 1);
    1.schedule(%this, 1000, "fadeOutBitmap", 1);
    2.schedule(%this, 2000, "fadeOutBitmap", 1);
};
function checkForClientUpgrades() {
    if (($Platform $= "macos")) {
        log("login", "debug", "No support for starting upgrade from within OSX client.");
        return;
    }
    if (($Net::UpgradeToolAvailable == 0.0)) {
        return;
    }
    if (!(isObject(LoginGui))) {
    }
    if (!(LoginGui.isAwake())) {
        return;
    }
    if ($Net::upgradeAvailable) {
        MessageBoxOK("Upgrade is available ", "Press OK to start the upgrade process.", "clientVersion::startUpgrade();");
    }
    clientVersion::checkForUpgrades();
    schedule(120000, 0, checkForClientUpgrades);
};
