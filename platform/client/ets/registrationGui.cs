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
    if (!(%this.initialized)) {
        %this.waitIcon = AnimCtrl::newAnimCtrl("300 98", "18 18");
        %this.waitIcon.setDelay(60);
        %this.waitIcon.addFrame("platform/client/ui/wait0.png");
        %this.waitIcon.addFrame("platform/client/ui/wait1.png");
        %this.waitIcon.addFrame("platform/client/ui/wait2.png");
        %this.waitIcon.addFrame("platform/client/ui/wait3.png");
        %this.waitIcon.addFrame("platform/client/ui/wait4.png");
        %this.waitIcon.addFrame("platform/client/ui/wait5.png");
        %this.waitIcon.addFrame("platform/client/ui/wait6.png");
        %this.waitIcon.addFrame("platform/client/ui/wait7.png");
        %this.waitIcon.add();
        %this.waitIcon.setVisible(0);
        %this.initialized = RegistrationCenteredFrame @ 1;
    }
};
function RegistrationGui::completeRegistration(%this) {
    %this.waitIcon.setVisible(1);
    %this.waitIcon.start();
    %request = sendRequest_CompleteClientRegistration($Net::RegistrationID, "onDoneOrErrorCallback_CompleteClientRegistration");
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
    %this.waitIcon.stop();
    %this.waitIcon.setVisible(0);
    if (%request.checkSuccess()) {
        $UserPref::Player::Name = %request.getValue("userName");
        RegistrationGui;
        $UserPref::Player::Password = %request.getValue("password");
        if ((%request.getValue("gender") $= "")) {
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
        LoginGui.doLoginButton();
    }
    %errorCode = %request.getValue("errorCode");
    LoginUserNameField;
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/registration/failed/" @ %errorCode);
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
    %errorMessage.setValue();
};
function RegistrationGui::close(%this) {
    popScreenSize();
    Canvas.setContent(LoginGui);
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
