$gPerformerMode = 0;
function clientCmdSetPerformerMode(%pm, %nativePerformer) {
    $gPerformerMode = %pm;
    if (!(isObject(ForceFieldCtrls))) {
        return;
    }
    %pm.setVisible();
    %nativePerformer.setVisible();
    %nativePerformer.setVisible();
};
function clientCmdSetPerformerMode_DEPRECATED(%pm, %nativePerformer) {
    $gPerformerMode = %pm;
    if (!($gPerformerMode)) {
        performerPanel.close();
    }
    if ($UserPref::Performer::AutoOpenPanel) {
        if (!(performerPanel.isVisible())) {
            performerPanel.open();
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
