function sendRequest_ClientHeartbeat(%userName, %callbackHandler) {
    %request = safeEnsureScriptObject("ManagerRequest", "");
    "UniformManagerRequest".bindClassName(%request);
    "request_ClientHeartbeat".setName(%request);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/ClientHeartbeat";
    %url.setURL(%request);
    %userName.addUserAndToken(%request);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
};
function sendRequest_CompleteClientRegistration(%registrationID, %callbackHandler) {
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    "request_CompleteClientRegistration".setName(%request);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/CompleteClientRegistration";
    %url.setURL(%request);
    %registrationID.addUrlParam(%request, "registrationID");
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
};
function sendRequest_GetBalancesAndScores(%userName, %callbackHandler) {
    %request = safeEnsureScriptObject("ManagerRequest", "");
    "UniformManagerRequest".bindClassName(%request);
    "request_GetBalancesAndScores".setName(%request);
    if (%request.isOpen()) {
        warn("network", getScopeName() @ " " @ "- got overlapping requests. postponing. url =" @ " " @ %request.getURL());
        return;
    }
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/GetBalancesAndScores";
    %url.setURL(%request);
    %userName.addUserAndToken(%request);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
};
function sendRequest_GetCustomSpaceInfo(%buildingName, %spaceName, %ownerName, %callbackHandler) {
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/GetCustomSpaceInfo";
    %url.setURL(%request);
    $Player::Name.addUserAndToken(%request);
    if ((%buildingName $= "")) {
    }
    if ((%ownerName $= "")) {
        error(getScopeName() @ " " @ "- either buildingName or ownerName must have a value." @ " " @ getTrace());
        return "";
    }
    %buildingName.setURLParamIfNotEmpty(%request, "building");
    %spaceName.setURLParamIfNotEmpty(%request, "space");
    %ownerName.setURLParamIfNotEmpty(%request, "owner");
    %callbackHandler.setCompletedCallback(%request);
    %request.start();
    return %request;
};
function sendRequest_GetClientUserProperties(%userName, %callbackHandler) {
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    "request_GetClientUserProperties".setName(%request);
    %url = "";
    %url = %url @ $Net::SecureClientServiceURL;
    %url = %url @ "/GetClientUserProperties";
    %url.setURL(%request);
    %userName.addUserAndToken(%request);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
};
function sendRequest_GetStoreInventory(%userName, %storename, %callbackHandler) {
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    "request_GetStoreInventory".setName(%request);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/GetStoreInventory";
    %url.setURL(%request);
    %userName.addUserAndToken(%request);
    %storename.addUrlParam(%request, "storeName");
    %request.storeName = %storename;
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
};
function sendRequest_GetUserInventoryCollection(%userName, %collectionName, %callbackHandler) {
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    "request_GetUserInventoryCollection".setName(%request);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/GetUserInventoryCollection";
    %url.setURL(%request);
    %userName.addUserAndToken(%request);
    %collectionName.addUrlParam(%request, "name");
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
};
function sendRequest_GetUserRelations(%userName, %singleUserName, %callbackHandler) {
    %requestName = "request_GetUserRelations";
    if ((%singleUserName $= "")) {
    }
    if (isObject(%requestName)) {
        if (%requestName.doAnother) {
            echo(getScopeName() @ " " @ "- got overlapping requests, dropping intermediate." @ " " @ getTrace());
        }
        echo(getScopeName() @ " " @ "- got overlapping requests." @ " " @ getTrace());
        %requestName.doAnother = 1;
        return "";
    }
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    %requestName.setName(%request);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/GetUserRelations";
    %url.setURL(%request);
    %userName.addUserAndToken(%request);
    if (!(%singleUserName $= "")) {
        %singleUserName.addUrlParam(%request, "buddy");
    }
    %request.callbackHandler = %callbackHandler;
    %request.singleUserName = %singleUserName;
    %request.doAnother = 0;
    %request.start();
    return %request;
};
function sendRequest_GetOnlineFriends(%maxCount, %sortCriteria, %callbackHandler) {
    %requestName = "request_GetOnlineFriends";
    if (isObject(%requestName)) {
        if (%requestName.doAnother) {
            echo(getScopeName() @ " " @ "- got overlapping requests, dropping intermediate." @ " " @ getTrace());
        }
        echo(getScopeName() @ " " @ "- got overlapping requests." @ " " @ getTrace());
        %requestName.doAnother = 1;
        return "";
    }
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    %requestName.setName(%request);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/GetOnlineFriends";
    %url.setURL(%request);
    $Player::Name.addUserAndToken(%request);
    if (!(%maxCount $= "")) {
        %maxCount.addUrlParam(%request, "maxCount");
    }
    if (!(%sortCriteria $= "")) {
        %sortCriteria.addUrlParam(%request, "sortCriteria");
    }
    %request.callbackHandler = %callbackHandler;
    %request.doAnother = 0;
    %request.start();
    return %request;
};
function sendRequest_GetOnlineUsers(%maxCount, %callbackHandler) {
    %requestName = "request_GetOnlineUsers";
    if (isObject(%requestName)) {
        if (%requestName.doAnother) {
            echo(getScopeName() @ " " @ "- got overlapping requests, dropping intermediate." @ " " @ getTrace());
        }
        echo(getScopeName() @ " " @ "- got overlapping requests." @ " " @ getTrace());
        %requestName.doAnother = 1;
        return "";
    }
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    %requestName.setName(%request);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/finder/GetOnlineUsers";
    %url.setURL(%request);
    $Player::Name.addUserAndToken(%request);
    %request.callbackHandler = %callbackHandler;
    %request.doAnother = 0;
    %maxCount.addUrlParam(%request, "maxCount");
    %isImplemented = 1;
    if (%isImplemented) {
    }
    if (!($StandAlone)) {
        %request.start();
    }
    echo(getScopeName() @ " " @ "- using fake data.");
    %num = 100;
    "success".putValue(%request, "status");
    %num.putValue(%request, "userCount");
    %n = 0;
    while ((%n < %num)) {
        %keyBase = "user" @ %n @ ".";
        getRandomUserName().putValue(%request, %keyBase @ "userName");
        (getRandom(0, 99) < 20.0) ? "friend" : "".putValue(%request, %keyBase @ "relationType");
        if ((getRandom(0, 1) == 0.0)) {
        }
        "".putValue(%request, %keyBase @ "age", getRandom(13, 25));
        getRandomWord("idle dancing chatting shoppingForClothes decorating  ").putValue(%request, %keyBase @ "currentActivities");
        "lga_yachts".putValue(%request, %keyBase @ "currentLocation.areaName");
        "LGAHarbor".putValue(%request, %keyBase @ "currentLocation.buildingName");
        "Yacht_LargeSouth".putValue(%request, %keyBase @ "currentLocation.serverName");
        "Da Shiznit".putValue(%request, %keyBase @ "levelName");
        getRandomWord("f m").putValue(%request, %keyBase @ "gender");
        "4 times the timmy 100% less fat 100% muscle ;)".putValue(%request, %keyBase @ "headline");
        "InworldOnEnvserver".putValue(%request, %keyBase @ "onlineStatus");
        694040.putValue(%request, %keyBase @ "score");
        "LGAHarbor".putValue(%request, %keyBase @ "homeLocation.buildingName");
        %n = (%n + 1.0);
    }
    %n = 0;
    (%n < %num);
    "nv".putValue(%request, "location" @ %n @ ".areaName");
    getRandom(30, 800).putValue(%request, "location" @ %n @ ".userCount");
    %n = (%n + 1.0);
    "rj".putValue(%request, "location" @ %n @ ".areaName");
    getRandom(30, 800).putValue(%request, "location" @ %n @ ".userCount");
    %n = (%n + 1.0);
    "lga".putValue(%request, "location" @ %n @ ".areaName");
    getRandom(30, 800).putValue(%request, "location" @ %n @ ".userCount");
    %n = (%n + 1.0);
    "pvt".putValue(%request, "location" @ %n @ ".areaName");
    getRandom(50, 1500).putValue(%request, "location" @ %n @ ".userCount");
    %n = (%n + 1.0);
    "gw".putValue(%request, "location" @ %n @ ".areaName");
    getRandom(0, 30).putValue(%request, "location" @ %n @ ".userCount");
    %n = (%n + 1.0);
    %n.putValue(%request, "locationCount");
    "onDoneOrError".schedule(%request, 500);
    return %request;
};
function sendRequest_GetHappeningsInProgress(%userName, %callbackHandler) {
    %requestName = "request_GetHappeningsInProgress";
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    %requestName.setName(%request);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/finder/GetHappeningsInProgress";
    %url.setURL(%request);
    %userName.addUserAndToken(%request);
    %request.callbackHandler = %callbackHandler;
    %request.doAnother = 0;
    %request.start();
    return %request;
};
function sendRequest_PurchaseInventory(%userName, %skusArray, %payWith, %storename, %callbackHandler) {
    %payWith = strlwr(%payWith);
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    "request_PurchaseInventory".setName(%request);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/PurchaseInventory";
    %url.setURL(%request);
    %userName.addUserAndToken(%request);
    %payWith.addUrlParam(%request, "payWith");
    %storename.addUrlParam(%request, "storeName");
    %storename[$gStoreStockRevision @ %storename].addUrlParam(%request, "storeRevisionDate");
    %skusNum = %skusArray.count();
    %skusNum.addUrlParam(%request, "itemsToBuyCount");
    %n = 0;
    while ((%n < %skusNum)) {
        if ((%n.getValue(%skusArray) != 1.0)) {
            error("trying to buy a non-unit quantity of a sku." @ " " @ %n.getKey(%skusArray) @ " " @ %n.getValue(%skusArray));
            1.setValue(%skusArray, %n);
        }
        %n = (%n + 1.0);
    }
    %n = 0;
    (%n < %skusNum);
    while ((%n < %skusNum)) {
        %n.getKey(%skusArray).addBodyParam(%request, "itemsToBuy" @ %n @ ".sku");
        %n.getValue(%skusArray).addBodyParam(%request, "itemsToBuy" @ %n @ ".quantity");
        %n = (%n + 1.0);
    }
    %request.payWith = (%n < %skusNum) @ %payWith;
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
};
function sendRequest_RemoveUserInventoryCollection(%userName, %collectionName, %callbackHandler) {
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    "request_RemoveUserInventoryCollection".setName(%request);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/RemoveUserInventoryCollection";
    %url.setURL(%request);
    %userName.addUserAndToken(%request);
    %collectionName.addUrlParam(%request, "name");
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
};
function sendRequest_SaveClientUserProperties(%userName, %stringMap, %callbackHandler) {
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::SecureClientServiceURL;
    %url = %url @ "/SaveClientUserProperties";
    %url.setURL(%request);
    $Player::Name.setBodyParam(%request, "user");
    $Token.setURLParam(%request, "token");
    %num = %stringMap.size();
    %num.setBodyParam(%request, "propertyCount");
    %n = 0;
    while ((%n < %num)) {
        %n.getKey(%stringMap).setBodyParam(%request, "property" @ %n @ ".key");
        %n.getValue(%stringMap).setBodyParam(%request, "property" @ %n @ ".value");
        %n = (%n + 1.0);
    }
    %callbackHandler.setCompletedCallback(%request);
    %request.start();
    return %request;
};
function sendRequest_UpdateUserInventoryCollection(%userName, %collectionName, %propertyMap, %callbackHandler) {
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    "request_UpdateUserInventoryCollection".setName(%request);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/UpdateUserInventoryCollection";
    %url.setURL(%request);
    %userName.addUserAndToken(%request);
    %collectionName.addUrlParam(%request, "name");
    %num = %propertyMap.size();
    %num.addUrlParam(%request, "propertyCount");
    %n = 0;
    while ((%n < %num)) {
        %n.getKey(%propertyMap).addUrlParam(%request, "property" @ %n @ ".key");
        %n.getValue(%propertyMap).addUrlParam(%request, "property" @ %n @ ".value");
        %n = (%n + 1.0);
    }
    %request.callbackHandler = (%n < %num) @ %callbackHandler;
    if ($StandAlone) {
        if (!(%callbackHandler $= "")) {
            warn(getScopeName() @ " " @ "- standalone: faking success" @ " " @ getTrace());
            "success".putValue(%request, "status");
            schedule(500, 0, %callbackHandler, %request);
        }
    }
    %request.start();
    return %request;
};
function sendRequest_GetHighGameScores(%userName, %gameName, %firstIndex, %maxCount, %callbackHandler) {
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    "request_GetHighGameScores".setName(%request);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/score/GetHighGameScores";
    %url.setURL(%request);
    %userName.addUserAndToken(%request);
    %gameName.addUrlParam(%request, "gameName");
    %firstIndex.addUrlParam(%request, "firstIndex");
    %maxCount.addUrlParam(%request, "maxCount");
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
};
function sendRequest_GetHighGameScoresForStation(%userName, %gameStationId, %firstIndex, %maxCount, %callbackHandler) {
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    "request_GetHighGameScoresForStation".setName(%request);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/score/GetHighGameScoresForStation";
    %url.setURL(%request);
    %userName.addUserAndToken(%request);
    %gameStationId.addUrlParam(%request, "gameStationId");
    %firstIndex.addUrlParam(%request, "firstIndex");
    %maxCount.addUrlParam(%request, "maxCount");
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
};
function sendRequest_GetMainHappenings(%maxCount, %callbackHandler) {
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/finder/GetMainHappenings";
    %url.setURL(%request);
    $Player::Name.addUserAndToken(%request);
    %maxCount.setURLParam(%request, "maxCount");
    %callbackHandler.setCompletedCallback(%request);
    %request.start();
    return %request;
};
function sendRequest_GetMainVenues(%maxCount, %callbackHandler) {
    %requestName = "request_GetMainVenues";
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    %requestName.setName(%request);
    %request.timeStart = getSimTime();
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/finder/GetMainVenues";
    %url = %url @ "FAKE";
    %url.setURL(%request);
    $Player::Name.addUserAndToken(%request);
    %maxCount.addUrlParam(%request, "maxCount");
    %request.callbackHandler = %callbackHandler;
    "success".putValue(%request, "status");
    %maxCount.putValue(%request, "venuesCount");
    %n = 0;
    %venue = "interscope_lounge";
    %notThese = %venue;
    fakeRequestListItem_GetMainVenues(%request, "venues", %n, %venue);
    %n = (%n + 1.0);
    %venue = DestinationList::GetRandomDestinationForTGF("venue", %notThese);
    %notThese = %notThese @ " " @ %venue;
    fakeRequestListItem_GetMainVenues(%request, "venues", %n, %venue);
    %n = (%n + 1.0);
    %venue = DestinationList::GetRandomDestinationForTGF("shop", %notThese);
    %notThese = %notThese @ " " @ %venue;
    fakeRequestListItem_GetMainVenues(%request, "venues", %n, %venue);
    %n = (%n + 1.0);
    %n = %n;
    while ((%n < %maxCount)) {
        if ((getRandom(0, 2) == 0.0)) {
            %type = "venue";
        }
        if ((getRandom(0, 2) == 1.0)) {
            %type = "shop";
        }
        if ((getRandom(0, 2) == 2.0)) {
            %type = "residence";
        }
        %type = "venue";
        %venue = DestinationList::GetRandomDestinationForTGF(%type, %notThese);
        %notThese = %notThese @ " " @ %venue;
        fakeRequestListItem_GetMainVenues(%request, "venues", %n, %venue);
        %n = (%n + 1.0);
    }
    "onDoneOrError".schedule(%request, 100);
};
function fakeRequestListItem_GetMainVenues(%request, %listNameBase, %listIndex, %venueCodeName) {
    %venueCodeName.putValue(%request, %listNameBase @ %listIndex @ ".codeName");
};
function sendRequest_AbuseReport(%abuser, %description, %occurrence, %abuseType, %chatSnippetFile, %callbackHandler) {
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/AbuseReport";
    %url.setURL(%request);
    $Player::Name.addUserAndToken(%request);
    %abuser.setURLParam(%request, "abuser");
    %description.setURLParam(%request, "description");
    %occurrence.setURLParam(%request, "occurrence");
    %abuseType.setURLParam(%request, "abuseType");
    %chatSnippetFile.setPostFile(%request, "chatSnippet");
    %callbackHandler.setCompletedCallback(%request);
    %request.start();
    return %request;
};
function sendRequest_BootNew(%callbackHandler) {
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::SecureClientServiceURL;
    %url = %url @ "/Boot";
    %url.setURL(%request);
    $Player::Name.setURLParam(%request, "user");
    $Player::Password.setBodyParam(%request, "password");
    %callbackHandler.setCompletedCallback(%request);
    %request.start();
    return %request;
};
function sendRequest_Boot(%callbackHandler) {
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    %url = "";
    %url = %url @ $Net::SecureClientServiceURL;
    %url = %url @ "/Boot";
    %url.setURL(%request);
    $Player::Name.addUrlParam(%request, "user");
    $Player::Password.addBodyParam(%request, "password");
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
};
function sendRequest_UpdateUserStates(%statesList, %callbackHandler) {
    if (isDefined("%callbackHandler")) {
    }
    %callbackHandler = "";
    %callbackHandler;
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/UpdateUserStates";
    %url.setURL(%request);
    $Player::Name.addUserAndToken(%request);
    %statesList.addUrlParam(%request, "userStates");
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
};
function sendRequest_EventInformation(%eventId, %callbackHandler) {
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    "request_EventInformation".setName(%request);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/GetEventBanner";
    %url.setURL(%request);
    %eventId.addUrlParam(%request, "eventId");
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
};
function sendRequest_GiftCurrency(%targetUserName, %currencyType, %currencyAmount, %dryRun, %callbackHandler) {
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::SecureClientServiceURL;
    %url = %url @ "/TransferCurrency";
    %url.setURL(%request);
    $Player::Name.addUserAndToken(%request);
    MD5($Player::Password).setBodyParam(%request, "password");
    %targetUserName.setURLParam(%request, "payee");
    (%currencyType $= "vPoints") ? "VPOINTS" : "VBUX".setURLParam(%request, "currencyType");
    %currencyAmount.setURLParam(%request, "amount");
    1.setURLParam(%request, "dryRun", %dryRun);
    %callbackHandler.setCompletedCallback(%request);
    %request.start();
    return %request;
};
function sendRequest_GetUserProfileInfo(%targetUser, %callbackHandler) {
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::SecureClientServiceURL;
    %url = %url @ "/GetUserProfileInfo";
    %url.setURL(%request);
    $Player::Name.addUserAndToken(%request);
    %targetUser.setURLParam(%request, "targetUser");
    %callbackHandler.setCompletedCallback(%request);
    %request.start();
    return %request;
};
function sendRequest_UploadPhoto(%fileName, %caption, %peopleInViewList, %type, %location, %featured, %callbackHandler) {
    %featured = %featured ? "true" : "false";
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/uploadPhoto";
    %url.setURL(%request);
    $Player::Name.addUserAndToken(%request);
    %caption.setURLParam(%request, "caption");
    %featured.setURLParam(%request, "featured");
    "".setURLParam(%request, "broadcast");
    %type.setURLParam(%request, "type");
    %location.setURLParam(%request, "location");
    %peopleInViewList.setURLParam(%request, "inView");
    %fileName.setPostFile(%request, "imageBody");
    if (!(CustomSpaceClient::GetSpaceImIn() $= "")) {
        $CSSpaceInfo.owner.setURLParam(%request, "apartmentOwner");
        $CSSpaceInfo.vurl.setURLParam(%request, "vurl");
    }
    "vside:/location/" @ $gContiguousSpaceName @ "/PlazaSpawns".setURLParam(%request, "vurl");
    %callbackHandler.setCompletedCallback(%request);
    %request.start();
    return %request;
};
function sendRequest_PublishToTicker(%message, %priority, %callbackHandler) {
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::ClientServiceURL;
    %url = %url @ "/PublishToTicker";
    %url.setURL(%request);
    $Player::Name.addUserAndToken(%request);
    %callbackHandler.setCompletedCallback(%request);
    %message.setURLParam(%request, "text");
    %priority.setURLParam(%request, "priority");
    %isImplementedOnBackEnd = 1;
    if (%isImplementedOnBackEnd) {
    }
    if (!($StandAlone)) {
        %request.start();
    }
    echo(getScopeName() @ " " @ "- using fake data. yep");
    "success".setResult(%request, "status");
    "onDoneOrError".schedule(%request, 500);
    return %request;
};
