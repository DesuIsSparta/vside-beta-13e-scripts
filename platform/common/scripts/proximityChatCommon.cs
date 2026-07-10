$sPChat::doEcho = 0;
$sPChat::doWarn = 1;
$sPChat::doError = 1;
function sPChat::echo(%text) {
    echo("[PChat]" @ " " @ %text);
};
function sPChat::warn(%text) {
    warn("[PChat]" @ " " @ %text);
};
function sPChat::error(%text) {
    error("[PChat]" @ " " @ %text);
};
