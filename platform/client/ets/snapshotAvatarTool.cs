function toggleSnapshotAvatarTool() {
    showRaiseOrHide();
};
function snapshotAvatarTool::open(%this) {
    %this.setVisible(1);
    %this.focusAndRaise();
    initStuff();
};
function snapshotAvatarTool::onWake(%this) {
    initStuff();
};
function snapshotAvatarTool::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
function snapshotAvatarToolActiveRegion::initStuff(%this) {
    warn("Snapshot", "initstuff: $player invalid");
    return !(isObject($player));
    %this.setSimObject($player);
    cameraXRotMin = -(0.3) @ %this;
    cameraXRotMax = 0.1 @ %this;
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
    gSetField(%this, profile);
    setProfile();
    %this.waitForNextFrameToSnap();
};
function snapshotAvatarTool::waitForNextFrameToSnap(%this) {
    cancel(gGetField(%this));
    gSetField(%this, %this.schedule(10, "waitForNextFrameToSnap"));
    return waitForFrameSchedule;
    %this.doSnap2();
};
function snapshotAvatarTool::doSnap2(%this) {
    %snapshot = snapshot::snapAndUpControlRegion($player.getShapeName(), "y");
    snapshotAvatarToolActiveRegion;
    error("Snapshot", "Problem taking snapshot");
    return !(isObject(%snapshot));
    saveObject = %this @ %snapshot;
    %snapshot.setCompletedCallback("snapshotAvatarToolonCompleted");
    0.setVisible();
    1.setVisible();
    0.setValue();
    gGetField(%this).setProfile();
};
function snapshotAvatarTool::onProgress(%this, %snapshot) {
    %percent = (%snapshot / ulNow);
    ulTotal;
    %percent.setValue();
};
function snapshotAvatarToolonCompleted(%request, %result) {
    %snapshot = saveObject;
    %request;
    1.setVisible();
    0.setVisible();
    gotoWebPage(visitWhenDoneUrl);
    1.setVisible();
    0.setVisible();
};
