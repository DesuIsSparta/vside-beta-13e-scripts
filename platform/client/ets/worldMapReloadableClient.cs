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
    %n = (WorldMapServers.getCount() - 1.0);
    while ((%n >= 0.0)) {
        %serverObj = %n.getObject(WorldMapServers);
        %serverAreaName = "city".get(%serverObj);
        %serverCapacity = "capacity".get(%serverObj);
        %serverOccupancy = "load".get(%serverObj);
        %areaSummaryObj = %serverAreaName.get(WorldAreaSummaries);
        if (!(isObject(%areaSummaryObj))) {
            %areaSummaryObj = new SimObject("");;
            0;
            %areaSummaryObj.put(WorldAreaSummaries, %serverAreaName);
            %areaSummaryObj.areaName = %serverAreaName;
            %areaSummaryObj.occupancy = 0;
            %areaSummaryObj.capacity = 0;
            %areaSummaryObj.numServers = 0;
            %areaSummaryObj.areaType = hasWord("lga nv rj", %serverAreaName) ? "city" : "pvt";
            if (hasWord("gw", %serverAreaName)) {
            }
            %areaSummaryObj.areaType = "gw" @ %areaSummaryObj.areaType;
        }
        %areaSummaryObj.occupancy = (%areaSummaryObj.occupancy + %serverOccupancy);
        %areaSummaryObj.capacity = (%areaSummaryObj.capacity + %serverCapacity);
        %areaSummaryObj.numServers = (%areaSummaryObj.numServers + 1.0);
        %areaSummaryObj.totalOccupancy = (%areaSummaryObj.totalOccupancy + %areaSummaryObj.areaType @ WorldAreaSummaries);
        %serverOccupancy;
        %areaSummaryObj.totalCapacity = (%areaSummaryObj.totalCapacity + %areaSummaryObj.areaType @ WorldAreaSummaries);
        %serverCapacity;
        %n = (%n - 1.0);
    }
};
