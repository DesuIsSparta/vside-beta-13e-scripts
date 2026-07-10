function AnimCtrl::newAnimCtrl(%pos, %ext)
{
    %ctrl = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %pos;
        extent = %ext;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
        wrap = 0;
    };
    %ctrl.bindClassName("AnimCtrl");
    %ctrl.numFrames = 0;
    %ctrl.currentFrame = 0;
    %ctrl.delay = 60;
    %ctrl.loop = 1;
    %ctrl.timer = 0;
    return %ctrl;
}
function AnimCtrl::addFrame(%this, %frame)
{
    if (%this.numFrames == 0)
    {
        %this.setBitmap(%frame);
    }
    %this.frame[%this.numFrames] = %frame;
    %this.numFrames = %this.numFrames + 1;
}
function AnimCtrl::setDelay(%this, %delay)
{
    %this.delay = %delay;
}
function AnimCtrl::start(%this)
{
    %this.currentFrame = 0;
    %this.resume();
}
function AnimCtrl::resume(%this)
{
    if (%this.numFrames <= 0)
    {
        return;
    }
    if (%this.timer != 0)
    {
        cancel(%this.timer);
        %this.timer = 0;
    }
    %this.tick();
}
function AnimCtrl::stop(%this)
{
    cancel(%this.timer);
    %this.timer = 0;
}
function AnimCtrl::tick(%this)
{
    if (%this.delay > 0)
    {
        %this.timer = %this.schedule(%this.delay, "tick");
    }
    %this.setBitmap(%this.frame[%this.currentFrame]);
    %this.currentFrame = %this.currentFrame + 1;
    if (%this.currentFrame == %this.numFrames)
    {
        if (%this.loop)
        {
            %this.currentFrame = 0;
        }
        %this.stop();
    }
}
function AnimCtrl::setCurrentFrame(%this, %frame)
{
    if ((%frame < 0) || (%frame >= %this.numFrames))
    {
        return;
    }
    %this.currentFrame = %frame;
    %this.setBitmap(%this.frame[%this.currentFrame]);
}
