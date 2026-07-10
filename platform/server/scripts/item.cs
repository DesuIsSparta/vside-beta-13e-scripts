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
    if (!(%this.maxInventory $= "")) {
        if ((%this.maxInventory > %amount)) {
            %amount = %this.maxInventory;
        }
    }
    if (!(%amount)) {
        return 0;
    }
    %user.decInventory(%this, %amount);
    0;
    %obj = new ""() {
        dataBlock = Item @ %this;
        rotation = "0 0 1 " @ (360.0 * getRandom());
        count = %amount;
    };
    %obj.add();
    %obj.schedulePop();
    return %obj;
};
function ItemData::onPickup(%this, %obj, %user, %amount) {
    %count = %obj.count;
    if ((%count $= "")) {
        if (!(%this.maxInventory $= "")) {
            %count = %this.maxInventory;
            if (!()) {
                return;
            }
        }
        %count = 1;
    }
    %user.incInventory(%this, %count);
    if (%user.client) {
        messageClient(%user.client, 'MsgItemPickup', '\x02\x01You picked up %1', %this.pickUpName);
    }
    if (%obj.isStatic()) {
        %obj.respawn();
    }
    %obj.delete();
    return 1;
};
function ItemData::create(%data) {
    0;
    %obj = new ""() {
        dataBlock = Item @ %data;
        static = 1;
        rotate = 1;
    };
    return %obj;
};
