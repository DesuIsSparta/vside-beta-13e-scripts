function WorldMap::TabulateWorldAreaSummary(%unused)
{
    safeEnsureScriptObject("StringMap", WorldAreaSummaries, 0);
    WorldAreaSummaries.deleteValuesAsObjects();
    WorldAreaSummaries.totalCapacity["gw"] = 0;
    WorldAreaSummaries.totalCapacity["pvt"] = 0;
    WorldAreaSummaries.totalCapacity["city"] = 0;
    WorldAreaSummaries.totalOccupancy["gw"] = 0;
    WorldAreaSummaries.totalOccupancy["pvt"] = 0;
    WorldAreaSummaries.totalOccupancy["city"] = 0;
    if (!isObject(WorldMapServers))
    {
        error(getScopeName() SPC "- no WorldMapServers object." SPC getTrace());
        return ;
    }
    %n = WorldMapServers.getCount() - 1;
    while (%n >= 0)
    {
        %serverObj = WorldMapServers.getObject(%n);
        %serverAreaName = %serverObj.get("city");
        %serverCapacity = %serverObj.get("capacity");
        %serverOccupancy = %serverObj.get("load");
        %areaSummaryObj = WorldAreaSummaries.get(%serverAreaName);
        if (!isObject(%areaSummaryObj))
        {
            %areaSummaryObj = new SimObject();
            WorldAreaSummaries.put(%serverAreaName, %areaSummaryObj);
            %areaSummaryObj.areaName = %serverAreaName;
            %areaSummaryObj.occupancy = 0;
            %areaSummaryObj.capacity = 0;
            %areaSummaryObj.numServers = 0;
            %areaSummaryObj.areaType = hasWord("lga nv rj", %serverAreaName) ? "city" : "pvt";
            %areaSummaryObj.areaType = hasWord("gw", %serverAreaName) ? "gw" : %areaSummaryObj;
        }
        %areaSummaryObj.occupancy = %areaSummaryObj.occupancy + %serverOccupancy;
        %areaSummaryObj.capacity = %areaSummaryObj.capacity + %serverCapacity;
        %areaSummaryObj.numServers = %areaSummaryObj.numServers + 1;
        WorldAreaSummaries.totalOccupancy[%areaSummaryObj.areaType] = WorldAreaSummaries.totalOccupancy[%areaSummaryObj.areaType] + %serverOccupancy;
        WorldAreaSummaries.totalCapacity[%areaSummaryObj.areaType] = WorldAreaSummaries.totalCapacity[%areaSummaryObj.areaType] + %serverCapacity;
        %n = %n - 1;
    }
    return WorldAreaSummaries;
}
