category = AcePetrolRoof @ datablock () @ "Commercials";
StaticShapeData;
shapeFile = 0 @ "~/data/shapes/commercials/acepetrol_roof.dts";
isPlaying = 1;
function AcePetrolRoof::onAdd(%unused, %obj) {
    %obj.playThread(0, "ambient");
    echo("onAdd");
    isPlaying = 1 @ %obj;
};
category = AcePetrolWall @ datablock () @ "Commercials";
StaticShapeData;
shapeFile = 0 @ "~/data/shapes/commercials/acepetrol_wall.dts";
isPlaying = 1;
function AcePetrolWall::onAdd(%unused, %obj) {
    %obj.playThread(0, "ambient");
    echo("onAdd");
    isPlaying = 1 @ %obj;
};
category = Cowboy @ datablock () @ "Commercials";
StaticShapeData;
shapeFile = 0 @ "~/data/shapes/commercials/commercialCowBoy.dts";
isPlaying = 1;
function Cowboy::onAdd(%unused, %obj) {
    %obj.playThread(0, "ambient");
    echo("onAdd");
    isPlaying = 1 @ %obj;
};
category = flipcom @ datablock () @ "Commercials";
StaticShapeData;
shapeFile = 0 @ "~/data/shapes/commercials/flipcom.dts";
isPlaying = 1;
function flipcom::onAdd(%unused, %obj) {
    %obj.playThread(0, "ambient");
    echo("onAdd");
    isPlaying = 1 @ %obj;
};
category = HotelSign1 @ datablock () @ "Commercials";
StaticShapeData;
shapeFile = 0 @ "~/data/shapes/commercials/hotelSignRotating.dts";
isPlaying = 1;
function HotelSign1::onAdd(%unused, %obj) {
    %obj.playThread(0, "ambient");
    echo("onAdd");
    isPlaying = 1 @ %obj;
};
category = HotelSign2 @ datablock () @ "Commercials";
StaticShapeData;
shapeFile = 0 @ "~/data/shapes/commercials/hotelSignRotatingVertical.dts";
isPlaying = 1;
function HotelSign2::onAdd(%unused, %obj) {
    %obj.playThread(0, "ambient");
    echo("onAdd");
    isPlaying = 1 @ %obj;
};
category = Tire @ datablock () @ "Commercials";
StaticShapeData;
shapeFile = 0 @ "~/data/shapes/commercials/rotatingTire.dts";
isPlaying = 1;
function Tire::onAdd(%unused, %obj) {
    %obj.playThread(0, "ambient");
    echo("onAdd");
    isPlaying = 1 @ %obj;
};
