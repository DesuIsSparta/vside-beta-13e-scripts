function InstrumentRegistry::initializeRegistryCommon(%this)
{
    %this.defaultStopAnimation = "idl1a";
    %this.stopAnimationsList = new StringMap();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%this.stopAnimationsList);
    }
    return ;
}
function InstrumentRegistry::clearRegistryCommon(%this)
{
    if (isObject(%this.stopAnimationsList))
    {
        %this.stopAnimationsList.clear();
    }
    return ;
}
function InstrumentRegistry::closeRegistryCommon(%this)
{
    if (isObject(%this.stopAnimationsList))
    {
        %this.stopAnimationsList.delete();
        %this.stopAnimationsList = "";
    }
    return ;
}
function InstrumentRegistry::registerCommonInstrumentProperties(%this, %instrumentName, %instrumentFemaleSku, %instrumentMaleSku, %instrumentNeuterSku, %instrumentGenre, %animationGenrePrefix, %rootAnim, %runAnim, %sideAnim, %backAnim, %jumpAnim)
{
    %instrument = %this.instrumentsList.get(%instrumentName);
    if (!isObject(%instrument))
    {
        warn(getScopeName() SPC "- cannot find instrument \'" @ %instrumentName @ "\'");
        return ;
    }
    %instrument.skus["f"] = %instrumentFemaleSku;
    %instrument.skus["m"] = %instrumentMaleSku;
    %instrument.skus["n"] = %instrumentNeuterSku;
    %instrument.genre = %instrumentGenre;
    %instrument.animationMaps["f"] = makeAnimationMapInstrument("f", %instrumentGenre, %animationGenrePrefix @ %rootAnim, %animationGenrePrefix @ %runAnim, %animationGenrePrefix @ %sideAnim, %animationGenrePrefix @ %backAnim, %animationGenrePrefix @ %jumpAnim);
    %instrument.animationMaps["m"] = makeAnimationMapInstrument("m", %instrumentGenre, %animationGenrePrefix @ %rootAnim, %animationGenrePrefix @ %runAnim, %animationGenrePrefix @ %sideAnim, %animationGenrePrefix @ %backAnim, %animationGenrePrefix @ %jumpAnim);
    %this.stopAnimationsList.put(%instrumentName, %rootAnim);
    return ;
}
function InstrumentRegistry::getInstrumentCount(%this)
{
    return %this.instrumentsList.size();
}
function InstrumentRegistry::getInstrumentByIndex(%this, %index)
{
    if ((%index < 0) && (%index >= %this.instrumentsList.size()))
    {
        warn(getScopeName() SPC "- bad index value =" SPC %index);
        return "";
    }
    return %this.instrumentsList.getValue(%index);
}
function InstrumentRegistry::getInstrumentBySku(%this, %sku)
{
    if (%sku $= "")
    {
        return "";
    }
    %i = %this.getInstrumentCount() - 1;
    while (%i >= 0)
    {
        %instrument = %this.getInstrumentByIndex(%i);
        if (((%instrument.skus["f"] $= %sku) || (%instrument.skus["m"] $= %sku)) || (%instrument.skus["n"] $= %sku))
        {
            return %instrument;
        }
        %i = %i - 1;
    }
    return "";
}
function InstrumentRegistry::getInstrumentObject(%this, %instrumentName)
{
    %instrument = %this.instrumentsList.get(%instrumentName);
    if (!isObject(%instrument))
    {
        warn(getScopeName() SPC "- cannot find instrument \'" @ %instrumentName @ "\'");
        return "";
    }
    return %instrument;
}
function InstrumentRegistry::isInstrument(%this, %instrumentNameOrObject)
{
    if (isObject(%instrumentNameOrObject))
    {
        %instrumentName = %instrumentNameOrObject.name;
    }
    else
    {
        %instrumentName = %instrumentNameOrObject;
    }
    return %this.instrumentsList.hasKey(%instrumentName);
}
function InstrumentRegistry::isInstrumentGenre(%this, %genre)
{
    if (%genre $= "")
    {
        return 0;
    }
    %i = %this.getInstrumentCount() - 1;
    while (%i >= 0)
    {
        %instrument = %this.getInstrumentByIndex(%i);
        if (%instrument.genre $= %genre)
        {
            return 1;
        }
        %i = %i - 1;
    }
    return 0;
}
function InstrumentRegistry::isInstrumentSku(%this, %sku)
{
    if (%sku $= "")
    {
        return 0;
    }
    %i = %this.getInstrumentCount() - 1;
    while (%i >= 0)
    {
        %instrument = %this.getInstrumentByIndex(%i);
        if (((%instrument.skus["f"] $= %sku) || (%instrument.skus["m"] $= %sku)) || (%instrument.skus["n"] $= %sku))
        {
            return 1;
        }
        %i = %i - 1;
    }
    return 0;
}
function InstrumentRegistry::isStopAnimation(%this, %animationName)
{
    return %this.stopAnimationsList.hasValue(%animationName);
}
function InstrumentRegistry::getStopAnimation(%this, %instrumentName)
{
    if (!%this.isInstrument(%instrumentName))
    {
        error(getScopeName() SPC "- could not find instrument \'" @ %instrumentName @ "\'");
        return "";
    }
    if (!%this.stopAnimationsList.hasKey(%instrumentName))
    {
        error(getScopeName() SPC "- could not find stopAnimation for existing instrument \'" @ %instrumentName @ "\'");
        return "";
    }
    return %this.stopAnimationsList.get(%instrumentName);
}
function InstrumentRegistry::getSku(%this, %instrumentName, %gender)
{
    %instrument = %this.instrumentsList.get(%instrumentName);
    if (!isObject(%instrument))
    {
        warn(getScopeName() SPC "- cannot find instrument \'" @ %instrumentName @ "\'");
        return "";
    }
    return %instrument.skus[%gender];
}
function InstrumentRegistry::registerCommonProperties(%this)
{
    %this.registerCommonInstrumentProperties("lguitar", 6051, 33704, "", "g", "n", "gtrglridl1", "gtrglrwlkf01", "gtrglrside01", "gtrglrwlkb01", "gtrglrjmp01");
    %this.registerCommonInstrumentProperties("rguitar", 6114, 33764, "", "r", "n", "gtrglridl1", "gtrglrwlkf01", "gtrglrside01", "gtrglrwlkb01", "gtrglrjmp01");
    %this.registerCommonInstrumentProperties("bassa", 6115, 33765, "", "a", "n", "gtrglridl1", "gtrglrwlkf01", "gtrglrside01", "gtrglrwlkb01", "gtrglrjmp01");
    %this.registerCommonInstrumentProperties("druma", 6116, 33766, "", "d", "n", "gtrglridl1", "gtrglrwlkf01", "gtrglrside01", "gtrglrwlkb01", "gtrglrjmp01");
    return ;
}
