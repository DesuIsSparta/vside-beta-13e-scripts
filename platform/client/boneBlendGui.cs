boneBlendGui.sliderOffset1 = "160 5";
boneBlendGui.sliderOffset2 = "160 25";
boneBlendGui.sliderOffset3 = "160 45";
boneBlendGui.labelOffset1 = "190 5";
boneBlendGui.labelOffset2 = "190 25";
boneBlendGui.labelOffset3 = "190 45";
boneBlendGui.groupOffset1 = "120 0";
function boneBlendGui::open(%this)
{
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    if (!%this.runOnce)
    {
        %i = 0;
        while (%i < $MAX_FREE_BONE_BLENDS)
        {
            %index = %i + $FIRST_FREE_BLEND_INDEX;
            $player.setBoneBlendOffsetByIndex(%index, blendOffsetSliderUniqueField.getValue());
            $player.setBoneBlendRateByIndex(%index, blendRateSliderUniqueField.getValue());
            $player.setBoneBlendScaleByIndex(%index, blendScaleSliderUniqueField.getValue());
            %i = %i + 1;
        }
        $player.setBoneBlendRate(blendRateSlider.getValue());
        $player.setBoneBlendScale(blendScaleSlider.getValue());
        %this.runOnce = 1;
    }
    return ;
}
function boneBlendGui::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
}
boneBlendGui.currentSliderIndex = -1;
function blendUpperPitch::onMouseEnter(%this)
{
    boneBlendGui.positionDynamicCtrls(%this);
    boneBlendGui.currentSliderIndex = $BB_UPPR_PITCH;
    blendUpperPitch.getGroup().setBitmap("platform/client/ui/messageHud");
    blendUpperYaw.getGroup().setBitmap("");
    blendUpperRoll.getGroup().setBitmap("");
    return ;
}
function blendUpperYaw::onMouseEnter(%this)
{
    boneBlendGui.positionDynamicCtrls(%this);
    boneBlendGui.currentSliderIndex = $BB_UPPR_YAW;
    blendUpperYaw.getGroup().setBitmap("platform/client/ui/messageHud");
    blendUpperPitch.getGroup().setBitmap("");
    blendUpperRoll.getGroup().setBitmap("");
    return ;
}
function blendUpperRoll::onMouseEnter(%this)
{
    boneBlendGui.positionDynamicCtrls(%this);
    boneBlendGui.currentSliderIndex = $BB_UPPR_ROLL;
    blendUpperPitch.getGroup().setBitmap("");
    blendUpperRoll.getGroup().setBitmap("platform/client/ui/messageHud");
    blendUpperYaw.getGroup().setBitmap("");
    return ;
}
function boneBlendGui::positionDynamicCtrls(%this, %contextControl)
{
    %groupPosition = VectorAdd(%contextControl.getGroup().position, boneBlendGui.groupOffset1);
    blendCyclicsGroup.position = %groupPosition;
    %Position1 = VectorAdd(%contextControl.getGroup().position, boneBlendGui.sliderOffset1);
    %Position2 = VectorAdd(%contextControl.getGroup().position, boneBlendGui.sliderOffset2);
    %Position3 = VectorAdd(%contextControl.getGroup().position, boneBlendGui.sliderOffset3);
    blendScaleSliderUniqueField.position = %Position1;
    blendRateSliderUniqueField.position = %Position2;
    blendOffsetSliderUniqueField.position = %Position3;
    %Position1 = VectorAdd(%contextControl.getGroup().position, boneBlendGui.labelOffset1);
    %Position2 = VectorAdd(%contextControl.getGroup().position, boneBlendGui.labelOffset2);
    %Position3 = VectorAdd(%contextControl.getGroup().position, boneBlendGui.labelOffset3);
    blendScaleLabel.position = %Position1;
    blendRateLabel.position = %Position2;
    blendOffsetLabel.position = %Position3;
    return ;
}
