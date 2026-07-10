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
function CURLObject::onRecvData(%this, %data) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " just received body data");
    log("communication", "debug", %data);
    if (isObject(callBackSink)) {
        callBackSink.onRecvData(%this, %data);
    }
};
function CURLObject::onDone(%this) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " is done!");
    %this.remove();
    %this.schedule(0);
    if (isObject(callBackSink)) {
        callBackSink.onDone(%this);
    }
};
function CURLObject::onError(%this, %errorNum, %errorName) {
    log("communication", "error", getScopeName() @ " " @ "- " @ %this.getName() @ " received error \"" @ %errorName @ "\"!");
    if (!(noDeleteOnError)) {
        %this.remove();
        %this.schedule(0);
    }
    if (isObject(callBackSink)) {
        callBackSink.onError(%this, %errorNum, %errorName);
    }
};
function CURLObject::onHeaderData(%this, %data) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " just received header data");
    log("communication", "debug", %data);
};
function CURLObject::onVerbose(%this, %data) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " just received verbose data");
    log("communication", "debug", %data);
};
function CURLObject::onProgress(%this, %dltotal, %dlnow, %ultotal, %ulnow) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " just received progress data");
    log("communication", "debug", "downloaded: " @ %dlnow @ "/" @ %dltotal);
    if ((0.0 != %ulnow)) {
    }
    if ((0.0 != %ultotal)) {
        log("communication", "debug", "uploaded: " @ %ulnow @ "/" @ %ultotal);
    }
    if (isObject(callBackSink)) {
        callBackSink.onProgress(%this, %dltotal, %dlnow, %ultotal, %ulnow);
    }
};
function CurlClassNameTest::onRecvData(%this, %data) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " just received body data with classname CurlClassNameTest");
    log("communication", "debug", %data);
};
function CurlDownloadClassName::onDone(%this) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " is done in CurlDownloadClassName!");
    %this.remove();
    %this.schedule(0);
    log("communication", "debug", delete @ getScopeName() @ " " @ "- Currently, " @ CURLSimGroup @ getCount() @ " CURLObjects remaining in the system.");
};
function PostTestClass::onDone(%this) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " is done!");
    %this.remove();
    %this.schedule(0);
    log("communication", "debug", delete @ getScopeName() @ " " @ "- Currently, " @ CURLSimGroup @ getCount() @ " CURLObjects remaining in the system.");
};
function PostTestClass::onError(%this, %errorNum, %errorName) {
    log("communication", "error", getScopeName() @ " " @ "- " @ %this.getName() @ " received error \"" @ %errorName @ "\"!");
    %this.remove();
    %this.schedule(0);
    log("communication", "debug", delete @ getScopeName() @ " " @ "- Currently, " @ CURLSimGroup @ getCount() @ " CURLObjects remaining in the system.");
};
function ScreenShotUploaderClass::onProgress(%this, %dltotal, %dlnow, %ultotal, %ulnow) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " just received progress data");
    log("communication", "debug", "downloaded: " @ %dlnow @ "/" @ %dltotal);
    log("communication", "debug", "uploaded: " @ %ulnow @ "/" @ %ultotal);
    dlTotal = %dltotal @ %this;
    dlNow = %dlnow @ %this;
    ulTotal = %ultotal @ %this;
    ulNow = %ulnow @ %this;
    if (isObject(callBackSink)) {
        callBackSink.onProgress(%this);
    }
};
function ScreenShotUploaderClass::onDone(%this) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " is done!");
    %this.remove();
    %this.schedule(0);
    log("communication", "debug", delete @ getScopeName() @ " " @ "- Currently, " @ CURLSimGroup @ getCount() @ " CURLObjects remaining in the system.");
    if (isObject(callBackSink)) {
        callBackSink.onDone(%this);
    }
};
function ScreenShotUploaderClass::onError(%this, %errorNum, %errorName) {
    log("communication", "error", getScopeName() @ " " @ "- " @ %this.getName() @ " received error \"" @ %errorName @ "\"!");
    %this.remove();
    %this.schedule(0);
    log("communication", "debug", delete @ getScopeName() @ " " @ "- Currently, " @ CURLSimGroup @ getCount() @ " CURLObjects remaining in the system.");
    if (isObject(callBackSink)) {
        callBackSink.onError(%this);
    }
};
