function geMiscHudsPanel::toggle(%this) {
    %this.showRaiseOrHide(PlayGui);
};
function geMiscHudsPanel::open(%this) {
    if (!("debugPassive".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    if (!(%this.isVisible())) {
        1.setVisible(%this);
        %this.focusAndRaise(PlayGui);
    }
};
function geMiscHudsPanel::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    return 1;
};
function geMiscHudsPanel::addHud(%this, %panelCtrl) {
    %offsetX = getWord(geMiscHudsContainer.getExtent(), 0);
    mMax(getWord(%panelCtrl.getExtent(), 1), getWord(geMiscHudsContainer.getExtent(), 1)).resize(geMiscHudsContainer, ((1.0 + %offsetX) + getWord(%panelCtrl.getExtent(), 0)));
    %panelCtrl.add(geMiscHudsContainer);
    0.reposition(%panelCtrl, %offsetX);
};
