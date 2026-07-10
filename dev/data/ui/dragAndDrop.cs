if (!isObject(DraggableProfile))
{
    new GuiControlProfile(DraggableProfile : ToolTipProfile) {
        modal = 1;
    };
}
function DragAndDropExampleList::Initialize(%this)
{
    if (!%this.initialized)
    {
        %this.setNumChildren(10);
        %this.initialized = 1;
    }
}
function DragAndDropExampleList::onCreatedChild(%this, %child, %unused, %yPos)
{
    %child.setProfile(DraggableProfile);
    %child.contentText = %yPos;
    %child.add(new GuiTextCtrl("") {
        profile = VPointsTextProfile;
        position = "10 8";
        extent = "200 50";
        text = %child.contentText;
    };);
    if (!(getWord(%child.getNamespaceList(), 0) $= "DragAndDropExampleDraggable"))
    {
        %child.bindClassName("DragAndDropExampleDraggable");
    }
}
function DragAndDropExampleList::onDragAndDropEnter(%this, %dragCtrl)
{
    hiliteControl(%this, 1);
}
function DragAndDropExampleList::onDragAndDropLeave(%this, %dragCtrl)
{
    hiliteControl(0);
    %marker = %this.getHiliteMarker();
    %marker.setVisible(0);
}
function DragAndDropExampleList::getHiliteMarker(%this)
{
    if (!isObject(%this.hiliteMarker))
    {
        %this.hiliteMarker = new GuiBitmapCtrl("") {
            profile = "ETSNonModalProfile";
            horizSizing = "width";
            vertSizing = "top";
            position = "0 0";
            extent = "40 6";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 0;
            bitmap = "dev/data/ui/hiliteSeparator";
            wrap = 1;
        };
    }
    return %this.hiliteMarker;
}
function DragAndDropExampleList::onDragAndDropMove(%this, %dragCtrl, %mousePos)
{
    %ctrl = %this.closestChildToPoint(getWord(%mousePos, 0), (getWord(%mousePos, 1) + (getWord(%this.childrenExtent, 1) / 2)));
    %marker = %this.getHiliteMarker();
    Canvas.getContent().add(%marker);
    Canvas.getContent().pushToBack(%marker);
    if (isObject(%ctrl))
    {
        %marker.reposition((getWord(%ctrl.getScreenPosition(), 0) + 5), ((getWord(%ctrl.getScreenPosition(), 1) - %this.spacing) - 1));
    }
    else
    {
        %ctrl = %this.closestChildToPoint(getWord(%mousePos, 0), (getWord(%mousePos, 1) - (getWord(%this.childrenExtent, 1) / 2)));
        if (isObject(%ctrl))
        {
            %marker.reposition((getWord(%ctrl.getScreenPosition(), 0) + 5), ((getWord(%ctrl.getScreenPosition(), 1) + getWord(%this.childrenExtent, 1)) - 1));
        }
    }
    %marker.setVisible(1);
}
function DragAndDropExampleList::onDragAndDropDrop(%this, %dragCtrl, %mousePos)
{
    %ctrl = %this.closestChildToPoint(getWord(%mousePos, 0), (getWord(%mousePos, 1) + (getWord(%this.childrenExtent, 1) / 2)));
    %this.reorderChild(%dragCtrl, %ctrl);
    return 1;
}
function DragAndDropExampleDraggable::onMouseDown(%this)
{
}
function DragAndDropExampleDraggable::onMouseDragged(%this)
{
    %this.setAsDragControl(1);
}
function DragAndDropExampleDraggable::onDragSet(%this)
{
}
function DragAndDropExampleDraggable::onDragReleased(%this)
{
}
function DragAndDropExampleDraggable::makeVisualClone(%this)
{
    return new GuiControl("") {
        profile = %this.profile;
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = %this.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };;
}
