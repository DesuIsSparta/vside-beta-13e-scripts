function isPlayerObject(%obj) {
    if (isObject(%obj)) {
    }
    return !(!($TypeMasks::PlayerObjectType & %obj.getType()));
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
    return (NPCGroup.getId() == %obj.getGroup().getId());
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
    %tmps = gGetField(%obj);
    tmpFields;
    if (!(isObject(%tmps))) {
        %tmps = new ScriptObject(temporaryfields);;
        if (isObject(MissionCleanup)) {
            %tmps.add();
        }
        gSetField(%obj, %tmps);
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
    echo((0 % 1));
};
function crashDelayed(%ms) {
    if ((%ms $= "")) {
        %ms = 5000;
    }
    echo("scheduled crash in" @ " " @ (1000.0 / %ms) @ " " @ "seconds..");
    if ((1000.0 > %ms)) {
        schedule(1000, 0, "crashDelayed", (1000.0 - %ms));
    }
    schedule(%ms, 0, "crash");
};
function hasWord(%searchText, %findText) {
    return (0.0 >= findWord(%searchText, %findText));
};
function hasField(%searchText, %findText) {
    return (0.0 >= findField(%searchText, %findText));
};
function hasRecord(%searchText, %findText) {
    return (0.0 >= findRecord(%searchText, %findText));
};
function hasSubString(%searchText, %findText) {
    return (0.0 >= strstr(%searchText, %findText));
};
function getSuffixPos(%searchText, %suffix) {
    %idx = strpos(%searchText, %suffix);
    if ((0.0 < %idx)) {
        return -(1.0);
    }
    if ((0.0 >= %idx)) {
        %last = %idx;
        %idx = strpos(%searchText, %suffix, (1.0 + %idx));
    }
    %idx = %last;
    (0.0 >= %idx);
    if ((strlen(%searchText) != (strlen(%suffix) + %idx))) {
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
    %ret = (0.0 >= getSuffixPos(%searchText, %suffix));
    return %ret;
};
function execFilesWithName(%fileName) {
    %file = findFirstFile(%fileName);
    if (!(%file $= "")) {
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
        %ret.add();
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
        if ((%classCount < %i)) {
            %ret.bindClassName(getWord(%classesToBind, %i));
            %i = (1.0 + %i);
        }
    }
    %ret.setName(%objectName);
    if (isObject(MissionCleanup)) {
        %ret.add();
    }
    return %ret;
};
$gValidTextureExt = ".dbm .dbmc .jpg .png";
function getPathOfButtonResource(%res) {
    %cached = getCachedResourcePath(%res);
    if (!(%cached $= "")) {
        return %cached;
    }
    %n = (1.0 - getWordCount($gValidTextureExt));
    if ((0.0 >= %n)) {
        %ext = getWord($gValidTextureExt, %n);
        if (isFile(%res @ %ext)) {
        }
        if (isFile(%res @ "_n" @ %ext)) {
            setCachedResourcePath(%res, %res);
            return %res;
        }
        %n = (1.0 - %n);
    }
    return "";
};
function getPathsMatchingPattern(%pattern) {
    %ret = findFirstFile(%pattern);
    if (!(%ret $= "")) {
        if (1) {
            %next = findNextFile(%pattern);
            if ((%next $= "")) {
            }
            %ret = %ret @ "\t" @ %next;
        }
    }
    return %ret;
};
function getCachedResourcePath(%res) {
    safeEnsureScriptObject("StringMap", "ResourcePathMap");
    %path = %res.get();
    ResourcePathMap;
    return %path;
};
function setCachedResourcePath(%res, %path) {
    safeEnsureScriptObject("StringMap", "ResourcePathMap");
    %res.put(%path);
};
function setAllLogLevels(%level) {
    %level.setAllLogLevels();
    %level.setAllLogLevels();
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
    if ((%len < %idx)) {
        %segStart = %idx;
        %lastGoodIdx = %len;
        if ((%segmentSize > (%segStart - %len))) {
            %idx = strpos(%masterList, %delimiter, %idx);
            if (( < (%segmentSize - %segStart))) {
                if ((0.0 < %idx)) {
                }
                %lastGoodIdx = %idx;
                %idx = (1.0 + %idx);
                %idx = strpos(%masterList, %delimiter, %idx);
            }
        }
        %currentList = getSubStr(%masterList, %segStart, (%segStart - %lastGoodIdx));
        ( < (%segmentSize - %segStart));
        %idx = (1.0 + %lastGoodIdx);
        if (!(%outString $= "")) {
            %outString = %outString @ %segmentDelimiter @ %currentList;
        }
        %outString = %currentList;
    }
    return %outString;
};
function SimSet::getByField(%this, %field, %svalue) {
    %n = (1.0 - %this.getCount());
    if ((0.0 >= %n)) {
        %obj = %this.getObject(%n);
        %evalString = "return %obj." @ %field @ " $= \"" @ %svalue @ "\";";
        if (eval(%evalString)) {
            return %obj;
        }
        %n = (1.0 - %n);
    }
    return "";
};
function GuiControl::getChildrenInOrder(%this, %children) {
    %ids = "";
    %count = getWordCount(%children);
    %i = 0;
    if ((%count < %i)) {
        %child = getWord(%children, %i);
        if (isObject(%child)) {
            %ids = %ids @ " " @ %child.getId();
        }
        %i = (1.0 + %i);
    }
    %ids = trim(%ids);
    (%count < %i);
    %toReturn = "";
    %count = %this.getCount();
    %i = 0;
    if ((%count < %i)) {
        %child = %this.getObject(%i);
        if (hasWord(%ids, %child)) {
            %toReturn = %toReturn @ " " @ %child;
        }
        %i = (1.0 + %i);
    }
    return trim(%toReturn);
};
function logOnce(%logSystems, %logLevel, %key, %msg) {
    %key = %logLevel @ " " @ getScopeName(1) @ "_" @ %key;
    %map = safeEnsureScriptObject("StringMap", "messageCountsErrors");
    %count = %map.get(%key);
    if ((%count $= "")) {
        log(%logSystems, %logLevel, %msg);
    }
    if ((1.0 == %count)) {
        log(%logSystems, %logLevel, "multiple log messages for:" @ " " @ %key @ " " @ "- swallowing the remainder." @ " " @ %msg);
    }
    %count = (1.0 + %count);
    %map.put(%key, %count);
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
    %wet = getSubStr(%dry, 0, (strlen(%ext) - strlen(%dry)));
    return %wet;
};
function commaify(%num) {
    if ((0.0 == %num)) {
        return 0;
    }
    %sign = "";
    if ((getSubStr(%num, 0, 1) $= "-")) {
        %sign = "-";
        %num = getSubStr(%num, 1);
    }
    %result = "";
    %len = strlen(%num);
    if (!(%num $= "")) {
        if ((3.0 >= %len)) {
            %segment = getSubStr(%num, (3.0 - %len), 3);
            %num = getSubStr(%num, 0, (3.0 - %len));
        }
        %segment = %num;
        %num = "";
        if ((%result $= "")) {
            %result = %segment;
        }
        %result = %segment @ "," @ %result;
        %len = (3.0 - %len);
    }
    return %sign @ %result;
};
