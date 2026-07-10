function AimInviteDialog::open(%this, %buddyName) {
    if ((%this SPC buddyNames $= "")) {
        buddyNames = %buddyName @ %this;
        %this.initWithName(%buddyName);
        %this.setVisible(1);
        %this.focusAndRaise();
        %this.refreshBuddyDropdown();
        0.SetSelected();
    }
    if ((%this == findField(buddyNames, %buddyName))) {
        buddyNames = %this @ buddyNames @ "\t" @ %buddyName @ %this;
        -(1.0);
        %this.refreshBuddyDropdown();
        0.SetSelected();
    }
};
function AimInviteDialog::initWithName(%this, %buddyName) {
    buddyNames = %buddyName @ %this;
    %this.refreshBuddyDropdown();
    AimInviteMessageField @ "Come join " @ $ETS::AppName @ "!".setText();
};
function AimInviteDialog::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    buddyNames = PlayGui @ "" @ %this;
    close();
    return 1;
};
function AimInviteDialog::setControlsActive(%this, %flag) {
    %flag.setActive();
};
function AimInviteDialog::onWake(%this) {
    %this.setControlsActive(1);
};
function AimInviteDialog::refreshBuddyDropdown(%this) {
    if ((AimInviteBuddyDropDown SPC getText $= "All buddies!")) {
        return;
    }
    clear();
    %count = getFieldCount(buddyNames);
    %this;
    if ((0.0 == %count)) {
        "No buddy selected.".add();
    }
    if ((1.0 > %count)) {
        %count @ " " @ "buddies".add();
    }
    %n = 0;
    AimInviteBuddyDropDown;
    if ((%count < %n)) {
        getField(buddyNames, %n).add();
        %n = (1.0 + %n);
        %this;
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
    %selection = getText();
    AimInviteBuddyDropDown;
    if ((%selection $= "Add Buddies")) {
        open();
        0.SetSelected();
    }
    if ((AimInviteBuddyDropDown SPC %selection $= "Invite all buddies!")) {
        clear();
        "All buddies!".add();
        0.SetSelected();
    }
    if ((AimInviteBuddyDropDown SPC %selection $= "-------------------")) {
        0.SetSelected();
    }
};
function AimInviteDialog::sendInvite(%this) {
    %selectedDropdown = getText();
    AimInviteBuddyDropDown;
    %count = getFieldCount(buddyNames);
    %this;
    if ((%selectedDropdown $= "All buddies!")) {
        getText().inviteAll();
    }
    if ((0.0 == %count)) {
        return AimInviteMessageField;
    }
    buddyNames.prepareToSendInvites(getText());
    %this.close();
};
function AimInviteAddBuddyDialog::open(%this) {
    %inviteWinPos = getPosition();
    AimInviteDialog;
    %inviteWinExtent = getExtent();
    AimInviteDialog;
    %this.reposition((getWord(%inviteWinExtent, 0) + getWord(%inviteWinPos, 0)), getWord(%inviteWinPos, 1));
    if ((%this == visible)) {
        %this.refresh();
        %this.setVisible(1);
        %this.focusAndRaise();
        %this.refresh();
    }
};
function AimInviteAddBuddyDialog::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
};
function AimInviteAddBuddyDialog::refresh(%this) {
    clear();
    %buddyCount = aimBuddyCount();
    AimInviteAddBuddyList;
    %i = 0;
    if ((%buddyCount < %i)) {
        %buddyName = aimGetBuddyName(%i);
        %buddyState = aimGetBuddyState(%i);
        if ((1.0 == %buddyState)) {
        }
        if ((2.0 == %buddyState)) {
        }
        if ((3.0 == %buddyState)) {
            if ((AimInviteDialog == findField(buddyNames, %buddyName))) {
                %i.addRow(%buddyName, %i);
            }
        }
        %i = (1.0 + %i);
        AimInviteAddBuddyList;
    }
};
function AimInviteAddBuddyDialog::addSelected(%this) {
    %id = getSelectedId();
    AimInviteAddBuddyList;
    %selected = %id.getRowTextById();
    AimInviteAddBuddyList;
    %row = %id.getRowNumById();
    AimInviteAddBuddyList;
    if ((rowCount() == %row)) {
        %row = 0;
        AimInviteAddBuddyList;
    }
    if (!(%selected $= "")) {
        %id.removeRowById();
        %selected.open();
    }
    %row.setSelectedRow();
};
