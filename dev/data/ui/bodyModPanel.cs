if (!(isObject(GuiBMPRed_Scroll))) {
    new GuiControlProfile(GuiBMPRed_Scroll) {
        fontColor = "255 0 0";
        justify = "center";
        fillColor = "200 200 200 120";
        border = 1;
        borderColor = "255 0 0";
    };
}
if (!(isObject(GuiBMPBlue_Scroll))) {
    new GuiControlProfile(GuiBMPBlue_Scroll : GuiBMPRed_Scroll) {
        fontColor = "0 0 255";
        borderColor = "0 0 255";
    };
}
if (!(isObject(GuiBMPRed_TE))) {
    new GuiControlProfile(GuiBMPRed_TE : GuiTextEditProfile) {
        fontColor = "255 0 0";
        justify = "center";
        tab = 0;
        autoSizeHeight = 0;
        fillColor = "200 200 200 120";
        border = 1;
        borderColorHL = "0 0 0";
        borderColor = "255 0 0";
    };
}
if (!(isObject(GuiBMPBlue_TE))) {
    new GuiControlProfile(GuiBMPBlue_TE : GuiBMPRed_TE) {
        fontColor = "0 0 255";
        borderColor = "0 0 255";
    };
}
if (!(isObject(GuiBMPRed_Button))) {
    new GuiControlProfile(GuiBMPRed_Button : GuiClickLabelProfile) {
        fontColor = "255 0 0";
        border = 1;
        borderColor = "255 0 0";
        justify = "center";
    };
}
if (!(isObject(GuiBMPBlue_Button))) {
    new GuiControlProfile(GuiBMPBlue_Button : GuiBMPRed_Button) {
        fontColor = "0 0 255";
        borderColor = "0 0 255";
    };
}
$bodyModPanel::BMBIGC = "BMBIGC";
$bodyModPanel::BMBIResetScaleButton = "BMBIResetScaleButton";
$bodyModPanel::BMBIAntiScaleCheckBox = "BMBIAntiScaleCheckBox";
$bodyModPanel::BMBIResetOffsetButton = "BMBIResetOffsetButton";
$bodyModPanel::BMBIAntiOffsetCheckBox = "BMBIAntiOffsetCheckBox";
$bodyModPanel::BMXGC = "BMXGC";
$bodyModPanel::BMYGC = "BMYGC";
$bodyModPanel::BMZGC = "BMZGC";
$bodyModPanel::BMXScaleSlider = "BMXScaleSlider";
$bodyModPanel::BMYScaleSlider = "BMYScaleSlider";
$bodyModPanel::BMZScaleSlider = "BMZScaleSlider";
$bodyModPanel::BMXScaleTextEdit = "BMXScaleTextEdit";
$bodyModPanel::BMYScaleTextEdit = "BMYScaleTextEdit";
$bodyModPanel::BMZScaleTextEdit = "BMZScaleTextEdit";
$bodyModPanel::BMXOffsetSlider = "BMXOffsetSlider";
$bodyModPanel::BMYOffsetSlider = "BMYOffsetSlider";
$bodyModPanel::BMZOffsetSlider = "BMZOffsetSlider";
$bodyModPanel::BMXOffsetTextEdit = "BMXOffsetTextEdit";
$bodyModPanel::BMYOffsetTextEdit = "BMYOffsetTextEdit";
$bodyModPanel::BMZOffsetTextEdit = "BMZOffsetTextEdit";
$bodyModPanel::scaleBoneRange = "0.05 5.0";
$bodyModPanel::offsetBoneRange = "-5.5 5.5";
function bodyModPanel::toggle(%this) {
    %this.showRaiseOrHide(playGui);
};
function bodyModPanel::createBodyModInfoCell(%realRow, %realCol, %boneIndex, %indexName, %buttonProfile) {
    %gcGCName = bodyModPanel::getArrayGCName($bodyModPanel::BMBIGC, %realRow, %realCol);
    %gcResetScaleButton = bodyModPanel::getArrayGCName($bodyModPanel::BMBIResetScaleButton, %realRow, %realCol);
    %gcAntiScaleCheckBox = bodyModPanel::getArrayGCName($bodyModPanel::BMBIAntiScaleCheckBox, %realRow, %realCol);
    %gcResetOffsetButton = bodyModPanel::getArrayGCName($bodyModPanel::BMBIResetOffsetButton, %realRow, %realCol);
    if (!(isObject(%gcGCName))) {
        %gcObj = bodyModPanelArray.addChild();
        %gcGCName.setName(%gcObj);
        %buttonProfile.setProfile(%gcObj);
        %gcObj.boneIndex = %boneIndex;
        %childGcObj = new GuiTextCtrl("") {
            profile = %buttonProfile;
            position = "3 30";
            extent = "114 19";
        };
        %childGcObj.add(%gcObj);
        %childGcObj = new GuiButtonCtrl(%gcResetScaleButton) {
            profile = %buttonProfile;
            position = "4 4";
            extent = "68 19";
            text = "Reset Scale";
            command = "bodyModPanel::setModGCControllers(true,$ThisControl.boneIndex,\"xyz\",1);bodyModPanel::setAntiModToChildren(true,$ThisControl.boneIndex,\"xyz\",1);";
        };
        %childGcObj.add(%gcObj);
        %childGcObj = new GuiTextCtrl("") {
            profile = %buttonProfile;
            position = "71 6";
            extent = "35 14";
            text = "antiS";
        };
        %childGcObj.add(%gcObj);
        %childGcObj = new GuiCheckBoxCtrl(%gcAntiScaleCheckBox) {
            profile = "GuiCheckBoxProfile";
            position = "103 7";
            extent = "14 14";
            buttonType = "ToggleButton";
        };
        %childGcObj.add(%gcObj);
        %childGcObj = new GuiButtonCtrl(%gcResetOffsetButton) {
            profile = %buttonProfile;
            position = "4 57";
            extent = "68 19";
            text = "Reset Offset";
            command = "bodyModPanel::setModGCControllers(false,$ThisControl.boneIndex,\"xyz\",0);bodyModPanel::setAntiModToChildren(false,$ThisControl.boneIndex,\"xyz\",0);";
        };
        %childGcObj.add(%gcObj);
    }
    %boneIndexStr = %boneIndex @ " " @ "-" @ " " @ %indexName;
    %boneIndexStr.setText(0.getObject(%gcGCName));
    %gcResetScaleButton.boneIndex = %boneIndex;
    %gcResetOffsetButton.boneIndex = %boneIndex;
};
function bmCellBG::onMouseEnterBounds(%this) {
    $pref::TS::highlightBone = %this.boneIndex;
    if ((%this.origProfile $= "")) {
        %this.origProfile = %this.profile;
        %this.profile = GuiDefaultProfile;
    }
};
function bmCellBG::onMouseLeaveBounds(%this) {
    if (!(%this.origProfile $= "")) {
        %this.profile = %this.origProfile;
        %this.origProfile = "";
    }
};
function bodyModPanel::createBodyModCell(%axis, %realRow, %realCol, %boneIndex, %scale, %offset, %scrollProfile, %TEProfile) {
    %axis = strlwr(%axis);
    if ((%axis $= "x")) {
        %gcGCName = bodyModPanel::getArrayGCName($bodyModPanel::BMXGC, %realRow, %realCol);
        %gcScaleSliderName = bodyModPanel::getArrayGCName($bodyModPanel::BMXScaleSlider, %realRow, %realCol);
        %gcScaleTEName = bodyModPanel::getArrayGCName($bodyModPanel::BMXScaleTextEdit, %realRow, %realCol);
        %gcOffsetSliderName = bodyModPanel::getArrayGCName($bodyModPanel::BMXOffsetSlider, %realRow, %realCol);
        %gcOffsetTEName = bodyModPanel::getArrayGCName($bodyModPanel::BMXOffsetTextEdit, %realRow, %realCol);
    }
    if ((%axis $= "y")) {
        %gcGCName = bodyModPanel::getArrayGCName($bodyModPanel::BMYGC, %realRow, %realCol);
        %gcScaleSliderName = bodyModPanel::getArrayGCName($bodyModPanel::BMYScaleSlider, %realRow, %realCol);
        %gcScaleTEName = bodyModPanel::getArrayGCName($bodyModPanel::BMYScaleTextEdit, %realRow, %realCol);
        %gcOffsetSliderName = bodyModPanel::getArrayGCName($bodyModPanel::BMYOffsetSlider, %realRow, %realCol);
        %gcOffsetTEName = bodyModPanel::getArrayGCName($bodyModPanel::BMYOffsetTextEdit, %realRow, %realCol);
    }
    if ((%axis $= "z")) {
        %gcGCName = bodyModPanel::getArrayGCName($bodyModPanel::BMZGC, %realRow, %realCol);
        %gcScaleSliderName = bodyModPanel::getArrayGCName($bodyModPanel::BMZScaleSlider, %realRow, %realCol);
        %gcScaleTEName = bodyModPanel::getArrayGCName($bodyModPanel::BMZScaleTextEdit, %realRow, %realCol);
        %gcOffsetSliderName = bodyModPanel::getArrayGCName($bodyModPanel::BMZOffsetSlider, %realRow, %realCol);
        %gcOffsetTEName = bodyModPanel::getArrayGCName($bodyModPanel::BMZOffsetTextEdit, %realRow, %realCol);
    }
    return;
    if (!(isObject(%gcGCName))) {
        %gcObj = bodyModPanelArray.addChild();
        %gcGCName.setName(%gcObj);
        %scrollProfile.setProfile(%gcObj);
        %gcObj.boneIndex = %boneIndex;
        %childGcObj = new GuiSliderCtrl(%gcScaleSliderName) {
            profile = %scrollProfile;
            position = "4 2";
            extent = "102 16";
            altCommand = "bodyModPanel::applyChangeFromGC($ThisControl);";
            range = $bodyModPanel::scaleBoneRange;
        };
        %childGcObj.add(%gcObj);
        %childGcObj = new GuiTextEditCtrl(%gcScaleTEName) {
            profile = %TEProfile;
            position = "23 19";
            extent = "64 18";
            altCommand = "bodyModPanel::applyChangeFromGC($ThisControl);";
        };
        %childGcObj.add(%gcObj);
        %childGcObj = new GuiSliderCtrl(%gcOffsetSliderName) {
            profile = %scrollProfile;
            position = "4 39";
            extent = "102 18";
            altCommand = "bodyModPanel::applyChangeFromGC($ThisControl);";
            range = $bodyModPanel::offsetBoneRange;
        };
        %childGcObj.add(%gcObj);
        %childGcObj = new GuiTextEditCtrl(%gcOffsetTEName) {
            profile = %TEProfile;
            position = "23 58";
            extent = "64 18";
            altCommand = "bodyModPanel::applyChangeFromGC($ThisControl);";
        };
        %childGcObj.add(%gcObj);
    }
    %gcOffsetTEName.boneIndex = %boneIndex;
    %gcOffsetSliderName.boneIndex = ;
    %gcScaleTEName.boneIndex = ;
    %gcScaleSliderName.boneIndex = ;
    %gcScaleSliderName.prevValue = %scale;
    %gcOffsetSliderName.prevValue = %offset;
    %scale.setValue(%gcScaleSliderName);
    %scale.setText(%gcScaleTEName);
    %offset.setValue(%gcOffsetSliderName);
    %offset.setText(%gcOffsetTEName);
};
function bodyModPanel::open(%this) {
    if (!("debugActive".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    if (!(%this.isVisible())) {
        1.setVisible(%this);
        %this.focusAndRaise(playGui);
    }
    %numNodes = $player.getNumBones();
    if ((%numNodes == -(1.0))) {
        return;
    }
    %cells = (%numNodes * bodyModPanelArray.numRowsOrCols);
    %col = 0;
    %row = 0;
    %boneIndex = 0;
    %realRow = 0;
    %realCol = 0;
    while ((%col < %cells)) {
        %indexName = %boneIndex.getBoneName($player);
        if ((%indexName $= "")) {
            %boneIndex = (%boneIndex + 1.0);
            %realRow = (%realRow + 1.0);
            %realCol = 0;
        }
        if (((%row % 2) == 0.0)) {
            %buttonProfile = "GuiBMPRed_Button";
            %scrollProfile = "GuiBMPRed_Scroll";
            %TEProfile = "GuiBMPRed_TE";
        }
        %buttonProfile = "GuiBMPBlue_Button";
        %scrollProfile = "GuiBMPBlue_Scroll";
        %TEProfile = "GuiBMPBlue_TE";
        bodyModPanel::createBodyModInfoCell(%realRow, %realCol, %boneIndex, %indexName, %buttonProfile);
        %col = (%col + 1.0);
        %realCol = (%realCol + 1.0);
        %boneScale = %boneIndex.getBoneScaling($player);
        %scaleX = getWord(%boneScale, 0);
        %scaleY = getWord(%boneScale, 1);
        %scaleZ = getWord(%boneScale, 2);
        %boneOffset = %boneIndex.getBoneOffsetting($player);
        %offsetX = getWord(%boneOffset, 0);
        %offsetY = getWord(%boneOffset, 1);
        %offsetZ = getWord(%boneOffset, 2);
        bodyModPanel::createBodyModCell("x", %realRow, %realCol, %boneIndex, %scaleX, %offsetX, %scrollProfile, %TEProfile);
        %col = (%col + 1.0);
        %realCol = (%realCol + 1.0);
        bodyModPanel::createBodyModCell("y", %realRow, %realCol, %boneIndex, %scaleY, %offsetY, %scrollProfile, %TEProfile);
        %col = (%col + 1.0);
        %realCol = (%realCol + 1.0);
        bodyModPanel::createBodyModCell("z", %realRow, %realCol, %boneIndex, %scaleZ, %offsetZ, %scrollProfile, %TEProfile);
        %boneIndex = (%boneIndex + 1.0);
        %col = (%col + 1.0);
        %row = (%row + 1.0);
        %realRow = (%realRow + 1.0);
        %realCol = 0;
    }
    %n = (bodyModPanelArray.getCount() - 1.0);
    (%col < %cells);
    while ((%n >= 0.0)) {
        %ctrl = %n.getObject(bodyModPanelArray);
        if (!(hasWord(%ctrl.getNamespaceList(), "bmCellBG"))) {
            "bmCellBG".bindClassName(%ctrl);
        }
        %n = (%n - 1.0);
    }
    bodyModPanelArray.reseatChildren();
    $pref::TS::bodyMod.setGCVisible(%this);
};
function bodyModPanel::close(%this) {
    $pref::TS::highlightBone = -(1.0);
    0.setVisible(%this);
    playGui.focusTopWindow();
    return 1;
};
function bodyModPanel::setGCVisible(%this, %val) {
    if (!("debugActive".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    %i = 1;
    while ((%i < %this.getCount())) {
        %child = %i.getObject(%this);
        if (isObject(%child)) {
            %val.setVisible(%child);
        }
        %i = (%i + 1.0);
    }
};
function bodyModPanel::getArrayGCName(%prefix, %row, %col) {
    if (!("debugActive".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    %gcName = %prefix @ "_" @ %row @ "_" @ %col;
    return %gcName;
};
function ClassBodyMod::setBoneMod(%isScale, %boneIndex, %axis, %val) {
    if ((%axis $= "")) {
    }
    if ((%boneIndex $= "")) {
    }
    if ((%val $= "")) {
        return "";
    }
    if (%isScale) {
        %scaleOffsetValue = %boneIndex.getBoneScaling($player);
    }
    %scaleOffsetValue = %boneIndex.getBoneOffsetting($player);
    %axis = strlwr(%axis);
    if ((%axis $= "x")) {
        %scaleOffsetValue = setWord(%scaleOffsetValue, 0, %val);
    }
    if ((%axis $= "y")) {
        %scaleOffsetValue = setWord(%scaleOffsetValue, 1, %val);
    }
    if ((%axis $= "z")) {
        %scaleOffsetValue = setWord(%scaleOffsetValue, 2, %val);
    }
    if ((%axis $= "xy")) {
    }
    if ((%axis $= "yx")) {
        %scaleOffsetValue = setWord(%scaleOffsetValue, 0, %val);
        %scaleOffsetValue = setWord(%scaleOffsetValue, 1, %val);
    }
    if ((%axis $= "xz")) {
    }
    if ((%axis $= "zx")) {
        %scaleOffsetValue = setWord(%scaleOffsetValue, 0, %val);
        %scaleOffsetValue = setWord(%scaleOffsetValue, 2, %val);
    }
    if ((%axis $= "yz")) {
    }
    if ((%axis $= "zy")) {
        %scaleOffsetValue = setWord(%scaleOffsetValue, 1, %val);
        %scaleOffsetValue = setWord(%scaleOffsetValue, 2, %val);
    }
    if ((%axis $= "xyz")) {
    }
    if ((%axis $= "yzx")) {
    }
    if ((%axis $= "zxy")) {
    }
    if ((%axis $= "yxz")) {
    }
    if ((%axis $= "zyx")) {
    }
    if ((%axis $= "xzy")) {
        %scaleOffsetValue = setWord(%scaleOffsetValue, 0, %val);
        %scaleOffsetValue = setWord(%scaleOffsetValue, 1, %val);
        %scaleOffsetValue = setWord(%scaleOffsetValue, 2, %val);
    }
    if (%isScale) {
        if (!(%scaleOffsetValue.setScaleBone($player, %boneIndex))) {
            %scaleOffsetValue = "1 1 1";
            %scaleOffsetValue.setScaleBone($player, %boneIndex);
        }
    }
    if (!(%scaleOffsetValue.setOffsetBone($player, %boneIndex))) {
        %scaleOffsetValue = "0 0 0";
        %scaleOffsetValue.setOffsetBone($player, %boneIndex);
    }
    return %scaleOffsetValue;
};
function bodyModPanel::setModGCControllers(%isScale, %boneIndex, %axis, %val) {
    if (!("debugActive".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    if (%isScale) {
        %xSlider = bodyModPanel::getArrayGCName($bodyModPanel::BMXScaleSlider, %boneIndex, 1);
        %xTEGC = bodyModPanel::getArrayGCName($bodyModPanel::BMXScaleTextEdit, %boneIndex, 1);
        if (!(isObject(%xSlider))) {
        }
        if (!(isObject(%xTEGC))) {
            return;
        }
        %ySlider = bodyModPanel::getArrayGCName($bodyModPanel::BMYScaleSlider, %boneIndex, 2);
        %yTEGC = bodyModPanel::getArrayGCName($bodyModPanel::BMYScaleTextEdit, %boneIndex, 2);
        if (!(isObject(%ySlider))) {
        }
        if (!(isObject(%yTEGC))) {
            return;
        }
        %zSlider = bodyModPanel::getArrayGCName($bodyModPanel::BMZScaleSlider, %boneIndex, 3);
        %zTEGC = bodyModPanel::getArrayGCName($bodyModPanel::BMZScaleTextEdit, %boneIndex, 3);
        if (!(isObject(%zSlider))) {
        }
        if (!(isObject(%zTEGC))) {
            return;
        }
    }
    %xSlider = bodyModPanel::getArrayGCName($bodyModPanel::BMXOffsetSlider, %boneIndex, 1);
    %xTEGC = bodyModPanel::getArrayGCName($bodyModPanel::BMXOffsetTextEdit, %boneIndex, 1);
    if (!(isObject(%xSlider))) {
    }
    if (!(isObject(%xTEGC))) {
        return;
    }
    %ySlider = bodyModPanel::getArrayGCName($bodyModPanel::BMYOffsetSlider, %boneIndex, 2);
    %yTEGC = bodyModPanel::getArrayGCName($bodyModPanel::BMYOffsetTextEdit, %boneIndex, 2);
    if (!(isObject(%ySlider))) {
    }
    if (!(isObject(%yTEGC))) {
        return;
    }
    %zSlider = bodyModPanel::getArrayGCName($bodyModPanel::BMZOffsetSlider, %boneIndex, 3);
    %zTEGC = bodyModPanel::getArrayGCName($bodyModPanel::BMZOffsetTextEdit, %boneIndex, 3);
    if (!(isObject(%zSlider))) {
    }
    if (!(isObject(%zTEGC))) {
        return;
    }
    %scaleOffsetVal = ClassBodyMod::setBoneMod(%isScale, %boneIndex, %axis, %val);
    getWord(%scaleOffsetVal, 0).setValue(%xSlider);
    %xSlider.prevValue = %xSlider.getValue();
    %xSlider.getValue().setText(%xTEGC);
    getWord(%scaleOffsetVal, 1).setValue(%ySlider);
    %ySlider.prevValue = %ySlider.getValue();
    %ySlider.getValue().setText(%yTEGC);
    getWord(%scaleOffsetVal, 2).setValue(%zSlider);
    %zSlider.prevValue = %zSlider.getValue();
    %zSlider.getValue().setText(%zTEGC);
};
function bodyModPanel::setAllBoneMods(%axis, %scaleVal, %offsetVal) {
    if (!("debugActive".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    bodyModPanel::setAllBoneScale(%axis, %scaleVal);
    bodyModPanel::setAllBoneOffsets(%axis, %offsetVal);
};
function bodyModPanel::setAllBoneScale(%axis, %val) {
    if (!("debugActive".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    %numNodes = $player.getNumBones();
    if ((%numNodes == -(1.0))) {
        return;
    }
    %boneIndex = 0;
    while ((%boneIndex < %numNodes)) {
        bodyModPanel::setModGCControllers(1, %boneIndex, %axis, %val);
        %boneIndex = (%boneIndex + 1.0);
    }
};
function bodyModPanel::setAllBoneOffsets(%axis, %val) {
    if (!("debugActive".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    %numNodes = $player.getNumBones();
    if ((%numNodes == -(1.0))) {
        return;
    }
    %boneIndex = 0;
    while ((%boneIndex < %numNodes)) {
        bodyModPanel::setModGCControllers(0, %boneIndex, %axis, %val);
        %boneIndex = (%boneIndex + 1.0);
    }
};
function bodyModPanel::applyChangeFromGC(%gc) {
    %gcName = strlwr(%gc.getName());
    %isScale = 0;
    if ((strstr(%gcName, "scale") != -(1.0))) {
        %isScale = 1;
    }
    %isSlider = 0;
    if ((strstr(%gcName, "slider") != -(1.0))) {
        %isSlider = 1;
    }
    %value = 0;
    if (%isSlider) {
        %value = %gc.getValue();
    }
    %value = %gc.getText();
    if (%isScale) {
        %rangeMin = getWord($bodyModPanel::scaleBoneRange, 0);
        %rangeMax = getWord($bodyModPanel::scaleBoneRange, 1);
    }
    %rangeMin = getWord($bodyModPanel::offsetBoneRange, 0);
    %rangeMax = getWord($bodyModPanel::offsetBoneRange, 1);
    if ((%value < %rangeMin)) {
        %value = %rangeMin;
    }
    if ((%value > %rangeMax)) {
        %value = %rangeMax;
    }
    %slider = 0;
    %te = 0;
    if (%isScale) {
        %slider = 0.getObject(%gc.getParent());
        %te = 1.getObject(%gc.getParent());
    }
    %slider = 2.getObject(%gc.getParent());
    %te = 3.getObject(%gc.getParent());
    if (!(isObject(%slider))) {
    }
    if (!(isObject(%te))) {
        return;
    }
    %value.setValue(%slider);
    %value.setText(%te);
    bodyModPanel::setBoneWithAntiMod(%isScale, %slider);
};
function bodyModPanel::setBoneWithAntiMod(%isScale, %slider) {
    if (!("debugActive".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    if (!(isObject(%slider))) {
        return;
    }
    %gcName = strlwr(%slider.getName());
    %axis = "";
    if ((strstr(%gcName, "x") != -(1.0))) {
        %axis = "x";
    }
    if ((strstr(%gcName, "y") != -(1.0))) {
        %axis = "y";
    }
    if ((strstr(%gcName, "z") != -(1.0))) {
        %axis = "z";
    }
    return;
    %curParentValue = %slider.getValue();
    %boneIndex = %slider.boneIndex;
    ClassBodyMod::setBoneMod(%isScale, %boneIndex, %axis, %curParentValue);
    %prevParentValue = %slider.prevValue;
    %deltaParentValue = (%curParentValue - %prevParentValue);
    %slider.prevValue = %curParentValue;
    %demo = %curParentValue;
    if ((%demo == 0.0)) {
        return;
    }
    if (%isScale) {
        %gcCheckBox = bodyModPanel::getArrayGCName($bodyModPanel::BMBIAntiScaleCheckBox, %boneIndex, 0);
    }
    return;
    if (!(isObject(%gcCheckBox))) {
    }
    if (!(%gcCheckBox.getValue())) {
        return;
    }
    %childBones = %boneIndex.getChildBones($player);
    %numBones = getWordCount(%childBones);
    if (!(%numBones)) {
        return;
    }
    if (%isScale) {
        %rangeMin = getWord($bodyModPanel::scaleBoneRange, 0);
        %rangeMax = getWord($bodyModPanel::scaleBoneRange, 1);
    }
    %rangeMin = getWord($bodyModPanel::offsetBoneRange, 0);
    %rangeMax = getWord($bodyModPanel::offsetBoneRange, 1);
    %i = 0;
    while ((%i < %numBones)) {
        %childBoneIndex = getWord(%childBones, %i);
        if ((%axis $= "x")) {
            if (%isScale) {
                %sliderGC = bodyModPanel::getArrayGCName($bodyModPanel::BMXScaleSlider, %childBoneIndex, 1);
                %teGC = bodyModPanel::getArrayGCName($bodyModPanel::BMXScaleTextEdit, %childBoneIndex, 1);
            }
            %sliderGC = bodyModPanel::getArrayGCName($bodyModPanel::BMXOffsetSlider, %childBoneIndex, 1);
            %teGC = bodyModPanel::getArrayGCName($bodyModPanel::BMXOffsetTextEdit, %childBoneIndex, 1);
        }
        if ((%axis $= "y")) {
            if (%isScale) {
                %sliderGC = bodyModPanel::getArrayGCName($bodyModPanel::BMYScaleSlider, %childBoneIndex, 2);
                %teGC = bodyModPanel::getArrayGCName($bodyModPanel::BMYScaleTextEdit, %childBoneIndex, 2);
            }
            %sliderGC = bodyModPanel::getArrayGCName($bodyModPanel::BMYOffsetSlider, %childBoneIndex, 2);
            %teGC = bodyModPanel::getArrayGCName($bodyModPanel::BMYOffsetTextEdit, %childBoneIndex, 2);
        }
        if ((%axis $= "z")) {
            if (%isScale) {
                %sliderGC = bodyModPanel::getArrayGCName($bodyModPanel::BMZScaleSlider, %childBoneIndex, 3);
                %teGC = bodyModPanel::getArrayGCName($bodyModPanel::BMZScaleTextEdit, %childBoneIndex, 3);
            }
            %sliderGC = bodyModPanel::getArrayGCName($bodyModPanel::BMZOffsetSlider, %childBoneIndex, 3);
            %teGC = bodyModPanel::getArrayGCName($bodyModPanel::BMZOffsetTextEdit, %childBoneIndex, 3);
        }
        if (!(isObject(%sliderGC))) {
        }
        if (!(isObject(%teGC))) {
        }
        %prevChildValue = %sliderGC.getValue();
        %numo = ((-(1.0) * %prevChildValue) * %deltaParentValue);
        %deltaChildValue = (%numo / %demo);
        %newChildValue = (%prevChildValue + %deltaChildValue);
        if ((%newChildValue < %rangeMin)) {
            %newChildValue = %rangeMin;
        }
        if ((%newChildValue > %rangeMax)) {
            %newChildValue = %rangeMax;
        }
        %sliderGC.prevValue = %sliderGC.getValue();
        %newChildValue.setValue(%sliderGC);
        %newChildValue.setText(%teGC);
        ClassBodyMod::setBoneMod(%isScale, %childBoneIndex, %axis, %sliderGC.getValue());
        %i = (%i + 1.0);
    }
};
function bodyModPanel::setAntiModToChildren(%isScale, %parentBoneIndex, %axis, %val) {
    if (!("debugActive".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    if (%isScale) {
        %gcCheckBox = bodyModPanel::getArrayGCName($bodyModPanel::BMBIAntiScaleCheckBox, %parentBoneIndex, 0);
    }
    return;
    if (!(isObject(%gcCheckBox))) {
    }
    if (!(%gcCheckBox.getValue())) {
        return;
    }
    %childBones = %parentBoneIndex.getChildBones($player);
    %numBones = getWordCount(%childBones);
    if (!(%numBones)) {
        return;
    }
    %i = 0;
    while ((%i < %numBones)) {
        %childBoneIndex = getWord(%childBones, %i);
        bodyModPanel::setModGCControllers(%isScale, %childBoneIndex, %axis, %val);
        %i = (%i + 1.0);
    }
};
