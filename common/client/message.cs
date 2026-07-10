if (isObject(MessageFuncDict)) {
    MessageFuncDict.delete();
}
$MessageFuncDict = new StringMap(MessageFuncDict);
if (isObject(MissionCleanup)) {
    MissionCleanup.add(MessageFuncDict);
}
function clientCmdChatMessage(%unused, %voice, %pitch, %msgString) {
    onChatMessage(detag(%msgString), %voice, %pitch);
};
function clientCmdServerMessage(%msgType, %msgString) {
    log("communication", "debug", "clientCmdServerMessage, msgType: " @ %msgType);
    log("communication", "debug", "clientCmdServerMessage, msgString: " @ %msgString);
    %tag = getWord(%msgType, 0);
    %defFuncList = MessageFuncDict.get("");
    if (isObject(%defFuncList)) {
        %i = 0;
        while (!((%func = %defFuncList.func[%i]) $= "")) {
            call(%func, %msgType, %msgString);
            %i = (%i + 1.0);
        }
    }
    if (!((!((%func = %defFuncList.func[%i]) $= "") @ " " @ %tag) $= "")) {
        %funcList = MessageFuncDict.get(%tag);
        if (isObject(%funcList)) {
            %i = 0;
            while (!((%func = %funcList.func[%i]) $= "")) {
                call(%func, %msgType, %msgString);
                %i = (%i + 1.0);
            }
        }
    }
};
function addMessageCallback(%msgType, %func) {
    %m = MessageFuncDict.get(%msgType);
    if (isObject(%m)) {
        %i = 0;
        while (!(%m.func[%i] $= "")) {
            %i = (%i + 1.0);
        }
        %m.func[%i] = !(%m.func[%i] $= "") @ %func;
    } else {
        %m = new SimObject("");
        MessageFuncDict.put(%msgType, %m);
        %m.func[0] = %func;
    }
};
function defaultMessageCallback(%msgType, %msgString) {
    onServerMessage(detag(%msgString));
};
addMessageCallback("", defaultMessageCallback);
