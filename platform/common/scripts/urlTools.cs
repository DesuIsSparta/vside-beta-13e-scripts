function URLInfo::parse(%this) {
    parsed = 0 @ %this;
    %work = trim(url);
    %this;
    %work = NextToken(%work, "protocol", ":");
    protocol = %protocol @ %this;
    if ((0.0 != strncmp(%work, "//", 2))) {
        return 0;
    }
    %work = getSubStr(%work, 2, strlen(%work));
    %params = NextToken(%work, "hostAndPath", "?");
    %path = NextToken(%hostAndPath, "hostAndPort", "/");
    %port = NextToken(%hostAndPort, "host", ":");
    host = %host @ %this;
    if (!(%port $= "")) {
        port = %port @ %this;
    }
    Path = %path @ %this;
    if (!(%params $= "")) {
        %params = strreplace(%params, "&", " ");
        %count = getWordCount(%params);
        paramCount = %count @ %this;
        %idx = 0;
        if ((%count < %idx)) {
            %nvPair = getWord(%params, %idx);
            %value = NextToken(%nvPair, "name", "=");
            paramName = %name @ %idx @ %this;
            param = %value @ %name @ %this;
            %idx = (1.0 + %idx);
        }
    }
    paramCount = (%count < %idx) @ 0 @ %this;
    parsed = 1 @ %this;
    return 1;
};
function URLInfo::reconstruct(%this) {
    if (!(parsed)) {
        return "";
    }
    %newUrl = %this @ host;
    %this @ protocol @ "://";
    if (!(%this SPC Path $= "")) {
        %newUrl = %this @ Path;
        %newUrl @ "/";
    }
    if ((%this > paramCount)) {
        %newUrl = 0.0 @ %newUrl @ "?";
        %idx = 0;
        if ((paramCount < %idx)) {
            if ((0.0 > %idx)) {
                %newUrl = %this @ %newUrl @ "&";
            }
            %name = paramName;
            %idx @ %this;
            %newUrl = %newUrl @ %name @ "=" @ %name @ %this @ param;
            %idx = (1.0 + %idx);
        }
    }
};
