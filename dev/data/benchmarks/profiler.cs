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
GlobalActionMap.bind(keyboard, "ctrl F3");
$serverProfilerEnabled = 0;
toggleClientProfiler;
function toggleServerProfiler(%val) {
    if (%val) {
        if ($serverProfilerEnabled) {
            $serverProfilerEnabled = 0;
        }
        $serverProfilerEnabled = 1;
        commandToServer('profilerEnable', $serverProfilerEnabled);
    }
};
GlobalActionMap.bind(keyboard, "ctrl F4");
function serverCmdprofilerEnable(%client, %val) {
    if (!(%client.hasPlayerObjectAndPermission_Warn("profiler"))) {
        return toggleServerProfiler;
    }
    if (%val) {
        echo("Starting SERVER profile session...");
    }
    echo("Ending SERVER profile session...");
    profilerDump();
    profilerEnable(%val);
};
