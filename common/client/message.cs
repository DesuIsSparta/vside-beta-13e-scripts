if (isObject(MessageFuncDict)) {
    MessageFuncDict.delete();
}
$MessageFuncDict = new StringMap(MessageFuncDict);
if (isObject(MissionCleanup)) {
    MessageFuncDict.add(MissionCleanup);
}
function clientCmdChatMessage(%unused, %voice, %pitch, %msgString) {
    onChatMessage(detag(%msgString), %voice, %pitch);
};
function clientCmdServerMessage(%msgType, %msgString) {
    log("communication", "debug", "clientCmdServerMessage, msgType: " @ %msgType);
    log("communication", "debug", "clientCmdServerMessage, msgString: " @ %msgString);
    %tag = getWord(%msgType, 0);
    %defFuncList = "".get(MessageFuncDict);
    if (isObject(%defFuncList)) {
        %i = 0;
        while (!((%i @ " " @ %func = %defFuncList.func) $= "")) {
            call(%func, %msgType, %msgString);
            %i = (%i + 1.0);
        }
    }
    if (!((!((%i @ " " @ %func = %defFuncList.func) $= "") @ " " @ %tag) $= "")) {
        %funcList = %tag.get(MessageFuncDict);
        if (isObject(%funcList)) {
            %i = 0;
            while (!((%i @ " " @ %func = %funcList.func) $= "")) {
                call(%func, %msgType, %msgString);
                %i = (%i + 1.0);
            }
        }
    }
};
function addMessageCallback(%msgType, %func) {
    %m = %msgType.get(MessageFuncDict);
    if (isObject(%m)) {
        %i = 0;
        while (!(%i @ " " @ %m.func $= "")) {
            %i = (%i + 1.0);
        }
        %m.func = !(%i @ " " @ %m.func $= "") @ %func @ %i;
    }
    %m = new SimObject("");
    %m.put(MessageFuncDict, %msgType);
    %m.func = %func @ 0;
};
function defaultMessageCallback(%msgType, %msgString) {
    onServerMessage(detag(%msgString));
};
addMessageCallback("", defaultMessageCallback);
