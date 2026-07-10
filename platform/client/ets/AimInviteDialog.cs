function AimInviteDialog::open(%this, %buddyName) {
    if ((%this.buddyNames $= "")) {
        %this.buddyNames = %buddyName;
        %this.initWithName(%buddyName);
        %this.setVisible(1);
        PlayGui.focusAndRaise(%this);
        %this.refreshBuddyDropdown();
        AimInviteBuddyDropDown.SetSelected(0);
    }
    if ((-(1.0) == findField(%this.buddyNames, %buddyName))) {
        %this.buddyNames = %this.buddyNames @ "\t" @ %buddyName;
        %this.refreshBuddyDropdown();
        AimInviteBuddyDropDown.SetSelected(0);
    }
};
function AimInviteDialog::initWithName(%this, %buddyName) {
    %this.buddyNames = %buddyName;
    %this.refreshBuddyDropdown();
    AimInviteMessageField.setText("Come join " @ $ETS::AppName @ "!");
};
function AimInviteDialog::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    %this.buddyNames = "";
    AimInviteAddBuddyDialog.close();
    return 1;
};
function AimInviteDialog::setControlsActive(%this, %flag) {
    AimInviteDialogButtonSend.setActive(%flag);
};
function AimInviteDialog::onWake(%this) {
    %this.setControlsActive(1);
};
function AimInviteDialog::refreshBuddyDropdown(%this) {
    if ((AimInviteBuddyDropDown @ " " @ %this.getText $= "All buddies!")) {
        return;
    }
    AimInviteBuddyDropDown.clear();
    %count = getFieldCount(%this.buddyNames);
    if ((0.0 == %count)) {
        AimInviteBuddyDropDown.add("No buddy selected.");
    }
    if ((1.0 > %count)) {
        AimInviteBuddyDropDown.add(%count @ " " @ "buddies");
    }
    %n = 0;
    if ((%count < %n)) {
        AimInviteBuddyDropDown.add(getField(%this.buddyNames, %n));
        %n = (1.0 + %n);
    }
    AimInviteBuddyDropDown.add("-------------------");
    AimInviteBuddyDropDown.add("Add Buddies");
    AimInviteBuddyDropDown.add("Invite all buddies!");
};
function AimInviteBuddyDropDown::selectBuddy(%this, %aBuddyName) {
    %n = %this.findText(%aBuddyName);
    %this.SetSelected(%n);
};
function AimInviteDialog::buddyDropdownChanged(%this) {
    %selection = AimInviteBuddyDropDown.getText();
    if ((%selection $= "Add Buddies")) {
        AimInviteAddBuddyDialog.open();
        AimInviteBuddyDropDown.SetSelected(0);
    }
    if ((%selection $= "Invite all buddies!")) {
        AimInviteBuddyDropDown.clear();
        AimInviteBuddyDropDown.add("All buddies!");
        AimInviteBuddyDropDown.SetSelected(0);
    }
    if ((%selection $= "-------------------")) {
        AimInviteBuddyDropDown.SetSelected(0);
    }
};
function AimInviteDialog::sendInvite(%this) {
    %selectedDropdown = AimInviteBuddyDropDown.getText();
    %count = getFieldCount(%this.buddyNames);
    if ((%selectedDropdown $= "All buddies!")) {
        AIMConvManager.inviteAll(AimInviteMessageField.getText());
    }
    if ((0.0 == %count)) {
        return;
    }
    AIMConvManager.prepareToSendInvites(%this.buddyNames, AimInviteMessageField.getText());
    %this.close();
};
function AimInviteAddBuddyDialog::open(%this) {
    %inviteWinPos = AimInviteDialog.getPosition();
    %inviteWinExtent = AimInviteDialog.getExtent();
    %this.reposition((getWord(%inviteWinExtent, 0) + getWord(%inviteWinPos, 0)), getWord(%inviteWinPos, 1));
    if ((0.0 == %this.visible)) {
        %this.refresh();
        %this.setVisible(1);
        PlayGui.focusAndRaise(%this);
        %this.refresh();
    }
};
function AimInviteAddBuddyDialog::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
};
function AimInviteAddBuddyDialog::refresh(%this) {
    AimInviteAddBuddyList.clear();
    %buddyCount = aimBuddyCount();
    %i = 0;
    if ((%buddyCount < %i)) {
        %buddyName = aimGetBuddyName(%i);
        %buddyState = aimGetBuddyState(%i);
        if ((1.0 == %buddyState)) {
        }
        if ((2.0 == %buddyState)) {
        }
        if ((3.0 == %buddyState)) {
            if ((-(1.0) == findField(AimInviteDialog, %this.buddyNames, %buddyName))) {
                AimInviteAddBuddyList.addRow(%i, %buddyName, %i);
            }
        }
        %i = (1.0 + %i);
    }
};
function AimInviteAddBuddyDialog::addSelected(%this) {
    %id = AimInviteAddBuddyList.getSelectedId();
    %selected = AimInviteAddBuddyList.getRowTextById(%id);
    %row = AimInviteAddBuddyList.getRowNumById(%id);
    if ((AimInviteAddBuddyList.rowCount() == %row)) {
        %row = 0;
    }
    if (!(%selected $= "")) {
        AimInviteAddBuddyList.removeRowById(%id);
        AimInviteDialog.open(%selected);
    }
    AimInviteAddBuddyList.setSelectedRow(%row);
};
