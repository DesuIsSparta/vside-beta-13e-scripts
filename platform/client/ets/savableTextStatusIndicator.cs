function SavableTextStatusIndicatorCreator::make(%indicatorName, %position, %controlToGetValueFrom, %callbackForSecondaryVisualUpdates, %acceptEmptyString, %arrowDescription) {
    if (isObject(%indicatorName)) {
        error(getScopeName() @ " " @ "- object with name '" @ %indicatorName @ "' already exists");
        return 0;
    }
    if (!(isObject(%controlToGetValueFrom))) {
        error(getScopeName() @ " " @ "- requires object parameter - '" @ %controlToGetValueFrom @ "' is not an object");
        return 0;
    }
    %obj = safeEnsureScriptObjectWithClassBindingsAndInit("GuiControl", %indicatorName, "SavableTextStatusIndicator", "{      profile      = \"GuiDefaultProfile\";" @ " " @ "horizSizing  = \"right\";" @ " " @ "vertSizing   = \"bottom\";" @ " " @ "position     = \"" @ %position @ "\";" @ " " @ "extent       = \"14 14\";" @ " " @ "minExtent    = \"14 14\";" @ " " @ "visible      = true; }");
    acceptEmptyString = %acceptEmptyString @ %obj;
    controlToGetValueFrom = %controlToGetValueFrom @ %obj;
    callbackForUpdates = %callbackForSecondaryVisualUpdates @ %obj;
    requestsPendingCount = 0 @ %obj;
    lastValueSaved = "" @ %obj;
    initialValueSet = 0 @ %obj;
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = "14 14";
    minExtent = "1 1";
    bitmap = "platform/client/ui/checkmark_green";
    visible = 0;
    savedBitmap = %obj;
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = "14 14";
    minExtent = "1 1";
    bitmap = "platform/client/ui/ellipsis_yellow";
    visible = 0;
    savingBitmap = %obj;
    if ((%arrowDescription $= "right")) {
        %arrowBitmap = "platform/client/ui/arrow_red_right";
    }
    if ((%arrowDescription $= "downAndRight")) {
        %arrowBitmap = "platform/client/ui/arrow_red_downAndRight";
    }
    %arrowBitmap = "platform/client/ui/arrow_red_right";
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = "14 14";
    minExtent = "1 1";
    bitmap = %arrowBitmap;
    visible = 0;
    changedBitmap = %obj;
    %obj.add(savedBitmap);
    %obj.add(savingBitmap);
    %obj.add(changedBitmap);
    return %obj;
};
function SavableTextStatusIndicator::setInitialValue(%this, %initialValue) {
    if (initialValueSet) {
        warn(getScopeName() @ " " @ "- initial value already set -" @ " " @ getTrace());
    }
    lastValueSaved = %this @ %initialValue @ %this;
    initialValueSet = 1 @ %this;
};
function SavableTextStatusIndicator::incrementRequestCount(%this) {
    %newValue = controlToGetValueFrom.getValue();
    %this;
    if (!(acceptEmptyString)) {
    }
    if ((%this SPC %newValue $= "")) {
        return;
    }
    lastValueSaved = %newValue @ %this;
    initialValueSet = 1 @ %this;
    requestsPendingCount = (%this + requestsPendingCount);
    1.0;
    %this.update(1);
};
function SavableTextStatusIndicator::decrementRequestCount(%this) {
    requestsPendingCount = (%this - requestsPendingCount);
    1.0;
    %this.update(1);
};
function SavableTextStatusIndicator::update(%this, %doCallback) {
    %valueSaved = (%this == requestsPendingCount);
    0.0;
    %valueChanged = !(%this $= controlToGetValueFrom.getValue());
    %this SPC lastValueSaved;
    if (%valueSaved) {
    }
    savedBitmap.setVisible(!(%valueChanged));
    if (!(%valueSaved)) {
    }
    savingBitmap.setVisible(!(%valueChanged));
    changedBitmap.setVisible(%valueChanged);
    if (%doCallback) {
    }
    if (!(%this SPC callbackForUpdates $= "")) {
        eval(callbackForUpdates);
    }
};
function SavableTextStatusIndicator::reset(%this) {
    requestsPendingCount = 0 @ %this;
    lastValueSaved = "" @ %this;
};
