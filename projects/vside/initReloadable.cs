$AmClient = !hasArg("-dedicated");
$AmServer = hasArg("-dedicated") || hasArg("-standalone");
exec("./worlds/" @ "gateway" @ "/initReloadable.cs", 0);
exec("./worlds/" @ "lga" @ "/initReloadable.cs", 0);
exec("./worlds/" @ "lounge" @ "/initReloadable.cs", 0);
exec("./worlds/" @ "raijuku" @ "/initReloadable.cs", 0);
exec("./worlds/" @ "minimal" @ "/initReloadable.cs", 0);
exec("./eventControlsProject.cs");
exec("./brands.cs");
exec("./destinations.cs");
exec("./buildings.cs");
$gBitmapCategoryRoot["badge"] = "projects/vside/client/ui/badges/";
$gBitmapCategoryRoot["token"] = "projects/vside/client/ui/tokens/";
$gBitmapCategoryRoot["swatch"] = "projects/vside/worlds/common/swatches/";

