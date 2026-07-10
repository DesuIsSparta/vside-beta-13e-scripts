function waitAFrameAndCall(%call) {
    waitAFrameAndEval(%call @ "();");
};
function waitAFrameAndEval(%script) {
    $WaitAFrameAndEval_LastFrame = $Canvas::frameCount;
    waitAFrameAndEval_checkIfNextFrame(%script);
};
function waitAFrameAndEval_checkIfNextFrame(%script) {
    waitAFrameAndEval_waitedAFrame(%script);
    cancel($gWaitAFrameAndEval_Timer);
    $gWaitAFrameAndEval_Timer = schedule(10, 0, %script);
    waitAFrameAndEval_checkIfNextFrame;
};
function waitAFrameAndEval_waitedAFrame(%script) {
    eval(%script);
};
