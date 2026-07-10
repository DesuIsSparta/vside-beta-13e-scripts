function roles::maskhaspermission(%rolesMask, %permName) {
    return rolesPermissionCheck(%permName, %rolesMask);
};
function Player::isSuperMod(%this) {
    if ("moderator".hasRoleString(%this)) {
    }
    return "staff".hasRoleString(%this);
};
function isObjectAndHasPermission_Warn(%obj, %permName) {
    if (!(isObject(%obj))) {
        error(getScopeName() @ " " @ "- not an object." @ " " @ getDebugString(%obj) @ " " @ %permName @ " " @ getTrace());
        return 0;
    }
    return %permName.rolesPermissionCheckWarn(%obj);
};
function isObjectAndHasPermission_NoWarn(%obj, %permName) {
    if (!(isObject(%obj))) {
        error(getScopeName() @ " " @ "- not an object." @ " " @ getDebugString(%obj) @ " " @ %permName @ " " @ getTrace());
        return 0;
    }
    return %permName.rolesPermissionCheckNoWarn(%obj);
};
