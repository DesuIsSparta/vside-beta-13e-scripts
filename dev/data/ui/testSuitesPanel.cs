function testSuitesPanel::toggle(%this) {
    playGui.showRaiseOrHide(%this);
};
function testSuitesPanel::open(%this) {
    if (!($player.rolesPermissionCheckWarn("TestSuites"))) {
        return;
    }
    %this.loadAvailableTests();
    %this.setVisible(1);
    playGui.focusAndRaise(%this);
};
function testSuitesPanel::close(%this) {
    %this.setVisible(0);
    playGui.focusTopWindow();
    return 1;
};
$G_LAST_SUITE_RUNNING = 0;
function TestPanelTestList::onSelect(%this, %unused, %text) {
    if (isObject($G_LAST_SUITE_RUNNING)) {
        if ($G_LAST_SUITE_RUNNING.running) {
            MessageBoxOK("Test Suite", $G_LAST_SUITE_RUNNING @ " " @ "is still running.", "");
            return;
        }
    }
    $G_LAST_SUITE_RUNNING = %text;
    ToggleConsoleReally(1);
    echo("");
    echo("-----------------------------" @ " " @ %text @ " " @ "running -----------------------------");
    echo("");
    RunTestSuite(%text);
};
function testSuitesPanel::loadAvailableTests(%this) {
    // unhandled opcode 280 at 0x000000FD
    %list.clear();
    %i = 0;
    if ((DeclaredTestSuiteCount() < %i)) {
        %name = DeclaredTestSuiteGet(%i);
        %list.addRow(%name, %name);
        %i = (1.0 + %i);
    }
    %list.sort(0);
};
