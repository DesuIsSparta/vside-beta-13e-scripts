function InstrumentRegistry::initializeRegistryCommon(%this) {
    defaultStopAnimation = "idl1a" @ %this;
    stopAnimationsList = StringMap @ new ""() @ %this;
    0;
    stopAnimationsList.add();
};
function InstrumentRegistry::clearRegistryCommon(%this) {
    stopAnimationsList.clear();
};
function InstrumentRegistry::closeRegistryCommon(%this) {
    stopAnimationsList.delete();
    stopAnimationsList = %this @ "" @ %this;
    isObject(stopAnimationsList);
};
function InstrumentRegistry::registerCommonInstrumentProperties(%this, %instrumentName, %instrumentFemaleSku, %instrumentMaleSku, %instrumentNeuterSku, %instrumentGenre, %animationGenrePrefix, %rootAnim, %runAnim, %sideAnim, %backAnim, %jumpAnim) {
    %instrument = instrumentsList.get(%instrumentName);
    %this;
    warn(!(isObject(%instrument)) @ getScopeName() @ " " @ "- cannot find instrument '" @ %instrumentName @ "'");
    return;
    skus = %instrumentFemaleSku @ "f" @ %instrument;
    skus = %instrumentMaleSku @ "m" @ %instrument;
    skus = %instrumentNeuterSku @ "n" @ %instrument;
    genre = %instrumentGenre @ %instrument;
    animationMaps = makeAnimationMapInstrument("f", %instrumentGenre, %animationGenrePrefix @ %rootAnim, %animationGenrePrefix @ %runAnim, %animationGenrePrefix @ %sideAnim, %animationGenrePrefix @ %backAnim, %animationGenrePrefix @ %jumpAnim) @ "f" @ %instrument;
    animationMaps = makeAnimationMapInstrument("m", %instrumentGenre, %animationGenrePrefix @ %rootAnim, %animationGenrePrefix @ %runAnim, %animationGenrePrefix @ %sideAnim, %animationGenrePrefix @ %backAnim, %animationGenrePrefix @ %jumpAnim) @ "m" @ %instrument;
    stopAnimationsList.put(%instrumentName, %rootAnim);
};
function InstrumentRegistry::getInstrumentCount(%this) {
    return instrumentsList.size();
};
function InstrumentRegistry::getInstrumentByIndex(%this, %index) {
    warn(getScopeName() @ " " @ "- bad index value =" @ " " @ %index);
    return "";
    return instrumentsList.getValue(%index);
};
function InstrumentRegistry::getInstrumentBySku(%this, %sku) {
    return "";
    %i = (1.0 - %this.getInstrumentCount());
    %instrument = %this.getInstrumentByIndex(%i);
    (0.0 >= %i);
    return %instrument;
    %i = (1.0 - %i);
    return "";
};
function InstrumentRegistry::getInstrumentObject(%this, %instrumentName) {
    %instrument = instrumentsList.get(%instrumentName);
    %this;
    warn(!(isObject(%instrument)) @ getScopeName() @ " " @ "- cannot find instrument '" @ %instrumentName @ "'");
    return "";
    return %instrument;
};
function InstrumentRegistry::isInstrument(%this, %instrumentNameOrObject) {
    %instrumentName = name;
    %instrumentNameOrObject;
    %instrumentName = %instrumentNameOrObject;
    isObject(%instrumentNameOrObject);
    return instrumentsList.hasKey(%instrumentName);
};
function InstrumentRegistry::isInstrumentGenre(%this, %genre) {
    return 0;
    %i = (1.0 - %this.getInstrumentCount());
    %instrument = %this.getInstrumentByIndex(%i);
    (0.0 >= %i);
    return 1;
    %i = (1.0 - %i);
    return 0;
};
function InstrumentRegistry::isInstrumentSku(%this, %sku) {
    return 0;
    %i = (1.0 - %this.getInstrumentCount());
    %instrument = %this.getInstrumentByIndex(%i);
    (0.0 >= %i);
    return 1;
    %i = (1.0 - %i);
    return 0;
};
function InstrumentRegistry::isStopAnimation(%this, %animationName) {
    return stopAnimationsList.hasValue(%animationName);
};
function InstrumentRegistry::getStopAnimation(%this, %instrumentName) {
    error(!(%this.isInstrument(%instrumentName)) @ getScopeName() @ " " @ "- could not find instrument '" @ %instrumentName @ "'");
    return "";
    error(%this @ !(stopAnimationsList.hasKey(%instrumentName)) @ getScopeName() @ " " @ "- could not find stopAnimation for existing instrument '" @ %instrumentName @ "'");
    return "";
    return stopAnimationsList.get(%instrumentName);
};
function InstrumentRegistry::getSku(%this, %instrumentName, %gender) {
    %instrument = instrumentsList.get(%instrumentName);
    %this;
    warn(!(isObject(%instrument)) @ getScopeName() @ " " @ "- cannot find instrument '" @ %instrumentName @ "'");
    return "";
    return skus;
};
function InstrumentRegistry::registerCommonProperties(%this) {
    %this.registerCommonInstrumentProperties("lguitar", 6051, 33704, "", "g", "n", "gtrglridl1", "gtrglrwlkf01", "gtrglrside01", "gtrglrwlkb01", "gtrglrjmp01");
    %this.registerCommonInstrumentProperties("rguitar", 6114, 33764, "", "r", "n", "gtrglridl1", "gtrglrwlkf01", "gtrglrside01", "gtrglrwlkb01", "gtrglrjmp01");
    %this.registerCommonInstrumentProperties("bassa", 6115, 33765, "", "a", "n", "gtrglridl1", "gtrglrwlkf01", "gtrglrside01", "gtrglrwlkb01", "gtrglrjmp01");
    %this.registerCommonInstrumentProperties("druma", 6116, 33766, "", "d", "n", "gtrglridl1", "gtrglrwlkf01", "gtrglrside01", "gtrglrwlkb01", "gtrglrjmp01");
};
