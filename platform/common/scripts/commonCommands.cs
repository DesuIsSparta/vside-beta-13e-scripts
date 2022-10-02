function isPlayerObject(%obj)
{
    return isObject(%obj) && !(!((%obj.getType() & $TypeMasks::PlayerObjectType)));
}
function isAIPlayerObject(%obj)
{
    return isObject(%obj) && !(!((%obj.getClassName() $= "AIPlayer")));
}
function isNPCObject(%obj)
{
    if (!isObject(NPCGroup) && !isAIPlayerObject(%obj))
    {
        return 0;
    }
    return %obj.getGroup().getId() == NPCGroup.getId();
}
function isPlayerCharacter(%obj)
{
    return isObject(%obj.client);
}
function stripColorChars(%line)
{
    return stripChars(%line, "\x10\c0\c1\c2\c3\c4\c5\c6\c7\c8\c9");
}
function reloadScripts()
{
    if ($AmClient)
    {
        exec($userMods @ "/client/ets/init.cs");
    }
    if ($AmServer)
    {
        exec($userMods @ "/server/scripts/ets/init.cs");
    }
    exec($userMods @ "/common/scripts/initReloadable.cs");
    reloadModScripts("");
    initProjectsReloadable();
    initProjectsReloadableLate();
    return ;
}
function gSetField(%object, %name, %value)
{
    $gGlobalFields[%object.getId(),%name] = %value ;
    return ;
}
function gGetField(%object, %name)
{
    return $gGlobalFields[%object.getId(),%name];
}
function gGetFieldWithDefault(%object, %name, %def)
{
    if (!isObject(%object))
    {
        error(getScopeName() SPC "called with bad object!");
        return %def;
    }
    if (isDefined("$gGlobalFields" @ %object.getId() @ "_" @ %name))
    {
        return $gGlobalFields[%object.getId(),%name];
    }
    return %def;
}
function tmpFields(%obj)
{
    %tmps = gGetField(%obj, tmpFields);
    if (!isObject(%tmps))
    {
        %tmps = new ScriptObject(temporaryfields);
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(%tmps);
        }
        gSetField(%obj, tmpFields, %tmps);
    }
    return %tmps;
}
function getDebugString(%obj)
{
    if (!isObject(%obj))
    {
        return "-(" @ %obj SPC "is not an object)-";
    }
    else
    {
        return %obj.getDebugString();
    }
    return ;
}
function makeTaggedString(%plainText)
{
    %cmd = "%ret = \"" @ expandEscape(%plainText) @ "\";";
    eval(%cmd);
    return %ret;
}
function crash()
{
    echo("intentionally crashing.");
    echo(1 % 0);
    return ;
}
function crashDelayed(%ms)
{
    if (%ms $= "")
    {
        %ms = 5000;
    }
    echo("scheduled crash in" SPC %ms / 1000 SPC "seconds..");
    if (%ms > 1000)
    {
        schedule(1000, 0, "crashDelayed", %ms - 1000);
    }
    else
    {
        schedule(%ms, 0, "crash");
    }
    return ;
}
function hasWord(%searchText, %findText)
{
    return findWord(%searchText, %findText) >= 0;
}
function hasField(%searchText, %findText)
{
    return findField(%searchText, %findText) >= 0;
}
function hasRecord(%searchText, %findText)
{
    return findRecord(%searchText, %findText) >= 0;
}
function hasSubString(%searchText, %findText)
{
    return strstr(%searchText, %findText) >= 0;
}
function getSuffixPos(%searchText, %suffix)
{
    %idx = strpos(%searchText, %suffix);
    if (%idx < 0)
    {
        return -1;
    }
    while (%idx >= 0)
    {
        %last = %idx;
        %idx = strpos(%searchText, %suffix, %idx + 1);
    }
    %idx = %last;
    if ((%idx + strlen(%suffix)) != strlen(%searchText))
    {
        return -1;
    }
    return %idx;
}
function hasPrefix(%searchText, %prefix)
{
    %len = strlen(%prefix);
    %ret = getSubStr(%searchText, 0, %len) $= %prefix;
    return %ret;
}
function hasSuffix(%searchText, %suffix)
{
    %ret = getSuffixPos(%searchText, %suffix) >= 0;
    return %ret;
}
function execFilesWithName(%fileName)
{
    %file = findFirstFile(%fileName);
    while (!(%file $= ""))
    {
        exec(%file);
        %file = findNextFile(%fileName);
    }
}

