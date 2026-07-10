$gCURLGlobalDelayMS = 0;
function CURLObject::onDonePreDelay(%this) {
    %totalDelayMS = (%this.delayMS + $gCURLGlobalDelayMS);
    if ((%totalDelayMS <= 0.0)) {
        %this.onDone();
    }
    log("network", "warn", getDebugString(%this) @ " " @ "- delaying call to onDone()  by" @ " " @ %totalDelayMS @ "ms. URL =" @ " " @ %this.getURL());
    %totalDelayMS.schedule(%this, %totalDelayMS, "onDonePostDelay");
};
function CURLObject::onErrorPreDelay(%this, %val, %name) {
    %totalDelayMS = (%this.delayMS + $gCURLGlobalDelayMS);
    if ((%totalDelayMS <= 0.0)) {
        %name.onError(%this, %val);
    }
    log("network", "warn", getDebugString(%this) @ " " @ "- delaying call to onError() by" @ " " @ %totalDelayMS @ "ms. URL =" @ " " @ %this.getURL());
    %name.schedule(%this, %totalDelayMS, "onErrorPostDelay", %totalDelayMS, %val);
};
function CURLObject::onDonePostDelay(%this, %totalDelayMS) {
    log("network", "warn", getDebugString(%this) @ " " @ "- now executing call to onDone()  after delay of" @ " " @ %totalDelayMS @ "ms. URL =" @ " " @ %this.getURL());
    %this.onDone();
};
function CURLObject::onErrorPostDelay(%this, %totalDelayMS, %val, %name) {
    log("network", "warn", getDebugString(%this) @ " " @ "- now executing call to onError() after delay of" @ " " @ %totalDelayMS @ "ms. URL =" @ " " @ %this.getURL());
    %name.onError(%this, %val);
};
