if (!(isObject(AIMConvManager))) {
    new ScriptObject(AIMConvManager);
    if (isObject(MissionCleanup)) {
        AIMConvManager.add(MissionCleanup);
    }
}
function AIMConvManager::Initialize(%this) {
    if (!(%this.initialized)) {
        %this.spamDict = 0 @ new StringMap("");;
        if (isObject(MissionCleanup)) {
            %this.spamDict.add(MissionCleanup);
        }
        %this.numConvs = 0;
        %this.currentConvIndex = -(1.0);
        if ((%this.maxConvs <= 0.0)) {
            %this.maxConvs = 20;
        }
        %this.movingBars = 0;
        %this.totalMessagesSent = 0;
        %this.initialized = 1;
    }
    %this.update();
};
function AIMConvManager::selectConvAtIndex(%this, %convIndex) {
    if ((%this.currentConvIndex >= 0.0)) {
    }
    if ((%this.currentConvIndex < %this.numConvs)) {
    }
    if ((%this.currentConvIndex != %convIndex)) {
        0.setVisible(%this.currentConvIndex, %this.convs.contents);
        %this.convs.titlebar.recipient.setProfile(%this.currentConvIndex);
        %this.convs.newMessage = ETSAIMDeselectedProfile @ 0 @ %this.currentConvIndex;
    }
    if ((%convIndex >= 0.0)) {
    }
    if ((%convIndex < %this.numConvs)) {
        %this.convs.titlebar.recipient.setProfile(%convIndex);
        %this.convs.newMessage = ETSAIMSelectedProfile @ 0 @ %convIndex;
    }
    %this.currentConvIndex = %convIndex;
    %this.update();
};
function AIMConvManager::selectCurrentConv(%this) {
    %this.currentConvIndex.selectConvAtIndex(%this);
};
function AIMConvManager::selectConv(%this, %convId) {
    %idx = 0;
    while ((%idx < %this.numConvs)) {
        if ((%this.convs.getId() == %convId @ %idx)) {
            %idx.selectConvAtIndex(%this);
            return;
        }
        %idx = (%idx + 1.0);
    }
};
function AIMConvManager::nextConv(%this) {
    if ((%this.numConvs > 0.0)) {
        ((%this.currentConvIndex + 1.0) % %this.numConvs).selectConvAtIndex(%this);
    }
};
function AIMConvManager::previousConv(%this) {
    if ((%this.numConvs > 0.0)) {
        (((%this.currentConvIndex - 1.0) + %this.numConvs) % %this.numConvs).selectConvAtIndex(%this);
    }
};
function AIMConvManager::removeConvAtIndex(%this, %convIndex) {
    if ((%convIndex >= 0.0)) {
    }
    if ((%convIndex < %this.numConvs)) {
        %conv = %this.convs;
        %convIndex;
        0.setVisible(%conv.contents);
        0.setVisible(%conv.titlebar);
        %conv.contents.delete();
        %conv.titlebar.delete();
        %conv.delete();
        %this.numConvs = (%this.numConvs - 1.0);
        %idx = %convIndex;
        while ((%idx < %this.numConvs)) {
            %this.convs = (%idx + 1.0) @ %this.convs @ %idx;
            %idx = (%idx + 1.0);
        }
        %this.convs = (%idx < %this.numConvs) @ 0 @ %this.numConvs;
        if ((%this.numConvs == 0.0)) {
            %this.currentConvIndex = -(1.0);
        }
        if ((%this.currentConvIndex >= %this.numConvs)) {
            (%this.numConvs - 1.0).selectConvAtIndex(%this);
        }
        if ((%this.currentConvIndex > %convIndex)) {
            (%this.currentConvIndex - 1.0).selectConvAtIndex(%this);
        }
        if ((%this.currentConvIndex == %convIndex)) {
            %this.selectCurrentConv();
        }
    }
    %this.update();
    ConvBub.updateAutoMargins();
};
function AIMConvManager::removeConv(%this, %convId) {
    %idx = 0;
    while ((%idx < %this.numConvs)) {
        if ((%this.convs.getId() == %convId @ %idx)) {
            %idx.removeConvAtIndex(%this);
            return;
        }
        %idx = (%idx + 1.0);
    }
};
function AIMConvManager::removeCurrentConv(%this) {
    %this.currentConvIndex.removeConvAtIndex(%this);
};
function AIMConvManager::update(%this) {
    if ((%this.numConvs == 0.0)) {
    }
    if ((%this.currentConvIndex < 0.0)) {
    }
    if ((Canvas.getFirstResponder() == 0.0)) {
        1.makeFirstResponder(TheShapeNameHud);
    }
    %this.updateContainer();
    %ypos = 20;
    %this.movingBars = 0;
    %idx = 0;
    while ((%idx < %this.numConvs)) {
        %conv = %this.convs;
        %idx;
        %titlebar = %conv.titlebar;
        %ypos.setTrgPosition(%titlebar, 0);
        %ypos = (%ypos + 24.0);
        %this.movingBars = (%this.movingBars + 1.0);
        %contents = %conv.contents;
        %ypos.setTrgPosition(%contents, getWord(%contents.getTrgPosition(), 0));
        if ((%idx == %this.currentConvIndex)) {
            if (%conv.newMessage) {
            }
            %status = %conv.status;
            "new_msg";
            "platform/client/ui/AIM_sel_" @ %status.setBitmap(%titlebar.status);
            26.resize(%titlebar.status, 0, 0, 29);
            26.resize(%titlebar.statusButton, 0, 0, 35);
            2.reposition(%titlebar.recipient, 35);
            "platform/client/buttons/close_m".setBitmap(%titlebar.close);
            13.resize(%titlebar.close, 180, 6, 13);
            %ypos = (%ypos + getWord(%contents.getExtent(), 1));
        }
        if (%conv.newMessage) {
        }
        %status = %conv.status;
        "new_msg";
        "platform/client/ui/AIM_" @ %status.setBitmap(%titlebar.status);
        15.resize(%titlebar.status, 11, 5, 30);
        26.resize(%titlebar.statusButton, 0, 0, 45);
        3.reposition(%titlebar.recipient, 45);
        "platform/client/buttons/close_s".setBitmap(%titlebar.close);
        11.resize(%titlebar.close, (57.0 + %titlebar.recipient.width), 8, 11);
        %idx = (%idx + 1.0);
    }
};
function AIMConvManager::finishUpdate(%this) {
    %curConv = %this.convs;
    %this.currentConvIndex;
    if (isObject(%curConv)) {
        1.setVisible(%curConv.contents);
        if (!(isObject(Canvas.getFirstResponder()))) {
            1.makeFirstResponder(%curConv.contents.textInput);
        }
    }
};
function AIMConvManager::titlebarReachedTarget(%this) {
    if ((%this.movingBars > 0.0)) {
        %this.movingBars = (%this.movingBars - 1.0);
        if ((%this.movingBars == 0.0)) {
            %this.finishUpdate();
        }
    }
};
function AIMConvManager::newConv(%this, %aimName) {
    %conv = %aimName.getConvWithName(%this);
    if (%conv) {
        return %conv;
    }
    %conv = new ScriptObject("") {
        class = 0 @ "AIMConversation";
    };
    if (isObject(MissionCleanup)) {
        %conv.add(MissionCleanup);
    }
    %conv.aimName = %aimName;
    %conv.status = "offline";
    %i = 0;
    while ((%i < aimBuddyCount())) {
        if ((aimGetBuddyName(%i) $= %conv.aimName)) {
            %state = aimGetBuddyState(%i);
            if ((%state == -(1.0))) {
                %state = 0;
            }
            %conv.status = getWord(%this.stateMapping, %state);
        }
        %i = (%i + 1.0);
    }
    %conv.newMessage = (%i < aimBuddyCount()) @ 0;
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
    %mlText.add(%scroll);
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
    %textInput.add(%contents);
    %scroll.add(%contents);
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
    %aimName.setText(%dummy);
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
    %recipient.add(%titlebar);
    %status.add(%titlebar);
    %statusButton.add(%titlebar);
    %close.add(%titlebar);
    %theFirstResponder = Canvas.getFirstResponder();
    %conv.titlebar = %titlebar;
    %conv.contents = %contents;
    %this.convs = %conv @ %this.numConvs;
    %contents.add(AimConvContainer);
    %titlebar.add(AimConvContainer);
    %this.numConvs = (%this.numConvs + 1.0);
    if (isObject(%theFirstResponder)) {
        1.makeFirstResponder(%theFirstResponder);
    }
    if ((%this.numConvs == 1.0)) {
        0.selectConvAtIndex(%this);
    }
    %this.update();
    ConvBub.updateAutoMargins();
    return %conv;
};
function AIMConvManager::convClicked(%this, %conv) {
    %curConv = %this.getCurrentConv();
    if (isObject(%curConv)) {
    }
    if ((%curConv.getId() == %conv.getId())) {
        -(1.0).selectConvAtIndex(%this);
    }
    %conv.selectConv(%this);
    1.makeFirstResponder(%conv.contents.textInput);
};
function AIMConvManager::getConvWithName(%this, %aimName) {
    %idx = 0;
    while ((%idx < %this.numConvs)) {
        if ((%idx @ " " @ %this.convs.aimName $= %aimName)) {
            return %this.convs;
        }
        %idx = (%idx + 1.0);
    }
    return 0;
};
function AIMConvManager::getCurrentText(%this) {
    if ((%this.numConvs > 0.0)) {
        return %this.convs.contents.textInput.getValue(%this.currentConvIndex);
    }
    return "";
};
function AIMConvManager::clearCurrentText(%this) {
    if ((%this.numConvs > 0.0)) {
        "".setValue(%this.currentConvIndex, %this.convs.contents.textInput);
    }
};
function AIMConvManager::getCurrentConv(%this) {
    if ((%this.numConvs > 0.0)) {
        return %this.convs;
    }
    return 0;
};
function AIMConvManager::wakeUp(%this) {
    %this.Initialize();
    if ((%this.numConvs > 0.0)) {
        %this.selectCurrentConv();
    }
};
function AIMConvManager::updateContainer(%this) {
    %ypadding = 8;
    if ((%this.numConvs <= 0.0)) {
        0.setVisible(AimConvContainer);
        WindowManager.update();
        return;
    }
    %titleHeight = getWord(%this.convs.titlebar.getExtent(0), 1);
    %height = ((((%this.numConvs - 1.0) * %titleHeight) + getWord(%this.convs.titlebar.getPosition(), 1)) + %ypadding @ 0);
    if ((%this.numConvs == 1.0)) {
        %height = (%titleHeight + %ypadding);
    }
    %curConv = %this.getCurrentConv();
    if (isObject(%curConv)) {
        %contentsExtent = %curConv.contents.getExtent();
        %height = (%height + (getWord(%contentsExtent, 1) + %titleHeight));
    }
    %height = (%height + %titleHeight);
    %height.resize(AimConvContainer, getWord(AimConvContainer.getExtent(), 0));
    1.setVisible(AimConvContainer);
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
    while ((%n < %count)) {
        %aBuddy = getField(%recipients, %n);
        %link = getField(%links, %n);
        %inviteText = %userMsg.buildSpamString(%this, $Net::AIMInvite, %aBuddy, %link);
        aimSend(%aBuddy, %inviteText);
        %n = (%n + 1.0);
    }
    MessageBoxOK("Invites Sent", %count @ " " @ "buddies have been invited to join" @ " " @ $ETS::AppName, "");
};
function AIMConvManager::inviteAll(%this, %userMsg) {
    %buddies = BuddyHudWin.onlineBuddiesToString();
    %userMsg.prepareToSendInvites(%this, %buddies);
};
function AIMConvManager::prepareToSendInvites(%this, %recipients, %userMsg) {
    %request = new ManagerRequest("") {
        className = 0 @ "GetAIMInviteURLsRequest";
        userMsg = %userMsg;
    };
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    %url = $Net::ClientServiceURL @ "/GetAIMInviteURLs" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token);
    %count = getFieldCount(%recipients);
    %url = %url @ "&urlCount=" @ %count;
    log("network", "info", getScopeName() @ ":" @ %url);
    %url.setURL(%request);
    %request.recipients = %recipients;
    %request.start();
};
function GetAIMInviteURLsRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() @ ":" @ %status);
    if ((%status $= "fail")) {
        warn("network", getScopeName() @ ": request failed: " @ "statusMessage".getValue(%this));
        MessageBoxOK("Invite failed", "Couldn't get unique invite URLs. Please try again later.", "");
    }
    %count = "urlCount".getValue(%this);
    %links = "";
    %i = 0;
    while ((%i < %count)) {
        %key = "url" @ %i;
        %val = %key.getValue(%this);
        %links = %links @ "\t" @ %val;
        %i = (%i + 1.0);
    }
    %this.userMsg.sendInvites(AIMConvManager, %this.recipients, %links);
    "delete".schedule(%this, 0);
};
function GetAIMInviteURLsRequest::onError(%this, %unused, %errMsg) {
    error("network", getScopeName() @ ":" @ %errMsg);
    "delete".schedule(%this, 0);
};
function AIMConvManager::sendMessage(%this) {
    %conv = %this.getCurrentConv();
    %message = %this.getCurrentText();
    if (!(%message $= "")) {
        %sendMessage = %message.filterMessage(%this, %conv);
        1.put(%this.spamDict, %conv.aimName);
        aimSend(%conv.aimName, %sendMessage);
        %prefix = (%conv.contents.mlText.getText() $= "") ? "" : "\n";
        %toBottom = %conv.contents.scroll.isAtBottom();
        %toBottom.addText(%conv.contents.mlText, %prefix @ "<spush><color:bebeee>" @ %message @ "<spop>", 1);
        %this.totalMessagesSent = (%this.totalMessagesSent + 1.0);
        %conv.newMessage = 0;
        %this.update();
    }
    %this.clearCurrentText();
};
function AIMConvManager::talkTo(%this, %aimName) {
    %conv = %aimName.newConv(%this);
    if (%conv) {
        %conv.selectConv(%this);
        1.makeFirstResponder(%conv.contents.textInput);
    }
    echo("Failed to talk to " @ %aimName @ ": too many conversations open");
};
function AIMConvManager::receivedMessage(%this, %aimName, %message) {
    %conv = %aimName.newConv(%this);
    if (%conv) {
        %prefix = (%conv.contents.mlText.getText() $= "") ? "" : "\n";
        %toBottom = %conv.contents.scroll.isAtBottom();
        %toBottom.addText(%conv.contents.mlText, %prefix @ "<spush><color:ee8fee>" @ %message @ "<spop>", 1);
        %conv.newMessage = 1;
        %this.update();
    }
    echo("Received message from " @ %aimName @ ": " @ %message);
    if (!(isForegroundWindow())) {
    }
    if (isIdle()) {
    }
    if (!(PlayGui.canPlayerSeeWorld()) && $UserPref::Audio::NotifyChat) {
        alxPlay(AudioIm_MessageIn);
    }
};
function AIMConvManager::closeAllConvs(%this) {
    %n = (%this.numConvs - 1.0);
    while ((%n >= 0.0)) {
        %n.removeConvAtIndex(%this);
        %n = (%n - 1.0);
    }
};
%this.stateMapping = "offline avail away away" @ AIMConvManager;
function AIMConvManager::buddyStateChanged(%this, %name, %state) {
    %conv = %name.getConvWithName(%this);
    if (%conv) {
        if ((%state == -(1.0))) {
            %state = 0;
        }
        %conv.status = getWord(%this.stateMapping, %state);
        %this.update();
    }
};
