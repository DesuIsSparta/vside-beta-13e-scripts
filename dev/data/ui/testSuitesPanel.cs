function testSuitesPanel::toggle(%this) {
    %this.showRaiseOrHide(playGui);
};
function testSuitesPanel::open(%this) {
    if (!("TestSuites".rolesPermissionCheckWarn($player))) {
        return;
    }
    %this.loadAvailableTests();
    1.setVisible(%this);
    %this.focusAndRaise(playGui);
};
function testSuitesPanel::close(%this) {
    0.setVisible(%this);
    playGui.focusTopWindow();
    return 1;
};
$G_LAST_SUITE_RUNNING = 0;
function TestPanelTestList::onSelect(%this, %unused, %text) {
    if (isObject($G_LAST_SUITE_RUNNING) && $G_LAST_SUITE_RUNNING.running) {
        MessageBoxOK("Test Suite", $G_LAST_SUITE_RUNNING @ " " @ "is still running.", "");
        return;
    }
    $G_LAST_SUITE_RUNNING = %text;
    ToggleConsoleReally(1);
    echo("");
    echo("-----------------------------" @ " " @ %text @ " " @ "running -----------------------------");
    echo("");
    RunTestSuite(%text);
};
function testSuitesPanel::loadAvailableTests(%this) {
    %list = TestPanelTestList;
    %list.clear();
    %i = 0;
    while ((%i < DeclaredTestSuiteCount())) {
        %name = DeclaredTestSuiteGet(%i);
        %name.addRow(%list, %name);
        %i = (%i + 1.0);
    }
    0.sort(%list);
};
