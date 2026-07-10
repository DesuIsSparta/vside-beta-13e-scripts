$gGetFakeBuildingDirectory = 0;
function CustomSpacesSelector::getFakeBuildingDirectory(%unused) {
    %buildingInfo = new SimGroup("") {
        name = "Hotel Erez";
        description = Buildings::GetLongDescription("");
        floorPlanCount = 4;
    };
    %floorplan = new SimObject("");
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%floorplan);
    }
    %floorplan.name = "floorPlan007";
    %floorplan.description = "floorPlanDescription";
    %floorplan.capacity = 999;
    %floorplan.minLevel = -1;
    %floorplan.priceVBux = 50;
    %floorplan.priceVPoints = "50,000";
    %floorplan.isUpgrade = 0;
    %floorplan.numAvailable = 42;
    %buildingInfo.floorplan[0] = %floorplan;
    %floorplan = new SimObject("");
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%floorplan);
    }
    %floorplan.name = "floorPlan007vpointsonly";
    %floorplan.description = "floorPlanDescription";
    %floorplan.capacity = 999;
    %floorplan.minLevel = -1;
    %floorplan.priceVBux = -1;
    %floorplan.priceVPoints = "50,000";
    %floorplan.isUpgrade = 0;
    %floorplan.numAvailable = 42;
    %buildingInfo.floorplan[1] = %floorplan;
    %floorplan = new SimObject("");
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%floorplan);
    }
    %floorplan.name = "floorPlan007vbuxonly";
    %floorplan.description = "floorPlanDescription";
    %floorplan.capacity = 999;
    %floorplan.minLevel = -1;
    %floorplan.priceVBux = 50;
    %floorplan.priceVPoints = -1;
    %floorplan.isUpgrade = 0;
    %floorplan.numAvailable = 42;
    %buildingInfo.floorplan[2] = %floorplan;
    %floorplan = new SimObject("");
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%floorplan);
    }
    %floorplan.name = "floorPlan007noavailable";
    %floorplan.description = "floorPlanDescription";
    %floorplan.capacity = 999;
    %floorplan.minLevel = -1;
    %floorplan.priceVBux = -1;
    %floorplan.priceVPoints = -1;
    %floorplan.isUpgrade = 0;
    %floorplan.numAvailable = 42;
    %buildingInfo.floorplan[3] = %floorplan;
    %buildingDir = new SimGroup("");
    new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = new SimGroup("") {
        owner = "the-manager";
        name = "Kenna";
        type = "CELEBSPACE";
        description = "Kenna's Loft";
        isFeatured = 1;
        floorPlanName = "floorPlan007";
        floorplan = %buildingInfo.floorplan[0];
        occupancy = 62;
        access = "open";
        vurl = "someVURL";
        longDescription = "yay long description!!";
        audioStream = "myAudioStream";
        videoStream = "myVideoStream";
        buildingName = %buildingInfo.name;
    }; @ "BBBB";
        name = 1000;
        type = "RESIDENCE";
        description = "i am door code-protected";
        isFeatured = 0;
        floorPlanName = "floorPlan007";
        floorplan = %buildingInfo.floorplan[0];
        occupancy = 101;
        access = "PASSWORDPROTECTED";
        vurl = "someVURL";
        longDescription = "yay long description!!";
        audioStream = "myAudioStream";
        videoStream = "myVideoStream";
        buildingName = %buildingInfo.name;
    }; @ "DDDD";
        name = 1002;
        type = "RESIDENCE";
        description = "foo foo";
        isFeatured = 0;
        floorPlanName = "floorPlan007";
        floorplan = %buildingInfo.floorplan[0];
        occupancy = 101;
        access = "FriendsOnly";
        vurl = "someVURL";
        longDescription = "yay long description!!";
        audioStream = "myAudioStream";
        videoStream = "myVideoStream";
        buildingName = %buildingInfo.name;
    }; @ "CCCC";
        name = 1003;
        type = "RESIDENCE";
        description = "foo foo";
        isFeatured = 0;
        floorPlanName = "floorPlan007";
        floorplan = %buildingInfo.floorplan[0];
        occupancy = 101;
        access = "FriendsOnly";
        vurl = "someVURL";
        longDescription = "yay long description!!";
        audioStream = "myAudioStream";
        videoStream = "myVideoStream";
        buildingName = %buildingInfo.name;
    }; @ "Stacy gfghjfghjf ghj 5y jgn fgjhtyj5y";
        name = 258;
        type = "RESIDENCE";
        description = "DJ Skully Show";
        isFeatured = 1;
        floorPlanName = "floorPlan007";
        floorplan = %buildingInfo.floorplan[0];
        occupancy = 986;
        access = "FriendsOnly";
        vurl = "someVURL";
        longDescription = "yay long description!!";
        audioStream = "myAudioStream";
        videoStream = "myVideoStream";
        buildingName = %buildingInfo.name;
    }; @ "aArOn";
        name = 369;
        type = "RESIDENCE";
        description = "MusicLand";
        isFeatured = 0;
        floorPlanName = "floorPlan007";
        floorplan = %buildingInfo.floorplan[0];
        occupancy = 21;
        access = "open";
        vurl = "someVURL";
        longDescription = "yay long description!!";
        audioStream = "myAudioStream";
        videoStream = "myVideoStream";
        buildingName = %buildingInfo.name;
    }; @ "erez gfghjfghjf ghj 5y jgn fgjhtyj5y";
        name = 159;
        type = "RESIDENCE";
        description = "Lincoln Log Cabin";
        isFeatured = 1;
        floorPlanName = "floorPlan007";
        floorplan = %buildingInfo.floorplan[0];
        occupancy = 777;
        access = "open";
        vurl = "someVURL";
        longDescription = "yay long description!!";
        audioStream = "myAudioStream";
        videoStream = "myVideoStream";
        buildingName = %buildingInfo.name;
    }; @ "adam gfghjfghjf ghj 5y jgn fgjhtyj5y";
        name = 753;
        type = "RESIDENCE";
        description = "RCMP HQ";
        isFeatured = 0;
        floorPlanName = "floorPlan007";
        floorplan = %buildingInfo.floorplan[0];
        occupancy = 444;
        access = "FriendsOnly";
        vurl = "someVURL";
        longDescription = "yay long description!!";
        audioStream = "myAudioStream";
        vi /* expression truncated */;
    customSpaceSelGotData(%buildingInfo, %buildingDir);
};
