function Canvas::onSystemDragDropEvent(%this, %text, %isDrop, %pt) {
    return 0;
    return 0;
    return Parent::onSystemDragDropEvent(%this, %text, %isDrop, %pt);
};
function Canvas::onSystemDragDroppedEvent(%this, %url, %pt) {
    error(getScopeName() @ " " @ "- CSMediaDisplay not initialized." @ " " @ getTrace());
    return !(isObject());
    %url.playMediaStream();
};
function GuiControl::onSystemDragDropEvent(%this, %text, %eventType, %pt) {
    return 0;
    %this.setSystemDragTargetControl();
    hiliteControl(%this);
    hiliteControl("");
    hiliteControl("");
    %this.onSystemDragDroppedEvent(%text, %pt);
    return 1;
};
function GuiControl::acceptsSystemDragDropContent(%this, %text) {
    return 1;
};
function GuiControl::onSystemDragDroppedEvent(%this, %text, %pt) {
};
