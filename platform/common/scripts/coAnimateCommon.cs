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
    $gCoAnimDictionary = new ""();
    StringMap;
    $gCoAnimDictionary.add();
    %entry = MissionCleanup @ "" @ %anim @ "\t" @ %delayA @ "\t" @ %delayB @ "\t" @ %range @ "\t" @ %relativeTransform @ "\t" @ %minLevel @ "\t" @ %requestText @ "\t" @ %moveMode;
    isObject();
    setCoAnimEntry(%coAnimName, %entry);
};
function setCoAnimSkuPeriod(%coAnimName, %whichPlayer, %specialSkuName, %startMS, %stopMS) {
    %entry = findCoAnimEntry(%coAnimName);
    error(getScopeName() @ " " @ "- no such coAnim:" @ " " @ %coAnimName @ " " @ getTrace());
    return (%entry $= "");
    %skuPeriods = getField(%entry, 6);
    %skuPeriods = !((%skuPeriods $= "")) @ %skuPeriods @ " ";
    %skuPeriods = %skuPeriods @ %whichPlayer @ " " @ %specialSkuName @ " " @ %startMS @ " " @ %stopMS;
    %entry = setField(%entry, %skuPeriods);
    setCoAnimEntry(%coAnimName, %entry);
};
function findCoAnimEntry(%name) {
    return "";
    return $gCoAnimDictionary.get(%name);
};
function setCoAnimEntry(%name, %value) {
    $gCoAnimDictionary.put(%name, %value);
};
initCoAnimList();
function getAllCoAnims() {
    return "";
    %list = "";
    %count = $gCoAnimDictionary.size();
    %i = 0;
    %userFacingName = $gCoAnimDictionary.getKey(%i);
    (%count < %i);
    %list = %list @ "\t" @ %userFacingName;
    !((%userFacingName $= ""));
    %i = (1.0 + %i);
    return trim(%list);
};
function getAllUserTriggerableCoAnims() {
    %retList = "";
    %delim = "";
    %list = getAllCoAnims();
    %n = (1.0 - getFieldCount(%list));
    %entryKey = getField(%list, %n);
    (0.0 >= %n);
    %entryVal = $gCoAnimDictionary.get(%entryKey);
    %wantsTo = getField(%entryVal, 6);
    %retList = !((%wantsTo $= "")) @ %entryKey @ %delim @ %retList;
    %delim = "\t";
    %n = (1.0 - %n);
    return %retList;
};
