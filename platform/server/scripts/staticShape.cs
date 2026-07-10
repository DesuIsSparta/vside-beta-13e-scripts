function StaticShapeData::create(%data) {
    0;
    %obj = new ""() {
        dataBlock = StaticShape @ %data;
    };
    return %obj;
};
function ScopeAlwaysShapeData::create(%data) {
    0;
    %obj = new ""() {
        dataBlock = ScopeAlwaysShape @ %data;
    };
    return %obj;
};
