function sendC2CCmd(%commandName, %targetUserName, %param1, %param2)
{
    if (!isDefined("%param1"))
    {
        %param1 = "";
    }
    if (!isDefined("%param2"))
    {
        %param2 = "";
    }
    %text = "[c2ccmd]" NL %commandName NL %param1 NL %param2;
    pChat.whisper(%text, %targetUserName, 0);
    return ;
}
function handleC2CCmd(%commandName, %senderUserName, %param1, %param2)
{
    echoDebug(getScopeName() SPC "-" SPC %commandName SPC %senderUserName SPC %param1 SPC %param2);
    if (%commandName $= "finishedGateway")
    {
        handleInviteeFinishedGateway(%senderUserName, %param1);
    }
    else
    {
        error("unknown c2c cmd:" SPC %commandName SPC "from" SPC %senderUserName);
    }
    return ;
}
function handleInviteeFinishedGateway(%otherPlayerName, %otherPlayerGender)
{
    %msg = $MsgCat::invitation["INVITEE-INWORLD"];
    %msg = strreplace(%msg, "[OTHERPLAYER]", "<spush><b>" @ %otherPlayerName @ "<spop>");
    %msg = strreplace(%msg, "[OTHERPLAYER_HIM_HER_IT]", getPronounHimHerIt(%otherPlayerGender));
    MessageBoxOK("Someone you invited has arrived!", %msg, "");
    return ;
}
