%registry = safeEnsureScriptObject("ScriptObject", "");
%registry.bindClassName("InstrumentRegistry");
%registry.bindClassName("InstrumentRegistryClient");
%registry.setName("InstrumentRegistryClient");
function InstrumentRegistryClient::initializeRegistry(%this)
{
    if (%this.initialized)
    {
        warn(getScopeName() SPC "- registry already intialized");
        return ;
    }
    %this.instrumentsList = new StringMap();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%this.instrumentsList);
    }
    %this.keyBindings = new StringMap();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%this.keyBindings);
    }
    %this.initializeRegistryCommon();
    %this.initialized = 1;
    return ;
}
InstrumentRegistryClient.initialized = 0;
InstrumentRegistryClient.initializeRegistry();
function InstrumentRegistryClient::clearRegistry(%this)
{
    if (isObject(%this.instrumentsList))
    {
        %i = %this.instrumentsList.size() - 1;
        while (%i >= 0)
        {
            %instrument = %this.instrumentsList.getValue(%i);
            if (isObject(%instrument))
            {
                if (isObject(%instrument.animationMaps["f"]))
                {
                    %instrument.animationMaps["f"].clear();
                    %instrument.animationMaps["f"].delete();
                }
                if (isObject(%instrument.animationMaps["m"]))
                {
                    %instrument.animationMaps["m"].clear();
                    %instrument.animationMaps["m"].delete();
                }
                %instrument.delete();
            }
            %i = %i - 1;
        }
        %this.instrumentsList.clear();
    }
    if (isObject(%this.keyBindings))
    {
        %this.keyBindings.clear();
    }
    %this.clearRegistryCommon();
    return ;
}
function InstrumentRegistryClient::closeRegistry(%this)
{
    %this.clearRegistry();
    if (isObject(%this.instrumentsList))
    {
        %this.instrumentsList.delete();
        %this.instrumentsList = "";
    }
    if (isObject(%this.keyBindings))
    {
        %this.keyBindings.delete();
        %this.keyBindings = "";
    }
    %this.closeRegistryCommon();
    return ;
}
function InstrumentRegistryClient::registerInstrument(%this, %instrumentName, %instrumentGameTitleText, %instrumentGameBodyText, %instrumentGameDisabledText, %instrumentGameActiveIconA, %instrumentGameActiveIconB, %instrumentGameIdleIcon, %instrumentGameUnfocusedIcon)
{
    %instrument = new SimSet()
    {
        name = %instrumentName;
        titleText = %instrumentGameTitleText;
        bodyText = %instrumentGameBodyText;
        disabledText = %instrumentGameDisabledText;
        activeIconA = %instrumentGameActiveIconA;
        activeIconB = %instrumentGameActiveIconB;
        idleIcon = %instrumentGameIdleIcon;
        unfocusedIcon = %instrumentGameUnfocusedIcon;
    };
    %this.instrumentsList.put(%instrumentName, %instrument);
    return ;
}
function InstrumentRegistryClient::registerInstrumentKeyBinding(%this, %instrumentName, %keyBinding, %animationName)
{
    if ((%instrumentName $= "") && !((%keyBinding $= "")))
    {
        error(getScopeName() SPC "- cannot bind nonempty keyBinding for empty instrumentName");
        return ;
    }
    if (%animationName $= "")
    {
        error(getScopeName() SPC "- cannot bind empty animationName");
        return ;
    }
    if (!((%instrumentName $= "")) && !%this.instrumentsList.hasKey(%instrumentName))
    {
        warn(getScopeName() SPC "- cannot find instrument \'" @ %instrumentName @ "\'");
        return ;
    }
    %keyBinding = %this.normalizeKeyBinding(%instrumentName, %keyBinding);
    %this.keyBindings.put(%keyBinding, %animationName);
    return ;
}
function InstrumentRegistryClient::getAnimation(%this, %instrumentName, %keyBinding)
{
    if (!%this.isInstrument(%instrumentName))
    {
        warn(getScopeName() SPC "- cannot find instrument \'" @ %instrumentName @ "\'");
        return ;
    }
    if (%keyBinding $= "")
    {
        return %this.getStopAnimation(%instrumentName);
    }
    %keyBinding = %this.normalizeKeyBinding(%instrumentName, %keyBinding);
    if (!%this.keyBindings.hasKey(%keyBinding))
    {
        return "";
    }
    return %this.keyBindings.get(%keyBinding);
}
function InstrumentRegistryClient::normalizeKeyBinding(%this, %instrumentName, %keyBinding)
{
    if (%keyBinding $= "")
    {
        %keyBinding = "stop";
    }
    %keyBinding = strlwr(%keyBinding);
    return %instrumentName TAB %keyBinding;
}
InstrumentRegistryClient.registerInstrument("lguitar", "Click here. Use the numbers 1-0 and keys Q-P to play riffs!", "Rock out!", "Sorry, the guitar is temporarily disabled.", "platform/client/ui/guitar_activeA", "platform/client/ui/guitar_activeB", "platform/client/ui/guitar_idle", "platform/client/ui/guitar_unfocused");
InstrumentRegistryClient.registerInstrument("rguitar", "Click here. Use the numbers 1-0 and keys Q-P to play riffs!", "Rock out YAY!", "Sorry, the guitar is temporarily disabled.", "platform/client/ui/rguitar_activeA", "platform/client/ui/rguitar_activeB", "platform/client/ui/rguitar_idle", "platform/client/ui/rguitar_unfocused");
InstrumentRegistryClient.registerInstrument("bassa", "Click here. Use the numbers 1-0 and keys Q-P to play riffs!", "Rock out BASS!", "Sorry, the guitar is temporarily disabled.", "platform/client/ui/rguitar_activeA", "platform/client/ui/rguitar_activeB", "platform/client/ui/rguitar_idle", "platform/client/ui/rguitar_unfocused");
InstrumentRegistryClient.registerInstrument("druma", "Click here. Use the numbers 1-0 and keys Q-P to play riffs!", "Rock out BASS!", "Sorry, the guitar is temporarily disabled.", "platform/client/ui/rguitar_activeA", "platform/client/ui/rguitar_activeB", "platform/client/ui/rguitar_idle", "platform/client/ui/rguitar_unfocused");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", 1, "gtrglr1e");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", 2, "gtrglr2e");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", 3, "gtrglr3e");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", 4, "gtrglr4e");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", 5, "gtrglr5e");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", 6, "gtrglr6e");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", 7, "gtrglr7e");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", 8, "gtrglr8a");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", 9, "gtrglr9a");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", 0, "gtrglr10a");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", "q", "gtrglr11a");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", "w", "gtrglr12a");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", "e", "gtrglr13a");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", "r", "gtrglr14a");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", "t", "gtrglr15b");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", "y", "gtrglr16b");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", "u", "gtrglr17b");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", "i", "gtrglr18b");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", "o", "gtrglr19b");
InstrumentRegistryClient.registerInstrumentKeyBinding("lguitar", "p", "gtrglr20b");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", 1, "gtrgr1e");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", 2, "gtrgr2e");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", 3, "gtrgr3e");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", 4, "gtrgr22a");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", 5, "gtrgr25b");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", 6, "gtrgr6e");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", 7, "gtrgr7e");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", 8, "gtrgr14a");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", 9, "gtrgr9e");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", 0, "gtrgr10e");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", "q", "gtrgr11e");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", "w", "gtrgr12a");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", "e", "gtrgr13a");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", "r", "gtrgr8e");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", "t", "gtrgr15a");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", "y", "gtrgr5e");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", "u", "gtrgr28b");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", "i", "gtrgr18a");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", "o", "gtrgr29b");
InstrumentRegistryClient.registerInstrumentKeyBinding("rguitar", "p", "gtrgr17a");
InstrumentRegistryClient.registerInstrumentKeyBinding("bassa", 1, "bassr1e");
InstrumentRegistryClient.registerInstrumentKeyBinding("druma", 1, "drumr1e");
InstrumentRegistryClient.registerCommonProperties();

