function DeclareFloorplans()
{
    DeclareFloorplan("NV255_S", 45000);
    DeclareFloorplan("NV255_M", 45001);
    DeclareFloorplan("NV255_L", 45002);
    DeclareFloorplan("LoftApartment", 45003);
    DeclareFloorplan("LGATowerS", 45004);
    DeclareFloorplan("LGATowerM", 45005);
    DeclareFloorplan("LGATowerL", 45006);
    DeclareFloorplan("LaBocaYatchM", 45007);
    DeclareFloorplan("LaBocaYatchL", 45008);
    DeclareFloorplan("iiRResidences_S", 45009);
    DeclareFloorplan("iiRResidences_M", 45010);
    DeclareFloorplan("iiRResidences_L", 45011);
    DeclareFloorplan("BeatupApartment", 45012);
    DeclareFloorplan("TheDotDorms_A", 45013);
    DeclareFloorplan("TheDotDorms_B", 45014);
    DeclareFloorplan("LaVilla", 45015);
    DeclareFloorplan("ClubRage", 45016);
    DeclareFloorplan("Islands", 45017);
    return ;
}
function DeclareBuildings()
{
    DeclareBuilding("LoftApartments", "The Warehouse Lofts", "", "Freshman", "nv_warehouselofts", "LoftApartment");
    DeclareBuilding("NV255Lofts", "NV255 Lofts", "", "Freshman", "nv_nv255", "NV255_L NV255_M NV255_S");
    DeclareBuilding("ClubRage", "Club Rage", "", "Freshman", "nv_clubrage", "ClubRage");
    DeclareBuilding("LGATower", "The LGA Tower", "", "Senior", "lga_tower", "LGATowerL LGATowerM LGATowerS");
    DeclareBuilding("LGAHarbor", "Luxury Yachts", "", "Senior", "lga_yachts", "LaBocaYatchL LaBocaYatchM");
    DeclareBuilding("TheDotGrill", "The Dot Dorms", "", "Freshman", "lga_dotdorms_A lga_dotdorms_B", "TheDotDorms_A TheDotDorms_B");
    DeclareBuilding("LGAHarbor2", "La Villa", "", "Senior", "lga_lavilla", "LaVilla");
    DeclareBuilding("HotelRaijuku", "iiR Residences", "", "Senior", "rj_iir", "iiRResidences_L iiRResidences_M iiRResidences_S");
    DeclareBuilding("RunDownRaijukuApartments", "Hidden Jewel Apartments", "", "Junior", "", "BeatupApartment");
    DeclareBuilding("RJHarbor", "Island Apartments", "", "Freshman", "rj_islands", "Islands");
    return ;
}
DeclareFloorplans();
DeclareBuildings();

