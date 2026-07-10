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
$ButtonBarVar::buttonBarActivatorHeight = ($ButtonBarVar::dotWidth + ($ButtonBarVar::buttonBarActivatorTopBorder * 2.0));
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
    %this.add(%background);
    %this.addButtonWithPopupMenu("PrivateSpacePopupMenu", "platform/client/buttons/bb_apartment", 0);
    "My Furnishings".addMenuItem("toggleCSPanel(CSInventoryBrowserWindow);", "platform/client/buttons/bb_apartment_furniture", "");
    "Shop".addMenuItem("toggleCSPanel(CSShoppingBrowserWindow);", "platform/client/buttons/bb_apartment_shop", "");
    "Materials & Surfaces".addMenuItem("toggleCSPanel(CSPaintingWindow);", "platform/client/buttons/bb_apartment_paint", "");
    "Layouts".addMenuItem("toggleCSPanel(CSLayoutSelector);", "platform/client/buttons/bb_apartment_layout", "");
    "My Music & Videos".addMenuItem("toggleCSPanel(CSMediaDisplay);", "platform/client/buttons/bb_apartment_audio_video", "");
    "My Rules & Description".addMenuItem("toggleCSPanel(CSRulesAndDescWindow);", "platform/client/buttons/bb_apartment_settings", "");
    "My Other Places".addMenuItem("geTGF.toggleToTabName(\"MyPlace\");", "platform/client/buttons/bb_apartment_myOtherPlaces", "");
    %this.addButton("StoreButton", "toggleStore();", "platform/client/buttons/bb_store", 0);
    %this.addButton("BuildingDirectoryButton", "toggleBuildingDirectory();", "platform/client/buttons/bb_building_directory", 0);
    %this.lastBuildingEntered = "" @ BuildingDirectoryButton;
    PrivateSpacePopupMenu;
    %this.addButton("PlacesButton", "toggleTGF();", "platform/client/buttons/bb_places", 1);
    %this.addButtonWithPopupMenu("MePopupMenu", "platform/client/buttons/bb_avatar", 1);
    if ($UserPref::UI::ShowReloadTextures) {
        "Reload Textures".addMenuItem("playerTexturesReload();", "platform/client/buttons/bb_avatar_hanger", "");
    }
    "View".addMenuItem("nextPlayerCamMode();", "platform/client/buttons/bb_avatar_view", "F7");
    "Body".addMenuItem("toggleBodyTab();", "platform/client/buttons/bb_avatar_body", "");
    "Closet".addMenuItem("toggleClosetTab();", "platform/client/buttons/bb_avatar_hanger", "F5");
    "Actions".addMenuItem("toggleEmoteHud();", "platform/client/buttons/bb_avatar_action", "F4");
    %this.addButtonWithPopupMenu("PeoplePopupMenu", "platform/client/buttons/bb_people", 1);
    "Invite friends - earn vPoints!".addMenuItem("doInviteFriends();", "platform/client/buttons/bb_people_friends", "");
    "AIM".addMenuItem("toggleBuddyHudForTab(\"AIM\");", "platform/client/buttons/bb_people_aim", "");
    "Requests".addMenuItem("toggleBuddyHudForTab(\"requests\");", "platform/client/buttons/bb_people_requests", "");
    "Friends".addMenuItem("toggleBuddyHudForTab(\"friends\");", "platform/client/buttons/bb_people_friends", "F3");
    %this.addButtonWithPopupMenu("ToolsPopupMenu", "platform/client/buttons/bb_tools", 1);
    "Camera".addMenuItem("toggleCameraImgBroadcast();", "platform/client/buttons/bb_tools_camera", "Ctrl B");
    "Radar".addMenuItem("toggleLocalMap();", "platform/client/buttons/bb_tools_radar", "F1");
    "Settings".addMenuItem("toggleOptionsPanel();", "platform/client/buttons/bb_tools_settings", "F6");
    %thisMenuName = "geWebPopupMenu";
    ToolsPopupMenu;
    %this.addButtonWithPopupMenu(%thisMenuName, "platform/client/buttons/bb_web", 1, "", "");
    %item = %thisMenuName.addMenuItem("Invite friends - earn vPoints!", "doInviteFriends();", "platform/client/buttons/bb_people_friends", "");
    ToolsPopupMenu;
    %item = %thisMenuName.addMenuItem("vSide Home", "gotoWebPage(\"http://" @ $Net::BaseDomain @ "/\");", "platform/client/buttons/bb_help_info", "");
    ToolsPopupMenu;
    %item = %thisMenuName.addMenuItem("My Profile", "doEditProfile();", "platform/client/buttons/bb_help_info", "");
    PeoplePopupMenu;
    %item = %thisMenuName.addMenuItem("Music", "gotoWebPage(\"" @ $Net::MusicURL @ "\" );", "platform/client/buttons/bb_help_info", "");
    PeoplePopupMenu;
    %item = %thisMenuName.addMenuItem("Forums", "gotoWebPage(\"" @ $Net::ForumsURL @ "\" );", "platform/client/buttons/bb_help_info", "");
    PeoplePopupMenu;
    %item = %thisMenuName.addMenuItem("Events", "gotoWebPage(\"" @ $Net::EventsURL @ "\" );", "platform/client/buttons/bb_help_info", "");
    PeoplePopupMenu;
    %this.addButtonWithPopupMenu("HelpPopupMenu", "platform/client/buttons/bb_help", 1, "", "");
    %item = "Info for parents".addMenuItem("gotoWebPage(\"" @ $Net::HelpURL_Parents @ "\");", "platform/client/buttons/bb_help_info", "");
    HelpPopupMenu;
    %item = "House Rules".addMenuItem("gotoWebPage(\"" @ $Net::HelpURL_Guidelines @ "\");", "platform/client/buttons/bb_help_info", "");
    HelpPopupMenu;
    %item = "FAQ: Designing your own clothes".addMenuItem("gotoWebPage(\"" @ $Net::HelpURL_VHD @ "\");", "platform/client/buttons/bb_help_faq", "");
    HelpPopupMenu;
    %item = "FAQ: Moving, dancing, and chatting".addMenuItem("gotoWebPage(\"" @ $Net::HelpURL_Navigation @ "\");", "platform/client/buttons/bb_help_faq", "");
    HelpPopupMenu;
    %item = "FAQ: Music and events".addMenuItem("gotoWebPage(\"" @ $Net::HelpURL_MusicNEvents @ "\");", "platform/client/buttons/bb_help_faq", "");
    HelpPopupMenu;
    %item = "FAQ: Something is wrong with vSide".addMenuItem("gotoWebPage(\"" @ $Net::HelpURL_Support @ "\");", "platform/client/buttons/bb_help_faq", "");
    HelpPopupMenu;
    %item = "Someone is bothering me!".addMenuItem("gotoWebPage(\"" @ $Net::HelpURL_Abuse @ "\");", "platform/client/buttons/bb_help_alert", "");
    HelpPopupMenu;
    %item = "Ask other vSiders for help".addMenuItem("toggleHelpMeMode();", "platform/client/buttons/bb_help_person", "");
    HelpPopupMenu;
    %item.setInternalName("helpMe");
    updateHelpMeModeMenu();
    %this.bringToFront(%background);
    %this.update();
    $ButtonBarVar::Hidden = 0;
    MePopupMenu;
    %this.handleContiguousSpace();
};
function ButtonBar::makeButton(%this, %buttonName, %command, %bitmap) {
    %bbHeight = getWord(%this.extent, 1);
    %xPos = (((1.0 - %this.getCount()) * ($ButtonBarVar::buttonPadding + $ButtonBarVar::buttonWidth)) + $ButtonBarVar::buttonBarSideBorder);
    %ypos = mFloor((2.0 / ($ButtonBarVar::buttonHeight - %bbHeight)));
    0;
    %button = new %buttonName() {
        profile = GuiBitmapButtonCtrl @ "GuiDefaultProfile";
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
    0;
    %dot = new ""() {
        profile = GuiBitmapCtrl @ "GuiDefaultProfile";
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
    %button = %this.makeButton(%buttonName, %command, %bitmap);
    %this.buttons = %button TAB %buttonName @ "button";
    %this.buttons = "count" @ %this.buttons TAB %buttonName @ "buttonIndex";
    %this.buttons = %this.makeNewDot() TAB %buttonName @ "dot";
    %this.buttons = (1.0 @ "count" + %this.buttons);
    if (%insertUponCreate) {
        %this.insertButton(%buttonName);
    }
};
function ButtonBar::addButtonWithPopupMenu(%this, %menuName, %bitmap, %insertUponCreate) {
    %menu = MenuLayer::newMenu(%menuName);
    %menu.canHilite = 0;
    %buttonName = %menuName @ "Button";
    %button = %this.makeButton(%buttonName, %menuName @ ".showRelativeTo(" @ %buttonName @ ", true);", %bitmap);
    %button.menu = %menu;
    %button.buttonType = "MenuButton";
    %this.buttons = %button TAB %buttonName @ "button";
    %this.buttons = "count" @ %this.buttons TAB %buttonName @ "buttonIndex";
    %this.buttons = %this.makeNewDot() TAB %buttonName @ "dot";
    %this.buttons = (1.0 @ "count" + %this.buttons);
    if (%insertUponCreate) {
        %this.insertButton(%buttonName);
    }
};
function ButtonBar::insertButton(%this, %buttonName) {
    %buttonToInsert = %this.buttons;
    %buttonName @ "button";
    %buttonIndex = %this.buttons;
    %buttonName @ "buttonIndex";
    %dotToInsert = %this.buttons;
    %buttonName @ "dot";
    if ((-(1.0) != %this.getObjectIndex(%buttonToInsert))) {
        return;
    }
    %dotToInsert.add();
    %dummyContainer = new ""();;
    GuiControl;
    %maxCount = %this.getCount();
    0;
    %i = (1.0 - %maxCount);
    ButtonBarActivator;
    if ((0.0 > %i)) {
        %currentButton = %this.getObject(%i);
        %this.remove(%currentButton);
        %dummyContainer.add(%currentButton);
        %i = (1.0 - %i);
    }
    %buttonHasBeenInserted = 0;
    (0.0 > %i);
    %maxCount = %dummyContainer.getCount();
    %i = (1.0 - %maxCount);
    if ((0.0 >= %i)) {
        %currentButton = %dummyContainer.getObject(%i);
        %dummyContainer.remove(%currentButton);
        if (!(%buttonHasBeenInserted)) {
        }
        if ((%buttonIndex TAB %currentButton.getName() @ "buttonIndex" > %this.buttons)) {
            %this.add(%buttonToInsert);
            %buttonHasBeenInserted = 1;
        }
        %this.add(%currentButton);
        %i = (1.0 - %i);
    }
    if (!(%buttonHasBeenInserted)) {
        %this.add(%buttonToInsert);
    }
    %dummyContainer.delete();
    %this.update();
};
function ButtonBar::removeButton(%this, %buttonName) {
    %buttonToRemove = %this.buttons;
    %buttonName @ "button";
    %dotToRemove = %this.buttons;
    %buttonName @ "dot";
    if (!(isObject(%buttonToRemove))) {
        return;
    }
    %indexOfButton = %this.getObjectIndex(%buttonToRemove);
    if ((-(1.0) == %indexOfButton)) {
        return;
    }
    %this.remove(%buttonToRemove);
    %dotToRemove.remove();
    %this.update();
};
function ButtonBar::update(%this) {
    %screenWidth = getWord($UserPref::Video::Resolution, 0);
    %screenHeight = ($ButtonBarVar::buttonBarPaddingBottom - getWord($UserPref::Video::Resolution, 1));
    %bbWidth = getWord(%this.extent, 0);
    %bbHeight = getWord(%this.extent, 1);
    %newWidth = %bbWidth;
    if (!($ButtonBarVar::Hidden)) {
        %numButtons = (1.0 - %this.getCount());
        %newWidth = (($ButtonBarVar::buttonPadding * (1.0 + %numButtons)) + (($ButtonBarVar::buttonWidth * %numButtons) + ($ButtonBarVar::buttonBarSideBorder * 2.0)));
    }
    %this.resize(mFloor((1.0 + (2.0 / (%newWidth - %screenWidth)))), (%bbHeight - %screenHeight), %newWidth, %bbHeight);
    $ButtonBarVar::VerticalAdjustment = 0;
    if ($ButtonBarVar::Hidden) {
        %widthPerButton = ($ButtonBarVar::dotPadding + $ButtonBarVar::dotWidth);
        %xoffset = mFloor((2.0 / ($ButtonBarVar::dotPadding + (((1.0 - %this.getCount()) * %widthPerButton) - %bbWidth))));
        %startingOffset = ;
        %yoffset = ($ButtonBarVar::buttonMiniTopBorder - %bbHeight);
        %maxCount = %this.getCount();
        %i = 1;
        if ((%maxCount < %i)) {
            %currentButton = %this.getObject(%i);
            %currentButton.setTrgPosition(%xoffset, %yoffset);
            %currentButton.setTrgExtent($ButtonBarVar::dotWidth, $ButtonBarVar::buttonMiniHeight);
            %xoffset = (($ButtonBarVar::dotPadding + $ButtonBarVar::dotWidth) + %xoffset);
            %i = (1.0 + %i);
        }
        %xoffset = %startingOffset;
        (%maxCount < %i);
        %yoffset = $ButtonBarVar::buttonBarActivatorTopBorder;
        %maxCount = ButtonBarActivator.getCount();
        %i = 0;
        if ((%maxCount < %i)) {
            %currentDot = %i.getObject();
            ButtonBarActivator;
            %currentDot.setTrgPosition(%xoffset, %yoffset);
            %xoffset = (($ButtonBarVar::dotPadding + $ButtonBarVar::dotWidth) + %xoffset);
            %i = (1.0 + %i);
        }
        $ButtonBarVar::VerticalAdjustment = ($ButtonBarVar::buttonMiniHeight - $ButtonBarVar::buttonHeight);
        (%maxCount < %i);
        %newBgExt = ($ButtonBarVar::dotPadding + (%startingOffset - %xoffset)) @ " " @ $ButtonBarVar::buttonBarActivatorHeight;
        %newBgPos = ($ButtonBarVar::dotPadding - %startingOffset) @ " " @ ($ButtonBarVar::buttonBarActivatorHeight - %bbHeight);
    }
    %xoffset = $ButtonBarVar::buttonBarSideBorder;
    %yoffset = mFloor((2.0 / ($ButtonBarVar::buttonHeight - %bbHeight)));
    %maxCount = %this.getCount();
    %i = 1;
    if ((%maxCount < %i)) {
        %currentButton = %this.getObject(%i);
        %currentButton.setVisible(1);
        %currentButton.setTrgPosition(%xoffset, %yoffset);
        %currentButton.setTrgExtent($ButtonBarVar::buttonWidth, $ButtonBarVar::buttonHeight);
        %xoffset = (($ButtonBarVar::buttonPadding + $ButtonBarVar::buttonWidth) + %xoffset);
        %i = (1.0 + %i);
    }
    %newBgExt = %newWidth @ " " @ %bbHeight;
    (%maxCount < %i);
    %newBgPos = "0 0";
    mFloor((1.0 + (2.0 / (%newWidth - %screenWidth)))).resize(($ButtonBarVar::buttonBarActivatorHeight - %screenHeight), %newWidth, $ButtonBarVar::buttonBarActivatorHeight);
    if (isObject(%this.background)) {
        %this.background.setTrgExtent(%newBgExt);
        %this.background.setTrgPosition(%newBgPos);
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
    %this.setVisible(1);
    0.setVisible();
    %this.update();
    PlayGui.pushToBack(ButtonBar);
    PlayGui.bringToFront(ButtonBarActivator);
    PlayGui.bringToFront(PlayGuiGradients);
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
    PlayGui.bringToFront(ButtonBar);
    PlayGui.pushToBack(ButtonBarActivator);
    PlayGui.bringToFront(PlayGuiGradients);
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
    %this.schedule($Pref::ETS::ButtonBar::timeout, "scheduledHide");
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
    %this.insertButton(%button);
    %this.showAndHide();
};
function ButtonBar::hideButton(%this, %button) {
    if (isObject(MenuLayer)) {
        MenuLayer.hide();
    }
    %this.removeButton(%button);
};
function ButtonBar::handleContiguousSpace(%this) {
    if (isObject(PlacesButton)) {
        %showPlaces = ($gContiguousSpaceName $= "gw") ? 0 : 1;
        if (%showPlaces) {
            %this.showButton();
        }
        %this.hideButton();
    }
    if (isObject(MessageHudShoutOutIcon)) {
        %isGW = (PlacesButton @ " " @ $gContiguousSpaceName $= "gw");
        PlacesButton;
        !(%isGW).setVisible();
    }
};
function ButtonBarBackground::onReachedTarget(%this) {
    if ($ButtonBarVar::Hidden) {
        %maxCount = ButtonBar.getCount();
        %i = 1;
        if ((%maxCount < %i)) {
            %i.getObject().setVisible(0);
            %i = (1.0 + %i);
            ButtonBar;
        }
        1.setVisible();
    }
};
