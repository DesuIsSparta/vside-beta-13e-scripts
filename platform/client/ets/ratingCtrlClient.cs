function ratingControl::Initialize(%this, %gradations, %buttonSize, %buttonBitmap)
{
    if (!%this.initialized)
    {
        %this.gradations = %gradations;
        %this.buttonSize = %buttonSize;
        %this.buttonBitmap = %buttonBitmap;
        %this.rating = 0;
        %this.mouseOver = -(1.0);
        %this.mouseDown = 0;
        %this.buildButtons();
        %this.update();
        %this.initialized = 1;
    }
}
function ratingControl::buildButtons(%this)
{
    %xPos = 0;
    %ypos = 0;
    %i = 0;
    while ((%i < %this.gradations))
    {
        %this.images[" ",%ypos;
            extent = %this.buttonSize;
            minExtent = "1 1";
            sluggishness = -(1.0);
            visible = 1;
            bitmap = %this.buttonBitmap,"_n";
            bitmapBase = %this.buttonBitmap;
        };,%i] = new GuiBitmapCtrl("") {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos;
        %this.images[%i].bindClassName("RatingControlImage");
        %this.add(%this.images[%i]);
        %xPos = (%xPos + getWord(%this.buttonSize, 0));
        %i = (%i + 1.0);
    }
    %this.eventCatcher = new GuiMouseEventCtrl("") {
        profile = (%i < %this.gradations) @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = (%this.gradations * getWord(%this.buttonSize, 0)) @ " " @ getWord(%this.buttonSize, 1);
        minExtent = "1 1";
        sluggishness = -(1.0);
        visible = 1;
    };
    %this.eventCatcher.bindClassName("RatingControlEventCatcher");
    %this.add(%this.eventCatcher);
}
function ratingControl::update(%this)
{
    %this.onUpdate();
    %cutoff = (%this.rating - 1.0);
    %suffix = "_d";
    if ((%this.mouseOver >= 0.0))
    {
        %cutoff = %this.mouseOver;
        if (!%this.mouseDown)
        {
            %suffix = "_h";
        }
    }
    %i = 0;
    while ((%i < %this.gradations))
    {
        if ((%i <= %cutoff))
        {
            %this.images[%i].setImageSuffix(%suffix);
        }
        else
        {
            %this.images[%i].setImageSuffix("_n");
        }
        %i = (%i + 1.0);
    }
}
function ratingControl::setRating(%this, %rating, %saveToManager)
{
    %this.rating = %rating;
    %this.update();
    if (%saveToManager)
    {
        Music::rateSong(%rating);
    }
}
function ratingControl::setMouseOver(%this, %level)
{
    %this.mouseOver = %level;
    %this.update();
}
function ratingControl::mouseDown(%this, %point)
{
    %this.mouseOver = mFloor((%point / getWord(%this.buttonSize, 0)));
    %this.mouseDown = 1;
    %this.update();
}
function ratingControl::mouseMove(%this, %point)
{
    %this.mouseOver = mFloor((%point / getWord(%this.buttonSize, 0)));
    %this.mouseDown = 0;
    %this.update();
}
function ratingControl::mouseUp(%this, %point)
{
    %this.mouseOver = -(1.0);
    %this.mouseDown = 0;
    %this.setRating((mFloor((%point / getWord(%this.buttonSize, 0))) + 1.0), 1);
}
function RatingControlEventCatcher::onMouseLeaveBounds(%this)
{
    %rc = %this.getParent();
    %rc.setMouseOver(-(1.0));
}
function RatingControlEventCatcher::onMouseEnterBounds(%this, %unused, %point, %unused)
{
}
function RatingControlEventCatcher::onMouseDown(%this, %unused, %point, %unused)
{
    %rc = %this.getParent();
    %rc.mouseDown(%rc.globalToLocal(%point));
}
function RatingControlEventCatcher::onMouseUp(%this, %unused, %point, %unused)
{
    %rc = %this.getParent();
    %rc.mouseUp(%rc.globalToLocal(%point));
}
function RatingControlEventCatcher::onMouseDragged(%this, %unused, %point, %unused)
{
    %rc = %this.getParent();
    %rc.mouseDown(%rc.globalToLocal(%point));
}
function RatingControlEventCatcher::onMouseMove(%this, %unused, %point, %unused)
{
    %rc = %this.getParent();
    %rc.mouseMove(%rc.globalToLocal(%point));
}
function RatingControlImage::setImageSuffix(%this, %suffix)
{
    %this.setBitmap(%this.bitmapBase @ %suffix);
}
