function InstrumentRegistry::initializeRegistryCommon(%this) {
    %this.defaultStopAnimation = "idl1a";
    %this.stopAnimationsList = 0 @ new StringMap("");;
    if (isObject(MissionCleanup)) {
        %this.stopAnimationsList.add(MissionCleanup);
    }
};
function InstrumentRegistry::clearRegistryCommon(%this) {
    if (isObject(%this.stopAnimationsList)) {
        %this.stopAnimationsList.clear();
    }
};
function InstrumentRegistry::closeRegistryCommon(%this) {
    if (isObject(%this.stopAnimationsList)) {
        %this.stopAnimationsList.delete();
        %this.stopAnimationsList = "";
    }
};
function InstrumentRegistry::registerCommonInstrumentProperties(%this, %instrumentName, %instrumentFemaleSku, %instrumentMaleSku, %instrumentNeuterSku, %instrumentGenre, %animationGenrePrefix, %rootAnim, %runAnim, %sideAnim, %backAnim, %jumpAnim) {
    %instrument = %instrumentName.get(%this.instrumentsList);
    if (!(isObject(%instrument))) {
        warn(getScopeName() @ " " @ "- cannot find instrument '" @ %instrumentName @ "'");
        return;
    }
    %instrument.skus = %instrumentFemaleSku @ "f";
    %instrument.skus = %instrumentMaleSku @ "m";
    %instrument.skus = %instrumentNeuterSku @ "n";
    %instrument.genre = %instrumentGenre;
    %instrument.animationMaps = makeAnimationMapInstrument("f", %instrumentGenre, %animationGenrePrefix @ %rootAnim, %animationGenrePrefix @ %runAnim, %animationGenrePrefix @ %sideAnim, %animationGenrePrefix @ %backAnim, %animationGenrePrefix @ %jumpAnim) @ "f";
    %instrument.animationMaps = makeAnimationMapInstrument("m", %instrumentGenre, %animationGenrePrefix @ %rootAnim, %animationGenrePrefix @ %runAnim, %animationGenrePrefix @ %sideAnim, %animationGenrePrefix @ %backAnim, %animationGenrePrefix @ %jumpAnim) @ "m";
    %rootAnim.put(%this.stopAnimationsList, %instrumentName);
};
function InstrumentRegistry::getInstrumentCount(%this) {
    return %this.instrumentsList.size();
};
function InstrumentRegistry::getInstrumentByIndex(%this, %index) {
    if ((%index < 0.0)) {
    }
    if ((%index >= %this.instrumentsList.size())) {
        warn(getScopeName() @ " " @ "- bad index value =" @ " " @ %index);
        return "";
    }
    return %index.getValue(%this.instrumentsList);
};
function InstrumentRegistry::getInstrumentBySku(%this, %sku) {
    if ((%sku $= "")) {
        return "";
    }
    %i = (%this.getInstrumentCount() - 1.0);
    while ((%i >= 0.0)) {
        %instrument = %i.getInstrumentByIndex(%this);
        if (("f" @ " " @ %instrument.skus $= %sku)) {
        }
        if (("m" @ " " @ %instrument.skus $= %sku)) {
        }
        if (("n" @ " " @ %instrument.skus $= %sku)) {
            return %instrument;
        }
        %i = (%i - 1.0);
    }
    return "";
};
function InstrumentRegistry::getInstrumentObject(%this, %instrumentName) {
    %instrument = %instrumentName.get(%this.instrumentsList);
    if (!(isObject(%instrument))) {
        warn(getScopeName() @ " " @ "- cannot find instrument '" @ %instrumentName @ "'");
        return "";
    }
    return %instrument;
};
function InstrumentRegistry::isInstrument(%this, %instrumentNameOrObject) {
    if (isObject(%instrumentNameOrObject)) {
        %instrumentName = %instrumentNameOrObject.name;
    }
    %instrumentName = %instrumentNameOrObject;
    return %instrumentName.hasKey(%this.instrumentsList);
};
function InstrumentRegistry::isInstrumentGenre(%this, %genre) {
    if ((%genre $= "")) {
        return 0;
    }
    %i = (%this.getInstrumentCount() - 1.0);
    while ((%i >= 0.0)) {
        %instrument = %i.getInstrumentByIndex(%this);
        if ((%instrument.genre $= %genre)) {
            return 1;
        }
        %i = (%i - 1.0);
    }
    return 0;
};
function InstrumentRegistry::isInstrumentSku(%this, %sku) {
    if ((%sku $= "")) {
        return 0;
    }
    %i = (%this.getInstrumentCount() - 1.0);
    while ((%i >= 0.0)) {
        %instrument = %i.getInstrumentByIndex(%this);
        if (("f" @ " " @ %instrument.skus $= %sku)) {
        }
        if (("m" @ " " @ %instrument.skus $= %sku)) {
        }
        if (("n" @ " " @ %instrument.skus $= %sku)) {
            return 1;
        }
        %i = (%i - 1.0);
    }
    return 0;
};
function InstrumentRegistry::isStopAnimation(%this, %animationName) {
    return %animationName.hasValue(%this.stopAnimationsList);
};
function InstrumentRegistry::getStopAnimation(%this, %instrumentName) {
    if (!(%instrumentName.isInstrument(%this))) {
        error(getScopeName() @ " " @ "- could not find instrument '" @ %instrumentName @ "'");
        return "";
    }
    if (!(%instrumentName.hasKey(%this.stopAnimationsList))) {
        error(getScopeName() @ " " @ "- could not find stopAnimation for existing instrument '" @ %instrumentName @ "'");
        return "";
    }
    return %instrumentName.get(%this.stopAnimationsList);
};
function InstrumentRegistry::getSku(%this, %instrumentName, %gender) {
    %instrument = %instrumentName.get(%this.instrumentsList);
    if (!(isObject(%instrument))) {
        warn(getScopeName() @ " " @ "- cannot find instrument '" @ %instrumentName @ "'");
        return "";
    }
    return %instrument.skus;
};
function InstrumentRegistry::registerCommonProperties(%this) {
    "gtrglrjmp01".registerCommonInstrumentProperties(%this, "lguitar", 6051, 33704, "", "g", "n", "gtrglridl1", "gtrglrwlkf01", "gtrglrside01", "gtrglrwlkb01");
    "gtrglrjmp01".registerCommonInstrumentProperties(%this, "rguitar", 6114, 33764, "", "r", "n", "gtrglridl1", "gtrglrwlkf01", "gtrglrside01", "gtrglrwlkb01");
    "gtrglrjmp01".registerCommonInstrumentProperties(%this, "bassa", 6115, 33765, "", "a", "n", "gtrglridl1", "gtrglrwlkf01", "gtrglrside01", "gtrglrwlkb01");
    "gtrglrjmp01".registerCommonInstrumentProperties(%this, "druma", 6116, 33766, "", "d", "n", "gtrglridl1", "gtrglrwlkf01", "gtrglrside01", "gtrglrwlkb01");
};
