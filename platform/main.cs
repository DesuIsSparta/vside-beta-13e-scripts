exec("./common/scripts/defaultsCommon.cs");
exec("./client/defaults.cs");
exec("./server/defaults.cs", 0);
exec("./client/userprefs.cs", 0);
$UserPref::Player::Password = unmunge($UserPref::Player::Password);
$UserPref::Player::AIMPassword = unmunge($UserPref::Player::AIMPassword);
package platform
{
    function displayHelp()
    {
        Parent::displayHelp();
        if ($Server::Dedicated)
        {
            displayServerHelp();
        }
        else
        {
            displayClientHelp();
        }
        return ;
    }
    function displayClientHelp()
    {
        print("\nClient options:\n" @ "  -manager <host[:port]>     Specify login server\n" @ "  -mainsite <url>            Specify main website\n" @ "  -download <host[:port]>    Specify download host\n" @ "  -large                     Large display\n" @ "  -small                     Small display\n" @ "  -noSound                   Disable sound\n" @ "  -display <OpenGL|D3D|Auto> Specify device or auto-detect\n" @ "  -server                    Start as server\n" @ "  -cache                     Do network caching\n" @ "  -staging                   Run in staging environment\n" @ "  -stagingrc                 Run in stagingRC environment\n" @ "  -alpha                     Run in alpha environment\n" @ "  -preload                   Run asset preloading\n" @ "  -url                       Specify a place to spawn to via vURL\n" @ "  -alphabuffer               Request an alpha buffer for OpenGL\n" @ "  -notexdelay                Disable teuxture delay load\n" @ "  -enableRawTextures         Enable use of compressed textures\n" @ "  -automated                 Automatically begin tasks like thumbnail generationg\n" @ "");
        return ;
    }
    function displayServerHelp()
    {
        print("\nServer options:\n" @ "  -manager <host[:port]>     Specify host and port of login server\n" @ "  -mission <mission>         Specify mission file\n" @ "  -serverName                Server name\n" @ "  -nameSpaceTag              a namespace tag for private spaces and buildings default \"\"\n" @ "  -serverAddress <address>   Server bind address\n" @ "  -serverPort                Server bind port\n" @ "  -mapLocation <x,y>         Server worldmap coordinates\n" @ "  -disableChat               NPCs don\'t chat\n" @ "  -webloglevel <n>           Set web log level\n" @ "  -notmappable               Server will not appear on the map\n" @ "");
        return ;
    }
    function rebaseURLs()
    {
        $Net::CrashURL = absoluteURL($Net::ManagerHost, "envmanager/envclient/CrashReport");
        $Net::ItunesURL = absoluteURL($Net::BaseDomain, "go/itunes");
        $Net::ProfileURL = absoluteURL($Net::BaseDomain, "go/profile/user/");
        $Net::ProfilesURL = absoluteURL($Net::BaseDomain, "go/profiles");
        $Net::ProfileEditURL = absoluteURL($Net::BaseDomain, "go/profile");
        $Net::AccountEditURL = absoluteURL($Net::BaseDomain, "go/account");
        $Net::AddFundsURL = absoluteURL($Net::BaseDomain, "go/account/purchase");
        $Net::AIMInviteURL = absoluteURL($Net::BaseDomain, "?");
        $Net::AvatarURL = absoluteURL($Net::BaseDomain, "photoservice/avatars/");
        $Net::BuildDirPhotoURL = absoluteURL($Net::BaseDomain, "photoservice/directory/");
        $Net::GalleryPhotoURL = absoluteURL($Net::BaseDomain, "photoservice/");
        $Net::EventDetailURL = absoluteURL($Net::BaseDomain, "go/event/id/");
        $Net::MusicURL = absoluteURL($Net::BaseDomain, "go/music");
        $Net::ViewTagURL = absoluteURL($Net::BaseDomain, "go/search/");
        $Net::HelpURL_Abuse = absoluteURL($Net::BaseDomain, "go/help/category/abuse");
        $Net::HelpURL_General = absoluteURL($Net::BaseDomain, "go/help");
        $Net::HelpURL_Guidelines = absoluteURL($Net::BaseDomain, "go/rules");
        $Net::HelpURL_MusicNEvents = absoluteURL($Net::BaseDomain, "go/help/category/musicevents");
        $Net::HelpURL_Navigation = absoluteURL($Net::BaseDomain, "go/help/category/navigation");
        $Net::HelpURL_Parents = absoluteURL($Net::BaseDomain, "go/help/category/parents");
        $Net::HelpURL_Support = absoluteURL($Net::BaseDomain, "go/help/category/support");
        $Net::HelpURL_VPoints = absoluteURL($Net::BaseDomain, "go/help/category/vpoints");
        $Net::HelpURL_VBux = absoluteURL($Net::BaseDomain, "go/help/category/vbux");
        $Net::HelpURL_VHD = absoluteURL($Net::BaseDomain, "go/help/category/vhd");
        $Net::HelpURL_MyShop = absoluteURL($Net::BaseDomain, "go/help/category/vhd");
        $Net::HelpURL_displayDevice = absoluteURL($Net::BaseDomain, "go/help/displayDevice");
        $Net::ForgotPassURL = absoluteURL($Net::BaseDomain, "go/login/forgot");
        $Net::ActivationURL = $Net::AccountEditURL;
        $Net::ReregisterURL = absoluteURL($Net::BaseDomain, "go/login/reregister");
        $Net::FinishRegistrationURL = absoluteURL($Net::BaseDomain, "app/start/registrationId/[REGISTRATIONID]");
        $Net::UploadPhotoURL = absoluteURL($Net::ManagerHost, "envmanager/envclient/uploadPhoto");
        $Net::PhotoAlbumURL = absoluteURL($Net::BaseDomain, "go/photos/user/[PLAYERNAME_URL]");
        $Net::PhotoPageURL = absoluteURL($Net::BaseDomain, "app/photo/id/");
        $Net::TermsOfUseURL = absoluteURL($Net::BaseDomain, "go/help/tou");
        $Net::EventsURL = absoluteURL($Net::BaseDomain, "go/events");
        $Net::SongPageURL = absoluteURL($Net::BaseDomain, "go/music");
        $Net::DynamicContentURL = absoluteURL($Net::BaseDomain, "dc");
        $Net::PlayerReportURL = absoluteURL($Net::ManagerHost, "envmanager/envclient/AbuseReport");
        $Net::ManageUserURL = absoluteURL($Net::ManagerHost, "envmanager/manage_user");
        $Net::inviteFriendsURL = absoluteURL($Net::BaseDomain, "app/invite");
        $Net::DashboardURL = absoluteURL($Net::BaseDomain, "go/dashboard");
        $Net::PartnerURL = absoluteURL($Net::BaseDomain, "go/partner");
        $Net::ForumsURL = absoluteURL($Net::BaseDomain, "go/forums");
        $Net::ImageInfoURL = absoluteURL($Net::BaseDomain, "go/imageinfo");
        $Net::downloadURL = "http://" @ $Net::DownloadHost;
        if ($Server::Dedicated)
        {
            $Net::BaseURL = "http://" @ $Net::ManagerHost @ "/envmanager/status";
            $Net::LoginURL = "http://" @ $Net::ManagerHost @ "/envmanager/login";
            $Net::ClientServiceURL = "http://" @ $Net::ManagerHost @ "/envmanager/envclient";
            $Net::ServerServiceURL = "http://" @ $Net::ManagerHost @ "/envmanager/envserver";
        }
        else
        {
            $Net::BaseURL = "http://" @ $Net::ManagerHost @ "/envmanager/login";
            $Net::SecureURL = "https://" @ $Net::SecureManagerHost @ "/envmanager/login";
            $Net::LoginURL = $Net::BaseURL;
            $Net::ClientServiceURL = "http://" @ $Net::ManagerHost @ "/envmanager/envclient";
            $Net::SecureClientServiceURL = "https://" @ $Net::SecureManagerHost @ "/envmanager/envclient";
            $Net::ServerServiceURL = "http://" @ $Net::ManagerHost @ "/envmanager/envserver";
        }
        setupProjectSpecificUrls();
        return ;
    }
    function notokenURL(%url)
    {
        return strreplace(%url, "http", "HTTP");
    }
    function clientRebaseHosts()
    {
        $Net::ManagerHost = "envmanager." @ $Net::BaseDomain @ ":8080";
        $Net::SecureManagerHost = "envmanager." @ $Net::BaseDomain @ ":8443";
        $Net::DownloadHost = "download." @ $Net::BaseDomain;
        return ;
    }
    function serverRebaseHosts()
    {
        $Net::ManagerHost = $Net::BaseDomain @ ":8081";
        $Net::IRCHost = "irc." @ $Net::BaseDomain @ ":6667";
        return ;
    }
    function isValidHostAddress(%address)
    {
        %ret = 1;
        if (%address $= "")
        {
            %ret = 0;
        }
        else
        {
            if (%address $= 0)
            {
                %ret = 0;
            }
            else
            {
                if (%address $= "0:0")
                {
                    %ret = 0;
                }
            }
        }
        return %ret;
    }
    function haveValidManagerHost()
    {
        %ret = isValidHostAddress($Net::ManagerHost);
        if (!%ret && !$StandAlone)
        {
            warn(getScopeName(1) SPC "- $Net::ManagerHost is invalid." SPC getTrace());
        }
        return %ret;
    }
    function haveValidToken()
    {
        return !($Token $= "");
    }
    function parseArgs()
    {
        Parent::parseArgs();
        echo("--------- Parsing Arg MOD: platform ---------");
        if ((hasArg("-dedicated") || hasArg("-server")) && !$Game::Compile)
        {
            $Server::Dedicated = 1;
            $Con::logBufferEnabled = 0;
            $Net::ManagerHost = "192.168.100.100:8081";
            $Net::DownloadHost = "192.168.100.100:8081";
        }
        if ($Server::Dedicated)
        {
            parseServerArgs();
        }
        else
        {
            parseClientArgs();
        }
        if ((hasArg("-dedicated") || hasArg("-server")) && !$Game::Compile)
        {
            enableWinConsole(1);
        }
        rebaseURLs();
        setupMessages();
        if (hasArg("-notmappable"))
        {
            $Server::Mappable = 0;
            log("initialization", "debug", "setting the map as invisible");
        }
        $NonInteractive = 0;
        if (!$NonInteractive)
        {
            log("initialization", "debug", "Exporting net_settings.log");
            export("$Net::*", "./net_settings.log", 0);
        }
        return ;
    }
    function parseMainsiteArg()
    {
        %haveMainsiteArg = findArg("-mainsite", "$Net::BaseDomain", "Missing mainsite <url>");
        if (%haveMainsiteArg)
        {
            if ($Server::Dedicated)
            {
                serverRebaseHosts();
            }
            else
            {
                clientRebaseHosts();
                %testdomain = strreplace($Net::BaseDomain, ":", " ");
                if (stricmp("www.vside.com", firstWord(%testdomain)))
                {
                    %analytic = getAnalytic();
                    %analytic.setDomainAndAccount("test.vside.com", "UA-324914-24");
                }
            }
        }
        return ;
    }
    function parseManagerArgs()
    {
        %haveManagerArg = findArg("-manager", "$Net::ManagerHost", "Missing manager <host[:port]>");
        %haveSManagerArg = findArg("-smanager", "$Net::SecureManagerHost", "Missing smanager <host[:port]>");
        if (%haveManagerArg)
        {
            %colonPos = strstr($Net::ManagerHost, ":");
            if (%colonPos == -1)
            {
                if (!%haveSManagerArg)
                {
                    $Net::SecureManagerHost = $Net::ManagerHost @ ":8443";
                }
                if ($Server::Dedicated)
                {
                    $Net::ManagerHost = $Net::ManagerHost @ ":8081";
                }
                else
                {
                    $Net::ManagerHost = $Net::ManagerHost @ ":8080";
                }
            }
            else
            {
                if (!%haveSManagerArg)
                {
                    %line = $Net::ManagerHost;
                    %line = NextToken(%line, host, ":");
                    NextToken(%line, port, " ");
                    if (%port $= 80)
                    {
                        $Net::SecureManagerHost = %host @ ":443";
                    }
                    else
                    {
                        $Net::SecureManagerHost = %host @ ":8443";
                    }
                }
            }
        }
        if (%haveSManagerArg)
        {
            %colonPos = strstr($Net::SecureManagerHost, ":");
            if (%colonPos == -1)
            {
                $Net::SecureManagerHost = $NetSecureManagerHost @ ":8443";
            }
        }
        log("initialization", "debug", "manager host: " @ $Net::ManagerHost);
        log("initialization", "debug", "secure manager host: " @ $Net::SecureManagerHost);
        return ;
    }
    function parseDownloadArg()
    {
        findArg("-download", "$Net::DownloadHost", "Missing download <host[:port]>");
        return ;
    }
    function parseIRCArg()
    {
        findArg("-irc", "$Net::IRCHost", "Missing irc <host[:port]>");
        return ;
    }
    function parseURLArg()
    {
        findArg("-url", "$VURLcmd", "No url specified");
        return ;
    }
    function parseClientArgs()
    {
        parseMainsiteArg();
        parseManagerArgs();
        startInitialSSLConnection();
        parseDownloadArg();
        parseURLArg();
        $CacheFlagIsSet = 0;
        if (findSwitch("-cache", "$CacheFlagIsSet"))
        {
            log("initialization", "debug", "cache flag set");
        }
        $Preload = 0;
        if (findSwitch("-preload", "$Preload"))
        {
            log("initialization", "debug", "preload flag set");
        }
        if (findArg("-extraCacheNameTag", "$Cache::ExtraNameTag", "Missing <extraCacheNameTag>"))
        {
            log("initialization", "debug", "cache extra tag: " @ $Cache::ExtraNameTag);
        }
        $AutoDownloadPackages = 0;
        if (findSwitch("-usePackages", "$AutoDownloadPackages"))
        {
            log("initialization", "debug", "usePackages specified. Start downloading ASAP.");
        }
        if (hasArg("-large"))
        {
            $UserPref::Video::Resolution = "960 544 32";
        }
        else
        {
            if (hasArg("-small"))
            {
                $UserPref::Video::Resolution = "480 272 32";
            }
        }
        if (hasArg("-noSound"))
        {
            error("initialization", "no support yet");
        }
        if (findArg("-display", "$Pref::Video::DisplayDevice", "Missing <display device>"))
        {
            if (($Pref::Video::DisplayDevice $= "D3D") && ($Pref::Video::DisplayDevice $= "OpenGL"))
            {
            }
            else
            {
                if ($Pref::Video::DisplayDevice $= "Auto")
                {
                    $Pref::Video::DisplayDevice = "";
                }
                else
                {
                    error("initialization", "Error: " @ $Pref::Video::DisplayDevice @ " not one of OpenGL|D3D|Auto");
                }
            }
        }
        if (hasArg("-notexdelay"))
        {
            log("initialization", "debug", "Disabling delay loading of textures");
            $pref::OpenGL::delayLoadTextures = 0;
        }
        if (hasArg("-nobgtexload"))
        {
            log("initialization", "debug", "Disabling background loading of textures");
            $pref::OpenGL::backgroundLoadTextures = 0;
        }
        if (hasArg("-enableRawTextures"))
        {
            log("initialization", "debug", "Enable use of compressed textures");
            $pref::OpenGL::enableRawTextures = 1;
        }
        if (hasArg("-automated"))
        {
            $gAutomatedRun = 1;
        }
        $ETS::WindowTitle = generateWindowTitle("");
        return ;
    }
    function generateWindowTitle(%ServerName)
    {
        %ServerNameString = %ServerName $= "" ? "" : " on server";
        %CityNameString = $ETS::cityName $= "" ? "" : " in";
        %LongCityNameString = "";
        if (isObject(WorldMap))
        {
            %areaName = WorldMap.cityNameForServerName(%ServerName);
            %locationName = DestinationList::GetAreaNameUserFacingName(%areaName);
            %LongCityNameString = %locationName $= "" ? "" : " - in";
        }
        else
        {
            echo(getScopeName() SPC "No WorldMap, not getting long city name from server");
        }
        if (hasArg("-staging"))
        {
            %title = $ETS::AppName @ " (Staging Build " @ getBuildVersion() @ %ServerNameString @ ")";
        }
        else
        {
            if (hasArg("-stagingrc"))
            {
                %title = $ETS::AppName @ " (StagingRC Build " @ getBuildVersion() @ %ServerNameString @ ")";
            }
            else
            {
                if (hasArg("-alpha"))
                {
                    %title = $ETS::AppName @ " (Alpha Build " @ getBuildVersion() @ %LongCityNameString @ ")";
                }
                else
                {
                    if (hasArg("-standalone"))
                    {
                        %alphabufferrequested = "";
                        if (hasArg("-alphabuffer"))
                        {
                            %alphabufferrequested = " * Alpha Buffer Requested *";
                        }
                        %title = $ETS::AppName @ " (Standalone Build " @ getBuildVersion() @ %ServerNameString @ %CityNameString @ %alphabufferrequested @ ")";
                    }
                    else
                    {
                        %title = $ETS::AppName @ " - " @ $ETS::AppVersion @ %LongCityNameString;
                    }
                }
            }
        }
        return %title;
    }
    function GetServerNameSpaceTaggedName(%name)
    {
        if (!($pref::Server::NameSpaceTag $= ""))
        {
            error(getScopeName() SPC "- $pref::Server::NameSpaceTag is being used to modify the building and model id names, this should not be released to the public");
            %name = $pref::Server::NameSpaceTag @ %name;
        }
        return %name;
    }
    function parseServerArgs()
    {
        parseMainsiteArg();
        parseManagerArgs();
        parseDownloadArg();
        parseIRCArg();
        $Pref::Net::BindAddress = "";
        if (findArg("-mission", "$MissionArg", "Missing <mission file>"))
        {
            log("initialization", "debug", "mission file: " @ $MissionArg);
            if (!(strchr($MissionArg, "\\") $= ""))
            {
                $MissionArg = strreplace($MissionArg, "\\", "/");
            }
        }
        if (findArg("-nameSpaceTag", "$Pref::Server::NameSpaceTag", "Missing <namespacetag>"))
        {
            log("initialization", "debug", "namespacetag: " @ $pref::Server::NameSpaceTag);
        }
        if (findArg("-serverName", "$Pref::Server::Name", "Missing <server name>"))
        {
            if (!($pref::Server::NameSpaceTag $= ""))
            {
                $Pref::Server::Name = $pref::Server::NameSpaceTag @ $Pref::Server::Name;
            }
            $Con::WindowTitle = $Con::WindowTitle @ " - " @ $Pref::Server::Name;
            log("initialization", "debug", "server name: " @ $Pref::Server::Name);
        }
        if (findArg("-cityName", "$Pref::Server::City", "Missing <city name>"))
        {
            $Con::WindowTitle = $Con::WindowTitle @ " - " @ $Pref::Server::City;
            log("initialization", "debug", "city name: " @ $Pref::Server::City);
        }
        if (findArg("-serverPort", "$Pref::Server::Port", "Missing <server port>"))
        {
            log("initialization", "debug", "server port: " @ $Pref::Server::Port);
        }
        if (findArg("-serverAddress", "$Pref::Net::BindAddress", "Missing <server address>"))
        {
            log("initialization", "debug", "server address: " @ $Pref::Net::BindAddress);
        }
        if (findArg("-mapLocation", "$Server::Location", "Missing <map location>"))
        {
            log("initialization", "debug", "map location: " @ $Server::Location);
        }
        if (findSwitch("-enableChat", "$Server::NPCChatEnabled"))
        {
            log("initialization", "debug", "enabling NPC chat");
        }
        if (findSwitch("-weblogLevel", "$Server::WebLogLevel", "Missing <log level>"))
        {
            log("initialization", "debug", "web log level: " @ $Server::WebLogLevel);
        }
        if (findSwitch("-usePackages", "$Pref::Server::usePackages", "Missing <usePackages>"))
        {
            log("initialization", "debug", "Enabling city package requirements.");
        }
        else
        {
            $Pref::Server::usePackages = 0;
        }
        hasArg("-webConfigFile");
        return ;
    }
    function startInitialSSLConnection()
    {
        %curl = new URLPostObject();
        %curl.setURL("https://" @ $Net::SecureManagerHost);
        %curl.setBody(0);
        %curl.start();
        return ;
    }
    function onStart()
    {
        Parent::onStart();
        echo("--------- Initializing MOD: platform ---------");
        exec("./client/init.cs");
        if ($StandAlone && $Server::Dedicated)
        {
            exec("./server/init.cs");
            initServer();
        }
        exec("./common/scripts/init.cs");
        $Token = "";
        $TokenStandalone = "TOKEN_STANDALONE";
        if ($Server::Dedicated)
        {
            initDedicated();
        }
        else
        {
            initClient();
        }
        if (isObject(ConsoleEntry))
        {
            ConsoleEntry.loadHistory("platform/client/consoleHistory.txt");
        }
        else
        {
            echo("---no ConsoleEntry not loading history");
        }
        return ;
    }
    function onExit()
    {
        dumpConsoleHistoryReally();
        if (!$Server::Dedicated)
        {
            shutdownClient();
            if (!$NonInteractive)
            {
                echo("Exporting client prefs");
                $UserPref::Player::Password = munge($UserPref::Player::Password);
                $UserPref::Player::AIMPassword = munge($UserPref::Player::AIMPassword);
                export("$UserPref::*", "./client/userprefs.cs", 0);
            }
            else
            {
                echo("NOTE: Skipping export of userprefs.cs on non-interactive client.");
            }
        }
        else
        {
            shutdownDedicated();
        }
        Parent::onExit();
        return ;
    }
    function dumpConsoleHistorySchedule()
    {
        if (!isDefined("$gConsoleHistoryDumpTimer"))
        {
            $gConsoleHistoryDumpTimer = "";
            $gConsoleHistoryPeriod = 15000;
        }
        cancel($gConsoleHistoryDumpTimer);
        $gConsoleHistoryDumpTimer = "";
        $gConsoleHistoryDumpTimer = schedule($gConsoleHistoryPeriod, 0, "dumpConsoleHistoryReally");
        return ;
    }
    function dumpConsoleHistoryReally()
    {
        if (isObject(ConsoleEntry) && !$NonInteractive)
        {
            ConsoleEntry.dumpHistory("platform/client/consoleHistory.txt");
        }
        else
        {
            echo("---no ConsoleEntry not dumping history");
        }
        return ;
    }
};

activatePackage(platform);

