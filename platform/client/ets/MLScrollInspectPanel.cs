function MLScrollInspectPanel::OnInspect(%this, %mlTextfileName) {
    %fo = new FileObject("");
    if (%mlTextfileName.openForRead(%fo)) {
        %text = "";
        while (!(%fo.isEOF())) {
            %text = %text @ %fo.readLine() @ "\n";
        }
        %text.setText(InspectPanelMLText);
        %this.open();
    }
    "I can't find the file: " @ %mlTextfileName.setText(InspectPanelMLText);
    %fo.delete();
};
function clientCmdShowInspectionPanel(%mlTextfileName) {
    %mlTextfileName.OnInspect(MLScrollInspectPanel);
};
function InspectPanelMLText::onURL(%this, %url) {
    %url.OnInspect(MLScrollInspectPanel);
};
function MLScrollInspectPanel::toggle(%this) {
    %this.showRaiseOrHide(PlayGui);
};
function MLScrollInspectPanel::updateSize(%this) {
    %screenWidth = getWord($UserPref::Video::Resolution, 0);
    %screenHeight = getWord($UserPref::Video::Resolution, 1);
    %posX = 0;
    %posY = 0;
    %width = 358;
    %height = 243;
    if ((%screenHeight == 272.0)) {
    }
    if ((%screenWidth == 480.0)) {
        %height = 243;
        %width = 358;
        %posX = ((%screenWidth - %width) / 2.0);
        %posY = 0;
    }
    if ((%screenHeight == 363.0)) {
    }
    if ((%screenWidth == 640.0)) {
        %height = (161.0 * 2.0);
        %width = (161.0 * 3.0);
        %posX = ((%screenWidth - %width) / 2.0);
        %posY = 0;
    }
    if ((%screenHeight == 544.0)) {
    }
    if ((%screenWidth == 960.0)) {
        %height = (242.0 * 2.0);
        %width = (242.0 * 3.0);
        %posX = ((%screenWidth - %width) / 2.0);
        %posY = 0;
    }
    if ((%screenHeight == 714.0)) {
    }
    if ((%screenWidth == 1260.0)) {
        %height = (317.0 * 2.0);
        %width = (317.0 * 3.0);
        %posX = ((%screenWidth - %width) / 2.0);
        %posY = 0;
    }
    %height.resize(%this, %posX, %posY, %width);
    InspectPanelScrollControl.scrollToTop();
};
function MLScrollInspectPanel::open(%this) {
    1.setVisible(%this);
    1.setConstrained(%this);
    %this.focusAndRaise(PlayGui);
    %this.updateSize();
    1.makeFirstResponder(InspectPanelScrollControl);
};
function MLScrollInspectPanel::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    return 1;
};
