function simpleGoogleTest() {
    %name = "simpleGoogleTest" @ getRandom(0, 100000);
    %curl = new %name();;
    CURLObject;
    %curl.setURL("http://www.google.com");
    %curl.setRecvData(1);
    if (%curl.start()) {
        %curl.add();
    }
    %curl.delete();
};
function headerTest() {
    %name = "headerTest" @ getRandom(0, 100000);
    %curl = new %name();;
    CURLObject;
    %curl.setURL("http://www.doppelganger.com");
    %curl.setHeader(1);
    if (%curl.start()) {
        %curl.add();
    }
    %curl.delete();
};
function verboseTest() {
    %name = "verboseTest" @ getRandom(0, 100000);
    %curl = new %name();;
    CURLObject;
    %curl.setURL("http://www.garagegames.com");
    %curl.setVerbose(1);
    if (%curl.start()) {
        %curl.add();
    }
    %curl.delete();
};
function progressTest() {
    %name = "progressTest" @ getRandom(0, 100000);
    %curl = new %name();;
    CURLObject;
    %curl.setURL("http://gdperftest.com/perftest/alltest.htm");
    %curl.setProgress(1);
    if (%curl.start()) {
        %curl.add();
    }
    %curl.delete();
};
function userheaderTest() {
    %name = "userheaderTest" @ getRandom(0, 100000);
    %curl = new %name();;
    CURLObject;
    %curl.setURL("http://www.google.com");
    %curl.setUserAgent("doppelganger-agent/1.0");
    %curl.includeHeader(1);
    %curl.setRecvData(1);
    if (%curl.start()) {
        %curl.add();
    }
    %curl.delete();
};
function classNameCurlTest() {
    %name = "classNameCurlTest" @ getRandom(0, 100000);
    0;
    %curl = new %name() {
        className = CURLObject @ "CurlClassNameTest";
    };
    %curl.setURL("http://www.google.com");
    %curl.setRecvData(1);
    if (%curl.start()) {
        %curl.add();
    }
    %curl.delete();
};
function simpleDownloadTest() {
    %name = "simpleDownloadTest" @ getRandom(0, 100000);
    %curl = new %name();;
    URLPostObject;
    %curl.setURL("http://gdperftest.com/perftest/gfx/test.jpg");
    %curl.setDownloadFile(%name @ ".jpg");
    %curl.setRecvData(1);
    if (%curl.start()) {
        %curl.add();
    }
    %curl.delete();
};
function downloadWithNameSpaceTest() {
    %name = "downloadWithNameSpaceTest" @ getRandom(0, 100000);
    0;
    %curl = new %name() {
        className = URLPostObject @ "CurlDownloadClassName";
    };
    %curl.setURL("http://www.historyplace.com/text-index.html");
    %curl.setDownloadFile(%name @ ".html");
    %curl.setRecvData(1);
    if (%curl.start()) {
        %curl.add();
    }
    %curl.delete();
};
function simplePostTest() {
    %name = "simplePostTest" @ getRandom(0, 100000);
    0;
    %curl = new %name() {
        className = CURLPost @ "PostTestClass";
    };
    %curl.setURL("http://www.cs.tut.fi/~jkorpela/feedback.html");
    %curl.setPostFields("msg=somemessagegoeshere&name=someonesname&from=someemail");
    %curl.setHeader(1);
    if (%curl.start()) {
        %curl.add();
    }
    %curl.delete();
};
function newPostFileUploaderTest() {
    %fo = new ""();;
    FileObject;
    if (%fo.openForWrite("platform/chatLog.txt")) {
        %fo.writeLine(getTimeStamp() @ " " @ getScopeName() @ " " @ "yee haw!");
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
    %curl = new %name();;
    URLPostObject;
    %curl.setURL("http://adam.codedv.com/examples/post_dump.php");
    %curl.setPostFile("file1", "EULA.txt");
    %curl.setURLParam("variable1", "this is variable 1");
    %curl.setURLParam("variable2", "this is variable 2");
    %curl.setURLParam("variable3", "this is variable 3");
    %curl.setProgress(1);
    if (%curl.start()) {
        %curl.add();
    }
    %curl.delete();
};
function anotherFileUploadTest() {
    %upurl = "http://www.lateralpunks.com/dc/post_dump.php";
    %local = "dc5.jpg";
    %name = "anotherFileUploadTest" @ getRandom(0, 100000);
    %curl = new %name();;
    CURLPostFileUploader;
    %curl.setURL(%upurl);
    %curl.setUploadFile("file1", "dc5.jpg");
    %curl.setProgress(1);
    %curl.setVerbose(1);
    if (%curl.start()) {
        %curl.add();
    }
    %curl.delete();
};
function simpleScreenShotUploaderTest(%fileName) {
    %name = "simpleScreenShotUploaderTest" @ getRandom(0, 100000);
    0;
    %screenshot = new %name() {
        className = ScreenShotUploader @ "ScreenShotUploaderClass";
    };
    %screenshot.setURL("http://adam.codedv.com/examples/post_dump.php");
    %screenshot.setProgress(1);
    %screenshot.setUploadFile("file1", %fileName);
    %screenshot.setKeyValue("variable1", "this is variable 1");
    if (%screenshot.shoot()) {
        %screenshot.add();
    }
    %screenshot.delete();
};
function curlTestAll() {
    %i = 0;
    if ((10.0 < %i)) {
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
        %i = (1.0 + %i);
    }
};
function testPcpUpdate() {
    0;
    %curl = new ""() {
        className = URLPostObject @ "DCClass";
    };
    %url = "http://s-website.eviltwinstudios.net/get_avatar?userId=" @ urlEncode("frida kahlo");
    %curl.setURL(%url);
    %curl.setDownloadFile("avatar.gif");
    %curl.setRecvData(1);
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
    0;
    %curl = new ""() {
        className = CURLObject @ "TestNamespace";
    };
    %curl.setRecvData(1);
    %curl.setURL("http://www.google.com");
    %curl.start();
};
function testCURLDownload() {
    0;
    %curl = new ""() {
        className = URLPostObject @ "TestDownload";
    };
    %curl.setURL("http://www.historyplace.com/text-index.html");
    %curl.setDownloadFile("testCURLDownload.html");
    %curl.setRecvData(1);
    %curl.start();
};
function stressTestFileDownload() {
    %i = 0;
    if ((100.0 < %i)) {
        0;
        %curl = new ""() {
            className = URLPostObject @ "TestDownload";
        };
        %localFile = "test" @ %i;
        %curl.setURL("http://winbuild/scripts/orion/images/jrrtbeams1.marquee.gardenbox.jpg");
        %curl.setDownloadFile(%localFile);
        %curl.setRecvData(1);
        if (!(%curl.start())) {
            warn(getScopeName() @ " " @ " - couldn't start download");
            %curl.delete();
            return;
        }
        %curl.add();
        %i = (1.0 + %i);
        CURLSimGroup;
    }
};
