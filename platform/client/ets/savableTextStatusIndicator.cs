function SavableTextStatusIndicatorCreator::make(%indicatorName, %position, %controlToGetValueFrom, %callbackForSecondaryVisualUpdates, %acceptEmptyString, %arrowDescription)
{
    if (isObject(%indicatorName))
    {
        error(getScopeName() SPC "- object with name \'" @ %indicatorName @ "\' already exists");
        return 0;
    }
    if (!isObject(%controlToGetValueFrom))
    {
        error(getScopeName() SPC "- requires object parameter - \'" @ %controlToGetValueFrom @ "\' is not an object");
        return 0;
    }
    %obj = safeEnsureScriptObjectWithClassBindingsAndInit("GuiControl", %indicatorName, "SavableTextStatusIndicator", "{      profile      = \"GuiDefaultProfile\";" SPC "horizSizing  = \"right\";" SPC "vertSizing   = \"bottom\";" SPC "position     = \"" @ %position @ "\";" SPC "extent       = \"14 14\";" SPC "minExtent    = \"14 14\";" SPC "visible      = true; }");
    %obj.acceptEmptyString = %acceptEmptyString;
    %obj.controlToGetValueFrom = %controlToGetValueFrom;
    %obj.callbackForUpdates = %callbackForSecondaryVisualUpdates;
    %obj.requestsPendingCount = 0;
    %obj.lastValueSaved = "";
    %obj.initialValueSet = 0;
    %obj.savedBitmap = new GuiBitmapCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "14 14";
        minExtent = "1 1";
        bitmap = "platform/client/ui/checkmark_green";
        visible = 0;
    };
    %obj.savingBitmap = new GuiBitmapCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "14 14";
        minExtent = "1 1";
        bitmap = "platform/client/ui/ellipsis_yellow";
        visible = 0;
    };
    if (%arrowDescription $= "right")
    {
        %arrowBitmap = "platform/client/ui/arrow_red_right";
    }
    else
    {
        if (%arrowDescription $= "downAndRight")
        {
            %arrowBitmap = "platform/client/ui/arrow_red_downAndRight";
        }
        else
        {
            %arrowBitmap = "platform/client/ui/arrow_red_right";
        }
    }
    %obj.changedBitmap = new GuiBitmapCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "14 14";
        minExtent = "1 1";
        bitmap = %arrowBitmap;
        visible = 0;
    };
    %obj.add(%obj.savedBitmap);
    %obj.add(%obj.savingBitmap);
    %obj.add(%obj.changedBitmap);
    return %obj;
}
function SavableTextStatusIndicator::setInitialValue(%this, %initialValue)
{
    if (%this.initialValueSet)
    {
        warn(getScopeName() SPC "- initial value already set -" SPC getTrace());
    }
    else
    {
        %this.lastValueSaved = %initialValue;
        %this.initialValueSet = 1;
    }
    return ;
}
function SavableTextStatusIndicator::incrementRequestCount(%this)
{
    %newValue = %this.controlToGetValueFrom.getValue();
    if (!(%this.acceptEmptyString) && (%newValue $= ""))
    {
        return ;
    }
    %this.lastValueSaved = %newValue;
    %this.initialValueSet = 1;
    %this.requestsPendingCount = %this.requestsPendingCount + 1;
    %this.update(1);
    return ;
}
function SavableTextStatusIndicator::decrementRequestCount(%this)
{
    %this.requestsPendingCount = %this.requestsPendingCount - 1;
    %this.update(1);
    return ;
}
function SavableTextStatusIndicator::update(%this, %doCallback)
{
    %valueSaved = %this.requestsPendingCount == 0;
    %valueChanged = !(%this.lastValueSaved $= %this.controlToGetValueFrom.getValue());
    %this.savedBitmap.setVisible(%valueSaved && !%valueChanged);
    %this.savingBitmap.setVisible(!%valueSaved && !%valueChanged);
    %this.changedBitmap.setVisible(%valueChanged);
    if (%doCallback && !((%this.callbackForUpdates $= "")))
    {
        eval(%this.callbackForUpdates);
    }
    return ;
}
function SavableTextStatusIndicator::reset(%this)
{
    %this.requestsPendingCount = 0;
    %this.lastValueSaved = "";
    return ;
}
