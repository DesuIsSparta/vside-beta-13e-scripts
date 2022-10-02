$sPChat::doEcho = 0;
$sPChat::doWarn = 1;
$sPChat::doError = 1;
function sPChat::echo(%text)
{
    if ($sPChat::doEcho)
    {
        echo("[PChat]" SPC %text);
    }
    return ;
}
function sPChat::warn(%text)
{
    if ($sPChat::doWarn)
    {
        warn("[PChat]" SPC %text);
    }
    return ;
}
function sPChat::error(%text)
{
    if ($sPChat::doError)
    {
        error("[PChat]" SPC %text);
    }
    return ;
}
