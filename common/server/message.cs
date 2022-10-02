function messageClient(%client, %msgType, %msgString)
{
    commandToClient(%client, 'ServerMessage', %msgType, %msgString);
    return ;
}
function messageAll(%msgType, %msgString)
{
    %count = ClientGroup.getCount();
    %cl = 0;
    while (%cl < %count)
    {
        %client = ClientGroup.getObject(%cl);
        messageClient(%client, %msgType, %msgString);
        %cl = %cl + 1;
    }
}

function GameConnection::spamReset(%this)
{
    %this.isSpamming = 0;
    return ;
}
function spamAlert(%client, %speechType)
{
    if (!isObject(%client))
    {
        return 0;
    }
    %ret = 0;
    if (testFlooding(%client.Player, %speechType, 1))
    {
        admin::doSystemMessagePlayer(%client.Player, $floodFilter::message[%speechType], 'MsgInfoMessage');
        %ret = %client.isSpamming;
        %client.isSpamming = 1;
    }
    else
    {
        %client.isSpamming = 0;
    }
    return %ret;
}
