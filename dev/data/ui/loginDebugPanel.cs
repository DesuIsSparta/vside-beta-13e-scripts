function loginDebugPanel::toggle(%this)
{
    LoginGui.showRaiseOrHide(%this);
    return ;
}
function loginDebugPanel::open(%this)
{
    if (!%this.isVisible())
    {
        %this.setVisible(1);
        LoginGui.focusAndRaise(%this);
    }
    DragAndDropExampleList.Initialize();
    return ;
}
function loginDebugPanel::close(%this)
{
    %this.setVisible(0);
    LoginGui.focusTopWindow();
    return ;
}
