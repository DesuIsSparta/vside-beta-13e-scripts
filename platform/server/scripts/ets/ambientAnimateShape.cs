function AmbientAnimateShapeData::onAdd(%unused, %obj)
{
    %obj.playThread(0, "ambient");
    return ;
}
datablock StaticShapeData(BasicAmbientAnimateShapeData)
{
    className = AmbientAnimateShapeData;
    category = "AutoAnimate";
};
exec("./etsShapes.cs");

