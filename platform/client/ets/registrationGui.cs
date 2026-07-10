$gHasOpenedRegistrationGui = 0;
function RegistrationGui::open(%this) {
    %this.setContent(Canvas);
    pushScreenSize(640, 363, 0, 1, 1);
    %this.init();
    if (0) {
    }
    0.setVisible(RegistrationPartnerLogo);
    $gHasOpenedRegistrationGui = 1;
};
function RegistrationGui::haveIncompleteRegistration(%this) {
    if (1) {
    }
    if (isDefined("$Net::registrationID")) {
    }
    if (!($Net::RegistrationID $= "")) {
    }
    %ret = !(hasField($UserPref::Login::completedRegistrations, $Net::RegistrationID));
    return %ret;
};
function RegistrationGui::tryOpenOrWebPage(%this) {
    if (%this.haveIncompleteRegistration()) {
        %this.open();
        %this.completeRegistration();
    }
    gotoWebPage($Net::ReregisterURL);
};
function RegistrationGui::init(%this) {
    if (!(%this.initialized)) {
        %this.waitIcon = AnimCtrl::newAnimCtrl("300 98", "18 18");
        60.setDelay(%this.waitIcon);
        "platform/client/ui/wait0.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait1.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait2.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait3.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait4.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait5.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait6.png".addFrame(%this.waitIcon);
        "platform/client/ui/wait7.png".addFrame(%this.waitIcon);
        %this.waitIcon.add(RegistrationCenteredFrame);
        0.setVisible(%this.waitIcon);
        %this.initialized = 1;
    }
};
function RegistrationGui::completeRegistration(%this) {
    1.setVisible(%this.waitIcon);
    %this.waitIcon.start();
    %request = sendRequest_CompleteClientRegistration($Net::RegistrationID, "onDoneOrErrorCallback_CompleteClientRegistration");
    "<spush><font:BauhausStd-Demi:20><just:center>Fetching your info..<spop>".setValue(geRegistrationStatusText);
};
function RegistrationGui::markCurrentRegistrationAsCompleted(%this) {
    if (!(%this.haveIncompleteRegistration())) {
        return;
    }
    $UserPref::Login::completedRegistrations = trim($UserPref::Login::completedRegistrations @ "\t" @ $Net::RegistrationID);
};
function onDoneOrErrorCallback_CompleteClientRegistration(%request) {
    '8';
    %this.waitIcon.stop();
    0.setVisible(%this.waitIcon);
    if (%request.checkSuccess()) {
        $UserPref::Player::Name = "userName".getValue(%request);
        RegistrationGui;
        $UserPref::Player::Password = "password".getValue(%request);
        if (("gender".getValue(%request) $= "")) {
        }
        $UserPref::Player::gender = "gender".getValue(%request);
        $UserPref::Player::gender;
        $Player::Name = $UserPrefPlayer::Name;
        $Player::Password = $UserPrefPlayer::Password;
        $Player::Name.setValue(LoginUserNameField);
        $Player::Password.setValue(LoginPasswordField);
        %this.close();
        %analytic = getAnalytic();
        "/client/registration/success".trackPageView(%analytic);
        LoginGui.doLoginButton();
    }
    %errorCode = "errorCode".getValue(%request);
    %analytic = getAnalytic();
    "/client/registration/failed/" @ %errorCode.trackPageView(%analytic);
    if ((%errorCode $= "UNKNOWN_ID")) {
        %errorMessage = %errorCode[$MsgCat::login @ "E-REG-UNKNOWN-ID"];
        error(getScopeName() @ " " @ "- unknown registration ID -" @ " " @ $Net::RegistrationID);
        RegistrationGui.markCurrentRegistrationAsCompleted();
        RegistrationGui.close();
    }
    if ((%errorCode $= "INCOMPLETE")) {
        %errorMessage = %errorCode[$MsgCat::login @ "E-REG-INCOMPLETE"];
    }
    %errorMessage = %errorMessage[$MsgCat::login @ "E-REG-UNKNOWN"];
    %errorMessage = "<spush><font:BauhausStd-Demi:20><just:center>" @ %errorMessage @ "<spop>";
    %errorMessage.setValue(geRegistrationStatusText);
};
function RegistrationGui::close(%this) {
    popScreenSize();
    LoginGui.setContent(Canvas);
};
function RegistrationLink::onURL(%this, %url) {
    if ((%url $= "HAVE_ACCOUNT")) {
        RegistrationGui.close();
    }
    if ((%url $= "REREGISTER")) {
        gotoWebPage($Net::ReregisterURL);
    }
    if ((%url $= "FINISH_REGISTRATION")) {
        gotoWebPage(standardSubstitutions($Net::FinishRegistrationURL));
    }
};
