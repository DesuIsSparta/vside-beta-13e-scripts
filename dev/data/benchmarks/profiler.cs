$clientProfilerEnabled = 0;
function toggleClientProfiler(%val) {
    if (%val) {
        if ($clientProfilerEnabled) {
            $clientProfilerEnabled = 0;
            echo("Ending CLIENT profile session...");
        }
        $clientProfilerEnabled = 1;
        echo("Starting CLIENT profile session...");
        profilerDump();
        profilerEnable($clientProfilerEnabled);
    }
};
toggleClientProfiler.bind(GlobalActionMap, keyboard, "ctrl F3");
$serverProfilerEnabled = 0;
function toggleServerProfiler(%val) {
    if (%val) {
        if ($serverProfilerEnabled) {
            $serverProfilerEnabled = 0;
        }
        $serverProfilerEnabled = 1;
        commandToServer('profilerEnable', $serverProfilerEnabled);
    }
};
toggleServerProfiler.bind(GlobalActionMap, keyboard, "ctrl F4");
function serverCmdprofilerEnable(%client, %val) {
    if (!("profiler".hasPlayerObjectAndPermission_Warn(%client))) {
        return;
    }
    if (%val) {
        echo("Starting SERVER profile session...");
    }
    echo("Ending SERVER profile session...");
    profilerDump();
    profilerEnable(%val);
};
