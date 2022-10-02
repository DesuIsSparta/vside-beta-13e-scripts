function InitClientSittingSystem()
{
    return ;
}
function ETSSeatMarker::moveDisplayedSeat(%this, %pos)
{
    if (!isObject(%this.myDisplaySeat))
    {
        return ;
    }
    %this.myDisplaySeat.setTransform(%pos);
    return ;
}
function ETSSeatMarker::showSeat(%this, %seatID)
{
    if (isObject(%this.myDisplaySeat))
    {
        %this.myDisplaySeat.seatID = %seatID;
        return ;
    }
    %type = "ClientSeatDisplayData";
    if (%this.isListeningStation())
    {
        %type = "ClientSeatListeningDisplayData";
    }
    %seatDisplay = new EtsClientModel()
    {
        dataBlock = %type;
        seatID = %seatID;
        seatDisplay = 1;
        notSoFast = 0;
        notSoFastClearTime = 1000;
    };
    %seatDisplay.setTransform(%this.getTransform());
    %this.myDisplaySeat = %seatDisplay;
    return ;
}
function ETSSeatMarker::hideSeat(%this)
{
    if (isObject(%this.myDisplaySeat))
    {
        %this.myDisplaySeat.delete();
        %this.myDisplaySeat = 0;
    }
    return ;
}
function EtsClientModel::cancelNotSoFast(%this)
{
    %this.notSoFast = 0;
    return ;
}
function clientCmdSitRequestSuccessful(%unused, %autosit_outfit, %isKissingSeat)
{
    if (!(%autosit_outfit $= ""))
    {
        $player.outfitBeforeAutosit = $gOutfits.get("currentOutfit");
        %success = $player.switchOutfitTo(%autosit_outfit);
        if (!%success)
        {
            error(getScopeName() @ "->Could not change outfit to trigger-specified autosit_outfit = " @ %autosit_outfit);
            $player.outfitBeforeAutosit = "";
        }
        userTips::showOnceEver("AutoChangeToSwimWear");
    }
    if (!(%isKissingSeat $= ""))
    {
        $player.isKissSeat = 1;
    }
    else
    {
        $player.isKissSeat = 0;
    }
    return ;
}
function clientCmdStandRequestSuccessful(%unused)
{
    if (ApplauseMeterGui.applauseMeterUse $= "blockgame")
    {
        ApplauseMeterGui.close();
    }
    $player.isKissSeat = 0;
    if (!($player.outfitBeforeAutosit $= ""))
    {
        %success = $player.switchOutfitTo($player.outfitBeforeAutosit);
        if (!%success)
        {
            error(getScopeName() @ "->Could not restore saved pre-autosit outfit! (previous outfit = " @ Player.outfitBeforeAutosit @ ")");
        }
        $player.outfitBeforeAutosit = "";
    }
    return ;
}
function SendStandCommand(%moveDir)
{
    if ($player.isSitting())
    {
        commandToServer('RequestToStand', %moveDir, 0);
    }
    return ;
}
function ClientSittingSystemOnClick(%obj)
{
    if (%obj.notSoFast == 1)
    {
        return ;
    }
    %obj.notSoFast = 1;
    %obj.schedule(%obj.notSoFastClearTime, cancelNotSoFast);
    if (isObject(CSFurnitureMover))
    {
        CSFurnitureMover.SelectNuggetID(-1);
    }
    commandToServer('RequestToSit', %obj.seatID);
    return ;
}
function clientCmdOnLeaveSittingTrigger(%autosit_outfit)
{
    return ;
}
