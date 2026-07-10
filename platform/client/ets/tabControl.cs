function TabControl::Initialize(%this, %container, %buttonSize, %sepBitmap, %sepSize, %orientation) {
    if (initialized) {
        return %this;
    }
    if (isObject(%container)) {
    }
    container = %container @ 0 @ %this;
    buttonSize = %buttonSize @ %this;
    hasButtons = 1 @ %this;
    if ((%buttonSize $= "")) {
        buttonSize = "0 0" @ %this;
    }
    if ((%this SPC buttonSize $= "0 0")) {
        hasButtons = 0 @ %this;
    }
    buttonOffset = %this.getInitialButtonOffset() @ %this;
    separatorBitmap = %sepBitmap @ %this;
    separatorSize = %sepSize @ %this;
    orientation = %orientation @ %this;
    if ((%this SPC tabsAlign $= "")) {
        tabsAlign = "near" @ %this;
    }
    %this.calculateTabDims();
    visibleTabsWidth = 1 @ %this;
    container.clear();
    numTabs = %this @ 0 @ %this;
    currentTabIndex = -(1.0) @ %this;
    prevTabIndex = -(1.0) @ %this;
    if ((%this <= maxTabs)) {
        maxTabs = 0.0 @ 10 @ %this;
    }
    overrideLockedOpen = 0 @ %this;
    if ((%this > getWord(separatorSize, 1))) {
        %this.drawSeparator();
    }
    initialized = 0.0 @ 1 @ %this;
    if (hasButtons) {
        profile = GuiBitmapButtonCtrl @ new ""() @ "GuiClickLabelProfile";
        0;
        horizSizing = %this @ "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = %this @ buttonSize;
        minExtent = "1 1";
        visible = 1;
        command = %this.getId() @ ".onHiddenButton();";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "platform/client/buttons/clear";
        helpTag = 0;
        drawText = 0;
        hiddenButton = %this;
        container.add(hiddenButton);
    }
    %this.update();
};
function TabControl::getInitialButtonOffset(%this) {
    if ((%this SPC tabsOffset $= "")) {
        tabsOffset = "0 1" @ %this;
    }
    %ret = tabsOffset;
    %this;
    %dimAlign = (%this SPC orientation $= "vertical") ? 1 : 0;
    if ((%this SPC tabsAlign $= "far")) {
        %basePosition = getWord(%ret, %dimAlign);
        %buttonSize = getWord(buttonSize, %dimAlign);
        %this;
        %separatorSize = getWord(separatorSize, %dimAlign);
        %this;
        %containerSize = getWord(container.getExtent(), %dimAlign);
        %this;
        %entireSize = (numTabs * (%separatorSize + %buttonSize));
        %this;
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
    overrideLockedOpen = 1 @ %this;
};
function TabControl::setOrientation(%this, %orientation) {
    orientation = %orientation @ %this;
    %this.update();
};
function TabControl::calculateTabDims(%this) {
    if ((%this SPC orientation $= "vertical")) {
        tabWidth = %this @ (getWord(buttonSize, 0) - (%this - getWord(container.getExtent(), 0))) @ %this;
        getWord(separatorSize, 0);
        tabHeight = %this @ getWord(container.getExtent(), 1) @ %this;
        %this;
        tabPosition = getWord(separatorSize, 0) @ (%this + getWord(buttonSize, 0)) @ " " @ 0 @ %this;
        %this;
    }
    tabWidth = %this @ getWord(container.getExtent(), 0) @ %this;
    tabHeight = %this @ (getWord(buttonSize, 1) - (%this - getWord(container.getExtent(), 1))) @ %this;
    getWord(separatorSize, 1);
    tabPosition = %this @ getWord(separatorSize, 1) @ (%this + getWord(buttonSize, 1)) @ %this;
    0 @ " ";
};
function TabControl::drawSeparator(%this) {
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "width";
    vertSizing = "bottom";
    position = 0 @ " " @ %this @ getWord(buttonSize, 1);
    extent = %this @ tabWidth @ " " @ %this @ getWord(separatorSize, 1);
    minExtent = "0 0";
    sluggishness = -(1.0);
    visible = 1;
    bitmap = %this @ separatorBitmap;
    separator = %this;
    container.add(separator);
};
function TabControl::setTabAtIndexVisible(%this, %tabIndex, %visible) {
    %tab = tabs;
    %tabIndex @ %this;
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
    upcomingTabIndex = %tabIndex @ %this;
    %this.showTabAtIndex(%tabIndex);
    if ((%this >= currentTabIndex)) {
    }
    if ((%this < currentTabIndex)) {
    }
    if ((%this != currentTabIndex)) {
        %this.setTabAtIndexVisible(currentTabIndex, 0);
        if (hasButtons) {
            buttons.setActive(1);
        }
    }
    prevTabIndex = %this @ currentTabIndex @ %this;
    %this @ currentTabIndex @ %this;
    currentTabIndex = %this @ %tabIndex @ %this;
    %this;
    if ((0.0 >= %tabIndex)) {
    }
    if ((numTabs < %tabIndex)) {
        %this.setTabAtIndexVisible(%tabIndex, 1);
        if (hasButtons) {
            buttons.setActive(0);
        }
        %this.tabSelected(tabs);
    }
    %this.tabSelected(0);
    %this.update();
};
function TabControl::tabSelected(%this, %tab) {
};
function TabControl::selectCurrentTab(%this) {
    %this.selectTabAtIndex(currentTabIndex);
};
function TabControl::selectTabWithName(%this, %name) {
    %idx = 0;
    if ((numTabs < %idx)) {
        if ((tabs SPC name $= %name)) {
            %this.selectTabAtIndex(%idx);
            return %this @ %idx @ %this;
        }
        %idx = (1.0 + %idx);
    }
};
function TabControl::manuallySelectTab(%this, %tabId) {
    overrideLockedOpen = 1 @ %this;
    %this.selectTab(%tabId);
};
function TabControl::selectTab(%this, %tabId) {
    %idx = 0;
    if ((numTabs < %idx)) {
        if ((%tabId @ %idx @ %this == tabs)) {
            %this.selectTabAtIndex(%idx);
            return %this;
        }
        %idx = (1.0 + %idx);
    }
};
function TabControl::getTabIndexWithName(%this, %name) {
    %idx = 0;
    if ((numTabs < %idx)) {
        if ((tabs SPC name $= %name)) {
            return %idx;
        }
        %idx = (1.0 + %idx);
    }
    return -(1.0);
};
function TabControl::getTabIndex(%this, %tabObject) {
    %idx = 0;
    if ((numTabs < %idx)) {
        if ((%tabObject.getId() @ %idx @ %this == tabs.getId())) {
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
    return tabs;
};
function TabControl::getTabAtIndex(%this, %idx) {
    if ((0.0 < %idx)) {
        return "";
    }
    return tabs;
};
function TabControl::getCurrentTab(%this) {
    if ((%this > numTabs)) {
        return tabs;
    }
    return 0;
};
function TabControl::getUpcomingTab(%this) {
    if ((%this > numTabs)) {
        return tabs;
    }
    return 0;
};
function TabControl::getPreviousTab(%this) {
    if ((%this > numTabs)) {
        return tabs;
    }
    return 0;
};
function TabControl::removeTabAtIndex(%this, %tabIndex) {
    if ((0.0 >= %tabIndex)) {
    }
    if ((numTabs < %tabIndex)) {
        tabs.setVisible(0);
        tabs.delete();
        if (hasButtons) {
            buttons.setVisible(0);
            buttons.delete();
        }
        numTabs = (%this - numTabs);
        1.0;
        %t = %tabIndex;
        %this @ %tabIndex @ %this @ %tabIndex @ %this;
        if ((numTabs < %t)) {
            tabs = %this @ %tabIndex @ %this @ %tabIndex @ %this @ %this @ (1.0 + %t) @ %this @ tabs @ %t @ %this;
            if (hasButtons) {
                buttons = %this @ (1.0 + %t) @ %this @ buttons @ %t @ %this;
            }
            %t = (1.0 + %t);
        }
        tabs = (numTabs < %t) @ 0 @ %this @ numTabs @ %this;
        %this;
        if (hasButtons) {
            buttons = %this @ 0 @ %this @ numTabs @ %this;
        }
        if ((%this == numTabs)) {
            currentTabIndex = 0.0 @ -(1.0) @ %this;
        }
        if ((%this >= currentTabIndex)) {
            %this.selectTabAtIndex((%this - numTabs));
        }
        if ((%this > currentTabIndex)) {
            %this.selectTabAtIndex((%this - currentTabIndex));
        }
        if ((%this == currentTabIndex)) {
            %this.selectCurrentTab();
        }
        %this.update();
    }
};
function TabControl::currentTabIsLockedOpen(%this) {
    if (overrideLockedOpen) {
        return 0;
    }
    %curTab = %this.getCurrentTab();
    if (!(isObject(%curTab))) {
        return 0;
    }
    if (!(isObject(button))) {
    }
    if (!(button.isVisible())) {
        return 0;
    }
    return locksOpen;
};
function TabControl::hideOrShowTab(%this, %tabObject, %show) {
    if (!(isObject(%tabObject))) {
        return;
    }
    if (%this.currentTabIsLockedOpen()) {
        overrideLockedOpen = 0 @ %this;
        return;
    }
    overrideLockedOpen = 0 @ %this;
    %button = button;
    %tabObject;
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
    %tab = tabs;
    %idx @ %this;
    %this.hideOrShowTab(%tab, 0);
};
function TabControl::showTabAtIndex(%this, %idx) {
    if ((0.0 < %idx)) {
        return;
    }
    %tab = tabs;
    %idx @ %this;
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
    buttonOffset = %this.getInitialButtonOffset() @ %this;
    %xoffset = getWord(buttonOffset, 0);
    %this;
    %yoffset = getWord(buttonOffset, 1);
    %this;
    if (hasButtons) {
        %idx = 0;
        %this;
        if ((numTabs < %idx)) {
            if (buttons.isVisible()) {
                buttons.reposition(%xoffset, %yoffset);
                if ((currentTabIndex == %idx)) {
                    hiddenButton.reposition(%xoffset, %yoffset);
                    tooltip = %this @ hiddenButton;
                    buttons @ tooltip;
                }
                if ((%this SPC orientation $= "vertical")) {
                    %yoffset = ((buttons + getWord(extent, 1)) + %yoffset);
                    %this.getPadding() @ %idx @ %this;
                }
                %xoffset = ((buttons + getWord(extent, 0)) + %xoffset);
                %this.getPadding() @ %idx @ %this;
            }
            %idx = (1.0 + %idx);
            %this @ %idx @ %this;
        }
        visibleTabsWidth = %this @ (getWord(buttonOffset, 0) - (%this.getPadding() - %xoffset)) @ %this;
        (numTabs < %idx);
        hiddenButton.setVisible((%this >= currentTabIndex));
    }
    if ((%this > numTabs)) {
        %curTab = %this.getCurrentTab();
        0.0;
        if (%curTab) {
            %this.calculateTabDims();
            %trgPos = %curTab.getTrgPosition();
            0.0;
            %curTab.resize(tabWidth, tabHeight);
            %curTab.setTrgPosition(getWord(%trgPos, 0), getWord(%trgPos, 1));
            if (hasButtons) {
                %idx = 0;
                %this;
                if ((numTabs < %idx)) {
                    container.pushToBack(buttons);
                    %idx = (1.0 + %idx);
                    %this @ %idx @ %this;
                }
                container.pushToBack(hiddenButton);
            }
        }
    }
};
function TabControl::CreateTab(%this, %name) {
    profile = GuiControl @ new ""() @ "ETSTabProfile";
    0;
    horizSizing = "width";
    vertSizing = "height";
    position = %this @ tabPosition;
    extent = %this @ tabWidth @ " " @ %this @ tabHeight;
    minExtent = "2 2";
    visible = 0;
    name = %name;
    autoHide = 1;
    locksOpen = 0;
    initialized = 0;
    return;
};
function TabControl::createButton(%this, %bitmapName, %tab, %name) {
    %horizSizing = "right";
    %vertSizing = "bottom";
    if ((%this SPC tabsAlign $= "far")) {
        if ((%this SPC orientation $= "vertical")) {
            %vertSizing = "top";
        }
        %horizSizing = "left";
    }
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiClickLabelProfile";
    0;
    horizSizing = %horizSizing;
    vertSizing = %vertSizing;
    position = "0 0";
    extent = %this @ buttonSize;
    minExtent = "1 1";
    visible = 1;
    command = %this.getId() @ ".manuallySelectTab(" @ %tab.getId() @ ");";
    text = "";
    groupNum = -1;
    buttonType = "PushButton";
    bitmap = %bitmapName;
    helpTag = 0;
    drawText = 1;
    return;
};
function TabControl::newTab(%this, %name, %bitmapName, %optionalToolTip) {
    %tab = %this.getTabWithName(%name);
    if (%tab) {
        return %tab;
    }
    if ((%this >= numTabs)) {
        return 0;
    }
    %tab = %this.CreateTab(%name);
    %button = 0;
    if (hasButtons) {
        %button = %this.createButton(%bitmapName, %tab, %name);
        %this;
        if (isDefined("%optionalToolTip")) {
        }
        if (!(%optionalToolTip $= "")) {
            tooltip = %optionalToolTip @ %button;
        }
    }
    button = %button @ %tab;
    tabs = %tab @ %this @ numTabs @ %this;
    container.add(%tab);
    buttons = %this @ %button @ %this @ numTabs @ %this;
    if ((0.0 != %button)) {
        container.add(%button);
    }
    numTabs = (%this + numTabs);
    1.0;
    if ((%this == numTabs)) {
        %this.selectTabAtIndex(0);
    }
    %this.update();
    return %tab;
};
