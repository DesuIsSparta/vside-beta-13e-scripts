$Player::furnitureInventory = new Array("");;
0;
$Player::bFakeFurnitureInventory = 0;
function getFurnitureSkus() {
    %count = $Player::furnitureInventory.count();
    %skulist = "";
    %index = 0;
    if ((%count < %index)) {
        %sku = $Player::furnitureInventory.getKey(%index);
        if ((0.0 != %index)) {
            %skulist = %skulist @ " " @ %sku;
        }
        %skulist = %sku;
        %index = (1.0 + %index);
    }
    return %skulist;
};
function addFurnitureSku(%sku, %quantity) {
    %index = $Player::furnitureInventory.getIndexFromKey(%sku);
    if ((-(1.0) == %index)) {
        $Player::furnitureInventory.push_back(%sku, %quantity @ " " @ 0);
        return;
    }
    %value = $Player::furnitureInventory.getValue(%index);
    %owned = getWord(%value, 0);
    %inUse = getWord(%value, 1);
    %owned = (%quantity + %owned);
    %value = %owned @ " " @ %inUse;
    $Player::furnitureInventory.setValue(%value, %index);
};
function removeFurnitureSku(%sku, %quantity) {
    %index = $Player::furnitureInventory.getIndexFromKey(%sku);
    if ((-(1.0) == %index)) {
        echo("Player does not own sku #" @ %sku);
        return;
    }
    %value = $Player::furnitureInventory.getValue(%index);
    %owned = getWord(%value, 0);
    %inUse = getWord(%value, 1);
    if ((%quantity <= %owned)) {
        if ((%quantity < %owned)) {
            echo("Player only onws " @ %owned @ " items of type " @ %sku @ ". Removing them all.");
        }
        $Player::furnitureInventory.erase(%index);
        return;
    }
    %owned = (%quantity - %owned);
    if ((%inUse < %owned)) {
        log("inventory", "warn", "Player now owns fewer (" @ %owned @ ") items of type " @ %sku @ " than are in use (" @ %inUse @ ")");
    }
    %value = %owned @ " " @ %inUse;
    $Player::furnitureInventory.setValue(%value, %index);
};
function removeAllFurnitureSku(%sku) {
    %index = $Player::furnitureInventory.getIndexFromKey(%sku);
    if ((-(1.0) == %index)) {
        echo("Player does not own sku #" @ %sku);
        return;
    }
    $Player::furnitureInventory.erase(%index);
};
function numOwnedFurnitureSku(%sku) {
    %index = $Player::furnitureInventory.getIndexFromKey(%sku);
    if ((-(1.0) == %index)) {
        return 0;
    }
    %value = $Player::furnitureInventory.getValue(%index);
    %owned = getWord(%value, 0);
    return %owned;
};
function numUsingFurnitureSku(%sku) {
    %index = $Player::furnitureInventory.getIndexFromKey(%sku);
    if ((-(1.0) == %index)) {
        return 0;
    }
    %value = $Player::furnitureInventory.getValue(%index);
    %inUse = getWord(%value, 1);
    return %inUse;
};
function numUsingFurnitureAll() {
    %total = 0;
    %count = $Player::furnitureInventory.count();
    %index = 0;
    if ((%count < %index)) {
        %value = $Player::furnitureInventory.getValue(%index);
        %inUse = getWord(%value, 1);
        %total = (%inUse + %total);
        %index = (1.0 + %index);
    }
    return %total;
};
function useFurnitureSku(%sku, %quantity) {
    %index = $Player::furnitureInventory.getIndexFromKey(%sku);
    if ((-(1.0) == %index)) {
        log("inventory", "warn", "Player does not own " @ %sku);
        return;
    }
    %value = $Player::furnitureInventory.getValue(%index);
    %owned = getWord(%value, 0);
    %inUse = getWord(%value, 1);
    if ((%quantity < %owned)) {
        log("inventory", "warn", "Player does not own " @ %quantity @ " of " @ %sku @ "(" @ %owned @ ")");
        %quantity = %owned;
    }
    %value = %owned @ " " @ %quantity;
    echo("putting " @ %value @ " for sku=" @ %sku);
    $Player::furnitureInventory.setValue(%value, %index);
};
function useAnotherFurnitureSku(%sku) {
    %index = $Player::furnitureInventory.getIndexFromKey(%sku);
    if ((-(1.0) == %index)) {
        log("inventory", "warn", "Player does not own " @ %sku);
        return 0;
    }
    %value = $Player::furnitureInventory.getValue(%index);
    %owned = getWord(%value, 0);
    %inUse = getWord(%value, 1);
    if ((%inUse == %owned)) {
        log("inventory", "warn", "No more " @ %sku @ " available");
        return 0;
    }
    if ((%inUse < %owned)) {
        log("inventory", "error", "More of " @ %sku @ " in use than owned!");
        return 0;
    }
    %inUse = (1.0 + %inUse);
    %value = %owned @ " " @ %inUse;
    $Player::furnitureInventory.setValue(%value, %index);
    return 1;
};
function putAwayAnotherFurnitureSku(%sku) {
    %index = $Player::furnitureInventory.getIndexFromKey(%sku);
    if ((-(1.0) == %index)) {
        log("inventory", "warn", "Player does not own " @ %sku);
        return 0;
    }
    %value = $Player::furnitureInventory.getValue(%index);
    %owned = getWord(%value, 0);
    %inUse = getWord(%value, 1);
    if ((0.0 == %inUse)) {
        log("inventory", "warn", "No more " @ %sku @ " available");
        return 0;
    }
    if ((0.0 < %inUse)) {
        log("inventory", "error", "Negative number (" @ %inUse @ ") of " @ %sku @ " in use!");
        return 0;
    }
    %inUse = (1.0 - %inUse);
    %value = %owned @ " " @ %inUse;
    $Player::furnitureInventory.setValue(%value, %index);
    return 1;
};
function putAwayAllFurniture() {
    %count = $Player::furnitureInventory.count();
    %index = 0;
    if ((%count < %index)) {
        %value = $Player::furnitureInventory.getValue(%index);
        %owned = getWord(%value, 0);
        $Player::furnitureInventory.setValue(%owned @ " " @ 0, %index);
        %index = (1.0 + %index);
    }
};
function dumpFurniture() {
    %count = $Player::furnitureInventory.count();
    %index = 0;
    if ((%count < %index)) {
        %val = $Player::furnitureInventory.getValue(%index);
        %sku = $Player::furnitureInventory.getKey(%index);
        %si = SkuManager.findBySku(%sku);
        echo(%sku @ " - owned: " @ getWord(%val, 0) @ ", in use: " @ getWord(%val, 1) @ " (" @ %si.descShrt @ ")");
        %index = (1.0 + %index);
    }
};
function dumpFurnitureInUse() {
    %count = $Player::furnitureInventory.count();
    %index = 0;
    if ((%count < %index)) {
        %val = $Player::furnitureInventory.getValue(%index);
        %sku = $Player::furnitureInventory.getKey(%index);
        %numInUse = getWord(%val, 1);
        if ((0.0 > %numInUse)) {
            %si = SkuManager.findBySku(%sku);
            echo(%sku @ " - owned: " @ getWord(%val, 0) @ ", in use: " @ getWord(%val, 1) @ " (" @ %si.descShrt @ ")");
        }
        %index = (1.0 + %index);
    }
};
function clearOwnedFurniture() {
    $Player::furnitureInventory.empty();
};
function getOwnedFurniture() {
    %request = safeEnsureScriptObject("ManagerRequest", "FurnitureRequest");
    if (%request.isOpen()) {
        warn("network", getScopeName() @ " " @ "- got overlapping requests. postponing. url =" @ " " @ %request.getURL());
        %request.doAnother = 1;
        return;
    }
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%request);
    }
    %url = $Net::ClientServiceURL @ "/GetUserInventory?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "skuType=furnishing";
    log("network", "debug", "FurnitureRequest: " @ %url);
    %request.setURL(%url);
    if ($StandAlone) {
    }
    if ($Player::bFakeFurnitureInventory) {
        %request.schedule(1000, "fakeOnDone");
    }
    %request.start();
};
function FurnitureRequest::onError(%this, %unused, %unused) {
};
function FurnitureRequest::onDone(%this) {
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if (!(CustomSpaceClient::isOwner())) {
        return;
    }
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() @ " " @ "- status =" @ " " @ %status @ " " @ "url =" @ " " @ %this.getURL());
    if (!(%status $= "success")) {
        error(getScopeName() @ " " @ "- status =" @ " " @ %status);
        return;
    }
    clearOwnedFurniture();
    %count = %this.getValue("itemsCount");
    if ((1.0 < %count)) {
        log("network", "debug", getScopeName() @ " " @ "- Nothing in furniture inventory.");
        return;
    }
    %index = 0;
    if ((%count < %index)) {
        %sku = %this.getValue("items" @ %index @ ".sku");
        %qty = %this.getValue("items" @ %index @ ".quantity");
        addFurnitureSku(%sku, %qty);
        %index = (1.0 + %index);
    }
    %request = safeNewScriptObject("ScriptObject", "Request_GetActiveFurnitureSkus", 1);
    (%count < %index);
    %request.result = "";
    commandToServer('GetActiveFurnitureSkus', CustomSpaceClient::GetSpaceImIn(), %request.getId());
};
function clientCmdOnFurniturePlaced(%sku, %quantity) {
    putIntoUseFurnitureSku(%sku, %quantity, 1);
};
$gGotFurnitureCallback = "";
function clientCmdGotFurnitureSkus(%skulistchunk, %requestId, %complete) {
    if (!(isObject(%requestId))) {
        warn("network", "results for deleted request: GotFurnitureSkus");
        return;
    }
    %requestId.result = %requestId.result @ %skulistchunk;
    if ((0.0 == %complete)) {
        return;
    }
    %skulist = %requestId.result;
    %requestId.delete();
    if (!(%skulist $= "")) {
        %value = firstWord(%skulist);
        %skulist = restWords(%skulist);
        %value = strreplace(%value, "|", " ");
        %sku = getWord(%value, 0);
        %quantity = getWord(%value, 1);
        useFurnitureSku(%sku, %quantity);
    }
    if (!(!(%skulist $= "") @ " " @ $gGotFurnitureCallback $= "")) {
        eval($gGotFurnitureCallback);
    }
};
function refreshActiveFurniture() {
    putAwayAllFurniture();
    %request = safeNewScriptObject("ScriptObject", "Request_GetActiveFurnitureSkus", 1);
    %request.result = "";
    commandToServer('GetActiveFurnitureSkus', CustomSpaceClient::GetSpaceImIn(), %request.getId());
};
function getFurnitureStore(%callback) {
    getEmporium("furnishings", %callback);
};
function getNuggetIdList(%callback) {
    %request = safeNewScriptObject("ScriptObject", "Request_CSGetNuggetIdList", 1);
    %request.result = "";
    %request.callback = %callback;
    commandToServer('CSGetNuggetIdList', CustomSpaceClient::GetSpaceImIn(), %request.getId());
};
function clientCmdGotNuggetIdList(%nuggetchunk, %requestId, %completed) {
    if (!(isObject(%requestId))) {
        warn("network", "results for deleted request: GotNuggetIdList");
        return;
    }
    %requestId.result = %requestId.result @ %nuggetchunk;
    if ((0.0 == %completed)) {
        return;
    }
    %cmd = %requestId.callback @ "( \"" @ %requestId.result @ "\");";
    %requestId.delete();
    eval(%cmd);
};
function getNuggetGhostList(%callback) {
    %request = safeNewScriptObject("ScriptObject", "Request_CSGetNuggetGhostList", 1);
    %request.result = "";
    %request.callback = %callback;
    commandToServer('CSGetNuggetGhostList', CustomSpaceClient::GetSpaceImIn(), %request.getId());
};
function clientCmdGotNuggetGhostList(%ghostchunk, %requestId, %completed) {
    if (!(isObject(%requestId))) {
        warn("network", "results for deleted request: GotNuggetGhostList");
        return;
    }
    %requestId.result = %requestId.result @ %ghostchunk;
    if ((0.0 == %completed)) {
        return;
    }
    %ghostlist = %requestId.result;
    %objectList = "";
    if (!(%ghostlist $= "")) {
        %ghostID = firstWord(%ghostlist);
        %ghostlist = restWords(%ghostlist);
        %objID = ServerConnection.resolveGhostID(%ghostID);
        %objectList = %objectList @ " " @ %objID;
    }
    %objectList = trim(%objectList);
    !(%ghostlist $= "");
    %requestId.delete();
    CSFurnitureMover::refreshGhostList(%objectList);
};
function FurnitureRequest::fakeOnDone(%this) {
    %this.putValue("status", "success");
    %fakeInventory = SkuManager.getSkusType("furnishing");
    %count = getWordCount(%fakeInventory);
    %this.putValue("itemsCount", %count);
    %idx = 0;
    if ((%count < %idx)) {
        %sku = getWord(%fakeInventory, %idx);
        %qty = 50;
        %this.putValue("items" @ %idx @ ".sku", %sku);
        %this.putValue("items" @ %idx @ ".quantity", %qty);
        %idx = (1.0 + %idx);
    }
    %this.onDone();
};
