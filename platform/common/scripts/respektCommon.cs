$gRespektLevelsNum = 0;
function respektAddLevel(%minPoints, %indefiniteArticle, %levelName) {
    $gRespektLevelsNum[$gRespektLevelsMinPoints @ $gRespektLevelsNum] = %minPoints;
    $gRespektLevelsNum[$gRespektLevelsIndefiniteArticles @ $gRespektLevelsNum] = %indefiniteArticle;
    $gRespektLevelsNum[$gRespektLevelsNames @ $gRespektLevelsNum] = %levelName;
    $gRespektLevelsNum = ($gRespektLevelsNum + 1.0);
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
    while ((%n < $gRespektLevelsNum)) {
        if ((%n[$gRespektLevelsMinPoints @ %n] > %score)) {
            return %level;
        }
        %level = (%level + 1.0);
        %n = (%n + 1.0);
    }
    return %level;
};
function respektScoreToNextLevel(%score) {
    %level = respektScoreToLevel(%score);
    if ((%level < ($gRespektLevelsNum - 1.0))) {
        %level = (%level + 1.0);
    }
    return %level;
};
function respektLevelValidate(%level) {
    if ((%level $= "")) {
        %level = 0;
    }
    if ((%level < 0.0)) {
        error(getScopeName() @ " " @ "- invalid level:" @ " " @ %level);
        %level = 0;
    }
    if ((%level >= $gRespektLevelsNum)) {
        error(getScopeName() @ " " @ "- invalid level:" @ " " @ %level);
        %level = ($gRespektLevelsNum - 1.0);
    }
    if ((%level < 0.0)) {
        error(getScopeName() @ " " @ "- levels not initialized:" @ " " @ %level);
        %level = 0;
    }
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
    if ((%article $= "")) {
    }
    %ret = %article @ " " @ %levelName;
    %levelName;
    return %ret;
};
function respektPointsNeededToNextLevel(%score) {
    %nextLevel = respektScoreToNextLevel(%score);
    return (%nextLevel[$gRespektLevelsMinPoints @ %nextLevel] - %score);
};
function respektPercentToNextLevel(%score) {
    %prevLevel = respektScoreToLevel(%score);
    %nextLevel = respektScoreToNextLevel(%score);
    %range = (%nextLevel[$gRespektLevelsMinPoints @ %nextLevel] - %prevLevel[$gRespektLevelsMinPoints @ %prevLevel]);
    if ((%range == 0.0)) {
        return 0;
    }
    %percent = ((%nextLevel[$gRespektLevelsMinPoints @ %nextLevel] - %score) / %range);
    return %percent;
};
function respektLevelMinPoints(%level) {
    %level = respektLevelValidate(%level);
    return %level[$gRespektLevelsMinPoints @ %level];
};
function respektLevelMaxPoints(%level) {
    %level = respektLevelValidate(%level);
    if ((%level >= ($gRespektLevelsNum - 1.0))) {
        return %level[$gRespektLevelsMinPoints @ %level];
    }
    return (%level[$gRespektLevelsMinPoints @ (%level + 1.0)] - 1.0);
};
function Player::getRespektLevel(%this) {
    return respektScoreToLevel(%this.getRespektPoints());
};
function Player::hasRespektLevel(%this, %level) {
    if (%this.isStaffOrModerator()) {
        return 1;
    }
    if ((%level $= "") || (%level == 0.0)) {
        return 1;
    }
    return (%this.getRespektPoints() >= respektLevelMinPoints(%level));
};
function Player::getRespektPoints(%this) {
    if (!%this.isServerObject()) {
        %this.setRespektPoints($gMyRespektPoints);
    }
    return gGetField(%this, "respektPoints");
};
function Player::setRespektPoints(%this, %points) {
    gSetField(%this, "respektPoints", %points);
};
function isNewerRevision(%isThis, %newerThanThis, %playerName) {
    if ((%isThis $= "")) {
        log("communication", "error", getScopeName() @ " " @ "- got empty revision for" @ " " @ %playerName);
        return 1;
    }
    if ((%isThis < %newerThanThis)) {
        log("communication", "debug", getScopeName() @ " " @ "- got revision out of order for" @ " " @ %playerName @ " " @ ":" @ " " @ %isThis @ " " @ "<" @ " " @ %newerThanThis);
        return 0;
    }
    if ((%isThis == %newerThanThis)) {
        log("communication", "info", getScopeName() @ " " @ "- got duplicate revision for" @ " " @ %playerName @ " " @ ":" @ " " @ %isThis @ " " @ "==" @ " " @ %newerThanThis);
        return 0;
    }
    return 1;
};
function isOlderRevision(%isThis, %olderThanThis, %playerName) {
    if ((%isThis $= "")) {
        log("communication", "error", getScopeName() @ " " @ "- got empty revision for" @ " " @ %playerName);
        return 1;
    }
    if ((%isThis < %olderThanThis)) {
        log("communication", "debug", getScopeName() @ " " @ "- got revision out of order for" @ " " @ %playerName @ " " @ ":" @ " " @ %isThis @ " " @ "<" @ " " @ %olderThanThis);
        return 1;
    }
    return 0;
};
function getRespektMessage(%dValue, %code) {
    %posNeg = (%dValue >= 0.0) ? "pos" : "neg";
    %msg = %posNeg[$MsgCat::respektEvent TAB %code @ %posNeg];
    if ((%msg $= "")) {
        error(getScopeName() @ " " @ "- unknown respekt event code:" @ " " @ %code @ " " @ "dValue:" @ " " @ %dValue @ " " @ getTrace());
        %msg = %posNeg[$MsgCat::respektEvent TAB "DEFAULT" @ %posNeg];
    }
    if ((%msg $= "")) {
        error(getScopeName() @ " " @ "- default respekt event message not defined.");
        %msg = "[NONOTIFY]";
    }
    return %msg;
};
