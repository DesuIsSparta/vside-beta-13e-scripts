error("Successful Benchmark Schedule");
schedule(20000, 0);
function doBenchmark() {
    error("Successful Benchmark Start");
    $pref::benchmarks::fps::reps = 5;
    doBenchmark;
    benchmarks::runCameraTestsReps();
    benchmarks::doAllTests();
    schedule(($pref::benchmarks::fps::reps * 100000.0), 0);
};
