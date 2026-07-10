sliderOffset1 = "160 5" @ boneBlendGui;
sliderOffset2 = "160 25" @ boneBlendGui;
sliderOffset3 = "160 45" @ boneBlendGui;
labelOffset1 = "190 5" @ boneBlendGui;
labelOffset2 = "190 25" @ boneBlendGui;
labelOffset3 = "190 45" @ boneBlendGui;
groupOffset1 = "120 0" @ boneBlendGui;
function boneBlendGui::open(%this) {
    %this.setVisible(1);
    %this.focusAndRaise();
    if (!(%this.runOnce)) {
        %i = 0;
        PlayGui;
        if (($MAX_FREE_BONE_BLENDS < %i)) {
            %index = ($FIRST_FREE_BLEND_INDEX + %i);
            $player.setBoneBlendOffsetByIndex(%index, blendOffsetSliderUniqueField.getValue());
            $player.setBoneBlendRateByIndex(%index, blendRateSliderUniqueField.getValue());
            $player.setBoneBlendScaleByIndex(%index, blendScaleSliderUniqueField.getValue());
            %i = (1.0 + %i);
        }
        $player.setBoneBlendRate(blendRateSlider.getValue());
        $player.setBoneBlendScale(blendScaleSlider.getValue());
        %this.runOnce = ($MAX_FREE_BONE_BLENDS < %i) @ 1;
    }
};
function boneBlendGui::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
};
%this.currentSliderIndex = -(1.0) @ boneBlendGui;
function blendUpperPitch::onMouseEnter(%this) {
    %this.positionDynamicCtrls();
    %this.currentSliderIndex = $BB_UPPR_PITCH @ boneBlendGui;
    boneBlendGui;
    blendUpperPitch.getGroup().setBitmap("platform/client/ui/messageHud");
    blendUpperYaw.getGroup().setBitmap("");
    blendUpperRoll.getGroup().setBitmap("");
};
function blendUpperYaw::onMouseEnter(%this) {
    %this.positionDynamicCtrls();
    %this.currentSliderIndex = $BB_UPPR_YAW @ boneBlendGui;
    boneBlendGui;
    blendUpperYaw.getGroup().setBitmap("platform/client/ui/messageHud");
    blendUpperPitch.getGroup().setBitmap("");
    blendUpperRoll.getGroup().setBitmap("");
};
function blendUpperRoll::onMouseEnter(%this) {
    %this.positionDynamicCtrls();
    %this.currentSliderIndex = $BB_UPPR_ROLL @ boneBlendGui;
    boneBlendGui;
    blendUpperPitch.getGroup().setBitmap("");
    blendUpperRoll.getGroup().setBitmap("platform/client/ui/messageHud");
    blendUpperYaw.getGroup().setBitmap("");
};
function boneBlendGui::positionDynamicCtrls(%this, %contextControl) {
    %groupPosition = VectorAdd(%contextControl.getGroup().position, %contextControl.getGroup().groupOffset1);
    boneBlendGui;
    %contextControl.getGroup().position = %groupPosition @ blendCyclicsGroup;
    %Position1 = VectorAdd(%contextControl.getGroup().position, %contextControl.getGroup().sliderOffset1);
    boneBlendGui;
    %Position2 = VectorAdd(%contextControl.getGroup().position, %contextControl.getGroup().sliderOffset2);
    boneBlendGui;
    %Position3 = VectorAdd(%contextControl.getGroup().position, %contextControl.getGroup().sliderOffset3);
    boneBlendGui;
    %contextControl.getGroup().position = %Position1 @ blendScaleSliderUniqueField;
    %contextControl.getGroup().position = %Position2 @ blendRateSliderUniqueField;
    %contextControl.getGroup().position = %Position3 @ blendOffsetSliderUniqueField;
    %Position1 = VectorAdd(%contextControl.getGroup().position, %contextControl.getGroup().labelOffset1);
    boneBlendGui;
    %Position2 = VectorAdd(%contextControl.getGroup().position, %contextControl.getGroup().labelOffset2);
    boneBlendGui;
    %Position3 = VectorAdd(%contextControl.getGroup().position, %contextControl.getGroup().labelOffset3);
    boneBlendGui;
    %contextControl.getGroup().position = %Position1 @ blendScaleLabel;
    %contextControl.getGroup().position = %Position2 @ blendRateLabel;
    %contextControl.getGroup().position = %Position3 @ blendOffsetLabel;
};
