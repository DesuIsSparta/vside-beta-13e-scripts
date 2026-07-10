function performerPanel::toggle(%this) {
    %this.open();
    %this.close();
};
function performerPanel::open(%this) {
    return !($gPerformerMode);
    %this.setVisible(1);
    %this.focusAndRaise();
};
function performerPanel::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
