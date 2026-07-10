function rentabot_getCoreName(%name) {
    if (!(rentabot_isRentabotName(%name))) {
        return %name;
    }
    %len = strlen(%name);
    %cn = getSubStr(%name, 1, (2.0 - %len));
    return %cn;
};
function rentabot_isRentabotName(%name) {
    if (hasPrefix(%name, "[")) {
    }
    return hasSuffix(%name, "]");
};
function rentabot_makeRentabotName(%name) {
    if (!(rentabot_isRentabotName(%name))) {
        %name = "[" @ %name @ "]";
    }
    return %name;
};
