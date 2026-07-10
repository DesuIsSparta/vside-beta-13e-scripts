function toggleBenchmarksDialog() {
    if (!("gameEditors".rolesPermissionCheckWarn($player))) {
        return;
    }
    toggleVisibleState(benchmarksGui);
};
function benchmarksGui::open(%this) {
    0.pushDialog(Canvas, %this);
    1.setVisible(%this);
    benchmarks::loadCameraTests();
    benchmarks::cameraToGui();
    gui_Benchs_Metrics_Menu1.populate();
};
function benchmarksGui::close(%this, %unused) {
    %this.popDialog(Canvas);
    0.setVisible(%this);
};
gSetField(gui_Benchs_Metrics_Menu1, populated, 0);
function gui_Benchs_Metrics_Menu1::populate(%this) {
    if (!(gGetField(%this))) {
        gSetField(%this, populated, 1);
        %this.clear();
        %num = getWordCount($metricsNamesList);
        populated;
        %n = 0;
        while ((%n < %num)) {
            %text = getWord($metricsNamesList, %n);
            %text.add(%this);
            %n = (%n + 1.0);
        }
        "none".setText(%this);
    }
};
function gui_Benchs_Metrics_Menu1::onSelect(%this, %unused, %text) {
    if ((%text $= "video")) {
    }
    if ((%text $= "texture")) {
        GLEnableMetrics(1);
    }
    GLEnableMetrics(0);
    metrics(%text);
};
function benchmarksGui::loadCameraTests(%this) {
    benchmarks::loadCameraTests();
    benchmarks::cameraToGui();
};
function benchmarksGui::saveCameraTests(%this) {
    benchmarks::saveCameraTests();
};
function benchmarksGui::clearCameraTests(%this) {
    benchmarks::clearCameraTests();
    benchmarks::cameraToGui();
};
function benchmarksGui::runCameraTests(%this) {
    benchmarks::runCameraTests();
    %this.updateProgressBars();
    1.setVisible(gui_Benchs_Cam_Prog1);
    1.setVisible(gui_Benchs_Cam_Prog2);
    0.setVisible(gui_Benchs_Cam_Run);
    1.setVisible(gui_Benchs_Cam_Cancel);
    $benchmarks::originalMetrics = gui_Benchs_Metrics_Menu1.getValue();
    if (!($pref::benchmarks::metricsLock)) {
        "video".setValue(gui_Benchs_Metrics_Menu1);
        gui_Benchs_Metrics_Menu1.getValue().onSelect(gui_Benchs_Metrics_Menu1, 0);
    }
};
function benchmarksGui::runCameraTestsReps(%this) {
    benchmarks::runCameraTestsReps();
    %this.updateProgressBars();
    1.setVisible(gui_Benchs_Cam_Prog1);
    1.setVisible(gui_Benchs_Cam_Prog2);
    0.setVisible(gui_Benchs_Cam_Run);
    1.setVisible(gui_Benchs_Cam_Cancel);
    $benchmarks::originalMetrics = gui_Benchs_Metrics_Menu1.getValue();
    if (!($pref::benchmarks::metricsLock)) {
        "video".setValue(gui_Benchs_Metrics_Menu1);
        gui_Benchs_Metrics_Menu1.getValue().onSelect(gui_Benchs_Metrics_Menu1, 0);
    }
};
function benchmarksGui::onFinishedCameraTests(%this) {
    1.setVisible(gui_Benchs_Cam_Run);
    0.setVisible(gui_Benchs_Cam_Cancel);
    0.setVisible(gui_Benchs_Cam_Prog1);
    0.setVisible(gui_Benchs_Cam_Prog2);
    $benchmarks::originalMetrics.setValue(gui_Benchs_Metrics_Menu1);
    gui_Benchs_Metrics_Menu1.getValue().onSelect(gui_Benchs_Metrics_Menu1, 0);
    if (!(benchmarks::isInteractive())) {
        return;
    }
    setClipboard($benchmarks::camera::resultString);
    benchmarks::MessageBoxOK("Benchmark Results", ".. are now in the clipboard,\n(and in the console.log)");
};
function benchmarksGui::cancelCameraTests(%this) {
    1.setVisible(gui_Benchs_Cam_Run);
    0.setVisible(gui_Benchs_Cam_Cancel);
    0.setVisible(gui_Benchs_Cam_Prog1);
    0.setVisible(gui_Benchs_Cam_Prog2);
    benchmarks::cancelCameraTests();
};
function benchmarksGui::updateProgressBars(%this) {
    (($benchmarks::camera::curPoint + 1.0) / cameraTestsGroup.getCount()).setValue(gui_Benchs_Cam_Prog1);
    (($benchmarks::camera::repsDone + 1.0) / $pref::benchmarks::fps::reps).setValue(gui_Benchs_Cam_Prog2);
};
function benchmarksGui::addNewCameraTestPoint1(%this) {
    0.setVisible(gui_Benchs_Cam_Add1);
    1.setVisible(gui_Benchs_Cam_Add2);
    1.setVisible(gui_Benchs_Cam_Add3);
    0.setVisible(gui_Benchs_Cam_Name);
    1.setVisible(gui_Benchs_Cam_NameIn);
    gui_Benchs_Cam_NameIn.selectAll();
    0.setVisible(gui_Benchs_Cam_Prog1);
    0.setVisible(gui_Benchs_Cam_Prog2);
    1.makeFirstResponder(gui_Benchs_Cam_NameIn);
    10000.setSelection(gui_Benchs_Cam_NameIn, 0);
};
function benchmarksGui::addNewCameraTestPoint2(%this) {
    1.setVisible(gui_Benchs_Cam_Add1);
    0.setVisible(gui_Benchs_Cam_Add2);
    0.setVisible(gui_Benchs_Cam_Add3);
    1.setVisible(gui_Benchs_Cam_Name);
    0.setVisible(gui_Benchs_Cam_NameIn);
    benchmarks::addNewCameraTestPoint(gui_Benchs_Cam_NameIn.getValue());
};
function benchmarksGui::addNewCameraTestPoint3(%this) {
    1.setVisible(gui_Benchs_Cam_Add1);
    0.setVisible(gui_Benchs_Cam_Add2);
    0.setVisible(gui_Benchs_Cam_Add3);
    1.setVisible(gui_Benchs_Cam_Name);
    0.setVisible(gui_Benchs_Cam_NameIn);
};
