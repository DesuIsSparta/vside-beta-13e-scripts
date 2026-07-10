$doPrintsDebug_WM = 0;
function DEBUG_WM(%text) {
    if ($doPrintsDebug_WM) {
        echo(%text);
    }
};
if (!isObject(WindowManager)) {
    new ScriptObject(WindowManager);
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(WindowManager);
    }
}
function WindowManager::Initialize(%this) {
    WindowManager.leftMargin = safeEnsureScriptObject("SimObject", "WindowManagerLeftMargin");
    WindowManager.leftMargin.bottomMargin = 225;
    %n = 0;
    WindowManager.leftMargin.windows[%n] = CSControlPanel;
    %n = (%n + 1.0);
    WindowManager.leftMargin.windows[%n] = CSFurnitureMover;
    %n = (%n + 1.0);
    WindowManager.leftMargin.windows[%n] = CSInventoryBrowserWindow;
    %n = (%n + 1.0);
    WindowManager.leftMargin.windows[%n] = CSShoppingBrowserWindow;
    %n = (%n + 1.0);
    WindowManager.leftMargin.windows[%n] = CSPaintingWindow;
    %n = (%n + 1.0);
    WindowManager.leftMargin.windows[%n] = CSMediaDisplay;
    %n = (%n + 1.0);
    WindowManager.leftMargin.windows[%n] = CSRulesAndDescWindow;
    %n = (%n + 1.0);
    WindowManager.leftMargin.windows[%n] = CSLayoutSelector;
    %n = (%n + 1.0);
    WindowManager.leftMargin.numWindows = %n;
    WindowManager.leftMargin.Padding = 4;
    WindowManager.rightMargin = safeEnsureScriptObject("SimObject", "WindowManagerRightMargin");
    %n = 0;
    WindowManager.rightMargin.windows[%n] = AccountBalanceHud;
    %n = (%n + 1.0);
    WindowManager.rightMargin.windows[%n] = BuddyHudWin;
    %n = (%n + 1.0);
    WindowManager.rightMargin.windows[%n] = EmoteHudWin;
    %n = (%n + 1.0);
    if (isObject(geActivitiesPanel)) {
        WindowManager.rightMargin.windows[%n] = geActivitiesPanel;
        %n = (%n + 1.0);
    }
    WindowManager.rightMargin.windows[%n] = GameMgrHudWin;
    %n = (%n + 1.0);
    WindowManager.rightMargin.windows[%n] = geLocalMapContainer;
    %n = (%n + 1.0);
    WindowManager.rightMargin.windows[%n] = BottomSpacerRTHudWin;
    %n = (%n + 1.0);
    WindowManager.rightMargin.windows[%n] = DownloadProgressHudWin;
    %n = (%n + 1.0);
    WindowManager.rightMargin.numWindows = %n;
    WindowManager.rightMargin.Padding = 0;
    $WindowManager::Initialized = 1;
};
function WindowManager::wakeUp(%this) {
    if (!$WindowManager::Initialized) {
        %this.Initialize();
    }
};
function WindowManager::getRightMargin(%this) {
    return %this.getRightMarginAtY(-(1.0));
};
$gWindowManagerMarginSpecialCasesRight = "";
$gWindowManagerMarginSpecialCasesLeft = "AimConvContainer";
function WindowManager::getRightMarginAtY(%this, %checkAtY) {
    %position = %windowWidth = getWord(getRes(), 0);
    %n = 0;
    while ((%n < %this.rightMargin.numWindows)) {
        %win = WindowManager.rightMargin.windows[%n];
        %pos = %win.getPosition();
        %posX = getWord(%pos, 0);
        %posY = getWord(%pos, 1);
        if ((%checkAtY >= 0.0)) {
            if ((%posY <= %checkAtY)) {
            }
            %overlap = ((%posY + getWord(%win.getExtent(), 1)) >= %checkAtY);
        } else {
            %overlap = 1;
        }
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
        if (!isObject(%ctrl)) {
            return;
        }
        %pos = %ctrl.getPosition();
        %posX = getWord(%pos, 0);
        %posY = getWord(%pos, 1);
        if ((%checkAtY >= 0.0)) {
            if ((%posY <= %checkAtY)) {
            }
            %overlap = ((%posY + getWord(%ctrl.getExtent(), 1)) >= %checkAtY);
        } else {
            %overlap = 1;
        }
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
    return %this.getLeftMarginAtY(-(1.0));
};
function WindowManager::getLeftMarginAtY(%this, %checkAtY) {
    %width = 0;
    %n = 0;
    while ((%n < %this.leftMargin.numWindows)) {
        %win = WindowManager.leftMargin.windows[%n];
        %pos = %win.getPosition();
        %posX = getWord(%pos, 0);
        %posY = getWord(%pos, 1);
        if ((%checkAtY >= 0.0)) {
            if ((%posY <= %checkAtY)) {
            }
            %overlap = ((%posY + getWord(%win.getExtent(), 1)) >= %checkAtY);
        } else {
            %overlap = 1;
        }
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
        if (!isObject(%ctrl)) {
            return;
        }
        %pos = %ctrl.getPosition();
        %posX = getWord(%pos, 0);
        %posY = getWord(%pos, 1);
        if ((%checkAtY >= 0.0)) {
            if ((%posY <= %checkAtY)) {
            }
            %overlap = ((%posY + getWord(%ctrl.getExtent(), 1)) >= %checkAtY);
        } else {
            %overlap = 1;
        }
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
        if (WindowManager.rightMargin.windows[%n].isVisible()) {
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
        %residualHeight = (%residualHeight - %windowSet.getFieldValue("bottomMargin"));
        %residualHeight = (%residualHeight - %padding);
        %oldestWin = "";
        %i = 0;
        while ((%i < %windowSet.numWindows)) {
            %win = %windowSet.windows[%i];
            if (!isObject(%win)) {
            } else {
                if (%win.isVisible()) {
                    if (%win.getFieldValue("doAutoClose")) {
                    }
                    if ((%win.getFieldValue("age") > 0.0)) {
                        if ((%oldestWin $= "")) {
                            %oldestWin = %win;
                        } else {
                            if ((%win.getFieldValue("age") > %oldestWin.getFieldValue("age"))) {
                                %oldestWin = %win;
                            }
                        }
                    }
                    %weight = %win.vWeight;
                    if ((%weight == 0.0)) {
                        %weight = 1.0;
                        %residualHeight = (%residualHeight - %padding);
                    } else {
                        if ((%weight < 0.0)) {
                            %weight = 0;
                            %residualHeight = (%residualHeight - (getWord(%win.getExtent(), 1) + %padding));
                        } else {
                            if ((%weight == 2.0)) {
                                %weight = $gWindowManagerSpacerWeight;
                            }
                        }
                    }
                    DEBUG_WM("weight: " @ %weight);
                    %totalWeight = (%totalWeight + %weight);
                }
            }
            %i = (%i + 1.0);
        }
        DEBUG_WM("total weight: " @ %totalWeight);
        %ypos = %padding;
        (%i < %windowSet.numWindows);
        %i = 0;
        while ((%i < %windowSet.numWindows)) {
            %win = %windowSet.windows[%i];
            if (%win.isVisible()) {
                %weight = %win.vWeight;
                if ((%weight == 0.0)) {
                    %weight = 1.0;
                } else {
                    if ((%weight == 2.0)) {
                        %weight = $gWindowManagerSpacerWeight;
                    }
                }
                if ((%totalWeight == 0.0)) {
                } else {
                }
                %ratio = (%weight / %totalWeight);
                0;
                if ((%ratio > 0.0)) {
                } else {
                }
                %height = getWord(%win.getExtent(), 1);
                (%ratio * %residualHeight);
                %minHeight = getWord(%win.minExtent, 1);
                if ((%height < %minHeight)) {
                } else {
                }
                %i[%height @ %i] = %minHeight @ %height;
                %ypos = (%ypos + (%i[%height @ %i] + %padding));
            }
            %i = (%i + 1.0);
        }
        if ((%ypos > (getWord($UserPref::Video::Resolution, 1) - %windowSet.getFieldValue("bottomMargin")))) {
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
        %win = %windowSet.windows[%i];
        if (%win.isVisible()) {
            %win.age = (%win.getFieldValue("age") + 1.0);
            %xPos = getWord(%win.getPosition(), 0);
            %width = getWord(%win.getExtent(), 0);
            %curHeight = getWord(%win.getExtent(), 1);
            %win.resize(%xPos, %ypos, %width, %i[%height @ %i]);
            if (%win.hasMethod("onResized")) {
            }
            if ((%i[%height @ %i] != %curHeight)) {
                %win.onResized();
            }
            %ypos = (%ypos + (getWord(%win.getExtent(), 1) + %padding));
        } else {
            %win.age = 0;
        }
        %i = (%i + 1.0);
    }
    %windowSet.bottom = (%i < %windowSet.numWindows) @ %ypos;
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
    if (!$UserPref::AIM::RememberMe) {
    }
    if (isObject(AIMScreenNameField)) {
        AIMScreenNameField.setText("");
    }
    if (!$UserPref::AIM::SavePassword) {
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
    if (!$player.rolesPermissionCheckNoWarn("debugActive")) {
        return;
    }
    if (%this.isVisible()) {
        %this.close();
    } else {
        %this.open();
    }
};
function GameMgrHudWin::open(%this) {
    return;
    if (!$player.rolesPermissionCheckNoWarn("debugActive")) {
        return;
    }
    if (!$player.rolesPermissionCheckNoWarn("gamesCreate")) {
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
    } else {
        PlayerWin.setText("\x04Player - Cam!");
    }
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
