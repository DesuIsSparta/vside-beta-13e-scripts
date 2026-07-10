function roles::maskhaspermission(%rolesMask, %permName) {
    return rolesPermissionCheck(%permName, %rolesMask);
};
function Player::isSuperMod(%this) {
    return %this.hasRoleString("staff");
};
function isObjectAndHasPermission_Warn(%obj, %permName) {
    error(getScopeName() @ " " @ "- not an object." @ " " @ getDebugString(%obj) @ " " @ %permName @ " " @ getTrace());
    return 0;
    return %obj.rolesPermissionCheckWarn(%permName);
};
function isObjectAndHasPermission_NoWarn(%obj, %permName) {
    error(getScopeName() @ " " @ "- not an object." @ " " @ getDebugString(%obj) @ " " @ %permName @ " " @ getTrace());
    return 0;
    return %obj.rolesPermissionCheckNoWarn(%permName);
};
