if (!isObject(CSControlPanelTabs))
{
    new ScriptObject(CSControlPanelTabs);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(CSControlPanelTabs);
    }
}
function CSControlPanelTabs::setup(%this)
{
    if (!%this.initialized)
    {
        %this.Initialize(CSControlPanelTabContainer, "", "", "", "horizontal");
        %this.newTab("MODEL_APT", "");
        %this.newTab("SKIP_TUTORIAL", "");
    }
    return ;
}
function CSControlPanelTabs::tabSelected(%this, %tab)
{
    if (%tab.name $= "MODEL_APT")
    {
        %this.fillModelAptTab(%tab);
    }
    else
    {
        if (%tab.name $= "SKIP_TUTORIAL")
        {
            %this.fillSkipTutorialTab(%tab);
        }
    }
    return ;
}
function CSControlPanelTabs::fillModelAptTab(%this, %theTab)
{
    if (%theTab.initialized)
    {
        return ;
    }
    %theTab.initialized = 1;
    CSSpaceModelAptText.update();
    return ;
}
function CSControlPanelTabs::fillSkipTutorialTab(%this, %theTab)
{
    if (%theTab.initialized)
    {
        return ;
    }
    %theTab.initialized = 1;
    %userFacingName = "vSide";
    %vrl = "vside://foo/bar/bim/bam";
    %text = "<a:VRL " @ %vrl @ ">Click here to go straight to<br>" @ %userFacingName @ "</a>";
    CSSpaceSkipTutorialText.setText(%text);
    return ;
}
function CSControlPanelTabs::updateSkipTutorialTab(%this)
{
    if (getCurrentContiguousSpaceOfferSkip())
    {
        CSControlPanel.open();
        CSControlPanelTabs.selectTabWithName("SKIP_TUTORIAL");
        CSSpaceSkipTutorialText.setText("<font:BauhausStd-Demi:18><linkcolor:eeffaa>To skip Gateway, <a:gamelink SKIP_TUTORIAL>Click Here</a>.");
    }
    else
    {
        if (CSControlPanelTabs.getCurrentTab() == CSControlPanelTabs.getTabWithName("SKIP_TUTORIAL"))
        {
            CSControlPanel.close();
        }
    }
    return ;
}
function CSSpaceSkipTutorialText::onURL(%this, %url)
{
    if (%url $= "gamelink SKIP_TUTORIAL")
    {
        gatewayExitTransition(1, 1);
    }
    else
    {
        error(getScopeName() SPC "- unknown option" SPC %url);
    }
    return ;
}
function CSControlPanel::open(%this)
{
    CSControlPanelTabs.setup();
    if (%this.isVisible())
    {
        return ;
    }
    %this.userHasClickedMe = 0;
    %this.setVisible(1);
    WindowManager.update();
    return ;
}
function CSControlPanel::close(%this)
{
    %this.setVisible(0);
    csDoneEditingSpace();
    WindowManager.update();
    return ;
}
function CSControlPanel::toggle(%this)
{
    if (%this.isVisible())
    {
        %this.close();
    }
    else
    {
        %this.open();
    }
    return ;
}
function CSControlPanel::update(%this)
{
    return ;
}
$CSPurchaseErrorInsufficientFunds = "You do not have enough funds to purchase a space like this.";
$CSPurchaseErrorNoLongerAvailable = "Spaces of this model are no longer available.";
$CSPurchaseErrorError = "We are unable to execute a space purchase at this time.";
function CSSpaceModelAptText::onURL(%this, %url)
{
    if (%this.userHasClickedMe)
    {
        return ;
    }
    %this.userHasClickedMe = 1;
    if (getWord(%url, 0) $= "gamelink")
    {
        %url = getWords(%url, 1);
    }
    if (getWord(%url, 0) $= "PURCHASESPACE")
    {
        CSSpacePurchase($CSSpaceInfo);
    }
    %this.userHasClickedMe = 0;
    return ;
}
function CSSpaceModelAptText::update(%this)
{
    if ($CSSpaceInfo == 0)
    {
        %this.lineSpacing = 0;
        %text = "Waiting for apartment info...";
    }
    else
    {
        %myLevel = respektScoreToLevel($gMyRespektPoints);
        if ($CSSpaceInfo.floorplan.minLevel > %myLevel)
        {
            %text = "You must be at least<spush><color:ffbbdd> " @ respektLevelToNameWithIndefiniteArticle($CSSpaceInfo.floorplan.minLevel) @ "<spop> to purchase an apartment like this.";
        }
        else
        {
            %this.lineSpacing = 4;
            if (ownerHasSpaceWithFloorplan($Player::Name, $CSSpaceInfo.floorPlanName))
            {
                %text = "<spush><font:BauhausStd-Demi:18><color:eeff3366>(You own one of these!)<spop>";
            }
            else
            {
                %text = "<spush><font:BauhausStd-Demi:18><linkcolor:eeff33><a:PURCHASESPACE>P u r c h a s e  T h i s  S p a c e !</a><spop>" NL CSSpacePurchasePriceFormatting($CSSpaceInfo.floorplan.priceVPoints, $CSSpaceInfo.floorplan.priceVBux);
            }
        }
    }
    %this.setText(%text);
    return ;
}
