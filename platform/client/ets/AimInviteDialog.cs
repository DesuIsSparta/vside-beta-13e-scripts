function AimInviteDialog::open(%this, %buddyName)
{
    if (%this.buddyNames $= "")
    {
        %this.buddyNames = %buddyName;
        %this.initWithName(%buddyName);
        %this.setVisible(1);
        PlayGui.focusAndRaise(%this);
        %this.refreshBuddyDropdown();
        AimInviteBuddyDropDown.SetSelected(0);
    }
    else
    {
        if (findField(%this.buddyNames, %buddyName) == -1)
        {
            %this.buddyNames = %this.buddyNames TAB %buddyName;
            %this.refreshBuddyDropdown();
            AimInviteBuddyDropDown.SetSelected(0);
        }
    }
    return ;
}
function AimInviteDialog::initWithName(%this, %buddyName)
{
    %this.buddyNames = %buddyName;
    %this.refreshBuddyDropdown();
    AimInviteMessageField.setText("Come join " @ $ETS::AppName @ "!");
    return ;
}
function AimInviteDialog::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    %this.buddyNames = "";
    AimInviteAddBuddyDialog.close();
    return 1;
}
function AimInviteDialog::setControlsActive(%this, %flag)
{
    AimInviteDialogButtonSend.setActive(%flag);
    return ;
}
function AimInviteDialog::onWake(%this)
{
    %this.setControlsActive(1);
    return ;
}
function AimInviteDialog::refreshBuddyDropdown(%this)
{
    if (AimInviteBuddyDropDown.getText $= "All buddies!")
    {
        return ;
    }
    AimInviteBuddyDropDown.clear();
    %count = getFieldCount(%this.buddyNames);
    if (%count == 0)
    {
        AimInviteBuddyDropDown.add("No buddy selected.");
    }
    else
    {
        if (%count > 1)
        {
            AimInviteBuddyDropDown.add(%count SPC "buddies");
        }
    }
    %n = 0;
    while (%n < %count)
    {
        AimInviteBuddyDropDown.add(getField(%this.buddyNames, %n));
        %n = %n + 1;
    }
    AimInviteBuddyDropDown.add("-------------------");
    AimInviteBuddyDropDown.add("Add Buddies");
    AimInviteBuddyDropDown.add("Invite all buddies!");
    return ;
}
function AimInviteBuddyDropDown::selectBuddy(%this, %aBuddyName)
{
    %n = %this.findText(%aBuddyName);
    %this.SetSelected(%n);
    return ;
}
function AimInviteDialog::buddyDropdownChanged(%this)
{
    %selection = AimInviteBuddyDropDown.getText();
    if (%selection $= "Add Buddies")
    {
        AimInviteAddBuddyDialog.open();
        AimInviteBuddyDropDown.SetSelected(0);
    }
    else
    {
        if (%selection $= "Invite all buddies!")
        {
            AimInviteBuddyDropDown.clear();
            AimInviteBuddyDropDown.add("All buddies!");
            AimInviteBuddyDropDown.SetSelected(0);
        }
        else
        {
            if (%selection $= "-------------------")
            {
                AimInviteBuddyDropDown.SetSelected(0);
            }
        }
    }
    return ;
}
function AimInviteDialog::sendInvite(%this)
{
    %selectedDropdown = AimInviteBuddyDropDown.getText();
    %count = getFieldCount(%this.buddyNames);
    if (%selectedDropdown $= "All buddies!")
    {
        AIMConvManager.inviteAll(AimInviteMessageField.getText());
    }
    else
    {
        if (%count == 0)
        {
            return ;
        }
        else
        {
            AIMConvManager.prepareToSendInvites(%this.buddyNames, AimInviteMessageField.getText());
        }
    }
    %this.close();
    return ;
}
function AimInviteAddBuddyDialog::open(%this)
{
    %inviteWinPos = AimInviteDialog.getPosition();
    %inviteWinExtent = AimInviteDialog.getExtent();
    %this.reposition(getWord(%inviteWinPos, 0) + getWord(%inviteWinExtent, 0), getWord(%inviteWinPos, 1));
    if (%this.visible == 0)
    {
        %this.refresh();
        %this.setVisible(1);
        PlayGui.focusAndRaise(%this);
        %this.refresh();
    }
    return ;
}
function AimInviteAddBuddyDialog::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return ;
}
function AimInviteAddBuddyDialog::refresh(%this)
{
    AimInviteAddBuddyList.clear();
    %buddyCount = aimBuddyCount();
    %i = 0;
    while (%i < %buddyCount)
    {
        %buddyName = aimGetBuddyName(%i);
        %buddyState = aimGetBuddyState(%i);
        if (((%buddyState == 1) || (%buddyState == 2)) || (%buddyState == 3))
        {
            if (findField(AimInviteDialog.buddyNames, %buddyName) == -1)
            {
                AimInviteAddBuddyList.addRow(%i, %buddyName, %i);
            }
        }
        %i = %i + 1;
    }
}

function AimInviteAddBuddyDialog::addSelected(%this)
{
    %id = AimInviteAddBuddyList.getSelectedId();
    %selected = AimInviteAddBuddyList.getRowTextById(%id);
    %row = AimInviteAddBuddyList.getRowNumById(%id);
    if (%row == AimInviteAddBuddyList.rowCount())
    {
        %row = 0;
    }
    if (!(%selected $= ""))
    {
        AimInviteAddBuddyList.removeRowById(%id);
        AimInviteDialog.open(%selected);
    }
    AimInviteAddBuddyList.setSelectedRow(%row);
    return ;
}
