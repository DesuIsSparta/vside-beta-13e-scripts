new SimSet(ModalMessageBoxes);
if (isObject()) {
    add();
}
$gNumModalDialogs = 0;
ModalMessageBoxes;
function MessageCallback(%dlg, %callback) {
    70.schedule("popDialog", %dlg);
    $gThisDialog = %dlg;
    Canvas;
    eval(%callback);
    %dlg.remove();
    $gNumModalDialogs = (1.0 - $gNumModalDialogs);
    ModalMessageBoxes;
    if ((0.0 <= $gNumModalDialogs)) {
        $gNumModalDialogs = 0;
        MissionCleanup;
        setActionMapsEnabled(1);
    }
    %dlg.schedule(1500, "delete");
};
function DestroyMessageBoxes() {
    if (isObject()) {
        $gNumModalDialogs = (getCount() - $gNumModalDialogs);
        ModalMessageBoxes;
        deleteMembers();
    }
    if ((0.0 == $gNumModalDialogs)) {
        setActionMapsEnabled(1);
    }
};
function ShowAllMessageBoxes() {
    %count = getCount();
    ModalMessageBoxes;
    %i = 0;
    if ((%count < %i)) {
        %mb = %i.getObject();
        ModalMessageBoxes;
        if (isObject(%mb)) {
            %mb.pushDialog(0);
        }
        %i = (1.0 + %i);
        Canvas;
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
    window.setText(%title);
    %dialog.tryAddStopShowing(%title, %message, %canStopShowing, %key);
    $gNumModalDialogs = (1.0 + $gNumModalDialogs);
    %dialog;
    %dialog.add();
    setActionMapsEnabled(0);
    %dialog.pushDialog(0);
    %dialog.setMessageText(%message);
    callback = ModalMessageBoxes @ Canvas @ %callback @ 0 @ %dialog;
    return %dialog;
};
function MessageBoxTextEntry(%title, %message, %callback, %defaultText) {
    %dialog = MessageBoxTextEntryDlg::newDialog();
    window.setText(%title);
    textEntry.setText(%defaultText);
    textEntry.setSelection(0, 1000);
    $gNumModalDialogs = (1.0 + $gNumModalDialogs);
    %dialog;
    %dialog.add();
    setActionMapsEnabled(0);
    %dialog.pushDialog(0);
    %dialog.setMessageText(%message);
    callback = Canvas @ %callback @ %dialog;
    ModalMessageBoxes;
    callback = %dialog @ %dialog @ %dialog @ ".doCallback();" @ 0 @ %dialog;
    return %dialog;
};
function MessageBoxTextEntryWithCancel(%title, %message, %callback, %defaultText, %maxLength) {
    %dialog = MessageBoxTextEntryWCancelDlg::newDialog();
    window.setText(%title);
    if ((0.0 != %maxLength)) {
    }
    if (!(%dialog SPC %maxLength $= "")) {
        maxLength = %dialog @ textEntry;
        %maxLength;
    }
    textEntry.setText(%defaultText);
    textEntry.setSelection(0, 1000);
    $gNumModalDialogs = (1.0 + $gNumModalDialogs);
    %dialog;
    %dialog.add();
    setActionMapsEnabled(0);
    %dialog.pushDialog(0);
    %dialog.setMessageText(%message);
    callback = Canvas @ %callback @ %dialog;
    ModalMessageBoxes;
    callback = %dialog @ %dialog @ ".doCallback();" @ 0 @ %dialog;
    return %dialog;
};
function MessageBoxTextEntryWithBitmapWithCancel(%title, %message, %callback, %defaultText, %maxLength, %bitmapPath) {
    %dialog = MessageBoxTextEntryWithCancel(%title, %message, %callback, %defaultText, %maxLength);
    %container = textEntry.getParent();
    %dialog;
    %spacing = 10;
    %posX = (%dialog + (textEntry + getWord(position, 0)));
    getWord(extent, 0);
    %posY = (textEntry - getWord(position, 1));
    %dialog;
    %extX = (%posX - (%container - getWord(extent, 0)));
    %spacing;
    %extY = %extX;
    10.0;
    position = GuiBitmapCtrl @ new ""() @ %posX @ " " @ %posY;
    0;
    extent = %dialog @ textEntry @ %extX @ " " @ %extY;
    %spacing;
    bitmap = %bitmapPath;
    %ctrl = ;
    %container.add(%ctrl);
    bitmapCtrl = %ctrl @ %dialog;
    return %dialog;
};
function MessageBoxOKDlg::onSleep(%this) {
};
function MessageBoxOkCancel(%title, %message, %callback, %cancelCallback, %canStopShowing) {
    isDefined("%callback", "");
    isDefined("%cancelCallback", "");
    isDefined("%canStopShowing", "");
    %dialog = MessageBoxOKCancelDlg::newDialog();
    window.setText(%title);
    $gNumModalDialogs = (1.0 + $gNumModalDialogs);
    %dialog;
    %dialog.add();
    setActionMapsEnabled(0);
    %dialog.pushDialog(0);
    %dialog.setMessageText(%message);
    callback = ModalMessageBoxes @ Canvas @ %callback @ 0 @ %dialog;
    callback = %cancelCallback @ 1 @ %dialog;
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
    window.setText(%title);
    %dialog.tryAddStopShowing(%title, %message, %canStopShowing, %key);
    $gNumModalDialogs = (1.0 + $gNumModalDialogs);
    %dialog;
    %dialog.add();
    setActionMapsEnabled(0);
    %dialog.pushDialog(0);
    %dialog.setMessageText(%message);
    callback = ModalMessageBoxes @ Canvas @ %yesCallback @ 0 @ %dialog;
    callback = %noCallback @ 1 @ %dialog;
    return %dialog;
};
function MessageBoxYesNoDlg::onSleep(%this) {
};
function MessagePopup(%title, %message, %delay) {
    %dialog = MessagePopupDlg::newDialog();
    window.setText(%title);
    $gNumModalDialogs = (1.0 + $gNumModalDialogs);
    %dialog;
    %dialog.add();
    setActionMapsEnabled(0);
    %dialog.pushDialog(0);
    %dialog.setMessageText(%message);
    if (!(Canvas SPC %delay $= "")) {
        %dialog.schedule(%delay, "close");
    }
    return %dialog;
};
function MessageBoxCustom(%title, %message, %buttonList) {
    %dialog = MessageBox::newDialog(%buttonList);
    window.setText(%title);
    $gNumModalDialogs = (1.0 + $gNumModalDialogs);
    %dialog;
    %dialog.add();
    setActionMapsEnabled(0);
    %dialog.pushDialog(0);
    %dialog.setMessageText(%message);
    return %dialog;
};
function MessageBox::newDialog(%buttonList) {
    profile = GuiControl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "width";
    vertSizing = "height";
    position = "0 0";
    extent = "640 480";
    minExtent = "8 8";
    sluggishness = -1;
    visible = 1;
    %dialog = ;
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "width";
    vertSizing = "height";
    position = "0 0";
    extent = "640 480";
    minExtent = "1 1";
    bitmap = "platform/client/ui/finelines";
    wrap = 1;
    modulationColor = "255 255 255  90";
    %ctrl = ;
    backgroundCtrl = %ctrl @ %dialog;
    %dialog.add(%ctrl);
    numButtons = getFieldCount(%buttonList) @ %dialog;
    %padding = 20;
    %minButtonWidth = 50;
    %buttonHeight = 23;
    %allButtonsWidth = %padding;
    %i = 0;
    if ((numButtons < %i)) {
        %buttonName = getField(%buttonList, %i);
        %dialog;
        %buttonWidth = mMax(%minButtonWidth, (getStrWidth(%buttonName) + %padding));
        GuiFocusableVWButtonProfile;
        %allButtonsWidth = ((%padding + %buttonWidth) + %allButtonsWidth);
        %i = (1.0 + %i);
    }
    %windowWidth = mMax(300, %allButtonsWidth);
    (numButtons < %i);
    profile = GuiWindowCtrl @ new ""() @ "GuiMessageWindowProfile";
    0;
    horizSizing = %dialog @ "center";
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
    %window = ;
    targetWidth = %windowWidth @ %dialog;
    profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
    0;
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
    %text = ;
    %xPos = mFloor(((%allButtonsWidth - %windowWidth) * 0.5));
    %ypos = 48;
    profile = GuiControl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "center";
    vertSizing = "top";
    position = %xPos @ " " @ %ypos;
    extent = %allButtonsWidth @ " " @ %buttonHeight;
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    %buttonContainer = ;
    %window.add(%buttonContainer);
    %xPos = %padding;
    %ypos = 0;
    %i = 0;
    if ((numButtons < %i)) {
        %buttonName = getField(%buttonList, %i);
        %dialog;
        %buttonWidth = mMax(%minButtonWidth, (getStrWidth(%buttonName) + %padding));
        GuiFocusableVWButtonProfile;
        profile = GuiVariableWidthButtonCtrl @ new ""() @ "GuiFocusableVWButtonProfile";
        0;
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
        %button = ;
        %xPos = ((%padding + %buttonWidth) + %xPos);
        button = %button @ %i @ %window;
        button = %button @ %i @ %dialog;
        %buttonContainer.add(%button);
        callback = "" @ %i @ %dialog;
        %i = (1.0 + %i);
    }
    %window.add(%text);
    %dialog.add(%window);
    window = (numButtons < %i) @ %window @ %dialog;
    %dialog;
    text = %text @ %dialog;
    doCallbackOnEscape = 1 @ %dialog;
    %dialog.bindClassName("MessageBox");
    return %dialog;
};
function MessageBox::setMessageText(%this, %msg) {
    %msg = standardSubstitutions(%msg);
    %msg = "<linkcolorhl:ffffff>" @ %msg;
    message = %msg @ %this;
    %this.reflow();
};
function MessageBox::setWindowWidth(%this, %newWidth) {
    if ((%newWidth $= "")) {
        return;
    }
    targetWidth = %newWidth @ %this;
    %this.reflow();
};
function MessageBox::reflow(%this) {
    %startingTextExt = text.getExtent();
    %this;
    targetWidth = window @ mMax(targetWidth, getWord(minExtent, 0)) @ %this;
    %this;
    %windowPos = window.getPosition();
    %this;
    %windowExt = window.getExtent();
    %this;
    %deltaX = (%this - targetWidth);
    getWord(%windowExt, 0);
    if ((0.0 != %deltaX)) {
        window.resize(((2.0 / %deltaX) - getWord(%windowPos, 0)), getWord(%windowPos, 1), (%deltaX + getWord(%windowExt, 0)), getWord(%windowExt, 1));
    }
    text.setText(%this @ message);
    text.forceReflow();
    %newTextExt = text.getExtent();
    %this;
    %deltaY = (getWord(%startingTextExt, 1) - getWord(%newTextExt, 1));
    %this;
    %windowPos = window.getPosition();
    %this;
    %windowExt = window.getExtent();
    %this;
    if ((0.0 != %deltaY)) {
        window.resize(getWord(%windowPos, 0), ((2.0 / %deltaY) - getWord(%windowPos, 1)), getWord(%windowExt, 0), (%deltaY + getWord(%windowExt, 1)));
    }
};
function MessageBox::close(%this) {
    if (doCallbackOnEscape) {
    }
    %callback = "";
    callback;
    MessageCallback(%this, %callback);
};
function MessageBox_TryDontShow(%title, %message, %callback, %canStopShowing, %key) {
    if ((%canStopShowing $= "")) {
        return 0;
    }
    safeEnsureScriptObject("StringMap", 0);
    %key = MessageBox_GetKey(%title, %message, %key);
    gMessageBoxDontShow;
    if (%key.get()) {
        eval(%callback);
        return 1;
    }
    return 0;
};
function MessageBox_SetDontShow(%key, %val) {
    safeEnsureScriptObject("StringMap", 0);
    %key.put(%val);
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
        profile = new ""() @ ETSLoginSmallCheckBoxProfile;
        GuiCheckBoxCtrl;
        position = 0 @ 9 @ " " @ 19.0 @ (%this - getWord(window.getExtent(), 1));
        extent = 50 @ " " @ 15;
        text = "show";
        horizSizing = "right";
        vertSizing = "top";
        command = "MessageBox_SetDontShow(\"" @ %key @ "\", !$ThisControl.getValue());";
        %ctrl = ;
        %ctrl.setValue(1);
        window.add(%ctrl);
    }
};
