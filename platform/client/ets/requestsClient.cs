function sendRequest_ClientHeartbeat(%userName, %callbackHandler)
{
    %request = safeEnsureScriptObject("ManagerRequest", "");
    %request.bindClassName("UniformManagerRequest");
    %request.setName("request_ClientHeartbeat");
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/ClientHeartbeat";
    %request.setURL(%url);
    %request.addUserAndToken(%userName);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
}
function sendRequest_CompleteClientRegistration(%registrationID, %callbackHandler)
{
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setName("request_CompleteClientRegistration");
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/CompleteClientRegistration";
    %request.setURL(%url);
    %request.addUrlParam("registrationID", %registrationID);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
}
function sendRequest_GetBalancesAndScores(%userName, %callbackHandler)
{
    %request = safeEnsureScriptObject("ManagerRequest", "");
    %request.bindClassName("UniformManagerRequest");
    %request.setName("request_GetBalancesAndScores");
    if (%request.isOpen())
    {
        warn("network", getScopeName() SPC "- got overlapping requests. postponing. url =" SPC %request.getURL());
        return ;
    }
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/GetBalancesAndScores";
    %request.setURL(%url);
    %request.addUserAndToken(%userName);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
}
function sendRequest_GetCustomSpaceInfo(%buildingName, %spaceName, %ownerName, %callbackHandler)
{
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/GetCustomSpaceInfo";
    %request.setURL(%url);
    %request.addUserAndToken($Player::Name);
    if ((%buildingName $= "") && (%ownerName $= ""))
    {
        error(getScopeName() SPC "- either buildingName or ownerName must have a value." SPC getTrace());
        return "";
    }
    %request.setURLParamIfNotEmpty("building", %buildingName);
    %request.setURLParamIfNotEmpty("space", %spaceName);
    %request.setURLParamIfNotEmpty("owner", %ownerName);
    %request.setCompletedCallback(%callbackHandler);
    %request.start();
    return %request;
}
function sendRequest_GetClientUserProperties(%userName, %callbackHandler)
{
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setName("request_GetClientUserProperties");
    %url = "";
    %url = %url @ $Net::SecureClientServiceURL;
    %url = %url @ "/GetClientUserProperties";
    %request.setURL(%url);
    %request.addUserAndToken(%userName);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
}
function sendRequest_GetStoreInventory(%userName, %storename, %callbackHandler)
{
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setName("request_GetStoreInventory");
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/GetStoreInventory";
    %request.setURL(%url);
    %request.addUserAndToken(%userName);
    %request.addUrlParam("storeName", %storename);
    %request.storeName = %storename;
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
}
function sendRequest_GetUserInventoryCollection(%userName, %collectionName, %callbackHandler)
{
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setName("request_GetUserInventoryCollection");
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/GetUserInventoryCollection";
    %request.setURL(%url);
    %request.addUserAndToken(%userName);
    %request.addUrlParam("name", %collectionName);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
}
function sendRequest_GetUserRelations(%userName, %singleUserName, %callbackHandler)
{
    %requestName = "request_GetUserRelations";
    if ((%singleUserName $= "") && isObject(%requestName))
    {
        if (%requestName.doAnother)
        {
            echo(getScopeName() SPC "- got overlapping requests, dropping intermediate." SPC getTrace());
        }
        else
        {
            echo(getScopeName() SPC "- got overlapping requests." SPC getTrace());
        }
        %requestName.doAnother = 1;
        return "";
    }
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setName(%requestName);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/GetUserRelations";
    %request.setURL(%url);
    %request.addUserAndToken(%userName);
    if (!(%singleUserName $= ""))
    {
        %request.addUrlParam("buddy", %singleUserName);
    }
    %request.callbackHandler = %callbackHandler;
    %request.singleUserName = %singleUserName;
    %request.doAnother = 0;
    %request.start();
    return %request;
}
function sendRequest_GetOnlineFriends(%maxCount, %sortCriteria, %callbackHandler)
{
    %requestName = "request_GetOnlineFriends";
    if (isObject(%requestName))
    {
        if (%requestName.doAnother)
        {
            echo(getScopeName() SPC "- got overlapping requests, dropping intermediate." SPC getTrace());
        }
        else
        {
            echo(getScopeName() SPC "- got overlapping requests." SPC getTrace());
        }
        %requestName.doAnother = 1;
        return "";
    }
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setName(%requestName);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/GetOnlineFriends";
    %request.setURL(%url);
    %request.addUserAndToken($Player::Name);
    if (!(%maxCount $= ""))
    {
        %request.addUrlParam("maxCount", %maxCount);
    }
    if (!(%sortCriteria $= ""))
    {
        %request.addUrlParam("sortCriteria", %sortCriteria);
    }
    %request.callbackHandler = %callbackHandler;
    %request.doAnother = 0;
    %request.start();
    return %request;
}
function sendRequest_GetOnlineUsers(%maxCount, %callbackHandler)
{
    %requestName = "request_GetOnlineUsers";
    if (isObject(%requestName))
    {
        if (%requestName.doAnother)
        {
            echo(getScopeName() SPC "- got overlapping requests, dropping intermediate." SPC getTrace());
        }
        else
        {
            echo(getScopeName() SPC "- got overlapping requests." SPC getTrace());
        }
        %requestName.doAnother = 1;
        return "";
    }
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setName(%requestName);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/finder/GetOnlineUsers";
    %request.setURL(%url);
    %request.addUserAndToken($Player::Name);
    %request.callbackHandler = %callbackHandler;
    %request.doAnother = 0;
    %request.addUrlParam("maxCount", %maxCount);
    %isImplemented = 1;
    if (%isImplemented && !$StandAlone)
    {
        %request.start();
    }
    else
    {
        echo(getScopeName() SPC "- using fake data.");
        %num = 100;
        %request.putValue("status", "success");
        %request.putValue("userCount", %num);
        %n = 0;
        while (%n < %num)
        {
            %keyBase = "user" @ %n @ ".";
            %request.putValue(%keyBase @ "userName", getRandomUserName());
            %request.putValue(%keyBase @ "relationType", getRandom(0, 99) < 20 ? "friend" : "");
            %request.putValue(%keyBase @ "age", getRandom(0, 1) == 0 ? getRandom(13, 25) : "");
            %request.putValue(%keyBase @ "currentActivities", getRandomWord("idle dancing chatting shoppingForClothes decorating  "));
            %request.putValue(%keyBase @ "currentLocation.areaName", "lga_yachts");
            %request.putValue(%keyBase @ "currentLocation.buildingName", "LGAHarbor");
            %request.putValue(%keyBase @ "currentLocation.serverName", "Yacht_LargeSouth");
            %request.putValue(%keyBase @ "levelName", "Da Shiznit");
            %request.putValue(%keyBase @ "gender", getRandomWord("f m"));
            %request.putValue(%keyBase @ "headline", "4 times the timmy 100% less fat 100% muscle ;)");
            %request.putValue(%keyBase @ "onlineStatus", "InworldOnEnvserver");
            %request.putValue(%keyBase @ "score", 694040);
            %request.putValue(%keyBase @ "homeLocation.buildingName", "LGAHarbor");
            %n = %n + 1;
        }
        %n = 0;
        %request.putValue("location" @ %n @ ".areaName", "nv");
        %request.putValue("location" @ %n @ ".userCount", getRandom(30, 800));
        %n = %n + 1;
        %request.putValue("location" @ %n @ ".areaName", "rj");
        %request.putValue("location" @ %n @ ".userCount", getRandom(30, 800));
        %n = %n + 1;
        %request.putValue("location" @ %n @ ".areaName", "lga");
        %request.putValue("location" @ %n @ ".userCount", getRandom(30, 800));
        %n = %n + 1;
        %request.putValue("location" @ %n @ ".areaName", "pvt");
        %request.putValue("location" @ %n @ ".userCount", getRandom(50, 1500));
        %n = %n + 1;
        %request.putValue("location" @ %n @ ".areaName", "gw");
        %request.putValue("location" @ %n @ ".userCount", getRandom(0, 30));
        %n = %n + 1;
        %request.putValue("locationCount", %n);
        %request.schedule(500, "onDoneOrError");
    }
    return %request;
}
function sendRequest_GetHappeningsInProgress(%userName, %callbackHandler)
{
    %requestName = "request_GetHappeningsInProgress";
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setName(%requestName);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/finder/GetHappeningsInProgress";
    %request.setURL(%url);
    %request.addUserAndToken(%userName);
    %request.callbackHandler = %callbackHandler;
    %request.doAnother = 0;
    %request.start();
    return %request;
}
function sendRequest_PurchaseInventory(%userName, %skusArray, %payWith, %storename, %callbackHandler)
{
    %payWith = strlwr(%payWith);
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setName("request_PurchaseInventory");
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/PurchaseInventory";
    %request.setURL(%url);
    %request.addUserAndToken(%userName);
    %request.addUrlParam("payWith", %payWith);
    %request.addUrlParam("storeName", %storename);
    %request.addUrlParam("storeRevisionDate", $gStoreStockRevision[%storename]);
    %skusNum = %skusArray.count();
    %request.addUrlParam("itemsToBuyCount", %skusNum);
    %n = 0;
    while (%n < %skusNum)
    {
        if (%skusArray.getValue(%n) != 1)
        {
            error("trying to buy a non-unit quantity of a sku." SPC %skusArray.getKey(%n) SPC %skusArray.getValue(%n));
            %skusArray.setValue(%n, 1);
        }
        %n = %n + 1;
    }
    %n = 0;
    while (%n < %skusNum)
    {
        %request.addBodyParam("itemsToBuy" @ %n @ ".sku", %skusArray.getKey(%n));
        %request.addBodyParam("itemsToBuy" @ %n @ ".quantity", %skusArray.getValue(%n));
        %n = %n + 1;
    }
    %request.payWith = %payWith;
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
}
function sendRequest_RemoveUserInventoryCollection(%userName, %collectionName, %callbackHandler)
{
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setName("request_RemoveUserInventoryCollection");
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/RemoveUserInventoryCollection";
    %request.setURL(%url);
    %request.addUserAndToken(%userName);
    %request.addUrlParam("name", %collectionName);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
}
function sendRequest_SaveClientUserProperties(%userName, %stringMap, %callbackHandler)
{
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::SecureClientServiceURL;
    %url = %url @ "/SaveClientUserProperties";
    %request.setURL(%url);
    %request.setBodyParam("user", $Player::Name);
    %request.setURLParam("token", $Token);
    %num = %stringMap.size();
    %request.setBodyParam("propertyCount", %num);
    %n = 0;
    while (%n < %num)
    {
        %request.setBodyParam("property" @ %n @ ".key", %stringMap.getKey(%n));
        %request.setBodyParam("property" @ %n @ ".value", %stringMap.getValue(%n));
        %n = %n + 1;
    }
    %request.setCompletedCallback(%callbackHandler);
    %request.start();
    return %request;
}
function sendRequest_UpdateUserInventoryCollection(%userName, %collectionName, %propertyMap, %callbackHandler)
{
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setName("request_UpdateUserInventoryCollection");
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/UpdateUserInventoryCollection";
    %request.setURL(%url);
    %request.addUserAndToken(%userName);
    %request.addUrlParam("name", %collectionName);
    %num = %propertyMap.size();
    %request.addUrlParam("propertyCount", %num);
    %n = 0;
    while (%n < %num)
    {
        %request.addUrlParam("property" @ %n @ ".key", %propertyMap.getKey(%n));
        %request.addUrlParam("property" @ %n @ ".value", %propertyMap.getValue(%n));
        %n = %n + 1;
    }
    %request.callbackHandler = %callbackHandler;
    if ($StandAlone)
    {
        if (!(%callbackHandler $= ""))
        {
            warn(getScopeName() SPC "- standalone: faking success" SPC getTrace());
            %request.putValue("status", "success");
            schedule(500, 0, %callbackHandler, %request);
        }
    }
    else
    {
        %request.start();
    }
    return %request;
}
function sendRequest_GetHighGameScores(%userName, %gameName, %firstIndex, %maxCount, %callbackHandler)
{
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setName("request_GetHighGameScores");
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/score/GetHighGameScores";
    %request.setURL(%url);
    %request.addUserAndToken(%userName);
    %request.addUrlParam("gameName", %gameName);
    %request.addUrlParam("firstIndex", %firstIndex);
    %request.addUrlParam("maxCount", %maxCount);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
}
function sendRequest_GetHighGameScoresForStation(%userName, %gameStationId, %firstIndex, %maxCount, %callbackHandler)
{
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setName("request_GetHighGameScoresForStation");
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/score/GetHighGameScoresForStation";
    %request.setURL(%url);
    %request.addUserAndToken(%userName);
    %request.addUrlParam("gameStationId", %gameStationId);
    %request.addUrlParam("firstIndex", %firstIndex);
    %request.addUrlParam("maxCount", %maxCount);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
}
function sendRequest_GetMainHappenings(%maxCount, %callbackHandler)
{
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/finder/GetMainHappenings";
    %request.setURL(%url);
    %request.addUserAndToken($Player::Name);
    %request.setURLParam("maxCount", %maxCount);
    %request.setCompletedCallback(%callbackHandler);
    %request.start();
    return %request;
}
function sendRequest_GetMainVenues(%maxCount, %callbackHandler)
{
    %requestName = "request_GetMainVenues";
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setName(%requestName);
    %request.timeStart = getSimTime();
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/finder/GetMainVenues";
    %url = %url @ "FAKE";
    %request.setURL(%url);
    %request.addUserAndToken($Player::Name);
    %request.addUrlParam("maxCount", %maxCount);
    %request.callbackHandler = %callbackHandler;
    %request.putValue("status", "success");
    %request.putValue("venuesCount", %maxCount);
    %n = 0;
    %venue = "interscope_lounge";
    %notThese = %venue;
    fakeRequestListItem_GetMainVenues(%request, "venues", %n, %venue);
    %n = %n + 1;
    %venue = DestinationList::GetRandomDestinationForTGF("venue", %notThese);
    %notThese = %notThese SPC %venue;
    fakeRequestListItem_GetMainVenues(%request, "venues", %n, %venue);
    %n = %n + 1;
    %venue = DestinationList::GetRandomDestinationForTGF("shop", %notThese);
    %notThese = %notThese SPC %venue;
    fakeRequestListItem_GetMainVenues(%request, "venues", %n, %venue);
    %n = %n + 1;
    %n = %n;
    while (%n < %maxCount)
    {
        if (getRandom(0, 2) == 0)
        {
            %type = "venue";
        }
        else
        {
            if (getRandom(0, 2) == 1)
            {
                %type = "shop";
            }
            else
            {
                if (getRandom(0, 2) == 2)
                {
                    %type = "residence";
                }
                else
                {
                    %type = "venue";
                }
            }
        }
        %venue = DestinationList::GetRandomDestinationForTGF(%type, %notThese);
        %notThese = %notThese SPC %venue;
        fakeRequestListItem_GetMainVenues(%request, "venues", %n, %venue);
        %n = %n + 1;
    }
    %request.schedule(100, "onDoneOrError");
    return ;
}
function fakeRequestListItem_GetMainVenues(%request, %listNameBase, %listIndex, %venueCodeName)
{
    %request.putValue(%listNameBase @ %listIndex @ ".codeName", %venueCodeName);
    return ;
}
function sendRequest_AbuseReport(%abuser, %description, %occurrence, %abuseType, %chatSnippetFile, %callbackHandler)
{
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/AbuseReport";
    %request.setURL(%url);
    %request.addUserAndToken($Player::Name);
    %request.setURLParam("abuser", %abuser);
    %request.setURLParam("description", %description);
    %request.setURLParam("occurrence", %occurrence);
    %request.setURLParam("abuseType", %abuseType);
    %request.setPostFile("chatSnippet", %chatSnippetFile);
    %request.setCompletedCallback(%callbackHandler);
    %request.start();
    return %request;
}
function sendRequest_BootNew(%callbackHandler)
{
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::SecureClientServiceURL;
    %url = %url @ "/Boot";
    %request.setURL(%url);
    %request.setURLParam("user", $Player::Name);
    %request.setBodyParam("password", $Player::Password);
    %request.setCompletedCallback(%callbackHandler);
    %request.start();
    return %request;
}
function sendRequest_Boot(%callbackHandler)
{
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %url = "";
    %url = %url @ $Net::SecureClientServiceURL;
    %url = %url @ "/Boot";
    %request.setURL(%url);
    %request.addUrlParam("user", $Player::Name);
    %request.addBodyParam("password", $Player::Password);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
}
function sendRequest_UpdateUserStates(%statesList, %callbackHandler)
{
    %callbackHandler = isDefined("%callbackHandler") ? %callbackHandler : "";
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/UpdateUserStates";
    %request.setURL(%url);
    %request.addUserAndToken($Player::Name);
    %request.addUrlParam("userStates", %statesList);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
}
function sendRequest_EventInformation(%eventId, %callbackHandler)
{
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setName("request_EventInformation");
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/GetEventBanner";
    %request.setURL(%url);
    %request.addUrlParam("eventId", %eventId);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
}
function sendRequest_GiftCurrency(%targetUserName, %currencyType, %currencyAmount, %dryRun, %callbackHandler)
{
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::SecureClientServiceURL;
    %url = %url @ "/TransferCurrency";
    %request.setURL(%url);
    %request.addUserAndToken($Player::Name);
    %request.setBodyParam("password", MD5($Player::Password));
    %request.setURLParam("payee", %targetUserName);
    %request.setURLParam("currencyType", %currencyType $= "vPoints" ? "VPOINTS" : "VBUX");
    %request.setURLParam("amount", %currencyAmount);
    %request.setURLParam("dryRun", %dryRun, 1);
    %request.setCompletedCallback(%callbackHandler);
    %request.start();
    return %request;
}
function sendRequest_GetUserProfileInfo(%targetUser, %callbackHandler)
{
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::SecureClientServiceURL;
    %url = %url @ "/GetUserProfileInfo";
    %request.setURL(%url);
    %request.addUserAndToken($Player::Name);
    %request.setURLParam("targetUser", %targetUser);
    %request.setCompletedCallback(%callbackHandler);
    %request.start();
    return %request;
}
function sendRequest_UploadPhoto(%fileName, %caption, %peopleInViewList, %type, %location, %featured, %callbackHandler)
{
    %featured = %featured ? "true" : "false";
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/uploadPhoto";
    %request.setURL(%url);
    %request.addUserAndToken($Player::Name);
    %request.setURLParam("caption", %caption);
    %request.setURLParam("featured", %featured);
    %request.setURLParam("broadcast", "");
    %request.setURLParam("type", %type);
    %request.setURLParam("location", %location);
    %request.setURLParam("inView", %peopleInViewList);
    %request.setPostFile("imageBody", %fileName);
    if (!(CustomSpaceClient::GetSpaceImIn() $= ""))
    {
        %request.setURLParam("apartmentOwner", $CSSpaceInfo.owner);
        %request.setURLParam("vurl", $CSSpaceInfo.vurl);
    }
    else
    {
        %request.setURLParam("vurl", "vside:/location/" @ $gContiguousSpaceName @ "/PlazaSpawns");
    }
    %request.setCompletedCallback(%callbackHandler);
    %request.start();
    return %request;
}
function sendRequest_PublishToTicker(%message, %priority, %callbackHandler)
{
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/PublishToTicker";
    %request.setURL(%url);
    %request.addUserAndToken($Player::Name);
    %request.setCompletedCallback(%callbackHandler);
    %request.setURLParam("text", %message);
    %request.setURLParam("priority", %priority);
    %isImplementedOnBackEnd = 1;
    if (%isImplementedOnBackEnd && !$StandAlone)
    {
        %request.start();
    }
    else
    {
        echo(getScopeName() SPC "- using fake data. yep");
        %request.setResult("status", "success");
        %request.schedule(500, "onDoneOrError");
    }
    return %request;
}
