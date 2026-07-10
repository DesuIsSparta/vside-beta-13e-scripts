function addSpace3DMap(%spaceName, %mapFile) {
    if (!(isObject(space3DMapsMap))) {
        new StringMap(space3DMapsMap);
        if (isObject(MissionCleanup)) {
            MissionCleanup.add(space3DMapsMap);
        }
    }
    %spaceName.put(%mapFile);
};
function addSpace2DMap(%spaceName, %mapFile, %coordUpperLeft, %coordUpperRight, %coordLowerLeft, %altitudeOffset) {
    if (!(isObject(space2DMapsMap))) {
        new StringMap(space2DMapsMap);
        if (isObject(MissionCleanup)) {
            MissionCleanup.add(space2DMapsMap);
        }
    }
    %obj = new ""();;
    SimObject;
    if (isObject(MissionCleanup)) {
        %obj.add();
    }
    %obj.spaceName = MissionCleanup @ %spaceName;
    0;
    %obj.mapFile = %mapFile;
    %obj.coordUpperLeft = %coordUpperLeft;
    %obj.coordLowerLeft = %coordLowerLeft;
    %obj.coordUpperRight = %coordUpperRight;
    %obj.altitudeOffset = %altitudeOffset;
    %obj.radians = "";
    %spaceName.put(%obj);
};
