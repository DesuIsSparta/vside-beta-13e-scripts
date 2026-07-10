function Canvas::onSystemDragDropEvent(%this, %text, %isDrop, %pt) {
    if (!(isURL(%text))) {
        return 0;
    }
    if ((Canvas.getId() == %this.getId())) {
        if (!(isObject($player))) {
        }
        if (!($player.isHost())) {
            return 0;
        }
    }
    return Parent::onSystemDragDropEvent(%this, %text, %isDrop, %pt);
};
function Canvas::onSystemDragDroppedEvent(%this, %url, %pt) {
    if (!(isObject(CSMediaDisplay))) {
        error(getScopeName() @ " " @ "- CSMediaDisplay not initialized." @ " " @ getTrace());
        return;
    }
    %url.playMediaStream();
};
function GuiControl::onSystemDragDropEvent(%this, %text, %eventType, %pt) {
    if (!(%this.acceptsSystemDragDropContent(%text))) {
        return 0;
    }
    %this.setSystemDragTargetControl();
    if ((Canvas @ " " @ %eventType $= "MAKE")) {
        hiliteControl(%this);
    }
    if ((%eventType $= "MOVE")) {
    }
    if ((%eventType $= "LEAVE")) {
        if (%this.isHiliteCtrl()) {
            hiliteControl("");
        }
    }
    if ((%eventType $= "BREAK")) {
        if (%this.isHiliteCtrl()) {
            hiliteControl("");
        }
        %this.onSystemDragDroppedEvent(%text, %pt);
    }
    return 1;
};
function GuiControl::acceptsSystemDragDropContent(%this, %text) {
    return 1;
};
function GuiControl::onSystemDragDroppedEvent(%this, %text, %pt) {
};
