$Player::furnitureInventory = new Array();
$Player::bFakeFurnitureInventory = 0;
function getFurnitureSkus()
{
    %count = $Player::furnitureInventory.count();
    %skulist = "";
    %index = 0;
    while (%index < %count)
    {
        %sku = $Player::furnitureInventory.getKey(%index);
        if (%index != 0)
        {
            %skulist = %skulist SPC %sku;
        }
        else
        {
            %skulist = %sku;
        }
        %index = %index + 1;
    }
    return %skulist;
}
function addFurnitureSku(%sku, %quantity)
{
    %index = $Player::furnitureInventory.getIndexFromKey(%sku);
    if (%index == -1)
    {
        $Player::furnitureInventory.push_back(%sku, %quantity SPC 0);
        return ;
    }
    %value = $Player::furnitureInventory.getValue(%index);
    %owned = getWord(%value, 0);
    %inUse = getWord(%value, 1);
    %owned = %owned + %quantity;
    %value = %owned SPC %inUse;
    $Player::furnitureInventory.setValue(%value, %index);
    return ;
}
function removeFurnitureSku(%sku, %quantity)
{
    %index = $Player::furnitureInventory.getIndexFromKey(%sku);
    if (%index == -1)
    {
        echo("Player does not own sku #" @ %sku);
        return ;
    }
    %value = $Player::furnitureInventory.getValue(%index);
    %owned = getWord(%value, 0);
    %inUse = getWord(%value, 1);
    if (%owned <= %quantity)
    {
        if (%owned < %quantity)
        {
            echo("Player only onws " @ %owned @ " items of type " @ %sku @ ". Removing them all.");
        }
        $Player::furnitureInventory.erase(%index);
        return ;
    }
    %owned = %owned - %quantity;
    if (%owned < %inUse)
    {
        log("inventory", "warn", "Player now owns fewer (" @ %owned @ ") items of type " @ %sku @ " than are in use (" @ %inUse @ ")");
    }
    %value = %owned SPC %inUse;
    $Player::furnitureInventory.setValue(%value, %index);
    return ;
}
function removeAllFurnitureSku(%sku)
{
    %index = $Player::furnitureInventory.getIndexFromKey(%sku);
    if (%index == -1)
    {
        echo("Player does not own sku #" @ %sku);
        return ;
    }
    $Player::furnitureInventory.erase(%index);
    return ;
}
function numOwnedFurnitureSku(%sku)
{
    %index = $Player::furnitureInventory.getIndexFromKey(%sku);
    if (%index == -1)
    {
        return 0;
    }
    %value = $Player::furnitureInventory.getValue(%index);
    %owned = getWord(%value, 0);
    return %owned;
}
function numUsingFurnitureSku(%sku)
{
    %index = $Player::furnitureInventory.getIndexFromKey(%sku);
    if (%index == -1)
    {
        return 0;
    }
    %value = $Player::furnitureInventory.getValue(%index);
    %inUse = getWord(%value, 1);
    return %inUse;
}
function numUsingFurnitureAll()
{
    %total = 0;
    %count = $Player::furnitureInventory.count();
    %index = 0;
    while (%index < %count)
    {
        %value = $Player::furnitureInventory.getValue(%index);
        %inUse = getWord(%value, 1);
        %total = %total + %inUse;
        %index = %index + 1;
    }
    return %total;
}
function useFurnitureSku(%sku, %quantity)
{
    %index = $Player::furnitureInventory.getIndexFromKey(%sku);
    if (%index == -1)
    {
        log("inventory", "warn", "Player does not own " @ %sku);
        return ;
    }
    %value = $Player::furnitureInventory.getValue(%index);
    %owned = getWord(%value, 0);
    %inUse = getWord(%value, 1);
    if (%owned < %quantity)
    {
        log("inventory", "warn", "Player does not own " @ %quantity @ " of " @ %sku @ "(" @ %owned @ ")");
        %quantity = %owned;
    }
    %value = %owned SPC %quantity;
    echo("putting " @ %value @ " for sku=" @ %sku);
    $Player::furnitureInventory.setValue(%value, %index);
    return ;
}
function useAnotherFurnitureSku(%sku)
{
    %index = $Player::furnitureInventory.getIndexFromKey(%sku);
    if (%index == -1)
    {
        log("inventory", "warn", "Player does not own " @ %sku);
        return 0;
    }
    %value = $Player::furnitureInventory.getValue(%index);
    %owned = getWord(%value, 0);
    %inUse = getWord(%value, 1);
    if (%owned == %inUse)
    {
        log("inventory", "warn", "No more " @ %sku @ " available");
        return 0;
    }
    else
    {
        if (%owned < %inUse)
        {
            log("inventory", "error", "More of " @ %sku @ " in use than owned!");
            return 0;
        }
    }
    %inUse = %inUse + 1;
    %value = %owned SPC %inUse;
    $Player::furnitureInventory.setValue(%value, %index);
    return 1;
}
function putAwayAnotherFurnitureSku(%sku)
{
    %index = $Player::furnitureInventory.getIndexFromKey(%sku);
    if (%index == -1)
    {
        log("inventory", "warn", "Player does not own " @ %sku);
        return 0;
    }
    %value = $Player::furnitureInventory.getValue(%index);
    %owned = getWord(%value, 0);
    %inUse = getWord(%value, 1);
    if (%inUse == 0)
    {
        log("inventory", "warn", "No more " @ %sku @ " available");
        return 0;
    }
    else
    {
        if (%inUse < 0)
        {
            log("inventory", "error", "Negative number (" @ %inUse @ ") of " @ %sku @ " in use!");
            return 0;
        }
    }
    %inUse = %inUse - 1;
    %value = %owned SPC %inUse;
    $Player::furnitureInventory.setValue(%value, %index);
    return 1;
}
function putAwayAllFurniture()
{
    %count = $Player::furnitureInventory.count();
    %index = 0;
    while (%index < %count)
    {
        %value = $Player::furnitureInventory.getValue(%index);
        %owned = getWord(%value, 0);
        $Player::furnitureInventory.setValue(%owned SPC 0, %index);
        %index = %index + 1;
    }
}

