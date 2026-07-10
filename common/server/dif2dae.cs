function convertDif2Dae() {
    %obj = new InteriorInstance("") {
        position = 0 @ "0 0 0";
        rotation = "1 0 0 0";
        scale = "1 1 1";
        interiorFile = "projects/vside/worlds/lounge/shapes/NV_apartmentb001.dif";
    };
    0.exportToCollada(%obj);
    %obj.add(MissionGroup);
};
