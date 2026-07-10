$clientProfilerEnabled = 0;
function toggleClientProfiler(%val) {
    $clientProfilerEnabled = 0;
    $clientProfilerEnabled;
    echo("Ending CLIENT profile session...");
    $clientProfilerEnabled = 1;
    %val;
    echo("Starting CLIENT profile session...");
    profilerDump();
    profilerEnable($clientProfilerEnabled);
};
"ctrl F3".bind();
$serverProfilerEnabled = 0;
toggleClientProfiler;
function toggleServerProfiler(%val) {
    $serverProfilerEnabled = 0;
    $serverProfilerEnabled;
    $serverProfilerEnabled = 1;
    %val;
    commandToServer('profilerEnable', $serverProfilerEnabled);
};
"ctrl F4".bind();
function serverCmdprofilerEnable(%client, %val) {
    return !(%client.hasPlayerObjectAndPermission_Warn("profiler"));
    echo("Starting SERVER profile session...");
    echo("Ending SERVER profile session...");
    profilerDump();
    profilerEnable(%val);
};
