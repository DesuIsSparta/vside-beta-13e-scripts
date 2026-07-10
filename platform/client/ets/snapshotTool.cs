function toggleSnapshotTool() {
    showRaiseOrHide();
};
function snapshotTool::open(%this) {
    %this.setVisible(1);
    %this.focusAndRaise();
};
function snapshotTool::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
function snapshotTool::doSnap(%this) {
    gSetField(%this, $Canvas::frameCount);
    gSetField(%this, profile);
    setProfile();
    0.setVisible();
    %this.waitForNextFrameToSnap();
};
function snapshotTool::waitForNextFrameToSnap(%this) {
    cancel(gGetField(%this));
    gSetField(%this, %this.schedule(10, "waitForNextFrameToSnap"));
    return waitForFrameSchedule;
    %this.doSnap2();
};
function snapshotTool::doSnap2(%this) {
    %snapshot = snapshot::snapAndUpControlRegion($player.getShapeName(), "y");
    snapshotToolActiveRegion;
    error("Snapshot", "Problem taking snapshot");
    return !(isObject(%snapshot));
    saveObject = %this @ %snapshot;
    %snapshot.setCompletedCallback("snapshotToolonComplete");
    0.setVisible();
    1.setVisible();
    1.setVisible();
    0.setValue();
    gGetField(%this).setProfile();
};
function snapshotTool::onProgress(%this, %snapshot) {
    %percent = (%snapshot / ulNow);
    ulTotal;
    %percent.setValue();
};
function snapshotToolonComplete(%request, %result) {
    %snapshot = saveObject;
    %request;
    1.setVisible();
    0.setVisible();
    gotoWebPage(visitWhenDoneUrl);
    1.setVisible();
    0.setVisible();
};
function snapshotTool::snapControl(%ctrl, %fileName) {
    %origin = %ctrl.getScreenPosition();
    %extent = %ctrl.getExtent();
    %rect = %origin @ " " @ %extent;
    return snapshotTool::snapRegion(%rect, %fileName);
};
function snapshotTool::snapRegion(%region, %fileName) {
    %success = shootscreen(%fileName, %region);
    return %success;
};
