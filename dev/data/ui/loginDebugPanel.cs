function loginDebugPanel::toggle(%this) {
    LoginGui.showRaiseOrHide(%this);
};
function loginDebugPanel::open(%this) {
    if (!%this.isVisible()) {
        %this.setVisible(1);
        LoginGui.focusAndRaise(%this);
    }
    DragAndDropExampleList.Initialize();
};
function loginDebugPanel::close(%this) {
    %this.setVisible(0);
    LoginGui.focusTopWindow();
};
