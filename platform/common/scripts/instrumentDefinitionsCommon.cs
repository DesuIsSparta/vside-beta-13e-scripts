function InstrumentRegistry::initializeRegistryCommon(%this) {
    %this.defaultStopAnimation = "idl1a";
    %this.stopAnimationsList = StringMap @ new ""();;
    0;
    if (isObject(MissionCleanup)) {
        %this.stopAnimationsList.add();
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
    %instrument = %this.instrumentsList.get(%instrumentName);
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
    %this.stopAnimationsList.put(%instrumentName, %rootAnim);
};
function InstrumentRegistry::getInstrumentCount(%this) {
    return %this.instrumentsList.size();
};
function InstrumentRegistry::getInstrumentByIndex(%this, %index) {
    if ((0.0 < %index)) {
    }
    if ((%this.instrumentsList.size() >= %index)) {
        warn(getScopeName() @ " " @ "- bad index value =" @ " " @ %index);
        return "";
    }
    return %this.instrumentsList.getValue(%index);
};
function InstrumentRegistry::getInstrumentBySku(%this, %sku) {
    if ((%sku $= "")) {
        return "";
    }
    %i = (1.0 - %this.getInstrumentCount());
    if ((0.0 >= %i)) {
        %instrument = %this.getInstrumentByIndex(%i);
        if (("f" @ " " @ %instrument.skus $= %sku)) {
        }
        if (("m" @ " " @ %instrument.skus $= %sku)) {
        }
        if (("n" @ " " @ %instrument.skus $= %sku)) {
            return %instrument;
        }
        %i = (1.0 - %i);
    }
    return "";
};
function InstrumentRegistry::getInstrumentObject(%this, %instrumentName) {
    %instrument = %this.instrumentsList.get(%instrumentName);
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
    return %this.instrumentsList.hasKey(%instrumentName);
};
function InstrumentRegistry::isInstrumentGenre(%this, %genre) {
    if ((%genre $= "")) {
        return 0;
    }
    %i = (1.0 - %this.getInstrumentCount());
    if ((0.0 >= %i)) {
        %instrument = %this.getInstrumentByIndex(%i);
        if ((%instrument.genre $= %genre)) {
            return 1;
        }
        %i = (1.0 - %i);
    }
    return 0;
};
function InstrumentRegistry::isInstrumentSku(%this, %sku) {
    if ((%sku $= "")) {
        return 0;
    }
    %i = (1.0 - %this.getInstrumentCount());
    if ((0.0 >= %i)) {
        %instrument = %this.getInstrumentByIndex(%i);
        if (("f" @ " " @ %instrument.skus $= %sku)) {
        }
        if (("m" @ " " @ %instrument.skus $= %sku)) {
        }
        if (("n" @ " " @ %instrument.skus $= %sku)) {
            return 1;
        }
        %i = (1.0 - %i);
    }
    return 0;
};
function InstrumentRegistry::isStopAnimation(%this, %animationName) {
    return %this.stopAnimationsList.hasValue(%animationName);
};
function InstrumentRegistry::getStopAnimation(%this, %instrumentName) {
    if (!(%this.isInstrument(%instrumentName))) {
        error(getScopeName() @ " " @ "- could not find instrument '" @ %instrumentName @ "'");
        return "";
    }
    if (!(%this.stopAnimationsList.hasKey(%instrumentName))) {
        error(getScopeName() @ " " @ "- could not find stopAnimation for existing instrument '" @ %instrumentName @ "'");
        return "";
    }
    return %this.stopAnimationsList.get(%instrumentName);
};
function InstrumentRegistry::getSku(%this, %instrumentName, %gender) {
    %instrument = %this.instrumentsList.get(%instrumentName);
    if (!(isObject(%instrument))) {
        warn(getScopeName() @ " " @ "- cannot find instrument '" @ %instrumentName @ "'");
        return "";
    }
    return %instrument.skus;
};
function InstrumentRegistry::registerCommonProperties(%this) {
    %this.registerCommonInstrumentProperties("lguitar", 6051, 33704, "", "g", "n", "gtrglridl1", "gtrglrwlkf01", "gtrglrside01", "gtrglrwlkb01", "gtrglrjmp01");
    %this.registerCommonInstrumentProperties("rguitar", 6114, 33764, "", "r", "n", "gtrglridl1", "gtrglrwlkf01", "gtrglrside01", "gtrglrwlkb01", "gtrglrjmp01");
    %this.registerCommonInstrumentProperties("bassa", 6115, 33765, "", "a", "n", "gtrglridl1", "gtrglrwlkf01", "gtrglrside01", "gtrglrwlkb01", "gtrglrjmp01");
    %this.registerCommonInstrumentProperties("druma", 6116, 33766, "", "d", "n", "gtrglridl1", "gtrglrwlkf01", "gtrglrside01", "gtrglrwlkb01", "gtrglrjmp01");
};
