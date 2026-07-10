function Player::onGotSKUs(%this) {
    %this.currentBaseActiveSkus = %this.getActiveSKUs();
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
    $gOutfits.put(%gender @ %outfitName, %skus.filterSkusForClothing());
    $Player::IsInHelpMeMode = %this.isInHelpMeMode();
    SkuManager;
    if (isObject(SalonStyleSelector)) {
    }
    if (SalonStyleSelector.isVisible()) {
        SalonStyleSelector.refreshAvailableStyles();
    }
    %instrumentGenre = "";
    SkuManager;
    if (isObject(ApplauseMeterGui)) {
        %usingInstrument = 0;
        %i = (1.0 - InstrumentRegistryClient.getInstrumentCount());
        if ((0.0 >= %i)) {
        }
        if (!(%usingInstrument)) {
            %instrument = %i.getInstrumentByIndex();
            InstrumentRegistryClient;
            if (hasWord(%skus, %instrument.skus)) {
                "instrument".open(%instrument.name);
                %instrumentGenre = %instrument.genre;
                ApplauseMeterGui;
                %usingInstrument = 1;
                %gender;
            }
            %i = (1.0 - %i);
            if ((0.0 >= %i)) {
            }
        }
        if (!(%usingInstrument)) {
        }
        if ((ApplauseMeterGui @ " " @ %instrument.applauseMeterUse $= "instrument")) {
            %instrument.closingFromServer = 1 @ ApplauseMeterGui;
            !(%usingInstrument);
            ApplauseMeterGui.close();
        }
    }
    %propSku = %this.getActivePropSku();
    %currentGenre = %this.getGenre();
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
    MessageHud.updateModeIcon();
    %this.resetSkuEffectsClient();
    %n = (1.0 - getWordCount(%skus));
    if ((0.0 >= %n)) {
        %sku = getWord(%skus, %n);
        if (!(hasWord(%this.prevActiveSkus, %sku))) {
            trySkuNotification(%sku);
        }
        %this.trySkuEffectsClient(%sku);
        %n = (1.0 - %n);
    }
    %this.prevActiveSkus = (0.0 >= %n) @ %this.getActiveSKUs();
};
function Player::applySkuBadge(%this, %skunum) {
    %prevSkuBadge = gGetField(%this);
    prevSkuBadge;
    if ((%skunum == %prevSkuBadge)) {
        return;
    }
    gSetField(%this, %skunum);
    %this.updateMapIcon();
    %hudCtrl = %this.hudCtrl;
    prevSkuBadge;
    if (!(isObject(%hudCtrl))) {
        return;
    }
    if (($player == %this)) {
        trySkuNotification(%skunum);
    }
    if ((0.0 == %skunum)) {
    }
    if (%this.rolesPermissionCheckNoWarn("hideBadges")) {
        if (isObject(%hudCtrl.badge)) {
            %hudCtrl.badge.setVisible(0);
        }
        return;
    }
    %si = %skunum.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        return;
    }
    if (!(isObject(%hudCtrl.badge))) {
        0;
        %ctrl = new ""() {
            profile = GuiBitmapCtrl @ "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 0";
            extent = "64 64";
            minExtent = "64 64";
            sluggishness = -1;
            visible = 1;
        };
        %hudCtrl.add(%ctrl);
        %hudCtrl.badge = %ctrl;
    }
    %bitmap = getBitmapFilename("badge", getWord(%si.getTxtrNames(), 0));
    %hudCtrl.badge.setBitmap(%bitmap);
    %hudCtrl.badge.setVisible(1);
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
    if (hasWord(%si.tags, "stagger3")) {
        %this.staggerSetAmount(0.1);
    }
    if (hasWord(%si.tags, "stagger2")) {
        %this.staggerSetAmount(0.05);
    }
    if (hasWord(%si.tags, "stagger1")) {
        %this.staggerSetAmount(0.01);
    }
};
function SkuItem::getBitmapPath(%this) {
    if ((%this.skuType $= "swatch")) {
        %ret = getBitmapFilename(%this.skuType, getWord(%this.getTxtrNames(), 1));
    }
    %ret = getBitmapFilename(%this.skuType, getWord(%this.getTxtrNames(), 0));
};
