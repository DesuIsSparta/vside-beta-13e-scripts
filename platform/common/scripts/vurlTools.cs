$VURL::curVURL = "";
function vurl::isResolvedVURL(%this)
{
    return !(%this._server[0] $= "");
}
function vurl::isParsed(%this)
{
    return %this.isParsed;
}
function vurl::parse(%this)
{
    %payload = NextToken(%this.vurl, "protocol", ":");
    if ((stricmp(%protocol, "vside") != 0) && ("" $= %payload))
    {
        %errorText = "VURL::parse attempting to determine protocol and paylod. %this.vurl =" SPC %this.vurl;
        %this.doReportError("parseError", %errorText);
        return 0;
    }
    %this.protocol = %protocol;
    %parameters = NextToken(%payload, "target", "?");
    %parameters = strreplace(%parameters, "?", "&");
    if (getSubStr(%target, 0, 1) $= "/")
    {
        %target = getSubStr(%target, 1, strlen(%target) - 1);
    }
    %targetpath = NextToken(%target, "targettype", "/");
    strlwr(%targettype);
    if (((stricmp(%targettype, "location") != 0) && (stricmp(%targettype, "user") != 0)) && (stricmp(%targettype, "apartment") != 0))
    {
        %errorText = "VURL::parse unknown type type in vurl =" SPC %this.vurl;
        %this.doReportError("parseError", %errorText);
        return 0;
    }
    %this.targetType = %targettype;
    %this.targetPath = %targetpath;
    %this.isIncomplete = 0;
    %this.isRawTransform = 0;
    if ((stricmp(%this.targetType, "location") == 0) && (stricmp(%this.targetType, "apartment") == 0))
    {
        %targetDest = NextToken(%targetpath, "city", "/");
        %this.targetDest = urlDecode(%targetDest);
        %this.targetCity = urlDecode(%city);
        if (%this.targetDest $= "")
        {
            %this.isResolved = 1;
            %this.isIncomplete = 1;
            %this.isParsed = 1;
            %this.reconstructVURL();
            return 1;
        }
        else
        {
            if (stricmp(%this.targetType, "location") == 0)
            {
                %testTargetDest = strreplace(%this.targetDest, ",", " ");
                %wordCount = getWordCount(%testTargetDest);
                if ((%wordCount == 3) && (%wordCount == 7))
                {
                    if (($Server::Dedicated == 1) && isObjectAndHasPermission_NoWarn($player, "freeVURLTransform"))
                    {
                        %this.targetDest = %testTargetDest;
                        %this.isRawTransform = 1;
                    }
                }
                if ((%wordCount != 1) && (%this.isRawTransform == 0))
                {
                    %errorText = "Invalid number of parameters in target";
                    %this.doReportError("parseError", %errorText);
                    return 0;
                }
            }
        }
    }
    while (!(%parameters $= ""))
    {
        %parameters = NextToken(%parameters, "param", "&");
        %value = urlDecode(NextToken(%param, "name", "="));
        %name = strlwr(%name);
        %this._[urlDecode(%name)] = %value;
    }
    %this.isResolved = !(%this._server[0] $= "");
    if (%this.isResolved)
    {
        %this.reorderServerList();
        %this.retryIndex = 0;
    }
    %this.isParsed = 1;
    %this.reconstructVURL();
    return 1;
}
function vurl::reconstructVURL(%this)
{
    if (!%this.isParsed)
    {
        return 0;
    }
    %newVurl = "vside:/" @ %this.targetType @ "/" @ %this.targetPath;
    %paramCount = 0;
    if (!(%this._key $= ""))
    {
        %newVurl = %newVurl @ %paramCount == 0 ? "?" : "&";
        %paramCount = %paramCount + 1;
        %newVurl = %newVurl @ "key=" @ urlEncode(%this._key);
    }
    %retryServer = 0;
    while (!(%this._server[%retryServer] $= ""))
    {
        %newVurl = %newVurl @ %paramCount == 0 ? "?" : "&";
        %paramCount = %paramCount + 1;
        %newVurl = %newVurl @ "server" @ %retryServer @ "=" @ urlEncode(%this._server[%retryServer]);
        %retryServer = %retryServer + 1;
    }
    %this.vurl = %newVurl;
    log("network", "debug", "Reconstructed VURL=\"" @ %this.vurl @ "\"");
    return 1;
}
function vurl::tryProcessDynamicVurl(%this)
{
    if (firstWord(%this.vurl) $= "dynamic")
    {
        %dvurlType = getWord(%this.vurl, 1);
        %newVurl = "";
        if (%dvurlType $= "partnerSpawn")
        {
            if (!$AmClient)
            {
                error(getScopeName() SPC "- type only valid on client:" SPC %this.vurl SPC getTrace());
                return 0;
            }
            %partnerObj = gLoginPartnersInfo.getPartnerObj($Net::userOwner);
            %newVurl = %partnerObj.vurl;
        }
        else
        {
            error(getScopeName() SPC "- Unknown dynamic vurl type:" SPC %this.vurl SPC getTrace());
            return 0;
        }
        echo(getScopeName() SPC "- converting dynamic vurl \"" @ %this.vurl @ "\" to \"" @ %newVurl @ "\".");
        %this.vurl = %newVurl;
    }
    return ;
}
function vurl::setVURL(%this, %vurl)
{
    %this.isParsed = 0;
    %this.isResolved = 0;
    %this.vurl = %vurl;
    %this.tryProcessDynamicVurl();
    return %this.parse();
}
function vurl::setPassword(%this, %password)
{
    %this._key = %password;
    %this.reconstructVURL();
    return ;
}
function vurl::setIgnoreDownloadStatus(%this, %val)
{
    %this.ignoreDownloadStatus = %val;
    return ;
}
function vurl::execute(%this)
{
    if ((%this.retryIndex == 0) && testFlooding($player, "teleport", 1))
    {
        log("network", "warn", "Teleport Flooding");
        %errorText = "";
        %this.doReportError("FLOOD", %errorText);
        return 0;
    }
    if (isObject($VURL::curVURL) && ($VURL::curVURL.getId() != %this.getId()))
    {
        log("network", "warn", "Pending VURL execution being overridden");
        $VURL::curVURL.schedule(0, "delete");
    }
    $VURL::curVURL = %this;
    log("network", "debug", "executing VURL =" SPC %this.vurl);
    if (!%this.isParsed)
    {
        %errorText = "Attempting to execute unparsed VURL =" SPC %this.vurl;
        %this.doReportError("EXECUTEERROR", %errorText);
        return 0;
    }
    if (stricmp(%this.targetType, "user") == 0)
    {
        if (stricmp(%this.targetPath, $Player::Name) == 0)
        {
            %errorText = "Teleporting to yourself?";
            %this.doReportError("TELETOSELF", "");
            return 0;
        }
        else
        {
            if (isObject(ServerConnection) && isNPCName(%this.targetPath))
            {
                %this.doReportSuccessExpected();
                commandToServer('TeleportToPlayer', %this.targetPath);
                return 1;
            }
        }
    }
    if (!(%this.isResolved) && !$StandAlone)
    {
        log("network", "info", "Unresolved VURL execution. Processing resolution request first");
        %this.doResolveVURL();
        return 1;
    }
    if (%this.isIncomplete)
    {
        log("network", "debug", "Handling incomplete VURL");
        %this.handleIncompeteVURL();
        %this.schedule(0, "delete");
        return 1;
    }
    if ((((%this._server[%this.retryIndex] $= "") && !$StandAlone) || $StandAlone) && !((%this.standAloneRetry $= "")))
    {
        %this.doReportError("nomoreretry", "");
        return 0;
    }
    else
    {
        if ($StandAlone)
        {
            %this.standAloneRetry = 1;
        }
    }
    %cityName = %this.getCityFromServerName(%this._server[%this.retryIndex]);
    if (isObject(WorldMapCityInfoMap) && isObject(LoadingGui))
    {
        %cityInfo = WorldMapCityInfoMap.get(%cityName);
        LoadingGui.setBitmap(%cityInfo.background);
    }
    if (%this.checkCityDownloadStatus(%cityName) && !(%this.ignoreDownloadStatus))
    {
        %this.doReportError("downloading", "");
        return 0;
    }
    %this.doReportSuccessExpected();
    if (((stricmp(%this.targetType, "location") == 0) || (stricmp(%this.targetType, "apartment") == 0)) || (stricmp(%this.targetType, "user") == 0))
    {
        if ($StandAlone)
        {
            commandToServer('TeleportToVURL', %this.vurl);
        }
        else
        {
            %serverDest = %this._server[%this.retryIndex];
            %this.retryIndex = %this.retryIndex + 1;
            SetTransition(%serverDest, %this.vurl);
        }
    }
    return 1;
}
function vurl::clearResolutionAndExecute(%this)
{
    %this.clearResolution();
    %this.execute();
    return ;
}
function vurl::getCityFromServerName(%this, %ServerName)
{
    %idx = 0;
    while (%idx < WorldMapServers.getCount())
    {
        %serverProps = WorldMapServers.getObject(%idx);
        %testname = %serverProps.get("name");
        if (%testname $= %ServerName)
        {
            %cityspec = %serverProps.get("city");
            %citybuilding = strreplace(%cityspec, "_", " ");
            %cityName = firstWord(%citybuilding);
            echo("cityspec - " @ %cityspec @ " becomes city name - " @ %cityName);
            return %cityName;
        }
        %idx = %idx + 1;
    }
    return "";
}
function vurl::checkCityDownloadStatus(%this, %cityName)
{
    if (isObject(packageDownload) && $AutoDownloadPackages)
    {
        %status = packageDownload.getStatusForCity(%cityName);
        if (%status $= "done")
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
    return 0;
}
function vurl::clearResolution(%this)
{
    %this.isResolved = 0;
    %idx = 0;
    while (!(%this._server[%idx] $= ""))
    {
        %this.server[%idx] = "";
        %idx = %idx + 1;
    }
}

function vurl::doResolveVURL(%this)
{
    %request = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %url = $Net::ClientServiceURL @ "/ResolveVURL" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token) @ "&vurl=" @ urlEncode(%this.vurl);
    log("network", "debug", "ResovleVURLRequest: " @ %url);
    %request.VURLHandler = %this;
    %request.setURL(%url);
    %request.start();
    return ;
}
function vurl::doReportError(%this, %errorCode, %errorText)
{
    if (stricmp(%errorCode, "nomoretry") == 0)
    {
        if (!(%this.lastError $= ""))
        {
            %errorCode = %this.lastErrorCode;
        }
    }
    else
    {
        %this.lastError = %errorCode;
    }
    %handled = 0;
    log("network", "info", "VURL Error code=\"" @ %errorCode @ "\" text=\"" @ %errorText @ "\"");
    if (!(%this.cbReportError $= ""))
    {
        %cmd = "%handled = " @ %this.cbReportError @ "( \"" @ %this.vurl @ "\", \"" @ %errorCode @ "\", \"" @ %errorText @ "\");";
        log("network", "debug", "Executing callback cbReportError = \"" @ %cmd @ "\"");
        eval(%cmd);
    }
    if (!%handled)
    {
        vurl::DefaultReportError(%this, %errorCode, %errorText);
    }
    return ;
}
function vurl::doReportSuccessExpected(%this)
{
    if (geTGF.isVisible())
    {
        geTGF.doreopen = 1;
    }
    geTGF.closeFully();
    log("network", "info", "VURL Teleport success expected. VURL=" @ %this.vurl);
    if (!(%this.cbSuccessExpected $= ""))
    {
        %cmd = %this.cbSuccessExpected @ "( \"" @ %this.vurl @ "\" );";
        log("network", "debug", "Executing callback cbSuccessExpected = \"" @ %cmd @ "\"");
        eval(%cmd);
    }
    return ;
}
function vurl::doReportSuccess(%this)
{
    log("network", "info", "VURL Teleport successful. VURL=" @ %this.vurl);
    if (!(%this.cbSuccess $= ""))
    {
        %cmd = %this.cbSuccess @ "( \"" @ %this.vurl @ "\" );";
        log("network", "debug", "Executing callback cbSuccess = \"" @ %cmd @ "\"");
        eval(%cmd);
    }
    return ;
}
function vurl::doRequestPassword(%this)
{
    %this.DefaultRequestPassword();
    return ;
}
function vurl::DefaultReportError(%vurl, %errorCode, %errorText)
{
    log("network", "error", "VURL Errorcode = \"" @ %errorCode @ "\" ErrorMessage = \"" @ %errorText @ "\"");
    if (%errorCode $= "")
    {
        log("network", "error", "empty VURL Errorcode.");
        return ;
    }
    if ((stricmp(%errorCode, "missingdoorcode") == 0) && (stricmp(%errorCode, "incorrectdoorcode") == 0))
    {
        %vurl.doRequestPassword();
    }
    else
    {
        if (((%errorCode $= "accessDenied") || (errorCode $= "parseError")) && !((%errorText $= "")))
        {
            handleSystemMessage("msgInfoMessage", %errorText);
            %vurl.VURLHandler.schedule(0, "delete");
        }
        else
        {
            if (stricmp(%errorCode, "offline") == 0)
            {
                if (stricmp(%vurl.targetType, "user") == 0)
                {
                    %errorCode = "USEROFFLINE";
                }
                else
                {
                    if (stricmp(%vurl.targetType, "apartment") == 0)
                    {
                        %errorCode = "NOSPACE";
                    }
                }
            }
            %errorMessage = $MsgCat::VURLError["ERROR_" @ strupr(%errorCode)];
            handleSystemMessage("msgInfoMessage", %errorMessage);
            if (geTGF.isVisible() && (WorldMap.loggedIn == 0))
            {
                MessageBoxOK("Whoa!", %errorMessage, "");
            }
        }
    }
    return ;
}
function vurl::DefaultRequestPassword(%this)
{
    MessageBoxTextEntryWithCancel($MsgCat::VURLText["PASSWORD_REQUIRED_TITLE"], $MsgCat::VURLText["PASSWORD_REQUIRED_TEXT"], VURL_ResumbmitWithPassword, "", 0);
    $VURL::saveVurlForPasswordCheck = %this.vurl;
    return ;
}
function VURL_ResumbmitWithPassword(%newPassword)
{
    %vurl = vurlGetParsedVurl($VURL::saveVurlForPasswordCheck);
    %vurl.setPassword(%newPassword);
    %vurl.execute();
    return ;
}
function vurl::handleIncompeteVURL(%this)
{
    if (stricmp(%this.targetType, "location") == 0)
    {
        if (isObject(WorldMap))
        {
            log("network", "info", "Showing map for city \"" @ %this.targetCity @ "\"");
            geTGF.openToTabName("Map");
            WorldMap.selectCity(%this.targetCity);
        }
    }
    else
    {
        if (stricmp(%this.targetType, "apartment") == 0)
        {
            log("network", "warn", "Should show directory for building \"" @ %this.targetCity @ "\" here");
        }
    }
    return ;
}
function ResolveVURLRequest::onDone(%this)
{
    echo(getScopeName());
    %status = findRequestStatus(%this);
    log("network", "debug", "ResolveVURLRequest status: " @ %status);
    if (stricmp(%status, "success") == 0)
    {
        %this.vurl = %this.getValue("vurl");
        log("network", "debug", "ResolveVURLRequest returned resolved VURL =" SPC %this.vurl);
        %vurl = %this.VURLHandler;
        if (%vurl.setVURL(%this.vurl))
        {
            if (%vurl.execute() == 0)
            {
                log("network", "error", "unable to execute VURL vurl=" SPC %this.vurl);
            }
        }
        else
        {
            log("network", "error", "Unable to parse returned VURL");
        }
    }
    else
    {
        echo(getScopeName() @ "->" @ %status);
        %statusMsg = %this.getValue("statusMsg");
        %errorCode = %this.getValue("errorCode");
        %this.VURLHandler.doReportError(%errorCode, %statusMsg);
        %this.VURLHandler.schedule(0, "delete");
    }
    %this.schedule(0, "delete");
    return ;
}
function ResolveVURLRequest::onError(%this, %unused, %errMsg)
{
    log("network", "debug", "ResolveVURLRequest::onError: " @ %errMsg);
    %this.VURLHandler.error = 1;
    %this.schedule(0, "delete");
    if (isObject($VURL::curVURL) && ($VURL::curVURL.getId() == %this.VURLHandler.getId()))
    {
        $VURL::curVURL.delete();
        $VURL::curVURL = 0;
    }
    return ;
}
function vurl::reorderServerList(%this)
{
    if ($Server::Dedicated)
    {
        return ;
    }
    %idxSwapout = 0;
    while (!(%this._server[%idxSwapout] $= ""))
    {
        if (stricmp($ServerName, %this._server[%idxSwapout]) == 0)
        {
            continue;
        }
        %idxSwapout = %idxSwapout + 1;
    }
    if ((%this._server[%idxSwapout] $= "") && (%idxSwapout == 0))
    {
        return ;
    }
    %idx = %idxSwapout;
    while (%idx > 0)
    {
        %this._server[%idx] = %this._server[(%idx - 1)];
        %idx = %idx - 1;
    }
    %this._server[0] = $ServerName;
    return ;
}
function vurlOperation(%line, %ignoreDownloadStatus)
{
    if (!isDefined("%ignoreDownloadStatus"))
    {
        %ignoreDownloadStatus = 0;
    }
    log("network", "debug", "vurlOperation, vurl=\"" @ %line @ "\"");
    %vurl = new ScriptObject();
    %vurl.bindClassName("VURL");
    %vurl.setIgnoreDownloadStatus(%ignoreDownloadStatus);
    if (%vurl.setVURL(%line))
    {
        if (%vurl.execute() == 0)
        {
            log("network", "error", "Unable to execute VURL" SPC %line);
            %vurl.delete();
        }
        else
        {
            CustomSpacesSelector.close();
        }
    }
    else
    {
        log("network", "error", "Unable to set and parse VURL" SPC %line);
        %vurl.delete();
    }
    return ;
}
function vurlClearResolutionAndExecute(%line)
{
    log("network", "debug", "vurlClearResolutionAndExecute, vurl=\"" @ %line @ "\"");
    %vurl = new ScriptObject();
    %vurl.bindClassName("VURL");
    if (%vurl.setVURL(%line))
    {
        %vurl.clearResolution();
        if (%vurl.execute() == 0)
        {
            log("network", "error", "Unable to execute VURL" SPC %line);
            %vurl.delete();
        }
    }
    else
    {
        log("network", "error", "Unable to set and parse VURL" SPC %line);
        %vurl.delete();
    }
    return ;
}
function vurlClearResolution(%line)
{
    %questionMarkIndex = strpos(%line, "?");
    if (%questionMarkIndex != -1)
    {
        return getSubStr(%line, 0, %questionMarkIndex);
    }
    return %line;
}
function vurlGetParsedVurl(%aVurlString)
{
    %theVurl = new ScriptObject();
    if (%theVurl.setVURL(%aVurlString))
    {
        log("login", "debug", getScopeName() SPC "- parsed VURL");
        return %theVurl;
    }
    else
    {
        log("login", "error", getScopeName() SPC "-Unable to set and parse VURL");
        %theVurl.delete();
        return 0;
    }
    return ;
}
