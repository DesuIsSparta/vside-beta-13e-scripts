$gGetFakeBuildingDirectory = 0;
function CustomSpacesSelector::getFakeBuildingDirectory(%unused) {
    %buildingInfo = new SimGroup("") {
        name = "Hotel Erez";
        description = Buildings::GetLongDescription("");
        floorPlanCount = 4;
    };
    %floorplan = new SimObject("");
    if (isObject(MissionCleanup)) {
        %floorplan.add(MissionCleanup);
    }
    %floorplan.name = "floorPlan007";
    %floorplan.description = "floorPlanDescription";
    %floorplan.capacity = 999;
    %floorplan.minLevel = -1;
    %floorplan.priceVBux = 50;
    %floorplan.priceVPoints = "50,000";
    %floorplan.isUpgrade = 0;
    %floorplan.numAvailable = 42;
    %buildingInfo.floorplan = %floorplan @ 0;
    %floorplan = new SimObject("");
    if (isObject(MissionCleanup)) {
        %floorplan.add(MissionCleanup);
    }
    %floorplan.name = "floorPlan007vpointsonly";
    %floorplan.description = "floorPlanDescription";
    %floorplan.capacity = 999;
    %floorplan.minLevel = -1;
    %floorplan.priceVBux = -1;
    %floorplan.priceVPoints = "50,000";
    %floorplan.isUpgrade = 0;
    %floorplan.numAvailable = 42;
    %buildingInfo.floorplan = %floorplan @ 1;
    %floorplan = new SimObject("");
    if (isObject(MissionCleanup)) {
        %floorplan.add(MissionCleanup);
    }
    %floorplan.name = "floorPlan007vbuxonly";
    %floorplan.description = "floorPlanDescription";
    %floorplan.capacity = 999;
    %floorplan.minLevel = -1;
    %floorplan.priceVBux = 50;
    %floorplan.priceVPoints = -1;
    %floorplan.isUpgrade = 0;
    %floorplan.numAvailable = 42;
    %buildingInfo.floorplan = %floorplan @ 2;
    %floorplan = new SimObject("");
    if (isObject(MissionCleanup)) {
        %floorplan.add(MissionCleanup);
    }
    %floorplan.name = "floorPlan007noavailable";
    %floorplan.description = "floorPlanDescription";
    %floorplan.capacity = 999;
    %floorplan.minLevel = -1;
    %floorplan.priceVBux = -1;
    %floorplan.priceVPoints = -1;
    %floorplan.isUpgrade = 0;
    %floorplan.numAvailable = 42;
    %buildingInfo.floorplan = %floorplan @ 3;
    %buildingDir = new SimGroup("");
    customSpaceSelGotData(%buildingInfo, %buildingDir);
};
