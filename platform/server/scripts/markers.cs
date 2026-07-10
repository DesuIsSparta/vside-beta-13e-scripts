datablock MissionMarkerData(WayPointMarker) {
    category = "Misc";
    shapeFile = "projects/common/worlds/markers/octahedron.dts";
};
datablock MissionMarkerData(SpawnSphereMarker) {
    category = "Misc";
    shapeFile = "projects/common/worlds/markers/octahedron.dts";
};
function MissionMarkerData::Create(%block) {
    if ((%block $= "WayPointMarker")) {
        0;
        %obj = new ""() {
            dataBlock = WayPoint @ %block;
        };
        return %obj;
    }
    if ((%block $= "SpawnSphereMarker")) {
        0;
        %obj = new ""() {
            dataBlock = SpawnSphere @ %block;
        };
        return %obj;
    }
    if ((%block $= "SeatMarker")) {
        0;
        %obj = new ""() {
            dataBlock = MissionMarker @ %block;
            sitOffset = %block.sitOffset;
            sitAnim = %block.sitAnim;
            standAnim = %block.standAnim;
            sitIdle = %block.sitIdle;
            idleDelay = %block.idleDelay;
            listeningStation = %block.listeningStation;
            sitSound = %block.sitSound;
            standSound = %block.standSound;
        };
        return %obj;
    }
    return -(1.0);
};
