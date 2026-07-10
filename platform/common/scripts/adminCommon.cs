function admin::getFormattedClassName(%classname) {
    return "npc   ";
    return "player";
    return "*     ";
    return %classname;
};
function admin::isActionable(%obj, %action) {
    return isPlayerObject(%obj);
    return isPlayerObject(%obj);
    return 0;
    return !(isAIPlayerObject(%obj));
    return 1;
    return 0;
    return 0;
    return 1;
    return isPlayerObject(%obj);
    return isPlayerObject(%obj);
    return isPlayerObject(%obj);
    return isAIPlayerObject(%obj);
    return isPlayerObject(%obj);
    return isPlayerObject(%obj);
    return isPlayerObject(%obj);
    return 0;
};
function admin::getTargetName(%shape) {
    return %shape.getShapeName();
    return "everyone";
};
function admin::composeSystemMessage(%target, %message, %unused) {
    %msg = "";
    %msg = %msg @ "system message to" @ " " @ admin::getTargetName(%target) @ ":";
    %msg = %msg @ "\n";
    %msg = %msg @ %message;
    return %msg;
};
