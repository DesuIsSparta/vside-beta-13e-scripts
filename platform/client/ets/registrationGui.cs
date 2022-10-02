$gHasOpenedRegistrationGui = 0;
function RegistrationGui::open(%this)
{
    Canvas.setContent(%this);
    pushScreenSize(640, 363, 0, 1, 1);
    %this.init();
    if (0)
    {
    }
    else
    {
        RegistrationPartnerLogo.setVisible(0);
    }
    $gHasOpenedRegistrationGui = 1;
    return ;
}
function RegistrationGui::haveIncompleteRegistration(%this)
{
    %ret = ((1 && isDefined("$Net::registrationID")) && !(($Net::RegistrationID $= ""))) && !hasField($UserPref::Login::completedRegistrations, $Net::RegistrationID);
    return %ret;
}
function RegistrationGui::tryOpenOrWebPage(%this)
{
    if (%this.haveIncompleteRegistration())
    {
        %this.open();
        %this.completeRegistration();
    }
    else
    {
        gotoWebPage($Net::ReregisterURL);
    }
    return ;
}
function RegistrationGui::init(%this)
{
    if (!%this.initialized)
    {
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
        RegistrationCenteredFrame.add(%this.waitIcon);
        %this.waitIcon.setVisible(0);
        %this.initialized = 1;
    }
    return ;
}
function RegistrationGui::completeRegistration(%this)
{
    %this.waitIcon.setVisible(1);
    %this.waitIcon.start();
    %request = sendRequest_CompleteClientRegistration($Net::RegistrationID, "onDoneOrErrorCallback_CompleteClientRegistration");
    geRegistrationStatusText.setValue("<spush><font:BauhausStd-Demi:20><just:center>Fetching your info..<spop>");
    return ;
}
function RegistrationGui::markCurrentRegistrationAsCompleted(%this)
{
    if (!%this.haveIncompleteRegistration())
    {
        return ;
    }
    $UserPref::Login::completedRegistrations = trim($UserPref::Login::completedRegistrations TAB $Net::RegistrationID);
    return ;
}
function onDoneOrErrorCallback_CompleteClientRegistration(%request)
{
    %this = RegistrationGui;
    %this.waitIcon.stop();
    %this.waitIcon.setVisible(0);
    if (%request.checkSuccess())
    {
        $UserPref::Player::Name = %request.getValue("userName");
        $UserPref::Player::Password = %request.getValue("password");
        $UserPref::Player::gender = %request.getValue("gender") $= "" ? $UserPref::Player::gender : %request.getValue("gender");
        $Player::Name = $UserPrefPlayer::Name;
        $Player::Password = $UserPrefPlayer::Password;
        LoginUserNameField.setValue($Player::Name);
        LoginPasswordField.setValue($Player::Password);
        %this.close();
        %analytic = getAnalytic();
        %analytic.trackPageView("/client/registration/success");
        LoginGui.doLoginButton();
    }
    else
    {
        %errorCode = %request.getValue("errorCode");
        %analytic = getAnalytic();
        %analytic.trackPageView("/client/registration/failed/" @ %errorCode);
        if (%errorCode $= "UNKNOWN_ID")
        {
            %errorMessage = $MsgCat::login["E-REG-UNKNOWN-ID"];
            error(getScopeName() SPC "- unknown registration ID -" SPC $Net::RegistrationID);
            RegistrationGui.markCurrentRegistrationAsCompleted();
            RegistrationGui.close();
        }
        else
        {
            if (%errorCode $= "INCOMPLETE")
            {
                %errorMessage = $MsgCat::login["E-REG-INCOMPLETE"];
            }
            else
            {
                %errorMessage = $MsgCat::login["E-REG-UNKNOWN"];
            }
        }
        %errorMessage = "<spush><font:BauhausStd-Demi:20><just:center>" @ %errorMessage @ "<spop>";
        geRegistrationStatusText.setValue(%errorMessage);
    }
    return ;
}
function RegistrationGui::close(%this)
{
    popScreenSize();
    Canvas.setContent(LoginGui);
    return ;
}
function RegistrationLink::onURL(%this, %url)
{
    if (%url $= "HAVE_ACCOUNT")
    {
        RegistrationGui.close();
    }
    else
    {
        if (%url $= "REREGISTER")
        {
            gotoWebPage($Net::ReregisterURL);
        }
        else
        {
            if (%url $= "FINISH_REGISTRATION")
            {
                gotoWebPage(standardSubstitutions($Net::FinishRegistrationURL));
            }
        }
    }
    return ;
}
