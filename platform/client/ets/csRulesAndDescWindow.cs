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
    PlayGui.focusAndRaise(%this);
    WindowManager.update();
    CustomSpaceClient::checkEditingSpace();
    if (!(CSRulesPasswordSavedIndicator @ " " @ lastValueSaved $= "")) {
        CSRulesPasswordField.setValue(CSRulesPasswordSavedIndicator, lastValueSaved);
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
        CSRulesAccessPopup.add("Open");
        CSRulesAccessPopup.add("Friends Only");
        CSRulesAccessPopup.add("Door Code");
        CSRulesAccessPopup.SetSelected(0);
        SavableTextStatusIndicatorCreator::make("CSRulesDescSavedIndicator", "210 72", CSDescTaglineTextBox, "CSRulesAndDescWindow.update();", 0, "right");
        %this.add();
        SavableTextStatusIndicatorCreator::make("CSRulesPasswordSavedIndicator", "210 125", CSRulesPasswordField, "CSRulesAndDescWindow.update();", 1, "right");
        %this.add();
        %this.update();
        %this.initialized = CSRulesPasswordSavedIndicator @ 1;
        CSRulesDescSavedIndicator;
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
    CSRulesDescSavedIndicator.setInitialValue(%description);
    CSDescTaglineTextBox.setText(%description);
    CSRulesPasswordField.setText(%password);
    CSRulesPasswordSavedIndicator.setInitialValue(%password);
    %this.accessLevel = strupr(%accessMode);
    if ((0.0 == findWord($gCSRulesAccessCodes, %this.accessLevel))) {
        CSRulesAccessPopup.SetSelected(0);
    }
    if ((1.0 == findWord($gCSRulesAccessCodes, %this.accessLevel))) {
        CSRulesAccessPopup.SetSelected(1);
    }
    if ((2.0 == findWord($gCSRulesAccessCodes, %this.accessLevel))) {
        CSRulesAccessPopup.SetSelected(2);
    }
    if ((3.0 == findWord($gCSRulesAccessCodes, %this.accessLevel))) {
        CSRulesAccessPopup.SetSelected(2);
    }
    %this.accessLevel = "OPEN";
    %this.update();
};
function CSRulesAndDescWindow::update(%this) {
    %this.setup();
    %flag = (findWord($gCSRulesAccessCodes, "PASSWORDPROTECTED") == CSRulesAccessPopup.GetSelected());
    CSRulesDescTextForLocked.setVisible(!(%flag));
    CSRulesPasswordLabel.setVisible(%flag);
    %this.text = CSRulesPasswordField.getValue() @ CSRulesPasswordField;
    if (%flag) {
        // unhandled opcode 975 at 0x000003CC
    }
    CSRulesPasswordField.setProfile(InfoWindowTextEditProfile, InfoWindowTextEditInactiveProfile);
    CSRulesPasswordField.setVisible(%flag);
    CSRulesPasswordButton.setActive(%flag);
    CSRulesPasswordButton.setVisible(%flag);
    CSRulesPasswordDescText.setVisible(%flag);
    if (%flag) {
    }
    (CSRulesPasswordFieldOverlay @ " " @ CSRulesPasswordField.getValue() $= "").setVisible();
    CSRulesDescSavedIndicator.update(0);
    CSRulesPasswordSavedIndicator.setVisible(%flag);
    CSRulesPasswordSavedIndicator.update(0);
};
function CSDescTaglineTextBox::onKeyUp(%this) {
    CSRulesDescSavedIndicator.update(0);
};
function CSRulesPasswordField::onKeyDown(%this, %unused, %unused) {
    CSRulesPasswordFieldOverlay.setVisible(0);
    return 0;
};
function CSRulesPasswordField::onKeyUp(%this, %unused, %unused) {
    %fieldIsVisible = %this.isVisible();
    if ((CSRulesPasswordSavedIndicator.isVisible() != %fieldIsVisible)) {
        CSRulesPasswordSavedIndicator.setVisible(%fieldIsVisible);
    }
    (CSRulesPasswordFieldOverlay @ " " @ %this.getValue() $= "").setVisible();
    CSRulesPasswordSavedIndicator.update(0);
    return 0;
};
