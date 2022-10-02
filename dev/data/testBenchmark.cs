error("Successful Benchmark Schedule");
schedule(20000, 0, doBenchmark);
function doBenchmark()
{
    error("Successful Benchmark Start");
    $pref::benchmarks::fps::reps = 5;
    benchmarks::runCameraTestsReps();
    benchmarks::doAllTests();
    schedule(100000 * $pref::benchmarks::fps::reps, 0, quit);
    return ;
}
