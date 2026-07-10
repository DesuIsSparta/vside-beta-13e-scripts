%registry = safeEnsureScriptObject("ScriptObject", "");
%registry.bindClassName("InstrumentRegistry");
%registry.bindClassName("InstrumentRegistryClient");
%registry.setName("InstrumentRegistryClient");
function InstrumentRegistryClient::initializeRegistry(%this) {
    if (%this.initialized) {
        warn(getScopeName() @ " " @ "- registry already intialized");
        return;
    }
    %this.instrumentsList = StringMap @ new ""();;
    0;
    if (isObject(MissionCleanup)) {
        %this.instrumentsList.add();
    }
    %this.keyBindings = StringMap @ new ""();;
    0;
    if (isObject(MissionCleanup)) {
        %this.keyBindings.add();
    }
    %this.initializeRegistryCommon();
    %this.initialized = MissionCleanup @ 1;
    MissionCleanup;
};
%this.initialized = 0 @ InstrumentRegistryClient;
InstrumentRegistryClient.initializeRegistry();
function InstrumentRegistryClient::clearRegistry(%this) {
    if (isObject(%this.instrumentsList)) {
        %i = (1.0 - %this.instrumentsList.size());
        if ((0.0 >= %i)) {
            %instrument = %this.instrumentsList.getValue(%i);
            if (isObject(%instrument)) {
                if (isObject(%instrument.animationMaps)) {
                    %instrument.animationMaps.clear();
                    %instrument.animationMaps.delete();
                }
                if (isObject(%instrument.animationMaps)) {
                    %instrument.animationMaps.clear();
                    %instrument.animationMaps.delete();
                }
                %instrument.delete();
            }
            %i = (1.0 - %i);
            "f" @ "f" @ "f" @ "m" @ "m" @ "m";
        }
        %this.instrumentsList.clear();
    }
    if (isObject(%this.keyBindings)) {
        %this.keyBindings.clear();
    }
    %this.clearRegistryCommon();
};
function InstrumentRegistryClient::closeRegistry(%this) {
    %this.clearRegistry();
    if (isObject(%this.instrumentsList)) {
        %this.instrumentsList.delete();
        %this.instrumentsList = "";
    }
    if (isObject(%this.keyBindings)) {
        %this.keyBindings.delete();
        %this.keyBindings = "";
    }
    %this.closeRegistryCommon();
};
function InstrumentRegistryClient::registerInstrument(%this, %instrumentName, %instrumentGameTitleText, %instrumentGameBodyText, %instrumentGameDisabledText, %instrumentGameActiveIconA, %instrumentGameActiveIconB, %instrumentGameIdleIcon, %instrumentGameUnfocusedIcon) {
    0;
    %instrument = new ""() {
        name = SimSet @ %instrumentName;
        titleText = %instrumentGameTitleText;
        bodyText = %instrumentGameBodyText;
        disabledText = %instrumentGameDisabledText;
        activeIconA = %instrumentGameActiveIconA;
        activeIconB = %instrumentGameActiveIconB;
        idleIcon = %instrumentGameIdleIcon;
        unfocusedIcon = %instrumentGameUnfocusedIcon;
    };
    %this.instrumentsList.put(%instrumentName, %instrument);
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
    if (!(%this.instrumentsList.hasKey(%instrumentName))) {
        warn(getScopeName() @ " " @ "- cannot find instrument '" @ %instrumentName @ "'");
        return;
    }
    %keyBinding = %this.normalizeKeyBinding(%instrumentName, %keyBinding);
    %this.keyBindings.put(%keyBinding, %animationName);
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
    if (!(%this.keyBindings.hasKey(%keyBinding))) {
        return "";
    }
    return %this.keyBindings.get(%keyBinding);
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
InstrumentRegistryClient.registerCommonProperties();
