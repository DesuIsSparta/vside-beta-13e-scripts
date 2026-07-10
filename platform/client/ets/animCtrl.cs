function AnimCtrl::newAnimCtrl(%pos, %ext) {
    0;
    %ctrl = new ""() {
        profile = GuiBitmapCtrl @ "GuiDefaultProfile";
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
};
function AnimCtrl::addFrame(%this, %frame) {
    if ((0.0 == %this.numFrames)) {
        %this.setBitmap(%frame);
    }
    %this.frame = %frame @ %this.numFrames;
    %this.numFrames = (1.0 + %this.numFrames);
};
function AnimCtrl::setDelay(%this, %delay) {
    %this.delay = %delay;
};
function AnimCtrl::start(%this) {
    %this.currentFrame = 0;
    %this.resume();
};
function AnimCtrl::resume(%this) {
    if ((0.0 <= %this.numFrames)) {
        return;
    }
    if ((0.0 != %this.timer)) {
        cancel(%this.timer);
        %this.timer = 0;
    }
    %this.tick();
};
function AnimCtrl::stop(%this) {
    cancel(%this.timer);
    %this.timer = 0;
};
function AnimCtrl::tick(%this) {
    if ((0.0 > %this.delay)) {
        %this.timer = %this.schedule(%this.delay, "tick");
    }
    %this.setBitmap(%this.frame);
    %this.currentFrame = (1.0 + %this.currentFrame);
    %this.currentFrame;
    if ((%this.numFrames == %this.currentFrame)) {
        if (%this.loop) {
            %this.currentFrame = 0;
        }
        %this.stop();
    }
};
function AnimCtrl::setCurrentFrame(%this, %frame) {
    if ((0.0 < %frame)) {
    }
    if ((%this.numFrames >= %frame)) {
        return;
    }
    %this.currentFrame = %frame;
    %this.setBitmap(%this.frame);
};
