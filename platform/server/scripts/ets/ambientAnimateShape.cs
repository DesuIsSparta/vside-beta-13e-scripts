function AmbientAnimateShapeData::onAdd(%unused, %obj) {
    %obj.playThread(0, "ambient");
    return;
};
className = datablock StaticShapeData(BasicAmbientAnimateShapeData) @ AmbientAnimateShapeData;
category = "AutoAnimate";
exec("./etsShapes.cs");
