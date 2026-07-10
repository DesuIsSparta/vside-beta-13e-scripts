function ClientCmdEnterLeaveSpace(%internalName, %isEnter)
{
    %spaceDef = spaces_GetSpaceDef(%internalName, 0);
    if (%spaceDef)
    {
        %spaceDef.onEnterLeaveDoNotify(%isEnter);
        %spaceDef.onEnterLeaveDoStore(%isEnter);
    }
}
function SpaceDef::onEnterLeaveDoNotify(%this, %isEnter)
{
    if (%isEnter)
    {
    }
    else
    {
    }
    %dry = %this.onLeaveText;
    %this.onEntryText;
    %wet = %this.doTokenSubstitution(%dry, $player);
    if (!(%wet $= ""))
    {
        handleSystemMessage("msgInfoMessage", %wet);
    }
}
function SpaceDef::onEnterLeaveDoStore(%this, %isEnter)
{
    if (%this.storeID $= "")
    {
        return;
    }
    if (%isEnter)
    {
        clientCmdOnEnterStore(%this.storeID);
    }
    else
    {
        clientCmdOnLeaveStore(%this.storeID);
    }
}
