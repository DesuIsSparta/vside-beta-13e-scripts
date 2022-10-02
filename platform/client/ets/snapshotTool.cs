function toggleSnapshotTool()
{
    PlayGui.showRaiseOrHide(snapshotTool);
    return ;
}
function snapshotTool::open(%this)
{
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    return ;
}
function snapshotTool::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
}
function snapshotTool::doSnap(%this)
{
    gSetField(%this, lastFrame, $Canvas::frameCount);
    gSetField(%this, origProfile, ClosetMainObjectView.profile);
    ClosetMainObjectView.setProfile(ETSSnapshotBackgroundProfile);
    snapshotToolActiveRegion.setVisible(0);
    %this.waitForNextFrameToSnap();
    return ;
}
function snapshotTool::waitForNextFrameToSnap(%this)
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
function snapshotTool::doSnap2(%this)
{
    %snapshot = snapshot::snapAndUpControlRegion(snapshotToolActiveRegion, $player.getShapeName(), "y");
    if (!isObject(%snapshot))
    {
        error("Snapshot", "Problem taking snapshot");
        return ;
    }
    %snapshot.saveObject = %this;
    %snapshot.setCompletedCallback("snapshotToolonComplete");
    snapshotToolSet1.setVisible(0);
    snapshotToolSet2.setVisible(1);
    snapshotToolActiveRegion.setVisible(1);
    snapshotToolProgressBar.setValue(0);
    ClosetMainObjectView.setProfile(gGetField(%this, origProfile));
    return ;
}
function snapshotTool::onProgress(%this, %snapshot)
{
    %percent = %snapshot.ulNow / %snapshot.ulTotal;
    snapshotToolProgressBar.setValue(%percent);
    return ;
}
function snapshotToolonComplete(%request, %result)
{
    %snapshot = %request.saveObject;
    if (%result == 0)
    {
        snapshotToolSet1.setVisible(1);
        snapshotToolSet2.setVisible(0);
        if (!((%snapshot.visitWhenDoneUrl $= "")) && $UserPref::Snapshots::View)
        {
            gotoWebPage(%snapshot.visitWhenDoneUrl);
        }
    }
    else
    {
        snapshotToolSet1.setVisible(1);
        snapshotToolSet2.setVisible(0);
    }
    return ;
}
function snapshotTool::snapControl(%ctrl, %fileName)
{
    %origin = %ctrl.getScreenPosition();
    %extent = %ctrl.getExtent();
    %rect = %origin SPC %extent;
    return snapshotTool::snapRegion(%rect, %fileName);
}
function snapshotTool::snapRegion(%region, %fileName)
{
    %success = shootscreen(%fileName, %region);
    return %success;
}
