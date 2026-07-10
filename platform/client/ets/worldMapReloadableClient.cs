function WorldMap::TabulateWorldAreaSummary(%unused) {
    safeEnsureScriptObject("StringMap", 0);
    deleteValuesAsObjects();
    totalCapacity = WorldAreaSummaries @ WorldAreaSummaries @ 0 @ "gw" @ WorldAreaSummaries;
    totalCapacity = 0 @ "pvt" @ WorldAreaSummaries;
    totalCapacity = 0 @ "city" @ WorldAreaSummaries;
    totalOccupancy = 0 @ "gw" @ WorldAreaSummaries;
    totalOccupancy = 0 @ "pvt" @ WorldAreaSummaries;
    totalOccupancy = 0 @ "city" @ WorldAreaSummaries;
    if (!(isObject())) {
        error(getScopeName() @ " " @ "- no WorldMapServers object." @ " " @ getTrace());
        return WorldMapServers;
    }
    %n = (WorldMapServers - getCount());
    1.0;
    if ((0.0 >= %n)) {
        %serverObj = %n.getObject();
        WorldMapServers;
        %serverAreaName = %serverObj.get("city");
        %serverCapacity = %serverObj.get("capacity");
        %serverOccupancy = %serverObj.get("load");
        %areaSummaryObj = %serverAreaName.get();
        WorldAreaSummaries;
        if (!(isObject(%areaSummaryObj))) {
            %areaSummaryObj = new ""();
            SimObject;
            %serverAreaName.put(%areaSummaryObj);
            areaName = WorldAreaSummaries @ %serverAreaName @ %areaSummaryObj;
            0;
            occupancy = 0 @ %areaSummaryObj;
            capacity = 0 @ %areaSummaryObj;
            numServers = 0 @ %areaSummaryObj;
            areaType = hasWord("lga nv rj", %serverAreaName) ? "city" : "pvt" @ %areaSummaryObj;
            if (hasWord("gw", %serverAreaName)) {
            }
            areaType = %areaSummaryObj @ areaType @ %areaSummaryObj;
            "gw";
        }
        occupancy = (%areaSummaryObj + occupancy);
        %serverOccupancy;
        capacity = (%areaSummaryObj + capacity);
        %serverCapacity;
        numServers = (%areaSummaryObj + numServers);
        1.0;
        totalOccupancy = (%areaSummaryObj @ areaType @ WorldAreaSummaries + totalOccupancy);
        %serverOccupancy;
        totalCapacity = (%areaSummaryObj @ areaType @ WorldAreaSummaries + totalCapacity);
        %serverCapacity;
        %n = (1.0 - %n);
    }
};
