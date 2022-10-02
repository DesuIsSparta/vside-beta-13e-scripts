DeclareTestSuite("TestSuite_GiftBox");
function TestSuite_GiftBox::setup(%this)
{
    %this.addTestCase("TEST_GiftBox_BASICS");
    %this.addTestCase("TEST_GiftBox_LoadFromFile");
    return ;
}
function TEST_GiftBox_BASICS::runTest(%this)
{
    if (!$StandAlone)
    {
        %this.assert(0, "this test must be run in $standalone");
        return ;
    }
    %testGiftBoxData = "dev/testData/giftbox.txt";
    %gb = GiftBoxData::construct();
    %ret = %gb.add(100, "VPOINTS", 5);
    %this.assert(%ret, "failed to add vPoints gift");
    %ret = %gb.add(50, "VPOINTS", 10);
    %this.assert(%ret, "failed to add vPoints gift");
    %ret = %gb.add(50, "VPOINTS", 15);
    %this.assert(%ret, "failed to add vPoints gift");
    %ret = %gb.add(10, "VPOINTS", 20);
    %this.assert(%ret, "failed to add vPoints gift");
    %ret = %gb.add(10, "VPOINTS", 25);
    %this.assert(%ret, "failed to add vPoints gift");
    %ret = %gb.add(1, "VPOINTS", 1000);
    %this.assert(%ret, "failed to add vPoints gift");
    %ret = %gb.add(1, "BLAHBLAH", 1000);
    %this.assert(!%ret, "expected failure for unknown gift type");
    %ret = %gb.add(0, "VPOINTS", 1000);
    %this.assert(!%ret, "expected failure for adding zero gifts");
    %ret = %gb.add(1, "VPOINTS", 0);
    %this.assert(!%ret, "expected failure for adding zero vpoint gifts");
    %ret = %gb.add(20, "SKUS", -1345);
    %this.assert(!%ret, "expected failure for adding a bad sku number");
    %this.assert(%gb.totalGiftsInBox() == 221, "expected 221 total possible gifts in the box but got:" SPC %gb.totalGiftsInBox());
    %giftString = %gb.GetAGift();
    %this.assert(getWordCount(%giftString) >= 2, "expected at least 2 words in the gift string");
    %type = getWord(%giftString, 0);
    %this.assert((%type $= "SKUS") || (%type $= "VPOINTS"), "expected first word of gift string to be SKUS or VPOINTS");
    %gb.delete();
    return ;
}
function TEST_GiftBox_LoadFromFile::runTest(%this)
{
    if (!$StandAlone)
    {
        %this.assert(0, "this test must be run in $standalone");
        return ;
    }
    %testGiftBoxData = "dev/testData/giftbox.txt";
    %gb = GiftBoxData::ConstructFromFile(%testGiftBoxData);
    %this.assert(%gb.totalGiftsInBox() == 601, "expected 601 total possible gifts in the box but got:" SPC %gb.totalGiftsInBox());
    %giftString = %gb.GetAGift();
    %this.assert(getWordCount(%giftString) >= 2, "expected at least 2 words in the gift string");
    %type = getWord(%giftString, 0);
    %this.assert((%type $= "SKUS") || (%type $= "VPOINTS"), "expected first word of gift string to be SKUS or VPOINTS");
    %gb.delete();
    return ;
}
