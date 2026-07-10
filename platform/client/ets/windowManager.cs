$doPrintsDebug_WM = 0;
function DEBUG_WM(%text) {
    if ($doPrintsDebug_WM) {
        echo(%text);
    }
};
if (!(isObject())) {
    new ScriptObject(WindowManager);
    if (isObject()) {
        add();
    }
}
function WindowManager::Initialize(%this) {
    leftMargin = WindowManager @ safeEnsureScriptObject("SimObject", "WindowManagerLeftMargin") @ WindowManager;
    MissionCleanup;
    bottomMargin = WindowManager @ leftMargin;
    MissionCleanup @ 225;
    %n = 0;
    WindowManager;
    windows = CSControlPanel @ %n @ WindowManager @ leftMargin;
    %n = (1.0 + %n);
    windows = CSFurnitureMover @ %n @ WindowManager @ leftMargin;
    %n = (1.0 + %n);
    windows = CSInventoryBrowserWindow @ %n @ WindowManager @ leftMargin;
    %n = (1.0 + %n);
    windows = CSShoppingBrowserWindow @ %n @ WindowManager @ leftMargin;
    %n = (1.0 + %n);
    windows = CSPaintingWindow @ %n @ WindowManager @ leftMargin;
    %n = (1.0 + %n);
    windows = CSMediaDisplay @ %n @ WindowManager @ leftMargin;
    %n = (1.0 + %n);
    windows = CSRulesAndDescWindow @ %n @ WindowManager @ leftMargin;
    %n = (1.0 + %n);
    windows = CSLayoutSelector @ %n @ WindowManager @ leftMargin;
    %n = (1.0 + %n);
    numWindows = WindowManager @ leftMargin;
    %n;
    Padding = WindowManager @ leftMargin;
    4;
    rightMargin = safeEnsureScriptObject("SimObject", "WindowManagerRightMargin") @ WindowManager;
    %n = 0;
    windows = AccountBalanceHud @ %n @ WindowManager @ rightMargin;
    %n = (1.0 + %n);
    windows = BuddyHudWin @ %n @ WindowManager @ rightMargin;
    %n = (1.0 + %n);
    windows = EmoteHudWin @ %n @ WindowManager @ rightMargin;
    %n = (1.0 + %n);
    if (isObject()) {
        windows = geActivitiesPanel @ %n @ WindowManager @ rightMargin;
        geActivitiesPanel;
        %n = (1.0 + %n);
    }
    windows = GameMgrHudWin @ %n @ WindowManager @ rightMargin;
    %n = (1.0 + %n);
    windows = geLocalMapContainer @ %n @ WindowManager @ rightMargin;
    %n = (1.0 + %n);
    windows = BottomSpacerRTHudWin @ %n @ WindowManager @ rightMargin;
    %n = (1.0 + %n);
    windows = DownloadProgressHudWin @ %n @ WindowManager @ rightMargin;
    %n = (1.0 + %n);
    numWindows = WindowManager @ rightMargin;
    %n;
    Padding = WindowManager @ rightMargin;
    0;
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
    if ((numWindows < %n)) {
        %win = windows;
        WindowManager @ rightMargin;
        %pos = %win.getPosition();
        rightMargin @ %n;
        %posX = getWord(%pos, 0);
        %this;
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
    (numWindows < %n);
    if ((0.0 >= %n)) {
        %ctrl = getWord($gWindowManagerMarginSpecialCasesRight, %n);
        rightMargin;
        if (!(isObject(%ctrl))) {
            return %this;
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
    if ((numWindows < %n)) {
        %win = windows;
        WindowManager @ leftMargin;
        %pos = %win.getPosition();
        leftMargin @ %n;
        %posX = getWord(%pos, 0);
        %this;
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
    (numWindows < %n);
    if ((0.0 >= %n)) {
        %ctrl = getWord($gWindowManagerMarginSpecialCasesLeft, %n);
        leftMargin;
        if (!(isObject(%ctrl))) {
            return %this;
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
    if ((numWindows < %n)) {
        if (windows.isVisible()) {
            %count = (1.0 + %count);
            WindowManager @ rightMargin;
        }
        %n = (1.0 + %n);
        rightMargin @ %n;
    }
    return %count;
};
$gWindowManagerSpacerWeight = 0.00001;
function WindowManager::repositionWindows(%this, %windowSet) {
    if ((%windowSet == numWindows)) {
        return 0.0;
    }
    %recomputing = 1;
    if (%recomputing) {
        %recomputing = 0;
        %totalWeight = 0.0;
        %padding = Padding;
        %windowSet;
        %residualHeight = getWord($UserPref::Video::Resolution, 1);
        %residualHeight = (%windowSet.getFieldValue("bottomMargin") - %residualHeight);
        %residualHeight = (%padding - %residualHeight);
        %oldestWin = "";
        %i = 0;
        if ((numWindows < %i)) {
            %win = windows;
            %windowSet @ %i @ %windowSet;
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
                %weight = vWeight;
                %win;
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
        DEBUG_WM((numWindows < %i) @ "total weight: " @ %totalWeight);
        %ypos = %padding;
        %windowSet;
        %i = 0;
        if ((numWindows < %i)) {
            %win = windows;
            %windowSet @ %i @ %windowSet;
            if (%win.isVisible()) {
                %weight = vWeight;
                %win;
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
                %minHeight = getWord(minExtent, 1);
                %win;
                if ((%minHeight < %height)) {
                }
                %i[%height @ %i] = %minHeight @ %height;
                %ypos = ((%padding + %i[%height @ %i]) + %ypos);
            }
            %i = (1.0 + %i);
        }
        if (((%windowSet.getFieldValue("bottomMargin") - getWord($UserPref::Video::Resolution, 1)) > %ypos)) {
        }
        if (!((numWindows < %i) SPC %oldestWin $= "")) {
            %recomputing = 1;
            %windowSet;
            %oldestWin.close();
        }
    }
    %ypos = %padding;
    %recomputing;
    %i = 0;
    if ((numWindows < %i)) {
        %win = windows;
        %windowSet @ %i @ %windowSet;
        if (%win.isVisible()) {
            age = (1.0 + %win.getFieldValue("age")) @ %win;
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
        age = 0 @ %win;
        %i = (1.0 + %i);
    }
    bottom = (numWindows < %i) @ %ypos @ %windowSet;
    %windowSet;
};
function WindowManager::update(%this) {
    %this.repositionWindows(leftMargin);
    %this.repositionWindows(rightMargin);
    updateAutoMargins();
};
function BuddyHudWin::open(%this) {
    refreshFavoritesList();
    refreshAIMBuddyList();
    clearSelections();
    %this.setVisible(1);
    %this.focusAndRaise();
    update();
};
function BuddyHudWin::close(%this) {
    %this.setVisible(0);
    if (!($UserPref::AIM::RememberMe)) {
    }
    if (isObject()) {
        "".setText();
    }
    if (!($UserPref::AIM::SavePassword)) {
    }
    if (isObject()) {
        "".setText();
    }
    focusTopWindow();
    update();
    return 1;
};
function toggleGameMgrHudWin() {
    toggle();
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
    %this.focusAndRaise();
    update();
};
function GameMgrHudWin::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    update();
    return 1;
};
function PlayerWin::open(%this) {
    if (!($player.getShapeName() $= "")) {
        "\x04" @ " " @ $player.getShapeName().setText();
    }
    "\x04Player - Cam!".setText();
    %this.setVisible(1);
    %this.focusAndRaise();
    update();
};
function PlayerWin::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    update();
    return 1;
};
