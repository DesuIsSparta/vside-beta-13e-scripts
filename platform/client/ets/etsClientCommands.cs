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
    $closeConfirmDlg = MessageBoxYesNo("Quit vSide", , "confirmQuitOnYes();", %noCmd @ " " @ "confirmQuitOnNo ();");
    %dialog = ;
    if (!($gLastLoggedInThisSessionAs $= "")) {
        %yesButtonPos = 0.getParent(%dialog.button).getPosition();
        %ctrl = new GuiCheckBoxCtrl("") {
            profile = 0 @ "ETSCheckBoxProfile";
            position = getWord(%yesButtonPos, 0) @ " " @ (23.0 - getWord(%yesButtonPos, 1));
            extent = "110 20";
            horizSizing = "center";
            vertSizing = "top";
            text = "Visit my web profile";
        };
        %ctrl.setValue($UserPref::General::onQuitVisitWebProfile);
        %window = %dialog.window;
        %window.add(%ctrl);
        %dialog.visitProfileOptionCtrl = %ctrl;
        %width = getWord(%window.getExtent(), 0);
        %height = getWord(%window.getExtent(), 1);
        %window.resize(%width, (20.0 + %height));
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
    geLocalMapContainer.onSpaceChange(%contiguousSpaceName);
    CSControlPanelTabs.updateSkipTutorialTab();
    ButtonBar.handleContiguousSpace();
    if (!(%contiguousSpaceName $= "")) {
    }
    %name = "[" @ $ServerName @ "]";
    %contiguousSpaceName;
    gUserPropMgrClient.incrementIntegerProperty($Player::Name, "level started count" @ " " @ %name, 1);
};
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
    gUserPropMgrClient.setProperty($Player::Name, "level completed" @ " " @ %levelName, 1);
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
