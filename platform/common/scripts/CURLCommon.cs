$gCURLGlobalDelayMS = 0;
function CURLObject::onDonePreDelay(%this) {
    %totalDelayMS = ($gCURLGlobalDelayMS + %this.delayMS);
    if ((0.0 <= %totalDelayMS)) {
        %this.onDone();
    }
    log("network", "warn", getDebugString(%this) @ " " @ "- delaying call to onDone()  by" @ " " @ %totalDelayMS @ "ms. URL =" @ " " @ %this.getURL());
    %this.schedule(%totalDelayMS, "onDonePostDelay", %totalDelayMS);
};
function CURLObject::onErrorPreDelay(%this, %val, %name) {
    %totalDelayMS = ($gCURLGlobalDelayMS + %this.delayMS);
    if ((0.0 <= %totalDelayMS)) {
        %this.onError(%val, %name);
    }
    log("network", "warn", getDebugString(%this) @ " " @ "- delaying call to onError() by" @ " " @ %totalDelayMS @ "ms. URL =" @ " " @ %this.getURL());
    %this.schedule(%totalDelayMS, "onErrorPostDelay", %totalDelayMS, %val, %name);
};
function CURLObject::onDonePostDelay(%this, %totalDelayMS) {
    log("network", "warn", getDebugString(%this) @ " " @ "- now executing call to onDone()  after delay of" @ " " @ %totalDelayMS @ "ms. URL =" @ " " @ %this.getURL());
    %this.onDone();
};
function CURLObject::onErrorPostDelay(%this, %totalDelayMS, %val, %name) {
    log("network", "warn", getDebugString(%this) @ " " @ "- now executing call to onError() after delay of" @ " " @ %totalDelayMS @ "ms. URL =" @ " " @ %this.getURL());
    %this.onError(%val, %name);
};
