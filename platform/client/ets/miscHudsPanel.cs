function geMiscHudsPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function geMiscHudsPanel::open(%this) {
    if (!($player.rolesPermissionCheckNoWarn("debugPassive"))) {
        return;
    }
    if (!(%this.isVisible())) {
        %this.setVisible(1);
        %this.focusAndRaise();
    }
};
function geMiscHudsPanel::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
};
function geMiscHudsPanel::addHud(%this, %panelCtrl) {
    %offsetX = getWord(geMiscHudsContainer.getExtent(), 0);
    (getWord(%panelCtrl.getExtent(), 0) + (%offsetX + 1.0)).resize(mMax(getWord(%panelCtrl.getExtent(), 1), getWord(geMiscHudsContainer.getExtent(), 1)));
    %panelCtrl.add();
    %panelCtrl.reposition(%offsetX, 0);
};
