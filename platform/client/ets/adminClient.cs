function clientCmdBeingBooted(%message) {
    echo("i got booted!" @ " " @ %message);
};
function clientCmdBeingBanned(%message) {
    echo("i got banned!" @ " " @ %message);
};
$gModNotificationHandlers["deleted"] = "onModNotificationDeleted";
$gModNotificationHandlers["micStatus"] = "onModNotificationMicStatus";
$gModNotificationHandlers["cussing"] = "onModNotificationCussing";
function clientCmdModNotification(%taggedNotifyType, %param1, %param2) {
    %notifyType = detag(%taggedNotifyType);
    %handler = %notifyType[$gModNotificationHandlers @ %notifyType];
    if ((%handler $= "")) {
        error(getScopeName() @ " " @ "- Unknown notifyType" @ " " @ %notifyType);
        return;
    }
    call(%handler, %param1, %param2);
};
function onModNotificationDeleted(%playerName, %unused) {
    onModNotificationMicStatus(%playerName, 0);
};
function onModNotificationMicStatus(%playerName, %hasOne) {
    if (%hasOne) {
        micPanel.addMicHolder(%playerName);
    } else {
        micPanel.delMicHolder(%playerName);
    }
};
