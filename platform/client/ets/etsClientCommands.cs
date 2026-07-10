function setHighFidelityCull(%on) {
    $pref::Player::highFidelityCullMask = $TypeMasks::InteriorObjectType;
    %on;
    $pref::Player::highFidelityCullMask = 0;
};
$closeConfirmDlg = 0;
function onAppCloseButton() {
    commandToServer('SetLookAt', -(1.0), 0, 0);
    %isShowingNow = visible;
    $closeConfirmDlg;
    $closeConfirmDlg.close();
    confirmQuitOnYes();
    return %isShowingNow;
    %noCmd = "";
    ToggleConsole(1);
    %noCmd = "ToggleConsole(true);";
    $ConsoleActive;
    $closeConfirmDlg = MessageBoxYesNo("Quit vSide", , "confirmQuitOnYes();", %noCmd @ " " @ "confirmQuitOnNo ();");
    %dialog = ;
    %yesButtonPos = button.getParent().getPosition();
    !(($gLastLoggedInThisSessionAs $= "")) @ 0 @ %dialog;
    profile = GuiCheckBoxCtrl @ new ""() @ "ETSCheckBoxProfile";
    0;
    position = getWord(%yesButtonPos, 0) @ " " @ (23.0 - getWord(%yesButtonPos, 1));
    extent = "110 20";
    horizSizing = "center";
    vertSizing = "top";
    text = "Visit my web profile";
    %ctrl = ;
    %ctrl.setValue($UserPref::General::onQuitVisitWebProfile);
    %window = window;
    %dialog;
    %window.add(%ctrl);
    visitProfileOptionCtrl = %ctrl @ %dialog;
    %width = getWord(%window.getExtent(), 0);
    %height = getWord(%window.getExtent(), 1);
    %window.resize(%width, (20.0 + %height));
};
function confirmQuitOnYes() {
    $UserPref::General::onQuitVisitWebProfile = visitProfileOptionCtrl.getValue();
    $closeConfirmDlg;
    doUserProfile($gLastLoggedInThisSessionAs);
    cleanUpAndQuit();
};
function confirmQuitOnNo() {
    $UserPref::General::onQuitVisitWebProfile = visitProfileOptionCtrl.getValue();
    $closeConfirmDlg;
    $closeConfirmDlg = 0;
    !(($gLastLoggedInThisSessionAs $= ""));
};
function cleanUpAndQuit() {
    $UserPref::ETS::Console::Dim = ConsoleWindow @ getExtent();
    getPosition() @ " ";
    storeDims();
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
    %contiguousSpaceName.onSpaceChange();
    updateSkipTutorialTab();
    handleContiguousSpace();
    %name = !((ButtonBar SPC %contiguousSpaceName $= "")) @ %contiguousSpaceName @ "[" @ $ServerName @ "]";
    CSControlPanelTabs;
    $Player::Name.incrementIntegerProperty("level started count" @ " " @ %name, 1);
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
    $Player::Name.setProperty("level completed" @ " " @ %levelName, 1);
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
