function setHighFidelityCull(%on)
{
    if (%on)
    {
        $pref::Player::highFidelityCullMask = $TypeMasks::InteriorObjectType;
    }
    else
    {
        $pref::Player::highFidelityCullMask = 0;
    }
    return ;
}
$closeConfirmDlg = 0;
function onAppCloseButton()
{
    commandToServer('SetLookAt', -1, 0, 0);
    if (isObject($closeConfirmDlg))
    {
        %isShowingNow = $closeConfirmDlg.visible;
        $closeConfirmDlg.close();
        if (%isShowingNow)
        {
            confirmQuitOnYes();
            return ;
        }
    }
    %noCmd = "";
    if ($ConsoleActive)
    {
        ToggleConsole(1);
        %noCmd = "ToggleConsole(true);";
    }
    %dialog = $closeConfirmDlg = MessageBoxYesNo("Quit vSide", $MsgCat::login["CONF-QUIT"], "confirmQuitOnYes();", %noCmd SPC "confirmQuitOnNo ();");
    if (!($gLastLoggedInThisSessionAs $= ""))
    {
        %yesButtonPos = %dialog.button[0].getParent().getPosition();
        %ctrl = new GuiCheckBoxCtrl()
        {
            profile = "ETSCheckBoxProfile";
            position = getWord(%yesButtonPos, 0) SPC getWord(%yesButtonPos, 1) - 23;
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
        %window.resize(%width, %height + 20);
    }
    return ;
}
function confirmQuitOnYes()
{
    if (!($gLastLoggedInThisSessionAs $= ""))
    {
        $UserPref::General::onQuitVisitWebProfile = $closeConfirmDlg.visitProfileOptionCtrl.getValue();
        if ($UserPref::General::onQuitVisitWebProfile)
        {
            doUserProfile($gLastLoggedInThisSessionAs);
        }
    }
    cleanUpAndQuit();
    return ;
}
function confirmQuitOnNo()
{
    if (!($gLastLoggedInThisSessionAs $= ""))
    {
        $UserPref::General::onQuitVisitWebProfile = $closeConfirmDlg.visitProfileOptionCtrl.getValue();
    }
    $closeConfirmDlg = 0;
    return ;
}
function cleanUpAndQuit()
{
    if (isObject(ConsoleWindow))
    {
        $UserPref::ETS::Console::Dim = ConsoleWindow.getPosition() SPC ConsoleWindow.getExtent();
    }
    if (isObject(SnoopPanel))
    {
        SnoopPanel.storeDims();
    }
    quit();
    return ;
}
$gContiguousSpaceName = "";
function ClientCmdMissionInfo(%contiguousSpaceName, %mode)
{
    $gMode = %mode;
    onGotContiguousSpaceName(%contiguousSpaceName);
    return ;
}
function onGotContiguousSpaceName(%contiguousSpaceName)
{
    $gContiguousSpaceName = %contiguousSpaceName;
    tutorials_Initialize();
    geLocalMapContainer.onSpaceChange(%contiguousSpaceName);
    CSControlPanelTabs.updateSkipTutorialTab();
    ButtonBar.handleContiguousSpace();
    %name = !(%contiguousSpaceName $= "") ? %contiguousSpaceName : "[";
    gUserPropMgrClient.incrementIntegerProperty($Player::Name, "level started count" SPC %name, 1);
    return ;
}
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
function getContiguousSpaceFullName(%code)
{
    return $gContiguousSpaceFullNames[%code];
}
function getCurrentContiguousSpaceFullName()
{
    return getContiguousSpaceFullName($gContiguousSpaceName);
}
function getCurrentContiguousSpaceOfferSkip()
{
    return $gContiguousSpaceOfferSkip[$gContiguousSpaceName];
}
function ClientCmdLevelCompleted(%levelName)
{
    gUserPropMgrClient.setProperty($Player::Name, "level completed" SPC %levelName, 1);
    return ;
}
function ClientCmdToonColorOffsetFill(%colorOffset)
{
    $pref::TS::ToonColorOffsetFill = %colorOffset;
    return ;
}
function ClientCmdToonColorOffsetEdge(%colorOffset)
{
    $pref::TS::ToonColorOffsetEdge = %colorOffset;
    return ;
}
function ClientCmdDoYouWantToOpenGiftBox(%boxID)
{
    MessageBoxYesNo("A Gift Box", "Would you like to take this gift?", "onOpenGiftBoxYes(" @ %boxID @ ");", "");
    return ;
}
function onOpenGiftBoxYes(%boxID)
{
    commandToServer('OpenGiftBox', %boxID);
    return ;
}
