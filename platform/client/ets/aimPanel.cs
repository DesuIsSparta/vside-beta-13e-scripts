function doAIMSignIn() {
    $Player::AIMName = trim($Player::AIMName);
    AIMScreenNameField.setValue($Player::AIMName);
    if (!($Player::AIMName $= "")) {
        if ($UserPref::AIM::RememberMe) {
            $UserPref::Player::AIMName = $Player::AIMName;
            $UserPref::Player::AIMPassword = $Player::AIMPassword;
        }
        $UserPref::Player::AIMName = "";
        $UserPref::Player::AIMPassword = "";
        AIMSignInButton.setActive(0);
        if ((0.0 == aimGetState())) {
            warn("Tried connecting to AIM when already connected.  Disconnecting.");
            aimDisconnect();
        }
        aimConnect($Player::AIMName, $Player::AIMPassword);
    }
};
function doAIMSignOff() {
    %aimTab = BuddyHudTabs.getTabWithName("AIM");
    %aimTab.aimListScroll.setVisible(0);
    %aimTab.signOffButton.setVisible(0);
    %aimTab.inviteButton.setVisible(0);
    %aimTab.loginFrame.setVisible(1);
    AIMSignInButton.setActive(1);
    aimDisconnect();
};
function silentAIMDisconnect() {
    if ((0.0 == aimGetState())) {
        aimDisconnect();
    }
};
function aimLoginCallback() {
    %aimTab = BuddyHudTabs.getTabWithName("AIM");
    %aimTab.aimListScroll.setVisible(1);
    %aimTab.signOffButton.setVisible(1);
    %aimTab.inviteButton.setVisible(1);
    %aimTab.loginFrame.setVisible(0);
    AIMConvManager.Initialize();
};
function onAIMStateChange(%state) {
    if ((0.0 == %state)) {
        if ((AIMLoginFrame == %aimTab.AIMState)) {
            AIMSignInButton.setActive(1);
            MessageBoxOK("AIM Login Failed", 200.0, "BuddyHudWin.open(); BuddyHudTabs.selectTabWithName(\"AIM\");");
        }
    }
    if ((50.0 == %state)) {
        AIMSignInButton.setActive(1);
        MessageBoxOK("AIM Disconnected", , "BuddyHudWin.open(); BuddyHudTabs.selectTabWithName(\"AIM\");");
    }
    if ((100.0 == %state)) {
        echo("AIM connecting");
    }
    if ((150.0 == %state)) {
        echo("AIM challenging");
    }
    if ((200.0 == %state)) {
        echo("AIM validating");
    }
    if ((210.0 == %state)) {
        echo("AIM secure ID");
    }
    if ((211.0 == %state)) {
        echo("AIM secure ID next key");
    }
    if ((300.0 == %state)) {
        echo("AIM transferring");
    }
    if ((350.0 == %state)) {
        echo("AIM negotiating");
    }
    if ((400.0 == %state)) {
        echo("AIM starting");
    }
    if ((500.0 == %state)) {
        echo("AIM online");
        aimLoginCallback();
    }
    %aimTab.AIMState = %state @ AIMLoginFrame;
    (600.0 == %state);
};
function AIMLoginFrame::setup(%this) {
    if ($UserPref::AIM::RememberMe) {
        AIMScreenNameField.setText($UserPref::Player::AIMName);
        AIMPasswordField.setText($UserPref::Player::AIMPassword);
    }
    AIMScreenNameField.setText("");
    AIMPasswordField.setText("");
    %this.update();
};
function AIMLoginFrame::update(%this) {
    if (AIMRememberMeCheckbox.getValue()) {
        AIMSavePasswordCheckbox.setActive(1);
        if (AIMSavePasswordCheckbox.getValue()) {
            AIMAutoSigninCheckbox.setActive(1);
        }
        AIMAutoSigninCheckbox.setActive(0);
        AIMAutoSigninCheckbox.setValue(0);
    }
    AIMSavePasswordCheckbox.setActive(0);
    AIMSavePasswordCheckbox.setValue(0);
    AIMAutoSigninCheckbox.setActive(0);
    AIMAutoSigninCheckbox.setValue(0);
    if (AIMRememberMeCheckbox.getValue()) {
        $UserPref::Player::AIMName = $Player::AIMName;
    }
    $UserPref::Player::AIMName = "";
    if (AIMSavePasswordCheckbox.getValue()) {
        $UserPref::Player::AIMPassword = $Player::AIMPassword;
    }
    $UserPref::Player::AIMPassword = "";
};
