$clientProfilerEnabled = 0;
function toggleClientProfiler(%val)
{
    if (%val)
    {
        if ($clientProfilerEnabled)
        {
            $clientProfilerEnabled = 0;
            echo("Ending CLIENT profile session...");
        }
        else
        {
            $clientProfilerEnabled = 1;
            echo("Starting CLIENT profile session...");
        }
        profilerDump();
        profilerEnable($clientProfilerEnabled);
    }
    return ;
}
GlobalActionMap.bind(keyboard, "ctrl F3", toggleClientProfiler);
$serverProfilerEnabled = 0;
function toggleServerProfiler(%val)
{
    if (%val)
    {
        if ($serverProfilerEnabled)
        {
            $serverProfilerEnabled = 0;
        }
        else
        {
            $serverProfilerEnabled = 1;
        }
        commandToServer('profilerEnable', $serverProfilerEnabled);
    }
    return ;
}
GlobalActionMap.bind(keyboard, "ctrl F4", toggleServerProfiler);
function serverCmdprofilerEnable(%client, %val)
{
    if (!%client.hasPlayerObjectAndPermission_Warn("profiler"))
    {
        return ;
    }
    if (%val)
    {
        echo("Starting SERVER profile session...");
    }
    else
    {
        echo("Ending SERVER profile session...");
    }
    profilerDump();
    profilerEnable(%val);
    return ;
}
