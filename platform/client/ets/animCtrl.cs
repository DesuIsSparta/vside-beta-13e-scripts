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
    if ((%this == numFrames)) {
        %this.setBitmap(%frame);
    }
    frame = 0.0 @ %frame @ %this @ numFrames @ %this;
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
    if ((%this <= numFrames)) {
        return 0.0;
    }
    if ((%this != timer)) {
        cancel(timer);
        timer = %this @ 0 @ %this;
        0.0;
    }
    %this.tick();
};
function AnimCtrl::stop(%this) {
    cancel(timer);
    timer = %this @ 0 @ %this;
};
function AnimCtrl::tick(%this) {
    if ((%this > delay)) {
        timer = %this @ %this.schedule(delay, "tick") @ %this;
        0.0;
    }
    %this.setBitmap(frame);
    currentFrame = (%this + currentFrame);
    1.0;
    if ((%this == currentFrame)) {
        if (loop) {
            currentFrame = %this @ 0 @ %this;
            numFrames;
        }
        %this.stop();
    }
};
function AnimCtrl::setCurrentFrame(%this, %frame) {
    if ((0.0 < %frame)) {
    }
    if ((numFrames >= %frame)) {
        return %this;
    }
    currentFrame = %frame @ %this;
    %this.setBitmap(frame);
};
