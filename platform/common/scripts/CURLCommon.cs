$gCURLGlobalDelayMS = 0;
function CURLObject::onDonePreDelay(%this)
{
    %totalDelayMS = %this.delayMS + $gCURLGlobalDelayMS;
    if (%totalDelayMS <= 0)
    {
        %this.onDone();
    }
    else
    {
        log("network", "warn", getDebugString(%this) SPC "- delaying call to onDone()  by" SPC %totalDelayMS @ "ms. URL =" SPC %this.getURL());
        %this.schedule(%totalDelayMS, "onDonePostDelay", %totalDelayMS);
    }
    return ;
}
function CURLObject::onErrorPreDelay(%this, %val, %name)
{
    %totalDelayMS = %this.delayMS + $gCURLGlobalDelayMS;
    if (%totalDelayMS <= 0)
    {
        %this.onError(%val, %name);
    }
    else
    {
        log("network", "warn", getDebugString(%this) SPC "- delaying call to onError() by" SPC %totalDelayMS @ "ms. URL =" SPC %this.getURL());
        %this.schedule(%totalDelayMS, "onErrorPostDelay", %totalDelayMS, %val, %name);
    }
    return ;
}
function CURLObject::onDonePostDelay(%this, %totalDelayMS)
{
    log("network", "warn", getDebugString(%this) SPC "- now executing call to onDone()  after delay of" SPC %totalDelayMS @ "ms. URL =" SPC %this.getURL());
    %this.onDone();
    return ;
}
function CURLObject::onErrorPostDelay(%this, %totalDelayMS, %val, %name)
{
    log("network", "warn", getDebugString(%this) SPC "- now executing call to onError() after delay of" SPC %totalDelayMS @ "ms. URL =" SPC %this.getURL());
    %this.onError(%val, %name);
    return ;
}
