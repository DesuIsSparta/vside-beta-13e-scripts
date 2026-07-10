function MapPointPanel::toggle(%this) {
    %this.showRaiseOrHide(PlayGui);
};
function MapPointPanel::open(%this) {
    if (!(%this.isVisible())) {
        1.setVisible(%this);
        %this.focusAndRaise(PlayGui);
    }
};
function MapPointPanel::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    return 1;
};
