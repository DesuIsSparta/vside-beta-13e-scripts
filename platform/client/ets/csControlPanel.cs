if (!(isObject())) {
    class = CSControlPanelTabs @ new ScriptObject(CSControlPanelTabs) @ "TabControl";
    if (isObject()) {
        add();
    }
}
function CSControlPanelTabs::setup(%this) {
    if (!(initialized)) {
        %this.Initialize("", "", "", "horizontal");
        %this.newTab("MODEL_APT", "");
        %this.newTab("SKIP_TUTORIAL", "");
    }
};
function CSControlPanelTabs::tabSelected(%this, %tab) {
    if ((%tab SPC name $= "MODEL_APT")) {
        %this.fillModelAptTab(%tab);
    }
    if ((%tab SPC name $= "SKIP_TUTORIAL")) {
        %this.fillSkipTutorialTab(%tab);
    }
};
function CSControlPanelTabs::fillModelAptTab(%this, %theTab) {
    if (initialized) {
        return %theTab;
    }
    initialized = 1 @ %theTab;
    profile = new GuiMLTextCtrl(CSSpaceModelAptText) @ "MusicMLTextProfileMedium";
    horizSizing = "right";
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
    if (initialized) {
        return %theTab;
    }
    initialized = 1 @ %theTab;
    %userFacingName = "vSide";
    %vrl = "vside://foo/bar/bim/bam";
    %text = "<a:VRL " @ %vrl @ ">Click here to go straight to<br>" @ %userFacingName @ "</a>";
    profile = new GuiMLTextCtrl(CSSpaceSkipTutorialText) @ "MusicMLTextProfileMedium";
    horizSizing = "right";
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
    if (getCurrentContiguousSpaceOfferSkip()) {
        open();
        "SKIP_TUTORIAL".selectTabWithName();
        "<font:BauhausStd-Demi:18><linkcolor:eeffaa>To skip Gateway, <a:gamelink SKIP_TUTORIAL>Click Here</a>.".setText();
    }
    if ((CSControlPanelTabs == getCurrentTab())) {
        close();
    }
};
function CSSpaceSkipTutorialText::onURL(%this, %url) {
    if ((%url $= "gamelink SKIP_TUTORIAL")) {
        gatewayExitTransition(1, 1);
    }
    error(getScopeName() @ " " @ "- unknown option" @ " " @ %url);
};
function CSControlPanel::open(%this) {
    setup();
    if (%this.isVisible()) {
        return CSControlPanelTabs;
    }
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
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
function CSControlPanel::update(%this) {
};
$CSPurchaseErrorInsufficientFunds = "You do not have enough funds to purchase a space like this.";
$CSPurchaseErrorNoLongerAvailable = "Spaces of this model are no longer available.";
$CSPurchaseErrorError = "We are unable to execute a space purchase at this time.";
function CSSpaceModelAptText::onURL(%this, %url) {
    if (userHasClickedMe) {
        return %this;
    }
    userHasClickedMe = 1 @ %this;
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    if ((getWord(%url, 0) $= "PURCHASESPACE")) {
        CSSpacePurchase($CSSpaceInfo);
    }
    userHasClickedMe = 0 @ %this;
};
function CSSpaceModelAptText::update(%this) {
    if ((0.0 == $CSSpaceInfo)) {
        lineSpacing = 0 @ %this;
        %text = "Waiting for apartment info...";
    }
    %myLevel = respektScoreToLevel($gMyRespektPoints);
    if ((floorplan > minLevel)) {
        %text = $CSSpaceInfo @ floorplan @ respektLevelToNameWithIndefiniteArticle(minLevel) @ "<spop> to purchase an apartment like this.";
        $CSSpaceInfo @ "You must be at least<spush><color:ffbbdd> ";
    }
    lineSpacing = %myLevel @ 4 @ %this;
    if (ownerHasSpaceWithFloorplan($Player::Name, floorPlanName)) {
        %text = "<spush><font:BauhausStd-Demi:18><color:eeff3366>(You own one of these!)<spop>";
        $CSSpaceInfo;
    }
    %text = floorplan @ CSSpacePurchasePriceFormatting(priceVPoints, priceVBux);
    $CSSpaceInfo;
    %this.setText(%text);
};
