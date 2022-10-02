function URLInfo::parse(%this)
{
    %this.parsed = 0;
    %work = trim(%this.url);
    %work = NextToken(%work, "protocol", ":");
    %this.protocol = %protocol;
    if (strncmp(%work, "//", 2) != 0)
    {
        return 0;
    }
    %work = getSubStr(%work, 2, strlen(%work));
    %params = NextToken(%work, "hostAndPath", "?");
    %path = NextToken(%hostAndPath, "hostAndPort", "/");
    %port = NextToken(%hostAndPort, "host", ":");
    %this.host = %host;
    if (!(%port $= ""))
    {
        %this.port = %port;
    }
    %this.Path = %path;
    if (!(%params $= ""))
    {
        %params = strreplace(%params, "&", " ");
        %count = getWordCount(%params);
        %this.paramCount = %count;
        %idx = 0;
        while (%idx < %count)
        {
            %nvPair = getWord(%params, %idx);
            %value = NextToken(%nvPair, "name", "=");
            %this.paramName[%idx] = %name;
            %this.param[%name] = %value;
            %idx = %idx + 1;
        }
    }
    else
    {
        %this.paramCount = 0;
    }
    %this.parsed = 1;
    return 1;
}
function URLInfo::reconstruct(%this)
{
    if (!%this.parsed)
    {
        return "";
    }
    %newUrl = %this.protocol @ "://" @ %this.host;
    if (!(%this.Path $= ""))
    {
        %newUrl = %newUrl @ "/" @ %this.Path;
    }
    if (%this.paramCount > 0)
    {
        %newUrl = %newUrl @ "?";
        %idx = 0;
        while (%idx < %this.paramCount)
        {
            if (%idx > 0)
            {
                %newUrl = %newUrl @ "&";
            }
            %name = %this.paramName[%idx];
            %newUrl = %newUrl @ %name @ "=" @ %this.param[%name];
            %idx = %idx + 1;
        }
    }
}


