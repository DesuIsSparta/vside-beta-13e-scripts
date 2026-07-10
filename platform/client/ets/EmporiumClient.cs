function getEmporium(%name, %callback) {
    %storeInfo = new ""();;
    SimObject;
    %storeInfo.bindClassName("Emporium");
    if (isObject(MissionCleanup)) {
        %storeInfo.add();
    }
    %storeInfo.storeName = MissionCleanup @ %name;
    0;
    %storeInfo.Inventory = Array @ new ""();;
    0;
    if (isObject(MissionCleanup)) {
        %storeInfo.Inventory.add();
    }
    %storeInfo.refreshInventory(%callback);
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
    if ((%count < %index)) {
        if ((0.0 != %index)) {
            %skus = %skus @ " " @ %this.Inventory.getKey(%index);
        }
        %skus = %this.Inventory.getKey(%index);
        %index = (1.0 + %index);
    }
    %skus = trim(%skus);
    (%count < %index);
    return %skus;
};
function Emporium::getItemByIndex(%this, %index) {
    if ((0.0 < %index)) {
    }
    if ((%this.Inventory.count() >= %index)) {
        return 0;
    }
    return %this.Inventory.getValue(%index);
};
function Emporium::getItemBySku(%this, %sku) {
    %index = %this.Inventory.getIndexFromKey(%sku);
    if ((0.0 < %index)) {
        return 0;
    }
    return %this.getItemByIndex(%index);
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
    %request.setURL(%url);
    %request.start();
};
function GetStoreInventory::onDone(%this) {
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() @ " " @ "- status =" @ " " @ %status @ " " @ "url =" @ " " @ %this.getURL());
    if (!(%status $= "success")) {
        %message = %this.getValue("statusMsg");
        error(getScopeName() @ " " @ "- status =" @ " " @ %status @ " " @ "\"" @ %message @ "\"");
        %cmd = %this.callback @ "( 0, " @ %status @ ");";
        eval(%cmd);
        return;
    }
    %storeInfo = %this.store;
    %storeInfo.storeName = %this.storeName;
    %storeInfo.inventoryRevision = %this.getValue("storeRevisionDate");
    %count = %this.getValue("itemsCount");
    %storeInfo.Inventory.empty();
    %index = 0;
    if ((%count < %index)) {
        %sku = %this.getValue("items" @ %index @ ".sku");
        %item = %sku.findBySku();
        SkuManager;
        %quantity = %this.getValue("items" @ %index @ ".quantity");
        %priceVPoints = %this.getValue("items" @ %index @ ".priceVPoints");
        %priceVBux = %this.getValue("items" @ %index @ ".priceVBux");
        if (!(%item)) {
            warn("Inventory", getScopeName() @ " " @ "- Unknown SKU returned from server. SKU =" @ " " @ %sku);
        }
        %item.quantityInStore = %quantity;
        %item.priceVPoints = %priceVPoints;
        %item.priceVBux = %priceVBux;
        %storeInfo.Inventory.push_back(%sku, %item);
        %index = (1.0 + %index);
    }
    %cmd = %this.callback @ "(" @ %storeInfo @ ", \"success\");";
    (%count < %index);
    eval(%cmd);
    %this.schedule(0, "delete");
};
function GetStoreInventory::onError(%this, %unused, %errorName) {
    %this.callback(0, %errorName);
    %this.schedule(0, "delete");
};
function Emporium::purchase(%this, %skulist, %currency, %callback) {
    %this.purchaseCollated(%skulist, %currency, %callback);
};
function Emporium::purchaseCollated(%this, %skulist, %currency, %callback) {
    %currency = strlwr(%currency);
    %this.shoppingList = %skulist;
    %purchaseArray = new ""();;
    Array;
    echoDebug(getScopeName());
    if (!(0 @ " " @ %skulist $= "")) {
        %sku = firstWord(%skulist);
        %skulist = restWords(%skulist);
        %index = %purchaseArray.getIndexFromKey(%sku);
        echo("Found sku=" @ %sku @ " at index " @ %index);
        if ((0.0 < %index)) {
            %purchaseArray.push_back(%sku, 1);
        }
        %count = %purchaseArray.getValue(%index);
        %count = (1.0 + %count);
        %purchaseArray.setValue(%count, %index);
    }
    %request = safeEnsureScriptObject("ManagerRequest", "PurchaseInventory");
    !(%skulist $= "");
    %request.callback = %callback;
    %request.store = %this;
    %url = $Net::ClientServiceURL @ "/PurchaseInventory?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "storeRevisionDate=" @ urlEncode(%this.inventoryRevision) @ "&" @ "payWith=" @ urlEncode(%currency) @ "&" @ "storeName=" @ urlEncode(%this.storeName) @ "&";
    %count = %purchaseArray.count();
    %url = %url @ "itemsToBuyCount=" @ %count;
    %index = 0;
    if ((%count < %index)) {
        %sku = %purchaseArray.getKey(%index);
        %qty = %purchaseArray.getValue(%index);
        %url = %url @ "&" @ "itemsToBuy" @ %index @ ".sku=" @ %sku @ "&" @ "itemsToBuy" @ %index @ ".quantity=" @ %qty;
        %index = (1.0 + %index);
    }
    %request.setURL(%url);
    %request.start();
    %request.purchaseArray = (%count < %index) @ %purchaseArray;
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
    if ((%count < %index)) {
        %sku = getWord(%skulist, %index);
        %url = %url @ "itemsToBuy" @ %index @ ".sku=" @ urlEncode(%sku) @ "&";
        %url = %url @ "itemsToBuy" @ %index @ ".quantity=" @ urlEncode(1) @ "&";
        %index = (1.0 + %index);
    }
    %request.setURL(%url);
    %request.start();
};
function PurchaseInventory::onDone(%this) {
    %skuStatuslist = "";
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() @ " " @ "- status =" @ " " @ %status @ " " @ "url =" @ " " @ %this.getURL());
    %count = %this.getValue("itemsCount");
    %index = 0;
    if ((%count < %index)) {
        %sku = %this.getValue("items" @ %index @ ".sku");
        %result = %this.getValue("items" @ %index @ ".validationResults");
        %qty = %this.purchaseArray.get(%sku);
        if ((%qty $= "")) {
            error(getScopeName() @ " " @ "- sku not found:" @ " " @ %sku @ " " @ %this.getURL());
            %qty = 1;
        }
        %value = "";
        %delim = "";
        %n = 0;
        if ((%qty < %n)) {
            %value = %value @ %delim @ %sku @ "|" @ %result;
            %delim = " ";
            %n = (1.0 + %n);
        }
        if ((0.0 == %index)) {
            %skuStatuslist = %value;
            (%qty < %n);
        }
        %skuStatuslist = %skuStatuslist @ " " @ %value;
        %index = (1.0 + %index);
    }
    %cmd = %this.callback @ "(" @ %status @ ", \"" @ %skuStatuslist @ "\");";
    (%count < %index);
    eval(%cmd);
    %this.purchaseArray.delete();
    %this.schedule(0, "delete");
};
function PurchaseInventory::onError(%this, %unused, %errorName) {
    %cmd = %this.callback @ "( \"error\", " @ %errorName @ ");";
    eval(%cmd);
    %this.schedule(0, "delete");
};
