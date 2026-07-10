function CSRulesAndDescWindow::toggle(%this) {
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
function CSRulesAndDescWindow::open(%this) {
    %this.setup();
    closeCSPanelsInOtherCategories(%this);
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    WindowManager.update();
    CustomSpaceClient::checkEditingSpace();
    if (!(CSRulesPasswordSavedIndicator @ " " @ lastValueSaved $= "")) {
        lastValueSaved.setValue(CSRulesPasswordField, CSRulesPasswordSavedIndicator);
    }
};
function CSRulesAndDescWindow::close(%this) {
    0.setVisible(%this);
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
        "Open".add(CSRulesAccessPopup);
        "Friends Only".add(CSRulesAccessPopup);
        "Door Code".add(CSRulesAccessPopup);
        0.SetSelected(CSRulesAccessPopup);
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
    %description.setInitialValue(CSRulesDescSavedIndicator);
    %description.setText(CSDescTaglineTextBox);
    %password.setText(CSRulesPasswordField);
    %password.setInitialValue(CSRulesPasswordSavedIndicator);
    %this.accessLevel = strupr(%accessMode);
    if ((findWord($gCSRulesAccessCodes, %this.accessLevel) == 0.0)) {
        0.SetSelected(CSRulesAccessPopup);
    }
    if ((findWord($gCSRulesAccessCodes, %this.accessLevel) == 1.0)) {
        1.SetSelected(CSRulesAccessPopup);
    }
    if ((findWord($gCSRulesAccessCodes, %this.accessLevel) == 2.0)) {
        2.SetSelected(CSRulesAccessPopup);
    }
    if ((findWord($gCSRulesAccessCodes, %this.accessLevel) == 3.0)) {
        2.SetSelected(CSRulesAccessPopup);
    }
    %this.accessLevel = "OPEN";
    %this.update();
};
function CSRulesAndDescWindow::update(%this) {
    %this.setup();
    %flag = (CSRulesAccessPopup.GetSelected() == findWord($gCSRulesAccessCodes, "PASSWORDPROTECTED"));
    !(%flag).setVisible(CSRulesDescTextForLocked);
    %flag.setVisible(CSRulesPasswordLabel);
    %this.text = CSRulesPasswordField.getValue() @ CSRulesPasswordField;
    if (%flag) {
        // unhandled opcode 975 at 0x000003CC
    }
    InfoWindowTextEditInactiveProfile.setProfile(CSRulesPasswordField, InfoWindowTextEditProfile);
    %flag.setVisible(CSRulesPasswordField);
    %flag.setActive(CSRulesPasswordButton);
    %flag.setVisible(CSRulesPasswordButton);
    %flag.setVisible(CSRulesPasswordDescText);
    if (%flag) {
    }
    (CSRulesPasswordFieldOverlay @ " " @ CSRulesPasswordField.getValue() $= "").setVisible();
    0.update(CSRulesDescSavedIndicator);
    %flag.setVisible(CSRulesPasswordSavedIndicator);
    0.update(CSRulesPasswordSavedIndicator);
};
function CSDescTaglineTextBox::onKeyUp(%this) {
    0.update(CSRulesDescSavedIndicator);
};
function CSRulesPasswordField::onKeyDown(%this, %unused, %unused) {
    0.setVisible(CSRulesPasswordFieldOverlay);
    return 0;
};
function CSRulesPasswordField::onKeyUp(%this, %unused, %unused) {
    %fieldIsVisible = %this.isVisible();
    if ((%fieldIsVisible != CSRulesPasswordSavedIndicator.isVisible())) {
        %fieldIsVisible.setVisible(CSRulesPasswordSavedIndicator);
    }
    (CSRulesPasswordFieldOverlay @ " " @ %this.getValue() $= "").setVisible();
    0.update(CSRulesPasswordSavedIndicator);
    return 0;
};
