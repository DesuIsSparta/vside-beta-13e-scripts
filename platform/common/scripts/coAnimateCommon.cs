function initCoAnimList() {
    addCoAnim("hug", "hug", 0, 0, 4, "-0.04 1.10 0 0 0 1 3.14159", 0, "hug you", "T");
    addCoAnim("slowdance", "slodance", 0, 0, 4, "-0.04 1.10 0 0 0 1 3.14159", 0, "slowdance with you", "T");
    addCoAnim("kickballs", "kickball", 0, 0, 4, "-0.04 1.10 0 0 0 1 3.14159", 0, "kick you in the balls", "T");
    addCoAnim("deepkiss", "deepkiss", 0, 0, 4, "-0.04 1.10 0 0 0 1 3.14159", 0, "deepkiss you", "T");
    addCoAnim("shake hands", "handshake", 0, 0, 4, "0 1.1 0 0 0 1 3.14159", 0, "shake your hand", "T");
    addCoAnim("kiss", "kiss", 0, 0, 4, "0 1.1 0 0 0 1 3.14159", 0, "kiss you", "T");
    addCoAnim("gift", "giveloot", 0, 0, 4, "0 1.1 0 0 0 1 3.14159", 0, "", "T");
    addCoAnim("giveDrink", "givedrink", 0, 0, 4, "0 0   0 0 0 1 3.14159", 0, "", "F");
    addCoAnim("makeDrink", "makedrink", 0, 0, 4, "0 0   0 0 0 1 3.14159", 0, "", "F");
};
$gCoAnimDictionary = 0;
function addCoAnim(%coAnimName, %anim, %delayA, %delayB, %range, %relativeTransform, %minLevel, %requestText, %moveMode) {
    if (!(isObject($gCoAnimDictionary))) {
        $gCoAnimDictionary = new ""();;
        StringMap;
        if (isObject(MissionCleanup)) {
            $gCoAnimDictionary.add();
        }
    }
    %entry = "" @ %anim @ "\t" @ %delayA @ "\t" @ %delayB @ "\t" @ %range @ "\t" @ %relativeTransform @ "\t" @ %minLevel @ "\t" @ %requestText @ "\t" @ %moveMode;
    MissionCleanup;
    setCoAnimEntry(%coAnimName, %entry);
};
function setCoAnimSkuPeriod(%coAnimName, %whichPlayer, %specialSkuName, %startMS, %stopMS) {
    %entry = findCoAnimEntry(%coAnimName);
    if ((%entry $= "")) {
        error(getScopeName() @ " " @ "- no such coAnim:" @ " " @ %coAnimName @ " " @ getTrace());
        return;
    }
    %skuPeriods = getField(%entry, 6);
    if (!(%skuPeriods $= "")) {
        %skuPeriods = %skuPeriods @ " ";
    }
    %skuPeriods = %skuPeriods @ %whichPlayer @ " " @ %specialSkuName @ " " @ %startMS @ " " @ %stopMS;
    %entry = setField(%entry, %skuPeriods);
    setCoAnimEntry(%coAnimName, %entry);
};
function findCoAnimEntry(%name) {
    if (!(isObject($gCoAnimDictionary))) {
        return "";
    }
    return $gCoAnimDictionary.get(%name);
};
function setCoAnimEntry(%name, %value) {
    $gCoAnimDictionary.put(%name, %value);
};
initCoAnimList();
function getAllCoAnims() {
    if (!(isObject($gCoAnimDictionary))) {
        return "";
    }
    %list = "";
    %count = $gCoAnimDictionary.size();
    %i = 0;
    if ((%count < %i)) {
        %userFacingName = $gCoAnimDictionary.getKey(%i);
        if (!(%userFacingName $= "")) {
            %list = %list @ "\t" @ %userFacingName;
        }
        %i = (1.0 + %i);
    }
    return trim(%list);
};
function getAllUserTriggerableCoAnims() {
    %retList = "";
    %delim = "";
    %list = getAllCoAnims();
    %n = (1.0 - getFieldCount(%list));
    if ((0.0 >= %n)) {
        %entryKey = getField(%list, %n);
        %entryVal = $gCoAnimDictionary.get(%entryKey);
        %wantsTo = getField(%entryVal, 6);
        if (!(%wantsTo $= "")) {
            %retList = %entryKey @ %delim @ %retList;
            %delim = "\t";
        }
        %n = (1.0 - %n);
    }
    return %retList;
};
