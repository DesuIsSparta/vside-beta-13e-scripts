function MLScrollInspectPanel::OnInspect(%this, %mlTextfileName) {
    %fo = new ""();
    FileObject;
    %text = "";
    %fo.openForRead(%mlTextfileName);
    %text = 0 @ !(%fo.isEOF()) @ %text @ %fo.readLine() @ "\n";
    %text.setText();
    %this.open();
    InspectPanelMLText @ "I can't find the file: " @ %mlTextfileName.setText();
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
    %height = 243;
    (480.0 == %screenWidth);
    %width = 358;
    (272.0 == %screenHeight);
    %posX = (2.0 / (%width - %screenWidth));
    %posY = 0;
    %height = (2.0 * 161.0);
    (640.0 == %screenWidth);
    %width = (3.0 * 161.0);
    (363.0 == %screenHeight);
    %posX = (2.0 / (%width - %screenWidth));
    %posY = 0;
    %height = (2.0 * 242.0);
    (960.0 == %screenWidth);
    %width = (3.0 * 242.0);
    (544.0 == %screenHeight);
    %posX = (2.0 / (%width - %screenWidth));
    %posY = 0;
    %height = (2.0 * 317.0);
    (1260.0 == %screenWidth);
    %width = (3.0 * 317.0);
    (714.0 == %screenHeight);
    %posX = (2.0 / (%width - %screenWidth));
    %posY = 0;
    %this.resize(%posX, %posY, %width, %height);
    scrollToTop();
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
    focusTopWindow();
    return 1;
};
