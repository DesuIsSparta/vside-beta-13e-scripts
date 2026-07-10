function toggleSnapshotAvatarTool() {
    PlayGui.showRaiseOrHide(snapshotAvatarTool);
};
function snapshotAvatarTool::open(%this) {
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    snapshotAvatarToolActiveRegion.initStuff();
};
function snapshotAvatarTool::onWake(%this) {
    snapshotAvatarToolActiveRegion.initStuff();
};
function snapshotAvatarTool::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
};
function snapshotAvatarToolActiveRegion::initStuff(%this) {
    if (!(isObject($player))) {
        warn("Snapshot", "initstuff: $player invalid");
        return;
    }
    %this.setSimObject($player);
    %this.cameraXRotMin = -(0.3);
    %this.cameraXRotMax = 0.1;
    %this.adjustForHeight($UserPref::Player::height, 0.3, 1.1);
    %this.setOrbitDistMin(0.4);
    %this.setOrbitDistMax(0.7);
    %this.setOrbitDist(0.5);
    %this.setLightDirection("0 3 -2");
    %anim = $player.getGender() @ "pidl1a";
    $player.playAnim(%anim);
};
function snapshotAvatarToolActiveRegion::adjustForHeight(%this, %height, %cMin, %cMax) {
    %hMin = 0.7;
    %hMax = 1.2;
    %h = ((%hMin - %hMax) / (%hMin - %height));
    %c = (%cMin + ((%cMin - %cMax) * %h));
    %this.setLookAtNudge("0 0" @ " " @ %c);
};
function snapshotAvatarTool::doSnap(%this) {
    gSetField(%this, lastFrame, $Canvas::frameCount);
    gSetField(%this, origProfile, snapshotAvatarToolActiveRegion, %this.profile);
    snapshotAvatarToolActiveRegion.setProfile(ETSSnapshotBackgroundProfile);
    %this.waitForNextFrameToSnap();
};
function snapshotAvatarTool::waitForNextFrameToSnap(%this) {
    cancel(waitForFrameSchedule, gGetField(%this));
    if ((gGetField(%this) <= $Canvas::frameCount)) {
        gSetField(%this, waitForFrameSchedule, %this.schedule(10, "waitForNextFrameToSnap"));
        return lastFrame;
    }
    %this.doSnap2();
};
function snapshotAvatarTool::doSnap2(%this) {
    %snapshot = snapshot::snapAndUpControlRegion(snapshotAvatarToolActiveRegion, $player.getShapeName(), "y");
    if (!(isObject(%snapshot))) {
        error("Snapshot", "Problem taking snapshot");
        return;
    }
    %snapshot.saveObject = %this;
    %snapshot.setCompletedCallback("snapshotAvatarToolonCompleted");
    snapshotAvatarToolSet1.setVisible(0);
    snapshotAvatarToolSet2.setVisible(1);
    snapshotAvatarToolProgressBar.setValue(0);
    snapshotAvatarToolActiveRegion.setProfile(origProfile, gGetField(%this));
};
function snapshotAvatarTool::onProgress(%this, %snapshot) {
    %percent = (%snapshot.ulTotal / %snapshot.ulNow);
    snapshotAvatarToolProgressBar.setValue(%percent);
};
function snapshotAvatarToolonCompleted(%request, %result) {
    %snapshot = %request.saveObject;
    if ((0.0 == %result)) {
        snapshotAvatarToolSet1.setVisible(1);
        snapshotAvatarToolSet2.setVisible(0);
        if (!(%snapshot.visitWhenDoneUrl $= "")) {
        }
        if ($UserPref::Snapshots::View) {
            gotoWebPage(%snapshot.visitWhenDoneUrl);
        }
    }
    snapshotAvatarToolSet1.setVisible(1);
    snapshotAvatarToolSet2.setVisible(0);
};
