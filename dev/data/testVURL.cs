DeclareTestSuite("TestSuite_VURL");
function TestSuite_VURL::setup(%this) {
    "TEST_VURL_PARSE_USER".addTestCase(%this);
    "TEST_VURL_PARSE_APARTMENT".addTestCase(%this);
    "TEST_VURL_PARSE_LOCATION".addTestCase(%this);
};
function TEST_VURL_PARSE_USER::runTest(%this) {
    %aVurlString = "vside:/user/Bob";
    %theVurl = new ScriptObject("") {
        class = "VURL";
    };
    if (%aVurlString.setVURL(%theVurl)) {
        "the target type should have been user".assertSameString(%this, %theVurl.targetType, "user");
        "the target type is wrong".assertSameString(%this, %theVurl.targetPath, "Bob");
    }
    "setVURL failed for this vurl:" @ " " @ %aVurlString.assert(%this, 0);
    %theVurl.delete();
};
function TEST_VURL_PARSE_APARTMENT::runTest(%this) {
    %aVurlString = "vside:/apartment/a_building/an_apartment";
    %theVurl = new ScriptObject("") {
        class = "VURL";
    };
    if (%aVurlString.setVURL(%theVurl)) {
        "the target type should have been apartment".assertSameString(%this, %theVurl.targetType, "apartment");
        "the targetDest is wrong".assertSameString(%this, %theVurl.targetDest, "an_apartment");
        "the targetCity is wrong".assertSameString(%this, %theVurl.targetCity, "a_building");
    }
    "setVURL failed for this vurl:" @ " " @ %aVurlString.assert(%this, 0);
    %theVurl.delete();
};
function TEST_VURL_PARSE_LOCATION::runTest(%this) {
    "test not implemented".assert(%this, 0);
};
