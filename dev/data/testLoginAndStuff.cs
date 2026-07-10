exec("./skeletonClient.cs");
function initGenres()
{
    %i = 0;
    $Genres[%i = (%i + 1.0),0] = "h";
    %i["confident" @ $Genres TAB %i @ 1] = ;
    $Genres[%i = (%i + 1.0),0] = "i";
    %i["relaxed" @ $Genres TAB %i @ 1] = ;
    $Genres[%i = (%i + 1.0),0] = "p";
    %i["upbeat" @ $Genres TAB %i @ 1] = ;
    $Genres[%i = (%i + 1.0),0] = "b";
    %i["blue" @ $Genres TAB %i @ 1] = ;
    $Genres[%i = (%i + 1.0),0] = "z";
    %i["zombie" @ $Genres TAB %i @ 1] = ;
    $GenresCount = %i;
}
function initTeleports()
{
    %i = 0;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["nobunaga" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["90.01 -21.00 4.29 0 0 -1 1.62" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["gari_c" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["88.93 -18.26 4.29 0 0 -1 1.65" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["roxee" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["sashe" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["21.24 15.68 3.00 0 0 -1 1.57" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["chaz" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["rosariod" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["26.57 1.50 2.02 0 0 1 3.16" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["20.81 -22.92 13.92 0 0 1 4.09" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["64.23 28.89 1.90 0 0 -1 .60" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["87.16 18.72 5.02 0 0 -1 1.91" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["81.37 19.26 5 0 0 1 1.47" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["5.96 -9.24 16.01 0 0 1 .05" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["11.23 -29.32 14.92 0 0 1 .65" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["52.41 65.23 2.59 0 0 1 1.13" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["168 70 1 0 0 1 3.70" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["19.81 43.16 1.19 0 0 1 2.45" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["164.68 -132.29 4.77 0 0 1 .82" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["82.11 -.49 6.14 0 0 -1 1.60" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["8.25 15.97 26.89 0 0 -1 .38" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["1.23 -15.72 6.01 0 0 1 1.16" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["83.43 11.78 1.89 0 0 1 3.77" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["122.12 37.63 1.00 0 0 1 3.40" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["18.603 25.3676 47.86 0 0 1 1.23" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["96.523 20.2376 18.5 0 0 -1 1.03" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-11.60 28.03 9.63 0 0 1 1.31" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["218.898 8.3469 143.12 0 0 -1 .65" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["106.884 131.137 174.676 0 0 1 1.40" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["82.19 -8.16 6.14 0 0 -1 1.43" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["81.00 -7.50 6.14 0 0 -1 1.48" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["BurtZito" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["JohnnyB" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["JojoKatz" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["RosarioD" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["StanDaMan" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["SuspiciousMan" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["WangC" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["alberta" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["annie" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["ashley" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["blackmarketjack" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["bob" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["carmit" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["carrie" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["chaz" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["dennis" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["djLarsBerg" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["djx" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["frank" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["gari_c" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["ike" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["jessica" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["kelvin" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["kim" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["lizzy" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["may" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["melody" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["merri" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["missy" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["nicole" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["ringo" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["ronn" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["ross" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["roxee" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["sashe" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["storekeeper" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["terri" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["vanessa_lee" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["plazaSpawns" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["ShoppingSpawns" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["LoungeSpawns" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["RailwaySpawns" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["LoftSpawns" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["LAXSpawns" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["GariSpawns" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["DressingRoomSpawns" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["80.10 24.99 10.45 0 0 1 .03" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNV[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["88.72 41.17 10.46 0 0 1 3.12" @ $teleportsNV TAB %i @ 1] = ;
    $teleportsNVCount = %i;
    %i = 0;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["kirakong" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["miah" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["rosa121" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["ginabes" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["che" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["nene" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-15.1823 72.7429 8.2398" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["curtis" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["22.15 42.04 7.22 0 0 -1 0.74" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-12.74 34.16 4.92 0 0 1 .31" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["42.07 37.53 24.70 0 0 1 .46" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-43.19 49.13 7.83 0 0 1 1.16" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["29.72 89.70 23.35 0 0 1 3.06" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["29.89 88.10 24.80 0 0 1 2.66" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["30.3594 83.1404 24.8008 0 0 1 2.83" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["37.77 77.46 25.510 0 0 -1 1.28" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["36 73 51 0 0 1 1.00" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-46.21 17.51 1.70" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-90.98 95.38 53.15" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-79.57 76.75 73.30" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Curtis" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["DonnieDarko" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Eilan" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["GinaBes" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Mannequin" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Miah" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Milan" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Nene" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Pia" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Rosa121" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["SuspiciousMan" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["blackmarketjack" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["che" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["djmic" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["djx" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["kirakong" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["kkong" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["lolalove" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["merri" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["MapSpawns_Waterfront" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["MapSpawns_Shopping" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["MapSpawns_Subway" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["MapSpawns_Dock" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["MapSpawns_Voy" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["plazaSpawns" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["DressingRoomSpawns" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-60.59 43.36 3.13 0 0 1 2.98" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGA[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-55.64 41.26 -.67 0 0 -1 1.64" @ $teleportsLGA TAB %i @ 1] = ;
    $teleportsLGACount = %i;
    %i = 0;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["kanade" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["hiroyuki" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["chiharu" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["syota" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["misaki" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["28.29 18.27 30.35 0 0 1 2.84" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["moumoko" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-66.97 48.74 31.01 0 0 1 1.64" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-16.28 13.03 30.76 0 0 -1 .98" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-14.77 -16.33 30 0 0 1 1.64" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["terrencesan" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["ramenmastershun" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-4.09 -43.65 36.26 0 0 1 2.01" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-2.57 -43.60 36.26 0 0 1 2.38" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-1.07 -43.22 35.39 0 0 1 2.84" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["0.29 -42.61 37.76 0 0 -1 0.53" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["1.51 -41.57 37.76 0 0 -1 1.04" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["2.69 -40.62 35.39 0 0 1 3.54" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["3.55 -39.15 33.39 0 0 1 3.21" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["12.52 -45.57 30.35 0 0 1 3.21" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["13.88 -49.72 30.10 0 0 1 3.21" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["16.02 -52.01 30.14 0 0 1 3.21" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["18.50 -47.40 30.26 0 0 1 3.21" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["15.76 -61.25 30.25 0 0 1 3.21" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["13.43 -63.12 30.14 0 0 1 3.21" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["11.44 -65.31 30.45 0 0 1 3.21" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["7.13 -75.76 36.42 0 0 1 3.12" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["10.23 -68.80 35.85 0 0 1 3.12" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["8.38 -63.12 37.26 0 0 1 1.81" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["14.77 -68.86 38.28 0 0 1 3.16" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["14.61 -90.59 38.29 0 0 -1 .23" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-15.81 112.43 30.46 0 0 1 1.97" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-24.96 12.35 53.89 0 0 1 2.16" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-15.02 64.29 30.42 0 0 1 .86" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["-9.21 68.78 30.26 0 0 1 1.64" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Chiharu" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Hiroyuki" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Ichiban" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Kanade" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Kotone" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Kurisoo" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Miho" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Misaki" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Mituki" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Nanami" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Ramenmastershun" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["SuspiciousMan" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Syota" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["TerrenceSan" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["Tetsumo" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["djJunko" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["merri" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["moumoko" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToPlayer';
    %i["nobunaga" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["MapSpawns_Shopping" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["MapSpawns_Hotel" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["MapSpawns_Railway" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["PlazaSpawns" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToSpawnGroup';
    %i["DressingRoomSpawns" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["6.53 57.26 43.51 0 0 1 .15" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["29.32 11.60 43.51 0 0 1 1.74" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["37.01 -29.13 31.03 0 0 1 .09" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJ[%i = (%i + 1.0),0] = 'TeleportToTransform';
    %i["35.37 -22.42 31.01 0 0 1 .1" @ $teleportsRJ TAB %i @ 1] = ;
    $teleportsRJCount = %i;
}
function testLoginAndStay()
{
    $loginLogout = 0;
    %testLogin = new ScriptObject(skeletonClient) {
        userName = $UserPref::Player::Name;
        password = $UserPref::Player::Password;
        joinAction = "doSomething";
        quitOnError = "true";
    };
    %testLogin.init();
    echo("LOAD: $TargetCity: " @ $DestServerName);
    %testLogin.doLogin($DestServerName);
}
echo("LOAD: starting via testLoginAndStay()");
$failureCount = 0;
initGenres();
initTeleports();
testLoginAndStay();
function doSomething()
{
    schedule(30000, 0, do_emote);
}
function do_emote()
{
    %i = getRandom(1, 2);
    if ((%i $= 1))
    {
        $mvYawLeftSpeed = $Pref::Input::KeyboardTurnSpeed;
    }
    else
    {
        if ((%i $= 2))
        {
            $mvYawRightSpeed = $Pref::Input::KeyboardTurnSpeed;
        }
    }
    $mvForwardAction = 0;
    $rand_emote = EmoteDict.getValue(getRandom(0, (EmoteDict.size() - 1.0)));
    $rand_genre = getRandom(1, $GenresCount);
    commandToServer('EtsPlayAnimName', $rand_emote);
    if (isObject(pChat))
    {
        pChat.say($rand_genre[$rand_genre["(" @ $Hostname @ ")" @ " " @ "Genre:" @ " " @ $Genres TAB $rand_genre @ 0] @ "(" @ $Genres TAB $rand_genre @ 1] @ "); Emote:" @ " " @ $rand_emote, 0, 0);
    }
    commandToServer('setGenre', $Genres[$rand_genre]);
    schedule(5000, 0, stopAndTalk);
    schedule(30000, 0, approveFriendRequests);
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
        if (($DestServerName $= "NewVeneziaNorth") || ($DestServerName $= "NewVeneziaSouth"))
        {
            %command = %rand_teleport_NV[$teleportsNV TAB %rand_teleport_NV @ 0];
            %destination = %rand_teleport_NV[$teleportsNV TAB %rand_teleport_NV @ 1];
        }
        else
        {
            if (($DestServerName $= "LaGenoaAiresNorth") || ($DestServerName $= "LaGenoaAiresSouth"))
            {
                %command = %rand_teleport_LGA[$teleportsLGA TAB %rand_teleport_LGA @ 0];
                %destination = %rand_teleport_LGA[$teleportsLGA TAB %rand_teleport_LGA @ 1];
            }
            else
            {
                if (($DestServerName $= "RaijukuNorth") || ($DestServerName $= "RaijukuSouth"))
                {
                    %command = %rand_teleport_RJ[$teleportsRJ TAB %rand_teleport_RJ @ 0];
                    %destination = %rand_teleport_RJ[$teleportsRJ TAB %rand_teleport_RJ @ 1];
                }
            }
        }
        pChat.say("(" @ $Hostname @ ")" @ " " @ $UserPref::Player::Name @ " " @ ":" @ " " @ $Hostname @ " " @ ":" @ " " @ %command @ " " @ ":" @ " " @ %destination, 0, 0);
        commandToServer(%command, %destination);
        schedule(4000, 0, changeClothes);
    }
    else
    {
        if (($failureCount == 5.0))
        {
            echo("LOAD: Giving up. Lost PChat object.");
            echo("LOAD: Quit()-ing...");
            logoffAndQuit();
        }
        else
        {
            echo("LOAD: Lost PChat... Gonna try again.");
            $failureCount = ($failureCount + 1.0);
        }
    }
    schedule(10000, 0, do_emote);
}
function logoffAndQuit()
{
    echo("LOAD: Logging off and quit()-ing...");
    echo("LOAD: Login::loggedIn:" @ " " @ $Login::loggedIn);
    logout(0);
    schedule(1000, 0, doQuit);
}
function takeSnapshot()
{
    echo("LOAD: takeSnapshot");
    BroadCastControlPanel.automateSnapshotUpload();
}
function useAndSaveRandomOutfit()
{
    %drwrs = "glasses torso legs legsb feet ear neck neckb wristleft wristleftb wristright wristrightb purse hat";
    %skus = SkuManager.getRandomSkusForLocalPlayer(%drwrs);
    commandToServer('SetActiveSkus', %skus);
}
function changeClothes()
{
    echo("LOAD: changeClothes enter...");
    useAndSaveRandomOutfit();
    echo("LOAD: changeClothes done...");
}
function approveFriendRequests()
{
    %fansHere = BuddyHudWin.buddyLists[FansHere];
    if (!isObject(%fansHere))
    {
        return;
    }
    if ((%fansHere.size() == 0.0))
    {
        echo("LOAD: There are no waiting requests.");
        return;
    }
    %n = (%fansHere.size() - 1.0);
    while ((%n >= 0.0))
    {
        %playerName = %fansHere.getKey(%n);
        echo("LOAD: Friend" @ " " @ %playerName);
        %action = "accept";
        doUserFavorite(%playerName, %action);
        pChat.whisper("(" @ $Hostname @ ")" @ " " @ "Hey" @ " " @ %playerName @ ", I" @ " " @ %action @ " " @ "your friendship.", %playerName);
        %n = (%n - 1.0);
    }
}
