%registry = safeEnsureScriptObject("ScriptObject", "");
"InstrumentRegistry".bindClassName(%registry);
"InstrumentRegistryClient".bindClassName(%registry);
"InstrumentRegistryClient".setName(%registry);
function InstrumentRegistryClient::initializeRegistry(%this) {
    if (%this.initialized) {
        warn(getScopeName() @ " " @ "- registry already intialized");
        return;
    }
    %this.instrumentsList = 0 @ new StringMap("");;
    if (isObject(MissionCleanup)) {
        %this.instrumentsList.add(MissionCleanup);
    }
    %this.keyBindings = 0 @ new StringMap("");;
    if (isObject(MissionCleanup)) {
        %this.keyBindings.add(MissionCleanup);
    }
    %this.initializeRegistryCommon();
    %this.initialized = 1;
};
%this.initialized = 0 @ InstrumentRegistryClient;
InstrumentRegistryClient.initializeRegistry();
function InstrumentRegistryClient::clearRegistry(%this) {
    if (isObject(%this.instrumentsList)) {
        %i = (%this.instrumentsList.size() - 1.0);
        while ((%i >= 0.0)) {
            %instrument = %i.getValue(%this.instrumentsList);
            if (isObject(%instrument)) {
                if (isObject("f", %instrument.animationMaps)) {
                    %instrument.animationMaps.clear("f");
                    %instrument.animationMaps.delete("f");
                }
                if (isObject("m", %instrument.animationMaps)) {
                    %instrument.animationMaps.clear("m");
                    %instrument.animationMaps.delete("m");
                }
                %instrument.delete();
            }
            %i = (%i - 1.0);
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
    %instrument = new SimSet("") {
        name = 0 @ %instrumentName;
        titleText = %instrumentGameTitleText;
        bodyText = %instrumentGameBodyText;
        disabledText = %instrumentGameDisabledText;
        activeIconA = %instrumentGameActiveIconA;
        activeIconB = %instrumentGameActiveIconB;
        idleIcon = %instrumentGameIdleIcon;
        unfocusedIcon = %instrumentGameUnfocusedIcon;
    };
    %instrument.put(%this.instrumentsList, %instrumentName);
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
    if (!(%instrumentName.hasKey(%this.instrumentsList))) {
        warn(getScopeName() @ " " @ "- cannot find instrument '" @ %instrumentName @ "'");
        return;
    }
    %keyBinding = %keyBinding.normalizeKeyBinding(%this, %instrumentName);
    %animationName.put(%this.keyBindings, %keyBinding);
};
function InstrumentRegistryClient::getAnimation(%this, %instrumentName, %keyBinding) {
    if (!(%instrumentName.isInstrument(%this))) {
        warn(getScopeName() @ " " @ "- cannot find instrument '" @ %instrumentName @ "'");
        return;
    }
    if ((%keyBinding $= "")) {
        return %instrumentName.getStopAnimation(%this);
    }
    %keyBinding = %keyBinding.normalizeKeyBinding(%this, %instrumentName);
    if (!(%keyBinding.hasKey(%this.keyBindings))) {
        return "";
    }
    return %keyBinding.get(%this.keyBindings);
};
function InstrumentRegistryClient::normalizeKeyBinding(%this, %instrumentName, %keyBinding) {
    if ((%keyBinding $= "")) {
        %keyBinding = "stop";
    }
    %keyBinding = strlwr(%keyBinding);
    return %instrumentName @ "\t" @ %keyBinding;
};
"platform/client/ui/guitar_unfocused".registerInstrument(InstrumentRegistryClient, "lguitar", "Click here. Use the numbers 1-0 and keys Q-P to play riffs!", "Rock out!", "Sorry, the guitar is temporarily disabled.", "platform/client/ui/guitar_activeA", "platform/client/ui/guitar_activeB", "platform/client/ui/guitar_idle");
"platform/client/ui/rguitar_unfocused".registerInstrument(InstrumentRegistryClient, "rguitar", "Click here. Use the numbers 1-0 and keys Q-P to play riffs!", "Rock out YAY!", "Sorry, the guitar is temporarily disabled.", "platform/client/ui/rguitar_activeA", "platform/client/ui/rguitar_activeB", "platform/client/ui/rguitar_idle");
"platform/client/ui/rguitar_unfocused".registerInstrument(InstrumentRegistryClient, "bassa", "Click here. Use the numbers 1-0 and keys Q-P to play riffs!", "Rock out BASS!", "Sorry, the guitar is temporarily disabled.", "platform/client/ui/rguitar_activeA", "platform/client/ui/rguitar_activeB", "platform/client/ui/rguitar_idle");
"platform/client/ui/rguitar_unfocused".registerInstrument(InstrumentRegistryClient, "druma", "Click here. Use the numbers 1-0 and keys Q-P to play riffs!", "Rock out BASS!", "Sorry, the guitar is temporarily disabled.", "platform/client/ui/rguitar_activeA", "platform/client/ui/rguitar_activeB", "platform/client/ui/rguitar_idle");
"gtrglr1e".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", 1);
"gtrglr2e".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", 2);
"gtrglr3e".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", 3);
"gtrglr4e".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", 4);
"gtrglr5e".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", 5);
"gtrglr6e".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", 6);
"gtrglr7e".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", 7);
"gtrglr8a".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", 8);
"gtrglr9a".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", 9);
"gtrglr10a".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", 0);
"gtrglr11a".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", "q");
"gtrglr12a".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", "w");
"gtrglr13a".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", "e");
"gtrglr14a".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", "r");
"gtrglr15b".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", "t");
"gtrglr16b".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", "y");
"gtrglr17b".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", "u");
"gtrglr18b".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", "i");
"gtrglr19b".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", "o");
"gtrglr20b".registerInstrumentKeyBinding(InstrumentRegistryClient, "lguitar", "p");
"gtrgr1e".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", 1);
"gtrgr2e".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", 2);
"gtrgr3e".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", 3);
"gtrgr22a".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", 4);
"gtrgr25b".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", 5);
"gtrgr6e".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", 6);
"gtrgr7e".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", 7);
"gtrgr14a".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", 8);
"gtrgr9e".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", 9);
"gtrgr10e".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", 0);
"gtrgr11e".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", "q");
"gtrgr12a".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", "w");
"gtrgr13a".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", "e");
"gtrgr8e".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", "r");
"gtrgr15a".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", "t");
"gtrgr5e".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", "y");
"gtrgr28b".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", "u");
"gtrgr18a".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", "i");
"gtrgr29b".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", "o");
"gtrgr17a".registerInstrumentKeyBinding(InstrumentRegistryClient, "rguitar", "p");
"bassr1e".registerInstrumentKeyBinding(InstrumentRegistryClient, "bassa", 1);
"drumr1e".registerInstrumentKeyBinding(InstrumentRegistryClient, "druma", 1);
InstrumentRegistryClient.registerCommonProperties();
