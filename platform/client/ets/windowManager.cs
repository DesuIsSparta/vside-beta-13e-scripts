$doPrintsDebug_WM = 0;
function DEBUG_WM(%text) {
    if ($doPrintsDebug_WM) {
        echo(%text);
    }
};
if (!(isObject(WindowManager))) {
    new ScriptObject(WindowManager);
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(WindowManager);
    }
}
function WindowManager::Initialize(%this) {
    leftMargin = safeEnsureScriptObject("SimObject", "WindowManagerLeftMargin") @ WindowManager;
    leftMargin.bottomMargin = 225 @ WindowManager;
    %n = 0;
    leftMargin.leftMargin.windows = CSControlPanel @ %n @ WindowManager;
    %n = (1.0 + %n);
    leftMargin.leftMargin.leftMargin.windows = CSFurnitureMover @ %n @ WindowManager;
    %n = (1.0 + %n);
    leftMargin.leftMargin.leftMargin.leftMargin.windows = CSInventoryBrowserWindow @ %n @ WindowManager;
    %n = (1.0 + %n);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.windows = CSShoppingBrowserWindow @ %n @ WindowManager;
    %n = (1.0 + %n);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.windows = CSPaintingWindow @ %n @ WindowManager;
    %n = (1.0 + %n);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.windows = CSMediaDisplay @ %n @ WindowManager;
    %n = (1.0 + %n);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.windows = CSRulesAndDescWindow @ %n @ WindowManager;
    %n = (1.0 + %n);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.windows = CSLayoutSelector @ %n @ WindowManager;
    %n = (1.0 + %n);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.numWindows = %n @ WindowManager;
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.Padding = 4 @ WindowManager;
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin = safeEnsureScriptObject("SimObject", "WindowManagerRightMargin") @ WindowManager;
    %n = 0;
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.windows = AccountBalanceHud @ %n @ WindowManager;
    %n = (1.0 + %n);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.rightMargin.windows = BuddyHudWin @ %n @ WindowManager;
    %n = (1.0 + %n);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.rightMargin.rightMargin.windows = EmoteHudWin @ %n @ WindowManager;
    %n = (1.0 + %n);
    if (isObject(geActivitiesPanel)) {
        leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.rightMargin.rightMargin.rightMargin.windows = geActivitiesPanel @ %n @ WindowManager;
        %n = (1.0 + %n);
    }
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.windows = GameMgrHudWin @ %n @ WindowManager;
    %n = (1.0 + %n);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.windows = geLocalMapContainer @ %n @ WindowManager;
    %n = (1.0 + %n);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.windows = BottomSpacerRTHudWin @ %n @ WindowManager;
    %n = (1.0 + %n);
    leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.leftMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.rightMargin.windows = DownloadProgressHudWin @ %n @ WindowManager;
    %n = (1.0 + %n);
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
    return %this.getRightMarginAtY(-(1.0));
};
$gWindowManagerMarginSpecialCasesRight = "";
$gWindowManagerMarginSpecialCasesLeft = "AimConvContainer";
function WindowManager::getRightMarginAtY(%this, %checkAtY) {
    %windowWidth = getWord(getRes(), 0);
    %position = ;
    %n = 0;
    if ((%this.rightMargin.numWindows < %n)) {
        %win = %this.rightMargin.rightMargin.windows;
        %n @ WindowManager;
        %pos = %win.getPosition();
        %posX = getWord(%pos, 0);
        %posY = getWord(%pos, 1);
        if ((0.0 >= %checkAtY)) {
            if ((%checkAtY <= %posY)) {
            }
            %overlap = (%checkAtY >= (getWord(%win.getExtent(), 1) + %posY));
        }
        %overlap = 1;
        if (%win.isVisible()) {
        }
        if (%overlap) {
            %position = mMin(%position, %posX);
        }
        %n = (1.0 + %n);
    }
    %n = (1.0 - getWordCount($gWindowManagerMarginSpecialCasesRight));
    (%this.rightMargin.numWindows < %n);
    if ((0.0 >= %n)) {
        %ctrl = getWord($gWindowManagerMarginSpecialCasesRight, %n);
        if (!(isObject(%ctrl))) {
            return;
        }
        %pos = %ctrl.getPosition();
        %posX = getWord(%pos, 0);
        %posY = getWord(%pos, 1);
        if ((0.0 >= %checkAtY)) {
            if ((%checkAtY <= %posY)) {
            }
            %overlap = (%checkAtY >= (getWord(%ctrl.getExtent(), 1) + %posY));
        }
        %overlap = 1;
        if (%ctrl.isVisible()) {
        }
        if (%overlap) {
            %position = mMin(%position, %posX);
        }
        %n = (1.0 - %n);
    }
    return (%position - %windowWidth);
};
function WindowManager::getLeftMargin(%this) {
    return %this.getLeftMarginAtY(-(1.0));
};
function WindowManager::getLeftMarginAtY(%this, %checkAtY) {
    %width = 0;
    %n = 0;
    if ((%this.leftMargin.numWindows < %n)) {
        %win = %this.leftMargin.leftMargin.windows;
        %n @ WindowManager;
        %pos = %win.getPosition();
        %posX = getWord(%pos, 0);
        %posY = getWord(%pos, 1);
        if ((0.0 >= %checkAtY)) {
            if ((%checkAtY <= %posY)) {
            }
            %overlap = (%checkAtY >= (getWord(%win.getExtent(), 1) + %posY));
        }
        %overlap = 1;
        if (%win.isVisible()) {
        }
        if (%overlap) {
            %edge = (%posX + getWord(%win.getExtent(), 0));
            %width = mMax(%width, %edge);
        }
        %n = (1.0 + %n);
    }
    %n = (1.0 - getWordCount($gWindowManagerMarginSpecialCasesLeft));
    (%this.leftMargin.numWindows < %n);
    if ((0.0 >= %n)) {
        %ctrl = getWord($gWindowManagerMarginSpecialCasesLeft, %n);
        if (!(isObject(%ctrl))) {
            return;
        }
        %pos = %ctrl.getPosition();
        %posX = getWord(%pos, 0);
        %posY = getWord(%pos, 1);
        if ((0.0 >= %checkAtY)) {
            if ((%checkAtY <= %posY)) {
            }
            %overlap = (%checkAtY >= (getWord(%ctrl.getExtent(), 1) + %posY));
        }
        %overlap = 1;
        if (%ctrl.isVisible()) {
        }
        if (%overlap) {
            %edge = (%posX + getWord(%ctrl.getExtent(), 0));
            %width = mMax(%width, %edge);
        }
        %n = (1.0 - %n);
    }
    return %width;
};
function WindowManager::getClientRectPosition(%this) {
    %pos = %this.getLeftMargin() @ " " @ 0;
    return %pos;
};
function WindowManager::getClientRectExtent(%this) {
    %min = %this.getClientRectPosition();
    %max = (%this.getRightMargin() - getWord(getRes(), 0)) @ " " @ getWord(getRes(), 1);
    %ext = getWords(VectorSub(%max, %min), 0, 1);
};
function WindowManager::countVisibleRightMarginWindows(%this) {
    %count = 0;
    %n = 0;
    if ((%this.rightMargin.numWindows < %n)) {
        if (%n @ WindowManager.isVisible(%this.rightMargin.rightMargin.windows)) {
            %count = (1.0 + %count);
        }
        %n = (1.0 + %n);
    }
    return %count;
};
$gWindowManagerSpacerWeight = 0.00001;
function WindowManager::repositionWindows(%this, %windowSet) {
    if ((0.0 == %windowSet.numWindows)) {
        return;
    }
    %recomputing = 1;
    if (%recomputing) {
        %recomputing = 0;
        %totalWeight = 0.0;
        %padding = %windowSet.Padding;
        %residualHeight = getWord($UserPref::Video::Resolution, 1);
        %residualHeight = (%windowSet.getFieldValue("bottomMargin") - %residualHeight);
        %residualHeight = (%padding - %residualHeight);
        %oldestWin = "";
        %i = 0;
        if ((%windowSet.numWindows < %i)) {
            %win = %windowSet.windows;
            %i;
            if (!(isObject(%win))) {
            }
            if (%win.isVisible()) {
                if (%win.getFieldValue("doAutoClose")) {
                }
                if ((0.0 > %win.getFieldValue("age"))) {
                    if ((%oldestWin $= "")) {
                        %oldestWin = %win;
                    }
                    if ((%oldestWin.getFieldValue("age") > %win.getFieldValue("age"))) {
                        %oldestWin = %win;
                    }
                }
                %weight = %win.vWeight;
                if ((0.0 == %weight)) {
                    %weight = 1.0;
                    %residualHeight = (%padding - %residualHeight);
                }
                if ((0.0 < %weight)) {
                    %weight = 0;
                    %residualHeight = ((%padding + getWord(%win.getExtent(), 1)) - %residualHeight);
                }
                if ((2.0 == %weight)) {
                    %weight = $gWindowManagerSpacerWeight;
                }
                DEBUG_WM("weight: " @ %weight);
                %totalWeight = (%weight + %totalWeight);
            }
            %i = (1.0 + %i);
        }
        DEBUG_WM("total weight: " @ %totalWeight);
        %ypos = %padding;
        (%windowSet.numWindows < %i);
        %i = 0;
        if ((%windowSet.numWindows < %i)) {
            %win = %windowSet.windows;
            %i;
            if (%win.isVisible()) {
                %weight = %win.vWeight;
                if ((0.0 == %weight)) {
                    %weight = 1.0;
                }
                if ((2.0 == %weight)) {
                    %weight = $gWindowManagerSpacerWeight;
                }
                if ((0.0 == %totalWeight)) {
                }
                %ratio = (%totalWeight / %weight);
                0;
                if ((0.0 > %ratio)) {
                }
                %height = getWord(%win.getExtent(), 1);
                (%residualHeight * %ratio);
                %minHeight = getWord(%win.minExtent, 1);
                if ((%minHeight < %height)) {
                }
                %i[%height @ %i] = %minHeight @ %height;
                %ypos = ((%padding + %i[%height @ %i]) + %ypos);
            }
            %i = (1.0 + %i);
        }
        if (((%windowSet.getFieldValue("bottomMargin") - getWord($UserPref::Video::Resolution, 1)) > %ypos)) {
        }
        if (!((%windowSet.numWindows < %i) @ " " @ %oldestWin $= "")) {
            %recomputing = 1;
            %oldestWin.close();
        }
    }
    %ypos = %padding;
    %recomputing;
    %i = 0;
    if ((%windowSet.numWindows < %i)) {
        %win = %windowSet.windows;
        %i;
        if (%win.isVisible()) {
            %win.age = (1.0 + %win.getFieldValue("age"));
            %xPos = getWord(%win.getPosition(), 0);
            %width = getWord(%win.getExtent(), 0);
            %curHeight = getWord(%win.getExtent(), 1);
            %win.resize(%xPos, %ypos, %width, %i[%height @ %i]);
            if (%win.hasMethod("onResized")) {
            }
            if ((%curHeight != %i[%height @ %i])) {
                %win.onResized();
            }
            %ypos = ((%padding + getWord(%win.getExtent(), 1)) + %ypos);
        }
        %win.age = 0;
        %i = (1.0 + %i);
    }
    %windowSet.bottom = (%windowSet.numWindows < %i) @ %ypos;
};
function WindowManager::update(%this) {
    %this.repositionWindows(%this.leftMargin);
    %this.repositionWindows(%this.rightMargin);
    ConvBub.updateAutoMargins();
};
function BuddyHudWin::open(%this) {
    BuddyHudWin.refreshFavoritesList();
    BuddyHudWin.refreshAIMBuddyList();
    BuddyHudWin.clearSelections();
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    WindowManager.update();
};
function BuddyHudWin::close(%this) {
    %this.setVisible(0);
    if (!($UserPref::AIM::RememberMe)) {
    }
    if (isObject(AIMScreenNameField)) {
        AIMScreenNameField.setText("");
    }
    if (!($UserPref::AIM::SavePassword)) {
    }
    if (isObject(AIMPasswordField)) {
        AIMPasswordField.setText("");
    }
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
};
function toggleGameMgrHudWin() {
    GameMgrHudWin.toggle();
};
function GameMgrHudWin::toggle(%this) {
    if (!($player.rolesPermissionCheckNoWarn("debugActive"))) {
        return;
    }
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
function GameMgrHudWin::open(%this) {
    return;
    if (!($player.rolesPermissionCheckNoWarn("debugActive"))) {
        return;
    }
    if (!($player.rolesPermissionCheckNoWarn("gamesCreate"))) {
        return;
    }
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    WindowManager.update();
};
function GameMgrHudWin::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
};
function PlayerWin::open(%this) {
    if (!($player.getShapeName() $= "")) {
        PlayerWin.setText("\x04" @ " " @ $player.getShapeName());
    }
    PlayerWin.setText("\x04Player - Cam!");
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    WindowManager.update();
};
function PlayerWin::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
};
