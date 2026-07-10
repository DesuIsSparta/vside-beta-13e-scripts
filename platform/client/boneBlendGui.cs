boneBlendGui.sliderOffset1 = "160 5";
boneBlendGui.sliderOffset2 = "160 25";
boneBlendGui.sliderOffset3 = "160 45";
boneBlendGui.labelOffset1 = "190 5";
boneBlendGui.labelOffset2 = "190 25";
boneBlendGui.labelOffset3 = "190 45";
boneBlendGui.groupOffset1 = "120 0";
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
boneBlendGui.currentSliderIndex = -(1.0);
function blendUpperPitch::onMouseEnter(%this) {
    %this.positionDynamicCtrls(boneBlendGui);
    boneBlendGui.currentSliderIndex = $BB_UPPR_PITCH;
    "platform/client/ui/messageHud".setBitmap(blendUpperPitch.getGroup());
    "".setBitmap(blendUpperYaw.getGroup());
    "".setBitmap(blendUpperRoll.getGroup());
};
function blendUpperYaw::onMouseEnter(%this) {
    %this.positionDynamicCtrls(boneBlendGui);
    boneBlendGui.currentSliderIndex = $BB_UPPR_YAW;
    "platform/client/ui/messageHud".setBitmap(blendUpperYaw.getGroup());
    "".setBitmap(blendUpperPitch.getGroup());
    "".setBitmap(blendUpperRoll.getGroup());
};
function blendUpperRoll::onMouseEnter(%this) {
    %this.positionDynamicCtrls(boneBlendGui);
    boneBlendGui.currentSliderIndex = $BB_UPPR_ROLL;
    "".setBitmap(blendUpperPitch.getGroup());
    "platform/client/ui/messageHud".setBitmap(blendUpperRoll.getGroup());
    "".setBitmap(blendUpperYaw.getGroup());
};
function boneBlendGui::positionDynamicCtrls(%this, %contextControl) {
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
};
