function AimInviteDialog::open(%this, %buddyName) {
    if ((%this.buddyNames $= "")) {
        %this.buddyNames = %buddyName;
        %buddyName.initWithName(%this);
        1.setVisible(%this);
        %this.focusAndRaise(PlayGui);
        %this.refreshBuddyDropdown();
        0.SetSelected(AimInviteBuddyDropDown);
    }
    if ((findField(%this.buddyNames, %buddyName) == -(1.0))) {
        %this.buddyNames = %this.buddyNames @ "\t" @ %buddyName;
        %this.refreshBuddyDropdown();
        0.SetSelected(AimInviteBuddyDropDown);
    }
};
function AimInviteDialog::initWithName(%this, %buddyName) {
    %this.buddyNames = %buddyName;
    %this.refreshBuddyDropdown();
    "Come join " @ $ETS::AppName @ "!".setText(AimInviteMessageField);
};
function AimInviteDialog::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    %this.buddyNames = "";
    AimInviteAddBuddyDialog.close();
    return 1;
};
function AimInviteDialog::setControlsActive(%this, %flag) {
    %flag.setActive(AimInviteDialogButtonSend);
};
function AimInviteDialog::onWake(%this) {
    1.setControlsActive(%this);
};
function AimInviteDialog::refreshBuddyDropdown(%this) {
    if ((AimInviteBuddyDropDown.getText $= "All buddies!")) {
        return;
    }
    AimInviteBuddyDropDown.clear();
    %count = getFieldCount(%this.buddyNames);
    if ((%count == 0.0)) {
        "No buddy selected.".add(AimInviteBuddyDropDown);
    }
    if ((%count > 1.0)) {
        %count @ " " @ "buddies".add(AimInviteBuddyDropDown);
    }
    %n = 0;
    while ((%n < %count)) {
        getField(%this.buddyNames, %n).add(AimInviteBuddyDropDown);
        %n = (%n + 1.0);
    }
    "-------------------".add(AimInviteBuddyDropDown);
    "Add Buddies".add(AimInviteBuddyDropDown);
    "Invite all buddies!".add(AimInviteBuddyDropDown);
};
function AimInviteBuddyDropDown::selectBuddy(%this, %aBuddyName) {
    %n = %aBuddyName.findText(%this);
    %n.SetSelected(%this);
};
function AimInviteDialog::buddyDropdownChanged(%this) {
    %selection = AimInviteBuddyDropDown.getText();
    if ((%selection $= "Add Buddies")) {
        AimInviteAddBuddyDialog.open();
        0.SetSelected(AimInviteBuddyDropDown);
    }
    if ((%selection $= "Invite all buddies!")) {
        AimInviteBuddyDropDown.clear();
        "All buddies!".add(AimInviteBuddyDropDown);
        0.SetSelected(AimInviteBuddyDropDown);
    }
    if ((%selection $= "-------------------")) {
        0.SetSelected(AimInviteBuddyDropDown);
    }
};
function AimInviteDialog::sendInvite(%this) {
    %selectedDropdown = AimInviteBuddyDropDown.getText();
    %count = getFieldCount(%this.buddyNames);
    if ((%selectedDropdown $= "All buddies!")) {
        AimInviteMessageField.getText().inviteAll(AIMConvManager);
    }
    if ((%count == 0.0)) {
        return;
    }
    AimInviteMessageField.getText().prepareToSendInvites(AIMConvManager, %this.buddyNames);
    %this.close();
};
function AimInviteAddBuddyDialog::open(%this) {
    %inviteWinPos = AimInviteDialog.getPosition();
    %inviteWinExtent = AimInviteDialog.getExtent();
    getWord(%inviteWinPos, 1).reposition(%this, (getWord(%inviteWinPos, 0) + getWord(%inviteWinExtent, 0)));
    if ((%this.visible == 0.0)) {
        %this.refresh();
        1.setVisible(%this);
        %this.focusAndRaise(PlayGui);
        %this.refresh();
    }
};
function AimInviteAddBuddyDialog::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
};
function AimInviteAddBuddyDialog::refresh(%this) {
    AimInviteAddBuddyList.clear();
    %buddyCount = aimBuddyCount();
    %i = 0;
    while ((%i < %buddyCount)) {
        %buddyName = aimGetBuddyName(%i);
        %buddyState = aimGetBuddyState(%i);
        if ((%buddyState == 1.0)) {
        }
        if ((%buddyState == 2.0)) {
        }
        if ((%buddyState == 3.0) && (findField(AimInviteDialog.buddyNames, %buddyName) == -(1.0))) {
            %i.addRow(AimInviteAddBuddyList, %i, %buddyName);
        }
        %i = (%i + 1.0);
    }
};
function AimInviteAddBuddyDialog::addSelected(%this) {
    %id = AimInviteAddBuddyList.getSelectedId();
    %selected = %id.getRowTextById(AimInviteAddBuddyList);
    %row = %id.getRowNumById(AimInviteAddBuddyList);
    if ((%row == AimInviteAddBuddyList.rowCount())) {
        %row = 0;
    }
    if (!(%selected $= "")) {
        %id.removeRowById(AimInviteAddBuddyList);
        %selected.open(AimInviteDialog);
    }
    %row.setSelectedRow(AimInviteAddBuddyList);
};
