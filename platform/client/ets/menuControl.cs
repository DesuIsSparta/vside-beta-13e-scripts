function MenuLayer::Initialize()
{
    if (!isObject(MenuLayer))
    {
        new GuiMouseEventCtrl(MenuLayer)
        {
            profile = "GuiDefaultProfile";
            horizSizing = "width";
            vertSizing = "height";
            position = "0 0";
            extent = getWords(getRes(), 0, 1);
            minExtent = "1 1";
            visible = 0;
        };
        MenuLayer.stack = "";
        MenuLayer.justOpened = 0;
        MenuLayer.activeButton = 0;
        MenuLayer.activeMenu = 0;
        MenuLayer.menus = new SimSet();
        MenuLayer.clones = new SimSet();
    }
    return ;
}
function MenuLayer::show(%this)
{
    if (MenuLayer.isVisible())
    {
        return ;
    }
    MenuLayer::Initialize();
    Canvas.pushDialog(%this, 0);
    %this.setVisible(1);
    %this.justOpened = 1;
    %this.schedule(500, "shownForAWhile");
    return ;
}
function MenuLayer::shownForAWhile(%this)
{
    %this.justOpened = 0;
    return ;
}
function MenuLayer::addCloneOf(%this, %ctrl)
{
    %clone = new GuiMouseEventCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %ctrl.getScreenPosition();
        extent = %ctrl.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        tooltip = %ctrl.tooltip;
        original = %ctrl;
        layer = %this;
    };
    %clone.bindClassName("MenuButtonClone");
    %this.add(%clone);
    %this.clones.add(%clone);
    return ;
}
function MenuLayer::deleteClones(%this)
{
    %count = %this.clones.getCount();
    %i = %count - 1;
    while (%i >= 0)
    {
        %this.clones.getObject(0).delete();
        %i = %i - 1;
    }
}

