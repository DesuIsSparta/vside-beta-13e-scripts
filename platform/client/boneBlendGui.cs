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
    if (!(runOnce)) {
        %i = 0;
        %this;
        if (($MAX_FREE_BONE_BLENDS < %i)) {
            %index = ($FIRST_FREE_BLEND_INDEX + %i);
            PlayGui;
            $player.setBoneBlendOffsetByIndex(%index, getValue());
            $player.setBoneBlendRateByIndex(%index, getValue());
            $player.setBoneBlendScaleByIndex(%index, getValue());
            %i = (1.0 + %i);
            blendScaleSliderUniqueField;
        }
        $player.setBoneBlendRate(getValue());
        $player.setBoneBlendScale(getValue());
        runOnce = blendScaleSlider @ 1 @ %this;
        blendRateSlider;
    }
};
function boneBlendGui::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
currentSliderIndex = -(1.0) @ boneBlendGui;
function blendUpperPitch::onMouseEnter(%this) {
    %this.positionDynamicCtrls();
    currentSliderIndex = boneBlendGui @ $BB_UPPR_PITCH @ boneBlendGui;
    getGroup().setBitmap("platform/client/ui/messageHud");
    getGroup().setBitmap("");
    getGroup().setBitmap("");
};
function blendUpperYaw::onMouseEnter(%this) {
    %this.positionDynamicCtrls();
    currentSliderIndex = boneBlendGui @ $BB_UPPR_YAW @ boneBlendGui;
    getGroup().setBitmap("platform/client/ui/messageHud");
    getGroup().setBitmap("");
    getGroup().setBitmap("");
};
function blendUpperRoll::onMouseEnter(%this) {
    %this.positionDynamicCtrls();
    currentSliderIndex = boneBlendGui @ $BB_UPPR_ROLL @ boneBlendGui;
    getGroup().setBitmap("");
    getGroup().setBitmap("platform/client/ui/messageHud");
    getGroup().setBitmap("");
};
function boneBlendGui::positionDynamicCtrls(%this, %contextControl) {
    %groupPosition = VectorAdd(position, groupOffset1);
    boneBlendGui;
    position = %contextControl.getGroup() @ %groupPosition @ blendCyclicsGroup;
    %Position1 = VectorAdd(position, sliderOffset1);
    boneBlendGui;
    %Position2 = VectorAdd(position, sliderOffset2);
    boneBlendGui;
    %Position3 = VectorAdd(position, sliderOffset3);
    boneBlendGui;
    position = %contextControl.getGroup() @ %Position1 @ blendScaleSliderUniqueField;
    %contextControl.getGroup();
    position = %contextControl.getGroup() @ %Position2 @ blendRateSliderUniqueField;
    position = %Position3 @ blendOffsetSliderUniqueField;
    %Position1 = VectorAdd(position, labelOffset1);
    boneBlendGui;
    %Position2 = VectorAdd(position, labelOffset2);
    boneBlendGui;
    %Position3 = VectorAdd(position, labelOffset3);
    boneBlendGui;
    position = %contextControl.getGroup() @ %Position1 @ blendScaleLabel;
    %contextControl.getGroup();
    position = %contextControl.getGroup() @ %Position2 @ blendRateLabel;
    position = %Position3 @ blendOffsetLabel;
};
