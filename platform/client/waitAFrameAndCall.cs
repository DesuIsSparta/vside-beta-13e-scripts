function waitAFrameAndCall(%call)
{
    waitAFrameAndEval(%call @ "();");
    return ;
}
function waitAFrameAndEval(%script)
{
    $WaitAFrameAndEval_LastFrame = $Canvas::frameCount;
    waitAFrameAndEval_checkIfNextFrame(%script);
    return ;
}
function waitAFrameAndEval_checkIfNextFrame(%script)
{
    if ($WaitAFrameAndEval_LastFrame < $Canvas::frameCount)
    {
        waitAFrameAndEval_waitedAFrame(%script);
    }
    else
    {
        cancel($gWaitAFrameAndEval_Timer);
        $gWaitAFrameAndEval_Timer = schedule(10, 0, waitAFrameAndEval_checkIfNextFrame, %script);
    }
    return ;
}
function waitAFrameAndEval_waitedAFrame(%script)
{
    eval(%script);
    return ;
}
