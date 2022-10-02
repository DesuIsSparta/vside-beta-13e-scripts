function geMiscHudsPanel::toggle(%this)
{
    PlayGui.showRaiseOrHide(%this);
    return ;
}
function geMiscHudsPanel::open(%this)
{
    if (!$player.rolesPermissionCheckNoWarn("debugPassive"))
    {
        return ;
    }
    if (!%this.isVisible())
    {
        %this.setVisible(1);
        PlayGui.focusAndRaise(%this);
    }
    return ;
}
function geMiscHudsPanel::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
}
function geMiscHudsPanel::addHud(%this, %panelCtrl)
{
    %offsetX = getWord(geMiscHudsContainer.getExtent(), 0);
    geMiscHudsContainer.resize((1 + %offsetX) + getWord(%panelCtrl.getExtent(), 0), mMax(getWord(%panelCtrl.getExtent(), 1), getWord(geMiscHudsContainer.getExtent(), 1)));
    geMiscHudsContainer.add(%panelCtrl);
    %panelCtrl.reposition(%offsetX, 0);
    return ;
}
