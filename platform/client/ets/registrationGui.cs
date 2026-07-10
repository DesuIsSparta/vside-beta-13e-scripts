$gHasOpenedRegistrationGui = 0;
function RegistrationGui::open(%this) {
    %this.setContent();
    pushScreenSize(640, 363, 0, 1, 1);
    %this.init();
    if (0) {
    }
    0.setVisible();
    $gHasOpenedRegistrationGui = 1;
    RegistrationPartnerLogo;
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
    if (!(initialized)) {
        waitIcon = %this @ AnimCtrl::newAnimCtrl("300 98", "18 18") @ %this;
        waitIcon.setDelay(60);
        waitIcon.addFrame("platform/client/ui/wait0.png");
        waitIcon.addFrame("platform/client/ui/wait1.png");
        waitIcon.addFrame("platform/client/ui/wait2.png");
        waitIcon.addFrame("platform/client/ui/wait3.png");
        waitIcon.addFrame("platform/client/ui/wait4.png");
        waitIcon.addFrame("platform/client/ui/wait5.png");
        waitIcon.addFrame("platform/client/ui/wait6.png");
        waitIcon.addFrame("platform/client/ui/wait7.png");
        waitIcon.add();
        waitIcon.setVisible(0);
        initialized = %this @ 1 @ %this;
        %this;
    }
};
function RegistrationGui::completeRegistration(%this) {
    waitIcon.setVisible(1);
    waitIcon.start();
    %request = sendRequest_CompleteClientRegistration($Net::RegistrationID, "onDoneOrErrorCallback_CompleteClientRegistration");
    %this;
    "<spush><font:BauhausStd-Demi:20><just:center>Fetching your info..<spop>".setValue();
};
function RegistrationGui::markCurrentRegistrationAsCompleted(%this) {
    if (!(%this.haveIncompleteRegistration())) {
        return;
    }
    $UserPref::Login::completedRegistrations = trim($UserPref::Login::completedRegistrations @ "\t" @ $Net::RegistrationID);
};
function onDoneOrErrorCallback_CompleteClientRegistration(%request) {
    '8';
    waitIcon.stop();
    waitIcon.setVisible(0);
    if (%request.checkSuccess()) {
        $UserPref::Player::Name = %request.getValue("userName");
        %this;
        $UserPref::Player::Password = %request.getValue("password");
        %this;
        if ((RegistrationGui SPC %request.getValue("gender") $= "")) {
        }
        $UserPref::Player::gender = %request.getValue("gender");
        $UserPref::Player::gender;
        $Player::Name = $UserPrefPlayer::Name;
        $Player::Password = $UserPrefPlayer::Password;
        $Player::Name.setValue();
        $Player::Password.setValue();
        %this.close();
        %analytic = getAnalytic();
        LoginPasswordField;
        %analytic.trackPageView("/client/registration/success");
        doLoginButton();
    }
    %errorCode = %request.getValue("errorCode");
    LoginGui;
    %analytic = getAnalytic();
    LoginUserNameField;
    %analytic.trackPageView("/client/registration/failed/" @ %errorCode);
    if ((%errorCode $= "UNKNOWN_ID")) {
        %errorMessage = %errorCode[$MsgCat::login @ "E-REG-UNKNOWN-ID"];
        error(getScopeName() @ " " @ "- unknown registration ID -" @ " " @ $Net::RegistrationID);
        markCurrentRegistrationAsCompleted();
        close();
    }
    if ((RegistrationGui SPC %errorCode $= "INCOMPLETE")) {
        %errorMessage = %errorCode[$MsgCat::login @ "E-REG-INCOMPLETE"];
        RegistrationGui;
    }
    %errorMessage = %errorMessage[$MsgCat::login @ "E-REG-UNKNOWN"];
    %errorMessage = "<spush><font:BauhausStd-Demi:20><just:center>" @ %errorMessage @ "<spop>";
    %errorMessage.setValue();
};
function RegistrationGui::close(%this) {
    popScreenSize();
    setContent();
};
function RegistrationLink::onURL(%this, %url) {
    if ((%url $= "HAVE_ACCOUNT")) {
        close();
    }
    if ((RegistrationGui SPC %url $= "REREGISTER")) {
        gotoWebPage($Net::ReregisterURL);
    }
    if ((%url $= "FINISH_REGISTRATION")) {
        gotoWebPage(standardSubstitutions($Net::FinishRegistrationURL));
    }
};
