function LoginGui::onWake(%this) {
    sendStatusRequest();
    %this.setControlsActive(1);
    0.setVisible();
    %this.displayPartnerInfo();
    clearScreenSizeStack();
    pushScreenSize(960, 544, 0, 1, 0);
    server = LoginProgressBarCtrls @ 0 @ WorldMap;
    $ServerName = "";
    loggedoutCleanup();
    if ($UserPref::Login::RememberMe) {
        $UserPref::Player::Name.setText();
        $UserPref::Player::Password.setText();
    }
    "".setText();
    "".setText();
    1.makeFirstResponder();
    selectAll();
    %colors = "<linkcolorhl:66aaff><linkcolor:ccddddff>";
    LoginUserNameField;
    LoginPasswordField @ LoginUserNameField @ LoginPasswordField @ LoginUserNameField @ LoginHelpLinks @ %colors @ "<a:gamelink " @ $Net::HelpURL_Guidelines @ "><just:right>User Guidelines</a> | <a:gamelink " @ $Net::HelpURL_General @ ">Help</a>".setText();
    %colors = "<linkcolorhl:66aaff><linkcolor:ccdddd80>";
    LoginUserNameField;
    LoginCreditsLink @ %colors @ "<a:gamelink CREDITS>Credits</a>".setText();
    if (haveIncompleteRegistration()) {
        %regText = "Complete Registration";
        RegistrationGui;
    }
    %regText = "Register";
    LoginRegistrationLinks @ %colors @ "<just:right><spush><b><a:gamelink REGISTER>" @ %regText @ "</a><spop> | <a:gamelink " @ $Net::ForgotPassURL @ ">Forgot Password</a>".setText();
    if (!(isObject())) {
        class = LoginPBController @ new ScriptObject(LoginPBController) @ "ProgressBarController";
    }
    "platform/client/ui/progress_empty".Initialize("platform/client/ui/progress_fill", "", "");
    %this.update(0);
    if ($UserPref::Login::firstRun) {
        if (isValidHostAddress($Net::DownloadHost)) {
            sendFirstLaunchRequest();
        }
        $UserPref::Login::firstRun = 0;
        LoginProgressHolder;
    }
    if (!($gHasOpenedRegistrationGui)) {
    }
    if (haveIncompleteRegistration()) {
        tryOpenOrWebPage();
    }
    checkForClientUpgrades();
    %analytic = getAnalytic();
    RegistrationGui;
    %analytic.requestNewSession();
};
function getStockPartnerShortcutText(%name) {
    %obj = %name.getPartnerObj();
    gLoginPartnersInfo;
    %vurl = vurl;
    %obj;
    return "Go straight to <spush><color:ddff55>" @ %obj @ extraLongName @ "<spop>: <a:" @ %vurl @ ">Click Here</a>" @ ".";
};
function initLoginPartners() {
    safeEnsureScriptObject("StringMap", "gLoginPartnersInfo");
    clear();
    %name = "doppelganger";
    gLoginPartnersInfo;
    %obj = %name.getOrMakePartnerObj();
    gLoginPartnersInfo;
    shortName = %name @ %obj;
    longName = "vSide" @ %obj;
    extraLongName = "vSide" @ %obj;
    vurl = "vside:/location/nv/MapSpawns_FUE" @ %obj;
    shortcutText = getStockPartnerShortcutText(%name) @ %obj;
    changesLoginScreen = 1 @ %obj;
    gatewayOptionBody = "<br>From here, you can check out a <spush><b>bumpin' party<spop><br>or chill out in <spush><b>your very own apartment<spop>." @ %obj;
    gatewayOptionButton1 = "Take me Clubbing!" @ %obj;
    gatewayOptionButton2 = "Take me to my place!" @ %obj;
    %name = "degrassi";
    %obj = %name.getOrMakePartnerObj();
    gLoginPartnersInfo;
    shortName = %name @ %obj;
    longName = "Degrassi" @ %obj;
    extraLongName = "The DOT Grill and DOT Dorms" @ %obj;
    vurl = "vside:/location/lga/MapSpawns_Degrassi" @ %obj;
    shortcutText = getStockPartnerShortcutText(%name) @ %obj;
    changesLoginScreen = 0 @ %obj;
    gatewayOptionBody = "<br>From here, you can check out <spush><b>the DOT Grill<spop><br>or chill out in your own room in <spush><b>the DOT Dorms<spop>." @ %obj;
    gatewayOptionButton1 = "Take me to the Grill!" @ %obj;
    gatewayOptionButton2 = "Take me to my Dorm!" @ %obj;
    %name = "";
    %obj = %name.getOrMakePartnerObj();
    gLoginPartnersInfo;
    shortName = "doppelganger".get() @ shortName @ %obj;
    gLoginPartnersInfo;
    longName = "doppelganger".get() @ longName @ %obj;
    gLoginPartnersInfo;
    extraLongName = "doppelganger".get() @ extraLongName @ %obj;
    gLoginPartnersInfo;
    vurl = "doppelganger".get() @ vurl @ %obj;
    gLoginPartnersInfo;
    shortcutText = "doppelganger".get() @ shortcutText @ %obj;
    gLoginPartnersInfo;
    changesLoginScreen = "doppelganger".get() @ changesLoginScreen @ %obj;
    gLoginPartnersInfo;
    gatewayOptionBody = "doppelganger".get() @ gatewayOptionBody @ %obj;
    gLoginPartnersInfo;
    gatewayOptionButton1 = "doppelganger".get() @ gatewayOptionButton1 @ %obj;
    gLoginPartnersInfo;
    gatewayOptionButton2 = "doppelganger".get() @ gatewayOptionButton2 @ %obj;
    gLoginPartnersInfo;
};
function gLoginPartnersInfo::getOrMakePartnerObj(%this, %shortName) {
    if (!(%this.hasKey(%shortName))) {
        %obj = safeNewScriptObject("ScriptObject", "", 0);
        %this.put(%shortName, %obj);
    }
    return %this.get(%shortName);
};
function gLoginPartnersInfo::getPartnerObj(%this, %shortName) {
    %obj = %this.get(%shortName);
    if (!(isObject(%obj))) {
        error(getScopeName(1) @ " " @ "- DNE:" @ " " @ %shortName);
        %obj = %this.get("");
    }
    return %obj;
};
initLoginPartners();
$gEnableStartHereMenu = 0;
function LoginGui::displayPartnerInfo(%this) {
    %partnerObj = $Net::userOwner.getPartnerObj();
    gLoginPartnersInfo;
    if (changesLoginScreen) {
        $gEnableStartHereMenu.setVisible();
        1.setVisible();
        LoginPartnerLogo @ "platform/client/ui/with_" @ $Net::userOwner.setBitmap();
        fitSize();
        clear();
        longName.add();
        "Map".add();
        selectUserPreferred();
    }
    0.setVisible();
    0.setVisible();
    clear();
};
function LoginStartHerePopup::selectUserPreferred(%this) {
    %size = %this.size();
    %i = 0;
    if ((%size < %i)) {
        if ((%this.getTextById(%i) $= $UserPref::Login::StartHere)) {
            %this.SetSelected(%i);
            return;
        }
        %i = (1.0 + %i);
    }
    %this.SetSelected(0);
};
function LoginStartHerePopup::onSelect(%this, %id, %entries) {
    $UserPref::Login::StartHere = %entries;
    if ((%entries $= "Degrassi")) {
        $VURLcmd = vurl;
        "degrassi".get();
    }
    log("initialization", "debug", "Disabled removal of the vSide address URL.");
};
function LoginRegistrationLinks::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    if ((%url $= "REGISTER")) {
        tryOpenOrWebPage();
    }
    if ((RegistrationGui SPC getSubStr(%url, 0, 7) $= "vside:/")) {
        vurlOperation(%url);
    }
    gotoWebPage(%url);
};
function LoginHelpLinks::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    if ((%url $= "REGISTER")) {
        tryOpenOrWebPage();
    }
    if ((RegistrationGui SPC getSubStr(%url, 0, 7) $= "vside:/")) {
        vurlOperation(%url);
    }
    gotoWebPage(%url);
};
function LoginCreditsLink::onURL(%this, %url) {
    doCredits();
};
function LoginGui::setControlsActive(%this, %flag) {
    controlsActive = %flag @ %this;
    %flag.setActive();
    %flag.setActive();
    if (%flag) {
        // unhandled opcode 1871 at 0x0000074C
    }
    // unhandled opcode 1584 at 0x00000750
    %flag = ETSLoginNoEditProfile;
    ETSLoginEditProfile;
    text = LoginUserNameField @ getValue() @ LoginUserNameField;
    LoginRememberMeCheckbox;
    %profile.setProfile();
    text = LoginPasswordField @ getValue() @ LoginPasswordField;
    LoginUserNameField;
    %profile.setProfile();
    %fr = getFirstResponder();
    Canvas;
    if (isObject(%fr)) {
        %fr.makeFirstResponder(0);
    }
};
function LoginGui::onCanvasResize(%this) {
    %this.update(0);
};
function LoginGui::doLoginButton(%this) {
    $Player::Name = getValue();
    LoginUserNameField;
    $Player::Password = getValue();
    LoginPasswordField;
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
        open();
        return loginDebugPanel;
    }
    %this.setControlsActive(0);
    1.setVisible();
    if ($UserPref::Login::RememberMe) {
        $UserPref::Player::Name = $Player::Name;
        LoginProgressBarCtrls;
        $UserPref::Player::Password = $Player::Password;
    }
    $UserPref::Player::Name = "";
    $UserPref::Player::Password = "";
    %this.envManagerLogin();
};
function LoginGui::envManagerLogin(%this) {
    firstTime = 1 @ BuddyHudWin;
    if (isObject()) {
        delete();
    }
    %loginRequest = new ManagerRequest(LoginRequest);
    LoginRequest;
    if (isObject()) {
        %loginRequest.add();
    }
    %url = MissionCleanup @ $Net::SecureClientServiceURL @ "/login?";
    MissionCleanup;
    %userValue = LoginRequest @ "user=" @ urlEncode($Player::Name);
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
    originalName = $Player::Name @ %loginRequest;
    1.setVisible();
    0.1.setValue();
    $Login::loggedIn = 0;
    LoginPBController;
    %loginRequest.start();
};
function LoginGui::onConnectFailed(%this, %msg) {
    sendStatusRequest();
    if ((%msg $= "")) {
        %msg = "Could not connect";
    }
    0.setVisible();
    0.setValue();
    %this.setControlsActive(1);
};
function LoginGui::nextControl(%this, %curControl) {
    %nextControl = "";
    if (LoginUserNameField) {
        // unhandled opcode 2163 at 0x00000A64
        %curControl.getName();
    }
    if (LoginPasswordField) {
        // unhandled opcode 2163 at 0x00000A7B
        %curControl.getName();
    }
    if ((%nextControl $= "")) {
        error("nextControl got invalid arg" @ " " @ %curControl);
        return;
    }
    if (LoginLoginButton) {
        $Login::newAccount = 0;
        %nextControl;
        doLoginButton();
    }
    %nextControl.makeFirstResponder(1);
    %nextControl.selectAll();
};
function LoginRequest::onError(%this, %errorNum, %unused) {
    if (($CURL::CouldNotResolveHost == %errorNum)) {
        "Could not reach server".onConnectFailed();
        MessageBoxOK("Could Not Find Server", LoginGui, "");
    }
    "Could not connect".onConnectFailed();
    MessageBoxOK("Could not connect", LoginGui @ "Could not connect to " @ $ETS::AppName @ " servers.  " @ $ETS::AppName[$MsgCat::network @ "H-SYS-DOWN"] @ "  " @ $ETS::AppName[$MsgCat::network @ "H-SYS-DOWN"][$MsgCat::network @ "H-SEE-FORUMS"], "");
};
function LoginRequest::onConnected(%this) {
    0.5.setValue();
};
function LoginRequest::onDone(%this) {
    1.setControlsActive();
    1.setValue();
    if (($HTTP::StatusOK != %this.statusCode())) {
        "Error communicating with server".onConnectFailed();
        log("communication", "error", LoginGui @ "client HTTP code: " @ %this.statusCode());
        MessageBoxOK("Server Unavailable", LoginPBController, "");
        return LoginGui;
    }
    %status = strlwr(findRequestStatus(%this));
    log("login", "debug", "LoginRequest::onDone status: " @ %status);
    if ((%status $= "fail")) {
    }
    if ((%status $= "error")) {
        %errorCode = %this.getValue("errorCode");
        %errorCode = strlwr(%errorCode);
        log("login", "error", "errorCode = " @ %errorCode);
        if ((%errorCode $= "invalid")) {
            "Wrong name or password".onConnectFailed();
            MessageBoxOK("Invalid Login", LoginGui, "");
        }
        if ((%errorCode $= "overloaded")) {
            "Overcrowded".onConnectFailed();
            MessageBoxOK("No More Room", LoginGui @ $ETS::AppName @ $ETS::AppName[$MsgCat::server @ "E-SERVER-FULL"], "");
        }
        if ((%errorCode $= "serverfail")) {
            "There was a server error".onConnectFailed();
            MessageBoxOK("Server Error", LoginGui, "");
        }
        if ((%errorCode $= "inactive")) {
            "Activation required".onConnectFailed();
            MessageBoxOK("Activation Required", LoginGui @ "You have not yet activated your account.  Check your email for the message with the activation link.  If you have not received an activation message, you can get another copy sent to you at <a:" @ $Net::ActivationURL @ ">the registration site</a>.", "");
        }
        if ((%errorCode $= "alreadyloggedin")) {
            "You are already logged in on another connection.".onConnectFailed();
            MessageBoxYesNo("Already Logged In", LoginGui, "LoginRequest::handleBoot();", "LoginRequest::cancelBoot();");
        }
        if ((%errorCode $= "banned")) {
            "Banned".onConnectFailed();
            MessageBoxOK("Banned", LoginGui, "");
        }
        if ((%errorCode $= "suspended")) {
            "Banned".onConnectFailed();
            %msg = "";
            LoginGui;
            %msg = %msg @ %msg[$MsgCat::login @ "E-SUSPENDED"];
            %msg = %msg @ "\n";
            %msg = %msg @ "\n";
            %msg = %msg @ %this.getValue("suspensionReason");
            %msg = strreplace(%msg, "[READTOU]", "");
            %msg = %msg @ "[READTOU]";
            %secs = %this.getValue("suspensionSecondsRemaining");
            %secs = ((60 % %secs) - (60.0 + %secs));
            %msg = %msg @ "\n";
            %msg = %msg @ "\n";
            %msg = %msg @ "Timeout Remaining:" @ " " @ secondsToDaysHoursMinutesSeconds(%secs);
            %msg = standardSubstitutions(%msg);
            MessageBoxOK("Suspended", %msg, "");
        }
        if ((%errorCode $= "upgrade_required")) {
            "Upgrade required".onConnectFailed();
            MessageBoxOK("Upgrade Required", LoginGui @ $ETS::AppName @ ".  " @ $ETS::AppName[$MsgCat::login @ "E-UPGRADE-2"], "");
        }
        "There was a server error".onConnectFailed();
        MessageBoxOK("Server Error", LoginGui, "");
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
    $Player::Name.forgetProperties();
    %cb = gUserPropMgrClient @ %this.getId() @ ".commonLogin_Part2();";
    $Player::Name.requestProperties(%cb);
    if (isObject()) {
        clear();
    }
    markCurrentRegistrationAsCompleted();
};
function LoginRequest::commonLogin_Part2(%this) {
    $Player::myPlaceVURL = "";
    %this.onGotUserProperties();
    outfits_init();
    outfits_retrieve();
    setNotConnectedToServer();
    onLogin();
    $Login::loggedIn = 1;
    geTGF;
    if (isObject()) {
        lastTabOpened = ClosetGui @ "" @ ClosetGui;
        WorldMap;
    }
    if (isObject()) {
        "".setBitmap();
    }
    $Player::attemptsToAutoUploadAvatarSnapshot = 0;
    ProfileCurrentPicture;
    $Player::hasSeenTakeAvatarPhotoDialog = 0;
    ProfileCurrentPicture;
    %vurl = getSkipMapVurl(0);
    if ((%vurl $= "")) {
        if ((1.0 != $Player::activated)) {
            if ((1.0 == $Player::hasEmail)) {
                if ($Login::newAccount) {
                    MessageBoxOK("Confirmation Sent", , "");
                }
                MessageBoxYesNo("Email Not Verified", , "gotoWebPage(\"" @ $Net::ActivationURL @ "\");", "");
            }
            if (!($Login::newAccount)) {
                MessageBoxYesNo("No Email Address", , "gotoWebPage(\"" @ $Net::AccountEditURL @ "\");", "");
            }
        }
    }
    %validCharacters = "abcdefghijklmnopqrstuvwxyz" @ "ABCDEFGHIJKLMNOPQRSTUVWXYZ" @ "0123456789-_ ";
    if (0) {
    }
    if (!(stripString($Player::Name, %validCharacters) $= $Player::Name)) {
        %msgBox = MessageBoxOK("USERNAME WARNING", "\n<b>" @ "\n", "");
        %msgBox.setWindowWidth(350);
    }
    destroySpaceInfo($CSSpaceInfo);
    $CSSpaceInfo = 0;
    if (isObject()) {
        "".setText();
        "".setText();
        if (initialized) {
            reset();
            reset();
            0.SetSelected();
        }
    }
    if (isObject()) {
        0.removeRowsByIndex(getRowCount());
        updateListeners();
    }
    if (isObject()) {
        0.removeRowsByIndex(getRowCount());
        updateListeners();
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
    $Player::Name.clearPropertyIfExists("favoriteActionsActionList");
    $Player::Name.clearPropertyIfExists("favoriteActionsKeyComboList");
    $UserPref::Audio::masterVolume = $Player::Name.getProperty("volumeMaster", $Defaults::UserPref::Audio::masterVolume);
    gUserPropMgrClient;
    $UserPref::Audio::channelVolume1 = $Player::Name.getProperty("volumeMusic", $Defaults::UserPref::Audio::channelVolume1);
    gUserPropMgrClient;
    $UserPref::Audio::channelVolume2 = $Player::Name.getProperty("volumeSfx", $Defaults::UserPref::Audio::channelVolume2);
    gUserPropMgrClient;
    $UserPref::Audio::mute = $Player::Name.getProperty("volumeMute", $Defaults::UserPref::Audio::mute);
    gUserPropMgrClient;
    $UserPref::Audio::NotifyChat = $Player::Name.getProperty("flashIncomingChat", $Defaults::UserPref::Audio::NotifyChat);
    gUserPropMgrClient;
    $UserPref::Audio::NotifyWhisper = $Player::Name.getProperty("flashIncomingWhisper", $Defaults::UserPref::Audio::NotifyWhisper);
    gUserPropMgrClient;
    $UserPref::Chat::ShowTyping = $Player::Name.getProperty("showTyping", $Defaults::UserPref::Chat::ShowTyping);
    gUserPropMgrClient;
    $UserPref::Display::farNameOpacity = $Player::Name.getProperty("farOpacity", $Defaults::UserPref::Display::farNameOpacity);
    gUserPropMgrClient;
    $UserPref::Display::hideChat = $Player::Name.getProperty("hideChat", $Defaults::UserPref::Display::hideChat);
    gUserPropMgrClient;
    $UserPref::Display::hideNames = $Player::Name.getProperty("hideNames", $Defaults::UserPref::Display::hideNames);
    gUserPropMgrClient;
    $UserPref::debug::alertOnLogWarning = $Player::Name.getProperty("alertOnLogWarning", $Defaults::UserPref::debug::alertOnLogWarning);
    gUserPropMgrClient;
    $UserPref::debug::alertOnLogError = $Player::Name.getProperty("alertOnLogError", $Defaults::UserPref::debug::alertOnLogError);
    gUserPropMgrClient;
    $UserPref::ETS::ButtonBar::AutoHide = $Player::Name.getProperty("hideButtonBar", $Defaults::UserPref::ETS::ButtonBar::AutoHide);
    gUserPropMgrClient;
    $UserPref::Player::TeleportBlock = $Player::Name.getProperty("refuseTeleports", $Defaults::UserPref::Player::TeleportBlock);
    gUserPropMgrClient;
    $UserPref::Player::WhisperBlock = $Player::Name.getProperty("refuseWhispers", $Defaults::UserPref::Player::WhisperBlock);
    gUserPropMgrClient;
    $UserPref::Player::YellBlock = $Player::Name.getProperty("refuseYells", $Defaults::UserPref::Player::YellBlock);
    gUserPropMgrClient;
    $UserPref::Player::EmotesPermissionFriends = $Player::Name.getProperty("emotesPermissionsFriends", $Defaults::UserPref::Player::EmotesPermissionFriends);
    gUserPropMgrClient;
    $UserPref::Player::EmotesPermissionStrangers = $Player::Name.getProperty("emotesPermissionsStrangers", $Defaults::UserPref::Player::EmotesPermissionStrangers);
    gUserPropMgrClient;
    $UserPref::Player::GiftsPermissionFriends = $Player::Name.getProperty("giftsPermissionsFriends", $Defaults::UserPref::Player::GiftsPermissionFriends);
    gUserPropMgrClient;
    $UserPref::Player::GiftsPermissionStrangers = $Player::Name.getProperty("giftsPermissionsStrangers", $Defaults::UserPref::Player::GiftsPermissionStrangers);
    gUserPropMgrClient;
    $UserPref::Player::awayMessage = $Player::Name.getProperty("awayMessage", $Pref::Player::defaultAwayMessage);
    gUserPropMgrClient;
    if (isObject()) {
        "".setText();
    }
    $UserPref::Player::autoReplyToWhispersWhenAway = $Player::Name.getProperty("autoReplyToWhipsers", $Defaults::UserPref::Player::autoReplyToWhispersWhenAway);
    gUserPropMgrClient;
    $UserPref::Player::filterProfanity = $Player::Name.getProperty("filterProfanity", $Defaults::UserPref::Player::filterProfanity);
    gUserPropMgrClient;
    $UserPref::Player::Genre = $Player::Name.getProperty("playerMood", "");
    gUserPropMgrClient;
    $UserPref::Player::height = $Player::Name.getProperty("avatarHeight", $Defaults::UserPref::Player::height);
    gUserPropMgrClient;
    $UserPref::Player::showOnRadar = $Player::Name.getProperty("showOnRadar", $Defaults::UserPref::Player::showOnRadar);
    gUserPropMgrClient;
    $UserPref::UI::FlashTaskBar = $Player::Name.getProperty("flashTaskBar", $Defaults::UserPref::UI::FlashTaskBar);
    gUserPropMgrClient;
    $UserPref::UI::Radar::AutoOpen = $Player::Name.getProperty("radarAutoOpen", $Defaults::UserPref::UI::Radar::AutoOpen);
    gUserPropMgrClient;
    $UserPref::UI::ShowAccountHud = $Player::Name.getProperty("showAccountHud", $Defaults::UserPref::UI::ShowAccountHud);
    gUserPropMgrClient;
    $UserPref::UI::ShowTooltips = $Player::Name.getProperty("showTooltips", $Defaults::UserPref::UI::ShowTooltips);
    gUserPropMgrClient;
    $UserPref::Video::Exposure = $Player::Name.getProperty("videoExposure", $Defaults::UserPref::Video::Exposure);
    gUserPropMgrClient;
    $UserPref::Video::exposureQualitySetting = $Player::Name.getProperty("videoExposureQuality", $Defaults::UserPref::Video::exposureQuality);
    gUserPropMgrClient;
    $UserPref::Video::shapeNameFontSize = $Player::Name.getProperty("videoNameSize", $Defaults::UserPref::Video::shapeNameFontSize);
    gUserPropMgrClient;
    $UserPref::Video::renderQualitySetting = $Player::Name.getProperty("videoRenderQuality", $Defaults::UserPref::Video::renderQuality);
    gUserPropMgrClient;
    $UserPref::Video::shadowQualitySetting = $Player::Name.getProperty("videoShadowQuality", $Defaults::UserPref::Video::shadowQuality);
    gUserPropMgrClient;
    $UserPref::Video::visibledistanceQualitySetting = $Player::Name.getProperty("videoVisibleDistance", $Defaults::UserPref::Video::visibledistanceQuality);
    gUserPropMgrClient;
    $UserPref::Video::waterreflectionQualitySetting = $Player::Name.getProperty("videoWaterReflection", $Defaults::UserPref::Video::waterreflectionQuality);
    gUserPropMgrClient;
    $UserPref::Video::ConstrainWindowDimensions = $Player::Name.getProperty("videoConstrainWindowDimensions", $Defaults::UserPref::Video::ConstrainWindowDimensions);
    gUserPropMgrClient;
    if ((DefaultAwayMsgEdit SPC $UserPref::Player::Genre $= "")) {
    }
    if (isObject($player)) {
        %rand = getRandom(0, 2);
        DefaultAwayMsgEdit;
        $UserPref::Player::Genre = getSubStr(possibleGenres, %rand, 1);
        $player.getDataBlock();
        echo("Chose random genre:" @ " " @ $UserPref::Player::Genre);
    }
    Music::setMuted($UserPref::Audio::mute);
    safeEnsureScriptObjectWithInit("StringMap", "EmoteBindingMap", "{ ignoreCase = true; }");
    clear();
    %maxNumberKeyCombos = getFieldCount($Defaults::UserPref::emotes::defaultKeyCombinations);
    EmoteBindingMap;
    %maxNumberKeyCombos[%numberOfUnboundKeyCombinations @ "f"] = 0;
    %maxNumberKeyCombos[%numberOfUnboundKeyCombinations @ "f"][%numberOfUnboundKeyCombinations @ "m"] = 0;
    %m = (1.0 - %maxNumberKeyCombos);
    if ((0.0 >= %m)) {
        %keyCombo = getField($Defaults::UserPref::emotes::defaultKeyCombinations, %m);
        %action = $Player::Name.getProperty(gUserPropMgrClient @ "favoriteActionsKey_f_" @ %keyCombo, -(1.0));
        if ((-(1.0) == %action)) {
        }
        if ((%action $= "")) {
            %action[%numberOfUnboundKeyCombinations @ "f"] = (1.0 + %action[%numberOfUnboundKeyCombinations @ "f"]);
            %keyCombo["" @ $UserPref::emotes TAB "f" @ %keyCombo] = ;
        }
        if (!(%keyCombo $= "")) {
            %action.put(%keyCombo);
            %keyCombo[EmoteBindingMap @ %action @ $UserPref::emotes TAB "f" @ %keyCombo] = ;
        }
        %action = $Player::Name.getProperty(gUserPropMgrClient @ "favoriteActionsKey_m_" @ %keyCombo, -(1.0));
        if ((-(1.0) == %action)) {
        }
        if ((%action $= "")) {
            %action[%numberOfUnboundKeyCombinations @ "m"] = (1.0 + %action[%numberOfUnboundKeyCombinations @ "m"]);
            %keyCombo["" @ $UserPref::emotes TAB "m" @ %keyCombo] = ;
        }
        if (!(%keyCombo $= "")) {
            %action.put(%keyCombo);
            %keyCombo[EmoteBindingMap @ %action @ $UserPref::emotes TAB "m" @ %keyCombo] = ;
        }
        %m = (1.0 - %m);
    }
    if ((EmoteBindingMap == size())) {
        setup();
    }
    populateLists();
    if ((%maxNumberKeyCombos == %maxNumberKeyCombos[%numberOfUnboundKeyCombinations @ "f"])) {
        %m = (1.0 - %maxNumberKeyCombos);
        EmoteHudList;
        if ((0.0 >= %m)) {
            %keyCombo = getField($Defaults::UserPref::emotes::defaultKeyCombinations, %m);
            EmoteHudList;
            %keyCombo[(0.0 >= %m) @ %keyCombo[0.0 @ $Defaults::UserPref::emotes TAB "f" @ %keyCombo] @ $UserPref::emotes TAB "f" @ %keyCombo] = ;
            %m = (1.0 - %m);
        }
    }
    if ((%maxNumberKeyCombos == %maxNumberKeyCombos[%numberOfUnboundKeyCombinations @ "m"])) {
        %m = (1.0 - %maxNumberKeyCombos);
        (0.0 >= %m);
        if ((0.0 >= %m)) {
            %keyCombo = getField($Defaults::UserPref::emotes::defaultKeyCombinations, %m);
            %keyCombo[%keyCombo[$Defaults::UserPref::emotes TAB "m" @ %keyCombo] @ $UserPref::emotes TAB "m" @ %keyCombo] = ;
            %m = (1.0 - %m);
        }
    }
};
function LoginRequest::handleBoot(%this) {
    sendRequest_BootNew("onDoneOrErrorCallback_Boot");
};
function onDoneOrErrorCallback_Boot(%request) {
    %status = %request.getResult("status");
    if ((%status $= "success")) {
        0.setControlsActive();
        envManagerLogin();
    }
    if ((LoginGui SPC %status $= "fail")) {
        error(LoginGui @ "client boot HTTP status: " @ %status);
        "Could not disconnect".onConnectFailed();
        MessageBoxOK("Could Not Disconnect", LoginGui, "");
    }
    if ((%status $= "error")) {
        error("client boot HTTP status: " @ %status);
        "There was a problem with the login server".onConnectFailed();
        MessageBoxOK("Problem Connecting", LoginGui, "");
    }
    error("client boot HTTP status: " @ %status);
    "Error communicating with server".onConnectFailed();
    MessageBoxOK("Server Unavailable", LoginGui, "");
};
function LoginGui::update(%this, %unused) {
    $gLoginStatusMessage.setText();
    0.setActive();
};
function LoginGui::getBitmap(%this, %set, %img) {
    return "LoginFader" @ %set @ "_" @ %img;
};
function LoginGui::fadeInBitmap(%this, %set, %img) {
    %bitmap = %this.getBitmap(%set, %img);
    if (done) {
        fadeInTime = %bitmap @ 1000 @ %bitmap;
        waitTime = 0 @ %bitmap;
        fadeOutTime = 0 @ %bitmap;
        %bitmap.reset();
    }
};
function LoginGui::fadeOutBitmap(%this, %set, %img) {
    %bitmap = %this.getBitmap(%set, %img);
    if (done) {
        fadeInTime = %bitmap @ 0 @ %bitmap;
        waitTime = 0 @ %bitmap;
        fadeOutTime = 1000 @ %bitmap;
        %bitmap.reset();
    }
};
function LoginGui::startAnimation(%this) {
    firstAnimation = 1 @ %this;
    bitmapSet = 1 @ %this;
    %this.schedule(1000, "animate");
};
function LoginGui::animate(%this) {
    if (!(firstAnimation)) {
        %this.hideBitmapSet(bitmapSet);
    }
    firstAnimation = %this @ 0 @ %this;
    %this;
    bitmapSet = 0.0 @ (%this == bitmapSet) ? 1 : 0 @ %this;
    %this.schedule(500, "showBitmapSet", bitmapSet);
    cancel(animateTimer);
    animateTimer = %this @ %this.schedule(45000, "animate") @ %this;
    %this;
};
function LoginGui::hideBitmap(%this, %set, %img) {
    %bitmap = %this.getBitmap(%set, %img);
    fadeInTime = 0 @ %bitmap;
    waitTime = 0 @ %bitmap;
    fadeOutTime = 1 @ %bitmap;
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
    cancel(animateTimer);
};
function LoginGui::showBitmapSet(%this, %set) {
    if ((0.0 == %set)) {
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
    if ((0.0 == %set)) {
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
    if ((0.0 == $Net::UpgradeToolAvailable)) {
        return;
    }
    if (!(isObject())) {
    }
    if (!(isAwake())) {
        return LoginGui;
    }
    if ($Net::upgradeAvailable) {
        MessageBoxOK("Upgrade is available ", "Press OK to start the upgrade process.", "clientVersion::startUpgrade();");
    }
    clientVersion::checkForUpgrades();
    schedule(120000, 0);
};
