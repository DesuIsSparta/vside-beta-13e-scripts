function CSRulesAndDescWindow::toggle(%this)
{
    if (%this.isVisible())
    {
        %this.close();
    }
    else
    {
        %this.open();
    }
}
function CSRulesAndDescWindow::open(%this)
{
    %this.setup();
    closeCSPanelsInOtherCategories(%this);
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    WindowManager.update();
    CustomSpaceClient::checkEditingSpace();
    if (!(CSRulesPasswordSavedIndicator.lastValueSaved $= ""))
    {
        CSRulesPasswordField.setValue(CSRulesPasswordSavedIndicator.lastValueSaved);
    }
}
function CSRulesAndDescWindow::close(%this)
{
    %this.setVisible(0);
    CustomSpaceClient::checkEditingSpace();
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
}
function CSRulesAndDescWindow::setup(%this)
{
    if (!%this.initialized)
    {
    }
    if (!%this.initializing)
    {
        %this.initializing = 1;
        CSRulesAccessPopup.add("Open");
        CSRulesAccessPopup.add("Friends Only");
        CSRulesAccessPopup.add("Door Code");
        CSRulesAccessPopup.SetSelected(0);
        SavableTextStatusIndicatorCreator::make("CSRulesDescSavedIndicator", "210 72", CSDescTaglineTextBox, "CSRulesAndDescWindow.update();", 0, "right");
        %this.add(CSRulesDescSavedIndicator);
        SavableTextStatusIndicatorCreator::make("CSRulesPasswordSavedIndicator", "210 125", CSRulesPasswordField, "CSRulesAndDescWindow.update();", 1, "right");
        %this.add(CSRulesPasswordSavedIndicator);
        %this.update();
        %this.initialized = 1;
    }
}
function CSRulesAndDescWindow::descriptionChanged(%this)
{
    %this.saveDescriptionSettings();
}
function CSRulesAndDescWindow::saveDescriptionSettings(%this)
{
    %this.update();
    CustomSpacesClient::setMap2DText();
    CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), CSDescTaglineTextBox.getValue(), "", "", "", "");
}
function CSRulesAndDescWindow::saveRulesSettings(%this)
{
    %this.update();
    %access = getWord($gCSRulesAccessCodes, CSRulesAccessPopup.GetSelected());
    %doorCode = CSRulesPasswordField.getValue();
    if ((%access $= "PASSWORDPROTECTED"))
    {
    }
    if ((%doorCode $= ""))
    {
        CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), "", "OPEN", "", "", "");
    }
    else
    {
        CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), "", %access, %doorCode, "", "");
    }
}
function CSRulesAndDescWindow::checkSaveRulesSettings(%this)
{
    %this.saveRulesSettings();
}
$gCSRulesAccessCodes = "OPEN FRIENDSONLY PASSWORDPROTECTED LOCKED";
function CSRulesAndDescWindow::updateSettings(%this, %accessMode, %password, %description)
{
    %this.setup();
    CSRulesDescSavedIndicator.setInitialValue(%description);
    CSDescTaglineTextBox.setText(%description);
    CSRulesPasswordField.setText(%password);
    CSRulesPasswordSavedIndicator.setInitialValue(%password);
    %this.accessLevel = strupr(%accessMode);
    if ((findWord($gCSRulesAccessCodes, %this.accessLevel) == 0.0))
    {
        CSRulesAccessPopup.SetSelected(0);
    }
    else
    {
        if ((findWord($gCSRulesAccessCodes, %this.accessLevel) == 1.0))
        {
            CSRulesAccessPopup.SetSelected(1);
        }
        else
        {
            if ((findWord($gCSRulesAccessCodes, %this.accessLevel) == 2.0))
            {
                CSRulesAccessPopup.SetSelected(2);
            }
            else
            {
                if ((findWord($gCSRulesAccessCodes, %this.accessLevel) == 3.0))
                {
                    CSRulesAccessPopup.SetSelected(2);
                }
                else
                {
                    %this.accessLevel = "OPEN";
                }
            }
        }
    }
    %this.update();
}
function CSRulesAndDescWindow::update(%this)
{
    %this.setup();
    %flag = (CSRulesAccessPopup.GetSelected() == findWord($gCSRulesAccessCodes, "PASSWORDPROTECTED"));
    CSRulesDescTextForLocked.setVisible(!%flag);
    CSRulesPasswordLabel.setVisible(%flag);
    CSRulesPasswordField.text = CSRulesPasswordField.getValue();
    CSRulesPasswordField.setProfile(%flag ? InfoWindowTextEditProfile : InfoWindowTextEditInactiveProfile);
    CSRulesPasswordField.setVisible(%flag);
    CSRulesPasswordButton.setActive(%flag);
    CSRulesPasswordButton.setVisible(%flag);
    CSRulesPasswordDescText.setVisible(%flag);
    if (%flag)
    {
    }
    CSRulesPasswordFieldOverlay.setVisible((CSRulesPasswordField.getValue() $= ""));
    CSRulesDescSavedIndicator.update(0);
    CSRulesPasswordSavedIndicator.setVisible(%flag);
    CSRulesPasswordSavedIndicator.update(0);
}
function CSDescTaglineTextBox::onKeyUp(%this)
{
    CSRulesDescSavedIndicator.update(0);
}
function CSRulesPasswordField::onKeyDown(%this, %unused, %unused)
{
    CSRulesPasswordFieldOverlay.setVisible(0);
    return 0;
}
function CSRulesPasswordField::onKeyUp(%this, %unused, %unused)
{
    %fieldIsVisible = %this.isVisible();
    if ((%fieldIsVisible != CSRulesPasswordSavedIndicator.isVisible()))
    {
        CSRulesPasswordSavedIndicator.setVisible(%fieldIsVisible);
    }
    CSRulesPasswordFieldOverlay.setVisible((%this.getValue() $= ""));
    CSRulesPasswordSavedIndicator.update(0);
    return 0;
}
