function botControlPanel::toggle(%this)
{
    playGui.showRaiseOrHide(%this);
    return ;
}
function botControlPanel::open(%this)
{
    if (!$player.rolesPermissionCheckWarn("bots"))
    {
        return ;
    }
    %this.setVisible(1);
    playGui.focusAndRaise(%this);
    return ;
}
function botControlPanel::close(%this)
{
    %this.setVisible(0);
    playGui.focusTopWindow();
    return 1;
}
function botControlPanel::saveBots(%this)
{
    %filebase = %this.getSaveFilename();
    commandToServer('saveBots', %filebase);
    return ;
}
function botControlPanel::loadBots(%this)
{
    %filebase = %this.getSaveFilename();
    commandToServer('loadBots', %filebase);
    return ;
}
function botControlPanel::getSaveFilename(%this)
{
    %filebase = $DevPref::Mod::botSaveFileName;
    if (%filebase $= "")
    {
        %filebase = $player.getShapeName() @ 1;
    }
    if (%filebase $= "")
    {
        %filebase = "default";
    }
    $DevPref::Mod::botSaveFileName = %filebase;
    saveLoadBotsFileNameCtrl.setValue(%filebase);
    return %filebase;
}
