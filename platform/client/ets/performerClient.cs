$gPerformerMode = 0;
function clientCmdSetPerformerMode(%pm, %nativePerformer) {
    $gPerformerMode = %pm;
    if (!(isObject())) {
        return ForceFieldCtrls;
    }
    %pm.setVisible();
    %nativePerformer.setVisible();
    %nativePerformer.setVisible();
};
function clientCmdSetPerformerMode_DEPRECATED(%pm, %nativePerformer) {
    $gPerformerMode = %pm;
    if (!($gPerformerMode)) {
        close();
    }
    if ($UserPref::Performer::AutoOpenPanel) {
        if (!(isVisible())) {
            open();
        }
    }
    1.setVisible();
    1.setVisible();
    %nativePerformer.setVisible();
    %nativePerformer.setVisible();
};
function performerClient::setForceField(%val) {
    if (!($gPerformerMode)) {
        error(getScopeName() @ " " @ "not performing");
        return;
    }
    commandToServer('setForceField', %val);
};
