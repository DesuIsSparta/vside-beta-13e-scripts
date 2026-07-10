function toggleSnapshotAvatarTool() {
    PlayGui.showRaiseOrHide(snapshotAvatarTool);
};
function snapshotAvatarTool::open(%this) {
    %this.setVisible(1);
    %this.focusAndRaise();
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
    gSetField(%this, $Canvas::frameCount);
    gSetField(%this, %this.profile);
    snapshotAvatarToolActiveRegion.setProfile(ETSSnapshotBackgroundProfile);
    %this.waitForNextFrameToSnap();
};
function snapshotAvatarTool::waitForNextFrameToSnap(%this) {
    cancel(gGetField(%this));
    if ((gGetField(%this) <= $Canvas::frameCount)) {
        gSetField(%this, %this.schedule(10, "waitForNextFrameToSnap"));
        return waitForFrameSchedule;
    }
    %this.doSnap2();
};
function snapshotAvatarTool::doSnap2(%this) {
    %snapshot = snapshot::snapAndUpControlRegion($player.getShapeName(), "y");
    snapshotAvatarToolActiveRegion;
    if (!(isObject(%snapshot))) {
        error("Snapshot", "Problem taking snapshot");
        return;
    }
    %snapshot.saveObject = %this;
    %snapshot.setCompletedCallback("snapshotAvatarToolonCompleted");
    0.setVisible();
    1.setVisible();
    0.setValue();
    gGetField(%this).setProfile();
};
function snapshotAvatarTool::onProgress(%this, %snapshot) {
    %percent = (%snapshot.ulTotal / %snapshot.ulNow);
    %percent.setValue();
};
function snapshotAvatarToolonCompleted(%request, %result) {
    %snapshot = %request.saveObject;
    if ((0.0 == %result)) {
        1.setVisible();
        0.setVisible();
        if (!(snapshotAvatarToolSet2 @ " " @ %snapshot.visitWhenDoneUrl $= "")) {
        }
        if ($UserPref::Snapshots::View) {
            gotoWebPage(%snapshot.visitWhenDoneUrl);
        }
    }
    1.setVisible();
    0.setVisible();
};
