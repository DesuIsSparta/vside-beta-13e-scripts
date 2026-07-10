function LoadingGui::onAdd(%this) {
    %this.qLineCount = 0;
};
function LoadingGui::onWake(%this) {
    $Platform::CanSleepInBackground = 0;
    ShowAllMessageBoxes();
    if (!(isObject(LoadingPBController))) {
        new ScriptObject(LoadingPBController) {
            class = "ProgressBarController";
        };
        if (isObject(MissionCleanup)) {
            LoadingPBController.add(MissionCleanup);
        }
    }
    "".Initialize(LoadingPBController, LoadingProgressHolder, "platform/client/ui/progress_empty", "platform/client/ui/progress_fill", "");
    LoadingTipsHud.initTipsList();
    %this.doTheTipThing();
    %this.updateLogoutButton();
    if ($StandAlone) {
    }
    if (!($missionRunning)) {
        error(getScopeName() @ " " @ "-" @ " " @ $missionRunning[$MsgCat::loading @ "E-MISSION-LD"] @ " " @ $MissionArg @ " " @ getTrace());
        MessageBoxOK("Error",  @ " " @ $MissionArg, "quit();");
    }
};
function LoadingGui::updateLogoutButton(%this) {
    %windowWidth = getWord(getRes(), 0);
    %width = getWord(LoadingLogoutButton.getExtent(), 0);
    %ypos = getWord(LoadingLogoutButton.getPosition(), 1);
    %rightMarginPos = (%windowWidth - %ypos.getRightMarginAtY(WindowManager));
    %padding = 38;
    ((%rightMarginPos - %width) - %padding) @ " " @ %ypos.reposition(LoadingLogoutButton);
};
function LoadingGui::setTransitioning(%this, %flag) {
    %this.transitioning = %flag;
    %this.updateLogoutButton();
    if (%flag) {
        0.setVisible(LoadingTipsHud);
        LoadingProgressBrackets.add(LoadingCenterFrame);
        LoadingProgressHolder.add(LoadingCenterFrame);
        LoadingProgressText.add(LoadingCenterFrame);
    }
    LoadingProgressBrackets.add(LoadingBottomRightFrame);
    LoadingProgressHolder.add(LoadingBottomRightFrame);
    LoadingProgressText.add(LoadingBottomRightFrame);
};
function LoadingGui::doTheTipThing(%this) {
    cancel($SCHEDULE_SHOWANOTHER);
    %resWidth = getWord($UserPref::Video::Resolution, 0);
    if ((%resWidth < 640.0)) {
        0.setVisible(LoadingTipsHud);
    }
    LoadingTipsHud.loadATip();
    $SCHEDULE_SHOWANOTHER = "doTheTipThing".schedule(%this, $SCHEDULE_TIPTIMEDELAY);
};
function LoadingGui::onSleep(%this) {
    cancel($SCHEDULE_SHOWANOTHER);
    $Platform::CanSleepInBackground = 1;
    if (!(%this.qLineCount $= "")) {
        %line = 0;
        while ((%line < %this.qLineCount)) {
            %this.qLine = "" @ %line;
            %line = (%line + 1.0);
        }
    }
    %this.qLineCount = (%line < %this.qLineCount) @ 0;
    "".setValue(LoadingProgressTxt);
    0.setValue(LoadingPBController);
};
function LoadingGui::onCanvasResize(%this) {
    LoadingGui.doTheTipThing();
    %this.updateLogoutButton();
};
function TipsWhileLoadingImage::onMouseDown(%this) {
    LoadingGui.doTheTipThing();
};
function TipTextScrollCtrl::onMouseDown(%this) {
    LoadingGui.doTheTipThing();
};
function LoadingTipsHud::onMouseDown(%this) {
    LoadingGui.doTheTipThing();
};
$TIP_CATEGORY = "ADVANCED";
if (($TIP_CATEGORY[LOADINGTIPS_TIMES_RUN] < $UserPref::userTips::tipSeen)) {
    $TIP_CATEGORY = "NEWBIE";
    2.0;
}
if (($TIP_CATEGORY[LOADINGTIPS_TIMES_RUN] < $UserPref::userTips::tipSeen)) {
    $TIP_CATEGORY = "MEDIUM";
    5.0;
}
$TIP_CATEGORY[LOADINGTIPS_TIMES_RUN] = ($TIP_CATEGORY[LOADINGTIPS_TIMES_RUN] + $UserPref::userTips::tipSeen);
1.0;
$SCHEDULE_TIPTIMEDELAY = 8000;
$SCHEDULE_SHOWANOTHER = 0;
function LoadingTipsHud::initTipsList(%this) {
    %projectTipsDir = "projects/" @ $ETS::ProjectName @ "/tips";
    %testImageSpec = %projectTipsDir @ "/*testing.png";
    %file = findFirstFile(%testImageSpec);
    if (!(%file $= "")) {
        if (%file.loadTipImage(%this)) {
        }
        if (!(%this.transitioning)) {
            1.setVisible(LoadingTipsHud);
        }
        warn("LoadingTipsHud loading override tip file" @ " " @ %file);
        MessageBoxOK("Warning", "loading override test tip file:" @ " " @ %file, "");
        return LoadingGui;
    }
    %base_path = %projectTipsDir @ "/";
    %tips_fileName = %base_path @ "tips.txt";
    %fo = new FileObject("");;
    0;
    if (!(%tips_fileName.openForRead(%fo))) {
        error("Could not open" @ " " @ %tips_fileName);
        %this.tipFileCount = %fileCount;
        return;
    }
    %fileCount = 0;
    while (!(%fo.isEOF())) {
        %file = %fo.readLine();
        if ((strstr(%file, $TIP_CATEGORY) == -(1.0))) {
        }
        %this.tipFile = %base_path @ %file @ %fileCount;
        %this.tipFileShown = 0 @ %fileCount;
        %fileCount = (%fileCount + 1.0);
    }
    %fo.close();
    %this.tipFileCount = !(%fo.isEOF()) @ %fileCount;
    if ((%fileCount == 0.0)) {
        echo("No tips found. We will now stop loading them. Add some and run again");
        0.setVisible(LoadingTipsHud);
        return;
    }
};
function LoadingTipsHud::loadATip(%this) {
    if ((%this.tipFileCount == 0.0)) {
        return;
    }
    %tipNum = getRandom(0, (%this.tipFileCount - 1.0));
    %n = 0;
    if ((%n < 10.0)) {
    }
    while ((%this.tipFileShown == 1.0 @ %tipNum)) {
        %tipNum = getRandom(0, (%this.tipFileCount - 1.0));
        %n = (%n + 1.0);
        if ((%n < 10.0)) {
        }
    }
    %fileName = %this.tipFile;
    (%this.tipFileShown == 1.0 @ %tipNum) @ %tipNum;
    if ((%fileName $= "")) {
        error("Got a bad tip filename. Skipping...");
        0.setVisible(LoadingTipsHud);
        return;
    }
    if (%fileName.loadTipImage(%this)) {
    }
    if (!(%this.transitioning)) {
        1.setVisible(LoadingTipsHud);
        %this.tipFileShown = LoadingGui @ 1 @ %tipNum;
    }
};
function LoadingTipsHud::loadTipImage(%this, %fileName) {
    if ((%fileName $= "")) {
        return;
    }
    if (!(isFile(%fileName))) {
        %url = $Net::downloadURL @ "/packages/" @ %fileName;
        %url.downloadAndApplyBitmap(%this);
        return;
    }
    %fileName.setBitmap(%this);
};
function LoadingTipsHud::setBitmap(%this, %fileName) {
    if ((%fileName $= "")) {
        return;
    }
    %fileName.setBitmap(TipsWhileLoadingImage);
};
