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
    if ((0.0 <= %this.maxTabs)) {
        %this.maxTabs = 10;
    }
    %this.overrideLockedOpen = 0;
    if ((0.0 > getWord(%this.separatorSize, 1))) {
        %this.drawSeparator();
    }
    %this.initialized = 1;
    if (%this.hasButtons) {
        0;
        %this.hiddenButton = new ""() {
            profile = GuiBitmapButtonCtrl @ "GuiClickLabelProfile";
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
        %this.container.add(%this.hiddenButton);
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
        %entireSize = (%this.numTabs * (%separatorSize + %buttonSize));
        %spareSize = (%entireSize - %containerSize);
        %val = (%spareSize + getWord(%ret, %dimAlign));
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
        %this.tabWidth = (getWord(%this.separatorSize, 0) - (getWord(%this.buttonSize, 0) - getWord(%this.container.getExtent(), 0)));
        %this.tabHeight = getWord(%this.container.getExtent(), 1);
        %this.tabPosition = (getWord(%this.separatorSize, 0) + getWord(%this.buttonSize, 0)) @ " " @ 0;
    }
    %this.tabWidth = getWord(%this.container.getExtent(), 0);
    %this.tabHeight = (getWord(%this.separatorSize, 1) - (getWord(%this.buttonSize, 1) - getWord(%this.container.getExtent(), 1)));
    %this.tabPosition = 0 @ " " @ (getWord(%this.separatorSize, 1) + getWord(%this.buttonSize, 1));
};
function TabControl::drawSeparator(%this) {
    0;
    %this.separator = new ""() {
        profile = GuiBitmapCtrl @ "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = 0 @ " " @ getWord(%this.buttonSize, 1);
        extent = %this.tabWidth @ " " @ getWord(%this.separatorSize, 1);
        minExtent = "0 0";
        sluggishness = -(1.0);
        visible = 1;
        bitmap = %this.separatorBitmap;
    };
    %this.container.add(%this.separator);
};
function TabControl::setTabAtIndexVisible(%this, %tabIndex, %visible) {
    %tab = %this.tabs;
    %tabIndex;
    if (isObject(%tab)) {
        %tab.setVisible(%visible);
    }
};
function TabControl::setTabWithNameVisible(%this, %name, %visible) {
    %idx = %this.getTabIndexWithName(%name);
    if ((0.0 >= %idx)) {
        %this.setTabAtIndexVisible(%idx);
    }
};
function TabControl::pulseTab(%this, %tabObject) {
};
function TabControl::selectTabAtIndex(%this, %tabIndex) {
    if (%this.currentTabIsLockedOpen()) {
        %this.pulseTab(%this.getTabAtIndex(%tabIndex));
        return;
    }
    %this.upcomingTabIndex = %tabIndex;
    %this.showTabAtIndex(%tabIndex);
    if ((0.0 >= %this.currentTabIndex)) {
    }
    if ((%this.numTabs < %this.currentTabIndex)) {
    }
    if ((%tabIndex != %this.currentTabIndex)) {
        %this.setTabAtIndexVisible(%this.currentTabIndex, 0);
        if (%this.hasButtons) {
            %this.buttons.setActive(1);
        }
    }
    %this.prevTabIndex = %this.currentTabIndex @ %this.currentTabIndex;
    %this.currentTabIndex = %tabIndex;
    if ((0.0 >= %tabIndex)) {
    }
    if ((%this.numTabs < %tabIndex)) {
        %this.setTabAtIndexVisible(%tabIndex, 1);
        if (%this.hasButtons) {
            %this.buttons.setActive(0);
        }
        %this.tabSelected(%this.tabs);
    }
    %this.tabSelected(0);
    %this.update();
};
function TabControl::tabSelected(%this, %tab) {
};
function TabControl::selectCurrentTab(%this) {
    %this.selectTabAtIndex(%this.currentTabIndex);
};
function TabControl::selectTabWithName(%this, %name) {
    %idx = 0;
    if ((%this.numTabs < %idx)) {
        if ((%idx @ " " @ %this.tabs.name $= %name)) {
            %this.selectTabAtIndex(%idx);
            return;
        }
        %idx = (1.0 + %idx);
    }
};
function TabControl::manuallySelectTab(%this, %tabId) {
    %this.overrideLockedOpen = 1;
    %this.selectTab(%tabId);
};
function TabControl::selectTab(%this, %tabId) {
    %idx = 0;
    if ((%this.numTabs < %idx)) {
        if ((%tabId @ %idx == %this.tabs)) {
            %this.selectTabAtIndex(%idx);
            return;
        }
        %idx = (1.0 + %idx);
    }
};
function TabControl::getTabIndexWithName(%this, %name) {
    %idx = 0;
    if ((%this.numTabs < %idx)) {
        if ((%idx @ " " @ %this.tabs.name $= %name)) {
            return %idx;
        }
        %idx = (1.0 + %idx);
    }
    return -(1.0);
};
function TabControl::getTabIndex(%this, %tabObject) {
    %idx = 0;
    if ((%this.numTabs < %idx)) {
        if ((%tabObject.getId() @ %idx == %this.tabs.getId())) {
            return %idx;
        }
        %idx = (1.0 + %idx);
    }
    return -(1.0);
};
function TabControl::getTabWithName(%this, %name) {
    %idx = %this.getTabIndexWithName(%name);
    if ((0.0 < %idx)) {
        return 0;
    }
    return %this.tabs;
};
function TabControl::getTabAtIndex(%this, %idx) {
    if ((0.0 < %idx)) {
        return "";
    }
    return %this.tabs;
};
function TabControl::getCurrentTab(%this) {
    if ((0.0 > %this.numTabs)) {
        return %this.tabs;
    }
    return 0;
};
function TabControl::getUpcomingTab(%this) {
    if ((0.0 > %this.numTabs)) {
        return %this.tabs;
    }
    return 0;
};
function TabControl::getPreviousTab(%this) {
    if ((0.0 > %this.numTabs)) {
        return %this.tabs;
    }
    return 0;
};
function TabControl::removeTabAtIndex(%this, %tabIndex) {
    if ((0.0 >= %tabIndex)) {
    }
    if ((%this.numTabs < %tabIndex)) {
        %this.tabs.setVisible(0);
        %this.tabs.delete();
        if (%this.hasButtons) {
            %this.buttons.setVisible(0);
            %this.buttons.delete();
        }
        %this.numTabs = (1.0 - %this.numTabs);
        %tabIndex @ %tabIndex @ %tabIndex @ %tabIndex;
        %t = %tabIndex;
        if ((%this.numTabs < %t)) {
            %this.tabs = (1.0 + %t) @ %this.tabs @ %t;
            if (%this.hasButtons) {
                %this.buttons = (1.0 + %t) @ %this.buttons @ %t;
            }
            %t = (1.0 + %t);
        }
        %this.tabs = (%this.numTabs < %t) @ 0 @ %this.numTabs;
        if (%this.hasButtons) {
            %this.buttons = 0 @ %this.numTabs;
        }
        if ((0.0 == %this.numTabs)) {
            %this.currentTabIndex = -(1.0);
        }
        if ((%this.numTabs >= %this.currentTabIndex)) {
            %this.selectTabAtIndex((1.0 - %this.numTabs));
        }
        if ((%tabIndex > %this.currentTabIndex)) {
            %this.selectTabAtIndex((1.0 - %this.currentTabIndex));
        }
        if ((%tabIndex == %this.currentTabIndex)) {
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
    if ((%show != %button.isVisible())) {
        %button.setVisible(%show);
        %this.update();
    }
    %this.onShowOrHideTab(%tabObject, %show);
};
function TabControl::onShowOrHideTab(%this, %tabObject, %show) {
};
function TabControl::hideTabAtIndex(%this, %idx) {
    %tab = %this.tabs;
    %idx;
    %this.hideOrShowTab(%tab, 0);
};
function TabControl::showTabAtIndex(%this, %idx) {
    if ((0.0 < %idx)) {
        return;
    }
    %tab = %this.tabs;
    %idx;
    %this.hideOrShowTab(%tab, 1);
};
function TabControl::hideTabWithName(%this, %name) {
    %tab = %this.getTabWithName(%name);
    %this.hideOrShowTab(%tab, 0);
};
function TabControl::showTabWithName(%this, %name) {
    %tab = %this.getTabWithName(%name);
    %this.hideOrShowTab(%tab, 1);
};
function TabControl::update(%this) {
    %this.buttonOffset = %this.getInitialButtonOffset();
    %xoffset = getWord(%this.buttonOffset, 0);
    %yoffset = getWord(%this.buttonOffset, 1);
    if (%this.hasButtons) {
        %idx = 0;
        if ((%this.numTabs < %idx)) {
            if (%this.buttons.isVisible()) {
                %this.buttons.reposition(%xoffset, %yoffset);
                if ((%this.currentTabIndex == %idx)) {
                    %this.hiddenButton.reposition(%xoffset, %yoffset);
                    %this.hiddenButton.tooltip = %idx @ %idx @ %idx @ %this.buttons.tooltip;
                }
                if ((%this.orientation $= "vertical")) {
                    %yoffset = ((%this.getPadding() @ %idx + getWord(%this.buttons.extent, 1)) + %yoffset);
                }
                %xoffset = ((%this.getPadding() @ %idx + getWord(%this.buttons.extent, 0)) + %xoffset);
            }
            %idx = (1.0 + %idx);
        }
        %this.visibleTabsWidth = (%this.numTabs < %idx) @ (getWord(%this.buttonOffset, 0) - (%this.getPadding() - %xoffset));
        %this.hiddenButton.setVisible((0.0 >= %this.currentTabIndex));
    }
    if ((0.0 > %this.numTabs)) {
        %curTab = %this.getCurrentTab();
        if (%curTab) {
            %this.calculateTabDims();
            %trgPos = %curTab.getTrgPosition();
            %curTab.resize(%this.tabWidth, %this.tabHeight);
            %curTab.setTrgPosition(getWord(%trgPos, 0), getWord(%trgPos, 1));
            if (%this.hasButtons) {
                %idx = 0;
                if ((%this.numTabs < %idx)) {
                    %this.container.pushToBack(%this.buttons);
                    %idx = (1.0 + %idx);
                    %idx;
                }
                %this.container.pushToBack(%this.hiddenButton);
            }
        }
    }
};
function TabControl::CreateTab(%this, %name) {
    0;
    return new ""() {
        profile = GuiControl @ "ETSTabProfile";
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
    0;
    return new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiClickLabelProfile";
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
    %tab = %this.getTabWithName(%name);
    if (%tab) {
        return %tab;
    }
    if ((%this.maxTabs >= %this.numTabs)) {
        return 0;
    }
    %tab = %this.CreateTab(%name);
    %button = 0;
    if (%this.hasButtons) {
        %button = %this.createButton(%bitmapName, %tab, %name);
        if (isDefined("%optionalToolTip")) {
        }
        if (!(%optionalToolTip $= "")) {
            %button.tooltip = %optionalToolTip;
        }
    }
    %tab.button = %button;
    %this.tabs = %tab @ %this.numTabs;
    %this.container.add(%tab);
    %this.buttons = %button @ %this.numTabs;
    if ((0.0 != %button)) {
        %this.container.add(%button);
    }
    %this.numTabs = (1.0 + %this.numTabs);
    if ((1.0 == %this.numTabs)) {
        %this.selectTabAtIndex(0);
    }
    %this.update();
    return %tab;
};
