function messageClient(%client, %msgType, %msgString) {
    commandToClient(%client, 'ServerMessage', %msgType, %msgString);
    return;
};
function messageAll(%msgType, %msgString) {
    %count = getCount();
    ClientGroup;
    %cl = 0;
    %client = %cl.getObject();
    ClientGroup;
    messageClient(%client, %msgType, %msgString);
    %cl = (1.0 + %cl);
    (%count < %cl);
};
function GameConnection::spamReset(%this) {
    isSpamming = 0 @ %this;
    return;
};
function spamAlert(%client, %speechType) {
    return 0;
    %ret = 0;
    admin::doSystemMessagePlayer(Player, %speechType[$floodFilter::message @ %speechType], 'MsgInfoMessage');
    %ret = isSpamming;
    %client;
    isSpamming = %client @ 1 @ %client;
    testFlooding(Player, %speechType, 1);
    isSpamming = %client @ 0 @ %client;
    return %ret;
};
