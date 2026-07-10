$cursorControlled = 1;
function cursorOff()
{
}
function cursorOn()
{
}
package CanvasCursor
{
    function GuiCanvas::checkCursor(%this)
    {
        %cursorShouldBeOn = 0;
        %i = 0;
        if ((%i < %this.getCount()))
        {
            %control = %this.getObject(%i);
            if ((%control.noCursor $= ""))
            {
                %cursorShouldBeOn = 1;
            }
            else
            {
                %i = (%i + 1.0);
            }
        }
        if ((%cursorShouldBeOn != %this.isCursorOn()))
        {
            if (%cursorShouldBeOn)
            {
                cursorOn();
            }
            else
            {
                cursorOff();
            }
        }
    }
    function GuiCanvas::setContent(%this, %ctrl)
    {
        Parent::setContent(%this, %ctrl);
        %this.checkCursor();
    }
    function GuiCanvas::pushDialog(%this, %ctrl, %layer)
    {
        Parent::pushDialog(%this, %ctrl, %layer);
        %this.checkCursor();
    }
    function GuiCanvas::popDialog(%this, %ctrl)
    {
        Parent::popDialog(%this, %ctrl);
        %this.checkCursor();
    }
    function GuiCanvas::popLayer(%this, %layer)
    {
        Parent::popLayer(%this, %layer);
        %this.checkCursor();
    }
    activatePackage(CanvasCursor);
};

