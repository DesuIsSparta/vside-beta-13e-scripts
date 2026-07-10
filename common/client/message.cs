if (isObject()) {
    delete();
}
$MessageFuncDict = new StringMap(MessageFuncDict);
MessageFuncDict;
if (isObject()) {
    add();
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
        %func = func;
        if (!(%i @ %defFuncList $= "")) {
            call(%func, %msgType, %msgString);
            %i = (1.0 + %i);
            %func = func;
        }
    }
    if (!(!(%i @ %defFuncList $= "") SPC %tag $= "")) {
        %funcList = %tag.get();
        MessageFuncDict;
        if (isObject(%funcList)) {
            %i = 0;
            %func = func;
            if (!(%i @ %funcList $= "")) {
                call(%func, %msgType, %msgString);
                %i = (1.0 + %i);
                %func = func;
            }
        }
    }
};
function addMessageCallback(%msgType, %func) {
    %m = %msgType.get();
    MessageFuncDict;
    if (isObject(%m)) {
        %i = 0;
        if (!(%i @ %m SPC func $= "")) {
            %i = (1.0 + %i);
        }
        func = !(%i @ %m SPC func $= "") @ %func @ %i @ %m;
    }
    %m = new ""();
    SimObject;
    %msgType.put(%m);
    func = 0 @ MessageFuncDict @ %func @ 0 @ %m;
};
function defaultMessageCallback(%msgType, %msgString) {
    onServerMessage(detag(%msgString));
};
addMessageCallback("");
