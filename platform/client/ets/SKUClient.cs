function Player::onGotSKUs(%this) {
    %this.currentBaseActiveSkus = %this.getActiveSKUs();
    if (%this.hasMicrophone()) {
        0.2.setBlendTargetValue(%this, $BB_UPPR_MICROPHONE);
        0.triggerBoneBlendAnimation(%this, $BB_UPPR_MICROPHONE, 1);
    }
    0.setBlendTargetValue(%this, $BB_UPPR_MICROPHONE);
    0.triggerBoneBlendAnimation(%this, $BB_UPPR_MICROPHONE, 0);
    if (!(%this.getShapeName() $= $Player::Name)) {
        return;
    }
    %skus = %this.getActiveSKUs();
    %gender = %this.getGender();
    %outfitName = "currentOutfit".get($gOutfits);
    %skus.filterSkusForBody(SkuManager).put($gOutfits, %gender @ "Body");
    %skus.filterSkusForClothing(SkuManager).put($gOutfits, %gender @ %outfitName);
    $Player::IsInHelpMeMode = %this.isInHelpMeMode();
    if (isObject(SalonStyleSelector)) {
    }
    if (SalonStyleSelector.isVisible()) {
        SalonStyleSelector.refreshAvailableStyles();
    }
    %instrumentGenre = "";
    if (isObject(ApplauseMeterGui)) {
        %usingInstrument = 0;
        %i = (InstrumentRegistryClient.getInstrumentCount() - 1.0);
        if ((%i >= 0.0)) {
        }
        while (!(%usingInstrument)) {
            %instrument = %i.getInstrumentByIndex(InstrumentRegistryClient);
            if (hasWord(%skus, %gender, %instrument.skus)) {
                %instrument.name.open(ApplauseMeterGui, "instrument");
                %instrumentGenre = %instrument.genre;
                %usingInstrument = 1;
            }
            %i = (%i - 1.0);
            if ((%i >= 0.0)) {
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
    %propGenre = %propSku.get(PropGenreMap);
    if ((%propGenre $= "")) {
        error(getScopeName() @ " " @ "- could not find genre for sku" @ " " @ %propSku @ " " @ "using y." @ " " @ getTrace());
        %propGenre = "y";
    }
    %currentGenreIsInstrumentGenre = %currentGenre.isInstrumentGenre(InstrumentRegistryClient);
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
    %n = (getWordCount(%skus) - 1.0);
    while ((%n >= 0.0)) {
        %sku = getWord(%skus, %n);
        if (!(hasWord(%this.prevActiveSkus, %sku))) {
            trySkuNotification(%sku);
        }
        %sku.trySkuEffectsClient(%this);
        %n = (%n - 1.0);
    }
    %this.prevActiveSkus = (%n >= 0.0) @ %this.getActiveSKUs();
};
function Player::applySkuBadge(%this, %skunum) {
    %prevSkuBadge = gGetField(%this);
    prevSkuBadge;
    if ((%prevSkuBadge == %skunum)) {
        return;
    }
    gSetField(%this, prevSkuBadge, %skunum);
    %this.updateMapIcon();
    %hudCtrl = %this.hudCtrl;
    if (!(isObject(%hudCtrl))) {
        return;
    }
    if ((%this == $player)) {
        trySkuNotification(%skunum);
    }
    if ((%skunum == 0.0)) {
    }
    if ("hideBadges".rolesPermissionCheckNoWarn(%this)) {
        if (isObject(%hudCtrl.badge)) {
            0.setVisible(%hudCtrl.badge);
        }
        return;
    }
    %si = %skunum.findBySku(SkuManager);
    if (!(isObject(%si))) {
        return;
    }
    if (!(isObject(%hudCtrl.badge))) {
        %ctrl = new GuiBitmapCtrl("") {
            profile = 0 @ "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 0";
            extent = "64 64";
            minExtent = "64 64";
            sluggishness = -1;
            visible = 1;
        };
        %ctrl.add(%hudCtrl);
        %hudCtrl.badge = %ctrl;
    }
    %bitmap = getBitmapFilename("badge", getWord(%si.getTxtrNames(), 0));
    %bitmap.setBitmap(%hudCtrl.badge);
    1.setVisible(%hudCtrl.badge);
};
$gSkuNotificationsMap = 0;
function initSkuNotificationsMap() {
    if (isObject($gSkuNotificationsMap)) {
        return;
    }
    $gSkuNotificationsMap = safeNewScriptObject("StringMap", "", 1);
    "displayMicrophoneHelp();".put($gSkuNotificationsMap, 17002);
    "displayMicrophoneHelp();".put($gSkuNotificationsMap, 27002);
};
function trySkuNotification(%skunum) {
    initSkuNotificationsMap();
    %cmd = %skunum.get($gSkuNotificationsMap);
    if (!(%cmd $= "")) {
        eval(%cmd);
    }
};
function Player::resetSkuEffectsClient(%this) {
    0.staggerSetAmount(%this);
};
function Player::trySkuEffectsClient(%this, %sku) {
    %si = %sku.findBySku(SkuManager);
    if (0) {
    }
    if (hasWord(%si.tags, "stagger3")) {
        0.1.staggerSetAmount(%this);
    }
    if (hasWord(%si.tags, "stagger2")) {
        0.05.staggerSetAmount(%this);
    }
    if (hasWord(%si.tags, "stagger1")) {
        0.01.staggerSetAmount(%this);
    }
};
function SkuItem::getBitmapPath(%this) {
    if ((%this.skuType $= "swatch")) {
        %ret = getBitmapFilename(%this.skuType, getWord(%this.getTxtrNames(), 1));
    }
    %ret = getBitmapFilename(%this.skuType, getWord(%this.getTxtrNames(), 0));
};
