new SimGroup(CURLSimGroup);
$HTTP::StatusOK = 200;
$HTTP::StatusNotFound = 404;
$HTTP::StatusServerError = 500;
$CURL::MalformedURL = 3;
$CURL::CouldNotResolveProxy = 5;
$CURL::CouldNotResolveHost = 6;
$CURL::CouldNotConnect = 7;
$CURL::WriteError = 23;
$CURL::ReadError = 26;
$CURL::OperationTimedOut = 28;
$CURL::SendError = 55;
$CURL::RecvError = 56;
function CURLObject::onRecvData(%this, %data)
{
    log("communication", "debug", getScopeName() SPC "- " @ %this.getName() @ " just received body data");
    log("communication", "debug", %data);
    if (isObject(%this.callBackSink))
    {
        %this.callBackSink.onRecvData(%this, %data);
    }
    return ;
}
function CURLObject::onDone(%this)
{
    log("communication", "debug", getScopeName() SPC "- " @ %this.getName() @ " is done!");
    CURLSimGroup.remove(%this);
    %this.schedule(0, delete);
    if (isObject(%this.callBackSink))
    {
        %this.callBackSink.onDone(%this);
    }
    return ;
}
function CURLObject::onError(%this, %errorNum, %errorName)
{
    log("communication", "error", getScopeName() SPC "- " @ %this.getName() @ " received error \"" @ %errorName @ "\"!");
    if (!%this.noDeleteOnError)
    {
        CURLSimGroup.remove(%this);
        %this.schedule(0, delete);
    }
    if (isObject(%this.callBackSink))
    {
        %this.callBackSink.onError(%this, %errorNum, %errorName);
    }
    return ;
}
function CURLObject::onHeaderData(%this, %data)
{
    log("communication", "debug", getScopeName() SPC "- " @ %this.getName() @ " just received header data");
    log("communication", "debug", %data);
    return ;
}
function CURLObject::onVerbose(%this, %data)
{
    log("communication", "debug", getScopeName() SPC "- " @ %this.getName() @ " just received verbose data");
    log("communication", "debug", %data);
    return ;
}
function CURLObject::onProgress(%this, %dltotal, %dlnow, %ultotal, %ulnow)
{
    log("communication", "debug", getScopeName() SPC "- " @ %this.getName() @ " just received progress data");
    log("communication", "debug", "downloaded: " @ %dlnow @ "/" @ %dltotal);
    if ((%ulnow != 0) && (%ultotal != 0))
    {
        log("communication", "debug", "uploaded: " @ %ulnow @ "/" @ %ultotal);
    }
    if (isObject(%this.callBackSink))
    {
        %this.callBackSink.onProgress(%this, %dltotal, %dlnow, %ultotal, %ulnow);
    }
    return ;
}
function CurlClassNameTest::onRecvData(%this, %data)
{
    log("communication", "debug", getScopeName() SPC "- " @ %this.getName() @ " just received body data with classname CurlClassNameTest");
    log("communication", "debug", %data);
    return ;
}
function CurlDownloadClassName::onDone(%this)
{
    log("communication", "debug", getScopeName() SPC "- " @ %this.getName() @ " is done in CurlDownloadClassName!");
    CURLSimGroup.remove(%this);
    %this.schedule(0, delete);
    log("communication", "debug", getScopeName() SPC "- Currently, " @ CURLSimGroup.getCount() @ " CURLObjects remaining in the system.");
    return ;
}
function PostTestClass::onDone(%this)
{
    log("communication", "debug", getScopeName() SPC "- " @ %this.getName() @ " is done!");
    CURLSimGroup.remove(%this);
    %this.schedule(0, delete);
    log("communication", "debug", getScopeName() SPC "- Currently, " @ CURLSimGroup.getCount() @ " CURLObjects remaining in the system.");
    return ;
}
function PostTestClass::onError(%this, %errorNum, %errorName)
{
    log("communication", "error", getScopeName() SPC "- " @ %this.getName() @ " received error \"" @ %errorName @ "\"!");
    CURLSimGroup.remove(%this);
    %this.schedule(0, delete);
    log("communication", "debug", getScopeName() SPC "- Currently, " @ CURLSimGroup.getCount() @ " CURLObjects remaining in the system.");
    return ;
}
function ScreenShotUploaderClass::onProgress(%this, %dltotal, %dlnow, %ultotal, %ulnow)
{
    log("communication", "debug", getScopeName() SPC "- " @ %this.getName() @ " just received progress data");
    log("communication", "debug", "downloaded: " @ %dlnow @ "/" @ %dltotal);
    log("communication", "debug", "uploaded: " @ %ulnow @ "/" @ %ultotal);
    %this.dlTotal = %dltotal;
    %this.dlNow = %dlnow;
    %this.ulTotal = %ultotal;
    %this.ulNow = %ulnow;
    if (isObject(%this.callBackSink))
    {
        %this.callBackSink.onProgress(%this);
    }
    return ;
}
function ScreenShotUploaderClass::onDone(%this)
{
    log("communication", "debug", getScopeName() SPC "- " @ %this.getName() @ " is done!");
    CURLSimGroup.remove(%this);
    %this.schedule(0, delete);
    log("communication", "debug", getScopeName() SPC "- Currently, " @ CURLSimGroup.getCount() @ " CURLObjects remaining in the system.");
    if (isObject(%this.callBackSink))
    {
        %this.callBackSink.onDone(%this);
    }
    return ;
}
function ScreenShotUploaderClass::onError(%this, %errorNum, %errorName)
{
    log("communication", "error", getScopeName() SPC "- " @ %this.getName() @ " received error \"" @ %errorName @ "\"!");
    CURLSimGroup.remove(%this);
    %this.schedule(0, delete);
    log("communication", "debug", getScopeName() SPC "- Currently, " @ CURLSimGroup.getCount() @ " CURLObjects remaining in the system.");
    if (isObject(%this.callBackSink))
    {
        %this.callBackSink.onError(%this);
    }
    return ;
}