function safeNewScriptObject(%classname, %objectName, %deleteExisting)
{
    if (%deleteExisting && isObject(%objectName))
    {
        %objectName.delete();
    }
    eval("%ret = new " @ %classname @ "(" @ %objectName @ ");");
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%ret);
    }
    return %ret;
}
function safeEnsureScriptObject(%classname, %objectName)
{
    return safeEnsureScriptObjectWithClassBindingsAndInit(%classname, %objectName, "", "");
}
function safeEnsureScriptObjectWithClassBindings(%classname, %objectName, %classesToBind)
{
    return safeEnsureScriptObjectWithClassBindingsAndInit(%classname, %objectName, %classesToBind, "");
}
function safeEnsureScriptObjectWithInit(%classname, %objectName, %datablock)
{
    return safeEnsureScriptObjectWithClassBindingsAndInit(%classname, %objectName, "", %datablock);
}
function safeEnsureScriptObjectWithClassBindingsAndInit(%classname, %objectName, %classesToBind, %datablock)
{
    if (isObject(%objectName))
    {
        return %objectName.getId();
    }
    %classesToBind = trim(%classesToBind);
    if (%classesToBind $= "")
    {
        %cmd = "%ret = new " @ %classname @ "(" @ %objectName @ ")";
        if (!(%datablock $= ""))
        {
            %cmd = %cmd SPC %datablock;
        }
        %cmd = %cmd @ ";";
        eval(%cmd);
    }
    else
    {
        %cmd = "%ret = new " @ %classname @ "()";
        if (!(%datablock $= ""))
        {
            %cmd = %cmd SPC %datablock;
        }
        %cmd = %cmd @ ";";
        eval(%cmd);
        if (!(%classesToBind $= ""))
        {
            %classCount = getWordCount(%classesToBind);
            %i = 0;
            while (%i < %classCount)
            {
                %ret.bindClassName(getWord(%classesToBind, %i));
                %i = %i + 1;
            }
        }
        %ret.setName(%objectName);
    }
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%ret);
    }
    return %ret;
}
$gValidTextureExt = ".dbm .dbmc .jpg .png";
function getPathOfButtonResource(%res)
{
    %cached = getCachedResourcePath(%res);
    if (!(%cached $= ""))
    {
        return %cached;
    }
    %n = getWordCount($gValidTextureExt) - 1;
    while (%n >= 0)
    {
        %ext = getWord($gValidTextureExt, %n);
        if (isFile(%res @ %ext) && isFile(%res @ "_n" @ %ext))
        {
            setCachedResourcePath(%res, %res);
            return %res;
        }
        %n = %n - 1;
    }
    return "";
}
function getPathsMatchingPattern(%pattern)
{
    %ret = findFirstFile(%pattern);
    if (!(%ret $= ""))
    {
        while (1)
        {
            %next = findNextFile(%pattern);
            if (%next $= "")
            {
                continue;
            }
            %ret = %ret TAB %next;
        }
    }
    return %ret;
}
function getCachedResourcePath(%res)
{
    safeEnsureScriptObject("StringMap", "ResourcePathMap");
    %path = ResourcePathMap.get(%res);
    return %path;
}
function setCachedResourcePath(%res, %path)
{
    safeEnsureScriptObject("StringMap", "ResourcePathMap");
    ResourcePathMap.put(%res, %path);
    return ;
}
function setAllLogLevels(%level)
{
    Console.setAllLogLevels(%level);
    log.setAllLogLevels(%level);
    setConsoleLogLevel(%level);
    return ;
}
function bitstreamCountToggle()
{
    setAllLogLevels("debug");
    $bitStreamCount = !$bitStreamCount;
    return ;
}
function getPlayerMarkup(%player, %color, %isNameNotObject)
{
    %playerName = "";
    if (!%isNameNotObject && (%isNameNotObject $= ""))
    {
        %playerName = %player.getShapeName();
    }
    if (%playerName $= "")
    {
        %playerName = %player;
        %player = "";
    }
    %result = "<spush>";
    if (((%color $= "") && isObject($player)) && (%playerName $= $player.getShapeName()))
    {
        %color = "4600a0ff";
    }
    if (!((%player $= "")) && %player.isIgnore())
    {
        %result = %result @ "<linkcolor:00000080>";
    }
    else
    {
        if (!(%color $= ""))
        {
            %result = %result @ makeLinkColorTag(%color);
        }
    }
    %botString = "";
    if (isObject(PlayerInstanceDict))
    {
        %playerObj = Player::findPlayerInstance(%playerName);
        if (isObject(%playerObj) && %playerObj.isClassAIPlayer())
        {
            %botString = " (bot)";
        }
    }
    %name = %playerName;
    %result = %result @ "<a:gamelink " @ munge(%name) @ ">" @ StripMLControlChars(%name) @ %botString @ "</a><spop>";
    return %result;
}
function SegmentList(%masterList, %delimiter, %segmentDelimiter, %segmentSize)
{
    if (%delimiter $= "")
    {
        error(getScopeName() @ "->delimiter argument unspecified!");
        return ;
    }
    if (%segmentSize $= "")
    {
        error(getScopeName() @ "->segmentSize argument unspecified!");
        return ;
    }
    if (%segmentDelimiter $= "")
    {
        error(getScopeName() @ "->segmentDelimiter argument unspecified!");
        return ;
    }
    %outString = "";
    %idx = 0;
    %len = strlen(%masterList);
    while (%idx < %len)
    {
        %segStart = %idx;
        %lastGoodIdx = %len;
        if ((%len - %segStart) > %segmentSize)
        {
            while ((%idx = strpos(%masterList, %delimiter, %idx) - %segStart) < %segmentSize)
            {
                if (%idx < 0)
                {
                    continue;
                }
                %lastGoodIdx = %idx;
                %idx = %idx + 1;
            }
        }
        %currentList = getSubStr(%masterList, %segStart, %lastGoodIdx - %segStart);
        %idx = %lastGoodIdx + 1;
        if (!(%outString $= ""))
        {
            %outString = %outString @ %segmentDelimiter @ %currentList;
        }
        else
        {
            %outString = %currentList;
        }
    }
    return %outString;
}
function SimSet::getByField(%this, %field, %svalue)
{
    %n = %this.getCount() - 1;
    while (%n >= 0)
    {
        %obj = %this.getObject(%n);
        %evalString = "return %obj." @ %field @ " $= \"" @ %svalue @ "\";";
        if (eval(%evalString))
        {
            return %obj;
        }
        %n = %n - 1;
    }
    return "";
}
function GuiControl::getChildrenInOrder(%this, %children)
{
    %ids = "";
    %count = getWordCount(%children);
    %i = 0;
    while (%i < %count)
    {
        %child = getWord(%children, %i);
        if (isObject(%child))
        {
            %ids = %ids SPC %child.getId();
        }
        %i = %i + 1;
    }
    %ids = trim(%ids);
    %toReturn = "";
    %count = %this.getCount();
    %i = 0;
    while (%i < %count)
    {
        %child = %this.getObject(%i);
        if (hasWord(%ids, %child))
        {
            %toReturn = %toReturn SPC %child;
        }
        %i = %i + 1;
    }
    return trim(%toReturn);
}
function logOnce(%logSystems, %logLevel, %key, %msg)
{
    %key = %logLevel SPC getScopeName(1) @ "_" @ %key;
    %map = safeEnsureScriptObject("StringMap", "messageCountsErrors");
    %count = %map.get(%key);
    if (%count $= "")
    {
        log(%logSystems, %logLevel, %msg);
    }
    else
    {
        if (%count == 1)
        {
            log(%logSystems, %logLevel, "multiple log messages for:" SPC %key SPC "- swallowing the remainder." SPC %msg);
        }
    }
    %count = %count + 1;
    %map.put(%key, %count);
    return ;
}
function debugOnce(%key, %msg)
{
    %key = getScopeName(1) @ "_" @ %key;
    logOnce("general", "debug", %key, %msg);
    return ;
}
function echoOnce(%key, %msg)
{
    %key = getScopeName(1) @ "_" @ %key;
    logOnce("general", "info", %key, %msg);
    return ;
}
function warnOnce(%key, %msg)
{
    %key = getScopeName(1) @ "_" @ %key;
    logOnce("general", "warn", %key, %msg);
    return ;
}
function errorOnce(%key, %msg)
{
    %key = getScopeName(1) @ "_" @ %key;
    logOnce("general", "error", %key, %msg);
    return ;
}
$gValidObjectNameChars = "abcdefghijklmnopqrstuvwxyz" @ "ABCDEFGHIJKLMNOPQRSTUVWXYZ" @ "0123456789-_";
function stripForObjectName(%dry)
{
    return stripString(%dry, $gValidObjectNameChars, "_");
}
function getExtension(%dry)
{
    %wet = %dry;
    %wet = strrchr(%wet, "/");
    %wet = %wet $= "" ? %dry : %wet;
    %wet2 = %wet;
    %wet = strrchr(%wet, "?");
    %wet = %wet $= "" ? %wet2 : %wet;
    %wet2 = %wet;
    %wet = strrchr(%wet, "&");
    %wet = %wet $= "" ? %wet2 : %wet;
    %wet2 = %wet;
    %wet = strrchr(%wet, "=");
    %wet = %wet $= "" ? %wet2 : %wet;
    %wet2 = %wet;
    %wet = strrchr(%wet2, ".");
    return %wet;
}
function stripExtension(%dry)
{
    %ext = getExtension(%dry);
    %wet = getSubStr(%dry, 0, strlen(%dry) - strlen(%ext));
    return %wet;
}
function commaify(%num)
{
    if (%num == 0)
    {
        return 0;
    }
    %sign = "";
    if (getSubStr(%num, 0, 1) $= "-")
    {
        %sign = "-";
        %num = getSubStr(%num, 1);
    }
    %result = "";
    %len = strlen(%num);
    while (!(%num $= ""))
    {
        if (%len >= 3)
        {
            %segment = getSubStr(%num, %len - 3, 3);
            %num = getSubStr(%num, 0, %len - 3);
        }
        else
        {
            %segment = %num;
            %num = "";
        }
        if (%result $= "")
        {
            %result = %segment;
        }
        else
        {
            %result = %segment @ "," @ %result;
        }
        %len = %len - 3;
    }
    return %sign @ %result;
}
