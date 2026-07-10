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
    %defFuncList = "".get();
    MessageFuncDict;
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
        %funcList = %tag.get();
        MessageFuncDict;
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
    %m = %msgType.get();
    MessageFuncDict;
    if (isObject(%m)) {
        %i = 0;
        if (!(%i @ " " @ %m.func $= "")) {
            %i = (1.0 + %i);
        }
        %m.func = !(%i @ " " @ %m.func $= "") @ %func @ %i;
    }
    %m = new ""();;
    SimObject;
    %msgType.put(%m);
    %m.func = MessageFuncDict @ %func @ 0;
    0;
};
function defaultMessageCallback(%msgType, %msgString) {
    onServerMessage(detag(%msgString));
};
addMessageCallback("");
