sliderOffset1 = "160 5" @ boneBlendGui;
sliderOffset2 = "160 25" @ boneBlendGui;
sliderOffset3 = "160 45" @ boneBlendGui;
labelOffset1 = "190 5" @ boneBlendGui;
labelOffset2 = "190 25" @ boneBlendGui;
labelOffset3 = "190 45" @ boneBlendGui;
groupOffset1 = "120 0" @ boneBlendGui;
function boneBlendGui::open(%this) {
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    if (!(%this.runOnce)) {
        %i = 0;
        while ((%i < $MAX_FREE_BONE_BLENDS)) {
            %index = (%i + $FIRST_FREE_BLEND_INDEX);
            blendOffsetSliderUniqueField.getValue().setBoneBlendOffsetByIndex($player, %index);
            blendRateSliderUniqueField.getValue().setBoneBlendRateByIndex($player, %index);
            blendScaleSliderUniqueField.getValue().setBoneBlendScaleByIndex($player, %index);
            %i = (%i + 1.0);
        }
        blendRateSlider.getValue().setBoneBlendRate($player);
        blendScaleSlider.getValue().setBoneBlendScale($player);
        %this.runOnce = (%i < $MAX_FREE_BONE_BLENDS) @ 1;
    }
};
function boneBlendGui::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    return 1;
};
%this.currentSliderIndex = -(1.0) @ boneBlendGui;
function blendUpperPitch::onMouseEnter(%this) {
    %this.positionDynamicCtrls(boneBlendGui);
    %this.currentSliderIndex = $BB_UPPR_PITCH @ boneBlendGui;
    "platform/client/ui/messageHud".setBitmap(blendUpperPitch.getGroup());
    "".setBitmap(blendUpperYaw.getGroup());
    "".setBitmap(blendUpperRoll.getGroup());
};
function blendUpperYaw::onMouseEnter(%this) {
    %this.positionDynamicCtrls(boneBlendGui);
    %this.currentSliderIndex = $BB_UPPR_YAW @ boneBlendGui;
    "platform/client/ui/messageHud".setBitmap(blendUpperYaw.getGroup());
    "".setBitmap(blendUpperPitch.getGroup());
    "".setBitmap(blendUpperRoll.getGroup());
};
function blendUpperRoll::onMouseEnter(%this) {
    %this.positionDynamicCtrls(boneBlendGui);
    %this.currentSliderIndex = $BB_UPPR_ROLL @ boneBlendGui;
    "".setBitmap(blendUpperPitch.getGroup());
    "platform/client/ui/messageHud".setBitmap(blendUpperRoll.getGroup());
    "".setBitmap(blendUpperYaw.getGroup());
};
function boneBlendGui::positionDynamicCtrls(%this, %contextControl) {
    %groupPosition = VectorAdd(%contextControl.getGroup().position, boneBlendGui, %contextControl.getGroup().groupOffset1);
    %contextControl.getGroup().position = %groupPosition @ blendCyclicsGroup;
    %Position1 = VectorAdd(%contextControl.getGroup().position, boneBlendGui, %contextControl.getGroup().sliderOffset1);
    %Position2 = VectorAdd(%contextControl.getGroup().position, boneBlendGui, %contextControl.getGroup().sliderOffset2);
    %Position3 = VectorAdd(%contextControl.getGroup().position, boneBlendGui, %contextControl.getGroup().sliderOffset3);
    %contextControl.getGroup().position = %Position1 @ blendScaleSliderUniqueField;
    %contextControl.getGroup().position = %Position2 @ blendRateSliderUniqueField;
    %contextControl.getGroup().position = %Position3 @ blendOffsetSliderUniqueField;
    %Position1 = VectorAdd(%contextControl.getGroup().position, boneBlendGui, %contextControl.getGroup().labelOffset1);
    %Position2 = VectorAdd(%contextControl.getGroup().position, boneBlendGui, %contextControl.getGroup().labelOffset2);
    %Position3 = VectorAdd(%contextControl.getGroup().position, boneBlendGui, %contextControl.getGroup().labelOffset3);
    %contextControl.getGroup().position = %Position1 @ blendScaleLabel;
    %contextControl.getGroup().position = %Position2 @ blendRateLabel;
    %contextControl.getGroup().position = %Position3 @ blendOffsetLabel;
};
