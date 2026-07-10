function MapPointPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function MapPointPanel::open(%this) {
    %this.setVisible(1);
    %this.focusAndRaise();
};
function MapPointPanel::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
