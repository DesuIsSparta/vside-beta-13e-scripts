function CSRulesAndDescWindow::toggle(%this) {
    %this.close();
    %this.open();
};
function CSRulesAndDescWindow::open(%this) {
    %this.setup();
    closeCSPanelsInOtherCategories(%this);
    %this.setVisible(1);
    %this.focusAndRaise();
    update();
    CustomSpaceClient::checkEditingSpace();
    lastValueSaved.setValue();
};
function CSRulesAndDescWindow::close(%this) {
    %this.setVisible(0);
    CustomSpaceClient::checkEditingSpace();
    focusTopWindow();
    update();
    return 1;
};
function CSRulesAndDescWindow::setup(%this) {
    initializing = !(initializing) @ 1 @ %this;
    %this;
    "Open".add();
    "Friends Only".add();
    "Door Code".add();
    0.SetSelected();
    SavableTextStatusIndicatorCreator::make("CSRulesDescSavedIndicator", "210 72", "CSRulesAndDescWindow.update();", 0, "right");
    %this.add();
    SavableTextStatusIndicatorCreator::make("CSRulesPasswordSavedIndicator", "210 125", "CSRulesAndDescWindow.update();", 1, "right");
    %this.add();
    %this.update();
    initialized = CSRulesPasswordSavedIndicator @ 1 @ %this;
    CSRulesPasswordField;
};
function CSRulesAndDescWindow::descriptionChanged(%this) {
    %this.saveDescriptionSettings();
};
function CSRulesAndDescWindow::saveDescriptionSettings(%this) {
    %this.update();
    CustomSpacesClient::setMap2DText();
    CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), getValue(), "", "", "", "");
};
function CSRulesAndDescWindow::saveRulesSettings(%this) {
    %this.update();
    %access = getWord($gCSRulesAccessCodes, GetSelected());
    CSRulesAccessPopup;
    %doorCode = getValue();
    CSRulesPasswordField;
    CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), "", "OPEN", "", "", "");
    CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), "", %access, %doorCode, "", "");
};
function CSRulesAndDescWindow::checkSaveRulesSettings(%this) {
    %this.saveRulesSettings();
};
$gCSRulesAccessCodes = "OPEN FRIENDSONLY PASSWORDPROTECTED LOCKED";
function CSRulesAndDescWindow::updateSettings(%this, %accessMode, %password, %description) {
    %this.setup();
    %description.setInitialValue();
    %description.setText();
    %password.setText();
    %password.setInitialValue();
    accessLevel = CSRulesPasswordSavedIndicator @ strupr(%accessMode) @ %this;
    CSRulesPasswordField;
    0.SetSelected();
    1.SetSelected();
    2.SetSelected();
    2.SetSelected();
    accessLevel = CSRulesAccessPopup @ "OPEN" @ %this;
    (%this == findWord($gCSRulesAccessCodes, accessLevel));
    %this.update();
};
function CSRulesAndDescWindow::update(%this) {
    %this.setup();
    %flag = (CSRulesAccessPopup == GetSelected());
    findWord($gCSRulesAccessCodes, "PASSWORDPROTECTED");
    !(%flag).setVisible();
    %flag.setVisible();
    text = CSRulesPasswordField @ getValue() @ CSRulesPasswordField;
    CSRulesPasswordLabel;
    // unhandled opcode 975 at 0x000003CC
    setProfile();
    %flag.setVisible();
    %flag.setActive();
    %flag.setVisible();
    %flag.setVisible();
    (CSRulesPasswordField SPC getValue() $= "").setVisible();
    0.update();
    %flag.setVisible();
    0.update();
};
function CSDescTaglineTextBox::onKeyUp(%this) {
    0.update();
};
function CSRulesPasswordField::onKeyDown(%this, %unused, %unused) {
    0.setVisible();
    return 0;
};
function CSRulesPasswordField::onKeyUp(%this, %unused, %unused) {
    %fieldIsVisible = %this.isVisible();
    %fieldIsVisible.setVisible();
    (CSRulesPasswordFieldOverlay SPC %this.getValue() $= "").setVisible();
    0.update();
    return 0;
};
