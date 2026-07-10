function rentabotClient_customizeBot(%obj)
{
    if ((CustomSpaceClient::GetSpaceImIn() $= "") || !$StandAlone && !CustomSpaceClient::isOwner())
    {
        return;
    }
    if (!isObject(%obj))
    {
        error(getScopeName() @ "- passed null object" @ " " @ getTrace());
        return;
    }
    if (!%obj.isClassAIPlayer())
    {
        error(getScopeName() @ "- passed a real player!" @ " " @ getTrace());
        return;
    }
    %name = %obj.getDisplayName();
    if (!rentabot_isRentabotName(%name))
    {
        return;
    }
    %name = rentabot_getCoreName(%name);
    %msgBlab = %obj.getMsgBlab();
    %msgWhisper = %obj.getMsgWhisper();
    %okayCmd = "CustomizeBotDialog_onOkay();";
    %dlg = MessageBoxOkCancel($MsgCat::furniture["BOTCUST-TITLE"], $MsgCat::furniture["BOTCUST-BODY"], %okayCmd, "");
    $gCustomizeBotDialog = %dlg;
    %window = %dlg.window;
    %window.rentabot = %obj;
    %winWidth = 400;
    %winHeight = 240;
    %window.resize(%winWidth, %winHeight);
    %colSpacing = 10;
    %col1 = %colSpacing;
    %col1Size = 45;
    %col2 = (%col1 + %col1Size) + %colSpacing;
    %col2Size = (%winWidth - %colSpacing) - %col2;
    %rowSpacing = 4;
    %row = 46 + %rowSpacing;
    %rowSize = 18;
    %tipStyle = "<color:ffffff88>";
    %validCharsName = "abcdefghijklmnopqrstuvwxyz" @ "ABCDEFGHIJKLMNOPQRSTUVWXYZ" @ 0123456789 @ "_[]" @ "";
    %validCharsMsgs = "abcdefghijklmnopqrstuvwxyz" @ "ABCDEFGHIJKLMNOPQRSTUVWXYZ" @ 0123456789 @ "_[]" @ " " @ ",./?:\"'+=-(){}|*&!@#$%" @ "";
    %ctrl = new GuiMLTextCtrl("") {
        profile = "GuiMessageTextProfile";
        position = %col1 @ " " @ %row;
        extent = %col1Size @ " " @ %rowSize;
        text = "<just:right>Name:";
    };
    %window.add(%ctrl);
    %ctrl = new GuiTextEditCtrl("") {
        profile = "ETSDarkTextEditProfile";
        position = %col2 @ " " @ %row;
        extent = %col2Size @ " " @ %rowSize;
        text = %name;
        validInputChars = %validCharsName;
        maxLength = 20;
    };
    %window.add(%ctrl);
    %window.ctrlName = %ctrl;
    %row = %row + %rowSize;
    %ctrl = new GuiMLTextCtrl("") {
        profile = "GuiMessageTextProfile";
        position = %col2 @ " " @ %row;
        extent = %col2Size @ " " @ %rowSize;
        text = %tipStyle @ %tipStyle[$MsgCat::furniture @ "BOTCUST-TIP-NAME"];
    };
    %window.add(%ctrl);
    %row = %row + (%rowSize + %rowSpacing);
    if (%obj.getCanSpew())
    {
        %ctrl = new GuiMLTextCtrl("") {
            profile = "GuiMessageTextProfile";
            position = %col1 @ " " @ %row;
            extent = %col1Size @ " " @ %rowSize;
            text = "<just:right>Blab:";
        };
        %window.add(%ctrl);
        %ctrl = new GuiTextEditCtrl("") {
            profile = "ETSDarkTextEditProfile";
            position = %col2 @ " " @ %row;
            extent = %col2Size @ " " @ %rowSize;
            text = %msgBlab;
            validInputChars = %validCharsMsgs;
            maxLength = 100;
        };
        %window.add(%ctrl);
        %window.ctrlBlab = %ctrl;
        %row = %row + %rowSize;
        %ctrl = new GuiMLTextCtrl("") {
            profile = "GuiMessageTextProfile";
            position = %col2 @ " " @ %row;
            extent = %col2Size @ " " @ %rowSize;
            text = %tipStyle @ %tipStyle[$MsgCat::furniture @ "BOTCUST-TIP-BLAB"];
        };
        %window.add(%ctrl);
        %row = %row + (%rowSize + %rowSpacing);
        %ctrl = new GuiMLTextCtrl("") {
            profile = "GuiMessageTextProfile";
            position = %col1 @ " " @ %row;
            extent = %col1Size @ " " @ %rowSize;
            text = "<just:right>Whisper:";
        };
        %window.add(%ctrl);
        %ctrl = new GuiTextEditCtrl("") {
            profile = "ETSDarkTextEditProfile";
            position = %col2 @ " " @ %row;
            extent = %col2Size @ " " @ %rowSize;
            text = %msgWhisper;
            validInputChars = %validCharsMsgs;
            maxLength = 100;
        };
        %window.add(%ctrl);
        %window.ctrlWhisper = %ctrl;
        %row = %row + %rowSize;
        %ctrl = new GuiMLTextCtrl("") {
            profile = "GuiMessageTextProfile";
            position = %col2 @ " " @ %row;
            extent = %col2Size @ " " @ %rowSize;
            text = %tipStyle @ %tipStyle[$MsgCat::furniture @ "BOTCUST-TIP-WHISPER"];
        };
        %window.add(%ctrl);
        %row = %row + (%rowSize + %rowSpacing);
    }
    if (%obj.getDressUpWrite() || (%obj.getGender() $= $player.getGender()) && %obj.getDressUpRead())
    {
        %ctrl = new GuiMLTextCtrl("") {
            profile = "GuiMessageTextProfile";
            position = %col1 @ " " @ %row;
            extent = %col1Size @ " " @ %rowSize;
            text = "<just:right>Dress:";
        };
        %window.add(%ctrl);
        if (%obj.getDressUpRead())
        {
            %ctrl = new GuiVariableWidthButtonCtrl("") {
                profile = "GuiFocusableVWButtonProfile";
                position = %col2 @ " " @ %row;
                extent = ((%col2Size - (%colSpacing * 2)) / 3) @ " " @ %rowSize;
                text = "Dress me like it!";
                command = "rentabotClient_DressUpRead(" @ %obj @ ");";
            };
            %window.add(%ctrl);
        }
        if (%obj.getDressUpWrite())
        {
            %ctrl = new GuiVariableWidthButtonCtrl("") {
                profile = "GuiFocusableVWButtonProfile";
                position = (%col2 + mFloor(((%col2Size + %colSpacing) / 3))) @ " " @ %row;
                extent = ((%col2Size - (%colSpacing * 2)) / 3) @ " " @ %rowSize;
                text = "Dress it like me!";
                command = "rentabotClient_DressUpWrite(" @ %obj @ ");";
            };
            %window.add(%ctrl);
            %ctrl = new GuiVariableWidthButtonCtrl("") {
                profile = "GuiFocusableVWButtonProfile";
                position = (%col2 + (mFloor(((%col2Size + %colSpacing) / 3)) * 2)) @ " " @ %row;
                extent = ((%col2Size - (%colSpacing * 2)) / 3) @ " " @ %rowSize;
                text = "Reset";
                command = "rentabotClient_DressUpReset(" @ %obj @ ");";
            };
            %window.add(%ctrl);
        }
        %row = %row + (%rowSize + %rowSpacing);
    }
    %window.ctrlName.makeFirstResponder(1);
    %window.ctrlName.setSelection(0, 1000);
    if (isObject(%window.ctrlBlab))
    {
        %window.ctrlName.altCommand = %window.ctrlBlab @ ".makeFirstResponder(true);";
    }
    else
    {
        %window.ctrlName.altCommand = %okayCmd @ " " @ %dlg @ ".close();";
    }
    %window.ctrlBlab.altCommand = %window.ctrlWhisper @ ".makeFirstResponder(true);";
    %window.ctrlWhisper.altCommand = %okayCmd @ " " @ %dlg @ ".close();";
}
function CustomizeBotDialog_onOkay()
{
    %window = $gCustomizeBotDialog.window;
    %obj = %window.rentabot;
    %name = %window.ctrlName.getValue();
    %name = rentabot_getCoreName(%name);
    if (%obj.getCanSpew())
    {
    }
    else
    {
    }
    %msgBlab = "";
    %window.ctrlBlab.getValue();
    if (%obj.getCanSpew())
    {
    }
    else
    {
    }
    %msgWhisper = "";
    %window.ctrlWhisper.getValue();
    commandToServer('Rentabot_Customize', CustomSpaceClient::GetSpaceImIn(), %obj.getGhostID(), %name, %msgBlab, %msgWhisper);
}
function rentabotClient_DressUpRead(%obj)
{
    if (!isObject(%obj))
    {
        error(getScopeName() @ " " @ "- something went wrong" @ " " @ getTrace());
        return;
    }
    %otherSkus = %obj.getActiveSKUs();
    %otherSkusOutfit = SkuManager.filterSkusForClothing(%otherSkus);
    %otherSkusGender = SkuManager.filterSkusGender(%otherSkusOutfit, $player.getGender());
    %otherSkusNotOwned = wordsNotInWords($Player::inventory, %otherSkusGender);
    %otherSkusAllGood = wordsNotInWords(%otherSkusNotOwned, %otherSkusGender);
    %numLostGender = getWordCount(%otherSkusOutfit) - getWordCount(%otherSkusGender);
    %numLostOwnership = getWordCount(%otherSkusGender) - getWordCount(%otherSkusAllGood);
    %msg = "";
    if (%numLostGender > 0)
    {
        %otherGender = (%obj.getGender() $= "f") ? "female" : "male";
        %msg = %msg @ "Some of those items are for" @ " " @ %otherGender @ " " @ "players";
    }
    if (%numLostOwnership > 0)
    {
        if (%msg $= "")
        {
            %msg = %msg @ "You don't own some of those items";
        }
        %msg = %msg @ ", and you don't own some of those items";
    }
    if (!(%msg $= ""))
    {
        %msg = %msg @ "!";
        %msg = $MsgCat::furniture["DRESSUP-READ-CONF-NOTALL-BODY"] @ %msg;
        %tit = $MsgCat::furniture["DRESSUP-READ-CONF-NOTALL-TITLE"];
    }
    else
    {
        %msg = $MsgCat::furniture["DRESSUP-READ-CONF-BODY"] @ %msg;
        %tit = $MsgCat::furniture["DRESSUP-READ-CONF-TITLE"];
    }
    MessageBoxYesNo(%tit, %msg, "rentabotClient_DressUpReadConfirmed(\"" @ %otherSkusAllGood @ "\");", "");
}
function rentabotClient_DressUpReadConfirmed(%skus)
{
    %underSkus = $player.getGender()["A"];
    $gNewStockOutfits;
    %underSkus = %underSkus @ " " @ SkuManager.filterSkusForBody($player.getActiveSKUs());
    %allSkus = SkuManager.overlaySkus(%underSkus, %skus);
    SaveOutfitAndBodySkusAsCurrent(%allSkus);
}
function rentabotClient_DressUpWrite(%obj)
{
    %skus = $player.getActiveSKUs();
    MessageBoxYesNo($MsgCat::furniture["DRESSUP-WRITE-CONF-TITLE"], $MsgCat::furniture["DRESSUP-WRITE-CONF-BODY"], "rentabotClient_DressUpWriteConfirmed(" @ %obj @ ", \"" @ %skus @ "\");", "");
}
function rentabotClient_DressUpWriteConfirmed(%obj, %skus)
{
    commandToServer('Rentabot_DressUpWrite', CustomSpaceClient::GetSpaceImIn(), %obj.getGhostID(), %skus);
}
function rentabotClient_DressUpReset(%obj)
{
    MessageBoxYesNo($MsgCat::furniture["DRESSUP-RESET-CONF-TITLE"], $MsgCat::furniture["DRESSUP-RESET-CONF-BODY"], "rentabotClient_DressUpResetConfirmed(" @ %obj @ ");", "");
}
function rentabotClient_DressUpResetConfirmed(%obj)
{
    commandToServer('Rentabot_DressUpReset', CustomSpaceClient::GetSpaceImIn(), %obj.getGhostID());
}
function rentabotClient_reignore()
{
    %n = getWordCount($gRentabotIgnores) - 1;
    while (%n >= 0)
    {
        %bot = getWord($gRentabotIgnores, %n);
        if (isObject(%bot))
        {
            %bot.setIgnore(1);
            %record = new ScriptObject("");
            %record.name = $ServerName;
            %record.serverName = $Pref::Server::Name;
            %roled = 0;
            %record.loggedIn = 0;
            %record.isIdle = 0;
            %record.isNPC = 1;
            %record.loggedIn = 0;
            %record.csn = BuddyHudTabs.getCityNameForServerName(%record.serverName);
            %record.activities = "";
            UserListIgnores.put(%bot.getShapeName(), %record);
        }
        else
        {
            $gRentabotIgnores = findAndRemoveAllOccurrencesOfWord($gRentabotIgnores, %bot);
        }
        %n = %n - 1;
    }
}
