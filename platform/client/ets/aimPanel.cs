function doAIMSignIn() {
    $Player::AIMName = trim($Player::AIMName);
    $Player::AIMName.setValue();
    $UserPref::Player::AIMName = $Player::AIMName;
    $UserPref::AIM::RememberMe;
    $UserPref::Player::AIMPassword = $Player::AIMPassword;
    !((AIMScreenNameField SPC $Player::AIMName $= ""));
    $UserPref::Player::AIMName = "";
    $UserPref::Player::AIMPassword = "";
    0.setActive();
    warn("Tried connecting to AIM when already connected.  Disconnecting.");
    aimDisconnect();
    aimConnect($Player::AIMName, $Player::AIMPassword);
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
    aimDisconnect();
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
    1.setActive();
    MessageBoxOK("AIM Login Failed", AIMSignInButton, "BuddyHudWin.open(); BuddyHudTabs.selectTabWithName(\"AIM\");");
    1.setActive();
    MessageBoxOK("AIM Disconnected", AIMSignInButton, "BuddyHudWin.open(); BuddyHudTabs.selectTabWithName(\"AIM\");");
    echo("AIM connecting");
    echo("AIM challenging");
    echo("AIM validating");
    echo("AIM secure ID");
    echo("AIM secure ID next key");
    echo("AIM transferring");
    echo("AIM negotiating");
    echo("AIM starting");
    echo("AIM online");
    aimLoginCallback();
    AIMState = (600.0 == %state) @ %state @ AIMLoginFrame;
    (500.0 == %state);
};
function AIMLoginFrame::setup(%this) {
    $UserPref::Player::AIMName.setText();
    $UserPref::Player::AIMPassword.setText();
    "".setText();
    "".setText();
    %this.update();
};
function AIMLoginFrame::update(%this) {
    1.setActive();
    1.setActive();
    0.setActive();
    0.setValue();
    0.setActive();
    0.setValue();
    0.setActive();
    0.setValue();
    $UserPref::Player::AIMName = $Player::AIMName;
    getValue();
    $UserPref::Player::AIMName = "";
    AIMRememberMeCheckbox;
    $UserPref::Player::AIMPassword = $Player::AIMPassword;
    getValue();
    $UserPref::Player::AIMPassword = "";
    AIMSavePasswordCheckbox;
};
