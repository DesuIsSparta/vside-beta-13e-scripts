function worldControlPanel::open(%this) {
    if (!($player.rolesPermissionCheckNoWarn("staffPanelMain"))) {
        return;
    }
    %this.setVisible(1);
    %this.focusAndRaise();
    gui_DevOpts_SetTexturesButtons();
};
function worldControlPanel::close(%this) {
    %this.setVisible(0);
    playGui.focusTopWindow();
    return 1;
};
function interiorRenderModeNext() {
    interiorRenderModeSet((1.0 + getInteriorRenderMode()));
};
function interiorRenderModePrev() {
    interiorRenderModeSet((1.0 - getInteriorRenderMode()));
};
function interiorRenderModeSet(%mode) {
    setInteriorRenderMode(%mode);
    %mode = getInteriorRenderMode();
    %mode.setValue();
    %mode[$interiorRenderModeNames @ %mode].setValue();
};
function interiorRenderModeTextChange() {
    interiorRenderModeSet(guiCtrlInteriorRenderMode.getValue());
};
function gui_DevOpts_ShowCamPos() {
    showHere = $UserPref::ETS::ShowCamPos @ TheShapeNameHud;
};
function gui_DevOpts_Toggle_WorldTextureLobotomyFile() {
    if (($DevPref::OpenGL::WorldTextureLobotomyFile $= "")) {
        $DevPref::OpenGL::WorldTextureLobotomyFile = "platform/client/ui/paperdolls/greychecks";
    }
    $DevPref::OpenGL::WorldTextureLobotomyFile = "";
    MessageBoxOK("lobotomize textures", "you will need to restart vSide for this to take effect", "");
    gui_DevOpts_SetTexturesButtons();
};
function gui_DevOpts_Toggle_PlayerTextureLobotomyFile() {
    if (($DevPref::OpenGL::PlayerTextureLobotomyFile $= "")) {
        $DevPref::OpenGL::PlayerTextureLobotomyFile = "projects/common/worlds/disco_floor";
    }
    $DevPref::OpenGL::PlayerTextureLobotomyFile = "";
    MessageBoxOK("lobotomize textures", "you will need to restart vSide for this to take effect", "");
    gui_DevOpts_SetTexturesButtons();
};
function gui_DevOpts_SetTexturesButtons() {
    (ge_LocalOpts_WorldTextures @ " " @ $DevPref::OpenGL::WorldTextureLobotomyFile $= "").setValue();
    (ge_LocalOpts_PlayerTextures @ " " @ $DevPref::OpenGL::PlayerTextureLobotomyFile $= "").setValue();
};
