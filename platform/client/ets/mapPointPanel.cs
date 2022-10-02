function MapPointPanel::toggle(%this)
{
    PlayGui.showRaiseOrHide(%this);
    return ;
}
function MapPointPanel::open(%this)
{
    if (!%this.isVisible())
    {
        %this.setVisible(1);
        PlayGui.focusAndRaise(%this);
    }
    return ;
}
function MapPointPanel::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
}
