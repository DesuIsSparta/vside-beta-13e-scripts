$gRespektLevelsNum = 0;
function respektAddLevel(%minPoints, %indefiniteArticle, %levelName)
{
    $gRespektLevelsMinPoints[$gRespektLevelsNum] = %minPoints ;
    $gRespektLevelsIndefiniteArticles[$gRespektLevelsNum] = %indefiniteArticle ;
    $gRespektLevelsNames[$gRespektLevelsNum] = %levelName ;
    $gRespektLevelsNum = $gRespektLevelsNum + 1;
    return ;
}
function respektScoresInit()
{
    $gRespektThreshholdsNum = 0;
    respektAddLevel(0, "a", "Wallflower");
    respektAddLevel(5300, "a", "Half-Pint");
    respektAddLevel(5800, "a", "Young\'in");
    respektAddLevel(9800, "a", "Sidekick");
    respektAddLevel(19800, "", "That One Kid");
    respektAddLevel(34800, "", "Somebody");
    respektAddLevel(79800, "", "Da Bomb");
    respektAddLevel(154800, "a", "Party Animal");
    respektAddLevel(254800, "", "Funkadelic");
    respektAddLevel(504800, "", "Da Shiznit");
    respektAddLevel(1004800, "a", "VIP");
    return ;
}
respektScoresInit();
function respektScoreToLevel(%score)
{
    %level = 0;
    %n = 1;
    while (%n < $gRespektLevelsNum)
    {
        if ($gRespektLevelsMinPoints[%n] > %score)
        {
            return %level;
        }
        %level = %level + 1;
        %n = %n + 1;
    }
    return %level;
}
function respektScoreToNextLevel(%score)
{
    %level = respektScoreToLevel(%score);
    if (%level < ($gRespektLevelsNum - 1))
    {
        %level = %level + 1;
    }
    return %level;
}
function respektLevelValidate(%level)
{
    if (%level $= "")
    {
        %level = 0;
    }
    if (%level < 0)
    {
        error(getScopeName() SPC "- invalid level:" SPC %level);
        %level = 0;
    }
    if (%level >= $gRespektLevelsNum)
    {
        error(getScopeName() SPC "- invalid level:" SPC %level);
        %level = $gRespektLevelsNum - 1;
    }
    if (%level < 0)
    {
        error(getScopeName() SPC "- levels not initialized:" SPC %level);
        %level = 0;
    }
    return %level;
}
function respektLevelToNameWithoutArticle(%level)
{
    %level = respektLevelValidate(%level);
    return $gRespektLevelsNames[%level];
}
function respektLevelToNameWithIndefiniteArticle(%level)
{
    %level = respektLevelValidate(%level);
    %article = $gRespektLevelsIndefiniteArticles[%level];
    %levelName = respektLevelToNameWithoutArticle(%level);
    %ret = %article $= "" ? %levelName : %article;
    return %ret;
}
function respektPointsNeededToNextLevel(%score)
{
    %nextLevel = respektScoreToNextLevel(%score);
    return $gRespektLevelsMinPoints[%nextLevel] - %score;
}
function respektPercentToNextLevel(%score)
{
    %prevLevel = respektScoreToLevel(%score);
    %nextLevel = respektScoreToNextLevel(%score);
    %range = $gRespektLevelsMinPoints[%nextLevel] - $gRespektLevelsMinPoints[%prevLevel];
    if (%range == 0)
    {
        return 0;
    }
    %percent = ($gRespektLevelsMinPoints[%nextLevel] - %score) / %range;
    return %percent;
}
function respektLevelMinPoints(%level)
{
    %level = respektLevelValidate(%level);
    return $gRespektLevelsMinPoints[%level];
}
function respektLevelMaxPoints(%level)
{
    %level = respektLevelValidate(%level);
    if (%level >= ($gRespektLevelsNum - 1))
    {
        return $gRespektLevelsMinPoints[%level];
    }
    else
    {
        return $gRespektLevelsMinPoints[%level + 1] - 1;
    }
    return ;
}
function Player::getRespektLevel(%this)
{
    return respektScoreToLevel(%this.getRespektPoints());
}
function Player::hasRespektLevel(%this, %level)
{
    if (%this.isStaffOrModerator())
    {
        return 1;
    }
    if ((%level $= "") && (%level == 0))
    {
        return 1;
    }
    return %this.getRespektPoints() >= respektLevelMinPoints(%level);
}
function Player::getRespektPoints(%this)
{
    if (!%this.isServerObject())
    {
        %this.setRespektPoints($gMyRespektPoints);
    }
    return gGetField(%this, "respektPoints");
}
function Player::setRespektPoints(%this, %points)
{
    gSetField(%this, "respektPoints", %points);
    return ;
}
function isNewerRevision(%isThis, %newerThanThis, %playerName)
{
    if (%isThis $= "")
    {
        log("communication", "error", getScopeName() SPC "- got empty revision for" SPC %playerName);
        return 1;
    }
    if (%isThis < %newerThanThis)
    {
        log("communication", "debug", getScopeName() SPC "- got revision out of order for" SPC %playerName SPC ":" SPC %isThis SPC "<" SPC %newerThanThis);
        return 0;
    }
    if (%isThis == %newerThanThis)
    {
        log("communication", "info", getScopeName() SPC "- got duplicate revision for" SPC %playerName SPC ":" SPC %isThis SPC "==" SPC %newerThanThis);
        return 0;
    }
    return 1;
}
function isOlderRevision(%isThis, %olderThanThis, %playerName)
{
    if (%isThis $= "")
    {
        log("communication", "error", getScopeName() SPC "- got empty revision for" SPC %playerName);
        return 1;
    }
    if (%isThis < %olderThanThis)
    {
        log("communication", "debug", getScopeName() SPC "- got revision out of order for" SPC %playerName SPC ":" SPC %isThis SPC "<" SPC %olderThanThis);
        return 1;
    }
    return 0;
}
function getRespektMessage(%dValue, %code)
{
    %posNeg = %dValue >= 0 ? "pos" : "neg";
    %msg = $MsgCat::respektEvent[%code,%posNeg];
    if (%msg $= "")
    {
        error(getScopeName() SPC "- unknown respekt event code:" SPC %code SPC "dValue:" SPC %dValue SPC getTrace());
        %msg = $MsgCat::respektEvent["DEFAULT",%posNeg];
    }
    if (%msg $= "")
    {
        error(getScopeName() SPC "- default respekt event message not defined.");
        %msg = "[NONOTIFY]";
    }
    return %msg;
}
