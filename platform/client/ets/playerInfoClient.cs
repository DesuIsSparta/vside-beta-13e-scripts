safeEnsureScriptObjectWithInit("StringMap", "PlayerInfoMap", "{ ignoreCase = true; }");
function PlayerInfoMap::addPlayerInfo(%this, %playerName, %age, %gender, %location, %hereToSee, %tags, %affinity, %respekt, %respektRank)
{
    if (%playerName $= "")
    {
        return 0;
    }
    if (%this.findKey(%playerName) != -1)
    {
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
    %entry = UserListFriends.get(%playerName);
    if (isObject(%entry))
    {
        %playerInfo.activities = %entry.activities;
    }
    %this.put(%playerName, %playerInfo.getId());
    if (!(%playerName $= $Player::Name))
    {
        %playerInstance = Player::findPlayerInstance(%playerName);
        if (isObject(%playerInstance))
        {
        }
    }
    return %playerInfo;
}
function PlayerInfoMap::removePlayerInfo(%this, %playerName)
{
    if (%playerName $= "")
    {
        return ;
    }
    if (%this.findKey(%playerName) == -1)
    {
        return ;
    }
    %this.get(%playerName).delete();
    %this.remove(%playerName);
    return ;
}
function PlayerInfoMap::removeAllInfo(%this)
{
    %size = %this.size();
    %i = 0;
    while (%i < %size)
    {
        %this.getValue(%i).delete();
        %i = %i + 1;
    }
    %this.clear();
    return ;
}
function clientCmdClearPlayerInfoCache()
{
    PlayerInfoMap.removeAllInfo();
    return ;
}
function getPlayerNamesInRadius(%radius)
{
    if (!isObject($player))
    {
        return "";
    }
    initContainerRadiusSearch($player.getTransform(), %radius, $TypeMasks::PlayerObjectType, 1);
    %names = "";
    while (1)
    {
        %player = containerSearchNext(1);
        if (!isObject(%player))
        {
            continue;
        }
        if (%player.getId() != $player.getId())
        {
            %names = %names TAB %player.getShapeName();
        }
    }
    return trim(%names);
}
function requestPlayerInfoFor(%playerName)
{
    requestPlayerInfoForWithCallback(%playerName, "", 0);
    return ;
}
function requestPlayerInfoForWithCallback(%playerName, %callback, %data)
{
    if (!haveValidManagerHost() && $StandAlone)
    {
        return ;
    }
    log("communication", "info", "Requesting information for player: " @ %playerName SPC getTrace());
    %request = safeEnsureScriptObject("ManagerRequest", "PlayerInfoRequest");
    if (%request.isOpen())
    {
        warn("network", getScopeName() SPC "- got overlapping requests. postponing. url =" SPC %request.getURL());
        return ;
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
    if (!haveValidManagerHost() && !haveValidToken())
    {
        %request.onDone();
        return ;
    }
    %request.start();
    return ;
}
function PlayerInfoRequest::onError(%this, %errorNum, %errorName)
{
    log("network", "warn", getScopeName() @ ": " @ %errorNum SPC %errorName);
    if (isObject(InfoPopupDlg) && InfoPopupDlg.isShowing())
    {
        InfoPopupDlg.stopAnimation();
    }
    %this.callback = "";
    return ;
}
function PlayerInfoRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    if (%status $= "success")
    {
        %numUsers = %this.getValue("numUsers");
        $ETS::PlayerInfo::NoTags = %this.getValue("notags");
        %failedPlayers = %this.askedForPlayers;
        %i = 0;
        while (%i < %numUsers)
        {
            %name = %this.getValue("proximalPlayers" @ %i @ ".userName");
            %age = %this.getValue("proximalPlayers" @ %i @ ".age");
            %gender = %this.getValue("proximalPlayers" @ %i @ ".gender");
            %location = %this.getValue("proximalPlayers" @ %i @ ".location");
            %hereToSee = %this.getValue("proximalPlayers" @ %i @ ".hereToSee");
            %tags = %this.getValue("proximalPlayers" @ %i @ ".tags");
            %affinity = %this.getValue("proximalPlayers" @ %i @ ".affinity");
            %respekt = %this.getValue("proximalPlayers" @ %i @ ".respekt");
            %respektRank = %this.getValue("proximalPlayers" @ %i @ ".respektRanking");
            PlayerInfoMap.addPlayerInfo(%name, %age, %gender, %location, %hereToSee, %tags, %affinity, %respekt, %respektRank);
            %askedForIndex = findField(%failedPlayers, %name);
            if (%askedForIndex >= 0)
            {
                %failedPlayers = removeField(%failedPlayers, %askedForIndex);
            }
            %i = %i + 1;
        }
        %num = getFieldCount(%failedPlayers);
        if (%num > 0)
        {
            error("Communication", getScopeName() SPC getDebugString(%this) SPC "- failed to get information for" SPC %num SPC "players:" SPC %failedPlayers);
            %i = 0;
            while (%i < %num)
            {
                %name = getField(%failedPlayers, %i);
                warn("adding null player info for" SPC %name);
                PlayerInfoMap.addPlayerInfo(%name, "unknown", "unknown", "unknown", "", "", 0, "", "");
                %i = %i + 1;
            }
        }
        if (%this.callback $= "")
        {
            if (isObject(InfoPopupDlg))
            {
                InfoPopupDlg.stopAnimation();
            }
            if (%numUsers == 0)
            {
                if (!(%this.requestPlayerInfoFor $= ""))
                {
                    InfoPopupDlg.showPlayerNotFound();
                }
            }
            else
            {
                if (isObject(InfoPopupDlg))
                {
                    if (!((%this.requestPlayerInfoFor $= "")) && (PlayerInfoMap.get(%this.requestPlayerInfoFor) $= ""))
                    {
                        InfoPopupDlg.showPlayerNotFound();
                    }
                    else
                    {
                        InfoPopupDlg.tryShowPlayerInfo();
                    }
                }
            }
        }
        else
        {
            if (%numUsers > 0)
            {
                %playinfo = PlayerInfoMap.get(%this.requestPlayerInfoFor);
            }
            else
            {
                %playinfo = 0;
            }
            %cmd = %this.callback @ "(" @ %this.requestPlayerInfoFor @ "," @ %playinfo @ "," @ %this.callbackData @ ");";
            eval(%cmd);
        }
    }
    else
    {
        if (%this.callback $= "")
        {
            if (isObject(InfoPopupDlg) && InfoPopupDlg.isShowing())
            {
                InfoPopupDlg.stopAnimation();
            }
        }
        else
        {
            %cmd = %this.callback @ "(" @ %this.requestPlayerInfoFor @ ",0," @ %this.callbackData @ ");";
            eval(%cmd);
        }
    }
    %this.requestPlayerInfoFor = "";
    %this.callback = "";
    return ;
}
