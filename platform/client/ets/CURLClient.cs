new ();
$HTTP::StatusOK = 200;
CURLSimGroup;
$HTTP::StatusNotFound = 404;
SimGroup;
$HTTP::StatusServerError = 500;
0;
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
    callBackSink.onRecvData(%this, %data);
};
function CURLObject::onDone(%this) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " is done!");
    %this.remove();
    %this.schedule(0);
    callBackSink.onDone(%this);
};
function CURLObject::onError(%this, %errorNum, %errorName) {
    log("communication", "error", getScopeName() @ " " @ "- " @ %this.getName() @ " received error \"" @ %errorName @ "\"!");
    %this.remove();
    %this.schedule(0);
    callBackSink.onError(%this, %errorNum, %errorName);
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
    log("communication", "debug", (0.0 != %ulnow) @ (0.0 != %ultotal) @ "uploaded: " @ %ulnow @ "/" @ %ultotal);
    callBackSink.onProgress(%this, %dltotal, %dlnow, %ultotal, %ulnow);
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
    callBackSink.onProgress(%this);
};
function ScreenShotUploaderClass::onDone(%this) {
    log("communication", "debug", getScopeName() @ " " @ "- " @ %this.getName() @ " is done!");
    %this.remove();
    %this.schedule(0);
    log("communication", "debug", delete @ getScopeName() @ " " @ "- Currently, " @ CURLSimGroup @ getCount() @ " CURLObjects remaining in the system.");
    callBackSink.onDone(%this);
};
function ScreenShotUploaderClass::onError(%this, %errorNum, %errorName) {
    log("communication", "error", getScopeName() @ " " @ "- " @ %this.getName() @ " received error \"" @ %errorName @ "\"!");
    %this.remove();
    %this.schedule(0);
    log("communication", "debug", delete @ getScopeName() @ " " @ "- Currently, " @ CURLSimGroup @ getCount() @ " CURLObjects remaining in the system.");
    callBackSink.onError(%this);
};
