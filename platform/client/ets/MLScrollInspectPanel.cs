function MLScrollInspectPanel::OnInspect(%this, %mlTextfileName) {
    %fo = new FileObject("");
    if (%fo.openForRead(%mlTextfileName)) {
        %text = "";
        while (!%fo.isEOF()) {
            %text = %text @ %fo.readLine() @ "\n";
        }
        InspectPanelMLText.setText(%text);
        %this.open();
    }
    InspectPanelMLText.setText("I can't find the file: " @ %mlTextfileName);
    %fo.delete();
};
function clientCmdShowInspectionPanel(%mlTextfileName) {
    MLScrollInspectPanel.OnInspect(%mlTextfileName);
};
function InspectPanelMLText::onURL(%this, %url) {
    MLScrollInspectPanel.OnInspect(%url);
};
function MLScrollInspectPanel::toggle(%this) {
    PlayGui.showRaiseOrHide(%this);
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
    %this.resize(%posX, %posY, %width, %height);
    InspectPanelScrollControl.scrollToTop();
};
function MLScrollInspectPanel::open(%this) {
    %this.setVisible(1);
    %this.setConstrained(1);
    PlayGui.focusAndRaise(%this);
    %this.updateSize();
    InspectPanelScrollControl.makeFirstResponder(1);
};
function MLScrollInspectPanel::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
};
