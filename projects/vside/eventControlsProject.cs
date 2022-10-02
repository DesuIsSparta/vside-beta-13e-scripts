function initProjectSpecificDoors()
{
    $gDoorsNum = 0;
    addLockableDoor("nv", "NV Doppel Store", 0, "", "", "doppledoor", "");
    addLockableDoor("nv", "NV Hottubs", 0, "", "", "deckdoor1", "");
    addLockableDoor("nv", "NV LAX", 0, "LAXdoorGroup", "TeleportFreeLAXDoors", "", "");
    addLockableDoor("nv", "NV Listening Stations", 0, "", "", "ListeningStationDoor", "");
    addLockableDoor("nv", "NV PCD Main Club", 0, "", "", "clubmaindoor1", "");
    addLockableDoor("nv", "NV PCD Store", 0, "", "TeleportFreeStore", "pcdstoredoor1", "");
    addLockableDoor("nv", "NV Private Space Lobby", 0, "", "TeleportFreePrivateSpace", "pslobbyeldoor", "");
    addLockableDoor("nv", "NV Teahouse lower A", 0, "", "", "PS1LowerDoor01", "");
    addLockableDoor("nv", "NV Teahouse lower B", 0, "", "", "PS1LowerDoor02", "");
    addLockableDoor("nv", "NV Teahouse upper", 0, "", "", "ps1upperdoor1", "");
    addLockableDoor("nv", "NV Upper Lounge", 0, "UpperLoungeDoorGroup", "TeleportFreeUpperLoungeDoors", "", "");
    addLockableDoor("nv", "NV VIP Room", 0, "", "TeleportFreeVIP", "", "");
    addLockableDoor("nv", "NV Lounge Dance floor", 0, "loungedanceWalls", "TeleportFreeNVdanceflWalls", "", "");
    addLockableDoor("rj", "RJ YJL Shoes", 0, "", "TeleportFreeYJL", "YJL_Door", "");
    addLockableDoor("rj", "RJ IIC Club", 0, "", "TeleportFreeIIC", "IICVelvetDoor", "");
    addLockableDoor("rj", "RJ IIC VIP Area", 0, "", "TeleportFreeIICVIPOnly", "IIC_VIP_VelvetDoor", "");
    addLockableDoor("rj", "RJ IIC Wall", 0, "iic_accessC_group", "TeleportFreeIICVIPOnly", "", "");
    addLockableDoor("rj", "RJ Kabuki Wall", 0, "kabuki_accessC_group", "TeleportFree_Kabuki_Stage", "", "");
    addLockableDoor("rj", "RJ Kabuki Frontdoor", 0, "kabuki_door_group", "TeleportFree_Kabuki_Door", "", "");
    addLockableDoor("rj", "RJ Gigoth Wall", 0, "gigoth_accessC_group", "TeleportFree_gigoth_Stage", "", "");
    addLockableDoor("rj", "RJ etswhite", 0, "", "", "rk_ets_white_doorb", "");
    addLockableDoor("rj", "RJ ets_black", 0, "", "", "rk_ets_black_doora", "");
    addLockableDoor("rj", "RJ rk_iig_doora", 0, "", "", "rk_iig_doora", "");
    addLockableDoor("rj", "RJ rk_iig_doorb", 0, "", "", "rk_iig_doorb", "");
    addLockableDoor("lga", "LGA Hacienda Main Door", 0, "", "TeleportFreeHacienda", "clubmaindoor1", "vside:/location/lga/MapSpawns_Hacienda");
    addLockableDoor("lga", "LGA Hacienda Dance Patio", 0, "haciendafordancecompGroup", "TeleportFreeHacDancePatio", "", "");
    addLockableDoor("lga", "LGA Hacienda Upstairs VIP", 0, "", "TeleportFreeHacUpVIP", "haciendaaccessdoor1_rope", "");
    addLockableDoor("lga", "LGA Hacienda Celeb Nook", 0, "haciendaCelebNookGroup", "TeleportFreeHacCelebNook", "", "");
    addLockableDoor("lga", "LGA Hacienda Catwalk Wall", 0, "hacienda_accessC_group", "TeleportFreeHaciendaWall", "", "");
    addLockableDoor("lga", "LGA The DotGrill Wall", 0, "degrassi_accessC_group", "TeleportFreedegrassiwall", "", "");
    addLockableDoor("lga", "LGA The DotGrill Door", 0, "degrassi_door_group", "TeleportFreedegrassi", "", "");
    addLockableDoor("lga", "LGA DDD", 0, "", "TeleportFreeDDD", "dddclub_mainrope", "");
    addLockableDoor("lga", "LGA Outside Bar", 0, "", "TeleportFreeOutsideBar", "outsidebar_rope", "");
    addLockableDoor("lga", "LGA DownTown Records", 0, "downtownrecordsrope", "TeleportFreeDowntownRecs", "", "");
    addLockableDoor("lga", "LGA VHD", 0, "vhd_access_group", "TeleportFreeVHD", "", "");
    addLockableDoor("lga", "LGA VHD Runway", 0, "vhd_access_stagegroup", "TeleportFreeVHDstage", "", "");
    addLockableDoor("lga", "LGA Stage Rope", 0, "stageRopeGroup", "TeleportFreeStage", "", "");
    return ;
}
initProjectSpecificDoors();

