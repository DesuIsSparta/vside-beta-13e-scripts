function GuiControl::newContextMenu(%this, %menuName) {
    if (isObject(%menuName)) {
        return %menuName;
    }
    %cm = new GuiPopUp2MenuCtrl("") {
        profile = "ETSRightClickProfile";
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
    %topContent = Canvas.getObject((Canvas.getCount() - 1.0));
    %topContent.add(%this);
    %topContent.pushToBack(%this);
    %this.setVisible(1);
    %this.forceOnAction();
    %popup = %this.getTextList().getParent().getParent();
    %width = getWord(%this.getExtent(), 0);
    %height = (getWord(%this.getExtent(), 1) + getWord(%popup.getExtent(), 1));
    %newPos = onscreenCoordinates(getWord(%pos, 0), getWord(%pos, 1), %width, %height);
    %this.reposition(getWord(%newPos, 0), getWord(%newPos, 1));
    %popup.reposition(getWord(%newPos, 0), (getWord(%newPos, 1) + getWord(%this.getExtent(), 1)));
};
function ContextMenu::showAtCursor(%this) {
    %this.showAtPoint(Canvas.getCursorPos());
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
Canvas.newContextMenu("EditContextMenu");
function GuiTextEditCtrl::onRightMouseUp(%this) {
    if (%this.password) {
        return;
    }
    %this.makeFirstResponder(1);
    EditContextMenu.init(%this);
    EditContextMenu.showAtCursor();
    %this.showCursor = 1;
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
    %modifiable = !%ctrl.readOnly;
    %canCopy = ((%end - %start) > 0.0);
    if (%modifiable) {
    }
    %canCut = %canCopy;
    if (%modifiable) {
    }
    %canPaste = !(getClipboard() $= "");
    %n = -(1.0);
    if (%modifiable) {
    } else {
    }
    %this.add("Undo", %n = (%n + 1.0), %schemeNormal, %schemeDisabled);
    %this.add("---", %n = (%n + 1.0), %schemeDisabled);
    if (%canCut) {
    } else {
    }
    %this.add("Cut", %n = (%n + 1.0), %schemeNormal, %schemeDisabled);
    if (%canCopy) {
    } else {
    }
    %this.add("Copy", %n = (%n + 1.0), %schemeNormal, %schemeDisabled);
    if (%canPaste) {
    } else {
    }
    %this.add("Paste", %n = (%n + 1.0), %schemeNormal, %schemeDisabled);
    if (%canCut) {
    } else {
    }
    %this.add("Delete", %n = (%n + 1.0), %schemeNormal, %schemeDisabled);
    %this.add("---", %n = (%n + 1.0), %schemeDisabled);
    %this.add("Select All", %n = (%n + 1.0), %schemeNormal);
};
function EditContextMenu::onCancel(%this) {
    %this.ctrl.showCursor = 0;
};
function EditContextMenu::onSelect(%this, %unused, %text) {
    %this.ctrl.showCursor = 0;
    if (!isObject(%this.ctrl)) {
        return;
    }
    if ((%text $= "Undo")) {
        %this.ctrl.doUndo();
    } else {
        if ((%text $= "Cut")) {
            %this.ctrl.doCut();
        } else {
            if ((%text $= "Copy")) {
                %this.ctrl.doCopy();
            } else {
                if ((%text $= "Paste")) {
                    %this.ctrl.doPaste();
                } else {
                    if ((%text $= "Delete")) {
                        %this.ctrl.deleteSelection();
                    } else {
                        if ((%text $= "Select All")) {
                            %this.ctrl.selectAll();
                        }
                    }
                }
            }
        }
    }
};
