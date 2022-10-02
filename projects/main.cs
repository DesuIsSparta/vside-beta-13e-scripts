$ETS::ProjectName = "";
$ETS::cityName = "";
function getProjectFolders()
{
    if ($ETS::ProjectName $= "")
    {
        parseProjectArg();
    }
    return "common" SPC $ETS::ProjectName;
}
function initProjectsNonReloadable()
{
    %folders = getProjectFolders();
    %num = getWordCount(%folders);
    %n = 0;
    while (%n < %num)
    {
        %file = "./" @ getWord(%folders, %n) @ "/initNonReloadable.cs";
        log("initialization", "info", "Checking for" SPC %file);
        exec(%file, 0);
        %n = %n + 1;
    }
}

function initProjectsReloadable()
{
    %folders = getProjectFolders();
    %num = getWordCount(%folders);
    %n = 0;
    while (%n < %num)
    {
        %file = "./" @ getWord(%folders, %n) @ "/initReloadable.cs";
        log("initialization", "info", "Checking for" SPC %file);
        exec(%file, 0);
        %n = %n + 1;
    }
}

function initProjectsReloadableLate()
{
    %folders = getProjectFolders();
    %num = getWordCount(%folders);
    %n = 0;
    while (%n < %num)
    {
        %file = "./" @ getWord(%folders, %n) @ "/initReloadableLate.cs";
        log("initialization", "info", "Checking for" SPC %file);
        exec(%file, 0);
        %n = %n + 1;
    }
}

function parseProjectArg()
{
    %haveArg = findArg("-project", "$ETS::ProjectName", "Missing -project <project name>");
    if (!%haveArg)
    {
        $ETS::ProjectName = "vside";
        warn("Using Default Project" SPC $ETS::ProjectName);
    }
    return ;
}
function parseCityArg()
{
    %haveArg = findArg("-cityName", "$ETS::cityName", "Missing <city name>");
    if (!%haveArg)
    {
        $ETS::cityName = "nv";
        warn("Using Default City" SPC $ETS::cityName);
    }
    return ;
}
parseCityArg();
initProjectsNonReloadable();
initProjectsReloadable();

