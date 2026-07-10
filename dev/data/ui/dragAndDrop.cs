if (!(isObject(DraggableProfile))) {
    new GuiControlProfile(DraggableProfile : ToolTipProfile) {
        modal = 1;
    };
}
function DragAndDropExampleList::Initialize(%this) {
    if (!(%this.initialized)) {
        10.setNumChildren(%this);
        %this.initialized = 1;
    }
};
function DragAndDropExampleList::onCreatedChild(%this, %child, %unused, %yPos) {
    %child.setProfile();
    %child.contentText = DraggableProfile @ %yPos;
    new GuiTextCtrl("") {
        profile = 0 @ VPointsTextProfile;
        position = "10 8";
        extent = "200 50";
        text = %child.contentText;
    };.add(%child);
    if (!(getWord(%child.getNamespaceList(), 0) $= "DragAndDropExampleDraggable")) {
        "DragAndDropExampleDraggable".bindClassName(%child);
    }
};
function DragAndDropExampleList::onDragAndDropEnter(%this, %dragCtrl) {
    hiliteControl(%this, 1);
};
function DragAndDropExampleList::onDragAndDropLeave(%this, %dragCtrl) {
    hiliteControl(0);
    %marker = %this.getHiliteMarker();
    0.setVisible(%marker);
};
function DragAndDropExampleList::getHiliteMarker(%this) {
    if (!(isObject(%this.hiliteMarker))) {
        %this.hiliteMarker = new GuiBitmapCtrl("") {
            profile = 0 @ "ETSNonModalProfile";
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
};
function DragAndDropExampleList::onDragAndDropMove(%this, %dragCtrl, %mousePos) {
    %ctrl = (getWord(%mousePos, 1) + (getWord(%this.childrenExtent, 1) / 2.0)).closestChildToPoint(%this, getWord(%mousePos, 0));
    %marker = %this.getHiliteMarker();
    %marker.add(Canvas.getContent());
    %marker.pushToBack(Canvas.getContent());
    if (isObject(%ctrl)) {
        ((getWord(%ctrl.getScreenPosition(), 1) - %this.spacing) - 1.0).reposition(%marker, (getWord(%ctrl.getScreenPosition(), 0) + 5.0));
    }
    %ctrl = (getWord(%mousePos, 1) - (getWord(%this.childrenExtent, 1) / 2.0)).closestChildToPoint(%this, getWord(%mousePos, 0));
    if (isObject(%ctrl)) {
        ((getWord(%ctrl.getScreenPosition(), 1) + getWord(%this.childrenExtent, 1)) - 1.0).reposition(%marker, (getWord(%ctrl.getScreenPosition(), 0) + 5.0));
    }
    1.setVisible(%marker);
};
function DragAndDropExampleList::onDragAndDropDrop(%this, %dragCtrl, %mousePos) {
    %ctrl = (getWord(%mousePos, 1) + (getWord(%this.childrenExtent, 1) / 2.0)).closestChildToPoint(%this, getWord(%mousePos, 0));
    %ctrl.reorderChild(%this, %dragCtrl);
    return 1;
};
function DragAndDropExampleDraggable::onMouseDown(%this) {
};
function DragAndDropExampleDraggable::onMouseDragged(%this) {
    1.setAsDragControl(%this);
};
function DragAndDropExampleDraggable::onDragSet(%this) {
};
function DragAndDropExampleDraggable::onDragReleased(%this) {
};
function DragAndDropExampleDraggable::makeVisualClone(%this) {
    return new GuiControl("") {
        profile = 0 @ %this.profile;
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = %this.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };;
};
