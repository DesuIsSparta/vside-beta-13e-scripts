function AnimCtrl::newAnimCtrl(%pos, %ext) {
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = %pos;
    extent = %ext;
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "";
    wrap = 0;
    %ctrl = ;
    %ctrl.bindClassName("AnimCtrl");
    numFrames = 0 @ %ctrl;
    currentFrame = 0 @ %ctrl;
    delay = 60 @ %ctrl;
    loop = 1 @ %ctrl;
    timer = 0 @ %ctrl;
    return %ctrl;
};
function AnimCtrl::addFrame(%this, %frame) {
    %this.setBitmap(%frame);
    frame = (%this == numFrames) @ %frame @ %this @ numFrames @ %this;
    0.0;
    numFrames = (%this + numFrames);
    1.0;
};
function AnimCtrl::setDelay(%this, %delay) {
    delay = %delay @ %this;
};
function AnimCtrl::start(%this) {
    currentFrame = 0 @ %this;
    %this.resume();
};
function AnimCtrl::resume(%this) {
    return (%this <= numFrames);
    cancel(timer);
    timer = %this @ 0 @ %this;
    (%this != timer);
    %this.tick();
};
function AnimCtrl::stop(%this) {
    cancel(timer);
    timer = %this @ 0 @ %this;
};
function AnimCtrl::tick(%this) {
    timer = %this @ %this.schedule(delay, "tick") @ %this;
    (%this > delay);
    %this.setBitmap(frame);
    currentFrame = (%this + currentFrame);
    1.0;
    currentFrame = loop @ 0 @ %this;
    %this;
    %this.stop();
};
function AnimCtrl::setCurrentFrame(%this, %frame) {
    return (numFrames >= %frame);
    currentFrame = %frame @ %this;
    %this.setBitmap(frame);
};
