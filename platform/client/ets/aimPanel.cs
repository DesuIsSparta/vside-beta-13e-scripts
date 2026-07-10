function doAIMSignIn() {
    $Player::AIMName = trim($Player::AIMName);
    $Player::AIMName.setValue();
    if (!(AIMScreenNameField SPC $Player::AIMName $= "")) {
        if ($UserPref::AIM::RememberMe) {
            $UserPref::Player::AIMName = $Player::AIMName;
            $UserPref::Player::AIMPassword = $Player::AIMPassword;
        }
        $UserPref::Player::AIMName = "";
        $UserPref::Player::AIMPassword = "";
        0.setActive();
        if ((0.0 == aimGetState())) {
            warn("Tried connecting to AIM when already connected.  Disconnecting.");
            aimDisconnect();
        }
        aimConnect($Player::AIMName, $Player::AIMPassword);
    }
};
function doAIMSignOff() {
    %aimTab = "AIM".getTabWithName();
    BuddyHudTabs;
    aimListScroll.setVisible(0);
    signOffButton.setVisible(0);
    inviteButton.setVisible(0);
    loginFrame.setVisible(1);
    1.setActive();
    aimDisconnect();
};
function silentAIMDisconnect() {
    if ((0.0 == aimGetState())) {
        aimDisconnect();
    }
};
function aimLoginCallback() {
    %aimTab = "AIM".getTabWithName();
    BuddyHudTabs;
    aimListScroll.setVisible(1);
    signOffButton.setVisible(1);
    inviteButton.setVisible(1);
    loginFrame.setVisible(0);
    Initialize();
};
function onAIMStateChange(%state) {
    if ((0.0 == %state)) {
        if ((AIMLoginFrame == AIMState)) {
            1.setActive();
            MessageBoxOK("AIM Login Failed", AIMSignInButton, "BuddyHudWin.open(); BuddyHudTabs.selectTabWithName(\"AIM\");");
        }
    }
    if ((50.0 == %state)) {
        1.setActive();
        MessageBoxOK("AIM Disconnected", AIMSignInButton, "BuddyHudWin.open(); BuddyHudTabs.selectTabWithName(\"AIM\");");
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
    AIMState = (600.0 == %state) @ %state @ AIMLoginFrame;
    200.0;
};
function AIMLoginFrame::setup(%this) {
    if ($UserPref::AIM::RememberMe) {
        $UserPref::Player::AIMName.setText();
        $UserPref::Player::AIMPassword.setText();
    }
    "".setText();
    "".setText();
    %this.update();
};
function AIMLoginFrame::update(%this) {
    if (getValue()) {
        1.setActive();
        if (getValue()) {
            1.setActive();
        }
        0.setActive();
        0.setValue();
    }
    0.setActive();
    0.setValue();
    0.setActive();
    0.setValue();
    if (getValue()) {
        $UserPref::Player::AIMName = $Player::AIMName;
        AIMRememberMeCheckbox;
    }
    $UserPref::Player::AIMName = "";
    AIMAutoSigninCheckbox;
    if (getValue()) {
        $UserPref::Player::AIMPassword = $Player::AIMPassword;
        AIMSavePasswordCheckbox;
    }
    $UserPref::Player::AIMPassword = "";
    AIMAutoSigninCheckbox;
};
