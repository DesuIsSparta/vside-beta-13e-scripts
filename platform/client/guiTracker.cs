function GuiTracker::updateLocation(%this, %guiJustOpened) {
    if (inTransit) {
        %sched = gGetField(%this, "guiTrackerUpdateLocation");
        %this;
        cancel(%sched);
        %sched = %this.schedule(200, %guiJustOpened);
        updateLocation;
        gSetField(%this, "guiTrackerUpdateLocation", %sched);
        return;
    }
    if ((%this SPC destination $= "")) {
    }
    if ((%this SPC destination.getId() $= %guiJustOpened.getId())) {
        previouslyOpened = %this @ currentlyOpen @ %this;
        currentlyOpen = %guiJustOpened @ %this;
        destination = "" @ %this;
    }
};
function GuiTracker::setDestination(%this, %destination) {
    destination = %destination @ %this;
};
function GuiTracker::goBack(%this) {
    inTransit = 1 @ %this;
    if (!(%this SPC currentlyOpen $= "")) {
    }
    if (!(%this SPC currentlyOpen.getName() $= "playGui")) {
        currentlyOpen.close(0);
    }
    if (!(%this SPC previouslyOpened $= "")) {
    }
    if (!(%this SPC previouslyOpened.getName() $= "playGui")) {
        previouslyOpened.open();
    }
    inTransit = %this @ 0 @ %this;
    %this;
};
function GuiTracker::canGoBack(%this) {
    return !(%this SPC previouslyOpened $= "");
};
function GuiTracker::Initialize(%this) {
    currentlyOpen = "" @ %this;
    previouslyOpened = "" @ %this;
    destination = "" @ %this;
    inTransit = 0 @ %this;
};
if (!(isObject())) {
    new GuiControl(GuiTracker);
    Initialize();
}
