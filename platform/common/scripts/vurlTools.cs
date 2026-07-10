$VURL::curVURL = "";
function vurl::isResolvedVURL(%this) {
    return !(0 @ %this SPC _server $= "");
};
function vurl::isParsed(%this) {
    return isParsed;
};
function vurl::parse(%this) {
    %payload = NextToken(vurl, "protocol", ":");
    %this;
    if ((0.0 != stricmp(%protocol, "vside"))) {
    }
    if (("" $= %payload)) {
        %errorText = %this @ vurl;
        "VURL::parse attempting to determine protocol and paylod. %this.vurl =" @ " ";
        %this.doReportError("parseError", %errorText);
        return 0;
    }
    protocol = %protocol @ %this;
    %parameters = NextToken(%payload, "target", "?");
    %parameters = strreplace(%parameters, "?", "&");
    if ((getSubStr(%target, 0, 1) $= "/")) {
        %target = getSubStr(%target, 1, (1.0 - strlen(%target)));
    }
    %targetpath = NextToken(%target, "targettype", "/");
    strlwr(%targettype);
    if ((0.0 != stricmp(%targettype, "location"))) {
    }
    if ((0.0 != stricmp(%targettype, "user"))) {
    }
    if ((0.0 != stricmp(%targettype, "apartment"))) {
        %errorText = %this @ vurl;
        "VURL::parse unknown type type in vurl =" @ " ";
        %this.doReportError("parseError", %errorText);
        return 0;
    }
    targetType = %targettype @ %this;
    targetPath = %targetpath @ %this;
    isIncomplete = 0 @ %this;
    isRawTransform = 0 @ %this;
    if ((%this == stricmp(targetType, "location"))) {
    }
    if ((%this == stricmp(targetType, "apartment"))) {
        %targetDest = NextToken(%targetpath, "city", "/");
        0.0;
        targetDest = 0.0 @ urlDecode(%targetDest) @ %this;
        targetCity = urlDecode(%city) @ %this;
        if ((%this SPC targetDest $= "")) {
            isResolved = 1 @ %this;
            isIncomplete = 1 @ %this;
            isParsed = 1 @ %this;
            %this.reconstructVURL();
            return 1;
        }
        if ((%this == stricmp(targetType, "location"))) {
            %testTargetDest = strreplace(targetDest, ",", " ");
            %this;
            %wordCount = getWordCount(%testTargetDest);
            0.0;
            if ((3.0 == %wordCount)) {
            }
            if ((7.0 == %wordCount)) {
                if ((1.0 == $Server::Dedicated)) {
                }
                if (isObjectAndHasPermission_NoWarn($player, "freeVURLTransform")) {
                    targetDest = %testTargetDest @ %this;
                    isRawTransform = 1 @ %this;
                }
            }
            if ((1.0 != %wordCount)) {
            }
            if ((%this == isRawTransform)) {
                %errorText = "Invalid number of parameters in target";
                0.0;
                %this.doReportError("parseError", %errorText);
                return 0;
            }
        }
    }
    if (!(%parameters $= "")) {
        %parameters = NextToken(%parameters, "param", "&");
        %value = urlDecode(NextToken(%param, "name", "="));
        %name = strlwr(%name);
        _ = %value @ urlDecode(%name) @ %this;
    }
    isResolved = !(!(%parameters $= "") @ 0 @ %this SPC _server $= "") @ %this;
    if (isResolved) {
        %this.reorderServerList();
        retryIndex = %this @ 0 @ %this;
    }
    isParsed = 1 @ %this;
    %this.reconstructVURL();
    return 1;
};
function vurl::reconstructVURL(%this) {
    if (!(isParsed)) {
        return 0;
    }
    %newVurl = %this @ targetPath;
    "vside:/" @ %this @ targetType @ "/";
    %paramCount = 0;
    if (!(%this SPC _key $= "")) {
        %newVurl = %newVurl @ (0.0 == %paramCount) ? "?" : "&";
        %paramCount = (1.0 + %paramCount);
        %newVurl = %this @ urlEncode(_key);
        %newVurl @ "key=";
    }
    %retryServer = 0;
    if (!(%retryServer @ %this SPC _server $= "")) {
        %newVurl = %newVurl @ (0.0 == %paramCount) ? "?" : "&";
        %paramCount = (1.0 + %paramCount);
        %newVurl = %newVurl @ "server" @ %retryServer @ "=" @ %retryServer @ %this @ urlEncode(_server);
        %retryServer = (1.0 + %retryServer);
    }
    vurl = !(%retryServer @ %this SPC _server $= "") @ %newVurl @ %this;
    log("network", "debug", "Reconstructed VURL=\"" @ %this @ vurl @ "\"");
    return 1;
};
function vurl::tryProcessDynamicVurl(%this) {
    if ((%this SPC firstWord(vurl) $= "dynamic")) {
        %dvurlType = getWord(vurl, 1);
        %this;
        %newVurl = "";
        if ((%dvurlType $= "partnerSpawn")) {
            if (!($AmClient)) {
                error(%this @ vurl @ " " @ getTrace());
                return 0;
            }
            %partnerObj = $Net::userOwner.getPartnerObj();
            gLoginPartnersInfo;
            %newVurl = vurl;
            %partnerObj;
        }
        error(%this @ vurl @ " " @ getTrace());
        return 0;
        echo(getScopeName() @ " " @ "- converting dynamic vurl \"" @ %this @ vurl @ "\" to \"" @ %newVurl @ "\".");
        vurl = %newVurl @ %this;
    }
};
function vurl::setVURL(%this, %vurl) {
    isParsed = 0 @ %this;
    isResolved = 0 @ %this;
    vurl = %vurl @ %this;
    %this.tryProcessDynamicVurl();
    return %this.parse();
};
function vurl::setPassword(%this, %password) {
    _key = %password @ %this;
    %this.reconstructVURL();
};
function vurl::setIgnoreDownloadStatus(%this, %val) {
    ignoreDownloadStatus = %val @ %this;
};
function vurl::execute(%this) {
    if ((%this == retryIndex)) {
    }
    if (testFlooding($player, "teleport", 1)) {
        log("network", "warn", "Teleport Flooding");
        %errorText = "";
        0.0;
        %this.doReportError("FLOOD", %errorText);
        return 0;
    }
    if (isObject($VURL::curVURL)) {
    }
    if ((%this.getId() != $VURL::curVURL.getId())) {
        log("network", "warn", "Pending VURL execution being overridden");
        $VURL::curVURL.schedule(0, "delete");
    }
    $VURL::curVURL = %this;
    log("network", "debug", %this @ vurl);
    if (!(isParsed)) {
        %errorText = %this @ vurl;
        "Attempting to execute unparsed VURL =" @ " ";
        %this.doReportError("EXECUTEERROR", %errorText);
        return 0;
    }
    if ((%this == stricmp(targetType, "user"))) {
        if ((%this == stricmp(targetPath, $Player::Name))) {
            %errorText = "Teleporting to yourself?";
            0.0;
            %this.doReportError("TELETOSELF", "");
            return 0;
        }
        if (isObject()) {
        }
        if (isNPCName(targetPath)) {
            %this.doReportSuccessExpected();
            commandToServer('TeleportToPlayer', targetPath);
            return 1;
        }
    }
    if (!(isResolved)) {
    }
    if (!($StandAlone)) {
        log("network", "info", "Unresolved VURL execution. Processing resolution request first");
        %this.doResolveVURL();
        return 1;
    }
    if (isIncomplete) {
        log("network", "debug", "Handling incomplete VURL");
        %this.handleIncompeteVURL();
        %this.schedule(0, "delete");
        return 1;
    }
    if ((%this @ retryIndex @ %this SPC _server $= "")) {
    }
    if (!($StandAlone)) {
        if ($StandAlone) {
        }
    }
    if (!(%this SPC standAloneRetry $= "")) {
        %this.doReportError("nomoreretry", "");
        return 0;
    }
    if ($StandAlone) {
        standAloneRetry = 1 @ %this;
    }
    %cityName = %this.getCityFromServerName(_server);
    %this @ retryIndex @ %this;
    if (isObject()) {
    }
    if (isObject()) {
        %cityInfo = %cityName.get();
        WorldMapCityInfoMap;
        background.setBitmap();
    }
    if (%this.checkCityDownloadStatus(%cityName)) {
    }
    if (!(ignoreDownloadStatus)) {
        %this.doReportError("downloading", "");
        return 0;
    }
    %this.doReportSuccessExpected();
    if ((%this == stricmp(targetType, "location"))) {
    }
    if ((%this == stricmp(targetType, "apartment"))) {
    }
    if ((%this == stricmp(targetType, "user"))) {
        if ($StandAlone) {
            commandToServer('TeleportToVURL', vurl);
        }
        %serverDest = _server;
        %this @ retryIndex @ %this;
        retryIndex = (%this + retryIndex);
        1.0;
        SetTransition(%serverDest, vurl);
    }
    return 1;
};
function vurl::clearResolutionAndExecute(%this) {
    %this.clearResolution();
    %this.execute();
};
function vurl::getCityFromServerName(%this, %ServerName) {
    %idx = 0;
    if ((getCount() < %idx)) {
        %serverProps = %idx.getObject();
        WorldMapServers;
        %testname = %serverProps.get("name");
        WorldMapServers;
        if ((%testname $= %ServerName)) {
            %cityspec = %serverProps.get("city");
            %citybuilding = strreplace(%cityspec, "_", " ");
            %cityName = firstWord(%citybuilding);
            echo("cityspec - " @ %cityspec @ " becomes city name - " @ %cityName);
            return %cityName;
        }
        %idx = (1.0 + %idx);
    }
    return "";
};
function vurl::checkCityDownloadStatus(%this, %cityName) {
    if (isObject()) {
    }
    if ($AutoDownloadPackages) {
        %status = %cityName.getStatusForCity();
        packageDownload;
        if ((packageDownload SPC %status $= "done")) {
            return 0;
        }
        return 1;
    }
    return 0;
};
function vurl::clearResolution(%this) {
    isResolved = 0 @ %this;
    %idx = 0;
    if (!(%idx @ %this SPC _server $= "")) {
        server = "" @ %idx @ %this;
        %idx = (1.0 + %idx);
    }
};
function vurl::doResolveVURL(%this) {
    className = ManagerRequest @ new ""() @ "ResolveVURLRequest";
    0;
    %request = ;
    if (isObject()) {
        %request.add();
    }
    %url = %this @ urlEncode(vurl);
    MissionCleanup @ MissionCleanup @ $Net::ClientServiceURL @ "/ResolveVURL" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token) @ "&vurl=";
    log("network", "debug", "ResovleVURLRequest: " @ %url);
    VURLHandler = %this @ %request;
    %request.setURL(%url);
    %request.start();
};
function vurl::doReportError(%this, %errorCode, %errorText) {
    if ((0.0 == stricmp(%errorCode, "nomoretry"))) {
        if (!(%this SPC lastError $= "")) {
            %errorCode = lastErrorCode;
            %this;
        }
    }
    lastError = %errorCode @ %this;
    %handled = 0;
    log("network", "info", "VURL Error code=\"" @ %errorCode @ "\" text=\"" @ %errorText @ "\"");
    if (!(%this SPC cbReportError $= "")) {
        %cmd = "%handled = " @ %this @ cbReportError @ "( \"" @ %this @ vurl @ "\", \"" @ %errorCode @ "\", \"" @ %errorText @ "\");";
        log("network", "debug", "Executing callback cbReportError = \"" @ %cmd @ "\"");
        eval(%cmd);
    }
    if (!(%handled)) {
        vurl::DefaultReportError(%this, %errorCode, %errorText);
    }
};
function vurl::doReportSuccessExpected(%this) {
    if (isVisible()) {
        doreopen = geTGF @ 1 @ geTGF;
    }
    closeFully();
    log("network", "info", %this @ vurl);
    if (!(%this SPC cbSuccessExpected $= "")) {
        %cmd = geTGF @ "VURL Teleport success expected. VURL=" @ %this @ cbSuccessExpected @ "( \"" @ %this @ vurl @ "\" );";
        log("network", "debug", "Executing callback cbSuccessExpected = \"" @ %cmd @ "\"");
        eval(%cmd);
    }
};
function vurl::doReportSuccess(%this) {
    log("network", "info", %this @ vurl);
    if (!(%this SPC cbSuccess $= "")) {
        %cmd = "VURL Teleport successful. VURL=" @ %this @ cbSuccess @ "( \"" @ %this @ vurl @ "\" );";
        log("network", "debug", "Executing callback cbSuccess = \"" @ %cmd @ "\"");
        eval(%cmd);
    }
};
function vurl::doRequestPassword(%this) {
    %this.DefaultRequestPassword();
};
function vurl::DefaultReportError(%vurl, %errorCode, %errorText) {
    log("network", "error", "VURL Errorcode = \"" @ %errorCode @ "\" ErrorMessage = \"" @ %errorText @ "\"");
    if ((%errorCode $= "")) {
        log("network", "error", "empty VURL Errorcode.");
        return;
    }
    if ((0.0 == stricmp(%errorCode, "missingdoorcode"))) {
    }
    if ((0.0 == stricmp(%errorCode, "incorrectdoorcode"))) {
        %vurl.doRequestPassword();
    }
    if ((%errorCode $= "accessDenied")) {
    }
    if ((errorCode $= "parseError")) {
    }
    if (!(%errorText $= "")) {
        handleSystemMessage("msgInfoMessage", %errorText);
        VURLHandler.schedule(0, "delete");
    }
    if ((0.0 == stricmp(%errorCode, "offline"))) {
        if ((%vurl == stricmp(targetType, "user"))) {
            %errorCode = "USEROFFLINE";
            0.0;
        }
        if ((%vurl == stricmp(targetType, "apartment"))) {
            %errorCode = "NOSPACE";
            0.0;
        }
    }
    %errorMessage = ;
    handleSystemMessage("msgInfoMessage", %errorMessage);
    if (isVisible()) {
    }
    if ((WorldMap == loggedIn)) {
        MessageBoxOK("Whoa!", %errorMessage, "");
    }
};
function vurl::DefaultRequestPassword(%this) {
    MessageBoxTextEntryWithCancel(, , "", 0);
    $VURL::saveVurlForPasswordCheck = vurl;
    %this;
};
function VURL_ResumbmitWithPassword(%newPassword) {
    %vurl = vurlGetParsedVurl($VURL::saveVurlForPasswordCheck);
    %vurl.setPassword(%newPassword);
    %vurl.execute();
};
function vurl::handleIncompeteVURL(%this) {
    if ((%this == stricmp(targetType, "location"))) {
        if (isObject()) {
            log("network", "info", WorldMap @ "Showing map for city \"" @ %this @ targetCity @ "\"");
            "Map".openToTabName();
            targetCity.selectCity();
        }
    }
    if ((%this == stricmp(targetType, "apartment"))) {
        log("network", "warn", 0.0 @ "Should show directory for building \"" @ %this @ targetCity @ "\" here");
    }
};
function ResolveVURLRequest::onDone(%this) {
    echo(getScopeName());
    %status = findRequestStatus(%this);
    log("network", "debug", "ResolveVURLRequest status: " @ %status);
    if ((0.0 == stricmp(%status, "success"))) {
        vurl = %this.getValue("vurl") @ %this;
        log("network", "debug", %this @ vurl);
        %vurl = VURLHandler;
        %this;
        if (%vurl.setVURL(vurl)) {
            if ((0.0 == %vurl.execute())) {
                log("network", "error", %this @ vurl);
            }
        }
        log("network", "error", "Unable to parse returned VURL");
    }
    echo(%this @ "unable to execute VURL vurl=" @ " " @ getScopeName() @ "->" @ %status);
    %statusMsg = %this.getValue("statusMsg");
    "ResolveVURLRequest returned resolved VURL =" @ " ";
    %errorCode = %this.getValue("errorCode");
    VURLHandler.doReportError(%errorCode, %statusMsg);
    VURLHandler.schedule(0, "delete");
    %this.schedule(0, "delete");
};
function ResolveVURLRequest::onError(%this, %unused, %errMsg) {
    log("network", "debug", "ResolveVURLRequest::onError: " @ %errMsg);
    error = %this @ VURLHandler;
    1;
    %this.schedule(0, "delete");
    if (isObject($VURL::curVURL)) {
    }
    if ((VURLHandler.getId() == $VURL::curVURL.getId())) {
        $VURL::curVURL.delete();
        $VURL::curVURL = 0;
        %this;
    }
};
function vurl::reorderServerList(%this) {
    if ($Server::Dedicated) {
        return;
    }
    %idxSwapout = 0;
    if (!(%idxSwapout @ %this SPC _server $= "")) {
        if ((0.0 @ %idxSwapout @ %this == stricmp($ServerName, _server))) {
        }
        %idxSwapout = (1.0 + %idxSwapout);
    }
    if ((!(%idxSwapout @ %this SPC _server $= "") @ %idxSwapout @ %this SPC _server $= "")) {
    }
    if ((0.0 == %idxSwapout)) {
        return;
    }
    %idx = %idxSwapout;
    if ((0.0 > %idx)) {
        _server = (1.0 - %idx) @ %this @ _server @ %idx @ %this;
        %idx = (1.0 - %idx);
    }
    _server = (0.0 > %idx) @ $ServerName @ 0 @ %this;
};
function vurlOperation(%line, %ignoreDownloadStatus) {
    if (!(isDefined("%ignoreDownloadStatus"))) {
        %ignoreDownloadStatus = 0;
    }
    log("network", "debug", "vurlOperation, vurl=\"" @ %line @ "\"");
    %vurl = new ""();
    ScriptObject;
    %vurl.bindClassName("VURL");
    %vurl.setIgnoreDownloadStatus(%ignoreDownloadStatus);
    if (%vurl.setVURL(%line)) {
        if ((0.0 == %vurl.execute())) {
            log("network", "error", "Unable to execute VURL" @ " " @ %line);
            %vurl.delete();
        }
        close();
    }
    log("network", "error", "Unable to set and parse VURL" @ " " @ %line);
    %vurl.delete();
};
function vurlClearResolutionAndExecute(%line) {
    log("network", "debug", "vurlClearResolutionAndExecute, vurl=\"" @ %line @ "\"");
    %vurl = new ""();
    ScriptObject;
    %vurl.bindClassName("VURL");
    if (%vurl.setVURL(%line)) {
        %vurl.clearResolution();
        if ((0.0 == %vurl.execute())) {
            log("network", "error", "Unable to execute VURL" @ " " @ %line);
            %vurl.delete();
        }
    }
    log("network", "error", "Unable to set and parse VURL" @ " " @ %line);
    %vurl.delete();
};
function vurlClearResolution(%line) {
    %questionMarkIndex = strpos(%line, "?");
    if ((-(1.0) != %questionMarkIndex)) {
        return getSubStr(%line, 0, %questionMarkIndex);
    }
    return %line;
};
function vurlGetParsedVurl(%aVurlString) {
    class = ScriptObject @ new ""() @ "VURL";
    0;
    %theVurl = ;
    if (%theVurl.setVURL(%aVurlString)) {
        log("login", "debug", getScopeName() @ " " @ "- parsed VURL");
        return %theVurl;
    }
    log("login", "error", getScopeName() @ " " @ "-Unable to set and parse VURL");
    %theVurl.delete();
    return 0;
};
