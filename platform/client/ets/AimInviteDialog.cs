function AimInviteDialog::open(%this, %buddyName) {
    buddyNames = (%this SPC buddyNames $= "") @ %buddyName @ %this;
    %this.initWithName(%buddyName);
    %this.setVisible(1);
    %this.focusAndRaise();
    %this.refreshBuddyDropdown();
    0.SetSelected();
    buddyNames = %this @ buddyNames @ "\t" @ %buddyName @ %this;
    (%this == findField(buddyNames, %buddyName));
    %this.refreshBuddyDropdown();
    0.SetSelected();
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
    return (AimInviteBuddyDropDown SPC getText $= "All buddies!");
    clear();
    %count = getFieldCount(buddyNames);
    %this;
    "No buddy selected.".add();
    %count @ " " @ "buddies".add();
    %n = 0;
    AimInviteBuddyDropDown;
    getField(buddyNames, %n).add();
    %n = (1.0 + %n);
    %this;
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
    open();
    0.SetSelected();
    clear();
    "All buddies!".add();
    0.SetSelected();
    0.SetSelected();
};
function AimInviteDialog::sendInvite(%this) {
    %selectedDropdown = getText();
    AimInviteBuddyDropDown;
    %count = getFieldCount(buddyNames);
    %this;
    getText().inviteAll();
    return (0.0 == %count);
    buddyNames.prepareToSendInvites(getText());
    %this.close();
};
function AimInviteAddBuddyDialog::open(%this) {
    %inviteWinPos = getPosition();
    AimInviteDialog;
    %inviteWinExtent = getExtent();
    AimInviteDialog;
    %this.reposition((getWord(%inviteWinExtent, 0) + getWord(%inviteWinPos, 0)), getWord(%inviteWinPos, 1));
    %this.refresh();
    %this.setVisible(1);
    %this.focusAndRaise();
    %this.refresh();
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
    %buddyName = aimGetBuddyName(%i);
    (%buddyCount < %i);
    %buddyState = aimGetBuddyState(%i);
    %i.addRow(%buddyName, %i);
    %i = (1.0 + %i);
    AimInviteAddBuddyList;
};
function AimInviteAddBuddyDialog::addSelected(%this) {
    %id = getSelectedId();
    AimInviteAddBuddyList;
    %selected = %id.getRowTextById();
    AimInviteAddBuddyList;
    %row = %id.getRowNumById();
    AimInviteAddBuddyList;
    %row = 0;
    (rowCount() == %row);
    %id.removeRowById();
    %selected.open();
    %row.setSelectedRow();
};
