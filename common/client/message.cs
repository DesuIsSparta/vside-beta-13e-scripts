if (isObject(MessageFuncDict))
{
    MessageFuncDict.delete();
}
$MessageFuncDict = new StringMap(MessageFuncDict);
if (isObject(MissionCleanup))
{
    MissionCleanup.add(MessageFuncDict);
}
function clientCmdChatMessage(%unused, %voice, %pitch, %msgString)
{
    onChatMessage(detag(%msgString), %voice, %pitch);
    return ;
}
function clientCmdServerMessage(%msgType, %msgString)
{
    log("communication", "debug", "clientCmdServerMessage, msgType: " @ %msgType);
    log("communication", "debug", "clientCmdServerMessage, msgString: " @ %msgString);
    %tag = getWord(%msgType, 0);
    %defFuncList = MessageFuncDict.get("");
    if (isObject(%defFuncList))
    {
        %i = 0;
        while (!(%func = %defFuncList.func[%i] $= ""))
        {
            call(%func, %msgType, %msgString);
            %i = %i + 1;
        }
    }
    if (!(%tag $= ""))
    {
        %funcList = MessageFuncDict.get(%tag);
        if (isObject(%funcList))
        {
            %i = 0;
            while (!(%func = %funcList.func[%i] $= ""))
            {
                call(%func, %msgType, %msgString);
                %i = %i + 1;
            }
        }
    }
}

function addMessageCallback(%msgType, %func)
{
    %m = MessageFuncDict.get(%msgType);
    if (isObject(%m))
    {
        %i = 0;
        while (!(%m.func[%i] $= ""))
        {
            %i = %i + 1;
        }
        %m.func[%i] = %func;
    }
    else
    {
        %m = new SimObject();
        MessageFuncDict.put(%msgType, %m);
        %m.func[0] = %func;
    }
    return ;
}
function defaultMessageCallback(%msgType, %msgString)
{
    onServerMessage(detag(%msgString));
    return ;
}
addMessageCallback("", defaultMessageCallback);

