function SystemMessageDialog::isShowing(%this) {
    return (HudTabs.getCurrentTab().name $= "word");
};
function toggleSystemMessageDialog() {
    if (SystemMessageDialog.isShowing()) {
        SystemMessageDialog.close();
    } else {
        SystemMessageDialog.open();
    }
};
function SystemMessageDialog::open(%this) {
    if ($UserPref::HudTabs::AutoOpen["word"]) {
        HudTabs.selectTabWithName("word");
    }
};
function SystemMessageDialog::close(%this) {
    if (%this.isShowing()) {
        HudTabs.close();
    }
};
function SystemMessageDialog::onClose(%this) {
};
addMessageCallback('MsgSystemMessage', handleSystemMessage);
addMessageCallback('MsgInfoMessage', handleSystemMessage);
addMessageCallback('MsgGamePlayOkMessage', handleGamePlayMessage);
addMessageCallback('MsgTickerMessage', handleTickerMessage);
function SystemMessageDialog::getTimeStampNice(%ts) {
    if ((%ts $= "")) {
        %ts = getTimeStamp();
    }
    %hr = getSubStr(%ts, 9, 2);
    %mn = getSubStr(%ts, 12, 2);
    %sc = getSubStr(%ts, 15, 2);
    %ap = "am";
    if ((%hr >= 12.0)) {
        %hr = (%hr - 12.0);
        %ap = "pm";
    }
    if ((%hr == 0.0)) {
        %hr = 12;
    }
    %tm = "";
    %tm = %tm @ %hr;
    %tm = %tm @ ":" @ %mn;
    %tm = %tm @ %ap;
    return %tm;
};
function handleGamePlayMessage(%msgType, %msgString) {
    %profileDlg = SystemMessageDialogProfile;
    %profileTxt = SystemMessageTextProfile;
    %timeStamp = SystemMessageDialog::getTimeStampNice(getTimeStamp()) @ " ";
    if ((detag(%msgType) $= "MsgGamePlayOkMessage")) {
        return MessageBoxOK("vSide - Notice", %msgString, "");
    }
};
function formatMessagePriority(%msgString) {
    %lastMsgLvl = "MSGLEVEL2";
    %idx = 0;
    while (((%idx = strstr(%msgString, "MSGLEVEL")) >= 0.0)) {
        %lastMsgLvl = getSubStr(%msgString, %idx, 9);
        %msgString = getSubStr(%msgString, 0, %idx) @ getSubStr(%msgString, (%idx + 9.0), 1000);
    }
    return getSubStr(%lastMsgLvl, 8, 1) @ " " @ %msgString;
};
function handleSystemMessage(%msgType, %msgString) {
    %timeStamp = SystemMessageDialog::getTimeStampNice(getTimeStamp()) @ " ";
    %msgString = formatMessagePriority(%msgString);
    %importanceLevel = getWord(%msgString, 0);
    %msgString = removeWord(%msgString, 0);
    if (!(%importanceLevel $= 3)) {
        SystemMessageDialog.open();
    }
    if ((%importanceLevel $= 1)) {
        HudTabs.dontCloseNextTime();
    } else {
        if ((%importanceLevel $= 2)) {
        } else {
            if ((%importanceLevel $= 3)) {
                HudTabs.pulseTabWithName("word");
            }
        }
    }
    %timeStamp = "<spush><color:66aaffff>" @ %timeStamp @ "<spop>";
    SystemMessageTextCtrl.addText(%timeStamp @ " " @ %msgString, 1, 1);
    if ($UserPref::Audio::NotifyWhisper) {
        alxPlay(AudioIm_SystemMessageIn);
    }
};
SystemMessageTextCtrl.bufferSize = 0;
function SystemMessageTextCtrl::addText(%this, %txtString) {
    if ((%this.bufferSize $= "")) {
        %this.bufferSize = 0;
    }
    if ((%this.bufferSize >= 20.0)) {
        %this.deleteOldestBufferLine();
    }
    %this.bufferMessage[%this.bufferSize] = %txtString;
    %this.bufferSize = (%this.bufferSize + 1.0);
    %this.refresh();
};
function SystemMessageTextCtrl::clearText(%this) {
    %this.bufferSize = 0;
    %this.setText(%this.DefaultMessage);
};
function SystemMessageTextCtrl::deleteOldestBufferLine(%this) {
    %n = 0;
    while ((%n < %this.bufferSize)) {
        %this.bufferMessage[%n] = %this.bufferMessage[(%n + 1.0)];
        %n = (%n + 1.0);
    }
    %this.bufferSize = (%this.bufferSize - 1.0);
    (%n < %this.bufferSize);
};
function SystemMessageTextCtrl::refresh(%this) {
    %this.setText("");
    %n = (%this.bufferSize - 1.0);
    while ((%n >= 0.0)) {
        %curString = "<spush>";
        if ((%n == (%this.bufferSize - 1.0))) {
            %curString = %curString @ "<b>";
        }
        %curString = %curString @ "<color:" @ %this.getMessageColor(((%this.bufferSize - %n) - 1.0)) @ ">" @ %this.bufferMessage[%n] @ "<spop>\n";
        Parent::addText(%this, %curString, 0, 1);
        %n = (%n - 1.0);
    }
    %this.scrollToTop();
};
$gAgedMessageColors[0] = "ffddffff";
$gAgedMessageColors[1] = "ffeeffcc";
$gAgedMessageColors[2] = "ffffff99";
$gAgedMessageColors[3] = "ffffff77";
function SystemMessageTextCtrl::getMessageColor(%this, %age) {
    if ((%age > 3.0)) {
        %age = 3;
    }
    return $gAgedMessageColors[%age];
};
function SystemMessageTextCtrl::onRightURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %name = unmunge(getWords(%url, 1));
        onRightClickPlayerName(%name);
    } else {
        if ((getSubStr(%url, 0, 7) $= "http://") || (getSubStr(%url, 0, 7) $= "vside:/")) {
            LinkContextMenu.initWithURL(%url);
            LinkContextMenu.showAtCursor();
        }
    }
    if (!%this.selectionActive) {
        TheShapeNameHud.makeFirstResponder(1);
    }
};
function SystemMessageTextCtrl::onURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %name = unmunge(getWords(%url, 1));
        onLeftClickPlayerName(%name, "");
    } else {
        if ((getWord(%url, 0) $= "ACCEPT")) {
            %name = unmunge(getWord(%url, 1));
            if (!(%name $= "")) {
                doUserFavorite(%name, "accept");
            }
        } else {
            if ((getWord(%url, 0) $= "DECLINE")) {
                %name = unmunge(getWord(%url, 1));
                if (!(%name $= "")) {
                    doUserFavorite(%name, "decline");
                }
            } else {
                if ((getWord(%url, 0) $= "CANCEL")) {
                    %name = unmunge(getWord(%url, 1));
                    if (!(%name $= "")) {
                        doUserFavorite(%name, "cancel");
                    }
                } else {
                    if ((getWord(%url, 0) $= "ACCEPT_2PLAYER_ACTION")) {
                        %name = unmunge(getWord(%url, 1));
                        %requestId = getWord(%url, 2);
                        %coanim = getWords(%url, 3);
                        if (!(%name $= "")) {
                        }
                        if (!(%coanim $= "")) {
                            setIdle(0);
                            commandToServer('CoAnimRespond', %requestId, "ACCEPT MANUAL");
                            %this.updateTwoPlayerActionRequest(%name, %coanim, %requestId, 1);
                        }
                    } else {
                        if ((getWord(%url, 0) $= "DECLINE_2PLAYER_ACTION")) {
                            %name = unmunge(getWord(%url, 1));
                            %requestId = getWord(%url, 2);
                            %coanim = getWords(%url, 3);
                            if (!(%name $= "")) {
                                commandToServer('CoAnimRespond', %requestId, "DECLINE MANUAL");
                                %this.updateTwoPlayerActionRequest(%name, %coanim, %requestId, 0);
                            }
                        } else {
                            if ((getSubStr(%url, 0, 7) $= "http://")) {
                                gotoWebPage(%url);
                            } else {
                                if ((getSubStr(%url, 0, 7) $= "vside:/")) {
                                    vurlOperation(%url);
                                } else {
                                    if ((getWord(%url, 0) $= "game")) {
                                        %cmd = getWord(%url, 1);
                                        if ((%cmd $= "inspect")) {
                                            gameMgrClient.requestToInspectGame(getWord(%url, 2));
                                            GameMgrHudWin.open();
                                            GameMgrHudTabs.selectTabWithName("INSPECT");
                                        }
                                    } else {
                                        if ((getWord(%url, 0) $= "answerHelpMeMode")) {
                                            %requestId = getWord(%url, 1);
                                            %newbName = unmunge(getWords(%url, 2, 11111));
                                            answerHelpMeMode(%newbName, %requestId);
                                            %this.changeLinesEndingInString("<a:" @ %url, "- You answered the call!");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    if (!%this.selectionActive) {
        TheShapeNameHud.makeFirstResponder(1);
    }
};
function SystemMessageTextCtrl::updateFriendRequest(%this, %name, %accept) {
    %acceptString = "";
    if ((%accept == 1.0)) {
        %acceptString = %acceptString @ "Accepted!";
    } else {
        if ((%accept == 0.0)) {
            %acceptString = %acceptString @ "Declined!";
        } else {
            %acceptString = %acceptString @ "(they cancelled)";
        }
    }
    if (!(%name $= "")) {
        %linkStart = "<a:ACCEPT " @ munge(%name) @ ">";
    } else {
        %linkStart = "<a:ACCEPT ";
    }
    %this.changeLinesEndingInString(%linkStart, %acceptString);
};
function SystemMessageTextCtrl::updateTwoPlayerActionRequest(%this, %name, %coAnimName, %requestId, %accept) {
    %acceptString = "";
    if ((%accept == 0.0)) {
        %acceptString = %acceptString @ "Declined!";
    } else {
        if ((%accept == 1.0)) {
            %acceptString = %acceptString @ "Accepted!";
        } else {
            if ((%accept == 2.0)) {
                %acceptString = %acceptString @ "(they cancelled)";
            } else {
                %acceptString = %acceptString @ "(timed out)";
            }
        }
    }
    if (!(%name $= "")) {
        %linkStart = "<a:ACCEPT_2PLAYER_ACTION " @ munge(%name) @ " " @ %requestId @ " " @ %coAnimName @ ">";
    }
    %this.changeLinesEndingInString(%linkStart, %acceptString);
};
function SystemMessageTextCtrl::changeLinesEndingInString(%this, %replaceThis, %withThis) {
    %i = (%this.bufferSize - 1.0);
    while ((%i >= 0.0)) {
        %curLine = %this.bufferMessage[%i];
        %start = strstr(%curLine, %replaceThis);
        if ((%start < 0.0)) {
        } else {
            %newLine = getSubStr(%curLine, 0, %start) @ " " @ %withThis;
            %this.bufferMessage[%i] = %newLine;
        }
        %i = (%i - 1.0);
    }
    %this.refresh();
};
