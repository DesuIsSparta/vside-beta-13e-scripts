function isPlayerObject(%obj) {
    if (isObject(%obj)) {
    }
    return !(!(%obj.getType() & $TypeMasks::PlayerObjectType));
};
function isAIPlayerObject(%obj) {
    if (isObject(%obj)) {
    }
    return !(!(%obj.getClassName() $= "AIPlayer"));
};
function isNPCObject(%obj) {
    if (!(isObject(NPCGroup))) {
    }
    if (!(isAIPlayerObject(%obj))) {
        return 0;
    }
    return (%obj.getGroup().getId() == NPCGroup.getId());
};
function isPlayerCharacter(%obj) {
    return isObject(%obj.client);
};
function stripColorChars(%line) {
    return stripChars(%line, "\x10\x01\x02\x03\x04\x05\x06\x07\x0B\x0C\x0E");
};
function reloadScripts() {
    if ($AmClient) {
        exec($userMods @ "/client/ets/init.cs");
    }
    if ($AmServer) {
        exec($userMods @ "/server/scripts/ets/init.cs");
    }
    exec($userMods @ "/common/scripts/initReloadable.cs");
    reloadModScripts("");
    initProjectsReloadable();
    initProjectsReloadableLate();
};
function gSetField(%object, %name, %value) {
    %name[%value @ $gGlobalFields TAB %object.getId() @ %name] = ;
};
function gGetField(%object, %name) {
    return %name[$gGlobalFields TAB %object.getId() @ %name];
};
function gGetFieldWithDefault(%object, %name, %def) {
    if (!(isObject(%object))) {
        error(getScopeName() @ " " @ "called with bad object!");
        return %def;
    }
    if (isDefined("$gGlobalFields" @ %object.getId() @ "_" @ %name)) {
        return %name[$gGlobalFields TAB %object.getId() @ %name];
    }
    return %def;
};
function tmpFields(%obj) {
    %tmps = gGetField(%obj, tmpFields);
    if (!(isObject(%tmps))) {
        %tmps = new ScriptObject(temporaryfields);
        if (isObject(MissionCleanup)) {
            %tmps.add(MissionCleanup);
        }
        gSetField(%obj, tmpFields, %tmps);
    }
    return %tmps;
};
function getDebugString(%obj) {
    if (!(isObject(%obj))) {
        return "-(" @ %obj @ " " @ "is not an object)-";
    }
    return %obj.getDebugString();
};
function makeTaggedString(%plainText) {
    %cmd = "%ret = \"" @ expandEscape(%plainText) @ "\";";
    eval(%cmd);
    return %ret;
};
function crash() {
    echo("intentionally crashing.");
    echo((1 % 0));
};
function crashDelayed(%ms) {
    if ((%ms $= "")) {
        %ms = 5000;
    }
    echo("scheduled crash in" @ " " @ (%ms / 1000.0) @ " " @ "seconds..");
    if ((%ms > 1000.0)) {
        schedule(1000, 0, "crashDelayed", (%ms - 1000.0));
    }
    schedule(%ms, 0, "crash");
};
function hasWord(%searchText, %findText) {
    return (findWord(%searchText, %findText) >= 0.0);
};
function hasField(%searchText, %findText) {
    return (findField(%searchText, %findText) >= 0.0);
};
function hasRecord(%searchText, %findText) {
    return (findRecord(%searchText, %findText) >= 0.0);
};
function hasSubString(%searchText, %findText) {
    return (strstr(%searchText, %findText) >= 0.0);
};
function getSuffixPos(%searchText, %suffix) {
    %idx = strpos(%searchText, %suffix);
    if ((%idx < 0.0)) {
        return -(1.0);
    }
    while ((%idx >= 0.0)) {
        %last = %idx;
        %idx = strpos(%searchText, %suffix, (%idx + 1.0));
    }
    %idx = %last;
    (%idx >= 0.0);
    if (((%idx + strlen(%suffix)) != strlen(%searchText))) {
        return -(1.0);
    }
    return %idx;
};
function hasPrefix(%searchText, %prefix) {
    %len = strlen(%prefix);
    %ret = (getSubStr(%searchText, 0, %len) $= %prefix);
    return %ret;
};
function hasSuffix(%searchText, %suffix) {
    %ret = (getSuffixPos(%searchText, %suffix) >= 0.0);
    return %ret;
};
function execFilesWithName(%fileName) {
    %file = findFirstFile(%fileName);
    while (!(%file $= "")) {
        exec(%file);
        %file = findNextFile(%fileName);
    }
};
function safeNewScriptObject(%classname, %objectName, %deleteExisting) {
    if (%deleteExisting) {
    }
    if (isObject(%objectName)) {
        %objectName.delete();
    }
    eval("%ret = new " @ %classname @ "(" @ %objectName @ ");");
    if (isObject(MissionCleanup)) {
        %ret.add(MissionCleanup);
    }
    return %ret;
};
function safeEnsureScriptObject(%classname, %objectName) {
    return safeEnsureScriptObjectWithClassBindingsAndInit(%classname, %objectName, "", "");
};
function safeEnsureScriptObjectWithClassBindings(%classname, %objectName, %classesToBind) {
    return safeEnsureScriptObjectWithClassBindingsAndInit(%classname, %objectName, %classesToBind, "");
};
function safeEnsureScriptObjectWithInit(%classname, %objectName, %datablock) {
    return safeEnsureScriptObjectWithClassBindingsAndInit(%classname, %objectName, "", %datablock);
};
function safeEnsureScriptObjectWithClassBindingsAndInit(%classname, %objectName, %classesToBind, %datablock) {
    if (isObject(%objectName)) {
        return %objectName.getId();
    }
    %classesToBind = trim(%classesToBind);
    if ((%classesToBind $= "")) {
        %cmd = "%ret = new " @ %classname @ "(" @ %objectName @ ")";
        if (!(%datablock $= "")) {
            %cmd = %cmd @ " " @ %datablock;
        }
        %cmd = %cmd @ ";";
        eval(%cmd);
    }
    %cmd = "%ret = new " @ %classname @ "()";
    if (!(%datablock $= "")) {
        %cmd = %cmd @ " " @ %datablock;
    }
    %cmd = %cmd @ ";";
    eval(%cmd);
    if (!(%classesToBind $= "")) {
        %classCount = getWordCount(%classesToBind);
        %i = 0;
        while ((%i < %classCount)) {
            getWord(%classesToBind, %i).bindClassName(%ret);
            %i = (%i + 1.0);
        }
    }
    %objectName.setName(%ret);
    if (isObject(MissionCleanup)) {
        %ret.add(MissionCleanup);
    }
    return %ret;
};
$gValidTextureExt = ".dbm .dbmc .jpg .png";
function getPathOfButtonResource(%res) {
    %cached = getCachedResourcePath(%res);
    if (!(%cached $= "")) {
        return %cached;
    }
    %n = (getWordCount($gValidTextureExt) - 1.0);
    while ((%n >= 0.0)) {
        %ext = getWord($gValidTextureExt, %n);
        if (isFile(%res @ %ext)) {
        }
        if (isFile(%res @ "_n" @ %ext)) {
            setCachedResourcePath(%res, %res);
            return %res;
        }
        %n = (%n - 1.0);
    }
    return "";
};
function getPathsMatchingPattern(%pattern) {
    %ret = findFirstFile(%pattern);
    if (!(%ret $= "") && 1) {
        %next = findNextFile(%pattern);
        if ((%next $= "")) {
        }
        %ret = %ret @ "\t" @ %next;
    }
    return %ret;
};
function getCachedResourcePath(%res) {
    safeEnsureScriptObject("StringMap", "ResourcePathMap");
    %path = %res.get(ResourcePathMap);
    return %path;
};
function setCachedResourcePath(%res, %path) {
    safeEnsureScriptObject("StringMap", "ResourcePathMap");
    %path.put(ResourcePathMap, %res);
};
function setAllLogLevels(%level) {
    %level.setAllLogLevels(Console);
    %level.setAllLogLevels(log);
    setConsoleLogLevel(%level);
};
function bitstreamCountToggle() {
    setAllLogLevels("debug");
    $bitStreamCount = !($bitStreamCount);
};
function getPlayerMarkup(%player, %color, %isNameNotObject) {
    %playerName = "";
    if (!(%isNameNotObject)) {
    }
    if ((%isNameNotObject $= "")) {
        %playerName = %player.getShapeName();
    }
    if ((%playerName $= "")) {
        %playerName = %player;
        %player = "";
    }
    %result = "<spush>";
    if ((%color $= "")) {
    }
    if (isObject($player)) {
    }
    if ((%playerName $= $player.getShapeName())) {
        %color = "4600a0ff";
    }
    if (!(%player $= "")) {
    }
    if (%player.isIgnore()) {
        %result = %result @ "<linkcolor:00000080>";
    }
    if (!(%color $= "")) {
        %result = %result @ makeLinkColorTag(%color);
    }
    %botString = "";
    if (isObject(PlayerInstanceDict)) {
        %playerObj = Player::findPlayerInstance(%playerName);
        if (isObject(%playerObj)) {
        }
        if (%playerObj.isClassAIPlayer()) {
            %botString = " (bot)";
        }
    }
    %name = %playerName;
    %result = %result @ "<a:gamelink " @ munge(%name) @ ">" @ StripMLControlChars(%name) @ %botString @ "</a><spop>";
    return %result;
};
function SegmentList(%masterList, %delimiter, %segmentDelimiter, %segmentSize) {
    if ((%delimiter $= "")) {
        error(getScopeName() @ "->delimiter argument unspecified!");
        return;
    }
    if ((%segmentSize $= "")) {
        error(getScopeName() @ "->segmentSize argument unspecified!");
        return;
    }
    if ((%segmentDelimiter $= "")) {
        error(getScopeName() @ "->segmentDelimiter argument unspecified!");
        return;
    }
    %outString = "";
    %idx = 0;
    %len = strlen(%masterList);
    while ((%idx < %len)) {
        %segStart = %idx;
        %lastGoodIdx = %len;
        if (((%len - %segStart) > %segmentSize) && (((%idx = strpos(%masterList, %delimiter, %idx)) - %segStart) < %segmentSize)) {
            if ((%idx < 0.0)) {
            }
            %lastGoodIdx = %idx;
            %idx = (%idx + 1.0);
        }
        %currentList = getSubStr(%masterList, %segStart, (%lastGoodIdx - %segStart));
        (((%idx = strpos(%masterList, %delimiter, %idx)) - %segStart) < %segmentSize);
        %idx = (%lastGoodIdx + 1.0);
        if (!(%outString $= "")) {
            %outString = %outString @ %segmentDelimiter @ %currentList;
        }
        %outString = %currentList;
    }
    return %outString;
};
function SimSet::getByField(%this, %field, %svalue) {
    %n = (%this.getCount() - 1.0);
    while ((%n >= 0.0)) {
        %obj = %n.getObject(%this);
        %evalString = "return %obj." @ %field @ " $= \"" @ %svalue @ "\";";
        if (eval(%evalString)) {
            return %obj;
        }
        %n = (%n - 1.0);
    }
    return "";
};
function GuiControl::getChildrenInOrder(%this, %children) {
    %ids = "";
    %count = getWordCount(%children);
    %i = 0;
    while ((%i < %count)) {
        %child = getWord(%children, %i);
        if (isObject(%child)) {
            %ids = %ids @ " " @ %child.getId();
        }
        %i = (%i + 1.0);
    }
    %ids = trim(%ids);
    (%i < %count);
    %toReturn = "";
    %count = %this.getCount();
    %i = 0;
    while ((%i < %count)) {
        %child = %i.getObject(%this);
        if (hasWord(%ids, %child)) {
            %toReturn = %toReturn @ " " @ %child;
        }
        %i = (%i + 1.0);
    }
    return trim(%toReturn);
};
function logOnce(%logSystems, %logLevel, %key, %msg) {
    %key = %logLevel @ " " @ getScopeName(1) @ "_" @ %key;
    %map = safeEnsureScriptObject("StringMap", "messageCountsErrors");
    %count = %key.get(%map);
    if ((%count $= "")) {
        log(%logSystems, %logLevel, %msg);
    }
    if ((%count == 1.0)) {
        log(%logSystems, %logLevel, "multiple log messages for:" @ " " @ %key @ " " @ "- swallowing the remainder." @ " " @ %msg);
    }
    %count = (%count + 1.0);
    %count.put(%map, %key);
};
function debugOnce(%key, %msg) {
    %key = getScopeName(1) @ "_" @ %key;
    logOnce("general", "debug", %key, %msg);
};
function echoOnce(%key, %msg) {
    %key = getScopeName(1) @ "_" @ %key;
    logOnce("general", "info", %key, %msg);
};
function warnOnce(%key, %msg) {
    %key = getScopeName(1) @ "_" @ %key;
    logOnce("general", "warn", %key, %msg);
};
function errorOnce(%key, %msg) {
    %key = getScopeName(1) @ "_" @ %key;
    logOnce("general", "error", %key, %msg);
};
$gValidObjectNameChars = "abcdefghijklmnopqrstuvwxyz" @ "ABCDEFGHIJKLMNOPQRSTUVWXYZ" @ "0123456789-_";
function stripForObjectName(%dry) {
    return stripString(%dry, $gValidObjectNameChars, "_");
};
function getExtension(%dry) {
    %wet = %dry;
    %wet = strrchr(%wet, "/");
    if ((%wet $= "")) {
    }
    %wet = %wet;
    %dry;
    %wet2 = %wet;
    %wet = strrchr(%wet, "?");
    if ((%wet $= "")) {
    }
    %wet = %wet;
    %wet2;
    %wet2 = %wet;
    %wet = strrchr(%wet, "&");
    if ((%wet $= "")) {
    }
    %wet = %wet;
    %wet2;
    %wet2 = %wet;
    %wet = strrchr(%wet, "=");
    if ((%wet $= "")) {
    }
    %wet = %wet;
    %wet2;
    %wet2 = %wet;
    %wet = strrchr(%wet2, ".");
    return %wet;
};
function stripExtension(%dry) {
    %ext = getExtension(%dry);
    %wet = getSubStr(%dry, 0, (strlen(%dry) - strlen(%ext)));
    return %wet;
};
function commaify(%num) {
    if ((%num == 0.0)) {
        return 0;
    }
    %sign = "";
    if ((getSubStr(%num, 0, 1) $= "-")) {
        %sign = "-";
        %num = getSubStr(%num, 1);
    }
    %result = "";
    %len = strlen(%num);
    while (!(%num $= "")) {
        if ((%len >= 3.0)) {
            %segment = getSubStr(%num, (%len - 3.0), 3);
            %num = getSubStr(%num, 0, (%len - 3.0));
        }
        %segment = %num;
        %num = "";
        if ((%result $= "")) {
            %result = %segment;
        }
        %result = %segment @ "," @ %result;
        %len = (%len - 3.0);
    }
    return %sign @ %result;
};
