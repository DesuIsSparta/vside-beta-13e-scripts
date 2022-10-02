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
    else
    {
        if (%cityName $= "lga")
        {
            exec("./worlds/lga/tutorials/tutorialDefinitions.cs");
        }
        else
        {
            if (%cityName $= "nv")
            {
                exec("./worlds/lounge/tutorials/tutorialDefinitions.cs");
            }
            else
            {
                if (%cityName $= "minimal")
                {
                    exec("./worlds/minimal/tutorials/tutorialDefinitions.cs");
                }
                else
                {
                    error("Unknown cityname " @ %cityName @ " for tutorials!");
                }
            }
        }
    }
}
