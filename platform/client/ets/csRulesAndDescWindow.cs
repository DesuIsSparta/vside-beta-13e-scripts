function CSRulesAndDescWindow::toggle(%this) {
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
function CSRulesAndDescWindow::open(%this) {
    %this.setup();
    closeCSPanelsInOtherCategories(%this);
    %this.setVisible(1);
    %this.focusAndRaise();
    update();
    CustomSpaceClient::checkEditingSpace();
    if (!(CSRulesPasswordSavedIndicator SPC lastValueSaved $= "")) {
        lastValueSaved.setValue();
    }
};
function CSRulesAndDescWindow::close(%this) {
    %this.setVisible(0);
    CustomSpaceClient::checkEditingSpace();
    focusTopWindow();
    update();
    return 1;
};
function CSRulesAndDescWindow::setup(%this) {
    if (!(initialized)) {
    }
    if (!(initializing)) {
        initializing = %this @ 1 @ %this;
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
    }
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
    if ((%access $= "PASSWORDPROTECTED")) {
    }
    if ((%doorCode $= "")) {
        CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), "", "OPEN", "", "", "");
    }
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
    if ((%this == findWord($gCSRulesAccessCodes, accessLevel))) {
        0.SetSelected();
    }
    if ((%this == findWord($gCSRulesAccessCodes, accessLevel))) {
        1.SetSelected();
    }
    if ((%this == findWord($gCSRulesAccessCodes, accessLevel))) {
        2.SetSelected();
    }
    if ((%this == findWord($gCSRulesAccessCodes, accessLevel))) {
        2.SetSelected();
    }
    accessLevel = CSRulesAccessPopup @ "OPEN" @ %this;
    3.0;
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
    if (%flag) {
        // unhandled opcode 975 at 0x000003CC
    }
    setProfile();
    %flag.setVisible();
    %flag.setActive();
    %flag.setVisible();
    %flag.setVisible();
    if (%flag) {
    }
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
    if ((isVisible() != %fieldIsVisible)) {
        %fieldIsVisible.setVisible();
    }
    (CSRulesPasswordFieldOverlay SPC %this.getValue() $= "").setVisible();
    0.update();
    return 0;
};
