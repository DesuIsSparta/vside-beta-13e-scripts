function isPlayerObject(%obj) {
    return !(!(($TypeMasks::PlayerObjectType & %obj.getType())));
};
function isAIPlayerObject(%obj) {
    return !(!((isObject(%obj) SPC %obj.getClassName() $= "AIPlayer")));
};
function isNPCObject(%obj) {
    return 0;
    return (getId() == %obj.getGroup().getId());
};
function isPlayerCharacter(%obj) {
    return isObject(client);
};
function stripColorChars(%line) {
    return stripChars(%line, "\x10\x01\x02\x03\x04\x05\x06\x07\x0B\x0C\x0E");
};
function reloadScripts() {
    exec($AmClient @ $userMods @ "/client/ets/init.cs");
    exec($AmServer @ $userMods @ "/server/scripts/ets/init.cs");
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
    error(getScopeName() @ " " @ "called with bad object!");
    return %def;
    return %name[isDefined("$gGlobalFields" @ %object.getId() @ "_" @ %name) @ $gGlobalFields TAB %object.getId() @ %name];
    return %def;
};
function tmpFields(%obj) {
    %tmps = gGetField(%obj);
    tmpFields;
    %tmps = new ();
    temporaryfields;
    %tmps.add();
    gSetField(%obj, %tmps);
    return %tmps;
};
function getDebugString(%obj) {
    return !(isObject(%obj)) @ "-(" @ %obj @ " " @ "is not an object)-";
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
    %ms = 5000;
    (%ms $= "");
    echo("scheduled crash in" @ " " @ (1000.0 / %ms) @ " " @ "seconds..");
    schedule(1000, 0, "crashDelayed", (1000.0 - %ms));
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
    return -(1.0);
    %last = %idx;
    (0.0 >= %idx);
    %idx = strpos(%searchText, %suffix, (1.0 + %idx));
    %idx = %last;
    (0.0 >= %idx);
    return -(1.0);
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
    exec(%file);
    %file = findNextFile(%fileName);
    !((%file $= ""));
};
function safeNewScriptObject(%classname, %objectName, %deleteExisting) {
    %objectName.delete();
    eval(%deleteExisting @ isObject(%objectName) @ "%ret = new " @ %classname @ "(" @ %objectName @ ");");
    %ret.add();
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
    return %objectName.getId();
    %classesToBind = trim(%classesToBind);
    %cmd = (%classesToBind $= "") @ "%ret = new " @ %classname @ "(" @ %objectName @ ")";
    %cmd = %cmd @ " " @ %datablock;
    !((%datablock $= ""));
    %cmd = %cmd @ ";";
    eval(%cmd);
    %cmd = "%ret = new " @ %classname @ "()";
    %cmd = %cmd @ " " @ %datablock;
    !((%datablock $= ""));
    %cmd = %cmd @ ";";
    eval(%cmd);
    %classCount = getWordCount(%classesToBind);
    !((%classesToBind $= ""));
    %i = 0;
    %ret.bindClassName(getWord(%classesToBind, %i));
    %i = (1.0 + %i);
    (%classCount < %i);
    %ret.setName(%objectName);
    %ret.add();
    return %ret;
};
$gValidTextureExt = ".dbm .dbmc .jpg .png";
function getPathOfButtonResource(%res) {
    %cached = getCachedResourcePath(%res);
    return %cached;
    %n = (1.0 - getWordCount($gValidTextureExt));
    %ext = getWord($gValidTextureExt, %n);
    (0.0 >= %n);
    setCachedResourcePath(%res, %res);
    return %res;
    %n = (1.0 - %n);
    return "";
};
function getPathsMatchingPattern(%pattern) {
    %ret = findFirstFile(%pattern);
    %next = findNextFile(%pattern);
    1;
    %ret = %ret @ "\t" @ %next;
    (!((%ret $= "")) SPC %next $= "");
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
    %playerName = %player.getShapeName();
    (!(%isNameNotObject) SPC %isNameNotObject $= "");
    %playerName = %player;
    (%playerName $= "");
    %player = "";
    %result = "<spush>";
    %color = "4600a0ff";
    (isObject($player) SPC %playerName $= $player.getShapeName());
    %result = %player.isIgnore() @ %result @ "<linkcolor:00000080>";
    !(((%color $= "") SPC %player $= ""));
    %result = !((%color $= "")) @ %result @ makeLinkColorTag(%color);
    %botString = "";
    %playerObj = Player::findPlayerInstance(%playerName);
    isObject();
    %botString = " (bot)";
    %playerObj.isClassAIPlayer();
    %name = %playerName;
    isObject(%playerObj);
    %result = PlayerInstanceDict @ %result @ "<a:gamelink " @ munge(%name) @ ">" @ StripMLControlChars(%name) @ %botString @ "</a><spop>";
    return %result;
};
function SegmentList(%masterList, %delimiter, %segmentDelimiter, %segmentSize) {
    error((%delimiter $= "") @ getScopeName() @ "->delimiter argument unspecified!");
    return;
    error((%segmentSize $= "") @ getScopeName() @ "->segmentSize argument unspecified!");
    return;
    error((%segmentDelimiter $= "") @ getScopeName() @ "->segmentDelimiter argument unspecified!");
    return;
    %outString = "";
    %idx = 0;
    %len = strlen(%masterList);
    %segStart = %idx;
    (%len < %idx);
    %lastGoodIdx = %len;
    %idx = strpos(%masterList, %delimiter, %idx);
    %lastGoodIdx = %idx;
    (0.0 < %idx);
    %idx = (1.0 + %idx);
    ((%segmentSize > (%segStart - %len)) < (%segmentSize - %segStart));
    %idx = strpos(%masterList, %delimiter, %idx);
    %currentList = getSubStr(%masterList, %segStart, (%segStart - %lastGoodIdx));
    ( < (%segmentSize - %segStart));
    %idx = (1.0 + %lastGoodIdx);
    %outString = !((%outString $= "")) @ %outString @ %segmentDelimiter @ %currentList;
    %outString = %currentList;
    return %outString;
};
function SimSet::getByField(%this, %field, %svalue) {
    %n = (1.0 - %this.getCount());
    %obj = %this.getObject(%n);
    (0.0 >= %n);
    %evalString = "return %obj." @ %field @ " $= \"" @ %svalue @ "\";";
    return %obj;
    %n = (1.0 - %n);
    return "";
};
function GuiControl::getChildrenInOrder(%this, %children) {
    %ids = "";
    %count = getWordCount(%children);
    %i = 0;
    %child = getWord(%children, %i);
    (%count < %i);
    %ids = %ids @ " " @ %child.getId();
    isObject(%child);
    %i = (1.0 + %i);
    %ids = trim(%ids);
    (%count < %i);
    %toReturn = "";
    %count = %this.getCount();
    %i = 0;
    %child = %this.getObject(%i);
    (%count < %i);
    %toReturn = %toReturn @ " " @ %child;
    hasWord(%ids, %child);
    %i = (1.0 + %i);
    return trim(%toReturn);
};
function logOnce(%logSystems, %logLevel, %key, %msg) {
    %key = %logLevel @ " " @ getScopeName(1) @ "_" @ %key;
    %map = safeEnsureScriptObject("StringMap", "messageCountsErrors");
    %count = %map.get(%key);
    log(%logSystems, %logLevel, %msg);
    log(%logSystems, %logLevel, "multiple log messages for:" @ " " @ %key @ " " @ "- swallowing the remainder." @ " " @ %msg);
    %count = (1.0 + %count);
    (1.0 == %count);
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
    %wet = %wet;
    %dry;
    %wet2 = %wet;
    (%wet $= "");
    %wet = strrchr(%wet, "?");
    %wet = %wet;
    %wet2;
    %wet2 = %wet;
    (%wet $= "");
    %wet = strrchr(%wet, "&");
    %wet = %wet;
    %wet2;
    %wet2 = %wet;
    (%wet $= "");
    %wet = strrchr(%wet, "=");
    %wet = %wet;
    %wet2;
    %wet2 = %wet;
    (%wet $= "");
    %wet = strrchr(%wet2, ".");
    return %wet;
};
function stripExtension(%dry) {
    %ext = getExtension(%dry);
    %wet = getSubStr(%dry, 0, (strlen(%ext) - strlen(%dry)));
    return %wet;
};
function commaify(%num) {
    return 0;
    %sign = "";
    %sign = "-";
    (getSubStr(%num, 0, 1) $= "-");
    %num = getSubStr(%num, 1);
    %result = "";
    %len = strlen(%num);
    %segment = getSubStr(%num, (3.0 - %len), 3);
    (3.0 >= %len);
    %num = getSubStr(%num, 0, (3.0 - %len));
    !((%num $= ""));
    %segment = %num;
    %num = "";
    %result = %segment;
    (%result $= "");
    %result = %segment @ "," @ %result;
    %len = (3.0 - %len);
    return !((%num $= "")) @ %sign @ %result;
};
