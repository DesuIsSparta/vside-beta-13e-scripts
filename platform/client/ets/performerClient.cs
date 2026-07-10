$gPerformerMode = 0;
function clientCmdSetPerformerMode(%pm, %nativePerformer) {
    $gPerformerMode = %pm;
    if (!(isObject(ForceFieldCtrls))) {
        return;
    }
    %pm.setVisible(ForceFieldCtrls);
    %nativePerformer.setVisible(performerPanelRadioButtonForceField2);
    %nativePerformer.setVisible(performerPanelRadioButtonForceField3);
};
function clientCmdSetPerformerMode_DEPRECATED(%pm, %nativePerformer) {
    $gPerformerMode = %pm;
    if (!($gPerformerMode)) {
        performerPanel.close();
    }
    if ($UserPref::Performer::AutoOpenPanel && !(performerPanel.isVisible())) {
        performerPanel.open();
    }
    1.setVisible(performerPanelRadioButtonForceField0);
    1.setVisible(performerPanelRadioButtonForceField1);
    %nativePerformer.setVisible(performerPanelRadioButtonForceField2);
    %nativePerformer.setVisible(performerPanelRadioButtonForceField3);
};
function performerClient::setForceField(%val) {
    if (!($gPerformerMode)) {
        error(getScopeName() @ " " @ "not performing");
        return;
    }
    commandToServer('setForceField', %val);
};
