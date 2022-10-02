function staffPanel::toggle(%this)
{
    playGui.showRaiseOrHide(%this);
    return ;
}
function staffPanel::open(%this)
{
    if (!$player.rolesPermissionCheckNoWarn("debugPassive"))
    {
        return ;
    }
    %this.setVisible(1);
    playGui.focusAndRaise(%this);
    return ;
}
function staffPanel::close(%this)
{
    %this.setVisible(0);
    playGui.focusTopWindow();
    return 1;
}
