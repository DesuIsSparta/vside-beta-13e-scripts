function LoadingGui::onAdd(%this)
{
    %this.qLineCount = 0;
}
function LoadingGui::onWake(%this)
{
    $Platform::CanSleepInBackground = 0;
    ShowAllMessageBoxes();
    if (!isObject(LoadingPBController))
    {
        new ScriptObject(LoadingPBController) {
            class = "ProgressBarController";
        };
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(LoadingPBController);
        }
    }
    LoadingPBController.Initialize(LoadingProgressHolder, "platform/client/ui/progress_empty", "platform/client/ui/progress_fill", "", "");
    LoadingTipsHud.initTipsList();
    %this.doTheTipThing();
    %this.updateLogoutButton();
    if ($StandAlone)
    {
    }
    if (!$missionRunning)
    {
        error(getScopeName() @ " " @ "-" @ " " @ $missionRunning[$MsgCat::loading @ "E-MISSION-LD"] @ " " @ $MissionArg @ " " @ getTrace());
        MessageBoxOK("Error", $MsgCat::loading["E-MISSION-LD"] @ " " @ $MissionArg, "quit();");
    }
}
function LoadingGui::updateLogoutButton(%this)
{
    %windowWidth = getWord(getRes(), 0);
    %width = getWord(LoadingLogoutButton.getExtent(), 0);
    %ypos = getWord(LoadingLogoutButton.getPosition(), 1);
    %rightMarginPos = %windowWidth - WindowManager.getRightMarginAtY(%ypos);
    %padding = 38;
    LoadingLogoutButton.reposition(((%rightMarginPos - %width) - %padding) @ " " @ %ypos);
}
function LoadingGui::setTransitioning(%this, %flag)
{
    %this.transitioning = %flag;
    %this.updateLogoutButton();
    if (%flag)
    {
        LoadingTipsHud.setVisible(0);
        LoadingCenterFrame.add(LoadingProgressBrackets);
        LoadingCenterFrame.add(LoadingProgressHolder);
        LoadingCenterFrame.add(LoadingProgressText);
    }
    else
    {
        LoadingBottomRightFrame.add(LoadingProgressBrackets);
        LoadingBottomRightFrame.add(LoadingProgressHolder);
        LoadingBottomRightFrame.add(LoadingProgressText);
    }
}
function LoadingGui::doTheTipThing(%this)
{
    cancel($SCHEDULE_SHOWANOTHER);
    %resWidth = getWord($UserPref::Video::Resolution, 0);
    if (%resWidth < 640)
    {
        LoadingTipsHud.setVisible(0);
    }
    else
    {
        LoadingTipsHud.loadATip();
    }
    $SCHEDULE_SHOWANOTHER = %this.schedule($SCHEDULE_TIPTIMEDELAY, "doTheTipThing");
}
function LoadingGui::onSleep(%this)
{
    cancel($SCHEDULE_SHOWANOTHER);
    $Platform::CanSleepInBackground = 1;
    if (!(%this.qLineCount $= ""))
    {
        %line = 0;
        while (%line < %this.qLineCount)
        {
            %this.qLine[%line] = "";
            %line = %line + 1;
        }
    }
    %this.qLineCount = (%line < %this.qLineCount) @ 0;
    LoadingProgressTxt.setValue("");
    LoadingPBController.setValue(0);
}
function LoadingGui::onCanvasResize(%this)
{
    LoadingGui.doTheTipThing();
    %this.updateLogoutButton();
}
function TipsWhileLoadingImage::onMouseDown(%this)
{
    LoadingGui.doTheTipThing();
}
function TipTextScrollCtrl::onMouseDown(%this)
{
    LoadingGui.doTheTipThing();
}
function LoadingTipsHud::onMouseDown(%this)
{
    LoadingGui.doTheTipThing();
}
$TIP_CATEGORY = "ADVANCED";
if ($UserPref::userTips::tipSeen[LOADINGTIPS_TIMES_RUN] < 2)
{
    $TIP_CATEGORY = "NEWBIE";
}
else
{
    if ($UserPref::userTips::tipSeen[LOADINGTIPS_TIMES_RUN] < 5)
    {
        $TIP_CATEGORY = "MEDIUM";
    }
}
$UserPref::userTips::tipSeen[LOADINGTIPS_TIMES_RUN] = $UserPref::userTips::tipSeen[LOADINGTIPS_TIMES_RUN] + 1;
$SCHEDULE_TIPTIMEDELAY = 8000;
$SCHEDULE_SHOWANOTHER = 0;
function LoadingTipsHud::initTipsList(%this)
{
    %projectTipsDir = "projects/" @ $ETS::ProjectName @ "/tips";
    %testImageSpec = %projectTipsDir @ "/*testing.png";
    %file = findFirstFile(%testImageSpec);
    if (!(%file $= ""))
    {
        if (%this.loadTipImage(%file))
        {
        }
        if (!LoadingGui.transitioning)
        {
            LoadingTipsHud.setVisible(1);
        }
        warn("LoadingTipsHud loading override tip file" @ " " @ %file);
        MessageBoxOK("Warning", "loading override test tip file:" @ " " @ %file, "");
        return;
    }
    %base_path = %projectTipsDir @ "/";
    %tips_fileName = %base_path @ "tips.txt";
    %fo = new FileObject("");
    if (!%fo.openForRead(%tips_fileName))
    {
        error("Could not open" @ " " @ %tips_fileName);
        %this.tipFileCount = %fileCount;
        return;
    }
    %fileCount = 0;
    while (!%fo.isEOF())
    {
        %file = %fo.readLine();
        if (strstr(%file, $TIP_CATEGORY) == -(1))
        {
        }
        else
        {
            %this.tipFile[%file,%fileCount] = %base_path;
            %this.tipFileShown[%fileCount] = 0;
            %fileCount = %fileCount + 1;
        }
    }
    %fo.close();
    %this.tipFileCount = !%fo.isEOF() @ %fileCount;
    if (%fileCount == 0)
    {
        echo("No tips found. We will now stop loading them. Add some and run again");
        LoadingTipsHud.setVisible(0);
        return;
    }
}
function LoadingTipsHud::loadATip(%this)
{
    if (%this.tipFileCount == 0)
    {
        return;
    }
    %tipNum = getRandom(0, (%this.tipFileCount - 1));
    %n = 0;
    if (%n < 10)
    {
    }
    while (%this.tipFileShown[%tipNum] == 1)
    {
        %tipNum = getRandom(0, (%this.tipFileCount - 1));
        %n = %n + 1;
        if (%n < 10)
        {
        }
    }
    %fileName = %this.tipFile[%tipNum];
    %this.tipFileShown[%tipNum] == 1;
    if (%fileName $= "")
    {
        error("Got a bad tip filename. Skipping...");
        LoadingTipsHud.setVisible(0);
        return;
    }
    if (%this.loadTipImage(%fileName))
    {
    }
    if (!LoadingGui.transitioning)
    {
        LoadingTipsHud.setVisible(1);
        %this.tipFileShown[%tipNum] = 1;
    }
}
function LoadingTipsHud::loadTipImage(%this, %fileName)
{
    if (%fileName $= "")
    {
        return;
    }
    if (!isFile(%fileName))
    {
        %url = $Net::downloadURL @ "/packages/" @ %fileName;
        %this.downloadAndApplyBitmap(%url);
        return;
    }
    %this.setBitmap(%fileName);
}
function LoadingTipsHud::setBitmap(%this, %fileName)
{
    if (%fileName $= "")
    {
        return;
    }
    TipsWhileLoadingImage.setBitmap(%fileName);
}
