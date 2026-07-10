delete();
$MessageFuncDict = new ();
MessageFuncDict;
add();
function clientCmdChatMessage(%unused, %voice, %pitch, %msgString) {
    onChatMessage(detag(%msgString), %voice, %pitch);
};
function clientCmdServerMessage(%msgType, %msgString) {
    log("communication", "debug", "clientCmdServerMessage, msgType: " @ %msgType);
    log("communication", "debug", "clientCmdServerMessage, msgString: " @ %msgString);
    %tag = getWord(%msgType, 0);
    %defFuncList = "".get();
    MessageFuncDict;
    %i = 0;
    isObject(%defFuncList);
    %func = func;
    call(%func, %msgType, %msgString);
    %i = (1.0 + %i);
    !((%i @ %defFuncList $= ""));
    %func = func;
    %funcList = %tag.get();
    MessageFuncDict;
    %i = 0;
    isObject(%funcList);
    %func = func;
    call(%func, %msgType, %msgString);
    %i = (1.0 + %i);
    !((!((!((%i @ %defFuncList $= "")) SPC %tag $= "")) @ %i @ %funcList $= ""));
    %func = func;
};
function addMessageCallback(%msgType, %func) {
    %m = %msgType.get();
    MessageFuncDict;
    %i = 0;
    isObject(%m);
    %i = (1.0 + %i);
    !((%i @ %m SPC func $= ""));
    func = !((%i @ %m SPC func $= "")) @ %func @ %i @ %m;
    %m = new ""();
    SimObject;
    %msgType.put(%m);
    func = 0 @ MessageFuncDict @ %func @ 0 @ %m;
};
function defaultMessageCallback(%msgType, %msgString) {
    onServerMessage(detag(%msgString));
};
addMessageCallback("");
