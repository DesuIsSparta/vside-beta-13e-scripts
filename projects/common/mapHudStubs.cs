function addSpace3DMap(%spaceName, %mapFile) {
    new ();
    add();
    %spaceName.put(%mapFile);
};
function addSpace2DMap(%spaceName, %mapFile, %coordUpperLeft, %coordUpperRight, %coordLowerLeft, %altitudeOffset) {
    new ();
    add();
    %obj = new ""();
    SimObject;
    %obj.add();
    spaceName = MissionCleanup @ %spaceName @ %obj;
    isObject();
    mapFile = MissionCleanup @ %mapFile @ %obj;
    0;
    coordUpperLeft = space2DMapsMap @ %coordUpperLeft @ %obj;
    MissionCleanup;
    coordLowerLeft = isObject() @ %coordLowerLeft @ %obj;
    MissionCleanup;
    coordUpperRight = space2DMapsMap @ %coordUpperRight @ %obj;
    StringMap;
    altitudeOffset = 0 @ %altitudeOffset @ %obj;
    !(isObject());
    radians = space2DMapsMap @ "" @ %obj;
    %spaceName.put(%obj);
};
