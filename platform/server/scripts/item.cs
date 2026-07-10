$Item::RespawnTime = (1000.0 * 20.0);
$Item::PopTime = (1000.0 * 10.0);
function Item::respawn(%this) {
    %this.startFade(0, 0, 1);
    %this.setHidden(1);
    %this.schedule($Item::RespawnTime, "setHidden", 0);
    %this.schedule((100.0 + $Item::RespawnTime), "startFade", 1000, 0, 0);
};
function Item::schedulePop(%this) {
    %this.schedule((1000.0 - $Item::PopTime), "startFade", 1000, 0, 1);
    %this.schedule($Item::PopTime, "delete");
};
function ItemData::onThrow(%this, %user, %amount) {
    if ((%amount $= "")) {
        %amount = 1;
    }
    if (!(%this SPC maxInventory $= "")) {
        if ((maxInventory > %amount)) {
            %amount = maxInventory;
            %this;
        }
    }
    if (!(%amount)) {
        return 0;
    }
    %user.decInventory(%this, %amount);
    dataBlock = Item @ new ""() @ %this;
    0;
    rotation = "0 0 1 " @ (360.0 * getRandom());
    count = %amount;
    %obj = ;
    %obj.add();
    %obj.schedulePop();
    return %obj;
};
function ItemData::onPickup(%this, %obj, %user, %amount) {
    %count = count;
    %obj;
    if ((%count $= "")) {
        if (!(%this SPC maxInventory $= "")) {
            %count = maxInventory;
            if (!(%this)) {
                return;
            }
        }
        %count = 1;
    }
    %user.incInventory(%this, %count);
    if (client) {
        messageClient(client, 'MsgItemPickup', '\x02\x01You picked up %1', pickUpName);
    }
    if (%obj.isStatic()) {
        %obj.respawn();
    }
    %obj.delete();
    return 1;
};
function ItemData::create(%data) {
    dataBlock = Item @ new ""() @ %data;
    0;
    static = 1;
    rotate = 1;
    %obj = ;
    return %obj;
};
