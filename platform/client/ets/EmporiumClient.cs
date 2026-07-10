function getEmporium(%name, %callback) {
    %storeInfo = new SimObject("");
    "Emporium".bindClassName(%storeInfo);
    if (isObject(MissionCleanup)) {
        %storeInfo.add(MissionCleanup);
    }
    %storeInfo.storeName = %name;
    %storeInfo.Inventory = new Array("");
    if (isObject(MissionCleanup)) {
        %storeInfo.Inventory.add(MissionCleanup);
    }
    %callback.refreshInventory(%storeInfo);
};
function Emporium::OnRemove(%this) {
    if (isObject(%storeInfo.Inventory)) {
        %storeInfo.Inventory.delete();
    }
};
function Emporium::getStoreName(%this) {
    return %this.storeName;
};
function Emporium::getSkuCount(%this) {
    return %this.Inventory.count();
};
function Emporium::getSkus(%this) {
    %count = %this.Inventory.count();
    %skus = "";
    %index = 0;
    while ((%index < %count)) {
        if ((%index != 0.0)) {
            %skus = %skus @ " " @ %index.getKey(%this.Inventory);
        }
        %skus = %index.getKey(%this.Inventory);
        %index = (%index + 1.0);
    }
    %skus = trim(%skus);
    (%index < %count);
    return %skus;
};
function Emporium::getItemByIndex(%this, %index) {
    if ((%index < 0.0)) {
    }
    if ((%index >= %this.Inventory.count())) {
        return 0;
    }
    return %index.getValue(%this.Inventory);
};
function Emporium::getItemBySku(%this, %sku) {
    %index = %sku.getIndexFromKey(%this.Inventory);
    if ((%index < 0.0)) {
        return 0;
    }
    return %index.getItemByIndex(%this);
};
function Emporium::destroyStore(%this) {
    %this.Inventory.empty();
};
function Emporium::refreshInventory(%this, %callback) {
    if (!(haveValidManagerHost())) {
        warn(getScopeName() @ " " @ "no valid manager, not doing this");
        return;
    }
    %request = safeEnsureScriptObject("ManagerRequest", "GetStoreInventory");
    %request.callback = %callback;
    %request.storeName = %this.storeName;
    %request.store = %this;
    %url = $Net::ClientServiceURL @ "/GetStoreInventory?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "storeName=" @ urlEncode(%this.storeName);
    %url.setURL(%request);
    %request.start();
};
function GetStoreInventory::onDone(%this) {
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() @ " " @ "- status =" @ " " @ %status @ " " @ "url =" @ " " @ %this.getURL());
    if (!(%status $= "success")) {
        %message = "statusMsg".getValue(%this);
        error(getScopeName() @ " " @ "- status =" @ " " @ %status @ " " @ "\"" @ %message @ "\"");
        %cmd = %this.callback @ "( 0, " @ %status @ ");";
        eval(%cmd);
        return;
    }
    %storeInfo = %this.store;
    %storeInfo.storeName = %this.storeName;
    %storeInfo.inventoryRevision = "storeRevisionDate".getValue(%this);
    %count = "itemsCount".getValue(%this);
    %storeInfo.Inventory.empty();
    %index = 0;
    while ((%index < %count)) {
        %sku = "items" @ %index @ ".sku".getValue(%this);
        %item = %sku.findBySku(SkuManager);
        %quantity = "items" @ %index @ ".quantity".getValue(%this);
        %priceVPoints = "items" @ %index @ ".priceVPoints".getValue(%this);
        %priceVBux = "items" @ %index @ ".priceVBux".getValue(%this);
        if (!(%item)) {
            warn("Inventory", getScopeName() @ " " @ "- Unknown SKU returned from server. SKU =" @ " " @ %sku);
        }
        %item.quantityInStore = %quantity;
        %item.priceVPoints = %priceVPoints;
        %item.priceVBux = %priceVBux;
        %item.push_back(%storeInfo.Inventory, %sku);
        %index = (%index + 1.0);
    }
    %cmd = %this.callback @ "(" @ %storeInfo @ ", \"success\");";
    (%index < %count);
    eval(%cmd);
    "delete".schedule(%this, 0);
};
function GetStoreInventory::onError(%this, %unused, %errorName) {
    %errorName.callback(%this, 0);
    "delete".schedule(%this, 0);
};
function Emporium::purchase(%this, %skulist, %currency, %callback) {
    %callback.purchaseCollated(%this, %skulist, %currency);
};
function Emporium::purchaseCollated(%this, %skulist, %currency, %callback) {
    %currency = strlwr(%currency);
    %this.shoppingList = %skulist;
    %purchaseArray = new Array("");
    echoDebug(getScopeName());
    while (!(%skulist $= "")) {
        %sku = firstWord(%skulist);
        %skulist = restWords(%skulist);
        %index = %sku.getIndexFromKey(%purchaseArray);
        echo("Found sku=" @ %sku @ " at index " @ %index);
        if ((%index < 0.0)) {
            1.push_back(%purchaseArray, %sku);
        }
        %count = %index.getValue(%purchaseArray);
        %count = (%count + 1.0);
        %index.setValue(%purchaseArray, %count);
    }
    %request = safeEnsureScriptObject("ManagerRequest", "PurchaseInventory");
    !(%skulist $= "");
    %request.callback = %callback;
    %request.store = %this;
    %url = $Net::ClientServiceURL @ "/PurchaseInventory?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "storeRevisionDate=" @ urlEncode(%this.inventoryRevision) @ "&" @ "payWith=" @ urlEncode(%currency) @ "&" @ "storeName=" @ urlEncode(%this.storeName) @ "&";
    %count = %purchaseArray.count();
    %url = %url @ "itemsToBuyCount=" @ %count;
    %index = 0;
    while ((%index < %count)) {
        %sku = %index.getKey(%purchaseArray);
        %qty = %index.getValue(%purchaseArray);
        %url = %url @ "&" @ "itemsToBuy" @ %index @ ".sku=" @ %sku @ "&" @ "itemsToBuy" @ %index @ ".quantity=" @ %qty;
        %index = (%index + 1.0);
    }
    %url.setURL(%request);
    %request.start();
    %request.purchaseArray = (%index < %count) @ %purchaseArray;
};
function Emporium::purchaseUncollated(%this, %skulist, %currency, %callback) {
    %currency = strlwr(%currency);
    %this.shoppingList = %skulist;
    %request = safeEnsureScriptObject("ManagerRequest", "PurchaseInventory");
    %request.callback = %callback;
    %request.store = %this;
    %count = getWordCount(%skulist);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/PurchaseInventory?";
    %url = %url @ "user=" @ urlEncode($Player::Name) @ "&";
    %url = %url @ "token=" @ urlEncode($Token) @ "&";
    %url = %url @ "storeRevisionDate=" @ urlEncode(%this.inventoryRevision) @ "&";
    %url = %url @ "payWith=" @ urlEncode(%currency) @ "&";
    %url = %url @ "storeName=" @ urlEncode(%this.storeName) @ "&";
    %url = %url @ "itemsToBuyCount=" @ urlEncode(%count) @ "&";
    %index = 0;
    while ((%index < %count)) {
        %sku = getWord(%skulist, %index);
        %url = %url @ "itemsToBuy" @ %index @ ".sku=" @ urlEncode(%sku) @ "&";
        %url = %url @ "itemsToBuy" @ %index @ ".quantity=" @ urlEncode(1) @ "&";
        %index = (%index + 1.0);
    }
    %url.setURL(%request);
    %request.start();
};
function PurchaseInventory::onDone(%this) {
    %skuStatuslist = "";
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() @ " " @ "- status =" @ " " @ %status @ " " @ "url =" @ " " @ %this.getURL());
    %count = "itemsCount".getValue(%this);
    %index = 0;
    while ((%index < %count)) {
        %sku = "items" @ %index @ ".sku".getValue(%this);
        %result = "items" @ %index @ ".validationResults".getValue(%this);
        %qty = %sku.get(%this.purchaseArray);
        if ((%qty $= "")) {
            error(getScopeName() @ " " @ "- sku not found:" @ " " @ %sku @ " " @ %this.getURL());
            %qty = 1;
        }
        %value = "";
        %delim = "";
        %n = 0;
        while ((%n < %qty)) {
            %value = %value @ %delim @ %sku @ "|" @ %result;
            %delim = " ";
            %n = (%n + 1.0);
        }
        if ((%index == 0.0)) {
            %skuStatuslist = %value;
            (%n < %qty);
        }
        %skuStatuslist = %skuStatuslist @ " " @ %value;
        %index = (%index + 1.0);
    }
    %cmd = %this.callback @ "(" @ %status @ ", \"" @ %skuStatuslist @ "\");";
    (%index < %count);
    eval(%cmd);
    %this.purchaseArray.delete();
    "delete".schedule(%this, 0);
};
function PurchaseInventory::onError(%this, %unused, %errorName) {
    %cmd = %this.callback @ "( \"error\", " @ %errorName @ ");";
    eval(%cmd);
    "delete".schedule(%this, 0);
};
