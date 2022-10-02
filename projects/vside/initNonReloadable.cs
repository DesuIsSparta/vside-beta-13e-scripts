$ETS::AppName = "vSide";
$ETS::AppVersion = "Beta 13e";
function setupProjectSpecificUrls()
{
    return ;
}
exec("./worlds/gateway/initNonReloadable.cs", 0);
exec("./worlds/lga/initNonReloadable.cs", 0);
exec("./worlds/lounge/initNonReloadable.cs", 0);
exec("./worlds/raijuku/initNonReloadable.cs", 0);
exec("./worlds/minimal/initNonReloadable.cs", 0);
exec("./tutorialDefinitions.cs", 0);

