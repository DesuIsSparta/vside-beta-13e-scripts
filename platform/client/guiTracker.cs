function GuiTracker::updateLocation(%this, %guiJustOpened)
{
    if (%this.inTransit)
    {
        %sched = gGetField(%this, "guiTrackerUpdateLocation");
        cancel(%sched);
        %sched = %this.schedule(200, updateLocation, %guiJustOpened);
        gSetField(%this, "guiTrackerUpdateLocation", %sched);
        return ;
    }
    if ((%this.destination $= "") && (%this.destination.getId() $= %guiJustOpened.getId()))
    {
        %this.previouslyOpened = %this.currentlyOpen;
        %this.currentlyOpen = %guiJustOpened;
        %this.destination = "";
    }
    return ;
}
function GuiTracker::setDestination(%this, %destination)
{
    %this.destination = %destination;
    return ;
}
function GuiTracker::goBack(%this)
{
    %this.inTransit = 1;
    if (!((%this.currentlyOpen $= "")) && !((%this.currentlyOpen.getName() $= "playGui")))
    {
        %this.currentlyOpen.close(0);
    }
    if (!((%this.previouslyOpened $= "")) && !((%this.previouslyOpened.getName() $= "playGui")))
    {
        %this.previouslyOpened.open();
    }
    %this.inTransit = 0;
    return ;
}
function GuiTracker::canGoBack(%this)
{
    return !(%this.previouslyOpened $= "");
}
function GuiTracker::Initialize(%this)
{
    %this.currentlyOpen = "";
    %this.previouslyOpened = "";
    %this.destination = "";
    %this.inTransit = 0;
    return ;
}
if (!isObject(GuiTracker))
{
    new GuiControl(GuiTracker);
    GuiTracker.Initialize();
}
