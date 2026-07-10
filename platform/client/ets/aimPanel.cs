function doAIMSignIn() {
    $Player::AIMName = trim($Player::AIMName);
    $Player::AIMName.setValue(AIMScreenNameField);
    if (!($Player::AIMName $= "")) {
        if ($UserPref::AIM::RememberMe) {
            $UserPref::Player::AIMName = $Player::AIMName;
            $UserPref::Player::AIMPassword = $Player::AIMPassword;
        }
        $UserPref::Player::AIMName = "";
        $UserPref::Player::AIMPassword = "";
        0.setActive(AIMSignInButton);
        if ((aimGetState() == 0.0)) {
            warn("Tried connecting to AIM when already connected.  Disconnecting.");
            aimDisconnect();
        }
        aimConnect($Player::AIMName, $Player::AIMPassword);
    }
};
function doAIMSignOff() {
    %aimTab = "AIM".getTabWithName(BuddyHudTabs);
    0.setVisible(%aimTab.aimListScroll);
    0.setVisible(%aimTab.signOffButton);
    0.setVisible(%aimTab.inviteButton);
    1.setVisible(%aimTab.loginFrame);
    1.setActive(AIMSignInButton);
    aimDisconnect();
};
function silentAIMDisconnect() {
    if ((aimGetState() == 0.0)) {
        aimDisconnect();
    }
};
function aimLoginCallback() {
    %aimTab = "AIM".getTabWithName(BuddyHudTabs);
    1.setVisible(%aimTab.aimListScroll);
    1.setVisible(%aimTab.signOffButton);
    1.setVisible(%aimTab.inviteButton);
    0.setVisible(%aimTab.loginFrame);
    AIMConvManager.Initialize();
};
function onAIMStateChange(%state) {
    if ((%state == 0.0)) {
        if ((AIMLoginFrame.AIMState == 200.0)) {
            1.setActive(AIMSignInButton);
            MessageBoxOK("AIM Login Failed", $MsgCat::login["E-AIM-PASSWORD"], "BuddyHudWin.open(); BuddyHudTabs.selectTabWithName(\"AIM\");");
        }
    }
    if ((%state == 50.0)) {
        1.setActive(AIMSignInButton);
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
        $UserPref::Player::AIMName.setText(AIMScreenNameField);
        $UserPref::Player::AIMPassword.setText(AIMPasswordField);
    }
    "".setText(AIMScreenNameField);
    "".setText(AIMPasswordField);
    %this.update();
};
function AIMLoginFrame::update(%this) {
    if (AIMRememberMeCheckbox.getValue()) {
        1.setActive(AIMSavePasswordCheckbox);
        if (AIMSavePasswordCheckbox.getValue()) {
            1.setActive(AIMAutoSigninCheckbox);
        }
        0.setActive(AIMAutoSigninCheckbox);
        0.setValue(AIMAutoSigninCheckbox);
    }
    0.setActive(AIMSavePasswordCheckbox);
    0.setValue(AIMSavePasswordCheckbox);
    0.setActive(AIMAutoSigninCheckbox);
    0.setValue(AIMAutoSigninCheckbox);
    if (AIMRememberMeCheckbox.getValue()) {
        $UserPref::Player::AIMName = $Player::AIMName;
    }
    $UserPref::Player::AIMName = "";
    if (AIMSavePasswordCheckbox.getValue()) {
        $UserPref::Player::AIMPassword = $Player::AIMPassword;
    }
    $UserPref::Player::AIMPassword = "";
};
