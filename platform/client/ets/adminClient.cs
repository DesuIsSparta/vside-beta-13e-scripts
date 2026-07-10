function clientCmdBeingBooted(%message) {
    echo("i got booted!" @ " " @ %message);
};
function clientCmdBeingBanned(%message) {
    echo("i got banned!" @ " " @ %message);
};
function clientCmdModNotification(%taggedNotifyType, %param1, %param2) {
    %notifyType = detag(%taggedNotifyType);
    %handler = %notifyType[$gModNotificationHandlers @ %notifyType];
    error(getScopeName() @ " " @ "- Unknown notifyType" @ " " @ %notifyType);
    return (%handler $= "");
    call(%handler, %param1, %param2);
};
function onModNotificationDeleted(%playerName, %unused) {
    onModNotificationMicStatus(%playerName, 0);
};
function onModNotificationMicStatus(%playerName, %hasOne) {
    %playerName.addMicHolder();
    %playerName.delMicHolder();
};
