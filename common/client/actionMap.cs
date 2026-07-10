function ActionMap::copyBind(%this, %otherMap, %command) {
    if (!(isObject(%otherMap))) {
        error("ActionMap::copyBind - \"" @ %otherMap @ "\" is not an object!");
        return;
    }
    %bind = %command.getBinding(%otherMap);
    if (!(%bind $= "")) {
        %device = getField(%bind, 0);
        %action = getField(%bind, 1);
        %flags = %action.isInverted(%otherMap, %device) ? "SDI" : "SD";
        %deadZone = %action.getDeadZone(%otherMap, %device);
        %scale = %action.getScale(%otherMap, %device);
        %command.bind(%this, %device, %action, %flags, %deadZone, %scale);
    }
};
function ActionMap::blockBind(%this, %otherMap, %command) {
    if (!(isObject(%otherMap))) {
        error("ActionMap::blockBind - \"" @ %otherMap @ "\" is not an object!");
        return;
    }
    %bind = %command.getBinding(%otherMap);
    if (!(%bind $= "")) {
        "".bind(%this, getField(%bind, 0), getField(%bind, 1));
    }
};
