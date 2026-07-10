function WorldMap::TabulateWorldAreaSummary(%unused) {
    safeEnsureScriptObject("StringMap", WorldAreaSummaries, 0);
    WorldAreaSummaries.deleteValuesAsObjects();
    totalCapacity = 0 @ "gw" @ WorldAreaSummaries;
    totalCapacity = 0 @ "pvt" @ WorldAreaSummaries;
    totalCapacity = 0 @ "city" @ WorldAreaSummaries;
    totalOccupancy = 0 @ "gw" @ WorldAreaSummaries;
    totalOccupancy = 0 @ "pvt" @ WorldAreaSummaries;
    totalOccupancy = 0 @ "city" @ WorldAreaSummaries;
    if (!(isObject(WorldMapServers))) {
        error(getScopeName() @ " " @ "- no WorldMapServers object." @ " " @ getTrace());
        return;
    }
    %n = (1.0 - WorldMapServers.getCount());
    if ((0.0 >= %n)) {
        %serverObj = WorldMapServers.getObject(%n);
        %serverAreaName = %serverObj.get("city");
        %serverCapacity = %serverObj.get("capacity");
        %serverOccupancy = %serverObj.get("load");
        %areaSummaryObj = WorldAreaSummaries.get(%serverAreaName);
        if (!(isObject(%areaSummaryObj))) {
            %areaSummaryObj = new SimObject("");;
            0;
            WorldAreaSummaries.put(%serverAreaName, %areaSummaryObj);
            %areaSummaryObj.areaName = %serverAreaName;
            %areaSummaryObj.occupancy = 0;
            %areaSummaryObj.capacity = 0;
            %areaSummaryObj.numServers = 0;
            %areaSummaryObj.areaType = hasWord("lga nv rj", %serverAreaName) ? "city" : "pvt";
            if (hasWord("gw", %serverAreaName)) {
            }
            %areaSummaryObj.areaType = "gw" @ %areaSummaryObj.areaType;
        }
        %areaSummaryObj.occupancy = (%serverOccupancy + %areaSummaryObj.occupancy);
        %areaSummaryObj.capacity = (%serverCapacity + %areaSummaryObj.capacity);
        %areaSummaryObj.numServers = (1.0 + %areaSummaryObj.numServers);
        %areaSummaryObj.totalOccupancy = (%areaSummaryObj.areaType @ WorldAreaSummaries + %areaSummaryObj.totalOccupancy);
        %serverOccupancy;
        %areaSummaryObj.totalCapacity = (%areaSummaryObj.areaType @ WorldAreaSummaries + %areaSummaryObj.totalCapacity);
        %serverCapacity;
        %n = (1.0 - %n);
    }
};
