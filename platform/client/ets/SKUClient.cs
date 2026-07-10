function Player::onGotSKUs(%this) {
    currentBaseActiveSkus = %this.getActiveSKUs() @ %this;
    if (%this.hasMicrophone()) {
        %this.setBlendTargetValue($BB_UPPR_MICROPHONE, 0.2);
        %this.triggerBoneBlendAnimation($BB_UPPR_MICROPHONE, 1, 0);
    }
    %this.setBlendTargetValue($BB_UPPR_MICROPHONE, 0);
    %this.triggerBoneBlendAnimation($BB_UPPR_MICROPHONE, 0, 0);
    if (!(%this.getShapeName() $= $Player::Name)) {
        return;
    }
    %skus = %this.getActiveSKUs();
    %gender = %this.getGender();
    %outfitName = $gOutfits.get("currentOutfit");
    $gOutfits.put(%gender @ "Body", %skus.filterSkusForBody());
    $gOutfits.put(SkuManager @ %gender @ %outfitName, %skus.filterSkusForClothing());
    $Player::IsInHelpMeMode = %this.isInHelpMeMode();
    SkuManager;
    if (isObject()) {
    }
    if (isVisible()) {
        refreshAvailableStyles();
    }
    %instrumentGenre = "";
    SalonStyleSelector;
    if (isObject()) {
        %usingInstrument = 0;
        ApplauseMeterGui;
        %i = (InstrumentRegistryClient - getInstrumentCount());
        1.0;
        if ((0.0 >= %i)) {
        }
        if (!(%usingInstrument)) {
            %instrument = %i.getInstrumentByIndex();
            InstrumentRegistryClient;
            if (hasWord(%skus, skus)) {
                "instrument".open(name);
                %instrumentGenre = genre;
                %instrument;
                %usingInstrument = 1;
                %instrument;
            }
            %i = (1.0 - %i);
            ApplauseMeterGui;
            if ((0.0 >= %i)) {
            }
        }
        if (!(%usingInstrument)) {
        }
        if ((ApplauseMeterGui SPC applauseMeterUse $= "instrument")) {
            closingFromServer = !(%usingInstrument) @ 1 @ ApplauseMeterGui;
            SalonStyleSelector @ %gender @ %instrument;
            close();
        }
    }
    %propSku = %this.getActivePropSku();
    ApplauseMeterGui;
    %currentGenre = %this.getGenre();
    SalonStyleSelector;
    if ((%propSku $= "")) {
        if (isPropGenre(%currentGenre)) {
            %propGenre = %currentGenre;
        }
        %propGenre = "y";
    }
    %propGenre = %propSku.get();
    PropGenreMap;
    if ((%propGenre $= "")) {
        error(getScopeName() @ " " @ "- could not find genre for sku" @ " " @ %propSku @ " " @ "using y." @ " " @ getTrace());
        %propGenre = "y";
    }
    %currentGenreIsInstrumentGenre = %currentGenre.isInstrumentGenre();
    InstrumentRegistryClient;
    %currentGenreIsPropGenre = isPropGenre(%currentGenre);
    if (!(%instrumentGenre $= "")) {
    }
    if (!(%currentGenre $= %instrumentGenre)) {
        commandToServer('EnterSpecialGenre', %instrumentGenre);
    }
    if ((%propSku $= "")) {
    }
    if (%currentGenreIsInstrumentGenre) {
        commandToServer('ExitSpecialGenre', %currentGenre);
    }
    if (!(%propSku $= "")) {
    }
    if ((%instrumentGenre $= "")) {
    }
    if (!(%currentGenreIsPropGenre)) {
        commandToServer('EnterSpecialGenre', %propGenre);
    }
    if ((%propSku $= "")) {
    }
    if (%currentGenreIsPropGenre) {
        commandToServer('ExitSpecialGenre', %propGenre);
    }
    if (!(%propSku $= "")) {
    }
    if (%currentGenreIsPropGenre) {
    }
    if (!(%currentGenre $= %propGenre)) {
        commandToServer('SwitchSpecialGenre', %currentGenre, %propGenre);
    }
    if ((%propSku $= "")) {
        if (%currentGenreIsPropGenre) {
        }
        if (%currentGenreIsInstrumentGenre) {
        }
    }
    if (!(%instrumentGenre $= "")) {
    }
    if (!(%currentGenre $= $UserPref::Player::Genre)) {
        commandToServer('setGenre', $UserPref::Player::Genre);
    }
    updateHelpMeModeMenu();
    updateModeIcon();
    %this.resetSkuEffectsClient();
    %n = (1.0 - getWordCount(%skus));
    MessageHud;
    if ((0.0 >= %n)) {
        %sku = getWord(%skus, %n);
        if (!(hasWord(prevActiveSkus, %sku))) {
            trySkuNotification(%sku);
        }
        %this.trySkuEffectsClient(%sku);
        %n = (1.0 - %n);
        %this;
    }
    prevActiveSkus = (0.0 >= %n) @ %this.getActiveSKUs() @ %this;
};
function Player::applySkuBadge(%this, %skunum) {
    %prevSkuBadge = gGetField(%this);
    prevSkuBadge;
    if ((%skunum == %prevSkuBadge)) {
        return;
    }
    gSetField(%this, %skunum);
    %this.updateMapIcon();
    %hudCtrl = hudCtrl;
    %this;
    if (!(isObject(%hudCtrl))) {
        return prevSkuBadge;
    }
    if (($player == %this)) {
        trySkuNotification(%skunum);
    }
    if ((0.0 == %skunum)) {
    }
    if (%this.rolesPermissionCheckNoWarn("hideBadges")) {
        if (isObject(badge)) {
            badge.setVisible(0);
        }
        return %hudCtrl;
    }
    %si = %skunum.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        return;
    }
    if (!(isObject(badge))) {
        profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
        0;
        horizSizing = %hudCtrl @ "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "64 64";
        minExtent = "64 64";
        sluggishness = -1;
        visible = 1;
        %ctrl = ;
        %hudCtrl.add(%ctrl);
        badge = %ctrl @ %hudCtrl;
    }
    %bitmap = getBitmapFilename("badge", getWord(%si.getTxtrNames(), 0));
    badge.setBitmap(%bitmap);
    badge.setVisible(1);
};
$gSkuNotificationsMap = 0;
function initSkuNotificationsMap() {
    if (isObject($gSkuNotificationsMap)) {
        return;
    }
    $gSkuNotificationsMap = safeNewScriptObject("StringMap", "", 1);
    $gSkuNotificationsMap.put(17002, "displayMicrophoneHelp();");
    $gSkuNotificationsMap.put(27002, "displayMicrophoneHelp();");
};
function trySkuNotification(%skunum) {
    initSkuNotificationsMap();
    %cmd = $gSkuNotificationsMap.get(%skunum);
    if (!(%cmd $= "")) {
        eval(%cmd);
    }
};
function Player::resetSkuEffectsClient(%this) {
    %this.staggerSetAmount(0);
};
function Player::trySkuEffectsClient(%this, %sku) {
    %si = %sku.findBySku();
    SkuManager;
    if (0) {
    }
    if (hasWord(tags, "stagger3")) {
        %this.staggerSetAmount(0.1);
    }
    if (hasWord(tags, "stagger2")) {
        %this.staggerSetAmount(0.05);
    }
    if (hasWord(tags, "stagger1")) {
        %this.staggerSetAmount(0.01);
    }
};
function SkuItem::getBitmapPath(%this) {
    if ((%this SPC skuType $= "swatch")) {
        %ret = getBitmapFilename(skuType, getWord(%this.getTxtrNames(), 1));
        %this;
    }
    %ret = getBitmapFilename(skuType, getWord(%this.getTxtrNames(), 0));
    %this;
};
