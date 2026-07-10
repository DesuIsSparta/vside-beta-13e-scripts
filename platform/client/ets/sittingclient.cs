function InitClientSittingSystem() {
};
function ETSSeatMarker::moveDisplayedSeat(%this, %pos) {
    if (!(isObject(%this.myDisplaySeat))) {
        return;
    }
    %pos.setTransform(%this.myDisplaySeat);
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
    %seatDisplay = new EtsClientModel("") {
        dataBlock = %type;
        seatID = %seatID;
        seatDisplay = 1;
        notSoFast = 0;
        notSoFastClearTime = 1000;
    };
    %this.getTransform().setTransform(%seatDisplay);
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
        $player.outfitBeforeAutosit = "currentOutfit".get($gOutfits);
        %success = %autosit_outfit.switchOutfitTo($player);
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
    if ((ApplauseMeterGui.applauseMeterUse $= "blockgame")) {
        ApplauseMeterGui.close();
    }
    $player.isKissSeat = 0;
    if (!($player.outfitBeforeAutosit $= "")) {
        %success = $player.outfitBeforeAutosit.switchOutfitTo($player);
        if (!(%success)) {
            error(getScopeName() @ "->Could not restore saved pre-autosit outfit! (previous outfit = " @ Player.outfitBeforeAutosit @ ")");
        }
        $player.outfitBeforeAutosit = "";
    }
};
function SendStandCommand(%moveDir) {
    if ($player.isSitting()) {
        commandToServer('RequestToStand', %moveDir, 0);
    }
};
function ClientSittingSystemOnClick(%obj) {
    if ((%obj.notSoFast == 1.0)) {
        return;
    }
    %obj.notSoFast = 1;
    cancelNotSoFast.schedule(%obj, %obj.notSoFastClearTime);
    if (isObject(CSFurnitureMover)) {
        -(1.0).SelectNuggetID(CSFurnitureMover);
    }
    commandToServer('RequestToSit', %obj.seatID);
};
function clientCmdOnLeaveSittingTrigger(%autosit_outfit) {
};
