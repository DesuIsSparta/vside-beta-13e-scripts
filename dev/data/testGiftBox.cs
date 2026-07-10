DeclareTestSuite("TestSuite_GiftBox");
function TestSuite_GiftBox::setup(%this) {
    "TEST_GiftBox_BASICS".addTestCase(%this);
    "TEST_GiftBox_LoadFromFile".addTestCase(%this);
};
function TEST_GiftBox_BASICS::runTest(%this) {
    if (!($StandAlone)) {
        "this test must be run in $standalone".assert(%this, 0);
        return;
    }
    %testGiftBoxData = "dev/testData/giftbox.txt";
    %gb = GiftBoxData::construct();
    %ret = 5.add(%gb, 100, "VPOINTS");
    "failed to add vPoints gift".assert(%this, %ret);
    %ret = 10.add(%gb, 50, "VPOINTS");
    "failed to add vPoints gift".assert(%this, %ret);
    %ret = 15.add(%gb, 50, "VPOINTS");
    "failed to add vPoints gift".assert(%this, %ret);
    %ret = 20.add(%gb, 10, "VPOINTS");
    "failed to add vPoints gift".assert(%this, %ret);
    %ret = 25.add(%gb, 10, "VPOINTS");
    "failed to add vPoints gift".assert(%this, %ret);
    %ret = 1000.add(%gb, 1, "VPOINTS");
    "failed to add vPoints gift".assert(%this, %ret);
    %ret = 1000.add(%gb, 1, "BLAHBLAH");
    "expected failure for unknown gift type".assert(%this, !(%ret));
    %ret = 1000.add(%gb, 0, "VPOINTS");
    "expected failure for adding zero gifts".assert(%this, !(%ret));
    %ret = 0.add(%gb, 1, "VPOINTS");
    "expected failure for adding zero vpoint gifts".assert(%this, !(%ret));
    %ret = -1345.add(%gb, 20, "SKUS");
    "expected failure for adding a bad sku number".assert(%this, !(%ret));
    "expected 221 total possible gifts in the box but got:" @ " " @ %gb.totalGiftsInBox().assert(%this, (%gb.totalGiftsInBox() == 221.0));
    %giftString = %gb.GetAGift();
    "expected at least 2 words in the gift string".assert(%this, (getWordCount(%giftString) >= 2.0));
    %type = getWord(%giftString, 0);
    if ((%type $= "SKUS")) {
    }
    "expected first word of gift string to be SKUS or VPOINTS".assert(%this, (%type $= "VPOINTS"));
    %gb.delete();
};
function TEST_GiftBox_LoadFromFile::runTest(%this) {
    if (!($StandAlone)) {
        "this test must be run in $standalone".assert(%this, 0);
        return;
    }
    %testGiftBoxData = "dev/testData/giftbox.txt";
    %gb = GiftBoxData::ConstructFromFile(%testGiftBoxData);
    "expected 601 total possible gifts in the box but got:" @ " " @ %gb.totalGiftsInBox().assert(%this, (%gb.totalGiftsInBox() == 601.0));
    %giftString = %gb.GetAGift();
    "expected at least 2 words in the gift string".assert(%this, (getWordCount(%giftString) >= 2.0));
    %type = getWord(%giftString, 0);
    if ((%type $= "SKUS")) {
    }
    "expected first word of gift string to be SKUS or VPOINTS".assert(%this, (%type $= "VPOINTS"));
    %gb.delete();
};
