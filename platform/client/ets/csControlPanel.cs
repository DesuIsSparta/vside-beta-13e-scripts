if (!(isObject(CSControlPanelTabs))) {
    new ScriptObject(CSControlPanelTabs) {
        class = "TabControl";
    };
    if (isObject(MissionCleanup)) {
        CSControlPanelTabs.add(MissionCleanup);
    }
}
function CSControlPanelTabs::setup(%this) {
    if (!(%this.initialized)) {
        "horizontal".Initialize(%this, CSControlPanelTabContainer, "", "", "");
        "".newTab(%this, "MODEL_APT");
        "".newTab(%this, "SKIP_TUTORIAL");
    }
};
function CSControlPanelTabs::tabSelected(%this, %tab) {
    if ((%tab.name $= "MODEL_APT")) {
        %tab.fillModelAptTab(%this);
    }
    if ((%tab.name $= "SKIP_TUTORIAL")) {
        %tab.fillSkipTutorialTab(%this);
    }
};
function CSControlPanelTabs::fillModelAptTab(%this, %theTab) {
    if (%theTab.initialized) {
        return;
    }
    %theTab.initialized = 1;
    new GuiMLTextCtrl(CSSpaceModelAptText) {
        profile = "MusicMLTextProfileMedium";
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
    };.add(%theTab);
    CSSpaceModelAptText.update();
};
function CSControlPanelTabs::fillSkipTutorialTab(%this, %theTab) {
    if (%theTab.initialized) {
        return;
    }
    %theTab.initialized = 1;
    %userFacingName = "vSide";
    %vrl = "vside://foo/bar/bim/bam";
    %text = "<a:VRL " @ %vrl @ ">Click here to go straight to<br>" @ %userFacingName @ "</a>";
    new GuiMLTextCtrl(CSSpaceSkipTutorialText) {
        profile = "MusicMLTextProfileMedium";
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
    };.add(%theTab);
    %text.setText(CSSpaceSkipTutorialText);
};
function CSControlPanelTabs::updateSkipTutorialTab(%this) {
    if (getCurrentContiguousSpaceOfferSkip()) {
        CSControlPanel.open();
        "SKIP_TUTORIAL".selectTabWithName(CSControlPanelTabs);
        "<font:BauhausStd-Demi:18><linkcolor:eeffaa>To skip Gateway, <a:gamelink SKIP_TUTORIAL>Click Here</a>.".setText(CSSpaceSkipTutorialText);
    }
    if ((CSControlPanelTabs.getCurrentTab() == "SKIP_TUTORIAL".getTabWithName(CSControlPanelTabs))) {
        CSControlPanel.close();
    }
};
function CSSpaceSkipTutorialText::onURL(%this, %url) {
    if ((%url $= "gamelink SKIP_TUTORIAL")) {
        gatewayExitTransition(1, 1);
    }
    error(getScopeName() @ " " @ "- unknown option" @ " " @ %url);
};
function CSControlPanel::open(%this) {
    CSControlPanelTabs.setup();
    if (%this.isVisible()) {
        return;
    }
    %this.userHasClickedMe = 0;
    1.setVisible(%this);
    WindowManager.update();
};
function CSControlPanel::close(%this) {
    0.setVisible(%this);
    csDoneEditingSpace();
    WindowManager.update();
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
    if (%this.userHasClickedMe) {
        return;
    }
    %this.userHasClickedMe = 1;
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    if ((getWord(%url, 0) $= "PURCHASESPACE")) {
        CSSpacePurchase($CSSpaceInfo);
    }
    %this.userHasClickedMe = 0;
};
function CSSpaceModelAptText::update(%this) {
    if (($CSSpaceInfo == 0.0)) {
        %this.lineSpacing = 0;
        %text = "Waiting for apartment info...";
    }
    %myLevel = respektScoreToLevel($gMyRespektPoints);
    if (($CSSpaceInfo.floorplan.minLevel > %myLevel)) {
        %text = "You must be at least<spush><color:ffbbdd> " @ respektLevelToNameWithIndefiniteArticle($CSSpaceInfo.floorplan.minLevel) @ "<spop> to purchase an apartment like this.";
    }
    %this.lineSpacing = 4;
    if (ownerHasSpaceWithFloorplan($Player::Name, $CSSpaceInfo.floorPlanName)) {
        %text = "<spush><font:BauhausStd-Demi:18><color:eeff3366>(You own one of these!)<spop>";
    }
    %text = "<spush><font:BauhausStd-Demi:18><linkcolor:eeff33><a:PURCHASESPACE>P u r c h a s e  T h i s  S p a c e !</a><spop>" @ "\n" @ CSSpacePurchasePriceFormatting($CSSpaceInfo.floorplan.priceVPoints, $CSSpaceInfo.floorplan.priceVBux);
    %text.setText(%this);
};
