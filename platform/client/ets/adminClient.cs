function clientCmdBeingBooted(%message)
{
    echo("i got booted!" SPC %message);
    return ;
}
function clientCmdBeingBanned(%message)
{
    echo("i got banned!" SPC %message);
    return ;
}
$gModNotificationHandlers["deleted"] = "onModNotificationDeleted";
$gModNotificationHandlers["micStatus"] = "onModNotificationMicStatus";
$gModNotificationHandlers["cussing"] = "onModNotificationCussing";
function clientCmdModNotification(%taggedNotifyType, %param1, %param2)
{
    %notifyType = detag(%taggedNotifyType);
    %handler = $gModNotificationHandlers[%notifyType];
    if (%handler $= "")
    {
        error(getScopeName() SPC "- Unknown notifyType" SPC %notifyType);
        return ;
    }
    call(%handler, %param1, %param2);
    return ;
}
function onModNotificationDeleted(%playerName, %unused)
{
    onModNotificationMicStatus(%playerName, 0);
    return ;
}
function onModNotificationMicStatus(%playerName, %hasOne)
{
    if (%hasOne)
    {
        micPanel.addMicHolder(%playerName);
    }
    else
    {
        micPanel.delMicHolder(%playerName);
    }
    return ;
}
