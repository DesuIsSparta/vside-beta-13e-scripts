datablock MissionMarkerData(WayPointMarker)
{
    category = "Misc";
    shapeFile = "projects/common/worlds/markers/octahedron.dts";
};
datablock MissionMarkerData(SpawnSphereMarker)
{
    category = "Misc";
    shapeFile = "projects/common/worlds/markers/octahedron.dts";
};
function MissionMarkerData::Create(%block)
{
    if (%block $= "WayPointMarker")
    {
        %obj = new WayPoint();
        return %obj;
    }
    else
    {
        if (%block $= "SpawnSphereMarker")
        {
            %obj = new SpawnSphere();
            return %obj;
        }
        else
        {
            if (%block $= "SeatMarker")
            {
                %obj = new MissionMarker()
                {
                    dataBlock = %block;
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
        }
    }
    return -1;
}
