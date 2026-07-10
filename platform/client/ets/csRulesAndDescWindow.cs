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
    WindowManager.update();
    CustomSpaceClient::checkEditingSpace();
    if (!(CSRulesPasswordSavedIndicator @ " " @ lastValueSaved $= "")) {
        lastValueSaved.setValue();
    }
};
function CSRulesAndDescWindow::close(%this) {
    %this.setVisible(0);
    CustomSpaceClient::checkEditingSpace();
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
};
function CSRulesAndDescWindow::setup(%this) {
    if (!(%this.initialized)) {
    }
    if (!(%this.initializing)) {
        %this.initializing = 1;
        "Open".add();
        "Friends Only".add();
        "Door Code".add();
        0.SetSelected();
        SavableTextStatusIndicatorCreator::make("CSRulesDescSavedIndicator", "210 72", "CSRulesAndDescWindow.update();", 0, "right");
        %this.add();
        SavableTextStatusIndicatorCreator::make("CSRulesPasswordSavedIndicator", "210 125", "CSRulesAndDescWindow.update();", 1, "right");
        %this.add();
        %this.update();
        %this.initialized = CSRulesPasswordSavedIndicator @ 1;
        CSRulesPasswordField;
    }
};
function CSRulesAndDescWindow::descriptionChanged(%this) {
    %this.saveDescriptionSettings();
};
function CSRulesAndDescWindow::saveDescriptionSettings(%this) {
    %this.update();
    CustomSpacesClient::setMap2DText();
    CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), CSDescTaglineTextBox.getValue(), "", "", "", "");
};
function CSRulesAndDescWindow::saveRulesSettings(%this) {
    %this.update();
    %access = getWord($gCSRulesAccessCodes, CSRulesAccessPopup.GetSelected());
    %doorCode = CSRulesPasswordField.getValue();
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
    %this.accessLevel = CSRulesPasswordSavedIndicator @ strupr(%accessMode);
    CSRulesPasswordField;
    if ((0.0 == findWord($gCSRulesAccessCodes, %this.accessLevel))) {
        0.SetSelected();
    }
    if ((1.0 == findWord($gCSRulesAccessCodes, %this.accessLevel))) {
        1.SetSelected();
    }
    if ((2.0 == findWord($gCSRulesAccessCodes, %this.accessLevel))) {
        2.SetSelected();
    }
    if ((3.0 == findWord($gCSRulesAccessCodes, %this.accessLevel))) {
        2.SetSelected();
    }
    %this.accessLevel = CSRulesAccessPopup @ "OPEN";
    CSRulesAccessPopup;
    %this.update();
};
function CSRulesAndDescWindow::update(%this) {
    %this.setup();
    %flag = (findWord($gCSRulesAccessCodes, "PASSWORDPROTECTED") == CSRulesAccessPopup.GetSelected());
    !(%flag).setVisible();
    %flag.setVisible();
    %this.text = CSRulesPasswordField.getValue() @ CSRulesPasswordField;
    CSRulesPasswordLabel;
    if (%flag) {
        // unhandled opcode 975 at 0x000003CC
    }
    CSRulesPasswordField.setProfile(InfoWindowTextEditProfile, InfoWindowTextEditInactiveProfile);
    %flag.setVisible();
    %flag.setActive();
    %flag.setVisible();
    %flag.setVisible();
    if (%flag) {
    }
    (CSRulesPasswordFieldOverlay @ " " @ CSRulesPasswordField.getValue() $= "").setVisible();
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
    if ((CSRulesPasswordSavedIndicator.isVisible() != %fieldIsVisible)) {
        %fieldIsVisible.setVisible();
    }
    (CSRulesPasswordFieldOverlay @ " " @ %this.getValue() $= "").setVisible();
    0.update();
    return 0;
};
