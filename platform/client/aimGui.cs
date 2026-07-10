if (!(isObject())) {
    new ScriptObject(AIMConvManager);
    if (isObject()) {
        add();
    }
}
function AIMConvManager::Initialize(%this) {
    if (!(initialized)) {
        spamDict = StringMap @ new ""() @ %this;
        0;
        if (isObject()) {
            spamDict.add();
        }
        numConvs = %this @ 0 @ %this;
        MissionCleanup;
        currentConvIndex = MissionCleanup @ -(1.0) @ %this;
        %this;
        if ((%this <= maxConvs)) {
            maxConvs = 0.0 @ 20 @ %this;
            AIMConvManager;
        }
        movingBars = MissionCleanup @ 0 @ %this;
        MissionCleanup;
        totalMessagesSent = AIMConvManager @ 0 @ %this;
        initialized = 1 @ %this;
    }
    %this.update();
};
function AIMConvManager::selectConvAtIndex(%this, %convIndex) {
    if ((%this >= currentConvIndex)) {
    }
    if ((%this < currentConvIndex)) {
    }
    if ((%this != currentConvIndex)) {
        contents.setVisible(0);
        recipient.setProfile();
        newMessage = %this @ currentConvIndex @ %this @ convs;
        ETSAIMDeselectedProfile @ 0;
    }
    if ((0.0 >= %convIndex)) {
    }
    if ((numConvs < %convIndex)) {
        recipient.setProfile();
        newMessage = ETSAIMSelectedProfile @ 0 @ %convIndex @ %this @ convs;
        titlebar;
    }
    currentConvIndex = convs @ %convIndex @ %this;
    %this @ %convIndex @ %this;
    %this.update();
};
function AIMConvManager::selectCurrentConv(%this) {
    %this.selectConvAtIndex(currentConvIndex);
};
function AIMConvManager::selectConv(%this, %convId) {
    %idx = 0;
    if ((numConvs < %idx)) {
        if ((%convId @ %idx @ %this == convs.getId())) {
            %this.selectConvAtIndex(%idx);
            return %this;
        }
        %idx = (1.0 + %idx);
    }
};
function AIMConvManager::nextConv(%this) {
    if ((%this > numConvs)) {
        %this.selectConvAtIndex((1.0 % (%this + currentConvIndex)));
    }
};
function AIMConvManager::previousConv(%this) {
    if ((%this > numConvs)) {
        %this.selectConvAtIndex((numConvs % (1.0 + (%this - currentConvIndex))));
    }
};
function AIMConvManager::removeConvAtIndex(%this, %convIndex) {
    if ((0.0 >= %convIndex)) {
    }
    if ((numConvs < %convIndex)) {
        %conv = convs;
        %this @ %convIndex @ %this;
        contents.setVisible(0);
        titlebar.setVisible(0);
        contents.delete();
        titlebar.delete();
        %conv.delete();
        numConvs = (%this - numConvs);
        1.0;
        %idx = %convIndex;
        %conv;
        if ((numConvs < %idx)) {
            convs = %conv @ %this @ (1.0 + %idx) @ %this @ convs @ %idx @ %this;
            %conv;
            %idx = (1.0 + %idx);
            %conv;
        }
        convs = (numConvs < %idx) @ 0 @ %this @ numConvs @ %this;
        %this;
        if ((%this == numConvs)) {
            currentConvIndex = 0.0 @ -(1.0) @ %this;
        }
        if ((%this >= currentConvIndex)) {
            %this.selectConvAtIndex((%this - numConvs));
        }
        if ((%this > currentConvIndex)) {
            %this.selectConvAtIndex((%this - currentConvIndex));
        }
        if ((%this == currentConvIndex)) {
            %this.selectCurrentConv();
        }
    }
    %this.update();
    updateAutoMargins();
};
function AIMConvManager::removeConv(%this, %convId) {
    %idx = 0;
    if ((numConvs < %idx)) {
        if ((%convId @ %idx @ %this == convs.getId())) {
            %this.removeConvAtIndex(%idx);
            return %this;
        }
        %idx = (1.0 + %idx);
    }
};
function AIMConvManager::removeCurrentConv(%this) {
    %this.removeConvAtIndex(currentConvIndex);
};
function AIMConvManager::update(%this) {
    if ((%this == numConvs)) {
    }
    if ((%this < currentConvIndex)) {
    }
    if ((Canvas == getFirstResponder())) {
        1.makeFirstResponder();
    }
    %this.updateContainer();
    %ypos = 20;
    TheShapeNameHud;
    movingBars = 0.0 @ 0 @ %this;
    0.0;
    %idx = 0;
    0.0;
    if ((numConvs < %idx)) {
        %conv = convs;
        %this @ %idx @ %this;
        %titlebar = titlebar;
        %conv;
        %titlebar.setTrgPosition(0, %ypos);
        %ypos = (24.0 + %ypos);
        movingBars = (%this + movingBars);
        1.0;
        %contents = contents;
        %conv;
        %contents.setTrgPosition(getWord(%contents.getTrgPosition(), 0), %ypos);
        if ((currentConvIndex == %idx)) {
            if (newMessage) {
            }
            %status = status;
            %conv;
            status.setBitmap(%titlebar @ "platform/client/ui/AIM_sel_" @ %status);
            status.resize(0, 0, 29, 26);
            statusButton.resize(0, 0, 35, 26);
            recipient.reposition(35, 2);
            close.setBitmap("platform/client/buttons/close_m");
            close.resize(180, 6, 13, 13);
            %ypos = (getWord(%contents.getExtent(), 1) + %ypos);
            %titlebar;
        }
        if (newMessage) {
        }
        %status = status;
        %conv;
        status.setBitmap(%titlebar @ "platform/client/ui/AIM_" @ %status);
        status.resize(11, 5, 30, 15);
        statusButton.resize(0, 0, 45, 26);
        recipient.reposition(45, 3);
        close.setBitmap("platform/client/buttons/close_s");
        close.resize((width + 57.0), 8, 11, 11);
        %idx = (1.0 + %idx);
        recipient;
    }
};
function AIMConvManager::finishUpdate(%this) {
    %curConv = convs;
    %this @ currentConvIndex @ %this;
    if (isObject(%curConv)) {
        contents.setVisible(1);
        if (!(isObject(getFirstResponder()))) {
            textInput.makeFirstResponder(1);
        }
    }
};
function AIMConvManager::titlebarReachedTarget(%this) {
    if ((%this > movingBars)) {
        movingBars = (%this - movingBars);
        1.0;
        if ((%this == movingBars)) {
            %this.finishUpdate();
        }
    }
};
function AIMConvManager::newConv(%this, %aimName) {
    %conv = %this.getConvWithName(%aimName);
    if (%conv) {
        return %conv;
    }
    class = ScriptObject @ new ""() @ "AIMConversation";
    0;
    %conv = ;
    if (isObject()) {
        %conv.add();
    }
    aimName = MissionCleanup @ %aimName @ %conv;
    MissionCleanup;
    status = "offline" @ %conv;
    %i = 0;
    if ((aimBuddyCount() < %i)) {
        if ((%conv $= aimName)) {
            %state = aimGetBuddyState(%i);
            aimGetBuddyName(%i);
            if ((-(1.0) == %state)) {
                %state = 0;
            }
            status = %this @ getWord(stateMapping, %state) @ %conv;
        }
        %i = (1.0 + %i);
    }
    newMessage = (aimBuddyCount() < %i) @ 0 @ %conv;
    profile = GuiTextEditCtrl @ new ""() @ "AIMTextEditProfile";
    0;
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
    %textInput = ;
    profile = GuiScrollCtrl @ new ""() @ "ETSAimMessageScrollProfile";
    0;
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
    %scroll = ;
    profile = GuiMLTextCtrl @ new ""() @ "ETSAIMMessageProfile";
    0;
    horizSizing = "relative";
    vertSizing = "relative";
    position = "1 1";
    extent = "147 92";
    minExtent = "8 92";
    visible = 1;
    lineSpacing = 0;
    allowColorChars = 1;
    stripTagsOnCopy = 1;
    %mlText = ;
    %scroll.add(%mlText);
    profile = GuiControl @ new ""() @ "ETSAIMTabProfile";
    0;
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
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "top";
    position = "2 101";
    extent = "8 13";
    minExtent = "8 13";
    sluggishness = -1;
    visible = 1;
    bitmap = "./ui/AIM_bracket_left";
    wrap = 0;
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "top";
    position = "157 101";
    extent = "8 13";
    minExtent = "8 13";
    sluggishness = -1;
    visible = 1;
    bitmap = "./ui/AIM_bracket_right";
    wrap = 0;
    %contents = ;
    %contents.add(%textInput);
    %contents.add(%scroll);
    profile = GuiTextCtrl @ new ""() @ "ETSAIMDeselectedProfile";
    0;
    horizSizing = "left";
    vertSizing = "top";
    position = "0 0";
    extent = "18 18";
    minExtent = "11 11";
    sluggishness = -1;
    visible = 0;
    maxLength = 255;
    %dummy = ;
    %dummy.setText(%aimName);
    %recipWidth = getWord(%dummy.getExtent(), 0);
    %dummy.delete();
    profile = GuiBitmapButtonCtrl @ new ""() @ "ETSAIMDeselectedProfile";
    0;
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
    %recipient = ;
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = "29 26";
    minExtent = "8 8";
    sluggishness = -1;
    visible = 1;
    bitmap = "./ui/AIM_sel_avail";
    wrap = 0;
    %status = ;
    profile = GuiBitmapButtonCtrl @ new ""() @ "ETSAIMDeselectedProfile";
    0;
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
    %statusButton = ;
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiButtonProfile";
    0;
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
    %close = ;
    profile = GuiControl @ new ""() @ "GuiDefaultProfile";
    0;
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
    %titlebar = ;
    %titlebar.add(%recipient);
    %titlebar.add(%status);
    %titlebar.add(%statusButton);
    %titlebar.add(%close);
    %theFirstResponder = getFirstResponder();
    Canvas;
    titlebar = %titlebar @ %conv;
    contents = %contents @ %conv;
    convs = %conv @ %this @ numConvs @ %this;
    %contents.add();
    %titlebar.add();
    numConvs = (%this + numConvs);
    1.0;
    if (isObject(%theFirstResponder)) {
        %theFirstResponder.makeFirstResponder(1);
    }
    if ((%this == numConvs)) {
        %this.selectConvAtIndex(0);
    }
    %this.update();
    updateAutoMargins();
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
    textInput.makeFirstResponder(1);
};
function AIMConvManager::getConvWithName(%this, %aimName) {
    %idx = 0;
    if ((numConvs < %idx)) {
        if ((convs SPC aimName $= %aimName)) {
            return convs;
        }
        %idx = (1.0 + %idx);
    }
    return 0;
};
function AIMConvManager::getCurrentText(%this) {
    if ((%this > numConvs)) {
        return textInput.getValue();
    }
    return "";
};
function AIMConvManager::clearCurrentText(%this) {
    if ((%this > numConvs)) {
        textInput.setValue("");
    }
};
function AIMConvManager::getCurrentConv(%this) {
    if ((%this > numConvs)) {
        return convs;
    }
    return 0;
};
function AIMConvManager::wakeUp(%this) {
    %this.Initialize();
    if ((%this > numConvs)) {
        %this.selectCurrentConv();
    }
};
function AIMConvManager::updateContainer(%this) {
    %ypadding = 8;
    if ((%this <= numConvs)) {
        0.setVisible();
        update();
        return WindowManager;
    }
    %titleHeight = getWord(titlebar.getExtent(), 1);
    convs;
    %height = (getWord(titlebar.getPosition(), 1) + (%titleHeight + (1.0 * (%this - numConvs))));
    convs;
    if ((%this == numConvs)) {
        %height = (%ypadding + %titleHeight);
        1.0;
    }
    %curConv = %this.getCurrentConv();
    %ypadding @ 0 @ %this;
    if (isObject(%curConv)) {
        %contentsExtent = contents.getExtent();
        %curConv;
        %height = ((%titleHeight + getWord(%contentsExtent, 1)) + %height);
        0 @ %this;
    }
    %height = (%titleHeight + %height);
    getWord(getExtent(), 0).resize(%height);
    1.setVisible();
    update();
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
    %buddies = onlineBuddiesToString();
    BuddyHudWin;
    %this.prepareToSendInvites(%buddies, %userMsg);
};
function AIMConvManager::prepareToSendInvites(%this, %recipients, %userMsg) {
    className = ManagerRequest @ new ""() @ "GetAIMInviteURLsRequest";
    0;
    userMsg = %userMsg;
    %request = ;
    if (isObject()) {
        %request.add();
    }
    %url = MissionCleanup @ MissionCleanup @ $Net::ClientServiceURL @ "/GetAIMInviteURLs" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token);
    %count = getFieldCount(%recipients);
    %url = %url @ "&urlCount=" @ %count;
    log("network", "info", getScopeName() @ ":" @ %url);
    %request.setURL(%url);
    recipients = %recipients @ %request;
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
    recipients.sendInvites(%links, userMsg);
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
        spamDict.put(aimName, 1);
        aimSend(aimName, %sendMessage);
        %prefix = (contents SPC mlText.getText() $= "") ? "" : "\n";
        %conv;
        %toBottom = scroll.isAtBottom();
        contents;
        mlText.addText(%conv @ %conv @ contents @ %prefix @ "<spush><color:bebeee>" @ %message @ "<spop>", 1, %toBottom);
        totalMessagesSent = (%this + totalMessagesSent);
        1.0;
        newMessage = %conv @ 0 @ %conv;
        %conv;
        %this.update();
    }
    %this.clearCurrentText();
};
function AIMConvManager::talkTo(%this, %aimName) {
    %conv = %this.newConv(%aimName);
    if (%conv) {
        %this.selectConv(%conv);
        textInput.makeFirstResponder(1);
    }
    echo(%conv @ contents @ "Failed to talk to " @ %aimName @ ": too many conversations open");
};
function AIMConvManager::receivedMessage(%this, %aimName, %message) {
    %conv = %this.newConv(%aimName);
    if (%conv) {
        %prefix = (contents SPC mlText.getText() $= "") ? "" : "\n";
        %conv;
        %toBottom = scroll.isAtBottom();
        contents;
        mlText.addText(%conv @ %conv @ contents @ %prefix @ "<spush><color:ee8fee>" @ %message @ "<spop>", 1, %toBottom);
        newMessage = 1 @ %conv;
        %this.update();
    }
    echo("Received message from " @ %aimName @ ": " @ %message);
    if (!(isForegroundWindow())) {
    }
    if (isIdle()) {
    }
    if (!(canPlayerSeeWorld())) {
        if ($UserPref::Audio::NotifyChat) {
            alxPlay();
        }
    }
};
function AIMConvManager::closeAllConvs(%this) {
    %n = (%this - numConvs);
    1.0;
    if ((0.0 >= %n)) {
        %this.removeConvAtIndex(%n);
        %n = (1.0 - %n);
    }
};
stateMapping = "offline avail away away" @ AIMConvManager;
function AIMConvManager::buddyStateChanged(%this, %name, %state) {
    %conv = %this.getConvWithName(%name);
    if (%conv) {
        if ((-(1.0) == %state)) {
            %state = 0;
        }
        status = %this @ getWord(stateMapping, %state) @ %conv;
        %this.update();
    }
};
