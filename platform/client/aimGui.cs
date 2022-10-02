if (!isObject(AIMConvManager))
{
    new ScriptObject(AIMConvManager);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(AIMConvManager);
    }
}
function AIMConvManager::Initialize(%this)
{
    if (!%this.initialized)
    {
        %this.spamDict = new StringMap();
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(%this.spamDict);
        }
        %this.numConvs = 0;
        %this.currentConvIndex = -1;
        if (%this.maxConvs <= 0)
        {
            %this.maxConvs = 20;
        }
        %this.movingBars = 0;
        %this.totalMessagesSent = 0;
        %this.initialized = 1;
    }
    %this.update();
    return ;
}
function AIMConvManager::selectConvAtIndex(%this, %convIndex)
{
    if (((%this.currentConvIndex >= 0) && (%this.currentConvIndex < %this.numConvs)) && (%this.currentConvIndex != %convIndex))
    {
        %this.convs[%this.currentConvIndex].contents.setVisible(0);
        %this.convs[%this.currentConvIndex].titlebar.recipient.setProfile(ETSAIMDeselectedProfile);
        %this.convs[%this.currentConvIndex].newMessage = 0;
    }
    if ((%convIndex >= 0) && (%convIndex < %this.numConvs))
    {
        %this.convs[%convIndex].titlebar.recipient.setProfile(ETSAIMSelectedProfile);
        %this.convs[%convIndex].newMessage = 0;
    }
    %this.currentConvIndex = %convIndex;
    %this.update();
    return ;
}
function AIMConvManager::selectCurrentConv(%this)
{
    %this.selectConvAtIndex(%this.currentConvIndex);
    return ;
}
function AIMConvManager::selectConv(%this, %convId)
{
    %idx = 0;
    while (%idx < %this.numConvs)
    {
        if (%this.convs[%idx].getId() == %convId)
        {
            %this.selectConvAtIndex(%idx);
            return ;
        }
        %idx = %idx + 1;
    }
}

function AIMConvManager::nextConv(%this)
{
    if (%this.numConvs > 0)
    {
        %this.selectConvAtIndex((%this.currentConvIndex + 1) % %this.numConvs);
    }
    return ;
}
function AIMConvManager::previousConv(%this)
{
    if (%this.numConvs > 0)
    {
        %this.selectConvAtIndex(((%this.currentConvIndex - 1) + %this.numConvs) % %this.numConvs);
    }
    return ;
}
function AIMConvManager::removeConvAtIndex(%this, %convIndex)
{
    if ((%convIndex >= 0) && (%convIndex < %this.numConvs))
    {
        %conv = %this.convs[%convIndex];
        %conv.contents.setVisible(0);
        %conv.titlebar.setVisible(0);
        %conv.contents.delete();
        %conv.titlebar.delete();
        %conv.delete();
        %this.numConvs = %this.numConvs - 1;
        %idx = %convIndex;
        while (%idx < %this.numConvs)
        {
            %this.convs[%idx] = %this.convs[(%idx + 1)];
            %idx = %idx + 1;
        }
        %this.convs[%this.numConvs] = 0;
        if (%this.numConvs == 0)
        {
            %this.currentConvIndex = -1;
        }
        else
        {
            if (%this.currentConvIndex >= %this.numConvs)
            {
                %this.selectConvAtIndex(%this.numConvs - 1);
            }
            else
            {
                if (%this.currentConvIndex > %convIndex)
                {
                    %this.selectConvAtIndex(%this.currentConvIndex - 1);
                }
                else
                {
                    if (%this.currentConvIndex == %convIndex)
                    {
                        %this.selectCurrentConv();
                    }
                }
            }
        }
    }
    %this.update();
    ConvBub.updateAutoMargins();
    return ;
}
function AIMConvManager::removeConv(%this, %convId)
{
    %idx = 0;
    while (%idx < %this.numConvs)
    {
        if (%this.convs[%idx].getId() == %convId)
        {
            %this.removeConvAtIndex(%idx);
            return ;
        }
        %idx = %idx + 1;
    }
}

