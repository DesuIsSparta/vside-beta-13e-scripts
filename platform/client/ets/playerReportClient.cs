function ReportAbuseDlg::open(%this, %targetName)
{
    Canvas.pushDialog(%this, 0);
    if (%targetName $= "")
    {
        %targetName = "foo";
    }
    %this.targetName = %targetName;
    pushScreenSize(640, 363, 0, 1, 1);
    %this.setVisible(1);
    %this.init();
    return ;
}
function ReportAbuseDlg::init(%this)
{
    AbuseTypePopup.clear();
    AbuseTypePopup.add("Please Select", 0);
    AbuseTypePopup.add("Inappropriate/Offensive Comment");
    AbuseTypePopup.add("Racism/Hate Speech");
    AbuseTypePopup.add("Profanity");
    AbuseTypePopup.add("Cyber Bullying");
    AbuseTypePopup.add("Stalker");
    AbuseTypePopup.add("Underage User");
    AbuseTypePopup.add("Banned User using Different Account");
    AbuseTypePopup.add("Other");
    AbuseTypePopup.SetSelected(0);
    OccurrencePopup.clear();
    OccurrencePopup.add("Please Select", 0);
    OccurrencePopup.add("First Offense");
    OccurrencePopup.add("Repeat Offender");
    OccurrencePopup.SetSelected(0);
    ReportDescription.setText("");
    %boldFont = "<font:Arial Bold:14>";
    %bigBoldFont = "<font:Arial Bold:16>";
    %italicFont = "<font:Arial Italic:14>";
    %redText = "<color:ff0000>";
    ReportText.setText("<spush><just:center>" @ %boldFont @ %redText @ "YOU ARE ABOUT TO REPORT ABUSE AGAINST " @ %this.targetName @ ".<spop>" NL "" NL "<just:left>Reporting abuse is a serious matter.  Abuse is defined as violations of the" NL "<a:" @ $Net::HelpURL_Guidelines @ ">vSide House Rules</a> or <a:" @ $Net::TermsOfUseURL @ ">Terms of Use.</a>" NL "" NL "Recent chat from your chat bubble will be sent to the Moderation team." NL "" NL "Reporter: <spush>" @ %italicFont @ $Player::Name @ "<spop>" NL "Abuser: <spush>" @ %italicFont @ %this.targetName @ "<spop>" NL "");
    ReportAbuseFrame.setText("Report Abuse Against " @ %this.targetName);
    return ;
}
function ReportAbuseDlg::close(%this)
{
    popScreenSize();
    Canvas.popDialog(%this);
    return ;
}
function ReportAbuseDlg::report(%this)
{
    %occurrence = OccurrencePopup.getTextById(OccurrencePopup.GetSelected());
    %abuseType = AbuseTypePopup.getTextById(AbuseTypePopup.GetSelected());
    %desc = ReportDescription.getText();
    if (((%occurrence $= "Please Select") || (%abuseType $= "Please Select")) || (%desc $= ""))
    {
        MessageBoxOK("Error", $MsgCat::abuse["E-ABUSE-TYPE"], "");
        return ;
    }
    %messageVector = ConvBubVecCtrl.getAttached();
    if (isObject(%messageVector))
    {
        echo("valid message vector");
        %messageVector.dumpToFile("./chatbub.txt", "", 200);
    }
    else
    {
        echo("creating dummy message vector");
        %messageVector = new MessageVector();
        %messageVector.dumpToFile("./chatbub.txt");
        %messageVector.delete();
    }
    %request = sendRequest_AbuseReport(%this.targetName, stripUnprintables(ReportDescription.getText()), %occurrence, %abuseType, "./chatBub.txt", "onDoneOrErrorCallback_AbuseReport");
    %request.targetName = %this.targetName;
    %request.dlg = MessageBoxOK("Reporting Abuse", "Your abuse report is being sent..", "");
    %this.close();
    return ;
}
function onDoneOrErrorCallback_AbuseReport(%request)
{
    if (%request.checkSuccess())
    {
        if (!($CSSpaceName $= ""))
        {
            MessageBoxOK("Report Abuse", $MsgCat::abuse["ABUSE-MSG-FROM-PRIVATE-SPACE"], "");
        }
        else
        {
            MessageBoxOK("Report Abuse", $MsgCat::abuse["ABUSE-MSG"], "");
        }
    }
    else
    {
        MessageBoxOK("Server Unavailable", $MsgCat::network["E-SERVER-UNAVAIL"], "");
    }
    commandToServer('NotifyAbuseReport', %request.targetName, getSubStr(ReportDescription.getText(), 0, 64));
    $gSecondsToWaitBetweenReportAbuseAndUnignore = 10 * 60;
    safeEnsureScriptObjectWithInit("StringMap", "cantUnignoreList", "{ ignoreCase = true; }");
    cantUnignoreList.put(%request.targetName, getSimTime() + ($gSecondsToWaitBetweenReportAbuseAndUnignore * 1000));
    %request.dlg.close();
    if (isFile("./chatbub.txt"))
    {
        deleteFile("./chatbub.txt");
    }
    return ;
}
function doUserReport(%targetName, %reportType)
{
    ReportAbuseDlg.targetName = %targetName;
    if (%reportType $= "abuse")
    {
        %ignored = BuddyHudWin.getIgnoreStatus(%targetName);
        if (!%ignored)
        {
            %dlg = MessageBoxCustom("WARNING", "You must ignore " @ %targetName @ " before you can report abuse against them.\nWould you like to report abuse against " @ %targetName @ " now?", "No, just ignore" TAB "Yes, ignore and report abuse" TAB "Cancel");
            %dlg.callback[0] = "doUserIgnore(\"" @ %targetName @ "\", \"add\");";
            %dlg.callback[1] = "doUserIgnore(\"" @ %targetName @ "\", \"add\"); ReportAbuseDlg.open(\"" @ %targetName @ "\"); " @ %dlg.getId() @ ".close();";
            %dlg.callback[2] = "";
        }
        else
        {
            ReportAbuseDlg.open(%targetName);
        }
    }
    return ;
}
