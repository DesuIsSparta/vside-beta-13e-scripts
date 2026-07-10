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
"ctrl F3".bind(GlobalActionMap, keyboard);
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
"ctrl F4".bind(GlobalActionMap, keyboard);
function serverCmdprofilerEnable(%client, %val) {
    if (!("profiler".hasPlayerObjectAndPermission_Warn(%client))) {
        return toggleServerProfiler;
    }
    if (%val) {
        echo("Starting SERVER profile session...");
    }
    echo("Ending SERVER profile session...");
    profilerDump();
    profilerEnable(%val);
};
