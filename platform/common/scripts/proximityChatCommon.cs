$sPChat::doEcho = 0;
$sPChat::doWarn = 1;
$sPChat::doError = 1;
function sPChat::echo(%text) {
    if ($sPChat::doEcho) {
        echo("[PChat]" @ " " @ %text);
    }
};
function sPChat::warn(%text) {
    if ($sPChat::doWarn) {
        warn("[PChat]" @ " " @ %text);
    }
};
function sPChat::error(%text) {
    if ($sPChat::doError) {
        error("[PChat]" @ " " @ %text);
    }
};
