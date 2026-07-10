new SimSet(ModalMessageBoxes);
if (isObject(MissionCleanup))
{
    MissionCleanup.add(ModalMessageBoxes);
}
$gNumModalDialogs = 0;
function MessageCallback(%dlg, %callback)
{
    Canvas.schedule(70, "popDialog", %dlg);
    $gThisDialog = %dlg;
    eval(%callback);
    ModalMessageBoxes.remove(%dlg);
    $gNumModalDialogs = $gNumModalDialogs - 1;
    if ($gNumModalDialogs <= 0)
    {
        $gNumModalDialogs = 0;
        setActionMapsEnabled(1);
    }
    %dlg.schedule(1500, "delete");
}
function DestroyMessageBoxes()
{
    if (isObject(ModalMessageBoxes))
    {
        $gNumModalDialogs = $gNumModalDialogs - ModalMessageBoxes.getCount();
        ModalMessageBoxes.deleteMembers();
    }
    if ($gNumModalDialogs == 0)
    {
        setActionMapsEnabled(1);
    }
}
function ShowAllMessageBoxes()
{
    %count = ModalMessageBoxes.getCount();
    %i = 0;
    while (%i < %count)
    {
        %mb = ModalMessageBoxes.getObject(%i);
        if (isObject(%mb))
        {
            Canvas.pushDialog(%mb, 0);
        }
        %i = %i + 1;
    }
}
function MBSetText(%text, %frame, %msg)
{
    %ext = %text.getExtent();
    %text.setText("<just:center>" @ %msg);
    %text.forceReflow();
    %newExtent = %text.getExtent();
    %deltaY = getWord(%newExtent, 1) - getWord(%ext, 1);
    %windowPos = %frame.getPosition();
    %windowExt = %frame.getExtent();
    %frame.resize(getWord(%windowPos, 0), (getWord(%windowPos, 1) - (%deltaY / 2)), getWord(%windowExt, 0), (getWord(%windowExt, 1) + %deltaY));
}
function MessageBoxOK(%title, %message, %callback, %canStopShowing, %key)
{
    isDefined("%callback", "");
    isDefined("%canStopShowing", "");
    isDefined("%key", "");
    if (MessageBox_TryDontShow(%title, %message, %callback, %canStopShowing, %key))
    {
        return;
    }
    %dialog = MessageBoxOKDlg::newDialog();
    %dialog.window.setText(%title);
    %dialog.tryAddStopShowing(%title, %message, %canStopShowing, %key);
    $gNumModalDialogs = $gNumModalDialogs + 1;
    ModalMessageBoxes.add(%dialog);
    setActionMapsEnabled(0);
    Canvas.pushDialog(%dialog, 0);
    %dialog.setMessageText(%message);
    %dialog.callback[0] = %callback;
    return %dialog;
}
function MessageBoxTextEntry(%title, %message, %callback, %defaultText)
{
    %dialog = MessageBoxTextEntryDlg::newDialog();
    %dialog.window.setText(%title);
    %dialog.textEntry.setText(%defaultText);
    %dialog.textEntry.setSelection(0, 1000);
    $gNumModalDialogs = $gNumModalDialogs + 1;
    ModalMessageBoxes.add(%dialog);
    setActionMapsEnabled(0);
    Canvas.pushDialog(%dialog, 0);
    %dialog.setMessageText(%message);
    %dialog.callback = %callback;
    %dialog.callback[".doCallback();",0] = %dialog;
    return %dialog;
}
function MessageBoxTextEntryWithCancel(%title, %message, %callback, %defaultText, %maxLength)
{
    %dialog = MessageBoxTextEntryWCancelDlg::newDialog();
    %dialog.window.setText(%title);
    if ((%maxLength != 0) && !(%maxLength $= ""))
    {
        %dialog.textEntry.maxLength = %maxLength;
    }
    %dialog.textEntry.setText(%defaultText);
    %dialog.textEntry.setSelection(0, 1000);
    $gNumModalDialogs = $gNumModalDialogs + 1;
    ModalMessageBoxes.add(%dialog);
    setActionMapsEnabled(0);
    Canvas.pushDialog(%dialog, 0);
    %dialog.setMessageText(%message);
    %dialog.callback = %callback;
    %dialog.callback[".doCallback();",0] = %dialog;
    return %dialog;
}
function MessageBoxTextEntryWithBitmapWithCancel(%title, %message, %callback, %defaultText, %maxLength, %bitmapPath)
{
    %dialog = MessageBoxTextEntryWithCancel(%title, %message, %callback, %defaultText, %maxLength);
    %container = %dialog.textEntry.getParent();
    %spacing = 10;
    %posX = (getWord(%dialog.textEntry.position, 0) + getWord(%dialog.textEntry.extent, 0)) + %spacing;
    %posY = getWord(%dialog.textEntry.position, 1) - 10;
    %extX = (getWord(%container.extent, 0) - %posX) - %spacing;
    %extY = %extX;
    %ctrl = new GuiBitmapCtrl("") {
        position = %posX @ " " @ %posY;
        extent = %extX @ " " @ %extY;
        bitmap = %bitmapPath;
    };
    %container.add(%ctrl);
    %dialog.bitmapCtrl = %ctrl;
    return %dialog;
}
function MessageBoxOKDlg::onSleep(%this)
{
}
function MessageBoxOkCancel(%title, %message, %callback, %cancelCallback, %canStopShowing)
{
    isDefined("%callback", "");
    isDefined("%cancelCallback", "");
    isDefined("%canStopShowing", "");
    %dialog = MessageBoxOKCancelDlg::newDialog();
    %dialog.window.setText(%title);
    $gNumModalDialogs = $gNumModalDialogs + 1;
    ModalMessageBoxes.add(%dialog);
    setActionMapsEnabled(0);
    Canvas.pushDialog(%dialog, 0);
    %dialog.setMessageText(%message);
    %dialog.callback[0] = %callback;
    %dialog.callback[1] = %cancelCallback;
    return %dialog;
}
function MessageBoxOKCancelDlg::onSleep(%this)
{
}
function MessageBoxYesNo(%title, %message, %yesCallback, %noCallback, %canStopShowing, %key)
{
    isDefined("%yesCallback", "");
    isDefined("%noCallback", "");
    isDefined("%canStopShowing", "");
    isDefined("%key", "");
    if (MessageBox_TryDontShow(%title, %message, %yesCallback, %canStopShowing, %key))
    {
        return;
    }
    %dialog = MessageBoxYesNoDlg::newDialog();
    %dialog.window.setText(%title);
    %dialog.tryAddStopShowing(%title, %message, %canStopShowing, %key);
    $gNumModalDialogs = $gNumModalDialogs + 1;
    ModalMessageBoxes.add(%dialog);
    setActionMapsEnabled(0);
    Canvas.pushDialog(%dialog, 0);
    %dialog.setMessageText(%message);
    %dialog.callback[0] = %yesCallback;
    %dialog.callback[1] = %noCallback;
    return %dialog;
}
function MessageBoxYesNoDlg::onSleep(%this)
{
}
function MessagePopup(%title, %message, %delay)
{
    %dialog = MessagePopupDlg::newDialog();
    %dialog.window.setText(%title);
    $gNumModalDialogs = $gNumModalDialogs + 1;
    ModalMessageBoxes.add(%dialog);
    setActionMapsEnabled(0);
    Canvas.pushDialog(%dialog, 0);
    %dialog.setMessageText(%message);
    if (!(%delay $= ""))
    {
        %dialog.schedule(%delay, "close");
    }
    return %dialog;
}
function MessageBoxCustom(%title, %message, %buttonList)
{
    %dialog = MessageBox::newDialog(%buttonList);
    %dialog.window.setText(%title);
    $gNumModalDialogs = $gNumModalDialogs + 1;
    ModalMessageBoxes.add(%dialog);
    setActionMapsEnabled(0);
    Canvas.pushDialog(%dialog, 0);
    %dialog.setMessageText(%message);
    return %dialog;
}
function MessageBox::newDialog(%buttonList)
{
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
    %dialog.add(%ctrl);
    %dialog.numButtons = getFieldCount(%buttonList);
    %padding = 20;
    %minButtonWidth = 50;
    %buttonHeight = 23;
    %allButtonsWidth = %padding;
    %i = 0;
    while (%i < %dialog.numButtons)
    {
        %buttonName = getField(%buttonList, %i);
        %buttonWidth = mMax(%minButtonWidth, (%padding + getStrWidth(%buttonName, GuiFocusableVWButtonProfile)));
        %allButtonsWidth = %allButtonsWidth + (%buttonWidth + %padding);
        %i = %i + 1;
    }
    %windowWidth = mMax(300, %allButtonsWidth);
    %i < %dialog.numButtons;
    %window = new GuiWindowCtrl("") {
        profile = "GuiMessageWindowProfile";
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
        extent = (%windowWidth - (2 * %padding)) @ " " @ 14;
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
    %window.add(%buttonContainer);
    %xPos = %padding;
    %ypos = 0;
    %i = 0;
    while (%i < %dialog.numButtons)
    {
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
        %xPos = %xPos + (%buttonWidth + %padding);
        %window.button[%i] = %button;
        %dialog.button[%i] = %button;
        %buttonContainer.add(%button);
        %dialog.callback[%i] = "";
        %i = %i + 1;
    }
    %window.add(%text);
    %dialog.add(%window);
    %dialog.window = (%i < %dialog.numButtons) @ %window;
    %dialog.text = %text;
    %dialog.doCallbackOnEscape = 1;
    %dialog.bindClassName("MessageBox");
    return %dialog;
}
function MessageBox::setMessageText(%this, %msg)
{
    %msg = standardSubstitutions(%msg);
    %msg = "<linkcolorhl:ffffff>" @ %msg;
    %this.message = %msg;
    %this.reflow();
}
function MessageBox::setWindowWidth(%this, %newWidth)
{
    if (%newWidth $= "")
    {
        return;
    }
    %this.targetWidth = %newWidth;
    %this.reflow();
}
function MessageBox::reflow(%this)
{
    %startingTextExt = %this.text.getExtent();
    %this.targetWidth = mMax(%this.targetWidth, getWord(%this.window.minExtent, 0));
    %windowPos = %this.window.getPosition();
    %windowExt = %this.window.getExtent();
    %deltaX = %this.targetWidth - getWord(%windowExt, 0);
    if (%deltaX != 0)
    {
        %this.window.resize((getWord(%windowPos, 0) - (%deltaX / 2)), getWord(%windowPos, 1), (getWord(%windowExt, 0) + %deltaX), getWord(%windowExt, 1));
    }
    %this.text.setText("<just:center>" @ %this.message);
    %this.text.forceReflow();
    %newTextExt = %this.text.getExtent();
    %deltaY = getWord(%newTextExt, 1) - getWord(%startingTextExt, 1);
    %windowPos = %this.window.getPosition();
    %windowExt = %this.window.getExtent();
    if (%deltaY != 0)
    {
        %this.window.resize(getWord(%windowPos, 0), (getWord(%windowPos, 1) - (%deltaY / 2)), getWord(%windowExt, 0), (getWord(%windowExt, 1) + %deltaY));
    }
}
function MessageBox::close(%this)
{
    if (%this.doCallbackOnEscape)
    {
    }
    else
    {
    }
    %callback = "";
    %this.callback[(%this.numButtons - 1)];
    MessageCallback(%this, %callback);
}
function MessageBox_TryDontShow(%title, %message, %callback, %canStopShowing, %key)
{
    if (%canStopShowing $= "")
    {
        return 0;
    }
    safeEnsureScriptObject("StringMap", gMessageBoxDontShow, 0);
    %key = MessageBox_GetKey(%title, %message, %key);
    if (gMessageBoxDontShow.get(%key))
    {
        eval(%callback);
        return 1;
    }
    return 0;
}
function MessageBox_SetDontShow(%key, %val)
{
    safeEnsureScriptObject("StringMap", gMessageBoxDontShow, 0);
    gMessageBoxDontShow.put(%key, %val);
}
function MessageBox_GetKey(%title, %message, %key)
{
    if (!(%key $= ""))
    {
    }
    else
    {
    }
    %key = stripVeryAgressively(%title @ "\t" @ %message);
    %key;
    return %key;
}
function MessageBox::tryAddStopShowing(%this, %title, %message, %canStopShowing, %key)
{
    if (!(%canStopShowing $= ""))
    {
        %key = MessageBox_GetKey(%title, %message, %key);
        %ctrl = new GuiCheckBoxCtrl("") {
            profile = ETSLoginSmallCheckBoxProfile;
            position = 9 @ " " @ (getWord(%this.window.getExtent(), 1) - 19);
            extent = 50 @ " " @ 15;
            text = "show";
            horizSizing = "right";
            vertSizing = "top";
            command = "MessageBox_SetDontShow(\"" @ %key @ "\", !$ThisControl.getValue());";
        };
        %ctrl.setValue(1);
        %this.window.add(%ctrl);
    }
}
