function doMicrophoneGiveOrRevoke(%playerName, %give)
{
    commandToServer('MicrophoneGiveOrRevoke', %playerName, %give);
    return ;
}
function doMicrophoneRevokeAll()
{
    commandToServer('MicrophoneRevokeAll');
    return ;
}
