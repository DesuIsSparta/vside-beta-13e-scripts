function toggleBenchmarksDialog()
{
    if (!$player.rolesPermissionCheckWarn("gameEditors"))
    {
        return ;
    }
    toggleVisibleState(benchmarksGui);
    return ;
}
function benchmarksGui::open(%this)
{
    Canvas.pushDialog(%this, 0);
    %this.setVisible(1);
    benchmarks::loadCameraTests();
    benchmarks::cameraToGui();
    gui_Benchs_Metrics_Menu1.populate();
    return ;
}
function benchmarksGui::close(%this, %unused)
{
    Canvas.popDialog(%this);
    %this.setVisible(0);
    return ;
}
gSetField(gui_Benchs_Metrics_Menu1, populated, 0);
function gui_Benchs_Metrics_Menu1::populate(%this)
{
    if (!gGetField(%this, populated))
    {
        gSetField(%this, populated, 1);
        %this.clear();
        %num = getWordCount($metricsNamesList);
        %n = 0;
        while (%n < %num)
        {
            %text = getWord($metricsNamesList, %n);
            %this.add(%text);
            %n = %n + 1;
        }
        %this.setText("none");
    }
    return ;
}
function gui_Benchs_Metrics_Menu1::onSelect(%this, %unused, %text)
{
    if ((%text $= "video") && (%text $= "texture"))
    {
        GLEnableMetrics(1);
    }
    else
    {
        GLEnableMetrics(0);
    }
    metrics(%text);
    return ;
}
function benchmarksGui::loadCameraTests(%this)
{
    benchmarks::loadCameraTests();
    benchmarks::cameraToGui();
    return ;
}
function benchmarksGui::saveCameraTests(%this)
{
    benchmarks::saveCameraTests();
    return ;
}
function benchmarksGui::clearCameraTests(%this)
{
    benchmarks::clearCameraTests();
    benchmarks::cameraToGui();
    return ;
}
function benchmarksGui::runCameraTests(%this)
{
    benchmarks::runCameraTests();
    %this.updateProgressBars();
    gui_Benchs_Cam_Prog1.setVisible(1);
    gui_Benchs_Cam_Prog2.setVisible(1);
    gui_Benchs_Cam_Run.setVisible(0);
    gui_Benchs_Cam_Cancel.setVisible(1);
    $benchmarks::originalMetrics = gui_Benchs_Metrics_Menu1.getValue();
    if (!$pref::benchmarks::metricsLock)
    {
        gui_Benchs_Metrics_Menu1.setValue("video");
        gui_Benchs_Metrics_Menu1.onSelect(0, gui_Benchs_Metrics_Menu1.getValue());
    }
    return ;
}
function benchmarksGui::runCameraTestsReps(%this)
{
    benchmarks::runCameraTestsReps();
    %this.updateProgressBars();
    gui_Benchs_Cam_Prog1.setVisible(1);
    gui_Benchs_Cam_Prog2.setVisible(1);
    gui_Benchs_Cam_Run.setVisible(0);
    gui_Benchs_Cam_Cancel.setVisible(1);
    $benchmarks::originalMetrics = gui_Benchs_Metrics_Menu1.getValue();
    if (!$pref::benchmarks::metricsLock)
    {
        gui_Benchs_Metrics_Menu1.setValue("video");
        gui_Benchs_Metrics_Menu1.onSelect(0, gui_Benchs_Metrics_Menu1.getValue());
    }
    return ;
}
function benchmarksGui::onFinishedCameraTests(%this)
{
    gui_Benchs_Cam_Run.setVisible(1);
    gui_Benchs_Cam_Cancel.setVisible(0);
    gui_Benchs_Cam_Prog1.setVisible(0);
    gui_Benchs_Cam_Prog2.setVisible(0);
    gui_Benchs_Metrics_Menu1.setValue($benchmarks::originalMetrics);
    gui_Benchs_Metrics_Menu1.onSelect(0, gui_Benchs_Metrics_Menu1.getValue());
    if (!benchmarks::isInteractive())
    {
        return ;
    }
    setClipboard($benchmarks::camera::resultString);
    benchmarks::MessageBoxOK("Benchmark Results", ".. are now in the clipboard,\n(and in the console.log)");
    return ;
}
function benchmarksGui::cancelCameraTests(%this)
{
    gui_Benchs_Cam_Run.setVisible(1);
    gui_Benchs_Cam_Cancel.setVisible(0);
    gui_Benchs_Cam_Prog1.setVisible(0);
    gui_Benchs_Cam_Prog2.setVisible(0);
    benchmarks::cancelCameraTests();
    return ;
}
function benchmarksGui::updateProgressBars(%this)
{
    gui_Benchs_Cam_Prog1.setValue(($benchmarks::camera::curPoint + 1) / cameraTestsGroup.getCount());
    gui_Benchs_Cam_Prog2.setValue(($benchmarks::camera::repsDone + 1) / $pref::benchmarks::fps::reps);
    return ;
}
function benchmarksGui::addNewCameraTestPoint1(%this)
{
    gui_Benchs_Cam_Add1.setVisible(0);
    gui_Benchs_Cam_Add2.setVisible(1);
    gui_Benchs_Cam_Add3.setVisible(1);
    gui_Benchs_Cam_Name.setVisible(0);
    gui_Benchs_Cam_NameIn.setVisible(1);
    gui_Benchs_Cam_NameIn.selectAll();
    gui_Benchs_Cam_Prog1.setVisible(0);
    gui_Benchs_Cam_Prog2.setVisible(0);
    gui_Benchs_Cam_NameIn.makeFirstResponder(1);
    gui_Benchs_Cam_NameIn.setSelection(0, 10000);
    return ;
}
function benchmarksGui::addNewCameraTestPoint2(%this)
{
    gui_Benchs_Cam_Add1.setVisible(1);
    gui_Benchs_Cam_Add2.setVisible(0);
    gui_Benchs_Cam_Add3.setVisible(0);
    gui_Benchs_Cam_Name.setVisible(1);
    gui_Benchs_Cam_NameIn.setVisible(0);
    benchmarks::addNewCameraTestPoint(gui_Benchs_Cam_NameIn.getValue());
    return ;
}
function benchmarksGui::addNewCameraTestPoint3(%this)
{
    gui_Benchs_Cam_Add1.setVisible(1);
    gui_Benchs_Cam_Add2.setVisible(0);
    gui_Benchs_Cam_Add3.setVisible(0);
    gui_Benchs_Cam_Name.setVisible(1);
    gui_Benchs_Cam_NameIn.setVisible(0);
    return ;
}
