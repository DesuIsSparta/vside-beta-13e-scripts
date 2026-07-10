function sendC2CCmd(%commandName, %targetUserName, %param1, %param2) {
    %param1 = "";
    !(isDefined("%param1"));
    %param2 = "";
    !(isDefined("%param2"));
    %text = "[c2ccmd]" @ "\n" @ %commandName @ "\n" @ %param1 @ "\n" @ %param2;
    %text.whisper(%targetUserName, 0);
};
function handleC2CCmd(%commandName, %senderUserName, %param1, %param2) {
    echoDebug(getScopeName() @ " " @ "-" @ " " @ %commandName @ " " @ %senderUserName @ " " @ %param1 @ " " @ %param2);
    handleInviteeFinishedGateway(%senderUserName, %param1);
    error("unknown c2c cmd:" @ " " @ %commandName @ " " @ "from" @ " " @ %senderUserName);
};
function handleInviteeFinishedGateway(%otherPlayerName, %otherPlayerGender) {
    %msg = ;
    %msg = strreplace(%msg, "[OTHERPLAYER]", "<spush><b>" @ %otherPlayerName @ "<spop>");
    %msg = strreplace(%msg, "[OTHERPLAYER_HIM_HER_IT]", getPronounHimHerIt(%otherPlayerGender));
    MessageBoxOK("Someone you invited has arrived!", %msg, "");
};
