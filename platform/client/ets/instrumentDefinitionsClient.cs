%registry = safeEnsureScriptObject("ScriptObject", "");
%registry.bindClassName("InstrumentRegistry");
%registry.bindClassName("InstrumentRegistryClient");
%registry.setName("InstrumentRegistryClient");
function InstrumentRegistryClient::initializeRegistry(%this) {
    if (initialized) {
        warn(getScopeName() @ " " @ "- registry already intialized");
        return %this;
    }
    instrumentsList = StringMap @ new ""() @ %this;
    0;
    if (isObject()) {
        instrumentsList.add();
    }
    keyBindings = StringMap @ new ""() @ %this;
    0;
    if (isObject()) {
        keyBindings.add();
    }
    %this.initializeRegistryCommon();
    initialized = %this @ 1 @ %this;
    MissionCleanup;
};
initialized = 0 @ InstrumentRegistryClient;
initializeRegistry();
function InstrumentRegistryClient::clearRegistry(%this) {
    if (isObject(instrumentsList)) {
        %i = (%this - instrumentsList.size());
        1.0;
        if ((0.0 >= %i)) {
            %instrument = instrumentsList.getValue(%i);
            %this;
            if (isObject(%instrument)) {
                if (isObject(animationMaps)) {
                    animationMaps.clear();
                    animationMaps.delete();
                }
                if (isObject(animationMaps)) {
                    animationMaps.clear();
                    animationMaps.delete();
                }
                %instrument.delete();
            }
            %i = (1.0 - %i);
            %this @ "f" @ %instrument @ "f" @ %instrument @ "f" @ %instrument @ "m" @ %instrument @ "m" @ %instrument @ "m" @ %instrument;
        }
        instrumentsList.clear();
    }
    if (isObject(keyBindings)) {
        keyBindings.clear();
    }
    %this.clearRegistryCommon();
};
function InstrumentRegistryClient::closeRegistry(%this) {
    %this.clearRegistry();
    if (isObject(instrumentsList)) {
        instrumentsList.delete();
        instrumentsList = %this @ "" @ %this;
        %this;
    }
    if (isObject(keyBindings)) {
        keyBindings.delete();
        keyBindings = %this @ "" @ %this;
        %this;
    }
    %this.closeRegistryCommon();
};
function InstrumentRegistryClient::registerInstrument(%this, %instrumentName, %instrumentGameTitleText, %instrumentGameBodyText, %instrumentGameDisabledText, %instrumentGameActiveIconA, %instrumentGameActiveIconB, %instrumentGameIdleIcon, %instrumentGameUnfocusedIcon) {
    name = SimSet @ new ""() @ %instrumentName;
    0;
    titleText = %instrumentGameTitleText;
    bodyText = %instrumentGameBodyText;
    disabledText = %instrumentGameDisabledText;
    activeIconA = %instrumentGameActiveIconA;
    activeIconB = %instrumentGameActiveIconB;
    idleIcon = %instrumentGameIdleIcon;
    unfocusedIcon = %instrumentGameUnfocusedIcon;
    %instrument = ;
    instrumentsList.put(%instrumentName, %instrument);
};
function InstrumentRegistryClient::registerInstrumentKeyBinding(%this, %instrumentName, %keyBinding, %animationName) {
    if ((%instrumentName $= "")) {
    }
    if (!(%keyBinding $= "")) {
        error(getScopeName() @ " " @ "- cannot bind nonempty keyBinding for empty instrumentName");
        return;
    }
    if ((%animationName $= "")) {
        error(getScopeName() @ " " @ "- cannot bind empty animationName");
        return;
    }
    if (!(%instrumentName $= "")) {
    }
    if (!(instrumentsList.hasKey(%instrumentName))) {
        warn(%this @ getScopeName() @ " " @ "- cannot find instrument '" @ %instrumentName @ "'");
        return;
    }
    %keyBinding = %this.normalizeKeyBinding(%instrumentName, %keyBinding);
    keyBindings.put(%keyBinding, %animationName);
};
function InstrumentRegistryClient::getAnimation(%this, %instrumentName, %keyBinding) {
    if (!(%this.isInstrument(%instrumentName))) {
        warn(getScopeName() @ " " @ "- cannot find instrument '" @ %instrumentName @ "'");
        return;
    }
    if ((%keyBinding $= "")) {
        return %this.getStopAnimation(%instrumentName);
    }
    %keyBinding = %this.normalizeKeyBinding(%instrumentName, %keyBinding);
    if (!(keyBindings.hasKey(%keyBinding))) {
        return "";
    }
    return keyBindings.get(%keyBinding);
};
function InstrumentRegistryClient::normalizeKeyBinding(%this, %instrumentName, %keyBinding) {
    if ((%keyBinding $= "")) {
        %keyBinding = "stop";
    }
    %keyBinding = strlwr(%keyBinding);
    return %instrumentName @ "\t" @ %keyBinding;
};
"lguitar".registerInstrument("Click here. Use the numbers 1-0 and keys Q-P to play riffs!", "Rock out!", "Sorry, the guitar is temporarily disabled.", "platform/client/ui/guitar_activeA", "platform/client/ui/guitar_activeB", "platform/client/ui/guitar_idle", "platform/client/ui/guitar_unfocused");
"rguitar".registerInstrument("Click here. Use the numbers 1-0 and keys Q-P to play riffs!", "Rock out YAY!", "Sorry, the guitar is temporarily disabled.", "platform/client/ui/rguitar_activeA", "platform/client/ui/rguitar_activeB", "platform/client/ui/rguitar_idle", "platform/client/ui/rguitar_unfocused");
"bassa".registerInstrument("Click here. Use the numbers 1-0 and keys Q-P to play riffs!", "Rock out BASS!", "Sorry, the guitar is temporarily disabled.", "platform/client/ui/rguitar_activeA", "platform/client/ui/rguitar_activeB", "platform/client/ui/rguitar_idle", "platform/client/ui/rguitar_unfocused");
"druma".registerInstrument("Click here. Use the numbers 1-0 and keys Q-P to play riffs!", "Rock out BASS!", "Sorry, the guitar is temporarily disabled.", "platform/client/ui/rguitar_activeA", "platform/client/ui/rguitar_activeB", "platform/client/ui/rguitar_idle", "platform/client/ui/rguitar_unfocused");
"lguitar".registerInstrumentKeyBinding(1, "gtrglr1e");
"lguitar".registerInstrumentKeyBinding(2, "gtrglr2e");
"lguitar".registerInstrumentKeyBinding(3, "gtrglr3e");
"lguitar".registerInstrumentKeyBinding(4, "gtrglr4e");
"lguitar".registerInstrumentKeyBinding(5, "gtrglr5e");
"lguitar".registerInstrumentKeyBinding(6, "gtrglr6e");
"lguitar".registerInstrumentKeyBinding(7, "gtrglr7e");
"lguitar".registerInstrumentKeyBinding(8, "gtrglr8a");
"lguitar".registerInstrumentKeyBinding(9, "gtrglr9a");
"lguitar".registerInstrumentKeyBinding(0, "gtrglr10a");
"lguitar".registerInstrumentKeyBinding("q", "gtrglr11a");
"lguitar".registerInstrumentKeyBinding("w", "gtrglr12a");
"lguitar".registerInstrumentKeyBinding("e", "gtrglr13a");
"lguitar".registerInstrumentKeyBinding("r", "gtrglr14a");
"lguitar".registerInstrumentKeyBinding("t", "gtrglr15b");
"lguitar".registerInstrumentKeyBinding("y", "gtrglr16b");
"lguitar".registerInstrumentKeyBinding("u", "gtrglr17b");
"lguitar".registerInstrumentKeyBinding("i", "gtrglr18b");
"lguitar".registerInstrumentKeyBinding("o", "gtrglr19b");
"lguitar".registerInstrumentKeyBinding("p", "gtrglr20b");
"rguitar".registerInstrumentKeyBinding(1, "gtrgr1e");
"rguitar".registerInstrumentKeyBinding(2, "gtrgr2e");
"rguitar".registerInstrumentKeyBinding(3, "gtrgr3e");
"rguitar".registerInstrumentKeyBinding(4, "gtrgr22a");
"rguitar".registerInstrumentKeyBinding(5, "gtrgr25b");
"rguitar".registerInstrumentKeyBinding(6, "gtrgr6e");
"rguitar".registerInstrumentKeyBinding(7, "gtrgr7e");
"rguitar".registerInstrumentKeyBinding(8, "gtrgr14a");
"rguitar".registerInstrumentKeyBinding(9, "gtrgr9e");
"rguitar".registerInstrumentKeyBinding(0, "gtrgr10e");
"rguitar".registerInstrumentKeyBinding("q", "gtrgr11e");
"rguitar".registerInstrumentKeyBinding("w", "gtrgr12a");
"rguitar".registerInstrumentKeyBinding("e", "gtrgr13a");
"rguitar".registerInstrumentKeyBinding("r", "gtrgr8e");
"rguitar".registerInstrumentKeyBinding("t", "gtrgr15a");
"rguitar".registerInstrumentKeyBinding("y", "gtrgr5e");
"rguitar".registerInstrumentKeyBinding("u", "gtrgr28b");
"rguitar".registerInstrumentKeyBinding("i", "gtrgr18a");
"rguitar".registerInstrumentKeyBinding("o", "gtrgr29b");
"rguitar".registerInstrumentKeyBinding("p", "gtrgr17a");
"bassa".registerInstrumentKeyBinding(1, "bassr1e");
"druma".registerInstrumentKeyBinding(1, "drumr1e");
registerCommonProperties();
