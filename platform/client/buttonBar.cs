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
$ButtonBarVar::buttonBarActivatorHeight = ((2.0 * $ButtonBarVar::buttonBarActivatorTopBorder) + $ButtonBarVar::dotWidth);
$ButtonBarVar::buttonBarPaddingBottom = 0;
function ButtonBarActivator::onMouseEnter(%this) {
    ButtonBar.show();
};
function ButtonBar::Initialize(%this) {
    if ($ButtonBarVar::Initialized) {
        return;
    }
    $ButtonBarVar::Initialized = 1;
    %this.clear();
    ButtonBarActivator.clear();
    %this.buttons = 0 @ "count";
    %background = new GuiBitmapCtrl(ButtonBarBackground) {
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
    %background.add(%this);
    0.addButtonWithPopupMenu(%this, "PrivateSpacePopupMenu", "platform/client/buttons/bb_apartment");
    "".addMenuItem(PrivateSpacePopupMenu, "My Furnishings", "toggleCSPanel(CSInventoryBrowserWindow);", "platform/client/buttons/bb_apartment_furniture");
    "".addMenuItem(PrivateSpacePopupMenu, "Shop", "toggleCSPanel(CSShoppingBrowserWindow);", "platform/client/buttons/bb_apartment_shop");
    "".addMenuItem(PrivateSpacePopupMenu, "Materials & Surfaces", "toggleCSPanel(CSPaintingWindow);", "platform/client/buttons/bb_apartment_paint");
    "".addMenuItem(PrivateSpacePopupMenu, "Layouts", "toggleCSPanel(CSLayoutSelector);", "platform/client/buttons/bb_apartment_layout");
    "".addMenuItem(PrivateSpacePopupMenu, "My Music & Videos", "toggleCSPanel(CSMediaDisplay);", "platform/client/buttons/bb_apartment_audio_video");
    "".addMenuItem(PrivateSpacePopupMenu, "My Rules & Description", "toggleCSPanel(CSRulesAndDescWindow);", "platform/client/buttons/bb_apartment_settings");
    "".addMenuItem(PrivateSpacePopupMenu, "My Other Places", "geTGF.toggleToTabName(\"MyPlace\");", "platform/client/buttons/bb_apartment_myOtherPlaces");
    0.addButton(%this, "StoreButton", "toggleStore();", "platform/client/buttons/bb_store");
    0.addButton(%this, "BuildingDirectoryButton", "toggleBuildingDirectory();", "platform/client/buttons/bb_building_directory");
    BuildingDirectoryButton.lastBuildingEntered = "";
    1.addButton(%this, "PlacesButton", "toggleTGF();", "platform/client/buttons/bb_places");
    1.addButtonWithPopupMenu(%this, "MePopupMenu", "platform/client/buttons/bb_avatar");
    if ($UserPref::UI::ShowReloadTextures) {
        "".addMenuItem(MePopupMenu, "Reload Textures", "playerTexturesReload();", "platform/client/buttons/bb_avatar_hanger");
    }
    "F7".addMenuItem(MePopupMenu, "View", "nextPlayerCamMode();", "platform/client/buttons/bb_avatar_view");
    "".addMenuItem(MePopupMenu, "Body", "toggleBodyTab();", "platform/client/buttons/bb_avatar_body");
    "F5".addMenuItem(MePopupMenu, "Closet", "toggleClosetTab();", "platform/client/buttons/bb_avatar_hanger");
    "F4".addMenuItem(MePopupMenu, "Actions", "toggleEmoteHud();", "platform/client/buttons/bb_avatar_action");
    1.addButtonWithPopupMenu(%this, "PeoplePopupMenu", "platform/client/buttons/bb_people");
    "".addMenuItem(PeoplePopupMenu, "Invite friends - earn vPoints!", "doInviteFriends();", "platform/client/buttons/bb_people_friends");
    "".addMenuItem(PeoplePopupMenu, "AIM", "toggleBuddyHudForTab(\"AIM\");", "platform/client/buttons/bb_people_aim");
    "".addMenuItem(PeoplePopupMenu, "Requests", "toggleBuddyHudForTab(\"requests\");", "platform/client/buttons/bb_people_requests");
    "F3".addMenuItem(PeoplePopupMenu, "Friends", "toggleBuddyHudForTab(\"friends\");", "platform/client/buttons/bb_people_friends");
    1.addButtonWithPopupMenu(%this, "ToolsPopupMenu", "platform/client/buttons/bb_tools");
    "Ctrl B".addMenuItem(ToolsPopupMenu, "Camera", "toggleCameraImgBroadcast();", "platform/client/buttons/bb_tools_camera");
    "F1".addMenuItem(ToolsPopupMenu, "Radar", "toggleLocalMap();", "platform/client/buttons/bb_tools_radar");
    "F6".addMenuItem(ToolsPopupMenu, "Settings", "toggleOptionsPanel();", "platform/client/buttons/bb_tools_settings");
    %thisMenuName = "geWebPopupMenu";
    "".addButtonWithPopupMenu(%this, %thisMenuName, "platform/client/buttons/bb_web", 1, "");
    %item = "".addMenuItem(%thisMenuName, "Invite friends - earn vPoints!", "doInviteFriends();", "platform/client/buttons/bb_people_friends");
    %item = "".addMenuItem(%thisMenuName, "vSide Home", "gotoWebPage(\"http://" @ $Net::BaseDomain @ "/\");", "platform/client/buttons/bb_help_info");
    %item = "".addMenuItem(%thisMenuName, "My Profile", "doEditProfile();", "platform/client/buttons/bb_help_info");
    %item = "".addMenuItem(%thisMenuName, "Music", "gotoWebPage(\"" @ $Net::MusicURL @ "\" );", "platform/client/buttons/bb_help_info");
    %item = "".addMenuItem(%thisMenuName, "Forums", "gotoWebPage(\"" @ $Net::ForumsURL @ "\" );", "platform/client/buttons/bb_help_info");
    %item = "".addMenuItem(%thisMenuName, "Events", "gotoWebPage(\"" @ $Net::EventsURL @ "\" );", "platform/client/buttons/bb_help_info");
    "".addButtonWithPopupMenu(%this, "HelpPopupMenu", "platform/client/buttons/bb_help", 1, "");
    %item = "".addMenuItem(HelpPopupMenu, "Info for parents", "gotoWebPage(\"" @ $Net::HelpURL_Parents @ "\");", "platform/client/buttons/bb_help_info");
    %item = "".addMenuItem(HelpPopupMenu, "House Rules", "gotoWebPage(\"" @ $Net::HelpURL_Guidelines @ "\");", "platform/client/buttons/bb_help_info");
    %item = "".addMenuItem(HelpPopupMenu, "FAQ: Designing your own clothes", "gotoWebPage(\"" @ $Net::HelpURL_VHD @ "\");", "platform/client/buttons/bb_help_faq");
    %item = "".addMenuItem(HelpPopupMenu, "FAQ: Moving, dancing, and chatting", "gotoWebPage(\"" @ $Net::HelpURL_Navigation @ "\");", "platform/client/buttons/bb_help_faq");
    %item = "".addMenuItem(HelpPopupMenu, "FAQ: Music and events", "gotoWebPage(\"" @ $Net::HelpURL_MusicNEvents @ "\");", "platform/client/buttons/bb_help_faq");
    %item = "".addMenuItem(HelpPopupMenu, "FAQ: Something is wrong with vSide", "gotoWebPage(\"" @ $Net::HelpURL_Support @ "\");", "platform/client/buttons/bb_help_faq");
    %item = "".addMenuItem(HelpPopupMenu, "Someone is bothering me!", "gotoWebPage(\"" @ $Net::HelpURL_Abuse @ "\");", "platform/client/buttons/bb_help_alert");
    %item = "".addMenuItem(HelpPopupMenu, "Ask other vSiders for help", "toggleHelpMeMode();", "platform/client/buttons/bb_help_person");
    "helpMe".setInternalName(%item);
    updateHelpMeModeMenu();
    %background.bringToFront(%this);
    %this.update();
    $ButtonBarVar::Hidden = 0;
    %this.handleContiguousSpace();
};
function ButtonBar::makeButton(%this, %buttonName, %command, %bitmap) {
    %bbHeight = getWord(%this.extent, 1);
    %xPos = ($ButtonBarVar::buttonBarSideBorder + (($ButtonBarVar::buttonWidth + $ButtonBarVar::buttonPadding) * (%this.getCount() - 1.0)));
    %ypos = mFloor(((%bbHeight - $ButtonBarVar::buttonHeight) / 2.0));
    %button = new GuiBitmapButtonCtrl(%buttonName) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
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
};
function ButtonBar::makeNewDot(%this) {
    %dot = new GuiBitmapCtrl("") {
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
};
function ButtonBar::addButton(%this, %buttonName, %command, %bitmap, %insertUponCreate) {
    %button = %bitmap.makeButton(%this, %buttonName, %command);
    %this.buttons = %button TAB %buttonName @ "button";
    %this.buttons = "count" @ %this.buttons TAB %buttonName @ "buttonIndex";
    %this.buttons = %this.makeNewDot() TAB %buttonName @ "dot";
    %this.buttons = (%this.buttons + 1.0 @ "count");
    if (%insertUponCreate) {
        %buttonName.insertButton(%this);
    }
};
function ButtonBar::addButtonWithPopupMenu(%this, %menuName, %bitmap, %insertUponCreate) {
    %menu = MenuLayer::newMenu(%menuName);
    %menu.canHilite = 0;
    %buttonName = %menuName @ "Button";
    %button = %bitmap.makeButton(%this, %buttonName, %menuName @ ".showRelativeTo(" @ %buttonName @ ", true);");
    %button.menu = %menu;
    %button.buttonType = "MenuButton";
    %this.buttons = %button TAB %buttonName @ "button";
    %this.buttons = "count" @ %this.buttons TAB %buttonName @ "buttonIndex";
    %this.buttons = %this.makeNewDot() TAB %buttonName @ "dot";
    %this.buttons = (%this.buttons + 1.0 @ "count");
    if (%insertUponCreate) {
        %buttonName.insertButton(%this);
    }
};
function ButtonBar::insertButton(%this, %buttonName) {
    %buttonToInsert = %this.buttons;
    %buttonIndex = %this.buttons;
    %dotToInsert = %this.buttons;
    if ((%buttonToInsert.getObjectIndex(%this) != -(1.0))) {
        return %buttonName @ "button" TAB %buttonName @ "buttonIndex" TAB %buttonName @ "dot";
    }
    %dotToInsert.add(ButtonBarActivator);
    %dummyContainer = new GuiControl("");
    %maxCount = %this.getCount();
    %i = (%maxCount - 1.0);
    while ((%i > 0.0)) {
        %currentButton = %i.getObject(%this);
        %currentButton.remove(%this);
        %currentButton.add(%dummyContainer);
        %i = (%i - 1.0);
    }
    %buttonHasBeenInserted = 0;
    %maxCount = %dummyContainer.getCount();
    %i = (%maxCount - 1.0);
    while ((%i >= 0.0)) {
        %currentButton = %i.getObject(%dummyContainer);
        %currentButton.remove(%dummyContainer);
        if (!(%buttonHasBeenInserted)) {
        }
        if ((%this.buttons > (%i > 0.0) @ %buttonIndex TAB %currentButton.getName() @ "buttonIndex")) {
            %buttonToInsert.add(%this);
            %buttonHasBeenInserted = 1;
        }
        %currentButton.add(%this);
        %i = (%i - 1.0);
    }
    if (!(%buttonHasBeenInserted)) {
        %buttonToInsert.add(%this);
    }
    %dummyContainer.delete();
    %this.update();
};
function ButtonBar::removeButton(%this, %buttonName) {
    %buttonToRemove = %this.buttons;
    %dotToRemove = %this.buttons;
    if (!(isObject(%buttonToRemove))) {
        return %buttonName @ "button" TAB %buttonName @ "dot";
    }
    %indexOfButton = %buttonToRemove.getObjectIndex(%this);
    if ((%indexOfButton == -(1.0))) {
        return;
    }
    %buttonToRemove.remove(%this);
    %dotToRemove.remove(ButtonBarActivator);
    %this.update();
};
function ButtonBar::update(%this) {
    %screenWidth = getWord($UserPref::Video::Resolution, 0);
    %screenHeight = (getWord($UserPref::Video::Resolution, 1) - $ButtonBarVar::buttonBarPaddingBottom);
    %bbWidth = getWord(%this.extent, 0);
    %bbHeight = getWord(%this.extent, 1);
    %newWidth = %bbWidth;
    if (!($ButtonBarVar::Hidden)) {
        %numButtons = (%this.getCount() - 1.0);
        %newWidth = (((2.0 * $ButtonBarVar::buttonBarSideBorder) + (%numButtons * $ButtonBarVar::buttonWidth)) + ((%numButtons + 1.0) * $ButtonBarVar::buttonPadding));
    }
    %bbHeight.resize(%this, mFloor((((%screenWidth - %newWidth) / 2.0) + 1.0)), (%screenHeight - %bbHeight), %newWidth);
    $ButtonBarVar::VerticalAdjustment = 0;
    if ($ButtonBarVar::Hidden) {
        %widthPerButton = ($ButtonBarVar::dotWidth + $ButtonBarVar::dotPadding);
        %startingOffset = %xoffset = mFloor((((%bbWidth - (%widthPerButton * (%this.getCount() - 1.0))) + $ButtonBarVar::dotPadding) / 2.0));
        %yoffset = (%bbHeight - $ButtonBarVar::buttonMiniTopBorder);
        %maxCount = %this.getCount();
        %i = 1;
        while ((%i < %maxCount)) {
            %currentButton = %i.getObject(%this);
            %yoffset.setTrgPosition(%currentButton, %xoffset);
            $ButtonBarVar::buttonMiniHeight.setTrgExtent(%currentButton, $ButtonBarVar::dotWidth);
            %xoffset = (%xoffset + ($ButtonBarVar::dotWidth + $ButtonBarVar::dotPadding));
            %i = (%i + 1.0);
        }
        %xoffset = %startingOffset;
        %yoffset = $ButtonBarVar::buttonBarActivatorTopBorder;
        %maxCount = ButtonBarActivator.getCount();
        %i = 0;
        while ((%i < %maxCount)) {
            %currentDot = %i.getObject(ButtonBarActivator);
            %yoffset.setTrgPosition(%currentDot, %xoffset);
            %xoffset = (%xoffset + ($ButtonBarVar::dotWidth + $ButtonBarVar::dotPadding));
            %i = (%i + 1.0);
        }
        $ButtonBarVar::VerticalAdjustment = ($ButtonBarVar::buttonHeight - $ButtonBarVar::buttonMiniHeight);
        %newBgExt = ((%xoffset - %startingOffset) + $ButtonBarVar::dotPadding) @ " " @ $ButtonBarVar::buttonBarActivatorHeight;
        %newBgPos = (%startingOffset - $ButtonBarVar::dotPadding) @ " " @ (%bbHeight - $ButtonBarVar::buttonBarActivatorHeight);
    }
    %xoffset = $ButtonBarVar::buttonBarSideBorder;
    %yoffset = mFloor(((%bbHeight - $ButtonBarVar::buttonHeight) / 2.0));
    %maxCount = %this.getCount();
    %i = 1;
    while ((%i < %maxCount)) {
        %currentButton = %i.getObject(%this);
        1.setVisible(%currentButton);
        %yoffset.setTrgPosition(%currentButton, %xoffset);
        $ButtonBarVar::buttonHeight.setTrgExtent(%currentButton, $ButtonBarVar::buttonWidth);
        %xoffset = (%xoffset + ($ButtonBarVar::buttonWidth + $ButtonBarVar::buttonPadding));
        %i = (%i + 1.0);
    }
    %newBgExt = %newWidth @ " " @ %bbHeight;
    %newBgPos = "0 0";
    $ButtonBarVar::buttonBarActivatorHeight.resize(ButtonBarActivator, mFloor((((%screenWidth - %newWidth) / 2.0) + 1.0)), (%screenHeight - $ButtonBarVar::buttonBarActivatorHeight), %newWidth);
    if (isObject(%this.background)) {
        %newBgExt.setTrgExtent(%this.background);
        %newBgPos.setTrgPosition(%this.background);
    }
    if (isObject(geTicker)) {
        geTicker.update();
    }
};
function ButtonBar::onMouseLeaveBounds(%this) {
    %this.hide();
};
function ButtonBar::onMouseEnterBounds(%this) {
    $ButtonBarVar::scheduled = 0;
    %this.update();
};
function ButtonBar::show(%this) {
    $ButtonBarVar::scheduled = 0;
    if (!($ButtonBarVar::Hidden)) {
        return;
    }
    $ButtonBarVar::Hidden = 0;
    1.setVisible(%this);
    0.setVisible(ButtonBarActivator);
    %this.update();
    ButtonBar.pushToBack(PlayGui);
    ButtonBarActivator.bringToFront(PlayGui);
    PlayGuiGradients.bringToFront(PlayGui);
    MessageHud.updatePosition();
};
function ButtonBar::hide(%this) {
    if ($ButtonBarVar::Hidden) {
        return;
    }
    if (!($UserPref::ETS::ButtonBar::AutoHide)) {
        return;
    }
    $ButtonBarVar::Hidden = 1;
    ButtonBar.bringToFront(PlayGui);
    ButtonBarActivator.pushToBack(PlayGui);
    PlayGuiGradients.bringToFront(PlayGui);
    %this.update();
    $ButtonBarVar::scheduled = 0;
    MessageHud.updatePosition();
};
function ButtonBar::scheduledHide(%this) {
    if ($ButtonBarVar::scheduled) {
        %this.hide();
    }
};
function ButtonBar::showAndHide(%this) {
    %this.show();
    "scheduledHide".schedule(%this, $Pref::ETS::ButtonBar::timeout);
    $ButtonBarVar::scheduled = 1;
};
function ButtonBar::setAutoHiding(%this, %flag) {
    if (%flag) {
        $UserPref::ETS::ButtonBar::AutoHide = 1;
        %this.hide();
    }
    $UserPref::ETS::ButtonBar::AutoHide = 0;
    %this.show();
};
function ButtonBar::showButton(%this, %button) {
    %button.insertButton(%this);
    %this.showAndHide();
};
function ButtonBar::hideButton(%this, %button) {
    if (isObject(MenuLayer)) {
        MenuLayer.hide();
    }
    %button.removeButton(%this);
};
function ButtonBar::handleContiguousSpace(%this) {
    if (isObject(PlacesButton)) {
        %showPlaces = ($gContiguousSpaceName $= "gw") ? 0 : 1;
        if (%showPlaces) {
            PlacesButton.showButton(%this);
        }
        PlacesButton.hideButton(%this);
    }
    if (isObject(MessageHudShoutOutIcon)) {
        %isGW = ($gContiguousSpaceName $= "gw");
        !(%isGW).setVisible(MessageHudShoutOutIcon);
    }
};
function ButtonBarBackground::onReachedTarget(%this) {
    if ($ButtonBarVar::Hidden) {
        %maxCount = ButtonBar.getCount();
        %i = 1;
        while ((%i < %maxCount)) {
            0.setVisible(%i.getObject(ButtonBar));
            %i = (%i + 1.0);
        }
        1.setVisible(ButtonBarActivator);
    }
};
