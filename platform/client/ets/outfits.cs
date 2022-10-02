$gOutfits = "";
$gOutfitsDefault = "";
function outfits_init()
{
    if (!($gOutfits $= ""))
    {
        $gOutfits.delete();
        $gOutfitsDefault.delete();
    }
    $gOutfits = new StringMap();
    $gOutfitsDefault = new StringMap();
    outfits_makeDefault($gOutfitsDefault);
    $gOutfits.duplicate($gOutfitsDefault);
    checkOutfitCorruption(0);
    if ($StandAlone)
    {
        return ;
    }
    return ;
}
function outfits_makeDefault(%stringMap)
{
    %stringMap.clear();
    %genders = "f m";
    %outfits = $gAllOutfits;
    %m = getWordCount(%genders) - 1;
    while (%m >= 0)
    {
        %gender = getWord(%genders, %m);
        %n = getWordCount(%outfits) - 1;
        while (%n >= 0)
        {
            %name = %gender @ getWord(%outfits, %n);
            %stringMap.put(%name, $gNewStockOutfits[%name]);
            %n = %n - 1;
        }
        %name = %gender @ "Body";
        %stringMap.put(%name, $gDefaultBodyAttrs[%gender]);
        %m = %m - 1;
    }
    if (isObject($player))
    {
        %gender = $player.getGender();
    }
    else
    {
        %gender = $UserPref::Player::gender;
    }
    %stringMap.put("currentOutfit", "A");
    return ;
}
function outfits_persist()
{
    if (checkOutfitCorruption(0))
    {
        error("outfits_persist outfits test failed, not persisting.");
        return 0;
    }
    if (haveValidManagerHost())
    {
        sendRequest_UpdateUserInventoryCollection($Player::Name, "outfits", $gOutfits, "");
    }
    return ;
}
function outfits_retrieve()
{
    if (checkOutfitCorruption(0))
    {
        error("pre-outfits_retrieve outfit test failed!");
    }
    if (haveValidManagerHost())
    {
        sendRequest_GetUserInventoryCollection($Player::Name, "outfits", "outfits_onDoneOrErrorCallback_GetUserInventoryCollection");
    }
    return ;
}
function outfits_dumpCurrent()
{
    %skus = outfits_getCurrentSkus();
    %num = getWordCount(%skus);
    %n = 0;
    while (%n < %num)
    {
        SkuManager.findBySku(getWord(%skus, %n)).dumpEts();
        %n = %n + 1;
    }
}

