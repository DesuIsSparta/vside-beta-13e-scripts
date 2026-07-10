function TryFixBadWords(%dry) {
    %doFilter = $UserPref::Player::filterProfanity;
    if (%doFilter) {
        return fixBadWords(%dry);
    }
    return %dry;
};
function Player::onGotTextFields(%this) {
    TryFixBadWords(%this.getAwayMessage()).setAwayMessage(%this);
    %this.updateMapIcon();
};
setIdleTimeout(((4.0 * 60.0) * 1000.0));
$gCurrentAwayMessage = "";
function setIdle(%idle, %message) {
    if (!(isDefined("%message"))) {
        %message = "";
    }
    if (isObject(ServerConnection)) {
    }
    if (!(ServerConnection.isPresentAtBody())) {
        %idle = 1;
    }
    if (!($Server::Dedicated)) {
        if ((%idle == 1.0)) {
            if (!(isIdle())) {
            }
            if (!(%message $= $gCurrentAwayMessage)) {
                onIdle(%message);
            }
        }
        if (isIdle()) {
            onUnidle();
        }
    }
    setGameInterfaceIdle(%idle);
};
function onIdle(%message) {
    if (!(isObject($player))) {
        return;
    }
    $gCurrentAwayMessage = %message;
    if ((%message $= "")) {
        %message = $UserPref::Player::awayMessage;
    }
    %message = getSubStr(%message, 0, $Pref::Player::awayMessageMaxLen);
    commandToServer('setAfkOn', makeTaggedString(%message));
    1.setActivityActive(getUserActivityMgr(), "idle");
};
function onUnidle() {
    if (!(isObject($player))) {
        return;
    }
    if (!(isObject($GameConnection))) {
        return;
    }
    if ($GameConnection.isPresentAtBody()) {
        commandToServer('setAfkOff');
        $player.playersNotifiedOfIdleStatus.clear();
    }
    if ($player.visible) {
        commandToServer('setAfkOn', $ClosetGuiOpenMessage);
    }
    0.setActivityActive(getUserActivityMgr(), "idle");
};
function awayOperation(%line) {
    DefaultAwayMsgEdit.applySettings();
    if (isDefined("%line")) {
        setIdle(1, %line);
    }
    setIdle(1);
};
