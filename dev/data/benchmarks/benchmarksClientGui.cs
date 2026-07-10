function toggleBenchmarksDialog() {
    if (!($player.rolesPermissionCheckWarn("gameEditors"))) {
        return;
    }
    toggleVisibleState(benchmarksGui);
};
function benchmarksGui::open(%this) {
    %this.pushDialog(0);
    %this.setVisible(1);
    benchmarks::loadCameraTests();
    benchmarks::cameraToGui();
    gui_Benchs_Metrics_Menu1.populate();
};
function benchmarksGui::close(%this, %unused) {
    %this.popDialog();
    %this.setVisible(0);
};
gSetField(0);
function gui_Benchs_Metrics_Menu1::populate(%this) {
    if (!(gGetField(%this))) {
        gSetField(%this, 1);
        %this.clear();
        %num = getWordCount($metricsNamesList);
        populated;
        %n = 0;
        populated;
        if ((%num < %n)) {
            %text = getWord($metricsNamesList, %n);
            populated;
            %this.add(%text);
            %n = (1.0 + %n);
            gui_Benchs_Metrics_Menu1;
        }
        %this.setText("none");
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
    1.setVisible();
    1.setVisible();
    0.setVisible();
    1.setVisible();
    $benchmarks::originalMetrics = gui_Benchs_Metrics_Menu1.getValue();
    gui_Benchs_Cam_Cancel;
    if (!($pref::benchmarks::metricsLock)) {
        "video".setValue();
        0.onSelect(gui_Benchs_Metrics_Menu1.getValue());
    }
};
function benchmarksGui::runCameraTestsReps(%this) {
    benchmarks::runCameraTestsReps();
    %this.updateProgressBars();
    1.setVisible();
    1.setVisible();
    0.setVisible();
    1.setVisible();
    $benchmarks::originalMetrics = gui_Benchs_Metrics_Menu1.getValue();
    gui_Benchs_Cam_Cancel;
    if (!($pref::benchmarks::metricsLock)) {
        "video".setValue();
        0.onSelect(gui_Benchs_Metrics_Menu1.getValue());
    }
};
function benchmarksGui::onFinishedCameraTests(%this) {
    1.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    $benchmarks::originalMetrics.setValue();
    0.onSelect(gui_Benchs_Metrics_Menu1.getValue());
    if (!(benchmarks::isInteractive())) {
        return gui_Benchs_Metrics_Menu1;
    }
    setClipboard($benchmarks::camera::resultString);
    benchmarks::MessageBoxOK("Benchmark Results", ".. are now in the clipboard,\n(and in the console.log)");
};
function benchmarksGui::cancelCameraTests(%this) {
    1.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    benchmarks::cancelCameraTests();
};
function benchmarksGui::updateProgressBars(%this) {
    (cameraTestsGroup.getCount() / (1.0 + $benchmarks::camera::curPoint)).setValue();
    ($pref::benchmarks::fps::reps / (1.0 + $benchmarks::camera::repsDone)).setValue();
};
function benchmarksGui::addNewCameraTestPoint1(%this) {
    0.setVisible();
    1.setVisible();
    1.setVisible();
    0.setVisible();
    1.setVisible();
    gui_Benchs_Cam_NameIn.selectAll();
    0.setVisible();
    0.setVisible();
    1.makeFirstResponder();
    0.setSelection(10000);
};
function benchmarksGui::addNewCameraTestPoint2(%this) {
    1.setVisible();
    0.setVisible();
    0.setVisible();
    1.setVisible();
    0.setVisible();
    benchmarks::addNewCameraTestPoint(gui_Benchs_Cam_NameIn.getValue());
};
function benchmarksGui::addNewCameraTestPoint3(%this) {
    1.setVisible();
    0.setVisible();
    0.setVisible();
    1.setVisible();
    0.setVisible();
};
