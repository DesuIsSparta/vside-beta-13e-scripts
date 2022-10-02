function initCoAnimList()
{
    addCoAnim("hug", "hug", 0, 0, 4, "-0.04 1.10 0 0 0 1 3.14159", 0, "hug you", "T");
    addCoAnim("slowdance", "slodance", 0, 0, 4, "-0.04 1.10 0 0 0 1 3.14159", 0, "slowdance with you", "T");
    addCoAnim("kickballs", "kickball", 0, 0, 4, "-0.04 1.10 0 0 0 1 3.14159", 0, "kick you in the balls", "T");
    addCoAnim("deepkiss", "deepkiss", 0, 0, 4, "-0.04 1.10 0 0 0 1 3.14159", 0, "deepkiss you", "T");
    addCoAnim("shake hands", "handshake", 0, 0, 4, "0 1.1 0 0 0 1 3.14159", 0, "shake your hand", "T");
    addCoAnim("kiss", "kiss", 0, 0, 4, "0 1.1 0 0 0 1 3.14159", 0, "kiss you", "T");
    addCoAnim("gift", "giveloot", 0, 0, 4, "0 1.1 0 0 0 1 3.14159", 0, "", "T");
    addCoAnim("giveDrink", "givedrink", 0, 0, 4, "0 0   0 0 0 1 3.14159", 0, "", "F");
    addCoAnim("makeDrink", "makedrink", 0, 0, 4, "0 0   0 0 0 1 3.14159", 0, "", "F");
    return ;
}
$gCoAnimDictionary = 0;
function addCoAnim(%coAnimName, %anim, %delayA, %delayB, %range, %relativeTransform, %minLevel, %requestText, %moveMode)
{
    if (!isObject($gCoAnimDictionary))
    {
        $gCoAnimDictionary = new StringMap();
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add($gCoAnimDictionary);
        }
    }
    %entry = "" @ %anim TAB %delayA TAB %delayB TAB %range TAB %relativeTransform TAB %minLevel TAB %requestText TAB %moveMode;
    setCoAnimEntry(%coAnimName, %entry);
    return ;
}
function setCoAnimSkuPeriod(%coAnimName, %whichPlayer, %specialSkuName, %startMS, %stopMS)
{
    %entry = findCoAnimEntry(%coAnimName);
    if (%entry $= "")
    {
        error(getScopeName() SPC "- no such coAnim:" SPC %coAnimName SPC getTrace());
        return ;
    }
    %skuPeriods = getField(%entry, 6);
    if (!(%skuPeriods $= ""))
    {
        %skuPeriods = %skuPeriods @ " ";
    }
    %skuPeriods = %skuPeriods @ %whichPlayer SPC %specialSkuName SPC %startMS SPC %stopMS;
    %entry = setField(%entry, %skuPeriods);
    setCoAnimEntry(%coAnimName, %entry);
    return ;
}
function findCoAnimEntry(%name)
{
    if (!isObject($gCoAnimDictionary))
    {
        return "";
    }
    return $gCoAnimDictionary.get(%name);
}
function setCoAnimEntry(%name, %value)
{
    $gCoAnimDictionary.put(%name, %value);
    return ;
}
initCoAnimList();
function getAllCoAnims()
{
    if (!isObject($gCoAnimDictionary))
    {
        return "";
    }
    %list = "";
    %count = $gCoAnimDictionary.size();
    %i = 0;
    while (%i < %count)
    {
        %userFacingName = $gCoAnimDictionary.getKey(%i);
        if (!(%userFacingName $= ""))
        {
            %list = %list TAB %userFacingName;
        }
        %i = %i + 1;
    }
    return trim(%list);
}
function getAllUserTriggerableCoAnims()
{
    %retList = "";
    %delim = "";
    %list = getAllCoAnims();
    %n = getFieldCount(%list) - 1;
    while (%n >= 0)
    {
        %entryKey = getField(%list, %n);
        %entryVal = $gCoAnimDictionary.get(%entryKey);
        %wantsTo = getField(%entryVal, 6);
        if (!(%wantsTo $= ""))
        {
            %retList = %entryKey @ %delim @ %retList;
            %delim = "\t";
        }
        %n = %n - 1;
    }
    return %retList;
}
