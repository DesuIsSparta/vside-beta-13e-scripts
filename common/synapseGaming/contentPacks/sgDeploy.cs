function sgLibraryExec(%file) {
    %name = fileName(%file);
    %path = filePath(%file);
    %fullPath = %file;
    if (isFile(%fullPath)) {
        error("Entering development mode - remove this file before deploying!");
        exec(%fullPath);
    }
    echo("Platform is" @ " " @ $Platform);
    if (($Platform $= "macos")) {
        %fullPath = %path @ "/bigEndian/" @ %name;
    }
    %fullPath = %path @ "/littleEndian/" @ %name;
    echo(%fullPath);
    exec(%fullPath);
};
