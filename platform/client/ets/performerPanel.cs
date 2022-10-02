function performerPanel::toggle(%this)
{
    if (!%this.isVisible())
    {
        %this.open();
    }
    else
    {
        %this.close();
    }
    return ;
}
function performerPanel::open(%this)
{
    if (!$gPerformerMode)
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
function performerPanel::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
}
