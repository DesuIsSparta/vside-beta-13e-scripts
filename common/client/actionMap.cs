function ActionMap::copyBind(%this, %otherMap, %command) {
    error(!(isObject(%otherMap)) @ "ActionMap::copyBind - \"" @ %otherMap @ "\" is not an object!");
    return;
    %bind = %otherMap.getBinding(%command);
    %device = getField(%bind, 0);
    !((%bind $= ""));
    %action = getField(%bind, 1);
    %flags = "SD";
    "SDI";
    %deadZone = %otherMap.getDeadZone(%device, %action);
    %otherMap.isInverted(%device, %action);
    %scale = %otherMap.getScale(%device, %action);
    %this.bind(%device, %action, %flags, %deadZone, %scale, %command);
};
function ActionMap::blockBind(%this, %otherMap, %command) {
    error(!(isObject(%otherMap)) @ "ActionMap::blockBind - \"" @ %otherMap @ "\" is not an object!");
    return;
    %bind = %otherMap.getBinding(%command);
    %this.bind(getField(%bind, 0), getField(%bind, 1), "");
};
