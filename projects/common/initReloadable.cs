$Settings::VisibleDistances[0] = 200;
$Settings::VisibleDistances[1] = 300;
$Settings::VisibleDistances[2] = 1000;
$Settings::VisibleDistances[3] = $Settings::VisibleDistances[2] ;
exec("./eventControlsProject.cs");
exec("./brands.cs");
exec("./buildings.cs");
exec("./destinations.cs");
exec("./messageCatalog.cs");
exec("./characters/initReloadable.cs");
exec("./spaceDefinitions.cs");
exec("./mapHudStubs.cs");

