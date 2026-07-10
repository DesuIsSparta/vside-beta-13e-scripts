safeEnsureScriptObjectWithInit("StringMap", "PlayerInfoMap", "{ ignoreCase = true; }");
function PlayerInfoMap::addPlayerInfo(%this, %playerName, %age, %gender, %location, %hereToSee, %tags, %affinity, %respekt, %respektRank) {
    if ((%playerName $= "")) {
        return 0;
    }
    if ((-(1.0) != %this.findKey(%playerName))) {
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
    %entry = %playerName.get();
    UserListFriends;
    if (isObject(%entry)) {
        %playerInfo.activities = %entry.activities;
    }
    %this.put(%playerName, %playerInfo.getId());
    if (!(%playerName $= $Player::Name)) {
        %playerInstance = Player::findPlayerInstance(%playerName);
    }
    return %playerInfo;
};
function PlayerInfoMap::removePlayerInfo(%this, %playerName) {
    if ((%playerName $= "")) {
        return;
    }
    if ((-(1.0) == %this.findKey(%playerName))) {
        return;
    }
    %this.get(%playerName).delete();
    %this.remove(%playerName);
};
function PlayerInfoMap::removeAllInfo(%this) {
    %size = %this.size();
    %i = 0;
    if ((%size < %i)) {
        %this.getValue(%i).delete();
        %i = (1.0 + %i);
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
    if (1) {
        %player = containerSearchNext(1);
        if (!(isObject(%player))) {
        }
        if (($player.getId() != %player.getId())) {
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
    %request.setURL(%url);
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
        %numUsers = %this.getValue("numUsers");
        $ETS::PlayerInfo::NoTags = %this.getValue("notags");
        %failedPlayers = %this.askedForPlayers;
        %i = 0;
        if ((%numUsers < %i)) {
            %name = %this.getValue("proximalPlayers" @ %i @ ".userName");
            %age = %this.getValue("proximalPlayers" @ %i @ ".age");
            %gender = %this.getValue("proximalPlayers" @ %i @ ".gender");
            %location = %this.getValue("proximalPlayers" @ %i @ ".location");
            %hereToSee = %this.getValue("proximalPlayers" @ %i @ ".hereToSee");
            %tags = %this.getValue("proximalPlayers" @ %i @ ".tags");
            %affinity = %this.getValue("proximalPlayers" @ %i @ ".affinity");
            %respekt = %this.getValue("proximalPlayers" @ %i @ ".respekt");
            %respektRank = %this.getValue("proximalPlayers" @ %i @ ".respektRanking");
            %name.addPlayerInfo(%age, %gender, %location, %hereToSee, %tags, %affinity, %respekt, %respektRank);
            %askedForIndex = findField(%failedPlayers, %name);
            PlayerInfoMap;
            if ((0.0 >= %askedForIndex)) {
                %failedPlayers = removeField(%failedPlayers, %askedForIndex);
            }
            %i = (1.0 + %i);
        }
        %num = getFieldCount(%failedPlayers);
        (%numUsers < %i);
        if ((0.0 > %num)) {
            error("Communication", getScopeName() @ " " @ getDebugString(%this) @ " " @ "- failed to get information for" @ " " @ %num @ " " @ "players:" @ " " @ %failedPlayers);
            %i = 0;
            if ((%num < %i)) {
                %name = getField(%failedPlayers, %i);
                warn("adding null player info for" @ " " @ %name);
                %name.addPlayerInfo("unknown", "unknown", "unknown", "", "", 0, "", "");
                %i = (1.0 + %i);
                PlayerInfoMap;
            }
        }
        if (((%num < %i) @ " " @ %this.callback $= "")) {
            if (isObject(InfoPopupDlg)) {
                InfoPopupDlg.stopAnimation();
            }
            if ((0.0 == %numUsers)) {
                if (!(%this.requestPlayerInfoFor $= "")) {
                    InfoPopupDlg.showPlayerNotFound();
                }
            }
            if (isObject(InfoPopupDlg)) {
                if (!(%this.requestPlayerInfoFor $= "")) {
                }
                if ((PlayerInfoMap @ " " @ %this.requestPlayerInfoFor.get() $= "")) {
                    InfoPopupDlg.showPlayerNotFound();
                }
                InfoPopupDlg.tryShowPlayerInfo();
            }
        }
        if ((0.0 > %numUsers)) {
            %playinfo = %this.requestPlayerInfoFor.get();
            PlayerInfoMap;
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
