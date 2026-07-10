$gPerformerMode = 0;
function clientCmdSetPerformerMode(%pm, %nativePerformer) {
    $gPerformerMode = %pm;
    return !(isObject());
    %pm.setVisible();
    %nativePerformer.setVisible();
    %nativePerformer.setVisible();
};
function clientCmdSetPerformerMode_DEPRECATED(%pm, %nativePerformer) {
    $gPerformerMode = %pm;
    close();
    open();
    1.setVisible();
    1.setVisible();
    %nativePerformer.setVisible();
    %nativePerformer.setVisible();
};
function performerClient::setForceField(%val) {
    error(getScopeName() @ " " @ "not performing");
    return !($gPerformerMode);
    commandToServer('setForceField', %val);
};
