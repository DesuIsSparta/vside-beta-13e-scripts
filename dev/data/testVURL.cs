DeclareTestSuite("TestSuite_VURL");
function TestSuite_VURL::setup(%this)
{
    %this.addTestCase("TEST_VURL_PARSE_USER");
    %this.addTestCase("TEST_VURL_PARSE_APARTMENT");
    %this.addTestCase("TEST_VURL_PARSE_LOCATION");
}
function TEST_VURL_PARSE_USER::runTest(%this)
{
    %aVurlString = "vside:/user/Bob";
    %theVurl = new ScriptObject("") {
        class = "VURL";
    };
    if (%theVurl.setVURL(%aVurlString))
    {
        %this.assertSameString(%theVurl.targetType, "user", "the target type should have been user");
        %this.assertSameString(%theVurl.targetPath, "Bob", "the target type is wrong");
    }
    else
    {
        %this.assert(0, "setVURL failed for this vurl:" @ " " @ %aVurlString);
    }
    %theVurl.delete();
}
function TEST_VURL_PARSE_APARTMENT::runTest(%this)
{
    %aVurlString = "vside:/apartment/a_building/an_apartment";
    %theVurl = new ScriptObject("") {
        class = "VURL";
    };
    if (%theVurl.setVURL(%aVurlString))
    {
        %this.assertSameString(%theVurl.targetType, "apartment", "the target type should have been apartment");
        %this.assertSameString(%theVurl.targetDest, "an_apartment", "the targetDest is wrong");
        %this.assertSameString(%theVurl.targetCity, "a_building", "the targetCity is wrong");
    }
    else
    {
        %this.assert(0, "setVURL failed for this vurl:" @ " " @ %aVurlString);
    }
    %theVurl.delete();
}
function TEST_VURL_PARSE_LOCATION::runTest(%this)
{
    %this.assert(0, "test not implemented");
}
