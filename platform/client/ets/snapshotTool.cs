function toggleSnapshotTool() {
    PlayGui.showRaiseOrHide(snapshotTool);
};
function snapshotTool::open(%this) {
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
};
function snapshotTool::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
};
function snapshotTool::doSnap(%this) {
    gSetField(%this, lastFrame, $Canvas::frameCount);
    gSetField(%this, origProfile, ClosetMainObjectView, profile);
    ClosetMainObjectView.setProfile(ETSSnapshotBackgroundProfile);
    snapshotToolActiveRegion.setVisible(0);
    %this.waitForNextFrameToSnap();
};
function snapshotTool::waitForNextFrameToSnap(%this) {
    cancel(waitForFrameSchedule, gGetField(%this));
    if ((gGetField(%this) <= $Canvas::frameCount)) {
        gSetField(%this, waitForFrameSchedule, %this.schedule(10, "waitForNextFrameToSnap"));
        return lastFrame;
    }
    %this.doSnap2();
};
function snapshotTool::doSnap2(%this) {
    %snapshot = snapshot::snapAndUpControlRegion(snapshotToolActiveRegion, $player.getShapeName(), "y");
    if (!(isObject(%snapshot))) {
        error("Snapshot", "Problem taking snapshot");
        return;
    }
    %snapshot.saveObject = %this;
    %snapshot.setCompletedCallback("snapshotToolonComplete");
    snapshotToolSet1.setVisible(0);
    snapshotToolSet2.setVisible(1);
    snapshotToolActiveRegion.setVisible(1);
    snapshotToolProgressBar.setValue(0);
    ClosetMainObjectView.setProfile(origProfile, gGetField(%this));
};
function snapshotTool::onProgress(%this, %snapshot) {
    %percent = (%snapshot.ulTotal / %snapshot.ulNow);
    snapshotToolProgressBar.setValue(%percent);
};
function snapshotToolonComplete(%request, %result) {
    %snapshot = %request.saveObject;
    if ((0.0 == %result)) {
        snapshotToolSet1.setVisible(1);
        snapshotToolSet2.setVisible(0);
        if (!(%snapshot.visitWhenDoneUrl $= "")) {
        }
        if ($UserPref::Snapshots::View) {
            gotoWebPage(%snapshot.visitWhenDoneUrl);
        }
    }
    snapshotToolSet1.setVisible(1);
    snapshotToolSet2.setVisible(0);
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
