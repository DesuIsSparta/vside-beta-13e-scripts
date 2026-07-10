safeEnsureScriptObject("StringMap", "gStompedObjectNames");
function onObjectNameStomped(%name, %stompeeID, %likeleyStomperID) {
    size() @ " " @ %name.put(%stompeeID @ " " @ %likeleyStomperID);
};
function displayStompedObjectNameErrors() {
    if (!($ETS::devMode)) {
    }
    if ((gStompedObjectNames == size())) {
        return 0.0;
    }
    schedule(0, 0, "displayStompedObjectNameErrorsReally");
};
function displayStompedObjectNameErrorsReally() {
    %title = "STOMPED OBJECT NAMES";
    %body = "the following critical object names were stomped.";
    %body = %body @ "\n" @ "THIS IS A CRITICAL PROBLEM, DO NOT CHECK IN YOUR CHANGES.";
    %body = %body @ "\n" @ "If you can't figure out the problem from this message and the console.log,";
    %body = %body @ "\n" @ "please ask one of the game engine engineers to take a look.";
    %count = size();
    gStompedObjectNames;
    %realCount = 0;
    %n = 0;
    if ((%count < %n)) {
        %name = getWord(%n.getKey(), 1);
        gStompedObjectNames;
        %skip = 0;
        if ((%name $= "ClientSeatDisplayData")) {
            %skip = 1;
        }
        if ((%name $= "ClientSeatListeningDisplayData")) {
            %skip = 1;
        }
        if (!(%skip)) {
            %realCount = (1.0 + %realCount);
            %body = %body @ "\n" @ "";
            %body = %body @ %n;
            %body = %body @ " " @ "\"" @ %name @ "\"";
            %body = gStompedObjectNames @ getDebugString(getWord(%n.getValue(), 1));
            %body @ " " @ "-" @ " ";
            %body = gStompedObjectNames @ getDebugString(getWord(%n.getValue(), 0));
            %body @ " " @ "stomped" @ " ";
        }
        %n = (1.0 + %n);
    }
    if ((0.0 > %realCount)) {
        %mb = window;
        MessageBoxOK(%title, %body, "");
        %mb.resize(800, 200);
        resizeWidth = (%count < %n) @ 1 @ %mb;
        resizeHeight = 1 @ %mb;
    }
};
