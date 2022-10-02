function simpleGoogleTest()
{
    %name = "simpleGoogleTest" @ getRandom(0, 100000);
    %curl = new CURLObject(%name);
    %curl.setURL("http://www.google.com");
    %curl.setRecvData(1);
    if (%curl.start())
    {
        CURLSimGroup.add(%curl);
    }
    else
    {
        %curl.delete();
    }
    return ;
}
function headerTest()
{
    %name = "headerTest" @ getRandom(0, 100000);
    %curl = new CURLObject(%name);
    %curl.setURL("http://www.doppelganger.com");
    %curl.setHeader(1);
    if (%curl.start())
    {
        CURLSimGroup.add(%curl);
    }
    else
    {
        %curl.delete();
    }
    return ;
}
function verboseTest()
{
    %name = "verboseTest" @ getRandom(0, 100000);
    %curl = new CURLObject(%name);
    %curl.setURL("http://www.garagegames.com");
    %curl.setVerbose(1);
    if (%curl.start())
    {
        CURLSimGroup.add(%curl);
    }
    else
    {
        %curl.delete();
    }
    return ;
}
function progressTest()
{
    %name = "progressTest" @ getRandom(0, 100000);
    %curl = new CURLObject(%name);
    %curl.setURL("http://gdperftest.com/perftest/alltest.htm");
    %curl.setProgress(1);
    if (%curl.start())
    {
        CURLSimGroup.add(%curl);
    }
    else
    {
        %curl.delete();
    }
    return ;
}
function userheaderTest()
{
    %name = "userheaderTest" @ getRandom(0, 100000);
    %curl = new CURLObject(%name);
    %curl.setURL("http://www.google.com");
    %curl.setUserAgent("doppelganger-agent/1.0");
    %curl.includeHeader(1);
    %curl.setRecvData(1);
    if (%curl.start())
    {
        CURLSimGroup.add(%curl);
    }
    else
    {
        %curl.delete();
    }
    return ;
}
function classNameCurlTest()
{
    %name = "classNameCurlTest" @ getRandom(0, 100000);
    %curl = new CURLObject(%name);
    %curl.setURL("http://www.google.com");
    %curl.setRecvData(1);
    if (%curl.start())
    {
        CURLSimGroup.add(%curl);
    }
    else
    {
        %curl.delete();
    }
    return ;
}
function simpleDownloadTest()
{
    %name = "simpleDownloadTest" @ getRandom(0, 100000);
    %curl = new URLPostObject(%name);
    %curl.setURL("http://gdperftest.com/perftest/gfx/test.jpg");
    %curl.setDownloadFile(%name @ ".jpg");
    %curl.setRecvData(1);
    if (%curl.start())
    {
        CURLSimGroup.add(%curl);
    }
    else
    {
        %curl.delete();
    }
    return ;
}
function downloadWithNameSpaceTest()
{
    %name = "downloadWithNameSpaceTest" @ getRandom(0, 100000);
    %curl = new URLPostObject(%name);
    %curl.setURL("http://www.historyplace.com/text-index.html");
    %curl.setDownloadFile(%name @ ".html");
    %curl.setRecvData(1);
    if (%curl.start())
    {
        CURLSimGroup.add(%curl);
    }
    else
    {
        %curl.delete();
    }
    return ;
}
function simplePostTest()
{
    %name = "simplePostTest" @ getRandom(0, 100000);
    %curl = new CURLPost(%name);
    %curl.setURL("http://www.cs.tut.fi/~jkorpela/feedback.html");
    %curl.setPostFields("msg=somemessagegoeshere&name=someonesname&from=someemail");
    %curl.setHeader(1);
    if (%curl.start())
    {
        CURLSimGroup.add(%curl);
    }
    else
    {
        %curl.delete();
    }
    return ;
}
function newPostFileUploaderTest()
{
    %fo = new FileObject();
    if (%fo.openForWrite("platform/chatLog.txt"))
    {
        %fo.writeLine(getTimeStamp() SPC getScopeName() SPC "yee haw!");
        %fo.close();
    }
    %fo.delete();
    sendRequest_AbuseReport("rudeGuy", "la la la", "First Offense", "Profanity", "platform/chatLog.txt", "onDoneOrErrorCallback_AbuseReport_Test");
    return ;
}
function onDoneOrErrorCallback_AbuseReport_Test(%request)
{
    if (%request.checkSuccess())
    {
        MessageBoxOK("File uploaded", "check out http://elenzil.com/doppelganger/posttests/incoming/chatLog.txt", "");
    }
    else
    {
        MessageBoxOK("File upload failed", "request status =" SPC %request.statusCode());
    }
    return ;
}
function simplePostFileUploaderTest()
{
    %name = "simplePostFileUploaderTest" @ getRandom(0, 100000);
    %curl = new URLPostObject(%name);
    %curl.setURL("http://adam.codedv.com/examples/post_dump.php");
    %curl.setPostFile("file1", "EULA.txt");
    %curl.setURLParam("variable1", "this is variable 1");
    %curl.setURLParam("variable2", "this is variable 2");
    %curl.setURLParam("variable3", "this is variable 3");
    %curl.setProgress(1);
    if (%curl.start())
    {
        CURLSimGroup.add(%curl);
    }
    else
    {
        %curl.delete();
    }
    return ;
}
function anotherFileUploadTest()
{
    %upurl = "http://www.lateralpunks.com/dc/post_dump.php";
    %local = "dc5.jpg";
    %name = "anotherFileUploadTest" @ getRandom(0, 100000);
    %curl = new CURLPostFileUploader(%name);
    %curl.setURL(%upurl);
    %curl.setUploadFile("file1", "dc5.jpg");
    %curl.setProgress(1);
    %curl.setVerbose(1);
    if (%curl.start())
    {
        CURLSimGroup.add(%curl);
    }
    else
    {
        %curl.delete();
    }
    return ;
}
function simpleScreenShotUploaderTest(%fileName)
{
    %name = "simpleScreenShotUploaderTest" @ getRandom(0, 100000);
    %screenshot = new ScreenShotUploader(%name);
    %screenshot.setURL("http://adam.codedv.com/examples/post_dump.php");
    %screenshot.setProgress(1);
    %screenshot.setUploadFile("file1", %fileName);
    %screenshot.setKeyValue("variable1", "this is variable 1");
    if (%screenshot.shoot())
    {
        CURLSimGroup.add(%screenshot);
    }
    else
    {
        %screenshot.delete();
    }
    return ;
}
function curlTestAll()
{
    %i = 0;
    while (%i < 10)
    {
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
        %i = %i + 1;
    }
}

