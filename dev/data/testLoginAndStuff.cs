exec("./skeletonClient.cs");
function initGenres()
{
    %i = 0;
    $Genres[%i = %i + 1,0] = "h";
    $Genres[%i,1] = "confident";
    $Genres[%i = %i + 1,0] = "i";
    $Genres[%i,1] = "relaxed";
    $Genres[%i = %i + 1,0] = "p";
    $Genres[%i,1] = "upbeat";
    $Genres[%i = %i + 1,0] = "b";
    $Genres[%i,1] = "blue";
    $Genres[%i = %i + 1,0] = "z";
    $Genres[%i,1] = "zombie";
    $GenresCount = %i;
    return ;
}
function initTeleports()
{
    %i = 0;
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "nobunaga";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "90.01 -21.00 4.29 0 0 -1 1.62";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "gari_c";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "88.93 -18.26 4.29 0 0 -1 1.65";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "roxee";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "sashe";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "21.24 15.68 3.00 0 0 -1 1.57";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "chaz";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "rosariod";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "26.57 1.50 2.02 0 0 1 3.16";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "20.81 -22.92 13.92 0 0 1 4.09";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "64.23 28.89 1.90 0 0 -1 .60";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "87.16 18.72 5.02 0 0 -1 1.91";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "81.37 19.26 5 0 0 1 1.47";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "5.96 -9.24 16.01 0 0 1 .05";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "11.23 -29.32 14.92 0 0 1 .65";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "52.41 65.23 2.59 0 0 1 1.13";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "168 70 1 0 0 1 3.70";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "19.81 43.16 1.19 0 0 1 2.45";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "164.68 -132.29 4.77 0 0 1 .82";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "82.11 -.49 6.14 0 0 -1 1.60";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "8.25 15.97 26.89 0 0 -1 .38";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "1.23 -15.72 6.01 0 0 1 1.16";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "83.43 11.78 1.89 0 0 1 3.77";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "122.12 37.63 1.00 0 0 1 3.40";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "18.603 25.3676 47.86 0 0 1 1.23";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "96.523 20.2376 18.5 0 0 -1 1.03";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "-11.60 28.03 9.63 0 0 1 1.31";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "218.898 8.3469 143.12 0 0 -1 .65";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "106.884 131.137 174.676 0 0 1 1.40";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "82.19 -8.16 6.14 0 0 -1 1.43";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "81.00 -7.50 6.14 0 0 -1 1.48";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "BurtZito";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "JohnnyB";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "JojoKatz";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "RosarioD";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "StanDaMan";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "SuspiciousMan";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "WangC";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "alberta";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "annie";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "ashley";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "blackmarketjack";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "bob";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "carmit";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "carrie";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "chaz";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "dennis";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "djLarsBerg";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "djx";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "frank";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "gari_c";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "ike";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "jessica";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "kelvin";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "kim";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "lizzy";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "may";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "melody";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "merri";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "missy";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "nicole";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "ringo";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "ronn";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "ross";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "roxee";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "sashe";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "storekeeper";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "terri";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsNV[%i,1] = "vanessa_lee";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsNV[%i,1] = "plazaSpawns";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsNV[%i,1] = "ShoppingSpawns";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsNV[%i,1] = "LoungeSpawns";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsNV[%i,1] = "RailwaySpawns";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsNV[%i,1] = "LoftSpawns";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsNV[%i,1] = "LAXSpawns";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsNV[%i,1] = "GariSpawns";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsNV[%i,1] = "DressingRoomSpawns";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "80.10 24.99 10.45 0 0 1 .03";
    $teleportsNV[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsNV[%i,1] = "88.72 41.17 10.46 0 0 1 3.12";
    $teleportsNVCount = %i;
    %i = 0;
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "kirakong";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "miah";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "rosa121";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "ginabes";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "che";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "nene";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsLGA[%i,1] = "-15.1823 72.7429 8.2398";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "curtis";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsLGA[%i,1] = "22.15 42.04 7.22 0 0 -1 0.74";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsLGA[%i,1] = "-12.74 34.16 4.92 0 0 1 .31";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsLGA[%i,1] = "42.07 37.53 24.70 0 0 1 .46";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsLGA[%i,1] = "-43.19 49.13 7.83 0 0 1 1.16";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsLGA[%i,1] = "29.72 89.70 23.35 0 0 1 3.06";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsLGA[%i,1] = "29.89 88.10 24.80 0 0 1 2.66";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsLGA[%i,1] = "30.3594 83.1404 24.8008 0 0 1 2.83";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsLGA[%i,1] = "37.77 77.46 25.510 0 0 -1 1.28";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsLGA[%i,1] = "36 73 51 0 0 1 1.00";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsLGA[%i,1] = "-46.21 17.51 1.70";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsLGA[%i,1] = "-90.98 95.38 53.15";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsLGA[%i,1] = "-79.57 76.75 73.30";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "Curtis";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "DonnieDarko";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "Eilan";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "GinaBes";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "Mannequin";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "Miah";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "Milan";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "Nene";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "Pia";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "Rosa121";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "SuspiciousMan";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "blackmarketjack";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "che";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "djmic";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "djx";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "kirakong";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "kkong";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "lolalove";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsLGA[%i,1] = "merri";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsLGA[%i,1] = "MapSpawns_Waterfront";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsLGA[%i,1] = "MapSpawns_Shopping";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsLGA[%i,1] = "MapSpawns_Subway";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsLGA[%i,1] = "MapSpawns_Dock";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsLGA[%i,1] = "MapSpawns_Voy";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsLGA[%i,1] = "plazaSpawns";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsLGA[%i,1] = "DressingRoomSpawns";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsLGA[%i,1] = "-60.59 43.36 3.13 0 0 1 2.98";
    $teleportsLGA[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsLGA[%i,1] = "-55.64 41.26 -.67 0 0 -1 1.64";
    $teleportsLGACount = %i;
    %i = 0;
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "kanade";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "hiroyuki";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "chiharu";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "syota";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "misaki";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "28.29 18.27 30.35 0 0 1 2.84";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "moumoko";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "-66.97 48.74 31.01 0 0 1 1.64";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "-16.28 13.03 30.76 0 0 -1 .98";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "-14.77 -16.33 30 0 0 1 1.64";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "terrencesan";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "ramenmastershun";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "-4.09 -43.65 36.26 0 0 1 2.01";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "-2.57 -43.60 36.26 0 0 1 2.38";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "-1.07 -43.22 35.39 0 0 1 2.84";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "0.29 -42.61 37.76 0 0 -1 0.53";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "1.51 -41.57 37.76 0 0 -1 1.04";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "2.69 -40.62 35.39 0 0 1 3.54";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "3.55 -39.15 33.39 0 0 1 3.21";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "12.52 -45.57 30.35 0 0 1 3.21";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "13.88 -49.72 30.10 0 0 1 3.21";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "16.02 -52.01 30.14 0 0 1 3.21";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "18.50 -47.40 30.26 0 0 1 3.21";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "15.76 -61.25 30.25 0 0 1 3.21";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "13.43 -63.12 30.14 0 0 1 3.21";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "11.44 -65.31 30.45 0 0 1 3.21";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "7.13 -75.76 36.42 0 0 1 3.12";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "10.23 -68.80 35.85 0 0 1 3.12";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "8.38 -63.12 37.26 0 0 1 1.81";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "14.77 -68.86 38.28 0 0 1 3.16";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "14.61 -90.59 38.29 0 0 -1 .23";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "-15.81 112.43 30.46 0 0 1 1.97";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "-24.96 12.35 53.89 0 0 1 2.16";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "-15.02 64.29 30.42 0 0 1 .86";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "-9.21 68.78 30.26 0 0 1 1.64";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "Chiharu";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "Hiroyuki";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "Ichiban";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "Kanade";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "Kotone";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "Kurisoo";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "Miho";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "Misaki";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "Mituki";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "Nanami";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "Ramenmastershun";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "SuspiciousMan";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "Syota";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "TerrenceSan";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "Tetsumo";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "djJunko";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "merri";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "moumoko";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToPlayer' ;
    $teleportsRJ[%i,1] = "nobunaga";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsRJ[%i,1] = "MapSpawns_Shopping";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsRJ[%i,1] = "MapSpawns_Hotel";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsRJ[%i,1] = "MapSpawns_Railway";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsRJ[%i,1] = "PlazaSpawns";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToSpawnGroup' ;
    $teleportsRJ[%i,1] = "DressingRoomSpawns";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "6.53 57.26 43.51 0 0 1 .15";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "29.32 11.60 43.51 0 0 1 1.74";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "37.01 -29.13 31.03 0 0 1 .09";
    $teleportsRJ[%i = %i + 1,0] = 'TeleportToTransform' ;
    $teleportsRJ[%i,1] = "35.37 -22.42 31.01 0 0 1 .1";
    $teleportsRJCount = %i;
    return ;
}
function testLoginAndStay()
{
    $loginLogout = 0;
    %testLogin = new ScriptObject(skeletonClient)
    {
        userName = $UserPref::Player::Name;
        password = $UserPref::Player::Password;
        joinAction = "doSomething";
        quitOnError = "true";
    };
    %testLogin.init();
    echo("LOAD: $TargetCity: " @ $DestServerName);
    %testLogin.doLogin($DestServerName);
    return ;
}
echo("LOAD: starting via testLoginAndStay()");
$failureCount = 0;
initGenres();
initTeleports();
testLoginAndStay();
function doSomething()
{
    schedule(30000, 0, do_emote);
    return ;
}
function do_emote()
{
    %i = getRandom(1, 2);
    if (%i $= 1)
    {
        $mvYawLeftSpeed = $Pref::Input::KeyboardTurnSpeed;
    }
    else
    {
        if (%i $= 2)
        {
            $mvYawRightSpeed = $Pref::Input::KeyboardTurnSpeed;
        }
    }
    $mvForwardAction = 0;
    $rand_emote = EmoteDict.getValue(getRandom(0, EmoteDict.size() - 1));
    $rand_genre = getRandom(1, $GenresCount);
    commandToServer('EtsPlayAnimName', $rand_emote);
    if (isObject(pChat))
    {
        pChat.say("(" @ $Hostname @ ")" SPC "Genre:" SPC $Genres[$rand_genre,0] @ "(" @ $Genres[$rand_genre,1] @ "); Emote:" SPC $rand_emote, 0, 0);
    }
    commandToServer('setGenre', $Genres[$rand_genre]);
    schedule(5000, 0, stopAndTalk);
    schedule(30000, 0, approveFriendRequests);
    return ;
}
function stopAndTalk()
{
    if (isObject(pChat))
    {
        if (ClosetGui.isVisible())
        {
            ClosetGui.close();
        }
        if (geTGF.isVisible())
        {
            geTGF.closeFully();
        }
        $mvYawLeftSpeed = 0;
        $mvYawRightSpeed = 0;
        $mvForwardAction = 0;
        %rand_teleport_NV = getRandom(1, $teleportsNVCount);
        %rand_teleport_LGA = getRandom(1, $teleportsLGACount);
        %rand_teleport_RJ = getRandom(1, $teleportsRJCount);
        if (($DestServerName $= "NewVeneziaNorth") && ($DestServerName $= "NewVeneziaSouth"))
        {
            %command = $teleportsNV[%rand_teleport_NV,0];
            %destination = $teleportsNV[%rand_teleport_NV,1];
        }
        else
        {
            if (($DestServerName $= "LaGenoaAiresNorth") && ($DestServerName $= "LaGenoaAiresSouth"))
            {
                %command = $teleportsLGA[%rand_teleport_LGA,0];
                %destination = $teleportsLGA[%rand_teleport_LGA,1];
            }
            else
            {
                if (($DestServerName $= "RaijukuNorth") && ($DestServerName $= "RaijukuSouth"))
                {
                    %command = $teleportsRJ[%rand_teleport_RJ,0];
                    %destination = $teleportsRJ[%rand_teleport_RJ,1];
                }
            }
        }
        pChat.say("(" @ $Hostname @ ")" SPC $UserPref::Player::Name SPC ":" SPC $Hostname SPC ":" SPC %command SPC ":" SPC %destination, 0, 0);
        commandToServer(%command, %destination);
        schedule(4000, 0, changeClothes);
    }
    else
    {
        if ($failureCount == 5)
        {
            echo("LOAD: Giving up. Lost PChat object.");
            echo("LOAD: Quit()-ing...");
            logoffAndQuit();
        }
        else
        {
            echo("LOAD: Lost PChat... Gonna try again.");
            $failureCount = $failureCount + 1;
        }
    }
    schedule(10000, 0, do_emote);
    return ;
}
function logoffAndQuit()
{
    echo("LOAD: Logging off and quit()-ing...");
    echo("LOAD: Login::loggedIn:" SPC $Login::loggedIn);
    logout(0);
    schedule(1000, 0, doQuit);
    return ;
}
function takeSnapshot()
{
    echo("LOAD: takeSnapshot");
    BroadCastControlPanel.automateSnapshotUpload();
    return ;
}
function useAndSaveRandomOutfit()
{
    %drwrs = "glasses torso legs legsb feet ear neck neckb wristleft wristleftb wristright wristrightb purse hat";
    %skus = SkuManager.getRandomSkusForLocalPlayer(%drwrs);
    commandToServer('SetActiveSkus', %skus);
    return ;
}
function changeClothes()
{
    echo("LOAD: changeClothes enter...");
    useAndSaveRandomOutfit();
    echo("LOAD: changeClothes done...");
    return ;
}
function approveFriendRequests()
{
    %fansHere = BuddyHudWin.buddyLists[FansHere];
    if (!isObject(%fansHere))
    {
        return ;
    }
    if (%fansHere.size() == 0)
    {
        echo("LOAD: There are no waiting requests.");
        return ;
    }
    %n = %fansHere.size() - 1;
    while (%n >= 0)
    {
        %playerName = %fansHere.getKey(%n);
        echo("LOAD: Friend" SPC %playerName);
        %action = "accept";
        doUserFavorite(%playerName, %action);
        pChat.whisper("(" @ $Hostname @ ")" SPC "Hey" SPC %playerName @ ", I" SPC %action SPC "your friendship.", %playerName);
        %n = %n - 1;
    }
}


