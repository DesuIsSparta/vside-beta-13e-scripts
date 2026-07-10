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
    %obj.acceptEmptyString = %acceptEmptyString;
    %obj.controlToGetValueFrom = %controlToGetValueFrom;
    %obj.callbackForUpdates = %callbackForSecondaryVisualUpdates;
    %obj.requestsPendingCount = 0;
    %obj.lastValueSaved = "";
    %obj.initialValueSet = 0;
    %obj.savedBitmap = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "14 14";
        minExtent = "1 1";
        bitmap = "platform/client/ui/checkmark_green";
        visible = 0;
    };
    %obj.savingBitmap = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "14 14";
        minExtent = "1 1";
        bitmap = "platform/client/ui/ellipsis_yellow";
        visible = 0;
    };
    if ((%arrowDescription $= "right")) {
        %arrowBitmap = "platform/client/ui/arrow_red_right";
    }
    if ((%arrowDescription $= "downAndRight")) {
        %arrowBitmap = "platform/client/ui/arrow_red_downAndRight";
    }
    %arrowBitmap = "platform/client/ui/arrow_red_right";
    %obj.changedBitmap = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "14 14";
        minExtent = "1 1";
        bitmap = %arrowBitmap;
        visible = 0;
    };
    %obj.savedBitmap.add(%obj);
    %obj.savingBitmap.add(%obj);
    %obj.changedBitmap.add(%obj);
    return %obj;
};
function SavableTextStatusIndicator::setInitialValue(%this, %initialValue) {
    if (%this.initialValueSet) {
        warn(getScopeName() @ " " @ "- initial value already set -" @ " " @ getTrace());
    }
    %this.lastValueSaved = %initialValue;
    %this.initialValueSet = 1;
};
function SavableTextStatusIndicator::incrementRequestCount(%this) {
    %newValue = %this.controlToGetValueFrom.getValue();
    if (!(%this.acceptEmptyString)) {
    }
    if ((%newValue $= "")) {
        return;
    }
    %this.lastValueSaved = %newValue;
    %this.initialValueSet = 1;
    %this.requestsPendingCount = (%this.requestsPendingCount + 1.0);
    1.update(%this);
};
function SavableTextStatusIndicator::decrementRequestCount(%this) {
    %this.requestsPendingCount = (%this.requestsPendingCount - 1.0);
    1.update(%this);
};
function SavableTextStatusIndicator::update(%this, %doCallback) {
    %valueSaved = (%this.requestsPendingCount == 0.0);
    %valueChanged = !(%this.lastValueSaved $= %this.controlToGetValueFrom.getValue());
    if (%valueSaved) {
    }
    !(%valueChanged).setVisible(%this.savedBitmap);
    if (!(%valueSaved)) {
    }
    !(%valueChanged).setVisible(%this.savingBitmap);
    %valueChanged.setVisible(%this.changedBitmap);
    if (%doCallback) {
    }
    if (!(%this.callbackForUpdates $= "")) {
        eval(%this.callbackForUpdates);
    }
};
function SavableTextStatusIndicator::reset(%this) {
    %this.requestsPendingCount = 0;
    %this.lastValueSaved = "";
};
