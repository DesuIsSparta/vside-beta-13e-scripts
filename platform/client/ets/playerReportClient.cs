function ReportAbuseDlg::open(%this, %targetName) {
    0.pushDialog(Canvas, %this);
    if ((%targetName $= "")) {
        %targetName = "foo";
    }
    %this.targetName = %targetName;
    pushScreenSize(640, 363, 0, 1, 1);
    1.setVisible(%this);
    %this.init();
};
function ReportAbuseDlg::init(%this) {
    AbuseTypePopup.clear();
    0.add(AbuseTypePopup, "Please Select");
    "Inappropriate/Offensive Comment".add(AbuseTypePopup);
    "Racism/Hate Speech".add(AbuseTypePopup);
    "Profanity".add(AbuseTypePopup);
    "Cyber Bullying".add(AbuseTypePopup);
    "Stalker".add(AbuseTypePopup);
    "Underage User".add(AbuseTypePopup);
    "Banned User using Different Account".add(AbuseTypePopup);
    "Other".add(AbuseTypePopup);
    0.SetSelected(AbuseTypePopup);
    OccurrencePopup.clear();
    0.add(OccurrencePopup, "Please Select");
    "First Offense".add(OccurrencePopup);
    "Repeat Offender".add(OccurrencePopup);
    0.SetSelected(OccurrencePopup);
    "".setText(ReportDescription);
    %boldFont = "<font:Arial Bold:14>";
    %bigBoldFont = "<font:Arial Bold:16>";
    %italicFont = "<font:Arial Italic:14>";
    %redText = "<color:ff0000>";
    "<spush><just:center>" @ %boldFont @ %redText @ "YOU ARE ABOUT TO REPORT ABUSE AGAINST " @ %this.targetName @ ".<spop>" @ "\n" @ "" @ "\n" @ "<just:left>Reporting abuse is a serious matter.  Abuse is defined as violations of the" @ "\n" @ "<a:" @ $Net::HelpURL_Guidelines @ ">vSide House Rules</a> or <a:" @ $Net::TermsOfUseURL @ ">Terms of Use.</a>" @ "\n" @ "" @ "\n" @ "Recent chat from your chat bubble will be sent to the Moderation team." @ "\n" @ "" @ "\n" @ "Reporter: <spush>" @ %italicFont @ $Player::Name @ "<spop>" @ "\n" @ "Abuser: <spush>" @ %italicFont @ %this.targetName @ "<spop>" @ "\n" @ "".setText(ReportText);
    "Report Abuse Against " @ %this.targetName.setText(ReportAbuseFrame);
};
function ReportAbuseDlg::close(%this) {
    popScreenSize();
    %this.popDialog(Canvas);
};
function ReportAbuseDlg::report(%this) {
    %occurrence = OccurrencePopup.GetSelected().getTextById(OccurrencePopup);
    %abuseType = AbuseTypePopup.GetSelected().getTextById(AbuseTypePopup);
    %desc = ReportDescription.getText();
    if ((%occurrence $= "Please Select")) {
    }
    if ((%abuseType $= "Please Select")) {
    }
    if ((%desc $= "")) {
        MessageBoxOK("Error", $MsgCat::abuse["E-ABUSE-TYPE"], "");
        return;
    }
    %messageVector = ConvBubVecCtrl.getAttached();
    if (isObject(%messageVector)) {
        echo("valid message vector");
        200.dumpToFile(%messageVector, "./chatbub.txt", "");
    }
    echo("creating dummy message vector");
    %messageVector = new MessageVector("");
    "./chatbub.txt".dumpToFile(%messageVector);
    %messageVector.delete();
    %request = sendRequest_AbuseReport(%this.targetName, stripUnprintables(ReportDescription.getText()), %occurrence, %abuseType, "./chatBub.txt", "onDoneOrErrorCallback_AbuseReport");
    %request.targetName = %this.targetName;
    %request.dlg = MessageBoxOK("Reporting Abuse", "Your abuse report is being sent..", "");
    %this.close();
};
function onDoneOrErrorCallback_AbuseReport(%request) {
    if (%request.checkSuccess()) {
        if (!($CSSpaceName $= "")) {
            MessageBoxOK("Report Abuse", $MsgCat::abuse["ABUSE-MSG-FROM-PRIVATE-SPACE"], "");
        }
        MessageBoxOK("Report Abuse", $MsgCat::abuse["ABUSE-MSG"], "");
    }
    MessageBoxOK("Server Unavailable", $MsgCat::network["E-SERVER-UNAVAIL"], "");
    commandToServer('NotifyAbuseReport', %request.targetName, getSubStr(ReportDescription.getText(), 0, 64));
    $gSecondsToWaitBetweenReportAbuseAndUnignore = (10.0 * 60.0);
    safeEnsureScriptObjectWithInit("StringMap", "cantUnignoreList", "{ ignoreCase = true; }");
    (getSimTime() + ($gSecondsToWaitBetweenReportAbuseAndUnignore * 1000.0)).put(cantUnignoreList, %request.targetName);
    %request.dlg.close();
    if (isFile("./chatbub.txt")) {
        deleteFile("./chatbub.txt");
    }
};
function doUserReport(%targetName, %reportType) {
    ReportAbuseDlg.targetName = %targetName;
    if ((%reportType $= "abuse")) {
        %ignored = %targetName.getIgnoreStatus(BuddyHudWin);
        if (!(%ignored)) {
            %dlg = MessageBoxCustom("WARNING", "You must ignore " @ %targetName @ " before you can report abuse against them.\nWould you like to report abuse against " @ %targetName @ " now?", "No, just ignore" @ "\t" @ "Yes, ignore and report abuse" @ "\t" @ "Cancel");
            %dlg.callback = "doUserIgnore(\"" @ %targetName @ "\", \"add\");" @ 0;
            %dlg.callback = "doUserIgnore(\"" @ %targetName @ "\", \"add\"); ReportAbuseDlg.open(\"" @ %targetName @ "\"); " @ %dlg.getId() @ ".close();" @ 1;
            %dlg.callback = "" @ 2;
        }
        %targetName.open(ReportAbuseDlg);
    }
};