function outfits_getCurrentSkus()
{
    %outfitName = $gOutfits.get("currentOutfit");
    %clothing = $gOutfits.get($player.getGender() @ %outfitName);
    %body = $gOutfits.get($player.getGender() @ "Body");
    %skus = %clothing SPC %body;
    return %skus;
}
$gRetrievedOutfits = 0;
function outfits_onDoneOrErrorCallback_GetUserInventoryCollection(%request)
{
    if (!%request.checkSuccess())
    {
        warn(getScopeName() @ "->outfits request failed!");
        checkOutfitCorruption(0);
        return ;
    }
    $gRetrievedOutfits = 1;
    %stringMap = new StringMap();
    %num = %request.getValue("propertyCount");
    %n = 0;
    while (%n < %num)
    {
        %key = %request.getValue("property" @ %n @ ".key");
        %value = %request.getValue("property" @ %n @ ".value");
        %value = outfits_filterSKUList(%value);
        %stringMap.put(%key, %value);
        %n = %n + 1;
    }
    echo("Retrieved outfit settings:");
    %stringMap.dumpValues();
    if (!((%stringMap.get("initialOutfitAndBody") $= "")) && (%stringMap.get("currentOutfit") $= ""))
    {
        $userpref::player::initialSkus[$Player::Name] = %stringMap.get("initialOutfitAndBody") ;
        if (!(SkuManager.filterSkusGender($userpref::player::initialSkus[$Player::Name], "f") $= $userpref::player::initialSkus[$Player::Name]))
        {
            %skusGender = "m";
        }
        else
        {
            %skusGender = "f";
        }
        if (!(%skusGender $= $UserPref::Player::gender))
        {
            error(getScopeName() SPC "incoming SKUs do not match gender. outfit will likely be old-school default.");
        }
        $userpref::player::initialSkusGender[$Player::Name] = $UserPref::Player::gender ;
        %stringMap.clear();
    }
    if ((%stringMap.size() == 0) && !(($userpref::player::initialSkus[$Player::Name] $= "")))
    {
        if ($userpref::player::initialSkusGender[$Player::Name] $= $UserPref::Player::gender)
        {
            %skusBody = SkuManager.filterSkusForBody($userpref::player::initialSkus[$Player::Name]);
            %skusOutfit = SkuManager.filterSkusForClothing($userpref::player::initialSkus[$Player::Name]);
            %stringMap.put("currentOutfit", "A");
            %stringMap.put($UserPref::Player::gender @ "Body", %skusBody);
            %stringMap.put($UserPref::Player::gender @ "A", %skusOutfit);
            schedule(500, 0, "outfits_persist");
        }
        else
        {
            error(getScopeName() SPC "- got" SPC $UserPref::Player::gender SPC "expected" SPC $userpref::player::initialSkusGender[$Player::Name]);
        }
        deleteVariables("$userpref::player::initialSkus" @ $Player::Name);
        deleteVariables("$userpref::player::initialSkusGender" @ $Player::Name);
    }
    $gOutfits.import(%stringMap);
    if (checkOutfitCorruption(0))
    {
        error(getScopeName() @ "->final post-outfits_retrieve test failed");
    }
    %stringMap.delete();
    return ;
}
function outfits_getCurrentSkus()
{
    %gender = $UserPref::Player::gender;
    %currentOutfit = %gender @ $gOutfits.get("currentOutfit");
    %currentBody = %gender @ "Body";
    %clothing = $gOutfits.get(%currentOutfit);
    %body = $gOutfits.get(%currentBody);
    return %clothing SPC %body;
}
function outfits_filterSKUList(%skulist)
{
    %helpmesku = getSpecialSKU(0, "helpmebadge");
    %filtered = "";
    %skulist = trim(%skulist);
    %idx = 0;
    while (%idx < getWordCount(%skulist))
    {
        %sku = getWord(%skulist, %idx);
        if (!(%sku $= %helpmesku))
        {
            %filtered = %filtered SPC %sku;
        }
        %idx = %idx + 1;
    }
    %filtered = trim(%filtered);
    return %filtered;
}
function SaveOutfitAndBodySkusAsCurrent(%skus)
{
    %gender = $player.getGender();
    %skus = SkuManager.filterSkusGender(%skus, %gender);
    %skusBody = SkuManager.filterSkusForBody(%skus);
    %skusOutfit = SkuManager.filterSkusForClothing(%skus);
    %keyBody = %gender @ "Body";
    %keyOutfit = %gender @ $gOutfits.get("currentOutfit");
    $gOutfits.put(%keyBody, %skusBody);
    $gOutfits.put(%keyOutfit, %skusOutfit);
    outfits_persist();
    $player.setActiveSKUs(%skus);
    commandToServer('SetActiveSkus', %skus);
    return ;
}
function Player::switchOutfitTo(%unused, %outfitName)
{
    %idx = findWord($Player::HangerNames[$player.getGender()], $player.getGender() @ %outfitName);
    if (%idx == -1)
    {
        warn(getScopeName() @ "->Trying to change to an outfit not in $Player::HangerNames");
    }
    if (!$gOutfits.hasKey($player.getGender() @ %outfitName))
    {
        error(getScopeName() @ "->No key in $gOutfits for requested outfit! Cancelling outfit change!");
        return 0;
    }
    if ($gOutfits.get("currentOutfit") $= %outfitName)
    {
        echo(getScopeName() @ "->Trying to change outfit to already selected outfit, returning.");
        return 1;
    }
    $gOutfits.put("currentOutfit", %outfitName);
    %outfitSkus = $gOutfits.get($player.getGender() @ %outfitName);
    %bodySkus = $gOutfits.get($player.getGender() @ "Body");
    %activeSkus = %outfitSkus SPC %bodySkus;
    %helpmesku = getSpecialSKU(0, "helpmebadge");
    %idx = findWord(%activeSkus, %helpmesku);
    if ($player.isInHelpMeMode())
    {
        if (%idx == -1)
        {
            %activeSkus = %activeSkus SPC %helpmesku;
        }
    }
    else
    {
        if (%idx >= 0)
        {
            %activeSkus = removeWord(%activeSkus, %idx);
        }
    }
    commandToServer('SetActiveSkus', %activeSkus);
    return 1;
}