function testPcpUpdate()
{
    %curl = new URLPostObject();
    %url = "http://s-website.eviltwinstudios.net/get_avatar?userId=" @ urlEncode("frida kahlo");
    %curl.setURL(%url);
    %curl.setDownloadFile("avatar.gif");
    %curl.setRecvData(1);
    if (!%curl.start())
    {
        %curl.delete();
        warn("ProfileCurrentPicture::update(): couldn\'t start dynamic download of +avatar pic.");
        return ;
    }
    return ;
}
function testNamespace::onDone(%unused)
{
    echo("done");
    return ;
}
function testNamespace::onRecvData(%unused, %unused)
{
    echo("recvData");
    return ;
}
function testCURLNamespace()
{
    %curl = new CURLObject();
    %curl.setRecvData(1);
    %curl.setURL("http://www.google.com");
    %curl.start();
    return ;
}
function testCURLDownload()
{
    %curl = new URLPostObject();
    %curl.setURL("http://www.historyplace.com/text-index.html");
    %curl.setDownloadFile("testCURLDownload.html");
    %curl.setRecvData(1);
    %curl.start();
    return ;
}
function stressTestFileDownload()
{
    %i = 0;
    while (%i < 100)
    {
        %curl = new URLPostObject();
        %localFile = "test" @ %i;
        %curl.setURL("http://winbuild/scripts/orion/images/jrrtbeams1.marquee.gardenbox.jpg");
        %curl.setDownloadFile(%localFile);
        %curl.setRecvData(1);
        if (!%curl.start())
        {
            warn(getScopeName() SPC " - couldn\'t start download");
            %curl.delete();
            return ;
        }
        CURLSimGroup.add(%curl);
        %i = %i + 1;
    }
}


