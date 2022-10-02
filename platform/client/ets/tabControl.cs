function TabControl::Initialize(%this, %container, %buttonSize, %sepBitmap, %sepSize, %orientation)
{
    if (%this.initialized)
    {
        return ;
    }
    %this.container = isObject(%container) ? %container : 0;
    %this.buttonSize = %buttonSize;
    %this.hasButtons = 1;
    if (%buttonSize $= "")
    {
        %this.buttonSize = "0 0";
    }
    if (%this.buttonSize $= "0 0")
    {
        %this.hasButtons = 0;
    }
    %this.buttonOffset = %this.getInitialButtonOffset();
    %this.separatorBitmap = %sepBitmap;
    %this.separatorSize = %sepSize;
    %this.orientation = %orientation;
    if (%this.tabsAlign $= "")
    {
        %this.tabsAlign = "near";
    }
    %this.calculateTabDims();
    %this.visibleTabsWidth = 1;
    %this.container.clear();
    %this.numTabs = 0;
    %this.currentTabIndex = -1;
    %this.prevTabIndex = -1;
    if (%this.maxTabs <= 0)
    {
        %this.maxTabs = 10;
    }
    %this.overrideLockedOpen = 0;
    if (getWord(%this.separatorSize, 1) > 0)
    {
        %this.drawSeparator();
    }
    %this.initialized = 1;
    if (%this.hasButtons)
    {
        %this.hiddenButton = new GuiBitmapButtonCtrl()
        {
            profile = "GuiClickLabelProfile";
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
    return ;
}
function TabControl::getInitialButtonOffset(%this)
{
    if (%this.tabsOffset $= "")
    {
        %this.tabsOffset = "0 1";
    }
    %ret = %this.tabsOffset;
    %dimAlign = %this.orientation $= "vertical" ? 1 : 0;
    if (%this.tabsAlign $= "far")
    {
        %basePosition = getWord(%ret, %dimAlign);
        %buttonSize = getWord(%this.buttonSize, %dimAlign);
        %separatorSize = getWord(%this.separatorSize, %dimAlign);
        %containerSize = getWord(%this.container.getExtent(), %dimAlign);
        %entireSize = (%buttonSize + %separatorSize) * %this.numTabs;
        %spareSize = %containerSize - %entireSize;
        %val = getWord(%ret, %dimAlign) + %spareSize;
        %ret = setWord(%ret, %dimAlign, %val);
    }
    return %ret;
}
function TabControl::getPadding(%this)
{
    return 2;
}
function TabControl::onHiddenButton(%this)
{
    %this.overrideLockedOpen = 1;
    return ;
}
function TabControl::setOrientation(%this, %orientation)
{
    %this.orientation = %orientation;
    %this.update();
    return ;
}
function TabControl::calculateTabDims(%this)
{
    if (%this.orientation $= "vertical")
    {
        %this.tabWidth = (getWord(%this.container.getExtent(), 0) - getWord(%this.buttonSize, 0)) - getWord(%this.separatorSize, 0);
        %this.tabHeight = getWord(%this.container.getExtent(), 1);
        %this.tabPosition = getWord(%this.buttonSize, 0) + getWord(%this.separatorSize, 0) SPC 0;
    }
    else
    {
        %this.tabWidth = getWord(%this.container.getExtent(), 0);
        %this.tabHeight = (getWord(%this.container.getExtent(), 1) - getWord(%this.buttonSize, 1)) - getWord(%this.separatorSize, 1);
        %this.tabPosition = 0 SPC getWord(%this.buttonSize, 1) + getWord(%this.separatorSize, 1);
    }
    return ;
}
function TabControl::drawSeparator(%this)
{
    %this.separator = new GuiBitmapCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = 0 SPC getWord(%this.buttonSize, 1);
        extent = %this.tabWidth SPC getWord(%this.separatorSize, 1);
        minExtent = "0 0";
        sluggishness = -1;
        visible = 1;
        bitmap = %this.separatorBitmap;
    };
    %this.container.add(%this.separator);
    return ;
}
function TabControl::setTabAtIndexVisible(%this, %tabIndex, %visible)
{
    %tab = %this.tabs[%tabIndex];
    if (isObject(%tab))
    {
        %tab.setVisible(%visible);
    }
    return ;
}
function TabControl::setTabWithNameVisible(%this, %name, %visible)
{
    %idx = %this.getTabIndexWithName(%name);
    if (%idx >= 0)
    {
        %this.setTabAtIndexVisible(%idx);
    }
    return ;
}
function TabControl::pulseTab(%this, %tabObject)
{
    return ;
}
function TabControl::selectTabAtIndex(%this, %tabIndex)
{
    if (%this.currentTabIsLockedOpen())
    {
        %this.pulseTab(%this.getTabAtIndex(%tabIndex));
        return ;
    }
    %this.upcomingTabIndex = %tabIndex;
    %this.showTabAtIndex(%tabIndex);
    if (((%this.currentTabIndex >= 0) && (%this.currentTabIndex < %this.numTabs)) && (%this.currentTabIndex != %tabIndex))
    {
        %this.setTabAtIndexVisible(%this.currentTabIndex, 0);
        if (%this.hasButtons)
        {
            %this.buttons[%this.currentTabIndex].setActive(1);
        }
    }
    %this.prevTabIndex = %this.currentTabIndex;
    %this.currentTabIndex = %tabIndex;
    if ((%tabIndex >= 0) && (%tabIndex < %this.numTabs))
    {
        %this.setTabAtIndexVisible(%tabIndex, 1);
        if (%this.hasButtons)
        {
            %this.buttons[%tabIndex].setActive(0);
        }
        %this.tabSelected(%this.tabs[%tabIndex]);
    }
    else
    {
        %this.tabSelected(0);
    }
    %this.update();
    return ;
}
function TabControl::tabSelected(%this, %tab)
{
    return ;
}
function TabControl::selectCurrentTab(%this)
{
    %this.selectTabAtIndex(%this.currentTabIndex);
    return ;
}
function TabControl::selectTabWithName(%this, %name)
{
    %idx = 0;
    while (%idx < %this.numTabs)
    {
        if (%this.tabs[%idx].name $= %name)
        {
            %this.selectTabAtIndex(%idx);
            return ;
        }
        %idx = %idx + 1;
    }
}

function TabControl::manuallySelectTab(%this, %tabId)
{
    %this.overrideLockedOpen = 1;
    %this.selectTab(%tabId);
    return ;
}
function TabControl::selectTab(%this, %tabId)
{
    %idx = 0;
    while (%idx < %this.numTabs)
    {
        if (%this.tabs[%idx] == %tabId)
        {
            %this.selectTabAtIndex(%idx);
            return ;
        }
        %idx = %idx + 1;
    }
}

function TabControl::getTabIndexWithName(%this, %name)
{
    %idx = 0;
    while (%idx < %this.numTabs)
    {
        if (%this.tabs[%idx].name $= %name)
        {
            return %idx;
        }
        %idx = %idx + 1;
    }
    return -1;
}
function TabControl::getTabIndex(%this, %tabObject)
{
    %idx = 0;
    while (%idx < %this.numTabs)
    {
        if (%this.tabs[%idx].getId() == %tabObject.getId())
        {
            return %idx;
        }
        %idx = %idx + 1;
    }
    return -1;
}
function TabControl::getTabWithName(%this, %name)
{
    %idx = %this.getTabIndexWithName(%name);
    if (%idx < 0)
    {
        return 0;
    }
    return %this.tabs[%idx];
}
function TabControl::getTabAtIndex(%this, %idx)
{
    if (%idx < 0)
    {
        return "";
    }
    else
    {
        return %this.tabs[%idx];
    }
    return ;
}
function TabControl::getCurrentTab(%this)
{
    if (%this.numTabs > 0)
    {
        return %this.tabs[%this.currentTabIndex];
    }
    else
    {
        return 0;
    }
    return ;
}
function TabControl::getUpcomingTab(%this)
{
    if (%this.numTabs > 0)
    {
        return %this.tabs[%this.upcomingTabIndex];
    }
    else
    {
        return 0;
    }
    return ;
}
function TabControl::getPreviousTab(%this)
{
    if (%this.numTabs > 0)
    {
        return %this.tabs[%this.prevTabIndex];
    }
    else
    {
        return 0;
    }
    return ;
}
function TabControl::removeTabAtIndex(%this, %tabIndex)
{
    if ((%tabIndex >= 0) && (%tabIndex < %this.numTabs))
    {
        %this.tabs[%tabIndex].setVisible(0);
        %this.tabs[%tabIndex].delete();
        if (%this.hasButtons)
        {
            %this.buttons[%tabIndex].setVisible(0);
            %this.buttons[%tabIndex].delete();
        }
        %this.numTabs = %this.numTabs - 1;
        %t = %tabIndex;
        while (%t < %this.numTabs)
        {
            %this.tabs[%t] = %this.tabs[(%t + 1)];
            if (%this.hasButtons)
            {
                %this.buttons[%t] = %this.buttons[(%t + 1)];
            }
            %t = %t + 1;
        }
        %this.tabs[%this.numTabs] = 0;
        if (%this.hasButtons)
        {
            %this.buttons[%this.numTabs] = 0;
        }
        if (%this.numTabs == 0)
        {
            %this.currentTabIndex = -1;
        }
        else
        {
            if (%this.currentTabIndex >= %this.numTabs)
            {
                %this.selectTabAtIndex(%this.numTabs - 1);
            }
            else
            {
                if (%this.currentTabIndex > %tabIndex)
                {
                    %this.selectTabAtIndex(%this.currentTabIndex - 1);
                }
                else
                {
                    if (%this.currentTabIndex == %tabIndex)
                    {
                        %this.selectCurrentTab();
                    }
                }
            }
        }
        %this.update();
    }
    return ;
}
function TabControl::currentTabIsLockedOpen(%this)
{
    if (%this.overrideLockedOpen)
    {
        return 0;
    }
    %curTab = %this.getCurrentTab();
    if (!isObject(%curTab))
    {
        return 0;
    }
    if (!isObject(%curTab.button) && !%curTab.button.isVisible())
    {
        return 0;
    }
    return %curTab.locksOpen;
}
function TabControl::hideOrShowTab(%this, %tabObject, %show)
{
    if (!isObject(%tabObject))
    {
        return ;
    }
    if (%this.currentTabIsLockedOpen())
    {
        %this.overrideLockedOpen = 0;
        return ;
    }
    %this.overrideLockedOpen = 0;
    %button = %tabObject.button;
    if (isObject(%button) && (%button.isVisible() != %show))
    {
        %button.setVisible(%show);
        %this.update();
    }
    %this.onShowOrHideTab(%tabObject, %show);
    return ;
}
function TabControl::onShowOrHideTab(%this, %tabObject, %show)
{
    return ;
}
function TabControl::hideTabAtIndex(%this, %idx)
{
    %tab = %this.tabs[%idx];
    %this.hideOrShowTab(%tab, 0);
    return ;
}
function TabControl::showTabAtIndex(%this, %idx)
{
    if (%idx < 0)
    {
        return ;
    }
    %tab = %this.tabs[%idx];
    %this.hideOrShowTab(%tab, 1);
    return ;
}
function TabControl::hideTabWithName(%this, %name)
{
    %tab = %this.getTabWithName(%name);
    %this.hideOrShowTab(%tab, 0);
    return ;
}
function TabControl::showTabWithName(%this, %name)
{
    %tab = %this.getTabWithName(%name);
    %this.hideOrShowTab(%tab, 1);
    return ;
}
function TabControl::update(%this)
{
    %this.buttonOffset = %this.getInitialButtonOffset();
    %xoffset = getWord(%this.buttonOffset, 0);
    %yoffset = getWord(%this.buttonOffset, 1);
    if (%this.hasButtons)
    {
        %idx = 0;
        while (%idx < %this.numTabs)
        {
            if (%this.buttons[%idx].isVisible())
            {
                %this.buttons[%idx].reposition(%xoffset, %yoffset);
                if (%idx == %this.currentTabIndex)
                {
                    %this.hiddenButton.reposition(%xoffset, %yoffset);
                    %this.hiddenButton.tooltip = %this.buttons[%idx].tooltip;
                }
                if (%this.orientation $= "vertical")
                {
                    %yoffset = %yoffset + (getWord(%this.buttons[%idx].extent, 1) + %this.getPadding());
                }
                else
                {
                    %xoffset = %xoffset + (getWord(%this.buttons[%idx].extent, 0) + %this.getPadding());
                }
            }
            %idx = %idx + 1;
        }
        %this.visibleTabsWidth = (%xoffset - %this.getPadding()) - getWord(%this.buttonOffset, 0);
        %this.hiddenButton.setVisible(%this.currentTabIndex >= 0);
    }
    if (%this.numTabs > 0)
    {
        %curTab = %this.getCurrentTab();
        if (%curTab)
        {
            %this.calculateTabDims();
            %trgPos = %curTab.getTrgPosition();
            %curTab.resize(%this.tabWidth, %this.tabHeight);
            %curTab.setTrgPosition(getWord(%trgPos, 0), getWord(%trgPos, 1));
            if (%this.hasButtons)
            {
                %idx = 0;
                while (%idx < %this.numTabs)
                {
                    %this.container.pushToBack(%this.buttons[%idx]);
                    %idx = %idx + 1;
                }
                %this.container.pushToBack(%this.hiddenButton);
            }
        }
    }
    return ;
}
function TabControl::CreateTab(%this, %name)
{
    return new GuiControl()
    {
        profile = "ETSTabProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = %this.tabPosition;
        extent = %this.tabWidth SPC %this.tabHeight;
        minExtent = "2 2";
        visible = 0;
        name = %name;
        autoHide = 1;
        locksOpen = 0;
        initialized = 0;
    };
    return ;
}
function TabControl::createButton(%this, %bitmapName, %tab, %name)
{
    %horizSizing = "right";
    %vertSizing = "bottom";
    if (%this.tabsAlign $= "far")
    {
        if (%this.orientation $= "vertical")
        {
            %vertSizing = "top";
        }
        else
        {
            %horizSizing = "left";
        }
    }
    return new GuiBitmapButtonCtrl()
    {
        profile = "GuiClickLabelProfile";
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
    };
    return ;
}
function TabControl::newTab(%this, %name, %bitmapName, %optionalToolTip)
{
    %tab = %this.getTabWithName(%name);
    if (%tab)
    {
        return %tab;
    }
    if (%this.numTabs >= %this.maxTabs)
    {
        return 0;
    }
    %tab = %this.CreateTab(%name);
    %button = 0;
    if (%this.hasButtons)
    {
        %button = %this.createButton(%bitmapName, %tab, %name);
        if (isDefined("%optionalToolTip") && !((%optionalToolTip $= "")))
        {
            %button.tooltip = %optionalToolTip;
        }
    }
    %tab.button = %button;
    %this.tabs[%this.numTabs] = %tab;
    %this.container.add(%tab);
    %this.buttons[%this.numTabs] = %button;
    if (%button != 0)
    {
        %this.container.add(%button);
    }
    %this.numTabs = %this.numTabs + 1;
    if (%this.numTabs == 1)
    {
        %this.selectTabAtIndex(0);
    }
    else
    {
        %this.update();
    }
    return %tab;
}
