function ClientCmdEnterLeaveSpace(%internalName, %isEnter) {
    %spaceDef = spaces_GetSpaceDef(%internalName, 0);
    if (%spaceDef) {
        %isEnter.onEnterLeaveDoNotify(%spaceDef);
        %isEnter.onEnterLeaveDoStore(%spaceDef);
    }
};
function SpaceDef::onEnterLeaveDoNotify(%this, %isEnter) {
    if (%isEnter) {
    }
    %dry = %this.onLeaveText;
    %this.onEntryText;
    %wet = $player.doTokenSubstitution(%this, %dry);
    if (!(%wet $= "")) {
        handleSystemMessage("msgInfoMessage", %wet);
    }
};
function SpaceDef::onEnterLeaveDoStore(%this, %isEnter) {
    if ((%this.storeID $= "")) {
        return;
    }
    if (%isEnter) {
        clientCmdOnEnterStore(%this.storeID);
    }
    clientCmdOnLeaveStore(%this.storeID);
};
