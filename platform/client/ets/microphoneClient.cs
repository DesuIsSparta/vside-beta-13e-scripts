function doMicrophoneGiveOrRevoke(%playerName, %give) {
    commandToServer('MicrophoneGiveOrRevoke', %playerName, %give);
};
function doMicrophoneRevokeAll() {
    commandToServer('MicrophoneRevokeAll');
};
