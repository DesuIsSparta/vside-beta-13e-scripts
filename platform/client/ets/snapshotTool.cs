function toggleSnapshotTool() {
    PlayGui.showRaiseOrHide(snapshotTool);
};
function snapshotTool::open(%this) {
    %this.setVisible(1);
    %this.focusAndRaise();
};
function snapshotTool::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
};
function snapshotTool::doSnap(%this) {
    gSetField(%this, $Canvas::frameCount);
    gSetField(%this, profile);
    ClosetMainObjectView.setProfile(ETSSnapshotBackgroundProfile);
    0.setVisible();
    %this.waitForNextFrameToSnap();
};
function snapshotTool::waitForNextFrameToSnap(%this) {
    cancel(gGetField(%this));
    if ((gGetField(%this) <= $Canvas::frameCount)) {
        gSetField(%this, %this.schedule(10, "waitForNextFrameToSnap"));
        return waitForFrameSchedule;
    }
    %this.doSnap2();
};
function snapshotTool::doSnap2(%this) {
    %snapshot = snapshot::snapAndUpControlRegion($player.getShapeName(), "y");
    snapshotToolActiveRegion;
    if (!(isObject(%snapshot))) {
        error("Snapshot", "Problem taking snapshot");
        return;
    }
    %snapshot.saveObject = %this;
    %snapshot.setCompletedCallback("snapshotToolonComplete");
    0.setVisible();
    1.setVisible();
    1.setVisible();
    0.setValue();
    gGetField(%this).setProfile();
};
function snapshotTool::onProgress(%this, %snapshot) {
    %percent = (%snapshot.ulTotal / %snapshot.ulNow);
    %percent.setValue();
};
function snapshotToolonComplete(%request, %result) {
    %snapshot = %request.saveObject;
    if ((0.0 == %result)) {
        1.setVisible();
        0.setVisible();
        if (!(snapshotToolSet2 @ " " @ %snapshot.visitWhenDoneUrl $= "")) {
        }
        if ($UserPref::Snapshots::View) {
            gotoWebPage(%snapshot.visitWhenDoneUrl);
        }
    }
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
