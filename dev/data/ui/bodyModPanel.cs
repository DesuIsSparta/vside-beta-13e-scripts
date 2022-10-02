if (!isObject(GuiBMPRed_Scroll))
{
}
new GuiControlProfile(GuiBMPRed_Scroll)
    {
        fontColor = "255 0 0";
        justify = "center";
        fillColor = "200 200 200 120";
        border = 1;
        borderColor = "255 0 0";
    };
if (!isObject(GuiBMPBlue_Scroll))
{
}
new GuiControlProfile(GuiBMPBlue_Scroll : GuiBMPRed_Scroll)
    {
        fontColor = "0 0 255";
        borderColor = "0 0 255";
    };
if (!isObject(GuiBMPRed_TE))
{
}
new GuiControlProfile(GuiBMPRed_TE : GuiTextEditProfile)
    {
        fontColor = "255 0 0";
        justify = "center";
        tab = 0;
        autoSizeHeight = 0;
        fillColor = "200 200 200 120";
        border = 1;
        borderColorHL = "0 0 0";
        borderColor = "255 0 0";
    };
if (!isObject(GuiBMPBlue_TE))
{
}
new GuiControlProfile(GuiBMPBlue_TE : GuiBMPRed_TE)
    {
        fontColor = "0 0 255";
        borderColor = "0 0 255";
    };
if (!isObject(GuiBMPRed_Button))
{
}
new GuiControlProfile(GuiBMPRed_Button : GuiClickLabelProfile)
    {
        fontColor = "255 0 0";
        border = 1;
        borderColor = "255 0 0";
        justify = "center";
    };
