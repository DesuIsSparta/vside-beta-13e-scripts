function AmbientAnimateShapeData::onAdd(%unused, %obj) {
    %obj.playThread(0, "ambient");
    return;
};
className = datablock () @ AmbientAnimateShapeData;
BasicAmbientAnimateShapeData;
category = 0 @ StaticShapeData @ "AutoAnimate";
exec("./etsShapes.cs");
