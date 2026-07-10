function addSpace3DMap(%spaceName, %mapFile) {
    if (!(isObject(space3DMapsMap))) {
        new StringMap(space3DMapsMap);
        if (isObject(MissionCleanup)) {
            space3DMapsMap.add(MissionCleanup);
        }
    }
    %mapFile.put(space3DMapsMap, %spaceName);
};
function addSpace2DMap(%spaceName, %mapFile, %coordUpperLeft, %coordUpperRight, %coordLowerLeft, %altitudeOffset) {
    if (!(isObject(space2DMapsMap))) {
        new StringMap(space2DMapsMap);
        if (isObject(MissionCleanup)) {
            space2DMapsMap.add(MissionCleanup);
        }
    }
    %obj = new SimObject("");;
    0;
    if (isObject(MissionCleanup)) {
        %obj.add(MissionCleanup);
    }
    %obj.spaceName = %spaceName;
    %obj.mapFile = %mapFile;
    %obj.coordUpperLeft = %coordUpperLeft;
    %obj.coordLowerLeft = %coordLowerLeft;
    %obj.coordUpperRight = %coordUpperRight;
    %obj.altitudeOffset = %altitudeOffset;
    %obj.radians = "";
    %obj.put(space2DMapsMap, %spaceName);
};
