$Player::furnitureInventory = new Array("");;
0;
$Player::bFakeFurnitureInventory = 0;
function getFurnitureSkus() {
    %count = $Player::furnitureInventory.count();
    %skulist = "";
    %index = 0;
    while ((%index < %count)) {
        %sku = %index.getKey($Player::furnitureInventory);
        if ((%index != 0.0)) {
            %skulist = %skulist @ " " @ %sku;
        }
        %skulist = %sku;
        %index = (%index + 1.0);
    }
    return %skulist;
};
function addFurnitureSku(%sku, %quantity) {
    %index = %sku.getIndexFromKey($Player::furnitureInventory);
    if ((%index == -(1.0))) {
        %quantity @ " " @ 0.push_back($Player::furnitureInventory, %sku);
        return;
    }
    %value = %index.getValue($Player::furnitureInventory);
    %owned = getWord(%value, 0);
    %inUse = getWord(%value, 1);
    %owned = (%owned + %quantity);
    %value = %owned @ " " @ %inUse;
    %index.setValue($Player::furnitureInventory, %value);
};
function removeFurnitureSku(%sku, %quantity) {
    %index = %sku.getIndexFromKey($Player::furnitureInventory);
    if ((%index == -(1.0))) {
        echo("Player does not own sku #" @ %sku);
        return;
    }
    %value = %index.getValue($Player::furnitureInventory);
    %owned = getWord(%value, 0);
    %inUse = getWord(%value, 1);
    if ((%owned <= %quantity)) {
        if ((%owned < %quantity)) {
            echo("Player only onws " @ %owned @ " items of type " @ %sku @ ". Removing them all.");
        }
        %index.erase($Player::furnitureInventory);
        return;
    }
    %owned = (%owned - %quantity);
    if ((%owned < %inUse)) {
        log("inventory", "warn", "Player now owns fewer (" @ %owned @ ") items of type " @ %sku @ " than are in use (" @ %inUse @ ")");
    }
    %value = %owned @ " " @ %inUse;
    %index.setValue($Player::furnitureInventory, %value);
};
function removeAllFurnitureSku(%sku) {
    %index = %sku.getIndexFromKey($Player::furnitureInventory);
    if ((%index == -(1.0))) {
        echo("Player does not own sku #" @ %sku);
        return;
    }
    %index.erase($Player::furnitureInventory);
};
function numOwnedFurnitureSku(%sku) {
    %index = %sku.getIndexFromKey($Player::furnitureInventory);
    if ((%index == -(1.0))) {
        return 0;
    }
    %value = %index.getValue($Player::furnitureInventory);
    %owned = getWord(%value, 0);
    return %owned;
};
function numUsingFurnitureSku(%sku) {
    %index = %sku.getIndexFromKey($Player::furnitureInventory);
    if ((%index == -(1.0))) {
        return 0;
    }
    %value = %index.getValue($Player::furnitureInventory);
    %inUse = getWord(%value, 1);
    return %inUse;
};
function numUsingFurnitureAll() {
    %total = 0;
    %count = $Player::furnitureInventory.count();
    %index = 0;
    while ((%index < %count)) {
        %value = %index.getValue($Player::furnitureInventory);
        %inUse = getWord(%value, 1);
        %total = (%total + %inUse);
        %index = (%index + 1.0);
    }
    return %total;
};
function useFurnitureSku(%sku, %quantity) {
    %index = %sku.getIndexFromKey($Player::furnitureInventory);
    if ((%index == -(1.0))) {
        log("inventory", "warn", "Player does not own " @ %sku);
        return;
    }
    %value = %index.getValue($Player::furnitureInventory);
    %owned = getWord(%value, 0);
    %inUse = getWord(%value, 1);
    if ((%owned < %quantity)) {
        log("inventory", "warn", "Player does not own " @ %quantity @ " of " @ %sku @ "(" @ %owned @ ")");
        %quantity = %owned;
    }
    %value = %owned @ " " @ %quantity;
    echo("putting " @ %value @ " for sku=" @ %sku);
    %index.setValue($Player::furnitureInventory, %value);
};
function useAnotherFurnitureSku(%sku) {
    %index = %sku.getIndexFromKey($Player::furnitureInventory);
    if ((%index == -(1.0))) {
        log("inventory", "warn", "Player does not own " @ %sku);
        return 0;
    }
    %value = %index.getValue($Player::furnitureInventory);
    %owned = getWord(%value, 0);
    %inUse = getWord(%value, 1);
    if ((%owned == %inUse)) {
        log("inventory", "warn", "No more " @ %sku @ " available");
        return 0;
    }
    if ((%owned < %inUse)) {
        log("inventory", "error", "More of " @ %sku @ " in use than owned!");
        return 0;
    }
    %inUse = (%inUse + 1.0);
    %value = %owned @ " " @ %inUse;
    %index.setValue($Player::furnitureInventory, %value);
    return 1;
};
function putAwayAnotherFurnitureSku(%sku) {
    %index = %sku.getIndexFromKey($Player::furnitureInventory);
    if ((%index == -(1.0))) {
        log("inventory", "warn", "Player does not own " @ %sku);
        return 0;
    }
    %value = %index.getValue($Player::furnitureInventory);
    %owned = getWord(%value, 0);
    %inUse = getWord(%value, 1);
    if ((%inUse == 0.0)) {
        log("inventory", "warn", "No more " @ %sku @ " available");
        return 0;
    }
    if ((%inUse < 0.0)) {
        log("inventory", "error", "Negative number (" @ %inUse @ ") of " @ %sku @ " in use!");
        return 0;
    }
    %inUse = (%inUse - 1.0);
    %value = %owned @ " " @ %inUse;
    %index.setValue($Player::furnitureInventory, %value);
    return 1;
};
function putAwayAllFurniture() {
    %count = $Player::furnitureInventory.count();
    %index = 0;
    while ((%index < %count)) {
        %value = %index.getValue($Player::furnitureInventory);
        %owned = getWord(%value, 0);
        %index.setValue($Player::furnitureInventory, %owned @ " " @ 0);
        %index = (%index + 1.0);
    }
};
function dumpFurniture() {
    %count = $Player::furnitureInventory.count();
    %index = 0;
    while ((%index < %count)) {
        %val = %index.getValue($Player::furnitureInventory);
        %sku = %index.getKey($Player::furnitureInventory);
        %si = %sku.findBySku(SkuManager);
        echo(%sku @ " - owned: " @ getWord(%val, 0) @ ", in use: " @ getWord(%val, 1) @ " (" @ %si.descShrt @ ")");
        %index = (%index + 1.0);
    }
};
function dumpFurnitureInUse() {
    %count = $Player::furnitureInventory.count();
    %index = 0;
    while ((%index < %count)) {
        %val = %index.getValue($Player::furnitureInventory);
        %sku = %index.getKey($Player::furnitureInventory);
        %numInUse = getWord(%val, 1);
        if ((%numInUse > 0.0)) {
            %si = %sku.findBySku(SkuManager);
            echo(%sku @ " - owned: " @ getWord(%val, 0) @ ", in use: " @ getWord(%val, 1) @ " (" @ %si.descShrt @ ")");
        }
        %index = (%index + 1.0);
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
        %request.add(MissionCleanup);
    }
    %url = $Net::ClientServiceURL @ "/GetUserInventory?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "skuType=furnishing";
    log("network", "debug", "FurnitureRequest: " @ %url);
    %url.setURL(%request);
    if ($StandAlone) {
    }
    if ($Player::bFakeFurnitureInventory) {
        "fakeOnDone".schedule(%request, 1000);
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
    %count = "itemsCount".getValue(%this);
    if ((%count < 1.0)) {
        log("network", "debug", getScopeName() @ " " @ "- Nothing in furniture inventory.");
        return;
    }
    %index = 0;
    while ((%index < %count)) {
        %sku = "items" @ %index @ ".sku".getValue(%this);
        %qty = "items" @ %index @ ".quantity".getValue(%this);
        addFurnitureSku(%sku, %qty);
        %index = (%index + 1.0);
    }
    %request = safeNewScriptObject("ScriptObject", "Request_GetActiveFurnitureSkus", 1);
    (%index < %count);
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
    if ((%complete == 0.0)) {
        return;
    }
    %skulist = %requestId.result;
    %requestId.delete();
    while (!(%skulist $= "")) {
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
    if ((%completed == 0.0)) {
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
    if ((%completed == 0.0)) {
        return;
    }
    %ghostlist = %requestId.result;
    %objectList = "";
    while (!(%ghostlist $= "")) {
        %ghostID = firstWord(%ghostlist);
        %ghostlist = restWords(%ghostlist);
        %objID = %ghostID.resolveGhostID(ServerConnection);
        %objectList = %objectList @ " " @ %objID;
    }
    %objectList = trim(%objectList);
    !(%ghostlist $= "");
    %requestId.delete();
    CSFurnitureMover::refreshGhostList(%objectList);
};
function FurnitureRequest::fakeOnDone(%this) {
    "success".putValue(%this, "status");
    %fakeInventory = "furnishing".getSkusType(SkuManager);
    %count = getWordCount(%fakeInventory);
    %count.putValue(%this, "itemsCount");
    %idx = 0;
    while ((%idx < %count)) {
        %sku = getWord(%fakeInventory, %idx);
        %qty = 50;
        %sku.putValue(%this, "items" @ %idx @ ".sku");
        %qty.putValue(%this, "items" @ %idx @ ".quantity");
        %idx = (%idx + 1.0);
    }
    %this.onDone();
};
