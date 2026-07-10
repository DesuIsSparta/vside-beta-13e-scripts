function staffPanel::toggle(%this) {
    %this.showRaiseOrHide(playGui);
};
function staffPanel::open(%this) {
    if (!("debugPassive".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    1.setVisible(%this);
    %this.focusAndRaise(playGui);
};
function staffPanel::close(%this) {
    0.setVisible(%this);
    playGui.focusTopWindow();
    return 1;
};
