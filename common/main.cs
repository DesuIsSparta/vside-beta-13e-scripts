$gEvalAfterEtsInit = "";
$Pref::Net::BindAddress = "";
function onStart()
{
    return ;
}
function onExit()
{
    return ;
}
function hasArg(%argToFind)
{
    $_Arg::Foo = "";
    return findCommandLineOption(%argToFind, "$_Arg::Foo", "", 1);
}
function findArg(%argToFind, %valToSet, %errorMsg)
{
    return findCommandLineOption(%argToFind, %valToSet, %errorMsg, 0);
}
function findSwitch(%argToFind, %valToSet)
{
    return findCommandLineOption(%argToFind, %valToSet, "", 1);
}
function findCommandLineOption(%argToFind, %valToSet, %errorMsg, %isSwitch)
{
    log("initialization", "debug", "find arg: looking for " @ %argToFind);
    %found = 0;
    %i = 1;
    while (%i < $Game::argc)
    {
        %arg = $Game::argv[%i];
        %nextArg = "";
        if (%i < ($Game::argc - 1))
        {
            %nextArg = $Game::argv[%i + 1];
        }
        %hasNextArg = ($Game::argc - %i) > 1;
        if (%arg $= %argToFind)
        {
            $Game::ArgUsed[%i] = $Game::ArgUsed[%i] + 1;
            if (!(%valToSet $= ""))
            {
                if (%isSwitch)
                {
                    eval(%valToSet @ "=true;");
                    log("initialization", "debug", "setting switch " @ %valToSet);
                    %found = 1;
                }
                else
                {
                    if (%hasNextArg)
                    {
                        %evalString = strreplace(%nextArg, "\\", "\\\\");
                        %evalString = strreplace(%evalString, "\"", "\\\"");
                        %evalString = "\"" @ %evalString @ "\"";
                        %evalString = %valToSet @ "=" @ %evalString @ ";";
                        %evalString = strreplace(%evalString, ";;", ";");
                        log("initialization", "debug", "evalString: " @ %evalString);
                        eval(%evalString);
                        log("initialization", "debug", "setting value " @ %valToSet);
                        $Game::ArgUsed[%i + 1] = $Game::ArgUsed[%i + 1] + 1;
                        %found = 1;
                    }
                    else
                    {
                        %found = 0;
                        error("initialization", "Error: " @ %errorMsg);
                    }
                }
            }
            else
            {
                %found = 1;
            }
            break;
        }
        %i = %i + 1;
    }
    return %found;
}
function parseMainArgs()
{
    findSwitch("-help", "$DisplayHelp");
    return ;
}
function parseArgs()
{
    return ;
}
function getAllArgs()
{
    %ret = "";
    %n = 1;
    while (%n < $Game::argc)
    {
        %sep = %n == 1 ? "" : " ";
        %ret = %ret @ %sep @ $Game::argv[%n];
        %n = %n + 1;
    }
    return %ret;
}
$gKnownUnusedArgsLogLevel["-debug"] = "info";
function checkUnusedArgs()
{
    %i = 1;
    while (%i < $Game::argc)
    {
        if (!$Game::ArgUsed[%i])
        {
            %arg = $Game::argv[%i];
            %level = $gKnownUnusedArgsLogLevel[%arg];
            if (%level $= "")
            {
                %level = "error";
            }
            log("initialization", %level, "unknown (or possibly duplicated) command line argument: " @ %arg);
        }
        %i = %i + 1;
    }
}

function doStart()
{
    log("initialization", "info", "--------- Args ---------");
    log("initialization", "info", $Game::argv[0] SPC getAllArgs());
    log("initialization", "info", "--------- Parsing Arg MOD: Main ---------");
    parseMainArgs();
    log("initialization", "info", "--------- Parsing Arguments ---------");
    parseArgs();
    checkUnusedArgs();
    if ($DisplayHelp)
    {
        enableWinConsole(1);
        displayHelp();
        quit();
    }
    else
    {
        onStart();
        log("initialization", "info", "Engine initialized...");
        $Platform::CanSleepInBackground = 1;
    }
    checkUnusedArgs();
    return ;
}
package Help
{
    function onExit()
    {
        return ;
    }
};

