new SimSet(ModalMessageBoxes);
if (isObject(MissionCleanup)) {
    ModalMessageBoxes.add(MissionCleanup);
}
$gNumModalDialogs = 0;
function MessageCallback(%dlg, %callback) {
    %dlg.schedule(Canvas, 70, "popDialog");
    $gThisDialog = %dlg;
    eval(%callback);
    %dlg.remove(ModalMessageBoxes);
    $gNumModalDialogs = ($gNumModalDialogs - 1.0);
    if (($gNumModalDialogs <= 0.0)) {
        $gNumModalDialogs = 0;
        setActionMapsEnabled(1);
    }
    "delete".schedule(%dlg, 1500);
};
function DestroyMessageBoxes() {
    if (isObject(ModalMessageBoxes)) {
        $gNumModalDialogs = ($gNumModalDialogs - ModalMessageBoxes.getCount());
        ModalMessageBoxes.deleteMembers();
    }
    if (($gNumModalDialogs == 0.0)) {
        setActionMapsEnabled(1);
    }
};
function ShowAllMessageBoxes() {
    %count = ModalMessageBoxes.getCount();
    %i = 0;
    while ((%i < %count)) {
        %mb = %i.getObject(ModalMessageBoxes);
        if (isObject(%mb)) {
            0.pushDialog(Canvas, %mb);
        }
        %i = (%i + 1.0);
    }
};
function MBSetText(%text, %frame, %msg) {
    %ext = %text.getExtent();
    "<just:center>" @ %msg.setText(%text);
    %text.forceReflow();
    %newExtent = %text.getExtent();
    %deltaY = (getWord(%newExtent, 1) - getWord(%ext, 1));
    %windowPos = %frame.getPosition();
    %windowExt = %frame.getExtent();
    (getWord(%windowExt, 1) + %deltaY).resize(%frame, getWord(%windowPos, 0), (getWord(%windowPos, 1) - (%deltaY / 2.0)), getWord(%windowExt, 0));
};
function MessageBoxOK(%title, %message, %callback, %canStopShowing, %key) {
    isDefined("%callback", "");
    isDefined("%canStopShowing", "");
    isDefined("%key", "");
    if (MessageBox_TryDontShow(%title, %message, %callback, %canStopShowing, %key)) {
        return;
    }
    %dialog = MessageBoxOKDlg::newDialog();
    %title.setText(%dialog.window);
    %key.tryAddStopShowing(%dialog, %title, %message, %canStopShowing);
    $gNumModalDialogs = ($gNumModalDialogs + 1.0);
    %dialog.add(ModalMessageBoxes);
    setActionMapsEnabled(0);
    0.pushDialog(Canvas, %dialog);
    %message.setMessageText(%dialog);
    %dialog.callback = %callback @ 0;
    return %dialog;
};
function MessageBoxTextEntry(%title, %message, %callback, %defaultText) {
    %dialog = MessageBoxTextEntryDlg::newDialog();
    %title.setText(%dialog.window);
    %defaultText.setText(%dialog.textEntry);
    1000.setSelection(%dialog.textEntry, 0);
    $gNumModalDialogs = ($gNumModalDialogs + 1.0);
    %dialog.add(ModalMessageBoxes);
    setActionMapsEnabled(0);
    0.pushDialog(Canvas, %dialog);
    %message.setMessageText(%dialog);
    %dialog.callback = %callback;
    %dialog.callback = %dialog @ ".doCallback();" @ 0;
    return %dialog;
};
function MessageBoxTextEntryWithCancel(%title, %message, %callback, %defaultText, %maxLength) {
    %dialog = MessageBoxTextEntryWCancelDlg::newDialog();
    %title.setText(%dialog.window);
    if ((%maxLength != 0.0)) {
    }
    if (!(%maxLength $= "")) {
        %dialog.textEntry.maxLength = %maxLength;
    }
    %defaultText.setText(%dialog.textEntry);
    1000.setSelection(%dialog.textEntry, 0);
    $gNumModalDialogs = ($gNumModalDialogs + 1.0);
    %dialog.add(ModalMessageBoxes);
    setActionMapsEnabled(0);
    0.pushDialog(Canvas, %dialog);
    %message.setMessageText(%dialog);
    %dialog.callback = %callback;
    %dialog.callback = %dialog @ ".doCallback();" @ 0;
    return %dialog;
};
function MessageBoxTextEntryWithBitmapWithCancel(%title, %message, %callback, %defaultText, %maxLength, %bitmapPath) {
    %dialog = MessageBoxTextEntryWithCancel(%title, %message, %callback, %defaultText, %maxLength);
    %container = %dialog.textEntry.getParent();
    %spacing = 10;
    %posX = ((getWord(%dialog.textEntry.position, 0) + getWord(%dialog.textEntry.extent, 0)) + %spacing);
    %posY = (getWord(%dialog.textEntry.position, 1) - 10.0);
    %extX = ((getWord(%container.extent, 0) - %posX) - %spacing);
    %extY = %extX;
    %ctrl = new GuiBitmapCtrl("") {
        position = %posX @ " " @ %posY;
        extent = %extX @ " " @ %extY;
        bitmap = %bitmapPath;
    };
    %ctrl.add(%container);
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
    %title.setText(%dialog.window);
    $gNumModalDialogs = ($gNumModalDialogs + 1.0);
    %dialog.add(ModalMessageBoxes);
    setActionMapsEnabled(0);
    0.pushDialog(Canvas, %dialog);
    %message.setMessageText(%dialog);
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
    %title.setText(%dialog.window);
    %key.tryAddStopShowing(%dialog, %title, %message, %canStopShowing);
    $gNumModalDialogs = ($gNumModalDialogs + 1.0);
    %dialog.add(ModalMessageBoxes);
    setActionMapsEnabled(0);
    0.pushDialog(Canvas, %dialog);
    %message.setMessageText(%dialog);
    %dialog.callback = %yesCallback @ 0;
    %dialog.callback = %noCallback @ 1;
    return %dialog;
};
function MessageBoxYesNoDlg::onSleep(%this) {
};
function MessagePopup(%title, %message, %delay) {
    %dialog = MessagePopupDlg::newDialog();
    %title.setText(%dialog.window);
    $gNumModalDialogs = ($gNumModalDialogs + 1.0);
    %dialog.add(ModalMessageBoxes);
    setActionMapsEnabled(0);
    0.pushDialog(Canvas, %dialog);
    %message.setMessageText(%dialog);
    if (!(%delay $= "")) {
        "close".schedule(%dialog, %delay);
    }
    return %dialog;
};
function MessageBoxCustom(%title, %message, %buttonList) {
    %dialog = MessageBox::newDialog(%buttonList);
    %title.setText(%dialog.window);
    $gNumModalDialogs = ($gNumModalDialogs + 1.0);
    %dialog.add(ModalMessageBoxes);
    setActionMapsEnabled(0);
    0.pushDialog(Canvas, %dialog);
    %message.setMessageText(%dialog);
    return %dialog;
};
function MessageBox::newDialog(%buttonList) {
    %dialog = new GuiControl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = "640 480";
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
    };
    %ctrl = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
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
    %ctrl.add(%dialog);
    %dialog.numButtons = getFieldCount(%buttonList);
    %padding = 20;
    %minButtonWidth = 50;
    %buttonHeight = 23;
    %allButtonsWidth = %padding;
    %i = 0;
    while ((%i < %dialog.numButtons)) {
        %buttonName = getField(%buttonList, %i);
        %buttonWidth = mMax(%minButtonWidth, (%padding + getStrWidth(%buttonName, GuiFocusableVWButtonProfile)));
        %allButtonsWidth = (%allButtonsWidth + (%buttonWidth + %padding));
        %i = (%i + 1.0);
    }
    %windowWidth = mMax(300, %allButtonsWidth);
    %window = new GuiWindowCtrl("") {
        profile = (%i < %dialog.numButtons) @ "GuiMessageWindowProfile";
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
        profile = "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %padding @ " " @ 29;
        extent = (%windowWidth - (2.0 * %padding)) @ " " @ 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        stripGamelink = 1;
        helpTag = 0;
    };
    %xPos = mFloor((0.5 * (%windowWidth - %allButtonsWidth)));
    %ypos = 48;
    %buttonContainer = new GuiControl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = %xPos @ " " @ %ypos;
        extent = %allButtonsWidth @ " " @ %buttonHeight;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %buttonContainer.add(%window);
    %xPos = %padding;
    %ypos = 0;
    %i = 0;
    while ((%i < %dialog.numButtons)) {
        %buttonName = getField(%buttonList, %i);
        %buttonWidth = mMax(%minButtonWidth, (%padding + getStrWidth(%buttonName, GuiFocusableVWButtonProfile)));
        %button = new GuiVariableWidthButtonCtrl("") {
            profile = "GuiFocusableVWButtonProfile";
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
        %xPos = (%xPos + (%buttonWidth + %padding));
        %window.button = %button @ %i;
        %dialog.button = %button @ %i;
        %button.add(%buttonContainer);
        %dialog.callback = "" @ %i;
        %i = (%i + 1.0);
    }
    %text.add(%window);
    %window.add(%dialog);
    %dialog.window = (%i < %dialog.numButtons) @ %window;
    %dialog.text = %text;
    %dialog.doCallbackOnEscape = 1;
    "MessageBox".bindClassName(%dialog);
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
    %deltaX = (%this.targetWidth - getWord(%windowExt, 0));
    if ((%deltaX != 0.0)) {
        getWord(%windowExt, 1).resize(%this.window, (getWord(%windowPos, 0) - (%deltaX / 2.0)), getWord(%windowPos, 1), (getWord(%windowExt, 0) + %deltaX));
    }
    "<just:center>" @ %this.message.setText(%this.text);
    %this.text.forceReflow();
    %newTextExt = %this.text.getExtent();
    %deltaY = (getWord(%newTextExt, 1) - getWord(%startingTextExt, 1));
    %windowPos = %this.window.getPosition();
    %windowExt = %this.window.getExtent();
    if ((%deltaY != 0.0)) {
        (getWord(%windowExt, 1) + %deltaY).resize(%this.window, getWord(%windowPos, 0), (getWord(%windowPos, 1) - (%deltaY / 2.0)), getWord(%windowExt, 0));
    }
};
function MessageBox::close(%this) {
    if (%this.doCallbackOnEscape) {
    }
    %callback = "";
    MessageCallback(%this, %callback);
};
function MessageBox_TryDontShow(%title, %message, %callback, %canStopShowing, %key) {
    if ((%canStopShowing $= "")) {
        return 0;
    }
    safeEnsureScriptObject("StringMap", gMessageBoxDontShow, 0);
    %key = MessageBox_GetKey(%title, %message, %key);
    if (%key.get(gMessageBoxDontShow)) {
        eval(%callback);
        return 1;
    }
    return 0;
};
function MessageBox_SetDontShow(%key, %val) {
    safeEnsureScriptObject("StringMap", gMessageBoxDontShow, 0);
    %val.put(gMessageBoxDontShow, %key);
};
function MessageBox_GetKey(%title, %message, %key) {
    if (!(%key $= "")) {
    }
    %key = stripVeryAgressively(%title @ "\t" @ %message);
    return %key;
};
function MessageBox::tryAddStopShowing(%this, %title, %message, %canStopShowing, %key) {
    if (!(%canStopShowing $= "")) {
        %key = MessageBox_GetKey(%title, %message, %key);
        %ctrl = new GuiCheckBoxCtrl("") {
            profile = ETSLoginSmallCheckBoxProfile;
            position = 9 @ " " @ (getWord(%this.window.getExtent(), 1) - 19.0);
            extent = 50 @ " " @ 15;
            text = "show";
            horizSizing = "right";
            vertSizing = "top";
            command = "MessageBox_SetDontShow(\"" @ %key @ "\", !$ThisControl.getValue());";
        };
        1.setValue(%ctrl);
        %ctrl.add(%this.window);
    }
};
