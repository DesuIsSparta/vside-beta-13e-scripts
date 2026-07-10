function toggleSnapshotTool() {
    snapshotTool.showRaiseOrHide(PlayGui);
};
function snapshotTool::open(%this) {
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
};
function snapshotTool::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    return 1;
};
function snapshotTool::doSnap(%this) {
    gSetField(%this, lastFrame, $Canvas::frameCount);
    gSetField(%this, origProfile, ClosetMainObjectView.profile);
    ETSSnapshotBackgroundProfile.setProfile(ClosetMainObjectView);
    0.setVisible(snapshotToolActiveRegion);
    %this.waitForNextFrameToSnap();
};
function snapshotTool::waitForNextFrameToSnap(%this) {
    cancel(gGetField(%this, waitForFrameSchedule));
    if (($Canvas::frameCount <= gGetField(%this, lastFrame))) {
        gSetField(%this, waitForFrameSchedule, "waitForNextFrameToSnap".schedule(%this, 10));
        return;
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
    "snapshotToolonComplete".setCompletedCallback(%snapshot);
    0.setVisible(snapshotToolSet1);
    1.setVisible(snapshotToolSet2);
    1.setVisible(snapshotToolActiveRegion);
    0.setValue(snapshotToolProgressBar);
    gGetField(%this, origProfile).setProfile(ClosetMainObjectView);
};
function snapshotTool::onProgress(%this, %snapshot) {
    %percent = (%snapshot.ulNow / %snapshot.ulTotal);
    %percent.setValue(snapshotToolProgressBar);
};
function snapshotToolonComplete(%request, %result) {
    %snapshot = %request.saveObject;
    if ((%result == 0.0)) {
        1.setVisible(snapshotToolSet1);
        0.setVisible(snapshotToolSet2);
        if (!(%snapshot.visitWhenDoneUrl $= "")) {
        }
        if ($UserPref::Snapshots::View) {
            gotoWebPage(%snapshot.visitWhenDoneUrl);
        }
    }
    1.setVisible(snapshotToolSet1);
    0.setVisible(snapshotToolSet2);
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
