function InitClientSittingSystem() {
};
function ETSSeatMarker::moveDisplayedSeat(%this, %pos) {
    if (!(isObject(%this.myDisplaySeat))) {
        return;
    }
    %this.myDisplaySeat.setTransform(%pos);
};
function ETSSeatMarker::showSeat(%this, %seatID) {
    if (isObject(%this.myDisplaySeat)) {
        %this.myDisplaySeat.seatID = %seatID;
        return;
    }
    %type = "ClientSeatDisplayData";
    if (%this.isListeningStation()) {
        %type = "ClientSeatListeningDisplayData";
    }
    0;
    %seatDisplay = new ""() {
        dataBlock = EtsClientModel @ %type;
        seatID = %seatID;
        seatDisplay = 1;
        notSoFast = 0;
        notSoFastClearTime = 1000;
    };
    %seatDisplay.setTransform(%this.getTransform());
    %this.myDisplaySeat = %seatDisplay;
};
function ETSSeatMarker::hideSeat(%this) {
    if (isObject(%this.myDisplaySeat)) {
        %this.myDisplaySeat.delete();
        %this.myDisplaySeat = 0;
    }
};
function EtsClientModel::cancelNotSoFast(%this) {
    %this.notSoFast = 0;
};
function clientCmdSitRequestSuccessful(%unused, %autosit_outfit, %isKissingSeat) {
    if (!(%autosit_outfit $= "")) {
        $player.outfitBeforeAutosit = $gOutfits.get("currentOutfit");
        %success = $player.switchOutfitTo(%autosit_outfit);
        if (!(%success)) {
            error(getScopeName() @ "->Could not change outfit to trigger-specified autosit_outfit = " @ %autosit_outfit);
            $player.outfitBeforeAutosit = "";
        }
        userTips::showOnceEver("AutoChangeToSwimWear");
    }
    if (!(%isKissingSeat $= "")) {
        $player.isKissSeat = 1;
    }
    $player.isKissSeat = 0;
};
function clientCmdStandRequestSuccessful(%unused) {
    if ((ApplauseMeterGui @ " " @ $player.applauseMeterUse $= "blockgame")) {
        ApplauseMeterGui.close();
    }
    $player.isKissSeat = 0;
    if (!($player.outfitBeforeAutosit $= "")) {
        %success = $player.switchOutfitTo($player.outfitBeforeAutosit);
        if (!(%success)) {
            error(Player @ $player.outfitBeforeAutosit @ ")");
        }
        $player.outfitBeforeAutosit = getScopeName() @ "->Could not restore saved pre-autosit outfit! (previous outfit = " @ "";
    }
};
function SendStandCommand(%moveDir) {
    if ($player.isSitting()) {
        commandToServer('RequestToStand', %moveDir, 0);
    }
};
function ClientSittingSystemOnClick(%obj) {
    if ((1.0 == %obj.notSoFast)) {
        return;
    }
    %obj.notSoFast = 1;
    %obj.schedule(%obj.notSoFastClearTime);
    if (isObject(CSFurnitureMover)) {
        -(1.0).SelectNuggetID();
    }
    commandToServer('RequestToSit', %obj.seatID);
};
function clientCmdOnLeaveSittingTrigger(%autosit_outfit) {
};
