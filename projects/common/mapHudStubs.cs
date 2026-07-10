function addSpace3DMap(%spaceName, %mapFile) {
    if (!(isObject())) {
        new StringMap(space3DMapsMap);
        if (isObject()) {
            add();
        }
    }
    %spaceName.put(%mapFile);
};
function addSpace2DMap(%spaceName, %mapFile, %coordUpperLeft, %coordUpperRight, %coordLowerLeft, %altitudeOffset) {
    if (!(isObject())) {
        new StringMap(space2DMapsMap);
        if (isObject()) {
            add();
        }
    }
    %obj = new ""();
    SimObject;
    if (isObject()) {
        %obj.add();
    }
    spaceName = MissionCleanup @ %spaceName @ %obj;
    MissionCleanup;
    mapFile = 0 @ %mapFile @ %obj;
    space2DMapsMap;
    coordUpperLeft = MissionCleanup @ %coordUpperLeft @ %obj;
    MissionCleanup;
    coordLowerLeft = space2DMapsMap @ %coordLowerLeft @ %obj;
    coordUpperRight = %coordUpperRight @ %obj;
    altitudeOffset = %altitudeOffset @ %obj;
    radians = "" @ %obj;
    %spaceName.put(%obj);
};
