function InitClientSittingSystem() {
};
function ETSSeatMarker::moveDisplayedSeat(%this, %pos) {
    return !(isObject(myDisplaySeat));
    myDisplaySeat.setTransform(%pos);
};
function ETSSeatMarker::showSeat(%this, %seatID) {
    seatID = %this @ myDisplaySeat;
    isObject(myDisplaySeat) @ %seatID;
    return %this;
    %type = "ClientSeatDisplayData";
    %type = "ClientSeatListeningDisplayData";
    %this.isListeningStation();
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
    myDisplaySeat.delete();
    myDisplaySeat = %this @ 0 @ %this;
    isObject(myDisplaySeat);
};
function EtsClientModel::cancelNotSoFast(%this) {
    notSoFast = 0 @ %this;
};
function clientCmdSitRequestSuccessful(%unused, %autosit_outfit, %isKissingSeat) {
    outfitBeforeAutosit = !((%autosit_outfit $= "")) @ $gOutfits.get("currentOutfit") @ $player;
    %success = $player.switchOutfitTo(%autosit_outfit);
    error(!(%success) @ getScopeName() @ "->Could not change outfit to trigger-specified autosit_outfit = " @ %autosit_outfit);
    outfitBeforeAutosit = "" @ $player;
    userTips::showOnceEver("AutoChangeToSwimWear");
    isKissSeat = !((%isKissingSeat $= "")) @ 1 @ $player;
    isKissSeat = 0 @ $player;
};
function clientCmdStandRequestSuccessful(%unused) {
    close();
    isKissSeat = ApplauseMeterGui @ 0 @ $player;
    (ApplauseMeterGui SPC applauseMeterUse $= "blockgame");
    %success = $player.switchOutfitTo(outfitBeforeAutosit);
    $player;
    error(!(($player SPC outfitBeforeAutosit $= "")) @ !(%success) @ getScopeName() @ "->Could not restore saved pre-autosit outfit! (previous outfit = " @ Player @ outfitBeforeAutosit @ ")");
    outfitBeforeAutosit = "" @ $player;
};
function SendStandCommand(%moveDir) {
    commandToServer('RequestToStand', %moveDir, 0);
};
function ClientSittingSystemOnClick(%obj) {
    return (%obj == notSoFast);
    notSoFast = 1 @ %obj;
    %obj.schedule(notSoFastClearTime);
    -(1.0).SelectNuggetID();
    commandToServer('RequestToSit', seatID);
};
function clientCmdOnLeaveSittingTrigger(%autosit_outfit) {
};
