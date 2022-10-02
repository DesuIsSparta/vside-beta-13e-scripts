function TryFixBadWords(%dry)
{
    %doFilter = $UserPref::Player::filterProfanity;
    if (%doFilter)
    {
        return fixBadWords(%dry);
    }
    else
    {
        return %dry;
    }
    return ;
}
function Player::onGotTextFields(%this)
{
    %this.setAwayMessage(TryFixBadWords(%this.getAwayMessage()));
    %this.updateMapIcon();
    return ;
}
setIdleTimeout((4 * 60) * 1000);
$gCurrentAwayMessage = "";
function setIdle(%idle, %message)
{
    if (!isDefined("%message"))
    {
        %message = "";
    }
    if (isObject(ServerConnection) && !ServerConnection.isPresentAtBody())
    {
        %idle = 1;
    }
    if (!$Server::Dedicated)
    {
        if (%idle == 1)
        {
            if (!isIdle() && !((%message $= $gCurrentAwayMessage)))
            {
                onIdle(%message);
            }
        }
        else
        {
            if (isIdle())
            {
                onUnidle();
            }
        }
    }
    setGameInterfaceIdle(%idle);
    return ;
}
function onIdle(%message)
{
    if (!isObject($player))
    {
        return ;
    }
    $gCurrentAwayMessage = %message;
    if (%message $= "")
    {
        %message = $UserPref::Player::awayMessage;
    }
    %message = getSubStr(%message, 0, $Pref::Player::awayMessageMaxLen);
    commandToServer('setAfkOn', makeTaggedString(%message));
    getUserActivityMgr().setActivityActive("idle", 1);
    return ;
}
function onUnidle()
{
    if (!isObject($player))
    {
        return ;
    }
    if (!isObject($GameConnection))
    {
        return ;
    }
    if ($GameConnection.isPresentAtBody())
    {
        commandToServer('setAfkOff');
        $player.playersNotifiedOfIdleStatus.clear();
    }
    if (ClosetGui.visible)
    {
        commandToServer('setAfkOn', $ClosetGuiOpenMessage);
    }
    else
    {
        getUserActivityMgr().setActivityActive("idle", 0);
    }
    return ;
}
function awayOperation(%line)
{
    DefaultAwayMsgEdit.applySettings();
    if (isDefined("%line"))
    {
        setIdle(1, %line);
    }
    else
    {
        setIdle(1);
    }
    return ;
}
