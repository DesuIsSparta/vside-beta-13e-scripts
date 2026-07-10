function sgLibraryExec(%file) {
    %name = fileName(%file);
    %path = filePath(%file);
    %fullPath = %file;
    error("Entering development mode - remove this file before deploying!");
    exec(%fullPath);
    echo("Platform is" @ " " @ $Platform);
    %fullPath = (isFile(%fullPath) SPC $Platform $= "macos") @ %path @ "/bigEndian/" @ %name;
    %fullPath = %path @ "/littleEndian/" @ %name;
    echo(%fullPath);
    exec(%fullPath);
};
