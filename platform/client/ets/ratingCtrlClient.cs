function ratingControl::Initialize(%this, %gradations, %buttonSize, %buttonBitmap)
{
    if (!%this.initialized)
    {
        %this.gradations = %gradations;
        %this.buttonSize = %buttonSize;
        %this.buttonBitmap = %buttonBitmap;
        %this.rating = 0;
        %this.mouseOver = -1;
        %this.mouseDown = 0;
        %this.buildButtons();
        %this.update();
        %this.initialized = 1;
    }
    return ;
}
function ratingControl::buildButtons(%this)
{
    %xPos = 0;
    %ypos = 0;
    %i = 0;
    while (%i < %this.gradations)
    {
        %this.images[%i] = new GuiBitmapCtrl()
        {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos SPC %ypos;
            extent = %this.buttonSize;
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = %this.buttonBitmap @ "_n";
            bitmapBase = %this.buttonBitmap;
        };
        %this.images[%i].bindClassName("RatingControlImage");
        %this.add(%this.images[%i]);
        %xPos = %xPos + getWord(%this.buttonSize, 0);
        %i = %i + 1;
    }
    %this.eventCatcher = new GuiMouseEventCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = %this.gradations * getWord(%this.buttonSize, 0) SPC getWord(%this.buttonSize, 1);
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %this.eventCatcher.bindClassName("RatingControlEventCatcher");
    %this.add(%this.eventCatcher);
    return ;
}
function ratingControl::update(%this)
{
    %this.onUpdate();
    %cutoff = %this.rating - 1;
    %suffix = "_d";
    if (%this.mouseOver >= 0)
    {
        %cutoff = %this.mouseOver;
        if (!%this.mouseDown)
        {
            %suffix = "_h";
        }
    }
    %i = 0;
    while (%i < %this.gradations)
    {
        if (%i <= %cutoff)
        {
            %this.images[%i].setImageSuffix(%suffix);
        }
        else
        {
            %this.images[%i].setImageSuffix("_n");
        }
        %i = %i + 1;
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
    return ;
}
function ratingControl::setMouseOver(%this, %level)
{
    %this.mouseOver = %level;
    %this.update();
    return ;
}
function ratingControl::mouseDown(%this, %point)
{
    %this.mouseOver = mFloor(%point / getWord(%this.buttonSize, 0));
    %this.mouseDown = 1;
    %this.update();
    return ;
}
function ratingControl::mouseMove(%this, %point)
{
    %this.mouseOver = mFloor(%point / getWord(%this.buttonSize, 0));
    %this.mouseDown = 0;
    %this.update();
    return ;
}
function ratingControl::mouseUp(%this, %point)
{
    %this.mouseOver = -1;
    %this.mouseDown = 0;
    %this.setRating(mFloor(%point / getWord(%this.buttonSize, 0)) + 1, 1);
    return ;
}
function RatingControlEventCatcher::onMouseLeaveBounds(%this)
{
    %rc = %this.getParent();
    %rc.setMouseOver(-1);
    return ;
}
function RatingControlEventCatcher::onMouseEnterBounds(%this, %unused, %point, %unused)
{
    return ;
}
function RatingControlEventCatcher::onMouseDown(%this, %unused, %point, %unused)
{
    %rc = %this.getParent();
    %rc.mouseDown(%rc.globalToLocal(%point));
    return ;
}
function RatingControlEventCatcher::onMouseUp(%this, %unused, %point, %unused)
{
    %rc = %this.getParent();
    %rc.mouseUp(%rc.globalToLocal(%point));
    return ;
}
function RatingControlEventCatcher::onMouseDragged(%this, %unused, %point, %unused)
{
    %rc = %this.getParent();
    %rc.mouseDown(%rc.globalToLocal(%point));
    return ;
}
function RatingControlEventCatcher::onMouseMove(%this, %unused, %point, %unused)
{
    %rc = %this.getParent();
    %rc.mouseMove(%rc.globalToLocal(%point));
    return ;
}
function RatingControlImage::setImageSuffix(%this, %suffix)
{
    %this.setBitmap(%this.bitmapBase @ %suffix);
    return ;
}
