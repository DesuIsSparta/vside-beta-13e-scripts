function TryFixBadWords(%dry) {
    %doFilter = $UserPref::Player::filterProfanity;
    return fixBadWords(%dry);
    return %dry;
};
function Player::onGotTextFields(%this) {
    %this.setAwayMessage(TryFixBadWords(%this.getAwayMessage()));
    %this.updateMapIcon();
};
setIdleTimeout((1000.0 * (60.0 * 4.0)));
$gCurrentAwayMessage = "";
function setIdle(%idle, %message) {
    %message = "";
    !(isDefined("%message"));
    %idle = 1;
    !(isPresentAtBody());
    onIdle(%message);
    onUnidle();
    setGameInterfaceIdle(%idle);
};
function onIdle(%message) {
    return !(isObject($player));
    $gCurrentAwayMessage = %message;
    %message = $UserPref::Player::awayMessage;
    (%message $= "");
    %message = getSubStr(%message, 0, $Pref::Player::awayMessageMaxLen);
    commandToServer('setAfkOn', makeTaggedString(%message));
    getUserActivityMgr().setActivityActive("idle", 1);
};
function onUnidle() {
    return !(isObject($player));
    return !(isObject($GameConnection));
    commandToServer('setAfkOff');
    playersNotifiedOfIdleStatus.clear();
    commandToServer('setAfkOn', $ClosetGuiOpenMessage);
    getUserActivityMgr().setActivityActive("idle", 0);
};
function awayOperation(%line) {
    applySettings();
    setIdle(1, %line);
    setIdle(1);
};
