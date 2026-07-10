function AimInviteDialog::open(%this, %buddyName) {
    if ((%this.buddyNames $= "")) {
        %this.buddyNames = %buddyName;
        %this.initWithName(%buddyName);
        %this.setVisible(1);
        %this.focusAndRaise();
        %this.refreshBuddyDropdown();
        0.SetSelected();
    }
    if ((-(1.0) == findField(%this.buddyNames, %buddyName))) {
        %this.buddyNames = AimInviteBuddyDropDown @ %this.buddyNames @ "\t" @ %buddyName;
        PlayGui;
        %this.refreshBuddyDropdown();
        0.SetSelected();
    }
};
function AimInviteDialog::initWithName(%this, %buddyName) {
    %this.buddyNames = %buddyName;
    %this.refreshBuddyDropdown();
    "Come join " @ $ETS::AppName @ "!".setText();
};
function AimInviteDialog::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    %this.buddyNames = "";
    AimInviteAddBuddyDialog.close();
    return 1;
};
function AimInviteDialog::setControlsActive(%this, %flag) {
    %flag.setActive();
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
        "No buddy selected.".add();
    }
    if ((1.0 > %count)) {
        %count @ " " @ "buddies".add();
    }
    %n = 0;
    AimInviteBuddyDropDown;
    if ((%count < %n)) {
        getField(%this.buddyNames, %n).add();
        %n = (1.0 + %n);
        AimInviteBuddyDropDown;
    }
    "-------------------".add();
    "Add Buddies".add();
    "Invite all buddies!".add();
};
function AimInviteBuddyDropDown::selectBuddy(%this, %aBuddyName) {
    %n = %this.findText(%aBuddyName);
    %this.SetSelected(%n);
};
function AimInviteDialog::buddyDropdownChanged(%this) {
    %selection = AimInviteBuddyDropDown.getText();
    if ((%selection $= "Add Buddies")) {
        AimInviteAddBuddyDialog.open();
        0.SetSelected();
    }
    if ((AimInviteBuddyDropDown @ " " @ %selection $= "Invite all buddies!")) {
        AimInviteBuddyDropDown.clear();
        "All buddies!".add();
        0.SetSelected();
    }
    if ((AimInviteBuddyDropDown @ " " @ %selection $= "-------------------")) {
        0.SetSelected();
    }
};
function AimInviteDialog::sendInvite(%this) {
    %selectedDropdown = AimInviteBuddyDropDown.getText();
    %count = getFieldCount(%this.buddyNames);
    if ((%selectedDropdown $= "All buddies!")) {
        AimInviteMessageField.getText().inviteAll();
    }
    if ((0.0 == %count)) {
        return AIMConvManager;
    }
    %this.buddyNames.prepareToSendInvites(AimInviteMessageField.getText());
    %this.close();
};
function AimInviteAddBuddyDialog::open(%this) {
    %inviteWinPos = AimInviteDialog.getPosition();
    %inviteWinExtent = AimInviteDialog.getExtent();
    %this.reposition((getWord(%inviteWinExtent, 0) + getWord(%inviteWinPos, 0)), getWord(%inviteWinPos, 1));
    if ((0.0 == %this.visible)) {
        %this.refresh();
        %this.setVisible(1);
        %this.focusAndRaise();
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
            if ((AimInviteDialog == findField(%this.buddyNames, %buddyName))) {
                %i.addRow(%buddyName, %i);
            }
        }
        %i = (1.0 + %i);
        AimInviteAddBuddyList;
    }
};
function AimInviteAddBuddyDialog::addSelected(%this) {
    %id = AimInviteAddBuddyList.getSelectedId();
    %selected = %id.getRowTextById();
    AimInviteAddBuddyList;
    %row = %id.getRowNumById();
    AimInviteAddBuddyList;
    if ((AimInviteAddBuddyList.rowCount() == %row)) {
        %row = 0;
    }
    if (!(%selected $= "")) {
        %id.removeRowById();
        %selected.open();
    }
    %row.setSelectedRow();
};
