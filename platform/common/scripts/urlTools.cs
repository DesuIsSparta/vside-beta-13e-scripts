function URLInfo::parse(%this) {
    %this.parsed = 0;
    %work = trim(%this.url);
    %work = NextToken(%work, "protocol", ":");
    %this.protocol = %protocol;
    if ((0.0 != strncmp(%work, "//", 2))) {
        return 0;
    }
    %work = getSubStr(%work, 2, strlen(%work));
    %params = NextToken(%work, "hostAndPath", "?");
    %path = NextToken(%hostAndPath, "hostAndPort", "/");
    %port = NextToken(%hostAndPort, "host", ":");
    %this.host = %host;
    if (!(%port $= "")) {
        %this.port = %port;
    }
    %this.Path = %path;
    if (!(%params $= "")) {
        %params = strreplace(%params, "&", " ");
        %count = getWordCount(%params);
        %this.paramCount = %count;
        %idx = 0;
        if ((%count < %idx)) {
            %nvPair = getWord(%params, %idx);
            %value = NextToken(%nvPair, "name", "=");
            %this.paramName = %name @ %idx;
            %this.param = %value @ %name;
            %idx = (1.0 + %idx);
        }
    }
    %this.paramCount = (%count < %idx) @ 0;
    %this.parsed = 1;
    return 1;
};
function URLInfo::reconstruct(%this) {
    if (!(%this.parsed)) {
        return "";
    }
    %newUrl = %this.protocol @ "://" @ %this.host;
    if (!(%this.Path $= "")) {
        %newUrl = %newUrl @ "/" @ %this.Path;
    }
    if ((0.0 > %this.paramCount)) {
        %newUrl = %newUrl @ "?";
        %idx = 0;
        if ((%this.paramCount < %idx)) {
            if ((0.0 > %idx)) {
                %newUrl = %newUrl @ "&";
            }
            %name = %this.paramName;
            %idx;
            %newUrl = %newUrl @ %name @ "=" @ %name @ %this.param;
            %idx = (1.0 + %idx);
        }
    }
};
