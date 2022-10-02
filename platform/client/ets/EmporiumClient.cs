function getEmporium(%name, %callback)
{
    %storeInfo = new SimObject();
    %storeInfo.bindClassName("Emporium");
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%storeInfo);
    }
    %storeInfo.storeName = %name;
    %storeInfo.Inventory = new Array();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%storeInfo.Inventory);
    }
    %storeInfo.refreshInventory(%callback);
    return ;
}
function Emporium::OnRemove(%this)
{
    if (isObject(%storeInfo.Inventory))
    {
        %storeInfo.Inventory.delete();
    }
    return ;
}
function Emporium::getStoreName(%this)
{
    return %this.storeName;
}
function Emporium::getSkuCount(%this)
{
    return %this.Inventory.count();
}
function Emporium::getSkus(%this)
{
    %count = %this.Inventory.count();
    %skus = "";
    %index = 0;
    while (%index < %count)
    {
        if (%index != 0)
        {
            %skus = %skus SPC %this.Inventory.getKey(%index);
        }
        else
        {
            %skus = %this.Inventory.getKey(%index);
        }
        %index = %index + 1;
    }
    %skus = trim(%skus);
    return %skus;
}
function Emporium::getItemByIndex(%this, %index)
{
    if ((%index < 0) && (%index >= %this.Inventory.count()))
    {
        return 0;
    }
    return %this.Inventory.getValue(%index);
}
function Emporium::getItemBySku(%this, %sku)
{
    %index = %this.Inventory.getIndexFromKey(%sku);
    if (%index < 0)
    {
        return 0;
    }
    return %this.getItemByIndex(%index);
}
function Emporium::destroyStore(%this)
{
    %this.Inventory.empty();
    return ;
}
function Emporium::refreshInventory(%this, %callback)
{
    if (!haveValidManagerHost())
    {
        warn(getScopeName() SPC "no valid manager, not doing this");
        return ;
    }
    %request = safeEnsureScriptObject("ManagerRequest", "GetStoreInventory");
    %request.callback = %callback;
    %request.storeName = %this.storeName;
    %request.store = %this;
    %url = $Net::ClientServiceURL @ "/GetStoreInventory?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "storeName=" @ urlEncode(%this.storeName);
    %request.setURL(%url);
    %request.start();
    return ;
}
function GetStoreInventory::onDone(%this)
{
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() SPC "- status =" SPC %status SPC "url =" SPC %this.getURL());
    if (!(%status $= "success"))
    {
        %message = %this.getValue("statusMsg");
        error(getScopeName() SPC "- status =" SPC %status SPC "\"" @ %message @ "\"");
        %cmd = %this.callback @ "( 0, " @ %status @ ");";
        eval(%cmd);
        return ;
    }
    %storeInfo = %this.store;
    %storeInfo.storeName = %this.storeName;
    %storeInfo.inventoryRevision = %this.getValue("storeRevisionDate");
    %count = %this.getValue("itemsCount");
    %storeInfo.Inventory.empty();
    %index = 0;
    while (%index < %count)
    {
        %sku = %this.getValue("items" @ %index @ ".sku");
        %item = SkuManager.findBySku(%sku);
        %quantity = %this.getValue("items" @ %index @ ".quantity");
        %priceVPoints = %this.getValue("items" @ %index @ ".priceVPoints");
        %priceVBux = %this.getValue("items" @ %index @ ".priceVBux");
        if (!%item)
        {
            warn("Inventory", getScopeName() SPC "- Unknown SKU returned from server. SKU =" SPC %sku);
        }
        else
        {
            %item.quantityInStore = %quantity;
            %item.priceVPoints = %priceVPoints;
            %item.priceVBux = %priceVBux;
            %storeInfo.Inventory.push_back(%sku, %item);
        }
        %index = %index + 1;
    }
    %cmd = %this.callback @ "(" @ %storeInfo @ ", \"success\");";
    eval(%cmd);
    %this.schedule(0, "delete");
    return ;
}
function GetStoreInventory::onError(%this, %unused, %errorName)
{
    %this.callback(0, %errorName);
    %this.schedule(0, "delete");
    return ;
}
function Emporium::purchase(%this, %skulist, %currency, %callback)
{
    %this.purchaseCollated(%skulist, %currency, %callback);
    return ;
}
function Emporium::purchaseCollated(%this, %skulist, %currency, %callback)
{
    %currency = strlwr(%currency);
    %this.shoppingList = %skulist;
    %purchaseArray = new Array();
    echoDebug(getScopeName());
    while (!(%skulist $= ""))
    {
        %sku = firstWord(%skulist);
        %skulist = restWords(%skulist);
        %index = %purchaseArray.getIndexFromKey(%sku);
        echo("Found sku=" @ %sku @ " at index " @ %index);
        if (%index < 0)
        {
            %purchaseArray.push_back(%sku, 1);
        }
        else
        {
            %count = %purchaseArray.getValue(%index);
            %count = %count + 1;
            %purchaseArray.setValue(%count, %index);
        }
    }
    %request = safeEnsureScriptObject("ManagerRequest", "PurchaseInventory");
    %request.callback = %callback;
    %request.store = %this;
    %url = $Net::ClientServiceURL @ "/PurchaseInventory?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "storeRevisionDate=" @ urlEncode(%this.inventoryRevision) @ "&" @ "payWith=" @ urlEncode(%currency) @ "&" @ "storeName=" @ urlEncode(%this.storeName) @ "&";
    %count = %purchaseArray.count();
    %url = %url @ "itemsToBuyCount=" @ %count;
    %index = 0;
    while (%index < %count)
    {
        %sku = %purchaseArray.getKey(%index);
        %qty = %purchaseArray.getValue(%index);
        %url = %url @ "&" @ "itemsToBuy" @ %index @ ".sku=" @ %sku @ "&" @ "itemsToBuy" @ %index @ ".quantity=" @ %qty;
        %index = %index + 1;
    }
    %request.setURL(%url);
    %request.start();
    %request.purchaseArray = %purchaseArray;
    return ;
}
function Emporium::purchaseUncollated(%this, %skulist, %currency, %callback)
{
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
    while (%index < %count)
    {
        %sku = getWord(%skulist, %index);
        %url = %url @ "itemsToBuy" @ %index @ ".sku=" @ urlEncode(%sku) @ "&";
        %url = %url @ "itemsToBuy" @ %index @ ".quantity=" @ urlEncode(1) @ "&";
        %index = %index + 1;
    }
    %request.setURL(%url);
    %request.start();
    return ;
}
function PurchaseInventory::onDone(%this)
{
    %skuStatuslist = "";
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() SPC "- status =" SPC %status SPC "url =" SPC %this.getURL());
    %count = %this.getValue("itemsCount");
    %index = 0;
    while (%index < %count)
    {
        %sku = %this.getValue("items" @ %index @ ".sku");
        %result = %this.getValue("items" @ %index @ ".validationResults");
        %qty = %this.purchaseArray.get(%sku);
        if (%qty $= "")
        {
            error(getScopeName() SPC "- sku not found:" SPC %sku SPC %this.getURL());
            %qty = 1;
        }
        %value = "";
        %delim = "";
        %n = 0;
        while (%n < %qty)
        {
            %value = %value @ %delim @ %sku @ "|" @ %result;
            %delim = " ";
            %n = %n + 1;
        }
        if (%index == 0)
        {
            %skuStatuslist = %value;
        }
        else
        {
            %skuStatuslist = %skuStatuslist SPC %value;
        }
        %index = %index + 1;
    }
    %cmd = %this.callback @ "(" @ %status @ ", \"" @ %skuStatuslist @ "\");";
    eval(%cmd);
    %this.purchaseArray.delete();
    %this.schedule(0, "delete");
    return ;
}
function PurchaseInventory::onError(%this, %unused, %errorName)
{
    %cmd = %this.callback @ "( \"error\", " @ %errorName @ ");";
    eval(%cmd);
    %this.schedule(0, "delete");
    return ;
}
