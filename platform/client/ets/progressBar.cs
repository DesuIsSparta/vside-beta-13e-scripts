function ProgressBarController::Initialize(%this, %parentCtrl, %emptyBitmap, %fillBitmap, %leftCapBitmap, %rightCapBitmap) {
    if (!(pbinitialized)) {
        ctrl = %this @ %parentCtrl @ %this;
        if (!(isObject(ctrl))) {
            return %this;
        }
        width = %this @ getWord(ctrl.getExtent(), 0) @ %this;
        height = %this @ getWord(ctrl.getExtent(), 1) @ %this;
        value = 0 @ %this;
        leftMargin = 0 @ %this;
        rightMargin = 0 @ %this;
        %this.makeLeftCap(%leftCapBitmap);
        %this.makeRightCap(%rightCapBitmap);
        %this.makeBackground(%emptyBitmap);
        %this.makeForeground(%fillBitmap);
        pbinitialized = 1 @ %this;
    }
};
function ProgressBarController::makeBackground(%this, %bitmap) {
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "width";
    vertSizing = "height";
    position = %this @ leftMargin @ " " @ 0;
    extent = (%this + leftMargin) @ (%this - width) @ " " @ %this @ height;
    rightMargin;
    minExtent = %this @ "0 1";
    sluggishness = -1;
    visible = 1;
    bitmap = %bitmap;
    wrap = 1;
    background = %this;
    ctrl.add(background);
};
function ProgressBarController::makeForeground(%this, %bitmap) {
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "width";
    vertSizing = "height";
    position = "0 0";
    extent = 0 @ " " @ %this @ height;
    minExtent = "0 1";
    sluggishness = -1;
    visible = 1;
    bitmap = %bitmap;
    wrap = 1;
    foreground = %this;
    ctrl.add(foreground);
};
function ProgressBarController::makeLeftCap(%this, %bitmap) {
    if (isObject(leftCap)) {
        leftCap.delete();
    }
    if ((%this SPC %bitmap $= "")) {
        leftCap = %this @ 0 @ %this;
        return;
    }
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = 0 @ " " @ %this @ height;
    minExtent = "0 1";
    sluggishness = -1;
    visible = 1;
    bitmap = %bitmap;
    wrap = 1;
    leftCap = %this;
    ctrl.add(leftCap);
    %this.reseatCaps();
};
function ProgressBarController::makeRightCap(%this, %bitmap) {
    if (isObject(rightCap)) {
        rightCap.delete();
    }
    if ((%this SPC %bitmap $= "")) {
        rightCap = %this @ 0 @ %this;
        return;
    }
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "left";
    vertSizing = "bottom";
    position = "0 0";
    extent = 0 @ " " @ %this @ height;
    minExtent = "0 1";
    sluggishness = -1;
    visible = 1;
    bitmap = %bitmap;
    wrap = 1;
    rightCap = %this;
    ctrl.add(rightCap);
    %this.reseatCaps();
};
function ProgressBarController::reseatCaps(%this) {
    if (isObject(leftCap)) {
        leftCap.fitSize();
        leftMargin = %this @ getWord(leftCap.getExtent(), 0) @ %this;
        %this;
    }
    if (isObject(rightCap)) {
        rightCap.fitSize();
        %parentWidth = getWord(ctrl.getExtent(), 0);
        %this;
        %capWidth = getWord(rightCap.getExtent(), 0);
        %this;
        rightCap.reposition((%capWidth - %parentWidth), 0);
        rightMargin = %this @ %capWidth @ %this;
        %this;
    }
};
function ProgressBarController::setValue(%this, %value) {
    value = mMax(mMin(%value, 1), 0) @ %this;
    %effectiveWidth = (%this - width);
    (%this + leftMargin);
    foreground.resize(leftMargin, 0, mFloor((%this * value)), height);
    %this.reseatCaps();
};
function ProgressBarController::update(%this) {
    if (isObject(ctrl)) {
        width = %this @ getWord(ctrl.getExtent(), 0) @ %this;
        %this;
        height = %this @ getWord(ctrl.getExtent(), 1) @ %this;
        if (isObject(background)) {
            background.resize(0, 0, width, height);
        }
        if (isObject(foreground)) {
            %this.setValue(value);
        }
    }
};