function AIMConvManager::removeCurrentConv(%this)
{
    %this.removeConvAtIndex(%this.currentConvIndex);
    return ;
}
function AIMConvManager::update(%this)
{
    if (((%this.numConvs == 0) || (%this.currentConvIndex < 0)) && (Canvas.getFirstResponder() == 0))
    {
        TheShapeNameHud.makeFirstResponder(1);
    }
    %this.updateContainer();
    %ypos = 20;
    %this.movingBars = 0;
    %idx = 0;
    while (%idx < %this.numConvs)
    {
        %conv = %this.convs[%idx];
        %titlebar = %conv.titlebar;
        %titlebar.setTrgPosition(0, %ypos);
        %ypos = %ypos + 24;
        %this.movingBars = %this.movingBars + 1;
        %contents = %conv.contents;
        %contents.setTrgPosition(getWord(%contents.getTrgPosition(), 0), %ypos);
        if (%idx == %this.currentConvIndex)
        {
            %status = %conv.newMessage ? "new_msg" : %conv;
            %titlebar.status.setBitmap("platform/client/ui/AIM_sel_" @ %status);
            %titlebar.status.resize(0, 0, 29, 26);
            %titlebar.statusButton.resize(0, 0, 35, 26);
            %titlebar.recipient.reposition(35, 2);
            %titlebar.close.setBitmap("platform/client/buttons/close_m");
            %titlebar.close.resize(180, 6, 13, 13);
            %ypos = %ypos + getWord(%contents.getExtent(), 1);
        }
        else
        {
            %status = %conv.newMessage ? "new_msg" : %conv;
            %titlebar.status.setBitmap("platform/client/ui/AIM_" @ %status);
            %titlebar.status.resize(11, 5, 30, 15);
            %titlebar.statusButton.resize(0, 0, 45, 26);
            %titlebar.recipient.reposition(45, 3);
            %titlebar.close.setBitmap("platform/client/buttons/close_s");
            %titlebar.close.resize(57 + %titlebar.recipient.width, 8, 11, 11);
        }
        %idx = %idx + 1;
    }
}

