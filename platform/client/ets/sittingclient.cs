function InitClientSittingSystem() {
};
function ETSSeatMarker::moveDisplayedSeat(%this, %pos) {
    if (!(isObject(myDisplaySeat))) {
        return %this;
    }
    myDisplaySeat.setTransform(%pos);
};
function ETSSeatMarker::showSeat(%this, %seatID) {
    if (isObject(myDisplaySeat)) {
        seatID = %this @ myDisplaySeat;
        %this @ %seatID;
        return;
    }
    %type = "ClientSeatDisplayData";
    if (%this.isListeningStation()) {
        %type = "ClientSeatListeningDisplayData";
    }
    dataBlock = EtsClientModel @ new ""() @ %type;
    0;
    seatID = %seatID;
    seatDisplay = 1;
    notSoFast = 0;
    notSoFastClearTime = 1000;
    %seatDisplay = ;
    %seatDisplay.setTransform(%this.getTransform());
    myDisplaySeat = %seatDisplay @ %this;
};
function ETSSeatMarker::hideSeat(%this) {
    if (isObject(myDisplaySeat)) {
        myDisplaySeat.delete();
        myDisplaySeat = %this @ 0 @ %this;
        %this;
    }
};
function EtsClientModel::cancelNotSoFast(%this) {
    notSoFast = 0 @ %this;
};
function clientCmdSitRequestSuccessful(%unused, %autosit_outfit, %isKissingSeat) {
    if (!(%autosit_outfit $= "")) {
        outfitBeforeAutosit = $gOutfits.get("currentOutfit") @ $player;
        %success = $player.switchOutfitTo(%autosit_outfit);
        if (!(%success)) {
            error(getScopeName() @ "->Could not change outfit to trigger-specified autosit_outfit = " @ %autosit_outfit);
            outfitBeforeAutosit = "" @ $player;
        }
        userTips::showOnceEver("AutoChangeToSwimWear");
    }
    if (!(%isKissingSeat $= "")) {
        isKissSeat = 1 @ $player;
    }
    isKissSeat = 0 @ $player;
};
function clientCmdStandRequestSuccessful(%unused) {
    if ((ApplauseMeterGui SPC applauseMeterUse $= "blockgame")) {
        close();
    }
    isKissSeat = ApplauseMeterGui @ 0 @ $player;
    if (!($player SPC outfitBeforeAutosit $= "")) {
        %success = $player.switchOutfitTo(outfitBeforeAutosit);
        $player;
        if (!(%success)) {
            error(getScopeName() @ "->Could not restore saved pre-autosit outfit! (previous outfit = " @ Player @ outfitBeforeAutosit @ ")");
        }
        outfitBeforeAutosit = "" @ $player;
    }
};
function SendStandCommand(%moveDir) {
    if ($player.isSitting()) {
        commandToServer('RequestToStand', %moveDir, 0);
    }
};
function ClientSittingSystemOnClick(%obj) {
    if ((%obj == notSoFast)) {
        return 1.0;
    }
    notSoFast = 1 @ %obj;
    %obj.schedule(notSoFastClearTime);
    if (isObject()) {
        -(1.0).SelectNuggetID();
    }
    commandToServer('RequestToSit', seatID);
};
function clientCmdOnLeaveSittingTrigger(%autosit_outfit) {
};
