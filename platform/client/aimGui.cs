if (!(isObject(AIMConvManager))) {
    new ScriptObject(AIMConvManager);
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(AIMConvManager);
    }
}
function AIMConvManager::Initialize(%this) {
    if (!(%this.initialized)) {
        %this.spamDict = 0 @ new StringMap("");;
        if (isObject(MissionCleanup)) {
            MissionCleanup.add(%this.spamDict);
        }
        %this.numConvs = 0;
        %this.currentConvIndex = -(1.0);
        if ((0.0 <= %this.maxConvs)) {
            %this.maxConvs = 20;
        }
        %this.movingBars = 0;
        %this.totalMessagesSent = 0;
        %this.initialized = 1;
    }
    %this.update();
};
function AIMConvManager::selectConvAtIndex(%this, %convIndex) {
    if ((0.0 >= %this.currentConvIndex)) {
    }
    if ((%this.numConvs < %this.currentConvIndex)) {
    }
    if ((%convIndex != %this.currentConvIndex)) {
        %this.currentConvIndex.setVisible(%this.convs.contents, 0);
        %this.currentConvIndex.setProfile(%this.convs.titlebar.recipient);
        %this.convs.newMessage = ETSAIMDeselectedProfile @ 0 @ %this.currentConvIndex;
    }
    if ((0.0 >= %convIndex)) {
    }
    if ((%this.numConvs < %convIndex)) {
        %convIndex.setProfile(%this.convs.titlebar.recipient);
        %this.convs.newMessage = ETSAIMSelectedProfile @ 0 @ %convIndex;
    }
    %this.currentConvIndex = %convIndex;
    %this.update();
};
function AIMConvManager::selectCurrentConv(%this) {
    %this.selectConvAtIndex(%this.currentConvIndex);
};
function AIMConvManager::selectConv(%this, %convId) {
    %idx = 0;
    if ((%this.numConvs < %idx)) {
        if ((%convId @ %idx == %this.convs.getId())) {
            %this.selectConvAtIndex(%idx);
            return;
        }
        %idx = (1.0 + %idx);
    }
};
function AIMConvManager::nextConv(%this) {
    if ((0.0 > %this.numConvs)) {
        %this.selectConvAtIndex((%this.numConvs % (1.0 + %this.currentConvIndex)));
    }
};
function AIMConvManager::previousConv(%this) {
    if ((0.0 > %this.numConvs)) {
        %this.selectConvAtIndex((%this.numConvs % (%this.numConvs + (1.0 - %this.currentConvIndex))));
    }
};
function AIMConvManager::removeConvAtIndex(%this, %convIndex) {
    if ((0.0 >= %convIndex)) {
    }
    if ((%this.numConvs < %convIndex)) {
        %conv = %this.convs;
        %convIndex;
        %conv.contents.setVisible(0);
        %conv.titlebar.setVisible(0);
        %conv.contents.delete();
        %conv.titlebar.delete();
        %conv.delete();
        %this.numConvs = (1.0 - %this.numConvs);
        %idx = %convIndex;
        if ((%this.numConvs < %idx)) {
            %this.convs = (1.0 + %idx) @ %this.convs @ %idx;
            %idx = (1.0 + %idx);
        }
        %this.convs = (%this.numConvs < %idx) @ 0 @ %this.numConvs;
        if ((0.0 == %this.numConvs)) {
            %this.currentConvIndex = -(1.0);
        }
        if ((%this.numConvs >= %this.currentConvIndex)) {
            %this.selectConvAtIndex((1.0 - %this.numConvs));
        }
        if ((%convIndex > %this.currentConvIndex)) {
            %this.selectConvAtIndex((1.0 - %this.currentConvIndex));
        }
        if ((%convIndex == %this.currentConvIndex)) {
            %this.selectCurrentConv();
        }
    }
    %this.update();
    ConvBub.updateAutoMargins();
};
function AIMConvManager::removeConv(%this, %convId) {
    %idx = 0;
    if ((%this.numConvs < %idx)) {
        if ((%convId @ %idx == %this.convs.getId())) {
            %this.removeConvAtIndex(%idx);
            return;
        }
        %idx = (1.0 + %idx);
    }
};
function AIMConvManager::removeCurrentConv(%this) {
    %this.removeConvAtIndex(%this.currentConvIndex);
};
function AIMConvManager::update(%this) {
    if ((0.0 == %this.numConvs)) {
    }
    if ((0.0 < %this.currentConvIndex)) {
    }
    if ((0.0 == Canvas.getFirstResponder())) {
        TheShapeNameHud.makeFirstResponder(1);
    }
    %this.updateContainer();
    %ypos = 20;
    %this.movingBars = 0;
    %idx = 0;
    if ((%this.numConvs < %idx)) {
        %conv = %this.convs;
        %idx;
        %titlebar = %conv.titlebar;
        %titlebar.setTrgPosition(0, %ypos);
        %ypos = (24.0 + %ypos);
        %this.movingBars = (1.0 + %this.movingBars);
        %contents = %conv.contents;
        %contents.setTrgPosition(getWord(%contents.getTrgPosition(), 0), %ypos);
        if ((%this.currentConvIndex == %idx)) {
            if (%conv.newMessage) {
            }
            %status = %conv.status;
            "new_msg";
            %titlebar.status.setBitmap("platform/client/ui/AIM_sel_" @ %status);
            %titlebar.status.resize(0, 0, 29, 26);
            %titlebar.statusButton.resize(0, 0, 35, 26);
            %titlebar.recipient.reposition(35, 2);
            %titlebar.close.setBitmap("platform/client/buttons/close_m");
            %titlebar.close.resize(180, 6, 13, 13);
            %ypos = (getWord(%contents.getExtent(), 1) + %ypos);
        }
        if (%conv.newMessage) {
        }
        %status = %conv.status;
        "new_msg";
        %titlebar.status.setBitmap("platform/client/ui/AIM_" @ %status);
        %titlebar.status.resize(11, 5, 30, 15);
        %titlebar.statusButton.resize(0, 0, 45, 26);
        %titlebar.recipient.reposition(45, 3);
        %titlebar.close.setBitmap("platform/client/buttons/close_s");
        %titlebar.close.resize((%titlebar.recipient.width + 57.0), 8, 11, 11);
        %idx = (1.0 + %idx);
    }
};
function AIMConvManager::finishUpdate(%this) {
    %curConv = %this.convs;
    %this.currentConvIndex;
    if (isObject(%curConv)) {
        %curConv.contents.setVisible(1);
        if (!(isObject(Canvas.getFirstResponder()))) {
            %curConv.contents.textInput.makeFirstResponder(1);
        }
    }
};
function AIMConvManager::titlebarReachedTarget(%this) {
    if ((0.0 > %this.movingBars)) {
        %this.movingBars = (1.0 - %this.movingBars);
        if ((0.0 == %this.movingBars)) {
            %this.finishUpdate();
        }
    }
};
function AIMConvManager::newConv(%this, %aimName) {
    %conv = %this.getConvWithName(%aimName);
    if (%conv) {
        return %conv;
    }
    %conv = new ScriptObject("") {
        class = 0 @ "AIMConversation";
    };
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%conv);
    }
    %conv.aimName = %aimName;
    %conv.status = "offline";
    %i = 0;
    if ((aimBuddyCount() < %i)) {
        if ((aimGetBuddyName(%i) $= %conv.aimName)) {
            %state = aimGetBuddyState(%i);
            if ((-(1.0) == %state)) {
                %state = 0;
            }
            %conv.status = getWord(%this.stateMapping, %state);
        }
        %i = (1.0 + %i);
    }
    %conv.newMessage = (aimBuddyCount() < %i) @ 0;
    %textInput = new GuiTextEditCtrl("") {
        profile = 0 @ "AIMTextEditProfile";
        horizSizing = "width";
        vertSizing = "top";
        position = "10 99";
        extent = "149 18";
        minExtent = "8 8";
        visible = 1;
        altCommand = "AIMConvManager.sendMessage();";
        maxLength = 255;
        historySize = 200;
        password = 0;
        tabComplete = 0;
        sinkAllKeyEvents = 0;
        willFirstRespond = 1;
        cursorType = 2;
        conv = %conv;
        escCommand = "AIMConvManager.selectConvAtIndex(-1);";
    };
    %scroll = new GuiScrollCtrl("") {
        profile = 0 @ "ETSAimMessageScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "4 0";
        extent = "160 94";
        minExtent = "10 10";
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        childMargin = "0 0";
        scrollMultiplier = 2.5;
    };
    %mlText = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSAIMMessageProfile";
        horizSizing = "relative";
        vertSizing = "relative";
        position = "1 1";
        extent = "147 92";
        minExtent = "8 92";
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        stripTagsOnCopy = 1;
    };
    %scroll.add(%mlText);
    %contents = new GuiControl("") {
        profile = 0 @ "ETSAIMTabProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "30 0";
        extent = "168 116";
        minExtent = "64 64";
        visible = 0;
        sluggishness = 0.3;
        textInput = %textInput;
        mlText = %mlText;
        scroll = %scroll;
    };
    new GuiBitmapCtrl("") {
        profile = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "top";
        position = "2 101";
        extent = "8 13";
        minExtent = "8 13";
        sluggishness = -1;
        visible = 1;
        bitmap = "./ui/AIM_bracket_left";
        wrap = 0;
    }; @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "top";
        position = "157 101";
        extent = "8 13";
        minExtent = "8 13";
        sluggishness = -1;
        visible = 1;
        bitmap = "./ui/AIM_bracket_right";
        wrap = 0;
    };
    %contents.add(%textInput);
    %contents.add(%scroll);
    %dummy = new GuiTextCtrl("") {
        profile = 0 @ "ETSAIMDeselectedProfile";
        horizSizing = "left";
        vertSizing = "top";
        position = "0 0";
        extent = "18 18";
        minExtent = "11 11";
        sluggishness = -1;
        visible = 0;
        maxLength = 255;
    };
    %dummy.setText(%aimName);
    %recipWidth = getWord(%dummy.getExtent(), 0);
    %dummy.delete();
    %recipient = new GuiBitmapButtonCtrl("") {
        profile = 0 @ "ETSAIMDeselectedProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "35 2";
        extent = "140 18";
        minExtent = "100 18";
        sluggishness = -1;
        visible = 1;
        command = "AIMConvManager.convClicked(" @ %conv @ ");";
        text = %aimName;
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "./buttons/clear";
        drawText = 1;
        width = %recipWidth;
    };
    %status = new GuiBitmapCtrl("") {
        profile = 0 @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "29 26";
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        bitmap = "./ui/AIM_sel_avail";
        wrap = 0;
    };
    %statusButton = new GuiBitmapButtonCtrl("") {
        profile = 0 @ "ETSAIMDeselectedProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "35 26";
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        command = "AIMConvManager.convClicked(" @ %conv @ ");";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "./buttons/clear";
        drawText = 0;
    };
    %close = new GuiBitmapButtonCtrl("") {
        profile = 0 @ "GuiButtonProfile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "180 0";
        extent = "13 13";
        minExtent = "2 2";
        sluggishness = -1;
        visible = 1;
        command = "AIMConvManager.removeConv(" @ %conv @ ");";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "./buttons/close_m";
        drawText = 0;
    };
    %titlebar = new GuiControl("") {
        profile = 0 @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 544";
        extent = "200 26";
        minExtent = "8 2";
        visible = 1;
        sluggishness = 0.3;
        trgReachedCommand = "AIMConvManager.titlebarReachedTarget();";
        recipient = %recipient;
        status = %status;
        statusButton = %statusButton;
        close = %close;
    };
    %titlebar.add(%recipient);
    %titlebar.add(%status);
    %titlebar.add(%statusButton);
    %titlebar.add(%close);
    %theFirstResponder = Canvas.getFirstResponder();
    %conv.titlebar = %titlebar;
    %conv.contents = %contents;
    %this.convs = %conv @ %this.numConvs;
    AimConvContainer.add(%contents);
    AimConvContainer.add(%titlebar);
    %this.numConvs = (1.0 + %this.numConvs);
    if (isObject(%theFirstResponder)) {
        %theFirstResponder.makeFirstResponder(1);
    }
    if ((1.0 == %this.numConvs)) {
        %this.selectConvAtIndex(0);
    }
    %this.update();
    ConvBub.updateAutoMargins();
    return %conv;
};
function AIMConvManager::convClicked(%this, %conv) {
    %curConv = %this.getCurrentConv();
    if (isObject(%curConv)) {
    }
    if ((%conv.getId() == %curConv.getId())) {
        %this.selectConvAtIndex(-(1.0));
    }
    %this.selectConv(%conv);
    %conv.contents.textInput.makeFirstResponder(1);
};
function AIMConvManager::getConvWithName(%this, %aimName) {
    %idx = 0;
    if ((%this.numConvs < %idx)) {
        if ((%idx @ " " @ %this.convs.aimName $= %aimName)) {
            return %this.convs;
        }
        %idx = (1.0 + %idx);
    }
    return 0;
};
function AIMConvManager::getCurrentText(%this) {
    if ((0.0 > %this.numConvs)) {
        return %this.currentConvIndex.getValue(%this.convs.contents.textInput);
    }
    return "";
};
function AIMConvManager::clearCurrentText(%this) {
    if ((0.0 > %this.numConvs)) {
        %this.currentConvIndex.setValue(%this.convs.contents.textInput, "");
    }
};
function AIMConvManager::getCurrentConv(%this) {
    if ((0.0 > %this.numConvs)) {
        return %this.convs;
    }
    return 0;
};
function AIMConvManager::wakeUp(%this) {
    %this.Initialize();
    if ((0.0 > %this.numConvs)) {
        %this.selectCurrentConv();
    }
};
function AIMConvManager::updateContainer(%this) {
    %ypadding = 8;
    if ((0.0 <= %this.numConvs)) {
        AimConvContainer.setVisible(0);
        WindowManager.update();
        return;
    }
    %titleHeight = getWord(0.getExtent(%this.convs.titlebar), 1);
    %height = (%ypadding @ 0 + (getWord(%this.convs.titlebar.getPosition(), 1) + (%titleHeight * (1.0 - %this.numConvs))));
    if ((1.0 == %this.numConvs)) {
        %height = (%ypadding + %titleHeight);
    }
    %curConv = %this.getCurrentConv();
    if (isObject(%curConv)) {
        %contentsExtent = %curConv.contents.getExtent();
        %height = ((%titleHeight + getWord(%contentsExtent, 1)) + %height);
    }
    %height = (%titleHeight + %height);
    AimConvContainer.resize(getWord(AimConvContainer.getExtent(), 0), %height);
    AimConvContainer.setVisible(1);
    WindowManager.update();
};
function AIMConvManager::buildSpamString(%this, %prepend, %aimName, %link, %message) {
    if (!(%message $= "")) {
        %text = %prepend @ " " @ "( " @ %link @ " ):" @ %message;
    }
    %text = %prepend @ %link;
    return %text;
};
function AIMConvManager::filterMessage(%this, %conv, %message) {
    %filteredMessage = %message;
    return %filteredMessage;
};
function AIMConvManager::sendInvites(%this, %recipients, %links, %userMsg) {
    %count = getFieldCount(%recipients);
    %n = 0;
    if ((%count < %n)) {
        %aBuddy = getField(%recipients, %n);
        %link = getField(%links, %n);
        %inviteText = %this.buildSpamString($Net::AIMInvite, %aBuddy, %link, %userMsg);
        aimSend(%aBuddy, %inviteText);
        %n = (1.0 + %n);
    }
    MessageBoxOK("Invites Sent", %count @ " " @ "buddies have been invited to join" @ " " @ $ETS::AppName, "");
};
function AIMConvManager::inviteAll(%this, %userMsg) {
    %buddies = BuddyHudWin.onlineBuddiesToString();
    %this.prepareToSendInvites(%buddies, %userMsg);
};
function AIMConvManager::prepareToSendInvites(%this, %recipients, %userMsg) {
    %request = new ManagerRequest("") {
        className = 0 @ "GetAIMInviteURLsRequest";
        userMsg = %userMsg;
    };
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%request);
    }
    %url = $Net::ClientServiceURL @ "/GetAIMInviteURLs" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token);
    %count = getFieldCount(%recipients);
    %url = %url @ "&urlCount=" @ %count;
    log("network", "info", getScopeName() @ ":" @ %url);
    %request.setURL(%url);
    %request.recipients = %recipients;
    %request.start();
};
function GetAIMInviteURLsRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() @ ":" @ %status);
    if ((%status $= "fail")) {
        warn("network", getScopeName() @ ": request failed: " @ %this.getValue("statusMessage"));
        MessageBoxOK("Invite failed", "Couldn't get unique invite URLs. Please try again later.", "");
    }
    %count = %this.getValue("urlCount");
    %links = "";
    %i = 0;
    if ((%count < %i)) {
        %key = "url" @ %i;
        %val = %this.getValue(%key);
        %links = %links @ "\t" @ %val;
        %i = (1.0 + %i);
    }
    AIMConvManager.sendInvites(%this.recipients, %links, %this.userMsg);
    %this.schedule(0, "delete");
};
function GetAIMInviteURLsRequest::onError(%this, %unused, %errMsg) {
    error("network", getScopeName() @ ":" @ %errMsg);
    %this.schedule(0, "delete");
};
function AIMConvManager::sendMessage(%this) {
    %conv = %this.getCurrentConv();
    %message = %this.getCurrentText();
    if (!(%message $= "")) {
        %sendMessage = %this.filterMessage(%conv, %message);
        %this.spamDict.put(%conv.aimName, 1);
        aimSend(%conv.aimName, %sendMessage);
        %prefix = (%conv.contents.mlText.getText() $= "") ? "" : "\n";
        %toBottom = %conv.contents.scroll.isAtBottom();
        %conv.contents.mlText.addText(%prefix @ "<spush><color:bebeee>" @ %message @ "<spop>", 1, %toBottom);
        %this.totalMessagesSent = (1.0 + %this.totalMessagesSent);
        %conv.newMessage = 0;
        %this.update();
    }
    %this.clearCurrentText();
};
function AIMConvManager::talkTo(%this, %aimName) {
    %conv = %this.newConv(%aimName);
    if (%conv) {
        %this.selectConv(%conv);
        %conv.contents.textInput.makeFirstResponder(1);
    }
    echo("Failed to talk to " @ %aimName @ ": too many conversations open");
};
function AIMConvManager::receivedMessage(%this, %aimName, %message) {
    %conv = %this.newConv(%aimName);
    if (%conv) {
        %prefix = (%conv.contents.mlText.getText() $= "") ? "" : "\n";
        %toBottom = %conv.contents.scroll.isAtBottom();
        %conv.contents.mlText.addText(%prefix @ "<spush><color:ee8fee>" @ %message @ "<spop>", 1, %toBottom);
        %conv.newMessage = 1;
        %this.update();
    }
    echo("Received message from " @ %aimName @ ": " @ %message);
    if (!(isForegroundWindow())) {
    }
    if (isIdle()) {
    }
    if (!(PlayGui.canPlayerSeeWorld())) {
        if ($UserPref::Audio::NotifyChat) {
            alxPlay(AudioIm_MessageIn);
        }
    }
};
function AIMConvManager::closeAllConvs(%this) {
    %n = (1.0 - %this.numConvs);
    if ((0.0 >= %n)) {
        %this.removeConvAtIndex(%n);
        %n = (1.0 - %n);
    }
};
%this.stateMapping = "offline avail away away" @ AIMConvManager;
function AIMConvManager::buddyStateChanged(%this, %name, %state) {
    %conv = %this.getConvWithName(%name);
    if (%conv) {
        if ((-(1.0) == %state)) {
            %state = 0;
        }
        %conv.status = getWord(%this.stateMapping, %state);
        %this.update();
    }
};
