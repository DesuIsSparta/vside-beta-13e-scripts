datablock StaticShapeData(AcePetrolRoof)
{
    category = "Commercials";
    shapeFile = "~/data/shapes/commercials/acepetrol_roof.dts";
    isPlaying = 1;
};
function AcePetrolRoof::onAdd(%unused, %obj)
{
    %obj.playThread(0, "ambient");
    echo("onAdd");
    %obj.isPlaying = 1;
    return ;
}
datablock StaticShapeData(AcePetrolWall)
{
    category = "Commercials";
    shapeFile = "~/data/shapes/commercials/acepetrol_wall.dts";
    isPlaying = 1;
};
function AcePetrolWall::onAdd(%unused, %obj)
{
    %obj.playThread(0, "ambient");
    echo("onAdd");
    %obj.isPlaying = 1;
    return ;
}
datablock StaticShapeData(Cowboy)
{
    category = "Commercials";
    shapeFile = "~/data/shapes/commercials/commercialCowBoy.dts";
    isPlaying = 1;
};
function Cowboy::onAdd(%unused, %obj)
{
    %obj.playThread(0, "ambient");
    echo("onAdd");
    %obj.isPlaying = 1;
    return ;
}
datablock StaticShapeData(flipcom)
{
    category = "Commercials";
    shapeFile = "~/data/shapes/commercials/flipcom.dts";
    isPlaying = 1;
};
function flipcom::onAdd(%unused, %obj)
{
    %obj.playThread(0, "ambient");
    echo("onAdd");
    %obj.isPlaying = 1;
    return ;
}
datablock StaticShapeData(HotelSign1)
{
    category = "Commercials";
    shapeFile = "~/data/shapes/commercials/hotelSignRotating.dts";
    isPlaying = 1;
};
function HotelSign1::onAdd(%unused, %obj)
{
    %obj.playThread(0, "ambient");
    echo("onAdd");
    %obj.isPlaying = 1;
    return ;
}
datablock StaticShapeData(HotelSign2)
{
    category = "Commercials";
    shapeFile = "~/data/shapes/commercials/hotelSignRotatingVertical.dts";
    isPlaying = 1;
};
function HotelSign2::onAdd(%unused, %obj)
{
    %obj.playThread(0, "ambient");
    echo("onAdd");
    %obj.isPlaying = 1;
    return ;
}
datablock StaticShapeData(Tire)
{
    category = "Commercials";
    shapeFile = "~/data/shapes/commercials/rotatingTire.dts";
    isPlaying = 1;
};
function Tire::onAdd(%unused, %obj)
{
    %obj.playThread(0, "ambient");
    echo("onAdd");
    %obj.isPlaying = 1;
    return ;
}
