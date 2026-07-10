function performerPanel::toggle(%this) {
    if (!(%this.isVisible())) {
        %this.open();
    }
    %this.close();
};
function performerPanel::open(%this) {
    if (!($gPerformerMode)) {
        return;
    }
    if (!(%this.isVisible())) {
        1.setVisible(%this);
        %this.focusAndRaise(PlayGui);
    }
};
function performerPanel::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    return 1;
};
