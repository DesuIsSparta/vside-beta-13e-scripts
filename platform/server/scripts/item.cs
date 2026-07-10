$Item::RespawnTime = (20.0 * 1000.0);
$Item::PopTime = (10.0 * 1000.0);
function Item::respawn(%this) {
    1.startFade(%this, 0, 0);
    1.setHidden(%this);
    0.schedule(%this, $Item::RespawnTime, "setHidden");
    0.schedule(%this, ($Item::RespawnTime + 100.0), "startFade", 1000, 0);
};
function Item::schedulePop(%this) {
    1.schedule(%this, ($Item::PopTime - 1000.0), "startFade", 1000, 0);
    "delete".schedule(%this, $Item::PopTime);
};
function ItemData::onThrow(%this, %user, %amount) {
    if ((%amount $= "")) {
        %amount = 1;
    }
    if (!(%this.maxInventory $= "") && (%amount > %this.maxInventory)) {
        %amount = %this.maxInventory;
    }
    if (!(%amount)) {
        return 0;
    }
    %amount.decInventory(%user, %this);
    %obj = new Item("") {
        dataBlock = %this;
        rotation = "0 0 1 " @ (getRandom() * 360.0);
        count = %amount;
    };
    %obj.add(MissionGroup);
    %obj.schedulePop();
    return %obj;
};
function ItemData::onPickup(%this, %obj, %user, %amount) {
    %count = %obj.count;
    if ((%count $= "")) {
        if (!(%this.maxInventory $= "")) {
            if (!(%count = %this.maxInventory)) {
                return;
            }
        }
        %count = 1;
    }
    %count.incInventory(%user, %this);
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
    %obj = new Item("") {
        dataBlock = %data;
        static = 1;
        rotate = 1;
    };
    return %obj;
};
