function worldControlPanel::open(%this)
{
    if (!$player.rolesPermissionCheckNoWarn("staffPanelMain"))
    {
        return ;
    }
    %this.setVisible(1);
    playGui.focusAndRaise(%this);
    gui_DevOpts_SetTexturesButtons();
    return ;
}
function worldControlPanel::close(%this)
{
    %this.setVisible(0);
    playGui.focusTopWindow();
    return 1;
}
function interiorRenderModeNext()
{
    interiorRenderModeSet(getInteriorRenderMode() + 1);
    return ;
}
function interiorRenderModePrev()
{
    interiorRenderModeSet(getInteriorRenderMode() - 1);
    return ;
}
$interiorRenderModeNames[0] = "normal";
$interiorRenderModeNames[1] = "lines";
$interiorRenderModeNames[2] = "detail polys";
$interiorRenderModeNames[3] = "ambiguous polys";
$interiorRenderModeNames[4] = "orphaned polys";
$interiorRenderModeNames[5] = "lightmap";
$interiorRenderModeNames[6] = "only textures";
$interiorRenderModeNames[7] = "portal zones";
$interiorRenderModeNames[8] = "ambient lit";
$interiorRenderModeNames[9] = "collision fans";
$interiorRenderModeNames[10] = "triangle strips";
$interiorRenderModeNames[11] = "null surfaces";
$interiorRenderModeNames[12] = "large textures";
$interiorRenderModeNames[13] = "hull surfaces";
$interiorRenderModeNames[14] = "vehicle hulls";
$interiorRenderModeNames[15] = "vertex color";
$interiorRenderModeNames[16] = "detail level";
$interiorRenderModeNames[17] = "portal zones nonRoot";
$interiorRenderModeNames[18] = "zonesNonRoot, Detail";
$interiorRenderModeNames[19] = "portals";
$interiorRenderModeNames[20] = "transparent polys";
function interiorRenderModeSet(%mode)
{
    setInteriorRenderMode(%mode);
    %mode = getInteriorRenderMode();
    guiCtrlInteriorRenderMode.setValue(%mode);
    guiCtrlInteriorRenderModeName.setValue($interiorRenderModeNames[%mode]);
    return ;
}
function interiorRenderModeTextChange()
{
    interiorRenderModeSet(guiCtrlInteriorRenderMode.getValue());
    return ;
}
function gui_DevOpts_ShowCamPos()
{
    TheShapeNameHud.showHere = $UserPref::ETS::ShowCamPos;
    return ;
}
function gui_DevOpts_Toggle_WorldTextureLobotomyFile()
{
    if ($DevPref::OpenGL::WorldTextureLobotomyFile $= "")
    {
        $DevPref::OpenGL::WorldTextureLobotomyFile = "platform/client/ui/paperdolls/greychecks";
    }
    else
    {
        $DevPref::OpenGL::WorldTextureLobotomyFile = "";
    }
    MessageBoxOK("lobotomize textures", "you will need to restart vSide for this to take effect", "");
    gui_DevOpts_SetTexturesButtons();
    return ;
}
function gui_DevOpts_Toggle_PlayerTextureLobotomyFile()
{
    if ($DevPref::OpenGL::PlayerTextureLobotomyFile $= "")
    {
        $DevPref::OpenGL::PlayerTextureLobotomyFile = "projects/common/worlds/disco_floor";
    }
    else
    {
        $DevPref::OpenGL::PlayerTextureLobotomyFile = "";
    }
    MessageBoxOK("lobotomize textures", "you will need to restart vSide for this to take effect", "");
    gui_DevOpts_SetTexturesButtons();
    return ;
}
function gui_DevOpts_SetTexturesButtons()
{
    ge_LocalOpts_WorldTextures.setValue($DevPref::OpenGL::WorldTextureLobotomyFile $= "");
    ge_LocalOpts_PlayerTextures.setValue($DevPref::OpenGL::PlayerTextureLobotomyFile $= "");
    return ;
}
