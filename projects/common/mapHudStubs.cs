function addSpace3DMap(%spaceName, %mapFile)
{
    if (!isObject(space3DMapsMap))
    {
        new StringMap(space3DMapsMap);
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(space3DMapsMap);
        }
    }
    space3DMapsMap.put(%spaceName, %mapFile);
    return ;
}
function addSpace2DMap(%spaceName, %mapFile, %coordUpperLeft, %coordUpperRight, %coordLowerLeft, %altitudeOffset)
{
    if (!isObject(space2DMapsMap))
    {
        new StringMap(space2DMapsMap);
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(space2DMapsMap);
        }
    }
    %obj = new SimObject();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%obj);
    }
    %obj.spaceName = %spaceName;
    %obj.mapFile = %mapFile;
    %obj.coordUpperLeft = %coordUpperLeft;
    %obj.coordLowerLeft = %coordLowerLeft;
    %obj.coordUpperRight = %coordUpperRight;
    %obj.altitudeOffset = %altitudeOffset;
    %obj.radians = "";
    space2DMapsMap.put(%spaceName, %obj);
    return ;
}
