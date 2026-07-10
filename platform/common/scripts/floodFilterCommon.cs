$tmp::eventName = "autolookat";
$floodFilter::maxEvents[$tmp::eventName] = 2;
$floodFilter::inPeriod[$tmp::eventName] = (1000.0 * 2.0);
$floodFilter::penalty[$tmp::eventName] = 0;
$floodFilter::message[$tmp::eventName] = "";
$floodFilter::exemptPermission[$tmp::eventName] = "";
$tmp::eventName = "helpMeNotify";
$floodFilter::maxEvents[$tmp::eventName] = 1;
$floodFilter::inPeriod[$tmp::eventName] = ((1000.0 * 60.0) * 4.0);
$floodFilter::penalty[$tmp::eventName] = 0;
$floodFilter::message[$tmp::eventName] = "";
$floodFilter::exemptPermission[$tmp::eventName] = "flood";
$tmp::eventName = "mic";
$floodFilter::maxEvents[$tmp::eventName] = 7;
$floodFilter::inPeriod[$tmp::eventName] = (1000.0 * 5.0);
$floodFilter::penalty[$tmp::eventName] = 0;
$floodFilter::message[$tmp::eventName] = "FLOOD PROTECTION: Chill out for a few seconds...";
$floodFilter::exemptPermission[$tmp::eventName] = "flood";
$tmp::eventName = "pubNotify";
$floodFilter::maxEvents[$tmp::eventName] = 10;
$floodFilter::inPeriod[$tmp::eventName] = (1000.0 * 5.0);
$floodFilter::penalty[$tmp::eventName] = 0;
$floodFilter::message[$tmp::eventName] = "";
$floodFilter::exemptPermission[$tmp::eventName] = "flood";
$tmp::eventName = "raiseHand";
$floodFilter::maxEvents[$tmp::eventName] = 1;
$floodFilter::inPeriod[$tmp::eventName] = (1000.0 * 60.0);
$floodFilter::penalty[$tmp::eventName] = 0;
$floodFilter::message[$tmp::eventName] = "";
$floodFilter::exemptPermission[$tmp::eventName] = "cussExempt";
$tmp::eventName = "regular";
$floodFilter::maxEvents[$tmp::eventName] = 7;
$floodFilter::inPeriod[$tmp::eventName] = (1000.0 * 10.0);
$floodFilter::penalty[$tmp::eventName] = (1000.0 * 4.0);
$floodFilter::message[$tmp::eventName] = "FLOOD PROTECTION: Chill out for a few seconds...";
$floodFilter::exemptPermission[$tmp::eventName] = "flood";
$tmp::eventName = "sos";
$floodFilter::maxEvents[$tmp::eventName] = 2;
$floodFilter::inPeriod[$tmp::eventName] = ((1000.0 * 60.0) * 5.0);
$floodFilter::penalty[$tmp::eventName] = 0;
$floodFilter::message[$tmp::eventName] = "Whoa there - spamming won't help you get help. Try again in a few minutes. For more information on how block annoying users or report abuse, press F1.";
$floodFilter::exemptPermission[$tmp::eventName] = "flood";
$tmp::eventName = "teleport";
$floodFilter::maxEvents[$tmp::eventName] = 5;
$floodFilter::inPeriod[$tmp::eventName] = (1000.0 * 2.0);
$floodFilter::penalty[$tmp::eventName] = (1000.0 * 30.0);
$floodFilter::message[$tmp::eventName] = "Whoa there!";
$floodFilter::exemptPermission[$tmp::eventName] = "flood";
$tmp::eventName = "whisper";
$floodFilter::maxEvents[$tmp::eventName] = 7;
$floodFilter::inPeriod[$tmp::eventName] = (1000.0 * 10.0);
$floodFilter::penalty[$tmp::eventName] = (1000.0 * 4.0);
$floodFilter::message[$tmp::eventName] = "FLOOD PROTECTION: Chill out for a few seconds...";
$floodFilter::exemptPermission[$tmp::eventName] = "flood";
$tmp::eventName = "yell";
$floodFilter::maxEvents[$tmp::eventName] = 1;
$floodFilter::inPeriod[$tmp::eventName] = ((1000.0 * 60.0) * 10.0);
$floodFilter::penalty[$tmp::eventName] = 0;
$floodFilter::message[$tmp::eventName] = "FLOOD PROTECTION: Don't yell so much!";
$floodFilter::exemptPermission[$tmp::eventName] = "flood";
function testFlooding(%player, %eventType, %testExempt)
{
    if (!isObject(%player))
    {
        if (!$AmClient)
        {
            error(getScopeName() @ " " @ "- called without an object on server. (allowing action)" @ " " @ getTrace());
            return 0;
        }
        if (isObject($player))
        {
            error(getScopeName() @ " " @ "- called without an object when $player is valid (allowing action)" @ " " @ getTrace());
            return 0;
        }
        %player = safeEnsureScriptObject("ScriptObject", "gConnectionlessFloodingProxy");
        %testExempt = 0;
    }
    if (%player.isClassAIPlayer())
    {
        return 0;
    }
    if (%testExempt && testFloodingExempt(%player, %eventType))
    {
        return 0;
    }
    %erOld = %player.eventRecord[%eventType];
    %erNew = "";
    %newNum = 0;
    %expiredTime = (getSimTime() - $floodFilter::inPeriod[%eventType]);
    %n = (getWordCount(%erOld) - 1.0);
    while ((%n >= 0.0))
    {
        %eventTime = getWord(%erOld, %n);
        if ((%eventTime >= %expiredTime))
        {
            %erNew = %eventTime @ " " @ %erNew;
            %newNum = (%newNum + 1.0);
        }
        %n = (%n - 1.0);
    }
    if ((%newNum >= $floodFilter::maxEvents[%eventType]))
    {
        if (($floodFilter::penalty[%eventType] > 0.0))
        {
            %erNew = (getSimTime() + $floodFilter::penalty[%eventType]) @ " " @ %erNew;
            (%n >= 0.0);
        }
        %player.eventRecord[%eventType] = %erNew;
        return 1;
    }
    else
    {
        %erNew = getSimTime() @ " " @ %erNew;
        %player.eventRecord[%eventType] = %erNew;
        return 0;
    }
}
function testFloodingExempt(%player, %eventType)
{
    %count = getWordCount($floodFilter::exemptPermission[%eventType]);
    %idx = 0;
    while ((%idx < %count))
    {
        %permission = getWord($floodFilter::exemptPermission[%eventType], %idx);
        if (%player.rolesPermissionCheckNoWarn(%permission))
        {
            return 1;
        }
        %idx = (%idx + 1.0);
    }
    return 0;
}
