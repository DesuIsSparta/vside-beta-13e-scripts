$doPrintsDebug_WM = 0;
function DEBUG_WM(%text)
{
    if ($doPrintsDebug_WM)
    {
        echo(%text);
    }
    return ;
}
if (!isObject(WindowManager))
{
    new ScriptObject(WindowManager);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(WindowManager);
    }
}
function WindowManager::Initialize(%this)
{
    WindowManager.leftMargin = safeEnsureScriptObject("SimObject", "WindowManagerLeftMargin");
    WindowManager.leftMargin.bottomMargin = 225;
    %n = 0;
    WindowManager.leftMargin.windows[%n] = CSControlPanel;
    %n = %n + 1;
    WindowManager.leftMargin.windows[%n] = CSFurnitureMover;
    %n = %n + 1;
    WindowManager.leftMargin.windows[%n] = CSInventoryBrowserWindow;
    %n = %n + 1;
    WindowManager.leftMargin.windows[%n] = CSShoppingBrowserWindow;
    %n = %n + 1;
    WindowManager.leftMargin.windows[%n] = CSPaintingWindow;
    %n = %n + 1;
    WindowManager.leftMargin.windows[%n] = CSMediaDisplay;
    %n = %n + 1;
    WindowManager.leftMargin.windows[%n] = CSRulesAndDescWindow;
    %n = %n + 1;
    WindowManager.leftMargin.windows[%n] = CSLayoutSelector;
    %n = %n + 1;
    WindowManager.leftMargin.numWindows = %n;
    WindowManager.leftMargin.Padding = 4;
    WindowManager.rightMargin = safeEnsureScriptObject("SimObject", "WindowManagerRightMargin");
    %n = 0;
    WindowManager.rightMargin.windows[%n] = AccountBalanceHud;
    %n = %n + 1;
    WindowManager.rightMargin.windows[%n] = BuddyHudWin;
    %n = %n + 1;
    WindowManager.rightMargin.windows[%n] = EmoteHudWin;
    %n = %n + 1;
    if (isObject(geActivitiesPanel))
    {
        WindowManager.rightMargin.windows[%n] = geActivitiesPanel;
        %n = %n + 1;
    }
    WindowManager.rightMargin.windows[%n] = GameMgrHudWin;
    %n = %n + 1;
    WindowManager.rightMargin.windows[%n] = geLocalMapContainer;
    %n = %n + 1;
    WindowManager.rightMargin.windows[%n] = BottomSpacerRTHudWin;
    %n = %n + 1;
    WindowManager.rightMargin.windows[%n] = DownloadProgressHudWin;
    %n = %n + 1;
    WindowManager.rightMargin.numWindows = %n;
    WindowManager.rightMargin.Padding = 0;
    $WindowManager::Initialized = 1;
    return ;
}
function WindowManager::wakeUp(%this)
{
    if (!$WindowManager::Initialized)
    {
        %this.Initialize();
    }
    return ;
}
function WindowManager::getRightMargin(%this)
{
    return %this.getRightMarginAtY(-1);
}
$gWindowManagerMarginSpecialCasesRight = "";
$gWindowManagerMarginSpecialCasesLeft = "AimConvContainer";
function WindowManager::getRightMarginAtY(%this, %checkAtY)
{
    %position = %windowWidth = getWord(getRes(), 0);
    %n = 0;
    while (%n < %this.rightMargin.numWindows)
    {
        %win = WindowManager.rightMargin.windows[%n];
        %pos = %win.getPosition();
        %posX = getWord(%pos, 0);
        %posY = getWord(%pos, 1);
        if (%checkAtY >= 0)
        {
            %overlap = (%posY <= %checkAtY) && ((%posY + getWord(%win.getExtent(), 1)) >= %checkAtY);
        }
        else
        {
            %overlap = 1;
        }
        if (%win.isVisible() && %overlap)
        {
            %position = mMin(%position, %posX);
        }
        %n = %n + 1;
    }
    %n = getWordCount($gWindowManagerMarginSpecialCasesRight) - 1;
    while (%n >= 0)
    {
        %ctrl = getWord($gWindowManagerMarginSpecialCasesRight, %n);
        if (!isObject(%ctrl))
        {
            return ;
        }
        %pos = %ctrl.getPosition();
        %posX = getWord(%pos, 0);
        %posY = getWord(%pos, 1);
        if (%checkAtY >= 0)
        {
            %overlap = (%posY <= %checkAtY) && ((%posY + getWord(%ctrl.getExtent(), 1)) >= %checkAtY);
        }
        else
        {
            %overlap = 1;
        }
        if (%ctrl.isVisible() && %overlap)
        {
            %position = mMin(%position, %posX);
        }
        %n = %n - 1;
    }
    return %windowWidth - %position;
}
function WindowManager::getLeftMargin(%this)
{
    return %this.getLeftMarginAtY(-1);
}
function WindowManager::getLeftMarginAtY(%this, %checkAtY)
{
    %width = 0;
    %n = 0;
    while (%n < %this.leftMargin.numWindows)
    {
        %win = WindowManager.leftMargin.windows[%n];
        %pos = %win.getPosition();
        %posX = getWord(%pos, 0);
        %posY = getWord(%pos, 1);
        if (%checkAtY >= 0)
        {
            %overlap = (%posY <= %checkAtY) && ((%posY + getWord(%win.getExtent(), 1)) >= %checkAtY);
        }
        else
        {
            %overlap = 1;
        }
        if (%win.isVisible() && %overlap)
        {
            %edge = getWord(%win.getExtent(), 0) + %posX;
            %width = mMax(%width, %edge);
        }
        %n = %n + 1;
    }
    %n = getWordCount($gWindowManagerMarginSpecialCasesLeft) - 1;
    while (%n >= 0)
    {
        %ctrl = getWord($gWindowManagerMarginSpecialCasesLeft, %n);
        if (!isObject(%ctrl))
        {
            return ;
        }
        %pos = %ctrl.getPosition();
        %posX = getWord(%pos, 0);
        %posY = getWord(%pos, 1);
        if (%checkAtY >= 0)
        {
            %overlap = (%posY <= %checkAtY) && ((%posY + getWord(%ctrl.getExtent(), 1)) >= %checkAtY);
        }
        else
        {
            %overlap = 1;
        }
        if (%ctrl.isVisible() && %overlap)
        {
            %edge = getWord(%ctrl.getExtent(), 0) + %posX;
            %width = mMax(%width, %edge);
        }
        %n = %n - 1;
    }
    return %width;
}
function WindowManager::getClientRectPosition(%this)
{
    %pos = %this.getLeftMargin() SPC 0;
    return %pos;
}
function WindowManager::getClientRectExtent(%this)
{
    %min = %this.getClientRectPosition();
    %max = getWord(getRes(), 0) - %this.getRightMargin() SPC getWord(getRes(), 1);
    %ext = getWords(VectorSub(%max, %min), 0, 1);
    return ;
}
function WindowManager::countVisibleRightMarginWindows(%this)
{
    %count = 0;
    %n = 0;
    while (%n < %this.rightMargin.numWindows)
    {
        if (WindowManager.rightMargin.windows[%n].isVisible())
        {
            %count = %count + 1;
        }
        %n = %n + 1;
    }
    return %count;
}
$gWindowManagerSpacerWeight = 1e-05;
function WindowManager::repositionWindows(%this, %windowSet)
{
    if (%windowSet.numWindows == 0)
    {
        return ;
    }
    %recomputing = 1;
    while (%recomputing)
    {
        %recomputing = 0;
        %totalWeight = 0;
        %padding = %windowSet.Padding;
        %residualHeight = getWord($UserPref::Video::Resolution, 1);
        %residualHeight = %residualHeight - %windowSet.getFieldValue("bottomMargin");
        %residualHeight = %residualHeight - %padding;
        %oldestWin = "";
        %i = 0;
        while (%i < %windowSet.numWindows)
        {
            %win = %windowSet.windows[%i];
            if (!isObject(%win))
            {
                continue;
            }
            if (%win.isVisible())
            {
                if (%win.getFieldValue("doAutoClose") && (%win.getFieldValue("age") > 0))
                {
                    if (%oldestWin $= "")
                    {
                        %oldestWin = %win;
                    }
                    else
                    {
                        if (%win.getFieldValue("age") > %oldestWin.getFieldValue("age"))
                        {
                            %oldestWin = %win;
                        }
                    }
                }
                %weight = %win.vWeight;
                if (%weight == 0)
                {
                    %weight = 1;
                    %residualHeight = %residualHeight - %padding;
                }
                else
                {
                    if (%weight < 0)
                    {
                        %weight = 0;
                        %residualHeight = %residualHeight - (getWord(%win.getExtent(), 1) + %padding);
                    }
                    else
                    {
                        if (%weight == 2)
                        {
                            %weight = $gWindowManagerSpacerWeight;
                        }
                    }
                }
                DEBUG_WM("weight: " @ %weight);
                %totalWeight = %totalWeight + %weight;
            }
            %i = %i + 1;
        }
        DEBUG_WM("total weight: " @ %totalWeight);
        %ypos = %padding;
        %i = 0;
        while (%i < %windowSet.numWindows)
        {
            %win = %windowSet.windows[%i];
            if (%win.isVisible())
            {
                %weight = %win.vWeight;
                if (%weight == 0)
                {
                    %weight = 1;
                }
                else
                {
                    if (%weight == 2)
                    {
                        %weight = $gWindowManagerSpacerWeight;
                    }
                }
                %ratio = %totalWeight == 0 ? 0 : %totalWeight;
                %height = %ratio > 0 ? %residualHeight : getWord(%win.getExtent(), 1);
                %minHeight = getWord(%win.minExtent, 1);
                %height[%i] = %height < %minHeight ? %minHeight : %height ;
                %ypos = %ypos + (%height[%i] + %padding);
            }
            %i = %i + 1;
        }
        if ((%ypos > (getWord($UserPref::Video::Resolution, 1) - %windowSet.getFieldValue("bottomMargin"))) && !((%oldestWin $= "")))
        {
            %recomputing = 1;
            %oldestWin.close();
        }
    }
    %ypos = %padding;
    %i = 0;
    while (%i < %windowSet.numWindows)
    {
        %win = %windowSet.windows[%i];
        if (%win.isVisible())
        {
            %win.age = %win.getFieldValue("age") + 1;
            %xPos = getWord(%win.getPosition(), 0);
            %width = getWord(%win.getExtent(), 0);
            %curHeight = getWord(%win.getExtent(), 1);
            %win.resize(%xPos, %ypos, %width, %height[%i]);
            if (%win.hasMethod("onResized") && (%height[%i] != %curHeight))
            {
                %win.onResized();
            }
            %ypos = %ypos + (getWord(%win.getExtent(), 1) + %padding);
        }
        else
        {
            %win.age = 0;
        }
        %i = %i + 1;
    }
    %windowSet.bottom = %ypos;
    return ;
}
function WindowManager::update(%this)
{
    %this.repositionWindows(%this.leftMargin);
    %this.repositionWindows(%this.rightMargin);
    ConvBub.updateAutoMargins();
    return ;
}
function BuddyHudWin::open(%this)
{
    BuddyHudWin.refreshFavoritesList();
    BuddyHudWin.refreshAIMBuddyList();
    BuddyHudWin.clearSelections();
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    WindowManager.update();
    return ;
}
function BuddyHudWin::close(%this)
{
    %this.setVisible(0);
    if (!$UserPref::AIM::RememberMe && isObject(AIMScreenNameField))
    {
        AIMScreenNameField.setText("");
    }
    if (!$UserPref::AIM::SavePassword && isObject(AIMPasswordField))
    {
        AIMPasswordField.setText("");
    }
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
}
function toggleGameMgrHudWin()
{
    GameMgrHudWin.toggle();
    return ;
}
function GameMgrHudWin::toggle(%this)
{
    if (!$player.rolesPermissionCheckNoWarn("debugActive"))
    {
        return ;
    }
    if (%this.isVisible())
    {
        %this.close();
    }
    else
    {
        %this.open();
    }
    return ;
}
function GameMgrHudWin::open(%this)
{
    return ;
    if (!$player.rolesPermissionCheckNoWarn("debugActive"))
    {
        return ;
    }
    if (!$player.rolesPermissionCheckNoWarn("gamesCreate"))
    {
        return ;
    }
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    WindowManager.update();
    return ;
}
function GameMgrHudWin::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
}
function PlayerWin::open(%this)
{
    if (!($player.getShapeName() $= ""))
    {
        PlayerWin.setText("\c3" SPC $player.getShapeName());
    }
    else
    {
        PlayerWin.setText("\c3Player - Cam!");
    }
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    WindowManager.update();
    return ;
}
function PlayerWin::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
}
