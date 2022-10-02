function doAIMSignIn()
{
    $Player::AIMName = trim($Player::AIMName);
    AIMScreenNameField.setValue($Player::AIMName);
    if (!($Player::AIMName $= ""))
    {
        if ($UserPref::AIM::RememberMe)
        {
            $UserPref::Player::AIMName = $Player::AIMName;
            $UserPref::Player::AIMPassword = $Player::AIMPassword;
        }
        else
        {
            $UserPref::Player::AIMName = "";
            $UserPref::Player::AIMPassword = "";
        }
        AIMSignInButton.setActive(0);
        if (aimGetState() == 0)
        {
            warn("Tried connecting to AIM when already connected.  Disconnecting.");
            aimDisconnect();
        }
        else
        {
            aimConnect($Player::AIMName, $Player::AIMPassword);
        }
    }
    return ;
}
function doAIMSignOff()
{
    %aimTab = BuddyHudTabs.getTabWithName("AIM");
    %aimTab.aimListScroll.setVisible(0);
    %aimTab.signOffButton.setVisible(0);
    %aimTab.inviteButton.setVisible(0);
    %aimTab.loginFrame.setVisible(1);
    AIMSignInButton.setActive(1);
    aimDisconnect();
    return ;
}
function silentAIMDisconnect()
{
    if (aimGetState() == 0)
    {
        aimDisconnect();
    }
    return ;
}
function aimLoginCallback()
{
    %aimTab = BuddyHudTabs.getTabWithName("AIM");
    %aimTab.aimListScroll.setVisible(1);
    %aimTab.signOffButton.setVisible(1);
    %aimTab.inviteButton.setVisible(1);
    %aimTab.loginFrame.setVisible(0);
    AIMConvManager.Initialize();
    return ;
}
function onAIMStateChange(%state)
{
    if (%state == 0)
    {
        if (AIMLoginFrame.AIMState == 200)
        {
            AIMSignInButton.setActive(1);
            MessageBoxOK("AIM Login Failed", $MsgCat::login["E-AIM-PASSWORD"], "BuddyHudWin.open(); BuddyHudTabs.selectTabWithName(\"AIM\");");
        }
    }
    else
    {
        if (%state == 50)
        {
            AIMSignInButton.setActive(1);
            MessageBoxOK("AIM Disconnected", $MsgCat::login["E-AIM-DISCONNECT"], "BuddyHudWin.open(); BuddyHudTabs.selectTabWithName(\"AIM\");");
        }
        else
        {
            if (%state == 100)
            {
                echo("AIM connecting");
            }
            else
            {
                if (%state == 150)
                {
                    echo("AIM challenging");
                }
                else
                {
                    if (%state == 200)
                    {
                        echo("AIM validating");
                    }
                    else
                    {
                        if (%state == 210)
                        {
                            echo("AIM secure ID");
                        }
                        else
                        {
                            if (%state == 211)
                            {
                                echo("AIM secure ID next key");
                            }
                            else
                            {
                                if (%state == 300)
                                {
                                    echo("AIM transferring");
                                }
                                else
                                {
                                    if (%state == 350)
                                    {
                                        echo("AIM negotiating");
                                    }
                                    else
                                    {
                                        if (%state == 400)
                                        {
                                            echo("AIM starting");
                                        }
                                        else
                                        {
                                            if (%state == 500)
                                            {
                                                echo("AIM online");
                                                aimLoginCallback();
                                            }
                                            else
                                            {
                                                if (%state == 600)
                                                {
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    AIMLoginFrame.AIMState = %state;
    return ;
}
function AIMLoginFrame::setup(%this)
{
    if ($UserPref::AIM::RememberMe)
    {
        AIMScreenNameField.setText($UserPref::Player::AIMName);
        AIMPasswordField.setText($UserPref::Player::AIMPassword);
    }
    else
    {
        AIMScreenNameField.setText("");
        AIMPasswordField.setText("");
    }
    %this.update();
    return ;
}
function AIMLoginFrame::update(%this)
{
    if (AIMRememberMeCheckbox.getValue())
    {
        AIMSavePasswordCheckbox.setActive(1);
        if (AIMSavePasswordCheckbox.getValue())
        {
            AIMAutoSigninCheckbox.setActive(1);
        }
        else
        {
            AIMAutoSigninCheckbox.setActive(0);
            AIMAutoSigninCheckbox.setValue(0);
        }
    }
    else
    {
        AIMSavePasswordCheckbox.setActive(0);
        AIMSavePasswordCheckbox.setValue(0);
        AIMAutoSigninCheckbox.setActive(0);
        AIMAutoSigninCheckbox.setValue(0);
    }
    if (AIMRememberMeCheckbox.getValue())
    {
        $UserPref::Player::AIMName = $Player::AIMName;
    }
    else
    {
        $UserPref::Player::AIMName = "";
    }
    if (AIMSavePasswordCheckbox.getValue())
    {
        $UserPref::Player::AIMPassword = $Player::AIMPassword;
    }
    else
    {
        $UserPref::Player::AIMPassword = "";
    }
    return ;
}
