function StaticShapeData::create(%data) {
    %obj = new StaticShape("") {
        dataBlock = 0 @ %data;
    };
    return %obj;
};
function ScopeAlwaysShapeData::create(%data) {
    %obj = new ScopeAlwaysShape("") {
        dataBlock = 0 @ %data;
    };
    return %obj;
};
