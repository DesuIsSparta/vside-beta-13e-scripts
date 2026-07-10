%cityName = $ETS::cityName;
if (%cityName $= "gw")
{
    exec("./worlds/gateway/tutorials/tutorialDefinitions.cs");
}
else
{
    if (%cityName $= "rj")
    {
        exec("./worlds/raijuku/tutorials/tutorialDefinitions.cs");
    }
    if (%cityName $= "lga")
    {
        exec("./worlds/lga/tutorials/tutorialDefinitions.cs");
    }
    if (%cityName $= "nv")
    {
        exec("./worlds/lounge/tutorials/tutorialDefinitions.cs");
    }
    if (%cityName $= "minimal")
    {
        exec("./worlds/minimal/tutorials/tutorialDefinitions.cs");
    }
    error("Unknown cityname " @ %cityName @ " for tutorials!");
}