if (!isObject(GuiBMPBlue_Button))
{
}
new GuiControlProfile(GuiBMPBlue_Button : GuiBMPRed_Button)
    {
        fontColor = "0 0 255";
        borderColor = "0 0 255";
    };
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
function bodyModPanel::toggle(%this)
{
    playGui.showRaiseOrHide(%this);
    return ;
}
function bodyModPanel::createBodyModInfoCell(%realRow, %realCol, %boneIndex, %indexName, %buttonProfile)
{
    %gcGCName = bodyModPanel::getArrayGCName($bodyModPanel::BMBIGC, %realRow, %realCol);
    %gcResetScaleButton = bodyModPanel::getArrayGCName($bodyModPanel::BMBIResetScaleButton, %realRow, %realCol);
    %gcAntiScaleCheckBox = bodyModPanel::getArrayGCName($bodyModPanel::BMBIAntiScaleCheckBox, %realRow, %realCol);
    %gcResetOffsetButton = bodyModPanel::getArrayGCName($bodyModPanel::BMBIResetOffsetButton, %realRow, %realCol);
    if (!isObject(%gcGCName))
    {
        %gcObj = bodyModPanelArray.addChild();
        %gcObj.setName(%gcGCName);
        %gcObj.setProfile(%buttonProfile);
        %gcObj.boneIndex = %boneIndex;
        %childGcObj = new GuiTextCtrl()
        {
            profile = %buttonProfile;
            position = "3 30";
            extent = "114 19";
        };
        %gcObj.add(%childGcObj);
        %childGcObj = new GuiButtonCtrl(%gcResetScaleButton)
        {
            profile = %buttonProfile;
            position = "4 4";
            extent = "68 19";
            text = "Reset Scale";
            command = "bodyModPanel::setModGCControllers(true,$ThisControl.boneIndex,\"xyz\",1);bodyModPanel::setAntiModToChildren(true,$ThisControl.boneIndex,\"xyz\",1);";
        };
        %gcObj.add(%childGcObj);
        %childGcObj = new GuiTextCtrl()
        {
            profile = %buttonProfile;
            position = "71 6";
            extent = "35 14";
            text = "antiS";
        };
        %gcObj.add(%childGcObj);
        %childGcObj = new GuiCheckBoxCtrl(%gcAntiScaleCheckBox)
        {
            profile = "GuiCheckBoxProfile";
            position = "103 7";
            extent = "14 14";
            buttonType = "ToggleButton";
        };
        %gcObj.add(%childGcObj);
        %childGcObj = new GuiButtonCtrl(%gcResetOffsetButton)
        {
            profile = %buttonProfile;
            position = "4 57";
            extent = "68 19";
            text = "Reset Offset";
            command = "bodyModPanel::setModGCControllers(false,$ThisControl.boneIndex,\"xyz\",0);bodyModPanel::setAntiModToChildren(false,$ThisControl.boneIndex,\"xyz\",0);";
        };
        %gcObj.add(%childGcObj);
    }
    %boneIndexStr = %boneIndex SPC "-" SPC %indexName;
    %gcGCName.getObject(0).setText(%boneIndexStr);
    %gcResetScaleButton.boneIndex = %boneIndex;
    %gcResetOffsetButton.boneIndex = %boneIndex;
    return ;
}
function bmCellBG::onMouseEnterBounds(%this)
{
    $pref::TS::highlightBone = %this.boneIndex;
    if (%this.origProfile $= "")
    {
        %this.origProfile = %this.profile;
        %this.profile = GuiDefaultProfile;
    }
    return ;
}
function bmCellBG::onMouseLeaveBounds(%this)
{
    if (!(%this.origProfile $= ""))
    {
        %this.profile = %this.origProfile;
        %this.origProfile = "";
    }
    return ;
}
function bodyModPanel::createBodyModCell(%axis, %realRow, %realCol, %boneIndex, %scale, %offset, %scrollProfile, %TEProfile)
{
    %axis = strlwr(%axis);
    if (%axis $= "x")
    {
        %gcGCName = bodyModPanel::getArrayGCName($bodyModPanel::BMXGC, %realRow, %realCol);
        %gcScaleSliderName = bodyModPanel::getArrayGCName($bodyModPanel::BMXScaleSlider, %realRow, %realCol);
        %gcScaleTEName = bodyModPanel::getArrayGCName($bodyModPanel::BMXScaleTextEdit, %realRow, %realCol);
        %gcOffsetSliderName = bodyModPanel::getArrayGCName($bodyModPanel::BMXOffsetSlider, %realRow, %realCol);
        %gcOffsetTEName = bodyModPanel::getArrayGCName($bodyModPanel::BMXOffsetTextEdit, %realRow, %realCol);
    }
    else
    {
        if (%axis $= "y")
        {
            %gcGCName = bodyModPanel::getArrayGCName($bodyModPanel::BMYGC, %realRow, %realCol);
            %gcScaleSliderName = bodyModPanel::getArrayGCName($bodyModPanel::BMYScaleSlider, %realRow, %realCol);
            %gcScaleTEName = bodyModPanel::getArrayGCName($bodyModPanel::BMYScaleTextEdit, %realRow, %realCol);
            %gcOffsetSliderName = bodyModPanel::getArrayGCName($bodyModPanel::BMYOffsetSlider, %realRow, %realCol);
            %gcOffsetTEName = bodyModPanel::getArrayGCName($bodyModPanel::BMYOffsetTextEdit, %realRow, %realCol);
        }
        else
        {
            if (%axis $= "z")
            {
                %gcGCName = bodyModPanel::getArrayGCName($bodyModPanel::BMZGC, %realRow, %realCol);
                %gcScaleSliderName = bodyModPanel::getArrayGCName($bodyModPanel::BMZScaleSlider, %realRow, %realCol);
                %gcScaleTEName = bodyModPanel::getArrayGCName($bodyModPanel::BMZScaleTextEdit, %realRow, %realCol);
                %gcOffsetSliderName = bodyModPanel::getArrayGCName($bodyModPanel::BMZOffsetSlider, %realRow, %realCol);
                %gcOffsetTEName = bodyModPanel::getArrayGCName($bodyModPanel::BMZOffsetTextEdit, %realRow, %realCol);
            }
        }
    }
    else
    {
        return ;
    }
    if (!isObject(%gcGCName))
    {
        %gcObj = bodyModPanelArray.addChild();
        %gcObj.setName(%gcGCName);
        %gcObj.setProfile(%scrollProfile);
        %gcObj.boneIndex = %boneIndex;
        %childGcObj = new GuiSliderCtrl(%gcScaleSliderName)
        {
            profile = %scrollProfile;
            position = "4 2";
            extent = "102 16";
            altCommand = "bodyModPanel::applyChangeFromGC($ThisControl);";
            range = $bodyModPanel::scaleBoneRange;
        };
        %gcObj.add(%childGcObj);
        %childGcObj = new GuiTextEditCtrl(%gcScaleTEName)
        {
            profile = %TEProfile;
            position = "23 19";
            extent = "64 18";
            altCommand = "bodyModPanel::applyChangeFromGC($ThisControl);";
        };
        %gcObj.add(%childGcObj);
        %childGcObj = new GuiSliderCtrl(%gcOffsetSliderName)
        {
            profile = %scrollProfile;
            position = "4 39";
            extent = "102 18";
            altCommand = "bodyModPanel::applyChangeFromGC($ThisControl);";
            range = $bodyModPanel::offsetBoneRange;
        };
        %gcObj.add(%childGcObj);
        %childGcObj = new GuiTextEditCtrl(%gcOffsetTEName)
        {
            profile = %TEProfile;
            position = "23 58";
            extent = "64 18";
            altCommand = "bodyModPanel::applyChangeFromGC($ThisControl);";
        };
        %gcObj.add(%childGcObj);
    }
    %gcScaleSliderName.boneIndex = %gcScaleTEName.boneIndex = %gcOffsetSliderName.boneIndex = %gcOffsetTEName.boneIndex = %boneIndex;
    %gcScaleSliderName.prevValue = %scale;
    %gcOffsetSliderName.prevValue = %offset;
    %gcScaleSliderName.setValue(%scale);
    %gcScaleTEName.setText(%scale);
    %gcOffsetSliderName.setValue(%offset);
    %gcOffsetTEName.setText(%offset);
    return ;
}
function bodyModPanel::open(%this)
{
    if (!$player.rolesPermissionCheckNoWarn("debugActive"))
    {
        return ;
    }
    if (!%this.isVisible())
    {
        %this.setVisible(1);
        playGui.focusAndRaise(%this);
    }
    %numNodes = $player.getNumBones();
    if (%numNodes == -1)
    {
        return ;
    }
    %cells = %numNodes * bodyModPanelArray.numRowsOrCols;
    %col = 0;
    %row = 0;
    %boneIndex = 0;
    %realRow = 0;
    %realCol = 0;
    while (%col < %cells)
    {
        %indexName = $player.getBoneName(%boneIndex);
        if (%indexName $= "")
        {
            continue;
            %boneIndex = %boneIndex + 1;
            %realRow = %realRow + 1;
            %realCol = 0;
        }
        if ((%row % 2) == 0)
        {
            %buttonProfile = "GuiBMPRed_Button";
            %scrollProfile = "GuiBMPRed_Scroll";
            %TEProfile = "GuiBMPRed_TE";
        }
        else
        {
            %buttonProfile = "GuiBMPBlue_Button";
            %scrollProfile = "GuiBMPBlue_Scroll";
            %TEProfile = "GuiBMPBlue_TE";
        }
        bodyModPanel::createBodyModInfoCell(%realRow, %realCol, %boneIndex, %indexName, %buttonProfile);
        %col = %col + 1;
        %realCol = %realCol + 1;
        %boneScale = $player.getBoneScaling(%boneIndex);
        %scaleX = getWord(%boneScale, 0);
        %scaleY = getWord(%boneScale, 1);
        %scaleZ = getWord(%boneScale, 2);
        %boneOffset = $player.getBoneOffsetting(%boneIndex);
        %offsetX = getWord(%boneOffset, 0);
        %offsetY = getWord(%boneOffset, 1);
        %offsetZ = getWord(%boneOffset, 2);
        bodyModPanel::createBodyModCell("x", %realRow, %realCol, %boneIndex, %scaleX, %offsetX, %scrollProfile, %TEProfile);
        %col = %col + 1;
        %realCol = %realCol + 1;
        bodyModPanel::createBodyModCell("y", %realRow, %realCol, %boneIndex, %scaleY, %offsetY, %scrollProfile, %TEProfile);
        %col = %col + 1;
        %realCol = %realCol + 1;
        bodyModPanel::createBodyModCell("z", %realRow, %realCol, %boneIndex, %scaleZ, %offsetZ, %scrollProfile, %TEProfile);
        %boneIndex = %boneIndex + 1;
        %col = %col + 1;
        %row = %row + 1;
        %realRow = %realRow + 1;
        %realCol = 0;
    }
    %n = bodyModPanelArray.getCount() - 1;
    while (%n >= 0)
    {
        %ctrl = bodyModPanelArray.getObject(%n);
        if (!hasWord(%ctrl.getNamespaceList(), "bmCellBG"))
        {
            %ctrl.bindClassName("bmCellBG");
        }
        %n = %n - 1;
    }
    bodyModPanelArray.reseatChildren();
    %this.setGCVisible($pref::TS::bodyMod);
    return ;
}
function bodyModPanel::close(%this)
{
    $pref::TS::highlightBone = -1;
    %this.setVisible(0);
    playGui.focusTopWindow();
    return 1;
}
function bodyModPanel::setGCVisible(%this, %val)
{
    if (!$player.rolesPermissionCheckNoWarn("debugActive"))
    {
        return ;
    }
    %i = 1;
    while (%i < %this.getCount())
    {
        %child = %this.getObject(%i);
        if (isObject(%child))
        {
            %child.setVisible(%val);
        }
        %i = %i + 1;
    }
}

