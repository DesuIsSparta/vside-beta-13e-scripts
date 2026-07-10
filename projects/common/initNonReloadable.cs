exec("./propertyMap.cs");
exec("./defaultMessages.cs");
$ETS::AppName = "vSide";
$ETS::AppVersion = "X";
function getMapType() {
    return "two_layer";
};
function showPlayerInfoPopup() {
    return 1;
};
function showInviteFriend() {
    return 0;
};
function showDanceTool() {
    return 1;
};
function cityInfoSetUp(%name, %background, %coords, %button, %altCoords, %altButton) {
    %cityInfo = safeEnsureScriptObject("ScriptObject", "");
    name = %name @ %cityInfo;
    background = %background @ %cityInfo;
    Coords = %coords @ %cityInfo;
    button = %button @ %cityInfo;
    altCoords = %altCoords @ %cityInfo;
    altButton = %altButton @ %cityInfo;
    venues = safeEnsureScriptObject("StringMap", "") @ %cityInfo;
    venues.bindClassName("VenuesMap");
    return %cityInfo;
};
function cityInfoAddVenue(%cityInfo, %venueName, %coords, %button, %spawnPointsGroup) {
    %venueInfo = safeEnsureScriptObject("ScriptObject", "");
    name = %venueName @ %venueInfo;
    Coords = %coords @ %venueInfo;
    button = %button @ %venueInfo;
    spawnName = %spawnPointsGroup @ %venueInfo;
    venues.put(name, %venueInfo);
    if ($StandAlone) {
    }
    if ((%cityInfo SPC name $= $gContiguousSpaceName)) {
    }
    if (!(isObject(%spawnPointsGroup))) {
        error(name @ " " @ "- unknown spawnPointGroup:" @ " " @ %spawnPointsGroup @ " " @ %venueName);
    }
};
function fillCityInfoMap(%map) {
    %map.clear();
    %cityInfo = cityInfoSetUp("nv", "platform/client/ui/city_maps/citymap_nv", "51 87 209 422", "platform/client/buttons/cities/newvenezia", "49 36 115 33", "platform/client/buttons/cities/sm_newvenezia");
    cityInfoAddVenue(%cityInfo, "Main Plaza", "256 422 44 50", "platform/client/buttons/venues/arrow_venue", "PlazaSpawns");
    cityInfoAddVenue(%cityInfo, "Shopping", "350 355 44 50", "platform/client/buttons/venues/arrow_venue", "ShoppingSpawns");
    cityInfoAddVenue(%cityInfo, "Warehouse Lofts", "459 285 44 50", "platform/client/buttons/venues/arrow_venue", "LoftSpawns");
    cityInfoAddVenue(%cityInfo, "The Lounge", "186 255 44 50", "platform/client/buttons/venues/arrow_venue", "LoungeSpawns");
    cityInfoAddVenue(%cityInfo, "Rail Station", "378 138 44 50", "platform/client/buttons/venues/arrow_venue", "RailwaySpawns");
    cityInfoAddVenue(%cityInfo, "NV255 Lofts", "325 267 44 50", "platform/client/buttons/venues/arrow_venue", "LobbySpawns_NV255Lofts");
    %map.put(name, %cityInfo);
    %cityInfo = cityInfoSetUp("lga", "platform/client/ui/city_maps/citymap_lga", "460 87 194 422", "platform/client/buttons/cities/lga", "59 100 121 31", "platform/client/buttons/cities/sm_lga");
    %cityInfo;
    cityInfoAddVenue(%cityInfo, "Waterfront", "152 384 44 50", "platform/client/buttons/venues/arrow_venue", "MapSpawns_Waterfront");
    cityInfoAddVenue(%cityInfo, "Main Plaza", "283 255 44 50", "platform/client/buttons/venues/arrow_venue", "PlazaSpawns");
    cityInfoAddVenue(%cityInfo, "Shopping", "432 191 44 50", "platform/client/buttons/venues/arrow_venue", "MapSpawns_Shopping");
    cityInfoAddVenue(%cityInfo, "Subway", "233 167 44 50", "platform/client/buttons/venues/arrow_venue", "MapSpawns_Subway");
    cityInfoAddVenue(%cityInfo, "The Hacienda", "466 55 44 50", "platform/client/buttons/venues/arrow_venue", "MapSpawns_Hacienda");
    cityInfoAddVenue(%cityInfo, "LGA Tower", "163 289 44 50", "platform/client/buttons/venues/arrow_venue", "LobbySpawns_LGATower");
    cityInfoAddVenue(%cityInfo, "The Docks", "515 97 44 50", "platform/client/buttons/venues/arrow_venue", "LobbySpawns_LGAYachts");
    cityInfoAddVenue(%cityInfo, "Degrassi", "270 321 44 50", "platform/client/buttons/venues/arrow_venue", "MapSpawns_Degrassi");
    %map.put(name, %cityInfo);
    %cityInfo = cityInfoSetUp("rj", "platform/client/ui/city_maps/citymap_rj", "260 87 200 422", "platform/client/buttons/cities/raijuku", "49 69 93 31", "platform/client/buttons/cities/sm_raijuku");
    %cityInfo;
    cityInfoAddVenue(%cityInfo, "Main Plaza", "270 315 44 50", "platform/client/buttons/venues/arrow_venue", "PlazaSpawns");
    cityInfoAddVenue(%cityInfo, "Shopping", "383 305 44 50", "platform/client/buttons/venues/arrow_venue", "MapSpawns_Shopping");
    cityInfoAddVenue(%cityInfo, "IIR Residences", "271 109 44 50", "platform/client/buttons/venues/arrow_venue", "LobbySpawns_HotelRaijuku");
    cityInfoAddVenue(%cityInfo, "IIC Club", "539 261 44 50", "platform/client/buttons/venues/arrow_venue", "VenueSpawns_iic");
    cityInfoAddVenue(%cityInfo, "GiGoth", "473 202 44 50", "platform/client/buttons/venues/arrow_venue", "VenueSpawns_gigoth");
    %map.put(name, %cityInfo);
    %cityInfo = cityInfoSetUp("gw", "platform/client/ui/city_maps/citymap_gw", "514 508 169 56", "platform/client/buttons/cities/gateway", "79 131 121 34", "platform/client/buttons/cities/sm_gateway");
    %cityInfo;
    cityInfoAddVenue(%cityInfo, "Start Here!", "348 106 88 100", "platform/client/buttons/venues/arrow_venue", "mapSpawns_entry");
    %map.put(name, %cityInfo);
    return %map;
};
function fillCityNamesMap(%map) {
    safeEnsureScriptObject("StringMap", "gCityNamesShortToLongMap");
    clear();
    %map.clear();
    %code = "nv";
    gCityNamesShortToLongMap;
    %long = "NewVenezia";
    %map.put("New Venezia", %code);
    %map.put(%long, %code);
    %map.put(%code, %code);
    %code.put(%long);
    %code = "lga";
    gCityNamesShortToLongMap;
    %long = "LaGenoaAires";
    %map.put("La Genoa Aires", %code);
    %map.put(%long, %code);
    %map.put(%code, %code);
    %code.put(%long);
    %code = "rj";
    gCityNamesShortToLongMap;
    %long = "RaiJuku";
    %map.put("Rai Juku", %code);
    %map.put(%long, %code);
    %map.put(%code, %code);
    %code.put(%long);
    %code = "gw";
    gCityNamesShortToLongMap;
    %long = "Gateway";
    %map.put("n00bieIsland", %code);
    %map.put(%long, %code);
    %map.put(%code, %code);
    %code.put(%long);
    return %map;
};
