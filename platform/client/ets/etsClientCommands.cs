function setHighFidelityCull(%on) {
    if (%on) {
        $pref::Player::highFidelityCullMask = $TypeMasks::InteriorObjectType;
    }
    $pref::Player::highFidelityCullMask = 0;
};
$closeConfirmDlg = 0;
function onAppCloseButton() {
    commandToServer('SetLookAt', -(1.0), 0, 0);
    if (isObject($closeConfirmDlg)) {
        %isShowingNow = $closeConfirmDlg.visible;
        $closeConfirmDlg.close();
        if (%isShowingNow) {
            confirmQuitOnYes();
            return;
        }
    }
    %noCmd = "";
    if ($ConsoleActive) {
        ToggleConsole(1);
        %noCmd = "ToggleConsole(true);";
    }
    %dialog = $closeConfirmDlg = MessageBoxYesNo("Quit vSide", $MsgCat::login["CONF-QUIT"], "confirmQuitOnYes();", %noCmd @ " " @ "confirmQuitOnNo ();");
    if (!($gLastLoggedInThisSessionAs $= "")) {
        %yesButtonPos = %dialog.button.getParent(0).getPosition();
        %ctrl = new GuiCheckBoxCtrl("") {
            profile = "ETSCheckBoxProfile";
            position = getWord(%yesButtonPos, 0) @ " " @ (getWord(%yesButtonPos, 1) - 23.0);
            extent = "110 20";
            horizSizing = "center";
            vertSizing = "top";
            text = "Visit my web profile";
        };
        $UserPref::General::onQuitVisitWebProfile.setValue(%ctrl);
        %window = %dialog.window;
        %ctrl.add(%window);
        %dialog.visitProfileOptionCtrl = %ctrl;
        %width = getWord(%window.getExtent(), 0);
        %height = getWord(%window.getExtent(), 1);
        (%height + 20.0).resize(%window, %width);
    }
};
function confirmQuitOnYes() {
    if (!($gLastLoggedInThisSessionAs $= "")) {
        $UserPref::General::onQuitVisitWebProfile = $closeConfirmDlg.visitProfileOptionCtrl.getValue();
        if ($UserPref::General::onQuitVisitWebProfile) {
            doUserProfile($gLastLoggedInThisSessionAs);
        }
    }
    cleanUpAndQuit();
};
function confirmQuitOnNo() {
    if (!($gLastLoggedInThisSessionAs $= "")) {
        $UserPref::General::onQuitVisitWebProfile = $closeConfirmDlg.visitProfileOptionCtrl.getValue();
    }
    $closeConfirmDlg = 0;
};
function cleanUpAndQuit() {
    if (isObject(ConsoleWindow)) {
        $UserPref::ETS::Console::Dim = ConsoleWindow.getPosition() @ " " @ ConsoleWindow.getExtent();
    }
    if (isObject(SnoopPanel)) {
        SnoopPanel.storeDims();
    }
    quit();
};
$gContiguousSpaceName = "";
function ClientCmdMissionInfo(%contiguousSpaceName, %mode) {
    $gMode = %mode;
    onGotContiguousSpaceName(%contiguousSpaceName);
};
function onGotContiguousSpaceName(%contiguousSpaceName) {
    $gContiguousSpaceName = %contiguousSpaceName;
    tutorials_Initialize();
    %contiguousSpaceName.onSpaceChange(geLocalMapContainer);
    CSControlPanelTabs.updateSkipTutorialTab();
    ButtonBar.handleContiguousSpace();
    if (!(%contiguousSpaceName $= "")) {
    }
    %name = "[" @ $ServerName @ "]";
    %contiguousSpaceName;
    1.incrementIntegerProperty(gUserPropMgrClient, $Player::Name, "level started count" @ " " @ %name);
};
$gContiguousSpaceFullNames[""] = "vSide";
$gContiguousSpaceOfferSkip[""] = 0;
$gContiguousSpaceFullNames["gw"] = "Gateway";
$gContiguousSpaceOfferSkip["gw"] = 1;
$gContiguousSpaceFullNames["lga"] = "LaGenoaAires";
$gContiguousSpaceOfferSkip["lga"] = 0;
$gContiguousSpaceFullNames["min"] = "Minimal";
$gContiguousSpaceOfferSkip["min"] = 1;
$gContiguousSpaceFullNames["minimal"] = "Minimal";
$gContiguousSpaceOfferSkip["minimal"] = 1;
$gContiguousSpaceFullNames["nv"] = "NewVenezia";
$gContiguousSpaceOfferSkip["nv"] = 0;
$gContiguousSpaceFullNames["rj"] = "RaiJuku";
$gContiguousSpaceOfferSkip["rj"] = 0;
function getContiguousSpaceFullName(%code) {
    return %code[$gContiguousSpaceFullNames @ %code];
};
function getCurrentContiguousSpaceFullName() {
    return getContiguousSpaceFullName($gContiguousSpaceName);
};
function getCurrentContiguousSpaceOfferSkip() {
    return $gContiguousSpaceName[$gContiguousSpaceOfferSkip @ $gContiguousSpaceName];
};
function ClientCmdLevelCompleted(%levelName) {
    1.setProperty(gUserPropMgrClient, $Player::Name, "level completed" @ " " @ %levelName);
};
function ClientCmdToonColorOffsetFill(%colorOffset) {
    $pref::TS::ToonColorOffsetFill = %colorOffset;
};
function ClientCmdToonColorOffsetEdge(%colorOffset) {
    $pref::TS::ToonColorOffsetEdge = %colorOffset;
};
function ClientCmdDoYouWantToOpenGiftBox(%boxID) {
    MessageBoxYesNo("A Gift Box", "Would you like to take this gift?", "onOpenGiftBoxYes(" @ %boxID @ ");", "");
};
function onOpenGiftBoxYes(%boxID) {
    commandToServer('OpenGiftBox', %boxID);
};
