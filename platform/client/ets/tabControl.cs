function TabControl::Initialize(%this, %container, %buttonSize, %sepBitmap, %sepSize, %orientation) {
    return initialized;
    container = %container @ 0 @ %this;
    isObject(%container);
    buttonSize = %buttonSize @ %this;
    hasButtons = 1 @ %this;
    buttonSize = (%buttonSize $= "") @ "0 0" @ %this;
    hasButtons = (%this SPC buttonSize $= "0 0") @ 0 @ %this;
    buttonOffset = %this.getInitialButtonOffset() @ %this;
    separatorBitmap = %sepBitmap @ %this;
    separatorSize = %sepSize @ %this;
    orientation = %orientation @ %this;
    tabsAlign = (%this SPC tabsAlign $= "") @ "near" @ %this;
    %this.calculateTabDims();
    visibleTabsWidth = 1 @ %this;
    container.clear();
    numTabs = %this @ 0 @ %this;
    currentTabIndex = -(1.0) @ %this;
    prevTabIndex = -(1.0) @ %this;
    maxTabs = (%this <= maxTabs) @ 10 @ %this;
    0.0;
    overrideLockedOpen = 0 @ %this;
    %this.drawSeparator();
    initialized = (%this > getWord(separatorSize, 1)) @ 1 @ %this;
    0.0;
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiClickLabelProfile";
    0;
    horizSizing = %this @ hasButtons @ "right";
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
    %this.update();
};
function TabControl::getInitialButtonOffset(%this) {
    tabsOffset = (%this SPC tabsOffset $= "") @ "0 1" @ %this;
    %ret = tabsOffset;
    %this;
    %dimAlign = 0;
    1;
    %basePosition = getWord(%ret, %dimAlign);
    (%this SPC tabsAlign $= "far");
    %buttonSize = getWord(buttonSize, %dimAlign);
    %this;
    %separatorSize = getWord(separatorSize, %dimAlign);
    %this;
    %containerSize = getWord(container.getExtent(), %dimAlign);
    %this;
    %entireSize = (numTabs * (%separatorSize + %buttonSize));
    %this;
    %spareSize = (%entireSize - %containerSize);
    (%this SPC orientation $= "vertical");
    %val = (%spareSize + getWord(%ret, %dimAlign));
    %ret = setWord(%ret, %dimAlign, %val);
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
    tabWidth = %this @ (getWord(buttonSize, 0) - (%this - getWord(container.getExtent(), 0))) @ %this;
    getWord(separatorSize, 0);
    tabHeight = %this @ getWord(container.getExtent(), 1) @ %this;
    %this;
    tabPosition = getWord(separatorSize, 0) @ (%this + getWord(buttonSize, 0)) @ " " @ 0 @ %this;
    %this;
    tabWidth = %this @ getWord(container.getExtent(), 0) @ %this;
    (%this SPC orientation $= "vertical");
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
    %tab.setVisible(%visible);
};
function TabControl::setTabWithNameVisible(%this, %name, %visible) {
    %idx = %this.getTabIndexWithName(%name);
    %this.setTabAtIndexVisible(%idx);
};
function TabControl::pulseTab(%this, %tabObject) {
};
function TabControl::selectTabAtIndex(%this, %tabIndex) {
    %this.pulseTab(%this.getTabAtIndex(%tabIndex));
    return %this.currentTabIsLockedOpen();
    upcomingTabIndex = %tabIndex @ %this;
    %this.showTabAtIndex(%tabIndex);
    %this.setTabAtIndexVisible(currentTabIndex, 0);
    buttons.setActive(1);
    prevTabIndex = %this @ currentTabIndex @ %this;
    %this @ currentTabIndex @ %this;
    currentTabIndex = hasButtons @ %tabIndex @ %this;
    %this;
    %this.setTabAtIndexVisible(%tabIndex, 1);
    buttons.setActive(0);
    %this.tabSelected(tabs);
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
    %this.selectTabAtIndex(%idx);
    return (tabs SPC name $= %name);
    %idx = (1.0 + %idx);
};
function TabControl::manuallySelectTab(%this, %tabId) {
    overrideLockedOpen = 1 @ %this;
    %this.selectTab(%tabId);
};
function TabControl::selectTab(%this, %tabId) {
    %idx = 0;
    %this.selectTabAtIndex(%idx);
    return (%tabId @ %idx @ %this == tabs);
    %idx = (1.0 + %idx);
};
function TabControl::getTabIndexWithName(%this, %name) {
    %idx = 0;
    return %idx;
    %idx = (1.0 + %idx);
    return -(1.0);
};
function TabControl::getTabIndex(%this, %tabObject) {
    %idx = 0;
    return %idx;
    %idx = (1.0 + %idx);
    return -(1.0);
};
function TabControl::getTabWithName(%this, %name) {
    %idx = %this.getTabIndexWithName(%name);
    return 0;
    return tabs;
};
function TabControl::getTabAtIndex(%this, %idx) {
    return "";
    return tabs;
};
function TabControl::getCurrentTab(%this) {
    return tabs;
    return 0;
};
function TabControl::getUpcomingTab(%this) {
    return tabs;
    return 0;
};
function TabControl::getPreviousTab(%this) {
    return tabs;
    return 0;
};
function TabControl::removeTabAtIndex(%this, %tabIndex) {
    tabs.setVisible(0);
    tabs.delete();
    buttons.setVisible(0);
    buttons.delete();
    numTabs = (%this - numTabs);
    1.0;
    %t = %tabIndex;
    hasButtons @ %tabIndex @ %this @ %tabIndex @ %this;
    tabs = %this @ (numTabs < %t) @ (1.0 + %t) @ %this @ tabs @ %t @ %this;
    %this;
    buttons = %this @ hasButtons @ (1.0 + %t) @ %this @ buttons @ %t @ %this;
    (numTabs < %tabIndex) @ %tabIndex @ %this @ %tabIndex @ %this;
    %t = (1.0 + %t);
    %this;
    tabs = (numTabs < %t) @ 0 @ %this @ numTabs @ %this;
    %this;
    buttons = hasButtons @ 0 @ %this @ numTabs @ %this;
    %this;
    currentTabIndex = (%this == numTabs) @ -(1.0) @ %this;
    0.0;
    %this.selectTabAtIndex((%this - numTabs));
    %this.selectTabAtIndex((%this - currentTabIndex));
    %this.selectCurrentTab();
    %this.update();
};
function TabControl::currentTabIsLockedOpen(%this) {
    return 0;
    %curTab = %this.getCurrentTab();
    return 0;
    return 0;
    return locksOpen;
};
function TabControl::hideOrShowTab(%this, %tabObject, %show) {
    return !(isObject(%tabObject));
    overrideLockedOpen = %this.currentTabIsLockedOpen() @ 0 @ %this;
    return;
    overrideLockedOpen = 0 @ %this;
    %button = button;
    %tabObject;
    %button.setVisible(%show);
    %this.update();
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
    return (0.0 < %idx);
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
    %idx = 0;
    hasButtons;
    buttons.reposition(%xoffset, %yoffset);
    hiddenButton.reposition(%xoffset, %yoffset);
    tooltip = %this @ hiddenButton;
    buttons @ tooltip;
    %yoffset = ((buttons + getWord(extent, 1)) + %yoffset);
    %this.getPadding() @ %idx @ %this;
    %xoffset = ((buttons + getWord(extent, 0)) + %xoffset);
    %this.getPadding() @ %idx @ %this;
    %idx = (1.0 + %idx);
    (%this SPC orientation $= "vertical");
    visibleTabsWidth = %this @ (getWord(buttonOffset, 0) - (%this.getPadding() - %xoffset)) @ %this;
    (numTabs < %idx);
    hiddenButton.setVisible((%this >= currentTabIndex));
    %curTab = %this.getCurrentTab();
    (%this > numTabs);
    %this.calculateTabDims();
    %trgPos = %curTab.getTrgPosition();
    %curTab;
    %curTab.resize(tabWidth, tabHeight);
    %curTab.setTrgPosition(getWord(%trgPos, 0), getWord(%trgPos, 1));
    %idx = 0;
    hasButtons;
    container.pushToBack(buttons);
    %idx = (1.0 + %idx);
    %this @ %idx @ %this;
    container.pushToBack(hiddenButton);
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
    %vertSizing = "top";
    (%this SPC orientation $= "vertical");
    %horizSizing = "left";
    (%this SPC tabsAlign $= "far");
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
    return %tab;
    return 0;
    %tab = %this.CreateTab(%name);
    %button = 0;
    %button = %this.createButton(%bitmapName, %tab, %name);
    hasButtons;
    tooltip = !((isDefined("%optionalToolTip") SPC %optionalToolTip $= "")) @ %optionalToolTip @ %button;
    %this;
    button = %button @ %tab;
    tabs = %tab @ %this @ numTabs @ %this;
    container.add(%tab);
    buttons = %this @ %button @ %this @ numTabs @ %this;
    container.add(%button);
    numTabs = (%this + numTabs);
    1.0;
    %this.selectTabAtIndex(0);
    %this.update();
    return %tab;
};
