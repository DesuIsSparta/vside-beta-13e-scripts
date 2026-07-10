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
    if (isObject(%this.callBackSink)) {
        %data.onRecvData(%this.callBackSink, %this);
    }
};
function CURLObject::onDone(%this) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " is done!");
    %this.remove(CURLSimGroup);
    0.schedule(%this);
    if (isObject(%this.callBackSink)) {
        %this.onDone(%this.callBackSink);
    }
};
function CURLObject::onError(%this, %errorNum, %errorName) {
    log("communication", "error", getScopeName() @ " " @ "- " @ %this.getName() @ " received error \"" @ %errorName @ "\"!");
    if (!(%this.noDeleteOnError)) {
        %this.remove(CURLSimGroup);
        0.schedule(%this);
    }
    if (isObject(%this.callBackSink)) {
        %errorName.onError(%this.callBackSink, %this, %errorNum);
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
    if ((%ulnow != 0.0)) {
    }
    if ((%ultotal != 0.0)) {
        log("communication", "debug", "uploaded: " @ %ulnow @ "/" @ %ultotal);
    }
    if (isObject(%this.callBackSink)) {
        %ulnow.onProgress(%this.callBackSink, %this, %dltotal, %dlnow, %ultotal);
    }
};
function CurlClassNameTest::onRecvData(%this, %data) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " just received body data with classname CurlClassNameTest");
    log("communication", "debug", %data);
};
function CurlDownloadClassName::onDone(%this) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " is done in CurlDownloadClassName!");
    %this.remove(CURLSimGroup);
    0.schedule(%this);
    log("communication", "debug", getScopeName() @ " " @ "- Currently, " @ CURLSimGroup.getCount() @ " CURLObjects remaining in the system.");
};
function PostTestClass::onDone(%this) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " is done!");
    %this.remove(CURLSimGroup);
    0.schedule(%this);
    log("communication", "debug", getScopeName() @ " " @ "- Currently, " @ CURLSimGroup.getCount() @ " CURLObjects remaining in the system.");
};
function PostTestClass::onError(%this, %errorNum, %errorName) {
    log("communication", "error", getScopeName() @ " " @ "- " @ %this.getName() @ " received error \"" @ %errorName @ "\"!");
    %this.remove(CURLSimGroup);
    0.schedule(%this);
    log("communication", "debug", getScopeName() @ " " @ "- Currently, " @ CURLSimGroup.getCount() @ " CURLObjects remaining in the system.");
};
function ScreenShotUploaderClass::onProgress(%this, %dltotal, %dlnow, %ultotal, %ulnow) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " just received progress data");
    log("communication", "debug", "downloaded: " @ %dlnow @ "/" @ %dltotal);
    log("communication", "debug", "uploaded: " @ %ulnow @ "/" @ %ultotal);
    %this.dlTotal = %dltotal;
    %this.dlNow = %dlnow;
    %this.ulTotal = %ultotal;
    %this.ulNow = %ulnow;
    if (isObject(%this.callBackSink)) {
        %this.onProgress(%this.callBackSink);
    }
};
function ScreenShotUploaderClass::onDone(%this) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " is done!");
    %this.remove(CURLSimGroup);
    0.schedule(%this);
    log("communication", "debug", getScopeName() @ " " @ "- Currently, " @ CURLSimGroup.getCount() @ " CURLObjects remaining in the system.");
    if (isObject(%this.callBackSink)) {
        %this.onDone(%this.callBackSink);
    }
};
function ScreenShotUploaderClass::onError(%this, %errorNum, %errorName) {
    log("communication", "error", getScopeName() @ " " @ "- " @ %this.getName() @ " received error \"" @ %errorName @ "\"!");
    %this.remove(CURLSimGroup);
    0.schedule(%this);
    log("communication", "debug", getScopeName() @ " " @ "- Currently, " @ CURLSimGroup.getCount() @ " CURLObjects remaining in the system.");
    if (isObject(%this.callBackSink)) {
        %this.onError(%this.callBackSink);
    }
};
