function GuiControl::newContextMenu(%this, %menuName) {
    if (isObject(%menuName)) {
        return %menuName;
    }
    profile = GuiPopUp2MenuCtrl @ new ""() @ "ETSRightClickProfile";
    0;
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
    %cm = ;
    %cm.bindClassName("ContextMenu");
    %cm.bindClassName(%menuName);
    %cm.setName(%menuName);
    return %cm;
};
function ContextMenu::showAtPoint(%this, %pos) {
    %topContent = (Canvas - getCount()).getObject();
    1.0;
    %topContent.add(%this);
    %topContent.pushToBack(%this);
    %this.setVisible(1);
    %this.forceOnAction();
    %popup = %this.getTextList().getParent().getParent();
    Canvas;
    %width = getWord(%this.getExtent(), 0);
    %height = (getWord(%popup.getExtent(), 1) + getWord(%this.getExtent(), 1));
    %newPos = onscreenCoordinates(getWord(%pos, 0), getWord(%pos, 1), %width, %height);
    %this.reposition(getWord(%newPos, 0), getWord(%newPos, 1));
    %popup.reposition(getWord(%newPos, 0), (getWord(%this.getExtent(), 1) + getWord(%newPos, 1)));
};
function ContextMenu::showAtCursor(%this) {
    %this.showAtPoint(getCursorPos());
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
    if (password) {
        return %this;
    }
    %this.makeFirstResponder(1);
    %this.init();
    showAtCursor();
    showCursor = EditContextMenu @ 1 @ %this;
    EditContextMenu;
};
function EditContextMenu::init(%this, %ctrl) {
    ctrl = %ctrl @ %this;
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
    %modifiable = !(readOnly);
    %ctrl;
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
    showCursor = %this @ ctrl;
    0;
};
function EditContextMenu::onSelect(%this, %unused, %text) {
    showCursor = %this @ ctrl;
    0;
    if (!(isObject(ctrl))) {
        return %this;
    }
    if ((%text $= "Undo")) {
        ctrl.doUndo();
    }
    if ((%this SPC %text $= "Cut")) {
        ctrl.doCut();
    }
    if ((%this SPC %text $= "Copy")) {
        ctrl.doCopy();
    }
    if ((%this SPC %text $= "Paste")) {
        ctrl.doPaste();
    }
    if ((%this SPC %text $= "Delete")) {
        ctrl.deleteSelection();
    }
    if ((%this SPC %text $= "Select All")) {
        ctrl.selectAll();
    }
};
