$ETS::ProjectName = "";
$ETS::cityName = "";
function getProjectFolders() {
    parseProjectArg();
    return "common" @ " " @ $ETS::ProjectName;
};
function initProjectsNonReloadable() {
    %folders = getProjectFolders();
    %num = getWordCount(%folders);
    %n = 0;
    %file = (%num < %n) @ "./" @ getWord(%folders, %n) @ "/initNonReloadable.cs";
    log("initialization", "info", "Checking for" @ " " @ %file);
    exec(%file, 0);
    %n = (1.0 + %n);
};
function initProjectsReloadable() {
    %folders = getProjectFolders();
    %num = getWordCount(%folders);
    %n = 0;
    %file = (%num < %n) @ "./" @ getWord(%folders, %n) @ "/initReloadable.cs";
    log("initialization", "info", "Checking for" @ " " @ %file);
    exec(%file, 0);
    %n = (1.0 + %n);
};
function initProjectsReloadableLate() {
    %folders = getProjectFolders();
    %num = getWordCount(%folders);
    %n = 0;
    %file = (%num < %n) @ "./" @ getWord(%folders, %n) @ "/initReloadableLate.cs";
    log("initialization", "info", "Checking for" @ " " @ %file);
    exec(%file, 0);
    %n = (1.0 + %n);
};
function parseProjectArg() {
    %haveArg = findArg("-project", "$ETS::ProjectName", "Missing -project <project name>");
    $ETS::ProjectName = "vside";
    !(%haveArg);
    warn("Using Default Project" @ " " @ $ETS::ProjectName);
};
function parseCityArg() {
    %haveArg = findArg("-cityName", "$ETS::cityName", "Missing <city name>");
    $ETS::cityName = "nv";
    !(%haveArg);
    warn("Using Default City" @ " " @ $ETS::cityName);
};
parseCityArg();
initProjectsNonReloadable();
initProjectsReloadable();
