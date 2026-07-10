function Player::hasRoleMask(%this, %mask) {
    return roles::maskHasRole(%this.getRolesMask(), %mask);
};
function Player::hasAnyRoleInMask(%this, %mask) {
    if ((%mask == 0.0)) {
    }
    return roles::masksOverlap(%this.getRolesMask(), %mask);
};
function Player::isStaff(%this) {
    return "staff".hasRoleString(%this);
};
function Player::isModerator(%this) {
    return "moderator".hasRoleString(%this);
};
function Player::isStaffOrModerator(%this) {
    if (%this.isStaff()) {
    }
    return %this.isModerator();
};
function Player::isCeleb(%this) {
    return "celeb".hasRoleString(%this);
};
function Player::mayConnectToFullServer(%this) {
    if (%this.isStaff()) {
    }
    if (%this.isModerator()) {
    }
    return %this.isCeleb();
};
function Player::isDebugging(%this) {
    if (isDefined("$UserPref::ETS::Debugging")) {
    }
    %debugging = 0;
    $UserPref::ETS::Debugging;
    if (%this.isStaff()) {
    }
    return %debugging;
};
function Player::hasRoleString(%this, %roleString) {
    %roleBits = roleGet(%roleString);
    if ((%roleBits == 0.0)) {
        return 0;
    }
    return %roleBits.hasRoleMask(%this);
};
function Player::getRoleStrings(%this) {
    return roles::getRoleStrings(%this.getRolesMask());
};
function Player::toggleRoleString(%this, %roleString) {
    %roleBits = roleGet(%roleString);
    return %roleBits.toggleRoleMask(%this);
};
function Player::toggleRoleMask(%this, %roleBits) {
    if (%roleBits.hasRoleMask(%this)) {
        %roleBits.removeRoleByMask(%this);
        %ret = 0;
    }
    %roleBits.addRoleByMask(%this);
    %ret = 1;
    return %ret;
};
function roles::masksOverlap(%maskA, %maskB) {
    return (%maskA & %maskB);
};
function roles::maskHasRole(%mask, %roleMask) {
    return ((%mask & %roleMask) == %roleMask);
};
function roles::maskHasRoleString(%mask, %roleString) {
    return roles::maskHasRole(%mask, roleGet(%roleString));
};
function roles::getRoleStrings(%mask) {
    return rolesGetStrings(%mask);
};
function roles::getRolesMaskFromStrings(%rolesStrings) {
    %rolesMask = 0;
    while (!(%rolesStrings $= "")) {
        %rolesStrings = NextToken(%rolesStrings, "roleString", " ");
        %rolesMask = (%rolesMask | roleGet(%roleString));
    }
    return %rolesMask;
};
