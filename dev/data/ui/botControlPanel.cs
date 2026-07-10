function botControlPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function botControlPanel::open(%this) {
    return !($player.rolesPermissionCheckWarn("bots"));
    %this.setVisible(1);
    %this.focusAndRaise();
};
function botControlPanel::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
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
    %filebase = (%filebase $= "") @ $player.getShapeName() @ 1;
    %filebase = "default";
    (%filebase $= "");
    $DevPref::Mod::botSaveFileName = %filebase;
    %filebase.setValue();
    return %filebase;
};