function AIMConvManager::finishUpdate(%this)
{
    %curConv = %this.convs[%this.currentConvIndex];
    if (isObject(%curConv))
    {
        %curConv.contents.setVisible(1);
        if (!isObject(Canvas.getFirstResponder()))
        {
            %curConv.contents.textInput.makeFirstResponder(1);
        }
    }
    return ;
}
function AIMConvManager::titlebarReachedTarget(%this)
{
    if (%this.movingBars > 0)
    {
        %this.movingBars = %this.movingBars - 1;
        if (%this.movingBars == 0)
        {
            %this.finishUpdate();
        }
    }
    return ;
}
function AIMConvManager::newConv(%this, %aimName)
{
    %conv = %this.getConvWithName(%aimName);
    if (%conv)
    {
        return %conv;
    }
    %conv = new ScriptObject();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%conv);
    }
    %conv.aimName = %aimName;
    %conv.status = "offline";
    %i = 0;
    while (%i < aimBuddyCount())
    {
        if (aimGetBuddyName(%i) $= %conv.aimName)
        {
            %state = aimGetBuddyState(%i);
            if (%state == -1)
            {
                %state = 0;
            }
            %conv.status = getWord(%this.stateMapping, %state);
            break;
        }
        %i = %i + 1;
    }
    %conv.newMessage = 0;
    %textInput = new GuiTextEditCtrl()
    {
        profile = "AIMTextEditProfile";
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
    %scroll = new GuiScrollCtrl()
    {
        profile = "ETSAimMessageScrollProfile";
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
    %mlText = new GuiMLTextCtrl()
    {
        profile = "ETSAIMMessageProfile";
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
    %contents = new GuiControl()
    {
        profile = "ETSAIMTabProfile";
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
    %contents.add(%textInput);
    %contents.add(%scroll);
    %dummy = new GuiTextCtrl()
    {
        profile = "ETSAIMDeselectedProfile";
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
    %recipient = new GuiBitmapButtonCtrl()
    {
        profile = "ETSAIMDeselectedProfile";
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
    %status = new GuiBitmapCtrl()
    {
        profile = "GuiDefaultProfile";
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
    %statusButton = new GuiBitmapButtonCtrl()
    {
        profile = "ETSAIMDeselectedProfile";
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
    %close = new GuiBitmapButtonCtrl()
    {
        profile = "GuiButtonProfile";
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
    %titlebar = new GuiControl()
    {
        profile = "GuiDefaultProfile";
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
    %this.convs[%this.numConvs] = %conv;
    AimConvContainer.add(%contents);
    AimConvContainer.add(%titlebar);
    %this.numConvs = %this.numConvs + 1;
    if (isObject(%theFirstResponder))
    {
        %theFirstResponder.makeFirstResponder(1);
    }
    if (%this.numConvs == 1)
    {
        %this.selectConvAtIndex(0);
    }
    else
    {
        %this.update();
    }
    ConvBub.updateAutoMargins();
    return %conv;
}
function AIMConvManager::convClicked(%this, %conv)
{
    %curConv = %this.getCurrentConv();
    if (isObject(%curConv) && (%curConv.getId() == %conv.getId()))
    {
        %this.selectConvAtIndex(-1);
    }
    else
    {
        %this.selectConv(%conv);
        %conv.contents.textInput.makeFirstResponder(1);
    }
    return ;
}
function AIMConvManager::getConvWithName(%this, %aimName)
{
    %idx = 0;
    while (%idx < %this.numConvs)
    {
        if (%this.convs[%idx].aimName $= %aimName)
        {
            return %this.convs[%idx];
        }
        %idx = %idx + 1;
    }
    return 0;
}
function AIMConvManager::getCurrentText(%this)
{
    if (%this.numConvs > 0)
    {
        return %this.convs[%this.currentConvIndex].contents.textInput.getValue();
    }
    else
    {
        return "";
    }
    return ;
}
function AIMConvManager::clearCurrentText(%this)
{
    if (%this.numConvs > 0)
    {
        %this.convs[%this.currentConvIndex].contents.textInput.setValue("");
    }
    return ;
}
function AIMConvManager::getCurrentConv(%this)
{
    if (%this.numConvs > 0)
    {
        return %this.convs[%this.currentConvIndex];
    }
    else
    {
        return 0;
    }
    return ;
}
function AIMConvManager::wakeUp(%this)
{
    %this.Initialize();
    if (%this.numConvs > 0)
    {
        %this.selectCurrentConv();
    }
    return ;
}
function AIMConvManager::updateContainer(%this)
{
    %ypadding = 8;
    if (%this.numConvs <= 0)
    {
        AimConvContainer.setVisible(0);
        WindowManager.update();
        return ;
    }
    else
    {
        %titleHeight = getWord(%this.convs[0].titlebar.getExtent(), 1);
        %height = (((%this.numConvs - 1) * %titleHeight) + getWord(%this.convs[0].titlebar.getPosition(), 1)) + %ypadding;
        if (%this.numConvs == 1)
        {
            %height = %titleHeight + %ypadding;
        }
    }
    %curConv = %this.getCurrentConv();
    if (isObject(%curConv))
    {
        %contentsExtent = %curConv.contents.getExtent();
        %height = %height + (getWord(%contentsExtent, 1) + %titleHeight);
    }
    else
    {
        %height = %height + %titleHeight;
    }
    AimConvContainer.resize(getWord(AimConvContainer.getExtent(), 0), %height);
    AimConvContainer.setVisible(1);
    WindowManager.update();
    return ;
}
function AIMConvManager::buildSpamString(%this, %prepend, %aimName, %link, %message)
{
    if (!(%message $= ""))
    {
        %text = %prepend SPC "( " @ %link @ " ):" @ %message;
    }
    else
    {
        %text = %prepend @ %link;
    }
    return %text;
}
function AIMConvManager::filterMessage(%this, %conv, %message)
{
    %filteredMessage = %message;
    return %filteredMessage;
}
function AIMConvManager::sendInvites(%this, %recipients, %links, %userMsg)
{
    %count = getFieldCount(%recipients);
    %n = 0;
    while (%n < %count)
    {
        %aBuddy = getField(%recipients, %n);
        %link = getField(%links, %n);
        %inviteText = %this.buildSpamString($Net::AIMInvite, %aBuddy, %link, %userMsg);
        aimSend(%aBuddy, %inviteText);
        %n = %n + 1;
    }
    MessageBoxOK("Invites Sent", %count SPC "buddies have been invited to join" SPC $ETS::AppName, "");
    return ;
}
function AIMConvManager::inviteAll(%this, %userMsg)
{
    %buddies = BuddyHudWin.onlineBuddiesToString();
    %this.prepareToSendInvites(%buddies, %userMsg);
    return ;
}
function AIMConvManager::prepareToSendInvites(%this, %recipients, %userMsg)
{
    %request = new ManagerRequest()
    {
        className = "GetAIMInviteURLsRequest";
        userMsg = %userMsg;
    };
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %url = $Net::ClientServiceURL @ "/GetAIMInviteURLs" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token);
    %count = getFieldCount(%recipients);
    %url = %url @ "&urlCount=" @ %count;
    log("network", "info", getScopeName() @ ":" @ %url);
    %request.setURL(%url);
    %request.recipients = %recipients;
    %request.start();
    return ;
}
function GetAIMInviteURLsRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() @ ":" @ %status);
    if (%status $= "fail")
    {
        warn("network", getScopeName() @ ": request failed: " @ %this.getValue("statusMessage"));
        MessageBoxOK("Invite failed", "Couldn\'t get unique invite URLs. Please try again later.", "");
    }
    else
    {
        %count = %this.getValue("urlCount");
        %links = "";
        %i = 0;
        while (%i < %count)
        {
            %key = "url" @ %i;
            %val = %this.getValue(%key);
            %links = %links TAB %val;
            %i = %i + 1;
        }
        AIMConvManager.sendInvites(%this.recipients, %links, %this.userMsg);
    }
    %this.schedule(0, "delete");
    return ;
}
function GetAIMInviteURLsRequest::onError(%this, %unused, %errMsg)
{
    error("network", getScopeName() @ ":" @ %errMsg);
    %this.schedule(0, "delete");
    return ;
}
function AIMConvManager::sendMessage(%this)
{
    %conv = %this.getCurrentConv();
    %message = %this.getCurrentText();
    if (!(%message $= ""))
    {
        %sendMessage = %this.filterMessage(%conv, %message);
        %this.spamDict.put(%conv.aimName, 1);
        aimSend(%conv.aimName, %sendMessage);
        %prefix = %conv.contents.mlText.getText() $= "" ? "" : "\n";
        %toBottom = %conv.contents.scroll.isAtBottom();
        %conv.contents.mlText.addText(%prefix @ "<spush><color:bebeee>" @ %message @ "<spop>", 1, %toBottom);
        %this.totalMessagesSent = %this.totalMessagesSent + 1;
        %conv.newMessage = 0;
        %this.update();
    }
    %this.clearCurrentText();
    return ;
}
function AIMConvManager::talkTo(%this, %aimName)
{
    %conv = %this.newConv(%aimName);
    if (%conv)
    {
        %this.selectConv(%conv);
        %conv.contents.textInput.makeFirstResponder(1);
    }
    else
    {
        echo("Failed to talk to " @ %aimName @ ": too many conversations open");
    }
    return ;
}
function AIMConvManager::receivedMessage(%this, %aimName, %message)
{
    %conv = %this.newConv(%aimName);
    if (%conv)
    {
        %prefix = %conv.contents.mlText.getText() $= "" ? "" : "\n";
        %toBottom = %conv.contents.scroll.isAtBottom();
        %conv.contents.mlText.addText(%prefix @ "<spush><color:ee8fee>" @ %message @ "<spop>", 1, %toBottom);
        %conv.newMessage = 1;
        %this.update();
    }
    else
    {
        echo("Received message from " @ %aimName @ ": " @ %message);
    }
    if ((!isForegroundWindow() || isIdle()) || !PlayGui.canPlayerSeeWorld())
    {
        if ($UserPref::Audio::NotifyChat)
        {
            alxPlay(AudioIm_MessageIn);
        }
    }
    return ;
}
function AIMConvManager::closeAllConvs(%this)
{
    %n = %this.numConvs - 1;
    while (%n >= 0)
    {
        %this.removeConvAtIndex(%n);
        %n = %n - 1;
    }
}

AIMConvManager.stateMapping = "offline avail away away";
function AIMConvManager::buddyStateChanged(%this, %name, %state)
{
    %conv = %this.getConvWithName(%name);
    if (%conv)
    {
        if (%state == -1)
        {
            %state = 0;
        }
        %conv.status = getWord(%this.stateMapping, %state);
        %this.update();
    }
    return ;
}
