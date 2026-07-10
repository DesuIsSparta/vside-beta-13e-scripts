function AmbientAnimateShapeData::onAdd(%unused, %obj) {
    "ambient".playThread(%obj, 0);
    return;
};
datablock StaticShapeData(BasicAmbientAnimateShapeData) {
    className = AmbientAnimateShapeData;
    category = "AutoAnimate";
};
exec("./etsShapes.cs");
