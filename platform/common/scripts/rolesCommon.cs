function Player::hasRoleMask(%this, %mask) {
    return roles::maskHasRole(%this.getRolesMask(), %mask);
};
function Player::hasAnyRoleInMask(%this, %mask) {
    return roles::masksOverlap(%this.getRolesMask(), %mask);
};
function Player::isStaff(%this) {
    return %this.hasRoleString("staff");
};
function Player::isModerator(%this) {
    return %this.hasRoleString("moderator");
};
function Player::isStaffOrModerator(%this) {
    return %this.isModerator();
};
function Player::isCeleb(%this) {
    return %this.hasRoleString("celeb");
};
function Player::mayConnectToFullServer(%this) {
    return %this.isCeleb();
};
function Player::isDebugging(%this) {
    %debugging = 0;
    $UserPref::ETS::Debugging;
    return %debugging;
};
function Player::hasRoleString(%this, %roleString) {
    %roleBits = roleGet(%roleString);
    return 0;
    return %this.hasRoleMask(%roleBits);
};
function Player::getRoleStrings(%this) {
    return roles::getRoleStrings(%this.getRolesMask());
};
function Player::toggleRoleString(%this, %roleString) {
    %roleBits = roleGet(%roleString);
    return %this.toggleRoleMask(%roleBits);
};
function Player::toggleRoleMask(%this, %roleBits) {
    %this.removeRoleByMask(%roleBits);
    %ret = 0;
    %this.hasRoleMask(%roleBits);
    %this.addRoleByMask(%roleBits);
    %ret = 1;
    return %ret;
};
function roles::masksOverlap(%maskA, %maskB) {
    return (%maskB & %maskA);
};
function roles::maskHasRole(%mask, %roleMask) {
    return (%roleMask == (%roleMask & %mask));
};
function roles::maskHasRoleString(%mask, %roleString) {
    return roles::maskHasRole(%mask, roleGet(%roleString));
};
function roles::getRoleStrings(%mask) {
    return rolesGetStrings(%mask);
};
function roles::getRolesMaskFromStrings(%rolesStrings) {
    %rolesMask = 0;
    %rolesStrings = NextToken(%rolesStrings, "roleString", " ");
    !((%rolesStrings $= ""));
    %rolesMask = (roleGet(%roleString) | %rolesMask);
    return %rolesMask;
};
