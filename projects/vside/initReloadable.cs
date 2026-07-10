$AmClient = !(hasArg("-dedicated"));
if (hasArg("-dedicated")) {
}
$AmServer = hasArg("-standalone");
exec("./worlds/" @ "gateway" @ "/initReloadable.cs", 0);
exec("./worlds/" @ "lga" @ "/initReloadable.cs", 0);
exec("./worlds/" @ "lounge" @ "/initReloadable.cs", 0);
exec("./worlds/" @ "raijuku" @ "/initReloadable.cs", 0);
exec("./worlds/" @ "minimal" @ "/initReloadable.cs", 0);
exec("./eventControlsProject.cs");
exec("./brands.cs");
exec("./destinations.cs");
exec("./buildings.cs");
