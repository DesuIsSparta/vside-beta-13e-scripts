safeEnsureScriptObjectWithInit("StringMap", "PlayerInfoMap", "{ ignoreCase = true; }");
function PlayerInfoMap::addPlayerInfo(%this, %playerName, %age, %gender, %location, %hereToSee, %tags, %affinity, %respekt, %respektRank) {
    if ((%playerName $= "")) {
        return 0;
    }
    if ((%playerName.findKey(%this) != -(1.0))) {
        return 0;
    }
    %playerInfo = safeEnsureScriptObject("ScriptObject", "");
    %playerInfo.age = %age;
    %playerInfo.gender = %gender;
    %playerInfo.location = %location;
    %playerInfo.hereToSee = %hereToSee;
    %playerInfo.tags = %tags;
    %playerInfo.affinity = %affinity;
    %playerInfo.respekt = %respekt;
    %playerInfo.respektRank = %respektRank;
    %playerInfo.activities = "";
    %entry = %playerName.get(UserListFriends);
    if (isObject(%entry)) {
        %playerInfo.activities = %entry.activities;
    }
    %playerInfo.getId().put(%this, %playerName);
    if (!(%playerName $= $Player::Name)) {
        %playerInstance = Player::findPlayerInstance(%playerName);
    }
    return %playerInfo;
};
function PlayerInfoMap::removePlayerInfo(%this, %playerName) {
    if ((%playerName $= "")) {
        return;
    }
    if ((%playerName.findKey(%this) == -(1.0))) {
        return;
    }
    %playerName.get(%this).delete();
    %playerName.remove(%this);
};
function PlayerInfoMap::removeAllInfo(%this) {
    %size = %this.size();
    %i = 0;
    while ((%i < %size)) {
        %i.getValue(%this).delete();
        %i = (%i + 1.0);
    }
    %this.clear();
};
function clientCmdClearPlayerInfoCache() {
    PlayerInfoMap.removeAllInfo();
};
function getPlayerNamesInRadius(%radius) {
    if (!(isObject($player))) {
        return "";
    }
    initContainerRadiusSearch($player.getTransform(), %radius, $TypeMasks::PlayerObjectType, 1);
    %names = "";
    while (1) {
        %player = containerSearchNext(1);
        if (!(isObject(%player))) {
        }
        if ((%player.getId() != $player.getId())) {
            %names = %names @ "\t" @ %player.getShapeName();
        }
    }
    return trim(%names);
};
function requestPlayerInfoFor(%playerName) {
    requestPlayerInfoForWithCallback(%playerName, "", 0);
};
function requestPlayerInfoForWithCallback(%playerName, %callback, %data) {
    if (!(haveValidManagerHost())) {
    }
    if ($StandAlone) {
        return;
    }
    log("communication", "info", "Requesting information for player: " @ %playerName @ " " @ getTrace());
    %request = safeEnsureScriptObject("ManagerRequest", "PlayerInfoRequest");
    if (%request.isOpen()) {
        warn("network", getScopeName() @ " " @ "- got overlapping requests. postponing. url =" @ " " @ %request.getURL());
        return;
    }
    %request.callback = %callback;
    %request.callbackData = %data;
    %url = $Net::ClientServiceURL @ "/getProximalPlayerInfo";
    %user = "user=" @ urlEncode($Player::Name);
    %token = "token=" @ urlEncode($Token);
    %proximalPlayers = "proximalPlayers=" @ urlEncode(%playerName);
    %url = %url @ "?" @ %user;
    %url = %url @ "&" @ %token;
    %url = %url @ "&" @ %proximalPlayers;
    %request.requestPlayerInfoFor = %playerName;
    %request.askedForPlayers = "";
    log("relations", "debug", "requestPlayerInfoFor: " @ %url);
    %url.setURL(%request);
    if (!(haveValidManagerHost())) {
    }
    if (!(haveValidToken())) {
        %request.onDone();
        return;
    }
    %request.start();
};
function PlayerInfoRequest::onError(%this, %errorNum, %errorName) {
    log("network", "warn", getScopeName() @ ": " @ %errorNum @ " " @ %errorName);
    if (isObject(InfoPopupDlg)) {
    }
    if (InfoPopupDlg.isShowing()) {
        InfoPopupDlg.stopAnimation();
    }
    %this.callback = "";
};
function PlayerInfoRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    if ((%status $= "success")) {
        %numUsers = "numUsers".getValue(%this);
        $ETS::PlayerInfo::NoTags = "notags".getValue(%this);
        %failedPlayers = %this.askedForPlayers;
        %i = 0;
        while ((%i < %numUsers)) {
            %name = "proximalPlayers" @ %i @ ".userName".getValue(%this);
            %age = "proximalPlayers" @ %i @ ".age".getValue(%this);
            %gender = "proximalPlayers" @ %i @ ".gender".getValue(%this);
            %location = "proximalPlayers" @ %i @ ".location".getValue(%this);
            %hereToSee = "proximalPlayers" @ %i @ ".hereToSee".getValue(%this);
            %tags = "proximalPlayers" @ %i @ ".tags".getValue(%this);
            %affinity = "proximalPlayers" @ %i @ ".affinity".getValue(%this);
            %respekt = "proximalPlayers" @ %i @ ".respekt".getValue(%this);
            %respektRank = "proximalPlayers" @ %i @ ".respektRanking".getValue(%this);
            %respektRank.addPlayerInfo(PlayerInfoMap, %name, %age, %gender, %location, %hereToSee, %tags, %affinity, %respekt);
            %askedForIndex = findField(%failedPlayers, %name);
            if ((%askedForIndex >= 0.0)) {
                %failedPlayers = removeField(%failedPlayers, %askedForIndex);
            }
            %i = (%i + 1.0);
        }
        %num = getFieldCount(%failedPlayers);
        (%i < %numUsers);
        if ((%num > 0.0)) {
            error("Communication", getScopeName() @ " " @ getDebugString(%this) @ " " @ "- failed to get information for" @ " " @ %num @ " " @ "players:" @ " " @ %failedPlayers);
            %i = 0;
            while ((%i < %num)) {
                %name = getField(%failedPlayers, %i);
                warn("adding null player info for" @ " " @ %name);
                "".addPlayerInfo(PlayerInfoMap, %name, "unknown", "unknown", "unknown", "", "", 0, "");
                %i = (%i + 1.0);
            }
        }
        if (((%i < %num) @ " " @ %this.callback $= "")) {
            if (isObject(InfoPopupDlg)) {
                InfoPopupDlg.stopAnimation();
            }
            if ((%numUsers == 0.0)) {
                if (!(%this.requestPlayerInfoFor $= "")) {
                    InfoPopupDlg.showPlayerNotFound();
                }
            }
            if (isObject(InfoPopupDlg)) {
                if (!(%this.requestPlayerInfoFor $= "")) {
                }
                if ((%this.requestPlayerInfoFor.get(PlayerInfoMap) $= "")) {
                    InfoPopupDlg.showPlayerNotFound();
                }
                InfoPopupDlg.tryShowPlayerInfo();
            }
        }
        if ((%numUsers > 0.0)) {
            %playinfo = %this.requestPlayerInfoFor.get(PlayerInfoMap);
        }
        %playinfo = 0;
        %cmd = %this.callback @ "(" @ %this.requestPlayerInfoFor @ "," @ %playinfo @ "," @ %this.callbackData @ ");";
        eval(%cmd);
    }
    if ((%this.callback $= "")) {
        if (isObject(InfoPopupDlg)) {
        }
        if (InfoPopupDlg.isShowing()) {
            InfoPopupDlg.stopAnimation();
        }
    }
    %cmd = %this.callback @ "(" @ %this.requestPlayerInfoFor @ ",0," @ %this.callbackData @ ");";
    eval(%cmd);
    %this.requestPlayerInfoFor = "";
    %this.callback = "";
};
