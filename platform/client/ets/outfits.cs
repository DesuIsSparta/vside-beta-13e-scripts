$gOutfits = "";
$gOutfitsDefault = "";
function outfits_init() {
    if (!($gOutfits $= "")) {
        $gOutfits.delete();
        $gOutfitsDefault.delete();
    }
    $gOutfits = new ""();
    StringMap;
    $gOutfitsDefault = new ""();
    StringMap;
    outfits_makeDefault($gOutfitsDefault);
    $gOutfits.duplicate($gOutfitsDefault);
    checkOutfitCorruption(0);
    if ($StandAlone) {
        return 0;
    }
};
function outfits_makeDefault(%stringMap) {
    %stringMap.clear();
    %genders = "f m";
    %outfits = $gAllOutfits;
    %m = (1.0 - getWordCount(%genders));
    if ((0.0 >= %m)) {
        %gender = getWord(%genders, %m);
        %n = (1.0 - getWordCount(%outfits));
        if ((0.0 >= %n)) {
            %name = %gender @ getWord(%outfits, %n);
            %stringMap.put(%name, %name[$gNewStockOutfits @ %name]);
            %n = (1.0 - %n);
        }
        %name = (0.0 >= %n) @ %gender @ "Body";
        %stringMap.put(%name, %gender[$gDefaultBodyAttrs @ %gender]);
        %m = (1.0 - %m);
    }
    if (isObject($player)) {
        %gender = $player.getGender();
        (0.0 >= %m);
    }
    %gender = $UserPref::Player::gender;
    %stringMap.put("currentOutfit", "A");
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
    if ((%num < %n)) {
        getWord(%skus, %n).findBySku().dumpEts();
        %n = (1.0 + %n);
        SkuManager;
    }
};
function outfits_getCurrentSkus() {
    %outfitName = $gOutfits.get("currentOutfit");
    %clothing = $gOutfits.get($player.getGender() @ %outfitName);
    %body = $gOutfits.get($player.getGender() @ "Body");
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
    %stringMap = new ""();
    StringMap;
    %num = %request.getValue("propertyCount");
    0;
    %n = 0;
    if ((%num < %n)) {
        %key = %request.getValue("property" @ %n @ ".key");
        %value = %request.getValue("property" @ %n @ ".value");
        %value = outfits_filterSKUList(%value);
        %stringMap.put(%key, %value);
        %n = (1.0 + %n);
    }
    echo("Retrieved outfit settings:");
    %stringMap.dumpValues();
    if (!((%num < %n) SPC %stringMap.get("initialOutfitAndBody") $= "")) {
    }
    if ((%stringMap.get("currentOutfit") $= "")) {
        $Player::Name[$userpref::player::initialSkus @ $Player::Name] = %stringMap.get("initialOutfitAndBody");
        if (!(SkuManager SPC $Player::Name[$userpref::player::initialSkus @ $Player::Name].filterSkusGender("f") $= $Player::Name[$userpref::player::initialSkus @ $Player::Name])) {
            %skusGender = "m";
        }
        %skusGender = "f";
        if (!(%skusGender $= $UserPref::Player::gender)) {
            error(getScopeName() @ " " @ "incoming SKUs do not match gender. outfit will likely be old-school default.");
        }
        $Player::Name[$userpref::player::initialSkusGender @ $Player::Name] = $UserPref::Player::gender;
        %stringMap.clear();
    }
    if ((0.0 == %stringMap.size())) {
    }
    if (!($Player::Name[$userpref::player::initialSkus @ $Player::Name] $= "")) {
        if (($Player::Name[$userpref::player::initialSkusGender @ $Player::Name] $= $UserPref::Player::gender)) {
            %skusBody = $Player::Name[$userpref::player::initialSkus @ $Player::Name].filterSkusForBody();
            SkuManager;
            %skusOutfit = $Player::Name[$userpref::player::initialSkus @ $Player::Name].filterSkusForClothing();
            SkuManager;
            %stringMap.put("currentOutfit", "A");
            %stringMap.put($UserPref::Player::gender @ "Body", %skusBody);
            %stringMap.put($UserPref::Player::gender @ "A", %skusOutfit);
            schedule(500, 0, "outfits_persist");
        }
        error(getScopeName() @ " " @ "- got" @ " " @ $UserPref::Player::gender @ " " @ "expected" @ " " @ $Player::Name[$userpref::player::initialSkusGender @ $Player::Name]);
        deleteVariables("$userpref::player::initialSkus" @ $Player::Name);
        deleteVariables("$userpref::player::initialSkusGender" @ $Player::Name);
    }
    $gOutfits.import(%stringMap);
    if (checkOutfitCorruption(0)) {
        error(getScopeName() @ "->final post-outfits_retrieve test failed");
    }
    %stringMap.delete();
};
function outfits_getCurrentSkus() {
    %gender = $UserPref::Player::gender;
    %currentOutfit = %gender @ $gOutfits.get("currentOutfit");
    %currentBody = %gender @ "Body";
    %clothing = $gOutfits.get(%currentOutfit);
    %body = $gOutfits.get(%currentBody);
    return %clothing @ " " @ %body;
};
function outfits_filterSKUList(%skulist) {
    %helpmesku = getSpecialSKU(0, "helpmebadge");
    %filtered = "";
    %skulist = trim(%skulist);
    %idx = 0;
    if ((getWordCount(%skulist) < %idx)) {
        %sku = getWord(%skulist, %idx);
        if (!(%sku $= %helpmesku)) {
            %filtered = %filtered @ " " @ %sku;
        }
        %idx = (1.0 + %idx);
    }
    %filtered = trim(%filtered);
    (getWordCount(%skulist) < %idx);
    return %filtered;
};
function SaveOutfitAndBodySkusAsCurrent(%skus) {
    %gender = $player.getGender();
    %skus = %skus.filterSkusGender(%gender);
    SkuManager;
    %skusBody = %skus.filterSkusForBody();
    SkuManager;
    %skusOutfit = %skus.filterSkusForClothing();
    SkuManager;
    %keyBody = %gender @ "Body";
    %keyOutfit = %gender @ $gOutfits.get("currentOutfit");
    $gOutfits.put(%keyBody, %skusBody);
    $gOutfits.put(%keyOutfit, %skusOutfit);
    outfits_persist();
    $player.setActiveSKUs(%skus);
    commandToServer('SetActiveSkus', %skus);
};
function Player::switchOutfitTo(%unused, %outfitName) {
    %idx = findWord(, $player.getGender() @ %outfitName);
    if ((-(1.0) == %idx)) {
        warn(getScopeName() @ "->Trying to change to an outfit not in $Player::HangerNames");
    }
    if (!($gOutfits.hasKey($player.getGender() @ %outfitName))) {
        error(getScopeName() @ "->No key in $gOutfits for requested outfit! Cancelling outfit change!");
        return 0;
    }
    if (($gOutfits.get("currentOutfit") $= %outfitName)) {
        echo(getScopeName() @ "->Trying to change outfit to already selected outfit, returning.");
        return 1;
    }
    $gOutfits.put("currentOutfit", %outfitName);
    %outfitSkus = $gOutfits.get($player.getGender() @ %outfitName);
    %bodySkus = $gOutfits.get($player.getGender() @ "Body");
    %activeSkus = %outfitSkus @ " " @ %bodySkus;
    %helpmesku = getSpecialSKU(0, "helpmebadge");
    %idx = findWord(%activeSkus, %helpmesku);
    if ($player.isInHelpMeMode()) {
        if ((-(1.0) == %idx)) {
            %activeSkus = %activeSkus @ " " @ %helpmesku;
        }
    }
    if ((0.0 >= %idx)) {
        %activeSkus = removeWord(%activeSkus, %idx);
    }
    commandToServer('SetActiveSkus', %activeSkus);
    return 1;
};
