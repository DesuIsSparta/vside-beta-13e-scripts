function clearLoadInfo()
{
    if (isObject(MissionInfo))
    {
        MissionInfo.delete();
    }
    return ;
}
function buildLoadInfo(%mission)
{
    clearLoadInfo();
    %infoObject = "";
    %file = new FileObject();
    if (%file.openForRead(%mission))
    {
        %inInfoBlock = 0;
        while (!%file.isEOF())
        {
            %line = %file.readLine();
            %line = trim(%line);
            if (%line $= "new ScriptObject(MissionInfo) {")
            {
                %inInfoBlock = 1;
            }
            else
            {
                if (%inInfoBlock && (%line $= "};"))
                {
                    %inInfoBlock = 0;
                    %infoObject = %infoObject @ %line;
                    break;
                }
            }
            if (%inInfoBlock)
            {
                %infoObject = %infoObject @ %line @ " ";
            }
        }
        %file.close();
    }
    eval(%infoObject);
    %file.delete();
    return ;
}
function dumpLoadInfo()
{
    echo("Mission Name: " @ MissionInfo.name);
    echo("Mission Description:");
    %i = 0;
    while (!(MissionInfo.desc[%i] $= ""))
    {
        echo("   " @ MissionInfo.desc[%i]);
        %i = %i + 1;
    }
}

function sendLoadInfoToClient(%client)
{
    messageClient(%client, 'MsgLoadInfo', MissionInfo.name);
    %i = 0;
    while (!(MissionInfo.desc[%i] $= ""))
    {
        messageClient(%client, 'MsgLoadDescripition', MissionInfo.desc[%i]);
        %i = %i + 1;
    }
    messageClient(%client, 'MsgLoadInfoDone', "");
    return ;
}
