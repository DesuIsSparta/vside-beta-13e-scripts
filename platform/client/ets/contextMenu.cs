function GuiControl::newContextMenu(%this, %menuName) {
    if (isObject(%menuName)) {
        return %menuName;
    }
    %cm = new GuiPopUp2MenuCtrl("") {
        profile = 0 @ "ETSRightClickProfile";
        scrollProfile = "ETSScrollProfile";
        winProfile = "ETSRightClickWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "79 171";
        extent = "200 23";
        minExtent = "8 8";
        sluggishness = -1;
        visible = 0;
        command = %menuName @ ".setVisible(0);";
        maxLength = 255;
        maxPopupHeight = 200;
        allowOffscreen = 1;
    };
    "ContextMenu".bindClassName(%cm);
    %menuName.bindClassName(%cm);
    %menuName.setName(%cm);
    return %cm;
};
function ContextMenu::showAtPoint(%this, %pos) {
    %topContent = (Canvas.getCount() - 1.0).getObject(Canvas);
    %this.add(%topContent);
    %this.pushToBack(%topContent);
    1.setVisible(%this);
    %this.forceOnAction();
    %popup = %this.getTextList().getParent().getParent();
    %width = getWord(%this.getExtent(), 0);
    %height = (getWord(%this.getExtent(), 1) + getWord(%popup.getExtent(), 1));
    %newPos = onscreenCoordinates(getWord(%pos, 0), getWord(%pos, 1), %width, %height);
    getWord(%newPos, 1).reposition(%this, getWord(%newPos, 0));
    (getWord(%newPos, 1) + getWord(%this.getExtent(), 1)).reposition(%popup, getWord(%newPos, 0));
};
function ContextMenu::showAtCursor(%this) {
    Canvas.getCursorPos().showAtPoint(%this);
};
function onscreenCoordinates(%left, %top, %width, %height) {
    %screenWidth = getWord(getRes(), 0);
    %screenHeight = getWord(getRes(), 1);
    if (((%left + %width) > %screenWidth)) {
        %left = (%screenWidth - %width);
    }
    if (((%top + %height) > %screenHeight)) {
        %top = (%screenHeight - %height);
    }
    if ((%left < 0.0)) {
        %left = 0;
    }
    if ((%top < 0.0)) {
        %top = 0;
    }
    return %left @ " " @ %top;
};
"EditContextMenu".newContextMenu(Canvas);
function GuiTextEditCtrl::onRightMouseUp(%this) {
    if (%this.password) {
        return;
    }
    1.makeFirstResponder(%this);
    %this.init(EditContextMenu);
    EditContextMenu.showAtCursor();
    %this.showCursor = 1;
};
function EditContextMenu::init(%this, %ctrl) {
    %this.ctrl = %ctrl;
    %this.clear();
    %grey = "255 255 255 128";
    %white = "255 255 255 255";
    %grey.addScheme(%this, 1, %grey, %grey);
    %white.addScheme(%this, 2, %white, %white);
    %schemeNormal = 0;
    %schemeDisabled = 1;
    %selection = %ctrl.getSelection();
    %start = getWord(%selection, 0);
    %end = getWord(%selection, 1);
    %modifiable = !(%ctrl.readOnly);
    %canCopy = ((%end - %start) > 0.0);
    if (%modifiable) {
    }
    %canCut = %canCopy;
    if (%modifiable) {
    }
    %canPaste = !(getClipboard() $= "");
    %n = -(1.0);
    %n = (%n + 1.0);
    if (%modifiable) {
    }
    %schemeDisabled.add(%this, "Undo", , %schemeNormal);
    %n = (%n + 1.0);
    %schemeDisabled.add(%this, "---", );
    %n = (%n + 1.0);
    if (%canCut) {
    }
    %schemeDisabled.add(%this, "Cut", , %schemeNormal);
    %n = (%n + 1.0);
    if (%canCopy) {
    }
    %schemeDisabled.add(%this, "Copy", , %schemeNormal);
    %n = (%n + 1.0);
    if (%canPaste) {
    }
    %schemeDisabled.add(%this, "Paste", , %schemeNormal);
    %n = (%n + 1.0);
    if (%canCut) {
    }
    %schemeDisabled.add(%this, "Delete", , %schemeNormal);
    %n = (%n + 1.0);
    %schemeDisabled.add(%this, "---", );
    %n = (%n + 1.0);
    %schemeNormal.add(%this, "Select All", );
};
function EditContextMenu::onCancel(%this) {
    %this.ctrl.showCursor = 0;
};
function EditContextMenu::onSelect(%this, %unused, %text) {
    %this.ctrl.showCursor = 0;
    if (!(isObject(%this.ctrl))) {
        return;
    }
    if ((%text $= "Undo")) {
        %this.ctrl.doUndo();
    }
    if ((%text $= "Cut")) {
        %this.ctrl.doCut();
    }
    if ((%text $= "Copy")) {
        %this.ctrl.doCopy();
    }
    if ((%text $= "Paste")) {
        %this.ctrl.doPaste();
    }
    if ((%text $= "Delete")) {
        %this.ctrl.deleteSelection();
    }
    if ((%text $= "Select All")) {
        %this.ctrl.selectAll();
    }
};
