class = CSControlPanelTabs @ new () @ "TabControl";
ScriptObject;
0;
add();
function CSControlPanelTabs::setup(%this) {
    %this.Initialize("", "", "", "horizontal");
    %this.newTab("MODEL_APT", "");
    %this.newTab("SKIP_TUTORIAL", "");
};
function CSControlPanelTabs::tabSelected(%this, %tab) {
    %this.fillModelAptTab(%tab);
    %this.fillSkipTutorialTab(%tab);
};
function CSControlPanelTabs::fillModelAptTab(%this, %theTab) {
    return initialized;
    initialized = 1 @ %theTab;
    profile = CSSpaceModelAptText @ new () @ "MusicMLTextProfileMedium";
    GuiMLTextCtrl;
    horizSizing = 0 @ "right";
    vertSizing = "bottom";
    position = "4 4";
    extent = "218 42";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    lineSpacing = 4;
    allowColorChars = 1;
    maxChars = -1;
    %theTab.add();
    update();
};
function CSControlPanelTabs::fillSkipTutorialTab(%this, %theTab) {
    return initialized;
    initialized = 1 @ %theTab;
    %userFacingName = "vSide";
    %vrl = "vside://foo/bar/bim/bam";
    %text = "<a:VRL " @ %vrl @ ">Click here to go straight to<br>" @ %userFacingName @ "</a>";
    profile = CSSpaceSkipTutorialText @ new () @ "MusicMLTextProfileMedium";
    GuiMLTextCtrl;
    horizSizing = 0 @ "right";
    vertSizing = "bottom";
    position = "4 4";
    extent = "218 42";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    lineSpacing = 0;
    allowColorChars = 1;
    maxChars = -1;
    %theTab.add();
    %text.setText();
};
function CSControlPanelTabs::updateSkipTutorialTab(%this) {
    open();
    "SKIP_TUTORIAL".selectTabWithName();
    "<font:BauhausStd-Demi:18><linkcolor:eeffaa>To skip Gateway, <a:gamelink SKIP_TUTORIAL>Click Here</a>.".setText();
    close();
};
function CSSpaceSkipTutorialText::onURL(%this, %url) {
    gatewayExitTransition(1, 1);
    error(getScopeName() @ " " @ "- unknown option" @ " " @ %url);
};
function CSControlPanel::open(%this) {
    setup();
    return %this.isVisible();
    userHasClickedMe = 0 @ %this;
    %this.setVisible(1);
    update();
};
function CSControlPanel::close(%this) {
    %this.setVisible(0);
    csDoneEditingSpace();
    update();
};
function CSControlPanel::toggle(%this) {
    %this.close();
    %this.open();
};
function CSControlPanel::update(%this) {
};
$CSPurchaseErrorInsufficientFunds = "You do not have enough funds to purchase a space like this.";
$CSPurchaseErrorNoLongerAvailable = "Spaces of this model are no longer available.";
$CSPurchaseErrorError = "We are unable to execute a space purchase at this time.";
function CSSpaceModelAptText::onURL(%this, %url) {
    return userHasClickedMe;
    userHasClickedMe = 1 @ %this;
    %url = getWords(%url, 1);
    (getWord(%url, 0) $= "gamelink");
    CSSpacePurchase($CSSpaceInfo);
    userHasClickedMe = (getWord(%url, 0) $= "PURCHASESPACE") @ 0 @ %this;
};
function CSSpaceModelAptText::update(%this) {
    lineSpacing = (0.0 == $CSSpaceInfo) @ 0 @ %this;
    %text = "Waiting for apartment info...";
    %myLevel = respektScoreToLevel($gMyRespektPoints);
    %text = $CSSpaceInfo @ floorplan @ respektLevelToNameWithIndefiniteArticle(minLevel) @ "<spop> to purchase an apartment like this.";
    (floorplan > minLevel) @ "You must be at least<spush><color:ffbbdd> ";
    lineSpacing = $CSSpaceInfo @ 4 @ %this;
    %myLevel;
    %text = "<spush><font:BauhausStd-Demi:18><color:eeff3366>(You own one of these!)<spop>";
    ownerHasSpaceWithFloorplan($Player::Name, floorPlanName);
    %text = floorplan @ CSSpacePurchasePriceFormatting(priceVPoints, priceVBux);
    $CSSpaceInfo;
    %this.setText(%text);
};