function dumpFurniture()
{
    %count = $Player::furnitureInventory.count();
    %index = 0;
    while (%index < %count)
    {
        %val = $Player::furnitureInventory.getValue(%index);
        %sku = $Player::furnitureInventory.getKey(%index);
        %si = SkuManager.findBySku(%sku);
        echo(%sku @ " - owned: " @ getWord(%val, 0) @ ", in use: " @ getWord(%val, 1) @ " (" @ %si.descShrt @ ")");
        %index = %index + 1;
    }
}

function dumpFurnitureInUse()
{
    %count = $Player::furnitureInventory.count();
    %index = 0;
    while (%index < %count)
    {
        %val = $Player::furnitureInventory.getValue(%index);
        %sku = $Player::furnitureInventory.getKey(%index);
        %numInUse = getWord(%val, 1);
        if (%numInUse > 0)
        {
            %si = SkuManager.findBySku(%sku);
            echo(%sku @ " - owned: " @ getWord(%val, 0) @ ", in use: " @ getWord(%val, 1) @ " (" @ %si.descShrt @ ")");
        }
        %index = %index + 1;
    }
}

function clearOwnedFurniture()
{
    $Player::furnitureInventory.empty();
    return ;
}
function getOwnedFurniture()
{
    %request = safeEnsureScriptObject("ManagerRequest", "FurnitureRequest");
    if (%request.isOpen())
    {
        warn("network", getScopeName() SPC "- got overlapping requests. postponing. url =" SPC %request.getURL());
        %request.doAnother = 1;
        return ;
    }
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %url = $Net::ClientServiceURL @ "/GetUserInventory?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "skuType=furnishing";
    log("network", "debug", "FurnitureRequest: " @ %url);
    %request.setURL(%url);
    if ($StandAlone && $Player::bFakeFurnitureInventory)
    {
        %request.schedule(1000, "fakeOnDone");
    }
    else
    {
        %request.start();
    }
    return ;
}
function FurnitureRequest::onError(%this, %unused, %unused)
{
    return ;
}
function FurnitureRequest::onDone(%this)
{
    if ((CustomSpaceClient::GetSpaceImIn() $= "") && !CustomSpaceClient::isOwner())
    {
        return ;
    }
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() SPC "- status =" SPC %status SPC "url =" SPC %this.getURL());
    if (!(%status $= "success"))
    {
        error(getScopeName() SPC "- status =" SPC %status);
        return ;
    }
    clearOwnedFurniture();
    %count = %this.getValue("itemsCount");
    if (%count < 1)
    {
        log("network", "debug", getScopeName() SPC "- Nothing in furniture inventory.");
        return ;
    }
    %index = 0;
    while (%index < %count)
    {
        %sku = %this.getValue("items" @ %index @ ".sku");
        %qty = %this.getValue("items" @ %index @ ".quantity");
        addFurnitureSku(%sku, %qty);
        %index = %index + 1;
    }
    %request = safeNewScriptObject("ScriptObject", "Request_GetActiveFurnitureSkus", 1);
    %request.result = "";
    commandToServer('GetActiveFurnitureSkus', CustomSpaceClient::GetSpaceImIn(), %request.getId());
    return ;
}
function clientCmdOnFurniturePlaced(%sku, %quantity)
{
    putIntoUseFurnitureSku(%sku, %quantity, 1);
    return ;
}
$gGotFurnitureCallback = "";
function clientCmdGotFurnitureSkus(%skulistchunk, %requestId, %complete)
{
    if (!isObject(%requestId))
    {
        warn("network", "results for deleted request: GotFurnitureSkus");
        return ;
    }
    %requestId.result = %requestId.result @ %skulistchunk;
    if (%complete == 0)
    {
        return ;
    }
    %skulist = %requestId.result;
    %requestId.delete();
    while (!(%skulist $= ""))
    {
        %value = firstWord(%skulist);
        %skulist = restWords(%skulist);
        %value = strreplace(%value, "|", " ");
        %sku = getWord(%value, 0);
        %quantity = getWord(%value, 1);
        useFurnitureSku(%sku, %quantity);
    }
    if (!($gGotFurnitureCallback $= ""))
    {
        eval($gGotFurnitureCallback);
    }
    return ;
}
function refreshActiveFurniture()
{
    putAwayAllFurniture();
    %request = safeNewScriptObject("ScriptObject", "Request_GetActiveFurnitureSkus", 1);
    %request.result = "";
    commandToServer('GetActiveFurnitureSkus', CustomSpaceClient::GetSpaceImIn(), %request.getId());
    return ;
}
function getFurnitureStore(%callback)
{
    getEmporium("furnishings", %callback);
    return ;
}
function getNuggetIdList(%callback)
{
    %request = safeNewScriptObject("ScriptObject", "Request_CSGetNuggetIdList", 1);
    %request.result = "";
    %request.callback = %callback;
    commandToServer('CSGetNuggetIdList', CustomSpaceClient::GetSpaceImIn(), %request.getId());
    return ;
}
function clientCmdGotNuggetIdList(%nuggetchunk, %requestId, %completed)
{
    if (!isObject(%requestId))
    {
        warn("network", "results for deleted request: GotNuggetIdList");
        return ;
    }
    %requestId.result = %requestId.result @ %nuggetchunk;
    if (%completed == 0)
    {
        return ;
    }
    %cmd = %requestId.callback @ "( \"" @ %requestId.result @ "\");";
    %requestId.delete();
    eval(%cmd);
    return ;
}
function getNuggetGhostList(%callback)
{
    %request = safeNewScriptObject("ScriptObject", "Request_CSGetNuggetGhostList", 1);
    %request.result = "";
    %request.callback = %callback;
    commandToServer('CSGetNuggetGhostList', CustomSpaceClient::GetSpaceImIn(), %request.getId());
    return ;
}
function clientCmdGotNuggetGhostList(%ghostchunk, %requestId, %completed)
{
    if (!isObject(%requestId))
    {
        warn("network", "results for deleted request: GotNuggetGhostList");
        return ;
    }
    %requestId.result = %requestId.result @ %ghostchunk;
    if (%completed == 0)
    {
        return ;
    }
    %ghostlist = %requestId.result;
    %objectList = "";
    while (!(%ghostlist $= ""))
    {
        %ghostID = firstWord(%ghostlist);
        %ghostlist = restWords(%ghostlist);
        %objID = ServerConnection.resolveGhostID(%ghostID);
        %objectList = %objectList SPC %objID;
    }
    %objectList = trim(%objectList);
    %requestId.delete();
    CSFurnitureMover::refreshGhostList(%objectList);
    return ;
}
function FurnitureRequest::fakeOnDone(%this)
{
    %this.putValue("status", "success");
    %fakeInventory = SkuManager.getSkusType("furnishing");
    %count = getWordCount(%fakeInventory);
    %this.putValue("itemsCount", %count);
    %idx = 0;
    while (%idx < %count)
    {
        %sku = getWord(%fakeInventory, %idx);
        %qty = 50;
        %this.putValue("items" @ %idx @ ".sku", %sku);
        %this.putValue("items" @ %idx @ ".quantity", %qty);
        %idx = %idx + 1;
    }
    %this.onDone();
    return ;
}
