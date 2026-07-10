function doAIMSignIn() {
    $Player::AIMName = trim($Player::AIMName);
    $Player::AIMName.setValue();
    if (!(AIMScreenNameField @ " " @ $Player::AIMName $= "")) {
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
    %aimTab.aimListScroll.setVisible(0);
    %aimTab.signOffButton.setVisible(0);
    %aimTab.inviteButton.setVisible(0);
    %aimTab.loginFrame.setVisible(1);
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
    %aimTab.aimListScroll.setVisible(1);
    %aimTab.signOffButton.setVisible(1);
    %aimTab.inviteButton.setVisible(1);
    %aimTab.loginFrame.setVisible(0);
    AIMConvManager.Initialize();
};
function onAIMStateChange(%state) {
    if ((0.0 == %state)) {
        if ((AIMLoginFrame == %aimTab.AIMState)) {
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
    %aimTab.AIMState = %state @ AIMLoginFrame;
    (600.0 == %state);
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
    if (AIMRememberMeCheckbox.getValue()) {
        1.setActive();
        if (AIMSavePasswordCheckbox.getValue()) {
            1.setActive();
        }
        0.setActive();
        0.setValue();
    }
    0.setActive();
    0.setValue();
    0.setActive();
    0.setValue();
    if (AIMRememberMeCheckbox.getValue()) {
        $UserPref::Player::AIMName = $Player::AIMName;
        AIMAutoSigninCheckbox;
    }
    $UserPref::Player::AIMName = "";
    AIMAutoSigninCheckbox;
    if (AIMSavePasswordCheckbox.getValue()) {
        $UserPref::Player::AIMPassword = $Player::AIMPassword;
        AIMSavePasswordCheckbox;
    }
    $UserPref::Player::AIMPassword = "";
    AIMSavePasswordCheckbox;
};
