category = datablock MissionMarkerData(WayPointMarker) @ "Misc";
shapeFile = "projects/common/worlds/markers/octahedron.dts";
category = datablock MissionMarkerData(SpawnSphereMarker) @ "Misc";
shapeFile = "projects/common/worlds/markers/octahedron.dts";
function MissionMarkerData::Create(%block) {
    if ((%block $= "WayPointMarker")) {
        dataBlock = WayPoint @ new ""() @ %block;
        0;
        %obj = ;
        return %obj;
    }
    if ((%block $= "SpawnSphereMarker")) {
        dataBlock = SpawnSphere @ new ""() @ %block;
        0;
        %obj = ;
        return %obj;
    }
    if ((%block $= "SeatMarker")) {
        dataBlock = MissionMarker @ new ""() @ %block;
        0;
        sitOffset = %block @ sitOffset;
        sitAnim = %block @ sitAnim;
        standAnim = %block @ standAnim;
        sitIdle = %block @ sitIdle;
        idleDelay = %block @ idleDelay;
        listeningStation = %block @ listeningStation;
        sitSound = %block @ sitSound;
        standSound = %block @ standSound;
        %obj = ;
        return %obj;
    }
    return -(1.0);
};
