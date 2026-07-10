function rentabotClient_customizeBot(%obj) {
    if (!($StandAlone)) {
        if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
        }
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
    %dlg = MessageBoxOkCancel(%okayCmd[$MsgCat::furniture @ "BOTCUST-TITLE"], , %okayCmd, "");
    $gCustomizeBotDialog = %dlg;
    %window = window;
    %dlg;
    rentabot = %obj @ %window;
    %winWidth = 400;
    %winHeight = 240;
    %window.resize(%winWidth, %winHeight);
    %colSpacing = 10;
    %col1 = %colSpacing;
    %col1Size = 45;
    %col2 = (%colSpacing + (%col1Size + %col1));
    %col2Size = (%col2 - (%colSpacing - %winWidth));
    %rowSpacing = 4;
    %row = (%rowSpacing + 46.0);
    %rowSize = 18;
    %tipStyle = "<color:ffffff88>";
    %validCharsName = "abcdefghijklmnopqrstuvwxyz" @ "ABCDEFGHIJKLMNOPQRSTUVWXYZ" @ 0123456789 @ "_[]" @ "";
    %validCharsMsgs = "abcdefghijklmnopqrstuvwxyz" @ "ABCDEFGHIJKLMNOPQRSTUVWXYZ" @ 0123456789 @ "_[]" @ " " @ ",./?:\"'+=-(){}|*&!@#$%" @ "";
    profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
    0;
    position = %col1 @ " " @ %row;
    extent = %col1Size @ " " @ %rowSize;
    text = "<just:right>Name:";
    %ctrl = ;
    %window.add(%ctrl);
    profile = GuiTextEditCtrl @ new ""() @ "ETSDarkTextEditProfile";
    0;
    position = %col2 @ " " @ %row;
    extent = %col2Size @ " " @ %rowSize;
    text = %name;
    validInputChars = %validCharsName;
    maxLength = 20;
    %ctrl = ;
    %window.add(%ctrl);
    ctrlName = %ctrl @ %window;
    %row = (%rowSize + %row);
    profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
    0;
    position = %col2 @ " " @ %row;
    extent = %col2Size @ " " @ %rowSize;
    text = %tipStyle @ %tipStyle[$MsgCat::furniture @ "BOTCUST-TIP-NAME"];
    %ctrl = ;
    %window.add(%ctrl);
    %row = ((%rowSpacing + %rowSize) + %row);
    if (%obj.getCanSpew()) {
        profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
        0;
        position = %col1 @ " " @ %row;
        extent = %col1Size @ " " @ %rowSize;
        text = "<just:right>Blab:";
        %ctrl = ;
        %window.add(%ctrl);
        profile = GuiTextEditCtrl @ new ""() @ "ETSDarkTextEditProfile";
        0;
        position = %col2 @ " " @ %row;
        extent = %col2Size @ " " @ %rowSize;
        text = %msgBlab;
        validInputChars = %validCharsMsgs;
        maxLength = 100;
        %ctrl = ;
        %window.add(%ctrl);
        ctrlBlab = %ctrl @ %window;
        %row = (%rowSize + %row);
        profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
        0;
        position = %col2 @ " " @ %row;
        extent = %col2Size @ " " @ %rowSize;
        text = %tipStyle @ %tipStyle[$MsgCat::furniture @ "BOTCUST-TIP-BLAB"];
        %ctrl = ;
        %window.add(%ctrl);
        %row = ((%rowSpacing + %rowSize) + %row);
        profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
        0;
        position = %col1 @ " " @ %row;
        extent = %col1Size @ " " @ %rowSize;
        text = "<just:right>Whisper:";
        %ctrl = ;
        %window.add(%ctrl);
        profile = GuiTextEditCtrl @ new ""() @ "ETSDarkTextEditProfile";
        0;
        position = %col2 @ " " @ %row;
        extent = %col2Size @ " " @ %rowSize;
        text = %msgWhisper;
        validInputChars = %validCharsMsgs;
        maxLength = 100;
        %ctrl = ;
        %window.add(%ctrl);
        ctrlWhisper = %ctrl @ %window;
        %row = (%rowSize + %row);
        profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
        0;
        position = %col2 @ " " @ %row;
        extent = %col2Size @ " " @ %rowSize;
        text = %tipStyle @ %tipStyle[$MsgCat::furniture @ "BOTCUST-TIP-WHISPER"];
        %ctrl = ;
        %window.add(%ctrl);
        %row = ((%rowSpacing + %rowSize) + %row);
    }
    if ((%obj.getGender() $= $player.getGender())) {
        if (%obj.getDressUpWrite()) {
        }
    }
    if (%obj.getDressUpRead()) {
        profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
        0;
        position = %col1 @ " " @ %row;
        extent = %col1Size @ " " @ %rowSize;
        text = "<just:right>Dress:";
        %ctrl = ;
        %window.add(%ctrl);
        if (%obj.getDressUpRead()) {
            profile = GuiVariableWidthButtonCtrl @ new ""() @ "GuiFocusableVWButtonProfile";
            0;
            position = %col2 @ " " @ %row;
            extent = (3.0 / ((2.0 * %colSpacing) - %col2Size)) @ " " @ %rowSize;
            text = "Dress me like it!";
            command = "rentabotClient_DressUpRead(" @ %obj @ ");";
            %ctrl = ;
            %window.add(%ctrl);
        }
        if (%obj.getDressUpWrite()) {
            profile = GuiVariableWidthButtonCtrl @ new ""() @ "GuiFocusableVWButtonProfile";
            0;
            position = (mFloor((3.0 / (%colSpacing + %col2Size))) + %col2) @ " " @ %row;
            extent = (3.0 / ((2.0 * %colSpacing) - %col2Size)) @ " " @ %rowSize;
            text = "Dress it like me!";
            command = "rentabotClient_DressUpWrite(" @ %obj @ ");";
            %ctrl = ;
            %window.add(%ctrl);
            profile = GuiVariableWidthButtonCtrl @ new ""() @ "GuiFocusableVWButtonProfile";
            0;
            position = ((2.0 * mFloor((3.0 / (%colSpacing + %col2Size)))) + %col2) @ " " @ %row;
            extent = (3.0 / ((2.0 * %colSpacing) - %col2Size)) @ " " @ %rowSize;
            text = "Reset";
            command = "rentabotClient_DressUpReset(" @ %obj @ ");";
            %ctrl = ;
            %window.add(%ctrl);
        }
        %row = ((%rowSpacing + %rowSize) + %row);
    }
    ctrlName.makeFirstResponder(1);
    ctrlName.setSelection(0, 1000);
    if (isObject(ctrlBlab)) {
        altCommand = %window @ ctrlName;
        %window @ %window @ ctrlBlab @ ".makeFirstResponder(true);";
    }
    altCommand = %window @ ctrlName;
    %window @ %window @ %okayCmd @ " " @ %dlg @ ".close();";
    altCommand = %window @ ctrlBlab;
    %window @ ctrlWhisper @ ".makeFirstResponder(true);";
    altCommand = %window @ ctrlWhisper;
    %okayCmd @ " " @ %dlg @ ".close();";
};
function CustomizeBotDialog_onOkay() {
    %window = window;
    $gCustomizeBotDialog;
    %obj = rentabot;
    %window;
    %name = ctrlName.getValue();
    %window;
    %name = rentabot_getCoreName(%name);
    if (%obj.getCanSpew()) {
    }
    %msgBlab = "";
    ctrlBlab.getValue();
    if (%obj.getCanSpew()) {
    }
    %msgWhisper = "";
    ctrlWhisper.getValue();
    commandToServer('Rentabot_Customize', CustomSpaceClient::GetSpaceImIn(), %obj.getGhostID(), %name, %msgBlab, %msgWhisper);
};
function rentabotClient_DressUpRead(%obj) {
    if (!(isObject(%obj))) {
        error(getScopeName() @ " " @ "- something went wrong" @ " " @ getTrace());
        return;
    }
    %otherSkus = %obj.getActiveSKUs();
    %otherSkusOutfit = %otherSkus.filterSkusForClothing();
    SkuManager;
    %otherSkusGender = %otherSkusOutfit.filterSkusGender($player.getGender());
    SkuManager;
    %otherSkusNotOwned = wordsNotInWords($Player::inventory, %otherSkusGender);
    %otherSkusAllGood = wordsNotInWords(%otherSkusNotOwned, %otherSkusGender);
    %numLostGender = (getWordCount(%otherSkusGender) - getWordCount(%otherSkusOutfit));
    %numLostOwnership = (getWordCount(%otherSkusAllGood) - getWordCount(%otherSkusGender));
    %msg = "";
    if ((0.0 > %numLostGender)) {
        %otherGender = (%obj.getGender() $= "f") ? "female" : "male";
        %msg = %msg @ "Some of those items are for" @ " " @ %otherGender @ " " @ "players";
    }
    if ((0.0 > %numLostOwnership)) {
        if ((%msg $= "")) {
            %msg = %msg @ "You don't own some of those items";
        }
        %msg = %msg @ ", and you don't own some of those items";
    }
    if (!(%msg $= "")) {
        %msg = %msg @ "!";
        %msg = %msg[$MsgCat::furniture @ "DRESSUP-READ-CONF-NOTALL-BODY"] @ %msg;
        %tit = %msg[$MsgCat::furniture @ "DRESSUP-READ-CONF-NOTALL-TITLE"];
    }
    %msg = %tit[$MsgCat::furniture @ "DRESSUP-READ-CONF-BODY"] @ %msg;
    %tit = %msg[$MsgCat::furniture @ "DRESSUP-READ-CONF-TITLE"];
    MessageBoxYesNo(%tit, %msg, "rentabotClient_DressUpReadConfirmed(\"" @ %otherSkusAllGood @ "\");", "");
};
function rentabotClient_DressUpReadConfirmed(%skus) {
    %underSkus = ;
    %underSkus = SkuManager @ $player.getActiveSKUs().filterSkusForBody();
    %underSkus @ " ";
    %allSkus = %underSkus.overlaySkus(%skus);
    SkuManager;
    SaveOutfitAndBodySkusAsCurrent(%allSkus);
};
function rentabotClient_DressUpWrite(%obj) {
    %skus = $player.getActiveSKUs();
    MessageBoxYesNo(%skus[$MsgCat::furniture @ "DRESSUP-WRITE-CONF-TITLE"], , "rentabotClient_DressUpWriteConfirmed(" @ %obj @ ", \"" @ %skus @ "\");", "");
};
function rentabotClient_DressUpWriteConfirmed(%obj, %skus) {
    commandToServer('Rentabot_DressUpWrite', CustomSpaceClient::GetSpaceImIn(), %obj.getGhostID(), %skus);
};
function rentabotClient_DressUpReset(%obj) {
    MessageBoxYesNo(, , "rentabotClient_DressUpResetConfirmed(" @ %obj @ ");", "");
};
function rentabotClient_DressUpResetConfirmed(%obj) {
    commandToServer('Rentabot_DressUpReset', CustomSpaceClient::GetSpaceImIn(), %obj.getGhostID());
};
function rentabotClient_reignore() {
    %n = (1.0 - getWordCount($gRentabotIgnores));
    if ((0.0 >= %n)) {
        %bot = getWord($gRentabotIgnores, %n);
        if (isObject(%bot)) {
            %bot.setIgnore(1);
            %record = new ""();
            ScriptObject;
            name = 0 @ $ServerName @ %record;
            serverName = $Pref::Server::Name @ %record;
            %roled = 0;
            loggedIn = 0 @ %record;
            isIdle = 0 @ %record;
            isNPC = 1 @ %record;
            loggedIn = 0 @ %record;
            csn = %record @ serverName.getCityNameForServerName() @ %record;
            BuddyHudTabs;
            activities = "" @ %record;
            %bot.getShapeName().put(%record);
        }
        $gRentabotIgnores = findAndRemoveAllOccurrencesOfWord($gRentabotIgnores, %bot);
        UserListIgnores;
        %n = (1.0 - %n);
    }
};
