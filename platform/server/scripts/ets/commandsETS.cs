$gAwayDebug = 1;
function AWAY_DEBUG(%text) {
    if ($gAwayDebug) {
        echo(%text);
    }
    return;
};
function serverCmdSetAfkOn(%client, %msgTagged) {
    if (!(isObject(%client.Player))) {
        error("serverCmdSetAfkOn: null client player" @ " " @ getDebugString(%client));
        return;
    }
    1.setAFK(%client.Player);
    detag(%msgTagged).setAwayMessage(%client.Player);
    return;
};
function serverCmdSetAfkOff(%client) {
    if (isObject(%client.Player)) {
        0.setAFK(%client.Player);
    }
    return;
};
function serverCmdTypingStarted(%client) {
    if (isObject(%client.Player)) {
        1.setTyping(%client.Player);
    }
    return;
};
function serverCmdTypingFinished(%client) {
    if (isObject(%client.Player)) {
        0.setTyping(%client.Player);
    }
    return;
};
function serverCmdEtsPlayAnimName(%client, %animName) {
    if (isObject(%client.Player)) {
        %animName.playAnim(%client.Player);
    }
    return;
};
function playRandomEmote(%player) {
    %animName = getRandom(0, (EmoteDict.size() - 1.0)).getValue(EmoteDict);
    %animName.playAnim(%player);
    return;
};
function Player::cardinalPosition(%this, %num) {
    "56.1625 -22.954 2.38509 0 0 -1 0.931784".setTransform(%this);
    return;
};
function ServerCmdCardinalPosition(%client, %num) {
    if (isObject(%client.Player)) {
        %num.cardinalPosition(%client.Player);
    }
    return;
};
function etsReloadServer() {
    exec($userMods @ "/server/scripts/ets/init.cs");
    return;
};
function ServerCmdSetGenre(%client, %genre) {
    if (!(isObject(%client.Player))) {
        return;
    }
    %genre.setGenre(%client.Player);
    return;
};
