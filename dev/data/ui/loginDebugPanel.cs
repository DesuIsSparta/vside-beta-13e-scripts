function loginDebugPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function loginDebugPanel::open(%this) {
    if (!(%this.isVisible())) {
        %this.setVisible(1);
        %this.focusAndRaise();
    }
    DragAndDropExampleList.Initialize();
};
function loginDebugPanel::close(%this) {
    %this.setVisible(0);
    LoginGui.focusTopWindow();
};
