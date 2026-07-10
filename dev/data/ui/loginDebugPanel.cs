function loginDebugPanel::toggle(%this) {
    %this.showRaiseOrHide(LoginGui);
};
function loginDebugPanel::open(%this) {
    if (!(%this.isVisible())) {
        1.setVisible(%this);
        %this.focusAndRaise(LoginGui);
    }
    DragAndDropExampleList.Initialize();
};
function loginDebugPanel::close(%this) {
    0.setVisible(%this);
    LoginGui.focusTopWindow();
};
