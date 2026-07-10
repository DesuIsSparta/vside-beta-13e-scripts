$VURL::curVURL = "";
function vurl::isResolvedVURL(%this) {
    return !(0 @ " " @ %this._server $= "");
};
function vurl::isParsed(%this) {
    return %this.isParsed;
};
function vurl::parse(%this) {
    %payload = NextToken(%this.vurl, "protocol", ":");
    if ((0.0 != stricmp(%protocol, "vside"))) {
    }
    if (("" $= %payload)) {
        %errorText = "VURL::parse attempting to determine protocol and paylod. %this.vurl =" @ " " @ %this.vurl;
        %this.doReportError("parseError", %errorText);
        return 0;
    }
    %this.protocol = %protocol;
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
        %errorText = "VURL::parse unknown type type in vurl =" @ " " @ %this.vurl;
        %this.doReportError("parseError", %errorText);
        return 0;
    }
    %this.targetType = %targettype;
    %this.targetPath = %targetpath;
    %this.isIncomplete = 0;
    %this.isRawTransform = 0;
    if ((0.0 == stricmp(%this.targetType, "location"))) {
    }
    if ((0.0 == stricmp(%this.targetType, "apartment"))) {
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
        if ((0.0 == stricmp(%this.targetType, "location"))) {
            %testTargetDest = strreplace(%this.targetDest, ",", " ");
            %wordCount = getWordCount(%testTargetDest);
            if ((3.0 == %wordCount)) {
            }
            if ((7.0 == %wordCount)) {
                if ((1.0 == $Server::Dedicated)) {
                }
                if (isObjectAndHasPermission_NoWarn($player, "freeVURLTransform")) {
                    %this.targetDest = %testTargetDest;
                    %this.isRawTransform = 1;
                }
            }
            if ((1.0 != %wordCount)) {
            }
            if ((0.0 == %this.isRawTransform)) {
                %errorText = "Invalid number of parameters in target";
                %this.doReportError("parseError", %errorText);
                return 0;
            }
        }
    }
    if (!(%parameters $= "")) {
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
        %newVurl = %newVurl @ (0.0 == %paramCount) ? "?" : "&";
        %paramCount = (1.0 + %paramCount);
        %newVurl = %newVurl @ "key=" @ urlEncode(%this._key);
    }
    %retryServer = 0;
    if (!(%retryServer @ " " @ %this._server $= "")) {
        %newVurl = %newVurl @ (0.0 == %paramCount) ? "?" : "&";
        %paramCount = (1.0 + %paramCount);
        %newVurl = %newVurl @ "server" @ %retryServer @ "=" @ %retryServer @ urlEncode(%this._server);
        %retryServer = (1.0 + %retryServer);
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
            %partnerObj = $Net::userOwner.getPartnerObj();
            gLoginPartnersInfo;
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
    if ((0.0 == %this.retryIndex)) {
    }
    if (testFlooding($player, "teleport", 1)) {
        log("network", "warn", "Teleport Flooding");
        %errorText = "";
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
    log("network", "debug", "executing VURL =" @ " " @ %this.vurl);
    if (!(%this.isParsed)) {
        %errorText = "Attempting to execute unparsed VURL =" @ " " @ %this.vurl;
        %this.doReportError("EXECUTEERROR", %errorText);
        return 0;
    }
    if ((0.0 == stricmp(%this.targetType, "user"))) {
        if ((0.0 == stricmp(%this.targetPath, $Player::Name))) {
            %errorText = "Teleporting to yourself?";
            %this.doReportError("TELETOSELF", "");
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
        %this.schedule(0, "delete");
        return 1;
    }
    if ((%this.retryIndex @ " " @ %this._server $= "")) {
    }
    if (!($StandAlone)) {
        if ($StandAlone) {
        }
    }
    if (!(%this.standAloneRetry $= "")) {
        %this.doReportError("nomoreretry", "");
        return 0;
    }
    if ($StandAlone) {
        %this.standAloneRetry = 1;
    }
    %cityName = %this.getCityFromServerName(%this._server);
    %this.retryIndex;
    if (isObject(WorldMapCityInfoMap)) {
    }
    if (isObject(LoadingGui)) {
        %cityInfo = %cityName.get();
        WorldMapCityInfoMap;
        %cityInfo.background.setBitmap();
    }
    if (%this.checkCityDownloadStatus(%cityName)) {
    }
    if (!(%this.ignoreDownloadStatus)) {
        %this.doReportError("downloading", "");
        return 0;
    }
    %this.doReportSuccessExpected();
    if ((0.0 == stricmp(%this.targetType, "location"))) {
    }
    if ((0.0 == stricmp(%this.targetType, "apartment"))) {
    }
    if ((0.0 == stricmp(%this.targetType, "user"))) {
        if ($StandAlone) {
            commandToServer('TeleportToVURL', %this.vurl);
        }
        %serverDest = %this._server;
        %this.retryIndex;
        %this.retryIndex = (1.0 + %this.retryIndex);
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
    if ((WorldMapServers.getCount() < %idx)) {
        %serverProps = %idx.getObject();
        WorldMapServers;
        %testname = %serverProps.get("name");
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
    if (isObject(packageDownload)) {
    }
    if ($AutoDownloadPackages) {
        %status = %cityName.getStatusForCity();
        packageDownload;
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
    if (!(%idx @ " " @ %this._server $= "")) {
        %this.server = "" @ %idx;
        %idx = (1.0 + %idx);
    }
};
function vurl::doResolveVURL(%this) {
    0;
    %request = new ""() {
        className = ManagerRequest @ "ResolveVURLRequest";
    };
    if (isObject(MissionCleanup)) {
        %request.add();
    }
    %url = $Net::ClientServiceURL @ "/ResolveVURL" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token) @ "&vurl=" @ urlEncode(%this.vurl);
    MissionCleanup;
    log("network", "debug", "ResovleVURLRequest: " @ %url);
    %request.VURLHandler = %this;
    %request.setURL(%url);
    %request.start();
};
function vurl::doReportError(%this, %errorCode, %errorText) {
    if ((0.0 == stricmp(%errorCode, "nomoretry"))) {
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
        %vurl.VURLHandler.schedule(0, "delete");
    }
    if ((0.0 == stricmp(%errorCode, "offline"))) {
        if ((0.0 == stricmp(%vurl.targetType, "user"))) {
            %errorCode = "USEROFFLINE";
        }
        if ((0.0 == stricmp(%vurl.targetType, "apartment"))) {
            %errorCode = "NOSPACE";
        }
    }
    %errorMessage = ;
    handleSystemMessage("msgInfoMessage", %errorMessage);
    if (geTGF.isVisible()) {
    }
    if ((WorldMap == %vurl.loggedIn)) {
        MessageBoxOK("Whoa!", %errorMessage, "");
    }
};
function vurl::DefaultRequestPassword(%this) {
    MessageBoxTextEntryWithCancel(, , "", 0);
    $VURL::saveVurlForPasswordCheck = %this.vurl;
    VURL_ResumbmitWithPassword;
};
function VURL_ResumbmitWithPassword(%newPassword) {
    %vurl = vurlGetParsedVurl($VURL::saveVurlForPasswordCheck);
    %vurl.setPassword(%newPassword);
    %vurl.execute();
};
function vurl::handleIncompeteVURL(%this) {
    if ((0.0 == stricmp(%this.targetType, "location"))) {
        if (isObject(WorldMap)) {
            log("network", "info", "Showing map for city \"" @ %this.targetCity @ "\"");
            "Map".openToTabName();
            %this.targetCity.selectCity();
        }
    }
    if ((0.0 == stricmp(%this.targetType, "apartment"))) {
        log("network", "warn", "Should show directory for building \"" @ %this.targetCity @ "\" here");
    }
};
function ResolveVURLRequest::onDone(%this) {
    echo(getScopeName());
    %status = findRequestStatus(%this);
    log("network", "debug", "ResolveVURLRequest status: " @ %status);
    if ((0.0 == stricmp(%status, "success"))) {
        %this.vurl = %this.getValue("vurl");
        log("network", "debug", "ResolveVURLRequest returned resolved VURL =" @ " " @ %this.vurl);
        %vurl = %this.VURLHandler;
        if (%vurl.setVURL(%this.vurl)) {
            if ((0.0 == %vurl.execute())) {
                log("network", "error", "unable to execute VURL vurl=" @ " " @ %this.vurl);
            }
        }
        log("network", "error", "Unable to parse returned VURL");
    }
    echo(getScopeName() @ "->" @ %status);
    %statusMsg = %this.getValue("statusMsg");
    %errorCode = %this.getValue("errorCode");
    %this.VURLHandler.doReportError(%errorCode, %statusMsg);
    %this.VURLHandler.schedule(0, "delete");
    %this.schedule(0, "delete");
};
function ResolveVURLRequest::onError(%this, %unused, %errMsg) {
    log("network", "debug", "ResolveVURLRequest::onError: " @ %errMsg);
    %this.VURLHandler.error = 1;
    %this.schedule(0, "delete");
    if (isObject($VURL::curVURL)) {
    }
    if ((%this.VURLHandler.getId() == $VURL::curVURL.getId())) {
        $VURL::curVURL.delete();
        $VURL::curVURL = 0;
    }
};
function vurl::reorderServerList(%this) {
    if ($Server::Dedicated) {
        return;
    }
    %idxSwapout = 0;
    if (!(%idxSwapout @ " " @ %this._server $= "")) {
        if ((0.0 @ %idxSwapout == stricmp($ServerName, %this._server))) {
        }
        %idxSwapout = (1.0 + %idxSwapout);
    }
    if ((!(%idxSwapout @ " " @ %this._server $= "") @ %idxSwapout @ " " @ %this._server $= "")) {
    }
    if ((0.0 == %idxSwapout)) {
        return;
    }
    %idx = %idxSwapout;
    if ((0.0 > %idx)) {
        %this._server = (1.0 - %idx) @ %this._server @ %idx;
        %idx = (1.0 - %idx);
    }
    %this._server = (0.0 > %idx) @ $ServerName @ 0;
};
function vurlOperation(%line, %ignoreDownloadStatus) {
    if (!(isDefined("%ignoreDownloadStatus"))) {
        %ignoreDownloadStatus = 0;
    }
    log("network", "debug", "vurlOperation, vurl=\"" @ %line @ "\"");
    %vurl = new ""();;
    ScriptObject;
    %vurl.bindClassName("VURL");
    %vurl.setIgnoreDownloadStatus(%ignoreDownloadStatus);
    if (%vurl.setVURL(%line)) {
        if ((0.0 == %vurl.execute())) {
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
    %vurl = new ""();;
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
    0;
    %theVurl = new ""() {
        class = ScriptObject @ "VURL";
    };
    if (%theVurl.setVURL(%aVurlString)) {
        log("login", "debug", getScopeName() @ " " @ "- parsed VURL");
        return %theVurl;
    }
    log("login", "error", getScopeName() @ " " @ "-Unable to set and parse VURL");
    %theVurl.delete();
    return 0;
};
