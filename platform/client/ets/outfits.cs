$gOutfits = "";
$gOutfitsDefault = "";
function outfits_init() {
    if (!($gOutfits $= "")) {
        $gOutfits.delete();
        $gOutfitsDefault.delete();
    }
    $gOutfits = new StringMap("");
    $gOutfitsDefault = new StringMap("");
    outfits_makeDefault($gOutfitsDefault);
    $gOutfitsDefault.duplicate($gOutfits);
    checkOutfitCorruption(0);
    if ($StandAlone) {
        return;
    }
};
function outfits_makeDefault(%stringMap) {
    %stringMap.clear();
    %genders = "f m";
    %outfits = $gAllOutfits;
    %m = (getWordCount(%genders) - 1.0);
    while ((%m >= 0.0)) {
        %gender = getWord(%genders, %m);
        %n = (getWordCount(%outfits) - 1.0);
        while ((%n >= 0.0)) {
            %name = %gender @ getWord(%outfits, %n);
            %name[$gNewStockOutfits @ %name].put(%stringMap, %name);
            %n = (%n - 1.0);
        }
        %name = %gender @ "Body";
        (%n >= 0.0);
        %gender[$gDefaultBodyAttrs @ %gender].put(%stringMap, %name);
        %m = (%m - 1.0);
    }
    if (isObject($player)) {
        %gender = $player.getGender();
        (%m >= 0.0);
    }
    %gender = $UserPref::Player::gender;
    "A".put(%stringMap, "currentOutfit");
};
function outfits_persist() {
    if (checkOutfitCorruption(0)) {
        error("outfits_persist outfits test failed, not persisting.");
        return 0;
    }
    if (haveValidManagerHost()) {
        sendRequest_UpdateUserInventoryCollection($Player::Name, "outfits", $gOutfits, "");
    }
};
function outfits_retrieve() {
    if (checkOutfitCorruption(0)) {
        error("pre-outfits_retrieve outfit test failed!");
    }
    if (haveValidManagerHost()) {
        sendRequest_GetUserInventoryCollection($Player::Name, "outfits", "outfits_onDoneOrErrorCallback_GetUserInventoryCollection");
    }
};
function outfits_dumpCurrent() {
    %skus = outfits_getCurrentSkus();
    %num = getWordCount(%skus);
    %n = 0;
    while ((%n < %num)) {
        getWord(%skus, %n).findBySku(SkuManager).dumpEts();
        %n = (%n + 1.0);
    }
};
function outfits_getCurrentSkus() {
    %outfitName = "currentOutfit".get($gOutfits);
    %clothing = $player.getGender() @ %outfitName.get($gOutfits);
    %body = $player.getGender() @ "Body".get($gOutfits);
    %skus = %clothing @ " " @ %body;
    return %skus;
};
$gRetrievedOutfits = 0;
function outfits_onDoneOrErrorCallback_GetUserInventoryCollection(%request) {
    if (!(%request.checkSuccess())) {
        warn(getScopeName() @ "->outfits request failed!");
        checkOutfitCorruption(0);
        return;
    }
    $gRetrievedOutfits = 1;
    %stringMap = new StringMap("");
    %num = "propertyCount".getValue(%request);
    %n = 0;
    while ((%n < %num)) {
        %key = "property" @ %n @ ".key".getValue(%request);
        %value = "property" @ %n @ ".value".getValue(%request);
        %value = outfits_filterSKUList(%value);
        %value.put(%stringMap, %key);
        %n = (%n + 1.0);
    }
    echo("Retrieved outfit settings:");
    %stringMap.dumpValues();
    if (!((%n < %num) @ " " @ "initialOutfitAndBody".get(%stringMap) $= "")) {
    }
    if (("currentOutfit".get(%stringMap) $= "")) {
        $Player::Name[$userpref::player::initialSkus @ $Player::Name] = "initialOutfitAndBody".get(%stringMap);
        if (!("f".filterSkusGender(SkuManager, $Player::Name[$userpref::player::initialSkus @ $Player::Name]) $= $Player::Name[$userpref::player::initialSkus @ $Player::Name])) {
            %skusGender = "m";
        }
        %skusGender = "f";
        if (!(%skusGender $= $UserPref::Player::gender)) {
            error(getScopeName() @ " " @ "incoming SKUs do not match gender. outfit will likely be old-school default.");
        }
        $Player::Name[$userpref::player::initialSkusGender @ $Player::Name] = $UserPref::Player::gender;
        %stringMap.clear();
    }
    if ((%stringMap.size() == 0.0)) {
    }
    if (!($Player::Name[$userpref::player::initialSkus @ $Player::Name] $= "")) {
        if (($Player::Name[$userpref::player::initialSkusGender @ $Player::Name] $= $UserPref::Player::gender)) {
            %skusBody = $Player::Name[$userpref::player::initialSkus @ $Player::Name].filterSkusForBody(SkuManager);
            %skusOutfit = $Player::Name[$userpref::player::initialSkus @ $Player::Name].filterSkusForClothing(SkuManager);
            "A".put(%stringMap, "currentOutfit");
            %skusBody.put(%stringMap, $UserPref::Player::gender @ "Body");
            %skusOutfit.put(%stringMap, $UserPref::Player::gender @ "A");
            schedule(500, 0, "outfits_persist");
        }
        error(getScopeName() @ " " @ "- got" @ " " @ $UserPref::Player::gender @ " " @ "expected" @ " " @ $Player::Name[$userpref::player::initialSkusGender @ $Player::Name]);
        deleteVariables("$userpref::player::initialSkus" @ $Player::Name);
        deleteVariables("$userpref::player::initialSkusGender" @ $Player::Name);
    }
    %stringMap.import($gOutfits);
    if (checkOutfitCorruption(0)) {
        error(getScopeName() @ "->final post-outfits_retrieve test failed");
    }
    %stringMap.delete();
};
function outfits_getCurrentSkus() {
    %gender = $UserPref::Player::gender;
    %currentOutfit = %gender @ "currentOutfit".get($gOutfits);
    %currentBody = %gender @ "Body";
    %clothing = %currentOutfit.get($gOutfits);
    %body = %currentBody.get($gOutfits);
    return %clothing @ " " @ %body;
};
function outfits_filterSKUList(%skulist) {
    %helpmesku = getSpecialSKU(0, "helpmebadge");
    %filtered = "";
    %skulist = trim(%skulist);
    %idx = 0;
    while ((%idx < getWordCount(%skulist))) {
        %sku = getWord(%skulist, %idx);
        if (!(%sku $= %helpmesku)) {
            %filtered = %filtered @ " " @ %sku;
        }
        %idx = (%idx + 1.0);
    }
    %filtered = trim(%filtered);
    (%idx < getWordCount(%skulist));
    return %filtered;
};
function SaveOutfitAndBodySkusAsCurrent(%skus) {
    %gender = $player.getGender();
    %skus = %gender.filterSkusGender(SkuManager, %skus);
    %skusBody = %skus.filterSkusForBody(SkuManager);
    %skusOutfit = %skus.filterSkusForClothing(SkuManager);
    %keyBody = %gender @ "Body";
    %keyOutfit = %gender @ "currentOutfit".get($gOutfits);
    %skusBody.put($gOutfits, %keyBody);
    %skusOutfit.put($gOutfits, %keyOutfit);
    outfits_persist();
    %skus.setActiveSKUs($player);
    commandToServer('SetActiveSkus', %skus);
};
function Player::switchOutfitTo(%unused, %outfitName) {
    %idx = findWord($Player::HangerNames, [$player.getGender()], $player.getGender() @ %outfitName);
    if ((%idx == -(1.0))) {
        warn(getScopeName() @ "->Trying to change to an outfit not in $Player::HangerNames");
    }
    if (!($player.getGender() @ %outfitName.hasKey($gOutfits))) {
        error(getScopeName() @ "->No key in $gOutfits for requested outfit! Cancelling outfit change!");
        return 0;
    }
    if (("currentOutfit".get($gOutfits) $= %outfitName)) {
        echo(getScopeName() @ "->Trying to change outfit to already selected outfit, returning.");
        return 1;
    }
    %outfitName.put($gOutfits, "currentOutfit");
    %outfitSkus = $player.getGender() @ %outfitName.get($gOutfits);
    %bodySkus = $player.getGender() @ "Body".get($gOutfits);
    %activeSkus = %outfitSkus @ " " @ %bodySkus;
    %helpmesku = getSpecialSKU(0, "helpmebadge");
    %idx = findWord(%activeSkus, %helpmesku);
    if ($player.isInHelpMeMode()) {
        if ((%idx == -(1.0))) {
            %activeSkus = %activeSkus @ " " @ %helpmesku;
        }
    }
    if ((%idx >= 0.0)) {
        %activeSkus = removeWord(%activeSkus, %idx);
    }
    commandToServer('SetActiveSkus', %activeSkus);
    return 1;
};
