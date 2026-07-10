function ratingControl::Initialize(%this, %gradations, %buttonSize, %buttonBitmap) {
    if (!(initialized)) {
        gradations = %this @ %gradations @ %this;
        buttonSize = %buttonSize @ %this;
        buttonBitmap = %buttonBitmap @ %this;
        rating = 0 @ %this;
        mouseOver = -(1.0) @ %this;
        mouseDown = 0 @ %this;
        %this.buildButtons();
        %this.update();
        initialized = 1 @ %this;
    }
};
function ratingControl::buildButtons(%this) {
    %xPos = 0;
    %ypos = 0;
    %i = 0;
    if ((gradations < %i)) {
        profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
        0;
        horizSizing = %this @ "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = %this @ buttonSize;
        minExtent = "1 1";
        sluggishness = -(1.0);
        visible = 1;
        bitmap = %this @ buttonBitmap @ "_n";
        bitmapBase = %this @ buttonBitmap;
        images = %i @ %this;
        images.bindClassName("RatingControlImage");
        %this.add(images);
        %xPos = (getWord(buttonSize, 0) + %xPos);
        %this;
        %i = (1.0 + %i);
        %i @ %this @ %i @ %this;
    }
    profile = GuiMouseEventCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = %this @ (gradations < %i) @ "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = getWord(buttonSize, 0) @ (%this * gradations) @ " " @ %this @ getWord(buttonSize, 1);
    %this;
    minExtent = "1 1";
    sluggishness = -(1.0);
    visible = 1;
    eventCatcher = %this;
    eventCatcher.bindClassName("RatingControlEventCatcher");
    %this.add(eventCatcher);
};
function ratingControl::update(%this) {
    %this.onUpdate();
    %cutoff = (%this - rating);
    1.0;
    %suffix = "_d";
    if ((%this >= mouseOver)) {
        %cutoff = mouseOver;
        %this;
        if (!(mouseDown)) {
            %suffix = "_h";
            %this;
        }
    }
    %i = 0;
    0.0;
    if ((gradations < %i)) {
        if ((%cutoff <= %i)) {
            images.setImageSuffix(%suffix);
        }
        images.setImageSuffix("_n");
        %i = (1.0 + %i);
        %this @ %i @ %this @ %i @ %this;
    }
};
function ratingControl::setRating(%this, %rating, %saveToManager) {
    rating = %rating @ %this;
    %this.update();
    if (%saveToManager) {
        Music::rateSong(%rating);
    }
};
function ratingControl::setMouseOver(%this, %level) {
    mouseOver = %level @ %this;
    %this.update();
};
function ratingControl::mouseDown(%this, %point) {
    mouseOver = %this @ mFloor((getWord(buttonSize, 0) / %point)) @ %this;
    mouseDown = 1 @ %this;
    %this.update();
};
function ratingControl::mouseMove(%this, %point) {
    mouseOver = %this @ mFloor((getWord(buttonSize, 0) / %point)) @ %this;
    mouseDown = 0 @ %this;
    %this.update();
};
function ratingControl::mouseUp(%this, %point) {
    mouseOver = -(1.0) @ %this;
    mouseDown = 0 @ %this;
    %this.setRating((%this + mFloor((getWord(buttonSize, 0) / %point))), 1);
};
function RatingControlEventCatcher::onMouseLeaveBounds(%this) {
    %rc = %this.getParent();
    %rc.setMouseOver(-(1.0));
};
function RatingControlEventCatcher::onMouseEnterBounds(%this, %unused, %point, %unused) {
};
function RatingControlEventCatcher::onMouseDown(%this, %unused, %point, %unused) {
    %rc = %this.getParent();
    %rc.mouseDown(%rc.globalToLocal(%point));
};
function RatingControlEventCatcher::onMouseUp(%this, %unused, %point, %unused) {
    %rc = %this.getParent();
    %rc.mouseUp(%rc.globalToLocal(%point));
};
function RatingControlEventCatcher::onMouseDragged(%this, %unused, %point, %unused) {
    %rc = %this.getParent();
    %rc.mouseDown(%rc.globalToLocal(%point));
};
function RatingControlEventCatcher::onMouseMove(%this, %unused, %point, %unused) {
    %rc = %this.getParent();
    %rc.mouseMove(%rc.globalToLocal(%point));
};
function RatingControlImage::setImageSuffix(%this, %suffix) {
    %this.setBitmap(%this @ bitmapBase @ %suffix);
};
