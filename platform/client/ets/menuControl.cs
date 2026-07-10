function MenuLayer::Initialize() {
    if (!(isObject(MenuLayer))) {
        new GuiMouseEventCtrl(MenuLayer) {
            profile = "GuiDefaultProfile";
            horizSizing = "width";
            vertSizing = "height";
            position = "0 0";
            extent = getWords(getRes(), 0, 1);
            minExtent = "1 1";
            visible = 0;
        };
        stack = "" @ MenuLayer;
        justOpened = 0 @ MenuLayer;
        activeButton = 0 @ MenuLayer;
        activeMenu = 0 @ MenuLayer;
        menus = new SimSet(""); @ MenuLayer;
        0;
        clones = new SimSet(""); @ MenuLayer;
        0;
    }
};
function MenuLayer::show(%this) {
    if (MenuLayer.isVisible()) {
        return;
    }
    MenuLayer::Initialize();
    0.pushDialog(Canvas, %this);
    1.setVisible(%this);
    %this.justOpened = 1;
    "shownForAWhile".schedule(%this, 500);
};
function MenuLayer::shownForAWhile(%this) {
    %this.justOpened = 0;
};
function MenuLayer::addCloneOf(%this, %ctrl) {
    %clone = new GuiMouseEventCtrl("") {
        profile = 0 @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %ctrl.getScreenPosition();
        extent = %ctrl.getExtent();
        minExtent = "1 1";
        sluggishness = -(1.0);
        visible = 1;
        tooltip = %ctrl.tooltip;
        original = %ctrl;
        layer = %this;
    };
    "MenuButtonClone".bindClassName(%clone);
    %clone.add(%this);
    %clone.add(%this.clones);
};
function MenuLayer::deleteClones(%this) {
    %count = %this.clones.getCount();
    %i = (%count - 1.0);
    while ((%i >= 0.0)) {
        0.getObject(%this.clones).delete();
        %i = (%i - 1.0);
    }
};
function MenuLayer::setActiveButton(%this, %ctrl) {
    if (!(isObject(%ctrl))) {
        return;
    }
    if ((%this.clones.getCount() == 0.0)) {
        %parent = %ctrl.getParent();
        if (isObject(%parent)) {
            %count = %parent.getCount();
            %i = 0;
            while ((%i < %count)) {
                %child = %i.getObject(%parent);
                if ((%child.buttonType $= "MenuButton")) {
                    if ((%child.getId() == %ctrl.getId())) {
                        %this.activeButton = %child.getId();
                    }
                    %child.addCloneOf(%this);
                }
                %i = (%i + 1.0);
            }
        }
    }
    if ((%this.activeButton.getId() != %ctrl.getId())) {
        %this.activeButton.depressed = (%i < %count) @ 0;
        %this.activeButton.menu.hide();
        %this.activeButton = %ctrl.getId();
        %this.activeButton.depressed = 1;
        1.showRelativeTo(%this.activeButton.menu, %this.activeButton);
    }
};
function MenuLayer::nextActiveButton(%this) {
    if (isObject(%this.activeButton)) {
        %next = 0;
        %count = %this.clones.getCount();
        %i = 0;
        while ((%i < %count)) {
            if ((%i.getObject(%this.clones).original == %this.activeButton.getId())) {
                %next = ((%i + 1.0) % %count).getObject(%this.clones).original;
            }
            %i = (%i + 1.0);
        }
        %next.setActiveButton(%this);
    }
};
function MenuLayer::previousActiveButton(%this) {
    if (isObject(%this.activeButton)) {
        %prev = 0;
        %count = %this.clones.getCount();
        %i = 0;
        while ((%i < %count)) {
            if ((%i.getObject(%this.clones).original == %this.activeButton.getId())) {
                %prev = (((%i - 1.0) + %count) % %count).getObject(%this.clones).original;
            }
            %i = (%i + 1.0);
        }
        %prev.setActiveButton(%this);
    }
};
function MenuLayer::hide(%this) {
    if (!(%this.isVisible())) {
        return;
    }
    0.setVisible(%this);
    %this.popDialog(Canvas);
    %count = %this.getCount();
    %i = 0;
    while ((%i < %count)) {
        0.setVisible(%i.getObject(%this));
        %i = (%i + 1.0);
    }
    %this.stack = (%i < %count) @ "";
    if (isObject(%this.activeButton)) {
        %this.activeButton.depressed = 0;
    }
    %this.deleteClones();
};
function MenuLayer::pop(%this) {
    %topMost = getWord(%this.stack, 0);
    %this.stack = removeWord(%this.stack, 0);
    if (isObject(%topMost)) {
        %topMost.hide();
    }
    %nextHighest = getWord(%this.stack, 0);
    if (isObject(%nextHighest)) {
        1.makeFirstResponder(%nextHighest);
    }
};
function MenuLayer::popToMenu(%this, %menu) {
    %idx = findWord(%this.stack, %menu);
    if ((%idx > 0.0)) {
        %i = 0;
        while ((%i < %idx)) {
            0.setVisible(getWord(MenuLayer, %this.stack, %i).scroll);
            %i = (%i + 1.0);
        }
    }
    %this.stack = (%i < %idx) @ getWords(%this.stack, %idx);
    %nextHighest = getWord(%this.stack, 0);
    if (isObject(%nextHighest)) {
        1.makeFirstResponder(%nextHighest);
    }
};
function MenuLayer::push(%this, %menu) {
    %menu = %menu.getId();
    %cleanStack = removeWord(%this.stack, findWord(%this.stack, %menu));
    %this.stack = trim(%menu @ " " @ %cleanStack);
};
function MenuLayer::close(%this) {
    %this.pop();
    if ((%this.stack $= "")) {
        %this.hide();
    }
};
function MenuLayer::onMouseDown(%this) {
    %this.hide();
};
function MenuLayer::onMouseUp(%this) {
    if (!(%this.justOpened)) {
        %this.hide();
    }
};
function MenuLayer::onRightMouseDown(%this) {
    %this.hide();
};
function MenuLayer::newMenu(%menuName) {
    MenuLayer::Initialize();
    %menu = MenuControl::newMenuWithScroll(%menuName);
    %menu.layer = MenuLayer;
    %menu.scroll.add(MenuLayer);
    %menu.add(MenuLayer, %menu.menus);
    return %menu;
};
function MenuButtonClone::onMouseEnter(%this) {
    if ((%this.original.getId() != %this.layer.activeButton.getId())) {
        %this.original.schedule(%this.layer, 0, "setActiveButton");
    }
};
function MenuButtonClone::onMouseDown(%this) {
    "hide".schedule(%this.layer, 0);
};
function MenuButtonClone::onMouseUp(%this) {
    "onMouseUp".schedule(%this.layer, 0);
};
function MenuControl::newMenuWithScroll(%menuName) {
    if (isObject(%menuName)) {
        return %menuName.getId();
    }
    %scroll = new GuiScrollCtrl("") {
        profile = 0 @ "ETSScrollProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "216 24";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        childMargin = "0 0";
    };
    %menu = new GuiArray2Ctrl("") {
        profile = 0 @ "ETSMenuProfile";
        childrenClassName = "GuiMouseEventCtrl";
        childrenExtent = "210 24";
        spacing = 2;
        numRowsOrCols = 1;
        inRows = 0;
        keyWrapX = 0;
        hilited = 0;
        unselectedProfile = "GuiDefaultProfile";
        selectedProfile = "ETSSelectedMenuItemProfile";
        menuTextProfile = "ETSUnselectedMenuTextProfile";
        menuTextSelectedProfile = "ETSSelectedMenuTextProfile";
    };
    "MenuControl".bindClassName(%menu);
    %menuName.setName(%menu);
    %menu.layer = 0;
    %menu.add(%scroll);
    %menu.scroll = %scroll;
    return %menu;
};
function MenuControl::onCreatedChild(%this, %child) {
    %icon = new GuiBitmapCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "24 24";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };
    %icon.add(%child);
    %child.icon = %icon;
    %menuText = new GuiTextCtrl("") {
        profile = 0 @ %this.menuTextProfile;
        horizSizing = "right";
        vertSizing = "bottom";
        position = "30 2";
        extent = "210 20";
        minExtent = "1 1";
        sluggishness = -(1.0);
        visible = 1;
        text = "";
        maxLength = 255;
    };
    %menuText.add(%child);
    %child.menuText = %menuText;
    %accelText = new GuiTextCtrl("") {
        profile = 0 @ %this.menuTextProfile;
        horizSizing = "right";
        vertSizing = "bottom";
        position = (getWord(%child.getExtent(), 0) - 35.0) @ " " @ 2;
        extent = "35 20";
        minExtent = "1 1";
        sluggishness = -(1.0);
        visible = 1;
        text = %child.accelerator;
        maxLength = 255;
    };
    %accelText.add(%child);
    %child.accelText = %accelText;
    %width = getWord(%child.getExtent(), 0);
    %height = getWord(%child.getExtent(), 1);
    %subArrow = new GuiBitmapCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "left";
        vertSizing = "center";
        position = (%width - 10.0) @ " " @ ((%height / 2.0) - 5.0);
        extent = "5 9";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        bitmap = "platform/client/ui/submenu_arrow";
    };
    %subArrow.add(%child);
    %child.subArrow = %subArrow;
    %child.Parent = %this;
    if (!(getWord(%child.getNamespaceList(), 0) $= "MenuItem")) {
        "MenuItem".bindClassName(%child);
    }
};
function MenuControl::onKeyDown(%this, %unused, %keyCode) {
    if (isObject(%this.layer)) {
        if ((%keyCode.getStringFromKeyCode(%this) $= "left")) {
            %this.layer.previousActiveButton();
            return 1;
        }
        if ((%keyCode.getStringFromKeyCode(%this) $= "right")) {
            %this.layer.nextActiveButton();
            return 1;
        }
    }
    return 0;
};
function MenuControl::addMenuItem(%this, %text, %command, %icon, %accelerator) {
    %item = %this.addChild();
    %text.setMenuItemText(%item);
    %item.command = %command;
    %icon.setIcon(%item);
    %accelerator.setAccelerator(%item);
    %item.submenu = 0;
    0.setVisible(%item.subArrow);
    if (!(%this.deferReseat)) {
        %this.reseatChildren();
    }
    return %item;
};
function MenuControl::addSubmenu(%this, %text, %icon, %menuName) {
    if (!(isObject(%this.layer))) {
        return;
    }
    %item = %this.addChild();
    %text.setMenuItemText(%item);
    %item.command = "";
    %icon.setIcon(%item);
    %item.submenu = MenuLayer::newMenu(%menuName);
    %item.submenu.layer = %this.layer;
    1.setVisible(%item.subArrow);
    %this.reseatChildren();
    return %item;
};
function MenuControl::showRelativeTo(%this, %baseCtrl, %vertical) {
    if (isObject(%this.layer)) {
        %this.layer.show();
        %baseCtrl.setActiveButton(%this.layer);
    }
    %vertical.positionRelativeTo(%this, %baseCtrl);
    %this.baseCtrl = %baseCtrl;
    %this.show();
};
function MenuControl::show(%this) {
    if (isObject(%this.layer)) {
        %this.push(%this.layer);
    }
    1.setVisible(%this.scroll);
    -(1.0).hiliteCell(%this, -(1.0));
    1.makeFirstResponder(%this);
};
function MenuControl::hide(%this) {
    if (isObject(%this.layer)) {
        %idx = findWord(%this.layer.stack, %this.getId());
        if ((%idx != -(1.0))) {
            %i = 0;
            while ((%i < %idx)) {
                0.setVisible(getWord(%this.layer.stack, %i).scroll);
                %i = (%i + 1.0);
            }
            %this.layer.stack = (%i < %idx) @ getWords(%this.layer.stack, (%idx + 1.0));
        }
    }
    0.makeFirstResponder(%this);
    -(1.0).hiliteCell(%this, -(1.0));
    0.setVisible(%this.scroll);
};
function MenuControl::positionRelativeTo(%this, %baseCtrl, %vertical) {
    %scrollCtrl = %this.scroll;
    %screenWidth = getWord(getRes(), 0);
    %screenHeight = getWord(getRes(), 1);
    %topMargin = getWord(%baseCtrl.getScreenPosition(), 1);
    %leftMargin = getWord(%baseCtrl.getScreenPosition(), 0);
    if (%vertical) {
        %menuHeight = getWord(%this.getExtent(), 1);
        %ctrlBottom = (getWord(%baseCtrl.getExtent(), 1) + %topMargin);
        %bottomMargin = (%screenHeight - %ctrlBottom);
        if ((%menuHeight <= %bottomMargin)) {
            %left = %leftMargin;
            %top = %ctrlBottom;
            %width = getWord(%scrollCtrl.getExtent(), 0);
            %height = (%menuHeight + 2.0);
        }
        if ((%topMargin >= %bottomMargin)) {
            %height = mMin(%topMargin, (getWord(%this.getExtent(), 1) + 2.0));
            %left = %leftMargin;
            %top = ((%topMargin - %height) - 6.0);
            %width = getWord(%scrollCtrl.getExtent(), 0);
        }
        %left = %leftMargin;
        %top = %ctrlBottom;
        %width = getWord(%scrollCtrl.getExtent(), 0);
        %height = %bottomMargin;
    }
    %menuWidth = getWord(%this.getExtent(), 0);
    %ctrlRight = (getWord(%baseCtrl.getExtent(), 0) + %leftMargin);
    %rightMargin = (%screenWidth - %ctrlRight);
    if ((%menuWidth <= %rightMargin)) {
    }
    if ((%rightMargin >= %leftMargin)) {
        %left = %ctrlRight;
        %top = %topMargin;
        %width = getWord(%scrollCtrl.getExtent(), 0);
        %height = (getWord(%this.getExtent(), 1) + 2.0);
    }
    %left = (%leftMargin - getWord(%scrollCtrl.getExtent(), 0));
    %top = %topMargin;
    %width = getWord(%scrollCtrl.getExtent(), 0);
    %height = (getWord(%this.getExtent(), 1) + 2.0);
    %width = mMin(%width, %screenWidth);
    %height = mMin(%height, %screenHeight);
    %onscreen = onscreenCoordinates(%left, %top, %width, %height);
    %left = getWord(%onscreen, 0);
    %top = getWord(%onscreen, 1);
    %height.resize(%scrollCtrl, %left, %top, %width);
};
function MenuItem::setMenuItemText(%this, %text) {
    if (isObject(%this.menuText)) {
        %text.setText(%this.menuText);
    }
};
function MenuItem::setIcon(%this, %icon) {
    if (isObject(%this.icon)) {
        %icon.setBitmap(%this.icon);
    }
};
function MenuItem::setAccelerator(%this, %accelerator) {
    %this.accelerator = %accelerator;
    %accelerator.setText(%this.accelText);
};
function MenuItem::onHilite(%this) {
    %scroll = %this.Parent.scroll;
    %this.Parent.selectedProfile.setProfile(%this);
    %this.menuText.text = %this.menuText.getValue();
    %this.Parent.menuTextSelectedProfile.setProfile(%this.menuText);
    %cellHeight = (getWord(%this.Parent.childrenExtent, 1) + %this.Parent.spacing);
    %numRowsVisible = (getWord(%scroll.getExtent(), 1) / %cellHeight);
    %ypos = (1.0 - getWord(%this.Parent.getPosition(), 1));
    %closestRow = ((%ypos - %this.Parent.spacing) / %cellHeight);
    %targetRow = getWord(%this.Parent.hilitedCell, 1);
    if ((%targetRow < %closestRow)) {
        ((%cellHeight * %targetRow) + %this.Parent.spacing).scrollTo(%scroll, 0);
    }
    if ((%targetRow > ((%closestRow + %numRowsVisible) - 1.0))) {
        (%cellHeight * ((%targetRow - %numRowsVisible) + 1.0)).scrollTo(%scroll, 0);
    }
    %layer = %this.Parent.layer;
    if (isObject(%layer)) {
        cancel(%layer.hoverTimer);
        %layer.hoverTimer = "onMouseHover".schedule(%this, 400);
    }
    if (isObject(%layer)) {
        %currentMenu = %this.Parent;
        %thisIdx = findWord(%layer.stack, %currentMenu.getId());
        if ((%thisIdx != -(1.0))) {
            %parentMenu = getWord(%layer.stack, (%thisIdx + 1.0));
            if (isObject(%parentMenu)) {
                %count = %parentMenu.getCount();
                %cellIdx = -(1.0);
                %i = 0;
                while ((%i < %count)) {
                    if ((%i.getObject(%parentMenu).submenu == %currentMenu.getId())) {
                        %cellIdx = %i;
                    }
                    %i = (%i + 1.0);
                }
                if ((%cellIdx != -(1.0))) {
                    %cellIdx.hiliteCell(%parentMenu, 0);
                }
            }
        }
    }
};
function MenuItem::onUnhilite(%this) {
    %this.Parent.unselectedProfile.setProfile(%this);
    if (isObject(%this.menuText)) {
        %this.menuText.text = %this.menuText.getValue();
        %this.Parent.menuTextProfile.setProfile(%this.menuText);
    }
};
function MenuItem::onSelect(%this) {
    eval(%this.command);
    if (isObject(%this.submenu)) {
        %this.openSubmenu();
    }
    if (isObject(%this.Parent.layer)) {
        %this.Parent.layer.hide();
    }
};
function MenuItem::openSubmenu(%this) {
    if (!(isObject(%this.Parent.layer))) {
        return;
    }
    if (isObject(%this.submenu)) {
        if (!(%this.submenu.scroll.isVisible())) {
            %this.Parent.popToMenu(%this.Parent.layer);
            0.showRelativeTo(%this.submenu, %this);
        }
    }
    %this.Parent.popToMenu(%this.Parent.layer);
};
function MenuItem::onMouseEnterBounds(%this) {
    %count = %this.Parent.getCount();
    %i = 0;
    while ((%i < %count)) {
        if ((%this.getId() == %i.getObject(%this.Parent))) {
        }
        %i = (%i + 1.0);
    }
    %i.hiliteCell(%this.Parent, 0);
};
function MenuItem::onMouseLeaveBounds(%this) {
};
function MenuItem::onMouseUp(%this) {
    %this.onSelect();
};
function MenuItem::onMouseMove(%this) {
    if (isObject(%this.Parent.layer)) {
        cancel(%this.Parent.layer.hoverTimer);
        %this.Parent.layer.hoverTimer = "onMouseHover".schedule(%this, 400);
    }
};
function MenuItem::onMouseHover(%this) {
    %this.openSubmenu();
};
