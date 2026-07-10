$tmp::eventName = "autolookat";
$tmp::eventName[$floodFilter::maxEvents @ $tmp::eventName] = 2;
$tmp::eventName[$floodFilter::inPeriod @ $tmp::eventName] = (2.0 * 1000.0);
$tmp::eventName[$floodFilter::penalty @ $tmp::eventName] = 0;
$tmp::eventName[$floodFilter::message @ $tmp::eventName] = "";
$tmp::eventName[$floodFilter::exemptPermission @ $tmp::eventName] = "";
$tmp::eventName = "helpMeNotify";
$tmp::eventName[$floodFilter::maxEvents @ $tmp::eventName] = 1;
$tmp::eventName[$floodFilter::inPeriod @ $tmp::eventName] = (4.0 * (60.0 * 1000.0));
$tmp::eventName[$floodFilter::penalty @ $tmp::eventName] = 0;
$tmp::eventName[$floodFilter::message @ $tmp::eventName] = "";
$tmp::eventName[$floodFilter::exemptPermission @ $tmp::eventName] = "flood";
$tmp::eventName = "mic";
$tmp::eventName[$floodFilter::maxEvents @ $tmp::eventName] = 7;
$tmp::eventName[$floodFilter::inPeriod @ $tmp::eventName] = (5.0 * 1000.0);
$tmp::eventName[$floodFilter::penalty @ $tmp::eventName] = 0;
$tmp::eventName[$floodFilter::message @ $tmp::eventName] = "FLOOD PROTECTION: Chill out for a few seconds...";
$tmp::eventName[$floodFilter::exemptPermission @ $tmp::eventName] = "flood";
$tmp::eventName = "pubNotify";
$tmp::eventName[$floodFilter::maxEvents @ $tmp::eventName] = 10;
$tmp::eventName[$floodFilter::inPeriod @ $tmp::eventName] = (5.0 * 1000.0);
$tmp::eventName[$floodFilter::penalty @ $tmp::eventName] = 0;
$tmp::eventName[$floodFilter::message @ $tmp::eventName] = "";
$tmp::eventName[$floodFilter::exemptPermission @ $tmp::eventName] = "flood";
$tmp::eventName = "raiseHand";
$tmp::eventName[$floodFilter::maxEvents @ $tmp::eventName] = 1;
$tmp::eventName[$floodFilter::inPeriod @ $tmp::eventName] = (60.0 * 1000.0);
$tmp::eventName[$floodFilter::penalty @ $tmp::eventName] = 0;
$tmp::eventName[$floodFilter::message @ $tmp::eventName] = "";
$tmp::eventName[$floodFilter::exemptPermission @ $tmp::eventName] = "cussExempt";
$tmp::eventName = "regular";
$tmp::eventName[$floodFilter::maxEvents @ $tmp::eventName] = 7;
$tmp::eventName[$floodFilter::inPeriod @ $tmp::eventName] = (10.0 * 1000.0);
$tmp::eventName[$floodFilter::penalty @ $tmp::eventName] = (4.0 * 1000.0);
$tmp::eventName[$floodFilter::message @ $tmp::eventName] = "FLOOD PROTECTION: Chill out for a few seconds...";
$tmp::eventName[$floodFilter::exemptPermission @ $tmp::eventName] = "flood";
$tmp::eventName = "sos";
$tmp::eventName[$floodFilter::maxEvents @ $tmp::eventName] = 2;
$tmp::eventName[$floodFilter::inPeriod @ $tmp::eventName] = (5.0 * (60.0 * 1000.0));
$tmp::eventName[$floodFilter::penalty @ $tmp::eventName] = 0;
$tmp::eventName[$floodFilter::message @ $tmp::eventName] = "Whoa there - spamming won't help you get help. Try again in a few minutes. For more information on how block annoying users or report abuse, press F1.";
$tmp::eventName[$floodFilter::exemptPermission @ $tmp::eventName] = "flood";
$tmp::eventName = "teleport";
$tmp::eventName[$floodFilter::maxEvents @ $tmp::eventName] = 5;
$tmp::eventName[$floodFilter::inPeriod @ $tmp::eventName] = (2.0 * 1000.0);
$tmp::eventName[$floodFilter::penalty @ $tmp::eventName] = (30.0 * 1000.0);
$tmp::eventName[$floodFilter::message @ $tmp::eventName] = "Whoa there!";
$tmp::eventName[$floodFilter::exemptPermission @ $tmp::eventName] = "flood";
$tmp::eventName = "whisper";
$tmp::eventName[$floodFilter::maxEvents @ $tmp::eventName] = 7;
$tmp::eventName[$floodFilter::inPeriod @ $tmp::eventName] = (10.0 * 1000.0);
$tmp::eventName[$floodFilter::penalty @ $tmp::eventName] = (4.0 * 1000.0);
$tmp::eventName[$floodFilter::message @ $tmp::eventName] = "FLOOD PROTECTION: Chill out for a few seconds...";
$tmp::eventName[$floodFilter::exemptPermission @ $tmp::eventName] = "flood";
$tmp::eventName = "yell";
$tmp::eventName[$floodFilter::maxEvents @ $tmp::eventName] = 1;
$tmp::eventName[$floodFilter::inPeriod @ $tmp::eventName] = (10.0 * (60.0 * 1000.0));
$tmp::eventName[$floodFilter::penalty @ $tmp::eventName] = 0;
$tmp::eventName[$floodFilter::message @ $tmp::eventName] = "FLOOD PROTECTION: Don't yell so much!";
$tmp::eventName[$floodFilter::exemptPermission @ $tmp::eventName] = "flood";
function testFlooding(%player, %eventType, %testExempt) {
    if (!(isObject(%player))) {
        if (!($AmClient)) {
            error(getScopeName() @ " " @ "- called without an object on server. (allowing action)" @ " " @ getTrace());
            return 0;
        }
        if (isObject($player)) {
            error(getScopeName() @ " " @ "- called without an object when $player is valid (allowing action)" @ " " @ getTrace());
            return 0;
        }
        %player = safeEnsureScriptObject("ScriptObject", "gConnectionlessFloodingProxy");
        %testExempt = 0;
    }
    if (%player.isClassAIPlayer()) {
        return 0;
    }
    if (%testExempt) {
        if (testFloodingExempt(%player, %eventType)) {
            return 0;
        }
    }
    %erOld = %player.eventRecord;
    %eventType;
    %erNew = "";
    %newNum = 0;
    %expiredTime = (%eventType[$floodFilter::inPeriod @ %eventType] - getSimTime());
    %n = (1.0 - getWordCount(%erOld));
    if ((0.0 >= %n)) {
        %eventTime = getWord(%erOld, %n);
        if ((%expiredTime >= %eventTime)) {
            %erNew = %eventTime @ " " @ %erNew;
            %newNum = (1.0 + %newNum);
        }
        %n = (1.0 - %n);
    }
    if ((%eventType[$floodFilter::maxEvents @ %eventType] >= %newNum)) {
        if ((0.0 > %eventType[$floodFilter::penalty @ %eventType])) {
            %erNew = (%eventType[$floodFilter::penalty @ %eventType] + getSimTime()) @ " " @ %erNew;
            (0.0 >= %n);
        }
        %player.eventRecord = %erNew @ %eventType;
        return 1;
    }
    %erNew = getSimTime() @ " " @ %erNew;
    %player.eventRecord = %erNew @ %eventType;
    return 0;
};
function testFloodingExempt(%player, %eventType) {
    %count = getWordCount(%eventType[$floodFilter::exemptPermission @ %eventType]);
    %idx = 0;
    if ((%count < %idx)) {
        %permission = getWord(%eventType[$floodFilter::exemptPermission @ %eventType], %idx);
        if (%player.rolesPermissionCheckNoWarn(%permission)) {
            return 1;
        }
        %idx = (1.0 + %idx);
    }
    return 0;
};
