$doPrintsDebug_WM = 0;
function DEBUG_WM(%text) {
    if ($doPrintsDebug_WM) {
        echo(%text);
    }
};
if (!(isObject(WindowManager))) {
    new ScriptObject(WindowManager);
    if (isObject(MissionCleanup)) {
        WindowManager.add(MissionCleanup);
    }
}
function WindowManager::Initialize(%this) {
    leftMargin = safeEnsureScriptObject("SimObject", "WindowManagerLeftMargin") @ WindowManager;
    leftMargin.bottomMargin = 225 @ WindowManager;
    %n = 0;
    leftMargin.leftMargin.windows = CSControlPanel @ %n @ WindowManager;
    %n = (%n + 1.0);
    leftMargin.leftMargin.leftMargin.windows = CSFurnitureMover @ %n @ WindowManager;
    %n = (%n + 1.0);
    leftMargin.leftMargin.leftMargin.leftMargin.windows = CSInventoryBrowserWindow @ %n @ WindowManager;
    %n = (%n + 1.0);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.windows = CSShoppingBrowserWindow @ %n @ WindowManager;
    %n = (%n + 1.0);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.windows = CSPaintingWindow @ %n @ WindowManager;
    %n = (%n + 1.0);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.windows = CSMediaDisplay @ %n @ WindowManager;
    %n = (%n + 1.0);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.windows = CSRulesAndDescWindow @ %n @ WindowManager;
    %n = (%n + 1.0);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.windows = CSLayoutSelector @ %n @ WindowManager;
    %n = (%n + 1.0);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.numWindows = %n @ WindowManager;
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.Padding = 4 @ WindowManager;
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin = safeEnsureScriptObject("SimObject", "WindowManagerRightMargin") @ WindowManager;
    %n = 0;
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.windows = AccountBalanceHud @ %n @ WindowManager;
    %n = (%n + 1.0);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.rightMargin.windows = BuddyHudWin @ %n @ WindowManager;
    %n = (%n + 1.0);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.rightMargin.rightMargin.windows = EmoteHudWin @ %n @ WindowManager;
    %n = (%n + 1.0);
    if (isObject(geActivitiesPanel)) {
        leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.rightMargin.rightMargin.rightMargin.windows = geActivitiesPanel @ %n @ WindowManager;
        %n = (%n + 1.0);
    }
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.windows = GameMgrHudWin @ %n @ WindowManager;
    %n = (%n + 1.0);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.windows = geLocalMapContainer @ %n @ WindowManager;
    %n = (%n + 1.0);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.windows = BottomSpacerRTHudWin @ %n @ WindowManager;
    %n = (%n + 1.0);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.windows = DownloadProgressHudWin @ %n @ WindowManager;
    %n = (%n + 1.0);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.numWindows = %n @ WindowManager;
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.Padding = 0 @ WindowManager;
    $WindowManager::Initialized = 1;
};
function WindowManager::wakeUp(%this) {
    if (!($WindowManager::Initialized)) {
        %this.Initialize();
    }
};
function WindowManager::getRightMargin(%this) {
    return -(1.0).getRightMarginAtY(%this);
};
$gWindowManagerMarginSpecialCasesRight = "";
$gWindowManagerMarginSpecialCasesLeft = "AimConvContainer";
function WindowManager::getRightMarginAtY(%this, %checkAtY) {
    %windowWidth = getWord(getRes(), 0);
    %position = ;
    %n = 0;
    while ((%n < %this.rightMargin.numWindows)) {
        %win = %this.rightMargin.rightMargin.windows;
        %n @ WindowManager;
        %pos = %win.getPosition();
        %posX = getWord(%pos, 0);
        %posY = getWord(%pos, 1);
        if ((%checkAtY >= 0.0)) {
            if ((%posY <= %checkAtY)) {
            }
            %overlap = ((%posY + getWord(%win.getExtent(), 1)) >= %checkAtY);
        }
        %overlap = 1;
        if (%win.isVisible()) {
        }
        if (%overlap) {
            %position = mMin(%position, %posX);
        }
        %n = (%n + 1.0);
    }
    %n = (getWordCount($gWindowManagerMarginSpecialCasesRight) - 1.0);
    (%n < %this.rightMargin.numWindows);
    while ((%n >= 0.0)) {
        %ctrl = getWord($gWindowManagerMarginSpecialCasesRight, %n);
        if (!(isObject(%ctrl))) {
            return;
        }
        %pos = %ctrl.getPosition();
        %posX = getWord(%pos, 0);
        %posY = getWord(%pos, 1);
        if ((%checkAtY >= 0.0)) {
            if ((%posY <= %checkAtY)) {
            }
            %overlap = ((%posY + getWord(%ctrl.getExtent(), 1)) >= %checkAtY);
        }
        %overlap = 1;
        if (%ctrl.isVisible()) {
        }
        if (%overlap) {
            %position = mMin(%position, %posX);
        }
        %n = (%n - 1.0);
    }
    return (%windowWidth - %position);
};
function WindowManager::getLeftMargin(%this) {
    return -(1.0).getLeftMarginAtY(%this);
};
function WindowManager::getLeftMarginAtY(%this, %checkAtY) {
    %width = 0;
    %n = 0;
    while ((%n < %this.leftMargin.numWindows)) {
        %win = %this.leftMargin.leftMargin.windows;
        %n @ WindowManager;
        %pos = %win.getPosition();
        %posX = getWord(%pos, 0);
        %posY = getWord(%pos, 1);
        if ((%checkAtY >= 0.0)) {
            if ((%posY <= %checkAtY)) {
            }
            %overlap = ((%posY + getWord(%win.getExtent(), 1)) >= %checkAtY);
        }
        %overlap = 1;
        if (%win.isVisible()) {
        }
        if (%overlap) {
            %edge = (getWord(%win.getExtent(), 0) + %posX);
            %width = mMax(%width, %edge);
        }
        %n = (%n + 1.0);
    }
    %n = (getWordCount($gWindowManagerMarginSpecialCasesLeft) - 1.0);
    (%n < %this.leftMargin.numWindows);
    while ((%n >= 0.0)) {
        %ctrl = getWord($gWindowManagerMarginSpecialCasesLeft, %n);
        if (!(isObject(%ctrl))) {
            return;
        }
        %pos = %ctrl.getPosition();
        %posX = getWord(%pos, 0);
        %posY = getWord(%pos, 1);
        if ((%checkAtY >= 0.0)) {
            if ((%posY <= %checkAtY)) {
            }
            %overlap = ((%posY + getWord(%ctrl.getExtent(), 1)) >= %checkAtY);
        }
        %overlap = 1;
        if (%ctrl.isVisible()) {
        }
        if (%overlap) {
            %edge = (getWord(%ctrl.getExtent(), 0) + %posX);
            %width = mMax(%width, %edge);
        }
        %n = (%n - 1.0);
    }
    return %width;
};
function WindowManager::getClientRectPosition(%this) {
    %pos = %this.getLeftMargin() @ " " @ 0;
    return %pos;
};
function WindowManager::getClientRectExtent(%this) {
    %min = %this.getClientRectPosition();
    %max = (getWord(getRes(), 0) - %this.getRightMargin()) @ " " @ getWord(getRes(), 1);
    %ext = getWords(VectorSub(%max, %min), 0, 1);
};
function WindowManager::countVisibleRightMarginWindows(%this) {
    %count = 0;
    %n = 0;
    while ((%n < %this.rightMargin.numWindows)) {
        if (%this.rightMargin.rightMargin.windows.isVisible(%n @ WindowManager)) {
            %count = (%count + 1.0);
        }
        %n = (%n + 1.0);
    }
    return %count;
};
$gWindowManagerSpacerWeight = 0.00001;
function WindowManager::repositionWindows(%this, %windowSet) {
    if ((%windowSet.numWindows == 0.0)) {
        return;
    }
    %recomputing = 1;
    while (%recomputing) {
        %recomputing = 0;
        %totalWeight = 0.0;
        %padding = %windowSet.Padding;
        %residualHeight = getWord($UserPref::Video::Resolution, 1);
        %residualHeight = (%residualHeight - "bottomMargin".getFieldValue(%windowSet));
        %residualHeight = (%residualHeight - %padding);
        %oldestWin = "";
        %i = 0;
        while ((%i < %windowSet.numWindows)) {
            %win = %windowSet.windows;
            %i;
            if (!(isObject(%win))) {
            }
            if (%win.isVisible()) {
                if ("doAutoClose".getFieldValue(%win)) {
                }
                if (("age".getFieldValue(%win) > 0.0)) {
                    if ((%oldestWin $= "")) {
                        %oldestWin = %win;
                    }
                    if (("age".getFieldValue(%win) > "age".getFieldValue(%oldestWin))) {
                        %oldestWin = %win;
                    }
                }
                %weight = %win.vWeight;
                if ((%weight == 0.0)) {
                    %weight = 1.0;
                    %residualHeight = (%residualHeight - %padding);
                }
                if ((%weight < 0.0)) {
                    %weight = 0;
                    %residualHeight = (%residualHeight - (getWord(%win.getExtent(), 1) + %padding));
                }
                if ((%weight == 2.0)) {
                    %weight = $gWindowManagerSpacerWeight;
                }
                DEBUG_WM("weight: " @ %weight);
                %totalWeight = (%totalWeight + %weight);
            }
            %i = (%i + 1.0);
        }
        DEBUG_WM("total weight: " @ %totalWeight);
        %ypos = %padding;
        (%i < %windowSet.numWindows);
        %i = 0;
        while ((%i < %windowSet.numWindows)) {
            %win = %windowSet.windows;
            %i;
            if (%win.isVisible()) {
                %weight = %win.vWeight;
                if ((%weight == 0.0)) {
                    %weight = 1.0;
                }
                if ((%weight == 2.0)) {
                    %weight = $gWindowManagerSpacerWeight;
                }
                if ((%totalWeight == 0.0)) {
                }
                %ratio = (%weight / %totalWeight);
                0;
                if ((%ratio > 0.0)) {
                }
                %height = getWord(%win.getExtent(), 1);
                (%ratio * %residualHeight);
                %minHeight = getWord(%win.minExtent, 1);
                if ((%height < %minHeight)) {
                }
                %i[%height @ %i] = %minHeight @ %height;
                %ypos = (%ypos + (%i[%height @ %i] + %padding));
            }
            %i = (%i + 1.0);
        }
        if ((%ypos > (getWord($UserPref::Video::Resolution, 1) - "bottomMargin".getFieldValue(%windowSet)))) {
        }
        if (!((%i < %windowSet.numWindows) @ " " @ %oldestWin $= "")) {
            %recomputing = 1;
            %oldestWin.close();
        }
    }
    %ypos = %padding;
    %recomputing;
    %i = 0;
    while ((%i < %windowSet.numWindows)) {
        %win = %windowSet.windows;
        %i;
        if (%win.isVisible()) {
            %win.age = ("age".getFieldValue(%win) + 1.0);
            %xPos = getWord(%win.getPosition(), 0);
            %width = getWord(%win.getExtent(), 0);
            %curHeight = getWord(%win.getExtent(), 1);
            %i[%height @ %i].resize(%win, %xPos, %ypos, %width);
            if ("onResized".hasMethod(%win)) {
            }
            if ((%i[%height @ %i] != %curHeight)) {
                %win.onResized();
            }
            %ypos = (%ypos + (getWord(%win.getExtent(), 1) + %padding));
        }
        %win.age = 0;
        %i = (%i + 1.0);
    }
    %windowSet.bottom = (%i < %windowSet.numWindows) @ %ypos;
};
function WindowManager::update(%this) {
    %this.leftMargin.repositionWindows(%this);
    %this.rightMargin.repositionWindows(%this);
    ConvBub.updateAutoMargins();
};
function BuddyHudWin::open(%this) {
    BuddyHudWin.refreshFavoritesList();
    BuddyHudWin.refreshAIMBuddyList();
    BuddyHudWin.clearSelections();
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    WindowManager.update();
};
function BuddyHudWin::close(%this) {
    0.setVisible(%this);
    if (!($UserPref::AIM::RememberMe)) {
    }
    if (isObject(AIMScreenNameField)) {
        "".setText(AIMScreenNameField);
    }
    if (!($UserPref::AIM::SavePassword)) {
    }
    if (isObject(AIMPasswordField)) {
        "".setText(AIMPasswordField);
    }
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
};
function toggleGameMgrHudWin() {
    GameMgrHudWin.toggle();
};
function GameMgrHudWin::toggle(%this) {
    if (!("debugActive".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
function GameMgrHudWin::open(%this) {
    return;
    if (!("debugActive".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    if (!("gamesCreate".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    WindowManager.update();
};
function GameMgrHudWin::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
};
function PlayerWin::open(%this) {
    if (!($player.getShapeName() $= "")) {
        "\x04" @ " " @ $player.getShapeName().setText(PlayerWin);
    }
    "\x04Player - Cam!".setText(PlayerWin);
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    WindowManager.update();
};
function PlayerWin::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
};
