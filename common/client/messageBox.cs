new SimSet(ModalMessageBoxes);
if (isObject(MissionCleanup)) {
    MissionCleanup.add(ModalMessageBoxes);
}
$gNumModalDialogs = 0;
function MessageCallback(%dlg, %callback) {
    Canvas.schedule(70, "popDialog", %dlg);
    $gThisDialog = %dlg;
    eval(%callback);
    ModalMessageBoxes.remove(%dlg);
    $gNumModalDialogs = (1.0 - $gNumModalDialogs);
    if ((0.0 <= $gNumModalDialogs)) {
        $gNumModalDialogs = 0;
        setActionMapsEnabled(1);
    }
    %dlg.schedule(1500, "delete");
};
function DestroyMessageBoxes() {
    if (isObject(ModalMessageBoxes)) {
        $gNumModalDialogs = (ModalMessageBoxes.getCount() - $gNumModalDialogs);
        ModalMessageBoxes.deleteMembers();
    }
    if ((0.0 == $gNumModalDialogs)) {
        setActionMapsEnabled(1);
    }
};
function ShowAllMessageBoxes() {
    %count = ModalMessageBoxes.getCount();
    %i = 0;
    if ((%count < %i)) {
        %mb = ModalMessageBoxes.getObject(%i);
        if (isObject(%mb)) {
            Canvas.pushDialog(%mb, 0);
        }
        %i = (1.0 + %i);
    }
};
function MBSetText(%text, %frame, %msg) {
    %ext = %text.getExtent();
    %text.setText("<just:center>" @ %msg);
    %text.forceReflow();
    %newExtent = %text.getExtent();
    %deltaY = (getWord(%ext, 1) - getWord(%newExtent, 1));
    %windowPos = %frame.getPosition();
    %windowExt = %frame.getExtent();
    %frame.resize(getWord(%windowPos, 0), ((2.0 / %deltaY) - getWord(%windowPos, 1)), getWord(%windowExt, 0), (%deltaY + getWord(%windowExt, 1)));
};
function MessageBoxOK(%title, %message, %callback, %canStopShowing, %key) {
    isDefined("%callback", "");
    isDefined("%canStopShowing", "");
    isDefined("%key", "");
    if (MessageBox_TryDontShow(%title, %message, %callback, %canStopShowing, %key)) {
        return;
    }
    %dialog = MessageBoxOKDlg::newDialog();
    %dialog.window.setText(%title);
    %dialog.tryAddStopShowing(%title, %message, %canStopShowing, %key);
    $gNumModalDialogs = (1.0 + $gNumModalDialogs);
    ModalMessageBoxes.add(%dialog);
    setActionMapsEnabled(0);
    Canvas.pushDialog(%dialog, 0);
    %dialog.setMessageText(%message);
    %dialog.callback = %callback @ 0;
    return %dialog;
};
function MessageBoxTextEntry(%title, %message, %callback, %defaultText) {
    %dialog = MessageBoxTextEntryDlg::newDialog();
    %dialog.window.setText(%title);
    %dialog.textEntry.setText(%defaultText);
    %dialog.textEntry.setSelection(0, 1000);
    $gNumModalDialogs = (1.0 + $gNumModalDialogs);
    ModalMessageBoxes.add(%dialog);
    setActionMapsEnabled(0);
    Canvas.pushDialog(%dialog, 0);
    %dialog.setMessageText(%message);
    %dialog.callback = %callback;
    %dialog.callback = %dialog @ ".doCallback();" @ 0;
    return %dialog;
};
function MessageBoxTextEntryWithCancel(%title, %message, %callback, %defaultText, %maxLength) {
    %dialog = MessageBoxTextEntryWCancelDlg::newDialog();
    %dialog.window.setText(%title);
    if ((0.0 != %maxLength)) {
    }
    if (!(%maxLength $= "")) {
        %dialog.textEntry.maxLength = %maxLength;
    }
    %dialog.textEntry.setText(%defaultText);
    %dialog.textEntry.setSelection(0, 1000);
    $gNumModalDialogs = (1.0 + $gNumModalDialogs);
    ModalMessageBoxes.add(%dialog);
    setActionMapsEnabled(0);
    Canvas.pushDialog(%dialog, 0);
    %dialog.setMessageText(%message);
    %dialog.callback = %callback;
    %dialog.callback = %dialog @ ".doCallback();" @ 0;
    return %dialog;
};
function MessageBoxTextEntryWithBitmapWithCancel(%title, %message, %callback, %defaultText, %maxLength, %bitmapPath) {
    %dialog = MessageBoxTextEntryWithCancel(%title, %message, %callback, %defaultText, %maxLength);
    %container = %dialog.textEntry.getParent();
    %spacing = 10;
    %posX = (%spacing + (getWord(%dialog.textEntry.extent, 0) + getWord(%dialog.textEntry.position, 0)));
    %posY = (10.0 - getWord(%dialog.textEntry.position, 1));
    %extX = (%spacing - (%posX - getWord(%container.extent, 0)));
    %extY = %extX;
    %ctrl = new GuiBitmapCtrl("") {
        position = 0 @ %posX @ " " @ %posY;
        extent = %extX @ " " @ %extY;
        bitmap = %bitmapPath;
    };
    %container.add(%ctrl);
    %dialog.bitmapCtrl = %ctrl;
    return %dialog;
};
function MessageBoxOKDlg::onSleep(%this) {
};
function MessageBoxOkCancel(%title, %message, %callback, %cancelCallback, %canStopShowing) {
    isDefined("%callback", "");
    isDefined("%cancelCallback", "");
    isDefined("%canStopShowing", "");
    %dialog = MessageBoxOKCancelDlg::newDialog();
    %dialog.window.setText(%title);
    $gNumModalDialogs = (1.0 + $gNumModalDialogs);
    ModalMessageBoxes.add(%dialog);
    setActionMapsEnabled(0);
    Canvas.pushDialog(%dialog, 0);
    %dialog.setMessageText(%message);
    %dialog.callback = %callback @ 0;
    %dialog.callback = %cancelCallback @ 1;
    return %dialog;
};
function MessageBoxOKCancelDlg::onSleep(%this) {
};
function MessageBoxYesNo(%title, %message, %yesCallback, %noCallback, %canStopShowing, %key) {
    isDefined("%yesCallback", "");
    isDefined("%noCallback", "");
    isDefined("%canStopShowing", "");
    isDefined("%key", "");
    if (MessageBox_TryDontShow(%title, %message, %yesCallback, %canStopShowing, %key)) {
        return;
    }
    %dialog = MessageBoxYesNoDlg::newDialog();
    %dialog.window.setText(%title);
    %dialog.tryAddStopShowing(%title, %message, %canStopShowing, %key);
    $gNumModalDialogs = (1.0 + $gNumModalDialogs);
    ModalMessageBoxes.add(%dialog);
    setActionMapsEnabled(0);
    Canvas.pushDialog(%dialog, 0);
    %dialog.setMessageText(%message);
    %dialog.callback = %yesCallback @ 0;
    %dialog.callback = %noCallback @ 1;
    return %dialog;
};
function MessageBoxYesNoDlg::onSleep(%this) {
};
function MessagePopup(%title, %message, %delay) {
    %dialog = MessagePopupDlg::newDialog();
    %dialog.window.setText(%title);
    $gNumModalDialogs = (1.0 + $gNumModalDialogs);
    ModalMessageBoxes.add(%dialog);
    setActionMapsEnabled(0);
    Canvas.pushDialog(%dialog, 0);
    %dialog.setMessageText(%message);
    if (!(%delay $= "")) {
        %dialog.schedule(%delay, "close");
    }
    return %dialog;
};
function MessageBoxCustom(%title, %message, %buttonList) {
    %dialog = MessageBox::newDialog(%buttonList);
    %dialog.window.setText(%title);
    $gNumModalDialogs = (1.0 + $gNumModalDialogs);
    ModalMessageBoxes.add(%dialog);
    setActionMapsEnabled(0);
    Canvas.pushDialog(%dialog, 0);
    %dialog.setMessageText(%message);
    return %dialog;
};
function MessageBox::newDialog(%buttonList) {
    %dialog = new GuiControl("") {
        profile = 0 @ "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = "640 480";
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
    };
    %ctrl = new GuiBitmapCtrl("") {
        profile = 0 @ "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = "640 480";
        minExtent = "1 1";
        bitmap = "platform/client/ui/finelines";
        wrap = 1;
        modulationColor = "255 255 255  90";
    };
    %dialog.backgroundCtrl = %ctrl;
    %dialog.add(%ctrl);
    %dialog.numButtons = getFieldCount(%buttonList);
    %padding = 20;
    %minButtonWidth = 50;
    %buttonHeight = 23;
    %allButtonsWidth = %padding;
    %i = 0;
    if ((%dialog.numButtons < %i)) {
        %buttonName = getField(%buttonList, %i);
        %buttonWidth = mMax(%minButtonWidth, GuiFocusableVWButtonProfile, (getStrWidth(%buttonName) + %padding));
        %allButtonsWidth = ((%padding + %buttonWidth) + %allButtonsWidth);
        %i = (1.0 + %i);
    }
    %windowWidth = mMax(300, %allButtonsWidth);
    (%dialog.numButtons < %i);
    %window = new GuiWindowCtrl("") {
        profile = 0 @ "GuiMessageWindowProfile";
        horizSizing = "center";
        vertSizing = "center";
        position = "170 175";
        extent = %windowWidth @ " " @ 75;
        minExtent = "48 92";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        resizeWidth = 0;
        resizeHeight = 0;
        canMove = 1;
        canClose = 0;
        canMinimize = 0;
        canMaximize = 0;
        MinSize = "50 50";
        helpTag = 0;
    };
    %dialog.targetWidth = %windowWidth;
    %text = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %padding @ " " @ 29;
        extent = ((%padding * 2.0) - %windowWidth) @ " " @ 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        stripGamelink = 1;
        helpTag = 0;
    };
    %xPos = mFloor(((%allButtonsWidth - %windowWidth) * 0.5));
    %ypos = 48;
    %buttonContainer = new GuiControl("") {
        profile = 0 @ "GuiDefaultProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = %xPos @ " " @ %ypos;
        extent = %allButtonsWidth @ " " @ %buttonHeight;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %window.add(%buttonContainer);
    %xPos = %padding;
    %ypos = 0;
    %i = 0;
    if ((%dialog.numButtons < %i)) {
        %buttonName = getField(%buttonList, %i);
        %buttonWidth = mMax(%minButtonWidth, GuiFocusableVWButtonProfile, (getStrWidth(%buttonName) + %padding));
        %button = new GuiVariableWidthButtonCtrl("") {
            profile = 0 @ "GuiFocusableVWButtonProfile";
            horizSizing = "right";
            vertSizing = "top";
            position = %xPos @ " " @ %ypos;
            extent = %buttonWidth @ " " @ %buttonHeight;
            minExtent = "8 8";
            sluggishness = -1;
            visible = 1;
            command = "MessageCallback(" @ %dialog @ "," @ %dialog @ ".callback[" @ %i @ "]);";
            accelerator = "return";
            text = %buttonName;
            groupNum = -1;
            buttonType = "PushButton";
            helpTag = 0;
            simpleStyle = 0;
        };
        %xPos = ((%padding + %buttonWidth) + %xPos);
        %window.button = %button @ %i;
        %dialog.button = %button @ %i;
        %buttonContainer.add(%button);
        %dialog.callback = "" @ %i;
        %i = (1.0 + %i);
    }
    %window.add(%text);
    %dialog.add(%window);
    %dialog.window = (%dialog.numButtons < %i) @ %window;
    %dialog.text = %text;
    %dialog.doCallbackOnEscape = 1;
    %dialog.bindClassName("MessageBox");
    return %dialog;
};
function MessageBox::setMessageText(%this, %msg) {
    %msg = standardSubstitutions(%msg);
    %msg = "<linkcolorhl:ffffff>" @ %msg;
    %this.message = %msg;
    %this.reflow();
};
function MessageBox::setWindowWidth(%this, %newWidth) {
    if ((%newWidth $= "")) {
        return;
    }
    %this.targetWidth = %newWidth;
    %this.reflow();
};
function MessageBox::reflow(%this) {
    %startingTextExt = %this.text.getExtent();
    %this.targetWidth = mMax(%this.targetWidth, getWord(%this.window.minExtent, 0));
    %windowPos = %this.window.getPosition();
    %windowExt = %this.window.getExtent();
    %deltaX = (getWord(%windowExt, 0) - %this.targetWidth);
    if ((0.0 != %deltaX)) {
        %this.window.resize(((2.0 / %deltaX) - getWord(%windowPos, 0)), getWord(%windowPos, 1), (%deltaX + getWord(%windowExt, 0)), getWord(%windowExt, 1));
    }
    %this.text.setText("<just:center>" @ %this.message);
    %this.text.forceReflow();
    %newTextExt = %this.text.getExtent();
    %deltaY = (getWord(%startingTextExt, 1) - getWord(%newTextExt, 1));
    %windowPos = %this.window.getPosition();
    %windowExt = %this.window.getExtent();
    if ((0.0 != %deltaY)) {
        %this.window.resize(getWord(%windowPos, 0), ((2.0 / %deltaY) - getWord(%windowPos, 1)), getWord(%windowExt, 0), (%deltaY + getWord(%windowExt, 1)));
    }
};
function MessageBox::close(%this) {
    if (%this.doCallbackOnEscape) {
    }
    %callback = "";
    %this.callback;
    MessageCallback(%this, %callback);
};
function MessageBox_TryDontShow(%title, %message, %callback, %canStopShowing, %key) {
    if ((%canStopShowing $= "")) {
        return 0;
    }
    safeEnsureScriptObject("StringMap", gMessageBoxDontShow, 0);
    %key = MessageBox_GetKey(%title, %message, %key);
    if (gMessageBoxDontShow.get(%key)) {
        eval(%callback);
        return 1;
    }
    return 0;
};
function MessageBox_SetDontShow(%key, %val) {
    safeEnsureScriptObject("StringMap", gMessageBoxDontShow, 0);
    gMessageBoxDontShow.put(%key, %val);
};
function MessageBox_GetKey(%title, %message, %key) {
    if (!(%key $= "")) {
    }
    %key = stripVeryAgressively(%title @ "\t" @ %message);
    %key;
    return %key;
};
function MessageBox::tryAddStopShowing(%this, %title, %message, %canStopShowing, %key) {
    if (!(%canStopShowing $= "")) {
        %key = MessageBox_GetKey(%title, %message, %key);
        %ctrl = new GuiCheckBoxCtrl("") {
            profile = 0 @ ETSLoginSmallCheckBoxProfile;
            position = 9 @ " " @ (19.0 - getWord(%this.window.getExtent(), 1));
            extent = 50 @ " " @ 15;
            text = "show";
            horizSizing = "right";
            vertSizing = "top";
            command = "MessageBox_SetDontShow(\"" @ %key @ "\", !$ThisControl.getValue());";
        };
        %ctrl.setValue(1);
        %this.window.add(%ctrl);
    }
};
