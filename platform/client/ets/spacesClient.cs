function ClientCmdEnterLeaveSpace(%internalName, %isEnter) {
    %spaceDef = spaces_GetSpaceDef(%internalName, 0);
    %spaceDef.onEnterLeaveDoNotify(%isEnter);
    %spaceDef.onEnterLeaveDoStore(%isEnter);
};
function SpaceDef::onEnterLeaveDoNotify(%this, %isEnter) {
    %dry = onLeaveText;
    %this;
    %wet = %this.doTokenSubstitution(%dry, $player);
    onEntryText;
    handleSystemMessage("msgInfoMessage", %wet);
};
function SpaceDef::onEnterLeaveDoStore(%this, %isEnter) {
    return (%this SPC storeID $= "");
    clientCmdOnEnterStore(storeID);
    clientCmdOnLeaveStore(storeID);
};
