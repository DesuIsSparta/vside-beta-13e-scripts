function StaticShapeData::create(%data) {
    %obj = new StaticShape("") {
        dataBlock = %data;
    };
    return %obj;
};
function ScopeAlwaysShapeData::create(%data) {
    %obj = new ScopeAlwaysShape("") {
        dataBlock = %data;
    };
    return %obj;
};
