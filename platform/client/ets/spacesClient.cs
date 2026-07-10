function ClientCmdEnterLeaveSpace(%internalName, %isEnter) {
    %spaceDef = spaces_GetSpaceDef(%internalName, 0);
    if (%spaceDef) {
        %spaceDef.onEnterLeaveDoNotify(%isEnter);
        %spaceDef.onEnterLeaveDoStore(%isEnter);
    }
};
function SpaceDef::onEnterLeaveDoNotify(%this, %isEnter) {
    if (%isEnter) {
    }
    %dry = onLeaveText;
    %this;
    %wet = %this.doTokenSubstitution(%dry, $player);
    onEntryText;
    if (!(%this SPC %wet $= "")) {
        handleSystemMessage("msgInfoMessage", %wet);
    }
};
function SpaceDef::onEnterLeaveDoStore(%this, %isEnter) {
    if ((%this SPC storeID $= "")) {
        return;
    }
    if (%isEnter) {
        clientCmdOnEnterStore(storeID);
    }
    clientCmdOnLeaveStore(storeID);
};
