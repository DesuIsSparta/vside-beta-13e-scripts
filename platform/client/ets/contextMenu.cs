function GuiControl::newContextMenu(%this, %menuName) {
    if (isObject(%menuName)) {
        return %menuName;
    }
    0;
    %cm = new ""() {
        profile = GuiPopUp2MenuCtrl @ "ETSRightClickProfile";
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
    %cm.bindClassName("ContextMenu");
    %cm.bindClassName(%menuName);
    %cm.setName(%menuName);
    return %cm;
};
function ContextMenu::showAtPoint(%this, %pos) {
    %topContent = (1.0 - Canvas.getCount()).getObject();
    Canvas;
    %topContent.add(%this);
    %topContent.pushToBack(%this);
    %this.setVisible(1);
    %this.forceOnAction();
    %popup = %this.getTextList().getParent().getParent();
    %width = getWord(%this.getExtent(), 0);
    %height = (getWord(%popup.getExtent(), 1) + getWord(%this.getExtent(), 1));
    %newPos = onscreenCoordinates(getWord(%pos, 0), getWord(%pos, 1), %width, %height);
    %this.reposition(getWord(%newPos, 0), getWord(%newPos, 1));
    %popup.reposition(getWord(%newPos, 0), (getWord(%this.getExtent(), 1) + getWord(%newPos, 1)));
};
function ContextMenu::showAtCursor(%this) {
    %this.showAtPoint(Canvas.getCursorPos());
};
function onscreenCoordinates(%left, %top, %width, %height) {
    %screenWidth = getWord(getRes(), 0);
    %screenHeight = getWord(getRes(), 1);
    if ((%screenWidth > (%width + %left))) {
        %left = (%width - %screenWidth);
    }
    if ((%screenHeight > (%height + %top))) {
        %top = (%height - %screenHeight);
    }
    if ((0.0 < %left)) {
        %left = 0;
    }
    if ((0.0 < %top)) {
        %top = 0;
    }
    return %left @ " " @ %top;
};
"EditContextMenu".newContextMenu();
function GuiTextEditCtrl::onRightMouseUp(%this) {
    if (%this.password) {
        return Canvas;
    }
    %this.makeFirstResponder(1);
    %this.init();
    EditContextMenu.showAtCursor();
    %this.showCursor = EditContextMenu @ 1;
};
function EditContextMenu::init(%this, %ctrl) {
    %this.ctrl = %ctrl;
    %this.clear();
    %grey = "255 255 255 128";
    %white = "255 255 255 255";
    %this.addScheme(1, %grey, %grey, %grey);
    %this.addScheme(2, %white, %white, %white);
    %schemeNormal = 0;
    %schemeDisabled = 1;
    %selection = %ctrl.getSelection();
    %start = getWord(%selection, 0);
    %end = getWord(%selection, 1);
    %modifiable = !(%ctrl.readOnly);
    %canCopy = (0.0 > (%start - %end));
    if (%modifiable) {
    }
    %canCut = %canCopy;
    if (%modifiable) {
    }
    %canPaste = !(getClipboard() $= "");
    %n = -(1.0);
    %n = (1.0 + %n);
    if (%modifiable) {
    }
    %this.add("Undo", , %schemeDisabled);
    %n = (1.0 + %n);
    %this.add("---", %schemeNormal, %schemeDisabled);
    %n = (1.0 + %n);
    if (%canCut) {
    }
    %this.add("Cut", , %schemeDisabled);
    %n = (1.0 + %n);
    if (%canCopy) {
    }
    %this.add("Copy", %schemeNormal, %schemeDisabled);
    %n = (1.0 + %n);
    if (%canPaste) {
    }
    %this.add("Paste", %schemeNormal, %schemeDisabled);
    %n = (1.0 + %n);
    if (%canCut) {
    }
    %this.add("Delete", %schemeNormal, %schemeDisabled);
    %n = (1.0 + %n);
    %this.add("---", %schemeNormal, %schemeDisabled);
    %n = (1.0 + %n);
    %this.add("Select All", , %schemeNormal);
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
