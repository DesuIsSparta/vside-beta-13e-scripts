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
        if ((aimGetState() == 0.0)) {
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
    if ((aimGetState() == 0.0)) {
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
    if ((%state == 0.0)) {
        if ((AIMLoginFrame.AIMState == 200.0)) {
            AIMSignInButton.setActive(1);
            MessageBoxOK("AIM Login Failed", $MsgCat::login["E-AIM-PASSWORD"], "BuddyHudWin.open(); BuddyHudTabs.selectTabWithName(\"AIM\");");
        }
    }
    if ((%state == 50.0)) {
        AIMSignInButton.setActive(1);
        MessageBoxOK("AIM Disconnected", $MsgCat::login["E-AIM-DISCONNECT"], "BuddyHudWin.open(); BuddyHudTabs.selectTabWithName(\"AIM\");");
    }
    if ((%state == 100.0)) {
        echo("AIM connecting");
    }
    if ((%state == 150.0)) {
        echo("AIM challenging");
    }
    if ((%state == 200.0)) {
        echo("AIM validating");
    }
    if ((%state == 210.0)) {
        echo("AIM secure ID");
    }
    if ((%state == 211.0)) {
        echo("AIM secure ID next key");
    }
    if ((%state == 300.0)) {
        echo("AIM transferring");
    }
    if ((%state == 350.0)) {
        echo("AIM negotiating");
    }
    if ((%state == 400.0)) {
        echo("AIM starting");
    }
    if ((%state == 500.0)) {
        echo("AIM online");
        aimLoginCallback();
    }
    AIMLoginFrame.AIMState = (%state == 600.0) @ %state;
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
