function staffPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function staffPanel::open(%this) {
    return !($player.rolesPermissionCheckNoWarn("debugPassive"));
    %this.setVisible(1);
    %this.focusAndRaise();
};
function staffPanel::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
