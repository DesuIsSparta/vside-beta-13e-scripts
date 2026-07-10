function toggleSnapshotAvatarTool() {
    snapshotAvatarTool.showRaiseOrHide(PlayGui);
};
function snapshotAvatarTool::open(%this) {
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    snapshotAvatarToolActiveRegion.initStuff();
};
function snapshotAvatarTool::onWake(%this) {
    snapshotAvatarToolActiveRegion.initStuff();
};
function snapshotAvatarTool::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    return 1;
};
function snapshotAvatarToolActiveRegion::initStuff(%this) {
    if (!(isObject($player))) {
        warn("Snapshot", "initstuff: $player invalid");
        return;
    }
    $player.setSimObject(%this);
    %this.cameraXRotMin = -(0.3);
    %this.cameraXRotMax = 0.1;
    1.1.adjustForHeight(%this, $UserPref::Player::height, 0.3);
    0.4.setOrbitDistMin(%this);
    0.7.setOrbitDistMax(%this);
    0.5.setOrbitDist(%this);
    "0 3 -2".setLightDirection(%this);
    %anim = $player.getGender() @ "pidl1a";
    %anim.playAnim($player);
};
function snapshotAvatarToolActiveRegion::adjustForHeight(%this, %height, %cMin, %cMax) {
    %hMin = 0.7;
    %hMax = 1.2;
    %h = ((%height - %hMin) / (%hMax - %hMin));
    %c = ((%h * (%cMax - %cMin)) + %cMin);
    "0 0" @ " " @ %c.setLookAtNudge(%this);
};
function snapshotAvatarTool::doSnap(%this) {
    gSetField(%this, lastFrame, $Canvas::frameCount);
    gSetField(%this, origProfile, snapshotAvatarToolActiveRegion, %this.profile);
    ETSSnapshotBackgroundProfile.setProfile(snapshotAvatarToolActiveRegion);
    %this.waitForNextFrameToSnap();
};
function snapshotAvatarTool::waitForNextFrameToSnap(%this) {
    cancel(waitForFrameSchedule, gGetField(%this));
    if (($Canvas::frameCount <= gGetField(%this))) {
        gSetField(%this, waitForFrameSchedule, "waitForNextFrameToSnap".schedule(%this, 10));
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
    "snapshotAvatarToolonCompleted".setCompletedCallback(%snapshot);
    0.setVisible(snapshotAvatarToolSet1);
    1.setVisible(snapshotAvatarToolSet2);
    0.setValue(snapshotAvatarToolProgressBar);
    gGetField(%this).setProfile(snapshotAvatarToolActiveRegion, origProfile);
};
function snapshotAvatarTool::onProgress(%this, %snapshot) {
    %percent = (%snapshot.ulNow / %snapshot.ulTotal);
    %percent.setValue(snapshotAvatarToolProgressBar);
};
function snapshotAvatarToolonCompleted(%request, %result) {
    %snapshot = %request.saveObject;
    if ((%result == 0.0)) {
        1.setVisible(snapshotAvatarToolSet1);
        0.setVisible(snapshotAvatarToolSet2);
        if (!(%snapshot.visitWhenDoneUrl $= "")) {
        }
        if ($UserPref::Snapshots::View) {
            gotoWebPage(%snapshot.visitWhenDoneUrl);
        }
    }
    1.setVisible(snapshotAvatarToolSet1);
    0.setVisible(snapshotAvatarToolSet2);
};
