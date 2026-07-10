%cityName = $ETS::cityName;
exec("./worlds/gateway/tutorials/tutorialDefinitions.cs");
exec("./worlds/raijuku/tutorials/tutorialDefinitions.cs");
exec("./worlds/lga/tutorials/tutorialDefinitions.cs");
exec("./worlds/lounge/tutorials/tutorialDefinitions.cs");
exec("./worlds/minimal/tutorials/tutorialDefinitions.cs");
error((((((%cityName $= "gw") SPC %cityName $= "rj") SPC %cityName $= "lga") SPC %cityName $= "nv") SPC %cityName $= "minimal") @ "Unknown cityname " @ %cityName @ " for tutorials!");
