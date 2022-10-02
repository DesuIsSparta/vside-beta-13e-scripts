$gPerformerMode = 0;
function clientCmdSetPerformerMode(%pm, %nativePerformer)
{
    $gPerformerMode = %pm;
    if (!isObject(ForceFieldCtrls))
    {
        return ;
    }
    ForceFieldCtrls.setVisible(%pm);
    performerPanelRadioButtonForceField2.setVisible(%nativePerformer);
    performerPanelRadioButtonForceField3.setVisible(%nativePerformer);
    return ;
}
function clientCmdSetPerformerMode_DEPRECATED(%pm, %nativePerformer)
{
    $gPerformerMode = %pm;
    if (!$gPerformerMode)
    {
        performerPanel.close();
    }
    else
    {
        if ($UserPref::Performer::AutoOpenPanel)
        {
            if (!performerPanel.isVisible())
            {
                performerPanel.open();
            }
        }
    }
    performerPanelRadioButtonForceField0.setVisible(1);
    performerPanelRadioButtonForceField1.setVisible(1);
    performerPanelRadioButtonForceField2.setVisible(%nativePerformer);
    performerPanelRadioButtonForceField3.setVisible(%nativePerformer);
    return ;
}
function performerClient::setForceField(%val)
{
    if (!$gPerformerMode)
    {
        error(getScopeName() SPC "not performing");
        return ;
    }
    commandToServer('setForceField', %val);
    return ;
}
