function testSuitesPanel::toggle(%this)
{
    playGui.showRaiseOrHide(%this);
    return ;
}
function testSuitesPanel::open(%this)
{
    if (!$player.rolesPermissionCheckWarn("TestSuites"))
    {
        return ;
    }
    %this.loadAvailableTests();
    %this.setVisible(1);
    playGui.focusAndRaise(%this);
    return ;
}
function testSuitesPanel::close(%this)
{
    %this.setVisible(0);
    playGui.focusTopWindow();
    return 1;
}
$G_LAST_SUITE_RUNNING = 0;
function TestPanelTestList::onSelect(%this, %unused, %text)
{
    if (isObject($G_LAST_SUITE_RUNNING))
    {
        if ($G_LAST_SUITE_RUNNING.running)
        {
            MessageBoxOK("Test Suite", $G_LAST_SUITE_RUNNING SPC "is still running.", "");
            return ;
        }
    }
    $G_LAST_SUITE_RUNNING = %text;
    ToggleConsoleReally(1);
    echo("");
    echo("-----------------------------" SPC %text SPC "running -----------------------------");
    echo("");
    RunTestSuite(%text);
    return ;
}
function testSuitesPanel::loadAvailableTests(%this)
{
    %list = TestPanelTestList;
    %list.clear();
    %i = 0;
    while (%i < DeclaredTestSuiteCount())
    {
        %name = DeclaredTestSuiteGet(%i);
        %list.addRow(%name, %name);
        %i = %i + 1;
    }
    %list.sort(0);
    return ;
}