function bodyModPanel::getArrayGCName(%prefix, %row, %col)
{
    if (!$player.rolesPermissionCheckNoWarn("debugActive"))
    {
        return ;
    }
    %gcName = %prefix @ "_" @ %row @ "_" @ %col;
    return %gcName;
}
function ClassBodyMod::setBoneMod(%isScale, %boneIndex, %axis, %val)
{
    if (((%axis $= "") || (%boneIndex $= "")) || (%val $= ""))
    {
        return "";
    }
    if (%isScale)
    {
        %scaleOffsetValue = $player.getBoneScaling(%boneIndex);
    }
    else
    {
        %scaleOffsetValue = $player.getBoneOffsetting(%boneIndex);
    }
    %axis = strlwr(%axis);
    if (%axis $= "x")
    {
        %scaleOffsetValue = setWord(%scaleOffsetValue, 0, %val);
    }
    else
    {
        if (%axis $= "y")
        {
            %scaleOffsetValue = setWord(%scaleOffsetValue, 1, %val);
        }
        else
        {
            if (%axis $= "z")
            {
                %scaleOffsetValue = setWord(%scaleOffsetValue, 2, %val);
            }
            else
            {
                if ((%axis $= "xy") && (%axis $= "yx"))
                {
                    %scaleOffsetValue = setWord(%scaleOffsetValue, 0, %val);
                    %scaleOffsetValue = setWord(%scaleOffsetValue, 1, %val);
                }
                else
                {
                    if ((%axis $= "xz") && (%axis $= "zx"))
                    {
                        %scaleOffsetValue = setWord(%scaleOffsetValue, 0, %val);
                        %scaleOffsetValue = setWord(%scaleOffsetValue, 2, %val);
                    }
                    else
                    {
                        if ((%axis $= "yz") && (%axis $= "zy"))
                        {
                            %scaleOffsetValue = setWord(%scaleOffsetValue, 1, %val);
                            %scaleOffsetValue = setWord(%scaleOffsetValue, 2, %val);
                        }
                        else
                        {
                            if ((((((%axis $= "xyz") || (%axis $= "yzx")) || (%axis $= "zxy")) || (%axis $= "yxz")) || (%axis $= "zyx")) || (%axis $= "xzy"))
                            {
                                %scaleOffsetValue = setWord(%scaleOffsetValue, 0, %val);
                                %scaleOffsetValue = setWord(%scaleOffsetValue, 1, %val);
                                %scaleOffsetValue = setWord(%scaleOffsetValue, 2, %val);
                            }
                        }
                    }
                }
            }
        }
    }
    if (%isScale)
    {
        if (!$player.setScaleBone(%boneIndex, %scaleOffsetValue))
        {
            %scaleOffsetValue = "1 1 1";
            $player.setScaleBone(%boneIndex, %scaleOffsetValue);
        }
    }
    else
    {
        if (!$player.setOffsetBone(%boneIndex, %scaleOffsetValue))
        {
            %scaleOffsetValue = "0 0 0";
            $player.setOffsetBone(%boneIndex, %scaleOffsetValue);
        }
    }
    return %scaleOffsetValue;
}
function bodyModPanel::setModGCControllers(%isScale, %boneIndex, %axis, %val)
{
    if (!$player.rolesPermissionCheckNoWarn("debugActive"))
    {
        return ;
    }
    if (%isScale)
    {
        %xSlider = bodyModPanel::getArrayGCName($bodyModPanel::BMXScaleSlider, %boneIndex, 1);
        %xTEGC = bodyModPanel::getArrayGCName($bodyModPanel::BMXScaleTextEdit, %boneIndex, 1);
        if (!isObject(%xSlider) && !isObject(%xTEGC))
        {
            return ;
        }
        %ySlider = bodyModPanel::getArrayGCName($bodyModPanel::BMYScaleSlider, %boneIndex, 2);
        %yTEGC = bodyModPanel::getArrayGCName($bodyModPanel::BMYScaleTextEdit, %boneIndex, 2);
        if (!isObject(%ySlider) && !isObject(%yTEGC))
        {
            return ;
        }
        %zSlider = bodyModPanel::getArrayGCName($bodyModPanel::BMZScaleSlider, %boneIndex, 3);
        %zTEGC = bodyModPanel::getArrayGCName($bodyModPanel::BMZScaleTextEdit, %boneIndex, 3);
        if (!isObject(%zSlider) && !isObject(%zTEGC))
        {
            return ;
        }
    }
    else
    {
        %xSlider = bodyModPanel::getArrayGCName($bodyModPanel::BMXOffsetSlider, %boneIndex, 1);
        %xTEGC = bodyModPanel::getArrayGCName($bodyModPanel::BMXOffsetTextEdit, %boneIndex, 1);
        if (!isObject(%xSlider) && !isObject(%xTEGC))
        {
            return ;
        }
        %ySlider = bodyModPanel::getArrayGCName($bodyModPanel::BMYOffsetSlider, %boneIndex, 2);
        %yTEGC = bodyModPanel::getArrayGCName($bodyModPanel::BMYOffsetTextEdit, %boneIndex, 2);
        if (!isObject(%ySlider) && !isObject(%yTEGC))
        {
            return ;
        }
        %zSlider = bodyModPanel::getArrayGCName($bodyModPanel::BMZOffsetSlider, %boneIndex, 3);
        %zTEGC = bodyModPanel::getArrayGCName($bodyModPanel::BMZOffsetTextEdit, %boneIndex, 3);
        if (!isObject(%zSlider) && !isObject(%zTEGC))
        {
            return ;
        }
    }
    %scaleOffsetVal = ClassBodyMod::setBoneMod(%isScale, %boneIndex, %axis, %val);
    %xSlider.setValue(getWord(%scaleOffsetVal, 0));
    %xSlider.prevValue = %xSlider.getValue();
    %xTEGC.setText(%xSlider.getValue());
    %ySlider.setValue(getWord(%scaleOffsetVal, 1));
    %ySlider.prevValue = %ySlider.getValue();
    %yTEGC.setText(%ySlider.getValue());
    %zSlider.setValue(getWord(%scaleOffsetVal, 2));
    %zSlider.prevValue = %zSlider.getValue();
    %zTEGC.setText(%zSlider.getValue());
    return ;
}
function bodyModPanel::setAllBoneMods(%axis, %scaleVal, %offsetVal)
{
    if (!$player.rolesPermissionCheckNoWarn("debugActive"))
    {
        return ;
    }
    bodyModPanel::setAllBoneScale(%axis, %scaleVal);
    bodyModPanel::setAllBoneOffsets(%axis, %offsetVal);
    return ;
}
function bodyModPanel::setAllBoneScale(%axis, %val)
{
    if (!$player.rolesPermissionCheckNoWarn("debugActive"))
    {
        return ;
    }
    %numNodes = $player.getNumBones();
    if (%numNodes == -1)
    {
        return ;
    }
    %boneIndex = 0;
    while (%boneIndex < %numNodes)
    {
        bodyModPanel::setModGCControllers(1, %boneIndex, %axis, %val);
        %boneIndex = %boneIndex + 1;
    }
}

