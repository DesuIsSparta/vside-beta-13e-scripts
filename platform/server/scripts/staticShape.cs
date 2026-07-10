function StaticShapeData::create(%data) {
    dataBlock = StaticShape @ new ""() @ %data;
    0;
    %obj = ;
    return %obj;
};
function ScopeAlwaysShapeData::create(%data) {
    dataBlock = ScopeAlwaysShape @ new ""() @ %data;
    0;
    %obj = ;
    return %obj;
};
