function MLScrollInspectPanel::OnInspect(%this, %mlTextfileName) {
    %fo = new ""();;
    FileObject;
    if (%fo.openForRead(%mlTextfileName)) {
        %text = "";
        0;
        if (!(%fo.isEOF())) {
            %text = %text @ %fo.readLine() @ "\n";
        }
        %text.setText();
        %this.open();
    }
    "I can't find the file: " @ %mlTextfileName.setText();
    %fo.delete();
};
function clientCmdShowInspectionPanel(%mlTextfileName) {
    %mlTextfileName.OnInspect();
};
function InspectPanelMLText::onURL(%this, %url) {
    %url.OnInspect();
};
function MLScrollInspectPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function MLScrollInspectPanel::updateSize(%this) {
    %screenWidth = getWord($UserPref::Video::Resolution, 0);
    %screenHeight = getWord($UserPref::Video::Resolution, 1);
    %posX = 0;
    %posY = 0;
    %width = 358;
    %height = 243;
    if ((272.0 == %screenHeight)) {
    }
    if ((480.0 == %screenWidth)) {
        %height = 243;
        %width = 358;
        %posX = (2.0 / (%width - %screenWidth));
        %posY = 0;
    }
    if ((363.0 == %screenHeight)) {
    }
    if ((640.0 == %screenWidth)) {
        %height = (2.0 * 161.0);
        %width = (3.0 * 161.0);
        %posX = (2.0 / (%width - %screenWidth));
        %posY = 0;
    }
    if ((544.0 == %screenHeight)) {
    }
    if ((960.0 == %screenWidth)) {
        %height = (2.0 * 242.0);
        %width = (3.0 * 242.0);
        %posX = (2.0 / (%width - %screenWidth));
        %posY = 0;
    }
    if ((714.0 == %screenHeight)) {
    }
    if ((1260.0 == %screenWidth)) {
        %height = (2.0 * 317.0);
        %width = (3.0 * 317.0);
        %posX = (2.0 / (%width - %screenWidth));
        %posY = 0;
    }
    %this.resize(%posX, %posY, %width, %height);
    InspectPanelScrollControl.scrollToTop();
};
function MLScrollInspectPanel::open(%this) {
    %this.setVisible(1);
    %this.setConstrained(1);
    %this.focusAndRaise();
    %this.updateSize();
    1.makeFirstResponder();
};
function MLScrollInspectPanel::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
};
