function messageClient(%client, %msgType, %msgString) {
    commandToClient(%client, 'ServerMessage', %msgType, %msgString);
    return;
};
function messageAll(%msgType, %msgString) {
    %count = getCount();
    ClientGroup;
    %cl = 0;
    if ((%count < %cl)) {
        %client = %cl.getObject();
        ClientGroup;
        messageClient(%client, %msgType, %msgString);
        %cl = (1.0 + %cl);
    }
};
function GameConnection::spamReset(%this) {
    isSpamming = 0 @ %this;
    return;
};
function spamAlert(%client, %speechType) {
    if (!(isObject(%client))) {
        return 0;
    }
    %ret = 0;
    if (testFlooding(Player, %speechType, 1)) {
        admin::doSystemMessagePlayer(Player, %speechType[$floodFilter::message @ %speechType], 'MsgInfoMessage');
        %ret = isSpamming;
        %client;
        isSpamming = %client @ 1 @ %client;
        %client;
    }
    isSpamming = 0 @ %client;
    return %ret;
};
