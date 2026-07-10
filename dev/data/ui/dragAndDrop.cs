if (!(isObject())) {
    modal = DraggableProfile @ new GuiControlProfile(DraggableProfile : ToolTipProfile) @ 1;
}
function DragAndDropExampleList::Initialize(%this) {
    if (!(initialized)) {
        %this.setNumChildren(10);
        initialized = %this @ 1 @ %this;
    }
};
function DragAndDropExampleList::onCreatedChild(%this, %child, %unused, %yPos) {
    %child.setProfile();
    contentText = DraggableProfile @ %yPos @ %child;
    profile = new ""() @ VPointsTextProfile;
    GuiTextCtrl;
    position = 0 @ "10 8";
    extent = "200 50";
    text = %child @ contentText;
    %child.add();
    if (!(getWord(%child.getNamespaceList(), 0) $= "DragAndDropExampleDraggable")) {
        %child.bindClassName("DragAndDropExampleDraggable");
    }
};
function DragAndDropExampleList::onDragAndDropEnter(%this, %dragCtrl) {
    hiliteControl(%this, 1);
};
function DragAndDropExampleList::onDragAndDropLeave(%this, %dragCtrl) {
    hiliteControl(0);
    %marker = %this.getHiliteMarker();
    %marker.setVisible(0);
};
function DragAndDropExampleList::getHiliteMarker(%this) {
    if (!(isObject(hiliteMarker))) {
        profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
        0;
        horizSizing = %this @ "width";
        vertSizing = "top";
        position = "0 0";
        extent = "40 6";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        bitmap = "dev/data/ui/hiliteSeparator";
        wrap = 1;
        hiliteMarker = %this;
    }
    return hiliteMarker;
};
function DragAndDropExampleList::onDragAndDropMove(%this, %dragCtrl, %mousePos) {
    %ctrl = %this.closestChildToPoint(getWord(%mousePos, 0), ((%this / getWord(childrenExtent, 1)) + getWord(%mousePos, 1)));
    2.0;
    %marker = %this.getHiliteMarker();
    getContent().add(%marker);
    getContent().pushToBack(%marker);
    if (isObject(%ctrl)) {
        %marker.reposition((5.0 + getWord(%ctrl.getScreenPosition(), 0)), (%this - (spacing - getWord(%ctrl.getScreenPosition(), 1))));
    }
    %ctrl = %this.closestChildToPoint(getWord(%mousePos, 0), ((%this / getWord(childrenExtent, 1)) - getWord(%mousePos, 1)));
    2.0;
    if (isObject(%ctrl)) {
        %marker.reposition((5.0 + getWord(%ctrl.getScreenPosition(), 0)), (%this - (getWord(childrenExtent, 1) + getWord(%ctrl.getScreenPosition(), 1))));
    }
    %marker.setVisible(1);
};
function DragAndDropExampleList::onDragAndDropDrop(%this, %dragCtrl, %mousePos) {
    %ctrl = %this.closestChildToPoint(getWord(%mousePos, 0), ((%this / getWord(childrenExtent, 1)) + getWord(%mousePos, 1)));
    2.0;
    %this.reorderChild(%dragCtrl, %ctrl);
    return 1;
};
function DragAndDropExampleDraggable::onMouseDown(%this) {
};
function DragAndDropExampleDraggable::onMouseDragged(%this) {
    %this.setAsDragControl(1);
};
function DragAndDropExampleDraggable::onDragSet(%this) {
};
function DragAndDropExampleDraggable::onDragReleased(%this) {
};
function DragAndDropExampleDraggable::makeVisualClone(%this) {
    profile = new ""() @ %this @ profile;
    GuiControl;
    horizSizing = 0 @ "width";
    vertSizing = "height";
    position = "0 0";
    extent = %this.getExtent();
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    profile = new ""() @ VPointsTextProfile;
    GuiTextCtrl;
    position = "10 8";
    extent = "200 50";
    text = %this @ contentText;
    return;
};
