function staffPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function staffPanel::open(%this) {
    if (!($player.rolesPermissionCheckNoWarn("debugPassive"))) {
        return;
    }
    %this.setVisible(1);
    %this.focusAndRaise();
};
function staffPanel::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
