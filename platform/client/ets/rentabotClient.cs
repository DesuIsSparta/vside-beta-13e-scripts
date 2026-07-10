function rentabotClient_customizeBot(%obj) {
    if (!($StandAlone) && (CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if (!(CustomSpaceClient::isOwner())) {
        return;
    }
    if (!(isObject(%obj))) {
        error(getScopeName() @ "- passed null object" @ " " @ getTrace());
        return;
    }
    if (!(%obj.isClassAIPlayer())) {
        error(getScopeName() @ "- passed a real player!" @ " " @ getTrace());
        return;
    }
    %name = %obj.getDisplayName();
    if (!(rentabot_isRentabotName(%name))) {
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
    %winHeight.resize(%window, %winWidth);
    %colSpacing = 10;
    %col1 = %colSpacing;
    %col1Size = 45;
    %col2 = ((%col1 + %col1Size) + %colSpacing);
    %col2Size = ((%winWidth - %colSpacing) - %col2);
    %rowSpacing = 4;
    %row = (46.0 + %rowSpacing);
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
    %ctrl.add(%window);
    %ctrl = new GuiTextEditCtrl("") {
        profile = "ETSDarkTextEditProfile";
        position = %col2 @ " " @ %row;
        extent = %col2Size @ " " @ %rowSize;
        text = %name;
        validInputChars = %validCharsName;
        maxLength = 20;
    };
    %ctrl.add(%window);
    %window.ctrlName = %ctrl;
    %row = (%row + %rowSize);
    %ctrl = new GuiMLTextCtrl("") {
        profile = "GuiMessageTextProfile";
        position = %col2 @ " " @ %row;
        extent = %col2Size @ " " @ %rowSize;
        text = %tipStyle @ %tipStyle[$MsgCat::furniture @ "BOTCUST-TIP-NAME"];
    };
    %ctrl.add(%window);
    %row = (%row + (%rowSize + %rowSpacing));
    if (%obj.getCanSpew()) {
        %ctrl = new GuiMLTextCtrl("") {
            profile = "GuiMessageTextProfile";
            position = %col1 @ " " @ %row;
            extent = %col1Size @ " " @ %rowSize;
            text = "<just:right>Blab:";
        };
        %ctrl.add(%window);
        %ctrl = new GuiTextEditCtrl("") {
            profile = "ETSDarkTextEditProfile";
            position = %col2 @ " " @ %row;
            extent = %col2Size @ " " @ %rowSize;
            text = %msgBlab;
            validInputChars = %validCharsMsgs;
            maxLength = 100;
        };
        %ctrl.add(%window);
        %window.ctrlBlab = %ctrl;
        %row = (%row + %rowSize);
        %ctrl = new GuiMLTextCtrl("") {
            profile = "GuiMessageTextProfile";
            position = %col2 @ " " @ %row;
            extent = %col2Size @ " " @ %rowSize;
            text = %tipStyle @ %tipStyle[$MsgCat::furniture @ "BOTCUST-TIP-BLAB"];
        };
        %ctrl.add(%window);
        %row = (%row + (%rowSize + %rowSpacing));
        %ctrl = new GuiMLTextCtrl("") {
            profile = "GuiMessageTextProfile";
            position = %col1 @ " " @ %row;
            extent = %col1Size @ " " @ %rowSize;
            text = "<just:right>Whisper:";
        };
        %ctrl.add(%window);
        %ctrl = new GuiTextEditCtrl("") {
            profile = "ETSDarkTextEditProfile";
            position = %col2 @ " " @ %row;
            extent = %col2Size @ " " @ %rowSize;
            text = %msgWhisper;
            validInputChars = %validCharsMsgs;
            maxLength = 100;
        };
        %ctrl.add(%window);
        %window.ctrlWhisper = %ctrl;
        %row = (%row + %rowSize);
        %ctrl = new GuiMLTextCtrl("") {
            profile = "GuiMessageTextProfile";
            position = %col2 @ " " @ %row;
            extent = %col2Size @ " " @ %rowSize;
            text = %tipStyle @ %tipStyle[$MsgCat::furniture @ "BOTCUST-TIP-WHISPER"];
        };
        %ctrl.add(%window);
        %row = (%row + (%rowSize + %rowSpacing));
    }
    if ((%obj.getGender() $= $player.getGender()) && %obj.getDressUpWrite()) {
    }
    if (%obj.getDressUpRead()) {
        %ctrl = new GuiMLTextCtrl("") {
            profile = "GuiMessageTextProfile";
            position = %col1 @ " " @ %row;
            extent = %col1Size @ " " @ %rowSize;
            text = "<just:right>Dress:";
        };
        %ctrl.add(%window);
        if (%obj.getDressUpRead()) {
            %ctrl = new GuiVariableWidthButtonCtrl("") {
                profile = "GuiFocusableVWButtonProfile";
                position = %col2 @ " " @ %row;
                extent = ((%col2Size - (%colSpacing * 2.0)) / 3.0) @ " " @ %rowSize;
                text = "Dress me like it!";
                command = "rentabotClient_DressUpRead(" @ %obj @ ");";
            };
            %ctrl.add(%window);
        }
        if (%obj.getDressUpWrite()) {
            %ctrl = new GuiVariableWidthButtonCtrl("") {
                profile = "GuiFocusableVWButtonProfile";
                position = (%col2 + mFloor(((%col2Size + %colSpacing) / 3.0))) @ " " @ %row;
                extent = ((%col2Size - (%colSpacing * 2.0)) / 3.0) @ " " @ %rowSize;
                text = "Dress it like me!";
                command = "rentabotClient_DressUpWrite(" @ %obj @ ");";
            };
            %ctrl.add(%window);
            %ctrl = new GuiVariableWidthButtonCtrl("") {
                profile = "GuiFocusableVWButtonProfile";
                position = (%col2 + (mFloor(((%col2Size + %colSpacing) / 3.0)) * 2.0)) @ " " @ %row;
                extent = ((%col2Size - (%colSpacing * 2.0)) / 3.0) @ " " @ %rowSize;
                text = "Reset";
                command = "rentabotClient_DressUpReset(" @ %obj @ ");";
            };
            %ctrl.add(%window);
        }
        %row = (%row + (%rowSize + %rowSpacing));
    }
    1.makeFirstResponder(%window.ctrlName);
    1000.setSelection(%window.ctrlName, 0);
    if (isObject(%window.ctrlBlab)) {
        %window.ctrlName.altCommand = %window.ctrlBlab @ ".makeFirstResponder(true);";
    }
    %window.ctrlName.altCommand = %okayCmd @ " " @ %dlg @ ".close();";
    %window.ctrlBlab.altCommand = %window.ctrlWhisper @ ".makeFirstResponder(true);";
    %window.ctrlWhisper.altCommand = %okayCmd @ " " @ %dlg @ ".close();";
};
function CustomizeBotDialog_onOkay() {
    %window = $gCustomizeBotDialog.window;
    %obj = %window.rentabot;
    %name = %window.ctrlName.getValue();
    %name = rentabot_getCoreName(%name);
    if (%obj.getCanSpew()) {
    }
    %msgBlab = "";
    %window.ctrlBlab.getValue();
    if (%obj.getCanSpew()) {
    }
    %msgWhisper = "";
    %window.ctrlWhisper.getValue();
    commandToServer('Rentabot_Customize', CustomSpaceClient::GetSpaceImIn(), %obj.getGhostID(), %name, %msgBlab, %msgWhisper);
};
function rentabotClient_DressUpRead(%obj) {
    if (!(isObject(%obj))) {
        error(getScopeName() @ " " @ "- something went wrong" @ " " @ getTrace());
        return;
    }
    %otherSkus = %obj.getActiveSKUs();
    %otherSkusOutfit = %otherSkus.filterSkusForClothing(SkuManager);
    %otherSkusGender = $player.getGender().filterSkusGender(SkuManager, %otherSkusOutfit);
    %otherSkusNotOwned = wordsNotInWords($Player::inventory, %otherSkusGender);
    %otherSkusAllGood = wordsNotInWords(%otherSkusNotOwned, %otherSkusGender);
    %numLostGender = (getWordCount(%otherSkusOutfit) - getWordCount(%otherSkusGender));
    %numLostOwnership = (getWordCount(%otherSkusGender) - getWordCount(%otherSkusAllGood));
    %msg = "";
    if ((%numLostGender > 0.0)) {
        %otherGender = (%obj.getGender() $= "f") ? "female" : "male";
        %msg = %msg @ "Some of those items are for" @ " " @ %otherGender @ " " @ "players";
    }
    if ((%numLostOwnership > 0.0)) {
        if ((%msg $= "")) {
            %msg = %msg @ "You don't own some of those items";
        }
        %msg = %msg @ ", and you don't own some of those items";
    }
    if (!(%msg $= "")) {
        %msg = %msg @ "!";
        %msg = $MsgCat::furniture["DRESSUP-READ-CONF-NOTALL-BODY"] @ %msg;
        %tit = $MsgCat::furniture["DRESSUP-READ-CONF-NOTALL-TITLE"];
    }
    %msg = $MsgCat::furniture["DRESSUP-READ-CONF-BODY"] @ %msg;
    %tit = $MsgCat::furniture["DRESSUP-READ-CONF-TITLE"];
    MessageBoxYesNo(%tit, %msg, "rentabotClient_DressUpReadConfirmed(\"" @ %otherSkusAllGood @ "\");", "");
};
function rentabotClient_DressUpReadConfirmed(%skus) {
    %underSkus = $player.getGender()["A"];
    $gNewStockOutfits;
    %underSkus = %underSkus @ " " @ $player.getActiveSKUs().filterSkusForBody(SkuManager);
    %allSkus = %skus.overlaySkus(SkuManager, %underSkus);
    SaveOutfitAndBodySkusAsCurrent(%allSkus);
};
function rentabotClient_DressUpWrite(%obj) {
    %skus = $player.getActiveSKUs();
    MessageBoxYesNo($MsgCat::furniture["DRESSUP-WRITE-CONF-TITLE"], $MsgCat::furniture["DRESSUP-WRITE-CONF-BODY"], "rentabotClient_DressUpWriteConfirmed(" @ %obj @ ", \"" @ %skus @ "\");", "");
};
function rentabotClient_DressUpWriteConfirmed(%obj, %skus) {
    commandToServer('Rentabot_DressUpWrite', CustomSpaceClient::GetSpaceImIn(), %obj.getGhostID(), %skus);
};
function rentabotClient_DressUpReset(%obj) {
    MessageBoxYesNo($MsgCat::furniture["DRESSUP-RESET-CONF-TITLE"], $MsgCat::furniture["DRESSUP-RESET-CONF-BODY"], "rentabotClient_DressUpResetConfirmed(" @ %obj @ ");", "");
};
function rentabotClient_DressUpResetConfirmed(%obj) {
    commandToServer('Rentabot_DressUpReset', CustomSpaceClient::GetSpaceImIn(), %obj.getGhostID());
};
function rentabotClient_reignore() {
    %n = (getWordCount($gRentabotIgnores) - 1.0);
    while ((%n >= 0.0)) {
        %bot = getWord($gRentabotIgnores, %n);
        if (isObject(%bot)) {
            1.setIgnore(%bot);
            %record = new ScriptObject("");
            %record.name = $ServerName;
            %record.serverName = $Pref::Server::Name;
            %roled = 0;
            %record.loggedIn = 0;
            %record.isIdle = 0;
            %record.isNPC = 1;
            %record.loggedIn = 0;
            %record.csn = %record.serverName.getCityNameForServerName(BuddyHudTabs);
            %record.activities = "";
            %record.put(UserListIgnores, %bot.getShapeName());
        }
        $gRentabotIgnores = findAndRemoveAllOccurrencesOfWord($gRentabotIgnores, %bot);
        %n = (%n - 1.0);
    }
};
