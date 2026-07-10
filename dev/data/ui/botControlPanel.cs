function botControlPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function botControlPanel::open(%this) {
    if (!($player.rolesPermissionCheckWarn("bots"))) {
        return;
    }
    %this.setVisible(1);
    %this.focusAndRaise();
};
function botControlPanel::close(%this) {
    %this.setVisible(0);
    playGui.focusTopWindow();
    return 1;
};
function botControlPanel::saveBots(%this) {
    %filebase = %this.getSaveFilename();
    commandToServer('saveBots', %filebase);
};
function botControlPanel::loadBots(%this) {
    %filebase = %this.getSaveFilename();
    commandToServer('loadBots', %filebase);
};
function botControlPanel::getSaveFilename(%this) {
    %filebase = $DevPref::Mod::botSaveFileName;
    if ((%filebase $= "")) {
        %filebase = $player.getShapeName() @ 1;
    }
    if ((%filebase $= "")) {
        %filebase = "default";
    }
    $DevPref::Mod::botSaveFileName = %filebase;
    %filebase.setValue();
    return %filebase;
};
