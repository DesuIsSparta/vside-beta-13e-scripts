function TabControl::Initialize(%this, %container, %buttonSize, %sepBitmap, %sepSize, %orientation) {
    if (%this.initialized) {
        return;
    }
    if (isObject(%container)) {
    }
    %this.container = %container @ 0;
    %this.buttonSize = %buttonSize;
    %this.hasButtons = 1;
    if ((%buttonSize $= "")) {
        %this.buttonSize = "0 0";
    }
    if ((%this.buttonSize $= "0 0")) {
        %this.hasButtons = 0;
    }
    %this.buttonOffset = %this.getInitialButtonOffset();
    %this.separatorBitmap = %sepBitmap;
    %this.separatorSize = %sepSize;
    %this.orientation = %orientation;
    if ((%this.tabsAlign $= "")) {
        %this.tabsAlign = "near";
    }
    %this.calculateTabDims();
    %this.visibleTabsWidth = 1;
    %this.container.clear();
    %this.numTabs = 0;
    %this.currentTabIndex = -(1.0);
    %this.prevTabIndex = -(1.0);
    if ((%this.maxTabs <= 0.0)) {
        %this.maxTabs = 10;
    }
    %this.overrideLockedOpen = 0;
    if ((getWord(%this.separatorSize, 1) > 0.0)) {
        %this.drawSeparator();
    }
    %this.initialized = 1;
    if (%this.hasButtons) {
        %this.hiddenButton = new GuiBitmapButtonCtrl("") {
            profile = 0 @ "GuiClickLabelProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 0";
            extent = %this.buttonSize;
            minExtent = "1 1";
            visible = 1;
            command = %this.getId() @ ".onHiddenButton();";
            text = "";
            groupNum = -1;
            buttonType = "PushButton";
            bitmap = "platform/client/buttons/clear";
            helpTag = 0;
            drawText = 0;
        };
        %this.hiddenButton.add(%this.container);
    }
    %this.update();
};
function TabControl::getInitialButtonOffset(%this) {
    if ((%this.tabsOffset $= "")) {
        %this.tabsOffset = "0 1";
    }
    %ret = %this.tabsOffset;
    %dimAlign = (%this.orientation $= "vertical") ? 1 : 0;
    if ((%this.tabsAlign $= "far")) {
        %basePosition = getWord(%ret, %dimAlign);
        %buttonSize = getWord(%this.buttonSize, %dimAlign);
        %separatorSize = getWord(%this.separatorSize, %dimAlign);
        %containerSize = getWord(%this.container.getExtent(), %dimAlign);
        %entireSize = ((%buttonSize + %separatorSize) * %this.numTabs);
        %spareSize = (%containerSize - %entireSize);
        %val = (getWord(%ret, %dimAlign) + %spareSize);
        %ret = setWord(%ret, %dimAlign, %val);
    }
    return %ret;
};
function TabControl::getPadding(%this) {
    return 2;
};
function TabControl::onHiddenButton(%this) {
    %this.overrideLockedOpen = 1;
};
function TabControl::setOrientation(%this, %orientation) {
    %this.orientation = %orientation;
    %this.update();
};
function TabControl::calculateTabDims(%this) {
    if ((%this.orientation $= "vertical")) {
        %this.tabWidth = ((getWord(%this.container.getExtent(), 0) - getWord(%this.buttonSize, 0)) - getWord(%this.separatorSize, 0));
        %this.tabHeight = getWord(%this.container.getExtent(), 1);
        %this.tabPosition = (getWord(%this.buttonSize, 0) + getWord(%this.separatorSize, 0)) @ " " @ 0;
    }
    %this.tabWidth = getWord(%this.container.getExtent(), 0);
    %this.tabHeight = ((getWord(%this.container.getExtent(), 1) - getWord(%this.buttonSize, 1)) - getWord(%this.separatorSize, 1));
    %this.tabPosition = 0 @ " " @ (getWord(%this.buttonSize, 1) + getWord(%this.separatorSize, 1));
};
function TabControl::drawSeparator(%this) {
    %this.separator = new GuiBitmapCtrl("") {
        profile = 0 @ "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = 0 @ " " @ getWord(%this.buttonSize, 1);
        extent = %this.tabWidth @ " " @ getWord(%this.separatorSize, 1);
        minExtent = "0 0";
        sluggishness = -(1.0);
        visible = 1;
        bitmap = %this.separatorBitmap;
    };
    %this.separator.add(%this.container);
};
function TabControl::setTabAtIndexVisible(%this, %tabIndex, %visible) {
    %tab = %this.tabs;
    %tabIndex;
    if (isObject(%tab)) {
        %visible.setVisible(%tab);
    }
};
function TabControl::setTabWithNameVisible(%this, %name, %visible) {
    %idx = %name.getTabIndexWithName(%this);
    if ((%idx >= 0.0)) {
        %idx.setTabAtIndexVisible(%this);
    }
};
function TabControl::pulseTab(%this, %tabObject) {
};
function TabControl::selectTabAtIndex(%this, %tabIndex) {
    if (%this.currentTabIsLockedOpen()) {
        %tabIndex.getTabAtIndex(%this).pulseTab(%this);
        return;
    }
    %this.upcomingTabIndex = %tabIndex;
    %tabIndex.showTabAtIndex(%this);
    if ((%this.currentTabIndex >= 0.0)) {
    }
    if ((%this.currentTabIndex < %this.numTabs)) {
    }
    if ((%this.currentTabIndex != %tabIndex)) {
        0.setTabAtIndexVisible(%this, %this.currentTabIndex);
        if (%this.hasButtons) {
            1.setActive(%this.currentTabIndex, %this.buttons);
        }
    }
    %this.prevTabIndex = %this.currentTabIndex;
    %this.currentTabIndex = %tabIndex;
    if ((%tabIndex >= 0.0)) {
    }
    if ((%tabIndex < %this.numTabs)) {
        1.setTabAtIndexVisible(%this, %tabIndex);
        if (%this.hasButtons) {
            0.setActive(%tabIndex, %this.buttons);
        }
        %this.tabs.tabSelected(%this, %tabIndex);
    }
    0.tabSelected(%this);
    %this.update();
};
function TabControl::tabSelected(%this, %tab) {
};
function TabControl::selectCurrentTab(%this) {
    %this.currentTabIndex.selectTabAtIndex(%this);
};
function TabControl::selectTabWithName(%this, %name) {
    %idx = 0;
    while ((%idx < %this.numTabs)) {
        if ((%idx @ " " @ %this.tabs.name $= %name)) {
            %idx.selectTabAtIndex(%this);
            return;
        }
        %idx = (%idx + 1.0);
    }
};
function TabControl::manuallySelectTab(%this, %tabId) {
    %this.overrideLockedOpen = 1;
    %tabId.selectTab(%this);
};
function TabControl::selectTab(%this, %tabId) {
    %idx = 0;
    while ((%idx < %this.numTabs)) {
        if ((%this.tabs == %tabId @ %idx)) {
            %idx.selectTabAtIndex(%this);
            return;
        }
        %idx = (%idx + 1.0);
    }
};
function TabControl::getTabIndexWithName(%this, %name) {
    %idx = 0;
    while ((%idx < %this.numTabs)) {
        if ((%idx @ " " @ %this.tabs.name $= %name)) {
            return %idx;
        }
        %idx = (%idx + 1.0);
    }
    return -(1.0);
};
function TabControl::getTabIndex(%this, %tabObject) {
    %idx = 0;
    while ((%idx < %this.numTabs)) {
        if ((%this.tabs.getId() == %tabObject.getId() @ %idx)) {
            return %idx;
        }
        %idx = (%idx + 1.0);
    }
    return -(1.0);
};
function TabControl::getTabWithName(%this, %name) {
    %idx = %name.getTabIndexWithName(%this);
    if ((%idx < 0.0)) {
        return 0;
    }
    return %this.tabs;
};
function TabControl::getTabAtIndex(%this, %idx) {
    if ((%idx < 0.0)) {
        return "";
    }
    return %this.tabs;
};
function TabControl::getCurrentTab(%this) {
    if ((%this.numTabs > 0.0)) {
        return %this.tabs;
    }
    return 0;
};
function TabControl::getUpcomingTab(%this) {
    if ((%this.numTabs > 0.0)) {
        return %this.tabs;
    }
    return 0;
};
function TabControl::getPreviousTab(%this) {
    if ((%this.numTabs > 0.0)) {
        return %this.tabs;
    }
    return 0;
};
function TabControl::removeTabAtIndex(%this, %tabIndex) {
    if ((%tabIndex >= 0.0)) {
    }
    if ((%tabIndex < %this.numTabs)) {
        0.setVisible(%tabIndex, %this.tabs);
        %this.tabs.delete(%tabIndex);
        if (%this.hasButtons) {
            0.setVisible(%tabIndex, %this.buttons);
            %this.buttons.delete(%tabIndex);
        }
        %this.numTabs = (%this.numTabs - 1.0);
        %t = %tabIndex;
        while ((%t < %this.numTabs)) {
            %this.tabs = (%t + 1.0) @ %this.tabs @ %t;
            if (%this.hasButtons) {
                %this.buttons = (%t + 1.0) @ %this.buttons @ %t;
            }
            %t = (%t + 1.0);
        }
        %this.tabs = (%t < %this.numTabs) @ 0 @ %this.numTabs;
        if (%this.hasButtons) {
            %this.buttons = 0 @ %this.numTabs;
        }
        if ((%this.numTabs == 0.0)) {
            %this.currentTabIndex = -(1.0);
        }
        if ((%this.currentTabIndex >= %this.numTabs)) {
            (%this.numTabs - 1.0).selectTabAtIndex(%this);
        }
        if ((%this.currentTabIndex > %tabIndex)) {
            (%this.currentTabIndex - 1.0).selectTabAtIndex(%this);
        }
        if ((%this.currentTabIndex == %tabIndex)) {
            %this.selectCurrentTab();
        }
        %this.update();
    }
};
function TabControl::currentTabIsLockedOpen(%this) {
    if (%this.overrideLockedOpen) {
        return 0;
    }
    %curTab = %this.getCurrentTab();
    if (!(isObject(%curTab))) {
        return 0;
    }
    if (!(isObject(%curTab.button))) {
    }
    if (!(%curTab.button.isVisible())) {
        return 0;
    }
    return %curTab.locksOpen;
};
function TabControl::hideOrShowTab(%this, %tabObject, %show) {
    if (!(isObject(%tabObject))) {
        return;
    }
    if (%this.currentTabIsLockedOpen()) {
        %this.overrideLockedOpen = 0;
        return;
    }
    %this.overrideLockedOpen = 0;
    %button = %tabObject.button;
    if (isObject(%button)) {
    }
    if ((%button.isVisible() != %show)) {
        %show.setVisible(%button);
        %this.update();
    }
    %show.onShowOrHideTab(%this, %tabObject);
};
function TabControl::onShowOrHideTab(%this, %tabObject, %show) {
};
function TabControl::hideTabAtIndex(%this, %idx) {
    %tab = %this.tabs;
    %idx;
    0.hideOrShowTab(%this, %tab);
};
function TabControl::showTabAtIndex(%this, %idx) {
    if ((%idx < 0.0)) {
        return;
    }
    %tab = %this.tabs;
    %idx;
    1.hideOrShowTab(%this, %tab);
};
function TabControl::hideTabWithName(%this, %name) {
    %tab = %name.getTabWithName(%this);
    0.hideOrShowTab(%this, %tab);
};
function TabControl::showTabWithName(%this, %name) {
    %tab = %name.getTabWithName(%this);
    1.hideOrShowTab(%this, %tab);
};
function TabControl::update(%this) {
    %this.buttonOffset = %this.getInitialButtonOffset();
    %xoffset = getWord(%this.buttonOffset, 0);
    %yoffset = getWord(%this.buttonOffset, 1);
    if (%this.hasButtons) {
        %idx = 0;
        while ((%idx < %this.numTabs)) {
            if (%this.buttons.isVisible(%idx)) {
                %yoffset.reposition(%idx, %this.buttons, %xoffset);
                if ((%idx == %this.currentTabIndex)) {
                    %yoffset.reposition(%this.hiddenButton, %xoffset);
                    %this.hiddenButton.tooltip = %idx @ %this.buttons.tooltip;
                }
                if ((%this.orientation $= "vertical")) {
                    %yoffset = (%yoffset + (getWord(%this.buttons.extent, 1) + %this.getPadding() @ %idx));
                }
                %xoffset = (%xoffset + (getWord(%this.buttons.extent, 0) + %this.getPadding() @ %idx));
            }
            %idx = (%idx + 1.0);
        }
        %this.visibleTabsWidth = (%idx < %this.numTabs) @ ((%xoffset - %this.getPadding()) - getWord(%this.buttonOffset, 0));
        (%this.currentTabIndex >= 0.0).setVisible(%this.hiddenButton);
    }
    if ((%this.numTabs > 0.0)) {
        %curTab = %this.getCurrentTab();
        if (%curTab) {
            %this.calculateTabDims();
            %trgPos = %curTab.getTrgPosition();
            %this.tabHeight.resize(%curTab, %this.tabWidth);
            getWord(%trgPos, 1).setTrgPosition(%curTab, getWord(%trgPos, 0));
            if (%this.hasButtons) {
                %idx = 0;
                while ((%idx < %this.numTabs)) {
                    %this.buttons.pushToBack(%this.container, %idx);
                    %idx = (%idx + 1.0);
                }
                %this.hiddenButton.pushToBack(%this.container);
            }
        }
    }
};
function TabControl::CreateTab(%this, %name) {
    return new GuiControl("") {
        profile = 0 @ "ETSTabProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = %this.tabPosition;
        extent = %this.tabWidth @ " " @ %this.tabHeight;
        minExtent = "2 2";
        visible = 0;
        name = %name;
        autoHide = 1;
        locksOpen = 0;
        initialized = 0;
    };;
};
function TabControl::createButton(%this, %bitmapName, %tab, %name) {
    %horizSizing = "right";
    %vertSizing = "bottom";
    if ((%this.tabsAlign $= "far")) {
        if ((%this.orientation $= "vertical")) {
            %vertSizing = "top";
        }
        %horizSizing = "left";
    }
    return new GuiBitmapButtonCtrl("") {
        profile = 0 @ "GuiClickLabelProfile";
        horizSizing = %horizSizing;
        vertSizing = %vertSizing;
        position = "0 0";
        extent = %this.buttonSize;
        minExtent = "1 1";
        visible = 1;
        command = %this.getId() @ ".manuallySelectTab(" @ %tab.getId() @ ");";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = %bitmapName;
        helpTag = 0;
        drawText = 1;
    };;
};
function TabControl::newTab(%this, %name, %bitmapName, %optionalToolTip) {
    %tab = %name.getTabWithName(%this);
    if (%tab) {
        return %tab;
    }
    if ((%this.numTabs >= %this.maxTabs)) {
        return 0;
    }
    %tab = %name.CreateTab(%this);
    %button = 0;
    if (%this.hasButtons) {
        %button = %name.createButton(%this, %bitmapName, %tab);
        if (isDefined("%optionalToolTip")) {
        }
        if (!(%optionalToolTip $= "")) {
            %button.tooltip = %optionalToolTip;
        }
    }
    %tab.button = %button;
    %this.tabs = %tab @ %this.numTabs;
    %tab.add(%this.container);
    %this.buttons = %button @ %this.numTabs;
    if ((%button != 0.0)) {
        %button.add(%this.container);
    }
    %this.numTabs = (%this.numTabs + 1.0);
    if ((%this.numTabs == 1.0)) {
        0.selectTabAtIndex(%this);
    }
    %this.update();
    return %tab;
};
