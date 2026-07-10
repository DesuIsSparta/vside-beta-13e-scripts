function botControlPanel::toggle(%this) {
    %this.showRaiseOrHide(playGui);
};
function botControlPanel::open(%this) {
    if (!("bots".rolesPermissionCheckWarn($player))) {
        return;
    }
    1.setVisible(%this);
    %this.focusAndRaise(playGui);
};
function botControlPanel::close(%this) {
    0.setVisible(%this);
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
    %filebase.setValue(saveLoadBotsFileNameCtrl);
    return %filebase;
};
