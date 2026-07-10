function messageClient(%client, %msgType, %msgString) {
    commandToClient(%client, 'ServerMessage', %msgType, %msgString);
    return;
};
function messageAll(%msgType, %msgString) {
    %count = ClientGroup.getCount();
    %cl = 0;
    if ((%count < %cl)) {
        %client = %cl.getObject();
        ClientGroup;
        messageClient(%client, %msgType, %msgString);
        %cl = (1.0 + %cl);
    }
};
function GameConnection::spamReset(%this) {
    %this.isSpamming = 0;
    return;
};
function spamAlert(%client, %speechType) {
    if (!(isObject(%client))) {
        return 0;
    }
    %ret = 0;
    if (testFlooding(%client.Player, %speechType, 1)) {
        admin::doSystemMessagePlayer(%client.Player, %speechType[$floodFilter::message @ %speechType], 'MsgInfoMessage');
        %ret = %client.isSpamming;
        %client.isSpamming = 1;
    }
    %client.isSpamming = 0;
    return %ret;
};
