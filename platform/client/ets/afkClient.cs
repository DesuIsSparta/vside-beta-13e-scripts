function TryFixBadWords(%dry) {
    %doFilter = $UserPref::Player::filterProfanity;
    if (%doFilter) {
        return fixBadWords(%dry);
    }
    return %dry;
};
function Player::onGotTextFields(%this) {
    %this.setAwayMessage(TryFixBadWords(%this.getAwayMessage()));
    %this.updateMapIcon();
};
setIdleTimeout((1000.0 * (60.0 * 4.0)));
$gCurrentAwayMessage = "";
function setIdle(%idle, %message) {
    if (!(isDefined("%message"))) {
        %message = "";
    }
    if (isObject()) {
    }
    if (!(isPresentAtBody())) {
        %idle = 1;
        ServerConnection;
    }
    if (!($Server::Dedicated)) {
        if ((1.0 == %idle)) {
            if (!(isIdle())) {
            }
            if (!(ServerConnection SPC %message $= $gCurrentAwayMessage)) {
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
    getUserActivityMgr().setActivityActive("idle", 1);
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
        playersNotifiedOfIdleStatus.clear();
    }
    if (visible) {
        commandToServer('setAfkOn', $ClosetGuiOpenMessage);
    }
    getUserActivityMgr().setActivityActive("idle", 0);
};
function awayOperation(%line) {
    applySettings();
    if (isDefined("%line")) {
        setIdle(1, %line);
    }
    setIdle(1);
};
