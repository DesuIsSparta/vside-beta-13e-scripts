function ProgressBarController::Initialize(%this, %parentCtrl, %emptyBitmap, %fillBitmap, %leftCapBitmap, %rightCapBitmap)
{
    if (!%this.pbinitialized)
    {
        %this.ctrl = %parentCtrl;
        if (!isObject(%this.ctrl))
        {
            return ;
        }
        %this.width = getWord(%this.ctrl.getExtent(), 0);
        %this.height = getWord(%this.ctrl.getExtent(), 1);
        %this.value = 0;
        %this.leftMargin = 0;
        %this.rightMargin = 0;
        %this.makeLeftCap(%leftCapBitmap);
        %this.makeRightCap(%rightCapBitmap);
        %this.makeBackground(%emptyBitmap);
        %this.makeForeground(%fillBitmap);
        %this.pbinitialized = 1;
    }
    return ;
}
function ProgressBarController::makeBackground(%this, %bitmap)
{
    %this.background = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = %this.leftMargin SPC 0;
        extent = %this.width - (%this.leftMargin + %this.rightMargin) SPC %this.height;
        minExtent = "0 1";
        sluggishness = -1;
        visible = 1;
        bitmap = %bitmap;
        wrap = 1;
    };
    %this.ctrl.add(%this.background);
    return ;
}
function ProgressBarController::makeForeground(%this, %bitmap)
{
    %this.foreground = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = 0 SPC %this.height;
        minExtent = "0 1";
        sluggishness = -1;
        visible = 1;
        bitmap = %bitmap;
        wrap = 1;
    };
    %this.ctrl.add(%this.foreground);
    return ;
}
function ProgressBarController::makeLeftCap(%this, %bitmap)
{
    if (isObject(%this.leftCap))
    {
        %this.leftCap.delete();
    }
    if (%bitmap $= "")
    {
        %this.leftCap = 0;
        return ;
    }
    %this.leftCap = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = 0 SPC %this.height;
        minExtent = "0 1";
        sluggishness = -1;
        visible = 1;
        bitmap = %bitmap;
        wrap = 1;
    };
    %this.ctrl.add(%this.leftCap);
    %this.reseatCaps();
    return ;
}
function ProgressBarController::makeRightCap(%this, %bitmap)
{
    if (isObject(%this.rightCap))
    {
        %this.rightCap.delete();
    }
    if (%bitmap $= "")
    {
        %this.rightCap = 0;
        return ;
    }
    %this.rightCap = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "0 0";
        extent = 0 SPC %this.height;
        minExtent = "0 1";
        sluggishness = -1;
        visible = 1;
        bitmap = %bitmap;
        wrap = 1;
    };
    %this.ctrl.add(%this.rightCap);
    %this.reseatCaps();
    return ;
}
function ProgressBarController::reseatCaps(%this)
{
    if (isObject(%this.leftCap))
    {
        %this.leftCap.fitSize();
        %this.leftMargin = getWord(%this.leftCap.getExtent(), 0);
    }
    if (isObject(%this.rightCap))
    {
        %this.rightCap.fitSize();
        %parentWidth = getWord(%this.ctrl.getExtent(), 0);
        %capWidth = getWord(%this.rightCap.getExtent(), 0);
        %this.rightCap.reposition(%parentWidth - %capWidth, 0);
        %this.rightMargin = %capWidth;
    }
    return ;
}
function ProgressBarController::setValue(%this, %value)
{
    %this.value = mMax(mMin(%value, 1), 0);
    %effectiveWidth = %this.width - (%this.leftMargin + %this.rightMargin);
    %this.foreground.resize(%this.leftMargin, 0, mFloor(%this.value * %effectiveWidth), %this.height);
    %this.reseatCaps();
    return ;
}
function ProgressBarController::update(%this)
{
    if (isObject(%this.ctrl))
    {
        %this.width = getWord(%this.ctrl.getExtent(), 0);
        %this.height = getWord(%this.ctrl.getExtent(), 1);
        if (isObject(%this.background))
        {
            %this.background.resize(0, 0, %this.width, %this.height);
        }
        if (isObject(%this.foreground))
        {
            %this.setValue(%this.value);
        }
    }
    return ;
}
