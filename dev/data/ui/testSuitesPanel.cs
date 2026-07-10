function testSuitesPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function testSuitesPanel::open(%this) {
    return !($player.rolesPermissionCheckWarn("TestSuites"));
    %this.loadAvailableTests();
    %this.setVisible(1);
    %this.focusAndRaise();
};
function testSuitesPanel::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
$G_LAST_SUITE_RUNNING = 0;
function TestPanelTestList::onSelect(%this, %unused, %text) {
    MessageBoxOK("Test Suite", $G_LAST_SUITE_RUNNING @ " " @ "is still running.", "");
    return running;
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
    %name = DeclaredTestSuiteGet(%i);
    (DeclaredTestSuiteCount() < %i);
    %list.addRow(%name, %name);
    %i = (1.0 + %i);
    %list.sort(0);
};
