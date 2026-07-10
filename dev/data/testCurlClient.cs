function simpleGoogleTest() {
    %name = "simpleGoogleTest" @ getRandom(0, 100000);
    %curl = new CURLObject(%name);
    "http://www.google.com".setURL(%curl);
    1.setRecvData(%curl);
    if (%curl.start()) {
        %curl.add(CURLSimGroup);
    }
    %curl.delete();
};
function headerTest() {
    %name = "headerTest" @ getRandom(0, 100000);
    %curl = new CURLObject(%name);
    "http://www.doppelganger.com".setURL(%curl);
    1.setHeader(%curl);
    if (%curl.start()) {
        %curl.add(CURLSimGroup);
    }
    %curl.delete();
};
function verboseTest() {
    %name = "verboseTest" @ getRandom(0, 100000);
    %curl = new CURLObject(%name);
    "http://www.garagegames.com".setURL(%curl);
    1.setVerbose(%curl);
    if (%curl.start()) {
        %curl.add(CURLSimGroup);
    }
    %curl.delete();
};
function progressTest() {
    %name = "progressTest" @ getRandom(0, 100000);
    %curl = new CURLObject(%name);
    "http://gdperftest.com/perftest/alltest.htm".setURL(%curl);
    1.setProgress(%curl);
    if (%curl.start()) {
        %curl.add(CURLSimGroup);
    }
    %curl.delete();
};
function userheaderTest() {
    %name = "userheaderTest" @ getRandom(0, 100000);
    %curl = new CURLObject(%name);
    "http://www.google.com".setURL(%curl);
    "doppelganger-agent/1.0".setUserAgent(%curl);
    1.includeHeader(%curl);
    1.setRecvData(%curl);
    if (%curl.start()) {
        %curl.add(CURLSimGroup);
    }
    %curl.delete();
};
function classNameCurlTest() {
    %name = "classNameCurlTest" @ getRandom(0, 100000);
    %curl = new CURLObject(%name) {
        className = "CurlClassNameTest";
    };
    "http://www.google.com".setURL(%curl);
    1.setRecvData(%curl);
    if (%curl.start()) {
        %curl.add(CURLSimGroup);
    }
    %curl.delete();
};
function simpleDownloadTest() {
    %name = "simpleDownloadTest" @ getRandom(0, 100000);
    %curl = new URLPostObject(%name);
    "http://gdperftest.com/perftest/gfx/test.jpg".setURL(%curl);
    %name @ ".jpg".setDownloadFile(%curl);
    1.setRecvData(%curl);
    if (%curl.start()) {
        %curl.add(CURLSimGroup);
    }
    %curl.delete();
};
function downloadWithNameSpaceTest() {
    %name = "downloadWithNameSpaceTest" @ getRandom(0, 100000);
    %curl = new URLPostObject(%name) {
        className = "CurlDownloadClassName";
    };
    "http://www.historyplace.com/text-index.html".setURL(%curl);
    %name @ ".html".setDownloadFile(%curl);
    1.setRecvData(%curl);
    if (%curl.start()) {
        %curl.add(CURLSimGroup);
    }
    %curl.delete();
};
function simplePostTest() {
    %name = "simplePostTest" @ getRandom(0, 100000);
    %curl = new CURLPost(%name) {
        className = "PostTestClass";
    };
    "http://www.cs.tut.fi/~jkorpela/feedback.html".setURL(%curl);
    "msg=somemessagegoeshere&name=someonesname&from=someemail".setPostFields(%curl);
    1.setHeader(%curl);
    if (%curl.start()) {
        %curl.add(CURLSimGroup);
    }
    %curl.delete();
};
function newPostFileUploaderTest() {
    %fo = new FileObject("");
    if ("platform/chatLog.txt".openForWrite(%fo)) {
        getTimeStamp() @ " " @ getScopeName() @ " " @ "yee haw!".writeLine(%fo);
        %fo.close();
    }
    %fo.delete();
    sendRequest_AbuseReport("rudeGuy", "la la la", "First Offense", "Profanity", "platform/chatLog.txt", "onDoneOrErrorCallback_AbuseReport_Test");
};
function onDoneOrErrorCallback_AbuseReport_Test(%request) {
    if (%request.checkSuccess()) {
        MessageBoxOK("File uploaded", "check out http://elenzil.com/doppelganger/posttests/incoming/chatLog.txt", "");
    }
    MessageBoxOK("File upload failed", "request status =" @ " " @ %request.statusCode());
};
function simplePostFileUploaderTest() {
    %name = "simplePostFileUploaderTest" @ getRandom(0, 100000);
    %curl = new URLPostObject(%name);
    "http://adam.codedv.com/examples/post_dump.php".setURL(%curl);
    "EULA.txt".setPostFile(%curl, "file1");
    "this is variable 1".setURLParam(%curl, "variable1");
    "this is variable 2".setURLParam(%curl, "variable2");
    "this is variable 3".setURLParam(%curl, "variable3");
    1.setProgress(%curl);
    if (%curl.start()) {
        %curl.add(CURLSimGroup);
    }
    %curl.delete();
};
function anotherFileUploadTest() {
    %upurl = "http://www.lateralpunks.com/dc/post_dump.php";
    %local = "dc5.jpg";
    %name = "anotherFileUploadTest" @ getRandom(0, 100000);
    %curl = new CURLPostFileUploader(%name);
    %upurl.setURL(%curl);
    "dc5.jpg".setUploadFile(%curl, "file1");
    1.setProgress(%curl);
    1.setVerbose(%curl);
    if (%curl.start()) {
        %curl.add(CURLSimGroup);
    }
    %curl.delete();
};
function simpleScreenShotUploaderTest(%fileName) {
    %name = "simpleScreenShotUploaderTest" @ getRandom(0, 100000);
    %screenshot = new ScreenShotUploader(%name) {
        className = "ScreenShotUploaderClass";
    };
    "http://adam.codedv.com/examples/post_dump.php".setURL(%screenshot);
    1.setProgress(%screenshot);
    %fileName.setUploadFile(%screenshot, "file1");
    "this is variable 1".setKeyValue(%screenshot, "variable1");
    if (%screenshot.shoot()) {
        %screenshot.add(CURLSimGroup);
    }
    %screenshot.delete();
};
function curlTestAll() {
    %i = 0;
    while ((%i < 10.0)) {
        simpleGoogleTest();
        headerTest();
        verboseTest();
        progressTest();
        userheaderTest();
        classNameCurlTest();
        simpleDownloadTest();
        downloadWithNameSpaceTest();
        simplePostTest();
        simplePostFileUploaderTest();
        %i = (%i + 1.0);
    }
};
function testPcpUpdate() {
    %curl = new URLPostObject("") {
        className = "DCClass";
    };
    %url = "http://s-website.eviltwinstudios.net/get_avatar?userId=" @ urlEncode("frida kahlo");
    %url.setURL(%curl);
    "avatar.gif".setDownloadFile(%curl);
    1.setRecvData(%curl);
    if (!(%curl.start())) {
        %curl.delete();
        warn("ProfileCurrentPicture::update(): couldn't start dynamic download of +avatar pic.");
        return;
    }
};
function testNamespace::onDone(%unused) {
    echo("done");
};
function testNamespace::onRecvData(%unused, %unused) {
    echo("recvData");
};
function testCURLNamespace() {
    %curl = new CURLObject("") {
        className = "TestNamespace";
    };
    1.setRecvData(%curl);
    "http://www.google.com".setURL(%curl);
    %curl.start();
};
function testCURLDownload() {
    %curl = new URLPostObject("") {
        className = "TestDownload";
    };
    "http://www.historyplace.com/text-index.html".setURL(%curl);
    "testCURLDownload.html".setDownloadFile(%curl);
    1.setRecvData(%curl);
    %curl.start();
};
function stressTestFileDownload() {
    %i = 0;
    while ((%i < 100.0)) {
        %curl = new URLPostObject("") {
            className = "TestDownload";
        };
        %localFile = "test" @ %i;
        "http://winbuild/scripts/orion/images/jrrtbeams1.marquee.gardenbox.jpg".setURL(%curl);
        %localFile.setDownloadFile(%curl);
        1.setRecvData(%curl);
        if (!(%curl.start())) {
            warn(getScopeName() @ " " @ " - couldn't start download");
            %curl.delete();
            return;
        }
        %curl.add(CURLSimGroup);
        %i = (%i + 1.0);
    }
};
