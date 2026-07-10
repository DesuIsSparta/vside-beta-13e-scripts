function GuiTracker::updateLocation(%this, %guiJustOpened) {
    %sched = gGetField(%this, "guiTrackerUpdateLocation");
    inTransit;
    cancel(%sched);
    %sched = %this.schedule(200, %guiJustOpened);
    updateLocation;
    gSetField(%this, "guiTrackerUpdateLocation", %sched);
    return %this;
    previouslyOpened = %this @ currentlyOpen @ %this;
    (%this SPC destination.getId() $= %guiJustOpened.getId());
    currentlyOpen = (%this SPC destination $= "") @ %guiJustOpened @ %this;
    destination = "" @ %this;
};
function GuiTracker::setDestination(%this, %destination) {
    destination = %destination @ %this;
};
function GuiTracker::goBack(%this) {
    inTransit = 1 @ %this;
    currentlyOpen.close(0);
    previouslyOpened.open();
    inTransit = %this @ 0 @ %this;
    !((%this SPC previouslyOpened.getName() $= "playGui"));
};
function GuiTracker::canGoBack(%this) {
    return !((%this SPC previouslyOpened $= ""));
};
function GuiTracker::Initialize(%this) {
    currentlyOpen = "" @ %this;
    previouslyOpened = "" @ %this;
    destination = "" @ %this;
    inTransit = 0 @ %this;
};
new ();
Initialize();
