function MapPointPanel::toggle(%this)
{
    PlayGui.showRaiseOrHide(%this);
}
function MapPointPanel::open(%this)
{
    if (!%this.isVisible())
    {
        %this.setVisible(1);
        PlayGui.focusAndRaise(%this);
    }
}
function MapPointPanel::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
}
