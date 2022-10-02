$cursorControlled = 1;
function cursorOff()
{
    return ;
}
function cursorOn()
{
    return ;
}
package CanvasCursor
{
    function GuiCanvas::checkCursor(%this)
    {
        %cursorShouldBeOn = 0;
        %i = 0;
        while (%i < %this.getCount())
        {
            %control = %this.getObject(%i);
            if (%control.noCursor $= "")
            {
                %cursorShouldBeOn = 1;
                break;
            }
            %i = %i + 1;
        }
        if (%cursorShouldBeOn != %this.isCursorOn())
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
        return ;
    }
    function GuiCanvas::setContent(%this, %ctrl)
    {
        Parent::setContent(%this, %ctrl);
        %this.checkCursor();
        return ;
    }
    function GuiCanvas::pushDialog(%this, %ctrl, %layer)
    {
        Parent::pushDialog(%this, %ctrl, %layer);
        %this.checkCursor();
        return ;
    }
    function GuiCanvas::popDialog(%this, %ctrl)
    {
        Parent::popDialog(%this, %ctrl);
        %this.checkCursor();
        return ;
    }
    function GuiCanvas::popLayer(%this, %layer)
    {
        Parent::popLayer(%this, %layer);
        %this.checkCursor();
        return ;
    }
};

activatePackage(CanvasCursor);

