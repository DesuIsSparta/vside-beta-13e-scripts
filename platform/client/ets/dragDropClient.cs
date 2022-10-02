function Canvas::onSystemDragDropEvent(%this, %text, %isDrop, %pt)
{
    if (!isURL(%text))
    {
        return 0;
    }
    if (%this.getId() == Canvas.getId())
    {
        if (!isObject($player) && !$player.isHost())
        {
            return 0;
        }
    }
    return Parent::onSystemDragDropEvent(%this, %text, %isDrop, %pt);
}
function Canvas::onSystemDragDroppedEvent(%this, %url, %pt)
{
    if (!isObject(CSMediaDisplay))
    {
        error(getScopeName() SPC "- CSMediaDisplay not initialized." SPC getTrace());
        return ;
    }
    CSMediaDisplay.playMediaStream(%url);
    return ;
}
function GuiControl::onSystemDragDropEvent(%this, %text, %eventType, %pt)
{
    if (!%this.acceptsSystemDragDropContent(%text))
    {
        return 0;
    }
    Canvas.setSystemDragTargetControl(%this);
    if (%eventType $= "MAKE")
    {
        hiliteControl(%this);
    }
    else
    {
        if (%eventType $= "MOVE")
        {
        }
        else
        {
            if (%eventType $= "LEAVE")
            {
                if (%this.isHiliteCtrl())
                {
                    hiliteControl("");
                }
            }
            else
            {
                if (%eventType $= "BREAK")
                {
                    if (%this.isHiliteCtrl())
                    {
                        hiliteControl("");
                    }
                    %this.onSystemDragDroppedEvent(%text, %pt);
                }
            }
        }
    }
    return 1;
}
function GuiControl::acceptsSystemDragDropContent(%this, %text)
{
    return 1;
}
function GuiControl::onSystemDragDroppedEvent(%this, %text, %pt)
{
    return ;
}
