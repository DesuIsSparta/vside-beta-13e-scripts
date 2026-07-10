$EvalString = "";
$ExecScript = "";
$ETS::devMode = 0;
package dev {
    function displayHelp() {
        Parent::displayHelp();
        print("\nDevelopment options:\n" @ "  -standAlone                Start as standalone client\n" @ "  -mission <mission>         Specify mission file\n" @ "  -compile                   Compiles .dso & lighting (via running & quitting)\n" @ "  -connect <host[:port]>     Connect directly to <host>\n" @ "  -eval <scriptExpression>   Evaluate <scriptExpression> after initialization\n" @ "  -exec <scriptFile>         Execute <script> after initialization\n" @ "  -noDisplay                 Disable display (and sound) for testing\n" @ "  -console                   Open a separate console\n" @ "  -record <file>             Record a journal and save to <file>\n" @ "  -play <file>               Playback journal from <file>\n" @ "  -playAndBreak <file>       Playback journal and issue an int3 at the end\n" @ "  -insecure                  Don't check tokens\n" @ "  -noninteractive            Tell things we're running non-interactive\n" @ "  -genRegistration           Generate an account registration & quit\n" @ "  -echoFileNames             Echo file names as they're accessed(via resmanager)\n" @ "");
    };
    function parseArgs() {
        Parent::parseArgs();
        $JournalRecordFile = "";
        $JournalPlayFile = "";
        $JournalPlayAndBreakFile = "";
        $JoinGameAddress = "";
        $Insecure = 0;
        $CommandLineMods = "";
        $NonInteractive = 0;
        echo("--------- Parsing MOD: Dev ---------");
        if (findSwitch("-standAlone", "$StandAlone")) {
            log("initialization", "debug", "standalone flag set");
        }
        if (findArg("-mission", "$MissionArg", "Missing <mission file>")) {
            log("initialization", "debug", "mission file: " @ $MissionArg);
            if (!(strchr($MissionArg, "\\") $= "")) {
                $MissionArg = strreplace($MissionArg, "\\", "/");
            }
        }
        if (findSwitch("-compile", "$Game::Compile")) {
            log("initialization", "debug", "compile flag set");
        }
        if (findArg("-connect", "$JoinGameAddress", "Missing <hostname>")) {
            log("initialization", "debug", "joining game: " @ $JoinGameAddress);
        }
        if (findArg("-eval", "$EvalString", "Missing <script expression>")) {
            log("initialization", "debug", "evaluating expression: " @ $EvalString);
        }
        if (findArg("-exec", "$ExecScript", "Missing <script file>")) {
            log("initialization", "debug", "executing script: " @ $ExecScript);
        }
        if (findSwitch("-noDisplay", "$NoDisplay")) {
            log("initialization", "debug", "disabling display");
        }
        if (findSwitch("-console", "$Console")) {
            log("initialization", "debug", "opening console");
        }
        if (findArg("-record", "$JournalRecordFile", "Missing <journal file>")) {
            log("initialization", "debug", "journal record file: " @ $JournalRecordFile);
        }
        if (findArg("-play", "$JournalPlayFile", "Missing <journal file>")) {
            log("initialization", "debug", "journal play file: " @ $JournalPlayFile);
        }
        if (findArg("-playAndBreak", "$JournalPlayAndBreakFile", "Missing <journal file>")) {
            log("initialization", "debug", "journal play and break file: " @ $JournalPlayAndBreakFile);
        }
        if (findSwitch("-insecure", "$Insecure")) {
            log("initialization", "debug", "insecure flag set");
        }
        if (findSwitch("-noninteractive", "$NonInteractive")) {
            log("initialization", "debug", "NonInteractive flag set");
        }
        if (findSwitch("-genRegistration", "$GenRegistration")) {
            log("initialization", "debug", "GenRegistration flag set");
        }
        if (findSwitch("-echoFileNames", "$EchoFileNames")) {
            setEchoFileLoads(1);
        }
        if (hasArg("-mods") && findArg("-mods", "$CommandLineMods", "no commandline mods")) {
            log("initialization", "info", "commandlinemods: " @ $CommandLineMods);
            setModPaths(getModPaths() @ ";" @ $CommandLineMods);
            loadMods($CommandLineMods);
        }
        if ($NoDisplay) {
            disableDisplay();
            enableWinConsole(1);
        }
        if ($Console) {
            enableWinConsole(1);
        }
        if ($Game::Compile) {
            $Server::Dedicated = 1;
            return;
        }
        if ($GenRegistration) {
            $Server::Dedicated = 1;
            return;
        }
        if (!($JournalRecordFile $= "")) {
            saveJournal($JournalRecordFile);
            log("initialization", "info", "saving event log to journal: " @ $JournalRecordFile);
        }
        if (!($JournalPlayFile $= "")) {
            playJournal($JournalPlayFile, 0);
            log("initialization", "info", "playing event log from journal: " @ $JournalPlayFile);
        }
        if (!($JournalPlayAndBreakFile $= "")) {
            playJournal($JournalPlayAndBreakFile, 1);
            log("initialization", "info", "playing event log from journal (with breaks): " @ $JournalPlayAndBreakFile);
        }
    };
    function onStart() {
        $ETS::devMode = 1;
        Parent::onStart();
        log("initialization", "info", "--------- Initializing MOD: Dev ---------");
        if ($Game::Compile) {
            compileAndQuit();
        }
        if ($GenRegistration) {
            generateRegistrationStart();
            return;
        }
        exec("dev/devDefaults.cs");
        exec("dev/devPrefs.cs");
        exec("./initNonReloadable.cs");
        exec("./initReloadable.cs");
        if (!($EvalString $= "")) {
            eval($EvalString);
        }
        if (!($ExecScript $= "")) {
            exec("./" @ $ExecScript);
        }
    };
    function onExit() {
        $ETS::devMode = 0;
        if (!($Server::Dedicated)) {
        }
        if (!($NonInteractive)) {
            echo("Exporting Dev Prefs");
            export("$DevPref::*", "dev/devPrefs.cs", 0);
        }
        Parent::onExit();
    };
    function compileAndQuit() {
        log("initialization", "info", "compiling script files");
        %success = compileScripts("*.cs *.gui *.mis");
        if (%success) {
            log("initialization", "info", "all script files compiled successfully");
            quit();
        }
        log("initialization", "info", "there were compile errors, exiting with non-zero status");
        exit(1);
    };
    function generateRegistrationStart() {
        %request = safeNewScriptObject("ManagerRequest", "", 0);
        "UniformManagerRequest".bindClassName(%request);
        "request_GenerateUserRegistration".setName(%request);
        $Net::AdminServiceURL = "http://" @ $Net::ManagerHost @ "/envmanager/admin";
        %url = "";
        %url = %url @ $Net::AdminServiceURL;
        %url = %url @ "/RegisterUser";
        %url.setURL(%request);
        "user" @ getRandom(0, 1000000).addUrlParam(%request, "userName");
        "password".addUrlParam(%request, "password");
        "f".addUrlParam(%request, "gender");
        "5163 5200 5303 5400 5526 5714 5803 5850 5900 5950 5980 15917 21519".addUrlParam(%request, "outfitAndBodySKUs");
        %request.callbackHandler = "onDoneOrErrorCallback_generateRegistration";
        %request.start();
    };
    function onDoneOrErrorCallback_generateRegistration(%request) {
        log("network", "debug", getScopeName() @ " " @ "- url =" @ " " @ %request.getURL());
        if (!(%request.checkSuccess())) {
            exit(1);
        }
        %registrationID = "registrationID".getValue(%request);
        if ((%registrationID $= "")) {
            error(getScopeName() @ " " @ "- no registration ID!");
            exit(2);
        }
        %f = new FileObject("");;
        0;
        if ("platform/client/default_owner.cs".openForAppend(%f)) {
            "// generated by the -genRegistration command line option:".writeLine(%f);
            "$Net::registrationID = \"" @ %registrationID @ "\";".writeLine(%f);
        }
        %f.close();
        quit();
    };
    function compileScripts(%extensions) {
        log("initialization", "info", "compiling \"" @ " " @ %extensions @ " " @ "\"...");
        %tryCount = 0;
        %sucCount = 0;
        %n = 0;
        while ((%n < getWordCount(%extensions))) {
            %ext = getWord(%extensions, %n);
            %file = findFirstFile(%ext);
            while (!(%file $= "")) {
                %suc = compile(%file);
                %tryCount = (%tryCount + 1.0);
                %sucCount = (%sucCount + %suc);
                if (!(%suc)) {
                    %tryCount[%fails @ (%tryCount - %sucCount)] = %file;
                }
                %file = findNextFile(%ext);
            }
            %n = (%n + 1.0);
            !(%file $= "");
        }
        log("initialization", "info", "compiled" @ " " @ %sucCount @ " " @ "out of" @ " " @ %tryCount @ " " @ "files");
        %n = 1;
        (%n < getWordCount(%extensions));
        while ((%n <= (%tryCount - %sucCount))) {
            error("initialization", "compile failed:" @ " " @ %n[%fails @ %n]);
            %n = (%n + 1.0);
        }
        if ((%tryCount == %sucCount)) {
            return 1;
        }
        return 0;
    };
    function GameConnection::etsInit(%this) {
        Parent::etsInit(%this);
    };
    function completeTest() {
        error("test completed successfully");
    };
    function initCanvas(%windowName) {
        if (!(Parent::initCanvas(%windowName))) {
            return 0;
        }
        exec("./ui/ConsoleDlg.gui");
        return 1;
    };
    activatePackage(dev);
};

