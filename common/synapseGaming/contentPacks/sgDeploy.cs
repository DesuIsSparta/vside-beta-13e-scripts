function sgLibraryExec(%file)
{
    %name = fileName(%file);
    %path = filePath(%file);
    %fullPath = %file;
    if (isFile(%fullPath))
    {
        error("Entering development mode - remove this file before deploying!");
        exec(%fullPath);
    }
    else
    {
        echo("Platform is" SPC $Platform);
        if ($Platform $= "macos")
        {
            %fullPath = %path @ "/bigEndian/" @ %name;
        }
        else
        {
            %fullPath = %path @ "/littleEndian/" @ %name;
        }
        echo(%fullPath);
        exec(%fullPath);
    }
    return ;
}
