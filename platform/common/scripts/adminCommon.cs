function admin::getFormattedClassName(%classname) {
    if ((%classname $= "AIPlayer")) {
        return "npc   ";
    }
    if ((%classname $= "Player")) {
        return "player";
    }
    if ((%classname $= "special")) {
        return "*     ";
    }
    return %classname;
};
function admin::isActionable(%obj, %action) {
    if ((%action $= "Boot")) {
        return isPlayerObject(%obj);
    }
    if ((%action $= "BootQuiet")) {
        return isPlayerObject(%obj);
    }
    if ((%action $= "Ban")) {
        if (!(isPlayerObject(%obj))) {
            return 0;
        }
        return !(isAIPlayerObject(%obj));
    }
    if ((%action $= "Message")) {
        if ((0.0 == %obj)) {
            return 1;
        }
        if (!(isPlayerObject(%obj))) {
            return 0;
        }
        if (isAIPlayerObject(%obj)) {
            return 0;
        }
        return 1;
    }
    if ((%action $= "Summon")) {
        return isPlayerObject(%obj);
    }
    if ((%action $= "Snoop Toggle")) {
        return isPlayerObject(%obj);
    }
    if ((%action $= "Respawn")) {
        return isPlayerObject(%obj);
    }
    if ((%action $= "Throw Voice")) {
        return isAIPlayerObject(%obj);
    }
    if ((%action $= "Teleport To")) {
        return isPlayerObject(%obj);
    }
    if ((%action $= "Fly To")) {
        return isPlayerObject(%obj);
    }
    if ((%action $= "Track")) {
        return isPlayerObject(%obj);
    }
    return 0;
};
function admin::getTargetName(%shape) {
    if (isObject(%shape)) {
        return %shape.getShapeName();
    }
    return "everyone";
};
function admin::composeSystemMessage(%target, %message, %unused) {
    %msg = "";
    %msg = %msg @ "system message to" @ " " @ admin::getTargetName(%target) @ ":";
    %msg = %msg @ "\n";
    %msg = %msg @ %message;
    return %msg;
};
