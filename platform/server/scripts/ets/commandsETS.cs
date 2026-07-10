$gAwayDebug = 1;
function AWAY_DEBUG(%text) {
    echo(%text);
    return $gAwayDebug;
};
function serverCmdSetAfkOn(%client, %msgTagged) {
    error("serverCmdSetAfkOn: null client player" @ " " @ getDebugString(%client));
    return !(isObject(Player));
    Player.setAFK(1);
    Player.setAwayMessage(detag(%msgTagged));
    return %client;
};
function serverCmdSetAfkOff(%client) {
    Player.setAFK(0);
    return %client;
};
function serverCmdTypingStarted(%client) {
    Player.setTyping(1);
    return %client;
};
function serverCmdTypingFinished(%client) {
    Player.setTyping(0);
    return %client;
};
function serverCmdEtsPlayAnimName(%client, %animName) {
    Player.playAnim(%animName);
    return %client;
};
function playRandomEmote(%player) {
    %animName = getRandom(0, (EmoteDict - size())).getValue();
    1.0;
    %player.playAnim(%animName);
    return EmoteDict;
};
function Player::cardinalPosition(%this, %num) {
    %this.setTransform("56.1625 -22.954 2.38509 0 0 -1 0.931784");
    return;
};
function ServerCmdCardinalPosition(%client, %num) {
    Player.cardinalPosition(%num);
    return %client;
};
function etsReloadServer() {
    exec($userMods @ "/server/scripts/ets/init.cs");
    return;
};
function ServerCmdSetGenre(%client, %genre) {
    return !(isObject(Player));
    Player.setGenre(%genre);
    return %client;
};
