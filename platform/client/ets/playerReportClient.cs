function ReportAbuseDlg::open(%this, %targetName) {
    %this.pushDialog(0);
    if ((Canvas SPC %targetName $= "")) {
        %targetName = "foo";
    }
    targetName = %targetName @ %this;
    pushScreenSize(640, 363, 0, 1, 1);
    %this.setVisible(1);
    %this.init();
};
function ReportAbuseDlg::init(%this) {
    clear();
    "Please Select".add(0);
    "Inappropriate/Offensive Comment".add();
    "Racism/Hate Speech".add();
    "Profanity".add();
    "Cyber Bullying".add();
    "Stalker".add();
    "Underage User".add();
    "Banned User using Different Account".add();
    "Other".add();
    0.SetSelected();
    clear();
    "Please Select".add(0);
    "First Offense".add();
    "Repeat Offender".add();
    0.SetSelected();
    "".setText();
    %boldFont = "<font:Arial Bold:14>";
    ReportDescription;
    %bigBoldFont = "<font:Arial Bold:16>";
    OccurrencePopup;
    %italicFont = "<font:Arial Italic:14>";
    OccurrencePopup;
    %redText = "<color:ff0000>";
    OccurrencePopup;
    AbuseTypePopup @ AbuseTypePopup @ AbuseTypePopup @ AbuseTypePopup @ AbuseTypePopup @ AbuseTypePopup @ AbuseTypePopup @ AbuseTypePopup @ AbuseTypePopup @ AbuseTypePopup @ OccurrencePopup @ OccurrencePopup @ ReportText @ "<spush><just:center>" @ %boldFont @ %redText @ "YOU ARE ABOUT TO REPORT ABUSE AGAINST " @ %this @ targetName @ ".<spop>" @ "\n" @ "" @ "\n" @ "<just:left>Reporting abuse is a serious matter.  Abuse is defined as violations of the" @ "\n" @ "<a:" @ $Net::HelpURL_Guidelines @ ">vSide House Rules</a> or <a:" @ $Net::TermsOfUseURL @ ">Terms of Use.</a>" @ "\n" @ "" @ "\n" @ "Recent chat from your chat bubble will be sent to the Moderation team." @ "\n" @ "" @ "\n" @ "Reporter: <spush>" @ %italicFont @ $Player::Name @ "<spop>" @ "\n" @ "Abuser: <spush>" @ %italicFont @ %this @ targetName @ "<spop>" @ "\n" @ "".setText();
    %this @ targetName.setText();
};
function ReportAbuseDlg::close(%this) {
    popScreenSize();
    %this.popDialog();
};
function ReportAbuseDlg::report(%this) {
    %occurrence = GetSelected().getTextById();
    OccurrencePopup;
    %abuseType = GetSelected().getTextById();
    AbuseTypePopup;
    %desc = getText();
    ReportDescription;
    if ((AbuseTypePopup SPC %occurrence $= "Please Select")) {
    }
    if ((OccurrencePopup SPC %abuseType $= "Please Select")) {
    }
    if ((%desc $= "")) {
        MessageBoxOK("Error", , "");
        return;
    }
    %messageVector = getAttached();
    ConvBubVecCtrl;
    if (isObject(%messageVector)) {
        echo("valid message vector");
        %messageVector.dumpToFile("./chatbub.txt", "", 200);
    }
    echo("creating dummy message vector");
    %messageVector = new ""();
    MessageVector;
    %messageVector.dumpToFile("./chatbub.txt");
    %messageVector.delete();
    %request = sendRequest_AbuseReport(targetName, stripUnprintables(getText()), %occurrence, %abuseType, "./chatBub.txt", "onDoneOrErrorCallback_AbuseReport");
    ReportDescription;
    targetName = %this @ targetName @ %request;
    %this;
    dlg = 0 @ MessageBoxOK("Reporting Abuse", "Your abuse report is being sent..", "") @ %request;
    %this.close();
};
function onDoneOrErrorCallback_AbuseReport(%request) {
    if (%request.checkSuccess()) {
        if (!($CSSpaceName $= "")) {
            MessageBoxOK("Report Abuse", , "");
        }
        MessageBoxOK("Report Abuse", , "");
    }
    MessageBoxOK("Server Unavailable", , "");
    commandToServer('NotifyAbuseReport', targetName, getSubStr(getText(), 0, 64));
    $gSecondsToWaitBetweenReportAbuseAndUnignore = (60.0 * 10.0);
    ReportDescription;
    safeEnsureScriptObjectWithInit("StringMap", "cantUnignoreList", "{ ignoreCase = true; }");
    targetName.put(((1000.0 * $gSecondsToWaitBetweenReportAbuseAndUnignore) + getSimTime()));
    dlg.close();
    if (isFile("./chatbub.txt")) {
        deleteFile("./chatbub.txt");
    }
};
function doUserReport(%targetName, %reportType) {
    targetName = %targetName @ ReportAbuseDlg;
    if ((%reportType $= "abuse")) {
        %ignored = %targetName.getIgnoreStatus();
        BuddyHudWin;
        if (!(%ignored)) {
            %dlg = MessageBoxCustom("WARNING", "You must ignore " @ %targetName @ " before you can report abuse against them.\nWould you like to report abuse against " @ %targetName @ " now?", "No, just ignore" @ "\t" @ "Yes, ignore and report abuse" @ "\t" @ "Cancel");
            callback = "doUserIgnore(\"" @ %targetName @ "\", \"add\");" @ 0 @ %dlg;
            callback = "doUserIgnore(\"" @ %targetName @ "\", \"add\"); ReportAbuseDlg.open(\"" @ %targetName @ "\"); " @ %dlg.getId() @ ".close();" @ 1 @ %dlg;
            callback = "" @ 2 @ %dlg;
        }
        %targetName.open();
    }
};
