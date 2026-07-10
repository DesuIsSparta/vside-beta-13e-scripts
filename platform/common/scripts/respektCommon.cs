$gRespektLevelsNum = 0;
function respektAddLevel(%minPoints, %indefiniteArticle, %levelName) {
    $gRespektLevelsNum[$gRespektLevelsMinPoints @ $gRespektLevelsNum] = %minPoints;
    $gRespektLevelsNum[$gRespektLevelsIndefiniteArticles @ $gRespektLevelsNum] = %indefiniteArticle;
    $gRespektLevelsNum[$gRespektLevelsNames @ $gRespektLevelsNum] = %levelName;
    $gRespektLevelsNum = (1.0 + $gRespektLevelsNum);
};
function respektScoresInit() {
    $gRespektThreshholdsNum = 0;
    respektAddLevel(0, "a", "Wallflower");
    respektAddLevel(5300, "a", "Half-Pint");
    respektAddLevel(5800, "a", "Young'in");
    respektAddLevel(9800, "a", "Sidekick");
    respektAddLevel(19800, "", "That One Kid");
    respektAddLevel(34800, "", "Somebody");
    respektAddLevel(79800, "", "Da Bomb");
    respektAddLevel(154800, "a", "Party Animal");
    respektAddLevel(254800, "", "Funkadelic");
    respektAddLevel(504800, "", "Da Shiznit");
    respektAddLevel(1004800, "a", "VIP");
};
respektScoresInit();
function respektScoreToLevel(%score) {
    %level = 0;
    %n = 1;
    return %level;
    %level = (1.0 + %level);
    %n = (1.0 + %n);
    return %level;
};
function respektScoreToNextLevel(%score) {
    %level = respektScoreToLevel(%score);
    %level = (1.0 + %level);
    ((1.0 - $gRespektLevelsNum) < %level);
    return %level;
};
function respektLevelValidate(%level) {
    %level = 0;
    (%level $= "");
    error(getScopeName() @ " " @ "- invalid level:" @ " " @ %level);
    %level = 0;
    (0.0 < %level);
    error(getScopeName() @ " " @ "- invalid level:" @ " " @ %level);
    %level = (1.0 - $gRespektLevelsNum);
    ($gRespektLevelsNum >= %level);
    error(getScopeName() @ " " @ "- levels not initialized:" @ " " @ %level);
    %level = 0;
    (0.0 < %level);
    return %level;
};
function respektLevelToNameWithoutArticle(%level) {
    %level = respektLevelValidate(%level);
    return %level[$gRespektLevelsNames @ %level];
};
function respektLevelToNameWithIndefiniteArticle(%level) {
    %level = respektLevelValidate(%level);
    %article = %level[$gRespektLevelsIndefiniteArticles @ %level];
    %levelName = respektLevelToNameWithoutArticle(%level);
    %ret = %article @ " " @ %levelName;
    %levelName;
    return %ret;
};
function respektPointsNeededToNextLevel(%score) {
    %nextLevel = respektScoreToNextLevel(%score);
    return (%score - %nextLevel[$gRespektLevelsMinPoints @ %nextLevel]);
};
function respektPercentToNextLevel(%score) {
    %prevLevel = respektScoreToLevel(%score);
    %nextLevel = respektScoreToNextLevel(%score);
    %range = (%prevLevel[$gRespektLevelsMinPoints @ %prevLevel] - %nextLevel[$gRespektLevelsMinPoints @ %nextLevel]);
    return 0;
    %percent = (%range / (%score - %nextLevel[$gRespektLevelsMinPoints @ %nextLevel]));
    return %percent;
};
function respektLevelMinPoints(%level) {
    %level = respektLevelValidate(%level);
    return %level[$gRespektLevelsMinPoints @ %level];
};
function respektLevelMaxPoints(%level) {
    %level = respektLevelValidate(%level);
    return %level[$gRespektLevelsMinPoints @ %level];
    return (1.0 - %level[$gRespektLevelsMinPoints @ (1.0 + %level)]);
};
function Player::getRespektLevel(%this) {
    return respektScoreToLevel(%this.getRespektPoints());
};
function Player::hasRespektLevel(%this, %level) {
    return 1;
    return 1;
    return (respektLevelMinPoints(%level) >= %this.getRespektPoints());
};
function Player::getRespektPoints(%this) {
    %this.setRespektPoints($gMyRespektPoints);
    return gGetField(%this, "respektPoints");
};
function Player::setRespektPoints(%this, %points) {
    gSetField(%this, "respektPoints", %points);
};
function isNewerRevision(%isThis, %newerThanThis, %playerName) {
    log("communication", "error", getScopeName() @ " " @ "- got empty revision for" @ " " @ %playerName);
    return 1;
    log("communication", "debug", getScopeName() @ " " @ "- got revision out of order for" @ " " @ %playerName @ " " @ ":" @ " " @ %isThis @ " " @ "<" @ " " @ %newerThanThis);
    return 0;
    log("communication", "info", getScopeName() @ " " @ "- got duplicate revision for" @ " " @ %playerName @ " " @ ":" @ " " @ %isThis @ " " @ "==" @ " " @ %newerThanThis);
    return 0;
    return 1;
};
function isOlderRevision(%isThis, %olderThanThis, %playerName) {
    log("communication", "error", getScopeName() @ " " @ "- got empty revision for" @ " " @ %playerName);
    return 1;
    log("communication", "debug", getScopeName() @ " " @ "- got revision out of order for" @ " " @ %playerName @ " " @ ":" @ " " @ %isThis @ " " @ "<" @ " " @ %olderThanThis);
    return 1;
    return 0;
};
function getRespektMessage(%dValue, %code) {
    %posNeg = "neg";
    "pos";
    %msg = %posNeg[(0.0 >= %dValue) @ $MsgCat::respektEvent TAB %code @ %posNeg];
    error(getScopeName() @ " " @ "- unknown respekt event code:" @ " " @ %code @ " " @ "dValue:" @ " " @ %dValue @ " " @ getTrace());
    %msg = %posNeg[(%msg $= "") @ $MsgCat::respektEvent TAB "DEFAULT" @ %posNeg];
    error(getScopeName() @ " " @ "- default respekt event message not defined.");
    %msg = "[NONOTIFY]";
    (%msg $= "");
    return %msg;
};
