$VURL::curVURL = "";
function vurl::isResolvedVURL(%this) {
    return !(0 @ " " @ %this._server $= "");
};
function vurl::isParsed(%this) {
    return %this.isParsed;
};
function vurl::parse(%this) {
    %payload = NextToken(%this.vurl, "protocol", ":");
    if ((stricmp(%protocol, "vside") != 0.0)) {
    }
    if (("" $= %payload)) {
        %errorText = "VURL::parse attempting to determine protocol and paylod. %this.vurl =" @ " " @ %this.vurl;
        %errorText.doReportError(%this, "parseError");
        return 0;
    }
    %this.protocol = %protocol;
    %parameters = NextToken(%payload, "target", "?");
    %parameters = strreplace(%parameters, "?", "&");
    if ((getSubStr(%target, 0, 1) $= "/")) {
        %target = getSubStr(%target, 1, (strlen(%target) - 1.0));
    }
    %targetpath = NextToken(%target, "targettype", "/");
    strlwr(%targettype);
    if ((stricmp(%targettype, "location") != 0.0)) {
    }
    if ((stricmp(%targettype, "user") != 0.0)) {
    }
    if ((stricmp(%targettype, "apartment") != 0.0)) {
        %errorText = "VURL::parse unknown type type in vurl =" @ " " @ %this.vurl;
        %errorText.doReportError(%this, "parseError");
        return 0;
    }
    %this.targetType = %targettype;
    %this.targetPath = %targetpath;
    %this.isIncomplete = 0;
    %this.isRawTransform = 0;
    if ((stricmp(%this.targetType, "location") == 0.0)) {
    }
    if ((stricmp(%this.targetType, "apartment") == 0.0)) {
        %targetDest = NextToken(%targetpath, "city", "/");
        %this.targetDest = urlDecode(%targetDest);
        %this.targetCity = urlDecode(%city);
        if ((%this.targetDest $= "")) {
            %this.isResolved = 1;
            %this.isIncomplete = 1;
            %this.isParsed = 1;
            %this.reconstructVURL();
            return 1;
        }
        if ((stricmp(%this.targetType, "location") == 0.0)) {
            %testTargetDest = strreplace(%this.targetDest, ",", " ");
            %wordCount = getWordCount(%testTargetDest);
            if ((%wordCount == 3.0)) {
            }
            if ((%wordCount == 7.0)) {
                if (($Server::Dedicated == 1.0)) {
                }
                if (isObjectAndHasPermission_NoWarn($player, "freeVURLTransform")) {
                    %this.targetDest = %testTargetDest;
                    %this.isRawTransform = 1;
                }
            }
            if ((%wordCount != 1.0)) {
            }
            if ((%this.isRawTransform == 0.0)) {
                %errorText = "Invalid number of parameters in target";
                %errorText.doReportError(%this, "parseError");
                return 0;
            }
        }
    }
    while (!(%parameters $= "")) {
        %parameters = NextToken(%parameters, "param", "&");
        %value = urlDecode(NextToken(%param, "name", "="));
        %name = strlwr(%name);
        %this._ = %value @ urlDecode(%name);
    }
    %this.isResolved = !(!(%parameters $= "") @ 0 @ " " @ %this._server $= "");
    if (%this.isResolved) {
        %this.reorderServerList();
        %this.retryIndex = 0;
    }
    %this.isParsed = 1;
    %this.reconstructVURL();
    return 1;
};
function vurl::reconstructVURL(%this) {
    if (!(%this.isParsed)) {
        return 0;
    }
    %newVurl = "vside:/" @ %this.targetType @ "/" @ %this.targetPath;
    %paramCount = 0;
    if (!(%this._key $= "")) {
        %newVurl = %newVurl @ (%paramCount == 0.0) ? "?" : "&";
        %paramCount = (%paramCount + 1.0);
        %newVurl = %newVurl @ "key=" @ urlEncode(%this._key);
    }
    %retryServer = 0;
    while (!(%retryServer @ " " @ %this._server $= "")) {
        %newVurl = %newVurl @ (%paramCount == 0.0) ? "?" : "&";
        %paramCount = (%paramCount + 1.0);
        %newVurl = %newVurl @ "server" @ %retryServer @ "=" @ %retryServer @ urlEncode(%this._server);
        %retryServer = (%retryServer + 1.0);
    }
    %this.vurl = !(%retryServer @ " " @ %this._server $= "") @ %newVurl;
    log("network", "debug", "Reconstructed VURL=\"" @ %this.vurl @ "\"");
    return 1;
};
function vurl::tryProcessDynamicVurl(%this) {
    if ((firstWord(%this.vurl) $= "dynamic")) {
        %dvurlType = getWord(%this.vurl, 1);
        %newVurl = "";
        if ((%dvurlType $= "partnerSpawn")) {
            if (!($AmClient)) {
                error(getScopeName() @ " " @ "- type only valid on client:" @ " " @ %this.vurl @ " " @ getTrace());
                return 0;
            }
            %partnerObj = $Net::userOwner.getPartnerObj(gLoginPartnersInfo);
            %newVurl = %partnerObj.vurl;
        }
        error(getScopeName() @ " " @ "- Unknown dynamic vurl type:" @ " " @ %this.vurl @ " " @ getTrace());
        return 0;
        echo(getScopeName() @ " " @ "- converting dynamic vurl \"" @ %this.vurl @ "\" to \"" @ %newVurl @ "\".");
        %this.vurl = %newVurl;
    }
};
function vurl::setVURL(%this, %vurl) {
    %this.isParsed = 0;
    %this.isResolved = 0;
    %this.vurl = %vurl;
    %this.tryProcessDynamicVurl();
    return %this.parse();
};
function vurl::setPassword(%this, %password) {
    %this._key = %password;
    %this.reconstructVURL();
};
function vurl::setIgnoreDownloadStatus(%this, %val) {
    %this.ignoreDownloadStatus = %val;
};
function vurl::execute(%this) {
    if ((%this.retryIndex == 0.0)) {
    }
    if (testFlooding($player, "teleport", 1)) {
        log("network", "warn", "Teleport Flooding");
        %errorText = "";
        %errorText.doReportError(%this, "FLOOD");
        return 0;
    }
    if (isObject($VURL::curVURL)) {
    }
    if (($VURL::curVURL.getId() != %this.getId())) {
        log("network", "warn", "Pending VURL execution being overridden");
        "delete".schedule($VURL::curVURL, 0);
    }
    $VURL::curVURL = %this;
    log("network", "debug", "executing VURL =" @ " " @ %this.vurl);
    if (!(%this.isParsed)) {
        %errorText = "Attempting to execute unparsed VURL =" @ " " @ %this.vurl;
        %errorText.doReportError(%this, "EXECUTEERROR");
        return 0;
    }
    if ((stricmp(%this.targetType, "user") == 0.0)) {
        if ((stricmp(%this.targetPath, $Player::Name) == 0.0)) {
            %errorText = "Teleporting to yourself?";
            "".doReportError(%this, "TELETOSELF");
            return 0;
        }
        if (isObject(ServerConnection)) {
        }
        if (isNPCName(%this.targetPath)) {
            %this.doReportSuccessExpected();
            commandToServer('TeleportToPlayer', %this.targetPath);
            return 1;
        }
    }
    if (!(%this.isResolved)) {
    }
    if (!($StandAlone)) {
        log("network", "info", "Unresolved VURL execution. Processing resolution request first");
        %this.doResolveVURL();
        return 1;
    }
    if (%this.isIncomplete) {
        log("network", "debug", "Handling incomplete VURL");
        %this.handleIncompeteVURL();
        "delete".schedule(%this, 0);
        return 1;
    }
    if ((%this.retryIndex @ " " @ %this._server $= "")) {
    }
    if (!($StandAlone) && $StandAlone) {
    }
    if (!(%this.standAloneRetry $= "")) {
        "".doReportError(%this, "nomoreretry");
        return 0;
    }
    if ($StandAlone) {
        %this.standAloneRetry = 1;
    }
    %cityName = %this._server.getCityFromServerName(%this, %this.retryIndex);
    if (isObject(WorldMapCityInfoMap)) {
    }
    if (isObject(LoadingGui)) {
        %cityInfo = %cityName.get(WorldMapCityInfoMap);
        %cityInfo.background.setBitmap(LoadingGui);
    }
    if (%cityName.checkCityDownloadStatus(%this)) {
    }
    if (!(%this.ignoreDownloadStatus)) {
        "".doReportError(%this, "downloading");
        return 0;
    }
    %this.doReportSuccessExpected();
    if ((stricmp(%this.targetType, "location") == 0.0)) {
    }
    if ((stricmp(%this.targetType, "apartment") == 0.0)) {
    }
    if ((stricmp(%this.targetType, "user") == 0.0)) {
        if ($StandAlone) {
            commandToServer('TeleportToVURL', %this.vurl);
        }
        %serverDest = %this._server;
        %this.retryIndex;
        %this.retryIndex = (%this.retryIndex + 1.0);
        SetTransition(%serverDest, %this.vurl);
    }
    return 1;
};
function vurl::clearResolutionAndExecute(%this) {
    %this.clearResolution();
    %this.execute();
};
function vurl::getCityFromServerName(%this, %ServerName) {
    %idx = 0;
    while ((%idx < WorldMapServers.getCount())) {
        %serverProps = %idx.getObject(WorldMapServers);
        %testname = "name".get(%serverProps);
        if ((%testname $= %ServerName)) {
            %cityspec = "city".get(%serverProps);
            %citybuilding = strreplace(%cityspec, "_", " ");
            %cityName = firstWord(%citybuilding);
            echo("cityspec - " @ %cityspec @ " becomes city name - " @ %cityName);
            return %cityName;
        }
        %idx = (%idx + 1.0);
    }
    return "";
};
function vurl::checkCityDownloadStatus(%this, %cityName) {
    if (isObject(packageDownload)) {
    }
    if ($AutoDownloadPackages) {
        %status = %cityName.getStatusForCity(packageDownload);
        if ((%status $= "done")) {
            return 0;
        }
        return 1;
    }
    return 0;
};
function vurl::clearResolution(%this) {
    %this.isResolved = 0;
    %idx = 0;
    while (!(%idx @ " " @ %this._server $= "")) {
        %this.server = "" @ %idx;
        %idx = (%idx + 1.0);
    }
};
function vurl::doResolveVURL(%this) {
    %request = new ManagerRequest("") {
        className = 0 @ "ResolveVURLRequest";
    };
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    %url = $Net::ClientServiceURL @ "/ResolveVURL" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token) @ "&vurl=" @ urlEncode(%this.vurl);
    log("network", "debug", "ResovleVURLRequest: " @ %url);
    %request.VURLHandler = %this;
    %url.setURL(%request);
    %request.start();
};
function vurl::doReportError(%this, %errorCode, %errorText) {
    if ((stricmp(%errorCode, "nomoretry") == 0.0)) {
        if (!(%this.lastError $= "")) {
            %errorCode = %this.lastErrorCode;
        }
    }
    %this.lastError = %errorCode;
    %handled = 0;
    log("network", "info", "VURL Error code=\"" @ %errorCode @ "\" text=\"" @ %errorText @ "\"");
    if (!(%this.cbReportError $= "")) {
        %cmd = "%handled = " @ %this.cbReportError @ "( \"" @ %this.vurl @ "\", \"" @ %errorCode @ "\", \"" @ %errorText @ "\");";
        log("network", "debug", "Executing callback cbReportError = \"" @ %cmd @ "\"");
        eval(%cmd);
    }
    if (!(%handled)) {
        vurl::DefaultReportError(%this, %errorCode, %errorText);
    }
};
function vurl::doReportSuccessExpected(%this) {
    if (geTGF.isVisible()) {
        %this.doreopen = 1 @ geTGF;
    }
    geTGF.closeFully();
    log("network", "info", "VURL Teleport success expected. VURL=" @ %this.vurl);
    if (!(%this.cbSuccessExpected $= "")) {
        %cmd = %this.cbSuccessExpected @ "( \"" @ %this.vurl @ "\" );";
        log("network", "debug", "Executing callback cbSuccessExpected = \"" @ %cmd @ "\"");
        eval(%cmd);
    }
};
function vurl::doReportSuccess(%this) {
    log("network", "info", "VURL Teleport successful. VURL=" @ %this.vurl);
    if (!(%this.cbSuccess $= "")) {
        %cmd = %this.cbSuccess @ "( \"" @ %this.vurl @ "\" );";
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
    if ((stricmp(%errorCode, "missingdoorcode") == 0.0)) {
    }
    if ((stricmp(%errorCode, "incorrectdoorcode") == 0.0)) {
        %vurl.doRequestPassword();
    }
    if ((%errorCode $= "accessDenied")) {
    }
    if ((errorCode $= "parseError")) {
    }
    if (!(%errorText $= "")) {
        handleSystemMessage("msgInfoMessage", %errorText);
        "delete".schedule(%vurl.VURLHandler, 0);
    }
    if ((stricmp(%errorCode, "offline") == 0.0)) {
        if ((stricmp(%vurl.targetType, "user") == 0.0)) {
            %errorCode = "USEROFFLINE";
        }
        if ((stricmp(%vurl.targetType, "apartment") == 0.0)) {
            %errorCode = "NOSPACE";
        }
    }
    %errorMessage = ;
    handleSystemMessage("msgInfoMessage", %errorMessage);
    if (geTGF.isVisible()) {
    }
    if ((%vurl.loggedIn == WorldMap)) {
        MessageBoxOK("Whoa!", %errorMessage, "");
    }
};
function vurl::DefaultRequestPassword(%this) {
    MessageBoxTextEntryWithCancel(, , VURL_ResumbmitWithPassword, "", 0);
    $VURL::saveVurlForPasswordCheck = %this.vurl;
};
function VURL_ResumbmitWithPassword(%newPassword) {
    %vurl = vurlGetParsedVurl($VURL::saveVurlForPasswordCheck);
    %newPassword.setPassword(%vurl);
    %vurl.execute();
};
function vurl::handleIncompeteVURL(%this) {
    if ((stricmp(%this.targetType, "location") == 0.0)) {
        if (isObject(WorldMap)) {
            log("network", "info", "Showing map for city \"" @ %this.targetCity @ "\"");
            "Map".openToTabName(geTGF);
            %this.targetCity.selectCity(WorldMap);
        }
    }
    if ((stricmp(%this.targetType, "apartment") == 0.0)) {
        log("network", "warn", "Should show directory for building \"" @ %this.targetCity @ "\" here");
    }
};
function ResolveVURLRequest::onDone(%this) {
    echo(getScopeName());
    %status = findRequestStatus(%this);
    log("network", "debug", "ResolveVURLRequest status: " @ %status);
    if ((stricmp(%status, "success") == 0.0)) {
        %this.vurl = "vurl".getValue(%this);
        log("network", "debug", "ResolveVURLRequest returned resolved VURL =" @ " " @ %this.vurl);
        %vurl = %this.VURLHandler;
        if (%this.vurl.setVURL(%vurl)) {
            if ((%vurl.execute() == 0.0)) {
                log("network", "error", "unable to execute VURL vurl=" @ " " @ %this.vurl);
            }
        }
        log("network", "error", "Unable to parse returned VURL");
    }
    echo(getScopeName() @ "->" @ %status);
    %statusMsg = "statusMsg".getValue(%this);
    %errorCode = "errorCode".getValue(%this);
    %statusMsg.doReportError(%this.VURLHandler, %errorCode);
    "delete".schedule(%this.VURLHandler, 0);
    "delete".schedule(%this, 0);
};
function ResolveVURLRequest::onError(%this, %unused, %errMsg) {
    log("network", "debug", "ResolveVURLRequest::onError: " @ %errMsg);
    %this.VURLHandler.error = 1;
    "delete".schedule(%this, 0);
    if (isObject($VURL::curVURL)) {
    }
    if (($VURL::curVURL.getId() == %this.VURLHandler.getId())) {
        $VURL::curVURL.delete();
        $VURL::curVURL = 0;
    }
};
function vurl::reorderServerList(%this) {
    if ($Server::Dedicated) {
        return;
    }
    %idxSwapout = 0;
    while (!(%idxSwapout @ " " @ %this._server $= "")) {
        if ((stricmp($ServerName, %this._server) == 0.0 @ %idxSwapout)) {
        }
        %idxSwapout = (%idxSwapout + 1.0);
    }
    if ((!(%idxSwapout @ " " @ %this._server $= "") @ %idxSwapout @ " " @ %this._server $= "")) {
    }
    if ((%idxSwapout == 0.0)) {
        return;
    }
    %idx = %idxSwapout;
    while ((%idx > 0.0)) {
        %this._server = (%idx - 1.0) @ %this._server @ %idx;
        %idx = (%idx - 1.0);
    }
    %this._server = (%idx > 0.0) @ $ServerName @ 0;
};
function vurlOperation(%line, %ignoreDownloadStatus) {
    if (!(isDefined("%ignoreDownloadStatus"))) {
        %ignoreDownloadStatus = 0;
    }
    log("network", "debug", "vurlOperation, vurl=\"" @ %line @ "\"");
    %vurl = new ScriptObject("");;
    0;
    "VURL".bindClassName(%vurl);
    %ignoreDownloadStatus.setIgnoreDownloadStatus(%vurl);
    if (%line.setVURL(%vurl)) {
        if ((%vurl.execute() == 0.0)) {
            log("network", "error", "Unable to execute VURL" @ " " @ %line);
            %vurl.delete();
        }
        CustomSpacesSelector.close();
    }
    log("network", "error", "Unable to set and parse VURL" @ " " @ %line);
    %vurl.delete();
};
function vurlClearResolutionAndExecute(%line) {
    log("network", "debug", "vurlClearResolutionAndExecute, vurl=\"" @ %line @ "\"");
    %vurl = new ScriptObject("");;
    0;
    "VURL".bindClassName(%vurl);
    if (%line.setVURL(%vurl)) {
        %vurl.clearResolution();
        if ((%vurl.execute() == 0.0)) {
            log("network", "error", "Unable to execute VURL" @ " " @ %line);
            %vurl.delete();
        }
    }
    log("network", "error", "Unable to set and parse VURL" @ " " @ %line);
    %vurl.delete();
};
function vurlClearResolution(%line) {
    %questionMarkIndex = strpos(%line, "?");
    if ((%questionMarkIndex != -(1.0))) {
        return getSubStr(%line, 0, %questionMarkIndex);
    }
    return %line;
};
function vurlGetParsedVurl(%aVurlString) {
    %theVurl = new ScriptObject("") {
        class = 0 @ "VURL";
    };
    if (%aVurlString.setVURL(%theVurl)) {
        log("login", "debug", getScopeName() @ " " @ "- parsed VURL");
        return %theVurl;
    }
    log("login", "error", getScopeName() @ " " @ "-Unable to set and parse VURL");
    %theVurl.delete();
    return 0;
};
