function toggleSnapshotAvatarTool()
{
    PlayGui.showRaiseOrHide(snapshotAvatarTool);
    return ;
}
function snapshotAvatarTool::open(%this)
{
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    snapshotAvatarToolActiveRegion.initStuff();
    return ;
}
function snapshotAvatarTool::onWake(%this)
{
    snapshotAvatarToolActiveRegion.initStuff();
    return ;
}
function snapshotAvatarTool::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
}
function snapshotAvatarToolActiveRegion::initStuff(%this)
{
    if (!isObject($player))
    {
        warn("Snapshot", "initstuff: $player invalid");
        return ;
    }
    %this.setSimObject($player);
    %this.cameraXRotMin = -0.3;
    %this.cameraXRotMax = 0.1;
    %this.adjustForHeight($UserPref::Player::height, 0.3, 1.1);
    %this.setOrbitDistMin(0.4);
    %this.setOrbitDistMax(0.7);
    %this.setOrbitDist(0.5);
    %this.setLightDirection("0 3 -2");
    %anim = $player.getGender() @ "pidl1a";
    $player.playAnim(%anim);
    return ;
}
function snapshotAvatarToolActiveRegion::adjustForHeight(%this, %height, %cMin, %cMax)
{
    %hMin = 0.7;
    %hMax = 1.2;
    %h = (%height - %hMin) / (%hMax - %hMin);
    %c = (%h * (%cMax - %cMin)) + %cMin;
    %this.setLookAtNudge("0 0" SPC %c);
    return ;
}
function snapshotAvatarTool::doSnap(%this)
{
    gSetField(%this, lastFrame, $Canvas::frameCount);
    gSetField(%this, origProfile, snapshotAvatarToolActiveRegion.profile);
    snapshotAvatarToolActiveRegion.setProfile(ETSSnapshotBackgroundProfile);
    %this.waitForNextFrameToSnap();
    return ;
}
function snapshotAvatarTool::waitForNextFrameToSnap(%this)
{
    cancel(gGetField(%this, waitForFrameSchedule));
    if ($Canvas::frameCount <= gGetField(%this, lastFrame))
    {
        gSetField(%this, waitForFrameSchedule, %this.schedule(10, "waitForNextFrameToSnap"));
        return ;
    }
    %this.doSnap2();
    return ;
}
function snapshotAvatarTool::doSnap2(%this)
{
    %snapshot = snapshot::snapAndUpControlRegion(snapshotAvatarToolActiveRegion, $player.getShapeName(), "y");
    if (!isObject(%snapshot))
    {
        error("Snapshot", "Problem taking snapshot");
        return ;
    }
    %snapshot.saveObject = %this;
    %snapshot.setCompletedCallback("snapshotAvatarToolonCompleted");
    snapshotAvatarToolSet1.setVisible(0);
    snapshotAvatarToolSet2.setVisible(1);
    snapshotAvatarToolProgressBar.setValue(0);
    snapshotAvatarToolActiveRegion.setProfile(gGetField(%this, origProfile));
    return ;
}
function snapshotAvatarTool::onProgress(%this, %snapshot)
{
    %percent = %snapshot.ulNow / %snapshot.ulTotal;
    snapshotAvatarToolProgressBar.setValue(%percent);
    return ;
}
function snapshotAvatarToolonCompleted(%request, %result)
{
    %snapshot = %request.saveObject;
    if (%result == 0)
    {
        snapshotAvatarToolSet1.setVisible(1);
        snapshotAvatarToolSet2.setVisible(0);
        if (!((%snapshot.visitWhenDoneUrl $= "")) && $UserPref::Snapshots::View)
        {
            gotoWebPage(%snapshot.visitWhenDoneUrl);
        }
    }
    else
    {
        snapshotAvatarToolSet1.setVisible(1);
        snapshotAvatarToolSet2.setVisible(0);
    }
    return ;
}
