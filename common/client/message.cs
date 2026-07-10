if (isObject(MessageFuncDict)) {
    MessageFuncDict.delete();
}
$MessageFuncDict = new StringMap(MessageFuncDict);;
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
        %func = %defFuncList.func;
        if (!(%i $= "")) {
            call(%func, %msgType, %msgString);
            %i = (1.0 + %i);
            %func = %defFuncList.func;
        }
    }
    if (!(!(%i $= "") @ " " @ %tag $= "")) {
        %funcList = MessageFuncDict.get(%tag);
        if (isObject(%funcList)) {
            %i = 0;
            %func = %funcList.func;
            if (!(%i $= "")) {
                call(%func, %msgType, %msgString);
                %i = (1.0 + %i);
                %func = %funcList.func;
            }
        }
    }
};
function addMessageCallback(%msgType, %func) {
    %m = MessageFuncDict.get(%msgType);
    if (isObject(%m)) {
        %i = 0;
        if (!(%i @ " " @ %m.func $= "")) {
            %i = (1.0 + %i);
        }
        %m.func = !(%i @ " " @ %m.func $= "") @ %func @ %i;
    }
    %m = new SimObject("");;
    0;
    MessageFuncDict.put(%msgType, %m);
    %m.func = %func @ 0;
};
function defaultMessageCallback(%msgType, %msgString) {
    onServerMessage(detag(%msgString));
};
addMessageCallback("");
