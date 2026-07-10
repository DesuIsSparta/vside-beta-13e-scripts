function AmbientAnimateShapeData::onAdd(%unused, %obj) {
    0.playThread("ambient", %obj);
    return;
};
datablock StaticShapeData(BasicAmbientAnimateShapeData) {
    className = AmbientAnimateShapeData;
    category = "AutoAnimate";
};
exec("./etsShapes.cs");
