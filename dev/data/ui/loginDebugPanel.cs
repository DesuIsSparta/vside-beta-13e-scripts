function loginDebugPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function loginDebugPanel::open(%this) {
    %this.setVisible(1);
    %this.focusAndRaise();
    Initialize();
};
function loginDebugPanel::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
};
