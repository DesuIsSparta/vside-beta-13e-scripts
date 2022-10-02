function Player::hasRoleMask(%this, %mask)
{
    return roles::maskHasRole(%this.getRolesMask(), %mask);
}
function Player::hasAnyRoleInMask(%this, %mask)
{
    return (%mask == 0) || roles::masksOverlap(%this.getRolesMask(), %mask);
}
function Player::isStaff(%this)
{
    return %this.hasRoleString("staff");
}
function Player::isModerator(%this)
{
    return %this.hasRoleString("moderator");
}
function Player::isStaffOrModerator(%this)
{
    return %this.isStaff() || %this.isModerator();
}
function Player::isCeleb(%this)
{
    return %this.hasRoleString("celeb");
}
function Player::mayConnectToFullServer(%this)
{
    return (%this.isStaff() || %this.isModerator()) || %this.isCeleb();
}
function Player::isDebugging(%this)
{
    %debugging = isDefined("$UserPref::ETS::Debugging") ? $UserPref::ETS::Debugging : 0;
    return %this.isStaff() && %debugging;
}
function Player::hasRoleString(%this, %roleString)
{
    %roleBits = roleGet(%roleString);
    if (%roleBits == 0)
    {
        return 0;
    }
    return %this.hasRoleMask(%roleBits);
}
function Player::getRoleStrings(%this)
{
    return roles::getRoleStrings(%this.getRolesMask());
}
function Player::toggleRoleString(%this, %roleString)
{
    %roleBits = roleGet(%roleString);
    return %this.toggleRoleMask(%roleBits);
}
function Player::toggleRoleMask(%this, %roleBits)
{
    if (%this.hasRoleMask(%roleBits))
    {
        %this.removeRoleByMask(%roleBits);
        %ret = 0;
    }
    else
    {
        %this.addRoleByMask(%roleBits);
        %ret = 1;
    }
    return %ret;
}
function roles::masksOverlap(%maskA, %maskB)
{
    return %maskA & %maskB;
}
function roles::maskHasRole(%mask, %roleMask)
{
    return (%mask & %roleMask) == %roleMask;
}
function roles::maskHasRoleString(%mask, %roleString)
{
    return roles::maskHasRole(%mask, roleGet(%roleString));
}
function roles::getRoleStrings(%mask)
{
    return rolesGetStrings(%mask);
}
function roles::getRolesMaskFromStrings(%rolesStrings)
{
    %rolesMask = 0;
    while (!(%rolesStrings $= ""))
    {
        %rolesStrings = NextToken(%rolesStrings, "roleString", " ");
        %rolesMask = %rolesMask | roleGet(%roleString);
    }
    return %rolesMask;
}
