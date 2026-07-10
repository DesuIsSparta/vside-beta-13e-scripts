safeEnsureScriptObject("StringMap", "gStompedObjectNames");
function onObjectNameStomped(%name, %stompeeID, %likeleyStomperID) {
    gStompedObjectNames.put(gStompedObjectNames.size() @ " " @ %name, %stompeeID @ " " @ %likeleyStomperID);
};
function displayStompedObjectNameErrors() {
    if (!($ETS::devMode)) {
    }
    if ((0.0 == gStompedObjectNames.size())) {
        return;
    }
    schedule(0, 0, "displayStompedObjectNameErrorsReally");
};
function displayStompedObjectNameErrorsReally() {
    %title = "STOMPED OBJECT NAMES";
    %body = "the following critical object names were stomped.";
    %body = %body @ "\n" @ "THIS IS A CRITICAL PROBLEM, DO NOT CHECK IN YOUR CHANGES.";
    %body = %body @ "\n" @ "If you can't figure out the problem from this message and the console.log,";
    %body = %body @ "\n" @ "please ask one of the game engine engineers to take a look.";
    %count = gStompedObjectNames.size();
    %realCount = 0;
    %n = 0;
    if ((%count < %n)) {
        %name = getWord(gStompedObjectNames.getKey(%n), 1);
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
            %body = %body @ " " @ "-" @ " " @ getDebugString(getWord(gStompedObjectNames.getValue(%n), 1));
            %body = %body @ " " @ "stomped" @ " " @ getDebugString(getWord(gStompedObjectNames.getValue(%n), 0));
        }
        %n = (1.0 + %n);
    }
    if ((0.0 > %realCount)) {
        %mb = MessageBoxOK(%title, %body, "").window;
        (%count < %n);
        %mb.resize(800, 200);
        %mb.resizeWidth = 1;
        %mb.resizeHeight = 1;
    }
};
