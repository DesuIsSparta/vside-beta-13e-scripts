$gDanceToolTimer = 0;
$gDanceToolTimerPeriod = 100;
$gDanceToolSequence = 0;
$gDanceToolTimeStart = 0;
function danceTool::open(%this) {
    %this.setVisible(1);
    %this.focusAndRaise();
    userTips::showOnceEver("DanceToolUsage");
    %this.initialcontent();
    if (!(isObject($gDanceToolSequence))) {
        $gDanceToolSequence = new ""();;
        StringMap;
        if (isObject(MissionCleanup)) {
            $gDanceToolSequence.add();
        }
    }
};
function danceTool::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    $gDanceToolTimer = 0;
    return 1;
};
function toggleDanceTool() {
    if (showDanceTool()) {
        PlayGui.showRaiseOrHide(danceTool);
    }
};
function danceTool::record(%this) {
    0.setVisible();
    0.setVisible();
    1.setVisible();
    "STOP (recording)".setText();
    0.setVisible();
    0.setVisible();
    %this.recording = guiDanceToolCheckBoxLoop @ 1;
    guiDanceToolButtonCopyFrom;
    %this.playing = guiDanceToolButtonStop @ 0;
    guiDanceToolButtonStop;
    %this.nextStep = guiDanceToolButtonPlay @ 0;
    guiDanceToolButtonRecord;
    %this.prevStepTime = -(1.0);
    "".setText();
    $player.getShapeName().setText();
    "my cool dance".setText();
    %this.setGender($player.getGender());
    %this.startTimer();
};
function danceTool::play(%this) {
    0.setVisible();
    0.setVisible();
    1.setVisible();
    "STOP (playing)".setText();
    0.setVisible();
    1.setVisible();
    %this.constructSequence(guiDanceToolMLTextBody.getText());
    %this.recording = guiDanceToolCheckBoxLoop @ 0;
    guiDanceToolButtonCopyFrom;
    %this.playing = guiDanceToolButtonStop @ 1;
    guiDanceToolButtonStop;
    %this.prevStep = guiDanceToolButtonPlay @ -(1.0);
    guiDanceToolButtonRecord;
    %this.startTimer();
};
function danceTool::stop(%this) {
    1.setVisible();
    1.setVisible();
    0.setVisible();
    1.setVisible();
    1.setVisible();
    commandToServer('DanceSequenceDone');
    if (%this.recording) {
        %this.finishRecordingPreviousStep();
    }
    %this.recording = guiDanceToolCheckBoxLoop @ 0;
    guiDanceToolButtonCopyFrom;
    %this.playing = guiDanceToolButtonStop @ 0;
    guiDanceToolButtonPlay;
    %this.stopTimer();
};
function danceTool::startTimer(%this) {
    cancel($gDanceToolTimer);
    $gDanceToolTimer = %this.schedule($gDanceToolTimerPeriod, "timerTick");
    $gDanceToolTimeStart = getSimTime();
};
function danceTool::stopTimer(%this) {
    cancel($gDanceToolTimer);
    $gDanceToolTimer = 0;
};
function danceTool::timerTick(%this) {
    if (!(%this.playing)) {
        return;
    }
    %this.playNextStep();
    $gDanceToolTimer = %this.schedule($gDanceToolTimerPeriod, "timerTick");
};
function danceTool::constructSequence(%this, %lines) {
    %numFields = 2;
    %totalT = 0;
    %this.numSteps = getRecordCount(%lines);
    %n = 0;
    if ((%this.numSteps < %n)) {
        %line = getRecord(%lines, %n);
        %wc = getWordCount(%line);
        if ((%numFields >= %wc)) {
            %stepName = getWords(%line, 0, (%numFields - %wc));
            %stepDuration = getWord(%line, (1.0 - %wc));
            %this.stepTimes = %totalT @ %n;
            %this.stepNames = %stepName @ %n;
            %totalT = (%stepDuration + %totalT);
        }
        %n = (1.0 + %n);
    }
    %this.stepTimes = (%this.numSteps < %n) @ %totalT @ %this.numSteps;
    %this.stepNames = "(finished)" @ %this.numSteps;
    %this.numSteps = (1.0 + %this.numSteps);
};
function danceTool::playNextStep(%this) {
    %curDanceTime = ($gDanceToolTimeStart - getSimTime());
    %curDanceTime = (0.001 * %curDanceTime);
    %playStep = -(1.0);
    %tooFar = 0;
    if ((0.0 >= %this.prevStep)) {
        %n = (1.0 + %this.prevStep);
        if ((%this.numSteps < %n)) {
        }
        if (!(%tooFar)) {
            if ((%curDanceTime @ %n <= %this.stepTimes)) {
                %playStep = %n;
                %playStepTime = %this.stepTimes;
                %n;
            }
            %tooFar = 1;
            %n = (1.0 + %n);
            if ((%this.numSteps < %n)) {
            }
        }
    }
    %playStep = 0;
    !(%tooFar);
    if (((1.0 - %this.numSteps) >= %playStep)) {
        if (guiDanceToolCheckBoxLoop.getValue()) {
            %this.play();
            $gDanceToolTimeStart = ((%playStepTime - %curDanceTime) + $gDanceToolTimeStart);
        }
        %this.stop();
    }
    if ((0.0 >= %playStep)) {
        %this.playStep(%playStep);
    }
};
function danceTool::playStep(%this, %stepNum) {
    if ((0.0 < %stepNum)) {
    }
    if ((%this.numSteps >= %stepNum)) {
        error("invalid step index" @ " " @ %stepNum @ " " @ " - we have" @ " " @ %this.numSteps);
        %this.stop();
        return;
    }
    %stepName = %this.stepNames;
    %stepNum;
    %this.prevStep = %stepNum;
    %animName = %this.getAnimName(%stepName);
    if (!(%this.canRecordAnim(%animName))) {
        return;
    }
    sendDanceToolAnimToServer(%animName);
};
function danceTool::getAnimName(%this, %stepName) {
    return %stepName;
};
function danceTool::canRecordAnim(%this, %nameInternal) {
    %cantRecordList = "mnapls01 mnapls02 mnapls03 mngtrglr1e mngtrglr2e mngtrglr3e mngtrglr4e mngtrglr5e mngtrglr6e mngtrglr7e mngtrglr8a mngtrglr9a mngtrglr10a" @ " " @ "mngtrglr11a mngtrglr12a mngtrglr13a mngtrglr14a mngtrglr15b mngtrglr16b mngtrglr17b mngtrglr18b mngtrglr19b mngtrglr20b mngtrglridl1 mngtrglrwlkf01" @ " " @ "mngtrglrwlkb01 mngtrglrside01 mngtrglrjmp01 mnjmp mnfall mnrent mnrext mnridl1 mnwidl1 mnwent2 mnwext2 mnwidl2 mnsidl1 mnsent mnsext mnhtidl1 mnlsnidl1" @ " " @ "mnlsnent mnlsnext mnclbent mnclbext mnclbidl1 mnbhop mnbedentr mnbedextr mnbedextl mnbedentl mnbedslpbk mnbedslpsdl mnbedrlx mnchzlngidl1 mnpckride" @ " " @ "mnreachdown mnspinbottle mndrumr1e mnbassr1e mnsumowlks mnsumoshortstun mnsumolongstun mnsumoidle mnsumojabattack mnsumopowerattack mnsumobbattack" @ " " @ "mnsumojabdefend mnsumopowerdefend mnsumotaunt01 mnsumotaunt02 mnsumoidl mnsumowlkf mnsumowlkb mnsumosde mnsumojmp mnsumoattack mnsumodefend mnsumostumble" @ " " @ "mnsumowin mnsumoloose mngtrgr1e mngtrgr2e mngtrgr3e mngtrgr5e mngtrgr6e mngtrgr7e mngtrgr8e mngtrgr9e mngtrgr10e mngtrgr11e mngtrgr12a mngtrgr13a mngtrgr14a" @ " " @ "mngtrgr15a mngtrgr17a mngtrgr18a mngtrgr22a mngtrgr25b mngtrgr28b mngtrgr29b mnarcadeidl fnapls01 fnapls02 fnapls03 fngtrglr1e fngtrglr2e fngtrglr3e fngtrglr4e" @ " " @ "mnbhop mnbedentr mnbedextr mnbedextl mnbedentl mnbedslpbk mnbedslpsdr mnbedslpsdl mnbedrlx mnchzlngidl1" @ " " @ "mnpckride mnreachdown mnspinbottle mndrumr1e mnbassr1e mnarcadeidl mnssentr mnssext mnssidl1 mnstyl1" @ " " @ "mycut1 mybdry mywatrpt myidl1a mywlkf1 mysde mywlkb1 myjmp mycidl1a mycidl2a mylidl1a mylidl2a mylidl3a" @ " " @ "mybrush myclip myhpick myshears mygunsling mymime mnswmidl1 mnswmf1 mnswmb1 mnswmsde" @ " " @ "mnpwidle mnpwwlkf mnpwwlkb mnpwsde mnpwjmp mnpwjabattack mnpwpowerattack mnpwbbattack mnpwjabdefend mnpwpowerdefend" @ " " @ "mnpwshortstun mnpwlongstun mnpwtaunt01 mnpwtaunt02" @ " " @ "mntapglass mnsmentr mnsmexit mnsmcidl1 mnsmanidl1 mysmanfl mysmanpnt" @ " " @ "mnspcentr mnspcexit mnspcidl1 mnspedentr mnspedexit mnspedidl myspedfl myspedpnt mynailpolish" @ " " @ "mnmmi_handshake mnmmr_handshake mnmsi_handshake mnmsr_handshake mnmti_handshake mnmtr_handshake mnsti_handshake" @ " " @ "mnstr_handshake mntsi_handshake mntsr_handshake" @ " " @ "mnmmi_hug mnmmr_hug mnmsi_hug mnmsr_hug mnmti_hug mnmtr_hug mnsti_hug mnstr_hug mntsi_hug mntsr_hug" @ " " @ "mnmmi_kiss mnmmr_kiss mnmsi_kiss mnmsr_kiss mnmti_kiss mnmtr_kiss mnsti_kiss mnstr_kiss mntsi_kiss mntsr_kiss" @ " " @ "mnmmi_giveloot mnmmr_giveloot mnmsi_giveloot mnmsr_giveloot mnmti_giveloot mnmtr_giveloot mnsti_giveloot mnstr_giveloot mntsi_giveloot mntsr_giveloot" @ " " @ "mynailfile mybowarrow myscissorhand myswitchcomb" @ " " @ "myadjwrench mypipewrench mypiercegun myforcepa myforcepb mypliera myplierb" @ " " @ "fngtrglr5e fngtrglr6e fngtrglr7e fngtrglr8a fngtrglr9a fngtrglr10a fngtrglr11a fngtrglr12a fngtrglr13a fngtrglr14a fngtrglr15b fngtrglr16b fngtrglr17b fngtrglr18b" @ " " @ "fngtrglr19b fngtrglr20b fngtrglridl1 fngtrglrwlkf01 fngtrglrwlkb01 fngtrglrside01 fngtrglrjmp01 fnjmp fnfall fnrent fnrext fnridl1 fnwidl1 fnwent2 fnwext2 fnwidl2" @ " " @ "fnsidl1 fnsent fnsext fnhtidl1 fnlsnidl1 fnlsnent fnlsnext fnclbent fnclbext fnclbidl1 fnbhop fnbedentr fnbedextr fnbedextl fnbedentl fnbedslpbk fnbedslpsdl fnbedrlx" @ " " @ "fnchzlngidl1 fnpckride fnreachdown fnspinbottle fndrumr1e fnbassr1e fnsumowlks fnsumoshortstun fnsumolongstun fnsumoidle fnsumojabattack fnsumopowerattack" @ " " @ "fnsumobbattack fnsumojabdefend fnsumopowerdefend fnsumotaunt01 fnsumotaunt02 fnsumoidl fnsumowlkf fnsumowlkb fnsumosde fnsumojmp fnsumoattack fnsumodefend fnsumostumble" @ " " @ "fnsumowin fnsumoloose fngtrgr1e fngtrgr2e fngtrgr3e fngtrgr5e fngtrgr6e fngtrgr7e fngtrgr8e fngtrgr9e fngtrgr10e fngtrgr11e fngtrgr12a fngtrgr13a fngtrgr14a" @ " " @ "fngtrgr15a fngtrgr17a fngtrgr18a fngtrgr22a fngtrgr25b fngtrgr28b fngtrgr29b fnarcadeidl fnswmb1 fnswmsde fnswmidl1 fnswmf1 mnswmb1 mnswmsde mnswmidl1 mnswmf1" @ " " @ "fnbhop fnbedentr fnbedextr fnbedextl fnbedentl fnbedslpbk fnbedslpsdr fnbedslpsdl fnbedrlx fnchzlngidl1" @ " " @ "fnpckride fnreachdown fnspinbottle fndrumr1e fnbassr1e fnarcadeidl fnssentr fnssext fnssidl1 fnstyl1" @ " " @ "fycut1 fybdry fywatrpt fyidl1a fywlkf1 fysde fywlkb1 fyjmp fycidl1a fycidl2a fylidl1a fylidl2a fylidl3a" @ " " @ "fybrush fyclip fyhpick fyshears fygunsling fymime fnswmidl1 fnswmf1 fnswmb1 fnswmsde" @ " " @ "fnpwidle fnpwwlkf fnpwwlkb fnpwsde fnpwjmp fnpwjabattack fnpwpowerattack fnpwbbattack fnpwjabdefend fnpwpowerdefend" @ " " @ "fnpwshortstun fnpwlongstun fnpwtaunt01 fnpwtaunt02" @ " " @ "fntapglass fnsmentr fnsmexit fnsmcidl1 fnsmanidl1 fysmanfl fysmanpnt" @ " " @ "fnspcentr fnspcexit fnspcidl1 fnspedentr fnspedexit fnspedidl fyspedfl fyspedpnt fynailpolish" @ " " @ "fnmmi_handshake fnmmr_handshake fnmsi_handshake fnmsr_handshake fnmti_handshake fnmtr_handshake fnsti_handshake" @ " " @ "fnstr_handshake fntsi_handshake fntsr_handshake" @ " " @ "fnmmi_hug fnmmr_hug fnmsi_hug fnmsr_hug fnmti_hug fnmtr_hug fnsti_hug fnstr_hug fntsi_hug fntsr_hug" @ " " @ "fnmmi_kiss fnmmr_kiss fnmsi_kiss fnmsr_kiss fnmti_kiss fnmtr_kiss fnsti_kiss fnstr_kiss fntsi_kiss fntsr_kiss" @ " " @ "fnmmi_giveloot fnmmr_giveloot fnmsi_giveloot fnmsr_giveloot fnmti_giveloot fnmtr_giveloot fnsti_giveloot fnstr_giveloot fntsi_giveloot fntsr_giveloot" @ " " @ "fynailfile fybowarrow fyscissorhand fyswitchcomb" @ " " @ "fyadjwrench fypipewrench fypiercegun fyforcepa fyforcepb fypliera fyplierb" @ " " @ "gtrglr5e gtrglr6e gtrglr7e gtrglr8a gtrglr9a gtrglr10a gtrglr11a gtrglr12a gtrglr13a gtrglr14a gtrglr15b gtrglr16b gtrglr17b gtrglr18b" @ " " @ "gtrglr19b gtrglr20b gtrglridl1 gtrglrwlkf01 gtrglrwlkb01 gtrglrside01 gtrglrjmp01 jmp fall rent rext ridl1 widl1 went2 wext2 widl2" @ " " @ "sidl1 sent sext htidl1 lsnidl1 lsnent lsnext clbent clbext clbidl1 bhop bedentr bedextr bedextl bedentl bedslpbk bedslpsdl bedrlx" @ " " @ "chzlngidl1 pckride reachdown spinbottle drumr1e bassr1e sumowlks sumoshortstun sumolongstun sumoidle sumojabattack sumopowerattack" @ " " @ "sumobbattack sumojabdefend sumopowerdefend sumotaunt01 sumotaunt02 sumoidl sumowlkf sumowlkb sumosde sumojmp sumoattack sumodefend sumostumble" @ " " @ "sumowin sumoloose gtrgr1e gtrgr2e gtrgr3e gtrgr5e gtrgr6e gtrgr7e gtrgr8e gtrgr9e gtrgr10e gtrgr11e gtrgr12a gtrgr13a gtrgr14a" @ " " @ "gtrgr15a gtrgr17a gtrgr18a gtrgr22a gtrgr25b gtrgr28b gtrgr29b arcadeidl swmb1 swmsde swmidl1 swmf1 mnswmb1 mnswmsde mnswmidl1 mnswmf1" @ " " @ "bhop bedentr bedextr bedextl bedentl bedslpbk bedslpsdr bedslpsdl bedrlx chzlngidl1" @ " " @ "pckride reachdown spinbottle drumr1e bassr1e arcadeidl ssentr ssext ssidl1 styl1" @ " " @ "cut1 bdry watrpt idl1a wlkf1 sde wlkb1 jmp cidl1a cidl2a lidl1a lidl2a lidl3a" @ " " @ "brush clip hpick shears gunsling mime swmidl1 swmf1 swmb1 swmsde" @ " " @ "pwidle pwwlkf pwwlkb pwsde pwjmp pwjabattack pwpowerattack pwbbattack pwjabdefend pwpowerdefend" @ " " @ "pwshortstun pwlongstun pwtaunt01 pwtaunt02" @ " " @ "tapglass smentr smexit smcidl1 smanidl1 smanfl smanpnt" @ " " @ "spcentr spcexit spcidl1 spedentr spedexit spedidl spedfl spedpnt nailpolish" @ " " @ "mmi_handshake mmr_handshake msi_handshake msr_handshake mti_handshake mtr_handshake sti_handshake" @ " " @ "str_handshake tsi_handshake tsr_handshake" @ " " @ "mmi_hug mmr_hug msi_hug msr_hug mti_hug mtr_hug sti_hug str_hug tsi_hug tsr_hug" @ " " @ "mmi_kiss mmr_kiss msi_kiss msr_kiss mti_kiss mtr_kiss sti_kiss str_kiss tsi_kiss tsr_kiss" @ " " @ "mmi_giveloot mmr_giveloot msi_giveloot msr_giveloot mti_giveloot mtr_giveloot sti_giveloot str_giveloot tsi_giveloot tsr_giveloot" @ " " @ "nailfile bowarrow scissorhand switchcomb" @ " " @ "adjwrench pipewrench piercegun forcepa forcepb pliera plierb";
    if ((-(1.0) == findWord(%cantRecordList, %nameInternal))) {
        return 1;
    }
    return 0;
};
function danceTool::addStep(%this, %nameInternal) {
    if (!(%this.recording)) {
        return;
    }
    if (!(%this.canRecordAnim(%nameInternal))) {
        return;
    }
    %animName = %nameInternal;
    %this.finishRecordingPreviousStep();
    %animName.addText(1, 1);
};
function danceTool::finishRecordingPreviousStep(%this) {
    %t = getSimTime();
    if ((-(1.0) != %this.prevStepTime)) {
        %dt = (%this.prevStepTime - %t);
        %dt = (0.001 * mFloor(%dt));
        if ((0.1 < %dt)) {
            %dt = 0.1;
        }
        " " @ %dt @ "\n".addText(1, 1);
    }
    %this.prevStepTime = guiDanceToolMLTextBody @ %t;
};
function clientCmdDisableDanceTool(%unused) {
    danceTool.stop();
    handleSystemMessage("msgInfoMessage", "");
};
function danceTool::setGender(%this, %gender) {
    if ((%gender $= $player.getGender())) {
        %colorTag = "";
    }
    %colorTag = "<color:ff0000ff>";
    if ((%gender $= "f")) {
        %genderFull = "females";
    }
    %genderFull = "males";
    %txt = "Designed for:" @ " " @ %colorTag @ %genderFull;
    %txt.setText();
};
$gDanceToolVersionString = "Dancetastique version 1.0";
function danceTool::clipboardCopyTo(%this) {
    setClipboard(%this.getContent());
};
function danceTool::clipboardPasteFrom(%this) {
    %this.setContent(getClipboard());
};
function danceTool::getContent(%this) {
    %content = "";
    %content = %content @ $gDanceToolVersionString @ "\n";
    %content = %content @ guiDanceToolTextAuthor.getValue() @ "\n";
    %content = %content @ guiDanceToolTextTitle.getValue() @ "\n";
    %content = %content @ $player.getGender() @ "\n";
    %content = %content @ guiDanceToolMLTextBody.getValue();
    return %content;
};
function danceTool::setContent(%this, %content) {
    %version = trim(getRecord(%content, 0));
    %content = trim(removeRecord(%content, 0));
    %author = trim(getRecord(%content, 0));
    %content = trim(removeRecord(%content, 0));
    %title = trim(getRecord(%content, 0));
    %content = trim(removeRecord(%content, 0));
    %gender = trim(getRecord(%content, 0));
    %content = trim(removeRecord(%content, 0));
    if (!(%version $= $gDanceToolVersionString)) {
        MessageBoxOK("Wrong Version!", "" @ $gDanceToolVersionString @ "\nand you're trying to use a dance from\n" @ " " @ %version, "");
        return;
    }
    %author.setValue();
    %title.setValue();
    %this.setGender(%gender);
    %content.setText();
};
$gDanceToolInitialized = 0;
function danceTool::initialcontent(%this) {
    if ($gDanceToolInitialized) {
        return;
    }
    %content = "";
    %content = %content @ $gDanceToolVersionString @ "\n";
    if (($player.getGender() $= "f")) {
        %content = %content @ $ETS::AppName @ "\n";
        %content = %content @ "The Nevada\n";
        %content = %content @ "f\n";
        %content = %content @ "hdnc1 2.61\nhdnc2 2.75\nhdnc3 3.61\nhdnc1 4.108\nidnc1 1.86\nidnc2 1.312\nidnc1 1.28\nidnc2 1.112\nidnc1 0.42\nidnc2 0.656\nidnc1 0.424\nidnc2 0.468\nidnc3 7.984\nhdncb1 5.624\nhdncb2 1.688\nhdncb3 3.594\nhdncb4 1.922\nhdncb1 2.5\nnmjlih 1.938\nnmjspin 4.422\nbusy 0.6\ncool 0.6\nbusy 0.6\ncool 0.6\nbusy 0.6\ncool 0.6\nhdnc1 2.704\n";
    }
    %content = %content @ $ETS::AppName @ "\n";
    %content = %content @ "Whack\n";
    %content = %content @ "m\n";
    %content = %content @ "nmjlih 0.258\nnmjspin 1.409\nhdnc2 1.004\nhdnc4 1.119\nnmjzstep 0.921\nidnc2 1.128\nhdnc1 0.854\nnmjlih 0.427\nhdnc1 0.409\nnmjlih 0.363\nhdnc1 0.59\nnmjlih 0.501\nhdnc1 0.594\nnmjlih 0.334\nhdnc1 0.449\nidnc2 0.441\nhdnc1 0.445\nidnc2 0.522\nnmjzstep 0.574\npdnc1 0.41\nnmjzstep 0.413\npdnc1 0.454\nnmjzstep 0.48\npdnc1 1.005\nhdnc4 1.419\nhdnc2 0.975\nhdnc4 0.462\nhdnc2 0.1\nnmjspin 0.917\nhdnc2 0.362\nnmjspin 0.524\nnmjlih 0.965\nhdnc1 0.684\nnmjlih 0.355\nhdnc1 0.43\nnmjlih 0.434\nnmjspin 1.46\nhdnc2 1.024\nhdnc4 0.966\npdnc1 0.491\nhdnc4 0.301\npdnc1 0.595\nhdnc4 0.376\npdnc1 0.467\nhdnc4 0.449\npdnc1 0.622\nhdnc4 0.367\npdnc1 0.743\nhdnc4 0.356\npdnc1 0.495\nhdnc4 0.316\npdnc1 0.492\nnmjzstep 0.489\nidnc2 0.448\nnmjzstep 0.585\nidnc2 0.37\nnmjzstep 0.494\nidnc2 0.499\nnmjzstep 0.52\nidnc2 0.438\nnmjzstep 0.465\nidnc2 0.487\nnmjzstep 0.422\nidnc2 0.426\nhdnc1 0.476\nnmjlih 0.446\nhdnc1 0.304\nnmjlih 0.264\nhdnc1 0.226\nnmjlih 0.184\nhdnc1 0.158\nnmjlih 0.204\nhdnc1 0.161\nnmjlih 0.159\nhdnc1 0.188\nnmjlih 0.157\nhdnc1 0.161\nnmjlih 0.137\nhdnc1 0.1\nnmjzstep 1.371\npdnc1 0.24\nnmjzstep 0.138\npdnc1 0.13\nnmjzstep 0.132\npdnc1 0.12\nnmjzstep 0.112\npdnc1 0.125\nnmjzstep 0.1\npdnc1 0.123\nnmjzstep 0.125\npdnc1 0.345\nhdnc4 0.263\nhdnc4 0.1\npdnc1 0.145\npdnc1 0.144\nhdnc4 0.167\npdnc1 0.139\npdnc1 0.163\nhdnc4 0.207\npdnc1 0.225\nhdnc4 0.254\npdnc1 0.199\npdnc1 0.1\nhdnc4 0.173\npdnc1 0.226\nhdnc4 0.4\npdnc1 0.107\nhdnc4 0.126\npdnc1 0.165\npdnc1 0.1\nhdnc4 0.311\nhdnc4 0.141\nhdnc2 0.483\nnmjspin 0.424\nnmjlih 0.397\nnmjspin 0.553\nnmjlih 0.306\nnmjspin 1.68\nnmjspin 1.868\nhdnc4 0.503\nhdnc2 0.39\nhdnc4 0.583\nhdnc2 0.485\nhdnc4 0.494\nhdnc2 0.348\nhdnc4 0.521\nhdnc2 0.35\nhdnc4 0.321\nhdnc4 0.171\npdnc1 0.545\nnmjzstep 0.969\nnmjzstep 0.563\npdnc1 0.388\nnmjzstep 0.51\nhdnc1 0.425\nidnc2 0.222\nidnc2 0.1\nhdnc1 0.223\nidnc2 0.186\nhdnc1 0.184\nidnc2 0.179\nhdnc1 0.124\nidnc2 0.127\nidnc2 0.1\nhdnc1 0.226\nidnc2 0.124\nhdnc1 0.184\nidnc2 0.119\nhdnc1 0.172\nidnc2 0.11\nhdnc1 0.163\nidnc2 0.114\nhdnc1 0.138\nidnc2 0.105\nhdnc1 0.24\nnmjlih 0.355\nhdnc1 0.268\nnmjlih 0.245\nhdnc1 0.209\nnmjlih 0.519\npdnc1 0.181\nhdnc4 0.922\npdnc1 0.489\nnmjzstep 0.399\npdnc1 0.427\nnmjzstep 0.465\npdnc1 0.518\nnmjlih 2.064\nnmjspin 1.808\nhdncb3 2.411\nhdncb2 2.967\nhdncb4 4.251\nhdnc1 2.409\nnmjlih 0.397\nnmjlih 4.307\n";
    %this.setContent(%content);
    $gDanceToolInitialized = 1;
};
