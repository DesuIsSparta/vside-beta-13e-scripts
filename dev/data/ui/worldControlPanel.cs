function worldControlPanel::open(%this) {
    if (!("staffPanelMain".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    1.setVisible(%this);
    %this.focusAndRaise(playGui);
    gui_DevOpts_SetTexturesButtons();
};
function worldControlPanel::close(%this) {
    0.setVisible(%this);
    playGui.focusTopWindow();
    return 1;
};
function interiorRenderModeNext() {
    interiorRenderModeSet((getInteriorRenderMode() + 1.0));
};
function interiorRenderModePrev() {
    interiorRenderModeSet((getInteriorRenderMode() - 1.0));
};
function interiorRenderModeSet(%mode) {
    setInteriorRenderMode(%mode);
    %mode = getInteriorRenderMode();
    %mode.setValue(guiCtrlInteriorRenderMode);
    %mode[$interiorRenderModeNames @ %mode].setValue(guiCtrlInteriorRenderModeName);
};
function interiorRenderModeTextChange() {
    interiorRenderModeSet(guiCtrlInteriorRenderMode.getValue());
};
function gui_DevOpts_ShowCamPos() {
    TheShapeNameHud.showHere = $UserPref::ETS::ShowCamPos;
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
    ($DevPref::OpenGL::WorldTextureLobotomyFile $= "").setValue(ge_LocalOpts_WorldTextures);
    ($DevPref::OpenGL::PlayerTextureLobotomyFile $= "").setValue(ge_LocalOpts_PlayerTextures);
};