function MenuLayer::setActiveButton(%this, %ctrl)
{
    if (!isObject(%ctrl))
    {
        return ;
    }
    if (%this.clones.getCount() == 0)
    {
        %parent = %ctrl.getParent();
        if (isObject(%parent))
        {
            %count = %parent.getCount();
            %i = 0;
            while (%i < %count)
            {
                %child = %parent.getObject(%i);
                if (%child.buttonType $= "MenuButton")
                {
                    if (%child.getId() == %ctrl.getId())
                    {
                        %this.activeButton = %child.getId();
                    }
                    %this.addCloneOf(%child);
                }
                %i = %i + 1;
            }
        }
    }
    else
    {
        if (%this.activeButton.getId() != %ctrl.getId())
        {
            %this.activeButton.depressed = 0;
            %this.activeButton.menu.hide();
            %this.activeButton = %ctrl.getId();
            %this.activeButton.depressed = 1;
            %this.activeButton.menu.showRelativeTo(%this.activeButton, 1);
        }
    }
    return ;
}
function MenuLayer::nextActiveButton(%this)
{
    if (isObject(%this.activeButton))
    {
        %next = 0;
        %count = %this.clones.getCount();
        %i = 0;
        while (%i < %count)
        {
            if (%this.clones.getObject(%i).original == %this.activeButton.getId())
            {
                %next = %this.clones.getObject((%i + 1) % %count).original;
                break;
            }
            %i = %i + 1;
        }
        %this.setActiveButton(%next);
    }
    return ;
}
function MenuLayer::previousActiveButton(%this)
{
    if (isObject(%this.activeButton))
    {
        %prev = 0;
        %count = %this.clones.getCount();
        %i = 0;
        while (%i < %count)
        {
            if (%this.clones.getObject(%i).original == %this.activeButton.getId())
            {
                %prev = %this.clones.getObject(((%i - 1) + %count) % %count).original;
                break;
            }
            %i = %i + 1;
        }
        %this.setActiveButton(%prev);
    }
    return ;
}
function MenuLayer::hide(%this)
{
    if (!%this.isVisible())
    {
        return ;
    }
    %this.setVisible(0);
    Canvas.popDialog(%this);
    %count = %this.getCount();
    %i = 0;
    while (%i < %count)
    {
        %this.getObject(%i).setVisible(0);
        %i = %i + 1;
    }
    %this.stack = "";
    if (isObject(%this.activeButton))
    {
        %this.activeButton.depressed = 0;
    }
    %this.deleteClones();
    return ;
}
function MenuLayer::pop(%this)
{
    %topMost = getWord(%this.stack, 0);
    %this.stack = removeWord(%this.stack, 0);
    if (isObject(%topMost))
    {
        %topMost.hide();
    }
    %nextHighest = getWord(%this.stack, 0);
    if (isObject(%nextHighest))
    {
        %nextHighest.makeFirstResponder(1);
    }
    return ;
}
function MenuLayer::popToMenu(%this, %menu)
{
    %idx = findWord(%this.stack, %menu);
    if (%idx > 0)
    {
        %i = 0;
        while (%i < %idx)
        {
            getWord(MenuLayer.stack, %i).scroll.setVisible(0);
            %i = %i + 1;
        }
    }
    %this.stack = getWords(%this.stack, %idx);
    %nextHighest = getWord(%this.stack, 0);
    if (isObject(%nextHighest))
    {
        %nextHighest.makeFirstResponder(1);
    }
    return ;
}
function MenuLayer::push(%this, %menu)
{
    %menu = %menu.getId();
    %cleanStack = removeWord(%this.stack, findWord(%this.stack, %menu));
    %this.stack = trim(%menu SPC %cleanStack);
    return ;
}
function MenuLayer::close(%this)
{
    %this.pop();
    if (%this.stack $= "")
    {
        %this.hide();
    }
    return ;
}
function MenuLayer::onMouseDown(%this)
{
    %this.hide();
    return ;
}
function MenuLayer::onMouseUp(%this)
{
    if (!%this.justOpened)
    {
        %this.hide();
    }
    return ;
}
function MenuLayer::onRightMouseDown(%this)
{
    %this.hide();
    return ;
}
function MenuLayer::newMenu(%menuName)
{
    MenuLayer::Initialize();
    %menu = MenuControl::newMenuWithScroll(%menuName);
    %menu.layer = MenuLayer;
    MenuLayer.add(%menu.scroll);
    MenuLayer.menus.add(%menu);
    return %menu;
}
function MenuButtonClone::onMouseEnter(%this)
{
    if (%this.original.getId() != %this.layer.activeButton.getId())
    {
        %this.layer.schedule(0, "setActiveButton", %this.original);
    }
    return ;
}
function MenuButtonClone::onMouseDown(%this)
{
    %this.layer.schedule(0, "hide");
    return ;
}
function MenuButtonClone::onMouseUp(%this)
{
    %this.layer.schedule(0, "onMouseUp");
    return ;
}
function MenuControl::newMenuWithScroll(%menuName)
{
    if (isObject(%menuName))
    {
        return %menuName.getId();
    }
    %scroll = new GuiScrollCtrl()
    {
        profile = "ETSScrollProfile";
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
    %menu = new GuiArray2Ctrl()
    {
        profile = "ETSMenuProfile";
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
    %menu.bindClassName("MenuControl");
    %menu.setName(%menuName);
    %menu.layer = 0;
    %scroll.add(%menu);
    %menu.scroll = %scroll;
    return %menu;
}
function MenuControl::onCreatedChild(%this, %child)
{
    %icon = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "24 24";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };
    %child.add(%icon);
    %child.icon = %icon;
    %menuText = new GuiTextCtrl()
    {
        profile = %this.menuTextProfile;
        horizSizing = "right";
        vertSizing = "bottom";
        position = "30 2";
        extent = "210 20";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 255;
    };
    %child.add(%menuText);
    %child.menuText = %menuText;
    %accelText = new GuiTextCtrl()
    {
        profile = %this.menuTextProfile;
        horizSizing = "right";
        vertSizing = "bottom";
        position = getWord(%child.getExtent(), 0) - 35 SPC 2;
        extent = "35 20";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = %child.accelerator;
        maxLength = 255;
    };
    %child.add(%accelText);
    %child.accelText = %accelText;
    %width = getWord(%child.getExtent(), 0);
    %height = getWord(%child.getExtent(), 1);
    %subArrow = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "left";
        vertSizing = "center";
        position = %width - 10 SPC (%height / 2) - 5;
        extent = "5 9";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        bitmap = "platform/client/ui/submenu_arrow";
    };
    %child.add(%subArrow);
    %child.subArrow = %subArrow;
    %child.Parent = %this;
    if (!(getWord(%child.getNamespaceList(), 0) $= "MenuItem"))
    {
        %child.bindClassName("MenuItem");
    }
    return ;
}
function MenuControl::onKeyDown(%this, %unused, %keyCode)
{
    if (isObject(%this.layer))
    {
        if (%this.getStringFromKeyCode(%keyCode) $= "left")
        {
            %this.layer.previousActiveButton();
            return 1;
        }
        else
        {
            if (%this.getStringFromKeyCode(%keyCode) $= "right")
            {
                %this.layer.nextActiveButton();
                return 1;
            }
        }
    }
    return 0;
}
function MenuControl::addMenuItem(%this, %text, %command, %icon, %accelerator)
{
    %item = %this.addChild();
    %item.setMenuItemText(%text);
    %item.command = %command;
    %item.setIcon(%icon);
    %item.setAccelerator(%accelerator);
    %item.submenu = 0;
    %item.subArrow.setVisible(0);
    if (!%this.deferReseat)
    {
        %this.reseatChildren();
    }
    return %item;
}
function MenuControl::addSubmenu(%this, %text, %icon, %menuName)
{
    if (!isObject(%this.layer))
    {
        return ;
    }
    %item = %this.addChild();
    %item.setMenuItemText(%text);
    %item.command = "";
    %item.setIcon(%icon);
    %item.submenu = MenuLayer::newMenu(%menuName);
    %item.submenu.layer = %this.layer;
    %item.subArrow.setVisible(1);
    %this.reseatChildren();
    return %item;
}
function MenuControl::showRelativeTo(%this, %baseCtrl, %vertical)
{
    if (isObject(%this.layer))
    {
        %this.layer.show();
        %this.layer.setActiveButton(%baseCtrl);
    }
    %this.positionRelativeTo(%baseCtrl, %vertical);
    %this.baseCtrl = %baseCtrl;
    %this.show();
    return ;
}
function MenuControl::show(%this)
{
    if (isObject(%this.layer))
    {
        %this.layer.push(%this);
    }
    %this.scroll.setVisible(1);
    %this.hiliteCell(-1, -1);
    %this.makeFirstResponder(1);
    return ;
}
function MenuControl::hide(%this)
{
    if (isObject(%this.layer))
    {
        %idx = findWord(%this.layer.stack, %this.getId());
        if (%idx != -1)
        {
            %i = 0;
            while (%i < %idx)
            {
                getWord(%this.layer.stack, %i).scroll.setVisible(0);
                %i = %i + 1;
            }
            %this.layer.stack = getWords(%this.layer.stack, %idx + 1);
        }
    }
    %this.makeFirstResponder(0);
    %this.hiliteCell(-1, -1);
    %this.scroll.setVisible(0);
    return ;
}
function MenuControl::positionRelativeTo(%this, %baseCtrl, %vertical)
{
    %scrollCtrl = %this.scroll;
    %screenWidth = getWord(getRes(), 0);
    %screenHeight = getWord(getRes(), 1);
    %topMargin = getWord(%baseCtrl.getScreenPosition(), 1);
    %leftMargin = getWord(%baseCtrl.getScreenPosition(), 0);
    if (%vertical)
    {
        %menuHeight = getWord(%this.getExtent(), 1);
        %ctrlBottom = getWord(%baseCtrl.getExtent(), 1) + %topMargin;
        %bottomMargin = %screenHeight - %ctrlBottom;
        if (%menuHeight <= %bottomMargin)
        {
            %left = %leftMargin;
            %top = %ctrlBottom;
            %width = getWord(%scrollCtrl.getExtent(), 0);
            %height = %menuHeight + 2;
        }
        else
        {
            if (%topMargin >= %bottomMargin)
            {
                %height = mMin(%topMargin, getWord(%this.getExtent(), 1) + 2);
                %left = %leftMargin;
                %top = (%topMargin - %height) - 6;
                %width = getWord(%scrollCtrl.getExtent(), 0);
            }
            else
            {
                %left = %leftMargin;
                %top = %ctrlBottom;
                %width = getWord(%scrollCtrl.getExtent(), 0);
                %height = %bottomMargin;
            }
        }
    }
    else
    {
        %menuWidth = getWord(%this.getExtent(), 0);
        %ctrlRight = getWord(%baseCtrl.getExtent(), 0) + %leftMargin;
        %rightMargin = %screenWidth - %ctrlRight;
        if ((%menuWidth <= %rightMargin) && (%rightMargin >= %leftMargin))
        {
            %left = %ctrlRight;
            %top = %topMargin;
            %width = getWord(%scrollCtrl.getExtent(), 0);
            %height = getWord(%this.getExtent(), 1) + 2;
        }
        else
        {
            %left = %leftMargin - getWord(%scrollCtrl.getExtent(), 0);
            %top = %topMargin;
            %width = getWord(%scrollCtrl.getExtent(), 0);
            %height = getWord(%this.getExtent(), 1) + 2;
        }
    }
    %width = mMin(%width, %screenWidth);
    %height = mMin(%height, %screenHeight);
    %onscreen = onscreenCoordinates(%left, %top, %width, %height);
    %left = getWord(%onscreen, 0);
    %top = getWord(%onscreen, 1);
    %scrollCtrl.resize(%left, %top, %width, %height);
    return ;
}
function MenuItem::setMenuItemText(%this, %text)
{
    if (isObject(%this.menuText))
    {
        %this.menuText.setText(%text);
    }
    return ;
}
function MenuItem::setIcon(%this, %icon)
{
    if (isObject(%this.icon))
    {
        %this.icon.setBitmap(%icon);
    }
    return ;
}
function MenuItem::setAccelerator(%this, %accelerator)
{
    %this.accelerator = %accelerator;
    %this.accelText.setText(%accelerator);
    return ;
}
function MenuItem::onHilite(%this)
{
    %scroll = %this.Parent.scroll;
    %this.setProfile(%this.Parent.selectedProfile);
    %this.menuText.text = %this.menuText.getValue();
    %this.menuText.setProfile(%this.Parent.menuTextSelectedProfile);
    %cellHeight = getWord(%this.Parent.childrenExtent, 1) + %this.Parent.spacing;
    %numRowsVisible = getWord(%scroll.getExtent(), 1) / %cellHeight;
    %ypos = 1 - getWord(%this.Parent.getPosition(), 1);
    %closestRow = (%ypos - %this.Parent.spacing) / %cellHeight;
    %targetRow = getWord(%this.Parent.hilitedCell, 1);
    if (%targetRow < %closestRow)
    {
        %scroll.scrollTo(0, (%cellHeight * %targetRow) + %this.Parent.spacing);
    }
    else
    {
        if (%targetRow > ((%closestRow + %numRowsVisible) - 1))
        {
            %scroll.scrollTo(0, %cellHeight * ((%targetRow - %numRowsVisible) + 1));
        }
    }
    %layer = %this.Parent.layer;
    if (isObject(%layer))
    {
        cancel(%layer.hoverTimer);
        %layer.hoverTimer = %this.schedule(400, "onMouseHover");
    }
    if (isObject(%layer))
    {
        %currentMenu = %this.Parent;
        %thisIdx = findWord(%layer.stack, %currentMenu.getId());
        if (%thisIdx != -1)
        {
            %parentMenu = getWord(%layer.stack, %thisIdx + 1);
            if (isObject(%parentMenu))
            {
                %count = %parentMenu.getCount();
                %cellIdx = -1;
                %i = 0;
                while (%i < %count)
                {
                    if (%parentMenu.getObject(%i).submenu == %currentMenu.getId())
                    {
                        %cellIdx = %i;
                        break;
                    }
                    %i = %i + 1;
                }
                if (%cellIdx != -1)
                {
                    %parentMenu.hiliteCell(0, %cellIdx);
                }
            }
        }
    }
    return ;
}
function MenuItem::onUnhilite(%this)
{
    %this.setProfile(%this.Parent.unselectedProfile);
    if (isObject(%this.menuText))
    {
        %this.menuText.text = %this.menuText.getValue();
        %this.menuText.setProfile(%this.Parent.menuTextProfile);
    }
    return ;
}
function MenuItem::onSelect(%this)
{
    eval(%this.command);
    if (isObject(%this.submenu))
    {
        %this.openSubmenu();
    }
    else
    {
        if (isObject(%this.Parent.layer))
        {
            %this.Parent.layer.hide();
        }
    }
    return ;
}
function MenuItem::openSubmenu(%this)
{
    if (!isObject(%this.Parent.layer))
    {
        return ;
    }
    if (isObject(%this.submenu))
    {
        if (!%this.submenu.scroll.isVisible())
        {
            %this.Parent.layer.popToMenu(%this.Parent);
            %this.submenu.showRelativeTo(%this, 0);
        }
    }
    else
    {
        %this.Parent.layer.popToMenu(%this.Parent);
    }
    return ;
}
function MenuItem::onMouseEnterBounds(%this)
{
    %count = %this.Parent.getCount();
    %i = 0;
    while (%i < %count)
    {
        if (%this.getId() == %this.Parent.getObject(%i))
        {
            continue;
        }
        %i = %i + 1;
    }
    %this.Parent.hiliteCell(0, %i);
    return ;
}
function MenuItem::onMouseLeaveBounds(%this)
{
    return ;
}
function MenuItem::onMouseUp(%this)
{
    %this.onSelect();
    return ;
}
function MenuItem::onMouseMove(%this)
{
    if (isObject(%this.Parent.layer))
    {
        cancel(%this.Parent.layer.hoverTimer);
        %this.Parent.layer.hoverTimer = %this.schedule(400, "onMouseHover");
    }
    return ;
}
function MenuItem::onMouseHover(%this)
{
    %this.openSubmenu();
    return ;
}
