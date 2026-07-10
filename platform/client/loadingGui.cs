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
            MissionCleanup.add(LoadingPBController);
        }
    }
    "platform/client/ui/progress_empty".Initialize("platform/client/ui/progress_fill", "", "");
    LoadingPBController.initTipsList(LoadingTipsHud);
    LoadingPBController.doTheTipThing(%this);
    LoadingPBController.updateLogoutButton(%this);
    if ($StandAlone) {
    }
    if (!($missionRunning)) {
        error(getScopeName() @ " " @ "-" @ " " @ $missionRunning[$MsgCat::loading @ "E-MISSION-LD"] @ " " @ $MissionArg @ " " @ getTrace());
        MessageBoxOK("Error", LoadingProgressHolder @ " " @ $MissionArg, "quit();");
    }
};
function LoadingGui::updateLogoutButton(%this) {
    %windowWidth = getWord(getRes(), 0);
    %width = getWord(LoadingLogoutButton.getExtent(), 0);
    %ypos = getWord(LoadingLogoutButton.getPosition(), 1);
    %rightMarginPos = (%ypos.getRightMarginAtY() - %windowWidth);
    WindowManager;
    %padding = 38;
    (%padding - (%width - %rightMarginPos)) @ " " @ %ypos.reposition();
};
function LoadingGui::setTransitioning(%this, %flag) {
    %this.transitioning = %flag;
    %this.updateLogoutButton();
    if (%flag) {
        0.setVisible();
        LoadingCenterFrame.add(LoadingProgressBrackets);
        LoadingCenterFrame.add(LoadingProgressHolder);
        LoadingCenterFrame.add(LoadingProgressText);
    }
    LoadingBottomRightFrame.add(LoadingProgressBrackets);
    LoadingBottomRightFrame.add(LoadingProgressHolder);
    LoadingBottomRightFrame.add(LoadingProgressText);
};
function LoadingGui::doTheTipThing(%this) {
    cancel($SCHEDULE_SHOWANOTHER);
    %resWidth = getWord($UserPref::Video::Resolution, 0);
    if ((640.0 < %resWidth)) {
        0.setVisible();
    }
    LoadingTipsHud.loadATip();
    $SCHEDULE_SHOWANOTHER = %this.schedule($SCHEDULE_TIPTIMEDELAY, "doTheTipThing");
    LoadingTipsHud;
};
function LoadingGui::onSleep(%this) {
    cancel($SCHEDULE_SHOWANOTHER);
    $Platform::CanSleepInBackground = 1;
    if (!(%this.qLineCount $= "")) {
        %line = 0;
        if ((%this.qLineCount < %line)) {
            %this.qLine = "" @ %line;
            %line = (1.0 + %line);
        }
    }
    %this.qLineCount = (%this.qLineCount < %line) @ 0;
    "".setValue();
    0.setValue();
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
if (($UserPref::userTips::tipSeen < $TIP_CATEGORY[LOADINGTIPS_TIMES_RUN])) {
    $TIP_CATEGORY = "NEWBIE";
    2.0;
}
if (($UserPref::userTips::tipSeen < $TIP_CATEGORY[LOADINGTIPS_TIMES_RUN])) {
    $TIP_CATEGORY = "MEDIUM";
    5.0;
}
$TIP_CATEGORY[LOADINGTIPS_TIMES_RUN] = ($UserPref::userTips::tipSeen + $TIP_CATEGORY[LOADINGTIPS_TIMES_RUN]);
1.0;
$SCHEDULE_TIPTIMEDELAY = 8000;
$SCHEDULE_SHOWANOTHER = 0;
function LoadingTipsHud::initTipsList(%this) {
    %projectTipsDir = "projects/" @ $ETS::ProjectName @ "/tips";
    %testImageSpec = %projectTipsDir @ "/*testing.png";
    %file = findFirstFile(%testImageSpec);
    if (!(%file $= "")) {
        if (%this.loadTipImage(%file)) {
        }
        if (!(%this.transitioning)) {
            1.setVisible();
        }
        warn("LoadingTipsHud loading override tip file" @ " " @ %file);
        MessageBoxOK("Warning", "loading override test tip file:" @ " " @ %file, "");
        return LoadingTipsHud;
    }
    %base_path = %projectTipsDir @ "/";
    %tips_fileName = %base_path @ "tips.txt";
    %fo = new ""();;
    FileObject;
    if (!(%fo.openForRead(%tips_fileName))) {
        error("Could not open" @ " " @ %tips_fileName);
        %this.tipFileCount = 0 @ %fileCount;
        return;
    }
    %fileCount = 0;
    if (!(%fo.isEOF())) {
        %file = %fo.readLine();
        if ((-(1.0) == strstr(%file, $TIP_CATEGORY))) {
        }
        %this.tipFile = %base_path @ %file @ %fileCount;
        %this.tipFileShown = 0 @ %fileCount;
        %fileCount = (1.0 + %fileCount);
    }
    %fo.close();
    %this.tipFileCount = !(%fo.isEOF()) @ %fileCount;
    if ((0.0 == %fileCount)) {
        echo("No tips found. We will now stop loading them. Add some and run again");
        0.setVisible();
        return LoadingTipsHud;
    }
};
function LoadingTipsHud::loadATip(%this) {
    if ((0.0 == %this.tipFileCount)) {
        return;
    }
    %tipNum = getRandom(0, (1.0 - %this.tipFileCount));
    %n = 0;
    if ((10.0 < %n)) {
    }
    if ((1.0 @ %tipNum == %this.tipFileShown)) {
        %tipNum = getRandom(0, (1.0 - %this.tipFileCount));
        %n = (1.0 + %n);
        if ((10.0 < %n)) {
        }
    }
    %fileName = %this.tipFile;
    (1.0 @ %tipNum == %this.tipFileShown) @ %tipNum;
    if ((%fileName $= "")) {
        error("Got a bad tip filename. Skipping...");
        0.setVisible();
        return LoadingTipsHud;
    }
    if (%this.loadTipImage(%fileName)) {
    }
    if (!(%this.transitioning)) {
        1.setVisible();
        %this.tipFileShown = LoadingTipsHud @ 1 @ %tipNum;
        LoadingGui;
    }
};
function LoadingTipsHud::loadTipImage(%this, %fileName) {
    if ((%fileName $= "")) {
        return;
    }
    if (!(isFile(%fileName))) {
        %url = $Net::downloadURL @ "/packages/" @ %fileName;
        %this.downloadAndApplyBitmap(%url);
        return;
    }
    %this.setBitmap(%fileName);
};
function LoadingTipsHud::setBitmap(%this, %fileName) {
    if ((%fileName $= "")) {
        return;
    }
    %fileName.setBitmap();
};
