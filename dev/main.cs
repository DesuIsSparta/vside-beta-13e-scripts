$EvalString = "";
$ExecScript = "";
$dev_kit_check_Version = 1234;
$dev_kit_auth = 0;
$ETS::devMode = 0;
package dev
{
    function displayHelp()
    {
        Parent::displayHelp();
        print("\nDevelopment options:\n" @ "  -standAlone                Start as standalone client\n" @ "  -mission <mission>         Specify mission file\n" @ "  -compile                   Compiles .dso & lighting (via running & quitting)\n" @ "  -connect <host[:port]>     Connect directly to <host>\n" @ "  -eval <scriptExpression>   Evaluate <scriptExpression> after initialization\n" @ "  -exec <scriptFile>         Execute <script> after initialization\n" @ "  -noDisplay                 Disable display (and sound) for testing\n" @ "  -console                   Open a separate console\n" @ "  -record <file>             Record a journal and save to <file>\n" @ "  -play <file>               Playback journal from <file>\n" @ "  -playAndBreak <file>       Playback journal and issue an int3 at the end\n" @ "  -insecure                  Don\'t check tokens\n" @ "  -noninteractive            Tell things we\'re running non-interactive\n" @ "  -genRegistration           Generate an account registration & quit\n" @ "  -echoFileNames             Echo file names as they\'re accessed(via resmanager)\n" @ "");
        return ;
    }
    function parseArgs()
    {
        Parent::parseArgs();
        echo("--------- Parsing MOD: Dev ---------");
        echo("My version: " SPC $dev_kit_check_Version);
        echo("Binary version: " SPC $dev_check_version);
        if (!($dev_kit_check_Version $= $dev_check_version))
        {
            echo("Bad Dev Kit. You need to swap to the Dev Kit for this version.");
        }
        else
        {
            echo("Dev Kit authorised.");
            $dev_kit_auth = 1;
        }
        $JournalRecordFile = "";
        $JournalPlayFile = "";
        $JournalPlayAndBreakFile = "";
        $JoinGameAddress = "";
        $Insecure = 0;
        $CommandLineMods = "";
        $NonInteractive = 0;
        if (findSwitch("-standAlone", "$StandAlone"))
        {
            log("initialization", "debug", "standalone flag set");
        }
        if (findArg("-mission", "$MissionArg", "Missing <mission file>"))
        {
            log("initialization", "debug", "mission file: " @ $MissionArg);
            if (!(strchr($MissionArg, "\\") $= ""))
            {
                $MissionArg = strreplace($MissionArg, "\\", "/");
            }
        }
        if (findSwitch("-compile", "$Game::Compile"))
        {
            log("initialization", "debug", "compile flag set");
        }
        if (findArg("-connect", "$JoinGameAddress", "Missing <hostname>"))
        {
            log("initialization", "debug", "joining game: " @ $JoinGameAddress);
        }
        if (findArg("-eval", "$EvalString", "Missing <script expression>"))
        {
            log("initialization", "debug", "evaluating expression: " @ $EvalString);
        }
        if (findArg("-exec", "$ExecScript", "Missing <script file>"))
        {
            log("initialization", "debug", "executing script: " @ $ExecScript);
        }
        if (findSwitch("-noDisplay", "$NoDisplay"))
        {
            log("initialization", "debug", "disabling display");
        }
        if (findSwitch("-console", "$Console"))
        {
            log("initialization", "debug", "opening console");
        }
        if (findArg("-record", "$JournalRecordFile", "Missing <journal file>"))
        {
            log("initialization", "debug", "journal record file: " @ $JournalRecordFile);
        }
        if (findArg("-play", "$JournalPlayFile", "Missing <journal file>"))
        {
            log("initialization", "debug", "journal play file: " @ $JournalPlayFile);
        }
        if (findArg("-playAndBreak", "$JournalPlayAndBreakFile", "Missing <journal file>"))
        {
            log("initialization", "debug", "journal play and break file: " @ $JournalPlayAndBreakFile);
        }
        if (findSwitch("-insecure", "$Insecure"))
        {
            log("initialization", "debug", "insecure flag set");
        }
        if (findSwitch("-noninteractive", "$NonInteractive"))
        {
            log("initialization", "debug", "NonInteractive flag set");
        }
        if (findSwitch("-genRegistration", "$GenRegistration"))
        {
            log("initialization", "debug", "GenRegistration flag set");
        }
        if (findSwitch("-echoFileNames", "$EchoFileNames"))
        {
            setEchoFileLoads(1);
        }
        if (hasArg("-mods"))
        {
            if (findArg("-mods", "$CommandLineMods", "no commandline mods"))
            {
                log("initialization", "info", "commandlinemods: " @ $CommandLineMods);
                loadMods($CommandLineMods);
            }
        }
        if ($NoDisplay)
        {
            disableDisplay();
            if ($dev_kit_auth)
            {
                enableWinConsole(1);
            }
        }
        else
        {
            if ($Console && $dev_kit_auth)
            {
                enableWinConsole(1);
            }
        }
        if ($Game::Compile)
        {
            $Server::Dedicated = 1;
            return ;
        }
        if ($GenRegistration)
        {
            $Server::Dedicated = 1;
            return ;
        }
        if (!($JournalRecordFile $= ""))
        {
            saveJournal($JournalRecordFile);
            log("initialization", "info", "saving event log to journal: " @ $JournalRecordFile);
        }
        else
        {
            if (!($JournalPlayFile $= ""))
            {
                playJournal($JournalPlayFile, 0);
                log("initialization", "info", "playing event log from journal: " @ $JournalPlayFile);
            }
            else
            {
                if (!($JournalPlayAndBreakFile $= ""))
                {
                    playJournal($JournalPlayAndBreakFile, 1);
                    log("initialization", "info", "playing event log from journal (with breaks): " @ $JournalPlayAndBreakFile);
                }
            }
        }
        return ;
    }
    function onStart()
    {
        $ETS::devMode = 1;
        Parent::onStart();
        if ($dev_kit_auth)
        {
            log("initialization", "info", "--------- Initializing MOD: Dev ---------");
            if ($Game::Compile)
            {
                compileAndQuit();
            }
            else
            {
                if ($GenRegistration)
                {
                    generateRegistrationStart();
                    return ;
                }
                else
                {
                    exec("dev/data/devDefaults.cs");
                    exec("dev/data/devPrefs.cs");
                    exec("./data/initNonReloadable.cs");
                    exec("./data/initReloadable.cs");
                    if (!($EvalString $= ""))
                    {
                        eval($EvalString);
                    }
                    if (!($ExecScript $= ""))
                    {
                        exec("./data/" @ $ExecScript);
                    }
                }
            }
        }
        else
        {
            log("initialization", "info", "--------- NOT Initializing MOD: Dev ---------");
        }
        return ;
    }
    function onExit()
    {
        $ETS::devMode = 0;
        if (!$Server::Dedicated && !$NonInteractive)
        {
            echo("Exporting Dev Prefs");
            export("$DevPref::*", "dev/data/devPrefs.cs", 0);
        }
        Parent::onExit();
        return ;
    }
    function compileAndQuit()
    {
        log("initialization", "info", "compiling script files");
        %success = compileScripts("*.cs *.gui *.mis");
        if (%success)
        {
            log("initialization", "info", "all script files compiled successfully");
            quit();
        }
        else
        {
            log("initialization", "info", "there were compile errors, exiting with non-zero status");
            exit(1);
        }
        return ;
    }
    function generateRegistrationStart()
    {
        %request = safeNewScriptObject("ManagerRequest", "", 0);
        %request.bindClassName("UniformManagerRequest");
        %request.setName("request_GenerateUserRegistration");
        $Net::AdminServiceURL = "http://" @ $Net::ManagerHost @ "/envmanager/admin";
        %url = "";
        %url = %url @ $Net::AdminServiceURL;
        %url = %url @ "/RegisterUser";
        %request.setURL(%url);
        %request.addUrlParam("userName", "user" @ getRandom(0, 1000000));
        %request.addUrlParam("password", "password");
        %request.addUrlParam("gender", "f");
        %request.addUrlParam("outfitAndBodySKUs", "5163 5200 5303 5400 5526 5714 5803 5850 5900 5950 5980 15917 21519");
        %request.callbackHandler = "onDoneOrErrorCallback_generateRegistration";
        %request.start();
        return ;
    }
    function onDoneOrErrorCallback_generateRegistration(%request)
    {
        log("network", "debug", getScopeName() SPC "- url =" SPC %request.getURL());
        if (!%request.checkSuccess())
        {
            exit(1);
        }
        %registrationID = %request.getValue("registrationID");
        if (%registrationID $= "")
        {
            error(getScopeName() SPC "- no registration ID!");
            exit(2);
        }
        %f = new FileObject();
        if (%f.openForAppend("platform/client/default_owner.cs"))
        {
            %f.writeLine("// generated by the -genRegistration command line option:");
            %f.writeLine("$Net::registrationID = \"" @ %registrationID @ "\";");
        }
        %f.close();
        quit();
        return ;
    }
    function compileScripts(%extensions)
    {
        log("initialization", "info", "compiling \"" SPC %extensions SPC "\"...");
        %tryCount = 0;
        %sucCount = 0;
        %n = 0;
        while (%n < getWordCount(%extensions))
        {
            %ext = getWord(%extensions, %n);
            %file = findFirstFile(%ext);
            while (!(%file $= ""))
            {
                %suc = compile(%file);
                %tryCount = %tryCount + 1;
                %sucCount = %sucCount + %suc;
                if (!%suc)
                {
                    %fails[%tryCount - %sucCount] = %file ;
                }
                %file = findNextFile(%ext);
            }
            %n = %n + 1;
        }
        log("initialization", "info", "compiled" SPC %sucCount SPC "out of" SPC %tryCount SPC "files");
        %n = 1;
        while (%n <= (%tryCount - %sucCount))
        {
            error("initialization", "compile failed:" SPC %fails[%n]);
            %n = %n + 1;
        }
        if (%tryCount == %sucCount)
        {
            return 1;
        }
        return 0;
    }
    function GameConnection::etsInit(%this)
    {
        Parent::etsInit(%this);
        return ;
    }
    function completeTest()
    {
        error("test completed successfully");
        return ;
    }
    function initCanvas(%windowName)
    {
        if (!Parent::initCanvas(%windowName))
        {
            return 0;
        }
        exec("./data/ui/ConsoleDlg.gui");
        return 1;
    }
};

activatePackage(dev);