function displayHelp()
{
    activatePackage(Help);
    print("\nGeneral options:\n" @ "  -logMode <0|disable|1|append|2|overwrite>\n" @ "                             Set the logging mode\n" @ "  -logLevel <0|none|1|error|2|warn|3|info|4|debug>\n" @ "                             Sets the debug level\n" @ "  -help                      Display this help message\n" @ "  -version                   Display version information and exit\n");
    return ;
}
function loadMods(%modPath)
{
    %modPath = NextToken(%modPath, token, ";");
    if (!(%modPath $= ""))
    {
        loadMods(%modPath);
    }
    log("initialization", "info", "--------- Loading MOD: " @ %token @ "---------");
    exec(%token @ "/main.cs");
    return ;
}
function dumpMods(%modPath)
{
    %modPath = NextToken(%modPath, token, ";");
    if (!(%modPath $= ""))
    {
        dumpMods(%modPath);
    }
    log("initialization", "info", %token @ "/main.cs");
    return ;
}
function doreloadModScripts(%modPath)
{
    %modPath = NextToken(%modPath, token, ";");
    if (!(%modPath $= ""))
    {
        doreloadModScripts(%modPath);
    }
    %fileName = %token @ "/initReloadable.cs";
    if (isFile(%fileName))
    {
        exec(%fileName);
    }
    return ;
}
function reloadModScripts(%modPath)
{
    if (%modPath $= "")
    {
        %modPath = $modPath;
    }
    doreloadModScripts(%modPath);
    return ;
}
$baseMods = "common";
$userMods = "platform";
$projectsMod = "projects";
$dynamicContentMod = "dc";
$DisplayHelp = 0;
$displayVersion = 0;
$player = 0;
$ServerGroup = 0;
$instantGroup = "Unknown";
$enableDirectInput = 0;
$DisableSystemProfiling = 1;
$Conv::updateLocationsTimeID = 0;
$SystemMetric::ObjectCounts = 0;
$System::LoginLog = 0;
$System::dumpMetricsTimerID = 0;
$pi = 3.14159;
$BoundPort = 0;
$Sim::Time = 0;
$Net::ManagerHost = 0;
$Net::SecureManagerHost = 0;
$Net::IRCHost = "";
$Game::Running = 0;
$NoDisplay = 0;
$Console = 0;
$Game::Compile = 0;
$StandAlone = 0;
$AmServer = 0;
$AmClient = 0;
$WindowManager::Initialized = 0;
$GenRegistration = 0;
$MissionArg = "";
$Con::WindowTitle = "Environment Console";
$GuiAudioType = 1;
$SimAudioType = 2;
$MessageAudioType = 3;
$Server::Dedicated = 0;
$Server::Location = "0.5 0.5";
$Server::NPCChatEnabled = 0;
$Server::WebLogLevel = 2;
$VURL = "";
function initCommon()
{
    setRandomSeed();
    exec("./client/canvas.cs");
    return ;
}
function initBaseClient()
{
    exec("./client/message.cs");
    exec("./client/mission.cs");
    exec("./client/missionDownload.cs");
    exec("./client/actionMap.cs");
    exec("./client/scriptDoc.cs");
    exec("./client/ets/consoleCommands.cs");
    return ;
}
function initBaseServer()
{
    exec("./server/server.cs");
    exec("./server/message.cs");
    exec("./server/commands.cs");
    exec("./server/missionInfo.cs");
    exec("./server/missionLoad.cs");
    exec("./server/missionDownload.cs");
    exec("./server/clientConnection.cs");
    exec("./server/kickban.cs");
    return ;
}
package common
{
    function onStart()
    {
        Parent::onStart();
        echo("--------- Initializing MOD: Common ---------");
        initCommon();
        return ;
    }
    function onExit()
    {
        Parent::onExit();
        return ;
    }
};

activatePackage(common);
function findRequestStatus(%managerRequest)
{
    if (%managerRequest.hasKey("status"))
    {
        return %managerRequest.getValue("status");
    }
    %stati = "invalid fail serverfail inactive alreadyloggedin banned suspended upgrade_required upgrade_available success overloaded sendstart boot";
    %count = getWordCount(%stati);
    %i = 0;
    while (%i <= %count)
    {
        %astatus = getWord(%stati, %i);
        if (%managerRequest.hasKey(%astatus))
        {
            return %astatus;
        }
        %i = %i + 1;
    }
    return "";
}
