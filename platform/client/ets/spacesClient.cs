function ClientCmdEnterLeaveSpace(%internalName, %isEnter)
{
    %spaceDef = spaces_GetSpaceDef(%internalName, 0);
    if (%spaceDef)
    {
        %spaceDef.onEnterLeaveDoNotify(%isEnter);
        %spaceDef.onEnterLeaveDoStore(%isEnter);
    }
    return ;
}
function SpaceDef::onEnterLeaveDoNotify(%this, %isEnter)
{
    %dry = %isEnter ? %this : %this;
    %wet = %this.doTokenSubstitution(%dry, $player);
    if (!(%wet $= ""))
    {
        handleSystemMessage("msgInfoMessage", %wet);
    }
    return ;
}
function SpaceDef::onEnterLeaveDoStore(%this, %isEnter)
{
    if (%this.storeID $= "")
    {
        return ;
    }
    if (%isEnter)
    {
        clientCmdOnEnterStore(%this.storeID);
    }
    else
    {
        clientCmdOnLeaveStore(%this.storeID);
    }
    return ;
}