function bodyModPanel::setAllBoneOffsets(%axis, %val)
{
    if (!$player.rolesPermissionCheckNoWarn("debugActive"))
    {
        return ;
    }
    %numNodes = $player.getNumBones();
    if (%numNodes == -1)
    {
        return ;
    }
    %boneIndex = 0;
    while (%boneIndex < %numNodes)
    {
        bodyModPanel::setModGCControllers(0, %boneIndex, %axis, %val);
        %boneIndex = %boneIndex + 1;
    }
}

function bodyModPanel::applyChangeFromGC(%gc)
{
    %gcName = strlwr(%gc.getName());
    %isScale = 0;
    if (strstr(%gcName, "scale") != -1)
    {
        %isScale = 1;
    }
    %isSlider = 0;
    if (strstr(%gcName, "slider") != -1)
    {
        %isSlider = 1;
    }
    %value = 0;
    if (%isSlider)
    {
        %value = %gc.getValue();
    }
    else
    {
        %value = %gc.getText();
    }
    if (%isScale)
    {
        %rangeMin = getWord($bodyModPanel::scaleBoneRange, 0);
        %rangeMax = getWord($bodyModPanel::scaleBoneRange, 1);
    }
    else
    {
        %rangeMin = getWord($bodyModPanel::offsetBoneRange, 0);
        %rangeMax = getWord($bodyModPanel::offsetBoneRange, 1);
    }
    if (%value < %rangeMin)
    {
        %value = %rangeMin;
    }
    else
    {
        if (%value > %rangeMax)
        {
            %value = %rangeMax;
        }
    }
    %slider = 0;
    %te = 0;
    if (%isScale)
    {
        %slider = %gc.getParent().getObject(0);
        %te = %gc.getParent().getObject(1);
    }
    else
    {
        %slider = %gc.getParent().getObject(2);
        %te = %gc.getParent().getObject(3);
    }
    if (!isObject(%slider) && !isObject(%te))
    {
        return ;
    }
    %slider.setValue(%value);
    %te.setText(%value);
    bodyModPanel::setBoneWithAntiMod(%isScale, %slider);
    return ;
}
function bodyModPanel::setBoneWithAntiMod(%isScale, %slider)
{
    if (!$player.rolesPermissionCheckNoWarn("debugActive"))
    {
        return ;
    }
    if (!isObject(%slider))
    {
        return ;
    }
    %gcName = strlwr(%slider.getName());
    %axis = "";
    if (strstr(%gcName, "x") != -1)
    {
        %axis = "x";
    }
    else
    {
        if (strstr(%gcName, "y") != -1)
        {
            %axis = "y";
        }
        else
        {
            if (strstr(%gcName, "z") != -1)
            {
                %axis = "z";
            }
        }
    }
    else
    {
        return ;
    }
    %curParentValue = %slider.getValue();
    %boneIndex = %slider.boneIndex;
    ClassBodyMod::setBoneMod(%isScale, %boneIndex, %axis, %curParentValue);
    %prevParentValue = %slider.prevValue;
    %deltaParentValue = %curParentValue - %prevParentValue;
    %slider.prevValue = %curParentValue;
    %demo = %curParentValue;
    if (%demo == 0)
    {
        return ;
    }
    if (%isScale)
    {
        %gcCheckBox = bodyModPanel::getArrayGCName($bodyModPanel::BMBIAntiScaleCheckBox, %boneIndex, 0);
    }
    else
    {
        return ;
    }
    if (!isObject(%gcCheckBox) && !%gcCheckBox.getValue())
    {
        return ;
    }
    %childBones = $player.getChildBones(%boneIndex);
    %numBones = getWordCount(%childBones);
    if (!%numBones)
    {
        return ;
    }
    if (%isScale)
    {
        %rangeMin = getWord($bodyModPanel::scaleBoneRange, 0);
        %rangeMax = getWord($bodyModPanel::scaleBoneRange, 1);
    }
    else
    {
        %rangeMin = getWord($bodyModPanel::offsetBoneRange, 0);
        %rangeMax = getWord($bodyModPanel::offsetBoneRange, 1);
    }
    %i = 0;
    while (%i < %numBones)
    {
        %childBoneIndex = getWord(%childBones, %i);
        if (%axis $= "x")
        {
            if (%isScale)
            {
                %sliderGC = bodyModPanel::getArrayGCName($bodyModPanel::BMXScaleSlider, %childBoneIndex, 1);
                %teGC = bodyModPanel::getArrayGCName($bodyModPanel::BMXScaleTextEdit, %childBoneIndex, 1);
            }
            else
            {
                %sliderGC = bodyModPanel::getArrayGCName($bodyModPanel::BMXOffsetSlider, %childBoneIndex, 1);
                %teGC = bodyModPanel::getArrayGCName($bodyModPanel::BMXOffsetTextEdit, %childBoneIndex, 1);
            }
        }
        else
        {
            if (%axis $= "y")
            {
                if (%isScale)
                {
                    %sliderGC = bodyModPanel::getArrayGCName($bodyModPanel::BMYScaleSlider, %childBoneIndex, 2);
                    %teGC = bodyModPanel::getArrayGCName($bodyModPanel::BMYScaleTextEdit, %childBoneIndex, 2);
                }
                else
                {
                    %sliderGC = bodyModPanel::getArrayGCName($bodyModPanel::BMYOffsetSlider, %childBoneIndex, 2);
                    %teGC = bodyModPanel::getArrayGCName($bodyModPanel::BMYOffsetTextEdit, %childBoneIndex, 2);
                }
            }
            else
            {
                if (%axis $= "z")
                {
                    if (%isScale)
                    {
                        %sliderGC = bodyModPanel::getArrayGCName($bodyModPanel::BMZScaleSlider, %childBoneIndex, 3);
                        %teGC = bodyModPanel::getArrayGCName($bodyModPanel::BMZScaleTextEdit, %childBoneIndex, 3);
                    }
                    else
                    {
                        %sliderGC = bodyModPanel::getArrayGCName($bodyModPanel::BMZOffsetSlider, %childBoneIndex, 3);
                        %teGC = bodyModPanel::getArrayGCName($bodyModPanel::BMZOffsetTextEdit, %childBoneIndex, 3);
                    }
                }
                else
                {
                }
            }
        }
        else
        {
            if (!isObject(%sliderGC) && !isObject(%teGC))
            {
            }
            else
            {
                %prevChildValue = %sliderGC.getValue();
                %numo = (-1 * %prevChildValue) * %deltaParentValue;
                %deltaChildValue = %numo / %demo;
                %newChildValue = %prevChildValue + %deltaChildValue;
                if (%newChildValue < %rangeMin)
                {
                    %newChildValue = %rangeMin;
                }
                else
                {
                    if (%newChildValue > %rangeMax)
                    {
                        %newChildValue = %rangeMax;
                    }
                }
                %sliderGC.prevValue = %sliderGC.getValue();
                %sliderGC.setValue(%newChildValue);
                %teGC.setText(%newChildValue);
                ClassBodyMod::setBoneMod(%isScale, %childBoneIndex, %axis, %sliderGC.getValue());
            }
        }
        %i = %i + 1;
    }
}

function bodyModPanel::setAntiModToChildren(%isScale, %parentBoneIndex, %axis, %val)
{
    if (!$player.rolesPermissionCheckNoWarn("debugActive"))
    {
        return ;
    }
    if (%isScale)
    {
        %gcCheckBox = bodyModPanel::getArrayGCName($bodyModPanel::BMBIAntiScaleCheckBox, %parentBoneIndex, 0);
    }
    else
    {
        return ;
    }
    if (!isObject(%gcCheckBox) && !%gcCheckBox.getValue())
    {
        return ;
    }
    %childBones = $player.getChildBones(%parentBoneIndex);
    %numBones = getWordCount(%childBones);
    if (!%numBones)
    {
        return ;
    }
    %i = 0;
    while (%i < %numBones)
    {
        %childBoneIndex = getWord(%childBones, %i);
        bodyModPanel::setModGCControllers(%isScale, %childBoneIndex, %axis, %val);
        %i = %i + 1;
    }
}


