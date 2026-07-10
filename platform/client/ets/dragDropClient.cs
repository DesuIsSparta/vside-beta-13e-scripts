function Canvas::onSystemDragDropEvent(%this, %text, %isDrop, %pt) {
    if (!(isURL(%text))) {
        return 0;
    }
    if ((%this.getId() == Canvas.getId())) {
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
    %url.playMediaStream(CSMediaDisplay);
};
function GuiControl::onSystemDragDropEvent(%this, %text, %eventType, %pt) {
    if (!(%text.acceptsSystemDragDropContent(%this))) {
        return 0;
    }
    %this.setSystemDragTargetControl(Canvas);
    if ((%eventType $= "MAKE")) {
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
        %pt.onSystemDragDroppedEvent(%this, %text);
    }
    return 1;
};
function GuiControl::acceptsSystemDragDropContent(%this, %text) {
    return 1;
};
function GuiControl::onSystemDragDroppedEvent(%this, %text, %pt) {
};
