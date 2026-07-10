category = datablock StaticShapeData(AcePetrolRoof) @ "Commercials";
shapeFile = "~/data/shapes/commercials/acepetrol_roof.dts";
isPlaying = 1;
function AcePetrolRoof::onAdd(%unused, %obj) {
    %obj.playThread(0, "ambient");
    echo("onAdd");
    isPlaying = 1 @ %obj;
};
category = datablock StaticShapeData(AcePetrolWall) @ "Commercials";
shapeFile = "~/data/shapes/commercials/acepetrol_wall.dts";
isPlaying = 1;
function AcePetrolWall::onAdd(%unused, %obj) {
    %obj.playThread(0, "ambient");
    echo("onAdd");
    isPlaying = 1 @ %obj;
};
category = datablock StaticShapeData(Cowboy) @ "Commercials";
shapeFile = "~/data/shapes/commercials/commercialCowBoy.dts";
isPlaying = 1;
function Cowboy::onAdd(%unused, %obj) {
    %obj.playThread(0, "ambient");
    echo("onAdd");
    isPlaying = 1 @ %obj;
};
category = datablock StaticShapeData(flipcom) @ "Commercials";
shapeFile = "~/data/shapes/commercials/flipcom.dts";
isPlaying = 1;
function flipcom::onAdd(%unused, %obj) {
    %obj.playThread(0, "ambient");
    echo("onAdd");
    isPlaying = 1 @ %obj;
};
category = datablock StaticShapeData(HotelSign1) @ "Commercials";
shapeFile = "~/data/shapes/commercials/hotelSignRotating.dts";
isPlaying = 1;
function HotelSign1::onAdd(%unused, %obj) {
    %obj.playThread(0, "ambient");
    echo("onAdd");
    isPlaying = 1 @ %obj;
};
category = datablock StaticShapeData(HotelSign2) @ "Commercials";
shapeFile = "~/data/shapes/commercials/hotelSignRotatingVertical.dts";
isPlaying = 1;
function HotelSign2::onAdd(%unused, %obj) {
    %obj.playThread(0, "ambient");
    echo("onAdd");
    isPlaying = 1 @ %obj;
};
category = datablock StaticShapeData(Tire) @ "Commercials";
shapeFile = "~/data/shapes/commercials/rotatingTire.dts";
isPlaying = 1;
function Tire::onAdd(%unused, %obj) {
    %obj.playThread(0, "ambient");
    echo("onAdd");
    isPlaying = 1 @ %obj;
};
