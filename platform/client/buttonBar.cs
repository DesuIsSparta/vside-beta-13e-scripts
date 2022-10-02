$ButtonBarVar::buttonWidth = 39;
$ButtonBarVar::buttonHeight = 48;
$ButtonBarVar::buttonPadding = 2;
$ButtonBarVar::buttonMiniWidth = 7;
$ButtonBarVar::buttonMiniHeight = 13;
$ButtonBarVar::buttonMiniPadding = 16;
$ButtonBarVar::buttonMiniTopBorder = 14;
$ButtonBarVar::buttonBarSideBorder = 10;
$ButtonBarVar::dotWidth = 7;
$ButtonBarVar::dotPadding = 16;
$ButtonBarVar::buttonBarActivatorTopBorder = 6;
$ButtonBarVar::buttonBarActivatorSideBorder = 10;
$ButtonBarVar::buttonBarActivatorHeight = (2 * $ButtonBarVar::buttonBarActivatorTopBorder) + $ButtonBarVar::dotWidth;
$ButtonBarVar::buttonBarPaddingBottom = 0;
function ButtonBarActivator::onMouseEnter(%this)
{
    ButtonBar.show();
    return ;
}
function ButtonBar::Initialize(%this)
{
    if ($ButtonBarVar::Initialized)
    {
        return ;
    }
    $ButtonBarVar::Initialized = 1;
    %this.clear();
    ButtonBarActivator.clear();
    %this.buttons["count"] = 0;
    %background = new GuiBitmapCtrl(ButtonBarBackground)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = %this.getExtent();
        minExtent = "1 1";
        sluggishness = 0.3;
        visible = 1;
        bitmap = "./ui/bb_background";
        wrap = 0;
        trgReachedCommand = "$ThisControl.onReachedTarget();";
    };
    %this.background = %background;
    %this.add(%background);
    %this.addButtonWithPopupMenu("PrivateSpacePopupMenu", "platform/client/buttons/bb_apartment", 0);
    PrivateSpacePopupMenu.addMenuItem("My Furnishings", "toggleCSPanel(CSInventoryBrowserWindow);", "platform/client/buttons/bb_apartment_furniture", "");
    PrivateSpacePopupMenu.addMenuItem("Shop", "toggleCSPanel(CSShoppingBrowserWindow);", "platform/client/buttons/bb_apartment_shop", "");
    PrivateSpacePopupMenu.addMenuItem("Materials & Surfaces", "toggleCSPanel(CSPaintingWindow);", "platform/client/buttons/bb_apartment_paint", "");
    PrivateSpacePopupMenu.addMenuItem("Layouts", "toggleCSPanel(CSLayoutSelector);", "platform/client/buttons/bb_apartment_layout", "");
    PrivateSpacePopupMenu.addMenuItem("My Music & Videos", "toggleCSPanel(CSMediaDisplay);", "platform/client/buttons/bb_apartment_audio_video", "");
    PrivateSpacePopupMenu.addMenuItem("My Rules & Description", "toggleCSPanel(CSRulesAndDescWindow);", "platform/client/buttons/bb_apartment_settings", "");
    PrivateSpacePopupMenu.addMenuItem("My Other Places", "geTGF.toggleToTabName(\"MyPlace\");", "platform/client/buttons/bb_apartment_myOtherPlaces", "");
    %this.addButton("StoreButton", "toggleStore();", "platform/client/buttons/bb_store", 0);
    %this.addButton("BuildingDirectoryButton", "toggleBuildingDirectory();", "platform/client/buttons/bb_building_directory", 0);
    BuildingDirectoryButton.lastBuildingEntered = "";
    %this.addButton("PlacesButton", "toggleTGF();", "platform/client/buttons/bb_places", 1);
    %this.addButtonWithPopupMenu("MePopupMenu", "platform/client/buttons/bb_avatar", 1);
    if ($UserPref::UI::ShowReloadTextures)
    {
        MePopupMenu.addMenuItem("Reload Textures", "playerTexturesReload();", "platform/client/buttons/bb_avatar_hanger", "");
    }
    MePopupMenu.addMenuItem("View", "nextPlayerCamMode();", "platform/client/buttons/bb_avatar_view", "F7");
    MePopupMenu.addMenuItem("Body", "toggleBodyTab();", "platform/client/buttons/bb_avatar_body", "");
    MePopupMenu.addMenuItem("Closet", "toggleClosetTab();", "platform/client/buttons/bb_avatar_hanger", "F5");
    MePopupMenu.addMenuItem("Actions", "toggleEmoteHud();", "platform/client/buttons/bb_avatar_action", "F4");
    %this.addButtonWithPopupMenu("PeoplePopupMenu", "platform/client/buttons/bb_people", 1);
    PeoplePopupMenu.addMenuItem("Invite friends - earn vPoints!", "doInviteFriends();", "platform/client/buttons/bb_people_friends", "");
    PeoplePopupMenu.addMenuItem("AIM", "toggleBuddyHudForTab(\"AIM\");", "platform/client/buttons/bb_people_aim", "");
    PeoplePopupMenu.addMenuItem("Requests", "toggleBuddyHudForTab(\"requests\");", "platform/client/buttons/bb_people_requests", "");
    PeoplePopupMenu.addMenuItem("Friends", "toggleBuddyHudForTab(\"friends\");", "platform/client/buttons/bb_people_friends", "F3");
    %this.addButtonWithPopupMenu("ToolsPopupMenu", "platform/client/buttons/bb_tools", 1);
    ToolsPopupMenu.addMenuItem("Camera", "toggleCameraImgBroadcast();", "platform/client/buttons/bb_tools_camera", "Ctrl B");
    ToolsPopupMenu.addMenuItem("Radar", "toggleLocalMap();", "platform/client/buttons/bb_tools_radar", "F1");
    ToolsPopupMenu.addMenuItem("Settings", "toggleOptionsPanel();", "platform/client/buttons/bb_tools_settings", "F6");
    %thisMenuName = "geWebPopupMenu";
    %this.addButtonWithPopupMenu(%thisMenuName, "platform/client/buttons/bb_web", 1, "", "");
    %item = %thisMenuName.addMenuItem("Invite friends - earn vPoints!", "doInviteFriends();", "platform/client/buttons/bb_people_friends", "");
    %item = %thisMenuName.addMenuItem("vSide Home", "gotoWebPage(\"http://" @ $Net::BaseDomain @ "/\");", "platform/client/buttons/bb_help_info", "");
    %item = %thisMenuName.addMenuItem("My Profile", "doEditProfile();", "platform/client/buttons/bb_help_info", "");
    %item = %thisMenuName.addMenuItem("Music", "gotoWebPage(\"" @ $Net::MusicURL @ "\" );", "platform/client/buttons/bb_help_info", "");
    %item = %thisMenuName.addMenuItem("Forums", "gotoWebPage(\"" @ $Net::ForumsURL @ "\" );", "platform/client/buttons/bb_help_info", "");
    %item = %thisMenuName.addMenuItem("Events", "gotoWebPage(\"" @ $Net::EventsURL @ "\" );", "platform/client/buttons/bb_help_info", "");
    %this.addButtonWithPopupMenu("HelpPopupMenu", "platform/client/buttons/bb_help", 1, "", "");
    %item = HelpPopupMenu.addMenuItem("Info for parents", "gotoWebPage(\"" @ $Net::HelpURL_Parents @ "\");", "platform/client/buttons/bb_help_info", "");
    %item = HelpPopupMenu.addMenuItem("House Rules", "gotoWebPage(\"" @ $Net::HelpURL_Guidelines @ "\");", "platform/client/buttons/bb_help_info", "");
    %item = HelpPopupMenu.addMenuItem("FAQ: Designing your own clothes", "gotoWebPage(\"" @ $Net::HelpURL_VHD @ "\");", "platform/client/buttons/bb_help_faq", "");
    %item = HelpPopupMenu.addMenuItem("FAQ: Moving, dancing, and chatting", "gotoWebPage(\"" @ $Net::HelpURL_Navigation @ "\");", "platform/client/buttons/bb_help_faq", "");
    %item = HelpPopupMenu.addMenuItem("FAQ: Music and events", "gotoWebPage(\"" @ $Net::HelpURL_MusicNEvents @ "\");", "platform/client/buttons/bb_help_faq", "");
    %item = HelpPopupMenu.addMenuItem("FAQ: Something is wrong with vSide", "gotoWebPage(\"" @ $Net::HelpURL_Support @ "\");", "platform/client/buttons/bb_help_faq", "");
    %item = HelpPopupMenu.addMenuItem("Someone is bothering me!", "gotoWebPage(\"" @ $Net::HelpURL_Abuse @ "\");", "platform/client/buttons/bb_help_alert", "");
    %item = HelpPopupMenu.addMenuItem("Ask other vSiders for help", "toggleHelpMeMode();", "platform/client/buttons/bb_help_person", "");
    %item.setInternalName("helpMe");
    updateHelpMeModeMenu();
    %this.bringToFront(%background);
    %this.update();
    $ButtonBarVar::Hidden = 0;
    %this.handleContiguousSpace();
    return ;
}
function ButtonBar::makeButton(%this, %buttonName, %command, %bitmap)
{
    %bbHeight = getWord(%this.extent, 1);
    %xPos = $ButtonBarVar::buttonBarSideBorder + (($ButtonBarVar::buttonWidth + $ButtonBarVar::buttonPadding) * (%this.getCount() - 1));
    %ypos = mFloor((%bbHeight - $ButtonBarVar::buttonHeight) / 2);
    %button = new GuiBitmapButtonCtrl(%buttonName)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos SPC %ypos;
        extent = "39 48";
        minExtent = "1 1";
        sluggishness = 0.3;
        visible = 1;
        command = %command;
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = %bitmap;
        drawText = 0;
    };
    return %button;
}
function ButtonBar::makeNewDot(%this)
{
    %dot = new GuiBitmapCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "top";
        position = "0 0";
        extent = "7 7";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "./ui/bb_dot.png";
        wrap = 0;
    };
    return %dot;
}
function ButtonBar::addButton(%this, %buttonName, %command, %bitmap, %insertUponCreate)
{
    %button = %this.makeButton(%buttonName, %command, %bitmap);
    %this.buttons[%buttonName,"button"] = %button;
    %this.buttons[%buttonName,"buttonIndex"] = %this.buttons["count"];
    %this.buttons[%buttonName,"dot"] = %this.makeNewDot();
    %this.buttons["count"] = %this.buttons["count"] + 1;
    if (%insertUponCreate)
    {
        %this.insertButton(%buttonName);
    }
    return ;
}
function ButtonBar::addButtonWithPopupMenu(%this, %menuName, %bitmap, %insertUponCreate)
{
    %menu = MenuLayer::newMenu(%menuName);
    %menu.canHilite = 0;
    %buttonName = %menuName @ "Button";
    %button = %this.makeButton(%buttonName, %menuName @ ".showRelativeTo(" @ %buttonName @ ", true);", %bitmap);
    %button.menu = %menu;
    %button.buttonType = "MenuButton";
    %this.buttons[%buttonName,"button"] = %button;
    %this.buttons[%buttonName,"buttonIndex"] = %this.buttons["count"];
    %this.buttons[%buttonName,"dot"] = %this.makeNewDot();
    %this.buttons["count"] = %this.buttons["count"] + 1;
    if (%insertUponCreate)
    {
        %this.insertButton(%buttonName);
    }
    return ;
}
function ButtonBar::insertButton(%this, %buttonName)
{
    %buttonToInsert = %this.buttons[(%buttonName,"button")];
    %buttonIndex = %this.buttons[(%buttonName,"buttonIndex")];
    %dotToInsert = %this.buttons[(%buttonName,"dot")];
    if (%this.getObjectIndex(%buttonToInsert) != -1)
    {
        return ;
    }
    ButtonBarActivator.add(%dotToInsert);
    %dummyContainer = new GuiControl();
    %maxCount = %this.getCount();
    %i = %maxCount - 1;
    while (%i > 0)
    {
        %currentButton = %this.getObject(%i);
        %this.remove(%currentButton);
        %dummyContainer.add(%currentButton);
        %i = %i - 1;
    }
    %buttonHasBeenInserted = 0;
    %maxCount = %dummyContainer.getCount();
    %i = %maxCount - 1;
    while (%i >= 0)
    {
        %currentButton = %dummyContainer.getObject(%i);
        %dummyContainer.remove(%currentButton);
        if (!%buttonHasBeenInserted && (%this.buttons[(%currentButton.getName(),"buttonIndex")] > %buttonIndex))
        {
            %this.add(%buttonToInsert);
            %buttonHasBeenInserted = 1;
        }
        %this.add(%currentButton);
        %i = %i - 1;
    }
    if (!%buttonHasBeenInserted)
    {
        %this.add(%buttonToInsert);
    }
    %dummyContainer.delete();
    %this.update();
    return ;
}
function ButtonBar::removeButton(%this, %buttonName)
{
    %buttonToRemove = %this.buttons[(%buttonName,"button")];
    %dotToRemove = %this.buttons[(%buttonName,"dot")];
    if (!isObject(%buttonToRemove))
    {
        return ;
    }
    %indexOfButton = %this.getObjectIndex(%buttonToRemove);
    if (%indexOfButton == -1)
    {
        return ;
    }
    %this.remove(%buttonToRemove);
    ButtonBarActivator.remove(%dotToRemove);
    %this.update();
    return ;
}
function ButtonBar::update(%this)
{
    %screenWidth = getWord($UserPref::Video::Resolution, 0);
    %screenHeight = getWord($UserPref::Video::Resolution, 1) - $ButtonBarVar::buttonBarPaddingBottom;
    %bbWidth = getWord(%this.extent, 0);
    %bbHeight = getWord(%this.extent, 1);
    %newWidth = %bbWidth;
    if (!$ButtonBarVar::Hidden)
    {
        %numButtons = %this.getCount() - 1;
        %newWidth = ((2 * $ButtonBarVar::buttonBarSideBorder) + (%numButtons * $ButtonBarVar::buttonWidth)) + ((%numButtons + 1) * $ButtonBarVar::buttonPadding);
    }
    %this.resize(mFloor(((%screenWidth - %newWidth) / 2) + 1), %screenHeight - %bbHeight, %newWidth, %bbHeight);
    $ButtonBarVar::VerticalAdjustment = 0;
    if ($ButtonBarVar::Hidden)
    {
        %widthPerButton = $ButtonBarVar::dotWidth + $ButtonBarVar::dotPadding;
        %startingOffset = %xoffset = mFloor(((%bbWidth - (%widthPerButton * (%this.getCount() - 1))) + $ButtonBarVar::dotPadding) / 2);
        %yoffset = %bbHeight - $ButtonBarVar::buttonMiniTopBorder;
        %maxCount = %this.getCount();
        %i = 1;
        while (%i < %maxCount)
        {
            %currentButton = %this.getObject(%i);
            %currentButton.setTrgPosition(%xoffset, %yoffset);
            %currentButton.setTrgExtent($ButtonBarVar::dotWidth, $ButtonBarVar::buttonMiniHeight);
            %xoffset = %xoffset + ($ButtonBarVar::dotWidth + $ButtonBarVar::dotPadding);
            %i = %i + 1;
        }
        %xoffset = %startingOffset;
        %yoffset = $ButtonBarVar::buttonBarActivatorTopBorder;
        %maxCount = ButtonBarActivator.getCount();
        %i = 0;
        while (%i < %maxCount)
        {
            %currentDot = ButtonBarActivator.getObject(%i);
            %currentDot.setTrgPosition(%xoffset, %yoffset);
            %xoffset = %xoffset + ($ButtonBarVar::dotWidth + $ButtonBarVar::dotPadding);
            %i = %i + 1;
        }
        $ButtonBarVar::VerticalAdjustment = $ButtonBarVar::buttonHeight - $ButtonBarVar::buttonMiniHeight;
        %newBgExt = (%xoffset - %startingOffset) + $ButtonBarVar::dotPadding SPC $ButtonBarVar::buttonBarActivatorHeight;
        %newBgPos = %startingOffset - $ButtonBarVar::dotPadding SPC %bbHeight - $ButtonBarVar::buttonBarActivatorHeight;
    }
    else
    {
        %xoffset = $ButtonBarVar::buttonBarSideBorder;
        %yoffset = mFloor((%bbHeight - $ButtonBarVar::buttonHeight) / 2);
        %maxCount = %this.getCount();
        %i = 1;
        while (%i < %maxCount)
        {
            %currentButton = %this.getObject(%i);
            %currentButton.setVisible(1);
            %currentButton.setTrgPosition(%xoffset, %yoffset);
            %currentButton.setTrgExtent($ButtonBarVar::buttonWidth, $ButtonBarVar::buttonHeight);
            %xoffset = %xoffset + ($ButtonBarVar::buttonWidth + $ButtonBarVar::buttonPadding);
            %i = %i + 1;
        }
        %newBgExt = %newWidth SPC %bbHeight;
        %newBgPos = "0 0";
    }
    ButtonBarActivator.resize(mFloor(((%screenWidth - %newWidth) / 2) + 1), %screenHeight - $ButtonBarVar::buttonBarActivatorHeight, %newWidth, $ButtonBarVar::buttonBarActivatorHeight);
    if (isObject(%this.background))
    {
        %this.background.setTrgExtent(%newBgExt);
        %this.background.setTrgPosition(%newBgPos);
    }
    if (isObject(geTicker))
    {
        geTicker.update();
    }
    return ;
}
function ButtonBar::onMouseLeaveBounds(%this)
{
    %this.hide();
    return ;
}
function ButtonBar::onMouseEnterBounds(%this)
{
    $ButtonBarVar::scheduled = 0;
    %this.update();
    return ;
}
function ButtonBar::show(%this)
{
    $ButtonBarVar::scheduled = 0;
    if (!$ButtonBarVar::Hidden)
    {
        return ;
    }
    $ButtonBarVar::Hidden = 0;
    %this.setVisible(1);
    ButtonBarActivator.setVisible(0);
    %this.update();
    PlayGui.pushToBack(ButtonBar);
    PlayGui.bringToFront(ButtonBarActivator);
    PlayGui.bringToFront(PlayGuiGradients);
    MessageHud.updatePosition();
    return ;
}
function ButtonBar::hide(%this)
{
    if ($ButtonBarVar::Hidden)
    {
        return ;
    }
    if (!$UserPref::ETS::ButtonBar::AutoHide)
    {
        return ;
    }
    $ButtonBarVar::Hidden = 1;
    PlayGui.bringToFront(ButtonBar);
    PlayGui.pushToBack(ButtonBarActivator);
    PlayGui.bringToFront(PlayGuiGradients);
    %this.update();
    $ButtonBarVar::scheduled = 0;
    MessageHud.updatePosition();
    return ;
}
function ButtonBar::scheduledHide(%this)
{
    if ($ButtonBarVar::scheduled)
    {
        %this.hide();
    }
    return ;
}
function ButtonBar::showAndHide(%this)
{
    %this.show();
    %this.schedule($Pref::ETS::ButtonBar::timeout, "scheduledHide");
    $ButtonBarVar::scheduled = 1;
    return ;
}
function ButtonBar::setAutoHiding(%this, %flag)
{
    if (%flag)
    {
        $UserPref::ETS::ButtonBar::AutoHide = 1;
        %this.hide();
    }
    else
    {
        $UserPref::ETS::ButtonBar::AutoHide = 0;
        %this.show();
    }
    return ;
}
function ButtonBar::showButton(%this, %button)
{
    %this.insertButton(%button);
    %this.showAndHide();
    return ;
}
function ButtonBar::hideButton(%this, %button)
{
    if (isObject(MenuLayer))
    {
        MenuLayer.hide();
    }
    %this.removeButton(%button);
    return ;
}
function ButtonBar::handleContiguousSpace(%this)
{
    if (isObject(PlacesButton))
    {
        %showPlaces = $gContiguousSpaceName $= "gw" ? 0 : 1;
        if (%showPlaces)
        {
            %this.showButton(PlacesButton);
        }
        else
        {
            %this.hideButton(PlacesButton);
        }
    }
    if (isObject(MessageHudShoutOutIcon))
    {
        %isGW = $gContiguousSpaceName $= "gw";
        MessageHudShoutOutIcon.setVisible(!%isGW);
    }
    return ;
}
function ButtonBarBackground::onReachedTarget(%this)
{
    if ($ButtonBarVar::Hidden)
    {
        %maxCount = ButtonBar.getCount();
        %i = 1;
        while (%i < %maxCount)
        {
            ButtonBar.getObject(%i).setVisible(0);
            %i = %i + 1;
        }
        ButtonBarActivator.setVisible(1);
    }
    return ;
}
